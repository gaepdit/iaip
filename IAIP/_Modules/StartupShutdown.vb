Module StartupShutdown

    ''' <summary>
    ''' All the procedures to run as the application is starting up
    ''' </summary>
    ''' <remarks> Called by MyApplication_Startup -> StartupShutdown.Init() </remarks>
    Friend Sub Init()
        AddHandler Application.ThreadException, AddressOf IaipExceptionManager.Application_ThreadException

        ' Upgrades: Should run each time program is upgraded
        If My.Settings.CallUpgrade Then
            AppUpgraded = True

            ' Put items to run before settings are migrated here
            ' [None currently]

            ' Upgrade() folds in settings from previous version
            My.Settings.Upgrade()

            ' Put items to run after settings are migrated here
            ' [None currently]

            ' Prevents this from running until next upgrade
            My.Settings.CallUpgrade = False
        End If

        ' First Run: Should only run the first time a new installation is run
        ' (A 'False' setting for My.Settings.FirstRun should be migrated by My.Settings.Upgrade() above
        ' before getting here.)
        If My.Settings.FirstRun Then
            AppFirstRun = True

            ' Put items to run on first installation here
            DeleteOldShortcuts()

            ' Prevents this from running in the future
            My.Settings.FirstRun = False
        End If

        My.Settings.Save()

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

End Module
