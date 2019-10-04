Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks

Public Module NetworkCheck
    Private ReadOnly externalUri As Uri = New Uri("https://api.ipify.org")
    Private ReadOnly internalUri As Uri = New Uri(GecoUrl, "IP.ashx")
    Private ReadOnly gtaNetworkIpRange As IPAddressRange = New IPAddressRange("167.192.0.0", "167.200.255.255")
    Private ReadOnly privateNetworkIpRange As IPAddressRange = New IPAddressRange("10.0.0.0", "10.255.255.255")

    Public Enum NetworkCheckResponse
        InNetwork
        OnVpn
        OutOfNetwork
        NoNetwork
        TestSiteDown
    End Enum

    Public Async Function CheckNetworkAsync() As Task(Of NetworkCheckResponse)
        Dim externalTestSiteIsDown As Boolean = False

        Try
            ' Get IP address as seen by external (non-DNR) website
            ExternalIPAddress = Await GetIpAddressAsync(externalUri).ConfigureAwait(False)

            ' If client is within GTA network
            If gtaNetworkIpRange.IsInRange(ExternalIPAddress) Then
                Return NetworkCheckResponse.InNetwork
            End If
        Catch ex As HttpRequestException
            If Not ex.Message.Contains("404") Then
                Return NetworkCheckResponse.NoNetwork
            End If

            ' If external test site is down, continue to next test
            externalTestSiteIsDown = True
        End Try

        Try
            ' Get IP address as seen by internal (DNR) website
            InternalIPAddress = Await GetIpAddressAsync(internalUri).ConfigureAwait(False)

            ' If client is outside GTA network but connected to VPN
            If gtaNetworkIpRange.IsInRange(InternalIPAddress) Then
                Return NetworkCheckResponse.OnVpn
            End If

            If externalTestSiteIsDown Then
                ' If test site is down and client is on private network, 
                ' no way to tell if connected to GTA network
                If privateNetworkIpRange.IsInRange(InternalIPAddress) Then
                    Return NetworkCheckResponse.TestSiteDown
                End If
            End If
        Catch ex As Exception
            ' If internal test site is down, log as error
            If ex.Message.Contains("404") Then
                ErrorReport(ex, $"GECO IP test URL down: {internalUri}", False)
                Return NetworkCheckResponse.TestSiteDown
            End If

            Return NetworkCheckResponse.NoNetwork
        End Try

        Return NetworkCheckResponse.OutOfNetwork
    End Function

    Private Async Function GetIpAddressAsync(requestUri As Uri) As Task(Of IPAddress)
        Using httpClient As New HttpClient
            Dim ip As String = Await httpClient.GetStringAsync(requestUri).ConfigureAwait(False)
            Return IPAddress.Parse(ip)
        End Using
    End Function

End Module
