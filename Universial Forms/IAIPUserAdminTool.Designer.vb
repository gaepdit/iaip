<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPUserAdminTool
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPUserAdminTool))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tsmOpenMaintenanceTool = New System.Windows.Forms.ToolStripMenuItem
        Me.tsbViewOrgChart = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiViewPhoneList = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpOnlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.tsbClear = New System.Windows.Forms.ToolStripButton
        Me.lblFirstName = New System.Windows.Forms.Label
        Me.lblLastName = New System.Windows.Forms.Label
        Me.lblPassword = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.lblEmployeeID = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblPermissions = New System.Windows.Forms.Label
        Me.lblUserID = New System.Windows.Forms.Label
        Me.lblFaxNumber = New System.Windows.Forms.Label
        Me.lblPhoneNumber = New System.Windows.Forms.Label
        Me.lblEmailAddress = New System.Windows.Forms.Label
        Me.btnCreateNewUser = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TCUserData = New System.Windows.Forms.TabControl
        Me.TPUserInformation = New System.Windows.Forms.TabPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rdbInactiveStatus = New System.Windows.Forms.RadioButton
        Me.rdbActiveStatus = New System.Windows.Forms.RadioButton
        Me.txtOfficeNumber = New System.Windows.Forms.TextBox
        Me.mtbPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.mtbFaxNumber = New System.Windows.Forms.MaskedTextBox
        Me.cboUnit = New System.Windows.Forms.ComboBox
        Me.cboProgram = New System.Windows.Forms.ComboBox
        Me.cboBranch = New System.Windows.Forms.ComboBox
        Me.txtEmailAddress = New System.Windows.Forms.TextBox
        Me.txtEmployeeID = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TPPermission = New System.Windows.Forms.TabPage
        Me.cboPermissionProgram = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lbl10 = New System.Windows.Forms.Label
        Me.chb10 = New System.Windows.Forms.CheckBox
        Me.lbl9 = New System.Windows.Forms.Label
        Me.chb9 = New System.Windows.Forms.CheckBox
        Me.lbl7 = New System.Windows.Forms.Label
        Me.lbl8 = New System.Windows.Forms.Label
        Me.lbl2 = New System.Windows.Forms.Label
        Me.lbl3 = New System.Windows.Forms.Label
        Me.lbl4 = New System.Windows.Forms.Label
        Me.lbl5 = New System.Windows.Forms.Label
        Me.lbl6 = New System.Windows.Forms.Label
        Me.lbl1 = New System.Windows.Forms.Label
        Me.chb1 = New System.Windows.Forms.CheckBox
        Me.chb2 = New System.Windows.Forms.CheckBox
        Me.chb8 = New System.Windows.Forms.CheckBox
        Me.chb3 = New System.Windows.Forms.CheckBox
        Me.chb7 = New System.Windows.Forms.CheckBox
        Me.chb4 = New System.Windows.Forms.CheckBox
        Me.chb6 = New System.Windows.Forms.CheckBox
        Me.chb5 = New System.Windows.Forms.CheckBox
        Me.btnClearAllPermissions = New System.Windows.Forms.Button
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtCurrentPermissions = New System.Windows.Forms.TextBox
        Me.cboPermissionAccounts = New System.Windows.Forms.ComboBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.cboPermissionBranch = New System.Windows.Forms.ComboBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblCount = New System.Windows.Forms.Label
        Me.dgvUserAdminTool = New System.Windows.Forms.DataGridView
        Me.btnAll = New System.Windows.Forms.Button
        Me.btnReset = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cboSearchUnit = New System.Windows.Forms.ComboBox
        Me.cboSearchProgram = New System.Windows.Forms.ComboBox
        Me.cboSearchBranch = New System.Windows.Forms.ComboBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtSearchLastName = New System.Windows.Forms.TextBox
        Me.txtSearchFirstName = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtSearchEmployeeID = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TCUserData.SuspendLayout()
        Me.TPUserInformation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TPPermission.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvUserAdminTool, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        Me.FileToolStripMenuItem.Visible = False
        '
        'ToolToolStripMenuItem
        '
        Me.ToolToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmOpenMaintenanceTool, Me.tsbViewOrgChart, Me.mmiViewPhoneList})
        Me.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem"
        Me.ToolToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.ToolToolStripMenuItem.Text = "Tool"
        '
        'tsmOpenMaintenanceTool
        '
        Me.tsmOpenMaintenanceTool.Name = "tsmOpenMaintenanceTool"
        Me.tsmOpenMaintenanceTool.Size = New System.Drawing.Size(241, 22)
        Me.tsmOpenMaintenanceTool.Text = "Open IAIP Organization Tool"
        '
        'tsbViewOrgChart
        '
        Me.tsbViewOrgChart.Name = "tsbViewOrgChart"
        Me.tsbViewOrgChart.Size = New System.Drawing.Size(241, 22)
        Me.tsbViewOrgChart.Text = "View Current Organization Chart"
        '
        'mmiViewPhoneList
        '
        Me.mmiViewPhoneList.Name = "mmiViewPhoneList"
        Me.mmiViewPhoneList.Size = New System.Drawing.Size(241, 22)
        Me.mmiViewPhoneList.Text = "View Current Phone List"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        Me.EditToolStripMenuItem.Visible = False
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpOnlineToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpOnlineToolStripMenuItem
        '
        Me.HelpOnlineToolStripMenuItem.Name = "HelpOnlineToolStripMenuItem"
        Me.HelpOnlineToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.HelpOnlineToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.HelpOnlineToolStripMenuItem.Text = "Help Online"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pnl1
        '
        Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(769, 17)
        Me.pnl1.Spring = True
        Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl2
        '
        Me.pnl2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(4, 17)
        '
        'pnl3
        '
        Me.pnl3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(4, 17)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbBack, Me.tsbClear})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        Me.tsbSave.Text = "ToolStripButton1"
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "ToolStripButton2"
        '
        'tsbClear
        '
        Me.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), System.Drawing.Image)
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.Text = "ToolStripButton1"
        '
        'lblFirstName
        '
        Me.lblFirstName.AutoSize = True
        Me.lblFirstName.Location = New System.Drawing.Point(2, 4)
        Me.lblFirstName.Name = "lblFirstName"
        Me.lblFirstName.Size = New System.Drawing.Size(60, 13)
        Me.lblFirstName.TabIndex = 3
        Me.lblFirstName.Text = "First Name:"
        '
        'lblLastName
        '
        Me.lblLastName.AutoSize = True
        Me.lblLastName.Location = New System.Drawing.Point(3, 21)
        Me.lblLastName.Name = "lblLastName"
        Me.lblLastName.Size = New System.Drawing.Size(61, 13)
        Me.lblLastName.TabIndex = 4
        Me.lblLastName.Text = "Last Name:"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(507, 21)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 6
        Me.lblPassword.Text = "Password:"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(507, 4)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(63, 13)
        Me.lblUserName.TabIndex = 7
        Me.lblUserName.Text = "User Name:"
        '
        'lblEmployeeID
        '
        Me.lblEmployeeID.AutoSize = True
        Me.lblEmployeeID.Location = New System.Drawing.Point(3, 38)
        Me.lblEmployeeID.Name = "lblEmployeeID"
        Me.lblEmployeeID.Size = New System.Drawing.Size(70, 13)
        Me.lblEmployeeID.TabIndex = 8
        Me.lblEmployeeID.Text = "Employee ID:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPermissions)
        Me.Panel1.Controls.Add(Me.lblUserID)
        Me.Panel1.Controls.Add(Me.lblFaxNumber)
        Me.Panel1.Controls.Add(Me.lblPhoneNumber)
        Me.Panel1.Controls.Add(Me.lblEmailAddress)
        Me.Panel1.Controls.Add(Me.btnCreateNewUser)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.lblFirstName)
        Me.Panel1.Controls.Add(Me.lblLastName)
        Me.Panel1.Controls.Add(Me.lblEmployeeID)
        Me.Panel1.Controls.Add(Me.lblPassword)
        Me.Panel1.Controls.Add(Me.lblUserName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(792, 87)
        Me.Panel1.TabIndex = 10
        '
        'lblPermissions
        '
        Me.lblPermissions.AutoSize = True
        Me.lblPermissions.Location = New System.Drawing.Point(507, 56)
        Me.lblPermissions.Name = "lblPermissions"
        Me.lblPermissions.Size = New System.Drawing.Size(0, 13)
        Me.lblPermissions.TabIndex = 36
        Me.lblPermissions.Visible = False
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Location = New System.Drawing.Point(507, 38)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(60, 13)
        Me.lblUserID.TabIndex = 35
        Me.lblUserID.Text = "numUserID"
        Me.lblUserID.Visible = False
        '
        'lblFaxNumber
        '
        Me.lblFaxNumber.AutoSize = True
        Me.lblFaxNumber.Location = New System.Drawing.Point(252, 38)
        Me.lblFaxNumber.Name = "lblFaxNumber"
        Me.lblFaxNumber.Size = New System.Drawing.Size(37, 13)
        Me.lblFaxNumber.TabIndex = 34
        Me.lblFaxNumber.Text = "Fax #:"
        '
        'lblPhoneNumber
        '
        Me.lblPhoneNumber.AutoSize = True
        Me.lblPhoneNumber.Location = New System.Drawing.Point(252, 21)
        Me.lblPhoneNumber.Name = "lblPhoneNumber"
        Me.lblPhoneNumber.Size = New System.Drawing.Size(51, 13)
        Me.lblPhoneNumber.TabIndex = 33
        Me.lblPhoneNumber.Text = "Phone #:"
        '
        'lblEmailAddress
        '
        Me.lblEmailAddress.AutoSize = True
        Me.lblEmailAddress.Location = New System.Drawing.Point(252, 4)
        Me.lblEmailAddress.Name = "lblEmailAddress"
        Me.lblEmailAddress.Size = New System.Drawing.Size(35, 13)
        Me.lblEmailAddress.TabIndex = 32
        Me.lblEmailAddress.Text = "Email:"
        '
        'btnCreateNewUser
        '
        Me.btnCreateNewUser.AutoSize = True
        Me.btnCreateNewUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCreateNewUser.Location = New System.Drawing.Point(113, 56)
        Me.btnCreateNewUser.Name = "btnCreateNewUser"
        Me.btnCreateNewUser.Size = New System.Drawing.Size(98, 23)
        Me.btnCreateNewUser.TabIndex = 31
        Me.btnCreateNewUser.Text = "Create New User"
        Me.btnCreateNewUser.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.AutoSize = True
        Me.btnSave.Location = New System.Drawing.Point(6, 56)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 23)
        Me.btnSave.TabIndex = 30
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.Blue
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 136)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel1.Controls.Add(Me.TCUserData)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 408)
        Me.SplitContainer1.SplitterDistance = 221
        Me.SplitContainer1.TabIndex = 11
        '
        'TCUserData
        '
        Me.TCUserData.Controls.Add(Me.TPUserInformation)
        Me.TCUserData.Controls.Add(Me.TPPermission)
        Me.TCUserData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCUserData.Location = New System.Drawing.Point(0, 0)
        Me.TCUserData.Name = "TCUserData"
        Me.TCUserData.SelectedIndex = 0
        Me.TCUserData.Size = New System.Drawing.Size(792, 221)
        Me.TCUserData.TabIndex = 0
        '
        'TPUserInformation
        '
        Me.TPUserInformation.Controls.Add(Me.Panel3)
        Me.TPUserInformation.Controls.Add(Me.txtOfficeNumber)
        Me.TPUserInformation.Controls.Add(Me.mtbPhoneNumber)
        Me.TPUserInformation.Controls.Add(Me.mtbFaxNumber)
        Me.TPUserInformation.Controls.Add(Me.cboUnit)
        Me.TPUserInformation.Controls.Add(Me.cboProgram)
        Me.TPUserInformation.Controls.Add(Me.cboBranch)
        Me.TPUserInformation.Controls.Add(Me.txtEmailAddress)
        Me.TPUserInformation.Controls.Add(Me.txtEmployeeID)
        Me.TPUserInformation.Controls.Add(Me.txtLastName)
        Me.TPUserInformation.Controls.Add(Me.txtFirstName)
        Me.TPUserInformation.Controls.Add(Me.Label16)
        Me.TPUserInformation.Controls.Add(Me.Label15)
        Me.TPUserInformation.Controls.Add(Me.Label14)
        Me.TPUserInformation.Controls.Add(Me.Label13)
        Me.TPUserInformation.Controls.Add(Me.Label12)
        Me.TPUserInformation.Controls.Add(Me.Label11)
        Me.TPUserInformation.Controls.Add(Me.Label10)
        Me.TPUserInformation.Controls.Add(Me.Label9)
        Me.TPUserInformation.Controls.Add(Me.Label8)
        Me.TPUserInformation.Controls.Add(Me.Label7)
        Me.TPUserInformation.Controls.Add(Me.Label3)
        Me.TPUserInformation.Location = New System.Drawing.Point(4, 22)
        Me.TPUserInformation.Name = "TPUserInformation"
        Me.TPUserInformation.Padding = New System.Windows.Forms.Padding(3)
        Me.TPUserInformation.Size = New System.Drawing.Size(784, 195)
        Me.TPUserInformation.TabIndex = 0
        Me.TPUserInformation.Text = "User Information"
        Me.TPUserInformation.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.Controls.Add(Me.rdbInactiveStatus)
        Me.Panel3.Controls.Add(Me.rdbActiveStatus)
        Me.Panel3.Location = New System.Drawing.Point(131, 87)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(128, 23)
        Me.Panel3.TabIndex = 26
        '
        'rdbInactiveStatus
        '
        Me.rdbInactiveStatus.AutoSize = True
        Me.rdbInactiveStatus.Location = New System.Drawing.Point(62, 3)
        Me.rdbInactiveStatus.Name = "rdbInactiveStatus"
        Me.rdbInactiveStatus.Size = New System.Drawing.Size(63, 17)
        Me.rdbInactiveStatus.TabIndex = 1
        Me.rdbInactiveStatus.Text = "Inactive"
        Me.rdbInactiveStatus.UseVisualStyleBackColor = True
        '
        'rdbActiveStatus
        '
        Me.rdbActiveStatus.AutoSize = True
        Me.rdbActiveStatus.Location = New System.Drawing.Point(3, 3)
        Me.rdbActiveStatus.Name = "rdbActiveStatus"
        Me.rdbActiveStatus.Size = New System.Drawing.Size(55, 17)
        Me.rdbActiveStatus.TabIndex = 0
        Me.rdbActiveStatus.Text = "Active"
        Me.rdbActiveStatus.UseVisualStyleBackColor = True
        '
        'txtOfficeNumber
        '
        Me.txtOfficeNumber.Location = New System.Drawing.Point(414, 88)
        Me.txtOfficeNumber.Name = "txtOfficeNumber"
        Me.txtOfficeNumber.Size = New System.Drawing.Size(181, 20)
        Me.txtOfficeNumber.TabIndex = 6
        '
        'mtbPhoneNumber
        '
        Me.mtbPhoneNumber.Location = New System.Drawing.Point(414, 34)
        Me.mtbPhoneNumber.Mask = "(999) 000-0000 ext.00000"
        Me.mtbPhoneNumber.Name = "mtbPhoneNumber"
        Me.mtbPhoneNumber.Size = New System.Drawing.Size(139, 20)
        Me.mtbPhoneNumber.TabIndex = 4
        '
        'mtbFaxNumber
        '
        Me.mtbFaxNumber.Location = New System.Drawing.Point(414, 60)
        Me.mtbFaxNumber.Mask = "(999) 000-0000 ext.00000"
        Me.mtbFaxNumber.Name = "mtbFaxNumber"
        Me.mtbFaxNumber.Size = New System.Drawing.Size(139, 20)
        Me.mtbFaxNumber.TabIndex = 5
        '
        'cboUnit
        '
        Me.cboUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUnit.FormattingEnabled = True
        Me.cboUnit.Location = New System.Drawing.Point(546, 121)
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.Size = New System.Drawing.Size(225, 21)
        Me.cboUnit.TabIndex = 9
        '
        'cboProgram
        '
        Me.cboProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProgram.FormattingEnabled = True
        Me.cboProgram.Location = New System.Drawing.Point(258, 121)
        Me.cboProgram.Name = "cboProgram"
        Me.cboProgram.Size = New System.Drawing.Size(250, 21)
        Me.cboProgram.TabIndex = 8
        '
        'cboBranch
        '
        Me.cboBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(54, 121)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(150, 21)
        Me.cboBranch.TabIndex = 7
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(414, 6)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(181, 20)
        Me.txtEmailAddress.TabIndex = 3
        '
        'txtEmployeeID
        '
        Me.txtEmployeeID.Location = New System.Drawing.Point(76, 60)
        Me.txtEmployeeID.Name = "txtEmployeeID"
        Me.txtEmployeeID.Size = New System.Drawing.Size(181, 20)
        Me.txtEmployeeID.TabIndex = 2
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(76, 34)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(180, 20)
        Me.txtLastName.TabIndex = 1
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(76, 6)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(180, 20)
        Me.txtFirstName.TabIndex = 0
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(335, 92)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 13)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Office #:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 92)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 13)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "State Employee Status:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(8, 125)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Branch:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(209, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Program:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(517, 125)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Unit:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Employee ID:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(335, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Email Address:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(335, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Phone #:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(335, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Fax #:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Last Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 10)
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
        Me.TPPermission.Controls.Add(Me.cboPermissionAccounts)
        Me.TPPermission.Controls.Add(Me.txtPassword)
        Me.TPPermission.Controls.Add(Me.cboPermissionBranch)
        Me.TPPermission.Controls.Add(Me.txtUserName)
        Me.TPPermission.Controls.Add(Me.Label21)
        Me.TPPermission.Controls.Add(Me.Label20)
        Me.TPPermission.Controls.Add(Me.Label17)
        Me.TPPermission.Controls.Add(Me.Label18)
        Me.TPPermission.Location = New System.Drawing.Point(4, 22)
        Me.TPPermission.Name = "TPPermission"
        Me.TPPermission.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPermission.Size = New System.Drawing.Size(784, 195)
        Me.TPPermission.TabIndex = 1
        Me.TPPermission.Text = "Permission"
        Me.TPPermission.UseVisualStyleBackColor = True
        '
        'cboPermissionProgram
        '
        Me.cboPermissionProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboPermissionProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPermissionProgram.FormattingEnabled = True
        Me.cboPermissionProgram.Location = New System.Drawing.Point(51, 84)
        Me.cboPermissionProgram.Name = "cboPermissionProgram"
        Me.cboPermissionProgram.Size = New System.Drawing.Size(215, 21)
        Me.cboPermissionProgram.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 88)
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
        Me.Panel4.TabIndex = 34
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
        Me.chb10.TabIndex = 38
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
        Me.chb9.TabIndex = 36
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
        Me.chb1.TabIndex = 15
        Me.chb1.UseVisualStyleBackColor = True
        Me.chb1.Visible = False
        '
        'chb2
        '
        Me.chb2.AutoSize = True
        Me.chb2.Location = New System.Drawing.Point(3, 22)
        Me.chb2.Name = "chb2"
        Me.chb2.Size = New System.Drawing.Size(15, 14)
        Me.chb2.TabIndex = 16
        Me.chb2.UseVisualStyleBackColor = True
        Me.chb2.Visible = False
        '
        'chb8
        '
        Me.chb8.AutoSize = True
        Me.chb8.Location = New System.Drawing.Point(3, 130)
        Me.chb8.Name = "chb8"
        Me.chb8.Size = New System.Drawing.Size(15, 14)
        Me.chb8.TabIndex = 22
        Me.chb8.UseVisualStyleBackColor = True
        Me.chb8.Visible = False
        '
        'chb3
        '
        Me.chb3.AutoSize = True
        Me.chb3.Location = New System.Drawing.Point(3, 40)
        Me.chb3.Name = "chb3"
        Me.chb3.Size = New System.Drawing.Size(15, 14)
        Me.chb3.TabIndex = 17
        Me.chb3.UseVisualStyleBackColor = True
        Me.chb3.Visible = False
        '
        'chb7
        '
        Me.chb7.AutoSize = True
        Me.chb7.Location = New System.Drawing.Point(3, 112)
        Me.chb7.Name = "chb7"
        Me.chb7.Size = New System.Drawing.Size(15, 14)
        Me.chb7.TabIndex = 21
        Me.chb7.UseVisualStyleBackColor = True
        Me.chb7.Visible = False
        '
        'chb4
        '
        Me.chb4.AutoSize = True
        Me.chb4.Location = New System.Drawing.Point(3, 58)
        Me.chb4.Name = "chb4"
        Me.chb4.Size = New System.Drawing.Size(15, 14)
        Me.chb4.TabIndex = 18
        Me.chb4.UseVisualStyleBackColor = True
        Me.chb4.Visible = False
        '
        'chb6
        '
        Me.chb6.AutoSize = True
        Me.chb6.Location = New System.Drawing.Point(3, 94)
        Me.chb6.Name = "chb6"
        Me.chb6.Size = New System.Drawing.Size(15, 14)
        Me.chb6.TabIndex = 20
        Me.chb6.UseVisualStyleBackColor = True
        Me.chb6.Visible = False
        '
        'chb5
        '
        Me.chb5.AutoSize = True
        Me.chb5.Location = New System.Drawing.Point(3, 76)
        Me.chb5.Name = "chb5"
        Me.chb5.Size = New System.Drawing.Size(15, 14)
        Me.chb5.TabIndex = 19
        Me.chb5.UseVisualStyleBackColor = True
        Me.chb5.Visible = False
        '
        'btnClearAllPermissions
        '
        Me.btnClearAllPermissions.AutoSize = True
        Me.btnClearAllPermissions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAllPermissions.Location = New System.Drawing.Point(51, 109)
        Me.btnClearAllPermissions.Name = "btnClearAllPermissions"
        Me.btnClearAllPermissions.Size = New System.Drawing.Size(113, 23)
        Me.btnClearAllPermissions.TabIndex = 33
        Me.btnClearAllPermissions.Text = "Clear All Permissions"
        Me.btnClearAllPermissions.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(532, 3)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(97, 13)
        Me.Label28.TabIndex = 20
        Me.Label28.Text = "Current Permission:"
        '
        'txtCurrentPermissions
        '
        Me.txtCurrentPermissions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrentPermissions.Location = New System.Drawing.Point(535, 19)
        Me.txtCurrentPermissions.Multiline = True
        Me.txtCurrentPermissions.Name = "txtCurrentPermissions"
        Me.txtCurrentPermissions.ReadOnly = True
        Me.txtCurrentPermissions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCurrentPermissions.Size = New System.Drawing.Size(241, 170)
        Me.txtCurrentPermissions.TabIndex = 19
        '
        'cboPermissionAccounts
        '
        Me.cboPermissionAccounts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboPermissionAccounts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPermissionAccounts.FormattingEnabled = True
        Me.cboPermissionAccounts.Location = New System.Drawing.Point(68, 133)
        Me.cboPermissionAccounts.Name = "cboPermissionAccounts"
        Me.cboPermissionAccounts.Size = New System.Drawing.Size(26, 21)
        Me.cboPermissionAccounts.TabIndex = 14
        Me.cboPermissionAccounts.Visible = False
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(100, 32)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(125, 20)
        Me.txtPassword.TabIndex = 11
        '
        'cboPermissionBranch
        '
        Me.cboPermissionBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboPermissionBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPermissionBranch.FormattingEnabled = True
        Me.cboPermissionBranch.Location = New System.Drawing.Point(49, 58)
        Me.cboPermissionBranch.Name = "cboPermissionBranch"
        Me.cboPermissionBranch.Size = New System.Drawing.Size(217, 21)
        Me.cboPermissionBranch.TabIndex = 12
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(65, 6)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(160, 20)
        Me.txtUserName.TabIndex = 10
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(3, 62)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(44, 13)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Branch:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(17, 136)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(55, 13)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "Accounts:"
        Me.Label20.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(38, 35)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 13)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "Password:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 13)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "User Name:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblCount)
        Me.Panel2.Controls.Add(Me.dgvUserAdminTool)
        Me.Panel2.Controls.Add(Me.btnAll)
        Me.Panel2.Controls.Add(Me.btnReset)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.cboSearchUnit)
        Me.Panel2.Controls.Add(Me.cboSearchProgram)
        Me.Panel2.Controls.Add(Me.cboSearchBranch)
        Me.Panel2.Controls.Add(Me.Label25)
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.Label27)
        Me.Panel2.Controls.Add(Me.txtSearchLastName)
        Me.Panel2.Controls.Add(Me.txtSearchFirstName)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.txtSearchEmployeeID)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(792, 183)
        Me.Panel2.TabIndex = 0
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(732, 11)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 13)
        Me.lblCount.TabIndex = 27
        '
        'dgvUserAdminTool
        '
        Me.dgvUserAdminTool.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUserAdminTool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUserAdminTool.Location = New System.Drawing.Point(0, 61)
        Me.dgvUserAdminTool.Name = "dgvUserAdminTool"
        Me.dgvUserAdminTool.ReadOnly = True
        Me.dgvUserAdminTool.Size = New System.Drawing.Size(792, 121)
        Me.dgvUserAdminTool.TabIndex = 32
        '
        'btnAll
        '
        Me.btnAll.AutoSize = True
        Me.btnAll.Location = New System.Drawing.Point(667, 6)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(58, 23)
        Me.btnAll.TabIndex = 31
        Me.btnAll.Text = "Show All"
        Me.btnAll.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.AutoSize = True
        Me.btnReset.Location = New System.Drawing.Point(593, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(58, 23)
        Me.btnReset.TabIndex = 30
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.AutoSize = True
        Me.btnSearch.Location = New System.Drawing.Point(519, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(58, 23)
        Me.btnSearch.TabIndex = 29
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboSearchUnit
        '
        Me.cboSearchUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchUnit.FormattingEnabled = True
        Me.cboSearchUnit.Location = New System.Drawing.Point(543, 34)
        Me.cboSearchUnit.Name = "cboSearchUnit"
        Me.cboSearchUnit.Size = New System.Drawing.Size(225, 21)
        Me.cboSearchUnit.TabIndex = 27
        '
        'cboSearchProgram
        '
        Me.cboSearchProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchProgram.FormattingEnabled = True
        Me.cboSearchProgram.Location = New System.Drawing.Point(255, 34)
        Me.cboSearchProgram.Name = "cboSearchProgram"
        Me.cboSearchProgram.Size = New System.Drawing.Size(250, 21)
        Me.cboSearchProgram.TabIndex = 26
        '
        'cboSearchBranch
        '
        Me.cboSearchBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchBranch.FormattingEnabled = True
        Me.cboSearchBranch.Location = New System.Drawing.Point(55, 34)
        Me.cboSearchBranch.Name = "cboSearchBranch"
        Me.cboSearchBranch.Size = New System.Drawing.Size(150, 21)
        Me.cboSearchBranch.TabIndex = 25
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(9, 38)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(44, 13)
        Me.Label25.TabIndex = 24
        Me.Label25.Text = "Branch:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(206, 38)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(49, 13)
        Me.Label26.TabIndex = 23
        Me.Label26.Text = "Program:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(514, 38)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(29, 13)
        Me.Label27.TabIndex = 22
        Me.Label27.Text = "Unit:"
        '
        'txtSearchLastName
        '
        Me.txtSearchLastName.Location = New System.Drawing.Point(231, 7)
        Me.txtSearchLastName.Name = "txtSearchLastName"
        Me.txtSearchLastName.Size = New System.Drawing.Size(100, 20)
        Me.txtSearchLastName.TabIndex = 18
        '
        'txtSearchFirstName
        '
        Me.txtSearchFirstName.Location = New System.Drawing.Point(405, 7)
        Me.txtSearchFirstName.Name = "txtSearchFirstName"
        Me.txtSearchFirstName.Size = New System.Drawing.Size(100, 20)
        Me.txtSearchFirstName.TabIndex = 17
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(343, 11)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(60, 13)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "First Name:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(167, 11)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(61, 13)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Last Name:"
        '
        'txtSearchEmployeeID
        '
        Me.txtSearchEmployeeID.Location = New System.Drawing.Point(78, 7)
        Me.txtSearchEmployeeID.Name = "txtSearchEmployeeID"
        Me.txtSearchEmployeeID.Size = New System.Drawing.Size(83, 20)
        Me.txtSearchEmployeeID.TabIndex = 14
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 11)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(70, 13)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "Employee ID:"
        '
        'IAIPUserAdminTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "IAIPUserAdminTool"
        Me.Text = "IAIP Profile Management"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TCUserData.ResumeLayout(False)
        Me.TPUserInformation.ResumeLayout(False)
        Me.TPUserInformation.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TPPermission.ResumeLayout(False)
        Me.TPPermission.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvUserAdminTool, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblFirstName As System.Windows.Forms.Label
    Friend WithEvents lblLastName As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblEmployeeID As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TCUserData As System.Windows.Forms.TabControl
    Friend WithEvents TPUserInformation As System.Windows.Forms.TabPage
    Friend WithEvents TPPermission As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtEmployeeID As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdbInactiveStatus As System.Windows.Forms.RadioButton
    Friend WithEvents rdbActiveStatus As System.Windows.Forms.RadioButton
    Friend WithEvents txtOfficeNumber As System.Windows.Forms.TextBox
    Friend WithEvents mtbPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cboUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cboProgram As System.Windows.Forms.ComboBox
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents chb1 As System.Windows.Forms.CheckBox
    Friend WithEvents cboPermissionBranch As System.Windows.Forms.ComboBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cboPermissionAccounts As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchEmployeeID As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
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
    Friend WithEvents btnAll As System.Windows.Forms.Button
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
    Friend WithEvents lblFaxNumber As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents lblEmailAddress As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents cboPermissionProgram As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmOpenMaintenanceTool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl7 As System.Windows.Forms.Label
    Friend WithEvents lbl8 As System.Windows.Forms.Label
    Friend WithEvents lbl2 As System.Windows.Forms.Label
    Friend WithEvents lbl3 As System.Windows.Forms.Label
    Friend WithEvents lbl4 As System.Windows.Forms.Label
    Friend WithEvents lbl5 As System.Windows.Forms.Label
    Friend WithEvents lbl6 As System.Windows.Forms.Label
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents lblPermissions As System.Windows.Forms.Label
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents HelpOnlineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbViewOrgChart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiViewPhoneList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl10 As System.Windows.Forms.Label
    Friend WithEvents chb10 As System.Windows.Forms.CheckBox
    Friend WithEvents lbl9 As System.Windows.Forms.Label
    Friend WithEvents chb9 As System.Windows.Forms.CheckBox
End Class
