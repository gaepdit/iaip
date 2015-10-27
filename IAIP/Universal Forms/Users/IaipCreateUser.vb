Imports System.Text
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Public Class IaipCreateUser

    Friend Property NewUserId As Integer = 0
    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)
    Private Property OrganizationDataSet As DataSet = OrganizationService.OrganizationDataSet

    Private Sub IaipUserProfile_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
        LoadComboBoxData()
    End Sub

#Region " ComboBoxes "
    Private Sub LoadComboBoxData()
        Branch.DataSource = Nothing
        Program.DataSource = Nothing
        Unit.DataSource = Nothing

        Dim view As DataView = New DataView(OrganizationDataSet.Tables("Branches"))
        view.Sort = "STRBRANCHDESC"

        With Branch
            .DisplayMember = "STRBRANCHDESC"
            .ValueMember = "NUMBRANCHCODE"
            .DataSource = view
            .SelectedValue = 0
        End With
    End Sub

    Private Sub Branch_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Branch.SelectionChangeCommitted
        Program.DataSource = Nothing
        Unit.DataSource = Nothing

        If Branch.SelectedValue > 0 Then
            Dim view As DataView = New DataView(OrganizationDataSet.Tables("Programs"))
            view.RowFilter = "NUMBRANCHCODE = " & Branch.SelectedValue & " OR NUMPROGRAMCODE = 0 "
            view.Sort = "STRPROGRAMDESC"

            With Program
                .DisplayMember = "STRPROGRAMDESC"
                .ValueMember = "NUMPROGRAMCODE"
                .DataSource = view
                .SelectedValue = 0
            End With
        End If
    End Sub

    Private Sub Program_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Program.SelectionChangeCommitted
        Unit.DataSource = Nothing

        If Program.SelectedValue > 0 Then
            Dim view As DataView = New DataView(OrganizationDataSet.Tables("Units"))
            view.RowFilter = "NUMPROGRAMCODE = " & Program.SelectedValue & " OR NUMUNITCODE = 0 "
            view.Sort = "STRUNITDESC"

            With Unit
                .DisplayMember = "STRUNITDESC"
                .ValueMember = "NUMUNITCODE"
                .DataSource = view
                .SelectedValue = 0
            End With
        End If
    End Sub

