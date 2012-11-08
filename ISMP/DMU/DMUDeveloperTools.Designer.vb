<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUDeveloperTools
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DMUDeveloperTools))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.bgwTransfer = New System.ComponentModel.BackgroundWorker
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TCDMUTools = New System.Windows.Forms.TabControl
        Me.TPWebErrorLog = New System.Windows.Forms.TabPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label95 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.txtWebErrorNumber = New System.Windows.Forms.TextBox
        Me.txtIPAddress = New System.Windows.Forms.TextBox
        Me.btnSaveWebErrorSolution = New System.Windows.Forms.Button
        Me.txtWebErrorCount = New System.Windows.Forms.TextBox
        Me.Label91 = New System.Windows.Forms.Label
        Me.btnFilterWebErrors = New System.Windows.Forms.Button
        Me.Label90 = New System.Windows.Forms.Label
        Me.rdbResolvedWebErrors = New System.Windows.Forms.RadioButton
        Me.Label88 = New System.Windows.Forms.Label
        Me.rdbUnresolvedWebErrors = New System.Windows.Forms.RadioButton
        Me.Label71 = New System.Windows.Forms.Label
        Me.rdbAllWebErrors = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtWebErrorSolution = New System.Windows.Forms.TextBox
        Me.txtWebErrorUser = New System.Windows.Forms.TextBox
        Me.txtWebErrorMessage = New System.Windows.Forms.TextBox
        Me.txtWebErrorLocation = New System.Windows.Forms.TextBox
        Me.txtWebErrorDate = New System.Windows.Forms.TextBox
        Me.dgrWebErrorList = New System.Windows.Forms.DataGrid
        Me.TPErrorLog = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnExporttoExcel = New System.Windows.Forms.Button
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.rdbNoLimit = New System.Windows.Forms.RadioButton
        Me.rdbLast60days = New System.Windows.Forms.RadioButton
        Me.rdbLast30Days = New System.Windows.Forms.RadioButton
        Me.Label61 = New System.Windows.Forms.Label
        Me.txtErrorCount = New System.Windows.Forms.TextBox
        Me.txtErrorNumber = New System.Windows.Forms.TextBox
        Me.btnFilterErrors = New System.Windows.Forms.Button
        Me.btnSaveError = New System.Windows.Forms.Button
        Me.rdbViewResolvedErrors = New System.Windows.Forms.RadioButton
        Me.Label62 = New System.Windows.Forms.Label
        Me.rdbViewUnresolvedErrors = New System.Windows.Forms.RadioButton
        Me.Label64 = New System.Windows.Forms.Label
        Me.rdbViewAllErrors = New System.Windows.Forms.RadioButton
        Me.Label65 = New System.Windows.Forms.Label
        Me.txtErrorSolution = New System.Windows.Forms.TextBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.txtErrorMessage = New System.Windows.Forms.TextBox
        Me.Label67 = New System.Windows.Forms.Label
        Me.txtErrorDate = New System.Windows.Forms.TextBox
        Me.txtErrorUser = New System.Windows.Forms.TextBox
        Me.txtErrorLocation = New System.Windows.Forms.TextBox
        Me.dgvErrorList = New System.Windows.Forms.DataGridView
        Me.TPAddNewFacility = New System.Windows.Forms.TabPage
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.btnPreLoadNewFacility = New System.Windows.Forms.Button
        Me.btnDeleteAIRSNumber = New System.Windows.Forms.Button
        Me.txtDeleteAIRSNumber = New System.Windows.Forms.TextBox
        Me.btnClearAddNewFacility = New System.Windows.Forms.Button
        Me.GBContactInformation = New System.Windows.Forms.GroupBox
        Me.mtbContactNumberExtension = New System.Windows.Forms.MaskedTextBox
        Me.txtContactPedigree = New System.Windows.Forms.TextBox
        Me.txtContactSocialTitle = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtContactLastName = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtContactFirstName = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.mtbContactPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtContactTitle = New System.Windows.Forms.TextBox
        Me.GBAirProgramCodes = New System.Windows.Forms.GroupBox
        Me.chbCDS_14 = New System.Windows.Forms.CheckBox
        Me.chbCDS_7 = New System.Windows.Forms.CheckBox
        Me.chbCDS_4 = New System.Windows.Forms.CheckBox
        Me.chbCDS_13 = New System.Windows.Forms.CheckBox
        Me.chbCDS_3 = New System.Windows.Forms.CheckBox
        Me.chbCDS_12 = New System.Windows.Forms.CheckBox
        Me.chbCDS_9 = New System.Windows.Forms.CheckBox
        Me.chbCDS_10 = New System.Windows.Forms.CheckBox
        Me.chbCDS_2 = New System.Windows.Forms.CheckBox
        Me.chbCDS_6 = New System.Windows.Forms.CheckBox
        Me.chbCDS_1 = New System.Windows.Forms.CheckBox
        Me.chbCDS_5 = New System.Windows.Forms.CheckBox
        Me.chbCDS_11 = New System.Windows.Forms.CheckBox
        Me.chbCDS_8 = New System.Windows.Forms.CheckBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.GBHeaderData = New System.Windows.Forms.GroupBox
        Me.mtbCDSSICCode = New System.Windows.Forms.MaskedTextBox
        Me.txtCDSRegionCode = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.cboCDSOperationalStatus = New System.Windows.Forms.ComboBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.cboCDSClassCode = New System.Windows.Forms.ComboBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.txtFacilityDescription = New System.Windows.Forms.TextBox
        Me.GBMailingLocation = New System.Windows.Forms.GroupBox
        Me.mtbMailingZipCode = New System.Windows.Forms.MaskedTextBox
        Me.txtMailingState = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtMailingCity = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtMailingAddress = New System.Windows.Forms.TextBox
        Me.GBFacilityInformation = New System.Windows.Forms.GroupBox
        Me.mtbFacilityLongitude = New System.Windows.Forms.MaskedTextBox
        Me.mtbFacilityLatitude = New System.Windows.Forms.MaskedTextBox
        Me.mtbCDSZipCode = New System.Windows.Forms.MaskedTextBox
        Me.Label103 = New System.Windows.Forms.Label
        Me.Label102 = New System.Windows.Forms.Label
        Me.txtCDSStreetAddress = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtCDSFacilityName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCDSCity = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCDSState = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.llbContactInformation = New System.Windows.Forms.LinkLabel
        Me.llbAirProgramCodes = New System.Windows.Forms.LinkLabel
        Me.llbHeaderData = New System.Windows.Forms.LinkLabel
        Me.llbMailingLocation = New System.Windows.Forms.LinkLabel
        Me.llbFacilityInformation = New System.Windows.Forms.LinkLabel
        Me.btnNewFacility = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCDSAIRSNumber = New System.Windows.Forms.TextBox
        Me.TPAFSFileGenerator = New System.Windows.Forms.TabPage
        Me.txtAFSBatchFile = New System.Windows.Forms.TextBox
        Me.PanelBatchOrder = New System.Windows.Forms.Panel
        Me.btnUpdateAllSubParts = New System.Windows.Forms.Button
        Me.btnForceBasicRefresh = New System.Windows.Forms.Button
        Me.btnClearAFSFileGenerator = New System.Windows.Forms.Button
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.Label59 = New System.Windows.Forms.Label
        Me.btnGenerateBatchFile = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.TPUpdateDEVTest = New System.Windows.Forms.TabPage
        Me.TCTables = New System.Windows.Forms.TabControl
        Me.TPAllTables = New System.Windows.Forms.TabPage
        Me.pnlMiscTables = New System.Windows.Forms.Panel
        Me.chbUpdateEIEU = New System.Windows.Forms.CheckBox
        Me.chbUpdateEIEP = New System.Windows.Forms.CheckBox
        Me.chbUpdateEIEM = New System.Windows.Forms.CheckBox
        Me.chbUpdateEIER = New System.Windows.Forms.CheckBox
        Me.chbUpDateEISI = New System.Windows.Forms.CheckBox
        Me.Label182 = New System.Windows.Forms.Label
        Me.pnlAFSTables = New System.Windows.Forms.Panel
        Me.chbAllAFSTables = New System.Windows.Forms.CheckBox
        Me.chbAFSSSPPRecords = New System.Windows.Forms.CheckBox
        Me.chbAFSSSCPRecords = New System.Windows.Forms.CheckBox
        Me.chbAFSAirPollutantData = New System.Windows.Forms.CheckBox
        Me.chbAFSBatchFiles = New System.Windows.Forms.CheckBox
        Me.chbAFSSSCPFCERecords = New System.Windows.Forms.CheckBox
        Me.chbAFSFacilityData = New System.Windows.Forms.CheckBox
        Me.chbAFSSSCPEnforcementRecords = New System.Windows.Forms.CheckBox
        Me.chbAFSISMPRecords = New System.Windows.Forms.CheckBox
        Me.pnlSSPPTables = New System.Windows.Forms.Panel
        Me.chbAllSSPPTables = New System.Windows.Forms.CheckBox
        Me.chbSSPPPublicLetters = New System.Windows.Forms.CheckBox
        Me.chbSSPPCDS = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationTracking = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationContact = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationData = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationQuality = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationInformation = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationMaster = New System.Windows.Forms.CheckBox
        Me.chbSSPPApplicationLinking = New System.Windows.Forms.CheckBox
        Me.pnlSSCPTables = New System.Windows.Forms.Panel
        Me.chbAllSSCPTables = New System.Windows.Forms.CheckBox
        Me.chbSSCPTestReports = New System.Windows.Forms.CheckBox
        Me.chbSSCPReportsHistory = New System.Windows.Forms.CheckBox
        Me.chbSSCPReports = New System.Windows.Forms.CheckBox
        Me.chbSSCPNotifications = New System.Windows.Forms.CheckBox
        Me.chbSSCPItemMaster = New System.Windows.Forms.CheckBox
        Me.chbSSCPInspectionTracking = New System.Windows.Forms.CheckBox
        Me.chbSSCPInspectionsRequired = New System.Windows.Forms.CheckBox
        Me.chbSSCPInspections = New System.Windows.Forms.CheckBox
        Me.chbSSCPInspectionActivity = New System.Windows.Forms.CheckBox
        Me.chbSSCPFCEMaster = New System.Windows.Forms.CheckBox
        Me.chbSSCPFCE = New System.Windows.Forms.CheckBox
        Me.chbSSCPFacilityAssignment = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcementStipulated = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcementNOVComments = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcementLetter = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcementItems = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcementCOComments = New System.Windows.Forms.CheckBox
        Me.chbSSCPACCS = New System.Windows.Forms.CheckBox
        Me.chbSSCPACCSHistory = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcementAOComments = New System.Windows.Forms.CheckBox
        Me.chbSSCPDistrictAssignment = New System.Windows.Forms.CheckBox
        Me.chbSSCPEnforcement = New System.Windows.Forms.CheckBox
        Me.chbSSCPDistrictResponsible = New System.Windows.Forms.CheckBox
        Me.pnlSBEAPTables = New System.Windows.Forms.Panel
        Me.chbAllSBEAPTables = New System.Windows.Forms.CheckBox
        Me.chbHBSBEAPClients = New System.Windows.Forms.CheckBox
        Me.chbHBSBEAPClientData = New System.Windows.Forms.CheckBox
        Me.chbSBEAPCaseLog = New System.Windows.Forms.CheckBox
        Me.chbSBEAPClientContacts = New System.Windows.Forms.CheckBox
        Me.chbSBEAPErrorLog = New System.Windows.Forms.CheckBox
        Me.chbSBEAPClientData = New System.Windows.Forms.CheckBox
        Me.chbSBEAPClients = New System.Windows.Forms.CheckBox
        Me.chbSBEAPClientLink = New System.Windows.Forms.CheckBox
        Me.pnlISMPTables = New System.Windows.Forms.Panel
        Me.chbAllISMPTables = New System.Windows.Forms.CheckBox
        Me.chbISMPWitnessingEng = New System.Windows.Forms.CheckBox
        Me.chbISMPTestReportMemo = New System.Windows.Forms.CheckBox
        Me.chbISMPTestReportAids = New System.Windows.Forms.CheckBox
        Me.chbISMPTestNotificationLog = New System.Windows.Forms.CheckBox
        Me.chbISMPTestNotification = New System.Windows.Forms.CheckBox
        Me.chbISMPTestLogNumber = New System.Windows.Forms.CheckBox
        Me.chbISMPTestLogLink = New System.Windows.Forms.CheckBox
        Me.chbISMPTestFirmComments = New System.Windows.Forms.CheckBox
        Me.chbISMPReportType = New System.Windows.Forms.CheckBox
        Me.chbISMPReportTwoStack = New System.Windows.Forms.CheckBox
        Me.chbISMPReportRATA = New System.Windows.Forms.CheckBox
        Me.chbISMPReportPondAndGas = New System.Windows.Forms.CheckBox
        Me.chbISMPReportOpacity = New System.Windows.Forms.CheckBox
        Me.chbISMPReportOneStack = New System.Windows.Forms.CheckBox
        Me.chbISMPReportMemo = New System.Windows.Forms.CheckBox
        Me.chbISMPDocumentTypes = New System.Windows.Forms.CheckBox
        Me.chbISMPFacilityAssignment = New System.Windows.Forms.CheckBox
        Me.chbISMPReportInformation = New System.Windows.Forms.CheckBox
        Me.chbISMPMaster = New System.Windows.Forms.CheckBox
        Me.chbISMPReportFlare = New System.Windows.Forms.CheckBox
        Me.chbISMPReferenceNumber = New System.Windows.Forms.CheckBox
        Me.pnlHeaderTables = New System.Windows.Forms.Panel
        Me.chbEPDUsers = New System.Windows.Forms.CheckBox
        Me.chbAllHeaderTables = New System.Windows.Forms.CheckBox
        Me.chbHBAPBHeaderData = New System.Windows.Forms.CheckBox
        Me.chbHBAPBFacilityInformation = New System.Windows.Forms.CheckBox
        Me.chbHBAPBAirProgramPollutants = New System.Windows.Forms.CheckBox
        Me.chbAPPMaster = New System.Windows.Forms.CheckBox
        Me.chbAPBSupplamentalData = New System.Windows.Forms.CheckBox
        Me.chbAPBSubPartData = New System.Windows.Forms.CheckBox
        Me.chbAPBPermits = New System.Windows.Forms.CheckBox
        Me.chbAPBMasterAPP = New System.Windows.Forms.CheckBox
        Me.chbAPBAirProgramPollutants = New System.Windows.Forms.CheckBox
        Me.chbAPBContactInformation = New System.Windows.Forms.CheckBox
        Me.chbAPBMasterAIRS = New System.Windows.Forms.CheckBox
        Me.chbAPBFacilityInformation = New System.Windows.Forms.CheckBox
        Me.chbAPBHeaderData = New System.Windows.Forms.CheckBox
        Me.pnlLookUpTables = New System.Windows.Forms.Panel
        Me.chbFSNSPSReason = New System.Windows.Forms.CheckBox
        Me.chbLookUpISMPMethods = New System.Windows.Forms.CheckBox
        Me.chbLookUpUnits = New System.Windows.Forms.CheckBox
        Me.chbLookUpTestingFirms = New System.Windows.Forms.CheckBox
        Me.chbLookUpSubPartSIP = New System.Windows.Forms.CheckBox
        Me.chbLookUpSubPart63 = New System.Windows.Forms.CheckBox
        Me.chbLookUpSubPart61 = New System.Windows.Forms.CheckBox
        Me.chbLookUpSubPart60 = New System.Windows.Forms.CheckBox
        Me.chbLookUpStates = New System.Windows.Forms.CheckBox
        Me.chbLookUpSSCPNotifications = New System.Windows.Forms.CheckBox
        Me.chbLookUpSICCodes = New System.Windows.Forms.CheckBox
        Me.chbLookUpSBEAPCaseWork = New System.Windows.Forms.CheckBox
        Me.chbLookUpPollutants = New System.Windows.Forms.CheckBox
        Me.chbLookUpPermitTypes = New System.Windows.Forms.CheckBox
        Me.chbLookUpPermittingUnits = New System.Windows.Forms.CheckBox
        Me.chbLookUpNonAttainment = New System.Windows.Forms.CheckBox
        Me.chbLookUpMonitoringUnits = New System.Windows.Forms.CheckBox
        Me.chbLookUpComplianceStatus = New System.Windows.Forms.CheckBox
        Me.chbLookUpComplianceUnits = New System.Windows.Forms.CheckBox
        Me.chbLookUpComplianceActivities = New System.Windows.Forms.CheckBox
        Me.chbLookUpCountyInformation = New System.Windows.Forms.CheckBox
        Me.chbLookUpApplicationType = New System.Windows.Forms.CheckBox
        Me.chbLookUpAPBManagement = New System.Windows.Forms.CheckBox
        Me.chbLookUpDistrictInformation = New System.Windows.Forms.CheckBox
        Me.chbLookUpDistrictOffice = New System.Windows.Forms.CheckBox
        Me.chbLookUpDistricts = New System.Windows.Forms.CheckBox
        Me.chbLookUpEPDBranches = New System.Windows.Forms.CheckBox
        Me.chbLookUpEPDPrograms = New System.Windows.Forms.CheckBox
        Me.chbLookUpEPDUnits = New System.Windows.Forms.CheckBox
        Me.chbLookUpHPVViolations = New System.Windows.Forms.CheckBox
        Me.chbLookUpIAIPAccounts = New System.Windows.Forms.CheckBox
        Me.chbLookUpIAIPForms = New System.Windows.Forms.CheckBox
        Me.chbLookUpISMPComplianceStatus = New System.Windows.Forms.CheckBox
        Me.chbAllLookUpTables = New System.Windows.Forms.CheckBox
        Me.Panel19 = New System.Windows.Forms.Panel
        Me.chbAllTables = New System.Windows.Forms.CheckBox
        Me.PanelSources = New System.Windows.Forms.Panel
        Me.lblTransfer = New System.Windows.Forms.Label
        Me.btnClearSelection = New System.Windows.Forms.Button
        Me.btnTransferData = New System.Windows.Forms.Button
        Me.Panel20 = New System.Windows.Forms.Panel
        Me.rdbTESTTransfer = New System.Windows.Forms.RadioButton
        Me.rdbDEVTransfer = New System.Windows.Forms.RadioButton
        Me.TSDMUStaffTools = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1.SuspendLayout()
        Me.TCDMUTools.SuspendLayout()
        Me.TPWebErrorLog.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgrWebErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPErrorLog.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.dgvErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPAddNewFacility.SuspendLayout()
        Me.GBContactInformation.SuspendLayout()
        Me.GBAirProgramCodes.SuspendLayout()
        Me.GBHeaderData.SuspendLayout()
        Me.GBMailingLocation.SuspendLayout()
        Me.GBFacilityInformation.SuspendLayout()
        Me.TPAFSFileGenerator.SuspendLayout()
        Me.PanelBatchOrder.SuspendLayout()
        Me.TPUpdateDEVTest.SuspendLayout()
        Me.TCTables.SuspendLayout()
        Me.TPAllTables.SuspendLayout()
        Me.pnlMiscTables.SuspendLayout()
        Me.pnlAFSTables.SuspendLayout()
        Me.pnlSSPPTables.SuspendLayout()
        Me.pnlSSCPTables.SuspendLayout()
        Me.pnlSBEAPTables.SuspendLayout()
        Me.pnlISMPTables.SuspendLayout()
        Me.pnlHeaderTables.SuspendLayout()
        Me.pnlLookUpTables.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.PanelSources.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.TSDMUStaffTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 665)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 254
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(769, 17)
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
        'bgwTransfer
        '
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
        'TCDMUTools
        '
        Me.TCDMUTools.Controls.Add(Me.TPWebErrorLog)
        Me.TCDMUTools.Controls.Add(Me.TPErrorLog)
        Me.TCDMUTools.Controls.Add(Me.TPAddNewFacility)
        Me.TCDMUTools.Controls.Add(Me.TPAFSFileGenerator)
        Me.TCDMUTools.Controls.Add(Me.TPUpdateDEVTest)
        Me.TCDMUTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDMUTools.Location = New System.Drawing.Point(0, 25)
        Me.TCDMUTools.Name = "TCDMUTools"
        Me.TCDMUTools.SelectedIndex = 0
        Me.TCDMUTools.Size = New System.Drawing.Size(792, 640)
        Me.TCDMUTools.TabIndex = 256
        '
        'TPWebErrorLog
        '
        Me.TPWebErrorLog.Controls.Add(Me.GroupBox4)
        Me.TPWebErrorLog.Controls.Add(Me.dgrWebErrorList)
        Me.TPWebErrorLog.Location = New System.Drawing.Point(4, 22)
        Me.TPWebErrorLog.Name = "TPWebErrorLog"
        Me.TPWebErrorLog.Size = New System.Drawing.Size(784, 614)
        Me.TPWebErrorLog.TabIndex = 8
        Me.TPWebErrorLog.Text = "Web Site Error Log"
        Me.TPWebErrorLog.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Panel5)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 241)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(784, 373)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Web Error Log"
        '
        'Panel5
        '
        Me.Panel5.AutoScroll = True
        Me.Panel5.Controls.Add(Me.Label95)
        Me.Panel5.Controls.Add(Me.Label96)
        Me.Panel5.Controls.Add(Me.txtWebErrorNumber)
        Me.Panel5.Controls.Add(Me.txtIPAddress)
        Me.Panel5.Controls.Add(Me.btnSaveWebErrorSolution)
        Me.Panel5.Controls.Add(Me.txtWebErrorCount)
        Me.Panel5.Controls.Add(Me.Label91)
        Me.Panel5.Controls.Add(Me.btnFilterWebErrors)
        Me.Panel5.Controls.Add(Me.Label90)
        Me.Panel5.Controls.Add(Me.rdbResolvedWebErrors)
        Me.Panel5.Controls.Add(Me.Label88)
        Me.Panel5.Controls.Add(Me.rdbUnresolvedWebErrors)
        Me.Panel5.Controls.Add(Me.Label71)
        Me.Panel5.Controls.Add(Me.rdbAllWebErrors)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.txtWebErrorSolution)
        Me.Panel5.Controls.Add(Me.txtWebErrorUser)
        Me.Panel5.Controls.Add(Me.txtWebErrorMessage)
        Me.Panel5.Controls.Add(Me.txtWebErrorLocation)
        Me.Panel5.Controls.Add(Me.txtWebErrorDate)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(778, 354)
        Me.Panel5.TabIndex = 20
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(8, 11)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(69, 13)
        Me.Label95.TabIndex = 0
        Me.Label95.Text = "Error Number"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(368, 11)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(58, 13)
        Me.Label96.TabIndex = 19
        Me.Label96.Text = "IP Address"
        '
        'txtWebErrorNumber
        '
        Me.txtWebErrorNumber.Location = New System.Drawing.Point(88, 9)
        Me.txtWebErrorNumber.Name = "txtWebErrorNumber"
        Me.txtWebErrorNumber.ReadOnly = True
        Me.txtWebErrorNumber.Size = New System.Drawing.Size(60, 20)
        Me.txtWebErrorNumber.TabIndex = 1
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(441, 9)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.ReadOnly = True
        Me.txtIPAddress.Size = New System.Drawing.Size(140, 20)
        Me.txtIPAddress.TabIndex = 18
        '
        'btnSaveWebErrorSolution
        '
        Me.btnSaveWebErrorSolution.Location = New System.Drawing.Point(8, 191)
        Me.btnSaveWebErrorSolution.Name = "btnSaveWebErrorSolution"
        Me.btnSaveWebErrorSolution.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveWebErrorSolution.TabIndex = 2
        Me.btnSaveWebErrorSolution.Text = "Save"
        '
        'txtWebErrorCount
        '
        Me.txtWebErrorCount.Location = New System.Drawing.Point(614, 7)
        Me.txtWebErrorCount.Name = "txtWebErrorCount"
        Me.txtWebErrorCount.ReadOnly = True
        Me.txtWebErrorCount.Size = New System.Drawing.Size(34, 20)
        Me.txtWebErrorCount.TabIndex = 17
        Me.txtWebErrorCount.Text = "0"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Location = New System.Drawing.Point(168, 11)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(29, 13)
        Me.Label91.TabIndex = 3
        Me.Label91.Text = "User"
        '
        'btnFilterWebErrors
        '
        Me.btnFilterWebErrors.Location = New System.Drawing.Point(154, 240)
        Me.btnFilterWebErrors.Name = "btnFilterWebErrors"
        Me.btnFilterWebErrors.Size = New System.Drawing.Size(63, 20)
        Me.btnFilterWebErrors.TabIndex = 16
        Me.btnFilterWebErrors.Text = "Filter"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(8, 33)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(76, 13)
        Me.Label90.TabIndex = 4
        Me.Label90.Text = "Error Location "
        '
        'rdbResolvedWebErrors
        '
        Me.rdbResolvedWebErrors.AutoSize = True
        Me.rdbResolvedWebErrors.Location = New System.Drawing.Point(14, 281)
        Me.rdbResolvedWebErrors.Name = "rdbResolvedWebErrors"
        Me.rdbResolvedWebErrors.Size = New System.Drawing.Size(121, 17)
        Me.rdbResolvedWebErrors.TabIndex = 15
        Me.rdbResolvedWebErrors.Text = "View Resolved Error"
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(8, 59)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(75, 13)
        Me.Label88.TabIndex = 5
        Me.Label88.Text = "Error Message"
        '
        'rdbUnresolvedWebErrors
        '
        Me.rdbUnresolvedWebErrors.AutoSize = True
        Me.rdbUnresolvedWebErrors.Location = New System.Drawing.Point(14, 260)
        Me.rdbUnresolvedWebErrors.Name = "rdbUnresolvedWebErrors"
        Me.rdbUnresolvedWebErrors.Size = New System.Drawing.Size(135, 17)
        Me.rdbUnresolvedWebErrors.TabIndex = 14
        Me.rdbUnresolvedWebErrors.Text = "View Unresolved Errors"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(8, 170)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(73, 13)
        Me.Label71.TabIndex = 6
        Me.Label71.Text = "Error Solution "
        '
        'rdbAllWebErrors
        '
        Me.rdbAllWebErrors.AutoSize = True
        Me.rdbAllWebErrors.Location = New System.Drawing.Point(14, 240)
        Me.rdbAllWebErrors.Name = "rdbAllWebErrors"
        Me.rdbAllWebErrors.Size = New System.Drawing.Size(92, 17)
        Me.rdbAllWebErrors.TabIndex = 13
        Me.rdbAllWebErrors.Text = "View All Errors"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(381, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Error Date"
        '
        'txtWebErrorSolution
        '
        Me.txtWebErrorSolution.AcceptsReturn = True
        Me.txtWebErrorSolution.Location = New System.Drawing.Point(88, 170)
        Me.txtWebErrorSolution.Multiline = True
        Me.txtWebErrorSolution.Name = "txtWebErrorSolution"
        Me.txtWebErrorSolution.Size = New System.Drawing.Size(546, 56)
        Me.txtWebErrorSolution.TabIndex = 12
        '
        'txtWebErrorUser
        '
        Me.txtWebErrorUser.Location = New System.Drawing.Point(201, 9)
        Me.txtWebErrorUser.Name = "txtWebErrorUser"
        Me.txtWebErrorUser.ReadOnly = True
        Me.txtWebErrorUser.Size = New System.Drawing.Size(153, 20)
        Me.txtWebErrorUser.TabIndex = 8
        '
        'txtWebErrorMessage
        '
        Me.txtWebErrorMessage.AcceptsReturn = True
        Me.txtWebErrorMessage.Location = New System.Drawing.Point(88, 59)
        Me.txtWebErrorMessage.Multiline = True
        Me.txtWebErrorMessage.Name = "txtWebErrorMessage"
        Me.txtWebErrorMessage.ReadOnly = True
        Me.txtWebErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWebErrorMessage.Size = New System.Drawing.Size(553, 104)
        Me.txtWebErrorMessage.TabIndex = 11
        '
        'txtWebErrorLocation
        '
        Me.txtWebErrorLocation.Location = New System.Drawing.Point(88, 32)
        Me.txtWebErrorLocation.Name = "txtWebErrorLocation"
        Me.txtWebErrorLocation.ReadOnly = True
        Me.txtWebErrorLocation.Size = New System.Drawing.Size(266, 20)
        Me.txtWebErrorLocation.TabIndex = 9
        '
        'txtWebErrorDate
        '
        Me.txtWebErrorDate.Location = New System.Drawing.Point(468, 32)
        Me.txtWebErrorDate.Name = "txtWebErrorDate"
        Me.txtWebErrorDate.ReadOnly = True
        Me.txtWebErrorDate.Size = New System.Drawing.Size(106, 20)
        Me.txtWebErrorDate.TabIndex = 10
        '
        'dgrWebErrorList
        '
        Me.dgrWebErrorList.DataMember = ""
        Me.dgrWebErrorList.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgrWebErrorList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrWebErrorList.Location = New System.Drawing.Point(0, 0)
        Me.dgrWebErrorList.Name = "dgrWebErrorList"
        Me.dgrWebErrorList.ReadOnly = True
        Me.dgrWebErrorList.Size = New System.Drawing.Size(784, 241)
        Me.dgrWebErrorList.TabIndex = 2
        '
        'TPErrorLog
        '
        Me.TPErrorLog.Controls.Add(Me.GroupBox3)
        Me.TPErrorLog.Controls.Add(Me.dgvErrorList)
        Me.TPErrorLog.Location = New System.Drawing.Point(4, 22)
        Me.TPErrorLog.Name = "TPErrorLog"
        Me.TPErrorLog.Size = New System.Drawing.Size(784, 614)
        Me.TPErrorLog.TabIndex = 7
        Me.TPErrorLog.Text = "IAIP Error Log"
        Me.TPErrorLog.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 242)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(784, 372)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "IAIP Error Log"
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.Controls.Add(Me.btnExporttoExcel)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Label61)
        Me.Panel4.Controls.Add(Me.txtErrorCount)
        Me.Panel4.Controls.Add(Me.txtErrorNumber)
        Me.Panel4.Controls.Add(Me.btnFilterErrors)
        Me.Panel4.Controls.Add(Me.btnSaveError)
        Me.Panel4.Controls.Add(Me.rdbViewResolvedErrors)
        Me.Panel4.Controls.Add(Me.Label62)
        Me.Panel4.Controls.Add(Me.rdbViewUnresolvedErrors)
        Me.Panel4.Controls.Add(Me.Label64)
        Me.Panel4.Controls.Add(Me.rdbViewAllErrors)
        Me.Panel4.Controls.Add(Me.Label65)
        Me.Panel4.Controls.Add(Me.txtErrorSolution)
        Me.Panel4.Controls.Add(Me.Label66)
        Me.Panel4.Controls.Add(Me.txtErrorMessage)
        Me.Panel4.Controls.Add(Me.Label67)
        Me.Panel4.Controls.Add(Me.txtErrorDate)
        Me.Panel4.Controls.Add(Me.txtErrorUser)
        Me.Panel4.Controls.Add(Me.txtErrorLocation)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 16)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(778, 353)
        Me.Panel4.TabIndex = 18
        '
        'btnExporttoExcel
        '
        Me.btnExporttoExcel.Location = New System.Drawing.Point(659, 6)
        Me.btnExporttoExcel.Name = "btnExporttoExcel"
        Me.btnExporttoExcel.Size = New System.Drawing.Size(99, 23)
        Me.btnExporttoExcel.TabIndex = 20
        Me.btnExporttoExcel.Text = "Export to Excel"
        Me.btnExporttoExcel.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdbNoLimit)
        Me.Panel6.Controls.Add(Me.rdbLast60days)
        Me.Panel6.Controls.Add(Me.rdbLast30Days)
        Me.Panel6.Location = New System.Drawing.Point(243, 239)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(104, 68)
        Me.Panel6.TabIndex = 19
        '
        'rdbNoLimit
        '
        Me.rdbNoLimit.AutoSize = True
        Me.rdbNoLimit.Location = New System.Drawing.Point(3, 44)
        Me.rdbNoLimit.Name = "rdbNoLimit"
        Me.rdbNoLimit.Size = New System.Drawing.Size(59, 17)
        Me.rdbNoLimit.TabIndex = 20
        Me.rdbNoLimit.TabStop = True
        Me.rdbNoLimit.Text = "No limit"
        Me.rdbNoLimit.UseVisualStyleBackColor = True
        '
        'rdbLast60days
        '
        Me.rdbLast60days.AutoSize = True
        Me.rdbLast60days.Location = New System.Drawing.Point(3, 25)
        Me.rdbLast60days.Name = "rdbLast60days"
        Me.rdbLast60days.Size = New System.Drawing.Size(85, 17)
        Me.rdbLast60days.TabIndex = 19
        Me.rdbLast60days.TabStop = True
        Me.rdbLast60days.Text = "Last 60 days"
        Me.rdbLast60days.UseVisualStyleBackColor = True
        '
        'rdbLast30Days
        '
        Me.rdbLast30Days.AutoSize = True
        Me.rdbLast30Days.Checked = True
        Me.rdbLast30Days.Location = New System.Drawing.Point(3, 4)
        Me.rdbLast30Days.Name = "rdbLast30Days"
        Me.rdbLast30Days.Size = New System.Drawing.Size(85, 17)
        Me.rdbLast30Days.TabIndex = 18
        Me.rdbLast30Days.TabStop = True
        Me.rdbLast30Days.Text = "Last 30 days"
        Me.rdbLast30Days.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(11, 10)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(69, 13)
        Me.Label61.TabIndex = 0
        Me.Label61.Text = "Error Number"
        '
        'txtErrorCount
        '
        Me.txtErrorCount.Location = New System.Drawing.Point(619, 7)
        Me.txtErrorCount.Name = "txtErrorCount"
        Me.txtErrorCount.ReadOnly = True
        Me.txtErrorCount.Size = New System.Drawing.Size(34, 20)
        Me.txtErrorCount.TabIndex = 17
        Me.txtErrorCount.Text = "0"
        '
        'txtErrorNumber
        '
        Me.txtErrorNumber.Location = New System.Drawing.Point(91, 8)
        Me.txtErrorNumber.Name = "txtErrorNumber"
        Me.txtErrorNumber.ReadOnly = True
        Me.txtErrorNumber.Size = New System.Drawing.Size(60, 20)
        Me.txtErrorNumber.TabIndex = 1
        '
        'btnFilterErrors
        '
        Me.btnFilterErrors.Location = New System.Drawing.Point(157, 239)
        Me.btnFilterErrors.Name = "btnFilterErrors"
        Me.btnFilterErrors.Size = New System.Drawing.Size(63, 20)
        Me.btnFilterErrors.TabIndex = 16
        Me.btnFilterErrors.Text = "Filter"
        '
        'btnSaveError
        '
        Me.btnSaveError.Location = New System.Drawing.Point(11, 190)
        Me.btnSaveError.Name = "btnSaveError"
        Me.btnSaveError.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveError.TabIndex = 2
        Me.btnSaveError.Text = "Save"
        '
        'rdbViewResolvedErrors
        '
        Me.rdbViewResolvedErrors.AutoSize = True
        Me.rdbViewResolvedErrors.Location = New System.Drawing.Point(17, 280)
        Me.rdbViewResolvedErrors.Name = "rdbViewResolvedErrors"
        Me.rdbViewResolvedErrors.Size = New System.Drawing.Size(121, 17)
        Me.rdbViewResolvedErrors.TabIndex = 15
        Me.rdbViewResolvedErrors.Text = "View Resolved Error"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(171, 10)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(29, 13)
        Me.Label62.TabIndex = 3
        Me.Label62.Text = "User"
        '
        'rdbViewUnresolvedErrors
        '
        Me.rdbViewUnresolvedErrors.AutoSize = True
        Me.rdbViewUnresolvedErrors.Checked = True
        Me.rdbViewUnresolvedErrors.Location = New System.Drawing.Point(17, 259)
        Me.rdbViewUnresolvedErrors.Name = "rdbViewUnresolvedErrors"
        Me.rdbViewUnresolvedErrors.Size = New System.Drawing.Size(135, 17)
        Me.rdbViewUnresolvedErrors.TabIndex = 14
        Me.rdbViewUnresolvedErrors.TabStop = True
        Me.rdbViewUnresolvedErrors.Text = "View Unresolved Errors"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(11, 32)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(76, 13)
        Me.Label64.TabIndex = 4
        Me.Label64.Text = "Error Location "
        '
        'rdbViewAllErrors
        '
        Me.rdbViewAllErrors.AutoSize = True
        Me.rdbViewAllErrors.Location = New System.Drawing.Point(17, 239)
        Me.rdbViewAllErrors.Name = "rdbViewAllErrors"
        Me.rdbViewAllErrors.Size = New System.Drawing.Size(92, 17)
        Me.rdbViewAllErrors.TabIndex = 13
        Me.rdbViewAllErrors.Text = "View All Errors"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(11, 58)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(75, 13)
        Me.Label65.TabIndex = 5
        Me.Label65.Text = "Error Message"
        '
        'txtErrorSolution
        '
        Me.txtErrorSolution.AcceptsReturn = True
        Me.txtErrorSolution.Location = New System.Drawing.Point(91, 169)
        Me.txtErrorSolution.Multiline = True
        Me.txtErrorSolution.Name = "txtErrorSolution"
        Me.txtErrorSolution.Size = New System.Drawing.Size(546, 56)
        Me.txtErrorSolution.TabIndex = 12
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(11, 169)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(73, 13)
        Me.Label66.TabIndex = 6
        Me.Label66.Text = "Error Solution "
        '
        'txtErrorMessage
        '
        Me.txtErrorMessage.AcceptsReturn = True
        Me.txtErrorMessage.Location = New System.Drawing.Point(91, 58)
        Me.txtErrorMessage.Multiline = True
        Me.txtErrorMessage.Name = "txtErrorMessage"
        Me.txtErrorMessage.ReadOnly = True
        Me.txtErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtErrorMessage.Size = New System.Drawing.Size(553, 104)
        Me.txtErrorMessage.TabIndex = 11
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(384, 32)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(55, 13)
        Me.Label67.TabIndex = 7
        Me.Label67.Text = "Error Date"
        '
        'txtErrorDate
        '
        Me.txtErrorDate.Location = New System.Drawing.Point(471, 31)
        Me.txtErrorDate.Name = "txtErrorDate"
        Me.txtErrorDate.ReadOnly = True
        Me.txtErrorDate.Size = New System.Drawing.Size(106, 20)
        Me.txtErrorDate.TabIndex = 10
        '
        'txtErrorUser
        '
        Me.txtErrorUser.Location = New System.Drawing.Point(204, 8)
        Me.txtErrorUser.Name = "txtErrorUser"
        Me.txtErrorUser.ReadOnly = True
        Me.txtErrorUser.Size = New System.Drawing.Size(193, 20)
        Me.txtErrorUser.TabIndex = 8
        '
        'txtErrorLocation
        '
        Me.txtErrorLocation.Location = New System.Drawing.Point(91, 31)
        Me.txtErrorLocation.Name = "txtErrorLocation"
        Me.txtErrorLocation.ReadOnly = True
        Me.txtErrorLocation.Size = New System.Drawing.Size(266, 20)
        Me.txtErrorLocation.TabIndex = 9
        '
        'dgvErrorList
        '
        Me.dgvErrorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvErrorList.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvErrorList.Location = New System.Drawing.Point(0, 0)
        Me.dgvErrorList.Name = "dgvErrorList"
        Me.dgvErrorList.Size = New System.Drawing.Size(784, 242)
        Me.dgvErrorList.TabIndex = 21
        '
        'TPAddNewFacility
        '
        Me.TPAddNewFacility.Controls.Add(Me.txtApplicationNumber)
        Me.TPAddNewFacility.Controls.Add(Me.btnPreLoadNewFacility)
        Me.TPAddNewFacility.Controls.Add(Me.btnDeleteAIRSNumber)
        Me.TPAddNewFacility.Controls.Add(Me.txtDeleteAIRSNumber)
        Me.TPAddNewFacility.Controls.Add(Me.btnClearAddNewFacility)
        Me.TPAddNewFacility.Controls.Add(Me.GBContactInformation)
        Me.TPAddNewFacility.Controls.Add(Me.GBAirProgramCodes)
        Me.TPAddNewFacility.Controls.Add(Me.GBHeaderData)
        Me.TPAddNewFacility.Controls.Add(Me.GBMailingLocation)
        Me.TPAddNewFacility.Controls.Add(Me.GBFacilityInformation)
        Me.TPAddNewFacility.Controls.Add(Me.llbContactInformation)
        Me.TPAddNewFacility.Controls.Add(Me.llbAirProgramCodes)
        Me.TPAddNewFacility.Controls.Add(Me.llbHeaderData)
        Me.TPAddNewFacility.Controls.Add(Me.llbMailingLocation)
        Me.TPAddNewFacility.Controls.Add(Me.llbFacilityInformation)
        Me.TPAddNewFacility.Controls.Add(Me.btnNewFacility)
        Me.TPAddNewFacility.Controls.Add(Me.Label4)
        Me.TPAddNewFacility.Controls.Add(Me.txtCDSAIRSNumber)
        Me.TPAddNewFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPAddNewFacility.Name = "TPAddNewFacility"
        Me.TPAddNewFacility.Size = New System.Drawing.Size(784, 614)
        Me.TPAddNewFacility.TabIndex = 2
        Me.TPAddNewFacility.Text = "Add New Facility"
        Me.TPAddNewFacility.UseVisualStyleBackColor = True
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Location = New System.Drawing.Point(16, 419)
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtApplicationNumber.TabIndex = 69
        Me.txtApplicationNumber.Text = "App No."
        '
        'btnPreLoadNewFacility
        '
        Me.btnPreLoadNewFacility.Location = New System.Drawing.Point(16, 390)
        Me.btnPreLoadNewFacility.Name = "btnPreLoadNewFacility"
        Me.btnPreLoadNewFacility.Size = New System.Drawing.Size(75, 23)
        Me.btnPreLoadNewFacility.TabIndex = 68
        Me.btnPreLoadNewFacility.Text = "PreLoad"
        Me.btnPreLoadNewFacility.UseVisualStyleBackColor = True
        Me.btnPreLoadNewFacility.Visible = False
        '
        'btnDeleteAIRSNumber
        '
        Me.btnDeleteAIRSNumber.Location = New System.Drawing.Point(13, 335)
        Me.btnDeleteAIRSNumber.Name = "btnDeleteAIRSNumber"
        Me.btnDeleteAIRSNumber.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteAIRSNumber.TabIndex = 67
        Me.btnDeleteAIRSNumber.Text = "Delete AIRS Number"
        Me.btnDeleteAIRSNumber.UseVisualStyleBackColor = True
        Me.btnDeleteAIRSNumber.Visible = False
        '
        'txtDeleteAIRSNumber
        '
        Me.txtDeleteAIRSNumber.Location = New System.Drawing.Point(13, 309)
        Me.txtDeleteAIRSNumber.Name = "txtDeleteAIRSNumber"
        Me.txtDeleteAIRSNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtDeleteAIRSNumber.TabIndex = 66
        '
        'btnClearAddNewFacility
        '
        Me.btnClearAddNewFacility.Location = New System.Drawing.Point(13, 263)
        Me.btnClearAddNewFacility.Name = "btnClearAddNewFacility"
        Me.btnClearAddNewFacility.Size = New System.Drawing.Size(112, 24)
        Me.btnClearAddNewFacility.TabIndex = 65
        Me.btnClearAddNewFacility.Text = "Clear Form"
        '
        'GBContactInformation
        '
        Me.GBContactInformation.Controls.Add(Me.mtbContactNumberExtension)
        Me.GBContactInformation.Controls.Add(Me.txtContactPedigree)
        Me.GBContactInformation.Controls.Add(Me.txtContactSocialTitle)
        Me.GBContactInformation.Controls.Add(Me.Label33)
        Me.GBContactInformation.Controls.Add(Me.Label34)
        Me.GBContactInformation.Controls.Add(Me.txtContactLastName)
        Me.GBContactInformation.Controls.Add(Me.Label35)
        Me.GBContactInformation.Controls.Add(Me.txtContactFirstName)
        Me.GBContactInformation.Controls.Add(Me.Label36)
        Me.GBContactInformation.Controls.Add(Me.mtbContactPhoneNumber)
        Me.GBContactInformation.Controls.Add(Me.Label23)
        Me.GBContactInformation.Controls.Add(Me.Label22)
        Me.GBContactInformation.Controls.Add(Me.Label30)
        Me.GBContactInformation.Controls.Add(Me.txtContactTitle)
        Me.GBContactInformation.Location = New System.Drawing.Point(187, 582)
        Me.GBContactInformation.Name = "GBContactInformation"
        Me.GBContactInformation.Size = New System.Drawing.Size(466, 157)
        Me.GBContactInformation.TabIndex = 64
        Me.GBContactInformation.TabStop = False
        Me.GBContactInformation.Text = "Contact Information"
        Me.GBContactInformation.Visible = False
        '
        'mtbContactNumberExtension
        '
        Me.mtbContactNumberExtension.Location = New System.Drawing.Point(248, 118)
        Me.mtbContactNumberExtension.Mask = "000000"
        Me.mtbContactNumberExtension.Name = "mtbContactNumberExtension"
        Me.mtbContactNumberExtension.Size = New System.Drawing.Size(46, 20)
        Me.mtbContactNumberExtension.TabIndex = 372
        '
        'txtContactPedigree
        '
        Me.txtContactPedigree.Location = New System.Drawing.Point(75, 91)
        Me.txtContactPedigree.Name = "txtContactPedigree"
        Me.txtContactPedigree.Size = New System.Drawing.Size(72, 20)
        Me.txtContactPedigree.TabIndex = 4
        '
        'txtContactSocialTitle
        '
        Me.txtContactSocialTitle.Location = New System.Drawing.Point(76, 16)
        Me.txtContactSocialTitle.Name = "txtContactSocialTitle"
        Me.txtContactSocialTitle.Size = New System.Drawing.Size(72, 20)
        Me.txtContactSocialTitle.TabIndex = 1
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(14, 20)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(59, 13)
        Me.Label33.TabIndex = 371
        Me.Label33.Text = "Social Title"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(14, 95)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(49, 13)
        Me.Label34.TabIndex = 370
        Me.Label34.Text = "Pedigree"
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Location = New System.Drawing.Point(75, 66)
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(151, 20)
        Me.txtContactLastName.TabIndex = 3
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(14, 70)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(58, 13)
        Me.Label35.TabIndex = 369
        Me.Label35.Text = "Last Name"
        '
        'txtContactFirstName
        '
        Me.txtContactFirstName.Location = New System.Drawing.Point(76, 41)
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(151, 20)
        Me.txtContactFirstName.TabIndex = 2
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(14, 45)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(57, 13)
        Me.Label36.TabIndex = 368
        Me.Label36.Text = "First Name"
        '
        'mtbContactPhoneNumber
        '
        Me.mtbContactPhoneNumber.Location = New System.Drawing.Point(102, 118)
        Me.mtbContactPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbContactPhoneNumber.Name = "mtbContactPhoneNumber"
        Me.mtbContactPhoneNumber.Size = New System.Drawing.Size(100, 20)
        Me.mtbContactPhoneNumber.TabIndex = 6
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(220, 122)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(24, 13)
        Me.Label23.TabIndex = 133
        Me.Label23.Text = "ext."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(14, 122)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(78, 13)
        Me.Label22.TabIndex = 130
        Me.Label22.Text = "Phone Number"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(237, 20)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(27, 13)
        Me.Label30.TabIndex = 128
        Me.Label30.Text = "Title"
        '
        'txtContactTitle
        '
        Me.txtContactTitle.Location = New System.Drawing.Point(273, 16)
        Me.txtContactTitle.Name = "txtContactTitle"
        Me.txtContactTitle.Size = New System.Drawing.Size(180, 20)
        Me.txtContactTitle.TabIndex = 5
        '
        'GBAirProgramCodes
        '
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_14)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_7)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_4)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_13)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_3)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_12)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_9)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_10)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_2)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_6)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_1)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_5)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_11)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_8)
        Me.GBAirProgramCodes.Controls.Add(Me.Label37)
        Me.GBAirProgramCodes.Location = New System.Drawing.Point(187, 488)
        Me.GBAirProgramCodes.Name = "GBAirProgramCodes"
        Me.GBAirProgramCodes.Size = New System.Drawing.Size(466, 104)
        Me.GBAirProgramCodes.TabIndex = 63
        Me.GBAirProgramCodes.TabStop = False
        Me.GBAirProgramCodes.Text = "Air Program Codes && Pollutants"
        Me.GBAirProgramCodes.Visible = False
        '
        'chbCDS_14
        '
        Me.chbCDS_14.AutoSize = True
        Me.chbCDS_14.Location = New System.Drawing.Point(327, 51)
        Me.chbCDS_14.Name = "chbCDS_14"
        Me.chbCDS_14.Size = New System.Drawing.Size(128, 17)
        Me.chbCDS_14.TabIndex = 158
        Me.chbCDS_14.Text = "G - Green House Gas"
        '
        'chbCDS_7
        '
        Me.chbCDS_7.AutoSize = True
        Me.chbCDS_7.Location = New System.Drawing.Point(127, 66)
        Me.chbCDS_7.Name = "chbCDS_7"
        Me.chbCDS_7.Size = New System.Drawing.Size(85, 17)
        Me.chbCDS_7.TabIndex = 7
        Me.chbCDS_7.Text = "8 - NESHAP"
        '
        'chbCDS_4
        '
        Me.chbCDS_4.AutoSize = True
        Me.chbCDS_4.Location = New System.Drawing.Point(20, 82)
        Me.chbCDS_4.Name = "chbCDS_4"
        Me.chbCDS_4.Size = New System.Drawing.Size(106, 17)
        Me.chbCDS_4.TabIndex = 4
        Me.chbCDS_4.Text = "4 - CFC Tracking"
        '
        'chbCDS_13
        '
        Me.chbCDS_13.AutoSize = True
        Me.chbCDS_13.Location = New System.Drawing.Point(327, 35)
        Me.chbCDS_13.Name = "chbCDS_13"
        Me.chbCDS_13.Size = New System.Drawing.Size(72, 17)
        Me.chbCDS_13.TabIndex = 13
        Me.chbCDS_13.Text = "V - Title V"
        '
        'chbCDS_3
        '
        Me.chbCDS_3.AutoSize = True
        Me.chbCDS_3.Location = New System.Drawing.Point(20, 66)
        Me.chbCDS_3.Name = "chbCDS_3"
        Me.chbCDS_3.Size = New System.Drawing.Size(85, 17)
        Me.chbCDS_3.TabIndex = 3
        Me.chbCDS_3.Text = "3 - Non Fed."
        '
        'chbCDS_12
        '
        Me.chbCDS_12.AutoSize = True
        Me.chbCDS_12.Location = New System.Drawing.Point(213, 82)
        Me.chbCDS_12.Name = "chbCDS_12"
        Me.chbCDS_12.Size = New System.Drawing.Size(74, 17)
        Me.chbCDS_12.TabIndex = 12
        Me.chbCDS_12.Text = "M - MACT"
        '
        'chbCDS_9
        '
        Me.chbCDS_9.AutoSize = True
        Me.chbCDS_9.Location = New System.Drawing.Point(213, 35)
        Me.chbCDS_9.Name = "chbCDS_9"
        Me.chbCDS_9.Size = New System.Drawing.Size(79, 17)
        Me.chbCDS_9.TabIndex = 9
        Me.chbCDS_9.Text = "F - FESOP "
        '
        'chbCDS_10
        '
        Me.chbCDS_10.AutoSize = True
        Me.chbCDS_10.Location = New System.Drawing.Point(213, 50)
        Me.chbCDS_10.Name = "chbCDS_10"
        Me.chbCDS_10.Size = New System.Drawing.Size(99, 17)
        Me.chbCDS_10.TabIndex = 10
        Me.chbCDS_10.Text = "A - Acid Precip."
        '
        'chbCDS_2
        '
        Me.chbCDS_2.AutoSize = True
        Me.chbCDS_2.Location = New System.Drawing.Point(20, 50)
        Me.chbCDS_2.Name = "chbCDS_2"
        Me.chbCDS_2.Size = New System.Drawing.Size(82, 17)
        Me.chbCDS_2.TabIndex = 2
        Me.chbCDS_2.Text = "1 - Fed. SIP"
        '
        'chbCDS_6
        '
        Me.chbCDS_6.AutoSize = True
        Me.chbCDS_6.Location = New System.Drawing.Point(127, 50)
        Me.chbCDS_6.Name = "chbCDS_6"
        Me.chbCDS_6.Size = New System.Drawing.Size(64, 17)
        Me.chbCDS_6.TabIndex = 6
        Me.chbCDS_6.Text = "7 - NSR"
        '
        'chbCDS_1
        '
        Me.chbCDS_1.AutoSize = True
        Me.chbCDS_1.Location = New System.Drawing.Point(20, 35)
        Me.chbCDS_1.Name = "chbCDS_1"
        Me.chbCDS_1.Size = New System.Drawing.Size(58, 17)
        Me.chbCDS_1.TabIndex = 1
        Me.chbCDS_1.Text = "0 - SIP"
        '
        'chbCDS_5
        '
        Me.chbCDS_5.AutoSize = True
        Me.chbCDS_5.Location = New System.Drawing.Point(127, 35)
        Me.chbCDS_5.Name = "chbCDS_5"
        Me.chbCDS_5.Size = New System.Drawing.Size(63, 17)
        Me.chbCDS_5.TabIndex = 5
        Me.chbCDS_5.Text = "6 - PSD"
        '
        'chbCDS_11
        '
        Me.chbCDS_11.AutoSize = True
        Me.chbCDS_11.Location = New System.Drawing.Point(213, 66)
        Me.chbCDS_11.Name = "chbCDS_11"
        Me.chbCDS_11.Size = New System.Drawing.Size(116, 17)
        Me.chbCDS_11.TabIndex = 11
        Me.chbCDS_11.Text = "I - Native American"
        '
        'chbCDS_8
        '
        Me.chbCDS_8.AutoSize = True
        Me.chbCDS_8.Location = New System.Drawing.Point(127, 82)
        Me.chbCDS_8.Name = "chbCDS_8"
        Me.chbCDS_8.Size = New System.Drawing.Size(70, 17)
        Me.chbCDS_8.TabIndex = 8
        Me.chbCDS_8.Text = "9 - NSPS"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(16, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(106, 13)
        Me.Label37.TabIndex = 157
        Me.Label37.Text = "(Select All that apply)"
        '
        'GBHeaderData
        '
        Me.GBHeaderData.Controls.Add(Me.mtbCDSSICCode)
        Me.GBHeaderData.Controls.Add(Me.txtCDSRegionCode)
        Me.GBHeaderData.Controls.Add(Me.Label21)
        Me.GBHeaderData.Controls.Add(Me.cboCDSOperationalStatus)
        Me.GBHeaderData.Controls.Add(Me.Label51)
        Me.GBHeaderData.Controls.Add(Me.cboCDSClassCode)
        Me.GBHeaderData.Controls.Add(Me.Label49)
        Me.GBHeaderData.Controls.Add(Me.Label42)
        Me.GBHeaderData.Controls.Add(Me.Label63)
        Me.GBHeaderData.Controls.Add(Me.txtFacilityDescription)
        Me.GBHeaderData.Location = New System.Drawing.Point(186, 351)
        Me.GBHeaderData.Name = "GBHeaderData"
        Me.GBHeaderData.Size = New System.Drawing.Size(466, 131)
        Me.GBHeaderData.TabIndex = 62
        Me.GBHeaderData.TabStop = False
        Me.GBHeaderData.Text = "Header Data"
        Me.GBHeaderData.Visible = False
        '
        'mtbCDSSICCode
        '
        Me.mtbCDSSICCode.Location = New System.Drawing.Point(104, 47)
        Me.mtbCDSSICCode.Mask = "0000"
        Me.mtbCDSSICCode.Name = "mtbCDSSICCode"
        Me.mtbCDSSICCode.Size = New System.Drawing.Size(76, 20)
        Me.mtbCDSSICCode.TabIndex = 169
        '
        'txtCDSRegionCode
        '
        Me.txtCDSRegionCode.Location = New System.Drawing.Point(104, 16)
        Me.txtCDSRegionCode.MaxLength = 4
        Me.txtCDSRegionCode.Name = "txtCDSRegionCode"
        Me.txtCDSRegionCode.ReadOnly = True
        Me.txtCDSRegionCode.Size = New System.Drawing.Size(76, 20)
        Me.txtCDSRegionCode.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(27, 18)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 13)
        Me.Label21.TabIndex = 167
        Me.Label21.Text = "Region Code"
        '
        'cboCDSOperationalStatus
        '
        Me.cboCDSOperationalStatus.Location = New System.Drawing.Point(300, 16)
        Me.cboCDSOperationalStatus.Name = "cboCDSOperationalStatus"
        Me.cboCDSOperationalStatus.Size = New System.Drawing.Size(153, 21)
        Me.cboCDSOperationalStatus.TabIndex = 1
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(200, 18)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(94, 26)
        Me.Label51.TabIndex = 168
        Me.Label51.Text = "Operational Status" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Use X only)"
        '
        'cboCDSClassCode
        '
        Me.cboCDSClassCode.Location = New System.Drawing.Point(300, 47)
        Me.cboCDSClassCode.Name = "cboCDSClassCode"
        Me.cboCDSClassCode.Size = New System.Drawing.Size(153, 21)
        Me.cboCDSClassCode.TabIndex = 3
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(193, 49)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(95, 13)
        Me.Label49.TabIndex = 164
        Me.Label49.Text = "Facility Class Code"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(44, 47)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(52, 13)
        Me.Label42.TabIndex = 161
        Me.Label42.Text = "SIC Code"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(8, 72)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(87, 13)
        Me.Label63.TabIndex = 8
        Me.Label63.Text = "Plant Description"
        '
        'txtFacilityDescription
        '
        Me.txtFacilityDescription.Location = New System.Drawing.Point(24, 88)
        Me.txtFacilityDescription.MaxLength = 4000
        Me.txtFacilityDescription.Name = "txtFacilityDescription"
        Me.txtFacilityDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityDescription.Size = New System.Drawing.Size(429, 20)
        Me.txtFacilityDescription.TabIndex = 4
        '
        'GBMailingLocation
        '
        Me.GBMailingLocation.Controls.Add(Me.mtbMailingZipCode)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingState)
        Me.GBMailingLocation.Controls.Add(Me.Label18)
        Me.GBMailingLocation.Controls.Add(Me.Label19)
        Me.GBMailingLocation.Controls.Add(Me.Label20)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingCity)
        Me.GBMailingLocation.Controls.Add(Me.Label24)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingAddress)
        Me.GBMailingLocation.Location = New System.Drawing.Point(187, 243)
        Me.GBMailingLocation.Name = "GBMailingLocation"
        Me.GBMailingLocation.Size = New System.Drawing.Size(465, 104)
        Me.GBMailingLocation.TabIndex = 61
        Me.GBMailingLocation.TabStop = False
        Me.GBMailingLocation.Text = "Mailing Location"
        Me.GBMailingLocation.Visible = False
        '
        'mtbMailingZipCode
        '
        Me.mtbMailingZipCode.Location = New System.Drawing.Point(104, 66)
        Me.mtbMailingZipCode.Mask = "00000-9999"
        Me.mtbMailingZipCode.Name = "mtbMailingZipCode"
        Me.mtbMailingZipCode.Size = New System.Drawing.Size(72, 20)
        Me.mtbMailingZipCode.TabIndex = 3
        '
        'txtMailingState
        '
        Me.txtMailingState.Location = New System.Drawing.Point(312, 40)
        Me.txtMailingState.MaxLength = 2
        Me.txtMailingState.Name = "txtMailingState"
        Me.txtMailingState.Size = New System.Drawing.Size(24, 20)
        Me.txtMailingState.TabIndex = 3
        Me.txtMailingState.Text = "GA"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(38, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 13)
        Me.Label18.TabIndex = 162
        Me.Label18.Text = "Mailing Zip"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(272, 42)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 161
        Me.Label19.Text = "State"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(34, 42)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 13)
        Me.Label20.TabIndex = 160
        Me.Label20.Text = "Mailing City"
        '
        'txtMailingCity
        '
        Me.txtMailingCity.Location = New System.Drawing.Point(104, 40)
        Me.txtMailingCity.Name = "txtMailingCity"
        Me.txtMailingCity.Size = New System.Drawing.Size(160, 20)
        Me.txtMailingCity.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 18)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(81, 13)
        Me.Label24.TabIndex = 159
        Me.Label24.Text = "Mailing Address"
        '
        'txtMailingAddress
        '
        Me.txtMailingAddress.Location = New System.Drawing.Point(104, 16)
        Me.txtMailingAddress.Name = "txtMailingAddress"
        Me.txtMailingAddress.Size = New System.Drawing.Size(352, 20)
        Me.txtMailingAddress.TabIndex = 1
        '
        'GBFacilityInformation
        '
        Me.GBFacilityInformation.Controls.Add(Me.mtbFacilityLongitude)
        Me.GBFacilityInformation.Controls.Add(Me.mtbFacilityLatitude)
        Me.GBFacilityInformation.Controls.Add(Me.mtbCDSZipCode)
        Me.GBFacilityInformation.Controls.Add(Me.Label103)
        Me.GBFacilityInformation.Controls.Add(Me.Label102)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSStreetAddress)
        Me.GBFacilityInformation.Controls.Add(Me.Label5)
        Me.GBFacilityInformation.Controls.Add(Me.Label9)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSFacilityName)
        Me.GBFacilityInformation.Controls.Add(Me.Label7)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSCity)
        Me.GBFacilityInformation.Controls.Add(Me.Label8)
        Me.GBFacilityInformation.Controls.Add(Me.Label10)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSState)
        Me.GBFacilityInformation.Controls.Add(Me.Label27)
        Me.GBFacilityInformation.Controls.Add(Me.Label28)
        Me.GBFacilityInformation.Location = New System.Drawing.Point(187, 0)
        Me.GBFacilityInformation.Name = "GBFacilityInformation"
        Me.GBFacilityInformation.Size = New System.Drawing.Size(466, 243)
        Me.GBFacilityInformation.TabIndex = 60
        Me.GBFacilityInformation.TabStop = False
        Me.GBFacilityInformation.Text = "Facility Information"
        Me.GBFacilityInformation.Visible = False
        '
        'mtbFacilityLongitude
        '
        Me.mtbFacilityLongitude.Location = New System.Drawing.Point(330, 135)
        Me.mtbFacilityLongitude.Mask = "-00.000000"
        Me.mtbFacilityLongitude.Name = "mtbFacilityLongitude"
        Me.mtbFacilityLongitude.Size = New System.Drawing.Size(69, 20)
        Me.mtbFacilityLongitude.TabIndex = 175
        '
        'mtbFacilityLatitude
        '
        Me.mtbFacilityLatitude.Location = New System.Drawing.Point(104, 131)
        Me.mtbFacilityLatitude.Mask = "00.000000"
        Me.mtbFacilityLatitude.Name = "mtbFacilityLatitude"
        Me.mtbFacilityLatitude.Size = New System.Drawing.Size(72, 20)
        Me.mtbFacilityLatitude.TabIndex = 174
        '
        'mtbCDSZipCode
        '
        Me.mtbCDSZipCode.Location = New System.Drawing.Point(104, 95)
        Me.mtbCDSZipCode.Mask = "00000-9999"
        Me.mtbCDSZipCode.Name = "mtbCDSZipCode"
        Me.mtbCDSZipCode.Size = New System.Drawing.Size(72, 20)
        Me.mtbCDSZipCode.TabIndex = 4
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Location = New System.Drawing.Point(294, 158)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(159, 13)
        Me.Label103.TabIndex = 173
        Me.Label103.Text = "(Range = 80.84111 -- 85.60444)"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(80, 158)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(147, 13)
        Me.Label102.TabIndex = 171
        Me.Label102.Text = "(Range = 30.3555 -- 35.0000)"
        '
        'txtCDSStreetAddress
        '
        Me.txtCDSStreetAddress.Location = New System.Drawing.Point(104, 48)
        Me.txtCDSStreetAddress.MaxLength = 100
        Me.txtCDSStreetAddress.Name = "txtCDSStreetAddress"
        Me.txtCDSStreetAddress.Size = New System.Drawing.Size(349, 20)
        Me.txtCDSStreetAddress.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(272, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "State"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(24, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Facility Name"
        '
        'txtCDSFacilityName
        '
        Me.txtCDSFacilityName.Location = New System.Drawing.Point(104, 24)
        Me.txtCDSFacilityName.MaxLength = 100
        Me.txtCDSFacilityName.Name = "txtCDSFacilityName"
        Me.txtCDSFacilityName.Size = New System.Drawing.Size(349, 20)
        Me.txtCDSFacilityName.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(73, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "City"
        '
        'txtCDSCity
        '
        Me.txtCDSCity.Location = New System.Drawing.Point(104, 72)
        Me.txtCDSCity.MaxLength = 50
        Me.txtCDSCity.Name = "txtCDSCity"
        Me.txtCDSCity.Size = New System.Drawing.Size(160, 20)
        Me.txtCDSCity.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Street Address"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(47, 98)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Zip Code"
        '
        'txtCDSState
        '
        Me.txtCDSState.Location = New System.Drawing.Point(312, 72)
        Me.txtCDSState.MaxLength = 4
        Me.txtCDSState.Name = "txtCDSState"
        Me.txtCDSState.ReadOnly = True
        Me.txtCDSState.Size = New System.Drawing.Size(24, 20)
        Me.txtCDSState.TabIndex = 7
        Me.txtCDSState.TabStop = False
        Me.txtCDSState.Text = "GA"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(270, 138)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(54, 13)
        Me.Label27.TabIndex = 156
        Me.Label27.Text = "Longitude"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(47, 138)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(45, 13)
        Me.Label28.TabIndex = 155
        Me.Label28.Text = "Latitude"
        '
        'llbContactInformation
        '
        Me.llbContactInformation.AutoSize = True
        Me.llbContactInformation.Enabled = False
        Me.llbContactInformation.Location = New System.Drawing.Point(13, 180)
        Me.llbContactInformation.Name = "llbContactInformation"
        Me.llbContactInformation.Size = New System.Drawing.Size(99, 13)
        Me.llbContactInformation.TabIndex = 57
        Me.llbContactInformation.TabStop = True
        Me.llbContactInformation.Text = "Contact Information"
        '
        'llbAirProgramCodes
        '
        Me.llbAirProgramCodes.AutoSize = True
        Me.llbAirProgramCodes.Enabled = False
        Me.llbAirProgramCodes.Location = New System.Drawing.Point(13, 146)
        Me.llbAirProgramCodes.Name = "llbAirProgramCodes"
        Me.llbAirProgramCodes.Size = New System.Drawing.Size(152, 13)
        Me.llbAirProgramCodes.TabIndex = 56
        Me.llbAirProgramCodes.TabStop = True
        Me.llbAirProgramCodes.Text = "Air Program Codes && Pollutants"
        '
        'llbHeaderData
        '
        Me.llbHeaderData.AutoSize = True
        Me.llbHeaderData.Enabled = False
        Me.llbHeaderData.Location = New System.Drawing.Point(13, 111)
        Me.llbHeaderData.Name = "llbHeaderData"
        Me.llbHeaderData.Size = New System.Drawing.Size(68, 13)
        Me.llbHeaderData.TabIndex = 55
        Me.llbHeaderData.TabStop = True
        Me.llbHeaderData.Text = "Header Data"
        '
        'llbMailingLocation
        '
        Me.llbMailingLocation.AutoSize = True
        Me.llbMailingLocation.Enabled = False
        Me.llbMailingLocation.Location = New System.Drawing.Point(13, 76)
        Me.llbMailingLocation.Name = "llbMailingLocation"
        Me.llbMailingLocation.Size = New System.Drawing.Size(84, 13)
        Me.llbMailingLocation.TabIndex = 54
        Me.llbMailingLocation.TabStop = True
        Me.llbMailingLocation.Text = "Mailing Location"
        '
        'llbFacilityInformation
        '
        Me.llbFacilityInformation.AutoSize = True
        Me.llbFacilityInformation.Enabled = False
        Me.llbFacilityInformation.Location = New System.Drawing.Point(13, 42)
        Me.llbFacilityInformation.Name = "llbFacilityInformation"
        Me.llbFacilityInformation.Size = New System.Drawing.Size(94, 13)
        Me.llbFacilityInformation.TabIndex = 53
        Me.llbFacilityInformation.TabStop = True
        Me.llbFacilityInformation.Text = "Facility Information"
        '
        'btnNewFacility
        '
        Me.btnNewFacility.Enabled = False
        Me.btnNewFacility.Location = New System.Drawing.Point(13, 215)
        Me.btnNewFacility.Name = "btnNewFacility"
        Me.btnNewFacility.Size = New System.Drawing.Size(112, 23)
        Me.btnNewFacility.TabIndex = 58
        Me.btnNewFacility.Text = "Create New Facility"
        Me.btnNewFacility.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "AIRS Number:"
        '
        'txtCDSAIRSNumber
        '
        Me.txtCDSAIRSNumber.Location = New System.Drawing.Point(87, 7)
        Me.txtCDSAIRSNumber.MaxLength = 12
        Me.txtCDSAIRSNumber.Name = "txtCDSAIRSNumber"
        Me.txtCDSAIRSNumber.Size = New System.Drawing.Size(88, 20)
        Me.txtCDSAIRSNumber.TabIndex = 52
        Me.txtCDSAIRSNumber.Text = "0413"
        '
        'TPAFSFileGenerator
        '
        Me.TPAFSFileGenerator.Controls.Add(Me.txtAFSBatchFile)
        Me.TPAFSFileGenerator.Controls.Add(Me.PanelBatchOrder)
        Me.TPAFSFileGenerator.Location = New System.Drawing.Point(4, 22)
        Me.TPAFSFileGenerator.Name = "TPAFSFileGenerator"
        Me.TPAFSFileGenerator.Size = New System.Drawing.Size(784, 614)
        Me.TPAFSFileGenerator.TabIndex = 1
        Me.TPAFSFileGenerator.Text = "AFS File Generator"
        Me.TPAFSFileGenerator.UseVisualStyleBackColor = True
        '
        'txtAFSBatchFile
        '
        Me.txtAFSBatchFile.AcceptsReturn = True
        Me.txtAFSBatchFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAFSBatchFile.Location = New System.Drawing.Point(0, 257)
        Me.txtAFSBatchFile.Multiline = True
        Me.txtAFSBatchFile.Name = "txtAFSBatchFile"
        Me.txtAFSBatchFile.ReadOnly = True
        Me.txtAFSBatchFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAFSBatchFile.Size = New System.Drawing.Size(784, 357)
        Me.txtAFSBatchFile.TabIndex = 11
        '
        'PanelBatchOrder
        '
        Me.PanelBatchOrder.Controls.Add(Me.btnUpdateAllSubParts)
        Me.PanelBatchOrder.Controls.Add(Me.btnForceBasicRefresh)
        Me.PanelBatchOrder.Controls.Add(Me.btnClearAFSFileGenerator)
        Me.PanelBatchOrder.Controls.Add(Me.Label29)
        Me.PanelBatchOrder.Controls.Add(Me.Label38)
        Me.PanelBatchOrder.Controls.Add(Me.Label58)
        Me.PanelBatchOrder.Controls.Add(Me.Label59)
        Me.PanelBatchOrder.Controls.Add(Me.btnGenerateBatchFile)
        Me.PanelBatchOrder.Controls.Add(Me.Label2)
        Me.PanelBatchOrder.Controls.Add(Me.Label41)
        Me.PanelBatchOrder.Controls.Add(Me.Label3)
        Me.PanelBatchOrder.Controls.Add(Me.Label55)
        Me.PanelBatchOrder.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelBatchOrder.Location = New System.Drawing.Point(0, 0)
        Me.PanelBatchOrder.Name = "PanelBatchOrder"
        Me.PanelBatchOrder.Size = New System.Drawing.Size(784, 257)
        Me.PanelBatchOrder.TabIndex = 10
        '
        'btnUpdateAllSubParts
        '
        Me.btnUpdateAllSubParts.AutoSize = True
        Me.btnUpdateAllSubParts.Location = New System.Drawing.Point(577, 11)
        Me.btnUpdateAllSubParts.Name = "btnUpdateAllSubParts"
        Me.btnUpdateAllSubParts.Size = New System.Drawing.Size(112, 23)
        Me.btnUpdateAllSubParts.TabIndex = 11
        Me.btnUpdateAllSubParts.Text = "Update All SubParts"
        Me.btnUpdateAllSubParts.UseVisualStyleBackColor = True
        '
        'btnForceBasicRefresh
        '
        Me.btnForceBasicRefresh.AutoSize = True
        Me.btnForceBasicRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnForceBasicRefresh.Location = New System.Drawing.Point(359, 8)
        Me.btnForceBasicRefresh.Name = "btnForceBasicRefresh"
        Me.btnForceBasicRefresh.Size = New System.Drawing.Size(113, 23)
        Me.btnForceBasicRefresh.TabIndex = 10
        Me.btnForceBasicRefresh.Text = "Force Basic Refresh"
        '
        'btnClearAFSFileGenerator
        '
        Me.btnClearAFSFileGenerator.Location = New System.Drawing.Point(7, 97)
        Me.btnClearAFSFileGenerator.Name = "btnClearAFSFileGenerator"
        Me.btnClearAFSFileGenerator.Size = New System.Drawing.Size(87, 23)
        Me.btnClearAFSFileGenerator.TabIndex = 9
        Me.btnClearAFSFileGenerator.Text = "Clear Form"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(144, 136)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(151, 13)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "5) Full Compliance Evaluations"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(144, 64)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(137, 13)
        Me.Label38.TabIndex = 3
        Me.Label38.Text = "2) Changes to Header Data"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(144, 160)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(117, 13)
        Me.Label58.TabIndex = 6
        Me.Label58.Text = "6) Enforcement Actions"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(144, 184)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(109, 13)
        Me.Label59.TabIndex = 7
        Me.Label59.Text = "7) ISMP Test Reports"
        '
        'btnGenerateBatchFile
        '
        Me.btnGenerateBatchFile.Location = New System.Drawing.Point(8, 8)
        Me.btnGenerateBatchFile.Name = "btnGenerateBatchFile"
        Me.btnGenerateBatchFile.Size = New System.Drawing.Size(88, 23)
        Me.btnGenerateBatchFile.TabIndex = 0
        Me.btnGenerateBatchFile.Text = "Generate File"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(144, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "1) New Facilities"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(144, 88)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(103, 13)
        Me.Label41.TabIndex = 4
        Me.Label41.Text = "3) Permitting Actions"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(128, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Batch File Hierarchy"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(144, 112)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(112, 13)
        Me.Label55.TabIndex = 5
        Me.Label55.Text = "4) Compliance Actions"
        '
        'TPUpdateDEVTest
        '
        Me.TPUpdateDEVTest.Controls.Add(Me.TCTables)
        Me.TPUpdateDEVTest.Controls.Add(Me.PanelSources)
        Me.TPUpdateDEVTest.Location = New System.Drawing.Point(4, 22)
        Me.TPUpdateDEVTest.Name = "TPUpdateDEVTest"
        Me.TPUpdateDEVTest.Size = New System.Drawing.Size(784, 614)
        Me.TPUpdateDEVTest.TabIndex = 12
        Me.TPUpdateDEVTest.Text = "Update DEV/Test Environments"
        Me.TPUpdateDEVTest.UseVisualStyleBackColor = True
        '
        'TCTables
        '
        Me.TCTables.Controls.Add(Me.TPAllTables)
        Me.TCTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCTables.Location = New System.Drawing.Point(0, 64)
        Me.TCTables.Name = "TCTables"
        Me.TCTables.SelectedIndex = 0
        Me.TCTables.Size = New System.Drawing.Size(784, 550)
        Me.TCTables.TabIndex = 2
        '
        'TPAllTables
        '
        Me.TPAllTables.AutoScroll = True
        Me.TPAllTables.Controls.Add(Me.pnlMiscTables)
        Me.TPAllTables.Controls.Add(Me.pnlAFSTables)
        Me.TPAllTables.Controls.Add(Me.pnlSSPPTables)
        Me.TPAllTables.Controls.Add(Me.pnlSSCPTables)
        Me.TPAllTables.Controls.Add(Me.pnlSBEAPTables)
        Me.TPAllTables.Controls.Add(Me.pnlISMPTables)
        Me.TPAllTables.Controls.Add(Me.pnlHeaderTables)
        Me.TPAllTables.Controls.Add(Me.pnlLookUpTables)
        Me.TPAllTables.Controls.Add(Me.Panel19)
        Me.TPAllTables.Location = New System.Drawing.Point(4, 22)
        Me.TPAllTables.Name = "TPAllTables"
        Me.TPAllTables.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAllTables.Size = New System.Drawing.Size(776, 524)
        Me.TPAllTables.TabIndex = 0
        Me.TPAllTables.Text = "All Tables"
        Me.TPAllTables.UseVisualStyleBackColor = True
        '
        'pnlMiscTables
        '
        Me.pnlMiscTables.AutoScroll = True
        Me.pnlMiscTables.Controls.Add(Me.chbUpdateEIEU)
        Me.pnlMiscTables.Controls.Add(Me.chbUpdateEIEP)
        Me.pnlMiscTables.Controls.Add(Me.chbUpdateEIEM)
        Me.pnlMiscTables.Controls.Add(Me.chbUpdateEIER)
        Me.pnlMiscTables.Controls.Add(Me.chbUpDateEISI)
        Me.pnlMiscTables.Controls.Add(Me.Label182)
        Me.pnlMiscTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMiscTables.Location = New System.Drawing.Point(1393, 30)
        Me.pnlMiscTables.Name = "pnlMiscTables"
        Me.pnlMiscTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlMiscTables.TabIndex = 42
        '
        'chbUpdateEIEU
        '
        Me.chbUpdateEIEU.AutoSize = True
        Me.chbUpdateEIEU.Location = New System.Drawing.Point(22, 57)
        Me.chbUpdateEIEU.Name = "chbUpdateEIEU"
        Me.chbUpdateEIEU.Size = New System.Drawing.Size(92, 17)
        Me.chbUpdateEIEU.TabIndex = 52
        Me.chbUpdateEIEU.Text = "Update EI EU"
        Me.chbUpdateEIEU.UseVisualStyleBackColor = True
        '
        'chbUpdateEIEP
        '
        Me.chbUpdateEIEP.AutoSize = True
        Me.chbUpdateEIEP.Location = New System.Drawing.Point(22, 83)
        Me.chbUpdateEIEP.Name = "chbUpdateEIEP"
        Me.chbUpdateEIEP.Size = New System.Drawing.Size(91, 17)
        Me.chbUpdateEIEP.TabIndex = 51
        Me.chbUpdateEIEP.Text = "Update EI EP"
        Me.chbUpdateEIEP.UseVisualStyleBackColor = True
        '
        'chbUpdateEIEM
        '
        Me.chbUpdateEIEM.AutoSize = True
        Me.chbUpdateEIEM.Location = New System.Drawing.Point(22, 106)
        Me.chbUpdateEIEM.Name = "chbUpdateEIEM"
        Me.chbUpdateEIEM.Size = New System.Drawing.Size(93, 17)
        Me.chbUpdateEIEM.TabIndex = 50
        Me.chbUpdateEIEM.Text = "Update EI EM"
        Me.chbUpdateEIEM.UseVisualStyleBackColor = True
        '
        'chbUpdateEIER
        '
        Me.chbUpdateEIER.AutoSize = True
        Me.chbUpdateEIER.Location = New System.Drawing.Point(22, 133)
        Me.chbUpdateEIER.Name = "chbUpdateEIER"
        Me.chbUpdateEIER.Size = New System.Drawing.Size(92, 17)
        Me.chbUpdateEIER.TabIndex = 49
        Me.chbUpdateEIER.Text = "Update EI ER"
        Me.chbUpdateEIER.UseVisualStyleBackColor = True
        '
        'chbUpDateEISI
        '
        Me.chbUpDateEISI.AutoSize = True
        Me.chbUpDateEISI.Location = New System.Drawing.Point(22, 34)
        Me.chbUpDateEISI.Name = "chbUpDateEISI"
        Me.chbUpDateEISI.Size = New System.Drawing.Size(87, 17)
        Me.chbUpDateEISI.TabIndex = 48
        Me.chbUpDateEISI.Text = "Update EI SI"
        Me.chbUpDateEISI.UseVisualStyleBackColor = True
        '
        'Label182
        '
        Me.Label182.AutoSize = True
        Me.Label182.Location = New System.Drawing.Point(4, 5)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(64, 13)
        Me.Label182.TabIndex = 0
        Me.Label182.Text = "Misc Tables"
        '
        'pnlAFSTables
        '
        Me.pnlAFSTables.AutoScroll = True
        Me.pnlAFSTables.Controls.Add(Me.chbAllAFSTables)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSSSPPRecords)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSSSCPRecords)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSAirPollutantData)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSBatchFiles)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSSSCPFCERecords)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSFacilityData)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSSSCPEnforcementRecords)
        Me.pnlAFSTables.Controls.Add(Me.chbAFSISMPRecords)
        Me.pnlAFSTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlAFSTables.Location = New System.Drawing.Point(1193, 30)
        Me.pnlAFSTables.Name = "pnlAFSTables"
        Me.pnlAFSTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlAFSTables.TabIndex = 41
        '
        'chbAllAFSTables
        '
        Me.chbAllAFSTables.AutoSize = True
        Me.chbAllAFSTables.Location = New System.Drawing.Point(3, 4)
        Me.chbAllAFSTables.Name = "chbAllAFSTables"
        Me.chbAllAFSTables.Size = New System.Drawing.Size(95, 17)
        Me.chbAllAFSTables.TabIndex = 73
        Me.chbAllAFSTables.Text = "All AFS Tables"
        Me.chbAllAFSTables.UseVisualStyleBackColor = True
        '
        'chbAFSSSPPRecords
        '
        Me.chbAFSSSPPRecords.AutoSize = True
        Me.chbAFSSSPPRecords.Location = New System.Drawing.Point(20, 217)
        Me.chbAFSSSPPRecords.Name = "chbAFSSSPPRecords"
        Me.chbAFSSSPPRecords.Size = New System.Drawing.Size(97, 17)
        Me.chbAFSSSPPRecords.TabIndex = 49
        Me.chbAFSSSPPRecords.Text = "SSPP Records"
        Me.chbAFSSSPPRecords.UseVisualStyleBackColor = True
        '
        'chbAFSSSCPRecords
        '
        Me.chbAFSSSCPRecords.AutoSize = True
        Me.chbAFSSSCPRecords.Location = New System.Drawing.Point(20, 189)
        Me.chbAFSSSCPRecords.Name = "chbAFSSSCPRecords"
        Me.chbAFSSSCPRecords.Size = New System.Drawing.Size(97, 17)
        Me.chbAFSSSCPRecords.TabIndex = 48
        Me.chbAFSSSCPRecords.Text = "SSCP Records"
        Me.chbAFSSSCPRecords.UseVisualStyleBackColor = True
        '
        'chbAFSAirPollutantData
        '
        Me.chbAFSAirPollutantData.AutoSize = True
        Me.chbAFSAirPollutantData.Location = New System.Drawing.Point(20, 21)
        Me.chbAFSAirPollutantData.Name = "chbAFSAirPollutantData"
        Me.chbAFSAirPollutantData.Size = New System.Drawing.Size(108, 17)
        Me.chbAFSAirPollutantData.TabIndex = 47
        Me.chbAFSAirPollutantData.Text = "Air Pollutant Data"
        Me.chbAFSAirPollutantData.UseVisualStyleBackColor = True
        '
        'chbAFSBatchFiles
        '
        Me.chbAFSBatchFiles.AutoSize = True
        Me.chbAFSBatchFiles.Location = New System.Drawing.Point(20, 49)
        Me.chbAFSBatchFiles.Name = "chbAFSBatchFiles"
        Me.chbAFSBatchFiles.Size = New System.Drawing.Size(78, 17)
        Me.chbAFSBatchFiles.TabIndex = 46
        Me.chbAFSBatchFiles.Text = "Batch Files"
        Me.chbAFSBatchFiles.UseVisualStyleBackColor = True
        '
        'chbAFSSSCPFCERecords
        '
        Me.chbAFSSSCPFCERecords.AutoSize = True
        Me.chbAFSSSCPFCERecords.Location = New System.Drawing.Point(20, 161)
        Me.chbAFSSSCPFCERecords.Name = "chbAFSSSCPFCERecords"
        Me.chbAFSSSCPFCERecords.Size = New System.Drawing.Size(120, 17)
        Me.chbAFSSSCPFCERecords.TabIndex = 42
        Me.chbAFSSSCPFCERecords.Text = "SSCP FCE Records"
        Me.chbAFSSSCPFCERecords.UseVisualStyleBackColor = True
        '
        'chbAFSFacilityData
        '
        Me.chbAFSFacilityData.AutoSize = True
        Me.chbAFSFacilityData.Location = New System.Drawing.Point(20, 77)
        Me.chbAFSFacilityData.Name = "chbAFSFacilityData"
        Me.chbAFSFacilityData.Size = New System.Drawing.Size(84, 17)
        Me.chbAFSFacilityData.TabIndex = 45
        Me.chbAFSFacilityData.Text = "Facility Data"
        Me.chbAFSFacilityData.UseVisualStyleBackColor = True
        '
        'chbAFSSSCPEnforcementRecords
        '
        Me.chbAFSSSCPEnforcementRecords.AutoSize = True
        Me.chbAFSSSCPEnforcementRecords.Location = New System.Drawing.Point(20, 133)
        Me.chbAFSSSCPEnforcementRecords.Name = "chbAFSSSCPEnforcementRecords"
        Me.chbAFSSSCPEnforcementRecords.Size = New System.Drawing.Size(160, 17)
        Me.chbAFSSSCPEnforcementRecords.TabIndex = 43
        Me.chbAFSSSCPEnforcementRecords.Text = "SSCP Enforcement Records"
        Me.chbAFSSSCPEnforcementRecords.UseVisualStyleBackColor = True
        '
        'chbAFSISMPRecords
        '
        Me.chbAFSISMPRecords.AutoSize = True
        Me.chbAFSISMPRecords.Location = New System.Drawing.Point(20, 105)
        Me.chbAFSISMPRecords.Name = "chbAFSISMPRecords"
        Me.chbAFSISMPRecords.Size = New System.Drawing.Size(95, 17)
        Me.chbAFSISMPRecords.TabIndex = 44
        Me.chbAFSISMPRecords.Text = "ISMP Records"
        Me.chbAFSISMPRecords.UseVisualStyleBackColor = True
        '
        'pnlSSPPTables
        '
        Me.pnlSSPPTables.AutoScroll = True
        Me.pnlSSPPTables.Controls.Add(Me.chbAllSSPPTables)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPPublicLetters)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPCDS)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationTracking)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationContact)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationData)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationQuality)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationInformation)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationMaster)
        Me.pnlSSPPTables.Controls.Add(Me.chbSSPPApplicationLinking)
        Me.pnlSSPPTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSSPPTables.Location = New System.Drawing.Point(993, 30)
        Me.pnlSSPPTables.Name = "pnlSSPPTables"
        Me.pnlSSPPTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlSSPPTables.TabIndex = 40
        '
        'chbAllSSPPTables
        '
        Me.chbAllSSPPTables.AutoSize = True
        Me.chbAllSSPPTables.Location = New System.Drawing.Point(3, 3)
        Me.chbAllSSPPTables.Name = "chbAllSSPPTables"
        Me.chbAllSSPPTables.Size = New System.Drawing.Size(103, 17)
        Me.chbAllSSPPTables.TabIndex = 73
        Me.chbAllSSPPTables.Text = "All SSPP Tables"
        Me.chbAllSSPPTables.UseVisualStyleBackColor = True
        '
        'chbSSPPPublicLetters
        '
        Me.chbSSPPPublicLetters.AutoSize = True
        Me.chbSSPPPublicLetters.Location = New System.Drawing.Point(9, 245)
        Me.chbSSPPPublicLetters.Name = "chbSSPPPublicLetters"
        Me.chbSSPPPublicLetters.Size = New System.Drawing.Size(90, 17)
        Me.chbSSPPPublicLetters.TabIndex = 51
        Me.chbSSPPPublicLetters.Text = "Public Letters"
        Me.chbSSPPPublicLetters.UseVisualStyleBackColor = True
        '
        'chbSSPPCDS
        '
        Me.chbSSPPCDS.AutoSize = True
        Me.chbSSPPCDS.Location = New System.Drawing.Point(9, 217)
        Me.chbSSPPCDS.Name = "chbSSPPCDS"
        Me.chbSSPPCDS.Size = New System.Drawing.Size(48, 17)
        Me.chbSSPPCDS.TabIndex = 50
        Me.chbSSPPCDS.Text = "CDS"
        Me.chbSSPPCDS.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationTracking
        '
        Me.chbSSPPApplicationTracking.AutoSize = True
        Me.chbSSPPApplicationTracking.Location = New System.Drawing.Point(9, 189)
        Me.chbSSPPApplicationTracking.Name = "chbSSPPApplicationTracking"
        Me.chbSSPPApplicationTracking.Size = New System.Drawing.Size(123, 17)
        Me.chbSSPPApplicationTracking.TabIndex = 49
        Me.chbSSPPApplicationTracking.Text = "Application Tracking"
        Me.chbSSPPApplicationTracking.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationContact
        '
        Me.chbSSPPApplicationContact.AutoSize = True
        Me.chbSSPPApplicationContact.Location = New System.Drawing.Point(9, 21)
        Me.chbSSPPApplicationContact.Name = "chbSSPPApplicationContact"
        Me.chbSSPPApplicationContact.Size = New System.Drawing.Size(118, 17)
        Me.chbSSPPApplicationContact.TabIndex = 48
        Me.chbSSPPApplicationContact.Text = "Application Contact"
        Me.chbSSPPApplicationContact.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationData
        '
        Me.chbSSPPApplicationData.AutoSize = True
        Me.chbSSPPApplicationData.Location = New System.Drawing.Point(9, 49)
        Me.chbSSPPApplicationData.Name = "chbSSPPApplicationData"
        Me.chbSSPPApplicationData.Size = New System.Drawing.Size(104, 17)
        Me.chbSSPPApplicationData.TabIndex = 47
        Me.chbSSPPApplicationData.Text = "Application Data"
        Me.chbSSPPApplicationData.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationQuality
        '
        Me.chbSSPPApplicationQuality.AutoSize = True
        Me.chbSSPPApplicationQuality.Location = New System.Drawing.Point(9, 161)
        Me.chbSSPPApplicationQuality.Name = "chbSSPPApplicationQuality"
        Me.chbSSPPApplicationQuality.Size = New System.Drawing.Size(113, 17)
        Me.chbSSPPApplicationQuality.TabIndex = 43
        Me.chbSSPPApplicationQuality.Text = "Application Quality"
        Me.chbSSPPApplicationQuality.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationInformation
        '
        Me.chbSSPPApplicationInformation.AutoSize = True
        Me.chbSSPPApplicationInformation.Location = New System.Drawing.Point(9, 77)
        Me.chbSSPPApplicationInformation.Name = "chbSSPPApplicationInformation"
        Me.chbSSPPApplicationInformation.Size = New System.Drawing.Size(133, 17)
        Me.chbSSPPApplicationInformation.TabIndex = 46
        Me.chbSSPPApplicationInformation.Text = "Application Information"
        Me.chbSSPPApplicationInformation.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationMaster
        '
        Me.chbSSPPApplicationMaster.AutoSize = True
        Me.chbSSPPApplicationMaster.Location = New System.Drawing.Point(9, 133)
        Me.chbSSPPApplicationMaster.Name = "chbSSPPApplicationMaster"
        Me.chbSSPPApplicationMaster.Size = New System.Drawing.Size(113, 17)
        Me.chbSSPPApplicationMaster.TabIndex = 44
        Me.chbSSPPApplicationMaster.Text = "Application Master"
        Me.chbSSPPApplicationMaster.UseVisualStyleBackColor = True
        '
        'chbSSPPApplicationLinking
        '
        Me.chbSSPPApplicationLinking.AutoSize = True
        Me.chbSSPPApplicationLinking.Location = New System.Drawing.Point(9, 105)
        Me.chbSSPPApplicationLinking.Name = "chbSSPPApplicationLinking"
        Me.chbSSPPApplicationLinking.Size = New System.Drawing.Size(115, 17)
        Me.chbSSPPApplicationLinking.TabIndex = 45
        Me.chbSSPPApplicationLinking.Text = "Application Linking"
        Me.chbSSPPApplicationLinking.UseVisualStyleBackColor = True
        '
        'pnlSSCPTables
        '
        Me.pnlSSCPTables.AutoScroll = True
        Me.pnlSSCPTables.Controls.Add(Me.chbAllSSCPTables)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPTestReports)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPReportsHistory)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPReports)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPNotifications)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPItemMaster)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPInspectionTracking)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPInspectionsRequired)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPInspections)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPInspectionActivity)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPFCEMaster)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPFCE)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPFacilityAssignment)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcementStipulated)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcementNOVComments)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcementLetter)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcementItems)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcementCOComments)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPACCS)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPACCSHistory)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcementAOComments)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPDistrictAssignment)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPEnforcement)
        Me.pnlSSCPTables.Controls.Add(Me.chbSSCPDistrictResponsible)
        Me.pnlSSCPTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSSCPTables.Location = New System.Drawing.Point(793, 30)
        Me.pnlSSCPTables.Name = "pnlSSCPTables"
        Me.pnlSSCPTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlSSCPTables.TabIndex = 39
        '
        'chbAllSSCPTables
        '
        Me.chbAllSSCPTables.AutoSize = True
        Me.chbAllSSCPTables.Location = New System.Drawing.Point(3, 3)
        Me.chbAllSSCPTables.Name = "chbAllSSCPTables"
        Me.chbAllSSCPTables.Size = New System.Drawing.Size(103, 17)
        Me.chbAllSSCPTables.TabIndex = 73
        Me.chbAllSSCPTables.Text = "All SSCP Tables"
        Me.chbAllSSCPTables.UseVisualStyleBackColor = True
        '
        'chbSSCPTestReports
        '
        Me.chbSSCPTestReports.AutoSize = True
        Me.chbSSCPTestReports.Location = New System.Drawing.Point(17, 621)
        Me.chbSSCPTestReports.Name = "chbSSCPTestReports"
        Me.chbSSCPTestReports.Size = New System.Drawing.Size(87, 17)
        Me.chbSSCPTestReports.TabIndex = 72
        Me.chbSSCPTestReports.Text = "Test Reports"
        Me.chbSSCPTestReports.UseVisualStyleBackColor = True
        '
        'chbSSCPReportsHistory
        '
        Me.chbSSCPReportsHistory.AutoSize = True
        Me.chbSSCPReportsHistory.Location = New System.Drawing.Point(17, 593)
        Me.chbSSCPReportsHistory.Name = "chbSSCPReportsHistory"
        Me.chbSSCPReportsHistory.Size = New System.Drawing.Size(98, 17)
        Me.chbSSCPReportsHistory.TabIndex = 71
        Me.chbSSCPReportsHistory.Text = "Reports History"
        Me.chbSSCPReportsHistory.UseVisualStyleBackColor = True
        '
        'chbSSCPReports
        '
        Me.chbSSCPReports.AutoSize = True
        Me.chbSSCPReports.Location = New System.Drawing.Point(17, 565)
        Me.chbSSCPReports.Name = "chbSSCPReports"
        Me.chbSSCPReports.Size = New System.Drawing.Size(63, 17)
        Me.chbSSCPReports.TabIndex = 70
        Me.chbSSCPReports.Text = "Reports"
        Me.chbSSCPReports.UseVisualStyleBackColor = True
        '
        'chbSSCPNotifications
        '
        Me.chbSSCPNotifications.AutoSize = True
        Me.chbSSCPNotifications.Location = New System.Drawing.Point(17, 537)
        Me.chbSSCPNotifications.Name = "chbSSCPNotifications"
        Me.chbSSCPNotifications.Size = New System.Drawing.Size(84, 17)
        Me.chbSSCPNotifications.TabIndex = 69
        Me.chbSSCPNotifications.Text = "Notifications"
        Me.chbSSCPNotifications.UseVisualStyleBackColor = True
        '
        'chbSSCPItemMaster
        '
        Me.chbSSCPItemMaster.AutoSize = True
        Me.chbSSCPItemMaster.Location = New System.Drawing.Point(17, 509)
        Me.chbSSCPItemMaster.Name = "chbSSCPItemMaster"
        Me.chbSSCPItemMaster.Size = New System.Drawing.Size(81, 17)
        Me.chbSSCPItemMaster.TabIndex = 68
        Me.chbSSCPItemMaster.Text = "Item Master"
        Me.chbSSCPItemMaster.UseVisualStyleBackColor = True
        '
        'chbSSCPInspectionTracking
        '
        Me.chbSSCPInspectionTracking.AutoSize = True
        Me.chbSSCPInspectionTracking.Location = New System.Drawing.Point(17, 481)
        Me.chbSSCPInspectionTracking.Name = "chbSSCPInspectionTracking"
        Me.chbSSCPInspectionTracking.Size = New System.Drawing.Size(120, 17)
        Me.chbSSCPInspectionTracking.TabIndex = 67
        Me.chbSSCPInspectionTracking.Text = "Inspection Tracking"
        Me.chbSSCPInspectionTracking.UseVisualStyleBackColor = True
        '
        'chbSSCPInspectionsRequired
        '
        Me.chbSSCPInspectionsRequired.AutoSize = True
        Me.chbSSCPInspectionsRequired.Location = New System.Drawing.Point(17, 453)
        Me.chbSSCPInspectionsRequired.Name = "chbSSCPInspectionsRequired"
        Me.chbSSCPInspectionsRequired.Size = New System.Drawing.Size(126, 17)
        Me.chbSSCPInspectionsRequired.TabIndex = 66
        Me.chbSSCPInspectionsRequired.Text = "Inspections Required"
        Me.chbSSCPInspectionsRequired.UseVisualStyleBackColor = True
        '
        'chbSSCPInspections
        '
        Me.chbSSCPInspections.AutoSize = True
        Me.chbSSCPInspections.Location = New System.Drawing.Point(17, 430)
        Me.chbSSCPInspections.Name = "chbSSCPInspections"
        Me.chbSSCPInspections.Size = New System.Drawing.Size(80, 17)
        Me.chbSSCPInspections.TabIndex = 65
        Me.chbSSCPInspections.Text = "Inspections"
        Me.chbSSCPInspections.UseVisualStyleBackColor = True
        '
        'chbSSCPInspectionActivity
        '
        Me.chbSSCPInspectionActivity.AutoSize = True
        Me.chbSSCPInspectionActivity.Location = New System.Drawing.Point(17, 404)
        Me.chbSSCPInspectionActivity.Name = "chbSSCPInspectionActivity"
        Me.chbSSCPInspectionActivity.Size = New System.Drawing.Size(112, 17)
        Me.chbSSCPInspectionActivity.TabIndex = 64
        Me.chbSSCPInspectionActivity.Text = "Inspection Activity"
        Me.chbSSCPInspectionActivity.UseVisualStyleBackColor = True
        '
        'chbSSCPFCEMaster
        '
        Me.chbSSCPFCEMaster.AutoSize = True
        Me.chbSSCPFCEMaster.Location = New System.Drawing.Point(17, 378)
        Me.chbSSCPFCEMaster.Name = "chbSSCPFCEMaster"
        Me.chbSSCPFCEMaster.Size = New System.Drawing.Size(81, 17)
        Me.chbSSCPFCEMaster.TabIndex = 63
        Me.chbSSCPFCEMaster.Text = "FCE Master"
        Me.chbSSCPFCEMaster.UseVisualStyleBackColor = True
        '
        'chbSSCPFCE
        '
        Me.chbSSCPFCE.AutoSize = True
        Me.chbSSCPFCE.Location = New System.Drawing.Point(17, 352)
        Me.chbSSCPFCE.Name = "chbSSCPFCE"
        Me.chbSSCPFCE.Size = New System.Drawing.Size(46, 17)
        Me.chbSSCPFCE.TabIndex = 62
        Me.chbSSCPFCE.Text = "FCE"
        Me.chbSSCPFCE.UseVisualStyleBackColor = True
        '
        'chbSSCPFacilityAssignment
        '
        Me.chbSSCPFacilityAssignment.AutoSize = True
        Me.chbSSCPFacilityAssignment.Location = New System.Drawing.Point(17, 326)
        Me.chbSSCPFacilityAssignment.Name = "chbSSCPFacilityAssignment"
        Me.chbSSCPFacilityAssignment.Size = New System.Drawing.Size(121, 17)
        Me.chbSSCPFacilityAssignment.TabIndex = 61
        Me.chbSSCPFacilityAssignment.Text = "Facility Assignement"
        Me.chbSSCPFacilityAssignment.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcementStipulated
        '
        Me.chbSSCPEnforcementStipulated.AutoSize = True
        Me.chbSSCPEnforcementStipulated.Location = New System.Drawing.Point(17, 300)
        Me.chbSSCPEnforcementStipulated.Name = "chbSSCPEnforcementStipulated"
        Me.chbSSCPEnforcementStipulated.Size = New System.Drawing.Size(136, 17)
        Me.chbSSCPEnforcementStipulated.TabIndex = 60
        Me.chbSSCPEnforcementStipulated.Text = "Enforcement Stipulated"
        Me.chbSSCPEnforcementStipulated.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcementNOVComments
        '
        Me.chbSSCPEnforcementNOVComments.AutoSize = True
        Me.chbSSCPEnforcementNOVComments.Location = New System.Drawing.Point(17, 274)
        Me.chbSSCPEnforcementNOVComments.Name = "chbSSCPEnforcementNOVComments"
        Me.chbSSCPEnforcementNOVComments.Size = New System.Drawing.Size(164, 17)
        Me.chbSSCPEnforcementNOVComments.TabIndex = 59
        Me.chbSSCPEnforcementNOVComments.Text = "Enforcement NOV Comments"
        Me.chbSSCPEnforcementNOVComments.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcementLetter
        '
        Me.chbSSCPEnforcementLetter.AutoSize = True
        Me.chbSSCPEnforcementLetter.Location = New System.Drawing.Point(17, 246)
        Me.chbSSCPEnforcementLetter.Name = "chbSSCPEnforcementLetter"
        Me.chbSSCPEnforcementLetter.Size = New System.Drawing.Size(113, 17)
        Me.chbSSCPEnforcementLetter.TabIndex = 58
        Me.chbSSCPEnforcementLetter.Text = "Enfocement Letter"
        Me.chbSSCPEnforcementLetter.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcementItems
        '
        Me.chbSSCPEnforcementItems.AutoSize = True
        Me.chbSSCPEnforcementItems.Location = New System.Drawing.Point(17, 218)
        Me.chbSSCPEnforcementItems.Name = "chbSSCPEnforcementItems"
        Me.chbSSCPEnforcementItems.Size = New System.Drawing.Size(114, 17)
        Me.chbSSCPEnforcementItems.TabIndex = 57
        Me.chbSSCPEnforcementItems.Text = "Enforcement Items"
        Me.chbSSCPEnforcementItems.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcementCOComments
        '
        Me.chbSSCPEnforcementCOComments.AutoSize = True
        Me.chbSSCPEnforcementCOComments.Location = New System.Drawing.Point(17, 190)
        Me.chbSSCPEnforcementCOComments.Name = "chbSSCPEnforcementCOComments"
        Me.chbSSCPEnforcementCOComments.Size = New System.Drawing.Size(156, 17)
        Me.chbSSCPEnforcementCOComments.TabIndex = 56
        Me.chbSSCPEnforcementCOComments.Text = "Enformcenet CO Comments"
        Me.chbSSCPEnforcementCOComments.UseVisualStyleBackColor = True
        '
        'chbSSCPACCS
        '
        Me.chbSSCPACCS.AutoSize = True
        Me.chbSSCPACCS.Location = New System.Drawing.Point(17, 22)
        Me.chbSSCPACCS.Name = "chbSSCPACCS"
        Me.chbSSCPACCS.Size = New System.Drawing.Size(54, 17)
        Me.chbSSCPACCS.TabIndex = 55
        Me.chbSSCPACCS.Text = "ACCS"
        Me.chbSSCPACCS.UseVisualStyleBackColor = True
        '
        'chbSSCPACCSHistory
        '
        Me.chbSSCPACCSHistory.AutoSize = True
        Me.chbSSCPACCSHistory.Location = New System.Drawing.Point(17, 50)
        Me.chbSSCPACCSHistory.Name = "chbSSCPACCSHistory"
        Me.chbSSCPACCSHistory.Size = New System.Drawing.Size(89, 17)
        Me.chbSSCPACCSHistory.TabIndex = 54
        Me.chbSSCPACCSHistory.Text = "ACCS History"
        Me.chbSSCPACCSHistory.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcementAOComments
        '
        Me.chbSSCPEnforcementAOComments.AutoSize = True
        Me.chbSSCPEnforcementAOComments.Location = New System.Drawing.Point(17, 162)
        Me.chbSSCPEnforcementAOComments.Name = "chbSSCPEnforcementAOComments"
        Me.chbSSCPEnforcementAOComments.Size = New System.Drawing.Size(156, 17)
        Me.chbSSCPEnforcementAOComments.TabIndex = 50
        Me.chbSSCPEnforcementAOComments.Text = "Enforcement AO Comments"
        Me.chbSSCPEnforcementAOComments.UseVisualStyleBackColor = True
        '
        'chbSSCPDistrictAssignment
        '
        Me.chbSSCPDistrictAssignment.AutoSize = True
        Me.chbSSCPDistrictAssignment.Location = New System.Drawing.Point(17, 78)
        Me.chbSSCPDistrictAssignment.Name = "chbSSCPDistrictAssignment"
        Me.chbSSCPDistrictAssignment.Size = New System.Drawing.Size(115, 17)
        Me.chbSSCPDistrictAssignment.TabIndex = 53
        Me.chbSSCPDistrictAssignment.Text = "District Assignment"
        Me.chbSSCPDistrictAssignment.UseVisualStyleBackColor = True
        '
        'chbSSCPEnforcement
        '
        Me.chbSSCPEnforcement.AutoSize = True
        Me.chbSSCPEnforcement.Location = New System.Drawing.Point(17, 134)
        Me.chbSSCPEnforcement.Name = "chbSSCPEnforcement"
        Me.chbSSCPEnforcement.Size = New System.Drawing.Size(86, 17)
        Me.chbSSCPEnforcement.TabIndex = 51
        Me.chbSSCPEnforcement.Text = "Enforcement"
        Me.chbSSCPEnforcement.UseVisualStyleBackColor = True
        '
        'chbSSCPDistrictResponsible
        '
        Me.chbSSCPDistrictResponsible.AutoSize = True
        Me.chbSSCPDistrictResponsible.Location = New System.Drawing.Point(17, 106)
        Me.chbSSCPDistrictResponsible.Name = "chbSSCPDistrictResponsible"
        Me.chbSSCPDistrictResponsible.Size = New System.Drawing.Size(119, 17)
        Me.chbSSCPDistrictResponsible.TabIndex = 52
        Me.chbSSCPDistrictResponsible.Text = "District Responsible"
        Me.chbSSCPDistrictResponsible.UseVisualStyleBackColor = True
        '
        'pnlSBEAPTables
        '
        Me.pnlSBEAPTables.AutoScroll = True
        Me.pnlSBEAPTables.Controls.Add(Me.chbAllSBEAPTables)
        Me.pnlSBEAPTables.Controls.Add(Me.chbHBSBEAPClients)
        Me.pnlSBEAPTables.Controls.Add(Me.chbHBSBEAPClientData)
        Me.pnlSBEAPTables.Controls.Add(Me.chbSBEAPCaseLog)
        Me.pnlSBEAPTables.Controls.Add(Me.chbSBEAPClientContacts)
        Me.pnlSBEAPTables.Controls.Add(Me.chbSBEAPErrorLog)
        Me.pnlSBEAPTables.Controls.Add(Me.chbSBEAPClientData)
        Me.pnlSBEAPTables.Controls.Add(Me.chbSBEAPClients)
        Me.pnlSBEAPTables.Controls.Add(Me.chbSBEAPClientLink)
        Me.pnlSBEAPTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSBEAPTables.Location = New System.Drawing.Point(593, 30)
        Me.pnlSBEAPTables.Name = "pnlSBEAPTables"
        Me.pnlSBEAPTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlSBEAPTables.TabIndex = 38
        '
        'chbAllSBEAPTables
        '
        Me.chbAllSBEAPTables.AutoSize = True
        Me.chbAllSBEAPTables.Location = New System.Drawing.Point(3, 3)
        Me.chbAllSBEAPTables.Name = "chbAllSBEAPTables"
        Me.chbAllSBEAPTables.Size = New System.Drawing.Size(110, 17)
        Me.chbAllSBEAPTables.TabIndex = 72
        Me.chbAllSBEAPTables.Text = "All SBEAP Tables"
        Me.chbAllSBEAPTables.UseVisualStyleBackColor = True
        '
        'chbHBSBEAPClients
        '
        Me.chbHBSBEAPClients.AutoSize = True
        Me.chbHBSBEAPClients.Location = New System.Drawing.Point(18, 217)
        Me.chbHBSBEAPClients.Name = "chbHBSBEAPClients"
        Me.chbHBSBEAPClients.Size = New System.Drawing.Size(113, 17)
        Me.chbHBSBEAPClients.TabIndex = 48
        Me.chbHBSBEAPClients.Text = "HB SBEAP Clients"
        Me.chbHBSBEAPClients.UseVisualStyleBackColor = True
        '
        'chbHBSBEAPClientData
        '
        Me.chbHBSBEAPClientData.AutoSize = True
        Me.chbHBSBEAPClientData.Location = New System.Drawing.Point(18, 189)
        Me.chbHBSBEAPClientData.Name = "chbHBSBEAPClientData"
        Me.chbHBSBEAPClientData.Size = New System.Drawing.Size(134, 17)
        Me.chbHBSBEAPClientData.TabIndex = 49
        Me.chbHBSBEAPClientData.Text = "HB SBEAP Client Data"
        Me.chbHBSBEAPClientData.UseVisualStyleBackColor = True
        '
        'chbSBEAPCaseLog
        '
        Me.chbSBEAPCaseLog.AutoSize = True
        Me.chbSBEAPCaseLog.Location = New System.Drawing.Point(18, 21)
        Me.chbSBEAPCaseLog.Name = "chbSBEAPCaseLog"
        Me.chbSBEAPCaseLog.Size = New System.Drawing.Size(71, 17)
        Me.chbSBEAPCaseLog.TabIndex = 47
        Me.chbSBEAPCaseLog.Text = "Case Log"
        Me.chbSBEAPCaseLog.UseVisualStyleBackColor = True
        '
        'chbSBEAPClientContacts
        '
        Me.chbSBEAPClientContacts.AutoSize = True
        Me.chbSBEAPClientContacts.Location = New System.Drawing.Point(18, 49)
        Me.chbSBEAPClientContacts.Name = "chbSBEAPClientContacts"
        Me.chbSBEAPClientContacts.Size = New System.Drawing.Size(97, 17)
        Me.chbSBEAPClientContacts.TabIndex = 46
        Me.chbSBEAPClientContacts.Text = "Client Contacts"
        Me.chbSBEAPClientContacts.UseVisualStyleBackColor = True
        '
        'chbSBEAPErrorLog
        '
        Me.chbSBEAPErrorLog.AutoSize = True
        Me.chbSBEAPErrorLog.Location = New System.Drawing.Point(18, 161)
        Me.chbSBEAPErrorLog.Name = "chbSBEAPErrorLog"
        Me.chbSBEAPErrorLog.Size = New System.Drawing.Size(69, 17)
        Me.chbSBEAPErrorLog.TabIndex = 42
        Me.chbSBEAPErrorLog.Text = "Error Log"
        Me.chbSBEAPErrorLog.UseVisualStyleBackColor = True
        '
        'chbSBEAPClientData
        '
        Me.chbSBEAPClientData.AutoSize = True
        Me.chbSBEAPClientData.Location = New System.Drawing.Point(18, 77)
        Me.chbSBEAPClientData.Name = "chbSBEAPClientData"
        Me.chbSBEAPClientData.Size = New System.Drawing.Size(78, 17)
        Me.chbSBEAPClientData.TabIndex = 45
        Me.chbSBEAPClientData.Text = "Client Data"
        Me.chbSBEAPClientData.UseVisualStyleBackColor = True
        '
        'chbSBEAPClients
        '
        Me.chbSBEAPClients.AutoSize = True
        Me.chbSBEAPClients.Location = New System.Drawing.Point(18, 133)
        Me.chbSBEAPClients.Name = "chbSBEAPClients"
        Me.chbSBEAPClients.Size = New System.Drawing.Size(57, 17)
        Me.chbSBEAPClients.TabIndex = 43
        Me.chbSBEAPClients.Text = "Clients"
        Me.chbSBEAPClients.UseVisualStyleBackColor = True
        '
        'chbSBEAPClientLink
        '
        Me.chbSBEAPClientLink.AutoSize = True
        Me.chbSBEAPClientLink.Location = New System.Drawing.Point(18, 105)
        Me.chbSBEAPClientLink.Name = "chbSBEAPClientLink"
        Me.chbSBEAPClientLink.Size = New System.Drawing.Size(75, 17)
        Me.chbSBEAPClientLink.TabIndex = 44
        Me.chbSBEAPClientLink.Text = "Client Link"
        Me.chbSBEAPClientLink.UseVisualStyleBackColor = True
        '
        'pnlISMPTables
        '
        Me.pnlISMPTables.AutoScroll = True
        Me.pnlISMPTables.Controls.Add(Me.chbAllISMPTables)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPWitnessingEng)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestReportMemo)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestReportAids)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestNotificationLog)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestNotification)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestLogNumber)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestLogLink)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPTestFirmComments)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportType)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportTwoStack)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportRATA)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportPondAndGas)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportOpacity)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportOneStack)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportMemo)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPDocumentTypes)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPFacilityAssignment)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportInformation)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPMaster)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReportFlare)
        Me.pnlISMPTables.Controls.Add(Me.chbISMPReferenceNumber)
        Me.pnlISMPTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlISMPTables.Location = New System.Drawing.Point(393, 30)
        Me.pnlISMPTables.Name = "pnlISMPTables"
        Me.pnlISMPTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlISMPTables.TabIndex = 37
        '
        'chbAllISMPTables
        '
        Me.chbAllISMPTables.AutoSize = True
        Me.chbAllISMPTables.Location = New System.Drawing.Point(6, 6)
        Me.chbAllISMPTables.Name = "chbAllISMPTables"
        Me.chbAllISMPTables.Size = New System.Drawing.Size(101, 17)
        Me.chbAllISMPTables.TabIndex = 71
        Me.chbAllISMPTables.Text = "All ISMP Tables"
        Me.chbAllISMPTables.UseVisualStyleBackColor = True
        '
        'chbISMPWitnessingEng
        '
        Me.chbISMPWitnessingEng.AutoSize = True
        Me.chbISMPWitnessingEng.Location = New System.Drawing.Point(15, 572)
        Me.chbISMPWitnessingEng.Name = "chbISMPWitnessingEng"
        Me.chbISMPWitnessingEng.Size = New System.Drawing.Size(100, 17)
        Me.chbISMPWitnessingEng.TabIndex = 70
        Me.chbISMPWitnessingEng.Text = "Witnessing Eng"
        Me.chbISMPWitnessingEng.UseVisualStyleBackColor = True
        '
        'chbISMPTestReportMemo
        '
        Me.chbISMPTestReportMemo.AutoSize = True
        Me.chbISMPTestReportMemo.Location = New System.Drawing.Point(15, 544)
        Me.chbISMPTestReportMemo.Name = "chbISMPTestReportMemo"
        Me.chbISMPTestReportMemo.Size = New System.Drawing.Size(114, 17)
        Me.chbISMPTestReportMemo.TabIndex = 69
        Me.chbISMPTestReportMemo.Text = "Test Report Memo"
        Me.chbISMPTestReportMemo.UseVisualStyleBackColor = True
        '
        'chbISMPTestReportAids
        '
        Me.chbISMPTestReportAids.AutoSize = True
        Me.chbISMPTestReportAids.Location = New System.Drawing.Point(15, 516)
        Me.chbISMPTestReportAids.Name = "chbISMPTestReportAids"
        Me.chbISMPTestReportAids.Size = New System.Drawing.Size(105, 17)
        Me.chbISMPTestReportAids.TabIndex = 68
        Me.chbISMPTestReportAids.Text = "Test Report Aids"
        Me.chbISMPTestReportAids.UseVisualStyleBackColor = True
        '
        'chbISMPTestNotificationLog
        '
        Me.chbISMPTestNotificationLog.AutoSize = True
        Me.chbISMPTestNotificationLog.Location = New System.Drawing.Point(15, 488)
        Me.chbISMPTestNotificationLog.Name = "chbISMPTestNotificationLog"
        Me.chbISMPTestNotificationLog.Size = New System.Drawing.Size(124, 17)
        Me.chbISMPTestNotificationLog.TabIndex = 67
        Me.chbISMPTestNotificationLog.Text = "Test Notification Log"
        Me.chbISMPTestNotificationLog.UseVisualStyleBackColor = True
        '
        'chbISMPTestNotification
        '
        Me.chbISMPTestNotification.AutoSize = True
        Me.chbISMPTestNotification.Location = New System.Drawing.Point(15, 460)
        Me.chbISMPTestNotification.Name = "chbISMPTestNotification"
        Me.chbISMPTestNotification.Size = New System.Drawing.Size(103, 17)
        Me.chbISMPTestNotification.TabIndex = 66
        Me.chbISMPTestNotification.Text = "Test Notification"
        Me.chbISMPTestNotification.UseVisualStyleBackColor = True
        '
        'chbISMPTestLogNumber
        '
        Me.chbISMPTestLogNumber.AutoSize = True
        Me.chbISMPTestLogNumber.Location = New System.Drawing.Point(15, 435)
        Me.chbISMPTestLogNumber.Name = "chbISMPTestLogNumber"
        Me.chbISMPTestLogNumber.Size = New System.Drawing.Size(108, 17)
        Me.chbISMPTestLogNumber.TabIndex = 65
        Me.chbISMPTestLogNumber.Text = "Test Log Number"
        Me.chbISMPTestLogNumber.UseVisualStyleBackColor = True
        '
        'chbISMPTestLogLink
        '
        Me.chbISMPTestLogLink.AutoSize = True
        Me.chbISMPTestLogLink.Location = New System.Drawing.Point(15, 409)
        Me.chbISMPTestLogLink.Name = "chbISMPTestLogLink"
        Me.chbISMPTestLogLink.Size = New System.Drawing.Size(91, 17)
        Me.chbISMPTestLogLink.TabIndex = 64
        Me.chbISMPTestLogLink.Text = "Test Log Link"
        Me.chbISMPTestLogLink.UseVisualStyleBackColor = True
        '
        'chbISMPTestFirmComments
        '
        Me.chbISMPTestFirmComments.AutoSize = True
        Me.chbISMPTestFirmComments.Location = New System.Drawing.Point(15, 383)
        Me.chbISMPTestFirmComments.Name = "chbISMPTestFirmComments"
        Me.chbISMPTestFirmComments.Size = New System.Drawing.Size(121, 17)
        Me.chbISMPTestFirmComments.TabIndex = 63
        Me.chbISMPTestFirmComments.Text = "Test Firm Comments"
        Me.chbISMPTestFirmComments.UseVisualStyleBackColor = True
        '
        'chbISMPReportType
        '
        Me.chbISMPReportType.AutoSize = True
        Me.chbISMPReportType.Location = New System.Drawing.Point(15, 357)
        Me.chbISMPReportType.Name = "chbISMPReportType"
        Me.chbISMPReportType.Size = New System.Drawing.Size(85, 17)
        Me.chbISMPReportType.TabIndex = 62
        Me.chbISMPReportType.Text = "Report Type"
        Me.chbISMPReportType.UseVisualStyleBackColor = True
        '
        'chbISMPReportTwoStack
        '
        Me.chbISMPReportTwoStack.AutoSize = True
        Me.chbISMPReportTwoStack.Location = New System.Drawing.Point(15, 331)
        Me.chbISMPReportTwoStack.Name = "chbISMPReportTwoStack"
        Me.chbISMPReportTwoStack.Size = New System.Drawing.Size(113, 17)
        Me.chbISMPReportTwoStack.TabIndex = 61
        Me.chbISMPReportTwoStack.Text = "Report Two Stack"
        Me.chbISMPReportTwoStack.UseVisualStyleBackColor = True
        '
        'chbISMPReportRATA
        '
        Me.chbISMPReportRATA.AutoSize = True
        Me.chbISMPReportRATA.Location = New System.Drawing.Point(15, 305)
        Me.chbISMPReportRATA.Name = "chbISMPReportRATA"
        Me.chbISMPReportRATA.Size = New System.Drawing.Size(90, 17)
        Me.chbISMPReportRATA.TabIndex = 60
        Me.chbISMPReportRATA.Text = "Report RATA"
        Me.chbISMPReportRATA.UseVisualStyleBackColor = True
        '
        'chbISMPReportPondAndGas
        '
        Me.chbISMPReportPondAndGas.AutoSize = True
        Me.chbISMPReportPondAndGas.Location = New System.Drawing.Point(15, 279)
        Me.chbISMPReportPondAndGas.Name = "chbISMPReportPondAndGas"
        Me.chbISMPReportPondAndGas.Size = New System.Drawing.Size(129, 17)
        Me.chbISMPReportPondAndGas.TabIndex = 59
        Me.chbISMPReportPondAndGas.Text = "Report Pond and Gas"
        Me.chbISMPReportPondAndGas.UseVisualStyleBackColor = True
        '
        'chbISMPReportOpacity
        '
        Me.chbISMPReportOpacity.AutoSize = True
        Me.chbISMPReportOpacity.Location = New System.Drawing.Point(15, 251)
        Me.chbISMPReportOpacity.Name = "chbISMPReportOpacity"
        Me.chbISMPReportOpacity.Size = New System.Drawing.Size(97, 17)
        Me.chbISMPReportOpacity.TabIndex = 58
        Me.chbISMPReportOpacity.Text = "Report Opacity"
        Me.chbISMPReportOpacity.UseVisualStyleBackColor = True
        '
        'chbISMPReportOneStack
        '
        Me.chbISMPReportOneStack.AutoSize = True
        Me.chbISMPReportOneStack.Location = New System.Drawing.Point(15, 223)
        Me.chbISMPReportOneStack.Name = "chbISMPReportOneStack"
        Me.chbISMPReportOneStack.Size = New System.Drawing.Size(112, 17)
        Me.chbISMPReportOneStack.TabIndex = 57
        Me.chbISMPReportOneStack.Text = "Report One Stack"
        Me.chbISMPReportOneStack.UseVisualStyleBackColor = True
        '
        'chbISMPReportMemo
        '
        Me.chbISMPReportMemo.AutoSize = True
        Me.chbISMPReportMemo.Location = New System.Drawing.Point(15, 195)
        Me.chbISMPReportMemo.Name = "chbISMPReportMemo"
        Me.chbISMPReportMemo.Size = New System.Drawing.Size(90, 17)
        Me.chbISMPReportMemo.TabIndex = 56
        Me.chbISMPReportMemo.Text = "Report Memo"
        Me.chbISMPReportMemo.UseVisualStyleBackColor = True
        '
        'chbISMPDocumentTypes
        '
        Me.chbISMPDocumentTypes.AutoSize = True
        Me.chbISMPDocumentTypes.Location = New System.Drawing.Point(15, 27)
        Me.chbISMPDocumentTypes.Name = "chbISMPDocumentTypes"
        Me.chbISMPDocumentTypes.Size = New System.Drawing.Size(107, 17)
        Me.chbISMPDocumentTypes.TabIndex = 55
        Me.chbISMPDocumentTypes.Text = "Document Types"
        Me.chbISMPDocumentTypes.UseVisualStyleBackColor = True
        '
        'chbISMPFacilityAssignment
        '
        Me.chbISMPFacilityAssignment.AutoSize = True
        Me.chbISMPFacilityAssignment.Location = New System.Drawing.Point(15, 55)
        Me.chbISMPFacilityAssignment.Name = "chbISMPFacilityAssignment"
        Me.chbISMPFacilityAssignment.Size = New System.Drawing.Size(115, 17)
        Me.chbISMPFacilityAssignment.TabIndex = 54
        Me.chbISMPFacilityAssignment.Text = "Facility Assignment"
        Me.chbISMPFacilityAssignment.UseVisualStyleBackColor = True
        '
        'chbISMPReportInformation
        '
        Me.chbISMPReportInformation.AutoSize = True
        Me.chbISMPReportInformation.Location = New System.Drawing.Point(15, 167)
        Me.chbISMPReportInformation.Name = "chbISMPReportInformation"
        Me.chbISMPReportInformation.Size = New System.Drawing.Size(113, 17)
        Me.chbISMPReportInformation.TabIndex = 50
        Me.chbISMPReportInformation.Text = "Report Information"
        Me.chbISMPReportInformation.UseVisualStyleBackColor = True
        '
        'chbISMPMaster
        '
        Me.chbISMPMaster.AutoSize = True
        Me.chbISMPMaster.Location = New System.Drawing.Point(15, 83)
        Me.chbISMPMaster.Name = "chbISMPMaster"
        Me.chbISMPMaster.Size = New System.Drawing.Size(58, 17)
        Me.chbISMPMaster.TabIndex = 53
        Me.chbISMPMaster.Text = "Master"
        Me.chbISMPMaster.UseVisualStyleBackColor = True
        '
        'chbISMPReportFlare
        '
        Me.chbISMPReportFlare.AutoSize = True
        Me.chbISMPReportFlare.Location = New System.Drawing.Point(15, 139)
        Me.chbISMPReportFlare.Name = "chbISMPReportFlare"
        Me.chbISMPReportFlare.Size = New System.Drawing.Size(84, 17)
        Me.chbISMPReportFlare.TabIndex = 51
        Me.chbISMPReportFlare.Text = "Report Flare"
        Me.chbISMPReportFlare.UseVisualStyleBackColor = True
        '
        'chbISMPReferenceNumber
        '
        Me.chbISMPReferenceNumber.AutoSize = True
        Me.chbISMPReferenceNumber.Location = New System.Drawing.Point(15, 111)
        Me.chbISMPReferenceNumber.Name = "chbISMPReferenceNumber"
        Me.chbISMPReferenceNumber.Size = New System.Drawing.Size(116, 17)
        Me.chbISMPReferenceNumber.TabIndex = 52
        Me.chbISMPReferenceNumber.Text = "Reference Number"
        Me.chbISMPReferenceNumber.UseVisualStyleBackColor = True
        '
        'pnlHeaderTables
        '
        Me.pnlHeaderTables.AutoScroll = True
        Me.pnlHeaderTables.Controls.Add(Me.chbEPDUsers)
        Me.pnlHeaderTables.Controls.Add(Me.chbAllHeaderTables)
        Me.pnlHeaderTables.Controls.Add(Me.chbHBAPBHeaderData)
        Me.pnlHeaderTables.Controls.Add(Me.chbHBAPBFacilityInformation)
        Me.pnlHeaderTables.Controls.Add(Me.chbHBAPBAirProgramPollutants)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPPMaster)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBSupplamentalData)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBSubPartData)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBPermits)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBMasterAPP)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBAirProgramPollutants)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBContactInformation)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBMasterAIRS)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBFacilityInformation)
        Me.pnlHeaderTables.Controls.Add(Me.chbAPBHeaderData)
        Me.pnlHeaderTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHeaderTables.Location = New System.Drawing.Point(193, 30)
        Me.pnlHeaderTables.Name = "pnlHeaderTables"
        Me.pnlHeaderTables.Size = New System.Drawing.Size(200, 474)
        Me.pnlHeaderTables.TabIndex = 36
        '
        'chbEPDUsers
        '
        Me.chbEPDUsers.AutoSize = True
        Me.chbEPDUsers.Location = New System.Drawing.Point(18, 430)
        Me.chbEPDUsers.Name = "chbEPDUsers"
        Me.chbEPDUsers.Size = New System.Drawing.Size(78, 17)
        Me.chbEPDUsers.TabIndex = 67
        Me.chbEPDUsers.Text = "EDP Users"
        Me.chbEPDUsers.UseVisualStyleBackColor = True
        '
        'chbAllHeaderTables
        '
        Me.chbAllHeaderTables.AutoSize = True
        Me.chbAllHeaderTables.Location = New System.Drawing.Point(6, 6)
        Me.chbAllHeaderTables.Name = "chbAllHeaderTables"
        Me.chbAllHeaderTables.Size = New System.Drawing.Size(110, 17)
        Me.chbAllHeaderTables.TabIndex = 66
        Me.chbAllHeaderTables.Text = "All Header Tables"
        Me.chbAllHeaderTables.UseVisualStyleBackColor = True
        '
        'chbHBAPBHeaderData
        '
        Me.chbHBAPBHeaderData.AutoSize = True
        Me.chbHBAPBHeaderData.Location = New System.Drawing.Point(19, 402)
        Me.chbHBAPBHeaderData.Name = "chbHBAPBHeaderData"
        Me.chbHBAPBHeaderData.Size = New System.Drawing.Size(129, 17)
        Me.chbHBAPBHeaderData.TabIndex = 65
        Me.chbHBAPBHeaderData.Text = "HB APB Header Data"
        Me.chbHBAPBHeaderData.UseVisualStyleBackColor = True
        '
        'chbHBAPBFacilityInformation
        '
        Me.chbHBAPBFacilityInformation.AutoSize = True
        Me.chbHBAPBFacilityInformation.Location = New System.Drawing.Point(19, 376)
        Me.chbHBAPBFacilityInformation.Name = "chbHBAPBFacilityInformation"
        Me.chbHBAPBFacilityInformation.Size = New System.Drawing.Size(155, 17)
        Me.chbHBAPBFacilityInformation.TabIndex = 64
        Me.chbHBAPBFacilityInformation.Text = "HB APB Facility Information"
        Me.chbHBAPBFacilityInformation.UseVisualStyleBackColor = True
        '
        'chbHBAPBAirProgramPollutants
        '
        Me.chbHBAPBAirProgramPollutants.AutoSize = True
        Me.chbHBAPBAirProgramPollutants.Location = New System.Drawing.Point(19, 350)
        Me.chbHBAPBAirProgramPollutants.Name = "chbHBAPBAirProgramPollutants"
        Me.chbHBAPBAirProgramPollutants.Size = New System.Drawing.Size(171, 17)
        Me.chbHBAPBAirProgramPollutants.TabIndex = 63
        Me.chbHBAPBAirProgramPollutants.Text = "HB APB Air Program Pollutants"
        Me.chbHBAPBAirProgramPollutants.UseVisualStyleBackColor = True
        '
        'chbAPPMaster
        '
        Me.chbAPPMaster.AutoSize = True
        Me.chbAPPMaster.Location = New System.Drawing.Point(19, 324)
        Me.chbAPPMaster.Name = "chbAPPMaster"
        Me.chbAPPMaster.Size = New System.Drawing.Size(82, 17)
        Me.chbAPPMaster.TabIndex = 62
        Me.chbAPPMaster.Text = "APP Master"
        Me.chbAPPMaster.UseVisualStyleBackColor = True
        '
        'chbAPBSupplamentalData
        '
        Me.chbAPBSupplamentalData.AutoSize = True
        Me.chbAPBSupplamentalData.Location = New System.Drawing.Point(19, 246)
        Me.chbAPBSupplamentalData.Name = "chbAPBSupplamentalData"
        Me.chbAPBSupplamentalData.Size = New System.Drawing.Size(140, 17)
        Me.chbAPBSupplamentalData.TabIndex = 59
        Me.chbAPBSupplamentalData.Text = "APB Supplamental Data"
        Me.chbAPBSupplamentalData.UseVisualStyleBackColor = True
        '
        'chbAPBSubPartData
        '
        Me.chbAPBSubPartData.AutoSize = True
        Me.chbAPBSubPartData.Location = New System.Drawing.Point(19, 218)
        Me.chbAPBSubPartData.Name = "chbAPBSubPartData"
        Me.chbAPBSubPartData.Size = New System.Drawing.Size(117, 17)
        Me.chbAPBSubPartData.TabIndex = 58
        Me.chbAPBSubPartData.Text = "APB Sub Part Data"
        Me.chbAPBSubPartData.UseVisualStyleBackColor = True
        '
        'chbAPBPermits
        '
        Me.chbAPBPermits.AutoSize = True
        Me.chbAPBPermits.Location = New System.Drawing.Point(19, 190)
        Me.chbAPBPermits.Name = "chbAPBPermits"
        Me.chbAPBPermits.Size = New System.Drawing.Size(84, 17)
        Me.chbAPBPermits.TabIndex = 57
        Me.chbAPBPermits.Text = "APB Permits"
        Me.chbAPBPermits.UseVisualStyleBackColor = True
        '
        'chbAPBMasterAPP
        '
        Me.chbAPBMasterAPP.AutoSize = True
        Me.chbAPBMasterAPP.Location = New System.Drawing.Point(19, 162)
        Me.chbAPBMasterAPP.Name = "chbAPBMasterAPP"
        Me.chbAPBMasterAPP.Size = New System.Drawing.Size(106, 17)
        Me.chbAPBMasterAPP.TabIndex = 56
        Me.chbAPBMasterAPP.Text = "APB Master APP"
        Me.chbAPBMasterAPP.UseVisualStyleBackColor = True
        '
        'chbAPBAirProgramPollutants
        '
        Me.chbAPBAirProgramPollutants.AutoSize = True
        Me.chbAPBAirProgramPollutants.Location = New System.Drawing.Point(19, 27)
        Me.chbAPBAirProgramPollutants.Name = "chbAPBAirProgramPollutants"
        Me.chbAPBAirProgramPollutants.Size = New System.Drawing.Size(153, 17)
        Me.chbAPBAirProgramPollutants.TabIndex = 55
        Me.chbAPBAirProgramPollutants.Text = "APB Air Program Pollutants"
        Me.chbAPBAirProgramPollutants.UseVisualStyleBackColor = True
        '
        'chbAPBContactInformation
        '
        Me.chbAPBContactInformation.AutoSize = True
        Me.chbAPBContactInformation.Location = New System.Drawing.Point(19, 55)
        Me.chbAPBContactInformation.Name = "chbAPBContactInformation"
        Me.chbAPBContactInformation.Size = New System.Drawing.Size(142, 17)
        Me.chbAPBContactInformation.TabIndex = 54
        Me.chbAPBContactInformation.Text = "APB Contact Information"
        Me.chbAPBContactInformation.UseVisualStyleBackColor = True
        '
        'chbAPBMasterAIRS
        '
        Me.chbAPBMasterAIRS.AutoSize = True
        Me.chbAPBMasterAIRS.Location = New System.Drawing.Point(19, 134)
        Me.chbAPBMasterAIRS.Name = "chbAPBMasterAIRS"
        Me.chbAPBMasterAIRS.Size = New System.Drawing.Size(110, 17)
        Me.chbAPBMasterAIRS.TabIndex = 50
        Me.chbAPBMasterAIRS.Text = "APB Master AIRS"
        Me.chbAPBMasterAIRS.UseVisualStyleBackColor = True
        '
        'chbAPBFacilityInformation
        '
        Me.chbAPBFacilityInformation.AutoSize = True
        Me.chbAPBFacilityInformation.Location = New System.Drawing.Point(19, 83)
        Me.chbAPBFacilityInformation.Name = "chbAPBFacilityInformation"
        Me.chbAPBFacilityInformation.Size = New System.Drawing.Size(137, 17)
        Me.chbAPBFacilityInformation.TabIndex = 53
        Me.chbAPBFacilityInformation.Text = "APB Facility Information"
        Me.chbAPBFacilityInformation.UseVisualStyleBackColor = True
        '
        'chbAPBHeaderData
        '
        Me.chbAPBHeaderData.AutoSize = True
        Me.chbAPBHeaderData.Location = New System.Drawing.Point(19, 106)
        Me.chbAPBHeaderData.Name = "chbAPBHeaderData"
        Me.chbAPBHeaderData.Size = New System.Drawing.Size(111, 17)
        Me.chbAPBHeaderData.TabIndex = 51
        Me.chbAPBHeaderData.Text = "APB Header Data"
        Me.chbAPBHeaderData.UseVisualStyleBackColor = True
        '
        'pnlLookUpTables
        '
        Me.pnlLookUpTables.AutoScroll = True
        Me.pnlLookUpTables.Controls.Add(Me.chbFSNSPSReason)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpISMPMethods)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpUnits)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpTestingFirms)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSubPartSIP)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSubPart63)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSubPart61)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSubPart60)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpStates)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSSCPNotifications)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSICCodes)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpSBEAPCaseWork)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpPollutants)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpPermitTypes)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpPermittingUnits)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpNonAttainment)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpMonitoringUnits)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpComplianceStatus)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpComplianceUnits)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpComplianceActivities)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpCountyInformation)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpApplicationType)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpAPBManagement)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpDistrictInformation)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpDistrictOffice)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpDistricts)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpEPDBranches)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpEPDPrograms)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpEPDUnits)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpHPVViolations)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpIAIPAccounts)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpIAIPForms)
        Me.pnlLookUpTables.Controls.Add(Me.chbLookUpISMPComplianceStatus)
        Me.pnlLookUpTables.Controls.Add(Me.chbAllLookUpTables)
        Me.pnlLookUpTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLookUpTables.Location = New System.Drawing.Point(3, 30)
        Me.pnlLookUpTables.Name = "pnlLookUpTables"
        Me.pnlLookUpTables.Size = New System.Drawing.Size(190, 474)
        Me.pnlLookUpTables.TabIndex = 35
        '
        'chbFSNSPSReason
        '
        Me.chbFSNSPSReason.AutoSize = True
        Me.chbFSNSPSReason.Location = New System.Drawing.Point(13, 895)
        Me.chbFSNSPSReason.Name = "chbFSNSPSReason"
        Me.chbFSNSPSReason.Size = New System.Drawing.Size(95, 17)
        Me.chbFSNSPSReason.TabIndex = 83
        Me.chbFSNSPSReason.Text = "NSPS Reason"
        Me.chbFSNSPSReason.UseVisualStyleBackColor = True
        '
        'chbLookUpISMPMethods
        '
        Me.chbLookUpISMPMethods.AutoSize = True
        Me.chbLookUpISMPMethods.Location = New System.Drawing.Point(15, 465)
        Me.chbLookUpISMPMethods.Name = "chbLookUpISMPMethods"
        Me.chbLookUpISMPMethods.Size = New System.Drawing.Size(96, 17)
        Me.chbLookUpISMPMethods.TabIndex = 67
        Me.chbLookUpISMPMethods.Text = "ISMP Methods"
        Me.chbLookUpISMPMethods.UseVisualStyleBackColor = True
        '
        'chbLookUpUnits
        '
        Me.chbLookUpUnits.AutoSize = True
        Me.chbLookUpUnits.Location = New System.Drawing.Point(14, 873)
        Me.chbLookUpUnits.Name = "chbLookUpUnits"
        Me.chbLookUpUnits.Size = New System.Drawing.Size(50, 17)
        Me.chbLookUpUnits.TabIndex = 82
        Me.chbLookUpUnits.Text = "Units"
        Me.chbLookUpUnits.UseVisualStyleBackColor = True
        '
        'chbLookUpTestingFirms
        '
        Me.chbLookUpTestingFirms.AutoSize = True
        Me.chbLookUpTestingFirms.Location = New System.Drawing.Point(14, 847)
        Me.chbLookUpTestingFirms.Name = "chbLookUpTestingFirms"
        Me.chbLookUpTestingFirms.Size = New System.Drawing.Size(88, 17)
        Me.chbLookUpTestingFirms.TabIndex = 81
        Me.chbLookUpTestingFirms.Text = "Testing Firms"
        Me.chbLookUpTestingFirms.UseVisualStyleBackColor = True
        '
        'chbLookUpSubPartSIP
        '
        Me.chbLookUpSubPartSIP.AutoSize = True
        Me.chbLookUpSubPartSIP.Location = New System.Drawing.Point(14, 821)
        Me.chbLookUpSubPartSIP.Name = "chbLookUpSubPartSIP"
        Me.chbLookUpSubPartSIP.Size = New System.Drawing.Size(87, 17)
        Me.chbLookUpSubPartSIP.TabIndex = 80
        Me.chbLookUpSubPartSIP.Text = "Sub Part SIP"
        Me.chbLookUpSubPartSIP.UseVisualStyleBackColor = True
        '
        'chbLookUpSubPart63
        '
        Me.chbLookUpSubPart63.AutoSize = True
        Me.chbLookUpSubPart63.Location = New System.Drawing.Point(14, 795)
        Me.chbLookUpSubPart63.Name = "chbLookUpSubPart63"
        Me.chbLookUpSubPart63.Size = New System.Drawing.Size(82, 17)
        Me.chbLookUpSubPart63.TabIndex = 79
        Me.chbLookUpSubPart63.Text = "Sub Part 63"
        Me.chbLookUpSubPart63.UseVisualStyleBackColor = True
        '
        'chbLookUpSubPart61
        '
        Me.chbLookUpSubPart61.AutoSize = True
        Me.chbLookUpSubPart61.Location = New System.Drawing.Point(14, 769)
        Me.chbLookUpSubPart61.Name = "chbLookUpSubPart61"
        Me.chbLookUpSubPart61.Size = New System.Drawing.Size(82, 17)
        Me.chbLookUpSubPart61.TabIndex = 78
        Me.chbLookUpSubPart61.Text = "Sub Part 61"
        Me.chbLookUpSubPart61.UseVisualStyleBackColor = True
        '
        'chbLookUpSubPart60
        '
        Me.chbLookUpSubPart60.AutoSize = True
        Me.chbLookUpSubPart60.Location = New System.Drawing.Point(15, 743)
        Me.chbLookUpSubPart60.Name = "chbLookUpSubPart60"
        Me.chbLookUpSubPart60.Size = New System.Drawing.Size(82, 17)
        Me.chbLookUpSubPart60.TabIndex = 77
        Me.chbLookUpSubPart60.Text = "Sub Part 60"
        Me.chbLookUpSubPart60.UseVisualStyleBackColor = True
        '
        'chbLookUpStates
        '
        Me.chbLookUpStates.AutoSize = True
        Me.chbLookUpStates.Location = New System.Drawing.Point(15, 717)
        Me.chbLookUpStates.Name = "chbLookUpStates"
        Me.chbLookUpStates.Size = New System.Drawing.Size(56, 17)
        Me.chbLookUpStates.TabIndex = 76
        Me.chbLookUpStates.Text = "States"
        Me.chbLookUpStates.UseVisualStyleBackColor = True
        '
        'chbLookUpSSCPNotifications
        '
        Me.chbLookUpSSCPNotifications.AutoSize = True
        Me.chbLookUpSSCPNotifications.Location = New System.Drawing.Point(15, 689)
        Me.chbLookUpSSCPNotifications.Name = "chbLookUpSSCPNotifications"
        Me.chbLookUpSSCPNotifications.Size = New System.Drawing.Size(115, 17)
        Me.chbLookUpSSCPNotifications.TabIndex = 75
        Me.chbLookUpSSCPNotifications.Text = "SSCP Notifications"
        Me.chbLookUpSSCPNotifications.UseVisualStyleBackColor = True
        '
        'chbLookUpSICCodes
        '
        Me.chbLookUpSICCodes.AutoSize = True
        Me.chbLookUpSICCodes.Location = New System.Drawing.Point(15, 661)
        Me.chbLookUpSICCodes.Name = "chbLookUpSICCodes"
        Me.chbLookUpSICCodes.Size = New System.Drawing.Size(76, 17)
        Me.chbLookUpSICCodes.TabIndex = 74
        Me.chbLookUpSICCodes.Text = "SIC Codes"
        Me.chbLookUpSICCodes.UseVisualStyleBackColor = True
        '
        'chbLookUpSBEAPCaseWork
        '
        Me.chbLookUpSBEAPCaseWork.AutoSize = True
        Me.chbLookUpSBEAPCaseWork.Location = New System.Drawing.Point(15, 633)
        Me.chbLookUpSBEAPCaseWork.Name = "chbLookUpSBEAPCaseWork"
        Me.chbLookUpSBEAPCaseWork.Size = New System.Drawing.Size(117, 17)
        Me.chbLookUpSBEAPCaseWork.TabIndex = 73
        Me.chbLookUpSBEAPCaseWork.Text = "SBEAP Case Work"
        Me.chbLookUpSBEAPCaseWork.UseVisualStyleBackColor = True
        '
        'chbLookUpPollutants
        '
        Me.chbLookUpPollutants.AutoSize = True
        Me.chbLookUpPollutants.Location = New System.Drawing.Point(15, 605)
        Me.chbLookUpPollutants.Name = "chbLookUpPollutants"
        Me.chbLookUpPollutants.Size = New System.Drawing.Size(72, 17)
        Me.chbLookUpPollutants.TabIndex = 72
        Me.chbLookUpPollutants.Text = "Pollutants"
        Me.chbLookUpPollutants.UseVisualStyleBackColor = True
        '
        'chbLookUpPermitTypes
        '
        Me.chbLookUpPermitTypes.AutoSize = True
        Me.chbLookUpPermitTypes.Location = New System.Drawing.Point(15, 577)
        Me.chbLookUpPermitTypes.Name = "chbLookUpPermitTypes"
        Me.chbLookUpPermitTypes.Size = New System.Drawing.Size(87, 17)
        Me.chbLookUpPermitTypes.TabIndex = 71
        Me.chbLookUpPermitTypes.Text = "Permit Types"
        Me.chbLookUpPermitTypes.UseVisualStyleBackColor = True
        '
        'chbLookUpPermittingUnits
        '
        Me.chbLookUpPermittingUnits.AutoSize = True
        Me.chbLookUpPermittingUnits.Location = New System.Drawing.Point(15, 549)
        Me.chbLookUpPermittingUnits.Name = "chbLookUpPermittingUnits"
        Me.chbLookUpPermittingUnits.Size = New System.Drawing.Size(99, 17)
        Me.chbLookUpPermittingUnits.TabIndex = 70
        Me.chbLookUpPermittingUnits.Text = "Permitting Units"
        Me.chbLookUpPermittingUnits.UseVisualStyleBackColor = True
        '
        'chbLookUpNonAttainment
        '
        Me.chbLookUpNonAttainment.AutoSize = True
        Me.chbLookUpNonAttainment.Location = New System.Drawing.Point(15, 521)
        Me.chbLookUpNonAttainment.Name = "chbLookUpNonAttainment"
        Me.chbLookUpNonAttainment.Size = New System.Drawing.Size(99, 17)
        Me.chbLookUpNonAttainment.TabIndex = 69
        Me.chbLookUpNonAttainment.Text = "Non Attainment"
        Me.chbLookUpNonAttainment.UseVisualStyleBackColor = True
        '
        'chbLookUpMonitoringUnits
        '
        Me.chbLookUpMonitoringUnits.AutoSize = True
        Me.chbLookUpMonitoringUnits.Location = New System.Drawing.Point(15, 493)
        Me.chbLookUpMonitoringUnits.Name = "chbLookUpMonitoringUnits"
        Me.chbLookUpMonitoringUnits.Size = New System.Drawing.Size(102, 17)
        Me.chbLookUpMonitoringUnits.TabIndex = 68
        Me.chbLookUpMonitoringUnits.Text = "Monitoring Units"
        Me.chbLookUpMonitoringUnits.UseVisualStyleBackColor = True
        '
        'chbLookUpComplianceStatus
        '
        Me.chbLookUpComplianceStatus.AutoSize = True
        Me.chbLookUpComplianceStatus.Location = New System.Drawing.Point(15, 118)
        Me.chbLookUpComplianceStatus.Name = "chbLookUpComplianceStatus"
        Me.chbLookUpComplianceStatus.Size = New System.Drawing.Size(114, 17)
        Me.chbLookUpComplianceStatus.TabIndex = 53
        Me.chbLookUpComplianceStatus.Text = "Compliance Status"
        Me.chbLookUpComplianceStatus.UseVisualStyleBackColor = True
        '
        'chbLookUpComplianceUnits
        '
        Me.chbLookUpComplianceUnits.AutoSize = True
        Me.chbLookUpComplianceUnits.Location = New System.Drawing.Point(15, 146)
        Me.chbLookUpComplianceUnits.Name = "chbLookUpComplianceUnits"
        Me.chbLookUpComplianceUnits.Size = New System.Drawing.Size(108, 17)
        Me.chbLookUpComplianceUnits.TabIndex = 52
        Me.chbLookUpComplianceUnits.Text = "Compliance Units"
        Me.chbLookUpComplianceUnits.UseVisualStyleBackColor = True
        '
        'chbLookUpComplianceActivities
        '
        Me.chbLookUpComplianceActivities.AutoSize = True
        Me.chbLookUpComplianceActivities.Location = New System.Drawing.Point(15, 90)
        Me.chbLookUpComplianceActivities.Name = "chbLookUpComplianceActivities"
        Me.chbLookUpComplianceActivities.Size = New System.Drawing.Size(126, 17)
        Me.chbLookUpComplianceActivities.TabIndex = 54
        Me.chbLookUpComplianceActivities.Text = "Compliance Activities"
        Me.chbLookUpComplianceActivities.UseVisualStyleBackColor = True
        '
        'chbLookUpCountyInformation
        '
        Me.chbLookUpCountyInformation.AutoSize = True
        Me.chbLookUpCountyInformation.Location = New System.Drawing.Point(15, 174)
        Me.chbLookUpCountyInformation.Name = "chbLookUpCountyInformation"
        Me.chbLookUpCountyInformation.Size = New System.Drawing.Size(117, 17)
        Me.chbLookUpCountyInformation.TabIndex = 51
        Me.chbLookUpCountyInformation.Text = "Country Information"
        Me.chbLookUpCountyInformation.UseVisualStyleBackColor = True
        '
        'chbLookUpApplicationType
        '
        Me.chbLookUpApplicationType.AutoSize = True
        Me.chbLookUpApplicationType.Location = New System.Drawing.Point(15, 62)
        Me.chbLookUpApplicationType.Name = "chbLookUpApplicationType"
        Me.chbLookUpApplicationType.Size = New System.Drawing.Size(105, 17)
        Me.chbLookUpApplicationType.TabIndex = 55
        Me.chbLookUpApplicationType.Text = "Application Type"
        Me.chbLookUpApplicationType.UseVisualStyleBackColor = True
        '
        'chbLookUpAPBManagement
        '
        Me.chbLookUpAPBManagement.AutoSize = True
        Me.chbLookUpAPBManagement.Location = New System.Drawing.Point(15, 34)
        Me.chbLookUpAPBManagement.Name = "chbLookUpAPBManagement"
        Me.chbLookUpAPBManagement.Size = New System.Drawing.Size(112, 17)
        Me.chbLookUpAPBManagement.TabIndex = 56
        Me.chbLookUpAPBManagement.Text = "APB Management"
        Me.chbLookUpAPBManagement.UseVisualStyleBackColor = True
        '
        'chbLookUpDistrictInformation
        '
        Me.chbLookUpDistrictInformation.AutoSize = True
        Me.chbLookUpDistrictInformation.Location = New System.Drawing.Point(15, 202)
        Me.chbLookUpDistrictInformation.Name = "chbLookUpDistrictInformation"
        Me.chbLookUpDistrictInformation.Size = New System.Drawing.Size(113, 17)
        Me.chbLookUpDistrictInformation.TabIndex = 57
        Me.chbLookUpDistrictInformation.Text = "District Information"
        Me.chbLookUpDistrictInformation.UseVisualStyleBackColor = True
        '
        'chbLookUpDistrictOffice
        '
        Me.chbLookUpDistrictOffice.AutoSize = True
        Me.chbLookUpDistrictOffice.Location = New System.Drawing.Point(15, 230)
        Me.chbLookUpDistrictOffice.Name = "chbLookUpDistrictOffice"
        Me.chbLookUpDistrictOffice.Size = New System.Drawing.Size(89, 17)
        Me.chbLookUpDistrictOffice.TabIndex = 58
        Me.chbLookUpDistrictOffice.Text = "District Office"
        Me.chbLookUpDistrictOffice.UseVisualStyleBackColor = True
        '
        'chbLookUpDistricts
        '
        Me.chbLookUpDistricts.AutoSize = True
        Me.chbLookUpDistricts.Location = New System.Drawing.Point(15, 258)
        Me.chbLookUpDistricts.Name = "chbLookUpDistricts"
        Me.chbLookUpDistricts.Size = New System.Drawing.Size(63, 17)
        Me.chbLookUpDistricts.TabIndex = 59
        Me.chbLookUpDistricts.Text = "Districts"
        Me.chbLookUpDistricts.UseVisualStyleBackColor = True
        '
        'chbLookUpEPDBranches
        '
        Me.chbLookUpEPDBranches.AutoSize = True
        Me.chbLookUpEPDBranches.Location = New System.Drawing.Point(15, 286)
        Me.chbLookUpEPDBranches.Name = "chbLookUpEPDBranches"
        Me.chbLookUpEPDBranches.Size = New System.Drawing.Size(96, 17)
        Me.chbLookUpEPDBranches.TabIndex = 60
        Me.chbLookUpEPDBranches.Text = "EPD Branches"
        Me.chbLookUpEPDBranches.UseVisualStyleBackColor = True
        '
        'chbLookUpEPDPrograms
        '
        Me.chbLookUpEPDPrograms.AutoSize = True
        Me.chbLookUpEPDPrograms.Location = New System.Drawing.Point(15, 312)
        Me.chbLookUpEPDPrograms.Name = "chbLookUpEPDPrograms"
        Me.chbLookUpEPDPrograms.Size = New System.Drawing.Size(95, 17)
        Me.chbLookUpEPDPrograms.TabIndex = 61
        Me.chbLookUpEPDPrograms.Text = "EPD Programs"
        Me.chbLookUpEPDPrograms.UseVisualStyleBackColor = True
        '
        'chbLookUpEPDUnits
        '
        Me.chbLookUpEPDUnits.AutoSize = True
        Me.chbLookUpEPDUnits.Location = New System.Drawing.Point(15, 338)
        Me.chbLookUpEPDUnits.Name = "chbLookUpEPDUnits"
        Me.chbLookUpEPDUnits.Size = New System.Drawing.Size(75, 17)
        Me.chbLookUpEPDUnits.TabIndex = 62
        Me.chbLookUpEPDUnits.Text = "EPD Units"
        Me.chbLookUpEPDUnits.UseVisualStyleBackColor = True
        '
        'chbLookUpHPVViolations
        '
        Me.chbLookUpHPVViolations.AutoSize = True
        Me.chbLookUpHPVViolations.Location = New System.Drawing.Point(15, 364)
        Me.chbLookUpHPVViolations.Name = "chbLookUpHPVViolations"
        Me.chbLookUpHPVViolations.Size = New System.Drawing.Size(96, 17)
        Me.chbLookUpHPVViolations.TabIndex = 63
        Me.chbLookUpHPVViolations.Text = "HPV Violations"
        Me.chbLookUpHPVViolations.UseVisualStyleBackColor = True
        '
        'chbLookUpIAIPAccounts
        '
        Me.chbLookUpIAIPAccounts.AutoSize = True
        Me.chbLookUpIAIPAccounts.Location = New System.Drawing.Point(15, 390)
        Me.chbLookUpIAIPAccounts.Name = "chbLookUpIAIPAccounts"
        Me.chbLookUpIAIPAccounts.Size = New System.Drawing.Size(94, 17)
        Me.chbLookUpIAIPAccounts.TabIndex = 64
        Me.chbLookUpIAIPAccounts.Text = "IAIP Accounts"
        Me.chbLookUpIAIPAccounts.UseVisualStyleBackColor = True
        '
        'chbLookUpIAIPForms
        '
        Me.chbLookUpIAIPForms.AutoSize = True
        Me.chbLookUpIAIPForms.Location = New System.Drawing.Point(15, 416)
        Me.chbLookUpIAIPForms.Name = "chbLookUpIAIPForms"
        Me.chbLookUpIAIPForms.Size = New System.Drawing.Size(77, 17)
        Me.chbLookUpIAIPForms.TabIndex = 65
        Me.chbLookUpIAIPForms.Text = "IAIP Forms"
        Me.chbLookUpIAIPForms.UseVisualStyleBackColor = True
        '
        'chbLookUpISMPComplianceStatus
        '
        Me.chbLookUpISMPComplianceStatus.AutoSize = True
        Me.chbLookUpISMPComplianceStatus.Location = New System.Drawing.Point(15, 442)
        Me.chbLookUpISMPComplianceStatus.Name = "chbLookUpISMPComplianceStatus"
        Me.chbLookUpISMPComplianceStatus.Size = New System.Drawing.Size(143, 17)
        Me.chbLookUpISMPComplianceStatus.TabIndex = 66
        Me.chbLookUpISMPComplianceStatus.Text = "ISMP Compliance Status"
        Me.chbLookUpISMPComplianceStatus.UseVisualStyleBackColor = True
        '
        'chbAllLookUpTables
        '
        Me.chbAllLookUpTables.AutoSize = True
        Me.chbAllLookUpTables.Location = New System.Drawing.Point(5, 6)
        Me.chbAllLookUpTables.Name = "chbAllLookUpTables"
        Me.chbAllLookUpTables.Size = New System.Drawing.Size(116, 17)
        Me.chbAllLookUpTables.TabIndex = 50
        Me.chbAllLookUpTables.Text = "All Look Up Tables"
        Me.chbAllLookUpTables.UseVisualStyleBackColor = True
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.chbAllTables)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(3, 3)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(1587, 27)
        Me.Panel19.TabIndex = 43
        '
        'chbAllTables
        '
        Me.chbAllTables.AutoSize = True
        Me.chbAllTables.Location = New System.Drawing.Point(4, 6)
        Me.chbAllTables.Name = "chbAllTables"
        Me.chbAllTables.Size = New System.Drawing.Size(72, 17)
        Me.chbAllTables.TabIndex = 2
        Me.chbAllTables.Text = "All Tables"
        Me.chbAllTables.UseVisualStyleBackColor = True
        '
        'PanelSources
        '
        Me.PanelSources.AutoSize = True
        Me.PanelSources.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelSources.Controls.Add(Me.lblTransfer)
        Me.PanelSources.Controls.Add(Me.btnClearSelection)
        Me.PanelSources.Controls.Add(Me.btnTransferData)
        Me.PanelSources.Controls.Add(Me.Panel20)
        Me.PanelSources.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSources.Location = New System.Drawing.Point(0, 0)
        Me.PanelSources.Name = "PanelSources"
        Me.PanelSources.Size = New System.Drawing.Size(784, 64)
        Me.PanelSources.TabIndex = 3
        '
        'lblTransfer
        '
        Me.lblTransfer.AutoSize = True
        Me.lblTransfer.Location = New System.Drawing.Point(212, 17)
        Me.lblTransfer.Name = "lblTransfer"
        Me.lblTransfer.Size = New System.Drawing.Size(101, 13)
        Me.lblTransfer.TabIndex = 3
        Me.lblTransfer.Text = "Make selects below"
        '
        'btnClearSelection
        '
        Me.btnClearSelection.Location = New System.Drawing.Point(592, 12)
        Me.btnClearSelection.Name = "btnClearSelection"
        Me.btnClearSelection.Size = New System.Drawing.Size(75, 23)
        Me.btnClearSelection.TabIndex = 2
        Me.btnClearSelection.Text = "Clear Selection"
        Me.btnClearSelection.UseVisualStyleBackColor = True
        '
        'btnTransferData
        '
        Me.btnTransferData.AutoSize = True
        Me.btnTransferData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnTransferData.Location = New System.Drawing.Point(122, 12)
        Me.btnTransferData.Name = "btnTransferData"
        Me.btnTransferData.Size = New System.Drawing.Size(82, 23)
        Me.btnTransferData.TabIndex = 1
        Me.btnTransferData.Text = "Transfer Data"
        Me.btnTransferData.UseVisualStyleBackColor = True
        '
        'Panel20
        '
        Me.Panel20.AutoSize = True
        Me.Panel20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel20.Controls.Add(Me.rdbTESTTransfer)
        Me.Panel20.Controls.Add(Me.rdbDEVTransfer)
        Me.Panel20.Location = New System.Drawing.Point(8, 3)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(101, 58)
        Me.Panel20.TabIndex = 0
        '
        'rdbTESTTransfer
        '
        Me.rdbTESTTransfer.AutoSize = True
        Me.rdbTESTTransfer.Location = New System.Drawing.Point(4, 38)
        Me.rdbTESTTransfer.Name = "rdbTESTTransfer"
        Me.rdbTESTTransfer.Size = New System.Drawing.Size(94, 17)
        Me.rdbTESTTransfer.TabIndex = 2
        Me.rdbTESTTransfer.Text = "PRD --> TEST"
        Me.rdbTESTTransfer.UseVisualStyleBackColor = True
        '
        'rdbDEVTransfer
        '
        Me.rdbDEVTransfer.AutoSize = True
        Me.rdbDEVTransfer.Checked = True
        Me.rdbDEVTransfer.Location = New System.Drawing.Point(4, 3)
        Me.rdbDEVTransfer.Name = "rdbDEVTransfer"
        Me.rdbDEVTransfer.Size = New System.Drawing.Size(88, 17)
        Me.rdbDEVTransfer.TabIndex = 1
        Me.rdbDEVTransfer.TabStop = True
        Me.rdbDEVTransfer.Text = "PRD --> DEV"
        Me.rdbDEVTransfer.UseVisualStyleBackColor = True
        '
        'TSDMUStaffTools
        '
        Me.TSDMUStaffTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack})
        Me.TSDMUStaffTools.Location = New System.Drawing.Point(0, 0)
        Me.TSDMUStaffTools.Name = "TSDMUStaffTools"
        Me.TSDMUStaffTools.Size = New System.Drawing.Size(792, 25)
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
        'DMUDeveloperTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 687)
        Me.Controls.Add(Me.TCDMUTools)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TSDMUStaffTools)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "DMUDeveloperTools"
        Me.Text = "DMU Developer"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TCDMUTools.ResumeLayout(False)
        Me.TPWebErrorLog.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgrWebErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPErrorLog.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.dgvErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPAddNewFacility.ResumeLayout(False)
        Me.TPAddNewFacility.PerformLayout()
        Me.GBContactInformation.ResumeLayout(False)
        Me.GBContactInformation.PerformLayout()
        Me.GBAirProgramCodes.ResumeLayout(False)
        Me.GBAirProgramCodes.PerformLayout()
        Me.GBHeaderData.ResumeLayout(False)
        Me.GBHeaderData.PerformLayout()
        Me.GBMailingLocation.ResumeLayout(False)
        Me.GBMailingLocation.PerformLayout()
        Me.GBFacilityInformation.ResumeLayout(False)
        Me.GBFacilityInformation.PerformLayout()
        Me.TPAFSFileGenerator.ResumeLayout(False)
        Me.TPAFSFileGenerator.PerformLayout()
        Me.PanelBatchOrder.ResumeLayout(False)
        Me.PanelBatchOrder.PerformLayout()
        Me.TPUpdateDEVTest.ResumeLayout(False)
        Me.TPUpdateDEVTest.PerformLayout()
        Me.TCTables.ResumeLayout(False)
        Me.TPAllTables.ResumeLayout(False)
        Me.pnlMiscTables.ResumeLayout(False)
        Me.pnlMiscTables.PerformLayout()
        Me.pnlAFSTables.ResumeLayout(False)
        Me.pnlAFSTables.PerformLayout()
        Me.pnlSSPPTables.ResumeLayout(False)
        Me.pnlSSPPTables.PerformLayout()
        Me.pnlSSCPTables.ResumeLayout(False)
        Me.pnlSSCPTables.PerformLayout()
        Me.pnlSBEAPTables.ResumeLayout(False)
        Me.pnlSBEAPTables.PerformLayout()
        Me.pnlISMPTables.ResumeLayout(False)
        Me.pnlISMPTables.PerformLayout()
        Me.pnlHeaderTables.ResumeLayout(False)
        Me.pnlHeaderTables.PerformLayout()
        Me.pnlLookUpTables.ResumeLayout(False)
        Me.pnlLookUpTables.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.PanelSources.ResumeLayout(False)
        Me.PanelSources.PerformLayout()
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.TSDMUStaffTools.ResumeLayout(False)
        Me.TSDMUStaffTools.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents bgwTransfer As System.ComponentModel.BackgroundWorker
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TCDMUTools As System.Windows.Forms.TabControl
    Friend WithEvents TPWebErrorLog As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorCount As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterWebErrors As System.Windows.Forms.Button
    Friend WithEvents rdbResolvedWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnresolvedWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAllWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents txtWebErrorSolution As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorDate As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorUser As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents btnSaveWebErrorSolution As System.Windows.Forms.Button
    Friend WithEvents txtWebErrorNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents dgrWebErrorList As System.Windows.Forms.DataGrid
    Friend WithEvents TPErrorLog As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtErrorCount As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterErrors As System.Windows.Forms.Button
    Friend WithEvents rdbViewResolvedErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewUnresolvedErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewAllErrors As System.Windows.Forms.RadioButton
    Friend WithEvents txtErrorSolution As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorDate As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorUser As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents btnSaveError As System.Windows.Forms.Button
    Friend WithEvents txtErrorNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents TPAddNewFacility As System.Windows.Forms.TabPage
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnPreLoadNewFacility As System.Windows.Forms.Button
    Friend WithEvents btnDeleteAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents txtDeleteAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnClearAddNewFacility As System.Windows.Forms.Button
    Friend WithEvents GBContactInformation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbContactNumberExtension As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtContactPedigree As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents mtbContactPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents GBAirProgramCodes As System.Windows.Forms.GroupBox
    Friend WithEvents chbCDS_7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_13 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_12 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_10 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_11 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_8 As System.Windows.Forms.CheckBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents GBHeaderData As System.Windows.Forms.GroupBox
    Friend WithEvents mtbCDSSICCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCDSRegionCode As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboCDSOperationalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cboCDSClassCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityDescription As System.Windows.Forms.TextBox
    Friend WithEvents GBMailingLocation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbMailingZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtMailingState As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMailingCity As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtMailingAddress As System.Windows.Forms.TextBox
    Friend WithEvents GBFacilityInformation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbFacilityLongitude As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbFacilityLatitude As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbCDSZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents txtCDSStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCDSFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCDSCity As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCDSState As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents llbContactInformation As System.Windows.Forms.LinkLabel
    Friend WithEvents llbAirProgramCodes As System.Windows.Forms.LinkLabel
    Friend WithEvents llbHeaderData As System.Windows.Forms.LinkLabel
    Friend WithEvents llbMailingLocation As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFacilityInformation As System.Windows.Forms.LinkLabel
    Friend WithEvents btnNewFacility As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCDSAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents TPAFSFileGenerator As System.Windows.Forms.TabPage
    Friend WithEvents txtAFSBatchFile As System.Windows.Forms.TextBox
    Friend WithEvents PanelBatchOrder As System.Windows.Forms.Panel
    Friend WithEvents btnClearAFSFileGenerator As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents btnGenerateBatchFile As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents TPUpdateDEVTest As System.Windows.Forms.TabPage
    Friend WithEvents TCTables As System.Windows.Forms.TabControl
    Friend WithEvents TPAllTables As System.Windows.Forms.TabPage
    Friend WithEvents pnlMiscTables As System.Windows.Forms.Panel
    Friend WithEvents chbUpdateEIEU As System.Windows.Forms.CheckBox
    Friend WithEvents chbUpdateEIEP As System.Windows.Forms.CheckBox
    Friend WithEvents chbUpdateEIEM As System.Windows.Forms.CheckBox
    Friend WithEvents chbUpdateEIER As System.Windows.Forms.CheckBox
    Friend WithEvents chbUpDateEISI As System.Windows.Forms.CheckBox
    Friend WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents pnlAFSTables As System.Windows.Forms.Panel
    Friend WithEvents chbAllAFSTables As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSSSPPRecords As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSSSCPRecords As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSAirPollutantData As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSBatchFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSSSCPFCERecords As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSFacilityData As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSSSCPEnforcementRecords As System.Windows.Forms.CheckBox
    Friend WithEvents chbAFSISMPRecords As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSSPPTables As System.Windows.Forms.Panel
    Friend WithEvents chbAllSSPPTables As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPPublicLetters As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPCDS As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationTracking As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationContact As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationData As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationQuality As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSPPApplicationLinking As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSSCPTables As System.Windows.Forms.Panel
    Friend WithEvents chbAllSSCPTables As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPTestReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPReportsHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPNotifications As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPItemMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPInspectionTracking As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPInspectionsRequired As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPInspections As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPInspectionActivity As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPFCEMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPFCE As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPFacilityAssignment As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcementStipulated As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcementNOVComments As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcementLetter As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcementItems As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcementCOComments As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPACCS As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPACCSHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcementAOComments As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPDistrictAssignment As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPEnforcement As System.Windows.Forms.CheckBox
    Friend WithEvents chbSSCPDistrictResponsible As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSBEAPTables As System.Windows.Forms.Panel
    Friend WithEvents chbAllSBEAPTables As System.Windows.Forms.CheckBox
    Friend WithEvents chbHBSBEAPClients As System.Windows.Forms.CheckBox
    Friend WithEvents chbHBSBEAPClientData As System.Windows.Forms.CheckBox
    Friend WithEvents chbSBEAPCaseLog As System.Windows.Forms.CheckBox
    Friend WithEvents chbSBEAPClientContacts As System.Windows.Forms.CheckBox
    Friend WithEvents chbSBEAPErrorLog As System.Windows.Forms.CheckBox
    Friend WithEvents chbSBEAPClientData As System.Windows.Forms.CheckBox
    Friend WithEvents chbSBEAPClients As System.Windows.Forms.CheckBox
    Friend WithEvents chbSBEAPClientLink As System.Windows.Forms.CheckBox
    Friend WithEvents pnlISMPTables As System.Windows.Forms.Panel
    Friend WithEvents chbAllISMPTables As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPWitnessingEng As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestReportMemo As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestReportAids As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestNotificationLog As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestNotification As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestLogNumber As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestLogLink As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPTestFirmComments As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportType As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportTwoStack As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportRATA As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportPondAndGas As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportOpacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportOneStack As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportMemo As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPDocumentTypes As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPFacilityAssignment As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReportFlare As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReferenceNumber As System.Windows.Forms.CheckBox
    Friend WithEvents pnlHeaderTables As System.Windows.Forms.Panel
    Friend WithEvents chbEPDUsers As System.Windows.Forms.CheckBox
    Friend WithEvents chbAllHeaderTables As System.Windows.Forms.CheckBox
    Friend WithEvents chbHBAPBHeaderData As System.Windows.Forms.CheckBox
    Friend WithEvents chbHBAPBFacilityInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbHBAPBAirProgramPollutants As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPPMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBSupplamentalData As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBSubPartData As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBPermits As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBMasterAPP As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBAirProgramPollutants As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBContactInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBMasterAIRS As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBFacilityInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPBHeaderData As System.Windows.Forms.CheckBox
    Friend WithEvents pnlLookUpTables As System.Windows.Forms.Panel
    Friend WithEvents chbFSNSPSReason As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpISMPMethods As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpUnits As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpTestingFirms As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSubPartSIP As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSubPart63 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSubPart61 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSubPart60 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpStates As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSSCPNotifications As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSICCodes As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpSBEAPCaseWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpPollutants As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpPermitTypes As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpPermittingUnits As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpNonAttainment As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpMonitoringUnits As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpComplianceStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpComplianceUnits As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpComplianceActivities As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpCountyInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpApplicationType As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpAPBManagement As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpDistrictInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpDistrictOffice As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpDistricts As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpEPDBranches As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpEPDPrograms As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpEPDUnits As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpHPVViolations As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpIAIPAccounts As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpIAIPForms As System.Windows.Forms.CheckBox
    Friend WithEvents chbLookUpISMPComplianceStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chbAllLookUpTables As System.Windows.Forms.CheckBox
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents chbAllTables As System.Windows.Forms.CheckBox
    Friend WithEvents PanelSources As System.Windows.Forms.Panel
    Friend WithEvents lblTransfer As System.Windows.Forms.Label
    Friend WithEvents btnClearSelection As System.Windows.Forms.Button
    Friend WithEvents btnTransferData As System.Windows.Forms.Button
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents rdbTESTTransfer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDEVTransfer As System.Windows.Forms.RadioButton
    Friend WithEvents TSDMUStaffTools As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnForceBasicRefresh As System.Windows.Forms.Button
    Friend WithEvents chbCDS_14 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbNoLimit As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLast60days As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLast30Days As System.Windows.Forms.RadioButton
    Friend WithEvents btnExporttoExcel As System.Windows.Forms.Button
    Friend WithEvents dgvErrorList As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdateAllSubParts As System.Windows.Forms.Button
End Class
