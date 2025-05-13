' API GET/POST calls
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Iaip.ApiCalls.WebRequest

Namespace ApiCalls.ApiUtils
    Friend Module WebApiUtils
        Public ReadOnly JsonOptions As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
        Public ReadOnly DefaultRequestOptions As New Options With {.ContentType = ContentType.ApplicationJson}

        Public Function GetApiAsync(url As Uri, Optional requestOptions As Options = Nothing) As Task(Of Response)
            Return GetApiAsync(url.ToString(), requestOptions)
        End Function

        Public Async Function GetApiAsync(url As String, Optional requestOptions As Options = Nothing) As Task(Of Response)
            If requestOptions Is Nothing Then requestOptions = DefaultRequestOptions
            Return (Await GetAsync(url, requestOptions).ConfigureAwait(False))
        End Function

        Public Function PostApiAsync(url As Uri, request As Object, Optional requestOptions As Options = Nothing) As Task(Of Response)
            Return PostApiAsync(url.ToString(), request, requestOptions)
        End Function

        Public Async Function PostApiAsync(url As String, request As Object, Optional requestOptions As Options = Nothing) As Task(Of Response)
            If requestOptions Is Nothing Then requestOptions = DefaultRequestOptions
            Return Await PostAsync(url, JsonSerializer.Serialize(request), requestOptions).ConfigureAwait(False)
        End Function

        Public Function UriCombine(baseUrl As String, endpoint As String) As Uri
            Return UriCombine(New Uri(baseUrl), endpoint)
        End Function

        Public Function UriCombine(baseUri As Uri, endpoint As String) As Uri
            If Not baseUri.IsAbsoluteUri Then Throw New ArgumentOutOfRangeException(NameOf(baseUri))
            If String.IsNullOrEmpty(endpoint) Then Return baseUri
            Const separator As Char = "/"c
            Return New Uri($"{baseUri.ToString().TrimEnd(separator)}{separator}{endpoint.TrimStart(separator)}")
        End Function
    End Module
End Namespace
