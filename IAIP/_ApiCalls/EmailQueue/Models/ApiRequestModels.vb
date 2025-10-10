Namespace ApiCalls.EmailQueue

    Friend Class EmailMessage
        Public Sub New(subject As String, body As String, from As String, recipients() As String)
            Me.From = NotNullOrWhiteSpace(from, NameOf(from))
            Me.Recipients = NotNullOrValueless(recipients, NameOf(recipients))
            Me.Subject = NotNullOrWhiteSpace(subject, NameOf(subject))
            Me.Body = NotNullOrWhiteSpace(body, NameOf(body))
        End Sub

        Public Property From As String ' StringLength(100)
        Public Property FromName As String ' StringLength(100)
        Public Property Recipients As String()
        Public Property CopyRecipients As String()
        Public Property Subject As String ' StringLength(200)
        Public Property Body As String ' StringLength(20000)
        Public Property IsHtml As Boolean
    End Class

    Friend Class EmailsForBatchRequest
        Public Property BatchId As Guid
        Public Property Emails As EmailMessage()
    End Class

End Namespace
