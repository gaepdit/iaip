<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinRefundView
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
        Me.dtpRefundDate = New System.Windows.Forms.DateTimePicker()
        Me.btnSaveNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txtRefundAmount = New Iaip.CurrencyTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblRefundDisplay = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDepositList = New System.Windows.Forms.Label()
        Me.lblFacility = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvDeposits = New Iaip.IaipDataGridView()
        Me.btnUpdateComment = New System.Windows.Forms.Button()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.txtCredits = New Iaip.CurrencyTextBox()
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpRefundDate
        '
        Me.dtpRefundDate.Checked = False
        Me.dtpRefundDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpRefundDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRefundDate.Location = New System.Drawing.Point(149, 86)
        Me.dtpRefundDate.Name = "dtpRefundDate"
        Me.dtpRefundDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpRefundDate.TabIndex = 1
        '
        'btnSaveNew
        '
        Me.btnSaveNew.AutoSize = True
        Me.btnSaveNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveNew.Location = New System.Drawing.Point(15, 216)
        Me.btnSaveNew.Name = "btnSaveNew"
        Me.btnSaveNew.Size = New System.Drawing.Size(141, 27)
        Me.btnSaveNew.TabIndex = 5
        Me.btnSaveNew.Text = "Save New Refund"
        Me.btnSaveNew.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.AutoSize = True
        Me.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDelete.Location = New System.Drawing.Point(226, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(105, 23)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete this Refund"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'txtRefundAmount
        '
        Me.txtRefundAmount.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtRefundAmount.Cue = "$ 0"
        Me.txtRefundAmount.Location = New System.Drawing.Point(149, 112)
        Me.txtRefundAmount.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtRefundAmount.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtRefundAmount.Name = "txtRefundAmount"
        Me.txtRefundAmount.Size = New System.Drawing.Size(91, 20)
        Me.txtRefundAmount.TabIndex = 2
        Me.txtRefundAmount.Text = "$0"
        Me.txtRefundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Refund Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Comment"
        '
        'lblRefundDisplay
        '
        Me.lblRefundDisplay.AutoSize = True
        Me.lblRefundDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefundDisplay.Location = New System.Drawing.Point(42, 15)
        Me.lblRefundDisplay.Name = "lblRefundDisplay"
        Me.lblRefundDisplay.Size = New System.Drawing.Size(95, 17)
        Me.lblRefundDisplay.TabIndex = 9
        Me.lblRefundDisplay.Text = "New Refund"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(15, 164)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(225, 46)
        Me.txtComment.TabIndex = 3
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblMessage.Location = New System.Drawing.Point(12, 41)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblMessage.Size = New System.Drawing.Size(80, 19)
        Me.lblMessage.TabIndex = 342
        Me.lblMessage.Text = "Error message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Refund Amount"
        '
        'lblDepositList
        '
        Me.lblDepositList.AutoSize = True
        Me.lblDepositList.Location = New System.Drawing.Point(15, 303)
        Me.lblDepositList.Name = "lblDepositList"
        Me.lblDepositList.Size = New System.Drawing.Size(294, 13)
        Me.lblDepositList.TabIndex = 6
        Me.lblDepositList.Text = "Refund amount will be applied to following deposits (in order):"
        '
        'lblFacility
        '
        Me.lblFacility.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFacility.Location = New System.Drawing.Point(15, 70)
        Me.lblFacility.Name = "lblFacility"
        Me.lblFacility.Size = New System.Drawing.Size(316, 13)
        Me.lblFacility.TabIndex = 0
        Me.lblFacility.Text = "Facility Display"
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefresh.Location = New System.Drawing.Point(12, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 24)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dgvDeposits
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvDeposits.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDeposits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDeposits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvDeposits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDeposits.LinkifyColumnByName = "DepositId"
        Me.dgvDeposits.Location = New System.Drawing.Point(12, 319)
        Me.dgvDeposits.Name = "dgvDeposits"
        Me.dgvDeposits.ResultsCountLabel = Nothing
        Me.dgvDeposits.ResultsCountLabelFormat = "{0} found"
        Me.dgvDeposits.Size = New System.Drawing.Size(319, 163)
        Me.dgvDeposits.StandardTab = True
        Me.dgvDeposits.TabIndex = 9
        '
        'btnUpdateComment
        '
        Me.btnUpdateComment.AutoSize = True
        Me.btnUpdateComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateComment.Location = New System.Drawing.Point(15, 216)
        Me.btnUpdateComment.Name = "btnUpdateComment"
        Me.btnUpdateComment.Size = New System.Drawing.Size(129, 27)
        Me.btnUpdateComment.TabIndex = 4
        Me.btnUpdateComment.Text = "Update Comment"
        Me.btnUpdateComment.UseVisualStyleBackColor = True
        '
        'lblCredits
        '
        Me.lblCredits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCredits.AutoSize = True
        Me.lblCredits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCredits.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblCredits.Location = New System.Drawing.Point(15, 268)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(126, 13)
        Me.lblCredits.TabIndex = 345
        Me.lblCredits.Text = "Total Unused Credits"
        '
        'txtCredits
        '
        Me.txtCredits.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCredits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCredits.BackColor = System.Drawing.SystemColors.Control
        Me.txtCredits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCredits.Cue = "$0"
        Me.txtCredits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredits.Location = New System.Drawing.Point(169, 268)
        Me.txtCredits.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtCredits.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCredits.Name = "txtCredits"
        Me.txtCredits.Size = New System.Drawing.Size(71, 13)
        Me.txtCredits.TabIndex = 6
        Me.txtCredits.Text = "$0"
        Me.txtCredits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FinRefundView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 494)
        Me.Controls.Add(Me.lblCredits)
        Me.Controls.Add(Me.txtCredits)
        Me.Controls.Add(Me.dgvDeposits)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.lblRefundDisplay)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.txtRefundAmount)
        Me.Controls.Add(Me.dtpRefundDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDepositList)
        Me.Controls.Add(Me.lblFacility)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnUpdateComment)
        Me.Controls.Add(Me.btnSaveNew)
        Me.MinimumSize = New System.Drawing.Size(359, 357)
        Me.Name = "FinRefundView"
        Me.Text = "New Refund"
        CType(Me.dgvDeposits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpRefundDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSaveNew As Button
    Friend WithEvents lblRefundDisplay As Label
    Friend WithEvents txtRefundAmount As CurrencyTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnDelete As Button
    Friend WithEvents txtComment As TextBox
    Friend WithEvents lblMessage As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblDepositList As Label
    Friend WithEvents lblFacility As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvDeposits As IaipDataGridView
    Friend WithEvents btnUpdateComment As Button
    Friend WithEvents lblCredits As Label
    Friend WithEvents txtCredits As CurrencyTextBox
End Class
