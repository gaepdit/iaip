Imports System.Collections.Generic
Imports System.Text

Public Class IaipUserManagement

#Region " Properties "

    Private Property SelectedUserID As Integer = 0
    Private Property SelectedUserRoles As IaipRoles

    Private Property Message As New IaipMessage
    Private Property InvalidEntries As New List(Of Control)

    Dim organizationDataSet As DataSet = OrganizationService.OrganizationDataSet
    Dim iaipAccountRoles As DataTable = SharedData.GetTable(SharedData.Tables.IaipAccountRoles)

#End Region

#Region " Page Load "

    Private Sub IAIPUserAdminTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)

        LoadComboBoxData()
        SetFormPermissions()
        SearchLastName.Focus()
    End Sub

    Private Sub SetFormPermissions()
        If CurrentUser.CheckIf(UserCan.EditUsers) Then
            SaveProfileChanges.Enabled = True
            RemoveRoles.Enabled = True
            AddNewRoles.Enabled = True
        End If

        If CurrentUser.CheckIf(UserCan.CreateUsers) Then
            CreateNewUserButton.Enabled = True
        End If



        If Not AccountFormAccess(8, 4) = "1" Then
            RolesBranch.Enabled = False
            ProfileBranch.Enabled = False

            If Not AccountFormAccess(8, 3) = "1" Then
                RolesProgram.Enabled = False
                ProfileProgram.Enabled = False
                If Not AccountFormAccess(8, 2) = "1" Then
                    RolesProgram.Enabled = False
                    ProfileProgram.Enabled = False
                    ProfileUnit.Enabled = False
                    ProfileStatusActive.Enabled = False
                    ProfileStatusInactive.Enabled = False
                    CreateNewUserButton.Visible = False
                    ProfileFirstName.ReadOnly = True
                    ProfileLastName.ReadOnly = True
                    ProfileEmailAddress.ReadOnly = True
                    ProfilePhoneNumber.ReadOnly = True
                    SaveProfileChanges.Enabled = False
                    ProfileUsername.ReadOnly = True
                End If
            End If
        End If
    End Sub

#End Region

