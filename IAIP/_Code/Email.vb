Public Module Email

    Public Function CreateEmail(Optional subject As String = Nothing,
                         Optional body As String = Nothing,
                         Optional recipientsTo As String() = Nothing,
                         Optional recipientsCC As String() = Nothing,
                         Optional recipientsBCC As String() = Nothing) As Boolean

        monitor.TrackFeature("Email.SendUrlEmail")

        Try
            Dim subjectParam As String = Nothing
            Dim bodyParam As String = Nothing
            Dim toParam As String = "test@example.com"
            Dim ccParam As String = Nothing
            Dim bccParam As String = Nothing

            If subject IsNot Nothing Then subjectParam = "subject=" & Uri.EscapeDataString(subject)
            If body IsNot Nothing Then bodyParam = "body=" & Uri.EscapeDataString(body)
            If recipientsTo IsNot Nothing Then toParam = String.Join(";", recipientsTo)
            If recipientsCC IsNot Nothing Then ccParam = "cc=" & Uri.EscapeDataString(String.Join(";", recipientsCC))
            If recipientsBCC IsNot Nothing Then bccParam = "bcc=" & Uri.EscapeDataString(String.Join(";", recipientsBCC))

            Dim uriQueryParams As String() = {subjectParam, bodyParam, ccParam, bccParam}
            Dim uriQueryString As String = ConcatNonEmptyStrings("&", uriQueryParams)

            Dim emailUriString As String = String.Format("mailto:{0}?{1}", toParam, uriQueryString)

            Dim result As Boolean = False

            If emailUriString.Length < 5000 Then
                ' The OpenUri method is preferable, but is limited by URI length, which can be exceeded
                ' if a lot of recipients are added
                result = OpenUri(New Uri(emailUriString))
            Else
                ' Failover is to create an Outlook Email
                result = CreateOutlookEmail(subject, body, recipientsTo, recipientsCC, recipientsBCC)
            End If

            Return result
        Catch ex As Exception
            ErrorReport(ex, Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Function

End Module
