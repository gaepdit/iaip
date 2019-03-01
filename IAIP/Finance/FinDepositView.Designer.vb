<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinDepositView
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dtpDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.btnSaveNewDeposit = New System.Windows.Forms.Button()
        Me.grpDepositDetails = New System.Windows.Forms.GroupBox()
        Me.txtDepositComments = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnUpdateDepositDetails = New System.Windows.Forms.Button()
        Me.lblDetailsMessage = New System.Windows.Forms.Label()
        Me.txtDepositAmount = New Iaip.CurrencyTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCreditConf = New System.Windows.Forms.TextBox()
        Me.txtBatchNumber = New System.Windows.Forms.TextBox()
        Me.txtDepositNumber = New System.Windows.Forms.TextBox()
        Me.txtCheckNumber = New System.Windows.Forms.TextBox()
        Me.btnDeleteDeposit = New System.Windows.Forms.Button()
        Me.lblDepositDisplay = New System.Windows.Forms.Label()
        Me.dgvInvoicesPaid = New Iaip.IaipDataGridView()
        Me.txtInvoiceToApply = New System.Windows.Forms.TextBox()
        Me.lblAmountToApply = New System.Windows.Forms.Label()
        Me.lblInvoiceToApply = New System.Windows.Forms.Label()
        Me.txtAmountToApply = New Iaip.CurrencyTextBox()
        Me.btnApplyToInvoice = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAirsInvoiceSearch = New Iaip.AirNumberEntryForm()
        Me.lblSearchFacilityDisplay = New System.Windows.Forms.Label()
        Me.grpInvoiceSearch = New System.Windows.Forms.GroupBox()
        Me.chkOnlyOpen = New System.Windows.Forms.CheckBox()
        Me.dgvSearchResults = New Iaip.IaipDataGridView()
        Me.btnInvoiceSearch = New System.Windows.Forms.Button()
        Me.grpRefunds = New System.Windows.Forms.GroupBox()
        Me.dgvRefunds = New Iaip.IaipDataGridView()
        Me.lblNoRefunds = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.InvoiceLine = New System.Windows.Forms.Label()
        Me.txtDepositBalance = New Iaip.CurrencyTextBox()
        Me.txtTotalDeposit = New Iaip.CurrencyTextBox()
        Me.txtAmountRefunded = New Iaip.CurrencyTextBox()
        Me.txtAmountAppliedToInvoices = New Iaip.CurrencyTextBox()
        Me.grpApplyToInvoice = New System.Windows.Forms.GroupBox()
        Me.lblApplyToInvoiceMessage = New System.Windows.Forms.Label()
        Me.lblInvoicesPaid = New System.Windows.Forms.Label()
        Me.grpSummary = New System.Windows.Forms.GroupBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblDeleteDepositMessage = New System.Windows.Forms.Label()
        Me.grpDepositDetails.SuspendLayout()
        CType(Me.dgvInvoicesPaid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInvoiceSearch.SuspendLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRefunds.SuspendLayout()
        CType(Me.dgvRefunds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpApplyToInvoice.SuspendLayout()
        Me.grpSummary.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpDepositDate
        '
        Me.dtpDepositDate.Checked = False
        Me.dtpDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositDate.Location = New System.Drawing.Point(100, 25)
        Me.dtpDepositDate.Name = "dtpDepositDate"
        Me.dtpDepositDate.Size = New System.Drawing.Size(118, 20)
        Me.dtpDepositDate.TabIndex = 0
        '
        'btnSaveNewDeposit
        '
        Me.btnSaveNewDeposit.AutoSize = True
        Me.btnSaveNewDeposit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveNewDeposit.Location = New System.Drawing.Point(15, 290)
        Me.btnSaveNewDeposit.Name = "btnSaveNewDeposit"
        Me.btnSaveNewDeposit.Size = New System.Drawing.Size(133, 27)
        Me.btnSaveNewDeposit.TabIndex = 8
        Me.btnSaveNewDeposit.Text = "Save New Deposit"
        Me.btnSaveNewDeposit.UseVisualStyleBackColor = True
        '
        'grpDepositDetails
        '
        Me.grpDepositDetails.Controls.Add(Me.txtDepositComments)
        Me.grpDepositDetails.Controls.Add(Me.Label7)
        Me.grpDepositDetails.Controls.Add(Me.btnUpdateDepositDetails)
        Me.grpDepositDetails.Controls.Add(Me.lblDetailsMessage)
        Me.grpDepositDetails.Controls.Add(Me.txtDepositAmount)
        Me.grpDepositDetails.Controls.Add(Me.Label8)
        Me.grpDepositDetails.Controls.Add(Me.Label2)
        Me.grpDepositDetails.Controls.Add(Me.Label3)
        Me.grpDepositDetails.Controls.Add(Me.Label5)
        Me.grpDepositDetails.Controls.Add(Me.Label6)
        Me.grpDepositDetails.Controls.Add(Me.Label4)
        Me.grpDepositDetails.Controls.Add(Me.txtCreditConf)
        Me.grpDepositDetails.Controls.Add(Me.txtBatchNumber)
        Me.grpDepositDetails.Controls.Add(Me.txtDepositNumber)
        Me.grpDepositDetails.Controls.Add(Me.dtpDepositDate)
        Me.grpDepositDetails.Controls.Add(Me.txtCheckNumber)
        Me.grpDepositDetails.Controls.Add(Me.btnSaveNewDeposit)
        Me.grpDepositDetails.Location = New System.Drawing.Point(12, 50)
        Me.grpDepositDetails.Name = "grpDepositDetails"
        Me.grpDepositDetails.Size = New System.Drawing.Size(231, 373)
        Me.grpDepositDetails.TabIndex = 0
        Me.grpDepositDetails.TabStop = False
        Me.grpDepositDetails.Text = "Deposit Details"
        '
        'txtDepositComments
        '
        Me.txtDepositComments.Location = New System.Drawing.Point(12, 213)
        Me.txtDepositComments.Multiline = True
        Me.txtDepositComments.Name = "txtDepositComments"
        Me.txtDepositComments.Size = New System.Drawing.Size(206, 71)
        Me.txtDepositComments.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Comments"
        '
        'btnUpdateDepositDetails
        '
        Me.btnUpdateDepositDetails.AutoSize = True
        Me.btnUpdateDepositDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateDepositDetails.Location = New System.Drawing.Point(15, 290)
        Me.btnUpdateDepositDetails.Name = "btnUpdateDepositDetails"
        Me.btnUpdateDepositDetails.Size = New System.Drawing.Size(111, 27)
        Me.btnUpdateDepositDetails.TabIndex = 7
        Me.btnUpdateDepositDetails.Text = "Update Details"
        Me.btnUpdateDepositDetails.UseVisualStyleBackColor = True
        '
        'lblDetailsMessage
        '
        Me.lblDetailsMessage.AutoSize = True
        Me.lblDetailsMessage.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblDetailsMessage.Location = New System.Drawing.Point(12, 320)
        Me.lblDetailsMessage.MaximumSize = New System.Drawing.Size(206, 0)
        Me.lblDetailsMessage.Name = "lblDetailsMessage"
        Me.lblDetailsMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblDetailsMessage.Size = New System.Drawing.Size(93, 19)
        Me.lblDetailsMessage.TabIndex = 0
        Me.lblDetailsMessage.Text = "Details message."
        '
        'txtDepositAmount
        '
        Me.txtDepositAmount.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDepositAmount.Cue = "$ 0"
        Me.txtDepositAmount.Location = New System.Drawing.Point(100, 51)
        Me.txtDepositAmount.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDepositAmount.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDepositAmount.Name = "txtDepositAmount"
        Me.txtDepositAmount.Size = New System.Drawing.Size(118, 20)
        Me.txtDepositAmount.TabIndex = 1
        Me.txtDepositAmount.Text = "$0"
        Me.txtDepositAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 158)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 26)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Credit Card " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Confirmation #"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Deposit Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Deposit Amount"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Batch #"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 132)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Check #"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Deposit #"
        '
        'txtCreditConf
        '
        Me.txtCreditConf.Location = New System.Drawing.Point(100, 155)
        Me.txtCreditConf.Name = "txtCreditConf"
        Me.txtCreditConf.Size = New System.Drawing.Size(118, 20)
        Me.txtCreditConf.TabIndex = 5
        '
        'txtBatchNumber
        '
        Me.txtBatchNumber.Location = New System.Drawing.Point(100, 103)
        Me.txtBatchNumber.Name = "txtBatchNumber"
        Me.txtBatchNumber.Size = New System.Drawing.Size(118, 20)
        Me.txtBatchNumber.TabIndex = 3
        '
        'txtDepositNumber
        '
        Me.txtDepositNumber.Location = New System.Drawing.Point(100, 77)
        Me.txtDepositNumber.Name = "txtDepositNumber"
        Me.txtDepositNumber.Size = New System.Drawing.Size(118, 20)
        Me.txtDepositNumber.TabIndex = 2
        '
        'txtCheckNumber
        '
        Me.txtCheckNumber.Location = New System.Drawing.Point(100, 129)
        Me.txtCheckNumber.Name = "txtCheckNumber"
        Me.txtCheckNumber.Size = New System.Drawing.Size(118, 20)
        Me.txtCheckNumber.TabIndex = 4
        '
        'btnDeleteDeposit
        '
        Me.btnDeleteDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteDeposit.AutoSize = True
        Me.btnDeleteDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteDeposit.Location = New System.Drawing.Point(490, 12)
        Me.btnDeleteDeposit.Name = "btnDeleteDeposit"
        Me.btnDeleteDeposit.Size = New System.Drawing.Size(106, 23)
        Me.btnDeleteDeposit.TabIndex = 7
        Me.btnDeleteDeposit.Text = "Delete this Deposit"
        Me.btnDeleteDeposit.UseVisualStyleBackColor = True
        '
        'lblDepositDisplay
        '
        Me.lblDepositDisplay.AutoSize = True
        Me.lblDepositDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositDisplay.Location = New System.Drawing.Point(42, 15)
        Me.lblDepositDisplay.Name = "lblDepositDisplay"
        Me.lblDepositDisplay.Size = New System.Drawing.Size(98, 17)
        Me.lblDepositDisplay.TabIndex = 5
        Me.lblDepositDisplay.Text = "New Deposit"
        '
        'dgvInvoicesPaid
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvInvoicesPaid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInvoicesPaid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInvoicesPaid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvInvoicesPaid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvoicesPaid.LinkifyColumnByName = "InvoiceID"
        Me.dgvInvoicesPaid.Location = New System.Drawing.Point(9, 128)
        Me.dgvInvoicesPaid.Name = "dgvInvoicesPaid"
        Me.dgvInvoicesPaid.ResultsCountLabel = Nothing
        Me.dgvInvoicesPaid.ResultsCountLabelFormat = "{0} found"
        Me.dgvInvoicesPaid.ShowEditingIcon = False
        Me.dgvInvoicesPaid.Size = New System.Drawing.Size(315, 227)
        Me.dgvInvoicesPaid.StandardTab = True
        Me.dgvInvoicesPaid.TabIndex = 4
        '
        'txtInvoiceToApply
        '
        Me.txtInvoiceToApply.Location = New System.Drawing.Point(9, 44)
        Me.txtInvoiceToApply.Name = "txtInvoiceToApply"
        Me.txtInvoiceToApply.Size = New System.Drawing.Size(94, 20)
        Me.txtInvoiceToApply.TabIndex = 0
        '
        'Label9
        '
        Me.lblAmountToApply.AutoSize = True
        Me.lblAmountToApply.Location = New System.Drawing.Point(109, 28)
        Me.lblAmountToApply.Name = "Label9"
        Me.lblAmountToApply.Size = New System.Drawing.Size(87, 13)
        Me.lblAmountToApply.TabIndex = 3
        Me.lblAmountToApply.Text = "Payment Amount"
        '
        'Label10
        '
        Me.lblInvoiceToApply.AutoSize = True
        Me.lblInvoiceToApply.Location = New System.Drawing.Point(9, 28)
        Me.lblInvoiceToApply.Name = "Label10"
        Me.lblInvoiceToApply.Size = New System.Drawing.Size(52, 13)
        Me.lblInvoiceToApply.TabIndex = 3
        Me.lblInvoiceToApply.Text = "Invoice #"
        '
        'txtAmountToApply
        '
        Me.txtAmountToApply.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountToApply.Cue = "$ 0"
        Me.txtAmountToApply.Location = New System.Drawing.Point(109, 44)
        Me.txtAmountToApply.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAmountToApply.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountToApply.Name = "txtAmountToApply"
        Me.txtAmountToApply.Size = New System.Drawing.Size(94, 20)
        Me.txtAmountToApply.TabIndex = 1
        Me.txtAmountToApply.Text = "$0"
        Me.txtAmountToApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnApplyToInvoice
        '
        Me.btnApplyToInvoice.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnApplyToInvoice.Enabled = False
        Me.btnApplyToInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApplyToInvoice.Location = New System.Drawing.Point(209, 40)
        Me.btnApplyToInvoice.Name = "btnApplyToInvoice"
        Me.btnApplyToInvoice.Size = New System.Drawing.Size(101, 27)
        Me.btnApplyToInvoice.TabIndex = 2
        Me.btnApplyToInvoice.Text = "Apply"
        Me.btnApplyToInvoice.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Label12.Size = New System.Drawing.Size(42, 19)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "AIRS #"
        '
        'txtAirsInvoiceSearch
        '
        Me.txtAirsInvoiceSearch.AirsNumber = Nothing
        Me.txtAirsInvoiceSearch.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.txtAirsInvoiceSearch.BackColor = System.Drawing.Color.Transparent
        Me.txtAirsInvoiceSearch.ErrorMessageLabel = Me.lblSearchFacilityDisplay
        Me.txtAirsInvoiceSearch.FacilityMustExist = True
        Me.txtAirsInvoiceSearch.Location = New System.Drawing.Point(60, 24)
        Me.txtAirsInvoiceSearch.Name = "txtAirsInvoiceSearch"
        Me.txtAirsInvoiceSearch.ReadOnly = False
        Me.txtAirsInvoiceSearch.Size = New System.Drawing.Size(74, 20)
        Me.txtAirsInvoiceSearch.TabIndex = 0
        Me.txtAirsInvoiceSearch.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'lblSearchFacilityDisplay
        '
        Me.lblSearchFacilityDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearchFacilityDisplay.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblSearchFacilityDisplay.Location = New System.Drawing.Point(140, 24)
        Me.lblSearchFacilityDisplay.MaximumSize = New System.Drawing.Size(185, 0)
        Me.lblSearchFacilityDisplay.Name = "lblSearchFacilityDisplay"
        Me.lblSearchFacilityDisplay.Padding = New System.Windows.Forms.Padding(3)
        Me.lblSearchFacilityDisplay.Size = New System.Drawing.Size(129, 0)
        Me.lblSearchFacilityDisplay.TabIndex = 0
        Me.lblSearchFacilityDisplay.Text = "Facility Display"
        '
        'grpInvoiceSearch
        '
        Me.grpInvoiceSearch.Controls.Add(Me.chkOnlyOpen)
        Me.grpInvoiceSearch.Controls.Add(Me.dgvSearchResults)
        Me.grpInvoiceSearch.Controls.Add(Me.Label12)
        Me.grpInvoiceSearch.Controls.Add(Me.lblSearchFacilityDisplay)
        Me.grpInvoiceSearch.Controls.Add(Me.txtAirsInvoiceSearch)
        Me.grpInvoiceSearch.Controls.Add(Me.btnInvoiceSearch)
        Me.grpInvoiceSearch.Location = New System.Drawing.Point(611, 51)
        Me.grpInvoiceSearch.Name = "grpInvoiceSearch"
        Me.grpInvoiceSearch.Size = New System.Drawing.Size(337, 536)
        Me.grpInvoiceSearch.TabIndex = 3
        Me.grpInvoiceSearch.TabStop = False
        Me.grpInvoiceSearch.Text = "Invoice Search"
        Me.grpInvoiceSearch.Visible = False
        '
        'chkOnlyOpen
        '
        Me.chkOnlyOpen.AutoSize = True
        Me.chkOnlyOpen.Checked = True
        Me.chkOnlyOpen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnlyOpen.Location = New System.Drawing.Point(15, 54)
        Me.chkOnlyOpen.Name = "chkOnlyOpen"
        Me.chkOnlyOpen.Size = New System.Drawing.Size(119, 17)
        Me.chkOnlyOpen.TabIndex = 1
        Me.chkOnlyOpen.Text = "Only Open Invoices"
        Me.chkOnlyOpen.UseVisualStyleBackColor = True
        '
        'dgvSearchResults
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvSearchResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSearchResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearchResults.LinkifyColumnByName = Nothing
        Me.dgvSearchResults.Location = New System.Drawing.Point(12, 79)
        Me.dgvSearchResults.Name = "dgvSearchResults"
        Me.dgvSearchResults.ResultsCountLabel = Nothing
        Me.dgvSearchResults.ResultsCountLabelFormat = "{0} found"
        Me.dgvSearchResults.ShowEditingIcon = False
        Me.dgvSearchResults.Size = New System.Drawing.Size(313, 441)
        Me.dgvSearchResults.StandardTab = True
        Me.dgvSearchResults.TabIndex = 3
        '
        'btnInvoiceSearch
        '
        Me.btnInvoiceSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnInvoiceSearch.Location = New System.Drawing.Point(143, 50)
        Me.btnInvoiceSearch.Name = "btnInvoiceSearch"
        Me.btnInvoiceSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnInvoiceSearch.TabIndex = 2
        Me.btnInvoiceSearch.Text = "Search"
        Me.btnInvoiceSearch.UseVisualStyleBackColor = True
        '
        'grpRefunds
        '
        Me.grpRefunds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpRefunds.Controls.Add(Me.dgvRefunds)
        Me.grpRefunds.Controls.Add(Me.lblNoRefunds)
        Me.grpRefunds.Location = New System.Drawing.Point(259, 429)
        Me.grpRefunds.Name = "grpRefunds"
        Me.grpRefunds.Size = New System.Drawing.Size(336, 173)
        Me.grpRefunds.TabIndex = 2
        Me.grpRefunds.TabStop = False
        Me.grpRefunds.Text = "Refunds Issued"
        '
        'dgvRefunds
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRefunds.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvRefunds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRefunds.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRefunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRefunds.LinkifyColumnByName = "RefundId"
        Me.dgvRefunds.Location = New System.Drawing.Point(9, 19)
        Me.dgvRefunds.Name = "dgvRefunds"
        Me.dgvRefunds.ResultsCountLabel = Nothing
        Me.dgvRefunds.ResultsCountLabelFormat = "{0} found"
        Me.dgvRefunds.ShowEditingIcon = False
        Me.dgvRefunds.Size = New System.Drawing.Size(312, 139)
        Me.dgvRefunds.StandardTab = True
        Me.dgvRefunds.TabIndex = 0
        '
        'lblNoRefunds
        '
        Me.lblNoRefunds.AutoSize = True
        Me.lblNoRefunds.Location = New System.Drawing.Point(9, 19)
        Me.lblNoRefunds.Name = "lblNoRefunds"
        Me.lblNoRefunds.Size = New System.Drawing.Size(36, 13)
        Me.lblNoRefunds.TabIndex = 3
        Me.lblNoRefunds.Text = "None."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label1.Location = New System.Drawing.Point(9, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 343
        Me.Label1.Text = "Total deposit"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label17.Location = New System.Drawing.Point(9, 66)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(54, 13)
        Me.Label17.TabIndex = 344
        Me.Label17.Text = "Refunded"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label11.Location = New System.Drawing.Point(9, 47)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 13)
        Me.Label11.TabIndex = 344
        Me.Label11.Text = "Applied to invoices"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 13)
        Me.Label13.TabIndex = 345
        Me.Label13.Text = "Balance"
        '
        'InvoiceLine
        '
        Me.InvoiceLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InvoiceLine.Location = New System.Drawing.Point(144, 82)
        Me.InvoiceLine.Name = "InvoiceLine"
        Me.InvoiceLine.Size = New System.Drawing.Size(71, 1)
        Me.InvoiceLine.TabIndex = 3
        '
        'txtDepositBalance
        '
        Me.txtDepositBalance.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDepositBalance.BackColor = System.Drawing.SystemColors.Control
        Me.txtDepositBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDepositBalance.Cue = "$0"
        Me.txtDepositBalance.Location = New System.Drawing.Point(144, 86)
        Me.txtDepositBalance.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDepositBalance.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtDepositBalance.Name = "txtDepositBalance"
        Me.txtDepositBalance.Size = New System.Drawing.Size(71, 13)
        Me.txtDepositBalance.TabIndex = 3
        Me.txtDepositBalance.Text = "$0"
        Me.txtDepositBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalDeposit
        '
        Me.txtTotalDeposit.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtTotalDeposit.BackColor = System.Drawing.SystemColors.Control
        Me.txtTotalDeposit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalDeposit.Cue = "$0"
        Me.txtTotalDeposit.Location = New System.Drawing.Point(144, 28)
        Me.txtTotalDeposit.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalDeposit.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalDeposit.Name = "txtTotalDeposit"
        Me.txtTotalDeposit.Size = New System.Drawing.Size(71, 13)
        Me.txtTotalDeposit.TabIndex = 0
        Me.txtTotalDeposit.Text = "$0"
        Me.txtTotalDeposit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmountRefunded
        '
        Me.txtAmountRefunded.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountRefunded.BackColor = System.Drawing.SystemColors.Control
        Me.txtAmountRefunded.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmountRefunded.Cue = "$0"
        Me.txtAmountRefunded.Location = New System.Drawing.Point(144, 66)
        Me.txtAmountRefunded.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAmountRefunded.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtAmountRefunded.Name = "txtAmountRefunded"
        Me.txtAmountRefunded.Size = New System.Drawing.Size(71, 13)
        Me.txtAmountRefunded.TabIndex = 2
        Me.txtAmountRefunded.Text = "$0"
        Me.txtAmountRefunded.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmountAppliedToInvoices
        '
        Me.txtAmountAppliedToInvoices.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountAppliedToInvoices.BackColor = System.Drawing.SystemColors.Control
        Me.txtAmountAppliedToInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmountAppliedToInvoices.Cue = "$0"
        Me.txtAmountAppliedToInvoices.Location = New System.Drawing.Point(144, 47)
        Me.txtAmountAppliedToInvoices.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAmountAppliedToInvoices.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtAmountAppliedToInvoices.Name = "txtAmountAppliedToInvoices"
        Me.txtAmountAppliedToInvoices.Size = New System.Drawing.Size(71, 13)
        Me.txtAmountAppliedToInvoices.TabIndex = 1
        Me.txtAmountAppliedToInvoices.Text = "$0"
        Me.txtAmountAppliedToInvoices.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grpApplyToInvoice
        '
        Me.grpApplyToInvoice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpApplyToInvoice.Controls.Add(Me.dgvInvoicesPaid)
        Me.grpApplyToInvoice.Controls.Add(Me.lblInvoiceToApply)
        Me.grpApplyToInvoice.Controls.Add(Me.lblAmountToApply)
        Me.grpApplyToInvoice.Controls.Add(Me.lblApplyToInvoiceMessage)
        Me.grpApplyToInvoice.Controls.Add(Me.txtAmountToApply)
        Me.grpApplyToInvoice.Controls.Add(Me.txtInvoiceToApply)
        Me.grpApplyToInvoice.Controls.Add(Me.lblInvoicesPaid)
        Me.grpApplyToInvoice.Controls.Add(Me.btnApplyToInvoice)
        Me.grpApplyToInvoice.Location = New System.Drawing.Point(259, 51)
        Me.grpApplyToInvoice.Name = "grpApplyToInvoice"
        Me.grpApplyToInvoice.Size = New System.Drawing.Size(336, 372)
        Me.grpApplyToInvoice.TabIndex = 1
        Me.grpApplyToInvoice.TabStop = False
        Me.grpApplyToInvoice.Text = "Apply To Invoice"
        '
        'lblApplyToInvoiceMessage
        '
        Me.lblApplyToInvoiceMessage.AutoSize = True
        Me.lblApplyToInvoiceMessage.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblApplyToInvoiceMessage.Location = New System.Drawing.Point(9, 75)
        Me.lblApplyToInvoiceMessage.MaximumSize = New System.Drawing.Size(301, 0)
        Me.lblApplyToInvoiceMessage.Name = "lblApplyToInvoiceMessage"
        Me.lblApplyToInvoiceMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblApplyToInvoiceMessage.Size = New System.Drawing.Size(96, 19)
        Me.lblApplyToInvoiceMessage.TabIndex = 3
        Me.lblApplyToInvoiceMessage.Text = "Invoice message."
        '
        'lblInvoicesPaid
        '
        Me.lblInvoicesPaid.AutoSize = True
        Me.lblInvoicesPaid.Location = New System.Drawing.Point(9, 112)
        Me.lblInvoicesPaid.Name = "lblInvoicesPaid"
        Me.lblInvoicesPaid.Size = New System.Drawing.Size(71, 13)
        Me.lblInvoicesPaid.TabIndex = 6
        Me.lblInvoicesPaid.Text = "Invoices Paid"
        '
        'grpSummary
        '
        Me.grpSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpSummary.Controls.Add(Me.Label1)
        Me.grpSummary.Controls.Add(Me.txtAmountAppliedToInvoices)
        Me.grpSummary.Controls.Add(Me.txtAmountRefunded)
        Me.grpSummary.Controls.Add(Me.Label17)
        Me.grpSummary.Controls.Add(Me.Label11)
        Me.grpSummary.Controls.Add(Me.txtTotalDeposit)
        Me.grpSummary.Controls.Add(Me.Label13)
        Me.grpSummary.Controls.Add(Me.txtDepositBalance)
        Me.grpSummary.Controls.Add(Me.InvoiceLine)
        Me.grpSummary.Location = New System.Drawing.Point(12, 429)
        Me.grpSummary.Name = "grpSummary"
        Me.grpSummary.Size = New System.Drawing.Size(231, 173)
        Me.grpSummary.TabIndex = 3
        Me.grpSummary.TabStop = False
        Me.grpSummary.Text = "Summary"
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefresh.Location = New System.Drawing.Point(12, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 24)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblDeleteDepositMessage
        '
        Me.lblDeleteDepositMessage.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblDeleteDepositMessage.Location = New System.Drawing.Point(320, 8)
        Me.lblDeleteDepositMessage.Name = "lblDeleteDepositMessage"
        Me.lblDeleteDepositMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblDeleteDepositMessage.Size = New System.Drawing.Size(164, 30)
        Me.lblDeleteDepositMessage.TabIndex = 6
        Me.lblDeleteDepositMessage.Text = "Delete deposit message."
        Me.lblDeleteDepositMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FinDepositView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(608, 615)
        Me.Controls.Add(Me.lblDeleteDepositMessage)
        Me.Controls.Add(Me.grpSummary)
        Me.Controls.Add(Me.grpApplyToInvoice)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.grpRefunds)
        Me.Controls.Add(Me.grpInvoiceSearch)
        Me.Controls.Add(Me.lblDepositDisplay)
        Me.Controls.Add(Me.grpDepositDetails)
        Me.Controls.Add(Me.btnDeleteDeposit)
        Me.MinimumSize = New System.Drawing.Size(624, 600)
        Me.Name = "FinDepositView"
        Me.Text = "New Application Fee Deposit"
        Me.grpDepositDetails.ResumeLayout(False)
        Me.grpDepositDetails.PerformLayout()
        CType(Me.dgvInvoicesPaid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInvoiceSearch.ResumeLayout(False)
        Me.grpInvoiceSearch.PerformLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRefunds.ResumeLayout(False)
        Me.grpRefunds.PerformLayout()
        CType(Me.dgvRefunds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpApplyToInvoice.ResumeLayout(False)
        Me.grpApplyToInvoice.PerformLayout()
        Me.grpSummary.ResumeLayout(False)
        Me.grpSummary.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSaveNewDeposit As Button
    Friend WithEvents grpDepositDetails As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCreditConf As TextBox
    Friend WithEvents txtBatchNumber As TextBox
    Friend WithEvents txtCheckNumber As TextBox
    Friend WithEvents txtDepositNumber As TextBox
    Friend WithEvents lblDepositDisplay As Label
    Friend WithEvents txtDepositAmount As CurrencyTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dgvInvoicesPaid As IaipDataGridView
    Friend WithEvents btnDeleteDeposit As Button
    Friend WithEvents btnUpdateDepositDetails As Button
    Friend WithEvents txtInvoiceToApply As TextBox
    Friend WithEvents lblAmountToApply As Label
    Friend WithEvents lblInvoiceToApply As Label
    Friend WithEvents txtAmountToApply As CurrencyTextBox
    Friend WithEvents btnApplyToInvoice As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents txtAirsInvoiceSearch As AirNumberEntryForm
    Friend WithEvents grpInvoiceSearch As GroupBox
    Friend WithEvents chkOnlyOpen As CheckBox
    Friend WithEvents dgvSearchResults As IaipDataGridView
    Friend WithEvents btnInvoiceSearch As Button
    Friend WithEvents grpRefunds As GroupBox
    Friend WithEvents txtDepositComments As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents lblSearchFacilityDisplay As Label
    Friend WithEvents grpApplyToInvoice As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents InvoiceLine As Label
    Friend WithEvents txtDepositBalance As CurrencyTextBox
    Friend WithEvents txtTotalDeposit As CurrencyTextBox
    Friend WithEvents txtAmountAppliedToInvoices As CurrencyTextBox
    Friend WithEvents lblApplyToInvoiceMessage As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents dgvRefunds As IaipDataGridView
    Friend WithEvents txtAmountRefunded As CurrencyTextBox
    Friend WithEvents lblDetailsMessage As Label
    Friend WithEvents grpSummary As GroupBox
    Friend WithEvents lblNoRefunds As Label
    Friend WithEvents lblInvoicesPaid As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblDeleteDepositMessage As Label
End Class
