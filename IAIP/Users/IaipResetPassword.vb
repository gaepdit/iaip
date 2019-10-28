Imports System.Text
Imports System.Collections.Generic

Public Class IaipResetPassword

    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Private Sub IaipResetPassword_Load(sender As Object, e As EventArgs) Handles Me.Load
        Message = New IaipMessage("Check your email for password reset information. Please allow up to 15 minutes for delivery.", IaipMessage.WarningLevels.Info)
        Message.Display(MessageDisplay)
    End Sub

    Private Sub IaipResetPassword_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Username.Text <> "" Then Token.Focus()
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        EP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If Me.ValidateChildren Then
            If Not SavePassword() Then DialogResult = DialogResult.None
        Else
            DialogResult = DialogResult.None
            DisplayInvalidMessage()
        End If
    End Sub

    Private Function SavePassword() As Boolean
        Dim result As DAL.ResetPasswordResponse = DAL.ResetUserPassword(Username.Text, NewPassword.Text, Token.Text)

        Select Case result
            Case DAL.ResetPasswordResponse.Success
                Return True
            Case DAL.ResetPasswordResponse.InvalidNewPassword
                Dim errorMsg As String = "The new password is not valid. Password not changed."
                EP.SetError(NewPassword, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
            Case DAL.ResetPasswordResponse.InvalidToken
                Dim errorMsg As String = "The password reset token is not valid. Password not changed."
                EP.SetError(Token, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
            Case DAL.ResetPasswordResponse.InvalidUsername
                Dim errorMsg As String = "No account exists with that username."
                EP.SetError(Username, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.Warning)
                Message.Display(MessageDisplay)
            Case DAL.ResetPasswordResponse.MaxAttemptsExceeded
                Dim errorMsg As String = "Maximum number of attempts exceeded."
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.Warning)
                Message.Display(MessageDisplay)
                DisableForm()
            Case DAL.ResetPasswordResponse.UnknownError
                Message = New IaipMessage("An unknown error occurred. Password not changed.", IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
        End Select

        Return False
    End Function

    Private Sub DisableForm()
        Username.Enabled = False
        Token.Enabled = False
        NewPassword.Enabled = False
        ConfirmPassword.Enabled = False
        Save.Enabled = False
    End Sub

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

    Private Sub Username_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Username.Validating
        If String.IsNullOrEmpty(Username.Text) Then
            EP.SetError(Username, "Enter your username.")
            If Not InvalidEntries.Contains(Username) Then InvalidEntries.Add(Username)
            e.Cancel = True
        Else
            EP.SetError(Username, String.Empty)
        End If
    End Sub

    Private Sub Token_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Token.Validating
        If String.IsNullOrEmpty(Token.Text) Then
            EP.SetError(Token, "Enter reset token from email.")
            If Not InvalidEntries.Contains(Token) Then InvalidEntries.Add(Token)
            e.Cancel = True
        Else
            EP.SetError(Token, String.Empty)
        End If
    End Sub

    Private Sub NewPasswordTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NewPassword.Validating
        If String.IsNullOrEmpty(NewPassword.Text) Then
            EP.SetError(NewPassword, "Enter new password.")
            If Not InvalidEntries.Contains(NewPassword) Then InvalidEntries.Add(NewPassword)
            e.Cancel = True
        ElseIf NewPassword.Text = Token.Text Then
            EP.SetError(NewPassword, "New password must differ from token.")
            If Not InvalidEntries.Contains(NewPassword) Then InvalidEntries.Add(NewPassword)
            e.Cancel = True
        Else
            EP.SetError(NewPassword, String.Empty)
        End If
    End Sub

    Private Sub ConfirmPasswordTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ConfirmPassword.Validating
        If String.IsNullOrEmpty(ConfirmPassword.Text) Then
            EP.SetError(ConfirmPassword, "Repeat new password to confirm")
            If Not InvalidEntries.Contains(ConfirmPassword) Then InvalidEntries.Add(ConfirmPassword)
            e.Cancel = True
        ElseIf ConfirmPassword.Text <> NewPassword.Text Then
            EP.SetError(ConfirmPassword, "New password fields must match")
            If Not InvalidEntries.Contains(ConfirmPassword) Then InvalidEntries.Add(ConfirmPassword)
            e.Cancel = True
        Else
            EP.SetError(ConfirmPassword, String.Empty)
        End If
    End Sub

#End Region

End Class
