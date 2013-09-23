<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MASPRegistrationTool
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
        Me.components = New System.ComponentModel.Container
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.mmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiNewApplication = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chbGECOlogInRequired = New System.Windows.Forms.CheckBox
        Me.Label48 = New System.Windows.Forms.Label
        Me.txtEventEndTime = New System.Windows.Forms.TextBox
        Me.txtWebsiteURL = New System.Windows.Forms.TextBox
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.DTPEventEndDate = New System.Windows.Forms.DateTimePicker
        Me.btnClearEventManagement = New System.Windows.Forms.Button
        Me.mtbEventPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.cboEventContact = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnGeneratePasscode = New System.Windows.Forms.Button
        Me.Label16 = New System.Windows.Forms.Label
        Me.chbEventPasscode = New System.Windows.Forms.CheckBox
        Me.btnMapEventLocation = New System.Windows.Forms.Button
        Me.mtbEventZipCode = New System.Windows.Forms.MaskedTextBox
        Me.mtbEventState = New System.Windows.Forms.MaskedTextBox
        Me.txtEventCity = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtEventAddress = New System.Windows.Forms.TextBox
        Me.mtbEventWebPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.cboEventWebContact = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtEventTime = New System.Windows.Forms.TextBox
        Me.btnDeleteEvent = New System.Windows.Forms.Button
        Me.btnUpdateEvent = New System.Windows.Forms.Button
        Me.btnSaveNewEvent = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtEventNotes = New System.Windows.Forms.TextBox
        Me.mtbEventCapacity = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.DTPEventDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEventVenue = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtEventDescription = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboEventStatus = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtEventTitle = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtEventID = New System.Windows.Forms.TextBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPEventOverview = New System.Windows.Forms.TabPage
        Me.dgvOverviewRegistrants = New System.Windows.Forms.DataGridView
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.lblNotes = New System.Windows.Forms.TextBox
        Me.lblVenue = New System.Windows.Forms.TextBox
        Me.lblAPBContact = New System.Windows.Forms.TextBox
        Me.lblEventContact = New System.Windows.Forms.TextBox
        Me.lblWaitingList = New System.Windows.Forms.TextBox
        Me.lblEventCapacity = New System.Windows.Forms.TextBox
        Me.lblNumberRegistered = New System.Windows.Forms.TextBox
        Me.lblEventStatus = New System.Windows.Forms.TextBox
        Me.lblPassCode = New System.Windows.Forms.TextBox
        Me.lblLogInRequired = New System.Windows.Forms.TextBox
        Me.lblEventDateTime = New System.Windows.Forms.TextBox
        Me.lblEvent = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.TextBox
        Me.txtEmails = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.btnEmailWaitList = New System.Windows.Forms.Button
        Me.Label36 = New System.Windows.Forms.Label
        Me.btnEmailRegistrants = New System.Windows.Forms.Button
        Me.Label37 = New System.Windows.Forms.Label
        Me.btnEmailAll = New System.Windows.Forms.Button
        Me.Label38 = New System.Windows.Forms.Label
        Me.btnExportToExcel = New System.Windows.Forms.Button
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.TPEventManagement = New System.Windows.Forms.TabPage
        Me.TPRegistrationManagement = New System.Windows.Forms.TabPage
        Me.dgvRegistrationManagement = New System.Windows.Forms.DataGridView
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.btnModifyRegistration = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtRegEmail = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtRegComments = New System.Windows.Forms.TextBox
        Me.DTPRegDateRegistered = New System.Windows.Forms.DateTimePicker
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtGECOUserID = New System.Windows.Forms.TextBox
        Me.cboRegUserType = New System.Windows.Forms.ComboBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.mtbRegPhoneExt = New System.Windows.Forms.MaskedTextBox
        Me.mtbRegPhoneNo = New System.Windows.Forms.MaskedTextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.mtbRegZipCode = New System.Windows.Forms.MaskedTextBox
        Me.mtbRegState = New System.Windows.Forms.MaskedTextBox
        Me.txtRegCity = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtRegAddress = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtRegTitle = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtRegLastName = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtRegFirstName = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtRegSalutation = New System.Windows.Forms.TextBox
        Me.cboRegStatus = New System.Windows.Forms.ComboBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtRegConfirmationNum = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtRegEventTitle = New System.Windows.Forms.TextBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.dgvRegistrationEvent = New System.Windows.Forms.DataGridView
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.txtSelectedEventID = New System.Windows.Forms.TextBox
        Me.lblEventTitle = New System.Windows.Forms.Label
        Me.btnViewDetails = New System.Windows.Forms.Button
        Me.lblEVentDate = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnFilterEvents = New System.Windows.Forms.Button
        Me.rdbAllEvents = New System.Windows.Forms.RadioButton
        Me.rdbPastEvents = New System.Windows.Forms.RadioButton
        Me.rdbUpcomingEvents = New System.Windows.Forms.RadioButton
        Me.StatusStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TPEventOverview.SuspendLayout()
        CType(Me.dgvOverviewRegistrants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.TPEventManagement.SuspendLayout()
        Me.TPRegistrationManagement.SuspendLayout()
        CType(Me.dgvRegistrationManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvRegistrationEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 256
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(667, 17)
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
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFile, Me.MmiView, Me.MenuItem2, Me.MmiHelp})
        '
        'MmiFile
        '
        Me.MmiFile.Index = 0
        Me.MmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiSave, Me.MenuItem1, Me.MmiBack})
        Me.MmiFile.Text = "File"
        '
        'mmiSave
        '
        Me.mmiSave.Index = 0
        Me.mmiSave.Text = "Save"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.Text = "-"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 2
        Me.MmiBack.Text = "Back"
        '
        'MmiView
        '
        Me.MmiView.Index = 1
        Me.MmiView.Text = "View"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiNewApplication})
        Me.MenuItem2.Text = "Tools"
        '
        'mmiNewApplication
        '
        Me.mmiNewApplication.Index = 0
        Me.mmiNewApplication.Text = "Assign Application No."
        Me.mmiNewApplication.Visible = False
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 3
        Me.MmiHelp.Text = "Help"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(778, 352)
        Me.Panel4.TabIndex = 257
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chbGECOlogInRequired)
        Me.GroupBox2.Controls.Add(Me.Label48)
        Me.GroupBox2.Controls.Add(Me.txtEventEndTime)
        Me.GroupBox2.Controls.Add(Me.txtWebsiteURL)
        Me.GroupBox2.Controls.Add(Me.Label47)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.DTPEventEndDate)
        Me.GroupBox2.Controls.Add(Me.btnClearEventManagement)
        Me.GroupBox2.Controls.Add(Me.mtbEventPhoneNumber)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.cboEventContact)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.btnGeneratePasscode)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.chbEventPasscode)
        Me.GroupBox2.Controls.Add(Me.btnMapEventLocation)
        Me.GroupBox2.Controls.Add(Me.mtbEventZipCode)
        Me.GroupBox2.Controls.Add(Me.mtbEventState)
        Me.GroupBox2.Controls.Add(Me.txtEventCity)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtEventAddress)
        Me.GroupBox2.Controls.Add(Me.mtbEventWebPhoneNumber)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.cboEventWebContact)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtEventTime)
        Me.GroupBox2.Controls.Add(Me.btnDeleteEvent)
        Me.GroupBox2.Controls.Add(Me.btnUpdateEvent)
        Me.GroupBox2.Controls.Add(Me.btnSaveNewEvent)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtEventNotes)
        Me.GroupBox2.Controls.Add(Me.mtbEventCapacity)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.DTPEventDate)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtEventVenue)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtEventDescription)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cboEventStatus)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtEventTitle)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtEventID)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(778, 352)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'chbGECOlogInRequired
        '
        Me.chbGECOlogInRequired.AutoSize = True
        Me.chbGECOlogInRequired.Location = New System.Drawing.Point(269, 150)
        Me.chbGECOlogInRequired.Name = "chbGECOlogInRequired"
        Me.chbGECOlogInRequired.Size = New System.Drawing.Size(134, 17)
        Me.chbGECOlogInRequired.TabIndex = 453
        Me.chbGECOlogInRequired.Text = "GECO Log in Required"
        Me.chbGECOlogInRequired.UseVisualStyleBackColor = True
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(212, 127)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(55, 13)
        Me.Label48.TabIndex = 452
        Me.Label48.Text = "End Time:"
        '
        'txtEventEndTime
        '
        Me.txtEventEndTime.Location = New System.Drawing.Point(269, 123)
        Me.txtEventEndTime.Name = "txtEventEndTime"
        Me.txtEventEndTime.Size = New System.Drawing.Size(107, 20)
        Me.txtEventEndTime.TabIndex = 451
        '
        'txtWebsiteURL
        '
        Me.txtWebsiteURL.Location = New System.Drawing.Point(500, 207)
        Me.txtWebsiteURL.Name = "txtWebsiteURL"
        Me.txtWebsiteURL.Size = New System.Drawing.Size(245, 20)
        Me.txtWebsiteURL.TabIndex = 450
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(413, 211)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(83, 13)
        Me.Label47.TabIndex = 449
        Me.Label47.Text = "Website: http://"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(166, 102)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(101, 13)
        Me.Label32.TabIndex = 447
        Me.Label32.Text = "End Date (2+ days):"
        '
        'DTPEventEndDate
        '
        Me.DTPEventEndDate.Checked = False
        Me.DTPEventEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventEndDate.Location = New System.Drawing.Point(269, 98)
        Me.DTPEventEndDate.Name = "DTPEventEndDate"
        Me.DTPEventEndDate.ShowCheckBox = True
        Me.DTPEventEndDate.Size = New System.Drawing.Size(107, 20)
        Me.DTPEventEndDate.TabIndex = 446
        '
        'btnClearEventManagement
        '
        Me.btnClearEventManagement.AutoSize = True
        Me.btnClearEventManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearEventManagement.Location = New System.Drawing.Point(335, 249)
        Me.btnClearEventManagement.Name = "btnClearEventManagement"
        Me.btnClearEventManagement.Size = New System.Drawing.Size(41, 23)
        Me.btnClearEventManagement.TabIndex = 445
        Me.btnClearEventManagement.Text = "Clear"
        Me.btnClearEventManagement.UseVisualStyleBackColor = True
        '
        'mtbEventPhoneNumber
        '
        Me.mtbEventPhoneNumber.Location = New System.Drawing.Point(319, 204)
        Me.mtbEventPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbEventPhoneNumber.Name = "mtbEventPhoneNumber"
        Me.mtbEventPhoneNumber.ReadOnly = True
        Me.mtbEventPhoneNumber.Size = New System.Drawing.Size(86, 20)
        Me.mtbEventPhoneNumber.TabIndex = 444
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(263, 208)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 13)
        Me.Label17.TabIndex = 443
        Me.Label17.Text = "Phone No:"
        '
        'cboEventContact
        '
        Me.cboEventContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventContact.FormattingEnabled = True
        Me.cboEventContact.Location = New System.Drawing.Point(112, 204)
        Me.cboEventContact.Name = "cboEventContact"
        Me.cboEventContact.Size = New System.Drawing.Size(142, 21)
        Me.cboEventContact.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(-2, 208)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 13)
        Me.Label18.TabIndex = 441
        Me.Label18.Text = "APB/Internal Contact:"
        '
        'btnGeneratePasscode
        '
        Me.btnGeneratePasscode.AutoSize = True
        Me.btnGeneratePasscode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGeneratePasscode.Location = New System.Drawing.Point(153, 147)
        Me.btnGeneratePasscode.Name = "btnGeneratePasscode"
        Me.btnGeneratePasscode.Size = New System.Drawing.Size(89, 23)
        Me.btnGeneratePasscode.TabIndex = 6
        Me.btnGeneratePasscode.Text = "Generate Code"
        Me.btnGeneratePasscode.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(10, 152)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 439
        Me.Label16.Text = "Passcode:"
        '
        'chbEventPasscode
        '
        Me.chbEventPasscode.AutoSize = True
        Me.chbEventPasscode.Location = New System.Drawing.Point(71, 150)
        Me.chbEventPasscode.Name = "chbEventPasscode"
        Me.chbEventPasscode.Size = New System.Drawing.Size(77, 17)
        Me.chbEventPasscode.TabIndex = 5
        Me.chbEventPasscode.Text = "GA123456"
        Me.chbEventPasscode.UseVisualStyleBackColor = True
        '
        'btnMapEventLocation
        '
        Me.btnMapEventLocation.AutoSize = True
        Me.btnMapEventLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMapEventLocation.Location = New System.Drawing.Point(698, 82)
        Me.btnMapEventLocation.Name = "btnMapEventLocation"
        Me.btnMapEventLocation.Size = New System.Drawing.Size(47, 23)
        Me.btnMapEventLocation.TabIndex = 16
        Me.btnMapEventLocation.Text = "Map It"
        Me.btnMapEventLocation.UseVisualStyleBackColor = True
        '
        'mtbEventZipCode
        '
        Me.mtbEventZipCode.Location = New System.Drawing.Point(620, 83)
        Me.mtbEventZipCode.Mask = "00000-9999"
        Me.mtbEventZipCode.Name = "mtbEventZipCode"
        Me.mtbEventZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbEventZipCode.TabIndex = 15
        Me.mtbEventZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbEventState
        '
        Me.mtbEventState.Location = New System.Drawing.Point(582, 83)
        Me.mtbEventState.Mask = "aa"
        Me.mtbEventState.Name = "mtbEventState"
        Me.mtbEventState.Size = New System.Drawing.Size(32, 20)
        Me.mtbEventState.TabIndex = 14
        Me.mtbEventState.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtEventCity
        '
        Me.txtEventCity.Location = New System.Drawing.Point(464, 83)
        Me.txtEventCity.Name = "txtEventCity"
        Me.txtEventCity.Size = New System.Drawing.Size(112, 20)
        Me.txtEventCity.TabIndex = 13
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(414, 64)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(48, 13)
        Me.Label15.TabIndex = 433
        Me.Label15.Text = "Address:"
        '
        'txtEventAddress
        '
        Me.txtEventAddress.Location = New System.Drawing.Point(464, 61)
        Me.txtEventAddress.Name = "txtEventAddress"
        Me.txtEventAddress.Size = New System.Drawing.Size(224, 20)
        Me.txtEventAddress.TabIndex = 12
        '
        'mtbEventWebPhoneNumber
        '
        Me.mtbEventWebPhoneNumber.Location = New System.Drawing.Point(674, 15)
        Me.mtbEventWebPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbEventWebPhoneNumber.Name = "mtbEventWebPhoneNumber"
        Me.mtbEventWebPhoneNumber.Size = New System.Drawing.Size(86, 20)
        Me.mtbEventWebPhoneNumber.TabIndex = 10
        Me.mtbEventWebPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(618, 19)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 13)
        Me.Label14.TabIndex = 430
        Me.Label14.Text = "Phone No:"
        '
        'cboEventWebContact
        '
        Me.cboEventWebContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventWebContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventWebContact.FormattingEnabled = True
        Me.cboEventWebContact.Location = New System.Drawing.Point(464, 15)
        Me.cboEventWebContact.Name = "cboEventWebContact"
        Me.cboEventWebContact.Size = New System.Drawing.Size(142, 21)
        Me.cboEventWebContact.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(389, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 427
        Me.Label13.Text = "Web Contact:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(36, 127)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 426
        Me.Label12.Text = "Time:"
        '
        'txtEventTime
        '
        Me.txtEventTime.Location = New System.Drawing.Point(71, 123)
        Me.txtEventTime.Name = "txtEventTime"
        Me.txtEventTime.Size = New System.Drawing.Size(121, 20)
        Me.txtEventTime.TabIndex = 4
        '
        'btnDeleteEvent
        '
        Me.btnDeleteEvent.AutoSize = True
        Me.btnDeleteEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteEvent.Location = New System.Drawing.Point(666, 249)
        Me.btnDeleteEvent.Name = "btnDeleteEvent"
        Me.btnDeleteEvent.Size = New System.Drawing.Size(79, 23)
        Me.btnDeleteEvent.TabIndex = 21
        Me.btnDeleteEvent.Text = "Delete Event"
        Me.btnDeleteEvent.UseVisualStyleBackColor = True
        '
        'btnUpdateEvent
        '
        Me.btnUpdateEvent.AutoSize = True
        Me.btnUpdateEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateEvent.Location = New System.Drawing.Point(199, 249)
        Me.btnUpdateEvent.Name = "btnUpdateEvent"
        Me.btnUpdateEvent.Size = New System.Drawing.Size(122, 23)
        Me.btnUpdateEvent.TabIndex = 20
        Me.btnUpdateEvent.Text = "Update Existing Event"
        Me.btnUpdateEvent.UseVisualStyleBackColor = True
        '
        'btnSaveNewEvent
        '
        Me.btnSaveNewEvent.AutoSize = True
        Me.btnSaveNewEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveNewEvent.Location = New System.Drawing.Point(81, 249)
        Me.btnSaveNewEvent.Name = "btnSaveNewEvent"
        Me.btnSaveNewEvent.Size = New System.Drawing.Size(104, 23)
        Me.btnSaveNewEvent.TabIndex = 19
        Me.btnSaveNewEvent.Text = "Create New Event"
        Me.btnSaveNewEvent.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(424, 153)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 420
        Me.Label9.Text = "Notes:"
        '
        'txtEventNotes
        '
        Me.txtEventNotes.Location = New System.Drawing.Point(464, 150)
        Me.txtEventNotes.Multiline = True
        Me.txtEventNotes.Name = "txtEventNotes"
        Me.txtEventNotes.Size = New System.Drawing.Size(281, 51)
        Me.txtEventNotes.TabIndex = 18
        '
        'mtbEventCapacity
        '
        Me.mtbEventCapacity.Location = New System.Drawing.Point(464, 120)
        Me.mtbEventCapacity.Mask = "00000"
        Me.mtbEventCapacity.Name = "mtbEventCapacity"
        Me.mtbEventCapacity.Size = New System.Drawing.Size(42, 20)
        Me.mtbEventCapacity.TabIndex = 17
        Me.mtbEventCapacity.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbEventCapacity.ValidatingType = GetType(Integer)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(411, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 417
        Me.Label8.Text = "Capacity:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(421, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 415
        Me.Label7.Text = "Venue:"
        '
        'DTPEventDate
        '
        Me.DTPEventDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventDate.Location = New System.Drawing.Point(71, 98)
        Me.DTPEventDate.Name = "DTPEventDate"
        Me.DTPEventDate.Size = New System.Drawing.Size(89, 20)
        Me.DTPEventDate.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Date:"
        '
        'txtEventVenue
        '
        Me.txtEventVenue.Location = New System.Drawing.Point(464, 38)
        Me.txtEventVenue.Name = "txtEventVenue"
        Me.txtEventVenue.Size = New System.Drawing.Size(121, 20)
        Me.txtEventVenue.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Description:"
        '
        'txtEventDescription
        '
        Me.txtEventDescription.Location = New System.Drawing.Point(71, 41)
        Me.txtEventDescription.Multiline = True
        Me.txtEventDescription.Name = "txtEventDescription"
        Me.txtEventDescription.Size = New System.Drawing.Size(305, 51)
        Me.txtEventDescription.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Event Title:"
        '
        'cboEventStatus
        '
        Me.cboEventStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventStatus.FormattingEnabled = True
        Me.cboEventStatus.Location = New System.Drawing.Point(88, 176)
        Me.cboEventStatus.Name = "cboEventStatus"
        Me.cboEventStatus.Size = New System.Drawing.Size(154, 21)
        Me.cboEventStatus.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 179)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Event Status:"
        '
        'txtEventTitle
        '
        Me.txtEventTitle.Location = New System.Drawing.Point(71, 12)
        Me.txtEventTitle.Name = "txtEventTitle"
        Me.txtEventTitle.Size = New System.Drawing.Size(305, 20)
        Me.txtEventTitle.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 235)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Event ID:"
        Me.Label1.Visible = False
        '
        'txtEventID
        '
        Me.txtEventID.Location = New System.Drawing.Point(39, 251)
        Me.txtEventID.Name = "txtEventID"
        Me.txtEventID.ReadOnly = True
        Me.txtEventID.Size = New System.Drawing.Size(36, 20)
        Me.txtEventID.TabIndex = 0
        Me.txtEventID.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPEventOverview)
        Me.TabControl1.Controls.Add(Me.TPEventManagement)
        Me.TabControl1.Controls.Add(Me.TPRegistrationManagement)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 160)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(792, 384)
        Me.TabControl1.TabIndex = 259
        '
        'TPEventOverview
        '
        Me.TPEventOverview.Controls.Add(Me.dgvOverviewRegistrants)
        Me.TPEventOverview.Controls.Add(Me.Panel9)
        Me.TPEventOverview.Location = New System.Drawing.Point(4, 22)
        Me.TPEventOverview.Name = "TPEventOverview"
        Me.TPEventOverview.Size = New System.Drawing.Size(784, 358)
        Me.TPEventOverview.TabIndex = 3
        Me.TPEventOverview.Text = "Event Overview"
        Me.TPEventOverview.UseVisualStyleBackColor = True
        '
        'dgvOverviewRegistrants
        '
        Me.dgvOverviewRegistrants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOverviewRegistrants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOverviewRegistrants.Location = New System.Drawing.Point(0, 168)
        Me.dgvOverviewRegistrants.Name = "dgvOverviewRegistrants"
        Me.dgvOverviewRegistrants.Size = New System.Drawing.Size(784, 190)
        Me.dgvOverviewRegistrants.TabIndex = 421
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.lblNotes)
        Me.Panel9.Controls.Add(Me.lblVenue)
        Me.Panel9.Controls.Add(Me.lblAPBContact)
        Me.Panel9.Controls.Add(Me.lblEventContact)
        Me.Panel9.Controls.Add(Me.lblWaitingList)
        Me.Panel9.Controls.Add(Me.lblEventCapacity)
        Me.Panel9.Controls.Add(Me.lblNumberRegistered)
        Me.Panel9.Controls.Add(Me.lblEventStatus)
        Me.Panel9.Controls.Add(Me.lblPassCode)
        Me.Panel9.Controls.Add(Me.lblLogInRequired)
        Me.Panel9.Controls.Add(Me.lblEventDateTime)
        Me.Panel9.Controls.Add(Me.lblEvent)
        Me.Panel9.Controls.Add(Me.lblDescription)
        Me.Panel9.Controls.Add(Me.txtEmails)
        Me.Panel9.Controls.Add(Me.Label41)
        Me.Panel9.Controls.Add(Me.Label34)
        Me.Panel9.Controls.Add(Me.Label35)
        Me.Panel9.Controls.Add(Me.btnEmailWaitList)
        Me.Panel9.Controls.Add(Me.Label36)
        Me.Panel9.Controls.Add(Me.btnEmailRegistrants)
        Me.Panel9.Controls.Add(Me.Label37)
        Me.Panel9.Controls.Add(Me.btnEmailAll)
        Me.Panel9.Controls.Add(Me.Label38)
        Me.Panel9.Controls.Add(Me.btnExportToExcel)
        Me.Panel9.Controls.Add(Me.Label39)
        Me.Panel9.Controls.Add(Me.Label40)
        Me.Panel9.Controls.Add(Me.Label42)
        Me.Panel9.Controls.Add(Me.Label43)
        Me.Panel9.Controls.Add(Me.Label44)
        Me.Panel9.Controls.Add(Me.Label45)
        Me.Panel9.Controls.Add(Me.Label46)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(784, 168)
        Me.Panel9.TabIndex = 422
        '
        'lblNotes
        '
        Me.lblNotes.Location = New System.Drawing.Point(464, 90)
        Me.lblNotes.Multiline = True
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.ReadOnly = True
        Me.lblNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblNotes.Size = New System.Drawing.Size(303, 41)
        Me.lblNotes.TabIndex = 436
        '
        'lblVenue
        '
        Me.lblVenue.Location = New System.Drawing.Point(464, 43)
        Me.lblVenue.Multiline = True
        Me.lblVenue.Name = "lblVenue"
        Me.lblVenue.ReadOnly = True
        Me.lblVenue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblVenue.Size = New System.Drawing.Size(303, 46)
        Me.lblVenue.TabIndex = 435
        '
        'lblAPBContact
        '
        Me.lblAPBContact.Location = New System.Drawing.Point(494, 23)
        Me.lblAPBContact.Multiline = True
        Me.lblAPBContact.Name = "lblAPBContact"
        Me.lblAPBContact.ReadOnly = True
        Me.lblAPBContact.Size = New System.Drawing.Size(273, 20)
        Me.lblAPBContact.TabIndex = 434
        '
        'lblEventContact
        '
        Me.lblEventContact.Location = New System.Drawing.Point(494, 3)
        Me.lblEventContact.Multiline = True
        Me.lblEventContact.Name = "lblEventContact"
        Me.lblEventContact.ReadOnly = True
        Me.lblEventContact.Size = New System.Drawing.Size(273, 20)
        Me.lblEventContact.TabIndex = 433
        '
        'lblWaitingList
        '
        Me.lblWaitingList.Location = New System.Drawing.Point(260, 140)
        Me.lblWaitingList.Multiline = True
        Me.lblWaitingList.Name = "lblWaitingList"
        Me.lblWaitingList.ReadOnly = True
        Me.lblWaitingList.Size = New System.Drawing.Size(43, 20)
        Me.lblWaitingList.TabIndex = 432
        '
        'lblEventCapacity
        '
        Me.lblEventCapacity.Location = New System.Drawing.Point(148, 140)
        Me.lblEventCapacity.Multiline = True
        Me.lblEventCapacity.Name = "lblEventCapacity"
        Me.lblEventCapacity.ReadOnly = True
        Me.lblEventCapacity.Size = New System.Drawing.Size(43, 20)
        Me.lblEventCapacity.TabIndex = 431
        '
        'lblNumberRegistered
        '
        Me.lblNumberRegistered.Location = New System.Drawing.Point(77, 140)
        Me.lblNumberRegistered.Multiline = True
        Me.lblNumberRegistered.Name = "lblNumberRegistered"
        Me.lblNumberRegistered.ReadOnly = True
        Me.lblNumberRegistered.Size = New System.Drawing.Size(43, 20)
        Me.lblNumberRegistered.TabIndex = 430
        '
        'lblEventStatus
        '
        Me.lblEventStatus.Location = New System.Drawing.Point(100, 114)
        Me.lblEventStatus.Multiline = True
        Me.lblEventStatus.Name = "lblEventStatus"
        Me.lblEventStatus.ReadOnly = True
        Me.lblEventStatus.Size = New System.Drawing.Size(140, 20)
        Me.lblEventStatus.TabIndex = 429
        '
        'lblPassCode
        '
        Me.lblPassCode.Location = New System.Drawing.Point(100, 94)
        Me.lblPassCode.Multiline = True
        Me.lblPassCode.Name = "lblPassCode"
        Me.lblPassCode.ReadOnly = True
        Me.lblPassCode.Size = New System.Drawing.Size(140, 20)
        Me.lblPassCode.TabIndex = 428
        '
        'lblLogInRequired
        '
        Me.lblLogInRequired.Location = New System.Drawing.Point(100, 75)
        Me.lblLogInRequired.Multiline = True
        Me.lblLogInRequired.Name = "lblLogInRequired"
        Me.lblLogInRequired.ReadOnly = True
        Me.lblLogInRequired.Size = New System.Drawing.Size(140, 20)
        Me.lblLogInRequired.TabIndex = 427
        '
        'lblEventDateTime
        '
        Me.lblEventDateTime.Location = New System.Drawing.Point(100, 56)
        Me.lblEventDateTime.Multiline = True
        Me.lblEventDateTime.Name = "lblEventDateTime"
        Me.lblEventDateTime.ReadOnly = True
        Me.lblEventDateTime.Size = New System.Drawing.Size(287, 20)
        Me.lblEventDateTime.TabIndex = 426
        '
        'lblEvent
        '
        Me.lblEvent.Location = New System.Drawing.Point(52, 3)
        Me.lblEvent.Multiline = True
        Me.lblEvent.Name = "lblEvent"
        Me.lblEvent.ReadOnly = True
        Me.lblEvent.Size = New System.Drawing.Size(335, 20)
        Me.lblEvent.TabIndex = 425
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(71, 23)
        Me.lblDescription.Multiline = True
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.ReadOnly = True
        Me.lblDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblDescription.Size = New System.Drawing.Size(316, 33)
        Me.lblDescription.TabIndex = 424
        '
        'txtEmails
        '
        Me.txtEmails.Location = New System.Drawing.Point(473, 142)
        Me.txtEmails.Multiline = True
        Me.txtEmails.Name = "txtEmails"
        Me.txtEmails.Size = New System.Drawing.Size(16, 11)
        Me.txtEmails.TabIndex = 423
        Me.txtEmails.Visible = False
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(8, 79)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(86, 13)
        Me.Label41.TabIndex = 421
        Me.Label41.Text = "Log In Required:"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(8, 7)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(38, 13)
        Me.Label34.TabIndex = 4
        Me.Label34.Text = "Event:"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(417, 7)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(78, 13)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "Event Contact:"
        '
        'btnEmailWaitList
        '
        Me.btnEmailWaitList.AutoSize = True
        Me.btnEmailWaitList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailWaitList.Location = New System.Drawing.Point(687, 140)
        Me.btnEmailWaitList.Name = "btnEmailWaitList"
        Me.btnEmailWaitList.Size = New System.Drawing.Size(89, 23)
        Me.btnEmailWaitList.TabIndex = 420
        Me.btnEmailWaitList.Text = "Email: Wait List"
        Me.btnEmailWaitList.UseVisualStyleBackColor = True
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(195, 147)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 13)
        Me.Label36.TabIndex = 6
        Me.Label36.Text = "Waiting List:"
        '
        'btnEmailRegistrants
        '
        Me.btnEmailRegistrants.AutoSize = True
        Me.btnEmailRegistrants.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailRegistrants.Location = New System.Drawing.Point(580, 140)
        Me.btnEmailRegistrants.Name = "btnEmailRegistrants"
        Me.btnEmailRegistrants.Size = New System.Drawing.Size(101, 23)
        Me.btnEmailRegistrants.TabIndex = 419
        Me.btnEmailRegistrants.Text = "Email: Registrants"
        Me.btnEmailRegistrants.UseVisualStyleBackColor = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(8, 118)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(71, 13)
        Me.Label37.TabIndex = 7
        Me.Label37.Text = "Event Status:"
        '
        'btnEmailAll
        '
        Me.btnEmailAll.AutoSize = True
        Me.btnEmailAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailAll.Location = New System.Drawing.Point(515, 140)
        Me.btnEmailAll.Name = "btnEmailAll"
        Me.btnEmailAll.Size = New System.Drawing.Size(59, 23)
        Me.btnEmailAll.TabIndex = 418
        Me.btnEmailAll.Text = "Email: All"
        Me.btnEmailAll.UseVisualStyleBackColor = True
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(129, 147)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(16, 13)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "of"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.AutoSize = True
        Me.btnExportToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportToExcel.Location = New System.Drawing.Point(420, 140)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(47, 23)
        Me.btnExportToExcel.TabIndex = 417
        Me.btnExportToExcel.Text = "Export"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(8, 98)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(58, 13)
        Me.Label39.TabIndex = 9
        Me.Label39.Text = "PassCode:"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(8, 147)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(66, 13)
        Me.Label40.TabIndex = 10
        Me.Label40.Text = "Registration:"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(8, 60)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(92, 13)
        Me.Label42.TabIndex = 12
        Me.Label42.Text = "Event Date/Time:"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(8, 27)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(63, 13)
        Me.Label43.TabIndex = 13
        Me.Label43.Text = "Description:"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(420, 93)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(38, 13)
        Me.Label44.TabIndex = 15
        Me.Label44.Text = "Notes:"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(417, 47)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(41, 13)
        Me.Label45.TabIndex = 16
        Me.Label45.Text = "Venue:"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(417, 27)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(71, 13)
        Me.Label46.TabIndex = 17
        Me.Label46.Text = "APB Contact:"
        '
        'TPEventManagement
        '
        Me.TPEventManagement.Controls.Add(Me.Panel4)
        Me.TPEventManagement.Location = New System.Drawing.Point(4, 22)
        Me.TPEventManagement.Name = "TPEventManagement"
        Me.TPEventManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEventManagement.Size = New System.Drawing.Size(784, 358)
        Me.TPEventManagement.TabIndex = 0
        Me.TPEventManagement.Text = "Event Management"
        Me.TPEventManagement.UseVisualStyleBackColor = True
        '
        'TPRegistrationManagement
        '
        Me.TPRegistrationManagement.Controls.Add(Me.dgvRegistrationManagement)
        Me.TPRegistrationManagement.Controls.Add(Me.Panel8)
        Me.TPRegistrationManagement.Location = New System.Drawing.Point(4, 22)
        Me.TPRegistrationManagement.Name = "TPRegistrationManagement"
        Me.TPRegistrationManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.TPRegistrationManagement.Size = New System.Drawing.Size(784, 358)
        Me.TPRegistrationManagement.TabIndex = 1
        Me.TPRegistrationManagement.Text = "Registration Management"
        Me.TPRegistrationManagement.UseVisualStyleBackColor = True
        '
        'dgvRegistrationManagement
        '
        Me.dgvRegistrationManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRegistrationManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRegistrationManagement.Location = New System.Drawing.Point(3, 3)
        Me.dgvRegistrationManagement.Name = "dgvRegistrationManagement"
        Me.dgvRegistrationManagement.Size = New System.Drawing.Size(404, 352)
        Me.dgvRegistrationManagement.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.btnModifyRegistration)
        Me.Panel8.Controls.Add(Me.Label10)
        Me.Panel8.Controls.Add(Me.txtRegID)
        Me.Panel8.Controls.Add(Me.Label33)
        Me.Panel8.Controls.Add(Me.txtRegEmail)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.txtRegComments)
        Me.Panel8.Controls.Add(Me.DTPRegDateRegistered)
        Me.Panel8.Controls.Add(Me.Label31)
        Me.Panel8.Controls.Add(Me.txtGECOUserID)
        Me.Panel8.Controls.Add(Me.cboRegUserType)
        Me.Panel8.Controls.Add(Me.Label30)
        Me.Panel8.Controls.Add(Me.mtbRegPhoneExt)
        Me.Panel8.Controls.Add(Me.mtbRegPhoneNo)
        Me.Panel8.Controls.Add(Me.Label28)
        Me.Panel8.Controls.Add(Me.mtbRegZipCode)
        Me.Panel8.Controls.Add(Me.mtbRegState)
        Me.Panel8.Controls.Add(Me.txtRegCity)
        Me.Panel8.Controls.Add(Me.Label27)
        Me.Panel8.Controls.Add(Me.txtRegAddress)
        Me.Panel8.Controls.Add(Me.Label26)
        Me.Panel8.Controls.Add(Me.txtRegTitle)
        Me.Panel8.Controls.Add(Me.Label25)
        Me.Panel8.Controls.Add(Me.txtRegLastName)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.txtRegFirstName)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Controls.Add(Me.txtRegSalutation)
        Me.Panel8.Controls.Add(Me.cboRegStatus)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.txtRegConfirmationNum)
        Me.Panel8.Controls.Add(Me.Label19)
        Me.Panel8.Controls.Add(Me.Label6)
        Me.Panel8.Controls.Add(Me.txtRegEventTitle)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(407, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(374, 352)
        Me.Panel8.TabIndex = 1
        '
        'btnModifyRegistration
        '
        Me.btnModifyRegistration.AutoSize = True
        Me.btnModifyRegistration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnModifyRegistration.Location = New System.Drawing.Point(275, 70)
        Me.btnModifyRegistration.Name = "btnModifyRegistration"
        Me.btnModifyRegistration.Size = New System.Drawing.Size(85, 23)
        Me.btnModifyRegistration.TabIndex = 467
        Me.btnModifyRegistration.Text = "Update Status"
        Me.btnModifyRegistration.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(283, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 466
        Me.Label10.Text = "Reg. ID"
        Me.Label10.Visible = False
        '
        'txtRegID
        '
        Me.txtRegID.Location = New System.Drawing.Point(333, 28)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(36, 20)
        Me.txtRegID.TabIndex = 465
        Me.txtRegID.Visible = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(16, 216)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(35, 13)
        Me.Label33.TabIndex = 464
        Me.Label33.Text = "Email:"
        '
        'txtRegEmail
        '
        Me.txtRegEmail.Location = New System.Drawing.Point(53, 212)
        Me.txtRegEmail.Name = "txtRegEmail"
        Me.txtRegEmail.ReadOnly = True
        Me.txtRegEmail.Size = New System.Drawing.Size(224, 20)
        Me.txtRegEmail.TabIndex = 463
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(4, 282)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 13)
        Me.Label21.TabIndex = 462
        Me.Label21.Text = "Comments:"
        '
        'txtRegComments
        '
        Me.txtRegComments.AcceptsReturn = True
        Me.txtRegComments.Location = New System.Drawing.Point(65, 282)
        Me.txtRegComments.Multiline = True
        Me.txtRegComments.Name = "txtRegComments"
        Me.txtRegComments.ReadOnly = True
        Me.txtRegComments.Size = New System.Drawing.Size(281, 41)
        Me.txtRegComments.TabIndex = 461
        '
        'DTPRegDateRegistered
        '
        Me.DTPRegDateRegistered.CustomFormat = "dd-MMM-yy hh:mm:ss tt"
        Me.DTPRegDateRegistered.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPRegDateRegistered.Location = New System.Drawing.Point(102, 24)
        Me.DTPRegDateRegistered.Name = "DTPRegDateRegistered"
        Me.DTPRegDateRegistered.Size = New System.Drawing.Size(147, 20)
        Me.DTPRegDateRegistered.TabIndex = 460
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(261, 54)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(68, 13)
        Me.Label31.TabIndex = 459
        Me.Label31.Text = "GECOuserID"
        Me.Label31.Visible = False
        '
        'txtGECOUserID
        '
        Me.txtGECOUserID.Location = New System.Drawing.Point(333, 50)
        Me.txtGECOUserID.Name = "txtGECOUserID"
        Me.txtGECOUserID.ReadOnly = True
        Me.txtGECOUserID.Size = New System.Drawing.Size(36, 20)
        Me.txtGECOUserID.TabIndex = 458
        Me.txtGECOUserID.Visible = False
        '
        'cboRegUserType
        '
        Me.cboRegUserType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegUserType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegUserType.Enabled = False
        Me.cboRegUserType.FormattingEnabled = True
        Me.cboRegUserType.Location = New System.Drawing.Point(65, 258)
        Me.cboRegUserType.Name = "cboRegUserType"
        Me.cboRegUserType.Size = New System.Drawing.Size(154, 21)
        Me.cboRegUserType.TabIndex = 457
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(4, 262)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(59, 13)
        Me.Label30.TabIndex = 456
        Me.Label30.Text = "User Type:"
        '
        'mtbRegPhoneExt
        '
        Me.mtbRegPhoneExt.Location = New System.Drawing.Point(158, 235)
        Me.mtbRegPhoneExt.Mask = "00000"
        Me.mtbRegPhoneExt.Name = "mtbRegPhoneExt"
        Me.mtbRegPhoneExt.ReadOnly = True
        Me.mtbRegPhoneExt.Size = New System.Drawing.Size(32, 20)
        Me.mtbRegPhoneExt.TabIndex = 452
        Me.mtbRegPhoneExt.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbRegPhoneExt.ValidatingType = GetType(Integer)
        '
        'mtbRegPhoneNo
        '
        Me.mtbRegPhoneNo.Location = New System.Drawing.Point(66, 235)
        Me.mtbRegPhoneNo.Mask = "(999) 000-0000"
        Me.mtbRegPhoneNo.Name = "mtbRegPhoneNo"
        Me.mtbRegPhoneNo.ReadOnly = True
        Me.mtbRegPhoneNo.Size = New System.Drawing.Size(86, 20)
        Me.mtbRegPhoneNo.TabIndex = 450
        Me.mtbRegPhoneNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(8, 239)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(58, 13)
        Me.Label28.TabIndex = 451
        Me.Label28.Text = "Phone No:"
        '
        'mtbRegZipCode
        '
        Me.mtbRegZipCode.Location = New System.Drawing.Point(209, 189)
        Me.mtbRegZipCode.Mask = "00000-9999"
        Me.mtbRegZipCode.Name = "mtbRegZipCode"
        Me.mtbRegZipCode.ReadOnly = True
        Me.mtbRegZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbRegZipCode.TabIndex = 448
        Me.mtbRegZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbRegState
        '
        Me.mtbRegState.Location = New System.Drawing.Point(171, 189)
        Me.mtbRegState.Mask = "aa"
        Me.mtbRegState.Name = "mtbRegState"
        Me.mtbRegState.ReadOnly = True
        Me.mtbRegState.Size = New System.Drawing.Size(32, 20)
        Me.mtbRegState.TabIndex = 447
        Me.mtbRegState.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtRegCity
        '
        Me.txtRegCity.Location = New System.Drawing.Point(53, 189)
        Me.txtRegCity.Name = "txtRegCity"
        Me.txtRegCity.ReadOnly = True
        Me.txtRegCity.Size = New System.Drawing.Size(112, 20)
        Me.txtRegCity.TabIndex = 446
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(3, 170)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(48, 13)
        Me.Label27.TabIndex = 449
        Me.Label27.Text = "Address:"
        '
        'txtRegAddress
        '
        Me.txtRegAddress.Location = New System.Drawing.Point(53, 167)
        Me.txtRegAddress.Name = "txtRegAddress"
        Me.txtRegAddress.ReadOnly = True
        Me.txtRegAddress.Size = New System.Drawing.Size(224, 20)
        Me.txtRegAddress.TabIndex = 445
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(4, 146)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(30, 13)
        Me.Label26.TabIndex = 444
        Me.Label26.Text = "Title:"
        '
        'txtRegTitle
        '
        Me.txtRegTitle.Location = New System.Drawing.Point(36, 143)
        Me.txtRegTitle.Name = "txtRegTitle"
        Me.txtRegTitle.ReadOnly = True
        Me.txtRegTitle.Size = New System.Drawing.Size(121, 20)
        Me.txtRegTitle.TabIndex = 443
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(188, 123)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 13)
        Me.Label25.TabIndex = 442
        Me.Label25.Text = "Last Name:"
        '
        'txtRegLastName
        '
        Me.txtRegLastName.Location = New System.Drawing.Point(249, 119)
        Me.txtRegLastName.Name = "txtRegLastName"
        Me.txtRegLastName.ReadOnly = True
        Me.txtRegLastName.Size = New System.Drawing.Size(121, 20)
        Me.txtRegLastName.TabIndex = 441
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(4, 123)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(60, 13)
        Me.Label24.TabIndex = 440
        Me.Label24.Text = "First Name:"
        '
        'txtRegFirstName
        '
        Me.txtRegFirstName.Location = New System.Drawing.Point(66, 119)
        Me.txtRegFirstName.Name = "txtRegFirstName"
        Me.txtRegFirstName.ReadOnly = True
        Me.txtRegFirstName.Size = New System.Drawing.Size(121, 20)
        Me.txtRegFirstName.TabIndex = 439
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(4, 99)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 13)
        Me.Label23.TabIndex = 438
        Me.Label23.Text = "Salutation:"
        '
        'txtRegSalutation
        '
        Me.txtRegSalutation.Location = New System.Drawing.Point(64, 95)
        Me.txtRegSalutation.Name = "txtRegSalutation"
        Me.txtRegSalutation.ReadOnly = True
        Me.txtRegSalutation.Size = New System.Drawing.Size(81, 20)
        Me.txtRegSalutation.TabIndex = 437
        '
        'cboRegStatus
        '
        Me.cboRegStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegStatus.FormattingEnabled = True
        Me.cboRegStatus.Location = New System.Drawing.Point(111, 70)
        Me.cboRegStatus.Name = "cboRegStatus"
        Me.cboRegStatus.Size = New System.Drawing.Size(154, 21)
        Me.cboRegStatus.TabIndex = 436
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(4, 74)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(99, 13)
        Me.Label22.TabIndex = 435
        Me.Label22.Text = "Registration Status:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(4, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(78, 13)
        Me.Label20.TabIndex = 432
        Me.Label20.Text = "Confirmation #:"
        '
        'txtRegConfirmationNum
        '
        Me.txtRegConfirmationNum.Location = New System.Drawing.Point(89, 46)
        Me.txtRegConfirmationNum.Name = "txtRegConfirmationNum"
        Me.txtRegConfirmationNum.ReadOnly = True
        Me.txtRegConfirmationNum.Size = New System.Drawing.Size(121, 20)
        Me.txtRegConfirmationNum.TabIndex = 431
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(4, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(87, 13)
        Me.Label19.TabIndex = 430
        Me.Label19.Text = "Date Registered:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 428
        Me.Label6.Text = "Event Title:"
        '
        'txtRegEventTitle
        '
        Me.txtRegEventTitle.Location = New System.Drawing.Point(73, 3)
        Me.txtRegEventTitle.Name = "txtRegEventTitle"
        Me.txtRegEventTitle.ReadOnly = True
        Me.txtRegEventTitle.Size = New System.Drawing.Size(247, 20)
        Me.txtRegEventTitle.TabIndex = 427
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.dgvRegistrationEvent)
        Me.Panel5.Controls.Add(Me.Panel7)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(792, 160)
        Me.Panel5.TabIndex = 260
        '
        'dgvRegistrationEvent
        '
        Me.dgvRegistrationEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRegistrationEvent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRegistrationEvent.Location = New System.Drawing.Point(0, 0)
        Me.dgvRegistrationEvent.Name = "dgvRegistrationEvent"
        Me.dgvRegistrationEvent.Size = New System.Drawing.Size(686, 125)
        Me.dgvRegistrationEvent.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.txtSelectedEventID)
        Me.Panel7.Controls.Add(Me.lblEventTitle)
        Me.Panel7.Controls.Add(Me.btnViewDetails)
        Me.Panel7.Controls.Add(Me.lblEVentDate)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 125)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(686, 35)
        Me.Panel7.TabIndex = 418
        '
        'txtSelectedEventID
        '
        Me.txtSelectedEventID.Location = New System.Drawing.Point(644, 6)
        Me.txtSelectedEventID.Name = "txtSelectedEventID"
        Me.txtSelectedEventID.ReadOnly = True
        Me.txtSelectedEventID.Size = New System.Drawing.Size(36, 20)
        Me.txtSelectedEventID.TabIndex = 417
        Me.txtSelectedEventID.Visible = False
        '
        'lblEventTitle
        '
        Me.lblEventTitle.AutoSize = True
        Me.lblEventTitle.Location = New System.Drawing.Point(3, 10)
        Me.lblEventTitle.Name = "lblEventTitle"
        Me.lblEventTitle.Size = New System.Drawing.Size(58, 13)
        Me.lblEventTitle.TabIndex = 3
        Me.lblEventTitle.Text = "Event Title"
        '
        'btnViewDetails
        '
        Me.btnViewDetails.Location = New System.Drawing.Point(563, 3)
        Me.btnViewDetails.Name = "btnViewDetails"
        Me.btnViewDetails.Size = New System.Drawing.Size(75, 23)
        Me.btnViewDetails.TabIndex = 416
        Me.btnViewDetails.Text = "View Details"
        Me.btnViewDetails.UseVisualStyleBackColor = True
        '
        'lblEVentDate
        '
        Me.lblEVentDate.AutoSize = True
        Me.lblEVentDate.Location = New System.Drawing.Point(314, 9)
        Me.lblEVentDate.Name = "lblEVentDate"
        Me.lblEVentDate.Size = New System.Drawing.Size(61, 13)
        Me.lblEVentDate.TabIndex = 5
        Me.lblEVentDate.Text = "Event Date"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnFilterEvents)
        Me.Panel6.Controls.Add(Me.rdbAllEvents)
        Me.Panel6.Controls.Add(Me.rdbPastEvents)
        Me.Panel6.Controls.Add(Me.rdbUpcomingEvents)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel6.Location = New System.Drawing.Point(686, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(106, 160)
        Me.Panel6.TabIndex = 417
        '
        'btnFilterEvents
        '
        Me.btnFilterEvents.Location = New System.Drawing.Point(10, 75)
        Me.btnFilterEvents.Name = "btnFilterEvents"
        Me.btnFilterEvents.Size = New System.Drawing.Size(75, 23)
        Me.btnFilterEvents.TabIndex = 417
        Me.btnFilterEvents.Text = "Filter"
        Me.btnFilterEvents.UseVisualStyleBackColor = True
        '
        'rdbAllEvents
        '
        Me.rdbAllEvents.AutoSize = True
        Me.rdbAllEvents.Location = New System.Drawing.Point(3, 52)
        Me.rdbAllEvents.Name = "rdbAllEvents"
        Me.rdbAllEvents.Size = New System.Drawing.Size(36, 17)
        Me.rdbAllEvents.TabIndex = 2
        Me.rdbAllEvents.Text = "All"
        Me.rdbAllEvents.UseVisualStyleBackColor = True
        '
        'rdbPastEvents
        '
        Me.rdbPastEvents.AutoSize = True
        Me.rdbPastEvents.Location = New System.Drawing.Point(3, 29)
        Me.rdbPastEvents.Name = "rdbPastEvents"
        Me.rdbPastEvents.Size = New System.Drawing.Size(82, 17)
        Me.rdbPastEvents.TabIndex = 1
        Me.rdbPastEvents.Text = "Past Events"
        Me.rdbPastEvents.UseVisualStyleBackColor = True
        '
        'rdbUpcomingEvents
        '
        Me.rdbUpcomingEvents.AutoSize = True
        Me.rdbUpcomingEvents.Checked = True
        Me.rdbUpcomingEvents.Location = New System.Drawing.Point(3, 6)
        Me.rdbUpcomingEvents.Name = "rdbUpcomingEvents"
        Me.rdbUpcomingEvents.Size = New System.Drawing.Size(73, 17)
        Me.rdbUpcomingEvents.TabIndex = 0
        Me.rdbUpcomingEvents.TabStop = True
        Me.rdbUpcomingEvents.Text = "Upcoming"
        Me.rdbUpcomingEvents.UseVisualStyleBackColor = True
        '
        'MASPRegistrationTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Panel5)
        Me.Menu = Me.MainMenu1
        Me.Name = "MASPRegistrationTool"
        Me.Text = "Registration Tool"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TPEventOverview.ResumeLayout(False)
        CType(Me.dgvOverviewRegistrants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.TPEventManagement.ResumeLayout(False)
        Me.TPRegistrationManagement.ResumeLayout(False)
        CType(Me.dgvRegistrationManagement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.dgvRegistrationEvent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiNewApplication As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEventID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEventTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEventDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboEventStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEventVenue As System.Windows.Forms.TextBox
    Friend WithEvents mtbEventCapacity As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DTPEventDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnDeleteEvent As System.Windows.Forms.Button
    Friend WithEvents btnUpdateEvent As System.Windows.Forms.Button
    Friend WithEvents btnSaveNewEvent As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEventNotes As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPEventManagement As System.Windows.Forms.TabPage
    Friend WithEvents TPRegistrationManagement As System.Windows.Forms.TabPage
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dgvRegistrationEvent As System.Windows.Forms.DataGridView
    Friend WithEvents lblEVentDate As System.Windows.Forms.Label
    Friend WithEvents lblEventTitle As System.Windows.Forms.Label
    Friend WithEvents btnViewDetails As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbAllEvents As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPastEvents As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUpcomingEvents As System.Windows.Forms.RadioButton
    Friend WithEvents TPEventOverview As System.Windows.Forms.TabPage
    Friend WithEvents btnFilterEvents As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEventTime As System.Windows.Forms.TextBox
    Friend WithEvents mtbEventWebPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboEventWebContact As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnMapEventLocation As System.Windows.Forms.Button
    Friend WithEvents mtbEventZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbEventState As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEventCity As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtEventAddress As System.Windows.Forms.TextBox
    Friend WithEvents mtbEventPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cboEventContact As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnGeneratePasscode As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chbEventPasscode As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents txtSelectedEventID As System.Windows.Forms.TextBox
    Friend WithEvents btnClearEventManagement As System.Windows.Forms.Button
    Friend WithEvents dgvRegistrationManagement As System.Windows.Forms.DataGridView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRegEventTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtRegConfirmationNum As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtRegLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtRegFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtRegSalutation As System.Windows.Forms.TextBox
    Friend WithEvents cboRegStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtRegTitle As System.Windows.Forms.TextBox
    Friend WithEvents DTPRegDateRegistered As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtGECOUserID As System.Windows.Forms.TextBox
    Friend WithEvents cboRegUserType As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents mtbRegPhoneExt As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbRegPhoneNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents mtbRegZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbRegState As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtRegCity As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtRegAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRegComments As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents DTPEventEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtRegEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents dgvOverviewRegistrants As System.Windows.Forms.DataGridView
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents btnEmailWaitList As System.Windows.Forms.Button
    Friend WithEvents btnEmailRegistrants As System.Windows.Forms.Button
    Friend WithEvents btnEmailAll As System.Windows.Forms.Button
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtWebsiteURL As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtEventEndTime As System.Windows.Forms.TextBox
    Friend WithEvents chbGECOlogInRequired As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnModifyRegistration As System.Windows.Forms.Button
    Friend WithEvents txtEmails As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblNumberRegistered As System.Windows.Forms.TextBox
    Friend WithEvents lblEventStatus As System.Windows.Forms.TextBox
    Friend WithEvents lblPassCode As System.Windows.Forms.TextBox
    Friend WithEvents lblLogInRequired As System.Windows.Forms.TextBox
    Friend WithEvents lblEventDateTime As System.Windows.Forms.TextBox
    Friend WithEvents lblEvent As System.Windows.Forms.TextBox
    Friend WithEvents lblWaitingList As System.Windows.Forms.TextBox
    Friend WithEvents lblEventCapacity As System.Windows.Forms.TextBox
    Friend WithEvents lblAPBContact As System.Windows.Forms.TextBox
    Friend WithEvents lblEventContact As System.Windows.Forms.TextBox
    Friend WithEvents lblNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblVenue As System.Windows.Forms.TextBox
End Class
