Imports System.IO
Imports System.Text

Public Module Email

    Public Const ApbContactEmail As String = "GeorgiaAirProtectionBranch@dnr.ga.gov"

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

        Try
            Dim subjectParam As String = Nothing
            Dim bodyParam As String = Nothing
            Dim toParam As String = ApbContactEmail
            Dim ccParam As String = Nothing
            Dim bccParam As String = Nothing

            If subject IsNot Nothing Then subjectParam = "subject=" & Uri.EscapeDataString(subject)
            If body IsNot Nothing Then bodyParam = "body=" & Uri.EscapeDataString(body)

            If recipientsTo IsNot Nothing Then
                If Not recipientsTo.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If
                toParam = ConcatNonEmptyStrings(",", TrimArray(recipientsTo))
            End If

            If recipientsCC IsNot Nothing Then
                If Not recipientsCC.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If
                ccParam = "cc=" & ConcatNonEmptyStrings(",", TrimArray(recipientsCC))
            End If

            If recipientsBCC IsNot Nothing Then
                If Not recipientsBCC.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If
                bccParam = "bcc=" & ConcatNonEmptyStrings(",", TrimArray(recipientsBCC))
            End If

            Dim uriQueryString As String = ConcatNonEmptyStrings("&", {subjectParam, bodyParam, ccParam, bccParam})

            Dim emailUriString As String
            If String.IsNullOrEmpty(uriQueryString) Then
                emailUriString = $"mailto:{toParam}"
            Else
                emailUriString = $"mailto:{toParam}?{uriQueryString}"
            End If

            Dim result As Boolean = False

            If emailUriString.Length < 2083 Then
                ' The OpenUri method is preferable, but is limited by URI length, which can be exceeded
                ' if a lot of recipients are added
                ' Ref: https://support.microsoft.com/en-us/help/208427/maximum-url-length-is-2-083-characters-in-internet-explorer
                result = OpenUri(New Uri(emailUriString), isMailto:=True)
            Else
                ' Failover is to create an Outlook Email
                OpenEmailAsTextFile(subject, body, toParam, ccParam, bccParam)
                result = True
            End If

            If result Then
                Return CreateEmailResult.Success
            Else
                Return CreateEmailResult.Failure
            End If

        Catch ex As Exception
            ErrorReport(ex, Reflection.MethodBase.GetCurrentMethod.Name)
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
