<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinInvoiceView
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblInvoiceID = New System.Windows.Forms.Label()
        Me.txtInvoiceDate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnVoid = New System.Windows.Forms.Button()
        Me.btnNewDeposit = New System.Windows.Forms.Button()
        Me.lnkViewInvoice = New System.Windows.Forms.LinkLabel()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnSaveComment = New System.Windows.Forms.Button()
        Me.dgvPaymentsApplied = New Iaip.IaipDataGridView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTotalDue = New Iaip.CurrencyTextBox()
        Me.txtAmountPaid = New Iaip.CurrencyTextBox()
        Me.txtCurrentBalance = New Iaip.CurrencyTextBox()
        Me.InvoiceLine = New System.Windows.Forms.Label()
        Me.lblInvoiceDescription = New System.Windows.Forms.Label()
        Me.lblSaveCommentMessage = New System.Windows.Forms.Label()
        Me.lblVoidMessage = New System.Windows.Forms.Label()
        Me.dgvInvoiceItems = New Iaip.IaipDataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblFacilityDisplay = New System.Windows.Forms.Label()
        Me.UrlToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgvPaymentsApplied, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvInvoiceItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblInvoiceID
        '
        Me.lblInvoiceID.AutoSize = True
        Me.lblInvoiceID.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceID.Location = New System.Drawing.Point(42, 15)
        Me.lblInvoiceID.Name = "lblInvoiceID"
        Me.lblInvoiceID.Size = New System.Drawing.Size(97, 17)
        Me.lblInvoiceID.TabIndex = 0
        Me.lblInvoiceID.Text = "Invoice View"
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInvoiceDate.Location = New System.Drawing.Point(101, 167)
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.ReadOnly = True
        Me.txtInvoiceDate.Size = New System.Drawing.Size(100, 13)
        Me.txtInvoiceDate.TabIndex = 7
        Me.txtInvoiceDate.Text = "N/A"
        Me.txtInvoiceDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(12, 167)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Invoice Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Location = New System.Drawing.Point(12, 205)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Total Due"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Location = New System.Drawing.Point(12, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Total Paid"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label6.Location = New System.Drawing.Point(12, 244)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Current Balance"
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(12, 45)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(347, 20)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "Status"
        '
        'btnVoid
        '
        Me.btnVoid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnVoid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnVoid.Location = New System.Drawing.Point(365, 12)
        Me.btnVoid.Name = "btnVoid"
        Me.btnVoid.Size = New System.Drawing.Size(101, 23)
        Me.btnVoid.TabIndex = 6
        Me.btnVoid.Text = "Void This Invoice"
        Me.btnVoid.UseVisualStyleBackColor = True
        '
        'btnNewDeposit
        '
        Me.btnNewDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewDeposit.AutoSize = True
        Me.btnNewDeposit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewDeposit.Location = New System.Drawing.Point(333, 554)
        Me.btnNewDeposit.Name = "btnNewDeposit"
        Me.btnNewDeposit.Size = New System.Drawing.Size(133, 27)
        Me.btnNewDeposit.TabIndex = 5
        Me.btnNewDeposit.Text = "Add New Deposit"
        Me.btnNewDeposit.UseVisualStyleBackColor = True
        Me.btnNewDeposit.Visible = False
        '
        'lnkViewInvoice
        '
        Me.lnkViewInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lnkViewInvoice.AutoSize = True
        Me.lnkViewInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkViewInvoice.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lnkViewInvoice.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkViewInvoice.Location = New System.Drawing.Point(12, 559)
        Me.lnkViewInvoice.Name = "lnkViewInvoice"
        Me.lnkViewInvoice.Size = New System.Drawing.Size(144, 17)
        Me.lnkViewInvoice.TabIndex = 4
        Me.lnkViewInvoice.TabStop = True
        Me.lnkViewInvoice.Text = "View printable invoice"
        Me.lnkViewInvoice.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtComments
        '
        Me.txtComments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComments.Location = New System.Drawing.Point(263, 125)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(203, 73)
        Me.txtComments.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(260, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Comments:"
        '
        'btnSaveComment
        '
        Me.btnSaveComment.AutoSize = True
        Me.btnSaveComment.Location = New System.Drawing.Point(263, 204)
        Me.btnSaveComment.Name = "btnSaveComment"
        Me.btnSaveComment.Size = New System.Drawing.Size(94, 27)
        Me.btnSaveComment.TabIndex = 1
        Me.btnSaveComment.Text = "Save Comments"
        Me.btnSaveComment.UseVisualStyleBackColor = True
        '
        'dgvPaymentsApplied
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvPaymentsApplied.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPaymentsApplied.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPaymentsApplied.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvPaymentsApplied.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaymentsApplied.LinkifyColumnByName = Nothing
        Me.dgvPaymentsApplied.LinkifyFirstColumn = True
        Me.dgvPaymentsApplied.Location = New System.Drawing.Point(12, 434)
        Me.dgvPaymentsApplied.Name = "dgvPaymentsApplied"
        Me.dgvPaymentsApplied.ResultsCountLabel = Nothing
        Me.dgvPaymentsApplied.ResultsCountLabelFormat = "{0} found"
        Me.dgvPaymentsApplied.ShowEditingIcon = False
        Me.dgvPaymentsApplied.Size = New System.Drawing.Size(454, 101)
        Me.dgvPaymentsApplied.StandardTab = True
        Me.dgvPaymentsApplied.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 418)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Payments applied:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 83)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Invoice for:"
        '
        'txtTotalDue
        '
        Me.txtTotalDue.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtTotalDue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalDue.Cue = "$0"
        Me.txtTotalDue.Location = New System.Drawing.Point(101, 205)
        Me.txtTotalDue.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalDue.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalDue.Name = "txtTotalDue"
        Me.txtTotalDue.ReadOnly = True
        Me.txtTotalDue.Size = New System.Drawing.Size(100, 13)
        Me.txtTotalDue.TabIndex = 8
        Me.txtTotalDue.Text = "$0"
        Me.txtTotalDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmountPaid.Cue = "$0"
        Me.txtAmountPaid.Location = New System.Drawing.Point(101, 224)
        Me.txtAmountPaid.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAmountPaid.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.ReadOnly = True
        Me.txtAmountPaid.Size = New System.Drawing.Size(100, 13)
        Me.txtAmountPaid.TabIndex = 9
        Me.txtAmountPaid.Text = "$0"
        Me.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCurrentBalance
        '
        Me.txtCurrentBalance.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCurrentBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCurrentBalance.Cue = "$0"
        Me.txtCurrentBalance.Location = New System.Drawing.Point(101, 244)
        Me.txtCurrentBalance.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtCurrentBalance.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtCurrentBalance.Name = "txtCurrentBalance"
        Me.txtCurrentBalance.ReadOnly = True
        Me.txtCurrentBalance.Size = New System.Drawing.Size(100, 13)
        Me.txtCurrentBalance.TabIndex = 10
        Me.txtCurrentBalance.Text = "$0"
        Me.txtCurrentBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'InvoiceLine
        '
        Me.InvoiceLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InvoiceLine.Location = New System.Drawing.Point(130, 240)
        Me.InvoiceLine.Name = "InvoiceLine"
        Me.InvoiceLine.Size = New System.Drawing.Size(71, 1)
        Me.InvoiceLine.TabIndex = 340
        '
        'lblInvoiceDescription
        '
        Me.lblInvoiceDescription.AutoSize = True
        Me.lblInvoiceDescription.Location = New System.Drawing.Point(12, 96)
        Me.lblInvoiceDescription.Name = "lblInvoiceDescription"
        Me.lblInvoiceDescription.Size = New System.Drawing.Size(146, 13)
        Me.lblInvoiceDescription.TabIndex = 3
        Me.lblInvoiceDescription.Text = "Emissions Fees or Application"
        '
        'lblSaveCommentMessage
        '
        Me.lblSaveCommentMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblSaveCommentMessage.Location = New System.Drawing.Point(263, 239)
        Me.lblSaveCommentMessage.Name = "lblSaveCommentMessage"
        Me.lblSaveCommentMessage.Size = New System.Drawing.Size(122, 23)
        Me.lblSaveCommentMessage.TabIndex = 341
        Me.lblSaveCommentMessage.Text = "Error saving comment."
        Me.lblSaveCommentMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVoidMessage
        '
        Me.lblVoidMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVoidMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblVoidMessage.Location = New System.Drawing.Point(298, 68)
        Me.lblVoidMessage.Name = "lblVoidMessage"
        Me.lblVoidMessage.Size = New System.Drawing.Size(168, 28)
        Me.lblVoidMessage.TabIndex = 15
        Me.lblVoidMessage.Text = "Cannot void invoice because payments have been applied."
        Me.lblVoidMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvInvoiceItems
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvInvoiceItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInvoiceItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInvoiceItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvInvoiceItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.NullValue = "N/A"
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInvoiceItems.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvInvoiceItems.LinkifyColumnByName = Nothing
        Me.dgvInvoiceItems.Location = New System.Drawing.Point(12, 289)
        Me.dgvInvoiceItems.Name = "dgvInvoiceItems"
        Me.dgvInvoiceItems.ResultsCountLabel = Nothing
        Me.dgvInvoiceItems.ResultsCountLabelFormat = "{0} found"
        Me.dgvInvoiceItems.ShowEditingIcon = False
        Me.dgvInvoiceItems.Size = New System.Drawing.Size(454, 113)
        Me.dgvInvoiceItems.StandardTab = True
        Me.dgvInvoiceItems.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 273)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Invoice Items:"
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefresh.Location = New System.Drawing.Point(12, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 24)
        Me.btnRefresh.TabIndex = 342
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblFacilityDisplay
        '
        Me.lblFacilityDisplay.AutoSize = True
        Me.lblFacilityDisplay.Location = New System.Drawing.Point(12, 128)
        Me.lblFacilityDisplay.Name = "lblFacilityDisplay"
        Me.lblFacilityDisplay.Size = New System.Drawing.Size(70, 26)
        Me.lblFacilityDisplay.TabIndex = 3
        Me.lblFacilityDisplay.Text = "000-00000" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Facility Name"
        '
        'FinInvoiceView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 593)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lblSaveCommentMessage)
        Me.Controls.Add(Me.InvoiceLine)
        Me.Controls.Add(Me.txtCurrentBalance)
        Me.Controls.Add(Me.txtAmountPaid)
        Me.Controls.Add(Me.txtTotalDue)
        Me.Controls.Add(Me.btnSaveComment)
        Me.Controls.Add(Me.dgvInvoiceItems)
        Me.Controls.Add(Me.dgvPaymentsApplied)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.lblVoidMessage)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lnkViewInvoice)
        Me.Controls.Add(Me.btnNewDeposit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblFacilityDisplay)
        Me.Controls.Add(Me.lblInvoiceDescription)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnVoid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtInvoiceDate)
        Me.Controls.Add(Me.lblInvoiceID)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Label4)
        Me.MinimumSize = New System.Drawing.Size(426, 471)
        Me.Name = "FinInvoiceView"
        Me.Text = "Invoice"
        CType(Me.dgvPaymentsApplied, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvInvoiceItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInvoiceID As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtInvoiceDate As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnVoid As Button
    Friend WithEvents btnNewDeposit As Button
    Friend WithEvents lnkViewInvoice As LinkLabel
    Friend WithEvents txtComments As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnSaveComment As Button
    Friend WithEvents dgvPaymentsApplied As IaipDataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtTotalDue As CurrencyTextBox
    Friend WithEvents txtAmountPaid As CurrencyTextBox
    Friend WithEvents txtCurrentBalance As CurrencyTextBox
    Friend WithEvents InvoiceLine As Label
    Friend WithEvents lblInvoiceDescription As Label
    Friend WithEvents lblSaveCommentMessage As Label
    Friend WithEvents lblVoidMessage As Label
    Friend WithEvents dgvInvoiceItems As IaipDataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblFacilityDisplay As Label
    Friend WithEvents UrlToolTip As ToolTip
End Class
