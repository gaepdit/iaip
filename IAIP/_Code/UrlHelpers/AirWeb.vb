Imports System.Configuration
Imports Iaip.Apb

Namespace UrlHelpers

    Public Module AirWeb

        Private ReadOnly ReportsUrl As New Uri(ConfigurationManager.AppSettings("AirWebUrl"))

        ' Facility Pages
        Public Sub OpenFacilityDetailsOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Facility/Details/{facilityId.FormattedString}")
            DAL.LogReportUsage("AirWeb Facility", url)
            OpenUrl(url, sender)
        End Sub

        Public Sub OpenComplianceWorkOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/Work/Search?FacilityId={facilityId.FormattedString}#search-results")
            DAL.LogReportUsage("AirWeb Facility", url)
            OpenUrl(url, sender)
        End Sub

        Public Sub OpenFcesOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/FCE/Search?FacilityId={facilityId.FormattedString}#search-results")
            DAL.LogReportUsage("AirWeb Facility", url)
            OpenUrl(url, sender)
        End Sub

        Public Sub OpenEnforcementOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Enforcement/Search?FacilityId={facilityId.FormattedString}#search-results")
            DAL.LogReportUsage("AirWeb Facility", url)
            OpenUrl(url, sender)
        End Sub

        ' Printouts

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