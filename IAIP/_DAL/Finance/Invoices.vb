Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports EpdIt.DBUtilities
Imports Iaip.Apb
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.Apb.Finance

Namespace DAL.Finance
    Public Module Invoices

        ' Read

        Public Function InvoiceExists(invoiceId As Integer) As Boolean
            Return DB.SPGetBoolean("fees.InvoiceExists", New SqlParameter("@InvoiceID", invoiceId))
        End Function

        Public Function InvoiceHasPaymentsApplied(invoiceId As Integer) As Boolean
            Return DB.SPGetBoolean("fees.InvoiceHasPaymentsApplied", New SqlParameter("@InvoiceID", invoiceId))
        End Function

        Public Function GetInvoice(invoiceId As Integer) As Invoice
            Dim ds As DataSet = DB.SPGetDataSet("fees.GetInvoice", New SqlParameter("@InvoiceID", invoiceId))

            If ds Is Nothing OrElse ds.Tables.Count <> 3 OrElse ds.Tables(0).Rows.Count <> 1 Then
                Return Nothing
            End If

            Dim invoice As Invoice = InvoiceFromDataRow(ds.Tables(0).Rows(0))

            For Each dr As DataRow In ds.Tables(1).Rows
                invoice.InvoiceItems.Add(InvoiceItemFromDataRow(dr))
            Next

            For Each dr As DataRow In ds.Tables(2).Rows
                invoice.DepositsApplied.Add(DepositAppliedFromDataRow(dr))
            Next

            Return invoice
        End Function

        Private Function InvoiceFromDataRow(dr As DataRow) As Invoice
            Return New Invoice With {
                .InvoiceID = CInt(dr("InvoiceID")),
                .InvoiceGuid = CType(dr("InvoiceGuid"), Guid),
                .CeasedCollections = CBool(dr("CeasedCollections")),
                .Comment = GetNullableString(dr("Comment")),
                .FacilityID = TryCastApbFacilityId(dr("FacilityID")),
                .FacilityName = GetNullableString(dr("FacilityName")),
                .InvoiceCategoryID = CChar(dr("InvoiceCategoryID")),
                .InvoiceDate = CDate(dr("InvoiceDate")),
                .DueDate = CDate(dr("DueDate")),
                .InvoiceType = New InvoiceType With {
                    .Active = CBool(dr("InvoiceTypeActive")),
                    .Description = GetNullableString(dr("InvoiceTypeDescription")),
                    .InvoiceTypeID = CInt(dr("InvoiceTypeID"))
                },
                .IsCreditMemo = CBool(dr("IsCreditMemo")),
                .SettlementStatus = GetNullableString(dr("SettlementStatus")),
                .TotalAmountDue = CDec(dr("TotalAmount")),
                .PaymentsApplied = -CDec(dr("PaymentsApplied")),
                .Voided = CBool(dr("Voided")),
                .VoidedDate = GetNullableDateTime(dr.Item("VoidedDate")),
                .ApplicationID = GetNullable(Of Integer)(dr("ApplicationID")),
                .FeeYear = GetNullable(Of Integer)(dr("FeeYear")),
                .InvoiceItems = New List(Of InvoiceItem),
                .DepositsApplied = New List(Of DepositApplied)
            }
        End Function

        Private Function InvoiceItemFromDataRow(dr As DataRow) As InvoiceItem
            Return New InvoiceItem With {
                .InvoiceID = GetNullable(Of Integer?)(dr("InvoiceID")),
                .InvoiceItemID = CInt(dr("InvoiceItemID")),
                .Amount = CDec(dr("Amount")),
                .ItemStatus = CType(dr("ItemStatusID"), InvoiceItemStatus),
                .RateCategory = CType(dr("RateCategoryID"), FeeRateCategory),
                .ApplicationID = GetNullable(Of Integer?)(dr("ApplicationID")),
                .FacilityID = TryCastApbFacilityId(dr("FacilityID")),
                .FeeYear = GetNullable(Of Integer?)(dr("FeeYear")),
                .InvoiceCategoryID = CChar(dr("InvoiceCategoryID"))
            }
        End Function

        Private Function DepositAppliedFromDataRow(dr As DataRow) As DepositApplied
            Return New DepositApplied With {
                .DepositAppliedID = CInt(dr("DepositAppliedID")),
                .DepositID = CInt(dr("DepositID")),
                .InvoiceID = CInt(dr("InvoiceID")),
                .FacilityID = TryCastApbFacilityId(dr("FacilityID")),
                .AmountApplied = CDec(dr("AmountApplied")),
                .DepositDate = CDate(dr("DepositDate")),
                .InvoiceVoided = CBool(dr("InvoiceVoided")),
                .DepositDeleted = CBool(dr("DepositDeleted"))
            }
        End Function

        Public Function SearchInvoices(facilityID As ApbFacilityId, onlyOpen As Boolean) As DataTable
            Return SearchInvoices(facilityID, Nothing, Nothing, Nothing, Nothing, onlyOpen, False)
        End Function

        Public Function SearchInvoices(facilityID As ApbFacilityId,
                                       facilityName As String,
                                       startDate As Date?,
                                       endDate As Date?,
                                       category As String,
                                       onlyOpen As Boolean,
                                       includeVoid As Boolean) As DataTable

            Dim params As SqlParameter() = {
                New SqlParameter("@FacilityID", facilityID?.DbFormattedString),
                New SqlParameter("@FacilityName", If(String.IsNullOrEmpty(facilityName), Nothing, "%" & facilityName & "%")),
                New SqlParameter("@StartDate", If(startDate, Nothing)),
                New SqlParameter("@EndDate", If(endDate, Nothing)),
                New SqlParameter("@Category", category),
                New SqlParameter("@OnlyOpen", onlyOpen),
                New SqlParameter("@IncludeVoid", includeVoid)
            }

            Return DB.SPGetDataTable("fees.SearchInvoices", params)
        End Function

        ' Write

        Public Function GenerateInvoice(appNumber As Integer, userId As Integer, ByRef invoiceId As Integer) As GenerateInvoiceResult
            Dim spName As String = "fees.GenerateInvoiceForPermitApplication"

            Dim params As SqlParameter() = {
                New SqlParameter("@AppNumber", appNumber),
                New SqlParameter("@UserID", userId),
                New SqlParameter("@FromIaip", True)
            }

            Dim returnValue As Integer

            invoiceId = DB.SPGetInteger(spName, params, returnValue)

            Return returnValue
        End Function

        Public Enum GenerateInvoiceResult
            DbError = -1
            Success = 0
            NoApplication = 1
            InvoiceExists = 2
            NoLineItems = 3
            InvalidInvoiceTotal = 4
            InvalidFacilityId = 5
        End Enum

        Public Function SaveInvoiceComment(invoiceId As Integer, comment As String) As Boolean
            Dim spName As String = "fees.SaveInvoiceComment"

            Dim params As SqlParameter() = {
                New SqlParameter("@InvoiceID", invoiceId),
                New SqlParameter("@Comment", comment),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            If DB.SPReturnValue(spName, params) = 0 Then
                Return True
            End If

            Return False
        End Function

        Public Function VoidInvoice(invoiceId As Integer) As VoidInvoiceResult
            Dim params As SqlParameter() = {
                New SqlParameter("@InvoiceID", invoiceId),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Return CType(DB.SPReturnValue("fees.VoidInvoice", params), VoidInvoiceResult)
        End Function

        Public Enum VoidInvoiceResult
            DbError = -1
            Success = 0
            DoesNotExist = 1
            AlreadyVoided = 2
            HasPayments = 3
        End Enum

        ' ID Validation

        Public Function ValidateInvoiceId(testID As Integer, Optional mustExist As Boolean = False) As InvoiceValidationResult
            If mustExist AndAlso Not InvoiceExists(testID) Then
                Return InvoiceValidationResult.Nonexistent
            End If

            Return InvoiceValidationResult.Valid
        End Function

        Public Function ValidateInvoiceId(testID As String, ByRef newID As Integer, Optional mustExist As Boolean = False) As InvoiceValidationResult
            If Not Regex.IsMatch(testID, NumericPattern) Then
                Return InvoiceValidationResult.Malformed
            End If

            If Not Integer.TryParse(testID, newID) Then
                Return InvoiceValidationResult.Malformed
            End If

            Return ValidateInvoiceId(newID, mustExist)
        End Function

        Public Enum InvoiceValidationResult
            Valid
            Malformed
            Nonexistent
        End Enum

    End Module
End Namespace
