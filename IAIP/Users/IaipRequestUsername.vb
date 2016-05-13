Imports System.Text
Imports System.Collections.Generic

Public Class IaipRequestUsername

    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Private Sub IaipRequestUsername_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub

    Private Sub Request_Click(sender As Object, e As EventArgs) Handles DoRequestUsername.Click
        EP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If Not Me.ValidateChildren Then
            DialogResult = DialogResult.None
            DisplayInvalidMessage()
        Else
            If Not RequestUsername() Then
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Function RequestUsername() As Boolean
        Dim result As DAL.UsernameReminderResponse = DAL.SendUsernameReminder(EmailAddress.Text)
        Select Case result
            Case DAL.UsernameReminderResponse.Success
                Return True
            Case DAL.UsernameReminderResponse.EmailNotExist
                Dim errorMsg As String = "No account exists with that email address."
                EP.SetError(EmailAddress, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.Warning)
                Message.Display(MessageDisplay)
            Case DAL.UsernameReminderResponse.InvalidInput
                Dim errorMsg As String = "That email address is not valid."
                EP.SetError(EmailAddress, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.Warning)
                Message.Display(MessageDisplay)
            Case DAL.UsernameReminderResponse.UnknownError
                Message = New IaipMessage("An unknown error occurred. Please contact support for help.", IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
        End Select
        Return False
    End Function

    Private Sub DisplayInvalidMessage()
        Dim sb As New StringBuilder()
        sb.AppendLine("Please correct the following errors:")

        For Each c As Control In InvalidEntries
            sb.AppendLine("• " & EP.GetError(c))
        Next

        Message = New IaipMessage(sb.ToString, IaipMessage.WarningLevels.ErrorReport)
        Message.Display(MessageDisplay)
    End Sub

#Region " Field validation "

    Private Sub EmailAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles EmailAddress.Validating
        If String.IsNullOrEmpty(EmailAddress.Text) Then
            EP.SetError(EmailAddress, "Enter your email address.")
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
            e.Cancel = True
        ElseIf Not IsValidEmailAddress(EmailAddress.Text) Then
            EP.SetError(EmailAddress, "Enter a valid email address.")
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
            e.Cancel = True
        Else
            EP.SetError(EmailAddress, String.Empty)
        End If
    End Sub

#End Region

End Class