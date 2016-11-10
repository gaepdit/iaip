<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPNavigation
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiFile = New System.Windows.Forms.MenuItem()
        Me.mmiExit = New System.Windows.Forms.MenuItem()
        Me.mmiTools = New System.Windows.Forms.MenuItem()
        Me.mmiExport = New System.Windows.Forms.MenuItem()
        Me.ProfileMenuItem = New System.Windows.Forms.MenuItem()
        Me.UsernameDisplay = New System.Windows.Forms.MenuItem()
        Me.UsernameSeparator = New System.Windows.Forms.MenuItem()
        Me.UpdateProfile = New System.Windows.Forms.MenuItem()
        Me.ChangePassword = New System.Windows.Forms.MenuItem()
        Me.LogOut = New System.Windows.Forms.MenuItem()
        Me.mmiHelp = New System.Windows.Forms.MenuItem()
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem()
        Me.mmiResetForm = New System.Windows.Forms.MenuItem()
        Me.mmiSeparator1 = New System.Windows.Forms.MenuItem()
        Me.mmiAbout = New System.Windows.Forms.MenuItem()
        Me.TestingMenu = New System.Windows.Forms.MenuItem()
        Me.RunTest = New System.Windows.Forms.MenuItem()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.flpNavButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.grpQuickAccess = New System.Windows.Forms.GroupBox()
        Me.btnOpenTestReport = New System.Windows.Forms.Button()
        Me.btnOpenApplication = New System.Windows.Forms.Button()
        Me.btnOpenTestLog = New System.Windows.Forms.Button()
        Me.btnOpenSscpItem = New System.Windows.Forms.Button()
        Me.btnOpenEnforcement = New System.Windows.Forms.Button()
        Me.btnOpenFacilitySummary = New System.Windows.Forms.Button()
        Me.SbeapQuickAccessPanel = New System.Windows.Forms.Panel()
        Me.txtOpenSbeapCaseLog = New Iaip.CueTextBox()
        Me.btnOpenSbeapCaseLog = New System.Windows.Forms.Button()
        Me.btnOpenSbeapClient = New System.Windows.Forms.Button()
        Me.SbeapCaseLogNumberLabel = New System.Windows.Forms.Label()
        Me.SbeapClientIDLabel = New System.Windows.Forms.Label()
        Me.txtOpenSbeapClient = New Iaip.CueTextBox()
        Me.txtOpenTestLog = New Iaip.CueTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblOpenFacilitySummary = New System.Windows.Forms.Label()
        Me.txtOpenFacilitySummary = New Iaip.CueTextBox()
        Me.txtOpenSscpItem = New Iaip.CueTextBox()
        Me.lblOpenSscpItem = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtOpenApplication = New Iaip.CueTextBox()
        Me.lblOpenEnforcement = New System.Windows.Forms.Label()
        Me.txtOpenEnforcement = New Iaip.CueTextBox()
        Me.lblOpenTestReport = New System.Windows.Forms.Label()
        Me.txtOpenTestReport = New Iaip.CueTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.pnlName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlProgram = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlDbEnv = New System.Windows.Forms.ToolStripStatusLabel()
        Me.rdbAllView = New System.Windows.Forms.RadioButton()
        Me.rdbUnitView = New System.Windows.Forms.RadioButton()
        Me.rdbStaffView = New System.Windows.Forms.RadioButton()
        Me.btnLoadNavWorkList = New System.Windows.Forms.Button()
        Me.lblWorkViewerContext = New System.Windows.Forms.Label()
        Me.lblResultsCount = New System.Windows.Forms.Label()
        Me.bgrLoadWorkViewer = New System.ComponentModel.BackgroundWorker()
        Me.lblMessageLabel = New System.Windows.Forms.Label()
        Me.bgrUserPermissions = New System.ComponentModel.BackgroundWorker()
        Me.pnlCurrentList = New System.Windows.Forms.Panel()
        Me.NavWorkListChangerPanel = New System.Windows.Forms.Panel()
        Me.cboNavWorkListContext = New System.Windows.Forms.ComboBox()
        Me.NavWorkListScopePanel = New System.Windows.Forms.Panel()
        Me.dgvWorkViewer = New System.Windows.Forms.DataGridView()
        Me.grpQuickAccess.SuspendLayout()
        Me.SbeapQuickAccessPanel.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.pnlCurrentList.SuspendLayout()
        Me.NavWorkListChangerPanel.SuspendLayout()
        Me.NavWorkListScopePanel.SuspendLayout()
        CType(Me.dgvWorkViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiFile, Me.mmiTools, Me.ProfileMenuItem, Me.mmiHelp, Me.TestingMenu})
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
        'mmiTools
        '
        Me.mmiTools.Index = 1
        Me.mmiTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiExport})
        Me.mmiTools.Text = "&Tools"
        '
        'mmiExport
        '
        Me.mmiExport.Index = 0
        Me.mmiExport.Text = "&Export list to Excel"
        '
        'ProfileMenuItem
        '
        Me.ProfileMenuItem.Index = 2
        Me.ProfileMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.UsernameDisplay, Me.UsernameSeparator, Me.UpdateProfile, Me.ChangePassword, Me.LogOut})
        Me.ProfileMenuItem.Text = "&Account"
        '
        'UsernameDisplay
        '
        Me.UsernameDisplay.Enabled = False
        Me.UsernameDisplay.Index = 0
        Me.UsernameDisplay.Text = "Logged in as username"
        '
        'UsernameSeparator
        '
        Me.UsernameSeparator.Index = 1
        Me.UsernameSeparator.Text = "-"
        '
        'UpdateProfile
        '
        Me.UpdateProfile.Index = 2
        Me.UpdateProfile.Text = "&Update profile"
        '
        'ChangePassword
        '
        Me.ChangePassword.Index = 3
        Me.ChangePassword.Text = "&Change password"
        '
        'LogOut
        '
        Me.LogOut.Index = 4
        Me.LogOut.Text = "&Log out"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 3
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp, Me.mmiResetForm, Me.mmiSeparator1, Me.mmiAbout})
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online &Help"
        '
        'mmiResetForm
        '
        Me.mmiResetForm.Index = 1
        Me.mmiResetForm.Text = "&Reset all IAIP forms"
        '
        'mmiSeparator1
        '
        Me.mmiSeparator1.Index = 2
        Me.mmiSeparator1.Text = "-"
        '
        'mmiAbout
        '
        Me.mmiAbout.Index = 3
        Me.mmiAbout.Text = "&About IAIP"
        '
        'TestingMenu
        '
        Me.TestingMenu.Index = 4
        Me.TestingMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.RunTest})
        Me.TestingMenu.Text = "T&esting"
        Me.TestingMenu.Visible = False
        '
        'RunTest
        '
        Me.RunTest.Index = 0
        Me.RunTest.Text = "Run test"
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblTitle.Location = New System.Drawing.Point(118, 0)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(686, 33)
        Me.lblTitle.TabIndex = 5
        Me.lblTitle.Text = "Integrated Air Information Platform"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'flpNavButtons
        '
        Me.flpNavButtons.AutoScroll = True
        Me.flpNavButtons.Dock = System.Windows.Forms.DockStyle.Left
        Me.flpNavButtons.Location = New System.Drawing.Point(0, 0)
        Me.flpNavButtons.Name = "flpNavButtons"
        Me.flpNavButtons.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.flpNavButtons.Size = New System.Drawing.Size(118, 398)
        Me.flpNavButtons.TabIndex = 0
        '
        'grpQuickAccess
        '
        Me.grpQuickAccess.Controls.Add(Me.btnOpenTestReport)
        Me.grpQuickAccess.Controls.Add(Me.btnOpenApplication)
        Me.grpQuickAccess.Controls.Add(Me.btnOpenTestLog)
        Me.grpQuickAccess.Controls.Add(Me.btnOpenSscpItem)
        Me.grpQuickAccess.Controls.Add(Me.btnOpenEnforcement)
        Me.grpQuickAccess.Controls.Add(Me.btnOpenFacilitySummary)
        Me.grpQuickAccess.Controls.Add(Me.SbeapQuickAccessPanel)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenTestLog)
        Me.grpQuickAccess.Controls.Add(Me.Label8)
        Me.grpQuickAccess.Controls.Add(Me.lblOpenFacilitySummary)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenFacilitySummary)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenSscpItem)
        Me.grpQuickAccess.Controls.Add(Me.lblOpenSscpItem)
        Me.grpQuickAccess.Controls.Add(Me.Label6)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenApplication)
        Me.grpQuickAccess.Controls.Add(Me.lblOpenEnforcement)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenEnforcement)
        Me.grpQuickAccess.Controls.Add(Me.lblOpenTestReport)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenTestReport)
        Me.grpQuickAccess.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpQuickAccess.Location = New System.Drawing.Point(118, 257)
        Me.grpQuickAccess.Name = "grpQuickAccess"
        Me.grpQuickAccess.Size = New System.Drawing.Size(686, 117)
        Me.grpQuickAccess.TabIndex = 2
        Me.grpQuickAccess.TabStop = False
        Me.grpQuickAccess.Text = "Quick Access"
        '
        'btnOpenTestReport
        '
        Me.btnOpenTestReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenTestReport.FlatAppearance.BorderSize = 0
        Me.btnOpenTestReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenTestReport.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenTestReport.Location = New System.Drawing.Point(425, 34)
        Me.btnOpenTestReport.Name = "btnOpenTestReport"
        Me.btnOpenTestReport.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenTestReport.TabIndex = 9
        Me.btnOpenTestReport.TabStop = False
        Me.btnOpenTestReport.Text = "Open"
        Me.btnOpenTestReport.UseVisualStyleBackColor = True
        '
        'btnOpenApplication
        '
        Me.btnOpenApplication.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenApplication.FlatAppearance.BorderSize = 0
        Me.btnOpenApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenApplication.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenApplication.Location = New System.Drawing.Point(105, 86)
        Me.btnOpenApplication.Name = "btnOpenApplication"
        Me.btnOpenApplication.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenApplication.TabIndex = 3
        Me.btnOpenApplication.TabStop = False
        Me.btnOpenApplication.Text = "Open"
        Me.btnOpenApplication.UseVisualStyleBackColor = True
        '
        'btnOpenTestLog
        '
        Me.btnOpenTestLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenTestLog.FlatAppearance.BorderSize = 0
        Me.btnOpenTestLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenTestLog.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenTestLog.Location = New System.Drawing.Point(426, 86)
        Me.btnOpenTestLog.Name = "btnOpenTestLog"
        Me.btnOpenTestLog.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenTestLog.TabIndex = 11
        Me.btnOpenTestLog.TabStop = False
        Me.btnOpenTestLog.Text = "Open"
        Me.btnOpenTestLog.UseVisualStyleBackColor = True
        '
        'btnOpenSscpItem
        '
        Me.btnOpenSscpItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenSscpItem.FlatAppearance.BorderSize = 0
        Me.btnOpenSscpItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSscpItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenSscpItem.Location = New System.Drawing.Point(265, 86)
        Me.btnOpenSscpItem.Name = "btnOpenSscpItem"
        Me.btnOpenSscpItem.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenSscpItem.TabIndex = 7
        Me.btnOpenSscpItem.TabStop = False
        Me.btnOpenSscpItem.Text = "Open"
        Me.btnOpenSscpItem.UseVisualStyleBackColor = True
        '
        'btnOpenEnforcement
        '
        Me.btnOpenEnforcement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenEnforcement.FlatAppearance.BorderSize = 0
        Me.btnOpenEnforcement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenEnforcement.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenEnforcement.Location = New System.Drawing.Point(265, 34)
        Me.btnOpenEnforcement.Name = "btnOpenEnforcement"
        Me.btnOpenEnforcement.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenEnforcement.TabIndex = 5
        Me.btnOpenEnforcement.TabStop = False
        Me.btnOpenEnforcement.Text = "Open"
        Me.btnOpenEnforcement.UseVisualStyleBackColor = True
        '
        'btnOpenFacilitySummary
        '
        Me.btnOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenFacilitySummary.FlatAppearance.BorderSize = 0
        Me.btnOpenFacilitySummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFacilitySummary.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenFacilitySummary.Location = New System.Drawing.Point(105, 34)
        Me.btnOpenFacilitySummary.Name = "btnOpenFacilitySummary"
        Me.btnOpenFacilitySummary.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenFacilitySummary.TabIndex = 1
        Me.btnOpenFacilitySummary.TabStop = False
        Me.btnOpenFacilitySummary.Text = "Open"
        Me.btnOpenFacilitySummary.UseVisualStyleBackColor = True
        '
        'SbeapQuickAccessPanel
        '
        Me.SbeapQuickAccessPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.txtOpenSbeapCaseLog)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.btnOpenSbeapCaseLog)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.btnOpenSbeapClient)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.SbeapCaseLogNumberLabel)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.SbeapClientIDLabel)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.txtOpenSbeapClient)
        Me.SbeapQuickAccessPanel.Location = New System.Drawing.Point(483, 12)
        Me.SbeapQuickAccessPanel.Name = "SbeapQuickAccessPanel"
        Me.SbeapQuickAccessPanel.Size = New System.Drawing.Size(147, 102)
        Me.SbeapQuickAccessPanel.TabIndex = 12
        Me.SbeapQuickAccessPanel.Visible = False
        '
        'txtOpenSbeapCaseLog
        '
        Me.txtOpenSbeapCaseLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenSbeapCaseLog.Cue = "Case #"
        Me.txtOpenSbeapCaseLog.Location = New System.Drawing.Point(7, 78)
        Me.txtOpenSbeapCaseLog.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenSbeapCaseLog.MaxLength = 10
        Me.txtOpenSbeapCaseLog.Name = "txtOpenSbeapCaseLog"
        Me.txtOpenSbeapCaseLog.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenSbeapCaseLog.TabIndex = 2
        '
        'btnOpenSbeapCaseLog
        '
        Me.btnOpenSbeapCaseLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenSbeapCaseLog.FlatAppearance.BorderSize = 0
        Me.btnOpenSbeapCaseLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSbeapCaseLog.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenSbeapCaseLog.Location = New System.Drawing.Point(102, 76)
        Me.btnOpenSbeapCaseLog.Name = "btnOpenSbeapCaseLog"
        Me.btnOpenSbeapCaseLog.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenSbeapCaseLog.TabIndex = 3
        Me.btnOpenSbeapCaseLog.TabStop = False
        Me.btnOpenSbeapCaseLog.Text = "Open"
        Me.btnOpenSbeapCaseLog.UseVisualStyleBackColor = True
        '
        'btnOpenSbeapClient
        '
        Me.btnOpenSbeapClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenSbeapClient.FlatAppearance.BorderSize = 0
        Me.btnOpenSbeapClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSbeapClient.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenSbeapClient.Location = New System.Drawing.Point(102, 24)
        Me.btnOpenSbeapClient.Name = "btnOpenSbeapClient"
        Me.btnOpenSbeapClient.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenSbeapClient.TabIndex = 1
        Me.btnOpenSbeapClient.TabStop = False
        Me.btnOpenSbeapClient.Text = "Open"
        Me.btnOpenSbeapClient.UseVisualStyleBackColor = True
        '
        'SbeapCaseLogNumberLabel
        '
        Me.SbeapCaseLogNumberLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapCaseLogNumberLabel.AutoSize = True
        Me.SbeapCaseLogNumberLabel.Location = New System.Drawing.Point(4, 63)
        Me.SbeapCaseLogNumberLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SbeapCaseLogNumberLabel.Name = "SbeapCaseLogNumberLabel"
        Me.SbeapCaseLogNumberLabel.Size = New System.Drawing.Size(90, 13)
        Me.SbeapCaseLogNumberLabel.TabIndex = 268
        Me.SbeapCaseLogNumberLabel.Text = "SBEAP Case Log"
        '
        'SbeapClientIDLabel
        '
        Me.SbeapClientIDLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapClientIDLabel.AutoSize = True
        Me.SbeapClientIDLabel.Location = New System.Drawing.Point(4, 11)
        Me.SbeapClientIDLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SbeapClientIDLabel.Name = "SbeapClientIDLabel"
        Me.SbeapClientIDLabel.Size = New System.Drawing.Size(89, 13)
        Me.SbeapClientIDLabel.TabIndex = 259
        Me.SbeapClientIDLabel.Text = "SBEAP Customer"
        '
        'txtOpenSbeapClient
        '
        Me.txtOpenSbeapClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenSbeapClient.Cue = "Customer ID"
        Me.txtOpenSbeapClient.Location = New System.Drawing.Point(7, 26)
        Me.txtOpenSbeapClient.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenSbeapClient.MaxLength = 10
        Me.txtOpenSbeapClient.Name = "txtOpenSbeapClient"
        Me.txtOpenSbeapClient.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenSbeapClient.TabIndex = 0
        '
        'txtOpenTestLog
        '
        Me.txtOpenTestLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenTestLog.Cue = "Notification #"
        Me.txtOpenTestLog.Location = New System.Drawing.Point(331, 88)
        Me.txtOpenTestLog.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenTestLog.MaxLength = 10
        Me.txtOpenTestLog.Name = "txtOpenTestLog"
        Me.txtOpenTestLog.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenTestLog.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(328, 73)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 13)
        Me.Label8.TabIndex = 268
        Me.Label8.Text = "ISMPTest Notification"
        '
        'lblOpenFacilitySummary
        '
        Me.lblOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOpenFacilitySummary.AutoSize = True
        Me.lblOpenFacilitySummary.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblOpenFacilitySummary.Location = New System.Drawing.Point(7, 21)
        Me.lblOpenFacilitySummary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOpenFacilitySummary.Name = "lblOpenFacilitySummary"
        Me.lblOpenFacilitySummary.Size = New System.Drawing.Size(85, 13)
        Me.lblOpenFacilitySummary.TabIndex = 265
        Me.lblOpenFacilitySummary.Text = "Facility Summary"
        '
        'txtOpenFacilitySummary
        '
        Me.txtOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenFacilitySummary.Cue = "AIRS #"
        Me.txtOpenFacilitySummary.Location = New System.Drawing.Point(10, 36)
        Me.txtOpenFacilitySummary.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenFacilitySummary.MaxLength = 9
        Me.txtOpenFacilitySummary.Name = "txtOpenFacilitySummary"
        Me.txtOpenFacilitySummary.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenFacilitySummary.TabIndex = 0
        Me.txtOpenFacilitySummary.Tag = ""
        '
        'txtOpenSscpItem
        '
        Me.txtOpenSscpItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenSscpItem.Cue = "Item #"
        Me.txtOpenSscpItem.Location = New System.Drawing.Point(170, 88)
        Me.txtOpenSscpItem.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenSscpItem.MaxLength = 10
        Me.txtOpenSscpItem.Name = "txtOpenSscpItem"
        Me.txtOpenSscpItem.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenSscpItem.TabIndex = 6
        '
        'lblOpenSscpItem
        '
        Me.lblOpenSscpItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOpenSscpItem.AutoSize = True
        Me.lblOpenSscpItem.Location = New System.Drawing.Point(167, 73)
        Me.lblOpenSscpItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOpenSscpItem.Name = "lblOpenSscpItem"
        Me.lblOpenSscpItem.Size = New System.Drawing.Size(87, 13)
        Me.lblOpenSscpItem.TabIndex = 261
        Me.lblOpenSscpItem.Text = "SSCP Work Item"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 73)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(122, 13)
        Me.Label6.TabIndex = 259
        Me.Label6.Text = "SSPP Permit Application"
        '
        'txtOpenApplication
        '
        Me.txtOpenApplication.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenApplication.Cue = "Application #"
        Me.txtOpenApplication.Location = New System.Drawing.Point(10, 88)
        Me.txtOpenApplication.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenApplication.MaxLength = 10
        Me.txtOpenApplication.Name = "txtOpenApplication"
        Me.txtOpenApplication.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenApplication.TabIndex = 2
        '
        'lblOpenEnforcement
        '
        Me.lblOpenEnforcement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOpenEnforcement.AutoSize = True
        Me.lblOpenEnforcement.Location = New System.Drawing.Point(167, 21)
        Me.lblOpenEnforcement.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOpenEnforcement.Name = "lblOpenEnforcement"
        Me.lblOpenEnforcement.Size = New System.Drawing.Size(98, 13)
        Me.lblOpenEnforcement.TabIndex = 255
        Me.lblOpenEnforcement.Text = "SSCP Enforcement"
        '
        'txtOpenEnforcement
        '
        Me.txtOpenEnforcement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenEnforcement.Cue = "Enforcement #"
        Me.txtOpenEnforcement.Location = New System.Drawing.Point(170, 36)
        Me.txtOpenEnforcement.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenEnforcement.MaxLength = 8
        Me.txtOpenEnforcement.Name = "txtOpenEnforcement"
        Me.txtOpenEnforcement.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenEnforcement.TabIndex = 4
        '
        'lblOpenTestReport
        '
        Me.lblOpenTestReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOpenTestReport.AutoSize = True
        Me.lblOpenTestReport.Location = New System.Drawing.Point(328, 21)
        Me.lblOpenTestReport.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOpenTestReport.Name = "lblOpenTestReport"
        Me.lblOpenTestReport.Size = New System.Drawing.Size(92, 13)
        Me.lblOpenTestReport.TabIndex = 249
        Me.lblOpenTestReport.Text = "ISMP Test Report"
        '
        'txtOpenTestReport
        '
        Me.txtOpenTestReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenTestReport.Cue = "Reference #"
        Me.txtOpenTestReport.Location = New System.Drawing.Point(330, 36)
        Me.txtOpenTestReport.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenTestReport.MaxLength = 9
        Me.txtOpenTestReport.Name = "txtOpenTestReport"
        Me.txtOpenTestReport.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenTestReport.TabIndex = 8
        Me.txtOpenTestReport.Tag = ""
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnlName, Me.pnlProgram, Me.pnlDate, Me.pnlDbEnv})
        Me.StatusStrip1.Location = New System.Drawing.Point(118, 374)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(686, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.Stretch = False
        Me.StatusStrip1.TabIndex = 264
        '
        'pnlName
        '
        Me.pnlName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.pnlName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnlName.Name = "pnlName"
        Me.pnlName.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnlName.Size = New System.Drawing.Size(47, 19)
        Me.pnlName.Text = "Name"
        '
        'pnlProgram
        '
        Me.pnlProgram.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnlProgram.Margin = New System.Windows.Forms.Padding(2, 3, 0, 2)
        Me.pnlProgram.Name = "pnlProgram"
        Me.pnlProgram.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnlProgram.Size = New System.Drawing.Size(504, 19)
        Me.pnlProgram.Spring = True
        Me.pnlProgram.Text = "Program"
        Me.pnlProgram.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDate
        '
        Me.pnlDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.pnlDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnlDate.Size = New System.Drawing.Size(39, 19)
        Me.pnlDate.Text = "Date"
        '
        'pnlDbEnv
        '
        Me.pnlDbEnv.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.pnlDbEnv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnlDbEnv.Name = "pnlDbEnv"
        Me.pnlDbEnv.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnlDbEnv.Size = New System.Drawing.Size(83, 19)
        Me.pnlDbEnv.Text = "Environment"
        '
        'rdbAllView
        '
        Me.rdbAllView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbAllView.AutoSize = True
        Me.rdbAllView.Location = New System.Drawing.Point(158, 8)
        Me.rdbAllView.Name = "rdbAllView"
        Me.rdbAllView.Size = New System.Drawing.Size(62, 17)
        Me.rdbAllView.TabIndex = 4
        Me.rdbAllView.Text = "View All"
        Me.rdbAllView.UseVisualStyleBackColor = True
        '
        'rdbUnitView
        '
        Me.rdbUnitView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbUnitView.AutoSize = True
        Me.rdbUnitView.Location = New System.Drawing.Point(82, 8)
        Me.rdbUnitView.Name = "rdbUnitView"
        Me.rdbUnitView.Size = New System.Drawing.Size(70, 17)
        Me.rdbUnitView.TabIndex = 3
        Me.rdbUnitView.Text = "View Unit"
        Me.rdbUnitView.UseVisualStyleBackColor = True
        '
        'rdbStaffView
        '
        Me.rdbStaffView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbStaffView.AutoSize = True
        Me.rdbStaffView.Checked = True
        Me.rdbStaffView.Location = New System.Drawing.Point(3, 8)
        Me.rdbStaffView.Name = "rdbStaffView"
        Me.rdbStaffView.Size = New System.Drawing.Size(73, 17)
        Me.rdbStaffView.TabIndex = 2
        Me.rdbStaffView.TabStop = True
        Me.rdbStaffView.Text = "View Staff"
        Me.rdbStaffView.UseVisualStyleBackColor = True
        '
        'btnLoadNavWorkList
        '
        Me.btnLoadNavWorkList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadNavWorkList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadNavWorkList.Enabled = False
        Me.btnLoadNavWorkList.Location = New System.Drawing.Point(272, 5)
        Me.btnLoadNavWorkList.Name = "btnLoadNavWorkList"
        Me.btnLoadNavWorkList.Size = New System.Drawing.Size(61, 23)
        Me.btnLoadNavWorkList.TabIndex = 1
        Me.btnLoadNavWorkList.Text = "Loading…"
        Me.btnLoadNavWorkList.UseVisualStyleBackColor = True
        '
        'lblWorkViewerContext
        '
        Me.lblWorkViewerContext.AutoSize = True
        Me.lblWorkViewerContext.Location = New System.Drawing.Point(5, 10)
        Me.lblWorkViewerContext.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblWorkViewerContext.Name = "lblWorkViewerContext"
        Me.lblWorkViewerContext.Size = New System.Drawing.Size(63, 13)
        Me.lblWorkViewerContext.TabIndex = 294
        Me.lblWorkViewerContext.Text = "Current List:"
        '
        'lblResultsCount
        '
        Me.lblResultsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResultsCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultsCount.Location = New System.Drawing.Point(599, 10)
        Me.lblResultsCount.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(83, 15)
        Me.lblResultsCount.TabIndex = 253
        Me.lblResultsCount.Text = "99999 results"
        Me.lblResultsCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblResultsCount.Visible = False
        '
        'bgrLoadWorkViewer
        '
        Me.bgrLoadWorkViewer.WorkerSupportsCancellation = True
        '
        'lblMessageLabel
        '
        Me.lblMessageLabel.AutoSize = True
        Me.lblMessageLabel.Location = New System.Drawing.Point(125, 59)
        Me.lblMessageLabel.Name = "lblMessageLabel"
        Me.lblMessageLabel.Size = New System.Drawing.Size(108, 13)
        Me.lblMessageLabel.TabIndex = 124
        Me.lblMessageLabel.Text = "Message placeholder"
        Me.lblMessageLabel.Visible = False
        '
        'bgrUserPermissions
        '
        Me.bgrUserPermissions.WorkerSupportsCancellation = True
        '
        'pnlCurrentList
        '
        Me.pnlCurrentList.Controls.Add(Me.NavWorkListChangerPanel)
        Me.pnlCurrentList.Controls.Add(Me.lblResultsCount)
        Me.pnlCurrentList.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCurrentList.Location = New System.Drawing.Point(118, 220)
        Me.pnlCurrentList.Name = "pnlCurrentList"
        Me.pnlCurrentList.Size = New System.Drawing.Size(686, 37)
        Me.pnlCurrentList.TabIndex = 1
        '
        'NavWorkListChangerPanel
        '
        Me.NavWorkListChangerPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NavWorkListChangerPanel.Controls.Add(Me.cboNavWorkListContext)
        Me.NavWorkListChangerPanel.Controls.Add(Me.btnLoadNavWorkList)
        Me.NavWorkListChangerPanel.Controls.Add(Me.lblWorkViewerContext)
        Me.NavWorkListChangerPanel.Controls.Add(Me.NavWorkListScopePanel)
        Me.NavWorkListChangerPanel.Location = New System.Drawing.Point(0, 0)
        Me.NavWorkListChangerPanel.MaximumSize = New System.Drawing.Size(630, 37)
        Me.NavWorkListChangerPanel.Name = "NavWorkListChangerPanel"
        Me.NavWorkListChangerPanel.Size = New System.Drawing.Size(594, 37)
        Me.NavWorkListChangerPanel.TabIndex = 296
        '
        'cboNavWorkListContext
        '
        Me.cboNavWorkListContext.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNavWorkListContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNavWorkListContext.FormattingEnabled = True
        Me.cboNavWorkListContext.Location = New System.Drawing.Point(73, 6)
        Me.cboNavWorkListContext.Name = "cboNavWorkListContext"
        Me.cboNavWorkListContext.Size = New System.Drawing.Size(193, 21)
        Me.cboNavWorkListContext.TabIndex = 266
        '
        'NavWorkListScopePanel
        '
        Me.NavWorkListScopePanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NavWorkListScopePanel.Controls.Add(Me.rdbStaffView)
        Me.NavWorkListScopePanel.Controls.Add(Me.rdbUnitView)
        Me.NavWorkListScopePanel.Controls.Add(Me.rdbAllView)
        Me.NavWorkListScopePanel.Location = New System.Drawing.Point(342, 0)
        Me.NavWorkListScopePanel.Name = "NavWorkListScopePanel"
        Me.NavWorkListScopePanel.Size = New System.Drawing.Size(252, 30)
        Me.NavWorkListScopePanel.TabIndex = 295
        '
        'dgvWorkViewer
        '
        Me.dgvWorkViewer.AllowUserToAddRows = False
        Me.dgvWorkViewer.AllowUserToDeleteRows = False
        Me.dgvWorkViewer.AllowUserToOrderColumns = True
        Me.dgvWorkViewer.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvWorkViewer.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvWorkViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvWorkViewer.ColumnHeadersHeight = 35
        Me.dgvWorkViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWorkViewer.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvWorkViewer.Location = New System.Drawing.Point(118, 33)
        Me.dgvWorkViewer.Name = "dgvWorkViewer"
        Me.dgvWorkViewer.ReadOnly = True
        Me.dgvWorkViewer.RowHeadersVisible = False
        Me.dgvWorkViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvWorkViewer.Size = New System.Drawing.Size(686, 187)
        Me.dgvWorkViewer.TabIndex = 4
        '
        'IAIPNavigation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 398)
        Me.Controls.Add(Me.dgvWorkViewer)
        Me.Controls.Add(Me.pnlCurrentList)
        Me.Controls.Add(Me.grpQuickAccess)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.flpNavButtons)
        Me.Controls.Add(Me.lblMessageLabel)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(762, 330)
        Me.Name = "IAIPNavigation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IAIP Navigation Screen"
        Me.grpQuickAccess.ResumeLayout(False)
        Me.grpQuickAccess.PerformLayout()
        Me.SbeapQuickAccessPanel.ResumeLayout(False)
        Me.SbeapQuickAccessPanel.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.pnlCurrentList.ResumeLayout(False)
        Me.NavWorkListChangerPanel.ResumeLayout(False)
        Me.NavWorkListChangerPanel.PerformLayout()
        Me.NavWorkListScopePanel.ResumeLayout(False)
        Me.NavWorkListScopePanel.PerformLayout()
        CType(Me.dgvWorkViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents mmiTools As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAbout As System.Windows.Forms.MenuItem
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents grpQuickAccess As System.Windows.Forms.GroupBox
    Friend WithEvents lblOpenSscpItem As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblOpenEnforcement As System.Windows.Forms.Label
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents lblOpenTestReport As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnlProgram As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnlName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnlDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblOpenFacilitySummary As System.Windows.Forms.Label
    Friend WithEvents pnlDbEnv As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblMessageLabel As System.Windows.Forms.Label
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnLoadNavWorkList As System.Windows.Forms.Button
    Friend WithEvents lblWorkViewerContext As System.Windows.Forms.Label
    Friend WithEvents rdbAllView As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitView As System.Windows.Forms.RadioButton
    Friend WithEvents rdbStaffView As System.Windows.Forms.RadioButton
    Friend WithEvents mmiExport As System.Windows.Forms.MenuItem
    Friend WithEvents mmiSeparator1 As System.Windows.Forms.MenuItem
    Friend WithEvents TestingMenu As System.Windows.Forms.MenuItem
    Friend WithEvents mmiResetForm As System.Windows.Forms.MenuItem
    Friend WithEvents pnlCurrentList As System.Windows.Forms.Panel
    Friend WithEvents NavWorkListScopePanel As System.Windows.Forms.Panel
    Friend WithEvents flpNavButtons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents SbeapCaseLogNumberLabel As System.Windows.Forms.Label
    Friend WithEvents SbeapClientIDLabel As System.Windows.Forms.Label
    Friend WithEvents SbeapQuickAccessPanel As System.Windows.Forms.Panel
    Friend WithEvents btnOpenFacilitySummary As System.Windows.Forms.Button
    Friend WithEvents btnOpenTestReport As System.Windows.Forms.Button
    Friend WithEvents btnOpenApplication As System.Windows.Forms.Button
    Friend WithEvents btnOpenTestLog As System.Windows.Forms.Button
    Friend WithEvents btnOpenSscpItem As System.Windows.Forms.Button
    Friend WithEvents btnOpenEnforcement As System.Windows.Forms.Button
    Friend WithEvents btnOpenSbeapCaseLog As System.Windows.Forms.Button
    Friend WithEvents btnOpenSbeapClient As System.Windows.Forms.Button
    Friend WithEvents txtOpenSscpItem As Iaip.CueTextBox
    Friend WithEvents txtOpenApplication As Iaip.CueTextBox
    Friend WithEvents txtOpenEnforcement As Iaip.CueTextBox
    Friend WithEvents txtOpenTestReport As Iaip.CueTextBox
    Friend WithEvents txtOpenFacilitySummary As Iaip.CueTextBox
    Friend WithEvents txtOpenTestLog As Iaip.CueTextBox
    Friend WithEvents txtOpenSbeapCaseLog As Iaip.CueTextBox
    Friend WithEvents txtOpenSbeapClient As Iaip.CueTextBox
    Private WithEvents bgrLoadWorkViewer As System.ComponentModel.BackgroundWorker
    Private WithEvents bgrUserPermissions As System.ComponentModel.BackgroundWorker
    Friend WithEvents dgvWorkViewer As System.Windows.Forms.DataGridView
    Friend WithEvents NavWorkListChangerPanel As System.Windows.Forms.Panel
    Friend WithEvents RunTest As System.Windows.Forms.MenuItem
    Friend WithEvents ProfileMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents UpdateProfile As System.Windows.Forms.MenuItem
    Friend WithEvents ChangePassword As System.Windows.Forms.MenuItem
    Friend WithEvents LogOut As System.Windows.Forms.MenuItem
    Friend WithEvents UsernameDisplay As MenuItem
    Friend WithEvents cboNavWorkListContext As ComboBox
    Friend WithEvents UsernameSeparator As MenuItem
End Class
