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
                toParam = String.Join(",", recipientsTo)
            End If

            If recipientsCC IsNot Nothing Then
                If Not recipientsCC.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If
                ccParam = "cc=" & String.Join(",", recipientsCC)
            End If

            If recipientsBCC IsNot Nothing Then
                If Not recipientsBCC.AreValidEmailAddresses() Then
                    Return CreateEmailResult.InvalidEmail
                End If
                bccParam = "bcc=" & String.Join(",", recipientsBCC)
            End If

            Dim uriQueryString As String = ConcatNonEmptyStrings("&", {subjectParam, bodyParam, ccParam, bccParam})

            Dim emailUriString As String
            If String.IsNullOrEmpty(uriQueryString) Then
                emailUriString = $"mailto:{toParam}"
            Else
                emailUriString = $"mailto:{toParam}?{uriQueryString}"
            End If

            Dim result As Boolean = False

            If emailUriString.Length < 5000 Then
                ' The OpenUri method is preferable, but is limited by URI length, which can be exceeded
                ' if a lot of recipients are added
                result = OpenUri(New Uri(emailUriString), isMailto:=True)
            Else
                ' Failover is to create an Outlook Email
                result = CreateOutlookEmail(subject, body, recipientsTo, recipientsCC, recipientsBCC)
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

End Module
