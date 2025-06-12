Imports System.Collections.Generic

Namespace ApiCalls.EmailQueue
    Friend Class NewEmailTask
        Public Property From As String ' StringLength(100)
        Public Property FromName As String ' StringLength(100)
        Public Property Recipients As List(Of String)
        Public Property CopyRecipients As List(Of String)
        Public Property Subject As String ' StringLength(200)
        Public Property Body As String ' StringLength(20000)
        Public Property IsHtml As Boolean
    End Class
End Namespace
