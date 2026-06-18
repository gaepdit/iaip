Imports System.Collections.Generic
Imports System.Configuration
Imports System.Net
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Iaip.ApiCalls.ApiUtils
Imports Iaip.ApiCalls.WebRequest

Namespace ApiCalls.Notifications

    Friend Module NotificationsApi

        Private ReadOnly RequestOptions As New Options With {.ContentType = ContentType.ApplicationJson}

        Public Async Function CheckNotificationApiAsync() As Task(Of List(Of OrgNotificationModel))
            Dim uriString As String = ConfigurationManager.AppSettings("NotificationsApiUrl")
            If String.IsNullOrEmpty(uriString) Then Return Nothing

            Dim StatusEndpoint As Uri = UriCombine(New Uri(uriString).AbsoluteUri, "current")
            Dim response As Response = Await GetApiAsync(StatusEndpoint, RequestOptions).ConfigureAwait(False)

            If response Is Nothing OrElse response.Result.StatusCode <> HttpStatusCode.OK OrElse response.Body Is Nothing Then
                Return Nothing
            End If

            Return JsonSerializer.Deserialize(Of List(Of OrgNotificationModel))(response.Body, JsonOptions)
        End Function

    End Module

    Friend Class OrgNotificationModel
        Public Property Message As String
    End Class

End Namespace