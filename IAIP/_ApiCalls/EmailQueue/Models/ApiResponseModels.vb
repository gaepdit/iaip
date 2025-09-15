Namespace ApiCalls.EmailQueue

    Friend Class EmailQueueApiResponse
        Public Property Status As String
        Public Property Body As EmailQueueResponseBody

        Public Shared Failed As New EmailQueueApiResponse With {.Status = "Failed", .Body = Nothing}

        Public Shared Function Ok(body As EmailQueueResponseBody) As EmailQueueApiResponse
            Return New EmailQueueApiResponse With {.Status = body.Status, .Body = body}
        End Function
    End Class

    Friend Class EmailQueueResponseBody
        Public Property Status As String
        Public Property Count As Integer
        Public Property BatchId As String
    End Class

End Namespace
