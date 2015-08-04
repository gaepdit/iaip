<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPLogIn
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiFile = New System.Windows.Forms.MenuItem()
        Me.mmiExit = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mmiRefreshUserID = New System.Windows.Forms.MenuItem()
        Me.mmiResetAllForms = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mmiTestingEnvironment = New System.Windows.Forms.MenuItem()
        Me.mmiHelp = New System.Windows.Forms.MenuItem()
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.mmiCheckForUpdate = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mmiAbout = New System.Windows.Forms.MenuItem()
        Me.btnLoginButton = New System.Windows.Forms.Button()
        Me.LogoBox = New System.Windows.Forms.PictureBox()
        Me.lblCurrentVersionMessage = New System.Windows.Forms.Label()
        Me.lblLicenseLabel = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.txtUserPassword = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.lblIAIP = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblGeneralMessage = New System.Windows.Forms.Label()
        Me.LoginProgressBar = New System.Windows.Forms.ProgressBar()
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiFile, Me.MenuItem1, Me.mmiHelp})
        '
        'mmiFile
        '
        Me.mmiFile.Index = 0
        Me.mmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiExit})
        Me.mmiFile.Text = "&File"
        '
        'mmiExit
        '
        Me.mmiExit.Index = 0
        Me.mmiExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ
        Me.mmiExit.Text = "E&xit IAIP"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiRefreshUserID, Me.mmiResetAllForms, Me.MenuItem2, Me.mmiTestingEnvironment})
        Me.MenuItem1.Text = "&Tools"
        '
        'mmiRefreshUserID
        '
        Me.mmiRefreshUserID.Index = 0
        Me.mmiRefreshUserID.Text = "Refresh Default User"
        '
        'mmiResetAllForms
        '
        Me.mmiResetAllForms.Index = 1
        Me.mmiResetAllForms.Text = "&Reset All Forms"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.Text = "-"
        '
        'mmiTestingEnvironment
        '
        Me.mmiTestingEnvironment.Index = 3
        Me.mmiTestingEnvironment.Shortcut = System.Windows.Forms.Shortcut.CtrlT
        Me.mmiTestingEnvironment.Text = "&Testing Environment"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp, Me.MenuItem4, Me.mmiCheckForUpdate, Me.MenuItem5, Me.mmiAbout})
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online &Help "
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.Text = "-"
        '
        'mmiCheckForUpdate
        '
        Me.mmiCheckForUpdate.Index = 2
        Me.mmiCheckForUpdate.Text = "Check for &Updates"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.Text = "-"
        '
        'mmiAbout
        '
        Me.mmiAbout.Index = 4
        Me.mmiAbout.Text = "&About IAIP"
        '
        'btnLoginButton
        '
        Me.btnLoginButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoginButton.Location = New System.Drawing.Point(422, 259)
        Me.btnLoginButton.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLoginButton.Name = "btnLoginButton"
        Me.btnLoginButton.Size = New System.Drawing.Size(175, 38)
        Me.btnLoginButton.TabIndex = 2
        Me.btnLoginButton.Text = "Log In"
        Me.btnLoginButton.UseVisualStyleBackColor = False
        '
        'LogoBox
        '
        Me.LogoBox.Image = Global.Iaip.My.Resources.Resources.Logo
        Me.LogoBox.InitialImage = Nothing
        Me.LogoBox.Location = New System.Drawing.Point(29, 55)
        Me.LogoBox.Name = "LogoBox"
        Me.LogoBox.Size = New System.Drawing.Size(256, 256)
        Me.LogoBox.TabIndex = 0
        Me.LogoBox.TabStop = False
        '
        'lblCurrentVersionMessage
        '
        Me.lblCurrentVersionMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentVersionMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblCurrentVersionMessage.Location = New System.Drawing.Point(26, 377)
        Me.lblCurrentVersionMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCurrentVersionMessage.Name = "lblCurrentVersionMessage"
        Me.lblCurrentVersionMessage.Size = New System.Drawing.Size(259, 18)
        Me.lblCurrentVersionMessage.TabIndex = 38
        Me.lblCurrentVersionMessage.Text = "Current Version Placeholder"
        Me.lblCurrentVersionMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblCurrentVersionMessage.Visible = False
        '
        'lblLicenseLabel
        '
        Me.lblLicenseLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseLabel.Location = New System.Drawing.Point(26, 327)
        Me.lblLicenseLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLicenseLabel.Name = "lblLicenseLabel"
        Me.lblLicenseLabel.Size = New System.Drawing.Size(259, 36)
        Me.lblLicenseLabel.TabIndex = 37
        Me.lblLicenseLabel.Text = "This product is licensed to Georgia" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DNR/EPD/APB employees only"
        Me.lblLicenseLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(336, 223)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(82, 20)
        Me.lblPassword.TabIndex = 36
        Me.lblPassword.Text = "Password:"
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserID.Location = New System.Drawing.Point(336, 184)
        Me.lblUserID.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(68, 20)
        Me.lblUserID.TabIndex = 35
        Me.lblUserID.Text = "User ID:"
        '
        'txtUserPassword
        '
        Me.txtUserPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserPassword.Location = New System.Drawing.Point(422, 220)
        Me.txtUserPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserPassword.Name = "txtUserPassword"
        Me.txtUserPassword.Size = New System.Drawing.Size(175, 26)
        Me.txtUserPassword.TabIndex = 1
        Me.txtUserPassword.UseSystemPasswordChar = True
        Me.txtUserPassword.WordWrap = False
        '
        'txtUserID
        '
        Me.txtUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.Location = New System.Drawing.Point(422, 181)
        Me.txtUserID.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(175, 26)
        Me.txtUserID.TabIndex = 0
        Me.txtUserID.WordWrap = False
        '
        'lblIAIP
        '
        Me.lblIAIP.AutoSize = True
        Me.lblIAIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIAIP.Location = New System.Drawing.Point(335, 128)
        Me.lblIAIP.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblIAIP.Name = "lblIAIP"
        Me.lblIAIP.Size = New System.Drawing.Size(345, 26)
        Me.lblIAIP.TabIndex = 32
        Me.lblIAIP.Text = "Integrated Air Information Platform" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTitle.Location = New System.Drawing.Point(335, 55)
        Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(339, 52)
        Me.lblSubTitle.TabIndex = 32
        Me.lblSubTitle.Text = "Environmental Protection Division" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Air Protection Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(23, 9)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(696, 31)
        Me.lblTitle.TabIndex = 33
        Me.lblTitle.Text = "GEORGIA DEPARTMENT OF NATURAL RESOURCES"
        '
        'lblGeneralMessage
        '
        Me.lblGeneralMessage.AutoSize = True
        Me.lblGeneralMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralMessage.ForeColor = System.Drawing.Color.Maroon
        Me.lblGeneralMessage.Location = New System.Drawing.Point(337, 185)
        Me.lblGeneralMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblGeneralMessage.Name = "lblGeneralMessage"
        Me.lblGeneralMessage.Size = New System.Drawing.Size(163, 72)
        Me.lblGeneralMessage.TabIndex = 38
        Me.lblGeneralMessage.Text = "Message Placeholder 1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4"
        Me.lblGeneralMessage.Visible = False
        '
        'LoginProgressBar
        '
        Me.LoginProgressBar.Location = New System.Drawing.Point(422, 302)
        Me.LoginProgressBar.MarqueeAnimationSpeed = 10
        Me.LoginProgressBar.Name = "LoginProgressBar"
        Me.LoginProgressBar.Size = New System.Drawing.Size(175, 23)
        Me.LoginProgressBar.Step = 20
        Me.LoginProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.LoginProgressBar.TabIndex = 46
        Me.LoginProgressBar.Visible = False
        '
        'IAIPLogIn
        '
        Me.AcceptButton = Me.btnLoginButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LavenderBlush
        Me.ClientSize = New System.Drawing.Size(742, 393)
        Me.Controls.Add(Me.LogoBox)
        Me.Controls.Add(Me.lblCurrentVersionMessage)
        Me.Controls.Add(Me.lblLicenseLabel)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUserID)
        Me.Controls.Add(Me.txtUserPassword)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.lblIAIP)
        Me.Controls.Add(Me.lblSubTitle)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblGeneralMessage)
        Me.Controls.Add(Me.LoginProgressBar)
        Me.Controls.Add(Me.btnLoginButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "IAIPLogIn"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Integrated Air Information Platform"
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents txtUserPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnLoginButton As System.Windows.Forms.Button
    Friend WithEvents lblLicenseLabel As System.Windows.Forms.Label
    Friend WithEvents mmiTestingEnvironment As System.Windows.Forms.MenuItem
    Friend WithEvents mmiRefreshUserID As System.Windows.Forms.MenuItem
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents lblCurrentVersionMessage As System.Windows.Forms.Label
    Friend WithEvents lblGeneralMessage As System.Windows.Forms.Label
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents lblIAIP As System.Windows.Forms.Label
    Friend WithEvents LogoBox As System.Windows.Forms.PictureBox
    Friend WithEvents LoginProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAbout As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiResetAllForms As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCheckForUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
End Class
