<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIP_EIS_Log
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIP_EIS_Log))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSDMUStaffTools = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.TCDMUTools = New System.Windows.Forms.TabControl
        Me.TPEISLog = New System.Windows.Forms.TabPage
        Me.TabControl6 = New System.Windows.Forms.TabControl
        Me.TPQAProcess = New System.Windows.Forms.TabPage
        Me.txtAllEISDeadlineComment = New System.Windows.Forms.TextBox
        Me.txtEISDeadlineComment = New System.Windows.Forms.TextBox
        Me.dtpEISDeadline = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtAllQAComments = New System.Windows.Forms.TextBox
        Me.txtAllPointTrackingNumbers = New System.Windows.Forms.TextBox
        Me.txtAllFITrackingNumbers = New System.Windows.Forms.TextBox
        Me.chbPointErrors = New System.Windows.Forms.CheckBox
        Me.chbFIErrors = New System.Windows.Forms.CheckBox
        Me.txtPointTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label291 = New System.Windows.Forms.Label
        Me.txtFITrackingNumber = New System.Windows.Forms.TextBox
        Me.Label290 = New System.Windows.Forms.Label
        Me.btnUpdateQAData = New System.Windows.Forms.Button
        Me.dtpQAStatus = New System.Windows.Forms.DateTimePicker
        Me.Label288 = New System.Windows.Forms.Label
        Me.cboEISQAStatus = New System.Windows.Forms.ComboBox
        Me.Label287 = New System.Windows.Forms.Label
        Me.dtpQACompleted = New System.Windows.Forms.DateTimePicker
        Me.Label286 = New System.Windows.Forms.Label
        Me.dtpQAPassed = New System.Windows.Forms.DateTimePicker
        Me.Label285 = New System.Windows.Forms.Label
        Me.dtpQAStarted = New System.Windows.Forms.DateTimePicker
        Me.Label284 = New System.Windows.Forms.Label
        Me.cboEISQAStaff = New System.Windows.Forms.ComboBox
        Me.Label283 = New System.Windows.Forms.Label
        Me.txtQAComments = New System.Windows.Forms.TextBox
        Me.Label282 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Label274 = New System.Windows.Forms.Label
        Me.btnCopyAIRSNumber = New System.Windows.Forms.Button
        Me.Panel20 = New System.Windows.Forms.Panel
        Me.rdbEILogActiveNo = New System.Windows.Forms.RadioButton
        Me.rdbEILogActiveYes = New System.Windows.Forms.RadioButton
        Me.Label234 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtEILogPrePopYear = New System.Windows.Forms.TextBox
        Me.Label233 = New System.Windows.Forms.Label
        Me.txtEILogStatusMgt = New System.Windows.Forms.TextBox
        Me.dtpEILogDateEnrolled = New System.Windows.Forms.DateTimePicker
        Me.Label232 = New System.Windows.Forms.Label
        Me.dtpEILogStatusDateSubmit = New System.Windows.Forms.DateTimePicker
        Me.Label229 = New System.Windows.Forms.Label
        Me.cboEILogYear = New System.Windows.Forms.ComboBox
        Me.Label230 = New System.Windows.Forms.Label
        Me.Label231 = New System.Windows.Forms.Label
        Me.btnReloadFSData = New System.Windows.Forms.Button
        Me.txtEILogSelectedAIRSNumber = New System.Windows.Forms.TextBox
        Me.btnEILogAddNewFacility = New System.Windows.Forms.Button
        Me.btnEILogUpdate = New System.Windows.Forms.Button
        Me.Label182 = New System.Windows.Forms.Label
        Me.txtEILogUpdatedTime = New System.Windows.Forms.TextBox
        Me.Label176 = New System.Windows.Forms.Label
        Me.txtEILogUpdatedBy = New System.Windows.Forms.TextBox
        Me.txtEILogComments = New System.Windows.Forms.TextBox
        Me.Label175 = New System.Windows.Forms.Label
        Me.cboEILogAccessCode = New System.Windows.Forms.ComboBox
        Me.Label103 = New System.Windows.Forms.Label
        Me.cboEILogStatusCode = New System.Windows.Forms.ComboBox
        Me.Label102 = New System.Windows.Forms.Label
        Me.Label101 = New System.Windows.Forms.Label
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.rdbEILogOpOutNo = New System.Windows.Forms.RadioButton
        Me.rdbEILogOpOutYes = New System.Windows.Forms.RadioButton
        Me.Label96 = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.rdbEILogMailoutNo = New System.Windows.Forms.RadioButton
        Me.rdbEILogMailoutYes = New System.Windows.Forms.RadioButton
        Me.Label95 = New System.Windows.Forms.Label
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.rdbEILogEnrolledNo = New System.Windows.Forms.RadioButton
        Me.rdbEILogEnrolledYes = New System.Windows.Forms.RadioButton
        Me.txtEILogSelectedYear = New System.Windows.Forms.TextBox
        Me.txtEILogFacilityName = New System.Windows.Forms.TextBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.mtbEILogAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.TPEISStatistics = New System.Windows.Forms.TabPage
        Me.Panel17 = New System.Windows.Forms.Panel
        Me.dgvEISStats = New System.Windows.Forms.DataGridView
        Me.Panel18 = New System.Windows.Forms.Panel
        Me.btnLoadEISLog = New System.Windows.Forms.Button
        Me.mtbEISLogAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblEISCount = New System.Windows.Forms.Label
        Me.txtEISStatsCount = New System.Windows.Forms.TextBox
        Me.btnEISSummaryToExcel = New System.Windows.Forms.Button
        Me.Panel19 = New System.Windows.Forms.Panel
        Me.TCEISStats = New System.Windows.Forms.TabControl
        Me.TPEISStatSummary = New System.Windows.Forms.TabPage
        Me.Panel22 = New System.Windows.Forms.Panel
        Me.btnClearInactiveData = New System.Windows.Forms.Button
        Me.btnEISComplete = New System.Windows.Forms.Button
        Me.Label289 = New System.Windows.Forms.Label
        Me.llbEISStatsOptedOutSubmittedToEPA = New System.Windows.Forms.LinkLabel
        Me.txtEISOpOutToEPA = New System.Windows.Forms.TextBox
        Me.llbEISStatsOptedOutBegan = New System.Windows.Forms.LinkLabel
        Me.txtEISOpOutBegan = New System.Windows.Forms.TextBox
        Me.llbEISStatsOptedOutToDo = New System.Windows.Forms.LinkLabel
        Me.txtEISOpOutToDo = New System.Windows.Forms.TextBox
        Me.llbEISStatsSubmittedToDo = New System.Windows.Forms.LinkLabel
        Me.txtEISSubmittedToDo = New System.Windows.Forms.TextBox
        Me.llbEISStatsSubmittedBegan = New System.Windows.Forms.LinkLabel
        Me.txtEISSubmittedBegan = New System.Windows.Forms.TextBox
        Me.Label281 = New System.Windows.Forms.Label
        Me.Label280 = New System.Windows.Forms.Label
        Me.Label279 = New System.Windows.Forms.Label
        Me.Label278 = New System.Windows.Forms.Label
        Me.Label276 = New System.Windows.Forms.Label
        Me.Label277 = New System.Windows.Forms.Label
        Me.btnCloseOutEIS = New System.Windows.Forms.Button
        Me.btnEISBeginQA = New System.Windows.Forms.Button
        Me.llbEISNoActivity = New System.Windows.Forms.LinkLabel
        Me.txtEISNoActivity = New System.Windows.Forms.TextBox
        Me.Label251 = New System.Windows.Forms.Label
        Me.llbEISFinalized = New System.Windows.Forms.LinkLabel
        Me.txtEISFinalized = New System.Windows.Forms.TextBox
        Me.Label250 = New System.Windows.Forms.Label
        Me.llbEISInProgress = New System.Windows.Forms.LinkLabel
        Me.txtEISInProgress = New System.Windows.Forms.TextBox
        Me.Label246 = New System.Windows.Forms.Label
        Me.llbEISSubmitted = New System.Windows.Forms.LinkLabel
        Me.txtEISSubmitted = New System.Windows.Forms.TextBox
        Me.Label249 = New System.Windows.Forms.Label
        Me.llbEISQABegan = New System.Windows.Forms.LinkLabel
        Me.txtEISQABegan = New System.Windows.Forms.TextBox
        Me.Label248 = New System.Windows.Forms.Label
        Me.llbEISSubmittedToEPA = New System.Windows.Forms.LinkLabel
        Me.txtEISSubmittedToEPA = New System.Windows.Forms.TextBox
        Me.Label247 = New System.Windows.Forms.Label
        Me.txtSelectedEISStatYear = New System.Windows.Forms.TextBox
        Me.llbEISOptedOut = New System.Windows.Forms.LinkLabel
        Me.txtEISOptedOut = New System.Windows.Forms.TextBox
        Me.Label242 = New System.Windows.Forms.Label
        Me.llbEISOptedIn = New System.Windows.Forms.LinkLabel
        Me.txtEISOptedIn = New System.Windows.Forms.TextBox
        Me.Label243 = New System.Windows.Forms.Label
        Me.llbEISUnenrolled = New System.Windows.Forms.LinkLabel
        Me.txtEISUnenrolled = New System.Windows.Forms.TextBox
        Me.Label244 = New System.Windows.Forms.Label
        Me.llbEISEIUniverse = New System.Windows.Forms.LinkLabel
        Me.txtEISActiveEIUniverse = New System.Windows.Forms.TextBox
        Me.Label245 = New System.Windows.Forms.Label
        Me.llbEISMailOutTotal = New System.Windows.Forms.LinkLabel
        Me.llbEISEnrolled = New System.Windows.Forms.LinkLabel
        Me.txtEISEnrolled = New System.Windows.Forms.TextBox
        Me.Label252 = New System.Windows.Forms.Label
        Me.txtEISMailout = New System.Windows.Forms.TextBox
        Me.Label253 = New System.Windows.Forms.Label
        Me.Label241 = New System.Windows.Forms.Label
        Me.TPEISStatMailout = New System.Windows.Forms.TabPage
        Me.btnAddtoEISMailout = New System.Windows.Forms.Button
        Me.llbSearchForFacility = New System.Windows.Forms.LinkLabel
        Me.txtEISStatsMailoutCreateDate = New System.Windows.Forms.TextBox
        Me.Label271 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutUpdateDate = New System.Windows.Forms.TextBox
        Me.Label270 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutUpdateUser = New System.Windows.Forms.TextBox
        Me.Label269 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutComments = New System.Windows.Forms.TextBox
        Me.Label268 = New System.Windows.Forms.Label
        Me.txtSelectedEISMailout = New System.Windows.Forms.TextBox
        Me.Label267 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutAddress1 = New System.Windows.Forms.TextBox
        Me.Label255 = New System.Windows.Forms.Label
        Me.btnEISStatsDelete = New System.Windows.Forms.Button
        Me.btnSaveEISStatMailout = New System.Windows.Forms.Button
        Me.txtEISStatsMailoutEmailAddress = New System.Windows.Forms.TextBox
        Me.Label256 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutZipCode = New System.Windows.Forms.TextBox
        Me.Label257 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutState = New System.Windows.Forms.TextBox
        Me.Label258 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutCity = New System.Windows.Forms.TextBox
        Me.Label259 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutAddress2 = New System.Windows.Forms.TextBox
        Me.Label260 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutCompanyName = New System.Windows.Forms.TextBox
        Me.Label261 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutLastName = New System.Windows.Forms.TextBox
        Me.Label262 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutFirstName = New System.Windows.Forms.TextBox
        Me.Label263 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutPrefix = New System.Windows.Forms.TextBox
        Me.Label264 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutFacilityName = New System.Windows.Forms.TextBox
        Me.Label265 = New System.Windows.Forms.Label
        Me.txtEISStatsMailoutAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label266 = New System.Windows.Forms.Label
        Me.Panel21 = New System.Windows.Forms.Panel
        Me.btnViewEISStats = New System.Windows.Forms.Button
        Me.Label74 = New System.Windows.Forms.Label
        Me.cboEISStatisticsYear = New System.Windows.Forms.ComboBox
        Me.StatusStrip1.SuspendLayout()
        Me.TSDMUStaffTools.SuspendLayout()
        Me.TCDMUTools.SuspendLayout()
        Me.TPEISLog.SuspendLayout()
        Me.TabControl6.SuspendLayout()
        Me.TPQAProcess.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.TPEISStatistics.SuspendLayout()
        Me.Panel17.SuspendLayout()
        CType(Me.dgvEISStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel18.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.TCEISStats.SuspendLayout()
        Me.TPEISStatSummary.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.TPEISStatMailout.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFile, Me.MmiView, Me.MmiHelp})
        '
        'MmiFile
        '
        Me.MmiFile.Index = 0
        Me.MmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack})
        Me.MmiFile.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Back"
        '
        'MmiView
        '
        Me.MmiView.Index = 1
        Me.MmiView.Text = "View"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 2
        Me.MmiHelp.Text = "Help"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 691)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1016, 22)
        Me.StatusStrip1.TabIndex = 255
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(993, 17)
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
        'TSDMUStaffTools
        '
        Me.TSDMUStaffTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack})
        Me.TSDMUStaffTools.Location = New System.Drawing.Point(0, 0)
        Me.TSDMUStaffTools.Name = "TSDMUStaffTools"
        Me.TSDMUStaffTools.Size = New System.Drawing.Size(1016, 25)
        Me.TSDMUStaffTools.TabIndex = 258
        Me.TSDMUStaffTools.Text = "ToolStrip1"
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
        'TCDMUTools
        '
        Me.TCDMUTools.Controls.Add(Me.TPEISLog)
        Me.TCDMUTools.Controls.Add(Me.TPEISStatistics)
        Me.TCDMUTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDMUTools.Location = New System.Drawing.Point(0, 25)
        Me.TCDMUTools.Name = "TCDMUTools"
        Me.TCDMUTools.SelectedIndex = 0
        Me.TCDMUTools.Size = New System.Drawing.Size(1016, 666)
        Me.TCDMUTools.TabIndex = 259
        '
        'TPEISLog
        '
        Me.TPEISLog.Controls.Add(Me.TabControl6)
        Me.TPEISLog.Controls.Add(Me.Panel9)
        Me.TPEISLog.Location = New System.Drawing.Point(4, 22)
        Me.TPEISLog.Name = "TPEISLog"
        Me.TPEISLog.Size = New System.Drawing.Size(1008, 640)
        Me.TPEISLog.TabIndex = 13
        Me.TPEISLog.Text = "Emission Inventory Log"
        Me.TPEISLog.UseVisualStyleBackColor = True
        '
        'TabControl6
        '
        Me.TabControl6.Controls.Add(Me.TPQAProcess)
        Me.TabControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl6.Location = New System.Drawing.Point(0, 169)
        Me.TabControl6.Name = "TabControl6"
        Me.TabControl6.SelectedIndex = 0
        Me.TabControl6.Size = New System.Drawing.Size(1008, 471)
        Me.TabControl6.TabIndex = 1
        '
        'TPQAProcess
        '
        Me.TPQAProcess.AutoScroll = True
        Me.TPQAProcess.Controls.Add(Me.txtAllEISDeadlineComment)
        Me.TPQAProcess.Controls.Add(Me.txtEISDeadlineComment)
        Me.TPQAProcess.Controls.Add(Me.dtpEISDeadline)
        Me.TPQAProcess.Controls.Add(Me.Label4)
        Me.TPQAProcess.Controls.Add(Me.Label3)
        Me.TPQAProcess.Controls.Add(Me.txtAllQAComments)
        Me.TPQAProcess.Controls.Add(Me.txtAllPointTrackingNumbers)
        Me.TPQAProcess.Controls.Add(Me.txtAllFITrackingNumbers)
        Me.TPQAProcess.Controls.Add(Me.chbPointErrors)
        Me.TPQAProcess.Controls.Add(Me.chbFIErrors)
        Me.TPQAProcess.Controls.Add(Me.txtPointTrackingNumber)
        Me.TPQAProcess.Controls.Add(Me.Label291)
        Me.TPQAProcess.Controls.Add(Me.txtFITrackingNumber)
        Me.TPQAProcess.Controls.Add(Me.Label290)
        Me.TPQAProcess.Controls.Add(Me.btnUpdateQAData)
        Me.TPQAProcess.Controls.Add(Me.dtpQAStatus)
        Me.TPQAProcess.Controls.Add(Me.Label288)
        Me.TPQAProcess.Controls.Add(Me.cboEISQAStatus)
        Me.TPQAProcess.Controls.Add(Me.Label287)
        Me.TPQAProcess.Controls.Add(Me.dtpQACompleted)
        Me.TPQAProcess.Controls.Add(Me.Label286)
        Me.TPQAProcess.Controls.Add(Me.dtpQAPassed)
        Me.TPQAProcess.Controls.Add(Me.Label285)
        Me.TPQAProcess.Controls.Add(Me.dtpQAStarted)
        Me.TPQAProcess.Controls.Add(Me.Label284)
        Me.TPQAProcess.Controls.Add(Me.cboEISQAStaff)
        Me.TPQAProcess.Controls.Add(Me.Label283)
        Me.TPQAProcess.Controls.Add(Me.txtQAComments)
        Me.TPQAProcess.Controls.Add(Me.Label282)
        Me.TPQAProcess.Location = New System.Drawing.Point(4, 22)
        Me.TPQAProcess.Name = "TPQAProcess"
        Me.TPQAProcess.Size = New System.Drawing.Size(1000, 445)
        Me.TPQAProcess.TabIndex = 3
        Me.TPQAProcess.Text = "QA Process"
        Me.TPQAProcess.UseVisualStyleBackColor = True
        '
        'txtAllEISDeadlineComment
        '
        Me.txtAllEISDeadlineComment.Location = New System.Drawing.Point(21, 202)
        Me.txtAllEISDeadlineComment.MaxLength = 100
        Me.txtAllEISDeadlineComment.Multiline = True
        Me.txtAllEISDeadlineComment.Name = "txtAllEISDeadlineComment"
        Me.txtAllEISDeadlineComment.ReadOnly = True
        Me.txtAllEISDeadlineComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAllEISDeadlineComment.Size = New System.Drawing.Size(282, 57)
        Me.txtAllEISDeadlineComment.TabIndex = 527
        Me.txtAllEISDeadlineComment.Visible = False
        '
        'txtEISDeadlineComment
        '
        Me.txtEISDeadlineComment.Location = New System.Drawing.Point(120, 146)
        Me.txtEISDeadlineComment.MaxLength = 100
        Me.txtEISDeadlineComment.Multiline = True
        Me.txtEISDeadlineComment.Name = "txtEISDeadlineComment"
        Me.txtEISDeadlineComment.Size = New System.Drawing.Size(183, 52)
        Me.txtEISDeadlineComment.TabIndex = 526
        Me.txtEISDeadlineComment.Visible = False
        '
        'dtpEISDeadline
        '
        Me.dtpEISDeadline.Checked = False
        Me.dtpEISDeadline.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEISDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEISDeadline.Location = New System.Drawing.Point(120, 120)
        Me.dtpEISDeadline.Name = "dtpEISDeadline"
        Me.dtpEISDeadline.ShowCheckBox = True
        Me.dtpEISDeadline.Size = New System.Drawing.Size(116, 20)
        Me.dtpEISDeadline.TabIndex = 525
        Me.dtpEISDeadline.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(45, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 524
        Me.Label4.Text = "EIS Deadline"
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(44, 350)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 523
        Me.Label3.Text = "All Comments"
        '
        'txtAllQAComments
        '
        Me.txtAllQAComments.Location = New System.Drawing.Point(120, 347)
        Me.txtAllQAComments.MaxLength = 100
        Me.txtAllQAComments.Multiline = True
        Me.txtAllQAComments.Name = "txtAllQAComments"
        Me.txtAllQAComments.ReadOnly = True
        Me.txtAllQAComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAllQAComments.Size = New System.Drawing.Size(786, 93)
        Me.txtAllQAComments.TabIndex = 522
        '
        'txtAllPointTrackingNumbers
        '
        Me.txtAllPointTrackingNumbers.Location = New System.Drawing.Point(432, 177)
        Me.txtAllPointTrackingNumbers.MaxLength = 100
        Me.txtAllPointTrackingNumbers.Multiline = True
        Me.txtAllPointTrackingNumbers.Name = "txtAllPointTrackingNumbers"
        Me.txtAllPointTrackingNumbers.ReadOnly = True
        Me.txtAllPointTrackingNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAllPointTrackingNumbers.Size = New System.Drawing.Size(474, 84)
        Me.txtAllPointTrackingNumbers.TabIndex = 521
        '
        'txtAllFITrackingNumbers
        '
        Me.txtAllFITrackingNumbers.Location = New System.Drawing.Point(432, 63)
        Me.txtAllFITrackingNumbers.MaxLength = 100
        Me.txtAllFITrackingNumbers.Multiline = True
        Me.txtAllFITrackingNumbers.Name = "txtAllFITrackingNumbers"
        Me.txtAllFITrackingNumbers.ReadOnly = True
        Me.txtAllFITrackingNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAllFITrackingNumbers.Size = New System.Drawing.Size(474, 84)
        Me.txtAllFITrackingNumbers.TabIndex = 520
        '
        'chbPointErrors
        '
        Me.chbPointErrors.AutoSize = True
        Me.chbPointErrors.Location = New System.Drawing.Point(690, 153)
        Me.chbPointErrors.Name = "chbPointErrors"
        Me.chbPointErrors.Size = New System.Drawing.Size(80, 17)
        Me.chbPointErrors.TabIndex = 519
        Me.chbPointErrors.Text = "Point Errors"
        Me.chbPointErrors.UseVisualStyleBackColor = True
        '
        'chbFIErrors
        '
        Me.chbFIErrors.AutoSize = True
        Me.chbFIErrors.Location = New System.Drawing.Point(690, 40)
        Me.chbFIErrors.Name = "chbFIErrors"
        Me.chbFIErrors.Size = New System.Drawing.Size(65, 17)
        Me.chbFIErrors.TabIndex = 518
        Me.chbFIErrors.Text = "FI Errors"
        Me.chbFIErrors.UseVisualStyleBackColor = True
        '
        'txtPointTrackingNumber
        '
        Me.txtPointTrackingNumber.Location = New System.Drawing.Point(432, 151)
        Me.txtPointTrackingNumber.MaxLength = 100
        Me.txtPointTrackingNumber.Name = "txtPointTrackingNumber"
        Me.txtPointTrackingNumber.Size = New System.Drawing.Size(252, 20)
        Me.txtPointTrackingNumber.TabIndex = 517
        '
        'Label291
        '
        Me.Label291.AutoSize = True
        Me.Label291.Location = New System.Drawing.Point(309, 154)
        Me.Label291.Name = "Label291"
        Me.Label291.Size = New System.Drawing.Size(119, 13)
        Me.Label291.TabIndex = 516
        Me.Label291.Text = "Point Tracking Number:"
        '
        'txtFITrackingNumber
        '
        Me.txtFITrackingNumber.Location = New System.Drawing.Point(432, 38)
        Me.txtFITrackingNumber.MaxLength = 100
        Me.txtFITrackingNumber.Name = "txtFITrackingNumber"
        Me.txtFITrackingNumber.Size = New System.Drawing.Size(252, 20)
        Me.txtFITrackingNumber.TabIndex = 515
        '
        'Label290
        '
        Me.Label290.AutoSize = True
        Me.Label290.Location = New System.Drawing.Point(326, 42)
        Me.Label290.Name = "Label290"
        Me.Label290.Size = New System.Drawing.Size(104, 13)
        Me.Label290.TabIndex = 514
        Me.Label290.Text = "FI Tracking Number:"
        '
        'btnUpdateQAData
        '
        Me.btnUpdateQAData.AutoSize = True
        Me.btnUpdateQAData.Location = New System.Drawing.Point(810, 7)
        Me.btnUpdateQAData.Name = "btnUpdateQAData"
        Me.btnUpdateQAData.Size = New System.Drawing.Size(96, 23)
        Me.btnUpdateQAData.TabIndex = 513
        Me.btnUpdateQAData.Text = "Update QA Data"
        Me.btnUpdateQAData.UseVisualStyleBackColor = True
        '
        'dtpQAStatus
        '
        Me.dtpQAStatus.CustomFormat = "dd-MMM-yyyy"
        Me.dtpQAStatus.Enabled = False
        Me.dtpQAStatus.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQAStatus.Location = New System.Drawing.Point(672, 8)
        Me.dtpQAStatus.Name = "dtpQAStatus"
        Me.dtpQAStatus.Size = New System.Drawing.Size(100, 20)
        Me.dtpQAStatus.TabIndex = 512
        '
        'Label288
        '
        Me.Label288.AutoSize = True
        Me.Label288.Location = New System.Drawing.Point(597, 12)
        Me.Label288.Name = "Label288"
        Me.Label288.Size = New System.Drawing.Size(63, 13)
        Me.Label288.TabIndex = 511
        Me.Label288.Text = "Status Date"
        '
        'cboEISQAStatus
        '
        Me.cboEISQAStatus.FormattingEnabled = True
        Me.cboEISQAStatus.Location = New System.Drawing.Point(432, 9)
        Me.cboEISQAStatus.Name = "cboEISQAStatus"
        Me.cboEISQAStatus.Size = New System.Drawing.Size(155, 21)
        Me.cboEISQAStatus.TabIndex = 510
        '
        'Label287
        '
        Me.Label287.AutoSize = True
        Me.Label287.Location = New System.Drawing.Point(371, 12)
        Me.Label287.Name = "Label287"
        Me.Label287.Size = New System.Drawing.Size(55, 13)
        Me.Label287.TabIndex = 509
        Me.Label287.Text = "QA Status"
        '
        'dtpQACompleted
        '
        Me.dtpQACompleted.Checked = False
        Me.dtpQACompleted.CustomFormat = "dd-MMM-yyyy"
        Me.dtpQACompleted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQACompleted.Location = New System.Drawing.Point(120, 94)
        Me.dtpQACompleted.Name = "dtpQACompleted"
        Me.dtpQACompleted.ShowCheckBox = True
        Me.dtpQACompleted.Size = New System.Drawing.Size(116, 20)
        Me.dtpQACompleted.TabIndex = 508
        '
        'Label286
        '
        Me.Label286.AutoSize = True
        Me.Label286.Location = New System.Drawing.Point(36, 94)
        Me.Label286.Name = "Label286"
        Me.Label286.Size = New System.Drawing.Size(78, 13)
        Me.Label286.TabIndex = 507
        Me.Label286.Text = "EPA Submitted"
        '
        'dtpQAPassed
        '
        Me.dtpQAPassed.Checked = False
        Me.dtpQAPassed.CustomFormat = "dd-MMM-yyyy"
        Me.dtpQAPassed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQAPassed.Location = New System.Drawing.Point(120, 68)
        Me.dtpQAPassed.Name = "dtpQAPassed"
        Me.dtpQAPassed.ShowCheckBox = True
        Me.dtpQAPassed.Size = New System.Drawing.Size(116, 20)
        Me.dtpQAPassed.TabIndex = 506
        '
        'Label285
        '
        Me.Label285.AutoSize = True
        Me.Label285.Location = New System.Drawing.Point(54, 72)
        Me.Label285.Name = "Label285"
        Me.Label285.Size = New System.Drawing.Size(60, 13)
        Me.Label285.TabIndex = 505
        Me.Label285.Text = "QA Passed"
        '
        'dtpQAStarted
        '
        Me.dtpQAStarted.CustomFormat = "dd-MMM-yyyy"
        Me.dtpQAStarted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQAStarted.Location = New System.Drawing.Point(120, 42)
        Me.dtpQAStarted.Name = "dtpQAStarted"
        Me.dtpQAStarted.Size = New System.Drawing.Size(116, 20)
        Me.dtpQAStarted.TabIndex = 504
        '
        'Label284
        '
        Me.Label284.AutoSize = True
        Me.Label284.Location = New System.Drawing.Point(55, 46)
        Me.Label284.Name = "Label284"
        Me.Label284.Size = New System.Drawing.Size(59, 13)
        Me.Label284.TabIndex = 503
        Me.Label284.Text = "QA Started"
        '
        'cboEISQAStaff
        '
        Me.cboEISQAStaff.FormattingEnabled = True
        Me.cboEISQAStaff.Location = New System.Drawing.Point(120, 9)
        Me.cboEISQAStaff.Name = "cboEISQAStaff"
        Me.cboEISQAStaff.Size = New System.Drawing.Size(149, 21)
        Me.cboEISQAStaff.TabIndex = 447
        '
        'Label283
        '
        Me.Label283.AutoSize = True
        Me.Label283.Location = New System.Drawing.Point(24, 12)
        Me.Label283.Name = "Label283"
        Me.Label283.Size = New System.Drawing.Size(90, 13)
        Me.Label283.TabIndex = 445
        Me.Label283.Text = "Staff Responsible"
        '
        'txtQAComments
        '
        Me.txtQAComments.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQAComments.Location = New System.Drawing.Point(120, 265)
        Me.txtQAComments.Multiline = True
        Me.txtQAComments.Name = "txtQAComments"
        Me.txtQAComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtQAComments.Size = New System.Drawing.Size(786, 76)
        Me.txtQAComments.TabIndex = 444
        '
        'Label282
        '
        Me.Label282.AutoSize = True
        Me.Label282.Location = New System.Drawing.Point(33, 265)
        Me.Label282.Name = "Label282"
        Me.Label282.Size = New System.Drawing.Size(81, 13)
        Me.Label282.TabIndex = 443
        Me.Label282.Text = "New Comments"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Label274)
        Me.Panel9.Controls.Add(Me.btnCopyAIRSNumber)
        Me.Panel9.Controls.Add(Me.Panel20)
        Me.Panel9.Controls.Add(Me.Label234)
        Me.Panel9.Controls.Add(Me.Label1)
        Me.Panel9.Controls.Add(Me.txtEILogPrePopYear)
        Me.Panel9.Controls.Add(Me.Label233)
        Me.Panel9.Controls.Add(Me.txtEILogStatusMgt)
        Me.Panel9.Controls.Add(Me.dtpEILogDateEnrolled)
        Me.Panel9.Controls.Add(Me.Label232)
        Me.Panel9.Controls.Add(Me.dtpEILogStatusDateSubmit)
        Me.Panel9.Controls.Add(Me.Label229)
        Me.Panel9.Controls.Add(Me.cboEILogYear)
        Me.Panel9.Controls.Add(Me.Label230)
        Me.Panel9.Controls.Add(Me.Label231)
        Me.Panel9.Controls.Add(Me.btnReloadFSData)
        Me.Panel9.Controls.Add(Me.txtEILogSelectedAIRSNumber)
        Me.Panel9.Controls.Add(Me.btnEILogAddNewFacility)
        Me.Panel9.Controls.Add(Me.btnEILogUpdate)
        Me.Panel9.Controls.Add(Me.Label182)
        Me.Panel9.Controls.Add(Me.txtEILogUpdatedTime)
        Me.Panel9.Controls.Add(Me.Label176)
        Me.Panel9.Controls.Add(Me.txtEILogUpdatedBy)
        Me.Panel9.Controls.Add(Me.txtEILogComments)
        Me.Panel9.Controls.Add(Me.Label175)
        Me.Panel9.Controls.Add(Me.cboEILogAccessCode)
        Me.Panel9.Controls.Add(Me.Label103)
        Me.Panel9.Controls.Add(Me.cboEILogStatusCode)
        Me.Panel9.Controls.Add(Me.Label102)
        Me.Panel9.Controls.Add(Me.Label101)
        Me.Panel9.Controls.Add(Me.Panel15)
        Me.Panel9.Controls.Add(Me.Label96)
        Me.Panel9.Controls.Add(Me.Panel14)
        Me.Panel9.Controls.Add(Me.Label95)
        Me.Panel9.Controls.Add(Me.Panel13)
        Me.Panel9.Controls.Add(Me.txtEILogSelectedYear)
        Me.Panel9.Controls.Add(Me.txtEILogFacilityName)
        Me.Panel9.Controls.Add(Me.Label49)
        Me.Panel9.Controls.Add(Me.mtbEILogAIRSNumber)
        Me.Panel9.Controls.Add(Me.Label48)
        Me.Panel9.Controls.Add(Me.Label47)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1008, 169)
        Me.Panel9.TabIndex = 0
        '
        'Label274
        '
        Me.Label274.AutoSize = True
        Me.Label274.Location = New System.Drawing.Point(156, 35)
        Me.Label274.Name = "Label274"
        Me.Label274.Size = New System.Drawing.Size(31, 13)
        Me.Label274.TabIndex = 511
        Me.Label274.Text = "Copy"
        '
        'btnCopyAIRSNumber
        '
        Me.btnCopyAIRSNumber.AutoSize = True
        Me.btnCopyAIRSNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCopyAIRSNumber.Image = CType(resources.GetObject("btnCopyAIRSNumber.Image"), System.Drawing.Image)
        Me.btnCopyAIRSNumber.Location = New System.Drawing.Point(128, 30)
        Me.btnCopyAIRSNumber.Name = "btnCopyAIRSNumber"
        Me.btnCopyAIRSNumber.Size = New System.Drawing.Size(22, 22)
        Me.btnCopyAIRSNumber.TabIndex = 510
        Me.btnCopyAIRSNumber.UseVisualStyleBackColor = True
        '
        'Panel20
        '
        Me.Panel20.AutoSize = True
        Me.Panel20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel20.Controls.Add(Me.rdbEILogActiveNo)
        Me.Panel20.Controls.Add(Me.rdbEILogActiveYes)
        Me.Panel20.Enabled = False
        Me.Panel20.Location = New System.Drawing.Point(856, 80)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(90, 22)
        Me.Panel20.TabIndex = 506
        '
        'rdbEILogActiveNo
        '
        Me.rdbEILogActiveNo.AutoSize = True
        Me.rdbEILogActiveNo.Location = New System.Drawing.Point(48, 2)
        Me.rdbEILogActiveNo.Name = "rdbEILogActiveNo"
        Me.rdbEILogActiveNo.Size = New System.Drawing.Size(39, 17)
        Me.rdbEILogActiveNo.TabIndex = 1
        Me.rdbEILogActiveNo.TabStop = True
        Me.rdbEILogActiveNo.Text = "No"
        Me.rdbEILogActiveNo.UseVisualStyleBackColor = True
        '
        'rdbEILogActiveYes
        '
        Me.rdbEILogActiveYes.AutoSize = True
        Me.rdbEILogActiveYes.Location = New System.Drawing.Point(3, 2)
        Me.rdbEILogActiveYes.Name = "rdbEILogActiveYes"
        Me.rdbEILogActiveYes.Size = New System.Drawing.Size(43, 17)
        Me.rdbEILogActiveYes.TabIndex = 0
        Me.rdbEILogActiveYes.TabStop = True
        Me.rdbEILogActiveYes.Text = "Yes"
        Me.rdbEILogActiveYes.UseVisualStyleBackColor = True
        '
        'Label234
        '
        Me.Label234.AutoSize = True
        Me.Label234.Location = New System.Drawing.Point(783, 84)
        Me.Label234.Name = "Label234"
        Me.Label234.Size = New System.Drawing.Size(70, 13)
        Me.Label234.TabIndex = 505
        Me.Label234.Text = "Active Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(626, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 505
        Me.Label1.Text = "Status Mgmt."
        '
        'txtEILogPrePopYear
        '
        Me.txtEILogPrePopYear.Location = New System.Drawing.Point(90, 140)
        Me.txtEILogPrePopYear.Name = "txtEILogPrePopYear"
        Me.txtEILogPrePopYear.ReadOnly = True
        Me.txtEILogPrePopYear.Size = New System.Drawing.Size(60, 20)
        Me.txtEILogPrePopYear.TabIndex = 504
        '
        'Label233
        '
        Me.Label233.AutoSize = True
        Me.Label233.Location = New System.Drawing.Point(7, 145)
        Me.Label233.Name = "Label233"
        Me.Label233.Size = New System.Drawing.Size(70, 13)
        Me.Label233.TabIndex = 503
        Me.Label233.Text = "Pre-Pop Year"
        '
        'txtEILogStatusMgt
        '
        Me.txtEILogStatusMgt.Location = New System.Drawing.Point(701, 7)
        Me.txtEILogStatusMgt.Multiline = True
        Me.txtEILogStatusMgt.Name = "txtEILogStatusMgt"
        Me.txtEILogStatusMgt.ReadOnly = True
        Me.txtEILogStatusMgt.Size = New System.Drawing.Size(137, 48)
        Me.txtEILogStatusMgt.TabIndex = 443
        '
        'dtpEILogDateEnrolled
        '
        Me.dtpEILogDateEnrolled.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEILogDateEnrolled.Enabled = False
        Me.dtpEILogDateEnrolled.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEILogDateEnrolled.Location = New System.Drawing.Point(288, 85)
        Me.dtpEILogDateEnrolled.Name = "dtpEILogDateEnrolled"
        Me.dtpEILogDateEnrolled.Size = New System.Drawing.Size(86, 20)
        Me.dtpEILogDateEnrolled.TabIndex = 502
        '
        'Label232
        '
        Me.Label232.AutoSize = True
        Me.Label232.Location = New System.Drawing.Point(213, 89)
        Me.Label232.Name = "Label232"
        Me.Label232.Size = New System.Drawing.Size(71, 13)
        Me.Label232.TabIndex = 501
        Me.Label232.Text = "Date Enrolled"
        '
        'dtpEILogStatusDateSubmit
        '
        Me.dtpEILogStatusDateSubmit.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEILogStatusDateSubmit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEILogStatusDateSubmit.Location = New System.Drawing.Point(609, 57)
        Me.dtpEILogStatusDateSubmit.Name = "dtpEILogStatusDateSubmit"
        Me.dtpEILogStatusDateSubmit.Size = New System.Drawing.Size(86, 20)
        Me.dtpEILogStatusDateSubmit.TabIndex = 500
        '
        'Label229
        '
        Me.Label229.AutoSize = True
        Me.Label229.Location = New System.Drawing.Point(466, 61)
        Me.Label229.Name = "Label229"
        Me.Label229.Size = New System.Drawing.Size(137, 13)
        Me.Label229.TabIndex = 499
        Me.Label229.Text = "Date Status Code Changed"
        '
        'cboEILogYear
        '
        Me.cboEILogYear.FormattingEnabled = True
        Me.cboEILogYear.Location = New System.Drawing.Point(59, 6)
        Me.cboEILogYear.Name = "cboEILogYear"
        Me.cboEILogYear.Size = New System.Drawing.Size(63, 21)
        Me.cboEILogYear.TabIndex = 1
        '
        'Label230
        '
        Me.Label230.AutoSize = True
        Me.Label230.Location = New System.Drawing.Point(451, 10)
        Me.Label230.Name = "Label230"
        Me.Label230.Size = New System.Drawing.Size(117, 13)
        Me.Label230.TabIndex = 498
        Me.Label230.Text = "Currently selected Data"
        '
        'Label231
        '
        Me.Label231.AutoSize = True
        Me.Label231.Location = New System.Drawing.Point(156, 10)
        Me.Label231.Name = "Label231"
        Me.Label231.Size = New System.Drawing.Size(30, 13)
        Me.Label231.TabIndex = 497
        Me.Label231.Text = "View"
        '
        'btnReloadFSData
        '
        Me.btnReloadFSData.AutoSize = True
        Me.btnReloadFSData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReloadFSData.Image = CType(resources.GetObject("btnReloadFSData.Image"), System.Drawing.Image)
        Me.btnReloadFSData.Location = New System.Drawing.Point(128, 5)
        Me.btnReloadFSData.Name = "btnReloadFSData"
        Me.btnReloadFSData.Size = New System.Drawing.Size(22, 22)
        Me.btnReloadFSData.TabIndex = 3
        Me.btnReloadFSData.UseVisualStyleBackColor = True
        '
        'txtEILogSelectedAIRSNumber
        '
        Me.txtEILogSelectedAIRSNumber.Location = New System.Drawing.Point(354, 7)
        Me.txtEILogSelectedAIRSNumber.Name = "txtEILogSelectedAIRSNumber"
        Me.txtEILogSelectedAIRSNumber.ReadOnly = True
        Me.txtEILogSelectedAIRSNumber.Size = New System.Drawing.Size(86, 20)
        Me.txtEILogSelectedAIRSNumber.TabIndex = 488
        '
        'btnEILogAddNewFacility
        '
        Me.btnEILogAddNewFacility.AutoSize = True
        Me.btnEILogAddNewFacility.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEILogAddNewFacility.Location = New System.Drawing.Point(856, 42)
        Me.btnEILogAddNewFacility.Name = "btnEILogAddNewFacility"
        Me.btnEILogAddNewFacility.Size = New System.Drawing.Size(133, 23)
        Me.btnEILogAddNewFacility.TabIndex = 487
        Me.btnEILogAddNewFacility.Text = "Add New Faciltiy to Year"
        Me.btnEILogAddNewFacility.UseVisualStyleBackColor = True
        Me.btnEILogAddNewFacility.Visible = False
        '
        'btnEILogUpdate
        '
        Me.btnEILogUpdate.AutoSize = True
        Me.btnEILogUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEILogUpdate.Location = New System.Drawing.Point(856, 13)
        Me.btnEILogUpdate.Name = "btnEILogUpdate"
        Me.btnEILogUpdate.Size = New System.Drawing.Size(110, 23)
        Me.btnEILogUpdate.TabIndex = 486
        Me.btnEILogUpdate.Text = "Update Admin Data"
        Me.btnEILogUpdate.UseVisualStyleBackColor = True
        '
        'Label182
        '
        Me.Label182.AutoSize = True
        Me.Label182.Location = New System.Drawing.Point(757, 143)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(93, 13)
        Me.Label182.TabIndex = 458
        Me.Label182.Text = "Updated Datetime"
        '
        'txtEILogUpdatedTime
        '
        Me.txtEILogUpdatedTime.Location = New System.Drawing.Point(856, 136)
        Me.txtEILogUpdatedTime.Name = "txtEILogUpdatedTime"
        Me.txtEILogUpdatedTime.ReadOnly = True
        Me.txtEILogUpdatedTime.Size = New System.Drawing.Size(133, 20)
        Me.txtEILogUpdatedTime.TabIndex = 457
        '
        'Label176
        '
        Me.Label176.AutoSize = True
        Me.Label176.Location = New System.Drawing.Point(784, 117)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(63, 13)
        Me.Label176.TabIndex = 456
        Me.Label176.Text = "Updated By"
        '
        'txtEILogUpdatedBy
        '
        Me.txtEILogUpdatedBy.Location = New System.Drawing.Point(856, 110)
        Me.txtEILogUpdatedBy.Name = "txtEILogUpdatedBy"
        Me.txtEILogUpdatedBy.ReadOnly = True
        Me.txtEILogUpdatedBy.Size = New System.Drawing.Size(133, 20)
        Me.txtEILogUpdatedBy.TabIndex = 455
        '
        'txtEILogComments
        '
        Me.txtEILogComments.Location = New System.Drawing.Point(288, 111)
        Me.txtEILogComments.Multiline = True
        Me.txtEILogComments.Name = "txtEILogComments"
        Me.txtEILogComments.ReadOnly = True
        Me.txtEILogComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEILogComments.Size = New System.Drawing.Size(469, 48)
        Me.txtEILogComments.TabIndex = 442
        '
        'Label175
        '
        Me.Label175.AutoSize = True
        Me.Label175.Location = New System.Drawing.Point(228, 112)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(56, 13)
        Me.Label175.TabIndex = 441
        Me.Label175.Text = "Comments"
        '
        'cboEILogAccessCode
        '
        Me.cboEILogAccessCode.FormattingEnabled = True
        Me.cboEILogAccessCode.Location = New System.Drawing.Point(454, 85)
        Me.cboEILogAccessCode.Name = "cboEILogAccessCode"
        Me.cboEILogAccessCode.Size = New System.Drawing.Size(303, 21)
        Me.cboEILogAccessCode.TabIndex = 440
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Location = New System.Drawing.Point(378, 89)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(70, 13)
        Me.Label103.TabIndex = 439
        Me.Label103.Text = "Access Code"
        '
        'cboEILogStatusCode
        '
        Me.cboEILogStatusCode.FormattingEnabled = True
        Me.cboEILogStatusCode.Location = New System.Drawing.Point(288, 57)
        Me.cboEILogStatusCode.Name = "cboEILogStatusCode"
        Me.cboEILogStatusCode.Size = New System.Drawing.Size(152, 21)
        Me.cboEILogStatusCode.TabIndex = 438
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(219, 61)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(65, 13)
        Me.Label102.TabIndex = 437
        Me.Label102.Text = "Status Code"
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(12, 118)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(41, 13)
        Me.Label101.TabIndex = 435
        Me.Label101.Text = "Op Out"
        '
        'Panel15
        '
        Me.Panel15.AutoSize = True
        Me.Panel15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel15.Controls.Add(Me.rdbEILogOpOutNo)
        Me.Panel15.Controls.Add(Me.rdbEILogOpOutYes)
        Me.Panel15.Enabled = False
        Me.Panel15.Location = New System.Drawing.Point(59, 115)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(91, 22)
        Me.Panel15.TabIndex = 436
        '
        'rdbEILogOpOutNo
        '
        Me.rdbEILogOpOutNo.AutoSize = True
        Me.rdbEILogOpOutNo.Location = New System.Drawing.Point(49, 2)
        Me.rdbEILogOpOutNo.Name = "rdbEILogOpOutNo"
        Me.rdbEILogOpOutNo.Size = New System.Drawing.Size(39, 17)
        Me.rdbEILogOpOutNo.TabIndex = 1
        Me.rdbEILogOpOutNo.TabStop = True
        Me.rdbEILogOpOutNo.Text = "No"
        Me.rdbEILogOpOutNo.UseVisualStyleBackColor = True
        '
        'rdbEILogOpOutYes
        '
        Me.rdbEILogOpOutYes.AutoSize = True
        Me.rdbEILogOpOutYes.Location = New System.Drawing.Point(3, 2)
        Me.rdbEILogOpOutYes.Name = "rdbEILogOpOutYes"
        Me.rdbEILogOpOutYes.Size = New System.Drawing.Size(43, 17)
        Me.rdbEILogOpOutYes.TabIndex = 0
        Me.rdbEILogOpOutYes.TabStop = True
        Me.rdbEILogOpOutYes.Text = "Yes"
        Me.rdbEILogOpOutYes.UseVisualStyleBackColor = True
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(12, 64)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(41, 13)
        Me.Label96.TabIndex = 433
        Me.Label96.Text = "Mailout"
        '
        'Panel14
        '
        Me.Panel14.AutoSize = True
        Me.Panel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel14.Controls.Add(Me.rdbEILogMailoutNo)
        Me.Panel14.Controls.Add(Me.rdbEILogMailoutYes)
        Me.Panel14.Enabled = False
        Me.Panel14.Location = New System.Drawing.Point(59, 57)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(91, 22)
        Me.Panel14.TabIndex = 434
        '
        'rdbEILogMailoutNo
        '
        Me.rdbEILogMailoutNo.AutoSize = True
        Me.rdbEILogMailoutNo.Location = New System.Drawing.Point(49, 2)
        Me.rdbEILogMailoutNo.Name = "rdbEILogMailoutNo"
        Me.rdbEILogMailoutNo.Size = New System.Drawing.Size(39, 17)
        Me.rdbEILogMailoutNo.TabIndex = 1
        Me.rdbEILogMailoutNo.TabStop = True
        Me.rdbEILogMailoutNo.Text = "No"
        Me.rdbEILogMailoutNo.UseVisualStyleBackColor = True
        '
        'rdbEILogMailoutYes
        '
        Me.rdbEILogMailoutYes.AutoSize = True
        Me.rdbEILogMailoutYes.Location = New System.Drawing.Point(3, 2)
        Me.rdbEILogMailoutYes.Name = "rdbEILogMailoutYes"
        Me.rdbEILogMailoutYes.Size = New System.Drawing.Size(43, 17)
        Me.rdbEILogMailoutYes.TabIndex = 0
        Me.rdbEILogMailoutYes.TabStop = True
        Me.rdbEILogMailoutYes.Text = "Yes"
        Me.rdbEILogMailoutYes.UseVisualStyleBackColor = True
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(8, 91)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(45, 13)
        Me.Label95.TabIndex = 431
        Me.Label95.Text = "Enrolled"
        '
        'Panel13
        '
        Me.Panel13.AutoSize = True
        Me.Panel13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel13.Controls.Add(Me.rdbEILogEnrolledNo)
        Me.Panel13.Controls.Add(Me.rdbEILogEnrolledYes)
        Me.Panel13.Enabled = False
        Me.Panel13.Location = New System.Drawing.Point(59, 85)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(91, 22)
        Me.Panel13.TabIndex = 432
        '
        'rdbEILogEnrolledNo
        '
        Me.rdbEILogEnrolledNo.AutoSize = True
        Me.rdbEILogEnrolledNo.Location = New System.Drawing.Point(49, 2)
        Me.rdbEILogEnrolledNo.Name = "rdbEILogEnrolledNo"
        Me.rdbEILogEnrolledNo.Size = New System.Drawing.Size(39, 17)
        Me.rdbEILogEnrolledNo.TabIndex = 1
        Me.rdbEILogEnrolledNo.TabStop = True
        Me.rdbEILogEnrolledNo.Text = "No"
        Me.rdbEILogEnrolledNo.UseVisualStyleBackColor = True
        '
        'rdbEILogEnrolledYes
        '
        Me.rdbEILogEnrolledYes.AutoSize = True
        Me.rdbEILogEnrolledYes.Location = New System.Drawing.Point(3, 2)
        Me.rdbEILogEnrolledYes.Name = "rdbEILogEnrolledYes"
        Me.rdbEILogEnrolledYes.Size = New System.Drawing.Size(43, 17)
        Me.rdbEILogEnrolledYes.TabIndex = 0
        Me.rdbEILogEnrolledYes.TabStop = True
        Me.rdbEILogEnrolledYes.Text = "Yes"
        Me.rdbEILogEnrolledYes.UseVisualStyleBackColor = True
        '
        'txtEILogSelectedYear
        '
        Me.txtEILogSelectedYear.Location = New System.Drawing.Point(288, 7)
        Me.txtEILogSelectedYear.Name = "txtEILogSelectedYear"
        Me.txtEILogSelectedYear.ReadOnly = True
        Me.txtEILogSelectedYear.Size = New System.Drawing.Size(60, 20)
        Me.txtEILogSelectedYear.TabIndex = 6
        '
        'txtEILogFacilityName
        '
        Me.txtEILogFacilityName.Location = New System.Drawing.Point(288, 30)
        Me.txtEILogFacilityName.Name = "txtEILogFacilityName"
        Me.txtEILogFacilityName.ReadOnly = True
        Me.txtEILogFacilityName.Size = New System.Drawing.Size(322, 20)
        Me.txtEILogFacilityName.TabIndex = 5
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(194, 34)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(90, 13)
        Me.Label49.TabIndex = 4
        Me.Label49.Text = "EIS Facility Name"
        '
        'mtbEILogAIRSNumber
        '
        Me.mtbEILogAIRSNumber.Location = New System.Drawing.Point(59, 32)
        Me.mtbEILogAIRSNumber.Mask = "000-00000"
        Me.mtbEILogAIRSNumber.Name = "mtbEILogAIRSNumber"
        Me.mtbEILogAIRSNumber.Size = New System.Drawing.Size(63, 20)
        Me.mtbEILogAIRSNumber.TabIndex = 2
        Me.mtbEILogAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(11, 37)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(42, 13)
        Me.Label48.TabIndex = 1
        Me.Label48.Text = "AIRS #"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(4, 10)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(49, 13)
        Me.Label47.TabIndex = 0
        Me.Label47.Text = "EIS Year"
        '
        'TPEISStatistics
        '
        Me.TPEISStatistics.Controls.Add(Me.Panel17)
        Me.TPEISStatistics.Controls.Add(Me.Panel19)
        Me.TPEISStatistics.Location = New System.Drawing.Point(4, 22)
        Me.TPEISStatistics.Name = "TPEISStatistics"
        Me.TPEISStatistics.Size = New System.Drawing.Size(1008, 640)
        Me.TPEISStatistics.TabIndex = 14
        Me.TPEISStatistics.Text = "EIS Statistics"
        Me.TPEISStatistics.UseVisualStyleBackColor = True
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.dgvEISStats)
        Me.Panel17.Controls.Add(Me.Panel18)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(446, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(562, 640)
        Me.Panel17.TabIndex = 0
        '
        'dgvEISStats
        '
        Me.dgvEISStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEISStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEISStats.Location = New System.Drawing.Point(0, 53)
        Me.dgvEISStats.Name = "dgvEISStats"
        Me.dgvEISStats.Size = New System.Drawing.Size(562, 587)
        Me.dgvEISStats.TabIndex = 1
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.btnLoadEISLog)
        Me.Panel18.Controls.Add(Me.mtbEISLogAIRSNumber)
        Me.Panel18.Controls.Add(Me.Label2)
        Me.Panel18.Controls.Add(Me.lblEISCount)
        Me.Panel18.Controls.Add(Me.txtEISStatsCount)
        Me.Panel18.Controls.Add(Me.btnEISSummaryToExcel)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(562, 53)
        Me.Panel18.TabIndex = 0
        '
        'btnLoadEISLog
        '
        Me.btnLoadEISLog.Location = New System.Drawing.Point(375, 21)
        Me.btnLoadEISLog.Name = "btnLoadEISLog"
        Me.btnLoadEISLog.Size = New System.Drawing.Size(95, 23)
        Me.btnLoadEISLog.TabIndex = 102
        Me.btnLoadEISLog.Text = "Load Log Data"
        Me.btnLoadEISLog.UseVisualStyleBackColor = True
        '
        'mtbEISLogAIRSNumber
        '
        Me.mtbEISLogAIRSNumber.Location = New System.Drawing.Point(306, 24)
        Me.mtbEISLogAIRSNumber.Mask = "000-00000"
        Me.mtbEISLogAIRSNumber.Name = "mtbEISLogAIRSNumber"
        Me.mtbEISLogAIRSNumber.Size = New System.Drawing.Size(63, 20)
        Me.mtbEISLogAIRSNumber.TabIndex = 101
        Me.mtbEISLogAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(258, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "AIRS #"
        '
        'lblEISCount
        '
        Me.lblEISCount.AutoSize = True
        Me.lblEISCount.Location = New System.Drawing.Point(6, 7)
        Me.lblEISCount.Name = "lblEISCount"
        Me.lblEISCount.Size = New System.Drawing.Size(38, 13)
        Me.lblEISCount.TabIndex = 84
        Me.lblEISCount.Text = "Count:"
        '
        'txtEISStatsCount
        '
        Me.txtEISStatsCount.Location = New System.Drawing.Point(32, 24)
        Me.txtEISStatsCount.Name = "txtEISStatsCount"
        Me.txtEISStatsCount.ReadOnly = True
        Me.txtEISStatsCount.Size = New System.Drawing.Size(100, 20)
        Me.txtEISStatsCount.TabIndex = 0
        '
        'btnEISSummaryToExcel
        '
        Me.btnEISSummaryToExcel.Location = New System.Drawing.Point(138, 22)
        Me.btnEISSummaryToExcel.Name = "btnEISSummaryToExcel"
        Me.btnEISSummaryToExcel.Size = New System.Drawing.Size(95, 23)
        Me.btnEISSummaryToExcel.TabIndex = 99
        Me.btnEISSummaryToExcel.Text = "Export To Excel"
        Me.btnEISSummaryToExcel.UseVisualStyleBackColor = True
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.TCEISStats)
        Me.Panel19.Controls.Add(Me.Panel21)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel19.Location = New System.Drawing.Point(0, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(446, 640)
        Me.Panel19.TabIndex = 2
        '
        'TCEISStats
        '
        Me.TCEISStats.Controls.Add(Me.TPEISStatSummary)
        Me.TCEISStats.Controls.Add(Me.TPEISStatMailout)
        Me.TCEISStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCEISStats.Location = New System.Drawing.Point(0, 44)
        Me.TCEISStats.Name = "TCEISStats"
        Me.TCEISStats.SelectedIndex = 0
        Me.TCEISStats.Size = New System.Drawing.Size(446, 596)
        Me.TCEISStats.TabIndex = 1
        '
        'TPEISStatSummary
        '
        Me.TPEISStatSummary.AutoScroll = True
        Me.TPEISStatSummary.Controls.Add(Me.Panel22)
        Me.TPEISStatSummary.Location = New System.Drawing.Point(4, 22)
        Me.TPEISStatSummary.Name = "TPEISStatSummary"
        Me.TPEISStatSummary.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEISStatSummary.Size = New System.Drawing.Size(438, 570)
        Me.TPEISStatSummary.TabIndex = 0
        Me.TPEISStatSummary.Text = "Summary"
        Me.TPEISStatSummary.UseVisualStyleBackColor = True
        '
        'Panel22
        '
        Me.Panel22.AutoScroll = True
        Me.Panel22.Controls.Add(Me.btnClearInactiveData)
        Me.Panel22.Controls.Add(Me.btnEISComplete)
        Me.Panel22.Controls.Add(Me.Label289)
        Me.Panel22.Controls.Add(Me.llbEISStatsOptedOutSubmittedToEPA)
        Me.Panel22.Controls.Add(Me.txtEISOpOutToEPA)
        Me.Panel22.Controls.Add(Me.llbEISStatsOptedOutBegan)
        Me.Panel22.Controls.Add(Me.txtEISOpOutBegan)
        Me.Panel22.Controls.Add(Me.llbEISStatsOptedOutToDo)
        Me.Panel22.Controls.Add(Me.txtEISOpOutToDo)
        Me.Panel22.Controls.Add(Me.llbEISStatsSubmittedToDo)
        Me.Panel22.Controls.Add(Me.txtEISSubmittedToDo)
        Me.Panel22.Controls.Add(Me.llbEISStatsSubmittedBegan)
        Me.Panel22.Controls.Add(Me.txtEISSubmittedBegan)
        Me.Panel22.Controls.Add(Me.Label281)
        Me.Panel22.Controls.Add(Me.Label280)
        Me.Panel22.Controls.Add(Me.Label279)
        Me.Panel22.Controls.Add(Me.Label278)
        Me.Panel22.Controls.Add(Me.Label276)
        Me.Panel22.Controls.Add(Me.Label277)
        Me.Panel22.Controls.Add(Me.btnCloseOutEIS)
        Me.Panel22.Controls.Add(Me.btnEISBeginQA)
        Me.Panel22.Controls.Add(Me.llbEISNoActivity)
        Me.Panel22.Controls.Add(Me.txtEISNoActivity)
        Me.Panel22.Controls.Add(Me.Label251)
        Me.Panel22.Controls.Add(Me.llbEISFinalized)
        Me.Panel22.Controls.Add(Me.txtEISFinalized)
        Me.Panel22.Controls.Add(Me.Label250)
        Me.Panel22.Controls.Add(Me.llbEISInProgress)
        Me.Panel22.Controls.Add(Me.txtEISInProgress)
        Me.Panel22.Controls.Add(Me.Label246)
        Me.Panel22.Controls.Add(Me.llbEISSubmitted)
        Me.Panel22.Controls.Add(Me.txtEISSubmitted)
        Me.Panel22.Controls.Add(Me.Label249)
        Me.Panel22.Controls.Add(Me.llbEISQABegan)
        Me.Panel22.Controls.Add(Me.txtEISQABegan)
        Me.Panel22.Controls.Add(Me.Label248)
        Me.Panel22.Controls.Add(Me.llbEISSubmittedToEPA)
        Me.Panel22.Controls.Add(Me.txtEISSubmittedToEPA)
        Me.Panel22.Controls.Add(Me.Label247)
        Me.Panel22.Controls.Add(Me.txtSelectedEISStatYear)
        Me.Panel22.Controls.Add(Me.llbEISOptedOut)
        Me.Panel22.Controls.Add(Me.txtEISOptedOut)
        Me.Panel22.Controls.Add(Me.Label242)
        Me.Panel22.Controls.Add(Me.llbEISOptedIn)
        Me.Panel22.Controls.Add(Me.txtEISOptedIn)
        Me.Panel22.Controls.Add(Me.Label243)
        Me.Panel22.Controls.Add(Me.llbEISUnenrolled)
        Me.Panel22.Controls.Add(Me.txtEISUnenrolled)
        Me.Panel22.Controls.Add(Me.Label244)
        Me.Panel22.Controls.Add(Me.llbEISEIUniverse)
        Me.Panel22.Controls.Add(Me.txtEISActiveEIUniverse)
        Me.Panel22.Controls.Add(Me.Label245)
        Me.Panel22.Controls.Add(Me.llbEISMailOutTotal)
        Me.Panel22.Controls.Add(Me.llbEISEnrolled)
        Me.Panel22.Controls.Add(Me.txtEISEnrolled)
        Me.Panel22.Controls.Add(Me.Label252)
        Me.Panel22.Controls.Add(Me.txtEISMailout)
        Me.Panel22.Controls.Add(Me.Label253)
        Me.Panel22.Controls.Add(Me.Label241)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22.Location = New System.Drawing.Point(3, 3)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(432, 564)
        Me.Panel22.TabIndex = 96
        '
        'btnClearInactiveData
        '
        Me.btnClearInactiveData.AutoSize = True
        Me.btnClearInactiveData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearInactiveData.Location = New System.Drawing.Point(107, 466)
        Me.btnClearInactiveData.Name = "btnClearInactiveData"
        Me.btnClearInactiveData.Size = New System.Drawing.Size(108, 23)
        Me.btnClearInactiveData.TabIndex = 149
        Me.btnClearInactiveData.Text = "Clear Inactive Data"
        Me.btnClearInactiveData.UseVisualStyleBackColor = True
        Me.btnClearInactiveData.Visible = False
        '
        'btnEISComplete
        '
        Me.btnEISComplete.AutoSize = True
        Me.btnEISComplete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEISComplete.Location = New System.Drawing.Point(9, 466)
        Me.btnEISComplete.Name = "btnEISComplete"
        Me.btnEISComplete.Size = New System.Drawing.Size(81, 23)
        Me.btnEISComplete.TabIndex = 148
        Me.btnEISComplete.Text = "EIS Complete"
        Me.btnEISComplete.UseVisualStyleBackColor = True
        Me.btnEISComplete.Visible = False
        '
        'Label289
        '
        Me.Label289.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label289.Location = New System.Drawing.Point(12, 454)
        Me.Label289.Name = "Label289"
        Me.Label289.Size = New System.Drawing.Size(350, 1)
        Me.Label289.TabIndex = 147
        '
        'llbEISStatsOptedOutSubmittedToEPA
        '
        Me.llbEISStatsOptedOutSubmittedToEPA.AutoSize = True
        Me.llbEISStatsOptedOutSubmittedToEPA.Location = New System.Drawing.Point(328, 427)
        Me.llbEISStatsOptedOutSubmittedToEPA.Name = "llbEISStatsOptedOutSubmittedToEPA"
        Me.llbEISStatsOptedOutSubmittedToEPA.Size = New System.Drawing.Size(30, 13)
        Me.llbEISStatsOptedOutSubmittedToEPA.TabIndex = 146
        Me.llbEISStatsOptedOutSubmittedToEPA.TabStop = True
        Me.llbEISStatsOptedOutSubmittedToEPA.Text = "View"
        '
        'txtEISOpOutToEPA
        '
        Me.txtEISOpOutToEPA.Location = New System.Drawing.Point(247, 424)
        Me.txtEISOpOutToEPA.Name = "txtEISOpOutToEPA"
        Me.txtEISOpOutToEPA.ReadOnly = True
        Me.txtEISOpOutToEPA.Size = New System.Drawing.Size(69, 20)
        Me.txtEISOpOutToEPA.TabIndex = 145
        '
        'llbEISStatsOptedOutBegan
        '
        Me.llbEISStatsOptedOutBegan.AutoSize = True
        Me.llbEISStatsOptedOutBegan.Location = New System.Drawing.Point(328, 400)
        Me.llbEISStatsOptedOutBegan.Name = "llbEISStatsOptedOutBegan"
        Me.llbEISStatsOptedOutBegan.Size = New System.Drawing.Size(30, 13)
        Me.llbEISStatsOptedOutBegan.TabIndex = 144
        Me.llbEISStatsOptedOutBegan.TabStop = True
        Me.llbEISStatsOptedOutBegan.Text = "View"
        '
        'txtEISOpOutBegan
        '
        Me.txtEISOpOutBegan.Location = New System.Drawing.Point(247, 397)
        Me.txtEISOpOutBegan.Name = "txtEISOpOutBegan"
        Me.txtEISOpOutBegan.ReadOnly = True
        Me.txtEISOpOutBegan.Size = New System.Drawing.Size(69, 20)
        Me.txtEISOpOutBegan.TabIndex = 143
        '
        'llbEISStatsOptedOutToDo
        '
        Me.llbEISStatsOptedOutToDo.AutoSize = True
        Me.llbEISStatsOptedOutToDo.Location = New System.Drawing.Point(328, 342)
        Me.llbEISStatsOptedOutToDo.Name = "llbEISStatsOptedOutToDo"
        Me.llbEISStatsOptedOutToDo.Size = New System.Drawing.Size(30, 13)
        Me.llbEISStatsOptedOutToDo.TabIndex = 142
        Me.llbEISStatsOptedOutToDo.TabStop = True
        Me.llbEISStatsOptedOutToDo.Text = "View"
        '
        'txtEISOpOutToDo
        '
        Me.txtEISOpOutToDo.Location = New System.Drawing.Point(247, 339)
        Me.txtEISOpOutToDo.Name = "txtEISOpOutToDo"
        Me.txtEISOpOutToDo.ReadOnly = True
        Me.txtEISOpOutToDo.Size = New System.Drawing.Size(69, 20)
        Me.txtEISOpOutToDo.TabIndex = 141
        '
        'llbEISStatsSubmittedToDo
        '
        Me.llbEISStatsSubmittedToDo.AutoSize = True
        Me.llbEISStatsSubmittedToDo.Location = New System.Drawing.Point(186, 342)
        Me.llbEISStatsSubmittedToDo.Name = "llbEISStatsSubmittedToDo"
        Me.llbEISStatsSubmittedToDo.Size = New System.Drawing.Size(30, 13)
        Me.llbEISStatsSubmittedToDo.TabIndex = 140
        Me.llbEISStatsSubmittedToDo.TabStop = True
        Me.llbEISStatsSubmittedToDo.Text = "View"
        '
        'txtEISSubmittedToDo
        '
        Me.txtEISSubmittedToDo.Location = New System.Drawing.Point(107, 339)
        Me.txtEISSubmittedToDo.Name = "txtEISSubmittedToDo"
        Me.txtEISSubmittedToDo.ReadOnly = True
        Me.txtEISSubmittedToDo.Size = New System.Drawing.Size(69, 20)
        Me.txtEISSubmittedToDo.TabIndex = 139
        '
        'llbEISStatsSubmittedBegan
        '
        Me.llbEISStatsSubmittedBegan.AutoSize = True
        Me.llbEISStatsSubmittedBegan.Location = New System.Drawing.Point(186, 397)
        Me.llbEISStatsSubmittedBegan.Name = "llbEISStatsSubmittedBegan"
        Me.llbEISStatsSubmittedBegan.Size = New System.Drawing.Size(30, 13)
        Me.llbEISStatsSubmittedBegan.TabIndex = 138
        Me.llbEISStatsSubmittedBegan.TabStop = True
        Me.llbEISStatsSubmittedBegan.Text = "View"
        '
        'txtEISSubmittedBegan
        '
        Me.txtEISSubmittedBegan.Location = New System.Drawing.Point(107, 394)
        Me.txtEISSubmittedBegan.Name = "txtEISSubmittedBegan"
        Me.txtEISSubmittedBegan.ReadOnly = True
        Me.txtEISSubmittedBegan.Size = New System.Drawing.Size(69, 20)
        Me.txtEISSubmittedBegan.TabIndex = 137
        '
        'Label281
        '
        Me.Label281.AutoSize = True
        Me.Label281.Location = New System.Drawing.Point(59, 394)
        Me.Label281.Name = "Label281"
        Me.Label281.Size = New System.Drawing.Size(41, 13)
        Me.Label281.TabIndex = 136
        Me.Label281.Text = "Started"
        '
        'Label280
        '
        Me.Label280.AutoSize = True
        Me.Label280.Location = New System.Drawing.Point(63, 339)
        Me.Label280.Name = "Label280"
        Me.Label280.Size = New System.Drawing.Size(37, 13)
        Me.Label280.TabIndex = 135
        Me.Label280.Text = "To Do"
        '
        'Label279
        '
        Me.Label279.AutoSize = True
        Me.Label279.Location = New System.Drawing.Point(244, 323)
        Me.Label279.Name = "Label279"
        Me.Label279.Size = New System.Drawing.Size(56, 13)
        Me.Label279.TabIndex = 134
        Me.Label279.Text = "Opted Out"
        '
        'Label278
        '
        Me.Label278.AutoSize = True
        Me.Label278.Location = New System.Drawing.Point(104, 323)
        Me.Label278.Name = "Label278"
        Me.Label278.Size = New System.Drawing.Size(54, 13)
        Me.Label278.TabIndex = 133
        Me.Label278.Text = "Submitted"
        '
        'Label276
        '
        Me.Label276.AutoSize = True
        Me.Label276.Location = New System.Drawing.Point(10, 308)
        Me.Label276.Name = "Label276"
        Me.Label276.Size = New System.Drawing.Size(63, 13)
        Me.Label276.TabIndex = 132
        Me.Label276.Text = "QA Process"
        '
        'Label277
        '
        Me.Label277.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label277.Location = New System.Drawing.Point(13, 298)
        Me.Label277.Name = "Label277"
        Me.Label277.Size = New System.Drawing.Size(350, 1)
        Me.Label277.TabIndex = 131
        '
        'btnCloseOutEIS
        '
        Me.btnCloseOutEIS.AutoSize = True
        Me.btnCloseOutEIS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCloseOutEIS.Location = New System.Drawing.Point(325, 32)
        Me.btnCloseOutEIS.Name = "btnCloseOutEIS"
        Me.btnCloseOutEIS.Size = New System.Drawing.Size(81, 23)
        Me.btnCloseOutEIS.TabIndex = 126
        Me.btnCloseOutEIS.Text = "Close out EIS"
        Me.btnCloseOutEIS.UseVisualStyleBackColor = True
        Me.btnCloseOutEIS.Visible = False
        '
        'btnEISBeginQA
        '
        Me.btnEISBeginQA.AutoSize = True
        Me.btnEISBeginQA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEISBeginQA.Location = New System.Drawing.Point(107, 365)
        Me.btnEISBeginQA.Name = "btnEISBeginQA"
        Me.btnEISBeginQA.Size = New System.Drawing.Size(98, 23)
        Me.btnEISBeginQA.TabIndex = 125
        Me.btnEISBeginQA.Text = "Start QA Process"
        Me.btnEISBeginQA.UseVisualStyleBackColor = True
        '
        'llbEISNoActivity
        '
        Me.llbEISNoActivity.AutoSize = True
        Me.llbEISNoActivity.Location = New System.Drawing.Point(270, 144)
        Me.llbEISNoActivity.Name = "llbEISNoActivity"
        Me.llbEISNoActivity.Size = New System.Drawing.Size(30, 13)
        Me.llbEISNoActivity.TabIndex = 123
        Me.llbEISNoActivity.TabStop = True
        Me.llbEISNoActivity.Text = "View"
        '
        'txtEISNoActivity
        '
        Me.txtEISNoActivity.Location = New System.Drawing.Point(158, 140)
        Me.txtEISNoActivity.Name = "txtEISNoActivity"
        Me.txtEISNoActivity.ReadOnly = True
        Me.txtEISNoActivity.Size = New System.Drawing.Size(100, 20)
        Me.txtEISNoActivity.TabIndex = 122
        '
        'Label251
        '
        Me.Label251.AutoSize = True
        Me.Label251.Location = New System.Drawing.Point(94, 143)
        Me.Label251.Name = "Label251"
        Me.Label251.Size = New System.Drawing.Size(58, 13)
        Me.Label251.TabIndex = 124
        Me.Label251.Text = "No Activity"
        '
        'llbEISFinalized
        '
        Me.llbEISFinalized.AutoSize = True
        Me.llbEISFinalized.Location = New System.Drawing.Point(228, 275)
        Me.llbEISFinalized.Name = "llbEISFinalized"
        Me.llbEISFinalized.Size = New System.Drawing.Size(30, 13)
        Me.llbEISFinalized.TabIndex = 120
        Me.llbEISFinalized.TabStop = True
        Me.llbEISFinalized.Text = "View"
        '
        'txtEISFinalized
        '
        Me.txtEISFinalized.Location = New System.Drawing.Point(116, 271)
        Me.txtEISFinalized.Name = "txtEISFinalized"
        Me.txtEISFinalized.ReadOnly = True
        Me.txtEISFinalized.Size = New System.Drawing.Size(100, 20)
        Me.txtEISFinalized.TabIndex = 119
        '
        'Label250
        '
        Me.Label250.AutoSize = True
        Me.Label250.Location = New System.Drawing.Point(52, 274)
        Me.Label250.Name = "Label250"
        Me.Label250.Size = New System.Drawing.Size(48, 13)
        Me.Label250.TabIndex = 121
        Me.Label250.Text = "Finalized"
        '
        'llbEISInProgress
        '
        Me.llbEISInProgress.AutoSize = True
        Me.llbEISInProgress.Location = New System.Drawing.Point(312, 223)
        Me.llbEISInProgress.Name = "llbEISInProgress"
        Me.llbEISInProgress.Size = New System.Drawing.Size(30, 13)
        Me.llbEISInProgress.TabIndex = 117
        Me.llbEISInProgress.TabStop = True
        Me.llbEISInProgress.Text = "View"
        '
        'txtEISInProgress
        '
        Me.txtEISInProgress.Location = New System.Drawing.Point(200, 219)
        Me.txtEISInProgress.Name = "txtEISInProgress"
        Me.txtEISInProgress.ReadOnly = True
        Me.txtEISInProgress.Size = New System.Drawing.Size(100, 20)
        Me.txtEISInProgress.TabIndex = 116
        '
        'Label246
        '
        Me.Label246.AutoSize = True
        Me.Label246.Location = New System.Drawing.Point(138, 222)
        Me.Label246.Name = "Label246"
        Me.Label246.Size = New System.Drawing.Size(60, 13)
        Me.Label246.TabIndex = 118
        Me.Label246.Text = "In Progress"
        '
        'llbEISSubmitted
        '
        Me.llbEISSubmitted.AutoSize = True
        Me.llbEISSubmitted.Location = New System.Drawing.Point(312, 249)
        Me.llbEISSubmitted.Name = "llbEISSubmitted"
        Me.llbEISSubmitted.Size = New System.Drawing.Size(30, 13)
        Me.llbEISSubmitted.TabIndex = 114
        Me.llbEISSubmitted.TabStop = True
        Me.llbEISSubmitted.Text = "View"
        '
        'txtEISSubmitted
        '
        Me.txtEISSubmitted.Location = New System.Drawing.Point(200, 245)
        Me.txtEISSubmitted.Name = "txtEISSubmitted"
        Me.txtEISSubmitted.ReadOnly = True
        Me.txtEISSubmitted.Size = New System.Drawing.Size(100, 20)
        Me.txtEISSubmitted.TabIndex = 113
        '
        'Label249
        '
        Me.Label249.AutoSize = True
        Me.Label249.Location = New System.Drawing.Point(47, 248)
        Me.Label249.Name = "Label249"
        Me.Label249.Size = New System.Drawing.Size(153, 13)
        Me.Label249.TabIndex = 115
        Me.Label249.Text = "Submitted  (Opted-In/Finalized)"
        '
        'llbEISQABegan
        '
        Me.llbEISQABegan.AutoSize = True
        Me.llbEISQABegan.Location = New System.Drawing.Point(197, 499)
        Me.llbEISQABegan.Name = "llbEISQABegan"
        Me.llbEISQABegan.Size = New System.Drawing.Size(30, 13)
        Me.llbEISQABegan.TabIndex = 111
        Me.llbEISQABegan.TabStop = True
        Me.llbEISQABegan.Text = "View"
        Me.llbEISQABegan.Visible = False
        '
        'txtEISQABegan
        '
        Me.txtEISQABegan.Location = New System.Drawing.Point(85, 495)
        Me.txtEISQABegan.Name = "txtEISQABegan"
        Me.txtEISQABegan.ReadOnly = True
        Me.txtEISQABegan.Size = New System.Drawing.Size(100, 20)
        Me.txtEISQABegan.TabIndex = 110
        Me.txtEISQABegan.Visible = False
        '
        'Label248
        '
        Me.Label248.AutoSize = True
        Me.Label248.Location = New System.Drawing.Point(21, 498)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(56, 13)
        Me.Label248.TabIndex = 112
        Me.Label248.Text = "QA Began"
        Me.Label248.Visible = False
        '
        'llbEISSubmittedToEPA
        '
        Me.llbEISSubmittedToEPA.AutoSize = True
        Me.llbEISSubmittedToEPA.Location = New System.Drawing.Point(186, 427)
        Me.llbEISSubmittedToEPA.Name = "llbEISSubmittedToEPA"
        Me.llbEISSubmittedToEPA.Size = New System.Drawing.Size(30, 13)
        Me.llbEISSubmittedToEPA.TabIndex = 108
        Me.llbEISSubmittedToEPA.TabStop = True
        Me.llbEISSubmittedToEPA.Text = "View"
        '
        'txtEISSubmittedToEPA
        '
        Me.txtEISSubmittedToEPA.Location = New System.Drawing.Point(107, 424)
        Me.txtEISSubmittedToEPA.Name = "txtEISSubmittedToEPA"
        Me.txtEISSubmittedToEPA.ReadOnly = True
        Me.txtEISSubmittedToEPA.Size = New System.Drawing.Size(69, 20)
        Me.txtEISSubmittedToEPA.TabIndex = 107
        '
        'Label247
        '
        Me.Label247.AutoSize = True
        Me.Label247.Location = New System.Drawing.Point(22, 427)
        Me.Label247.Name = "Label247"
        Me.Label247.Size = New System.Drawing.Size(78, 13)
        Me.Label247.TabIndex = 109
        Me.Label247.Text = "EPA Submitted"
        '
        'txtSelectedEISStatYear
        '
        Me.txtSelectedEISStatYear.Location = New System.Drawing.Point(325, 6)
        Me.txtSelectedEISStatYear.Name = "txtSelectedEISStatYear"
        Me.txtSelectedEISStatYear.ReadOnly = True
        Me.txtSelectedEISStatYear.Size = New System.Drawing.Size(65, 20)
        Me.txtSelectedEISStatYear.TabIndex = 2
        '
        'llbEISOptedOut
        '
        Me.llbEISOptedOut.AutoSize = True
        Me.llbEISOptedOut.Location = New System.Drawing.Point(270, 169)
        Me.llbEISOptedOut.Name = "llbEISOptedOut"
        Me.llbEISOptedOut.Size = New System.Drawing.Size(30, 13)
        Me.llbEISOptedOut.TabIndex = 67
        Me.llbEISOptedOut.TabStop = True
        Me.llbEISOptedOut.Text = "View"
        '
        'txtEISOptedOut
        '
        Me.txtEISOptedOut.Location = New System.Drawing.Point(158, 165)
        Me.txtEISOptedOut.Name = "txtEISOptedOut"
        Me.txtEISOptedOut.ReadOnly = True
        Me.txtEISOptedOut.Size = New System.Drawing.Size(100, 20)
        Me.txtEISOptedOut.TabIndex = 66
        '
        'Label242
        '
        Me.Label242.AutoSize = True
        Me.Label242.Location = New System.Drawing.Point(94, 168)
        Me.Label242.Name = "Label242"
        Me.Label242.Size = New System.Drawing.Size(56, 13)
        Me.Label242.TabIndex = 86
        Me.Label242.Text = "Opted-Out"
        '
        'llbEISOptedIn
        '
        Me.llbEISOptedIn.AutoSize = True
        Me.llbEISOptedIn.Location = New System.Drawing.Point(270, 193)
        Me.llbEISOptedIn.Name = "llbEISOptedIn"
        Me.llbEISOptedIn.Size = New System.Drawing.Size(30, 13)
        Me.llbEISOptedIn.TabIndex = 64
        Me.llbEISOptedIn.TabStop = True
        Me.llbEISOptedIn.Text = "View"
        '
        'txtEISOptedIn
        '
        Me.txtEISOptedIn.Location = New System.Drawing.Point(158, 189)
        Me.txtEISOptedIn.Name = "txtEISOptedIn"
        Me.txtEISOptedIn.ReadOnly = True
        Me.txtEISOptedIn.Size = New System.Drawing.Size(100, 20)
        Me.txtEISOptedIn.TabIndex = 63
        '
        'Label243
        '
        Me.Label243.AutoSize = True
        Me.Label243.Location = New System.Drawing.Point(102, 192)
        Me.Label243.Name = "Label243"
        Me.Label243.Size = New System.Drawing.Size(48, 13)
        Me.Label243.TabIndex = 85
        Me.Label243.Text = "Opted-In"
        '
        'llbEISUnenrolled
        '
        Me.llbEISUnenrolled.AutoSize = True
        Me.llbEISUnenrolled.Location = New System.Drawing.Point(228, 87)
        Me.llbEISUnenrolled.Name = "llbEISUnenrolled"
        Me.llbEISUnenrolled.Size = New System.Drawing.Size(30, 13)
        Me.llbEISUnenrolled.TabIndex = 61
        Me.llbEISUnenrolled.TabStop = True
        Me.llbEISUnenrolled.Text = "View"
        '
        'txtEISUnenrolled
        '
        Me.txtEISUnenrolled.Location = New System.Drawing.Point(116, 87)
        Me.txtEISUnenrolled.Name = "txtEISUnenrolled"
        Me.txtEISUnenrolled.ReadOnly = True
        Me.txtEISUnenrolled.Size = New System.Drawing.Size(100, 20)
        Me.txtEISUnenrolled.TabIndex = 60
        '
        'Label244
        '
        Me.Label244.AutoSize = True
        Me.Label244.Location = New System.Drawing.Point(47, 87)
        Me.Label244.Name = "Label244"
        Me.Label244.Size = New System.Drawing.Size(61, 13)
        Me.Label244.TabIndex = 84
        Me.Label244.Text = "Unenrolled "
        '
        'llbEISEIUniverse
        '
        Me.llbEISEIUniverse.AutoSize = True
        Me.llbEISEIUniverse.Location = New System.Drawing.Point(228, 38)
        Me.llbEISEIUniverse.Name = "llbEISEIUniverse"
        Me.llbEISEIUniverse.Size = New System.Drawing.Size(30, 13)
        Me.llbEISEIUniverse.TabIndex = 56
        Me.llbEISEIUniverse.TabStop = True
        Me.llbEISEIUniverse.Text = "View"
        '
        'txtEISActiveEIUniverse
        '
        Me.txtEISActiveEIUniverse.Location = New System.Drawing.Point(116, 35)
        Me.txtEISActiveEIUniverse.Name = "txtEISActiveEIUniverse"
        Me.txtEISActiveEIUniverse.ReadOnly = True
        Me.txtEISActiveEIUniverse.Size = New System.Drawing.Size(100, 20)
        Me.txtEISActiveEIUniverse.TabIndex = 54
        '
        'Label245
        '
        Me.Label245.AutoSize = True
        Me.Label245.Location = New System.Drawing.Point(13, 38)
        Me.Label245.Name = "Label245"
        Me.Label245.Size = New System.Drawing.Size(95, 13)
        Me.Label245.TabIndex = 83
        Me.Label245.Text = "Active EI Universe"
        '
        'llbEISMailOutTotal
        '
        Me.llbEISMailOutTotal.AutoSize = True
        Me.llbEISMailOutTotal.Location = New System.Drawing.Point(228, 64)
        Me.llbEISMailOutTotal.Name = "llbEISMailOutTotal"
        Me.llbEISMailOutTotal.Size = New System.Drawing.Size(30, 13)
        Me.llbEISMailOutTotal.TabIndex = 53
        Me.llbEISMailOutTotal.TabStop = True
        Me.llbEISMailOutTotal.Text = "View"
        '
        'llbEISEnrolled
        '
        Me.llbEISEnrolled.AutoSize = True
        Me.llbEISEnrolled.Location = New System.Drawing.Point(228, 117)
        Me.llbEISEnrolled.Name = "llbEISEnrolled"
        Me.llbEISEnrolled.Size = New System.Drawing.Size(30, 13)
        Me.llbEISEnrolled.TabIndex = 58
        Me.llbEISEnrolled.TabStop = True
        Me.llbEISEnrolled.Text = "View"
        '
        'txtEISEnrolled
        '
        Me.txtEISEnrolled.Location = New System.Drawing.Point(116, 114)
        Me.txtEISEnrolled.Name = "txtEISEnrolled"
        Me.txtEISEnrolled.ReadOnly = True
        Me.txtEISEnrolled.Size = New System.Drawing.Size(100, 20)
        Me.txtEISEnrolled.TabIndex = 57
        '
        'Label252
        '
        Me.Label252.AutoSize = True
        Me.Label252.Location = New System.Drawing.Point(63, 117)
        Me.Label252.Name = "Label252"
        Me.Label252.Size = New System.Drawing.Size(45, 13)
        Me.Label252.TabIndex = 59
        Me.Label252.Text = "Enrolled"
        '
        'txtEISMailout
        '
        Me.txtEISMailout.Location = New System.Drawing.Point(116, 61)
        Me.txtEISMailout.Name = "txtEISMailout"
        Me.txtEISMailout.ReadOnly = True
        Me.txtEISMailout.Size = New System.Drawing.Size(100, 20)
        Me.txtEISMailout.TabIndex = 52
        '
        'Label253
        '
        Me.Label253.AutoSize = True
        Me.Label253.Location = New System.Drawing.Point(37, 64)
        Me.Label253.Name = "Label253"
        Me.Label253.Size = New System.Drawing.Size(71, 13)
        Me.Label253.TabIndex = 55
        Me.Label253.Text = "Mailout Total:"
        '
        'Label241
        '
        Me.Label241.AutoSize = True
        Me.Label241.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label241.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label241.Location = New System.Drawing.Point(5, 5)
        Me.Label241.Name = "Label241"
        Me.Label241.Size = New System.Drawing.Size(318, 22)
        Me.Label241.TabIndex = 1
        Me.Label241.Text = "Emission Inventory Summary of Year "
        '
        'TPEISStatMailout
        '
        Me.TPEISStatMailout.Controls.Add(Me.btnAddtoEISMailout)
        Me.TPEISStatMailout.Controls.Add(Me.llbSearchForFacility)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutCreateDate)
        Me.TPEISStatMailout.Controls.Add(Me.Label271)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutUpdateDate)
        Me.TPEISStatMailout.Controls.Add(Me.Label270)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutUpdateUser)
        Me.TPEISStatMailout.Controls.Add(Me.Label269)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutComments)
        Me.TPEISStatMailout.Controls.Add(Me.Label268)
        Me.TPEISStatMailout.Controls.Add(Me.txtSelectedEISMailout)
        Me.TPEISStatMailout.Controls.Add(Me.Label267)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutAddress1)
        Me.TPEISStatMailout.Controls.Add(Me.Label255)
        Me.TPEISStatMailout.Controls.Add(Me.btnEISStatsDelete)
        Me.TPEISStatMailout.Controls.Add(Me.btnSaveEISStatMailout)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutEmailAddress)
        Me.TPEISStatMailout.Controls.Add(Me.Label256)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutZipCode)
        Me.TPEISStatMailout.Controls.Add(Me.Label257)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutState)
        Me.TPEISStatMailout.Controls.Add(Me.Label258)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutCity)
        Me.TPEISStatMailout.Controls.Add(Me.Label259)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutAddress2)
        Me.TPEISStatMailout.Controls.Add(Me.Label260)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutCompanyName)
        Me.TPEISStatMailout.Controls.Add(Me.Label261)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutLastName)
        Me.TPEISStatMailout.Controls.Add(Me.Label262)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutFirstName)
        Me.TPEISStatMailout.Controls.Add(Me.Label263)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutPrefix)
        Me.TPEISStatMailout.Controls.Add(Me.Label264)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutFacilityName)
        Me.TPEISStatMailout.Controls.Add(Me.Label265)
        Me.TPEISStatMailout.Controls.Add(Me.txtEISStatsMailoutAIRSNumber)
        Me.TPEISStatMailout.Controls.Add(Me.Label266)
        Me.TPEISStatMailout.Location = New System.Drawing.Point(4, 22)
        Me.TPEISStatMailout.Name = "TPEISStatMailout"
        Me.TPEISStatMailout.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEISStatMailout.Size = New System.Drawing.Size(438, 570)
        Me.TPEISStatMailout.TabIndex = 1
        Me.TPEISStatMailout.Text = "Mailout"
        Me.TPEISStatMailout.UseVisualStyleBackColor = True
        '
        'btnAddtoEISMailout
        '
        Me.btnAddtoEISMailout.AutoSize = True
        Me.btnAddtoEISMailout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddtoEISMailout.Location = New System.Drawing.Point(302, 9)
        Me.btnAddtoEISMailout.Name = "btnAddtoEISMailout"
        Me.btnAddtoEISMailout.Size = New System.Drawing.Size(130, 23)
        Me.btnAddtoEISMailout.TabIndex = 71
        Me.btnAddtoEISMailout.Text = "Add selected to Maillout"
        Me.btnAddtoEISMailout.UseVisualStyleBackColor = True
        Me.btnAddtoEISMailout.Visible = False
        '
        'llbSearchForFacility
        '
        Me.llbSearchForFacility.AutoSize = True
        Me.llbSearchForFacility.Location = New System.Drawing.Point(225, 52)
        Me.llbSearchForFacility.Name = "llbSearchForFacility"
        Me.llbSearchForFacility.Size = New System.Drawing.Size(91, 13)
        Me.llbSearchForFacility.TabIndex = 70
        Me.llbSearchForFacility.TabStop = True
        Me.llbSearchForFacility.Text = "Search for Facility"
        '
        'txtEISStatsMailoutCreateDate
        '
        Me.txtEISStatsMailoutCreateDate.Location = New System.Drawing.Point(106, 463)
        Me.txtEISStatsMailoutCreateDate.Name = "txtEISStatsMailoutCreateDate"
        Me.txtEISStatsMailoutCreateDate.ReadOnly = True
        Me.txtEISStatsMailoutCreateDate.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutCreateDate.TabIndex = 68
        '
        'Label271
        '
        Me.Label271.AutoSize = True
        Me.Label271.Location = New System.Drawing.Point(6, 467)
        Me.Label271.Name = "Label271"
        Me.Label271.Size = New System.Drawing.Size(67, 13)
        Me.Label271.TabIndex = 69
        Me.Label271.Text = "Create Date:"
        '
        'txtEISStatsMailoutUpdateDate
        '
        Me.txtEISStatsMailoutUpdateDate.Location = New System.Drawing.Point(106, 437)
        Me.txtEISStatsMailoutUpdateDate.Name = "txtEISStatsMailoutUpdateDate"
        Me.txtEISStatsMailoutUpdateDate.ReadOnly = True
        Me.txtEISStatsMailoutUpdateDate.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutUpdateDate.TabIndex = 66
        '
        'Label270
        '
        Me.Label270.AutoSize = True
        Me.Label270.Location = New System.Drawing.Point(6, 441)
        Me.Label270.Name = "Label270"
        Me.Label270.Size = New System.Drawing.Size(71, 13)
        Me.Label270.TabIndex = 67
        Me.Label270.Text = "Update Time:"
        '
        'txtEISStatsMailoutUpdateUser
        '
        Me.txtEISStatsMailoutUpdateUser.Location = New System.Drawing.Point(106, 411)
        Me.txtEISStatsMailoutUpdateUser.Name = "txtEISStatsMailoutUpdateUser"
        Me.txtEISStatsMailoutUpdateUser.ReadOnly = True
        Me.txtEISStatsMailoutUpdateUser.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutUpdateUser.TabIndex = 64
        '
        'Label269
        '
        Me.Label269.AutoSize = True
        Me.Label269.Location = New System.Drawing.Point(6, 415)
        Me.Label269.Name = "Label269"
        Me.Label269.Size = New System.Drawing.Size(70, 13)
        Me.Label269.TabIndex = 65
        Me.Label269.Text = "Update User:"
        '
        'txtEISStatsMailoutComments
        '
        Me.txtEISStatsMailoutComments.Location = New System.Drawing.Point(106, 361)
        Me.txtEISStatsMailoutComments.Multiline = True
        Me.txtEISStatsMailoutComments.Name = "txtEISStatsMailoutComments"
        Me.txtEISStatsMailoutComments.Size = New System.Drawing.Size(221, 44)
        Me.txtEISStatsMailoutComments.TabIndex = 62
        '
        'Label268
        '
        Me.Label268.AutoSize = True
        Me.Label268.Location = New System.Drawing.Point(6, 361)
        Me.Label268.Name = "Label268"
        Me.Label268.Size = New System.Drawing.Size(59, 13)
        Me.Label268.TabIndex = 63
        Me.Label268.Text = "Comments:"
        '
        'txtSelectedEISMailout
        '
        Me.txtSelectedEISMailout.Location = New System.Drawing.Point(188, 12)
        Me.txtSelectedEISMailout.Name = "txtSelectedEISMailout"
        Me.txtSelectedEISMailout.ReadOnly = True
        Me.txtSelectedEISMailout.Size = New System.Drawing.Size(65, 20)
        Me.txtSelectedEISMailout.TabIndex = 61
        '
        'Label267
        '
        Me.Label267.AutoSize = True
        Me.Label267.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label267.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label267.Location = New System.Drawing.Point(11, 10)
        Me.Label267.Name = "Label267"
        Me.Label267.Size = New System.Drawing.Size(179, 22)
        Me.Label267.TabIndex = 60
        Me.Label267.Text = "Mailout list for Year "
        '
        'txtEISStatsMailoutAddress1
        '
        Me.txtEISStatsMailoutAddress1.Location = New System.Drawing.Point(106, 205)
        Me.txtEISStatsMailoutAddress1.Name = "txtEISStatsMailoutAddress1"
        Me.txtEISStatsMailoutAddress1.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutAddress1.TabIndex = 42
        '
        'Label255
        '
        Me.Label255.AutoSize = True
        Me.Label255.Location = New System.Drawing.Point(6, 209)
        Me.Label255.Name = "Label255"
        Me.Label255.Size = New System.Drawing.Size(104, 13)
        Me.Label255.TabIndex = 53
        Me.Label255.Text = "Company Address1: "
        '
        'btnEISStatsDelete
        '
        Me.btnEISStatsDelete.Location = New System.Drawing.Point(241, 500)
        Me.btnEISStatsDelete.Name = "btnEISStatsDelete"
        Me.btnEISStatsDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnEISStatsDelete.TabIndex = 52
        Me.btnEISStatsDelete.Text = "Delete"
        Me.btnEISStatsDelete.UseVisualStyleBackColor = True
        Me.btnEISStatsDelete.Visible = False
        '
        'btnSaveEISStatMailout
        '
        Me.btnSaveEISStatMailout.AutoSize = True
        Me.btnSaveEISStatMailout.Location = New System.Drawing.Point(103, 500)
        Me.btnSaveEISStatMailout.Name = "btnSaveEISStatMailout"
        Me.btnSaveEISStatMailout.Size = New System.Drawing.Size(115, 23)
        Me.btnSaveEISStatMailout.TabIndex = 51
        Me.btnSaveEISStatMailout.Text = "Update Mailout Data"
        Me.btnSaveEISStatMailout.UseVisualStyleBackColor = True
        Me.btnSaveEISStatMailout.Visible = False
        '
        'txtEISStatsMailoutEmailAddress
        '
        Me.txtEISStatsMailoutEmailAddress.Location = New System.Drawing.Point(106, 335)
        Me.txtEISStatsMailoutEmailAddress.Name = "txtEISStatsMailoutEmailAddress"
        Me.txtEISStatsMailoutEmailAddress.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutEmailAddress.TabIndex = 49
        '
        'Label256
        '
        Me.Label256.AutoSize = True
        Me.Label256.Location = New System.Drawing.Point(6, 339)
        Me.Label256.Name = "Label256"
        Me.Label256.Size = New System.Drawing.Size(79, 13)
        Me.Label256.TabIndex = 58
        Me.Label256.Text = "Email Address: "
        '
        'txtEISStatsMailoutZipCode
        '
        Me.txtEISStatsMailoutZipCode.Location = New System.Drawing.Point(106, 309)
        Me.txtEISStatsMailoutZipCode.Name = "txtEISStatsMailoutZipCode"
        Me.txtEISStatsMailoutZipCode.Size = New System.Drawing.Size(147, 20)
        Me.txtEISStatsMailoutZipCode.TabIndex = 48
        '
        'Label257
        '
        Me.Label257.AutoSize = True
        Me.Label257.Location = New System.Drawing.Point(6, 313)
        Me.Label257.Name = "Label257"
        Me.Label257.Size = New System.Drawing.Size(56, 13)
        Me.Label257.TabIndex = 57
        Me.Label257.Text = "Zip Code: "
        '
        'txtEISStatsMailoutState
        '
        Me.txtEISStatsMailoutState.Location = New System.Drawing.Point(106, 283)
        Me.txtEISStatsMailoutState.Name = "txtEISStatsMailoutState"
        Me.txtEISStatsMailoutState.Size = New System.Drawing.Size(147, 20)
        Me.txtEISStatsMailoutState.TabIndex = 46
        '
        'Label258
        '
        Me.Label258.AutoSize = True
        Me.Label258.Location = New System.Drawing.Point(6, 287)
        Me.Label258.Name = "Label258"
        Me.Label258.Size = New System.Drawing.Size(82, 13)
        Me.Label258.TabIndex = 56
        Me.Label258.Text = "Company State:"
        '
        'txtEISStatsMailoutCity
        '
        Me.txtEISStatsMailoutCity.Location = New System.Drawing.Point(106, 257)
        Me.txtEISStatsMailoutCity.Name = "txtEISStatsMailoutCity"
        Me.txtEISStatsMailoutCity.Size = New System.Drawing.Size(147, 20)
        Me.txtEISStatsMailoutCity.TabIndex = 44
        '
        'Label259
        '
        Me.Label259.AutoSize = True
        Me.Label259.Location = New System.Drawing.Point(6, 261)
        Me.Label259.Name = "Label259"
        Me.Label259.Size = New System.Drawing.Size(74, 13)
        Me.Label259.TabIndex = 55
        Me.Label259.Text = "Company City:"
        '
        'txtEISStatsMailoutAddress2
        '
        Me.txtEISStatsMailoutAddress2.Location = New System.Drawing.Point(106, 231)
        Me.txtEISStatsMailoutAddress2.Name = "txtEISStatsMailoutAddress2"
        Me.txtEISStatsMailoutAddress2.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutAddress2.TabIndex = 43
        '
        'Label260
        '
        Me.Label260.AutoSize = True
        Me.Label260.Location = New System.Drawing.Point(6, 235)
        Me.Label260.Name = "Label260"
        Me.Label260.Size = New System.Drawing.Size(104, 13)
        Me.Label260.TabIndex = 54
        Me.Label260.Text = "Company Address2: "
        '
        'txtEISStatsMailoutCompanyName
        '
        Me.txtEISStatsMailoutCompanyName.Location = New System.Drawing.Point(106, 179)
        Me.txtEISStatsMailoutCompanyName.Name = "txtEISStatsMailoutCompanyName"
        Me.txtEISStatsMailoutCompanyName.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutCompanyName.TabIndex = 40
        '
        'Label261
        '
        Me.Label261.AutoSize = True
        Me.Label261.Location = New System.Drawing.Point(6, 183)
        Me.Label261.Name = "Label261"
        Me.Label261.Size = New System.Drawing.Size(88, 13)
        Me.Label261.TabIndex = 50
        Me.Label261.Text = "Company Name: "
        '
        'txtEISStatsMailoutLastName
        '
        Me.txtEISStatsMailoutLastName.Location = New System.Drawing.Point(106, 153)
        Me.txtEISStatsMailoutLastName.Name = "txtEISStatsMailoutLastName"
        Me.txtEISStatsMailoutLastName.Size = New System.Drawing.Size(147, 20)
        Me.txtEISStatsMailoutLastName.TabIndex = 39
        '
        'Label262
        '
        Me.Label262.AutoSize = True
        Me.Label262.Location = New System.Drawing.Point(6, 157)
        Me.Label262.Name = "Label262"
        Me.Label262.Size = New System.Drawing.Size(64, 13)
        Me.Label262.TabIndex = 47
        Me.Label262.Text = "Last Name: "
        '
        'txtEISStatsMailoutFirstName
        '
        Me.txtEISStatsMailoutFirstName.Location = New System.Drawing.Point(106, 127)
        Me.txtEISStatsMailoutFirstName.Name = "txtEISStatsMailoutFirstName"
        Me.txtEISStatsMailoutFirstName.Size = New System.Drawing.Size(147, 20)
        Me.txtEISStatsMailoutFirstName.TabIndex = 37
        '
        'Label263
        '
        Me.Label263.AutoSize = True
        Me.Label263.Location = New System.Drawing.Point(6, 131)
        Me.Label263.Name = "Label263"
        Me.Label263.Size = New System.Drawing.Size(63, 13)
        Me.Label263.TabIndex = 45
        Me.Label263.Text = "First Name: "
        '
        'txtEISStatsMailoutPrefix
        '
        Me.txtEISStatsMailoutPrefix.Location = New System.Drawing.Point(106, 101)
        Me.txtEISStatsMailoutPrefix.Name = "txtEISStatsMailoutPrefix"
        Me.txtEISStatsMailoutPrefix.Size = New System.Drawing.Size(110, 20)
        Me.txtEISStatsMailoutPrefix.TabIndex = 36
        '
        'Label264
        '
        Me.Label264.AutoSize = True
        Me.Label264.Location = New System.Drawing.Point(6, 105)
        Me.Label264.Name = "Label264"
        Me.Label264.Size = New System.Drawing.Size(39, 13)
        Me.Label264.TabIndex = 41
        Me.Label264.Text = "Prefix: "
        '
        'txtEISStatsMailoutFacilityName
        '
        Me.txtEISStatsMailoutFacilityName.Location = New System.Drawing.Point(106, 75)
        Me.txtEISStatsMailoutFacilityName.Name = "txtEISStatsMailoutFacilityName"
        Me.txtEISStatsMailoutFacilityName.Size = New System.Drawing.Size(221, 20)
        Me.txtEISStatsMailoutFacilityName.TabIndex = 35
        '
        'Label265
        '
        Me.Label265.AutoSize = True
        Me.Label265.Location = New System.Drawing.Point(6, 75)
        Me.Label265.Name = "Label265"
        Me.Label265.Size = New System.Drawing.Size(76, 13)
        Me.Label265.TabIndex = 38
        Me.Label265.Text = "Facility Name: "
        '
        'txtEISStatsMailoutAIRSNumber
        '
        Me.txtEISStatsMailoutAIRSNumber.Location = New System.Drawing.Point(106, 49)
        Me.txtEISStatsMailoutAIRSNumber.Name = "txtEISStatsMailoutAIRSNumber"
        Me.txtEISStatsMailoutAIRSNumber.Size = New System.Drawing.Size(110, 20)
        Me.txtEISStatsMailoutAIRSNumber.TabIndex = 33
        '
        'Label266
        '
        Me.Label266.AutoSize = True
        Me.Label266.Location = New System.Drawing.Point(6, 53)
        Me.Label266.Name = "Label266"
        Me.Label266.Size = New System.Drawing.Size(50, 13)
        Me.Label266.TabIndex = 34
        Me.Label266.Text = "Airs No.: "
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.btnViewEISStats)
        Me.Panel21.Controls.Add(Me.Label74)
        Me.Panel21.Controls.Add(Me.cboEISStatisticsYear)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel21.Location = New System.Drawing.Point(0, 0)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(446, 44)
        Me.Panel21.TabIndex = 2
        '
        'btnViewEISStats
        '
        Me.btnViewEISStats.AutoSize = True
        Me.btnViewEISStats.Location = New System.Drawing.Point(195, 10)
        Me.btnViewEISStats.Name = "btnViewEISStats"
        Me.btnViewEISStats.Size = New System.Drawing.Size(112, 23)
        Me.btnViewEISStats.TabIndex = 9
        Me.btnViewEISStats.Text = "View Selected Data"
        Me.btnViewEISStats.UseVisualStyleBackColor = True
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(9, 15)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(71, 13)
        Me.Label74.TabIndex = 10
        Me.Label74.Text = "Select a Year"
        '
        'cboEISStatisticsYear
        '
        Me.cboEISStatisticsYear.FormattingEnabled = True
        Me.cboEISStatisticsYear.Location = New System.Drawing.Point(86, 12)
        Me.cboEISStatisticsYear.Name = "cboEISStatisticsYear"
        Me.cboEISStatisticsYear.Size = New System.Drawing.Size(97, 21)
        Me.cboEISStatisticsYear.TabIndex = 8
        '
        'IAIP_EIS_Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 713)
        Me.Controls.Add(Me.TCDMUTools)
        Me.Controls.Add(Me.TSDMUStaffTools)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIP_EIS_Log"
        Me.Text = "Emission Inventory System Log"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TSDMUStaffTools.ResumeLayout(False)
        Me.TSDMUStaffTools.PerformLayout()
        Me.TCDMUTools.ResumeLayout(False)
        Me.TPEISLog.ResumeLayout(False)
        Me.TabControl6.ResumeLayout(False)
        Me.TPQAProcess.ResumeLayout(False)
        Me.TPQAProcess.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.TPEISStatistics.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        CType(Me.dgvEISStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.TCEISStats.ResumeLayout(False)
        Me.TPEISStatSummary.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        Me.Panel22.PerformLayout()
        Me.TPEISStatMailout.ResumeLayout(False)
        Me.TPEISStatMailout.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSDMUStaffTools As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents TCDMUTools As System.Windows.Forms.TabControl
    Friend WithEvents TPEISLog As System.Windows.Forms.TabPage
    Friend WithEvents TabControl6 As System.Windows.Forms.TabControl
    Friend WithEvents TPQAProcess As System.Windows.Forms.TabPage
    Friend WithEvents chbPointErrors As System.Windows.Forms.CheckBox
    Friend WithEvents chbFIErrors As System.Windows.Forms.CheckBox
    Friend WithEvents txtPointTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label291 As System.Windows.Forms.Label
    Friend WithEvents txtFITrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label290 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateQAData As System.Windows.Forms.Button
    Friend WithEvents dtpQAStatus As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label288 As System.Windows.Forms.Label
    Friend WithEvents cboEISQAStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label287 As System.Windows.Forms.Label
    Friend WithEvents dtpQACompleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label286 As System.Windows.Forms.Label
    Friend WithEvents dtpQAPassed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label285 As System.Windows.Forms.Label
    Friend WithEvents dtpQAStarted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label284 As System.Windows.Forms.Label
    Friend WithEvents cboEISQAStaff As System.Windows.Forms.ComboBox
    Friend WithEvents Label283 As System.Windows.Forms.Label
    Friend WithEvents txtQAComments As System.Windows.Forms.TextBox
    Friend WithEvents Label282 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents rdbEILogActiveNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEILogActiveYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label234 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEILogPrePopYear As System.Windows.Forms.TextBox
    Friend WithEvents Label233 As System.Windows.Forms.Label
    Friend WithEvents txtEILogStatusMgt As System.Windows.Forms.TextBox
    Friend WithEvents dtpEILogDateEnrolled As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label232 As System.Windows.Forms.Label
    Friend WithEvents dtpEILogStatusDateSubmit As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label229 As System.Windows.Forms.Label
    Friend WithEvents cboEILogYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label230 As System.Windows.Forms.Label
    Friend WithEvents Label231 As System.Windows.Forms.Label
    Friend WithEvents btnReloadFSData As System.Windows.Forms.Button
    Friend WithEvents txtEILogSelectedAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnEILogAddNewFacility As System.Windows.Forms.Button
    Friend WithEvents btnEILogUpdate As System.Windows.Forms.Button
    Friend WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents txtEILogUpdatedTime As System.Windows.Forms.TextBox
    Friend WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents txtEILogUpdatedBy As System.Windows.Forms.TextBox
    Friend WithEvents txtEILogComments As System.Windows.Forms.TextBox
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents cboEILogAccessCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents cboEILogStatusCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents rdbEILogOpOutNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEILogOpOutYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents rdbEILogMailoutNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEILogMailoutYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents rdbEILogEnrolledNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEILogEnrolledYes As System.Windows.Forms.RadioButton
    Friend WithEvents txtEILogSelectedYear As System.Windows.Forms.TextBox
    Friend WithEvents txtEILogFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents mtbEILogAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents TPEISStatistics As System.Windows.Forms.TabPage
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents dgvEISStats As System.Windows.Forms.DataGridView
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents lblEISCount As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsCount As System.Windows.Forms.TextBox
    Friend WithEvents btnEISSummaryToExcel As System.Windows.Forms.Button
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents TCEISStats As System.Windows.Forms.TabControl
    Friend WithEvents TPEISStatSummary As System.Windows.Forms.TabPage
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents btnClearInactiveData As System.Windows.Forms.Button
    Friend WithEvents btnEISComplete As System.Windows.Forms.Button
    Friend WithEvents Label289 As System.Windows.Forms.Label
    Friend WithEvents llbEISStatsOptedOutSubmittedToEPA As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISOpOutToEPA As System.Windows.Forms.TextBox
    Friend WithEvents llbEISStatsOptedOutBegan As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISOpOutBegan As System.Windows.Forms.TextBox
    Friend WithEvents llbEISStatsOptedOutToDo As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISOpOutToDo As System.Windows.Forms.TextBox
    Friend WithEvents llbEISStatsSubmittedToDo As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISSubmittedToDo As System.Windows.Forms.TextBox
    Friend WithEvents llbEISStatsSubmittedBegan As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISSubmittedBegan As System.Windows.Forms.TextBox
    Friend WithEvents Label281 As System.Windows.Forms.Label
    Friend WithEvents Label280 As System.Windows.Forms.Label
    Friend WithEvents Label279 As System.Windows.Forms.Label
    Friend WithEvents Label278 As System.Windows.Forms.Label
    Friend WithEvents Label276 As System.Windows.Forms.Label
    Friend WithEvents Label277 As System.Windows.Forms.Label
    Friend WithEvents btnCloseOutEIS As System.Windows.Forms.Button
    Friend WithEvents btnEISBeginQA As System.Windows.Forms.Button
    Friend WithEvents llbEISNoActivity As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISNoActivity As System.Windows.Forms.TextBox
    Friend WithEvents Label251 As System.Windows.Forms.Label
    Friend WithEvents llbEISFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISFinalized As System.Windows.Forms.TextBox
    Friend WithEvents Label250 As System.Windows.Forms.Label
    Friend WithEvents llbEISInProgress As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISInProgress As System.Windows.Forms.TextBox
    Friend WithEvents Label246 As System.Windows.Forms.Label
    Friend WithEvents llbEISSubmitted As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISSubmitted As System.Windows.Forms.TextBox
    Friend WithEvents Label249 As System.Windows.Forms.Label
    Friend WithEvents llbEISQABegan As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISQABegan As System.Windows.Forms.TextBox
    Friend WithEvents Label248 As System.Windows.Forms.Label
    Friend WithEvents llbEISSubmittedToEPA As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISSubmittedToEPA As System.Windows.Forms.TextBox
    Friend WithEvents Label247 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedEISStatYear As System.Windows.Forms.TextBox
    Friend WithEvents llbEISOptedOut As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISOptedOut As System.Windows.Forms.TextBox
    Friend WithEvents Label242 As System.Windows.Forms.Label
    Friend WithEvents llbEISOptedIn As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISOptedIn As System.Windows.Forms.TextBox
    Friend WithEvents Label243 As System.Windows.Forms.Label
    Friend WithEvents llbEISUnenrolled As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISUnenrolled As System.Windows.Forms.TextBox
    Friend WithEvents Label244 As System.Windows.Forms.Label
    Friend WithEvents llbEISEIUniverse As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISActiveEIUniverse As System.Windows.Forms.TextBox
    Friend WithEvents Label245 As System.Windows.Forms.Label
    Friend WithEvents llbEISMailOutTotal As System.Windows.Forms.LinkLabel
    Friend WithEvents llbEISEnrolled As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISEnrolled As System.Windows.Forms.TextBox
    Friend WithEvents Label252 As System.Windows.Forms.Label
    Friend WithEvents txtEISMailout As System.Windows.Forms.TextBox
    Friend WithEvents Label253 As System.Windows.Forms.Label
    Friend WithEvents Label241 As System.Windows.Forms.Label
    Friend WithEvents TPEISStatMailout As System.Windows.Forms.TabPage
    Friend WithEvents btnAddtoEISMailout As System.Windows.Forms.Button
    Friend WithEvents llbSearchForFacility As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEISStatsMailoutCreateDate As System.Windows.Forms.TextBox
    Friend WithEvents Label271 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutUpdateDate As System.Windows.Forms.TextBox
    Friend WithEvents Label270 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutUpdateUser As System.Windows.Forms.TextBox
    Friend WithEvents Label269 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutComments As System.Windows.Forms.TextBox
    Friend WithEvents Label268 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedEISMailout As System.Windows.Forms.TextBox
    Friend WithEvents Label267 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label255 As System.Windows.Forms.Label
    Friend WithEvents btnEISStatsDelete As System.Windows.Forms.Button
    Friend WithEvents btnSaveEISStatMailout As System.Windows.Forms.Button
    Friend WithEvents txtEISStatsMailoutEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label256 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutZipCode As System.Windows.Forms.TextBox
    Friend WithEvents Label257 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutState As System.Windows.Forms.TextBox
    Friend WithEvents Label258 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutCity As System.Windows.Forms.TextBox
    Friend WithEvents Label259 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents Label260 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label261 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label262 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label263 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label264 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label265 As System.Windows.Forms.Label
    Friend WithEvents txtEISStatsMailoutAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label266 As System.Windows.Forms.Label
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents btnViewEISStats As System.Windows.Forms.Button
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents cboEISStatisticsYear As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoadEISLog As System.Windows.Forms.Button
    Friend WithEvents mtbEISLogAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAllFITrackingNumbers As System.Windows.Forms.TextBox
    Friend WithEvents txtAllPointTrackingNumbers As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAllQAComments As System.Windows.Forms.TextBox
    Friend WithEvents Label274 As System.Windows.Forms.Label
    Friend WithEvents btnCopyAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents txtEISDeadlineComment As System.Windows.Forms.TextBox
    Friend WithEvents dtpEISDeadline As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAllEISDeadlineComment As System.Windows.Forms.TextBox
End Class
