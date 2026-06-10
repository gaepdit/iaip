Imports System.Deployment.Application
Imports Microsoft.VisualBasic.ApplicationServices
Imports Sentry
Imports Sentry.Protocol

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private _sentry As IDisposable

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) _
            Handles Me.Startup

            ' Initialize Sentry
            Dim sentryOptions As New SentryOptions With
            {
                .Release = GetCurrentVersion.ToString(),
                .Dsn = "https://ff64fd15e76aa7fc7878695979472555@o104051.ingest.us.sentry.io/4511540846198784",
                .SendDefaultPii = True,
                .TracesSampleRate = 1.0,
                .IsGlobalModeEnabled = True,
                .AutoSessionTracking = True
            }
            _sentry = SentrySdk.Init(sentryOptions)

            e.Cancel = Not StartupShutdown.Init()
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) _
            Handles Me.Shutdown

            StartupShutdown.Finish()

            _sentry.Dispose()
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) _
            Handles Me.UnhandledException

            ' Set some additional data on the exception for Sentry to recognize this exception as unhandled
            Dim ex As Exception = e.Exception
            ex.Data(Mechanism.HandledKey) = False
            ex.Data(Mechanism.MechanismKey) = "WindowsFormsApplicationBase.UnhandledException"

            e.ExitApplication = ExceptionManager.ErrorReport(
                ex,
                sender.ToString,
                NameOf(MyApplication_UnhandledException),
                True,
                e.ExitApplication)
        End Sub

    End Class

End Namespace
