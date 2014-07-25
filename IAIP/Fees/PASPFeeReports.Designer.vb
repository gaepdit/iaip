<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPFeeReports
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
        Me.tabReport = New System.Windows.Forms.TabControl
        Me.TPFacilitySpecific = New System.Windows.Forms.TabPage
        Me.cboFacilityName = New System.Windows.Forms.ComboBox
        Me.cboAirsNo = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.llbViewAll = New System.Windows.Forms.LinkLabel
        Me.Label = New System.Windows.Forms.Label
        Me.TPFinancial = New System.Windows.Forms.TabPage
        Me.pnlDateRange = New System.Windows.Forms.Panel
        Me.btnRunVarianceReport = New System.Windows.Forms.Button
        Me.rdb2006Variance = New System.Windows.Forms.RadioButton
        Me.rdb2005Variance = New System.Windows.Forms.RadioButton
        Me.btndeposit = New System.Windows.Forms.Button
        Me.btnDateReport = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnvariance = New System.Windows.Forms.Button
        Me.btnFeeByYear = New System.Windows.Forms.Button
        Me.btnBankrupt = New System.Windows.Forms.Button
        Me.btnPayDate = New System.Windows.Forms.Button
        Me.btnPayment = New System.Windows.Forms.Button
        Me.TPYearSpecific = New System.Windows.Forms.TabPage
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.mtbFacilityBalanceYear = New System.Windows.Forms.MaskedTextBox
        Me.chbFacilityBalance = New System.Windows.Forms.CheckBox
        Me.lblFacilityBalanceReportTag = New System.Windows.Forms.Label
        Me.btnRunBalanceReport = New System.Windows.Forms.Button
        Me.btnFeeBalanceZero = New System.Windows.Forms.Button
        Me.btnFeeBalance = New System.Windows.Forms.Button
        Me.btnClassification = New System.Windows.Forms.Button
        Me.btnFeesandEmissions = New System.Windows.Forms.Button
        Me.TPDeposits = New System.Windows.Forms.TabPage
        Me.cboDepositNo = New System.Windows.Forms.ComboBox
        Me.cboAirs = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDepositData = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.TPCompliance = New System.Windows.Forms.TabPage
        Me.pnlNSPS = New System.Windows.Forms.Panel
        Me.btnRunNonRespondent = New System.Windows.Forms.Button
        Me.lblNonRespondant = New System.Windows.Forms.Label
        Me.mtbNonRespondentYear = New System.Windows.Forms.MaskedTextBox
        Me.lblNSPS3 = New System.Windows.Forms.LinkLabel
        Me.lblNSPS2 = New System.Windows.Forms.LinkLabel
        Me.lblNSPS1 = New System.Windows.Forms.LinkLabel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnNSPSChange = New System.Windows.Forms.Button
        Me.btnNoOperate = New System.Windows.Forms.Button
        Me.btnNoResponse = New System.Windows.Forms.Button
        Me.btnClassChange = New System.Windows.Forms.Button
        Me.TPGeneral = New System.Windows.Forms.TabPage
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.btnTrainingReg = New System.Windows.Forms.Button
        Me.btnFacInfoChange = New System.Windows.Forms.Button
        Me.btnComments = New System.Windows.Forms.Button
        Me.CRFeesReports = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.tabReport.SuspendLayout()
        Me.TPFacilitySpecific.SuspendLayout()
        Me.TPFinancial.SuspendLayout()
        Me.pnlDateRange.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TPYearSpecific.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TPDeposits.SuspendLayout()
        Me.TPCompliance.SuspendLayout()
        Me.pnlNSPS.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.TPGeneral.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabReport
        '
        Me.tabReport.Controls.Add(Me.TPFacilitySpecific)
        Me.tabReport.Controls.Add(Me.TPFinancial)
        Me.tabReport.Controls.Add(Me.TPYearSpecific)
        Me.tabReport.Controls.Add(Me.TPDeposits)
        Me.tabReport.Controls.Add(Me.TPCompliance)
        Me.tabReport.Controls.Add(Me.TPGeneral)
        Me.tabReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.tabReport.Location = New System.Drawing.Point(0, 0)
        Me.tabReport.Margin = New System.Windows.Forms.Padding(2)
        Me.tabReport.Name = "tabReport"
        Me.tabReport.SelectedIndex = 0
        Me.tabReport.Size = New System.Drawing.Size(669, 114)
        Me.tabReport.TabIndex = 268
        '
        'TPFacilitySpecific
        '
        Me.TPFacilitySpecific.Controls.Add(Me.cboFacilityName)
        Me.TPFacilitySpecific.Controls.Add(Me.cboAirsNo)
        Me.TPFacilitySpecific.Controls.Add(Me.Label10)
        Me.TPFacilitySpecific.Controls.Add(Me.llbViewAll)
        Me.TPFacilitySpecific.Controls.Add(Me.Label)
        Me.TPFacilitySpecific.Location = New System.Drawing.Point(4, 22)
        Me.TPFacilitySpecific.Margin = New System.Windows.Forms.Padding(2)
        Me.TPFacilitySpecific.Name = "TPFacilitySpecific"
        Me.TPFacilitySpecific.Padding = New System.Windows.Forms.Padding(2)
        Me.TPFacilitySpecific.Size = New System.Drawing.Size(661, 88)
        Me.TPFacilitySpecific.TabIndex = 0
        Me.TPFacilitySpecific.Text = "Facility Specific"
        Me.TPFacilitySpecific.UseVisualStyleBackColor = True
        '
        'cboFacilityName
        '
        Me.cboFacilityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityName.Location = New System.Drawing.Point(82, 8)
        Me.cboFacilityName.Margin = New System.Windows.Forms.Padding(2)
        Me.cboFacilityName.Name = "cboFacilityName"
        Me.cboFacilityName.Size = New System.Drawing.Size(194, 21)
        Me.cboFacilityName.TabIndex = 145
        '
        'cboAirsNo
        '
        Me.cboAirsNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirsNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirsNo.Location = New System.Drawing.Point(369, 8)
        Me.cboAirsNo.Margin = New System.Windows.Forms.Padding(2)
        Me.cboAirsNo.Name = "cboAirsNo"
        Me.cboAirsNo.Size = New System.Drawing.Size(98, 21)
        Me.cboAirsNo.TabIndex = 146
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(276, 11)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 13)
        Me.Label10.TabIndex = 148
        Me.Label10.Text = "OR AIRS Number:"
        '
        'llbViewAll
        '
        Me.llbViewAll.AutoSize = True
        Me.llbViewAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbViewAll.Location = New System.Drawing.Point(471, 11)
        Me.llbViewAll.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbViewAll.Name = "llbViewAll"
        Me.llbViewAll.Size = New System.Drawing.Size(56, 13)
        Me.llbViewAll.TabIndex = 149
        Me.llbViewAll.TabStop = True
        Me.llbViewAll.Text = "View Data"
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label.Location = New System.Drawing.Point(6, 11)
        Me.Label.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(73, 13)
        Me.Label.TabIndex = 147
        Me.Label.Text = "Facility Name:"
        Me.Label.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPFinancial
        '
        Me.TPFinancial.Controls.Add(Me.pnlDateRange)
        Me.TPFinancial.Controls.Add(Me.Panel4)
        Me.TPFinancial.Location = New System.Drawing.Point(4, 22)
        Me.TPFinancial.Margin = New System.Windows.Forms.Padding(2)
        Me.TPFinancial.Name = "TPFinancial"
        Me.TPFinancial.Padding = New System.Windows.Forms.Padding(2)
        Me.TPFinancial.Size = New System.Drawing.Size(661, 88)
        Me.TPFinancial.TabIndex = 1
        Me.TPFinancial.Text = "Financial"
        Me.TPFinancial.UseVisualStyleBackColor = True
        '
        'pnlDateRange
        '
        Me.pnlDateRange.Controls.Add(Me.btnRunVarianceReport)
        Me.pnlDateRange.Controls.Add(Me.rdb2006Variance)
        Me.pnlDateRange.Controls.Add(Me.rdb2005Variance)
        Me.pnlDateRange.Controls.Add(Me.btndeposit)
        Me.pnlDateRange.Controls.Add(Me.btnDateReport)
        Me.pnlDateRange.Controls.Add(Me.Label5)
        Me.pnlDateRange.Controls.Add(Me.DateTimePicker2)
        Me.pnlDateRange.Controls.Add(Me.Label4)
        Me.pnlDateRange.Controls.Add(Me.Label3)
        Me.pnlDateRange.Controls.Add(Me.DateTimePicker1)
        Me.pnlDateRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDateRange.Location = New System.Drawing.Point(2, 26)
        Me.pnlDateRange.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(657, 60)
        Me.pnlDateRange.TabIndex = 149
        Me.pnlDateRange.Visible = False
        '
        'btnRunVarianceReport
        '
        Me.btnRunVarianceReport.AutoSize = True
        Me.btnRunVarianceReport.Location = New System.Drawing.Point(243, 13)
        Me.btnRunVarianceReport.Name = "btnRunVarianceReport"
        Me.btnRunVarianceReport.Size = New System.Drawing.Size(117, 23)
        Me.btnRunVarianceReport.TabIndex = 9
        Me.btnRunVarianceReport.Text = "Run Variance Report"
        Me.btnRunVarianceReport.UseVisualStyleBackColor = True
        '
        'rdb2006Variance
        '
        Me.rdb2006Variance.AutoSize = True
        Me.rdb2006Variance.Location = New System.Drawing.Point(6, 21)
        Me.rdb2006Variance.Name = "rdb2006Variance"
        Me.rdb2006Variance.Size = New System.Drawing.Size(207, 17)
        Me.rdb2006Variance.TabIndex = 8
        Me.rdb2006Variance.TabStop = True
        Me.rdb2006Variance.Text = "Fee Variance between 2005 and 2006"
        Me.rdb2006Variance.UseVisualStyleBackColor = True
        '
        'rdb2005Variance
        '
        Me.rdb2005Variance.AutoSize = True
        Me.rdb2005Variance.Location = New System.Drawing.Point(6, 3)
        Me.rdb2005Variance.Name = "rdb2005Variance"
        Me.rdb2005Variance.Size = New System.Drawing.Size(207, 17)
        Me.rdb2005Variance.TabIndex = 7
        Me.rdb2005Variance.TabStop = True
        Me.rdb2005Variance.Text = "Fee Variance between 2004 and 2005"
        Me.rdb2005Variance.UseVisualStyleBackColor = True
        '
        'btndeposit
        '
        Me.btndeposit.Location = New System.Drawing.Point(375, 21)
        Me.btndeposit.Margin = New System.Windows.Forms.Padding(2)
        Me.btndeposit.Name = "btndeposit"
        Me.btndeposit.Size = New System.Drawing.Size(109, 19)
        Me.btndeposit.TabIndex = 6
        Me.btndeposit.Text = "Get Deposit Report"
        '
        'btnDateReport
        '
        Me.btnDateReport.Location = New System.Drawing.Point(258, 20)
        Me.btnDateReport.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDateReport.Name = "btnDateReport"
        Me.btnDateReport.Size = New System.Drawing.Size(102, 19)
        Me.btnDateReport.TabIndex = 5
        Me.btnDateReport.Text = "Get Detail Report"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(138, 23)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "To: "
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(162, 20)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(92, 20)
        Me.DateTimePicker2.TabIndex = 3
        Me.DateTimePicker2.Value = New Date(2008, 8, 12, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 23)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "From: "
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(588, 19)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Select the Date Range and click on ""Get Report"" button. If no date is selected, t" & _
            "oday's date will be assumed."
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(42, 20)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(92, 20)
        Me.DateTimePicker1.TabIndex = 0
        Me.DateTimePicker1.Value = New Date(2008, 8, 12, 0, 0, 0, 0)
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnvariance)
        Me.Panel4.Controls.Add(Me.btnFeeByYear)
        Me.Panel4.Controls.Add(Me.btnBankrupt)
        Me.Panel4.Controls.Add(Me.btnPayDate)
        Me.Panel4.Controls.Add(Me.btnPayment)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(2, 2)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(657, 24)
        Me.Panel4.TabIndex = 147
        '
        'btnvariance
        '
        Me.btnvariance.AutoSize = True
        Me.btnvariance.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnvariance.CausesValidation = False
        Me.btnvariance.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnvariance.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnvariance.Location = New System.Drawing.Point(422, 0)
        Me.btnvariance.Margin = New System.Windows.Forms.Padding(2)
        Me.btnvariance.Name = "btnvariance"
        Me.btnvariance.Size = New System.Drawing.Size(94, 23)
        Me.btnvariance.TabIndex = 4
        Me.btnvariance.Text = "Variance Report"
        Me.btnvariance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnvariance.UseVisualStyleBackColor = False
        '
        'btnFeeByYear
        '
        Me.btnFeeByYear.AutoSize = True
        Me.btnFeeByYear.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnFeeByYear.CausesValidation = False
        Me.btnFeeByYear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFeeByYear.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnFeeByYear.Location = New System.Drawing.Point(314, 0)
        Me.btnFeeByYear.Margin = New System.Windows.Forms.Padding(2)
        Me.btnFeeByYear.Name = "btnFeeByYear"
        Me.btnFeeByYear.Size = New System.Drawing.Size(109, 23)
        Me.btnFeeByYear.TabIndex = 3
        Me.btnFeeByYear.Text = "Facility Fee by Year"
        Me.btnFeeByYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFeeByYear.UseVisualStyleBackColor = False
        '
        'btnBankrupt
        '
        Me.btnBankrupt.AutoSize = True
        Me.btnBankrupt.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnBankrupt.CausesValidation = False
        Me.btnBankrupt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBankrupt.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnBankrupt.Location = New System.Drawing.Point(214, 0)
        Me.btnBankrupt.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBankrupt.Name = "btnBankrupt"
        Me.btnBankrupt.Size = New System.Drawing.Size(103, 23)
        Me.btnBankrupt.TabIndex = 2
        Me.btnBankrupt.Text = "Bankrupt Facilities"
        Me.btnBankrupt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBankrupt.UseVisualStyleBackColor = False
        '
        'btnPayDate
        '
        Me.btnPayDate.AutoSize = True
        Me.btnPayDate.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnPayDate.CausesValidation = False
        Me.btnPayDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPayDate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnPayDate.Location = New System.Drawing.Point(112, 0)
        Me.btnPayDate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPayDate.Name = "btnPayDate"
        Me.btnPayDate.Size = New System.Drawing.Size(103, 23)
        Me.btnPayDate.TabIndex = 1
        Me.btnPayDate.Text = "Payments by Date"
        Me.btnPayDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPayDate.UseVisualStyleBackColor = False
        '
        'btnPayment
        '
        Me.btnPayment.AutoSize = True
        Me.btnPayment.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnPayment.CausesValidation = False
        Me.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPayment.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnPayment.Location = New System.Drawing.Point(0, 0)
        Me.btnPayment.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(113, 23)
        Me.btnPayment.TabIndex = 0
        Me.btnPayment.Text = "Overall Fee Balance"
        Me.btnPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPayment.UseVisualStyleBackColor = False
        '
        'TPYearSpecific
        '
        Me.TPYearSpecific.Controls.Add(Me.Panel5)
        Me.TPYearSpecific.Location = New System.Drawing.Point(4, 22)
        Me.TPYearSpecific.Margin = New System.Windows.Forms.Padding(2)
        Me.TPYearSpecific.Name = "TPYearSpecific"
        Me.TPYearSpecific.Size = New System.Drawing.Size(661, 88)
        Me.TPYearSpecific.TabIndex = 2
        Me.TPYearSpecific.Text = "Year Specific"
        Me.TPYearSpecific.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.mtbFacilityBalanceYear)
        Me.Panel5.Controls.Add(Me.chbFacilityBalance)
        Me.Panel5.Controls.Add(Me.lblFacilityBalanceReportTag)
        Me.Panel5.Controls.Add(Me.btnRunBalanceReport)
        Me.Panel5.Controls.Add(Me.btnFeeBalanceZero)
        Me.Panel5.Controls.Add(Me.btnFeeBalance)
        Me.Panel5.Controls.Add(Me.btnClassification)
        Me.Panel5.Controls.Add(Me.btnFeesandEmissions)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(661, 88)
        Me.Panel5.TabIndex = 146
        '
        'mtbFacilityBalanceYear
        '
        Me.mtbFacilityBalanceYear.Location = New System.Drawing.Point(11, 47)
        Me.mtbFacilityBalanceYear.Mask = "0000"
        Me.mtbFacilityBalanceYear.Name = "mtbFacilityBalanceYear"
        Me.mtbFacilityBalanceYear.Size = New System.Drawing.Size(37, 20)
        Me.mtbFacilityBalanceYear.TabIndex = 8
        '
        'chbFacilityBalance
        '
        Me.chbFacilityBalance.AutoSize = True
        Me.chbFacilityBalance.Enabled = False
        Me.chbFacilityBalance.Location = New System.Drawing.Point(54, 68)
        Me.chbFacilityBalance.Name = "chbFacilityBalance"
        Me.chbFacilityBalance.Size = New System.Drawing.Size(90, 17)
        Me.chbFacilityBalance.TabIndex = 7
        Me.chbFacilityBalance.Text = "Zero Balance"
        Me.chbFacilityBalance.UseVisualStyleBackColor = True
        '
        'lblFacilityBalanceReportTag
        '
        Me.lblFacilityBalanceReportTag.AutoSize = True
        Me.lblFacilityBalanceReportTag.Location = New System.Drawing.Point(8, 29)
        Me.lblFacilityBalanceReportTag.Name = "lblFacilityBalanceReportTag"
        Me.lblFacilityBalanceReportTag.Size = New System.Drawing.Size(533, 13)
        Me.lblFacilityBalanceReportTag.TabIndex = 6
        Me.lblFacilityBalanceReportTag.Text = "Please enter in a valid year (2000 or 2001, etc...) If you do not enter a valid y" & _
            "ear the current year will be entered."
        '
        'btnRunBalanceReport
        '
        Me.btnRunBalanceReport.AutoSize = True
        Me.btnRunBalanceReport.Location = New System.Drawing.Point(54, 44)
        Me.btnRunBalanceReport.Name = "btnRunBalanceReport"
        Me.btnRunBalanceReport.Size = New System.Drawing.Size(114, 23)
        Me.btnRunBalanceReport.TabIndex = 4
        Me.btnRunBalanceReport.Text = "Run Balance Report"
        Me.btnRunBalanceReport.UseVisualStyleBackColor = True
        '
        'btnFeeBalanceZero
        '
        Me.btnFeeBalanceZero.AutoSize = True
        Me.btnFeeBalanceZero.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnFeeBalanceZero.CausesValidation = False
        Me.btnFeeBalanceZero.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFeeBalanceZero.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnFeeBalanceZero.Location = New System.Drawing.Point(369, 0)
        Me.btnFeeBalanceZero.Margin = New System.Windows.Forms.Padding(2)
        Me.btnFeeBalanceZero.Name = "btnFeeBalanceZero"
        Me.btnFeeBalanceZero.Size = New System.Drawing.Size(235, 23)
        Me.btnFeeBalanceZero.TabIndex = 3
        Me.btnFeeBalanceZero.Text = "Facility Fee Balance with Zero Balance shown"
        Me.btnFeeBalanceZero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFeeBalanceZero.UseVisualStyleBackColor = False
        '
        'btnFeeBalance
        '
        Me.btnFeeBalance.AutoSize = True
        Me.btnFeeBalance.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnFeeBalance.CausesValidation = False
        Me.btnFeeBalance.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFeeBalance.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnFeeBalance.Location = New System.Drawing.Point(258, 0)
        Me.btnFeeBalance.Margin = New System.Windows.Forms.Padding(2)
        Me.btnFeeBalance.Name = "btnFeeBalance"
        Me.btnFeeBalance.Size = New System.Drawing.Size(112, 23)
        Me.btnFeeBalance.TabIndex = 2
        Me.btnFeeBalance.Text = "Facility Fee Balance"
        Me.btnFeeBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFeeBalance.UseVisualStyleBackColor = False
        '
        'btnClassification
        '
        Me.btnClassification.AutoSize = True
        Me.btnClassification.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnClassification.CausesValidation = False
        Me.btnClassification.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClassification.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnClassification.Location = New System.Drawing.Point(120, 0)
        Me.btnClassification.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClassification.Name = "btnClassification"
        Me.btnClassification.Size = New System.Drawing.Size(145, 23)
        Me.btnClassification.TabIndex = 1
        Me.btnClassification.Text = "Facility Classification Totals"
        Me.btnClassification.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClassification.UseVisualStyleBackColor = False
        '
        'btnFeesandEmissions
        '
        Me.btnFeesandEmissions.AutoSize = True
        Me.btnFeesandEmissions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnFeesandEmissions.CausesValidation = False
        Me.btnFeesandEmissions.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFeesandEmissions.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnFeesandEmissions.Location = New System.Drawing.Point(0, 0)
        Me.btnFeesandEmissions.Margin = New System.Windows.Forms.Padding(2)
        Me.btnFeesandEmissions.Name = "btnFeesandEmissions"
        Me.btnFeesandEmissions.Size = New System.Drawing.Size(125, 23)
        Me.btnFeesandEmissions.TabIndex = 0
        Me.btnFeesandEmissions.Text = "Total Fees && Emissions"
        Me.btnFeesandEmissions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFeesandEmissions.UseVisualStyleBackColor = False
        '
        'TPDeposits
        '
        Me.TPDeposits.Controls.Add(Me.cboDepositNo)
        Me.TPDeposits.Controls.Add(Me.cboAirs)
        Me.TPDeposits.Controls.Add(Me.Label1)
        Me.TPDeposits.Controls.Add(Me.lblDepositData)
        Me.TPDeposits.Controls.Add(Me.Label2)
        Me.TPDeposits.Location = New System.Drawing.Point(4, 22)
        Me.TPDeposits.Margin = New System.Windows.Forms.Padding(2)
        Me.TPDeposits.Name = "TPDeposits"
        Me.TPDeposits.Size = New System.Drawing.Size(661, 88)
        Me.TPDeposits.TabIndex = 3
        Me.TPDeposits.Text = "Deposits"
        Me.TPDeposits.UseVisualStyleBackColor = True
        '
        'cboDepositNo
        '
        Me.cboDepositNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDepositNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDepositNo.Location = New System.Drawing.Point(94, 7)
        Me.cboDepositNo.Margin = New System.Windows.Forms.Padding(2)
        Me.cboDepositNo.Name = "cboDepositNo"
        Me.cboDepositNo.Size = New System.Drawing.Size(109, 21)
        Me.cboDepositNo.Sorted = True
        Me.cboDepositNo.TabIndex = 144
        '
        'cboAirs
        '
        Me.cboAirs.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirs.Location = New System.Drawing.Point(310, 11)
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
        Me.Label1.Location = New System.Drawing.Point(214, 13)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 147
        Me.Label1.Text = "OR AIRS Number:"
        '
        'lblDepositData
        '
        Me.lblDepositData.AutoSize = True
        Me.lblDepositData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositData.Location = New System.Drawing.Point(422, 13)
        Me.lblDepositData.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDepositData.Name = "lblDepositData"
        Me.lblDepositData.Size = New System.Drawing.Size(56, 13)
        Me.lblDepositData.TabIndex = 148
        Me.lblDepositData.TabStop = True
        Me.lblDepositData.Text = "View Data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "Deposit Number:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPCompliance
        '
        Me.TPCompliance.Controls.Add(Me.pnlNSPS)
        Me.TPCompliance.Controls.Add(Me.Panel6)
        Me.TPCompliance.Location = New System.Drawing.Point(4, 22)
        Me.TPCompliance.Margin = New System.Windows.Forms.Padding(2)
        Me.TPCompliance.Name = "TPCompliance"
        Me.TPCompliance.Size = New System.Drawing.Size(661, 88)
        Me.TPCompliance.TabIndex = 4
        Me.TPCompliance.Text = "Compliance"
        Me.TPCompliance.UseVisualStyleBackColor = True
        '
        'pnlNSPS
        '
        Me.pnlNSPS.Controls.Add(Me.btnRunNonRespondent)
        Me.pnlNSPS.Controls.Add(Me.lblNonRespondant)
        Me.pnlNSPS.Controls.Add(Me.mtbNonRespondentYear)
        Me.pnlNSPS.Controls.Add(Me.lblNSPS3)
        Me.pnlNSPS.Controls.Add(Me.lblNSPS2)
        Me.pnlNSPS.Controls.Add(Me.lblNSPS1)
        Me.pnlNSPS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNSPS.Location = New System.Drawing.Point(0, 29)
        Me.pnlNSPS.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlNSPS.Name = "pnlNSPS"
        Me.pnlNSPS.Size = New System.Drawing.Size(661, 59)
        Me.pnlNSPS.TabIndex = 149
        Me.pnlNSPS.Visible = False
        '
        'btnRunNonRespondent
        '
        Me.btnRunNonRespondent.AutoSize = True
        Me.btnRunNonRespondent.Location = New System.Drawing.Point(46, 14)
        Me.btnRunNonRespondent.Name = "btnRunNonRespondent"
        Me.btnRunNonRespondent.Size = New System.Drawing.Size(126, 23)
        Me.btnRunNonRespondent.TabIndex = 5
        Me.btnRunNonRespondent.Text = "Run Non Respondents"
        Me.btnRunNonRespondent.UseVisualStyleBackColor = True
        '
        'lblNonRespondant
        '
        Me.lblNonRespondant.AutoSize = True
        Me.lblNonRespondant.Location = New System.Drawing.Point(1, 1)
        Me.lblNonRespondant.Name = "lblNonRespondant"
        Me.lblNonRespondant.Size = New System.Drawing.Size(292, 13)
        Me.lblNonRespondant.TabIndex = 4
        Me.lblNonRespondant.Text = "Enter a year - if none is entered the current year will be used."
        '
        'mtbNonRespondentYear
        '
        Me.mtbNonRespondentYear.Location = New System.Drawing.Point(4, 17)
        Me.mtbNonRespondentYear.Mask = "0000"
        Me.mtbNonRespondentYear.Name = "mtbNonRespondentYear"
        Me.mtbNonRespondentYear.Size = New System.Drawing.Size(36, 20)
        Me.mtbNonRespondentYear.TabIndex = 3
        '
        'lblNSPS3
        '
        Me.lblNSPS3.AutoSize = True
        Me.lblNSPS3.Location = New System.Drawing.Point(0, 39)
        Me.lblNSPS3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNSPS3.Name = "lblNSPS3"
        Me.lblNSPS3.Size = New System.Drawing.Size(363, 13)
        Me.lblNSPS3.TabIndex = 2
        Me.lblNSPS3.TabStop = True
        Me.lblNSPS3.Text = "All facilities that are subject to NSPS, but indicated that they did not operate"
        '
        'lblNSPS2
        '
        Me.lblNSPS2.AutoSize = True
        Me.lblNSPS2.Location = New System.Drawing.Point(0, 20)
        Me.lblNSPS2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNSPS2.Name = "lblNSPS2"
        Me.lblNSPS2.Size = New System.Drawing.Size(357, 13)
        Me.lblNSPS2.TabIndex = 1
        Me.lblNSPS2.TabStop = True
        Me.lblNSPS2.Text = "All facilities that are not subject to NSPS, but indicated that they are NSPS"
        '
        'lblNSPS1
        '
        Me.lblNSPS1.AutoSize = True
        Me.lblNSPS1.Location = New System.Drawing.Point(0, 0)
        Me.lblNSPS1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNSPS1.Name = "lblNSPS1"
        Me.lblNSPS1.Size = New System.Drawing.Size(335, 13)
        Me.lblNSPS1.TabIndex = 0
        Me.lblNSPS1.TabStop = True
        Me.lblNSPS1.Text = "All facilities that are subject to NSPS, but chose to exempt from NSPS"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnNSPSChange)
        Me.Panel6.Controls.Add(Me.btnNoOperate)
        Me.Panel6.Controls.Add(Me.btnNoResponse)
        Me.Panel6.Controls.Add(Me.btnClassChange)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(661, 29)
        Me.Panel6.TabIndex = 147
        '
        'btnNSPSChange
        '
        Me.btnNSPSChange.AutoSize = True
        Me.btnNSPSChange.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnNSPSChange.CausesValidation = False
        Me.btnNSPSChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNSPSChange.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnNSPSChange.Location = New System.Drawing.Point(128, 0)
        Me.btnNSPSChange.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNSPSChange.Name = "btnNSPSChange"
        Me.btnNSPSChange.Size = New System.Drawing.Size(130, 23)
        Me.btnNSPSChange.TabIndex = 4
        Me.btnNSPSChange.Text = "Change in NSPS Status"
        Me.btnNSPSChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNSPSChange.UseVisualStyleBackColor = False
        '
        'btnNoOperate
        '
        Me.btnNoOperate.AutoSize = True
        Me.btnNoOperate.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnNoOperate.CausesValidation = False
        Me.btnNoOperate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNoOperate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnNoOperate.Location = New System.Drawing.Point(358, 0)
        Me.btnNoOperate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNoOperate.Name = "btnNoOperate"
        Me.btnNoOperate.Size = New System.Drawing.Size(94, 23)
        Me.btnNoOperate.TabIndex = 3
        Me.btnNoOperate.Text = "Did Not Operate"
        Me.btnNoOperate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNoOperate.UseVisualStyleBackColor = False
        '
        'btnNoResponse
        '
        Me.btnNoResponse.AutoSize = True
        Me.btnNoResponse.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnNoResponse.CausesValidation = False
        Me.btnNoResponse.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNoResponse.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnNoResponse.Location = New System.Drawing.Point(257, 0)
        Me.btnNoResponse.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNoResponse.Name = "btnNoResponse"
        Me.btnNoResponse.Size = New System.Drawing.Size(103, 23)
        Me.btnNoResponse.TabIndex = 1
        Me.btnNoResponse.Text = "Non Respondents"
        Me.btnNoResponse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNoResponse.UseVisualStyleBackColor = False
        '
        'btnClassChange
        '
        Me.btnClassChange.AutoSize = True
        Me.btnClassChange.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnClassChange.CausesValidation = False
        Me.btnClassChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClassChange.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnClassChange.Location = New System.Drawing.Point(0, 0)
        Me.btnClassChange.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClassChange.Name = "btnClassChange"
        Me.btnClassChange.Size = New System.Drawing.Size(129, 23)
        Me.btnClassChange.TabIndex = 0
        Me.btnClassChange.Text = "Change in Classification"
        Me.btnClassChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClassChange.UseVisualStyleBackColor = False
        '
        'TPGeneral
        '
        Me.TPGeneral.Controls.Add(Me.Panel7)
        Me.TPGeneral.Location = New System.Drawing.Point(4, 22)
        Me.TPGeneral.Margin = New System.Windows.Forms.Padding(2)
        Me.TPGeneral.Name = "TPGeneral"
        Me.TPGeneral.Size = New System.Drawing.Size(661, 88)
        Me.TPGeneral.TabIndex = 5
        Me.TPGeneral.Text = "General"
        Me.TPGeneral.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.btnTrainingReg)
        Me.Panel7.Controls.Add(Me.btnFacInfoChange)
        Me.Panel7.Controls.Add(Me.btnComments)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(661, 88)
        Me.Panel7.TabIndex = 148
        '
        'btnTrainingReg
        '
        Me.btnTrainingReg.AutoSize = True
        Me.btnTrainingReg.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnTrainingReg.CausesValidation = False
        Me.btnTrainingReg.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTrainingReg.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnTrainingReg.Location = New System.Drawing.Point(212, 0)
        Me.btnTrainingReg.Margin = New System.Windows.Forms.Padding(2)
        Me.btnTrainingReg.Name = "btnTrainingReg"
        Me.btnTrainingReg.Size = New System.Drawing.Size(111, 23)
        Me.btnTrainingReg.TabIndex = 4
        Me.btnTrainingReg.Text = "Training Registrants"
        Me.btnTrainingReg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTrainingReg.UseVisualStyleBackColor = False
        Me.btnTrainingReg.Visible = False
        '
        'btnFacInfoChange
        '
        Me.btnFacInfoChange.AutoSize = True
        Me.btnFacInfoChange.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnFacInfoChange.CausesValidation = False
        Me.btnFacInfoChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFacInfoChange.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnFacInfoChange.Location = New System.Drawing.Point(96, 0)
        Me.btnFacInfoChange.Margin = New System.Windows.Forms.Padding(2)
        Me.btnFacInfoChange.Name = "btnFacInfoChange"
        Me.btnFacInfoChange.Size = New System.Drawing.Size(121, 23)
        Me.btnFacInfoChange.TabIndex = 3
        Me.btnFacInfoChange.Text = "Change in Facility Info"
        Me.btnFacInfoChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFacInfoChange.UseVisualStyleBackColor = False
        '
        'btnComments
        '
        Me.btnComments.AutoSize = True
        Me.btnComments.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnComments.CausesValidation = False
        Me.btnComments.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnComments.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnComments.Location = New System.Drawing.Point(0, 0)
        Me.btnComments.Margin = New System.Windows.Forms.Padding(2)
        Me.btnComments.Name = "btnComments"
        Me.btnComments.Size = New System.Drawing.Size(101, 23)
        Me.btnComments.TabIndex = 2
        Me.btnComments.Text = "Facility Comments"
        Me.btnComments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnComments.UseVisualStyleBackColor = False
        '
        'CRFeesReports
        '
        Me.CRFeesReports.ActiveViewIndex = -1
        Me.CRFeesReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRFeesReports.DisplayGroupTree = False
        Me.CRFeesReports.DisplayToolbar = False
        Me.CRFeesReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRFeesReports.Location = New System.Drawing.Point(0, 114)
        Me.CRFeesReports.Margin = New System.Windows.Forms.Padding(2)
        Me.CRFeesReports.Name = "CRFeesReports"
        Me.CRFeesReports.SelectionFormula = ""
        Me.CRFeesReports.Size = New System.Drawing.Size(669, 422)
        Me.CRFeesReports.TabIndex = 269
        Me.CRFeesReports.ViewTimeSelectionFormula = ""
        '
        'PASPFeeReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 536)
        Me.Controls.Add(Me.CRFeesReports)
        Me.Controls.Add(Me.tabReport)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "PASPFeeReports"
        Me.Text = "Fee Reports"
        Me.tabReport.ResumeLayout(False)
        Me.TPFacilitySpecific.ResumeLayout(False)
        Me.TPFacilitySpecific.PerformLayout()
        Me.TPFinancial.ResumeLayout(False)
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRange.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TPYearSpecific.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TPDeposits.ResumeLayout(False)
        Me.TPDeposits.PerformLayout()
        Me.TPCompliance.ResumeLayout(False)
        Me.pnlNSPS.ResumeLayout(False)
        Me.pnlNSPS.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.TPGeneral.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabReport As System.Windows.Forms.TabControl
    Friend WithEvents TPFacilitySpecific As System.Windows.Forms.TabPage
    Friend WithEvents TPFinancial As System.Windows.Forms.TabPage
    Friend WithEvents CRFeesReports As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents TPYearSpecific As System.Windows.Forms.TabPage
    Friend WithEvents TPDeposits As System.Windows.Forms.TabPage
    Friend WithEvents TPCompliance As System.Windows.Forms.TabPage
    Friend WithEvents TPGeneral As System.Windows.Forms.TabPage
    Friend WithEvents cboFacilityName As System.Windows.Forms.ComboBox
    Friend WithEvents cboAirsNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents llbViewAll As System.Windows.Forms.LinkLabel
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnvariance As System.Windows.Forms.Button
    Friend WithEvents btnFeeByYear As System.Windows.Forms.Button
    Friend WithEvents btnBankrupt As System.Windows.Forms.Button
    Friend WithEvents btnPayDate As System.Windows.Forms.Button
    Friend WithEvents btnPayment As System.Windows.Forms.Button
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Friend WithEvents btnDateReport As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnFeeBalance As System.Windows.Forms.Button
    Friend WithEvents btnClassification As System.Windows.Forms.Button
    Friend WithEvents btnFeesandEmissions As System.Windows.Forms.Button
    Friend WithEvents cboDepositNo As System.Windows.Forms.ComboBox
    Friend WithEvents cboAirs As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDepositData As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnNSPSChange As System.Windows.Forms.Button
    Friend WithEvents btnNoOperate As System.Windows.Forms.Button
    Friend WithEvents btnNoResponse As System.Windows.Forms.Button
    Friend WithEvents btnClassChange As System.Windows.Forms.Button
    Friend WithEvents pnlNSPS As System.Windows.Forms.Panel
    Friend WithEvents lblNSPS3 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblNSPS2 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblNSPS1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnFacInfoChange As System.Windows.Forms.Button
    Friend WithEvents btnComments As System.Windows.Forms.Button
    Friend WithEvents btndeposit As System.Windows.Forms.Button
    Friend WithEvents btnFeeBalanceZero As System.Windows.Forms.Button
    Friend WithEvents btnTrainingReg As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnRunBalanceReport As System.Windows.Forms.Button
    Friend WithEvents chbFacilityBalance As System.Windows.Forms.CheckBox
    Friend WithEvents lblFacilityBalanceReportTag As System.Windows.Forms.Label
    Friend WithEvents mtbFacilityBalanceYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents rdb2006Variance As System.Windows.Forms.RadioButton
    Friend WithEvents rdb2005Variance As System.Windows.Forms.RadioButton
    Friend WithEvents btnRunVarianceReport As System.Windows.Forms.Button
    Friend WithEvents mtbNonRespondentYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnRunNonRespondent As System.Windows.Forms.Button
    Friend WithEvents lblNonRespondant As System.Windows.Forms.Label
End Class
