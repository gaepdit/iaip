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
        Private Const SendForBatchEndpoint As String = "add-to-batch"
        Private Const BatchDetailsEndpoint As String = "batch-details"
        Private Const BatchStatusEndpoint As String = "batch-status"

        Private ReadOnly EmailQueueRequestOptions As New Options With {
            .ContentType = ContentType.ApplicationJson,
            .Headers = New WebHeaderCollection From {
                {"X-Client-ID", CurrentAppConfig.EmailQueueClientId},
                {"X-API-Key", CurrentAppConfig.EmailQueueApiKey}
            }
        }

        Private Async Function InternalSendAsync(body As Object, endpoint As String) As Task(Of EmailQueueApiResponse)
            If String.IsNullOrEmpty(ApiUrl) Then Return Nothing
            Dim endpointUri As Uri = UriCombine(ApiUrl, endpoint)
            Dim response As Response

            Try
                response = Await PostApiAsync(endpointUri, body, EmailQueueRequestOptions).ConfigureAwait(False)
            Catch ex As Exception
                Return EmailQueueApiResponse.Failed
            End Try

            If response Is Nothing OrElse response.Result.StatusCode <> HttpStatusCode.OK OrElse String.IsNullOrEmpty(response.Body) Then
                Return EmailQueueApiResponse.Failed
            End If

            Return EmailQueueApiResponse.Ok(JsonSerializer.Deserialize(Of EmailQueueResponseBody)(response.Body, JsonOptions))
        End Function

        Public Function SendEmailAsync(emails As NewEmailTask()) As Task(Of EmailQueueApiResponse)
            Return InternalSendAsync(emails, SendEndpoint)
        End Function

        Public Function SendEmailAsync(email As NewEmailTask) As Task(Of EmailQueueApiResponse)
            Return SendEmailAsync({email})
        End Function

        Public Function SendEmailAsync(batchId As Guid, emails As NewEmailTask()) As Task(Of EmailQueueApiResponse)
            Dim request As New EmailsForBatchRequest() With {
                .BatchId = batchId,
                .Emails = emails
            }

            Return InternalSendAsync(request, SendForBatchEndpoint)
        End Function

        Public Function SendEmailAsync(batchId As Guid, Email As NewEmailTask) As Task(Of EmailQueueApiResponse)
            Return SendEmailAsync(batchId, {Email})
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

            Return EmailBatchDetails.Ok(JsonSerializer.Deserialize(Of List(Of EmailTask))(response.Body, JsonOptions))
        End Function

        Public Class BatchRequest
            Public Property BatchId As Guid
        End Class

    End Module
End Namespace
