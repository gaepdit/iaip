Imports System.Collections.Generic
Imports EQATEC.Analytics.Monitor

Module Analytics
    ' Initialize and set up EQATEC.Analytics.Monitor Library

    Public monitor As IAnalyticsMonitor
    Public monitorInstallationInfo As New Dictionary(Of String, String)

    Public Sub InitializeMonitor()
        Dim AnalyticsApiKey As String = "F635C42ABA7B4DE886EEFCEE31C98000"
        Dim monitorSettings As IAnalyticsMonitorSettings = AnalyticsMonitorFactory.CreateSettings(AnalyticsApiKey)

#If DEBUG Then
        monitorSettings.TestMode = True
#End If

        monitorSettings.UseSSL = True

        monitor = AnalyticsMonitorFactory.Create(monitorSettings)
        With monitor

#If Not BETA Then
            .Start()
#End If

            .TrackFeatureStart("Runtime")
            .TrackFeatureStart("Startup.Loading")
        End With

        ' Add additional installation meta data for analytics
        With monitorInstallationInfo
            .Add("MachineName", Environment.MachineName)
            .Add("WindowsUserName", Environment.UserName)
        End With
        monitor.SetInstallationInfo(Environment.MachineName, monitorInstallationInfo)
    End Sub

    Public Sub AddMonitorLoginData()
        ' Add additional installation meta data for analytics
        monitorInstallationInfo.Add("IaipUsername", CurrentUser.Username)
        monitor.SetInstallationInfo(CurrentUser.Username, monitorInstallationInfo)
        If (CurrentServerEnvironment <> DB.DefaultServerEnvironment) Then
            monitor.TrackFeature("Main.TestingEnvironment")
        End If
        monitor.ForceSync()
    End Sub

    Public Sub StopMonitor()
        With monitor
            .TrackFeatureStop("Runtime")
            .Stop()
            .Dispose()
        End With
        monitorInstallationInfo.Clear()
    End Sub

End Module
