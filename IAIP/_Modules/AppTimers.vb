Module AppTimers

#Region " Start and stop timers "

    Public Sub StartAppTimers()
        StartTimer(AppDurationTimer, 1000 * 60 * 60 * 3, AddressOf AppDurationTimerElapsed, False) ' 3 hours 
        StartTimer(DbPingTimer, 1000 * 60 * 45, AddressOf DbPingTimerElapsed) ' 45 minutes 
        'StartTimer(DbPingTimer, 1000 * 60 * 2, AddressOf DbPingTimerElapsed) ' 2 minutes (for testing purposes)
        StartNadcCutoverTimer()
    End Sub

    Public Sub StopAppTimers()
        StopTimer(AppDurationTimer)
        StopTimer(DbPingTimer)
        StopTimer(ShutdownWarningTimer)
        StopTimer(NadcCutoverTimer)
    End Sub

    ''' <summary>
    ''' Sets up and starts a Timer.
    ''' </summary>
    ''' <param name="timer">The Timer to start.</param>
    ''' <param name="interval">The time in milliseconds between events.</param>
    ''' <param name="timerElapsedEventHandler">The procedure to handle the Timer Elapsed event.</param>
    ''' <param name="autoReset">A value indicating whether the Timer Elapsed event should recur or not.</param>
    ''' <remarks></remarks>
    Private Sub StartTimer(ByRef timer As Timers.Timer, ByVal interval As Double, _
                           ByVal timerElapsedEventHandler As Timers.ElapsedEventHandler, _
                           Optional ByVal autoReset As Boolean = True)
        timer = New Timers.Timer(interval)
        AddHandler timer.Elapsed, timerElapsedEventHandler
        timer.Enabled = True
        timer.AutoReset = autoReset
    End Sub

    ''' <summary>
    ''' Stops and disposes of a Timer.
    ''' </summary>
    ''' <param name="timer">The Timer to stop</param>
    Private Sub StopTimer(ByVal timer As Timers.Timer)
        If timer IsNot Nothing Then
            timer.Enabled = False
            timer.Dispose()
        End If
    End Sub

#End Region

#Region " Database ping timer "

    Private DbPingTimer As Timers.Timer

    Private Sub DbPingTimerElapsed()
        Dim result As Boolean = DB.PingDBConnection(CurrentConnection)
        If Not result Then
            MessageBox.Show("The database connection has been lost. " & vbNewLine & _
                            "Please close and restart the IAIP.", _
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

#End Region

#Region " App duration timer "

    Private AppDurationTimer As Timers.Timer
    Private ShutdownWarningTimer As Timers.Timer

    Private Sub AppDurationTimerElapsed()
        StartTimer(ShutdownWarningTimer, 1000 * 60 * 5, AddressOf ShutdownWarningTimerElapsed, False) ' 5 minutes 

        Dim result As DialogResult
        result = MessageBox.Show("The IAIP has been open for three hours. " & vbNewLine & _
                                 "Do you want to continue to use it?" & vbNewLine & _
                                    vbNewLine & _
                                 "(The IAIP will be automatically terminated " & vbNewLine & _
                                 "in five minutes.)", _
                                 "Are you still there?", MessageBoxButtons.YesNo, _
                                 MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

        Select Case result
            Case DialogResult.Yes
                StopTimer(ShutdownWarningTimer)
                AppDurationTimer.Start()
            Case DialogResult.No
                StartupShutdown.CloseIaip()
        End Select

    End Sub

    Private Sub ShutdownWarningTimerElapsed()
        StartupShutdown.CloseIaip()
    End Sub

#End Region

#Region " NADC cutover timer "

    Private NadcCutoverTimer As Timers.Timer
    Private Enum NadcCutoverTimerState
        Init
        FirstWarning
        FinalWarning
    End Enum
    Private NadcCutoverTimerStatus As NadcCutoverTimerState = NadcCutoverTimerState.Init

    Private Sub StartNadcCutoverTimer()
        If DateTime.Now < DB.NADC_CUTOVER_DATETIME Then
            ' First time span ends 30 minutes before NADC_CUTOVER_DATETIME
            Dim span As TimeSpan = (DB.NADC_CUTOVER_DATETIME - DateTime.Now).Subtract(TimeSpan.FromMinutes(30))

            StartTimer(NadcCutoverTimer, span.TotalMilliseconds, AddressOf NadcCutoverTimerElapsed, False)
            NadcCutoverTimerStatus = NadcCutoverTimerState.Init
        End If
    End Sub

    Private Sub NadcCutoverTimerElapsed()
        Select Case NadcCutoverTimerStatus

            Case NadcCutoverTimerState.Init ' Occurs 30 minutes before NADC_CUTOVER_DATETIME
                If DateTime.Now < DB.NADC_CUTOVER_DATETIME Then
                    ' Second time span ends 5 minutes before NADC_CUTOVER_DATETIME
                    Dim span As TimeSpan = (DB.NADC_CUTOVER_DATETIME - DateTime.Now).Subtract(TimeSpan.FromMinutes(5))

                    StartTimer(NadcCutoverTimer, span.TotalMilliseconds, AddressOf NadcCutoverTimerElapsed, False)
                    NadcCutoverTimerStatus = NadcCutoverTimerState.FirstWarning

                    MessageBox.Show("The IAIP will shut down at 5pm EDT on Friday, " & vbNewLine & _
                                    "May 2, 2014. Service will resume on Monday " & vbNewLine & _
                                    "morning (May 5). Please finish your work. " & vbNewLine & _
                                    vbNewLine & _
                                    "Thank you for your patience and understanding. ", _
                                    "Service interruption: 30-minute warning", MessageBoxButtons.OK, _
                                             MessageBoxIcon.Warning)
                End If

            Case NadcCutoverTimerState.FirstWarning ' Occurs 5 minutes before NADC_CUTOVER_DATETIME
                If DateTime.Now < DB.NADC_CUTOVER_DATETIME Then
                    ' Third time span ends at NADC_CUTOVER_DATETIME
                    Dim span As TimeSpan = (DB.NADC_CUTOVER_DATETIME - DateTime.Now)

                    StartTimer(NadcCutoverTimer, span.TotalMilliseconds, AddressOf NadcCutoverTimerElapsed, False)
                    NadcCutoverTimerStatus = NadcCutoverTimerState.FinalWarning

                    MessageBox.Show("The IAIP will shut down at 5pm EDT on Friday, " & vbNewLine & _
                                    "May 2, 2014. Service will resume on Monday " & vbNewLine & _
                                    "morning (May 5). Please finish your work and " & vbNewLine & _
                                    "close the IAIP. " & vbNewLine & _
                                    vbNewLine & _
                                    "This is your final notice. " & vbNewLine & _
                                    vbNewLine & _
                                    "Thank you for your patience and understanding. ", _
                                    "Service interruption: 5-MINUTE WARNING", MessageBoxButtons.OK, _
                                             MessageBoxIcon.Warning)
                End If

            Case NadcCutoverTimerState.FinalWarning ' Occurs at NADC_CUTOVER_DATETIME
                StartupShutdown.CloseIaip()

        End Select
    End Sub

#End Region

End Module
