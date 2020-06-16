Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks

Module GtaNetworkCheck

    Private ReadOnly externalUri As Uri = New Uri("https://api.ipify.org")
    Private ReadOnly gtaNetworkIpRange As IPAddressRange = New IPAddressRange("167.192.0.0", "167.200.255.255")

    Public Enum CheckGtaNetworkResponse
        NoNetwork
        InGtaNetwork
        OutOfGtaNetwork
        Unknown
    End Enum

    Public Async Function GetGtaNetworkStatusAsync() As Task(Of CheckGtaNetworkResponse)
        Try
            ' Get IP address as seen by external (non-DNR) website
            ExternalIPAddress = Await GetIpAddressAsync(externalUri).ConfigureAwait(False)
        Catch ex As HttpRequestException
            If ex.Message.Contains("404") Then
                ' If external test site is down, can't make a determination
                Return CheckGtaNetworkResponse.Unknown
            End If

            Return CheckGtaNetworkResponse.NoNetwork
        End Try

        ' If client is within GTA IP range
        If gtaNetworkIpRange.IsInRange(ExternalIPAddress) Then
                Return CheckGtaNetworkResponse.InGtaNetwork
            End If

            Return CheckGtaNetworkResponse.OutOfGtaNetwork
    End Function

    Private Async Function GetIpAddressAsync(requestUri As Uri) As Task(Of IPAddress)
        Using httpClient As New HttpClient
            Dim ip As String = Await httpClient.GetStringAsync(requestUri).ConfigureAwait(False)
            Return IPAddress.Parse(ip)
        End Using
    End Function

End Module
