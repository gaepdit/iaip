Imports System.Data.SqlClient
Imports EpdIt
Imports Iaip.Apb.Sspp

Namespace DAL.Sspp
    Module ApplicationFees

        Public Function GetApplicationFeesInfo(appNumber As Integer) As ApplicationFeeInfo
            Dim query As String = "select ApplicationFeeApplies,
                    ApplicationFeeType,
                    ApplicationFeeAmount,
                    ApplicationFeeOverride,
                    ApplicationFeeOverrideReason,
                    ExpeditedFeeApplies,
                    ExpeditedFeeType,
                    ExpeditedFeeAmount,
                    ExpeditedFeeOverride,
                    ExpeditedFeeOverrideReason,
                    FeeDataFinalized,
                    DateFacilityNotifiedOfFees,
                    DateFeeDataFinalized,
                    case
                        when m.STRPERMITTYPE = 11
                                then convert(bit, 1)
                        else convert(bit, 0)
                    end as ApplicationWithdrawn
            from SSPPAPPLICATIONDATA d
                    inner join SSPPAPPLICATIONMASTER m
                            on d.STRAPPLICATIONNUMBER = m.STRAPPLICATIONNUMBER
            where convert(int, d.STRAPPLICATIONNUMBER) = @AppNumber"

            Dim dr As DataRow = DB.GetDataRow(query, New SqlParameter("@AppNumber", appNumber))

            If dr Is Nothing Then
                Return Nothing
            End If

            Return New ApplicationFeeInfo With {
                .ApplicationID = appNumber,
                .ApplicationWithdrawn = CBool(dr.Item("ApplicationWithdrawn")),
                .ApplicationFeeApplies = CBool(dr.Item("ApplicationFeeApplies")),
                .ApplicationFeeType = DBUtilities.GetNullable(Of Integer?)(dr.Item("ApplicationFeeType")),
                .ApplicationFeeAmount = DBUtilities.GetNullable(Of Decimal)(dr.Item("ApplicationFeeAmount")),
                .ApplicationFeeOverride = CBool(dr.Item("ApplicationFeeOverride")),
                .ApplicationFeeOverrideReason = DBUtilities.GetNullableString(dr.Item("ApplicationFeeOverrideReason")),
                .ExpeditedFeeApplies = CBool(dr.Item("ExpeditedFeeApplies")),
                .ExpeditedFeeType = DBUtilities.GetNullable(Of Integer?)(dr.Item("ExpeditedFeeType")),
                .ExpeditedFeeAmount = DBUtilities.GetNullable(Of Decimal)(dr.Item("ExpeditedFeeAmount")),
                .ExpeditedFeeOverride = CBool(dr.Item("ExpeditedFeeOverride")),
                .ExpeditedFeeOverrideReason = DBUtilities.GetNullableString(dr.Item("ExpeditedFeeOverrideReason")),
                .FeeDataFinalized = CBool(dr.Item("FeeDataFinalized")),
                .DateFacilityNotifiedOfFees = DBUtilities.GetNullableDateTime(dr.Item("DateFacilityNotifiedOfFees")),
                .DateFeeDataFinalized = DBUtilities.GetNullableDateTime(dr.Item("DateFeeDataFinalized"))
            }
        End Function

        Public Function GetApplicationPayments(appNumber As Integer) As DataTable
            Dim query As String = "select r.DepositDate   as [Date],
                       i.InvoiceId     as [Invoice #],
                       X.AmountApplied as [Payment]
                from fees.Deposit r
                     inner join fees.Deposit_Invoice X
                                on r.DepositID = X.DepositID
                     inner join fees.VW_Invoices i
                                on X.InvoiceID = i.InvoiceID
                where ApplicationID = @appNumber
                order by r.DepositDate, i.InvoiceID"

            Return DB.GetDataTable(query, New SqlParameter("@appNumber", appNumber))
        End Function

        Public Function GetApplicationInvoices(appNumber As Integer) As DataTable
            Dim query As String = "select InvoiceID        as [Invoice #],
                       InvoiceDate      as [Invoice Date],
                       TotalAmount      as Amount,
                       PaymentsApplied  as [Total Payments],
                       SettlementStatus as Status
                from fees.VW_Invoices
                where ApplicationID = @appNumber
                order by InvoiceID"

            Return DB.GetDataTable(query, New SqlParameter("@appNumber", appNumber))
        End Function

        Public Function IsInvoiceGeneratedForApplication(appNumber As Integer) As Boolean
            Dim query As String = "select convert(bit, count(*))
                from fees.VW_Invoices
                where ApplicationID = @appNumber and Voided = 0"

            Return DB.GetBoolean(query, New SqlParameter("@appNumber", appNumber))
        End Function

        Public Function SaveApplicationFeesData(appFeesInfo As ApplicationFeeInfo) As Integer
            Dim spName As String = "fees.SaveApplicationFees"

            Dim params As SqlParameter() = {
                New SqlParameter("@ApplicationNumber", appFeesInfo.ApplicationID),
                New SqlParameter("@FacilityID", appFeesInfo.FacilityID.DbFormattedString),
                New SqlParameter("@ApplicationWithdrawn", appFeesInfo.ApplicationWithdrawn),
                New SqlParameter("@ApplicationFeeApplies", appFeesInfo.ApplicationFeeApplies),
                New SqlParameter("@ApplicationFeeType", appFeesInfo.ApplicationFeeType),
                New SqlParameter("@ApplicationFeeAmount", appFeesInfo.ApplicationFeeAmount),
                New SqlParameter("@ApplicationFeeOverride", appFeesInfo.ApplicationFeeOverride),
                New SqlParameter("@ApplicationFeeOverrideReason", appFeesInfo.ApplicationFeeOverrideReason),
                New SqlParameter("@ExpeditedFeeApplies", appFeesInfo.ExpeditedFeeApplies),
                New SqlParameter("@ExpeditedFeeType", appFeesInfo.ExpeditedFeeType),
                New SqlParameter("@ExpeditedFeeAmount", appFeesInfo.ExpeditedFeeAmount),
                New SqlParameter("@ExpeditedFeeOverride", appFeesInfo.ExpeditedFeeOverride),
                New SqlParameter("@ExpeditedFeeOverrideReason", appFeesInfo.ExpeditedFeeOverrideReason),
                New SqlParameter("@FeeDataFinalized", appFeesInfo.FeeDataFinalized),
                New SqlParameter("@DateFeeDataFinalized", appFeesInfo.DateFeeDataFinalized),
                New SqlParameter("@DateFacilityNotifiedOfFees", appFeesInfo.DateFacilityNotifiedOfFees),
                New SqlParameter("@CurrentUserID", CurrentUser.UserID)
            }

            Return DB.SPReturnValue(spName, params)
        End Function

    End Module
End Namespace
