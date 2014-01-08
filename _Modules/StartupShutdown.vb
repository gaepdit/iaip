Module StartupShutdown

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

End Module
