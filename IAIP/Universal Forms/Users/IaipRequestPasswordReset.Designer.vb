<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipRequestPasswordReset
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
        Me.DoResetPassword = New System.Windows.Forms.Button()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.Username = New System.Windows.Forms.TextBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.MessageDisplay = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DoResetPassword
        '
        Me.DoResetPassword.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.DoResetPassword.Location = New System.Drawing.Point(25, 80)
        Me.DoResetPassword.Name = "DoResetPassword"
        Me.DoResetPassword.Size = New System.Drawing.Size(157, 23)
        Me.DoResetPassword.TabIndex = 3
        Me.DoResetPassword.Text = "Reset password"
        Me.DoResetPassword.UseVisualStyleBackColor = True
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(22, 22)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(127, 13)
        Me.UsernameLabel.TabIndex = 15
        Me.UsernameLabel.Text = "Enter your IAIP username"
        '
        'Username
        '
        Me.Username.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Username.Location = New System.Drawing.Point(25, 38)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(235, 23)
        Me.Username.TabIndex = 0
        '
        'Cancel
        '
        Me.Cancel.CausesValidation = False
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(188, 80)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(72, 23)
        Me.Cancel.TabIndex = 4
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'MessageDisplay
        '
        Me.MessageDisplay.AutoSize = True
        Me.MessageDisplay.Location = New System.Drawing.Point(22, 120)
        Me.MessageDisplay.MaximumSize = New System.Drawing.Size(238, 0)
        Me.MessageDisplay.Name = "MessageDisplay"
        Me.MessageDisplay.Size = New System.Drawing.Size(50, 39)
        Me.MessageDisplay.TabIndex = 15
        Me.MessageDisplay.Text = "Message" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3"
        Me.MessageDisplay.Visible = False
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'IaipRequestPasswordReset
        '
        Me.AcceptButton = Me.DoResetPassword
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(283, 177)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.DoResetPassword)
        Me.Controls.Add(Me.MessageDisplay)
        Me.Controls.Add(Me.UsernameLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IaipRequestPasswordReset"
        Me.Text = "Forgot Password"
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DoResetPassword As System.Windows.Forms.Button
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents Username As System.Windows.Forms.TextBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents MessageDisplay As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
End Class
