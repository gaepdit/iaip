Imports System.Collections.Generic

Namespace ApiCalls.EmailQueue
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
