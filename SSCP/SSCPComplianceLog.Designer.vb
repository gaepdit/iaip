<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSCPComplianceLog
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPComplianceLog))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiBack = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.TBWork_EnTry = New System.Windows.Forms.ToolBar
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.tbbSearch = New System.Windows.Forms.ToolBarButton
        Me.tbbExportToExcel = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GBWorkTypes = New System.Windows.Forms.GroupBox
        Me.chbRMPInspections = New System.Windows.Forms.CheckBox
        Me.GBNotifications = New System.Windows.Forms.GroupBox
        Me.clbNotifications = New System.Windows.Forms.CheckedListBox
        Me.chbFCE = New System.Windows.Forms.CheckBox
        Me.chbEnforcement = New System.Windows.Forms.CheckBox
        Me.chbAllWork = New System.Windows.Forms.CheckBox
        Me.chbNotifications = New System.Windows.Forms.CheckBox
        Me.chbPerformanceTests = New System.Windows.Forms.CheckBox
        Me.chbReports = New System.Windows.Forms.CheckBox
        Me.chbInspections = New System.Windows.Forms.CheckBox
        Me.chbACCs = New System.Windows.Forms.CheckBox
        Me.GBEnforcementDates = New System.Windows.Forms.GroupBox
        Me.chbLastModifiedDate = New System.Windows.Forms.CheckBox
        Me.chbFilterDates = New System.Windows.Forms.CheckBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.DTPFilterEnd = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.DTPFilterStart = New System.Windows.Forms.DateTimePicker
        Me.Label20 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.chbMacon = New System.Windows.Forms.CheckBox
        Me.chbAugusta = New System.Windows.Forms.CheckBox
        Me.chbCartersville = New System.Windows.Forms.CheckBox
        Me.chbAtlanta = New System.Windows.Forms.CheckBox
        Me.chbAlbany = New System.Windows.Forms.CheckBox
        Me.chbAthens = New System.Windows.Forms.CheckBox
        Me.chbBrunswick = New System.Windows.Forms.CheckBox
        Me.chbAdministrative = New System.Windows.Forms.CheckBox
        Me.chbAIRToxics = New System.Windows.Forms.CheckBox
        Me.chbChemicalsMineral = New System.Windows.Forms.CheckBox
        Me.chbVOCCombustion = New System.Windows.Forms.CheckBox
        Me.txtWorkCount = New System.Windows.Forms.TextBox
        Me.btnRunFilter = New System.Windows.Forms.Button
        Me.TCComplianceLog = New System.Windows.Forms.TabControl
        Me.TPSelectWork = New System.Windows.Forms.TabPage
        Me.btnOpenSummary = New System.Windows.Forms.Button
        Me.btnUndeleteWork = New System.Windows.Forms.Button
        Me.btnDeleteWork = New System.Windows.Forms.Button
        Me.btnSelectWork = New System.Windows.Forms.Button
        Me.lblWorkType = New System.Windows.Forms.Label
        Me.txtFacilityCounty = New System.Windows.Forms.TextBox
        Me.txtWorkNumber = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFacilityCity = New System.Windows.Forms.TextBox
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTestType = New System.Windows.Forms.TextBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TPStartNewWork = New System.Windows.Forms.TabPage
        Me.pnlOtherEvents = New System.Windows.Forms.Panel
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblOtherNumber = New System.Windows.Forms.Label
        Me.cboEvent = New System.Windows.Forms.ComboBox
        Me.LabEventDescription = New System.Windows.Forms.Label
        Me.DTPDateReceived = New System.Windows.Forms.DateTimePicker
        Me.lblDateField = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rdbPerformanceTest = New System.Windows.Forms.RadioButton
        Me.rdbOther = New System.Windows.Forms.RadioButton
        Me.rdbFCE = New System.Windows.Forms.RadioButton
        Me.rdbEnforcementAction = New System.Windows.Forms.RadioButton
        Me.btnAddNewEntry = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFacilityInformation = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtNewAIRSNumber = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.chbCompletedWork = New System.Windows.Forms.CheckBox
        Me.chbOpenWork = New System.Windows.Forms.CheckBox
        Me.chbDeletedWork = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtFacilityNameFilter = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtFCENumberFilter = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtEnforcementNumberFilter = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtTrackingNumberFilter = New System.Windows.Forms.TextBox
        Me.txtAIRSNumberFilter = New System.Windows.Forms.TextBox
        Me.GBEngineer = New System.Windows.Forms.GroupBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.clbDistrictOffices = New System.Windows.Forms.CheckedListBox
        Me.clbAirBranchUnits = New System.Windows.Forms.CheckedListBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rdbUseUnits = New System.Windows.Forms.RadioButton
        Me.rdbUseEngineer = New System.Windows.Forms.RadioButton
        Me.rdbIgnoreEngineer = New System.Windows.Forms.RadioButton
        Me.clbEngineer = New System.Windows.Forms.CheckedListBox
        Me.chbEngineer = New System.Windows.Forms.CheckBox
        Me.dgvWork = New System.Windows.Forms.DataGridView
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBWorkTypes.SuspendLayout()
        Me.GBNotifications.SuspendLayout()
        Me.GBEnforcementDates.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.TCComplianceLog.SuspendLayout()
        Me.TPSelectWork.SuspendLayout()
        Me.TPStartNewWork.SuspendLayout()
        Me.pnlOtherEvents.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GBEngineer.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvWork, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Image_List_All
        '
        Me.Image_List_All.ImageStream = CType(resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        Me.Image_List_All.Images.SetKeyName(0, "")
        Me.Image_List_All.Images.SetKeyName(1, "")
        Me.Image_List_All.Images.SetKeyName(2, "")
        Me.Image_List_All.Images.SetKeyName(3, "")
        Me.Image_List_All.Images.SetKeyName(4, "")
        Me.Image_List_All.Images.SetKeyName(5, "")
        Me.Image_List_All.Images.SetKeyName(6, "")
        Me.Image_List_All.Images.SetKeyName(7, "")
        Me.Image_List_All.Images.SetKeyName(8, "")
        Me.Image_List_All.Images.SetKeyName(9, "")
        Me.Image_List_All.Images.SetKeyName(10, "")
        Me.Image_List_All.Images.SetKeyName(11, "")
        Me.Image_List_All.Images.SetKeyName(12, "")
        Me.Image_List_All.Images.SetKeyName(13, "")
        Me.Image_List_All.Images.SetKeyName(14, "")
        Me.Image_List_All.Images.SetKeyName(15, "")
        Me.Image_List_All.Images.SetKeyName(16, "")
        Me.Image_List_All.Images.SetKeyName(17, "")
        Me.Image_List_All.Images.SetKeyName(18, "")
        Me.Image_List_All.Images.SetKeyName(19, "")
        Me.Image_List_All.Images.SetKeyName(20, "")
        Me.Image_List_All.Images.SetKeyName(21, "")
        Me.Image_List_All.Images.SetKeyName(22, "")
        Me.Image_List_All.Images.SetKeyName(23, "")
        Me.Image_List_All.Images.SetKeyName(24, "")
        Me.Image_List_All.Images.SetKeyName(25, "")
        Me.Image_List_All.Images.SetKeyName(26, "")
        Me.Image_List_All.Images.SetKeyName(27, "")
        Me.Image_List_All.Images.SetKeyName(28, "")
        Me.Image_List_All.Images.SetKeyName(29, "")
        Me.Image_List_All.Images.SetKeyName(30, "")
        Me.Image_List_All.Images.SetKeyName(31, "")
        Me.Image_List_All.Images.SetKeyName(32, "")
        Me.Image_List_All.Images.SetKeyName(33, "")
        Me.Image_List_All.Images.SetKeyName(34, "")
        Me.Image_List_All.Images.SetKeyName(35, "")
        Me.Image_List_All.Images.SetKeyName(36, "")
        Me.Image_List_All.Images.SetKeyName(37, "")
        Me.Image_List_All.Images.SetKeyName(38, "")
        Me.Image_List_All.Images.SetKeyName(39, "")
        Me.Image_List_All.Images.SetKeyName(40, "")
        Me.Image_List_All.Images.SetKeyName(41, "")
        Me.Image_List_All.Images.SetKeyName(42, "")
        Me.Image_List_All.Images.SetKeyName(43, "")
        Me.Image_List_All.Images.SetKeyName(44, "")
        Me.Image_List_All.Images.SetKeyName(45, "")
        Me.Image_List_All.Images.SetKeyName(46, "")
        Me.Image_List_All.Images.SetKeyName(47, "")
        Me.Image_List_All.Images.SetKeyName(48, "")
        Me.Image_List_All.Images.SetKeyName(49, "")
        Me.Image_List_All.Images.SetKeyName(50, "")
        Me.Image_List_All.Images.SetKeyName(51, "")
        Me.Image_List_All.Images.SetKeyName(52, "")
        Me.Image_List_All.Images.SetKeyName(53, "")
        Me.Image_List_All.Images.SetKeyName(54, "")
        Me.Image_List_All.Images.SetKeyName(55, "")
        Me.Image_List_All.Images.SetKeyName(56, "")
        Me.Image_List_All.Images.SetKeyName(57, "")
        Me.Image_List_All.Images.SetKeyName(58, "")
        Me.Image_List_All.Images.SetKeyName(59, "")
        Me.Image_List_All.Images.SetKeyName(60, "")
        Me.Image_List_All.Images.SetKeyName(61, "")
        Me.Image_List_All.Images.SetKeyName(62, "")
        Me.Image_List_All.Images.SetKeyName(63, "")
        Me.Image_List_All.Images.SetKeyName(64, "")
        Me.Image_List_All.Images.SetKeyName(65, "")
        Me.Image_List_All.Images.SetKeyName(66, "")
        Me.Image_List_All.Images.SetKeyName(67, "")
        Me.Image_List_All.Images.SetKeyName(68, "")
        Me.Image_List_All.Images.SetKeyName(69, "")
        Me.Image_List_All.Images.SetKeyName(70, "")
        Me.Image_List_All.Images.SetKeyName(71, "")
        Me.Image_List_All.Images.SetKeyName(72, "")
        Me.Image_List_All.Images.SetKeyName(73, "")
        Me.Image_List_All.Images.SetKeyName(74, "")
        Me.Image_List_All.Images.SetKeyName(75, "")
        Me.Image_List_All.Images.SetKeyName(76, "")
        Me.Image_List_All.Images.SetKeyName(77, "")
        Me.Image_List_All.Images.SetKeyName(78, "")
        Me.Image_List_All.Images.SetKeyName(79, "")
        Me.Image_List_All.Images.SetKeyName(80, "")
        Me.Image_List_All.Images.SetKeyName(81, "")
        Me.Image_List_All.Images.SetKeyName(82, "")
        Me.Image_List_All.Images.SetKeyName(83, "")
        Me.Image_List_All.Images.SetKeyName(84, "")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiBack})
        Me.MenuItem1.Text = "File"
        '
        'mmiBack
        '
        Me.mmiBack.Index = 0
        Me.mmiBack.Text = "Back"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 1
        Me.mmiHelp.Text = "Help"
        '
        'TBWork_EnTry
        '
        Me.TBWork_EnTry.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbBack, Me.tbbSearch, Me.tbbExportToExcel, Me.tbbClear})
        Me.TBWork_EnTry.DropDownArrows = True
        Me.TBWork_EnTry.ImageList = Me.Image_List_All
        Me.TBWork_EnTry.Location = New System.Drawing.Point(0, 0)
        Me.TBWork_EnTry.Name = "TBWork_EnTry"
        Me.TBWork_EnTry.ShowToolTips = True
        Me.TBWork_EnTry.Size = New System.Drawing.Size(1016, 28)
        Me.TBWork_EnTry.TabIndex = 2
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        '
        'tbbSearch
        '
        Me.tbbSearch.ImageIndex = 3
        Me.tbbSearch.Name = "tbbSearch"
        '
        'tbbExportToExcel
        '
        Me.tbbExportToExcel.ImageIndex = 14
        Me.tbbExportToExcel.Name = "tbbExportToExcel"
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 687)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1016, 22)
        Me.StatusStrip1.TabIndex = 3
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GBWorkTypes)
        Me.GroupBox1.Controls.Add(Me.GBEnforcementDates)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.txtWorkCount)
        Me.GroupBox1.Controls.Add(Me.btnRunFilter)
        Me.GroupBox1.Controls.Add(Me.TCComplianceLog)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox7)
        Me.GroupBox1.Controls.Add(Me.GBEngineer)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 430)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter and Sort Option"
        '
        'GBWorkTypes
        '
        Me.GBWorkTypes.Controls.Add(Me.chbRMPInspections)
        Me.GBWorkTypes.Controls.Add(Me.GBNotifications)
        Me.GBWorkTypes.Controls.Add(Me.chbFCE)
        Me.GBWorkTypes.Controls.Add(Me.chbEnforcement)
        Me.GBWorkTypes.Controls.Add(Me.chbAllWork)
        Me.GBWorkTypes.Controls.Add(Me.chbNotifications)
        Me.GBWorkTypes.Controls.Add(Me.chbPerformanceTests)
        Me.GBWorkTypes.Controls.Add(Me.chbReports)
        Me.GBWorkTypes.Controls.Add(Me.chbInspections)
        Me.GBWorkTypes.Controls.Add(Me.chbACCs)
        Me.GBWorkTypes.Location = New System.Drawing.Point(6, 112)
        Me.GBWorkTypes.Name = "GBWorkTypes"
        Me.GBWorkTypes.Size = New System.Drawing.Size(208, 310)
        Me.GBWorkTypes.TabIndex = 280
        Me.GBWorkTypes.TabStop = False
        Me.GBWorkTypes.Text = "Work Type"
        '
        'chbRMPInspections
        '
        Me.chbRMPInspections.AutoSize = True
        Me.chbRMPInspections.Location = New System.Drawing.Point(8, 132)
        Me.chbRMPInspections.Name = "chbRMPInspections"
        Me.chbRMPInspections.Size = New System.Drawing.Size(155, 17)
        Me.chbRMPInspections.TabIndex = 24
        Me.chbRMPInspections.Text = "Risk Mgmt. Plan Inspection"
        '
        'GBNotifications
        '
        Me.GBNotifications.BackColor = System.Drawing.SystemColors.Control
        Me.GBNotifications.Controls.Add(Me.clbNotifications)
        Me.GBNotifications.Enabled = False
        Me.GBNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GBNotifications.Location = New System.Drawing.Point(16, 173)
        Me.GBNotifications.Name = "GBNotifications"
        Me.GBNotifications.Size = New System.Drawing.Size(186, 133)
        Me.GBNotifications.TabIndex = 23
        Me.GBNotifications.TabStop = False
        Me.GBNotifications.Text = "Notification Types"
        '
        'clbNotifications
        '
        Me.clbNotifications.CheckOnClick = True
        Me.clbNotifications.FormattingEnabled = True
        Me.clbNotifications.Location = New System.Drawing.Point(6, 20)
        Me.clbNotifications.Name = "clbNotifications"
        Me.clbNotifications.Size = New System.Drawing.Size(172, 109)
        Me.clbNotifications.TabIndex = 23
        '
        'chbFCE
        '
        Me.chbFCE.AutoSize = True
        Me.chbFCE.Location = New System.Drawing.Point(8, 65)
        Me.chbFCE.Name = "chbFCE"
        Me.chbFCE.Size = New System.Drawing.Size(153, 17)
        Me.chbFCE.TabIndex = 5
        Me.chbFCE.Text = "Full Compliance Evaluation"
        '
        'chbEnforcement
        '
        Me.chbEnforcement.AutoSize = True
        Me.chbEnforcement.Location = New System.Drawing.Point(8, 48)
        Me.chbEnforcement.Name = "chbEnforcement"
        Me.chbEnforcement.Size = New System.Drawing.Size(86, 17)
        Me.chbEnforcement.TabIndex = 4
        Me.chbEnforcement.Text = "Enforcement"
        '
        'chbAllWork
        '
        Me.chbAllWork.Checked = True
        Me.chbAllWork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbAllWork.Location = New System.Drawing.Point(8, 16)
        Me.chbAllWork.Name = "chbAllWork"
        Me.chbAllWork.Size = New System.Drawing.Size(40, 16)
        Me.chbAllWork.TabIndex = 2
        Me.chbAllWork.Text = "All"
        '
        'chbNotifications
        '
        Me.chbNotifications.AutoSize = True
        Me.chbNotifications.Location = New System.Drawing.Point(8, 150)
        Me.chbNotifications.Name = "chbNotifications"
        Me.chbNotifications.Size = New System.Drawing.Size(79, 17)
        Me.chbNotifications.TabIndex = 7
        Me.chbNotifications.Text = "Notification"
        '
        'chbPerformanceTests
        '
        Me.chbPerformanceTests.AutoSize = True
        Me.chbPerformanceTests.Location = New System.Drawing.Point(8, 98)
        Me.chbPerformanceTests.Name = "chbPerformanceTests"
        Me.chbPerformanceTests.Size = New System.Drawing.Size(110, 17)
        Me.chbPerformanceTests.TabIndex = 8
        Me.chbPerformanceTests.Text = "Performance Test"
        '
        'chbReports
        '
        Me.chbReports.AutoSize = True
        Me.chbReports.Location = New System.Drawing.Point(8, 115)
        Me.chbReports.Name = "chbReports"
        Me.chbReports.Size = New System.Drawing.Size(58, 17)
        Me.chbReports.TabIndex = 9
        Me.chbReports.Text = "Report"
        '
        'chbInspections
        '
        Me.chbInspections.AutoSize = True
        Me.chbInspections.Location = New System.Drawing.Point(8, 82)
        Me.chbInspections.Name = "chbInspections"
        Me.chbInspections.Size = New System.Drawing.Size(75, 17)
        Me.chbInspections.TabIndex = 6
        Me.chbInspections.Text = "Inspection"
        '
        'chbACCs
        '
        Me.chbACCs.AutoSize = True
        Me.chbACCs.Location = New System.Drawing.Point(8, 32)
        Me.chbACCs.Name = "chbACCs"
        Me.chbACCs.Size = New System.Drawing.Size(175, 17)
        Me.chbACCs.TabIndex = 3
        Me.chbACCs.Text = "Annual Compliance Certification"
        '
        'GBEnforcementDates
        '
        Me.GBEnforcementDates.Controls.Add(Me.chbLastModifiedDate)
        Me.GBEnforcementDates.Controls.Add(Me.chbFilterDates)
        Me.GBEnforcementDates.Controls.Add(Me.Label13)
        Me.GBEnforcementDates.Controls.Add(Me.DTPFilterEnd)
        Me.GBEnforcementDates.Controls.Add(Me.Label14)
        Me.GBEnforcementDates.Controls.Add(Me.DTPFilterStart)
        Me.GBEnforcementDates.Location = New System.Drawing.Point(498, 281)
        Me.GBEnforcementDates.Name = "GBEnforcementDates"
        Me.GBEnforcementDates.Size = New System.Drawing.Size(251, 83)
        Me.GBEnforcementDates.TabIndex = 283
        Me.GBEnforcementDates.TabStop = False
        Me.GBEnforcementDates.Text = "Date Bias"
        '
        'chbLastModifiedDate
        '
        Me.chbLastModifiedDate.AutoSize = True
        Me.chbLastModifiedDate.Location = New System.Drawing.Point(123, 18)
        Me.chbLastModifiedDate.Name = "chbLastModifiedDate"
        Me.chbLastModifiedDate.Size = New System.Drawing.Size(122, 17)
        Me.chbLastModifiedDate.TabIndex = 33
        Me.chbLastModifiedDate.Text = "Include last modified"
        Me.chbLastModifiedDate.UseVisualStyleBackColor = True
        '
        'chbFilterDates
        '
        Me.chbFilterDates.AutoSize = True
        Me.chbFilterDates.Location = New System.Drawing.Point(3, 19)
        Me.chbFilterDates.Name = "chbFilterDates"
        Me.chbFilterDates.Size = New System.Drawing.Size(71, 17)
        Me.chbFilterDates.TabIndex = 15
        Me.chbFilterDates.Text = "Use Date"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(133, 38)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "End Date:"
        '
        'DTPFilterEnd
        '
        Me.DTPFilterEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFilterEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFilterEnd.Location = New System.Drawing.Point(149, 54)
        Me.DTPFilterEnd.Name = "DTPFilterEnd"
        Me.DTPFilterEnd.Size = New System.Drawing.Size(96, 20)
        Me.DTPFilterEnd.TabIndex = 17
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(21, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 13)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Start Date:"
        '
        'DTPFilterStart
        '
        Me.DTPFilterStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFilterStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFilterStart.Location = New System.Drawing.Point(29, 54)
        Me.DTPFilterStart.Name = "DTPFilterStart"
        Me.DTPFilterStart.Size = New System.Drawing.Size(96, 20)
        Me.DTPFilterStart.TabIndex = 16
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(110, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(54, 9)
        Me.Label20.TabIndex = 302
        Me.Label20.Text = "Record Count:"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.chbMacon)
        Me.GroupBox6.Controls.Add(Me.chbAugusta)
        Me.GroupBox6.Controls.Add(Me.chbCartersville)
        Me.GroupBox6.Controls.Add(Me.chbAtlanta)
        Me.GroupBox6.Controls.Add(Me.chbAlbany)
        Me.GroupBox6.Controls.Add(Me.chbAthens)
        Me.GroupBox6.Controls.Add(Me.chbBrunswick)
        Me.GroupBox6.Controls.Add(Me.chbAdministrative)
        Me.GroupBox6.Controls.Add(Me.chbAIRToxics)
        Me.GroupBox6.Controls.Add(Me.chbChemicalsMineral)
        Me.GroupBox6.Controls.Add(Me.chbVOCCombustion)
        Me.GroupBox6.Location = New System.Drawing.Point(657, 18)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(100, 17)
        Me.GroupBox6.TabIndex = 282
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Compliance Unit(s) and Districts"
        Me.GroupBox6.Visible = False
        '
        'chbMacon
        '
        Me.chbMacon.AutoSize = True
        Me.chbMacon.Location = New System.Drawing.Point(162, 102)
        Me.chbMacon.Name = "chbMacon"
        Me.chbMacon.Size = New System.Drawing.Size(162, 17)
        Me.chbMacon.TabIndex = 22
        Me.chbMacon.Text = "West Central Distrct (Macon)"
        '
        'chbAugusta
        '
        Me.chbAugusta.AutoSize = True
        Me.chbAugusta.Location = New System.Drawing.Point(6, 101)
        Me.chbAugusta.Name = "chbAugusta"
        Me.chbAugusta.Size = New System.Drawing.Size(155, 17)
        Me.chbAugusta.TabIndex = 20
        Me.chbAugusta.Text = "Northeast District (Augusta)"
        '
        'chbCartersville
        '
        Me.chbCartersville.AutoSize = True
        Me.chbCartersville.Location = New System.Drawing.Point(6, 152)
        Me.chbCartersville.Name = "chbCartersville"
        Me.chbCartersville.Size = New System.Drawing.Size(165, 17)
        Me.chbCartersville.TabIndex = 19
        Me.chbCartersville.Text = "Mountain District (Cartersville)"
        '
        'chbAtlanta
        '
        Me.chbAtlanta.AutoSize = True
        Me.chbAtlanta.Location = New System.Drawing.Point(6, 135)
        Me.chbAtlanta.Name = "chbAtlanta"
        Me.chbAtlanta.Size = New System.Drawing.Size(147, 17)
        Me.chbAtlanta.TabIndex = 18
        Me.chbAtlanta.Text = "Mountain District (Atlanta)"
        '
        'chbAlbany
        '
        Me.chbAlbany.AutoSize = True
        Me.chbAlbany.Location = New System.Drawing.Point(162, 85)
        Me.chbAlbany.Name = "chbAlbany"
        Me.chbAlbany.Size = New System.Drawing.Size(150, 17)
        Me.chbAlbany.TabIndex = 17
        Me.chbAlbany.Text = "Southwest Distrct (Albany)"
        '
        'chbAthens
        '
        Me.chbAthens.AutoSize = True
        Me.chbAthens.Location = New System.Drawing.Point(6, 84)
        Me.chbAthens.Name = "chbAthens"
        Me.chbAthens.Size = New System.Drawing.Size(147, 17)
        Me.chbAthens.TabIndex = 16
        Me.chbAthens.Text = "Northeast Distrct (Athens)"
        '
        'chbBrunswick
        '
        Me.chbBrunswick.AutoSize = True
        Me.chbBrunswick.Location = New System.Drawing.Point(6, 118)
        Me.chbBrunswick.Name = "chbBrunswick"
        Me.chbBrunswick.Size = New System.Drawing.Size(154, 17)
        Me.chbBrunswick.TabIndex = 15
        Me.chbBrunswick.Text = "Coastal District (Brunswick)"
        '
        'chbAdministrative
        '
        Me.chbAdministrative.AutoSize = True
        Me.chbAdministrative.Location = New System.Drawing.Point(6, 16)
        Me.chbAdministrative.Name = "chbAdministrative"
        Me.chbAdministrative.Size = New System.Drawing.Size(91, 17)
        Me.chbAdministrative.TabIndex = 11
        Me.chbAdministrative.Text = "Administrative"
        '
        'chbAIRToxics
        '
        Me.chbAIRToxics.AutoSize = True
        Me.chbAIRToxics.Location = New System.Drawing.Point(6, 33)
        Me.chbAIRToxics.Name = "chbAIRToxics"
        Me.chbAIRToxics.Size = New System.Drawing.Size(94, 17)
        Me.chbAIRToxics.TabIndex = 12
        Me.chbAIRToxics.Text = "Air Toxics Unit"
        '
        'chbChemicalsMineral
        '
        Me.chbChemicalsMineral.AutoSize = True
        Me.chbChemicalsMineral.Location = New System.Drawing.Point(6, 50)
        Me.chbChemicalsMineral.Name = "chbChemicalsMineral"
        Me.chbChemicalsMineral.Size = New System.Drawing.Size(140, 17)
        Me.chbChemicalsMineral.TabIndex = 13
        Me.chbChemicalsMineral.Text = "Chemicals/Minerals Unit"
        '
        'chbVOCCombustion
        '
        Me.chbVOCCombustion.AutoSize = True
        Me.chbVOCCombustion.Location = New System.Drawing.Point(6, 67)
        Me.chbVOCCombustion.Name = "chbVOCCombustion"
        Me.chbVOCCombustion.Size = New System.Drawing.Size(130, 17)
        Me.chbVOCCombustion.TabIndex = 14
        Me.chbVOCCombustion.Text = "VOC/Combustion Unit"
        '
        'txtWorkCount
        '
        Me.txtWorkCount.Location = New System.Drawing.Point(166, 15)
        Me.txtWorkCount.Name = "txtWorkCount"
        Me.txtWorkCount.ReadOnly = True
        Me.txtWorkCount.Size = New System.Drawing.Size(48, 20)
        Me.txtWorkCount.TabIndex = 301
        '
        'btnRunFilter
        '
        Me.btnRunFilter.AutoSize = True
        Me.btnRunFilter.Location = New System.Drawing.Point(8, 15)
        Me.btnRunFilter.Name = "btnRunFilter"
        Me.btnRunFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnRunFilter.TabIndex = 1
        Me.btnRunFilter.Text = "Run Filter"
        Me.btnRunFilter.UseVisualStyleBackColor = True
        '
        'TCComplianceLog
        '
        Me.TCComplianceLog.Controls.Add(Me.TPSelectWork)
        Me.TCComplianceLog.Controls.Add(Me.TPStartNewWork)
        Me.TCComplianceLog.Dock = System.Windows.Forms.DockStyle.Right
        Me.TCComplianceLog.Location = New System.Drawing.Point(763, 16)
        Me.TCComplianceLog.Name = "TCComplianceLog"
        Me.TCComplianceLog.SelectedIndex = 0
        Me.TCComplianceLog.Size = New System.Drawing.Size(250, 411)
        Me.TCComplianceLog.TabIndex = 290
        '
        'TPSelectWork
        '
        Me.TPSelectWork.Controls.Add(Me.btnOpenSummary)
        Me.TPSelectWork.Controls.Add(Me.btnUndeleteWork)
        Me.TPSelectWork.Controls.Add(Me.btnDeleteWork)
        Me.TPSelectWork.Controls.Add(Me.btnSelectWork)
        Me.TPSelectWork.Controls.Add(Me.lblWorkType)
        Me.TPSelectWork.Controls.Add(Me.txtFacilityCounty)
        Me.TPSelectWork.Controls.Add(Me.txtWorkNumber)
        Me.TPSelectWork.Controls.Add(Me.Label7)
        Me.TPSelectWork.Controls.Add(Me.Label3)
        Me.TPSelectWork.Controls.Add(Me.txtFacilityCity)
        Me.TPSelectWork.Controls.Add(Me.txtFacilityName)
        Me.TPSelectWork.Controls.Add(Me.Label6)
        Me.TPSelectWork.Controls.Add(Me.Label2)
        Me.TPSelectWork.Controls.Add(Me.txtTestType)
        Me.TPSelectWork.Controls.Add(Me.txtAIRSNumber)
        Me.TPSelectWork.Controls.Add(Me.Label1)
        Me.TPSelectWork.Location = New System.Drawing.Point(4, 22)
        Me.TPSelectWork.Name = "TPSelectWork"
        Me.TPSelectWork.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSelectWork.Size = New System.Drawing.Size(242, 385)
        Me.TPSelectWork.TabIndex = 0
        Me.TPSelectWork.Text = "Select Work Entry "
        Me.TPSelectWork.UseVisualStyleBackColor = True
        '
        'btnOpenSummary
        '
        Me.btnOpenSummary.AutoSize = True
        Me.btnOpenSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenSummary.Location = New System.Drawing.Point(135, 68)
        Me.btnOpenSummary.Name = "btnOpenSummary"
        Me.btnOpenSummary.Size = New System.Drawing.Size(95, 23)
        Me.btnOpenSummary.TabIndex = 29
        Me.btnOpenSummary.Text = "Facility Summary"
        Me.btnOpenSummary.UseVisualStyleBackColor = True
        '
        'btnUndeleteWork
        '
        Me.btnUndeleteWork.AutoSize = True
        Me.btnUndeleteWork.Location = New System.Drawing.Point(141, 303)
        Me.btnUndeleteWork.Name = "btnUndeleteWork"
        Me.btnUndeleteWork.Size = New System.Drawing.Size(89, 23)
        Me.btnUndeleteWork.TabIndex = 35
        Me.btnUndeleteWork.Text = "Undelete Work"
        Me.btnUndeleteWork.UseVisualStyleBackColor = True
        '
        'btnDeleteWork
        '
        Me.btnDeleteWork.AutoSize = True
        Me.btnDeleteWork.Location = New System.Drawing.Point(22, 303)
        Me.btnDeleteWork.Name = "btnDeleteWork"
        Me.btnDeleteWork.Size = New System.Drawing.Size(77, 23)
        Me.btnDeleteWork.TabIndex = 34
        Me.btnDeleteWork.Text = "Delete Work"
        Me.btnDeleteWork.UseVisualStyleBackColor = True
        '
        'btnSelectWork
        '
        Me.btnSelectWork.AutoSize = True
        Me.btnSelectWork.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectWork.Location = New System.Drawing.Point(145, 23)
        Me.btnSelectWork.Name = "btnSelectWork"
        Me.btnSelectWork.Size = New System.Drawing.Size(72, 23)
        Me.btnSelectWork.TabIndex = 27
        Me.btnSelectWork.Text = "Open Work"
        Me.btnSelectWork.UseVisualStyleBackColor = True
        '
        'lblWorkType
        '
        Me.lblWorkType.AutoSize = True
        Me.lblWorkType.Location = New System.Drawing.Point(6, 8)
        Me.lblWorkType.Name = "lblWorkType"
        Me.lblWorkType.Size = New System.Drawing.Size(110, 13)
        Me.lblWorkType.TabIndex = 234
        Me.lblWorkType.Text = "Unique Work Number"
        '
        'txtFacilityCounty
        '
        Me.txtFacilityCounty.Location = New System.Drawing.Point(22, 248)
        Me.txtFacilityCounty.Name = "txtFacilityCounty"
        Me.txtFacilityCounty.ReadOnly = True
        Me.txtFacilityCounty.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityCounty.TabIndex = 33
        '
        'txtWorkNumber
        '
        Me.txtWorkNumber.Location = New System.Drawing.Point(22, 24)
        Me.txtWorkNumber.Name = "txtWorkNumber"
        Me.txtWorkNumber.ReadOnly = True
        Me.txtWorkNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtWorkNumber.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 232)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 244
        Me.Label7.Text = "Facility County"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 236
        Me.Label3.Text = "Facility Name"
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.Location = New System.Drawing.Point(22, 208)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.ReadOnly = True
        Me.txtFacilityCity.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityCity.TabIndex = 32
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(22, 104)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityName.TabIndex = 30
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 192)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 242
        Me.Label6.Text = "Facility City"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 238
        Me.Label2.Text = "AIRS Number"
        '
        'txtTestType
        '
        Me.txtTestType.Location = New System.Drawing.Point(22, 144)
        Me.txtTestType.Name = "txtTestType"
        Me.txtTestType.ReadOnly = True
        Me.txtTestType.Size = New System.Drawing.Size(208, 20)
        Me.txtTestType.TabIndex = 31
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(22, 67)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtAIRSNumber.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 240
        Me.Label1.Text = "Work Type"
        '
        'TPStartNewWork
        '
        Me.TPStartNewWork.Controls.Add(Me.pnlOtherEvents)
        Me.TPStartNewWork.Controls.Add(Me.Panel4)
        Me.TPStartNewWork.Controls.Add(Me.btnAddNewEntry)
        Me.TPStartNewWork.Controls.Add(Me.Label5)
        Me.TPStartNewWork.Controls.Add(Me.txtFacilityInformation)
        Me.TPStartNewWork.Controls.Add(Me.Label4)
        Me.TPStartNewWork.Controls.Add(Me.txtNewAIRSNumber)
        Me.TPStartNewWork.Location = New System.Drawing.Point(4, 22)
        Me.TPStartNewWork.Name = "TPStartNewWork"
        Me.TPStartNewWork.Padding = New System.Windows.Forms.Padding(3)
        Me.TPStartNewWork.Size = New System.Drawing.Size(242, 385)
        Me.TPStartNewWork.TabIndex = 1
        Me.TPStartNewWork.Text = "Start New Work"
        Me.TPStartNewWork.UseVisualStyleBackColor = True
        '
        'pnlOtherEvents
        '
        Me.pnlOtherEvents.Controls.Add(Me.txtTrackingNumber)
        Me.pnlOtherEvents.Controls.Add(Me.Label8)
        Me.pnlOtherEvents.Controls.Add(Me.lblOtherNumber)
        Me.pnlOtherEvents.Controls.Add(Me.cboEvent)
        Me.pnlOtherEvents.Controls.Add(Me.LabEventDescription)
        Me.pnlOtherEvents.Controls.Add(Me.DTPDateReceived)
        Me.pnlOtherEvents.Controls.Add(Me.lblDateField)
        Me.pnlOtherEvents.Location = New System.Drawing.Point(9, 247)
        Me.pnlOtherEvents.Name = "pnlOtherEvents"
        Me.pnlOtherEvents.Size = New System.Drawing.Size(236, 157)
        Me.pnlOtherEvents.TabIndex = 43
        Me.pnlOtherEvents.Visible = False
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(17, 22)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtTrackingNumber.TabIndex = 44
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1, 57)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 13)
        Me.Label8.TabIndex = 275
        Me.Label8.Text = "Compliance Event"
        '
        'lblOtherNumber
        '
        Me.lblOtherNumber.AutoSize = True
        Me.lblOtherNumber.Location = New System.Drawing.Point(1, 4)
        Me.lblOtherNumber.Name = "lblOtherNumber"
        Me.lblOtherNumber.Size = New System.Drawing.Size(92, 13)
        Me.lblOtherNumber.TabIndex = 279
        Me.lblOtherNumber.Text = "Tracking Number:"
        '
        'cboEvent
        '
        Me.cboEvent.Location = New System.Drawing.Point(17, 75)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.Size = New System.Drawing.Size(208, 21)
        Me.cboEvent.TabIndex = 46
        '
        'LabEventDescription
        '
        Me.LabEventDescription.Location = New System.Drawing.Point(21, 104)
        Me.LabEventDescription.Name = "LabEventDescription"
        Me.LabEventDescription.Size = New System.Drawing.Size(204, 41)
        Me.LabEventDescription.TabIndex = 276
        Me.LabEventDescription.Visible = False
        '
        'DTPDateReceived
        '
        Me.DTPDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReceived.Location = New System.Drawing.Point(117, 23)
        Me.DTPDateReceived.Name = "DTPDateReceived"
        Me.DTPDateReceived.Size = New System.Drawing.Size(104, 20)
        Me.DTPDateReceived.TabIndex = 45
        Me.DTPDateReceived.Value = New Date(2007, 1, 23, 0, 0, 0, 0)
        '
        'lblDateField
        '
        Me.lblDateField.AutoSize = True
        Me.lblDateField.Location = New System.Drawing.Point(101, 5)
        Me.lblDateField.Name = "lblDateField"
        Me.lblDateField.Size = New System.Drawing.Size(103, 13)
        Me.lblDateField.TabIndex = 277
        Me.lblDateField.Text = "Received by GEPD:"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rdbPerformanceTest)
        Me.Panel4.Controls.Add(Me.rdbOther)
        Me.Panel4.Controls.Add(Me.rdbFCE)
        Me.Panel4.Controls.Add(Me.rdbEnforcementAction)
        Me.Panel4.Location = New System.Drawing.Point(22, 132)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(173, 84)
        Me.Panel4.TabIndex = 38
        '
        'rdbPerformanceTest
        '
        Me.rdbPerformanceTest.AutoSize = True
        Me.rdbPerformanceTest.Location = New System.Drawing.Point(7, 43)
        Me.rdbPerformanceTest.Name = "rdbPerformanceTest"
        Me.rdbPerformanceTest.Size = New System.Drawing.Size(109, 17)
        Me.rdbPerformanceTest.TabIndex = 42
        Me.rdbPerformanceTest.TabStop = True
        Me.rdbPerformanceTest.Text = "Performance Test"
        Me.rdbPerformanceTest.UseVisualStyleBackColor = True
        '
        'rdbOther
        '
        Me.rdbOther.AutoSize = True
        Me.rdbOther.Location = New System.Drawing.Point(7, 61)
        Me.rdbOther.Name = "rdbOther"
        Me.rdbOther.Size = New System.Drawing.Size(51, 17)
        Me.rdbOther.TabIndex = 41
        Me.rdbOther.TabStop = True
        Me.rdbOther.Text = "Other"
        Me.rdbOther.UseVisualStyleBackColor = True
        '
        'rdbFCE
        '
        Me.rdbFCE.AutoSize = True
        Me.rdbFCE.Location = New System.Drawing.Point(7, 25)
        Me.rdbFCE.Name = "rdbFCE"
        Me.rdbFCE.Size = New System.Drawing.Size(152, 17)
        Me.rdbFCE.TabIndex = 40
        Me.rdbFCE.TabStop = True
        Me.rdbFCE.Text = "Full Compliance Evaluation"
        Me.rdbFCE.UseVisualStyleBackColor = True
        '
        'rdbEnforcementAction
        '
        Me.rdbEnforcementAction.AutoSize = True
        Me.rdbEnforcementAction.Location = New System.Drawing.Point(7, 8)
        Me.rdbEnforcementAction.Name = "rdbEnforcementAction"
        Me.rdbEnforcementAction.Size = New System.Drawing.Size(118, 17)
        Me.rdbEnforcementAction.TabIndex = 39
        Me.rdbEnforcementAction.TabStop = True
        Me.rdbEnforcementAction.Text = "Enforcement Action"
        Me.rdbEnforcementAction.UseVisualStyleBackColor = True
        '
        'btnAddNewEntry
        '
        Me.btnAddNewEntry.AutoSize = True
        Me.btnAddNewEntry.Location = New System.Drawing.Point(22, 221)
        Me.btnAddNewEntry.Name = "btnAddNewEntry"
        Me.btnAddNewEntry.Size = New System.Drawing.Size(88, 23)
        Me.btnAddNewEntry.TabIndex = 42
        Me.btnAddNewEntry.Text = "&Add New Entry"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 242
        Me.Label5.Text = "Facility Information"
        '
        'txtFacilityInformation
        '
        Me.txtFacilityInformation.Location = New System.Drawing.Point(22, 62)
        Me.txtFacilityInformation.Multiline = True
        Me.txtFacilityInformation.Name = "txtFacilityInformation"
        Me.txtFacilityInformation.ReadOnly = True
        Me.txtFacilityInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityInformation.Size = New System.Drawing.Size(208, 64)
        Me.txtFacilityInformation.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 240
        Me.Label4.Text = "AIRS Number"
        '
        'txtNewAIRSNumber
        '
        Me.txtNewAIRSNumber.Location = New System.Drawing.Point(22, 23)
        Me.txtNewAIRSNumber.MaxLength = 8
        Me.txtNewAIRSNumber.Name = "txtNewAIRSNumber"
        Me.txtNewAIRSNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtNewAIRSNumber.TabIndex = 36
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chbCompletedWork)
        Me.GroupBox5.Controls.Add(Me.chbOpenWork)
        Me.GroupBox5.Controls.Add(Me.chbDeletedWork)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 42)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(206, 68)
        Me.GroupBox5.TabIndex = 285
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Open/Closed/Deleted"
        '
        'chbCompletedWork
        '
        Me.chbCompletedWork.Location = New System.Drawing.Point(9, 33)
        Me.chbCompletedWork.Name = "chbCompletedWork"
        Me.chbCompletedWork.Size = New System.Drawing.Size(112, 16)
        Me.chbCompletedWork.TabIndex = 24
        Me.chbCompletedWork.Text = "Completed Work"
        '
        'chbOpenWork
        '
        Me.chbOpenWork.Checked = True
        Me.chbOpenWork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbOpenWork.Location = New System.Drawing.Point(9, 17)
        Me.chbOpenWork.Name = "chbOpenWork"
        Me.chbOpenWork.Size = New System.Drawing.Size(80, 16)
        Me.chbOpenWork.TabIndex = 23
        Me.chbOpenWork.Text = "Open Work"
        '
        'chbDeletedWork
        '
        Me.chbDeletedWork.Location = New System.Drawing.Point(9, 50)
        Me.chbDeletedWork.Name = "chbDeletedWork"
        Me.chbDeletedWork.Size = New System.Drawing.Size(96, 16)
        Me.chbDeletedWork.TabIndex = 25
        Me.chbDeletedWork.Text = "Deleted Work"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.txtFacilityNameFilter)
        Me.GroupBox7.Controls.Add(Me.Label12)
        Me.GroupBox7.Controls.Add(Me.txtFCENumberFilter)
        Me.GroupBox7.Controls.Add(Me.Label10)
        Me.GroupBox7.Controls.Add(Me.txtEnforcementNumberFilter)
        Me.GroupBox7.Controls.Add(Me.Label11)
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Controls.Add(Me.txtTrackingNumberFilter)
        Me.GroupBox7.Controls.Add(Me.txtAIRSNumberFilter)
        Me.GroupBox7.Location = New System.Drawing.Point(226, 275)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(266, 138)
        Me.GroupBox7.TabIndex = 284
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Misc. "
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 114)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 39
        Me.Label15.Text = "Facility Name"
        '
        'txtFacilityNameFilter
        '
        Me.txtFacilityNameFilter.Location = New System.Drawing.Point(119, 110)
        Me.txtFacilityNameFilter.MaxLength = 8
        Me.txtFacilityNameFilter.Name = "txtFacilityNameFilter"
        Me.txtFacilityNameFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtFacilityNameFilter.TabIndex = 22
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 13)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "FCE Number"
        '
        'txtFCENumberFilter
        '
        Me.txtFCENumberFilter.Location = New System.Drawing.Point(119, 84)
        Me.txtFCENumberFilter.MaxLength = 8
        Me.txtFCENumberFilter.Name = "txtFCENumberFilter"
        Me.txtFCENumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtFCENumberFilter.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 13)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Enforcement Number"
        '
        'txtEnforcementNumberFilter
        '
        Me.txtEnforcementNumberFilter.Location = New System.Drawing.Point(119, 61)
        Me.txtEnforcementNumberFilter.MaxLength = 8
        Me.txtEnforcementNumberFilter.Name = "txtEnforcementNumberFilter"
        Me.txtEnforcementNumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtEnforcementNumberFilter.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Tracking Number"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "AIRS Number:"
        '
        'txtTrackingNumberFilter
        '
        Me.txtTrackingNumberFilter.Location = New System.Drawing.Point(119, 37)
        Me.txtTrackingNumberFilter.MaxLength = 8
        Me.txtTrackingNumberFilter.Name = "txtTrackingNumberFilter"
        Me.txtTrackingNumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtTrackingNumberFilter.TabIndex = 19
        '
        'txtAIRSNumberFilter
        '
        Me.txtAIRSNumberFilter.Location = New System.Drawing.Point(119, 14)
        Me.txtAIRSNumberFilter.MaxLength = 8
        Me.txtAIRSNumberFilter.Name = "txtAIRSNumberFilter"
        Me.txtAIRSNumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtAIRSNumberFilter.TabIndex = 18
        '
        'GBEngineer
        '
        Me.GBEngineer.Controls.Add(Me.Label17)
        Me.GBEngineer.Controls.Add(Me.Label16)
        Me.GBEngineer.Controls.Add(Me.clbDistrictOffices)
        Me.GBEngineer.Controls.Add(Me.clbAirBranchUnits)
        Me.GBEngineer.Controls.Add(Me.Panel5)
        Me.GBEngineer.Controls.Add(Me.clbEngineer)
        Me.GBEngineer.Controls.Add(Me.chbEngineer)
        Me.GBEngineer.Location = New System.Drawing.Point(226, 15)
        Me.GBEngineer.Name = "GBEngineer"
        Me.GBEngineer.Size = New System.Drawing.Size(420, 257)
        Me.GBEngineer.TabIndex = 281
        Me.GBEngineer.TabStop = False
        Me.GBEngineer.Text = "Staff Search Criteria"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(208, 119)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 13)
        Me.Label17.TabIndex = 309
        Me.Label17.Text = "District Office"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(208, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(78, 13)
        Me.Label16.TabIndex = 308
        Me.Label16.Text = "Air Branch Unit"
        '
        'clbDistrictOffices
        '
        Me.clbDistrictOffices.CheckOnClick = True
        Me.clbDistrictOffices.Location = New System.Drawing.Point(218, 137)
        Me.clbDistrictOffices.Name = "clbDistrictOffices"
        Me.clbDistrictOffices.Size = New System.Drawing.Size(199, 109)
        Me.clbDistrictOffices.TabIndex = 307
        '
        'clbAirBranchUnits
        '
        Me.clbAirBranchUnits.CheckOnClick = True
        Me.clbAirBranchUnits.Location = New System.Drawing.Point(215, 62)
        Me.clbAirBranchUnits.Name = "clbAirBranchUnits"
        Me.clbAirBranchUnits.Size = New System.Drawing.Size(199, 49)
        Me.clbAirBranchUnits.TabIndex = 305
        '
        'Panel5
        '
        Me.Panel5.AutoSize = True
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.Controls.Add(Me.rdbUseUnits)
        Me.Panel5.Controls.Add(Me.rdbUseEngineer)
        Me.Panel5.Controls.Add(Me.rdbIgnoreEngineer)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(3, 16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(414, 25)
        Me.Panel5.TabIndex = 303
        '
        'rdbUseUnits
        '
        Me.rdbUseUnits.AutoSize = True
        Me.rdbUseUnits.Location = New System.Drawing.Point(202, 4)
        Me.rdbUseUnits.Name = "rdbUseUnits"
        Me.rdbUseUnits.Size = New System.Drawing.Size(134, 17)
        Me.rdbUseUnits.TabIndex = 2
        Me.rdbUseUnits.Text = "Use Unit/District Office"
        Me.rdbUseUnits.UseVisualStyleBackColor = True
        '
        'rdbUseEngineer
        '
        Me.rdbUseEngineer.AutoSize = True
        Me.rdbUseEngineer.Checked = True
        Me.rdbUseEngineer.Location = New System.Drawing.Point(91, 4)
        Me.rdbUseEngineer.Name = "rdbUseEngineer"
        Me.rdbUseEngineer.Size = New System.Drawing.Size(91, 17)
        Me.rdbUseEngineer.TabIndex = 1
        Me.rdbUseEngineer.TabStop = True
        Me.rdbUseEngineer.Text = "Use Inspector"
        Me.rdbUseEngineer.UseVisualStyleBackColor = True
        '
        'rdbIgnoreEngineer
        '
        Me.rdbIgnoreEngineer.AutoSize = True
        Me.rdbIgnoreEngineer.Location = New System.Drawing.Point(3, 5)
        Me.rdbIgnoreEngineer.Name = "rdbIgnoreEngineer"
        Me.rdbIgnoreEngineer.Size = New System.Drawing.Size(80, 17)
        Me.rdbIgnoreEngineer.TabIndex = 0
        Me.rdbIgnoreEngineer.Text = "Entire State"
        Me.rdbIgnoreEngineer.UseVisualStyleBackColor = True
        '
        'clbEngineer
        '
        Me.clbEngineer.CheckOnClick = True
        Me.clbEngineer.Location = New System.Drawing.Point(6, 62)
        Me.clbEngineer.Name = "clbEngineer"
        Me.clbEngineer.Size = New System.Drawing.Size(199, 184)
        Me.clbEngineer.TabIndex = 200
        '
        'chbEngineer
        '
        Me.chbEngineer.AutoSize = True
        Me.chbEngineer.Checked = True
        Me.chbEngineer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbEngineer.Location = New System.Drawing.Point(6, 44)
        Me.chbEngineer.Name = "chbEngineer"
        Me.chbEngineer.Size = New System.Drawing.Size(107, 17)
        Me.chbEngineer.TabIndex = 10
        Me.chbEngineer.Text = "Current Inspector"
        '
        'dgvWork
        '
        Me.dgvWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWork.Location = New System.Drawing.Point(0, 458)
        Me.dgvWork.Name = "dgvWork"
        Me.dgvWork.ReadOnly = True
        Me.dgvWork.Size = New System.Drawing.Size(1016, 229)
        Me.dgvWork.TabIndex = 5
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.Blue
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 458)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1016, 5)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'SSCPComplianceLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1016, 709)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.dgvWork)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TBWork_EnTry)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPComplianceLog"
        Me.Text = "Compliance Log"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBWorkTypes.ResumeLayout(False)
        Me.GBWorkTypes.PerformLayout()
        Me.GBNotifications.ResumeLayout(False)
        Me.GBEnforcementDates.ResumeLayout(False)
        Me.GBEnforcementDates.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TCComplianceLog.ResumeLayout(False)
        Me.TPSelectWork.ResumeLayout(False)
        Me.TPSelectWork.PerformLayout()
        Me.TPStartNewWork.ResumeLayout(False)
        Me.TPStartNewWork.PerformLayout()
        Me.pnlOtherEvents.ResumeLayout(False)
        Me.pnlOtherEvents.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GBEngineer.ResumeLayout(False)
        Me.GBEngineer.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgvWork, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents TBWork_EnTry As System.Windows.Forms.ToolBar
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvWork As System.Windows.Forms.DataGridView
    Friend WithEvents GBEngineer As System.Windows.Forms.GroupBox
    Friend WithEvents clbEngineer As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents GBWorkTypes As System.Windows.Forms.GroupBox
    Friend WithEvents chbAllWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbNotifications As System.Windows.Forms.CheckBox
    Friend WithEvents chbPerformanceTests As System.Windows.Forms.CheckBox
    Friend WithEvents chbReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbInspections As System.Windows.Forms.CheckBox
    Friend WithEvents chbACCs As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFacilityCounty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTestType As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblWorkType As System.Windows.Forms.Label
    Friend WithEvents txtWorkNumber As System.Windows.Forms.TextBox
    Friend WithEvents chbFCE As System.Windows.Forms.CheckBox
    Friend WithEvents chbEnforcement As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents chbAIRToxics As System.Windows.Forms.CheckBox
    Friend WithEvents chbChemicalsMineral As System.Windows.Forms.CheckBox
    Friend WithEvents chbVOCCombustion As System.Windows.Forms.CheckBox
    Friend WithEvents chbCompletedWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbOpenWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbDeletedWork As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFCENumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEnforcementNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTrackingNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents GBEnforcementDates As System.Windows.Forms.GroupBox
    Friend WithEvents chbFilterDates As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DTPFilterEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DTPFilterStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents btnSelectWork As System.Windows.Forms.Button
    Friend WithEvents TCComplianceLog As System.Windows.Forms.TabControl
    Friend WithEvents TPSelectWork As System.Windows.Forms.TabPage
    Friend WithEvents TPStartNewWork As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityInformation As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboEvent As System.Windows.Forms.ComboBox
    Friend WithEvents LabEventDescription As System.Windows.Forms.Label
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblOtherNumber As System.Windows.Forms.Label
    Friend WithEvents btnAddNewEntry As System.Windows.Forms.Button
    Friend WithEvents DTPDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDateField As System.Windows.Forms.Label
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents chbAdministrative As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtWorkCount As System.Windows.Forms.TextBox
    Friend WithEvents pnlOtherEvents As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbOther As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFCE As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEnforcementAction As System.Windows.Forms.RadioButton
    Friend WithEvents btnDeleteWork As System.Windows.Forms.Button
    Friend WithEvents btnUndeleteWork As System.Windows.Forms.Button
    Friend WithEvents btnOpenSummary As System.Windows.Forms.Button
    Friend WithEvents tbbSearch As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbExportToExcel As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents chbMacon As System.Windows.Forms.CheckBox
    Friend WithEvents chbAugusta As System.Windows.Forms.CheckBox
    Friend WithEvents chbCartersville As System.Windows.Forms.CheckBox
    Friend WithEvents chbAtlanta As System.Windows.Forms.CheckBox
    Friend WithEvents chbAlbany As System.Windows.Forms.CheckBox
    Friend WithEvents chbAthens As System.Windows.Forms.CheckBox
    Friend WithEvents chbBrunswick As System.Windows.Forms.CheckBox
    Friend WithEvents rdbPerformanceTest As System.Windows.Forms.RadioButton
    Friend WithEvents GBNotifications As System.Windows.Forms.GroupBox
    Friend WithEvents clbNotifications As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbLastModifiedDate As System.Windows.Forms.CheckBox
    Friend WithEvents chbRMPInspections As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdbUseEngineer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbIgnoreEngineer As System.Windows.Forms.RadioButton
    Friend WithEvents clbAirBranchUnits As System.Windows.Forms.CheckedListBox
    Friend WithEvents rdbUseUnits As System.Windows.Forms.RadioButton
    Friend WithEvents clbDistrictOffices As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
