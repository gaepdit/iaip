<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GecoTool
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TCGecoTools = New System.Windows.Forms.TabControl()
        Me.TPWebUsers = New System.Windows.Forms.TabPage()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.PanelFacility = New System.Windows.Forms.Panel()
        Me.lblFaciltyName = New System.Windows.Forms.Label()
        Me.lblFacility = New System.Windows.Forms.Label()
        Me.cboUsers = New System.Windows.Forms.ComboBox()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnViewUserData = New System.Windows.Forms.Button()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.TPWebUsers1 = New System.Windows.Forms.TabPage()
        Me.pnlUserFacility = New System.Windows.Forms.Panel()
        Me.dgvUserFacilities = New Iaip.IaipDataGridView()
        Me.pnlUserInfo = New System.Windows.Forms.Panel()
        Me.panelUserPermissionsTools = New System.Windows.Forms.Panel()
        Me.btnUpdateUser = New System.Windows.Forms.Button()
        Me.mtbFacilityToAdd = New Iaip.AirsNumberEntryForm()
        Me.btnAddFacilitytoUser = New System.Windows.Forms.Button()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.btnDeleteFacilityUser = New System.Windows.Forms.Button()
        Me.lblChangeEmailAddress = New System.Windows.Forms.Label()
        Me.btnChangeEmailAddress = New System.Windows.Forms.Button()
        Me.txtEditEmail = New System.Windows.Forms.TextBox()
        Me.lblConfirmDate = New System.Windows.Forms.Label()
        Me.lblLastLogIn = New System.Windows.Forms.Label()
        Me.txtWebUserID = New System.Windows.Forms.TextBox()
        Me.btnEditUserData = New System.Windows.Forms.Button()
        Me.lblCityStateZip = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lableUserAddress = New System.Windows.Forms.Label()
        Me.lblFaxNo = New System.Windows.Forms.Label()
        Me.lblPhoneNo = New System.Windows.Forms.Label()
        Me.lblCoName = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblLName = New System.Windows.Forms.Label()
        Me.lblFName = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.pnlUserEmail = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtWebUserEmail = New System.Windows.Forms.TextBox()
        Me.btnViewEmailData = New System.Windows.Forms.Button()
        Me.TCGecoTools.SuspendLayout()
        Me.TPWebUsers.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFacility.SuspendLayout()
        Me.TPWebUsers1.SuspendLayout()
        Me.pnlUserFacility.SuspendLayout()
        CType(Me.dgvUserFacilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUserInfo.SuspendLayout()
        Me.panelUserPermissionsTools.SuspendLayout()
        Me.pnlUserEmail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(731, 132)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 258
        '
        'TCGecoTools
        '
        Me.TCGecoTools.Controls.Add(Me.TPWebUsers)
        Me.TCGecoTools.Controls.Add(Me.TPWebUsers1)
        Me.TCGecoTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCGecoTools.Location = New System.Drawing.Point(0, 0)
        Me.TCGecoTools.Name = "TCGecoTools"
        Me.TCGecoTools.SelectedIndex = 0
        Me.TCGecoTools.Size = New System.Drawing.Size(747, 661)
        Me.TCGecoTools.TabIndex = 0
        '
        'TPWebUsers
        '
        Me.TPWebUsers.Controls.Add(Me.dgvUsers)
        Me.TPWebUsers.Controls.Add(Me.PanelFacility)
        Me.TPWebUsers.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers.Name = "TPWebUsers"
        Me.TPWebUsers.Size = New System.Drawing.Size(739, 635)
        Me.TPWebUsers.TabIndex = 1
        Me.TPWebUsers.Text = "Web App Users - Facility"
        Me.TPWebUsers.UseVisualStyleBackColor = True
        '
        'dgvUsers
        '
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUsers.Location = New System.Drawing.Point(0, 162)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.Size = New System.Drawing.Size(739, 473)
        Me.dgvUsers.TabIndex = 0
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.lblFaciltyName)
        Me.PanelFacility.Controls.Add(Me.lblFacility)
        Me.PanelFacility.Controls.Add(Me.cboUsers)
        Me.PanelFacility.Controls.Add(Me.Label177)
        Me.PanelFacility.Controls.Add(Me.Label6)
        Me.PanelFacility.Controls.Add(Me.mtbAIRSNumber)
        Me.PanelFacility.Controls.Add(Me.btnDelete)
        Me.PanelFacility.Controls.Add(Me.btnUpdate)
        Me.PanelFacility.Controls.Add(Me.btnViewUserData)
        Me.PanelFacility.Controls.Add(Me.btnAddUser)
        Me.PanelFacility.Controls.Add(Me.Label17)
        Me.PanelFacility.Controls.Add(Me.txtEmail)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(739, 162)
        Me.PanelFacility.TabIndex = 147
        '
        'lblFaciltyName
        '
        Me.lblFaciltyName.AutoSize = True
        Me.lblFaciltyName.Location = New System.Drawing.Point(97, 139)
        Me.lblFaciltyName.Name = "lblFaciltyName"
        Me.lblFaciltyName.Size = New System.Drawing.Size(0, 13)
        Me.lblFaciltyName.TabIndex = 290
        '
        'lblFacility
        '
        Me.lblFacility.AutoSize = True
        Me.lblFacility.Location = New System.Drawing.Point(15, 139)
        Me.lblFacility.Name = "lblFacility"
        Me.lblFacility.Size = New System.Drawing.Size(89, 13)
        Me.lblFacility.TabIndex = 289
        Me.lblFacility.Text = "Current Users for:"
        '
        'cboUsers
        '
        Me.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(366, 61)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(259, 21)
        Me.cboUsers.TabIndex = 4
        '
        'Label177
        '
        Me.Label177.AutoSize = True
        Me.Label177.Location = New System.Drawing.Point(15, 13)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(72, 13)
        Me.Label177.TabIndex = 285
        Me.Label177.Text = "AIRS Number"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(363, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(150, 13)
        Me.Label6.TabIndex = 280
        Me.Label6.Text = "Delete a User for this Facility:  "
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(93, 10)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(66, 20)
        Me.mtbAIRSNumber.TabIndex = 0
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(631, 61)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(97, 21)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete User"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(18, 101)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "Save Changes"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnViewUserData
        '
        Me.btnViewUserData.Location = New System.Drawing.Point(165, 9)
        Me.btnViewUserData.Name = "btnViewUserData"
        Me.btnViewUserData.Size = New System.Drawing.Size(82, 21)
        Me.btnViewUserData.TabIndex = 1
        Me.btnViewUserData.Text = "View Data"
        Me.btnViewUserData.UseVisualStyleBackColor = True
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(253, 61)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(82, 21)
        Me.btnAddUser.TabIndex = 3
        Me.btnAddUser.Text = "Add User"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(15, 45)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(132, 13)
        Me.Label17.TabIndex = 276
        Me.Label17.Text = "Add a User to this Facility: "
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(18, 61)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(229, 20)
        Me.txtEmail.TabIndex = 2
        '
        'TPWebUsers1
        '
        Me.TPWebUsers1.Controls.Add(Me.pnlUserFacility)
        Me.TPWebUsers1.Controls.Add(Me.pnlUserEmail)
        Me.TPWebUsers1.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers1.Name = "TPWebUsers1"
        Me.TPWebUsers1.Size = New System.Drawing.Size(739, 635)
        Me.TPWebUsers1.TabIndex = 2
        Me.TPWebUsers1.Text = "Web App Users - Email"
        Me.TPWebUsers1.UseVisualStyleBackColor = True
        '
        'pnlUserFacility
        '
        Me.pnlUserFacility.Controls.Add(Me.dgvUserFacilities)
        Me.pnlUserFacility.Controls.Add(Me.pnlUserInfo)
        Me.pnlUserFacility.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUserFacility.Location = New System.Drawing.Point(0, 45)
        Me.pnlUserFacility.Name = "pnlUserFacility"
        Me.pnlUserFacility.Size = New System.Drawing.Size(739, 590)
        Me.pnlUserFacility.TabIndex = 153
        '
        'dgvUserFacilities
        '
        Me.dgvUserFacilities.AllowUserToOrderColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvUserFacilities.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUserFacilities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvUserFacilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUserFacilities.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUserFacilities.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2
        Me.dgvUserFacilities.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvUserFacilities.LinkifyColumnByName = Nothing
        Me.dgvUserFacilities.Location = New System.Drawing.Point(0, 335)
        Me.dgvUserFacilities.Name = "dgvUserFacilities"
        Me.dgvUserFacilities.ReadOnly = False
        Me.dgvUserFacilities.ResultsCountLabel = Nothing
        Me.dgvUserFacilities.ResultsCountLabelFormat = "{0} found"
        Me.dgvUserFacilities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect
        Me.dgvUserFacilities.Size = New System.Drawing.Size(739, 255)
        Me.dgvUserFacilities.StandardTab = True
        Me.dgvUserFacilities.TabIndex = 1
        '
        'pnlUserInfo
        '
        Me.pnlUserInfo.Controls.Add(Me.panelUserPermissionsTools)
        Me.pnlUserInfo.Controls.Add(Me.lblChangeEmailAddress)
        Me.pnlUserInfo.Controls.Add(Me.btnChangeEmailAddress)
        Me.pnlUserInfo.Controls.Add(Me.txtEditEmail)
        Me.pnlUserInfo.Controls.Add(Me.lblConfirmDate)
        Me.pnlUserInfo.Controls.Add(Me.lblLastLogIn)
        Me.pnlUserInfo.Controls.Add(Me.txtWebUserID)
        Me.pnlUserInfo.Controls.Add(Me.btnEditUserData)
        Me.pnlUserInfo.Controls.Add(Me.lblCityStateZip)
        Me.pnlUserInfo.Controls.Add(Me.lblAddress)
        Me.pnlUserInfo.Controls.Add(Me.lableUserAddress)
        Me.pnlUserInfo.Controls.Add(Me.lblFaxNo)
        Me.pnlUserInfo.Controls.Add(Me.lblPhoneNo)
        Me.pnlUserInfo.Controls.Add(Me.lblCoName)
        Me.pnlUserInfo.Controls.Add(Me.lblTitle)
        Me.pnlUserInfo.Controls.Add(Me.lblLName)
        Me.pnlUserInfo.Controls.Add(Me.lblFName)
        Me.pnlUserInfo.Controls.Add(Me.Label44)
        Me.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserInfo.Name = "pnlUserInfo"
        Me.pnlUserInfo.Size = New System.Drawing.Size(739, 335)
        Me.pnlUserInfo.TabIndex = 0
        '
        'panelUserPermissionsTools
        '
        Me.panelUserPermissionsTools.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelUserPermissionsTools.Controls.Add(Me.btnUpdateUser)
        Me.panelUserPermissionsTools.Controls.Add(Me.mtbFacilityToAdd)
        Me.panelUserPermissionsTools.Controls.Add(Me.btnAddFacilitytoUser)
        Me.panelUserPermissionsTools.Controls.Add(Me.Label53)
        Me.panelUserPermissionsTools.Controls.Add(Me.btnDeleteFacilityUser)
        Me.panelUserPermissionsTools.Location = New System.Drawing.Point(0, 289)
        Me.panelUserPermissionsTools.Name = "panelUserPermissionsTools"
        Me.panelUserPermissionsTools.Size = New System.Drawing.Size(743, 43)
        Me.panelUserPermissionsTools.TabIndex = 2
        Me.panelUserPermissionsTools.Visible = False
        '
        'btnUpdateUser
        '
        Me.btnUpdateUser.Location = New System.Drawing.Point(324, 3)
        Me.btnUpdateUser.Name = "btnUpdateUser"
        Me.btnUpdateUser.Size = New System.Drawing.Size(156, 25)
        Me.btnUpdateUser.TabIndex = 2
        Me.btnUpdateUser.Text = "Save changes to permissions"
        Me.btnUpdateUser.UseVisualStyleBackColor = True
        '
        'mtbFacilityToAdd
        '
        Me.mtbFacilityToAdd.AirsNumber = Nothing
        Me.mtbFacilityToAdd.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.mtbFacilityToAdd.ErrorMessageLabel = Nothing
        Me.mtbFacilityToAdd.FacilityMustExist = True
        Me.mtbFacilityToAdd.Location = New System.Drawing.Point(145, 5)
        Me.mtbFacilityToAdd.Name = "mtbFacilityToAdd"
        Me.mtbFacilityToAdd.ReadOnly = False
        Me.mtbFacilityToAdd.Size = New System.Drawing.Size(78, 23)
        Me.mtbFacilityToAdd.TabIndex = 0
        Me.mtbFacilityToAdd.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'btnAddFacilitytoUser
        '
        Me.btnAddFacilitytoUser.AutoSize = True
        Me.btnAddFacilitytoUser.Location = New System.Drawing.Point(229, 3)
        Me.btnAddFacilitytoUser.Name = "btnAddFacilitytoUser"
        Me.btnAddFacilitytoUser.Size = New System.Drawing.Size(77, 25)
        Me.btnAddFacilitytoUser.TabIndex = 1
        Me.btnAddFacilitytoUser.Text = "Add Facility"
        Me.btnAddFacilitytoUser.UseVisualStyleBackColor = True
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(15, 9)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(124, 13)
        Me.Label53.TabIndex = 276
        Me.Label53.Text = "Add a facility to this user:"
        '
        'btnDeleteFacilityUser
        '
        Me.btnDeleteFacilityUser.Location = New System.Drawing.Point(500, 3)
        Me.btnDeleteFacilityUser.Name = "btnDeleteFacilityUser"
        Me.btnDeleteFacilityUser.Size = New System.Drawing.Size(148, 37)
        Me.btnDeleteFacilityUser.TabIndex = 3
        Me.btnDeleteFacilityUser.Text = "Remove selected facility " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "for this user"
        Me.btnDeleteFacilityUser.UseVisualStyleBackColor = True
        '
        'lblChangeEmailAddress
        '
        Me.lblChangeEmailAddress.AutoSize = True
        Me.lblChangeEmailAddress.Location = New System.Drawing.Point(269, 180)
        Me.lblChangeEmailAddress.Name = "lblChangeEmailAddress"
        Me.lblChangeEmailAddress.Size = New System.Drawing.Size(173, 26)
        Me.lblChangeEmailAddress.TabIndex = 284
        Me.lblChangeEmailAddress.Text = "User will be required to confirm this " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "email address before it is active."
        Me.lblChangeEmailAddress.Visible = False
        '
        'btnChangeEmailAddress
        '
        Me.btnChangeEmailAddress.AutoSize = True
        Me.btnChangeEmailAddress.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnChangeEmailAddress.Location = New System.Drawing.Point(272, 154)
        Me.btnChangeEmailAddress.Name = "btnChangeEmailAddress"
        Me.btnChangeEmailAddress.Size = New System.Drawing.Size(136, 23)
        Me.btnChangeEmailAddress.TabIndex = 2
        Me.btnChangeEmailAddress.Text = "Save New Email Address"
        Me.btnChangeEmailAddress.UseVisualStyleBackColor = True
        Me.btnChangeEmailAddress.Visible = False
        '
        'txtEditEmail
        '
        Me.txtEditEmail.Location = New System.Drawing.Point(272, 128)
        Me.txtEditEmail.Name = "txtEditEmail"
        Me.txtEditEmail.Size = New System.Drawing.Size(208, 20)
        Me.txtEditEmail.TabIndex = 1
        Me.txtEditEmail.Visible = False
        '
        'lblConfirmDate
        '
        Me.lblConfirmDate.AutoSize = True
        Me.lblConfirmDate.Location = New System.Drawing.Point(269, 3)
        Me.lblConfirmDate.Name = "lblConfirmDate"
        Me.lblConfirmDate.Size = New System.Drawing.Size(65, 13)
        Me.lblConfirmDate.TabIndex = 42
        Me.lblConfirmDate.Text = "confirm date"
        '
        'lblLastLogIn
        '
        Me.lblLastLogIn.AutoSize = True
        Me.lblLastLogIn.Location = New System.Drawing.Point(269, 22)
        Me.lblLastLogIn.Name = "lblLastLogIn"
        Me.lblLastLogIn.Size = New System.Drawing.Size(72, 13)
        Me.lblLastLogIn.TabIndex = 41
        Me.lblLastLogIn.Text = "last login date"
        '
        'txtWebUserID
        '
        Me.txtWebUserID.Location = New System.Drawing.Point(447, 3)
        Me.txtWebUserID.Name = "txtWebUserID"
        Me.txtWebUserID.Size = New System.Drawing.Size(33, 20)
        Me.txtWebUserID.TabIndex = 0
        Me.txtWebUserID.Visible = False
        '
        'btnEditUserData
        '
        Me.btnEditUserData.AutoSize = True
        Me.btnEditUserData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditUserData.Location = New System.Drawing.Point(272, 99)
        Me.btnEditUserData.Name = "btnEditUserData"
        Me.btnEditUserData.Size = New System.Drawing.Size(129, 23)
        Me.btnEditUserData.TabIndex = 0
        Me.btnEditUserData.Text = "Edit User Email Address"
        Me.btnEditUserData.UseVisualStyleBackColor = True
        Me.btnEditUserData.Visible = False
        '
        'lblCityStateZip
        '
        Me.lblCityStateZip.AutoSize = True
        Me.lblCityStateZip.Location = New System.Drawing.Point(11, 193)
        Me.lblCityStateZip.Name = "lblCityStateZip"
        Me.lblCityStateZip.Size = New System.Drawing.Size(0, 13)
        Me.lblCityStateZip.TabIndex = 9
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(11, 172)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(0, 13)
        Me.lblAddress.TabIndex = 8
        '
        'lableUserAddress
        '
        Me.lableUserAddress.AutoSize = True
        Me.lableUserAddress.Location = New System.Drawing.Point(11, 151)
        Me.lableUserAddress.Name = "lableUserAddress"
        Me.lableUserAddress.Size = New System.Drawing.Size(48, 13)
        Me.lableUserAddress.TabIndex = 7
        Me.lableUserAddress.Text = "Address:"
        Me.lableUserAddress.Visible = False
        '
        'lblFaxNo
        '
        Me.lblFaxNo.AutoSize = True
        Me.lblFaxNo.Location = New System.Drawing.Point(11, 130)
        Me.lblFaxNo.Name = "lblFaxNo"
        Me.lblFaxNo.Size = New System.Drawing.Size(0, 13)
        Me.lblFaxNo.TabIndex = 6
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.Location = New System.Drawing.Point(11, 109)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(0, 13)
        Me.lblPhoneNo.TabIndex = 5
        '
        'lblCoName
        '
        Me.lblCoName.AutoSize = True
        Me.lblCoName.Location = New System.Drawing.Point(11, 88)
        Me.lblCoName.Name = "lblCoName"
        Me.lblCoName.Size = New System.Drawing.Size(0, 13)
        Me.lblCoName.TabIndex = 4
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(11, 67)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(0, 13)
        Me.lblTitle.TabIndex = 3
        '
        'lblLName
        '
        Me.lblLName.AutoSize = True
        Me.lblLName.Location = New System.Drawing.Point(11, 46)
        Me.lblLName.Name = "lblLName"
        Me.lblLName.Size = New System.Drawing.Size(0, 13)
        Me.lblLName.TabIndex = 2
        '
        'lblFName
        '
        Me.lblFName.AutoSize = True
        Me.lblFName.Location = New System.Drawing.Point(11, 25)
        Me.lblFName.Name = "lblFName"
        Me.lblFName.Size = New System.Drawing.Size(0, 13)
        Me.lblFName.TabIndex = 1
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(4, 3)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(76, 13)
        Me.Label44.TabIndex = 0
        Me.Label44.Text = "User Details"
        '
        'pnlUserEmail
        '
        Me.pnlUserEmail.Controls.Add(Me.Label39)
        Me.pnlUserEmail.Controls.Add(Me.txtWebUserEmail)
        Me.pnlUserEmail.Controls.Add(Me.btnViewEmailData)
        Me.pnlUserEmail.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserEmail.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserEmail.Name = "pnlUserEmail"
        Me.pnlUserEmail.Size = New System.Drawing.Size(739, 45)
        Me.pnlUserEmail.TabIndex = 0
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(7, 12)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(101, 13)
        Me.Label39.TabIndex = 285
        Me.Label39.Text = "User Email Address:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtWebUserEmail
        '
        Me.txtWebUserEmail.Location = New System.Drawing.Point(114, 9)
        Me.txtWebUserEmail.Name = "txtWebUserEmail"
        Me.txtWebUserEmail.Size = New System.Drawing.Size(224, 20)
        Me.txtWebUserEmail.TabIndex = 0
        '
        'btnViewEmailData
        '
        Me.btnViewEmailData.AutoSize = True
        Me.btnViewEmailData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewEmailData.Location = New System.Drawing.Point(344, 7)
        Me.btnViewEmailData.Name = "btnViewEmailData"
        Me.btnViewEmailData.Size = New System.Drawing.Size(66, 23)
        Me.btnViewEmailData.TabIndex = 1
        Me.btnViewEmailData.Text = "View Data"
        Me.btnViewEmailData.UseVisualStyleBackColor = True
        '
        'GecoTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 661)
        Me.Controls.Add(Me.TCGecoTools)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(763, 376)
        Me.Name = "GecoTool"
        Me.Text = "GECO User Tools"
        Me.TCGecoTools.ResumeLayout(False)
        Me.TPWebUsers.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.TPWebUsers1.ResumeLayout(False)
        Me.pnlUserFacility.ResumeLayout(False)
        CType(Me.dgvUserFacilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUserInfo.ResumeLayout(False)
        Me.pnlUserInfo.PerformLayout()
        Me.panelUserPermissionsTools.ResumeLayout(False)
        Me.panelUserPermissionsTools.PerformLayout()
        Me.pnlUserEmail.ResumeLayout(False)
        Me.pnlUserEmail.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TCGecoTools As TabControl
    Friend WithEvents TPWebUsers As TabPage
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents PanelFacility As Panel
    Friend WithEvents lblFaciltyName As Label
    Friend WithEvents lblFacility As Label
    Friend WithEvents cboUsers As ComboBox
    Friend WithEvents Label177 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents mtbAIRSNumber As MaskedTextBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnAddUser As Button
    Friend WithEvents Label17 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents TPWebUsers1 As TabPage
    Friend WithEvents pnlUserFacility As Panel
    Friend WithEvents dgvUserFacilities As IaipDataGridView
    Friend WithEvents pnlUserInfo As Panel
    Friend WithEvents btnChangeEmailAddress As Button
    Friend WithEvents txtEditEmail As TextBox
    Friend WithEvents lblConfirmDate As Label
    Friend WithEvents lblLastLogIn As Label
    Friend WithEvents btnDeleteFacilityUser As Button
    Friend WithEvents btnUpdateUser As Button
    Friend WithEvents Label53 As Label
    Friend WithEvents btnAddFacilitytoUser As Button
    Friend WithEvents txtWebUserID As TextBox
    Friend WithEvents btnEditUserData As Button
    Friend WithEvents lblCityStateZip As Label
    Friend WithEvents lblAddress As Label
    Friend WithEvents lableUserAddress As Label
    Friend WithEvents lblFaxNo As Label
    Friend WithEvents lblPhoneNo As Label
    Friend WithEvents lblCoName As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblLName As Label
    Friend WithEvents lblFName As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents pnlUserEmail As Panel
    Friend WithEvents Label39 As Label
    Friend WithEvents txtWebUserEmail As TextBox
    Friend WithEvents btnViewUserData As Button
    Friend WithEvents btnViewEmailData As Button
    Friend WithEvents lblChangeEmailAddress As Label
    Friend WithEvents mtbFacilityToAdd As AirsNumberEntryForm
    Friend WithEvents panelUserPermissionsTools As Panel
End Class
