Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports GaEpd.DBUtilities
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.Apb.ApplicationFees

Namespace DAL.ApplicationFees
    Public Module Refunds

        ' Read

        Public Function RefundExists(refundID As Integer) As Boolean
            Return DB.SPGetBoolean("fees.RefundExists", New SqlParameter("@RefundID", refundID))
        End Function

        Public Function GetRefund(refundID As Integer) As Refund
            Dim ds As DataSet = DB.SPGetDataSet("fees.GetRefund", New SqlParameter("@RefundID", refundID))

            If ds Is Nothing OrElse ds.Tables.Count <> 2 OrElse ds.Tables(0).Rows.Count <> 1 Then
                Return Nothing
            End If

            Dim refund As Refund = RefundFromDataRow(ds.Tables(0).Rows(0))

            For Each dr As DataRow In ds.Tables(1).Rows
                refund.RefundsApplied.Add(RefundAppliedFromDataRow(dr))
            Next

            Return refund
        End Function

        Public Function RefundFromDataRow(dr As DataRow) As Refund
            NotNull(dr, NameOf(dr))

            Return New Refund With {
                .RefundId = CInt(dr("RefundID")),
                .RefundDate = CDate(dr("RefundDate")),
                .Amount = CDec(dr("Amount")),
                .FacilityID = TryCastApbFacilityId(dr("FacilityId")),
                .Comment = GetNullableString(dr("Comment")),
                .Deleted = CBool(dr("Deleted")),
                .RefundsApplied = New List(Of RefundApplied)
            }
        End Function

        Public Function RefundAppliedFromDataRow(dr As DataRow) As RefundApplied
            NotNull(dr, NameOf(dr))

            Return New RefundApplied With {
                .RefundAppliedId = CInt(dr("RefundAppliedId")),
                .RefundId = CInt(dr("RefundID")),
                .DepositId = CInt(dr("DepositId")),
                .AmountApplied = CDec(dr("AmountApplied")),
                .RefundDate = CDate(dr("RefundDate")),
                .RefundDeleted = CBool(dr("RefundDeleted")),
                .DepositDeleted = CBool(dr("DepositDeleted"))
            }
        End Function

        ' Write

        Public Function SaveNewRefund(refund As Refund, ByRef newRefundId As Integer) As SaveRefundResult
            NotNull(refund, NameOf(refund))

            Dim params As SqlParameter() = {
                New SqlParameter("@RefundDate", refund.RefundDate),
                New SqlParameter("@Amount", refund.Amount),
                New SqlParameter("@FacilityID", refund.FacilityID.DbFormattedString),
                New SqlParameter("@Comment", refund.Comment),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Dim returnValue As Integer

            newRefundId = DB.SPGetInteger("fees.SaveNewRefund", params, returnValue)

            Return CType(returnValue, SaveRefundResult)
        End Function

        Public Enum SaveRefundResult
            DbError = -1
            Success = 0
            NonPositiveAmount = 1
            InsufficientFunds = 2
        End Enum

        Public Function UpdateRefundComment(refundId As Integer, comment As String) As UpdateRefundCommentResult
            Dim params As SqlParameter() = {
                New SqlParameter("@RefundId", refundId),
                New SqlParameter("@Comment", comment),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Return CType(DB.SPReturnValue("fees.UpdateRefundComment", params), UpdateRefundCommentResult)
        End Function

        Public Enum UpdateRefundCommentResult
            DbError = -1
            Success = 0
            DoesNotExist = 1
            RefundDeleted = 2
        End Enum

        Public Function DeleteRefund(refundId As Integer) As DeleteRefundResult
            Dim params As SqlParameter() = {
                New SqlParameter("@RefundId", refundId),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Return CType(DB.SPReturnValue("fees.DeleteRefund", params), DeleteRefundResult)
        End Function

        Public Enum DeleteRefundResult
            DbError = -1
            Success = 0
            DoesNotExist = 1
            AlreadyDeleted = 2
        End Enum

    End Module
End Namespace
