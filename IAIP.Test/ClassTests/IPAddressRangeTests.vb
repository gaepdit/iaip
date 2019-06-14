Imports Xunit

Public Class IPAddressRangeTests

    <Theory>
    <InlineData("222.168.2.1", "222.168.5.10", "222.168.3.55")>
    <InlineData("167.192.0.0", "167.200.255.255", "167.192.187.109")>
    <InlineData("167.192.0.0", "167.200.255.255", "167.192.0.0")>
    <InlineData("167.192.0.0", "167.200.255.255", "167.200.255.255")>
    Public Sub IpAddressIsInRange(lowerInclusive As String, upperInclusive As String, target As String)
        Dim range As New IPAddressRange(lowerInclusive, upperInclusive)

        Dim result As Boolean = range.IsInRange(target)

        Assert.True(result)
    End Sub

    <Theory>
    <InlineData("167.192.0.0", "167.200.255.255", "167.201.0.0")>
    <InlineData("167.192.0.0", "167.200.255.255", "167.191.255.255")>
    Public Sub IpAddressIsNotInRange(lowerInclusive As String, upperInclusive As String, target As String)
        Dim range As New IPAddressRange(lowerInclusive, upperInclusive)

        Dim result As Boolean = range.IsInRange(target)

        Assert.False(result)
    End Sub

End Class
