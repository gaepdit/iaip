Imports System.Collections.Generic

Namespace ApiCalls.EmailQueue
    Public Class EmailBatchDetails
        Public Property Status As String
        Public Property Emails As IEnumerable(Of EmailTaskViewModel)

        Public Shared Failed As New EmailBatchDetails With {.Status = "Failed", .Emails = Nothing}

        Public Shared Function Ok(emails As List(Of EmailTaskViewModel)) As EmailBatchDetails
            Return New EmailBatchDetails With {.Status = "Success", .Emails = emails}
        End Function

    End Class

    Public Class EmailTaskViewModel
        Public Property Counter As Integer
        Public Property Status As String
        Public Property ApiKeyOwner As String
        Public Property CreatedAt As Date
        Public Property AttemptedAt As Date?
        Public Property Recipients As List(Of String)
        Public Property From As String
        Public Property Subject As String
    End Class
End Namespace
