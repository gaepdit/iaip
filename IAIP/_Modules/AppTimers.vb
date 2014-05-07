Module AppTimers

#Region " Start and stop timers "

    Public Sub StartAppTimers()
        StartTimer(AppDurationTimer, AppDurationTimerInterval.TotalMilliseconds, AddressOf AppDurationTimerElapsed, False)
        StartTimer(DbPingTimer, DbPingTimerInterval.TotalMilliseconds, AddressOf DbPingTimerElapsed)
        'StartTimer(DbPingTimer, 1000 * 60 * 2, AddressOf DbPingTimerElapsed) ' 2 minutes (for testing purposes)
    End Sub

    Public Sub StopAppTimers()
        StopTimer(AppDurationTimer)
        StopTimer(ShutdownWarningTimer)
        StopTimer(DbPingTimer)
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
    Private DbPingTimerInterval As TimeSpan = TimeSpan.FromMinutes(45) ' 45 minutes 

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
    Private AppDurationTimerInterval As TimeSpan = TimeSpan.FromHours(3) ' 3 hours

    Private ShutdownWarningTimer As Timers.Timer
    Private ShutdownWarningTimerInterval As TimeSpan = TimeSpan.FromMinutes(5) ' 5 minutes

    Private Sub AppDurationTimerElapsed()
        StartTimer(ShutdownWarningTimer, ShutdownWarningTimerInterval.TotalMilliseconds, _
                   AddressOf ShutdownWarningTimerElapsed, False)

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

End Module
