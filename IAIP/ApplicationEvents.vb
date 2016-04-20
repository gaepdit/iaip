Imports System.Collections.Generic
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) _
            Handles Me.Startup
            StartupShutdown.Init()
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) _
            Handles Me.Shutdown
            StartupShutdown.Finish()
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) _
            Handles Me.UnhandledException
            monitor.TrackException(e.Exception, sender.ToString)
            ApplicationInsights.TrackException(e.Exception, sender.ToString)
            StopMonitor()
            ApplicationInsights.StopTelemetryClient()
        End Sub
    End Class

End Namespace

