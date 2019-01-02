Imports System.Data.SqlClient
Imports Iaip.Apb

Namespace DAL.Finance
    Public Module FacilityFinances

        ' Read

        Public Function SearchFacilityAccounts(facilityID As ApbFacilityId,
                                               facilityName As String,
                                               openInvoicesOnly As Boolean,
                                               unusedCreditsOnly As Boolean,
                                               pendingOnly As Boolean) As DataTable

            Dim params As SqlParameter() = {
                New SqlParameter("@FacilityID", facilityID?.DbFormattedString),
                New SqlParameter("@FacilityName", If(String.IsNullOrEmpty(facilityName), Nothing, "%" & facilityName & "%")),
                New SqlParameter("@OpenInvoicesOnly", openInvoicesOnly),
                New SqlParameter("@UnusedCreditsOnly", unusedCreditsOnly),
                New SqlParameter("@PendingItemsOnly", pendingOnly)
            }

            Return DB.SPGetDataTable("fees.SearchFacilityAccounts", params)
        End Function

        Public Function GetFacilityFinances(facilityId As ApbFacilityId) As DataSet
            Return DB.SPGetDataSet("fees.GetFacilityFinances", New SqlParameter("FacilityID", facilityId.DbFormattedString))
        End Function

    End Module
End Namespace
