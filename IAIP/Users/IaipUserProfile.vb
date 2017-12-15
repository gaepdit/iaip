Imports System.Text
Imports System.Collections.Generic

Public Class IaipUserProfile

    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Private Sub IaipUserProfile_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CurrentUser Is Nothing Then
            MessageBox.Show("Something has gone awry.", "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        DisplayCurrentUserProfile()
    End Sub

    Private Sub DisplayCurrentUserProfile()
        FirstName.Text = CurrentUser.FirstName
        LastName.Text = CurrentUser.LastName
        EmailAddress.Text = CurrentUser.EmailAddress
        PhoneNumber.Text = CurrentUser.PhoneNumber
        OfficeNumber.Text = CurrentUser.OfficeNumber
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        EP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If Me.ValidateChildren() Then
            If Not SaveProfileData() Then DialogResult = DialogResult.None
        Else
            DialogResult = DialogResult.None
            DisplayInvalidMessage()
        End If
    End Sub

    Private Function SaveProfileData() As Boolean
        Dim updatedStaff As IaipUser = CurrentUser.Clone

        updatedStaff.FirstName = FirstName.Text.Trim
        updatedStaff.LastName = LastName.Text.Trim
        updatedStaff.EmailAddress = EmailAddress.Text.Trim
        updatedStaff.PhoneNumber = PhoneNumber.Text
        updatedStaff.OfficeNumber = OfficeNumber.Text.Trim

        Dim result As Boolean = DAL.UpdateUserProfile(updatedStaff)

        If result Then
            CurrentUser.FirstName = FirstName.Text.Trim
            CurrentUser.LastName = LastName.Text.Trim
            CurrentUser.EmailAddress = EmailAddress.Text.Trim
            CurrentUser.PhoneNumber = PhoneNumber.Text
            CurrentUser.OfficeNumber = OfficeNumber.Text.Trim
            Return True
        Else
            Message = New IaipMessage("An unknown error occurred. Profile not updated.", _
                                      IaipMessage.WarningLevels.ErrorReport)
            Message.Display(MessageDisplay)
            Return False
        End If
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

    Private Sub FirstName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles FirstName.Validating
        If IsValidNonEmptyString(FirstName.Text) Then
            EP.SetError(FirstName, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(FirstName, "First name is required.")
            If Not InvalidEntries.Contains(FirstName) Then InvalidEntries.Add(FirstName)
        End If
    End Sub

    Private Sub LastName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LastName.Validating
        If IsValidNonEmptyString(LastName.Text) Then
            EP.SetError(LastName, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(LastName, "Last name is required.")
            If Not InvalidEntries.Contains(LastName) Then InvalidEntries.Add(LastName)
        End If
    End Sub

    Private Sub PhoneNumber_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PhoneNumber.Validating
        If PhoneNumber.MaskCompleted Then
            EP.SetError(PhoneNumber, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(PhoneNumber, "Phone number is required.")
            If Not InvalidEntries.Contains(PhoneNumber) Then InvalidEntries.Add(PhoneNumber)
        End If
    End Sub

    Private Sub EmailAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles EmailAddress.Validating
        If DAL.EmailIsInUse(EmailAddress.Text.Trim, CurrentUser.UserID) Then
            EP.SetError(EmailAddress, "Email address is already in use by another user.")
            e.Cancel = True
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        ElseIf IsValidEmailAddress(EmailAddress.Text.Trim, True) Then
            EP.SetError(EmailAddress, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(EmailAddress, "A valid DNR email address is required.")
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        End If
    End Sub

#End Region

End Class