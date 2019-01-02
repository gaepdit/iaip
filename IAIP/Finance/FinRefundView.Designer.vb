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
        Me.dtpRefundDate = New System.Windows.Forms.DateTimePicker()
        Me.btnSaveNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.txtRefundAmount = New Iaip.CurrencyTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblRefundID = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblSaveMessage = New System.Windows.Forms.Label()
        Me.lblErrorMessage = New System.Windows.Forms.Label()
        Me.txtDepositBalance = New Iaip.CurrencyTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDepositDisplay = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'dtpRefundDate
        '
        Me.dtpRefundDate.Checked = False
        Me.dtpRefundDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpRefundDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRefundDate.Location = New System.Drawing.Point(149, 119)
        Me.dtpRefundDate.Name = "dtpRefundDate"
        Me.dtpRefundDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpRefundDate.TabIndex = 1
        '
        'btnSaveNew
        '
        Me.btnSaveNew.AutoSize = True
        Me.btnSaveNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveNew.Location = New System.Drawing.Point(15, 268)
        Me.btnSaveNew.Name = "btnSaveNew"
        Me.btnSaveNew.Size = New System.Drawing.Size(133, 27)
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
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete this Refund"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.AutoSize = True
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(15, 268)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(114, 27)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Update Refund"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'txtRefundAmount
        '
        Me.txtRefundAmount.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtRefundAmount.Cue = "$ 0"
        Me.txtRefundAmount.Location = New System.Drawing.Point(149, 145)
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
        Me.Label2.Location = New System.Drawing.Point(12, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Refund Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Amount"
        '
        'lblRefundID
        '
        Me.lblRefundID.AutoSize = True
        Me.lblRefundID.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefundID.Location = New System.Drawing.Point(42, 15)
        Me.lblRefundID.Name = "lblRefundID"
        Me.lblRefundID.Size = New System.Drawing.Size(95, 17)
        Me.lblRefundID.TabIndex = 9
        Me.lblRefundID.Text = "New Refund"
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(15, 190)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(225, 46)
        Me.txtComments.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 174)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 335
        Me.Label7.Text = "Comments"
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefresh.Location = New System.Drawing.Point(12, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 24)
        Me.btnRefresh.TabIndex = 337
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblSaveMessage
        '
        Me.lblSaveMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblSaveMessage.Location = New System.Drawing.Point(154, 270)
        Me.lblSaveMessage.Name = "lblSaveMessage"
        Me.lblSaveMessage.Size = New System.Drawing.Size(122, 23)
        Me.lblSaveMessage.TabIndex = 342
        Me.lblSaveMessage.Text = "Refund save message."
        Me.lblSaveMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblErrorMessage
        '
        Me.lblErrorMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblErrorMessage.Location = New System.Drawing.Point(209, 43)
        Me.lblErrorMessage.Name = "lblErrorMessage"
        Me.lblErrorMessage.Size = New System.Drawing.Size(122, 23)
        Me.lblErrorMessage.TabIndex = 342
        Me.lblErrorMessage.Text = "Refund delete message."
        Me.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDepositBalance
        '
        Me.txtDepositBalance.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDepositBalance.BackColor = System.Drawing.SystemColors.Control
        Me.txtDepositBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDepositBalance.Cue = "$ 0"
        Me.txtDepositBalance.Location = New System.Drawing.Point(146, 87)
        Me.txtDepositBalance.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDepositBalance.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDepositBalance.Name = "txtDepositBalance"
        Me.txtDepositBalance.ReadOnly = True
        Me.txtDepositBalance.Size = New System.Drawing.Size(91, 13)
        Me.txtDepositBalance.TabIndex = 2
        Me.txtDepositBalance.Text = "$0"
        Me.txtDepositBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Refund Amount"
        '
        'lblDepositDisplay
        '
        Me.lblDepositDisplay.AutoSize = True
        Me.lblDepositDisplay.Location = New System.Drawing.Point(12, 61)
        Me.lblDepositDisplay.Name = "lblDepositDisplay"
        Me.lblDepositDisplay.Size = New System.Drawing.Size(75, 13)
        Me.lblDepositDisplay.TabIndex = 6
        Me.lblDepositDisplay.Text = "For Deposit ID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Available deposit balance"
        '
        'FinRefundView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 319)
        Me.Controls.Add(Me.lblErrorMessage)
        Me.Controls.Add(Me.lblSaveMessage)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblRefundID)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnSaveNew)
        Me.Controls.Add(Me.txtDepositBalance)
        Me.Controls.Add(Me.txtRefundAmount)
        Me.Controls.Add(Me.dtpRefundDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblDepositDisplay)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.MinimumSize = New System.Drawing.Size(359, 357)
        Me.Name = "FinRefundView"
        Me.Text = "New Refund"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpRefundDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSaveNew As Button
    Friend WithEvents lblRefundID As Label
    Friend WithEvents txtRefundAmount As CurrencyTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents txtComments As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblSaveMessage As Label
    Friend WithEvents lblErrorMessage As Label
    Friend WithEvents txtDepositBalance As CurrencyTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblDepositDisplay As Label
    Friend WithEvents Label4 As Label
End Class
