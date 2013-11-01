<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPNotificationLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPNotificationLog))
        Me.lblStreetAddress = New System.Windows.Forms.Label
        Me.lblCityStateZipCode = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtEmissionUnit = New System.Windows.Forms.TextBox
        Me.DTPTestDateStart = New System.Windows.Forms.DateTimePicker
        Me.DTPTestDateEnd = New System.Windows.Forms.DateTimePicker
        Me.txtNotificationComments = New System.Windows.Forms.TextBox
        Me.cboAIRSNumber = New System.Windows.Forms.ComboBox
        Me.cboFacilityName = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.bbtSave = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtContactFirstName = New System.Windows.Forms.TextBox
        Me.txtContactLastName = New System.Windows.Forms.TextBox
        Me.txtContactEmailAddress = New System.Windows.Forms.TextBox
        Me.chbWebContact = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3X = New System.Windows.Forms.Panel
        Me.rdbTestPlanNotAvailable = New System.Windows.Forms.RadioButton
        Me.rdbTestPlanAvailable = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rdbNoTimelyNotification = New System.Windows.Forms.RadioButton
        Me.rdbTimelyNotification = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTestNotificationNumber = New System.Windows.Forms.TextBox
        Me.txtISMPComments = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.cboStaffResponsible = New System.Windows.Forms.ComboBox
        Me.btnNewTestNotification = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.mtbFaxNumber = New System.Windows.Forms.MaskedTextBox
        Me.mtbPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtPollutants = New System.Windows.Forms.TextBox
        Me.lblFaxNumber = New System.Windows.Forms.Label
        Me.lblPhoneNumber = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblReceivedDate = New System.Windows.Forms.Label
        Me.DTPTestNotification = New System.Windows.Forms.DateTimePicker
        Me.DTPTestPlanReceived = New System.Windows.Forms.DateTimePicker
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel3X.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblStreetAddress
        '
        Me.lblStreetAddress.AutoSize = True
        Me.lblStreetAddress.Location = New System.Drawing.Point(175, 58)
        Me.lblStreetAddress.Name = "lblStreetAddress"
        Me.lblStreetAddress.Size = New System.Drawing.Size(76, 13)
        Me.lblStreetAddress.TabIndex = 1
        Me.lblStreetAddress.Text = "Street Address"
        '
        'lblCityStateZipCode
        '
        Me.lblCityStateZipCode.AutoSize = True
        Me.lblCityStateZipCode.Location = New System.Drawing.Point(175, 76)
        Me.lblCityStateZipCode.Name = "lblCityStateZipCode"
        Me.lblCityStateZipCode.Size = New System.Drawing.Size(90, 13)
        Me.lblCityStateZipCode.TabIndex = 2
        Me.lblCityStateZipCode.Text = "City, ST Zip Code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(481, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Contact Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(483, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Email Address"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Emission Unit(s) "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 163)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Test Date Start"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(131, 163)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Test Date End"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 205)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Test Plan Comments"
        '
        'txtEmissionUnit
        '
        Me.txtEmissionUnit.Location = New System.Drawing.Point(26, 98)
        Me.txtEmissionUnit.Multiline = True
        Me.txtEmissionUnit.Name = "txtEmissionUnit"
        Me.txtEmissionUnit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmissionUnit.Size = New System.Drawing.Size(239, 54)
        Me.txtEmissionUnit.TabIndex = 9
        '
        'DTPTestDateStart
        '
        Me.DTPTestDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateStart.Location = New System.Drawing.Point(26, 179)
        Me.DTPTestDateStart.Name = "DTPTestDateStart"
        Me.DTPTestDateStart.Size = New System.Drawing.Size(97, 20)
        Me.DTPTestDateStart.TabIndex = 10
        Me.DTPTestDateStart.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPTestDateEnd
        '
        Me.DTPTestDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateEnd.Location = New System.Drawing.Point(149, 179)
        Me.DTPTestDateEnd.Name = "DTPTestDateEnd"
        Me.DTPTestDateEnd.Size = New System.Drawing.Size(97, 20)
        Me.DTPTestDateEnd.TabIndex = 11
        Me.DTPTestDateEnd.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'txtNotificationComments
        '
        Me.txtNotificationComments.Location = New System.Drawing.Point(26, 221)
        Me.txtNotificationComments.Multiline = True
        Me.txtNotificationComments.Name = "txtNotificationComments"
        Me.txtNotificationComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotificationComments.Size = New System.Drawing.Size(437, 54)
        Me.txtNotificationComments.TabIndex = 12
        '
        'cboAIRSNumber
        '
        Me.cboAIRSNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAIRSNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAIRSNumber.Location = New System.Drawing.Point(26, 33)
        Me.cboAIRSNumber.Name = "cboAIRSNumber"
        Me.cboAIRSNumber.Size = New System.Drawing.Size(128, 21)
        Me.cboAIRSNumber.TabIndex = 139
        '
        'cboFacilityName
        '
        Me.cboFacilityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityName.Location = New System.Drawing.Point(178, 33)
        Me.cboFacilityName.Name = "cboFacilityName"
        Me.cboFacilityName.Size = New System.Drawing.Size(285, 21)
        Me.cboFacilityName.TabIndex = 138
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 14)
        Me.Label14.TabIndex = 141
        Me.Label14.Text = "AIRS Number"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(163, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 14)
        Me.Label10.TabIndex = 140
        Me.Label10.Text = "Facility Name"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bbtSave, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(719, 25)
        Me.ToolStrip1.TabIndex = 142
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'bbtSave
        '
        Me.bbtSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bbtSave.Image = CType(resources.GetObject("bbtSave.Image"), System.Drawing.Image)
        Me.bbtSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bbtSave.Name = "bbtSave"
        Me.bbtSave.Size = New System.Drawing.Size(23, 22)
        Me.bbtSave.Text = "Save"
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(719, 24)
        Me.MenuStrip1.TabIndex = 143
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.BackToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'BackToolStripMenuItem
        '
        Me.BackToolStripMenuItem.Name = "BackToolStripMenuItem"
        Me.BackToolStripMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.BackToolStripMenuItem.Text = "Back"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(719, 22)
        Me.StatusStrip1.TabIndex = 144
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(696, 17)
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
        'txtContactFirstName
        '
        Me.txtContactFirstName.Location = New System.Drawing.Point(484, 33)
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(100, 20)
        Me.txtContactFirstName.TabIndex = 145
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Location = New System.Drawing.Point(594, 33)
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(100, 20)
        Me.txtContactLastName.TabIndex = 146
        '
        'txtContactEmailAddress
        '
        Me.txtContactEmailAddress.Location = New System.Drawing.Point(484, 73)
        Me.txtContactEmailAddress.Name = "txtContactEmailAddress"
        Me.txtContactEmailAddress.Size = New System.Drawing.Size(210, 20)
        Me.txtContactEmailAddress.TabIndex = 147
        '
        'chbWebContact
        '
        Me.chbWebContact.AutoSize = True
        Me.chbWebContact.Location = New System.Drawing.Point(484, 98)
        Me.chbWebContact.Name = "chbWebContact"
        Me.chbWebContact.Size = New System.Drawing.Size(89, 17)
        Me.chbWebContact.TabIndex = 148
        Me.chbWebContact.Text = "Web Contact"
        Me.chbWebContact.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 149
        Me.Label1.Text = "Test Plan Available"
        '
        'Panel3X
        '
        Me.Panel3X.AutoSize = True
        Me.Panel3X.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3X.Controls.Add(Me.rdbTestPlanNotAvailable)
        Me.Panel3X.Controls.Add(Me.rdbTestPlanAvailable)
        Me.Panel3X.Location = New System.Drawing.Point(24, 32)
        Me.Panel3X.Name = "Panel3X"
        Me.Panel3X.Size = New System.Drawing.Size(94, 23)
        Me.Panel3X.TabIndex = 150
        '
        'rdbTestPlanNotAvailable
        '
        Me.rdbTestPlanNotAvailable.AutoSize = True
        Me.rdbTestPlanNotAvailable.Location = New System.Drawing.Point(52, 3)
        Me.rdbTestPlanNotAvailable.Name = "rdbTestPlanNotAvailable"
        Me.rdbTestPlanNotAvailable.Size = New System.Drawing.Size(39, 17)
        Me.rdbTestPlanNotAvailable.TabIndex = 1
        Me.rdbTestPlanNotAvailable.TabStop = True
        Me.rdbTestPlanNotAvailable.Text = "No"
        Me.rdbTestPlanNotAvailable.UseVisualStyleBackColor = True
        '
        'rdbTestPlanAvailable
        '
        Me.rdbTestPlanAvailable.AutoSize = True
        Me.rdbTestPlanAvailable.Location = New System.Drawing.Point(3, 3)
        Me.rdbTestPlanAvailable.Name = "rdbTestPlanAvailable"
        Me.rdbTestPlanAvailable.Size = New System.Drawing.Size(43, 17)
        Me.rdbTestPlanAvailable.TabIndex = 0
        Me.rdbTestPlanAvailable.TabStop = True
        Me.rdbTestPlanAvailable.Text = "Yes"
        Me.rdbTestPlanAvailable.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(249, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 151
        Me.Label2.Text = "Timely Notification"
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.rdbNoTimelyNotification)
        Me.Panel4.Controls.Add(Me.rdbTimelyNotification)
        Me.Panel4.Location = New System.Drawing.Point(263, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(97, 23)
        Me.Panel4.TabIndex = 152
        '
        'rdbNoTimelyNotification
        '
        Me.rdbNoTimelyNotification.AutoSize = True
        Me.rdbNoTimelyNotification.Location = New System.Drawing.Point(55, 3)
        Me.rdbNoTimelyNotification.Name = "rdbNoTimelyNotification"
        Me.rdbNoTimelyNotification.Size = New System.Drawing.Size(39, 17)
        Me.rdbNoTimelyNotification.TabIndex = 1
        Me.rdbNoTimelyNotification.TabStop = True
        Me.rdbNoTimelyNotification.Text = "No"
        Me.rdbNoTimelyNotification.UseVisualStyleBackColor = True
        '
        'rdbTimelyNotification
        '
        Me.rdbTimelyNotification.AutoSize = True
        Me.rdbTimelyNotification.Location = New System.Drawing.Point(3, 3)
        Me.rdbTimelyNotification.Name = "rdbTimelyNotification"
        Me.rdbTimelyNotification.Size = New System.Drawing.Size(43, 17)
        Me.rdbTimelyNotification.TabIndex = 0
        Me.rdbTimelyNotification.TabStop = True
        Me.rdbTimelyNotification.Text = "Yes"
        Me.rdbTimelyNotification.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 14)
        Me.Label3.TabIndex = 153
        Me.Label3.Text = "Test Notification Number"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtTestNotificationNumber
        '
        Me.txtTestNotificationNumber.Location = New System.Drawing.Point(137, 57)
        Me.txtTestNotificationNumber.Name = "txtTestNotificationNumber"
        Me.txtTestNotificationNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtTestNotificationNumber.TabIndex = 154
        '
        'txtISMPComments
        '
        Me.txtISMPComments.Location = New System.Drawing.Point(27, 78)
        Me.txtISMPComments.Multiline = True
        Me.txtISMPComments.Name = "txtISMPComments"
        Me.txtISMPComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtISMPComments.Size = New System.Drawing.Size(436, 69)
        Me.txtISMPComments.TabIndex = 155
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 62)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 13)
        Me.Label12.TabIndex = 157
        Me.Label12.Text = "ISMP Comments"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(479, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(119, 13)
        Me.Label11.TabIndex = 158
        Me.Label11.Text = "ISMP Staff Responsible"
        '
        'cboStaffResponsible
        '
        Me.cboStaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaffResponsible.Location = New System.Drawing.Point(484, 35)
        Me.cboStaffResponsible.Name = "cboStaffResponsible"
        Me.cboStaffResponsible.Size = New System.Drawing.Size(208, 21)
        Me.cboStaffResponsible.TabIndex = 159
        '
        'btnNewTestNotification
        '
        Me.btnNewTestNotification.AutoSize = True
        Me.btnNewTestNotification.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNewTestNotification.Location = New System.Drawing.Point(243, 54)
        Me.btnNewTestNotification.Name = "btnNewTestNotification"
        Me.btnNewTestNotification.Size = New System.Drawing.Size(119, 23)
        Me.btnNewTestNotification.TabIndex = 160
        Me.btnNewTestNotification.Text = "New Test Notification"
        Me.btnNewTestNotification.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.mtbFaxNumber)
        Me.GroupBox1.Controls.Add(Me.mtbPhoneNumber)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtPollutants)
        Me.GroupBox1.Controls.Add(Me.lblFaxNumber)
        Me.GroupBox1.Controls.Add(Me.lblPhoneNumber)
        Me.GroupBox1.Controls.Add(Me.txtContactFirstName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtContactLastName)
        Me.GroupBox1.Controls.Add(Me.txtContactEmailAddress)
        Me.GroupBox1.Controls.Add(Me.chbWebContact)
        Me.GroupBox1.Controls.Add(Me.cboFacilityName)
        Me.GroupBox1.Controls.Add(Me.lblStreetAddress)
        Me.GroupBox1.Controls.Add(Me.lblCityStateZipCode)
        Me.GroupBox1.Controls.Add(Me.cboAIRSNumber)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtNotificationComments)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtEmissionUnit)
        Me.GroupBox1.Controls.Add(Me.DTPTestDateEnd)
        Me.GroupBox1.Controls.Add(Me.DTPTestDateStart)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 77)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(719, 283)
        Me.GroupBox1.TabIndex = 161
        Me.GroupBox1.TabStop = False
        '
        'mtbFaxNumber
        '
        Me.mtbFaxNumber.Location = New System.Drawing.Point(537, 142)
        Me.mtbFaxNumber.Mask = "(999) 000-0000"
        Me.mtbFaxNumber.Name = "mtbFaxNumber"
        Me.mtbFaxNumber.Size = New System.Drawing.Size(96, 20)
        Me.mtbFaxNumber.TabIndex = 156
        Me.mtbFaxNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbPhoneNumber
        '
        Me.mtbPhoneNumber.Location = New System.Drawing.Point(537, 116)
        Me.mtbPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbPhoneNumber.Name = "mtbPhoneNumber"
        Me.mtbPhoneNumber.Size = New System.Drawing.Size(96, 20)
        Me.mtbPhoneNumber.TabIndex = 155
        Me.mtbPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(271, 98)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 13)
        Me.Label16.TabIndex = 152
        Me.Label16.Text = "Pollutants"
        '
        'txtPollutants
        '
        Me.txtPollutants.Location = New System.Drawing.Point(285, 114)
        Me.txtPollutants.Multiline = True
        Me.txtPollutants.Name = "txtPollutants"
        Me.txtPollutants.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPollutants.Size = New System.Drawing.Size(178, 36)
        Me.txtPollutants.TabIndex = 151
        '
        'lblFaxNumber
        '
        Me.lblFaxNumber.AutoSize = True
        Me.lblFaxNumber.Location = New System.Drawing.Point(483, 137)
        Me.lblFaxNumber.Name = "lblFaxNumber"
        Me.lblFaxNumber.Size = New System.Drawing.Size(37, 13)
        Me.lblFaxNumber.TabIndex = 150
        Me.lblFaxNumber.Text = "Fax #:"
        '
        'lblPhoneNumber
        '
        Me.lblPhoneNumber.AutoSize = True
        Me.lblPhoneNumber.Location = New System.Drawing.Point(483, 119)
        Me.lblPhoneNumber.Name = "lblPhoneNumber"
        Me.lblPhoneNumber.Size = New System.Drawing.Size(51, 13)
        Me.lblPhoneNumber.TabIndex = 149
        Me.lblPhoneNumber.Text = "Phone #:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.lblReceivedDate)
        Me.GroupBox2.Controls.Add(Me.DTPTestNotification)
        Me.GroupBox2.Controls.Add(Me.DTPTestPlanReceived)
        Me.GroupBox2.Controls.Add(Me.txtReferenceNumber)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cboStaffResponsible)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtISMPComments)
        Me.GroupBox2.Controls.Add(Me.Panel3X)
        Me.GroupBox2.Controls.Add(Me.Panel4)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 366)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(719, 175)
        Me.GroupBox2.TabIndex = 162
        Me.GroupBox2.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(394, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(69, 13)
        Me.Label15.TabIndex = 168
        Me.Label15.Text = "Date Notified"
        '
        'lblReceivedDate
        '
        Me.lblReceivedDate.AutoSize = True
        Me.lblReceivedDate.Location = New System.Drawing.Point(139, 16)
        Me.lblReceivedDate.Name = "lblReceivedDate"
        Me.lblReceivedDate.Size = New System.Drawing.Size(79, 13)
        Me.lblReceivedDate.TabIndex = 167
        Me.lblReceivedDate.Text = "Date Received"
        Me.lblReceivedDate.Visible = False
        '
        'DTPTestNotification
        '
        Me.DTPTestNotification.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestNotification.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestNotification.Location = New System.Drawing.Point(366, 32)
        Me.DTPTestNotification.Name = "DTPTestNotification"
        Me.DTPTestNotification.Size = New System.Drawing.Size(97, 20)
        Me.DTPTestNotification.TabIndex = 166
        Me.DTPTestNotification.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPTestPlanReceived
        '
        Me.DTPTestPlanReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestPlanReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestPlanReceived.Location = New System.Drawing.Point(121, 32)
        Me.DTPTestPlanReceived.Name = "DTPTestPlanReceived"
        Me.DTPTestPlanReceived.Size = New System.Drawing.Size(97, 20)
        Me.DTPTestPlanReceived.TabIndex = 165
        Me.DTPTestPlanReceived.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPTestPlanReceived.Visible = False
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(556, 78)
        Me.txtReferenceNumber.Multiline = True
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.ReadOnly = True
        Me.txtReferenceNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReferenceNumber.Size = New System.Drawing.Size(136, 66)
        Me.txtReferenceNumber.TabIndex = 164
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(479, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 163
        Me.Label13.Text = "Reference #'s"
        '
        'ISMPNotificationLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(719, 566)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnNewTestNotification)
        Me.Controls.Add(Me.txtTestNotificationNumber)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "ISMPNotificationLog"
        Me.Text = "ISMP Notification Log"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel3X.ResumeLayout(False)
        Me.Panel3X.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblStreetAddress As System.Windows.Forms.Label
    Friend WithEvents lblCityStateZipCode As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEmissionUnit As System.Windows.Forms.TextBox
    Friend WithEvents DTPTestDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPTestDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNotificationComments As System.Windows.Forms.TextBox
    Friend WithEvents cboAIRSNumber As System.Windows.Forms.ComboBox
    Friend WithEvents cboFacilityName As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bbtSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents chbWebContact As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3X As System.Windows.Forms.Panel
    Friend WithEvents rdbTestPlanNotAvailable As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTestPlanAvailable As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbNoTimelyNotification As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTimelyNotification As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTestNotificationNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtISMPComments As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboStaffResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents btnNewTestNotification As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblFaxNumber As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents DTPTestNotification As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPTestPlanReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblReceivedDate As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtPollutants As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents mtbFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbPhoneNumber As System.Windows.Forms.MaskedTextBox
End Class
