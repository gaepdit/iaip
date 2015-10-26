Imports System.Text.RegularExpressions

Module Validation

    Public Function IsValidNonEmptyString(s As String, Optional minLength As Integer = 0) As Boolean
        If String.IsNullOrWhiteSpace(s.Trim) Then Return False
        If minLength > 0 AndAlso s.Trim.Length < minLength Then Return False
        Return True
    End Function

    Public Function IsValidEmailAddress(ByVal emailAddress As String) As Boolean
        If String.IsNullOrEmpty(emailAddress) Then Return False
        Try
            Dim testEmail As Net.Mail.MailAddress = New Net.Mail.MailAddress(emailAddress)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Enum UserNameValidationResult
        Valid
        Empty
        TooShort
        InvalidCharacters
    End Enum

    Public Function IsValidUserName(username As String, Optional minLength As Integer = MinUsernameLength) As UserNameValidationResult
        If String.IsNullOrEmpty(username) Then Return UserNameValidationResult.Empty
        If Not Regex.IsMatch(username, AlphaNumericPattern) Then Return UserNameValidationResult.InvalidCharacters
        If username.Length < minLength Then Return UserNameValidationResult.TooShort
        Return UserNameValidationResult.Valid
    End Function

    'Public Function IsValidPhoneNumber(phoneNumber As String) As Boolean
    '    If String.IsNullOrEmpty(phoneNumber) Then Return False
    '    Return Regex.IsMatch(phoneNumber, MatchPhoneNumberPattern)
    'End Function

End Module
