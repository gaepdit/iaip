﻿Imports System.Text
Imports System.Collections.Generic

Public Class IaipChangePassword

    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Private Sub IaipChangePassword_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CurrentUser Is Nothing Then
            MessageBox.Show("Something has gone awry.", "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End If
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        PasswordEP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If ValidateChildren Then
            If SavePassword() Then DialogResult = DialogResult.OK
        Else
            DisplayInvalidMessage()
        End If
    End Sub

    Private Function SavePassword() As Boolean
        Dim result As DAL.PasswordUpdateResponse = DAL.UpdateUserPassword(CurrentUser.Username, NewPassword.Text, CurrentPassword.Text)
        Select Case result
            Case DAL.PasswordUpdateResponse.Success
                Return True
            Case DAL.PasswordUpdateResponse.InvalidUsername
                Const errorMsg As String = "User does not exist. Password not changed."
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
            Case DAL.PasswordUpdateResponse.InvalidLogin
                Const errorMsg As String = "The current password is incorrect. Password not changed."
                PasswordEP.SetError(CurrentPassword, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
            Case DAL.PasswordUpdateResponse.InvalidNewPassword
                Const errorMsg As String = "The new password is not valid. Password not changed."
                PasswordEP.SetError(NewPassword, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
            Case Else
                Message = New IaipMessage("An unknown error occurred. Password not changed.", IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
        End Select
        Return False
    End Function

    Private Sub DisplayInvalidMessage()
        Dim sb As New StringBuilder()
        sb.AppendLine("Please correct the following errors:")

        For Each c As Control In InvalidEntries
            sb.AppendLine("• " & PasswordEP.GetError(c))
        Next

        Message = New IaipMessage(sb.ToString, IaipMessage.WarningLevels.ErrorReport)
        Message.Display(MessageDisplay)
    End Sub

    ' Field validation

    Private Sub CurrentPassword_Validating(sender As Object, e As ComponentModel.CancelEventArgs) Handles CurrentPassword.Validating
        If String.IsNullOrEmpty(CurrentPassword.Text) Then
            PasswordEP.SetError(CurrentPassword, "Enter current password")
            If Not InvalidEntries.Contains(CurrentPassword) Then InvalidEntries.Add(CurrentPassword)
            e.Cancel = True
        Else
            PasswordEP.SetError(CurrentPassword, String.Empty)
        End If
    End Sub

    Private Sub NewPasswordTextBox_Validating(sender As Object, e As ComponentModel.CancelEventArgs) Handles NewPassword.Validating
        Select Case IsValidPassword(NewPassword.Text)
            Case StringValidationResult.Empty
                PasswordEP.SetError(NewPassword, "Enter new password")
                If Not InvalidEntries.Contains(NewPassword) Then InvalidEntries.Add(NewPassword)
                e.Cancel = True
            Case StringValidationResult.TooShort
                PasswordEP.SetError(NewPassword, "New password is too short (minimum three characters)")
                If Not InvalidEntries.Contains(NewPassword) Then InvalidEntries.Add(NewPassword)
                e.Cancel = True
            Case Else
                If NewPassword.Text.ToLower.Contains(CurrentUser.LastName.ToLower) _
                    OrElse NewPassword.Text.ToLower.Contains(CurrentUser.FirstName.ToLower) Then
                    PasswordEP.SetError(NewPassword, "New password cannot contain your name")
                    If Not InvalidEntries.Contains(NewPassword) Then InvalidEntries.Add(NewPassword)
                    e.Cancel = True
                ElseIf NewPassword.Text = CurrentPassword.Text Then
                    PasswordEP.SetError(NewPassword, "New password must differ from previous password")
                    If Not InvalidEntries.Contains(NewPassword) Then InvalidEntries.Add(NewPassword)
                    e.Cancel = True
                Else
                    PasswordEP.SetError(NewPassword, String.Empty)
                End If
        End Select
    End Sub

    Private Sub ConfirmPasswordTextBox_Validating(sender As Object, e As ComponentModel.CancelEventArgs) Handles ConfirmPassword.Validating
        If String.IsNullOrEmpty(ConfirmPassword.Text) Then
            PasswordEP.SetError(ConfirmPassword, "Repeat new password to confirm")
            If Not InvalidEntries.Contains(ConfirmPassword) Then InvalidEntries.Add(ConfirmPassword)
            e.Cancel = True
        ElseIf ConfirmPassword.Text <> NewPassword.Text Then
            PasswordEP.SetError(ConfirmPassword, "New password fields must match")
            If Not InvalidEntries.Contains(ConfirmPassword) Then InvalidEntries.Add(ConfirmPassword)
            e.Cancel = True
        Else
            PasswordEP.SetError(ConfirmPassword, String.Empty)
        End If
    End Sub

End Class
