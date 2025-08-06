Imports System.Collections.Generic

Namespace ApiCalls.EmailQueue

    Friend Class NewEmailTask
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
        Public Property Emails As NewEmailTask()
    End Class

End Namespace
