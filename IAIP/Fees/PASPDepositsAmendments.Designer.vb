<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPDepositsAmendments
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.txtCreditCardNo = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtInvoiceForDeposit = New System.Windows.Forms.TextBox()
        Me.mtbFeeYear2 = New System.Windows.Forms.MaskedTextBox()
        Me.DTPBatchDepositDateField = New System.Windows.Forms.DateTimePicker()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtCheckNumberField = New System.Windows.Forms.TextBox()
        Me.txtBatchNoField = New System.Windows.Forms.TextBox()
        Me.txtDepositNumberField = New System.Windows.Forms.TextBox()
        Me.btnDeleteInventoryRecords = New System.Windows.Forms.Button()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtDepositCount = New System.Windows.Forms.TextBox()
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
        Me.dgvInvoices = New System.Windows.Forms.DataGridView()
        Me.pnlInvoiceSearch = New System.Windows.Forms.Panel()
        Me.llbSearchForInvoice = New System.Windows.Forms.LinkLabel()
        Me.txtSearchInvoice = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.llbSearchForCheck = New System.Windows.Forms.LinkLabel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.txtCountInvoices = New System.Windows.Forms.TextBox()
        Me.btnClearEntryInformation = New System.Windows.Forms.Button()
        Me.mtbFeeYear = New System.Windows.Forms.MaskedTextBox()
        Me.lblViewInvoices = New System.Windows.Forms.LinkLabel()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox()
        Me.txtCheckNumber = New System.Windows.Forms.TextBox()
        Me.pnlDepositSearchs = New System.Windows.Forms.Panel()
        Me.btnClearForm = New System.Windows.Forms.Button()
        Me.txtBatchNumber = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.dtpBatchDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.btnSearchDeposits = New System.Windows.Forms.Button()
        Me.dgvDeposits = New System.Windows.Forms.DataGridView()
        Me.bgwDeposits = New System.ComponentModel.BackgroundWorker()
        Me.bgwInvoices = New System.ComponentModel.BackgroundWorker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDepositReportEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDepositReportStartDate = New System.Windows.Forms.DateTimePicker()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInvoiceSearch.SuspendLayout()
        Me.pnlDepositSearchs.SuspendLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvDeposits)
        Me.SplitContainer2.Size = New System.Drawing.Size(811, 698)
        Me.SplitContainer2.SplitterDistance = 490
        Me.SplitContainer2.SplitterWidth = 10
        Me.SplitContainer2.TabIndex = 33
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.dgvInvoices)
        Me.Panel5.Controls.Add(Me.pnlInvoiceSearch)
        Me.Panel5.Controls.Add(Me.pnlDepositSearchs)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(811, 490)
        Me.Panel5.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.txtCreditCardNo)
        Me.Panel6.Controls.Add(Me.Label56)
        Me.Panel6.Controls.Add(Me.txtInvoiceForDeposit)
        Me.Panel6.Controls.Add(Me.mtbFeeYear2)
        Me.Panel6.Controls.Add(Me.DTPBatchDepositDateField)
        Me.Panel6.Controls.Add(Me.Label37)
        Me.Panel6.Controls.Add(Me.txtCheckNumberField)
        Me.Panel6.Controls.Add(Me.txtBatchNoField)
        Me.Panel6.Controls.Add(Me.txtDepositNumberField)
        Me.Panel6.Controls.Add(Me.btnDeleteInventoryRecords)
        Me.Panel6.Controls.Add(Me.Label47)
        Me.Panel6.Controls.Add(Me.txtDepositCount)
        Me.Panel6.Controls.Add(Me.lblBatchNo)
        Me.Panel6.Controls.Add(Me.lblCheckNo)
        Me.Panel6.Controls.Add(Me.lblDepositNo)
        Me.Panel6.Controls.Add(Me.Label36)
        Me.Panel6.Controls.Add(Me.lblInvoiceNumber)
        Me.Panel6.Controls.Add(Me.txtTransactionID)
        Me.Panel6.Controls.Add(Me.txtDepositComments)
        Me.Panel6.Controls.Add(Me.txtDepositAmount)
        Me.Panel6.Controls.Add(Me.lblAIRSNumber)
        Me.Panel6.Controls.Add(Me.Label38)
        Me.Panel6.Controls.Add(Me.lblFeeYear)
        Me.Panel6.Controls.Add(Me.Label39)
        Me.Panel6.Controls.Add(Me.lblFacilityName)
        Me.Panel6.Controls.Add(Me.btnDeleteCheckDeposit)
        Me.Panel6.Controls.Add(Me.btnAddNewCheckDeposit)
        Me.Panel6.Controls.Add(Me.btnUpdateExistingDeposit)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 313)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(811, 177)
        Me.Panel6.TabIndex = 35
        '
        'txtCreditCardNo
        '
        Me.txtCreditCardNo.Location = New System.Drawing.Point(698, 34)
        Me.txtCreditCardNo.Name = "txtCreditCardNo"
        Me.txtCreditCardNo.Size = New System.Drawing.Size(100, 20)
        Me.txtCreditCardNo.TabIndex = 62
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(617, 28)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(75, 26)
        Me.Label56.TabIndex = 61
        Me.Label56.Text = "Credit Card " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Confirmation #"
        '
        'txtInvoiceForDeposit
        '
        Me.txtInvoiceForDeposit.Location = New System.Drawing.Point(510, 64)
        Me.txtInvoiceForDeposit.Name = "txtInvoiceForDeposit"
        Me.txtInvoiceForDeposit.ReadOnly = True
        Me.txtInvoiceForDeposit.Size = New System.Drawing.Size(99, 20)
        Me.txtInvoiceForDeposit.TabIndex = 60
        '
        'mtbFeeYear2
        '
        Me.mtbFeeYear2.Location = New System.Drawing.Point(69, 34)
        Me.mtbFeeYear2.Mask = "0000"
        Me.mtbFeeYear2.Name = "mtbFeeYear2"
        Me.mtbFeeYear2.Size = New System.Drawing.Size(37, 20)
        Me.mtbFeeYear2.TabIndex = 59
        Me.mtbFeeYear2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'DTPBatchDepositDateField
        '
        Me.DTPBatchDepositDateField.CustomFormat = "dd-MMM-yyyy"
        Me.DTPBatchDepositDateField.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPBatchDepositDateField.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPBatchDepositDateField.Location = New System.Drawing.Point(302, 60)
        Me.DTPBatchDepositDateField.Name = "DTPBatchDepositDateField"
        Me.DTPBatchDepositDateField.Size = New System.Drawing.Size(114, 22)
        Me.DTPBatchDepositDateField.TabIndex = 57
        Me.DTPBatchDepositDateField.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(193, 67)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(103, 13)
        Me.Label37.TabIndex = 58
        Me.Label37.Text = "Batch Deposit Date:"
        '
        'txtCheckNumberField
        '
        Me.txtCheckNumberField.Location = New System.Drawing.Point(510, 34)
        Me.txtCheckNumberField.Name = "txtCheckNumberField"
        Me.txtCheckNumberField.Size = New System.Drawing.Size(100, 20)
        Me.txtCheckNumberField.TabIndex = 56
        '
        'txtBatchNoField
        '
        Me.txtBatchNoField.Location = New System.Drawing.Point(347, 34)
        Me.txtBatchNoField.Name = "txtBatchNoField"
        Me.txtBatchNoField.Size = New System.Drawing.Size(100, 20)
        Me.txtBatchNoField.TabIndex = 55
        '
        'txtDepositNumberField
        '
        Me.txtDepositNumberField.Location = New System.Drawing.Point(177, 34)
        Me.txtDepositNumberField.MaxLength = 20
        Me.txtDepositNumberField.Name = "txtDepositNumberField"
        Me.txtDepositNumberField.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositNumberField.TabIndex = 54
        '
        'btnDeleteInventoryRecords
        '
        Me.btnDeleteInventoryRecords.AutoSize = True
        Me.btnDeleteInventoryRecords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteInventoryRecords.Location = New System.Drawing.Point(527, 137)
        Me.btnDeleteInventoryRecords.Name = "btnDeleteInventoryRecords"
        Me.btnDeleteInventoryRecords.Size = New System.Drawing.Size(124, 23)
        Me.btnDeleteInventoryRecords.TabIndex = 53
        Me.btnDeleteInventoryRecords.Text = "Delete Invoice Record"
        Me.btnDeleteInventoryRecords.UseVisualStyleBackColor = True
        Me.btnDeleteInventoryRecords.Visible = False
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(693, 142)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(38, 13)
        Me.Label47.TabIndex = 52
        Me.Label47.Text = "Count:"
        '
        'txtDepositCount
        '
        Me.txtDepositCount.Location = New System.Drawing.Point(737, 139)
        Me.txtDepositCount.Name = "txtDepositCount"
        Me.txtDepositCount.ReadOnly = True
        Me.txtDepositCount.Size = New System.Drawing.Size(61, 20)
        Me.txtDepositCount.TabIndex = 51
        '
        'lblBatchNo
        '
        Me.lblBatchNo.AutoSize = True
        Me.lblBatchNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchNo.Location = New System.Drawing.Point(296, 37)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(45, 13)
        Me.lblBatchNo.TabIndex = 50
        Me.lblBatchNo.Text = "Batch #"
        '
        'lblCheckNo
        '
        Me.lblCheckNo.AutoSize = True
        Me.lblCheckNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckNo.Location = New System.Drawing.Point(456, 37)
        Me.lblCheckNo.Name = "lblCheckNo"
        Me.lblCheckNo.Size = New System.Drawing.Size(48, 13)
        Me.lblCheckNo.TabIndex = 49
        Me.lblCheckNo.Text = "Check #"
        '
        'lblDepositNo
        '
        Me.lblDepositNo.AutoSize = True
        Me.lblDepositNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositNo.Location = New System.Drawing.Point(118, 37)
        Me.lblDepositNo.Name = "lblDepositNo"
        Me.lblDepositNo.Size = New System.Drawing.Size(53, 13)
        Me.lblDepositNo.TabIndex = 48
        Me.lblDepositNo.Text = "Deposit #"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(615, 67)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(77, 13)
        Me.Label36.TabIndex = 47
        Me.Label36.Text = "Transaction ID"
        '
        'lblInvoiceNumber
        '
        Me.lblInvoiceNumber.AutoSize = True
        Me.lblInvoiceNumber.Location = New System.Drawing.Point(452, 67)
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        Me.lblInvoiceNumber.Size = New System.Drawing.Size(52, 13)
        Me.lblInvoiceNumber.TabIndex = 46
        Me.lblInvoiceNumber.Text = "Invoice #"
        '
        'txtTransactionID
        '
        Me.txtTransactionID.Location = New System.Drawing.Point(698, 64)
        Me.txtTransactionID.Name = "txtTransactionID"
        Me.txtTransactionID.ReadOnly = True
        Me.txtTransactionID.Size = New System.Drawing.Size(100, 20)
        Me.txtTransactionID.TabIndex = 3
        '
        'txtDepositComments
        '
        Me.txtDepositComments.AcceptsReturn = True
        Me.txtDepositComments.Location = New System.Drawing.Point(69, 92)
        Me.txtDepositComments.Multiline = True
        Me.txtDepositComments.Name = "txtDepositComments"
        Me.txtDepositComments.Size = New System.Drawing.Size(322, 39)
        Me.txtDepositComments.TabIndex = 4
        '
        'txtDepositAmount
        '
        Me.txtDepositAmount.Location = New System.Drawing.Point(69, 64)
        Me.txtDepositAmount.Name = "txtDepositAmount"
        Me.txtDepositAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositAmount.TabIndex = 1
        '
        'lblAIRSNumber
        '
        Me.lblAIRSNumber.AutoSize = True
        Me.lblAIRSNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAIRSNumber.Location = New System.Drawing.Point(7, 3)
        Me.lblAIRSNumber.Name = "lblAIRSNumber"
        Me.lblAIRSNumber.Size = New System.Drawing.Size(49, 16)
        Me.lblAIRSNumber.TabIndex = 42
        Me.lblAIRSNumber.Text = "AIRS #"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(20, 67)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(43, 13)
        Me.Label38.TabIndex = 40
        Me.Label38.Text = "Amount"
        '
        'lblFeeYear
        '
        Me.lblFeeYear.AutoSize = True
        Me.lblFeeYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFeeYear.Location = New System.Drawing.Point(13, 37)
        Me.lblFeeYear.Name = "lblFeeYear"
        Me.lblFeeYear.Size = New System.Drawing.Size(50, 13)
        Me.lblFeeYear.TabIndex = 43
        Me.lblFeeYear.Text = "Fee Year"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(7, 92)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(56, 26)
        Me.Label39.TabIndex = 44
        Me.Label39.Text = "EPD " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Comments"
        '
        'lblFacilityName
        '
        Me.lblFacilityName.AutoSize = True
        Me.lblFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityName.Location = New System.Drawing.Point(195, 3)
        Me.lblFacilityName.Name = "lblFacilityName"
        Me.lblFacilityName.Size = New System.Drawing.Size(90, 16)
        Me.lblFacilityName.TabIndex = 41
        Me.lblFacilityName.Text = "Facility Name"
        '
        'btnDeleteCheckDeposit
        '
        Me.btnDeleteCheckDeposit.AutoSize = True
        Me.btnDeleteCheckDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteCheckDeposit.Location = New System.Drawing.Point(378, 137)
        Me.btnDeleteCheckDeposit.Name = "btnDeleteCheckDeposit"
        Me.btnDeleteCheckDeposit.Size = New System.Drawing.Size(121, 23)
        Me.btnDeleteCheckDeposit.TabIndex = 7
        Me.btnDeleteCheckDeposit.Text = "Delete Check Deposit"
        Me.btnDeleteCheckDeposit.UseVisualStyleBackColor = True
        '
        'btnAddNewCheckDeposit
        '
        Me.btnAddNewCheckDeposit.AutoSize = True
        Me.btnAddNewCheckDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewCheckDeposit.Location = New System.Drawing.Point(13, 137)
        Me.btnAddNewCheckDeposit.Name = "btnAddNewCheckDeposit"
        Me.btnAddNewCheckDeposit.Size = New System.Drawing.Size(134, 23)
        Me.btnAddNewCheckDeposit.TabIndex = 5
        Me.btnAddNewCheckDeposit.Text = "Add New Check Deposit"
        Me.btnAddNewCheckDeposit.UseVisualStyleBackColor = True
        '
        'btnUpdateExistingDeposit
        '
        Me.btnUpdateExistingDeposit.AutoSize = True
        Me.btnUpdateExistingDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateExistingDeposit.Location = New System.Drawing.Point(177, 137)
        Me.btnUpdateExistingDeposit.Name = "btnUpdateExistingDeposit"
        Me.btnUpdateExistingDeposit.Size = New System.Drawing.Size(164, 23)
        Me.btnUpdateExistingDeposit.TabIndex = 6
        Me.btnUpdateExistingDeposit.Text = "Update Existing Check Deposit"
        Me.btnUpdateExistingDeposit.UseVisualStyleBackColor = True
        '
        'dgvInvoices
        '
        Me.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvInvoices.Location = New System.Drawing.Point(0, 113)
        Me.dgvInvoices.Name = "dgvInvoices"
        Me.dgvInvoices.ReadOnly = True
        Me.dgvInvoices.Size = New System.Drawing.Size(811, 200)
        Me.dgvInvoices.TabIndex = 13
        '
        'pnlInvoiceSearch
        '
        Me.pnlInvoiceSearch.Controls.Add(Me.llbSearchForInvoice)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtSearchInvoice)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label55)
        Me.pnlInvoiceSearch.Controls.Add(Me.llbSearchForCheck)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label48)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtCountInvoices)
        Me.pnlInvoiceSearch.Controls.Add(Me.btnClearEntryInformation)
        Me.pnlInvoiceSearch.Controls.Add(Me.mtbFeeYear)
        Me.pnlInvoiceSearch.Controls.Add(Me.lblViewInvoices)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label41)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label42)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label43)
        Me.pnlInvoiceSearch.Controls.Add(Me.mtbAIRSNumber)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtCheckNumber)
        Me.pnlInvoiceSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInvoiceSearch.Location = New System.Drawing.Point(0, 54)
        Me.pnlInvoiceSearch.Name = "pnlInvoiceSearch"
        Me.pnlInvoiceSearch.Size = New System.Drawing.Size(811, 59)
        Me.pnlInvoiceSearch.TabIndex = 34
        '
        'llbSearchForInvoice
        '
        Me.llbSearchForInvoice.AutoSize = True
        Me.llbSearchForInvoice.Location = New System.Drawing.Point(455, 9)
        Me.llbSearchForInvoice.Name = "llbSearchForInvoice"
        Me.llbSearchForInvoice.Size = New System.Drawing.Size(107, 13)
        Me.llbSearchForInvoice.TabIndex = 59
        Me.llbSearchForInvoice.TabStop = True
        Me.llbSearchForInvoice.Text = "Search For Invoice #"
        '
        'txtSearchInvoice
        '
        Me.txtSearchInvoice.Location = New System.Drawing.Point(349, 5)
        Me.txtSearchInvoice.Name = "txtSearchInvoice"
        Me.txtSearchInvoice.Size = New System.Drawing.Size(100, 20)
        Me.txtSearchInvoice.TabIndex = 58
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(291, 9)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(52, 13)
        Me.Label55.TabIndex = 57
        Me.Label55.Text = "Invoice #"
        '
        'llbSearchForCheck
        '
        Me.llbSearchForCheck.AutoSize = True
        Me.llbSearchForCheck.Location = New System.Drawing.Point(167, 8)
        Me.llbSearchForCheck.Name = "llbSearchForCheck"
        Me.llbSearchForCheck.Size = New System.Drawing.Size(103, 13)
        Me.llbSearchForCheck.TabIndex = 55
        Me.llbSearchForCheck.TabStop = True
        Me.llbSearchForCheck.Text = "Search For Check #"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(650, 34)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(38, 13)
        Me.Label48.TabIndex = 54
        Me.Label48.Text = "Count:"
        '
        'txtCountInvoices
        '
        Me.txtCountInvoices.Location = New System.Drawing.Point(694, 31)
        Me.txtCountInvoices.Name = "txtCountInvoices"
        Me.txtCountInvoices.ReadOnly = True
        Me.txtCountInvoices.Size = New System.Drawing.Size(61, 20)
        Me.txtCountInvoices.TabIndex = 53
        '
        'btnClearEntryInformation
        '
        Me.btnClearEntryInformation.AutoSize = True
        Me.btnClearEntryInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearEntryInformation.Location = New System.Drawing.Point(632, 3)
        Me.btnClearEntryInformation.Name = "btnClearEntryInformation"
        Me.btnClearEntryInformation.Size = New System.Drawing.Size(123, 23)
        Me.btnClearEntryInformation.TabIndex = 4
        Me.btnClearEntryInformation.Text = "Clear Entry Information"
        Me.btnClearEntryInformation.UseVisualStyleBackColor = True
        '
        'mtbFeeYear
        '
        Me.mtbFeeYear.Location = New System.Drawing.Point(177, 31)
        Me.mtbFeeYear.Mask = "0000"
        Me.mtbFeeYear.Name = "mtbFeeYear"
        Me.mtbFeeYear.Size = New System.Drawing.Size(36, 20)
        Me.mtbFeeYear.TabIndex = 2
        Me.mtbFeeYear.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblViewInvoices
        '
        Me.lblViewInvoices.AutoSize = True
        Me.lblViewInvoices.Location = New System.Drawing.Point(219, 34)
        Me.lblViewInvoices.Name = "lblViewInvoices"
        Me.lblViewInvoices.Size = New System.Drawing.Size(79, 13)
        Me.lblViewInvoices.TabIndex = 3
        Me.lblViewInvoices.TabStop = True
        Me.lblViewInvoices.Text = "View Invoice(s)"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(142, 34)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(29, 13)
        Me.Label41.TabIndex = 3
        Me.Label41.Text = "Year"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(7, 8)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(48, 13)
        Me.Label42.TabIndex = 4
        Me.Label42.Text = "Check #"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(13, 34)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(42, 13)
        Me.Label43.TabIndex = 5
        Me.Label43.Text = "AIRS #"
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(61, 31)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(62, 20)
        Me.mtbAIRSNumber.TabIndex = 1
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtCheckNumber
        '
        Me.txtCheckNumber.Location = New System.Drawing.Point(61, 5)
        Me.txtCheckNumber.Name = "txtCheckNumber"
        Me.txtCheckNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtCheckNumber.TabIndex = 0
        '
        'pnlDepositSearchs
        '
        Me.pnlDepositSearchs.AutoScroll = True
        Me.pnlDepositSearchs.Controls.Add(Me.Label2)
        Me.pnlDepositSearchs.Controls.Add(Me.Label3)
        Me.pnlDepositSearchs.Controls.Add(Me.dtpDepositReportEndDate)
        Me.pnlDepositSearchs.Controls.Add(Me.dtpDepositReportStartDate)
        Me.pnlDepositSearchs.Controls.Add(Me.btnClearForm)
        Me.pnlDepositSearchs.Controls.Add(Me.txtBatchNumber)
        Me.pnlDepositSearchs.Controls.Add(Me.Label45)
        Me.pnlDepositSearchs.Controls.Add(Me.dtpBatchDepositDate)
        Me.pnlDepositSearchs.Controls.Add(Me.Label46)
        Me.pnlDepositSearchs.Controls.Add(Me.btnSearchDeposits)
        Me.pnlDepositSearchs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDepositSearchs.Location = New System.Drawing.Point(0, 0)
        Me.pnlDepositSearchs.Name = "pnlDepositSearchs"
        Me.pnlDepositSearchs.Size = New System.Drawing.Size(811, 54)
        Me.pnlDepositSearchs.TabIndex = 33
        '
        'btnClearForm
        '
        Me.btnClearForm.AutoSize = True
        Me.btnClearForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearForm.Location = New System.Drawing.Point(700, 22)
        Me.btnClearForm.Name = "btnClearForm"
        Me.btnClearForm.Size = New System.Drawing.Size(55, 23)
        Me.btnClearForm.TabIndex = 4
        Me.btnClearForm.Text = "Clear All"
        Me.btnClearForm.UseVisualStyleBackColor = True
        '
        'txtBatchNumber
        '
        Me.txtBatchNumber.Location = New System.Drawing.Point(373, 25)
        Me.txtBatchNumber.Name = "txtBatchNumber"
        Me.txtBatchNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtBatchNumber.TabIndex = 2
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(372, 9)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(45, 13)
        Me.Label45.TabIndex = 2
        Me.Label45.Text = "Batch #"
        '
        'dtpBatchDepositDate
        '
        Me.dtpBatchDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpBatchDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBatchDepositDate.Location = New System.Drawing.Point(499, 25)
        Me.dtpBatchDepositDate.Name = "dtpBatchDepositDate"
        Me.dtpBatchDepositDate.Size = New System.Drawing.Size(114, 20)
        Me.dtpBatchDepositDate.TabIndex = 3
        Me.dtpBatchDepositDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(496, 9)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(103, 13)
        Me.Label46.TabIndex = 6
        Me.Label46.Text = "Batch Deposit Date:"
        '
        'btnSearchDeposits
        '
        Me.btnSearchDeposits.AutoSize = True
        Me.btnSearchDeposits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchDeposits.Location = New System.Drawing.Point(227, 23)
        Me.btnSearchDeposits.Name = "btnSearchDeposits"
        Me.btnSearchDeposits.Size = New System.Drawing.Size(90, 23)
        Me.btnSearchDeposits.TabIndex = 1
        Me.btnSearchDeposits.Text = "Deposit Search"
        Me.btnSearchDeposits.UseVisualStyleBackColor = True
        '
        'dgvDeposits
        '
        Me.dgvDeposits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDeposits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDeposits.Location = New System.Drawing.Point(0, 0)
        Me.dgvDeposits.Name = "dgvDeposits"
        Me.dgvDeposits.ReadOnly = True
        Me.dgvDeposits.Size = New System.Drawing.Size(811, 198)
        Me.dgvDeposits.TabIndex = 28
        '
        'bgwDeposits
        '
        '
        'bgwInvoices
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(118, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 412
        Me.Label2.Text = "End Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 411
        Me.Label3.Text = "Start Date"
        '
        'dtpDepositReportEndDate
        '
        Me.dtpDepositReportEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositReportEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositReportEndDate.Location = New System.Drawing.Point(121, 25)
        Me.dtpDepositReportEndDate.Name = "dtpDepositReportEndDate"
        Me.dtpDepositReportEndDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDepositReportEndDate.TabIndex = 410
        '
        'dtpDepositReportStartDate
        '
        Me.dtpDepositReportStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDepositReportStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepositReportStartDate.Location = New System.Drawing.Point(15, 25)
        Me.dtpDepositReportStartDate.Name = "dtpDepositReportStartDate"
        Me.dtpDepositReportStartDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDepositReportStartDate.TabIndex = 409
        '
        'PASPDepositsAmendments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 698)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "PASPDepositsAmendments"
        Me.Text = "Fee Deposits"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInvoiceSearch.ResumeLayout(False)
        Me.pnlInvoiceSearch.PerformLayout()
        Me.pnlDepositSearchs.ResumeLayout(False)
        Me.pnlDepositSearchs.PerformLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bgwDeposits As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwInvoices As System.ComponentModel.BackgroundWorker
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dgvInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents pnlInvoiceSearch As System.Windows.Forms.Panel
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtCountInvoices As System.Windows.Forms.TextBox
    Friend WithEvents btnClearEntryInformation As System.Windows.Forms.Button
    Friend WithEvents mtbFeeYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblViewInvoices As System.Windows.Forms.LinkLabel
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCheckNumber As System.Windows.Forms.TextBox
    Friend WithEvents pnlDepositSearchs As System.Windows.Forms.Panel
    Friend WithEvents btnClearForm As System.Windows.Forms.Button
    Friend WithEvents txtBatchNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents dtpBatchDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents btnSearchDeposits As System.Windows.Forms.Button
    Friend WithEvents dgvDeposits As System.Windows.Forms.DataGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnDeleteInventoryRecords As System.Windows.Forms.Button
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtDepositCount As System.Windows.Forms.TextBox
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
    Friend WithEvents DTPBatchDepositDateField As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents mtbFeeYear2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents llbSearchForCheck As System.Windows.Forms.LinkLabel
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceForDeposit As System.Windows.Forms.TextBox
    Friend WithEvents txtCreditCardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents llbSearchForInvoice As System.Windows.Forms.LinkLabel
    Friend WithEvents txtSearchInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDepositReportEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDepositReportStartDate As System.Windows.Forms.DateTimePicker
End Class
