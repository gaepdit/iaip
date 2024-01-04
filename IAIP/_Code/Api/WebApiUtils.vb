' API GET/POST calls
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Utils.WebRequest

Public Module WebApiUtils
    Public Async Function GetApiAsync(url As String) As Task(Of String)
        ' Optional: Specify request options
        Dim options As New Options With {.ContentType = ContentType.ApplicationJson}

        ' Execute a get request at the following url
        Return (Await GetAsync(url, options).ConfigureAwait(False)).Body
    End Function

    Public Async Function PostApiAsync(url As String, request As Object) As Task(Of String)
        ' Optional: Specify request options
        Dim options As New Options With {.ContentType = ContentType.ApplicationJson}

        ' Execute a post request at the following url
        Return (Await PostAsync(url, JsonSerializer.Serialize(request), options).ConfigureAwait(False)).Body
    End Function
End Module
