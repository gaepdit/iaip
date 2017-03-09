Imports System.Text.RegularExpressions

Module IaipValidation

    Public Function IsValidNonEmptyString(s As String, Optional minLength As Integer = 0) As Boolean
        If String.IsNullOrWhiteSpace(s.Trim) Then Return False
        If minLength > 0 AndAlso s.Trim.Length < minLength Then Return False
        Return True
    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId:="testEmail")>
    Public Function IsValidEmailAddress(emailAddress As String, Optional requireDnrAddress As Boolean = False) As Boolean
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

    Public Function IsValidUsername(username As String, Optional minLength As Integer = MIN_USERNAME_LENGTH) As StringValidationResult
        If String.IsNullOrEmpty(username) Then Return StringValidationResult.Empty
        If Not Regex.IsMatch(username, AlphaNumericPattern) Then Return StringValidationResult.InvalidCharacters
        If username.Length < minLength Then Return StringValidationResult.TooShort
        Return StringValidationResult.Valid
    End Function

    Public Function IsValidPassword(password As String, Optional minLength As Integer = MIN_PASSWORD_LENGTH) As StringValidationResult
        If String.IsNullOrEmpty(password) Then Return StringValidationResult.Empty
        If password.Length < minLength Then Return StringValidationResult.TooShort
        Return StringValidationResult.Valid
    End Function

    'Public Function IsValidPhoneNumber(phoneNumber As String) As Boolean
    '    If String.IsNullOrEmpty(phoneNumber) Then Return False
    '    Return Regex.IsMatch(phoneNumber, MatchPhoneNumberPattern)
    'End Function

    ''' <summary>
    ''' Validates whether a string is a valid URL.
    ''' </summary>
    ''' <param name="url">A string to validate.</param>
    ''' <returns>True if the URL is valid; otherwise false.</returns>
    Public Function IsValidURL(url As String) As Boolean
        Dim validatedUri As Uri = Nothing

        If Uri.TryCreate(url, UriKind.Absolute, validatedUri) Then
            Return (validatedUri.Scheme = Uri.UriSchemeHttp Or validatedUri.Scheme = Uri.UriSchemeHttps)
        End If

        Return False
    End Function

End Module
