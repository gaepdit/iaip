<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPDepositsAmendments
    Inherits BaseForm

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlDepositsEntry = New System.Windows.Forms.Panel()
        Me.txtCreditCardNo = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtInvoiceForDeposit = New System.Windows.Forms.TextBox()
        Me.btnClearForm = New System.Windows.Forms.Button()
        Me.dtpBatchDepositDateField = New System.Windows.Forms.DateTimePicker()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtCheckNumberField = New System.Windows.Forms.TextBox()
        Me.txtBatchNoField = New System.Windows.Forms.TextBox()
        Me.txtDepositNumberField = New System.Windows.Forms.TextBox()
        Me.lblBatchNo = New System.Windows.Forms.Label()
        Me.lblCheckNo = New System.Windows.Forms.Label()
        Me.lblDepositNo = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.lblInvoiceNumber = New System.Windows.Forms.Label()
        Me.txtTransactionID = New System.Windows.Forms.TextBox()
        Me.txtDepositComments = New System.Windows.Forms.TextBox()
        Me.txtDepositAmount = New System.Windows.Forms.TextBox()
        Me.lblAIRSNumber = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblFeeYear = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblFacilityName = New System.Windows.Forms.Label()
        Me.btnDeleteCheckDeposit = New System.Windows.Forms.Button()
        Me.btnAddNewCheckDeposit = New System.Windows.Forms.Button()
        Me.btnUpdateExistingDeposit = New System.Windows.Forms.Button()
        Me.txtDepositCount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.dgvInvoices = New System.Windows.Forms.DataGridView()
        Me.pnlInvoiceSearch = New System.Windows.Forms.Panel()
        Me.cbYear = New System.Windows.Forms.ComboBox()
        Me.btnViewInvoices = New System.Windows.Forms.Button()
        Me.btnSearchForInvoice = New System.Windows.Forms.Button()
        Me.btnSearchForCheck = New System.Windows.Forms.Button()
        Me.txtCountInvoices = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSearchInvoice = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox()
        Me.txtCheckNumber = New System.Windows.Forms.TextBox()
        Me.pnlDepositSearchs = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDepositReportEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDepositReportStartDate = New System.Windows.Forms.DateTimePicker()
        Me.btnSearchDeposits = New System.Windows.Forms.Button()
        Me.dgvDeposits = New System.Windows.Forms.DataGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cbYear2 = New System.Windows.Forms.ComboBox()
        Me.pnlDepositsEntry.SuspendLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInvoiceSearch.SuspendLayout()
        Me.pnlDepositSearchs.SuspendLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDepositsEntry
        '
        Me.pnlDepositsEntry.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pnlDepositsEntry.Controls.Add(Me.cbYear2)
        Me.pnlDepositsEntry.Controls.Add(Me.txtCreditCardNo)
        Me.pnlDepositsEntry.Controls.Add(Me.Label56)
        Me.pnlDepositsEntry.Controls.Add(Me.txtInvoiceForDeposit)
        Me.pnlDepositsEntry.Controls.Add(Me.btnClearForm)
        Me.pnlDepositsEntry.Controls.Add(Me.dtpBatchDepositDateField)
        Me.pnlDepositsEntry.Controls.Add(Me.Label37)
        Me.pnlDepositsEntry.Controls.Add(Me.txtCheckNumberField)
        Me.pnlDepositsEntry.Controls.Add(Me.txtBatchNoField)
        Me.pnlDepositsEntry.Controls.Add(Me.txtDepositNumberField)
        Me.pnlDepositsEntry.Controls.Add(Me.lblBatchNo)
        Me.pnlDepositsEntry.Controls.Add(Me.lblCheckNo)
        Me.pnlDepositsEntry.Controls.Add(Me.lblDepositNo)
        Me.pnlDepositsEntry.Controls.Add(Me.Label36)
        Me.pnlDepositsEntry.Controls.Add(Me.lblInvoiceNumber)
        Me.pnlDepositsEntry.Controls.Add(Me.txtTransactionID)
        Me.pnlDepositsEntry.Controls.Add(Me.txtDepositComments)
        Me.pnlDepositsEntry.Controls.Add(Me.txtDepositAmount)
        Me.pnlDepositsEntry.Controls.Add(Me.lblAIRSNumber)
        Me.pnlDepositsEntry.Controls.Add(Me.Label38)
        Me.pnlDepositsEntry.Controls.Add(Me.lblFeeYear)
        Me.pnlDepositsEntry.Controls.Add(Me.Label39)
        Me.pnlDepositsEntry.Controls.Add(Me.lblFacilityName)
        Me.pnlDepositsEntry.Controls.Add(Me.btnDeleteCheckDeposit)
        Me.pnlDepositsEntry.Controls.Add(Me.btnAddNewCheckDeposit)
        Me.pnlDepositsEntry.Controls.Add(Me.btnUpdateExistingDeposit)
        Me.pnlDepositsEntry.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDepositsEntry.Location = New System.Drawing.Point(0, 276)
        Me.pnlDepositsEntry.Name = "pnlDepositsEntry"
        Me.pnlDepositsEntry.Size = New System.Drawing.Size(811, 208)
        Me.pnlDepositsEntry.TabIndex = 0
        '
        'txtCreditCardNo
        '
        Me.txtCreditCardNo.Location = New System.Drawing.Point(487, 92)
        Me.txtCreditCardNo.Name = "txtCreditCardNo"
        Me.txtCreditCardNo.Size = New System.Drawing.Size(100, 20)
        Me.txtCreditCardNo.TabIndex = 6
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(351, 95)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(130, 13)
        Me.Label56.TabIndex = 61
        Me.Label56.Text = "Credit Card Confirmation #"
        '
        'txtInvoiceForDeposit
        '
        Me.txtInvoiceForDeposit.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtInvoiceForDeposit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInvoiceForDeposit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceForDeposit.Location = New System.Drawing.Point(79, 36)
        Me.txtInvoiceForDeposit.Name = "txtInvoiceForDeposit"
        Me.txtInvoiceForDeposit.ReadOnly = True
        Me.txtInvoiceForDeposit.Size = New System.Drawing.Size(99, 15)
        Me.txtInvoiceForDeposit.TabIndex = 7
        Me.txtInvoiceForDeposit.TabStop = False
        '
        'btnClearForm
        '
        Me.btnClearForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearForm.Location = New System.Drawing.Point(709, 11)
        Me.btnClearForm.Name = "btnClearForm"
        Me.btnClearForm.Size = New System.Drawing.Size(90, 31)
        Me.btnClearForm.TabIndex = 3
        Me.btnClearForm.Text = "Clear All"
        Me.btnClearForm.UseVisualStyleBackColor = True
        '
        'dtpBatchDepositDateField
        '
        Me.dtpBatchDepositDateField.CustomFormat = "dd-MMM-yyyy"
        Me.dtpBatchDepositDateField.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBatchDepositDateField.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBatchDepositDateField.Location = New System.Drawing.Point(586, 60)
        Me.dtpBatchDepositDateField.Name = "dtpBatchDepositDateField"
        Me.dtpBatchDepositDateField.Size = New System.Drawing.Size(114, 22)
        Me.dtpBatchDepositDateField.TabIndex = 3
        Me.dtpBatchDepositDateField.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(480, 65)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(100, 13)
        Me.Label37.TabIndex = 58
        Me.Label37.Text = "Batch Deposit Date"
        '
        'txtCheckNumberField
        '
        Me.txtCheckNumberField.Location = New System.Drawing.Point(239, 92)
        Me.txtCheckNumberField.Name = "txtCheckNumberField"
        Me.txtCheckNumberField.Size = New System.Drawing.Size(100, 20)
        Me.txtCheckNumberField.TabIndex = 5
        '
        'txtBatchNoField
        '
        Me.txtBatchNoField.Location = New System.Drawing.Point(368, 62)
        Me.txtBatchNoField.Name = "txtBatchNoField"
        Me.txtBatchNoField.Size = New System.Drawing.Size(100, 20)
        Me.txtBatchNoField.TabIndex = 2
        '
        'txtDepositNumberField
        '
        Me.txtDepositNumberField.Location = New System.Drawing.Point(205, 62)
        Me.txtDepositNumberField.MaxLength = 20
        Me.txtDepositNumberField.Name = "txtDepositNumberField"
        Me.txtDepositNumberField.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositNumberField.TabIndex = 1
        '
        'lblBatchNo
        '
        Me.lblBatchNo.AutoSize = True
        Me.lblBatchNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchNo.Location = New System.Drawing.Point(317, 65)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(45, 13)
        Me.lblBatchNo.TabIndex = 50
        Me.lblBatchNo.Text = "Batch #"
        '
        'lblCheckNo
        '
        Me.lblCheckNo.AutoSize = True
        Me.lblCheckNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckNo.Location = New System.Drawing.Point(185, 95)
        Me.lblCheckNo.Name = "lblCheckNo"
        Me.lblCheckNo.Size = New System.Drawing.Size(48, 13)
        Me.lblCheckNo.TabIndex = 49
        Me.lblCheckNo.Text = "Check #"
        '
        'lblDepositNo
        '
        Me.lblDepositNo.AutoSize = True
        Me.lblDepositNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositNo.Location = New System.Drawing.Point(146, 65)
        Me.lblDepositNo.Name = "lblDepositNo"
        Me.lblDepositNo.Size = New System.Drawing.Size(53, 13)
        Me.lblDepositNo.TabIndex = 48
        Me.lblDepositNo.Text = "Deposit #"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(184, 36)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(95, 16)
        Me.Label36.TabIndex = 47
        Me.Label36.Text = "Transaction ID"
        '
        'lblInvoiceNumber
        '
        Me.lblInvoiceNumber.AutoSize = True
        Me.lblInvoiceNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNumber.Location = New System.Drawing.Point(12, 36)
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        Me.lblInvoiceNumber.Size = New System.Drawing.Size(61, 16)
        Me.lblInvoiceNumber.TabIndex = 46
        Me.lblInvoiceNumber.Text = "Invoice #"
        '
        'txtTransactionID
        '
        Me.txtTransactionID.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtTransactionID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTransactionID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransactionID.Location = New System.Drawing.Point(285, 36)
        Me.txtTransactionID.Name = "txtTransactionID"
        Me.txtTransactionID.ReadOnly = True
        Me.txtTransactionID.Size = New System.Drawing.Size(100, 15)
        Me.txtTransactionID.TabIndex = 8
        Me.txtTransactionID.TabStop = False
        '
        'txtDepositComments
        '
        Me.txtDepositComments.AcceptsReturn = True
        Me.txtDepositComments.Location = New System.Drawing.Point(74, 120)
        Me.txtDepositComments.Multiline = True
        Me.txtDepositComments.Name = "txtDepositComments"
        Me.txtDepositComments.Size = New System.Drawing.Size(322, 39)
        Me.txtDepositComments.TabIndex = 7
        '
        'txtDepositAmount
        '
        Me.txtDepositAmount.Location = New System.Drawing.Point(74, 92)
        Me.txtDepositAmount.Name = "txtDepositAmount"
        Me.txtDepositAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositAmount.TabIndex = 4
        '
        'lblAIRSNumber
        '
        Me.lblAIRSNumber.AutoSize = True
        Me.lblAIRSNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAIRSNumber.Location = New System.Drawing.Point(12, 11)
        Me.lblAIRSNumber.Name = "lblAIRSNumber"
        Me.lblAIRSNumber.Size = New System.Drawing.Size(49, 16)
        Me.lblAIRSNumber.TabIndex = 42
        Me.lblAIRSNumber.Text = "AIRS #"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(12, 95)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(43, 13)
        Me.Label38.TabIndex = 40
        Me.Label38.Text = "Amount"
        '
        'lblFeeYear
        '
        Me.lblFeeYear.AutoSize = True
        Me.lblFeeYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFeeYear.Location = New System.Drawing.Point(12, 65)
        Me.lblFeeYear.Name = "lblFeeYear"
        Me.lblFeeYear.Size = New System.Drawing.Size(50, 13)
        Me.lblFeeYear.TabIndex = 43
        Me.lblFeeYear.Text = "Fee Year"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(12, 120)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(56, 26)
        Me.Label39.TabIndex = 44
        Me.Label39.Text = "EPD " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Comments"
        '
        'lblFacilityName
        '
        Me.lblFacilityName.AutoSize = True
        Me.lblFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityName.Location = New System.Drawing.Point(131, 11)
        Me.lblFacilityName.Name = "lblFacilityName"
        Me.lblFacilityName.Size = New System.Drawing.Size(90, 16)
        Me.lblFacilityName.TabIndex = 41
        Me.lblFacilityName.Text = "Facility Name"
        '
        'btnDeleteCheckDeposit
        '
        Me.btnDeleteCheckDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteCheckDeposit.Location = New System.Drawing.Point(403, 165)
        Me.btnDeleteCheckDeposit.Name = "btnDeleteCheckDeposit"
        Me.btnDeleteCheckDeposit.Size = New System.Drawing.Size(135, 31)
        Me.btnDeleteCheckDeposit.TabIndex = 10
        Me.btnDeleteCheckDeposit.Text = "Delete Check Deposit"
        Me.btnDeleteCheckDeposit.UseVisualStyleBackColor = True
        '
        'btnAddNewCheckDeposit
        '
        Me.btnAddNewCheckDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewCheckDeposit.Location = New System.Drawing.Point(74, 165)
        Me.btnAddNewCheckDeposit.Name = "btnAddNewCheckDeposit"
        Me.btnAddNewCheckDeposit.Size = New System.Drawing.Size(134, 31)
        Me.btnAddNewCheckDeposit.TabIndex = 8
        Me.btnAddNewCheckDeposit.Text = "Add New Check Deposit"
        Me.btnAddNewCheckDeposit.UseVisualStyleBackColor = True
        '
        'btnUpdateExistingDeposit
        '
        Me.btnUpdateExistingDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateExistingDeposit.Location = New System.Drawing.Point(224, 165)
        Me.btnUpdateExistingDeposit.Name = "btnUpdateExistingDeposit"
        Me.btnUpdateExistingDeposit.Size = New System.Drawing.Size(164, 31)
        Me.btnUpdateExistingDeposit.TabIndex = 9
        Me.btnUpdateExistingDeposit.Text = "Update Existing Check Deposit"
        Me.btnUpdateExistingDeposit.UseVisualStyleBackColor = True
        '
        'txtDepositCount
        '
        Me.txtDepositCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDepositCount.AutoSize = True
        Me.txtDepositCount.Location = New System.Drawing.Point(750, 33)
        Me.txtDepositCount.Name = "txtDepositCount"
        Me.txtDepositCount.Size = New System.Drawing.Size(13, 13)
        Me.txtDepositCount.TabIndex = 64
        Me.txtDepositCount.Text = "0"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Deposits Search"
        '
        'Label47
        '
        Me.Label47.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(706, 33)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(38, 13)
        Me.Label47.TabIndex = 52
        Me.Label47.Text = "Count:"
        '
        'dgvInvoices
        '
        Me.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInvoices.Location = New System.Drawing.Point(0, 71)
        Me.dgvInvoices.Name = "dgvInvoices"
        Me.dgvInvoices.ReadOnly = True
        Me.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInvoices.Size = New System.Drawing.Size(811, 205)
        Me.dgvInvoices.TabIndex = 1
        '
        'pnlInvoiceSearch
        '
        Me.pnlInvoiceSearch.Controls.Add(Me.cbYear)
        Me.pnlInvoiceSearch.Controls.Add(Me.btnViewInvoices)
        Me.pnlInvoiceSearch.Controls.Add(Me.btnSearchForInvoice)
        Me.pnlInvoiceSearch.Controls.Add(Me.btnSearchForCheck)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtCountInvoices)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label1)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtSearchInvoice)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label55)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label48)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label41)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label42)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label43)
        Me.pnlInvoiceSearch.Controls.Add(Me.mtbAIRSNumber)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtCheckNumber)
        Me.pnlInvoiceSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInvoiceSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlInvoiceSearch.Name = "pnlInvoiceSearch"
        Me.pnlInvoiceSearch.Size = New System.Drawing.Size(811, 71)
        Me.pnlInvoiceSearch.TabIndex = 0
        '
        'cbYear
        '
        Me.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbYear.FormattingEnabled = True
        Me.cbYear.Location = New System.Drawing.Point(278, 12)
        Me.cbYear.Name = "cbYear"
        Me.cbYear.Size = New System.Drawing.Size(65, 21)
        Me.cbYear.TabIndex = 1
        '
        'btnViewInvoices
        '
        Me.btnViewInvoices.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnViewInvoices.Location = New System.Drawing.Point(354, 10)
        Me.btnViewInvoices.Name = "btnViewInvoices"
        Me.btnViewInvoices.Size = New System.Drawing.Size(142, 23)
        Me.btnViewInvoices.TabIndex = 2
        Me.btnViewInvoices.Text = "View Invoices for Facility"
        Me.btnViewInvoices.UseVisualStyleBackColor = True
        '
        'btnSearchForInvoice
        '
        Me.btnSearchForInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearchForInvoice.Location = New System.Drawing.Point(484, 39)
        Me.btnSearchForInvoice.Name = "btnSearchForInvoice"
        Me.btnSearchForInvoice.Size = New System.Drawing.Size(142, 23)
        Me.btnSearchForInvoice.TabIndex = 7
        Me.btnSearchForInvoice.Text = "Search For Invoice #"
        Me.btnSearchForInvoice.UseVisualStyleBackColor = True
        '
        'btnSearchForCheck
        '
        Me.btnSearchForCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearchForCheck.Location = New System.Drawing.Point(172, 39)
        Me.btnSearchForCheck.Name = "btnSearchForCheck"
        Me.btnSearchForCheck.Size = New System.Drawing.Size(142, 23)
        Me.btnSearchForCheck.TabIndex = 5
        Me.btnSearchForCheck.Text = "Search for Check #"
        Me.btnSearchForCheck.UseVisualStyleBackColor = True
        '
        'txtCountInvoices
        '
        Me.txtCountInvoices.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCountInvoices.AutoSize = True
        Me.txtCountInvoices.Location = New System.Drawing.Point(750, 55)
        Me.txtCountInvoices.Name = "txtCountInvoices"
        Me.txtCountInvoices.Size = New System.Drawing.Size(13, 13)
        Me.txtCountInvoices.TabIndex = 61
        Me.txtCountInvoices.Text = "0"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Invoice Search"
        '
        'txtSearchInvoice
        '
        Me.txtSearchInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearchInvoice.Location = New System.Drawing.Point(378, 41)
        Me.txtSearchInvoice.Name = "txtSearchInvoice"
        Me.txtSearchInvoice.Size = New System.Drawing.Size(100, 20)
        Me.txtSearchInvoice.TabIndex = 6
        '
        'Label55
        '
        Me.Label55.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(320, 44)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(52, 13)
        Me.Label55.TabIndex = 57
        Me.Label55.Text = "Invoice #"
        '
        'Label48
        '
        Me.Label48.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(706, 55)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(38, 13)
        Me.Label48.TabIndex = 54
        Me.Label48.Text = "Count:"
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(243, 15)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(29, 13)
        Me.Label41.TabIndex = 7
        Me.Label41.Text = "Year"
        '
        'Label42
        '
        Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(12, 44)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(48, 13)
        Me.Label42.TabIndex = 4
        Me.Label42.Text = "Check #"
        '
        'Label43
        '
        Me.Label43.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(124, 15)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(42, 13)
        Me.Label43.TabIndex = 0
        Me.Label43.Text = "AIRS #"
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(172, 12)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(62, 20)
        Me.mtbAIRSNumber.TabIndex = 0
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtCheckNumber
        '
        Me.txtCheckNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCheckNumber.Location = New System.Drawing.Point(66, 41)
        Me.txtCheckNumber.Name = "txtCheckNumber"
        Me.txtCheckNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtCheckNumber.TabIndex = 4
        '
        'pnlDepositSearchs
        '
        Me.pnlDepositSearchs.AutoScroll = True
        Me.pnlDepositSearchs.Controls.Add(Me.Label4)
        Me.pnlDepositSearchs.Controls.Add(Me.txtDepositCount)
        Me.pnlDepositSearchs.Controls.Add(Me.Label2)
        Me.pnlDepositSearchs.Controls.Add(Me.Label3)
        Me.pnlDepositSearchs.Controls.Add(Me.dtpDepositReportEndDate)
        Me.pnlDepositSearchs.Controls.Add(Me.dtpDepositReportStartDate)
        Me.pnlDepositSearchs.Controls.Add(Me.btnSearchDeposits)
        Me.pnlDepositSearchs.Controls.Add(Me.Label47)
        Me.pnlDepositSearchs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDepositSearchs.Location = New System.Drawing.Point(0, 0)
        Me.pnlDepositSearchs.Name = "pnlDepositSearchs"
        Me.pnlDepositSearchs.Size = New System.Drawing.Size(811, 49)
        Me.pnlDepositSearchs.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(291, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 412
        Me.Label2.Text = "End Date"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(124, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 411
        Me.Label3.Text = "Start Date"
        '
        'dtpDepositReportEndDate
        '
        Me.dtpDepositReportEndDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpDepositReportEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositReportEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositReportEndDate.Location = New System.Drawing.Point(349, 16)
        Me.dtpDepositReportEndDate.Name = "dtpDepositReportEndDate"
        Me.dtpDepositReportEndDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDepositReportEndDate.TabIndex = 1
        '
        'dtpDepositReportStartDate
        '
        Me.dtpDepositReportStartDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpDepositReportStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositReportStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositReportStartDate.Location = New System.Drawing.Point(185, 16)
        Me.dtpDepositReportStartDate.Name = "dtpDepositReportStartDate"
        Me.dtpDepositReportStartDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDepositReportStartDate.TabIndex = 0
        '
        'btnSearchDeposits
        '
        Me.btnSearchDeposits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearchDeposits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchDeposits.Location = New System.Drawing.Point(455, 15)
        Me.btnSearchDeposits.Name = "btnSearchDeposits"
        Me.btnSearchDeposits.Size = New System.Drawing.Size(142, 23)
        Me.btnSearchDeposits.TabIndex = 2
        Me.btnSearchDeposits.Text = "Search Deposits"
        Me.btnSearchDeposits.UseVisualStyleBackColor = True
        '
        'dgvDeposits
        '
        Me.dgvDeposits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDeposits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDeposits.Location = New System.Drawing.Point(0, 49)
        Me.dgvDeposits.Name = "dgvDeposits"
        Me.dgvDeposits.ReadOnly = True
        Me.dgvDeposits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDeposits.Size = New System.Drawing.Size(811, 153)
        Me.dgvDeposits.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvInvoices)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlInvoiceSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlDepositsEntry)
        Me.SplitContainer1.Panel1MinSize = 335
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvDeposits)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlDepositSearchs)
        Me.SplitContainer1.Panel2MinSize = 120
        Me.SplitContainer1.Size = New System.Drawing.Size(811, 690)
        Me.SplitContainer1.SplitterDistance = 484
        Me.SplitContainer1.TabIndex = 35
        '
        'cbYear2
        '
        Me.cbYear2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbYear2.FormattingEnabled = True
        Me.cbYear2.Location = New System.Drawing.Point(74, 62)
        Me.cbYear2.Name = "cbYear2"
        Me.cbYear2.Size = New System.Drawing.Size(65, 21)
        Me.cbYear2.TabIndex = 0
        '
        'PASPDepositsAmendments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 690)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(818, 488)
        Me.Name = "PASPDepositsAmendments"
        Me.Text = "Fee Deposits"
        Me.pnlDepositsEntry.ResumeLayout(False)
        Me.pnlDepositsEntry.PerformLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInvoiceSearch.ResumeLayout(False)
        Me.pnlInvoiceSearch.PerformLayout()
        Me.pnlDepositSearchs.ResumeLayout(False)
        Me.pnlDepositSearchs.PerformLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents pnlInvoiceSearch As System.Windows.Forms.Panel
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCheckNumber As System.Windows.Forms.TextBox
    Friend WithEvents pnlDepositSearchs As System.Windows.Forms.Panel
    Friend WithEvents btnClearForm As System.Windows.Forms.Button
    Friend WithEvents dgvDeposits As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDepositsEntry As System.Windows.Forms.Panel
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lblBatchNo As System.Windows.Forms.Label
    Friend WithEvents lblCheckNo As System.Windows.Forms.Label
    Friend WithEvents lblDepositNo As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblInvoiceNumber As System.Windows.Forms.Label
    Friend WithEvents txtTransactionID As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositComments As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblAIRSNumber As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblFeeYear As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityName As System.Windows.Forms.Label
    Friend WithEvents btnDeleteCheckDeposit As System.Windows.Forms.Button
    Friend WithEvents btnAddNewCheckDeposit As System.Windows.Forms.Button
    Friend WithEvents btnUpdateExistingDeposit As System.Windows.Forms.Button
    Friend WithEvents txtDepositNumberField As System.Windows.Forms.TextBox
    Friend WithEvents txtBatchNoField As System.Windows.Forms.TextBox
    Friend WithEvents txtCheckNumberField As System.Windows.Forms.TextBox
    Friend WithEvents dtpBatchDepositDateField As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceForDeposit As System.Windows.Forms.TextBox
    Friend WithEvents txtCreditCardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtSearchInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDepositReportEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDepositReportStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtDepositCount As Label
    Friend WithEvents txtCountInvoices As Label
    Friend WithEvents btnSearchForCheck As Button
    Friend WithEvents btnSearchForInvoice As Button
    Friend WithEvents btnViewInvoices As Button
    Friend WithEvents btnSearchDeposits As Button
    Friend WithEvents cbYear As ComboBox
    Friend WithEvents cbYear2 As ComboBox
End Class
