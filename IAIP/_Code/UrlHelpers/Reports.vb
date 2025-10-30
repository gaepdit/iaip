Imports System.Configuration

Namespace UrlHelpers

    Public Module Reports

        Private ReadOnly ReportsUrl As New Uri(ConfigurationManager.AppSettings("AirWebUrl"))

        Public Sub OpenAccUrl(id As Integer, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"print/acc/{id}")
            DAL.LogReportUsage("acc", url)
            OpenUrl(url, sender)
        End Sub

        Public Sub OpenFceUrl(id As Integer, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"print/fce/{id}")
            DAL.LogReportUsage("fce", url)
            OpenUrl(url, sender)
        End Sub

        Public Sub OpenStackTestUrl(referenceNumber As Integer, includeConfidentialInfo As Boolean, Optional sender As Form = Nothing)
            Dim queryString As String = If(includeConfidentialInfo, "?includeConfidentialInfo=true", "")
            Dim url As New Uri(ReportsUrl, $"print/source-test/{referenceNumber}{queryString}")
            DAL.LogReportUsage("stack test", url)
            OpenUrl(url, sender)
        End Sub

    End Module

End Namespace