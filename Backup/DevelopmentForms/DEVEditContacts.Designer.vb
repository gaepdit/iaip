<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DEVEditContacts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DEVEditContacts))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.tsbClear = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtContactId = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chbDistrictContact = New System.Windows.Forms.CheckBox
        Me.chbAmbientContact = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chbFeeContact = New System.Windows.Forms.CheckBox
        Me.chbGECOContact = New System.Windows.Forms.CheckBox
        Me.chbEIContact = New System.Windows.Forms.CheckBox
        Me.chbESContact = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chbPermittingContact = New System.Windows.Forms.CheckBox
        Me.chbPlanningContact = New System.Windows.Forms.CheckBox
        Me.chbMonitoringContact = New System.Windows.Forms.CheckBox
        Me.chbComplianceContact = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtAssociatedFacilites = New System.Windows.Forms.TextBox
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.btnAddNewContact = New System.Windows.Forms.Button
        Me.btnSaveContactInformation = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtContactNotes = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtEmailAddress = New System.Windows.Forms.TextBox
        Me.mtbFaxNumber = New System.Windows.Forms.MaskedTextBox
        Me.mtbPhoneNumber2 = New System.Windows.Forms.MaskedTextBox
        Me.mtbPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtContactFirstName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtContactPedigree = New System.Windows.Forms.TextBox
        Me.txtContactLastName = New System.Windows.Forms.TextBox
        Me.txtContactSocialTitle = New System.Windows.Forms.TextBox
        Me.Label71 = New System.Windows.Forms.Label
        Me.txtContactTitle = New System.Windows.Forms.TextBox
        Me.Label74 = New System.Windows.Forms.Label
        Me.txtContactCompanyName = New System.Windows.Forms.TextBox
        Me.Label75 = New System.Windows.Forms.Label
        Me.txtContactAddress = New System.Windows.Forms.TextBox
        Me.txtContactState = New System.Windows.Forms.TextBox
        Me.txtContactZipCode = New System.Windows.Forms.TextBox
        Me.txtContactCity = New System.Windows.Forms.TextBox
        Me.Label73 = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.mtbSearchAIRS = New System.Windows.Forms.MaskedTextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.btnSearchContacts = New System.Windows.Forms.Button
        Me.txtSearchEmail = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtSearchLastName = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtSearchFirstName = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.dgvContacts = New System.Windows.Forms.DataGridView
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgvContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolToolStripMenuItem, Me.HelpToolStripMenuItem})
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
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'ToolToolStripMenuItem
        '
        Me.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem"
        Me.ToolToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.ToolToolStripMenuItem.Text = "Tool"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbClear, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        Me.tsbSave.Text = "Save"
        '
        'tsbClear
        '
        Me.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), System.Drawing.Image)
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.Text = "Clear"
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "Back"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtContactId)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtAssociatedFacilites)
        Me.Panel1.Controls.Add(Me.mtbAIRSNumber)
        Me.Panel1.Controls.Add(Me.btnAddNewContact)
        Me.Panel1.Controls.Add(Me.btnSaveContactInformation)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtContactNotes)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtEmailAddress)
        Me.Panel1.Controls.Add(Me.mtbFaxNumber)
        Me.Panel1.Controls.Add(Me.mtbPhoneNumber2)
        Me.Panel1.Controls.Add(Me.mtbPhoneNumber)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtContactFirstName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtContactPedigree)
        Me.Panel1.Controls.Add(Me.txtContactLastName)
        Me.Panel1.Controls.Add(Me.txtContactSocialTitle)
        Me.Panel1.Controls.Add(Me.Label71)
        Me.Panel1.Controls.Add(Me.txtContactTitle)
        Me.Panel1.Controls.Add(Me.Label74)
        Me.Panel1.Controls.Add(Me.txtContactCompanyName)
        Me.Panel1.Controls.Add(Me.Label75)
        Me.Panel1.Controls.Add(Me.txtContactAddress)
        Me.Panel1.Controls.Add(Me.txtContactState)
        Me.Panel1.Controls.Add(Me.txtContactZipCode)
        Me.Panel1.Controls.Add(Me.txtContactCity)
        Me.Panel1.Controls.Add(Me.Label73)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(792, 252)
        Me.Panel1.TabIndex = 2
        '
        'txtContactId
        '
        Me.txtContactId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactId.Location = New System.Drawing.Point(9, 6)
        Me.txtContactId.MaxLength = 15
        Me.txtContactId.Name = "txtContactId"
        Me.txtContactId.Size = New System.Drawing.Size(43, 20)
        Me.txtContactId.TabIndex = 284
        Me.txtContactId.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chbDistrictContact)
        Me.GroupBox3.Controls.Add(Me.chbAmbientContact)
        Me.GroupBox3.Location = New System.Drawing.Point(434, 191)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(178, 52)
        Me.GroupBox3.TabIndex = 283
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Other"
        '
        'chbDistrictContact
        '
        Me.chbDistrictContact.AutoSize = True
        Me.chbDistrictContact.Location = New System.Drawing.Point(6, 15)
        Me.chbDistrictContact.Name = "chbDistrictContact"
        Me.chbDistrictContact.Size = New System.Drawing.Size(98, 17)
        Me.chbDistrictContact.TabIndex = 273
        Me.chbDistrictContact.Text = "District Contact"
        Me.chbDistrictContact.UseVisualStyleBackColor = True
        '
        'chbAmbientContact
        '
        Me.chbAmbientContact.AutoSize = True
        Me.chbAmbientContact.Location = New System.Drawing.Point(6, 32)
        Me.chbAmbientContact.Name = "chbAmbientContact"
        Me.chbAmbientContact.Size = New System.Drawing.Size(156, 17)
        Me.chbAmbientContact.TabIndex = 272
        Me.chbAmbientContact.Text = "Ambient Monitoring Contact"
        Me.chbAmbientContact.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chbFeeContact)
        Me.GroupBox2.Controls.Add(Me.chbGECOContact)
        Me.GroupBox2.Controls.Add(Me.chbEIContact)
        Me.GroupBox2.Controls.Add(Me.chbESContact)
        Me.GroupBox2.Location = New System.Drawing.Point(618, 102)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(165, 84)
        Me.GroupBox2.TabIndex = 282
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Web Based Contacts"
        '
        'chbFeeContact
        '
        Me.chbFeeContact.AutoSize = True
        Me.chbFeeContact.Location = New System.Drawing.Point(6, 66)
        Me.chbFeeContact.Name = "chbFeeContact"
        Me.chbFeeContact.Size = New System.Drawing.Size(84, 17)
        Me.chbFeeContact.TabIndex = 277
        Me.chbFeeContact.Text = "Fee Contact"
        Me.chbFeeContact.UseVisualStyleBackColor = True
        '
        'chbGECOContact
        '
        Me.chbGECOContact.AutoSize = True
        Me.chbGECOContact.Location = New System.Drawing.Point(6, 15)
        Me.chbGECOContact.Name = "chbGECOContact"
        Me.chbGECOContact.Size = New System.Drawing.Size(117, 17)
        Me.chbGECOContact.TabIndex = 276
        Me.chbGECOContact.Text = "GECO Fee Contact"
        Me.chbGECOContact.UseVisualStyleBackColor = True
        '
        'chbEIContact
        '
        Me.chbEIContact.AutoSize = True
        Me.chbEIContact.Location = New System.Drawing.Point(6, 32)
        Me.chbEIContact.Name = "chbEIContact"
        Me.chbEIContact.Size = New System.Drawing.Size(154, 17)
        Me.chbEIContact.TabIndex = 274
        Me.chbEIContact.Text = "Emission Inventory Contact"
        Me.chbEIContact.UseVisualStyleBackColor = True
        '
        'chbESContact
        '
        Me.chbESContact.AutoSize = True
        Me.chbESContact.Location = New System.Drawing.Point(6, 49)
        Me.chbESContact.Name = "chbESContact"
        Me.chbESContact.Size = New System.Drawing.Size(158, 17)
        Me.chbESContact.TabIndex = 275
        Me.chbESContact.Text = "Emission Statement Contact"
        Me.chbESContact.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.chbPermittingContact)
        Me.GroupBox1.Controls.Add(Me.chbPlanningContact)
        Me.GroupBox1.Controls.Add(Me.chbMonitoringContact)
        Me.GroupBox1.Controls.Add(Me.chbComplianceContact)
        Me.GroupBox1.Location = New System.Drawing.Point(433, 102)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(179, 84)
        Me.GroupBox1.TabIndex = 281
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stationary Source Contacts"
        '
        'chbPermittingContact
        '
        Me.chbPermittingContact.AutoSize = True
        Me.chbPermittingContact.Location = New System.Drawing.Point(6, 15)
        Me.chbPermittingContact.Name = "chbPermittingContact"
        Me.chbPermittingContact.Size = New System.Drawing.Size(112, 17)
        Me.chbPermittingContact.TabIndex = 280
        Me.chbPermittingContact.Text = "Permitting Contact"
        Me.chbPermittingContact.UseVisualStyleBackColor = True
        '
        'chbPlanningContact
        '
        Me.chbPlanningContact.AutoSize = True
        Me.chbPlanningContact.Location = New System.Drawing.Point(6, 66)
        Me.chbPlanningContact.Name = "chbPlanningContact"
        Me.chbPlanningContact.Size = New System.Drawing.Size(168, 17)
        Me.chbPlanningContact.TabIndex = 278
        Me.chbPlanningContact.Text = "Planning and Support Contact"
        Me.chbPlanningContact.UseVisualStyleBackColor = True
        '
        'chbMonitoringContact
        '
        Me.chbMonitoringContact.AutoSize = True
        Me.chbMonitoringContact.Location = New System.Drawing.Point(6, 49)
        Me.chbMonitoringContact.Name = "chbMonitoringContact"
        Me.chbMonitoringContact.Size = New System.Drawing.Size(115, 17)
        Me.chbMonitoringContact.TabIndex = 277
        Me.chbMonitoringContact.Text = "Monitoring Contact"
        Me.chbMonitoringContact.UseVisualStyleBackColor = True
        '
        'chbComplianceContact
        '
        Me.chbComplianceContact.AutoSize = True
        Me.chbComplianceContact.Location = New System.Drawing.Point(6, 32)
        Me.chbComplianceContact.Name = "chbComplianceContact"
        Me.chbComplianceContact.Size = New System.Drawing.Size(121, 17)
        Me.chbComplianceContact.TabIndex = 279
        Me.chbComplianceContact.Text = "Compliance Contact"
        Me.chbComplianceContact.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(327, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 13)
        Me.Label10.TabIndex = 271
        Me.Label10.Text = "Associated AIRS # "
        '
        'txtAssociatedFacilites
        '
        Me.txtAssociatedFacilites.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssociatedFacilites.Location = New System.Drawing.Point(433, 6)
        Me.txtAssociatedFacilites.MaxLength = 15
        Me.txtAssociatedFacilites.Multiline = True
        Me.txtAssociatedFacilites.Name = "txtAssociatedFacilites"
        Me.txtAssociatedFacilites.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAssociatedFacilites.Size = New System.Drawing.Size(234, 66)
        Me.txtAssociatedFacilites.TabIndex = 270
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(433, 78)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(63, 20)
        Me.mtbAIRSNumber.TabIndex = 269
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnAddNewContact
        '
        Me.btnAddNewContact.AutoSize = True
        Me.btnAddNewContact.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewContact.Location = New System.Drawing.Point(677, 6)
        Me.btnAddNewContact.Name = "btnAddNewContact"
        Me.btnAddNewContact.Size = New System.Drawing.Size(101, 23)
        Me.btnAddNewContact.TabIndex = 265
        Me.btnAddNewContact.Text = "Add New Contact"
        Me.btnAddNewContact.UseVisualStyleBackColor = True
        '
        'btnSaveContactInformation
        '
        Me.btnSaveContactInformation.AutoSize = True
        Me.btnSaveContactInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveContactInformation.Location = New System.Drawing.Point(677, 47)
        Me.btnSaveContactInformation.Name = "btnSaveContactInformation"
        Me.btnSaveContactInformation.Size = New System.Drawing.Size(106, 23)
        Me.btnSaveContactInformation.TabIndex = 266
        Me.btnSaveContactInformation.Text = "Save Contact Info."
        Me.btnSaveContactInformation.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(385, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 268
        Me.Label3.Text = "AIRS #"
        '
        'txtContactNotes
        '
        Me.txtContactNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactNotes.Location = New System.Drawing.Point(277, 186)
        Me.txtContactNotes.MaxLength = 15
        Me.txtContactNotes.Multiline = True
        Me.txtContactNotes.Name = "txtContactNotes"
        Me.txtContactNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtContactNotes.Size = New System.Drawing.Size(150, 60)
        Me.txtContactNotes.TabIndex = 264
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(233, 189)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 263
        Me.Label9.Text = "Notes:"
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailAddress.Location = New System.Drawing.Point(89, 226)
        Me.txtEmailAddress.MaxLength = 100
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(182, 20)
        Me.txtEmailAddress.TabIndex = 262
        '
        'mtbFaxNumber
        '
        Me.mtbFaxNumber.Location = New System.Drawing.Point(89, 204)
        Me.mtbFaxNumber.Mask = "(999) 000-0000"
        Me.mtbFaxNumber.Name = "mtbFaxNumber"
        Me.mtbFaxNumber.Size = New System.Drawing.Size(83, 20)
        Me.mtbFaxNumber.TabIndex = 261
        Me.mtbFaxNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbPhoneNumber2
        '
        Me.mtbPhoneNumber2.Location = New System.Drawing.Point(89, 182)
        Me.mtbPhoneNumber2.Mask = "(999) 000-0000 ext 00000"
        Me.mtbPhoneNumber2.Name = "mtbPhoneNumber2"
        Me.mtbPhoneNumber2.Size = New System.Drawing.Size(133, 20)
        Me.mtbPhoneNumber2.TabIndex = 260
        Me.mtbPhoneNumber2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbPhoneNumber
        '
        Me.mtbPhoneNumber.Location = New System.Drawing.Point(89, 160)
        Me.mtbPhoneNumber.Mask = "(999) 000-0000 ext 00000"
        Me.mtbPhoneNumber.Name = "mtbPhoneNumber"
        Me.mtbPhoneNumber.Size = New System.Drawing.Size(133, 20)
        Me.mtbPhoneNumber.TabIndex = 259
        Me.mtbPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 230)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 258
        Me.Label8.Text = "Email Address:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(20, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 257
        Me.Label7.Text = "Fax Number:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 256
        Me.Label6.Text = "Alt. Number:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 255
        Me.Label5.Text = "Phone Number:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(51, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 254
        Me.Label4.Text = "Suffix:"
        '
        'txtContactFirstName
        '
        Me.txtContactFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactFirstName.Location = New System.Drawing.Point(89, 28)
        Me.txtContactFirstName.MaxLength = 35
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(88, 20)
        Me.txtContactFirstName.TabIndex = 237
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(51, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 251
        Me.Label1.Text = "Prefix:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 252
        Me.Label2.Text = "Full Name:"
        '
        'txtContactPedigree
        '
        Me.txtContactPedigree.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactPedigree.Location = New System.Drawing.Point(89, 50)
        Me.txtContactPedigree.MaxLength = 15
        Me.txtContactPedigree.Name = "txtContactPedigree"
        Me.txtContactPedigree.Size = New System.Drawing.Size(69, 20)
        Me.txtContactPedigree.TabIndex = 239
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactLastName.Location = New System.Drawing.Point(183, 28)
        Me.txtContactLastName.MaxLength = 35
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(88, 20)
        Me.txtContactLastName.TabIndex = 238
        '
        'txtContactSocialTitle
        '
        Me.txtContactSocialTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactSocialTitle.Location = New System.Drawing.Point(89, 6)
        Me.txtContactSocialTitle.MaxLength = 15
        Me.txtContactSocialTitle.Name = "txtContactSocialTitle"
        Me.txtContactSocialTitle.Size = New System.Drawing.Size(52, 20)
        Me.txtContactSocialTitle.TabIndex = 236
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(15, 76)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(72, 13)
        Me.Label71.TabIndex = 247
        Me.Label71.Text = "Title/Position:"
        '
        'txtContactTitle
        '
        Me.txtContactTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactTitle.Location = New System.Drawing.Point(89, 72)
        Me.txtContactTitle.MaxLength = 100
        Me.txtContactTitle.Name = "txtContactTitle"
        Me.txtContactTitle.Size = New System.Drawing.Size(256, 20)
        Me.txtContactTitle.TabIndex = 240
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(2, 98)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(85, 13)
        Me.Label74.TabIndex = 248
        Me.Label74.Text = "Company Name:"
        '
        'txtContactCompanyName
        '
        Me.txtContactCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactCompanyName.Location = New System.Drawing.Point(89, 94)
        Me.txtContactCompanyName.MaxLength = 200
        Me.txtContactCompanyName.Name = "txtContactCompanyName"
        Me.txtContactCompanyName.Size = New System.Drawing.Size(256, 20)
        Me.txtContactCompanyName.TabIndex = 241
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(3, 120)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(84, 13)
        Me.Label75.TabIndex = 249
        Me.Label75.Text = "Mailing Address:"
        '
        'txtContactAddress
        '
        Me.txtContactAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactAddress.Location = New System.Drawing.Point(89, 116)
        Me.txtContactAddress.MaxLength = 250
        Me.txtContactAddress.Name = "txtContactAddress"
        Me.txtContactAddress.Size = New System.Drawing.Size(256, 20)
        Me.txtContactAddress.TabIndex = 242
        '
        'txtContactState
        '
        Me.txtContactState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactState.Location = New System.Drawing.Point(240, 138)
        Me.txtContactState.MaxLength = 2
        Me.txtContactState.Name = "txtContactState"
        Me.txtContactState.Size = New System.Drawing.Size(28, 20)
        Me.txtContactState.TabIndex = 245
        '
        'txtContactZipCode
        '
        Me.txtContactZipCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactZipCode.Location = New System.Drawing.Point(274, 138)
        Me.txtContactZipCode.MaxLength = 9
        Me.txtContactZipCode.Name = "txtContactZipCode"
        Me.txtContactZipCode.Size = New System.Drawing.Size(71, 20)
        Me.txtContactZipCode.TabIndex = 246
        '
        'txtContactCity
        '
        Me.txtContactCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactCity.Location = New System.Drawing.Point(89, 138)
        Me.txtContactCity.MaxLength = 50
        Me.txtContactCity.Name = "txtContactCity"
        Me.txtContactCity.Size = New System.Drawing.Size(145, 20)
        Me.txtContactCity.TabIndex = 244
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(25, 142)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(62, 13)
        Me.Label73.TabIndex = 250
        Me.Label73.Text = "City/St/Zip:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 202
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(667, 17)
        Me.ToolStripStatusLabel1.Spring = True
        Me.ToolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.mtbSearchAIRS)
        Me.Panel4.Controls.Add(Me.Button4)
        Me.Panel4.Controls.Add(Me.btnSearchContacts)
        Me.Panel4.Controls.Add(Me.txtSearchEmail)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.txtSearchLastName)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.txtSearchFirstName)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 301)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(792, 57)
        Me.Panel4.TabIndex = 203
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(24, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(42, 13)
        Me.Label14.TabIndex = 285
        Me.Label14.Text = "AIRS #"
        '
        'mtbSearchAIRS
        '
        Me.mtbSearchAIRS.Location = New System.Drawing.Point(70, 32)
        Me.mtbSearchAIRS.Mask = "000-00000"
        Me.mtbSearchAIRS.Name = "mtbSearchAIRS"
        Me.mtbSearchAIRS.Size = New System.Drawing.Size(63, 20)
        Me.mtbSearchAIRS.TabIndex = 285
        Me.mtbSearchAIRS.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(581, 6)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 284
        Me.Button4.Text = "transfer data "
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'btnSearchContacts
        '
        Me.btnSearchContacts.AutoSize = True
        Me.btnSearchContacts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchContacts.Location = New System.Drawing.Point(677, 4)
        Me.btnSearchContacts.Name = "btnSearchContacts"
        Me.btnSearchContacts.Size = New System.Drawing.Size(51, 23)
        Me.btnSearchContacts.TabIndex = 266
        Me.btnSearchContacts.Text = "Search"
        Me.btnSearchContacts.UseVisualStyleBackColor = True
        '
        'txtSearchEmail
        '
        Me.txtSearchEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchEmail.Location = New System.Drawing.Point(365, 6)
        Me.txtSearchEmail.MaxLength = 35
        Me.txtSearchEmail.Name = "txtSearchEmail"
        Me.txtSearchEmail.Size = New System.Drawing.Size(158, 20)
        Me.txtSearchEmail.TabIndex = 257
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(331, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 13)
        Me.Label13.TabIndex = 258
        Me.Label13.Text = "Email:"
        '
        'txtSearchLastName
        '
        Me.txtSearchLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchLastName.Location = New System.Drawing.Point(223, 6)
        Me.txtSearchLastName.MaxLength = 35
        Me.txtSearchLastName.Name = "txtSearchLastName"
        Me.txtSearchLastName.Size = New System.Drawing.Size(88, 20)
        Me.txtSearchLastName.TabIndex = 255
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(164, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 13)
        Me.Label12.TabIndex = 256
        Me.Label12.Text = "Last Name:"
        '
        'txtSearchFirstName
        '
        Me.txtSearchFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchFirstName.Location = New System.Drawing.Point(70, 6)
        Me.txtSearchFirstName.MaxLength = 35
        Me.txtSearchFirstName.Name = "txtSearchFirstName"
        Me.txtSearchFirstName.Size = New System.Drawing.Size(88, 20)
        Me.txtSearchFirstName.TabIndex = 253
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(11, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 254
        Me.Label11.Text = "First Name:"
        '
        'dgvContacts
        '
        Me.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvContacts.Location = New System.Drawing.Point(0, 358)
        Me.dgvContacts.Name = "dgvContacts"
        Me.dgvContacts.Size = New System.Drawing.Size(792, 186)
        Me.dgvContacts.TabIndex = 0
        '
        'DEVEditContacts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.dgvContacts)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "DEVEditContacts"
        Me.Text = "DEV Edit Contacts"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgvContacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtContactPedigree As System.Windows.Forms.TextBox
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtContactCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtContactAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtContactState As System.Windows.Forms.TextBox
    Friend WithEvents txtContactZipCode As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCity As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents mtbPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents mtbFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbPhoneNumber2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtContactNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnAddNewContact As System.Windows.Forms.Button
    Friend WithEvents btnSaveContactInformation As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAssociatedFacilites As System.Windows.Forms.TextBox
    Friend WithEvents chbAmbientContact As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents dgvContacts As System.Windows.Forms.DataGridView
    Friend WithEvents chbMonitoringContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbGECOContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbESContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbEIContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbDistrictContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbPermittingContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbPlanningContact As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearchEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSearchLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSearchFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnSearchContacts As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents txtContactId As System.Windows.Forms.TextBox
    Friend WithEvents chbFeeContact As System.Windows.Forms.CheckBox
    Friend WithEvents mtbSearchAIRS As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
End Class
