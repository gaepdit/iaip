Module AppTimers

#Region " Start and stop timers "

    Public Sub StartAppTimers()
        StartTimer(AppDurationTimer, AppDurationTimerInterval, AddressOf AppDurationTimerElapsed, False)
    End Sub

    Public Sub StopAppTimers()
        StopTimer(AppDurationTimer)
        StopTimer(ShutdownWarningTimer)
    End Sub

    ''' <summary>
    ''' Sets up and starts a Timer.
    ''' </summary>
    ''' <param name="timer">The Timer to start.</param>
    ''' <param name="interval">The time in milliseconds between events.</param>
    ''' <param name="timerElapsedEventHandler">The procedure to handle the Timer Elapsed event.</param>
    ''' <param name="autoReset">A value indicating whether the Timer Elapsed event should recur or not.</param>
    ''' <remarks></remarks>
    Private Sub StartTimer(ByRef timer As Timers.Timer, ByVal interval As TimeSpan,
                           ByVal timerElapsedEventHandler As Timers.ElapsedEventHandler,
                           Optional ByVal autoReset As Boolean = True)
        timer = New Timers.Timer(interval.TotalMilliseconds)
        AddHandler timer.Elapsed, timerElapsedEventHandler
        timer.AutoReset = autoReset
        timer.Start()
    End Sub

    ''' <summary>
    ''' Stops and disposes of a Timer.
    ''' </summary>
    ''' <param name="timer">The Timer to stop</param>
    Private Sub StopTimer(ByVal timer As Timers.Timer)
        If timer IsNot Nothing Then
            timer.Stop()
            timer.Dispose()
        End If
    End Sub

#End Region

#Region " App duration timer "

    Private AppDurationTimer As Timers.Timer
    Private AppDurationTimerInterval As TimeSpan = TimeSpan.FromHours(3) ' 3 hours

    Private ShutdownWarningTimer As Timers.Timer
    Private ShutdownWarningTimerInterval As TimeSpan = TimeSpan.FromMinutes(5) ' 5 minutes

    Private Sub AppDurationTimerElapsed()
        monitor.TrackFeature("Timers.AppDurationTimer")
        StartTimer(ShutdownWarningTimer, ShutdownWarningTimerInterval,
                   AddressOf ShutdownWarningTimerElapsed, False)

        Dim result As DialogResult
        result = MessageBox.Show("The IAIP has been open for three hours. " & vbNewLine &
                                 "Do you want to continue to use it?" & vbNewLine &
                                    vbNewLine &
                                 "(The IAIP will be automatically terminated " & vbNewLine &
                                 "in five minutes.)",
                                 "Are you still there?", MessageBoxButtons.YesNo,
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
        monitor.TrackFeature("Timers.ShutdownWarningTimer")
        StartupShutdown.CloseIaip()
    End Sub

#End Region

End Module
