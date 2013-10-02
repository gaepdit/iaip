<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPTestReportAdministrative
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPTestReportAdministrative))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.MmiReferenceNumber = New System.Windows.Forms.MenuItem
        Me.MenuItem8 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MmiClear = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.MmiCut = New System.Windows.Forms.MenuItem
        Me.MmiCopy = New System.Windows.Forms.MenuItem
        Me.MmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.MmiDelete = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MmiOpenRecords = New System.Windows.Forms.MenuItem
        Me.MmiClosedRecords = New System.Windows.Forms.MenuItem
        Me.MenuItem12 = New System.Windows.Forms.MenuItem
        Me.MmiViewByTestType = New System.Windows.Forms.MenuItem
        Me.MmiAllTestReports = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiUnassigned = New System.Windows.Forms.MenuItem
        Me.MenuItem29 = New System.Windows.Forms.MenuItem
        Me.MmiOneStackTwoRun = New System.Windows.Forms.MenuItem
        Me.MmiOneStackThreeRun = New System.Windows.Forms.MenuItem
        Me.MmiOneStackFourRun = New System.Windows.Forms.MenuItem
        Me.MenuItem30 = New System.Windows.Forms.MenuItem
        Me.MmiTwoStackStandard = New System.Windows.Forms.MenuItem
        Me.MmiTwoStackDRE = New System.Windows.Forms.MenuItem
        Me.MenuItem31 = New System.Windows.Forms.MenuItem
        Me.MmiLoadingRack = New System.Windows.Forms.MenuItem
        Me.MmiFlare = New System.Windows.Forms.MenuItem
        Me.MenuItem32 = New System.Windows.Forms.MenuItem
        Me.MmiPondTreatment = New System.Windows.Forms.MenuItem
        Me.MmiGasConcentration = New System.Windows.Forms.MenuItem
        Me.MenuItem33 = New System.Windows.Forms.MenuItem
        Me.MmiRata = New System.Windows.Forms.MenuItem
        Me.MmiPEMS = New System.Windows.Forms.MenuItem
        Me.MenuItem34 = New System.Windows.Forms.MenuItem
        Me.MmiMemoStandard = New System.Windows.Forms.MenuItem
        Me.MmiMemoToFile = New System.Windows.Forms.MenuItem
        Me.MmiMemoPTE = New System.Windows.Forms.MenuItem
        Me.MenuItem35 = New System.Windows.Forms.MenuItem
        Me.MmiMethod9Single = New System.Windows.Forms.MenuItem
        Me.MmiMethod9Multi = New System.Windows.Forms.MenuItem
        Me.MmiMethod22 = New System.Windows.Forms.MenuItem
        Me.MenuItem14 = New System.Windows.Forms.MenuItem
        Me.MmiShowDeletedRecords = New System.Windows.Forms.MenuItem
        Me.MenuItem13 = New System.Windows.Forms.MenuItem
        Me.MmiViewByFacility = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MmiShowToolbar = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.mmiMemo = New System.Windows.Forms.MenuItem
        Me.mmiAddTestingFirm = New System.Windows.Forms.MenuItem
        Me.mmiAddPollutant = New System.Windows.Forms.MenuItem
        Me.mmiRefreshLists = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.cmPrint = New System.Windows.Forms.ContextMenu
        Me.cmiPrintAFSForm = New System.Windows.Forms.MenuItem
        Me.cmiPrintTestReport = New System.Windows.Forms.MenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TBFacilityInfo = New System.Windows.Forms.ToolBar
        Me.TbbSave = New System.Windows.Forms.ToolBarButton
        Me.TbbFind = New System.Windows.Forms.ToolBarButton
        Me.TbbForward = New System.Windows.Forms.ToolBarButton
        Me.TbbAddMemo = New System.Windows.Forms.ToolBarButton
        Me.TbbClear = New System.Windows.Forms.ToolBarButton
        Me.TbbDelete = New System.Windows.Forms.ToolBarButton
        Me.TbbBack = New System.Windows.Forms.ToolBarButton
        Me.TbbExit = New System.Windows.Forms.ToolBarButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnLoadCombos = New System.Windows.Forms.Button
        Me.btnSearchForAIRS = New System.Windows.Forms.Button
        Me.btnClearReferenceNumber = New System.Windows.Forms.Button
        Me.btnCloseTestReport = New System.Windows.Forms.Button
        Me.clbReferenceNumbers = New System.Windows.Forms.CheckedListBox
        Me.btnDeleteTestReport = New System.Windows.Forms.Button
        Me.btnInsert = New System.Windows.Forms.Button
        Me.DTPTestDateStart = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDaysInAPB = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.DTPTestDateEnd = New System.Windows.Forms.DateTimePicker
        Me.cboPollutant = New System.Windows.Forms.ComboBox
        Me.txtEmissionSource = New System.Windows.Forms.TextBox
        Me.cboTestingFirms = New System.Windows.Forms.ComboBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.LLFacilityName = New System.Windows.Forms.LinkLabel
        Me.Label41 = New System.Windows.Forms.Label
        Me.cboFacilityName = New System.Windows.Forms.ComboBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.txtFacilityAddress = New System.Windows.Forms.TextBox
        Me.txtFacilityCity = New System.Windows.Forms.TextBox
        Me.txtFacilityState = New System.Windows.Forms.TextBox
        Me.txtFacilityZipCode = New System.Windows.Forms.TextBox
        Me.cboAIRSNumber = New System.Windows.Forms.ComboBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.DTPDateReceived = New System.Windows.Forms.DateTimePicker
        Me.Label21 = New System.Windows.Forms.Label
        Me.DTPDateClosed = New System.Windows.Forms.DateTimePicker
        Me.Label17 = New System.Windows.Forms.Label
        Me.GBRecordStatus = New System.Windows.Forms.GroupBox
        Me.rdbCloseReport = New System.Windows.Forms.RadioButton
        Me.rdbOpenReport = New System.Windows.Forms.RadioButton
        Me.chbOverright = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dgvFacilityInfo = New System.Windows.Forms.DataGridView
        Me.TCTestReports = New System.Windows.Forms.TabControl
        Me.TPNewTestReports = New System.Windows.Forms.TabPage
        Me.TPHistoricalReports = New System.Windows.Forms.TabPage
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.btnOpenTestReport = New System.Windows.Forms.Button
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnReOpenHistoricTestReport = New System.Windows.Forms.Button
        Me.btnCloseHistoricTestReport = New System.Windows.Forms.Button
        Me.txtCloseTestReportRefNum = New System.Windows.Forms.TextBox
        Me.Label78 = New System.Windows.Forms.Label
        Me.Label77 = New System.Windows.Forms.Label
        Me.Label76 = New System.Windows.Forms.Label
        Me.DTPAddTestReportDateCompleted = New System.Windows.Forms.DateTimePicker
        Me.Label67 = New System.Windows.Forms.Label
        Me.btnClearAddTestReport = New System.Windows.Forms.Button
        Me.mtbAddTestReportAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.dtpAddTestReportDateReceived = New System.Windows.Forms.DateTimePicker
        Me.txtAddTestReportCommissioner = New System.Windows.Forms.TextBox
        Me.txtAddTestReportDirector = New System.Windows.Forms.TextBox
        Me.txtAddTestReportProgramManager = New System.Windows.Forms.TextBox
        Me.Label74 = New System.Windows.Forms.Label
        Me.Label75 = New System.Windows.Forms.Label
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label62 = New System.Windows.Forms.Label
        Me.txtAddTestReportRefNum = New System.Windows.Forms.TextBox
        Me.btnAddTestReport = New System.Windows.Forms.Button
        Me.Label61 = New System.Windows.Forms.Label
        Me.bgw1 = New System.ComponentModel.BackgroundWorker
        Me.bgwAIRS = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBRecordStatus.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvFacilityInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TCTestReports.SuspendLayout()
        Me.TPNewTestReports.SuspendLayout()
        Me.TPHistoricalReports.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.MmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem7, Me.MmiReferenceNumber, Me.MenuItem8, Me.MmiBack, Me.MenuItem9, Me.MmiExit})
        Me.MenuItem1.Text = "File"
        '
        'MmiSave
        '
        Me.MmiSave.Index = 0
        Me.MmiSave.Text = "&Save"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 1
        Me.MenuItem7.Text = "-"
        '
        'MmiReferenceNumber
        '
        Me.MmiReferenceNumber.Index = 2
        Me.MmiReferenceNumber.Text = "Get New Reference Number"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 3
        Me.MenuItem8.Text = "-"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 4
        Me.MmiBack.Text = "&Return to Navigator"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 5
        Me.MenuItem9.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 6
        Me.MmiExit.Text = "E&xit"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiClear, Me.MenuItem6, Me.MmiCut, Me.MmiCopy, Me.MmiPaste, Me.MenuItem11, Me.MmiDelete})
        Me.MenuItem2.Text = "Edit"
        '
        'MmiClear
        '
        Me.MmiClear.Index = 0
        Me.MmiClear.Text = "Clear Page"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 1
        Me.MenuItem6.Text = "-"
        '
        'MmiCut
        '
        Me.MmiCut.Index = 2
        Me.MmiCut.Text = "Cut"
        '
        'MmiCopy
        '
        Me.MmiCopy.Index = 3
        Me.MmiCopy.Text = "Copy "
        '
        'MmiPaste
        '
        Me.MmiPaste.Index = 4
        Me.MmiPaste.Text = "Paste"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 5
        Me.MenuItem11.Text = "-"
        '
        'MmiDelete
        '
        Me.MmiDelete.Index = 6
        Me.MmiDelete.Text = "Delete Record"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiOpenRecords, Me.MmiClosedRecords, Me.MenuItem12, Me.MmiViewByTestType, Me.MenuItem14, Me.MmiShowDeletedRecords, Me.MenuItem13, Me.MmiViewByFacility})
        Me.MenuItem3.Text = "View"
        '
        'MmiOpenRecords
        '
        Me.MmiOpenRecords.Index = 0
        Me.MmiOpenRecords.Text = "View Open Records"
        '
        'MmiClosedRecords
        '
        Me.MmiClosedRecords.Index = 1
        Me.MmiClosedRecords.Text = "View Closed Records"
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 2
        Me.MenuItem12.Text = "-"
        '
        'MmiViewByTestType
        '
        Me.MmiViewByTestType.Index = 3
        Me.MmiViewByTestType.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiAllTestReports, Me.MenuItem10, Me.MmiUnassigned, Me.MenuItem29, Me.MmiOneStackTwoRun, Me.MmiOneStackThreeRun, Me.MmiOneStackFourRun, Me.MenuItem30, Me.MmiTwoStackStandard, Me.MmiTwoStackDRE, Me.MenuItem31, Me.MmiLoadingRack, Me.MmiFlare, Me.MenuItem32, Me.MmiPondTreatment, Me.MmiGasConcentration, Me.MenuItem33, Me.MmiRata, Me.MmiPEMS, Me.MenuItem34, Me.MmiMemoStandard, Me.MmiMemoToFile, Me.MmiMemoPTE, Me.MenuItem35, Me.MmiMethod9Single, Me.MmiMethod9Multi, Me.MmiMethod22})
        Me.MmiViewByTestType.Text = "View By Test Type"
        '
        'MmiAllTestReports
        '
        Me.MmiAllTestReports.Index = 0
        Me.MmiAllTestReports.Text = "All Test Reports"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "-"
        '
        'MmiUnassigned
        '
        Me.MmiUnassigned.Index = 2
        Me.MmiUnassigned.Text = "Unassigned"
        '
        'MenuItem29
        '
        Me.MenuItem29.Index = 3
        Me.MenuItem29.Text = "-"
        '
        'MmiOneStackTwoRun
        '
        Me.MmiOneStackTwoRun.Index = 4
        Me.MmiOneStackTwoRun.Text = "One Stack (Two Runs)"
        '
        'MmiOneStackThreeRun
        '
        Me.MmiOneStackThreeRun.Index = 5
        Me.MmiOneStackThreeRun.Text = "One Stack (Three Runs)"
        '
        'MmiOneStackFourRun
        '
        Me.MmiOneStackFourRun.Index = 6
        Me.MmiOneStackFourRun.Text = "One Stack (Four Runs)"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 7
        Me.MenuItem30.Text = "-"
        '
        'MmiTwoStackStandard
        '
        Me.MmiTwoStackStandard.Index = 8
        Me.MmiTwoStackStandard.Text = "Two Stack (Standard)"
        '
        'MmiTwoStackDRE
        '
        Me.MmiTwoStackDRE.Index = 9
        Me.MmiTwoStackDRE.Text = "Two Stack (DRE)"
        '
        'MenuItem31
        '
        Me.MenuItem31.Index = 10
        Me.MenuItem31.Text = "-"
        '
        'MmiLoadingRack
        '
        Me.MmiLoadingRack.Index = 11
        Me.MmiLoadingRack.Text = "Loading Rack"
        '
        'MmiFlare
        '
        Me.MmiFlare.Index = 12
        Me.MmiFlare.Text = "Flare"
        '
        'MenuItem32
        '
        Me.MenuItem32.Index = 13
        Me.MenuItem32.Text = "-"
        '
        'MmiPondTreatment
        '
        Me.MmiPondTreatment.Index = 14
        Me.MmiPondTreatment.Text = "Pond Treatment"
        '
        'MmiGasConcentration
        '
        Me.MmiGasConcentration.Index = 15
        Me.MmiGasConcentration.Text = "Gas Concentration"
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = 16
        Me.MenuItem33.Text = "-"
        '
        'MmiRata
        '
        Me.MmiRata.Index = 17
        Me.MmiRata.Text = "Rata"
        '
        'MmiPEMS
        '
        Me.MmiPEMS.Index = 18
        Me.MmiPEMS.Text = "PEMS"
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 19
        Me.MenuItem34.Text = "-"
        '
        'MmiMemoStandard
        '
        Me.MmiMemoStandard.Index = 20
        Me.MmiMemoStandard.Text = "Memorandum (Standard)"
        '
        'MmiMemoToFile
        '
        Me.MmiMemoToFile.Index = 21
        Me.MmiMemoToFile.Text = "Memorandum (To File)"
        '
        'MmiMemoPTE
        '
        Me.MmiMemoPTE.Index = 22
        Me.MmiMemoPTE.Text = "PTE (Perminate Total Enclosure)"
        '
        'MenuItem35
        '
        Me.MenuItem35.Index = 23
        Me.MenuItem35.Text = "-"
        '
        'MmiMethod9Single
        '
        Me.MmiMethod9Single.Index = 24
        Me.MmiMethod9Single.Text = "Method 9 (Single)"
        '
        'MmiMethod9Multi
        '
        Me.MmiMethod9Multi.Index = 25
        Me.MmiMethod9Multi.Text = "Method9 (Multi.)"
        '
        'MmiMethod22
        '
        Me.MmiMethod22.Index = 26
        Me.MmiMethod22.Text = "Method 22"
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 4
        Me.MenuItem14.Text = "-"
        '
        'MmiShowDeletedRecords
        '
        Me.MmiShowDeletedRecords.Index = 5
        Me.MmiShowDeletedRecords.Text = "View Deleted Records"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 6
        Me.MenuItem13.Text = "-"
        '
        'MmiViewByFacility
        '
        Me.MmiViewByFacility.Index = 7
        Me.MmiViewByFacility.Text = "View By Facility"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiShowToolbar, Me.MenuItem5, Me.mmiMemo, Me.mmiAddTestingFirm, Me.mmiAddPollutant, Me.mmiRefreshLists})
        Me.MenuItem4.Text = "Tools"
        '
        'MmiShowToolbar
        '
        Me.MmiShowToolbar.Index = 0
        Me.MmiShowToolbar.Text = "Show Toolbar"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 1
        Me.MenuItem5.Text = "-"
        '
        'mmiMemo
        '
        Me.mmiMemo.Index = 2
        Me.mmiMemo.Text = "Add Memo"
        '
        'mmiAddTestingFirm
        '
        Me.mmiAddTestingFirm.Index = 3
        Me.mmiAddTestingFirm.Text = "Add Testing Firm"
        '
        'mmiAddPollutant
        '
        Me.mmiAddPollutant.Index = 4
        Me.mmiAddPollutant.Text = "Add Pollutant"
        '
        'mmiRefreshLists
        '
        Me.mmiRefreshLists.Index = 5
        Me.mmiRefreshLists.Text = "Refresh List(s)"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 4
        Me.MmiHelp.Text = "Help"
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
        'cmPrint
        '
        Me.cmPrint.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmiPrintAFSForm, Me.cmiPrintTestReport})
        '
        'cmiPrintAFSForm
        '
        Me.cmiPrintAFSForm.Index = 0
        Me.cmiPrintAFSForm.Text = "Print AFS Form"
        '
        'cmiPrintTestReport
        '
        Me.cmiPrintTestReport.Index = 1
        Me.cmiPrintTestReport.Text = "Print Test Report"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 523)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(816, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(793, 17)
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
        Me.Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 17)
        Me.Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TBFacilityInfo
        '
        Me.TBFacilityInfo.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbSave, Me.TbbFind, Me.TbbForward, Me.TbbAddMemo, Me.TbbClear, Me.TbbDelete, Me.TbbBack, Me.TbbExit})
        Me.TBFacilityInfo.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBFacilityInfo.ContextMenu = Me.cmPrint
        Me.TBFacilityInfo.DropDownArrows = True
        Me.TBFacilityInfo.ImageList = Me.Image_List_All
        Me.TBFacilityInfo.Location = New System.Drawing.Point(0, 0)
        Me.TBFacilityInfo.Name = "TBFacilityInfo"
        Me.TBFacilityInfo.ShowToolTips = True
        Me.TBFacilityInfo.Size = New System.Drawing.Size(816, 28)
        Me.TBFacilityInfo.TabIndex = 47
        '
        'TbbSave
        '
        Me.TbbSave.ImageIndex = 65
        Me.TbbSave.Name = "TbbSave"
        '
        'TbbFind
        '
        Me.TbbFind.ImageIndex = 3
        Me.TbbFind.Name = "TbbFind"
        '
        'TbbForward
        '
        Me.TbbForward.ImageIndex = 61
        Me.TbbForward.Name = "TbbForward"
        '
        'TbbAddMemo
        '
        Me.TbbAddMemo.ImageIndex = 0
        Me.TbbAddMemo.Name = "TbbAddMemo"
        '
        'TbbClear
        '
        Me.TbbClear.ImageIndex = 84
        Me.TbbClear.Name = "TbbClear"
        '
        'TbbDelete
        '
        Me.TbbDelete.ImageIndex = 13
        Me.TbbDelete.Name = "TbbDelete"
        '
        'TbbBack
        '
        Me.TbbBack.ImageIndex = 2
        Me.TbbBack.Name = "TbbBack"
        '
        'TbbExit
        '
        Me.TbbExit.ImageIndex = 81
        Me.TbbExit.Name = "TbbExit"
        Me.TbbExit.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnLoadCombos)
        Me.GroupBox1.Controls.Add(Me.btnSearchForAIRS)
        Me.GroupBox1.Controls.Add(Me.btnClearReferenceNumber)
        Me.GroupBox1.Controls.Add(Me.btnCloseTestReport)
        Me.GroupBox1.Controls.Add(Me.clbReferenceNumbers)
        Me.GroupBox1.Controls.Add(Me.btnDeleteTestReport)
        Me.GroupBox1.Controls.Add(Me.btnInsert)
        Me.GroupBox1.Controls.Add(Me.DTPTestDateStart)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDaysInAPB)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label45)
        Me.GroupBox1.Controls.Add(Me.DTPTestDateEnd)
        Me.GroupBox1.Controls.Add(Me.cboPollutant)
        Me.GroupBox1.Controls.Add(Me.txtEmissionSource)
        Me.GroupBox1.Controls.Add(Me.cboTestingFirms)
        Me.GroupBox1.Controls.Add(Me.Label51)
        Me.GroupBox1.Controls.Add(Me.Label47)
        Me.GroupBox1.Controls.Add(Me.Label46)
        Me.GroupBox1.Controls.Add(Me.LLFacilityName)
        Me.GroupBox1.Controls.Add(Me.Label41)
        Me.GroupBox1.Controls.Add(Me.cboFacilityName)
        Me.GroupBox1.Controls.Add(Me.Label42)
        Me.GroupBox1.Controls.Add(Me.txtFacilityAddress)
        Me.GroupBox1.Controls.Add(Me.txtFacilityCity)
        Me.GroupBox1.Controls.Add(Me.txtFacilityState)
        Me.GroupBox1.Controls.Add(Me.txtFacilityZipCode)
        Me.GroupBox1.Controls.Add(Me.cboAIRSNumber)
        Me.GroupBox1.Controls.Add(Me.Label40)
        Me.GroupBox1.Controls.Add(Me.DTPDateReceived)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.DTPDateClosed)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.GBRecordStatus)
        Me.GroupBox1.Controls.Add(Me.chbOverright)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtReferenceNumber)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(802, 279)
        Me.GroupBox1.TabIndex = 48
        Me.GroupBox1.TabStop = False
        '
        'btnLoadCombos
        '
        Me.btnLoadCombos.AutoSize = True
        Me.btnLoadCombos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadCombos.Location = New System.Drawing.Point(652, 13)
        Me.btnLoadCombos.Name = "btnLoadCombos"
        Me.btnLoadCombos.Size = New System.Drawing.Size(137, 23)
        Me.btnLoadCombos.TabIndex = 221
        Me.btnLoadCombos.Text = "Load AIRS/Facility Name"
        Me.btnLoadCombos.UseVisualStyleBackColor = True
        '
        'btnSearchForAIRS
        '
        Me.btnSearchForAIRS.AutoSize = True
        Me.btnSearchForAIRS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchForAIRS.Location = New System.Drawing.Point(216, 38)
        Me.btnSearchForAIRS.Name = "btnSearchForAIRS"
        Me.btnSearchForAIRS.Size = New System.Drawing.Size(51, 23)
        Me.btnSearchForAIRS.TabIndex = 220
        Me.btnSearchForAIRS.Text = "Search"
        Me.btnSearchForAIRS.UseVisualStyleBackColor = True
        '
        'btnClearReferenceNumber
        '
        Me.btnClearReferenceNumber.AutoSize = True
        Me.btnClearReferenceNumber.ImageIndex = 84
        Me.btnClearReferenceNumber.ImageList = Me.Image_List_All
        Me.btnClearReferenceNumber.Location = New System.Drawing.Point(188, 13)
        Me.btnClearReferenceNumber.Name = "btnClearReferenceNumber"
        Me.btnClearReferenceNumber.Size = New System.Drawing.Size(22, 22)
        Me.btnClearReferenceNumber.TabIndex = 219
        Me.btnClearReferenceNumber.UseVisualStyleBackColor = True
        '
        'btnCloseTestReport
        '
        Me.btnCloseTestReport.AutoSize = True
        Me.btnCloseTestReport.Enabled = False
        Me.btnCloseTestReport.Location = New System.Drawing.Point(629, 196)
        Me.btnCloseTestReport.Name = "btnCloseTestReport"
        Me.btnCloseTestReport.Size = New System.Drawing.Size(120, 23)
        Me.btnCloseTestReport.TabIndex = 15
        Me.btnCloseTestReport.Text = "Close Out Report(s)"
        Me.btnCloseTestReport.UseVisualStyleBackColor = True
        '
        'clbReferenceNumbers
        '
        Me.clbReferenceNumbers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbReferenceNumbers.CheckOnClick = True
        Me.clbReferenceNumbers.FormattingEnabled = True
        Me.clbReferenceNumbers.HorizontalScrollbar = True
        Me.clbReferenceNumbers.Location = New System.Drawing.Point(509, 39)
        Me.clbReferenceNumbers.Name = "clbReferenceNumbers"
        Me.clbReferenceNumbers.Size = New System.Drawing.Size(280, 109)
        Me.clbReferenceNumbers.TabIndex = 217
        '
        'btnDeleteTestReport
        '
        Me.btnDeleteTestReport.AutoSize = True
        Me.btnDeleteTestReport.Location = New System.Drawing.Point(301, 250)
        Me.btnDeleteTestReport.Name = "btnDeleteTestReport"
        Me.btnDeleteTestReport.Size = New System.Drawing.Size(120, 23)
        Me.btnDeleteTestReport.TabIndex = 11
        Me.btnDeleteTestReport.Text = "Delete Test Report"
        Me.btnDeleteTestReport.UseVisualStyleBackColor = True
        '
        'btnInsert
        '
        Me.btnInsert.AutoSize = True
        Me.btnInsert.Location = New System.Drawing.Point(116, 250)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(135, 23)
        Me.btnInsert.TabIndex = 10
        Me.btnInsert.Text = "Add/Update Test Report"
        Me.btnInsert.UseVisualStyleBackColor = True
        '
        'DTPTestDateStart
        '
        Me.DTPTestDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateStart.Location = New System.Drawing.Point(116, 171)
        Me.DTPTestDateStart.Name = "DTPTestDateStart"
        Me.DTPTestDateStart.Size = New System.Drawing.Size(104, 20)
        Me.DTPTestDateStart.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(390, 146)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 201
        Me.Label3.Text = "Days"
        '
        'txtDaysInAPB
        '
        Me.txtDaysInAPB.Location = New System.Drawing.Point(328, 142)
        Me.txtDaysInAPB.Name = "txtDaysInAPB"
        Me.txtDaysInAPB.ReadOnly = True
        Me.txtDaysInAPB.Size = New System.Drawing.Size(61, 20)
        Me.txtDaysInAPB.TabIndex = 200
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(226, 146)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(96, 13)
        Me.Label24.TabIndex = 199
        Me.Label24.Text = "Total Days in APB:"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(26, 228)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(80, 13)
        Me.Label45.TabIndex = 188
        Me.Label45.Text = "Source Tested:"
        '
        'DTPTestDateEnd
        '
        Me.DTPTestDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateEnd.Location = New System.Drawing.Point(248, 171)
        Me.DTPTestDateEnd.Name = "DTPTestDateEnd"
        Me.DTPTestDateEnd.Size = New System.Drawing.Size(104, 20)
        Me.DTPTestDateEnd.TabIndex = 6
        Me.DTPTestDateEnd.Value = New Date(2005, 2, 25, 0, 0, 0, 0)
        '
        'cboPollutant
        '
        Me.cboPollutant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollutant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollutant.Location = New System.Drawing.Point(498, 224)
        Me.cboPollutant.Name = "cboPollutant"
        Me.cboPollutant.Size = New System.Drawing.Size(281, 21)
        Me.cboPollutant.TabIndex = 9
        '
        'txtEmissionSource
        '
        Me.txtEmissionSource.Location = New System.Drawing.Point(116, 224)
        Me.txtEmissionSource.MaxLength = 100
        Me.txtEmissionSource.Name = "txtEmissionSource"
        Me.txtEmissionSource.Size = New System.Drawing.Size(305, 20)
        Me.txtEmissionSource.TabIndex = 8
        '
        'cboTestingFirms
        '
        Me.cboTestingFirms.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTestingFirms.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTestingFirms.Location = New System.Drawing.Point(116, 197)
        Me.cboTestingFirms.Name = "cboTestingFirms"
        Me.cboTestingFirms.Size = New System.Drawing.Size(305, 21)
        Me.cboTestingFirms.TabIndex = 7
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(44, 175)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(62, 13)
        Me.Label51.TabIndex = 187
        Me.Label51.Text = "Test Dates:"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(444, 228)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(51, 13)
        Me.Label47.TabIndex = 186
        Me.Label47.Text = "Pollutant:"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(39, 201)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(67, 13)
        Me.Label46.TabIndex = 185
        Me.Label46.Text = "Testing Firm:"
        '
        'LLFacilityName
        '
        Me.LLFacilityName.AutoSize = True
        Me.LLFacilityName.Location = New System.Drawing.Point(33, 67)
        Me.LLFacilityName.Name = "LLFacilityName"
        Me.LLFacilityName.Size = New System.Drawing.Size(73, 13)
        Me.LLFacilityName.TabIndex = 180
        Me.LLFacilityName.TabStop = True
        Me.LLFacilityName.Text = "Facility Name:"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(23, 94)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(83, 13)
        Me.Label41.TabIndex = 172
        Me.Label41.Text = "Facility Address:"
        '
        'cboFacilityName
        '
        Me.cboFacilityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityName.Location = New System.Drawing.Point(116, 63)
        Me.cboFacilityName.Name = "cboFacilityName"
        Me.cboFacilityName.Size = New System.Drawing.Size(304, 21)
        Me.cboFacilityName.TabIndex = 2
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(29, 120)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(77, 13)
        Me.Label42.TabIndex = 173
        Me.Label42.Text = "City/State/Zip:"
        '
        'txtFacilityAddress
        '
        Me.txtFacilityAddress.Location = New System.Drawing.Point(116, 90)
        Me.txtFacilityAddress.Name = "txtFacilityAddress"
        Me.txtFacilityAddress.ReadOnly = True
        Me.txtFacilityAddress.Size = New System.Drawing.Size(304, 20)
        Me.txtFacilityAddress.TabIndex = 177
        Me.txtFacilityAddress.TabStop = False
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.Location = New System.Drawing.Point(117, 116)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.ReadOnly = True
        Me.txtFacilityCity.Size = New System.Drawing.Size(144, 20)
        Me.txtFacilityCity.TabIndex = 178
        Me.txtFacilityCity.TabStop = False
        '
        'txtFacilityState
        '
        Me.txtFacilityState.Location = New System.Drawing.Point(267, 116)
        Me.txtFacilityState.MaxLength = 5
        Me.txtFacilityState.Name = "txtFacilityState"
        Me.txtFacilityState.ReadOnly = True
        Me.txtFacilityState.Size = New System.Drawing.Size(40, 20)
        Me.txtFacilityState.TabIndex = 179
        Me.txtFacilityState.TabStop = False
        '
        'txtFacilityZipCode
        '
        Me.txtFacilityZipCode.Location = New System.Drawing.Point(313, 116)
        Me.txtFacilityZipCode.MaxLength = 5
        Me.txtFacilityZipCode.Name = "txtFacilityZipCode"
        Me.txtFacilityZipCode.ReadOnly = True
        Me.txtFacilityZipCode.Size = New System.Drawing.Size(108, 20)
        Me.txtFacilityZipCode.TabIndex = 174
        Me.txtFacilityZipCode.TabStop = False
        '
        'cboAIRSNumber
        '
        Me.cboAIRSNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAIRSNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAIRSNumber.Location = New System.Drawing.Point(115, 39)
        Me.cboAIRSNumber.MaxLength = 8
        Me.cboAIRSNumber.Name = "cboAIRSNumber"
        Me.cboAIRSNumber.Size = New System.Drawing.Size(85, 21)
        Me.cboAIRSNumber.TabIndex = 3
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(31, 43)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(75, 13)
        Me.Label40.TabIndex = 171
        Me.Label40.Text = "AIRS Number:"
        '
        'DTPDateReceived
        '
        Me.DTPDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReceived.Location = New System.Drawing.Point(116, 142)
        Me.DTPDateReceived.Name = "DTPDateReceived"
        Me.DTPDateReceived.Size = New System.Drawing.Size(104, 20)
        Me.DTPDateReceived.TabIndex = 4
        Me.DTPDateReceived.Value = New Date(2005, 3, 24, 0, 0, 0, 0)
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(24, 146)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(82, 13)
        Me.Label21.TabIndex = 164
        Me.Label21.Text = "Date Recieved:"
        '
        'DTPDateClosed
        '
        Me.DTPDateClosed.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateClosed.Enabled = False
        Me.DTPDateClosed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateClosed.Location = New System.Drawing.Point(509, 197)
        Me.DTPDateClosed.Name = "DTPDateClosed"
        Me.DTPDateClosed.Size = New System.Drawing.Size(104, 20)
        Me.DTPDateClosed.TabIndex = 14
        Me.DTPDateClosed.Value = New Date(2005, 2, 25, 0, 0, 0, 0)
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(433, 201)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 13)
        Me.Label17.TabIndex = 161
        Me.Label17.Text = "Date Closed:"
        '
        'GBRecordStatus
        '
        Me.GBRecordStatus.Controls.Add(Me.rdbCloseReport)
        Me.GBRecordStatus.Controls.Add(Me.rdbOpenReport)
        Me.GBRecordStatus.Location = New System.Drawing.Point(509, 151)
        Me.GBRecordStatus.Name = "GBRecordStatus"
        Me.GBRecordStatus.Size = New System.Drawing.Size(240, 40)
        Me.GBRecordStatus.TabIndex = 160
        Me.GBRecordStatus.TabStop = False
        Me.GBRecordStatus.Text = "Report Status"
        '
        'rdbCloseReport
        '
        Me.rdbCloseReport.Location = New System.Drawing.Point(128, 16)
        Me.rdbCloseReport.Name = "rdbCloseReport"
        Me.rdbCloseReport.Size = New System.Drawing.Size(104, 16)
        Me.rdbCloseReport.TabIndex = 13
        Me.rdbCloseReport.Text = "Report Closed"
        '
        'rdbOpenReport
        '
        Me.rdbOpenReport.Checked = True
        Me.rdbOpenReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbOpenReport.Location = New System.Drawing.Point(16, 16)
        Me.rdbOpenReport.Name = "rdbOpenReport"
        Me.rdbOpenReport.Size = New System.Drawing.Size(104, 16)
        Me.rdbOpenReport.TabIndex = 12
        Me.rdbOpenReport.TabStop = True
        Me.rdbOpenReport.Text = "Report Open"
        '
        'chbOverright
        '
        Me.chbOverright.Location = New System.Drawing.Point(216, 16)
        Me.chbOverright.Name = "chbOverright"
        Me.chbOverright.Size = New System.Drawing.Size(200, 16)
        Me.chbOverright.TabIndex = 159
        Me.chbOverright.Text = "Manually Enter Reference Number"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 13)
        Me.Label10.TabIndex = 158
        Me.Label10.Text = "Reference Number:"
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(112, 15)
        Me.txtReferenceNumber.MaxLength = 9
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.ReadOnly = True
        Me.txtReferenceNumber.Size = New System.Drawing.Size(70, 20)
        Me.txtReferenceNumber.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvFacilityInfo)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 342)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(816, 181)
        Me.GroupBox2.TabIndex = 213
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Test Reports"
        '
        'dgvFacilityInfo
        '
        Me.dgvFacilityInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFacilityInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFacilityInfo.Location = New System.Drawing.Point(3, 16)
        Me.dgvFacilityInfo.Name = "dgvFacilityInfo"
        Me.dgvFacilityInfo.ReadOnly = True
        Me.dgvFacilityInfo.Size = New System.Drawing.Size(810, 162)
        Me.dgvFacilityInfo.TabIndex = 213
        '
        'TCTestReports
        '
        Me.TCTestReports.Controls.Add(Me.TPNewTestReports)
        Me.TCTestReports.Controls.Add(Me.TPHistoricalReports)
        Me.TCTestReports.Dock = System.Windows.Forms.DockStyle.Top
        Me.TCTestReports.Location = New System.Drawing.Point(0, 28)
        Me.TCTestReports.Name = "TCTestReports"
        Me.TCTestReports.SelectedIndex = 0
        Me.TCTestReports.Size = New System.Drawing.Size(816, 314)
        Me.TCTestReports.TabIndex = 214
        '
        'TPNewTestReports
        '
        Me.TPNewTestReports.Controls.Add(Me.GroupBox1)
        Me.TPNewTestReports.Location = New System.Drawing.Point(4, 22)
        Me.TPNewTestReports.Name = "TPNewTestReports"
        Me.TPNewTestReports.Padding = New System.Windows.Forms.Padding(3)
        Me.TPNewTestReports.Size = New System.Drawing.Size(808, 288)
        Me.TPNewTestReports.TabIndex = 0
        Me.TPNewTestReports.Text = "Add/Edit New Test Reports"
        Me.TPNewTestReports.UseVisualStyleBackColor = True
        '
        'TPHistoricalReports
        '
        Me.TPHistoricalReports.Controls.Add(Me.Panel11)
        Me.TPHistoricalReports.Location = New System.Drawing.Point(4, 22)
        Me.TPHistoricalReports.Name = "TPHistoricalReports"
        Me.TPHistoricalReports.Padding = New System.Windows.Forms.Padding(3)
        Me.TPHistoricalReports.Size = New System.Drawing.Size(808, 288)
        Me.TPHistoricalReports.TabIndex = 1
        Me.TPHistoricalReports.Text = "Historical Test Reports"
        Me.TPHistoricalReports.UseVisualStyleBackColor = True
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.btnOpenTestReport)
        Me.Panel11.Controls.Add(Me.Panel12)
        Me.Panel11.Controls.Add(Me.Label77)
        Me.Panel11.Controls.Add(Me.Label76)
        Me.Panel11.Controls.Add(Me.DTPAddTestReportDateCompleted)
        Me.Panel11.Controls.Add(Me.Label67)
        Me.Panel11.Controls.Add(Me.btnClearAddTestReport)
        Me.Panel11.Controls.Add(Me.mtbAddTestReportAIRSNumber)
        Me.Panel11.Controls.Add(Me.dtpAddTestReportDateReceived)
        Me.Panel11.Controls.Add(Me.txtAddTestReportCommissioner)
        Me.Panel11.Controls.Add(Me.txtAddTestReportDirector)
        Me.Panel11.Controls.Add(Me.txtAddTestReportProgramManager)
        Me.Panel11.Controls.Add(Me.Label74)
        Me.Panel11.Controls.Add(Me.Label75)
        Me.Panel11.Controls.Add(Me.Label66)
        Me.Panel11.Controls.Add(Me.Label63)
        Me.Panel11.Controls.Add(Me.Label62)
        Me.Panel11.Controls.Add(Me.txtAddTestReportRefNum)
        Me.Panel11.Controls.Add(Me.btnAddTestReport)
        Me.Panel11.Controls.Add(Me.Label61)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(3, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(802, 282)
        Me.Panel11.TabIndex = 1
        '
        'btnOpenTestReport
        '
        Me.btnOpenTestReport.AutoSize = True
        Me.btnOpenTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenTestReport.Location = New System.Drawing.Point(259, 218)
        Me.btnOpenTestReport.Name = "btnOpenTestReport"
        Me.btnOpenTestReport.Size = New System.Drawing.Size(102, 23)
        Me.btnOpenTestReport.TabIndex = 13
        Me.btnOpenTestReport.Text = "Open Test Report"
        Me.btnOpenTestReport.UseVisualStyleBackColor = True
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.btnReOpenHistoricTestReport)
        Me.Panel12.Controls.Add(Me.btnCloseHistoricTestReport)
        Me.Panel12.Controls.Add(Me.txtCloseTestReportRefNum)
        Me.Panel12.Controls.Add(Me.Label78)
        Me.Panel12.Location = New System.Drawing.Point(548, 176)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(249, 100)
        Me.Panel12.TabIndex = 12
        '
        'btnReOpenHistoricTestReport
        '
        Me.btnReOpenHistoricTestReport.AutoSize = True
        Me.btnReOpenHistoricTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReOpenHistoricTestReport.Location = New System.Drawing.Point(106, 61)
        Me.btnReOpenHistoricTestReport.Name = "btnReOpenHistoricTestReport"
        Me.btnReOpenHistoricTestReport.Size = New System.Drawing.Size(119, 23)
        Me.btnReOpenHistoricTestReport.TabIndex = 9
        Me.btnReOpenHistoricTestReport.Text = "Re-Open Test Report"
        Me.btnReOpenHistoricTestReport.UseVisualStyleBackColor = True
        '
        'btnCloseHistoricTestReport
        '
        Me.btnCloseHistoricTestReport.AutoSize = True
        Me.btnCloseHistoricTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCloseHistoricTestReport.Location = New System.Drawing.Point(106, 32)
        Me.btnCloseHistoricTestReport.Name = "btnCloseHistoricTestReport"
        Me.btnCloseHistoricTestReport.Size = New System.Drawing.Size(102, 23)
        Me.btnCloseHistoricTestReport.TabIndex = 8
        Me.btnCloseHistoricTestReport.Text = "Close Test Report"
        Me.btnCloseHistoricTestReport.UseVisualStyleBackColor = True
        '
        'txtCloseTestReportRefNum
        '
        Me.txtCloseTestReportRefNum.Location = New System.Drawing.Point(106, 6)
        Me.txtCloseTestReportRefNum.Name = "txtCloseTestReportRefNum"
        Me.txtCloseTestReportRefNum.Size = New System.Drawing.Size(136, 20)
        Me.txtCloseTestReportRefNum.TabIndex = 3
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(3, 9)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(97, 13)
        Me.Label78.TabIndex = 4
        Me.Label78.Text = "Reference Number"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(359, 102)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(233, 52)
        Me.Label77.TabIndex = 10
        Me.Label77.Text = "Please enter the " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Commissioner, Director, and Program Managers " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "from the origin" & _
            "al Test Report " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "not the Current individuals if different. "
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(18, 11)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(458, 25)
        Me.Label76.TabIndex = 9
        Me.Label76.Text = "This tool is for historical test reports only. "
        '
        'DTPAddTestReportDateCompleted
        '
        Me.DTPAddTestReportDateCompleted.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAddTestReportDateCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPAddTestReportDateCompleted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAddTestReportDateCompleted.Location = New System.Drawing.Point(345, 176)
        Me.DTPAddTestReportDateCompleted.Name = "DTPAddTestReportDateCompleted"
        Me.DTPAddTestReportDateCompleted.Size = New System.Drawing.Size(108, 22)
        Me.DTPAddTestReportDateCompleted.TabIndex = 6
        Me.DTPAddTestReportDateCompleted.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(256, 181)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(83, 13)
        Me.Label67.TabIndex = 6
        Me.Label67.Text = "Date Completed"
        '
        'btnClearAddTestReport
        '
        Me.btnClearAddTestReport.AutoSize = True
        Me.btnClearAddTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAddTestReport.Location = New System.Drawing.Point(386, 218)
        Me.btnClearAddTestReport.Name = "btnClearAddTestReport"
        Me.btnClearAddTestReport.Size = New System.Drawing.Size(67, 23)
        Me.btnClearAddTestReport.TabIndex = 8
        Me.btnClearAddTestReport.Text = "Clear Data"
        Me.btnClearAddTestReport.UseVisualStyleBackColor = True
        '
        'mtbAddTestReportAIRSNumber
        '
        Me.mtbAddTestReportAIRSNumber.Location = New System.Drawing.Point(124, 76)
        Me.mtbAddTestReportAIRSNumber.Mask = "000-00000"
        Me.mtbAddTestReportAIRSNumber.Name = "mtbAddTestReportAIRSNumber"
        Me.mtbAddTestReportAIRSNumber.Size = New System.Drawing.Size(80, 20)
        Me.mtbAddTestReportAIRSNumber.TabIndex = 1
        Me.mtbAddTestReportAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'dtpAddTestReportDateReceived
        '
        Me.dtpAddTestReportDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.dtpAddTestReportDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAddTestReportDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAddTestReportDateReceived.Location = New System.Drawing.Point(124, 176)
        Me.dtpAddTestReportDateReceived.Name = "dtpAddTestReportDateReceived"
        Me.dtpAddTestReportDateReceived.Size = New System.Drawing.Size(108, 22)
        Me.dtpAddTestReportDateReceived.TabIndex = 5
        Me.dtpAddTestReportDateReceived.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'txtAddTestReportCommissioner
        '
        Me.txtAddTestReportCommissioner.Location = New System.Drawing.Point(124, 102)
        Me.txtAddTestReportCommissioner.Name = "txtAddTestReportCommissioner"
        Me.txtAddTestReportCommissioner.Size = New System.Drawing.Size(211, 20)
        Me.txtAddTestReportCommissioner.TabIndex = 2
        '
        'txtAddTestReportDirector
        '
        Me.txtAddTestReportDirector.Location = New System.Drawing.Point(124, 127)
        Me.txtAddTestReportDirector.Name = "txtAddTestReportDirector"
        Me.txtAddTestReportDirector.Size = New System.Drawing.Size(211, 20)
        Me.txtAddTestReportDirector.TabIndex = 3
        '
        'txtAddTestReportProgramManager
        '
        Me.txtAddTestReportProgramManager.Location = New System.Drawing.Point(124, 151)
        Me.txtAddTestReportProgramManager.Name = "txtAddTestReportProgramManager"
        Me.txtAddTestReportProgramManager.Size = New System.Drawing.Size(211, 20)
        Me.txtAddTestReportProgramManager.TabIndex = 4
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(20, 181)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(79, 13)
        Me.Label74.TabIndex = 8
        Me.Label74.Text = "Date Received"
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(20, 155)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(91, 13)
        Me.Label75.TabIndex = 7
        Me.Label75.Text = "Program Manager"
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(20, 131)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(44, 13)
        Me.Label66.TabIndex = 6
        Me.Label66.Text = "Director"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(20, 106)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(74, 13)
        Me.Label63.TabIndex = 5
        Me.Label63.Text = "Commissioner:"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(20, 80)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(72, 13)
        Me.Label62.TabIndex = 4
        Me.Label62.Text = "AIRS Number"
        '
        'txtAddTestReportRefNum
        '
        Me.txtAddTestReportRefNum.Location = New System.Drawing.Point(124, 51)
        Me.txtAddTestReportRefNum.Name = "txtAddTestReportRefNum"
        Me.txtAddTestReportRefNum.Size = New System.Drawing.Size(108, 20)
        Me.txtAddTestReportRefNum.TabIndex = 0
        '
        'btnAddTestReport
        '
        Me.btnAddTestReport.AutoSize = True
        Me.btnAddTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddTestReport.Location = New System.Drawing.Point(124, 218)
        Me.btnAddTestReport.Name = "btnAddTestReport"
        Me.btnAddTestReport.Size = New System.Drawing.Size(95, 23)
        Me.btnAddTestReport.TabIndex = 7
        Me.btnAddTestReport.Text = "Add Test Report"
        Me.btnAddTestReport.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(20, 55)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(97, 13)
        Me.Label61.TabIndex = 2
        Me.Label61.Text = "Reference Number"
        '
        'bgw1
        '
        '
        'ISMPTestReportAdministrative
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 545)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TCTestReports)
        Me.Controls.Add(Me.TBFacilityInfo)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPTestReportAdministrative"
        Me.Text = "ISMP Test Report Administrative"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBRecordStatus.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvFacilityInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TCTestReports.ResumeLayout(False)
        Me.TPNewTestReports.ResumeLayout(False)
        Me.TPHistoricalReports.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiReferenceNumber As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOpenRecords As System.Windows.Forms.MenuItem
    Friend WithEvents MmiClosedRecords As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiViewByTestType As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllTestReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiUnassigned As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem29 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOneStackTwoRun As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOneStackThreeRun As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOneStackFourRun As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiTwoStackStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiTwoStackDRE As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem31 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiLoadingRack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiFlare As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem32 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPondTreatment As System.Windows.Forms.MenuItem
    Friend WithEvents MmiGasConcentration As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiRata As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPEMS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMemoStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMemoToFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMemoPTE As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem35 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMethod9Single As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMethod9Multi As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMethod22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiShowDeletedRecords As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiViewByFacility As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiShowToolbar As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiMemo As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAddTestingFirm As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAddPollutant As System.Windows.Forms.MenuItem
    Friend WithEvents mmiRefreshLists As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents cmPrint As System.Windows.Forms.ContextMenu
    Friend WithEvents cmiPrintAFSForm As System.Windows.Forms.MenuItem
    Friend WithEvents cmiPrintTestReport As System.Windows.Forms.MenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TBFacilityInfo As System.Windows.Forms.ToolBar
    Friend WithEvents TbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbFind As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbForward As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbAddMemo As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbDelete As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DTPDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents DTPDateClosed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GBRecordStatus As System.Windows.Forms.GroupBox
    Friend WithEvents rdbCloseReport As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOpenReport As System.Windows.Forms.RadioButton
    Friend WithEvents chbOverright As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents LLFacilityName As System.Windows.Forms.LinkLabel
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents cboFacilityName As System.Windows.Forms.ComboBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityState As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityZipCode As System.Windows.Forms.TextBox
    Friend WithEvents cboAIRSNumber As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents DTPTestDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDaysInAPB As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents DTPTestDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboPollutant As System.Windows.Forms.ComboBox
    Friend WithEvents txtEmissionSource As System.Windows.Forms.TextBox
    Friend WithEvents cboTestingFirms As System.Windows.Forms.ComboBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnInsert As System.Windows.Forms.Button
    Friend WithEvents dgvFacilityInfo As System.Windows.Forms.DataGridView
    Friend WithEvents btnDeleteTestReport As System.Windows.Forms.Button
    Friend WithEvents clbReferenceNumbers As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnCloseTestReport As System.Windows.Forms.Button
    Friend WithEvents btnClearReferenceNumber As System.Windows.Forms.Button
    Friend WithEvents TCTestReports As System.Windows.Forms.TabControl
    Friend WithEvents TPNewTestReports As System.Windows.Forms.TabPage
    Friend WithEvents TPHistoricalReports As System.Windows.Forms.TabPage
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents DTPAddTestReportDateCompleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents btnClearAddTestReport As System.Windows.Forms.Button
    Friend WithEvents mtbAddTestReportAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dtpAddTestReportDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAddTestReportCommissioner As System.Windows.Forms.TextBox
    Friend WithEvents txtAddTestReportDirector As System.Windows.Forms.TextBox
    Friend WithEvents txtAddTestReportProgramManager As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtAddTestReportRefNum As System.Windows.Forms.TextBox
    Friend WithEvents btnAddTestReport As System.Windows.Forms.Button
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents bgw1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwAIRS As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnReOpenHistoricTestReport As System.Windows.Forms.Button
    Friend WithEvents btnCloseHistoricTestReport As System.Windows.Forms.Button
    Friend WithEvents txtCloseTestReportRefNum As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents btnOpenTestReport As System.Windows.Forms.Button
    Friend WithEvents btnSearchForAIRS As System.Windows.Forms.Button
    Friend WithEvents btnLoadCombos As System.Windows.Forms.Button
End Class
