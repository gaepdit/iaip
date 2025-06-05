Imports System.Collections.Generic

Namespace ApiCalls.EmailQueue
    Public Class EmailBatchDetails
        Public Property Status As String

        Public Property Emails As IEnumerable(Of EmailTaskViewModel)

        Public Shared Property Failed As New EmailBatchDetails With {.Status = "Failed", .Emails = Nothing}

        Public Shared Function Ok(emails As List(Of EmailTaskViewModel)) As EmailBatchDetails
            Return New EmailBatchDetails With {.Status = "Success", .Emails = emails}
        End Function
    End Class

    Public Class EmailTaskViewModel
        Public Property Counter As Integer
        Public Property Status As String
        Public Property AttemptedAt As Date?
        Public Property Subject As String
        Public Property Recipients As List(Of String)
    End Class
End Namespace
