Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Runtime.InteropServices

Module OutlookEmail

    Private Function IsOutlookRunning() As Boolean
        IsOutlookRunning = False
        Try
            Dim out As Object = GetObject(, "Outlook.Application")
            If out IsNot Nothing Then IsOutlookRunning = True
        Catch ex As Exception
        End Try
    End Function

    Public Function CreateOutlookEmail( _
                Optional subject As String = Nothing, _
                Optional body As String = Nothing, _
                Optional recipientsTo As String() = Nothing, _
                Optional recipientsCC As String() = Nothing, _
                Optional recipientsBCC As String() = Nothing _
                ) As Boolean
        monitor.TrackFeature("Email.SendOutlookEmail")

        If Not IsOutlookRunning() Then
            MsgBox("Microsoft Outlook must be running first. Please start Outlook and try again.", MsgBoxStyle.OkOnly, "Error")
            Return False
        End If

        Dim outlookApp As New Outlook.Application
        Dim mail As Outlook.MailItem = Nothing

        Try
            mail = outlookApp.CreateItem(Outlook.OlItemType.olMailItem)
            mail.Subject = subject
            mail.Body = body

            If recipientsTo IsNot Nothing Then AddRecipients(mail, recipientsTo, Outlook.OlMailRecipientType.olTo)
            If recipientsCC IsNot Nothing Then AddRecipients(mail, recipientsCC, Outlook.OlMailRecipientType.olCC)
            If recipientsBCC IsNot Nothing Then AddRecipients(mail, recipientsBCC, Outlook.OlMailRecipientType.olBCC)

            mail.Save()
            mail.Display()

            Return True
        Catch ex As Exception
            ErrorReport(ex, System.Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        Finally
            If outlookApp IsNot Nothing Then Marshal.ReleaseComObject(outlookApp)
            If mail IsNot Nothing Then Marshal.ReleaseComObject(mail)
            mail = Nothing
        End Try
    End Function

    Private Sub AddRecipients(ByRef mail As Outlook.MailItem, recipientArray As String(), toType As Outlook.OlMailRecipientType)
        Dim recipients As Outlook.Recipients = Nothing
        Dim recipient As Outlook.Recipient = Nothing

        Try
            recipients = mail.Recipients
            recipient = recipients.Add(String.Join(";", recipientArray))
            recipient.Type = toType

        Catch ex As Exception
            ErrorReport(ex, System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Not IsNothing(recipient) Then Marshal.ReleaseComObject(recipient)
            If Not IsNothing(recipients) Then Marshal.ReleaseComObject(recipients)
        End Try
    End Sub

End Module
