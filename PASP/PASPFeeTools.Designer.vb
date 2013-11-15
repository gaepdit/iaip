<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPFeeTools
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
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TCFeeTools = New System.Windows.Forms.TabControl
        Me.TPFeeDeposits = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgvInvoices = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDepositInvoiceNo = New System.Windows.Forms.TextBox
        Me.txtDepositPayID = New System.Windows.Forms.TextBox
        Me.cboDepositPayType = New System.Windows.Forms.ComboBox
        Me.txtDepositComments = New System.Windows.Forms.TextBox
        Me.txtDepositAmount = New System.Windows.Forms.TextBox
        Me.lblAIRSNumber = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnDeleteCheckDeposit = New System.Windows.Forms.Button
        Me.lblFeeYear = New System.Windows.Forms.Label
        Me.btnAddNewCheckDeposit = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblFacilityName = New System.Windows.Forms.Label
        Me.btnUpdateExistingDeposit = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.pnlInvoiceSearch = New System.Windows.Forms.Panel
        Me.btnClearForm = New System.Windows.Forms.Button
        Me.mtbFeeYear = New System.Windows.Forms.MaskedTextBox
        Me.lblViewInvoices = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.txtCheckNumber = New System.Windows.Forms.TextBox
        Me.pnlDepositSearchs = New System.Windows.Forms.Panel
        Me.txtBatchNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDepositNumber = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpBatchDepositDate = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnSearchDeposits = New System.Windows.Forms.Button
        Me.dgvDeposits = New System.Windows.Forms.DataGridView
        Me.bgwDeposits = New System.ComponentModel.BackgroundWorker
        Me.bgwInvoices = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCFeeTools.SuspendLayout()
        Me.TPFeeDeposits.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlInvoiceSearch.SuspendLayout()
        Me.pnlDepositSearchs.SuspendLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 668)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(816, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pnl1
        '
        Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(793, 17)
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
        Me.MenuStrip1.Size = New System.Drawing.Size(816, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'TCFeeTools
        '
        Me.TCFeeTools.Controls.Add(Me.TPFeeDeposits)
        Me.TCFeeTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCFeeTools.Location = New System.Drawing.Point(0, 24)
        Me.TCFeeTools.Name = "TCFeeTools"
        Me.TCFeeTools.SelectedIndex = 0
        Me.TCFeeTools.Size = New System.Drawing.Size(816, 644)
        Me.TCFeeTools.TabIndex = 9
        '
        'TPFeeDeposits
        '
        Me.TPFeeDeposits.Controls.Add(Me.SplitContainer1)
        Me.TPFeeDeposits.Location = New System.Drawing.Point(4, 22)
        Me.TPFeeDeposits.Name = "TPFeeDeposits"
        Me.TPFeeDeposits.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFeeDeposits.Size = New System.Drawing.Size(808, 618)
        Me.TPFeeDeposits.TabIndex = 0
        Me.TPFeeDeposits.Text = "Fee Deposits"
        Me.TPFeeDeposits.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvDeposits)
        Me.SplitContainer1.Size = New System.Drawing.Size(802, 612)
        Me.SplitContainer1.SplitterDistance = 454
        Me.SplitContainer1.TabIndex = 32
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgvInvoices)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.pnlInvoiceSearch)
        Me.Panel1.Controls.Add(Me.pnlDepositSearchs)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(802, 454)
        Me.Panel1.TabIndex = 1
        '
        'dgvInvoices
        '
        Me.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInvoices.Location = New System.Drawing.Point(0, 97)
        Me.dgvInvoices.Name = "dgvInvoices"
        Me.dgvInvoices.ReadOnly = True
        Me.dgvInvoices.Size = New System.Drawing.Size(802, 196)
        Me.dgvInvoices.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtDepositInvoiceNo)
        Me.Panel2.Controls.Add(Me.txtDepositPayID)
        Me.Panel2.Controls.Add(Me.cboDepositPayType)
        Me.Panel2.Controls.Add(Me.txtDepositComments)
        Me.Panel2.Controls.Add(Me.txtDepositAmount)
        Me.Panel2.Controls.Add(Me.lblAIRSNumber)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.btnDeleteCheckDeposit)
        Me.Panel2.Controls.Add(Me.lblFeeYear)
        Me.Panel2.Controls.Add(Me.btnAddNewCheckDeposit)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.lblFacilityName)
        Me.Panel2.Controls.Add(Me.btnUpdateExistingDeposit)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 293)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(802, 161)
        Me.Panel2.TabIndex = 35
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(428, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Pay ID"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(205, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Invoice Number"
        '
        'txtDepositInvoiceNo
        '
        Me.txtDepositInvoiceNo.Location = New System.Drawing.Point(293, 62)
        Me.txtDepositInvoiceNo.Name = "txtDepositInvoiceNo"
        Me.txtDepositInvoiceNo.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositInvoiceNo.TabIndex = 2
        '
        'txtDepositPayID
        '
        Me.txtDepositPayID.Location = New System.Drawing.Point(486, 62)
        Me.txtDepositPayID.Name = "txtDepositPayID"
        Me.txtDepositPayID.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositPayID.TabIndex = 3
        '
        'cboDepositPayType
        '
        Me.cboDepositPayType.FormattingEnabled = True
        Me.cboDepositPayType.Location = New System.Drawing.Point(83, 36)
        Me.cboDepositPayType.Name = "cboDepositPayType"
        Me.cboDepositPayType.Size = New System.Drawing.Size(121, 21)
        Me.cboDepositPayType.TabIndex = 0
        '
        'txtDepositComments
        '
        Me.txtDepositComments.AcceptsReturn = True
        Me.txtDepositComments.Location = New System.Drawing.Point(83, 88)
        Me.txtDepositComments.Multiline = True
        Me.txtDepositComments.Name = "txtDepositComments"
        Me.txtDepositComments.Size = New System.Drawing.Size(237, 28)
        Me.txtDepositComments.TabIndex = 4
        '
        'txtDepositAmount
        '
        Me.txtDepositAmount.Location = New System.Drawing.Point(83, 62)
        Me.txtDepositAmount.Name = "txtDepositAmount"
        Me.txtDepositAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositAmount.TabIndex = 1
        '
        'lblAIRSNumber
        '
        Me.lblAIRSNumber.AutoSize = True
        Me.lblAIRSNumber.Location = New System.Drawing.Point(21, 16)
        Me.lblAIRSNumber.Name = "lblAIRSNumber"
        Me.lblAIRSNumber.Size = New System.Drawing.Size(42, 13)
        Me.lblAIRSNumber.TabIndex = 10
        Me.lblAIRSNumber.Text = "AIRS #"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(34, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Amount"
        '
        'btnDeleteCheckDeposit
        '
        Me.btnDeleteCheckDeposit.AutoSize = True
        Me.btnDeleteCheckDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteCheckDeposit.Location = New System.Drawing.Point(441, 125)
        Me.btnDeleteCheckDeposit.Name = "btnDeleteCheckDeposit"
        Me.btnDeleteCheckDeposit.Size = New System.Drawing.Size(121, 23)
        Me.btnDeleteCheckDeposit.TabIndex = 7
        Me.btnDeleteCheckDeposit.Text = "Delete Check Deposit"
        Me.btnDeleteCheckDeposit.UseVisualStyleBackColor = True
        '
        'lblFeeYear
        '
        Me.lblFeeYear.AutoSize = True
        Me.lblFeeYear.Location = New System.Drawing.Point(223, 39)
        Me.lblFeeYear.Name = "lblFeeYear"
        Me.lblFeeYear.Size = New System.Drawing.Size(50, 13)
        Me.lblFeeYear.TabIndex = 11
        Me.lblFeeYear.Text = "Fee Year"
        '
        'btnAddNewCheckDeposit
        '
        Me.btnAddNewCheckDeposit.AutoSize = True
        Me.btnAddNewCheckDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewCheckDeposit.Location = New System.Drawing.Point(16, 125)
        Me.btnAddNewCheckDeposit.Name = "btnAddNewCheckDeposit"
        Me.btnAddNewCheckDeposit.Size = New System.Drawing.Size(134, 23)
        Me.btnAddNewCheckDeposit.TabIndex = 5
        Me.btnAddNewCheckDeposit.Text = "Add New Check Deposit"
        Me.btnAddNewCheckDeposit.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(21, 91)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 13)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Comments"
        '
        'lblFacilityName
        '
        Me.lblFacilityName.AutoSize = True
        Me.lblFacilityName.Location = New System.Drawing.Point(203, 16)
        Me.lblFacilityName.Name = "lblFacilityName"
        Me.lblFacilityName.Size = New System.Drawing.Size(70, 13)
        Me.lblFacilityName.TabIndex = 9
        Me.lblFacilityName.Text = "Facility Name"
        '
        'btnUpdateExistingDeposit
        '
        Me.btnUpdateExistingDeposit.AutoSize = True
        Me.btnUpdateExistingDeposit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateExistingDeposit.Location = New System.Drawing.Point(198, 125)
        Me.btnUpdateExistingDeposit.Name = "btnUpdateExistingDeposit"
        Me.btnUpdateExistingDeposit.Size = New System.Drawing.Size(164, 23)
        Me.btnUpdateExistingDeposit.TabIndex = 6
        Me.btnUpdateExistingDeposit.Text = "Update Existing Check Deposit"
        Me.btnUpdateExistingDeposit.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(25, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(52, 13)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Pay Type"
        '
        'pnlInvoiceSearch
        '
        Me.pnlInvoiceSearch.Controls.Add(Me.btnClearForm)
        Me.pnlInvoiceSearch.Controls.Add(Me.mtbFeeYear)
        Me.pnlInvoiceSearch.Controls.Add(Me.lblViewInvoices)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label3)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label4)
        Me.pnlInvoiceSearch.Controls.Add(Me.Label5)
        Me.pnlInvoiceSearch.Controls.Add(Me.mtbAIRSNumber)
        Me.pnlInvoiceSearch.Controls.Add(Me.txtCheckNumber)
        Me.pnlInvoiceSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInvoiceSearch.Location = New System.Drawing.Point(0, 38)
        Me.pnlInvoiceSearch.Name = "pnlInvoiceSearch"
        Me.pnlInvoiceSearch.Size = New System.Drawing.Size(802, 59)
        Me.pnlInvoiceSearch.TabIndex = 34
        '
        'btnClearForm
        '
        Me.btnClearForm.AutoSize = True
        Me.btnClearForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearForm.Location = New System.Drawing.Point(588, 10)
        Me.btnClearForm.Name = "btnClearForm"
        Me.btnClearForm.Size = New System.Drawing.Size(67, 23)
        Me.btnClearForm.TabIndex = 33
        Me.btnClearForm.Text = "Clear Form"
        Me.btnClearForm.UseVisualStyleBackColor = True
        '
        'mtbFeeYear
        '
        Me.mtbFeeYear.Location = New System.Drawing.Point(216, 30)
        Me.mtbFeeYear.Mask = "0000"
        Me.mtbFeeYear.Name = "mtbFeeYear"
        Me.mtbFeeYear.Size = New System.Drawing.Size(37, 20)
        Me.mtbFeeYear.TabIndex = 2
        Me.mtbFeeYear.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblViewInvoices
        '
        Me.lblViewInvoices.AutoSize = True
        Me.lblViewInvoices.Location = New System.Drawing.Point(259, 35)
        Me.lblViewInvoices.Name = "lblViewInvoices"
        Me.lblViewInvoices.Size = New System.Drawing.Size(79, 13)
        Me.lblViewInvoices.TabIndex = 3
        Me.lblViewInvoices.TabStop = True
        Me.lblViewInvoices.Text = "View Invoice(s)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(184, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Year"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Check #"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "AIRS #"
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(61, 32)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(62, 20)
        Me.mtbAIRSNumber.TabIndex = 1
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtCheckNumber
        '
        Me.txtCheckNumber.Location = New System.Drawing.Point(61, 6)
        Me.txtCheckNumber.Name = "txtCheckNumber"
        Me.txtCheckNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtCheckNumber.TabIndex = 0
        '
        'pnlDepositSearchs
        '
        Me.pnlDepositSearchs.Controls.Add(Me.txtBatchNumber)
        Me.pnlDepositSearchs.Controls.Add(Me.Label1)
        Me.pnlDepositSearchs.Controls.Add(Me.txtDepositNumber)
        Me.pnlDepositSearchs.Controls.Add(Me.Label2)
        Me.pnlDepositSearchs.Controls.Add(Me.dtpBatchDepositDate)
        Me.pnlDepositSearchs.Controls.Add(Me.Label6)
        Me.pnlDepositSearchs.Controls.Add(Me.btnSearchDeposits)
        Me.pnlDepositSearchs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDepositSearchs.Location = New System.Drawing.Point(0, 0)
        Me.pnlDepositSearchs.Name = "pnlDepositSearchs"
        Me.pnlDepositSearchs.Size = New System.Drawing.Size(802, 38)
        Me.pnlDepositSearchs.TabIndex = 33
        '
        'txtBatchNumber
        '
        Me.txtBatchNumber.Location = New System.Drawing.Point(326, 5)
        Me.txtBatchNumber.Name = "txtBatchNumber"
        Me.txtBatchNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtBatchNumber.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Deposit #"
        '
        'txtDepositNumber
        '
        Me.txtDepositNumber.Location = New System.Drawing.Point(61, 5)
        Me.txtDepositNumber.Name = "txtDepositNumber"
        Me.txtDepositNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtDepositNumber.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(275, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Batch #"
        '
        'dtpBatchDepositDate
        '
        Me.dtpBatchDepositDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpBatchDepositDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBatchDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBatchDepositDate.Location = New System.Drawing.Point(547, 4)
        Me.dtpBatchDepositDate.Name = "dtpBatchDepositDate"
        Me.dtpBatchDepositDate.Size = New System.Drawing.Size(115, 22)
        Me.dtpBatchDepositDate.TabIndex = 3
        Me.dtpBatchDepositDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(438, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Batch Deposit Date:"
        '
        'btnSearchDeposits
        '
        Me.btnSearchDeposits.AutoSize = True
        Me.btnSearchDeposits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchDeposits.Location = New System.Drawing.Point(167, 4)
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
        Me.dgvDeposits.Size = New System.Drawing.Size(802, 154)
        Me.dgvDeposits.TabIndex = 28
        '
        'bgwDeposits
        '
        '
        'bgwInvoices
        '
        '
        'PASPFeeTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 690)
        Me.Controls.Add(Me.TCFeeTools)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "PASPFeeTools"
        Me.Text = "PASP Fee Tools"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCFeeTools.ResumeLayout(False)
        Me.TPFeeDeposits.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlInvoiceSearch.ResumeLayout(False)
        Me.pnlInvoiceSearch.PerformLayout()
        Me.pnlDepositSearchs.ResumeLayout(False)
        Me.pnlDepositSearchs.PerformLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents TCFeeTools As System.Windows.Forms.TabControl
    Friend WithEvents TPFeeDeposits As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDepositNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFeeYear As System.Windows.Forms.Label
    Friend WithEvents lblAIRSNumber As System.Windows.Forms.Label
    Friend WithEvents lblFacilityName As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBatchNumber As System.Windows.Forms.TextBox
    Friend WithEvents dgvInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchDeposits As System.Windows.Forms.Button
    Friend WithEvents dgvDeposits As System.Windows.Forms.DataGridView
    Friend WithEvents btnAddNewCheckDeposit As System.Windows.Forms.Button
    Friend WithEvents btnUpdateExistingDeposit As System.Windows.Forms.Button
    Friend WithEvents btnDeleteCheckDeposit As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtCheckNumber As System.Windows.Forms.TextBox
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dtpBatchDepositDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlInvoiceSearch As System.Windows.Forms.Panel
    Friend WithEvents lblViewInvoices As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlDepositSearchs As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents mtbFeeYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cboDepositPayType As System.Windows.Forms.ComboBox
    Friend WithEvents txtDepositComments As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositPayID As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents bgwDeposits As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwInvoices As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnClearForm As System.Windows.Forms.Button
End Class
