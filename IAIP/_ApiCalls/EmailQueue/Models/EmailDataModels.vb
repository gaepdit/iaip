Imports System.Collections.Generic
Imports System.ComponentModel

Namespace ApiCalls.EmailQueue
    Public Class EmailBatchStatus
        Public Property Status As String
        Public Property EmailCounts As EmailBatchCounts
        Public Shared Property Failed As New EmailBatchStatus With {.Status = "Failed", .EmailCounts = Nothing}

        Public Shared Function Ok(emailCounts As EmailBatchCounts) As EmailBatchStatus
            Return New EmailBatchStatus With {.Status = "Success", .EmailCounts = emailCounts}
        End Function
    End Class

    Public Class EmailBatchCounts
        <DisplayName("Total")> Public Property Count As Integer
        <DisplayName("Waiting")> Public Property Queued As Integer
        Public Property Sent As Integer
        Public Property Failed As Integer
        Public Property Skipped As Integer
    End Class

    Public Class EmailBatchDetails
        Public Property Status As String
        Public Property Emails As IEnumerable(Of EmailTask)
        Public Shared Property Failed As New EmailBatchDetails With {.Status = "Failed", .Emails = Nothing}

        Public Shared Function Ok(emails As List(Of EmailTask)) As EmailBatchDetails
            Return New EmailBatchDetails With {.Status = "Success", .Emails = emails}
        End Function
    End Class

    Public Class EmailTask
        Public Property Counter As Integer
        Public Property Status As String
        Public Property FailureReason As String
        Public Property AttemptedAt As Date?
        Public Property Subject As String
        Public Property Recipients As List(Of String)
    End Class

    Public Class EmailTaskViewModel
        Public Sub New(email As EmailTask)
            Id = email.Counter
            Status = email.Status
            Sent = If(email.AttemptedAt.HasValue, email.AttemptedAt.Value.ToLocalTime(), CType(Nothing, Date?))
            Subject = email.Subject
            Recipients = String.Join(", ", email.Recipients)
            FailureReason = IfEmpty(email.FailureReason, "N/A")
        End Sub

        Public Property Id As Integer
        Public Property Status As String
        Public Property Sent As Date?
        Public Property Subject As String
        Public Property Recipients As String
        <DisplayName("Failure Reason")> Public Property FailureReason As String
    End Class

End Namespace
