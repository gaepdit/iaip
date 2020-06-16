Imports System.Threading.Tasks

Public Module NetworkStatusCheck

    Public Enum NetworkCheckResponse
        NoInternet
        InNetwork
        OnVpn
        OutOfNetwork
        Unknown
    End Enum

    Public Async Function GetNetworkStatusAsync() As Task(Of NetworkCheckResponse)
        Dim gtaNetworkStatus As CheckGtaNetworkResponse = Await GetGtaNetworkStatusAsync().ConfigureAwait(False)

        ' No Internet connection
        If gtaNetworkStatus = CheckGtaNetworkResponse.NoNetwork Then
            Return NetworkCheckResponse.NoInternet
        End If

        ' Inside the GTA network
        If gtaNetworkStatus = CheckGtaNetworkResponse.InGtaNetwork Then
            Return NetworkCheckResponse.InNetwork
        End If

        ' Outside the GTA network but connected to VPN
        If IsVpnConnected() Then
            Return NetworkCheckResponse.OnVpn
        End If

        ' IP test site not found
        If gtaNetworkStatus = CheckGtaNetworkResponse.Unknown Then
            Return NetworkCheckResponse.Unknown
        End If

        ' Not in GTA network and not connected to VPN
        Return NetworkCheckResponse.OutOfNetwork
    End Function

End Module
