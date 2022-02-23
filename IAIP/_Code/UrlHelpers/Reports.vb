Imports System.Configuration
Imports Iaip.Apb

Namespace UrlHelpers

    Public Module Reports

        Private ReadOnly ReportsUrl As New Uri(ConfigurationManager.AppSettings("AirReportsUrl"))

        Public Function GetAccUrl(facilityId As ApbFacilityId, year As Integer) As Uri
            Return New Uri(ReportsUrl, $"facility/{facilityId.FormattedString}/acc-report/{year}")
        End Function

        Public Sub OpenAccUrl(facilityId As ApbFacilityId, year As Integer, Optional sender As Form = Nothing)
            Dim url As Uri = GetAccUrl(facilityId, year)
            DAL.LogReportUsage("acc", url.ToString)
            OpenUrl(url, sender)
        End Sub

        Public Function GetFceUrl(facilityId As ApbFacilityId, id As Integer) As Uri
            Return New Uri(ReportsUrl, $"facility/{facilityId.FormattedString}/fce/{id}")
        End Function

        Public Sub OpenFceUrl(facilityId As ApbFacilityId, id As Integer, Optional sender As Form = Nothing)
            Dim url As Uri = GetFceUrl(facilityId, id)
            DAL.LogReportUsage("fce", url.ToString)
            OpenUrl(url, sender)
        End Sub

        Public Function GetStackTestUrl(facilityId As ApbFacilityId, referenceNumber As Integer) As Uri
            Return New Uri(ReportsUrl, $"facility/{facilityId.FormattedString}/stack-test/{referenceNumber}")
        End Function

        Public Sub OpenStackTestUrl(facilityId As ApbFacilityId, referenceNumber As Integer, Optional sender As Form = Nothing)
            Dim url As Uri = GetStackTestUrl(facilityId, referenceNumber)
            DAL.LogReportUsage("stack test", url.ToString)
            OpenUrl(url, sender)
        End Sub

    End Module

End Namespace