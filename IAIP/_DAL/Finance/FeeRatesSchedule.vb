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

        Private Function FeeRateItems() As List(Of FeeRateItem)
            Return GetFeeRatesSchedule().Values.AsEnumerable().ToList()
        End Function

        Public Function FeeRateItemsAsOf(effectiveDate As Date) As List(Of FeeRateItem)
            Return FeeRateItems().Where(
                Function(m) m.BeginDate <= effectiveDate AndAlso (Not m.EndDate.HasValue OrElse m.EndDate.Value >= effectiveDate)
                ).ToList()
        End Function

        Public Function GetFeeRateAsOf(rateItem As Integer, effectiveDate As Date) As Decimal
            Dim ri As FeeRateItem = Nothing

            If Not GetFeeRatesSchedule().TryGetValue(rateItem, ri) Then
                Return 0
            End If

            If ri.Rates.Count = 0 OrElse ri.Rates.EarliestDate > effectiveDate Then
                Return 0
            End If

            Return ri.Rates.GetValueAt(effectiveDate)
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

    End Module
End Namespace
