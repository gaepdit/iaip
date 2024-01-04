Imports System.Text
Imports System.Collections.Generic
Imports System.Threading.Tasks

Public Class IaipRequestPasswordReset
    Private Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Private Async Sub DoResetPassword_Click(sender As Object, e As EventArgs) Handles DoResetPassword.Click
        EP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If ValidateChildren Then
            If Await ResetPasswordAsync() Then DialogResult = DialogResult.OK
        Else
            DisplayInvalidMessage()
        End If
    End Sub

    Private Async Function ResetPasswordAsync() As Task(Of Boolean)
        Dim result As DAL.RequestPasswordResetResponse = Await DAL.RequestPasswordResetAsync(Username.Text)
        Select Case result
            Case DAL.RequestPasswordResetResponse.Success
                Return True
            Case DAL.RequestPasswordResetResponse.UnknownError
                Message = New IaipMessage("An unknown error occurred. Please contact support for help.", IaipMessage.WarningLevels.ErrorReport)
                Message.Display(MessageDisplay)
            Case DAL.RequestPasswordResetResponse.InvalidUsername
                Dim errorMsg As String = "No account exists with that username."
                EP.SetError(Username, errorMsg)
                Message = New IaipMessage(errorMsg, IaipMessage.WarningLevels.Warning)
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

    Private Sub Username_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Username.Validating
        If String.IsNullOrEmpty(Username.Text) Then
            EP.SetError(Username, "Enter your username.")
            If Not InvalidEntries.Contains(Username) Then InvalidEntries.Add(Username)
            e.Cancel = True
        Else
            EP.SetError(Username, String.Empty)
        End If
    End Sub

#End Region

End Class
