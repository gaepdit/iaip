Imports System.Text.RegularExpressions

Module IaipValidation

    Public Function IsValidNonEmptyString(s As String, Optional minLength As Integer = 0) As Boolean
        If String.IsNullOrWhiteSpace(s.Trim) Then Return False
        If minLength > 0 AndAlso s.Trim.Length < minLength Then Return False
        Return True
    End Function

    Public Function IsValidEmailAddress(ByVal emailAddress As String, Optional requireDnrAddress As Boolean = False) As Boolean
        If String.IsNullOrEmpty(emailAddress) Then Return False
        If requireDnrAddress AndAlso Not Regex.IsMatch(emailAddress, DnrEmailPattern) Then Return False

        Try
            Dim testEmail As Net.Mail.MailAddress = New Net.Mail.MailAddress(emailAddress)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Enum StringValidationResult
        Valid
        Empty
        TooShort
        InvalidCharacters
    End Enum

    Public Function IsValidUsername(username As String, Optional minLength As Integer = MinUsernameLength) As StringValidationResult
        If String.IsNullOrEmpty(username) Then Return StringValidationResult.Empty
        If Not Regex.IsMatch(username, AlphaNumericPattern) Then Return StringValidationResult.InvalidCharacters
        If username.Length < minLength Then Return StringValidationResult.TooShort
        Return StringValidationResult.Valid
    End Function

    Public Function IsValidPassword(password As String, Optional minLength As Integer = MinPasswordLength) As StringValidationResult
        If String.IsNullOrEmpty(password) Then Return StringValidationResult.Empty
        If password.Length < minLength Then Return StringValidationResult.TooShort
        Return StringValidationResult.Valid
    End Function

    'Public Function IsValidPhoneNumber(phoneNumber As String) As Boolean
    '    If String.IsNullOrEmpty(phoneNumber) Then Return False
    '    Return Regex.IsMatch(phoneNumber, MatchPhoneNumberPattern)
    'End Function

End Module
