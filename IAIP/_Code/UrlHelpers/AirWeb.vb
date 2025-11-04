Imports System.Configuration
Imports Iaip.Apb

Namespace UrlHelpers

    Public Module AirWeb

        Private ReadOnly ReportsUrl As New Uri(ConfigurationManager.AppSettings("AirWebUrl"))

        ' Facility
        Public Sub OpenFacilityOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Facility/Details/{facilityId.FormattedString}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Facility", url)
        End Sub

        ' Compliance Work
        Public Sub OpenComplianceWorkOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, "Compliance/Work")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Compliance Search", url)
        End Sub
        Public Sub OpenComplianceWorkOnWeb(id As String, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/Work/Details/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Compliance Item", url)
        End Sub
        Public Sub OpenComplianceWorkOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/Work/Search?FacilityId={facilityId.FormattedString}#search-results")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Facility Compliance", url)
        End Sub

        ' FCEs
        Public Sub OpenFceOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/FCE")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web FCE Search", url)
        End Sub
        Public Sub OpenFceOnWeb(id As String, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/FCE/Details/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web FCE Item", url)
        End Sub
        Public Sub OpenFceOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/FCE/Search?FacilityId={facilityId.FormattedString}#search-results")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Facility FCEs", url)
        End Sub

        ' Enforcement
        Public Sub OpenEnforcementOnWeb(Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Enforcement")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Enforcmement Search", url)
        End Sub
        Public Sub OpenEnforcementOnWeb(id As String, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Enforcement/Details/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Enforcement Item", url)
        End Sub
        Public Sub OpenEnforcementOnWeb(facilityId As ApbFacilityId, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Enforcement/Search?FacilityId={facilityId.FormattedString}#search-results")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Facility Enforcement", url)
        End Sub

        ' Source Tests
        Public Sub OpenSourceTestSummaryOnWeb(referenceNumber As String, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"Compliance/SourceTest/Details/{referenceNumber}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Source Test Summary", url)
        End Sub

        ' Printouts
        Public Sub OpenAccPrintout(id As Integer, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"print/acc/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web ACC Printout", url)
        End Sub

        Public Sub OpenFcePrintout(id As Integer, Optional sender As Form = Nothing)
            Dim url As New Uri(ReportsUrl, $"print/fce/{id}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web FCE Printout", url)
        End Sub

        Public Sub OpenSourceTestPrintout(referenceNumber As Integer, includeConfidentialInfo As Boolean, Optional sender As Form = Nothing)
            Dim queryString As String = If(includeConfidentialInfo, "?includeConfidentialInfo=true", "")
            Dim url As New Uri(ReportsUrl, $"print/source-test/{referenceNumber}{queryString}")
            OpenUrl(url, sender)
            DAL.LogReportUsage("Air Web Source Test Printout", url)
        End Sub

    End Module

End Namespace