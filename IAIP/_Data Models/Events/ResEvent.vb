Namespace Apb.Res

    Public Class ResEvent

        Public Property EventId As Integer
        Public Property Active As Boolean = True
        Public Property EventStatus As EventState = EventState.Scheduled
        Public Property WebContactId As Integer
        Public Property WebContactPhone As String
        Public Property Title As String
        Public Property Description As String
        Public Property StartDate As Date
        Public Property EndDate As Date?
        Public Property Venue As String
        Public Property Capacity As Integer
        Public Property Notes As String
        Public Property PassCode As String
        Public Property Street As String
        Public Property City As String
        Public Property State As String
        Public Property PostalCode As String
        Public Property StartTime As String
        Public Property EndTime As String
        Public Property WebLink As String

        Public Enum EventState
            Scheduled = 2
            Cancelled = 3
        End Enum

    End Class

End Namespace
