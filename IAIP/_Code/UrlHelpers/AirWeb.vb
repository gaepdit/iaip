Imports System.Configuration
Imports Iaip.Apb

Namespace UrlHelpers

    Public Module AirWeb

        Private ReadOnly ReportsUrl As New Uri(ConfigurationManager.AppSettings("AirWebUrl"))

        ' Facility Pages
        Public Sub OpenFacilityDetailsOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, "Facility")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Facility Search", url)
        End Sub
        Public Sub OpenFacilityDetailsOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Facility/Details/{facilityId.FormattedString}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Facility", url)
        End Sub

        Public Sub OpenComplianceWorkOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, "Compliance/Work")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Compliance Search", url)
        End Sub
        Public Sub OpenComplianceWorkOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/Work/Search?FacilityId={facilityId.FormattedString}#search-results")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Facility Compliance", url)
        End Sub

        Public Sub OpenFcesOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/FCE")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb FCE Search", url)
        End Sub
        Public Sub OpenFcesOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/FCE/Search?FacilityId={facilityId.FormattedString}#search-results")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Facility FCEs", url)
        End Sub

        Public Sub OpenEnforcementOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Enforcement")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Enforcmement Search", url)
        End Sub
        Public Sub OpenEnforcementOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Enforcement/Search?FacilityId={facilityId.FormattedString}#search-results")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Facility Enforcement", url)
        End Sub

        Public Sub OpenSourceTestSummaryOnWeb(referenceNumber As String, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/SourceTest/Details/{referenceNumber}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("AirWeb Source Test Summary", url)
        End Sub

        ' Printouts

        Public Sub OpenAccPrintout(id As Integer, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"print/acc/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("acc", url)
        End Sub

        Public Sub OpenFcePrintout(id As Integer, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"print/fce/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("fce", url)
        End Sub

        Public Sub OpenSourceTestPrintout(referenceNumber As Integer, includeConfidentialInfo As Boolean, Optional sender As Form = Nothing)
            Dim queryString As String = If(includeConfidentialInfo, "?includeConfidentialInfo=true", "")
            Dim url As New Uri(ReportsUrl, $"print/source-test/{referenceNumber}{queryString}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("stack test", url)
        End Sub

    End Module

End Namespace