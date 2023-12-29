Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions

Module IaipValidation

    Public Function IsValidNonEmptyString(s As String, Optional minLength As Integer = 0) As Boolean
        If String.IsNullOrWhiteSpace(s.Trim) Then Return False
        If minLength > 0 AndAlso s.Trim.Length < minLength Then Return False
        Return True
    End Function

    <CodeAnalysis.SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification:="Use of unused local variable is intentional.")>
    <Extension>
    Public Function IsValidEmailAddress(emailAddress As String, Optional requireDnrAddress As Boolean = False) As Boolean
        If String.IsNullOrEmpty(emailAddress) Then Return False
        If requireDnrAddress AndAlso Not Regex.IsMatch(emailAddress, DnrEmailPattern) Then Return False

        Try
            Dim testEmail As New Net.Mail.MailAddress(emailAddress)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    <Extension>
    Public Function AreValidEmailAddresses(emailAddresses As String(), Optional requireDnrAddress As Boolean = False) As Boolean
        If emailAddresses Is Nothing Then Return False

        For Each emailAddress As String In emailAddresses
            If Not emailAddress.IsValidEmailAddress(requireDnrAddress) Then
                Return False
            End If
        Next

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
    ''' <param name="isMailto">Optional flag to indicate whether URL is a mailto. Defaults to False.</param>
    ''' <returns>True if the URL is valid; otherwise false.</returns>
    Public Function IsValidURL(url As String, Optional isMailto As Boolean = False) As Boolean
        Dim validatedUri As Uri = Nothing

        If Uri.TryCreate(url, UriKind.Absolute, validatedUri) Then
            If isMailto Then
                Return (validatedUri.Scheme = Uri.UriSchemeMailto)
            End If
            Return (validatedUri.Scheme = Uri.UriSchemeHttp OrElse validatedUri.Scheme = Uri.UriSchemeHttps)
        End If

        Return False
    End Function

End Module