#Region " Load and set up ComboBoxes "

    Private Sub LoadComboBoxData()
        ProfileBranch.DataSource = Nothing
        ProfileProgram.DataSource = Nothing
        ProfileUnit.DataSource = Nothing

        For Each cb As ComboBox In {ProfileUnit, SearchUnit}
            With cb
                .DataSource = New DataView(organizationDataSet.Tables("Units"))
                CType(.DataSource, DataView).Sort = "Description"
                CType(.DataSource, DataView).RowFilter = "ProgramCode = 0"
                .DisplayMember = "Description"
                .ValueMember = "UnitCode"
                .SelectedValue = 0
            End With
        Next

        For Each cb As ComboBox In {ProfileProgram, SearchProgram, RolesProgram}
            With cb
                .DataSource = New DataView(organizationDataSet.Tables("Programs"))
                CType(.DataSource, DataView).Sort = "Description"
                CType(.DataSource, DataView).RowFilter = "BranchCode = 0"
                .DisplayMember = "Description"
                .ValueMember = "ProgramCode"
                .SelectedValue = 0
            End With
        Next

        AddHandler SearchProgram.SelectedIndexChanged, AddressOf SearchProgram_SelectedIndexChanged
        AddHandler ProfileProgram.SelectedIndexChanged, AddressOf ProfileProgram_SelectedIndexChanged
        AddHandler RolesProgram.SelectedIndexChanged, AddressOf RoleProgram_SelectedIndexChanged

        For Each cb As ComboBox In {ProfileBranch, SearchBranch, RolesBranch}
            With cb
                .DataSource = New DataView(organizationDataSet.Tables("Branches"))
                CType(.DataSource, DataView).Sort = "Description"
                .DisplayMember = "Description"
                .ValueMember = "BranchCode"
                .SelectedValue = 0
            End With
        Next

        AddHandler SearchBranch.SelectedIndexChanged, AddressOf SearchBranch_SelectedIndexChanged
        AddHandler ProfileBranch.SelectedIndexChanged, AddressOf ProfileBranch_SelectedIndexChanged
        AddHandler RolesBranch.SelectedIndexChanged, AddressOf RoleBranch_SelectedIndexChanged

        SearchBranch.SelectedValue = CurrentUser.BranchID
    End Sub

    Private Sub SetComboBoxFilter(cbo As ComboBox, rowFilter As String)
        Dim view As DataView = CType(cbo.DataSource, DataView)
        view.RowFilter = rowFilter
        cbo.SelectedValue = 0
    End Sub

    Private Sub BranchCboSelectionChanged(branchCbo As ComboBox, programCbo As ComboBox)
        If branchCbo.SelectedValue > 0 Then
            SetComboBoxFilter(programCbo, "BranchCode = " & branchCbo.SelectedValue.ToString & " OR ProgramCode = 0 ")
        Else
            SetComboBoxFilter(programCbo, "BranchCode = 0")
        End If
    End Sub
    Private Sub ProgramCboSelectionChanged(programCbo As ComboBox, unitCbo As ComboBox)
        If programCbo.SelectedValue > 0 Then
            SetComboBoxFilter(unitCbo, "ProgramCode = " & programCbo.SelectedValue & " OR UnitCode = 0 ")
        Else
            SetComboBoxFilter(unitCbo, "ProgramCode = 0")
        End If
    End Sub

    Private Sub SearchBranch_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles SearchBranch.SelectedIndexChanged
        BranchCboSelectionChanged(SearchBranch, SearchProgram)
    End Sub
    Private Sub ProfileBranch_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles ProfileBranch.SelectedIndexChanged
        BranchCboSelectionChanged(ProfileBranch, ProfileProgram)
    End Sub
    Private Sub RoleBranch_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles RoleBranch.SelectedIndexChanged
        BranchCboSelectionChanged(RolesBranch, RolesProgram)
    End Sub
    Private Sub SearchProgram_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles SearchProgram.SelectedIndexChanged
        ProgramCboSelectionChanged(SearchProgram, SearchUnit)
    End Sub
    Private Sub ProfileProgram_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles ProfileProgram.SelectedIndexChanged
        ProgramCboSelectionChanged(ProfileProgram, ProfileUnit)
    End Sub
    Private Sub RoleProgram_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles RoleProgram.SelectedIndexChanged
        DisplayAvailableRoles()
    End Sub

#End Region

#Region " Accept button "

    Private Sub SearchPanel_Enter(sender As Object, e As EventArgs) Handles SearchPanel.Enter
        Me.AcceptButton = Search
    End Sub

    Private Sub SearchPanel_Leave(sender As Object, e As EventArgs) Handles SearchPanel.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub EditPanel_Enter(sender As Object, e As EventArgs) Handles ProfilePanel.Enter
        Me.AcceptButton = SaveProfileChanges
    End Sub

    Private Sub EditPanel_Leave(sender As Object, e As EventArgs) Handles ProfilePanel.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

