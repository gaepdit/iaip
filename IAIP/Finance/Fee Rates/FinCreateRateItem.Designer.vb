<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FinCreateRateItem
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
        Me.components = New System.ComponentModel.Container()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpNewRateDate = New System.Windows.Forms.DateTimePicker()
        Me.txtNewRate = New Iaip.CurrencyTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.AutoSize = True
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(30, 179)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(164, 27)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save New Rate Item"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(200, 181)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(86, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(28, 65)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(35, 13)
        Me.UsernameLabel.TabIndex = 20
        Me.UsernameLabel.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(123, 62)
        Me.txtName.MaxLength = 50
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(230, 20)
        Me.txtName.TabIndex = 0
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(123, 22)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(230, 21)
        Me.cmbCategory.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Category"
        '
        'dtpNewRateDate
        '
        Me.dtpNewRateDate.Checked = False
        Me.dtpNewRateDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpNewRateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNewRateDate.Location = New System.Drawing.Point(123, 140)
        Me.dtpNewRateDate.Name = "dtpNewRateDate"
        Me.dtpNewRateDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpNewRateDate.TabIndex = 26
        '
        'txtNewRate
        '
        Me.txtNewRate.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtNewRate.Cue = "$ 0"
        Me.txtNewRate.Location = New System.Drawing.Point(123, 101)
        Me.txtNewRate.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNewRate.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNewRate.Name = "txtNewRate"
        Me.txtNewRate.Size = New System.Drawing.Size(91, 20)
        Me.txtNewRate.TabIndex = 25
        Me.txtNewRate.Text = "$0"
        Me.txtNewRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Rate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Effective Date"
        '
        'FinCreateRateItem
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(378, 226)
        Me.Controls.Add(Me.dtpNewRateDate)
        Me.Controls.Add(Me.txtNewRate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbCategory)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.UsernameLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FinCreateRateItem"
        Me.Text = "Create New Fee Rate Item"
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpNewRateDate As DateTimePicker
    Friend WithEvents txtNewRate As CurrencyTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
End Class
