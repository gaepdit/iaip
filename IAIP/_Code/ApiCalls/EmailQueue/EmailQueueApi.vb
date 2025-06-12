Imports System.Collections.Generic
Imports System.Configuration
Imports System.Net
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Iaip.ApiCalls.ApiUtils
Imports Iaip.ApiCalls.WebRequest

Namespace ApiCalls.EmailQueue
    Friend Module EmailQueueApi
        Private ReadOnly ApiUrl As String = ConfigurationManager.AppSettings("EmailQueueApiUrl")

        Private Const SendEndpoint As String = "add"
        Private Const BatchDetailsEndpoint As String = "batch-details"
        Private Const BatchStatusEndpoint As String = "batch-status"

        Private ReadOnly EmailQueueRequestOptions As New Options With {
            .ContentType = ContentType.ApplicationJson,
            .Headers = New WebHeaderCollection From {
                {"X-Client-ID", CurrentAppConfig.EmailQueueClientId},
                {"X-API-Key", CurrentAppConfig.EmailQueueApiKey}
            }
        }

        Public Async Function SendEmailsAsync(emails As NewEmailTask()) As Task(Of EmailQueueResponse)
            If String.IsNullOrEmpty(ApiUrl) Then Return Nothing
            Dim endpoint As Uri = UriCombine(ApiUrl, SendEndpoint)
            Dim response As Response

            Try
                response = Await PostApiAsync(endpoint, emails, EmailQueueRequestOptions).ConfigureAwait(False)
            Catch ex As Exception
                Return EmailQueueResponse.Failed
            End Try

            If response Is Nothing OrElse response.Result.StatusCode <> HttpStatusCode.OK OrElse String.IsNullOrEmpty(response.Body) Then
                Return EmailQueueResponse.Failed
            End If

            Return EmailQueueResponse.Ok(JsonSerializer.Deserialize(Of EmailQueueResponseBody)(response.Body, JsonOptions))
        End Function

        Public Async Function GetBatchStatus(batchId As Guid?) As Task(Of EmailBatchStatus)
            If String.IsNullOrEmpty(ApiUrl) OrElse Not batchId.HasValue Then Return Nothing
            Dim endpoint As Uri = UriCombine(ApiUrl, BatchStatusEndpoint)
            Dim batchRequest As New BatchRequest() With {.BatchId = batchId}
            Dim response As Response

            Try
                response = Await PostApiAsync(endpoint, batchRequest, EmailQueueRequestOptions).ConfigureAwait(False)
            Catch ex As Exception
                Return Nothing
            End Try

            If response Is Nothing OrElse response.Result.StatusCode <> HttpStatusCode.OK OrElse String.IsNullOrEmpty(response.Body) Then
                Return EmailBatchStatus.Failed
            End If

            Return EmailBatchStatus.Ok(JsonSerializer.Deserialize(Of EmailBatchCounts)(response.Body, JsonOptions))
        End Function

        Public Async Function GetBatchDetails(batchId As Guid?) As Task(Of EmailBatchDetails)
            If String.IsNullOrEmpty(ApiUrl) OrElse Not batchId.HasValue Then Return Nothing
            Dim endpoint As Uri = UriCombine(ApiUrl, BatchDetailsEndpoint)
            Dim batchRequest As New BatchRequest() With {.BatchId = batchId}
            Dim response As Response

            Try
                response = Await PostApiAsync(endpoint, batchRequest, EmailQueueRequestOptions).ConfigureAwait(False)
            Catch ex As Exception
                Return Nothing
            End Try

            If response Is Nothing OrElse response.Result.StatusCode <> HttpStatusCode.OK OrElse String.IsNullOrEmpty(response.Body) Then
                Return EmailBatchDetails.Failed
            End If

            Return EmailBatchDetails.Ok(JsonSerializer.Deserialize(Of List(Of EmailTaskViewModel))(response.Body, JsonOptions))
        End Function

        Public Class BatchRequest
            Public Property BatchId As Guid
        End Class

    End Module
End Namespace
