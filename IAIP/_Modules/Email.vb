Module Email

    Public Sub CreateEmail(Optional ByVal subject As String = Nothing, _
                         Optional ByVal body As String = Nothing, _
                         Optional ByVal recipientsTo As String() = Nothing, _
                         Optional ByVal recipientsCC As String() = Nothing, _
                         Optional ByVal recipientsBCC As String() = Nothing, _
                         Optional ByVal objectSender As Object = Nothing)

        monitor.TrackFeature("Email.SendUrlEmail")

        If objectSender IsNot Nothing Then
            objectSender.Cursor = Cursors.AppStarting
        End If

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

            Dim queryParams As String() = {subjectParam, bodyParam, ccParam, bccParam}
            Dim queryString As String = ConcatNonEmptyStrings("&", queryParams)

            Dim emailUriString As String = String.Format("mailto:{0}?{1}", toParam, queryString)

            Dim result As Boolean = False

            If emailUriString.Length < 5000 Then
                ' The OpenUri method is preferable, but is limited by URI length, which can be exceeded
                ' if a lot of recipients are added
                result = OpenUri(New Uri(emailUriString))
            Else
                ' Failover is to create an Outlook Email
                result = CreateOutlookEmail(subject, body, recipientsTo, recipientsCC, recipientsBCC)
            End If

            If Not result Then
                MsgBox("There was an error sending the message. Please try again.", MsgBoxStyle.OkOnly, "Error")
            End If
        Catch ex As Exception
            ErrorReport(ex, System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If objectSender IsNot Nothing Then
                objectSender.Cursor = Nothing
            End If
        End Try

    End Sub

End Module
