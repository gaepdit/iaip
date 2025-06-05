Imports IAIP.Apb
Imports IAIP.Apb.ApbFacilityId
Imports Xunit

Public Class ApbFacilityIdTest

    <Theory>
    <InlineData("00100001")>
    <InlineData("00110000")>
    <InlineData("00199999")>
    <InlineData("32100001")>
    <InlineData("32110000")>
    <InlineData("32199999")>
    <InlineData("77700001")>
    <InlineData("77710000")>
    <InlineData("77799999")>
    <InlineData("001-00001")>
    <InlineData("001-10000")>
    <InlineData("001-99999")>
    <InlineData("321-00001")>
    <InlineData("321-10000")>
    <InlineData("321-99999")>
    <InlineData("777-00001")>
    <InlineData("777-10000")>
    <InlineData("777-99999")>
    <InlineData("1-1")>
    <InlineData("1-10")>
    <InlineData("1-10000")>
    <InlineData("1-99999")>
    <InlineData("321-1")>
    <InlineData("777-1")>
    <InlineData("321-10")>
    <InlineData("777-10")>
    <InlineData("1-01")>
    <InlineData("1-10")>
    <InlineData("1-001")>
    <InlineData("1-0001")>
    <InlineData("1-00001")>
    <InlineData("01-1")>
    <InlineData("01-10")>
    <InlineData("01-99999")>
    Private Sub AcceptsValidAirsNumbers(input As String)
        Assert.True(IsValidAirsNumberFormat(input))
    End Sub

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    <InlineData("111")>
    <InlineData("ABC")>
    <InlineData("041300100001")>
    <InlineData("04-13-001-00001")>
    <InlineData("0413-001-0001")>
    <InlineData("abc-defgh")>
    <InlineData("001-0000a")>
    <InlineData("0100001")>
    <InlineData("0010001")>
    <InlineData("00200001")>
    <InlineData("002-00001")>
    <InlineData("2-1")>
    <InlineData("32300001")>
    <InlineData("323-00001")>
    <InlineData("323-1")>
    <InlineData("00000001")>
    <InlineData("000-00001")>
    <InlineData("000-99999")>
    <InlineData("0-1")>
    <InlineData("00100000")>
    <InlineData("001-00000")>
    <InlineData("001-0")>
    <InlineData("1-0")>
    Private Sub RejectsInvalidAirsNumbers(input As String)
        Assert.False(IsValidAirsNumberFormat(input))
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
        Dim airs = CType("123-45678", ApbFacilityId)

        Assert.Equal("12345678", airs.ShortString)
        Assert.Equal("123", airs.CountySubstring)
        Assert.Equal("041312345678", airs.DbFormattedString)
        Assert.Equal("GA0000001312345678", airs.EpaFacilityIdentifier)
        Assert.Equal("123-45678", airs.FormattedString)
        Assert.Equal(12345678, airs.ToInt)
    End Sub

    <Fact>
    Private Sub AbbreviatedFacilityIdCTypeWorks()
        Dim airs = CType("1-1", ApbFacilityId)

        Assert.Equal("00100001", airs.ShortString)
        Assert.Equal("001", airs.CountySubstring)
        Assert.Equal("041300100001", airs.DbFormattedString)
        Assert.Equal("GA0000001300100001", airs.EpaFacilityIdentifier)
        Assert.Equal("001-00001", airs.FormattedString)
        Assert.Equal(100001, airs.ToInt)
    End Sub

    <Theory>
    <InlineData("001-00001", "00100001")>
    Private Sub FormatNormalizationWorks(input As String, output As String)
        Dim airs As New ApbFacilityId(input)
        Assert.Equal(output, airs.ShortString)
    End Sub

    <Theory>
    <InlineData("00100001", "001-00001")>
    <InlineData("00100001", "1-1")>
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
    <InlineData("00100001", "1-1")>
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
