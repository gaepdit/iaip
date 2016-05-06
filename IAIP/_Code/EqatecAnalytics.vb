﻿Imports System.Collections.Generic
Imports EQATEC.Analytics.Monitor

Module EqatecAnalytics
    ' Initialize and set up EQATEC.Analytics.Monitor Library

    Public monitor As IAnalyticsMonitor
    Public monitorInstallationInfo As New Dictionary(Of String, String)

    Public Sub InitializeMonitor()
        Dim monitorSettings As IAnalyticsMonitorSettings = AnalyticsMonitorFactory.CreateSettings(EqatecAnalyticsApiKey)

#If DEBUG Or BETA Then
        monitorSettings.TestMode = True
#End If

        monitorSettings.UseSSL = True
        monitor = AnalyticsMonitorFactory.Create(monitorSettings)
        With monitor
            .Start()
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
#If DEBUG Then
        monitor.TrackFeature("Main.TestingEnvironment")
#ElseIf UAT Then
        monitor.TrackFeature("Main.UatEnvironment")
#Else
        monitor.TrackFeature("Main.ProductionEnvironment")
#End If
        monitor.ForceSync()
        ApplicationInsights.UpdateTelemetryClientUser()
    End Sub

    Public Sub StopMonitor()
        If monitor IsNot Nothing Then
            With monitor
                .TrackFeatureStop("Runtime")
                .Stop()
                .Dispose()
            End With
        End If
        monitorInstallationInfo.Clear()
    End Sub

End Module