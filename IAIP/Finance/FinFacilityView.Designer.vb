<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinFacilityView
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblFacilityDisplay = New System.Windows.Forms.Label()
        Me.dgvInvoices = New Iaip.IaipDataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnNewDeposit = New System.Windows.Forms.Button()
        Me.InvoiceLine = New System.Windows.Forms.Label()
        Me.txtAmountInvoiced = New Iaip.CurrencyTextBox()
        Me.txtPaymentsApplied = New Iaip.CurrencyTextBox()
        Me.txtInvoiceBalance = New Iaip.CurrencyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnAddRefund = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCredits = New Iaip.CurrencyTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvCredits = New Iaip.IaipDataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgvUnInvoiced = New Iaip.IaipDataGridView()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblDataErrorMessage = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUnInvoiced, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblFacilityDisplay
        '
        Me.lblFacilityDisplay.AutoSize = True
        Me.lblFacilityDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityDisplay.Location = New System.Drawing.Point(42, 16)
        Me.lblFacilityDisplay.Name = "lblFacilityDisplay"
        Me.lblFacilityDisplay.Size = New System.Drawing.Size(188, 17)
        Me.lblFacilityDisplay.TabIndex = 9
        Me.lblFacilityDisplay.Text = "000-00000 Facility Name"
        '
        'dgvInvoices
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvInvoices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInvoices.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInvoices.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvInvoices.LinkifyFirstColumn = True
        Me.dgvInvoices.Location = New System.Drawing.Point(12, 19)
        Me.dgvInvoices.Name = "dgvInvoices"
        Me.dgvInvoices.ResultsCountLabel = Nothing
        Me.dgvInvoices.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInvoices.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvInvoices.ShowEditingIcon = False
        Me.dgvInvoices.Size = New System.Drawing.Size(550, 188)
        Me.dgvInvoices.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 336
        Me.Label4.Text = "Invoices"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label5.Location = New System.Drawing.Point(12, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 13)
        Me.Label5.TabIndex = 334
        Me.Label5.Text = "Total Amount Invoiced"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label2.Location = New System.Drawing.Point(12, 251)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 334
        Me.Label2.Text = "Payments Applied"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label3.Location = New System.Drawing.Point(12, 271)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 13)
        Me.Label3.TabIndex = 334
        Me.Label3.Text = "Current Balance On Invoices"
        '
        'btnNewDeposit
        '
        Me.btnNewDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewDeposit.AutoSize = True
        Me.btnNewDeposit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewDeposit.Location = New System.Drawing.Point(429, 11)
        Me.btnNewDeposit.Name = "btnNewDeposit"
        Me.btnNewDeposit.Size = New System.Drawing.Size(133, 27)
        Me.btnNewDeposit.TabIndex = 1
        Me.btnNewDeposit.Text = "Add New Deposit"
        Me.btnNewDeposit.UseVisualStyleBackColor = True
        Me.btnNewDeposit.Visible = False
        '
        'InvoiceLine
        '
        Me.InvoiceLine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.InvoiceLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InvoiceLine.Location = New System.Drawing.Point(188, 267)
        Me.InvoiceLine.Name = "InvoiceLine"
        Me.InvoiceLine.Size = New System.Drawing.Size(71, 1)
        Me.InvoiceLine.TabIndex = 339
        '
        'txtAmountInvoiced
        '
        Me.txtAmountInvoiced.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountInvoiced.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAmountInvoiced.BackColor = System.Drawing.SystemColors.Control
        Me.txtAmountInvoiced.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmountInvoiced.Cue = "$0"
        Me.txtAmountInvoiced.Location = New System.Drawing.Point(188, 232)
        Me.txtAmountInvoiced.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAmountInvoiced.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountInvoiced.Name = "txtAmountInvoiced"
        Me.txtAmountInvoiced.Size = New System.Drawing.Size(71, 13)
        Me.txtAmountInvoiced.TabIndex = 0
        Me.txtAmountInvoiced.Text = "$0"
        Me.txtAmountInvoiced.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPaymentsApplied
        '
        Me.txtPaymentsApplied.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPaymentsApplied.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPaymentsApplied.BackColor = System.Drawing.SystemColors.Control
        Me.txtPaymentsApplied.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPaymentsApplied.Cue = "$0"
        Me.txtPaymentsApplied.Location = New System.Drawing.Point(188, 251)
        Me.txtPaymentsApplied.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPaymentsApplied.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPaymentsApplied.Name = "txtPaymentsApplied"
        Me.txtPaymentsApplied.Size = New System.Drawing.Size(71, 13)
        Me.txtPaymentsApplied.TabIndex = 1
        Me.txtPaymentsApplied.Text = "$0"
        Me.txtPaymentsApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInvoiceBalance
        '
        Me.txtInvoiceBalance.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtInvoiceBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtInvoiceBalance.BackColor = System.Drawing.SystemColors.Control
        Me.txtInvoiceBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInvoiceBalance.Cue = "$0"
        Me.txtInvoiceBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceBalance.Location = New System.Drawing.Point(188, 271)
        Me.txtInvoiceBalance.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtInvoiceBalance.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtInvoiceBalance.Name = "txtInvoiceBalance"
        Me.txtInvoiceBalance.Size = New System.Drawing.Size(71, 13)
        Me.txtInvoiceBalance.TabIndex = 2
        Me.txtInvoiceBalance.Text = "$0"
        Me.txtInvoiceBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 51)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvInvoices)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAmountInvoiced)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInvoiceBalance)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPaymentsApplied)
        Me.SplitContainer1.Panel1.Controls.Add(Me.InvoiceLine)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(574, 612)
        Me.SplitContainer1.SplitterDistance = 306
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddRefund)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCredits)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvCredits)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label11)
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvUnInvoiced)
        Me.SplitContainer2.Size = New System.Drawing.Size(574, 305)
        Me.SplitContainer2.SplitterDistance = 170
        Me.SplitContainer2.TabIndex = 335
        '
        'btnAddRefund
        '
        Me.btnAddRefund.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddRefund.AutoSize = True
        Me.btnAddRefund.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddRefund.Location = New System.Drawing.Point(429, 133)
        Me.btnAddRefund.Name = "btnAddRefund"
        Me.btnAddRefund.Size = New System.Drawing.Size(133, 27)
        Me.btnAddRefund.TabIndex = 339
        Me.btnAddRefund.Text = "Add Refund"
        Me.btnAddRefund.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label6.Location = New System.Drawing.Point(12, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 13)
        Me.Label6.TabIndex = 338
        Me.Label6.Text = "Total Unused Credits"
        '
        'txtCredits
        '
        Me.txtCredits.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCredits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCredits.BackColor = System.Drawing.SystemColors.Control
        Me.txtCredits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCredits.Cue = "$0"
        Me.txtCredits.Location = New System.Drawing.Point(188, 140)
        Me.txtCredits.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtCredits.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCredits.Name = "txtCredits"
        Me.txtCredits.Size = New System.Drawing.Size(71, 13)
        Me.txtCredits.TabIndex = 337
        Me.txtCredits.Text = "$0"
        Me.txtCredits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 336
        Me.Label1.Text = "Unused credits"
        '
        'dgvCredits
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvCredits.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvCredits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCredits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCredits.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvCredits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCredits.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvCredits.LinkifyFirstColumn = True
        Me.dgvCredits.Location = New System.Drawing.Point(12, 24)
        Me.dgvCredits.Name = "dgvCredits"
        Me.dgvCredits.ResultsCountLabel = Nothing
        Me.dgvCredits.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCredits.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvCredits.ShowEditingIcon = False
        Me.dgvCredits.Size = New System.Drawing.Size(550, 103)
        Me.dgvCredits.TabIndex = 335
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(169, 13)
        Me.Label11.TabIndex = 334
        Me.Label11.Text = "Pending billable items not invoiced"
        '
        'dgvUnInvoiced
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvUnInvoiced.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvUnInvoiced.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUnInvoiced.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnInvoiced.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvUnInvoiced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUnInvoiced.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvUnInvoiced.Location = New System.Drawing.Point(12, 16)
        Me.dgvUnInvoiced.Name = "dgvUnInvoiced"
        Me.dgvUnInvoiced.ResultsCountLabel = Nothing
        Me.dgvUnInvoiced.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnInvoiced.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvUnInvoiced.ShowEditingIcon = False
        Me.dgvUnInvoiced.Size = New System.Drawing.Size(550, 100)
        Me.dgvUnInvoiced.TabIndex = 3
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblDataErrorMessage)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.lblFacilityDisplay)
        Me.pnlTop.Controls.Add(Me.btnNewDeposit)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(574, 51)
        Me.pnlTop.TabIndex = 0
        '
        'lblDataErrorMessage
        '
        Me.lblDataErrorMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataErrorMessage.Location = New System.Drawing.Point(444, 11)
        Me.lblDataErrorMessage.Name = "lblDataErrorMessage"
        Me.lblDataErrorMessage.Size = New System.Drawing.Size(118, 27)
        Me.lblDataErrorMessage.TabIndex = 336
        Me.lblDataErrorMessage.Text = "Error loading data"
        Me.lblDataErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefresh.Location = New System.Drawing.Point(12, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 24)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'FinFacilityView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 663)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.pnlTop)
        Me.MinimumSize = New System.Drawing.Size(590, 472)
        Me.Name = "FinFacilityView"
        Me.Text = "Facility Account"
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvUnInvoiced, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblFacilityDisplay As Label
    Friend WithEvents dgvInvoices As IaipDataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnNewDeposit As Button
    Friend WithEvents InvoiceLine As Label
    Friend WithEvents txtAmountInvoiced As CurrencyTextBox
    Friend WithEvents txtPaymentsApplied As CurrencyTextBox
    Friend WithEvents txtInvoiceBalance As CurrencyTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblDataErrorMessage As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents dgvUnInvoiced As IaipDataGridView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnAddRefund As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCredits As CurrencyTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvCredits As IaipDataGridView
End Class
