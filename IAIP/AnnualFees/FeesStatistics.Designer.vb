<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FeesStatistics
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
        Me.TCMailoutAndStats = New System.Windows.Forms.TabControl()
        Me.TPDepositAndPaymentStats = New System.Windows.Forms.TabPage()
        Me.dgvDepositsAndPayments = New Iaip.IaipDataGridView()
        Me.lblCountDepositsPayments = New System.Windows.Forms.Label()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.txtIAIPStatus = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.txtAllFees = New System.Windows.Forms.TextBox()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.txtAdminFee = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.txtSMfee = New System.Windows.Forms.TextBox()
        Me.txtNSPSfee = New System.Windows.Forms.TextBox()
        Me.dgvStats = New System.Windows.Forms.DataGridView()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.txtShutDown = New System.Windows.Forms.TextBox()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.txtNSPS = New System.Windows.Forms.TextBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.txtCalculatedFee = New System.Windows.Forms.TextBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.txtSyntheticMinor = New System.Windows.Forms.TextBox()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.txtPart70 = New System.Windows.Forms.TextBox()
        Me.txtNSPSExemptReason = New System.Windows.Forms.TextBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.txtFeeRate = New System.Windows.Forms.TextBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.txtOperate = New System.Windows.Forms.TextBox()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.txtNSPSExempt = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.txtPMTons = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.txtSO2Tons = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.txtNOxTons = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtPart70MaintenanceFee = New System.Windows.Forms.TextBox()
        Me.txtPart70Fee = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtVOCTons = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtSubmittalComments = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.txtDateSubmitted = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtOnlineSubmittalStatus = New System.Windows.Forms.TextBox()
        Me.txtPaymentType = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnViewInvoicedBalance = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnInvoiceReportVariance = New System.Windows.Forms.Button()
        Me.btnInvoicedPaymentDue = New System.Windows.Forms.Button()
        Me.txtInvoicedBalance = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTotalPaymentInvoiced = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnRunDepositReport = New System.Windows.Forms.Button()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.dtpEndDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.chbDepositDateSearch = New System.Windows.Forms.CheckBox()
        Me.chbNonZeroBalance = New System.Windows.Forms.CheckBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.txtSelectedYear = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.txtSelectedFacilityName = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtSelectedAIRSNumber = New System.Windows.Forms.TextBox()
        Me.btnViewSelectedFeeData = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.btnViewTotalPaid = New System.Windows.Forms.Button()
        Me.btnViewPaymentDue = New System.Windows.Forms.Button()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.txtTotalPaid = New System.Windows.Forms.TextBox()
        Me.cboStatYear = New System.Windows.Forms.ComboBox()
        Me.cboStatPayType = New System.Windows.Forms.ComboBox()
        Me.txtTotalPaymentDue = New System.Windows.Forms.TextBox()
        Me.TPFeeStatistics2 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvFeeStats = New Iaip.IaipDataGridView()
        Me.lblCountFeeStats = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.FeeStatAirsNumberEntry = New Iaip.AirsNumberEntryForm()
        Me.btnOpenFeesLog = New System.Windows.Forms.Button()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.btnCheckInvoices = New System.Windows.Forms.Button()
        Me.llbFSSummaryPaidNotFinalized = New System.Windows.Forms.LinkLabel()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtFSPaidNotFinalized = New System.Windows.Forms.TextBox()
        Me.llbDetailPaidNotFinalized = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryPaidFinalized = New System.Windows.Forms.LinkLabel()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtFSPaidFinalized = New System.Windows.Forms.TextBox()
        Me.llbDetailPaidFinalized = New System.Windows.Forms.LinkLabel()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.llbFSSummaryLateWithFee = New System.Windows.Forms.LinkLabel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtFSLateFee = New System.Windows.Forms.TextBox()
        Me.llbDetailLateWithFee = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryLateResponse = New System.Windows.Forms.LinkLabel()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtFSOnTimeResponse = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtFSLateResponse = New System.Windows.Forms.TextBox()
        Me.llbFSDetailOnTime = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryOnTime = New System.Windows.Forms.LinkLabel()
        Me.llbDetailLateResponse = New System.Windows.Forms.LinkLabel()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtFSQuarterly = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtFSAnnual = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtFSOverPaid = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtFSPartial = New System.Windows.Forms.TextBox()
        Me.llbFSSummaryPaidInFull = New System.Windows.Forms.LinkLabel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtFSPaidInFull = New System.Windows.Forms.TextBox()
        Me.llbDetailPaidInFull = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryOutofBalance = New System.Windows.Forms.LinkLabel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtFSOutOfBalance = New System.Windows.Forms.TextBox()
        Me.llbDetailOutOfBalance = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryNotPaid = New System.Windows.Forms.LinkLabel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtFSNotPaid = New System.Windows.Forms.TextBox()
        Me.llbDetailNotPaid = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryFinalized = New System.Windows.Forms.LinkLabel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtFSFinalized = New System.Windows.Forms.TextBox()
        Me.llbDetailFinalized = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryInProgress = New System.Windows.Forms.LinkLabel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtFSInProgress = New System.Windows.Forms.TextBox()
        Me.llbDetailInProgress = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryNotReported = New System.Windows.Forms.LinkLabel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtFSNotReported = New System.Windows.Forms.TextBox()
        Me.llbDetailNotReported = New System.Windows.Forms.LinkLabel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.llbFSSummaryAdditions = New System.Windows.Forms.LinkLabel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtFSAdditions = New System.Windows.Forms.TextBox()
        Me.llbDetailAdditions = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryMailOut = New System.Windows.Forms.LinkLabel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtFSMailout = New System.Windows.Forms.TextBox()
        Me.llbDetailMailout = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryEnrolled = New System.Windows.Forms.LinkLabel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtFSEnrolled = New System.Windows.Forms.TextBox()
        Me.llbDetailEnrolled = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryCeaseCollection = New System.Windows.Forms.LinkLabel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFSCeaseCollection = New System.Windows.Forms.TextBox()
        Me.llbDetailCeaseCollection = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryUnEnrolled = New System.Windows.Forms.LinkLabel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtFSUnEnrolled = New System.Windows.Forms.TextBox()
        Me.llbDetailUnEnrolled = New System.Windows.Forms.LinkLabel()
        Me.llbFSSummaryFeeUniverse = New System.Windows.Forms.LinkLabel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtFSFeeUniverse = New System.Windows.Forms.TextBox()
        Me.llbDetailFeeUniverse = New System.Windows.Forms.LinkLabel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboFeeStatYear = New System.Windows.Forms.ComboBox()
        Me.TPReported = New System.Windows.Forms.TabPage()
        Me.dgvReported = New Iaip.IaipDataGridView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.cbReportedYear = New System.Windows.Forms.ComboBox()
        Me.btnExportedRun = New System.Windows.Forms.Button()
        Me.TPReports = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GridFeesReports = New Iaip.IaipDataGridView()
        Me.tabReport = New System.Windows.Forms.TabControl()
        Me.TPFinancial = New System.Windows.Forms.TabPage()
        Me.btnPayment = New System.Windows.Forms.Button()
        Me.TPAnnualBalance = New System.Windows.Forms.TabPage()
        Me.cbBalanceYear = New System.Windows.Forms.ComboBox()
        Me.lblFacilityBalanceReportTag = New System.Windows.Forms.Label()
        Me.btnRunBalanceReport = New System.Windows.Forms.Button()
        Me.TPDeposits = New System.Windows.Forms.TabPage()
        Me.btnViewDepositsReportByDate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDepositReportEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDepositReportStartDate = New System.Windows.Forms.DateTimePicker()
        Me.btnViewFacilityDepositsReport = New System.Windows.Forms.Button()
        Me.cboAirs = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TPCompliance = New System.Windows.Forms.TabPage()
        Me.btnClassification = New System.Windows.Forms.Button()
        Me.btnClassChange = New System.Windows.Forms.Button()
        Me.TPNsps = New System.Windows.Forms.TabPage()
        Me.lblNSPS3 = New System.Windows.Forms.LinkLabel()
        Me.lblNSPS2 = New System.Windows.Forms.LinkLabel()
        Me.lblNSPS1 = New System.Windows.Forms.LinkLabel()
        Me.TCMailoutAndStats.SuspendLayout()
        Me.TPDepositAndPaymentStats.SuspendLayout()
        CType(Me.dgvDepositsAndPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails.SuspendLayout()
        CType(Me.dgvStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TPFeeStatistics2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvFeeStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.TPReported.SuspendLayout()
        CType(Me.dgvReported, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.TPReports.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.GridFeesReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabReport.SuspendLayout()
        Me.TPFinancial.SuspendLayout()
        Me.TPAnnualBalance.SuspendLayout()
        Me.TPDeposits.SuspendLayout()
        Me.TPCompliance.SuspendLayout()
        Me.TPNsps.SuspendLayout()
        Me.SuspendLayout()
        '
        'TCMailoutAndStats
        '
        Me.TCMailoutAndStats.Controls.Add(Me.TPDepositAndPaymentStats)
        Me.TCMailoutAndStats.Controls.Add(Me.TPFeeStatistics2)
        Me.TCMailoutAndStats.Controls.Add(Me.TPReported)
        Me.TCMailoutAndStats.Controls.Add(Me.TPReports)
        Me.TCMailoutAndStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMailoutAndStats.Location = New System.Drawing.Point(0, 0)
        Me.TCMailoutAndStats.Name = "TCMailoutAndStats"
        Me.TCMailoutAndStats.SelectedIndex = 0
        Me.TCMailoutAndStats.Size = New System.Drawing.Size(944, 718)
        Me.TCMailoutAndStats.TabIndex = 9
        '
        'TPDepositAndPaymentStats
        '
        Me.TPDepositAndPaymentStats.Controls.Add(Me.dgvDepositsAndPayments)
        Me.TPDepositAndPaymentStats.Controls.Add(Me.pnlDetails)
        Me.TPDepositAndPaymentStats.Controls.Add(Me.Panel1)
        Me.TPDepositAndPaymentStats.Location = New System.Drawing.Point(4, 22)
        Me.TPDepositAndPaymentStats.Name = "TPDepositAndPaymentStats"
        Me.TPDepositAndPaymentStats.Size = New System.Drawing.Size(936, 692)
        Me.TPDepositAndPaymentStats.TabIndex = 4
        Me.TPDepositAndPaymentStats.Text = "Deposits and Payments"
        Me.TPDepositAndPaymentStats.UseVisualStyleBackColor = True
        '
        'dgvDepositsAndPayments
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDepositsAndPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvDepositsAndPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDepositsAndPayments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDepositsAndPayments.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvDepositsAndPayments.LinkifyColumnByName = Nothing
        Me.dgvDepositsAndPayments.Location = New System.Drawing.Point(0, 525)
        Me.dgvDepositsAndPayments.Name = "dgvDepositsAndPayments"
        Me.dgvDepositsAndPayments.ResultsCountLabel = Me.lblCountDepositsPayments
        Me.dgvDepositsAndPayments.ResultsCountLabelFormat = "{0} found"
        Me.dgvDepositsAndPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvDepositsAndPayments.Size = New System.Drawing.Size(936, 167)
        Me.dgvDepositsAndPayments.StandardTab = True
        Me.dgvDepositsAndPayments.TabIndex = 7
        '
        'lblCountDepositsPayments
        '
        Me.lblCountDepositsPayments.Location = New System.Drawing.Point(813, 133)
        Me.lblCountDepositsPayments.Name = "lblCountDepositsPayments"
        Me.lblCountDepositsPayments.Size = New System.Drawing.Size(100, 23)
        Me.lblCountDepositsPayments.TabIndex = 415
        Me.lblCountDepositsPayments.Text = "0 found"
        Me.lblCountDepositsPayments.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlDetails
        '
        Me.pnlDetails.Controls.Add(Me.txtIAIPStatus)
        Me.pnlDetails.Controls.Add(Me.Label20)
        Me.pnlDetails.Controls.Add(Me.Label116)
        Me.pnlDetails.Controls.Add(Me.txtAllFees)
        Me.pnlDetails.Controls.Add(Me.Label115)
        Me.pnlDetails.Controls.Add(Me.txtAdminFee)
        Me.pnlDetails.Controls.Add(Me.Label48)
        Me.pnlDetails.Controls.Add(Me.txtSMfee)
        Me.pnlDetails.Controls.Add(Me.txtNSPSfee)
        Me.pnlDetails.Controls.Add(Me.dgvStats)
        Me.pnlDetails.Controls.Add(Me.Label93)
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
        Me.pnlDetails.Controls.Add(Me.txtNSPSExemptReason)
        Me.pnlDetails.Controls.Add(Me.Label83)
        Me.pnlDetails.Controls.Add(Me.txtFeeRate)
        Me.pnlDetails.Controls.Add(Me.Label82)
        Me.pnlDetails.Controls.Add(Me.txtOperate)
        Me.pnlDetails.Controls.Add(Me.Label80)
        Me.pnlDetails.Controls.Add(Me.txtNSPSExempt)
        Me.pnlDetails.Controls.Add(Me.Label78)
        Me.pnlDetails.Controls.Add(Me.Label77)
        Me.pnlDetails.Controls.Add(Me.txtPMTons)
        Me.pnlDetails.Controls.Add(Me.Label76)
        Me.pnlDetails.Controls.Add(Me.txtSO2Tons)
        Me.pnlDetails.Controls.Add(Me.Label71)
        Me.pnlDetails.Controls.Add(Me.txtNOxTons)
        Me.pnlDetails.Controls.Add(Me.Label5)
        Me.pnlDetails.Controls.Add(Me.Label70)
        Me.pnlDetails.Controls.Add(Me.txtPart70MaintenanceFee)
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
        Me.pnlDetails.Size = New System.Drawing.Size(936, 362)
        Me.pnlDetails.TabIndex = 8
        '
        'txtIAIPStatus
        '
        Me.txtIAIPStatus.AcceptsReturn = True
        Me.txtIAIPStatus.Location = New System.Drawing.Point(131, 150)
        Me.txtIAIPStatus.Multiline = True
        Me.txtIAIPStatus.Name = "txtIAIPStatus"
        Me.txtIAIPStatus.ReadOnly = True
        Me.txtIAIPStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtIAIPStatus.Size = New System.Drawing.Size(143, 61)
        Me.txtIAIPStatus.TabIndex = 401
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(33, 150)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(97, 13)
        Me.Label20.TabIndex = 400
        Me.Label20.Text = "IAIP Current Status"
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Location = New System.Drawing.Point(503, 124)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(52, 13)
        Me.Label116.TabIndex = 399
        Me.Label116.Text = "Total Fee"
        '
        'txtAllFees
        '
        Me.txtAllFees.Location = New System.Drawing.Point(559, 120)
        Me.txtAllFees.Name = "txtAllFees"
        Me.txtAllFees.ReadOnly = True
        Me.txtAllFees.Size = New System.Drawing.Size(89, 20)
        Me.txtAllFees.TabIndex = 398
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Location = New System.Drawing.Point(498, 101)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(57, 13)
        Me.Label115.TabIndex = 397
        Me.Label115.Text = "Admin Fee"
        '
        'txtAdminFee
        '
        Me.txtAdminFee.Location = New System.Drawing.Point(559, 97)
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
        Me.txtSMfee.Location = New System.Drawing.Point(559, 50)
        Me.txtSMfee.Name = "txtSMfee"
        Me.txtSMfee.ReadOnly = True
        Me.txtSMfee.Size = New System.Drawing.Size(89, 20)
        Me.txtSMfee.TabIndex = 394
        '
        'txtNSPSfee
        '
        Me.txtNSPSfee.Location = New System.Drawing.Point(559, 74)
        Me.txtNSPSfee.Name = "txtNSPSfee"
        Me.txtNSPSfee.ReadOnly = True
        Me.txtNSPSfee.Size = New System.Drawing.Size(89, 20)
        Me.txtNSPSfee.TabIndex = 393
        '
        'dgvStats
        '
        Me.dgvStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStats.Location = New System.Drawing.Point(106, 222)
        Me.dgvStats.Name = "dgvStats"
        Me.dgvStats.ReadOnly = True
        Me.dgvStats.Size = New System.Drawing.Size(824, 131)
        Me.dgvStats.TabIndex = 59
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(3, 222)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(98, 13)
        Me.Label93.TabIndex = 58
        Me.Label93.Text = "Deposit Information"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(44, 124)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(86, 13)
        Me.Label90.TabIndex = 52
        Me.Label90.Text = "Shut Down Date"
        '
        'txtShutDown
        '
        Me.txtShutDown.Location = New System.Drawing.Point(131, 120)
        Me.txtShutDown.Name = "txtShutDown"
        Me.txtShutDown.ReadOnly = True
        Me.txtShutDown.Size = New System.Drawing.Size(121, 20)
        Me.txtShutDown.TabIndex = 51
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.Location = New System.Drawing.Point(307, 55)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(69, 13)
        Me.Label89.TabIndex = 50
        Me.Label89.Text = "NSPS Status"
        '
        'txtNSPS
        '
        Me.txtNSPS.Location = New System.Drawing.Point(379, 51)
        Me.txtNSPS.Name = "txtNSPS"
        Me.txtNSPS.ReadOnly = True
        Me.txtNSPS.Size = New System.Drawing.Size(53, 20)
        Me.txtNSPS.TabIndex = 49
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(62, 78)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(68, 13)
        Me.Label88.TabIndex = 48
        Me.Label88.Text = "Classification"
        '
        'txtClass
        '
        Me.txtClass.Location = New System.Drawing.Point(131, 74)
        Me.txtClass.Name = "txtClass"
        Me.txtClass.ReadOnly = True
        Me.txtClass.Size = New System.Drawing.Size(53, 20)
        Me.txtClass.TabIndex = 47
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Location = New System.Drawing.Point(298, 193)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(78, 13)
        Me.Label87.TabIndex = 46
        Me.Label87.Text = "Calculated Fee"
        '
        'txtCalculatedFee
        '
        Me.txtCalculatedFee.Location = New System.Drawing.Point(379, 189)
        Me.txtCalculatedFee.Name = "txtCalculatedFee"
        Me.txtCalculatedFee.ReadOnly = True
        Me.txtCalculatedFee.Size = New System.Drawing.Size(109, 20)
        Me.txtCalculatedFee.TabIndex = 45
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Location = New System.Drawing.Point(296, 30)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(80, 13)
        Me.Label86.TabIndex = 44
        Me.Label86.Text = "Synthetic Minor"
        '
        'txtSyntheticMinor
        '
        Me.txtSyntheticMinor.Location = New System.Drawing.Point(379, 27)
        Me.txtSyntheticMinor.Name = "txtSyntheticMinor"
        Me.txtSyntheticMinor.ReadOnly = True
        Me.txtSyntheticMinor.Size = New System.Drawing.Size(37, 20)
        Me.txtSyntheticMinor.TabIndex = 43
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Location = New System.Drawing.Point(335, 8)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(41, 13)
        Me.Label85.TabIndex = 42
        Me.Label85.Text = "Part 70"
        '
        'txtPart70
        '
        Me.txtPart70.Location = New System.Drawing.Point(379, 4)
        Me.txtPart70.Name = "txtPart70"
        Me.txtPart70.ReadOnly = True
        Me.txtPart70.Size = New System.Drawing.Size(37, 20)
        Me.txtPart70.TabIndex = 41
        '
        'txtNSPSExemptReason
        '
        Me.txtNSPSExemptReason.AcceptsReturn = True
        Me.txtNSPSExemptReason.Location = New System.Drawing.Point(654, 74)
        Me.txtNSPSExemptReason.Multiline = True
        Me.txtNSPSExemptReason.Name = "txtNSPSExemptReason"
        Me.txtNSPSExemptReason.ReadOnly = True
        Me.txtNSPSExemptReason.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNSPSExemptReason.Size = New System.Drawing.Size(274, 66)
        Me.txtNSPSExemptReason.TabIndex = 39
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Location = New System.Drawing.Point(325, 170)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(51, 13)
        Me.Label83.TabIndex = 38
        Me.Label83.Text = "Fee Rate"
        '
        'txtFeeRate
        '
        Me.txtFeeRate.Location = New System.Drawing.Point(379, 166)
        Me.txtFeeRate.Name = "txtFeeRate"
        Me.txtFeeRate.ReadOnly = True
        Me.txtFeeRate.Size = New System.Drawing.Size(68, 20)
        Me.txtFeeRate.TabIndex = 37
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Location = New System.Drawing.Point(77, 101)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(53, 13)
        Me.Label82.TabIndex = 36
        Me.Label82.Text = "Operating"
        '
        'txtOperate
        '
        Me.txtOperate.Location = New System.Drawing.Point(131, 97)
        Me.txtOperate.Name = "txtOperate"
        Me.txtOperate.ReadOnly = True
        Me.txtOperate.Size = New System.Drawing.Size(37, 20)
        Me.txtOperate.TabIndex = 35
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(655, 55)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(74, 13)
        Me.Label80.TabIndex = 32
        Me.Label80.Text = "NSPS Exempt"
        '
        'txtNSPSExempt
        '
        Me.txtNSPSExempt.Location = New System.Drawing.Point(732, 51)
        Me.txtNSPSExempt.Name = "txtNSPSExempt"
        Me.txtNSPSExempt.ReadOnly = True
        Me.txtNSPSExempt.Size = New System.Drawing.Size(143, 20)
        Me.txtNSPSExempt.TabIndex = 31
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(499, 78)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(57, 13)
        Me.Label78.TabIndex = 28
        Me.Label78.Text = "NSPS Fee"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(326, 101)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(50, 13)
        Me.Label77.TabIndex = 26
        Me.Label77.Text = "PM Tons"
        '
        'txtPMTons
        '
        Me.txtPMTons.Location = New System.Drawing.Point(379, 97)
        Me.txtPMTons.Name = "txtPMTons"
        Me.txtPMTons.ReadOnly = True
        Me.txtPMTons.Size = New System.Drawing.Size(68, 20)
        Me.txtPMTons.TabIndex = 25
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(321, 124)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(55, 13)
        Me.Label76.TabIndex = 24
        Me.Label76.Text = "SO2 Tons"
        '
        'txtSO2Tons
        '
        Me.txtSO2Tons.Location = New System.Drawing.Point(379, 120)
        Me.txtSO2Tons.Name = "txtSO2Tons"
        Me.txtSO2Tons.ReadOnly = True
        Me.txtSO2Tons.Size = New System.Drawing.Size(68, 20)
        Me.txtSO2Tons.TabIndex = 23
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(321, 147)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(55, 13)
        Me.Label71.TabIndex = 22
        Me.Label71.Text = "NOx Tons"
        '
        'txtNOxTons
        '
        Me.txtNOxTons.Location = New System.Drawing.Point(379, 143)
        Me.txtNOxTons.Name = "txtNOxTons"
        Me.txtNOxTons.ReadOnly = True
        Me.txtNOxTons.Size = New System.Drawing.Size(68, 20)
        Me.txtNOxTons.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(465, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Maintenance Fee"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(488, 8)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(67, 13)
        Me.Label70.TabIndex = 20
        Me.Label70.Text = "Part 70 Fees"
        '
        'txtPart70MaintenanceFee
        '
        Me.txtPart70MaintenanceFee.Location = New System.Drawing.Point(558, 27)
        Me.txtPart70MaintenanceFee.Name = "txtPart70MaintenanceFee"
        Me.txtPart70MaintenanceFee.ReadOnly = True
        Me.txtPart70MaintenanceFee.Size = New System.Drawing.Size(89, 20)
        Me.txtPart70MaintenanceFee.TabIndex = 19
        '
        'txtPart70Fee
        '
        Me.txtPart70Fee.Location = New System.Drawing.Point(558, 4)
        Me.txtPart70Fee.Name = "txtPart70Fee"
        Me.txtPart70Fee.ReadOnly = True
        Me.txtPart70Fee.Size = New System.Drawing.Size(89, 20)
        Me.txtPart70Fee.TabIndex = 19
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(512, 54)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(44, 13)
        Me.Label69.TabIndex = 18
        Me.Label69.Text = "SM Fee"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(320, 78)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(56, 13)
        Me.Label68.TabIndex = 16
        Me.Label68.Text = "VOC Tons"
        '
        'txtVOCTons
        '
        Me.txtVOCTons.Location = New System.Drawing.Point(379, 74)
        Me.txtVOCTons.Name = "txtVOCTons"
        Me.txtVOCTons.ReadOnly = True
        Me.txtVOCTons.Size = New System.Drawing.Size(68, 20)
        Me.txtVOCTons.TabIndex = 15
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(498, 152)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(56, 13)
        Me.Label67.TabIndex = 14
        Me.Label67.Text = "Comments"
        '
        'txtSubmittalComments
        '
        Me.txtSubmittalComments.AcceptsReturn = True
        Me.txtSubmittalComments.AcceptsTab = True
        Me.txtSubmittalComments.Location = New System.Drawing.Point(558, 150)
        Me.txtSubmittalComments.Multiline = True
        Me.txtSubmittalComments.Name = "txtSubmittalComments"
        Me.txtSubmittalComments.ReadOnly = True
        Me.txtSubmittalComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSubmittalComments.Size = New System.Drawing.Size(370, 66)
        Me.txtSubmittalComments.TabIndex = 13
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(50, 55)
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
        Me.Label61.Location = New System.Drawing.Point(47, 30)
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
        Me.Panel1.Controls.Add(Me.lblCountDepositsPayments)
        Me.Panel1.Controls.Add(Me.btnViewInvoicedBalance)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.btnInvoiceReportVariance)
        Me.Panel1.Controls.Add(Me.btnInvoicedPaymentDue)
        Me.Panel1.Controls.Add(Me.txtInvoicedBalance)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.txtTotalPaymentInvoiced)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.btnRunDepositReport)
        Me.Panel1.Controls.Add(Me.Label112)
        Me.Panel1.Controls.Add(Me.Label111)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label110)
        Me.Panel1.Controls.Add(Me.dtpEndDepositDate)
        Me.Panel1.Controls.Add(Me.dtpStartDepositDate)
        Me.Panel1.Controls.Add(Me.chbDepositDateSearch)
        Me.Panel1.Controls.Add(Me.chbNonZeroBalance)
        Me.Panel1.Controls.Add(Me.Label66)
        Me.Panel1.Controls.Add(Me.txtSelectedYear)
        Me.Panel1.Controls.Add(Me.Label65)
        Me.Panel1.Controls.Add(Me.txtSelectedFacilityName)
        Me.Panel1.Controls.Add(Me.Label64)
        Me.Panel1.Controls.Add(Me.txtSelectedAIRSNumber)
        Me.Panel1.Controls.Add(Me.btnViewSelectedFeeData)
        Me.Panel1.Controls.Add(Me.Label56)
        Me.Panel1.Controls.Add(Me.btnViewTotalPaid)
        Me.Panel1.Controls.Add(Me.btnViewPaymentDue)
        Me.Panel1.Controls.Add(Me.txtBalance)
        Me.Panel1.Controls.Add(Me.txtTotalPaid)
        Me.Panel1.Controls.Add(Me.cboStatYear)
        Me.Panel1.Controls.Add(Me.cboStatPayType)
        Me.Panel1.Controls.Add(Me.txtTotalPaymentDue)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(936, 163)
        Me.Panel1.TabIndex = 6
        '
        'btnViewInvoicedBalance
        '
        Me.btnViewInvoicedBalance.Location = New System.Drawing.Point(838, 68)
        Me.btnViewInvoicedBalance.Name = "btnViewInvoicedBalance"
        Me.btnViewInvoicedBalance.Size = New System.Drawing.Size(75, 23)
        Me.btnViewInvoicedBalance.TabIndex = 414
        Me.btnViewInvoicedBalance.Text = "View"
        Me.btnViewInvoicedBalance.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(741, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 13)
        Me.Label19.TabIndex = 413
        Me.Label19.Text = "Balance"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(620, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(28, 13)
        Me.Label18.TabIndex = 412
        Me.Label18.Text = "Paid"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(457, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(27, 13)
        Me.Label17.TabIndex = 411
        Me.Label17.Text = "Due"
        '
        'btnInvoiceReportVariance
        '
        Me.btnInvoiceReportVariance.AutoSize = True
        Me.btnInvoiceReportVariance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnInvoiceReportVariance.Location = New System.Drawing.Point(460, 67)
        Me.btnInvoiceReportVariance.Name = "btnInvoiceReportVariance"
        Me.btnInvoiceReportVariance.Size = New System.Drawing.Size(59, 23)
        Me.btnInvoiceReportVariance.TabIndex = 410
        Me.btnInvoiceReportVariance.Text = "Variance"
        Me.btnInvoiceReportVariance.UseVisualStyleBackColor = True
        '
        'btnInvoicedPaymentDue
        '
        Me.btnInvoicedPaymentDue.AutoSize = True
        Me.btnInvoicedPaymentDue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnInvoicedPaymentDue.Location = New System.Drawing.Point(554, 43)
        Me.btnInvoicedPaymentDue.Name = "btnInvoicedPaymentDue"
        Me.btnInvoicedPaymentDue.Size = New System.Drawing.Size(40, 23)
        Me.btnInvoicedPaymentDue.TabIndex = 409
        Me.btnInvoicedPaymentDue.Text = "View"
        Me.btnInvoicedPaymentDue.UseVisualStyleBackColor = True
        '
        'txtInvoicedBalance
        '
        Me.txtInvoicedBalance.Location = New System.Drawing.Point(744, 44)
        Me.txtInvoicedBalance.Name = "txtInvoicedBalance"
        Me.txtInvoicedBalance.ReadOnly = True
        Me.txtInvoicedBalance.Size = New System.Drawing.Size(88, 20)
        Me.txtInvoicedBalance.TabIndex = 408
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(406, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 13)
        Me.Label16.TabIndex = 407
        Me.Label16.Text = "Invoiced"
        '
        'txtTotalPaymentInvoiced
        '
        Me.txtTotalPaymentInvoiced.Location = New System.Drawing.Point(460, 44)
        Me.txtTotalPaymentInvoiced.Name = "txtTotalPaymentInvoiced"
        Me.txtTotalPaymentInvoiced.ReadOnly = True
        Me.txtTotalPaymentInvoiced.Size = New System.Drawing.Size(88, 20)
        Me.txtTotalPaymentInvoiced.TabIndex = 406
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(388, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 13)
        Me.Label8.TabIndex = 405
        Me.Label8.Text = "As Reported"
        '
        'btnRunDepositReport
        '
        Me.btnRunDepositReport.AutoSize = True
        Me.btnRunDepositReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunDepositReport.Location = New System.Drawing.Point(311, 68)
        Me.btnRunDepositReport.Name = "btnRunDepositReport"
        Me.btnRunDepositReport.Size = New System.Drawing.Size(72, 23)
        Me.btnRunDepositReport.TabIndex = 404
        Me.btnRunDepositReport.Text = "Run Report"
        Me.btnRunDepositReport.UseVisualStyleBackColor = True
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.Location = New System.Drawing.Point(202, 55)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(52, 13)
        Me.Label112.TabIndex = 403
        Me.Label112.Text = "End Date"
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.Location = New System.Drawing.Point(96, 54)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(55, 13)
        Me.Label111.TabIndex = 402
        Me.Label111.Text = "Start Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(149, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 401
        Me.Label6.Text = "Payment Type"
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Location = New System.Drawing.Point(14, 4)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(50, 13)
        Me.Label110.TabIndex = 401
        Me.Label110.Text = "Fee Year"
        '
        'dtpEndDepositDate
        '
        Me.dtpEndDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDepositDate.Location = New System.Drawing.Point(205, 70)
        Me.dtpEndDepositDate.Name = "dtpEndDepositDate"
        Me.dtpEndDepositDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpEndDepositDate.TabIndex = 400
        '
        'dtpStartDepositDate
        '
        Me.dtpStartDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpStartDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDepositDate.Location = New System.Drawing.Point(99, 70)
        Me.dtpStartDepositDate.Name = "dtpStartDepositDate"
        Me.dtpStartDepositDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpStartDepositDate.TabIndex = 399
        '
        'chbDepositDateSearch
        '
        Me.chbDepositDateSearch.AutoSize = True
        Me.chbDepositDateSearch.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbDepositDateSearch.Location = New System.Drawing.Point(13, 72)
        Me.chbDepositDateSearch.Name = "chbDepositDateSearch"
        Me.chbDepositDateSearch.Size = New System.Drawing.Size(84, 30)
        Me.chbDepositDateSearch.TabIndex = 28
        Me.chbDepositDateSearch.Text = "Use Deposit" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      Dates"
        Me.chbDepositDateSearch.UseVisualStyleBackColor = True
        '
        'chbNonZeroBalance
        '
        Me.chbNonZeroBalance.AutoSize = True
        Me.chbNonZeroBalance.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbNonZeroBalance.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbNonZeroBalance.Location = New System.Drawing.Point(744, 70)
        Me.chbNonZeroBalance.Name = "chbNonZeroBalance"
        Me.chbNonZeroBalance.Size = New System.Drawing.Size(93, 30)
        Me.chbNonZeroBalance.TabIndex = 26
        Me.chbNonZeroBalance.Text = "Only Non-zero" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Balance"
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
        Me.Label64.Location = New System.Drawing.Point(8, 138)
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
        Me.btnViewSelectedFeeData.Location = New System.Drawing.Point(645, 133)
        Me.btnViewSelectedFeeData.Name = "btnViewSelectedFeeData"
        Me.btnViewSelectedFeeData.Size = New System.Drawing.Size(84, 23)
        Me.btnViewSelectedFeeData.TabIndex = 19
        Me.btnViewSelectedFeeData.Text = "View Details ↓"
        Me.btnViewSelectedFeeData.UseVisualStyleBackColor = True
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(14, 117)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(479, 13)
        Me.Label56.TabIndex = 18
        Me.Label56.Text = "Notice - AMENDMENT, ONE-TIME, and REFUND will all be summed up under Total Paymen" &
    "t Due. "
        '
        'btnViewTotalPaid
        '
        Me.btnViewTotalPaid.AutoSize = True
        Me.btnViewTotalPaid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewTotalPaid.Location = New System.Drawing.Point(623, 43)
        Me.btnViewTotalPaid.Name = "btnViewTotalPaid"
        Me.btnViewTotalPaid.Size = New System.Drawing.Size(91, 23)
        Me.btnViewTotalPaid.TabIndex = 14
        Me.btnViewTotalPaid.Text = "View Total Paid"
        Me.btnViewTotalPaid.UseVisualStyleBackColor = True
        '
        'btnViewPaymentDue
        '
        Me.btnViewPaymentDue.AutoSize = True
        Me.btnViewPaymentDue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewPaymentDue.Location = New System.Drawing.Point(554, 18)
        Me.btnViewPaymentDue.Name = "btnViewPaymentDue"
        Me.btnViewPaymentDue.Size = New System.Drawing.Size(40, 23)
        Me.btnViewPaymentDue.TabIndex = 13
        Me.btnViewPaymentDue.Text = "View"
        Me.btnViewPaymentDue.UseVisualStyleBackColor = True
        '
        'txtBalance
        '
        Me.txtBalance.Location = New System.Drawing.Point(744, 19)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(88, 20)
        Me.txtBalance.TabIndex = 11
        '
        'txtTotalPaid
        '
        Me.txtTotalPaid.Location = New System.Drawing.Point(623, 20)
        Me.txtTotalPaid.Name = "txtTotalPaid"
        Me.txtTotalPaid.ReadOnly = True
        Me.txtTotalPaid.Size = New System.Drawing.Size(88, 20)
        Me.txtTotalPaid.TabIndex = 10
        '
        'cboStatYear
        '
        Me.cboStatYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatYear.FormattingEnabled = True
        Me.cboStatYear.Location = New System.Drawing.Point(13, 20)
        Me.cboStatYear.Name = "cboStatYear"
        Me.cboStatYear.Size = New System.Drawing.Size(121, 21)
        Me.cboStatYear.TabIndex = 0
        '
        'cboStatPayType
        '
        Me.cboStatPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatPayType.FormattingEnabled = True
        Me.cboStatPayType.Location = New System.Drawing.Point(148, 20)
        Me.cboStatPayType.Name = "cboStatPayType"
        Me.cboStatPayType.Size = New System.Drawing.Size(144, 21)
        Me.cboStatPayType.TabIndex = 4
        '
        'txtTotalPaymentDue
        '
        Me.txtTotalPaymentDue.Location = New System.Drawing.Point(460, 19)
        Me.txtTotalPaymentDue.Name = "txtTotalPaymentDue"
        Me.txtTotalPaymentDue.ReadOnly = True
        Me.txtTotalPaymentDue.Size = New System.Drawing.Size(88, 20)
        Me.txtTotalPaymentDue.TabIndex = 3
        '
        'TPFeeStatistics2
        '
        Me.TPFeeStatistics2.Controls.Add(Me.GroupBox1)
        Me.TPFeeStatistics2.Controls.Add(Me.Panel10)
        Me.TPFeeStatistics2.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeStatistics2.Name = "TPFeeStatistics2"
        Me.TPFeeStatistics2.Size = New System.Drawing.Size(936, 692)
        Me.TPFeeStatistics2.TabIndex = 7
        Me.TPFeeStatistics2.Text = "Fee Statistics"
        Me.TPFeeStatistics2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvFeeStats)
        Me.GroupBox1.Controls.Add(Me.Panel11)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(478, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(458, 692)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View Data"
        '
        'dgvFeeStats
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvFeeStats.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvFeeStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvFeeStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFeeStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFeeStats.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvFeeStats.LinkifyColumnByName = Nothing
        Me.dgvFeeStats.LinkifyFirstColumn = True
        Me.dgvFeeStats.Location = New System.Drawing.Point(3, 57)
        Me.dgvFeeStats.Name = "dgvFeeStats"
        Me.dgvFeeStats.ResultsCountLabel = Me.lblCountFeeStats
        Me.dgvFeeStats.ResultsCountLabelFormat = "{0} found"
        Me.dgvFeeStats.Size = New System.Drawing.Size(452, 632)
        Me.dgvFeeStats.StandardTab = True
        Me.dgvFeeStats.TabIndex = 1
        '
        'lblCountFeeStats
        '
        Me.lblCountFeeStats.AutoSize = True
        Me.lblCountFeeStats.Location = New System.Drawing.Point(193, 11)
        Me.lblCountFeeStats.Name = "lblCountFeeStats"
        Me.lblCountFeeStats.Size = New System.Drawing.Size(43, 13)
        Me.lblCountFeeStats.TabIndex = 122
        Me.lblCountFeeStats.Text = "0 found"
        Me.lblCountFeeStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.FeeStatAirsNumberEntry)
        Me.Panel11.Controls.Add(Me.btnOpenFeesLog)
        Me.Panel11.Controls.Add(Me.lblCountFeeStats)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(3, 16)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(452, 41)
        Me.Panel11.TabIndex = 0
        '
        'FeeStatAirsNumberEntry
        '
        Me.FeeStatAirsNumberEntry.AirsNumber = Nothing
        Me.FeeStatAirsNumberEntry.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.FeeStatAirsNumberEntry.ErrorMessageLabel = Nothing
        Me.FeeStatAirsNumberEntry.FacilityMustExist = True
        Me.FeeStatAirsNumberEntry.Location = New System.Drawing.Point(3, 9)
        Me.FeeStatAirsNumberEntry.Name = "FeeStatAirsNumberEntry"
        Me.FeeStatAirsNumberEntry.ReadOnly = False
        Me.FeeStatAirsNumberEntry.Size = New System.Drawing.Size(83, 20)
        Me.FeeStatAirsNumberEntry.TabIndex = 125
        Me.FeeStatAirsNumberEntry.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'btnOpenFeesLog
        '
        Me.btnOpenFeesLog.Location = New System.Drawing.Point(92, 6)
        Me.btnOpenFeesLog.Name = "btnOpenFeesLog"
        Me.btnOpenFeesLog.Size = New System.Drawing.Size(101, 23)
        Me.btnOpenFeesLog.TabIndex = 124
        Me.btnOpenFeesLog.Text = "Open Fees Log"
        Me.btnOpenFeesLog.UseVisualStyleBackColor = True
        Me.btnOpenFeesLog.Visible = False
        '
        'Panel10
        '
        Me.Panel10.AutoScroll = True
        Me.Panel10.Controls.Add(Me.btnCheckInvoices)
        Me.Panel10.Controls.Add(Me.llbFSSummaryPaidNotFinalized)
        Me.Panel10.Controls.Add(Me.Label60)
        Me.Panel10.Controls.Add(Me.txtFSPaidNotFinalized)
        Me.Panel10.Controls.Add(Me.llbDetailPaidNotFinalized)
        Me.Panel10.Controls.Add(Me.llbFSSummaryPaidFinalized)
        Me.Panel10.Controls.Add(Me.Label72)
        Me.Panel10.Controls.Add(Me.txtFSPaidFinalized)
        Me.Panel10.Controls.Add(Me.llbDetailPaidFinalized)
        Me.Panel10.Controls.Add(Me.Label59)
        Me.Panel10.Controls.Add(Me.llbFSSummaryLateWithFee)
        Me.Panel10.Controls.Add(Me.Label15)
        Me.Panel10.Controls.Add(Me.txtFSLateFee)
        Me.Panel10.Controls.Add(Me.llbDetailLateWithFee)
        Me.Panel10.Controls.Add(Me.llbFSSummaryLateResponse)
        Me.Panel10.Controls.Add(Me.Label55)
        Me.Panel10.Controls.Add(Me.txtFSOnTimeResponse)
        Me.Panel10.Controls.Add(Me.Label57)
        Me.Panel10.Controls.Add(Me.txtFSLateResponse)
        Me.Panel10.Controls.Add(Me.llbFSDetailOnTime)
        Me.Panel10.Controls.Add(Me.llbFSSummaryOnTime)
        Me.Panel10.Controls.Add(Me.llbDetailLateResponse)
        Me.Panel10.Controls.Add(Me.Label58)
        Me.Panel10.Controls.Add(Me.Label54)
        Me.Panel10.Controls.Add(Me.txtFSQuarterly)
        Me.Panel10.Controls.Add(Me.Label53)
        Me.Panel10.Controls.Add(Me.txtFSAnnual)
        Me.Panel10.Controls.Add(Me.Label52)
        Me.Panel10.Controls.Add(Me.txtFSOverPaid)
        Me.Panel10.Controls.Add(Me.Label51)
        Me.Panel10.Controls.Add(Me.txtFSPartial)
        Me.Panel10.Controls.Add(Me.llbFSSummaryPaidInFull)
        Me.Panel10.Controls.Add(Me.Label31)
        Me.Panel10.Controls.Add(Me.txtFSPaidInFull)
        Me.Panel10.Controls.Add(Me.llbDetailPaidInFull)
        Me.Panel10.Controls.Add(Me.llbFSSummaryOutofBalance)
        Me.Panel10.Controls.Add(Me.Label33)
        Me.Panel10.Controls.Add(Me.txtFSOutOfBalance)
        Me.Panel10.Controls.Add(Me.llbDetailOutOfBalance)
        Me.Panel10.Controls.Add(Me.llbFSSummaryNotPaid)
        Me.Panel10.Controls.Add(Me.Label39)
        Me.Panel10.Controls.Add(Me.txtFSNotPaid)
        Me.Panel10.Controls.Add(Me.llbDetailNotPaid)
        Me.Panel10.Controls.Add(Me.llbFSSummaryFinalized)
        Me.Panel10.Controls.Add(Me.Label30)
        Me.Panel10.Controls.Add(Me.txtFSFinalized)
        Me.Panel10.Controls.Add(Me.llbDetailFinalized)
        Me.Panel10.Controls.Add(Me.llbFSSummaryInProgress)
        Me.Panel10.Controls.Add(Me.Label29)
        Me.Panel10.Controls.Add(Me.txtFSInProgress)
        Me.Panel10.Controls.Add(Me.llbDetailInProgress)
        Me.Panel10.Controls.Add(Me.llbFSSummaryNotReported)
        Me.Panel10.Controls.Add(Me.Label28)
        Me.Panel10.Controls.Add(Me.txtFSNotReported)
        Me.Panel10.Controls.Add(Me.llbDetailNotReported)
        Me.Panel10.Controls.Add(Me.Label24)
        Me.Panel10.Controls.Add(Me.llbFSSummaryAdditions)
        Me.Panel10.Controls.Add(Me.Label23)
        Me.Panel10.Controls.Add(Me.txtFSAdditions)
        Me.Panel10.Controls.Add(Me.llbDetailAdditions)
        Me.Panel10.Controls.Add(Me.llbFSSummaryMailOut)
        Me.Panel10.Controls.Add(Me.Label22)
        Me.Panel10.Controls.Add(Me.txtFSMailout)
        Me.Panel10.Controls.Add(Me.llbDetailMailout)
        Me.Panel10.Controls.Add(Me.llbFSSummaryEnrolled)
        Me.Panel10.Controls.Add(Me.Label14)
        Me.Panel10.Controls.Add(Me.txtFSEnrolled)
        Me.Panel10.Controls.Add(Me.llbDetailEnrolled)
        Me.Panel10.Controls.Add(Me.llbFSSummaryCeaseCollection)
        Me.Panel10.Controls.Add(Me.Label13)
        Me.Panel10.Controls.Add(Me.txtFSCeaseCollection)
        Me.Panel10.Controls.Add(Me.llbDetailCeaseCollection)
        Me.Panel10.Controls.Add(Me.llbFSSummaryUnEnrolled)
        Me.Panel10.Controls.Add(Me.Label12)
        Me.Panel10.Controls.Add(Me.txtFSUnEnrolled)
        Me.Panel10.Controls.Add(Me.llbDetailUnEnrolled)
        Me.Panel10.Controls.Add(Me.llbFSSummaryFeeUniverse)
        Me.Panel10.Controls.Add(Me.Label11)
        Me.Panel10.Controls.Add(Me.txtFSFeeUniverse)
        Me.Panel10.Controls.Add(Me.llbDetailFeeUniverse)
        Me.Panel10.Controls.Add(Me.Label7)
        Me.Panel10.Controls.Add(Me.Label9)
        Me.Panel10.Controls.Add(Me.cboFeeStatYear)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(478, 692)
        Me.Panel10.TabIndex = 0
        '
        'btnCheckInvoices
        '
        Me.btnCheckInvoices.AutoSize = True
        Me.btnCheckInvoices.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCheckInvoices.Location = New System.Drawing.Point(23, 478)
        Me.btnCheckInvoices.Name = "btnCheckInvoices"
        Me.btnCheckInvoices.Size = New System.Drawing.Size(124, 23)
        Me.btnCheckInvoices.TabIndex = 36
        Me.btnCheckInvoices.Text = "Revalidate all Invoices"
        Me.btnCheckInvoices.UseVisualStyleBackColor = True
        '
        'llbFSSummaryPaidNotFinalized
        '
        Me.llbFSSummaryPaidNotFinalized.AutoSize = True
        Me.llbFSSummaryPaidNotFinalized.Location = New System.Drawing.Point(328, 743)
        Me.llbFSSummaryPaidNotFinalized.Name = "llbFSSummaryPaidNotFinalized"
        Me.llbFSSummaryPaidNotFinalized.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryPaidNotFinalized.TabIndex = 54
        Me.llbFSSummaryPaidNotFinalized.TabStop = True
        Me.llbFSSummaryPaidNotFinalized.Text = "Summary"
        Me.llbFSSummaryPaidNotFinalized.UseCompatibleTextRendering = True
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(106, 745)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(68, 13)
        Me.Label60.TabIndex = 217
        Me.Label60.Text = "Not Finalized"
        '
        'txtFSPaidNotFinalized
        '
        Me.txtFSPaidNotFinalized.Location = New System.Drawing.Point(180, 741)
        Me.txtFSPaidNotFinalized.Name = "txtFSPaidNotFinalized"
        Me.txtFSPaidNotFinalized.Size = New System.Drawing.Size(100, 20)
        Me.txtFSPaidNotFinalized.TabIndex = 53
        '
        'llbDetailPaidNotFinalized
        '
        Me.llbDetailPaidNotFinalized.AutoSize = True
        Me.llbDetailPaidNotFinalized.Location = New System.Drawing.Point(413, 743)
        Me.llbDetailPaidNotFinalized.Name = "llbDetailPaidNotFinalized"
        Me.llbDetailPaidNotFinalized.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailPaidNotFinalized.TabIndex = 55
        Me.llbDetailPaidNotFinalized.TabStop = True
        Me.llbDetailPaidNotFinalized.Text = "Detail"
        Me.llbDetailPaidNotFinalized.UseCompatibleTextRendering = True
        '
        'llbFSSummaryPaidFinalized
        '
        Me.llbFSSummaryPaidFinalized.AutoSize = True
        Me.llbFSSummaryPaidFinalized.Location = New System.Drawing.Point(328, 717)
        Me.llbFSSummaryPaidFinalized.Name = "llbFSSummaryPaidFinalized"
        Me.llbFSSummaryPaidFinalized.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryPaidFinalized.TabIndex = 51
        Me.llbFSSummaryPaidFinalized.TabStop = True
        Me.llbFSSummaryPaidFinalized.Text = "Summary"
        Me.llbFSSummaryPaidFinalized.UseCompatibleTextRendering = True
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(126, 719)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(48, 13)
        Me.Label72.TabIndex = 213
        Me.Label72.Text = "Finalized"
        '
        'txtFSPaidFinalized
        '
        Me.txtFSPaidFinalized.Location = New System.Drawing.Point(180, 715)
        Me.txtFSPaidFinalized.Name = "txtFSPaidFinalized"
        Me.txtFSPaidFinalized.Size = New System.Drawing.Size(100, 20)
        Me.txtFSPaidFinalized.TabIndex = 50
        '
        'llbDetailPaidFinalized
        '
        Me.llbDetailPaidFinalized.AutoSize = True
        Me.llbDetailPaidFinalized.Location = New System.Drawing.Point(413, 717)
        Me.llbDetailPaidFinalized.Name = "llbDetailPaidFinalized"
        Me.llbDetailPaidFinalized.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailPaidFinalized.TabIndex = 52
        Me.llbDetailPaidFinalized.TabStop = True
        Me.llbDetailPaidFinalized.Text = "Detail"
        Me.llbDetailPaidFinalized.UseCompatibleTextRendering = True
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(3, 793)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(10, 13)
        Me.Label59.TabIndex = 209
        Me.Label59.Text = " "
        '
        'llbFSSummaryLateWithFee
        '
        Me.llbFSSummaryLateWithFee.AutoSize = True
        Me.llbFSSummaryLateWithFee.Location = New System.Drawing.Point(328, 460)
        Me.llbFSSummaryLateWithFee.Name = "llbFSSummaryLateWithFee"
        Me.llbFSSummaryLateWithFee.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryLateWithFee.TabIndex = 34
        Me.llbFSSummaryLateWithFee.TabStop = True
        Me.llbFSSummaryLateWithFee.Text = "Summary"
        Me.llbFSSummaryLateWithFee.UseCompatibleTextRendering = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(74, 462)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 13)
        Me.Label15.TabIndex = 208
        Me.Label15.Text = "Late w/ Admin Fee:"
        '
        'txtFSLateFee
        '
        Me.txtFSLateFee.Location = New System.Drawing.Point(180, 458)
        Me.txtFSLateFee.Name = "txtFSLateFee"
        Me.txtFSLateFee.Size = New System.Drawing.Size(100, 20)
        Me.txtFSLateFee.TabIndex = 33
        '
        'llbDetailLateWithFee
        '
        Me.llbDetailLateWithFee.AutoSize = True
        Me.llbDetailLateWithFee.Location = New System.Drawing.Point(413, 460)
        Me.llbDetailLateWithFee.Name = "llbDetailLateWithFee"
        Me.llbDetailLateWithFee.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailLateWithFee.TabIndex = 35
        Me.llbDetailLateWithFee.TabStop = True
        Me.llbDetailLateWithFee.Text = "Detail"
        Me.llbDetailLateWithFee.UseCompatibleTextRendering = True
        '
        'llbFSSummaryLateResponse
        '
        Me.llbFSSummaryLateResponse.AutoSize = True
        Me.llbFSSummaryLateResponse.Location = New System.Drawing.Point(328, 434)
        Me.llbFSSummaryLateResponse.Name = "llbFSSummaryLateResponse"
        Me.llbFSSummaryLateResponse.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryLateResponse.TabIndex = 31
        Me.llbFSSummaryLateResponse.TabStop = True
        Me.llbFSSummaryLateResponse.Text = "Summary"
        Me.llbFSSummaryLateResponse.UseCompatibleTextRendering = True
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(73, 412)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(101, 13)
        Me.Label55.TabIndex = 203
        Me.Label55.Text = "On Time Response:"
        '
        'txtFSOnTimeResponse
        '
        Me.txtFSOnTimeResponse.Location = New System.Drawing.Point(180, 408)
        Me.txtFSOnTimeResponse.Name = "txtFSOnTimeResponse"
        Me.txtFSOnTimeResponse.Size = New System.Drawing.Size(100, 20)
        Me.txtFSOnTimeResponse.TabIndex = 28
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(92, 436)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(82, 13)
        Me.Label57.TabIndex = 204
        Me.Label57.Text = "Late Response:"
        '
        'txtFSLateResponse
        '
        Me.txtFSLateResponse.Location = New System.Drawing.Point(180, 432)
        Me.txtFSLateResponse.Name = "txtFSLateResponse"
        Me.txtFSLateResponse.Size = New System.Drawing.Size(100, 20)
        Me.txtFSLateResponse.TabIndex = 30
        '
        'llbFSDetailOnTime
        '
        Me.llbFSDetailOnTime.AutoSize = True
        Me.llbFSDetailOnTime.Location = New System.Drawing.Point(413, 410)
        Me.llbFSDetailOnTime.Name = "llbFSDetailOnTime"
        Me.llbFSDetailOnTime.Size = New System.Drawing.Size(33, 17)
        Me.llbFSDetailOnTime.TabIndex = 29
        Me.llbFSDetailOnTime.TabStop = True
        Me.llbFSDetailOnTime.Text = "Detail"
        Me.llbFSDetailOnTime.UseCompatibleTextRendering = True
        '
        'llbFSSummaryOnTime
        '
        Me.llbFSSummaryOnTime.AutoSize = True
        Me.llbFSSummaryOnTime.Location = New System.Drawing.Point(328, 410)
        Me.llbFSSummaryOnTime.Name = "llbFSSummaryOnTime"
        Me.llbFSSummaryOnTime.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryOnTime.TabIndex = 29
        Me.llbFSSummaryOnTime.TabStop = True
        Me.llbFSSummaryOnTime.Text = "Summary"
        Me.llbFSSummaryOnTime.UseCompatibleTextRendering = True
        '
        'llbDetailLateResponse
        '
        Me.llbDetailLateResponse.AutoSize = True
        Me.llbDetailLateResponse.Location = New System.Drawing.Point(413, 434)
        Me.llbDetailLateResponse.Name = "llbDetailLateResponse"
        Me.llbDetailLateResponse.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailLateResponse.TabIndex = 32
        Me.llbDetailLateResponse.TabStop = True
        Me.llbDetailLateResponse.Text = "Detail"
        Me.llbDetailLateResponse.UseCompatibleTextRendering = True
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(20, 388)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(89, 13)
        Me.Label58.TabIndex = 202
        Me.Label58.Text = "Timeliness Status"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(168, 625)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(49, 13)
        Me.Label54.TabIndex = 196
        Me.Label54.Text = "Quarterly"
        '
        'txtFSQuarterly
        '
        Me.txtFSQuarterly.Location = New System.Drawing.Point(222, 621)
        Me.txtFSQuarterly.Name = "txtFSQuarterly"
        Me.txtFSQuarterly.Size = New System.Drawing.Size(100, 20)
        Me.txtFSQuarterly.TabIndex = 45
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(177, 599)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(40, 13)
        Me.Label53.TabIndex = 192
        Me.Label53.Text = "Annual"
        '
        'txtFSAnnual
        '
        Me.txtFSAnnual.Location = New System.Drawing.Point(222, 595)
        Me.txtFSAnnual.Name = "txtFSAnnual"
        Me.txtFSAnnual.Size = New System.Drawing.Size(100, 20)
        Me.txtFSAnnual.TabIndex = 44
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(124, 658)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(50, 13)
        Me.Label52.TabIndex = 188
        Me.Label52.Text = "Overpaid"
        '
        'txtFSOverPaid
        '
        Me.txtFSOverPaid.Location = New System.Drawing.Point(180, 654)
        Me.txtFSOverPaid.Name = "txtFSOverPaid"
        Me.txtFSOverPaid.Size = New System.Drawing.Size(100, 20)
        Me.txtFSOverPaid.TabIndex = 46
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(138, 567)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(36, 13)
        Me.Label51.TabIndex = 184
        Me.Label51.Text = "Partial"
        '
        'txtFSPartial
        '
        Me.txtFSPartial.Location = New System.Drawing.Point(180, 563)
        Me.txtFSPartial.Name = "txtFSPartial"
        Me.txtFSPartial.Size = New System.Drawing.Size(100, 20)
        Me.txtFSPartial.TabIndex = 43
        '
        'llbFSSummaryPaidInFull
        '
        Me.llbFSSummaryPaidInFull.AutoSize = True
        Me.llbFSSummaryPaidInFull.Location = New System.Drawing.Point(328, 689)
        Me.llbFSSummaryPaidInFull.Name = "llbFSSummaryPaidInFull"
        Me.llbFSSummaryPaidInFull.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryPaidInFull.TabIndex = 48
        Me.llbFSSummaryPaidInFull.TabStop = True
        Me.llbFSSummaryPaidInFull.Text = "Summary"
        Me.llbFSSummaryPaidInFull.UseCompatibleTextRendering = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(79, 691)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(58, 13)
        Me.Label31.TabIndex = 180
        Me.Label31.Text = "Paid in Full"
        '
        'txtFSPaidInFull
        '
        Me.txtFSPaidInFull.Location = New System.Drawing.Point(143, 687)
        Me.txtFSPaidInFull.Name = "txtFSPaidInFull"
        Me.txtFSPaidInFull.Size = New System.Drawing.Size(100, 20)
        Me.txtFSPaidInFull.TabIndex = 47
        '
        'llbDetailPaidInFull
        '
        Me.llbDetailPaidInFull.AutoSize = True
        Me.llbDetailPaidInFull.Location = New System.Drawing.Point(413, 689)
        Me.llbDetailPaidInFull.Name = "llbDetailPaidInFull"
        Me.llbDetailPaidInFull.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailPaidInFull.TabIndex = 49
        Me.llbDetailPaidInFull.TabStop = True
        Me.llbDetailPaidInFull.Text = "Detail"
        Me.llbDetailPaidInFull.UseCompatibleTextRendering = True
        '
        'llbFSSummaryOutofBalance
        '
        Me.llbFSSummaryOutofBalance.AutoSize = True
        Me.llbFSSummaryOutofBalance.Location = New System.Drawing.Point(328, 535)
        Me.llbFSSummaryOutofBalance.Name = "llbFSSummaryOutofBalance"
        Me.llbFSSummaryOutofBalance.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryOutofBalance.TabIndex = 41
        Me.llbFSSummaryOutofBalance.TabStop = True
        Me.llbFSSummaryOutofBalance.Text = "Summary"
        Me.llbFSSummaryOutofBalance.UseCompatibleTextRendering = True
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(59, 537)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(78, 13)
        Me.Label33.TabIndex = 176
        Me.Label33.Text = "Out of Balance"
        '
        'txtFSOutOfBalance
        '
        Me.txtFSOutOfBalance.Location = New System.Drawing.Point(143, 533)
        Me.txtFSOutOfBalance.Name = "txtFSOutOfBalance"
        Me.txtFSOutOfBalance.Size = New System.Drawing.Size(100, 20)
        Me.txtFSOutOfBalance.TabIndex = 40
        '
        'llbDetailOutOfBalance
        '
        Me.llbDetailOutOfBalance.AutoSize = True
        Me.llbDetailOutOfBalance.Location = New System.Drawing.Point(413, 535)
        Me.llbDetailOutOfBalance.Name = "llbDetailOutOfBalance"
        Me.llbDetailOutOfBalance.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailOutOfBalance.TabIndex = 42
        Me.llbDetailOutOfBalance.TabStop = True
        Me.llbDetailOutOfBalance.Text = "Detail"
        Me.llbDetailOutOfBalance.UseCompatibleTextRendering = True
        '
        'llbFSSummaryNotPaid
        '
        Me.llbFSSummaryNotPaid.AutoSize = True
        Me.llbFSSummaryNotPaid.Location = New System.Drawing.Point(328, 509)
        Me.llbFSSummaryNotPaid.Name = "llbFSSummaryNotPaid"
        Me.llbFSSummaryNotPaid.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryNotPaid.TabIndex = 38
        Me.llbFSSummaryNotPaid.TabStop = True
        Me.llbFSSummaryNotPaid.Text = "Summary"
        Me.llbFSSummaryNotPaid.UseCompatibleTextRendering = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(89, 511)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(48, 13)
        Me.Label39.TabIndex = 172
        Me.Label39.Text = "Not Paid"
        '
        'txtFSNotPaid
        '
        Me.txtFSNotPaid.Location = New System.Drawing.Point(143, 507)
        Me.txtFSNotPaid.Name = "txtFSNotPaid"
        Me.txtFSNotPaid.Size = New System.Drawing.Size(100, 20)
        Me.txtFSNotPaid.TabIndex = 37
        '
        'llbDetailNotPaid
        '
        Me.llbDetailNotPaid.AutoSize = True
        Me.llbDetailNotPaid.Location = New System.Drawing.Point(413, 509)
        Me.llbDetailNotPaid.Name = "llbDetailNotPaid"
        Me.llbDetailNotPaid.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailNotPaid.TabIndex = 39
        Me.llbDetailNotPaid.TabStop = True
        Me.llbDetailNotPaid.Text = "Detail"
        Me.llbDetailNotPaid.UseCompatibleTextRendering = True
        '
        'llbFSSummaryFinalized
        '
        Me.llbFSSummaryFinalized.AutoSize = True
        Me.llbFSSummaryFinalized.Location = New System.Drawing.Point(328, 356)
        Me.llbFSSummaryFinalized.Name = "llbFSSummaryFinalized"
        Me.llbFSSummaryFinalized.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryFinalized.TabIndex = 26
        Me.llbFSSummaryFinalized.TabStop = True
        Me.llbFSSummaryFinalized.Text = "Summary"
        Me.llbFSSummaryFinalized.UseCompatibleTextRendering = True
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(89, 358)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 13)
        Me.Label30.TabIndex = 167
        Me.Label30.Text = "Finalized"
        '
        'txtFSFinalized
        '
        Me.txtFSFinalized.Location = New System.Drawing.Point(143, 354)
        Me.txtFSFinalized.Name = "txtFSFinalized"
        Me.txtFSFinalized.Size = New System.Drawing.Size(100, 20)
        Me.txtFSFinalized.TabIndex = 25
        '
        'llbDetailFinalized
        '
        Me.llbDetailFinalized.AutoSize = True
        Me.llbDetailFinalized.Location = New System.Drawing.Point(413, 356)
        Me.llbDetailFinalized.Name = "llbDetailFinalized"
        Me.llbDetailFinalized.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailFinalized.TabIndex = 27
        Me.llbDetailFinalized.TabStop = True
        Me.llbDetailFinalized.Text = "Detail"
        Me.llbDetailFinalized.UseCompatibleTextRendering = True
        '
        'llbFSSummaryInProgress
        '
        Me.llbFSSummaryInProgress.AutoSize = True
        Me.llbFSSummaryInProgress.Location = New System.Drawing.Point(328, 330)
        Me.llbFSSummaryInProgress.Name = "llbFSSummaryInProgress"
        Me.llbFSSummaryInProgress.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryInProgress.TabIndex = 23
        Me.llbFSSummaryInProgress.TabStop = True
        Me.llbFSSummaryInProgress.Text = "Summary"
        Me.llbFSSummaryInProgress.UseCompatibleTextRendering = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(77, 332)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(60, 13)
        Me.Label29.TabIndex = 163
        Me.Label29.Text = "In Progress"
        '
        'txtFSInProgress
        '
        Me.txtFSInProgress.Location = New System.Drawing.Point(143, 328)
        Me.txtFSInProgress.Name = "txtFSInProgress"
        Me.txtFSInProgress.Size = New System.Drawing.Size(100, 20)
        Me.txtFSInProgress.TabIndex = 22
        '
        'llbDetailInProgress
        '
        Me.llbDetailInProgress.AutoSize = True
        Me.llbDetailInProgress.Location = New System.Drawing.Point(413, 330)
        Me.llbDetailInProgress.Name = "llbDetailInProgress"
        Me.llbDetailInProgress.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailInProgress.TabIndex = 24
        Me.llbDetailInProgress.TabStop = True
        Me.llbDetailInProgress.Text = "Detail"
        Me.llbDetailInProgress.UseCompatibleTextRendering = True
        '
        'llbFSSummaryNotReported
        '
        Me.llbFSSummaryNotReported.AutoSize = True
        Me.llbFSSummaryNotReported.Location = New System.Drawing.Point(328, 304)
        Me.llbFSSummaryNotReported.Name = "llbFSSummaryNotReported"
        Me.llbFSSummaryNotReported.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryNotReported.TabIndex = 20
        Me.llbFSSummaryNotReported.TabStop = True
        Me.llbFSSummaryNotReported.Text = "Summary"
        Me.llbFSSummaryNotReported.UseCompatibleTextRendering = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(66, 306)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(71, 13)
        Me.Label28.TabIndex = 159
        Me.Label28.Text = "Not Reported"
        '
        'txtFSNotReported
        '
        Me.txtFSNotReported.Location = New System.Drawing.Point(143, 302)
        Me.txtFSNotReported.Name = "txtFSNotReported"
        Me.txtFSNotReported.Size = New System.Drawing.Size(100, 20)
        Me.txtFSNotReported.TabIndex = 19
        '
        'llbDetailNotReported
        '
        Me.llbDetailNotReported.AutoSize = True
        Me.llbDetailNotReported.Location = New System.Drawing.Point(413, 304)
        Me.llbDetailNotReported.Name = "llbDetailNotReported"
        Me.llbDetailNotReported.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailNotReported.TabIndex = 21
        Me.llbDetailNotReported.TabStop = True
        Me.llbDetailNotReported.Text = "Detail"
        Me.llbDetailNotReported.UseCompatibleTextRendering = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(20, 277)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(94, 13)
        Me.Label24.TabIndex = 155
        Me.Label24.Text = "Enrolled Reporting"
        '
        'llbFSSummaryAdditions
        '
        Me.llbFSSummaryAdditions.AutoSize = True
        Me.llbFSSummaryAdditions.Location = New System.Drawing.Point(328, 242)
        Me.llbFSSummaryAdditions.Name = "llbFSSummaryAdditions"
        Me.llbFSSummaryAdditions.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryAdditions.TabIndex = 17
        Me.llbFSSummaryAdditions.TabStop = True
        Me.llbFSSummaryAdditions.Text = "Summary"
        Me.llbFSSummaryAdditions.UseCompatibleTextRendering = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(124, 244)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(50, 13)
        Me.Label23.TabIndex = 151
        Me.Label23.Text = "Additions"
        '
        'txtFSAdditions
        '
        Me.txtFSAdditions.Location = New System.Drawing.Point(180, 240)
        Me.txtFSAdditions.Name = "txtFSAdditions"
        Me.txtFSAdditions.Size = New System.Drawing.Size(100, 20)
        Me.txtFSAdditions.TabIndex = 16
        '
        'llbDetailAdditions
        '
        Me.llbDetailAdditions.AutoSize = True
        Me.llbDetailAdditions.Location = New System.Drawing.Point(413, 242)
        Me.llbDetailAdditions.Name = "llbDetailAdditions"
        Me.llbDetailAdditions.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailAdditions.TabIndex = 18
        Me.llbDetailAdditions.TabStop = True
        Me.llbDetailAdditions.Text = "Detail"
        Me.llbDetailAdditions.UseCompatibleTextRendering = True
        '
        'llbFSSummaryMailOut
        '
        Me.llbFSSummaryMailOut.AutoSize = True
        Me.llbFSSummaryMailOut.Location = New System.Drawing.Point(328, 216)
        Me.llbFSSummaryMailOut.Name = "llbFSSummaryMailOut"
        Me.llbFSSummaryMailOut.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryMailOut.TabIndex = 14
        Me.llbFSSummaryMailOut.TabStop = True
        Me.llbFSSummaryMailOut.Text = "Summary"
        Me.llbFSSummaryMailOut.UseCompatibleTextRendering = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(133, 218)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(41, 13)
        Me.Label22.TabIndex = 147
        Me.Label22.Text = "Mailout"
        '
        'txtFSMailout
        '
        Me.txtFSMailout.Location = New System.Drawing.Point(180, 214)
        Me.txtFSMailout.Name = "txtFSMailout"
        Me.txtFSMailout.Size = New System.Drawing.Size(100, 20)
        Me.txtFSMailout.TabIndex = 13
        '
        'llbDetailMailout
        '
        Me.llbDetailMailout.AutoSize = True
        Me.llbDetailMailout.Location = New System.Drawing.Point(413, 216)
        Me.llbDetailMailout.Name = "llbDetailMailout"
        Me.llbDetailMailout.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailMailout.TabIndex = 15
        Me.llbDetailMailout.TabStop = True
        Me.llbDetailMailout.Text = "Detail"
        Me.llbDetailMailout.UseCompatibleTextRendering = True
        '
        'llbFSSummaryEnrolled
        '
        Me.llbFSSummaryEnrolled.AutoSize = True
        Me.llbFSSummaryEnrolled.Location = New System.Drawing.Point(328, 188)
        Me.llbFSSummaryEnrolled.Name = "llbFSSummaryEnrolled"
        Me.llbFSSummaryEnrolled.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryEnrolled.TabIndex = 11
        Me.llbFSSummaryEnrolled.TabStop = True
        Me.llbFSSummaryEnrolled.Text = "Summary"
        Me.llbFSSummaryEnrolled.UseCompatibleTextRendering = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(92, 190)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 139
        Me.Label14.Text = "Enrolled"
        '
        'txtFSEnrolled
        '
        Me.txtFSEnrolled.Location = New System.Drawing.Point(143, 186)
        Me.txtFSEnrolled.Name = "txtFSEnrolled"
        Me.txtFSEnrolled.Size = New System.Drawing.Size(100, 20)
        Me.txtFSEnrolled.TabIndex = 10
        '
        'llbDetailEnrolled
        '
        Me.llbDetailEnrolled.AutoSize = True
        Me.llbDetailEnrolled.Location = New System.Drawing.Point(413, 188)
        Me.llbDetailEnrolled.Name = "llbDetailEnrolled"
        Me.llbDetailEnrolled.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailEnrolled.TabIndex = 12
        Me.llbDetailEnrolled.TabStop = True
        Me.llbDetailEnrolled.Text = "Detail"
        Me.llbDetailEnrolled.UseCompatibleTextRendering = True
        '
        'llbFSSummaryCeaseCollection
        '
        Me.llbFSSummaryCeaseCollection.AutoSize = True
        Me.llbFSSummaryCeaseCollection.Location = New System.Drawing.Point(328, 162)
        Me.llbFSSummaryCeaseCollection.Name = "llbFSSummaryCeaseCollection"
        Me.llbFSSummaryCeaseCollection.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryCeaseCollection.TabIndex = 8
        Me.llbFSSummaryCeaseCollection.TabStop = True
        Me.llbFSSummaryCeaseCollection.Text = "Summary"
        Me.llbFSSummaryCeaseCollection.UseCompatibleTextRendering = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(51, 164)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 13)
        Me.Label13.TabIndex = 135
        Me.Label13.Text = "Cease-Collection"
        '
        'txtFSCeaseCollection
        '
        Me.txtFSCeaseCollection.Location = New System.Drawing.Point(143, 160)
        Me.txtFSCeaseCollection.Name = "txtFSCeaseCollection"
        Me.txtFSCeaseCollection.Size = New System.Drawing.Size(100, 20)
        Me.txtFSCeaseCollection.TabIndex = 7
        '
        'llbDetailCeaseCollection
        '
        Me.llbDetailCeaseCollection.AutoSize = True
        Me.llbDetailCeaseCollection.Location = New System.Drawing.Point(413, 162)
        Me.llbDetailCeaseCollection.Name = "llbDetailCeaseCollection"
        Me.llbDetailCeaseCollection.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailCeaseCollection.TabIndex = 9
        Me.llbDetailCeaseCollection.TabStop = True
        Me.llbDetailCeaseCollection.Text = "Detail"
        Me.llbDetailCeaseCollection.UseCompatibleTextRendering = True
        '
        'llbFSSummaryUnEnrolled
        '
        Me.llbFSSummaryUnEnrolled.AutoSize = True
        Me.llbFSSummaryUnEnrolled.Location = New System.Drawing.Point(328, 136)
        Me.llbFSSummaryUnEnrolled.Name = "llbFSSummaryUnEnrolled"
        Me.llbFSSummaryUnEnrolled.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryUnEnrolled.TabIndex = 5
        Me.llbFSSummaryUnEnrolled.TabStop = True
        Me.llbFSSummaryUnEnrolled.Text = "Summary"
        Me.llbFSSummaryUnEnrolled.UseCompatibleTextRendering = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(78, 138)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 131
        Me.Label12.Text = "UnEnrolled"
        '
        'txtFSUnEnrolled
        '
        Me.txtFSUnEnrolled.Location = New System.Drawing.Point(143, 134)
        Me.txtFSUnEnrolled.Name = "txtFSUnEnrolled"
        Me.txtFSUnEnrolled.Size = New System.Drawing.Size(100, 20)
        Me.txtFSUnEnrolled.TabIndex = 4
        '
        'llbDetailUnEnrolled
        '
        Me.llbDetailUnEnrolled.AutoSize = True
        Me.llbDetailUnEnrolled.Location = New System.Drawing.Point(413, 136)
        Me.llbDetailUnEnrolled.Name = "llbDetailUnEnrolled"
        Me.llbDetailUnEnrolled.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailUnEnrolled.TabIndex = 6
        Me.llbDetailUnEnrolled.TabStop = True
        Me.llbDetailUnEnrolled.Text = "Detail"
        Me.llbDetailUnEnrolled.UseCompatibleTextRendering = True
        '
        'llbFSSummaryFeeUniverse
        '
        Me.llbFSSummaryFeeUniverse.AutoSize = True
        Me.llbFSSummaryFeeUniverse.Location = New System.Drawing.Point(328, 109)
        Me.llbFSSummaryFeeUniverse.Name = "llbFSSummaryFeeUniverse"
        Me.llbFSSummaryFeeUniverse.Size = New System.Drawing.Size(53, 17)
        Me.llbFSSummaryFeeUniverse.TabIndex = 2
        Me.llbFSSummaryFeeUniverse.TabStop = True
        Me.llbFSSummaryFeeUniverse.Text = "Summary"
        Me.llbFSSummaryFeeUniverse.UseCompatibleTextRendering = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(20, 111)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 13)
        Me.Label11.TabIndex = 127
        Me.Label11.Text = "Fee Universe "
        '
        'txtFSFeeUniverse
        '
        Me.txtFSFeeUniverse.Location = New System.Drawing.Point(97, 107)
        Me.txtFSFeeUniverse.Name = "txtFSFeeUniverse"
        Me.txtFSFeeUniverse.Size = New System.Drawing.Size(100, 20)
        Me.txtFSFeeUniverse.TabIndex = 1
        '
        'llbDetailFeeUniverse
        '
        Me.llbDetailFeeUniverse.AutoSize = True
        Me.llbDetailFeeUniverse.Location = New System.Drawing.Point(413, 109)
        Me.llbDetailFeeUniverse.Name = "llbDetailFeeUniverse"
        Me.llbDetailFeeUniverse.Size = New System.Drawing.Size(33, 17)
        Me.llbDetailFeeUniverse.TabIndex = 3
        Me.llbDetailFeeUniverse.TabStop = True
        Me.llbDetailFeeUniverse.Text = "Detail"
        Me.llbDetailFeeUniverse.UseCompatibleTextRendering = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label7.Location = New System.Drawing.Point(8, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(280, 22)
        Me.Label7.TabIndex = 122
        Me.Label7.Text = "Fee Summary for Calendar Year "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 121
        Me.Label9.Text = "Select a Year:"
        '
        'cboFeeStatYear
        '
        Me.cboFeeStatYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFeeStatYear.FormattingEnabled = True
        Me.cboFeeStatYear.Location = New System.Drawing.Point(100, 57)
        Me.cboFeeStatYear.Name = "cboFeeStatYear"
        Me.cboFeeStatYear.Size = New System.Drawing.Size(97, 21)
        Me.cboFeeStatYear.TabIndex = 0
        '
        'TPReported
        '
        Me.TPReported.Controls.Add(Me.dgvReported)
        Me.TPReported.Controls.Add(Me.Panel6)
        Me.TPReported.Location = New System.Drawing.Point(4, 22)
        Me.TPReported.Name = "TPReported"
        Me.TPReported.Padding = New System.Windows.Forms.Padding(3)
        Me.TPReported.Size = New System.Drawing.Size(936, 692)
        Me.TPReported.TabIndex = 8
        Me.TPReported.Text = "Reported Emissions/Fees"
        Me.TPReported.UseVisualStyleBackColor = True
        '
        'dgvReported
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvReported.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvReported.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvReported.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReported.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReported.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvReported.LinkifyColumnByName = Nothing
        Me.dgvReported.Location = New System.Drawing.Point(3, 52)
        Me.dgvReported.Name = "dgvReported"
        Me.dgvReported.ResultsCountLabel = Nothing
        Me.dgvReported.ResultsCountLabelFormat = "{0} found"
        Me.dgvReported.Size = New System.Drawing.Size(930, 637)
        Me.dgvReported.StandardTab = True
        Me.dgvReported.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.cbReportedYear)
        Me.Panel6.Controls.Add(Me.btnExportedRun)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(930, 49)
        Me.Panel6.TabIndex = 0
        '
        'cbReportedYear
        '
        Me.cbReportedYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReportedYear.FormattingEnabled = True
        Me.cbReportedYear.Location = New System.Drawing.Point(14, 14)
        Me.cbReportedYear.Name = "cbReportedYear"
        Me.cbReportedYear.Size = New System.Drawing.Size(83, 21)
        Me.cbReportedYear.TabIndex = 1
        '
        'btnExportedRun
        '
        Me.btnExportedRun.Location = New System.Drawing.Point(103, 13)
        Me.btnExportedRun.Name = "btnExportedRun"
        Me.btnExportedRun.Size = New System.Drawing.Size(75, 23)
        Me.btnExportedRun.TabIndex = 0
        Me.btnExportedRun.Text = "View Data"
        Me.btnExportedRun.UseVisualStyleBackColor = True
        '
        'TPReports
        '
        Me.TPReports.Controls.Add(Me.Panel2)
        Me.TPReports.Controls.Add(Me.tabReport)
        Me.TPReports.Location = New System.Drawing.Point(4, 22)
        Me.TPReports.Name = "TPReports"
        Me.TPReports.Size = New System.Drawing.Size(936, 692)
        Me.TPReports.TabIndex = 6
        Me.TPReports.Text = "Fee Reports"
        Me.TPReports.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GridFeesReports)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 114)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(936, 578)
        Me.Panel2.TabIndex = 271
        '
        'GridFeesReports
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GridFeesReports.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GridFeesReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridFeesReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.GridFeesReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridFeesReports.GridColor = System.Drawing.SystemColors.ControlLight
        Me.GridFeesReports.LinkifyColumnByName = Nothing
        Me.GridFeesReports.Location = New System.Drawing.Point(0, 0)
        Me.GridFeesReports.Name = "GridFeesReports"
        Me.GridFeesReports.ResultsCountLabel = Nothing
        Me.GridFeesReports.ResultsCountLabelFormat = "{0} found"
        Me.GridFeesReports.Size = New System.Drawing.Size(936, 578)
        Me.GridFeesReports.StandardTab = True
        Me.GridFeesReports.TabIndex = 271
        '
        'tabReport
        '
        Me.tabReport.Controls.Add(Me.TPFinancial)
        Me.tabReport.Controls.Add(Me.TPAnnualBalance)
        Me.tabReport.Controls.Add(Me.TPDeposits)
        Me.tabReport.Controls.Add(Me.TPCompliance)
        Me.tabReport.Controls.Add(Me.TPNsps)
        Me.tabReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.tabReport.Location = New System.Drawing.Point(0, 0)
        Me.tabReport.Margin = New System.Windows.Forms.Padding(2)
        Me.tabReport.Name = "tabReport"
        Me.tabReport.SelectedIndex = 0
        Me.tabReport.Size = New System.Drawing.Size(936, 114)
        Me.tabReport.TabIndex = 269
        '
        'TPFinancial
        '
        Me.TPFinancial.Controls.Add(Me.btnPayment)
        Me.TPFinancial.Location = New System.Drawing.Point(4, 22)
        Me.TPFinancial.Margin = New System.Windows.Forms.Padding(2)
        Me.TPFinancial.Name = "TPFinancial"
        Me.TPFinancial.Padding = New System.Windows.Forms.Padding(2)
        Me.TPFinancial.Size = New System.Drawing.Size(928, 88)
        Me.TPFinancial.TabIndex = 1
        Me.TPFinancial.Text = "Financial"
        Me.TPFinancial.UseVisualStyleBackColor = True
        '
        'btnPayment
        '
        Me.btnPayment.CausesValidation = False
        Me.btnPayment.Location = New System.Drawing.Point(4, 4)
        Me.btnPayment.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(113, 23)
        Me.btnPayment.TabIndex = 0
        Me.btnPayment.Text = "Overall Fee Balance"
        '
        'TPAnnualBalance
        '
        Me.TPAnnualBalance.Controls.Add(Me.cbBalanceYear)
        Me.TPAnnualBalance.Controls.Add(Me.lblFacilityBalanceReportTag)
        Me.TPAnnualBalance.Controls.Add(Me.btnRunBalanceReport)
        Me.TPAnnualBalance.Location = New System.Drawing.Point(4, 22)
        Me.TPAnnualBalance.Name = "TPAnnualBalance"
        Me.TPAnnualBalance.Size = New System.Drawing.Size(928, 88)
        Me.TPAnnualBalance.TabIndex = 6
        Me.TPAnnualBalance.Text = "Annual Balances"
        Me.TPAnnualBalance.UseVisualStyleBackColor = True
        '
        'cbBalanceYear
        '
        Me.cbBalanceYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBalanceYear.FormattingEnabled = True
        Me.cbBalanceYear.Location = New System.Drawing.Point(42, 10)
        Me.cbBalanceYear.Name = "cbBalanceYear"
        Me.cbBalanceYear.Size = New System.Drawing.Size(72, 21)
        Me.cbBalanceYear.TabIndex = 13
        '
        'lblFacilityBalanceReportTag
        '
        Me.lblFacilityBalanceReportTag.AutoSize = True
        Me.lblFacilityBalanceReportTag.Location = New System.Drawing.Point(4, 13)
        Me.lblFacilityBalanceReportTag.Name = "lblFacilityBalanceReportTag"
        Me.lblFacilityBalanceReportTag.Size = New System.Drawing.Size(32, 13)
        Me.lblFacilityBalanceReportTag.TabIndex = 10
        Me.lblFacilityBalanceReportTag.Text = "Year:"
        '
        'btnRunBalanceReport
        '
        Me.btnRunBalanceReport.AutoSize = True
        Me.btnRunBalanceReport.Location = New System.Drawing.Point(120, 8)
        Me.btnRunBalanceReport.Name = "btnRunBalanceReport"
        Me.btnRunBalanceReport.Size = New System.Drawing.Size(114, 23)
        Me.btnRunBalanceReport.TabIndex = 9
        Me.btnRunBalanceReport.Text = "Run Balance Report"
        Me.btnRunBalanceReport.UseVisualStyleBackColor = True
        '
        'TPDeposits
        '
        Me.TPDeposits.Controls.Add(Me.btnViewDepositsReportByDate)
        Me.TPDeposits.Controls.Add(Me.Label2)
        Me.TPDeposits.Controls.Add(Me.Label3)
        Me.TPDeposits.Controls.Add(Me.dtpDepositReportEndDate)
        Me.TPDeposits.Controls.Add(Me.dtpDepositReportStartDate)
        Me.TPDeposits.Controls.Add(Me.btnViewFacilityDepositsReport)
        Me.TPDeposits.Controls.Add(Me.cboAirs)
        Me.TPDeposits.Controls.Add(Me.Label1)
        Me.TPDeposits.Location = New System.Drawing.Point(4, 22)
        Me.TPDeposits.Margin = New System.Windows.Forms.Padding(2)
        Me.TPDeposits.Name = "TPDeposits"
        Me.TPDeposits.Size = New System.Drawing.Size(928, 88)
        Me.TPDeposits.TabIndex = 3
        Me.TPDeposits.Text = "Deposits"
        Me.TPDeposits.UseVisualStyleBackColor = True
        '
        'btnViewDepositsReportByDate
        '
        Me.btnViewDepositsReportByDate.AutoSize = True
        Me.btnViewDepositsReportByDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewDepositsReportByDate.Location = New System.Drawing.Point(537, 18)
        Me.btnViewDepositsReportByDate.Name = "btnViewDepositsReportByDate"
        Me.btnViewDepositsReportByDate.Size = New System.Drawing.Size(66, 23)
        Me.btnViewDepositsReportByDate.TabIndex = 409
        Me.btnViewDepositsReportByDate.Text = "View Data"
        Me.btnViewDepositsReportByDate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(428, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 408
        Me.Label2.Text = "End Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(322, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 407
        Me.Label3.Text = "Start Date"
        '
        'dtpDepositReportEndDate
        '
        Me.dtpDepositReportEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositReportEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositReportEndDate.Location = New System.Drawing.Point(431, 20)
        Me.dtpDepositReportEndDate.Name = "dtpDepositReportEndDate"
        Me.dtpDepositReportEndDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDepositReportEndDate.TabIndex = 406
        '
        'dtpDepositReportStartDate
        '
        Me.dtpDepositReportStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositReportStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositReportStartDate.Location = New System.Drawing.Point(325, 20)
        Me.dtpDepositReportStartDate.Name = "dtpDepositReportStartDate"
        Me.dtpDepositReportStartDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDepositReportStartDate.TabIndex = 405
        '
        'btnViewFacilityDepositsReport
        '
        Me.btnViewFacilityDepositsReport.Location = New System.Drawing.Point(194, 19)
        Me.btnViewFacilityDepositsReport.Name = "btnViewFacilityDepositsReport"
        Me.btnViewFacilityDepositsReport.Size = New System.Drawing.Size(75, 23)
        Me.btnViewFacilityDepositsReport.TabIndex = 149
        Me.btnViewFacilityDepositsReport.Text = "View Data"
        Me.btnViewFacilityDepositsReport.UseVisualStyleBackColor = True
        '
        'cboAirs
        '
        Me.cboAirs.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirs.Location = New System.Drawing.Point(81, 20)
        Me.cboAirs.Margin = New System.Windows.Forms.Padding(2)
        Me.cboAirs.Name = "cboAirs"
        Me.cboAirs.Size = New System.Drawing.Size(108, 21)
        Me.cboAirs.Sorted = True
        Me.cboAirs.TabIndex = 145
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 147
        Me.Label1.Text = "AIRS Number:"
        '
        'TPCompliance
        '
        Me.TPCompliance.Controls.Add(Me.btnClassification)
        Me.TPCompliance.Controls.Add(Me.btnClassChange)
        Me.TPCompliance.Location = New System.Drawing.Point(4, 22)
        Me.TPCompliance.Margin = New System.Windows.Forms.Padding(2)
        Me.TPCompliance.Name = "TPCompliance"
        Me.TPCompliance.Size = New System.Drawing.Size(928, 88)
        Me.TPCompliance.TabIndex = 4
        Me.TPCompliance.Text = "Class"
        Me.TPCompliance.UseVisualStyleBackColor = True
        '
        'btnClassification
        '
        Me.btnClassification.CausesValidation = False
        Me.btnClassification.Location = New System.Drawing.Point(3, 4)
        Me.btnClassification.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClassification.Name = "btnClassification"
        Me.btnClassification.Size = New System.Drawing.Size(129, 23)
        Me.btnClassification.TabIndex = 5
        Me.btnClassification.Text = "Classification Totals"
        '
        'btnClassChange
        '
        Me.btnClassChange.CausesValidation = False
        Me.btnClassChange.Location = New System.Drawing.Point(136, 4)
        Me.btnClassChange.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClassChange.Name = "btnClassChange"
        Me.btnClassChange.Size = New System.Drawing.Size(129, 23)
        Me.btnClassChange.TabIndex = 0
        Me.btnClassChange.Text = "Change in Classification"
        Me.btnClassChange.UseVisualStyleBackColor = False
        '
        'TPNsps
        '
        Me.TPNsps.Controls.Add(Me.lblNSPS3)
        Me.TPNsps.Controls.Add(Me.lblNSPS2)
        Me.TPNsps.Controls.Add(Me.lblNSPS1)
        Me.TPNsps.Location = New System.Drawing.Point(4, 22)
        Me.TPNsps.Name = "TPNsps"
        Me.TPNsps.Size = New System.Drawing.Size(928, 88)
        Me.TPNsps.TabIndex = 7
        Me.TPNsps.Text = "NSPS"
        Me.TPNsps.UseVisualStyleBackColor = True
        '
        'lblNSPS3
        '
        Me.lblNSPS3.AutoSize = True
        Me.lblNSPS3.Location = New System.Drawing.Point(3, 33)
        Me.lblNSPS3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNSPS3.Name = "lblNSPS3"
        Me.lblNSPS3.Size = New System.Drawing.Size(363, 13)
        Me.lblNSPS3.TabIndex = 5
        Me.lblNSPS3.TabStop = True
        Me.lblNSPS3.Text = "All facilities that are subject to NSPS, but indicated that they did not operate"
        '
        'lblNSPS2
        '
        Me.lblNSPS2.AutoSize = True
        Me.lblNSPS2.Location = New System.Drawing.Point(2, 56)
        Me.lblNSPS2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNSPS2.Name = "lblNSPS2"
        Me.lblNSPS2.Size = New System.Drawing.Size(357, 13)
        Me.lblNSPS2.TabIndex = 4
        Me.lblNSPS2.TabStop = True
        Me.lblNSPS2.Text = "All facilities that are not subject to NSPS, but indicated that they are NSPS"
        '
        'lblNSPS1
        '
        Me.lblNSPS1.AutoSize = True
        Me.lblNSPS1.Location = New System.Drawing.Point(3, 10)
        Me.lblNSPS1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNSPS1.Name = "lblNSPS1"
        Me.lblNSPS1.Size = New System.Drawing.Size(335, 13)
        Me.lblNSPS1.TabIndex = 3
        Me.lblNSPS1.TabStop = True
        Me.lblNSPS1.Text = "All facilities that are subject to NSPS, but chose to exempt from NSPS"
        '
        'FeesStatistics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 718)
        Me.Controls.Add(Me.TCMailoutAndStats)
        Me.MinimumSize = New System.Drawing.Size(960, 756)
        Me.Name = "FeesStatistics"
        Me.Text = "Annual Fees - Statistics and Reports"
        Me.TCMailoutAndStats.ResumeLayout(False)
        Me.TPDepositAndPaymentStats.ResumeLayout(False)
        CType(Me.dgvDepositsAndPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        CType(Me.dgvStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TPFeeStatistics2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvFeeStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.TPReported.ResumeLayout(False)
        CType(Me.dgvReported, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.TPReports.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.GridFeesReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabReport.ResumeLayout(False)
        Me.TPFinancial.ResumeLayout(False)
        Me.TPAnnualBalance.ResumeLayout(False)
        Me.TPAnnualBalance.PerformLayout()
        Me.TPDeposits.ResumeLayout(False)
        Me.TPDeposits.PerformLayout()
        Me.TPCompliance.ResumeLayout(False)
        Me.TPNsps.ResumeLayout(False)
        Me.TPNsps.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bgwEmails As System.ComponentModel.BackgroundWorker
    Friend WithEvents TCMailoutAndStats As System.Windows.Forms.TabControl
    Friend WithEvents TPDepositAndPaymentStats As System.Windows.Forms.TabPage
    Friend WithEvents dgvDepositsAndPayments As Iaip.IaipDataGridView
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents txtAllFees As System.Windows.Forms.TextBox
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents txtAdminFee As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtSMfee As System.Windows.Forms.TextBox
    Friend WithEvents txtNSPSfee As System.Windows.Forms.TextBox
    Friend WithEvents dgvStats As System.Windows.Forms.DataGridView
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents txtShutDown As System.Windows.Forms.TextBox
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
    Friend WithEvents txtNSPSExemptReason As System.Windows.Forms.TextBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents txtFeeRate As System.Windows.Forms.TextBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents txtOperate As System.Windows.Forms.TextBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents txtNSPSExempt As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtPMTons As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents txtSO2Tons As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtNOxTons As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtPart70Fee As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtVOCTons As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtSubmittalComments As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtDateSubmitted As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtOnlineSubmittalStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPaymentType As System.Windows.Forms.TextBox
    Friend WithEvents btnRunDepositReport As System.Windows.Forms.Button
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbDepositDateSearch As System.Windows.Forms.CheckBox
    Friend WithEvents chbNonZeroBalance As System.Windows.Forms.CheckBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedYear As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnViewSelectedFeeData As System.Windows.Forms.Button
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents btnViewTotalPaid As System.Windows.Forms.Button
    Friend WithEvents btnViewPaymentDue As System.Windows.Forms.Button
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPaid As System.Windows.Forms.TextBox
    Friend WithEvents cboStatYear As System.Windows.Forms.ComboBox
    Friend WithEvents cboStatPayType As System.Windows.Forms.ComboBox
    Friend WithEvents txtTotalPaymentDue As System.Windows.Forms.TextBox
    Friend WithEvents TPReports As System.Windows.Forms.TabPage
    Friend WithEvents tabReport As System.Windows.Forms.TabControl
    Friend WithEvents TPFinancial As System.Windows.Forms.TabPage
    Friend WithEvents btnPayment As System.Windows.Forms.Button
    Friend WithEvents TPDeposits As System.Windows.Forms.TabPage
    Friend WithEvents cboAirs As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TPCompliance As System.Windows.Forms.TabPage
    Friend WithEvents btnClassChange As System.Windows.Forms.Button
    Friend WithEvents TPFeeStatistics2 As System.Windows.Forms.TabPage
    Friend WithEvents dgvFeeStats As Iaip.IaipDataGridView
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents llbFSSummaryFeeUniverse As System.Windows.Forms.LinkLabel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtFSFeeUniverse As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailFeeUniverse As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboFeeStatYear As System.Windows.Forms.ComboBox
    Friend WithEvents llbFSSummaryAdditions As System.Windows.Forms.LinkLabel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtFSAdditions As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailAdditions As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryMailOut As System.Windows.Forms.LinkLabel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtFSMailout As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailMailout As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryEnrolled As System.Windows.Forms.LinkLabel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtFSEnrolled As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailEnrolled As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryCeaseCollection As System.Windows.Forms.LinkLabel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtFSCeaseCollection As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailCeaseCollection As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryUnEnrolled As System.Windows.Forms.LinkLabel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFSUnEnrolled As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailUnEnrolled As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtFSFinalized As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryInProgress As System.Windows.Forms.LinkLabel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtFSInProgress As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailInProgress As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryNotReported As System.Windows.Forms.LinkLabel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtFSNotReported As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailNotReported As System.Windows.Forms.LinkLabel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents llbFSSummaryPaidInFull As System.Windows.Forms.LinkLabel
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtFSPaidInFull As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailPaidInFull As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryOutofBalance As System.Windows.Forms.LinkLabel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtFSOutOfBalance As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailOutOfBalance As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryNotPaid As System.Windows.Forms.LinkLabel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtFSNotPaid As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailNotPaid As System.Windows.Forms.LinkLabel
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtFSQuarterly As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtFSAnnual As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtFSOverPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents txtFSPartial As System.Windows.Forms.TextBox
    Friend WithEvents llbFSSummaryLateWithFee As System.Windows.Forms.LinkLabel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFSLateFee As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailLateWithFee As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryLateResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtFSOnTimeResponse As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtFSLateResponse As System.Windows.Forms.TextBox
    Friend WithEvents llbFSSummaryOnTime As System.Windows.Forms.LinkLabel
    Friend WithEvents llbDetailLateResponse As System.Windows.Forms.LinkLabel
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents llbFSSummaryPaidNotFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtFSPaidNotFinalized As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailPaidNotFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFSSummaryPaidFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtFSPaidFinalized As System.Windows.Forms.TextBox
    Friend WithEvents llbDetailPaidFinalized As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnOpenFeesLog As System.Windows.Forms.Button
    Friend WithEvents lblCountFeeStats As System.Windows.Forms.Label
    Friend WithEvents btnCheckInvoices As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPaymentInvoiced As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtInvoicedBalance As System.Windows.Forms.TextBox
    Friend WithEvents btnInvoicedPaymentDue As System.Windows.Forms.Button
    Friend WithEvents btnInvoiceReportVariance As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnViewInvoicedBalance As System.Windows.Forms.Button
    Friend WithEvents txtIAIPStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TPAnnualBalance As TabPage
    Friend WithEvents btnRunBalanceReport As Button
    Friend WithEvents lblFacilityBalanceReportTag As Label
    Friend WithEvents btnViewFacilityDepositsReport As Button
    Friend WithEvents TPNsps As TabPage
    Friend WithEvents lblNSPS1 As LinkLabel
    Friend WithEvents lblNSPS2 As LinkLabel
    Friend WithEvents lblNSPS3 As LinkLabel
    Friend WithEvents btnViewDepositsReportByDate As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpDepositReportEndDate As DateTimePicker
    Friend WithEvents dtpDepositReportStartDate As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents TPReported As TabPage
    Friend WithEvents dgvReported As Iaip.IaipDataGridView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents cbReportedYear As ComboBox
    Friend WithEvents btnExportedRun As Button
    Friend WithEvents cbBalanceYear As ComboBox
    Friend WithEvents lblCountDepositsPayments As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtPart70MaintenanceFee As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GridFeesReports As IaipDataGridView
    Friend WithEvents btnClassification As Button
    Friend WithEvents llbFSDetailOnTime As LinkLabel
    Friend WithEvents FeeStatAirsNumberEntry As AirsNumberEntryForm
End Class