#End Region

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        EP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        If Me.ValidateChildren() Then
            If Not SaveNewUser() Then DialogResult = System.Windows.Forms.DialogResult.None
        Else
            DialogResult = System.Windows.Forms.DialogResult.None
            DisplayInvalidMessage()
        End If
    End Sub

    Private Function SaveNewUser() As Boolean
        Dim tempPassword As String = CreateTempPassword()
        Dim result As Boolean = False

        result = DAL.CreateNewUser(Username.Text, tempPassword, LastName.Text.Trim, _
                                   FirstName.Text.Trim, EmailAddress.Text.Trim, PhoneNumber.Text, _
                                   Branch.SelectedValue, Program.SelectedValue, Unit.SelectedValue, _
                                   OfficeNumber.Text.Trim, True, NewUserId)

        If result Then
            Message = New IaipMessage("User successfully created.", _
                          IaipMessage.WarningLevels.Success)
            Message.Display(MessageDisplay)

            ConfirmNewAccount(tempPassword)
            Return True
        Else
            Message = New IaipMessage("An unknown error occurred. User not created.", _
                                      IaipMessage.WarningLevels.ErrorReport)
            Message.Display(MessageDisplay)
            Return False
        End If
    End Function

    Private Function CreateTempPassword() As String
        Dim tp As String = RandomHelper.RandomReadableString(4) & "-" & RandomHelper.RandomReadableString(4)
        If Regex.IsMatch(tp, AtLeastOneDigitPattern) _
            AndAlso Regex.IsMatch(tp, AtLeastOneLetterPattern) Then
            Return tp
        Else
            Return CreateTempPassword()
        End If
    End Function

    Private Function ConfirmNewAccount(tempPassword As String) As Boolean
        If Not SendConfirmationEmail(Username.Text, tempPassword) Then
            If CurrentServerEnvironment = DB.ServerEnvironment.PRD Then
                MessageBox.Show(String.Format("A new user account was created with the following login details. Please write them down now." & vbNewLine & vbNewLine &
                                              "Username: {0}" & vbNewLine & "Temporary password: {1}", Username.Text, tempPassword) &
                                         vbNewLine & vbNewLine &
                                          "Please note, the password is case sensitive. The first time you log in, you will be asked to change your password and verify your profile." &
                                          vbNewLine & vbNewLine &
                                          "Please set permissions on the next screen, then restart the IAIP and create an identical user in the testing environment.", _
                                          "Success")
            Else
                MessageBox.Show(String.Format("A new user account was created with the following login details. Please write them down now." & vbNewLine & vbNewLine &
                                             "Username: {0}" & vbNewLine & "Temporary password: {1}", Username.Text, tempPassword) &
                                         vbNewLine & vbNewLine &
                                         "Please note, the password is case sensitive. The first time you log in, you will be asked to change your password and verify your profile." &
                                         vbNewLine & vbNewLine &
                                         "Please set permissions on the next screen.", _
                                         "Success")
            End If
        Else
            If CurrentServerEnvironment = DB.ServerEnvironment.PRD Then
                MessageBox.Show(String.Format("User account {0} created. An email with login details has been sent to {1}.", _
                                              Username.Text, EmailAddress.Text) &
                                          vbNewLine & vbNewLine &
                                          "Please set permissions on the next screen, then restart the IAIP and create an identical user in the testing environment.", _
                                          "Success")
            Else
                MessageBox.Show(String.Format("User account {0} created. An email with login details has been sent to {1}.", _
                                              Username.Text, EmailAddress.Text) &
                                          vbNewLine & vbNewLine &
                                          "Please set permissions on the next screen.", _
                                          "Success")
            End If
        End If
    End Function

    Private Function SendConfirmationEmail(username As String, tempPassword As String) As Boolean
        ' TODO: Sending email from the IAIP is goofy; it just opens a new email in Outlook 
        ' and hopes you'll send it. This should all be handled on the server side,
        ' which hopefully will be possible one day. Maybe the account creation process 
        ' will be an Oracle procedure that sends or schedules a confirmation email. Or 
        ' maybe account creation will be implemented as a web app, which will handle all this.

        Return False

        'Dim subject As String = "Welcome new IAIP user!"
        'Dim body As String = String.Format(My.Resources.EmailNewUserWelcome, _
        '                                   username, tempPassword)

        'If Not CreateEmail(subject, body, {EmailAddress.Text}) Then Return False
        'Return True
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

    Private Sub UserName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Username.Validating
        Dim usernameIsValid As Boolean = False

        If DAL.UsernameExists(Username.Text) Then
            EP.SetError(Username, "Username is already in use; choose another")
        Else
            Select Case IsValidUserName(Username.Text)
                Case UserNameValidationResult.Valid
                    EP.SetError(Username, String.Empty)
                    usernameIsValid = True
                Case UserNameValidationResult.Empty
                    EP.SetError(Username, "Username is required")
                Case UserNameValidationResult.TooShort
                    EP.SetError(Username, "Username must be at least " & MinUsernameLength.ToString & " characters long")
                Case UserNameValidationResult.InvalidCharacters
                    EP.SetError(Username, "Username must only contain alphanumeric characters")
            End Select
        End If

        If Not usernameIsValid Then
            e.Cancel = True
            If Not InvalidEntries.Contains(Username) Then InvalidEntries.Add(Username)
        End If
    End Sub

    Private Sub FirstName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles FirstName.Validating
        If IsValidNonEmptyString(FirstName.Text) Then
            EP.SetError(FirstName, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(FirstName, "First name is required")
            If Not InvalidEntries.Contains(FirstName) Then InvalidEntries.Add(FirstName)
        End If
    End Sub

    Private Sub LastName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LastName.Validating
        If IsValidNonEmptyString(LastName.Text) Then
            EP.SetError(LastName, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(LastName, "Last name is required")
            If Not InvalidEntries.Contains(LastName) Then InvalidEntries.Add(LastName)
        End If
    End Sub

    Private Sub EmailAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles EmailAddress.Validating
        If DAL.EmailIsInUse(EmailAddress.Text.Trim) Then
            EP.SetError(EmailAddress, "Email address is already in use")
            e.Cancel = True
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        ElseIf IsValidEmailAddress(EmailAddress.Text.Trim, True) Then
            EP.SetError(EmailAddress, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(EmailAddress, "Valid DNR email address is required (remember to use @dnr.ga.gov, not @dnr.state.ga.us)")
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        End If
    End Sub

#End Region

End Class