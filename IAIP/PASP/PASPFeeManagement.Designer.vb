<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPFeeManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPFeeManagement))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPFeeAdminTools = New System.Windows.Forms.TabPage
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtNonAttainmentThreshold = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtAttainmentThreshold = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.dtpFourthQrtDue = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.dtpThirdQrtDue = New System.Windows.Forms.DateTimePicker
        Me.Label11 = New System.Windows.Forms.Label
        Me.dtpSecondQrtDue = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtpFirstQrtDue = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnReloadFeeRate = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.dtpFeeDueDate = New System.Windows.Forms.DateTimePicker
        Me.btnViewDeletedFeeRates = New System.Windows.Forms.Button
        Me.btnDeleteFeeRate = New System.Windows.Forms.Button
        Me.btnClearFeeData = New System.Windows.Forms.Button
        Me.btnUpdateFeeData = New System.Windows.Forms.Button
        Me.Label36 = New System.Windows.Forms.Label
        Me.txtFeeNotes = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtFeeYear = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtFeeID = New System.Windows.Forms.TextBox
        Me.Label248 = New System.Windows.Forms.Label
        Me.dtpFeePeriodStart = New System.Windows.Forms.DateTimePicker
        Me.txtAdminFeePercent = New System.Windows.Forms.TextBox
        Me.dtpFeePeriodEnd = New System.Windows.Forms.DateTimePicker
        Me.Label55 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtperTonRate = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTitleVfee = New System.Windows.Forms.TextBox
        Me.Label58 = New System.Windows.Forms.Label
        Me.btnsaveRate = New System.Windows.Forms.Button
        Me.Label59 = New System.Windows.Forms.Label
        Me.dtpAdminApplicable = New System.Windows.Forms.DateTimePicker
        Me.txtAnnualNSPSFee = New System.Windows.Forms.TextBox
        Me.Label60 = New System.Windows.Forms.Label
        Me.txtAnnualSMFee = New System.Windows.Forms.TextBox
        Me.dgvFeeRates = New System.Windows.Forms.DataGridView
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.pnlNSPSExemptions = New System.Windows.Forms.Panel
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.Label100 = New System.Windows.Forms.Label
        Me.dgvNSPSExemptions = New System.Windows.Forms.DataGridView
        Me.btnSelectForm = New System.Windows.Forms.Button
        Me.btnSelectAllForms = New System.Windows.Forms.Button
        Me.Label109 = New System.Windows.Forms.Label
        Me.btnUnselectForm = New System.Windows.Forms.Button
        Me.btnUnselectAllForms = New System.Windows.Forms.Button
        Me.btnUpdateNSPSbyYear = New System.Windows.Forms.Button
        Me.btnViewNSPSExemptionsByYear = New System.Windows.Forms.Button
        Me.dgvNSPSExemptionsByYear = New System.Windows.Forms.DataGridView
        Me.Label108 = New System.Windows.Forms.Label
        Me.cboNSPSExemptionYear = New System.Windows.Forms.ComboBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.btnClearNSPSExemptions = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnRefreshNSPSExemptions = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.dgvExistingExemptions = New System.Windows.Forms.DataGridView
        Me.btnUpdateNSPSExemption = New System.Windows.Forms.Button
        Me.btnViewDeletedNSPS = New System.Windows.Forms.Button
        Me.txtNSPSExemption = New System.Windows.Forms.TextBox
        Me.Label101 = New System.Windows.Forms.Label
        Me.btnAddNSPSExemption = New System.Windows.Forms.Button
        Me.btnDeleteNSPSExemption = New System.Windows.Forms.Button
        Me.txtDeleteNSPSExemptions = New System.Windows.Forms.TextBox
        Me.Label107 = New System.Windows.Forms.Label
        Me.TPFeeManagementTools = New System.Windows.Forms.TabPage
        Me.btnOpenFeesLog = New System.Windows.Forms.Button
        Me.lblCheckState = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCheckFacility = New System.Windows.Forms.TextBox
        Me.llbCheckFacility = New System.Windows.Forms.LinkLabel
        Me.btnSaveAddition = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.mtbCheckAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.dgvFeeManagmentLists = New System.Windows.Forms.DataGridView
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.btnViewEnrolledFacilities = New System.Windows.Forms.Button
        Me.btnViewMailout = New System.Windows.Forms.Button
        Me.btnSetMailoutDate = New System.Windows.Forms.Button
        Me.btnUpdateContactData = New System.Windows.Forms.Button
        Me.dtpDateMailoutSent = New System.Windows.Forms.DateTimePicker
        Me.btnViewFacilitiesSubjectToFees = New System.Windows.Forms.Button
        Me.btnGenerateMailoutList = New System.Windows.Forms.Button
        Me.btnUnenrollFeeYear = New System.Windows.Forms.Button
        Me.cboAvailableFeeYears = New System.Windows.Forms.ComboBox
        Me.btnFirstEnrollment = New System.Windows.Forms.Button
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnExportToExcel = New System.Windows.Forms.Button
        Me.txtCount = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.TPWebTools = New System.Windows.Forms.TabPage
        Me.TabControl3 = New System.Windows.Forms.TabControl
        Me.TPWebUsers = New System.Windows.Forms.TabPage
        Me.pnlUser = New System.Windows.Forms.Panel
        Me.dgvUsers = New System.Windows.Forms.DataGridView
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.lblFaciltyName = New System.Windows.Forms.Label
        Me.lblFacility = New System.Windows.Forms.Label
        Me.cboUsers = New System.Windows.Forms.ComboBox
        Me.Label177 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.llbViewUserData = New System.Windows.Forms.LinkLabel
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnAddUser = New System.Windows.Forms.Button
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.TPWebUsers1 = New System.Windows.Forms.TabPage
        Me.pnlUserFacility = New System.Windows.Forms.Panel
        Me.dgvUserFacilities = New System.Windows.Forms.DataGridView
        Me.pnlUserInfo = New System.Windows.Forms.Panel
        Me.btnChangeEmailAddress = New System.Windows.Forms.Button
        Me.mtbFacilityToAdd = New System.Windows.Forms.MaskedTextBox
        Me.txtEditEmail = New System.Windows.Forms.TextBox
        Me.cboFacilityToDelete = New System.Windows.Forms.ComboBox
        Me.lblConfirmDate = New System.Windows.Forms.Label
        Me.Label75 = New System.Windows.Forms.Label
        Me.lblLastLogIn = New System.Windows.Forms.Label
        Me.btnDeleteFacilityUser = New System.Windows.Forms.Button
        Me.txtEditUserPassword = New System.Windows.Forms.TextBox
        Me.btnUpdateUser = New System.Windows.Forms.Button
        Me.Label53 = New System.Windows.Forms.Label
        Me.btnUpdatePassword = New System.Windows.Forms.Button
        Me.btnAddFacilitytoUser = New System.Windows.Forms.Button
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
        Me.pnlUserEmail = New System.Windows.Forms.Panel
        Me.lblViewEmailData = New System.Windows.Forms.LinkLabel
        Me.Label39 = New System.Windows.Forms.Label
        Me.txtWebUserEmail = New System.Windows.Forms.TextBox
        Me.cboUserEmail = New System.Windows.Forms.ComboBox
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
        Me.TabControl1.SuspendLayout()
        Me.TPFeeAdminTools.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel6.SuspendLayout()
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
        CType(Me.dgvFeeManagmentLists, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.TPWebTools.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TPWebUsers.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFacility.SuspendLayout()
        Me.TPWebUsers1.SuspendLayout()
        Me.pnlUserFacility.SuspendLayout()
        CType(Me.dgvUserFacilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUserInfo.SuspendLayout()
        Me.pnlUserEmail.SuspendLayout()
        Me.TPActivate.SuspendLayout()
        Me.TPFeeFacility.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPFeeAdminTools)
        Me.TabControl1.Controls.Add(Me.TPFeeManagementTools)
        Me.TabControl1.Controls.Add(Me.TPWebTools)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1001, 692)
        Me.TabControl1.TabIndex = 257
        '
        'TPFeeAdminTools
        '
        Me.TPFeeAdminTools.Controls.Add(Me.TabControl2)
        Me.TPFeeAdminTools.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeAdminTools.Name = "TPFeeAdminTools"
        Me.TPFeeAdminTools.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFeeAdminTools.Size = New System.Drawing.Size(993, 666)
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
        Me.TabControl2.Size = New System.Drawing.Size(987, 660)
        Me.TabControl2.TabIndex = 402
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(979, 634)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Annual Fee Rates"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel12)
        Me.Panel6.Controls.Add(Me.dgvFeeRates)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(973, 628)
        Me.Panel6.TabIndex = 399
        '
        'Panel12
        '
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
        Me.Panel12.Controls.Add(Me.Label7)
        Me.Panel12.Controls.Add(Me.btnReloadFeeRate)
        Me.Panel12.Controls.Add(Me.Label19)
        Me.Panel12.Controls.Add(Me.Label37)
        Me.Panel12.Controls.Add(Me.dtpFeeDueDate)
        Me.Panel12.Controls.Add(Me.btnViewDeletedFeeRates)
        Me.Panel12.Controls.Add(Me.btnDeleteFeeRate)
        Me.Panel12.Controls.Add(Me.btnClearFeeData)
        Me.Panel12.Controls.Add(Me.btnUpdateFeeData)
        Me.Panel12.Controls.Add(Me.Label36)
        Me.Panel12.Controls.Add(Me.txtFeeNotes)
        Me.Panel12.Controls.Add(Me.Label35)
        Me.Panel12.Controls.Add(Me.txtFeeYear)
        Me.Panel12.Controls.Add(Me.Label34)
        Me.Panel12.Controls.Add(Me.txtFeeID)
        Me.Panel12.Controls.Add(Me.Label248)
        Me.Panel12.Controls.Add(Me.dtpFeePeriodStart)
        Me.Panel12.Controls.Add(Me.txtAdminFeePercent)
        Me.Panel12.Controls.Add(Me.dtpFeePeriodEnd)
        Me.Panel12.Controls.Add(Me.Label55)
        Me.Panel12.Controls.Add(Me.Label3)
        Me.Panel12.Controls.Add(Me.txtperTonRate)
        Me.Panel12.Controls.Add(Me.Label2)
        Me.Panel12.Controls.Add(Me.Label57)
        Me.Panel12.Controls.Add(Me.Label1)
        Me.Panel12.Controls.Add(Me.txtTitleVfee)
        Me.Panel12.Controls.Add(Me.Label58)
        Me.Panel12.Controls.Add(Me.btnsaveRate)
        Me.Panel12.Controls.Add(Me.Label59)
        Me.Panel12.Controls.Add(Me.dtpAdminApplicable)
        Me.Panel12.Controls.Add(Me.txtAnnualNSPSFee)
        Me.Panel12.Controls.Add(Me.Label60)
        Me.Panel12.Controls.Add(Me.txtAnnualSMFee)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(628, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(345, 628)
        Me.Panel12.TabIndex = 405
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(-2, 521)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(133, 13)
        Me.Label16.TabIndex = 501
        Me.Label16.Text = "Non-Attainment Threshold:"
        '
        'txtNonAttainmentThreshold
        '
        Me.txtNonAttainmentThreshold.Location = New System.Drawing.Point(137, 521)
        Me.txtNonAttainmentThreshold.Name = "txtNonAttainmentThreshold"
        Me.txtNonAttainmentThreshold.Size = New System.Drawing.Size(89, 20)
        Me.txtNonAttainmentThreshold.TabIndex = 500
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(21, 496)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 13)
        Me.Label15.TabIndex = 499
        Me.Label15.Text = "Attainment Threshold:"
        '
        'txtAttainmentThreshold
        '
        Me.txtAttainmentThreshold.Location = New System.Drawing.Point(137, 493)
        Me.txtAttainmentThreshold.Name = "txtAttainmentThreshold"
        Me.txtAttainmentThreshold.Size = New System.Drawing.Size(89, 20)
        Me.txtAttainmentThreshold.TabIndex = 498
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 441)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 13)
        Me.Label12.TabIndex = 497
        Me.Label12.Text = "4th Quarter Due Date:"
        '
        'dtpFourthQrtDue
        '
        Me.dtpFourthQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFourthQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFourthQrtDue.Location = New System.Drawing.Point(137, 437)
        Me.dtpFourthQrtDue.Name = "dtpFourthQrtDue"
        Me.dtpFourthQrtDue.Size = New System.Drawing.Size(89, 20)
        Me.dtpFourthQrtDue.TabIndex = 496
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 415)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 13)
        Me.Label14.TabIndex = 495
        Me.Label14.Text = "3rd Quarter Due Date:"
        '
        'dtpThirdQrtDue
        '
        Me.dtpThirdQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpThirdQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpThirdQrtDue.Location = New System.Drawing.Point(137, 411)
        Me.dtpThirdQrtDue.Name = "dtpThirdQrtDue"
        Me.dtpThirdQrtDue.Size = New System.Drawing.Size(89, 20)
        Me.dtpThirdQrtDue.TabIndex = 494
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 390)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(115, 13)
        Me.Label11.TabIndex = 493
        Me.Label11.Text = "2nd Quarter Due Date:"
        '
        'dtpSecondQrtDue
        '
        Me.dtpSecondQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpSecondQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSecondQrtDue.Location = New System.Drawing.Point(137, 386)
        Me.dtpSecondQrtDue.Name = "dtpSecondQrtDue"
        Me.dtpSecondQrtDue.Size = New System.Drawing.Size(89, 20)
        Me.dtpSecondQrtDue.TabIndex = 492
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 364)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(111, 13)
        Me.Label9.TabIndex = 491
        Me.Label9.Text = "1st Quarter Due Date:"
        '
        'dtpFirstQrtDue
        '
        Me.dtpFirstQrtDue.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFirstQrtDue.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFirstQrtDue.Location = New System.Drawing.Point(137, 360)
        Me.dtpFirstQrtDue.Name = "dtpFirstQrtDue"
        Me.dtpFirstQrtDue.Size = New System.Drawing.Size(89, 20)
        Me.dtpFirstQrtDue.TabIndex = 490
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(267, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 489
        Me.Label7.Text = "Reload"
        '
        'btnReloadFeeRate
        '
        Me.btnReloadFeeRate.AutoSize = True
        Me.btnReloadFeeRate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReloadFeeRate.Image = CType(resources.GetObject("btnReloadFeeRate.Image"), System.Drawing.Image)
        Me.btnReloadFeeRate.Location = New System.Drawing.Point(239, 9)
        Me.btnReloadFeeRate.Name = "btnReloadFeeRate"
        Me.btnReloadFeeRate.Size = New System.Drawing.Size(22, 22)
        Me.btnReloadFeeRate.TabIndex = 488
        Me.btnReloadFeeRate.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(232, 219)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(15, 13)
        Me.Label19.TabIndex = 413
        Me.Label19.Text = "%"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(9, 195)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(95, 13)
        Me.Label37.TabIndex = 412
        Me.Label37.Text = "Annual Due Date: "
        '
        'dtpFeeDueDate
        '
        Me.dtpFeeDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeeDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeeDueDate.Location = New System.Drawing.Point(137, 191)
        Me.dtpFeeDueDate.Name = "dtpFeeDueDate"
        Me.dtpFeeDueDate.Size = New System.Drawing.Size(89, 20)
        Me.dtpFeeDueDate.TabIndex = 411
        '
        'btnViewDeletedFeeRates
        '
        Me.btnViewDeletedFeeRates.AutoSize = True
        Me.btnViewDeletedFeeRates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewDeletedFeeRates.Location = New System.Drawing.Point(134, 570)
        Me.btnViewDeletedFeeRates.Name = "btnViewDeletedFeeRates"
        Me.btnViewDeletedFeeRates.Size = New System.Drawing.Size(143, 23)
        Me.btnViewDeletedFeeRates.TabIndex = 410
        Me.btnViewDeletedFeeRates.Text = "View all deleted Fee Rates"
        Me.btnViewDeletedFeeRates.UseVisualStyleBackColor = True
        Me.btnViewDeletedFeeRates.Visible = False
        '
        'btnDeleteFeeRate
        '
        Me.btnDeleteFeeRate.AutoSize = True
        Me.btnDeleteFeeRate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteFeeRate.Location = New System.Drawing.Point(8, 570)
        Me.btnDeleteFeeRate.Name = "btnDeleteFeeRate"
        Me.btnDeleteFeeRate.Size = New System.Drawing.Size(121, 23)
        Me.btnDeleteFeeRate.TabIndex = 409
        Me.btnDeleteFeeRate.Text = "Delete Fee Rate Data"
        Me.btnDeleteFeeRate.UseVisualStyleBackColor = True
        Me.btnDeleteFeeRate.Visible = False
        '
        'btnClearFeeData
        '
        Me.btnClearFeeData.AutoSize = True
        Me.btnClearFeeData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearFeeData.Location = New System.Drawing.Point(170, 9)
        Me.btnClearFeeData.Name = "btnClearFeeData"
        Me.btnClearFeeData.Size = New System.Drawing.Size(41, 23)
        Me.btnClearFeeData.TabIndex = 3
        Me.btnClearFeeData.Text = "Clear"
        Me.btnClearFeeData.UseVisualStyleBackColor = True
        '
        'btnUpdateFeeData
        '
        Me.btnUpdateFeeData.AutoSize = True
        Me.btnUpdateFeeData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateFeeData.Location = New System.Drawing.Point(137, 463)
        Me.btnUpdateFeeData.Name = "btnUpdateFeeData"
        Me.btnUpdateFeeData.Size = New System.Drawing.Size(125, 23)
        Me.btnUpdateFeeData.TabIndex = 15
        Me.btnUpdateFeeData.Text = "Update Fee Rate Data"
        Me.btnUpdateFeeData.UseVisualStyleBackColor = True
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(5, 265)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(35, 13)
        Me.Label36.TabIndex = 408
        Me.Label36.Text = "Notes"
        '
        'txtFeeNotes
        '
        Me.txtFeeNotes.Location = New System.Drawing.Point(46, 265)
        Me.txtFeeNotes.Multiline = True
        Me.txtFeeNotes.Name = "txtFeeNotes"
        Me.txtFeeNotes.Size = New System.Drawing.Size(268, 85)
        Me.txtFeeNotes.TabIndex = 13
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(12, 14)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(50, 13)
        Me.Label35.TabIndex = 406
        Me.Label35.Text = "Fee Year"
        '
        'txtFeeYear
        '
        Me.txtFeeYear.Location = New System.Drawing.Point(75, 10)
        Me.txtFeeYear.Name = "txtFeeYear"
        Me.txtFeeYear.ReadOnly = True
        Me.txtFeeYear.Size = New System.Drawing.Size(89, 20)
        Me.txtFeeYear.TabIndex = 4
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(274, 580)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(18, 13)
        Me.Label34.TabIndex = 404
        Me.Label34.Text = "ID"
        Me.Label34.Visible = False
        '
        'txtFeeID
        '
        Me.txtFeeID.Location = New System.Drawing.Point(298, 574)
        Me.txtFeeID.Name = "txtFeeID"
        Me.txtFeeID.ReadOnly = True
        Me.txtFeeID.Size = New System.Drawing.Size(44, 20)
        Me.txtFeeID.TabIndex = 2
        Me.txtFeeID.Visible = False
        '
        'Label248
        '
        Me.Label248.AutoSize = True
        Me.Label248.Location = New System.Drawing.Point(9, 171)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(82, 13)
        Me.Label248.TabIndex = 381
        Me.Label248.Text = "Per Ton Rates: "
        '
        'dtpFeePeriodStart
        '
        Me.dtpFeePeriodStart.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeePeriodStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeePeriodStart.Location = New System.Drawing.Point(28, 51)
        Me.dtpFeePeriodStart.Name = "dtpFeePeriodStart"
        Me.dtpFeePeriodStart.Size = New System.Drawing.Size(100, 20)
        Me.dtpFeePeriodStart.TabIndex = 5
        '
        'txtAdminFeePercent
        '
        Me.txtAdminFeePercent.Location = New System.Drawing.Point(137, 215)
        Me.txtAdminFeePercent.Name = "txtAdminFeePercent"
        Me.txtAdminFeePercent.Size = New System.Drawing.Size(89, 20)
        Me.txtAdminFeePercent.TabIndex = 11
        '
        'dtpFeePeriodEnd
        '
        Me.dtpFeePeriodEnd.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeePeriodEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeePeriodEnd.Location = New System.Drawing.Point(134, 51)
        Me.dtpFeePeriodEnd.Name = "dtpFeePeriodEnd"
        Me.dtpFeePeriodEnd.Size = New System.Drawing.Size(100, 20)
        Me.dtpFeePeriodEnd.TabIndex = 6
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(9, 99)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(122, 13)
        Me.Label55.TabIndex = 378
        Me.Label55.Text = "Title V Fee (Part70 Fee):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 401
        Me.Label3.Text = "Start"
        '
        'txtperTonRate
        '
        Me.txtperTonRate.Location = New System.Drawing.Point(137, 167)
        Me.txtperTonRate.Name = "txtperTonRate"
        Me.txtperTonRate.Size = New System.Drawing.Size(89, 20)
        Me.txtperTonRate.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(134, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 400
        Me.Label2.Text = "End"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(9, 123)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(47, 13)
        Me.Label57.TabIndex = 379
        Me.Label57.Text = "SM Fee:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 399
        Me.Label1.Text = "Fee Period"
        '
        'txtTitleVfee
        '
        Me.txtTitleVfee.Location = New System.Drawing.Point(137, 95)
        Me.txtTitleVfee.Name = "txtTitleVfee"
        Me.txtTitleVfee.Size = New System.Drawing.Size(89, 20)
        Me.txtTitleVfee.TabIndex = 7
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(9, 243)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(89, 13)
        Me.Label58.TabIndex = 383
        Me.Label58.Text = "Admin Fee Date: "
        '
        'btnsaveRate
        '
        Me.btnsaveRate.AutoSize = True
        Me.btnsaveRate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnsaveRate.Location = New System.Drawing.Point(11, 541)
        Me.btnsaveRate.Name = "btnsaveRate"
        Me.btnsaveRate.Size = New System.Drawing.Size(109, 23)
        Me.btnsaveRate.TabIndex = 14
        Me.btnsaveRate.Text = "Add Fee Rate Data"
        Me.btnsaveRate.UseVisualStyleBackColor = True
        Me.btnsaveRate.Visible = False
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(9, 147)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(63, 13)
        Me.Label59.TabIndex = 380
        Me.Label59.Text = "NSPS Fee: "
        '
        'dtpAdminApplicable
        '
        Me.dtpAdminApplicable.CustomFormat = "dd-MMM-yyyy"
        Me.dtpAdminApplicable.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAdminApplicable.Location = New System.Drawing.Point(137, 239)
        Me.dtpAdminApplicable.Name = "dtpAdminApplicable"
        Me.dtpAdminApplicable.Size = New System.Drawing.Size(89, 20)
        Me.dtpAdminApplicable.TabIndex = 12
        '
        'txtAnnualNSPSFee
        '
        Me.txtAnnualNSPSFee.Location = New System.Drawing.Point(137, 143)
        Me.txtAnnualNSPSFee.Name = "txtAnnualNSPSFee"
        Me.txtAnnualNSPSFee.Size = New System.Drawing.Size(89, 20)
        Me.txtAnnualNSPSFee.TabIndex = 9
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(9, 219)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(103, 13)
        Me.Label60.TabIndex = 382
        Me.Label60.Text = "Admin Fee Percent: "
        '
        'txtAnnualSMFee
        '
        Me.txtAnnualSMFee.Location = New System.Drawing.Point(137, 119)
        Me.txtAnnualSMFee.Name = "txtAnnualSMFee"
        Me.txtAnnualSMFee.Size = New System.Drawing.Size(89, 20)
        Me.txtAnnualSMFee.TabIndex = 8
        '
        'dgvFeeRates
        '
        Me.dgvFeeRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFeeRates.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgvFeeRates.Location = New System.Drawing.Point(0, 0)
        Me.dgvFeeRates.Name = "dgvFeeRates"
        Me.dgvFeeRates.Size = New System.Drawing.Size(628, 628)
        Me.dgvFeeRates.TabIndex = 404
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlNSPSExemptions)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(979, 634)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "NSPS Exemption Tool"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'pnlNSPSExemptions
        '
        Me.pnlNSPSExemptions.Controls.Add(Me.Panel14)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label109)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUnselectForm)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUnselectAllForms)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnUpdateNSPSbyYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.btnViewNSPSExemptionsByYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.dgvNSPSExemptionsByYear)
        Me.pnlNSPSExemptions.Controls.Add(Me.Label108)
        Me.pnlNSPSExemptions.Controls.Add(Me.cboNSPSExemptionYear)
        Me.pnlNSPSExemptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNSPSExemptions.Location = New System.Drawing.Point(3, 3)
        Me.pnlNSPSExemptions.Name = "pnlNSPSExemptions"
        Me.pnlNSPSExemptions.Size = New System.Drawing.Size(973, 628)
        Me.pnlNSPSExemptions.TabIndex = 400
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Panel13)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel14.Location = New System.Drawing.Point(0, 409)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(973, 219)
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
        Me.Panel13.Size = New System.Drawing.Size(973, 219)
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
        Me.dgvNSPSExemptions.Location = New System.Drawing.Point(0, 35)
        Me.dgvNSPSExemptions.Name = "dgvNSPSExemptions"
        Me.dgvNSPSExemptions.ReadOnly = True
        Me.dgvNSPSExemptions.Size = New System.Drawing.Size(971, 182)
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
        Me.btnSelectAllForms.Size = New System.Drawing.Size(96, 23)
        Me.btnSelectAllForms.TabIndex = 409
        Me.btnSelectAllForms.Text = "Select All Entries"
        Me.btnSelectAllForms.UseVisualStyleBackColor = True
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Location = New System.Drawing.Point(16, 39)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(136, 13)
        Me.Label109.TabIndex = 413
        Me.Label109.Text = "NSPS Exemptions By Year:"
        '
        'btnUnselectForm
        '
        Me.btnUnselectForm.AutoSize = True
        Me.btnUnselectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectForm.Location = New System.Drawing.Point(465, 8)
        Me.btnUnselectForm.Name = "btnUnselectForm"
        Me.btnUnselectForm.Size = New System.Drawing.Size(127, 23)
        Me.btnUnselectForm.TabIndex = 410
        Me.btnUnselectForm.Text = "Remove Selected Row"
        Me.btnUnselectForm.UseVisualStyleBackColor = True
        '
        'btnUnselectAllForms
        '
        Me.btnUnselectAllForms.AutoSize = True
        Me.btnUnselectAllForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectAllForms.Location = New System.Drawing.Point(668, 7)
        Me.btnUnselectAllForms.Name = "btnUnselectAllForms"
        Me.btnUnselectAllForms.Size = New System.Drawing.Size(90, 23)
        Me.btnUnselectAllForms.TabIndex = 411
        Me.btnUnselectAllForms.Text = "Clear Entire List"
        Me.btnUnselectAllForms.UseVisualStyleBackColor = True
        Me.btnUnselectAllForms.Visible = False
        '
        'btnUpdateNSPSbyYear
        '
        Me.btnUpdateNSPSbyYear.AutoSize = True
        Me.btnUpdateNSPSbyYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateNSPSbyYear.Location = New System.Drawing.Point(346, 7)
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
        Me.btnViewNSPSExemptionsByYear.Location = New System.Drawing.Point(158, 7)
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
        Me.dgvNSPSExemptionsByYear.Location = New System.Drawing.Point(19, 55)
        Me.dgvNSPSExemptionsByYear.Name = "dgvNSPSExemptionsByYear"
        Me.dgvNSPSExemptionsByYear.ReadOnly = True
        Me.dgvNSPSExemptionsByYear.Size = New System.Drawing.Size(914, 326)
        Me.dgvNSPSExemptionsByYear.TabIndex = 403
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Location = New System.Drawing.Point(16, 12)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(53, 13)
        Me.Label108.TabIndex = 402
        Me.Label108.Text = "Fee Year:"
        '
        'cboNSPSExemptionYear
        '
        Me.cboNSPSExemptionYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNSPSExemptionYear.FormattingEnabled = True
        Me.cboNSPSExemptionYear.Location = New System.Drawing.Point(75, 8)
        Me.cboNSPSExemptionYear.Name = "cboNSPSExemptionYear"
        Me.cboNSPSExemptionYear.Size = New System.Drawing.Size(77, 21)
        Me.cboNSPSExemptionYear.TabIndex = 401
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel15)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(979, 634)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "Edit Exemptions"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel15
        '
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Controls.Add(Me.btnClearNSPSExemptions)
        Me.Panel15.Controls.Add(Me.Label5)
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
        Me.Panel15.Size = New System.Drawing.Size(979, 634)
        Me.Panel15.TabIndex = 415
        '
        'btnClearNSPSExemptions
        '
        Me.btnClearNSPSExemptions.AutoSize = True
        Me.btnClearNSPSExemptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearNSPSExemptions.Location = New System.Drawing.Point(123, 6)
        Me.btnClearNSPSExemptions.Name = "btnClearNSPSExemptions"
        Me.btnClearNSPSExemptions.Size = New System.Drawing.Size(41, 23)
        Me.btnClearNSPSExemptions.TabIndex = 492
        Me.btnClearNSPSExemptions.Text = "Clear"
        Me.btnClearNSPSExemptions.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(146, 273)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 491
        Me.Label5.Text = "Reload"
        '
        'btnRefreshNSPSExemptions
        '
        Me.btnRefreshNSPSExemptions.AutoSize = True
        Me.btnRefreshNSPSExemptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshNSPSExemptions.Image = CType(resources.GetObject("btnRefreshNSPSExemptions.Image"), System.Drawing.Image)
        Me.btnRefreshNSPSExemptions.Location = New System.Drawing.Point(123, 268)
        Me.btnRefreshNSPSExemptions.Name = "btnRefreshNSPSExemptions"
        Me.btnRefreshNSPSExemptions.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshNSPSExemptions.TabIndex = 490
        Me.btnRefreshNSPSExemptions.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 273)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 13)
        Me.Label4.TabIndex = 404
        Me.Label4.Text = "All Existing Exemptions"
        '
        'dgvExistingExemptions
        '
        Me.dgvExistingExemptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExistingExemptions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvExistingExemptions.Location = New System.Drawing.Point(0, 320)
        Me.dgvExistingExemptions.Name = "dgvExistingExemptions"
        Me.dgvExistingExemptions.Size = New System.Drawing.Size(977, 312)
        Me.dgvExistingExemptions.TabIndex = 403
        '
        'btnUpdateNSPSExemption
        '
        Me.btnUpdateNSPSExemption.AutoSize = True
        Me.btnUpdateNSPSExemption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateNSPSExemption.Location = New System.Drawing.Point(189, 116)
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
        Me.btnViewDeletedNSPS.Location = New System.Drawing.Point(208, 179)
        Me.btnViewDeletedNSPS.Name = "btnViewDeletedNSPS"
        Me.btnViewDeletedNSPS.Size = New System.Drawing.Size(126, 23)
        Me.btnViewDeletedNSPS.TabIndex = 401
        Me.btnViewDeletedNSPS.Text = "View All Deleted NSPS"
        Me.btnViewDeletedNSPS.UseVisualStyleBackColor = True
        '
        'txtNSPSExemption
        '
        Me.txtNSPSExemption.Location = New System.Drawing.Point(26, 30)
        Me.txtNSPSExemption.Multiline = True
        Me.txtNSPSExemption.Name = "txtNSPSExemption"
        Me.txtNSPSExemption.Size = New System.Drawing.Size(598, 80)
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
        Me.btnAddNSPSExemption.Location = New System.Drawing.Point(26, 116)
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
        Me.btnDeleteNSPSExemption.Location = New System.Drawing.Point(102, 179)
        Me.btnDeleteNSPSExemption.Name = "btnDeleteNSPSExemption"
        Me.btnDeleteNSPSExemption.Size = New System.Drawing.Size(80, 23)
        Me.btnDeleteNSPSExemption.TabIndex = 400
        Me.btnDeleteNSPSExemption.Text = "Delete NSPS"
        Me.btnDeleteNSPSExemption.UseVisualStyleBackColor = True
        '
        'txtDeleteNSPSExemptions
        '
        Me.txtDeleteNSPSExemptions.Location = New System.Drawing.Point(31, 181)
        Me.txtDeleteNSPSExemptions.Name = "txtDeleteNSPSExemptions"
        Me.txtDeleteNSPSExemptions.ReadOnly = True
        Me.txtDeleteNSPSExemptions.Size = New System.Drawing.Size(65, 20)
        Me.txtDeleteNSPSExemptions.TabIndex = 5
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Location = New System.Drawing.Point(14, 165)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(96, 13)
        Me.Label107.TabIndex = 6
        Me.Label107.Text = "NSPS ID to Delete"
        '
        'TPFeeManagementTools
        '
        Me.TPFeeManagementTools.Controls.Add(Me.btnOpenFeesLog)
        Me.TPFeeManagementTools.Controls.Add(Me.lblCheckState)
        Me.TPFeeManagementTools.Controls.Add(Me.Label8)
        Me.TPFeeManagementTools.Controls.Add(Me.txtCheckFacility)
        Me.TPFeeManagementTools.Controls.Add(Me.llbCheckFacility)
        Me.TPFeeManagementTools.Controls.Add(Me.btnSaveAddition)
        Me.TPFeeManagementTools.Controls.Add(Me.Label6)
        Me.TPFeeManagementTools.Controls.Add(Me.mtbCheckAIRSNumber)
        Me.TPFeeManagementTools.Controls.Add(Me.dgvFeeManagmentLists)
        Me.TPFeeManagementTools.Controls.Add(Me.Panel10)
        Me.TPFeeManagementTools.Controls.Add(Me.btnExportToExcel)
        Me.TPFeeManagementTools.Controls.Add(Me.txtCount)
        Me.TPFeeManagementTools.Controls.Add(Me.Label13)
        Me.TPFeeManagementTools.Controls.Add(Me.Label10)
        Me.TPFeeManagementTools.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeManagementTools.Name = "TPFeeManagementTools"
        Me.TPFeeManagementTools.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFeeManagementTools.Size = New System.Drawing.Size(993, 666)
        Me.TPFeeManagementTools.TabIndex = 2
        Me.TPFeeManagementTools.Text = "Fee Management Tools"
        Me.TPFeeManagementTools.UseVisualStyleBackColor = True
        '
        'btnOpenFeesLog
        '
        Me.btnOpenFeesLog.AutoSize = True
        Me.btnOpenFeesLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenFeesLog.Location = New System.Drawing.Point(844, 226)
        Me.btnOpenFeesLog.Name = "btnOpenFeesLog"
        Me.btnOpenFeesLog.Size = New System.Drawing.Size(90, 23)
        Me.btnOpenFeesLog.TabIndex = 473
        Me.btnOpenFeesLog.Text = "Open Fees Log"
        Me.btnOpenFeesLog.UseVisualStyleBackColor = True
        '
        'lblCheckState
        '
        Me.lblCheckState.AutoSize = True
        Me.lblCheckState.Location = New System.Drawing.Point(765, 349)
        Me.lblCheckState.Name = "lblCheckState"
        Me.lblCheckState.Size = New System.Drawing.Size(0, 13)
        Me.lblCheckState.TabIndex = 472
        Me.lblCheckState.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(618, 271)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 13)
        Me.Label8.TabIndex = 471
        Me.Label8.Text = "Facility Name"
        '
        'txtCheckFacility
        '
        Me.txtCheckFacility.Location = New System.Drawing.Point(693, 268)
        Me.txtCheckFacility.Name = "txtCheckFacility"
        Me.txtCheckFacility.ReadOnly = True
        Me.txtCheckFacility.Size = New System.Drawing.Size(274, 20)
        Me.txtCheckFacility.TabIndex = 470
        '
        'llbCheckFacility
        '
        Me.llbCheckFacility.AutoSize = True
        Me.llbCheckFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbCheckFacility.Location = New System.Drawing.Point(765, 231)
        Me.llbCheckFacility.Name = "llbCheckFacility"
        Me.llbCheckFacility.Size = New System.Drawing.Size(73, 13)
        Me.llbCheckFacility.TabIndex = 469
        Me.llbCheckFacility.TabStop = True
        Me.llbCheckFacility.Text = "Check Facility"
        '
        'btnSaveAddition
        '
        Me.btnSaveAddition.AutoSize = True
        Me.btnSaveAddition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveAddition.Enabled = False
        Me.btnSaveAddition.Location = New System.Drawing.Point(612, 294)
        Me.btnSaveAddition.Name = "btnSaveAddition"
        Me.btnSaveAddition.Size = New System.Drawing.Size(116, 23)
        Me.btnSaveAddition.TabIndex = 468
        Me.btnSaveAddition.Text = "Add to Mailout/Enroll"
        Me.btnSaveAddition.UseVisualStyleBackColor = True
        Me.btnSaveAddition.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(615, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 466
        Me.Label6.Text = "AIRS Number"
        '
        'mtbCheckAIRSNumber
        '
        Me.mtbCheckAIRSNumber.Location = New System.Drawing.Point(693, 228)
        Me.mtbCheckAIRSNumber.Mask = "000-00000"
        Me.mtbCheckAIRSNumber.Name = "mtbCheckAIRSNumber"
        Me.mtbCheckAIRSNumber.Size = New System.Drawing.Size(66, 20)
        Me.mtbCheckAIRSNumber.TabIndex = 467
        Me.mtbCheckAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'dgvFeeManagmentLists
        '
        Me.dgvFeeManagmentLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFeeManagmentLists.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgvFeeManagmentLists.Location = New System.Drawing.Point(3, 107)
        Me.dgvFeeManagmentLists.Name = "dgvFeeManagmentLists"
        Me.dgvFeeManagmentLists.Size = New System.Drawing.Size(566, 556)
        Me.dgvFeeManagmentLists.TabIndex = 1
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.btnViewEnrolledFacilities)
        Me.Panel10.Controls.Add(Me.btnViewMailout)
        Me.Panel10.Controls.Add(Me.btnSetMailoutDate)
        Me.Panel10.Controls.Add(Me.btnUpdateContactData)
        Me.Panel10.Controls.Add(Me.dtpDateMailoutSent)
        Me.Panel10.Controls.Add(Me.btnViewFacilitiesSubjectToFees)
        Me.Panel10.Controls.Add(Me.btnGenerateMailoutList)
        Me.Panel10.Controls.Add(Me.btnUnenrollFeeYear)
        Me.Panel10.Controls.Add(Me.cboAvailableFeeYears)
        Me.Panel10.Controls.Add(Me.btnFirstEnrollment)
        Me.Panel10.Controls.Add(Me.Label18)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(3, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(987, 104)
        Me.Panel10.TabIndex = 0
        '
        'btnViewEnrolledFacilities
        '
        Me.btnViewEnrolledFacilities.AutoSize = True
        Me.btnViewEnrolledFacilities.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewEnrolledFacilities.Location = New System.Drawing.Point(189, 12)
        Me.btnViewEnrolledFacilities.Name = "btnViewEnrolledFacilities"
        Me.btnViewEnrolledFacilities.Size = New System.Drawing.Size(81, 23)
        Me.btnViewEnrolledFacilities.TabIndex = 471
        Me.btnViewEnrolledFacilities.Text = "View Enrolled"
        Me.btnViewEnrolledFacilities.UseVisualStyleBackColor = True
        '
        'btnViewMailout
        '
        Me.btnViewMailout.AutoSize = True
        Me.btnViewMailout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewMailout.Location = New System.Drawing.Point(377, 12)
        Me.btnViewMailout.Name = "btnViewMailout"
        Me.btnViewMailout.Size = New System.Drawing.Size(77, 23)
        Me.btnViewMailout.TabIndex = 470
        Me.btnViewMailout.Text = "View Mailout"
        Me.btnViewMailout.UseVisualStyleBackColor = True
        '
        'btnSetMailoutDate
        '
        Me.btnSetMailoutDate.AutoSize = True
        Me.btnSetMailoutDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSetMailoutDate.Location = New System.Drawing.Point(582, 14)
        Me.btnSetMailoutDate.Name = "btnSetMailoutDate"
        Me.btnSetMailoutDate.Size = New System.Drawing.Size(105, 23)
        Me.btnSetMailoutDate.TabIndex = 469
        Me.btnSetMailoutDate.Text = "Save Mailout Date"
        Me.btnSetMailoutDate.UseVisualStyleBackColor = True
        '
        'btnUpdateContactData
        '
        Me.btnUpdateContactData.AutoSize = True
        Me.btnUpdateContactData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateContactData.Location = New System.Drawing.Point(377, 70)
        Me.btnUpdateContactData.Name = "btnUpdateContactData"
        Me.btnUpdateContactData.Size = New System.Drawing.Size(137, 23)
        Me.btnUpdateContactData.TabIndex = 468
        Me.btnUpdateContactData.Text = "Get Current Fee Contacts"
        Me.btnUpdateContactData.UseVisualStyleBackColor = True
        '
        'dtpDateMailoutSent
        '
        Me.dtpDateMailoutSent.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDateMailoutSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateMailoutSent.Location = New System.Drawing.Point(690, 15)
        Me.dtpDateMailoutSent.Name = "dtpDateMailoutSent"
        Me.dtpDateMailoutSent.Size = New System.Drawing.Size(100, 20)
        Me.dtpDateMailoutSent.TabIndex = 467
        '
        'btnViewFacilitiesSubjectToFees
        '
        Me.btnViewFacilitiesSubjectToFees.AutoSize = True
        Me.btnViewFacilitiesSubjectToFees.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewFacilitiesSubjectToFees.Location = New System.Drawing.Point(113, 22)
        Me.btnViewFacilitiesSubjectToFees.Name = "btnViewFacilitiesSubjectToFees"
        Me.btnViewFacilitiesSubjectToFees.Size = New System.Drawing.Size(60, 36)
        Me.btnViewFacilitiesSubjectToFees.TabIndex = 466
        Me.btnViewFacilitiesSubjectToFees.Text = "View " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Fee Year"
        Me.btnViewFacilitiesSubjectToFees.UseVisualStyleBackColor = True
        '
        'btnGenerateMailoutList
        '
        Me.btnGenerateMailoutList.AutoSize = True
        Me.btnGenerateMailoutList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGenerateMailoutList.Location = New System.Drawing.Point(377, 41)
        Me.btnGenerateMailoutList.Name = "btnGenerateMailoutList"
        Me.btnGenerateMailoutList.Size = New System.Drawing.Size(117, 23)
        Me.btnGenerateMailoutList.TabIndex = 10
        Me.btnGenerateMailoutList.Text = "Generate Mailout List"
        Me.btnGenerateMailoutList.UseVisualStyleBackColor = True
        '
        'btnUnenrollFeeYear
        '
        Me.btnUnenrollFeeYear.AutoSize = True
        Me.btnUnenrollFeeYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnenrollFeeYear.Location = New System.Drawing.Point(189, 70)
        Me.btnUnenrollFeeYear.Name = "btnUnenrollFeeYear"
        Me.btnUnenrollFeeYear.Size = New System.Drawing.Size(129, 23)
        Me.btnUnenrollFeeYear.TabIndex = 9
        Me.btnUnenrollFeeYear.Text = "Un-enroll entire fee year"
        Me.btnUnenrollFeeYear.UseVisualStyleBackColor = True
        '
        'cboAvailableFeeYears
        '
        Me.cboAvailableFeeYears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAvailableFeeYears.FormattingEnabled = True
        Me.cboAvailableFeeYears.Location = New System.Drawing.Point(18, 22)
        Me.cboAvailableFeeYears.Name = "cboAvailableFeeYears"
        Me.cboAvailableFeeYears.Size = New System.Drawing.Size(89, 21)
        Me.cboAvailableFeeYears.TabIndex = 7
        '
        'btnFirstEnrollment
        '
        Me.btnFirstEnrollment.AutoSize = True
        Me.btnFirstEnrollment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFirstEnrollment.Location = New System.Drawing.Point(189, 41)
        Me.btnFirstEnrollment.Name = "btnFirstEnrollment"
        Me.btnFirstEnrollment.Size = New System.Drawing.Size(100, 23)
        Me.btnFirstEnrollment.TabIndex = 6
        Me.btnFirstEnrollment.Text = "Enroll All Facilities"
        Me.btnFirstEnrollment.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(4, 6)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Mailout Year"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.AutoSize = True
        Me.btnExportToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportToExcel.Location = New System.Drawing.Point(672, 113)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(88, 23)
        Me.btnExportToExcel.TabIndex = 465
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(612, 114)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(54, 20)
        Me.txtCount.TabIndex = 463
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(576, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 13)
        Me.Label13.TabIndex = 464
        Me.Label13.Text = "Count"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(595, 197)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(201, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Add Facility to Current Mailout/Enrollment"
        '
        'TPWebTools
        '
        Me.TPWebTools.Controls.Add(Me.TabControl3)
        Me.TPWebTools.Location = New System.Drawing.Point(4, 22)
        Me.TPWebTools.Name = "TPWebTools"
        Me.TPWebTools.Size = New System.Drawing.Size(993, 666)
        Me.TPWebTools.TabIndex = 5
        Me.TPWebTools.Text = "GECO Tools"
        Me.TPWebTools.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TPWebUsers)
        Me.TabControl3.Controls.Add(Me.TPWebUsers1)
        Me.TabControl3.Controls.Add(Me.TPActivate)
        Me.TabControl3.Controls.Add(Me.TPFeeFacility)
        Me.TabControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl3.Location = New System.Drawing.Point(0, 0)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(993, 666)
        Me.TabControl3.TabIndex = 151
        '
        'TPWebUsers
        '
        Me.TPWebUsers.Controls.Add(Me.pnlUser)
        Me.TPWebUsers.Controls.Add(Me.PanelFacility)
        Me.TPWebUsers.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers.Name = "TPWebUsers"
        Me.TPWebUsers.Size = New System.Drawing.Size(985, 640)
        Me.TPWebUsers.TabIndex = 1
        Me.TPWebUsers.Text = "Web App Users - Facility"
        Me.TPWebUsers.UseVisualStyleBackColor = True
        '
        'pnlUser
        '
        Me.pnlUser.Controls.Add(Me.dgvUsers)
        Me.pnlUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUser.Location = New System.Drawing.Point(0, 144)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(985, 496)
        Me.pnlUser.TabIndex = 147
        '
        'dgvUsers
        '
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUsers.Location = New System.Drawing.Point(0, 0)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.Size = New System.Drawing.Size(985, 496)
        Me.dgvUsers.TabIndex = 274
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.lblFaciltyName)
        Me.PanelFacility.Controls.Add(Me.lblFacility)
        Me.PanelFacility.Controls.Add(Me.cboUsers)
        Me.PanelFacility.Controls.Add(Me.Label177)
        Me.PanelFacility.Controls.Add(Me.Label28)
        Me.PanelFacility.Controls.Add(Me.mtbAIRSNumber)
        Me.PanelFacility.Controls.Add(Me.btnDelete)
        Me.PanelFacility.Controls.Add(Me.llbViewUserData)
        Me.PanelFacility.Controls.Add(Me.btnUpdate)
        Me.PanelFacility.Controls.Add(Me.btnAddUser)
        Me.PanelFacility.Controls.Add(Me.Label29)
        Me.PanelFacility.Controls.Add(Me.txtEmail)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(985, 144)
        Me.PanelFacility.TabIndex = 146
        '
        'lblFaciltyName
        '
        Me.lblFaciltyName.AutoSize = True
        Me.lblFaciltyName.Location = New System.Drawing.Point(97, 126)
        Me.lblFaciltyName.Name = "lblFaciltyName"
        Me.lblFaciltyName.Size = New System.Drawing.Size(0, 13)
        Me.lblFaciltyName.TabIndex = 290
        '
        'lblFacility
        '
        Me.lblFacility.AutoSize = True
        Me.lblFacility.Location = New System.Drawing.Point(4, 126)
        Me.lblFacility.Name = "lblFacility"
        Me.lblFacility.Size = New System.Drawing.Size(89, 13)
        Me.lblFacility.TabIndex = 289
        Me.lblFacility.Text = "Current Users for:"
        '
        'cboUsers
        '
        Me.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(366, 63)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(259, 21)
        Me.cboUsers.TabIndex = 281
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
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(346, 48)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(150, 13)
        Me.Label28.TabIndex = 280
        Me.Label28.Text = "Delete a User for this Facility:  "
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
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(642, 64)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 20)
        Me.btnDelete.TabIndex = 278
        Me.btnDelete.Text = "Delete User"
        Me.btnDelete.UseVisualStyleBackColor = True
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
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(7, 99)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdate.TabIndex = 277
        Me.btnUpdate.Text = "Save Changes"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(252, 63)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(62, 20)
        Me.btnAddUser.TabIndex = 274
        Me.btnAddUser.Text = "Add User"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(4, 48)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(132, 13)
        Me.Label29.TabIndex = 276
        Me.Label29.Text = "Add a User to this Facility: "
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(17, 64)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(229, 20)
        Me.txtEmail.TabIndex = 275
        '
        'TPWebUsers1
        '
        Me.TPWebUsers1.Controls.Add(Me.pnlUserFacility)
        Me.TPWebUsers1.Controls.Add(Me.pnlUserEmail)
        Me.TPWebUsers1.Location = New System.Drawing.Point(4, 22)
        Me.TPWebUsers1.Name = "TPWebUsers1"
        Me.TPWebUsers1.Size = New System.Drawing.Size(985, 640)
        Me.TPWebUsers1.TabIndex = 2
        Me.TPWebUsers1.Text = "Web App Users - Email"
        Me.TPWebUsers1.UseVisualStyleBackColor = True
        '
        'pnlUserFacility
        '
        Me.pnlUserFacility.Controls.Add(Me.dgvUserFacilities)
        Me.pnlUserFacility.Controls.Add(Me.pnlUserInfo)
        Me.pnlUserFacility.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUserFacility.Location = New System.Drawing.Point(0, 49)
        Me.pnlUserFacility.Name = "pnlUserFacility"
        Me.pnlUserFacility.Size = New System.Drawing.Size(985, 591)
        Me.pnlUserFacility.TabIndex = 148
        '
        'dgvUserFacilities
        '
        Me.dgvUserFacilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUserFacilities.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUserFacilities.Location = New System.Drawing.Point(0, 366)
        Me.dgvUserFacilities.Name = "dgvUserFacilities"
        Me.dgvUserFacilities.Size = New System.Drawing.Size(985, 225)
        Me.dgvUserFacilities.TabIndex = 151
        '
        'pnlUserInfo
        '
        Me.pnlUserInfo.Controls.Add(Me.btnChangeEmailAddress)
        Me.pnlUserInfo.Controls.Add(Me.mtbFacilityToAdd)
        Me.pnlUserInfo.Controls.Add(Me.txtEditEmail)
        Me.pnlUserInfo.Controls.Add(Me.cboFacilityToDelete)
        Me.pnlUserInfo.Controls.Add(Me.lblConfirmDate)
        Me.pnlUserInfo.Controls.Add(Me.Label75)
        Me.pnlUserInfo.Controls.Add(Me.lblLastLogIn)
        Me.pnlUserInfo.Controls.Add(Me.btnDeleteFacilityUser)
        Me.pnlUserInfo.Controls.Add(Me.txtEditUserPassword)
        Me.pnlUserInfo.Controls.Add(Me.btnUpdateUser)
        Me.pnlUserInfo.Controls.Add(Me.Label53)
        Me.pnlUserInfo.Controls.Add(Me.btnUpdatePassword)
        Me.pnlUserInfo.Controls.Add(Me.btnAddFacilitytoUser)
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
        Me.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserInfo.Name = "pnlUserInfo"
        Me.pnlUserInfo.Size = New System.Drawing.Size(985, 366)
        Me.pnlUserInfo.TabIndex = 150
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
        'mtbFacilityToAdd
        '
        Me.mtbFacilityToAdd.Location = New System.Drawing.Point(179, 303)
        Me.mtbFacilityToAdd.Mask = "000-00000"
        Me.mtbFacilityToAdd.Name = "mtbFacilityToAdd"
        Me.mtbFacilityToAdd.Size = New System.Drawing.Size(64, 20)
        Me.mtbFacilityToAdd.TabIndex = 286
        Me.mtbFacilityToAdd.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtEditEmail
        '
        Me.txtEditEmail.Location = New System.Drawing.Point(513, 81)
        Me.txtEditEmail.Name = "txtEditEmail"
        Me.txtEditEmail.Size = New System.Drawing.Size(208, 20)
        Me.txtEditEmail.TabIndex = 43
        Me.txtEditEmail.Visible = False
        '
        'cboFacilityToDelete
        '
        Me.cboFacilityToDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFacilityToDelete.FormattingEnabled = True
        Me.cboFacilityToDelete.Location = New System.Drawing.Point(179, 329)
        Me.cboFacilityToDelete.Name = "cboFacilityToDelete"
        Me.cboFacilityToDelete.Size = New System.Drawing.Size(252, 21)
        Me.cboFacilityToDelete.TabIndex = 284
        '
        'lblConfirmDate
        '
        Me.lblConfirmDate.AutoSize = True
        Me.lblConfirmDate.Location = New System.Drawing.Point(9, 258)
        Me.lblConfirmDate.Name = "lblConfirmDate"
        Me.lblConfirmDate.Size = New System.Drawing.Size(0, 13)
        Me.lblConfirmDate.TabIndex = 42
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(9, 333)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(150, 13)
        Me.Label75.TabIndex = 283
        Me.Label75.Text = "Delete a Facility for this User:  "
        '
        'lblLastLogIn
        '
        Me.lblLastLogIn.AutoSize = True
        Me.lblLastLogIn.Location = New System.Drawing.Point(9, 280)
        Me.lblLastLogIn.Name = "lblLastLogIn"
        Me.lblLastLogIn.Size = New System.Drawing.Size(0, 13)
        Me.lblLastLogIn.TabIndex = 41
        '
        'btnDeleteFacilityUser
        '
        Me.btnDeleteFacilityUser.AutoSize = True
        Me.btnDeleteFacilityUser.Location = New System.Drawing.Point(437, 328)
        Me.btnDeleteFacilityUser.Name = "btnDeleteFacilityUser"
        Me.btnDeleteFacilityUser.Size = New System.Drawing.Size(151, 23)
        Me.btnDeleteFacilityUser.TabIndex = 282
        Me.btnDeleteFacilityUser.Text = "Remove Facility for this User"
        Me.btnDeleteFacilityUser.UseVisualStyleBackColor = True
        '
        'txtEditUserPassword
        '
        Me.txtEditUserPassword.Location = New System.Drawing.Point(513, 18)
        Me.txtEditUserPassword.Name = "txtEditUserPassword"
        Me.txtEditUserPassword.Size = New System.Drawing.Size(198, 20)
        Me.txtEditUserPassword.TabIndex = 40
        Me.txtEditUserPassword.Visible = False
        '
        'btnUpdateUser
        '
        Me.btnUpdateUser.Location = New System.Drawing.Point(627, 329)
        Me.btnUpdateUser.Name = "btnUpdateUser"
        Me.btnUpdateUser.Size = New System.Drawing.Size(106, 24)
        Me.btnUpdateUser.TabIndex = 277
        Me.btnUpdateUser.Text = "Save Changes"
        Me.btnUpdateUser.UseVisualStyleBackColor = True
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(9, 307)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(164, 13)
        Me.Label53.TabIndex = 276
        Me.Label53.Text = "Add a Facility to the above User: "
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
        'btnAddFacilitytoUser
        '
        Me.btnAddFacilitytoUser.AutoSize = True
        Me.btnAddFacilitytoUser.Location = New System.Drawing.Point(249, 302)
        Me.btnAddFacilitytoUser.Name = "btnAddFacilitytoUser"
        Me.btnAddFacilitytoUser.Size = New System.Drawing.Size(77, 23)
        Me.btnAddFacilitytoUser.TabIndex = 274
        Me.btnAddFacilitytoUser.Text = "Add Facility"
        Me.btnAddFacilitytoUser.UseVisualStyleBackColor = True
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
        Me.btnEditUserData.Location = New System.Drawing.Point(14, 225)
        Me.btnEditUserData.Name = "btnEditUserData"
        Me.btnEditUserData.Size = New System.Drawing.Size(86, 23)
        Me.btnEditUserData.TabIndex = 26
        Me.btnEditUserData.Text = "Edit User Data"
        Me.btnEditUserData.UseVisualStyleBackColor = True
        '
        'lblCityStateZip
        '
        Me.lblCityStateZip.AutoSize = True
        Me.lblCityStateZip.Location = New System.Drawing.Point(11, 193)
        Me.lblCityStateZip.Name = "lblCityStateZip"
        Me.lblCityStateZip.Size = New System.Drawing.Size(0, 13)
        Me.lblCityStateZip.TabIndex = 9
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(11, 172)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(0, 13)
        Me.lblAddress.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(11, 151)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 13)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "Address:"
        '
        'lblFaxNo
        '
        Me.lblFaxNo.AutoSize = True
        Me.lblFaxNo.Location = New System.Drawing.Point(11, 130)
        Me.lblFaxNo.Name = "lblFaxNo"
        Me.lblFaxNo.Size = New System.Drawing.Size(0, 13)
        Me.lblFaxNo.TabIndex = 6
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.Location = New System.Drawing.Point(11, 109)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(0, 13)
        Me.lblPhoneNo.TabIndex = 5
        '
        'lblCoName
        '
        Me.lblCoName.AutoSize = True
        Me.lblCoName.Location = New System.Drawing.Point(11, 88)
        Me.lblCoName.Name = "lblCoName"
        Me.lblCoName.Size = New System.Drawing.Size(0, 13)
        Me.lblCoName.TabIndex = 4
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(11, 67)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(0, 13)
        Me.lblTitle.TabIndex = 3
        '
        'lblLName
        '
        Me.lblLName.AutoSize = True
        Me.lblLName.Location = New System.Drawing.Point(11, 46)
        Me.lblLName.Name = "lblLName"
        Me.lblLName.Size = New System.Drawing.Size(0, 13)
        Me.lblLName.TabIndex = 2
        '
        'lblFName
        '
        Me.lblFName.AutoSize = True
        Me.lblFName.Location = New System.Drawing.Point(11, 25)
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
        'pnlUserEmail
        '
        Me.pnlUserEmail.Controls.Add(Me.lblViewEmailData)
        Me.pnlUserEmail.Controls.Add(Me.Label39)
        Me.pnlUserEmail.Controls.Add(Me.txtWebUserEmail)
        Me.pnlUserEmail.Controls.Add(Me.cboUserEmail)
        Me.pnlUserEmail.Controls.Add(Me.lblViewFacility)
        Me.pnlUserEmail.Controls.Add(Me.Label52)
        Me.pnlUserEmail.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserEmail.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserEmail.Name = "pnlUserEmail"
        Me.pnlUserEmail.Size = New System.Drawing.Size(985, 49)
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
        Me.cboUserEmail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUserEmail.Location = New System.Drawing.Point(618, 9)
        Me.cboUserEmail.Name = "cboUserEmail"
        Me.cboUserEmail.Size = New System.Drawing.Size(244, 21)
        Me.cboUserEmail.TabIndex = 1
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
        Me.TPActivate.Size = New System.Drawing.Size(985, 640)
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
        Me.TPFeeFacility.Size = New System.Drawing.Size(985, 640)
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
        'PASPFeeManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1001, 692)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "PASPFeeManagement"
        Me.Text = "PASP Fee Management"
        Me.TabControl1.ResumeLayout(False)
        Me.TPFeeAdminTools.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
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
        Me.TPFeeManagementTools.PerformLayout()
        CType(Me.dgvFeeManagmentLists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.TPWebTools.ResumeLayout(False)
        Me.TabControl3.ResumeLayout(False)
        Me.TPWebUsers.ResumeLayout(False)
        Me.pnlUser.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.TPWebUsers1.ResumeLayout(False)
        Me.pnlUserFacility.ResumeLayout(False)
        CType(Me.dgvUserFacilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUserInfo.ResumeLayout(False)
        Me.pnlUserInfo.PerformLayout()
        Me.pnlUserEmail.ResumeLayout(False)
        Me.pnlUserEmail.PerformLayout()
        Me.TPActivate.ResumeLayout(False)
        Me.TPActivate.PerformLayout()
        Me.TPFeeFacility.ResumeLayout(False)
        Me.TPFeeFacility.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPFeeAdminTools As System.Windows.Forms.TabPage
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnReloadFeeRate As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents dtpFeeDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnViewDeletedFeeRates As System.Windows.Forms.Button
    Friend WithEvents btnDeleteFeeRate As System.Windows.Forms.Button
    Friend WithEvents btnClearFeeData As System.Windows.Forms.Button
    Friend WithEvents btnUpdateFeeData As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtFeeNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtFeeYear As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtFeeID As System.Windows.Forms.TextBox
    Friend WithEvents Label248 As System.Windows.Forms.Label
    Friend WithEvents dtpFeePeriodStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAdminFeePercent As System.Windows.Forms.TextBox
    Friend WithEvents dtpFeePeriodEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtperTonRate As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTitleVfee As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents btnsaveRate As System.Windows.Forms.Button
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents dtpAdminApplicable As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAnnualNSPSFee As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtAnnualSMFee As System.Windows.Forms.TextBox
    Friend WithEvents dgvFeeRates As System.Windows.Forms.DataGridView
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
    Friend WithEvents btnUnselectAllForms As System.Windows.Forms.Button
    Friend WithEvents btnUpdateNSPSbyYear As System.Windows.Forms.Button
    Friend WithEvents btnViewNSPSExemptionsByYear As System.Windows.Forms.Button
    Friend WithEvents dgvNSPSExemptionsByYear As System.Windows.Forms.DataGridView
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents cboNSPSExemptionYear As System.Windows.Forms.ComboBox
    Friend WithEvents TPFeeManagementTools As System.Windows.Forms.TabPage
    Friend WithEvents dgvFeeManagmentLists As System.Windows.Forms.DataGridView
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateMailoutList As System.Windows.Forms.Button
    Friend WithEvents btnUnenrollFeeYear As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboAvailableFeeYears As System.Windows.Forms.ComboBox
    Friend WithEvents btnFirstEnrollment As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgvExistingExemptions As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnRefreshNSPSExemptions As System.Windows.Forms.Button
    Friend WithEvents btnClearNSPSExemptions As System.Windows.Forms.Button
    Friend WithEvents btnViewFacilitiesSubjectToFees As System.Windows.Forms.Button
    Friend WithEvents btnUpdateContactData As System.Windows.Forms.Button
    Friend WithEvents dtpDateMailoutSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSetMailoutDate As System.Windows.Forms.Button
    Friend WithEvents btnViewEnrolledFacilities As System.Windows.Forms.Button
    Friend WithEvents btnViewMailout As System.Windows.Forms.Button
    Friend WithEvents TPWebTools As System.Windows.Forms.TabPage
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TPWebUsers As System.Windows.Forms.TabPage
    Friend WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents cboUsers As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents llbViewUserData As System.Windows.Forms.LinkLabel
    Friend WithEvents TPWebUsers1 As System.Windows.Forms.TabPage
    Friend WithEvents pnlUserFacility As System.Windows.Forms.Panel
    Friend WithEvents pnlUserInfo As System.Windows.Forms.Panel
    Friend WithEvents btnChangeEmailAddress As System.Windows.Forms.Button
    Friend WithEvents txtEditEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmDate As System.Windows.Forms.Label
    Friend WithEvents lblLastLogIn As System.Windows.Forms.Label
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
    Friend WithEvents mtbFacilityToAdd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cboFacilityToDelete As System.Windows.Forms.ComboBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteFacilityUser As System.Windows.Forms.Button
    Friend WithEvents btnUpdateUser As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents btnAddFacilitytoUser As System.Windows.Forms.Button
    Friend WithEvents pnlUserEmail As System.Windows.Forms.Panel
    Friend WithEvents lblViewEmailData As System.Windows.Forms.LinkLabel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtWebUserEmail As System.Windows.Forms.TextBox
    Friend WithEvents cboUserEmail As System.Windows.Forms.ComboBox
    Friend WithEvents lblViewFacility As System.Windows.Forms.LinkLabel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents TPActivate As System.Windows.Forms.TabPage
    Friend WithEvents btnActivateUser As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents TPFeeFacility As System.Windows.Forms.TabPage
    Friend WithEvents mtbyear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbFeeAirsNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnRemoveFacility As System.Windows.Forms.Button
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents btnAddFacility As System.Windows.Forms.Button
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents lblFacility As System.Windows.Forms.Label
    Friend WithEvents lblFaciltyName As System.Windows.Forms.Label
    Friend WithEvents dgvUserFacilities As System.Windows.Forms.DataGridView
    Friend WithEvents llbCheckFacility As System.Windows.Forms.LinkLabel
    Friend WithEvents btnSaveAddition As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents mtbCheckAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCheckFacility As System.Windows.Forms.TextBox
    Friend WithEvents lblCheckState As System.Windows.Forms.Label
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
End Class
