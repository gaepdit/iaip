Imports System.Data.SqlClient
Imports Iaip.IAIPFacilitySummary

Namespace DAL
    Module FacilitySummaryData

        Public Function GetFSDataTable(whichTable As FacilityDataTable, airsNumber As Apb.ApbFacilityId) As DataTable
            Dim spName As String = GetCorrectStoredProcedureName(whichTable)
            Dim parameter As SqlParameter = New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        Private Function GetCorrectStoredProcedureName(whichTable As FacilityDataTable) As String
            Select Case whichTable

                Case FacilityDataTable.ComplianceEnforcement
                    Return "iaip_facility.GetSummaryEnforcement"

                Case FacilityDataTable.ComplianceFCE
                    Return "iaip_facility.GetSummaryFce"

                Case FacilityDataTable.ComplianceWork
                    Return "iaip_facility.GetSummaryCompliance"

                Case FacilityDataTable.ContactsCompliance
                    Return "iaip_facility.GetContactsCompliance"

                Case FacilityDataTable.ContactsGeco
                    Return "iaip_facility.GetContactsGeco"

                Case FacilityDataTable.ContactsPermitting
                    Return "iaip_facility.GetContactsPermitting"

                Case FacilityDataTable.ContactsState
                    Return "iaip_facility.GetContactsState"

                Case FacilityDataTable.ContactsTesting
                    Return "iaip_facility.GetContactsTesting"

                Case FacilityDataTable.ContactsWebSite
                    Return "iaip_facility.GetContactsWebSite"

                Case FacilityDataTable.EiPost2009
                    Return "iaip_facility.GetSummaryEiPost2009"

                Case FacilityDataTable.EiPre2009
                    Return "iaip_facility.GetSummaryEiPre2009"

                Case FacilityDataTable.Fees
                    Return "iaip_facility.GetSummaryFees"

                Case FacilityDataTable.FinancialDeposits
                    Return "iaip_facility.FinancialDeposits"

                Case FacilityDataTable.FinancialFees
                    Return "iaip_facility.FinancialFees"

                Case FacilityDataTable.FinancialInvoices
                    Return "iaip_facility.FinancialInvoices"

                Case FacilityDataTable.PermitApplications
                    Return "iaip_facility.PermitApplications"

                Case FacilityDataTable.PermitRuleHistory
                    Return "iaip_facility.PermitRuleHistory"

                Case FacilityDataTable.PermitRules
                    Return "iaip_facility.PermitRules"

                Case FacilityDataTable.Permits
                    Return "iaip_facility.Permits"

                Case FacilityDataTable.TestMemos
                    Return "iaip_facility.TestMemos"

                Case FacilityDataTable.TestNotifications
                    Return "iaip_facility.TestNotifications"

                Case FacilityDataTable.TestReports
                    Return "iaip_facility.TestReports"

                Case Else
                    Return ""
            End Select
        End Function

    End Module
End Namespace