#Region " Search tool and results "

    Private Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click
        RunSearch()
    End Sub

    Private Sub RunSearch(Optional userid As Integer = 0)
        Message.Clear()
        ProfilePanel.Enabled = False
        RolesPanel.Enabled = False

        Dim dt As DataTable
        If userid = 0 Then
            dt = DAL.SearchUsers(SearchLastName.Text, SearchFirstName.Text, SearchBranch.SelectedValue, SearchProgram.SelectedValue, SearchUnit.SelectedValue, SearchIncludeInactive.Checked)
        Else
            dt = DAL.GetIaipUserByUserIdAsDataTable(userid)
        End If
        With SearchResults
            .DataSource = dt
            .Columns("UserID").Visible = False
            .Columns("BranchID").Visible = False
            .Columns("ProgramID").Visible = False
            .Columns("UnitID").Visible = False
            .Columns("Office").Visible = False
            .Columns("ActiveEmployee").Visible = False
            .Columns("RolesString").Visible = False
            .Columns("RequirePasswordChange").Visible = False
            .Columns("RequestProfileUpdate").Visible = False
            .Columns("Employee status").Visible = SearchIncludeInactive.Checked
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub SearchResults_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles SearchResults.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < SearchResults.RowCount Then
            LoadUserProfile(SearchResults.Rows(e.RowIndex))
        End If
    End Sub

    Private Sub LoadUserProfile(dgv As DataGridViewRow)
        Message.Clear()

        SelectedUserID = dgv.Cells("UserID").Value.ToString
        SelectedUserRoles = dgv.Cells("RolesString").Value.ToString

        ProfileUsername.Text = dgv.Cells("Username").Value.ToString
        ProfileLastName.Text = dgv.Cells("Last name").Value.ToString
        ProfileFirstName.Text = dgv.Cells("First name").Value.ToString
        ProfileEmailAddress.Text = dgv.Cells("Email address").Value.ToString
        ProfilePhoneNumber.Text = dgv.Cells("Phone number").Value.ToString
        ProfileOfficeNumber.Text = dgv.Cells("Office").Value.ToString
        If dgv.Cells("BranchID").Value.ToString = "" Then
            ProfileBranch.SelectedValue = 0
            RolesBranch.SelectedValue = 0
        Else
            ProfileBranch.SelectedValue = dgv.Cells("BranchID").Value.ToString()
            RolesBranch.SelectedValue = dgv.Cells("BranchID").Value.ToString()
        End If
        If dgv.Cells("ProgramID").Value.ToString = "" Then
            ProfileProgram.SelectedValue = 0
            RolesProgram.SelectedValue = 0
        Else
            ProfileProgram.SelectedValue = dgv.Cells("ProgramID").Value.ToString()
            RolesProgram.SelectedValue = dgv.Cells("ProgramID").Value.ToString()
        End If
        If dgv.Cells("UnitID").Value.ToString = "" Then
            ProfileUnit.SelectedValue = 0
        Else
            ProfileUnit.SelectedValue = dgv.Cells("UnitID").Value.ToString()
        End If
        ProfileStatusActive.Checked = (dgv.Cells("ActiveEmployee").Value.ToString = "1")
        ProfileStatusInactive.Checked = Not (dgv.Cells("ActiveEmployee").Value.ToString = "1")

        DisplayCurrentRoles(dgv.Cells("RolesString").Value.ToString)

        ProfilePanel.Enabled = True
        RolesPanel.Enabled = True
    End Sub

#End Region

#Region " Create new user "

    Private Sub CreateNewUser()
        Using newUserForm As New IaipCreateUser
            If newUserForm.ShowDialog() = DialogResult.OK Then
                RunSearch(newUserForm.NewUserId)
                If SearchResults.RowCount > 0 Then
                    SearchResults.Rows(0).Selected = True
                End If
            End If
        End Using
    End Sub

    Private Sub CreateNewUserButton_Click(sender As Object, e As EventArgs) Handles CreateNewUserButton.Click
        CreateNewUser()
    End Sub

#End Region

