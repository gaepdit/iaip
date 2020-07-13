Imports System.Configuration
Imports System.Net.NetworkInformation
Imports System.Threading.Tasks

Module DbConnectionCheck
    Private ReadOnly dbIpAddress As String = ConfigurationManager.AppSettings("DbIpAddress")

    Public Async Function IsDbPingableAsync() As Task(Of Boolean)
        Return Await Task.Run(
            Function()
                Return PingDb()
            End Function
            ).ConfigureAwait(False)
    End Function

    Private Function PingDb() As Boolean
        Using pingSender As New Ping()
            Dim reply As PingReply

            Try
                reply = pingSender.Send(dbIpAddress)
            Catch ex As PingException
                Return False
            End Try

            Return reply.Status = IPStatus.Success
        End Using
    End Function

End Module
