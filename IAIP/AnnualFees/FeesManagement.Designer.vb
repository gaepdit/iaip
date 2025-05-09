<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FeesManagement
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.FeeManagementTabControl = New System.Windows.Forms.TabControl()
        Me.TPFeeAdminTools = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.dgvFeeRates = New Iaip.IaipDataGridView()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNonAttainmentThreshold = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtAttainmentThreshold = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpFourthQrtDue = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpThirdQrtDue = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpSecondQrtDue = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpFirstQrtDue = New System.Windows.Forms.DateTimePicker()
        Me.btnReloadFeeRate = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.dtpFeeDueDate = New System.Windows.Forms.DateTimePicker()
        Me.btnUpdateFeeData = New System.Windows.Forms.Button()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtFeeNotes = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtFeeYear = New System.Windows.Forms.TextBox()
        Me.Label248 = New System.Windows.Forms.Label()
        Me.dtpFeePeriodStart = New System.Windows.Forms.DateTimePicker()
        Me.txtAdminFeePercent = New System.Windows.Forms.TextBox()
        Me.dtpFeePeriodEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPerTonRate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtPart70MaintenanceFee = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPart70Fee = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.dtpAdminApplicableDate = New System.Windows.Forms.DateTimePicker()
        Me.txtAnnualNSPSFee = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtAnnualSMFee = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.pnlNSPSExemptions = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.dgvNSPSExemptions = New System.Windows.Forms.DataGridView()
        Me.btnSelectForm = New System.Windows.Forms.Button()
        Me.btnSelectAllForms = New System.Windows.Forms.Button()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.btnUnselectForm = New System.Windows.Forms.Button()
        Me.btnUpdateNSPSbyYear = New System.Windows.Forms.Button()
        Me.btnViewNSPSExemptionsByYear = New System.Windows.Forms.Button()
        Me.dgvNSPSExemptionsByYear = New System.Windows.Forms.DataGridView()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.cboNSPSExemptionYear = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.btnClearNSPSExemptions = New System.Windows.Forms.Button()
        Me.btnRefreshNSPSExemptions = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgvExistingExemptions = New System.Windows.Forms.DataGridView()
        Me.btnUpdateNSPSExemption = New System.Windows.Forms.Button()
        Me.btnViewDeletedNSPS = New System.Windows.Forms.Button()
        Me.txtNSPSExemption = New System.Windows.Forms.TextBox()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.btnAddNSPSExemption = New System.Windows.Forms.Button()
        Me.btnDeleteNSPSExemption = New System.Windows.Forms.Button()
        Me.txtDeleteNSPSExemptions = New System.Windows.Forms.TextBox()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.TPFeeManagementTools = New System.Windows.Forms.TabPage()
        Me.FeeManagementSidePanel = New System.Windows.Forms.Panel()
        Me.AIRSNumberEntry = New Iaip.AirsNumberEntryForm()
        Me.dgvFeeManagementLists = New Iaip.IaipDataGridView()
        Me.FeeManagementListCountLabel = New System.Windows.Forms.Label()
        Me.btnOpenFeesLog = New System.Windows.Forms.Button()
        Me.FeeManagementToolPanel = New System.Windows.Forms.Panel()
        Me.MailoutInfoGroupBox = New System.Windows.Forms.GroupBox()
        Me.lblInitialMailoutDate = New System.Windows.Forms.Label()
        Me.btnSendInitialEmail = New System.Windows.Forms.Button()
        Me.btnViewEmailBatchStatus = New System.Windows.Forms.Button()
        Me.btnViewEmailList = New System.Windows.Forms.Button()
        Me.btnViewPhysicalMailList = New System.Windows.Forms.Button()
        Me.EnrollmentGroupbox = New System.Windows.Forms.GroupBox()
        Me.btnUnenrollFeeYear = New System.Windows.Forms.Button()
        Me.btnFirstEnrollment = New System.Windows.Forms.Button()
        Me.lblFeeDueDate = New System.Windows.Forms.Label()
        Me.lblEnrollmentCount = New System.Windows.Forms.Label()
        Me.lblMailoutCount = New System.Windows.Forms.Label()
        Me.lblFeeYearCount = New System.Windows.Forms.Label()
        Me.btnViewMailout = New System.Windows.Forms.Button()
        Me.btnViewEnrolledFacilities = New System.Windows.Forms.Button()
        Me.btnViewFacilitiesSubjectToFees = New System.Windows.Forms.Button()
        Me.cboAvailableFeeYears = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.InitialMailoutGroupbox = New System.Windows.Forms.GroupBox()
        Me.btnGenerateMailoutList = New System.Windows.Forms.Button()
        Me.btnUpdateContactData = New System.Windows.Forms.Button()
        Me.FeeManagementTabControl.SuspendLayout()
        Me.TPFeeAdminTools.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel12.SuspendLayout()
        CType(Me.dgvFeeRates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.pnlNSPSExemptions.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel13.SuspendLayout()
        CType(Me.dgvNSPSExemptions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNSPSExemptionsByYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.dgvExistingExemptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPFeeManagementTools.SuspendLayout()
        Me.FeeManagementSidePanel.SuspendLayout()
        CType(Me.dgvFeeManagementLists, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FeeManagementToolPanel.SuspendLayout()
        Me.MailoutInfoGroupBox.SuspendLayout()
        Me.EnrollmentGroupbox.SuspendLayout()
        Me.InitialMailoutGroupbox.SuspendLayout()
        Me.SuspendLayout()
        '
        'FeeManagementTabControl
        '
        Me.FeeManagementTabControl.Controls.Add(Me.TPFeeAdminTools)
        Me.FeeManagementTabControl.Controls.Add(Me.TPFeeManagementTools)
        Me.FeeManagementTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FeeManagementTabControl.Location = New System.Drawing.Point(0, 0)
        Me.FeeManagementTabControl.Name = "FeeManagementTabControl"
        Me.FeeManagementTabControl.SelectedIndex = 0
        Me.FeeManagementTabControl.Size = New System.Drawing.Size(826, 659)
        Me.FeeManagementTabControl.TabIndex = 0
        '
        'TPFeeAdminTools
        '
        Me.TPFeeAdminTools.Controls.Add(Me.TabControl2)
        Me.TPFeeAdminTools.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeAdminTools.Name = "TPFeeAdminTools"
        Me.TPFeeAdminTools.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFeeAdminTools.Size = New System.Drawing.Size(818, 633)
        Me.TPFeeAdminTools.TabIndex = 0
        Me.TPFeeAdminTools.Text = "Fee Admin Tools"
        Me.TPFeeAdminTools.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage2)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(3, 3)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(812, 627)
        Me.TabControl2.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel12)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(804, 601)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Annual Fee Rates"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.dgvFeeRates)
        Me.Panel12.Controls.Add(Me.Label16)
        Me.Panel12.Controls.Add(Me.txtNonAttainmentThreshold)
        Me.Panel12.Controls.Add(Me.Label15)
        Me.Panel12.Controls.Add(Me.txtAttainmentThreshold)
        Me.Panel12.Controls.Add(Me.Label12)
        Me.Panel12.Controls.Add(Me.dtpFourthQrtDue)
        Me.Panel12.Controls.Add(Me.Label14)
        Me.Panel12.Controls.Add(Me.dtpThirdQrtDue)
        Me.Panel12.Controls.Add(Me.Label11)
        Me.Panel12.Controls.Add(Me.dtpSecondQrtDue)
        Me.Panel12.Controls.Add(Me.Label9)
        Me.Panel12.Controls.Add(Me.dtpFirstQrtDue)
        Me.Panel12.Controls.Add(Me.btnReloadFeeRate)
        Me.Panel12.Controls.Add(Me.Label19)
        Me.Panel12.Controls.Add(Me.Label37)
        Me.Panel12.Controls.Add(Me.dtpFeeDueDate)
        Me.Panel12.Controls.Add(Me.btnUpdateFeeData)
        Me.Panel12.Controls.Add(Me.Label36)
        Me.Panel12.Controls.Add(Me.txtFeeNotes)
        Me.Panel12.Controls.Add(Me.Label35)
        Me.Panel12.Controls.Add(Me.txtFeeYear)
        Me.Panel12.Controls.Add(Me.Label248)
        Me.Panel12.Controls.Add(Me.dtpFeePeriodStart)
        Me.Panel12.Controls.Add(Me.txtAdminFeePercent)
        Me.Panel12.Controls.Add(Me.dtpFeePeriodEnd)
        Me.Panel12.Controls.Add(Me.Label5)
        Me.Panel12.Controls.Add(Me.Label55)
        Me.Panel12.Controls.Add(Me.Label3)
        Me.Panel12.Controls.Add(Me.txtPerTonRate)
        Me.Panel12.Controls.Add(Me.Label2)
        Me.Panel12.Controls.Add(Me.Label57)
        Me.Panel12.Controls.Add(Me.txtPart70MaintenanceFee)
        Me.Panel12.Controls.Add(Me.Label1)
        Me.Panel12.Controls.Add(Me.txtPart70Fee)
        Me.Panel12.Controls.Add(Me.Label58)
        Me.Panel12.Controls.Add(Me.Label59)
        Me.Panel12.Controls.Add(Me.dtpAdminApplicableDate)
        Me.Panel12.Controls.Add(Me.txtAnnualNSPSFee)
        Me.Panel12.Controls.Add(Me.Label60)
        Me.Panel12.Controls.Add(Me.txtAnnualSMFee)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(3, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(798, 595)
        Me.Panel12.TabIndex = 0
        '
        'dgvFeeRates
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvFeeRates.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvFeeRates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFeeRates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvFeeRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFeeRates.DecimalFieldFormat = "#,0.##"
        Me.dgvFeeRates.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvFeeRates.LinkifyColumnByName = Nothing
        Me.dgvFeeRates.Location = New System.Drawing.Point(0, 0)
        Me.dgvFeeRates.Name = "dgvFeeRates"
        Me.dgvFeeRates.ResultsCountLabel = Nothing
        Me.dgvFeeRates.ResultsCountLabelFormat = "{0} found"
        Me.dgvFeeRates.Size = New System.Drawing.Size(507, 595)
        Me.dgvFeeRates.StandardTab = True
        Me.dgvFeeRates.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(518, 481)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(133, 13)
        Me.Label16.TabIndex = 501
        Me.Label16.Text = "Non-Attainment Threshold:"
        '
        'txtNonAttainmentThreshold
        '
        Me.txtNonAttainmentThreshold.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNonAttainmentThreshold.Location = New System.Drawing.Point(695, 479)
        Me.txtNonAttainmentThreshold.Name = "txtNonAttainmentThreshold"
        Me.txtNonAttainmentThreshold.Size = New System.Drawing.Size(100, 20)
        Me.txtNonAttainmentThreshold.TabIndex = 18
        Me.txtNonAttainmentThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(518, 457)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 13)
        Me.Label15.TabIndex = 499
        Me.Label15.Text = "Attainment Threshold:"
        '
        'txtAttainmentThreshold
        '
        Me.txtAttainmentThreshold.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAttainmentThreshold.Location = New System.Drawing.Point(695, 455)
        Me.txtAttainmentThreshold.Name = "txtAttainmentThreshold"
        Me.txtAttainmentThreshold.Size = New System.Drawing.Size(100, 20)
        Me.txtAttainmentThreshold.TabIndex = 17
        Me.txtAttainmentThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(518, 385)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 13)
        Me.Label12.TabIndex = 497
        Me.Label12.Text = "4th Quarter Due Date:"
        '
        'dtpFourthQrtDue
        '
        Me.dtpFourthQrtDue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFourthQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFourthQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFourthQrtDue.Location = New System.Drawing.Point(695, 381)
        Me.dtpFourthQrtDue.Name = "dtpFourthQrtDue"
        Me.dtpFourthQrtDue.Size = New System.Drawing.Size(100, 20)
        Me.dtpFourthQrtDue.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(518, 359)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 13)
        Me.Label14.TabIndex = 495
        Me.Label14.Text = "3rd Quarter Due Date:"
        '
        'dtpThirdQrtDue
        '
        Me.dtpThirdQrtDue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpThirdQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpThirdQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpThirdQrtDue.Location = New System.Drawing.Point(695, 355)
        Me.dtpThirdQrtDue.Name = "dtpThirdQrtDue"
        Me.dtpThirdQrtDue.Size = New System.Drawing.Size(100, 20)
        Me.dtpThirdQrtDue.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(518, 333)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(115, 13)
        Me.Label11.TabIndex = 493
        Me.Label11.Text = "2nd Quarter Due Date:"
        '
        'dtpSecondQrtDue
        '
        Me.dtpSecondQrtDue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpSecondQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpSecondQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSecondQrtDue.Location = New System.Drawing.Point(695, 330)
        Me.dtpSecondQrtDue.Name = "dtpSecondQrtDue"
        Me.dtpSecondQrtDue.Size = New System.Drawing.Size(100, 20)
        Me.dtpSecondQrtDue.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(518, 307)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(111, 13)
        Me.Label9.TabIndex = 491
        Me.Label9.Text = "1st Quarter Due Date:"
        '
        'dtpFirstQrtDue
        '
        Me.dtpFirstQrtDue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFirstQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFirstQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFirstQrtDue.Location = New System.Drawing.Point(695, 304)
        Me.dtpFirstQrtDue.Name = "dtpFirstQrtDue"
        Me.dtpFirstQrtDue.Size = New System.Drawing.Size(100, 20)
        Me.dtpFirstQrtDue.TabIndex = 11
        '
        'btnReloadFeeRate
        '
        Me.btnReloadFeeRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReloadFeeRate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReloadFeeRate.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnReloadFeeRate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReloadFeeRate.Location = New System.Drawing.Point(522, 518)
        Me.btnReloadFeeRate.Name = "btnReloadFeeRate"
        Me.btnReloadFeeRate.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnReloadFeeRate.Size = New System.Drawing.Size(100, 23)
        Me.btnReloadFeeRate.TabIndex = 20
        Me.btnReloadFeeRate.Text = "Reload Table"
        Me.btnReloadFeeRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReloadFeeRate.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(779, 433)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(15, 13)
        Me.Label19.TabIndex = 413
        Me.Label19.Text = "%"
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(518, 282)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(95, 13)
        Me.Label37.TabIndex = 412
        Me.Label37.Text = "Annual Due Date: "
        '
        'dtpFeeDueDate
        '
        Me.dtpFeeDueDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFeeDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeeDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeeDueDate.Location = New System.Drawing.Point(695, 280)
        Me.dtpFeeDueDate.Name = "dtpFeeDueDate"
        Me.dtpFeeDueDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpFeeDueDate.TabIndex = 10
        '
        'btnUpdateFeeData
        '
        Me.btnUpdateFeeData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateFeeData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateFeeData.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.btnUpdateFeeData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdateFeeData.Location = New System.Drawing.Point(678, 518)
        Me.btnUpdateFeeData.Name = "btnUpdateFeeData"
        Me.btnUpdateFeeData.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnUpdateFeeData.Size = New System.Drawing.Size(117, 23)
        Me.btnUpdateFeeData.TabIndex = 19
        Me.btnUpdateFeeData.Text = "Update Fee Year"
        Me.btnUpdateFeeData.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUpdateFeeData.UseVisualStyleBackColor = True
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(518, 208)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(38, 13)
        Me.Label36.TabIndex = 408
        Me.Label36.Text = "Notes:"
        '
        'txtFeeNotes
        '
        Me.txtFeeNotes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFeeNotes.Location = New System.Drawing.Point(559, 206)
        Me.txtFeeNotes.Multiline = True
        Me.txtFeeNotes.Name = "txtFeeNotes"
        Me.txtFeeNotes.Size = New System.Drawing.Size(236, 68)
        Me.txtFeeNotes.TabIndex = 9
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(518, 8)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(95, 13)
        Me.Label35.TabIndex = 406
        Me.Label35.Text = "Selected Fee Year"
        '
        'txtFeeYear
        '
        Me.txtFeeYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFeeYear.Location = New System.Drawing.Point(695, 5)
        Me.txtFeeYear.Name = "txtFeeYear"
        Me.txtFeeYear.ReadOnly = True
        Me.txtFeeYear.Size = New System.Drawing.Size(100, 20)
        Me.txtFeeYear.TabIndex = 1
        '
        'Label248
        '
        Me.Label248.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label248.AutoSize = True
        Me.Label248.Location = New System.Drawing.Point(518, 184)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(98, 13)
        Me.Label248.TabIndex = 381
        Me.Label248.Text = "Per Ton Fee Rate: "
        '
        'dtpFeePeriodStart
        '
        Me.dtpFeePeriodStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFeePeriodStart.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeePeriodStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeePeriodStart.Location = New System.Drawing.Point(553, 59)
        Me.dtpFeePeriodStart.Name = "dtpFeePeriodStart"
        Me.dtpFeePeriodStart.Size = New System.Drawing.Size(100, 20)
        Me.dtpFeePeriodStart.TabIndex = 2
        '
        'txtAdminFeePercent
        '
        Me.txtAdminFeePercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAdminFeePercent.Location = New System.Drawing.Point(695, 430)
        Me.txtAdminFeePercent.Name = "txtAdminFeePercent"
        Me.txtAdminFeePercent.Size = New System.Drawing.Size(79, 20)
        Me.txtAdminFeePercent.TabIndex = 16
        Me.txtAdminFeePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dtpFeePeriodEnd
        '
        Me.dtpFeePeriodEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFeePeriodEnd.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeePeriodEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeePeriodEnd.Location = New System.Drawing.Point(695, 59)
        Me.dtpFeePeriodEnd.Name = "dtpFeePeriodEnd"
        Me.dtpFeePeriodEnd.Size = New System.Drawing.Size(100, 20)
        Me.dtpFeePeriodEnd.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(518, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 13)
        Me.Label5.TabIndex = 378
        Me.Label5.Text = "Part 70 Maintenance Fee:"
        '
        'Label55
        '
        Me.Label55.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(518, 87)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(65, 13)
        Me.Label55.TabIndex = 378
        Me.Label55.Text = "Part 70 Fee:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(518, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 401
        Me.Label3.Text = "Start"
        '
        'txtPerTonRate
        '
        Me.txtPerTonRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPerTonRate.Location = New System.Drawing.Point(695, 182)
        Me.txtPerTonRate.Name = "txtPerTonRate"
        Me.txtPerTonRate.Size = New System.Drawing.Size(100, 20)
        Me.txtPerTonRate.TabIndex = 8
        Me.txtPerTonRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(663, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 400
        Me.Label2.Text = "End"
        '
        'Label57
        '
        Me.Label57.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(518, 136)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(83, 13)
        Me.Label57.TabIndex = 379
        Me.Label57.Text = "SM Annual Fee:"
        '
        'txtPart70MaintenanceFee
        '
        Me.txtPart70MaintenanceFee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPart70MaintenanceFee.Location = New System.Drawing.Point(695, 109)
        Me.txtPart70MaintenanceFee.Name = "txtPart70MaintenanceFee"
        Me.txtPart70MaintenanceFee.Size = New System.Drawing.Size(100, 20)
        Me.txtPart70MaintenanceFee.TabIndex = 5
        Me.txtPart70MaintenanceFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(518, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 399
        Me.Label1.Text = "Fee Period"
        '
        'txtPart70Fee
        '
        Me.txtPart70Fee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPart70Fee.Location = New System.Drawing.Point(695, 85)
        Me.txtPart70Fee.Name = "txtPart70Fee"
        Me.txtPart70Fee.Size = New System.Drawing.Size(100, 20)
        Me.txtPart70Fee.TabIndex = 4
        Me.txtPart70Fee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label58
        '
        Me.Label58.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(518, 409)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(138, 13)
        Me.Label58.TabIndex = 383
        Me.Label58.Text = "Date Admin Fee Applicable:"
        '
        'Label59
        '
        Me.Label59.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(518, 160)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(99, 13)
        Me.Label59.TabIndex = 380
        Me.Label59.Text = "NSPS Annual Fee: "
        '
        'dtpAdminApplicableDate
        '
        Me.dtpAdminApplicableDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpAdminApplicableDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpAdminApplicableDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAdminApplicableDate.Location = New System.Drawing.Point(695, 406)
        Me.dtpAdminApplicableDate.Name = "dtpAdminApplicableDate"
        Me.dtpAdminApplicableDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpAdminApplicableDate.TabIndex = 15
        '
        'txtAnnualNSPSFee
        '
        Me.txtAnnualNSPSFee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAnnualNSPSFee.Location = New System.Drawing.Point(695, 158)
        Me.txtAnnualNSPSFee.Name = "txtAnnualNSPSFee"
        Me.txtAnnualNSPSFee.Size = New System.Drawing.Size(100, 20)
        Me.txtAnnualNSPSFee.TabIndex = 7
        Me.txtAnnualNSPSFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label60
        '
        Me.Label60.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(518, 433)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(103, 13)
        Me.Label60.TabIndex = 382
        Me.Label60.Text = "Admin Fee Percent: "
        '
        'txtAnnualSMFee
        '
        Me.txtAnnualSMFee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAnnualSMFee.Location = New System.Drawing.Point(695, 134)
        Me.txtAnnualSMFee.Name = "txtAnnualSMFee"
        Me.txtAnnualSMFee.Size = New System.Drawing.Size(100, 20)
        Me.txtAnnualSMFee.TabIndex = 6
        Me.txtAnnualSMFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlNSPSExemptions)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(804, 601)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "NSPS Exemption Tool"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'pnlNSPSExemptions
        '
        Me.pnlNSPSExemptions.Controls.Add(Me.Panel14)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label109)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUnselectForm)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUpdateNSPSbyYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnViewNSPSExemptionsByYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.dgvNSPSExemptionsByYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label108)
        Me.pnlNSPSExemptions.Controls.Add(Me.cboNSPSExemptionYear)
        Me.pnlNSPSExemptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNSPSExemptions.Location = New System.Drawing.Point(3, 3)
        Me.pnlNSPSExemptions.Name = "pnlNSPSExemptions"
        Me.pnlNSPSExemptions.Size = New System.Drawing.Size(798, 595)
        Me.pnlNSPSExemptions.TabIndex = 400
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Panel13)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel14.Location = New System.Drawing.Point(0, 326)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(798, 269)
        Me.Panel14.TabIndex = 415
        '
        'Panel13
        '
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Controls.Add(Me.Label100)
        Me.Panel13.Controls.Add(Me.dgvNSPSExemptions)
        Me.Panel13.Controls.Add(Me.btnSelectForm)
        Me.Panel13.Controls.Add(Me.btnSelectAllForms)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(798, 269)
        Me.Panel13.TabIndex = 414
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Location = New System.Drawing.Point(3, 11)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(132, 13)
        Me.Label100.TabIndex = 1
        Me.Label100.Text = "Existing NSPS Exemptions"
        '
        'dgvNSPSExemptions
        '
        Me.dgvNSPSExemptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSExemptions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvNSPSExemptions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvNSPSExemptions.Location = New System.Drawing.Point(0, 35)
        Me.dgvNSPSExemptions.MultiSelect = False
        Me.dgvNSPSExemptions.Name = "dgvNSPSExemptions"
        Me.dgvNSPSExemptions.ReadOnly = True
        Me.dgvNSPSExemptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNSPSExemptions.Size = New System.Drawing.Size(796, 232)
        Me.dgvNSPSExemptions.TabIndex = 0
        '
        'btnSelectForm
        '
        Me.btnSelectForm.AutoSize = True
        Me.btnSelectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectForm.Location = New System.Drawing.Point(157, 6)
        Me.btnSelectForm.Name = "btnSelectForm"
        Me.btnSelectForm.Size = New System.Drawing.Size(106, 23)
        Me.btnSelectForm.TabIndex = 408
        Me.btnSelectForm.Text = "Add Selected Row"
        Me.btnSelectForm.UseVisualStyleBackColor = True
        '
        'btnSelectAllForms
        '
        Me.btnSelectAllForms.AutoSize = True
        Me.btnSelectAllForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectAllForms.Location = New System.Drawing.Point(269, 6)
        Me.btnSelectAllForms.Name = "btnSelectAllForms"
        Me.btnSelectAllForms.Size = New System.Drawing.Size(80, 23)
        Me.btnSelectAllForms.TabIndex = 409
        Me.btnSelectAllForms.Text = "Add All Rows"
        Me.btnSelectAllForms.UseVisualStyleBackColor = True
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Location = New System.Drawing.Point(16, 39)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(160, 13)
        Me.Label109.TabIndex = 413
        Me.Label109.Text = "NSPS Exemptions For Fee Year:"
        '
        'btnUnselectForm
        '
        Me.btnUnselectForm.AutoSize = True
        Me.btnUnselectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectForm.Location = New System.Drawing.Point(476, 8)
        Me.btnUnselectForm.Name = "btnUnselectForm"
        Me.btnUnselectForm.Size = New System.Drawing.Size(127, 23)
        Me.btnUnselectForm.TabIndex = 410
        Me.btnUnselectForm.Text = "Remove Selected Row"
        Me.btnUnselectForm.UseVisualStyleBackColor = True
        '
        'btnUpdateNSPSbyYear
        '
        Me.btnUpdateNSPSbyYear.AutoSize = True
        Me.btnUpdateNSPSbyYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateNSPSbyYear.Location = New System.Drawing.Point(325, 8)
        Me.btnUpdateNSPSbyYear.Name = "btnUpdateNSPSbyYear"
        Me.btnUpdateNSPSbyYear.Size = New System.Drawing.Size(113, 23)
        Me.btnUpdateNSPSbyYear.TabIndex = 407
        Me.btnUpdateNSPSbyYear.Text = "Save Exemption List"
        Me.btnUpdateNSPSbyYear.UseVisualStyleBackColor = True
        '
        'btnViewNSPSExemptionsByYear
        '
        Me.btnViewNSPSExemptionsByYear.AutoSize = True
        Me.btnViewNSPSExemptionsByYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewNSPSExemptionsByYear.Location = New System.Drawing.Point(158, 8)
        Me.btnViewNSPSExemptionsByYear.Name = "btnViewNSPSExemptionsByYear"
        Me.btnViewNSPSExemptionsByYear.Size = New System.Drawing.Size(129, 23)
        Me.btnViewNSPSExemptionsByYear.TabIndex = 404
        Me.btnViewNSPSExemptionsByYear.Text = "View selected Fee Year"
        Me.btnViewNSPSExemptionsByYear.UseVisualStyleBackColor = True
        '
        'dgvNSPSExemptionsByYear
        '
        Me.dgvNSPSExemptionsByYear.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNSPSExemptionsByYear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSExemptionsByYear.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvNSPSExemptionsByYear.Location = New System.Drawing.Point(1, 55)
        Me.dgvNSPSExemptionsByYear.MultiSelect = False
        Me.dgvNSPSExemptionsByYear.Name = "dgvNSPSExemptionsByYear"
        Me.dgvNSPSExemptionsByYear.ReadOnly = True
        Me.dgvNSPSExemptionsByYear.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNSPSExemptionsByYear.Size = New System.Drawing.Size(797, 255)
        Me.dgvNSPSExemptionsByYear.TabIndex = 403
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Location = New System.Drawing.Point(16, 13)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(53, 13)
        Me.Label108.TabIndex = 402
        Me.Label108.Text = "Fee Year:"
        '
        'cboNSPSExemptionYear
        '
        Me.cboNSPSExemptionYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNSPSExemptionYear.FormattingEnabled = True
        Me.cboNSPSExemptionYear.Location = New System.Drawing.Point(75, 9)
        Me.cboNSPSExemptionYear.Name = "cboNSPSExemptionYear"
        Me.cboNSPSExemptionYear.Size = New System.Drawing.Size(77, 21)
        Me.cboNSPSExemptionYear.TabIndex = 401
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel15)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(804, 601)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "Edit Exemptions"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel15
        '
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Controls.Add(Me.btnClearNSPSExemptions)
        Me.Panel15.Controls.Add(Me.btnRefreshNSPSExemptions)
        Me.Panel15.Controls.Add(Me.Label4)
        Me.Panel15.Controls.Add(Me.dgvExistingExemptions)
        Me.Panel15.Controls.Add(Me.btnUpdateNSPSExemption)
        Me.Panel15.Controls.Add(Me.btnViewDeletedNSPS)
        Me.Panel15.Controls.Add(Me.txtNSPSExemption)
        Me.Panel15.Controls.Add(Me.Label101)
        Me.Panel15.Controls.Add(Me.btnAddNSPSExemption)
        Me.Panel15.Controls.Add(Me.btnDeleteNSPSExemption)
        Me.Panel15.Controls.Add(Me.txtDeleteNSPSExemptions)
        Me.Panel15.Controls.Add(Me.Label107)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Location = New System.Drawing.Point(0, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(804, 601)
        Me.Panel15.TabIndex = 415
        '
        'btnClearNSPSExemptions
        '
        Me.btnClearNSPSExemptions.AutoSize = True
        Me.btnClearNSPSExemptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearNSPSExemptions.Location = New System.Drawing.Point(129, 6)
        Me.btnClearNSPSExemptions.Name = "btnClearNSPSExemptions"
        Me.btnClearNSPSExemptions.Size = New System.Drawing.Size(41, 23)
        Me.btnClearNSPSExemptions.TabIndex = 492
        Me.btnClearNSPSExemptions.Text = "Clear"
        Me.btnClearNSPSExemptions.UseVisualStyleBackColor = True
        '
        'btnRefreshNSPSExemptions
        '
        Me.btnRefreshNSPSExemptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshNSPSExemptions.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefreshNSPSExemptions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefreshNSPSExemptions.Location = New System.Drawing.Point(133, 205)
        Me.btnRefreshNSPSExemptions.Name = "btnRefreshNSPSExemptions"
        Me.btnRefreshNSPSExemptions.Size = New System.Drawing.Size(68, 24)
        Me.btnRefreshNSPSExemptions.TabIndex = 490
        Me.btnRefreshNSPSExemptions.Text = "Reload"
        Me.btnRefreshNSPSExemptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefreshNSPSExemptions.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 211)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 13)
        Me.Label4.TabIndex = 404
        Me.Label4.Text = "All Existing Exemptions"
        '
        'dgvExistingExemptions
        '
        Me.dgvExistingExemptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvExistingExemptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExistingExemptions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvExistingExemptions.Location = New System.Drawing.Point(0, 235)
        Me.dgvExistingExemptions.MultiSelect = False
        Me.dgvExistingExemptions.Name = "dgvExistingExemptions"
        Me.dgvExistingExemptions.ReadOnly = True
        Me.dgvExistingExemptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvExistingExemptions.Size = New System.Drawing.Size(802, 365)
        Me.dgvExistingExemptions.TabIndex = 403
        '
        'btnUpdateNSPSExemption
        '
        Me.btnUpdateNSPSExemption.AutoSize = True
        Me.btnUpdateNSPSExemption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateNSPSExemption.Location = New System.Drawing.Point(167, 102)
        Me.btnUpdateNSPSExemption.Name = "btnUpdateNSPSExemption"
        Me.btnUpdateNSPSExemption.Size = New System.Drawing.Size(136, 23)
        Me.btnUpdateNSPSExemption.TabIndex = 402
        Me.btnUpdateNSPSExemption.Text = "Update NSPS Exemption"
        Me.btnUpdateNSPSExemption.UseVisualStyleBackColor = True
        '
        'btnViewDeletedNSPS
        '
        Me.btnViewDeletedNSPS.AutoSize = True
        Me.btnViewDeletedNSPS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewDeletedNSPS.Location = New System.Drawing.Point(177, 162)
        Me.btnViewDeletedNSPS.Name = "btnViewDeletedNSPS"
        Me.btnViewDeletedNSPS.Size = New System.Drawing.Size(126, 23)
        Me.btnViewDeletedNSPS.TabIndex = 401
        Me.btnViewDeletedNSPS.Text = "View All Deleted NSPS"
        Me.btnViewDeletedNSPS.UseVisualStyleBackColor = True
        '
        'txtNSPSExemption
        '
        Me.txtNSPSExemption.Location = New System.Drawing.Point(16, 35)
        Me.txtNSPSExemption.Multiline = True
        Me.txtNSPSExemption.Name = "txtNSPSExemption"
        Me.txtNSPSExemption.Size = New System.Drawing.Size(598, 61)
        Me.txtNSPSExemption.TabIndex = 4
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(13, 11)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(104, 13)
        Me.Label101.TabIndex = 3
        Me.Label101.Text = "New NSPS Reason:"
        '
        'btnAddNSPSExemption
        '
        Me.btnAddNSPSExemption.AutoSize = True
        Me.btnAddNSPSExemption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNSPSExemption.Location = New System.Drawing.Point(16, 102)
        Me.btnAddNSPSExemption.Name = "btnAddNSPSExemption"
        Me.btnAddNSPSExemption.Size = New System.Drawing.Size(145, 23)
        Me.btnAddNSPSExemption.TabIndex = 2
        Me.btnAddNSPSExemption.Text = "Add New NSPS Exemption"
        Me.btnAddNSPSExemption.UseVisualStyleBackColor = True
        '
        'btnDeleteNSPSExemption
        '
        Me.btnDeleteNSPSExemption.AutoSize = True
        Me.btnDeleteNSPSExemption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteNSPSExemption.Location = New System.Drawing.Point(90, 162)
        Me.btnDeleteNSPSExemption.Name = "btnDeleteNSPSExemption"
        Me.btnDeleteNSPSExemption.Size = New System.Drawing.Size(80, 23)
        Me.btnDeleteNSPSExemption.TabIndex = 400
        Me.btnDeleteNSPSExemption.Text = "Delete NSPS"
        Me.btnDeleteNSPSExemption.UseVisualStyleBackColor = True
        '
        'txtDeleteNSPSExemptions
        '
        Me.txtDeleteNSPSExemptions.Location = New System.Drawing.Point(16, 164)
        Me.txtDeleteNSPSExemptions.Name = "txtDeleteNSPSExemptions"
        Me.txtDeleteNSPSExemptions.ReadOnly = True
        Me.txtDeleteNSPSExemptions.Size = New System.Drawing.Size(65, 20)
        Me.txtDeleteNSPSExemptions.TabIndex = 5
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Location = New System.Drawing.Point(13, 146)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(96, 13)
        Me.Label107.TabIndex = 6
        Me.Label107.Text = "NSPS ID to Delete"
        '
        'TPFeeManagementTools
        '
        Me.TPFeeManagementTools.Controls.Add(Me.FeeManagementSidePanel)
        Me.TPFeeManagementTools.Controls.Add(Me.FeeManagementToolPanel)
        Me.TPFeeManagementTools.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeManagementTools.Name = "TPFeeManagementTools"
        Me.TPFeeManagementTools.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFeeManagementTools.Size = New System.Drawing.Size(818, 633)
        Me.TPFeeManagementTools.TabIndex = 2
        Me.TPFeeManagementTools.Text = "Fee Management Tools"
        Me.TPFeeManagementTools.UseVisualStyleBackColor = True
        '
        'FeeManagementSidePanel
        '
        Me.FeeManagementSidePanel.Controls.Add(Me.AIRSNumberEntry)
        Me.FeeManagementSidePanel.Controls.Add(Me.dgvFeeManagementLists)
        Me.FeeManagementSidePanel.Controls.Add(Me.FeeManagementListCountLabel)
        Me.FeeManagementSidePanel.Controls.Add(Me.btnOpenFeesLog)
        Me.FeeManagementSidePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FeeManagementSidePanel.Location = New System.Drawing.Point(206, 3)
        Me.FeeManagementSidePanel.Name = "FeeManagementSidePanel"
        Me.FeeManagementSidePanel.Size = New System.Drawing.Size(609, 627)
        Me.FeeManagementSidePanel.TabIndex = 0
        '
        'AIRSNumberEntry
        '
        Me.AIRSNumberEntry.AirsNumber = Nothing
        Me.AIRSNumberEntry.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.AIRSNumberEntry.ErrorMessageLabel = Nothing
        Me.AIRSNumberEntry.FacilityMustExist = True
        Me.AIRSNumberEntry.Location = New System.Drawing.Point(6, 11)
        Me.AIRSNumberEntry.Name = "AIRSNumberEntry"
        Me.AIRSNumberEntry.ReadOnly = False
        Me.AIRSNumberEntry.Size = New System.Drawing.Size(78, 20)
        Me.AIRSNumberEntry.TabIndex = 465
        Me.AIRSNumberEntry.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'dgvFeeManagementLists
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvFeeManagementLists.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvFeeManagementLists.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFeeManagementLists.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvFeeManagementLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFeeManagementLists.DecimalFieldFormat = "G2"
        Me.dgvFeeManagementLists.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvFeeManagementLists.LinkifyColumnByName = Nothing
        Me.dgvFeeManagementLists.LinkifyFirstColumn = True
        Me.dgvFeeManagementLists.Location = New System.Drawing.Point(0, 39)
        Me.dgvFeeManagementLists.Name = "dgvFeeManagementLists"
        Me.dgvFeeManagementLists.ResultsCountLabel = Nothing
        Me.dgvFeeManagementLists.ResultsCountLabelFormat = "{0} found"
        Me.dgvFeeManagementLists.Size = New System.Drawing.Size(609, 586)
        Me.dgvFeeManagementLists.StandardTab = True
        Me.dgvFeeManagementLists.TabIndex = 3
        '
        'FeeManagementListCountLabel
        '
        Me.FeeManagementListCountLabel.AutoSize = True
        Me.FeeManagementListCountLabel.Location = New System.Drawing.Point(197, 14)
        Me.FeeManagementListCountLabel.Name = "FeeManagementListCountLabel"
        Me.FeeManagementListCountLabel.Size = New System.Drawing.Size(35, 13)
        Me.FeeManagementListCountLabel.TabIndex = 464
        Me.FeeManagementListCountLabel.Text = "Count"
        Me.FeeManagementListCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnOpenFeesLog
        '
        Me.btnOpenFeesLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenFeesLog.Location = New System.Drawing.Point(90, 9)
        Me.btnOpenFeesLog.Name = "btnOpenFeesLog"
        Me.btnOpenFeesLog.Size = New System.Drawing.Size(101, 23)
        Me.btnOpenFeesLog.TabIndex = 1
        Me.btnOpenFeesLog.Text = "Open Fees Log"
        Me.btnOpenFeesLog.UseVisualStyleBackColor = True
        '
        'FeeManagementToolPanel
        '
        Me.FeeManagementToolPanel.Controls.Add(Me.MailoutInfoGroupBox)
        Me.FeeManagementToolPanel.Controls.Add(Me.EnrollmentGroupbox)
        Me.FeeManagementToolPanel.Controls.Add(Me.lblFeeDueDate)
        Me.FeeManagementToolPanel.Controls.Add(Me.lblEnrollmentCount)
        Me.FeeManagementToolPanel.Controls.Add(Me.lblMailoutCount)
        Me.FeeManagementToolPanel.Controls.Add(Me.lblFeeYearCount)
        Me.FeeManagementToolPanel.Controls.Add(Me.btnViewMailout)
        Me.FeeManagementToolPanel.Controls.Add(Me.btnViewEnrolledFacilities)
        Me.FeeManagementToolPanel.Controls.Add(Me.btnViewFacilitiesSubjectToFees)
        Me.FeeManagementToolPanel.Controls.Add(Me.cboAvailableFeeYears)
        Me.FeeManagementToolPanel.Controls.Add(Me.Label18)
        Me.FeeManagementToolPanel.Controls.Add(Me.InitialMailoutGroupbox)
        Me.FeeManagementToolPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.FeeManagementToolPanel.Location = New System.Drawing.Point(3, 3)
        Me.FeeManagementToolPanel.Name = "FeeManagementToolPanel"
        Me.FeeManagementToolPanel.Size = New System.Drawing.Size(203, 627)
        Me.FeeManagementToolPanel.TabIndex = 0
        '
        'MailoutInfoGroupBox
        '
        Me.MailoutInfoGroupBox.Controls.Add(Me.lblInitialMailoutDate)
        Me.MailoutInfoGroupBox.Controls.Add(Me.btnSendInitialEmail)
        Me.MailoutInfoGroupBox.Controls.Add(Me.btnViewEmailBatchStatus)
        Me.MailoutInfoGroupBox.Controls.Add(Me.btnViewEmailList)
        Me.MailoutInfoGroupBox.Controls.Add(Me.btnViewPhysicalMailList)
        Me.MailoutInfoGroupBox.Location = New System.Drawing.Point(12, 413)
        Me.MailoutInfoGroupBox.Name = "MailoutInfoGroupBox"
        Me.MailoutInfoGroupBox.Size = New System.Drawing.Size(151, 170)
        Me.MailoutInfoGroupBox.TabIndex = 6
        Me.MailoutInfoGroupBox.TabStop = False
        Me.MailoutInfoGroupBox.Text = "Mailout Information"
        '
        'lblInitialMailoutDate
        '
        Me.lblInitialMailoutDate.Location = New System.Drawing.Point(14, 90)
        Me.lblInitialMailoutDate.Name = "lblInitialMailoutDate"
        Me.lblInitialMailoutDate.Size = New System.Drawing.Size(121, 36)
        Me.lblInitialMailoutDate.TabIndex = 464
        Me.lblInitialMailoutDate.Text = "Initial Mailout Date:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Yesterday"
        Me.lblInitialMailoutDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnSendInitialEmail
        '
        Me.btnSendInitialEmail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSendInitialEmail.Location = New System.Drawing.Point(6, 90)
        Me.btnSendInitialEmail.Name = "btnSendInitialEmail"
        Me.btnSendInitialEmail.Size = New System.Drawing.Size(139, 36)
        Me.btnSendInitialEmail.TabIndex = 3
        Me.btnSendInitialEmail.Text = "Send Initial Email Notification for Fee Year"
        Me.btnSendInitialEmail.UseVisualStyleBackColor = True
        '
        'btnViewEmailBatchStatus
        '
        Me.btnViewEmailBatchStatus.Location = New System.Drawing.Point(6, 132)
        Me.btnViewEmailBatchStatus.Name = "btnViewEmailBatchStatus"
        Me.btnViewEmailBatchStatus.Size = New System.Drawing.Size(139, 23)
        Me.btnViewEmailBatchStatus.TabIndex = 0
        Me.btnViewEmailBatchStatus.Text = "View Email Batch Status"
        Me.btnViewEmailBatchStatus.UseVisualStyleBackColor = True
        '
        'btnViewEmailList
        '
        Me.btnViewEmailList.Location = New System.Drawing.Point(6, 19)
        Me.btnViewEmailList.Name = "btnViewEmailList"
        Me.btnViewEmailList.Size = New System.Drawing.Size(139, 36)
        Me.btnViewEmailList.TabIndex = 0
        Me.btnViewEmailList.Text = "View Email List"
        Me.btnViewEmailList.UseVisualStyleBackColor = True
        '
        'btnViewPhysicalMailList
        '
        Me.btnViewPhysicalMailList.Location = New System.Drawing.Point(6, 61)
        Me.btnViewPhysicalMailList.Name = "btnViewPhysicalMailList"
        Me.btnViewPhysicalMailList.Size = New System.Drawing.Size(139, 23)
        Me.btnViewPhysicalMailList.TabIndex = 0
        Me.btnViewPhysicalMailList.Text = "View Physical Mail List"
        Me.btnViewPhysicalMailList.UseVisualStyleBackColor = True
        '
        'EnrollmentGroupbox
        '
        Me.EnrollmentGroupbox.Controls.Add(Me.btnUnenrollFeeYear)
        Me.EnrollmentGroupbox.Controls.Add(Me.btnFirstEnrollment)
        Me.EnrollmentGroupbox.Location = New System.Drawing.Point(12, 269)
        Me.EnrollmentGroupbox.Name = "EnrollmentGroupbox"
        Me.EnrollmentGroupbox.Size = New System.Drawing.Size(151, 99)
        Me.EnrollmentGroupbox.TabIndex = 5
        Me.EnrollmentGroupbox.TabStop = False
        Me.EnrollmentGroupbox.Text = "Initial Enrollment"
        '
        'btnUnenrollFeeYear
        '
        Me.btnUnenrollFeeYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnenrollFeeYear.Location = New System.Drawing.Point(6, 61)
        Me.btnUnenrollFeeYear.Name = "btnUnenrollFeeYear"
        Me.btnUnenrollFeeYear.Size = New System.Drawing.Size(139, 23)
        Me.btnUnenrollFeeYear.TabIndex = 1
        Me.btnUnenrollFeeYear.Text = "Un-enroll Entire Fee Year"
        Me.btnUnenrollFeeYear.UseVisualStyleBackColor = True
        '
        'btnFirstEnrollment
        '
        Me.btnFirstEnrollment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFirstEnrollment.Location = New System.Drawing.Point(6, 19)
        Me.btnFirstEnrollment.Name = "btnFirstEnrollment"
        Me.btnFirstEnrollment.Size = New System.Drawing.Size(139, 36)
        Me.btnFirstEnrollment.TabIndex = 0
        Me.btnFirstEnrollment.Text = "Enroll All Active Facilities For New Fee Year"
        Me.btnFirstEnrollment.UseVisualStyleBackColor = True
        '
        'lblFeeDueDate
        '
        Me.lblFeeDueDate.Location = New System.Drawing.Point(15, 371)
        Me.lblFeeDueDate.Name = "lblFeeDueDate"
        Me.lblFeeDueDate.Size = New System.Drawing.Size(142, 26)
        Me.lblFeeDueDate.TabIndex = 464
        Me.lblFeeDueDate.Text = "Reporting Due Date:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tomorrow"
        Me.lblFeeDueDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEnrollmentCount
        '
        Me.lblEnrollmentCount.Location = New System.Drawing.Point(162, 102)
        Me.lblEnrollmentCount.Name = "lblEnrollmentCount"
        Me.lblEnrollmentCount.Size = New System.Drawing.Size(35, 13)
        Me.lblEnrollmentCount.TabIndex = 464
        Me.lblEnrollmentCount.Text = "Count"
        Me.lblEnrollmentCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMailoutCount
        '
        Me.lblMailoutCount.Location = New System.Drawing.Point(162, 73)
        Me.lblMailoutCount.Name = "lblMailoutCount"
        Me.lblMailoutCount.Size = New System.Drawing.Size(35, 13)
        Me.lblMailoutCount.TabIndex = 464
        Me.lblMailoutCount.Text = "Count"
        Me.lblMailoutCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFeeYearCount
        '
        Me.lblFeeYearCount.Location = New System.Drawing.Point(162, 44)
        Me.lblFeeYearCount.Name = "lblFeeYearCount"
        Me.lblFeeYearCount.Size = New System.Drawing.Size(35, 13)
        Me.lblFeeYearCount.TabIndex = 464
        Me.lblFeeYearCount.Text = "Count"
        Me.lblFeeYearCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnViewMailout
        '
        Me.btnViewMailout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewMailout.Location = New System.Drawing.Point(18, 68)
        Me.btnViewMailout.Name = "btnViewMailout"
        Me.btnViewMailout.Size = New System.Drawing.Size(139, 23)
        Me.btnViewMailout.TabIndex = 2
        Me.btnViewMailout.Text = "View Mailout Facilities"
        Me.btnViewMailout.UseVisualStyleBackColor = True
        '
        'btnViewEnrolledFacilities
        '
        Me.btnViewEnrolledFacilities.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewEnrolledFacilities.Location = New System.Drawing.Point(18, 97)
        Me.btnViewEnrolledFacilities.Name = "btnViewEnrolledFacilities"
        Me.btnViewEnrolledFacilities.Size = New System.Drawing.Size(139, 23)
        Me.btnViewEnrolledFacilities.TabIndex = 3
        Me.btnViewEnrolledFacilities.Text = "View Enrolled Facilities"
        Me.btnViewEnrolledFacilities.UseVisualStyleBackColor = True
        '
        'btnViewFacilitiesSubjectToFees
        '
        Me.btnViewFacilitiesSubjectToFees.Location = New System.Drawing.Point(18, 39)
        Me.btnViewFacilitiesSubjectToFees.Name = "btnViewFacilitiesSubjectToFees"
        Me.btnViewFacilitiesSubjectToFees.Size = New System.Drawing.Size(139, 23)
        Me.btnViewFacilitiesSubjectToFees.TabIndex = 1
        Me.btnViewFacilitiesSubjectToFees.Text = "View Fee Year"
        Me.btnViewFacilitiesSubjectToFees.UseVisualStyleBackColor = True
        '
        'cboAvailableFeeYears
        '
        Me.cboAvailableFeeYears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAvailableFeeYears.FormattingEnabled = True
        Me.cboAvailableFeeYears.Location = New System.Drawing.Point(74, 11)
        Me.cboAvailableFeeYears.Name = "cboAvailableFeeYears"
        Me.cboAvailableFeeYears.Size = New System.Drawing.Size(83, 21)
        Me.cboAvailableFeeYears.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(15, 14)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(53, 13)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Fee Year:"
        '
        'InitialMailoutGroupbox
        '
        Me.InitialMailoutGroupbox.Controls.Add(Me.btnGenerateMailoutList)
        Me.InitialMailoutGroupbox.Controls.Add(Me.btnUpdateContactData)
        Me.InitialMailoutGroupbox.Location = New System.Drawing.Point(12, 139)
        Me.InitialMailoutGroupbox.Name = "InitialMailoutGroupbox"
        Me.InitialMailoutGroupbox.Size = New System.Drawing.Size(151, 111)
        Me.InitialMailoutGroupbox.TabIndex = 4
        Me.InitialMailoutGroupbox.TabStop = False
        Me.InitialMailoutGroupbox.Text = "Initial Mailout"
        '
        'btnGenerateMailoutList
        '
        Me.btnGenerateMailoutList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGenerateMailoutList.Location = New System.Drawing.Point(6, 19)
        Me.btnGenerateMailoutList.Name = "btnGenerateMailoutList"
        Me.btnGenerateMailoutList.Size = New System.Drawing.Size(139, 36)
        Me.btnGenerateMailoutList.TabIndex = 0
        Me.btnGenerateMailoutList.Text = "Generate Initial " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Mailout List"
        Me.btnGenerateMailoutList.UseVisualStyleBackColor = True
        '
        'btnUpdateContactData
        '
        Me.btnUpdateContactData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateContactData.Location = New System.Drawing.Point(6, 61)
        Me.btnUpdateContactData.Name = "btnUpdateContactData"
        Me.btnUpdateContactData.Size = New System.Drawing.Size(139, 36)
        Me.btnUpdateContactData.TabIndex = 1
        Me.btnUpdateContactData.Text = "Update Mailout List With Current Fee Contacts"
        Me.btnUpdateContactData.UseVisualStyleBackColor = True
        '
        'FeesManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 659)
        Me.Controls.Add(Me.FeeManagementTabControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(842, 698)
        Me.Name = "FeesManagement"
        Me.Text = "Annual Fees - Management"
        Me.FeeManagementTabControl.ResumeLayout(False)
        Me.TPFeeAdminTools.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        CType(Me.dgvFeeRates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.pnlNSPSExemptions.ResumeLayout(False)
        Me.pnlNSPSExemptions.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        CType(Me.dgvNSPSExemptions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNSPSExemptionsByYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        CType(Me.dgvExistingExemptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPFeeManagementTools.ResumeLayout(False)
        Me.FeeManagementSidePanel.ResumeLayout(False)
        Me.FeeManagementSidePanel.PerformLayout()
        CType(Me.dgvFeeManagementLists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FeeManagementToolPanel.ResumeLayout(False)
        Me.FeeManagementToolPanel.PerformLayout()
        Me.MailoutInfoGroupBox.ResumeLayout(False)
        Me.EnrollmentGroupbox.ResumeLayout(False)
        Me.InitialMailoutGroupbox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FeeManagementTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TPFeeAdminTools As System.Windows.Forms.TabPage
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnReloadFeeRate As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents dtpFeeDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnUpdateFeeData As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtFeeNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtFeeYear As System.Windows.Forms.TextBox
    Friend WithEvents Label248 As System.Windows.Forms.Label
    Friend WithEvents dtpFeePeriodStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAdminFeePercent As System.Windows.Forms.TextBox
    Friend WithEvents dtpFeePeriodEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPerTonRate As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPart70Fee As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents dtpAdminApplicableDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAnnualNSPSFee As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtAnnualSMFee As System.Windows.Forms.TextBox
    Friend WithEvents dgvFeeRates As IaipDataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents pnlNSPSExemptions As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents dgvNSPSExemptions As System.Windows.Forms.DataGridView
    Friend WithEvents btnSelectForm As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllForms As System.Windows.Forms.Button
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents btnUpdateNSPSExemption As System.Windows.Forms.Button
    Friend WithEvents btnViewDeletedNSPS As System.Windows.Forms.Button
    Friend WithEvents txtNSPSExemption As System.Windows.Forms.TextBox
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents btnAddNSPSExemption As System.Windows.Forms.Button
    Friend WithEvents btnDeleteNSPSExemption As System.Windows.Forms.Button
    Friend WithEvents txtDeleteNSPSExemptions As System.Windows.Forms.TextBox
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents btnUnselectForm As System.Windows.Forms.Button
    Friend WithEvents btnUpdateNSPSbyYear As System.Windows.Forms.Button
    Friend WithEvents btnViewNSPSExemptionsByYear As System.Windows.Forms.Button
    Friend WithEvents dgvNSPSExemptionsByYear As System.Windows.Forms.DataGridView
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents cboNSPSExemptionYear As System.Windows.Forms.ComboBox
    Friend WithEvents TPFeeManagementTools As System.Windows.Forms.TabPage
    Friend WithEvents dgvFeeManagementLists As IaipDataGridView
    Friend WithEvents FeeManagementToolPanel As System.Windows.Forms.Panel
    Friend WithEvents FeeManagementListCountLabel As System.Windows.Forms.Label
    Friend WithEvents btnGenerateMailoutList As System.Windows.Forms.Button
    Friend WithEvents btnUnenrollFeeYear As System.Windows.Forms.Button
    Friend WithEvents cboAvailableFeeYears As System.Windows.Forms.ComboBox
    Friend WithEvents btnFirstEnrollment As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgvExistingExemptions As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefreshNSPSExemptions As System.Windows.Forms.Button
    Friend WithEvents btnClearNSPSExemptions As System.Windows.Forms.Button
    Friend WithEvents btnViewFacilitiesSubjectToFees As System.Windows.Forms.Button
    Friend WithEvents btnUpdateContactData As System.Windows.Forms.Button
    Friend WithEvents btnSendInitialEmail As System.Windows.Forms.Button
    Friend WithEvents btnViewEnrolledFacilities As System.Windows.Forms.Button
    Friend WithEvents btnViewMailout As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpFourthQrtDue As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpThirdQrtDue As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpSecondQrtDue As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpFirstQrtDue As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnOpenFeesLog As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNonAttainmentThreshold As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtAttainmentThreshold As System.Windows.Forms.TextBox
    Friend WithEvents InitialMailoutGroupbox As System.Windows.Forms.GroupBox
    Friend WithEvents EnrollmentGroupbox As System.Windows.Forms.GroupBox
    Friend WithEvents FeeManagementSidePanel As System.Windows.Forms.Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents txtPart70MaintenanceFee As TextBox
    Friend WithEvents MailoutInfoGroupBox As GroupBox
    Friend WithEvents btnViewEmailList As Button
    Friend WithEvents btnViewPhysicalMailList As Button
    Friend WithEvents AIRSNumberEntry As AirsNumberEntryForm
    Friend WithEvents lblEnrollmentCount As Label
    Friend WithEvents lblMailoutCount As Label
    Friend WithEvents lblFeeYearCount As Label
    Friend WithEvents lblInitialMailoutDate As Label
    Friend WithEvents lblFeeDueDate As Label
    Friend WithEvents btnViewEmailBatchStatus As Button
End Class
