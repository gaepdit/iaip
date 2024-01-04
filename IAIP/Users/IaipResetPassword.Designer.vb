<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipResetPassword
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
        Me.Save = New System.Windows.Forms.Button()
        Me.NewPassword = New System.Windows.Forms.TextBox()
        Me.ConfirmPasswordLabel = New System.Windows.Forms.Label()
        Me.ConfirmPassword = New System.Windows.Forms.TextBox()
        Me.NewPasswordLabel = New System.Windows.Forms.Label()
        Me.TokenLabel = New System.Windows.Forms.Label()
        Me.Token = New System.Windows.Forms.TextBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.MessageDisplay = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.Username = New System.Windows.Forms.TextBox()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Save
        '
        Me.Save.Location = New System.Drawing.Point(25, 146)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(133, 23)
        Me.Save.TabIndex = 3
        Me.Save.Text = "Change Password"
        Me.Save.UseVisualStyleBackColor = True
        '
        'NewPassword
        '
        Me.NewPassword.Location = New System.Drawing.Point(149, 82)
        Me.NewPassword.Name = "NewPassword"
        Me.NewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.NewPassword.Size = New System.Drawing.Size(116, 20)
        Me.NewPassword.TabIndex = 1
        '
        'ConfirmPasswordLabel
        '
        Me.ConfirmPasswordLabel.AutoSize = True
        Me.ConfirmPasswordLabel.Location = New System.Drawing.Point(22, 111)
        Me.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel"
        Me.ConfirmPasswordLabel.Size = New System.Drawing.Size(116, 13)
        Me.ConfirmPasswordLabel.TabIndex = 17
        Me.ConfirmPasswordLabel.Text = "Confirm New Password"
        '
        'ConfirmPassword
        '
        Me.ConfirmPassword.Location = New System.Drawing.Point(149, 108)
        Me.ConfirmPassword.Name = "ConfirmPassword"
        Me.ConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ConfirmPassword.Size = New System.Drawing.Size(116, 20)
        Me.ConfirmPassword.TabIndex = 2
        '
        'NewPasswordLabel
        '
        Me.NewPasswordLabel.AutoSize = True
        Me.NewPasswordLabel.Location = New System.Drawing.Point(22, 85)
        Me.NewPasswordLabel.Name = "NewPasswordLabel"
        Me.NewPasswordLabel.Size = New System.Drawing.Size(78, 13)
        Me.NewPasswordLabel.TabIndex = 15
        Me.NewPasswordLabel.Text = "New Password"
        '
        'TokenLabel
        '
        Me.TokenLabel.AutoSize = True
        Me.TokenLabel.Location = New System.Drawing.Point(22, 59)
        Me.TokenLabel.Name = "TokenLabel"
        Me.TokenLabel.Size = New System.Drawing.Size(118, 13)
        Me.TokenLabel.TabIndex = 15
        Me.TokenLabel.Text = "Reset code (from email)"
        '
        'Token
        '
        Me.Token.Location = New System.Drawing.Point(149, 56)
        Me.Token.Name = "Token"
        Me.Token.Size = New System.Drawing.Size(116, 20)
        Me.Token.TabIndex = 0
        '
        'Cancel
        '
        Me.Cancel.CausesValidation = False
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(164, 146)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(101, 23)
        Me.Cancel.TabIndex = 4
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'MessageDisplay
        '
        Me.MessageDisplay.AutoSize = True
        Me.MessageDisplay.Location = New System.Drawing.Point(22, 186)
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
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(22, 33)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(55, 13)
        Me.UsernameLabel.TabIndex = 15
        Me.UsernameLabel.Text = "Username"
        '
        'Username
        '
        Me.Username.Location = New System.Drawing.Point(149, 30)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(116, 20)
        Me.Username.TabIndex = 0
        '
        'IaipResetPassword
        '
        Me.AcceptButton = Me.Save
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(286, 265)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Token)
        Me.Controls.Add(Me.NewPassword)
        Me.Controls.Add(Me.ConfirmPassword)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.ConfirmPasswordLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.MessageDisplay)
        Me.Controls.Add(Me.TokenLabel)
        Me.Controls.Add(Me.NewPasswordLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IaipResetPassword"
        Me.Text = "Reset Password"
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents NewPassword As System.Windows.Forms.TextBox
    Friend WithEvents ConfirmPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents ConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents NewPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents TokenLabel As System.Windows.Forms.Label
    Friend WithEvents Token As System.Windows.Forms.TextBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents MessageDisplay As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Username As TextBox
    Friend WithEvents UsernameLabel As Label
End Class
