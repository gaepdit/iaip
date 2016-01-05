Imports System.Collections.Generic
Imports System.Globalization
Imports Microsoft.ApplicationInsights
Imports Microsoft.ApplicationInsights.Extensibility

Friend Class ApplicationInsights

    Private Sub New()
    End Sub

    Public Shared telemetryClient As TelemetryClient

    Public Shared Sub InitializeTelemetryClient()
        telemetryClient = New TelemetryClient

        telemetryClient.InstrumentationKey = "fc9b3ee9-eae9-4af4-a62d-ca041194890d"


        With telemetryClient.Context
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
        AddTelemetryProperty(TelemetryProperty.DebugMode, True.ToString)
#Else
        AddTelemetryProperty(TelemetryProperty.DebugMode, False.ToString)
#End If

#If BETA Then
        AddTelemetryProperty(TelemetryProperty.ReleaseChannel, "Beta")
#Else
        AddTelemetryProperty(TelemetryProperty.ReleaseChannel, "Production")
#End If

        AddTelemetryProperty(TelemetryProperty.OS64Bit, Environment.Is64BitOperatingSystem.ToString)
        AddTelemetryProperty(TelemetryProperty.ProcessorCount, Environment.ProcessorCount.ToString)
        AddTelemetryProperty(TelemetryProperty.ClrVersion, Environment.Version.ToString)

        telemetryClient.TrackEvent("Application Start")
        telemetryClient.Flush()
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
        If telemetryClient IsNot Nothing Then
            telemetryClient.TrackEvent("Application End")
            telemetryClient.Flush()
        End If
    End Sub

    Public Shared Sub UpdateTelemetryUser()
        With telemetryClient.Context
            .User.AuthenticatedUserId = CurrentUser.UserID
            AddTelemetryProperty(TelemetryProperty.IaipUserName, CurrentUser.UserName)
            AddTelemetryProperty(TelemetryProperty.ServerEnvironment, CurrentServerEnvironment.ToString)
        End With
        telemetryClient.Flush()
    End Sub

    Public Shared Sub AddTelemetryProperty(prop As TelemetryProperty, value As String)
        telemetryClient.Context.Properties.Add(prop.ToString, value)
    End Sub

    Public Shared Sub TrackException(exc As Exception, context As String)
        TrackException(exc, Nothing, context)
    End Sub

    Public Shared Sub TrackException(exc As Exception, message As String, context As String)
        Dim telemetryProperties As New Dictionary(Of String, String)
        telemetryProperties.Add("Context", context)
        If message IsNot Nothing Then
            telemetryProperties.Add("SupplementalMessage", message)
        End If

        telemetryClient.TrackException(exc, telemetryProperties)
    End Sub

End Class

Public Enum TelemetryProperty
    IaipUserName
    ServerEnvironment
    OS64Bit
    ProcessorCount
    ClrVersion
    ReleaseChannel
    DebugMode
End Enum