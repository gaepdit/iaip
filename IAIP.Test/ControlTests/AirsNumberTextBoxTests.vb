Imports IAIP.DAL
Imports IAIP.Test.ControlExtensions
Imports Xunit

Public Class AirsNumberTextBoxTests

    Private ReadOnly airs As AirsNumberTextBox

    Public Sub New()
        airs = New AirsNumberTextBox()
    End Sub

    <Fact>
    Private Sub InitialValues()
        Assert.Null(airs.AirsNumber)
        Assert.Equal(9, airs.MaxLength)
        Assert.Equal("000-00000", airs.Cue)
        Assert.False(airs.FacilityMustExist)
        Assert.Equal(AirsNumberValidationResult.Empty, airs.ValidationStatus)
    End Sub

    <Theory>
    <InlineData("111")>
    <InlineData("ABC")>
    <InlineData("0010001")>
    <InlineData("001-0001")>
    Private Sub InvalidFormat(input As String)
        airs.Text = input
        Dim result As Boolean = airs.Validate()

        Assert.True(result)
        Assert.Equal(AirsNumberValidationResult.InvalidFormat, airs.ValidationStatus)
        Assert.Equal(input, airs.Text)
    End Sub

    <Theory>
    <InlineData("00100001")>
    <InlineData("001-00001")>
    <InlineData("99999999")>
    <InlineData("999-99999")>
    Private Sub ValidFormat(input As String)
        airs.Text = input
        Dim result As Boolean = airs.Validate()

        Assert.True(result)
        Assert.Equal(AirsNumberValidationResult.Valid, airs.ValidationStatus)
        Assert.Equal(String.Concat(input.Substring(0, 3), "-", input.Substring(input.Length - 5)), airs.Text)
    End Sub

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    Private Sub EmptyInput(input As String)
        airs.Text = input
        Dim result As Boolean = airs.Validate()

        Assert.True(result)
        Assert.Equal(AirsNumberValidationResult.Empty, airs.ValidationStatus)
        Assert.Equal(String.Empty, airs.Text)
    End Sub

    <Fact>
    Private Sub ValidationStatusChanged()
        AddHandler airs.ValidationStatusChanged, AddressOf Airs_ValidationStatusChanged

        Assert.Null(airs.Tag)

        airs.Text = "001-00001"
        airs.Validate()

        Assert.NotNull(airs.Tag)
        Assert.True(CBool(airs.Tag))
    End Sub

    Private Sub Airs_ValidationStatusChanged(sender As Object, e As System.EventArgs)
        airs.Tag = True
    End Sub

End Class
