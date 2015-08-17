<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPManagersTools
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents TPUnitStatistics2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label122 As System.Windows.Forms.Label
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents DTPUnitStatsEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPUnitStatsStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRunUnitStatsReport As System.Windows.Forms.Button
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents txtAverageofTotalReviewed As System.Windows.Forms.TextBox
    Friend WithEvents txtCombustionTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtChemicalTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalTests As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTotalReviewed As System.Windows.Forms.TextBox
    Friend WithEvents txtEngineerCount As System.Windows.Forms.TextBox
    Friend WithEvents txtAverageMedianDays As System.Windows.Forms.TextBox
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents txtPercentialAverage As System.Windows.Forms.TextBox
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents Label138 As System.Windows.Forms.Label
    Friend WithEvents txtAverageWitnessed As System.Windows.Forms.TextBox
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents lblComTests As System.Windows.Forms.LinkLabel
    Friend WithEvents lblChemTests As System.Windows.Forms.LinkLabel
    Friend WithEvents dgvUnitStats As System.Windows.Forms.DataGridView
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents txtUnitStatsCount As System.Windows.Forms.TextBox
    Friend WithEvents btnViewTestReport As System.Windows.Forms.Button
    Friend WithEvents txtUnitStatReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents TPMiscTools As System.Windows.Forms.TabPage
    Friend WithEvents TCMiscTools As System.Windows.Forms.TabControl
    Friend WithEvents TPMethods As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents dgvMethods As System.Windows.Forms.DataGridView
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnUpdateMethods As System.Windows.Forms.Button
    Friend WithEvents txtMethodNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtMethodDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtMethodCode As System.Windows.Forms.TextBox
    Friend WithEvents TPTestReportAdd As System.Windows.Forms.TabPage
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtAddTestReportRefNum As System.Windows.Forms.TextBox
    Friend WithEvents btnAddTestReport As System.Windows.Forms.Button
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtAddTestReportCommissioner As System.Windows.Forms.TextBox
    Friend WithEvents txtAddTestReportDirector As System.Windows.Forms.TextBox
    Friend WithEvents txtAddTestReportProgramManager As System.Windows.Forms.TextBox
    Friend WithEvents dtpAddTestReportDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents mtbAddTestReportAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnClearAddTestReport As System.Windows.Forms.Button
    Friend WithEvents DTPAddTestReportDateCompleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnReOpenHistoricTestReport As System.Windows.Forms.Button
    Friend WithEvents btnCloseHistoricTestReport As System.Windows.Forms.Button
    Friend WithEvents txtCloseTestReportRefNum As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents chbNonComplianceTestReport As System.Windows.Forms.CheckBox
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents DTPAppStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnApplicationReport As System.Windows.Forms.Button
    Friend WithEvents DTPAppEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRunApplicationReport As System.Windows.Forms.Button
    Friend WithEvents txtISMPApplicationReport As System.Windows.Forms.TextBox
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents TPApplicationsReviewed As System.Windows.Forms.TabPage
    Friend WithEvents llbDownloadExcelFiles As System.Windows.Forms.LinkLabel
    Friend WithEvents llbExportStatsToWord As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents llbRunEngineerStatReport As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEngineerStatistics As System.Windows.Forms.TextBox
    Friend WithEvents dgrTestSummary As System.Windows.Forms.DataGrid
    Friend WithEvents llbPrintSummaryReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Splitter14 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents Splitter13 As System.Windows.Forms.Splitter
    Friend WithEvents dgrExcelFiles As System.Windows.Forms.DataGrid
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents txtNewFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents llbRemoveFile As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents llbAddExcelFile As System.Windows.Forms.LinkLabel
    Friend WithEvents TPExcelFiles As System.Windows.Forms.TabPage
    Friend WithEvents Splitter11 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter10 As System.Windows.Forms.Splitter
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents llbExportToExcel As System.Windows.Forms.LinkLabel
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents llbViewReport As System.Windows.Forms.LinkLabel
    Friend WithEvents dgrEngineersFacilityList As System.Windows.Forms.DataGrid
    Friend WithEvents rdbEngineerTestReportAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEngineerTestReportCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEngineerTestReportReceived As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEngineerTestReportTestDate As System.Windows.Forms.RadioButton
    Friend WithEvents DTPEngineerTestReportStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEngineerTestReportEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPUnitStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPUnitEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents llbEngineerTestReports As System.Windows.Forms.LinkLabel
    Friend WithEvents Splitter9 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter8 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents PanelDate As System.Windows.Forms.Panel
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents clbEngineersList2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents lsbEngineers As System.Windows.Forms.ListBox
    Friend WithEvents TPEngineerTestReport As System.Windows.Forms.TabPage
    Friend WithEvents rdbUnitDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitStatsAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateTestStarted As System.Windows.Forms.RadioButton
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LlbUnitStatistics As System.Windows.Forms.LinkLabel
    Friend WithEvents Splitter7 As System.Windows.Forms.Splitter
    Friend WithEvents txtOpenFiles3 As System.Windows.Forms.TextBox
    Friend WithEvents txtClosed3 As System.Windows.Forms.TextBox
    Friend WithEvents txtWitnessed3 As System.Windows.Forms.TextBox
    Friend WithEvents txtFilesOpen3 As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtDaysOpen3 As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtOpenFilesTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtFilesOpenTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtWitnessedTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtClosedTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtDaysOpen4 As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents PanelAll As System.Windows.Forms.Panel
    Friend WithEvents PanelCombustMineral As System.Windows.Forms.Panel
    Friend WithEvents PanelChemVOC As System.Windows.Forms.Panel
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAIRSNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents rdbStatsAll As System.Windows.Forms.RadioButton
    Friend WithEvents Splitter6 As System.Windows.Forms.Splitter
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtOpenFacility As System.Windows.Forms.TextBox
    Friend WithEvents txtClosedFacility As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtDaysOpenFacility As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityOpenDays As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtCSNotInComplianceOpen As System.Windows.Forms.TextBox
    Friend WithEvents txtIndeterminateOpen As System.Windows.Forms.TextBox
    Friend WithEvents txtCSInComplianceOpen As System.Windows.Forms.TextBox
    Friend WithEvents txtCSInfoOnlyOpen As System.Windows.Forms.TextBox
    Friend WithEvents txtCSFileOpenOpen As System.Windows.Forms.TextBox
    Friend WithEvents txtCSNotInComplianceClosed As System.Windows.Forms.TextBox
    Friend WithEvents txtIndeterminateClosed As System.Windows.Forms.TextBox
    Friend WithEvents txtCSInComplianceClosed As System.Windows.Forms.TextBox
    Friend WithEvents txtCSInfoOnlyClosed As System.Windows.Forms.TextBox
    Friend WithEvents txtCSFileOpenClosed As System.Windows.Forms.TextBox
    Friend WithEvents txtCSNotInComplianceOpenDays As System.Windows.Forms.TextBox
    Friend WithEvents txtIndeterminateOpenDays As System.Windows.Forms.TextBox
    Friend WithEvents txtCSInComplianceOpenDays As System.Windows.Forms.TextBox
    Friend WithEvents txtCSInfoOnlyOpenDays As System.Windows.Forms.TextBox
    Friend WithEvents txtCSFileOpenOpenDays As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter5 As System.Windows.Forms.Splitter
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents llbRunMonthlyReport As System.Windows.Forms.LinkLabel
    Friend WithEvents llbPrintMonthlyReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txt80Percent As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtMedianTimeToComplete As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtPercential As System.Windows.Forms.TextBox
    Friend WithEvents DTPMonthlyStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPMonthlyEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtReceived As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtTestCompleted As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents txtTestOutOfCompliance As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtTestWitnessed As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TPUnitAssignment As System.Windows.Forms.TabPage
    Friend WithEvents lsbFacilities As System.Windows.Forms.ListBox
    Friend WithEvents rdbCombusMineral As System.Windows.Forms.RadioButton
    Friend WithEvents rdbChemVOC As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents LVFacilities As System.Windows.Forms.ListView
    Friend WithEvents DataGridBoolColumn2 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents DataGridBoolColumn1 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents LLBAllFacilities As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chbOneStack3Runs As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Runs As System.Windows.Forms.CheckBox
    Friend WithEvents GBChoose As System.Windows.Forms.GroupBox
    Friend WithEvents TPTestReportStatistics As System.Windows.Forms.TabPage
    Friend WithEvents LLFaciltiySearch As System.Windows.Forms.LinkLabel
    Friend WithEvents rdbFacilityDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityDateTestStarted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityDateCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents LLRunFacilityReport As System.Windows.Forms.LinkLabel
    Friend WithEvents txtFacility As System.Windows.Forms.TextBox
    Friend WithEvents DTPEndDateFacility As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPStartDateFacility As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MmiByEngineer As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllNoDoc As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllMethod22 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllMethod9Multi As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllMethod9Single As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllMemoPTE As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllMemoToFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllMemoStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllPEMS As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllRata As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllGasConcentration As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllPondTreatment As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllFlare As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllLoadingRack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem32 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem31 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem29 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem76 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem75 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem74 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem73 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem72 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem71 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedMethod22 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedMethod9Multi As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedMethod9Single As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedMemoPTE As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedMemoToFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedMemoStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedPEMS As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedRata As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem62 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedGasConcentration As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedPondTreatment As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedFlare As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedLoadingRack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedTwoStackDRE As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedTwoStackStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem49 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllTwoStackDRE As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllTwoStackStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllOneStackFourRuns As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllOneStackThreeRuns As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllOneStackTwoRuns As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedOneStackFourRuns As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedOneStackThreeRuns As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedOneStackTwoRuns As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedNoDocument As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllDocument As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedByUnit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiUnassignedByUnit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAllByUnit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiByUnit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiAssignedTestReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiUnassignedTestReports As System.Windows.Forms.MenuItem
    Friend WithEvents MmiViewTestReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiClearTab As System.Windows.Forms.MenuItem
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents txtTestReportCount As System.Windows.Forms.TextBox
    Friend WithEvents lblTestReportAssignment As System.Windows.Forms.ListBox
    Friend WithEvents SplitterTestReportAssignment As System.Windows.Forms.Splitter
    Friend WithEvents LVTestReportAssignment As System.Windows.Forms.ListView
    Friend WithEvents PanelReportAssignment As System.Windows.Forms.Panel
    Friend WithEvents cboEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents chbMemorandum As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9Multi As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRE As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStack As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Runs As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9Single As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTreatmentPonds As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasTests As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATA As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemorandumToFile As System.Windows.Forms.CheckBox
    Friend WithEvents chbPTE As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRack As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlare As System.Windows.Forms.CheckBox
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents chbPEMS As System.Windows.Forms.CheckBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents cboCounty As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents cboCity As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents clbEngineers1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents clbEngineers2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtOpenFiles1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOpenFiles2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDaysOpen As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtFilesOpen1 As System.Windows.Forms.TextBox
    Friend WithEvents txtFilesOpen2 As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtWitnessed2 As System.Windows.Forms.TextBox
    Friend WithEvents txtWitnessed1 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtClosed1 As System.Windows.Forms.TextBox
    Friend WithEvents txtClosed2 As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtDaysOpen2 As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnRunReport As System.Windows.Forms.Button
    Friend WithEvents txtReportText As System.Windows.Forms.RichTextBox
    Friend WithEvents txtOutOfComplianceReport As System.Windows.Forms.RichTextBox
    Friend WithEvents TCManagersTools As System.Windows.Forms.TabControl
    Friend WithEvents TPAIRSReportsPrinted As System.Windows.Forms.TabPage
    Friend WithEvents TPUnitStatistics As System.Windows.Forms.TabPage
    Friend WithEvents TPMonthlyReport As System.Windows.Forms.TabPage
    Friend WithEvents TPReportAssignment As System.Windows.Forms.TabPage
    Friend WithEvents PanelManagersTools As System.Windows.Forms.Panel
    Friend WithEvents SplitterManagersTools As System.Windows.Forms.Splitter
    Friend WithEvents TbbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbFind As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents TBManagersTools As System.Windows.Forms.ToolBar
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MmiShowToolbar As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiViewByFacility As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiShowDeletedRecords As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMethod22 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMethod9Multi As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMethod9Single As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMemoPTE As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMemoToFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiMemoStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPEMS As System.Windows.Forms.MenuItem
    Friend WithEvents MmiRata As System.Windows.Forms.MenuItem
    Friend WithEvents MmiGasConcentration As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPondTreatment As System.Windows.Forms.MenuItem
    Friend WithEvents MmiFlare As System.Windows.Forms.MenuItem
    Friend WithEvents MmiLoadingRack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiTwoStackDRE As System.Windows.Forms.MenuItem
    Friend WithEvents MmiTwoStackStandard As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOneStackFourRun As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOneStackThreeRun As System.Windows.Forms.MenuItem
    Friend WithEvents MmiOneStackTwoRun As System.Windows.Forms.MenuItem
    Friend WithEvents MmiUnassigned As System.Windows.Forms.MenuItem
    Friend WithEvents MmiViewByTestType As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPManagersTools))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MmiSave = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MmiBack = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MmiExit = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MmiClear = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MmiClearTab = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MmiCut = New System.Windows.Forms.MenuItem()
        Me.MmiCopy = New System.Windows.Forms.MenuItem()
        Me.MmiPaste = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.MmiDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MmiViewTestReports = New System.Windows.Forms.MenuItem()
        Me.MmiUnassignedTestReports = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedTestReports = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.MmiViewByTestType = New System.Windows.Forms.MenuItem()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.MmiUnassigned = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.MmiOneStackTwoRun = New System.Windows.Forms.MenuItem()
        Me.MmiOneStackThreeRun = New System.Windows.Forms.MenuItem()
        Me.MmiOneStackFourRun = New System.Windows.Forms.MenuItem()
        Me.MenuItem24 = New System.Windows.Forms.MenuItem()
        Me.MmiTwoStackStandard = New System.Windows.Forms.MenuItem()
        Me.MmiTwoStackDRE = New System.Windows.Forms.MenuItem()
        Me.MenuItem23 = New System.Windows.Forms.MenuItem()
        Me.MmiLoadingRack = New System.Windows.Forms.MenuItem()
        Me.MmiFlare = New System.Windows.Forms.MenuItem()
        Me.MenuItem22 = New System.Windows.Forms.MenuItem()
        Me.MmiPondTreatment = New System.Windows.Forms.MenuItem()
        Me.MmiGasConcentration = New System.Windows.Forms.MenuItem()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.MmiRata = New System.Windows.Forms.MenuItem()
        Me.MmiPEMS = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.MmiMemoStandard = New System.Windows.Forms.MenuItem()
        Me.MmiMemoToFile = New System.Windows.Forms.MenuItem()
        Me.MmiMemoPTE = New System.Windows.Forms.MenuItem()
        Me.MenuItem19 = New System.Windows.Forms.MenuItem()
        Me.MmiMethod9Single = New System.Windows.Forms.MenuItem()
        Me.MmiMethod9Multi = New System.Windows.Forms.MenuItem()
        Me.MmiMethod22 = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedNoDocument = New System.Windows.Forms.MenuItem()
        Me.MenuItem71 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedOneStackTwoRuns = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedOneStackThreeRuns = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedOneStackFourRuns = New System.Windows.Forms.MenuItem()
        Me.MenuItem72 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedTwoStackStandard = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedTwoStackDRE = New System.Windows.Forms.MenuItem()
        Me.MenuItem73 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedLoadingRack = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedFlare = New System.Windows.Forms.MenuItem()
        Me.MenuItem74 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedPondTreatment = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedGasConcentration = New System.Windows.Forms.MenuItem()
        Me.MenuItem62 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedRata = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedPEMS = New System.Windows.Forms.MenuItem()
        Me.MenuItem75 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedMemoStandard = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedMemoToFile = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedMemoPTE = New System.Windows.Forms.MenuItem()
        Me.MenuItem76 = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedMethod9Single = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedMethod9Multi = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedMethod22 = New System.Windows.Forms.MenuItem()
        Me.MmiAllDocument = New System.Windows.Forms.MenuItem()
        Me.MmiAllNoDoc = New System.Windows.Forms.MenuItem()
        Me.MenuItem29 = New System.Windows.Forms.MenuItem()
        Me.MmiAllOneStackTwoRuns = New System.Windows.Forms.MenuItem()
        Me.MmiAllOneStackThreeRuns = New System.Windows.Forms.MenuItem()
        Me.MmiAllOneStackFourRuns = New System.Windows.Forms.MenuItem()
        Me.MenuItem30 = New System.Windows.Forms.MenuItem()
        Me.MmiAllTwoStackStandard = New System.Windows.Forms.MenuItem()
        Me.MmiAllTwoStackDRE = New System.Windows.Forms.MenuItem()
        Me.MenuItem31 = New System.Windows.Forms.MenuItem()
        Me.MmiAllLoadingRack = New System.Windows.Forms.MenuItem()
        Me.MmiAllFlare = New System.Windows.Forms.MenuItem()
        Me.MenuItem32 = New System.Windows.Forms.MenuItem()
        Me.MmiAllPondTreatment = New System.Windows.Forms.MenuItem()
        Me.MmiAllGasConcentration = New System.Windows.Forms.MenuItem()
        Me.MenuItem33 = New System.Windows.Forms.MenuItem()
        Me.MmiAllRata = New System.Windows.Forms.MenuItem()
        Me.MmiAllPEMS = New System.Windows.Forms.MenuItem()
        Me.MenuItem49 = New System.Windows.Forms.MenuItem()
        Me.MmiAllMemoStandard = New System.Windows.Forms.MenuItem()
        Me.MmiAllMemoToFile = New System.Windows.Forms.MenuItem()
        Me.MmiAllMemoPTE = New System.Windows.Forms.MenuItem()
        Me.MenuItem34 = New System.Windows.Forms.MenuItem()
        Me.MmiAllMethod9Single = New System.Windows.Forms.MenuItem()
        Me.MmiAllMethod9Multi = New System.Windows.Forms.MenuItem()
        Me.MmiAllMethod22 = New System.Windows.Forms.MenuItem()
        Me.MenuItem14 = New System.Windows.Forms.MenuItem()
        Me.MmiShowDeletedRecords = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.MmiViewByFacility = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MmiByEngineer = New System.Windows.Forms.MenuItem()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.MmiByUnit = New System.Windows.Forms.MenuItem()
        Me.MmiAllByUnit = New System.Windows.Forms.MenuItem()
        Me.MmiUnassignedByUnit = New System.Windows.Forms.MenuItem()
        Me.MmiAssignedByUnit = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MmiShowToolbar = New System.Windows.Forms.MenuItem()
        Me.MmiHelp = New System.Windows.Forms.MenuItem()
        Me.TBManagersTools = New System.Windows.Forms.ToolBar()
        Me.TbbSave = New System.Windows.Forms.ToolBarButton()
        Me.TbbFind = New System.Windows.Forms.ToolBarButton()
        Me.TbbPrint = New System.Windows.Forms.ToolBarButton()
        Me.TbbClear = New System.Windows.Forms.ToolBarButton()
        Me.TbbBack = New System.Windows.Forms.ToolBarButton()
        Me.TbbExit = New System.Windows.Forms.ToolBarButton()
        Me.SplitterManagersTools = New System.Windows.Forms.Splitter()
        Me.PanelManagersTools = New System.Windows.Forms.Panel()
        Me.TCManagersTools = New System.Windows.Forms.TabControl()
        Me.TPReportAssignment = New System.Windows.Forms.TabPage()
        Me.SplitterTestReportAssignment = New System.Windows.Forms.Splitter()
        Me.PanelReportAssignment = New System.Windows.Forms.Panel()
        Me.chbNonComplianceTestReport = New System.Windows.Forms.CheckBox()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.txtTestReportCount = New System.Windows.Forms.TextBox()
        Me.lblTestReportAssignment = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboEngineer = New System.Windows.Forms.ComboBox()
        Me.LVTestReportAssignment = New System.Windows.Forms.ListView()
        Me.TPUnitStatistics = New System.Windows.Forms.TabPage()
        Me.Splitter7 = New System.Windows.Forms.Splitter()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.PanelAll = New System.Windows.Forms.Panel()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtClosedTotal = New System.Windows.Forms.TextBox()
        Me.txtWitnessedTotal = New System.Windows.Forms.TextBox()
        Me.txtFilesOpenTotal = New System.Windows.Forms.TextBox()
        Me.txtOpenFilesTotal = New System.Windows.Forms.TextBox()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtDaysOpen3 = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtFilesOpen3 = New System.Windows.Forms.TextBox()
        Me.txtWitnessed3 = New System.Windows.Forms.TextBox()
        Me.txtClosed3 = New System.Windows.Forms.TextBox()
        Me.txtOpenFiles3 = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtDaysOpen4 = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtEngineerStatistics = New System.Windows.Forms.TextBox()
        Me.PanelCombustMineral = New System.Windows.Forms.Panel()
        Me.GroupBox17 = New System.Windows.Forms.GroupBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtOpenFiles2 = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtFilesOpen2 = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtDaysOpen2 = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtWitnessed2 = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.txtClosed2 = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.clbEngineers2 = New System.Windows.Forms.CheckedListBox()
        Me.PanelChemVOC = New System.Windows.Forms.Panel()
        Me.GroupBox18 = New System.Windows.Forms.GroupBox()
        Me.txtOpenFiles1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFilesOpen1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDaysOpen = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtWitnessed1 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtClosed1 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.clbEngineers1 = New System.Windows.Forms.CheckedListBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.llbExportStatsToWord = New System.Windows.Forms.LinkLabel()
        Me.llbRunEngineerStatReport = New System.Windows.Forms.LinkLabel()
        Me.LlbUnitStatistics = New System.Windows.Forms.LinkLabel()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.rdbUnitStatsAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdbUnitDateCompleted = New System.Windows.Forms.RadioButton()
        Me.rdbUnitDateTestStarted = New System.Windows.Forms.RadioButton()
        Me.rdbUnitDateReceived = New System.Windows.Forms.RadioButton()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.DTPUnitEnd = New System.Windows.Forms.DateTimePicker()
        Me.DTPUnitStart = New System.Windows.Forms.DateTimePicker()
        Me.TPUnitStatistics2 = New System.Windows.Forms.TabPage()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.dgvUnitStats = New System.Windows.Forms.DataGridView()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.btnViewTestReport = New System.Windows.Forms.Button()
        Me.txtUnitStatReferenceNumber = New System.Windows.Forms.TextBox()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.txtUnitStatsCount = New System.Windows.Forms.TextBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.lblComTests = New System.Windows.Forms.LinkLabel()
        Me.lblChemTests = New System.Windows.Forms.LinkLabel()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.txtAverageWitnessed = New System.Windows.Forms.TextBox()
        Me.txtPercentialAverage = New System.Windows.Forms.TextBox()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.txtEngineerCount = New System.Windows.Forms.TextBox()
        Me.txtAverageMedianDays = New System.Windows.Forms.TextBox()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.txtAverageofTotalReviewed = New System.Windows.Forms.TextBox()
        Me.txtCombustionTotal = New System.Windows.Forms.TextBox()
        Me.txtChemicalTotal = New System.Windows.Forms.TextBox()
        Me.lblTotalTests = New System.Windows.Forms.LinkLabel()
        Me.txtTotalReviewed = New System.Windows.Forms.TextBox()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.btnRunUnitStatsReport = New System.Windows.Forms.Button()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.DTPUnitStatsEndDate = New System.Windows.Forms.DateTimePicker()
        Me.DTPUnitStatsStartDate = New System.Windows.Forms.DateTimePicker()
        Me.TPUnitAssignment = New System.Windows.Forms.TabPage()
        Me.Splitter5 = New System.Windows.Forms.Splitter()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lsbFacilities = New System.Windows.Forms.ListBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rdbCombusMineral = New System.Windows.Forms.RadioButton()
        Me.rdbChemVOC = New System.Windows.Forms.RadioButton()
        Me.LLBAllFacilities = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LVFacilities = New System.Windows.Forms.ListView()
        Me.TPTestReportStatistics = New System.Windows.Forms.TabPage()
        Me.Splitter6 = New System.Windows.Forms.Splitter()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.GBChoose = New System.Windows.Forms.GroupBox()
        Me.txtAIRSNumber2 = New System.Windows.Forms.TextBox()
        Me.cboCity = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cboCounty = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.LLFaciltiySearch = New System.Windows.Forms.LinkLabel()
        Me.txtFacility = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbStatsAll = New System.Windows.Forms.RadioButton()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.rdbFacilityDateCompleted = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityDateTestStarted = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityDateReceived = New System.Windows.Forms.RadioButton()
        Me.LLRunFacilityReport = New System.Windows.Forms.LinkLabel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.DTPStartDateFacility = New System.Windows.Forms.DateTimePicker()
        Me.DTPEndDateFacility = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.txtCSFileOpenOpenDays = New System.Windows.Forms.TextBox()
        Me.txtCSInfoOnlyOpenDays = New System.Windows.Forms.TextBox()
        Me.txtCSInComplianceOpenDays = New System.Windows.Forms.TextBox()
        Me.txtIndeterminateOpenDays = New System.Windows.Forms.TextBox()
        Me.txtCSNotInComplianceOpenDays = New System.Windows.Forms.TextBox()
        Me.txtCSFileOpenClosed = New System.Windows.Forms.TextBox()
        Me.txtCSInfoOnlyClosed = New System.Windows.Forms.TextBox()
        Me.txtCSInComplianceClosed = New System.Windows.Forms.TextBox()
        Me.txtIndeterminateClosed = New System.Windows.Forms.TextBox()
        Me.txtCSNotInComplianceClosed = New System.Windows.Forms.TextBox()
        Me.txtCSFileOpenOpen = New System.Windows.Forms.TextBox()
        Me.txtCSInfoOnlyOpen = New System.Windows.Forms.TextBox()
        Me.txtCSInComplianceOpen = New System.Windows.Forms.TextBox()
        Me.txtIndeterminateOpen = New System.Windows.Forms.TextBox()
        Me.txtCSNotInComplianceOpen = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtFacilityOpenDays = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDaysOpenFacility = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtClosedFacility = New System.Windows.Forms.TextBox()
        Me.txtOpenFacility = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TPAIRSReportsPrinted = New System.Windows.Forms.TabPage()
        Me.chbOneStack3Runs = New System.Windows.Forms.CheckBox()
        Me.chbPEMS = New System.Windows.Forms.CheckBox()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.chbFlare = New System.Windows.Forms.CheckBox()
        Me.chbLoadingRack = New System.Windows.Forms.CheckBox()
        Me.chbPTE = New System.Windows.Forms.CheckBox()
        Me.chbMemorandumToFile = New System.Windows.Forms.CheckBox()
        Me.chbRATA = New System.Windows.Forms.CheckBox()
        Me.chbGasTests = New System.Windows.Forms.CheckBox()
        Me.chbTreatmentPonds = New System.Windows.Forms.CheckBox()
        Me.chbMethod22 = New System.Windows.Forms.CheckBox()
        Me.chbMethod9Single = New System.Windows.Forms.CheckBox()
        Me.chbOneStack4Runs = New System.Windows.Forms.CheckBox()
        Me.chbTwoStack = New System.Windows.Forms.CheckBox()
        Me.chbTwoStackDRE = New System.Windows.Forms.CheckBox()
        Me.chbMethod9Multi = New System.Windows.Forms.CheckBox()
        Me.chbMemorandum = New System.Windows.Forms.CheckBox()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.chbOneStack2Runs = New System.Windows.Forms.CheckBox()
        Me.TPExcelFiles = New System.Windows.Forms.TabPage()
        Me.Splitter13 = New System.Windows.Forms.Splitter()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.llbDownloadExcelFiles = New System.Windows.Forms.LinkLabel()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.txtNewFileName = New System.Windows.Forms.TextBox()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.llbRemoveFile = New System.Windows.Forms.LinkLabel()
        Me.llbAddExcelFile = New System.Windows.Forms.LinkLabel()
        Me.dgrExcelFiles = New System.Windows.Forms.DataGrid()
        Me.TPEngineerTestReport = New System.Windows.Forms.TabPage()
        Me.Splitter9 = New System.Windows.Forms.Splitter()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.lsbEngineers = New System.Windows.Forms.ListBox()
        Me.Splitter8 = New System.Windows.Forms.Splitter()
        Me.PanelDate = New System.Windows.Forms.Panel()
        Me.Splitter11 = New System.Windows.Forms.Splitter()
        Me.Splitter10 = New System.Windows.Forms.Splitter()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.DTPEngineerTestReportEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.DTPEngineerTestReportStart = New System.Windows.Forms.DateTimePicker()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox()
        Me.llbExportToExcel = New System.Windows.Forms.LinkLabel()
        Me.llbViewReport = New System.Windows.Forms.LinkLabel()
        Me.llbEngineerTestReports = New System.Windows.Forms.LinkLabel()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.clbEngineersList2 = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.rdbEngineerTestReportAll = New System.Windows.Forms.RadioButton()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.rdbEngineerTestReportCompleted = New System.Windows.Forms.RadioButton()
        Me.rdbEngineerTestReportTestDate = New System.Windows.Forms.RadioButton()
        Me.rdbEngineerTestReportReceived = New System.Windows.Forms.RadioButton()
        Me.dgrEngineersFacilityList = New System.Windows.Forms.DataGrid()
        Me.TPApplicationsReviewed = New System.Windows.Forms.TabPage()
        Me.btnApplicationReport = New System.Windows.Forms.Button()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.DTPAppEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.DTPAppStartDate = New System.Windows.Forms.DateTimePicker()
        Me.txtISMPApplicationReport = New System.Windows.Forms.TextBox()
        Me.btnRunApplicationReport = New System.Windows.Forms.Button()
        Me.TPMonthlyReport = New System.Windows.Forms.TabPage()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.llbPrintSummaryReport = New System.Windows.Forms.LinkLabel()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.txtTestWitnessed = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtTestOutOfCompliance = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.txtTestCompleted = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtReceived = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.DTPMonthlyEnd = New System.Windows.Forms.DateTimePicker()
        Me.DTPMonthlyStart = New System.Windows.Forms.DateTimePicker()
        Me.txtPercential = New System.Windows.Forms.TextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtMedianTimeToComplete = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.txt80Percent = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.llbPrintMonthlyReport = New System.Windows.Forms.LinkLabel()
        Me.llbRunMonthlyReport = New System.Windows.Forms.LinkLabel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtReportText = New System.Windows.Forms.RichTextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Splitter14 = New System.Windows.Forms.Splitter()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.dgrTestSummary = New System.Windows.Forms.DataGrid()
        Me.txtOutOfComplianceReport = New System.Windows.Forms.RichTextBox()
        Me.btnRunReport = New System.Windows.Forms.Button()
        Me.TPMiscTools = New System.Windows.Forms.TabPage()
        Me.TCMiscTools = New System.Windows.Forms.TabControl()
        Me.TPMethods = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.dgvMethods = New System.Windows.Forms.DataGridView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtMethodCode = New System.Windows.Forms.TextBox()
        Me.txtMethodDescription = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.btnUpdateMethods = New System.Windows.Forms.Button()
        Me.txtMethodNumber = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.TPTestReportAdd = New System.Windows.Forms.TabPage()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.btnReOpenHistoricTestReport = New System.Windows.Forms.Button()
        Me.btnCloseHistoricTestReport = New System.Windows.Forms.Button()
        Me.txtCloseTestReportRefNum = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.DTPAddTestReportDateCompleted = New System.Windows.Forms.DateTimePicker()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.btnClearAddTestReport = New System.Windows.Forms.Button()
        Me.mtbAddTestReportAIRSNumber = New System.Windows.Forms.MaskedTextBox()
        Me.dtpAddTestReportDateReceived = New System.Windows.Forms.DateTimePicker()
        Me.txtAddTestReportCommissioner = New System.Windows.Forms.TextBox()
        Me.txtAddTestReportDirector = New System.Windows.Forms.TextBox()
        Me.txtAddTestReportProgramManager = New System.Windows.Forms.TextBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtAddTestReportRefNum = New System.Windows.Forms.TextBox()
        Me.btnAddTestReport = New System.Windows.Forms.Button()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridBoolColumn1 = New System.Windows.Forms.DataGridBoolColumn()
        Me.DataGridBoolColumn2 = New System.Windows.Forms.DataGridBoolColumn()
        Me.PanelManagersTools.SuspendLayout()
        Me.TCManagersTools.SuspendLayout()
        Me.TPReportAssignment.SuspendLayout()
        Me.PanelReportAssignment.SuspendLayout()
        Me.TPUnitStatistics.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.PanelAll.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.PanelCombustMineral.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.PanelChemVOC.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.TPUnitStatistics2.SuspendLayout()
        Me.GroupBox19.SuspendLayout()
        CType(Me.dgvUnitStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel15.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.TPUnitAssignment.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TPTestReportStatistics.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.GBChoose.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.TPAIRSReportsPrinted.SuspendLayout()
        Me.TPExcelFiles.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.dgrExcelFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPEngineerTestReport.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.PanelDate.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        CType(Me.dgrEngineersFacilityList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPApplicationsReviewed.SuspendLayout()
        Me.TPMonthlyReport.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        CType(Me.dgrTestSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPMiscTools.SuspendLayout()
        Me.TCMiscTools.SuspendLayout()
        Me.TPMethods.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.dgvMethods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.TPTestReportAdd.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
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
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.MmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem7, Me.MmiBack, Me.MenuItem9, Me.MmiExit})
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
        'MmiBack
        '
        Me.MmiBack.Index = 2
        Me.MmiBack.Text = "&Return to Navigator"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 3
        Me.MenuItem9.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 4
        Me.MmiExit.Text = "E&xit"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiClear, Me.MenuItem8, Me.MmiClearTab, Me.MenuItem6, Me.MmiCut, Me.MmiCopy, Me.MmiPaste, Me.MenuItem11, Me.MmiDelete})
        Me.MenuItem2.Text = "Edit"
        '
        'MmiClear
        '
        Me.MmiClear.Index = 0
        Me.MmiClear.Text = "Clear Page"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 1
        Me.MenuItem8.Text = "-"
        '
        'MmiClearTab
        '
        Me.MmiClearTab.Index = 2
        Me.MmiClearTab.Text = "Clear Tab"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 3
        Me.MenuItem6.Text = "-"
        '
        'MmiCut
        '
        Me.MmiCut.Index = 4
        Me.MmiCut.Text = "Cut"
        '
        'MmiCopy
        '
        Me.MmiCopy.Index = 5
        Me.MmiCopy.Text = "Copy "
        '
        'MmiPaste
        '
        Me.MmiPaste.Index = 6
        Me.MmiPaste.Text = "Paste"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 7
        Me.MenuItem11.Text = "-"
        '
        'MmiDelete
        '
        Me.MmiDelete.Index = 8
        Me.MmiDelete.Text = "Delete Record"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiViewTestReports, Me.MmiUnassignedTestReports, Me.MmiAssignedTestReports, Me.MenuItem12, Me.MmiViewByTestType, Me.MenuItem14, Me.MmiShowDeletedRecords, Me.MenuItem13, Me.MmiViewByFacility, Me.MenuItem10, Me.MmiByEngineer, Me.MenuItem16, Me.MmiByUnit})
        Me.MenuItem3.Text = "View"
        '
        'MmiViewTestReports
        '
        Me.MmiViewTestReports.Index = 0
        Me.MmiViewTestReports.Text = "View All Test Reports"
        '
        'MmiUnassignedTestReports
        '
        Me.MmiUnassignedTestReports.Index = 1
        Me.MmiUnassignedTestReports.Text = "View Unassigned Test Reports"
        '
        'MmiAssignedTestReports
        '
        Me.MmiAssignedTestReports.Index = 2
        Me.MmiAssignedTestReports.Text = "View Assigned Test Reports"
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 3
        Me.MenuItem12.Text = "-"
        '
        'MmiViewByTestType
        '
        Me.MmiViewByTestType.Index = 4
        Me.MmiViewByTestType.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem15, Me.MenuItem17, Me.MmiAllDocument})
        Me.MmiViewByTestType.Text = "View By Test Type"
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 0
        Me.MenuItem15.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiUnassigned, Me.MenuItem25, Me.MmiOneStackTwoRun, Me.MmiOneStackThreeRun, Me.MmiOneStackFourRun, Me.MenuItem24, Me.MmiTwoStackStandard, Me.MmiTwoStackDRE, Me.MenuItem23, Me.MmiLoadingRack, Me.MmiFlare, Me.MenuItem22, Me.MmiPondTreatment, Me.MmiGasConcentration, Me.MenuItem21, Me.MmiRata, Me.MmiPEMS, Me.MenuItem20, Me.MmiMemoStandard, Me.MmiMemoToFile, Me.MmiMemoPTE, Me.MenuItem19, Me.MmiMethod9Single, Me.MmiMethod9Multi, Me.MmiMethod22})
        Me.MenuItem15.Text = "Unassigned Test Reports"
        '
        'MmiUnassigned
        '
        Me.MmiUnassigned.Index = 0
        Me.MmiUnassigned.Text = "Unassigned (Document Type)"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 1
        Me.MenuItem25.Text = "-"
        '
        'MmiOneStackTwoRun
        '
        Me.MmiOneStackTwoRun.Index = 2
        Me.MmiOneStackTwoRun.Text = "One Stack (Two Runs)"
        '
        'MmiOneStackThreeRun
        '
        Me.MmiOneStackThreeRun.Index = 3
        Me.MmiOneStackThreeRun.Text = "One Stack (Three Runs)"
        '
        'MmiOneStackFourRun
        '
        Me.MmiOneStackFourRun.Index = 4
        Me.MmiOneStackFourRun.Text = "One Stack (Four Runs)"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 5
        Me.MenuItem24.Text = "-"
        '
        'MmiTwoStackStandard
        '
        Me.MmiTwoStackStandard.Index = 6
        Me.MmiTwoStackStandard.Text = "Two Stack (Standard)"
        '
        'MmiTwoStackDRE
        '
        Me.MmiTwoStackDRE.Index = 7
        Me.MmiTwoStackDRE.Text = "Two Stack (DRE)"
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 8
        Me.MenuItem23.Text = "-"
        '
        'MmiLoadingRack
        '
        Me.MmiLoadingRack.Index = 9
        Me.MmiLoadingRack.Text = "Loading Rack"
        '
        'MmiFlare
        '
        Me.MmiFlare.Index = 10
        Me.MmiFlare.Text = "Flare"
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 11
        Me.MenuItem22.Text = "-"
        '
        'MmiPondTreatment
        '
        Me.MmiPondTreatment.Index = 12
        Me.MmiPondTreatment.Text = "Pond Treatment"
        '
        'MmiGasConcentration
        '
        Me.MmiGasConcentration.Index = 13
        Me.MmiGasConcentration.Text = "Gas Concentration"
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 14
        Me.MenuItem21.Text = "-"
        '
        'MmiRata
        '
        Me.MmiRata.Index = 15
        Me.MmiRata.Text = "Rata"
        '
        'MmiPEMS
        '
        Me.MmiPEMS.Index = 16
        Me.MmiPEMS.Text = "PEMS"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 17
        Me.MenuItem20.Text = "-"
        '
        'MmiMemoStandard
        '
        Me.MmiMemoStandard.Index = 18
        Me.MmiMemoStandard.Text = "Memorandum (Standard)"
        '
        'MmiMemoToFile
        '
        Me.MmiMemoToFile.Index = 19
        Me.MmiMemoToFile.Text = "Memorandum (To File)"
        '
        'MmiMemoPTE
        '
        Me.MmiMemoPTE.Index = 20
        Me.MmiMemoPTE.Text = "PTE (Perminate Total Enclosure)"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 21
        Me.MenuItem19.Text = "-"
        '
        'MmiMethod9Single
        '
        Me.MmiMethod9Single.Index = 22
        Me.MmiMethod9Single.Text = "Method 9 (Single)"
        '
        'MmiMethod9Multi
        '
        Me.MmiMethod9Multi.Index = 23
        Me.MmiMethod9Multi.Text = "Method9 (Multi.)"
        '
        'MmiMethod22
        '
        Me.MmiMethod22.Index = 24
        Me.MmiMethod22.Text = "Method 22"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 1
        Me.MenuItem17.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiAssignedNoDocument, Me.MenuItem71, Me.MmiAssignedOneStackTwoRuns, Me.MmiAssignedOneStackThreeRuns, Me.MmiAssignedOneStackFourRuns, Me.MenuItem72, Me.MmiAssignedTwoStackStandard, Me.MmiAssignedTwoStackDRE, Me.MenuItem73, Me.MmiAssignedLoadingRack, Me.MmiAssignedFlare, Me.MenuItem74, Me.MmiAssignedPondTreatment, Me.MmiAssignedGasConcentration, Me.MenuItem62, Me.MmiAssignedRata, Me.MmiAssignedPEMS, Me.MenuItem75, Me.MmiAssignedMemoStandard, Me.MmiAssignedMemoToFile, Me.MmiAssignedMemoPTE, Me.MenuItem76, Me.MmiAssignedMethod9Single, Me.MmiAssignedMethod9Multi, Me.MmiAssignedMethod22})
        Me.MenuItem17.Text = "Assigned Test Reports "
        '
        'MmiAssignedNoDocument
        '
        Me.MmiAssignedNoDocument.Index = 0
        Me.MmiAssignedNoDocument.Text = "Unassigned (Document Type)"
        '
        'MenuItem71
        '
        Me.MenuItem71.Index = 1
        Me.MenuItem71.Text = "-"
        '
        'MmiAssignedOneStackTwoRuns
        '
        Me.MmiAssignedOneStackTwoRuns.Index = 2
        Me.MmiAssignedOneStackTwoRuns.Text = "One Stack (Two Runs)"
        '
        'MmiAssignedOneStackThreeRuns
        '
        Me.MmiAssignedOneStackThreeRuns.Index = 3
        Me.MmiAssignedOneStackThreeRuns.Text = "One Stack (Three Runs)"
        '
        'MmiAssignedOneStackFourRuns
        '
        Me.MmiAssignedOneStackFourRuns.Index = 4
        Me.MmiAssignedOneStackFourRuns.Text = "One Stack (Four Runs)"
        '
        'MenuItem72
        '
        Me.MenuItem72.Index = 5
        Me.MenuItem72.Text = "-"
        '
        'MmiAssignedTwoStackStandard
        '
        Me.MmiAssignedTwoStackStandard.Index = 6
        Me.MmiAssignedTwoStackStandard.Text = "Two Stack (Standard)"
        '
        'MmiAssignedTwoStackDRE
        '
        Me.MmiAssignedTwoStackDRE.Index = 7
        Me.MmiAssignedTwoStackDRE.Text = "Two Stack (DRE)"
        '
        'MenuItem73
        '
        Me.MenuItem73.Index = 8
        Me.MenuItem73.Text = "-"
        '
        'MmiAssignedLoadingRack
        '
        Me.MmiAssignedLoadingRack.Index = 9
        Me.MmiAssignedLoadingRack.Text = "Loading Rack"
        '
        'MmiAssignedFlare
        '
        Me.MmiAssignedFlare.Index = 10
        Me.MmiAssignedFlare.Text = "Flare"
        '
        'MenuItem74
        '
        Me.MenuItem74.Index = 11
        Me.MenuItem74.Text = "-"
        '
        'MmiAssignedPondTreatment
        '
        Me.MmiAssignedPondTreatment.Index = 12
        Me.MmiAssignedPondTreatment.Text = "Pond Treatment"
        '
        'MmiAssignedGasConcentration
        '
        Me.MmiAssignedGasConcentration.Index = 13
        Me.MmiAssignedGasConcentration.Text = "Gas Concentration"
        '
        'MenuItem62
        '
        Me.MenuItem62.Index = 14
        Me.MenuItem62.Text = "-"
        '
        'MmiAssignedRata
        '
        Me.MmiAssignedRata.Index = 15
        Me.MmiAssignedRata.Text = "Rata"
        '
        'MmiAssignedPEMS
        '
        Me.MmiAssignedPEMS.Index = 16
        Me.MmiAssignedPEMS.Text = "PEMS"
        '
        'MenuItem75
        '
        Me.MenuItem75.Index = 17
        Me.MenuItem75.Text = "-"
        '
        'MmiAssignedMemoStandard
        '
        Me.MmiAssignedMemoStandard.Index = 18
        Me.MmiAssignedMemoStandard.Text = "Memorandum (Standard)"
        '
        'MmiAssignedMemoToFile
        '
        Me.MmiAssignedMemoToFile.Index = 19
        Me.MmiAssignedMemoToFile.Text = "Memorandum (To File)"
        '
        'MmiAssignedMemoPTE
        '
        Me.MmiAssignedMemoPTE.Index = 20
        Me.MmiAssignedMemoPTE.Text = "PTE (Perminate Total Enclosure)"
        '
        'MenuItem76
        '
        Me.MenuItem76.Index = 21
        Me.MenuItem76.Text = "-"
        '
        'MmiAssignedMethod9Single
        '
        Me.MmiAssignedMethod9Single.Index = 22
        Me.MmiAssignedMethod9Single.Text = "Method 9 (Single)"
        '
        'MmiAssignedMethod9Multi
        '
        Me.MmiAssignedMethod9Multi.Index = 23
        Me.MmiAssignedMethod9Multi.Text = "Method9 (Multi.)"
        '
        'MmiAssignedMethod22
        '
        Me.MmiAssignedMethod22.Index = 24
        Me.MmiAssignedMethod22.Text = "Method 22"
        '
        'MmiAllDocument
        '
        Me.MmiAllDocument.Index = 2
        Me.MmiAllDocument.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiAllNoDoc, Me.MenuItem29, Me.MmiAllOneStackTwoRuns, Me.MmiAllOneStackThreeRuns, Me.MmiAllOneStackFourRuns, Me.MenuItem30, Me.MmiAllTwoStackStandard, Me.MmiAllTwoStackDRE, Me.MenuItem31, Me.MmiAllLoadingRack, Me.MmiAllFlare, Me.MenuItem32, Me.MmiAllPondTreatment, Me.MmiAllGasConcentration, Me.MenuItem33, Me.MmiAllRata, Me.MmiAllPEMS, Me.MenuItem49, Me.MmiAllMemoStandard, Me.MmiAllMemoToFile, Me.MmiAllMemoPTE, Me.MenuItem34, Me.MmiAllMethod9Single, Me.MmiAllMethod9Multi, Me.MmiAllMethod22})
        Me.MmiAllDocument.Text = "All Test Reports"
        '
        'MmiAllNoDoc
        '
        Me.MmiAllNoDoc.Index = 0
        Me.MmiAllNoDoc.Text = "Unassigned (Document Type)"
        '
        'MenuItem29
        '
        Me.MenuItem29.Index = 1
        Me.MenuItem29.Text = "-"
        '
        'MmiAllOneStackTwoRuns
        '
        Me.MmiAllOneStackTwoRuns.Index = 2
        Me.MmiAllOneStackTwoRuns.Text = "One Stack (Two Runs)"
        '
        'MmiAllOneStackThreeRuns
        '
        Me.MmiAllOneStackThreeRuns.Index = 3
        Me.MmiAllOneStackThreeRuns.Text = "One Stack (Three Runs)"
        '
        'MmiAllOneStackFourRuns
        '
        Me.MmiAllOneStackFourRuns.Index = 4
        Me.MmiAllOneStackFourRuns.Text = "One Stack (Four Runs)"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 5
        Me.MenuItem30.Text = "-"
        '
        'MmiAllTwoStackStandard
        '
        Me.MmiAllTwoStackStandard.Index = 6
        Me.MmiAllTwoStackStandard.Text = "Two Stack (Standard)"
        '
        'MmiAllTwoStackDRE
        '
        Me.MmiAllTwoStackDRE.Index = 7
        Me.MmiAllTwoStackDRE.Text = "Two Stack (DRE)"
        '
        'MenuItem31
        '
        Me.MenuItem31.Index = 8
        Me.MenuItem31.Text = "-"
        '
        'MmiAllLoadingRack
        '
        Me.MmiAllLoadingRack.Index = 9
        Me.MmiAllLoadingRack.Text = "Loading Rack"
        '
        'MmiAllFlare
        '
        Me.MmiAllFlare.Index = 10
        Me.MmiAllFlare.Text = "Flare"
        '
        'MenuItem32
        '
        Me.MenuItem32.Index = 11
        Me.MenuItem32.Text = "-"
        '
        'MmiAllPondTreatment
        '
        Me.MmiAllPondTreatment.Index = 12
        Me.MmiAllPondTreatment.Text = "Pond Treatment"
        '
        'MmiAllGasConcentration
        '
        Me.MmiAllGasConcentration.Index = 13
        Me.MmiAllGasConcentration.Text = "Gas Concentration"
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = 14
        Me.MenuItem33.Text = "-"
        '
        'MmiAllRata
        '
        Me.MmiAllRata.Index = 15
        Me.MmiAllRata.Text = "Rata"
        '
        'MmiAllPEMS
        '
        Me.MmiAllPEMS.Index = 16
        Me.MmiAllPEMS.Text = "PEMS"
        '
        'MenuItem49
        '
        Me.MenuItem49.Index = 17
        Me.MenuItem49.Text = "-"
        '
        'MmiAllMemoStandard
        '
        Me.MmiAllMemoStandard.Index = 18
        Me.MmiAllMemoStandard.Text = "Memorandum (Standard)"
        '
        'MmiAllMemoToFile
        '
        Me.MmiAllMemoToFile.Index = 19
        Me.MmiAllMemoToFile.Text = "Memorandum (To File)"
        '
        'MmiAllMemoPTE
        '
        Me.MmiAllMemoPTE.Index = 20
        Me.MmiAllMemoPTE.Text = "PTE (Perminate Total Enclosure)"
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 21
        Me.MenuItem34.Text = "-"
        '
        'MmiAllMethod9Single
        '
        Me.MmiAllMethod9Single.Index = 22
        Me.MmiAllMethod9Single.Text = "Method 9 (Single)"
        '
        'MmiAllMethod9Multi
        '
        Me.MmiAllMethod9Multi.Index = 23
        Me.MmiAllMethod9Multi.Text = "Method9 (Multi.)"
        '
        'MmiAllMethod22
        '
        Me.MmiAllMethod22.Index = 24
        Me.MmiAllMethod22.Text = "Method 22"
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 5
        Me.MenuItem14.Text = "-"
        '
        'MmiShowDeletedRecords
        '
        Me.MmiShowDeletedRecords.Index = 6
        Me.MmiShowDeletedRecords.Text = "View Deleted Records"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 7
        Me.MenuItem13.Text = "-"
        '
        'MmiViewByFacility
        '
        Me.MmiViewByFacility.Index = 8
        Me.MmiViewByFacility.Text = "View By Facility"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 9
        Me.MenuItem10.Text = "-"
        '
        'MmiByEngineer
        '
        Me.MmiByEngineer.Index = 10
        Me.MmiByEngineer.Text = "View By Engineer (Coming Soon)"
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 11
        Me.MenuItem16.Text = "-"
        '
        'MmiByUnit
        '
        Me.MmiByUnit.Index = 12
        Me.MmiByUnit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiAllByUnit, Me.MmiUnassignedByUnit, Me.MmiAssignedByUnit})
        Me.MmiByUnit.Text = "By Unit"
        '
        'MmiAllByUnit
        '
        Me.MmiAllByUnit.Index = 0
        Me.MmiAllByUnit.Text = "All Test Reports"
        '
        'MmiUnassignedByUnit
        '
        Me.MmiUnassignedByUnit.Index = 1
        Me.MmiUnassignedByUnit.Text = "Unassigned Test Reports"
        '
        'MmiAssignedByUnit
        '
        Me.MmiAssignedByUnit.Index = 2
        Me.MmiAssignedByUnit.Text = "Assigned Test Reports"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiShowToolbar})
        Me.MenuItem4.Text = "Tools"
        '
        'MmiShowToolbar
        '
        Me.MmiShowToolbar.Index = 0
        Me.MmiShowToolbar.Text = "Show Toolbar"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 4
        Me.MmiHelp.Text = "Help"
        '
        'TBManagersTools
        '
        Me.TBManagersTools.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbSave, Me.TbbFind, Me.TbbPrint, Me.TbbClear, Me.TbbBack, Me.TbbExit})
        Me.TBManagersTools.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBManagersTools.DropDownArrows = True
        Me.TBManagersTools.ImageList = Me.Image_List_All
        Me.TBManagersTools.Location = New System.Drawing.Point(0, 0)
        Me.TBManagersTools.Name = "TBManagersTools"
        Me.TBManagersTools.ShowToolTips = True
        Me.TBManagersTools.Size = New System.Drawing.Size(1142, 28)
        Me.TBManagersTools.TabIndex = 46
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
        'TbbPrint
        '
        Me.TbbPrint.ImageIndex = 56
        Me.TbbPrint.Name = "TbbPrint"
        Me.TbbPrint.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        '
        'TbbClear
        '
        Me.TbbClear.ImageIndex = 84
        Me.TbbClear.Name = "TbbClear"
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
        'SplitterManagersTools
        '
        Me.SplitterManagersTools.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitterManagersTools.Location = New System.Drawing.Point(0, 28)
        Me.SplitterManagersTools.Name = "SplitterManagersTools"
        Me.SplitterManagersTools.Size = New System.Drawing.Size(1142, 4)
        Me.SplitterManagersTools.TabIndex = 231
        Me.SplitterManagersTools.TabStop = False
        '
        'PanelManagersTools
        '
        Me.PanelManagersTools.Controls.Add(Me.TCManagersTools)
        Me.PanelManagersTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelManagersTools.Location = New System.Drawing.Point(0, 32)
        Me.PanelManagersTools.Name = "PanelManagersTools"
        Me.PanelManagersTools.Size = New System.Drawing.Size(1142, 762)
        Me.PanelManagersTools.TabIndex = 232
        '
        'TCManagersTools
        '
        Me.TCManagersTools.Controls.Add(Me.TPReportAssignment)
        Me.TCManagersTools.Controls.Add(Me.TPUnitStatistics)
        Me.TCManagersTools.Controls.Add(Me.TPUnitStatistics2)
        Me.TCManagersTools.Controls.Add(Me.TPUnitAssignment)
        Me.TCManagersTools.Controls.Add(Me.TPTestReportStatistics)
        Me.TCManagersTools.Controls.Add(Me.TPAIRSReportsPrinted)
        Me.TCManagersTools.Controls.Add(Me.TPExcelFiles)
        Me.TCManagersTools.Controls.Add(Me.TPEngineerTestReport)
        Me.TCManagersTools.Controls.Add(Me.TPApplicationsReviewed)
        Me.TCManagersTools.Controls.Add(Me.TPMonthlyReport)
        Me.TCManagersTools.Controls.Add(Me.TPMiscTools)
        Me.TCManagersTools.Controls.Add(Me.TPTestReportAdd)
        Me.TCManagersTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCManagersTools.HotTrack = True
        Me.TCManagersTools.Location = New System.Drawing.Point(0, 0)
        Me.TCManagersTools.Multiline = True
        Me.TCManagersTools.Name = "TCManagersTools"
        Me.TCManagersTools.SelectedIndex = 0
        Me.TCManagersTools.Size = New System.Drawing.Size(1142, 762)
        Me.TCManagersTools.TabIndex = 0
        '
        'TPReportAssignment
        '
        Me.TPReportAssignment.Controls.Add(Me.SplitterTestReportAssignment)
        Me.TPReportAssignment.Controls.Add(Me.PanelReportAssignment)
        Me.TPReportAssignment.Controls.Add(Me.LVTestReportAssignment)
        Me.TPReportAssignment.Location = New System.Drawing.Point(4, 40)
        Me.TPReportAssignment.Name = "TPReportAssignment"
        Me.TPReportAssignment.Size = New System.Drawing.Size(1134, 718)
        Me.TPReportAssignment.TabIndex = 0
        Me.TPReportAssignment.Text = "Test Report Assignment"
        Me.TPReportAssignment.UseVisualStyleBackColor = True
        '
        'SplitterTestReportAssignment
        '
        Me.SplitterTestReportAssignment.BackColor = System.Drawing.SystemColors.HotTrack
        Me.SplitterTestReportAssignment.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitterTestReportAssignment.Location = New System.Drawing.Point(0, 274)
        Me.SplitterTestReportAssignment.MinExtra = 10
        Me.SplitterTestReportAssignment.Name = "SplitterTestReportAssignment"
        Me.SplitterTestReportAssignment.Size = New System.Drawing.Size(1134, 4)
        Me.SplitterTestReportAssignment.TabIndex = 44
        Me.SplitterTestReportAssignment.TabStop = False
        '
        'PanelReportAssignment
        '
        Me.PanelReportAssignment.Controls.Add(Me.chbNonComplianceTestReport)
        Me.PanelReportAssignment.Controls.Add(Me.txtAIRSNumber)
        Me.PanelReportAssignment.Controls.Add(Me.Label107)
        Me.PanelReportAssignment.Controls.Add(Me.txtTestReportCount)
        Me.PanelReportAssignment.Controls.Add(Me.lblTestReportAssignment)
        Me.PanelReportAssignment.Controls.Add(Me.Label1)
        Me.PanelReportAssignment.Controls.Add(Me.cboEngineer)
        Me.PanelReportAssignment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelReportAssignment.Location = New System.Drawing.Point(0, 0)
        Me.PanelReportAssignment.Name = "PanelReportAssignment"
        Me.PanelReportAssignment.Size = New System.Drawing.Size(1134, 278)
        Me.PanelReportAssignment.TabIndex = 43
        '
        'chbNonComplianceTestReport
        '
        Me.chbNonComplianceTestReport.AutoSize = True
        Me.chbNonComplianceTestReport.Location = New System.Drawing.Point(176, 83)
        Me.chbNonComplianceTestReport.Name = "chbNonComplianceTestReport"
        Me.chbNonComplianceTestReport.Size = New System.Drawing.Size(212, 17)
        Me.chbNonComplianceTestReport.TabIndex = 44
        Me.chbNonComplianceTestReport.Text = "Test Report(s) potentially non-compliant"
        Me.chbNonComplianceTestReport.UseVisualStyleBackColor = True
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(440, 34)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(24, 20)
        Me.txtAIRSNumber.TabIndex = 43
        Me.txtAIRSNumber.Visible = False
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Location = New System.Drawing.Point(384, 8)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(41, 13)
        Me.Label107.TabIndex = 41
        Me.Label107.Text = "Count: "
        '
        'txtTestReportCount
        '
        Me.txtTestReportCount.Location = New System.Drawing.Point(440, 8)
        Me.txtTestReportCount.Name = "txtTestReportCount"
        Me.txtTestReportCount.ReadOnly = True
        Me.txtTestReportCount.Size = New System.Drawing.Size(24, 20)
        Me.txtTestReportCount.TabIndex = 40
        Me.txtTestReportCount.Text = "0"
        '
        'lblTestReportAssignment
        '
        Me.lblTestReportAssignment.Location = New System.Drawing.Point(176, 8)
        Me.lblTestReportAssignment.Name = "lblTestReportAssignment"
        Me.lblTestReportAssignment.Size = New System.Drawing.Size(200, 69)
        Me.lblTestReportAssignment.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Engineer: "
        '
        'cboEngineer
        '
        Me.cboEngineer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEngineer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEngineer.Location = New System.Drawing.Point(8, 32)
        Me.cboEngineer.Name = "cboEngineer"
        Me.cboEngineer.Size = New System.Drawing.Size(144, 21)
        Me.cboEngineer.TabIndex = 38
        '
        'LVTestReportAssignment
        '
        Me.LVTestReportAssignment.AllowColumnReorder = True
        Me.LVTestReportAssignment.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LVTestReportAssignment.Location = New System.Drawing.Point(0, 278)
        Me.LVTestReportAssignment.Name = "LVTestReportAssignment"
        Me.LVTestReportAssignment.Size = New System.Drawing.Size(1134, 440)
        Me.LVTestReportAssignment.TabIndex = 41
        Me.LVTestReportAssignment.UseCompatibleStateImageBehavior = False
        '
        'TPUnitStatistics
        '
        Me.TPUnitStatistics.Controls.Add(Me.Splitter7)
        Me.TPUnitStatistics.Controls.Add(Me.GroupBox11)
        Me.TPUnitStatistics.Controls.Add(Me.Panel9)
        Me.TPUnitStatistics.Location = New System.Drawing.Point(4, 40)
        Me.TPUnitStatistics.Name = "TPUnitStatistics"
        Me.TPUnitStatistics.Size = New System.Drawing.Size(1134, 718)
        Me.TPUnitStatistics.TabIndex = 3
        Me.TPUnitStatistics.Text = "Unit Statistics"
        Me.TPUnitStatistics.UseVisualStyleBackColor = True
        '
        'Splitter7
        '
        Me.Splitter7.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter7.Location = New System.Drawing.Point(0, 136)
        Me.Splitter7.Name = "Splitter7"
        Me.Splitter7.Size = New System.Drawing.Size(1134, 5)
        Me.Splitter7.TabIndex = 146
        Me.Splitter7.TabStop = False
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.PanelAll)
        Me.GroupBox11.Controls.Add(Me.PanelCombustMineral)
        Me.GroupBox11.Controls.Add(Me.PanelChemVOC)
        Me.GroupBox11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox11.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox11.Location = New System.Drawing.Point(0, 136)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(1134, 582)
        Me.GroupBox11.TabIndex = 145
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Results"
        '
        'PanelAll
        '
        Me.PanelAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelAll.Controls.Add(Me.GroupBox16)
        Me.PanelAll.Controls.Add(Me.txtEngineerStatistics)
        Me.PanelAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelAll.Location = New System.Drawing.Point(343, 22)
        Me.PanelAll.Name = "PanelAll"
        Me.PanelAll.Size = New System.Drawing.Size(788, 557)
        Me.PanelAll.TabIndex = 2
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.Label57)
        Me.GroupBox16.Controls.Add(Me.Label58)
        Me.GroupBox16.Controls.Add(Me.txtClosedTotal)
        Me.GroupBox16.Controls.Add(Me.txtWitnessedTotal)
        Me.GroupBox16.Controls.Add(Me.txtFilesOpenTotal)
        Me.GroupBox16.Controls.Add(Me.txtOpenFilesTotal)
        Me.GroupBox16.Controls.Add(Me.Label92)
        Me.GroupBox16.Controls.Add(Me.Label91)
        Me.GroupBox16.Controls.Add(Me.Label90)
        Me.GroupBox16.Controls.Add(Me.Label60)
        Me.GroupBox16.Controls.Add(Me.Label59)
        Me.GroupBox16.Controls.Add(Me.Label53)
        Me.GroupBox16.Controls.Add(Me.txtDaysOpen3)
        Me.GroupBox16.Controls.Add(Me.Label48)
        Me.GroupBox16.Controls.Add(Me.Label49)
        Me.GroupBox16.Controls.Add(Me.Label50)
        Me.GroupBox16.Controls.Add(Me.Label51)
        Me.GroupBox16.Controls.Add(Me.txtFilesOpen3)
        Me.GroupBox16.Controls.Add(Me.txtWitnessed3)
        Me.GroupBox16.Controls.Add(Me.txtClosed3)
        Me.GroupBox16.Controls.Add(Me.txtOpenFiles3)
        Me.GroupBox16.Controls.Add(Me.Label54)
        Me.GroupBox16.Controls.Add(Me.txtDaysOpen4)
        Me.GroupBox16.Controls.Add(Me.Label55)
        Me.GroupBox16.Controls.Add(Me.Label56)
        Me.GroupBox16.Controls.Add(Me.Label5)
        Me.GroupBox16.Controls.Add(Me.Label43)
        Me.GroupBox16.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(80, 16)
        Me.GroupBox16.TabIndex = 157
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "Old Method"
        Me.GroupBox16.Visible = False
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(192, 128)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(76, 15)
        Me.Label57.TabIndex = 133
        Me.Label57.Text = "Files Open"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(192, 72)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(76, 15)
        Me.Label58.TabIndex = 132
        Me.Label58.Text = "Open Files"
        '
        'txtClosedTotal
        '
        Me.txtClosedTotal.Location = New System.Drawing.Point(232, 264)
        Me.txtClosedTotal.Name = "txtClosedTotal"
        Me.txtClosedTotal.Size = New System.Drawing.Size(96, 20)
        Me.txtClosedTotal.TabIndex = 131
        '
        'txtWitnessedTotal
        '
        Me.txtWitnessedTotal.Location = New System.Drawing.Point(232, 208)
        Me.txtWitnessedTotal.Name = "txtWitnessedTotal"
        Me.txtWitnessedTotal.Size = New System.Drawing.Size(96, 20)
        Me.txtWitnessedTotal.TabIndex = 130
        '
        'txtFilesOpenTotal
        '
        Me.txtFilesOpenTotal.Location = New System.Drawing.Point(232, 152)
        Me.txtFilesOpenTotal.Name = "txtFilesOpenTotal"
        Me.txtFilesOpenTotal.Size = New System.Drawing.Size(96, 20)
        Me.txtFilesOpenTotal.TabIndex = 129
        '
        'txtOpenFilesTotal
        '
        Me.txtOpenFilesTotal.Location = New System.Drawing.Point(232, 96)
        Me.txtOpenFilesTotal.Name = "txtOpenFilesTotal"
        Me.txtOpenFilesTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtOpenFilesTotal.TabIndex = 128
        '
        'Label92
        '
        Me.Label92.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label92.Location = New System.Drawing.Point(176, 72)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(1, 250)
        Me.Label92.TabIndex = 155
        Me.Label92.Text = "Label92"
        '
        'Label91
        '
        Me.Label91.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label91.Location = New System.Drawing.Point(8, 232)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(550, 1)
        Me.Label91.TabIndex = 154
        Me.Label91.Text = "Label91"
        '
        'Label90
        '
        Me.Label90.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label90.Location = New System.Drawing.Point(8, 176)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(550, 1)
        Me.Label90.TabIndex = 153
        Me.Label90.Text = "Label90"
        '
        'Label60
        '
        Me.Label60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label60.Location = New System.Drawing.Point(16, 288)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(550, 1)
        Me.Label60.TabIndex = 152
        Me.Label60.Text = "Label60"
        '
        'Label59
        '
        Me.Label59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label59.Location = New System.Drawing.Point(16, 120)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(550, 1)
        Me.Label59.TabIndex = 151
        Me.Label59.Text = "Label59"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(128, 128)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(41, 15)
        Me.Label53.TabIndex = 148
        Me.Label53.Text = "-days"
        '
        'txtDaysOpen3
        '
        Me.txtDaysOpen3.Location = New System.Drawing.Point(96, 128)
        Me.txtDaysOpen3.Name = "txtDaysOpen3"
        Me.txtDaysOpen3.ReadOnly = True
        Me.txtDaysOpen3.Size = New System.Drawing.Size(32, 20)
        Me.txtDaysOpen3.TabIndex = 147
        Me.txtDaysOpen3.Text = "50"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(32, 240)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(86, 15)
        Me.Label48.TabIndex = 146
        Me.Label48.Text = "Closed Files"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(32, 184)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(111, 15)
        Me.Label49.TabIndex = 145
        Me.Label49.Text = "Tests Witnessed"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(32, 128)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(76, 15)
        Me.Label50.TabIndex = 144
        Me.Label50.Text = "Files Open"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(32, 72)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(76, 15)
        Me.Label51.TabIndex = 143
        Me.Label51.Text = "Open Files"
        '
        'txtFilesOpen3
        '
        Me.txtFilesOpen3.Location = New System.Drawing.Point(72, 152)
        Me.txtFilesOpen3.Name = "txtFilesOpen3"
        Me.txtFilesOpen3.Size = New System.Drawing.Size(100, 20)
        Me.txtFilesOpen3.TabIndex = 141
        '
        'txtWitnessed3
        '
        Me.txtWitnessed3.Location = New System.Drawing.Point(72, 208)
        Me.txtWitnessed3.Name = "txtWitnessed3"
        Me.txtWitnessed3.Size = New System.Drawing.Size(100, 20)
        Me.txtWitnessed3.TabIndex = 140
        '
        'txtClosed3
        '
        Me.txtClosed3.Location = New System.Drawing.Point(72, 264)
        Me.txtClosed3.Name = "txtClosed3"
        Me.txtClosed3.Size = New System.Drawing.Size(100, 20)
        Me.txtClosed3.TabIndex = 139
        '
        'txtOpenFiles3
        '
        Me.txtOpenFiles3.Location = New System.Drawing.Point(72, 96)
        Me.txtOpenFiles3.Name = "txtOpenFiles3"
        Me.txtOpenFiles3.Size = New System.Drawing.Size(100, 20)
        Me.txtOpenFiles3.TabIndex = 138
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(296, 128)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(41, 15)
        Me.Label54.TabIndex = 137
        Me.Label54.Text = "-days"
        '
        'txtDaysOpen4
        '
        Me.txtDaysOpen4.Location = New System.Drawing.Point(264, 128)
        Me.txtDaysOpen4.Name = "txtDaysOpen4"
        Me.txtDaysOpen4.ReadOnly = True
        Me.txtDaysOpen4.Size = New System.Drawing.Size(32, 20)
        Me.txtDaysOpen4.TabIndex = 136
        Me.txtDaysOpen4.Text = "50"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(192, 240)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(86, 15)
        Me.Label55.TabIndex = 135
        Me.Label55.Text = "Closed Files"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(192, 184)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(111, 15)
        Me.Label56.TabIndex = 134
        Me.Label56.Text = "Tests Witnessed"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(224, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 16)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "Totals"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(32, 24)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(91, 16)
        Me.Label43.TabIndex = 142
        Me.Label43.Text = "Unassigned"
        '
        'txtEngineerStatistics
        '
        Me.txtEngineerStatistics.AcceptsReturn = True
        Me.txtEngineerStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEngineerStatistics.Location = New System.Drawing.Point(0, 0)
        Me.txtEngineerStatistics.Multiline = True
        Me.txtEngineerStatistics.Name = "txtEngineerStatistics"
        Me.txtEngineerStatistics.ReadOnly = True
        Me.txtEngineerStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEngineerStatistics.Size = New System.Drawing.Size(786, 555)
        Me.txtEngineerStatistics.TabIndex = 156
        '
        'PanelCombustMineral
        '
        Me.PanelCombustMineral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelCombustMineral.Controls.Add(Me.GroupBox17)
        Me.PanelCombustMineral.Controls.Add(Me.Label4)
        Me.PanelCombustMineral.Controls.Add(Me.clbEngineers2)
        Me.PanelCombustMineral.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelCombustMineral.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelCombustMineral.Location = New System.Drawing.Point(173, 22)
        Me.PanelCombustMineral.Name = "PanelCombustMineral"
        Me.PanelCombustMineral.Size = New System.Drawing.Size(170, 557)
        Me.PanelCombustMineral.TabIndex = 1
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.Label42)
        Me.GroupBox17.Controls.Add(Me.Label41)
        Me.GroupBox17.Controls.Add(Me.Label16)
        Me.GroupBox17.Controls.Add(Me.Label14)
        Me.GroupBox17.Controls.Add(Me.txtOpenFiles2)
        Me.GroupBox17.Controls.Add(Me.Label47)
        Me.GroupBox17.Controls.Add(Me.txtFilesOpen2)
        Me.GroupBox17.Controls.Add(Me.Label52)
        Me.GroupBox17.Controls.Add(Me.txtDaysOpen2)
        Me.GroupBox17.Controls.Add(Me.Label46)
        Me.GroupBox17.Controls.Add(Me.txtWitnessed2)
        Me.GroupBox17.Controls.Add(Me.Label45)
        Me.GroupBox17.Controls.Add(Me.txtClosed2)
        Me.GroupBox17.Controls.Add(Me.Label44)
        Me.GroupBox17.Location = New System.Drawing.Point(8, 176)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(80, 16)
        Me.GroupBox17.TabIndex = 154
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Old method"
        Me.GroupBox17.Visible = False
        '
        'Label42
        '
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Location = New System.Drawing.Point(8, 240)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(170, 1)
        Me.Label42.TabIndex = 153
        Me.Label42.Text = "Label42"
        '
        'Label41
        '
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Location = New System.Drawing.Point(8, 184)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(170, 1)
        Me.Label41.TabIndex = 152
        Me.Label41.Text = "Label41"
        '
        'Label16
        '
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Location = New System.Drawing.Point(8, 128)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(170, 1)
        Me.Label16.TabIndex = 151
        Me.Label16.Text = "Label16"
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Location = New System.Drawing.Point(8, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(170, 1)
        Me.Label14.TabIndex = 150
        Me.Label14.Text = "Label14"
        '
        'txtOpenFiles2
        '
        Me.txtOpenFiles2.Location = New System.Drawing.Point(72, 48)
        Me.txtOpenFiles2.Name = "txtOpenFiles2"
        Me.txtOpenFiles2.Size = New System.Drawing.Size(100, 20)
        Me.txtOpenFiles2.TabIndex = 71
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(24, 24)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(76, 15)
        Me.Label47.TabIndex = 105
        Me.Label47.Text = "Open Files"
        '
        'txtFilesOpen2
        '
        Me.txtFilesOpen2.Location = New System.Drawing.Point(72, 104)
        Me.txtFilesOpen2.Name = "txtFilesOpen2"
        Me.txtFilesOpen2.Size = New System.Drawing.Size(96, 20)
        Me.txtFilesOpen2.TabIndex = 79
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(128, 80)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(41, 15)
        Me.Label52.TabIndex = 114
        Me.Label52.Text = "-days"
        '
        'txtDaysOpen2
        '
        Me.txtDaysOpen2.Location = New System.Drawing.Point(104, 80)
        Me.txtDaysOpen2.Name = "txtDaysOpen2"
        Me.txtDaysOpen2.Size = New System.Drawing.Size(32, 20)
        Me.txtDaysOpen2.TabIndex = 113
        Me.txtDaysOpen2.Text = "50"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(24, 80)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(76, 15)
        Me.Label46.TabIndex = 106
        Me.Label46.Text = "Files Open"
        '
        'txtWitnessed2
        '
        Me.txtWitnessed2.Location = New System.Drawing.Point(72, 160)
        Me.txtWitnessed2.Name = "txtWitnessed2"
        Me.txtWitnessed2.Size = New System.Drawing.Size(96, 20)
        Me.txtWitnessed2.TabIndex = 85
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(24, 136)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(111, 15)
        Me.Label45.TabIndex = 107
        Me.Label45.Text = "Tests Witnessed"
        '
        'txtClosed2
        '
        Me.txtClosed2.Location = New System.Drawing.Point(72, 216)
        Me.txtClosed2.Name = "txtClosed2"
        Me.txtClosed2.Size = New System.Drawing.Size(96, 20)
        Me.txtClosed2.TabIndex = 91
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(24, 192)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(86, 15)
        Me.Label44.TabIndex = 108
        Me.Label44.Text = "Closed Files"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 16)
        Me.Label4.TabIndex = 65
        Me.Label4.Text = "Combustion And Mineral"
        '
        'clbEngineers2
        '
        Me.clbEngineers2.CheckOnClick = True
        Me.clbEngineers2.Location = New System.Drawing.Point(8, 32)
        Me.clbEngineers2.Name = "clbEngineers2"
        Me.clbEngineers2.Size = New System.Drawing.Size(152, 109)
        Me.clbEngineers2.TabIndex = 67
        '
        'PanelChemVOC
        '
        Me.PanelChemVOC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelChemVOC.Controls.Add(Me.GroupBox18)
        Me.PanelChemVOC.Controls.Add(Me.Label3)
        Me.PanelChemVOC.Controls.Add(Me.clbEngineers1)
        Me.PanelChemVOC.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelChemVOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelChemVOC.Location = New System.Drawing.Point(3, 22)
        Me.PanelChemVOC.Name = "PanelChemVOC"
        Me.PanelChemVOC.Size = New System.Drawing.Size(170, 557)
        Me.PanelChemVOC.TabIndex = 0
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.txtOpenFiles1)
        Me.GroupBox18.Controls.Add(Me.Label6)
        Me.GroupBox18.Controls.Add(Me.txtFilesOpen1)
        Me.GroupBox18.Controls.Add(Me.Label10)
        Me.GroupBox18.Controls.Add(Me.txtDaysOpen)
        Me.GroupBox18.Controls.Add(Me.Label9)
        Me.GroupBox18.Controls.Add(Me.txtWitnessed1)
        Me.GroupBox18.Controls.Add(Me.Label13)
        Me.GroupBox18.Controls.Add(Me.txtClosed1)
        Me.GroupBox18.Controls.Add(Me.Label15)
        Me.GroupBox18.Controls.Add(Me.Label7)
        Me.GroupBox18.Controls.Add(Me.Label12)
        Me.GroupBox18.Controls.Add(Me.Label11)
        Me.GroupBox18.Controls.Add(Me.Label8)
        Me.GroupBox18.Location = New System.Drawing.Point(8, 176)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(88, 16)
        Me.GroupBox18.TabIndex = 153
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "Old Method"
        Me.GroupBox18.Visible = False
        '
        'txtOpenFiles1
        '
        Me.txtOpenFiles1.Location = New System.Drawing.Point(56, 48)
        Me.txtOpenFiles1.Name = "txtOpenFiles1"
        Me.txtOpenFiles1.Size = New System.Drawing.Size(96, 20)
        Me.txtOpenFiles1.TabIndex = 70
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 15)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "Open Files"
        '
        'txtFilesOpen1
        '
        Me.txtFilesOpen1.Location = New System.Drawing.Point(56, 104)
        Me.txtFilesOpen1.Name = "txtFilesOpen1"
        Me.txtFilesOpen1.Size = New System.Drawing.Size(96, 20)
        Me.txtFilesOpen1.TabIndex = 78
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(120, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 15)
        Me.Label10.TabIndex = 77
        Me.Label10.Text = "-days"
        '
        'txtDaysOpen
        '
        Me.txtDaysOpen.Location = New System.Drawing.Point(88, 80)
        Me.txtDaysOpen.Name = "txtDaysOpen"
        Me.txtDaysOpen.Size = New System.Drawing.Size(32, 20)
        Me.txtDaysOpen.TabIndex = 76
        Me.txtDaysOpen.Text = "50"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 15)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "Files Open"
        '
        'txtWitnessed1
        '
        Me.txtWitnessed1.Location = New System.Drawing.Point(56, 160)
        Me.txtWitnessed1.Name = "txtWitnessed1"
        Me.txtWitnessed1.Size = New System.Drawing.Size(96, 20)
        Me.txtWitnessed1.TabIndex = 86
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(16, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(111, 15)
        Me.Label13.TabIndex = 83
        Me.Label13.Text = "Tests Witnessed"
        '
        'txtClosed1
        '
        Me.txtClosed1.Location = New System.Drawing.Point(56, 216)
        Me.txtClosed1.Name = "txtClosed1"
        Me.txtClosed1.Size = New System.Drawing.Size(96, 20)
        Me.txtClosed1.TabIndex = 89
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(16, 192)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(86, 15)
        Me.Label15.TabIndex = 88
        Me.Label15.Text = "Closed Files"
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(0, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 1)
        Me.Label7.TabIndex = 149
        Me.Label7.Text = "Label7"
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Location = New System.Drawing.Point(0, 128)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(170, 1)
        Me.Label12.TabIndex = 152
        Me.Label12.Text = "Label12"
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Location = New System.Drawing.Point(0, 240)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(170, 1)
        Me.Label11.TabIndex = 151
        Me.Label11.Text = "Label11"
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(0, 184)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 1)
        Me.Label8.TabIndex = 150
        Me.Label8.Text = "Label8"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 16)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "Chemical And VOC"
        '
        'clbEngineers1
        '
        Me.clbEngineers1.CheckOnClick = True
        Me.clbEngineers1.Location = New System.Drawing.Point(8, 32)
        Me.clbEngineers1.Name = "clbEngineers1"
        Me.clbEngineers1.Size = New System.Drawing.Size(152, 109)
        Me.clbEngineers1.TabIndex = 66
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.llbExportStatsToWord)
        Me.Panel9.Controls.Add(Me.llbRunEngineerStatReport)
        Me.Panel9.Controls.Add(Me.LlbUnitStatistics)
        Me.Panel9.Controls.Add(Me.GroupBox10)
        Me.Panel9.Controls.Add(Me.Label29)
        Me.Panel9.Controls.Add(Me.Label28)
        Me.Panel9.Controls.Add(Me.DTPUnitEnd)
        Me.Panel9.Controls.Add(Me.DTPUnitStart)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1134, 136)
        Me.Panel9.TabIndex = 144
        '
        'llbExportStatsToWord
        '
        Me.llbExportStatsToWord.AutoSize = True
        Me.llbExportStatsToWord.Location = New System.Drawing.Point(240, 80)
        Me.llbExportStatsToWord.Name = "llbExportStatsToWord"
        Me.llbExportStatsToWord.Size = New System.Drawing.Size(116, 13)
        Me.llbExportStatsToWord.TabIndex = 146
        Me.llbExportStatsToWord.TabStop = True
        Me.llbExportStatsToWord.Text = "Export Results to Word"
        '
        'llbRunEngineerStatReport
        '
        Me.llbRunEngineerStatReport.AutoSize = True
        Me.llbRunEngineerStatReport.Location = New System.Drawing.Point(240, 56)
        Me.llbRunEngineerStatReport.Name = "llbRunEngineerStatReport"
        Me.llbRunEngineerStatReport.Size = New System.Drawing.Size(65, 13)
        Me.llbRunEngineerStatReport.TabIndex = 145
        Me.llbRunEngineerStatReport.TabStop = True
        Me.llbRunEngineerStatReport.Text = "Run Report "
        '
        'LlbUnitStatistics
        '
        Me.LlbUnitStatistics.AutoSize = True
        Me.LlbUnitStatistics.Location = New System.Drawing.Point(240, 112)
        Me.LlbUnitStatistics.Name = "LlbUnitStatistics"
        Me.LlbUnitStatistics.Size = New System.Drawing.Size(62, 13)
        Me.LlbUnitStatistics.TabIndex = 144
        Me.LlbUnitStatistics.TabStop = True
        Me.LlbUnitStatistics.Text = "Run Report"
        Me.LlbUnitStatistics.Visible = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.rdbUnitStatsAll)
        Me.GroupBox10.Controls.Add(Me.Label2)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateCompleted)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateTestStarted)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateReceived)
        Me.GroupBox10.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(232, 128)
        Me.GroupBox10.TabIndex = 143
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Date Bias"
        '
        'rdbUnitStatsAll
        '
        Me.rdbUnitStatsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitStatsAll.Location = New System.Drawing.Point(8, 88)
        Me.rdbUnitStatsAll.Name = "rdbUnitStatsAll"
        Me.rdbUnitStatsAll.Size = New System.Drawing.Size(104, 16)
        Me.rdbUnitStatsAll.TabIndex = 152
        Me.rdbUnitStatsAll.Text = "All Test Reports"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(185, 13)
        Me.Label2.TabIndex = 153
        Me.Label2.Text = "(All Dates - Excluding SM23 Archives)"
        '
        'rdbUnitDateCompleted
        '
        Me.rdbUnitDateCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateCompleted.Location = New System.Drawing.Point(8, 64)
        Me.rdbUnitDateCompleted.Name = "rdbUnitDateCompleted"
        Me.rdbUnitDateCompleted.Size = New System.Drawing.Size(144, 24)
        Me.rdbUnitDateCompleted.TabIndex = 2
        Me.rdbUnitDateCompleted.Text = "Date Report Completed"
        '
        'rdbUnitDateTestStarted
        '
        Me.rdbUnitDateTestStarted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateTestStarted.Location = New System.Drawing.Point(8, 16)
        Me.rdbUnitDateTestStarted.Name = "rdbUnitDateTestStarted"
        Me.rdbUnitDateTestStarted.Size = New System.Drawing.Size(136, 24)
        Me.rdbUnitDateTestStarted.TabIndex = 1
        Me.rdbUnitDateTestStarted.Text = "Date Test Started"
        '
        'rdbUnitDateReceived
        '
        Me.rdbUnitDateReceived.Checked = True
        Me.rdbUnitDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateReceived.Location = New System.Drawing.Point(8, 40)
        Me.rdbUnitDateReceived.Name = "rdbUnitDateReceived"
        Me.rdbUnitDateReceived.Size = New System.Drawing.Size(104, 24)
        Me.rdbUnitDateReceived.TabIndex = 0
        Me.rdbUnitDateReceived.TabStop = True
        Me.rdbUnitDateReceived.Text = "Date Received"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(360, 8)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(55, 13)
        Me.Label29.TabIndex = 97
        Me.Label29.Text = "End Date "
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(240, 8)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(55, 13)
        Me.Label28.TabIndex = 96
        Me.Label28.Text = "Start Date"
        '
        'DTPUnitEnd
        '
        Me.DTPUnitEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitEnd.Location = New System.Drawing.Point(376, 24)
        Me.DTPUnitEnd.Name = "DTPUnitEnd"
        Me.DTPUnitEnd.Size = New System.Drawing.Size(112, 20)
        Me.DTPUnitEnd.TabIndex = 95
        Me.DTPUnitEnd.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'DTPUnitStart
        '
        Me.DTPUnitStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitStart.Location = New System.Drawing.Point(256, 24)
        Me.DTPUnitStart.Name = "DTPUnitStart"
        Me.DTPUnitStart.Size = New System.Drawing.Size(104, 20)
        Me.DTPUnitStart.TabIndex = 94
        Me.DTPUnitStart.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'TPUnitStatistics2
        '
        Me.TPUnitStatistics2.Controls.Add(Me.GroupBox19)
        Me.TPUnitStatistics2.Controls.Add(Me.Panel13)
        Me.TPUnitStatistics2.Location = New System.Drawing.Point(4, 40)
        Me.TPUnitStatistics2.Name = "TPUnitStatistics2"
        Me.TPUnitStatistics2.Size = New System.Drawing.Size(1134, 718)
        Me.TPUnitStatistics2.TabIndex = 12
        Me.TPUnitStatistics2.Text = "Unit Statistics 2"
        Me.TPUnitStatistics2.UseVisualStyleBackColor = True
        '
        'GroupBox19
        '
        Me.GroupBox19.Controls.Add(Me.dgvUnitStats)
        Me.GroupBox19.Controls.Add(Me.Panel15)
        Me.GroupBox19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox19.Location = New System.Drawing.Point(342, 0)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(792, 718)
        Me.GroupBox19.TabIndex = 1
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "View Test Report(s)"
        '
        'dgvUnitStats
        '
        Me.dgvUnitStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUnitStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUnitStats.Location = New System.Drawing.Point(3, 80)
        Me.dgvUnitStats.Name = "dgvUnitStats"
        Me.dgvUnitStats.ReadOnly = True
        Me.dgvUnitStats.Size = New System.Drawing.Size(786, 635)
        Me.dgvUnitStats.TabIndex = 2
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.btnViewTestReport)
        Me.Panel15.Controls.Add(Me.txtUnitStatReferenceNumber)
        Me.Panel15.Controls.Add(Me.Label127)
        Me.Panel15.Controls.Add(Me.txtUnitStatsCount)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel15.Location = New System.Drawing.Point(3, 16)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(786, 64)
        Me.Panel15.TabIndex = 1
        '
        'btnViewTestReport
        '
        Me.btnViewTestReport.AutoSize = True
        Me.btnViewTestReport.Location = New System.Drawing.Point(89, 27)
        Me.btnViewTestReport.Name = "btnViewTestReport"
        Me.btnViewTestReport.Size = New System.Drawing.Size(99, 23)
        Me.btnViewTestReport.TabIndex = 143
        Me.btnViewTestReport.Text = "View Test Report"
        Me.btnViewTestReport.UseVisualStyleBackColor = True
        '
        'txtUnitStatReferenceNumber
        '
        Me.txtUnitStatReferenceNumber.Location = New System.Drawing.Point(3, 29)
        Me.txtUnitStatReferenceNumber.Name = "txtUnitStatReferenceNumber"
        Me.txtUnitStatReferenceNumber.ReadOnly = True
        Me.txtUnitStatReferenceNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtUnitStatReferenceNumber.TabIndex = 142
        '
        'Label127
        '
        Me.Label127.AutoSize = True
        Me.Label127.Location = New System.Drawing.Point(48, 10)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(35, 13)
        Me.Label127.TabIndex = 141
        Me.Label127.Text = "Count"
        '
        'txtUnitStatsCount
        '
        Me.txtUnitStatsCount.Location = New System.Drawing.Point(3, 3)
        Me.txtUnitStatsCount.Name = "txtUnitStatsCount"
        Me.txtUnitStatsCount.ReadOnly = True
        Me.txtUnitStatsCount.Size = New System.Drawing.Size(39, 20)
        Me.txtUnitStatsCount.TabIndex = 140
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.lblComTests)
        Me.Panel13.Controls.Add(Me.lblChemTests)
        Me.Panel13.Controls.Add(Me.Label126)
        Me.Panel13.Controls.Add(Me.txtAverageWitnessed)
        Me.Panel13.Controls.Add(Me.txtPercentialAverage)
        Me.Panel13.Controls.Add(Me.Label140)
        Me.Panel13.Controls.Add(Me.Label139)
        Me.Panel13.Controls.Add(Me.Label138)
        Me.Panel13.Controls.Add(Me.txtEngineerCount)
        Me.Panel13.Controls.Add(Me.txtAverageMedianDays)
        Me.Panel13.Controls.Add(Me.Label130)
        Me.Panel13.Controls.Add(Me.Label129)
        Me.Panel13.Controls.Add(Me.txtAverageofTotalReviewed)
        Me.Panel13.Controls.Add(Me.txtCombustionTotal)
        Me.Panel13.Controls.Add(Me.txtChemicalTotal)
        Me.Panel13.Controls.Add(Me.lblTotalTests)
        Me.Panel13.Controls.Add(Me.txtTotalReviewed)
        Me.Panel13.Controls.Add(Me.Label125)
        Me.Panel13.Controls.Add(Me.Label124)
        Me.Panel13.Controls.Add(Me.btnRunUnitStatsReport)
        Me.Panel13.Controls.Add(Me.Label122)
        Me.Panel13.Controls.Add(Me.Label123)
        Me.Panel13.Controls.Add(Me.DTPUnitStatsEndDate)
        Me.Panel13.Controls.Add(Me.DTPUnitStatsStartDate)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(342, 718)
        Me.Panel13.TabIndex = 0
        '
        'lblComTests
        '
        Me.lblComTests.AutoSize = True
        Me.lblComTests.Location = New System.Drawing.Point(279, 150)
        Me.lblComTests.Name = "lblComTests"
        Me.lblComTests.Size = New System.Drawing.Size(30, 13)
        Me.lblComTests.TabIndex = 142
        Me.lblComTests.TabStop = True
        Me.lblComTests.Text = "View"
        '
        'lblChemTests
        '
        Me.lblChemTests.AutoSize = True
        Me.lblChemTests.Location = New System.Drawing.Point(279, 127)
        Me.lblChemTests.Name = "lblChemTests"
        Me.lblChemTests.Size = New System.Drawing.Size(30, 13)
        Me.lblChemTests.TabIndex = 141
        Me.lblChemTests.TabStop = True
        Me.lblChemTests.Text = "View"
        '
        'Label126
        '
        Me.Label126.AutoSize = True
        Me.Label126.Location = New System.Drawing.Point(8, 73)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(76, 13)
        Me.Label126.TabIndex = 140
        Me.Label126.Text = "# of Engineers"
        '
        'txtAverageWitnessed
        '
        Me.txtAverageWitnessed.Location = New System.Drawing.Point(232, 246)
        Me.txtAverageWitnessed.Name = "txtAverageWitnessed"
        Me.txtAverageWitnessed.ReadOnly = True
        Me.txtAverageWitnessed.Size = New System.Drawing.Size(39, 20)
        Me.txtAverageWitnessed.TabIndex = 139
        '
        'txtPercentialAverage
        '
        Me.txtPercentialAverage.Location = New System.Drawing.Point(232, 223)
        Me.txtPercentialAverage.Name = "txtPercentialAverage"
        Me.txtPercentialAverage.ReadOnly = True
        Me.txtPercentialAverage.Size = New System.Drawing.Size(39, 20)
        Me.txtPercentialAverage.TabIndex = 138
        '
        'Label140
        '
        Me.Label140.AutoSize = True
        Me.Label140.Location = New System.Drawing.Point(8, 181)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(137, 13)
        Me.Label140.TabIndex = 137
        Me.Label140.Text = "Average of Total Reviewed"
        '
        'Label139
        '
        Me.Label139.AutoSize = True
        Me.Label139.Location = New System.Drawing.Point(15, 150)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(187, 13)
        Me.Label139.TabIndex = 136
        Me.Label139.Text = "# Reviewed - Combustion and Mineral"
        '
        'Label138
        '
        Me.Label138.AutoSize = True
        Me.Label138.Location = New System.Drawing.Point(15, 127)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(163, 13)
        Me.Label138.TabIndex = 135
        Me.Label138.Text = "# Reviewed - Chemical and VOC"
        '
        'txtEngineerCount
        '
        Me.txtEngineerCount.Location = New System.Drawing.Point(232, 69)
        Me.txtEngineerCount.Name = "txtEngineerCount"
        Me.txtEngineerCount.ReadOnly = True
        Me.txtEngineerCount.Size = New System.Drawing.Size(39, 20)
        Me.txtEngineerCount.TabIndex = 129
        '
        'txtAverageMedianDays
        '
        Me.txtAverageMedianDays.Location = New System.Drawing.Point(232, 200)
        Me.txtAverageMedianDays.Name = "txtAverageMedianDays"
        Me.txtAverageMedianDays.ReadOnly = True
        Me.txtAverageMedianDays.Size = New System.Drawing.Size(39, 20)
        Me.txtAverageMedianDays.TabIndex = 128
        '
        'Label130
        '
        Me.Label130.AutoSize = True
        Me.Label130.Location = New System.Drawing.Point(8, 227)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(149, 13)
        Me.Label130.TabIndex = 127
        Me.Label130.Text = "Average of 80% - Goal (60.00)"
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.Location = New System.Drawing.Point(8, 204)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(191, 13)
        Me.Label129.TabIndex = 126
        Me.Label129.Text = "Average of Median Days - Goal (30.00)"
        '
        'txtAverageofTotalReviewed
        '
        Me.txtAverageofTotalReviewed.Location = New System.Drawing.Point(232, 177)
        Me.txtAverageofTotalReviewed.Name = "txtAverageofTotalReviewed"
        Me.txtAverageofTotalReviewed.ReadOnly = True
        Me.txtAverageofTotalReviewed.Size = New System.Drawing.Size(39, 20)
        Me.txtAverageofTotalReviewed.TabIndex = 125
        '
        'txtCombustionTotal
        '
        Me.txtCombustionTotal.Location = New System.Drawing.Point(232, 146)
        Me.txtCombustionTotal.Name = "txtCombustionTotal"
        Me.txtCombustionTotal.ReadOnly = True
        Me.txtCombustionTotal.Size = New System.Drawing.Size(39, 20)
        Me.txtCombustionTotal.TabIndex = 122
        '
        'txtChemicalTotal
        '
        Me.txtChemicalTotal.Location = New System.Drawing.Point(232, 123)
        Me.txtChemicalTotal.Name = "txtChemicalTotal"
        Me.txtChemicalTotal.ReadOnly = True
        Me.txtChemicalTotal.Size = New System.Drawing.Size(39, 20)
        Me.txtChemicalTotal.TabIndex = 121
        '
        'lblTotalTests
        '
        Me.lblTotalTests.AutoSize = True
        Me.lblTotalTests.Location = New System.Drawing.Point(279, 102)
        Me.lblTotalTests.Name = "lblTotalTests"
        Me.lblTotalTests.Size = New System.Drawing.Size(30, 13)
        Me.lblTotalTests.TabIndex = 120
        Me.lblTotalTests.TabStop = True
        Me.lblTotalTests.Text = "View"
        '
        'txtTotalReviewed
        '
        Me.txtTotalReviewed.Location = New System.Drawing.Point(232, 98)
        Me.txtTotalReviewed.Name = "txtTotalReviewed"
        Me.txtTotalReviewed.ReadOnly = True
        Me.txtTotalReviewed.Size = New System.Drawing.Size(39, 20)
        Me.txtTotalReviewed.TabIndex = 119
        '
        'Label125
        '
        Me.Label125.AutoSize = True
        Me.Label125.Location = New System.Drawing.Point(8, 250)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(189, 13)
        Me.Label125.TabIndex = 107
        Me.Label125.Text = "Average of # Witnessed - Goal (30.00)"
        '
        'Label124
        '
        Me.Label124.AutoSize = True
        Me.Label124.Location = New System.Drawing.Point(8, 102)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(111, 13)
        Me.Label124.TabIndex = 106
        Me.Label124.Text = "Total Tests Reviewed"
        '
        'btnRunUnitStatsReport
        '
        Me.btnRunUnitStatsReport.Location = New System.Drawing.Point(196, 11)
        Me.btnRunUnitStatsReport.Name = "btnRunUnitStatsReport"
        Me.btnRunUnitStatsReport.Size = New System.Drawing.Size(75, 23)
        Me.btnRunUnitStatsReport.TabIndex = 102
        Me.btnRunUnitStatsReport.Text = "Run Report"
        Me.btnRunUnitStatsReport.UseVisualStyleBackColor = True
        '
        'Label122
        '
        Me.Label122.AutoSize = True
        Me.Label122.Location = New System.Drawing.Point(118, 42)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(55, 13)
        Me.Label122.TabIndex = 101
        Me.Label122.Text = "End Date "
        '
        'Label123
        '
        Me.Label123.AutoSize = True
        Me.Label123.Location = New System.Drawing.Point(118, 16)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(55, 13)
        Me.Label123.TabIndex = 100
        Me.Label123.Text = "Start Date"
        '
        'DTPUnitStatsEndDate
        '
        Me.DTPUnitStatsEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitStatsEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitStatsEndDate.Location = New System.Drawing.Point(8, 38)
        Me.DTPUnitStatsEndDate.Name = "DTPUnitStatsEndDate"
        Me.DTPUnitStatsEndDate.Size = New System.Drawing.Size(104, 20)
        Me.DTPUnitStatsEndDate.TabIndex = 99
        Me.DTPUnitStatsEndDate.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'DTPUnitStatsStartDate
        '
        Me.DTPUnitStatsStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitStatsStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitStatsStartDate.Location = New System.Drawing.Point(8, 12)
        Me.DTPUnitStatsStartDate.Name = "DTPUnitStatsStartDate"
        Me.DTPUnitStatsStartDate.Size = New System.Drawing.Size(104, 20)
        Me.DTPUnitStatsStartDate.TabIndex = 98
        Me.DTPUnitStatsStartDate.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'TPUnitAssignment
        '
        Me.TPUnitAssignment.Controls.Add(Me.Splitter5)
        Me.TPUnitAssignment.Controls.Add(Me.Panel7)
        Me.TPUnitAssignment.Controls.Add(Me.GroupBox2)
        Me.TPUnitAssignment.Location = New System.Drawing.Point(4, 40)
        Me.TPUnitAssignment.Name = "TPUnitAssignment"
        Me.TPUnitAssignment.Size = New System.Drawing.Size(1134, 718)
        Me.TPUnitAssignment.TabIndex = 7
        Me.TPUnitAssignment.Text = "Unit Assignments"
        Me.TPUnitAssignment.UseVisualStyleBackColor = True
        '
        'Splitter5
        '
        Me.Splitter5.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter5.Location = New System.Drawing.Point(0, 305)
        Me.Splitter5.Name = "Splitter5"
        Me.Splitter5.Size = New System.Drawing.Size(1134, 5)
        Me.Splitter5.TabIndex = 6
        Me.Splitter5.TabStop = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.lsbFacilities)
        Me.Panel7.Controls.Add(Me.GroupBox3)
        Me.Panel7.Controls.Add(Me.LLBAllFacilities)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1134, 310)
        Me.Panel7.TabIndex = 5
        '
        'lsbFacilities
        '
        Me.lsbFacilities.Location = New System.Drawing.Point(8, 8)
        Me.lsbFacilities.Name = "lsbFacilities"
        Me.lsbFacilities.Size = New System.Drawing.Size(224, 134)
        Me.lsbFacilities.TabIndex = 4
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdbCombusMineral)
        Me.GroupBox3.Controls.Add(Me.rdbChemVOC)
        Me.GroupBox3.Location = New System.Drawing.Point(240, 8)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 56)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Unit Assignment"
        '
        'rdbCombusMineral
        '
        Me.rdbCombusMineral.Location = New System.Drawing.Point(16, 32)
        Me.rdbCombusMineral.Name = "rdbCombusMineral"
        Me.rdbCombusMineral.Size = New System.Drawing.Size(152, 16)
        Me.rdbCombusMineral.TabIndex = 1
        Me.rdbCombusMineral.Text = "Combustion && Mineral"
        '
        'rdbChemVOC
        '
        Me.rdbChemVOC.Location = New System.Drawing.Point(16, 16)
        Me.rdbChemVOC.Name = "rdbChemVOC"
        Me.rdbChemVOC.Size = New System.Drawing.Size(144, 16)
        Me.rdbChemVOC.TabIndex = 0
        Me.rdbChemVOC.Text = "Chemical && VOC "
        '
        'LLBAllFacilities
        '
        Me.LLBAllFacilities.AutoSize = True
        Me.LLBAllFacilities.Location = New System.Drawing.Point(448, 16)
        Me.LLBAllFacilities.Name = "LLBAllFacilities"
        Me.LLBAllFacilities.Size = New System.Drawing.Size(92, 13)
        Me.LLBAllFacilities.TabIndex = 2
        Me.LLBAllFacilities.TabStop = True
        Me.LLBAllFacilities.Text = "List of All Facilities"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LVFacilities)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 310)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1134, 408)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "List of Facilities"
        '
        'LVFacilities
        '
        Me.LVFacilities.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LVFacilities.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LVFacilities.Location = New System.Drawing.Point(3, 22)
        Me.LVFacilities.Name = "LVFacilities"
        Me.LVFacilities.Size = New System.Drawing.Size(1128, 383)
        Me.LVFacilities.TabIndex = 0
        Me.LVFacilities.UseCompatibleStateImageBehavior = False
        '
        'TPTestReportStatistics
        '
        Me.TPTestReportStatistics.Controls.Add(Me.Splitter6)
        Me.TPTestReportStatistics.Controls.Add(Me.Panel8)
        Me.TPTestReportStatistics.Controls.Add(Me.GroupBox9)
        Me.TPTestReportStatistics.Location = New System.Drawing.Point(4, 40)
        Me.TPTestReportStatistics.Name = "TPTestReportStatistics"
        Me.TPTestReportStatistics.Size = New System.Drawing.Size(1134, 718)
        Me.TPTestReportStatistics.TabIndex = 4
        Me.TPTestReportStatistics.Text = "Test Report Statistics"
        Me.TPTestReportStatistics.UseVisualStyleBackColor = True
        '
        'Splitter6
        '
        Me.Splitter6.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter6.Location = New System.Drawing.Point(0, 297)
        Me.Splitter6.Name = "Splitter6"
        Me.Splitter6.Size = New System.Drawing.Size(1134, 5)
        Me.Splitter6.TabIndex = 153
        Me.Splitter6.TabStop = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.GBChoose)
        Me.Panel8.Controls.Add(Me.GroupBox1)
        Me.Panel8.Controls.Add(Me.LLRunFacilityReport)
        Me.Panel8.Controls.Add(Me.Label17)
        Me.Panel8.Controls.Add(Me.Label115)
        Me.Panel8.Controls.Add(Me.DTPStartDateFacility)
        Me.Panel8.Controls.Add(Me.DTPEndDateFacility)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1134, 302)
        Me.Panel8.TabIndex = 152
        '
        'GBChoose
        '
        Me.GBChoose.Controls.Add(Me.txtAIRSNumber2)
        Me.GBChoose.Controls.Add(Me.cboCity)
        Me.GBChoose.Controls.Add(Me.Label38)
        Me.GBChoose.Controls.Add(Me.cboCounty)
        Me.GBChoose.Controls.Add(Me.Label37)
        Me.GBChoose.Controls.Add(Me.LLFaciltiySearch)
        Me.GBChoose.Controls.Add(Me.txtFacility)
        Me.GBChoose.Controls.Add(Me.Label40)
        Me.GBChoose.Dock = System.Windows.Forms.DockStyle.Left
        Me.GBChoose.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBChoose.Location = New System.Drawing.Point(0, 0)
        Me.GBChoose.Name = "GBChoose"
        Me.GBChoose.Size = New System.Drawing.Size(256, 302)
        Me.GBChoose.TabIndex = 150
        Me.GBChoose.TabStop = False
        Me.GBChoose.Text = "Choose Search Method (Optional)"
        '
        'txtAIRSNumber2
        '
        Me.txtAIRSNumber2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAIRSNumber2.Location = New System.Drawing.Point(208, 40)
        Me.txtAIRSNumber2.Name = "txtAIRSNumber2"
        Me.txtAIRSNumber2.ReadOnly = True
        Me.txtAIRSNumber2.Size = New System.Drawing.Size(16, 20)
        Me.txtAIRSNumber2.TabIndex = 150
        Me.txtAIRSNumber2.Visible = False
        '
        'cboCity
        '
        Me.cboCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCity.Location = New System.Drawing.Point(24, 120)
        Me.cboCity.Name = "cboCity"
        Me.cboCity.Size = New System.Drawing.Size(184, 21)
        Me.cboCity.TabIndex = 128
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(8, 104)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(39, 13)
        Me.Label38.TabIndex = 125
        Me.Label38.Text = "By City"
        '
        'cboCounty
        '
        Me.cboCounty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCounty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCounty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCounty.Location = New System.Drawing.Point(24, 80)
        Me.cboCounty.Name = "cboCounty"
        Me.cboCounty.Size = New System.Drawing.Size(184, 21)
        Me.cboCounty.TabIndex = 123
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(8, 64)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(55, 13)
        Me.Label37.TabIndex = 122
        Me.Label37.Text = "By County"
        '
        'LLFaciltiySearch
        '
        Me.LLFaciltiySearch.AutoSize = True
        Me.LLFaciltiySearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LLFaciltiySearch.Location = New System.Drawing.Point(80, 24)
        Me.LLFaciltiySearch.Name = "LLFaciltiySearch"
        Me.LLFaciltiySearch.Size = New System.Drawing.Size(41, 13)
        Me.LLFaciltiySearch.TabIndex = 149
        Me.LLFaciltiySearch.TabStop = True
        Me.LLFaciltiySearch.Text = "Search"
        '
        'txtFacility
        '
        Me.txtFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacility.Location = New System.Drawing.Point(24, 40)
        Me.txtFacility.Name = "txtFacility"
        Me.txtFacility.ReadOnly = True
        Me.txtFacility.Size = New System.Drawing.Size(184, 20)
        Me.txtFacility.TabIndex = 148
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(8, 24)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(54, 13)
        Me.Label40.TabIndex = 131
        Me.Label40.Text = "By Facility"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbStatsAll)
        Me.GroupBox1.Controls.Add(Me.Label39)
        Me.GroupBox1.Controls.Add(Me.rdbFacilityDateCompleted)
        Me.GroupBox1.Controls.Add(Me.rdbFacilityDateTestStarted)
        Me.GroupBox1.Controls.Add(Me.rdbFacilityDateReceived)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(256, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 128)
        Me.GroupBox1.TabIndex = 142
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Bias"
        '
        'rdbStatsAll
        '
        Me.rdbStatsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbStatsAll.Location = New System.Drawing.Point(8, 88)
        Me.rdbStatsAll.Name = "rdbStatsAll"
        Me.rdbStatsAll.Size = New System.Drawing.Size(104, 16)
        Me.rdbStatsAll.TabIndex = 152
        Me.rdbStatsAll.Text = "All Test Reports"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(24, 104)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(185, 13)
        Me.Label39.TabIndex = 153
        Me.Label39.Text = "(All Dates - Excluding SM23 Archives)"
        '
        'rdbFacilityDateCompleted
        '
        Me.rdbFacilityDateCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbFacilityDateCompleted.Location = New System.Drawing.Point(8, 64)
        Me.rdbFacilityDateCompleted.Name = "rdbFacilityDateCompleted"
        Me.rdbFacilityDateCompleted.Size = New System.Drawing.Size(144, 24)
        Me.rdbFacilityDateCompleted.TabIndex = 2
        Me.rdbFacilityDateCompleted.Text = "Date Report Completed"
        '
        'rdbFacilityDateTestStarted
        '
        Me.rdbFacilityDateTestStarted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbFacilityDateTestStarted.Location = New System.Drawing.Point(8, 16)
        Me.rdbFacilityDateTestStarted.Name = "rdbFacilityDateTestStarted"
        Me.rdbFacilityDateTestStarted.Size = New System.Drawing.Size(136, 24)
        Me.rdbFacilityDateTestStarted.TabIndex = 1
        Me.rdbFacilityDateTestStarted.Text = "Date Test Started"
        '
        'rdbFacilityDateReceived
        '
        Me.rdbFacilityDateReceived.Checked = True
        Me.rdbFacilityDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbFacilityDateReceived.Location = New System.Drawing.Point(8, 40)
        Me.rdbFacilityDateReceived.Name = "rdbFacilityDateReceived"
        Me.rdbFacilityDateReceived.Size = New System.Drawing.Size(104, 24)
        Me.rdbFacilityDateReceived.TabIndex = 0
        Me.rdbFacilityDateReceived.TabStop = True
        Me.rdbFacilityDateReceived.Text = "Date Received"
        '
        'LLRunFacilityReport
        '
        Me.LLRunFacilityReport.Location = New System.Drawing.Point(504, 56)
        Me.LLRunFacilityReport.Name = "LLRunFacilityReport"
        Me.LLRunFacilityReport.Size = New System.Drawing.Size(128, 16)
        Me.LLRunFacilityReport.TabIndex = 147
        Me.LLRunFacilityReport.TabStop = True
        Me.LLRunFacilityReport.Text = "Click here to Run Report"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(592, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 16)
        Me.Label17.TabIndex = 146
        Me.Label17.Text = "End Date"
        '
        'Label115
        '
        Me.Label115.Location = New System.Drawing.Point(488, 8)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(64, 16)
        Me.Label115.TabIndex = 145
        Me.Label115.Text = "Start Date"
        '
        'DTPStartDateFacility
        '
        Me.DTPStartDateFacility.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartDateFacility.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartDateFacility.Location = New System.Drawing.Point(496, 24)
        Me.DTPStartDateFacility.Name = "DTPStartDateFacility"
        Me.DTPStartDateFacility.Size = New System.Drawing.Size(104, 20)
        Me.DTPStartDateFacility.TabIndex = 144
        Me.DTPStartDateFacility.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPEndDateFacility
        '
        Me.DTPEndDateFacility.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEndDateFacility.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEndDateFacility.Location = New System.Drawing.Point(600, 24)
        Me.DTPEndDateFacility.Name = "DTPEndDateFacility"
        Me.DTPEndDateFacility.Size = New System.Drawing.Size(104, 20)
        Me.DTPEndDateFacility.TabIndex = 143
        Me.DTPEndDateFacility.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.txtCSFileOpenOpenDays)
        Me.GroupBox9.Controls.Add(Me.txtCSInfoOnlyOpenDays)
        Me.GroupBox9.Controls.Add(Me.txtCSInComplianceOpenDays)
        Me.GroupBox9.Controls.Add(Me.txtIndeterminateOpenDays)
        Me.GroupBox9.Controls.Add(Me.txtCSNotInComplianceOpenDays)
        Me.GroupBox9.Controls.Add(Me.txtCSFileOpenClosed)
        Me.GroupBox9.Controls.Add(Me.txtCSInfoOnlyClosed)
        Me.GroupBox9.Controls.Add(Me.txtCSInComplianceClosed)
        Me.GroupBox9.Controls.Add(Me.txtIndeterminateClosed)
        Me.GroupBox9.Controls.Add(Me.txtCSNotInComplianceClosed)
        Me.GroupBox9.Controls.Add(Me.txtCSFileOpenOpen)
        Me.GroupBox9.Controls.Add(Me.txtCSInfoOnlyOpen)
        Me.GroupBox9.Controls.Add(Me.txtCSInComplianceOpen)
        Me.GroupBox9.Controls.Add(Me.txtIndeterminateOpen)
        Me.GroupBox9.Controls.Add(Me.txtCSNotInComplianceOpen)
        Me.GroupBox9.Controls.Add(Me.Label36)
        Me.GroupBox9.Controls.Add(Me.Label35)
        Me.GroupBox9.Controls.Add(Me.Label34)
        Me.GroupBox9.Controls.Add(Me.Label33)
        Me.GroupBox9.Controls.Add(Me.Label32)
        Me.GroupBox9.Controls.Add(Me.Label27)
        Me.GroupBox9.Controls.Add(Me.Label25)
        Me.GroupBox9.Controls.Add(Me.Label18)
        Me.GroupBox9.Controls.Add(Me.Label19)
        Me.GroupBox9.Controls.Add(Me.txtFacilityOpenDays)
        Me.GroupBox9.Controls.Add(Me.Label20)
        Me.GroupBox9.Controls.Add(Me.txtDaysOpenFacility)
        Me.GroupBox9.Controls.Add(Me.Label21)
        Me.GroupBox9.Controls.Add(Me.Label22)
        Me.GroupBox9.Controls.Add(Me.Label23)
        Me.GroupBox9.Controls.Add(Me.txtClosedFacility)
        Me.GroupBox9.Controls.Add(Me.txtOpenFacility)
        Me.GroupBox9.Controls.Add(Me.Label24)
        Me.GroupBox9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox9.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.Location = New System.Drawing.Point(0, 302)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(1134, 416)
        Me.GroupBox9.TabIndex = 151
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Results"
        '
        'txtCSFileOpenOpenDays
        '
        Me.txtCSFileOpenOpenDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSFileOpenOpenDays.Location = New System.Drawing.Point(616, 94)
        Me.txtCSFileOpenOpenDays.Name = "txtCSFileOpenOpenDays"
        Me.txtCSFileOpenOpenDays.ReadOnly = True
        Me.txtCSFileOpenOpenDays.Size = New System.Drawing.Size(96, 20)
        Me.txtCSFileOpenOpenDays.TabIndex = 154
        '
        'txtCSInfoOnlyOpenDays
        '
        Me.txtCSInfoOnlyOpenDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSInfoOnlyOpenDays.Location = New System.Drawing.Point(616, 114)
        Me.txtCSInfoOnlyOpenDays.Name = "txtCSInfoOnlyOpenDays"
        Me.txtCSInfoOnlyOpenDays.ReadOnly = True
        Me.txtCSInfoOnlyOpenDays.Size = New System.Drawing.Size(96, 20)
        Me.txtCSInfoOnlyOpenDays.TabIndex = 153
        '
        'txtCSInComplianceOpenDays
        '
        Me.txtCSInComplianceOpenDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSInComplianceOpenDays.Location = New System.Drawing.Point(616, 134)
        Me.txtCSInComplianceOpenDays.Name = "txtCSInComplianceOpenDays"
        Me.txtCSInComplianceOpenDays.ReadOnly = True
        Me.txtCSInComplianceOpenDays.Size = New System.Drawing.Size(96, 20)
        Me.txtCSInComplianceOpenDays.TabIndex = 152
        '
        'txtIndeterminateOpenDays
        '
        Me.txtIndeterminateOpenDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndeterminateOpenDays.Location = New System.Drawing.Point(616, 154)
        Me.txtIndeterminateOpenDays.Name = "txtIndeterminateOpenDays"
        Me.txtIndeterminateOpenDays.ReadOnly = True
        Me.txtIndeterminateOpenDays.Size = New System.Drawing.Size(96, 20)
        Me.txtIndeterminateOpenDays.TabIndex = 151
        '
        'txtCSNotInComplianceOpenDays
        '
        Me.txtCSNotInComplianceOpenDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSNotInComplianceOpenDays.Location = New System.Drawing.Point(616, 174)
        Me.txtCSNotInComplianceOpenDays.Name = "txtCSNotInComplianceOpenDays"
        Me.txtCSNotInComplianceOpenDays.ReadOnly = True
        Me.txtCSNotInComplianceOpenDays.Size = New System.Drawing.Size(96, 20)
        Me.txtCSNotInComplianceOpenDays.TabIndex = 150
        '
        'txtCSFileOpenClosed
        '
        Me.txtCSFileOpenClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSFileOpenClosed.Location = New System.Drawing.Point(416, 94)
        Me.txtCSFileOpenClosed.Name = "txtCSFileOpenClosed"
        Me.txtCSFileOpenClosed.ReadOnly = True
        Me.txtCSFileOpenClosed.Size = New System.Drawing.Size(96, 20)
        Me.txtCSFileOpenClosed.TabIndex = 149
        '
        'txtCSInfoOnlyClosed
        '
        Me.txtCSInfoOnlyClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSInfoOnlyClosed.Location = New System.Drawing.Point(416, 114)
        Me.txtCSInfoOnlyClosed.Name = "txtCSInfoOnlyClosed"
        Me.txtCSInfoOnlyClosed.ReadOnly = True
        Me.txtCSInfoOnlyClosed.Size = New System.Drawing.Size(96, 20)
        Me.txtCSInfoOnlyClosed.TabIndex = 148
        '
        'txtCSInComplianceClosed
        '
        Me.txtCSInComplianceClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSInComplianceClosed.Location = New System.Drawing.Point(416, 134)
        Me.txtCSInComplianceClosed.Name = "txtCSInComplianceClosed"
        Me.txtCSInComplianceClosed.ReadOnly = True
        Me.txtCSInComplianceClosed.Size = New System.Drawing.Size(96, 20)
        Me.txtCSInComplianceClosed.TabIndex = 147
        '
        'txtIndeterminateClosed
        '
        Me.txtIndeterminateClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndeterminateClosed.Location = New System.Drawing.Point(416, 154)
        Me.txtIndeterminateClosed.Name = "txtIndeterminateClosed"
        Me.txtIndeterminateClosed.ReadOnly = True
        Me.txtIndeterminateClosed.Size = New System.Drawing.Size(96, 20)
        Me.txtIndeterminateClosed.TabIndex = 146
        '
        'txtCSNotInComplianceClosed
        '
        Me.txtCSNotInComplianceClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSNotInComplianceClosed.Location = New System.Drawing.Point(416, 174)
        Me.txtCSNotInComplianceClosed.Name = "txtCSNotInComplianceClosed"
        Me.txtCSNotInComplianceClosed.ReadOnly = True
        Me.txtCSNotInComplianceClosed.Size = New System.Drawing.Size(96, 20)
        Me.txtCSNotInComplianceClosed.TabIndex = 145
        '
        'txtCSFileOpenOpen
        '
        Me.txtCSFileOpenOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSFileOpenOpen.Location = New System.Drawing.Point(248, 94)
        Me.txtCSFileOpenOpen.Name = "txtCSFileOpenOpen"
        Me.txtCSFileOpenOpen.ReadOnly = True
        Me.txtCSFileOpenOpen.Size = New System.Drawing.Size(96, 20)
        Me.txtCSFileOpenOpen.TabIndex = 144
        '
        'txtCSInfoOnlyOpen
        '
        Me.txtCSInfoOnlyOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSInfoOnlyOpen.Location = New System.Drawing.Point(248, 114)
        Me.txtCSInfoOnlyOpen.Name = "txtCSInfoOnlyOpen"
        Me.txtCSInfoOnlyOpen.ReadOnly = True
        Me.txtCSInfoOnlyOpen.Size = New System.Drawing.Size(96, 20)
        Me.txtCSInfoOnlyOpen.TabIndex = 143
        '
        'txtCSInComplianceOpen
        '
        Me.txtCSInComplianceOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSInComplianceOpen.Location = New System.Drawing.Point(248, 134)
        Me.txtCSInComplianceOpen.Name = "txtCSInComplianceOpen"
        Me.txtCSInComplianceOpen.ReadOnly = True
        Me.txtCSInComplianceOpen.Size = New System.Drawing.Size(96, 20)
        Me.txtCSInComplianceOpen.TabIndex = 142
        '
        'txtIndeterminateOpen
        '
        Me.txtIndeterminateOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndeterminateOpen.Location = New System.Drawing.Point(248, 154)
        Me.txtIndeterminateOpen.Name = "txtIndeterminateOpen"
        Me.txtIndeterminateOpen.ReadOnly = True
        Me.txtIndeterminateOpen.Size = New System.Drawing.Size(96, 20)
        Me.txtIndeterminateOpen.TabIndex = 141
        '
        'txtCSNotInComplianceOpen
        '
        Me.txtCSNotInComplianceOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSNotInComplianceOpen.Location = New System.Drawing.Point(248, 174)
        Me.txtCSNotInComplianceOpen.Name = "txtCSNotInComplianceOpen"
        Me.txtCSNotInComplianceOpen.ReadOnly = True
        Me.txtCSNotInComplianceOpen.Size = New System.Drawing.Size(96, 20)
        Me.txtCSNotInComplianceOpen.TabIndex = 140
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(32, 176)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(94, 13)
        Me.Label36.TabIndex = 139
        Me.Label36.Text = "Not In Compliance"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(32, 116)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(148, 13)
        Me.Label35.TabIndex = 138
        Me.Label35.Text = "For Information Purposes Only"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(32, 136)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(74, 13)
        Me.Label34.TabIndex = 137
        Me.Label34.Text = "In Compliance"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(32, 156)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(71, 13)
        Me.Label33.TabIndex = 136
        Me.Label33.Text = "Indeterminate"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(32, 96)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(52, 13)
        Me.Label32.TabIndex = 135
        Me.Label32.Text = "File Open"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(384, 24)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(63, 13)
        Me.Label27.TabIndex = 134
        Me.Label27.Text = "Closed Files"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(8, 80)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(95, 13)
        Me.Label25.TabIndex = 133
        Me.Label25.Text = "Compliance Status"
        '
        'Label18
        '
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(544, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 177)
        Me.Label18.TabIndex = 132
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(8, 200)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(700, 1)
        Me.Label19.TabIndex = 131
        '
        'txtFacilityOpenDays
        '
        Me.txtFacilityOpenDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityOpenDays.Location = New System.Drawing.Point(616, 48)
        Me.txtFacilityOpenDays.Name = "txtFacilityOpenDays"
        Me.txtFacilityOpenDays.ReadOnly = True
        Me.txtFacilityOpenDays.Size = New System.Drawing.Size(96, 20)
        Me.txtFacilityOpenDays.TabIndex = 130
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(672, 24)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(32, 13)
        Me.Label20.TabIndex = 129
        Me.Label20.Text = "-days"
        '
        'txtDaysOpenFacility
        '
        Me.txtDaysOpenFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDaysOpenFacility.Location = New System.Drawing.Point(640, 22)
        Me.txtDaysOpenFacility.Name = "txtDaysOpenFacility"
        Me.txtDaysOpenFacility.Size = New System.Drawing.Size(32, 20)
        Me.txtDaysOpenFacility.TabIndex = 128
        Me.txtDaysOpenFacility.Text = "50"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(568, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(57, 13)
        Me.Label21.TabIndex = 127
        Me.Label21.Text = "Files Open"
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(376, 24)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 177)
        Me.Label22.TabIndex = 126
        '
        'Label23
        '
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(8, 72)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(700, 1)
        Me.Label23.TabIndex = 125
        '
        'txtClosedFacility
        '
        Me.txtClosedFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClosedFacility.Location = New System.Drawing.Point(416, 48)
        Me.txtClosedFacility.Name = "txtClosedFacility"
        Me.txtClosedFacility.ReadOnly = True
        Me.txtClosedFacility.Size = New System.Drawing.Size(100, 20)
        Me.txtClosedFacility.TabIndex = 124
        '
        'txtOpenFacility
        '
        Me.txtOpenFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpenFacility.Location = New System.Drawing.Point(248, 48)
        Me.txtOpenFacility.Name = "txtOpenFacility"
        Me.txtOpenFacility.ReadOnly = True
        Me.txtOpenFacility.Size = New System.Drawing.Size(96, 20)
        Me.txtOpenFacility.TabIndex = 123
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(184, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(57, 13)
        Me.Label24.TabIndex = 122
        Me.Label24.Text = "Open Files"
        '
        'TPAIRSReportsPrinted
        '
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbOneStack3Runs)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbPEMS)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.Label102)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.Label101)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbFlare)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbLoadingRack)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbPTE)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbMemorandumToFile)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbRATA)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbGasTests)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbTreatmentPonds)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbMethod22)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbMethod9Single)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbOneStack4Runs)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbTwoStack)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbTwoStackDRE)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbMethod9Multi)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbMemorandum)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.Label100)
        Me.TPAIRSReportsPrinted.Controls.Add(Me.chbOneStack2Runs)
        Me.TPAIRSReportsPrinted.Location = New System.Drawing.Point(4, 40)
        Me.TPAIRSReportsPrinted.Name = "TPAIRSReportsPrinted"
        Me.TPAIRSReportsPrinted.Size = New System.Drawing.Size(1134, 718)
        Me.TPAIRSReportsPrinted.TabIndex = 5
        Me.TPAIRSReportsPrinted.Text = "AIRS Reports Printed"
        Me.TPAIRSReportsPrinted.UseVisualStyleBackColor = True
        '
        'chbOneStack3Runs
        '
        Me.chbOneStack3Runs.Location = New System.Drawing.Point(16, 120)
        Me.chbOneStack3Runs.Name = "chbOneStack3Runs"
        Me.chbOneStack3Runs.Size = New System.Drawing.Size(232, 24)
        Me.chbOneStack3Runs.TabIndex = 40
        Me.chbOneStack3Runs.Text = "One Stack (3-Runs)"
        '
        'chbPEMS
        '
        Me.chbPEMS.Location = New System.Drawing.Point(248, 264)
        Me.chbPEMS.Name = "chbPEMS"
        Me.chbPEMS.Size = New System.Drawing.Size(232, 24)
        Me.chbPEMS.TabIndex = 39
        Me.chbPEMS.Text = "PEMS"
        '
        'Label102
        '
        Me.Label102.Location = New System.Drawing.Point(16, 48)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(376, 23)
        Me.Label102.TabIndex = 38
        Me.Label102.Text = "Boxes with check marks will not be printed out. "
        '
        'Label101
        '
        Me.Label101.Location = New System.Drawing.Point(16, 72)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(336, 23)
        Me.Label101.TabIndex = 37
        Me.Label101.Text = "Forms without a check mark will generate an AIRS form."
        '
        'chbFlare
        '
        Me.chbFlare.Location = New System.Drawing.Point(248, 240)
        Me.chbFlare.Name = "chbFlare"
        Me.chbFlare.Size = New System.Drawing.Size(232, 24)
        Me.chbFlare.TabIndex = 36
        Me.chbFlare.Text = "Flare"
        '
        'chbLoadingRack
        '
        Me.chbLoadingRack.Location = New System.Drawing.Point(248, 168)
        Me.chbLoadingRack.Name = "chbLoadingRack"
        Me.chbLoadingRack.Size = New System.Drawing.Size(232, 24)
        Me.chbLoadingRack.TabIndex = 35
        Me.chbLoadingRack.Text = "Loading Rack (DRE)"
        '
        'chbPTE
        '
        Me.chbPTE.Location = New System.Drawing.Point(248, 192)
        Me.chbPTE.Name = "chbPTE"
        Me.chbPTE.Size = New System.Drawing.Size(232, 24)
        Me.chbPTE.TabIndex = 34
        Me.chbPTE.Text = "PTE (Perminate Total Enclosure)"
        '
        'chbMemorandumToFile
        '
        Me.chbMemorandumToFile.Location = New System.Drawing.Point(16, 288)
        Me.chbMemorandumToFile.Name = "chbMemorandumToFile"
        Me.chbMemorandumToFile.Size = New System.Drawing.Size(232, 24)
        Me.chbMemorandumToFile.TabIndex = 33
        Me.chbMemorandumToFile.Text = "Memorandum To File"
        '
        'chbRATA
        '
        Me.chbRATA.Location = New System.Drawing.Point(248, 216)
        Me.chbRATA.Name = "chbRATA"
        Me.chbRATA.Size = New System.Drawing.Size(232, 24)
        Me.chbRATA.TabIndex = 32
        Me.chbRATA.Text = "RATA"
        '
        'chbGasTests
        '
        Me.chbGasTests.Location = New System.Drawing.Point(248, 96)
        Me.chbGasTests.Name = "chbGasTests"
        Me.chbGasTests.Size = New System.Drawing.Size(232, 24)
        Me.chbGasTests.TabIndex = 31
        Me.chbGasTests.Text = "Gas Tests"
        '
        'chbTreatmentPonds
        '
        Me.chbTreatmentPonds.Location = New System.Drawing.Point(248, 120)
        Me.chbTreatmentPonds.Name = "chbTreatmentPonds"
        Me.chbTreatmentPonds.Size = New System.Drawing.Size(232, 24)
        Me.chbTreatmentPonds.TabIndex = 30
        Me.chbTreatmentPonds.Text = "Treatment Ponds"
        '
        'chbMethod22
        '
        Me.chbMethod22.Location = New System.Drawing.Point(248, 144)
        Me.chbMethod22.Name = "chbMethod22"
        Me.chbMethod22.Size = New System.Drawing.Size(232, 24)
        Me.chbMethod22.TabIndex = 29
        Me.chbMethod22.Text = "Method 22"
        '
        'chbMethod9Single
        '
        Me.chbMethod9Single.Location = New System.Drawing.Point(16, 216)
        Me.chbMethod9Single.Name = "chbMethod9Single"
        Me.chbMethod9Single.Size = New System.Drawing.Size(232, 24)
        Me.chbMethod9Single.TabIndex = 28
        Me.chbMethod9Single.Text = "Method 9 (Single)"
        '
        'chbOneStack4Runs
        '
        Me.chbOneStack4Runs.Location = New System.Drawing.Point(16, 144)
        Me.chbOneStack4Runs.Name = "chbOneStack4Runs"
        Me.chbOneStack4Runs.Size = New System.Drawing.Size(232, 24)
        Me.chbOneStack4Runs.TabIndex = 27
        Me.chbOneStack4Runs.Text = "One Stack (4-Runs)"
        '
        'chbTwoStack
        '
        Me.chbTwoStack.Location = New System.Drawing.Point(16, 168)
        Me.chbTwoStack.Name = "chbTwoStack"
        Me.chbTwoStack.Size = New System.Drawing.Size(232, 24)
        Me.chbTwoStack.TabIndex = 26
        Me.chbTwoStack.Text = "Two Stack (Standard)"
        '
        'chbTwoStackDRE
        '
        Me.chbTwoStackDRE.Location = New System.Drawing.Point(16, 192)
        Me.chbTwoStackDRE.Name = "chbTwoStackDRE"
        Me.chbTwoStackDRE.Size = New System.Drawing.Size(232, 24)
        Me.chbTwoStackDRE.TabIndex = 25
        Me.chbTwoStackDRE.Text = "Two Stack DRE"
        '
        'chbMethod9Multi
        '
        Me.chbMethod9Multi.Location = New System.Drawing.Point(16, 240)
        Me.chbMethod9Multi.Name = "chbMethod9Multi"
        Me.chbMethod9Multi.Size = New System.Drawing.Size(232, 24)
        Me.chbMethod9Multi.TabIndex = 24
        Me.chbMethod9Multi.Text = "Method 9 (Multi)"
        '
        'chbMemorandum
        '
        Me.chbMemorandum.Location = New System.Drawing.Point(16, 264)
        Me.chbMemorandum.Name = "chbMemorandum"
        Me.chbMemorandum.Size = New System.Drawing.Size(232, 24)
        Me.chbMemorandum.TabIndex = 23
        Me.chbMemorandum.Text = "Memorandum (Standard)"
        '
        'Label100
        '
        Me.Label100.Location = New System.Drawing.Point(16, 24)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(400, 23)
        Me.Label100.TabIndex = 21
        Me.Label100.Text = "Select which Report Types will not have an AIRS Form Printed Out "
        '
        'chbOneStack2Runs
        '
        Me.chbOneStack2Runs.Location = New System.Drawing.Point(16, 96)
        Me.chbOneStack2Runs.Name = "chbOneStack2Runs"
        Me.chbOneStack2Runs.Size = New System.Drawing.Size(232, 24)
        Me.chbOneStack2Runs.TabIndex = 20
        Me.chbOneStack2Runs.Text = "One Stack (2-Runs)"
        '
        'TPExcelFiles
        '
        Me.TPExcelFiles.Controls.Add(Me.Splitter13)
        Me.TPExcelFiles.Controls.Add(Me.Panel14)
        Me.TPExcelFiles.Controls.Add(Me.dgrExcelFiles)
        Me.TPExcelFiles.Location = New System.Drawing.Point(4, 40)
        Me.TPExcelFiles.Name = "TPExcelFiles"
        Me.TPExcelFiles.Size = New System.Drawing.Size(1134, 718)
        Me.TPExcelFiles.TabIndex = 10
        Me.TPExcelFiles.Text = "Add Excel Files"
        Me.TPExcelFiles.UseVisualStyleBackColor = True
        '
        'Splitter13
        '
        Me.Splitter13.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter13.Location = New System.Drawing.Point(761, 0)
        Me.Splitter13.Name = "Splitter13"
        Me.Splitter13.Size = New System.Drawing.Size(5, 718)
        Me.Splitter13.TabIndex = 8
        Me.Splitter13.TabStop = False
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.llbDownloadExcelFiles)
        Me.Panel14.Controls.Add(Me.Label116)
        Me.Panel14.Controls.Add(Me.Label95)
        Me.Panel14.Controls.Add(Me.txtNewFileName)
        Me.Panel14.Controls.Add(Me.txtFileName)
        Me.Panel14.Controls.Add(Me.llbRemoveFile)
        Me.Panel14.Controls.Add(Me.llbAddExcelFile)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel14.Location = New System.Drawing.Point(766, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(368, 718)
        Me.Panel14.TabIndex = 2
        '
        'llbDownloadExcelFiles
        '
        Me.llbDownloadExcelFiles.AutoSize = True
        Me.llbDownloadExcelFiles.Location = New System.Drawing.Point(8, 160)
        Me.llbDownloadExcelFiles.Name = "llbDownloadExcelFiles"
        Me.llbDownloadExcelFiles.Size = New System.Drawing.Size(175, 13)
        Me.llbDownloadExcelFiles.TabIndex = 6
        Me.llbDownloadExcelFiles.TabStop = True
        Me.llbDownloadExcelFiles.Text = "Download Excel File from Database"
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Location = New System.Drawing.Point(16, 72)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(52, 13)
        Me.Label116.TabIndex = 5
        Me.Label116.Text = "(Optional)"
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(8, 56)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(54, 13)
        Me.Label95.TabIndex = 4
        Me.Label95.Text = "File Name"
        '
        'txtNewFileName
        '
        Me.txtNewFileName.Location = New System.Drawing.Point(80, 56)
        Me.txtNewFileName.Name = "txtNewFileName"
        Me.txtNewFileName.Size = New System.Drawing.Size(216, 20)
        Me.txtNewFileName.TabIndex = 3
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(32, 184)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(256, 20)
        Me.txtFileName.TabIndex = 2
        '
        'llbRemoveFile
        '
        Me.llbRemoveFile.AutoSize = True
        Me.llbRemoveFile.Location = New System.Drawing.Point(8, 232)
        Me.llbRemoveFile.Name = "llbRemoveFile"
        Me.llbRemoveFile.Size = New System.Drawing.Size(158, 13)
        Me.llbRemoveFile.TabIndex = 1
        Me.llbRemoveFile.TabStop = True
        Me.llbRemoveFile.Text = "Delete Excel File from Database"
        '
        'llbAddExcelFile
        '
        Me.llbAddExcelFile.AutoSize = True
        Me.llbAddExcelFile.Location = New System.Drawing.Point(16, 24)
        Me.llbAddExcelFile.Name = "llbAddExcelFile"
        Me.llbAddExcelFile.Size = New System.Drawing.Size(150, 13)
        Me.llbAddExcelFile.TabIndex = 0
        Me.llbAddExcelFile.TabStop = True
        Me.llbAddExcelFile.Text = "Add an Excel File to Database"
        '
        'dgrExcelFiles
        '
        Me.dgrExcelFiles.DataMember = ""
        Me.dgrExcelFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrExcelFiles.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrExcelFiles.Location = New System.Drawing.Point(0, 0)
        Me.dgrExcelFiles.Name = "dgrExcelFiles"
        Me.dgrExcelFiles.ReadOnly = True
        Me.dgrExcelFiles.Size = New System.Drawing.Size(1134, 718)
        Me.dgrExcelFiles.TabIndex = 7
        '
        'TPEngineerTestReport
        '
        Me.TPEngineerTestReport.Controls.Add(Me.Splitter9)
        Me.TPEngineerTestReport.Controls.Add(Me.GroupBox14)
        Me.TPEngineerTestReport.Controls.Add(Me.Splitter8)
        Me.TPEngineerTestReport.Controls.Add(Me.PanelDate)
        Me.TPEngineerTestReport.Controls.Add(Me.dgrEngineersFacilityList)
        Me.TPEngineerTestReport.Location = New System.Drawing.Point(4, 40)
        Me.TPEngineerTestReport.Name = "TPEngineerTestReport"
        Me.TPEngineerTestReport.Size = New System.Drawing.Size(1134, 718)
        Me.TPEngineerTestReport.TabIndex = 8
        Me.TPEngineerTestReport.Text = "Engineer Test Reports"
        Me.TPEngineerTestReport.UseVisualStyleBackColor = True
        '
        'Splitter9
        '
        Me.Splitter9.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter9.Location = New System.Drawing.Point(0, 449)
        Me.Splitter9.Name = "Splitter9"
        Me.Splitter9.Size = New System.Drawing.Size(1134, 5)
        Me.Splitter9.TabIndex = 148
        Me.Splitter9.TabStop = False
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.lsbEngineers)
        Me.GroupBox14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox14.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox14.Location = New System.Drawing.Point(0, 141)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(1134, 313)
        Me.GroupBox14.TabIndex = 147
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "Engineer \ Reference Number \ Facility \ Date Received \ (Days)"
        '
        'lsbEngineers
        '
        Me.lsbEngineers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsbEngineers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lsbEngineers.HorizontalScrollbar = True
        Me.lsbEngineers.Location = New System.Drawing.Point(3, 22)
        Me.lsbEngineers.Name = "lsbEngineers"
        Me.lsbEngineers.ScrollAlwaysVisible = True
        Me.lsbEngineers.Size = New System.Drawing.Size(1128, 288)
        Me.lsbEngineers.TabIndex = 99
        '
        'Splitter8
        '
        Me.Splitter8.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter8.Location = New System.Drawing.Point(0, 136)
        Me.Splitter8.Name = "Splitter8"
        Me.Splitter8.Size = New System.Drawing.Size(1134, 5)
        Me.Splitter8.TabIndex = 146
        Me.Splitter8.TabStop = False
        '
        'PanelDate
        '
        Me.PanelDate.Controls.Add(Me.Splitter11)
        Me.PanelDate.Controls.Add(Me.Splitter10)
        Me.PanelDate.Controls.Add(Me.Panel10)
        Me.PanelDate.Controls.Add(Me.GroupBox13)
        Me.PanelDate.Controls.Add(Me.GroupBox12)
        Me.PanelDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelDate.Location = New System.Drawing.Point(0, 0)
        Me.PanelDate.Name = "PanelDate"
        Me.PanelDate.Size = New System.Drawing.Size(1134, 136)
        Me.PanelDate.TabIndex = 145
        '
        'Splitter11
        '
        Me.Splitter11.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter11.Location = New System.Drawing.Point(905, 0)
        Me.Splitter11.Name = "Splitter11"
        Me.Splitter11.Size = New System.Drawing.Size(5, 136)
        Me.Splitter11.TabIndex = 154
        Me.Splitter11.TabStop = False
        '
        'Splitter10
        '
        Me.Splitter10.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter10.Location = New System.Drawing.Point(232, 0)
        Me.Splitter10.Name = "Splitter10"
        Me.Splitter10.Size = New System.Drawing.Size(5, 136)
        Me.Splitter10.TabIndex = 153
        Me.Splitter10.TabStop = False
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.DTPEngineerTestReportEnd)
        Me.Panel10.Controls.Add(Me.Label121)
        Me.Panel10.Controls.Add(Me.DTPEngineerTestReportStart)
        Me.Panel10.Controls.Add(Me.Label120)
        Me.Panel10.Controls.Add(Me.txtReferenceNumber)
        Me.Panel10.Controls.Add(Me.llbExportToExcel)
        Me.Panel10.Controls.Add(Me.llbViewReport)
        Me.Panel10.Controls.Add(Me.llbEngineerTestReports)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(232, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(678, 136)
        Me.Panel10.TabIndex = 152
        '
        'DTPEngineerTestReportEnd
        '
        Me.DTPEngineerTestReportEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEngineerTestReportEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEngineerTestReportEnd.Location = New System.Drawing.Point(152, 24)
        Me.DTPEngineerTestReportEnd.Name = "DTPEngineerTestReportEnd"
        Me.DTPEngineerTestReportEnd.Size = New System.Drawing.Size(112, 20)
        Me.DTPEngineerTestReportEnd.TabIndex = 92
        Me.DTPEngineerTestReportEnd.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'Label121
        '
        Me.Label121.AutoSize = True
        Me.Label121.Location = New System.Drawing.Point(8, 8)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(55, 13)
        Me.Label121.TabIndex = 145
        Me.Label121.Text = "Start Date"
        '
        'DTPEngineerTestReportStart
        '
        Me.DTPEngineerTestReportStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEngineerTestReportStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEngineerTestReportStart.Location = New System.Drawing.Point(16, 24)
        Me.DTPEngineerTestReportStart.Name = "DTPEngineerTestReportStart"
        Me.DTPEngineerTestReportStart.Size = New System.Drawing.Size(112, 20)
        Me.DTPEngineerTestReportStart.TabIndex = 91
        Me.DTPEngineerTestReportStart.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.Location = New System.Drawing.Point(136, 8)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(55, 13)
        Me.Label120.TabIndex = 146
        Me.Label120.Text = "End Date "
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(40, 112)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtReferenceNumber.TabIndex = 150
        '
        'llbExportToExcel
        '
        Me.llbExportToExcel.AutoSize = True
        Me.llbExportToExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbExportToExcel.Location = New System.Drawing.Point(176, 88)
        Me.llbExportToExcel.Name = "llbExportToExcel"
        Me.llbExportToExcel.Size = New System.Drawing.Size(78, 13)
        Me.llbExportToExcel.TabIndex = 151
        Me.llbExportToExcel.TabStop = True
        Me.llbExportToExcel.Text = "Export to Excel"
        '
        'llbViewReport
        '
        Me.llbViewReport.AutoSize = True
        Me.llbViewReport.Location = New System.Drawing.Point(16, 88)
        Me.llbViewReport.Name = "llbViewReport"
        Me.llbViewReport.Size = New System.Drawing.Size(110, 13)
        Me.llbViewReport.TabIndex = 149
        Me.llbViewReport.TabStop = True
        Me.llbViewReport.Text = "View Selected Report"
        '
        'llbEngineerTestReports
        '
        Me.llbEngineerTestReports.AutoSize = True
        Me.llbEngineerTestReports.Location = New System.Drawing.Point(16, 48)
        Me.llbEngineerTestReports.Name = "llbEngineerTestReports"
        Me.llbEngineerTestReports.Size = New System.Drawing.Size(62, 13)
        Me.llbEngineerTestReports.TabIndex = 148
        Me.llbEngineerTestReports.TabStop = True
        Me.llbEngineerTestReports.Text = "Run Report"
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.clbEngineersList2)
        Me.GroupBox13.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox13.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox13.Location = New System.Drawing.Point(910, 0)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(224, 136)
        Me.GroupBox13.TabIndex = 147
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Engineers Within Unit"
        '
        'clbEngineersList2
        '
        Me.clbEngineersList2.CheckOnClick = True
        Me.clbEngineersList2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbEngineersList2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clbEngineersList2.Location = New System.Drawing.Point(3, 22)
        Me.clbEngineersList2.Name = "clbEngineersList2"
        Me.clbEngineersList2.Size = New System.Drawing.Size(218, 111)
        Me.clbEngineersList2.TabIndex = 90
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.rdbEngineerTestReportAll)
        Me.GroupBox12.Controls.Add(Me.Label119)
        Me.GroupBox12.Controls.Add(Me.rdbEngineerTestReportCompleted)
        Me.GroupBox12.Controls.Add(Me.rdbEngineerTestReportTestDate)
        Me.GroupBox12.Controls.Add(Me.rdbEngineerTestReportReceived)
        Me.GroupBox12.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox12.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox12.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(232, 136)
        Me.GroupBox12.TabIndex = 144
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Date Bias"
        '
        'rdbEngineerTestReportAll
        '
        Me.rdbEngineerTestReportAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbEngineerTestReportAll.Location = New System.Drawing.Point(8, 88)
        Me.rdbEngineerTestReportAll.Name = "rdbEngineerTestReportAll"
        Me.rdbEngineerTestReportAll.Size = New System.Drawing.Size(104, 16)
        Me.rdbEngineerTestReportAll.TabIndex = 152
        Me.rdbEngineerTestReportAll.Text = "All Test Reports"
        '
        'Label119
        '
        Me.Label119.AutoSize = True
        Me.Label119.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.Location = New System.Drawing.Point(24, 104)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(185, 13)
        Me.Label119.TabIndex = 153
        Me.Label119.Text = "(All Dates - Excluding SM23 Archives)"
        '
        'rdbEngineerTestReportCompleted
        '
        Me.rdbEngineerTestReportCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbEngineerTestReportCompleted.Location = New System.Drawing.Point(8, 64)
        Me.rdbEngineerTestReportCompleted.Name = "rdbEngineerTestReportCompleted"
        Me.rdbEngineerTestReportCompleted.Size = New System.Drawing.Size(144, 24)
        Me.rdbEngineerTestReportCompleted.TabIndex = 2
        Me.rdbEngineerTestReportCompleted.Text = "Date Report Completed"
        '
        'rdbEngineerTestReportTestDate
        '
        Me.rdbEngineerTestReportTestDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbEngineerTestReportTestDate.Location = New System.Drawing.Point(8, 16)
        Me.rdbEngineerTestReportTestDate.Name = "rdbEngineerTestReportTestDate"
        Me.rdbEngineerTestReportTestDate.Size = New System.Drawing.Size(136, 24)
        Me.rdbEngineerTestReportTestDate.TabIndex = 1
        Me.rdbEngineerTestReportTestDate.Text = "Date Test Started"
        '
        'rdbEngineerTestReportReceived
        '
        Me.rdbEngineerTestReportReceived.Checked = True
        Me.rdbEngineerTestReportReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbEngineerTestReportReceived.Location = New System.Drawing.Point(8, 40)
        Me.rdbEngineerTestReportReceived.Name = "rdbEngineerTestReportReceived"
        Me.rdbEngineerTestReportReceived.Size = New System.Drawing.Size(104, 24)
        Me.rdbEngineerTestReportReceived.TabIndex = 0
        Me.rdbEngineerTestReportReceived.TabStop = True
        Me.rdbEngineerTestReportReceived.Text = "Date Received"
        '
        'dgrEngineersFacilityList
        '
        Me.dgrEngineersFacilityList.DataMember = ""
        Me.dgrEngineersFacilityList.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgrEngineersFacilityList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrEngineersFacilityList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrEngineersFacilityList.Location = New System.Drawing.Point(0, 454)
        Me.dgrEngineersFacilityList.Name = "dgrEngineersFacilityList"
        Me.dgrEngineersFacilityList.ReadOnly = True
        Me.dgrEngineersFacilityList.Size = New System.Drawing.Size(1134, 264)
        Me.dgrEngineersFacilityList.TabIndex = 96
        '
        'TPApplicationsReviewed
        '
        Me.TPApplicationsReviewed.Controls.Add(Me.btnApplicationReport)
        Me.TPApplicationsReviewed.Controls.Add(Me.Label118)
        Me.TPApplicationsReviewed.Controls.Add(Me.DTPAppEndDate)
        Me.TPApplicationsReviewed.Controls.Add(Me.Label117)
        Me.TPApplicationsReviewed.Controls.Add(Me.DTPAppStartDate)
        Me.TPApplicationsReviewed.Controls.Add(Me.txtISMPApplicationReport)
        Me.TPApplicationsReviewed.Controls.Add(Me.btnRunApplicationReport)
        Me.TPApplicationsReviewed.Location = New System.Drawing.Point(4, 40)
        Me.TPApplicationsReviewed.Name = "TPApplicationsReviewed"
        Me.TPApplicationsReviewed.Size = New System.Drawing.Size(1134, 718)
        Me.TPApplicationsReviewed.TabIndex = 11
        Me.TPApplicationsReviewed.Text = "ISMP Applications Reviewed"
        Me.TPApplicationsReviewed.UseVisualStyleBackColor = True
        '
        'btnApplicationReport
        '
        Me.btnApplicationReport.Location = New System.Drawing.Point(633, 14)
        Me.btnApplicationReport.Name = "btnApplicationReport"
        Me.btnApplicationReport.Size = New System.Drawing.Size(74, 21)
        Me.btnApplicationReport.TabIndex = 149
        Me.btnApplicationReport.Text = "Print"
        '
        'Label118
        '
        Me.Label118.AutoSize = True
        Me.Label118.Location = New System.Drawing.Point(400, 14)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(55, 13)
        Me.Label118.TabIndex = 148
        Me.Label118.Text = "Start Date"
        '
        'DTPAppEndDate
        '
        Me.DTPAppEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAppEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAppEndDate.Location = New System.Drawing.Point(460, 14)
        Me.DTPAppEndDate.Name = "DTPAppEndDate"
        Me.DTPAppEndDate.Size = New System.Drawing.Size(104, 20)
        Me.DTPAppEndDate.TabIndex = 147
        Me.DTPAppEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.Location = New System.Drawing.Point(213, 14)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(55, 13)
        Me.Label117.TabIndex = 146
        Me.Label117.Text = "Start Date"
        '
        'DTPAppStartDate
        '
        Me.DTPAppStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAppStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAppStartDate.Location = New System.Drawing.Point(273, 14)
        Me.DTPAppStartDate.Name = "DTPAppStartDate"
        Me.DTPAppStartDate.Size = New System.Drawing.Size(104, 20)
        Me.DTPAppStartDate.TabIndex = 145
        Me.DTPAppStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'txtISMPApplicationReport
        '
        Me.txtISMPApplicationReport.AcceptsReturn = True
        Me.txtISMPApplicationReport.Location = New System.Drawing.Point(20, 49)
        Me.txtISMPApplicationReport.Multiline = True
        Me.txtISMPApplicationReport.Name = "txtISMPApplicationReport"
        Me.txtISMPApplicationReport.ReadOnly = True
        Me.txtISMPApplicationReport.Size = New System.Drawing.Size(827, 270)
        Me.txtISMPApplicationReport.TabIndex = 1
        '
        'btnRunApplicationReport
        '
        Me.btnRunApplicationReport.Location = New System.Drawing.Point(20, 14)
        Me.btnRunApplicationReport.Name = "btnRunApplicationReport"
        Me.btnRunApplicationReport.Size = New System.Drawing.Size(153, 20)
        Me.btnRunApplicationReport.TabIndex = 0
        Me.btnRunApplicationReport.Text = "Run Application Report"
        '
        'TPMonthlyReport
        '
        Me.TPMonthlyReport.Controls.Add(Me.Splitter2)
        Me.TPMonthlyReport.Controls.Add(Me.GroupBox6)
        Me.TPMonthlyReport.Controls.Add(Me.GroupBox4)
        Me.TPMonthlyReport.Controls.Add(Me.btnRunReport)
        Me.TPMonthlyReport.Location = New System.Drawing.Point(4, 40)
        Me.TPMonthlyReport.Name = "TPMonthlyReport"
        Me.TPMonthlyReport.Size = New System.Drawing.Size(1134, 718)
        Me.TPMonthlyReport.TabIndex = 2
        Me.TPMonthlyReport.Text = "Monthly Report"
        Me.TPMonthlyReport.UseVisualStyleBackColor = True
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(0, 369)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(1134, 5)
        Me.Splitter2.TabIndex = 129
        Me.Splitter2.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Splitter1)
        Me.GroupBox6.Controls.Add(Me.Panel4)
        Me.GroupBox6.Controls.Add(Me.GroupBox5)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(1134, 374)
        Me.GroupBox6.TabIndex = 127
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Monthly Report "
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(3, 254)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1128, 5)
        Me.Splitter1.TabIndex = 128
        Me.Splitter1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.llbPrintSummaryReport)
        Me.Panel4.Controls.Add(Me.Label73)
        Me.Panel4.Controls.Add(Me.txtTestWitnessed)
        Me.Panel4.Controls.Add(Me.Label70)
        Me.Panel4.Controls.Add(Me.txtTestOutOfCompliance)
        Me.Panel4.Controls.Add(Me.Label69)
        Me.Panel4.Controls.Add(Me.txtTestCompleted)
        Me.Panel4.Controls.Add(Me.Label68)
        Me.Panel4.Controls.Add(Me.txtReceived)
        Me.Panel4.Controls.Add(Me.Label65)
        Me.Panel4.Controls.Add(Me.Label64)
        Me.Panel4.Controls.Add(Me.DTPMonthlyEnd)
        Me.Panel4.Controls.Add(Me.DTPMonthlyStart)
        Me.Panel4.Controls.Add(Me.txtPercential)
        Me.Panel4.Controls.Add(Me.Label72)
        Me.Panel4.Controls.Add(Me.txtMedianTimeToComplete)
        Me.Panel4.Controls.Add(Me.Label71)
        Me.Panel4.Controls.Add(Me.txt80Percent)
        Me.Panel4.Controls.Add(Me.Label31)
        Me.Panel4.Controls.Add(Me.llbPrintMonthlyReport)
        Me.Panel4.Controls.Add(Me.llbRunMonthlyReport)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 22)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1128, 237)
        Me.Panel4.TabIndex = 127
        '
        'llbPrintSummaryReport
        '
        Me.llbPrintSummaryReport.AutoSize = True
        Me.llbPrintSummaryReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbPrintSummaryReport.Location = New System.Drawing.Point(8, 104)
        Me.llbPrintSummaryReport.Name = "llbPrintSummaryReport"
        Me.llbPrintSummaryReport.Size = New System.Drawing.Size(109, 13)
        Me.llbPrintSummaryReport.TabIndex = 145
        Me.llbPrintSummaryReport.TabStop = True
        Me.llbPrintSummaryReport.Text = "Print Summary Report"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(304, 32)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(138, 13)
        Me.Label73.TabIndex = 140
        Me.Label73.Text = "Number of Tests Witnessed"
        '
        'txtTestWitnessed
        '
        Me.txtTestWitnessed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestWitnessed.Location = New System.Drawing.Point(200, 32)
        Me.txtTestWitnessed.Name = "txtTestWitnessed"
        Me.txtTestWitnessed.Size = New System.Drawing.Size(100, 20)
        Me.txtTestWitnessed.TabIndex = 139
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(552, 32)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(125, 13)
        Me.Label70.TabIndex = 134
        Me.Label70.Text = "Tests Out Of Compliance"
        '
        'txtTestOutOfCompliance
        '
        Me.txtTestOutOfCompliance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestOutOfCompliance.Location = New System.Drawing.Point(448, 32)
        Me.txtTestOutOfCompliance.Name = "txtTestOutOfCompliance"
        Me.txtTestOutOfCompliance.Size = New System.Drawing.Size(100, 20)
        Me.txtTestOutOfCompliance.TabIndex = 133
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(552, 8)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(86, 13)
        Me.Label69.TabIndex = 132
        Me.Label69.Text = "Tests Completed"
        '
        'txtTestCompleted
        '
        Me.txtTestCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestCompleted.Location = New System.Drawing.Point(448, 8)
        Me.txtTestCompleted.Name = "txtTestCompleted"
        Me.txtTestCompleted.Size = New System.Drawing.Size(100, 20)
        Me.txtTestCompleted.TabIndex = 131
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(304, 8)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(82, 13)
        Me.Label68.TabIndex = 130
        Me.Label68.Text = "Tests Received"
        '
        'txtReceived
        '
        Me.txtReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceived.Location = New System.Drawing.Point(200, 8)
        Me.txtReceived.Name = "txtReceived"
        Me.txtReceived.Size = New System.Drawing.Size(100, 20)
        Me.txtReceived.TabIndex = 129
        '
        'Label65
        '
        Me.Label65.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(120, 32)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(80, 16)
        Me.Label65.TabIndex = 128
        Me.Label65.Text = "End Date"
        '
        'Label64
        '
        Me.Label64.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(120, 8)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(80, 16)
        Me.Label64.TabIndex = 127
        Me.Label64.Text = "Start Date"
        '
        'DTPMonthlyEnd
        '
        Me.DTPMonthlyEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPMonthlyEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPMonthlyEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPMonthlyEnd.Location = New System.Drawing.Point(8, 32)
        Me.DTPMonthlyEnd.Name = "DTPMonthlyEnd"
        Me.DTPMonthlyEnd.Size = New System.Drawing.Size(104, 20)
        Me.DTPMonthlyEnd.TabIndex = 126
        '
        'DTPMonthlyStart
        '
        Me.DTPMonthlyStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPMonthlyStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPMonthlyStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPMonthlyStart.Location = New System.Drawing.Point(8, 8)
        Me.DTPMonthlyStart.Name = "DTPMonthlyStart"
        Me.DTPMonthlyStart.Size = New System.Drawing.Size(104, 20)
        Me.DTPMonthlyStart.TabIndex = 125
        '
        'txtPercential
        '
        Me.txtPercential.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPercential.Location = New System.Drawing.Point(304, 80)
        Me.txtPercential.Name = "txtPercential"
        Me.txtPercential.Size = New System.Drawing.Size(32, 20)
        Me.txtPercential.TabIndex = 141
        Me.txtPercential.Text = "80"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(304, 56)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(205, 13)
        Me.Label72.TabIndex = 138
        Me.Label72.Text = "Median Time Taken to Complete Reviews"
        '
        'txtMedianTimeToComplete
        '
        Me.txtMedianTimeToComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMedianTimeToComplete.Location = New System.Drawing.Point(200, 56)
        Me.txtMedianTimeToComplete.Name = "txtMedianTimeToComplete"
        Me.txtMedianTimeToComplete.Size = New System.Drawing.Size(100, 20)
        Me.txtMedianTimeToComplete.TabIndex = 137
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(352, 80)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(217, 13)
        Me.Label71.TabIndex = 136
        Me.Label71.Text = "Percentile Time Taken to Complete Reviews"
        '
        'txt80Percent
        '
        Me.txt80Percent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt80Percent.Location = New System.Drawing.Point(200, 80)
        Me.txt80Percent.Name = "txt80Percent"
        Me.txt80Percent.Size = New System.Drawing.Size(100, 20)
        Me.txt80Percent.TabIndex = 135
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(336, 80)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(24, 16)
        Me.Label31.TabIndex = 142
        Me.Label31.Text = " %"
        '
        'llbPrintMonthlyReport
        '
        Me.llbPrintMonthlyReport.AutoSize = True
        Me.llbPrintMonthlyReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbPrintMonthlyReport.Location = New System.Drawing.Point(8, 80)
        Me.llbPrintMonthlyReport.Name = "llbPrintMonthlyReport"
        Me.llbPrintMonthlyReport.Size = New System.Drawing.Size(103, 13)
        Me.llbPrintMonthlyReport.TabIndex = 144
        Me.llbPrintMonthlyReport.TabStop = True
        Me.llbPrintMonthlyReport.Text = "Print Monthly Report"
        '
        'llbRunMonthlyReport
        '
        Me.llbRunMonthlyReport.AutoSize = True
        Me.llbRunMonthlyReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbRunMonthlyReport.Location = New System.Drawing.Point(8, 56)
        Me.llbRunMonthlyReport.Name = "llbRunMonthlyReport"
        Me.llbRunMonthlyReport.Size = New System.Drawing.Size(124, 13)
        Me.llbRunMonthlyReport.TabIndex = 143
        Me.llbRunMonthlyReport.TabStop = True
        Me.llbRunMonthlyReport.Text = "Click here to Run Report"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtReportText)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox5.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(3, 259)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(1128, 112)
        Me.GroupBox5.TabIndex = 126
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Test Report"
        '
        'txtReportText
        '
        Me.txtReportText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReportText.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportText.Location = New System.Drawing.Point(3, 22)
        Me.txtReportText.Name = "txtReportText"
        Me.txtReportText.Size = New System.Drawing.Size(1122, 87)
        Me.txtReportText.TabIndex = 48
        Me.txtReportText.Text = ""
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Splitter14)
        Me.GroupBox4.Controls.Add(Me.GroupBox15)
        Me.GroupBox4.Controls.Add(Me.txtOutOfComplianceReport)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox4.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(0, 374)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1134, 344)
        Me.GroupBox4.TabIndex = 125
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Non-Compliance Facilities"
        '
        'Splitter14
        '
        Me.Splitter14.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter14.Location = New System.Drawing.Point(3, 153)
        Me.Splitter14.Name = "Splitter14"
        Me.Splitter14.Size = New System.Drawing.Size(1128, 5)
        Me.Splitter14.TabIndex = 51
        Me.Splitter14.TabStop = False
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.dgrTestSummary)
        Me.GroupBox15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox15.Location = New System.Drawing.Point(3, 158)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(1128, 183)
        Me.GroupBox15.TabIndex = 50
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Source Test Summary"
        '
        'dgrTestSummary
        '
        Me.dgrTestSummary.DataMember = ""
        Me.dgrTestSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrTestSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrTestSummary.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrTestSummary.Location = New System.Drawing.Point(3, 22)
        Me.dgrTestSummary.Name = "dgrTestSummary"
        Me.dgrTestSummary.ReadOnly = True
        Me.dgrTestSummary.Size = New System.Drawing.Size(1122, 158)
        Me.dgrTestSummary.TabIndex = 2
        '
        'txtOutOfComplianceReport
        '
        Me.txtOutOfComplianceReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOutOfComplianceReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutOfComplianceReport.Location = New System.Drawing.Point(3, 22)
        Me.txtOutOfComplianceReport.Name = "txtOutOfComplianceReport"
        Me.txtOutOfComplianceReport.Size = New System.Drawing.Size(1128, 319)
        Me.txtOutOfComplianceReport.TabIndex = 49
        Me.txtOutOfComplianceReport.Text = ""
        '
        'btnRunReport
        '
        Me.btnRunReport.Location = New System.Drawing.Point(784, 503)
        Me.btnRunReport.Name = "btnRunReport"
        Me.btnRunReport.Size = New System.Drawing.Size(75, 23)
        Me.btnRunReport.TabIndex = 42
        Me.btnRunReport.Text = "Run Report"
        '
        'TPMiscTools
        '
        Me.TPMiscTools.BackColor = System.Drawing.Color.Transparent
        Me.TPMiscTools.Controls.Add(Me.TCMiscTools)
        Me.TPMiscTools.Location = New System.Drawing.Point(4, 40)
        Me.TPMiscTools.Name = "TPMiscTools"
        Me.TPMiscTools.Size = New System.Drawing.Size(1134, 718)
        Me.TPMiscTools.TabIndex = 13
        Me.TPMiscTools.Text = "Misc. Tools"
        Me.TPMiscTools.UseVisualStyleBackColor = True
        '
        'TCMiscTools
        '
        Me.TCMiscTools.Controls.Add(Me.TPMethods)
        Me.TCMiscTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMiscTools.Location = New System.Drawing.Point(0, 0)
        Me.TCMiscTools.Name = "TCMiscTools"
        Me.TCMiscTools.SelectedIndex = 0
        Me.TCMiscTools.Size = New System.Drawing.Size(1134, 718)
        Me.TCMiscTools.TabIndex = 0
        '
        'TPMethods
        '
        Me.TPMethods.Controls.Add(Me.Panel6)
        Me.TPMethods.Controls.Add(Me.Panel5)
        Me.TPMethods.Location = New System.Drawing.Point(4, 22)
        Me.TPMethods.Name = "TPMethods"
        Me.TPMethods.Padding = New System.Windows.Forms.Padding(3)
        Me.TPMethods.Size = New System.Drawing.Size(1126, 692)
        Me.TPMethods.TabIndex = 0
        Me.TPMethods.Text = "Edit Methods"
        Me.TPMethods.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.dgvMethods)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 103)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1120, 586)
        Me.Panel6.TabIndex = 1
        '
        'dgvMethods
        '
        Me.dgvMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMethods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMethods.Location = New System.Drawing.Point(0, 0)
        Me.dgvMethods.Name = "dgvMethods"
        Me.dgvMethods.ReadOnly = True
        Me.dgvMethods.Size = New System.Drawing.Size(1120, 586)
        Me.dgvMethods.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.txtMethodCode)
        Me.Panel5.Controls.Add(Me.txtMethodDescription)
        Me.Panel5.Controls.Add(Me.Label30)
        Me.Panel5.Controls.Add(Me.btnUpdateMethods)
        Me.Panel5.Controls.Add(Me.txtMethodNumber)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1120, 100)
        Me.Panel5.TabIndex = 0
        '
        'txtMethodCode
        '
        Me.txtMethodCode.Location = New System.Drawing.Point(119, 28)
        Me.txtMethodCode.Name = "txtMethodCode"
        Me.txtMethodCode.Size = New System.Drawing.Size(13, 20)
        Me.txtMethodCode.TabIndex = 5
        Me.txtMethodCode.Visible = False
        '
        'txtMethodDescription
        '
        Me.txtMethodDescription.Location = New System.Drawing.Point(138, 28)
        Me.txtMethodDescription.Multiline = True
        Me.txtMethodDescription.Name = "txtMethodDescription"
        Me.txtMethodDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMethodDescription.Size = New System.Drawing.Size(417, 49)
        Me.txtMethodDescription.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(135, 12)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(99, 13)
        Me.Label30.TabIndex = 3
        Me.Label30.Text = "Method Description"
        '
        'btnUpdateMethods
        '
        Me.btnUpdateMethods.AutoSize = True
        Me.btnUpdateMethods.Location = New System.Drawing.Point(18, 54)
        Me.btnUpdateMethods.Name = "btnUpdateMethods"
        Me.btnUpdateMethods.Size = New System.Drawing.Size(79, 23)
        Me.btnUpdateMethods.TabIndex = 2
        Me.btnUpdateMethods.Text = "Edit Methods"
        Me.btnUpdateMethods.UseVisualStyleBackColor = True
        '
        'txtMethodNumber
        '
        Me.txtMethodNumber.Location = New System.Drawing.Point(18, 28)
        Me.txtMethodNumber.Name = "txtMethodNumber"
        Me.txtMethodNumber.Size = New System.Drawing.Size(98, 20)
        Me.txtMethodNumber.TabIndex = 1
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(3, 12)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(113, 13)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "Method - example (1A)"
        '
        'TPTestReportAdd
        '
        Me.TPTestReportAdd.Controls.Add(Me.Panel11)
        Me.TPTestReportAdd.Location = New System.Drawing.Point(4, 40)
        Me.TPTestReportAdd.Name = "TPTestReportAdd"
        Me.TPTestReportAdd.Size = New System.Drawing.Size(1134, 718)
        Me.TPTestReportAdd.TabIndex = 14
        Me.TPTestReportAdd.Text = "Add Test Report"
        Me.TPTestReportAdd.UseVisualStyleBackColor = True
        '
        'Panel11
        '
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
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1134, 432)
        Me.Panel11.TabIndex = 0
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.btnReOpenHistoricTestReport)
        Me.Panel12.Controls.Add(Me.btnCloseHistoricTestReport)
        Me.Panel12.Controls.Add(Me.txtCloseTestReportRefNum)
        Me.Panel12.Controls.Add(Me.Label78)
        Me.Panel12.Location = New System.Drawing.Point(23, 285)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(372, 100)
        Me.Panel12.TabIndex = 11
        '
        'btnReOpenHistoricTestReport
        '
        Me.btnReOpenHistoricTestReport.AutoSize = True
        Me.btnReOpenHistoricTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReOpenHistoricTestReport.Location = New System.Drawing.Point(114, 68)
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
        Me.btnCloseHistoricTestReport.Location = New System.Drawing.Point(114, 39)
        Me.btnCloseHistoricTestReport.Name = "btnCloseHistoricTestReport"
        Me.btnCloseHistoricTestReport.Size = New System.Drawing.Size(102, 23)
        Me.btnCloseHistoricTestReport.TabIndex = 8
        Me.btnCloseHistoricTestReport.Text = "Close Test Report"
        Me.btnCloseHistoricTestReport.UseVisualStyleBackColor = True
        '
        'txtCloseTestReportRefNum
        '
        Me.txtCloseTestReportRefNum.Location = New System.Drawing.Point(114, 13)
        Me.txtCloseTestReportRefNum.Name = "txtCloseTestReportRefNum"
        Me.txtCloseTestReportRefNum.Size = New System.Drawing.Size(136, 20)
        Me.txtCloseTestReportRefNum.TabIndex = 3
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(10, 17)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(97, 13)
        Me.Label78.TabIndex = 4
        Me.Label78.Text = "Reference Number"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(383, 102)
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
        Me.DTPAddTestReportDateCompleted.Size = New System.Drawing.Size(110, 22)
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
        Me.btnClearAddTestReport.Location = New System.Drawing.Point(259, 218)
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
        Me.dtpAddTestReportDateReceived.Size = New System.Drawing.Size(110, 22)
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
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridBoolColumn1
        '
        Me.DataGridBoolColumn1.Width = 75
        '
        'DataGridBoolColumn2
        '
        Me.DataGridBoolColumn2.Width = 75
        '
        'ISMPManagersTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScroll = True
        Me.AutoScrollMargin = New System.Drawing.Size(10, 10)
        Me.AutoScrollMinSize = New System.Drawing.Size(10, 10)
        Me.ClientSize = New System.Drawing.Size(1142, 794)
        Me.Controls.Add(Me.PanelManagersTools)
        Me.Controls.Add(Me.SplitterManagersTools)
        Me.Controls.Add(Me.TBManagersTools)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(250, 173)
        Me.Name = "ISMPManagersTools"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Managers Tools"
        Me.PanelManagersTools.ResumeLayout(False)
        Me.TCManagersTools.ResumeLayout(False)
        Me.TPReportAssignment.ResumeLayout(False)
        Me.PanelReportAssignment.ResumeLayout(False)
        Me.PanelReportAssignment.PerformLayout()
        Me.TPUnitStatistics.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.PanelAll.ResumeLayout(False)
        Me.PanelAll.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        Me.PanelCombustMineral.ResumeLayout(False)
        Me.PanelCombustMineral.PerformLayout()
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox17.PerformLayout()
        Me.PanelChemVOC.ResumeLayout(False)
        Me.PanelChemVOC.PerformLayout()
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox18.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.TPUnitStatistics2.ResumeLayout(False)
        Me.GroupBox19.ResumeLayout(False)
        CType(Me.dgvUnitStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.TPUnitAssignment.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.TPTestReportStatistics.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.GBChoose.ResumeLayout(False)
        Me.GBChoose.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.TPAIRSReportsPrinted.ResumeLayout(False)
        Me.TPExcelFiles.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.dgrExcelFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPEngineerTestReport.ResumeLayout(False)
        Me.GroupBox14.ResumeLayout(False)
        Me.PanelDate.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        CType(Me.dgrEngineersFacilityList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPApplicationsReviewed.ResumeLayout(False)
        Me.TPApplicationsReviewed.PerformLayout()
        Me.TPMonthlyReport.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        CType(Me.dgrTestSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPMiscTools.ResumeLayout(False)
        Me.TCMiscTools.ResumeLayout(False)
        Me.TPMethods.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.dgvMethods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TPTestReportAdd.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
