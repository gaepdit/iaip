<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPWebApplicationUser
    Inherits DefaultForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPWebApplicationUser))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbClear = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPWebUsers = New System.Windows.Forms.TabPage
        Me.pnlUser = New System.Windows.Forms.Panel
        Me.cboUsers = New System.Windows.Forms.ComboBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.Label44 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.btnAddUser = New System.Windows.Forms.Button
        Me.dgrUsers = New System.Windows.Forms.DataGrid
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.cboFacilityName = New System.Windows.Forms.ComboBox
        Me.cboAirsNo = New System.Windows.Forms.ComboBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.llbViewAll = New System.Windows.Forms.LinkLabel
        Me.Label = New System.Windows.Forms.Label
        Me.btnActivateTool = New System.Windows.Forms.Button
        Me.TPWebUsers1 = New System.Windows.Forms.TabPage
        Me.pnlUserInfo = New System.Windows.Forms.Panel
        Me.lblCityStateZip = New System.Windows.Forms.Label
        Me.lblAddress = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblFaxNo = New System.Windows.Forms.Label
        Me.lblPhoneNo = New System.Windows.Forms.Label
        Me.lblCoName = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.lblLName = New System.Windows.Forms.Label
        Me.lblFName = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.pnlUserFacility = New System.Windows.Forms.Panel
        Me.cboFacilityToDelete = New System.Windows.Forms.ComboBox
        Me.Label75 = New System.Windows.Forms.Label
        Me.btnDeleteFacilityUser = New System.Windows.Forms.Button
        Me.cboFacilityToAdd = New System.Windows.Forms.ComboBox
        Me.btnUpdateUser = New System.Windows.Forms.Button
        Me.Label53 = New System.Windows.Forms.Label
        Me.btnAddFacilitytoUser = New System.Windows.Forms.Button
        Me.dgrFacilities = New System.Windows.Forms.DataGrid
        Me.pnlUserEmail = New System.Windows.Forms.Panel
        Me.cboUserEmail = New System.Windows.Forms.ComboBox
        Me.lblViewFacility = New System.Windows.Forms.LinkLabel
        Me.Label52 = New System.Windows.Forms.Label
        Me.btnActivateEmail = New System.Windows.Forms.Button
        Me.TPActivate = New System.Windows.Forms.TabPage
        Me.btnActivateUser = New System.Windows.Forms.Button
        Me.Label54 = New System.Windows.Forms.Label
        Me.txtEmailAddress = New System.Windows.Forms.TextBox
        Me.TPFeeFacility = New System.Windows.Forms.TabPage
        Me.btnRemoveFacility = New System.Windows.Forms.Button
        Me.Label74 = New System.Windows.Forms.Label
        Me.txtYear = New System.Windows.Forms.TextBox
        Me.Label73 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label72 = New System.Windows.Forms.Label
        Me.btnAddFacility = New System.Windows.Forms.Button
        Me.txtAirsNo = New System.Windows.Forms.TextBox
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TPWebUsers.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        CType(Me.dgrUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFacility.SuspendLayout()
        Me.TPWebUsers1.SuspendLayout()
        Me.pnlUserInfo.SuspendLayout()
        Me.pnlUserFacility.SuspendLayout()
        CType(Me.dgrFacilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUserEmail.SuspendLayout()
        Me.TPActivate.SuspendLayout()
        Me.TPFeeFacility.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClear, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(964, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "ToolStripButton2"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 602)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(964, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(941, 17)
        Me.Panel1.Spring = True
        Me.Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 17)
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 17)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPWebUsers)
        Me.TabControl1.Controls.Add(Me.TPWebUsers1)
        Me.TabControl1.Controls.Add(Me.TPActivate)
        Me.TabControl1.Controls.Add(Me.TPFeeFacility)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(964, 577)
        Me.TabControl1.TabIndex = 149
        '
        'TPWebUsers
        '
        Me.TPWebUsers.Controls.Add(Me.pnlUser)
        Me.TPWebUsers.Controls.Add(Me.PanelFacility)
        Me.TPWebUsers.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers.Name = "TPWebUsers"
        Me.TPWebUsers.Size = New System.Drawing.Size(956, 551)
        Me.TPWebUsers.TabIndex = 1
        Me.TPWebUsers.Text = "Web App Users - Facility"
        Me.TPWebUsers.UseVisualStyleBackColor = True
        '
        'pnlUser
        '
        Me.pnlUser.Controls.Add(Me.cboUsers)
        Me.pnlUser.Controls.Add(Me.Label40)
        Me.pnlUser.Controls.Add(Me.btnDelete)
        Me.pnlUser.Controls.Add(Me.btnUpdate)
        Me.pnlUser.Controls.Add(Me.Label44)
        Me.pnlUser.Controls.Add(Me.txtEmail)
        Me.pnlUser.Controls.Add(Me.btnAddUser)
        Me.pnlUser.Controls.Add(Me.dgrUsers)
        Me.pnlUser.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUser.Location = New System.Drawing.Point(0, 55)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(956, 278)
        Me.pnlUser.TabIndex = 147
        Me.pnlUser.Visible = False
        '
        'cboUsers
        '
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(166, 227)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(200, 21)
        Me.cboUsers.TabIndex = 281
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(6, 233)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(150, 13)
        Me.Label40.TabIndex = 280
        Me.Label40.Text = "Delete a User for this Facility:  "
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(371, 228)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 20)
        Me.btnDelete.TabIndex = 278
        Me.btnDelete.Text = "Delete User"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(2, 167)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdate.TabIndex = 277
        Me.btnUpdate.Text = "Save Changes"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(6, 201)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(132, 13)
        Me.Label44.TabIndex = 276
        Me.Label44.Text = "Add a User to this Facility: "
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(166, 197)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(200, 20)
        Me.txtEmail.TabIndex = 275
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(371, 196)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(62, 20)
        Me.btnAddUser.TabIndex = 274
        Me.btnAddUser.Text = "Add User"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'dgrUsers
        '
        Me.dgrUsers.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgrUsers.BackColor = System.Drawing.Color.GhostWhite
        Me.dgrUsers.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgrUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgrUsers.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgrUsers.CaptionForeColor = System.Drawing.Color.White
        Me.dgrUsers.CaptionText = "Current Users for this Facility"
        Me.dgrUsers.DataMember = ""
        Me.dgrUsers.FlatMode = True
        Me.dgrUsers.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgrUsers.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrUsers.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgrUsers.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgrUsers.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgrUsers.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgrUsers.LinkColor = System.Drawing.Color.Teal
        Me.dgrUsers.Location = New System.Drawing.Point(2, 5)
        Me.dgrUsers.Name = "dgrUsers"
        Me.dgrUsers.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgrUsers.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrUsers.ReadOnly = True
        Me.dgrUsers.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgrUsers.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgrUsers.Size = New System.Drawing.Size(709, 157)
        Me.dgrUsers.TabIndex = 273
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.cboFacilityName)
        Me.PanelFacility.Controls.Add(Me.cboAirsNo)
        Me.PanelFacility.Controls.Add(Me.Label39)
        Me.PanelFacility.Controls.Add(Me.llbViewAll)
        Me.PanelFacility.Controls.Add(Me.Label)
        Me.PanelFacility.Controls.Add(Me.btnActivateTool)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(956, 55)
        Me.PanelFacility.TabIndex = 146
        '
        'cboFacilityName
        '
        Me.cboFacilityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityName.Location = New System.Drawing.Point(92, 28)
        Me.cboFacilityName.Name = "cboFacilityName"
        Me.cboFacilityName.Size = New System.Drawing.Size(215, 21)
        Me.cboFacilityName.TabIndex = 1
        '
        'cboAirsNo
        '
        Me.cboAirsNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirsNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirsNo.Location = New System.Drawing.Point(410, 28)
        Me.cboAirsNo.Name = "cboAirsNo"
        Me.cboAirsNo.Size = New System.Drawing.Size(90, 21)
        Me.cboAirsNo.TabIndex = 2
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(307, 28)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(94, 13)
        Me.Label39.TabIndex = 107
        Me.Label39.Text = "OR AIRS Number:"
        '
        'llbViewAll
        '
        Me.llbViewAll.AutoSize = True
        Me.llbViewAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbViewAll.Location = New System.Drawing.Point(507, 28)
        Me.llbViewAll.Name = "llbViewAll"
        Me.llbViewAll.Size = New System.Drawing.Size(56, 13)
        Me.llbViewAll.TabIndex = 143
        Me.llbViewAll.TabStop = True
        Me.llbViewAll.Text = "View Data"
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label.Location = New System.Drawing.Point(7, 28)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(73, 13)
        Me.Label.TabIndex = 106
        Me.Label.Text = "Facility Name:"
        Me.Label.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'btnActivateTool
        '
        Me.btnActivateTool.AutoSize = True
        Me.btnActivateTool.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnActivateTool.BackColor = System.Drawing.Color.YellowGreen
        Me.btnActivateTool.Location = New System.Drawing.Point(0, 0)
        Me.btnActivateTool.Name = "btnActivateTool"
        Me.btnActivateTool.Size = New System.Drawing.Size(116, 23)
        Me.btnActivateTool.TabIndex = 174
        Me.btnActivateTool.Text = "Refresh Facilities List"
        Me.btnActivateTool.UseVisualStyleBackColor = False
        '
        'TPWebUsers1
        '
        Me.TPWebUsers1.Controls.Add(Me.pnlUserInfo)
        Me.TPWebUsers1.Controls.Add(Me.pnlUserFacility)
        Me.TPWebUsers1.Controls.Add(Me.pnlUserEmail)
        Me.TPWebUsers1.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers1.Name = "TPWebUsers1"
        Me.TPWebUsers1.Size = New System.Drawing.Size(956, 551)
        Me.TPWebUsers1.TabIndex = 2
        Me.TPWebUsers1.Text = "Web App Users - Email"
        Me.TPWebUsers1.UseVisualStyleBackColor = True
        '
        'pnlUserInfo
        '
        Me.pnlUserInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlUserInfo.Controls.Add(Me.lblCityStateZip)
        Me.pnlUserInfo.Controls.Add(Me.lblAddress)
        Me.pnlUserInfo.Controls.Add(Me.Label17)
        Me.pnlUserInfo.Controls.Add(Me.lblFaxNo)
        Me.pnlUserInfo.Controls.Add(Me.lblPhoneNo)
        Me.pnlUserInfo.Controls.Add(Me.lblCoName)
        Me.pnlUserInfo.Controls.Add(Me.lblTitle)
        Me.pnlUserInfo.Controls.Add(Me.lblLName)
        Me.pnlUserInfo.Controls.Add(Me.lblFName)
        Me.pnlUserInfo.Controls.Add(Me.Label6)
        Me.pnlUserInfo.Location = New System.Drawing.Point(0, 327)
        Me.pnlUserInfo.Name = "pnlUserInfo"
        Me.pnlUserInfo.Size = New System.Drawing.Size(863, 212)
        Me.pnlUserInfo.TabIndex = 150
        Me.pnlUserInfo.Visible = False
        '
        'lblCityStateZip
        '
        Me.lblCityStateZip.AutoSize = True
        Me.lblCityStateZip.Location = New System.Drawing.Point(4, 193)
        Me.lblCityStateZip.Name = "lblCityStateZip"
        Me.lblCityStateZip.Size = New System.Drawing.Size(0, 13)
        Me.lblCityStateZip.TabIndex = 9
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(4, 172)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(0, 13)
        Me.lblAddress.TabIndex = 8
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 151)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Address:"
        '
        'lblFaxNo
        '
        Me.lblFaxNo.AutoSize = True
        Me.lblFaxNo.Location = New System.Drawing.Point(4, 130)
        Me.lblFaxNo.Name = "lblFaxNo"
        Me.lblFaxNo.Size = New System.Drawing.Size(0, 13)
        Me.lblFaxNo.TabIndex = 6
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.Location = New System.Drawing.Point(4, 109)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(0, 13)
        Me.lblPhoneNo.TabIndex = 5
        '
        'lblCoName
        '
        Me.lblCoName.AutoSize = True
        Me.lblCoName.Location = New System.Drawing.Point(4, 88)
        Me.lblCoName.Name = "lblCoName"
        Me.lblCoName.Size = New System.Drawing.Size(0, 13)
        Me.lblCoName.TabIndex = 4
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(4, 67)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(0, 13)
        Me.lblTitle.TabIndex = 3
        '
        'lblLName
        '
        Me.lblLName.AutoSize = True
        Me.lblLName.Location = New System.Drawing.Point(4, 46)
        Me.lblLName.Name = "lblLName"
        Me.lblLName.Size = New System.Drawing.Size(0, 13)
        Me.lblLName.TabIndex = 2
        '
        'lblFName
        '
        Me.lblFName.AutoSize = True
        Me.lblFName.Location = New System.Drawing.Point(4, 25)
        Me.lblFName.Name = "lblFName"
        Me.lblFName.Size = New System.Drawing.Size(0, 13)
        Me.lblFName.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Above User's Details"
        '
        'pnlUserFacility
        '
        Me.pnlUserFacility.Controls.Add(Me.cboFacilityToDelete)
        Me.pnlUserFacility.Controls.Add(Me.Label75)
        Me.pnlUserFacility.Controls.Add(Me.btnDeleteFacilityUser)
        Me.pnlUserFacility.Controls.Add(Me.cboFacilityToAdd)
        Me.pnlUserFacility.Controls.Add(Me.btnUpdateUser)
        Me.pnlUserFacility.Controls.Add(Me.Label53)
        Me.pnlUserFacility.Controls.Add(Me.btnAddFacilitytoUser)
        Me.pnlUserFacility.Controls.Add(Me.dgrFacilities)
        Me.pnlUserFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserFacility.Location = New System.Drawing.Point(0, 55)
        Me.pnlUserFacility.Name = "pnlUserFacility"
        Me.pnlUserFacility.Size = New System.Drawing.Size(956, 266)
        Me.pnlUserFacility.TabIndex = 148
        Me.pnlUserFacility.Visible = False
        '
        'cboFacilityToDelete
        '
        Me.cboFacilityToDelete.FormattingEnabled = True
        Me.cboFacilityToDelete.Location = New System.Drawing.Point(176, 224)
        Me.cboFacilityToDelete.Name = "cboFacilityToDelete"
        Me.cboFacilityToDelete.Size = New System.Drawing.Size(215, 21)
        Me.cboFacilityToDelete.TabIndex = 284
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(6, 230)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(150, 13)
        Me.Label75.TabIndex = 283
        Me.Label75.Text = "Delete a Facility for this User:  "
        '
        'btnDeleteFacilityUser
        '
        Me.btnDeleteFacilityUser.AutoSize = True
        Me.btnDeleteFacilityUser.Location = New System.Drawing.Point(403, 225)
        Me.btnDeleteFacilityUser.Name = "btnDeleteFacilityUser"
        Me.btnDeleteFacilityUser.Size = New System.Drawing.Size(151, 23)
        Me.btnDeleteFacilityUser.TabIndex = 282
        Me.btnDeleteFacilityUser.Text = "Remove Facility for this User"
        Me.btnDeleteFacilityUser.UseVisualStyleBackColor = True
        '
        'cboFacilityToAdd
        '
        Me.cboFacilityToAdd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityToAdd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityToAdd.Location = New System.Drawing.Point(176, 198)
        Me.cboFacilityToAdd.Name = "cboFacilityToAdd"
        Me.cboFacilityToAdd.Size = New System.Drawing.Size(215, 21)
        Me.cboFacilityToAdd.TabIndex = 278
        '
        'btnUpdateUser
        '
        Me.btnUpdateUser.Location = New System.Drawing.Point(2, 167)
        Me.btnUpdateUser.Name = "btnUpdateUser"
        Me.btnUpdateUser.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdateUser.TabIndex = 277
        Me.btnUpdateUser.Text = "Save Changes"
        Me.btnUpdateUser.UseVisualStyleBackColor = True
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(6, 201)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(164, 13)
        Me.Label53.TabIndex = 276
        Me.Label53.Text = "Add a Facility to the above User: "
        '
        'btnAddFacilitytoUser
        '
        Me.btnAddFacilitytoUser.AutoSize = True
        Me.btnAddFacilitytoUser.Location = New System.Drawing.Point(403, 196)
        Me.btnAddFacilitytoUser.Name = "btnAddFacilitytoUser"
        Me.btnAddFacilitytoUser.Size = New System.Drawing.Size(77, 23)
        Me.btnAddFacilitytoUser.TabIndex = 274
        Me.btnAddFacilitytoUser.Text = "Add Facility"
        Me.btnAddFacilitytoUser.UseVisualStyleBackColor = True
        '
        'dgrFacilities
        '
        Me.dgrFacilities.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgrFacilities.BackColor = System.Drawing.Color.GhostWhite
        Me.dgrFacilities.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgrFacilities.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgrFacilities.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgrFacilities.CaptionForeColor = System.Drawing.Color.White
        Me.dgrFacilities.CaptionText = "Current Facilities for this User"
        Me.dgrFacilities.DataMember = ""
        Me.dgrFacilities.FlatMode = True
        Me.dgrFacilities.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgrFacilities.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrFacilities.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgrFacilities.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgrFacilities.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgrFacilities.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgrFacilities.LinkColor = System.Drawing.Color.Teal
        Me.dgrFacilities.Location = New System.Drawing.Point(2, 5)
        Me.dgrFacilities.Name = "dgrFacilities"
        Me.dgrFacilities.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgrFacilities.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrFacilities.ReadOnly = True
        Me.dgrFacilities.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgrFacilities.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgrFacilities.Size = New System.Drawing.Size(709, 157)
        Me.dgrFacilities.TabIndex = 273
        '
        'pnlUserEmail
        '
        Me.pnlUserEmail.Controls.Add(Me.cboUserEmail)
        Me.pnlUserEmail.Controls.Add(Me.lblViewFacility)
        Me.pnlUserEmail.Controls.Add(Me.Label52)
        Me.pnlUserEmail.Controls.Add(Me.btnActivateEmail)
        Me.pnlUserEmail.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserEmail.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserEmail.Name = "pnlUserEmail"
        Me.pnlUserEmail.Size = New System.Drawing.Size(956, 55)
        Me.pnlUserEmail.TabIndex = 147
        '
        'cboUserEmail
        '
        Me.cboUserEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUserEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUserEmail.Location = New System.Drawing.Point(105, 28)
        Me.cboUserEmail.Name = "cboUserEmail"
        Me.cboUserEmail.Size = New System.Drawing.Size(244, 21)
        Me.cboUserEmail.TabIndex = 1
        '
        'lblViewFacility
        '
        Me.lblViewFacility.AutoSize = True
        Me.lblViewFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewFacility.Location = New System.Drawing.Point(355, 28)
        Me.lblViewFacility.Name = "lblViewFacility"
        Me.lblViewFacility.Size = New System.Drawing.Size(56, 13)
        Me.lblViewFacility.TabIndex = 143
        Me.lblViewFacility.TabStop = True
        Me.lblViewFacility.Text = "View Data"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(7, 28)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(101, 13)
        Me.Label52.TabIndex = 106
        Me.Label52.Text = "User Email Address:"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'btnActivateEmail
        '
        Me.btnActivateEmail.AutoSize = True
        Me.btnActivateEmail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnActivateEmail.BackColor = System.Drawing.Color.YellowGreen
        Me.btnActivateEmail.Location = New System.Drawing.Point(0, 0)
        Me.btnActivateEmail.Name = "btnActivateEmail"
        Me.btnActivateEmail.Size = New System.Drawing.Size(134, 23)
        Me.btnActivateEmail.TabIndex = 174
        Me.btnActivateEmail.Text = "Refresh Email Addresses"
        Me.btnActivateEmail.UseVisualStyleBackColor = False
        '
        'TPActivate
        '
        Me.TPActivate.Controls.Add(Me.btnActivateUser)
        Me.TPActivate.Controls.Add(Me.Label54)
        Me.TPActivate.Controls.Add(Me.txtEmailAddress)
        Me.TPActivate.Location = New System.Drawing.Point(4, 22)
        Me.TPActivate.Name = "TPActivate"
        Me.TPActivate.Size = New System.Drawing.Size(956, 551)
        Me.TPActivate.TabIndex = 3
        Me.TPActivate.Text = "Activate User Account"
        Me.TPActivate.UseVisualStyleBackColor = True
        '
        'btnActivateUser
        '
        Me.btnActivateUser.AutoSize = True
        Me.btnActivateUser.Location = New System.Drawing.Point(361, 11)
        Me.btnActivateUser.Name = "btnActivateUser"
        Me.btnActivateUser.Size = New System.Drawing.Size(61, 23)
        Me.btnActivateUser.TabIndex = 2
        Me.btnActivateUser.Text = "Activate"
        Me.btnActivateUser.UseVisualStyleBackColor = True
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(4, 14)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(101, 13)
        Me.Label54.TabIndex = 1
        Me.Label54.Text = "User Email Address:"
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(111, 14)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(244, 20)
        Me.txtEmailAddress.TabIndex = 0
        '
        'TPFeeFacility
        '
        Me.TPFeeFacility.Controls.Add(Me.btnRemoveFacility)
        Me.TPFeeFacility.Controls.Add(Me.Label74)
        Me.TPFeeFacility.Controls.Add(Me.txtYear)
        Me.TPFeeFacility.Controls.Add(Me.Label73)
        Me.TPFeeFacility.Controls.Add(Me.Label50)
        Me.TPFeeFacility.Controls.Add(Me.Label72)
        Me.TPFeeFacility.Controls.Add(Me.btnAddFacility)
        Me.TPFeeFacility.Controls.Add(Me.txtAirsNo)
        Me.TPFeeFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeFacility.Name = "TPFeeFacility"
        Me.TPFeeFacility.Size = New System.Drawing.Size(956, 551)
        Me.TPFeeFacility.TabIndex = 4
        Me.TPFeeFacility.Text = "Add/Remove Fee Facility"
        Me.TPFeeFacility.UseVisualStyleBackColor = True
        '
        'btnRemoveFacility
        '
        Me.btnRemoveFacility.AutoSize = True
        Me.btnRemoveFacility.Location = New System.Drawing.Point(447, 25)
        Me.btnRemoveFacility.Name = "btnRemoveFacility"
        Me.btnRemoveFacility.Size = New System.Drawing.Size(155, 23)
        Me.btnRemoveFacility.TabIndex = 7
        Me.btnRemoveFacility.Text = "Remove Facility from Fee List"
        Me.btnRemoveFacility.UseVisualStyleBackColor = True
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(222, 30)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(35, 13)
        Me.Label74.TabIndex = 6
        Me.Label74.Text = "Year: "
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(260, 27)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(52, 20)
        Me.txtYear.TabIndex = 5
        Me.txtYear.Text = "2006"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(85, 8)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(100, 13)
        Me.Label73.TabIndex = 4
        Me.Label73.Text = "Ex: 041300100001 "
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(257, 11)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(49, 13)
        Me.Label50.TabIndex = 3
        Me.Label50.Text = "Ex: 2006"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(4, 29)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(78, 13)
        Me.Label72.TabIndex = 2
        Me.Label72.Text = "AIRS Number: "
        '
        'btnAddFacility
        '
        Me.btnAddFacility.AutoSize = True
        Me.btnAddFacility.Location = New System.Drawing.Point(318, 24)
        Me.btnAddFacility.Name = "btnAddFacility"
        Me.btnAddFacility.Size = New System.Drawing.Size(123, 23)
        Me.btnAddFacility.TabIndex = 1
        Me.btnAddFacility.Text = "Add Facility to Fee List"
        Me.btnAddFacility.UseVisualStyleBackColor = True
        '
        'txtAirsNo
        '
        Me.txtAirsNo.Location = New System.Drawing.Point(88, 27)
        Me.txtAirsNo.Name = "txtAirsNo"
        Me.txtAirsNo.Size = New System.Drawing.Size(124, 20)
        Me.txtAirsNo.TabIndex = 0
        Me.txtAirsNo.Text = "0413"
        '
        'PASPWebApplicationUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 624)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "PASPWebApplicationUser"
        Me.Text = "Web Application User"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TPWebUsers.ResumeLayout(False)
        Me.pnlUser.ResumeLayout(False)
        Me.pnlUser.PerformLayout()
        CType(Me.dgrUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.TPWebUsers1.ResumeLayout(False)
        Me.pnlUserInfo.ResumeLayout(False)
        Me.pnlUserInfo.PerformLayout()
        Me.pnlUserFacility.ResumeLayout(False)
        Me.pnlUserFacility.PerformLayout()
        CType(Me.dgrFacilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUserEmail.ResumeLayout(False)
        Me.pnlUserEmail.PerformLayout()
        Me.TPActivate.ResumeLayout(False)
        Me.TPActivate.PerformLayout()
        Me.TPFeeFacility.ResumeLayout(False)
        Me.TPFeeFacility.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPWebUsers As System.Windows.Forms.TabPage
    Friend WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents cboUsers As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents dgrUsers As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents cboFacilityName As System.Windows.Forms.ComboBox
    Friend WithEvents cboAirsNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents llbViewAll As System.Windows.Forms.LinkLabel
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents btnActivateTool As System.Windows.Forms.Button
    Friend WithEvents TPWebUsers1 As System.Windows.Forms.TabPage
    Friend WithEvents pnlUserFacility As System.Windows.Forms.Panel
    Friend WithEvents cboFacilityToDelete As System.Windows.Forms.ComboBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteFacilityUser As System.Windows.Forms.Button
    Friend WithEvents cboFacilityToAdd As System.Windows.Forms.ComboBox
    Friend WithEvents btnUpdateUser As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents btnAddFacilitytoUser As System.Windows.Forms.Button
    Friend WithEvents dgrFacilities As System.Windows.Forms.DataGrid
    Friend WithEvents pnlUserEmail As System.Windows.Forms.Panel
    Friend WithEvents cboUserEmail As System.Windows.Forms.ComboBox
    Friend WithEvents lblViewFacility As System.Windows.Forms.LinkLabel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnActivateEmail As System.Windows.Forms.Button
    Friend WithEvents TPActivate As System.Windows.Forms.TabPage
    Friend WithEvents btnActivateUser As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents TPFeeFacility As System.Windows.Forms.TabPage
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents btnAddFacility As System.Windows.Forms.Button
    Friend WithEvents txtAirsNo As System.Windows.Forms.TextBox
    Friend WithEvents btnRemoveFacility As System.Windows.Forms.Button
    Friend WithEvents pnlUserInfo As System.Windows.Forms.Panel
    Friend WithEvents lblCityStateZip As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblFaxNo As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents lblCoName As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblLName As System.Windows.Forms.Label
    Friend WithEvents lblFName As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
