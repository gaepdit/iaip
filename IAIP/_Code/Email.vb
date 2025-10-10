Imports System.IO
Imports System.Text
Imports Iaip.UrlHelpers

Public Module Email

    Public Enum CreateEmailResult
        Success
        Failure
        InvalidEmail
        FunctionError
    End Enum

    Public Function CreateEmail(Optional subject As String = Nothing,
                         Optional body As String = Nothing,
                         Optional recipientsTo As String() = Nothing,
                         Optional recipientsCC As String() = Nothing,
                         Optional recipientsBCC As String() = Nothing) As CreateEmailResult
        Dim emailUriString As String = ""

        Try
            Dim subjectParam As String = Nothing
            Dim bodyParam As String = Nothing
            Dim toParam As String = ApbContactEmail
            Dim cc As String = Nothing
            Dim bcc As String = Nothing
            Dim ccParam As String = Nothing
            Dim bccParam As String = Nothing

            If subject IsNot Nothing Then subjectParam = "subject=" & Uri.EscapeDataString(subject)
            If body IsNot Nothing Then bodyParam = "body=" & Uri.EscapeDataString(body)

            If recipientsTo IsNot Nothing Then
                If Not recipientsTo.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If

                toParam = TrimArray(recipientsTo).ConcatNonEmptyStrings(",")
            End If

            If recipientsCC IsNot Nothing Then
                If Not recipientsCC.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If

                cc = TrimArray(recipientsCC).ConcatNonEmptyStrings(",")
                ccParam = "cc=" & cc
            End If

            If recipientsBCC IsNot Nothing Then
                If Not recipientsBCC.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If

                bcc = TrimArray(recipientsBCC).ConcatNonEmptyStrings(",")
                bccParam = "bcc=" & bcc
            End If

            Dim uriQueryString As String = {subjectParam, bodyParam, ccParam, bccParam}.ConcatNonEmptyStrings("&")

            If String.IsNullOrEmpty(uriQueryString) Then
                emailUriString = $"mailto:{toParam}"
            Else
                emailUriString = $"mailto:{toParam}?{uriQueryString}"
            End If

            Dim result As Boolean = False


            If emailUriString.Length < 2084 Then
                ' This method is preferable, but is limited by URI length, which can be exceeded if a lot of recipients are added.
                ' Ref: https://support.microsoft.com/en-us/help/208427/maximum-url-length-is-2-083-characters-in-internet-explorer
                result = OpenUriString(emailUriString, isMailto:=True)
            Else
                ' Failover is to create a text file with instructions to user.
                OpenEmailAsTextFile(subject, body, toParam, cc, bcc)
                result = True
            End If

            If result Then
                Return CreateEmailResult.Success
            Else
                Return CreateEmailResult.Failure
            End If

        Catch ex As Exception
            ErrorReport(ex, $"emailUriString: {emailUriString}", Reflection.MethodBase.GetCurrentMethod.Name)
            Return CreateEmailResult.FunctionError
        End Try
    End Function

    Private Sub OpenEmailAsTextFile(subject As String, body As String, toParam As String, ccParam As String, bccParam As String)
        Dim sb As New StringBuilder()

        sb.AppendLine("Could not create email. Please copy and paste the text below into a new email.")
        sb.AppendLine()

        sb.AppendLine("To:")
        sb.AppendLine(toParam)
        sb.AppendLine()

        If Not String.IsNullOrEmpty(ccParam) Then
            sb.AppendLine("CC:")
            sb.AppendLine(ccParam)
            sb.AppendLine()
        End If

        If Not String.IsNullOrEmpty(bccParam) Then
            sb.AppendLine("BCC:")
            sb.AppendLine(bccParam)
            sb.AppendLine()
        End If

        If subject IsNot Nothing Then
            sb.AppendLine("Subject:")
            sb.AppendLine(subject)
            sb.AppendLine()
        End If

        sb.AppendLine("Message:")
        sb.AppendLine()

        If body IsNot Nothing Then sb.Append(body)

        Dim filePath As String = Path.GetTempPath() & "TempEmail.txt"

        File.WriteAllText(filePath, sb.ToString())
        Process.Start(filePath)
    End Sub
End Module
