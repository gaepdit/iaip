Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Iaip.ApiCalls.ApiUtils
Imports Iaip.ApiCalls.WebRequest

Namespace ApiCalls.EmailQueue
    Friend Module EmailQueueApi
        Private ReadOnly ApiUrl As String = ConfigurationManager.AppSettings("EmailQueueApiUrl")
        Private ReadOnly ApiKey As String = ConfigurationManager.AppSettings("EmailQueueApiKey")

        Private Const SendEndpoint As String = "add"
        Private Const BatchEndpoint As String = "batch"

        Private ReadOnly EmailQueueRequestOptions As New Options With {
            .ContentType = ContentType.ApplicationJson,
            .Headers = New Net.WebHeaderCollection From {{"X-API-Key", ApiKey}}
        }

        Public Async Function SendEmailsAsync(emails As NewEmailTask()) As Task(Of EmailQueueResponse)
            If String.IsNullOrEmpty(ApiUrl) OrElse String.IsNullOrEmpty(ApiKey) Then Return Nothing
            Dim endpoint As Uri = UriCombine(ApiUrl, SendEndpoint)
            Dim jsonValue As String

            Try
                jsonValue = Await PostApiAsync(endpoint, emails, EmailQueueRequestOptions).ConfigureAwait(False)
            Catch ex As Exception
                Return Nothing
            End Try

            If String.IsNullOrEmpty(jsonValue) Then Return Nothing
            Return JsonSerializer.Deserialize(Of EmailQueueResponse)(jsonValue, JsonOptions)
        End Function

        Public Async Function GetBatchDetails(batchId As String) As Task(Of IEnumerable(Of EmailTaskViewModel))
            If String.IsNullOrEmpty(ApiUrl) OrElse String.IsNullOrEmpty(ApiKey) Then Return Nothing
            Dim endpoint As Uri = UriCombine(ApiUrl, BatchEndpoint)
            Dim batchRequest As New BatchRequest() With {.BatchId = batchId}
            Dim jsonValue As String

            Try
                jsonValue = Await PostApiAsync(endpoint, batchRequest, EmailQueueRequestOptions).ConfigureAwait(False)
            Catch ex As Exception
                Return Nothing
            End Try

            If String.IsNullOrEmpty(jsonValue) Then Return Nothing
            Return JsonSerializer.Deserialize(Of List(Of EmailTaskViewModel))(jsonValue, JsonOptions)
        End Function
    End Module
End Namespace
