Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Linq
Imports EpdIt.DBUtilities
Imports Iaip.Apb.Finance
Imports Iaip.SharedData

Namespace DAL.Finance
    Public Module FeeRatesSchedule

        Public Function GetFeeRatesSchedule() As Dictionary(Of Integer, FeeRateItem)
            Return GetSharedObject(Of Dictionary(Of Integer, FeeRateItem))(SharedObject.FeeRatesSchedule)
        End Function

        Public Function FeeRateItemsAsOf(effectiveDate As Date) As List(Of FeeRateItem)
            Dim FeeRateItems As List(Of FeeRateItem) = GetFeeRatesSchedule().Values.AsEnumerable().ToList()

            Return FeeRateItems.Where(
                Function(m) m.BeginDate <= effectiveDate AndAlso (Not m.EndDate.HasValue OrElse m.EndDate.Value >= effectiveDate)
                ).ToList()
        End Function

        Public Function GetFeeRateAsOf(rateItemID As Integer, effectiveDate As Date) As Decimal
            Dim rateItem As FeeRateItem = Nothing

            If Not GetFeeRatesSchedule().TryGetValue(rateItemID, rateItem) Then
                Return 0
            End If

            If rateItem.Rates.Count = 0 OrElse rateItem.Rates.EarliestDate > effectiveDate Then
                Return 0
            End If

            Return rateItem.Rates.GetValueAt(effectiveDate)
        End Function

        Public Function LoadFeeRatesSchedule() As Dictionary(Of Integer, FeeRateItem)
            Dim ds As DataSet = DB.SPGetDataSet("fees.GetFeeRatesSchedule")

            If ds Is Nothing OrElse ds.Tables.Count <> 2 Then
                Return Nothing
            End If

            Dim rates As New Dictionary(Of Integer, FeeRateItem)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim item As New FeeRateItem() With {
                    .BeginDate = CDate(dr.Item("BeginDate")),
                    .Description = GetNullableString(dr.Item("Description")),
                    .EndDate = GetNullableDateTime(dr.Item("EndDate")),
                    .RateCategory = CType(dr.Item("RateCategoryID"), FeeRateCategory),
                    .FeeRateItemID = CType(dr.Item("ID"), Integer)
                }

                rates.Add(item.FeeRateItemID, item)
            Next

            Dim rateItem As New FeeRateItem

            For Each dr As DataRow In ds.Tables(1).Rows
                If rates.TryGetValue(CType(dr.Item("ID"), Integer), rateItem) Then
                    rateItem.Rates.AddValue(CDate(dr.Item("EffectiveDate")), CDec(dr.Item("Rate")))
                End If
            Next

            Return rates
        End Function

        Public Function GetRateItemMaxUsedDate(rateItemID As Integer) As Date?
            Return DB.SPGetSingleValue(Of Date?)("fees.GetRateItemMaxUsedDate", New SqlParameter("@RateItemID", rateItemID))
        End Function

        Public Function UpdateRateItemDescription(rateItemID As Integer, description As String) As DbResult
            Dim params As SqlParameter() = {
                New SqlParameter("@Description", description),
                New SqlParameter("@RateItemID", rateItemID),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            If DB.SPRunCommand("fees.UpdateRateItemDescription", params) Then
                Return DbResult.Success
            End If

            Return DbResult.DbError
        End Function

        Public Function SaveNewRateItem(category As FeeRateCategory,
                                        description As String,
                                        amount As Decimal,
                                        effectiveDate As Date,
                                        ByRef newRateItemID As Integer) As DbResult
            newRateItemID = -1

            Dim params As SqlParameter() = {
                New SqlParameter("@Description", description),
                New SqlParameter("@RateCategoryID", CInt(category)),
                New SqlParameter("@Amount", amount),
                New SqlParameter("@EffectiveDate", effectiveDate),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            Dim returnValue As Integer

            newRateItemID = DB.SPGetInteger("fees.SaveNewRateItem", params, returnValue)

            Return CType(returnValue, DbResult)
        End Function

        Public Function AddNewEffectiveRate(rateItemID As Integer, rate As Decimal, effectiveDate As Date) As DbResult
            Dim params As SqlParameter() = {
                New SqlParameter("@RateItemID", rateItemID),
                New SqlParameter("@Rate", rate),
                New SqlParameter("@EffectiveDate", effectiveDate),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            If DB.SPRunCommand("fees.AddNewEffectiveRate", params) Then
                Return DbResult.Success
            End If

            Return DbResult.DbError
        End Function

        Public Function EndUseOfRateItem(rateItemID As Integer, endDate As Date) As DbResult
            Dim params As SqlParameter() = {
                New SqlParameter("@RateItemID", rateItemID),
                New SqlParameter("@EndDate", endDate),
                New SqlParameter("@UserID", CurrentUser.UserID)
            }

            If DB.SPRunCommand("fees.EndUseOfRateItem", params) Then
                Return DbResult.Success
            End If

            Return DbResult.DbError
        End Function

    End Module
End Namespace
