Imports System.Data.SqlClient
Imports EpdIt.DBUtilities
Imports Iaip.Apb.Finance

Namespace DAL.Finance
    Public Module Refunds

        ' Read

        Public Function RefundExists(refundID As Integer) As Boolean
            Return DB.SPGetBoolean("fees.RefundExists", New SqlParameter("@RefundID", refundID))
        End Function

        Public Function GetRefund(refundID As Integer) As Refund
            Dim dr As DataRow = DB.SPGetDataRow("fees.GetRefund", New SqlParameter("@RefundID", refundID))

            If dr Is Nothing Then Return Nothing

            Return RefundFromDataRow(dr)
        End Function

        Public Function RefundFromDataRow(dr As DataRow) As Refund
            Return New Refund With {
                .RefundID = CInt(dr("RefundID")),
                .Amount = CDec(dr("TotalAmount")),
                .Comment = GetNullableString(dr("Comment")),
                .DepositID = CInt(dr("DepositID")),
                .RefundDate = CDate(dr("RefundDate")),
                .Deleted = CBool(dr("Deleted"))
            }
        End Function

        ' Write

        Public Function DeleteRefund(refundId As Integer) As DeleteRefundResult
            Dim result As Integer = DB.SPReturnValue("fees.DeleteRefund", New SqlParameter("@RefundID", refundId))

            Select Case result
                Case 0
                    Return DeleteRefundResult.Success
                Case 1
                    Return DeleteRefundResult.DoesNotExist
                Case 2
                    Return DeleteRefundResult.AlreadyDeleted
                Case Else
                    Return DeleteRefundResult.DbError
            End Select
        End Function

        Public Enum DeleteRefundResult
            Success
            DbError
            DoesNotExist
            AlreadyDeleted
        End Enum

    End Module
End Namespace
