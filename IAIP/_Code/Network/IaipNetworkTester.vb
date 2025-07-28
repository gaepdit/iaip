Imports System.Threading.Tasks

Public Module IaipNetworkTester

    Public Async Function GetIaipNetworkStatusAsync() As Task(Of IaipNetworkStatus)
        Dim networkStatus As NetworkCheckResponse = Await GetNetworkStatusAsync().ConfigureAwait(False)

        Select Case networkStatus
            Case NetworkCheckResponse.OutOfNetwork
                Return IaipNetworkStatus.NoVpn
        End Select

        Try

            If Await DAL.IsIaipEnabledAsync().ConfigureAwait(False) Then
                Return IaipNetworkStatus.Enabled
            End If

            Return IaipNetworkStatus.AppDisabled

        Catch ex As Exception

            Select Case networkStatus
                Case NetworkCheckResponse.InNetwork, NetworkCheckResponse.OnVpn
                    Return IaipNetworkStatus.NoDb

                Case Else
                    Return IaipNetworkStatus.NoInternet
            End Select

        End Try
    End Function

End Module

Public Enum IaipNetworkStatus
    Enabled
    AppDisabled
    NoDb
    NoInternet
    NoVpn
End Enum
