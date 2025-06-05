Namespace Apb.ApplicationFees
    Public Class FeeRateItem

        Public Property FeeRateItemID As Integer
        Public Property Description As String
        Public Property RateCategory As FeeRateCategory
        Public Property BeginDate As Date
        Public Property EndDate As Date?
        Public Property Rates As New TemporalCollection(Of Decimal)

    End Class
End Namespace
