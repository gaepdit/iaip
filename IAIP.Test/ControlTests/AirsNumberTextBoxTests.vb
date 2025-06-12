Imports IAIP.DAL
Imports Xunit

Public Class AirsNumberTextBoxTests

    <Fact>
    Private Sub InitialValues()
        Using airsTextBox As New AirsNumberTextBox()
            Assert.Null(airsTextBox.AirsNumber)
            Assert.Equal(9, airsTextBox.MaxLength)
            Assert.Equal("000-00000", airsTextBox.Cue)
            Assert.False(airsTextBox.FacilityMustExist)
            Assert.Equal(AirsNumberValidationResult.Empty, airsTextBox.ValidationStatus)
        End Using
    End Sub

    <Fact>
    Private Sub InvalidFormat()
        Dim input As String = "invalid"

        Using airsTextBox As New AirsNumberTextBox With {.Text = input}
            Dim result As Boolean = airsTextBox.Validate()

            Assert.True(result)
            Assert.Equal(AirsNumberValidationResult.InvalidFormat, airsTextBox.ValidationStatus)
            Assert.Equal(input, airsTextBox.Text)
        End Using
    End Sub

    <Fact>
    Private Sub ValidInput()
        Dim input As String = "00199999"

        Using airsTextBox As New AirsNumberTextBox With {.Text = input}
            Dim result As Boolean = airsTextBox.Validate()

            Assert.True(result)
            Assert.Equal(AirsNumberValidationResult.Valid, airsTextBox.ValidationStatus)
            Assert.Equal(String.Concat(input.Substring(0, 3), "-", input.Substring(input.Length - 5)), airsTextBox.Text)
        End Using
    End Sub

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    Private Sub EmptyInput(input As String)
        Using airsTextBox As New AirsNumberTextBox With {.Text = input}
            Dim result As Boolean = airsTextBox.Validate()

            Assert.True(result)
            Assert.Equal(AirsNumberValidationResult.Empty, airsTextBox.ValidationStatus)
            Assert.Equal(String.Empty, airsTextBox.Text)
        End Using
    End Sub

    <Fact>
    Private Sub ValidationStatusChanged()
        Using airsTextBox As New AirsNumberTextBox()
            AddHandler airsTextBox.ValidationStatusChanged, AddressOf Airs_ValidationStatusChanged

            Assert.Null(airsTextBox.Tag)

            airsTextBox.Text = "001-00001"
            airsTextBox.Validate()

            Assert.NotNull(airsTextBox.Tag)
            Assert.True(CBool(airsTextBox.Tag))
        End Using
    End Sub

    Private Sub Airs_ValidationStatusChanged(sender As Object, e As EventArgs)
        Dim airsTextBox As AirsNumberTextBox = CType(sender, AirsNumberTextBox)
        airsTextBox.Tag = True
    End Sub

End Class
