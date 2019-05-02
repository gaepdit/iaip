Imports System.Collections.Generic
Imports System.Text

Public Class IaipCreateUser

#Region " Properties "

    Friend Property NewUserId As Integer = 0
    Friend Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)
    Private Property OrganizationDataSet As DataSet = GetSharedData(SharedDataSet.EpdOrganization)

#End Region

    Private Sub IaipUserProfile_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadComboBoxData()
    End Sub

#Region " ComboBoxes "

    Private Sub LoadComboBoxData()
        Branch.DataSource = Nothing
        Program.DataSource = Nothing
        Unit.DataSource = Nothing

        Dim view As DataView = New DataView(OrganizationDataSet.Tables("Branches"))
        view.Sort = "Description"

        With Branch
            .DisplayMember = "Description"
            .ValueMember = "BranchCode"
            .DataSource = view
            .SelectedValue = 0
        End With
    End Sub

    Private Sub Branch_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Branch.SelectionChangeCommitted
        Program.DataSource = Nothing
        Unit.DataSource = Nothing

        If Branch.SelectedValue > 0 Then
            Dim view As DataView = New DataView(OrganizationDataSet.Tables("Programs"))
            view.RowFilter = "BranchCode = " & Branch.SelectedValue & " OR ProgramCode = 0 "
            view.Sort = "Description"

            With Program
                .DisplayMember = "Description"
                .ValueMember = "ProgramCode"
                .DataSource = view
                .SelectedValue = 0
            End With
        End If
    End Sub

    Private Sub Program_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Program.SelectionChangeCommitted
        Unit.DataSource = Nothing

        If Program.SelectedValue > 0 Then
            Dim view As DataView = New DataView(OrganizationDataSet.Tables("Units"))
            view.RowFilter = "ProgramCode = " & Program.SelectedValue & " OR UnitCode = 0 "
            view.Sort = "Description"

            With Unit
                .DisplayMember = "Description"
                .ValueMember = "UnitCode"
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
            If Not SaveNewUser() Then DialogResult = DialogResult.None
        Else
            DialogResult = DialogResult.None
            DisplayInvalidMessage()
        End If
    End Sub

    Private Function SaveNewUser() As Boolean
        Dim result As Boolean = DAL.CreateNewUser(Username.Text, LastName.Text.Trim,
                                   FirstName.Text.Trim, EmailAddress.Text.Trim, PhoneNumber.Text,
                                   Branch.SelectedValue, Program.SelectedValue, Unit.SelectedValue,
                                   OfficeNumber.Text.Trim, True, NewUserId)

        If result Then
            Dim msg As String = String.Format("User account {0} successfully created. " &
                                              "An email with login details has been sent to {1}. " &
                                              vbNewLine & vbNewLine &
                                              "Please set permissions on the next screen.",
                                              Username.Text, EmailAddress.Text)
            MessageBox.Show(msg, "Success")
            Return True
        Else
            Message = New IaipMessage("An unknown error occurred. User not created.",
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

    Private Sub UserName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Username.Validating
        Dim usernameIsValid As Boolean = False

        If DAL.UsernameExists(Username.Text) Then
            EP.SetError(Username, "Username is already in use; choose another.")
        Else
            Select Case IsValidUsername(Username.Text)
                Case StringValidationResult.Valid
                    EP.SetError(Username, String.Empty)
                    usernameIsValid = True
                Case StringValidationResult.Empty
                    EP.SetError(Username, "Username is required.")
                Case StringValidationResult.TooShort
                    EP.SetError(Username, "Username must be at least " & MIN_USERNAME_LENGTH.ToString & " characters long.")
                Case StringValidationResult.InvalidCharacters
                    EP.SetError(Username, "Username may only contain alphanumeric characters.")
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

    Private Sub EmailAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles EmailAddress.Validating
        If DAL.EmailIsInUse(EmailAddress.Text.Trim) Then
            EP.SetError(EmailAddress, "Email address is already in use.")
            e.Cancel = True
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        ElseIf Not IsValidEmailAddress(EmailAddress.Text.Trim, True) Then
            e.Cancel = True
            EP.SetError(EmailAddress, "A valid DNR email address is required.")
            If Not InvalidEntries.Contains(EmailAddress) Then InvalidEntries.Add(EmailAddress)
        Else
            EP.SetError(EmailAddress, String.Empty)
        End If
    End Sub

#End Region

End Class