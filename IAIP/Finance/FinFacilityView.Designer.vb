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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblFacilityDisplay = New System.Windows.Forms.Label()
        Me.dgvInvoices = New Iaip.IaipDataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.InvoiceLine = New System.Windows.Forms.Label()
        Me.txtAmountInvoiced = New Iaip.CurrencyTextBox()
        Me.txtPaymentsApplied = New Iaip.CurrencyTextBox()
        Me.txtInvoiceBalance = New Iaip.CurrencyTextBox()
        Me.SplitInvoicesAndPending = New System.Windows.Forms.SplitContainer()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPending = New Iaip.CurrencyTextBox()
        Me.dgvPending = New Iaip.IaipDataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.SplitCreditsAndRefunds = New System.Windows.Forms.SplitContainer()
        Me.btnAddRefund = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCredits = New Iaip.CurrencyTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvCredits = New Iaip.IaipDataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtRefunds = New Iaip.CurrencyTextBox()
        Me.dgvRefunds = New Iaip.IaipDataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblDataErrorMessage = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnOpenFacilitySummary = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpDeposits = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDeposits = New Iaip.CurrencyTextBox()
        Me.dgvDeposits = New Iaip.IaipDataGridView()
        Me.tpInvoices = New System.Windows.Forms.TabPage()
        Me.tpRefunds = New System.Windows.Forms.TabPage()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitInvoicesAndPending, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitInvoicesAndPending.Panel1.SuspendLayout()
        Me.SplitInvoicesAndPending.Panel2.SuspendLayout()
        Me.SplitInvoicesAndPending.SuspendLayout()
        CType(Me.dgvPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitCreditsAndRefunds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitCreditsAndRefunds.Panel1.SuspendLayout()
        Me.SplitCreditsAndRefunds.Panel2.SuspendLayout()
        Me.SplitCreditsAndRefunds.SuspendLayout()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRefunds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpDeposits.SuspendLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpInvoices.SuspendLayout()
        Me.tpRefunds.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblFacilityDisplay
        '
        Me.lblFacilityDisplay.AutoSize = True
        Me.lblFacilityDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityDisplay.Location = New System.Drawing.Point(42, 16)
        Me.lblFacilityDisplay.Name = "lblFacilityDisplay"
        Me.lblFacilityDisplay.Size = New System.Drawing.Size(188, 17)
        Me.lblFacilityDisplay.TabIndex = 1
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
        Me.dgvInvoices.LinkifyColumnByName = Nothing
        Me.dgvInvoices.LinkifyFirstColumn = True
        Me.dgvInvoices.Location = New System.Drawing.Point(12, 28)
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
        Me.dgvInvoices.Size = New System.Drawing.Size(430, 467)
        Me.dgvInvoices.StandardTab = True
        Me.dgvInvoices.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 12)
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
        Me.Label5.Location = New System.Drawing.Point(12, 522)
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
        Me.Label2.Location = New System.Drawing.Point(12, 541)
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
        Me.Label3.Location = New System.Drawing.Point(12, 561)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 13)
        Me.Label3.TabIndex = 334
        Me.Label3.Text = "Current Balance On Invoices"
        '
        'InvoiceLine
        '
        Me.InvoiceLine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.InvoiceLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InvoiceLine.Location = New System.Drawing.Point(188, 557)
        Me.InvoiceLine.Name = "InvoiceLine"
        Me.InvoiceLine.Size = New System.Drawing.Size(84, 1)
        Me.InvoiceLine.TabIndex = 339
        '
        'txtAmountInvoiced
        '
        Me.txtAmountInvoiced.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountInvoiced.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAmountInvoiced.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtAmountInvoiced.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmountInvoiced.Cue = "$0"
        Me.txtAmountInvoiced.Location = New System.Drawing.Point(188, 522)
        Me.txtAmountInvoiced.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAmountInvoiced.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtAmountInvoiced.Name = "txtAmountInvoiced"
        Me.txtAmountInvoiced.Size = New System.Drawing.Size(84, 13)
        Me.txtAmountInvoiced.TabIndex = 1
        Me.txtAmountInvoiced.Text = "$0"
        Me.txtAmountInvoiced.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPaymentsApplied
        '
        Me.txtPaymentsApplied.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPaymentsApplied.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPaymentsApplied.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPaymentsApplied.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPaymentsApplied.Cue = "$0"
        Me.txtPaymentsApplied.Location = New System.Drawing.Point(188, 541)
        Me.txtPaymentsApplied.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPaymentsApplied.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPaymentsApplied.Name = "txtPaymentsApplied"
        Me.txtPaymentsApplied.Size = New System.Drawing.Size(84, 13)
        Me.txtPaymentsApplied.TabIndex = 2
        Me.txtPaymentsApplied.Text = "$0"
        Me.txtPaymentsApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInvoiceBalance
        '
        Me.txtInvoiceBalance.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtInvoiceBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtInvoiceBalance.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtInvoiceBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInvoiceBalance.Cue = "$0"
        Me.txtInvoiceBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceBalance.Location = New System.Drawing.Point(188, 561)
        Me.txtInvoiceBalance.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtInvoiceBalance.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtInvoiceBalance.Name = "txtInvoiceBalance"
        Me.txtInvoiceBalance.Size = New System.Drawing.Size(84, 13)
        Me.txtInvoiceBalance.TabIndex = 3
        Me.txtInvoiceBalance.Text = "$0"
        Me.txtInvoiceBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SplitInvoicesAndPending
        '
        Me.SplitInvoicesAndPending.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SplitInvoicesAndPending.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitInvoicesAndPending.Location = New System.Drawing.Point(3, 3)
        Me.SplitInvoicesAndPending.Name = "SplitInvoicesAndPending"
        '
        'SplitInvoicesAndPending.Panel1
        '
        Me.SplitInvoicesAndPending.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.Label4)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.Label5)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.InvoiceLine)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.Label2)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.txtPaymentsApplied)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.dgvInvoices)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.txtInvoiceBalance)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.txtAmountInvoiced)
        Me.SplitInvoicesAndPending.Panel1.Controls.Add(Me.Label3)
        Me.SplitInvoicesAndPending.Panel1MinSize = 400
        '
        'SplitInvoicesAndPending.Panel2
        '
        Me.SplitInvoicesAndPending.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.SplitInvoicesAndPending.Panel2.Controls.Add(Me.Label8)
        Me.SplitInvoicesAndPending.Panel2.Controls.Add(Me.txtPending)
        Me.SplitInvoicesAndPending.Panel2.Controls.Add(Me.dgvPending)
        Me.SplitInvoicesAndPending.Panel2.Controls.Add(Me.Label11)
        Me.SplitInvoicesAndPending.Panel2MinSize = 100
        Me.SplitInvoicesAndPending.Size = New System.Drawing.Size(859, 596)
        Me.SplitInvoicesAndPending.SplitterDistance = 457
        Me.SplitInvoicesAndPending.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label8.Location = New System.Drawing.Point(15, 561)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 13)
        Me.Label8.TabIndex = 340
        Me.Label8.Text = "Total Pending Items"
        '
        'txtPending
        '
        Me.txtPending.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPending.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPending.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPending.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPending.Cue = "$0"
        Me.txtPending.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPending.Location = New System.Drawing.Point(191, 561)
        Me.txtPending.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPending.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPending.Name = "txtPending"
        Me.txtPending.Size = New System.Drawing.Size(84, 13)
        Me.txtPending.TabIndex = 1
        Me.txtPending.Text = "$0"
        Me.txtPending.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvPending
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvPending.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPending.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPending.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPending.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPending.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvPending.LinkifyColumnByName = Nothing
        Me.dgvPending.Location = New System.Drawing.Point(15, 28)
        Me.dgvPending.Name = "dgvPending"
        Me.dgvPending.ResultsCountLabel = Nothing
        Me.dgvPending.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPending.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvPending.ShowEditingIcon = False
        Me.dgvPending.Size = New System.Drawing.Size(371, 467)
        Me.dgvPending.StandardTab = True
        Me.dgvPending.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(169, 13)
        Me.Label11.TabIndex = 334
        Me.Label11.Text = "Pending billable items not invoiced"
        '
        'SplitCreditsAndRefunds
        '
        Me.SplitCreditsAndRefunds.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SplitCreditsAndRefunds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitCreditsAndRefunds.Location = New System.Drawing.Point(3, 3)
        Me.SplitCreditsAndRefunds.Name = "SplitCreditsAndRefunds"
        '
        'SplitCreditsAndRefunds.Panel1
        '
        Me.SplitCreditsAndRefunds.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.SplitCreditsAndRefunds.Panel1.Controls.Add(Me.btnAddRefund)
        Me.SplitCreditsAndRefunds.Panel1.Controls.Add(Me.Label6)
        Me.SplitCreditsAndRefunds.Panel1.Controls.Add(Me.txtCredits)
        Me.SplitCreditsAndRefunds.Panel1.Controls.Add(Me.Label1)
        Me.SplitCreditsAndRefunds.Panel1.Controls.Add(Me.dgvCredits)
        Me.SplitCreditsAndRefunds.Panel1MinSize = 400
        '
        'SplitCreditsAndRefunds.Panel2
        '
        Me.SplitCreditsAndRefunds.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.SplitCreditsAndRefunds.Panel2.Controls.Add(Me.Label9)
        Me.SplitCreditsAndRefunds.Panel2.Controls.Add(Me.txtRefunds)
        Me.SplitCreditsAndRefunds.Panel2.Controls.Add(Me.dgvRefunds)
        Me.SplitCreditsAndRefunds.Panel2.Controls.Add(Me.Label7)
        Me.SplitCreditsAndRefunds.Panel2MinSize = 100
        Me.SplitCreditsAndRefunds.Size = New System.Drawing.Size(859, 596)
        Me.SplitCreditsAndRefunds.SplitterDistance = 457
        Me.SplitCreditsAndRefunds.TabIndex = 0
        '
        'btnAddRefund
        '
        Me.btnAddRefund.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddRefund.AutoSize = True
        Me.btnAddRefund.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddRefund.Location = New System.Drawing.Point(329, 554)
        Me.btnAddRefund.Name = "btnAddRefund"
        Me.btnAddRefund.Size = New System.Drawing.Size(113, 27)
        Me.btnAddRefund.TabIndex = 2
        Me.btnAddRefund.Text = "Add Refund"
        Me.btnAddRefund.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label6.Location = New System.Drawing.Point(12, 561)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 13)
        Me.Label6.TabIndex = 338
        Me.Label6.Text = "Total Unused Credits"
        '
        'txtCredits
        '
        Me.txtCredits.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCredits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCredits.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtCredits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCredits.Cue = "$0"
        Me.txtCredits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredits.Location = New System.Drawing.Point(175, 561)
        Me.txtCredits.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtCredits.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtCredits.Name = "txtCredits"
        Me.txtCredits.Size = New System.Drawing.Size(84, 13)
        Me.txtCredits.TabIndex = 1
        Me.txtCredits.Text = "$0"
        Me.txtCredits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 336
        Me.Label1.Text = "Unused credits"
        '
        'dgvCredits
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvCredits.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvCredits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCredits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCredits.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvCredits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCredits.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvCredits.LinkifyColumnByName = Nothing
        Me.dgvCredits.LinkifyFirstColumn = True
        Me.dgvCredits.Location = New System.Drawing.Point(12, 28)
        Me.dgvCredits.Name = "dgvCredits"
        Me.dgvCredits.ResultsCountLabel = Nothing
        Me.dgvCredits.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCredits.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvCredits.ShowEditingIcon = False
        Me.dgvCredits.Size = New System.Drawing.Size(430, 512)
        Me.dgvCredits.StandardTab = True
        Me.dgvCredits.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label9.Location = New System.Drawing.Point(15, 561)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Total Refunds Issued"
        '
        'txtRefunds
        '
        Me.txtRefunds.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtRefunds.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRefunds.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtRefunds.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRefunds.Cue = "$0"
        Me.txtRefunds.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefunds.Location = New System.Drawing.Point(178, 561)
        Me.txtRefunds.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtRefunds.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtRefunds.Name = "txtRefunds"
        Me.txtRefunds.Size = New System.Drawing.Size(84, 13)
        Me.txtRefunds.TabIndex = 2
        Me.txtRefunds.Text = "$0"
        Me.txtRefunds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvRefunds
        '
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRefunds.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvRefunds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRefunds.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRefunds.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.dgvRefunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRefunds.DefaultCellStyle = DataGridViewCellStyle15
        Me.dgvRefunds.LinkifyColumnByName = Nothing
        Me.dgvRefunds.LinkifyFirstColumn = True
        Me.dgvRefunds.Location = New System.Drawing.Point(15, 28)
        Me.dgvRefunds.Name = "dgvRefunds"
        Me.dgvRefunds.ResultsCountLabel = Nothing
        Me.dgvRefunds.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRefunds.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvRefunds.ShowEditingIcon = False
        Me.dgvRefunds.Size = New System.Drawing.Size(371, 512)
        Me.dgvRefunds.StandardTab = True
        Me.dgvRefunds.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 334
        Me.Label7.Text = "Refunds issued"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblDataErrorMessage)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.lblFacilityDisplay)
        Me.pnlTop.Controls.Add(Me.btnOpenFacilitySummary)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(873, 51)
        Me.pnlTop.TabIndex = 0
        '
        'lblDataErrorMessage
        '
        Me.lblDataErrorMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataErrorMessage.Location = New System.Drawing.Point(743, 11)
        Me.lblDataErrorMessage.Name = "lblDataErrorMessage"
        Me.lblDataErrorMessage.Size = New System.Drawing.Size(118, 27)
        Me.lblDataErrorMessage.TabIndex = 3
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
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnOpenFacilitySummary
        '
        Me.btnOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenFacilitySummary.Location = New System.Drawing.Point(728, 13)
        Me.btnOpenFacilitySummary.Name = "btnOpenFacilitySummary"
        Me.btnOpenFacilitySummary.Size = New System.Drawing.Size(133, 23)
        Me.btnOpenFacilitySummary.TabIndex = 2
        Me.btnOpenFacilitySummary.Text = "Open Facility Summary"
        Me.btnOpenFacilitySummary.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpDeposits)
        Me.TabControl1.Controls.Add(Me.tpInvoices)
        Me.TabControl1.Controls.Add(Me.tpRefunds)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 51)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(873, 628)
        Me.TabControl1.TabIndex = 1
        '
        'tpDeposits
        '
        Me.tpDeposits.Controls.Add(Me.Label10)
        Me.tpDeposits.Controls.Add(Me.txtDeposits)
        Me.tpDeposits.Controls.Add(Me.dgvDeposits)
        Me.tpDeposits.Location = New System.Drawing.Point(4, 22)
        Me.tpDeposits.Name = "tpDeposits"
        Me.tpDeposits.Size = New System.Drawing.Size(865, 602)
        Me.tpDeposits.TabIndex = 2
        Me.tpDeposits.Text = "Deposits"
        Me.tpDeposits.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label10.Location = New System.Drawing.Point(12, 561)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 342
        Me.Label10.Text = "Total Deposits"
        '
        'txtDeposits
        '
        Me.txtDeposits.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDeposits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDeposits.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDeposits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDeposits.Cue = "$0"
        Me.txtDeposits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeposits.Location = New System.Drawing.Point(188, 561)
        Me.txtDeposits.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDeposits.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtDeposits.Name = "txtDeposits"
        Me.txtDeposits.Size = New System.Drawing.Size(84, 13)
        Me.txtDeposits.TabIndex = 341
        Me.txtDeposits.Text = "$0"
        Me.txtDeposits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvDeposits
        '
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvDeposits.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle17
        Me.dgvDeposits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDeposits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDeposits.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgvDeposits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDeposits.DefaultCellStyle = DataGridViewCellStyle19
        Me.dgvDeposits.LinkifyColumnByName = Nothing
        Me.dgvDeposits.LinkifyFirstColumn = True
        Me.dgvDeposits.Location = New System.Drawing.Point(12, 28)
        Me.dgvDeposits.Name = "dgvDeposits"
        Me.dgvDeposits.ResultsCountLabel = Nothing
        Me.dgvDeposits.ResultsCountLabelFormat = "{0} found"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDeposits.RowHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.dgvDeposits.ShowEditingIcon = False
        Me.dgvDeposits.Size = New System.Drawing.Size(845, 508)
        Me.dgvDeposits.StandardTab = True
        Me.dgvDeposits.TabIndex = 337
        '
        'tpInvoices
        '
        Me.tpInvoices.Controls.Add(Me.SplitInvoicesAndPending)
        Me.tpInvoices.Location = New System.Drawing.Point(4, 22)
        Me.tpInvoices.Name = "tpInvoices"
        Me.tpInvoices.Padding = New System.Windows.Forms.Padding(3)
        Me.tpInvoices.Size = New System.Drawing.Size(865, 602)
        Me.tpInvoices.TabIndex = 0
        Me.tpInvoices.Text = "Invoices"
        Me.tpInvoices.UseVisualStyleBackColor = True
        '
        'tpRefunds
        '
        Me.tpRefunds.Controls.Add(Me.SplitCreditsAndRefunds)
        Me.tpRefunds.Location = New System.Drawing.Point(4, 22)
        Me.tpRefunds.Name = "tpRefunds"
        Me.tpRefunds.Padding = New System.Windows.Forms.Padding(3)
        Me.tpRefunds.Size = New System.Drawing.Size(865, 602)
        Me.tpRefunds.TabIndex = 1
        Me.tpRefunds.Text = "Credits & Refunds"
        Me.tpRefunds.UseVisualStyleBackColor = True
        '
        'FinFacilityView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(873, 679)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pnlTop)
        Me.MinimumSize = New System.Drawing.Size(590, 593)
        Me.Name = "FinFacilityView"
        Me.Text = "Application Fee Facility Account"
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitInvoicesAndPending.Panel1.ResumeLayout(False)
        Me.SplitInvoicesAndPending.Panel1.PerformLayout()
        Me.SplitInvoicesAndPending.Panel2.ResumeLayout(False)
        Me.SplitInvoicesAndPending.Panel2.PerformLayout()
        CType(Me.SplitInvoicesAndPending, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitInvoicesAndPending.ResumeLayout(False)
        CType(Me.dgvPending, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitCreditsAndRefunds.Panel1.ResumeLayout(False)
        Me.SplitCreditsAndRefunds.Panel1.PerformLayout()
        Me.SplitCreditsAndRefunds.Panel2.ResumeLayout(False)
        Me.SplitCreditsAndRefunds.Panel2.PerformLayout()
        CType(Me.SplitCreditsAndRefunds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitCreditsAndRefunds.ResumeLayout(False)
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRefunds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tpDeposits.ResumeLayout(False)
        Me.tpDeposits.PerformLayout()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpInvoices.ResumeLayout(False)
        Me.tpRefunds.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblFacilityDisplay As Label
    Friend WithEvents dgvInvoices As IaipDataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents InvoiceLine As Label
    Friend WithEvents txtAmountInvoiced As CurrencyTextBox
    Friend WithEvents txtPaymentsApplied As CurrencyTextBox
    Friend WithEvents txtInvoiceBalance As CurrencyTextBox
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblDataErrorMessage As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents dgvPending As IaipDataGridView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents SplitCreditsAndRefunds As SplitContainer
    Friend WithEvents btnAddRefund As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCredits As CurrencyTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvCredits As IaipDataGridView
    Friend WithEvents SplitInvoicesAndPending As SplitContainer
    Friend WithEvents dgvRefunds As IaipDataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtPending As CurrencyTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtRefunds As CurrencyTextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tpDeposits As TabPage
    Friend WithEvents Label10 As Label
    Friend WithEvents txtDeposits As CurrencyTextBox
    Friend WithEvents dgvDeposits As IaipDataGridView
    Friend WithEvents tpInvoices As TabPage
    Friend WithEvents tpRefunds As TabPage
    Friend WithEvents btnOpenFacilitySummary As Button
End Class
