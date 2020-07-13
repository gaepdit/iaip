﻿Imports System.Net.NetworkInformation

Public Module VpnStatusCheck

    Public Function IsVpnConnected() As Boolean
        If NetworkInterface.GetIsNetworkAvailable() Then
            For Each adapter As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                If adapter.OperationalStatus = OperationalStatus.Up AndAlso
                    adapter.Description = "Juniper Networks Virtual Adapter" Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

End Module
