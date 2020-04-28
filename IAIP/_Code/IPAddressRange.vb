Imports System.Net
Imports System.Net.Sockets

' From https://stackoverflow.com/a/2138724/212978
Public Class IPAddressRange
    Private ReadOnly addressFamily As AddressFamily
    Private ReadOnly lowerBytes As Byte()
    Private ReadOnly upperBytes As Byte()

    Public Sub New(lowerInclusive As IPAddress, upperInclusive As IPAddress)
        ArgumentNotNull(lowerInclusive, NameOf(lowerInclusive))
        ArgumentNotNull(upperInclusive, NameOf(upperInclusive))

        addressFamily = lowerInclusive.AddressFamily
        lowerBytes = lowerInclusive.GetAddressBytes()
        upperBytes = upperInclusive.GetAddressBytes()
    End Sub

    Public Sub New(lowerInclusive As String, upperInclusive As String)
        Me.New(IPAddress.Parse(lowerInclusive), IPAddress.Parse(upperInclusive))
    End Sub

    Public Function IsInRange(address As String) As Boolean
        Return IsInRange(IPAddress.Parse(address))
    End Function

    Public Function IsInRange(address As IPAddress) As Boolean
        ArgumentNotNull(address, NameOf(address))

        If address.AddressFamily <> addressFamily Then
            Return False
        End If

        Dim addressBytes As Byte() = address.GetAddressBytes()
        Dim lowerBoundary As Boolean = True, upperBoundary As Boolean = True
        Dim i As Integer = 0

        While i < lowerBytes.Length AndAlso (lowerBoundary OrElse upperBoundary)

            If (lowerBoundary AndAlso addressBytes(i) < lowerBytes(i)) OrElse (upperBoundary AndAlso addressBytes(i) > upperBytes(i)) Then
                Return False
            End If

            lowerBoundary = lowerBoundary AndAlso (addressBytes(i) = lowerBytes(i))
            upperBoundary = upperBoundary AndAlso (addressBytes(i) = upperBytes(i))
            i += 1
        End While

        Return True
    End Function
End Class
