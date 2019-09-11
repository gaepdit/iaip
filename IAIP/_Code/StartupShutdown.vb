Imports System.Configuration
Imports System.Net

Friend Module StartupShutdown

    ''' <summary>
    ''' All the procedures to run as the application is starting up
    ''' </summary>
    ''' <remarks> Called by MyApplication_Startup -> StartupShutdown.Init() </remarks>
    Friend Sub Init()
        AddHandler Application.ThreadException, AddressOf IaipExceptionManager.ApplicationThreadException

        ' Updates: Should run each time program is updated
        If AppVersion.JustUpdated Then
            ' Settings.Upgrade() folds in settings from previous version
            My.Settings.Upgrade()
            AppUpdated = True
        End If

        ' First Run: Should only run the first time a new installation is run
        ' (A 'False' setting for My.Settings.JustInstalled should be migrated by My.Settings.Upgrade() above
        ' before getting here.)
        If My.Settings.JustInstalled Then
            AppFirstRun = True

            ' Prevents this from running in the future
            My.Settings.JustInstalled = False
            My.Settings.Save()
        End If

        ' Enable TLS
        EnableTLS()

        ' DB Environment
        SetUpDbServerEnvironment()

        ' Start exception monitor
        SetUpExceptionLogger()

        ' Initialize form settings
        AllFormSettings = GetAllFormSettings()

    End Sub

    ''' <summary>
    ''' All the procedures to run as the application is shutting down
    ''' </summary>
    ''' <remarks> Called by MyApplication_Shutdown -> StartupShutdown.Finish() </remarks>
    Friend Sub Finish()
        ' Form settings
        SaveAllFormSettings()
    End Sub

    ''' <summary>
    ''' Shuts down the running IAIP application
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CloseIaip()
        IaipExiting = True
        Application.Exit()
    End Sub

    Friend Sub LogOutUser()
        UpdateSession(False)
        CurrentUser = Nothing
        Array.Clear(AccountFormAccess, 0, AccountFormAccess.Length)
        ' Remove username from analytics
        ExceptionLogger.Tags.Remove("IaipUser")
        ExceptionLogger.Tags.Remove("IaipUserID")
    End Sub

    Private Sub SetUpDbServerEnvironment()
        ' Set current server environment based on environment
        If Not [Enum].TryParse(ConfigurationManager.AppSettings("Environment"), CurrentServerEnvironment) Then
            CloseIaip()
        End If

        ' Create EpdIt.DBHelper object based on current server environment
        ' This method is preferred and should be used for all future work
        DB = New EpdIt.DBHelper(CurrentConnectionString)
    End Sub

    Private Sub SetUpExceptionLogger()
        ExceptionLogger = New SharpRaven.RavenClient(SENTRY_DSN) With {
            .Environment = CurrentServerEnvironment.ToString,
            .Release = GetCurrentVersionAsMajorMinorBuild().ToString
        }
    End Sub

    Private Sub EnableTLS()
        ' Enable newer TLS protocols. This should be removed if the IAIP is transitioned to a 
        ' newer version of .NET Framework.
        ' Refs: 
        ' Transport Layer Security (TLS) best practices with the .NET Framework | Microsoft Docs
        ' https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls
        ' Your .NET Code Could Stop Working in June 2018 - Kyle Gagnet - Medium
        ' https://medium.com/@kyle.gagnet/your-net-code-could-stop-working-in-june-afb35fbf29ca
        ' SecurityProtocolType Enum (System.Net) | Microsoft Docs
        ' https://docs.microsoft.com/en-us/dotnet/api/system.net.securityprotocoltype?view=netframework-4.5.2
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
    End Sub

End Module
