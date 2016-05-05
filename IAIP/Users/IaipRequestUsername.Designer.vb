<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipRequestUsername
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
        Me.DoRequestUsername = New System.Windows.Forms.Button()
        Me.EmailAddressLabel = New System.Windows.Forms.Label()
        Me.EmailAddress = New System.Windows.Forms.TextBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.MessageDisplay = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DoRequestUsername
        '
        Me.DoRequestUsername.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.DoRequestUsername.Location = New System.Drawing.Point(25, 80)
        Me.DoRequestUsername.Name = "DoRequestUsername"
        Me.DoRequestUsername.Size = New System.Drawing.Size(157, 23)
        Me.DoRequestUsername.TabIndex = 3
        Me.DoRequestUsername.Text = "Send username reminder"
        Me.DoRequestUsername.UseVisualStyleBackColor = True
        '
        'EmailAddressLabel
        '
        Me.EmailAddressLabel.AutoSize = True
        Me.EmailAddressLabel.Location = New System.Drawing.Point(22, 22)
        Me.EmailAddressLabel.Name = "EmailAddressLabel"
        Me.EmailAddressLabel.Size = New System.Drawing.Size(122, 13)
        Me.EmailAddressLabel.TabIndex = 15
        Me.EmailAddressLabel.Text = "Enter your email address"
        '
        'EmailAddress
        '
        Me.EmailAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmailAddress.Location = New System.Drawing.Point(25, 38)
        Me.EmailAddress.Name = "EmailAddress"
        Me.EmailAddress.Size = New System.Drawing.Size(235, 23)
        Me.EmailAddress.TabIndex = 0
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
        'IaipRequestUsername
        '
        Me.AcceptButton = Me.DoRequestUsername
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(283, 177)
        Me.Controls.Add(Me.EmailAddress)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.DoRequestUsername)
        Me.Controls.Add(Me.MessageDisplay)
        Me.Controls.Add(Me.EmailAddressLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IaipRequestUsername"
        Me.Text = "Forgot Username"
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DoRequestUsername As System.Windows.Forms.Button
    Friend WithEvents EmailAddressLabel As System.Windows.Forms.Label
    Friend WithEvents EmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents MessageDisplay As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
End Class
