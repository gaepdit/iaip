<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DEVMailoutAndStats
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DEVMailoutAndStats))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TCMailoutAndStats = New System.Windows.Forms.TabControl
        Me.TPDepositAndPaymentStats = New System.Windows.Forms.TabPage
        Me.dgvDepositsAndPayments = New System.Windows.Forms.DataGridView
        Me.pnlDetails = New System.Windows.Forms.Panel
        Me.pnlCorrectPaymentType = New System.Windows.Forms.Panel
        Me.Label95 = New System.Windows.Forms.Label
        Me.btnUpdatePaymentType = New System.Windows.Forms.Button
        Me.cboNewPaymentType = New System.Windows.Forms.ComboBox
        Me.Label116 = New System.Windows.Forms.Label
        Me.txtAllFees = New System.Windows.Forms.TextBox
        Me.Label115 = New System.Windows.Forms.Label
        Me.txtAdminFee = New System.Windows.Forms.TextBox
        Me.Label48 = New System.Windows.Forms.Label
        Me.txtSMfee = New System.Windows.Forms.TextBox
        Me.txtNSPSfee = New System.Windows.Forms.TextBox
        Me.btnCorrectPaymentType = New System.Windows.Forms.Button
        Me.btnHideResults = New System.Windows.Forms.Button
        Me.dgvStats = New System.Windows.Forms.DataGridView
        Me.Label93 = New System.Windows.Forms.Label
        Me.Label92 = New System.Windows.Forms.Label
        Me.txtVarianceComments = New System.Windows.Forms.TextBox
        Me.Label91 = New System.Windows.Forms.Label
        Me.txtVarianceCheck = New System.Windows.Forms.TextBox
        Me.Label90 = New System.Windows.Forms.Label
        Me.txtShutDown = New System.Windows.Forms.TextBox
        Me.Label89 = New System.Windows.Forms.Label
        Me.txtNSPS = New System.Windows.Forms.TextBox
        Me.Label88 = New System.Windows.Forms.Label
        Me.txtClass = New System.Windows.Forms.TextBox
        Me.Label87 = New System.Windows.Forms.Label
        Me.txtCalculatedFee = New System.Windows.Forms.TextBox
        Me.Label86 = New System.Windows.Forms.Label
        Me.txtSyntheticMinor = New System.Windows.Forms.TextBox
        Me.Label85 = New System.Windows.Forms.Label
        Me.txtPart70 = New System.Windows.Forms.TextBox
        Me.Label84 = New System.Windows.Forms.Label
        Me.txtNSPSExemptReason = New System.Windows.Forms.TextBox
        Me.Label83 = New System.Windows.Forms.Label
        Me.txtFeeRate = New System.Windows.Forms.TextBox
        Me.Label82 = New System.Windows.Forms.Label
        Me.txtOperate = New System.Windows.Forms.TextBox
        Me.Label81 = New System.Windows.Forms.Label
        Me.txtNSPSReason = New System.Windows.Forms.TextBox
        Me.Label80 = New System.Windows.Forms.Label
        Me.txtNSPSExempt = New System.Windows.Forms.TextBox
        Me.Label79 = New System.Windows.Forms.Label
        Me.txtTotalFee = New System.Windows.Forms.TextBox
        Me.Label78 = New System.Windows.Forms.Label
        Me.Label77 = New System.Windows.Forms.Label
        Me.txtPMTons = New System.Windows.Forms.TextBox
        Me.Label76 = New System.Windows.Forms.Label
        Me.txtSO2Tons = New System.Windows.Forms.TextBox
        Me.Label71 = New System.Windows.Forms.Label
        Me.txtNOxTons = New System.Windows.Forms.TextBox
        Me.Label70 = New System.Windows.Forms.Label
        Me.txtPart70Fee = New System.Windows.Forms.TextBox
        Me.Label69 = New System.Windows.Forms.Label
        Me.Label68 = New System.Windows.Forms.Label
        Me.txtVOCTons = New System.Windows.Forms.TextBox
        Me.Label67 = New System.Windows.Forms.Label
        Me.txtSubmittalComments = New System.Windows.Forms.TextBox
        Me.Label63 = New System.Windows.Forms.Label
        Me.txtDateSubmitted = New System.Windows.Forms.TextBox
        Me.Label61 = New System.Windows.Forms.Label
        Me.txtOnlineSubmittalStatus = New System.Windows.Forms.TextBox
        Me.txtPaymentType = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnRunDepositReport = New System.Windows.Forms.Button
        Me.Label112 = New System.Windows.Forms.Label
        Me.Label111 = New System.Windows.Forms.Label
        Me.Label110 = New System.Windows.Forms.Label
        Me.dtpEndDepositDate = New System.Windows.Forms.DateTimePicker
        Me.dtpStartDepositDate = New System.Windows.Forms.DateTimePicker
        Me.chbDepositDateSearch = New System.Windows.Forms.CheckBox
        Me.btnExportToExcel = New System.Windows.Forms.Button
        Me.chbNonZeroBalance = New System.Windows.Forms.CheckBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.txtSelectedYear = New System.Windows.Forms.TextBox
        Me.Label65 = New System.Windows.Forms.Label
        Me.txtSelectedFacilityName = New System.Windows.Forms.TextBox
        Me.Label64 = New System.Windows.Forms.Label
        Me.txtSelectedAIRSNumber = New System.Windows.Forms.TextBox
        Me.btnViewSelectedFeeData = New System.Windows.Forms.Button
        Me.Label56 = New System.Windows.Forms.Label
        Me.txtCount = New System.Windows.Forms.TextBox
        Me.btnViewBalance = New System.Windows.Forms.Button
        Me.bntViewTotalPaid = New System.Windows.Forms.Button
        Me.btnViewPaymentDue = New System.Windows.Forms.Button
        Me.txtBalance = New System.Windows.Forms.TextBox
        Me.txtTotalPaid = New System.Windows.Forms.TextBox
        Me.cboStatYear = New System.Windows.Forms.ComboBox
        Me.btnViewDepositsStats = New System.Windows.Forms.Button
        Me.cboStatPayType = New System.Windows.Forms.ComboBox
        Me.txtTotalPaymentDue = New System.Windows.Forms.TextBox
        Me.TPFeeStatistics = New System.Windows.Forms.TabPage
        Me.SCFeeStatistics = New System.Windows.Forms.SplitContainer
        Me.lblsumExtraNonresponse = New System.Windows.Forms.LinkLabel
        Me.Label42 = New System.Windows.Forms.Label
        Me.txtExtraNonResponse = New System.Windows.Forms.TextBox
        Me.lblExtraNonResponse = New System.Windows.Forms.LinkLabel
        Me.Label35 = New System.Windows.Forms.Label
        Me.lblsumextrafacility = New System.Windows.Forms.LinkLabel
        Me.txtextrafacilities = New System.Windows.Forms.TextBox
        Me.lblextrafacility = New System.Windows.Forms.LinkLabel
        Me.lblstate = New System.Windows.Forms.Label
        Me.lblzip = New System.Windows.Forms.Label
        Me.lblLastname = New System.Windows.Forms.Label
        Me.lblEmail = New System.Windows.Forms.Label
        Me.lblcity = New System.Windows.Forms.Label
        Me.lblcontactstreet = New System.Windows.Forms.Label
        Me.lblfirstname = New System.Windows.Forms.Label
        Me.lblFacilityName = New System.Windows.Forms.Label
        Me.lblPart70 = New System.Windows.Forms.Label
        Me.lblNSPS = New System.Windows.Forms.Label
        Me.lbloperationalstatus = New System.Windows.Forms.Label
        Me.lblclass = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblshutdowndate = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.lblAirsNo = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.lblviewsumTrueNonresponse = New System.Windows.Forms.LinkLabel
        Me.lblfeeYear = New System.Windows.Forms.Label
        Me.lblviewSumRemovedFacility = New System.Windows.Forms.LinkLabel
        Me.btnView = New System.Windows.Forms.Button
        Me.lblviewsumarryMailout = New System.Windows.Forms.LinkLabel
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblviewSumTotalResponse = New System.Windows.Forms.LinkLabel
        Me.cboYear = New System.Windows.Forms.ComboBox
        Me.lblViewSumInProcess = New System.Windows.Forms.LinkLabel
        Me.txtfeeYear = New System.Windows.Forms.TextBox
        Me.lblviewSumFinalized = New System.Windows.Forms.LinkLabel
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblViewSumExtraResponse = New System.Windows.Forms.LinkLabel
        Me.txtMailOutCount = New System.Windows.Forms.TextBox
        Me.lblviewSumNonResponse = New System.Windows.Forms.LinkLabel
        Me.txtTotalFinalizedCount = New System.Windows.Forms.TextBox
        Me.lblviewSumLateresponse = New System.Windows.Forms.LinkLabel
        Me.txtTotalInProcessCount = New System.Windows.Forms.TextBox
        Me.lblviewSumtotalInporcess = New System.Windows.Forms.LinkLabel
        Me.Label40 = New System.Windows.Forms.Label
        Me.lblviewSumExtraToalFinalized = New System.Windows.Forms.LinkLabel
        Me.txtNonResponseCount = New System.Windows.Forms.TextBox
        Me.lblViewTrueNonresponsers = New System.Windows.Forms.LinkLabel
        Me.Label44 = New System.Windows.Forms.Label
        Me.txtTrueNonResponsers = New System.Windows.Forms.TextBox
        Me.txtTotalincompliance = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.lblViewRemovedFacilities = New System.Windows.Forms.LinkLabel
        Me.txtTotaloutofcompliance = New System.Windows.Forms.TextBox
        Me.txtRemovedFacilities = New System.Windows.Forms.TextBox
        Me.lblViewTotalFinalized = New System.Windows.Forms.LinkLabel
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblViewTotalInProcess = New System.Windows.Forms.LinkLabel
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblViewINCompliance = New System.Windows.Forms.LinkLabel
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblViewOutofcompliance = New System.Windows.Forms.LinkLabel
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblViewNonResponse = New System.Windows.Forms.LinkLabel
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblViewMailOut = New System.Windows.Forms.LinkLabel
        Me.txtextraResponse = New System.Windows.Forms.TextBox
        Me.Label62 = New System.Windows.Forms.Label
        Me.lblextraResponse = New System.Windows.Forms.LinkLabel
        Me.txtResponseCount = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.lblViewTotalResponse = New System.Windows.Forms.LinkLabel
        Me.Label45 = New System.Windows.Forms.Label
        Me.txtTotalResponse = New System.Windows.Forms.TextBox
        Me.txtMailoutFinalized = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.lblViewMailoutFinalized = New System.Windows.Forms.LinkLabel
        Me.lblViewExtraInProcess = New System.Windows.Forms.LinkLabel
        Me.Label46 = New System.Windows.Forms.Label
        Me.txtExtraInProcess = New System.Windows.Forms.TextBox
        Me.txtMailOutInProcess = New System.Windows.Forms.TextBox
        Me.lblViewExtraFinalized = New System.Windows.Forms.LinkLabel
        Me.lblViewMailoutInprocess = New System.Windows.Forms.LinkLabel
        Me.txtExtraFinalized = New System.Windows.Forms.TextBox
        Me.dgvFeeDataCount2 = New System.Windows.Forms.DataGridView
        Me.gpViewdata2 = New System.Windows.Forms.GroupBox
        Me.BtnExportExcel2 = New System.Windows.Forms.Button
        Me.txtRecordNumber2 = New System.Windows.Forms.TextBox
        Me.TPMiscWebTools = New System.Windows.Forms.TabPage
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPWebUsers = New System.Windows.Forms.TabPage
        Me.pnlUser = New System.Windows.Forms.Panel
        Me.cboUsers = New System.Windows.Forms.ComboBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.btnAddUser = New System.Windows.Forms.Button
        Me.dgrUsers = New System.Windows.Forms.DataGrid
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label177 = New System.Windows.Forms.Label
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.llbViewUserData = New System.Windows.Forms.LinkLabel
        Me.TPWebUsers1 = New System.Windows.Forms.TabPage
        Me.pnlUserFacility = New System.Windows.Forms.Panel
        Me.pnlUserInfo = New System.Windows.Forms.Panel
        Me.btnChangeEmailAddress = New System.Windows.Forms.Button
        Me.txtEditEmail = New System.Windows.Forms.TextBox
        Me.lblConfirmDate = New System.Windows.Forms.Label
        Me.lblLastLogIn = New System.Windows.Forms.Label
        Me.txtEditUserPassword = New System.Windows.Forms.TextBox
        Me.btnUpdatePassword = New System.Windows.Forms.Button
        Me.txtWebUserID = New System.Windows.Forms.TextBox
        Me.btnSaveEditedData = New System.Windows.Forms.Button
        Me.mtbEditZipCode = New System.Windows.Forms.MaskedTextBox
        Me.mtbEditState = New System.Windows.Forms.MaskedTextBox
        Me.mtbEditFaxNumber = New System.Windows.Forms.MaskedTextBox
        Me.mtbEditPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.txtEditCity = New System.Windows.Forms.TextBox
        Me.txtEditAddress = New System.Windows.Forms.TextBox
        Me.txtEditCompany = New System.Windows.Forms.TextBox
        Me.txtEditTitle = New System.Windows.Forms.TextBox
        Me.txtEditLastName = New System.Windows.Forms.TextBox
        Me.txtEditFirstName = New System.Windows.Forms.TextBox
        Me.btnEditUserData = New System.Windows.Forms.Button
        Me.lblCityStateZip = New System.Windows.Forms.Label
        Me.lblAddress = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.lblFaxNo = New System.Windows.Forms.Label
        Me.lblPhoneNo = New System.Windows.Forms.Label
        Me.lblCoName = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.lblLName = New System.Windows.Forms.Label
        Me.lblFName = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.mtbFacilityToAdd = New System.Windows.Forms.MaskedTextBox
        Me.cboFacilityToDelete = New System.Windows.Forms.ComboBox
        Me.Label75 = New System.Windows.Forms.Label
        Me.btnDeleteFacilityUser = New System.Windows.Forms.Button
        Me.btnUpdateUser = New System.Windows.Forms.Button
        Me.Label53 = New System.Windows.Forms.Label
        Me.btnAddFacilitytoUser = New System.Windows.Forms.Button
        Me.dgrFacilities = New System.Windows.Forms.DataGrid
        Me.pnlUserEmail = New System.Windows.Forms.Panel
        Me.lblViewEmailData = New System.Windows.Forms.LinkLabel
        Me.Label39 = New System.Windows.Forms.Label
        Me.txtWebUserEmail = New System.Windows.Forms.TextBox
        Me.cboUserEmail = New System.Windows.Forms.ComboBox
        Me.btnActivateEmail = New System.Windows.Forms.Button
        Me.lblViewFacility = New System.Windows.Forms.LinkLabel
        Me.Label52 = New System.Windows.Forms.Label
        Me.TPActivate = New System.Windows.Forms.TabPage
        Me.btnActivateUser = New System.Windows.Forms.Button
        Me.Label54 = New System.Windows.Forms.Label
        Me.txtEmailAddress = New System.Windows.Forms.TextBox
        Me.TPFeeFacility = New System.Windows.Forms.TabPage
        Me.mtbyear = New System.Windows.Forms.MaskedTextBox
        Me.mtbFeeAirsNumber = New System.Windows.Forms.MaskedTextBox
        Me.btnRemoveFacility = New System.Windows.Forms.Button
        Me.Label74 = New System.Windows.Forms.Label
        Me.Label73 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label72 = New System.Windows.Forms.Label
        Me.btnAddFacility = New System.Windows.Forms.Button
        Me.TPGenerateMailOut = New System.Windows.Forms.TabPage
        Me.SCGenerateMailOut = New System.Windows.Forms.SplitContainer
        Me.lblviewsumarrymailoutinfo = New System.Windows.Forms.LinkLabel
        Me.btnGenerateFeeMailOut = New System.Windows.Forms.Button
        Me.btnRefreshAirsNo = New System.Windows.Forms.Button
        Me.cboMailoutYear = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.cboPart70 = New System.Windows.Forms.ComboBox
        Me.txtFacilityZip = New System.Windows.Forms.TextBox
        Me.cboNSPS = New System.Windows.Forms.ComboBox
        Me.btnDeleteFeeMailOut = New System.Windows.Forms.Button
        Me.cboOperation = New System.Windows.Forms.ComboBox
        Me.txtFacilityState = New System.Windows.Forms.TextBox
        Me.cboClass = New System.Windows.Forms.ComboBox
        Me.lblviewselectedyearMailOutlist = New System.Windows.Forms.LinkLabel
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtFacilityCity = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtFacilityAddress = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtFacilityStreet = New System.Windows.Forms.TextBox
        Me.txtContactAddress1 = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnFeeDelete = New System.Windows.Forms.Button
        Me.txtAirsNo = New System.Windows.Forms.TextBox
        Me.btnFeeSave = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMailoutEmail = New System.Windows.Forms.TextBox
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtContactZip = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtContactState = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtContactCity = New System.Windows.Forms.TextBox
        Me.txtCompanyName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtShutdowndate = New System.Windows.Forms.TextBox
        Me.dgvFeeDataCount = New System.Windows.Forms.DataGridView
        Me.gpViewdata = New System.Windows.Forms.GroupBox
        Me.BtnExportExcel = New System.Windows.Forms.Button
        Me.txtRecordNumber = New System.Windows.Forms.TextBox
        Me.TPEnrollment = New System.Windows.Forms.TabPage
        Me.lblEnrollYear = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnDeEnroll = New System.Windows.Forms.Button
        Me.btnEnroll = New System.Windows.Forms.Button
        Me.TPFeeRates = New System.Windows.Forms.TabPage
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label114 = New System.Windows.Forms.Label
        Me.dtpFeeDueDate = New System.Windows.Forms.DateTimePicker
        Me.Label113 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.btnsaveRate = New System.Windows.Forms.Button
        Me.dtpduedate = New System.Windows.Forms.DateTimePicker
        Me.cboFeeRateYear = New System.Windows.Forms.ComboBox
        Me.lblViewFeeRate = New System.Windows.Forms.LinkLabel
        Me.txtAnnualSMFee = New System.Windows.Forms.TextBox
        Me.Label60 = New System.Windows.Forms.Label
        Me.txtAnnualNSPSFee = New System.Windows.Forms.TextBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.txtTitleVfee = New System.Windows.Forms.TextBox
        Me.Label57 = New System.Windows.Forms.Label
        Me.txtperTonRate = New System.Windows.Forms.TextBox
        Me.Label55 = New System.Windows.Forms.Label
        Me.txtAdminFeePercent = New System.Windows.Forms.TextBox
        Me.Label248 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.pnlNSPSExemptions = New System.Windows.Forms.Panel
        Me.btnLoadNSPSTool = New System.Windows.Forms.Button
        Me.Label109 = New System.Windows.Forms.Label
        Me.btnSelectForm = New System.Windows.Forms.Button
        Me.btnUnselectForm = New System.Windows.Forms.Button
        Me.btnSelectAllForms = New System.Windows.Forms.Button
        Me.btnUnselectAllForms = New System.Windows.Forms.Button
        Me.btnUpdateNSPSbyYear = New System.Windows.Forms.Button
        Me.btnAddExemptionToYear = New System.Windows.Forms.Button
        Me.cboNSPSExemptions = New System.Windows.Forms.ComboBox
        Me.btnViewNSPSExemptionsByYear = New System.Windows.Forms.Button
        Me.dgvNSPSExemptionsByYear = New System.Windows.Forms.DataGridView
        Me.Label108 = New System.Windows.Forms.Label
        Me.cboNSPSExemptionYear = New System.Windows.Forms.ComboBox
        Me.btnDeleteNSPSExemption = New System.Windows.Forms.Button
        Me.Label107 = New System.Windows.Forms.Label
        Me.txtDeleteNSPSExemptions = New System.Windows.Forms.TextBox
        Me.txtNSPSExemption = New System.Windows.Forms.TextBox
        Me.Label101 = New System.Windows.Forms.Label
        Me.btnAddNSPSExemption = New System.Windows.Forms.Button
        Me.Label100 = New System.Windows.Forms.Label
        Me.dgvNSPSExemptions = New System.Windows.Forms.DataGridView
        Me.TPNonRespondersReport = New System.Windows.Forms.TabPage
        Me.TCLateFeeReports = New System.Windows.Forms.TabControl
        Me.TPQuickFeeReport = New System.Windows.Forms.TabPage
        Me.dgvLateFeeReport = New System.Windows.Forms.DataGridView
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnViewData = New System.Windows.Forms.Button
        Me.btnFeePendingPermittingEvent = New System.Windows.Forms.Button
        Me.Label106 = New System.Windows.Forms.Label
        Me.txtFeePendingPermitType = New System.Windows.Forms.TextBox
        Me.txtFeePendingPermit = New System.Windows.Forms.TextBox
        Me.Label105 = New System.Windows.Forms.Label
        Me.Label104 = New System.Windows.Forms.Label
        Me.txtFeePermitNumber = New System.Windows.Forms.TextBox
        Me.lblPermitDate = New System.Windows.Forms.Label
        Me.Label102 = New System.Windows.Forms.Label
        Me.txtFeePermittingEvent = New System.Windows.Forms.TextBox
        Me.btnFeeViewPermittingEvent = New System.Windows.Forms.Button
        Me.txtFeePermittingDate = New System.Windows.Forms.TextBox
        Me.Label103 = New System.Windows.Forms.Label
        Me.txtFeePermittingEventType = New System.Windows.Forms.TextBox
        Me.lblComplianceDate = New System.Windows.Forms.Label
        Me.Label99 = New System.Windows.Forms.Label
        Me.txtFeeComplianceEvent = New System.Windows.Forms.TextBox
        Me.btnFeeViewComplianceEvent = New System.Windows.Forms.Button
        Me.txtFeeLastComplianceEvent = New System.Windows.Forms.TextBox
        Me.btnFeeFacilitySummary = New System.Windows.Forms.Button
        Me.Label98 = New System.Windows.Forms.Label
        Me.txtFeeComplianceEventType = New System.Windows.Forms.TextBox
        Me.Label97 = New System.Windows.Forms.Label
        Me.txtFeeFacilityName = New System.Windows.Forms.TextBox
        Me.Label96 = New System.Windows.Forms.Label
        Me.txtFeeAIRSNumber = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnRemovePaidFacilities = New System.Windows.Forms.Button
        Me.btnViewUnenrolled = New System.Windows.Forms.Button
        Me.rdbHasNotPaidFee = New System.Windows.Forms.RadioButton
        Me.btnCheckforFeesPaid = New System.Windows.Forms.Button
        Me.rdbHasPaidFee = New System.Windows.Forms.RadioButton
        Me.TPFullReport = New System.Windows.Forms.TabPage
        Me.dgvLateFeePayerReport = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnExportFeeReport = New System.Windows.Forms.Button
        Me.btnRunReport = New System.Windows.Forms.Button
        Me.txtFeeCount = New System.Windows.Forms.TextBox
        Me.lblCount = New System.Windows.Forms.Label
        Me.btnRunLateFeeReport = New System.Windows.Forms.Button
        Me.cboFeeYear = New System.Windows.Forms.ComboBox
        Me.Label94 = New System.Windows.Forms.Label
        Me.bgwAIRS = New System.ComponentModel.BackgroundWorker
        Me.bgwEmails = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCMailoutAndStats.SuspendLayout()
        Me.TPDepositAndPaymentStats.SuspendLayout()
        CType(Me.dgvDepositsAndPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails.SuspendLayout()
        Me.pnlCorrectPaymentType.SuspendLayout()
        CType(Me.dgvStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TPFeeStatistics.SuspendLayout()
        Me.SCFeeStatistics.Panel1.SuspendLayout()
        Me.SCFeeStatistics.Panel2.SuspendLayout()
        Me.SCFeeStatistics.SuspendLayout()
        CType(Me.dgvFeeDataCount2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpViewdata2.SuspendLayout()
        Me.TPMiscWebTools.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TPWebUsers.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        CType(Me.dgrUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFacility.SuspendLayout()
        Me.TPWebUsers1.SuspendLayout()
        Me.pnlUserFacility.SuspendLayout()
        Me.pnlUserInfo.SuspendLayout()
        CType(Me.dgrFacilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUserEmail.SuspendLayout()
        Me.TPActivate.SuspendLayout()
        Me.TPFeeFacility.SuspendLayout()
        Me.TPGenerateMailOut.SuspendLayout()
        Me.SCGenerateMailOut.Panel1.SuspendLayout()
        Me.SCGenerateMailOut.Panel2.SuspendLayout()
        Me.SCGenerateMailOut.SuspendLayout()
        CType(Me.dgvFeeDataCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpViewdata.SuspendLayout()
        Me.TPEnrollment.SuspendLayout()
        Me.TPFeeRates.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pnlNSPSExemptions.SuspendLayout()
        CType(Me.dgvNSPSExemptionsByYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNSPSExemptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPNonRespondersReport.SuspendLayout()
        Me.TCLateFeeReports.SuspendLayout()
        Me.TPQuickFeeReport.SuspendLayout()
        CType(Me.dgvLateFeeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TPFullReport.SuspendLayout()
        CType(Me.dgvLateFeePayerReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 724)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(946, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pnl1
        '
        Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(923, 17)
        Me.pnl1.Spring = True
        Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl2
        '
        Me.pnl2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(4, 17)
        '
        'pnl3
        '
        Me.pnl3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(4, 17)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(946, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'TCMailoutAndStats
        '
        Me.TCMailoutAndStats.Controls.Add(Me.TPDepositAndPaymentStats)
        Me.TCMailoutAndStats.Controls.Add(Me.TPFeeStatistics)
        Me.TCMailoutAndStats.Controls.Add(Me.TPMiscWebTools)
        Me.TCMailoutAndStats.Controls.Add(Me.TPGenerateMailOut)
        Me.TCMailoutAndStats.Controls.Add(Me.TPEnrollment)
        Me.TCMailoutAndStats.Controls.Add(Me.TPFeeRates)
        Me.TCMailoutAndStats.Controls.Add(Me.TPNonRespondersReport)
        Me.TCMailoutAndStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMailoutAndStats.Location = New System.Drawing.Point(0, 24)
        Me.TCMailoutAndStats.Name = "TCMailoutAndStats"
        Me.TCMailoutAndStats.SelectedIndex = 0
        Me.TCMailoutAndStats.Size = New System.Drawing.Size(946, 700)
        Me.TCMailoutAndStats.TabIndex = 7
        '
        'TPDepositAndPaymentStats
        '
        Me.TPDepositAndPaymentStats.Controls.Add(Me.dgvDepositsAndPayments)
        Me.TPDepositAndPaymentStats.Controls.Add(Me.pnlDetails)
        Me.TPDepositAndPaymentStats.Controls.Add(Me.Panel1)
        Me.TPDepositAndPaymentStats.Location = New System.Drawing.Point(4, 22)
        Me.TPDepositAndPaymentStats.Name = "TPDepositAndPaymentStats"
        Me.TPDepositAndPaymentStats.Size = New System.Drawing.Size(938, 674)
        Me.TPDepositAndPaymentStats.TabIndex = 4
        Me.TPDepositAndPaymentStats.Text = "Deposits and Payments"
        Me.TPDepositAndPaymentStats.UseVisualStyleBackColor = True
        '
        'dgvDepositsAndPayments
        '
        Me.dgvDepositsAndPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDepositsAndPayments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDepositsAndPayments.Location = New System.Drawing.Point(0, 565)
        Me.dgvDepositsAndPayments.Name = "dgvDepositsAndPayments"
        Me.dgvDepositsAndPayments.ReadOnly = True
        Me.dgvDepositsAndPayments.Size = New System.Drawing.Size(938, 109)
        Me.dgvDepositsAndPayments.TabIndex = 7
        '
        'pnlDetails
        '
        Me.pnlDetails.Controls.Add(Me.pnlCorrectPaymentType)
        Me.pnlDetails.Controls.Add(Me.Label116)
        Me.pnlDetails.Controls.Add(Me.txtAllFees)
        Me.pnlDetails.Controls.Add(Me.Label115)
        Me.pnlDetails.Controls.Add(Me.txtAdminFee)
        Me.pnlDetails.Controls.Add(Me.Label48)
        Me.pnlDetails.Controls.Add(Me.txtSMfee)
        Me.pnlDetails.Controls.Add(Me.txtNSPSfee)
        Me.pnlDetails.Controls.Add(Me.btnCorrectPaymentType)
        Me.pnlDetails.Controls.Add(Me.btnHideResults)
        Me.pnlDetails.Controls.Add(Me.dgvStats)
        Me.pnlDetails.Controls.Add(Me.Label93)
        Me.pnlDetails.Controls.Add(Me.Label92)
        Me.pnlDetails.Controls.Add(Me.txtVarianceComments)
        Me.pnlDetails.Controls.Add(Me.Label91)
        Me.pnlDetails.Controls.Add(Me.txtVarianceCheck)
        Me.pnlDetails.Controls.Add(Me.Label90)
        Me.pnlDetails.Controls.Add(Me.txtShutDown)
        Me.pnlDetails.Controls.Add(Me.Label89)
        Me.pnlDetails.Controls.Add(Me.txtNSPS)
        Me.pnlDetails.Controls.Add(Me.Label88)
        Me.pnlDetails.Controls.Add(Me.txtClass)
        Me.pnlDetails.Controls.Add(Me.Label87)
        Me.pnlDetails.Controls.Add(Me.txtCalculatedFee)
        Me.pnlDetails.Controls.Add(Me.Label86)
        Me.pnlDetails.Controls.Add(Me.txtSyntheticMinor)
        Me.pnlDetails.Controls.Add(Me.Label85)
        Me.pnlDetails.Controls.Add(Me.txtPart70)
        Me.pnlDetails.Controls.Add(Me.Label84)
        Me.pnlDetails.Controls.Add(Me.txtNSPSExemptReason)
        Me.pnlDetails.Controls.Add(Me.Label83)
        Me.pnlDetails.Controls.Add(Me.txtFeeRate)
        Me.pnlDetails.Controls.Add(Me.Label82)
        Me.pnlDetails.Controls.Add(Me.txtOperate)
        Me.pnlDetails.Controls.Add(Me.Label81)
        Me.pnlDetails.Controls.Add(Me.txtNSPSReason)
        Me.pnlDetails.Controls.Add(Me.Label80)
        Me.pnlDetails.Controls.Add(Me.txtNSPSExempt)
        Me.pnlDetails.Controls.Add(Me.Label79)
        Me.pnlDetails.Controls.Add(Me.txtTotalFee)
        Me.pnlDetails.Controls.Add(Me.Label78)
        Me.pnlDetails.Controls.Add(Me.Label77)
        Me.pnlDetails.Controls.Add(Me.txtPMTons)
        Me.pnlDetails.Controls.Add(Me.Label76)
        Me.pnlDetails.Controls.Add(Me.txtSO2Tons)
        Me.pnlDetails.Controls.Add(Me.Label71)
        Me.pnlDetails.Controls.Add(Me.txtNOxTons)
        Me.pnlDetails.Controls.Add(Me.Label70)
        Me.pnlDetails.Controls.Add(Me.txtPart70Fee)
        Me.pnlDetails.Controls.Add(Me.Label69)
        Me.pnlDetails.Controls.Add(Me.Label68)
        Me.pnlDetails.Controls.Add(Me.txtVOCTons)
        Me.pnlDetails.Controls.Add(Me.Label67)
        Me.pnlDetails.Controls.Add(Me.txtSubmittalComments)
        Me.pnlDetails.Controls.Add(Me.Label63)
        Me.pnlDetails.Controls.Add(Me.txtDateSubmitted)
        Me.pnlDetails.Controls.Add(Me.Label61)
        Me.pnlDetails.Controls.Add(Me.txtOnlineSubmittalStatus)
        Me.pnlDetails.Controls.Add(Me.txtPaymentType)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDetails.Location = New System.Drawing.Point(0, 163)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(938, 402)
        Me.pnlDetails.TabIndex = 8
        '
        'pnlCorrectPaymentType
        '
        Me.pnlCorrectPaymentType.Controls.Add(Me.Label95)
        Me.pnlCorrectPaymentType.Controls.Add(Me.btnUpdatePaymentType)
        Me.pnlCorrectPaymentType.Controls.Add(Me.cboNewPaymentType)
        Me.pnlCorrectPaymentType.Location = New System.Drawing.Point(321, 4)
        Me.pnlCorrectPaymentType.Name = "pnlCorrectPaymentType"
        Me.pnlCorrectPaymentType.Size = New System.Drawing.Size(609, 242)
        Me.pnlCorrectPaymentType.TabIndex = 62
        Me.pnlCorrectPaymentType.Visible = False
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(21, 7)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(117, 13)
        Me.Label95.TabIndex = 46
        Me.Label95.Text = "Select a new Pay Type"
        '
        'btnUpdatePaymentType
        '
        Me.btnUpdatePaymentType.AutoSize = True
        Me.btnUpdatePaymentType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdatePaymentType.Location = New System.Drawing.Point(196, 24)
        Me.btnUpdatePaymentType.Name = "btnUpdatePaymentType"
        Me.btnUpdatePaymentType.Size = New System.Drawing.Size(123, 23)
        Me.btnUpdatePaymentType.TabIndex = 44
        Me.btnUpdatePaymentType.Text = "Update Payment Type"
        Me.btnUpdatePaymentType.UseVisualStyleBackColor = True
        '
        'cboNewPaymentType
        '
        Me.cboNewPaymentType.FormattingEnabled = True
        Me.cboNewPaymentType.Location = New System.Drawing.Point(24, 25)
        Me.cboNewPaymentType.Name = "cboNewPaymentType"
        Me.cboNewPaymentType.Size = New System.Drawing.Size(144, 21)
        Me.cboNewPaymentType.TabIndex = 45
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Location = New System.Drawing.Point(485, 130)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(52, 13)
        Me.Label116.TabIndex = 399
        Me.Label116.Text = "Total Fee"
        '
        'txtAllFees
        '
        Me.txtAllFees.Location = New System.Drawing.Point(558, 126)
        Me.txtAllFees.Name = "txtAllFees"
        Me.txtAllFees.ReadOnly = True
        Me.txtAllFees.Size = New System.Drawing.Size(89, 20)
        Me.txtAllFees.TabIndex = 398
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Location = New System.Drawing.Point(485, 104)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(57, 13)
        Me.Label115.TabIndex = 397
        Me.Label115.Text = "Admin Fee"
        '
        'txtAdminFee
        '
        Me.txtAdminFee.Location = New System.Drawing.Point(558, 100)
        Me.txtAdminFee.Name = "txtAdminFee"
        Me.txtAdminFee.ReadOnly = True
        Me.txtAdminFee.Size = New System.Drawing.Size(89, 20)
        Me.txtAdminFee.TabIndex = 396
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(8, 8)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(122, 13)
        Me.Label48.TabIndex = 395
        Me.Label48.Text = "Payment Type Reported"
        '
        'txtSMfee
        '
        Me.txtSMfee.Location = New System.Drawing.Point(558, 27)
        Me.txtSMfee.Name = "txtSMfee"
        Me.txtSMfee.ReadOnly = True
        Me.txtSMfee.Size = New System.Drawing.Size(89, 20)
        Me.txtSMfee.TabIndex = 394
        '
        'txtNSPSfee
        '
        Me.txtNSPSfee.Location = New System.Drawing.Point(558, 50)
        Me.txtNSPSfee.Name = "txtNSPSfee"
        Me.txtNSPSfee.ReadOnly = True
        Me.txtNSPSfee.Size = New System.Drawing.Size(89, 20)
        Me.txtNSPSfee.TabIndex = 393
        '
        'btnCorrectPaymentType
        '
        Me.btnCorrectPaymentType.Image = CType(resources.GetObject("btnCorrectPaymentType.Image"), System.Drawing.Image)
        Me.btnCorrectPaymentType.Location = New System.Drawing.Point(283, 3)
        Me.btnCorrectPaymentType.Name = "btnCorrectPaymentType"
        Me.btnCorrectPaymentType.Size = New System.Drawing.Size(32, 21)
        Me.btnCorrectPaymentType.TabIndex = 61
        Me.btnCorrectPaymentType.UseVisualStyleBackColor = True
        '
        'btnHideResults
        '
        Me.btnHideResults.AutoSize = True
        Me.btnHideResults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnHideResults.Location = New System.Drawing.Point(11, 360)
        Me.btnHideResults.Name = "btnHideResults"
        Me.btnHideResults.Size = New System.Drawing.Size(77, 23)
        Me.btnHideResults.TabIndex = 60
        Me.btnHideResults.Text = "Hide Results"
        Me.btnHideResults.UseVisualStyleBackColor = True
        '
        'dgvStats
        '
        Me.dgvStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStats.Location = New System.Drawing.Point(106, 252)
        Me.dgvStats.Name = "dgvStats"
        Me.dgvStats.ReadOnly = True
        Me.dgvStats.Size = New System.Drawing.Size(824, 131)
        Me.dgvStats.TabIndex = 59
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(3, 252)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(98, 13)
        Me.Label93.TabIndex = 58
        Me.Label93.Text = "Deposit Information"
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.Location = New System.Drawing.Point(537, 218)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(101, 13)
        Me.Label92.TabIndex = 56
        Me.Label92.Text = "Variance Comments"
        '
        'txtVarianceComments
        '
        Me.txtVarianceComments.Location = New System.Drawing.Point(653, 212)
        Me.txtVarianceComments.Name = "txtVarianceComments"
        Me.txtVarianceComments.ReadOnly = True
        Me.txtVarianceComments.Size = New System.Drawing.Size(121, 20)
        Me.txtVarianceComments.TabIndex = 55
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Location = New System.Drawing.Point(537, 192)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(83, 13)
        Me.Label91.TabIndex = 54
        Me.Label91.Text = "Variance Check"
        '
        'txtVarianceCheck
        '
        Me.txtVarianceCheck.Location = New System.Drawing.Point(653, 186)
        Me.txtVarianceCheck.Name = "txtVarianceCheck"
        Me.txtVarianceCheck.ReadOnly = True
        Me.txtVarianceCheck.Size = New System.Drawing.Size(121, 20)
        Me.txtVarianceCheck.TabIndex = 53
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(537, 166)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(86, 13)
        Me.Label90.TabIndex = 52
        Me.Label90.Text = "Shut Down Date"
        '
        'txtShutDown
        '
        Me.txtShutDown.Location = New System.Drawing.Point(653, 160)
        Me.txtShutDown.Name = "txtShutDown"
        Me.txtShutDown.ReadOnly = True
        Me.txtShutDown.Size = New System.Drawing.Size(121, 20)
        Me.txtShutDown.TabIndex = 51
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.Location = New System.Drawing.Point(266, 219)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(69, 13)
        Me.Label89.TabIndex = 50
        Me.Label89.Text = "NSPS Status"
        '
        'txtNSPS
        '
        Me.txtNSPS.Location = New System.Drawing.Point(382, 213)
        Me.txtNSPS.Name = "txtNSPS"
        Me.txtNSPS.ReadOnly = True
        Me.txtNSPS.Size = New System.Drawing.Size(121, 20)
        Me.txtNSPS.TabIndex = 49
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(266, 193)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(68, 13)
        Me.Label88.TabIndex = 48
        Me.Label88.Text = "Classification"
        '
        'txtClass
        '
        Me.txtClass.Location = New System.Drawing.Point(382, 187)
        Me.txtClass.Name = "txtClass"
        Me.txtClass.ReadOnly = True
        Me.txtClass.Size = New System.Drawing.Size(121, 20)
        Me.txtClass.TabIndex = 47
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Location = New System.Drawing.Point(268, 167)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(78, 13)
        Me.Label87.TabIndex = 46
        Me.Label87.Text = "Calculated Fee"
        '
        'txtCalculatedFee
        '
        Me.txtCalculatedFee.Location = New System.Drawing.Point(384, 161)
        Me.txtCalculatedFee.Name = "txtCalculatedFee"
        Me.txtCalculatedFee.ReadOnly = True
        Me.txtCalculatedFee.Size = New System.Drawing.Size(121, 20)
        Me.txtCalculatedFee.TabIndex = 45
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Location = New System.Drawing.Point(15, 194)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(80, 13)
        Me.Label86.TabIndex = 44
        Me.Label86.Text = "Synthetic Minor"
        '
        'txtSyntheticMinor
        '
        Me.txtSyntheticMinor.Location = New System.Drawing.Point(131, 188)
        Me.txtSyntheticMinor.Name = "txtSyntheticMinor"
        Me.txtSyntheticMinor.ReadOnly = True
        Me.txtSyntheticMinor.Size = New System.Drawing.Size(121, 20)
        Me.txtSyntheticMinor.TabIndex = 43
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Location = New System.Drawing.Point(15, 168)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(41, 13)
        Me.Label85.TabIndex = 42
        Me.Label85.Text = "Part 70"
        '
        'txtPart70
        '
        Me.txtPart70.Location = New System.Drawing.Point(131, 162)
        Me.txtPart70.Name = "txtPart70"
        Me.txtPart70.ReadOnly = True
        Me.txtPart70.Size = New System.Drawing.Size(121, 20)
        Me.txtPart70.TabIndex = 41
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Location = New System.Drawing.Point(671, 61)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(114, 13)
        Me.Label84.TabIndex = 40
        Me.Label84.Text = "NSPS Exempt Reason"
        '
        'txtNSPSExemptReason
        '
        Me.txtNSPSExemptReason.AcceptsReturn = True
        Me.txtNSPSExemptReason.Location = New System.Drawing.Point(787, 55)
        Me.txtNSPSExemptReason.Multiline = True
        Me.txtNSPSExemptReason.Name = "txtNSPSExemptReason"
        Me.txtNSPSExemptReason.ReadOnly = True
        Me.txtNSPSExemptReason.Size = New System.Drawing.Size(143, 66)
        Me.txtNSPSExemptReason.TabIndex = 39
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Location = New System.Drawing.Point(328, 103)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(51, 13)
        Me.Label83.TabIndex = 38
        Me.Label83.Text = "Fee Rate"
        '
        'txtFeeRate
        '
        Me.txtFeeRate.Location = New System.Drawing.Point(398, 100)
        Me.txtFeeRate.Name = "txtFeeRate"
        Me.txtFeeRate.ReadOnly = True
        Me.txtFeeRate.Size = New System.Drawing.Size(68, 20)
        Me.txtFeeRate.TabIndex = 37
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Location = New System.Drawing.Point(15, 216)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(53, 13)
        Me.Label82.TabIndex = 36
        Me.Label82.Text = "Operating"
        '
        'txtOperate
        '
        Me.txtOperate.Location = New System.Drawing.Point(131, 211)
        Me.txtOperate.Name = "txtOperate"
        Me.txtOperate.ReadOnly = True
        Me.txtOperate.Size = New System.Drawing.Size(52, 20)
        Me.txtOperate.TabIndex = 35
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Location = New System.Drawing.Point(671, 35)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(114, 13)
        Me.Label81.TabIndex = 34
        Me.Label81.Text = "NSPS Exempt Reason"
        '
        'txtNSPSReason
        '
        Me.txtNSPSReason.Location = New System.Drawing.Point(787, 29)
        Me.txtNSPSReason.Name = "txtNSPSReason"
        Me.txtNSPSReason.ReadOnly = True
        Me.txtNSPSReason.Size = New System.Drawing.Size(143, 20)
        Me.txtNSPSReason.TabIndex = 33
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(667, 8)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(74, 13)
        Me.Label80.TabIndex = 32
        Me.Label80.Text = "NSPS Exempt"
        '
        'txtNSPSExempt
        '
        Me.txtNSPSExempt.Location = New System.Drawing.Point(747, 3)
        Me.txtNSPSExempt.Name = "txtNSPSExempt"
        Me.txtNSPSExempt.ReadOnly = True
        Me.txtNSPSExempt.Size = New System.Drawing.Size(121, 20)
        Me.txtNSPSExempt.TabIndex = 31
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(485, 79)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(53, 13)
        Me.Label79.TabIndex = 30
        Me.Label79.Text = "Sub Total"
        '
        'txtTotalFee
        '
        Me.txtTotalFee.Location = New System.Drawing.Point(558, 75)
        Me.txtTotalFee.Name = "txtTotalFee"
        Me.txtTotalFee.ReadOnly = True
        Me.txtTotalFee.Size = New System.Drawing.Size(89, 20)
        Me.txtTotalFee.TabIndex = 29
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(485, 55)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(57, 13)
        Me.Label78.TabIndex = 28
        Me.Label78.Text = "NSPS Fee"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(327, 31)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(50, 13)
        Me.Label77.TabIndex = 26
        Me.Label77.Text = "PM Tons"
        '
        'txtPMTons
        '
        Me.txtPMTons.Location = New System.Drawing.Point(398, 27)
        Me.txtPMTons.Name = "txtPMTons"
        Me.txtPMTons.ReadOnly = True
        Me.txtPMTons.Size = New System.Drawing.Size(68, 20)
        Me.txtPMTons.TabIndex = 25
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(328, 55)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(55, 13)
        Me.Label76.TabIndex = 24
        Me.Label76.Text = "SO2 Tons"
        '
        'txtSO2Tons
        '
        Me.txtSO2Tons.Location = New System.Drawing.Point(398, 51)
        Me.txtSO2Tons.Name = "txtSO2Tons"
        Me.txtSO2Tons.ReadOnly = True
        Me.txtSO2Tons.Size = New System.Drawing.Size(68, 20)
        Me.txtSO2Tons.TabIndex = 23
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(328, 79)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(55, 13)
        Me.Label71.TabIndex = 22
        Me.Label71.Text = "NOx Tons"
        '
        'txtNOxTons
        '
        Me.txtNOxTons.Location = New System.Drawing.Point(398, 75)
        Me.txtNOxTons.Name = "txtNOxTons"
        Me.txtNOxTons.ReadOnly = True
        Me.txtNOxTons.Size = New System.Drawing.Size(68, 20)
        Me.txtNOxTons.TabIndex = 21
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(488, 7)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(67, 13)
        Me.Label70.TabIndex = 20
        Me.Label70.Text = "Part 70 Fees"
        '
        'txtPart70Fee
        '
        Me.txtPart70Fee.Location = New System.Drawing.Point(558, 3)
        Me.txtPart70Fee.Name = "txtPart70Fee"
        Me.txtPart70Fee.ReadOnly = True
        Me.txtPart70Fee.Size = New System.Drawing.Size(89, 20)
        Me.txtPart70Fee.TabIndex = 19
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(488, 31)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(44, 13)
        Me.Label69.TabIndex = 18
        Me.Label69.Text = "SM Fee"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(327, 7)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(56, 13)
        Me.Label68.TabIndex = 16
        Me.Label68.Text = "VOC Tons"
        '
        'txtVOCTons
        '
        Me.txtVOCTons.Location = New System.Drawing.Point(398, 3)
        Me.txtVOCTons.Name = "txtVOCTons"
        Me.txtVOCTons.ReadOnly = True
        Me.txtVOCTons.Size = New System.Drawing.Size(68, 20)
        Me.txtVOCTons.TabIndex = 15
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(8, 79)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(56, 13)
        Me.Label67.TabIndex = 14
        Me.Label67.Text = "Comments"
        '
        'txtSubmittalComments
        '
        Me.txtSubmittalComments.AcceptsReturn = True
        Me.txtSubmittalComments.AcceptsTab = True
        Me.txtSubmittalComments.Location = New System.Drawing.Point(131, 77)
        Me.txtSubmittalComments.Multiline = True
        Me.txtSubmittalComments.Name = "txtSubmittalComments"
        Me.txtSubmittalComments.ReadOnly = True
        Me.txtSubmittalComments.Size = New System.Drawing.Size(184, 79)
        Me.txtSubmittalComments.TabIndex = 13
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(8, 55)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(80, 13)
        Me.Label63.TabIndex = 12
        Me.Label63.Text = "Date Submitted"
        '
        'txtDateSubmitted
        '
        Me.txtDateSubmitted.Location = New System.Drawing.Point(131, 51)
        Me.txtDateSubmitted.Name = "txtDateSubmitted"
        Me.txtDateSubmitted.ReadOnly = True
        Me.txtDateSubmitted.Size = New System.Drawing.Size(76, 20)
        Me.txtDateSubmitted.TabIndex = 11
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(8, 31)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(83, 13)
        Me.Label61.TabIndex = 10
        Me.Label61.Text = "Online Submittal"
        '
        'txtOnlineSubmittalStatus
        '
        Me.txtOnlineSubmittalStatus.Location = New System.Drawing.Point(131, 27)
        Me.txtOnlineSubmittalStatus.Name = "txtOnlineSubmittalStatus"
        Me.txtOnlineSubmittalStatus.ReadOnly = True
        Me.txtOnlineSubmittalStatus.Size = New System.Drawing.Size(76, 20)
        Me.txtOnlineSubmittalStatus.TabIndex = 9
        '
        'txtPaymentType
        '
        Me.txtPaymentType.Location = New System.Drawing.Point(131, 4)
        Me.txtPaymentType.Name = "txtPaymentType"
        Me.txtPaymentType.ReadOnly = True
        Me.txtPaymentType.Size = New System.Drawing.Size(149, 20)
        Me.txtPaymentType.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnRunDepositReport)
        Me.Panel1.Controls.Add(Me.Label112)
        Me.Panel1.Controls.Add(Me.Label111)
        Me.Panel1.Controls.Add(Me.Label110)
        Me.Panel1.Controls.Add(Me.dtpEndDepositDate)
        Me.Panel1.Controls.Add(Me.dtpStartDepositDate)
        Me.Panel1.Controls.Add(Me.chbDepositDateSearch)
        Me.Panel1.Controls.Add(Me.btnExportToExcel)
        Me.Panel1.Controls.Add(Me.chbNonZeroBalance)
        Me.Panel1.Controls.Add(Me.Label66)
        Me.Panel1.Controls.Add(Me.txtSelectedYear)
        Me.Panel1.Controls.Add(Me.Label65)
        Me.Panel1.Controls.Add(Me.txtSelectedFacilityName)
        Me.Panel1.Controls.Add(Me.Label64)
        Me.Panel1.Controls.Add(Me.txtSelectedAIRSNumber)
        Me.Panel1.Controls.Add(Me.btnViewSelectedFeeData)
        Me.Panel1.Controls.Add(Me.Label56)
        Me.Panel1.Controls.Add(Me.txtCount)
        Me.Panel1.Controls.Add(Me.btnViewBalance)
        Me.Panel1.Controls.Add(Me.bntViewTotalPaid)
        Me.Panel1.Controls.Add(Me.btnViewPaymentDue)
        Me.Panel1.Controls.Add(Me.txtBalance)
        Me.Panel1.Controls.Add(Me.txtTotalPaid)
        Me.Panel1.Controls.Add(Me.cboStatYear)
        Me.Panel1.Controls.Add(Me.btnViewDepositsStats)
        Me.Panel1.Controls.Add(Me.cboStatPayType)
        Me.Panel1.Controls.Add(Me.txtTotalPaymentDue)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(938, 163)
        Me.Panel1.TabIndex = 6
        '
        'btnRunDepositReport
        '
        Me.btnRunDepositReport.AutoSize = True
        Me.btnRunDepositReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunDepositReport.Location = New System.Drawing.Point(352, 70)
        Me.btnRunDepositReport.Name = "btnRunDepositReport"
        Me.btnRunDepositReport.Size = New System.Drawing.Size(72, 23)
        Me.btnRunDepositReport.TabIndex = 404
        Me.btnRunDepositReport.Text = "Run Report"
        Me.btnRunDepositReport.UseVisualStyleBackColor = True
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.Location = New System.Drawing.Point(250, 55)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(52, 13)
        Me.Label112.TabIndex = 403
        Me.Label112.Text = "End Date"
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.Location = New System.Drawing.Point(140, 54)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(55, 13)
        Me.Label111.TabIndex = 402
        Me.Label111.Text = "Start Date"
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Location = New System.Drawing.Point(4, 4)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(50, 13)
        Me.Label110.TabIndex = 401
        Me.Label110.Text = "Fee Year"
        '
        'dtpEndDepositDate
        '
        Me.dtpEndDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDepositDate.Location = New System.Drawing.Point(253, 70)
        Me.dtpEndDepositDate.Name = "dtpEndDepositDate"
        Me.dtpEndDepositDate.Size = New System.Drawing.Size(93, 20)
        Me.dtpEndDepositDate.TabIndex = 400
        '
        'dtpStartDepositDate
        '
        Me.dtpStartDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpStartDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDepositDate.Location = New System.Drawing.Point(143, 70)
        Me.dtpStartDepositDate.Name = "dtpStartDepositDate"
        Me.dtpStartDepositDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpStartDepositDate.TabIndex = 399
        '
        'chbDepositDateSearch
        '
        Me.chbDepositDateSearch.AutoSize = True
        Me.chbDepositDateSearch.Location = New System.Drawing.Point(18, 72)
        Me.chbDepositDateSearch.Name = "chbDepositDateSearch"
        Me.chbDepositDateSearch.Size = New System.Drawing.Size(115, 17)
        Me.chbDepositDateSearch.TabIndex = 28
        Me.chbDepositDateSearch.Text = "Use Deposit Dates"
        Me.chbDepositDateSearch.UseVisualStyleBackColor = True
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.AutoSize = True
        Me.btnExportToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportToExcel.Location = New System.Drawing.Point(842, 133)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(88, 23)
        Me.btnExportToExcel.TabIndex = 27
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'chbNonZeroBalance
        '
        Me.chbNonZeroBalance.AutoSize = True
        Me.chbNonZeroBalance.Location = New System.Drawing.Point(751, 70)
        Me.chbNonZeroBalance.Name = "chbNonZeroBalance"
        Me.chbNonZeroBalance.Size = New System.Drawing.Size(135, 17)
        Me.chbNonZeroBalance.TabIndex = 26
        Me.chbNonZeroBalance.Text = "Only Non-zero Balance"
        Me.chbNonZeroBalance.UseVisualStyleBackColor = True
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(542, 139)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(29, 13)
        Me.Label66.TabIndex = 25
        Me.Label66.Text = "Year"
        '
        'txtSelectedYear
        '
        Me.txtSelectedYear.Location = New System.Drawing.Point(577, 135)
        Me.txtSelectedYear.Name = "txtSelectedYear"
        Me.txtSelectedYear.ReadOnly = True
        Me.txtSelectedYear.Size = New System.Drawing.Size(54, 20)
        Me.txtSelectedYear.TabIndex = 24
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(182, 139)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(70, 13)
        Me.Label65.TabIndex = 23
        Me.Label65.Text = "Facility Name"
        '
        'txtSelectedFacilityName
        '
        Me.txtSelectedFacilityName.Location = New System.Drawing.Point(258, 135)
        Me.txtSelectedFacilityName.Name = "txtSelectedFacilityName"
        Me.txtSelectedFacilityName.ReadOnly = True
        Me.txtSelectedFacilityName.Size = New System.Drawing.Size(274, 20)
        Me.txtSelectedFacilityName.TabIndex = 22
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(8, 139)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(72, 13)
        Me.Label64.TabIndex = 21
        Me.Label64.Text = "AIRS Number"
        '
        'txtSelectedAIRSNumber
        '
        Me.txtSelectedAIRSNumber.Location = New System.Drawing.Point(86, 135)
        Me.txtSelectedAIRSNumber.Name = "txtSelectedAIRSNumber"
        Me.txtSelectedAIRSNumber.ReadOnly = True
        Me.txtSelectedAIRSNumber.Size = New System.Drawing.Size(82, 20)
        Me.txtSelectedAIRSNumber.TabIndex = 20
        '
        'btnViewSelectedFeeData
        '
        Me.btnViewSelectedFeeData.AutoSize = True
        Me.btnViewSelectedFeeData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewSelectedFeeData.Location = New System.Drawing.Point(653, 134)
        Me.btnViewSelectedFeeData.Name = "btnViewSelectedFeeData"
        Me.btnViewSelectedFeeData.Size = New System.Drawing.Size(40, 23)
        Me.btnViewSelectedFeeData.TabIndex = 19
        Me.btnViewSelectedFeeData.Text = "View"
        Me.btnViewSelectedFeeData.UseVisualStyleBackColor = True
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(14, 117)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(479, 13)
        Me.Label56.TabIndex = 18
        Me.Label56.Text = "Notice - AMENDMENT, ONE-TIME, and REFUND will all be summed up under Total Paymen" & _
            "t Due. "
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(764, 136)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(72, 20)
        Me.txtCount.TabIndex = 17
        '
        'btnViewBalance
        '
        Me.btnViewBalance.AutoSize = True
        Me.btnViewBalance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewBalance.Location = New System.Drawing.Point(749, 43)
        Me.btnViewBalance.Name = "btnViewBalance"
        Me.btnViewBalance.Size = New System.Drawing.Size(82, 23)
        Me.btnViewBalance.TabIndex = 15
        Me.btnViewBalance.Text = "View Balance"
        Me.btnViewBalance.UseVisualStyleBackColor = True
        '
        'bntViewTotalPaid
        '
        Me.bntViewTotalPaid.AutoSize = True
        Me.bntViewTotalPaid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.bntViewTotalPaid.Location = New System.Drawing.Point(622, 43)
        Me.bntViewTotalPaid.Name = "bntViewTotalPaid"
        Me.bntViewTotalPaid.Size = New System.Drawing.Size(91, 23)
        Me.bntViewTotalPaid.TabIndex = 14
        Me.bntViewTotalPaid.Text = "View Total Paid"
        Me.bntViewTotalPaid.UseVisualStyleBackColor = True
        '
        'btnViewPaymentDue
        '
        Me.btnViewPaymentDue.AutoSize = True
        Me.btnViewPaymentDue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewPaymentDue.Location = New System.Drawing.Point(495, 43)
        Me.btnViewPaymentDue.Name = "btnViewPaymentDue"
        Me.btnViewPaymentDue.Size = New System.Drawing.Size(107, 23)
        Me.btnViewPaymentDue.TabIndex = 13
        Me.btnViewPaymentDue.Text = "View Payment Due"
        Me.btnViewPaymentDue.UseVisualStyleBackColor = True
        '
        'txtBalance
        '
        Me.txtBalance.Location = New System.Drawing.Point(747, 19)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(121, 20)
        Me.txtBalance.TabIndex = 11
        '
        'txtTotalPaid
        '
        Me.txtTotalPaid.Location = New System.Drawing.Point(620, 19)
        Me.txtTotalPaid.Name = "txtTotalPaid"
        Me.txtTotalPaid.ReadOnly = True
        Me.txtTotalPaid.Size = New System.Drawing.Size(121, 20)
        Me.txtTotalPaid.TabIndex = 10
        '
        'cboStatYear
        '
        Me.cboStatYear.FormattingEnabled = True
        Me.cboStatYear.Location = New System.Drawing.Point(13, 20)
        Me.cboStatYear.Name = "cboStatYear"
        Me.cboStatYear.Size = New System.Drawing.Size(121, 21)
        Me.cboStatYear.TabIndex = 0
        '
        'btnViewDepositsStats
        '
        Me.btnViewDepositsStats.AutoSize = True
        Me.btnViewDepositsStats.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewDepositsStats.Location = New System.Drawing.Point(304, 18)
        Me.btnViewDepositsStats.Name = "btnViewDepositsStats"
        Me.btnViewDepositsStats.Size = New System.Drawing.Size(75, 23)
        Me.btnViewDepositsStats.TabIndex = 2
        Me.btnViewDepositsStats.Text = "View Report"
        Me.btnViewDepositsStats.UseVisualStyleBackColor = True
        '
        'cboStatPayType
        '
        Me.cboStatPayType.FormattingEnabled = True
        Me.cboStatPayType.Location = New System.Drawing.Point(148, 20)
        Me.cboStatPayType.Name = "cboStatPayType"
        Me.cboStatPayType.Size = New System.Drawing.Size(144, 21)
        Me.cboStatPayType.TabIndex = 4
        '
        'txtTotalPaymentDue
        '
        Me.txtTotalPaymentDue.Location = New System.Drawing.Point(493, 19)
        Me.txtTotalPaymentDue.Name = "txtTotalPaymentDue"
        Me.txtTotalPaymentDue.ReadOnly = True
        Me.txtTotalPaymentDue.Size = New System.Drawing.Size(121, 20)
        Me.txtTotalPaymentDue.TabIndex = 3
        '
        'TPFeeStatistics
        '
        Me.TPFeeStatistics.Controls.Add(Me.SCFeeStatistics)
        Me.TPFeeStatistics.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeStatistics.Name = "TPFeeStatistics"
        Me.TPFeeStatistics.Size = New System.Drawing.Size(938, 706)
        Me.TPFeeStatistics.TabIndex = 2
        Me.TPFeeStatistics.Text = "Fee Statistics"
        Me.TPFeeStatistics.UseVisualStyleBackColor = True
        '
        'SCFeeStatistics
        '
        Me.SCFeeStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCFeeStatistics.Location = New System.Drawing.Point(0, 0)
        Me.SCFeeStatistics.Name = "SCFeeStatistics"
        '
        'SCFeeStatistics.Panel1
        '
        Me.SCFeeStatistics.Panel1.AutoScroll = True
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblsumExtraNonresponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label42)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtExtraNonResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblExtraNonResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label35)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblsumextrafacility)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtextrafacilities)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblextrafacility)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblstate)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblzip)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblLastname)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblEmail)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblcity)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblcontactstreet)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblfirstname)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblFacilityName)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblPart70)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblNSPS)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lbloperationalstatus)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblclass)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label49)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label47)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label38)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label34)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblshutdowndate)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label32)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblAirsNo)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label27)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewsumTrueNonresponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblfeeYear)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumRemovedFacility)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.btnView)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewsumarryMailout)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label16)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumTotalResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.cboYear)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewSumInProcess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtfeeYear)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumFinalized)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label37)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewSumExtraResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtMailOutCount)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumNonResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtTotalFinalizedCount)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumLateresponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtTotalInProcessCount)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumtotalInporcess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label40)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblviewSumExtraToalFinalized)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtNonResponseCount)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewTrueNonresponsers)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label44)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtTrueNonResponsers)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtTotalincompliance)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label20)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label43)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewRemovedFacilities)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtTotaloutofcompliance)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtRemovedFacilities)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewTotalFinalized)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label21)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewTotalInProcess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label17)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewINCompliance)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label18)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewOutofcompliance)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label19)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewNonResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label25)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label26)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewMailOut)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtextraResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label62)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblextraResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtResponseCount)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label36)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewTotalResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label45)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtTotalResponse)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtMailoutFinalized)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label41)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewMailoutFinalized)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewExtraInProcess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.Label46)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtExtraInProcess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtMailOutInProcess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewExtraFinalized)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.lblViewMailoutInprocess)
        Me.SCFeeStatistics.Panel1.Controls.Add(Me.txtExtraFinalized)
        '
        'SCFeeStatistics.Panel2
        '
        Me.SCFeeStatistics.Panel2.Controls.Add(Me.dgvFeeDataCount2)
        Me.SCFeeStatistics.Panel2.Controls.Add(Me.gpViewdata2)
        Me.SCFeeStatistics.Size = New System.Drawing.Size(938, 706)
        Me.SCFeeStatistics.SplitterDistance = 508
        Me.SCFeeStatistics.TabIndex = 118
        '
        'lblsumExtraNonresponse
        '
        Me.lblsumExtraNonresponse.AutoSize = True
        Me.lblsumExtraNonresponse.Location = New System.Drawing.Point(324, 243)
        Me.lblsumExtraNonresponse.Name = "lblsumExtraNonresponse"
        Me.lblsumExtraNonresponse.Size = New System.Drawing.Size(53, 17)
        Me.lblsumExtraNonresponse.TabIndex = 19
        Me.lblsumExtraNonresponse.TabStop = True
        Me.lblsumExtraNonresponse.Text = "Summary"
        Me.lblsumExtraNonresponse.UseCompatibleTextRendering = True
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(56, 243)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(112, 13)
        Me.Label42.TabIndex = 158
        Me.Label42.Text = "Extra Non-responders:"
        '
        'txtExtraNonResponse
        '
        Me.txtExtraNonResponse.Location = New System.Drawing.Point(177, 243)
        Me.txtExtraNonResponse.Name = "txtExtraNonResponse"
        Me.txtExtraNonResponse.Size = New System.Drawing.Size(100, 20)
        Me.txtExtraNonResponse.TabIndex = 18
        '
        'lblExtraNonResponse
        '
        Me.lblExtraNonResponse.AutoSize = True
        Me.lblExtraNonResponse.Location = New System.Drawing.Point(410, 243)
        Me.lblExtraNonResponse.Name = "lblExtraNonResponse"
        Me.lblExtraNonResponse.Size = New System.Drawing.Size(33, 17)
        Me.lblExtraNonResponse.TabIndex = 20
        Me.lblExtraNonResponse.TabStop = True
        Me.lblExtraNonResponse.Text = "Detail"
        Me.lblExtraNonResponse.UseCompatibleTextRendering = True
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(15, 130)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(77, 13)
        Me.Label35.TabIndex = 155
        Me.Label35.Text = "Extra Facilities:"
        '
        'lblsumextrafacility
        '
        Me.lblsumextrafacility.AutoSize = True
        Me.lblsumextrafacility.Location = New System.Drawing.Point(325, 130)
        Me.lblsumextrafacility.Name = "lblsumextrafacility"
        Me.lblsumextrafacility.Size = New System.Drawing.Size(53, 17)
        Me.lblsumextrafacility.TabIndex = 7
        Me.lblsumextrafacility.TabStop = True
        Me.lblsumextrafacility.Text = "Summary"
        Me.lblsumextrafacility.UseCompatibleTextRendering = True
        '
        'txtextrafacilities
        '
        Me.txtextrafacilities.Location = New System.Drawing.Point(117, 127)
        Me.txtextrafacilities.Name = "txtextrafacilities"
        Me.txtextrafacilities.Size = New System.Drawing.Size(100, 20)
        Me.txtextrafacilities.TabIndex = 6
        '
        'lblextrafacility
        '
        Me.lblextrafacility.AutoSize = True
        Me.lblextrafacility.Location = New System.Drawing.Point(410, 130)
        Me.lblextrafacility.Name = "lblextrafacility"
        Me.lblextrafacility.Size = New System.Drawing.Size(33, 17)
        Me.lblextrafacility.TabIndex = 8
        Me.lblextrafacility.TabStop = True
        Me.lblextrafacility.Text = "Detail"
        Me.lblextrafacility.UseCompatibleTextRendering = True
        '
        'lblstate
        '
        Me.lblstate.AutoSize = True
        Me.lblstate.Location = New System.Drawing.Point(297, 736)
        Me.lblstate.Name = "lblstate"
        Me.lblstate.Size = New System.Drawing.Size(32, 13)
        Me.lblstate.TabIndex = 57
        Me.lblstate.Text = "State"
        '
        'lblzip
        '
        Me.lblzip.AutoSize = True
        Me.lblzip.Location = New System.Drawing.Point(334, 736)
        Me.lblzip.Name = "lblzip"
        Me.lblzip.Size = New System.Drawing.Size(22, 13)
        Me.lblzip.TabIndex = 149
        Me.lblzip.Text = "Zip"
        '
        'lblLastname
        '
        Me.lblLastname.AutoSize = True
        Me.lblLastname.Location = New System.Drawing.Point(271, 690)
        Me.lblLastname.Name = "lblLastname"
        Me.lblLastname.Size = New System.Drawing.Size(58, 13)
        Me.lblLastname.TabIndex = 52
        Me.lblLastname.Text = "Last Name"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(207, 761)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(32, 13)
        Me.lblEmail.TabIndex = 59
        Me.lblEmail.Text = "Email"
        '
        'lblcity
        '
        Me.lblcity.AutoSize = True
        Me.lblcity.Location = New System.Drawing.Point(207, 736)
        Me.lblcity.Name = "lblcity"
        Me.lblcity.Size = New System.Drawing.Size(24, 13)
        Me.lblcity.TabIndex = 56
        Me.lblcity.Text = "City"
        '
        'lblcontactstreet
        '
        Me.lblcontactstreet.AutoSize = True
        Me.lblcontactstreet.Location = New System.Drawing.Point(207, 713)
        Me.lblcontactstreet.Name = "lblcontactstreet"
        Me.lblcontactstreet.Size = New System.Drawing.Size(35, 13)
        Me.lblcontactstreet.TabIndex = 54
        Me.lblcontactstreet.Text = "Street"
        '
        'lblfirstname
        '
        Me.lblfirstname.AutoSize = True
        Me.lblfirstname.Location = New System.Drawing.Point(207, 690)
        Me.lblfirstname.Name = "lblfirstname"
        Me.lblfirstname.Size = New System.Drawing.Size(57, 13)
        Me.lblfirstname.TabIndex = 51
        Me.lblfirstname.Text = "First Name"
        '
        'lblFacilityName
        '
        Me.lblFacilityName.AutoSize = True
        Me.lblFacilityName.Location = New System.Drawing.Point(207, 668)
        Me.lblFacilityName.Name = "lblFacilityName"
        Me.lblFacilityName.Size = New System.Drawing.Size(70, 13)
        Me.lblFacilityName.TabIndex = 49
        Me.lblFacilityName.Text = "Facility Name"
        '
        'lblPart70
        '
        Me.lblPart70.AutoSize = True
        Me.lblPart70.Location = New System.Drawing.Point(65, 783)
        Me.lblPart70.Name = "lblPart70"
        Me.lblPart70.Size = New System.Drawing.Size(38, 13)
        Me.lblPart70.TabIndex = 60
        Me.lblPart70.Text = "Part70"
        '
        'lblNSPS
        '
        Me.lblNSPS.AutoSize = True
        Me.lblNSPS.Location = New System.Drawing.Point(59, 758)
        Me.lblNSPS.Name = "lblNSPS"
        Me.lblNSPS.Size = New System.Drawing.Size(36, 13)
        Me.lblNSPS.TabIndex = 58
        Me.lblNSPS.Text = "NSPS"
        '
        'lbloperationalstatus
        '
        Me.lbloperationalstatus.AutoSize = True
        Me.lbloperationalstatus.Location = New System.Drawing.Point(115, 735)
        Me.lbloperationalstatus.Name = "lbloperationalstatus"
        Me.lbloperationalstatus.Size = New System.Drawing.Size(51, 13)
        Me.lbloperationalstatus.TabIndex = 55
        Me.lbloperationalstatus.Text = "OpStatus"
        '
        'lblclass
        '
        Me.lblclass.AutoSize = True
        Me.lblclass.Location = New System.Drawing.Point(93, 713)
        Me.lblclass.Name = "lblclass"
        Me.lblclass.Size = New System.Drawing.Size(32, 13)
        Me.lblclass.TabIndex = 53
        Me.lblclass.Text = "Class"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(15, 783)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(44, 13)
        Me.Label49.TabIndex = 127
        Me.Label49.Text = "Part 70:"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(15, 758)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(39, 13)
        Me.Label47.TabIndex = 125
        Me.Label47.Text = "NSPS:"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(15, 735)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(97, 13)
        Me.Label38.TabIndex = 123
        Me.Label38.Text = "Operational Status:"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(15, 713)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(72, 13)
        Me.Label34.TabIndex = 121
        Me.Label34.Text = "Source Class:"
        '
        'lblshutdowndate
        '
        Me.lblshutdowndate.AutoSize = True
        Me.lblshutdowndate.Location = New System.Drawing.Point(116, 690)
        Me.lblshutdowndate.Name = "lblshutdowndate"
        Me.lblshutdowndate.Size = New System.Drawing.Size(30, 13)
        Me.lblshutdowndate.TabIndex = 50
        Me.lblshutdowndate.Text = "Date"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(15, 690)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(89, 13)
        Me.Label32.TabIndex = 119
        Me.Label32.Text = "Shut Down Date:"
        '
        'lblAirsNo
        '
        Me.lblAirsNo.AutoSize = True
        Me.lblAirsNo.Location = New System.Drawing.Point(18, 668)
        Me.lblAirsNo.Name = "lblAirsNo"
        Me.lblAirsNo.Size = New System.Drawing.Size(64, 13)
        Me.lblAirsNo.TabIndex = 118
        Me.lblAirsNo.Text = "Airs Number"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label27.Location = New System.Drawing.Point(13, 15)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(283, 22)
        Me.Label27.TabIndex = 116
        Me.Label27.Text = "Fee Summary for Calendar Year "
        '
        'lblviewsumTrueNonresponse
        '
        Me.lblviewsumTrueNonresponse.AutoSize = True
        Me.lblviewsumTrueNonresponse.Location = New System.Drawing.Point(325, 220)
        Me.lblviewsumTrueNonresponse.Name = "lblviewsumTrueNonresponse"
        Me.lblviewsumTrueNonresponse.Size = New System.Drawing.Size(53, 17)
        Me.lblviewsumTrueNonresponse.TabIndex = 16
        Me.lblviewsumTrueNonresponse.TabStop = True
        Me.lblviewsumTrueNonresponse.Text = "Summary"
        Me.lblviewsumTrueNonresponse.UseCompatibleTextRendering = True
        '
        'lblfeeYear
        '
        Me.lblfeeYear.AutoSize = True
        Me.lblfeeYear.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfeeYear.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblfeeYear.Location = New System.Drawing.Point(302, 15)
        Me.lblfeeYear.Name = "lblfeeYear"
        Me.lblfeeYear.Size = New System.Drawing.Size(50, 22)
        Me.lblfeeYear.TabIndex = 117
        Me.lblfeeYear.Text = "Year"
        '
        'lblviewSumRemovedFacility
        '
        Me.lblviewSumRemovedFacility.AutoSize = True
        Me.lblviewSumRemovedFacility.Location = New System.Drawing.Point(325, 192)
        Me.lblviewSumRemovedFacility.Name = "lblviewSumRemovedFacility"
        Me.lblviewSumRemovedFacility.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumRemovedFacility.TabIndex = 13
        Me.lblviewSumRemovedFacility.TabStop = True
        Me.lblviewSumRemovedFacility.Text = "Summary"
        Me.lblviewSumRemovedFacility.UseCompatibleTextRendering = True
        '
        'btnView
        '
        Me.btnView.AutoSize = True
        Me.btnView.Location = New System.Drawing.Point(195, 54)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(40, 23)
        Me.btnView.TabIndex = 1
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'lblviewsumarryMailout
        '
        Me.lblviewsumarryMailout.AutoSize = True
        Me.lblviewsumarryMailout.Location = New System.Drawing.Point(325, 104)
        Me.lblviewsumarryMailout.Name = "lblviewsumarryMailout"
        Me.lblviewsumarryMailout.Size = New System.Drawing.Size(53, 17)
        Me.lblviewsumarryMailout.TabIndex = 4
        Me.lblviewsumarryMailout.TabStop = True
        Me.lblviewsumarryMailout.Text = "Summary"
        Me.lblviewsumarryMailout.UseCompatibleTextRendering = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(15, 57)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 13)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Select a Year:"
        '
        'lblviewSumTotalResponse
        '
        Me.lblviewSumTotalResponse.AutoSize = True
        Me.lblviewSumTotalResponse.Location = New System.Drawing.Point(322, 468)
        Me.lblviewSumTotalResponse.Name = "lblviewSumTotalResponse"
        Me.lblviewSumTotalResponse.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumTotalResponse.TabIndex = 36
        Me.lblviewSumTotalResponse.TabStop = True
        Me.lblviewSumTotalResponse.Text = "Summary"
        Me.lblviewSumTotalResponse.UseCompatibleTextRendering = True
        '
        'cboYear
        '
        Me.cboYear.FormattingEnabled = True
        Me.cboYear.Location = New System.Drawing.Point(92, 54)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.Size = New System.Drawing.Size(97, 21)
        Me.cboYear.TabIndex = 0
        '
        'lblViewSumInProcess
        '
        Me.lblViewSumInProcess.AutoSize = True
        Me.lblViewSumInProcess.Location = New System.Drawing.Point(322, 340)
        Me.lblViewSumInProcess.Name = "lblViewSumInProcess"
        Me.lblViewSumInProcess.Size = New System.Drawing.Size(53, 17)
        Me.lblViewSumInProcess.TabIndex = 26
        Me.lblViewSumInProcess.TabStop = True
        Me.lblViewSumInProcess.Text = "Summary"
        Me.lblViewSumInProcess.UseCompatibleTextRendering = True
        '
        'txtfeeYear
        '
        Me.txtfeeYear.Location = New System.Drawing.Point(296, 54)
        Me.txtfeeYear.Name = "txtfeeYear"
        Me.txtfeeYear.Size = New System.Drawing.Size(100, 20)
        Me.txtfeeYear.TabIndex = 2
        Me.txtfeeYear.Visible = False
        '
        'lblviewSumFinalized
        '
        Me.lblviewSumFinalized.AutoSize = True
        Me.lblviewSumFinalized.Location = New System.Drawing.Point(322, 311)
        Me.lblviewSumFinalized.Name = "lblviewSumFinalized"
        Me.lblviewSumFinalized.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumFinalized.TabIndex = 23
        Me.lblviewSumFinalized.TabStop = True
        Me.lblviewSumFinalized.Text = "Summary"
        Me.lblviewSumFinalized.UseCompatibleTextRendering = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(15, 104)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(44, 13)
        Me.Label37.TabIndex = 62
        Me.Label37.Text = "Mailout:"
        '
        'lblViewSumExtraResponse
        '
        Me.lblViewSumExtraResponse.AutoSize = True
        Me.lblViewSumExtraResponse.Location = New System.Drawing.Point(322, 374)
        Me.lblViewSumExtraResponse.Name = "lblViewSumExtraResponse"
        Me.lblViewSumExtraResponse.Size = New System.Drawing.Size(53, 17)
        Me.lblViewSumExtraResponse.TabIndex = 29
        Me.lblViewSumExtraResponse.TabStop = True
        Me.lblViewSumExtraResponse.Text = "Summary"
        Me.lblViewSumExtraResponse.UseCompatibleTextRendering = True
        '
        'txtMailOutCount
        '
        Me.txtMailOutCount.Location = New System.Drawing.Point(117, 101)
        Me.txtMailOutCount.Name = "txtMailOutCount"
        Me.txtMailOutCount.Size = New System.Drawing.Size(100, 20)
        Me.txtMailOutCount.TabIndex = 3
        '
        'lblviewSumNonResponse
        '
        Me.lblviewSumNonResponse.AutoSize = True
        Me.lblviewSumNonResponse.Location = New System.Drawing.Point(324, 166)
        Me.lblviewSumNonResponse.Name = "lblviewSumNonResponse"
        Me.lblviewSumNonResponse.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumNonResponse.TabIndex = 10
        Me.lblviewSumNonResponse.TabStop = True
        Me.lblviewSumNonResponse.Text = "Summary"
        Me.lblviewSumNonResponse.UseCompatibleTextRendering = True
        '
        'txtTotalFinalizedCount
        '
        Me.txtTotalFinalizedCount.Location = New System.Drawing.Point(177, 495)
        Me.txtTotalFinalizedCount.Name = "txtTotalFinalizedCount"
        Me.txtTotalFinalizedCount.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalFinalizedCount.TabIndex = 38
        '
        'lblviewSumLateresponse
        '
        Me.lblviewSumLateresponse.AutoSize = True
        Me.lblviewSumLateresponse.Location = New System.Drawing.Point(322, 612)
        Me.lblviewSumLateresponse.Name = "lblviewSumLateresponse"
        Me.lblviewSumLateresponse.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumLateresponse.TabIndex = 47
        Me.lblviewSumLateresponse.TabStop = True
        Me.lblviewSumLateresponse.Text = "Summary"
        Me.lblviewSumLateresponse.UseCompatibleTextRendering = True
        '
        'txtTotalInProcessCount
        '
        Me.txtTotalInProcessCount.Location = New System.Drawing.Point(177, 521)
        Me.txtTotalInProcessCount.Name = "txtTotalInProcessCount"
        Me.txtTotalInProcessCount.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalInProcessCount.TabIndex = 41
        '
        'lblviewSumtotalInporcess
        '
        Me.lblviewSumtotalInporcess.AutoSize = True
        Me.lblviewSumtotalInporcess.Location = New System.Drawing.Point(322, 524)
        Me.lblviewSumtotalInporcess.Name = "lblviewSumtotalInporcess"
        Me.lblviewSumtotalInporcess.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumtotalInporcess.TabIndex = 42
        Me.lblviewSumtotalInporcess.TabStop = True
        Me.lblviewSumtotalInporcess.Text = "Summary"
        Me.lblviewSumtotalInporcess.UseCompatibleTextRendering = True
        Me.lblviewSumtotalInporcess.Visible = False
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(87, 166)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(81, 13)
        Me.Label40.TabIndex = 66
        Me.Label40.Text = "Non-Response:"
        '
        'lblviewSumExtraToalFinalized
        '
        Me.lblviewSumExtraToalFinalized.AutoSize = True
        Me.lblviewSumExtraToalFinalized.Location = New System.Drawing.Point(322, 495)
        Me.lblviewSumExtraToalFinalized.Name = "lblviewSumExtraToalFinalized"
        Me.lblviewSumExtraToalFinalized.Size = New System.Drawing.Size(53, 17)
        Me.lblviewSumExtraToalFinalized.TabIndex = 39
        Me.lblviewSumExtraToalFinalized.TabStop = True
        Me.lblviewSumExtraToalFinalized.Text = "Summary"
        Me.lblviewSumExtraToalFinalized.UseCompatibleTextRendering = True
        '
        'txtNonResponseCount
        '
        Me.txtNonResponseCount.Location = New System.Drawing.Point(177, 166)
        Me.txtNonResponseCount.Name = "txtNonResponseCount"
        Me.txtNonResponseCount.Size = New System.Drawing.Size(100, 20)
        Me.txtNonResponseCount.TabIndex = 9
        '
        'lblViewTrueNonresponsers
        '
        Me.lblViewTrueNonresponsers.AutoSize = True
        Me.lblViewTrueNonresponsers.Location = New System.Drawing.Point(410, 220)
        Me.lblViewTrueNonresponsers.Name = "lblViewTrueNonresponsers"
        Me.lblViewTrueNonresponsers.Size = New System.Drawing.Size(33, 17)
        Me.lblViewTrueNonresponsers.TabIndex = 17
        Me.lblViewTrueNonresponsers.TabStop = True
        Me.lblViewTrueNonresponsers.Text = "Detail"
        Me.lblViewTrueNonresponsers.UseCompatibleTextRendering = True
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(59, 590)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(101, 13)
        Me.Label44.TabIndex = 102
        Me.Label44.Text = "On Time Response:"
        '
        'txtTrueNonResponsers
        '
        Me.txtTrueNonResponsers.Location = New System.Drawing.Point(177, 217)
        Me.txtTrueNonResponsers.Name = "txtTrueNonResponsers"
        Me.txtTrueNonResponsers.Size = New System.Drawing.Size(100, 20)
        Me.txtTrueNonResponsers.TabIndex = 15
        '
        'txtTotalincompliance
        '
        Me.txtTotalincompliance.Location = New System.Drawing.Point(177, 583)
        Me.txtTotalincompliance.Name = "txtTotalincompliance"
        Me.txtTotalincompliance.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalincompliance.TabIndex = 44
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(46, 220)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(122, 13)
        Me.Label20.TabIndex = 73
        Me.Label20.Text = "Mailout Non-responders:"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(78, 612)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(82, 13)
        Me.Label43.TabIndex = 103
        Me.Label43.Text = "Late Response:"
        '
        'lblViewRemovedFacilities
        '
        Me.lblViewRemovedFacilities.AutoSize = True
        Me.lblViewRemovedFacilities.Location = New System.Drawing.Point(410, 192)
        Me.lblViewRemovedFacilities.Name = "lblViewRemovedFacilities"
        Me.lblViewRemovedFacilities.Size = New System.Drawing.Size(33, 17)
        Me.lblViewRemovedFacilities.TabIndex = 14
        Me.lblViewRemovedFacilities.TabStop = True
        Me.lblViewRemovedFacilities.Text = "Detail"
        Me.lblViewRemovedFacilities.UseCompatibleTextRendering = True
        '
        'txtTotaloutofcompliance
        '
        Me.txtTotaloutofcompliance.Location = New System.Drawing.Point(177, 609)
        Me.txtTotaloutofcompliance.Name = "txtTotaloutofcompliance"
        Me.txtTotaloutofcompliance.Size = New System.Drawing.Size(100, 20)
        Me.txtTotaloutofcompliance.TabIndex = 46
        '
        'txtRemovedFacilities
        '
        Me.txtRemovedFacilities.Location = New System.Drawing.Point(177, 192)
        Me.txtRemovedFacilities.Name = "txtRemovedFacilities"
        Me.txtRemovedFacilities.Size = New System.Drawing.Size(100, 20)
        Me.txtRemovedFacilities.TabIndex = 12
        '
        'lblViewTotalFinalized
        '
        Me.lblViewTotalFinalized.AutoSize = True
        Me.lblViewTotalFinalized.Location = New System.Drawing.Point(407, 495)
        Me.lblViewTotalFinalized.Name = "lblViewTotalFinalized"
        Me.lblViewTotalFinalized.Size = New System.Drawing.Size(33, 17)
        Me.lblViewTotalFinalized.TabIndex = 40
        Me.lblViewTotalFinalized.TabStop = True
        Me.lblViewTotalFinalized.Text = "Detail"
        Me.lblViewTotalFinalized.UseCompatibleTextRendering = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(69, 192)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(99, 13)
        Me.Label21.TabIndex = 69
        Me.Label21.Text = "Removed Facilities:"
        '
        'lblViewTotalInProcess
        '
        Me.lblViewTotalInProcess.AutoSize = True
        Me.lblViewTotalInProcess.Location = New System.Drawing.Point(407, 524)
        Me.lblViewTotalInProcess.Name = "lblViewTotalInProcess"
        Me.lblViewTotalInProcess.Size = New System.Drawing.Size(33, 17)
        Me.lblViewTotalInProcess.TabIndex = 43
        Me.lblViewTotalInProcess.TabStop = True
        Me.lblViewTotalInProcess.Text = "Detail"
        Me.lblViewTotalInProcess.UseCompatibleTextRendering = True
        Me.lblViewTotalInProcess.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(113, 528)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 100
        Me.Label17.Text = "In process:"
        '
        'lblViewINCompliance
        '
        Me.lblViewINCompliance.AutoSize = True
        Me.lblViewINCompliance.Location = New System.Drawing.Point(322, 586)
        Me.lblViewINCompliance.Name = "lblViewINCompliance"
        Me.lblViewINCompliance.Size = New System.Drawing.Size(53, 17)
        Me.lblViewINCompliance.TabIndex = 45
        Me.lblViewINCompliance.TabStop = True
        Me.lblViewINCompliance.Text = "Summary"
        Me.lblViewINCompliance.UseCompatibleTextRendering = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(113, 502)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 99
        Me.Label18.Text = "Finalized:"
        '
        'lblViewOutofcompliance
        '
        Me.lblViewOutofcompliance.AutoSize = True
        Me.lblViewOutofcompliance.Location = New System.Drawing.Point(407, 612)
        Me.lblViewOutofcompliance.Name = "lblViewOutofcompliance"
        Me.lblViewOutofcompliance.Size = New System.Drawing.Size(33, 17)
        Me.lblViewOutofcompliance.TabIndex = 48
        Me.lblViewOutofcompliance.TabStop = True
        Me.lblViewOutofcompliance.Text = "Detail"
        Me.lblViewOutofcompliance.UseCompatibleTextRendering = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(114, 425)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(59, 13)
        Me.Label19.TabIndex = 96
        Me.Label19.Text = "In process:"
        '
        'lblViewNonResponse
        '
        Me.lblViewNonResponse.AutoSize = True
        Me.lblViewNonResponse.Location = New System.Drawing.Point(407, 166)
        Me.lblViewNonResponse.Name = "lblViewNonResponse"
        Me.lblViewNonResponse.Size = New System.Drawing.Size(33, 17)
        Me.lblViewNonResponse.TabIndex = 11
        Me.lblViewNonResponse.TabStop = True
        Me.lblViewNonResponse.Text = "Detail"
        Me.lblViewNonResponse.UseCompatibleTextRendering = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(114, 400)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(51, 13)
        Me.Label25.TabIndex = 93
        Me.Label25.Text = "Finalized:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(15, 377)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(85, 13)
        Me.Label26.TabIndex = 89
        Me.Label26.Text = "Extra Response:"
        '
        'lblViewMailOut
        '
        Me.lblViewMailOut.AutoSize = True
        Me.lblViewMailOut.Location = New System.Drawing.Point(410, 104)
        Me.lblViewMailOut.Name = "lblViewMailOut"
        Me.lblViewMailOut.Size = New System.Drawing.Size(33, 17)
        Me.lblViewMailOut.TabIndex = 5
        Me.lblViewMailOut.TabStop = True
        Me.lblViewMailOut.Text = "Detail"
        Me.lblViewMailOut.UseCompatibleTextRendering = True
        '
        'txtextraResponse
        '
        Me.txtextraResponse.Location = New System.Drawing.Point(116, 374)
        Me.txtextraResponse.Name = "txtextraResponse"
        Me.txtextraResponse.Size = New System.Drawing.Size(100, 20)
        Me.txtextraResponse.TabIndex = 28
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(46, 287)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(58, 13)
        Me.Label62.TabIndex = 77
        Me.Label62.Text = "Response:"
        '
        'lblextraResponse
        '
        Me.lblextraResponse.AutoSize = True
        Me.lblextraResponse.Location = New System.Drawing.Point(407, 374)
        Me.lblextraResponse.Name = "lblextraResponse"
        Me.lblextraResponse.Size = New System.Drawing.Size(33, 17)
        Me.lblextraResponse.TabIndex = 30
        Me.lblextraResponse.TabStop = True
        Me.lblextraResponse.Text = "Detail"
        Me.lblextraResponse.UseCompatibleTextRendering = True
        '
        'txtResponseCount
        '
        Me.txtResponseCount.Location = New System.Drawing.Point(112, 284)
        Me.txtResponseCount.Name = "txtResponseCount"
        Me.txtResponseCount.Size = New System.Drawing.Size(100, 20)
        Me.txtResponseCount.TabIndex = 21
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(15, 465)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(85, 13)
        Me.Label36.TabIndex = 98
        Me.Label36.Text = "Total Response:"
        '
        'lblViewTotalResponse
        '
        Me.lblViewTotalResponse.AutoSize = True
        Me.lblViewTotalResponse.Location = New System.Drawing.Point(407, 468)
        Me.lblViewTotalResponse.Name = "lblViewTotalResponse"
        Me.lblViewTotalResponse.Size = New System.Drawing.Size(33, 17)
        Me.lblViewTotalResponse.TabIndex = 37
        Me.lblViewTotalResponse.TabStop = True
        Me.lblViewTotalResponse.Text = "Detail"
        Me.lblViewTotalResponse.UseCompatibleTextRendering = True
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(109, 314)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(51, 13)
        Me.Label45.TabIndex = 80
        Me.Label45.Text = "Finalized:"
        '
        'txtTotalResponse
        '
        Me.txtTotalResponse.Location = New System.Drawing.Point(114, 465)
        Me.txtTotalResponse.Name = "txtTotalResponse"
        Me.txtTotalResponse.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalResponse.TabIndex = 35
        '
        'txtMailoutFinalized
        '
        Me.txtMailoutFinalized.Location = New System.Drawing.Point(177, 311)
        Me.txtMailoutFinalized.Name = "txtMailoutFinalized"
        Me.txtMailoutFinalized.Size = New System.Drawing.Size(100, 20)
        Me.txtMailoutFinalized.TabIndex = 22
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(15, 565)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(89, 13)
        Me.Label41.TabIndex = 101
        Me.Label41.Text = "Timeliness Status"
        '
        'lblViewMailoutFinalized
        '
        Me.lblViewMailoutFinalized.AutoSize = True
        Me.lblViewMailoutFinalized.Location = New System.Drawing.Point(407, 311)
        Me.lblViewMailoutFinalized.Name = "lblViewMailoutFinalized"
        Me.lblViewMailoutFinalized.Size = New System.Drawing.Size(33, 17)
        Me.lblViewMailoutFinalized.TabIndex = 24
        Me.lblViewMailoutFinalized.TabStop = True
        Me.lblViewMailoutFinalized.Text = "Detail"
        Me.lblViewMailoutFinalized.UseCompatibleTextRendering = True
        '
        'lblViewExtraInProcess
        '
        Me.lblViewExtraInProcess.AutoSize = True
        Me.lblViewExtraInProcess.Location = New System.Drawing.Point(322, 429)
        Me.lblViewExtraInProcess.Name = "lblViewExtraInProcess"
        Me.lblViewExtraInProcess.Size = New System.Drawing.Size(53, 17)
        Me.lblViewExtraInProcess.TabIndex = 34
        Me.lblViewExtraInProcess.TabStop = True
        Me.lblViewExtraInProcess.Text = "Summary"
        Me.lblViewExtraInProcess.UseCompatibleTextRendering = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(109, 340)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(59, 13)
        Me.Label46.TabIndex = 84
        Me.Label46.Text = "In process:"
        '
        'txtExtraInProcess
        '
        Me.txtExtraInProcess.Location = New System.Drawing.Point(177, 423)
        Me.txtExtraInProcess.Name = "txtExtraInProcess"
        Me.txtExtraInProcess.Size = New System.Drawing.Size(100, 20)
        Me.txtExtraInProcess.TabIndex = 33
        '
        'txtMailOutInProcess
        '
        Me.txtMailOutInProcess.Location = New System.Drawing.Point(177, 337)
        Me.txtMailOutInProcess.Name = "txtMailOutInProcess"
        Me.txtMailOutInProcess.Size = New System.Drawing.Size(100, 20)
        Me.txtMailOutInProcess.TabIndex = 25
        '
        'lblViewExtraFinalized
        '
        Me.lblViewExtraFinalized.AutoSize = True
        Me.lblViewExtraFinalized.Location = New System.Drawing.Point(322, 400)
        Me.lblViewExtraFinalized.Name = "lblViewExtraFinalized"
        Me.lblViewExtraFinalized.Size = New System.Drawing.Size(53, 17)
        Me.lblViewExtraFinalized.TabIndex = 32
        Me.lblViewExtraFinalized.TabStop = True
        Me.lblViewExtraFinalized.Text = "Summary"
        Me.lblViewExtraFinalized.UseCompatibleTextRendering = True
        '
        'lblViewMailoutInprocess
        '
        Me.lblViewMailoutInprocess.AutoSize = True
        Me.lblViewMailoutInprocess.Location = New System.Drawing.Point(407, 340)
        Me.lblViewMailoutInprocess.Name = "lblViewMailoutInprocess"
        Me.lblViewMailoutInprocess.Size = New System.Drawing.Size(33, 17)
        Me.lblViewMailoutInprocess.TabIndex = 27
        Me.lblViewMailoutInprocess.TabStop = True
        Me.lblViewMailoutInprocess.Text = "Detail"
        Me.lblViewMailoutInprocess.UseCompatibleTextRendering = True
        '
        'txtExtraFinalized
        '
        Me.txtExtraFinalized.Location = New System.Drawing.Point(177, 397)
        Me.txtExtraFinalized.Name = "txtExtraFinalized"
        Me.txtExtraFinalized.Size = New System.Drawing.Size(100, 20)
        Me.txtExtraFinalized.TabIndex = 31
        '
        'dgvFeeDataCount2
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFeeDataCount2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvFeeDataCount2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFeeDataCount2.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvFeeDataCount2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFeeDataCount2.Location = New System.Drawing.Point(0, 54)
        Me.dgvFeeDataCount2.Name = "dgvFeeDataCount2"
        Me.dgvFeeDataCount2.ReadOnly = True
        Me.dgvFeeDataCount2.Size = New System.Drawing.Size(426, 652)
        Me.dgvFeeDataCount2.TabIndex = 1
        '
        'gpViewdata2
        '
        Me.gpViewdata2.Controls.Add(Me.BtnExportExcel2)
        Me.gpViewdata2.Controls.Add(Me.txtRecordNumber2)
        Me.gpViewdata2.Dock = System.Windows.Forms.DockStyle.Top
        Me.gpViewdata2.Location = New System.Drawing.Point(0, 0)
        Me.gpViewdata2.Name = "gpViewdata2"
        Me.gpViewdata2.Size = New System.Drawing.Size(426, 54)
        Me.gpViewdata2.TabIndex = 0
        Me.gpViewdata2.TabStop = False
        Me.gpViewdata2.Text = "View Data"
        '
        'BtnExportExcel2
        '
        Me.BtnExportExcel2.Location = New System.Drawing.Point(130, 16)
        Me.BtnExportExcel2.Name = "BtnExportExcel2"
        Me.BtnExportExcel2.Size = New System.Drawing.Size(95, 23)
        Me.BtnExportExcel2.TabIndex = 1
        Me.BtnExportExcel2.Text = "Export To Excel"
        Me.BtnExportExcel2.UseVisualStyleBackColor = True
        '
        'txtRecordNumber2
        '
        Me.txtRecordNumber2.Location = New System.Drawing.Point(6, 19)
        Me.txtRecordNumber2.Name = "txtRecordNumber2"
        Me.txtRecordNumber2.Size = New System.Drawing.Size(100, 20)
        Me.txtRecordNumber2.TabIndex = 0
        '
        'TPMiscWebTools
        '
        Me.TPMiscWebTools.Controls.Add(Me.TabControl1)
        Me.TPMiscWebTools.Location = New System.Drawing.Point(4, 22)
        Me.TPMiscWebTools.Name = "TPMiscWebTools"
        Me.TPMiscWebTools.Size = New System.Drawing.Size(938, 706)
        Me.TPMiscWebTools.TabIndex = 3
        Me.TPMiscWebTools.Text = "Misc. Web Tools"
        Me.TPMiscWebTools.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPWebUsers)
        Me.TabControl1.Controls.Add(Me.TPWebUsers1)
        Me.TabControl1.Controls.Add(Me.TPActivate)
        Me.TabControl1.Controls.Add(Me.TPFeeFacility)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(938, 706)
        Me.TabControl1.TabIndex = 150
        '
        'TPWebUsers
        '
        Me.TPWebUsers.Controls.Add(Me.pnlUser)
        Me.TPWebUsers.Controls.Add(Me.PanelFacility)
        Me.TPWebUsers.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers.Name = "TPWebUsers"
        Me.TPWebUsers.Size = New System.Drawing.Size(930, 680)
        Me.TPWebUsers.TabIndex = 1
        Me.TPWebUsers.Text = "Web App Users - Facility"
        Me.TPWebUsers.UseVisualStyleBackColor = True
        '
        'pnlUser
        '
        Me.pnlUser.Controls.Add(Me.cboUsers)
        Me.pnlUser.Controls.Add(Me.Label28)
        Me.pnlUser.Controls.Add(Me.btnDelete)
        Me.pnlUser.Controls.Add(Me.btnUpdate)
        Me.pnlUser.Controls.Add(Me.Label29)
        Me.pnlUser.Controls.Add(Me.txtEmail)
        Me.pnlUser.Controls.Add(Me.btnAddUser)
        Me.pnlUser.Controls.Add(Me.dgrUsers)
        Me.pnlUser.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUser.Location = New System.Drawing.Point(0, 41)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(930, 278)
        Me.pnlUser.TabIndex = 147
        Me.pnlUser.Visible = False
        '
        'cboUsers
        '
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(166, 227)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(200, 21)
        Me.cboUsers.TabIndex = 281
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 233)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(150, 13)
        Me.Label28.TabIndex = 280
        Me.Label28.Text = "Delete a User for this Facility:  "
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(371, 228)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 20)
        Me.btnDelete.TabIndex = 278
        Me.btnDelete.Text = "Delete User"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(2, 167)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdate.TabIndex = 277
        Me.btnUpdate.Text = "Save Changes"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 201)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(132, 13)
        Me.Label29.TabIndex = 276
        Me.Label29.Text = "Add a User to this Facility: "
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(166, 197)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(200, 20)
        Me.txtEmail.TabIndex = 275
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(371, 196)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(62, 20)
        Me.btnAddUser.TabIndex = 274
        Me.btnAddUser.Text = "Add User"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'dgrUsers
        '
        Me.dgrUsers.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgrUsers.BackColor = System.Drawing.Color.GhostWhite
        Me.dgrUsers.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgrUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgrUsers.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgrUsers.CaptionForeColor = System.Drawing.Color.White
        Me.dgrUsers.CaptionText = "Current Users for this Facility"
        Me.dgrUsers.DataMember = ""
        Me.dgrUsers.FlatMode = True
        Me.dgrUsers.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgrUsers.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrUsers.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgrUsers.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgrUsers.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgrUsers.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgrUsers.LinkColor = System.Drawing.Color.Teal
        Me.dgrUsers.Location = New System.Drawing.Point(2, 5)
        Me.dgrUsers.Name = "dgrUsers"
        Me.dgrUsers.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgrUsers.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrUsers.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgrUsers.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgrUsers.Size = New System.Drawing.Size(709, 157)
        Me.dgrUsers.TabIndex = 273
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.Label33)
        Me.PanelFacility.Controls.Add(Me.Label177)
        Me.PanelFacility.Controls.Add(Me.mtbAIRSNumber)
        Me.PanelFacility.Controls.Add(Me.llbViewUserData)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(930, 41)
        Me.PanelFacility.TabIndex = 146
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(22, 130)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(110, 16)
        Me.Label33.TabIndex = 153
        Me.Label33.Text = "Extra Facilities"
        '
        'Label177
        '
        Me.Label177.AutoSize = True
        Me.Label177.Location = New System.Drawing.Point(4, 13)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(72, 13)
        Me.Label177.TabIndex = 285
        Me.Label177.Text = "AIRS Number"
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(82, 9)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(66, 20)
        Me.mtbAIRSNumber.TabIndex = 287
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'llbViewUserData
        '
        Me.llbViewUserData.AutoSize = True
        Me.llbViewUserData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbViewUserData.Location = New System.Drawing.Point(161, 13)
        Me.llbViewUserData.Name = "llbViewUserData"
        Me.llbViewUserData.Size = New System.Drawing.Size(56, 13)
        Me.llbViewUserData.TabIndex = 288
        Me.llbViewUserData.TabStop = True
        Me.llbViewUserData.Text = "View Data"
        '
        'TPWebUsers1
        '
        Me.TPWebUsers1.Controls.Add(Me.pnlUserFacility)
        Me.TPWebUsers1.Controls.Add(Me.pnlUserEmail)
        Me.TPWebUsers1.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers1.Name = "TPWebUsers1"
        Me.TPWebUsers1.Size = New System.Drawing.Size(930, 680)
        Me.TPWebUsers1.TabIndex = 2
        Me.TPWebUsers1.Text = "Web App Users - Email"
        Me.TPWebUsers1.UseVisualStyleBackColor = True
        '
        'pnlUserFacility
        '
        Me.pnlUserFacility.Controls.Add(Me.pnlUserInfo)
        Me.pnlUserFacility.Controls.Add(Me.mtbFacilityToAdd)
        Me.pnlUserFacility.Controls.Add(Me.cboFacilityToDelete)
        Me.pnlUserFacility.Controls.Add(Me.Label75)
        Me.pnlUserFacility.Controls.Add(Me.btnDeleteFacilityUser)
        Me.pnlUserFacility.Controls.Add(Me.btnUpdateUser)
        Me.pnlUserFacility.Controls.Add(Me.Label53)
        Me.pnlUserFacility.Controls.Add(Me.btnAddFacilitytoUser)
        Me.pnlUserFacility.Controls.Add(Me.dgrFacilities)
        Me.pnlUserFacility.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUserFacility.Location = New System.Drawing.Point(0, 49)
        Me.pnlUserFacility.Name = "pnlUserFacility"
        Me.pnlUserFacility.Size = New System.Drawing.Size(930, 631)
        Me.pnlUserFacility.TabIndex = 148
        Me.pnlUserFacility.Visible = False
        '
        'pnlUserInfo
        '
        Me.pnlUserInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlUserInfo.Controls.Add(Me.btnChangeEmailAddress)
        Me.pnlUserInfo.Controls.Add(Me.txtEditEmail)
        Me.pnlUserInfo.Controls.Add(Me.lblConfirmDate)
        Me.pnlUserInfo.Controls.Add(Me.lblLastLogIn)
        Me.pnlUserInfo.Controls.Add(Me.txtEditUserPassword)
        Me.pnlUserInfo.Controls.Add(Me.btnUpdatePassword)
        Me.pnlUserInfo.Controls.Add(Me.txtWebUserID)
        Me.pnlUserInfo.Controls.Add(Me.btnSaveEditedData)
        Me.pnlUserInfo.Controls.Add(Me.mtbEditZipCode)
        Me.pnlUserInfo.Controls.Add(Me.mtbEditState)
        Me.pnlUserInfo.Controls.Add(Me.mtbEditFaxNumber)
        Me.pnlUserInfo.Controls.Add(Me.mtbEditPhoneNumber)
        Me.pnlUserInfo.Controls.Add(Me.txtEditCity)
        Me.pnlUserInfo.Controls.Add(Me.txtEditAddress)
        Me.pnlUserInfo.Controls.Add(Me.txtEditCompany)
        Me.pnlUserInfo.Controls.Add(Me.txtEditTitle)
        Me.pnlUserInfo.Controls.Add(Me.txtEditLastName)
        Me.pnlUserInfo.Controls.Add(Me.txtEditFirstName)
        Me.pnlUserInfo.Controls.Add(Me.btnEditUserData)
        Me.pnlUserInfo.Controls.Add(Me.lblCityStateZip)
        Me.pnlUserInfo.Controls.Add(Me.lblAddress)
        Me.pnlUserInfo.Controls.Add(Me.Label30)
        Me.pnlUserInfo.Controls.Add(Me.lblFaxNo)
        Me.pnlUserInfo.Controls.Add(Me.lblPhoneNo)
        Me.pnlUserInfo.Controls.Add(Me.lblCoName)
        Me.pnlUserInfo.Controls.Add(Me.lblTitle)
        Me.pnlUserInfo.Controls.Add(Me.lblLName)
        Me.pnlUserInfo.Controls.Add(Me.lblFName)
        Me.pnlUserInfo.Controls.Add(Me.Label31)
        Me.pnlUserInfo.Location = New System.Drawing.Point(6, 271)
        Me.pnlUserInfo.Name = "pnlUserInfo"
        Me.pnlUserInfo.Size = New System.Drawing.Size(918, 357)
        Me.pnlUserInfo.TabIndex = 150
        Me.pnlUserInfo.Visible = False
        '
        'btnChangeEmailAddress
        '
        Me.btnChangeEmailAddress.AutoSize = True
        Me.btnChangeEmailAddress.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnChangeEmailAddress.Location = New System.Drawing.Point(513, 104)
        Me.btnChangeEmailAddress.Name = "btnChangeEmailAddress"
        Me.btnChangeEmailAddress.Size = New System.Drawing.Size(123, 23)
        Me.btnChangeEmailAddress.TabIndex = 44
        Me.btnChangeEmailAddress.Text = "Change Email Address"
        Me.btnChangeEmailAddress.UseVisualStyleBackColor = True
        Me.btnChangeEmailAddress.Visible = False
        '
        'txtEditEmail
        '
        Me.txtEditEmail.Location = New System.Drawing.Point(513, 81)
        Me.txtEditEmail.Name = "txtEditEmail"
        Me.txtEditEmail.Size = New System.Drawing.Size(208, 20)
        Me.txtEditEmail.TabIndex = 43
        '
        'lblConfirmDate
        '
        Me.lblConfirmDate.AutoSize = True
        Me.lblConfirmDate.Location = New System.Drawing.Point(4, 261)
        Me.lblConfirmDate.Name = "lblConfirmDate"
        Me.lblConfirmDate.Size = New System.Drawing.Size(0, 13)
        Me.lblConfirmDate.TabIndex = 42
        '
        'lblLastLogIn
        '
        Me.lblLastLogIn.AutoSize = True
        Me.lblLastLogIn.Location = New System.Drawing.Point(4, 283)
        Me.lblLastLogIn.Name = "lblLastLogIn"
        Me.lblLastLogIn.Size = New System.Drawing.Size(0, 13)
        Me.lblLastLogIn.TabIndex = 41
        '
        'txtEditUserPassword
        '
        Me.txtEditUserPassword.Location = New System.Drawing.Point(513, 18)
        Me.txtEditUserPassword.Name = "txtEditUserPassword"
        Me.txtEditUserPassword.Size = New System.Drawing.Size(198, 20)
        Me.txtEditUserPassword.TabIndex = 40
        Me.txtEditUserPassword.Visible = False
        '
        'btnUpdatePassword
        '
        Me.btnUpdatePassword.AutoSize = True
        Me.btnUpdatePassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdatePassword.Location = New System.Drawing.Point(513, 44)
        Me.btnUpdatePassword.Name = "btnUpdatePassword"
        Me.btnUpdatePassword.Size = New System.Drawing.Size(103, 23)
        Me.btnUpdatePassword.TabIndex = 39
        Me.btnUpdatePassword.Text = "Change Password"
        Me.btnUpdatePassword.UseVisualStyleBackColor = True
        Me.btnUpdatePassword.Visible = False
        '
        'txtWebUserID
        '
        Me.txtWebUserID.Location = New System.Drawing.Point(233, 18)
        Me.txtWebUserID.Name = "txtWebUserID"
        Me.txtWebUserID.Size = New System.Drawing.Size(33, 20)
        Me.txtWebUserID.TabIndex = 38
        Me.txtWebUserID.Visible = False
        '
        'btnSaveEditedData
        '
        Me.btnSaveEditedData.AutoSize = True
        Me.btnSaveEditedData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveEditedData.Location = New System.Drawing.Point(272, 225)
        Me.btnSaveEditedData.Name = "btnSaveEditedData"
        Me.btnSaveEditedData.Size = New System.Drawing.Size(68, 23)
        Me.btnSaveEditedData.TabIndex = 37
        Me.btnSaveEditedData.Text = "Save Data"
        Me.btnSaveEditedData.UseVisualStyleBackColor = True
        Me.btnSaveEditedData.Visible = False
        '
        'mtbEditZipCode
        '
        Me.mtbEditZipCode.Location = New System.Drawing.Point(442, 199)
        Me.mtbEditZipCode.Mask = "00000"
        Me.mtbEditZipCode.Name = "mtbEditZipCode"
        Me.mtbEditZipCode.Size = New System.Drawing.Size(38, 20)
        Me.mtbEditZipCode.TabIndex = 36
        Me.mtbEditZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbEditZipCode.Visible = False
        '
        'mtbEditState
        '
        Me.mtbEditState.Location = New System.Drawing.Point(409, 199)
        Me.mtbEditState.Mask = "&&"
        Me.mtbEditState.Name = "mtbEditState"
        Me.mtbEditState.Size = New System.Drawing.Size(27, 20)
        Me.mtbEditState.TabIndex = 35
        Me.mtbEditState.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbEditState.Visible = False
        '
        'mtbEditFaxNumber
        '
        Me.mtbEditFaxNumber.Location = New System.Drawing.Point(272, 146)
        Me.mtbEditFaxNumber.Mask = "(999) 000-0000"
        Me.mtbEditFaxNumber.Name = "mtbEditFaxNumber"
        Me.mtbEditFaxNumber.Size = New System.Drawing.Size(82, 20)
        Me.mtbEditFaxNumber.TabIndex = 34
        Me.mtbEditFaxNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbEditFaxNumber.Visible = False
        '
        'mtbEditPhoneNumber
        '
        Me.mtbEditPhoneNumber.Location = New System.Drawing.Point(272, 120)
        Me.mtbEditPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbEditPhoneNumber.Name = "mtbEditPhoneNumber"
        Me.mtbEditPhoneNumber.Size = New System.Drawing.Size(82, 20)
        Me.mtbEditPhoneNumber.TabIndex = 33
        Me.mtbEditPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbEditPhoneNumber.Visible = False
        '
        'txtEditCity
        '
        Me.txtEditCity.Location = New System.Drawing.Point(272, 199)
        Me.txtEditCity.Name = "txtEditCity"
        Me.txtEditCity.Size = New System.Drawing.Size(128, 20)
        Me.txtEditCity.TabIndex = 32
        Me.txtEditCity.Visible = False
        '
        'txtEditAddress
        '
        Me.txtEditAddress.Location = New System.Drawing.Point(272, 172)
        Me.txtEditAddress.Name = "txtEditAddress"
        Me.txtEditAddress.Size = New System.Drawing.Size(128, 20)
        Me.txtEditAddress.TabIndex = 31
        Me.txtEditAddress.Visible = False
        '
        'txtEditCompany
        '
        Me.txtEditCompany.Location = New System.Drawing.Point(272, 95)
        Me.txtEditCompany.Name = "txtEditCompany"
        Me.txtEditCompany.Size = New System.Drawing.Size(164, 20)
        Me.txtEditCompany.TabIndex = 30
        Me.txtEditCompany.Visible = False
        '
        'txtEditTitle
        '
        Me.txtEditTitle.Location = New System.Drawing.Point(272, 70)
        Me.txtEditTitle.Name = "txtEditTitle"
        Me.txtEditTitle.Size = New System.Drawing.Size(164, 20)
        Me.txtEditTitle.TabIndex = 29
        Me.txtEditTitle.Visible = False
        '
        'txtEditLastName
        '
        Me.txtEditLastName.Location = New System.Drawing.Point(272, 44)
        Me.txtEditLastName.Name = "txtEditLastName"
        Me.txtEditLastName.Size = New System.Drawing.Size(164, 20)
        Me.txtEditLastName.TabIndex = 28
        Me.txtEditLastName.Visible = False
        '
        'txtEditFirstName
        '
        Me.txtEditFirstName.Location = New System.Drawing.Point(272, 18)
        Me.txtEditFirstName.Name = "txtEditFirstName"
        Me.txtEditFirstName.Size = New System.Drawing.Size(164, 20)
        Me.txtEditFirstName.TabIndex = 27
        Me.txtEditFirstName.Visible = False
        '
        'btnEditUserData
        '
        Me.btnEditUserData.AutoSize = True
        Me.btnEditUserData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditUserData.Location = New System.Drawing.Point(7, 226)
        Me.btnEditUserData.Name = "btnEditUserData"
        Me.btnEditUserData.Size = New System.Drawing.Size(86, 23)
        Me.btnEditUserData.TabIndex = 26
        Me.btnEditUserData.Text = "Edit User Data"
        Me.btnEditUserData.UseVisualStyleBackColor = True
        '
        'lblCityStateZip
        '
        Me.lblCityStateZip.AutoSize = True
        Me.lblCityStateZip.Location = New System.Drawing.Point(4, 193)
        Me.lblCityStateZip.Name = "lblCityStateZip"
        Me.lblCityStateZip.Size = New System.Drawing.Size(0, 13)
        Me.lblCityStateZip.TabIndex = 9
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(4, 172)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(0, 13)
        Me.lblAddress.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(4, 151)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 13)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "Address:"
        '
        'lblFaxNo
        '
        Me.lblFaxNo.AutoSize = True
        Me.lblFaxNo.Location = New System.Drawing.Point(4, 130)
        Me.lblFaxNo.Name = "lblFaxNo"
        Me.lblFaxNo.Size = New System.Drawing.Size(0, 13)
        Me.lblFaxNo.TabIndex = 6
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.Location = New System.Drawing.Point(4, 109)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(0, 13)
        Me.lblPhoneNo.TabIndex = 5
        '
        'lblCoName
        '
        Me.lblCoName.AutoSize = True
        Me.lblCoName.Location = New System.Drawing.Point(4, 88)
        Me.lblCoName.Name = "lblCoName"
        Me.lblCoName.Size = New System.Drawing.Size(0, 13)
        Me.lblCoName.TabIndex = 4
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(4, 67)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(0, 13)
        Me.lblTitle.TabIndex = 3
        '
        'lblLName
        '
        Me.lblLName.AutoSize = True
        Me.lblLName.Location = New System.Drawing.Point(4, 46)
        Me.lblLName.Name = "lblLName"
        Me.lblLName.Size = New System.Drawing.Size(0, 13)
        Me.lblLName.TabIndex = 2
        '
        'lblFName
        '
        Me.lblFName.AutoSize = True
        Me.lblFName.Location = New System.Drawing.Point(4, 25)
        Me.lblFName.Name = "lblFName"
        Me.lblFName.Size = New System.Drawing.Size(0, 13)
        Me.lblFName.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(4, 3)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(125, 13)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Above User's Details"
        '
        'mtbFacilityToAdd
        '
        Me.mtbFacilityToAdd.Location = New System.Drawing.Point(176, 201)
        Me.mtbFacilityToAdd.Mask = "000-00000"
        Me.mtbFacilityToAdd.Name = "mtbFacilityToAdd"
        Me.mtbFacilityToAdd.Size = New System.Drawing.Size(64, 20)
        Me.mtbFacilityToAdd.TabIndex = 286
        Me.mtbFacilityToAdd.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'cboFacilityToDelete
        '
        Me.cboFacilityToDelete.FormattingEnabled = True
        Me.cboFacilityToDelete.Location = New System.Drawing.Point(176, 227)
        Me.cboFacilityToDelete.Name = "cboFacilityToDelete"
        Me.cboFacilityToDelete.Size = New System.Drawing.Size(215, 21)
        Me.cboFacilityToDelete.TabIndex = 284
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(6, 231)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(150, 13)
        Me.Label75.TabIndex = 283
        Me.Label75.Text = "Delete a Facility for this User:  "
        '
        'btnDeleteFacilityUser
        '
        Me.btnDeleteFacilityUser.AutoSize = True
        Me.btnDeleteFacilityUser.Location = New System.Drawing.Point(403, 226)
        Me.btnDeleteFacilityUser.Name = "btnDeleteFacilityUser"
        Me.btnDeleteFacilityUser.Size = New System.Drawing.Size(151, 23)
        Me.btnDeleteFacilityUser.TabIndex = 282
        Me.btnDeleteFacilityUser.Text = "Remove Facility for this User"
        Me.btnDeleteFacilityUser.UseVisualStyleBackColor = True
        '
        'btnUpdateUser
        '
        Me.btnUpdateUser.Location = New System.Drawing.Point(2, 167)
        Me.btnUpdateUser.Name = "btnUpdateUser"
        Me.btnUpdateUser.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdateUser.TabIndex = 277
        Me.btnUpdateUser.Text = "Save Changes"
        Me.btnUpdateUser.UseVisualStyleBackColor = True
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(6, 205)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(164, 13)
        Me.Label53.TabIndex = 276
        Me.Label53.Text = "Add a Facility to the above User: "
        '
        'btnAddFacilitytoUser
        '
        Me.btnAddFacilitytoUser.AutoSize = True
        Me.btnAddFacilitytoUser.Location = New System.Drawing.Point(246, 200)
        Me.btnAddFacilitytoUser.Name = "btnAddFacilitytoUser"
        Me.btnAddFacilitytoUser.Size = New System.Drawing.Size(77, 23)
        Me.btnAddFacilitytoUser.TabIndex = 274
        Me.btnAddFacilitytoUser.Text = "Add Facility"
        Me.btnAddFacilitytoUser.UseVisualStyleBackColor = True
        '
        'dgrFacilities
        '
        Me.dgrFacilities.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgrFacilities.BackColor = System.Drawing.Color.GhostWhite
        Me.dgrFacilities.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgrFacilities.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgrFacilities.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgrFacilities.CaptionForeColor = System.Drawing.Color.White
        Me.dgrFacilities.CaptionText = "Current Facilities for this User"
        Me.dgrFacilities.DataMember = ""
        Me.dgrFacilities.FlatMode = True
        Me.dgrFacilities.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgrFacilities.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrFacilities.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgrFacilities.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgrFacilities.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgrFacilities.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgrFacilities.LinkColor = System.Drawing.Color.Teal
        Me.dgrFacilities.Location = New System.Drawing.Point(2, 5)
        Me.dgrFacilities.Name = "dgrFacilities"
        Me.dgrFacilities.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgrFacilities.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgrFacilities.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgrFacilities.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgrFacilities.Size = New System.Drawing.Size(709, 157)
        Me.dgrFacilities.TabIndex = 273
        '
        'pnlUserEmail
        '
        Me.pnlUserEmail.Controls.Add(Me.lblViewEmailData)
        Me.pnlUserEmail.Controls.Add(Me.Label39)
        Me.pnlUserEmail.Controls.Add(Me.txtWebUserEmail)
        Me.pnlUserEmail.Controls.Add(Me.cboUserEmail)
        Me.pnlUserEmail.Controls.Add(Me.btnActivateEmail)
        Me.pnlUserEmail.Controls.Add(Me.lblViewFacility)
        Me.pnlUserEmail.Controls.Add(Me.Label52)
        Me.pnlUserEmail.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserEmail.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserEmail.Name = "pnlUserEmail"
        Me.pnlUserEmail.Size = New System.Drawing.Size(930, 49)
        Me.pnlUserEmail.TabIndex = 147
        '
        'lblViewEmailData
        '
        Me.lblViewEmailData.AutoSize = True
        Me.lblViewEmailData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewEmailData.Location = New System.Drawing.Point(309, 13)
        Me.lblViewEmailData.Name = "lblViewEmailData"
        Me.lblViewEmailData.Size = New System.Drawing.Size(56, 13)
        Me.lblViewEmailData.TabIndex = 289
        Me.lblViewEmailData.TabStop = True
        Me.lblViewEmailData.Text = "View Data"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(3, 13)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(101, 13)
        Me.Label39.TabIndex = 288
        Me.Label39.Text = "User Email Address:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtWebUserEmail
        '
        Me.txtWebUserEmail.Location = New System.Drawing.Point(110, 9)
        Me.txtWebUserEmail.Name = "txtWebUserEmail"
        Me.txtWebUserEmail.Size = New System.Drawing.Size(196, 20)
        Me.txtWebUserEmail.TabIndex = 287
        '
        'cboUserEmail
        '
        Me.cboUserEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUserEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUserEmail.Location = New System.Drawing.Point(618, 9)
        Me.cboUserEmail.Name = "cboUserEmail"
        Me.cboUserEmail.Size = New System.Drawing.Size(244, 21)
        Me.cboUserEmail.TabIndex = 1
        '
        'btnActivateEmail
        '
        Me.btnActivateEmail.AutoSize = True
        Me.btnActivateEmail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnActivateEmail.BackColor = System.Drawing.Color.YellowGreen
        Me.btnActivateEmail.Location = New System.Drawing.Point(371, 8)
        Me.btnActivateEmail.Name = "btnActivateEmail"
        Me.btnActivateEmail.Size = New System.Drawing.Size(134, 23)
        Me.btnActivateEmail.TabIndex = 174
        Me.btnActivateEmail.Text = "Refresh Email Addresses"
        Me.btnActivateEmail.UseVisualStyleBackColor = False
        '
        'lblViewFacility
        '
        Me.lblViewFacility.AutoSize = True
        Me.lblViewFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewFacility.Location = New System.Drawing.Point(868, 13)
        Me.lblViewFacility.Name = "lblViewFacility"
        Me.lblViewFacility.Size = New System.Drawing.Size(56, 13)
        Me.lblViewFacility.TabIndex = 143
        Me.lblViewFacility.TabStop = True
        Me.lblViewFacility.Text = "View Data"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(511, 13)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(101, 13)
        Me.Label52.TabIndex = 106
        Me.Label52.Text = "User Email Address:"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPActivate
        '
        Me.TPActivate.Controls.Add(Me.btnActivateUser)
        Me.TPActivate.Controls.Add(Me.Label54)
        Me.TPActivate.Controls.Add(Me.txtEmailAddress)
        Me.TPActivate.Location = New System.Drawing.Point(4, 22)
        Me.TPActivate.Name = "TPActivate"
        Me.TPActivate.Size = New System.Drawing.Size(930, 680)
        Me.TPActivate.TabIndex = 3
        Me.TPActivate.Text = "Activate User Account"
        Me.TPActivate.UseVisualStyleBackColor = True
        '
        'btnActivateUser
        '
        Me.btnActivateUser.AutoSize = True
        Me.btnActivateUser.Location = New System.Drawing.Point(361, 11)
        Me.btnActivateUser.Name = "btnActivateUser"
        Me.btnActivateUser.Size = New System.Drawing.Size(61, 23)
        Me.btnActivateUser.TabIndex = 2
        Me.btnActivateUser.Text = "Activate"
        Me.btnActivateUser.UseVisualStyleBackColor = True
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(4, 14)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(101, 13)
        Me.Label54.TabIndex = 1
        Me.Label54.Text = "User Email Address:"
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(111, 14)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(244, 20)
        Me.txtEmailAddress.TabIndex = 0
        '
        'TPFeeFacility
        '
        Me.TPFeeFacility.Controls.Add(Me.mtbyear)
        Me.TPFeeFacility.Controls.Add(Me.mtbFeeAirsNumber)
        Me.TPFeeFacility.Controls.Add(Me.btnRemoveFacility)
        Me.TPFeeFacility.Controls.Add(Me.Label74)
        Me.TPFeeFacility.Controls.Add(Me.Label73)
        Me.TPFeeFacility.Controls.Add(Me.Label50)
        Me.TPFeeFacility.Controls.Add(Me.Label72)
        Me.TPFeeFacility.Controls.Add(Me.btnAddFacility)
        Me.TPFeeFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeFacility.Name = "TPFeeFacility"
        Me.TPFeeFacility.Size = New System.Drawing.Size(930, 680)
        Me.TPFeeFacility.TabIndex = 4
        Me.TPFeeFacility.Text = "Add/Remove Fee Facility"
        Me.TPFeeFacility.UseVisualStyleBackColor = True
        '
        'mtbyear
        '
        Me.mtbyear.Location = New System.Drawing.Point(260, 29)
        Me.mtbyear.Mask = "0000"
        Me.mtbyear.Name = "mtbyear"
        Me.mtbyear.Size = New System.Drawing.Size(39, 20)
        Me.mtbyear.TabIndex = 9
        '
        'mtbFeeAirsNumber
        '
        Me.mtbFeeAirsNumber.Location = New System.Drawing.Point(88, 29)
        Me.mtbFeeAirsNumber.Mask = "000-00000"
        Me.mtbFeeAirsNumber.Name = "mtbFeeAirsNumber"
        Me.mtbFeeAirsNumber.Size = New System.Drawing.Size(76, 20)
        Me.mtbFeeAirsNumber.TabIndex = 8
        Me.mtbFeeAirsNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnRemoveFacility
        '
        Me.btnRemoveFacility.AutoSize = True
        Me.btnRemoveFacility.Location = New System.Drawing.Point(447, 28)
        Me.btnRemoveFacility.Name = "btnRemoveFacility"
        Me.btnRemoveFacility.Size = New System.Drawing.Size(155, 23)
        Me.btnRemoveFacility.TabIndex = 7
        Me.btnRemoveFacility.Text = "Remove Facility from Fee List"
        Me.btnRemoveFacility.UseVisualStyleBackColor = True
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(222, 33)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(35, 13)
        Me.Label74.TabIndex = 6
        Me.Label74.Text = "Year: "
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(85, 8)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(79, 13)
        Me.Label73.TabIndex = 4
        Me.Label73.Text = "Ex: 001-00001 "
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(257, 11)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(49, 13)
        Me.Label50.TabIndex = 3
        Me.Label50.Text = "Ex: 2006"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(4, 33)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(78, 13)
        Me.Label72.TabIndex = 2
        Me.Label72.Text = "AIRS Number: "
        '
        'btnAddFacility
        '
        Me.btnAddFacility.AutoSize = True
        Me.btnAddFacility.Location = New System.Drawing.Point(318, 28)
        Me.btnAddFacility.Name = "btnAddFacility"
        Me.btnAddFacility.Size = New System.Drawing.Size(123, 23)
        Me.btnAddFacility.TabIndex = 1
        Me.btnAddFacility.Text = "Add Facility to Fee List"
        Me.btnAddFacility.UseVisualStyleBackColor = True
        '
        'TPGenerateMailOut
        '
        Me.TPGenerateMailOut.Controls.Add(Me.SCGenerateMailOut)
        Me.TPGenerateMailOut.Location = New System.Drawing.Point(4, 22)
        Me.TPGenerateMailOut.Name = "TPGenerateMailOut"
        Me.TPGenerateMailOut.Padding = New System.Windows.Forms.Padding(3)
        Me.TPGenerateMailOut.Size = New System.Drawing.Size(938, 706)
        Me.TPGenerateMailOut.TabIndex = 0
        Me.TPGenerateMailOut.Text = "Generate Mailout"
        Me.TPGenerateMailOut.UseVisualStyleBackColor = True
        '
        'SCGenerateMailOut
        '
        Me.SCGenerateMailOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCGenerateMailOut.Location = New System.Drawing.Point(3, 3)
        Me.SCGenerateMailOut.Name = "SCGenerateMailOut"
        '
        'SCGenerateMailOut.Panel1
        '
        Me.SCGenerateMailOut.Panel1.AutoScroll = True
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.lblviewsumarrymailoutinfo)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.btnGenerateFeeMailOut)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.btnRefreshAirsNo)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.cboMailoutYear)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label2)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label15)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.cboPart70)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFacilityZip)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.cboNSPS)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.btnDeleteFeeMailOut)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.cboOperation)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFacilityState)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.cboClass)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.lblviewselectedyearMailOutlist)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label24)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFacilityCity)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label22)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFacilityAddress)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label23)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFacilityStreet)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtContactAddress1)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFirstName)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label3)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label14)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.btnFeeDelete)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtAirsNo)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.btnFeeSave)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label13)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtMailoutEmail)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtFacilityName)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label4)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label12)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtContactZip)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label11)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label5)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label10)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtContactState)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtLastName)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label6)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label9)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtContactCity)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtCompanyName)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label7)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.Label8)
        Me.SCGenerateMailOut.Panel1.Controls.Add(Me.txtShutdowndate)
        '
        'SCGenerateMailOut.Panel2
        '
        Me.SCGenerateMailOut.Panel2.Controls.Add(Me.dgvFeeDataCount)
        Me.SCGenerateMailOut.Panel2.Controls.Add(Me.gpViewdata)
        Me.SCGenerateMailOut.Size = New System.Drawing.Size(932, 700)
        Me.SCGenerateMailOut.SplitterDistance = 599
        Me.SCGenerateMailOut.TabIndex = 382
        '
        'lblviewsumarrymailoutinfo
        '
        Me.lblviewsumarrymailoutinfo.AutoSize = True
        Me.lblviewsumarrymailoutinfo.Location = New System.Drawing.Point(262, 94)
        Me.lblviewsumarrymailoutinfo.Name = "lblviewsumarrymailoutinfo"
        Me.lblviewsumarrymailoutinfo.Size = New System.Drawing.Size(219, 13)
        Me.lblviewsumarrymailoutinfo.TabIndex = 381
        Me.lblviewsumarrymailoutinfo.TabStop = True
        Me.lblviewsumarrymailoutinfo.Text = "Veiw Summary Mailout Info for Selected Year"
        '
        'btnGenerateFeeMailOut
        '
        Me.btnGenerateFeeMailOut.Location = New System.Drawing.Point(258, 17)
        Me.btnGenerateFeeMailOut.Name = "btnGenerateFeeMailOut"
        Me.btnGenerateFeeMailOut.Size = New System.Drawing.Size(160, 23)
        Me.btnGenerateFeeMailOut.TabIndex = 338
        Me.btnGenerateFeeMailOut.Text = "Generate Mailout List by Year"
        Me.btnGenerateFeeMailOut.UseVisualStyleBackColor = True
        Me.btnGenerateFeeMailOut.Visible = False
        '
        'btnRefreshAirsNo
        '
        Me.btnRefreshAirsNo.AutoSize = True
        Me.btnRefreshAirsNo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshAirsNo.Image = CType(resources.GetObject("btnRefreshAirsNo.Image"), System.Drawing.Image)
        Me.btnRefreshAirsNo.Location = New System.Drawing.Point(236, 128)
        Me.btnRefreshAirsNo.Name = "btnRefreshAirsNo"
        Me.btnRefreshAirsNo.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshAirsNo.TabIndex = 380
        Me.btnRefreshAirsNo.UseVisualStyleBackColor = True
        '
        'cboMailoutYear
        '
        Me.cboMailoutYear.FormattingEnabled = True
        Me.cboMailoutYear.Location = New System.Drawing.Point(111, 33)
        Me.cboMailoutYear.Name = "cboMailoutYear"
        Me.cboMailoutYear.Size = New System.Drawing.Size(120, 21)
        Me.cboMailoutYear.TabIndex = 337
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(109, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 378
        Me.Label2.Text = "Ex: 041300100001"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(108, 17)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(138, 13)
        Me.Label15.TabIndex = 379
        Me.Label15.Text = "Choose the Year for Mailout"
        '
        'cboPart70
        '
        Me.cboPart70.Enabled = False
        Me.cboPart70.FormattingEnabled = True
        Me.cboPart70.Location = New System.Drawing.Point(111, 311)
        Me.cboPart70.Name = "cboPart70"
        Me.cboPart70.Size = New System.Drawing.Size(121, 21)
        Me.cboPart70.TabIndex = 347
        '
        'txtFacilityZip
        '
        Me.txtFacilityZip.Location = New System.Drawing.Point(12, 506)
        Me.txtFacilityZip.Name = "txtFacilityZip"
        Me.txtFacilityZip.Size = New System.Drawing.Size(42, 20)
        Me.txtFacilityZip.TabIndex = 377
        Me.txtFacilityZip.Visible = False
        '
        'cboNSPS
        '
        Me.cboNSPS.Enabled = False
        Me.cboNSPS.FormattingEnabled = True
        Me.cboNSPS.Location = New System.Drawing.Point(111, 285)
        Me.cboNSPS.Name = "cboNSPS"
        Me.cboNSPS.Size = New System.Drawing.Size(121, 21)
        Me.cboNSPS.TabIndex = 346
        '
        'btnDeleteFeeMailOut
        '
        Me.btnDeleteFeeMailOut.Location = New System.Drawing.Point(258, 46)
        Me.btnDeleteFeeMailOut.Name = "btnDeleteFeeMailOut"
        Me.btnDeleteFeeMailOut.Size = New System.Drawing.Size(160, 23)
        Me.btnDeleteFeeMailOut.TabIndex = 339
        Me.btnDeleteFeeMailOut.Text = "Delete Mailout List by Year"
        Me.btnDeleteFeeMailOut.UseVisualStyleBackColor = True
        Me.btnDeleteFeeMailOut.Visible = False
        '
        'cboOperation
        '
        Me.cboOperation.Enabled = False
        Me.cboOperation.FormattingEnabled = True
        Me.cboOperation.Location = New System.Drawing.Point(112, 259)
        Me.cboOperation.Name = "cboOperation"
        Me.cboOperation.Size = New System.Drawing.Size(121, 21)
        Me.cboOperation.TabIndex = 345
        '
        'txtFacilityState
        '
        Me.txtFacilityState.Location = New System.Drawing.Point(12, 480)
        Me.txtFacilityState.Name = "txtFacilityState"
        Me.txtFacilityState.Size = New System.Drawing.Size(42, 20)
        Me.txtFacilityState.TabIndex = 376
        Me.txtFacilityState.Visible = False
        '
        'cboClass
        '
        Me.cboClass.Enabled = False
        Me.cboClass.FormattingEnabled = True
        Me.cboClass.Location = New System.Drawing.Point(112, 233)
        Me.cboClass.Name = "cboClass"
        Me.cboClass.Size = New System.Drawing.Size(121, 21)
        Me.cboClass.TabIndex = 344
        '
        'lblviewselectedyearMailOutlist
        '
        Me.lblviewselectedyearMailOutlist.AutoSize = True
        Me.lblviewselectedyearMailOutlist.Location = New System.Drawing.Point(262, 72)
        Me.lblviewselectedyearMailOutlist.Name = "lblviewselectedyearMailOutlist"
        Me.lblviewselectedyearMailOutlist.Size = New System.Drawing.Size(215, 13)
        Me.lblviewselectedyearMailOutlist.TabIndex = 340
        Me.lblviewselectedyearMailOutlist.TabStop = True
        Me.lblviewselectedyearMailOutlist.Text = "Veiw Detailed Mailout Info for Selected Year"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(9, 315)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 13)
        Me.Label24.TabIndex = 372
        Me.Label24.Text = "Part 70 Status:"
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.Location = New System.Drawing.Point(12, 454)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.Size = New System.Drawing.Size(42, 20)
        Me.txtFacilityCity.TabIndex = 375
        Me.txtFacilityCity.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(9, 263)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(97, 13)
        Me.Label22.TabIndex = 371
        Me.Label22.Text = "Operational Status:"
        '
        'txtFacilityAddress
        '
        Me.txtFacilityAddress.Location = New System.Drawing.Point(12, 402)
        Me.txtFacilityAddress.Name = "txtFacilityAddress"
        Me.txtFacilityAddress.Size = New System.Drawing.Size(42, 20)
        Me.txtFacilityAddress.TabIndex = 373
        Me.txtFacilityAddress.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(9, 289)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(72, 13)
        Me.Label23.TabIndex = 370
        Me.Label23.Text = "NSPS Status:"
        '
        'txtFacilityStreet
        '
        Me.txtFacilityStreet.Location = New System.Drawing.Point(12, 428)
        Me.txtFacilityStreet.Name = "txtFacilityStreet"
        Me.txtFacilityStreet.Size = New System.Drawing.Size(42, 20)
        Me.txtFacilityStreet.TabIndex = 374
        Me.txtFacilityStreet.Visible = False
        '
        'txtContactAddress1
        '
        Me.txtContactAddress1.Location = New System.Drawing.Point(401, 207)
        Me.txtContactAddress1.Name = "txtContactAddress1"
        Me.txtContactAddress1.Size = New System.Drawing.Size(125, 20)
        Me.txtContactAddress1.TabIndex = 351
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(401, 129)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(125, 20)
        Me.txtFirstName.TabIndex = 348
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(264, 211)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 13)
        Me.Label3.TabIndex = 364
        Me.Label3.Text = "Contact Company Address: "
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 133)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(50, 13)
        Me.Label14.TabIndex = 358
        Me.Label14.Text = "Airs No.: "
        '
        'btnFeeDelete
        '
        Me.btnFeeDelete.Location = New System.Drawing.Point(290, 355)
        Me.btnFeeDelete.Name = "btnFeeDelete"
        Me.btnFeeDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnFeeDelete.TabIndex = 357
        Me.btnFeeDelete.Text = "Delete"
        Me.btnFeeDelete.UseVisualStyleBackColor = True
        '
        'txtAirsNo
        '
        Me.txtAirsNo.Location = New System.Drawing.Point(110, 129)
        Me.txtAirsNo.Name = "txtAirsNo"
        Me.txtAirsNo.Size = New System.Drawing.Size(120, 20)
        Me.txtAirsNo.TabIndex = 341
        Me.txtAirsNo.Text = "0413"
        '
        'btnFeeSave
        '
        Me.btnFeeSave.Location = New System.Drawing.Point(167, 355)
        Me.btnFeeSave.Name = "btnFeeSave"
        Me.btnFeeSave.Size = New System.Drawing.Size(75, 23)
        Me.btnFeeSave.TabIndex = 356
        Me.btnFeeSave.Text = "Save"
        Me.btnFeeSave.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 185)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 13)
        Me.Label13.TabIndex = 359
        Me.Label13.Text = "Facility Name: "
        '
        'txtMailoutEmail
        '
        Me.txtMailoutEmail.Location = New System.Drawing.Point(401, 311)
        Me.txtMailoutEmail.Name = "txtMailoutEmail"
        Me.txtMailoutEmail.Size = New System.Drawing.Size(125, 20)
        Me.txtMailoutEmail.TabIndex = 355
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(111, 181)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(121, 20)
        Me.txtFacilityName.TabIndex = 342
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(264, 315)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 13)
        Me.Label4.TabIndex = 369
        Me.Label4.Text = "Contact Email Address:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 237)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 13)
        Me.Label12.TabIndex = 360
        Me.Label12.Text = "Source Classification:"
        '
        'txtContactZip
        '
        Me.txtContactZip.Location = New System.Drawing.Point(401, 285)
        Me.txtContactZip.Name = "txtContactZip"
        Me.txtContactZip.Size = New System.Drawing.Size(125, 20)
        Me.txtContactZip.TabIndex = 354
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(264, 133)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 13)
        Me.Label11.TabIndex = 361
        Me.Label11.Text = "Contact First Name:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(264, 289)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(140, 13)
        Me.Label5.TabIndex = 368
        Me.Label5.Text = "Contact Company ZipCode: "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(264, 159)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 13)
        Me.Label10.TabIndex = 362
        Me.Label10.Text = "Contact Last Name:"
        '
        'txtContactState
        '
        Me.txtContactState.Location = New System.Drawing.Point(401, 259)
        Me.txtContactState.Name = "txtContactState"
        Me.txtContactState.Size = New System.Drawing.Size(125, 20)
        Me.txtContactState.TabIndex = 353
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(401, 155)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(125, 20)
        Me.txtLastName.TabIndex = 349
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(264, 263)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 13)
        Me.Label6.TabIndex = 367
        Me.Label6.Text = "Contact Company State: "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(264, 185)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 13)
        Me.Label9.TabIndex = 363
        Me.Label9.Text = "Contact Company Name: "
        '
        'txtContactCity
        '
        Me.txtContactCity.Location = New System.Drawing.Point(401, 233)
        Me.txtContactCity.Name = "txtContactCity"
        Me.txtContactCity.Size = New System.Drawing.Size(125, 20)
        Me.txtContactCity.TabIndex = 352
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(401, 181)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(126, 20)
        Me.txtCompanyName.TabIndex = 350
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(264, 237)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 13)
        Me.Label7.TabIndex = 366
        Me.Label7.Text = "Contact Company City: "
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 211)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 13)
        Me.Label8.TabIndex = 365
        Me.Label8.Text = "Shut Down Date:"
        '
        'txtShutdowndate
        '
        Me.txtShutdowndate.Location = New System.Drawing.Point(111, 207)
        Me.txtShutdowndate.Name = "txtShutdowndate"
        Me.txtShutdowndate.ReadOnly = True
        Me.txtShutdowndate.Size = New System.Drawing.Size(121, 20)
        Me.txtShutdowndate.TabIndex = 343
        '
        'dgvFeeDataCount
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFeeDataCount.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvFeeDataCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFeeDataCount.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvFeeDataCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFeeDataCount.Location = New System.Drawing.Point(0, 45)
        Me.dgvFeeDataCount.Name = "dgvFeeDataCount"
        Me.dgvFeeDataCount.ReadOnly = True
        Me.dgvFeeDataCount.Size = New System.Drawing.Size(329, 655)
        Me.dgvFeeDataCount.TabIndex = 2
        '
        'gpViewdata
        '
        Me.gpViewdata.Controls.Add(Me.BtnExportExcel)
        Me.gpViewdata.Controls.Add(Me.txtRecordNumber)
        Me.gpViewdata.Dock = System.Windows.Forms.DockStyle.Top
        Me.gpViewdata.Location = New System.Drawing.Point(0, 0)
        Me.gpViewdata.Name = "gpViewdata"
        Me.gpViewdata.Size = New System.Drawing.Size(329, 45)
        Me.gpViewdata.TabIndex = 381
        Me.gpViewdata.TabStop = False
        Me.gpViewdata.Text = "View Data"
        '
        'BtnExportExcel
        '
        Me.BtnExportExcel.Location = New System.Drawing.Point(130, 16)
        Me.BtnExportExcel.Name = "BtnExportExcel"
        Me.BtnExportExcel.Size = New System.Drawing.Size(95, 23)
        Me.BtnExportExcel.TabIndex = 1
        Me.BtnExportExcel.Text = "Export To Excel"
        Me.BtnExportExcel.UseVisualStyleBackColor = True
        '
        'txtRecordNumber
        '
        Me.txtRecordNumber.Location = New System.Drawing.Point(6, 19)
        Me.txtRecordNumber.Name = "txtRecordNumber"
        Me.txtRecordNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtRecordNumber.TabIndex = 0
        '
        'TPEnrollment
        '
        Me.TPEnrollment.Controls.Add(Me.lblEnrollYear)
        Me.TPEnrollment.Controls.Add(Me.Label1)
        Me.TPEnrollment.Controls.Add(Me.btnDeEnroll)
        Me.TPEnrollment.Controls.Add(Me.btnEnroll)
        Me.TPEnrollment.Location = New System.Drawing.Point(4, 22)
        Me.TPEnrollment.Name = "TPEnrollment"
        Me.TPEnrollment.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEnrollment.Size = New System.Drawing.Size(938, 706)
        Me.TPEnrollment.TabIndex = 1
        Me.TPEnrollment.Text = "Enrollment"
        Me.TPEnrollment.UseVisualStyleBackColor = True
        '
        'lblEnrollYear
        '
        Me.lblEnrollYear.AutoSize = True
        Me.lblEnrollYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnrollYear.Location = New System.Drawing.Point(199, 19)
        Me.lblEnrollYear.Name = "lblEnrollYear"
        Me.lblEnrollYear.Size = New System.Drawing.Size(37, 16)
        Me.lblEnrollYear.TabIndex = 8
        Me.lblEnrollYear.Text = "Year"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Mailout Enrollment List for"
        '
        'btnDeEnroll
        '
        Me.btnDeEnroll.Location = New System.Drawing.Point(28, 136)
        Me.btnDeEnroll.Name = "btnDeEnroll"
        Me.btnDeEnroll.Size = New System.Drawing.Size(165, 23)
        Me.btnDeEnroll.TabIndex = 6
        Me.btnDeEnroll.Text = "Remove Mailout Enrollment"
        Me.btnDeEnroll.UseVisualStyleBackColor = True
        '
        'btnEnroll
        '
        Me.btnEnroll.Location = New System.Drawing.Point(28, 90)
        Me.btnEnroll.Name = "btnEnroll"
        Me.btnEnroll.Size = New System.Drawing.Size(165, 23)
        Me.btnEnroll.TabIndex = 5
        Me.btnEnroll.Text = "Enroll Mailout List"
        Me.btnEnroll.UseVisualStyleBackColor = True
        '
        'TPFeeRates
        '
        Me.TPFeeRates.Controls.Add(Me.TabControl2)
        Me.TPFeeRates.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeRates.Name = "TPFeeRates"
        Me.TPFeeRates.Size = New System.Drawing.Size(938, 706)
        Me.TPFeeRates.TabIndex = 4
        Me.TPFeeRates.Text = "Annual Fee Rates"
        Me.TPFeeRates.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Controls.Add(Me.TabPage2)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(938, 706)
        Me.TabControl2.TabIndex = 401
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(930, 680)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pollutant Fee Rates"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label114)
        Me.Panel6.Controls.Add(Me.dtpFeeDueDate)
        Me.Panel6.Controls.Add(Me.Label113)
        Me.Panel6.Controls.Add(Me.Label51)
        Me.Panel6.Controls.Add(Me.btnsaveRate)
        Me.Panel6.Controls.Add(Me.dtpduedate)
        Me.Panel6.Controls.Add(Me.cboFeeRateYear)
        Me.Panel6.Controls.Add(Me.lblViewFeeRate)
        Me.Panel6.Controls.Add(Me.txtAnnualSMFee)
        Me.Panel6.Controls.Add(Me.Label60)
        Me.Panel6.Controls.Add(Me.txtAnnualNSPSFee)
        Me.Panel6.Controls.Add(Me.Label59)
        Me.Panel6.Controls.Add(Me.Label58)
        Me.Panel6.Controls.Add(Me.txtTitleVfee)
        Me.Panel6.Controls.Add(Me.Label57)
        Me.Panel6.Controls.Add(Me.txtperTonRate)
        Me.Panel6.Controls.Add(Me.Label55)
        Me.Panel6.Controls.Add(Me.txtAdminFeePercent)
        Me.Panel6.Controls.Add(Me.Label248)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(924, 674)
        Me.Panel6.TabIndex = 399
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.Location = New System.Drawing.Point(341, 207)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(15, 13)
        Me.Label114.TabIndex = 401
        Me.Label114.Text = "%"
        '
        'dtpFeeDueDate
        '
        Me.dtpFeeDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeeDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeeDueDate.Location = New System.Drawing.Point(199, 175)
        Me.dtpFeeDueDate.Name = "dtpFeeDueDate"
        Me.dtpFeeDueDate.ShowCheckBox = True
        Me.dtpFeeDueDate.Size = New System.Drawing.Size(136, 20)
        Me.dtpFeeDueDate.TabIndex = 400
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.Location = New System.Drawing.Point(35, 179)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(77, 13)
        Me.Label113.TabIndex = 399
        Me.Label113.Text = "Fee Due Date:"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(35, 14)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(122, 13)
        Me.Label51.TabIndex = 394
        Me.Label51.Text = "Choose fee mailout Year"
        '
        'btnsaveRate
        '
        Me.btnsaveRate.Location = New System.Drawing.Point(37, 270)
        Me.btnsaveRate.Name = "btnsaveRate"
        Me.btnsaveRate.Size = New System.Drawing.Size(75, 23)
        Me.btnsaveRate.TabIndex = 397
        Me.btnsaveRate.Text = "Save"
        Me.btnsaveRate.UseVisualStyleBackColor = True
        '
        'dtpduedate
        '
        Me.dtpduedate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpduedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpduedate.Location = New System.Drawing.Point(199, 233)
        Me.dtpduedate.Name = "dtpduedate"
        Me.dtpduedate.ShowCheckBox = True
        Me.dtpduedate.Size = New System.Drawing.Size(136, 20)
        Me.dtpduedate.TabIndex = 398
        '
        'cboFeeRateYear
        '
        Me.cboFeeRateYear.FormattingEnabled = True
        Me.cboFeeRateYear.Location = New System.Drawing.Point(35, 33)
        Me.cboFeeRateYear.Name = "cboFeeRateYear"
        Me.cboFeeRateYear.Size = New System.Drawing.Size(121, 21)
        Me.cboFeeRateYear.TabIndex = 0
        '
        'lblViewFeeRate
        '
        Me.lblViewFeeRate.AutoSize = True
        Me.lblViewFeeRate.Location = New System.Drawing.Point(168, 41)
        Me.lblViewFeeRate.Name = "lblViewFeeRate"
        Me.lblViewFeeRate.Size = New System.Drawing.Size(167, 13)
        Me.lblViewFeeRate.TabIndex = 1
        Me.lblViewFeeRate.TabStop = True
        Me.lblViewFeeRate.Text = "View Fee Rates for Selected Year"
        '
        'txtAnnualSMFee
        '
        Me.txtAnnualSMFee.Location = New System.Drawing.Point(199, 92)
        Me.txtAnnualSMFee.Name = "txtAnnualSMFee"
        Me.txtAnnualSMFee.Size = New System.Drawing.Size(136, 20)
        Me.txtAnnualSMFee.TabIndex = 396
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(35, 211)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(103, 13)
        Me.Label60.TabIndex = 382
        Me.Label60.Text = "Admin Fee Percent: "
        '
        'txtAnnualNSPSFee
        '
        Me.txtAnnualNSPSFee.Location = New System.Drawing.Point(199, 118)
        Me.txtAnnualNSPSFee.Name = "txtAnnualNSPSFee"
        Me.txtAnnualNSPSFee.Size = New System.Drawing.Size(136, 20)
        Me.txtAnnualNSPSFee.TabIndex = 395
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(35, 125)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(63, 13)
        Me.Label59.TabIndex = 380
        Me.Label59.Text = "NSPS Fee: "
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(35, 237)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(89, 13)
        Me.Label58.TabIndex = 383
        Me.Label58.Text = "Admin Fee Date: "
        '
        'txtTitleVfee
        '
        Me.txtTitleVfee.Location = New System.Drawing.Point(199, 66)
        Me.txtTitleVfee.Name = "txtTitleVfee"
        Me.txtTitleVfee.Size = New System.Drawing.Size(136, 20)
        Me.txtTitleVfee.TabIndex = 393
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(35, 99)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(47, 13)
        Me.Label57.TabIndex = 379
        Me.Label57.Text = "SM Fee:"
        '
        'txtperTonRate
        '
        Me.txtperTonRate.Location = New System.Drawing.Point(199, 144)
        Me.txtperTonRate.Name = "txtperTonRate"
        Me.txtperTonRate.Size = New System.Drawing.Size(136, 20)
        Me.txtperTonRate.TabIndex = 390
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(35, 73)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(122, 13)
        Me.Label55.TabIndex = 378
        Me.Label55.Text = "Title V Fee (Part70 Fee):"
        '
        'txtAdminFeePercent
        '
        Me.txtAdminFeePercent.Location = New System.Drawing.Point(199, 204)
        Me.txtAdminFeePercent.Name = "txtAdminFeePercent"
        Me.txtAdminFeePercent.Size = New System.Drawing.Size(136, 20)
        Me.txtAdminFeePercent.TabIndex = 389
        '
        'Label248
        '
        Me.Label248.AutoSize = True
        Me.Label248.Location = New System.Drawing.Point(35, 151)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(82, 13)
        Me.Label248.TabIndex = 381
        Me.Label248.Text = "Per Ton Rates: "
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pnlNSPSExemptions)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(930, 680)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "NSPS Exemption Tool"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnlNSPSExemptions
        '
        Me.pnlNSPSExemptions.Controls.Add(Me.btnLoadNSPSTool)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label109)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnSelectForm)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUnselectForm)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnSelectAllForms)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUnselectAllForms)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUpdateNSPSbyYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnAddExemptionToYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.cboNSPSExemptions)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnViewNSPSExemptionsByYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.dgvNSPSExemptionsByYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label108)
        Me.pnlNSPSExemptions.Controls.Add(Me.cboNSPSExemptionYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnDeleteNSPSExemption)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label107)
        Me.pnlNSPSExemptions.Controls.Add(Me.txtDeleteNSPSExemptions)
        Me.pnlNSPSExemptions.Controls.Add(Me.txtNSPSExemption)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label101)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnAddNSPSExemption)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label100)
        Me.pnlNSPSExemptions.Controls.Add(Me.dgvNSPSExemptions)
        Me.pnlNSPSExemptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNSPSExemptions.Location = New System.Drawing.Point(3, 3)
        Me.pnlNSPSExemptions.Name = "pnlNSPSExemptions"
        Me.pnlNSPSExemptions.Size = New System.Drawing.Size(924, 674)
        Me.pnlNSPSExemptions.TabIndex = 400
        '
        'btnLoadNSPSTool
        '
        Me.btnLoadNSPSTool.AutoSize = True
        Me.btnLoadNSPSTool.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadNSPSTool.Location = New System.Drawing.Point(3, 4)
        Me.btnLoadNSPSTool.Name = "btnLoadNSPSTool"
        Me.btnLoadNSPSTool.Size = New System.Drawing.Size(149, 23)
        Me.btnLoadNSPSTool.TabIndex = 399
        Me.btnLoadNSPSTool.Text = "Load NSPS Exemption Tool"
        Me.btnLoadNSPSTool.UseVisualStyleBackColor = True
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Location = New System.Drawing.Point(420, 46)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(136, 13)
        Me.Label109.TabIndex = 413
        Me.Label109.Text = "NSPS Exemptions By Year:"
        '
        'btnSelectForm
        '
        Me.btnSelectForm.AutoSize = True
        Me.btnSelectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectForm.Image = CType(resources.GetObject("btnSelectForm.Image"), System.Drawing.Image)
        Me.btnSelectForm.Location = New System.Drawing.Point(379, 112)
        Me.btnSelectForm.Name = "btnSelectForm"
        Me.btnSelectForm.Size = New System.Drawing.Size(30, 28)
        Me.btnSelectForm.TabIndex = 408
        Me.btnSelectForm.UseVisualStyleBackColor = True
        '
        'btnUnselectForm
        '
        Me.btnUnselectForm.AutoSize = True
        Me.btnUnselectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectForm.Image = CType(resources.GetObject("btnUnselectForm.Image"), System.Drawing.Image)
        Me.btnUnselectForm.Location = New System.Drawing.Point(379, 194)
        Me.btnUnselectForm.Name = "btnUnselectForm"
        Me.btnUnselectForm.Size = New System.Drawing.Size(30, 28)
        Me.btnUnselectForm.TabIndex = 410
        Me.btnUnselectForm.UseVisualStyleBackColor = True
        '
        'btnSelectAllForms
        '
        Me.btnSelectAllForms.AutoSize = True
        Me.btnSelectAllForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectAllForms.Image = CType(resources.GetObject("btnSelectAllForms.Image"), System.Drawing.Image)
        Me.btnSelectAllForms.Location = New System.Drawing.Point(379, 153)
        Me.btnSelectAllForms.Name = "btnSelectAllForms"
        Me.btnSelectAllForms.Size = New System.Drawing.Size(30, 28)
        Me.btnSelectAllForms.TabIndex = 409
        Me.btnSelectAllForms.UseVisualStyleBackColor = True
        '
        'btnUnselectAllForms
        '
        Me.btnUnselectAllForms.AutoSize = True
        Me.btnUnselectAllForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectAllForms.Image = CType(resources.GetObject("btnUnselectAllForms.Image"), System.Drawing.Image)
        Me.btnUnselectAllForms.Location = New System.Drawing.Point(379, 235)
        Me.btnUnselectAllForms.Name = "btnUnselectAllForms"
        Me.btnUnselectAllForms.Size = New System.Drawing.Size(30, 28)
        Me.btnUnselectAllForms.TabIndex = 411
        Me.btnUnselectAllForms.UseVisualStyleBackColor = True
        Me.btnUnselectAllForms.Visible = False
        '
        'btnUpdateNSPSbyYear
        '
        Me.btnUpdateNSPSbyYear.AutoSize = True
        Me.btnUpdateNSPSbyYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateNSPSbyYear.Location = New System.Drawing.Point(425, 441)
        Me.btnUpdateNSPSbyYear.Name = "btnUpdateNSPSbyYear"
        Me.btnUpdateNSPSbyYear.Size = New System.Drawing.Size(143, 23)
        Me.btnUpdateNSPSbyYear.TabIndex = 407
        Me.btnUpdateNSPSbyYear.Text = "Update Exemption by Year"
        Me.btnUpdateNSPSbyYear.UseVisualStyleBackColor = True
        '
        'btnAddExemptionToYear
        '
        Me.btnAddExemptionToYear.AutoSize = True
        Me.btnAddExemptionToYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddExemptionToYear.Location = New System.Drawing.Point(640, 634)
        Me.btnAddExemptionToYear.Name = "btnAddExemptionToYear"
        Me.btnAddExemptionToYear.Size = New System.Drawing.Size(125, 23)
        Me.btnAddExemptionToYear.TabIndex = 406
        Me.btnAddExemptionToYear.Text = "Add Exemption to Year"
        Me.btnAddExemptionToYear.UseVisualStyleBackColor = True
        Me.btnAddExemptionToYear.Visible = False
        '
        'cboNSPSExemptions
        '
        Me.cboNSPSExemptions.FormattingEnabled = True
        Me.cboNSPSExemptions.Location = New System.Drawing.Point(174, 586)
        Me.cboNSPSExemptions.Name = "cboNSPSExemptions"
        Me.cboNSPSExemptions.Size = New System.Drawing.Size(567, 21)
        Me.cboNSPSExemptions.TabIndex = 405
        Me.cboNSPSExemptions.Visible = False
        '
        'btnViewNSPSExemptionsByYear
        '
        Me.btnViewNSPSExemptionsByYear.AutoSize = True
        Me.btnViewNSPSExemptionsByYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewNSPSExemptionsByYear.Location = New System.Drawing.Point(506, 9)
        Me.btnViewNSPSExemptionsByYear.Name = "btnViewNSPSExemptionsByYear"
        Me.btnViewNSPSExemptionsByYear.Size = New System.Drawing.Size(140, 23)
        Me.btnViewNSPSExemptionsByYear.TabIndex = 404
        Me.btnViewNSPSExemptionsByYear.Text = "View selected NSPS Year"
        Me.btnViewNSPSExemptionsByYear.UseVisualStyleBackColor = True
        '
        'dgvNSPSExemptionsByYear
        '
        Me.dgvNSPSExemptionsByYear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNSPSExemptionsByYear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSExemptionsByYear.Location = New System.Drawing.Point(423, 64)
        Me.dgvNSPSExemptionsByYear.Name = "dgvNSPSExemptionsByYear"
        Me.dgvNSPSExemptionsByYear.ReadOnly = True
        Me.dgvNSPSExemptionsByYear.Size = New System.Drawing.Size(482, 354)
        Me.dgvNSPSExemptionsByYear.TabIndex = 403
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Location = New System.Drawing.Point(364, 14)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(53, 13)
        Me.Label108.TabIndex = 402
        Me.Label108.Text = "Fee Year:"
        '
        'cboNSPSExemptionYear
        '
        Me.cboNSPSExemptionYear.FormattingEnabled = True
        Me.cboNSPSExemptionYear.Location = New System.Drawing.Point(423, 11)
        Me.cboNSPSExemptionYear.Name = "cboNSPSExemptionYear"
        Me.cboNSPSExemptionYear.Size = New System.Drawing.Size(77, 21)
        Me.cboNSPSExemptionYear.TabIndex = 401
        '
        'btnDeleteNSPSExemption
        '
        Me.btnDeleteNSPSExemption.AutoSize = True
        Me.btnDeleteNSPSExemption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteNSPSExemption.Location = New System.Drawing.Point(25, 613)
        Me.btnDeleteNSPSExemption.Name = "btnDeleteNSPSExemption"
        Me.btnDeleteNSPSExemption.Size = New System.Drawing.Size(80, 23)
        Me.btnDeleteNSPSExemption.TabIndex = 400
        Me.btnDeleteNSPSExemption.Text = "Delete NSPS"
        Me.btnDeleteNSPSExemption.UseVisualStyleBackColor = True
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Location = New System.Drawing.Point(18, 571)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(82, 13)
        Me.Label107.TabIndex = 6
        Me.Label107.Text = "NSPS to Delete"
        '
        'txtDeleteNSPSExemptions
        '
        Me.txtDeleteNSPSExemptions.Location = New System.Drawing.Point(35, 587)
        Me.txtDeleteNSPSExemptions.Name = "txtDeleteNSPSExemptions"
        Me.txtDeleteNSPSExemptions.Size = New System.Drawing.Size(65, 20)
        Me.txtDeleteNSPSExemptions.TabIndex = 5
        '
        'txtNSPSExemption
        '
        Me.txtNSPSExemption.Location = New System.Drawing.Point(25, 443)
        Me.txtNSPSExemption.Multiline = True
        Me.txtNSPSExemption.Name = "txtNSPSExemption"
        Me.txtNSPSExemption.Size = New System.Drawing.Size(308, 80)
        Me.txtNSPSExemption.TabIndex = 4
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(12, 424)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(104, 13)
        Me.Label101.TabIndex = 3
        Me.Label101.Text = "New NSPS Reason:"
        '
        'btnAddNSPSExemption
        '
        Me.btnAddNSPSExemption.AutoSize = True
        Me.btnAddNSPSExemption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNSPSExemption.Location = New System.Drawing.Point(15, 529)
        Me.btnAddNSPSExemption.Name = "btnAddNSPSExemption"
        Me.btnAddNSPSExemption.Size = New System.Drawing.Size(145, 23)
        Me.btnAddNSPSExemption.TabIndex = 2
        Me.btnAddNSPSExemption.Text = "Add New NSPS Exemption"
        Me.btnAddNSPSExemption.UseVisualStyleBackColor = True
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Location = New System.Drawing.Point(22, 30)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(132, 13)
        Me.Label100.TabIndex = 1
        Me.Label100.Text = "Existing NSPS Exemptions"
        '
        'dgvNSPSExemptions
        '
        Me.dgvNSPSExemptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSExemptions.Location = New System.Drawing.Point(15, 46)
        Me.dgvNSPSExemptions.Name = "dgvNSPSExemptions"
        Me.dgvNSPSExemptions.ReadOnly = True
        Me.dgvNSPSExemptions.Size = New System.Drawing.Size(347, 372)
        Me.dgvNSPSExemptions.TabIndex = 0
        '
        'TPNonRespondersReport
        '
        Me.TPNonRespondersReport.Controls.Add(Me.TCLateFeeReports)
        Me.TPNonRespondersReport.Controls.Add(Me.Panel2)
        Me.TPNonRespondersReport.Location = New System.Drawing.Point(4, 22)
        Me.TPNonRespondersReport.Name = "TPNonRespondersReport"
        Me.TPNonRespondersReport.Size = New System.Drawing.Size(938, 706)
        Me.TPNonRespondersReport.TabIndex = 5
        Me.TPNonRespondersReport.Text = "Non Responders Report"
        Me.TPNonRespondersReport.UseVisualStyleBackColor = True
        '
        'TCLateFeeReports
        '
        Me.TCLateFeeReports.Controls.Add(Me.TPQuickFeeReport)
        Me.TCLateFeeReports.Controls.Add(Me.TPFullReport)
        Me.TCLateFeeReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCLateFeeReports.Location = New System.Drawing.Point(0, 50)
        Me.TCLateFeeReports.Name = "TCLateFeeReports"
        Me.TCLateFeeReports.SelectedIndex = 0
        Me.TCLateFeeReports.Size = New System.Drawing.Size(938, 656)
        Me.TCLateFeeReports.TabIndex = 3
        '
        'TPQuickFeeReport
        '
        Me.TPQuickFeeReport.Controls.Add(Me.dgvLateFeeReport)
        Me.TPQuickFeeReport.Controls.Add(Me.Panel3)
        Me.TPQuickFeeReport.Location = New System.Drawing.Point(4, 22)
        Me.TPQuickFeeReport.Name = "TPQuickFeeReport"
        Me.TPQuickFeeReport.Padding = New System.Windows.Forms.Padding(3)
        Me.TPQuickFeeReport.Size = New System.Drawing.Size(930, 630)
        Me.TPQuickFeeReport.TabIndex = 1
        Me.TPQuickFeeReport.Text = "Fee Report"
        Me.TPQuickFeeReport.UseVisualStyleBackColor = True
        '
        'dgvLateFeeReport
        '
        Me.dgvLateFeeReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLateFeeReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLateFeeReport.Location = New System.Drawing.Point(3, 183)
        Me.dgvLateFeeReport.Name = "dgvLateFeeReport"
        Me.dgvLateFeeReport.ReadOnly = True
        Me.dgvLateFeeReport.Size = New System.Drawing.Size(924, 444)
        Me.dgvLateFeeReport.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(924, 180)
        Me.Panel3.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnViewData)
        Me.Panel5.Controls.Add(Me.btnFeePendingPermittingEvent)
        Me.Panel5.Controls.Add(Me.Label106)
        Me.Panel5.Controls.Add(Me.txtFeePendingPermitType)
        Me.Panel5.Controls.Add(Me.txtFeePendingPermit)
        Me.Panel5.Controls.Add(Me.Label105)
        Me.Panel5.Controls.Add(Me.Label104)
        Me.Panel5.Controls.Add(Me.txtFeePermitNumber)
        Me.Panel5.Controls.Add(Me.lblPermitDate)
        Me.Panel5.Controls.Add(Me.Label102)
        Me.Panel5.Controls.Add(Me.txtFeePermittingEvent)
        Me.Panel5.Controls.Add(Me.btnFeeViewPermittingEvent)
        Me.Panel5.Controls.Add(Me.txtFeePermittingDate)
        Me.Panel5.Controls.Add(Me.Label103)
        Me.Panel5.Controls.Add(Me.txtFeePermittingEventType)
        Me.Panel5.Controls.Add(Me.lblComplianceDate)
        Me.Panel5.Controls.Add(Me.Label99)
        Me.Panel5.Controls.Add(Me.txtFeeComplianceEvent)
        Me.Panel5.Controls.Add(Me.btnFeeViewComplianceEvent)
        Me.Panel5.Controls.Add(Me.txtFeeLastComplianceEvent)
        Me.Panel5.Controls.Add(Me.btnFeeFacilitySummary)
        Me.Panel5.Controls.Add(Me.Label98)
        Me.Panel5.Controls.Add(Me.txtFeeComplianceEventType)
        Me.Panel5.Controls.Add(Me.Label97)
        Me.Panel5.Controls.Add(Me.txtFeeFacilityName)
        Me.Panel5.Controls.Add(Me.Label96)
        Me.Panel5.Controls.Add(Me.txtFeeAIRSNumber)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(143, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(781, 180)
        Me.Panel5.TabIndex = 10
        '
        'btnViewData
        '
        Me.btnViewData.Location = New System.Drawing.Point(192, 5)
        Me.btnViewData.Name = "btnViewData"
        Me.btnViewData.Size = New System.Drawing.Size(75, 23)
        Me.btnViewData.TabIndex = 26
        Me.btnViewData.Text = "View Data"
        Me.btnViewData.UseVisualStyleBackColor = True
        '
        'btnFeePendingPermittingEvent
        '
        Me.btnFeePendingPermittingEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFeePendingPermittingEvent.Location = New System.Drawing.Point(631, 129)
        Me.btnFeePendingPermittingEvent.Name = "btnFeePendingPermittingEvent"
        Me.btnFeePendingPermittingEvent.Size = New System.Drawing.Size(129, 23)
        Me.btnFeePendingPermittingEvent.TabIndex = 25
        Me.btnFeePendingPermittingEvent.Text = "View Permitting Event"
        Me.btnFeePendingPermittingEvent.UseVisualStyleBackColor = True
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.Location = New System.Drawing.Point(249, 119)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(31, 13)
        Me.Label106.TabIndex = 24
        Me.Label106.Text = "Type"
        '
        'txtFeePendingPermitType
        '
        Me.txtFeePendingPermitType.Location = New System.Drawing.Point(254, 135)
        Me.txtFeePendingPermitType.Name = "txtFeePendingPermitType"
        Me.txtFeePendingPermitType.ReadOnly = True
        Me.txtFeePendingPermitType.Size = New System.Drawing.Size(149, 20)
        Me.txtFeePendingPermitType.TabIndex = 23
        '
        'txtFeePendingPermit
        '
        Me.txtFeePendingPermit.Location = New System.Drawing.Point(188, 135)
        Me.txtFeePendingPermit.Name = "txtFeePendingPermit"
        Me.txtFeePendingPermit.ReadOnly = True
        Me.txtFeePendingPermit.Size = New System.Drawing.Size(60, 20)
        Me.txtFeePendingPermit.TabIndex = 22
        '
        'Label105
        '
        Me.Label105.AutoSize = True
        Me.Label105.Location = New System.Drawing.Point(57, 139)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(129, 13)
        Me.Label105.TabIndex = 21
        Me.Label105.Text = "Pending Permitting Event:"
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.Location = New System.Drawing.Point(496, 78)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(76, 13)
        Me.Label104.TabIndex = 20
        Me.Label104.Text = "Permit Number"
        '
        'txtFeePermitNumber
        '
        Me.txtFeePermitNumber.Location = New System.Drawing.Point(499, 92)
        Me.txtFeePermitNumber.Name = "txtFeePermitNumber"
        Me.txtFeePermitNumber.ReadOnly = True
        Me.txtFeePermitNumber.Size = New System.Drawing.Size(126, 20)
        Me.txtFeePermitNumber.TabIndex = 19
        '
        'lblPermitDate
        '
        Me.lblPermitDate.AutoSize = True
        Me.lblPermitDate.Location = New System.Drawing.Point(402, 78)
        Me.lblPermitDate.Name = "lblPermitDate"
        Me.lblPermitDate.Size = New System.Drawing.Size(88, 13)
        Me.lblPermitDate.TabIndex = 18
        Me.lblPermitDate.Text = "Date Final Action"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(249, 78)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(31, 13)
        Me.Label102.TabIndex = 17
        Me.Label102.Text = "Type"
        '
        'txtFeePermittingEvent
        '
        Me.txtFeePermittingEvent.Location = New System.Drawing.Point(188, 92)
        Me.txtFeePermittingEvent.Name = "txtFeePermittingEvent"
        Me.txtFeePermittingEvent.ReadOnly = True
        Me.txtFeePermittingEvent.Size = New System.Drawing.Size(60, 20)
        Me.txtFeePermittingEvent.TabIndex = 16
        '
        'btnFeeViewPermittingEvent
        '
        Me.btnFeeViewPermittingEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFeeViewPermittingEvent.Location = New System.Drawing.Point(631, 91)
        Me.btnFeeViewPermittingEvent.Name = "btnFeeViewPermittingEvent"
        Me.btnFeeViewPermittingEvent.Size = New System.Drawing.Size(129, 23)
        Me.btnFeeViewPermittingEvent.TabIndex = 15
        Me.btnFeeViewPermittingEvent.Text = "View Permitting Event"
        Me.btnFeeViewPermittingEvent.UseVisualStyleBackColor = True
        '
        'txtFeePermittingDate
        '
        Me.txtFeePermittingDate.Location = New System.Drawing.Point(408, 92)
        Me.txtFeePermittingDate.Name = "txtFeePermittingDate"
        Me.txtFeePermittingDate.ReadOnly = True
        Me.txtFeePermittingDate.Size = New System.Drawing.Size(81, 20)
        Me.txtFeePermittingDate.TabIndex = 14
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Location = New System.Drawing.Point(76, 96)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(110, 13)
        Me.Label103.TabIndex = 13
        Me.Label103.Text = "Last Permitting Event:"
        '
        'txtFeePermittingEventType
        '
        Me.txtFeePermittingEventType.Location = New System.Drawing.Point(254, 92)
        Me.txtFeePermittingEventType.Name = "txtFeePermittingEventType"
        Me.txtFeePermittingEventType.ReadOnly = True
        Me.txtFeePermittingEventType.Size = New System.Drawing.Size(149, 20)
        Me.txtFeePermittingEventType.TabIndex = 12
        '
        'lblComplianceDate
        '
        Me.lblComplianceDate.AutoSize = True
        Me.lblComplianceDate.Location = New System.Drawing.Point(402, 38)
        Me.lblComplianceDate.Name = "lblComplianceDate"
        Me.lblComplianceDate.Size = New System.Drawing.Size(30, 13)
        Me.lblComplianceDate.TabIndex = 11
        Me.lblComplianceDate.Text = "Date"
        '
        'Label99
        '
        Me.Label99.AutoSize = True
        Me.Label99.Location = New System.Drawing.Point(249, 38)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(31, 13)
        Me.Label99.TabIndex = 10
        Me.Label99.Text = "Type"
        '
        'txtFeeComplianceEvent
        '
        Me.txtFeeComplianceEvent.Location = New System.Drawing.Point(188, 52)
        Me.txtFeeComplianceEvent.Name = "txtFeeComplianceEvent"
        Me.txtFeeComplianceEvent.ReadOnly = True
        Me.txtFeeComplianceEvent.Size = New System.Drawing.Size(60, 20)
        Me.txtFeeComplianceEvent.TabIndex = 9
        '
        'btnFeeViewComplianceEvent
        '
        Me.btnFeeViewComplianceEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFeeViewComplianceEvent.Location = New System.Drawing.Point(631, 51)
        Me.btnFeeViewComplianceEvent.Name = "btnFeeViewComplianceEvent"
        Me.btnFeeViewComplianceEvent.Size = New System.Drawing.Size(129, 23)
        Me.btnFeeViewComplianceEvent.TabIndex = 8
        Me.btnFeeViewComplianceEvent.Text = "View Compliance Event"
        Me.btnFeeViewComplianceEvent.UseVisualStyleBackColor = True
        '
        'txtFeeLastComplianceEvent
        '
        Me.txtFeeLastComplianceEvent.Location = New System.Drawing.Point(408, 52)
        Me.txtFeeLastComplianceEvent.Name = "txtFeeLastComplianceEvent"
        Me.txtFeeLastComplianceEvent.ReadOnly = True
        Me.txtFeeLastComplianceEvent.Size = New System.Drawing.Size(81, 20)
        Me.txtFeeLastComplianceEvent.TabIndex = 7
        '
        'btnFeeFacilitySummary
        '
        Me.btnFeeFacilitySummary.AutoSize = True
        Me.btnFeeFacilitySummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFeeFacilitySummary.Location = New System.Drawing.Point(590, 5)
        Me.btnFeeFacilitySummary.Name = "btnFeeFacilitySummary"
        Me.btnFeeFacilitySummary.Size = New System.Drawing.Size(95, 23)
        Me.btnFeeFacilitySummary.TabIndex = 6
        Me.btnFeeFacilitySummary.Text = "Facility Summary"
        Me.btnFeeFacilitySummary.UseVisualStyleBackColor = True
        '
        'Label98
        '
        Me.Label98.AutoSize = True
        Me.Label98.Location = New System.Drawing.Point(67, 56)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(119, 13)
        Me.Label98.TabIndex = 5
        Me.Label98.Text = "Last Compliance Event:"
        '
        'txtFeeComplianceEventType
        '
        Me.txtFeeComplianceEventType.Location = New System.Drawing.Point(254, 52)
        Me.txtFeeComplianceEventType.Name = "txtFeeComplianceEventType"
        Me.txtFeeComplianceEventType.ReadOnly = True
        Me.txtFeeComplianceEventType.Size = New System.Drawing.Size(149, 20)
        Me.txtFeeComplianceEventType.TabIndex = 4
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.Location = New System.Drawing.Point(279, 10)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(70, 13)
        Me.Label97.TabIndex = 3
        Me.Label97.Text = "Facility Name"
        '
        'txtFeeFacilityName
        '
        Me.txtFeeFacilityName.Location = New System.Drawing.Point(357, 6)
        Me.txtFeeFacilityName.Name = "txtFeeFacilityName"
        Me.txtFeeFacilityName.ReadOnly = True
        Me.txtFeeFacilityName.Size = New System.Drawing.Size(227, 20)
        Me.txtFeeFacilityName.TabIndex = 2
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(49, 10)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(72, 13)
        Me.Label96.TabIndex = 1
        Me.Label96.Text = "AIRS Number"
        '
        'txtFeeAIRSNumber
        '
        Me.txtFeeAIRSNumber.Location = New System.Drawing.Point(127, 6)
        Me.txtFeeAIRSNumber.Name = "txtFeeAIRSNumber"
        Me.txtFeeAIRSNumber.ReadOnly = True
        Me.txtFeeAIRSNumber.Size = New System.Drawing.Size(59, 20)
        Me.txtFeeAIRSNumber.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.btnRemovePaidFacilities)
        Me.Panel4.Controls.Add(Me.btnViewUnenrolled)
        Me.Panel4.Controls.Add(Me.rdbHasNotPaidFee)
        Me.Panel4.Controls.Add(Me.btnCheckforFeesPaid)
        Me.Panel4.Controls.Add(Me.rdbHasPaidFee)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(143, 180)
        Me.Panel4.TabIndex = 7
        '
        'btnRemovePaidFacilities
        '
        Me.btnRemovePaidFacilities.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRemovePaidFacilities.Location = New System.Drawing.Point(3, 89)
        Me.btnRemovePaidFacilities.Name = "btnRemovePaidFacilities"
        Me.btnRemovePaidFacilities.Size = New System.Drawing.Size(137, 23)
        Me.btnRemovePaidFacilities.TabIndex = 8
        Me.btnRemovePaidFacilities.Text = "Remove Paid Facilities"
        Me.btnRemovePaidFacilities.UseVisualStyleBackColor = True
        '
        'btnViewUnenrolled
        '
        Me.btnViewUnenrolled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewUnenrolled.Location = New System.Drawing.Point(3, 133)
        Me.btnViewUnenrolled.Name = "btnViewUnenrolled"
        Me.btnViewUnenrolled.Size = New System.Drawing.Size(137, 23)
        Me.btnViewUnenrolled.TabIndex = 9
        Me.btnViewUnenrolled.Text = "View Unenrolled Facilities"
        Me.btnViewUnenrolled.UseVisualStyleBackColor = True
        '
        'rdbHasNotPaidFee
        '
        Me.rdbHasNotPaidFee.AutoSize = True
        Me.rdbHasNotPaidFee.Location = New System.Drawing.Point(3, 55)
        Me.rdbHasNotPaidFee.Name = "rdbHasNotPaidFee"
        Me.rdbHasNotPaidFee.Size = New System.Drawing.Size(66, 17)
        Me.rdbHasNotPaidFee.TabIndex = 1
        Me.rdbHasNotPaidFee.TabStop = True
        Me.rdbHasNotPaidFee.Text = "Not Paid"
        Me.rdbHasNotPaidFee.UseVisualStyleBackColor = True
        '
        'btnCheckforFeesPaid
        '
        Me.btnCheckforFeesPaid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCheckforFeesPaid.Location = New System.Drawing.Point(3, 3)
        Me.btnCheckforFeesPaid.Name = "btnCheckforFeesPaid"
        Me.btnCheckforFeesPaid.Size = New System.Drawing.Size(137, 23)
        Me.btnCheckforFeesPaid.TabIndex = 6
        Me.btnCheckforFeesPaid.Text = "Check Non Responders"
        Me.btnCheckforFeesPaid.UseVisualStyleBackColor = True
        '
        'rdbHasPaidFee
        '
        Me.rdbHasPaidFee.AutoSize = True
        Me.rdbHasPaidFee.Location = New System.Drawing.Point(3, 32)
        Me.rdbHasPaidFee.Name = "rdbHasPaidFee"
        Me.rdbHasPaidFee.Size = New System.Drawing.Size(46, 17)
        Me.rdbHasPaidFee.TabIndex = 0
        Me.rdbHasPaidFee.TabStop = True
        Me.rdbHasPaidFee.Text = "Paid"
        Me.rdbHasPaidFee.UseVisualStyleBackColor = True
        '
        'TPFullReport
        '
        Me.TPFullReport.Controls.Add(Me.dgvLateFeePayerReport)
        Me.TPFullReport.Location = New System.Drawing.Point(4, 22)
        Me.TPFullReport.Name = "TPFullReport"
        Me.TPFullReport.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFullReport.Size = New System.Drawing.Size(930, 630)
        Me.TPFullReport.TabIndex = 0
        Me.TPFullReport.Text = "Full Report (Slow)"
        Me.TPFullReport.UseVisualStyleBackColor = True
        '
        'dgvLateFeePayerReport
        '
        Me.dgvLateFeePayerReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLateFeePayerReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLateFeePayerReport.Location = New System.Drawing.Point(3, 3)
        Me.dgvLateFeePayerReport.Name = "dgvLateFeePayerReport"
        Me.dgvLateFeePayerReport.ReadOnly = True
        Me.dgvLateFeePayerReport.Size = New System.Drawing.Size(924, 624)
        Me.dgvLateFeePayerReport.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnExportFeeReport)
        Me.Panel2.Controls.Add(Me.btnRunReport)
        Me.Panel2.Controls.Add(Me.txtFeeCount)
        Me.Panel2.Controls.Add(Me.lblCount)
        Me.Panel2.Controls.Add(Me.btnRunLateFeeReport)
        Me.Panel2.Controls.Add(Me.cboFeeYear)
        Me.Panel2.Controls.Add(Me.Label94)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(938, 50)
        Me.Panel2.TabIndex = 1
        '
        'btnExportFeeReport
        '
        Me.btnExportFeeReport.AutoSize = True
        Me.btnExportFeeReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportFeeReport.Location = New System.Drawing.Point(526, 7)
        Me.btnExportFeeReport.Name = "btnExportFeeReport"
        Me.btnExportFeeReport.Size = New System.Drawing.Size(88, 23)
        Me.btnExportFeeReport.TabIndex = 28
        Me.btnExportFeeReport.Text = "Export to Excel"
        Me.btnExportFeeReport.UseVisualStyleBackColor = True
        '
        'btnRunReport
        '
        Me.btnRunReport.AutoSize = True
        Me.btnRunReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunReport.Location = New System.Drawing.Point(208, 6)
        Me.btnRunReport.Name = "btnRunReport"
        Me.btnRunReport.Size = New System.Drawing.Size(93, 23)
        Me.btnRunReport.TabIndex = 5
        Me.btnRunReport.Text = "Run Fee Report"
        Me.btnRunReport.UseVisualStyleBackColor = True
        '
        'txtFeeCount
        '
        Me.txtFeeCount.Location = New System.Drawing.Point(667, 7)
        Me.txtFeeCount.Name = "txtFeeCount"
        Me.txtFeeCount.ReadOnly = True
        Me.txtFeeCount.Size = New System.Drawing.Size(63, 20)
        Me.txtFeeCount.TabIndex = 4
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(626, 11)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(35, 13)
        Me.lblCount.TabIndex = 3
        Me.lblCount.Text = "Count"
        '
        'btnRunLateFeeReport
        '
        Me.btnRunLateFeeReport.AutoSize = True
        Me.btnRunLateFeeReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunLateFeeReport.Location = New System.Drawing.Point(311, 5)
        Me.btnRunLateFeeReport.Name = "btnRunLateFeeReport"
        Me.btnRunLateFeeReport.Size = New System.Drawing.Size(123, 23)
        Me.btnRunLateFeeReport.TabIndex = 2
        Me.btnRunLateFeeReport.Text = "Run Full Report (Slow)"
        Me.btnRunLateFeeReport.UseVisualStyleBackColor = True
        '
        'cboFeeYear
        '
        Me.cboFeeYear.FormattingEnabled = True
        Me.cboFeeYear.Location = New System.Drawing.Point(81, 8)
        Me.cboFeeYear.Name = "cboFeeYear"
        Me.cboFeeYear.Size = New System.Drawing.Size(121, 21)
        Me.cboFeeYear.TabIndex = 1
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Location = New System.Drawing.Point(8, 11)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(71, 13)
        Me.Label94.TabIndex = 0
        Me.Label94.Text = "Select a Year"
        '
        'bgwAIRS
        '
        Me.bgwAIRS.WorkerSupportsCancellation = True
        '
        'bgwEmails
        '
        Me.bgwEmails.WorkerSupportsCancellation = True
        '
        'PASPMailoutAndStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 746)
        Me.Controls.Add(Me.TCMailoutAndStats)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PASPMailoutAndStats"
        Me.Text = "PASP Fee Statistics & Mailout"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCMailoutAndStats.ResumeLayout(False)
        Me.TPDepositAndPaymentStats.ResumeLayout(False)
        CType(Me.dgvDepositsAndPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.pnlCorrectPaymentType.ResumeLayout(False)
        Me.pnlCorrectPaymentType.PerformLayout()
        CType(Me.dgvStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TPFeeStatistics.ResumeLayout(False)
        Me.SCFeeStatistics.Panel1.ResumeLayout(False)
        Me.SCFeeStatistics.Panel1.PerformLayout()
        Me.SCFeeStatistics.Panel2.ResumeLayout(False)
        Me.SCFeeStatistics.ResumeLayout(False)
        CType(Me.dgvFeeDataCount2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpViewdata2.ResumeLayout(False)
        Me.gpViewdata2.PerformLayout()
        Me.TPMiscWebTools.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TPWebUsers.ResumeLayout(False)
        Me.pnlUser.ResumeLayout(False)
        Me.pnlUser.PerformLayout()
        CType(Me.dgrUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.TPWebUsers1.ResumeLayout(False)
        Me.pnlUserFacility.ResumeLayout(False)
        Me.pnlUserFacility.PerformLayout()
        Me.pnlUserInfo.ResumeLayout(False)
        Me.pnlUserInfo.PerformLayout()
        CType(Me.dgrFacilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUserEmail.ResumeLayout(False)
        Me.pnlUserEmail.PerformLayout()
        Me.TPActivate.ResumeLayout(False)
        Me.TPActivate.PerformLayout()
        Me.TPFeeFacility.ResumeLayout(False)
        Me.TPFeeFacility.PerformLayout()
        Me.TPGenerateMailOut.ResumeLayout(False)
        Me.SCGenerateMailOut.Panel1.ResumeLayout(False)
        Me.SCGenerateMailOut.Panel1.PerformLayout()
        Me.SCGenerateMailOut.Panel2.ResumeLayout(False)
        Me.SCGenerateMailOut.ResumeLayout(False)
        CType(Me.dgvFeeDataCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpViewdata.ResumeLayout(False)
        Me.gpViewdata.PerformLayout()
        Me.TPEnrollment.ResumeLayout(False)
        Me.TPEnrollment.PerformLayout()
        Me.TPFeeRates.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.pnlNSPSExemptions.ResumeLayout(False)
        Me.pnlNSPSExemptions.PerformLayout()
        CType(Me.dgvNSPSExemptionsByYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNSPSExemptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPNonRespondersReport.ResumeLayout(False)
        Me.TCLateFeeReports.ResumeLayout(False)
        Me.TPQuickFeeReport.ResumeLayout(False)
        CType(Me.dgvLateFeeReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TPFullReport.ResumeLayout(False)
        CType(Me.dgvLateFeePayerReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TCMailoutAndStats As System.Windows.Forms.TabControl
    Friend WithEvents TPGenerateMailOut As System.Windows.Forms.TabPage
    Friend WithEvents TPEnrollment As System.Windows.Forms.TabPage
    Friend WithEvents btnRefreshAirsNo As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblviewselectedyearMailOutlist As System.Windows.Forms.LinkLabel
    Friend WithEvents btnDeleteFeeMailOut As System.Windows.Forms.Button
    Friend WithEvents cboMailoutYear As System.Windows.Forms.ComboBox
    Friend WithEvents btnGenerateFeeMailOut As System.Windows.Forms.Button
    Friend WithEvents txtFacilityZip As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityState As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityStreet As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityAddress As System.Windows.Forms.TextBox
    Friend WithEvents cboPart70 As System.Windows.Forms.ComboBox
    Friend WithEvents cboNSPS As System.Windows.Forms.ComboBox
    Friend WithEvents cboOperation As System.Windows.Forms.ComboBox
    Friend WithEvents cboClass As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtContactAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnFeeDelete As System.Windows.Forms.Button
    Friend WithEvents btnFeeSave As System.Windows.Forms.Button
    Friend WithEvents txtMailoutEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtContactZip As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtContactState As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtContactCity As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtShutdowndate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAirsNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TPFeeStatistics As System.Windows.Forms.TabPage
    Friend WithEvents TPMiscWebTools As System.Windows.Forms.TabPage
    Friend WithEvents gpViewdata As System.Windows.Forms.GroupBox
    Friend WithEvents dgvFeeDataCount As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExportExcel As System.Windows.Forms.Button
    Friend WithEvents txtRecordNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblEnrollYear As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDeEnroll As System.Windows.Forms.Button
    Friend WithEvents btnEnroll As System.Windows.Forms.Button
    Friend WithEvents gpViewdata2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvFeeDataCount2 As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExportExcel2 As System.Windows.Forms.Button
    Friend WithEvents txtRecordNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents lblviewsumTrueNonresponse As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumRemovedFacility As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewsumarryMailout As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumTotalResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewSumInProcess As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewSumExtraResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumNonResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumLateresponse As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumtotalInporcess As System.Windows.Forms.LinkLabel
    Friend WithEvents lblviewSumExtraToalFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewTrueNonresponsers As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTrueNonResponsers As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblViewRemovedFacilities As System.Windows.Forms.LinkLabel
    Friend WithEvents txtRemovedFacilities As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblViewMailOut As System.Windows.Forms.LinkLabel
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtResponseCount As System.Windows.Forms.TextBox
    Friend WithEvents lblViewTotalResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTotalResponse As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblViewExtraInProcess As System.Windows.Forms.LinkLabel
    Friend WithEvents txtExtraInProcess As System.Windows.Forms.TextBox
    Friend WithEvents lblViewExtraFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents txtExtraFinalized As System.Windows.Forms.TextBox
    Friend WithEvents lblViewMailoutInprocess As System.Windows.Forms.LinkLabel
    Friend WithEvents txtMailOutInProcess As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents lblViewMailoutFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents txtMailoutFinalized As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblextraResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents txtextraResponse As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblViewNonResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewOutofcompliance As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewINCompliance As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewTotalInProcess As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewTotalFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTotaloutofcompliance As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtTotalincompliance As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtNonResponseCount As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtTotalInProcessCount As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalFinalizedCount As System.Windows.Forms.TextBox
    Friend WithEvents txtMailOutCount As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtfeeYear As System.Windows.Forms.TextBox
    Friend WithEvents cboYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents lblfeeYear As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPWebUsers As System.Windows.Forms.TabPage
    Friend WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents cboUsers As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents dgrUsers As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents TPWebUsers1 As System.Windows.Forms.TabPage
    Friend WithEvents pnlUserInfo As System.Windows.Forms.Panel
    Friend WithEvents lblCityStateZip As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblFaxNo As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents lblCoName As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblLName As System.Windows.Forms.Label
    Friend WithEvents lblFName As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnlUserFacility As System.Windows.Forms.Panel
    Friend WithEvents cboFacilityToDelete As System.Windows.Forms.ComboBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteFacilityUser As System.Windows.Forms.Button
    Friend WithEvents btnUpdateUser As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents btnAddFacilitytoUser As System.Windows.Forms.Button
    Friend WithEvents dgrFacilities As System.Windows.Forms.DataGrid
    Friend WithEvents pnlUserEmail As System.Windows.Forms.Panel
    Friend WithEvents cboUserEmail As System.Windows.Forms.ComboBox
    Friend WithEvents lblViewFacility As System.Windows.Forms.LinkLabel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnActivateEmail As System.Windows.Forms.Button
    Friend WithEvents TPActivate As System.Windows.Forms.TabPage
    Friend WithEvents btnActivateUser As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents TPFeeFacility As System.Windows.Forms.TabPage
    Friend WithEvents btnRemoveFacility As System.Windows.Forms.Button
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents btnAddFacility As System.Windows.Forms.Button
    Friend WithEvents SCGenerateMailOut As System.Windows.Forms.SplitContainer
    Friend WithEvents SCFeeStatistics As System.Windows.Forms.SplitContainer
    Friend WithEvents bgwAIRS As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwEmails As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblviewsumarrymailoutinfo As System.Windows.Forms.LinkLabel
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblshutdowndate As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents lblAirsNo As System.Windows.Forms.Label
    Friend WithEvents lblclass As System.Windows.Forms.Label
    Friend WithEvents lblPart70 As System.Windows.Forms.Label
    Friend WithEvents lblNSPS As System.Windows.Forms.Label
    Friend WithEvents lbloperationalstatus As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblcity As System.Windows.Forms.Label
    Friend WithEvents lblcontactstreet As System.Windows.Forms.Label
    Friend WithEvents lblfirstname As System.Windows.Forms.Label
    Friend WithEvents lblFacilityName As System.Windows.Forms.Label
    Friend WithEvents lblLastname As System.Windows.Forms.Label
    Friend WithEvents lblstate As System.Windows.Forms.Label
    Friend WithEvents lblzip As System.Windows.Forms.Label

    Friend WithEvents lblsumextrafacility As System.Windows.Forms.LinkLabel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtextrafacilities As System.Windows.Forms.TextBox
    Friend WithEvents lblextrafacility As System.Windows.Forms.LinkLabel

    '  Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents llbViewUserData As System.Windows.Forms.LinkLabel
    Friend WithEvents lblViewEmailData As System.Windows.Forms.LinkLabel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtWebUserEmail As System.Windows.Forms.TextBox
    Friend WithEvents mtbFacilityToAdd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEditUserPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdatePassword As System.Windows.Forms.Button
    Friend WithEvents txtWebUserID As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveEditedData As System.Windows.Forms.Button
    Friend WithEvents mtbEditZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbEditState As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbEditFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbEditPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEditCity As System.Windows.Forms.TextBox
    Friend WithEvents txtEditAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtEditCompany As System.Windows.Forms.TextBox
    Friend WithEvents txtEditTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtEditLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFirstName As System.Windows.Forms.TextBox
    Friend WithEvents btnEditUserData As System.Windows.Forms.Button
    Friend WithEvents lblConfirmDate As System.Windows.Forms.Label
    Friend WithEvents lblLastLogIn As System.Windows.Forms.Label
    Friend WithEvents btnChangeEmailAddress As System.Windows.Forms.Button
    Friend WithEvents txtEditEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblsumExtraNonresponse As System.Windows.Forms.LinkLabel
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtExtraNonResponse As System.Windows.Forms.TextBox
    Friend WithEvents lblExtraNonResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents Label35 As System.Windows.Forms.Label

    Friend WithEvents TPFeeRates As System.Windows.Forms.TabPage
    Friend WithEvents Label248 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents lblViewFeeRate As System.Windows.Forms.LinkLabel
    Friend WithEvents cboFeeRateYear As System.Windows.Forms.ComboBox
    Friend WithEvents txtTitleVfee As System.Windows.Forms.TextBox
    Friend WithEvents txtperTonRate As System.Windows.Forms.TextBox
    Friend WithEvents txtAdminFeePercent As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeRate As System.Windows.Forms.Button

    Friend WithEvents TPDepositAndPaymentStats As System.Windows.Forms.TabPage
    Friend WithEvents dgvDepositsAndPayments As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents cboStatPayType As System.Windows.Forms.ComboBox
    Friend WithEvents txtTotalPaymentDue As System.Windows.Forms.TextBox
    Friend WithEvents btnViewDepositsStats As System.Windows.Forms.Button
    Friend WithEvents cboStatYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents btnViewBalance As System.Windows.Forms.Button
    Friend WithEvents bntViewTotalPaid As System.Windows.Forms.Button
    Friend WithEvents btnViewPaymentDue As System.Windows.Forms.Button
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtDateSubmitted As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtOnlineSubmittalStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents txtPaymentType As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedYear As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnViewSelectedFeeData As System.Windows.Forms.Button
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtSubmittalComments As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents txtTotalFee As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtNSPSFee2 As System.Windows.Forms.TextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtPMTons As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents txtSO2Tons As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtNOxTons As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtPart70Fee As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents txtSMFee2 As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtVOCTons As System.Windows.Forms.TextBox
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents txtNSPS As System.Windows.Forms.TextBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents txtCalculatedFee As System.Windows.Forms.TextBox
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents txtSyntheticMinor As System.Windows.Forms.TextBox
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents txtPart70 As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents txtNSPSExemptReason As System.Windows.Forms.TextBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents txtFeeRate As System.Windows.Forms.TextBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents txtOperate As System.Windows.Forms.TextBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents txtNSPSReason As System.Windows.Forms.TextBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents txtNSPSExempt As System.Windows.Forms.TextBox
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents txtVarianceComments As System.Windows.Forms.TextBox
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents txtVarianceCheck As System.Windows.Forms.TextBox
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents txtShutDown As System.Windows.Forms.TextBox
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents dgvStats As System.Windows.Forms.DataGridView
    Friend WithEvents btnHideResults As System.Windows.Forms.Button
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents chbNonZeroBalance As System.Windows.Forms.CheckBox
    Friend WithEvents btnCorrectPaymentType As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlCorrectPaymentType As System.Windows.Forms.Panel
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents btnUpdatePaymentType As System.Windows.Forms.Button
    Friend WithEvents cboNewPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSMfee As System.Windows.Forms.TextBox
    Friend WithEvents txtNSPSfee As System.Windows.Forms.TextBox
    Friend WithEvents txtAnnualSMFee As System.Windows.Forms.TextBox
    Friend WithEvents txtAnnualNSPSFee As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents btnsaveRate As System.Windows.Forms.Button
    Friend WithEvents dtpduedate As System.Windows.Forms.DateTimePicker
    Friend WithEvents mtbFeeAirsNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbyear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TPNonRespondersReport As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cboFeeYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents dgvLateFeePayerReport As System.Windows.Forms.DataGridView
    Friend WithEvents btnRunLateFeeReport As System.Windows.Forms.Button
    Friend WithEvents txtFeeCount As System.Windows.Forms.TextBox
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents TCLateFeeReports As System.Windows.Forms.TabControl
    Friend WithEvents TPFullReport As System.Windows.Forms.TabPage
    Friend WithEvents TPQuickFeeReport As System.Windows.Forms.TabPage
    Friend WithEvents btnRunReport As System.Windows.Forms.Button
    Friend WithEvents dgvLateFeeReport As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbHasPaidFee As System.Windows.Forms.RadioButton
    Friend WithEvents btnCheckforFeesPaid As System.Windows.Forms.Button
    Friend WithEvents rdbHasNotPaidFee As System.Windows.Forms.RadioButton
    Friend WithEvents btnRemovePaidFacilities As System.Windows.Forms.Button
    Friend WithEvents btnViewUnenrolled As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents txtFeeAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents txtFeeFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents txtFeeComplianceEventType As System.Windows.Forms.TextBox
    Friend WithEvents txtFeeLastComplianceEvent As System.Windows.Forms.TextBox
    Friend WithEvents btnFeeFacilitySummary As System.Windows.Forms.Button
    Friend WithEvents btnFeeViewComplianceEvent As System.Windows.Forms.Button
    Friend WithEvents txtFeeComplianceEvent As System.Windows.Forms.TextBox
    Friend WithEvents lblComplianceDate As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents lblPermitDate As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents txtFeePermittingEvent As System.Windows.Forms.TextBox
    Friend WithEvents btnFeeViewPermittingEvent As System.Windows.Forms.Button
    Friend WithEvents txtFeePermittingDate As System.Windows.Forms.TextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents txtFeePermittingEventType As System.Windows.Forms.TextBox
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents txtFeePermitNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnFeePendingPermittingEvent As System.Windows.Forms.Button
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents txtFeePendingPermitType As System.Windows.Forms.TextBox
    Friend WithEvents txtFeePendingPermit As System.Windows.Forms.TextBox
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents btnViewData As System.Windows.Forms.Button
    Friend WithEvents btnExportFeeReport As System.Windows.Forms.Button
    Friend WithEvents pnlNSPSExemptions As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents dgvNSPSExemptions As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoadNSPSTool As System.Windows.Forms.Button
    Friend WithEvents txtNSPSExemption As System.Windows.Forms.TextBox
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents btnAddNSPSExemption As System.Windows.Forms.Button
    Friend WithEvents btnDeleteNSPSExemption As System.Windows.Forms.Button
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents txtDeleteNSPSExemptions As System.Windows.Forms.TextBox
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents cboNSPSExemptionYear As System.Windows.Forms.ComboBox
    Friend WithEvents btnViewNSPSExemptionsByYear As System.Windows.Forms.Button
    Friend WithEvents dgvNSPSExemptionsByYear As System.Windows.Forms.DataGridView
    Friend WithEvents cboNSPSExemptions As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddExemptionToYear As System.Windows.Forms.Button
    Friend WithEvents btnUpdateNSPSbyYear As System.Windows.Forms.Button
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnSelectForm As System.Windows.Forms.Button
    Friend WithEvents btnUnselectForm As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllForms As System.Windows.Forms.Button
    Friend WithEvents btnUnselectAllForms As System.Windows.Forms.Button
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbDepositDateSearch As System.Windows.Forms.CheckBox
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents btnRunDepositReport As System.Windows.Forms.Button
    Friend WithEvents dtpFeeDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents txtAdminFee As System.Windows.Forms.TextBox
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents txtAllFees As System.Windows.Forms.TextBox

End Class
