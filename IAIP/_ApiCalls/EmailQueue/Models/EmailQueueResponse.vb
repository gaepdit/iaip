Namespace ApiCalls.EmailQueue

    Friend Class EmailQueueResponse
        Public Property Status As String
        Public Property Body As EmailQueueResponseBody

        Public Shared Failed As New EmailQueueResponse With {.Status = "Failed", .Body = Nothing}

        Public Shared Function Ok(body As EmailQueueResponseBody) As EmailQueueResponse
            Return New EmailQueueResponse With {.Status = body.Status, .Body = body}
        End Function
    End Class

    Friend Class EmailQueueResponseBody
            Public Property Status As String
            Public Property Count As Integer
            Public Property BatchId As String
        End Class

End Namespace
