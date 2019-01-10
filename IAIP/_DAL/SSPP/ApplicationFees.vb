Imports System.Data.SqlClient
Imports EpdIt
Imports Iaip.Apb.Sspp

Namespace DAL.Sspp
    Module ApplicationFees

        Public Function GetApplicationFeesInfo(appNumber As Integer) As ApplicationFeeInfo
            Dim ds As DataSet = DB.SPGetDataSet("fees.GetApplicationFeeInfo", New SqlParameter("@AppNumber", appNumber))

            If ds Is Nothing OrElse ds.Tables.Count <> 3 OrElse ds.Tables(0).Rows.Count <> 1 Then
                Return Nothing
            End If

            Dim info As ApplicationFeeInfo = ApplicationFeeInfoFromDataRow(appNumber, ds.Tables(0).Rows(0))

            info.Invoices = ds.Tables(1)
            info.Payments = ds.Tables(2)

            Return info
        End Function

        Private Function ApplicationFeeInfoFromDataRow(appNumber As Integer, dr As DataRow) As ApplicationFeeInfo
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

        Public Function IsInvoiceGeneratedForApplication(appNumber As Integer) As Boolean
            Return DB.GetBoolean("select fees.IsInvoiceGeneratedForApplication(@AppNumber)", New SqlParameter("@AppNumber", appNumber))
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
