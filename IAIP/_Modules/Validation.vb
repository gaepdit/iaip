Imports System.Text.RegularExpressions

Module Validation

    Public Function ValidateAsNonEmptyString(textBox As TextBox, errorProvider As ErrorProvider, readableName As String) As Boolean
        If Not String.IsNullOrWhiteSpace(textBox.Text.Trim) Then
            errorProvider.SetError(textBox, String.Empty)
            Return True
        Else
            errorProvider.SetError(textBox, readableName & " is required")
            Return False
        End If
    End Function

    Public Function ValidateAsEmailAddress(textBox As TextBox, errorProvider As ErrorProvider, readableName As String) As Boolean
        If IsValidEmailAddress(textBox.Text.Trim) Then
            errorProvider.SetError(textBox, String.Empty)
            Return True
        Else
            errorProvider.SetError(textBox, readableName & " is required")
            Return False
        End If
    End Function

    Public Function ValidateAsPhoneNumber(maskedTextBox As MaskedTextBox, errorProvider As ErrorProvider, readableName As String) As Boolean
        If IsValidPhoneNumber(maskedTextBox.Text) Then
            errorProvider.SetError(maskedTextBox, String.Empty)
            Return True
        Else
            errorProvider.SetError(maskedTextBox, readableName & " is required")
            Return False
        End If
    End Function

    Public Function IsValidEmailAddress(ByVal email As String) As Boolean
        If String.IsNullOrEmpty(email) Then Return False
        Try
            Dim testEmail As Net.Mail.MailAddress = New Net.Mail.MailAddress(email)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function IsValidPhoneNumber(phoneNumber As String) As Boolean
        Return Regex.IsMatch(phoneNumber, "^[2-9][0-9]{2}[2-9][0-9]{2}[0-9]{4}$")
    End Function

End Module
