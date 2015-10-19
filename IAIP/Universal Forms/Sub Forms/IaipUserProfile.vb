Imports System.Text
Imports System.Collections.Generic

Public Class IaipUserProfile

    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Private Sub IaipUserProfile_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
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
        ProfileEP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If Me.ValidateChildren() Then
            If Not SaveProfileData() Then DialogResult = System.Windows.Forms.DialogResult.None
        Else
            DialogResult = System.Windows.Forms.DialogResult.None
            DisplayInvalidMessage()
        End If
    End Sub

    Private Function SaveProfileData() As Boolean
        Dim updatedStaff As Staff = CurrentUser.Clone

        updatedStaff.FirstName = FirstName.Text
        updatedStaff.LastName = LastName.Text
        updatedStaff.EmailAddress = EmailAddress.Text
        updatedStaff.PhoneNumber = PhoneNumber.Text
        updatedStaff.OfficeNumber = OfficeNumber.Text

        Dim result As Boolean = DAL.UpdateStaffInfo(updatedStaff)

        If result Then
            CurrentUser.FirstName = FirstName.Text
            CurrentUser.LastName = LastName.Text
            CurrentUser.EmailAddress = EmailAddress.Text
            CurrentUser.PhoneNumber = PhoneNumber.Text
            CurrentUser.OfficeNumber = OfficeNumber.Text
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
            sb.AppendLine("• " & ProfileEP.GetError(c))
        Next

        Message = New IaipMessage(sb.ToString, IaipMessage.WarningLevels.ErrorReport)
        Message.Display(MessageDisplay)
    End Sub

#Region " File validation "
    Private Sub FirstName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles FirstName.Validating
        If Not ValidateAsNonEmptyString(FirstName, ProfileEP, "First name") Then
            e.Cancel = True
            If Not InvalidEntries.Contains(FirstName) Then InvalidEntries.Add(FirstName)
        Else
            ProfileEP.SetError(FirstName, String.Empty)
        End If
    End Sub

    Private Sub LastName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LastName.Validating
        If Not ValidateAsNonEmptyString(LastName, ProfileEP, "Last name") Then
            e.Cancel = True
            If Not InvalidEntries.Contains(LastName) Then InvalidEntries.Add(LastName)
        Else
            ProfileEP.SetError(LastName, String.Empty)
        End If
    End Sub

    Private Sub EmailAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles EmailAddress.Validating
        If Not ValidateAsEmailAddress(EmailAddress, ProfileEP, "Valid email address") Then
            e.Cancel = True
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        Else
            ProfileEP.SetError(EmailAddress, String.Empty)
        End If
    End Sub

    Private Sub PhoneNumber_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PhoneNumber.Validating
        If Not ValidateAsPhoneNumber(PhoneNumber, ProfileEP, "Valid telephone number") Then
            e.Cancel = True
            If Not InvalidEntries.Contains(PhoneNumber) Then InvalidEntries.Add(PhoneNumber)
        Else
            ProfileEP.SetError(PhoneNumber, String.Empty)
        End If
    End Sub
#End Region

End Class