<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPUserAdminTool
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblPermissions = New System.Windows.Forms.Label()
        Me.btnCreateNewUser = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.TCUserData = New System.Windows.Forms.TabControl()
        Me.TPUserInformation = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rdbInactiveStatus = New System.Windows.Forms.RadioButton()
        Me.rdbActiveStatus = New System.Windows.Forms.RadioButton()
        Me.txtOfficeNumber = New System.Windows.Forms.TextBox()
        Me.txtPhoneNumber = New System.Windows.Forms.MaskedTextBox()
        Me.cboUnit = New System.Windows.Forms.ComboBox()
        Me.cboProgram = New System.Windows.Forms.ComboBox()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.txtEmailAddress = New System.Windows.Forms.TextBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TPPermission = New System.Windows.Forms.TabPage()
        Me.cboPermissionProgram = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lbl10 = New System.Windows.Forms.Label()
        Me.chb10 = New System.Windows.Forms.CheckBox()
        Me.lbl9 = New System.Windows.Forms.Label()
        Me.chb9 = New System.Windows.Forms.CheckBox()
        Me.lbl7 = New System.Windows.Forms.Label()
        Me.lbl8 = New System.Windows.Forms.Label()
        Me.lbl2 = New System.Windows.Forms.Label()
        Me.lbl3 = New System.Windows.Forms.Label()
        Me.lbl4 = New System.Windows.Forms.Label()
        Me.lbl5 = New System.Windows.Forms.Label()
        Me.lbl6 = New System.Windows.Forms.Label()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.chb1 = New System.Windows.Forms.CheckBox()
        Me.chb2 = New System.Windows.Forms.CheckBox()
        Me.chb8 = New System.Windows.Forms.CheckBox()
        Me.chb3 = New System.Windows.Forms.CheckBox()
        Me.chb7 = New System.Windows.Forms.CheckBox()
        Me.chb4 = New System.Windows.Forms.CheckBox()
        Me.chb6 = New System.Windows.Forms.CheckBox()
        Me.chb5 = New System.Windows.Forms.CheckBox()
        Me.btnClearAllPermissions = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtCurrentPermissions = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cboPermissionBranch = New System.Windows.Forms.ComboBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboSearchUnit = New System.Windows.Forms.ComboBox()
        Me.cboSearchProgram = New System.Windows.Forms.ComboBox()
        Me.cboSearchBranch = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSearchLastName = New System.Windows.Forms.TextBox()
        Me.txtSearchFirstName = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.dgvUserAdminTool = New System.Windows.Forms.DataGridView()
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.lblEmailAddress = New System.Windows.Forms.Label()
        Me.TCUserData.SuspendLayout()
        Me.TPUserInformation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TPPermission.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgvUserAdminTool, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(12, 9)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(63, 13)
        Me.lblUserName.TabIndex = 7
        Me.lblUserName.Text = "User Name:"
        '
        'lblPermissions
        '
        Me.lblPermissions.AutoSize = True
        Me.lblPermissions.Location = New System.Drawing.Point(286, 70)
        Me.lblPermissions.Name = "lblPermissions"
        Me.lblPermissions.Size = New System.Drawing.Size(0, 13)
        Me.lblPermissions.TabIndex = 36
        Me.lblPermissions.Visible = False
        '
        'btnCreateNewUser
        '
        Me.btnCreateNewUser.AutoSize = True
        Me.btnCreateNewUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCreateNewUser.Location = New System.Drawing.Point(116, 65)
        Me.btnCreateNewUser.Name = "btnCreateNewUser"
        Me.btnCreateNewUser.Size = New System.Drawing.Size(98, 23)
        Me.btnCreateNewUser.TabIndex = 2
        Me.btnCreateNewUser.Text = "Create New User"
        Me.btnCreateNewUser.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.AutoSize = True
        Me.btnSave.Location = New System.Drawing.Point(12, 65)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'TCUserData
        '
        Me.TCUserData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TCUserData.Controls.Add(Me.TPUserInformation)
        Me.TCUserData.Controls.Add(Me.TPPermission)
        Me.TCUserData.Location = New System.Drawing.Point(0, 94)
        Me.TCUserData.Name = "TCUserData"
        Me.TCUserData.SelectedIndex = 0
        Me.TCUserData.Size = New System.Drawing.Size(739, 224)
        Me.TCUserData.TabIndex = 0
        '
        'TPUserInformation
        '
        Me.TPUserInformation.Controls.Add(Me.Panel3)
        Me.TPUserInformation.Controls.Add(Me.txtOfficeNumber)
        Me.TPUserInformation.Controls.Add(Me.txtPhoneNumber)
        Me.TPUserInformation.Controls.Add(Me.cboUnit)
        Me.TPUserInformation.Controls.Add(Me.cboProgram)
        Me.TPUserInformation.Controls.Add(Me.cboBranch)
        Me.TPUserInformation.Controls.Add(Me.txtEmailAddress)
        Me.TPUserInformation.Controls.Add(Me.txtLastName)
        Me.TPUserInformation.Controls.Add(Me.txtFirstName)
        Me.TPUserInformation.Controls.Add(Me.Label16)
        Me.TPUserInformation.Controls.Add(Me.Label15)
        Me.TPUserInformation.Controls.Add(Me.Label14)
        Me.TPUserInformation.Controls.Add(Me.Label13)
        Me.TPUserInformation.Controls.Add(Me.Label12)
        Me.TPUserInformation.Controls.Add(Me.Label10)
        Me.TPUserInformation.Controls.Add(Me.Label9)
        Me.TPUserInformation.Controls.Add(Me.Label7)
        Me.TPUserInformation.Controls.Add(Me.Label3)
        Me.TPUserInformation.Location = New System.Drawing.Point(4, 22)
        Me.TPUserInformation.Name = "TPUserInformation"
        Me.TPUserInformation.Padding = New System.Windows.Forms.Padding(3)
        Me.TPUserInformation.Size = New System.Drawing.Size(731, 198)
        Me.TPUserInformation.TabIndex = 0
        Me.TPUserInformation.Text = "User Information"
        Me.TPUserInformation.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.Controls.Add(Me.rdbInactiveStatus)
        Me.Panel3.Controls.Add(Me.rdbActiveStatus)
        Me.Panel3.Location = New System.Drawing.Point(384, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(141, 26)
        Me.Panel3.TabIndex = 6
        '
        'rdbInactiveStatus
        '
        Me.rdbInactiveStatus.AutoSize = True
        Me.rdbInactiveStatus.Location = New System.Drawing.Point(70, 4)
        Me.rdbInactiveStatus.Name = "rdbInactiveStatus"
        Me.rdbInactiveStatus.Size = New System.Drawing.Size(63, 17)
        Me.rdbInactiveStatus.TabIndex = 1
        Me.rdbInactiveStatus.Text = "Inactive"
        Me.rdbInactiveStatus.UseVisualStyleBackColor = True
        '
        'rdbActiveStatus
        '
        Me.rdbActiveStatus.AutoSize = True
        Me.rdbActiveStatus.Location = New System.Drawing.Point(9, 4)
        Me.rdbActiveStatus.Name = "rdbActiveStatus"
        Me.rdbActiveStatus.Size = New System.Drawing.Size(55, 17)
        Me.rdbActiveStatus.TabIndex = 0
        Me.rdbActiveStatus.Text = "Active"
        Me.rdbActiveStatus.UseVisualStyleBackColor = True
        '
        'txtOfficeNumber
        '
        Me.txtOfficeNumber.Location = New System.Drawing.Point(90, 110)
        Me.txtOfficeNumber.Name = "txtOfficeNumber"
        Me.txtOfficeNumber.Size = New System.Drawing.Size(139, 20)
        Me.txtOfficeNumber.TabIndex = 5
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Location = New System.Drawing.Point(90, 84)
        Me.txtPhoneNumber.Mask = "(999) 000-0000 ext.00000"
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(139, 20)
        Me.txtPhoneNumber.TabIndex = 3
        '
        'cboUnit
        '
        Me.cboUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnit.FormattingEnabled = True
        Me.cboUnit.Location = New System.Drawing.Point(353, 83)
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.Size = New System.Drawing.Size(172, 21)
        Me.cboUnit.TabIndex = 9
        '
        'cboProgram
        '
        Me.cboProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProgram.FormattingEnabled = True
        Me.cboProgram.Location = New System.Drawing.Point(353, 58)
        Me.cboProgram.Name = "cboProgram"
        Me.cboProgram.Size = New System.Drawing.Size(172, 21)
        Me.cboProgram.TabIndex = 8
        '
        'cboBranch
        '
        Me.cboBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(353, 32)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(172, 21)
        Me.cboBranch.TabIndex = 7
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(90, 58)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(181, 20)
        Me.txtEmailAddress.TabIndex = 2
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(90, 32)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(180, 20)
        Me.txtLastName.TabIndex = 1
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(90, 6)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(180, 20)
        Me.txtFirstName.TabIndex = 0
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 113)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(58, 13)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Office No.:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(298, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(89, 13)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Employee Status:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(298, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Branch:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(298, 61)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Program:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(298, 87)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Unit:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 61)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Email Address:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Phone:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Last Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "First Name:"
        '
        'TPPermission
        '
        Me.TPPermission.Controls.Add(Me.cboPermissionProgram)
        Me.TPPermission.Controls.Add(Me.Label1)
        Me.TPPermission.Controls.Add(Me.Panel4)
        Me.TPPermission.Controls.Add(Me.btnClearAllPermissions)
        Me.TPPermission.Controls.Add(Me.Label28)
        Me.TPPermission.Controls.Add(Me.txtCurrentPermissions)
        Me.TPPermission.Controls.Add(Me.txtPassword)
        Me.TPPermission.Controls.Add(Me.cboPermissionBranch)
        Me.TPPermission.Controls.Add(Me.txtUserName)
        Me.TPPermission.Controls.Add(Me.Label21)
        Me.TPPermission.Controls.Add(Me.Label17)
        Me.TPPermission.Controls.Add(Me.Label18)
        Me.TPPermission.Location = New System.Drawing.Point(4, 22)
        Me.TPPermission.Name = "TPPermission"
        Me.TPPermission.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPermission.Size = New System.Drawing.Size(731, 198)
        Me.TPPermission.TabIndex = 1
        Me.TPPermission.Text = "Permissions"
        Me.TPPermission.UseVisualStyleBackColor = True
        '
        'cboPermissionProgram
        '
        Me.cboPermissionProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboPermissionProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPermissionProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPermissionProgram.FormattingEnabled = True
        Me.cboPermissionProgram.Location = New System.Drawing.Point(76, 84)
        Me.cboPermissionProgram.Name = "cboPermissionProgram"
        Me.cboPermissionProgram.Size = New System.Drawing.Size(190, 21)
        Me.cboPermissionProgram.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Program:"
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.lbl10)
        Me.Panel4.Controls.Add(Me.chb10)
        Me.Panel4.Controls.Add(Me.lbl9)
        Me.Panel4.Controls.Add(Me.chb9)
        Me.Panel4.Controls.Add(Me.lbl7)
        Me.Panel4.Controls.Add(Me.lbl8)
        Me.Panel4.Controls.Add(Me.lbl2)
        Me.Panel4.Controls.Add(Me.lbl3)
        Me.Panel4.Controls.Add(Me.lbl4)
        Me.Panel4.Controls.Add(Me.lbl5)
        Me.Panel4.Controls.Add(Me.lbl6)
        Me.Panel4.Controls.Add(Me.lbl1)
        Me.Panel4.Controls.Add(Me.chb1)
        Me.Panel4.Controls.Add(Me.chb2)
        Me.Panel4.Controls.Add(Me.chb8)
        Me.Panel4.Controls.Add(Me.chb3)
        Me.Panel4.Controls.Add(Me.chb7)
        Me.Panel4.Controls.Add(Me.chb4)
        Me.Panel4.Controls.Add(Me.chb6)
        Me.Panel4.Controls.Add(Me.chb5)
        Me.Panel4.Location = New System.Drawing.Point(272, 6)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(257, 184)
        Me.Panel4.TabIndex = 5
        '
        'lbl10
        '
        Me.lbl10.AutoSize = True
        Me.lbl10.Location = New System.Drawing.Point(24, 166)
        Me.lbl10.Name = "lbl10"
        Me.lbl10.Size = New System.Drawing.Size(0, 13)
        Me.lbl10.TabIndex = 39
        Me.lbl10.Visible = False
        '
        'chb10
        '
        Me.chb10.AutoSize = True
        Me.chb10.Location = New System.Drawing.Point(3, 166)
        Me.chb10.Name = "chb10"
        Me.chb10.Size = New System.Drawing.Size(15, 14)
        Me.chb10.TabIndex = 9
        Me.chb10.UseVisualStyleBackColor = True
        Me.chb10.Visible = False
        '
        'lbl9
        '
        Me.lbl9.AutoSize = True
        Me.lbl9.Location = New System.Drawing.Point(24, 148)
        Me.lbl9.Name = "lbl9"
        Me.lbl9.Size = New System.Drawing.Size(0, 13)
        Me.lbl9.TabIndex = 37
        Me.lbl9.Visible = False
        '
        'chb9
        '
        Me.chb9.AutoSize = True
        Me.chb9.Location = New System.Drawing.Point(3, 148)
        Me.chb9.Name = "chb9"
        Me.chb9.Size = New System.Drawing.Size(15, 14)
        Me.chb9.TabIndex = 8
        Me.chb9.UseVisualStyleBackColor = True
        Me.chb9.Visible = False
        '
        'lbl7
        '
        Me.lbl7.AutoSize = True
        Me.lbl7.Location = New System.Drawing.Point(24, 113)
        Me.lbl7.Name = "lbl7"
        Me.lbl7.Size = New System.Drawing.Size(0, 13)
        Me.lbl7.TabIndex = 35
        Me.lbl7.Visible = False
        '
        'lbl8
        '
        Me.lbl8.AutoSize = True
        Me.lbl8.Location = New System.Drawing.Point(24, 130)
        Me.lbl8.Name = "lbl8"
        Me.lbl8.Size = New System.Drawing.Size(0, 13)
        Me.lbl8.TabIndex = 34
        Me.lbl8.Visible = False
        '
        'lbl2
        '
        Me.lbl2.AutoSize = True
        Me.lbl2.Location = New System.Drawing.Point(24, 23)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(0, 13)
        Me.lbl2.TabIndex = 33
        Me.lbl2.Visible = False
        '
        'lbl3
        '
        Me.lbl3.AutoSize = True
        Me.lbl3.Location = New System.Drawing.Point(24, 41)
        Me.lbl3.Name = "lbl3"
        Me.lbl3.Size = New System.Drawing.Size(0, 13)
        Me.lbl3.TabIndex = 32
        Me.lbl3.Visible = False
        '
        'lbl4
        '
        Me.lbl4.AutoSize = True
        Me.lbl4.Location = New System.Drawing.Point(24, 60)
        Me.lbl4.Name = "lbl4"
        Me.lbl4.Size = New System.Drawing.Size(0, 13)
        Me.lbl4.TabIndex = 31
        Me.lbl4.Visible = False
        '
        'lbl5
        '
        Me.lbl5.AutoSize = True
        Me.lbl5.Location = New System.Drawing.Point(24, 76)
        Me.lbl5.Name = "lbl5"
        Me.lbl5.Size = New System.Drawing.Size(0, 13)
        Me.lbl5.TabIndex = 30
        Me.lbl5.Visible = False
        '
        'lbl6
        '
        Me.lbl6.AutoSize = True
        Me.lbl6.Location = New System.Drawing.Point(24, 95)
        Me.lbl6.Name = "lbl6"
        Me.lbl6.Size = New System.Drawing.Size(0, 13)
        Me.lbl6.TabIndex = 29
        Me.lbl6.Visible = False
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(24, 4)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(0, 13)
        Me.lbl1.TabIndex = 28
        Me.lbl1.Visible = False
        '
        'chb1
        '
        Me.chb1.AutoSize = True
        Me.chb1.Location = New System.Drawing.Point(3, 4)
        Me.chb1.Name = "chb1"
        Me.chb1.Size = New System.Drawing.Size(15, 14)
        Me.chb1.TabIndex = 0
        Me.chb1.UseVisualStyleBackColor = True
        Me.chb1.Visible = False
        '
        'chb2
        '
        Me.chb2.AutoSize = True
        Me.chb2.Location = New System.Drawing.Point(3, 22)
        Me.chb2.Name = "chb2"
        Me.chb2.Size = New System.Drawing.Size(15, 14)
        Me.chb2.TabIndex = 1
        Me.chb2.UseVisualStyleBackColor = True
        Me.chb2.Visible = False
        '
        'chb8
        '
        Me.chb8.AutoSize = True
        Me.chb8.Location = New System.Drawing.Point(3, 130)
        Me.chb8.Name = "chb8"
        Me.chb8.Size = New System.Drawing.Size(15, 14)
        Me.chb8.TabIndex = 7
        Me.chb8.UseVisualStyleBackColor = True
        Me.chb8.Visible = False
        '
        'chb3
        '
        Me.chb3.AutoSize = True
        Me.chb3.Location = New System.Drawing.Point(3, 40)
        Me.chb3.Name = "chb3"
        Me.chb3.Size = New System.Drawing.Size(15, 14)
        Me.chb3.TabIndex = 2
        Me.chb3.UseVisualStyleBackColor = True
        Me.chb3.Visible = False
        '
        'chb7
        '
        Me.chb7.AutoSize = True
        Me.chb7.Location = New System.Drawing.Point(3, 112)
        Me.chb7.Name = "chb7"
        Me.chb7.Size = New System.Drawing.Size(15, 14)
        Me.chb7.TabIndex = 6
        Me.chb7.UseVisualStyleBackColor = True
        Me.chb7.Visible = False
        '
        'chb4
        '
        Me.chb4.AutoSize = True
        Me.chb4.Location = New System.Drawing.Point(3, 58)
        Me.chb4.Name = "chb4"
        Me.chb4.Size = New System.Drawing.Size(15, 14)
        Me.chb4.TabIndex = 3
        Me.chb4.UseVisualStyleBackColor = True
        Me.chb4.Visible = False
        '
        'chb6
        '
        Me.chb6.AutoSize = True
        Me.chb6.Location = New System.Drawing.Point(3, 94)
        Me.chb6.Name = "chb6"
        Me.chb6.Size = New System.Drawing.Size(15, 14)
        Me.chb6.TabIndex = 5
        Me.chb6.UseVisualStyleBackColor = True
        Me.chb6.Visible = False
        '
        'chb5
        '
        Me.chb5.AutoSize = True
        Me.chb5.Location = New System.Drawing.Point(3, 76)
        Me.chb5.Name = "chb5"
        Me.chb5.Size = New System.Drawing.Size(15, 14)
        Me.chb5.TabIndex = 4
        Me.chb5.UseVisualStyleBackColor = True
        Me.chb5.Visible = False
        '
        'btnClearAllPermissions
        '
        Me.btnClearAllPermissions.AutoSize = True
        Me.btnClearAllPermissions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAllPermissions.Location = New System.Drawing.Point(535, 147)
        Me.btnClearAllPermissions.Name = "btnClearAllPermissions"
        Me.btnClearAllPermissions.Size = New System.Drawing.Size(113, 23)
        Me.btnClearAllPermissions.TabIndex = 7
        Me.btnClearAllPermissions.Text = "Clear All Permissions"
        Me.btnClearAllPermissions.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(535, 9)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(102, 13)
        Me.Label28.TabIndex = 20
        Me.Label28.Text = "Current Permissions:"
        '
        'txtCurrentPermissions
        '
        Me.txtCurrentPermissions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrentPermissions.Location = New System.Drawing.Point(535, 25)
        Me.txtCurrentPermissions.Multiline = True
        Me.txtCurrentPermissions.Name = "txtCurrentPermissions"
        Me.txtCurrentPermissions.ReadOnly = True
        Me.txtCurrentPermissions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCurrentPermissions.Size = New System.Drawing.Size(188, 116)
        Me.txtCurrentPermissions.TabIndex = 6
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(76, 32)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(190, 20)
        Me.txtPassword.TabIndex = 1
        '
        'cboPermissionBranch
        '
        Me.cboPermissionBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboPermissionBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPermissionBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPermissionBranch.FormattingEnabled = True
        Me.cboPermissionBranch.Location = New System.Drawing.Point(76, 58)
        Me.cboPermissionBranch.Name = "cboPermissionBranch"
        Me.cboPermissionBranch.Size = New System.Drawing.Size(190, 21)
        Me.cboPermissionBranch.TabIndex = 2
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(76, 6)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(189, 20)
        Me.txtUserName.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(8, 61)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(44, 13)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Branch:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(8, 35)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 13)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "Password:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(8, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 13)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "User Name:"
        '
        'pnlSearch
        '
        Me.pnlSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSearch.Controls.Add(Me.lblCount)
        Me.pnlSearch.Controls.Add(Me.btnReset)
        Me.pnlSearch.Controls.Add(Me.btnSearch)
        Me.pnlSearch.Controls.Add(Me.cboSearchUnit)
        Me.pnlSearch.Controls.Add(Me.cboSearchProgram)
        Me.pnlSearch.Controls.Add(Me.cboSearchBranch)
        Me.pnlSearch.Controls.Add(Me.Label25)
        Me.pnlSearch.Controls.Add(Me.Label26)
        Me.pnlSearch.Controls.Add(Me.Label27)
        Me.pnlSearch.Controls.Add(Me.txtSearchLastName)
        Me.pnlSearch.Controls.Add(Me.txtSearchFirstName)
        Me.pnlSearch.Controls.Add(Me.Label23)
        Me.pnlSearch.Controls.Add(Me.Label24)
        Me.pnlSearch.Location = New System.Drawing.Point(0, 334)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(737, 62)
        Me.pnlSearch.TabIndex = 3
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(585, 11)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(41, 13)
        Me.lblCount.TabIndex = 27
        Me.lblCount.Text = "Count: "
        '
        'btnReset
        '
        Me.btnReset.AutoSize = True
        Me.btnReset.Location = New System.Drawing.Point(521, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(58, 23)
        Me.btnReset.TabIndex = 8
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.AutoSize = True
        Me.btnSearch.Location = New System.Drawing.Point(457, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(58, 23)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboSearchUnit
        '
        Me.cboSearchUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchUnit.FormattingEnabled = True
        Me.cboSearchUnit.Location = New System.Drawing.Point(457, 35)
        Me.cboSearchUnit.Name = "cboSearchUnit"
        Me.cboSearchUnit.Size = New System.Drawing.Size(138, 21)
        Me.cboSearchUnit.TabIndex = 6
        '
        'cboSearchProgram
        '
        Me.cboSearchProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchProgram.FormattingEnabled = True
        Me.cboSearchProgram.Location = New System.Drawing.Point(277, 35)
        Me.cboSearchProgram.Name = "cboSearchProgram"
        Me.cboSearchProgram.Size = New System.Drawing.Size(139, 21)
        Me.cboSearchProgram.TabIndex = 5
        '
        'cboSearchBranch
        '
        Me.cboSearchBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchBranch.FormattingEnabled = True
        Me.cboSearchBranch.Location = New System.Drawing.Point(76, 35)
        Me.cboSearchBranch.Name = "cboSearchBranch"
        Me.cboSearchBranch.Size = New System.Drawing.Size(129, 21)
        Me.cboSearchBranch.TabIndex = 4
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(26, 38)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(44, 13)
        Me.Label25.TabIndex = 24
        Me.Label25.Text = "Branch:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(222, 38)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(49, 13)
        Me.Label26.TabIndex = 23
        Me.Label26.Text = "Program:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(422, 38)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(29, 13)
        Me.Label27.TabIndex = 22
        Me.Label27.Text = "Unit:"
        '
        'txtSearchLastName
        '
        Me.txtSearchLastName.Location = New System.Drawing.Point(76, 8)
        Me.txtSearchLastName.Name = "txtSearchLastName"
        Me.txtSearchLastName.Size = New System.Drawing.Size(129, 20)
        Me.txtSearchLastName.TabIndex = 0
        '
        'txtSearchFirstName
        '
        Me.txtSearchFirstName.Location = New System.Drawing.Point(277, 8)
        Me.txtSearchFirstName.Name = "txtSearchFirstName"
        Me.txtSearchFirstName.Size = New System.Drawing.Size(139, 20)
        Me.txtSearchFirstName.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(211, 11)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(60, 13)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "First Name:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(9, 11)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(61, 13)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Last Name:"
        '
        'dgvUserAdminTool
        '
        Me.dgvUserAdminTool.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUserAdminTool.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvUserAdminTool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUserAdminTool.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvUserAdminTool.Location = New System.Drawing.Point(0, 396)
        Me.dgvUserAdminTool.Name = "dgvUserAdminTool"
        Me.dgvUserAdminTool.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUserAdminTool.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvUserAdminTool.Size = New System.Drawing.Size(737, 165)
        Me.dgvUserAdminTool.TabIndex = 4
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Location = New System.Drawing.Point(220, 70)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(0, 13)
        Me.lblUserID.TabIndex = 35
        Me.lblUserID.Visible = False
        '
        'lblEmailAddress
        '
        Me.lblEmailAddress.AutoSize = True
        Me.lblEmailAddress.Location = New System.Drawing.Point(12, 35)
        Me.lblEmailAddress.Name = "lblEmailAddress"
        Me.lblEmailAddress.Size = New System.Drawing.Size(79, 13)
        Me.lblEmailAddress.TabIndex = 36
        Me.lblEmailAddress.Text = "Email Address: "
        '
        'IAIPUserAdminTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 561)
        Me.Controls.Add(Me.lblEmailAddress)
        Me.Controls.Add(Me.lblPermissions)
        Me.Controls.Add(Me.TCUserData)
        Me.Controls.Add(Me.lblUserID)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.btnCreateNewUser)
        Me.Controls.Add(Me.dgvUserAdminTool)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblUserName)
        Me.MinimumSize = New System.Drawing.Size(711, 526)
        Me.Name = "IAIPUserAdminTool"
        Me.Text = "IAIP User Management"
        Me.TCUserData.ResumeLayout(False)
        Me.TPUserInformation.ResumeLayout(False)
        Me.TPUserInformation.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TPPermission.ResumeLayout(False)
        Me.TPPermission.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgvUserAdminTool, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents TCUserData As System.Windows.Forms.TabControl
    Friend WithEvents TPUserInformation As System.Windows.Forms.TabPage
    Friend WithEvents TPPermission As System.Windows.Forms.TabPage
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdbInactiveStatus As System.Windows.Forms.RadioButton
    Friend WithEvents rdbActiveStatus As System.Windows.Forms.RadioButton
    Friend WithEvents txtOfficeNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cboUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cboProgram As System.Windows.Forms.ComboBox
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents chb1 As System.Windows.Forms.CheckBox
    Friend WithEvents cboPermissionBranch As System.Windows.Forms.ComboBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cboSearchUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cboSearchProgram As System.Windows.Forms.ComboBox
    Friend WithEvents cboSearchBranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSearchLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtCurrentPermissions As System.Windows.Forms.TextBox
    Friend WithEvents dgvUserAdminTool As System.Windows.Forms.DataGridView
    Friend WithEvents btnCreateNewUser As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents chb8 As System.Windows.Forms.CheckBox
    Friend WithEvents chb7 As System.Windows.Forms.CheckBox
    Friend WithEvents chb6 As System.Windows.Forms.CheckBox
    Friend WithEvents chb5 As System.Windows.Forms.CheckBox
    Friend WithEvents chb4 As System.Windows.Forms.CheckBox
    Friend WithEvents chb3 As System.Windows.Forms.CheckBox
    Friend WithEvents chb2 As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearAllPermissions As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents cboPermissionProgram As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl7 As System.Windows.Forms.Label
    Friend WithEvents lbl8 As System.Windows.Forms.Label
    Friend WithEvents lbl2 As System.Windows.Forms.Label
    Friend WithEvents lbl3 As System.Windows.Forms.Label
    Friend WithEvents lbl4 As System.Windows.Forms.Label
    Friend WithEvents lbl5 As System.Windows.Forms.Label
    Friend WithEvents lbl6 As System.Windows.Forms.Label
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents lblPermissions As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents lbl10 As System.Windows.Forms.Label
    Friend WithEvents chb10 As System.Windows.Forms.CheckBox
    Friend WithEvents lbl9 As System.Windows.Forms.Label
    Friend WithEvents chb9 As System.Windows.Forms.CheckBox
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents lblEmailAddress As System.Windows.Forms.Label
End Class
