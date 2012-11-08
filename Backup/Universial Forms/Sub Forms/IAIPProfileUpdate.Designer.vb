<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPProfileUpdate
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPProfileUpdate))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtEmailAddress = New System.Windows.Forms.TextBox
        Me.btnUpdateEmail = New System.Windows.Forms.Button
        Me.lblEmailAddress = New System.Windows.Forms.Label
        Me.lblPhoneNumber = New System.Windows.Forms.Label
        Me.lblValidateIDPassword = New System.Windows.Forms.Label
        Me.pnlEmailAddress = New System.Windows.Forms.Panel
        Me.pnlPhoneNumber = New System.Windows.Forms.Panel
        Me.mtbPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnUpdatePhoneNumber = New System.Windows.Forms.Button
        Me.pnlUserIDPassword = New System.Windows.Forms.Panel
        Me.btnUpdatePassword = New System.Windows.Forms.Button
        Me.txtUserPassword = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlEmailAddress.SuspendLayout()
        Me.pnlPhoneNumber.SuspendLayout()
        Me.pnlUserIDPassword.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Email Address"
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(82, 25)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(162, 20)
        Me.txtEmailAddress.TabIndex = 0
        '
        'btnUpdateEmail
        '
        Me.btnUpdateEmail.AutoSize = True
        Me.btnUpdateEmail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateEmail.Location = New System.Drawing.Point(260, 24)
        Me.btnUpdateEmail.Name = "btnUpdateEmail"
        Me.btnUpdateEmail.Size = New System.Drawing.Size(121, 23)
        Me.btnUpdateEmail.TabIndex = 1
        Me.btnUpdateEmail.Text = "Update Email Address"
        Me.btnUpdateEmail.UseVisualStyleBackColor = True
        '
        'lblEmailAddress
        '
        Me.lblEmailAddress.AutoSize = True
        Me.lblEmailAddress.Location = New System.Drawing.Point(3, 0)
        Me.lblEmailAddress.Name = "lblEmailAddress"
        Me.lblEmailAddress.Size = New System.Drawing.Size(114, 13)
        Me.lblEmailAddress.TabIndex = 3
        Me.lblEmailAddress.Text = "Validate Email Address"
        '
        'lblPhoneNumber
        '
        Me.lblPhoneNumber.AutoSize = True
        Me.lblPhoneNumber.Location = New System.Drawing.Point(3, 2)
        Me.lblPhoneNumber.Name = "lblPhoneNumber"
        Me.lblPhoneNumber.Size = New System.Drawing.Size(119, 13)
        Me.lblPhoneNumber.TabIndex = 4
        Me.lblPhoneNumber.Text = "Validate Phone Number"
        '
        'lblValidateIDPassword
        '
        Me.lblValidateIDPassword.AutoSize = True
        Me.lblValidateIDPassword.Location = New System.Drawing.Point(8, 4)
        Me.lblValidateIDPassword.Name = "lblValidateIDPassword"
        Me.lblValidateIDPassword.Size = New System.Drawing.Size(228, 13)
        Me.lblValidateIDPassword.TabIndex = 5
        Me.lblValidateIDPassword.Text = "Password and Last Name cannot be the same."
        '
        'pnlEmailAddress
        '
        Me.pnlEmailAddress.Controls.Add(Me.lblEmailAddress)
        Me.pnlEmailAddress.Controls.Add(Me.txtEmailAddress)
        Me.pnlEmailAddress.Controls.Add(Me.Label1)
        Me.pnlEmailAddress.Controls.Add(Me.btnUpdateEmail)
        Me.pnlEmailAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEmailAddress.Location = New System.Drawing.Point(0, 0)
        Me.pnlEmailAddress.Name = "pnlEmailAddress"
        Me.pnlEmailAddress.Size = New System.Drawing.Size(398, 62)
        Me.pnlEmailAddress.TabIndex = 7
        '
        'pnlPhoneNumber
        '
        Me.pnlPhoneNumber.Controls.Add(Me.mtbPhoneNumber)
        Me.pnlPhoneNumber.Controls.Add(Me.Label2)
        Me.pnlPhoneNumber.Controls.Add(Me.btnUpdatePhoneNumber)
        Me.pnlPhoneNumber.Controls.Add(Me.lblPhoneNumber)
        Me.pnlPhoneNumber.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPhoneNumber.Location = New System.Drawing.Point(0, 62)
        Me.pnlPhoneNumber.Name = "pnlPhoneNumber"
        Me.pnlPhoneNumber.Size = New System.Drawing.Size(398, 66)
        Me.pnlPhoneNumber.TabIndex = 8
        '
        'mtbPhoneNumber
        '
        Me.mtbPhoneNumber.Location = New System.Drawing.Point(93, 30)
        Me.mtbPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbPhoneNumber.Name = "mtbPhoneNumber"
        Me.mtbPhoneNumber.Size = New System.Drawing.Size(88, 20)
        Me.mtbPhoneNumber.TabIndex = 0
        Me.mtbPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Phone Number"
        '
        'btnUpdatePhoneNumber
        '
        Me.btnUpdatePhoneNumber.AutoSize = True
        Me.btnUpdatePhoneNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdatePhoneNumber.Location = New System.Drawing.Point(260, 27)
        Me.btnUpdatePhoneNumber.Name = "btnUpdatePhoneNumber"
        Me.btnUpdatePhoneNumber.Size = New System.Drawing.Size(126, 23)
        Me.btnUpdatePhoneNumber.TabIndex = 1
        Me.btnUpdatePhoneNumber.Text = "Update Phone Number"
        Me.btnUpdatePhoneNumber.UseVisualStyleBackColor = True
        '
        'pnlUserIDPassword
        '
        Me.pnlUserIDPassword.Controls.Add(Me.Label5)
        Me.pnlUserIDPassword.Controls.Add(Me.btnUpdatePassword)
        Me.pnlUserIDPassword.Controls.Add(Me.txtUserPassword)
        Me.pnlUserIDPassword.Controls.Add(Me.Label4)
        Me.pnlUserIDPassword.Controls.Add(Me.txtConfirmPassword)
        Me.pnlUserIDPassword.Controls.Add(Me.Label3)
        Me.pnlUserIDPassword.Controls.Add(Me.lblValidateIDPassword)
        Me.pnlUserIDPassword.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUserIDPassword.Location = New System.Drawing.Point(0, 128)
        Me.pnlUserIDPassword.Name = "pnlUserIDPassword"
        Me.pnlUserIDPassword.Size = New System.Drawing.Size(398, 107)
        Me.pnlUserIDPassword.TabIndex = 9
        '
        'btnUpdatePassword
        '
        Me.btnUpdatePassword.AutoSize = True
        Me.btnUpdatePassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdatePassword.Location = New System.Drawing.Point(260, 56)
        Me.btnUpdatePassword.Name = "btnUpdatePassword"
        Me.btnUpdatePassword.Size = New System.Drawing.Size(101, 23)
        Me.btnUpdatePassword.TabIndex = 2
        Me.btnUpdatePassword.Text = "Update Password"
        Me.btnUpdatePassword.UseVisualStyleBackColor = True
        '
        'txtUserPassword
        '
        Me.txtUserPassword.Location = New System.Drawing.Point(109, 30)
        Me.txtUserPassword.Name = "txtUserPassword"
        Me.txtUserPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtUserPassword.Size = New System.Drawing.Size(100, 20)
        Me.txtUserPassword.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Confirm Password"
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Location = New System.Drawing.Point(109, 58)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPassword.Size = New System.Drawing.Size(100, 20)
        Me.txtConfirmPassword.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Password"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(106, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(150, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "*Passwords are case sensitive"
        '
        'IAIPProfileUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 235)
        Me.Controls.Add(Me.pnlUserIDPassword)
        Me.Controls.Add(Me.pnlPhoneNumber)
        Me.Controls.Add(Me.pnlEmailAddress)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "IAIPProfileUpdate"
        Me.Text = "IAIP Profile Update"
        Me.pnlEmailAddress.ResumeLayout(False)
        Me.pnlEmailAddress.PerformLayout()
        Me.pnlPhoneNumber.ResumeLayout(False)
        Me.pnlPhoneNumber.PerformLayout()
        Me.pnlUserIDPassword.ResumeLayout(False)
        Me.pnlUserIDPassword.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdateEmail As System.Windows.Forms.Button
    Friend WithEvents lblEmailAddress As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents lblValidateIDPassword As System.Windows.Forms.Label
    Friend WithEvents pnlEmailAddress As System.Windows.Forms.Panel
    Friend WithEvents pnlPhoneNumber As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUpdatePhoneNumber As System.Windows.Forms.Button
    Friend WithEvents pnlUserIDPassword As System.Windows.Forms.Panel
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mtbPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtUserPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnUpdatePassword As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
