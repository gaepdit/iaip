﻿Imports Microsoft.Data.SqlClient
Imports Iaip.IAIPFacilitySummary

Namespace DAL
    Module FacilitySummaryData

        Public Function GetFSDataTable(whichTable As FacilityDataTable, airsNumber As Apb.ApbFacilityId) As DataTable
            Dim spName As String = GetCorrectStoredProcedureName(whichTable)

            If String.IsNullOrEmpty(spName) Then
                Return Nothing
            End If

            Return DB.SPGetDataTable(spName, New SqlParameter("@AirsNumber", airsNumber.DbFormattedString))
        End Function

        Private Function GetCorrectStoredProcedureName(whichTable As FacilityDataTable) As String
            Select Case whichTable

                Case FacilityDataTable.ColocatedFacilities
                    Return "iaip_facility.GetColocatedFacilities"

                Case FacilityDataTable.ComplianceEnforcement
                    Return "iaip_facility.GetSummaryEnforcement"

                Case FacilityDataTable.ComplianceFCE
                    Return "iaip_facility.GetSummaryFce"

                Case FacilityDataTable.ComplianceWork
                    Return "iaip_facility.GetSummaryCompliance"

                Case FacilityDataTable.ContactsGecoFacility
                    Return "iaip_facility.GetContactsGecoFacility"

                Case FacilityDataTable.ContactsGecoEmails
                    Return "iaip_facility.GetContactsGecoEmails"

                Case FacilityDataTable.ContactsGecoUsers
                    Return "iaip_facility.GetContactsGecoUsers"

                Case FacilityDataTable.ContactsCaersUsers
                    Return "iaip_facility.GetContactsCaersUsers"

                Case FacilityDataTable.ContactsIaipFacility
                    Return "iaip_facility.GetContactsIaipFacility"

                Case FacilityDataTable.ContactsStaff
                    Return "iaip_facility.GetContactsStaff"

                Case FacilityDataTable.EIPost2009
                    Return "iaip_facility.GetSummaryEiPost2009"

                Case FacilityDataTable.EIPre2009
                    Return "iaip_facility.GetSummaryEiPre2009"

                Case FacilityDataTable.EmissionsFeesSummary
                    Return "iaip_facility.GetSummaryFees"

                Case FacilityDataTable.EmissionsFeesDeposits
                    Return "iaip_facility.FinancialDeposits"

                Case FacilityDataTable.EmissionsFeesInvoices
                    Return "iaip_facility.FinancialInvoices"

                Case FacilityDataTable.PermitApplications
                    Return "iaip_facility.PermitApplications"

                Case FacilityDataTable.PermitRuleHistory
                    Return "iaip_facility.PermitRuleHistory"

                Case FacilityDataTable.PermitRules
                    Return "iaip_facility.PermitRules"

                Case FacilityDataTable.Permits
                    Return "iaip_facility.Permits"

                Case FacilityDataTable.PermitApplicationFees
                    Return "iaip_facility.PermitAppInvoices"

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
