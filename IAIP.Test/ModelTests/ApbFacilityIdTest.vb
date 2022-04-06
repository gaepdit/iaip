Imports IAIP.Apb
Imports IAIP.Apb.ApbFacilityId
Imports Xunit

Public Class ApbFacilityIdTest

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    <InlineData("111")>
    <InlineData("ABC")>
    <InlineData("0010001")>
    <InlineData("001-0001")>
    Private Sub RejectsInvalidAirsNumbers(input As String)
        Assert.False(IsValidAirsNumberFormat(input))
    End Sub

    <Theory>
    <InlineData("00100001")>
    <InlineData("001-00001")>
    <InlineData("041300100001")>
    <InlineData("04-13-001-00001")>
    Private Sub AcceptsValidAirsNumbers(input As String)
        Assert.True(IsValidAirsNumberFormat(input))
    End Sub

    <Fact>
    Private Sub FacilityIdPropertiesCorrect()
        Dim airs As New ApbFacilityId("12345678")

        Assert.Equal("12345678", airs.ShortString)
        Assert.Equal("123", airs.CountySubstring)
        Assert.Equal("041312345678", airs.DbFormattedString)
        Assert.Equal("GA0000001312345678", airs.EpaFacilityIdentifier)
        Assert.Equal("123-45678", airs.FormattedString)
        Assert.Equal(12345678, airs.ToInt)
    End Sub

    <Fact>
    Private Sub FacilityIdCTypeWorks()
        Dim airs2 = CType("123-45678", ApbFacilityId)

        Assert.Equal("12345678", airs2.ShortString)
        Assert.Equal("123", airs2.CountySubstring)
        Assert.Equal("041312345678", airs2.DbFormattedString)
        Assert.Equal("GA0000001312345678", airs2.EpaFacilityIdentifier)
        Assert.Equal("123-45678", airs2.FormattedString)
        Assert.Equal(12345678, airs2.ToInt)
    End Sub

    <Theory>
    <InlineData("00100001", "001-00001")>
    <InlineData("041300100001", "04-13-001-00001")>
    Private Sub EqualityTrue(input1 As String, input2 As String)
        Dim airs1 As New ApbFacilityId(input1)
        Dim airs2 As New ApbFacilityId(input2)

        Assert.True(airs1 = airs2)
    End Sub

    <Theory>
    <InlineData("00100001", "00100002")>
    Private Sub EqualityFalse(input1 As String, input2 As String)
        Dim airs1 As New ApbFacilityId(input1)
        Dim airs2 As New ApbFacilityId(input2)

        Assert.False(airs1 = airs2)
    End Sub

    <Theory>
    <InlineData("00100001", "00100002")>
    Private Sub InequalityTrue(input1 As String, input2 As String)
        Dim airs1 As New ApbFacilityId(input1)
        Dim airs2 As New ApbFacilityId(input2)

        Assert.True(airs1 <> airs2)
    End Sub

    <Theory>
    <InlineData("00100001", "001-00001")>
    Private Sub InequalityFalse(input1 As String, input2 As String)
        Dim airs1 As New ApbFacilityId(input1)
        Dim airs2 As New ApbFacilityId(input2)

        Assert.False(airs1 <> airs2)
    End Sub

    <Fact>
    Private Sub NullEquality()
        Dim airs1 As ApbFacilityId = Nothing
        Dim airs2 As ApbFacilityId = Nothing

        Assert.True(airs1 = airs2)
    End Sub

    <Theory>
    <InlineData("00100001")>
    Private Sub NullInequality(input As String)
        Dim airs As New ApbFacilityId(input)

        Assert.True(airs <> Nothing)
    End Sub

End Class
