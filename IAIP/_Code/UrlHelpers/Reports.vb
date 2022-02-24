Imports System.Configuration
Imports Iaip.Apb

Namespace UrlHelpers

    Public Module Reports

        Private ReadOnly ReportsUrl As New Uri(ConfigurationManager.AppSettings("AirReportsUrl"))

        Private Function GetAccUrl(facilityId As ApbFacilityId, id As Integer) As Uri
            Return New Uri(ReportsUrl, $"facility/{facilityId.FormattedString}/acc-report/{id}")
        End Function

        Public Sub OpenAccUrl(facilityId As ApbFacilityId, id As Integer, Optional sender As Form = Nothing)
            Dim url As Uri = GetAccUrl(facilityId, id)
            DAL.LogReportUsage("acc", url.ToString)
            OpenUrl(url, sender)
        End Sub

        Private Function GetFceUrl(facilityId As ApbFacilityId, id As Integer) As Uri
            Return New Uri(ReportsUrl, $"facility/{facilityId.FormattedString}/fce/{id}")
        End Function

        Public Sub OpenFceUrl(facilityId As ApbFacilityId, id As Integer, Optional sender As Form = Nothing)
            Dim url As Uri = GetFceUrl(facilityId, id)
            DAL.LogReportUsage("fce", url.ToString)
            OpenUrl(url, sender)
        End Sub

        Private Function GetStackTestUrl(facilityId As ApbFacilityId,
                                         referenceNumber As Integer,
                                         includeConfidentialInfo As Boolean) As Uri

            Dim query As String = If(includeConfidentialInfo, "?includeConfidentialInfo=true", "")
            Return New Uri(ReportsUrl, $"facility/{facilityId.FormattedString}/stack-test/{referenceNumber}{query}")
        End Function

        Public Sub OpenStackTestUrl(facilityId As ApbFacilityId,
                                    referenceNumber As Integer,
                                    includeConfidentialInfo As Boolean,
                                    Optional sender As Form = Nothing)
            Dim url As Uri = GetStackTestUrl(facilityId, referenceNumber, includeConfidentialInfo)
            DAL.LogReportUsage("stack test", url.ToString)
            OpenUrl(url, sender)
        End Sub

    End Module

End Namespace