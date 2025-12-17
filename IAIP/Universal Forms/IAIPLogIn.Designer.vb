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
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiForgotUsername = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiForgotPassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiPasswordReset = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mmiRefreshUserID = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiResetAllForms = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiOnlineHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiTestingMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiThrowHandledError = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiThrowUnhandledError = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiForceEnableLogin = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReloadNotificationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnLoginButton = New System.Windows.Forms.Button()
        Me.LogoBox = New System.Windows.Forms.PictureBox()
        Me.lblCurrentVersionMessage = New System.Windows.Forms.Label()
        Me.lblLicenseLabel = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.txtUserPassword = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblGeneralMessage = New System.Windows.Forms.Label()
        Me.ForgotUsernameLink = New System.Windows.Forms.LinkLabel()
        Me.ForgotPasswordLink = New System.Windows.Forms.LinkLabel()
        Me.RetryButton = New System.Windows.Forms.Button()
        Me.chkRemember = New System.Windows.Forms.CheckBox()
        Me.lblIAIP = New System.Windows.Forms.Label()
        Me.lnkChangelog = New System.Windows.Forms.LinkLabel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlNotificationContainer = New System.Windows.Forms.Panel()
        Me.pnlSpacing = New System.Windows.Forms.Panel()
        Me.pnlNotifications = New System.Windows.Forms.Panel()
        Me.lblNotification = New System.Windows.Forms.Label()
        Me.pnlNoticeLabelPanel = New System.Windows.Forms.Panel()
        Me.lblNoticeLabel = New System.Windows.Forms.Label()
        Me.bgrOrgNotifications = New System.ComponentModel.BackgroundWorker()
        Me.MainMenu1.SuspendLayout()
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlNotificationContainer.SuspendLayout()
        Me.pnlSpacing.SuspendLayout()
        Me.pnlNotifications.SuspendLayout()
        Me.pnlNoticeLabelPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiTools, Me.mmiHelp, Me.mmiTestingMenu})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(756, 24)
        Me.MainMenu1.TabIndex = 39
        '
        'mmiTools
        '
        Me.mmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiForgotUsername, Me.mmiForgotPassword, Me.mmiPasswordReset, Me.ToolStripSeparator3, Me.mmiRefreshUserID, Me.mmiResetAllForms})
        Me.mmiTools.MergeIndex = 1
        Me.mmiTools.Name = "mmiTools"
        Me.mmiTools.Size = New System.Drawing.Size(46, 20)
        Me.mmiTools.Text = "&Tools"
        '
        'mmiForgotUsername
        '
        Me.mmiForgotUsername.MergeIndex = 0
        Me.mmiForgotUsername.Name = "mmiForgotUsername"
        Me.mmiForgotUsername.Size = New System.Drawing.Size(216, 22)
        Me.mmiForgotUsername.Text = "Forgot Username"
        '
        'mmiForgotPassword
        '
        Me.mmiForgotPassword.MergeIndex = 1
        Me.mmiForgotPassword.Name = "mmiForgotPassword"
        Me.mmiForgotPassword.Size = New System.Drawing.Size(216, 22)
        Me.mmiForgotPassword.Text = "Forgot Password"
        '
        'mmiPasswordReset
        '
        Me.mmiPasswordReset.MergeIndex = 2
        Me.mmiPasswordReset.Name = "mmiPasswordReset"
        Me.mmiPasswordReset.Size = New System.Drawing.Size(216, 22)
        Me.mmiPasswordReset.Text = "Enter Password Reset Code"
        Me.mmiPasswordReset.Visible = False
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(213, 6)
        '
        'mmiRefreshUserID
        '
        Me.mmiRefreshUserID.MergeIndex = 4
        Me.mmiRefreshUserID.Name = "mmiRefreshUserID"
        Me.mmiRefreshUserID.Size = New System.Drawing.Size(216, 22)
        Me.mmiRefreshUserID.Text = "Reset Default Username"
        '
        'mmiResetAllForms
        '
        Me.mmiResetAllForms.MergeIndex = 5
        Me.mmiResetAllForms.Name = "mmiResetAllForms"
        Me.mmiResetAllForms.Size = New System.Drawing.Size(216, 22)
        Me.mmiResetAllForms.Text = "&Reset All Form Sizes"
        '
        'mmiHelp
        '
        Me.mmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiOnlineHelp, Me.mmiAbout})
        Me.mmiHelp.MergeIndex = 2
        Me.mmiHelp.Name = "mmiHelp"
        Me.mmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.MergeIndex = 0
        Me.mmiOnlineHelp.Name = "mmiOnlineHelp"
        Me.mmiOnlineHelp.Size = New System.Drawing.Size(140, 22)
        Me.mmiOnlineHelp.Text = "Online &Help "
        '
        'mmiAbout
        '
        Me.mmiAbout.MergeIndex = 4
        Me.mmiAbout.Name = "mmiAbout"
        Me.mmiAbout.Size = New System.Drawing.Size(140, 22)
        Me.mmiAbout.Text = "&About IAIP"
        '
        'mmiTestingMenu
        '
        Me.mmiTestingMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiThrowHandledError, Me.mmiThrowUnhandledError, Me.mmiForceEnableLogin, Me.ReloadNotificationsToolStripMenuItem})
        Me.mmiTestingMenu.MergeIndex = 3
        Me.mmiTestingMenu.Name = "mmiTestingMenu"
        Me.mmiTestingMenu.Size = New System.Drawing.Size(39, 20)
        Me.mmiTestingMenu.Text = "T&est"
        Me.mmiTestingMenu.Visible = False
        '
        'mmiThrowHandledError
        '
        Me.mmiThrowHandledError.MergeIndex = 0
        Me.mmiThrowHandledError.Name = "mmiThrowHandledError"
        Me.mmiThrowHandledError.Size = New System.Drawing.Size(188, 22)
        Me.mmiThrowHandledError.Text = "&Handled Exception"
        '
        'mmiThrowUnhandledError
        '
        Me.mmiThrowUnhandledError.MergeIndex = 1
        Me.mmiThrowUnhandledError.Name = "mmiThrowUnhandledError"
        Me.mmiThrowUnhandledError.Size = New System.Drawing.Size(188, 22)
        Me.mmiThrowUnhandledError.Text = "&Unhandled Exception"
        '
        'mmiForceEnableLogin
        '
        Me.mmiForceEnableLogin.MergeIndex = 2
        Me.mmiForceEnableLogin.Name = "mmiForceEnableLogin"
        Me.mmiForceEnableLogin.Size = New System.Drawing.Size(188, 22)
        Me.mmiForceEnableLogin.Text = "&Enable the login form"
        '
        'ReloadNotificationsToolStripMenuItem
        '
        Me.ReloadNotificationsToolStripMenuItem.Name = "ReloadNotificationsToolStripMenuItem"
        Me.ReloadNotificationsToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ReloadNotificationsToolStripMenuItem.Text = "&Reload notifications"
        '
        'btnLoginButton
        '
        Me.btnLoginButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoginButton.Location = New System.Drawing.Point(418, 249)
        Me.btnLoginButton.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLoginButton.Name = "btnLoginButton"
        Me.btnLoginButton.Size = New System.Drawing.Size(175, 38)
        Me.btnLoginButton.TabIndex = 3
        Me.btnLoginButton.Text = "Log In"
        Me.btnLoginButton.UseVisualStyleBackColor = False
        '
        'LogoBox
        '
        Me.LogoBox.Image = Global.Iaip.My.Resources.Resources.EpdLogo
        Me.LogoBox.InitialImage = Nothing
        Me.LogoBox.Location = New System.Drawing.Point(29, 55)
        Me.LogoBox.Name = "LogoBox"
        Me.LogoBox.Size = New System.Drawing.Size(256, 256)
        Me.LogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LogoBox.TabIndex = 0
        Me.LogoBox.TabStop = False
        '
        'lblCurrentVersionMessage
        '
        Me.lblCurrentVersionMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentVersionMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblCurrentVersionMessage.Location = New System.Drawing.Point(29, 351)
        Me.lblCurrentVersionMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCurrentVersionMessage.Name = "lblCurrentVersionMessage"
        Me.lblCurrentVersionMessage.Size = New System.Drawing.Size(256, 38)
        Me.lblCurrentVersionMessage.TabIndex = 38
        Me.lblCurrentVersionMessage.Text = "Current Version Placeholder"
        Me.lblCurrentVersionMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblCurrentVersionMessage.Visible = False
        '
        'lblLicenseLabel
        '
        Me.lblLicenseLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseLabel.Location = New System.Drawing.Point(29, 314)
        Me.lblLicenseLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLicenseLabel.Name = "lblLicenseLabel"
        Me.lblLicenseLabel.Size = New System.Drawing.Size(256, 36)
        Me.lblLicenseLabel.TabIndex = 37
        Me.lblLicenseLabel.Text = "This product is licensed to " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "State of Georgia employees only."
        Me.lblLicenseLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(331, 185)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(78, 20)
        Me.lblPassword.TabIndex = 36
        Me.lblPassword.Text = "Password"
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserID.Location = New System.Drawing.Point(331, 146)
        Me.lblUserID.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(83, 20)
        Me.lblUserID.TabIndex = 35
        Me.lblUserID.Text = "Username"
        '
        'txtUserPassword
        '
        Me.txtUserPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtUserPassword.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtUserPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserPassword.Location = New System.Drawing.Point(418, 182)
        Me.txtUserPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserPassword.Name = "txtUserPassword"
        Me.txtUserPassword.Size = New System.Drawing.Size(175, 26)
        Me.txtUserPassword.TabIndex = 1
        Me.txtUserPassword.UseSystemPasswordChar = True
        Me.txtUserPassword.WordWrap = False
        '
        'txtUserID
        '
        Me.txtUserID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtUserID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.Location = New System.Drawing.Point(418, 143)
        Me.txtUserID.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(175, 26)
        Me.txtUserID.TabIndex = 0
        Me.txtUserID.WordWrap = False
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTitle.Location = New System.Drawing.Point(330, 55)
        Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(339, 26)
        Me.lblSubTitle.TabIndex = 32
        Me.lblSubTitle.Text = "Environmental Protection Division" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
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
        Me.lblGeneralMessage.Location = New System.Drawing.Point(332, 299)
        Me.lblGeneralMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblGeneralMessage.MaximumSize = New System.Drawing.Size(382, 96)
        Me.lblGeneralMessage.Name = "lblGeneralMessage"
        Me.lblGeneralMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblGeneralMessage.Size = New System.Drawing.Size(169, 60)
        Me.lblGeneralMessage.TabIndex = 38
        Me.lblGeneralMessage.Text = "Message Placeholder 1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3"
        Me.lblGeneralMessage.Visible = False
        '
        'ForgotUsernameLink
        '
        Me.ForgotUsernameLink.AutoSize = True
        Me.ForgotUsernameLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForgotUsernameLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ForgotUsernameLink.Location = New System.Drawing.Point(598, 146)
        Me.ForgotUsernameLink.Name = "ForgotUsernameLink"
        Me.ForgotUsernameLink.Size = New System.Drawing.Size(143, 20)
        Me.ForgotUsernameLink.TabIndex = 5
        Me.ForgotUsernameLink.TabStop = True
        Me.ForgotUsernameLink.Text = "Forgot Username?"
        Me.ForgotUsernameLink.Visible = False
        '
        'ForgotPasswordLink
        '
        Me.ForgotPasswordLink.AutoSize = True
        Me.ForgotPasswordLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForgotPasswordLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ForgotPasswordLink.Location = New System.Drawing.Point(598, 185)
        Me.ForgotPasswordLink.Name = "ForgotPasswordLink"
        Me.ForgotPasswordLink.Size = New System.Drawing.Size(138, 20)
        Me.ForgotPasswordLink.TabIndex = 6
        Me.ForgotPasswordLink.TabStop = True
        Me.ForgotPasswordLink.Text = "Forgot Password?"
        Me.ForgotPasswordLink.Visible = False
        '
        'RetryButton
        '
        Me.RetryButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RetryButton.Location = New System.Drawing.Point(332, 365)
        Me.RetryButton.Name = "RetryButton"
        Me.RetryButton.Size = New System.Drawing.Size(107, 31)
        Me.RetryButton.TabIndex = 4
        Me.RetryButton.Text = "Try again"
        Me.RetryButton.UseVisualStyleBackColor = True
        Me.RetryButton.Visible = False
        '
        'chkRemember
        '
        Me.chkRemember.AutoSize = True
        Me.chkRemember.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRemember.Location = New System.Drawing.Point(419, 219)
        Me.chkRemember.Name = "chkRemember"
        Me.chkRemember.Size = New System.Drawing.Size(119, 21)
        Me.chkRemember.TabIndex = 2
        Me.chkRemember.Text = "Remember Me"
        Me.chkRemember.UseVisualStyleBackColor = True
        '
        'lblIAIP
        '
        Me.lblIAIP.AutoSize = True
        Me.lblIAIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIAIP.Location = New System.Drawing.Point(330, 94)
        Me.lblIAIP.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblIAIP.Name = "lblIAIP"
        Me.lblIAIP.Size = New System.Drawing.Size(345, 26)
        Me.lblIAIP.TabIndex = 32
        Me.lblIAIP.Text = "Integrated Air Information Platform" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lnkChangelog
        '
        Me.lnkChangelog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkChangelog.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkChangelog.Location = New System.Drawing.Point(29, 389)
        Me.lnkChangelog.Name = "lnkChangelog"
        Me.lnkChangelog.Size = New System.Drawing.Size(256, 20)
        Me.lnkChangelog.TabIndex = 7
        Me.lnkChangelog.TabStop = True
        Me.lnkChangelog.Text = "Change Log"
        Me.lnkChangelog.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lnkChangelog.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.lblTitle)
        Me.pnlMain.Controls.Add(Me.btnLoginButton)
        Me.pnlMain.Controls.Add(Me.lnkChangelog)
        Me.pnlMain.Controls.Add(Me.lblGeneralMessage)
        Me.pnlMain.Controls.Add(Me.chkRemember)
        Me.pnlMain.Controls.Add(Me.lblSubTitle)
        Me.pnlMain.Controls.Add(Me.RetryButton)
        Me.pnlMain.Controls.Add(Me.lblIAIP)
        Me.pnlMain.Controls.Add(Me.ForgotPasswordLink)
        Me.pnlMain.Controls.Add(Me.txtUserID)
        Me.pnlMain.Controls.Add(Me.ForgotUsernameLink)
        Me.pnlMain.Controls.Add(Me.txtUserPassword)
        Me.pnlMain.Controls.Add(Me.LogoBox)
        Me.pnlMain.Controls.Add(Me.lblUserID)
        Me.pnlMain.Controls.Add(Me.lblCurrentVersionMessage)
        Me.pnlMain.Controls.Add(Me.lblPassword)
        Me.pnlMain.Controls.Add(Me.lblLicenseLabel)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 96)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(756, 351)
        Me.pnlMain.TabIndex = 0
        '
        'pnlNotificationContainer
        '
        Me.pnlNotificationContainer.BackColor = System.Drawing.Color.PapayaWhip
        Me.pnlNotificationContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlNotificationContainer.Controls.Add(Me.pnlSpacing)
        Me.pnlNotificationContainer.Controls.Add(Me.pnlNoticeLabelPanel)
        Me.pnlNotificationContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlNotificationContainer.Location = New System.Drawing.Point(0, 24)
        Me.pnlNotificationContainer.Name = "pnlNotificationContainer"
        Me.pnlNotificationContainer.Size = New System.Drawing.Size(756, 72)
        Me.pnlNotificationContainer.TabIndex = 39
        Me.pnlNotificationContainer.Visible = False
        '
        'pnlSpacing
        '
        Me.pnlSpacing.Controls.Add(Me.pnlNotifications)
        Me.pnlSpacing.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSpacing.Location = New System.Drawing.Point(74, 0)
        Me.pnlSpacing.Name = "pnlSpacing"
        Me.pnlSpacing.Padding = New System.Windows.Forms.Padding(0, 6, 0, 6)
        Me.pnlSpacing.Size = New System.Drawing.Size(680, 70)
        Me.pnlSpacing.TabIndex = 4
        '
        'pnlNotifications
        '
        Me.pnlNotifications.AutoScroll = True
        Me.pnlNotifications.AutoSize = True
        Me.pnlNotifications.Controls.Add(Me.lblNotification)
        Me.pnlNotifications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNotifications.Location = New System.Drawing.Point(0, 6)
        Me.pnlNotifications.Name = "pnlNotifications"
        Me.pnlNotifications.Size = New System.Drawing.Size(680, 58)
        Me.pnlNotifications.TabIndex = 4
        '
        'lblNotification
        '
        Me.lblNotification.AutoSize = True
        Me.lblNotification.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotification.Location = New System.Drawing.Point(0, 0)
        Me.lblNotification.Name = "lblNotification"
        Me.lblNotification.Size = New System.Drawing.Size(90, 45)
        Me.lblNotification.TabIndex = 2
        Me.lblNotification.Text = "Notification text" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Line 2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Line 3"
        '
        'pnlNoticeLabelPanel
        '
        Me.pnlNoticeLabelPanel.Controls.Add(Me.lblNoticeLabel)
        Me.pnlNoticeLabelPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlNoticeLabelPanel.Location = New System.Drawing.Point(0, 0)
        Me.pnlNoticeLabelPanel.Name = "pnlNoticeLabelPanel"
        Me.pnlNoticeLabelPanel.Size = New System.Drawing.Size(74, 70)
        Me.pnlNoticeLabelPanel.TabIndex = 7
        '
        'lblNoticeLabel
        '
        Me.lblNoticeLabel.AutoSize = True
        Me.lblNoticeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoticeLabel.Location = New System.Drawing.Point(5, 5)
        Me.lblNoticeLabel.Name = "lblNoticeLabel"
        Me.lblNoticeLabel.Size = New System.Drawing.Size(62, 17)
        Me.lblNoticeLabel.TabIndex = 1
        Me.lblNoticeLabel.Text = "Notices"
        '
        'bgrOrgNotifications
        '
        '
        'IAIPLogIn
        '
        Me.AcceptButton = Me.btnLoginButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 447)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlNotificationContainer)
        Me.Controls.Add(Me.MainMenu1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IAIPLogIn"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Integrated Air Information Platform"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlNotificationContainer.ResumeLayout(False)
        Me.pnlSpacing.ResumeLayout(False)
        Me.pnlSpacing.PerformLayout()
        Me.pnlNotifications.ResumeLayout(False)
        Me.pnlNotifications.PerformLayout()
        Me.pnlNoticeLabelPanel.ResumeLayout(False)
        Me.pnlNoticeLabelPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents txtUserPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnLoginButton As System.Windows.Forms.Button
    Friend WithEvents lblLicenseLabel As System.Windows.Forms.Label
    Friend WithEvents mmiRefreshUserID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblCurrentVersionMessage As System.Windows.Forms.Label
    Friend WithEvents lblGeneralMessage As System.Windows.Forms.Label
    Friend WithEvents LogoBox As System.Windows.Forms.PictureBox
    Friend WithEvents mmiAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiResetAllForms As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiTestingMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiForgotUsername As ToolStripMenuItem
    Friend WithEvents mmiForgotPassword As ToolStripMenuItem
    Friend WithEvents ForgotPasswordLink As LinkLabel
    Friend WithEvents ForgotUsernameLink As LinkLabel
    Friend WithEvents mmiPasswordReset As ToolStripMenuItem
    Friend WithEvents RetryButton As Button
    Friend WithEvents mmiThrowUnhandledError As ToolStripMenuItem
    Friend WithEvents mmiThrowHandledError As ToolStripMenuItem
    Friend WithEvents chkRemember As CheckBox
    Friend WithEvents lblIAIP As Label
    Friend WithEvents lnkChangelog As LinkLabel
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents pnlMain As Panel
    Friend WithEvents mmiForceEnableLogin As ToolStripMenuItem
    Friend WithEvents pnlNotificationContainer As Panel
    Friend WithEvents pnlSpacing As Panel
    Friend WithEvents pnlNotifications As Panel
    Friend WithEvents lblNotification As Label
    Friend WithEvents pnlNoticeLabelPanel As Panel
    Friend WithEvents lblNoticeLabel As Label
    Friend WithEvents bgrOrgNotifications As System.ComponentModel.BackgroundWorker
    Friend WithEvents ReloadNotificationsToolStripMenuItem As ToolStripMenuItem
End Class
