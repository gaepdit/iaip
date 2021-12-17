<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPMonitoringLog
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiReset = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbClear = New System.Windows.Forms.ToolStripButton()
        Me.tsbFacilitySearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbResize = New System.Windows.Forms.ToolStripButton()
        Me.tsbExportToExcel = New System.Windows.Forms.ToolStripButton()
        Me.SCMonitoringLog = New System.Windows.Forms.SplitContainer()
        Me.GBFilterAndSortOptions = New System.Windows.Forms.GroupBox()
        Me.txtTestFirmCommentsCount = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtNotificationCount = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.chbNotificationUnlinked = New System.Windows.Forms.CheckBox()
        Me.chbNotificationLinked = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtTestingFirm = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtPollutantFilter = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCommentFieldFilter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEmissionSourceTestedFilter = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtCountyFilter = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFacilityNameFilter = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCityFilter = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtNotificationNumberFilter = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtReferenceNumberFilter = New System.Windows.Forms.TextBox()
        Me.txtAIRSNumberFilter = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.clbWitnessingStaff = New System.Windows.Forms.CheckedListBox()
        Me.chbWitnessingEngineer = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.clbEngineer = New System.Windows.Forms.CheckedListBox()
        Me.chbReviewingEngineer = New System.Windows.Forms.CheckBox()
        Me.btnRunFilter = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chbTestFirmComments = New System.Windows.Forms.CheckBox()
        Me.chbNotifications = New System.Windows.Forms.CheckBox()
        Me.chbTestReports = New System.Windows.Forms.CheckBox()
        Me.TCMonitoringSelection = New System.Windows.Forms.TabControl()
        Me.TPSelectTestReport = New System.Windows.Forms.TabPage()
        Me.GBSelectedTestReport = New System.Windows.Forms.GroupBox()
        Me.LLSelectReport = New System.Windows.Forms.LinkLabel()
        Me.txtPollutant = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFacilityCounty = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFacilityCity = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTestType = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox()
        Me.TPSelectTestLog = New System.Windows.Forms.TabPage()
        Me.llbSelectTestLog = New System.Windows.Forms.LinkLabel()
        Me.txtNotificationTestStart = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtNotificationCounty = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtNotificationCity = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNotificationEmissionUnit = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtNotificationAIRSNumber = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNotificationFacilityName = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtTestLogNumber = New System.Windows.Forms.TextBox()
        Me.TPTestFirmComment = New System.Windows.Forms.TabPage()
        Me.txtTestFirmName = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtTestFirmTestLogNumber = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtTestFirmReferenceNumber = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.llbOpenComments = New System.Windows.Forms.LinkLabel()
        Me.txtTestFirmCounty = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtTestFirmFacilityCity = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtTestFirmCommentType = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtTestFirmAirsNumber = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtTestFirmFacilityName = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtCommentNumber = New System.Windows.Forms.TextBox()
        Me.txtReportCount = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chbAll = New System.Windows.Forms.CheckBox()
        Me.chbPTE = New System.Windows.Forms.CheckBox()
        Me.chbMethod22 = New System.Windows.Forms.CheckBox()
        Me.chbMethod9Single = New System.Windows.Forms.CheckBox()
        Me.chbMethod9Multi = New System.Windows.Forms.CheckBox()
        Me.chbMemorandumToFile = New System.Windows.Forms.CheckBox()
        Me.chbMemorandumStandard = New System.Windows.Forms.CheckBox()
        Me.chbRata = New System.Windows.Forms.CheckBox()
        Me.chbFlare = New System.Windows.Forms.CheckBox()
        Me.chbGasConcentration = New System.Windows.Forms.CheckBox()
        Me.chbPondTreatment = New System.Windows.Forms.CheckBox()
        Me.chbLoadingRack = New System.Windows.Forms.CheckBox()
        Me.chbTwoStackDRE = New System.Windows.Forms.CheckBox()
        Me.chbTwoStackStandard = New System.Windows.Forms.CheckBox()
        Me.chbOneStackFourRun = New System.Windows.Forms.CheckBox()
        Me.chbOneStackThreeRun = New System.Windows.Forms.CheckBox()
        Me.chbOneStackTwoRun = New System.Windows.Forms.CheckBox()
        Me.chbUnassigned = New System.Windows.Forms.CheckBox()
        Me.GBReportType = New System.Windows.Forms.GroupBox()
        Me.chbOther = New System.Windows.Forms.CheckBox()
        Me.chbSourceTest = New System.Windows.Forms.CheckBox()
        Me.chbRATAandCEMS = New System.Windows.Forms.CheckBox()
        Me.chbPEMSDevelopment = New System.Windows.Forms.CheckBox()
        Me.chbMonitorCertification = New System.Windows.Forms.CheckBox()
        Me.GBDateBias = New System.Windows.Forms.GroupBox()
        Me.rdbNA = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DTPEndDate = New System.Windows.Forms.DateTimePicker()
        Me.DTPStartDate = New System.Windows.Forms.DateTimePicker()
        Me.rdbFacilityDateCompleted = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityDateTested = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityDateReceived = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chbClosed = New System.Windows.Forms.CheckBox()
        Me.chbOpen = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chbComplianceStatus5 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus4 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus3 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus2 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus1 = New System.Windows.Forms.CheckBox()
        Me.TCMonitoringGrids = New System.Windows.Forms.TabControl()
        Me.TPTestReports = New System.Windows.Forms.TabPage()
        Me.dgvTestReportViewer = New Iaip.IaipDataGridView()
        Me.TPNotifications = New System.Windows.Forms.TabPage()
        Me.dgvNotificationLog = New Iaip.IaipDataGridView()
        Me.TPTestFirmComments = New System.Windows.Forms.TabPage()
        Me.dgvTestFirmComments = New Iaip.IaipDataGridView()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SCMonitoringLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCMonitoringLog.Panel1.SuspendLayout()
        Me.SCMonitoringLog.Panel2.SuspendLayout()
        Me.SCMonitoringLog.SuspendLayout()
        Me.GBFilterAndSortOptions.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TCMonitoringSelection.SuspendLayout()
        Me.TPSelectTestReport.SuspendLayout()
        Me.GBSelectedTestReport.SuspendLayout()
        Me.TPSelectTestLog.SuspendLayout()
        Me.TPTestFirmComment.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GBReportType.SuspendLayout()
        Me.GBDateBias.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TCMonitoringGrids.SuspendLayout()
        Me.TPTestReports.SuspendLayout()
        CType(Me.dgvTestReportViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPNotifications.SuspendLayout()
        CType(Me.dgvNotificationLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPTestFirmComments.SuspendLayout()
        CType(Me.dgvTestFirmComments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1016, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiReset})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'mmiReset
        '
        Me.mmiReset.Name = "mmiReset"
        Me.mmiReset.Size = New System.Drawing.Size(133, 22)
        Me.mmiReset.Text = "&Reset Form"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiReports, Me.ExportToExcelToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'mmiReports
        '
        Me.mmiReports.Name = "mmiReports"
        Me.mmiReports.Size = New System.Drawing.Size(173, 22)
        Me.mmiReports.Text = "Open Staff &Reports"
        '
        'ExportToExcelToolStripMenuItem
        '
        Me.ExportToExcelToolStripMenuItem.Name = "ExportToExcelToolStripMenuItem"
        Me.ExportToExcelToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ExportToExcelToolStripMenuItem.Text = "&Export to Excel"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClear, Me.tsbFacilitySearch, Me.tsbResize, Me.tsbExportToExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1016, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbClear
        '
        Me.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClear.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.Text = "Clear"
        '
        'tsbFacilitySearch
        '
        Me.tsbFacilitySearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbFacilitySearch.Image = Global.Iaip.My.Resources.Resources.FindIcon
        Me.tsbFacilitySearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFacilitySearch.Name = "tsbFacilitySearch"
        Me.tsbFacilitySearch.Size = New System.Drawing.Size(23, 22)
        Me.tsbFacilitySearch.Text = "Facility Search Tool"
        '
        'tsbResize
        '
        Me.tsbResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbResize.Image = Global.Iaip.My.Resources.Resources.PanelResizeIcon
        Me.tsbResize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbResize.Name = "tsbResize"
        Me.tsbResize.Size = New System.Drawing.Size(23, 22)
        Me.tsbResize.Text = "Resize"
        '
        'tsbExportToExcel
        '
        Me.tsbExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbExportToExcel.Image = Global.Iaip.My.Resources.Resources.SpreadsheetIcon
        Me.tsbExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExportToExcel.Name = "tsbExportToExcel"
        Me.tsbExportToExcel.Size = New System.Drawing.Size(23, 22)
        Me.tsbExportToExcel.Text = "Export to Excel"
        '
        'SCMonitoringLog
        '
        Me.SCMonitoringLog.BackColor = System.Drawing.Color.Blue
        Me.SCMonitoringLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCMonitoringLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SCMonitoringLog.Location = New System.Drawing.Point(0, 49)
        Me.SCMonitoringLog.Name = "SCMonitoringLog"
        Me.SCMonitoringLog.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCMonitoringLog.Panel1
        '
        Me.SCMonitoringLog.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.SCMonitoringLog.Panel1.Controls.Add(Me.GBFilterAndSortOptions)
        '
        'SCMonitoringLog.Panel2
        '
        Me.SCMonitoringLog.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SCMonitoringLog.Panel2.Controls.Add(Me.TCMonitoringGrids)
        Me.SCMonitoringLog.Size = New System.Drawing.Size(1016, 700)
        Me.SCMonitoringLog.SplitterDistance = 500
        Me.SCMonitoringLog.TabIndex = 2
        '
        'GBFilterAndSortOptions
        '
        Me.GBFilterAndSortOptions.Controls.Add(Me.txtTestFirmCommentsCount)
        Me.GBFilterAndSortOptions.Controls.Add(Me.Label29)
        Me.GBFilterAndSortOptions.Controls.Add(Me.txtNotificationCount)
        Me.GBFilterAndSortOptions.Controls.Add(Me.Label27)
        Me.GBFilterAndSortOptions.Controls.Add(Me.Label13)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox9)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox8)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox7)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox6)
        Me.GBFilterAndSortOptions.Controls.Add(Me.btnRunFilter)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox5)
        Me.GBFilterAndSortOptions.Controls.Add(Me.TCMonitoringSelection)
        Me.GBFilterAndSortOptions.Controls.Add(Me.txtReportCount)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox4)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GBReportType)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GBDateBias)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox3)
        Me.GBFilterAndSortOptions.Controls.Add(Me.GroupBox2)
        Me.GBFilterAndSortOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBFilterAndSortOptions.Location = New System.Drawing.Point(0, 0)
        Me.GBFilterAndSortOptions.Name = "GBFilterAndSortOptions"
        Me.GBFilterAndSortOptions.Size = New System.Drawing.Size(1016, 500)
        Me.GBFilterAndSortOptions.TabIndex = 0
        Me.GBFilterAndSortOptions.TabStop = False
        Me.GBFilterAndSortOptions.Text = "Filter and Sort Options"
        '
        'txtTestFirmCommentsCount
        '
        Me.txtTestFirmCommentsCount.Location = New System.Drawing.Point(695, 121)
        Me.txtTestFirmCommentsCount.Name = "txtTestFirmCommentsCount"
        Me.txtTestFirmCommentsCount.ReadOnly = True
        Me.txtTestFirmCommentsCount.Size = New System.Drawing.Size(43, 20)
        Me.txtTestFirmCommentsCount.TabIndex = 17
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(606, 122)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(81, 13)
        Me.Label29.TabIndex = 290
        Me.Label29.Text = "Test Firm Count"
        '
        'txtNotificationCount
        '
        Me.txtNotificationCount.Location = New System.Drawing.Point(695, 97)
        Me.txtNotificationCount.Name = "txtNotificationCount"
        Me.txtNotificationCount.ReadOnly = True
        Me.txtNotificationCount.Size = New System.Drawing.Size(43, 20)
        Me.txtNotificationCount.TabIndex = 16
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(606, 98)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(91, 13)
        Me.Label27.TabIndex = 288
        Me.Label27.Text = "Notification Count"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(606, 76)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 13)
        Me.Label13.TabIndex = 287
        Me.Label13.Text = "Report Count"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.chbNotificationUnlinked)
        Me.GroupBox9.Controls.Add(Me.chbNotificationLinked)
        Me.GroupBox9.Location = New System.Drawing.Point(609, 16)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(96, 54)
        Me.GroupBox9.TabIndex = 10
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Notification "
        '
        'chbNotificationUnlinked
        '
        Me.chbNotificationUnlinked.Location = New System.Drawing.Point(8, 32)
        Me.chbNotificationUnlinked.Name = "chbNotificationUnlinked"
        Me.chbNotificationUnlinked.Size = New System.Drawing.Size(80, 16)
        Me.chbNotificationUnlinked.TabIndex = 1
        Me.chbNotificationUnlinked.Text = "Unlinked"
        '
        'chbNotificationLinked
        '
        Me.chbNotificationLinked.Location = New System.Drawing.Point(8, 16)
        Me.chbNotificationLinked.Name = "chbNotificationLinked"
        Me.chbNotificationLinked.Size = New System.Drawing.Size(80, 16)
        Me.chbNotificationLinked.TabIndex = 0
        Me.chbNotificationLinked.Text = "Linked"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label28)
        Me.GroupBox8.Controls.Add(Me.txtTestingFirm)
        Me.GroupBox8.Controls.Add(Me.Label12)
        Me.GroupBox8.Controls.Add(Me.txtPollutantFilter)
        Me.GroupBox8.Controls.Add(Me.Label9)
        Me.GroupBox8.Controls.Add(Me.txtCommentFieldFilter)
        Me.GroupBox8.Controls.Add(Me.Label1)
        Me.GroupBox8.Controls.Add(Me.txtEmissionSourceTestedFilter)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.txtCountyFilter)
        Me.GroupBox8.Controls.Add(Me.Label21)
        Me.GroupBox8.Controls.Add(Me.txtFacilityNameFilter)
        Me.GroupBox8.Controls.Add(Me.Label22)
        Me.GroupBox8.Controls.Add(Me.txtCityFilter)
        Me.GroupBox8.Controls.Add(Me.Label23)
        Me.GroupBox8.Controls.Add(Me.txtNotificationNumberFilter)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Controls.Add(Me.txtReferenceNumberFilter)
        Me.GroupBox8.Controls.Add(Me.txtAIRSNumberFilter)
        Me.GroupBox8.Location = New System.Drawing.Point(383, 245)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(355, 250)
        Me.GroupBox8.TabIndex = 9
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Misc. "
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 234)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(64, 13)
        Me.Label28.TabIndex = 19
        Me.Label28.Text = "Testing Firm"
        '
        'txtTestingFirm
        '
        Me.txtTestingFirm.Location = New System.Drawing.Point(136, 230)
        Me.txtTestingFirm.MaxLength = 100
        Me.txtTestingFirm.Name = "txtTestingFirm"
        Me.txtTestingFirm.Size = New System.Drawing.Size(129, 20)
        Me.txtTestingFirm.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 209)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 13)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "Pollutant"
        '
        'txtPollutantFilter
        '
        Me.txtPollutantFilter.Location = New System.Drawing.Point(136, 205)
        Me.txtPollutantFilter.MaxLength = 100
        Me.txtPollutantFilter.Name = "txtPollutantFilter"
        Me.txtPollutantFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtPollutantFilter.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 185)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Comment Field"
        '
        'txtCommentFieldFilter
        '
        Me.txtCommentFieldFilter.Location = New System.Drawing.Point(136, 181)
        Me.txtCommentFieldFilter.MaxLength = 1000
        Me.txtCommentFieldFilter.Name = "txtCommentFieldFilter"
        Me.txtCommentFieldFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtCommentFieldFilter.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Emission Source Tested"
        '
        'txtEmissionSourceTestedFilter
        '
        Me.txtEmissionSourceTestedFilter.Location = New System.Drawing.Point(136, 157)
        Me.txtEmissionSourceTestedFilter.MaxLength = 1000
        Me.txtEmissionSourceTestedFilter.Name = "txtEmissionSourceTestedFilter"
        Me.txtEmissionSourceTestedFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtEmissionSourceTestedFilter.TabIndex = 6
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 137)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(40, 13)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "County"
        '
        'txtCountyFilter
        '
        Me.txtCountyFilter.Location = New System.Drawing.Point(136, 133)
        Me.txtCountyFilter.MaxLength = 1000
        Me.txtCountyFilter.Name = "txtCountyFilter"
        Me.txtCountyFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtCountyFilter.TabIndex = 5
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 41)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(70, 13)
        Me.Label21.TabIndex = 11
        Me.Label21.Text = "Facility Name"
        '
        'txtFacilityNameFilter
        '
        Me.txtFacilityNameFilter.Location = New System.Drawing.Point(136, 37)
        Me.txtFacilityNameFilter.MaxLength = 1000
        Me.txtFacilityNameFilter.Name = "txtFacilityNameFilter"
        Me.txtFacilityNameFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtFacilityNameFilter.TabIndex = 1
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 113)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(24, 13)
        Me.Label22.TabIndex = 14
        Me.Label22.Text = "City"
        '
        'txtCityFilter
        '
        Me.txtCityFilter.Location = New System.Drawing.Point(136, 109)
        Me.txtCityFilter.MaxLength = 1000
        Me.txtCityFilter.Name = "txtCityFilter"
        Me.txtCityFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtCityFilter.TabIndex = 4
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 89)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(89, 13)
        Me.Label23.TabIndex = 13
        Me.Label23.Text = "Test Log Number"
        '
        'txtNotificationNumberFilter
        '
        Me.txtNotificationNumberFilter.Location = New System.Drawing.Point(136, 85)
        Me.txtNotificationNumberFilter.MaxLength = 1000
        Me.txtNotificationNumberFilter.Name = "txtNotificationNumberFilter"
        Me.txtNotificationNumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtNotificationNumberFilter.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 65)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(97, 13)
        Me.Label24.TabIndex = 12
        Me.Label24.Text = "Reference Number"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 17)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(75, 13)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "AIRS Number:"
        '
        'txtReferenceNumberFilter
        '
        Me.txtReferenceNumberFilter.Location = New System.Drawing.Point(136, 61)
        Me.txtReferenceNumberFilter.MaxLength = 1000
        Me.txtReferenceNumberFilter.Name = "txtReferenceNumberFilter"
        Me.txtReferenceNumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtReferenceNumberFilter.TabIndex = 2
        '
        'txtAIRSNumberFilter
        '
        Me.txtAIRSNumberFilter.Location = New System.Drawing.Point(136, 13)
        Me.txtAIRSNumberFilter.MaxLength = 8
        Me.txtAIRSNumberFilter.Name = "txtAIRSNumberFilter"
        Me.txtAIRSNumberFilter.Size = New System.Drawing.Size(129, 20)
        Me.txtAIRSNumberFilter.TabIndex = 0
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.clbWitnessingStaff)
        Me.GroupBox7.Controls.Add(Me.chbWitnessingEngineer)
        Me.GroupBox7.Location = New System.Drawing.Point(7, 329)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(208, 166)
        Me.GroupBox7.TabIndex = 4
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Witnessing Staff"
        '
        'clbWitnessingStaff
        '
        Me.clbWitnessingStaff.CheckOnClick = True
        Me.clbWitnessingStaff.Location = New System.Drawing.Point(8, 34)
        Me.clbWitnessingStaff.Name = "clbWitnessingStaff"
        Me.clbWitnessingStaff.Size = New System.Drawing.Size(186, 124)
        Me.clbWitnessingStaff.TabIndex = 1
        '
        'chbWitnessingEngineer
        '
        Me.chbWitnessingEngineer.AutoSize = True
        Me.chbWitnessingEngineer.Location = New System.Drawing.Point(8, 18)
        Me.chbWitnessingEngineer.Name = "chbWitnessingEngineer"
        Me.chbWitnessingEngineer.Size = New System.Drawing.Size(85, 17)
        Me.chbWitnessingEngineer.TabIndex = 0
        Me.chbWitnessingEngineer.Text = "Current Staff"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.clbEngineer)
        Me.GroupBox6.Controls.Add(Me.chbReviewingEngineer)
        Me.GroupBox6.Location = New System.Drawing.Point(7, 151)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(208, 172)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Reviewing Staff"
        '
        'clbEngineer
        '
        Me.clbEngineer.CheckOnClick = True
        Me.clbEngineer.Location = New System.Drawing.Point(8, 34)
        Me.clbEngineer.Name = "clbEngineer"
        Me.clbEngineer.Size = New System.Drawing.Size(186, 124)
        Me.clbEngineer.TabIndex = 1
        '
        'chbReviewingEngineer
        '
        Me.chbReviewingEngineer.AutoSize = True
        Me.chbReviewingEngineer.Checked = True
        Me.chbReviewingEngineer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbReviewingEngineer.Location = New System.Drawing.Point(8, 18)
        Me.chbReviewingEngineer.Name = "chbReviewingEngineer"
        Me.chbReviewingEngineer.Size = New System.Drawing.Size(85, 17)
        Me.chbReviewingEngineer.TabIndex = 0
        Me.chbReviewingEngineer.Text = "Current Staff"
        '
        'btnRunFilter
        '
        Me.btnRunFilter.Location = New System.Drawing.Point(12, 16)
        Me.btnRunFilter.Name = "btnRunFilter"
        Me.btnRunFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnRunFilter.TabIndex = 0
        Me.btnRunFilter.Text = "Run Filter"
        Me.btnRunFilter.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.AutoSize = True
        Me.GroupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox5.Controls.Add(Me.chbTestFirmComments)
        Me.GroupBox5.Controls.Add(Me.chbNotifications)
        Me.GroupBox5.Controls.Add(Me.chbTestReports)
        Me.GroupBox5.Location = New System.Drawing.Point(7, 51)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(111, 96)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Work Types"
        '
        'chbTestFirmComments
        '
        Me.chbTestFirmComments.AutoSize = True
        Me.chbTestFirmComments.Location = New System.Drawing.Point(6, 60)
        Me.chbTestFirmComments.Name = "chbTestFirmComments"
        Me.chbTestFirmComments.Size = New System.Drawing.Size(99, 17)
        Me.chbTestFirmComments.TabIndex = 2
        Me.chbTestFirmComments.Text = "Test Comments"
        '
        'chbNotifications
        '
        Me.chbNotifications.AutoSize = True
        Me.chbNotifications.Location = New System.Drawing.Point(6, 41)
        Me.chbNotifications.Name = "chbNotifications"
        Me.chbNotifications.Size = New System.Drawing.Size(68, 17)
        Me.chbNotifications.TabIndex = 1
        Me.chbNotifications.Text = "Test Log"
        '
        'chbTestReports
        '
        Me.chbTestReports.AutoSize = True
        Me.chbTestReports.Checked = True
        Me.chbTestReports.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbTestReports.Location = New System.Drawing.Point(6, 22)
        Me.chbTestReports.Name = "chbTestReports"
        Me.chbTestReports.Size = New System.Drawing.Size(87, 17)
        Me.chbTestReports.TabIndex = 0
        Me.chbTestReports.Text = "Test Reports"
        '
        'TCMonitoringSelection
        '
        Me.TCMonitoringSelection.Controls.Add(Me.TPSelectTestReport)
        Me.TCMonitoringSelection.Controls.Add(Me.TPSelectTestLog)
        Me.TCMonitoringSelection.Controls.Add(Me.TPTestFirmComment)
        Me.TCMonitoringSelection.Dock = System.Windows.Forms.DockStyle.Right
        Me.TCMonitoringSelection.Location = New System.Drawing.Point(752, 16)
        Me.TCMonitoringSelection.Name = "TCMonitoringSelection"
        Me.TCMonitoringSelection.SelectedIndex = 0
        Me.TCMonitoringSelection.Size = New System.Drawing.Size(261, 481)
        Me.TCMonitoringSelection.TabIndex = 11
        '
        'TPSelectTestReport
        '
        Me.TPSelectTestReport.Controls.Add(Me.GBSelectedTestReport)
        Me.TPSelectTestReport.Location = New System.Drawing.Point(4, 22)
        Me.TPSelectTestReport.Name = "TPSelectTestReport"
        Me.TPSelectTestReport.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSelectTestReport.Size = New System.Drawing.Size(253, 455)
        Me.TPSelectTestReport.TabIndex = 0
        Me.TPSelectTestReport.Text = "Test Report"
        Me.TPSelectTestReport.UseVisualStyleBackColor = True
        '
        'GBSelectedTestReport
        '
        Me.GBSelectedTestReport.Controls.Add(Me.LLSelectReport)
        Me.GBSelectedTestReport.Controls.Add(Me.txtPollutant)
        Me.GBSelectedTestReport.Controls.Add(Me.Label8)
        Me.GBSelectedTestReport.Controls.Add(Me.txtFacilityCounty)
        Me.GBSelectedTestReport.Controls.Add(Me.Label7)
        Me.GBSelectedTestReport.Controls.Add(Me.txtFacilityCity)
        Me.GBSelectedTestReport.Controls.Add(Me.Label6)
        Me.GBSelectedTestReport.Controls.Add(Me.txtTestType)
        Me.GBSelectedTestReport.Controls.Add(Me.Label5)
        Me.GBSelectedTestReport.Controls.Add(Me.txtAIRSNumber)
        Me.GBSelectedTestReport.Controls.Add(Me.Label4)
        Me.GBSelectedTestReport.Controls.Add(Me.txtFacilityName)
        Me.GBSelectedTestReport.Controls.Add(Me.Label3)
        Me.GBSelectedTestReport.Controls.Add(Me.Label2)
        Me.GBSelectedTestReport.Controls.Add(Me.txtReferenceNumber)
        Me.GBSelectedTestReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBSelectedTestReport.Location = New System.Drawing.Point(3, 3)
        Me.GBSelectedTestReport.Name = "GBSelectedTestReport"
        Me.GBSelectedTestReport.Size = New System.Drawing.Size(247, 449)
        Me.GBSelectedTestReport.TabIndex = 0
        Me.GBSelectedTestReport.TabStop = False
        '
        'LLSelectReport
        '
        Me.LLSelectReport.AutoSize = True
        Me.LLSelectReport.Location = New System.Drawing.Point(128, 40)
        Me.LLSelectReport.Name = "LLSelectReport"
        Me.LLSelectReport.Size = New System.Drawing.Size(72, 13)
        Me.LLSelectReport.TabIndex = 1
        Me.LLSelectReport.TabStop = True
        Me.LLSelectReport.Text = "Select Report"
        '
        'txtPollutant
        '
        Me.txtPollutant.Location = New System.Drawing.Point(24, 304)
        Me.txtPollutant.Name = "txtPollutant"
        Me.txtPollutant.ReadOnly = True
        Me.txtPollutant.Size = New System.Drawing.Size(208, 20)
        Me.txtPollutant.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 288)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 13)
        Me.Label8.TabIndex = 246
        Me.Label8.Text = "Pollutant"
        '
        'txtFacilityCounty
        '
        Me.txtFacilityCounty.Location = New System.Drawing.Point(24, 264)
        Me.txtFacilityCounty.Name = "txtFacilityCounty"
        Me.txtFacilityCounty.ReadOnly = True
        Me.txtFacilityCounty.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityCounty.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 248)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 244
        Me.Label7.Text = "Facility County"
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.Location = New System.Drawing.Point(24, 224)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.ReadOnly = True
        Me.txtFacilityCity.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityCity.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 208)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 242
        Me.Label6.Text = "Facility City"
        '
        'txtTestType
        '
        Me.txtTestType.Location = New System.Drawing.Point(24, 160)
        Me.txtTestType.Name = "txtTestType"
        Me.txtTestType.ReadOnly = True
        Me.txtTestType.Size = New System.Drawing.Size(208, 20)
        Me.txtTestType.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 240
        Me.Label5.Text = "Test Type"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(24, 120)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtAIRSNumber.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 238
        Me.Label4.Text = "AIRS Number"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(24, 80)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 236
        Me.Label3.Text = "Facility Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 234
        Me.Label2.Text = "Reference Number"
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(24, 40)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtReferenceNumber.TabIndex = 0
        '
        'TPSelectTestLog
        '
        Me.TPSelectTestLog.Controls.Add(Me.llbSelectTestLog)
        Me.TPSelectTestLog.Controls.Add(Me.txtNotificationTestStart)
        Me.TPSelectTestLog.Controls.Add(Me.Label14)
        Me.TPSelectTestLog.Controls.Add(Me.txtNotificationCounty)
        Me.TPSelectTestLog.Controls.Add(Me.Label15)
        Me.TPSelectTestLog.Controls.Add(Me.txtNotificationCity)
        Me.TPSelectTestLog.Controls.Add(Me.Label16)
        Me.TPSelectTestLog.Controls.Add(Me.txtNotificationEmissionUnit)
        Me.TPSelectTestLog.Controls.Add(Me.Label17)
        Me.TPSelectTestLog.Controls.Add(Me.txtNotificationAIRSNumber)
        Me.TPSelectTestLog.Controls.Add(Me.Label18)
        Me.TPSelectTestLog.Controls.Add(Me.txtNotificationFacilityName)
        Me.TPSelectTestLog.Controls.Add(Me.Label19)
        Me.TPSelectTestLog.Controls.Add(Me.Label20)
        Me.TPSelectTestLog.Controls.Add(Me.txtTestLogNumber)
        Me.TPSelectTestLog.Location = New System.Drawing.Point(4, 22)
        Me.TPSelectTestLog.Name = "TPSelectTestLog"
        Me.TPSelectTestLog.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSelectTestLog.Size = New System.Drawing.Size(253, 455)
        Me.TPSelectTestLog.TabIndex = 1
        Me.TPSelectTestLog.Text = "Test Log"
        Me.TPSelectTestLog.UseVisualStyleBackColor = True
        '
        'llbSelectTestLog
        '
        Me.llbSelectTestLog.AutoSize = True
        Me.llbSelectTestLog.Location = New System.Drawing.Point(126, 26)
        Me.llbSelectTestLog.Name = "llbSelectTestLog"
        Me.llbSelectTestLog.Size = New System.Drawing.Size(93, 26)
        Me.llbSelectTestLog.TabIndex = 1
        Me.llbSelectTestLog.TabStop = True
        Me.llbSelectTestLog.Text = "Select Notification" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    (Add New) "
        '
        'txtNotificationTestStart
        '
        Me.txtNotificationTestStart.Location = New System.Drawing.Point(22, 290)
        Me.txtNotificationTestStart.Name = "txtNotificationTestStart"
        Me.txtNotificationTestStart.ReadOnly = True
        Me.txtNotificationTestStart.Size = New System.Drawing.Size(208, 20)
        Me.txtNotificationTestStart.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 274)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(79, 13)
        Me.Label14.TabIndex = 261
        Me.Label14.Text = "Test Date Start"
        '
        'txtNotificationCounty
        '
        Me.txtNotificationCounty.Location = New System.Drawing.Point(22, 250)
        Me.txtNotificationCounty.Name = "txtNotificationCounty"
        Me.txtNotificationCounty.ReadOnly = True
        Me.txtNotificationCounty.Size = New System.Drawing.Size(208, 20)
        Me.txtNotificationCounty.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 234)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 13)
        Me.Label15.TabIndex = 259
        Me.Label15.Text = "Facility County"
        '
        'txtNotificationCity
        '
        Me.txtNotificationCity.Location = New System.Drawing.Point(22, 210)
        Me.txtNotificationCity.Name = "txtNotificationCity"
        Me.txtNotificationCity.ReadOnly = True
        Me.txtNotificationCity.Size = New System.Drawing.Size(208, 20)
        Me.txtNotificationCity.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 194)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 13)
        Me.Label16.TabIndex = 257
        Me.Label16.Text = "Facility City"
        '
        'txtNotificationEmissionUnit
        '
        Me.txtNotificationEmissionUnit.Location = New System.Drawing.Point(22, 146)
        Me.txtNotificationEmissionUnit.Name = "txtNotificationEmissionUnit"
        Me.txtNotificationEmissionUnit.ReadOnly = True
        Me.txtNotificationEmissionUnit.Size = New System.Drawing.Size(208, 20)
        Me.txtNotificationEmissionUnit.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 130)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 13)
        Me.Label17.TabIndex = 255
        Me.Label17.Text = "Emission Unit"
        '
        'txtNotificationAIRSNumber
        '
        Me.txtNotificationAIRSNumber.Location = New System.Drawing.Point(22, 106)
        Me.txtNotificationAIRSNumber.Name = "txtNotificationAIRSNumber"
        Me.txtNotificationAIRSNumber.ReadOnly = True
        Me.txtNotificationAIRSNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtNotificationAIRSNumber.TabIndex = 3
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 90)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 13)
        Me.Label18.TabIndex = 253
        Me.Label18.Text = "AIRS Number"
        '
        'txtNotificationFacilityName
        '
        Me.txtNotificationFacilityName.Location = New System.Drawing.Point(22, 66)
        Me.txtNotificationFacilityName.Name = "txtNotificationFacilityName"
        Me.txtNotificationFacilityName.ReadOnly = True
        Me.txtNotificationFacilityName.Size = New System.Drawing.Size(208, 20)
        Me.txtNotificationFacilityName.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 50)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(70, 13)
        Me.Label19.TabIndex = 251
        Me.Label19.Text = "Facility Name"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(89, 13)
        Me.Label20.TabIndex = 249
        Me.Label20.Text = "Test Log Number"
        '
        'txtTestLogNumber
        '
        Me.txtTestLogNumber.Location = New System.Drawing.Point(22, 26)
        Me.txtTestLogNumber.Name = "txtTestLogNumber"
        Me.txtTestLogNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtTestLogNumber.TabIndex = 0
        '
        'TPTestFirmComment
        '
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmName)
        Me.TPTestFirmComment.Controls.Add(Me.Label38)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmTestLogNumber)
        Me.TPTestFirmComment.Controls.Add(Me.Label37)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmReferenceNumber)
        Me.TPTestFirmComment.Controls.Add(Me.Label30)
        Me.TPTestFirmComment.Controls.Add(Me.llbOpenComments)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmCounty)
        Me.TPTestFirmComment.Controls.Add(Me.Label31)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmFacilityCity)
        Me.TPTestFirmComment.Controls.Add(Me.Label32)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmCommentType)
        Me.TPTestFirmComment.Controls.Add(Me.Label33)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmAirsNumber)
        Me.TPTestFirmComment.Controls.Add(Me.Label34)
        Me.TPTestFirmComment.Controls.Add(Me.txtTestFirmFacilityName)
        Me.TPTestFirmComment.Controls.Add(Me.Label35)
        Me.TPTestFirmComment.Controls.Add(Me.Label36)
        Me.TPTestFirmComment.Controls.Add(Me.txtCommentNumber)
        Me.TPTestFirmComment.Location = New System.Drawing.Point(4, 22)
        Me.TPTestFirmComment.Name = "TPTestFirmComment"
        Me.TPTestFirmComment.Size = New System.Drawing.Size(253, 455)
        Me.TPTestFirmComment.TabIndex = 2
        Me.TPTestFirmComment.Text = "Test Firm Comments"
        Me.TPTestFirmComment.UseVisualStyleBackColor = True
        '
        'txtTestFirmName
        '
        Me.txtTestFirmName.Location = New System.Drawing.Point(24, 159)
        Me.txtTestFirmName.Name = "txtTestFirmName"
        Me.txtTestFirmName.ReadOnly = True
        Me.txtTestFirmName.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmName.TabIndex = 4
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(8, 143)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(64, 13)
        Me.Label38.TabIndex = 268
        Me.Label38.Text = "Testing Firm"
        '
        'txtTestFirmTestLogNumber
        '
        Me.txtTestFirmTestLogNumber.Location = New System.Drawing.Point(24, 361)
        Me.txtTestFirmTestLogNumber.Name = "txtTestFirmTestLogNumber"
        Me.txtTestFirmTestLogNumber.ReadOnly = True
        Me.txtTestFirmTestLogNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmTestLogNumber.TabIndex = 9
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(8, 345)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(89, 13)
        Me.Label37.TabIndex = 266
        Me.Label37.Text = "Test Log Number"
        '
        'txtTestFirmReferenceNumber
        '
        Me.txtTestFirmReferenceNumber.Location = New System.Drawing.Point(24, 320)
        Me.txtTestFirmReferenceNumber.Name = "txtTestFirmReferenceNumber"
        Me.txtTestFirmReferenceNumber.ReadOnly = True
        Me.txtTestFirmReferenceNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmReferenceNumber.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(8, 304)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(97, 13)
        Me.Label30.TabIndex = 264
        Me.Label30.Text = "Reference Number"
        '
        'llbOpenComments
        '
        Me.llbOpenComments.AutoSize = True
        Me.llbOpenComments.Location = New System.Drawing.Point(128, 40)
        Me.llbOpenComments.Name = "llbOpenComments"
        Me.llbOpenComments.Size = New System.Drawing.Size(84, 26)
        Me.llbOpenComments.TabIndex = 1
        Me.llbOpenComments.TabStop = True
        Me.llbOpenComments.Text = "Select Comment" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    (Add New)"
        '
        'txtTestFirmCounty
        '
        Me.txtTestFirmCounty.Location = New System.Drawing.Point(24, 281)
        Me.txtTestFirmCounty.Name = "txtTestFirmCounty"
        Me.txtTestFirmCounty.ReadOnly = True
        Me.txtTestFirmCounty.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmCounty.TabIndex = 7
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(8, 265)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(75, 13)
        Me.Label31.TabIndex = 259
        Me.Label31.Text = "Facility County"
        '
        'txtTestFirmFacilityCity
        '
        Me.txtTestFirmFacilityCity.Location = New System.Drawing.Point(24, 241)
        Me.txtTestFirmFacilityCity.Name = "txtTestFirmFacilityCity"
        Me.txtTestFirmFacilityCity.ReadOnly = True
        Me.txtTestFirmFacilityCity.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmFacilityCity.TabIndex = 6
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(8, 225)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(59, 13)
        Me.Label32.TabIndex = 257
        Me.Label32.Text = "Facility City"
        '
        'txtTestFirmCommentType
        '
        Me.txtTestFirmCommentType.Location = New System.Drawing.Point(24, 198)
        Me.txtTestFirmCommentType.Name = "txtTestFirmCommentType"
        Me.txtTestFirmCommentType.ReadOnly = True
        Me.txtTestFirmCommentType.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmCommentType.TabIndex = 5
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(8, 182)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(78, 13)
        Me.Label33.TabIndex = 255
        Me.Label33.Text = "Comment Type"
        '
        'txtTestFirmAirsNumber
        '
        Me.txtTestFirmAirsNumber.Location = New System.Drawing.Point(24, 118)
        Me.txtTestFirmAirsNumber.Name = "txtTestFirmAirsNumber"
        Me.txtTestFirmAirsNumber.ReadOnly = True
        Me.txtTestFirmAirsNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmAirsNumber.TabIndex = 3
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(8, 102)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(72, 13)
        Me.Label34.TabIndex = 253
        Me.Label34.Text = "AIRS Number"
        '
        'txtTestFirmFacilityName
        '
        Me.txtTestFirmFacilityName.Location = New System.Drawing.Point(24, 78)
        Me.txtTestFirmFacilityName.Name = "txtTestFirmFacilityName"
        Me.txtTestFirmFacilityName.ReadOnly = True
        Me.txtTestFirmFacilityName.Size = New System.Drawing.Size(208, 20)
        Me.txtTestFirmFacilityName.TabIndex = 2
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(8, 62)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(70, 13)
        Me.Label35.TabIndex = 251
        Me.Label35.Text = "Facility Name"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(8, 24)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(91, 13)
        Me.Label36.TabIndex = 249
        Me.Label36.Text = "Comment Number"
        '
        'txtCommentNumber
        '
        Me.txtCommentNumber.Location = New System.Drawing.Point(24, 40)
        Me.txtCommentNumber.Name = "txtCommentNumber"
        Me.txtCommentNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtCommentNumber.TabIndex = 0
        '
        'txtReportCount
        '
        Me.txtReportCount.Location = New System.Drawing.Point(695, 73)
        Me.txtReportCount.Name = "txtReportCount"
        Me.txtReportCount.ReadOnly = True
        Me.txtReportCount.Size = New System.Drawing.Size(43, 20)
        Me.txtReportCount.TabIndex = 15
        '
        'GroupBox4
        '
        Me.GroupBox4.AutoSize = True
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.chbAll)
        Me.GroupBox4.Controls.Add(Me.chbPTE)
        Me.GroupBox4.Controls.Add(Me.chbMethod22)
        Me.GroupBox4.Controls.Add(Me.chbMethod9Single)
        Me.GroupBox4.Controls.Add(Me.chbMethod9Multi)
        Me.GroupBox4.Controls.Add(Me.chbMemorandumToFile)
        Me.GroupBox4.Controls.Add(Me.chbMemorandumStandard)
        Me.GroupBox4.Controls.Add(Me.chbRata)
        Me.GroupBox4.Controls.Add(Me.chbFlare)
        Me.GroupBox4.Controls.Add(Me.chbGasConcentration)
        Me.GroupBox4.Controls.Add(Me.chbPondTreatment)
        Me.GroupBox4.Controls.Add(Me.chbLoadingRack)
        Me.GroupBox4.Controls.Add(Me.chbTwoStackDRE)
        Me.GroupBox4.Controls.Add(Me.chbTwoStackStandard)
        Me.GroupBox4.Controls.Add(Me.chbOneStackFourRun)
        Me.GroupBox4.Controls.Add(Me.chbOneStackThreeRun)
        Me.GroupBox4.Controls.Add(Me.chbOneStackTwoRun)
        Me.GroupBox4.Controls.Add(Me.chbUnassigned)
        Me.GroupBox4.Location = New System.Drawing.Point(221, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(156, 342)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Document Type"
        '
        'chbAll
        '
        Me.chbAll.AutoSize = True
        Me.chbAll.Location = New System.Drawing.Point(8, 16)
        Me.chbAll.Name = "chbAll"
        Me.chbAll.Size = New System.Drawing.Size(121, 17)
        Me.chbAll.TabIndex = 0
        Me.chbAll.Text = "All Document Types"
        '
        'chbPTE
        '
        Me.chbPTE.AutoSize = True
        Me.chbPTE.Location = New System.Drawing.Point(8, 238)
        Me.chbPTE.Name = "chbPTE"
        Me.chbPTE.Size = New System.Drawing.Size(47, 17)
        Me.chbPTE.TabIndex = 14
        Me.chbPTE.Text = "PTE"
        '
        'chbMethod22
        '
        Me.chbMethod22.AutoSize = True
        Me.chbMethod22.Location = New System.Drawing.Point(8, 152)
        Me.chbMethod22.Name = "chbMethod22"
        Me.chbMethod22.Size = New System.Drawing.Size(77, 17)
        Me.chbMethod22.TabIndex = 8
        Me.chbMethod22.Text = "Method 22"
        '
        'chbMethod9Single
        '
        Me.chbMethod9Single.AutoSize = True
        Me.chbMethod9Single.Location = New System.Drawing.Point(8, 135)
        Me.chbMethod9Single.Name = "chbMethod9Single"
        Me.chbMethod9Single.Size = New System.Drawing.Size(109, 17)
        Me.chbMethod9Single.TabIndex = 7
        Me.chbMethod9Single.Text = "Method 9 (Single)"
        '
        'chbMethod9Multi
        '
        Me.chbMethod9Multi.AutoSize = True
        Me.chbMethod9Multi.Location = New System.Drawing.Point(8, 118)
        Me.chbMethod9Multi.Name = "chbMethod9Multi"
        Me.chbMethod9Multi.Size = New System.Drawing.Size(105, 17)
        Me.chbMethod9Multi.TabIndex = 6
        Me.chbMethod9Multi.Text = "Method 9 (Multi.)"
        '
        'chbMemorandumToFile
        '
        Me.chbMemorandumToFile.AutoSize = True
        Me.chbMemorandumToFile.Location = New System.Drawing.Point(8, 101)
        Me.chbMemorandumToFile.Name = "chbMemorandumToFile"
        Me.chbMemorandumToFile.Size = New System.Drawing.Size(131, 17)
        Me.chbMemorandumToFile.TabIndex = 5
        Me.chbMemorandumToFile.Text = "Memorandum (To File)"
        '
        'chbMemorandumStandard
        '
        Me.chbMemorandumStandard.AutoSize = True
        Me.chbMemorandumStandard.Location = New System.Drawing.Point(8, 84)
        Me.chbMemorandumStandard.Name = "chbMemorandumStandard"
        Me.chbMemorandumStandard.Size = New System.Drawing.Size(142, 17)
        Me.chbMemorandumStandard.TabIndex = 4
        Me.chbMemorandumStandard.Text = "Memorandum (Standard)"
        '
        'chbRata
        '
        Me.chbRata.AutoSize = True
        Me.chbRata.Location = New System.Drawing.Point(8, 255)
        Me.chbRata.Name = "chbRata"
        Me.chbRata.Size = New System.Drawing.Size(49, 17)
        Me.chbRata.TabIndex = 15
        Me.chbRata.Text = "Rata"
        '
        'chbFlare
        '
        Me.chbFlare.AutoSize = True
        Me.chbFlare.Location = New System.Drawing.Point(8, 50)
        Me.chbFlare.Name = "chbFlare"
        Me.chbFlare.Size = New System.Drawing.Size(49, 17)
        Me.chbFlare.TabIndex = 2
        Me.chbFlare.Text = "Flare"
        '
        'chbGasConcentration
        '
        Me.chbGasConcentration.AutoSize = True
        Me.chbGasConcentration.Location = New System.Drawing.Point(8, 33)
        Me.chbGasConcentration.Name = "chbGasConcentration"
        Me.chbGasConcentration.Size = New System.Drawing.Size(114, 17)
        Me.chbGasConcentration.TabIndex = 1
        Me.chbGasConcentration.Text = "Gas Concentration"
        '
        'chbPondTreatment
        '
        Me.chbPondTreatment.AutoSize = True
        Me.chbPondTreatment.Location = New System.Drawing.Point(8, 221)
        Me.chbPondTreatment.Name = "chbPondTreatment"
        Me.chbPondTreatment.Size = New System.Drawing.Size(102, 17)
        Me.chbPondTreatment.TabIndex = 13
        Me.chbPondTreatment.Text = "Pond Treatment"
        '
        'chbLoadingRack
        '
        Me.chbLoadingRack.AutoSize = True
        Me.chbLoadingRack.Location = New System.Drawing.Point(8, 67)
        Me.chbLoadingRack.Name = "chbLoadingRack"
        Me.chbLoadingRack.Size = New System.Drawing.Size(93, 17)
        Me.chbLoadingRack.TabIndex = 3
        Me.chbLoadingRack.Text = "Loading Rack"
        '
        'chbTwoStackDRE
        '
        Me.chbTwoStackDRE.AutoSize = True
        Me.chbTwoStackDRE.Location = New System.Drawing.Point(8, 289)
        Me.chbTwoStackDRE.Name = "chbTwoStackDRE"
        Me.chbTwoStackDRE.Size = New System.Drawing.Size(110, 17)
        Me.chbTwoStackDRE.TabIndex = 17
        Me.chbTwoStackDRE.Text = "Two Stack (DRE)"
        '
        'chbTwoStackStandard
        '
        Me.chbTwoStackStandard.AutoSize = True
        Me.chbTwoStackStandard.Location = New System.Drawing.Point(8, 272)
        Me.chbTwoStackStandard.Name = "chbTwoStackStandard"
        Me.chbTwoStackStandard.Size = New System.Drawing.Size(130, 17)
        Me.chbTwoStackStandard.TabIndex = 16
        Me.chbTwoStackStandard.Text = "Two Stack (Standard)"
        '
        'chbOneStackFourRun
        '
        Me.chbOneStackFourRun.AutoSize = True
        Me.chbOneStackFourRun.Location = New System.Drawing.Point(8, 203)
        Me.chbOneStackFourRun.Name = "chbOneStackFourRun"
        Me.chbOneStackFourRun.Size = New System.Drawing.Size(135, 17)
        Me.chbOneStackFourRun.TabIndex = 11
        Me.chbOneStackFourRun.Text = "One Stack (Four Runs)"
        '
        'chbOneStackThreeRun
        '
        Me.chbOneStackThreeRun.AutoSize = True
        Me.chbOneStackThreeRun.Location = New System.Drawing.Point(8, 186)
        Me.chbOneStackThreeRun.Name = "chbOneStackThreeRun"
        Me.chbOneStackThreeRun.Size = New System.Drawing.Size(142, 17)
        Me.chbOneStackThreeRun.TabIndex = 10
        Me.chbOneStackThreeRun.Text = "One Stack (Three Runs)"
        '
        'chbOneStackTwoRun
        '
        Me.chbOneStackTwoRun.AutoSize = True
        Me.chbOneStackTwoRun.Location = New System.Drawing.Point(8, 169)
        Me.chbOneStackTwoRun.Name = "chbOneStackTwoRun"
        Me.chbOneStackTwoRun.Size = New System.Drawing.Size(135, 17)
        Me.chbOneStackTwoRun.TabIndex = 9
        Me.chbOneStackTwoRun.Text = "One Stack (Two Runs)"
        '
        'chbUnassigned
        '
        Me.chbUnassigned.AutoSize = True
        Me.chbUnassigned.Location = New System.Drawing.Point(8, 306)
        Me.chbUnassigned.Name = "chbUnassigned"
        Me.chbUnassigned.Size = New System.Drawing.Size(82, 17)
        Me.chbUnassigned.TabIndex = 18
        Me.chbUnassigned.Text = "Unassigned"
        '
        'GBReportType
        '
        Me.GBReportType.Controls.Add(Me.chbOther)
        Me.GBReportType.Controls.Add(Me.chbSourceTest)
        Me.GBReportType.Controls.Add(Me.chbRATAandCEMS)
        Me.GBReportType.Controls.Add(Me.chbPEMSDevelopment)
        Me.GBReportType.Controls.Add(Me.chbMonitorCertification)
        Me.GBReportType.Location = New System.Drawing.Point(221, 380)
        Me.GBReportType.Name = "GBReportType"
        Me.GBReportType.Size = New System.Drawing.Size(156, 115)
        Me.GBReportType.TabIndex = 6
        Me.GBReportType.TabStop = False
        Me.GBReportType.Text = "Report Type"
        '
        'chbOther
        '
        Me.chbOther.Location = New System.Drawing.Point(8, 81)
        Me.chbOther.Name = "chbOther"
        Me.chbOther.Size = New System.Drawing.Size(136, 16)
        Me.chbOther.TabIndex = 4
        Me.chbOther.Text = "Other"
        '
        'chbSourceTest
        '
        Me.chbSourceTest.Location = New System.Drawing.Point(8, 64)
        Me.chbSourceTest.Name = "chbSourceTest"
        Me.chbSourceTest.Size = New System.Drawing.Size(136, 16)
        Me.chbSourceTest.TabIndex = 3
        Me.chbSourceTest.Text = "SOURCE TEST"
        '
        'chbRATAandCEMS
        '
        Me.chbRATAandCEMS.Location = New System.Drawing.Point(8, 48)
        Me.chbRATAandCEMS.Name = "chbRATAandCEMS"
        Me.chbRATAandCEMS.Size = New System.Drawing.Size(136, 16)
        Me.chbRATAandCEMS.TabIndex = 2
        Me.chbRATAandCEMS.Text = "RATA/CEMS "
        '
        'chbPEMSDevelopment
        '
        Me.chbPEMSDevelopment.Location = New System.Drawing.Point(8, 32)
        Me.chbPEMSDevelopment.Name = "chbPEMSDevelopment"
        Me.chbPEMSDevelopment.Size = New System.Drawing.Size(136, 16)
        Me.chbPEMSDevelopment.TabIndex = 1
        Me.chbPEMSDevelopment.Text = "PEMS Development "
        '
        'chbMonitorCertification
        '
        Me.chbMonitorCertification.Location = New System.Drawing.Point(8, 16)
        Me.chbMonitorCertification.Name = "chbMonitorCertification"
        Me.chbMonitorCertification.Size = New System.Drawing.Size(136, 16)
        Me.chbMonitorCertification.TabIndex = 0
        Me.chbMonitorCertification.Text = "Monitor Certification"
        '
        'GBDateBias
        '
        Me.GBDateBias.Controls.Add(Me.rdbNA)
        Me.GBDateBias.Controls.Add(Me.Label11)
        Me.GBDateBias.Controls.Add(Me.Label10)
        Me.GBDateBias.Controls.Add(Me.DTPEndDate)
        Me.GBDateBias.Controls.Add(Me.DTPStartDate)
        Me.GBDateBias.Controls.Add(Me.rdbFacilityDateCompleted)
        Me.GBDateBias.Controls.Add(Me.rdbFacilityDateTested)
        Me.GBDateBias.Controls.Add(Me.rdbFacilityDateReceived)
        Me.GBDateBias.Location = New System.Drawing.Point(383, 16)
        Me.GBDateBias.Name = "GBDateBias"
        Me.GBDateBias.Size = New System.Drawing.Size(220, 120)
        Me.GBDateBias.TabIndex = 7
        Me.GBDateBias.TabStop = False
        Me.GBDateBias.Text = "Date Bias"
        '
        'rdbNA
        '
        Me.rdbNA.Checked = True
        Me.rdbNA.Location = New System.Drawing.Point(8, 16)
        Me.rdbNA.Name = "rdbNA"
        Me.rdbNA.Size = New System.Drawing.Size(136, 16)
        Me.rdbNA.TabIndex = 0
        Me.rdbNA.TabStop = True
        Me.rdbNA.Text = "N/A"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(109, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 13)
        Me.Label11.TabIndex = 239
        Me.Label11.Text = "End Date"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 13)
        Me.Label10.TabIndex = 238
        Me.Label10.Text = "Start Date"
        '
        'DTPEndDate
        '
        Me.DTPEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEndDate.Location = New System.Drawing.Point(112, 96)
        Me.DTPEndDate.Name = "DTPEndDate"
        Me.DTPEndDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEndDate.TabIndex = 5
        '
        'DTPStartDate
        '
        Me.DTPStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartDate.Location = New System.Drawing.Point(8, 96)
        Me.DTPStartDate.Name = "DTPStartDate"
        Me.DTPStartDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPStartDate.TabIndex = 4
        '
        'rdbFacilityDateCompleted
        '
        Me.rdbFacilityDateCompleted.Location = New System.Drawing.Point(8, 64)
        Me.rdbFacilityDateCompleted.Name = "rdbFacilityDateCompleted"
        Me.rdbFacilityDateCompleted.Size = New System.Drawing.Size(144, 16)
        Me.rdbFacilityDateCompleted.TabIndex = 3
        Me.rdbFacilityDateCompleted.Text = "Date Report Completed"
        '
        'rdbFacilityDateTested
        '
        Me.rdbFacilityDateTested.Location = New System.Drawing.Point(8, 32)
        Me.rdbFacilityDateTested.Name = "rdbFacilityDateTested"
        Me.rdbFacilityDateTested.Size = New System.Drawing.Size(136, 16)
        Me.rdbFacilityDateTested.TabIndex = 1
        Me.rdbFacilityDateTested.Text = "Date Tested"
        '
        'rdbFacilityDateReceived
        '
        Me.rdbFacilityDateReceived.Location = New System.Drawing.Point(8, 48)
        Me.rdbFacilityDateReceived.Name = "rdbFacilityDateReceived"
        Me.rdbFacilityDateReceived.Size = New System.Drawing.Size(104, 16)
        Me.rdbFacilityDateReceived.TabIndex = 2
        Me.rdbFacilityDateReceived.Text = "Date Received"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chbClosed)
        Me.GroupBox3.Controls.Add(Me.chbOpen)
        Me.GroupBox3.Location = New System.Drawing.Point(128, 55)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(87, 90)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Open/Closed"
        '
        'chbClosed
        '
        Me.chbClosed.Location = New System.Drawing.Point(8, 32)
        Me.chbClosed.Name = "chbClosed"
        Me.chbClosed.Size = New System.Drawing.Size(80, 16)
        Me.chbClosed.TabIndex = 1
        Me.chbClosed.Text = "Closed"
        '
        'chbOpen
        '
        Me.chbOpen.Checked = True
        Me.chbOpen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbOpen.Location = New System.Drawing.Point(8, 16)
        Me.chbOpen.Name = "chbOpen"
        Me.chbOpen.Size = New System.Drawing.Size(80, 16)
        Me.chbOpen.TabIndex = 0
        Me.chbOpen.Text = "Open"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chbComplianceStatus5)
        Me.GroupBox2.Controls.Add(Me.chbComplianceStatus4)
        Me.GroupBox2.Controls.Add(Me.chbComplianceStatus3)
        Me.GroupBox2.Controls.Add(Me.chbComplianceStatus2)
        Me.GroupBox2.Controls.Add(Me.chbComplianceStatus1)
        Me.GroupBox2.Location = New System.Drawing.Point(383, 142)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(173, 100)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Compliance Status"
        '
        'chbComplianceStatus5
        '
        Me.chbComplianceStatus5.AutoSize = True
        Me.chbComplianceStatus5.Location = New System.Drawing.Point(8, 80)
        Me.chbComplianceStatus5.Name = "chbComplianceStatus5"
        Me.chbComplianceStatus5.Size = New System.Drawing.Size(113, 17)
        Me.chbComplianceStatus5.TabIndex = 4
        Me.chbComplianceStatus5.Text = "Not In Compliance"
        '
        'chbComplianceStatus4
        '
        Me.chbComplianceStatus4.AutoSize = True
        Me.chbComplianceStatus4.Location = New System.Drawing.Point(8, 64)
        Me.chbComplianceStatus4.Name = "chbComplianceStatus4"
        Me.chbComplianceStatus4.Size = New System.Drawing.Size(90, 17)
        Me.chbComplianceStatus4.TabIndex = 3
        Me.chbComplianceStatus4.Text = "Indeterminate"
        '
        'chbComplianceStatus3
        '
        Me.chbComplianceStatus3.AutoSize = True
        Me.chbComplianceStatus3.Location = New System.Drawing.Point(8, 48)
        Me.chbComplianceStatus3.Name = "chbComplianceStatus3"
        Me.chbComplianceStatus3.Size = New System.Drawing.Size(93, 17)
        Me.chbComplianceStatus3.TabIndex = 2
        Me.chbComplianceStatus3.Text = "In Compliance"
        '
        'chbComplianceStatus2
        '
        Me.chbComplianceStatus2.AutoSize = True
        Me.chbComplianceStatus2.Location = New System.Drawing.Point(8, 32)
        Me.chbComplianceStatus2.Name = "chbComplianceStatus2"
        Me.chbComplianceStatus2.Size = New System.Drawing.Size(162, 17)
        Me.chbComplianceStatus2.TabIndex = 1
        Me.chbComplianceStatus2.Text = "For Information Purpose Only"
        '
        'chbComplianceStatus1
        '
        Me.chbComplianceStatus1.AutoSize = True
        Me.chbComplianceStatus1.Location = New System.Drawing.Point(8, 16)
        Me.chbComplianceStatus1.Name = "chbComplianceStatus1"
        Me.chbComplianceStatus1.Size = New System.Drawing.Size(71, 17)
        Me.chbComplianceStatus1.TabIndex = 0
        Me.chbComplianceStatus1.Text = "File Open"
        '
        'TCMonitoringGrids
        '
        Me.TCMonitoringGrids.Controls.Add(Me.TPTestReports)
        Me.TCMonitoringGrids.Controls.Add(Me.TPNotifications)
        Me.TCMonitoringGrids.Controls.Add(Me.TPTestFirmComments)
        Me.TCMonitoringGrids.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMonitoringGrids.Location = New System.Drawing.Point(0, 0)
        Me.TCMonitoringGrids.Name = "TCMonitoringGrids"
        Me.TCMonitoringGrids.SelectedIndex = 0
        Me.TCMonitoringGrids.Size = New System.Drawing.Size(1016, 196)
        Me.TCMonitoringGrids.TabIndex = 0
        '
        'TPTestReports
        '
        Me.TPTestReports.Controls.Add(Me.dgvTestReportViewer)
        Me.TPTestReports.Location = New System.Drawing.Point(4, 22)
        Me.TPTestReports.Name = "TPTestReports"
        Me.TPTestReports.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTestReports.Size = New System.Drawing.Size(1008, 170)
        Me.TPTestReports.TabIndex = 0
        Me.TPTestReports.Text = "Test Reports"
        Me.TPTestReports.UseVisualStyleBackColor = True
        '
        'dgvTestReportViewer
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvTestReportViewer.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTestReportViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTestReportViewer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTestReportViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTestReportViewer.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTestReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestReportViewer.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvTestReportViewer.LinkifyColumnByName = Nothing
        Me.dgvTestReportViewer.LinkifyFirstColumn = True
        Me.dgvTestReportViewer.Location = New System.Drawing.Point(3, 3)
        Me.dgvTestReportViewer.Name = "dgvTestReportViewer"
        Me.dgvTestReportViewer.ResultsCountLabel = Nothing
        Me.dgvTestReportViewer.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTestReportViewer.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTestReportViewer.Size = New System.Drawing.Size(1002, 164)
        Me.dgvTestReportViewer.StandardTab = True
        Me.dgvTestReportViewer.TabIndex = 0
        '
        'TPNotifications
        '
        Me.TPNotifications.Controls.Add(Me.dgvNotificationLog)
        Me.TPNotifications.Location = New System.Drawing.Point(4, 22)
        Me.TPNotifications.Name = "TPNotifications"
        Me.TPNotifications.Padding = New System.Windows.Forms.Padding(3)
        Me.TPNotifications.Size = New System.Drawing.Size(1008, 170)
        Me.TPNotifications.TabIndex = 1
        Me.TPNotifications.Text = "Test Notifications"
        Me.TPNotifications.UseVisualStyleBackColor = True
        '
        'dgvNotificationLog
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvNotificationLog.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvNotificationLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNotificationLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvNotificationLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNotificationLog.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvNotificationLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvNotificationLog.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvNotificationLog.LinkifyColumnByName = "strTestLogNumber"
        Me.dgvNotificationLog.Location = New System.Drawing.Point(3, 3)
        Me.dgvNotificationLog.Name = "dgvNotificationLog"
        Me.dgvNotificationLog.ResultsCountLabel = Nothing
        Me.dgvNotificationLog.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNotificationLog.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvNotificationLog.Size = New System.Drawing.Size(1002, 164)
        Me.dgvNotificationLog.StandardTab = True
        Me.dgvNotificationLog.TabIndex = 1
        '
        'TPTestFirmComments
        '
        Me.TPTestFirmComments.Controls.Add(Me.dgvTestFirmComments)
        Me.TPTestFirmComments.Location = New System.Drawing.Point(4, 22)
        Me.TPTestFirmComments.Name = "TPTestFirmComments"
        Me.TPTestFirmComments.Size = New System.Drawing.Size(1008, 170)
        Me.TPTestFirmComments.TabIndex = 2
        Me.TPTestFirmComments.Text = "Test Firm Comments"
        Me.TPTestFirmComments.UseVisualStyleBackColor = True
        '
        'dgvTestFirmComments
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvTestFirmComments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvTestFirmComments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTestFirmComments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvTestFirmComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTestFirmComments.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvTestFirmComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestFirmComments.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvTestFirmComments.LinkifyColumnByName = Nothing
        Me.dgvTestFirmComments.LinkifyFirstColumn = True
        Me.dgvTestFirmComments.Location = New System.Drawing.Point(0, 0)
        Me.dgvTestFirmComments.Name = "dgvTestFirmComments"
        Me.dgvTestFirmComments.ResultsCountLabel = Nothing
        Me.dgvTestFirmComments.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTestFirmComments.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvTestFirmComments.Size = New System.Drawing.Size(1008, 170)
        Me.dgvTestFirmComments.StandardTab = True
        Me.dgvTestFirmComments.TabIndex = 1
        '
        'ISMPMonitoringLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 749)
        Me.Controls.Add(Me.SCMonitoringLog)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ISMPMonitoringLog"
        Me.Text = "ISMP Monitoring Log"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SCMonitoringLog.Panel1.ResumeLayout(False)
        Me.SCMonitoringLog.Panel2.ResumeLayout(False)
        CType(Me.SCMonitoringLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCMonitoringLog.ResumeLayout(False)
        Me.GBFilterAndSortOptions.ResumeLayout(False)
        Me.GBFilterAndSortOptions.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TCMonitoringSelection.ResumeLayout(False)
        Me.TPSelectTestReport.ResumeLayout(False)
        Me.GBSelectedTestReport.ResumeLayout(False)
        Me.GBSelectedTestReport.PerformLayout()
        Me.TPSelectTestLog.ResumeLayout(False)
        Me.TPSelectTestLog.PerformLayout()
        Me.TPTestFirmComment.ResumeLayout(False)
        Me.TPTestFirmComment.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GBReportType.ResumeLayout(False)
        Me.GBDateBias.ResumeLayout(False)
        Me.GBDateBias.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TCMonitoringGrids.ResumeLayout(False)
        Me.TPTestReports.ResumeLayout(False)
        CType(Me.dgvTestReportViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPNotifications.ResumeLayout(False)
        CType(Me.dgvNotificationLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPTestFirmComments.ResumeLayout(False)
        CType(Me.dgvTestFirmComments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents SCMonitoringLog As System.Windows.Forms.SplitContainer
    Friend WithEvents GBFilterAndSortOptions As System.Windows.Forms.GroupBox
    Friend WithEvents txtReportCount As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chbAll As System.Windows.Forms.CheckBox
    Friend WithEvents chbPTE As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9Single As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9Multi As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemorandumToFile As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemorandumStandard As System.Windows.Forms.CheckBox
    Friend WithEvents chbRata As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlare As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasConcentration As System.Windows.Forms.CheckBox
    Friend WithEvents chbPondTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRack As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRE As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandard As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackFourRun As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackThreeRun As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackTwoRun As System.Windows.Forms.CheckBox
    Friend WithEvents chbUnassigned As System.Windows.Forms.CheckBox
    Friend WithEvents GBReportType As System.Windows.Forms.GroupBox
    Friend WithEvents chbSourceTest As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATAandCEMS As System.Windows.Forms.CheckBox
    Friend WithEvents chbPEMSDevelopment As System.Windows.Forms.CheckBox
    Friend WithEvents chbMonitorCertification As System.Windows.Forms.CheckBox
    Friend WithEvents GBDateBias As System.Windows.Forms.GroupBox
    Friend WithEvents rdbNA As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DTPEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdbFacilityDateCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityDateTested As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chbClosed As System.Windows.Forms.CheckBox
    Friend WithEvents chbOpen As System.Windows.Forms.CheckBox
    Friend WithEvents GBSelectedTestReport As System.Windows.Forms.GroupBox
    Friend WithEvents LLSelectReport As System.Windows.Forms.LinkLabel
    Friend WithEvents txtFacilityCounty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTestType As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chbComplianceStatus5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus1 As System.Windows.Forms.CheckBox
    Friend WithEvents TCMonitoringSelection As System.Windows.Forms.TabControl
    Friend WithEvents TPSelectTestReport As System.Windows.Forms.TabPage
    Friend WithEvents TPSelectTestLog As System.Windows.Forms.TabPage
    Friend WithEvents TCMonitoringGrids As System.Windows.Forms.TabControl
    Friend WithEvents TPTestReports As System.Windows.Forms.TabPage
    Friend WithEvents dgvTestReportViewer As Iaip.IaipDataGridView
    Friend WithEvents TPNotifications As System.Windows.Forms.TabPage
    Friend WithEvents dgvNotificationLog As Iaip.IaipDataGridView
    Friend WithEvents mmiReset As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbResize As System.Windows.Forms.ToolStripButton
    Friend WithEvents llbSelectTestLog As System.Windows.Forms.LinkLabel
    Friend WithEvents txtNotificationTestStart As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationCounty As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationCity As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationEmissionUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtTestLogNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chbNotifications As System.Windows.Forms.CheckBox
    Friend WithEvents chbTestReports As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents clbWitnessingStaff As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbWitnessingEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents clbEngineer As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbReviewingEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtCityFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCountyFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPollutantFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCommentFieldFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEmissionSourceTestedFilter As System.Windows.Forms.TextBox
    Friend WithEvents chbOther As System.Windows.Forms.CheckBox
    Friend WithEvents txtNotificationCount As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents chbNotificationUnlinked As System.Windows.Forms.CheckBox
    Friend WithEvents chbNotificationLinked As System.Windows.Forms.CheckBox
    Friend WithEvents TPTestFirmComments As System.Windows.Forms.TabPage
    Friend WithEvents dgvTestFirmComments As Iaip.IaipDataGridView
    Friend WithEvents chbTestFirmComments As System.Windows.Forms.CheckBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtTestingFirm As System.Windows.Forms.TextBox
    Friend WithEvents TPTestFirmComment As System.Windows.Forms.TabPage
    Friend WithEvents txtTestFirmCommentsCount As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents llbOpenComments As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTestFirmCounty As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmCommentType As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmAirsNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtCommentNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtPollutant As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmTestLogNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtTestFirmName As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents tsbFacilitySearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExportToExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToExcelToolStripMenuItem As ToolStripMenuItem
End Class
