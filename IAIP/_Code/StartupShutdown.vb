Friend Module StartupShutdown

    ''' <summary>
    ''' All the procedures to run as the application is starting up
    ''' </summary>
    ''' <remarks> Called by MyApplication_Startup -> StartupShutdown.Init() </remarks>
    Friend Sub Init()
        AddHandler Application.ThreadException, AddressOf IaipExceptionManager.Application_ThreadException

        ' Updates: Should run each time program is updated
        If App.JustUpdated Then
            ' Settings.Upgrade() folds in settings from previous version
            My.Settings.Upgrade()
            AppUpdated = True
        End If

        ' First Run: Should only run the first time a new installation is run
        ' (A 'False' setting for My.Settings.JustInstalled should be migrated by My.Settings.Upgrade() above
        ' before getting here.)
        If My.Settings.JustInstalled Then
            AppFirstRun = True

            DeleteOldShortcuts()

            ' Prevents this from running in the future
            My.Settings.JustInstalled = False
            My.Settings.Save()
        End If

        ' Language settings
        CheckLanguageRegistrySetting()

        ' DB Environment
        SetUpDbServerEnvironment()

        ' EQATEC analytics monitor
        InitializeMonitor()

        ' Initialize form settings
        AllFormSettings = GetAllFormSettings()

    End Sub

    ''' <summary>
    ''' All the procedures to run as the application is shutting down
    ''' </summary>
    ''' <remarks> Called by MyApplication_Shutdown -> StartupShutdown.Finish() </remarks>
    Friend Sub Finish()

        ' Timers
        AppTimers.StopAppTimers()

        ' Form settings
        SaveAllFormSettings()

        ' EQATEC analytics monitor
        StopMonitor()

    End Sub

    ''' <summary>
    ''' Deletes old IAIP shortcuts from user's Desktop and Start Menu
    ''' </summary>
    ''' <remarks>Actually moves them to Recycle Bin</remarks>
    Private Sub DeleteOldShortcuts()
        Dim shortcutName As String = "\IAIP.lnk"

        DeleteFileIfPossible(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & shortcutName)
        DeleteFileIfPossible(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) & shortcutName)
        DeleteFileIfPossible(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & shortcutName)
        DeleteFileIfPossible(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) & shortcutName)
    End Sub

    ''' <summary>
    ''' Shuts down the running IAIP application
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CloseIaip()
        If CurrentConnection IsNot Nothing Then CurrentConnection.Dispose()
        Application.Exit()
    End Sub

    Friend Sub LogOutUser()
        CurrentUser = Nothing
        Array.Clear(AccountFormAccess, 0, AccountFormAccess.Length)
        monitor.TrackFeature("Main.LogOut")
        StopMonitor()
        InitializeMonitor()
    End Sub

    Private Sub CheckLanguageRegistrySetting()
        Dim currentSetting As String
        currentSetting = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", Nothing)
        If currentSetting Is Nothing Or currentSetting <> "AMERICAN" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", "AMERICAN")
            Application.Restart()
        End If
    End Sub

    Private Sub SetUpDbServerEnvironment()

        ' Set current server environment based on project build parameters
        CurrentServerEnvironment = ServerEnvironment.PRD
#If DEBUG Then
        CurrentServerEnvironment = ServerEnvironment.DEV
#ElseIf UAT Then
        CurrentServerEnvironment = ServerEnvironment.UAT
#End If

        ' Set one single open connection based on current server environment
        ' This method is deprecated, but is still used throughout the project
        CurrentConnection = New SqlClient.SqlConnection(CurrentConnectionString)

        ' Create EpdIt.DBHelper object based on current server environment
        ' This method is preferred and should be used for all future work
        DB = New EpdIt.DBHelper(CurrentConnectionString)
    End Sub

End Module
