Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports System.Text.RegularExpressions
Imports GaEpd.DBUtilities
Imports Iaip.Apb
Imports Iaip.Apb.ApplicationFees
Imports Iaip.Apb.ApbFacilityId

Namespace DAL.ApplicationFees
    Public Module Deposits

        ' Read

        Public Function DepositExists(depositID As Integer) As Boolean
            Return DB.SPGetBoolean("fees.DepositExists", New SqlParameter("@DepositID", depositID))
        End Function

        Public Function GetDeposit(depositID As Integer) As Deposit
            Dim ds As DataSet = DB.SPGetDataSet("fees.GetDeposit", New SqlParameter("@DepositID", depositID))

            If ds Is Nothing OrElse ds.Tables.Count <> 3 OrElse ds.Tables(0).Rows.Count <> 1 Then Return Nothing

            Dim deposit As Deposit = DepositFromDataRow(ds.Tables(0).Rows(0))

            For Each dr As DataRow In ds.Tables(1).Rows
                deposit.DepositsApplied.Add(DepositAppliedFromDataRow(dr))
            Next

            For Each dr As DataRow In ds.Tables(2).Rows
                deposit.RefundsApplied.Add(RefundAppliedFromDataRow(dr))
            Next

            Return deposit
        End Function

        Private Function DepositFromDataRow(dr As DataRow) As Deposit
            Return New Deposit With {
                .DepositID = CInt(dr("DepositID")),
                .DepositDate = CDate(dr("DepositDate")),
                .DepositAmount = -CDec(dr("TotalAmount")),
                .TotalPaymentsApplied = CDec(dr("TotalPaymentsApplied")),
                .TotalRefunded = CDec(dr("TotalRefunded")),
                .DepositNumber = GetNullableString(dr("DepositNumber")),
                .BatchNumber = GetNullableString(dr("BatchNumber")),
                .CheckNumber = GetNullableString(dr("CheckNumber")),
                .CreditCardConf = GetNullableString(dr("CreditCardConf")),
                .Comment = GetNullableString(dr("Comment")),
                .FacilityID = TryCastApbFacilityId(dr("FacilityID")),
                .Deleted = CBool(dr("Deleted")),
                .DepositsApplied = New List(Of DepositApplied),
                .RefundsApplied = New List(Of RefundApplied)
            }
        End Function

        Private Function DepositAppliedFromDataRow(dr As DataRow) As DepositApplied
            Return New DepositApplied With {
                .DepositAppliedID = CInt(dr("DepositAppliedID")),
                .DepositID = CInt(dr("DepositID")),
                .InvoiceID = CInt(dr("InvoiceID")),
                .FacilityID = TryCastApbFacilityId(dr("FacilityID")),
                .AmountApplied = CDec(dr("AmountApplied")),
                .InvoiceVoided = CBool(dr("InvoiceVoided")),
                .DepositDate = CDate(dr("DepositDate")),
                .DepositDeleted = CBool(dr("DepositDeleted"))
            }
        End Function

        Public Function SearchDeposits(depositNo As String,
                                       batchNo As String,
                                       checkNo As String,
                                       creditConf As String,
                                       facilityID As ApbFacilityId,
                                       startDate As Date?,
                                       endDate As Date?,
                                       unusedBalance As Boolean,
                                       includeDeleted As Boolean) As DataTable

            Dim params As SqlParameter() = {
                New SqlParameter("@DepositNo", If(String.IsNullOrEmpty(depositNo), Nothing, "%" & depositNo & "%")),
                New SqlParameter("@BatchNo", If(String.IsNullOrEmpty(batchNo), Nothing, "%" & batchNo & "%")),
                New SqlParameter("@CheckNo", If(String.IsNullOrEmpty(checkNo), Nothing, "%" & checkNo & "%")),
                New SqlParameter("@CreditConf", If(String.IsNullOrEmpty(creditConf), Nothing, "%" & creditConf & "%")),
                New SqlParameter("@FacilityID", facilityID?.DbFormattedString),
                New SqlParameter("@StartDate", If(startDate, Nothing)),
                New SqlParameter("@EndDate", If(endDate, Nothing)),
                New SqlParameter("@UnusedBalance", unusedBalance),
                New SqlParameter("@IncludeDeleted", includeDeleted)
            }

            Return DB.SPGetDataTable("fees.SearchDeposits", params)
        End Function

        ' Delete deposit

        Public Function DeleteDeposit(depositID As Integer) As DeleteDepositResult
            Dim params As SqlParameter() = {
                New SqlParameter("@DepositID", depositID),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Return CType(DB.SPReturnValue("fees.DeleteDeposit", params), DeleteDepositResult)
        End Function

        Public Enum DeleteDepositResult
            DbError = -1
            Success = 0
            DoesNotExist = 1
            AlreadyDeleted = 2
            PaymentsApplied = 3
        End Enum

        ' Save deposit

        Public Function SaveNewDeposit(deposit As Deposit, ByRef newDepositId As Integer) As DbResult
            ArgumentNotNull(deposit, NameOf(deposit))

            Dim params As SqlParameter() = {
                New SqlParameter("@DepositDate", deposit.DepositDate),
                New SqlParameter("@TotalAmount", -deposit.DepositAmount),
                New SqlParameter("@DepositNumber", deposit.DepositNumber),
                New SqlParameter("@BatchNumber", deposit.BatchNumber),
                New SqlParameter("@CheckNumber", deposit.CheckNumber),
                New SqlParameter("@CreditCardConf", deposit.CreditCardConf),
                New SqlParameter("@Comment", deposit.Comment),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Dim returnValue As Integer

            newDepositId = DB.SPGetInteger("fees.SaveNewDeposit", params, returnValue)

            Return CType(returnValue, DbResult)
        End Function

        Public Function UpdateDeposit(deposit As Deposit) As UpdateDepositResult
            ArgumentNotNull(deposit, NameOf(deposit))

            Dim params As SqlParameter() = {
                New SqlParameter("@DepositID", deposit.DepositID),
                New SqlParameter("@DepositDate", deposit.DepositDate),
                New SqlParameter("@TotalAmount", -deposit.DepositAmount),
                New SqlParameter("@DepositNumber", deposit.DepositNumber),
                New SqlParameter("@BatchNumber", deposit.BatchNumber),
                New SqlParameter("@CheckNumber", deposit.CheckNumber),
                New SqlParameter("@CreditCardConf", deposit.CreditCardConf),
                New SqlParameter("@Comment", deposit.Comment),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Return CType(DB.SPReturnValue("fees.UpdateDeposit", params), UpdateDepositResult)
        End Function

        Public Enum UpdateDepositResult
            DbError = -1
            Success = 0
            DoesNotExist = 1
            DepositDeleted = 2
            AmountBelowMinimum = 3
        End Enum

        ' Payment applied

        Public Function ApplyPaymentToInvoice(invoiceId As Integer, amount As Decimal, depositId As Integer, Optional ByRef newDepositAppliedId As Integer = -1) As ApplyPaymentToInvoiceResult
            Dim params As SqlParameter() = {
                New SqlParameter("@InvoiceId", invoiceId),
                New SqlParameter("@DepositId", depositId),
                New SqlParameter("@Amount", amount),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Dim returnValue As Integer

            newDepositAppliedId = DB.SPGetInteger("fees.ApplyPaymentToInvoice", params, returnValue)

            Return CType(returnValue, ApplyPaymentToInvoiceResult)
        End Function

        Public Enum ApplyPaymentToInvoiceResult
            DbError = -1
            Success = 0
            DepositDoesNotExist = 1
            DepositDeleted = 2
            InvoiceDoesNotExist = 3
            InvoiceVoided = 4
            DepositBalanceInsufficient = 5
            InvoiceBalanceExceeded = 6
        End Enum

        ' ID Validation

        Public Function ValidateDepositId(testID As Integer, Optional mustExist As Boolean = False) As DepositValidationResult
            If mustExist AndAlso Not DepositExists(testID) Then
                Return DepositValidationResult.Nonexistent
            End If

            Return DepositValidationResult.Valid
        End Function

        Public Function ValidateDepositId(testID As String, ByRef newID As Integer, Optional mustExist As Boolean = False) As DepositValidationResult
            If Not Regex.IsMatch(testID, NumericPattern) Then
                Return DepositValidationResult.Malformed
            End If

            If Not Integer.TryParse(testID, newID) Then
                Return DepositValidationResult.Malformed
            End If

            Return ValidateDepositId(newID, mustExist)
        End Function

        Public Enum DepositValidationResult
            Valid
            Malformed
            Nonexistent
        End Enum


    End Module
End Namespace
