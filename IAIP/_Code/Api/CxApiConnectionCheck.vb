Imports System.Configuration
Imports System.Net.NetworkInformation
Imports System.Threading.Tasks

Module CxApiConnectionCheck
    'Public Async Function IsCxApiPingableAsync() As Task(Of Boolean)
    '    Return Await Task.Run(
    '        Function()
    '            Return PingApi()
    '        End Function
    '        ).ConfigureAwait(False)
    'End Function

    'Private Function PingApi() As Boolean
    '    Dim CxApiUrl As New Uri(ConfigurationManager.AppSettings("CxApiUrl"))

    '    Using pingSender As New Ping()
    '        Dim reply As PingReply

    '        Try
    '            reply = pingSender.Send(CxApiUrl.Authority)
    '        Catch ex As PingException
    '            Return False
    '        End Try

    '        Return reply.Status = IPStatus.Success
    '    End Using
    'End Function

End Module
