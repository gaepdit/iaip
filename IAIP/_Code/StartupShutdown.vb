Imports System.Configuration
Imports System.Deployment.Application

Friend Module StartupShutdown

    ''' <summary>
    ''' All the procedures to run as the application is starting up
    ''' </summary>
    ''' <remarks> Called by MyApplication_Startup -> StartupShutdown.Init() </remarks>
    Friend Function Init() As Boolean
        AddHandler Application.ThreadException, AddressOf Application_ThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException

        If CheckForUpdate() Then Return False

        ' Updates: Should run each time program is updated
        If ApplicationDeployment.IsNetworkDeployed AndAlso ApplicationDeployment.CurrentDeployment.IsFirstRun Then
            ' Settings.Upgrade() folds in settings from previous version
            My.Settings.Upgrade()
        End If

        ' DB Environment
        SetUpDbServerEnvironment()

        ' Initialize form settings
        AllFormSettings = GetAllFormSettings()

        Return True
    End Function

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

    Private Sub Application_ThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
        ErrorReport(e.Exception, sender.ToString, NameOf(Application_ThreadException), True, True)
    End Sub

    Private Sub CurrentDomain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        ErrorReport(CType(e.ExceptionObject, Exception), sender.ToString, NameOf(CurrentDomain_UnhandledException), True, e.IsTerminating)
    End Sub

End Module
