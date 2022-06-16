<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPQueryGenerator
    Inherits BaseForm

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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbExport = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveQuery = New System.Windows.Forms.ToolStripButton()
        Me.tsbSearchQuery = New System.Windows.Forms.ToolStripButton()
        Me.tsbReSizeFilterOptions = New System.Windows.Forms.ToolStripButton()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rdbFacilityNameNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityNameEqual = New System.Windows.Forms.RadioButton()
        Me.Panel35 = New System.Windows.Forms.Panel()
        Me.rdbFacilityNameOr = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityNameAnd = New System.Windows.Forms.RadioButton()
        Me.txtFacilityNameSearch2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityNameSearch1 = New System.Windows.Forms.TextBox()
        Me.Panel36 = New System.Windows.Forms.Panel()
        Me.rdbAIRSNumberNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAIRSNumberEqual = New System.Windows.Forms.RadioButton()
        Me.Panel37 = New System.Windows.Forms.Panel()
        Me.rdbAIRSNumberOr = New System.Windows.Forms.RadioButton()
        Me.rdbAIRSNumberAnd = New System.Windows.Forms.RadioButton()
        Me.txtAIRSNumberSearch2 = New System.Windows.Forms.TextBox()
        Me.txtAIRSNumberSearch1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRunSearch = New System.Windows.Forms.Button()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.dgvQueryGenerator = New Iaip.IaipDataGridView()
        Me.lblQueryCount = New System.Windows.Forms.Label()
        Me.tcQueryOptions = New System.Windows.Forms.TabControl()
        Me.TPPhysicalLocation = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.rdbCountyNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbCountyEqual = New System.Windows.Forms.RadioButton()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.rdbDistrictNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbDistrictEqual = New System.Windows.Forms.RadioButton()
        Me.cboDistrictSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboDistrictSearch1 = New System.Windows.Forms.ComboBox()
        Me.cboCountySearch2 = New System.Windows.Forms.ComboBox()
        Me.cboCountySearch1 = New System.Windows.Forms.ComboBox()
        Me.chbDistrict = New System.Windows.Forms.CheckBox()
        Me.chbCounty = New System.Windows.Forms.CheckBox()
        Me.txtFacilityLatitudeSearch2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityLatitudeSearch1 = New System.Windows.Forms.TextBox()
        Me.txtFacilityLongitudeSearch2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityLongitudeSearch1 = New System.Windows.Forms.TextBox()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.rdbFacilityZipCodeNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityZipCodeEqual = New System.Windows.Forms.RadioButton()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.rdbFacilityZipCodeOr = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityZipCodeAnd = New System.Windows.Forms.RadioButton()
        Me.txtFacilityZipCodeSearch2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityZipCodeSearch1 = New System.Windows.Forms.TextBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.rdbFacilityCityNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityCityEqual = New System.Windows.Forms.RadioButton()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.rdbFacilityCityOr = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityCityAnd = New System.Windows.Forms.RadioButton()
        Me.txtFacilityCitySearch2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityCitySearch1 = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rdbFacilityStreet2NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityStreet2Equal = New System.Windows.Forms.RadioButton()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.rdbFacilityStreet2Or = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityStreet2And = New System.Windows.Forms.RadioButton()
        Me.txtFacilityStreet2Search2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityStreet2Search1 = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rdbFacilityStreet1NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityStreet1Equal = New System.Windows.Forms.RadioButton()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdbFacilityStreet1Or = New System.Windows.Forms.RadioButton()
        Me.rdbFacilityStreet1And = New System.Windows.Forms.RadioButton()
        Me.txtFacilityStreet1Search2 = New System.Windows.Forms.TextBox()
        Me.txtFacilityStreet1Search1 = New System.Windows.Forms.TextBox()
        Me.chbFacilityLatitude = New System.Windows.Forms.CheckBox()
        Me.chbFacilityLongitude = New System.Windows.Forms.CheckBox()
        Me.chbFacilityZipCode = New System.Windows.Forms.CheckBox()
        Me.chbFacilityCity = New System.Windows.Forms.CheckBox()
        Me.chbFacilityStreet2 = New System.Windows.Forms.CheckBox()
        Me.chbFacilityStreet1 = New System.Windows.Forms.CheckBox()
        Me.TPHeaderInformation = New System.Windows.Forms.TabPage()
        Me.Panel74 = New System.Windows.Forms.Panel()
        Me.rdbNAICSCodeNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbNAICSCodeEqual = New System.Windows.Forms.RadioButton()
        Me.Panel75 = New System.Windows.Forms.Panel()
        Me.rdbNAICSCodeOr = New System.Windows.Forms.RadioButton()
        Me.rdbNAICSCodeAnd = New System.Windows.Forms.RadioButton()
        Me.txtNAICSCodeSearch2 = New System.Windows.Forms.TextBox()
        Me.txtNAICSCodeSearch1 = New System.Windows.Forms.TextBox()
        Me.chbNAICSCode = New System.Windows.Forms.CheckBox()
        Me.cboOperationStatusSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboOperationStatusSearch1 = New System.Windows.Forms.ComboBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.rdbOperationStatusNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbOperationStatusEqual = New System.Windows.Forms.RadioButton()
        Me.chbOperationStatus = New System.Windows.Forms.CheckBox()
        Me.cboCMSUniverseSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboCMSUniverseSearch1 = New System.Windows.Forms.ComboBox()
        Me.Panel33 = New System.Windows.Forms.Panel()
        Me.rdbPlantDescriptionNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbPlantDescriptionEqual = New System.Windows.Forms.RadioButton()
        Me.Panel34 = New System.Windows.Forms.Panel()
        Me.rdbPlantDescriptionOR = New System.Windows.Forms.RadioButton()
        Me.rdbPlantDescriptionAND = New System.Windows.Forms.RadioButton()
        Me.txtPlantDescriptionSearch2 = New System.Windows.Forms.TextBox()
        Me.txtPlantDescriptionSearch1 = New System.Windows.Forms.TextBox()
        Me.chbPlantDescription = New System.Windows.Forms.CheckBox()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.rdbCMSUniverseNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbCMSUniverseEqual = New System.Windows.Forms.RadioButton()
        Me.chbCMSUniverse = New System.Windows.Forms.CheckBox()
        Me.DTPShutDownDateSearch2 = New System.Windows.Forms.DateTimePicker()
        Me.DTPShutDownDateSearch1 = New System.Windows.Forms.DateTimePicker()
        Me.DTPStartUpDateSearch2 = New System.Windows.Forms.DateTimePicker()
        Me.DTPStartUpDateSearch1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.rdbSICCodeNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbSICCodeEqual = New System.Windows.Forms.RadioButton()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.rdbSICCodeOr = New System.Windows.Forms.RadioButton()
        Me.rdbSICCodeAnd = New System.Windows.Forms.RadioButton()
        Me.txtSICCodeSearch2 = New System.Windows.Forms.TextBox()
        Me.txtSICCodeSearch1 = New System.Windows.Forms.TextBox()
        Me.cboClassificationSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboClassificationSearch1 = New System.Windows.Forms.ComboBox()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.rdbClassificationNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbClassificationEqual = New System.Windows.Forms.RadioButton()
        Me.chbShutDownDate = New System.Windows.Forms.CheckBox()
        Me.chbStartUpDate = New System.Windows.Forms.CheckBox()
        Me.chbSICCode = New System.Windows.Forms.CheckBox()
        Me.chbClassification = New System.Windows.Forms.CheckBox()
        Me.TPHeaderInformation2 = New System.Windows.Forms.TabPage()
        Me.chbStateProgramCodes = New System.Windows.Forms.CheckBox()
        Me.chbAttainmentStatus = New System.Windows.Forms.CheckBox()
        Me.Panel63 = New System.Windows.Forms.Panel()
        Me.rdbHAPMajorNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbHAPMajorEqual = New System.Windows.Forms.RadioButton()
        Me.Panel62 = New System.Windows.Forms.Panel()
        Me.rdbNSRPSDMajorNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbNSRPSDMajorEqual = New System.Windows.Forms.RadioButton()
        Me.Panel61 = New System.Windows.Forms.Panel()
        Me.rdbPMNoNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbPMNoEqual = New System.Windows.Forms.RadioButton()
        Me.Panel60 = New System.Windows.Forms.Panel()
        Me.rdbPMMaconNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbPMMaconEqual = New System.Windows.Forms.RadioButton()
        Me.Panel59 = New System.Windows.Forms.Panel()
        Me.rdbPMFloydNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbPMFloydEqual = New System.Windows.Forms.RadioButton()
        Me.Panel58 = New System.Windows.Forms.Panel()
        Me.rdbPMChattanoogaNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbPMChattanoogaEqual = New System.Windows.Forms.RadioButton()
        Me.Panel57 = New System.Windows.Forms.Panel()
        Me.rdbPMAtlantaNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbPMAtlantaEqual = New System.Windows.Forms.RadioButton()
        Me.Panel56 = New System.Windows.Forms.Panel()
        Me.rdb8HrNoNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdb8HrNoEqual = New System.Windows.Forms.RadioButton()
        Me.Panel55 = New System.Windows.Forms.Panel()
        Me.rdb8HrMaconNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdb8HrMaconEqual = New System.Windows.Forms.RadioButton()
        Me.Panel54 = New System.Windows.Forms.Panel()
        Me.rdb8HrAtlantaNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdb8HrAtlantaEqual = New System.Windows.Forms.RadioButton()
        Me.Panel53 = New System.Windows.Forms.Panel()
        Me.rdb1HrContributeNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdb1HrContributeEqual = New System.Windows.Forms.RadioButton()
        Me.Panel52 = New System.Windows.Forms.Panel()
        Me.rdb1HrNoNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdb1HrNoEqual = New System.Windows.Forms.RadioButton()
        Me.chbHAPMajor = New System.Windows.Forms.CheckBox()
        Me.chbNSRPSDMajor = New System.Windows.Forms.CheckBox()
        Me.Panel51 = New System.Windows.Forms.Panel()
        Me.rdb1HrYesNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdb1HrYesEqual = New System.Windows.Forms.RadioButton()
        Me.chbPMNo = New System.Windows.Forms.CheckBox()
        Me.chbPMMacon = New System.Windows.Forms.CheckBox()
        Me.chbPMFloyd = New System.Windows.Forms.CheckBox()
        Me.chbPMChattanooga = New System.Windows.Forms.CheckBox()
        Me.chbPMAtlanta = New System.Windows.Forms.CheckBox()
        Me.chb8HrNo = New System.Windows.Forms.CheckBox()
        Me.chb8HrMacon = New System.Windows.Forms.CheckBox()
        Me.chb8HrAtlanta = New System.Windows.Forms.CheckBox()
        Me.chb1HrContribute = New System.Windows.Forms.CheckBox()
        Me.chb1HrNo = New System.Windows.Forms.CheckBox()
        Me.chb1HrYes = New System.Windows.Forms.CheckBox()
        Me.TPAirProgramCodes = New System.Windows.Forms.TabPage()
        Me.Panel70 = New System.Windows.Forms.Panel()
        Me.rdbAPCOr = New System.Windows.Forms.RadioButton()
        Me.rdbAPCAnd = New System.Windows.Forms.RadioButton()
        Me.chbViewAirPrograms = New System.Windows.Forms.CheckBox()
        Me.chbAPC0 = New System.Windows.Forms.CheckBox()
        Me.Panel50 = New System.Windows.Forms.Panel()
        Me.rdbAPCVNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPCVEqual = New System.Windows.Forms.RadioButton()
        Me.chbAPC1 = New System.Windows.Forms.CheckBox()
        Me.Panel38 = New System.Windows.Forms.Panel()
        Me.rdbAPC0NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC0Equal = New System.Windows.Forms.RadioButton()
        Me.Panel49 = New System.Windows.Forms.Panel()
        Me.rdbAPCMNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPCMEqual = New System.Windows.Forms.RadioButton()
        Me.Panel39 = New System.Windows.Forms.Panel()
        Me.rdbAPC1NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC1Equal = New System.Windows.Forms.RadioButton()
        Me.Panel48 = New System.Windows.Forms.Panel()
        Me.rdbAPCINotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPCIEqual = New System.Windows.Forms.RadioButton()
        Me.chbAPCV = New System.Windows.Forms.CheckBox()
        Me.chbAPCM = New System.Windows.Forms.CheckBox()
        Me.chbAPC3 = New System.Windows.Forms.CheckBox()
        Me.chbAPCI = New System.Windows.Forms.CheckBox()
        Me.Panel47 = New System.Windows.Forms.Panel()
        Me.rdbAPCFNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPCFEqual = New System.Windows.Forms.RadioButton()
        Me.chbAPC4 = New System.Windows.Forms.CheckBox()
        Me.Panel40 = New System.Windows.Forms.Panel()
        Me.rdbAPC3NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC3Equal = New System.Windows.Forms.RadioButton()
        Me.Panel46 = New System.Windows.Forms.Panel()
        Me.rdbAPCANotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPCAEqual = New System.Windows.Forms.RadioButton()
        Me.Panel41 = New System.Windows.Forms.Panel()
        Me.rdbAPC4NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC4Equal = New System.Windows.Forms.RadioButton()
        Me.Panel45 = New System.Windows.Forms.Panel()
        Me.rdbAPC9NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC9Equal = New System.Windows.Forms.RadioButton()
        Me.chbAPCF = New System.Windows.Forms.CheckBox()
        Me.chbAPC6 = New System.Windows.Forms.CheckBox()
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.rdbAPC8NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC8Equal = New System.Windows.Forms.RadioButton()
        Me.chbAPCA = New System.Windows.Forms.CheckBox()
        Me.chbAPC7 = New System.Windows.Forms.CheckBox()
        Me.chbAPC8 = New System.Windows.Forms.CheckBox()
        Me.Panel43 = New System.Windows.Forms.Panel()
        Me.rdbAPC7NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC7Equal = New System.Windows.Forms.RadioButton()
        Me.Panel42 = New System.Windows.Forms.Panel()
        Me.rdbAPC6NotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbAPC6Equal = New System.Windows.Forms.RadioButton()
        Me.chbAPC9 = New System.Windows.Forms.CheckBox()
        Me.TPSubpartData = New System.Windows.Forms.TabPage()
        Me.cboPart63Search2 = New System.Windows.Forms.ComboBox()
        Me.cboPart60Search2 = New System.Windows.Forms.ComboBox()
        Me.cboPart63Search1 = New System.Windows.Forms.ComboBox()
        Me.cboPart60Search1 = New System.Windows.Forms.ComboBox()
        Me.cboPart61Search2 = New System.Windows.Forms.ComboBox()
        Me.cboPart61Search1 = New System.Windows.Forms.ComboBox()
        Me.cboSIPSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboSIPSearch1 = New System.Windows.Forms.ComboBox()
        Me.chbSIP = New System.Windows.Forms.CheckBox()
        Me.chbAllSubparts = New System.Windows.Forms.CheckBox()
        Me.chbPart61Subpart = New System.Windows.Forms.CheckBox()
        Me.chbPart60Subpart = New System.Windows.Forms.CheckBox()
        Me.chbPart63Subpart = New System.Windows.Forms.CheckBox()
        Me.TPComplianceInfo = New System.Windows.Forms.TabPage()
        Me.Panel76 = New System.Windows.Forms.Panel()
        Me.rdbDistrictResponsibleFalse = New System.Windows.Forms.RadioButton()
        Me.rdbDistrictResponsibleTrue = New System.Windows.Forms.RadioButton()
        Me.chbDistrictResponsible = New System.Windows.Forms.CheckBox()
        Me.DTPLastFCESearch2 = New System.Windows.Forms.DateTimePicker()
        Me.DTPLastFCESearch1 = New System.Windows.Forms.DateTimePicker()
        Me.chbLastFCE = New System.Windows.Forms.CheckBox()
        Me.cboSSCPUnitSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboSSCPUnitSearch1 = New System.Windows.Forms.ComboBox()
        Me.Panel72 = New System.Windows.Forms.Panel()
        Me.rdbSSCPUnitNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbSSCPUnitEqual = New System.Windows.Forms.RadioButton()
        Me.chbSSCPUnit = New System.Windows.Forms.CheckBox()
        Me.cboSSCPEngineerSearch2 = New System.Windows.Forms.ComboBox()
        Me.cboSSCPEngineerSearch1 = New System.Windows.Forms.ComboBox()
        Me.Panel69 = New System.Windows.Forms.Panel()
        Me.rdbSSCPEngineerNotEqual = New System.Windows.Forms.RadioButton()
        Me.rdbSSCPEngineerEqual = New System.Windows.Forms.RadioButton()
        Me.chbSSCPEngineer = New System.Windows.Forms.CheckBox()
        Me.TPCannedReports = New System.Windows.Forms.TabPage()
        Me.lblCannedQueryCount = New System.Windows.Forms.Label()
        Me.cboCannedSelection = New System.Windows.Forms.ComboBox()
        Me.lblCannedSelection = New System.Windows.Forms.Label()
        Me.lblCannedEndDate = New System.Windows.Forms.Label()
        Me.lblCannedStartDate = New System.Windows.Forms.Label()
        Me.dtpCannedEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpCannedStartDate = New System.Windows.Forms.DateTimePicker()
        Me.rdbCannedHistoryClass = New System.Windows.Forms.RadioButton()
        Me.rdbCannedHistoryAirProgram = New System.Windows.Forms.RadioButton()
        Me.rdbCannedPermitContactData = New System.Windows.Forms.RadioButton()
        Me.lblCannedHistoryAirProgram = New System.Windows.Forms.Label()
        Me.lblCannedHistoryClass = New System.Windows.Forms.Label()
        Me.lblCannedPermitContactData = New System.Windows.Forms.Label()
        Me.btnRunCannedReport = New System.Windows.Forms.Button()
        Me.BasicSearchGroup = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.bgwQueryGenerator = New System.ComponentModel.BackgroundWorker()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveSearchQueryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenSavedSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mmiExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel35.SuspendLayout()
        Me.Panel36.SuspendLayout()
        Me.Panel37.SuspendLayout()
        CType(Me.dgvQueryGenerator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcQueryOptions.SuspendLayout()
        Me.TPPhysicalLocation.SuspendLayout()
        Me.Panel30.SuspendLayout()
        Me.Panel29.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TPHeaderInformation.SuspendLayout()
        Me.Panel74.SuspendLayout()
        Me.Panel75.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel33.SuspendLayout()
        Me.Panel34.SuspendLayout()
        Me.Panel31.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.TPHeaderInformation2.SuspendLayout()
        Me.Panel63.SuspendLayout()
        Me.Panel62.SuspendLayout()
        Me.Panel61.SuspendLayout()
        Me.Panel60.SuspendLayout()
        Me.Panel59.SuspendLayout()
        Me.Panel58.SuspendLayout()
        Me.Panel57.SuspendLayout()
        Me.Panel56.SuspendLayout()
        Me.Panel55.SuspendLayout()
        Me.Panel54.SuspendLayout()
        Me.Panel53.SuspendLayout()
        Me.Panel52.SuspendLayout()
        Me.Panel51.SuspendLayout()
        Me.TPAirProgramCodes.SuspendLayout()
        Me.Panel70.SuspendLayout()
        Me.Panel50.SuspendLayout()
        Me.Panel38.SuspendLayout()
        Me.Panel49.SuspendLayout()
        Me.Panel39.SuspendLayout()
        Me.Panel48.SuspendLayout()
        Me.Panel47.SuspendLayout()
        Me.Panel40.SuspendLayout()
        Me.Panel46.SuspendLayout()
        Me.Panel41.SuspendLayout()
        Me.Panel45.SuspendLayout()
        Me.Panel44.SuspendLayout()
        Me.Panel43.SuspendLayout()
        Me.Panel42.SuspendLayout()
        Me.TPSubpartData.SuspendLayout()
        Me.TPComplianceInfo.SuspendLayout()
        Me.Panel76.SuspendLayout()
        Me.Panel72.SuspendLayout()
        Me.Panel69.SuspendLayout()
        Me.TPCannedReports.SuspendLayout()
        Me.BasicSearchGroup.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExport, Me.tsbSaveQuery, Me.tsbSearchQuery, Me.tsbReSizeFilterOptions})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(739, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbExport
        '
        Me.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbExport.Image = Global.Iaip.My.Resources.Resources.SpreadsheetIcon
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(23, 22)
        Me.tsbExport.Text = "Export to Excel"
        '
        'tsbSaveQuery
        '
        Me.tsbSaveQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSaveQuery.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.tsbSaveQuery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveQuery.Name = "tsbSaveQuery"
        Me.tsbSaveQuery.Size = New System.Drawing.Size(23, 22)
        Me.tsbSaveQuery.Text = "Save Query"
        '
        'tsbSearchQuery
        '
        Me.tsbSearchQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSearchQuery.Image = Global.Iaip.My.Resources.Resources.OpenFolderIcon
        Me.tsbSearchQuery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSearchQuery.Name = "tsbSearchQuery"
        Me.tsbSearchQuery.Size = New System.Drawing.Size(23, 22)
        Me.tsbSearchQuery.Text = "Open Query"
        '
        'tsbReSizeFilterOptions
        '
        Me.tsbReSizeFilterOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbReSizeFilterOptions.Image = Global.Iaip.My.Resources.Resources.PanelResizeIcon
        Me.tsbReSizeFilterOptions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReSizeFilterOptions.Name = "tsbReSizeFilterOptions"
        Me.tsbReSizeFilterOptions.Size = New System.Drawing.Size(23, 22)
        Me.tsbReSizeFilterOptions.Text = "Resize form"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(619, 67)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(84, 20)
        Me.btnReset.TabIndex = 16
        Me.btnReset.Text = "Reset Form"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdbFacilityNameNotEqual)
        Me.Panel6.Controls.Add(Me.rdbFacilityNameEqual)
        Me.Panel6.Location = New System.Drawing.Point(473, 66)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(98, 24)
        Me.Panel6.TabIndex = 272
        '
        'rdbFacilityNameNotEqual
        '
        Me.rdbFacilityNameNotEqual.AutoSize = True
        Me.rdbFacilityNameNotEqual.Location = New System.Drawing.Point(42, 3)
        Me.rdbFacilityNameNotEqual.Name = "rdbFacilityNameNotEqual"
        Me.rdbFacilityNameNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbFacilityNameNotEqual.TabIndex = 13
        Me.rdbFacilityNameNotEqual.Text = "<>"
        '
        'rdbFacilityNameEqual
        '
        Me.rdbFacilityNameEqual.AutoSize = True
        Me.rdbFacilityNameEqual.Checked = True
        Me.rdbFacilityNameEqual.Location = New System.Drawing.Point(7, 3)
        Me.rdbFacilityNameEqual.Name = "rdbFacilityNameEqual"
        Me.rdbFacilityNameEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbFacilityNameEqual.TabIndex = 12
        Me.rdbFacilityNameEqual.TabStop = True
        Me.rdbFacilityNameEqual.Text = "="
        '
        'Panel35
        '
        Me.Panel35.Controls.Add(Me.rdbFacilityNameOr)
        Me.Panel35.Controls.Add(Me.rdbFacilityNameAnd)
        Me.Panel35.Location = New System.Drawing.Point(373, 67)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Size = New System.Drawing.Size(100, 21)
        Me.Panel35.TabIndex = 271
        '
        'rdbFacilityNameOr
        '
        Me.rdbFacilityNameOr.AutoSize = True
        Me.rdbFacilityNameOr.Checked = True
        Me.rdbFacilityNameOr.Location = New System.Drawing.Point(58, 2)
        Me.rdbFacilityNameOr.Name = "rdbFacilityNameOr"
        Me.rdbFacilityNameOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbFacilityNameOr.TabIndex = 11
        Me.rdbFacilityNameOr.TabStop = True
        Me.rdbFacilityNameOr.Text = "OR"
        '
        'rdbFacilityNameAnd
        '
        Me.rdbFacilityNameAnd.AutoSize = True
        Me.rdbFacilityNameAnd.Location = New System.Drawing.Point(4, 2)
        Me.rdbFacilityNameAnd.Name = "rdbFacilityNameAnd"
        Me.rdbFacilityNameAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbFacilityNameAnd.TabIndex = 10
        Me.rdbFacilityNameAnd.Text = "AND"
        '
        'txtFacilityNameSearch2
        '
        Me.txtFacilityNameSearch2.Location = New System.Drawing.Point(237, 68)
        Me.txtFacilityNameSearch2.Name = "txtFacilityNameSearch2"
        Me.txtFacilityNameSearch2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityNameSearch2.TabIndex = 9
        '
        'txtFacilityNameSearch1
        '
        Me.txtFacilityNameSearch1.Location = New System.Drawing.Point(97, 68)
        Me.txtFacilityNameSearch1.Name = "txtFacilityNameSearch1"
        Me.txtFacilityNameSearch1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityNameSearch1.TabIndex = 8
        '
        'Panel36
        '
        Me.Panel36.Controls.Add(Me.rdbAIRSNumberNotEqual)
        Me.Panel36.Controls.Add(Me.rdbAIRSNumberEqual)
        Me.Panel36.Location = New System.Drawing.Point(473, 39)
        Me.Panel36.Name = "Panel36"
        Me.Panel36.Size = New System.Drawing.Size(98, 22)
        Me.Panel36.TabIndex = 270
        '
        'rdbAIRSNumberNotEqual
        '
        Me.rdbAIRSNumberNotEqual.AutoSize = True
        Me.rdbAIRSNumberNotEqual.Location = New System.Drawing.Point(42, 3)
        Me.rdbAIRSNumberNotEqual.Name = "rdbAIRSNumberNotEqual"
        Me.rdbAIRSNumberNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAIRSNumberNotEqual.TabIndex = 6
        Me.rdbAIRSNumberNotEqual.Text = "<>"
        '
        'rdbAIRSNumberEqual
        '
        Me.rdbAIRSNumberEqual.AutoSize = True
        Me.rdbAIRSNumberEqual.Checked = True
        Me.rdbAIRSNumberEqual.Location = New System.Drawing.Point(7, 3)
        Me.rdbAIRSNumberEqual.Name = "rdbAIRSNumberEqual"
        Me.rdbAIRSNumberEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbAIRSNumberEqual.TabIndex = 5
        Me.rdbAIRSNumberEqual.TabStop = True
        Me.rdbAIRSNumberEqual.Text = "="
        '
        'Panel37
        '
        Me.Panel37.Controls.Add(Me.rdbAIRSNumberOr)
        Me.Panel37.Controls.Add(Me.rdbAIRSNumberAnd)
        Me.Panel37.Location = New System.Drawing.Point(373, 40)
        Me.Panel37.Name = "Panel37"
        Me.Panel37.Size = New System.Drawing.Size(100, 21)
        Me.Panel37.TabIndex = 269
        '
        'rdbAIRSNumberOr
        '
        Me.rdbAIRSNumberOr.AutoSize = True
        Me.rdbAIRSNumberOr.Checked = True
        Me.rdbAIRSNumberOr.Location = New System.Drawing.Point(58, 2)
        Me.rdbAIRSNumberOr.Name = "rdbAIRSNumberOr"
        Me.rdbAIRSNumberOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbAIRSNumberOr.TabIndex = 4
        Me.rdbAIRSNumberOr.TabStop = True
        Me.rdbAIRSNumberOr.Text = "OR"
        '
        'rdbAIRSNumberAnd
        '
        Me.rdbAIRSNumberAnd.AutoSize = True
        Me.rdbAIRSNumberAnd.Location = New System.Drawing.Point(4, 2)
        Me.rdbAIRSNumberAnd.Name = "rdbAIRSNumberAnd"
        Me.rdbAIRSNumberAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbAIRSNumberAnd.TabIndex = 3
        Me.rdbAIRSNumberAnd.Text = "AND"
        '
        'txtAIRSNumberSearch2
        '
        Me.txtAIRSNumberSearch2.Location = New System.Drawing.Point(237, 41)
        Me.txtAIRSNumberSearch2.Name = "txtAIRSNumberSearch2"
        Me.txtAIRSNumberSearch2.Size = New System.Drawing.Size(127, 20)
        Me.txtAIRSNumberSearch2.TabIndex = 2
        '
        'txtAIRSNumberSearch1
        '
        Me.txtAIRSNumberSearch1.Location = New System.Drawing.Point(97, 41)
        Me.txtAIRSNumberSearch1.Name = "txtAIRSNumberSearch1"
        Me.txtAIRSNumberSearch1.Size = New System.Drawing.Size(127, 20)
        Me.txtAIRSNumberSearch1.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(234, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 264
        Me.Label3.Text = "Search Field # 2"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(94, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 14)
        Me.Label2.TabIndex = 263
        Me.Label2.Text = "Search Field # 1"
        '
        'btnRunSearch
        '
        Me.btnRunSearch.Location = New System.Drawing.Point(619, 10)
        Me.btnRunSearch.Name = "btnRunSearch"
        Me.btnRunSearch.Size = New System.Drawing.Size(84, 28)
        Me.btnRunSearch.TabIndex = 15
        Me.btnRunSearch.Text = "Run Search"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 534)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(739, 5)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'dgvQueryGenerator
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvQueryGenerator.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvQueryGenerator.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvQueryGenerator.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvQueryGenerator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvQueryGenerator.DecimalFieldFormat = "N6"
        Me.dgvQueryGenerator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvQueryGenerator.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvQueryGenerator.LinkifyColumnByName = Nothing
        Me.dgvQueryGenerator.LinkifyFirstColumn = True
        Me.dgvQueryGenerator.Location = New System.Drawing.Point(0, 539)
        Me.dgvQueryGenerator.Name = "dgvQueryGenerator"
        Me.dgvQueryGenerator.ResultsCountLabel = Me.lblQueryCount
        Me.dgvQueryGenerator.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvQueryGenerator.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvQueryGenerator.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvQueryGenerator.Size = New System.Drawing.Size(739, 122)
        Me.dgvQueryGenerator.StandardTab = True
        Me.dgvQueryGenerator.TabIndex = 13
        '
        'lblQueryCount
        '
        Me.lblQueryCount.AutoSize = True
        Me.lblQueryCount.Location = New System.Drawing.Point(619, 44)
        Me.lblQueryCount.Name = "lblQueryCount"
        Me.lblQueryCount.Size = New System.Drawing.Size(31, 13)
        Me.lblQueryCount.TabIndex = 275
        Me.lblQueryCount.Text = "Hello"
        '
        'tcQueryOptions
        '
        Me.tcQueryOptions.Controls.Add(Me.TPPhysicalLocation)
        Me.tcQueryOptions.Controls.Add(Me.TPHeaderInformation)
        Me.tcQueryOptions.Controls.Add(Me.TPHeaderInformation2)
        Me.tcQueryOptions.Controls.Add(Me.TPAirProgramCodes)
        Me.tcQueryOptions.Controls.Add(Me.TPSubpartData)
        Me.tcQueryOptions.Controls.Add(Me.TPComplianceInfo)
        Me.tcQueryOptions.Controls.Add(Me.TPCannedReports)
        Me.tcQueryOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.tcQueryOptions.Location = New System.Drawing.Point(0, 145)
        Me.tcQueryOptions.Name = "tcQueryOptions"
        Me.tcQueryOptions.SelectedIndex = 0
        Me.tcQueryOptions.Size = New System.Drawing.Size(739, 389)
        Me.tcQueryOptions.TabIndex = 1
        '
        'TPPhysicalLocation
        '
        Me.TPPhysicalLocation.Controls.Add(Me.Label5)
        Me.TPPhysicalLocation.Controls.Add(Me.Label4)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel30)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel29)
        Me.TPPhysicalLocation.Controls.Add(Me.cboDistrictSearch2)
        Me.TPPhysicalLocation.Controls.Add(Me.cboDistrictSearch1)
        Me.TPPhysicalLocation.Controls.Add(Me.cboCountySearch2)
        Me.TPPhysicalLocation.Controls.Add(Me.cboCountySearch1)
        Me.TPPhysicalLocation.Controls.Add(Me.chbDistrict)
        Me.TPPhysicalLocation.Controls.Add(Me.chbCounty)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityLatitudeSearch2)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityLatitudeSearch1)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityLongitudeSearch2)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityLongitudeSearch1)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel15)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel16)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityZipCodeSearch2)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityZipCodeSearch1)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel13)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel14)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityCitySearch2)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityCitySearch1)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel7)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel8)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityStreet2Search2)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityStreet2Search1)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel4)
        Me.TPPhysicalLocation.Controls.Add(Me.Panel5)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityStreet1Search2)
        Me.TPPhysicalLocation.Controls.Add(Me.txtFacilityStreet1Search1)
        Me.TPPhysicalLocation.Controls.Add(Me.chbFacilityLatitude)
        Me.TPPhysicalLocation.Controls.Add(Me.chbFacilityLongitude)
        Me.TPPhysicalLocation.Controls.Add(Me.chbFacilityZipCode)
        Me.TPPhysicalLocation.Controls.Add(Me.chbFacilityCity)
        Me.TPPhysicalLocation.Controls.Add(Me.chbFacilityStreet2)
        Me.TPPhysicalLocation.Controls.Add(Me.chbFacilityStreet1)
        Me.TPPhysicalLocation.Location = New System.Drawing.Point(4, 22)
        Me.TPPhysicalLocation.Name = "TPPhysicalLocation"
        Me.TPPhysicalLocation.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPhysicalLocation.Size = New System.Drawing.Size(731, 363)
        Me.TPPhysicalLocation.TabIndex = 0
        Me.TPPhysicalLocation.Text = "Physical Location"
        Me.TPPhysicalLocation.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(422, 137)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 164
        Me.Label5.Text = "(81.0 — 85.9)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(422, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 163
        Me.Label4.Text = "(30.5 — 35.0)"
        '
        'Panel30
        '
        Me.Panel30.AutoSize = True
        Me.Panel30.Controls.Add(Me.rdbCountyNotEqual)
        Me.Panel30.Controls.Add(Me.rdbCountyEqual)
        Me.Panel30.Location = New System.Drawing.Point(539, 153)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(76, 27)
        Me.Panel30.TabIndex = 176
        '
        'rdbCountyNotEqual
        '
        Me.rdbCountyNotEqual.AutoSize = True
        Me.rdbCountyNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbCountyNotEqual.Name = "rdbCountyNotEqual"
        Me.rdbCountyNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbCountyNotEqual.TabIndex = 64
        Me.rdbCountyNotEqual.Text = "<>"
        '
        'rdbCountyEqual
        '
        Me.rdbCountyEqual.AutoSize = True
        Me.rdbCountyEqual.Checked = True
        Me.rdbCountyEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbCountyEqual.Name = "rdbCountyEqual"
        Me.rdbCountyEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbCountyEqual.TabIndex = 63
        Me.rdbCountyEqual.TabStop = True
        Me.rdbCountyEqual.Text = "="
        '
        'Panel29
        '
        Me.Panel29.AutoSize = True
        Me.Panel29.Controls.Add(Me.rdbDistrictNotEqual)
        Me.Panel29.Controls.Add(Me.rdbDistrictEqual)
        Me.Panel29.Location = New System.Drawing.Point(539, 178)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(76, 27)
        Me.Panel29.TabIndex = 178
        '
        'rdbDistrictNotEqual
        '
        Me.rdbDistrictNotEqual.AutoSize = True
        Me.rdbDistrictNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbDistrictNotEqual.Name = "rdbDistrictNotEqual"
        Me.rdbDistrictNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbDistrictNotEqual.TabIndex = 72
        Me.rdbDistrictNotEqual.Text = "<>"
        '
        'rdbDistrictEqual
        '
        Me.rdbDistrictEqual.AutoSize = True
        Me.rdbDistrictEqual.Checked = True
        Me.rdbDistrictEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbDistrictEqual.Name = "rdbDistrictEqual"
        Me.rdbDistrictEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbDistrictEqual.TabIndex = 71
        Me.rdbDistrictEqual.TabStop = True
        Me.rdbDistrictEqual.Text = "="
        '
        'cboDistrictSearch2
        '
        Me.cboDistrictSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistrictSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistrictSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrictSearch2.Location = New System.Drawing.Point(281, 184)
        Me.cboDistrictSearch2.Name = "cboDistrictSearch2"
        Me.cboDistrictSearch2.Size = New System.Drawing.Size(127, 21)
        Me.cboDistrictSearch2.TabIndex = 68
        '
        'cboDistrictSearch1
        '
        Me.cboDistrictSearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistrictSearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistrictSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrictSearch1.Location = New System.Drawing.Point(141, 184)
        Me.cboDistrictSearch1.Name = "cboDistrictSearch1"
        Me.cboDistrictSearch1.Size = New System.Drawing.Size(127, 21)
        Me.cboDistrictSearch1.TabIndex = 67
        '
        'cboCountySearch2
        '
        Me.cboCountySearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCountySearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCountySearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCountySearch2.Location = New System.Drawing.Point(281, 159)
        Me.cboCountySearch2.Name = "cboCountySearch2"
        Me.cboCountySearch2.Size = New System.Drawing.Size(127, 21)
        Me.cboCountySearch2.TabIndex = 60
        '
        'cboCountySearch1
        '
        Me.cboCountySearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCountySearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCountySearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCountySearch1.Location = New System.Drawing.Point(141, 159)
        Me.cboCountySearch1.Name = "cboCountySearch1"
        Me.cboCountySearch1.Size = New System.Drawing.Size(127, 21)
        Me.cboCountySearch1.TabIndex = 59
        '
        'chbDistrict
        '
        Me.chbDistrict.AutoSize = True
        Me.chbDistrict.Location = New System.Drawing.Point(8, 186)
        Me.chbDistrict.Name = "chbDistrict"
        Me.chbDistrict.Size = New System.Drawing.Size(58, 17)
        Me.chbDistrict.TabIndex = 66
        Me.chbDistrict.Text = "District"
        '
        'chbCounty
        '
        Me.chbCounty.AutoSize = True
        Me.chbCounty.Location = New System.Drawing.Point(8, 161)
        Me.chbCounty.Name = "chbCounty"
        Me.chbCounty.Size = New System.Drawing.Size(59, 17)
        Me.chbCounty.TabIndex = 58
        Me.chbCounty.Text = "County"
        '
        'txtFacilityLatitudeSearch2
        '
        Me.txtFacilityLatitudeSearch2.Location = New System.Drawing.Point(281, 109)
        Me.txtFacilityLatitudeSearch2.Name = "txtFacilityLatitudeSearch2"
        Me.txtFacilityLatitudeSearch2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityLatitudeSearch2.TabIndex = 50
        '
        'txtFacilityLatitudeSearch1
        '
        Me.txtFacilityLatitudeSearch1.Location = New System.Drawing.Point(141, 109)
        Me.txtFacilityLatitudeSearch1.Name = "txtFacilityLatitudeSearch1"
        Me.txtFacilityLatitudeSearch1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityLatitudeSearch1.TabIndex = 49
        '
        'txtFacilityLongitudeSearch2
        '
        Me.txtFacilityLongitudeSearch2.Location = New System.Drawing.Point(281, 134)
        Me.txtFacilityLongitudeSearch2.Name = "txtFacilityLongitudeSearch2"
        Me.txtFacilityLongitudeSearch2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityLongitudeSearch2.TabIndex = 55
        '
        'txtFacilityLongitudeSearch1
        '
        Me.txtFacilityLongitudeSearch1.Location = New System.Drawing.Point(141, 134)
        Me.txtFacilityLongitudeSearch1.Name = "txtFacilityLongitudeSearch1"
        Me.txtFacilityLongitudeSearch1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityLongitudeSearch1.TabIndex = 54
        '
        'Panel15
        '
        Me.Panel15.AutoSize = True
        Me.Panel15.Controls.Add(Me.rdbFacilityZipCodeNotEqual)
        Me.Panel15.Controls.Add(Me.rdbFacilityZipCodeEqual)
        Me.Panel15.Location = New System.Drawing.Point(539, 78)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(76, 27)
        Me.Panel15.TabIndex = 172
        '
        'rdbFacilityZipCodeNotEqual
        '
        Me.rdbFacilityZipCodeNotEqual.AutoSize = True
        Me.rdbFacilityZipCodeNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbFacilityZipCodeNotEqual.Name = "rdbFacilityZipCodeNotEqual"
        Me.rdbFacilityZipCodeNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbFacilityZipCodeNotEqual.TabIndex = 46
        Me.rdbFacilityZipCodeNotEqual.Text = "<>"
        '
        'rdbFacilityZipCodeEqual
        '
        Me.rdbFacilityZipCodeEqual.AutoSize = True
        Me.rdbFacilityZipCodeEqual.Checked = True
        Me.rdbFacilityZipCodeEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbFacilityZipCodeEqual.Name = "rdbFacilityZipCodeEqual"
        Me.rdbFacilityZipCodeEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbFacilityZipCodeEqual.TabIndex = 45
        Me.rdbFacilityZipCodeEqual.TabStop = True
        Me.rdbFacilityZipCodeEqual.Text = "="
        '
        'Panel16
        '
        Me.Panel16.AutoSize = True
        Me.Panel16.Controls.Add(Me.rdbFacilityZipCodeOr)
        Me.Panel16.Controls.Add(Me.rdbFacilityZipCodeAnd)
        Me.Panel16.Location = New System.Drawing.Point(417, 81)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(99, 27)
        Me.Panel16.TabIndex = 171
        '
        'rdbFacilityZipCodeOr
        '
        Me.rdbFacilityZipCodeOr.AutoSize = True
        Me.rdbFacilityZipCodeOr.Checked = True
        Me.rdbFacilityZipCodeOr.Location = New System.Drawing.Point(55, 4)
        Me.rdbFacilityZipCodeOr.Name = "rdbFacilityZipCodeOr"
        Me.rdbFacilityZipCodeOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbFacilityZipCodeOr.TabIndex = 44
        Me.rdbFacilityZipCodeOr.TabStop = True
        Me.rdbFacilityZipCodeOr.Text = "OR"
        '
        'rdbFacilityZipCodeAnd
        '
        Me.rdbFacilityZipCodeAnd.AutoSize = True
        Me.rdbFacilityZipCodeAnd.Location = New System.Drawing.Point(7, 4)
        Me.rdbFacilityZipCodeAnd.Name = "rdbFacilityZipCodeAnd"
        Me.rdbFacilityZipCodeAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbFacilityZipCodeAnd.TabIndex = 43
        Me.rdbFacilityZipCodeAnd.Text = "AND"
        '
        'txtFacilityZipCodeSearch2
        '
        Me.txtFacilityZipCodeSearch2.Location = New System.Drawing.Point(281, 84)
        Me.txtFacilityZipCodeSearch2.Name = "txtFacilityZipCodeSearch2"
        Me.txtFacilityZipCodeSearch2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityZipCodeSearch2.TabIndex = 42
        '
        'txtFacilityZipCodeSearch1
        '
        Me.txtFacilityZipCodeSearch1.Location = New System.Drawing.Point(141, 84)
        Me.txtFacilityZipCodeSearch1.Name = "txtFacilityZipCodeSearch1"
        Me.txtFacilityZipCodeSearch1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityZipCodeSearch1.TabIndex = 41
        '
        'Panel13
        '
        Me.Panel13.AutoSize = True
        Me.Panel13.Controls.Add(Me.rdbFacilityCityNotEqual)
        Me.Panel13.Controls.Add(Me.rdbFacilityCityEqual)
        Me.Panel13.Location = New System.Drawing.Point(539, 53)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(76, 27)
        Me.Panel13.TabIndex = 170
        '
        'rdbFacilityCityNotEqual
        '
        Me.rdbFacilityCityNotEqual.AutoSize = True
        Me.rdbFacilityCityNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbFacilityCityNotEqual.Name = "rdbFacilityCityNotEqual"
        Me.rdbFacilityCityNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbFacilityCityNotEqual.TabIndex = 38
        Me.rdbFacilityCityNotEqual.Text = "<>"
        '
        'rdbFacilityCityEqual
        '
        Me.rdbFacilityCityEqual.AutoSize = True
        Me.rdbFacilityCityEqual.Checked = True
        Me.rdbFacilityCityEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbFacilityCityEqual.Name = "rdbFacilityCityEqual"
        Me.rdbFacilityCityEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbFacilityCityEqual.TabIndex = 37
        Me.rdbFacilityCityEqual.TabStop = True
        Me.rdbFacilityCityEqual.Text = "="
        '
        'Panel14
        '
        Me.Panel14.AutoSize = True
        Me.Panel14.Controls.Add(Me.rdbFacilityCityOr)
        Me.Panel14.Controls.Add(Me.rdbFacilityCityAnd)
        Me.Panel14.Location = New System.Drawing.Point(417, 56)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(99, 27)
        Me.Panel14.TabIndex = 169
        '
        'rdbFacilityCityOr
        '
        Me.rdbFacilityCityOr.AutoSize = True
        Me.rdbFacilityCityOr.Checked = True
        Me.rdbFacilityCityOr.Location = New System.Drawing.Point(55, 4)
        Me.rdbFacilityCityOr.Name = "rdbFacilityCityOr"
        Me.rdbFacilityCityOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbFacilityCityOr.TabIndex = 36
        Me.rdbFacilityCityOr.TabStop = True
        Me.rdbFacilityCityOr.Text = "OR"
        '
        'rdbFacilityCityAnd
        '
        Me.rdbFacilityCityAnd.AutoSize = True
        Me.rdbFacilityCityAnd.Location = New System.Drawing.Point(7, 4)
        Me.rdbFacilityCityAnd.Name = "rdbFacilityCityAnd"
        Me.rdbFacilityCityAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbFacilityCityAnd.TabIndex = 35
        Me.rdbFacilityCityAnd.Text = "AND"
        '
        'txtFacilityCitySearch2
        '
        Me.txtFacilityCitySearch2.Location = New System.Drawing.Point(281, 59)
        Me.txtFacilityCitySearch2.Name = "txtFacilityCitySearch2"
        Me.txtFacilityCitySearch2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityCitySearch2.TabIndex = 34
        '
        'txtFacilityCitySearch1
        '
        Me.txtFacilityCitySearch1.Location = New System.Drawing.Point(141, 59)
        Me.txtFacilityCitySearch1.Name = "txtFacilityCitySearch1"
        Me.txtFacilityCitySearch1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityCitySearch1.TabIndex = 33
        '
        'Panel7
        '
        Me.Panel7.AutoSize = True
        Me.Panel7.Controls.Add(Me.rdbFacilityStreet2NotEqual)
        Me.Panel7.Controls.Add(Me.rdbFacilityStreet2Equal)
        Me.Panel7.Location = New System.Drawing.Point(539, 28)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(76, 27)
        Me.Panel7.TabIndex = 168
        '
        'rdbFacilityStreet2NotEqual
        '
        Me.rdbFacilityStreet2NotEqual.AutoSize = True
        Me.rdbFacilityStreet2NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbFacilityStreet2NotEqual.Name = "rdbFacilityStreet2NotEqual"
        Me.rdbFacilityStreet2NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbFacilityStreet2NotEqual.TabIndex = 31
        Me.rdbFacilityStreet2NotEqual.Text = "<>"
        '
        'rdbFacilityStreet2Equal
        '
        Me.rdbFacilityStreet2Equal.AutoSize = True
        Me.rdbFacilityStreet2Equal.Checked = True
        Me.rdbFacilityStreet2Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbFacilityStreet2Equal.Name = "rdbFacilityStreet2Equal"
        Me.rdbFacilityStreet2Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbFacilityStreet2Equal.TabIndex = 30
        Me.rdbFacilityStreet2Equal.TabStop = True
        Me.rdbFacilityStreet2Equal.Text = "="
        '
        'Panel8
        '
        Me.Panel8.AutoSize = True
        Me.Panel8.Controls.Add(Me.rdbFacilityStreet2Or)
        Me.Panel8.Controls.Add(Me.rdbFacilityStreet2And)
        Me.Panel8.Location = New System.Drawing.Point(417, 31)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(99, 27)
        Me.Panel8.TabIndex = 167
        '
        'rdbFacilityStreet2Or
        '
        Me.rdbFacilityStreet2Or.AutoSize = True
        Me.rdbFacilityStreet2Or.Checked = True
        Me.rdbFacilityStreet2Or.Location = New System.Drawing.Point(55, 4)
        Me.rdbFacilityStreet2Or.Name = "rdbFacilityStreet2Or"
        Me.rdbFacilityStreet2Or.Size = New System.Drawing.Size(41, 17)
        Me.rdbFacilityStreet2Or.TabIndex = 29
        Me.rdbFacilityStreet2Or.TabStop = True
        Me.rdbFacilityStreet2Or.Text = "OR"
        '
        'rdbFacilityStreet2And
        '
        Me.rdbFacilityStreet2And.AutoSize = True
        Me.rdbFacilityStreet2And.Location = New System.Drawing.Point(7, 4)
        Me.rdbFacilityStreet2And.Name = "rdbFacilityStreet2And"
        Me.rdbFacilityStreet2And.Size = New System.Drawing.Size(48, 17)
        Me.rdbFacilityStreet2And.TabIndex = 28
        Me.rdbFacilityStreet2And.Text = "AND"
        '
        'txtFacilityStreet2Search2
        '
        Me.txtFacilityStreet2Search2.Location = New System.Drawing.Point(281, 34)
        Me.txtFacilityStreet2Search2.Name = "txtFacilityStreet2Search2"
        Me.txtFacilityStreet2Search2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityStreet2Search2.TabIndex = 27
        '
        'txtFacilityStreet2Search1
        '
        Me.txtFacilityStreet2Search1.Location = New System.Drawing.Point(141, 34)
        Me.txtFacilityStreet2Search1.Name = "txtFacilityStreet2Search1"
        Me.txtFacilityStreet2Search1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityStreet2Search1.TabIndex = 26
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.Controls.Add(Me.rdbFacilityStreet1NotEqual)
        Me.Panel4.Controls.Add(Me.rdbFacilityStreet1Equal)
        Me.Panel4.Location = New System.Drawing.Point(539, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(76, 27)
        Me.Panel4.TabIndex = 166
        '
        'rdbFacilityStreet1NotEqual
        '
        Me.rdbFacilityStreet1NotEqual.AutoSize = True
        Me.rdbFacilityStreet1NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbFacilityStreet1NotEqual.Name = "rdbFacilityStreet1NotEqual"
        Me.rdbFacilityStreet1NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbFacilityStreet1NotEqual.TabIndex = 23
        Me.rdbFacilityStreet1NotEqual.Text = "<>"
        '
        'rdbFacilityStreet1Equal
        '
        Me.rdbFacilityStreet1Equal.AutoSize = True
        Me.rdbFacilityStreet1Equal.Checked = True
        Me.rdbFacilityStreet1Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbFacilityStreet1Equal.Name = "rdbFacilityStreet1Equal"
        Me.rdbFacilityStreet1Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbFacilityStreet1Equal.TabIndex = 22
        Me.rdbFacilityStreet1Equal.TabStop = True
        Me.rdbFacilityStreet1Equal.Text = "="
        '
        'Panel5
        '
        Me.Panel5.AutoSize = True
        Me.Panel5.Controls.Add(Me.rdbFacilityStreet1Or)
        Me.Panel5.Controls.Add(Me.rdbFacilityStreet1And)
        Me.Panel5.Location = New System.Drawing.Point(417, 6)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(99, 27)
        Me.Panel5.TabIndex = 165
        '
        'rdbFacilityStreet1Or
        '
        Me.rdbFacilityStreet1Or.AutoSize = True
        Me.rdbFacilityStreet1Or.Checked = True
        Me.rdbFacilityStreet1Or.Location = New System.Drawing.Point(55, 4)
        Me.rdbFacilityStreet1Or.Name = "rdbFacilityStreet1Or"
        Me.rdbFacilityStreet1Or.Size = New System.Drawing.Size(41, 17)
        Me.rdbFacilityStreet1Or.TabIndex = 21
        Me.rdbFacilityStreet1Or.TabStop = True
        Me.rdbFacilityStreet1Or.Text = "OR"
        '
        'rdbFacilityStreet1And
        '
        Me.rdbFacilityStreet1And.AutoSize = True
        Me.rdbFacilityStreet1And.Location = New System.Drawing.Point(7, 4)
        Me.rdbFacilityStreet1And.Name = "rdbFacilityStreet1And"
        Me.rdbFacilityStreet1And.Size = New System.Drawing.Size(48, 17)
        Me.rdbFacilityStreet1And.TabIndex = 20
        Me.rdbFacilityStreet1And.Text = "AND"
        '
        'txtFacilityStreet1Search2
        '
        Me.txtFacilityStreet1Search2.Location = New System.Drawing.Point(281, 9)
        Me.txtFacilityStreet1Search2.Name = "txtFacilityStreet1Search2"
        Me.txtFacilityStreet1Search2.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityStreet1Search2.TabIndex = 19
        '
        'txtFacilityStreet1Search1
        '
        Me.txtFacilityStreet1Search1.Location = New System.Drawing.Point(141, 9)
        Me.txtFacilityStreet1Search1.Name = "txtFacilityStreet1Search1"
        Me.txtFacilityStreet1Search1.Size = New System.Drawing.Size(127, 20)
        Me.txtFacilityStreet1Search1.TabIndex = 18
        '
        'chbFacilityLatitude
        '
        Me.chbFacilityLatitude.AutoSize = True
        Me.chbFacilityLatitude.Location = New System.Drawing.Point(8, 111)
        Me.chbFacilityLatitude.Name = "chbFacilityLatitude"
        Me.chbFacilityLatitude.Size = New System.Drawing.Size(81, 17)
        Me.chbFacilityLatitude.TabIndex = 48
        Me.chbFacilityLatitude.Text = "Latitude (N)"
        '
        'chbFacilityLongitude
        '
        Me.chbFacilityLongitude.AutoSize = True
        Me.chbFacilityLongitude.Location = New System.Drawing.Point(8, 136)
        Me.chbFacilityLongitude.Name = "chbFacilityLongitude"
        Me.chbFacilityLongitude.Size = New System.Drawing.Size(93, 17)
        Me.chbFacilityLongitude.TabIndex = 53
        Me.chbFacilityLongitude.Text = "Longitude (W)"
        '
        'chbFacilityZipCode
        '
        Me.chbFacilityZipCode.AutoSize = True
        Me.chbFacilityZipCode.Location = New System.Drawing.Point(8, 86)
        Me.chbFacilityZipCode.Name = "chbFacilityZipCode"
        Me.chbFacilityZipCode.Size = New System.Drawing.Size(69, 17)
        Me.chbFacilityZipCode.TabIndex = 40
        Me.chbFacilityZipCode.Text = "Zip Code"
        '
        'chbFacilityCity
        '
        Me.chbFacilityCity.AutoSize = True
        Me.chbFacilityCity.Location = New System.Drawing.Point(8, 61)
        Me.chbFacilityCity.Name = "chbFacilityCity"
        Me.chbFacilityCity.Size = New System.Drawing.Size(43, 17)
        Me.chbFacilityCity.TabIndex = 32
        Me.chbFacilityCity.Text = "City"
        '
        'chbFacilityStreet2
        '
        Me.chbFacilityStreet2.AutoSize = True
        Me.chbFacilityStreet2.Location = New System.Drawing.Point(8, 36)
        Me.chbFacilityStreet2.Name = "chbFacilityStreet2"
        Me.chbFacilityStreet2.Size = New System.Drawing.Size(104, 17)
        Me.chbFacilityStreet2.TabIndex = 25
        Me.chbFacilityStreet2.Text = "Street Address 2"
        '
        'chbFacilityStreet1
        '
        Me.chbFacilityStreet1.AutoSize = True
        Me.chbFacilityStreet1.Location = New System.Drawing.Point(8, 11)
        Me.chbFacilityStreet1.Name = "chbFacilityStreet1"
        Me.chbFacilityStreet1.Size = New System.Drawing.Size(104, 17)
        Me.chbFacilityStreet1.TabIndex = 17
        Me.chbFacilityStreet1.Text = "Street Address 1"
        '
        'TPHeaderInformation
        '
        Me.TPHeaderInformation.Controls.Add(Me.Panel74)
        Me.TPHeaderInformation.Controls.Add(Me.Panel75)
        Me.TPHeaderInformation.Controls.Add(Me.txtNAICSCodeSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.txtNAICSCodeSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.chbNAICSCode)
        Me.TPHeaderInformation.Controls.Add(Me.cboOperationStatusSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.cboOperationStatusSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.Panel12)
        Me.TPHeaderInformation.Controls.Add(Me.chbOperationStatus)
        Me.TPHeaderInformation.Controls.Add(Me.cboCMSUniverseSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.cboCMSUniverseSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.Panel33)
        Me.TPHeaderInformation.Controls.Add(Me.Panel34)
        Me.TPHeaderInformation.Controls.Add(Me.txtPlantDescriptionSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.txtPlantDescriptionSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.chbPlantDescription)
        Me.TPHeaderInformation.Controls.Add(Me.Panel31)
        Me.TPHeaderInformation.Controls.Add(Me.chbCMSUniverse)
        Me.TPHeaderInformation.Controls.Add(Me.DTPShutDownDateSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.DTPShutDownDateSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.DTPStartUpDateSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.DTPStartUpDateSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.Panel21)
        Me.TPHeaderInformation.Controls.Add(Me.Panel22)
        Me.TPHeaderInformation.Controls.Add(Me.txtSICCodeSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.txtSICCodeSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.cboClassificationSearch2)
        Me.TPHeaderInformation.Controls.Add(Me.cboClassificationSearch1)
        Me.TPHeaderInformation.Controls.Add(Me.Panel19)
        Me.TPHeaderInformation.Controls.Add(Me.chbShutDownDate)
        Me.TPHeaderInformation.Controls.Add(Me.chbStartUpDate)
        Me.TPHeaderInformation.Controls.Add(Me.chbSICCode)
        Me.TPHeaderInformation.Controls.Add(Me.chbClassification)
        Me.TPHeaderInformation.Location = New System.Drawing.Point(4, 22)
        Me.TPHeaderInformation.Name = "TPHeaderInformation"
        Me.TPHeaderInformation.Padding = New System.Windows.Forms.Padding(3)
        Me.TPHeaderInformation.Size = New System.Drawing.Size(731, 363)
        Me.TPHeaderInformation.TabIndex = 1
        Me.TPHeaderInformation.Text = "Header Information"
        Me.TPHeaderInformation.UseVisualStyleBackColor = True
        '
        'Panel74
        '
        Me.Panel74.AutoSize = True
        Me.Panel74.Controls.Add(Me.rdbNAICSCodeNotEqual)
        Me.Panel74.Controls.Add(Me.rdbNAICSCodeEqual)
        Me.Panel74.Location = New System.Drawing.Point(529, 84)
        Me.Panel74.Name = "Panel74"
        Me.Panel74.Size = New System.Drawing.Size(77, 27)
        Me.Panel74.TabIndex = 239
        '
        'rdbNAICSCodeNotEqual
        '
        Me.rdbNAICSCodeNotEqual.AutoSize = True
        Me.rdbNAICSCodeNotEqual.Location = New System.Drawing.Point(37, 0)
        Me.rdbNAICSCodeNotEqual.Name = "rdbNAICSCodeNotEqual"
        Me.rdbNAICSCodeNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbNAICSCodeNotEqual.TabIndex = 96
        Me.rdbNAICSCodeNotEqual.Text = "<>"
        '
        'rdbNAICSCodeEqual
        '
        Me.rdbNAICSCodeEqual.AutoSize = True
        Me.rdbNAICSCodeEqual.Checked = True
        Me.rdbNAICSCodeEqual.Location = New System.Drawing.Point(7, 0)
        Me.rdbNAICSCodeEqual.Name = "rdbNAICSCodeEqual"
        Me.rdbNAICSCodeEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbNAICSCodeEqual.TabIndex = 95
        Me.rdbNAICSCodeEqual.TabStop = True
        Me.rdbNAICSCodeEqual.Text = "="
        '
        'Panel75
        '
        Me.Panel75.AutoSize = True
        Me.Panel75.Controls.Add(Me.rdbNAICSCodeOr)
        Me.Panel75.Controls.Add(Me.rdbNAICSCodeAnd)
        Me.Panel75.Location = New System.Drawing.Point(421, 80)
        Me.Panel75.Name = "Panel75"
        Me.Panel75.Size = New System.Drawing.Size(101, 27)
        Me.Panel75.TabIndex = 238
        '
        'rdbNAICSCodeOr
        '
        Me.rdbNAICSCodeOr.AutoSize = True
        Me.rdbNAICSCodeOr.Checked = True
        Me.rdbNAICSCodeOr.Location = New System.Drawing.Point(57, 4)
        Me.rdbNAICSCodeOr.Name = "rdbNAICSCodeOr"
        Me.rdbNAICSCodeOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbNAICSCodeOr.TabIndex = 94
        Me.rdbNAICSCodeOr.TabStop = True
        Me.rdbNAICSCodeOr.Text = "OR"
        '
        'rdbNAICSCodeAnd
        '
        Me.rdbNAICSCodeAnd.AutoSize = True
        Me.rdbNAICSCodeAnd.Location = New System.Drawing.Point(7, 4)
        Me.rdbNAICSCodeAnd.Name = "rdbNAICSCodeAnd"
        Me.rdbNAICSCodeAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbNAICSCodeAnd.TabIndex = 93
        Me.rdbNAICSCodeAnd.Text = "AND"
        '
        'txtNAICSCodeSearch2
        '
        Me.txtNAICSCodeSearch2.Location = New System.Drawing.Point(283, 83)
        Me.txtNAICSCodeSearch2.Name = "txtNAICSCodeSearch2"
        Me.txtNAICSCodeSearch2.Size = New System.Drawing.Size(132, 20)
        Me.txtNAICSCodeSearch2.TabIndex = 236
        '
        'txtNAICSCodeSearch1
        '
        Me.txtNAICSCodeSearch1.Location = New System.Drawing.Point(145, 83)
        Me.txtNAICSCodeSearch1.Name = "txtNAICSCodeSearch1"
        Me.txtNAICSCodeSearch1.Size = New System.Drawing.Size(132, 20)
        Me.txtNAICSCodeSearch1.TabIndex = 235
        '
        'chbNAICSCode
        '
        Me.chbNAICSCode.AutoSize = True
        Me.chbNAICSCode.Location = New System.Drawing.Point(6, 85)
        Me.chbNAICSCode.Name = "chbNAICSCode"
        Me.chbNAICSCode.Size = New System.Drawing.Size(86, 17)
        Me.chbNAICSCode.TabIndex = 234
        Me.chbNAICSCode.Text = "NAICS Code"
        '
        'cboOperationStatusSearch2
        '
        Me.cboOperationStatusSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperationStatusSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperationStatusSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperationStatusSearch2.Location = New System.Drawing.Point(283, 10)
        Me.cboOperationStatusSearch2.Name = "cboOperationStatusSearch2"
        Me.cboOperationStatusSearch2.Size = New System.Drawing.Size(132, 21)
        Me.cboOperationStatusSearch2.TabIndex = 76
        '
        'cboOperationStatusSearch1
        '
        Me.cboOperationStatusSearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperationStatusSearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperationStatusSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperationStatusSearch1.Location = New System.Drawing.Point(145, 10)
        Me.cboOperationStatusSearch1.Name = "cboOperationStatusSearch1"
        Me.cboOperationStatusSearch1.Size = New System.Drawing.Size(132, 21)
        Me.cboOperationStatusSearch1.TabIndex = 75
        '
        'Panel12
        '
        Me.Panel12.AutoSize = True
        Me.Panel12.Controls.Add(Me.rdbOperationStatusNotEqual)
        Me.Panel12.Controls.Add(Me.rdbOperationStatusEqual)
        Me.Panel12.Location = New System.Drawing.Point(529, 9)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(77, 27)
        Me.Panel12.TabIndex = 233
        '
        'rdbOperationStatusNotEqual
        '
        Me.rdbOperationStatusNotEqual.AutoSize = True
        Me.rdbOperationStatusNotEqual.Location = New System.Drawing.Point(37, 2)
        Me.rdbOperationStatusNotEqual.Name = "rdbOperationStatusNotEqual"
        Me.rdbOperationStatusNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbOperationStatusNotEqual.TabIndex = 80
        Me.rdbOperationStatusNotEqual.Text = "<>"
        '
        'rdbOperationStatusEqual
        '
        Me.rdbOperationStatusEqual.AutoSize = True
        Me.rdbOperationStatusEqual.Checked = True
        Me.rdbOperationStatusEqual.Location = New System.Drawing.Point(7, 2)
        Me.rdbOperationStatusEqual.Name = "rdbOperationStatusEqual"
        Me.rdbOperationStatusEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbOperationStatusEqual.TabIndex = 79
        Me.rdbOperationStatusEqual.TabStop = True
        Me.rdbOperationStatusEqual.Text = "="
        '
        'chbOperationStatus
        '
        Me.chbOperationStatus.AutoSize = True
        Me.chbOperationStatus.Location = New System.Drawing.Point(6, 12)
        Me.chbOperationStatus.Name = "chbOperationStatus"
        Me.chbOperationStatus.Size = New System.Drawing.Size(105, 17)
        Me.chbOperationStatus.TabIndex = 74
        Me.chbOperationStatus.Text = "Operation Status"
        '
        'cboCMSUniverseSearch2
        '
        Me.cboCMSUniverseSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCMSUniverseSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCMSUniverseSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCMSUniverseSearch2.Location = New System.Drawing.Point(283, 157)
        Me.cboCMSUniverseSearch2.Name = "cboCMSUniverseSearch2"
        Me.cboCMSUniverseSearch2.Size = New System.Drawing.Size(132, 21)
        Me.cboCMSUniverseSearch2.TabIndex = 110
        '
        'cboCMSUniverseSearch1
        '
        Me.cboCMSUniverseSearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCMSUniverseSearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCMSUniverseSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCMSUniverseSearch1.Location = New System.Drawing.Point(145, 157)
        Me.cboCMSUniverseSearch1.Name = "cboCMSUniverseSearch1"
        Me.cboCMSUniverseSearch1.Size = New System.Drawing.Size(132, 21)
        Me.cboCMSUniverseSearch1.TabIndex = 109
        '
        'Panel33
        '
        Me.Panel33.AutoSize = True
        Me.Panel33.Controls.Add(Me.rdbPlantDescriptionNotEqual)
        Me.Panel33.Controls.Add(Me.rdbPlantDescriptionEqual)
        Me.Panel33.Location = New System.Drawing.Point(529, 179)
        Me.Panel33.Name = "Panel33"
        Me.Panel33.Size = New System.Drawing.Size(77, 27)
        Me.Panel33.TabIndex = 227
        '
        'rdbPlantDescriptionNotEqual
        '
        Me.rdbPlantDescriptionNotEqual.AutoSize = True
        Me.rdbPlantDescriptionNotEqual.Location = New System.Drawing.Point(37, 4)
        Me.rdbPlantDescriptionNotEqual.Name = "rdbPlantDescriptionNotEqual"
        Me.rdbPlantDescriptionNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbPlantDescriptionNotEqual.TabIndex = 122
        Me.rdbPlantDescriptionNotEqual.Text = "<>"
        '
        'rdbPlantDescriptionEqual
        '
        Me.rdbPlantDescriptionEqual.AutoSize = True
        Me.rdbPlantDescriptionEqual.Checked = True
        Me.rdbPlantDescriptionEqual.Location = New System.Drawing.Point(7, 4)
        Me.rdbPlantDescriptionEqual.Name = "rdbPlantDescriptionEqual"
        Me.rdbPlantDescriptionEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbPlantDescriptionEqual.TabIndex = 121
        Me.rdbPlantDescriptionEqual.TabStop = True
        Me.rdbPlantDescriptionEqual.Text = "="
        '
        'Panel34
        '
        Me.Panel34.AutoSize = True
        Me.Panel34.Controls.Add(Me.rdbPlantDescriptionOR)
        Me.Panel34.Controls.Add(Me.rdbPlantDescriptionAND)
        Me.Panel34.Location = New System.Drawing.Point(421, 179)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Size = New System.Drawing.Size(101, 27)
        Me.Panel34.TabIndex = 226
        '
        'rdbPlantDescriptionOR
        '
        Me.rdbPlantDescriptionOR.AutoSize = True
        Me.rdbPlantDescriptionOR.Checked = True
        Me.rdbPlantDescriptionOR.Location = New System.Drawing.Point(57, 4)
        Me.rdbPlantDescriptionOR.Name = "rdbPlantDescriptionOR"
        Me.rdbPlantDescriptionOR.Size = New System.Drawing.Size(41, 17)
        Me.rdbPlantDescriptionOR.TabIndex = 120
        Me.rdbPlantDescriptionOR.TabStop = True
        Me.rdbPlantDescriptionOR.Text = "OR"
        '
        'rdbPlantDescriptionAND
        '
        Me.rdbPlantDescriptionAND.AutoSize = True
        Me.rdbPlantDescriptionAND.Location = New System.Drawing.Point(7, 4)
        Me.rdbPlantDescriptionAND.Name = "rdbPlantDescriptionAND"
        Me.rdbPlantDescriptionAND.Size = New System.Drawing.Size(48, 17)
        Me.rdbPlantDescriptionAND.TabIndex = 119
        Me.rdbPlantDescriptionAND.Text = "AND"
        '
        'txtPlantDescriptionSearch2
        '
        Me.txtPlantDescriptionSearch2.Location = New System.Drawing.Point(283, 182)
        Me.txtPlantDescriptionSearch2.Name = "txtPlantDescriptionSearch2"
        Me.txtPlantDescriptionSearch2.Size = New System.Drawing.Size(132, 20)
        Me.txtPlantDescriptionSearch2.TabIndex = 118
        '
        'txtPlantDescriptionSearch1
        '
        Me.txtPlantDescriptionSearch1.Location = New System.Drawing.Point(145, 182)
        Me.txtPlantDescriptionSearch1.Name = "txtPlantDescriptionSearch1"
        Me.txtPlantDescriptionSearch1.Size = New System.Drawing.Size(132, 20)
        Me.txtPlantDescriptionSearch1.TabIndex = 117
        '
        'chbPlantDescription
        '
        Me.chbPlantDescription.AutoSize = True
        Me.chbPlantDescription.Location = New System.Drawing.Point(6, 184)
        Me.chbPlantDescription.Name = "chbPlantDescription"
        Me.chbPlantDescription.Size = New System.Drawing.Size(106, 17)
        Me.chbPlantDescription.TabIndex = 116
        Me.chbPlantDescription.Text = "Plant Description"
        '
        'Panel31
        '
        Me.Panel31.AutoSize = True
        Me.Panel31.Controls.Add(Me.rdbCMSUniverseNotEqual)
        Me.Panel31.Controls.Add(Me.rdbCMSUniverseEqual)
        Me.Panel31.Location = New System.Drawing.Point(529, 154)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(77, 27)
        Me.Panel31.TabIndex = 225
        '
        'rdbCMSUniverseNotEqual
        '
        Me.rdbCMSUniverseNotEqual.AutoSize = True
        Me.rdbCMSUniverseNotEqual.Location = New System.Drawing.Point(37, 4)
        Me.rdbCMSUniverseNotEqual.Name = "rdbCMSUniverseNotEqual"
        Me.rdbCMSUniverseNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbCMSUniverseNotEqual.TabIndex = 114
        Me.rdbCMSUniverseNotEqual.Text = "<>"
        '
        'rdbCMSUniverseEqual
        '
        Me.rdbCMSUniverseEqual.AutoSize = True
        Me.rdbCMSUniverseEqual.Checked = True
        Me.rdbCMSUniverseEqual.Location = New System.Drawing.Point(7, 4)
        Me.rdbCMSUniverseEqual.Name = "rdbCMSUniverseEqual"
        Me.rdbCMSUniverseEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbCMSUniverseEqual.TabIndex = 113
        Me.rdbCMSUniverseEqual.TabStop = True
        Me.rdbCMSUniverseEqual.Text = "="
        '
        'chbCMSUniverse
        '
        Me.chbCMSUniverse.AutoSize = True
        Me.chbCMSUniverse.Location = New System.Drawing.Point(6, 159)
        Me.chbCMSUniverse.Name = "chbCMSUniverse"
        Me.chbCMSUniverse.Size = New System.Drawing.Size(94, 17)
        Me.chbCMSUniverse.TabIndex = 108
        Me.chbCMSUniverse.Text = "CMS Universe"
        '
        'DTPShutDownDateSearch2
        '
        Me.DTPShutDownDateSearch2.CustomFormat = "dd-MMM-yyyy"
        Me.DTPShutDownDateSearch2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPShutDownDateSearch2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPShutDownDateSearch2.Location = New System.Drawing.Point(283, 131)
        Me.DTPShutDownDateSearch2.Name = "DTPShutDownDateSearch2"
        Me.DTPShutDownDateSearch2.ShowCheckBox = True
        Me.DTPShutDownDateSearch2.Size = New System.Drawing.Size(132, 22)
        Me.DTPShutDownDateSearch2.TabIndex = 105
        Me.DTPShutDownDateSearch2.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPShutDownDateSearch1
        '
        Me.DTPShutDownDateSearch1.CustomFormat = "dd-MMM-yyyy"
        Me.DTPShutDownDateSearch1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPShutDownDateSearch1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPShutDownDateSearch1.Location = New System.Drawing.Point(145, 131)
        Me.DTPShutDownDateSearch1.Name = "DTPShutDownDateSearch1"
        Me.DTPShutDownDateSearch1.ShowCheckBox = True
        Me.DTPShutDownDateSearch1.Size = New System.Drawing.Size(132, 22)
        Me.DTPShutDownDateSearch1.TabIndex = 104
        Me.DTPShutDownDateSearch1.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPStartUpDateSearch2
        '
        Me.DTPStartUpDateSearch2.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartUpDateSearch2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPStartUpDateSearch2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartUpDateSearch2.Location = New System.Drawing.Point(283, 106)
        Me.DTPStartUpDateSearch2.Name = "DTPStartUpDateSearch2"
        Me.DTPStartUpDateSearch2.ShowCheckBox = True
        Me.DTPStartUpDateSearch2.Size = New System.Drawing.Size(132, 22)
        Me.DTPStartUpDateSearch2.TabIndex = 100
        Me.DTPStartUpDateSearch2.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPStartUpDateSearch1
        '
        Me.DTPStartUpDateSearch1.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartUpDateSearch1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPStartUpDateSearch1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartUpDateSearch1.Location = New System.Drawing.Point(145, 106)
        Me.DTPStartUpDateSearch1.Name = "DTPStartUpDateSearch1"
        Me.DTPStartUpDateSearch1.ShowCheckBox = True
        Me.DTPStartUpDateSearch1.Size = New System.Drawing.Size(132, 22)
        Me.DTPStartUpDateSearch1.TabIndex = 99
        Me.DTPStartUpDateSearch1.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Panel21
        '
        Me.Panel21.AutoSize = True
        Me.Panel21.Controls.Add(Me.rdbSICCodeNotEqual)
        Me.Panel21.Controls.Add(Me.rdbSICCodeEqual)
        Me.Panel21.Location = New System.Drawing.Point(529, 57)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(77, 27)
        Me.Panel21.TabIndex = 217
        '
        'rdbSICCodeNotEqual
        '
        Me.rdbSICCodeNotEqual.AutoSize = True
        Me.rdbSICCodeNotEqual.Location = New System.Drawing.Point(37, 4)
        Me.rdbSICCodeNotEqual.Name = "rdbSICCodeNotEqual"
        Me.rdbSICCodeNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbSICCodeNotEqual.TabIndex = 96
        Me.rdbSICCodeNotEqual.Text = "<>"
        '
        'rdbSICCodeEqual
        '
        Me.rdbSICCodeEqual.AutoSize = True
        Me.rdbSICCodeEqual.Checked = True
        Me.rdbSICCodeEqual.Location = New System.Drawing.Point(7, 4)
        Me.rdbSICCodeEqual.Name = "rdbSICCodeEqual"
        Me.rdbSICCodeEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbSICCodeEqual.TabIndex = 95
        Me.rdbSICCodeEqual.TabStop = True
        Me.rdbSICCodeEqual.Text = "="
        '
        'Panel22
        '
        Me.Panel22.AutoSize = True
        Me.Panel22.Controls.Add(Me.rdbSICCodeOr)
        Me.Panel22.Controls.Add(Me.rdbSICCodeAnd)
        Me.Panel22.Location = New System.Drawing.Point(421, 57)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(101, 27)
        Me.Panel22.TabIndex = 216
        '
        'rdbSICCodeOr
        '
        Me.rdbSICCodeOr.AutoSize = True
        Me.rdbSICCodeOr.Checked = True
        Me.rdbSICCodeOr.Location = New System.Drawing.Point(57, 4)
        Me.rdbSICCodeOr.Name = "rdbSICCodeOr"
        Me.rdbSICCodeOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbSICCodeOr.TabIndex = 94
        Me.rdbSICCodeOr.TabStop = True
        Me.rdbSICCodeOr.Text = "OR"
        '
        'rdbSICCodeAnd
        '
        Me.rdbSICCodeAnd.AutoSize = True
        Me.rdbSICCodeAnd.Location = New System.Drawing.Point(7, 4)
        Me.rdbSICCodeAnd.Name = "rdbSICCodeAnd"
        Me.rdbSICCodeAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbSICCodeAnd.TabIndex = 93
        Me.rdbSICCodeAnd.Text = "AND"
        '
        'txtSICCodeSearch2
        '
        Me.txtSICCodeSearch2.Location = New System.Drawing.Point(283, 60)
        Me.txtSICCodeSearch2.Name = "txtSICCodeSearch2"
        Me.txtSICCodeSearch2.Size = New System.Drawing.Size(132, 20)
        Me.txtSICCodeSearch2.TabIndex = 92
        '
        'txtSICCodeSearch1
        '
        Me.txtSICCodeSearch1.Location = New System.Drawing.Point(145, 60)
        Me.txtSICCodeSearch1.Name = "txtSICCodeSearch1"
        Me.txtSICCodeSearch1.Size = New System.Drawing.Size(132, 20)
        Me.txtSICCodeSearch1.TabIndex = 91
        '
        'cboClassificationSearch2
        '
        Me.cboClassificationSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboClassificationSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClassificationSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClassificationSearch2.Location = New System.Drawing.Point(283, 35)
        Me.cboClassificationSearch2.Name = "cboClassificationSearch2"
        Me.cboClassificationSearch2.Size = New System.Drawing.Size(132, 21)
        Me.cboClassificationSearch2.TabIndex = 84
        '
        'cboClassificationSearch1
        '
        Me.cboClassificationSearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboClassificationSearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClassificationSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClassificationSearch1.Location = New System.Drawing.Point(145, 35)
        Me.cboClassificationSearch1.Name = "cboClassificationSearch1"
        Me.cboClassificationSearch1.Size = New System.Drawing.Size(132, 21)
        Me.cboClassificationSearch1.TabIndex = 83
        '
        'Panel19
        '
        Me.Panel19.AutoSize = True
        Me.Panel19.Controls.Add(Me.rdbClassificationNotEqual)
        Me.Panel19.Controls.Add(Me.rdbClassificationEqual)
        Me.Panel19.Location = New System.Drawing.Point(529, 32)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(77, 27)
        Me.Panel19.TabIndex = 215
        '
        'rdbClassificationNotEqual
        '
        Me.rdbClassificationNotEqual.AutoSize = True
        Me.rdbClassificationNotEqual.Location = New System.Drawing.Point(37, 4)
        Me.rdbClassificationNotEqual.Name = "rdbClassificationNotEqual"
        Me.rdbClassificationNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbClassificationNotEqual.TabIndex = 88
        Me.rdbClassificationNotEqual.Text = "<>"
        '
        'rdbClassificationEqual
        '
        Me.rdbClassificationEqual.AutoSize = True
        Me.rdbClassificationEqual.Checked = True
        Me.rdbClassificationEqual.Location = New System.Drawing.Point(7, 4)
        Me.rdbClassificationEqual.Name = "rdbClassificationEqual"
        Me.rdbClassificationEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbClassificationEqual.TabIndex = 87
        Me.rdbClassificationEqual.TabStop = True
        Me.rdbClassificationEqual.Text = "="
        '
        'chbShutDownDate
        '
        Me.chbShutDownDate.AutoSize = True
        Me.chbShutDownDate.Location = New System.Drawing.Point(6, 134)
        Me.chbShutDownDate.Name = "chbShutDownDate"
        Me.chbShutDownDate.Size = New System.Drawing.Size(139, 17)
        Me.chbShutDownDate.TabIndex = 103
        Me.chbShutDownDate.Text = "Permit Revocation Date"
        '
        'chbStartUpDate
        '
        Me.chbStartUpDate.AutoSize = True
        Me.chbStartUpDate.Location = New System.Drawing.Point(6, 109)
        Me.chbStartUpDate.Name = "chbStartUpDate"
        Me.chbStartUpDate.Size = New System.Drawing.Size(86, 17)
        Me.chbStartUpDate.TabIndex = 98
        Me.chbStartUpDate.Text = "Startup Date"
        '
        'chbSICCode
        '
        Me.chbSICCode.AutoSize = True
        Me.chbSICCode.Location = New System.Drawing.Point(6, 62)
        Me.chbSICCode.Name = "chbSICCode"
        Me.chbSICCode.Size = New System.Drawing.Size(71, 17)
        Me.chbSICCode.TabIndex = 90
        Me.chbSICCode.Text = "SIC Code"
        '
        'chbClassification
        '
        Me.chbClassification.AutoSize = True
        Me.chbClassification.Location = New System.Drawing.Point(6, 37)
        Me.chbClassification.Name = "chbClassification"
        Me.chbClassification.Size = New System.Drawing.Size(87, 17)
        Me.chbClassification.TabIndex = 82
        Me.chbClassification.Text = "Classification"
        '
        'TPHeaderInformation2
        '
        Me.TPHeaderInformation2.Controls.Add(Me.chbStateProgramCodes)
        Me.TPHeaderInformation2.Controls.Add(Me.chbAttainmentStatus)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel63)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel62)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel61)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel60)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel59)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel58)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel57)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel56)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel55)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel54)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel53)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel52)
        Me.TPHeaderInformation2.Controls.Add(Me.chbHAPMajor)
        Me.TPHeaderInformation2.Controls.Add(Me.chbNSRPSDMajor)
        Me.TPHeaderInformation2.Controls.Add(Me.Panel51)
        Me.TPHeaderInformation2.Controls.Add(Me.chbPMNo)
        Me.TPHeaderInformation2.Controls.Add(Me.chbPMMacon)
        Me.TPHeaderInformation2.Controls.Add(Me.chbPMFloyd)
        Me.TPHeaderInformation2.Controls.Add(Me.chbPMChattanooga)
        Me.TPHeaderInformation2.Controls.Add(Me.chbPMAtlanta)
        Me.TPHeaderInformation2.Controls.Add(Me.chb8HrNo)
        Me.TPHeaderInformation2.Controls.Add(Me.chb8HrMacon)
        Me.TPHeaderInformation2.Controls.Add(Me.chb8HrAtlanta)
        Me.TPHeaderInformation2.Controls.Add(Me.chb1HrContribute)
        Me.TPHeaderInformation2.Controls.Add(Me.chb1HrNo)
        Me.TPHeaderInformation2.Controls.Add(Me.chb1HrYes)
        Me.TPHeaderInformation2.Location = New System.Drawing.Point(4, 22)
        Me.TPHeaderInformation2.Name = "TPHeaderInformation2"
        Me.TPHeaderInformation2.Size = New System.Drawing.Size(731, 363)
        Me.TPHeaderInformation2.TabIndex = 3
        Me.TPHeaderInformation2.Text = "Header Information 2"
        Me.TPHeaderInformation2.UseVisualStyleBackColor = True
        '
        'chbStateProgramCodes
        '
        Me.chbStateProgramCodes.AutoSize = True
        Me.chbStateProgramCodes.Location = New System.Drawing.Point(4, 290)
        Me.chbStateProgramCodes.Name = "chbStateProgramCodes"
        Me.chbStateProgramCodes.Size = New System.Drawing.Size(126, 17)
        Me.chbStateProgramCodes.TabIndex = 158
        Me.chbStateProgramCodes.Text = "State Program Codes"
        '
        'chbAttainmentStatus
        '
        Me.chbAttainmentStatus.AutoSize = True
        Me.chbAttainmentStatus.Location = New System.Drawing.Point(4, 0)
        Me.chbAttainmentStatus.Name = "chbAttainmentStatus"
        Me.chbAttainmentStatus.Size = New System.Drawing.Size(109, 17)
        Me.chbAttainmentStatus.TabIndex = 124
        Me.chbAttainmentStatus.Text = "Attainment Status"
        '
        'Panel63
        '
        Me.Panel63.AutoSize = True
        Me.Panel63.Controls.Add(Me.rdbHAPMajorNotEqual)
        Me.Panel63.Controls.Add(Me.rdbHAPMajorEqual)
        Me.Panel63.Location = New System.Drawing.Point(224, 327)
        Me.Panel63.Name = "Panel63"
        Me.Panel63.Size = New System.Drawing.Size(78, 27)
        Me.Panel63.TabIndex = 304
        '
        'rdbHAPMajorNotEqual
        '
        Me.rdbHAPMajorNotEqual.AutoSize = True
        Me.rdbHAPMajorNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbHAPMajorNotEqual.Name = "rdbHAPMajorNotEqual"
        Me.rdbHAPMajorNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbHAPMajorNotEqual.TabIndex = 164
        Me.rdbHAPMajorNotEqual.Text = "<>"
        '
        'rdbHAPMajorEqual
        '
        Me.rdbHAPMajorEqual.AutoSize = True
        Me.rdbHAPMajorEqual.Checked = True
        Me.rdbHAPMajorEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbHAPMajorEqual.Name = "rdbHAPMajorEqual"
        Me.rdbHAPMajorEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbHAPMajorEqual.TabIndex = 163
        Me.rdbHAPMajorEqual.TabStop = True
        Me.rdbHAPMajorEqual.Text = "="
        '
        'Panel62
        '
        Me.Panel62.AutoSize = True
        Me.Panel62.Controls.Add(Me.rdbNSRPSDMajorNotEqual)
        Me.Panel62.Controls.Add(Me.rdbNSRPSDMajorEqual)
        Me.Panel62.Location = New System.Drawing.Point(224, 302)
        Me.Panel62.Name = "Panel62"
        Me.Panel62.Size = New System.Drawing.Size(78, 27)
        Me.Panel62.TabIndex = 302
        '
        'rdbNSRPSDMajorNotEqual
        '
        Me.rdbNSRPSDMajorNotEqual.AutoSize = True
        Me.rdbNSRPSDMajorNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbNSRPSDMajorNotEqual.Name = "rdbNSRPSDMajorNotEqual"
        Me.rdbNSRPSDMajorNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbNSRPSDMajorNotEqual.TabIndex = 161
        Me.rdbNSRPSDMajorNotEqual.Text = "<>"
        '
        'rdbNSRPSDMajorEqual
        '
        Me.rdbNSRPSDMajorEqual.AutoSize = True
        Me.rdbNSRPSDMajorEqual.Checked = True
        Me.rdbNSRPSDMajorEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbNSRPSDMajorEqual.Name = "rdbNSRPSDMajorEqual"
        Me.rdbNSRPSDMajorEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbNSRPSDMajorEqual.TabIndex = 160
        Me.rdbNSRPSDMajorEqual.TabStop = True
        Me.rdbNSRPSDMajorEqual.Text = "="
        '
        'Panel61
        '
        Me.Panel61.AutoSize = True
        Me.Panel61.Controls.Add(Me.rdbPMNoNotEqual)
        Me.Panel61.Controls.Add(Me.rdbPMNoEqual)
        Me.Panel61.Location = New System.Drawing.Point(224, 264)
        Me.Panel61.Name = "Panel61"
        Me.Panel61.Size = New System.Drawing.Size(78, 27)
        Me.Panel61.TabIndex = 300
        '
        'rdbPMNoNotEqual
        '
        Me.rdbPMNoNotEqual.AutoSize = True
        Me.rdbPMNoNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbPMNoNotEqual.Name = "rdbPMNoNotEqual"
        Me.rdbPMNoNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbPMNoNotEqual.TabIndex = 157
        Me.rdbPMNoNotEqual.Text = "<>"
        '
        'rdbPMNoEqual
        '
        Me.rdbPMNoEqual.AutoSize = True
        Me.rdbPMNoEqual.Checked = True
        Me.rdbPMNoEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbPMNoEqual.Name = "rdbPMNoEqual"
        Me.rdbPMNoEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbPMNoEqual.TabIndex = 156
        Me.rdbPMNoEqual.TabStop = True
        Me.rdbPMNoEqual.Text = "="
        '
        'Panel60
        '
        Me.Panel60.AutoSize = True
        Me.Panel60.Controls.Add(Me.rdbPMMaconNotEqual)
        Me.Panel60.Controls.Add(Me.rdbPMMaconEqual)
        Me.Panel60.Location = New System.Drawing.Point(224, 239)
        Me.Panel60.Name = "Panel60"
        Me.Panel60.Size = New System.Drawing.Size(78, 27)
        Me.Panel60.TabIndex = 298
        '
        'rdbPMMaconNotEqual
        '
        Me.rdbPMMaconNotEqual.AutoSize = True
        Me.rdbPMMaconNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbPMMaconNotEqual.Name = "rdbPMMaconNotEqual"
        Me.rdbPMMaconNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbPMMaconNotEqual.TabIndex = 154
        Me.rdbPMMaconNotEqual.Text = "<>"
        '
        'rdbPMMaconEqual
        '
        Me.rdbPMMaconEqual.AutoSize = True
        Me.rdbPMMaconEqual.Checked = True
        Me.rdbPMMaconEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbPMMaconEqual.Name = "rdbPMMaconEqual"
        Me.rdbPMMaconEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbPMMaconEqual.TabIndex = 153
        Me.rdbPMMaconEqual.TabStop = True
        Me.rdbPMMaconEqual.Text = "="
        '
        'Panel59
        '
        Me.Panel59.AutoSize = True
        Me.Panel59.Controls.Add(Me.rdbPMFloydNotEqual)
        Me.Panel59.Controls.Add(Me.rdbPMFloydEqual)
        Me.Panel59.Location = New System.Drawing.Point(224, 214)
        Me.Panel59.Name = "Panel59"
        Me.Panel59.Size = New System.Drawing.Size(78, 27)
        Me.Panel59.TabIndex = 296
        '
        'rdbPMFloydNotEqual
        '
        Me.rdbPMFloydNotEqual.AutoSize = True
        Me.rdbPMFloydNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbPMFloydNotEqual.Name = "rdbPMFloydNotEqual"
        Me.rdbPMFloydNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbPMFloydNotEqual.TabIndex = 151
        Me.rdbPMFloydNotEqual.Text = "<>"
        '
        'rdbPMFloydEqual
        '
        Me.rdbPMFloydEqual.AutoSize = True
        Me.rdbPMFloydEqual.Checked = True
        Me.rdbPMFloydEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbPMFloydEqual.Name = "rdbPMFloydEqual"
        Me.rdbPMFloydEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbPMFloydEqual.TabIndex = 150
        Me.rdbPMFloydEqual.TabStop = True
        Me.rdbPMFloydEqual.Text = "="
        '
        'Panel58
        '
        Me.Panel58.AutoSize = True
        Me.Panel58.Controls.Add(Me.rdbPMChattanoogaNotEqual)
        Me.Panel58.Controls.Add(Me.rdbPMChattanoogaEqual)
        Me.Panel58.Location = New System.Drawing.Point(224, 189)
        Me.Panel58.Name = "Panel58"
        Me.Panel58.Size = New System.Drawing.Size(78, 27)
        Me.Panel58.TabIndex = 294
        '
        'rdbPMChattanoogaNotEqual
        '
        Me.rdbPMChattanoogaNotEqual.AutoSize = True
        Me.rdbPMChattanoogaNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbPMChattanoogaNotEqual.Name = "rdbPMChattanoogaNotEqual"
        Me.rdbPMChattanoogaNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbPMChattanoogaNotEqual.TabIndex = 148
        Me.rdbPMChattanoogaNotEqual.Text = "<>"
        '
        'rdbPMChattanoogaEqual
        '
        Me.rdbPMChattanoogaEqual.AutoSize = True
        Me.rdbPMChattanoogaEqual.Checked = True
        Me.rdbPMChattanoogaEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbPMChattanoogaEqual.Name = "rdbPMChattanoogaEqual"
        Me.rdbPMChattanoogaEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbPMChattanoogaEqual.TabIndex = 147
        Me.rdbPMChattanoogaEqual.TabStop = True
        Me.rdbPMChattanoogaEqual.Text = "="
        '
        'Panel57
        '
        Me.Panel57.AutoSize = True
        Me.Panel57.Controls.Add(Me.rdbPMAtlantaNotEqual)
        Me.Panel57.Controls.Add(Me.rdbPMAtlantaEqual)
        Me.Panel57.Location = New System.Drawing.Point(224, 164)
        Me.Panel57.Name = "Panel57"
        Me.Panel57.Size = New System.Drawing.Size(78, 27)
        Me.Panel57.TabIndex = 292
        '
        'rdbPMAtlantaNotEqual
        '
        Me.rdbPMAtlantaNotEqual.AutoSize = True
        Me.rdbPMAtlantaNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdbPMAtlantaNotEqual.Name = "rdbPMAtlantaNotEqual"
        Me.rdbPMAtlantaNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbPMAtlantaNotEqual.TabIndex = 145
        Me.rdbPMAtlantaNotEqual.Text = "<>"
        '
        'rdbPMAtlantaEqual
        '
        Me.rdbPMAtlantaEqual.AutoSize = True
        Me.rdbPMAtlantaEqual.Checked = True
        Me.rdbPMAtlantaEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbPMAtlantaEqual.Name = "rdbPMAtlantaEqual"
        Me.rdbPMAtlantaEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbPMAtlantaEqual.TabIndex = 144
        Me.rdbPMAtlantaEqual.TabStop = True
        Me.rdbPMAtlantaEqual.Text = "="
        '
        'Panel56
        '
        Me.Panel56.AutoSize = True
        Me.Panel56.Controls.Add(Me.rdb8HrNoNotEqual)
        Me.Panel56.Controls.Add(Me.rdb8HrNoEqual)
        Me.Panel56.Location = New System.Drawing.Point(224, 139)
        Me.Panel56.Name = "Panel56"
        Me.Panel56.Size = New System.Drawing.Size(78, 27)
        Me.Panel56.TabIndex = 290
        '
        'rdb8HrNoNotEqual
        '
        Me.rdb8HrNoNotEqual.AutoSize = True
        Me.rdb8HrNoNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdb8HrNoNotEqual.Name = "rdb8HrNoNotEqual"
        Me.rdb8HrNoNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdb8HrNoNotEqual.TabIndex = 142
        Me.rdb8HrNoNotEqual.Text = "<>"
        '
        'rdb8HrNoEqual
        '
        Me.rdb8HrNoEqual.AutoSize = True
        Me.rdb8HrNoEqual.Checked = True
        Me.rdb8HrNoEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdb8HrNoEqual.Name = "rdb8HrNoEqual"
        Me.rdb8HrNoEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdb8HrNoEqual.TabIndex = 141
        Me.rdb8HrNoEqual.TabStop = True
        Me.rdb8HrNoEqual.Text = "="
        '
        'Panel55
        '
        Me.Panel55.AutoSize = True
        Me.Panel55.Controls.Add(Me.rdb8HrMaconNotEqual)
        Me.Panel55.Controls.Add(Me.rdb8HrMaconEqual)
        Me.Panel55.Location = New System.Drawing.Point(224, 114)
        Me.Panel55.Name = "Panel55"
        Me.Panel55.Size = New System.Drawing.Size(78, 27)
        Me.Panel55.TabIndex = 288
        '
        'rdb8HrMaconNotEqual
        '
        Me.rdb8HrMaconNotEqual.AutoSize = True
        Me.rdb8HrMaconNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdb8HrMaconNotEqual.Name = "rdb8HrMaconNotEqual"
        Me.rdb8HrMaconNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdb8HrMaconNotEqual.TabIndex = 139
        Me.rdb8HrMaconNotEqual.Text = "<>"
        '
        'rdb8HrMaconEqual
        '
        Me.rdb8HrMaconEqual.AutoSize = True
        Me.rdb8HrMaconEqual.Checked = True
        Me.rdb8HrMaconEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdb8HrMaconEqual.Name = "rdb8HrMaconEqual"
        Me.rdb8HrMaconEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdb8HrMaconEqual.TabIndex = 138
        Me.rdb8HrMaconEqual.TabStop = True
        Me.rdb8HrMaconEqual.Text = "="
        '
        'Panel54
        '
        Me.Panel54.AutoSize = True
        Me.Panel54.Controls.Add(Me.rdb8HrAtlantaNotEqual)
        Me.Panel54.Controls.Add(Me.rdb8HrAtlantaEqual)
        Me.Panel54.Location = New System.Drawing.Point(224, 89)
        Me.Panel54.Name = "Panel54"
        Me.Panel54.Size = New System.Drawing.Size(78, 27)
        Me.Panel54.TabIndex = 286
        '
        'rdb8HrAtlantaNotEqual
        '
        Me.rdb8HrAtlantaNotEqual.AutoSize = True
        Me.rdb8HrAtlantaNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdb8HrAtlantaNotEqual.Name = "rdb8HrAtlantaNotEqual"
        Me.rdb8HrAtlantaNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdb8HrAtlantaNotEqual.TabIndex = 136
        Me.rdb8HrAtlantaNotEqual.Text = "<>"
        '
        'rdb8HrAtlantaEqual
        '
        Me.rdb8HrAtlantaEqual.AutoSize = True
        Me.rdb8HrAtlantaEqual.Checked = True
        Me.rdb8HrAtlantaEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdb8HrAtlantaEqual.Name = "rdb8HrAtlantaEqual"
        Me.rdb8HrAtlantaEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdb8HrAtlantaEqual.TabIndex = 135
        Me.rdb8HrAtlantaEqual.TabStop = True
        Me.rdb8HrAtlantaEqual.Text = "="
        '
        'Panel53
        '
        Me.Panel53.AutoSize = True
        Me.Panel53.Controls.Add(Me.rdb1HrContributeNotEqual)
        Me.Panel53.Controls.Add(Me.rdb1HrContributeEqual)
        Me.Panel53.Location = New System.Drawing.Point(224, 64)
        Me.Panel53.Name = "Panel53"
        Me.Panel53.Size = New System.Drawing.Size(78, 27)
        Me.Panel53.TabIndex = 284
        '
        'rdb1HrContributeNotEqual
        '
        Me.rdb1HrContributeNotEqual.AutoSize = True
        Me.rdb1HrContributeNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdb1HrContributeNotEqual.Name = "rdb1HrContributeNotEqual"
        Me.rdb1HrContributeNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdb1HrContributeNotEqual.TabIndex = 133
        Me.rdb1HrContributeNotEqual.Text = "<>"
        '
        'rdb1HrContributeEqual
        '
        Me.rdb1HrContributeEqual.AutoSize = True
        Me.rdb1HrContributeEqual.Checked = True
        Me.rdb1HrContributeEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdb1HrContributeEqual.Name = "rdb1HrContributeEqual"
        Me.rdb1HrContributeEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdb1HrContributeEqual.TabIndex = 132
        Me.rdb1HrContributeEqual.TabStop = True
        Me.rdb1HrContributeEqual.Text = "="
        '
        'Panel52
        '
        Me.Panel52.AutoSize = True
        Me.Panel52.Controls.Add(Me.rdb1HrNoNotEqual)
        Me.Panel52.Controls.Add(Me.rdb1HrNoEqual)
        Me.Panel52.Location = New System.Drawing.Point(224, 39)
        Me.Panel52.Name = "Panel52"
        Me.Panel52.Size = New System.Drawing.Size(78, 27)
        Me.Panel52.TabIndex = 282
        '
        'rdb1HrNoNotEqual
        '
        Me.rdb1HrNoNotEqual.AutoSize = True
        Me.rdb1HrNoNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdb1HrNoNotEqual.Name = "rdb1HrNoNotEqual"
        Me.rdb1HrNoNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdb1HrNoNotEqual.TabIndex = 130
        Me.rdb1HrNoNotEqual.Text = "<>"
        '
        'rdb1HrNoEqual
        '
        Me.rdb1HrNoEqual.AutoSize = True
        Me.rdb1HrNoEqual.Checked = True
        Me.rdb1HrNoEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdb1HrNoEqual.Name = "rdb1HrNoEqual"
        Me.rdb1HrNoEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdb1HrNoEqual.TabIndex = 129
        Me.rdb1HrNoEqual.TabStop = True
        Me.rdb1HrNoEqual.Text = "="
        '
        'chbHAPMajor
        '
        Me.chbHAPMajor.AutoSize = True
        Me.chbHAPMajor.Location = New System.Drawing.Point(27, 335)
        Me.chbHAPMajor.Name = "chbHAPMajor"
        Me.chbHAPMajor.Size = New System.Drawing.Size(82, 17)
        Me.chbHAPMajor.TabIndex = 162
        Me.chbHAPMajor.Text = "HAPs Major"
        '
        'chbNSRPSDMajor
        '
        Me.chbNSRPSDMajor.AutoSize = True
        Me.chbNSRPSDMajor.Location = New System.Drawing.Point(27, 310)
        Me.chbNSRPSDMajor.Name = "chbNSRPSDMajor"
        Me.chbNSRPSDMajor.Size = New System.Drawing.Size(105, 17)
        Me.chbNSRPSDMajor.TabIndex = 159
        Me.chbNSRPSDMajor.Text = "NSR/PSD Major"
        '
        'Panel51
        '
        Me.Panel51.AutoSize = True
        Me.Panel51.Controls.Add(Me.rdb1HrYesNotEqual)
        Me.Panel51.Controls.Add(Me.rdb1HrYesEqual)
        Me.Panel51.Location = New System.Drawing.Point(224, 14)
        Me.Panel51.Name = "Panel51"
        Me.Panel51.Size = New System.Drawing.Size(78, 27)
        Me.Panel51.TabIndex = 277
        '
        'rdb1HrYesNotEqual
        '
        Me.rdb1HrYesNotEqual.AutoSize = True
        Me.rdb1HrYesNotEqual.Location = New System.Drawing.Point(38, 7)
        Me.rdb1HrYesNotEqual.Name = "rdb1HrYesNotEqual"
        Me.rdb1HrYesNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdb1HrYesNotEqual.TabIndex = 127
        Me.rdb1HrYesNotEqual.Text = "<>"
        '
        'rdb1HrYesEqual
        '
        Me.rdb1HrYesEqual.AutoSize = True
        Me.rdb1HrYesEqual.Checked = True
        Me.rdb1HrYesEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdb1HrYesEqual.Name = "rdb1HrYesEqual"
        Me.rdb1HrYesEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdb1HrYesEqual.TabIndex = 126
        Me.rdb1HrYesEqual.TabStop = True
        Me.rdb1HrYesEqual.Text = "="
        '
        'chbPMNo
        '
        Me.chbPMNo.AutoSize = True
        Me.chbPMNo.Location = New System.Drawing.Point(26, 272)
        Me.chbPMNo.Name = "chbPMNo"
        Me.chbPMNo.Size = New System.Drawing.Size(77, 17)
        Me.chbPMNo.TabIndex = 155
        Me.chbPMNo.Text = "PM-2.5 No"
        '
        'chbPMMacon
        '
        Me.chbPMMacon.AutoSize = True
        Me.chbPMMacon.Location = New System.Drawing.Point(26, 247)
        Me.chbPMMacon.Name = "chbPMMacon"
        Me.chbPMMacon.Size = New System.Drawing.Size(96, 17)
        Me.chbPMMacon.TabIndex = 152
        Me.chbPMMacon.Text = "PM-2.5 Macon"
        '
        'chbPMFloyd
        '
        Me.chbPMFloyd.AutoSize = True
        Me.chbPMFloyd.Location = New System.Drawing.Point(26, 222)
        Me.chbPMFloyd.Name = "chbPMFloyd"
        Me.chbPMFloyd.Size = New System.Drawing.Size(88, 17)
        Me.chbPMFloyd.TabIndex = 149
        Me.chbPMFloyd.Text = "PM-2.5 Floyd"
        '
        'chbPMChattanooga
        '
        Me.chbPMChattanooga.AutoSize = True
        Me.chbPMChattanooga.Location = New System.Drawing.Point(26, 197)
        Me.chbPMChattanooga.Name = "chbPMChattanooga"
        Me.chbPMChattanooga.Size = New System.Drawing.Size(124, 17)
        Me.chbPMChattanooga.TabIndex = 146
        Me.chbPMChattanooga.Text = "PM-2.5 Chattanooga"
        '
        'chbPMAtlanta
        '
        Me.chbPMAtlanta.AutoSize = True
        Me.chbPMAtlanta.Location = New System.Drawing.Point(26, 172)
        Me.chbPMAtlanta.Name = "chbPMAtlanta"
        Me.chbPMAtlanta.Size = New System.Drawing.Size(96, 17)
        Me.chbPMAtlanta.TabIndex = 143
        Me.chbPMAtlanta.Text = "PM-2.5 Atlanta"
        '
        'chb8HrNo
        '
        Me.chb8HrNo.AutoSize = True
        Me.chb8HrNo.Location = New System.Drawing.Point(26, 147)
        Me.chb8HrNo.Name = "chb8HrNo"
        Me.chb8HrNo.Size = New System.Drawing.Size(63, 17)
        Me.chb8HrNo.TabIndex = 140
        Me.chb8HrNo.Text = "8-Hr No"
        '
        'chb8HrMacon
        '
        Me.chb8HrMacon.AutoSize = True
        Me.chb8HrMacon.Location = New System.Drawing.Point(26, 122)
        Me.chb8HrMacon.Name = "chb8HrMacon"
        Me.chb8HrMacon.Size = New System.Drawing.Size(82, 17)
        Me.chb8HrMacon.TabIndex = 137
        Me.chb8HrMacon.Text = "8-Hr Macon"
        '
        'chb8HrAtlanta
        '
        Me.chb8HrAtlanta.AutoSize = True
        Me.chb8HrAtlanta.Location = New System.Drawing.Point(26, 97)
        Me.chb8HrAtlanta.Name = "chb8HrAtlanta"
        Me.chb8HrAtlanta.Size = New System.Drawing.Size(82, 17)
        Me.chb8HrAtlanta.TabIndex = 134
        Me.chb8HrAtlanta.Text = "8-Hr Atlanta"
        '
        'chb1HrContribute
        '
        Me.chb1HrContribute.AutoSize = True
        Me.chb1HrContribute.Location = New System.Drawing.Point(26, 72)
        Me.chb1HrContribute.Name = "chb1HrContribute"
        Me.chb1HrContribute.Size = New System.Drawing.Size(175, 17)
        Me.chb1HrContribute.TabIndex = 131
        Me.chb1HrContribute.Text = "1-Hr Nonattainment (Contribute)"
        '
        'chb1HrNo
        '
        Me.chb1HrNo.AutoSize = True
        Me.chb1HrNo.Location = New System.Drawing.Point(26, 47)
        Me.chb1HrNo.Name = "chb1HrNo"
        Me.chb1HrNo.Size = New System.Drawing.Size(141, 17)
        Me.chb1HrNo.TabIndex = 128
        Me.chb1HrNo.Text = "1-Hr Nonattainment (No)"
        '
        'chb1HrYes
        '
        Me.chb1HrYes.AutoSize = True
        Me.chb1HrYes.Location = New System.Drawing.Point(26, 22)
        Me.chb1HrYes.Name = "chb1HrYes"
        Me.chb1HrYes.Size = New System.Drawing.Size(145, 17)
        Me.chb1HrYes.TabIndex = 125
        Me.chb1HrYes.Text = "1-Hr Nonattainment (Yes)"
        '
        'TPAirProgramCodes
        '
        Me.TPAirProgramCodes.Controls.Add(Me.Panel70)
        Me.TPAirProgramCodes.Controls.Add(Me.chbViewAirPrograms)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC0)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel50)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC1)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel38)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel49)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel39)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel48)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPCV)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPCM)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC3)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPCI)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel47)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC4)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel40)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel46)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel41)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel45)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPCF)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC6)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel44)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPCA)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC7)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC8)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel43)
        Me.TPAirProgramCodes.Controls.Add(Me.Panel42)
        Me.TPAirProgramCodes.Controls.Add(Me.chbAPC9)
        Me.TPAirProgramCodes.Location = New System.Drawing.Point(4, 22)
        Me.TPAirProgramCodes.Name = "TPAirProgramCodes"
        Me.TPAirProgramCodes.Size = New System.Drawing.Size(731, 363)
        Me.TPAirProgramCodes.TabIndex = 2
        Me.TPAirProgramCodes.Text = "Air Program Codes"
        Me.TPAirProgramCodes.UseVisualStyleBackColor = True
        '
        'Panel70
        '
        Me.Panel70.AutoSize = True
        Me.Panel70.Controls.Add(Me.rdbAPCOr)
        Me.Panel70.Controls.Add(Me.rdbAPCAnd)
        Me.Panel70.Location = New System.Drawing.Point(287, 21)
        Me.Panel70.Name = "Panel70"
        Me.Panel70.Size = New System.Drawing.Size(101, 27)
        Me.Panel70.TabIndex = 252
        '
        'rdbAPCOr
        '
        Me.rdbAPCOr.AutoSize = True
        Me.rdbAPCOr.Location = New System.Drawing.Point(57, 7)
        Me.rdbAPCOr.Name = "rdbAPCOr"
        Me.rdbAPCOr.Size = New System.Drawing.Size(41, 17)
        Me.rdbAPCOr.TabIndex = 219
        Me.rdbAPCOr.Text = "OR"
        '
        'rdbAPCAnd
        '
        Me.rdbAPCAnd.AutoSize = True
        Me.rdbAPCAnd.Checked = True
        Me.rdbAPCAnd.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPCAnd.Name = "rdbAPCAnd"
        Me.rdbAPCAnd.Size = New System.Drawing.Size(48, 17)
        Me.rdbAPCAnd.TabIndex = 218
        Me.rdbAPCAnd.TabStop = True
        Me.rdbAPCAnd.Text = "AND"
        '
        'chbViewAirPrograms
        '
        Me.chbViewAirPrograms.AutoSize = True
        Me.chbViewAirPrograms.Location = New System.Drawing.Point(4, 6)
        Me.chbViewAirPrograms.Name = "chbViewAirPrograms"
        Me.chbViewAirPrograms.Size = New System.Drawing.Size(111, 17)
        Me.chbViewAirPrograms.TabIndex = 165
        Me.chbViewAirPrograms.Text = "View Air Programs"
        '
        'chbAPC0
        '
        Me.chbAPC0.AutoSize = True
        Me.chbAPC0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC0.Location = New System.Drawing.Point(22, 29)
        Me.chbAPC0.Name = "chbAPC0"
        Me.chbAPC0.Size = New System.Drawing.Size(58, 17)
        Me.chbAPC0.TabIndex = 166
        Me.chbAPC0.Text = "0 - SIP"
        '
        'Panel50
        '
        Me.Panel50.AutoSize = True
        Me.Panel50.Controls.Add(Me.rdbAPCVNotEqual)
        Me.Panel50.Controls.Add(Me.rdbAPCVEqual)
        Me.Panel50.Location = New System.Drawing.Point(187, 321)
        Me.Panel50.Name = "Panel50"
        Me.Panel50.Size = New System.Drawing.Size(76, 27)
        Me.Panel50.TabIndex = 250
        '
        'rdbAPCVNotEqual
        '
        Me.rdbAPCVNotEqual.AutoSize = True
        Me.rdbAPCVNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPCVNotEqual.Name = "rdbAPCVNotEqual"
        Me.rdbAPCVNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPCVNotEqual.TabIndex = 216
        Me.rdbAPCVNotEqual.Text = "<>"
        '
        'rdbAPCVEqual
        '
        Me.rdbAPCVEqual.AutoSize = True
        Me.rdbAPCVEqual.Checked = True
        Me.rdbAPCVEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPCVEqual.Name = "rdbAPCVEqual"
        Me.rdbAPCVEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPCVEqual.TabIndex = 215
        Me.rdbAPCVEqual.TabStop = True
        Me.rdbAPCVEqual.Text = "="
        '
        'chbAPC1
        '
        Me.chbAPC1.AutoSize = True
        Me.chbAPC1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC1.Location = New System.Drawing.Point(22, 54)
        Me.chbAPC1.Name = "chbAPC1"
        Me.chbAPC1.Size = New System.Drawing.Size(96, 17)
        Me.chbAPC1.TabIndex = 170
        Me.chbAPC1.Text = "1 - Federal SIP"
        '
        'Panel38
        '
        Me.Panel38.AutoSize = True
        Me.Panel38.Controls.Add(Me.rdbAPC0NotEqual)
        Me.Panel38.Controls.Add(Me.rdbAPC0Equal)
        Me.Panel38.Location = New System.Drawing.Point(187, 21)
        Me.Panel38.Name = "Panel38"
        Me.Panel38.Size = New System.Drawing.Size(76, 27)
        Me.Panel38.TabIndex = 226
        '
        'rdbAPC0NotEqual
        '
        Me.rdbAPC0NotEqual.AutoSize = True
        Me.rdbAPC0NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC0NotEqual.Name = "rdbAPC0NotEqual"
        Me.rdbAPC0NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC0NotEqual.TabIndex = 168
        Me.rdbAPC0NotEqual.Text = "<>"
        '
        'rdbAPC0Equal
        '
        Me.rdbAPC0Equal.AutoSize = True
        Me.rdbAPC0Equal.Checked = True
        Me.rdbAPC0Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC0Equal.Name = "rdbAPC0Equal"
        Me.rdbAPC0Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC0Equal.TabIndex = 167
        Me.rdbAPC0Equal.TabStop = True
        Me.rdbAPC0Equal.Text = "="
        '
        'Panel49
        '
        Me.Panel49.AutoSize = True
        Me.Panel49.Controls.Add(Me.rdbAPCMNotEqual)
        Me.Panel49.Controls.Add(Me.rdbAPCMEqual)
        Me.Panel49.Location = New System.Drawing.Point(187, 296)
        Me.Panel49.Name = "Panel49"
        Me.Panel49.Size = New System.Drawing.Size(76, 27)
        Me.Panel49.TabIndex = 248
        '
        'rdbAPCMNotEqual
        '
        Me.rdbAPCMNotEqual.AutoSize = True
        Me.rdbAPCMNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPCMNotEqual.Name = "rdbAPCMNotEqual"
        Me.rdbAPCMNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPCMNotEqual.TabIndex = 212
        Me.rdbAPCMNotEqual.Text = "<>"
        '
        'rdbAPCMEqual
        '
        Me.rdbAPCMEqual.AutoSize = True
        Me.rdbAPCMEqual.Checked = True
        Me.rdbAPCMEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPCMEqual.Name = "rdbAPCMEqual"
        Me.rdbAPCMEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPCMEqual.TabIndex = 211
        Me.rdbAPCMEqual.TabStop = True
        Me.rdbAPCMEqual.Text = "="
        '
        'Panel39
        '
        Me.Panel39.AutoSize = True
        Me.Panel39.Controls.Add(Me.rdbAPC1NotEqual)
        Me.Panel39.Controls.Add(Me.rdbAPC1Equal)
        Me.Panel39.Location = New System.Drawing.Point(187, 46)
        Me.Panel39.Name = "Panel39"
        Me.Panel39.Size = New System.Drawing.Size(76, 27)
        Me.Panel39.TabIndex = 228
        '
        'rdbAPC1NotEqual
        '
        Me.rdbAPC1NotEqual.AutoSize = True
        Me.rdbAPC1NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC1NotEqual.Name = "rdbAPC1NotEqual"
        Me.rdbAPC1NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC1NotEqual.TabIndex = 172
        Me.rdbAPC1NotEqual.Text = "<>"
        '
        'rdbAPC1Equal
        '
        Me.rdbAPC1Equal.AutoSize = True
        Me.rdbAPC1Equal.Checked = True
        Me.rdbAPC1Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC1Equal.Name = "rdbAPC1Equal"
        Me.rdbAPC1Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC1Equal.TabIndex = 171
        Me.rdbAPC1Equal.TabStop = True
        Me.rdbAPC1Equal.Text = "="
        '
        'Panel48
        '
        Me.Panel48.AutoSize = True
        Me.Panel48.Controls.Add(Me.rdbAPCINotEqual)
        Me.Panel48.Controls.Add(Me.rdbAPCIEqual)
        Me.Panel48.Location = New System.Drawing.Point(187, 271)
        Me.Panel48.Name = "Panel48"
        Me.Panel48.Size = New System.Drawing.Size(76, 27)
        Me.Panel48.TabIndex = 246
        '
        'rdbAPCINotEqual
        '
        Me.rdbAPCINotEqual.AutoSize = True
        Me.rdbAPCINotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPCINotEqual.Name = "rdbAPCINotEqual"
        Me.rdbAPCINotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPCINotEqual.TabIndex = 208
        Me.rdbAPCINotEqual.Text = "<>"
        '
        'rdbAPCIEqual
        '
        Me.rdbAPCIEqual.AutoSize = True
        Me.rdbAPCIEqual.Checked = True
        Me.rdbAPCIEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPCIEqual.Name = "rdbAPCIEqual"
        Me.rdbAPCIEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPCIEqual.TabIndex = 207
        Me.rdbAPCIEqual.TabStop = True
        Me.rdbAPCIEqual.Text = "="
        '
        'chbAPCV
        '
        Me.chbAPCV.AutoSize = True
        Me.chbAPCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCV.Location = New System.Drawing.Point(22, 329)
        Me.chbAPCV.Name = "chbAPCV"
        Me.chbAPCV.Size = New System.Drawing.Size(72, 17)
        Me.chbAPCV.TabIndex = 214
        Me.chbAPCV.Text = "V - Title V"
        '
        'chbAPCM
        '
        Me.chbAPCM.AutoSize = True
        Me.chbAPCM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCM.Location = New System.Drawing.Point(22, 304)
        Me.chbAPCM.Name = "chbAPCM"
        Me.chbAPCM.Size = New System.Drawing.Size(74, 17)
        Me.chbAPCM.TabIndex = 210
        Me.chbAPCM.Text = "M - MACT"
        '
        'chbAPC3
        '
        Me.chbAPC3.AutoSize = True
        Me.chbAPC3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC3.Location = New System.Drawing.Point(22, 79)
        Me.chbAPC3.Name = "chbAPC3"
        Me.chbAPC3.Size = New System.Drawing.Size(119, 17)
        Me.chbAPC3.TabIndex = 174
        Me.chbAPC3.Text = "3 - Non-Federal SIP"
        '
        'chbAPCI
        '
        Me.chbAPCI.AutoSize = True
        Me.chbAPCI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCI.Location = New System.Drawing.Point(22, 279)
        Me.chbAPCI.Name = "chbAPCI"
        Me.chbAPCI.Size = New System.Drawing.Size(116, 17)
        Me.chbAPCI.TabIndex = 206
        Me.chbAPCI.Text = "I - Native American"
        '
        'Panel47
        '
        Me.Panel47.AutoSize = True
        Me.Panel47.Controls.Add(Me.rdbAPCFNotEqual)
        Me.Panel47.Controls.Add(Me.rdbAPCFEqual)
        Me.Panel47.Location = New System.Drawing.Point(187, 246)
        Me.Panel47.Name = "Panel47"
        Me.Panel47.Size = New System.Drawing.Size(76, 27)
        Me.Panel47.TabIndex = 244
        '
        'rdbAPCFNotEqual
        '
        Me.rdbAPCFNotEqual.AutoSize = True
        Me.rdbAPCFNotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPCFNotEqual.Name = "rdbAPCFNotEqual"
        Me.rdbAPCFNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPCFNotEqual.TabIndex = 204
        Me.rdbAPCFNotEqual.Text = "<>"
        '
        'rdbAPCFEqual
        '
        Me.rdbAPCFEqual.AutoSize = True
        Me.rdbAPCFEqual.Checked = True
        Me.rdbAPCFEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPCFEqual.Name = "rdbAPCFEqual"
        Me.rdbAPCFEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPCFEqual.TabIndex = 203
        Me.rdbAPCFEqual.TabStop = True
        Me.rdbAPCFEqual.Text = "="
        '
        'chbAPC4
        '
        Me.chbAPC4.AutoSize = True
        Me.chbAPC4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC4.Location = New System.Drawing.Point(22, 104)
        Me.chbAPC4.Name = "chbAPC4"
        Me.chbAPC4.Size = New System.Drawing.Size(106, 17)
        Me.chbAPC4.TabIndex = 178
        Me.chbAPC4.Text = "4 - CFC Tracking"
        '
        'Panel40
        '
        Me.Panel40.AutoSize = True
        Me.Panel40.Controls.Add(Me.rdbAPC3NotEqual)
        Me.Panel40.Controls.Add(Me.rdbAPC3Equal)
        Me.Panel40.Location = New System.Drawing.Point(187, 71)
        Me.Panel40.Name = "Panel40"
        Me.Panel40.Size = New System.Drawing.Size(76, 27)
        Me.Panel40.TabIndex = 230
        '
        'rdbAPC3NotEqual
        '
        Me.rdbAPC3NotEqual.AutoSize = True
        Me.rdbAPC3NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC3NotEqual.Name = "rdbAPC3NotEqual"
        Me.rdbAPC3NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC3NotEqual.TabIndex = 176
        Me.rdbAPC3NotEqual.Text = "<>"
        '
        'rdbAPC3Equal
        '
        Me.rdbAPC3Equal.AutoSize = True
        Me.rdbAPC3Equal.Checked = True
        Me.rdbAPC3Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC3Equal.Name = "rdbAPC3Equal"
        Me.rdbAPC3Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC3Equal.TabIndex = 175
        Me.rdbAPC3Equal.TabStop = True
        Me.rdbAPC3Equal.Text = "="
        '
        'Panel46
        '
        Me.Panel46.AutoSize = True
        Me.Panel46.Controls.Add(Me.rdbAPCANotEqual)
        Me.Panel46.Controls.Add(Me.rdbAPCAEqual)
        Me.Panel46.Location = New System.Drawing.Point(187, 221)
        Me.Panel46.Name = "Panel46"
        Me.Panel46.Size = New System.Drawing.Size(76, 27)
        Me.Panel46.TabIndex = 242
        '
        'rdbAPCANotEqual
        '
        Me.rdbAPCANotEqual.AutoSize = True
        Me.rdbAPCANotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPCANotEqual.Name = "rdbAPCANotEqual"
        Me.rdbAPCANotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPCANotEqual.TabIndex = 200
        Me.rdbAPCANotEqual.Text = "<>"
        '
        'rdbAPCAEqual
        '
        Me.rdbAPCAEqual.AutoSize = True
        Me.rdbAPCAEqual.Checked = True
        Me.rdbAPCAEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPCAEqual.Name = "rdbAPCAEqual"
        Me.rdbAPCAEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPCAEqual.TabIndex = 199
        Me.rdbAPCAEqual.TabStop = True
        Me.rdbAPCAEqual.Text = "="
        '
        'Panel41
        '
        Me.Panel41.AutoSize = True
        Me.Panel41.Controls.Add(Me.rdbAPC4NotEqual)
        Me.Panel41.Controls.Add(Me.rdbAPC4Equal)
        Me.Panel41.Location = New System.Drawing.Point(187, 96)
        Me.Panel41.Name = "Panel41"
        Me.Panel41.Size = New System.Drawing.Size(76, 27)
        Me.Panel41.TabIndex = 232
        '
        'rdbAPC4NotEqual
        '
        Me.rdbAPC4NotEqual.AutoSize = True
        Me.rdbAPC4NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC4NotEqual.Name = "rdbAPC4NotEqual"
        Me.rdbAPC4NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC4NotEqual.TabIndex = 180
        Me.rdbAPC4NotEqual.Text = "<>"
        '
        'rdbAPC4Equal
        '
        Me.rdbAPC4Equal.AutoSize = True
        Me.rdbAPC4Equal.Checked = True
        Me.rdbAPC4Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC4Equal.Name = "rdbAPC4Equal"
        Me.rdbAPC4Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC4Equal.TabIndex = 179
        Me.rdbAPC4Equal.TabStop = True
        Me.rdbAPC4Equal.Text = "="
        '
        'Panel45
        '
        Me.Panel45.AutoSize = True
        Me.Panel45.Controls.Add(Me.rdbAPC9NotEqual)
        Me.Panel45.Controls.Add(Me.rdbAPC9Equal)
        Me.Panel45.Location = New System.Drawing.Point(187, 196)
        Me.Panel45.Name = "Panel45"
        Me.Panel45.Size = New System.Drawing.Size(76, 27)
        Me.Panel45.TabIndex = 240
        '
        'rdbAPC9NotEqual
        '
        Me.rdbAPC9NotEqual.AutoSize = True
        Me.rdbAPC9NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC9NotEqual.Name = "rdbAPC9NotEqual"
        Me.rdbAPC9NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC9NotEqual.TabIndex = 196
        Me.rdbAPC9NotEqual.Text = "<>"
        '
        'rdbAPC9Equal
        '
        Me.rdbAPC9Equal.AutoSize = True
        Me.rdbAPC9Equal.Checked = True
        Me.rdbAPC9Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC9Equal.Name = "rdbAPC9Equal"
        Me.rdbAPC9Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC9Equal.TabIndex = 195
        Me.rdbAPC9Equal.TabStop = True
        Me.rdbAPC9Equal.Text = "="
        '
        'chbAPCF
        '
        Me.chbAPCF.AutoSize = True
        Me.chbAPCF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCF.Location = New System.Drawing.Point(22, 254)
        Me.chbAPCF.Name = "chbAPCF"
        Me.chbAPCF.Size = New System.Drawing.Size(76, 17)
        Me.chbAPCF.TabIndex = 202
        Me.chbAPCF.Text = "F - FESOP"
        '
        'chbAPC6
        '
        Me.chbAPC6.AutoSize = True
        Me.chbAPC6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC6.Location = New System.Drawing.Point(22, 129)
        Me.chbAPC6.Name = "chbAPC6"
        Me.chbAPC6.Size = New System.Drawing.Size(63, 17)
        Me.chbAPC6.TabIndex = 182
        Me.chbAPC6.Text = "6 - PSD"
        '
        'Panel44
        '
        Me.Panel44.AutoSize = True
        Me.Panel44.Controls.Add(Me.rdbAPC8NotEqual)
        Me.Panel44.Controls.Add(Me.rdbAPC8Equal)
        Me.Panel44.Location = New System.Drawing.Point(187, 171)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Size = New System.Drawing.Size(76, 27)
        Me.Panel44.TabIndex = 238
        '
        'rdbAPC8NotEqual
        '
        Me.rdbAPC8NotEqual.AutoSize = True
        Me.rdbAPC8NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC8NotEqual.Name = "rdbAPC8NotEqual"
        Me.rdbAPC8NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC8NotEqual.TabIndex = 192
        Me.rdbAPC8NotEqual.Text = "<>"
        '
        'rdbAPC8Equal
        '
        Me.rdbAPC8Equal.AutoSize = True
        Me.rdbAPC8Equal.Checked = True
        Me.rdbAPC8Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC8Equal.Name = "rdbAPC8Equal"
        Me.rdbAPC8Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC8Equal.TabIndex = 191
        Me.rdbAPC8Equal.TabStop = True
        Me.rdbAPC8Equal.Text = "="
        '
        'chbAPCA
        '
        Me.chbAPCA.AutoSize = True
        Me.chbAPCA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCA.Location = New System.Drawing.Point(22, 229)
        Me.chbAPCA.Name = "chbAPCA"
        Me.chbAPCA.Size = New System.Drawing.Size(124, 17)
        Me.chbAPCA.TabIndex = 198
        Me.chbAPCA.Text = "A - Acid Precipitation"
        '
        'chbAPC7
        '
        Me.chbAPC7.AutoSize = True
        Me.chbAPC7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC7.Location = New System.Drawing.Point(22, 154)
        Me.chbAPC7.Name = "chbAPC7"
        Me.chbAPC7.Size = New System.Drawing.Size(64, 17)
        Me.chbAPC7.TabIndex = 186
        Me.chbAPC7.Text = "7 - NSR"
        '
        'chbAPC8
        '
        Me.chbAPC8.AutoSize = True
        Me.chbAPC8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC8.Location = New System.Drawing.Point(22, 179)
        Me.chbAPC8.Name = "chbAPC8"
        Me.chbAPC8.Size = New System.Drawing.Size(85, 17)
        Me.chbAPC8.TabIndex = 190
        Me.chbAPC8.Text = "8 - NESHAP"
        '
        'Panel43
        '
        Me.Panel43.AutoSize = True
        Me.Panel43.Controls.Add(Me.rdbAPC7NotEqual)
        Me.Panel43.Controls.Add(Me.rdbAPC7Equal)
        Me.Panel43.Location = New System.Drawing.Point(187, 146)
        Me.Panel43.Name = "Panel43"
        Me.Panel43.Size = New System.Drawing.Size(76, 27)
        Me.Panel43.TabIndex = 236
        '
        'rdbAPC7NotEqual
        '
        Me.rdbAPC7NotEqual.AutoSize = True
        Me.rdbAPC7NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC7NotEqual.Name = "rdbAPC7NotEqual"
        Me.rdbAPC7NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC7NotEqual.TabIndex = 188
        Me.rdbAPC7NotEqual.Text = "<>"
        '
        'rdbAPC7Equal
        '
        Me.rdbAPC7Equal.AutoSize = True
        Me.rdbAPC7Equal.Checked = True
        Me.rdbAPC7Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC7Equal.Name = "rdbAPC7Equal"
        Me.rdbAPC7Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC7Equal.TabIndex = 187
        Me.rdbAPC7Equal.TabStop = True
        Me.rdbAPC7Equal.Text = "="
        '
        'Panel42
        '
        Me.Panel42.AutoSize = True
        Me.Panel42.Controls.Add(Me.rdbAPC6NotEqual)
        Me.Panel42.Controls.Add(Me.rdbAPC6Equal)
        Me.Panel42.Location = New System.Drawing.Point(187, 121)
        Me.Panel42.Name = "Panel42"
        Me.Panel42.Size = New System.Drawing.Size(76, 27)
        Me.Panel42.TabIndex = 234
        '
        'rdbAPC6NotEqual
        '
        Me.rdbAPC6NotEqual.AutoSize = True
        Me.rdbAPC6NotEqual.Location = New System.Drawing.Point(36, 7)
        Me.rdbAPC6NotEqual.Name = "rdbAPC6NotEqual"
        Me.rdbAPC6NotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbAPC6NotEqual.TabIndex = 184
        Me.rdbAPC6NotEqual.Text = "<>"
        '
        'rdbAPC6Equal
        '
        Me.rdbAPC6Equal.AutoSize = True
        Me.rdbAPC6Equal.Checked = True
        Me.rdbAPC6Equal.Location = New System.Drawing.Point(7, 7)
        Me.rdbAPC6Equal.Name = "rdbAPC6Equal"
        Me.rdbAPC6Equal.Size = New System.Drawing.Size(31, 17)
        Me.rdbAPC6Equal.TabIndex = 183
        Me.rdbAPC6Equal.TabStop = True
        Me.rdbAPC6Equal.Text = "="
        '
        'chbAPC9
        '
        Me.chbAPC9.AutoSize = True
        Me.chbAPC9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC9.Location = New System.Drawing.Point(22, 204)
        Me.chbAPC9.Name = "chbAPC9"
        Me.chbAPC9.Size = New System.Drawing.Size(70, 17)
        Me.chbAPC9.TabIndex = 194
        Me.chbAPC9.Text = "9 - NSPS"
        '
        'TPSubpartData
        '
        Me.TPSubpartData.Controls.Add(Me.cboPart63Search2)
        Me.TPSubpartData.Controls.Add(Me.cboPart60Search2)
        Me.TPSubpartData.Controls.Add(Me.cboPart63Search1)
        Me.TPSubpartData.Controls.Add(Me.cboPart60Search1)
        Me.TPSubpartData.Controls.Add(Me.cboPart61Search2)
        Me.TPSubpartData.Controls.Add(Me.cboPart61Search1)
        Me.TPSubpartData.Controls.Add(Me.cboSIPSearch2)
        Me.TPSubpartData.Controls.Add(Me.cboSIPSearch1)
        Me.TPSubpartData.Controls.Add(Me.chbSIP)
        Me.TPSubpartData.Controls.Add(Me.chbAllSubparts)
        Me.TPSubpartData.Controls.Add(Me.chbPart61Subpart)
        Me.TPSubpartData.Controls.Add(Me.chbPart60Subpart)
        Me.TPSubpartData.Controls.Add(Me.chbPart63Subpart)
        Me.TPSubpartData.Location = New System.Drawing.Point(4, 22)
        Me.TPSubpartData.Name = "TPSubpartData"
        Me.TPSubpartData.Size = New System.Drawing.Size(731, 363)
        Me.TPSubpartData.TabIndex = 4
        Me.TPSubpartData.Text = "Subpart Data"
        Me.TPSubpartData.UseVisualStyleBackColor = True
        '
        'cboPart63Search2
        '
        Me.cboPart63Search2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart63Search2.FormattingEnabled = True
        Me.cboPart63Search2.Location = New System.Drawing.Point(299, 100)
        Me.cboPart63Search2.Name = "cboPart63Search2"
        Me.cboPart63Search2.Size = New System.Drawing.Size(127, 21)
        Me.cboPart63Search2.TabIndex = 244
        '
        'cboPart60Search2
        '
        Me.cboPart60Search2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart60Search2.FormattingEnabled = True
        Me.cboPart60Search2.Location = New System.Drawing.Point(299, 76)
        Me.cboPart60Search2.Name = "cboPart60Search2"
        Me.cboPart60Search2.Size = New System.Drawing.Size(127, 21)
        Me.cboPart60Search2.TabIndex = 237
        '
        'cboPart63Search1
        '
        Me.cboPart63Search1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart63Search1.FormattingEnabled = True
        Me.cboPart63Search1.Location = New System.Drawing.Point(159, 100)
        Me.cboPart63Search1.Name = "cboPart63Search1"
        Me.cboPart63Search1.Size = New System.Drawing.Size(127, 21)
        Me.cboPart63Search1.TabIndex = 243
        '
        'cboPart60Search1
        '
        Me.cboPart60Search1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart60Search1.FormattingEnabled = True
        Me.cboPart60Search1.Location = New System.Drawing.Point(159, 76)
        Me.cboPart60Search1.Name = "cboPart60Search1"
        Me.cboPart60Search1.Size = New System.Drawing.Size(127, 21)
        Me.cboPart60Search1.TabIndex = 236
        '
        'cboPart61Search2
        '
        Me.cboPart61Search2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart61Search2.FormattingEnabled = True
        Me.cboPart61Search2.Location = New System.Drawing.Point(299, 51)
        Me.cboPart61Search2.Name = "cboPart61Search2"
        Me.cboPart61Search2.Size = New System.Drawing.Size(127, 21)
        Me.cboPart61Search2.TabIndex = 230
        '
        'cboPart61Search1
        '
        Me.cboPart61Search1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart61Search1.FormattingEnabled = True
        Me.cboPart61Search1.Location = New System.Drawing.Point(159, 51)
        Me.cboPart61Search1.Name = "cboPart61Search1"
        Me.cboPart61Search1.Size = New System.Drawing.Size(127, 21)
        Me.cboPart61Search1.TabIndex = 229
        '
        'cboSIPSearch2
        '
        Me.cboSIPSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSIPSearch2.FormattingEnabled = True
        Me.cboSIPSearch2.Location = New System.Drawing.Point(298, 26)
        Me.cboSIPSearch2.Name = "cboSIPSearch2"
        Me.cboSIPSearch2.Size = New System.Drawing.Size(128, 21)
        Me.cboSIPSearch2.TabIndex = 223
        '
        'cboSIPSearch1
        '
        Me.cboSIPSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSIPSearch1.FormattingEnabled = True
        Me.cboSIPSearch1.Location = New System.Drawing.Point(159, 26)
        Me.cboSIPSearch1.Name = "cboSIPSearch1"
        Me.cboSIPSearch1.Size = New System.Drawing.Size(127, 21)
        Me.cboSIPSearch1.TabIndex = 222
        '
        'chbSIP
        '
        Me.chbSIP.AutoSize = True
        Me.chbSIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbSIP.Location = New System.Drawing.Point(22, 28)
        Me.chbSIP.Name = "chbSIP"
        Me.chbSIP.Size = New System.Drawing.Size(58, 17)
        Me.chbSIP.TabIndex = 221
        Me.chbSIP.Text = "0 - SIP"
        '
        'chbAllSubparts
        '
        Me.chbAllSubparts.AutoSize = True
        Me.chbAllSubparts.Location = New System.Drawing.Point(4, 6)
        Me.chbAllSubparts.Name = "chbAllSubparts"
        Me.chbAllSubparts.Size = New System.Drawing.Size(108, 17)
        Me.chbAllSubparts.TabIndex = 220
        Me.chbAllSubparts.Text = "View All Subparts"
        '
        'chbPart61Subpart
        '
        Me.chbPart61Subpart.AutoSize = True
        Me.chbPart61Subpart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbPart61Subpart.Location = New System.Drawing.Point(22, 53)
        Me.chbPart61Subpart.Name = "chbPart61Subpart"
        Me.chbPart61Subpart.Size = New System.Drawing.Size(131, 17)
        Me.chbPart61Subpart.TabIndex = 228
        Me.chbPart61Subpart.Text = "8 - NESHAP (Part 61) "
        '
        'chbPart60Subpart
        '
        Me.chbPart60Subpart.AutoSize = True
        Me.chbPart60Subpart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbPart60Subpart.Location = New System.Drawing.Point(22, 78)
        Me.chbPart60Subpart.Name = "chbPart60Subpart"
        Me.chbPart60Subpart.Size = New System.Drawing.Size(113, 17)
        Me.chbPart60Subpart.TabIndex = 235
        Me.chbPart60Subpart.Text = "9 - NSPS (Part 60)"
        '
        'chbPart63Subpart
        '
        Me.chbPart63Subpart.AutoSize = True
        Me.chbPart63Subpart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbPart63Subpart.Location = New System.Drawing.Point(22, 102)
        Me.chbPart63Subpart.Name = "chbPart63Subpart"
        Me.chbPart63Subpart.Size = New System.Drawing.Size(120, 17)
        Me.chbPart63Subpart.TabIndex = 242
        Me.chbPart63Subpart.Text = "M - MACT (Part 63) "
        '
        'TPComplianceInfo
        '
        Me.TPComplianceInfo.Controls.Add(Me.Panel76)
        Me.TPComplianceInfo.Controls.Add(Me.chbDistrictResponsible)
        Me.TPComplianceInfo.Controls.Add(Me.DTPLastFCESearch2)
        Me.TPComplianceInfo.Controls.Add(Me.DTPLastFCESearch1)
        Me.TPComplianceInfo.Controls.Add(Me.chbLastFCE)
        Me.TPComplianceInfo.Controls.Add(Me.cboSSCPUnitSearch2)
        Me.TPComplianceInfo.Controls.Add(Me.cboSSCPUnitSearch1)
        Me.TPComplianceInfo.Controls.Add(Me.Panel72)
        Me.TPComplianceInfo.Controls.Add(Me.chbSSCPUnit)
        Me.TPComplianceInfo.Controls.Add(Me.cboSSCPEngineerSearch2)
        Me.TPComplianceInfo.Controls.Add(Me.cboSSCPEngineerSearch1)
        Me.TPComplianceInfo.Controls.Add(Me.Panel69)
        Me.TPComplianceInfo.Controls.Add(Me.chbSSCPEngineer)
        Me.TPComplianceInfo.Location = New System.Drawing.Point(4, 22)
        Me.TPComplianceInfo.Name = "TPComplianceInfo"
        Me.TPComplianceInfo.Size = New System.Drawing.Size(731, 363)
        Me.TPComplianceInfo.TabIndex = 5
        Me.TPComplianceInfo.Text = "Compliance Info"
        Me.TPComplianceInfo.UseVisualStyleBackColor = True
        '
        'Panel76
        '
        Me.Panel76.AutoSize = True
        Me.Panel76.Controls.Add(Me.rdbDistrictResponsibleFalse)
        Me.Panel76.Controls.Add(Me.rdbDistrictResponsibleTrue)
        Me.Panel76.Location = New System.Drawing.Point(420, 93)
        Me.Panel76.Name = "Panel76"
        Me.Panel76.Size = New System.Drawing.Size(77, 27)
        Me.Panel76.TabIndex = 234
        '
        'rdbDistrictResponsibleFalse
        '
        Me.rdbDistrictResponsibleFalse.AutoSize = True
        Me.rdbDistrictResponsibleFalse.Location = New System.Drawing.Point(37, 7)
        Me.rdbDistrictResponsibleFalse.Name = "rdbDistrictResponsibleFalse"
        Me.rdbDistrictResponsibleFalse.Size = New System.Drawing.Size(37, 17)
        Me.rdbDistrictResponsibleFalse.TabIndex = 88
        Me.rdbDistrictResponsibleFalse.Text = "<>"
        '
        'rdbDistrictResponsibleTrue
        '
        Me.rdbDistrictResponsibleTrue.AutoSize = True
        Me.rdbDistrictResponsibleTrue.Checked = True
        Me.rdbDistrictResponsibleTrue.Location = New System.Drawing.Point(7, 7)
        Me.rdbDistrictResponsibleTrue.Name = "rdbDistrictResponsibleTrue"
        Me.rdbDistrictResponsibleTrue.Size = New System.Drawing.Size(31, 17)
        Me.rdbDistrictResponsibleTrue.TabIndex = 87
        Me.rdbDistrictResponsibleTrue.TabStop = True
        Me.rdbDistrictResponsibleTrue.Text = "="
        '
        'chbDistrictResponsible
        '
        Me.chbDistrictResponsible.AutoSize = True
        Me.chbDistrictResponsible.Location = New System.Drawing.Point(8, 98)
        Me.chbDistrictResponsible.Name = "chbDistrictResponsible"
        Me.chbDistrictResponsible.Size = New System.Drawing.Size(119, 17)
        Me.chbDistrictResponsible.TabIndex = 233
        Me.chbDistrictResponsible.Text = "District Responsible"
        '
        'DTPLastFCESearch2
        '
        Me.DTPLastFCESearch2.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLastFCESearch2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPLastFCESearch2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLastFCESearch2.Location = New System.Drawing.Point(279, 63)
        Me.DTPLastFCESearch2.Name = "DTPLastFCESearch2"
        Me.DTPLastFCESearch2.ShowCheckBox = True
        Me.DTPLastFCESearch2.Size = New System.Drawing.Size(132, 22)
        Me.DTPLastFCESearch2.TabIndex = 230
        Me.DTPLastFCESearch2.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPLastFCESearch1
        '
        Me.DTPLastFCESearch1.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLastFCESearch1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPLastFCESearch1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLastFCESearch1.Location = New System.Drawing.Point(141, 63)
        Me.DTPLastFCESearch1.Name = "DTPLastFCESearch1"
        Me.DTPLastFCESearch1.ShowCheckBox = True
        Me.DTPLastFCESearch1.Size = New System.Drawing.Size(132, 22)
        Me.DTPLastFCESearch1.TabIndex = 229
        Me.DTPLastFCESearch1.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'chbLastFCE
        '
        Me.chbLastFCE.AutoSize = True
        Me.chbLastFCE.Location = New System.Drawing.Point(8, 66)
        Me.chbLastFCE.Name = "chbLastFCE"
        Me.chbLastFCE.Size = New System.Drawing.Size(69, 17)
        Me.chbLastFCE.TabIndex = 228
        Me.chbLastFCE.Text = "Last FCE"
        '
        'cboSSCPUnitSearch2
        '
        Me.cboSSCPUnitSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSSCPUnitSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSSCPUnitSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSSCPUnitSearch2.Location = New System.Drawing.Point(279, 36)
        Me.cboSSCPUnitSearch2.Name = "cboSSCPUnitSearch2"
        Me.cboSSCPUnitSearch2.Size = New System.Drawing.Size(132, 21)
        Me.cboSSCPUnitSearch2.TabIndex = 224
        '
        'cboSSCPUnitSearch1
        '
        Me.cboSSCPUnitSearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSSCPUnitSearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSSCPUnitSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSSCPUnitSearch1.Location = New System.Drawing.Point(141, 36)
        Me.cboSSCPUnitSearch1.Name = "cboSSCPUnitSearch1"
        Me.cboSSCPUnitSearch1.Size = New System.Drawing.Size(132, 21)
        Me.cboSSCPUnitSearch1.TabIndex = 223
        '
        'Panel72
        '
        Me.Panel72.AutoSize = True
        Me.Panel72.Controls.Add(Me.rdbSSCPUnitNotEqual)
        Me.Panel72.Controls.Add(Me.rdbSSCPUnitEqual)
        Me.Panel72.Location = New System.Drawing.Point(420, 33)
        Me.Panel72.Name = "Panel72"
        Me.Panel72.Size = New System.Drawing.Size(77, 27)
        Me.Panel72.TabIndex = 227
        '
        'rdbSSCPUnitNotEqual
        '
        Me.rdbSSCPUnitNotEqual.AutoSize = True
        Me.rdbSSCPUnitNotEqual.Location = New System.Drawing.Point(37, 7)
        Me.rdbSSCPUnitNotEqual.Name = "rdbSSCPUnitNotEqual"
        Me.rdbSSCPUnitNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbSSCPUnitNotEqual.TabIndex = 88
        Me.rdbSSCPUnitNotEqual.Text = "<>"
        '
        'rdbSSCPUnitEqual
        '
        Me.rdbSSCPUnitEqual.AutoSize = True
        Me.rdbSSCPUnitEqual.Checked = True
        Me.rdbSSCPUnitEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbSSCPUnitEqual.Name = "rdbSSCPUnitEqual"
        Me.rdbSSCPUnitEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbSSCPUnitEqual.TabIndex = 87
        Me.rdbSSCPUnitEqual.TabStop = True
        Me.rdbSSCPUnitEqual.Text = "="
        '
        'chbSSCPUnit
        '
        Me.chbSSCPUnit.AutoSize = True
        Me.chbSSCPUnit.Location = New System.Drawing.Point(8, 38)
        Me.chbSSCPUnit.Name = "chbSSCPUnit"
        Me.chbSSCPUnit.Size = New System.Drawing.Size(103, 17)
        Me.chbSSCPUnit.TabIndex = 222
        Me.chbSSCPUnit.Text = "Compliance Unit"
        '
        'cboSSCPEngineerSearch2
        '
        Me.cboSSCPEngineerSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSSCPEngineerSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSSCPEngineerSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSSCPEngineerSearch2.Location = New System.Drawing.Point(279, 9)
        Me.cboSSCPEngineerSearch2.Name = "cboSSCPEngineerSearch2"
        Me.cboSSCPEngineerSearch2.Size = New System.Drawing.Size(132, 21)
        Me.cboSSCPEngineerSearch2.TabIndex = 218
        '
        'cboSSCPEngineerSearch1
        '
        Me.cboSSCPEngineerSearch1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSSCPEngineerSearch1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSSCPEngineerSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSSCPEngineerSearch1.Location = New System.Drawing.Point(141, 9)
        Me.cboSSCPEngineerSearch1.Name = "cboSSCPEngineerSearch1"
        Me.cboSSCPEngineerSearch1.Size = New System.Drawing.Size(132, 21)
        Me.cboSSCPEngineerSearch1.TabIndex = 217
        '
        'Panel69
        '
        Me.Panel69.AutoSize = True
        Me.Panel69.Controls.Add(Me.rdbSSCPEngineerNotEqual)
        Me.Panel69.Controls.Add(Me.rdbSSCPEngineerEqual)
        Me.Panel69.Location = New System.Drawing.Point(420, 6)
        Me.Panel69.Name = "Panel69"
        Me.Panel69.Size = New System.Drawing.Size(77, 27)
        Me.Panel69.TabIndex = 221
        '
        'rdbSSCPEngineerNotEqual
        '
        Me.rdbSSCPEngineerNotEqual.AutoSize = True
        Me.rdbSSCPEngineerNotEqual.Location = New System.Drawing.Point(37, 7)
        Me.rdbSSCPEngineerNotEqual.Name = "rdbSSCPEngineerNotEqual"
        Me.rdbSSCPEngineerNotEqual.Size = New System.Drawing.Size(37, 17)
        Me.rdbSSCPEngineerNotEqual.TabIndex = 88
        Me.rdbSSCPEngineerNotEqual.Text = "<>"
        '
        'rdbSSCPEngineerEqual
        '
        Me.rdbSSCPEngineerEqual.AutoSize = True
        Me.rdbSSCPEngineerEqual.Checked = True
        Me.rdbSSCPEngineerEqual.Location = New System.Drawing.Point(7, 7)
        Me.rdbSSCPEngineerEqual.Name = "rdbSSCPEngineerEqual"
        Me.rdbSSCPEngineerEqual.Size = New System.Drawing.Size(31, 17)
        Me.rdbSSCPEngineerEqual.TabIndex = 87
        Me.rdbSSCPEngineerEqual.TabStop = True
        Me.rdbSSCPEngineerEqual.Text = "="
        '
        'chbSSCPEngineer
        '
        Me.chbSSCPEngineer.AutoSize = True
        Me.chbSSCPEngineer.Location = New System.Drawing.Point(8, 11)
        Me.chbSSCPEngineer.Name = "chbSSCPEngineer"
        Me.chbSSCPEngineer.Size = New System.Drawing.Size(126, 17)
        Me.chbSSCPEngineer.TabIndex = 216
        Me.chbSSCPEngineer.Text = "Compliance Engineer"
        '
        'TPCannedReports
        '
        Me.TPCannedReports.Controls.Add(Me.lblCannedQueryCount)
        Me.TPCannedReports.Controls.Add(Me.cboCannedSelection)
        Me.TPCannedReports.Controls.Add(Me.lblCannedSelection)
        Me.TPCannedReports.Controls.Add(Me.lblCannedEndDate)
        Me.TPCannedReports.Controls.Add(Me.lblCannedStartDate)
        Me.TPCannedReports.Controls.Add(Me.dtpCannedEndDate)
        Me.TPCannedReports.Controls.Add(Me.dtpCannedStartDate)
        Me.TPCannedReports.Controls.Add(Me.rdbCannedHistoryClass)
        Me.TPCannedReports.Controls.Add(Me.rdbCannedHistoryAirProgram)
        Me.TPCannedReports.Controls.Add(Me.rdbCannedPermitContactData)
        Me.TPCannedReports.Controls.Add(Me.lblCannedHistoryAirProgram)
        Me.TPCannedReports.Controls.Add(Me.lblCannedHistoryClass)
        Me.TPCannedReports.Controls.Add(Me.lblCannedPermitContactData)
        Me.TPCannedReports.Controls.Add(Me.btnRunCannedReport)
        Me.TPCannedReports.Location = New System.Drawing.Point(4, 22)
        Me.TPCannedReports.Name = "TPCannedReports"
        Me.TPCannedReports.Size = New System.Drawing.Size(731, 363)
        Me.TPCannedReports.TabIndex = 6
        Me.TPCannedReports.Text = "Canned Reports"
        Me.TPCannedReports.UseVisualStyleBackColor = True
        '
        'lblCannedQueryCount
        '
        Me.lblCannedQueryCount.AutoSize = True
        Me.lblCannedQueryCount.Location = New System.Drawing.Point(121, 123)
        Me.lblCannedQueryCount.Name = "lblCannedQueryCount"
        Me.lblCannedQueryCount.Size = New System.Drawing.Size(100, 13)
        Me.lblCannedQueryCount.TabIndex = 276
        Me.lblCannedQueryCount.Text = "99999 results found"
        '
        'cboCannedSelection
        '
        Me.cboCannedSelection.FormattingEnabled = True
        Me.cboCannedSelection.Location = New System.Drawing.Point(262, 68)
        Me.cboCannedSelection.Name = "cboCannedSelection"
        Me.cboCannedSelection.Size = New System.Drawing.Size(106, 21)
        Me.cboCannedSelection.TabIndex = 5
        '
        'lblCannedSelection
        '
        Me.lblCannedSelection.AutoSize = True
        Me.lblCannedSelection.Location = New System.Drawing.Point(201, 71)
        Me.lblCannedSelection.Name = "lblCannedSelection"
        Me.lblCannedSelection.Size = New System.Drawing.Size(32, 13)
        Me.lblCannedSelection.TabIndex = 101
        Me.lblCannedSelection.Text = "Class"
        '
        'lblCannedEndDate
        '
        Me.lblCannedEndDate.AutoSize = True
        Me.lblCannedEndDate.Location = New System.Drawing.Point(201, 45)
        Me.lblCannedEndDate.Name = "lblCannedEndDate"
        Me.lblCannedEndDate.Size = New System.Drawing.Size(52, 13)
        Me.lblCannedEndDate.TabIndex = 101
        Me.lblCannedEndDate.Text = "End Date"
        '
        'lblCannedStartDate
        '
        Me.lblCannedStartDate.AutoSize = True
        Me.lblCannedStartDate.Location = New System.Drawing.Point(201, 19)
        Me.lblCannedStartDate.Name = "lblCannedStartDate"
        Me.lblCannedStartDate.Size = New System.Drawing.Size(55, 13)
        Me.lblCannedStartDate.TabIndex = 101
        Me.lblCannedStartDate.Text = "Start Date"
        '
        'dtpCannedEndDate
        '
        Me.dtpCannedEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpCannedEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCannedEndDate.Location = New System.Drawing.Point(262, 42)
        Me.dtpCannedEndDate.Name = "dtpCannedEndDate"
        Me.dtpCannedEndDate.Size = New System.Drawing.Size(106, 20)
        Me.dtpCannedEndDate.TabIndex = 4
        Me.dtpCannedEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'dtpCannedStartDate
        '
        Me.dtpCannedStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpCannedStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCannedStartDate.Location = New System.Drawing.Point(262, 16)
        Me.dtpCannedStartDate.Name = "dtpCannedStartDate"
        Me.dtpCannedStartDate.Size = New System.Drawing.Size(106, 20)
        Me.dtpCannedStartDate.TabIndex = 3
        Me.dtpCannedStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'rdbCannedHistoryClass
        '
        Me.rdbCannedHistoryClass.AutoSize = True
        Me.rdbCannedHistoryClass.Checked = True
        Me.rdbCannedHistoryClass.Location = New System.Drawing.Point(8, 17)
        Me.rdbCannedHistoryClass.Name = "rdbCannedHistoryClass"
        Me.rdbCannedHistoryClass.Size = New System.Drawing.Size(129, 17)
        Me.rdbCannedHistoryClass.TabIndex = 0
        Me.rdbCannedHistoryClass.TabStop = True
        Me.rdbCannedHistoryClass.Text = "Historical Class Status"
        Me.rdbCannedHistoryClass.UseVisualStyleBackColor = True
        '
        'rdbCannedHistoryAirProgram
        '
        Me.rdbCannedHistoryAirProgram.AutoSize = True
        Me.rdbCannedHistoryAirProgram.Location = New System.Drawing.Point(8, 43)
        Me.rdbCannedHistoryAirProgram.Name = "rdbCannedHistoryAirProgram"
        Me.rdbCannedHistoryAirProgram.Size = New System.Drawing.Size(158, 17)
        Me.rdbCannedHistoryAirProgram.TabIndex = 1
        Me.rdbCannedHistoryAirProgram.Text = "Historical Air Program Status"
        Me.rdbCannedHistoryAirProgram.UseVisualStyleBackColor = True
        '
        'rdbCannedPermitContactData
        '
        Me.rdbCannedPermitContactData.AutoSize = True
        Me.rdbCannedPermitContactData.Location = New System.Drawing.Point(8, 69)
        Me.rdbCannedPermitContactData.Name = "rdbCannedPermitContactData"
        Me.rdbCannedPermitContactData.Size = New System.Drawing.Size(120, 17)
        Me.rdbCannedPermitContactData.TabIndex = 2
        Me.rdbCannedPermitContactData.Text = "Permit/Contact data"
        Me.rdbCannedPermitContactData.UseVisualStyleBackColor = True
        '
        'lblCannedHistoryAirProgram
        '
        Me.lblCannedHistoryAirProgram.AutoSize = True
        Me.lblCannedHistoryAirProgram.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCannedHistoryAirProgram.Location = New System.Drawing.Point(402, 19)
        Me.lblCannedHistoryAirProgram.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblCannedHistoryAirProgram.Name = "lblCannedHistoryAirProgram"
        Me.lblCannedHistoryAirProgram.Size = New System.Drawing.Size(249, 65)
        Me.lblCannedHistoryAirProgram.TabIndex = 1
        Me.lblCannedHistoryAirProgram.Text = "All facilities subject to the selected Air Program at any time during the selecte" &
    "d date range (inclusive). " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Does not include facilities that were shut down at" &
    " the time."
        Me.lblCannedHistoryAirProgram.Visible = False
        '
        'lblCannedHistoryClass
        '
        Me.lblCannedHistoryClass.AutoSize = True
        Me.lblCannedHistoryClass.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCannedHistoryClass.Location = New System.Drawing.Point(402, 19)
        Me.lblCannedHistoryClass.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblCannedHistoryClass.Name = "lblCannedHistoryClass"
        Me.lblCannedHistoryClass.Size = New System.Drawing.Size(241, 65)
        Me.lblCannedHistoryClass.TabIndex = 1
        Me.lblCannedHistoryClass.Text = "All facilities with the selected Class at any time during the selected date range" &
    " (inclusive). " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Does not include facilities that were shut down at the time."
        '
        'lblCannedPermitContactData
        '
        Me.lblCannedPermitContactData.AutoSize = True
        Me.lblCannedPermitContactData.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCannedPermitContactData.Location = New System.Drawing.Point(402, 19)
        Me.lblCannedPermitContactData.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblCannedPermitContactData.Name = "lblCannedPermitContactData"
        Me.lblCannedPermitContactData.Size = New System.Drawing.Size(236, 65)
        Me.lblCannedPermitContactData.TabIndex = 1
        Me.lblCannedPermitContactData.Text = "All facilities, with:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Fee contact if it exists, otherwise permit contact" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- La" &
    "st permit issued" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Warning: This report may take a long time to run." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblCannedPermitContactData.Visible = False
        '
        'btnRunCannedReport
        '
        Me.btnRunCannedReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunCannedReport.Location = New System.Drawing.Point(8, 115)
        Me.btnRunCannedReport.Name = "btnRunCannedReport"
        Me.btnRunCannedReport.Size = New System.Drawing.Size(107, 28)
        Me.btnRunCannedReport.TabIndex = 6
        Me.btnRunCannedReport.Text = "Run Report"
        Me.btnRunCannedReport.UseVisualStyleBackColor = True
        '
        'BasicSearchGroup
        '
        Me.BasicSearchGroup.Controls.Add(Me.Label7)
        Me.BasicSearchGroup.Controls.Add(Me.Label6)
        Me.BasicSearchGroup.Controls.Add(Me.lblQueryCount)
        Me.BasicSearchGroup.Controls.Add(Me.btnReset)
        Me.BasicSearchGroup.Controls.Add(Me.btnRunSearch)
        Me.BasicSearchGroup.Controls.Add(Me.Label2)
        Me.BasicSearchGroup.Controls.Add(Me.Panel6)
        Me.BasicSearchGroup.Controls.Add(Me.Label3)
        Me.BasicSearchGroup.Controls.Add(Me.Panel35)
        Me.BasicSearchGroup.Controls.Add(Me.txtFacilityNameSearch2)
        Me.BasicSearchGroup.Controls.Add(Me.txtFacilityNameSearch1)
        Me.BasicSearchGroup.Controls.Add(Me.txtAIRSNumberSearch1)
        Me.BasicSearchGroup.Controls.Add(Me.Panel36)
        Me.BasicSearchGroup.Controls.Add(Me.txtAIRSNumberSearch2)
        Me.BasicSearchGroup.Controls.Add(Me.Panel37)
        Me.BasicSearchGroup.Dock = System.Windows.Forms.DockStyle.Top
        Me.BasicSearchGroup.Location = New System.Drawing.Point(0, 49)
        Me.BasicSearchGroup.Name = "BasicSearchGroup"
        Me.BasicSearchGroup.Size = New System.Drawing.Size(739, 96)
        Me.BasicSearchGroup.TabIndex = 0
        Me.BasicSearchGroup.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 276
        Me.Label7.Text = "Facility Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 276
        Me.Label6.Text = "AIRS Number"
        '
        'bgwQueryGenerator
        '
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveSearchQueryToolStripMenuItem, Me.OpenSavedSearchToolStripMenuItem, Me.ToolStripSeparator1, Me.mmiExport})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'SaveSearchQueryToolStripMenuItem
        '
        Me.SaveSearchQueryToolStripMenuItem.Name = "SaveSearchQueryToolStripMenuItem"
        Me.SaveSearchQueryToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.SaveSearchQueryToolStripMenuItem.Text = "&Save Search Query"
        '
        'OpenSavedSearchToolStripMenuItem
        '
        Me.OpenSavedSearchToolStripMenuItem.Name = "OpenSavedSearchToolStripMenuItem"
        Me.OpenSavedSearchToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.OpenSavedSearchToolStripMenuItem.Text = "&Open Saved Query"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(169, 6)
        '
        'mmiExport
        '
        Me.mmiExport.Name = "mmiExport"
        Me.mmiExport.Size = New System.Drawing.Size(172, 22)
        Me.mmiExport.Text = "&Export to Excel"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(739, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'IAIPQueryGenerator
        '
        Me.AcceptButton = Me.btnRunSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 661)
        Me.Controls.Add(Me.dgvQueryGenerator)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.tcQueryOptions)
        Me.Controls.Add(Me.BasicSearchGroup)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(755, 700)
        Me.Name = "IAIPQueryGenerator"
        Me.Text = "Query Generator"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel35.ResumeLayout(False)
        Me.Panel35.PerformLayout()
        Me.Panel36.ResumeLayout(False)
        Me.Panel36.PerformLayout()
        Me.Panel37.ResumeLayout(False)
        Me.Panel37.PerformLayout()
        CType(Me.dgvQueryGenerator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcQueryOptions.ResumeLayout(False)
        Me.TPPhysicalLocation.ResumeLayout(False)
        Me.TPPhysicalLocation.PerformLayout()
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        Me.Panel29.ResumeLayout(False)
        Me.Panel29.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TPHeaderInformation.ResumeLayout(False)
        Me.TPHeaderInformation.PerformLayout()
        Me.Panel74.ResumeLayout(False)
        Me.Panel74.PerformLayout()
        Me.Panel75.ResumeLayout(False)
        Me.Panel75.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel33.ResumeLayout(False)
        Me.Panel33.PerformLayout()
        Me.Panel34.ResumeLayout(False)
        Me.Panel34.PerformLayout()
        Me.Panel31.ResumeLayout(False)
        Me.Panel31.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.Panel22.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.TPHeaderInformation2.ResumeLayout(False)
        Me.TPHeaderInformation2.PerformLayout()
        Me.Panel63.ResumeLayout(False)
        Me.Panel63.PerformLayout()
        Me.Panel62.ResumeLayout(False)
        Me.Panel62.PerformLayout()
        Me.Panel61.ResumeLayout(False)
        Me.Panel61.PerformLayout()
        Me.Panel60.ResumeLayout(False)
        Me.Panel60.PerformLayout()
        Me.Panel59.ResumeLayout(False)
        Me.Panel59.PerformLayout()
        Me.Panel58.ResumeLayout(False)
        Me.Panel58.PerformLayout()
        Me.Panel57.ResumeLayout(False)
        Me.Panel57.PerformLayout()
        Me.Panel56.ResumeLayout(False)
        Me.Panel56.PerformLayout()
        Me.Panel55.ResumeLayout(False)
        Me.Panel55.PerformLayout()
        Me.Panel54.ResumeLayout(False)
        Me.Panel54.PerformLayout()
        Me.Panel53.ResumeLayout(False)
        Me.Panel53.PerformLayout()
        Me.Panel52.ResumeLayout(False)
        Me.Panel52.PerformLayout()
        Me.Panel51.ResumeLayout(False)
        Me.Panel51.PerformLayout()
        Me.TPAirProgramCodes.ResumeLayout(False)
        Me.TPAirProgramCodes.PerformLayout()
        Me.Panel70.ResumeLayout(False)
        Me.Panel70.PerformLayout()
        Me.Panel50.ResumeLayout(False)
        Me.Panel50.PerformLayout()
        Me.Panel38.ResumeLayout(False)
        Me.Panel38.PerformLayout()
        Me.Panel49.ResumeLayout(False)
        Me.Panel49.PerformLayout()
        Me.Panel39.ResumeLayout(False)
        Me.Panel39.PerformLayout()
        Me.Panel48.ResumeLayout(False)
        Me.Panel48.PerformLayout()
        Me.Panel47.ResumeLayout(False)
        Me.Panel47.PerformLayout()
        Me.Panel40.ResumeLayout(False)
        Me.Panel40.PerformLayout()
        Me.Panel46.ResumeLayout(False)
        Me.Panel46.PerformLayout()
        Me.Panel41.ResumeLayout(False)
        Me.Panel41.PerformLayout()
        Me.Panel45.ResumeLayout(False)
        Me.Panel45.PerformLayout()
        Me.Panel44.ResumeLayout(False)
        Me.Panel44.PerformLayout()
        Me.Panel43.ResumeLayout(False)
        Me.Panel43.PerformLayout()
        Me.Panel42.ResumeLayout(False)
        Me.Panel42.PerformLayout()
        Me.TPSubpartData.ResumeLayout(False)
        Me.TPSubpartData.PerformLayout()
        Me.TPComplianceInfo.ResumeLayout(False)
        Me.TPComplianceInfo.PerformLayout()
        Me.Panel76.ResumeLayout(False)
        Me.Panel76.PerformLayout()
        Me.Panel72.ResumeLayout(False)
        Me.Panel72.PerformLayout()
        Me.Panel69.ResumeLayout(False)
        Me.Panel69.PerformLayout()
        Me.TPCannedReports.ResumeLayout(False)
        Me.TPCannedReports.PerformLayout()
        Me.BasicSearchGroup.ResumeLayout(False)
        Me.BasicSearchGroup.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSearchQuery As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveQuery As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityNameNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityNameEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityNameOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityNameAnd As System.Windows.Forms.RadioButton
    Friend WithEvents txtFacilityNameSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityNameSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel36 As System.Windows.Forms.Panel
    Friend WithEvents rdbAIRSNumberNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAIRSNumberEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel37 As System.Windows.Forms.Panel
    Friend WithEvents rdbAIRSNumberOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAIRSNumberAnd As System.Windows.Forms.RadioButton
    Friend WithEvents txtAIRSNumberSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumberSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnRunSearch As System.Windows.Forms.Button
    Friend WithEvents BasicSearchGroup As System.Windows.Forms.GroupBox
    Friend WithEvents dgvQueryGenerator As IaipDataGridView
    Friend WithEvents tcQueryOptions As System.Windows.Forms.TabControl
    Friend WithEvents TPPhysicalLocation As System.Windows.Forms.TabPage
    Friend WithEvents TPHeaderInformation As System.Windows.Forms.TabPage
    Friend WithEvents TPAirProgramCodes As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents rdbCountyNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCountyEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents rdbDistrictNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDistrictEqual As System.Windows.Forms.RadioButton
    Friend WithEvents cboDistrictSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboDistrictSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCountySearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCountySearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents chbDistrict As System.Windows.Forms.CheckBox
    Friend WithEvents chbCounty As System.Windows.Forms.CheckBox
    Friend WithEvents txtFacilityLatitudeSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityLatitudeSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityLongitudeSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityLongitudeSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityZipCodeNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityZipCodeEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityZipCodeOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityZipCodeAnd As System.Windows.Forms.RadioButton
    Friend WithEvents txtFacilityZipCodeSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityZipCodeSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityCityNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityCityEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityCityOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityCityAnd As System.Windows.Forms.RadioButton
    Friend WithEvents txtFacilityCitySearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityCitySearch1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityStreet2NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityStreet2Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityStreet2Or As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityStreet2And As System.Windows.Forms.RadioButton
    Friend WithEvents txtFacilityStreet2Search2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityStreet2Search1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityStreet1NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityStreet1Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdbFacilityStreet1Or As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityStreet1And As System.Windows.Forms.RadioButton
    Friend WithEvents txtFacilityStreet1Search2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityStreet1Search1 As System.Windows.Forms.TextBox
    Friend WithEvents chbFacilityLatitude As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacilityLongitude As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacilityZipCode As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacilityCity As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacilityStreet2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacilityStreet1 As System.Windows.Forms.CheckBox
    Friend WithEvents cboCMSUniverseSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCMSUniverseSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel33 As System.Windows.Forms.Panel
    Friend WithEvents rdbPlantDescriptionNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPlantDescriptionEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents rdbPlantDescriptionOR As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPlantDescriptionAND As System.Windows.Forms.RadioButton
    Friend WithEvents txtPlantDescriptionSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPlantDescriptionSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents chbPlantDescription As System.Windows.Forms.CheckBox
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents rdbCMSUniverseNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCMSUniverseEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbCMSUniverse As System.Windows.Forms.CheckBox
    Friend WithEvents DTPShutDownDateSearch2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPShutDownDateSearch1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPStartUpDateSearch2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPStartUpDateSearch1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents rdbSICCodeNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSICCodeEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents rdbSICCodeOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSICCodeAnd As System.Windows.Forms.RadioButton
    Friend WithEvents txtSICCodeSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSICCodeSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents cboClassificationSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboClassificationSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents rdbClassificationNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbClassificationEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbShutDownDate As System.Windows.Forms.CheckBox
    Friend WithEvents chbStartUpDate As System.Windows.Forms.CheckBox
    Friend WithEvents chbSICCode As System.Windows.Forms.CheckBox
    Friend WithEvents chbClassification As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC0 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel50 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPCVNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPCVEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbAPC1 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel38 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC0NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC0Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel49 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPCMNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPCMEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel39 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC1NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC1Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel48 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPCINotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPCIEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbAPCV As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCM As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCI As System.Windows.Forms.CheckBox
    Friend WithEvents Panel47 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPCFNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPCFEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbAPC4 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel40 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC3NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC3Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel46 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPCANotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPCAEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel41 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC4NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC4Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel45 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC9NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC9Equal As System.Windows.Forms.RadioButton
    Friend WithEvents chbAPCF As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC6 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC8NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC8Equal As System.Windows.Forms.RadioButton
    Friend WithEvents chbAPCA As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC8 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel43 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC7NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC7Equal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel42 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPC6NotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPC6Equal As System.Windows.Forms.RadioButton
    Friend WithEvents chbAPC9 As System.Windows.Forms.CheckBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents cboOperationStatusSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboOperationStatusSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents rdbOperationStatusNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOperationStatusEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbOperationStatus As System.Windows.Forms.CheckBox
    Friend WithEvents tsbReSizeFilterOptions As System.Windows.Forms.ToolStripButton
    Friend WithEvents chbViewAirPrograms As System.Windows.Forms.CheckBox
    Friend WithEvents TPHeaderInformation2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel51 As System.Windows.Forms.Panel
    Friend WithEvents rdb1HrYesNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdb1HrYesEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbPMNo As System.Windows.Forms.CheckBox
    Friend WithEvents chbPMMacon As System.Windows.Forms.CheckBox
    Friend WithEvents chbPMFloyd As System.Windows.Forms.CheckBox
    Friend WithEvents chbPMChattanooga As System.Windows.Forms.CheckBox
    Friend WithEvents chbPMAtlanta As System.Windows.Forms.CheckBox
    Friend WithEvents chb8HrNo As System.Windows.Forms.CheckBox
    Friend WithEvents chb8HrMacon As System.Windows.Forms.CheckBox
    Friend WithEvents chb8HrAtlanta As System.Windows.Forms.CheckBox
    Friend WithEvents chb1HrContribute As System.Windows.Forms.CheckBox
    Friend WithEvents chb1HrNo As System.Windows.Forms.CheckBox
    Friend WithEvents chb1HrYes As System.Windows.Forms.CheckBox
    Friend WithEvents chbHAPMajor As System.Windows.Forms.CheckBox
    Friend WithEvents chbNSRPSDMajor As System.Windows.Forms.CheckBox
    Friend WithEvents Panel63 As System.Windows.Forms.Panel
    Friend WithEvents rdbHAPMajorNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbHAPMajorEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel62 As System.Windows.Forms.Panel
    Friend WithEvents rdbNSRPSDMajorNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNSRPSDMajorEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel61 As System.Windows.Forms.Panel
    Friend WithEvents rdbPMNoNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMNoEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel60 As System.Windows.Forms.Panel
    Friend WithEvents rdbPMMaconNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMMaconEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel59 As System.Windows.Forms.Panel
    Friend WithEvents rdbPMFloydNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMFloydEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel58 As System.Windows.Forms.Panel
    Friend WithEvents rdbPMChattanoogaNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMChattanoogaEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel57 As System.Windows.Forms.Panel
    Friend WithEvents rdbPMAtlantaNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMAtlantaEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel56 As System.Windows.Forms.Panel
    Friend WithEvents rdb8HrNoNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdb8HrNoEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel55 As System.Windows.Forms.Panel
    Friend WithEvents rdb8HrMaconNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdb8HrMaconEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel54 As System.Windows.Forms.Panel
    Friend WithEvents rdb8HrAtlantaNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdb8HrAtlantaEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel53 As System.Windows.Forms.Panel
    Friend WithEvents rdb1HrContributeNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdb1HrContributeEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel52 As System.Windows.Forms.Panel
    Friend WithEvents rdb1HrNoNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdb1HrNoEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbAttainmentStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chbStateProgramCodes As System.Windows.Forms.CheckBox
    Friend WithEvents TPSubpartData As System.Windows.Forms.TabPage
    Friend WithEvents chbAllSubparts As System.Windows.Forms.CheckBox
    Friend WithEvents chbPart61Subpart As System.Windows.Forms.CheckBox
    Friend WithEvents chbPart60Subpart As System.Windows.Forms.CheckBox
    Friend WithEvents chbPart63Subpart As System.Windows.Forms.CheckBox
    Friend WithEvents chbSIP As System.Windows.Forms.CheckBox
    Friend WithEvents Panel70 As System.Windows.Forms.Panel
    Friend WithEvents rdbAPCOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAPCAnd As System.Windows.Forms.RadioButton
    Friend WithEvents cboSIPSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSIPSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents bgwQueryGenerator As System.ComponentModel.BackgroundWorker
    Friend WithEvents cboPart63Search2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboPart60Search2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboPart63Search1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboPart60Search1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboPart61Search2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboPart61Search1 As System.Windows.Forms.ComboBox
    Friend WithEvents TPComplianceInfo As System.Windows.Forms.TabPage
    Friend WithEvents cboSSCPUnitSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSSCPUnitSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel72 As System.Windows.Forms.Panel
    Friend WithEvents rdbSSCPUnitNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSSCPUnitEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbSSCPUnit As System.Windows.Forms.CheckBox
    Friend WithEvents cboSSCPEngineerSearch2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSSCPEngineerSearch1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel69 As System.Windows.Forms.Panel
    Friend WithEvents rdbSSCPEngineerNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSSCPEngineerEqual As System.Windows.Forms.RadioButton
    Friend WithEvents chbSSCPEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents DTPLastFCESearch2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPLastFCESearch1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbLastFCE As System.Windows.Forms.CheckBox
    Friend WithEvents Panel74 As System.Windows.Forms.Panel
    Friend WithEvents rdbNAICSCodeNotEqual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNAICSCodeEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Panel75 As System.Windows.Forms.Panel
    Friend WithEvents rdbNAICSCodeOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNAICSCodeAnd As System.Windows.Forms.RadioButton
    Friend WithEvents txtNAICSCodeSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNAICSCodeSearch1 As System.Windows.Forms.TextBox
    Friend WithEvents chbNAICSCode As System.Windows.Forms.CheckBox
    Friend WithEvents TPCannedReports As System.Windows.Forms.TabPage
    Friend WithEvents lblCannedPermitContactData As System.Windows.Forms.Label
    Friend WithEvents btnRunCannedReport As System.Windows.Forms.Button
    Friend WithEvents Panel76 As System.Windows.Forms.Panel
    Friend WithEvents rdbDistrictResponsibleFalse As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDistrictResponsibleTrue As System.Windows.Forms.RadioButton
    Friend WithEvents chbDistrictResponsible As System.Windows.Forms.CheckBox
    Friend WithEvents lblQueryCount As System.Windows.Forms.Label
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveSearchQueryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenSavedSearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mmiExport As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents rdbCannedHistoryClass As RadioButton
    Friend WithEvents rdbCannedHistoryAirProgram As RadioButton
    Friend WithEvents rdbCannedPermitContactData As RadioButton
    Friend WithEvents cboCannedSelection As ComboBox
    Friend WithEvents lblCannedSelection As Label
    Friend WithEvents lblCannedEndDate As Label
    Friend WithEvents lblCannedStartDate As Label
    Friend WithEvents dtpCannedEndDate As DateTimePicker
    Friend WithEvents dtpCannedStartDate As DateTimePicker
    Friend WithEvents lblCannedHistoryAirProgram As Label
    Friend WithEvents lblCannedHistoryClass As Label
    Friend WithEvents lblCannedQueryCount As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