#Region " Profile editor "

    Private Sub SaveProfileChanges_Click(sender As Object, e As EventArgs) Handles SaveProfileChanges.Click
        Message.Clear()

        If SelectedUserID = 0 Then
            MessageBox.Show("Select a user from the search tool to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If AccountFormAccess(8, 2) = "0" And AccountFormAccess(8, 3) = "0" And AccountFormAccess(8, 4) = "0" Then
                If SelectedUserID = CurrentUser.UserID Then
                    UpdateUserProfile()
                Else
                    MessageBox.Show("You may only edit your own data.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                If AccountFormAccess(8, 4) = "0" Then
                    If AccountFormAccess(8, 3) = "0" Then
                        If CurrentUser.BranchID = ProfileBranch.SelectedValue And CurrentUser.ProgramID = ProfileProgram.SelectedValue Then
                            UpdateUserProfile()
                        Else
                            MessageBox.Show("You may only edit users in your Program.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Else
                        If CurrentUser.BranchID = ProfileBranch.SelectedValue Then
                            UpdateUserProfile()
                        Else
                            MessageBox.Show("You may only edit users in your Branch.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    End If
                Else
                    UpdateUserProfile()
                End If
            End If
        End If
    End Sub

    Private Sub UpdateUserProfile()
        EP.Clear()
        InvalidEntries.Clear()
        Message.Clear()

        ProfilePanel.CausesValidation = True

        If ValidateChildren() Then
            SaveUserProfileData()
        Else
            DisplayInvalidMessage()
        End If

        ProfilePanel.CausesValidation = False
    End Sub


    Private Sub SaveUserProfileData()
        Dim user As New IaipUser()
        With user
            .UserID = SelectedUserID
            .Username = ProfileUsername.Text
            .LastName = ProfileLastName.Text
            .FirstName = ProfileFirstName.Text
            .EmailAddress = ProfileEmailAddress.Text
            .PhoneNumber = ProfilePhoneNumber.Text
            .BranchID = ProfileBranch.SelectedValue
            .ProgramID = ProfileProgram.SelectedValue
            .UnitId = ProfileUnit.SelectedValue
            .OfficeNumber = ProfileOfficeNumber.Text
            .ActiveEmployee = ProfileStatusActive.Checked
        End With

        If DAL.UpdateUserProfile(user) Then
            Message = New IaipMessage("Profile successfully updated.", IaipMessage.WarningLevels.Success)
            Message.Display(MessageDisplay)
        Else
            Message = New IaipMessage("An unknown error occurred. Profile not updated.", IaipMessage.WarningLevels.ErrorReport)
            Message.Display(MessageDisplay)
        End If
    End Sub


#End Region

#Region " Roles editor "

    Private Sub DisplayAvailableRoles()
        DisplayRoles("BranchID = " & RolesBranch.SelectedValue.ToString & " AND ProgramID = " & RolesProgram.SelectedValue.ToString, AvailableRoles)
    End Sub

    Private Sub DisplayCurrentRoles(roles As IaipRoles)
        DisplayRoles("RoleCode IN (" & String.Join(",", roles.RoleCodes) & ")", CurrentRoles)
    End Sub

    Private Sub DisplayRoles(filter As String, listBox As ListBox)
        Dim dv As New DataView(iaipAccountRoles)
        dv.RowFilter = filter
        With listBox
            .DataSource = dv
            .DisplayMember = "Role"
            .ValueMember = "RoleCode"
            .ClearSelected()
        End With
    End Sub

    Private Sub AddNewRoles_Click(sender As Object, e As EventArgs) Handles AddNewRoles.Click
        For Each item As Object In AvailableRoles.SelectedItems
            SelectedUserRoles.RoleCodes.Add(CType(item, DataRowView).Item("RoleCode"))
        Next
        UpdateRoles()
    End Sub

    Private Sub RemoveRoles_Click(sender As Object, e As EventArgs) Handles RemoveRoles.Click
        For Each item As Object In CurrentRoles.SelectedItems
            SelectedUserRoles.RoleCodes.Remove(CType(item, DataRowView).Item("RoleCode"))
        Next
        UpdateRoles()
    End Sub

    Private Sub UpdateRoles()
        ' TODO: check permissions first!!!
        Message.Clear()
        If DAL.UpdateUserRoles(SelectedUserID, SelectedUserRoles) Then
            DisplayCurrentRoles(SelectedUserRoles)
            Message = New IaipMessage("User roles were successfully updated.", IaipMessage.WarningLevels.Success)
            Message.Display(MessageDisplay)
        Else
            Message = New IaipMessage("There was an error when updating the user roles.", IaipMessage.WarningLevels.ErrorReport)
            Message.Display(MessageDisplay)
        End If
    End Sub

#End Region

#Region " Field validation "

    Private Sub DisplayInvalidMessage()
        Dim sb As New StringBuilder()
        sb.AppendLine("Please correct the following errors:")

        Dim i As Integer = 1
        For Each c As Control In InvalidEntries
            i = i + 1
            If i < 5 Then
                sb.AppendLine("• " & EP.GetError(c))
            ElseIf i = 5 Then
                sb.AppendLine("• ...")
            End If
        Next

        Message = New IaipMessage(sb.ToString, IaipMessage.WarningLevels.ErrorReport)
        Message.Display(MessageDisplay)
    End Sub

    Private Sub ProfileUsername_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ProfileUsername.Validating
        Dim usernameIsValid As Boolean = False

        If DAL.UsernameExists(ProfileUsername.Text, SelectedUserID) Then
            EP.SetError(ProfileUsername, "Username is already in use by another user; choose another.")
        Else
            Select Case IsValidUserName(ProfileUsername.Text)
                Case UserNameValidationResult.Valid
                    EP.SetError(ProfileUsername, String.Empty)
                    usernameIsValid = True
                Case UserNameValidationResult.Empty
                    EP.SetError(ProfileUsername, "Username is required")
                Case UserNameValidationResult.TooShort
                    EP.SetError(ProfileUsername, "Username must be at least " & MinUsernameLength.ToString & " characters long")
                Case UserNameValidationResult.InvalidCharacters
                    EP.SetError(ProfileUsername, "Username may only contain alphanumeric characters.")
            End Select
        End If

        If Not usernameIsValid Then
            e.Cancel = True
            If Not InvalidEntries.Contains(ProfileUsername) Then InvalidEntries.Add(ProfileUsername)
        End If
    End Sub

    Private Sub ProfileFirstName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ProfileFirstName.Validating
        If IsValidNonEmptyString(ProfileFirstName.Text) Then
            EP.SetError(ProfileFirstName, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(ProfileFirstName, "First name is required.")
            If Not InvalidEntries.Contains(ProfileFirstName) Then InvalidEntries.Add(ProfileFirstName)
        End If
    End Sub

    Private Sub ProfileLastName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ProfileLastName.Validating
        If IsValidNonEmptyString(ProfileLastName.Text) Then
            EP.SetError(ProfileLastName, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(ProfileLastName, "Last name is required.")
            If Not InvalidEntries.Contains(ProfileLastName) Then InvalidEntries.Add(ProfileLastName)
        End If
    End Sub

    Private Sub ProfileEmailAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ProfileEmailAddress.Validating
        If DAL.EmailIsInUse(ProfileEmailAddress.Text.Trim, SelectedUserID) Then
            EP.SetError(ProfileEmailAddress, "Email address is already in use.")
            e.Cancel = True
            If Not InvalidEntries.Contains(ProfileEmailAddress) Then InvalidEntries.Add(ProfileEmailAddress)
        ElseIf Not IsValidEmailAddress(ProfileEmailAddress.Text.Trim, True) Then
            e.Cancel = True
            EP.SetError(ProfileEmailAddress, "A valid DNR email address is required.")
            If Not InvalidEntries.Contains(ProfileEmailAddress) Then InvalidEntries.Add(ProfileEmailAddress)
        Else
            EP.SetError(ProfileEmailAddress, String.Empty)
        End If
    End Sub

    Private Sub ProfilePhoneNumber_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ProfilePhoneNumber.Validating
        If ProfilePhoneNumber.MaskCompleted Then
            EP.SetError(ProfilePhoneNumber, String.Empty)
        Else
            e.Cancel = True
            EP.SetError(ProfilePhoneNumber, "Phone number is required.")
            If Not InvalidEntries.Contains(ProfilePhoneNumber) Then InvalidEntries.Add(ProfilePhoneNumber)
        End If
    End Sub

#End Region

End Class