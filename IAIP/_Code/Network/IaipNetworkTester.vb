Imports System.Threading.Tasks

Public Module IaipNetworkTester

    Public Async Function GetIaipNetworkStatusAsync() As Task(Of IaipNetworkStatus)
        If Await IsDbPingableAsync().ConfigureAwait(False) Then
            If Await IsIaipEnabledAsync().ConfigureAwait(False) Then
                Return IaipNetworkStatus.Enabled
            End If

            Return IaipNetworkStatus.AppDisabled
        End If

        Select Case Await GetNetworkStatusAsync().ConfigureAwait(False)
            Case NetworkCheckResponse.InNetwork,
                 NetworkCheckResponse.OnVpn
                Return IaipNetworkStatus.NoDb

            Case NetworkCheckResponse.OutOfNetwork
                Return IaipNetworkStatus.NoVpn

            Case Else
                Return IaipNetworkStatus.NoInternet

        End Select
    End Function

End Module

Public Enum IaipNetworkStatus
    Enabled
    AppDisabled
    NoDb
    NoInternet
    NoVpn
End Enum
