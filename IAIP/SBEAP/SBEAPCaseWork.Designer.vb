<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPCaseWork
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SBEAPCaseWork))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.tsbClientSearch = New System.Windows.Forms.ToolStripButton
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton
        Me.tsbClearFrom = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Label1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiAddNewClient = New System.Windows.Forms.ToolStripMenuItem
        Me.tsmDeleteCaseWork = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtCaseID = New System.Windows.Forms.TextBox
        Me.Label199 = New System.Windows.Forms.Label
        Me.Label299 = New System.Windows.Forms.Label
        Me.txtCaseNotes = New System.Windows.Forms.TextBox
        Me.Label399 = New System.Windows.Forms.Label
        Me.cboInteragency = New System.Windows.Forms.ComboBox
        Me.cboStaffResponsible = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.DTPCaseOpened = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboActionType = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.DTPCaseClosed = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtLastModifingStaff = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.DTPLastModified = New System.Windows.Forms.DateTimePicker
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtClientID = New System.Windows.Forms.TextBox
        Me.txtClientInformation = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtOutstandingCases = New System.Windows.Forms.TextBox
        Me.btnRefreshClient = New System.Windows.Forms.Button
        Me.TCCaseSpecificData = New System.Windows.Forms.TabControl
        Me.TPComplianceAssistance = New System.Windows.Forms.TabPage
        Me.Label44 = New System.Windows.Forms.Label
        Me.txtComplianceAssistanceComments = New System.Windows.Forms.TextBox
        Me.chbOtherAssist = New System.Windows.Forms.CheckBox
        Me.chbStormWaterAssist = New System.Windows.Forms.CheckBox
        Me.chbHazardousWasteAssist = New System.Windows.Forms.CheckBox
        Me.chbSolidWasteAssist = New System.Windows.Forms.CheckBox
        Me.chbUSTAssist = New System.Windows.Forms.CheckBox
        Me.chbScrapTireAssist = New System.Windows.Forms.CheckBox
        Me.chbLeadAndAsbestosAssist = New System.Windows.Forms.CheckBox
        Me.chbAirAssist = New System.Windows.Forms.CheckBox
        Me.TPTechnicalAssist = New System.Windows.Forms.TabPage
        Me.txtTechnicalAssistNotes = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chbPollSovent = New System.Windows.Forms.CheckBox
        Me.chbPollWater = New System.Windows.Forms.CheckBox
        Me.chbPollOther = New System.Windows.Forms.CheckBox
        Me.chbPollWaste = New System.Windows.Forms.CheckBox
        Me.chbPollEnergy = New System.Windows.Forms.CheckBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chbGeneralOther = New System.Windows.Forms.CheckBox
        Me.chbGeneralEMS = New System.Windows.Forms.CheckBox
        Me.chbGeneralMultiMedia = New System.Windows.Forms.CheckBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chbWasteOther = New System.Windows.Forms.CheckBox
        Me.chbWasteHazWaste = New System.Windows.Forms.CheckBox
        Me.chbWasteSolidWaste = New System.Windows.Forms.CheckBox
        Me.chbWasteUST = New System.Windows.Forms.CheckBox
        Me.chbWasteAST = New System.Windows.Forms.CheckBox
        Me.chbWasteTier2 = New System.Windows.Forms.CheckBox
        Me.chbWasteFormR = New System.Windows.Forms.CheckBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chbWaterOther = New System.Windows.Forms.CheckBox
        Me.chbWaterWetlands = New System.Windows.Forms.CheckBox
        Me.chbWaterSPCCC = New System.Windows.Forms.CheckBox
        Me.chbWaterEandS = New System.Windows.Forms.CheckBox
        Me.chbWaterNPDES = New System.Windows.Forms.CheckBox
        Me.chbWaterPOTW = New System.Windows.Forms.CheckBox
        Me.chbWaterIndustrial = New System.Windows.Forms.CheckBox
        Me.chbWaterConstruction = New System.Windows.Forms.CheckBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.chbAirOther = New System.Windows.Forms.CheckBox
        Me.chbAirCompCert = New System.Windows.Forms.CheckBox
        Me.chbAirPermitAssit = New System.Windows.Forms.CheckBox
        Me.chbAirRecordAssist = New System.Windows.Forms.CheckBox
        Me.chbAirEnforceAssist = New System.Windows.Forms.CheckBox
        Me.chbAirEmissInv = New System.Windows.Forms.CheckBox
        Me.chbAirAppPrep = New System.Windows.Forms.CheckBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.DTPTechAssistStart = New System.Windows.Forms.DateTimePicker
        Me.Label18 = New System.Windows.Forms.Label
        Me.DTPTechAssistEnd = New System.Windows.Forms.DateTimePicker
        Me.Label17 = New System.Windows.Forms.Label
        Me.DTPTechAssistInitialContact = New System.Windows.Forms.DateTimePicker
        Me.Label16 = New System.Windows.Forms.Label
        Me.cboTechAssistType = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TPPhoneCalls = New System.Windows.Forms.TabPage
        Me.chbOnetimeAssist = New System.Windows.Forms.CheckBox
        Me.chbFrontDeskCall = New System.Windows.Forms.CheckBox
        Me.txtPhoneCallNotes = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.mtbPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtCallName = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TPConferences = New System.Windows.Forms.TabPage
        Me.clbStaffAttending = New System.Windows.Forms.CheckedListBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.txtConferenceNotes = New System.Windows.Forms.TextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.txtConferenceFollowUp = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.txtConferenceAttendees = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.DTPConferenceEnd = New System.Windows.Forms.DateTimePicker
        Me.Label34 = New System.Windows.Forms.Label
        Me.DTPConferenceStart = New System.Windows.Forms.DateTimePicker
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtListOfBusinessSectors = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rdbSBEAPPresentationYes = New System.Windows.Forms.RadioButton
        Me.rdbSBEAPPresentationNo = New System.Windows.Forms.RadioButton
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtConferenceTopic = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtConferenceLocation = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtConferenceAttended = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TPOtherCases = New System.Windows.Forms.TabPage
        Me.txtReferralInformation = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.pnlBasicCaseData = New System.Windows.Forms.Panel
        Me.chbCaseClosureLetter = New System.Windows.Forms.CheckBox
        Me.pnlMultiClient = New System.Windows.Forms.Panel
        Me.btnRemoveClient = New System.Windows.Forms.Button
        Me.txtDeleteClient = New System.Windows.Forms.TextBox
        Me.txtMultiClients = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.btnAddClients = New System.Windows.Forms.Button
        Me.txtAddMultiClient = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMultiClientList = New System.Windows.Forms.TextBox
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.rdbMultiClient = New System.Windows.Forms.RadioButton
        Me.rdbSingleClient = New System.Windows.Forms.RadioButton
        Me.pnlSingleClient = New System.Windows.Forms.Panel
        Me.chbComplaintBased = New System.Windows.Forms.CheckBox
        Me.txtCaseDescription = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.DTPReferralDate = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.pnlModifingData = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.DTPActionOccured = New System.Windows.Forms.DateTimePicker
        Me.Label43 = New System.Windows.Forms.Label
        Me.txtCreationDate = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.txtActionCount = New System.Windows.Forms.TextBox
        Me.btnDeleteAction = New System.Windows.Forms.Button
        Me.btnClearActions = New System.Windows.Forms.Button
        Me.txtActionType = New System.Windows.Forms.TextBox
        Me.btnAddNewAction = New System.Windows.Forms.Button
        Me.txtActionID = New System.Windows.Forms.TextBox
        Me.dgvActionLog = New System.Windows.Forms.DataGridView
        Me.btnViewActionType = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCCaseSpecificData.SuspendLayout()
        Me.TPComplianceAssistance.SuspendLayout()
        Me.TPTechnicalAssist.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TPPhoneCalls.SuspendLayout()
        Me.TPConferences.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TPOtherCases.SuspendLayout()
        Me.pnlBasicCaseData.SuspendLayout()
        Me.pnlMultiClient.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlSingleClient.SuspendLayout()
        Me.pnlModifingData.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.dgvActionLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbClientSearch, Me.tsbPrint, Me.tsbClearFrom, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1016, 25)
        Me.ToolStrip1.TabIndex = 5
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
        'tsbClientSearch
        '
        Me.tsbClientSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClientSearch.Image = CType(resources.GetObject("tsbClientSearch.Image"), System.Drawing.Image)
        Me.tsbClientSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClientSearch.Name = "tsbClientSearch"
        Me.tsbClientSearch.Size = New System.Drawing.Size(23, 22)
        Me.tsbClientSearch.Text = "Client Search"
        '
        'tsbPrint
        '
        Me.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPrint.Enabled = False
        Me.tsbPrint.Image = CType(resources.GetObject("tsbPrint.Image"), System.Drawing.Image)
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(23, 22)
        Me.tsbPrint.Text = "Print Case Work"
        Me.tsbPrint.Visible = False
        '
        'tsbClearFrom
        '
        Me.tsbClearFrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClearFrom.Image = CType(resources.GetObject("tsbClearFrom.Image"), System.Drawing.Image)
        Me.tsbClearFrom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClearFrom.Name = "tsbClearFrom"
        Me.tsbClearFrom.Size = New System.Drawing.Size(23, 22)
        Me.tsbClearFrom.Text = "ToolStripButton1"
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
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Label1, Me.Label2, Me.Label3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 792)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1016, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Label1
        '
        Me.Label1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Label1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(993, 17)
        Me.Label1.Spring = True
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Label2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(4, 17)
        '
        'Label3
        '
        Me.Label3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Label3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(4, 17)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1016, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ToolToolStripMenuItem
        '
        Me.ToolToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiAddNewClient, Me.tsmDeleteCaseWork})
        Me.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem"
        Me.ToolToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.ToolToolStripMenuItem.Text = "Tool"
        '
        'mmiAddNewClient
        '
        Me.mmiAddNewClient.Name = "mmiAddNewClient"
        Me.mmiAddNewClient.Size = New System.Drawing.Size(166, 22)
        Me.mmiAddNewClient.Text = "Add New Client"
        '
        'tsmDeleteCaseWork
        '
        Me.tsmDeleteCaseWork.Name = "tsmDeleteCaseWork"
        Me.tsmDeleteCaseWork.Size = New System.Drawing.Size(166, 22)
        Me.tsmDeleteCaseWork.Text = "Delete Case Work"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'txtCaseID
        '
        Me.txtCaseID.Location = New System.Drawing.Point(47, 4)
        Me.txtCaseID.Name = "txtCaseID"
        Me.txtCaseID.ReadOnly = True
        Me.txtCaseID.Size = New System.Drawing.Size(83, 20)
        Me.txtCaseID.TabIndex = 7
        '
        'Label199
        '
        Me.Label199.AutoSize = True
        Me.Label199.Location = New System.Drawing.Point(-2, 7)
        Me.Label199.Name = "Label199"
        Me.Label199.Size = New System.Drawing.Size(48, 13)
        Me.Label199.TabIndex = 8
        Me.Label199.Text = "Case ID:"
        '
        'Label299
        '
        Me.Label299.AutoSize = True
        Me.Label299.Location = New System.Drawing.Point(5, 10)
        Me.Label299.Name = "Label299"
        Me.Label299.Size = New System.Drawing.Size(38, 13)
        Me.Label299.TabIndex = 10
        Me.Label299.Text = "Notes:"
        '
        'txtCaseNotes
        '
        Me.txtCaseNotes.AcceptsReturn = True
        Me.txtCaseNotes.Location = New System.Drawing.Point(25, 27)
        Me.txtCaseNotes.Multiline = True
        Me.txtCaseNotes.Name = "txtCaseNotes"
        Me.txtCaseNotes.Size = New System.Drawing.Size(959, 353)
        Me.txtCaseNotes.TabIndex = 9
        '
        'Label399
        '
        Me.Label399.AutoSize = True
        Me.Label399.Location = New System.Drawing.Point(591, 53)
        Me.Label399.Name = "Label399"
        Me.Label399.Size = New System.Drawing.Size(66, 13)
        Me.Label399.TabIndex = 12
        Me.Label399.Text = "Interagency:"
        '
        'cboInteragency
        '
        Me.cboInteragency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboInteragency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboInteragency.FormattingEnabled = True
        Me.cboInteragency.Location = New System.Drawing.Point(663, 50)
        Me.cboInteragency.Name = "cboInteragency"
        Me.cboInteragency.Size = New System.Drawing.Size(170, 21)
        Me.cboInteragency.TabIndex = 5
        '
        'cboStaffResponsible
        '
        Me.cboStaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboStaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaffResponsible.FormattingEnabled = True
        Me.cboStaffResponsible.Location = New System.Drawing.Point(663, 24)
        Me.cboStaffResponsible.Name = "cboStaffResponsible"
        Me.cboStaffResponsible.Size = New System.Drawing.Size(170, 21)
        Me.cboStaffResponsible.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(564, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Staff Responsible:"
        '
        'DTPCaseOpened
        '
        Me.DTPCaseOpened.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCaseOpened.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCaseOpened.Location = New System.Drawing.Point(663, 3)
        Me.DTPCaseOpened.Name = "DTPCaseOpened"
        Me.DTPCaseOpened.Size = New System.Drawing.Size(107, 20)
        Me.DTPCaseOpened.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(559, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Date Case Opened"
        '
        'cboActionType
        '
        Me.cboActionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboActionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboActionType.FormattingEnabled = True
        Me.cboActionType.Location = New System.Drawing.Point(207, 83)
        Me.cboActionType.Name = "cboActionType"
        Me.cboActionType.Size = New System.Drawing.Size(234, 21)
        Me.cboActionType.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(137, 87)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Action Type:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(779, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Date Case Closed"
        '
        'DTPCaseClosed
        '
        Me.DTPCaseClosed.Checked = False
        Me.DTPCaseClosed.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCaseClosed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCaseClosed.Location = New System.Drawing.Point(884, 3)
        Me.DTPCaseClosed.Name = "DTPCaseClosed"
        Me.DTPCaseClosed.ShowCheckBox = True
        Me.DTPCaseClosed.Size = New System.Drawing.Size(107, 20)
        Me.DTPCaseClosed.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(551, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Last Modifing Staff"
        '
        'txtLastModifingStaff
        '
        Me.txtLastModifingStaff.Location = New System.Drawing.Point(652, 3)
        Me.txtLastModifingStaff.Name = "txtLastModifingStaff"
        Me.txtLastModifingStaff.ReadOnly = True
        Me.txtLastModifingStaff.Size = New System.Drawing.Size(163, 20)
        Me.txtLastModifingStaff.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(821, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Last Modified"
        '
        'DTPLastModified
        '
        Me.DTPLastModified.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLastModified.Enabled = False
        Me.DTPLastModified.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLastModified.Location = New System.Drawing.Point(897, 3)
        Me.DTPLastModified.Name = "DTPLastModified"
        Me.DTPLastModified.Size = New System.Drawing.Size(107, 20)
        Me.DTPLastModified.TabIndex = 24
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 7)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Customer ID:"
        '
        'txtClientID
        '
        Me.txtClientID.Location = New System.Drawing.Point(73, 3)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.Size = New System.Drawing.Size(83, 20)
        Me.txtClientID.TabIndex = 0
        '
        'txtClientInformation
        '
        Me.txtClientInformation.Location = New System.Drawing.Point(14, 28)
        Me.txtClientInformation.Multiline = True
        Me.txtClientInformation.Name = "txtClientInformation"
        Me.txtClientInformation.ReadOnly = True
        Me.txtClientInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtClientInformation.Size = New System.Drawing.Size(266, 58)
        Me.txtClientInformation.TabIndex = 28
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(141, 96)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(99, 13)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Outstanding Cases:"
        '
        'txtOutstandingCases
        '
        Me.txtOutstandingCases.Location = New System.Drawing.Point(246, 92)
        Me.txtOutstandingCases.Name = "txtOutstandingCases"
        Me.txtOutstandingCases.ReadOnly = True
        Me.txtOutstandingCases.Size = New System.Drawing.Size(34, 20)
        Me.txtOutstandingCases.TabIndex = 30
        '
        'btnRefreshClient
        '
        Me.btnRefreshClient.AutoSize = True
        Me.btnRefreshClient.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshClient.Image = CType(resources.GetObject("btnRefreshClient.Image"), System.Drawing.Image)
        Me.btnRefreshClient.Location = New System.Drawing.Point(162, 2)
        Me.btnRefreshClient.Name = "btnRefreshClient"
        Me.btnRefreshClient.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshClient.TabIndex = 32
        Me.btnRefreshClient.UseVisualStyleBackColor = True
        '
        'TCCaseSpecificData
        '
        Me.TCCaseSpecificData.Controls.Add(Me.TPComplianceAssistance)
        Me.TCCaseSpecificData.Controls.Add(Me.TPTechnicalAssist)
        Me.TCCaseSpecificData.Controls.Add(Me.TPPhoneCalls)
        Me.TCCaseSpecificData.Controls.Add(Me.TPConferences)
        Me.TCCaseSpecificData.Controls.Add(Me.TPOtherCases)
        Me.TCCaseSpecificData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCCaseSpecificData.Location = New System.Drawing.Point(0, 340)
        Me.TCCaseSpecificData.Name = "TCCaseSpecificData"
        Me.TCCaseSpecificData.SelectedIndex = 0
        Me.TCCaseSpecificData.Size = New System.Drawing.Size(1016, 424)
        Me.TCCaseSpecificData.TabIndex = 33
        '
        'TPComplianceAssistance
        '
        Me.TPComplianceAssistance.Controls.Add(Me.Label44)
        Me.TPComplianceAssistance.Controls.Add(Me.txtComplianceAssistanceComments)
        Me.TPComplianceAssistance.Controls.Add(Me.chbOtherAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbStormWaterAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbHazardousWasteAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbSolidWasteAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbUSTAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbScrapTireAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbLeadAndAsbestosAssist)
        Me.TPComplianceAssistance.Controls.Add(Me.chbAirAssist)
        Me.TPComplianceAssistance.Location = New System.Drawing.Point(4, 22)
        Me.TPComplianceAssistance.Name = "TPComplianceAssistance"
        Me.TPComplianceAssistance.Size = New System.Drawing.Size(1008, 398)
        Me.TPComplianceAssistance.TabIndex = 4
        Me.TPComplianceAssistance.Text = "Compliance Assistance"
        Me.TPComplianceAssistance.UseVisualStyleBackColor = True
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(8, 206)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(108, 13)
        Me.Label44.TabIndex = 34
        Me.Label44.Text = "Additional Comments:"
        '
        'txtComplianceAssistanceComments
        '
        Me.txtComplianceAssistanceComments.Location = New System.Drawing.Point(11, 222)
        Me.txtComplianceAssistanceComments.Multiline = True
        Me.txtComplianceAssistanceComments.Name = "txtComplianceAssistanceComments"
        Me.txtComplianceAssistanceComments.Size = New System.Drawing.Size(533, 100)
        Me.txtComplianceAssistanceComments.TabIndex = 8
        '
        'chbOtherAssist
        '
        Me.chbOtherAssist.AutoSize = True
        Me.chbOtherAssist.Location = New System.Drawing.Point(6, 179)
        Me.chbOtherAssist.Name = "chbOtherAssist"
        Me.chbOtherAssist.Size = New System.Drawing.Size(52, 17)
        Me.chbOtherAssist.TabIndex = 7
        Me.chbOtherAssist.Text = "Other"
        Me.chbOtherAssist.UseVisualStyleBackColor = True
        '
        'chbStormWaterAssist
        '
        Me.chbStormWaterAssist.AutoSize = True
        Me.chbStormWaterAssist.Location = New System.Drawing.Point(6, 41)
        Me.chbStormWaterAssist.Name = "chbStormWaterAssist"
        Me.chbStormWaterAssist.Size = New System.Drawing.Size(85, 17)
        Me.chbStormWaterAssist.TabIndex = 6
        Me.chbStormWaterAssist.Text = "Storm Water"
        Me.chbStormWaterAssist.UseVisualStyleBackColor = True
        '
        'chbHazardousWasteAssist
        '
        Me.chbHazardousWasteAssist.AutoSize = True
        Me.chbHazardousWasteAssist.Location = New System.Drawing.Point(6, 64)
        Me.chbHazardousWasteAssist.Name = "chbHazardousWasteAssist"
        Me.chbHazardousWasteAssist.Size = New System.Drawing.Size(111, 17)
        Me.chbHazardousWasteAssist.TabIndex = 5
        Me.chbHazardousWasteAssist.Text = "Hazardous Waste"
        Me.chbHazardousWasteAssist.UseVisualStyleBackColor = True
        '
        'chbSolidWasteAssist
        '
        Me.chbSolidWasteAssist.AutoSize = True
        Me.chbSolidWasteAssist.Location = New System.Drawing.Point(6, 87)
        Me.chbSolidWasteAssist.Name = "chbSolidWasteAssist"
        Me.chbSolidWasteAssist.Size = New System.Drawing.Size(83, 17)
        Me.chbSolidWasteAssist.TabIndex = 4
        Me.chbSolidWasteAssist.Text = "Solid Waste"
        Me.chbSolidWasteAssist.UseVisualStyleBackColor = True
        '
        'chbUSTAssist
        '
        Me.chbUSTAssist.AutoSize = True
        Me.chbUSTAssist.Location = New System.Drawing.Point(6, 110)
        Me.chbUSTAssist.Name = "chbUSTAssist"
        Me.chbUSTAssist.Size = New System.Drawing.Size(48, 17)
        Me.chbUSTAssist.TabIndex = 3
        Me.chbUSTAssist.Text = "UST"
        Me.chbUSTAssist.UseVisualStyleBackColor = True
        '
        'chbScrapTireAssist
        '
        Me.chbScrapTireAssist.AutoSize = True
        Me.chbScrapTireAssist.Location = New System.Drawing.Point(6, 133)
        Me.chbScrapTireAssist.Name = "chbScrapTireAssist"
        Me.chbScrapTireAssist.Size = New System.Drawing.Size(75, 17)
        Me.chbScrapTireAssist.TabIndex = 2
        Me.chbScrapTireAssist.Text = "Scrap Tire"
        Me.chbScrapTireAssist.UseVisualStyleBackColor = True
        '
        'chbLeadAndAsbestosAssist
        '
        Me.chbLeadAndAsbestosAssist.AutoSize = True
        Me.chbLeadAndAsbestosAssist.Location = New System.Drawing.Point(6, 156)
        Me.chbLeadAndAsbestosAssist.Name = "chbLeadAndAsbestosAssist"
        Me.chbLeadAndAsbestosAssist.Size = New System.Drawing.Size(117, 17)
        Me.chbLeadAndAsbestosAssist.TabIndex = 1
        Me.chbLeadAndAsbestosAssist.Text = "Lead and Asbestos"
        Me.chbLeadAndAsbestosAssist.UseVisualStyleBackColor = True
        '
        'chbAirAssist
        '
        Me.chbAirAssist.AutoSize = True
        Me.chbAirAssist.Location = New System.Drawing.Point(6, 18)
        Me.chbAirAssist.Name = "chbAirAssist"
        Me.chbAirAssist.Size = New System.Drawing.Size(38, 17)
        Me.chbAirAssist.TabIndex = 0
        Me.chbAirAssist.Text = "Air"
        Me.chbAirAssist.UseVisualStyleBackColor = True
        '
        'TPTechnicalAssist
        '
        Me.TPTechnicalAssist.AutoScroll = True
        Me.TPTechnicalAssist.Controls.Add(Me.txtTechnicalAssistNotes)
        Me.TPTechnicalAssist.Controls.Add(Me.Label37)
        Me.TPTechnicalAssist.Controls.Add(Me.Panel5)
        Me.TPTechnicalAssist.Controls.Add(Me.Panel4)
        Me.TPTechnicalAssist.Controls.Add(Me.Panel3)
        Me.TPTechnicalAssist.Controls.Add(Me.Panel2)
        Me.TPTechnicalAssist.Controls.Add(Me.Panel1)
        Me.TPTechnicalAssist.Controls.Add(Me.DTPTechAssistStart)
        Me.TPTechnicalAssist.Controls.Add(Me.Label18)
        Me.TPTechnicalAssist.Controls.Add(Me.DTPTechAssistEnd)
        Me.TPTechnicalAssist.Controls.Add(Me.Label17)
        Me.TPTechnicalAssist.Controls.Add(Me.DTPTechAssistInitialContact)
        Me.TPTechnicalAssist.Controls.Add(Me.Label16)
        Me.TPTechnicalAssist.Controls.Add(Me.cboTechAssistType)
        Me.TPTechnicalAssist.Controls.Add(Me.Label15)
        Me.TPTechnicalAssist.Location = New System.Drawing.Point(4, 22)
        Me.TPTechnicalAssist.Name = "TPTechnicalAssist"
        Me.TPTechnicalAssist.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTechnicalAssist.Size = New System.Drawing.Size(1008, 398)
        Me.TPTechnicalAssist.TabIndex = 1
        Me.TPTechnicalAssist.Text = "Permit Assistance"
        Me.TPTechnicalAssist.UseVisualStyleBackColor = True
        '
        'txtTechnicalAssistNotes
        '
        Me.txtTechnicalAssistNotes.AcceptsReturn = True
        Me.txtTechnicalAssistNotes.Location = New System.Drawing.Point(20, 245)
        Me.txtTechnicalAssistNotes.Multiline = True
        Me.txtTechnicalAssistNotes.Name = "txtTechnicalAssistNotes"
        Me.txtTechnicalAssistNotes.Size = New System.Drawing.Size(959, 141)
        Me.txtTechnicalAssistNotes.TabIndex = 37
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(8, 229)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(38, 13)
        Me.Label37.TabIndex = 38
        Me.Label37.Text = "Notes:"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.chbPollSovent)
        Me.Panel5.Controls.Add(Me.chbPollWater)
        Me.Panel5.Controls.Add(Me.chbPollOther)
        Me.Panel5.Controls.Add(Me.chbPollWaste)
        Me.Panel5.Controls.Add(Me.chbPollEnergy)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Location = New System.Drawing.Point(814, 42)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(186, 183)
        Me.Panel5.TabIndex = 36
        '
        'chbPollSovent
        '
        Me.chbPollSovent.AutoSize = True
        Me.chbPollSovent.Location = New System.Drawing.Point(1, 72)
        Me.chbPollSovent.Name = "chbPollSovent"
        Me.chbPollSovent.Size = New System.Drawing.Size(174, 17)
        Me.chbPollSovent.TabIndex = 2
        Me.chbPollSovent.Text = "Solvent Substitution Assistance"
        Me.chbPollSovent.UseVisualStyleBackColor = True
        '
        'chbPollWater
        '
        Me.chbPollWater.AutoSize = True
        Me.chbPollWater.Location = New System.Drawing.Point(1, 89)
        Me.chbPollWater.Name = "chbPollWater"
        Me.chbPollWater.Size = New System.Drawing.Size(169, 17)
        Me.chbPollWater.TabIndex = 3
        Me.chbPollWater.Text = "Water Minimization Assistance"
        Me.chbPollWater.UseVisualStyleBackColor = True
        '
        'chbPollOther
        '
        Me.chbPollOther.AutoSize = True
        Me.chbPollOther.Location = New System.Drawing.Point(1, 106)
        Me.chbPollOther.Name = "chbPollOther"
        Me.chbPollOther.Size = New System.Drawing.Size(52, 17)
        Me.chbPollOther.TabIndex = 4
        Me.chbPollOther.Text = "Other"
        Me.chbPollOther.UseVisualStyleBackColor = True
        '
        'chbPollWaste
        '
        Me.chbPollWaste.AutoSize = True
        Me.chbPollWaste.Location = New System.Drawing.Point(1, 55)
        Me.chbPollWaste.Name = "chbPollWaste"
        Me.chbPollWaste.Size = New System.Drawing.Size(171, 17)
        Me.chbPollWaste.TabIndex = 1
        Me.chbPollWaste.Text = "Waste Minimization Assistance"
        Me.chbPollWaste.UseVisualStyleBackColor = True
        '
        'chbPollEnergy
        '
        Me.chbPollEnergy.AutoSize = True
        Me.chbPollEnergy.Location = New System.Drawing.Point(1, 38)
        Me.chbPollEnergy.Name = "chbPollEnergy"
        Me.chbPollEnergy.Size = New System.Drawing.Size(162, 17)
        Me.chbPollEnergy.TabIndex = 0
        Me.chbPollEnergy.Text = "Energy Efficiency Assistance"
        Me.chbPollEnergy.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(174, 32)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "Pollution Prevention " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      Assistance Request:"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.chbGeneralOther)
        Me.Panel4.Controls.Add(Me.chbGeneralEMS)
        Me.Panel4.Controls.Add(Me.chbGeneralMultiMedia)
        Me.Panel4.Controls.Add(Me.Label24)
        Me.Panel4.Location = New System.Drawing.Point(601, 42)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(210, 186)
        Me.Panel4.TabIndex = 35
        '
        'chbGeneralOther
        '
        Me.chbGeneralOther.AutoSize = True
        Me.chbGeneralOther.Location = New System.Drawing.Point(3, 55)
        Me.chbGeneralOther.Name = "chbGeneralOther"
        Me.chbGeneralOther.Size = New System.Drawing.Size(52, 17)
        Me.chbGeneralOther.TabIndex = 2
        Me.chbGeneralOther.Text = "Other"
        Me.chbGeneralOther.UseVisualStyleBackColor = True
        '
        'chbGeneralEMS
        '
        Me.chbGeneralEMS.AutoSize = True
        Me.chbGeneralEMS.Location = New System.Drawing.Point(3, 38)
        Me.chbGeneralEMS.Name = "chbGeneralEMS"
        Me.chbGeneralEMS.Size = New System.Drawing.Size(197, 17)
        Me.chbGeneralEMS.TabIndex = 1
        Me.chbGeneralEMS.Text = "EMS Development / Implementation"
        Me.chbGeneralEMS.UseVisualStyleBackColor = True
        '
        'chbGeneralMultiMedia
        '
        Me.chbGeneralMultiMedia.AutoSize = True
        Me.chbGeneralMultiMedia.Location = New System.Drawing.Point(3, 21)
        Me.chbGeneralMultiMedia.Name = "chbGeneralMultiMedia"
        Me.chbGeneralMultiMedia.Size = New System.Drawing.Size(161, 17)
        Me.chbGeneralMultiMedia.TabIndex = 0
        Me.chbGeneralMultiMedia.Text = "Multimedia Compliance Audit"
        Me.chbGeneralMultiMedia.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(209, 16)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "General Assistance Request:"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.chbWasteOther)
        Me.Panel3.Controls.Add(Me.chbWasteHazWaste)
        Me.Panel3.Controls.Add(Me.chbWasteSolidWaste)
        Me.Panel3.Controls.Add(Me.chbWasteUST)
        Me.Panel3.Controls.Add(Me.chbWasteAST)
        Me.Panel3.Controls.Add(Me.chbWasteTier2)
        Me.Panel3.Controls.Add(Me.chbWasteFormR)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Location = New System.Drawing.Point(394, 42)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(204, 183)
        Me.Panel3.TabIndex = 34
        '
        'chbWasteOther
        '
        Me.chbWasteOther.AutoSize = True
        Me.chbWasteOther.Location = New System.Drawing.Point(3, 123)
        Me.chbWasteOther.Name = "chbWasteOther"
        Me.chbWasteOther.Size = New System.Drawing.Size(52, 17)
        Me.chbWasteOther.TabIndex = 6
        Me.chbWasteOther.Text = "Other"
        Me.chbWasteOther.UseVisualStyleBackColor = True
        '
        'chbWasteHazWaste
        '
        Me.chbWasteHazWaste.AutoSize = True
        Me.chbWasteHazWaste.Location = New System.Drawing.Point(3, 55)
        Me.chbWasteHazWaste.Name = "chbWasteHazWaste"
        Me.chbWasteHazWaste.Size = New System.Drawing.Size(111, 17)
        Me.chbWasteHazWaste.TabIndex = 2
        Me.chbWasteHazWaste.Text = "Hazardous Waste"
        Me.chbWasteHazWaste.UseVisualStyleBackColor = True
        '
        'chbWasteSolidWaste
        '
        Me.chbWasteSolidWaste.AutoSize = True
        Me.chbWasteSolidWaste.Location = New System.Drawing.Point(3, 72)
        Me.chbWasteSolidWaste.Name = "chbWasteSolidWaste"
        Me.chbWasteSolidWaste.Size = New System.Drawing.Size(83, 17)
        Me.chbWasteSolidWaste.TabIndex = 3
        Me.chbWasteSolidWaste.Text = "Solid Waste"
        Me.chbWasteSolidWaste.UseVisualStyleBackColor = True
        '
        'chbWasteUST
        '
        Me.chbWasteUST.AutoSize = True
        Me.chbWasteUST.Location = New System.Drawing.Point(3, 89)
        Me.chbWasteUST.Name = "chbWasteUST"
        Me.chbWasteUST.Size = New System.Drawing.Size(48, 17)
        Me.chbWasteUST.TabIndex = 4
        Me.chbWasteUST.Text = "UST"
        Me.chbWasteUST.UseVisualStyleBackColor = True
        '
        'chbWasteAST
        '
        Me.chbWasteAST.AutoSize = True
        Me.chbWasteAST.Location = New System.Drawing.Point(3, 106)
        Me.chbWasteAST.Name = "chbWasteAST"
        Me.chbWasteAST.Size = New System.Drawing.Size(47, 17)
        Me.chbWasteAST.TabIndex = 5
        Me.chbWasteAST.Text = "AST"
        Me.chbWasteAST.UseVisualStyleBackColor = True
        '
        'chbWasteTier2
        '
        Me.chbWasteTier2.AutoSize = True
        Me.chbWasteTier2.Location = New System.Drawing.Point(3, 38)
        Me.chbWasteTier2.Name = "chbWasteTier2"
        Me.chbWasteTier2.Size = New System.Drawing.Size(53, 17)
        Me.chbWasteTier2.TabIndex = 1
        Me.chbWasteTier2.Text = "Tier 2"
        Me.chbWasteTier2.UseVisualStyleBackColor = True
        '
        'chbWasteFormR
        '
        Me.chbWasteFormR.AutoSize = True
        Me.chbWasteFormR.Location = New System.Drawing.Point(3, 21)
        Me.chbWasteFormR.Name = "chbWasteFormR"
        Me.chbWasteFormR.Size = New System.Drawing.Size(60, 17)
        Me.chbWasteFormR.TabIndex = 0
        Me.chbWasteFormR.Text = "Form R"
        Me.chbWasteFormR.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(198, 16)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Waste Assistance Request:"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.chbWaterOther)
        Me.Panel2.Controls.Add(Me.chbWaterWetlands)
        Me.Panel2.Controls.Add(Me.chbWaterSPCCC)
        Me.Panel2.Controls.Add(Me.chbWaterEandS)
        Me.Panel2.Controls.Add(Me.chbWaterNPDES)
        Me.Panel2.Controls.Add(Me.chbWaterPOTW)
        Me.Panel2.Controls.Add(Me.chbWaterIndustrial)
        Me.Panel2.Controls.Add(Me.chbWaterConstruction)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Location = New System.Drawing.Point(192, 42)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(199, 183)
        Me.Panel2.TabIndex = 33
        '
        'chbWaterOther
        '
        Me.chbWaterOther.AutoSize = True
        Me.chbWaterOther.Location = New System.Drawing.Point(3, 140)
        Me.chbWaterOther.Name = "chbWaterOther"
        Me.chbWaterOther.Size = New System.Drawing.Size(52, 17)
        Me.chbWaterOther.TabIndex = 7
        Me.chbWaterOther.Text = "Other"
        Me.chbWaterOther.UseVisualStyleBackColor = True
        '
        'chbWaterWetlands
        '
        Me.chbWaterWetlands.AutoSize = True
        Me.chbWaterWetlands.Location = New System.Drawing.Point(3, 123)
        Me.chbWaterWetlands.Name = "chbWaterWetlands"
        Me.chbWaterWetlands.Size = New System.Drawing.Size(71, 17)
        Me.chbWaterWetlands.TabIndex = 6
        Me.chbWaterWetlands.Text = "Wetlands"
        Me.chbWaterWetlands.UseVisualStyleBackColor = True
        '
        'chbWaterSPCCC
        '
        Me.chbWaterSPCCC.AutoSize = True
        Me.chbWaterSPCCC.Location = New System.Drawing.Point(3, 55)
        Me.chbWaterSPCCC.Name = "chbWaterSPCCC"
        Me.chbWaterSPCCC.Size = New System.Drawing.Size(61, 17)
        Me.chbWaterSPCCC.TabIndex = 2
        Me.chbWaterSPCCC.Text = "SPCCC"
        Me.chbWaterSPCCC.UseVisualStyleBackColor = True
        '
        'chbWaterEandS
        '
        Me.chbWaterEandS.AutoSize = True
        Me.chbWaterEandS.Location = New System.Drawing.Point(3, 72)
        Me.chbWaterEandS.Name = "chbWaterEandS"
        Me.chbWaterEandS.Size = New System.Drawing.Size(52, 17)
        Me.chbWaterEandS.TabIndex = 3
        Me.chbWaterEandS.Text = "E && S"
        Me.chbWaterEandS.UseVisualStyleBackColor = True
        '
        'chbWaterNPDES
        '
        Me.chbWaterNPDES.AutoSize = True
        Me.chbWaterNPDES.Location = New System.Drawing.Point(3, 89)
        Me.chbWaterNPDES.Name = "chbWaterNPDES"
        Me.chbWaterNPDES.Size = New System.Drawing.Size(63, 17)
        Me.chbWaterNPDES.TabIndex = 4
        Me.chbWaterNPDES.Text = "NPDES"
        Me.chbWaterNPDES.UseVisualStyleBackColor = True
        '
        'chbWaterPOTW
        '
        Me.chbWaterPOTW.AutoSize = True
        Me.chbWaterPOTW.Location = New System.Drawing.Point(3, 106)
        Me.chbWaterPOTW.Name = "chbWaterPOTW"
        Me.chbWaterPOTW.Size = New System.Drawing.Size(59, 17)
        Me.chbWaterPOTW.TabIndex = 5
        Me.chbWaterPOTW.Text = "POTW"
        Me.chbWaterPOTW.UseVisualStyleBackColor = True
        '
        'chbWaterIndustrial
        '
        Me.chbWaterIndustrial.AutoSize = True
        Me.chbWaterIndustrial.Location = New System.Drawing.Point(3, 38)
        Me.chbWaterIndustrial.Name = "chbWaterIndustrial"
        Me.chbWaterIndustrial.Size = New System.Drawing.Size(110, 17)
        Me.chbWaterIndustrial.TabIndex = 1
        Me.chbWaterIndustrial.Text = "Industrial SWPPP"
        Me.chbWaterIndustrial.UseVisualStyleBackColor = True
        '
        'chbWaterConstruction
        '
        Me.chbWaterConstruction.AutoSize = True
        Me.chbWaterConstruction.Location = New System.Drawing.Point(3, 21)
        Me.chbWaterConstruction.Name = "chbWaterConstruction"
        Me.chbWaterConstruction.Size = New System.Drawing.Size(127, 17)
        Me.chbWaterConstruction.TabIndex = 0
        Me.chbWaterConstruction.Text = "Construction SWPPP"
        Me.chbWaterConstruction.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(195, 16)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Water Assistance Request:"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtAIRSNumber)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.chbAirOther)
        Me.Panel1.Controls.Add(Me.chbAirCompCert)
        Me.Panel1.Controls.Add(Me.chbAirPermitAssit)
        Me.Panel1.Controls.Add(Me.chbAirRecordAssist)
        Me.Panel1.Controls.Add(Me.chbAirEnforceAssist)
        Me.Panel1.Controls.Add(Me.chbAirEmissInv)
        Me.Panel1.Controls.Add(Me.chbAirAppPrep)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Location = New System.Drawing.Point(2, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(188, 183)
        Me.Panel1.TabIndex = 32
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(54, 144)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(119, 20)
        Me.txtAIRSNumber.TabIndex = 6
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(3, 148)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(45, 13)
        Me.Label21.TabIndex = 33
        Me.Label21.Text = "AIRS #:"
        '
        'chbAirOther
        '
        Me.chbAirOther.AutoSize = True
        Me.chbAirOther.Location = New System.Drawing.Point(3, 123)
        Me.chbAirOther.Name = "chbAirOther"
        Me.chbAirOther.Size = New System.Drawing.Size(52, 17)
        Me.chbAirOther.TabIndex = 7
        Me.chbAirOther.Text = "Other"
        Me.chbAirOther.UseVisualStyleBackColor = True
        '
        'chbAirCompCert
        '
        Me.chbAirCompCert.AutoSize = True
        Me.chbAirCompCert.Location = New System.Drawing.Point(3, 55)
        Me.chbAirCompCert.Name = "chbAirCompCert"
        Me.chbAirCompCert.Size = New System.Drawing.Size(139, 17)
        Me.chbAirCompCert.TabIndex = 2
        Me.chbAirCompCert.Text = "Compliance Certification"
        Me.chbAirCompCert.UseVisualStyleBackColor = True
        '
        'chbAirPermitAssit
        '
        Me.chbAirPermitAssit.AutoSize = True
        Me.chbAirPermitAssit.Location = New System.Drawing.Point(3, 72)
        Me.chbAirPermitAssit.Name = "chbAirPermitAssit"
        Me.chbAirPermitAssit.Size = New System.Drawing.Size(109, 17)
        Me.chbAirPermitAssit.TabIndex = 3
        Me.chbAirPermitAssit.Text = "Permit Assistance"
        Me.chbAirPermitAssit.UseVisualStyleBackColor = True
        '
        'chbAirRecordAssist
        '
        Me.chbAirRecordAssist.AutoSize = True
        Me.chbAirRecordAssist.Location = New System.Drawing.Point(3, 89)
        Me.chbAirRecordAssist.Name = "chbAirRecordAssist"
        Me.chbAirRecordAssist.Size = New System.Drawing.Size(153, 17)
        Me.chbAirRecordAssist.TabIndex = 4
        Me.chbAirRecordAssist.Text = "Recordkeeping Assistance"
        Me.chbAirRecordAssist.UseVisualStyleBackColor = True
        '
        'chbAirEnforceAssist
        '
        Me.chbAirEnforceAssist.AutoSize = True
        Me.chbAirEnforceAssist.Location = New System.Drawing.Point(3, 106)
        Me.chbAirEnforceAssist.Name = "chbAirEnforceAssist"
        Me.chbAirEnforceAssist.Size = New System.Drawing.Size(140, 17)
        Me.chbAirEnforceAssist.TabIndex = 5
        Me.chbAirEnforceAssist.Text = "Enforcement Assistance"
        Me.chbAirEnforceAssist.UseVisualStyleBackColor = True
        '
        'chbAirEmissInv
        '
        Me.chbAirEmissInv.AutoSize = True
        Me.chbAirEmissInv.Location = New System.Drawing.Point(3, 38)
        Me.chbAirEmissInv.Name = "chbAirEmissInv"
        Me.chbAirEmissInv.Size = New System.Drawing.Size(119, 17)
        Me.chbAirEmissInv.TabIndex = 1
        Me.chbAirEmissInv.Text = "Emissions Inventory"
        Me.chbAirEmissInv.UseVisualStyleBackColor = True
        '
        'chbAirAppPrep
        '
        Me.chbAirAppPrep.AutoSize = True
        Me.chbAirAppPrep.Location = New System.Drawing.Point(3, 21)
        Me.chbAirAppPrep.Name = "chbAirAppPrep"
        Me.chbAirAppPrep.Size = New System.Drawing.Size(135, 17)
        Me.chbAirAppPrep.TabIndex = 0
        Me.chbAirAppPrep.Text = "Application Preparation"
        Me.chbAirAppPrep.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(173, 16)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Air Assistance Request:"
        '
        'DTPTechAssistStart
        '
        Me.DTPTechAssistStart.Checked = False
        Me.DTPTechAssistStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTechAssistStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTechAssistStart.Location = New System.Drawing.Point(550, 6)
        Me.DTPTechAssistStart.Name = "DTPTechAssistStart"
        Me.DTPTechAssistStart.ShowCheckBox = True
        Me.DTPTechAssistStart.Size = New System.Drawing.Size(107, 20)
        Me.DTPTechAssistStart.TabIndex = 3
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(444, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 13)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "Assist Start Date:"
        '
        'DTPTechAssistEnd
        '
        Me.DTPTechAssistEnd.Checked = False
        Me.DTPTechAssistEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTechAssistEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTechAssistEnd.Location = New System.Drawing.Point(789, 6)
        Me.DTPTechAssistEnd.Name = "DTPTechAssistEnd"
        Me.DTPTechAssistEnd.ShowCheckBox = True
        Me.DTPTechAssistEnd.Size = New System.Drawing.Size(107, 20)
        Me.DTPTechAssistEnd.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(683, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 13)
        Me.Label17.TabIndex = 26
        Me.Label17.Text = "Assist End Date:"
        '
        'DTPTechAssistInitialContact
        '
        Me.DTPTechAssistInitialContact.Checked = False
        Me.DTPTechAssistInitialContact.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTechAssistInitialContact.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTechAssistInitialContact.Location = New System.Drawing.Point(313, 6)
        Me.DTPTechAssistInitialContact.Name = "DTPTechAssistInitialContact"
        Me.DTPTechAssistInitialContact.ShowCheckBox = True
        Me.DTPTechAssistInitialContact.Size = New System.Drawing.Size(107, 20)
        Me.DTPTechAssistInitialContact.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(207, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 13)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Initial Contact Date:"
        '
        'cboTechAssistType
        '
        Me.cboTechAssistType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboTechAssistType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTechAssistType.FormattingEnabled = True
        Me.cboTechAssistType.Location = New System.Drawing.Point(73, 6)
        Me.cboTechAssistType.Name = "cboTechAssistType"
        Me.cboTechAssistType.Size = New System.Drawing.Size(121, 21)
        Me.cboTechAssistType.Sorted = True
        Me.cboTechAssistType.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 10)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 13)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Assist Level:"
        '
        'TPPhoneCalls
        '
        Me.TPPhoneCalls.Controls.Add(Me.chbOnetimeAssist)
        Me.TPPhoneCalls.Controls.Add(Me.chbFrontDeskCall)
        Me.TPPhoneCalls.Controls.Add(Me.txtPhoneCallNotes)
        Me.TPPhoneCalls.Controls.Add(Me.Label38)
        Me.TPPhoneCalls.Controls.Add(Me.mtbPhoneNumber)
        Me.TPPhoneCalls.Controls.Add(Me.Label26)
        Me.TPPhoneCalls.Controls.Add(Me.txtCallName)
        Me.TPPhoneCalls.Controls.Add(Me.Label19)
        Me.TPPhoneCalls.Location = New System.Drawing.Point(4, 22)
        Me.TPPhoneCalls.Name = "TPPhoneCalls"
        Me.TPPhoneCalls.Size = New System.Drawing.Size(1008, 398)
        Me.TPPhoneCalls.TabIndex = 2
        Me.TPPhoneCalls.Text = "Phone Call Made/Received"
        Me.TPPhoneCalls.UseVisualStyleBackColor = True
        '
        'chbOnetimeAssist
        '
        Me.chbOnetimeAssist.AutoSize = True
        Me.chbOnetimeAssist.Checked = True
        Me.chbOnetimeAssist.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbOnetimeAssist.Location = New System.Drawing.Point(201, 31)
        Me.chbOnetimeAssist.Name = "chbOnetimeAssist"
        Me.chbOnetimeAssist.Size = New System.Drawing.Size(98, 17)
        Me.chbOnetimeAssist.TabIndex = 3
        Me.chbOnetimeAssist.Text = "One-time Assist"
        Me.chbOnetimeAssist.UseVisualStyleBackColor = True
        '
        'chbFrontDeskCall
        '
        Me.chbFrontDeskCall.AutoSize = True
        Me.chbFrontDeskCall.Location = New System.Drawing.Point(52, 31)
        Me.chbFrontDeskCall.Name = "chbFrontDeskCall"
        Me.chbFrontDeskCall.Size = New System.Drawing.Size(98, 17)
        Me.chbFrontDeskCall.TabIndex = 2
        Me.chbFrontDeskCall.Text = "Front Desk Call"
        Me.chbFrontDeskCall.UseVisualStyleBackColor = True
        '
        'txtPhoneCallNotes
        '
        Me.txtPhoneCallNotes.AcceptsReturn = True
        Me.txtPhoneCallNotes.Location = New System.Drawing.Point(52, 62)
        Me.txtPhoneCallNotes.Multiline = True
        Me.txtPhoneCallNotes.Name = "txtPhoneCallNotes"
        Me.txtPhoneCallNotes.Size = New System.Drawing.Size(501, 317)
        Me.txtPhoneCallNotes.TabIndex = 6
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(11, 62)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(38, 13)
        Me.Label38.TabIndex = 15
        Me.Label38.Text = "Notes:"
        '
        'mtbPhoneNumber
        '
        Me.mtbPhoneNumber.Location = New System.Drawing.Point(418, 3)
        Me.mtbPhoneNumber.Mask = "(999) 000-0000 ext:00000"
        Me.mtbPhoneNumber.Name = "mtbPhoneNumber"
        Me.mtbPhoneNumber.Size = New System.Drawing.Size(135, 20)
        Me.mtbPhoneNumber.TabIndex = 1
        Me.mtbPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(361, 8)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(51, 13)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "Phone #:"
        '
        'txtCallName
        '
        Me.txtCallName.Location = New System.Drawing.Point(52, 5)
        Me.txtCallName.Name = "txtCallName"
        Me.txtCallName.Size = New System.Drawing.Size(247, 20)
        Me.txtCallName.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 12)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(36, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Caller:"
        '
        'TPConferences
        '
        Me.TPConferences.AutoScroll = True
        Me.TPConferences.Controls.Add(Me.clbStaffAttending)
        Me.TPConferences.Controls.Add(Me.Label40)
        Me.TPConferences.Controls.Add(Me.txtConferenceNotes)
        Me.TPConferences.Controls.Add(Me.Label39)
        Me.TPConferences.Controls.Add(Me.txtConferenceFollowUp)
        Me.TPConferences.Controls.Add(Me.Label36)
        Me.TPConferences.Controls.Add(Me.txtConferenceAttendees)
        Me.TPConferences.Controls.Add(Me.Label35)
        Me.TPConferences.Controls.Add(Me.DTPConferenceEnd)
        Me.TPConferences.Controls.Add(Me.Label34)
        Me.TPConferences.Controls.Add(Me.DTPConferenceStart)
        Me.TPConferences.Controls.Add(Me.Label33)
        Me.TPConferences.Controls.Add(Me.txtListOfBusinessSectors)
        Me.TPConferences.Controls.Add(Me.Label32)
        Me.TPConferences.Controls.Add(Me.Panel7)
        Me.TPConferences.Controls.Add(Me.Label31)
        Me.TPConferences.Controls.Add(Me.txtConferenceTopic)
        Me.TPConferences.Controls.Add(Me.Label30)
        Me.TPConferences.Controls.Add(Me.txtConferenceLocation)
        Me.TPConferences.Controls.Add(Me.Label29)
        Me.TPConferences.Controls.Add(Me.txtConferenceAttended)
        Me.TPConferences.Controls.Add(Me.Label27)
        Me.TPConferences.Location = New System.Drawing.Point(4, 22)
        Me.TPConferences.Name = "TPConferences"
        Me.TPConferences.Size = New System.Drawing.Size(1008, 398)
        Me.TPConferences.TabIndex = 3
        Me.TPConferences.Text = "Meeting/Conferences Attended"
        Me.TPConferences.UseVisualStyleBackColor = True
        '
        'clbStaffAttending
        '
        Me.clbStaffAttending.CheckOnClick = True
        Me.clbStaffAttending.FormattingEnabled = True
        Me.clbStaffAttending.Location = New System.Drawing.Point(671, 50)
        Me.clbStaffAttending.Name = "clbStaffAttending"
        Me.clbStaffAttending.Size = New System.Drawing.Size(305, 64)
        Me.clbStaffAttending.TabIndex = 5
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(585, 52)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(80, 13)
        Me.Label40.TabIndex = 29
        Me.Label40.Text = "Attending Staff:"
        '
        'txtConferenceNotes
        '
        Me.txtConferenceNotes.AcceptsReturn = True
        Me.txtConferenceNotes.Location = New System.Drawing.Point(31, 312)
        Me.txtConferenceNotes.Multiline = True
        Me.txtConferenceNotes.Name = "txtConferenceNotes"
        Me.txtConferenceNotes.Size = New System.Drawing.Size(945, 74)
        Me.txtConferenceNotes.TabIndex = 27
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(10, 296)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(38, 13)
        Me.Label39.TabIndex = 28
        Me.Label39.Text = "Notes:"
        '
        'txtConferenceFollowUp
        '
        Me.txtConferenceFollowUp.AcceptsReturn = True
        Me.txtConferenceFollowUp.Location = New System.Drawing.Point(535, 193)
        Me.txtConferenceFollowUp.Multiline = True
        Me.txtConferenceFollowUp.Name = "txtConferenceFollowUp"
        Me.txtConferenceFollowUp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConferenceFollowUp.Size = New System.Drawing.Size(441, 96)
        Me.txtConferenceFollowUp.TabIndex = 8
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(513, 174)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(307, 13)
        Me.Label36.TabIndex = 25
        Me.Label36.Text = "Description of follow-up activities as a result of this presentation:"
        '
        'txtConferenceAttendees
        '
        Me.txtConferenceAttendees.Location = New System.Drawing.Point(671, 149)
        Me.txtConferenceAttendees.Name = "txtConferenceAttendees"
        Me.txtConferenceAttendees.Size = New System.Drawing.Size(100, 20)
        Me.txtConferenceAttendees.TabIndex = 6
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(585, 149)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(80, 13)
        Me.Label35.TabIndex = 23
        Me.Label35.Text = "# of Attendees:"
        '
        'DTPConferenceEnd
        '
        Me.DTPConferenceEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPConferenceEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPConferenceEnd.Location = New System.Drawing.Point(359, 131)
        Me.DTPConferenceEnd.Name = "DTPConferenceEnd"
        Me.DTPConferenceEnd.Size = New System.Drawing.Size(90, 20)
        Me.DTPConferenceEnd.TabIndex = 3
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(290, 135)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(67, 13)
        Me.Label34.TabIndex = 21
        Me.Label34.Text = "Date Ended:"
        '
        'DTPConferenceStart
        '
        Me.DTPConferenceStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPConferenceStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPConferenceStart.Location = New System.Drawing.Point(165, 131)
        Me.DTPConferenceStart.Name = "DTPConferenceStart"
        Me.DTPConferenceStart.Size = New System.Drawing.Size(90, 20)
        Me.DTPConferenceStart.TabIndex = 2
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(89, 135)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(70, 13)
        Me.Label33.TabIndex = 19
        Me.Label33.Text = "Date Started:"
        '
        'txtListOfBusinessSectors
        '
        Me.txtListOfBusinessSectors.AcceptsReturn = True
        Me.txtListOfBusinessSectors.Location = New System.Drawing.Point(30, 193)
        Me.txtListOfBusinessSectors.Multiline = True
        Me.txtListOfBusinessSectors.Name = "txtListOfBusinessSectors"
        Me.txtListOfBusinessSectors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtListOfBusinessSectors.Size = New System.Drawing.Size(429, 96)
        Me.txtListOfBusinessSectors.TabIndex = 7
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(10, 174)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(240, 13)
        Me.Label32.TabIndex = 17
        Me.Label32.Text = "List of Business Sectors or Organizations Present:"
        '
        'Panel7
        '
        Me.Panel7.AutoSize = True
        Me.Panel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel7.Controls.Add(Me.rdbSBEAPPresentationYes)
        Me.Panel7.Controls.Add(Me.rdbSBEAPPresentationNo)
        Me.Panel7.Location = New System.Drawing.Point(671, 120)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(94, 23)
        Me.Panel7.TabIndex = 16
        '
        'rdbSBEAPPresentationYes
        '
        Me.rdbSBEAPPresentationYes.AutoSize = True
        Me.rdbSBEAPPresentationYes.Location = New System.Drawing.Point(3, 3)
        Me.rdbSBEAPPresentationYes.Name = "rdbSBEAPPresentationYes"
        Me.rdbSBEAPPresentationYes.Size = New System.Drawing.Size(43, 17)
        Me.rdbSBEAPPresentationYes.TabIndex = 0
        Me.rdbSBEAPPresentationYes.TabStop = True
        Me.rdbSBEAPPresentationYes.Text = "Yes"
        Me.rdbSBEAPPresentationYes.UseVisualStyleBackColor = True
        '
        'rdbSBEAPPresentationNo
        '
        Me.rdbSBEAPPresentationNo.AutoSize = True
        Me.rdbSBEAPPresentationNo.Location = New System.Drawing.Point(52, 3)
        Me.rdbSBEAPPresentationNo.Name = "rdbSBEAPPresentationNo"
        Me.rdbSBEAPPresentationNo.Size = New System.Drawing.Size(39, 17)
        Me.rdbSBEAPPresentationNo.TabIndex = 0
        Me.rdbSBEAPPresentationNo.TabStop = True
        Me.rdbSBEAPPresentationNo.Text = "No"
        Me.rdbSBEAPPresentationNo.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(485, 125)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(175, 13)
        Me.Label31.TabIndex = 15
        Me.Label31.Text = "Was Presentation given by SBEAP:"
        '
        'txtConferenceTopic
        '
        Me.txtConferenceTopic.AcceptsReturn = True
        Me.txtConferenceTopic.Location = New System.Drawing.Point(671, 7)
        Me.txtConferenceTopic.Multiline = True
        Me.txtConferenceTopic.Name = "txtConferenceTopic"
        Me.txtConferenceTopic.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConferenceTopic.Size = New System.Drawing.Size(305, 36)
        Me.txtConferenceTopic.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(530, 10)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(135, 13)
        Me.Label30.TabIndex = 13
        Me.Label30.Text = "Meeting/Conference Topic"
        '
        'txtConferenceLocation
        '
        Me.txtConferenceLocation.AcceptsReturn = True
        Me.txtConferenceLocation.Location = New System.Drawing.Point(165, 71)
        Me.txtConferenceLocation.Multiline = True
        Me.txtConferenceLocation.Name = "txtConferenceLocation"
        Me.txtConferenceLocation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConferenceLocation.Size = New System.Drawing.Size(295, 54)
        Me.txtConferenceLocation.TabIndex = 1
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(108, 71)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(51, 13)
        Me.Label29.TabIndex = 11
        Me.Label29.Text = "Location:"
        '
        'txtConferenceAttended
        '
        Me.txtConferenceAttended.AcceptsReturn = True
        Me.txtConferenceAttended.Location = New System.Drawing.Point(165, 7)
        Me.txtConferenceAttended.Multiline = True
        Me.txtConferenceAttended.Name = "txtConferenceAttended"
        Me.txtConferenceAttended.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConferenceAttended.Size = New System.Drawing.Size(295, 58)
        Me.txtConferenceAttended.TabIndex = 0
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(10, 7)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(151, 13)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "Meeting/Conference Attended"
        '
        'TPOtherCases
        '
        Me.TPOtherCases.Controls.Add(Me.txtCaseNotes)
        Me.TPOtherCases.Controls.Add(Me.Label299)
        Me.TPOtherCases.Location = New System.Drawing.Point(4, 22)
        Me.TPOtherCases.Name = "TPOtherCases"
        Me.TPOtherCases.Padding = New System.Windows.Forms.Padding(3)
        Me.TPOtherCases.Size = New System.Drawing.Size(1008, 398)
        Me.TPOtherCases.TabIndex = 0
        Me.TPOtherCases.Text = "Other"
        Me.TPOtherCases.UseVisualStyleBackColor = True
        '
        'txtReferralInformation
        '
        Me.txtReferralInformation.AcceptsReturn = True
        Me.txtReferralInformation.Location = New System.Drawing.Point(663, 76)
        Me.txtReferralInformation.Multiline = True
        Me.txtReferralInformation.Name = "txtReferralInformation"
        Me.txtReferralInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReferralInformation.Size = New System.Drawing.Size(328, 36)
        Me.txtReferralInformation.TabIndex = 4
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(555, 76)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(102, 13)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "Referral Information:"
        '
        'pnlBasicCaseData
        '
        Me.pnlBasicCaseData.Controls.Add(Me.chbCaseClosureLetter)
        Me.pnlBasicCaseData.Controls.Add(Me.pnlMultiClient)
        Me.pnlBasicCaseData.Controls.Add(Me.Panel9)
        Me.pnlBasicCaseData.Controls.Add(Me.pnlSingleClient)
        Me.pnlBasicCaseData.Controls.Add(Me.chbComplaintBased)
        Me.pnlBasicCaseData.Controls.Add(Me.txtCaseDescription)
        Me.pnlBasicCaseData.Controls.Add(Me.Label11)
        Me.pnlBasicCaseData.Controls.Add(Me.Label199)
        Me.pnlBasicCaseData.Controls.Add(Me.txtCaseID)
        Me.pnlBasicCaseData.Controls.Add(Me.txtReferralInformation)
        Me.pnlBasicCaseData.Controls.Add(Me.Label28)
        Me.pnlBasicCaseData.Controls.Add(Me.cboStaffResponsible)
        Me.pnlBasicCaseData.Controls.Add(Me.Label4)
        Me.pnlBasicCaseData.Controls.Add(Me.cboInteragency)
        Me.pnlBasicCaseData.Controls.Add(Me.Label399)
        Me.pnlBasicCaseData.Controls.Add(Me.DTPReferralDate)
        Me.pnlBasicCaseData.Controls.Add(Me.Label14)
        Me.pnlBasicCaseData.Controls.Add(Me.DTPCaseOpened)
        Me.pnlBasicCaseData.Controls.Add(Me.Label7)
        Me.pnlBasicCaseData.Controls.Add(Me.Label5)
        Me.pnlBasicCaseData.Controls.Add(Me.DTPCaseClosed)
        Me.pnlBasicCaseData.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBasicCaseData.Location = New System.Drawing.Point(0, 49)
        Me.pnlBasicCaseData.Name = "pnlBasicCaseData"
        Me.pnlBasicCaseData.Size = New System.Drawing.Size(1016, 144)
        Me.pnlBasicCaseData.TabIndex = 34
        '
        'chbCaseClosureLetter
        '
        Me.chbCaseClosureLetter.AutoSize = True
        Me.chbCaseClosureLetter.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbCaseClosureLetter.Location = New System.Drawing.Point(900, 27)
        Me.chbCaseClosureLetter.Name = "chbCaseClosureLetter"
        Me.chbCaseClosureLetter.Size = New System.Drawing.Size(91, 30)
        Me.chbCaseClosureLetter.TabIndex = 38
        Me.chbCaseClosureLetter.Text = "Case Closure " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Letter Sent"
        Me.chbCaseClosureLetter.UseVisualStyleBackColor = True
        '
        'pnlMultiClient
        '
        Me.pnlMultiClient.Controls.Add(Me.btnRemoveClient)
        Me.pnlMultiClient.Controls.Add(Me.txtDeleteClient)
        Me.pnlMultiClient.Controls.Add(Me.txtMultiClients)
        Me.pnlMultiClient.Controls.Add(Me.Label42)
        Me.pnlMultiClient.Controls.Add(Me.btnAddClients)
        Me.pnlMultiClient.Controls.Add(Me.txtAddMultiClient)
        Me.pnlMultiClient.Controls.Add(Me.Label13)
        Me.pnlMultiClient.Controls.Add(Me.txtMultiClientList)
        Me.pnlMultiClient.Location = New System.Drawing.Point(2, 27)
        Me.pnlMultiClient.Name = "pnlMultiClient"
        Me.pnlMultiClient.Size = New System.Drawing.Size(339, 114)
        Me.pnlMultiClient.TabIndex = 37
        Me.pnlMultiClient.Visible = False
        '
        'btnRemoveClient
        '
        Me.btnRemoveClient.AutoSize = True
        Me.btnRemoveClient.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRemoveClient.Location = New System.Drawing.Point(229, 91)
        Me.btnRemoveClient.Name = "btnRemoveClient"
        Me.btnRemoveClient.Size = New System.Drawing.Size(104, 23)
        Me.btnRemoveClient.TabIndex = 6
        Me.btnRemoveClient.Text = "Remove Customer"
        Me.btnRemoveClient.UseVisualStyleBackColor = True
        '
        'txtDeleteClient
        '
        Me.txtDeleteClient.Location = New System.Drawing.Point(121, 92)
        Me.txtDeleteClient.Name = "txtDeleteClient"
        Me.txtDeleteClient.Size = New System.Drawing.Size(100, 20)
        Me.txtDeleteClient.TabIndex = 5
        '
        'txtMultiClients
        '
        Me.txtMultiClients.Location = New System.Drawing.Point(100, 16)
        Me.txtMultiClients.Multiline = True
        Me.txtMultiClients.Name = "txtMultiClients"
        Me.txtMultiClients.ReadOnly = True
        Me.txtMultiClients.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMultiClients.Size = New System.Drawing.Size(233, 70)
        Me.txtMultiClients.TabIndex = 4
        Me.txtMultiClients.WordWrap = False
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(92, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(62, 13)
        Me.Label42.TabIndex = 3
        Me.Label42.Text = "Customer(s)"
        '
        'btnAddClients
        '
        Me.btnAddClients.AutoSize = True
        Me.btnAddClients.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddClients.Location = New System.Drawing.Point(3, 90)
        Me.btnAddClients.Name = "btnAddClients"
        Me.btnAddClients.Size = New System.Drawing.Size(88, 23)
        Me.btnAddClients.TabIndex = 2
        Me.btnAddClients.Text = "Add Customers"
        Me.btnAddClients.UseVisualStyleBackColor = True
        '
        'txtAddMultiClient
        '
        Me.txtAddMultiClient.Location = New System.Drawing.Point(8, 16)
        Me.txtAddMultiClient.Multiline = True
        Me.txtAddMultiClient.Name = "txtAddMultiClient"
        Me.txtAddMultiClient.Size = New System.Drawing.Size(78, 70)
        Me.txtAddMultiClient.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Add Clustomer(s)"
        '
        'txtMultiClientList
        '
        Me.txtMultiClientList.Location = New System.Drawing.Point(217, 16)
        Me.txtMultiClientList.Multiline = True
        Me.txtMultiClientList.Name = "txtMultiClientList"
        Me.txtMultiClientList.Size = New System.Drawing.Size(100, 61)
        Me.txtMultiClientList.TabIndex = 7
        Me.txtMultiClientList.Visible = False
        Me.txtMultiClientList.WordWrap = False
        '
        'Panel9
        '
        Me.Panel9.AutoSize = True
        Me.Panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel9.Controls.Add(Me.rdbMultiClient)
        Me.Panel9.Controls.Add(Me.rdbSingleClient)
        Me.Panel9.Location = New System.Drawing.Point(136, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(216, 20)
        Me.Panel9.TabIndex = 36
        '
        'rdbMultiClient
        '
        Me.rdbMultiClient.AutoSize = True
        Me.rdbMultiClient.Location = New System.Drawing.Point(100, 0)
        Me.rdbMultiClient.Name = "rdbMultiClient"
        Me.rdbMultiClient.Size = New System.Drawing.Size(113, 17)
        Me.rdbMultiClient.TabIndex = 1
        Me.rdbMultiClient.Text = "Multiple Customers"
        Me.rdbMultiClient.UseVisualStyleBackColor = True
        '
        'rdbSingleClient
        '
        Me.rdbSingleClient.AutoSize = True
        Me.rdbSingleClient.Location = New System.Drawing.Point(0, 0)
        Me.rdbSingleClient.Name = "rdbSingleClient"
        Me.rdbSingleClient.Size = New System.Drawing.Size(101, 17)
        Me.rdbSingleClient.TabIndex = 0
        Me.rdbSingleClient.Text = "Single Customer"
        Me.rdbSingleClient.UseVisualStyleBackColor = True
        '
        'pnlSingleClient
        '
        Me.pnlSingleClient.Controls.Add(Me.txtClientInformation)
        Me.pnlSingleClient.Controls.Add(Me.txtClientID)
        Me.pnlSingleClient.Controls.Add(Me.Label10)
        Me.pnlSingleClient.Controls.Add(Me.btnRefreshClient)
        Me.pnlSingleClient.Controls.Add(Me.txtOutstandingCases)
        Me.pnlSingleClient.Controls.Add(Me.Label12)
        Me.pnlSingleClient.Location = New System.Drawing.Point(2, 27)
        Me.pnlSingleClient.Name = "pnlSingleClient"
        Me.pnlSingleClient.Size = New System.Drawing.Size(290, 114)
        Me.pnlSingleClient.TabIndex = 35
        Me.pnlSingleClient.Visible = False
        '
        'chbComplaintBased
        '
        Me.chbComplaintBased.AutoSize = True
        Me.chbComplaintBased.Location = New System.Drawing.Point(782, 120)
        Me.chbComplaintBased.Name = "chbComplaintBased"
        Me.chbComplaintBased.Size = New System.Drawing.Size(119, 17)
        Me.chbComplaintBased.TabIndex = 34
        Me.chbComplaintBased.Text = "Enforcement Based"
        Me.chbComplaintBased.UseVisualStyleBackColor = True
        '
        'txtCaseDescription
        '
        Me.txtCaseDescription.Location = New System.Drawing.Point(358, 22)
        Me.txtCaseDescription.Multiline = True
        Me.txtCaseDescription.Name = "txtCaseDescription"
        Me.txtCaseDescription.Size = New System.Drawing.Size(196, 91)
        Me.txtCaseDescription.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(363, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 13)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "Case Description:"
        '
        'DTPReferralDate
        '
        Me.DTPReferralDate.Checked = False
        Me.DTPReferralDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReferralDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReferralDate.Location = New System.Drawing.Point(662, 118)
        Me.DTPReferralDate.Name = "DTPReferralDate"
        Me.DTPReferralDate.ShowCheckBox = True
        Me.DTPReferralDate.Size = New System.Drawing.Size(107, 20)
        Me.DTPReferralDate.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(584, 121)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 13)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Referral Date:"
        '
        'pnlModifingData
        '
        Me.pnlModifingData.Controls.Add(Me.DTPLastModified)
        Me.pnlModifingData.Controls.Add(Me.txtLastModifingStaff)
        Me.pnlModifingData.Controls.Add(Me.Label8)
        Me.pnlModifingData.Controls.Add(Me.Label9)
        Me.pnlModifingData.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlModifingData.Location = New System.Drawing.Point(0, 764)
        Me.pnlModifingData.Name = "pnlModifingData"
        Me.pnlModifingData.Size = New System.Drawing.Size(1016, 28)
        Me.pnlModifingData.TabIndex = 35
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel6)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 147)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Case - Actions"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.DTPActionOccured)
        Me.Panel6.Controls.Add(Me.Label43)
        Me.Panel6.Controls.Add(Me.txtCreationDate)
        Me.Panel6.Controls.Add(Me.Label41)
        Me.Panel6.Controls.Add(Me.txtActionCount)
        Me.Panel6.Controls.Add(Me.btnDeleteAction)
        Me.Panel6.Controls.Add(Me.btnClearActions)
        Me.Panel6.Controls.Add(Me.txtActionType)
        Me.Panel6.Controls.Add(Me.btnAddNewAction)
        Me.Panel6.Controls.Add(Me.txtActionID)
        Me.Panel6.Controls.Add(Me.dgvActionLog)
        Me.Panel6.Controls.Add(Me.cboActionType)
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Controls.Add(Me.btnViewActionType)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(3, 16)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1010, 129)
        Me.Panel6.TabIndex = 37
        '
        'DTPActionOccured
        '
        Me.DTPActionOccured.CustomFormat = "dd-MMM-yyyy"
        Me.DTPActionOccured.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPActionOccured.Location = New System.Drawing.Point(13, 83)
        Me.DTPActionOccured.Name = "DTPActionOccured"
        Me.DTPActionOccured.Size = New System.Drawing.Size(91, 20)
        Me.DTPActionOccured.TabIndex = 46
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(9, 65)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(107, 13)
        Me.Label43.TabIndex = 47
        Me.Label43.Text = "Date Action Occured"
        '
        'txtCreationDate
        '
        Me.txtCreationDate.Location = New System.Drawing.Point(10, 31)
        Me.txtCreationDate.Name = "txtCreationDate"
        Me.txtCreationDate.ReadOnly = True
        Me.txtCreationDate.Size = New System.Drawing.Size(106, 20)
        Me.txtCreationDate.TabIndex = 45
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(473, 13)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(37, 13)
        Me.Label41.TabIndex = 44
        Me.Label41.Text = "count:"
        '
        'txtActionCount
        '
        Me.txtActionCount.Location = New System.Drawing.Point(516, 9)
        Me.txtActionCount.Name = "txtActionCount"
        Me.txtActionCount.ReadOnly = True
        Me.txtActionCount.Size = New System.Drawing.Size(35, 20)
        Me.txtActionCount.TabIndex = 43
        '
        'btnDeleteAction
        '
        Me.btnDeleteAction.AutoSize = True
        Me.btnDeleteAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteAction.Location = New System.Drawing.Point(251, 30)
        Me.btnDeleteAction.Name = "btnDeleteAction"
        Me.btnDeleteAction.Size = New System.Drawing.Size(81, 23)
        Me.btnDeleteAction.TabIndex = 42
        Me.btnDeleteAction.Text = "Delete Action"
        Me.btnDeleteAction.UseVisualStyleBackColor = True
        '
        'btnClearActions
        '
        Me.btnClearActions.AutoSize = True
        Me.btnClearActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearActions.Location = New System.Drawing.Point(126, 30)
        Me.btnClearActions.Name = "btnClearActions"
        Me.btnClearActions.Size = New System.Drawing.Size(79, 23)
        Me.btnClearActions.TabIndex = 1
        Me.btnClearActions.Text = "Clear Actions"
        Me.btnClearActions.UseVisualStyleBackColor = True
        '
        'txtActionType
        '
        Me.txtActionType.Location = New System.Drawing.Point(10, 8)
        Me.txtActionType.Name = "txtActionType"
        Me.txtActionType.ReadOnly = True
        Me.txtActionType.Size = New System.Drawing.Size(361, 20)
        Me.txtActionType.TabIndex = 40
        '
        'btnAddNewAction
        '
        Me.btnAddNewAction.AutoSize = True
        Me.btnAddNewAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewAction.Location = New System.Drawing.Point(451, 82)
        Me.btnAddNewAction.Name = "btnAddNewAction"
        Me.btnAddNewAction.Size = New System.Drawing.Size(94, 23)
        Me.btnAddNewAction.TabIndex = 2
        Me.btnAddNewAction.Text = "Add New Action"
        Me.btnAddNewAction.UseVisualStyleBackColor = True
        '
        'txtActionID
        '
        Me.txtActionID.Location = New System.Drawing.Point(523, 31)
        Me.txtActionID.Name = "txtActionID"
        Me.txtActionID.ReadOnly = True
        Me.txtActionID.Size = New System.Drawing.Size(28, 20)
        Me.txtActionID.TabIndex = 37
        Me.txtActionID.Visible = False
        '
        'dgvActionLog
        '
        Me.dgvActionLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvActionLog.Location = New System.Drawing.Point(559, 1)
        Me.dgvActionLog.Name = "dgvActionLog"
        Me.dgvActionLog.Size = New System.Drawing.Size(433, 124)
        Me.dgvActionLog.TabIndex = 0
        '
        'btnViewActionType
        '
        Me.btnViewActionType.AutoSize = True
        Me.btnViewActionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewActionType.Location = New System.Drawing.Point(379, 8)
        Me.btnViewActionType.Name = "btnViewActionType"
        Me.btnViewActionType.Size = New System.Drawing.Size(73, 23)
        Me.btnViewActionType.TabIndex = 0
        Me.btnViewActionType.Text = "View Action"
        Me.btnViewActionType.UseVisualStyleBackColor = True
        '
        'SBEAPCaseWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 814)
        Me.Controls.Add(Me.TCCaseSpecificData)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pnlModifingData)
        Me.Controls.Add(Me.pnlBasicCaseData)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "SBEAPCaseWork"
        Me.Text = "Case Work"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCCaseSpecificData.ResumeLayout(False)
        Me.TPComplianceAssistance.ResumeLayout(False)
        Me.TPComplianceAssistance.PerformLayout()
        Me.TPTechnicalAssist.ResumeLayout(False)
        Me.TPTechnicalAssist.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TPPhoneCalls.ResumeLayout(False)
        Me.TPPhoneCalls.PerformLayout()
        Me.TPConferences.ResumeLayout(False)
        Me.TPConferences.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TPOtherCases.ResumeLayout(False)
        Me.TPOtherCases.PerformLayout()
        Me.pnlBasicCaseData.ResumeLayout(False)
        Me.pnlBasicCaseData.PerformLayout()
        Me.pnlMultiClient.ResumeLayout(False)
        Me.pnlMultiClient.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.pnlSingleClient.ResumeLayout(False)
        Me.pnlSingleClient.PerformLayout()
        Me.pnlModifingData.ResumeLayout(False)
        Me.pnlModifingData.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.dgvActionLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Label1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtCaseID As System.Windows.Forms.TextBox
    Friend WithEvents Label199 As System.Windows.Forms.Label
    Friend WithEvents Label299 As System.Windows.Forms.Label
    Friend WithEvents txtCaseNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label399 As System.Windows.Forms.Label
    Friend WithEvents cboInteragency As System.Windows.Forms.ComboBox
    Friend WithEvents cboStaffResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTPCaseOpened As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboActionType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DTPCaseClosed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLastModifingStaff As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DTPLastModified As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    Friend WithEvents txtClientInformation As System.Windows.Forms.TextBox
    Friend WithEvents tsbClientSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtOutstandingCases As System.Windows.Forms.TextBox
    Friend WithEvents btnRefreshClient As System.Windows.Forms.Button
    Friend WithEvents TCCaseSpecificData As System.Windows.Forms.TabControl
    Friend WithEvents TPOtherCases As System.Windows.Forms.TabPage
    Friend WithEvents TPTechnicalAssist As System.Windows.Forms.TabPage
    Friend WithEvents pnlBasicCaseData As System.Windows.Forms.Panel
    Friend WithEvents pnlModifingData As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents DTPTechAssistStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents DTPTechAssistEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents DTPTechAssistInitialContact As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboTechAssistType As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chbAirOther As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirCompCert As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirPermitAssit As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirRecordAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirEnforceAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirEmissInv As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirAppPrep As System.Windows.Forms.CheckBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chbGeneralOther As System.Windows.Forms.CheckBox
    Friend WithEvents chbGeneralEMS As System.Windows.Forms.CheckBox
    Friend WithEvents chbGeneralMultiMedia As System.Windows.Forms.CheckBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chbWasteOther As System.Windows.Forms.CheckBox
    Friend WithEvents chbWasteHazWaste As System.Windows.Forms.CheckBox
    Friend WithEvents chbWasteSolidWaste As System.Windows.Forms.CheckBox
    Friend WithEvents chbWasteUST As System.Windows.Forms.CheckBox
    Friend WithEvents chbWasteAST As System.Windows.Forms.CheckBox
    Friend WithEvents chbWasteTier2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbWasteFormR As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chbWaterOther As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterWetlands As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterSPCCC As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterEandS As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterNPDES As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterPOTW As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterIndustrial As System.Windows.Forms.CheckBox
    Friend WithEvents chbWaterConstruction As System.Windows.Forms.CheckBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chbPollSovent As System.Windows.Forms.CheckBox
    Friend WithEvents chbPollWater As System.Windows.Forms.CheckBox
    Friend WithEvents chbPollOther As System.Windows.Forms.CheckBox
    Friend WithEvents chbPollWaste As System.Windows.Forms.CheckBox
    Friend WithEvents chbPollEnergy As System.Windows.Forms.CheckBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents TPPhoneCalls As System.Windows.Forms.TabPage
    Friend WithEvents mtbPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCallName As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtReferralInformation As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TPConferences As System.Windows.Forms.TabPage
    Friend WithEvents txtConferenceAttended As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtListOfBusinessSectors As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdbSBEAPPresentationYes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSBEAPPresentationNo As System.Windows.Forms.RadioButton
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtConferenceTopic As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtConferenceLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtConferenceFollowUp As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtConferenceAttendees As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents DTPConferenceEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents DTPConferenceStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtTechnicalAssistNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtPhoneCallNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtConferenceNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents clbStaffAttending As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtActionID As System.Windows.Forms.TextBox
    Friend WithEvents btnViewActionType As System.Windows.Forms.Button
    Friend WithEvents btnAddNewAction As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents dgvActionLog As System.Windows.Forms.DataGridView
    Friend WithEvents txtCaseDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtActionType As System.Windows.Forms.TextBox
    Friend WithEvents btnClearActions As System.Windows.Forms.Button
    Friend WithEvents btnDeleteAction As System.Windows.Forms.Button
    Friend WithEvents chbOnetimeAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbFrontDeskCall As System.Windows.Forms.CheckBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtActionCount As System.Windows.Forms.TextBox
    Friend WithEvents txtCreationDate As System.Windows.Forms.TextBox
    Friend WithEvents ToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiAddNewClient As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DTPReferralDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tsbClearFrom As System.Windows.Forms.ToolStripButton
    Friend WithEvents chbComplaintBased As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSingleClient As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents rdbMultiClient As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSingleClient As System.Windows.Forms.RadioButton
    Friend WithEvents pnlMultiClient As System.Windows.Forms.Panel
    Friend WithEvents btnAddClients As System.Windows.Forms.Button
    Friend WithEvents txtAddMultiClient As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveClient As System.Windows.Forms.Button
    Friend WithEvents txtDeleteClient As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiClients As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtMultiClientList As System.Windows.Forms.TextBox
    Friend WithEvents DTPActionOccured As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents tsmDeleteCaseWork As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents chbCaseClosureLetter As System.Windows.Forms.CheckBox
    Friend WithEvents TPComplianceAssistance As System.Windows.Forms.TabPage
    Friend WithEvents chbStormWaterAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbHazardousWasteAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbSolidWasteAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbUSTAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbScrapTireAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbLeadAndAsbestosAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbAirAssist As System.Windows.Forms.CheckBox
    Friend WithEvents chbOtherAssist As System.Windows.Forms.CheckBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtComplianceAssistanceComments As System.Windows.Forms.TextBox
End Class
