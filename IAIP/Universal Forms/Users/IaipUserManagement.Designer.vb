<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IaipUserManagement
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.CreateNewUserButton = New System.Windows.Forms.Button()
        Me.SaveProfileChanges = New System.Windows.Forms.Button()
        Me.ProfileStatusInactive = New System.Windows.Forms.RadioButton()
        Me.ProfileStatusActive = New System.Windows.Forms.RadioButton()
        Me.ProfileOfficeNumber = New System.Windows.Forms.TextBox()
        Me.ProfilePhoneNumber = New System.Windows.Forms.MaskedTextBox()
        Me.ProfileUnit = New System.Windows.Forms.ComboBox()
        Me.ProfileProgram = New System.Windows.Forms.ComboBox()
        Me.ProfileBranch = New System.Windows.Forms.ComboBox()
        Me.ProfileEmailAddress = New System.Windows.Forms.TextBox()
        Me.ProfileLastName = New System.Windows.Forms.TextBox()
        Me.ProfileFirstName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RolesProgram = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RolesBranch = New System.Windows.Forms.ComboBox()
        Me.ProfileUsername = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Search = New System.Windows.Forms.Button()
        Me.SearchUnit = New System.Windows.Forms.ComboBox()
        Me.SearchProgram = New System.Windows.Forms.ComboBox()
        Me.SearchBranch = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.SearchLastName = New System.Windows.Forms.TextBox()
        Me.SearchFirstName = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.SearchResults = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ProfilePanel = New System.Windows.Forms.Panel()
        Me.ProfileStatusSelection = New System.Windows.Forms.Panel()
        Me.RequiredLabel = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RolesPanel = New System.Windows.Forms.Panel()
        Me.AddRolesGroupbox = New System.Windows.Forms.GroupBox()
        Me.AvailableRoles = New System.Windows.Forms.ListBox()
        Me.AddNewRoles = New System.Windows.Forms.Button()
        Me.CurrentRolesGroupBox = New System.Windows.Forms.GroupBox()
        Me.CurrentRoles = New System.Windows.Forms.ListBox()
        Me.RemoveRoles = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SearchPanel = New System.Windows.Forms.Panel()
        Me.SearchIncludeInactive = New System.Windows.Forms.CheckBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MessageDisplay = New System.Windows.Forms.Label()
        CType(Me.SearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProfilePanel.SuspendLayout()
        Me.ProfileStatusSelection.SuspendLayout()
        Me.RolesPanel.SuspendLayout()
        Me.AddRolesGroupbox.SuspendLayout()
        Me.CurrentRolesGroupBox.SuspendLayout()
        Me.SearchPanel.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(13, 16)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(83, 13)
        Me.lblUserName.TabIndex = 7
        Me.lblUserName.Text = "IAIP username *"
        '
        'CreateNewUserButton
        '
        Me.CreateNewUserButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateNewUserButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CreateNewUserButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateNewUserButton.Location = New System.Drawing.Point(615, 11)
        Me.CreateNewUserButton.Name = "CreateNewUserButton"
        Me.CreateNewUserButton.Size = New System.Drawing.Size(153, 27)
        Me.CreateNewUserButton.TabIndex = 0
        Me.CreateNewUserButton.Text = "→ Create New User"
        Me.CreateNewUserButton.UseVisualStyleBackColor = True
        '
        'SaveProfileChanges
        '
        Me.SaveProfileChanges.Enabled = False
        Me.SaveProfileChanges.Location = New System.Drawing.Point(133, 285)
        Me.SaveProfileChanges.Name = "SaveProfileChanges"
        Me.SaveProfileChanges.Size = New System.Drawing.Size(117, 23)
        Me.SaveProfileChanges.TabIndex = 10
        Me.SaveProfileChanges.Text = "Save profile changes"
        Me.SaveProfileChanges.UseVisualStyleBackColor = True
        '
        'ProfileStatusInactive
        '
        Me.ProfileStatusInactive.AutoSize = True
        Me.ProfileStatusInactive.Location = New System.Drawing.Point(61, 9)
        Me.ProfileStatusInactive.Name = "ProfileStatusInactive"
        Me.ProfileStatusInactive.Size = New System.Drawing.Size(63, 17)
        Me.ProfileStatusInactive.TabIndex = 1
        Me.ProfileStatusInactive.Text = "Inactive"
        Me.ProfileStatusInactive.UseVisualStyleBackColor = True
        '
        'ProfileStatusActive
        '
        Me.ProfileStatusActive.AutoSize = True
        Me.ProfileStatusActive.Location = New System.Drawing.Point(0, 9)
        Me.ProfileStatusActive.Name = "ProfileStatusActive"
        Me.ProfileStatusActive.Size = New System.Drawing.Size(55, 17)
        Me.ProfileStatusActive.TabIndex = 0
        Me.ProfileStatusActive.Text = "Active"
        Me.ProfileStatusActive.UseVisualStyleBackColor = True
        '
        'ProfileOfficeNumber
        '
        Me.ProfileOfficeNumber.Location = New System.Drawing.Point(110, 143)
        Me.ProfileOfficeNumber.Name = "ProfileOfficeNumber"
        Me.ProfileOfficeNumber.Size = New System.Drawing.Size(96, 20)
        Me.ProfileOfficeNumber.TabIndex = 5
        '
        'ProfilePhoneNumber
        '
        Me.ProfilePhoneNumber.Location = New System.Drawing.Point(110, 117)
        Me.ProfilePhoneNumber.Mask = "(000) 000-0000"
        Me.ProfilePhoneNumber.Name = "ProfilePhoneNumber"
        Me.ProfilePhoneNumber.Size = New System.Drawing.Size(96, 20)
        Me.ProfilePhoneNumber.TabIndex = 4
        Me.ProfilePhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'ProfileUnit
        '
        Me.ProfileUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ProfileUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ProfileUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProfileUnit.FormattingEnabled = True
        Me.ProfileUnit.Location = New System.Drawing.Point(110, 248)
        Me.ProfileUnit.Name = "ProfileUnit"
        Me.ProfileUnit.Size = New System.Drawing.Size(140, 21)
        Me.ProfileUnit.TabIndex = 9
        '
        'ProfileProgram
        '
        Me.ProfileProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ProfileProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ProfileProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProfileProgram.FormattingEnabled = True
        Me.ProfileProgram.Location = New System.Drawing.Point(110, 222)
        Me.ProfileProgram.Name = "ProfileProgram"
        Me.ProfileProgram.Size = New System.Drawing.Size(140, 21)
        Me.ProfileProgram.TabIndex = 8
        '
        'ProfileBranch
        '
        Me.ProfileBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ProfileBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ProfileBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProfileBranch.FormattingEnabled = True
        Me.ProfileBranch.Location = New System.Drawing.Point(110, 196)
        Me.ProfileBranch.Name = "ProfileBranch"
        Me.ProfileBranch.Size = New System.Drawing.Size(140, 21)
        Me.ProfileBranch.TabIndex = 7
        '
        'ProfileEmailAddress
        '
        Me.ProfileEmailAddress.Location = New System.Drawing.Point(110, 91)
        Me.ProfileEmailAddress.Name = "ProfileEmailAddress"
        Me.ProfileEmailAddress.Size = New System.Drawing.Size(140, 20)
        Me.ProfileEmailAddress.TabIndex = 3
        '
        'ProfileLastName
        '
        Me.ProfileLastName.Location = New System.Drawing.Point(110, 39)
        Me.ProfileLastName.Name = "ProfileLastName"
        Me.ProfileLastName.Size = New System.Drawing.Size(140, 20)
        Me.ProfileLastName.TabIndex = 1
        '
        'ProfileFirstName
        '
        Me.ProfileFirstName.Location = New System.Drawing.Point(110, 65)
        Me.ProfileFirstName.Name = "ProfileFirstName"
        Me.ProfileFirstName.Size = New System.Drawing.Size(140, 20)
        Me.ProfileFirstName.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 146)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 13)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Office No."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(13, 173)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 13)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Employee status *"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(13, 199)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Branch"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 225)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Program"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 251)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Unit"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 94)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Email address *"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 120)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Phone"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Last name *"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "First name *"
        '
        'RolesProgram
        '
        Me.RolesProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.RolesProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RolesProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RolesProgram.FormattingEnabled = True
        Me.RolesProgram.Location = New System.Drawing.Point(67, 49)
        Me.RolesProgram.Name = "RolesProgram"
        Me.RolesProgram.Size = New System.Drawing.Size(127, 21)
        Me.RolesProgram.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Program"
        '
        'RolesBranch
        '
        Me.RolesBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.RolesBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RolesBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RolesBranch.FormattingEnabled = True
        Me.RolesBranch.Location = New System.Drawing.Point(67, 23)
        Me.RolesBranch.Name = "RolesBranch"
        Me.RolesBranch.Size = New System.Drawing.Size(127, 21)
        Me.RolesBranch.TabIndex = 0
        '
        'ProfileUsername
        '
        Me.ProfileUsername.Location = New System.Drawing.Point(110, 13)
        Me.ProfileUsername.Name = "ProfileUsername"
        Me.ProfileUsername.Size = New System.Drawing.Size(96, 20)
        Me.ProfileUsername.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(15, 26)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(41, 13)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Branch"
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search.Location = New System.Drawing.Point(110, 195)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(140, 27)
        Me.Search.TabIndex = 6
        Me.Search.Text = "Search"
        Me.Search.UseVisualStyleBackColor = True
        '
        'SearchUnit
        '
        Me.SearchUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SearchUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SearchUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SearchUnit.FormattingEnabled = True
        Me.SearchUnit.Location = New System.Drawing.Point(110, 145)
        Me.SearchUnit.Name = "SearchUnit"
        Me.SearchUnit.Size = New System.Drawing.Size(140, 21)
        Me.SearchUnit.TabIndex = 4
        '
        'SearchProgram
        '
        Me.SearchProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SearchProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SearchProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SearchProgram.FormattingEnabled = True
        Me.SearchProgram.Location = New System.Drawing.Point(110, 118)
        Me.SearchProgram.Name = "SearchProgram"
        Me.SearchProgram.Size = New System.Drawing.Size(140, 21)
        Me.SearchProgram.TabIndex = 3
        '
        'SearchBranch
        '
        Me.SearchBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SearchBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SearchBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SearchBranch.FormattingEnabled = True
        Me.SearchBranch.Location = New System.Drawing.Point(110, 91)
        Me.SearchBranch.Name = "SearchBranch"
        Me.SearchBranch.Size = New System.Drawing.Size(140, 21)
        Me.SearchBranch.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(13, 94)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(41, 13)
        Me.Label25.TabIndex = 24
        Me.Label25.Text = "Branch"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(13, 121)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(46, 13)
        Me.Label26.TabIndex = 23
        Me.Label26.Text = "Program"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(13, 148)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(26, 13)
        Me.Label27.TabIndex = 22
        Me.Label27.Text = "Unit"
        '
        'SearchLastName
        '
        Me.SearchLastName.Location = New System.Drawing.Point(110, 39)
        Me.SearchLastName.Name = "SearchLastName"
        Me.SearchLastName.Size = New System.Drawing.Size(140, 20)
        Me.SearchLastName.TabIndex = 0
        '
        'SearchFirstName
        '
        Me.SearchFirstName.Location = New System.Drawing.Point(110, 65)
        Me.SearchFirstName.Name = "SearchFirstName"
        Me.SearchFirstName.Size = New System.Drawing.Size(140, 20)
        Me.SearchFirstName.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 68)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 13)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "First Name"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(13, 42)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(58, 13)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Last Name"
        '
        'SearchResults
        '
        Me.SearchResults.AllowUserToAddRows = False
        Me.SearchResults.AllowUserToDeleteRows = False
        Me.SearchResults.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SearchResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.SearchResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SearchResults.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.SearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SearchResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.SearchResults.Location = New System.Drawing.Point(317, 47)
        Me.SearchResults.Name = "SearchResults"
        Me.SearchResults.ReadOnly = True
        Me.SearchResults.RowHeadersVisible = False
        Me.SearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SearchResults.Size = New System.Drawing.Size(451, 183)
        Me.SearchResults.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "User Search"
        '
        'ProfilePanel
        '
        Me.ProfilePanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ProfilePanel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ProfilePanel.CausesValidation = False
        Me.ProfilePanel.Controls.Add(Me.ProfileStatusSelection)
        Me.ProfilePanel.Controls.Add(Me.RequiredLabel)
        Me.ProfilePanel.Controls.Add(Me.ProfileLastName)
        Me.ProfilePanel.Controls.Add(Me.Label7)
        Me.ProfilePanel.Controls.Add(Me.SaveProfileChanges)
        Me.ProfilePanel.Controls.Add(Me.ProfileUnit)
        Me.ProfilePanel.Controls.Add(Me.ProfileProgram)
        Me.ProfilePanel.Controls.Add(Me.ProfileOfficeNumber)
        Me.ProfilePanel.Controls.Add(Me.ProfileBranch)
        Me.ProfilePanel.Controls.Add(Me.lblUserName)
        Me.ProfilePanel.Controls.Add(Me.Label15)
        Me.ProfilePanel.Controls.Add(Me.ProfilePhoneNumber)
        Me.ProfilePanel.Controls.Add(Me.Label14)
        Me.ProfilePanel.Controls.Add(Me.ProfileUsername)
        Me.ProfilePanel.Controls.Add(Me.Label13)
        Me.ProfilePanel.Controls.Add(Me.ProfileFirstName)
        Me.ProfilePanel.Controls.Add(Me.Label12)
        Me.ProfilePanel.Controls.Add(Me.Label3)
        Me.ProfilePanel.Controls.Add(Me.ProfileEmailAddress)
        Me.ProfilePanel.Controls.Add(Me.Label9)
        Me.ProfilePanel.Controls.Add(Me.Label10)
        Me.ProfilePanel.Controls.Add(Me.Label16)
        Me.ProfilePanel.Enabled = False
        Me.ProfilePanel.Location = New System.Drawing.Point(19, 267)
        Me.ProfilePanel.Name = "ProfilePanel"
        Me.ProfilePanel.Size = New System.Drawing.Size(273, 331)
        Me.ProfilePanel.TabIndex = 3
        '
        'ProfileStatusSelection
        '
        Me.ProfileStatusSelection.Controls.Add(Me.ProfileStatusInactive)
        Me.ProfileStatusSelection.Controls.Add(Me.ProfileStatusActive)
        Me.ProfileStatusSelection.Location = New System.Drawing.Point(110, 162)
        Me.ProfileStatusSelection.Name = "ProfileStatusSelection"
        Me.ProfileStatusSelection.Size = New System.Drawing.Size(138, 32)
        Me.ProfileStatusSelection.TabIndex = 6
        '
        'RequiredLabel
        '
        Me.RequiredLabel.AutoSize = True
        Me.RequiredLabel.Location = New System.Drawing.Point(13, 290)
        Me.RequiredLabel.Name = "RequiredLabel"
        Me.RequiredLabel.Size = New System.Drawing.Size(58, 13)
        Me.RequiredLabel.TabIndex = 26
        Me.RequiredLabel.Text = "(* required)"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 247)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Edit Profile"
        '
        'RolesPanel
        '
        Me.RolesPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RolesPanel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.RolesPanel.CausesValidation = False
        Me.RolesPanel.Controls.Add(Me.AddRolesGroupbox)
        Me.RolesPanel.Controls.Add(Me.CurrentRolesGroupBox)
        Me.RolesPanel.Enabled = False
        Me.RolesPanel.Location = New System.Drawing.Point(317, 267)
        Me.RolesPanel.Name = "RolesPanel"
        Me.RolesPanel.Size = New System.Drawing.Size(447, 331)
        Me.RolesPanel.TabIndex = 4
        '
        'AddRolesGroupbox
        '
        Me.AddRolesGroupbox.Controls.Add(Me.AvailableRoles)
        Me.AddRolesGroupbox.Controls.Add(Me.RolesBranch)
        Me.AddRolesGroupbox.Controls.Add(Me.AddNewRoles)
        Me.AddRolesGroupbox.Controls.Add(Me.Label1)
        Me.AddRolesGroupbox.Controls.Add(Me.RolesProgram)
        Me.AddRolesGroupbox.Controls.Add(Me.Label21)
        Me.AddRolesGroupbox.Location = New System.Drawing.Point(15, 16)
        Me.AddRolesGroupbox.Name = "AddRolesGroupbox"
        Me.AddRolesGroupbox.Size = New System.Drawing.Size(202, 302)
        Me.AddRolesGroupbox.TabIndex = 0
        Me.AddRolesGroupbox.TabStop = False
        Me.AddRolesGroupbox.Text = "Add Roles"
        '
        'AvailableRoles
        '
        Me.AvailableRoles.Location = New System.Drawing.Point(18, 75)
        Me.AvailableRoles.Name = "AvailableRoles"
        Me.AvailableRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.AvailableRoles.Size = New System.Drawing.Size(176, 186)
        Me.AvailableRoles.Sorted = True
        Me.AvailableRoles.TabIndex = 2
        '
        'AddNewRoles
        '
        Me.AddNewRoles.Enabled = False
        Me.AddNewRoles.Location = New System.Drawing.Point(71, 269)
        Me.AddNewRoles.Name = "AddNewRoles"
        Me.AddNewRoles.Size = New System.Drawing.Size(123, 23)
        Me.AddNewRoles.TabIndex = 3
        Me.AddNewRoles.Text = "Add selected roles"
        Me.AddNewRoles.UseVisualStyleBackColor = True
        '
        'CurrentRolesGroupBox
        '
        Me.CurrentRolesGroupBox.Controls.Add(Me.CurrentRoles)
        Me.CurrentRolesGroupBox.Controls.Add(Me.RemoveRoles)
        Me.CurrentRolesGroupBox.Location = New System.Drawing.Point(227, 16)
        Me.CurrentRolesGroupBox.Name = "CurrentRolesGroupBox"
        Me.CurrentRolesGroupBox.Size = New System.Drawing.Size(202, 302)
        Me.CurrentRolesGroupBox.TabIndex = 1
        Me.CurrentRolesGroupBox.TabStop = False
        Me.CurrentRolesGroupBox.Text = "Current Roles"
        '
        'CurrentRoles
        '
        Me.CurrentRoles.Location = New System.Drawing.Point(12, 23)
        Me.CurrentRoles.Name = "CurrentRoles"
        Me.CurrentRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.CurrentRoles.Size = New System.Drawing.Size(176, 238)
        Me.CurrentRoles.Sorted = True
        Me.CurrentRoles.TabIndex = 0
        '
        'RemoveRoles
        '
        Me.RemoveRoles.Enabled = False
        Me.RemoveRoles.Location = New System.Drawing.Point(63, 269)
        Me.RemoveRoles.Name = "RemoveRoles"
        Me.RemoveRoles.Size = New System.Drawing.Size(125, 23)
        Me.RemoveRoles.TabIndex = 1
        Me.RemoveRoles.Text = "Remove selected roles"
        Me.RemoveRoles.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(329, 247)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Edit Roles"
        '
        'SearchPanel
        '
        Me.SearchPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SearchPanel.Controls.Add(Me.SearchIncludeInactive)
        Me.SearchPanel.Controls.Add(Me.SearchUnit)
        Me.SearchPanel.Controls.Add(Me.SearchProgram)
        Me.SearchPanel.Controls.Add(Me.SearchBranch)
        Me.SearchPanel.Controls.Add(Me.Label25)
        Me.SearchPanel.Controls.Add(Me.Label26)
        Me.SearchPanel.Controls.Add(Me.Search)
        Me.SearchPanel.Controls.Add(Me.Label27)
        Me.SearchPanel.Controls.Add(Me.Label2)
        Me.SearchPanel.Controls.Add(Me.SearchLastName)
        Me.SearchPanel.Controls.Add(Me.SearchFirstName)
        Me.SearchPanel.Controls.Add(Me.Label24)
        Me.SearchPanel.Controls.Add(Me.Label23)
        Me.SearchPanel.Location = New System.Drawing.Point(19, 8)
        Me.SearchPanel.Name = "SearchPanel"
        Me.SearchPanel.Size = New System.Drawing.Size(273, 236)
        Me.SearchPanel.TabIndex = 1
        '
        'SearchIncludeInactive
        '
        Me.SearchIncludeInactive.AutoSize = True
        Me.SearchIncludeInactive.Location = New System.Drawing.Point(110, 172)
        Me.SearchIncludeInactive.Name = "SearchIncludeInactive"
        Me.SearchIncludeInactive.Size = New System.Drawing.Size(129, 17)
        Me.SearchIncludeInactive.TabIndex = 5
        Me.SearchIncludeInactive.Text = "Include inactive users"
        Me.SearchIncludeInactive.UseVisualStyleBackColor = True
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'MessageDisplay
        '
        Me.MessageDisplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MessageDisplay.AutoSize = True
        Me.MessageDisplay.Location = New System.Drawing.Point(32, 610)
        Me.MessageDisplay.MaximumSize = New System.Drawing.Size(270, 0)
        Me.MessageDisplay.MinimumSize = New System.Drawing.Size(270, 0)
        Me.MessageDisplay.Name = "MessageDisplay"
        Me.MessageDisplay.Padding = New System.Windows.Forms.Padding(5)
        Me.MessageDisplay.Size = New System.Drawing.Size(270, 75)
        Me.MessageDisplay.TabIndex = 5
        Me.MessageDisplay.Text = "Message Label" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.MessageDisplay.Visible = False
        '
        'IaipUserManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(785, 693)
        Me.Controls.Add(Me.MessageDisplay)
        Me.Controls.Add(Me.SearchPanel)
        Me.Controls.Add(Me.RolesPanel)
        Me.Controls.Add(Me.ProfilePanel)
        Me.Controls.Add(Me.CreateNewUserButton)
        Me.Controls.Add(Me.SearchResults)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.MinimumSize = New System.Drawing.Size(801, 731)
        Me.Name = "IaipUserManagement"
        Me.Text = "IAIP User Management"
        CType(Me.SearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProfilePanel.ResumeLayout(False)
        Me.ProfilePanel.PerformLayout()
        Me.ProfileStatusSelection.ResumeLayout(False)
        Me.ProfileStatusSelection.PerformLayout()
        Me.RolesPanel.ResumeLayout(False)
        Me.AddRolesGroupbox.ResumeLayout(False)
        Me.AddRolesGroupbox.PerformLayout()
        Me.CurrentRolesGroupBox.ResumeLayout(False)
        Me.SearchPanel.ResumeLayout(False)
        Me.SearchPanel.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents ProfileEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents ProfileLastName As System.Windows.Forms.TextBox
    Friend WithEvents ProfileFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProfileStatusInactive As System.Windows.Forms.RadioButton
    Friend WithEvents ProfileStatusActive As System.Windows.Forms.RadioButton
    Friend WithEvents ProfileOfficeNumber As System.Windows.Forms.TextBox
    Friend WithEvents ProfilePhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ProfileUnit As System.Windows.Forms.ComboBox
    Friend WithEvents ProfileProgram As System.Windows.Forms.ComboBox
    Friend WithEvents ProfileBranch As System.Windows.Forms.ComboBox
    Friend WithEvents RolesBranch As System.Windows.Forms.ComboBox
    Friend WithEvents ProfileUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents SearchUnit As System.Windows.Forms.ComboBox
    Friend WithEvents SearchProgram As System.Windows.Forms.ComboBox
    Friend WithEvents SearchBranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents SearchLastName As System.Windows.Forms.TextBox
    Friend WithEvents SearchFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents SearchResults As System.Windows.Forms.DataGridView
    Friend WithEvents CreateNewUserButton As System.Windows.Forms.Button
    Friend WithEvents SaveProfileChanges As System.Windows.Forms.Button
    Friend WithEvents RolesProgram As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ProfilePanel As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents RolesPanel As Panel
    Friend WithEvents AddNewRoles As Button
    Friend WithEvents AddRolesGroupbox As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents AvailableRoles As ListBox
    Friend WithEvents SearchPanel As Panel
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents MessageDisplay As Label
    Friend WithEvents RequiredLabel As Label
    Friend WithEvents SearchIncludeInactive As CheckBox
    Friend WithEvents CurrentRoles As ListBox
    Friend WithEvents CurrentRolesGroupBox As GroupBox
    Friend WithEvents RemoveRoles As Button
    Friend WithEvents ProfileStatusSelection As Panel
End Class
