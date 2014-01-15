﻿Module StartupShutdown

    ' MyApplication_Startup -> App.Init()
    Friend Sub Init()

#If DEBUG Then
        Console.WriteLine("Me.Startup")
        Console.WriteLine("Environment.MachineName: " & Environment.MachineName)
        Console.WriteLine("Environment.UserName: " & Environment.UserName)
#End If

        ' Settings
        ' Should run each time program is upgraded
        ' Upgrade() folds in settings from previous version
        If My.Settings.CallUpgrade Then
            My.Settings.Upgrade()
            My.Settings.CallUpgrade = False
        End If

        ' First Run
        ' Should only run the first time a new installation is run
        ' The False setting should be migrated by above Upgrade()
        If My.Settings.FirstRun Then
            FirstRun()
        End If

        ' EQATEC analytics monitor
        MonitorInit()

        ' Form settings
        AllFormSettings = GetAllFormSettings()

    End Sub

    ' MyApplication_Shutdown -> App.Finish()
    Friend Sub Finish()

        ' Form settings
        SaveAllFormSettings()

        ' EQATEC analytics monitor
        MonitorStop()

    End Sub

    ''' <summary>
    ''' Should only execute the first time a new installation is run
    ''' </summary>
    ''' <remarks>
    ''' Flips FirstRun setting to false when run
    ''' Can be used for setup requirements
    ''' + Deletes old JohnGa1tProject shortcut icons
    ''' </remarks>
    Friend Sub FirstRun()
        If Not My.Settings.FirstRun Then Exit Sub

        DeleteOldShortcuts()

        My.Settings.FirstRun = False
    End Sub

    ''' <summary>
    ''' Deletes old IAIP shortcuts from user's Desktop and Start Menu
    ''' </summary>
    ''' <remarks>Actually moves them to Recycle Bin</remarks>
    Friend Sub DeleteOldShortcuts()
        Dim shortcutName As String = "\IAIP.lnk"

        DeleteFileIfPossible(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & shortcutName)
        DeleteFileIfPossible(GetAllUsersDesktopPath() & shortcutName)
        DeleteFileIfPossible(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & shortcutName)
        DeleteFileIfPossible(GetAllUsersStartMenuPath() & shortcutName)
    End Sub

End Module
