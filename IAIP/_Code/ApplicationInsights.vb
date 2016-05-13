Imports System.Collections.Generic
Imports System.Globalization
Imports Microsoft.ApplicationInsights
Imports Microsoft.ApplicationInsights.DataContracts
Imports Microsoft.ApplicationInsights.Extensibility

Friend Class ApplicationInsights

    Private Shared iaipTelemetryClient As TelemetryClient

    Private Sub New()
    End Sub

    Public Shared Sub InitializeTelemetryClient()
        iaipTelemetryClient = New TelemetryClient

        iaipTelemetryClient.InstrumentationKey = ApplicationInsightsKey


        With iaipTelemetryClient.Context
            .Session.Id = Guid.NewGuid.ToString
            .Session.IsFirst = AppFirstRun

            .Component.Version = Application.ProductVersion

            .Device.Id = Environment.MachineName
            .Device.Language = CultureInfo.CurrentUICulture.IetfLanguageTag
            .Device.OperatingSystem = Environment.OSVersion.ToString
            .Device.ScreenResolution = GetScreenResolution()

            .User.Id = Environment.UserName
        End With

#If DEBUG Then
        TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = True
        AddTelemetryClientProperty(TelemetryContextProperty.DebugMode, True.ToString)
#ElseIf UAT Then
        TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = True
        AddTelemetryClientProperty(TelemetryContextProperty.DebugMode, True.ToString)
#Else
        AddTelemetryClientProperty(TelemetryContextProperty.DebugMode, False.ToString)
#End If

#If UAT Then
        AddTelemetryClientProperty(TelemetryContextProperty.ReleaseChannel, "UAT")
#Else
        AddTelemetryClientProperty(TelemetryContextProperty.ReleaseChannel, "Production")
#End If

        AddTelemetryClientProperty(TelemetryContextProperty.OS64Bit, Environment.Is64BitOperatingSystem.ToString)
        AddTelemetryClientProperty(TelemetryContextProperty.ProcessorCount, Environment.ProcessorCount.ToString)
        AddTelemetryClientProperty(TelemetryContextProperty.ClrVersion, Environment.Version.ToString)

        iaipTelemetryClient.TrackEvent("Application Start")
        iaipTelemetryClient.Flush()
    End Sub

    Private Shared Function GetScreenResolution() As String
        If Screen.AllScreens.Length > 1 Then
            Dim screenData As New Text.StringBuilder()
            Dim scr As Screen
            For i As Integer = 0 To Screen.AllScreens.Length - 1
                scr = Screen.AllScreens(i)
                screenData.AppendLine(String.Format("[{0}] {1}×{2}", i, scr.Bounds.Width, scr.Bounds.Height))
            Next
            Return screenData.ToString()
        Else
            Return String.Concat(Screen.PrimaryScreen.Bounds.Width, "×", Screen.PrimaryScreen.Bounds.Height)
        End If
    End Function

    Public Shared Sub StopTelemetryClient()
        If iaipTelemetryClient IsNot Nothing Then
            iaipTelemetryClient.TrackEvent("Application End")
            iaipTelemetryClient.Flush()
        End If
    End Sub

    Public Shared Sub UpdateTelemetryClientUser()
        With iaipTelemetryClient.Context
            .User.AuthenticatedUserId = CurrentUser.UserID
            AddTelemetryClientProperty(TelemetryContextProperty.IaipUserName, CurrentUser.UserName)
            AddTelemetryClientProperty(TelemetryContextProperty.ServerEnvironment, CurrentServerEnvironment.ToString)
        End With
        iaipTelemetryClient.Flush()
    End Sub

    Public Shared Sub AddTelemetryClientProperty(prop As TelemetryContextProperty, value As String)
        If iaipTelemetryClient.Context.Properties.ContainsKey(prop.ToString) Then
            iaipTelemetryClient.Context.Properties.Remove(prop.ToString)
        End If
        iaipTelemetryClient.Context.Properties.Add(prop.ToString, value)
    End Sub

#Region " General use methods "

#Region " Track Exceptions "
    Public Shared Sub TrackException(exc As Exception, context As String)
        TrackException(exc, Nothing, context)
    End Sub

    Public Shared Sub TrackException(exc As Exception, message As String, context As String)
        Dim telemetryProperties As New Dictionary(Of String, String)
        telemetryProperties.Add("Context", context)
        If message IsNot Nothing Then
            telemetryProperties.Add("SupplementalMessage", message)
        End If

        iaipTelemetryClient.TrackException(exc, telemetryProperties)
    End Sub

#End Region

#Region " Track Page Views "

    Public Shared Sub TrackPageView(pageViewType As TelemetryPageViewType, name As String)
        iaipTelemetryClient.TrackPageView(pageViewType.ToString & "." & name)
    End Sub

    Public Shared Sub TrackPageView(pageViewType As TelemetryPageViewType, name As String, duration As TimeSpan)
        Dim pageViewTelemetryData As New PageViewTelemetry(pageViewType.ToString & "." & name)
        If Not duration.Equals(TimeSpan.Zero) Then pageViewTelemetryData.Duration = duration
        iaipTelemetryClient.TrackPageView(pageViewTelemetryData)
    End Sub

#End Region

#Region " Track Events "

    Public Shared Sub TrackEvent(eventName As String, Optional properties As IDictionary(Of String, String) = Nothing, Optional metrics As IDictionary(Of String, Double) = Nothing)
        iaipTelemetryClient.TrackEvent(eventName, properties, metrics)
    End Sub

    Public Shared Sub TrackEvent(eventName As String, propertyName As String, value As String)
        Dim properties As New Dictionary(Of String, String) From {{propertyName, value}}
        iaipTelemetryClient.TrackEvent(eventName, properties)
    End Sub

#End Region

#Region " Track Dependencies "

    Public Shared Sub TrackDependency(dependencyType As TelemetryDependencyType, dependencyName As String, commandName As String, startTime As DateTimeOffset, duration As TimeSpan, success As Boolean)
        iaipTelemetryClient.TrackDependency(dependencyType.ToString & "." & dependencyName, commandName, startTime, duration, success)
    End Sub

#End Region

#End Region

End Class

Public Enum TelemetryContextProperty
    IaipUserName
    ServerEnvironment
    OS64Bit
    ProcessorCount
    ClrVersion
    ReleaseChannel
    DebugMode
End Enum

Public Enum TelemetryPageViewType
    IaipForms
    IaipCrReport
    IaipFacilitySummaryTab
End Enum

Public Enum TelemetryDependencyType
    Oracle
End Enum