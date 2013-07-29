Imports EQATEC.Analytics.Monitor

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Friend monitor As IAnalyticsMonitor = AnalyticsMonitorFactory.CreateMonitor("094F22FFB35C42E9A5D65279634F5028")

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) _
            Handles Me.Startup

            monitor.Start()

            '' Just for fun: sample code for a new way of handling login form
            '' http://visualstudiomagazine.com/articles/2008/08/01/customize-your-application-startup.aspx
            'Dim frm = New LoginForm
            'Dim result = frm.ShowDialog
            'If result <> DialogResult.OK Then
            '    e.Cancel = True
            '    Me.HideSplashScreen()
            'End If
        End Sub

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            monitor.Stop()
        End Sub

    End Class

End Namespace

