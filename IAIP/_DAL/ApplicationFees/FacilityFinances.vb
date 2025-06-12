Imports Microsoft.Data.SqlClient
Imports Iaip.Apb

Namespace DAL.ApplicationFees
    Public Module FacilityFinances

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
            ArgumentNotNull(facilityId, NameOf(facilityId))

            Dim ds As DataSet = DB.SPGetDataSet("fees.GetFacilityFinances", New SqlParameter("FacilityID", facilityId.DbFormattedString))

            If ds Is Nothing OrElse ds.Tables.Count <> 5 Then
                Return Nothing
            End If

            ds.Tables(0).TableName = "Invoices"
            ds.Tables(1).TableName = "Credits"
            ds.Tables(2).TableName = "Pending"
            ds.Tables(3).TableName = "Refunds"
            ds.Tables(4).TableName = "Deposits"

            Return ds
        End Function

    End Module
End Namespace
