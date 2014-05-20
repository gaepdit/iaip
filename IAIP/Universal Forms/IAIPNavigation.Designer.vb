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
        Me.components = New System.ComponentModel.Container
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiFile = New System.Windows.Forms.MenuItem
        Me.mmiExit = New System.Windows.Forms.MenuItem
        Me.mmiTools = New System.Windows.Forms.MenuItem
        Me.mmiExport = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem
        Me.mmiResetForm = New System.Windows.Forms.MenuItem
        Me.mmiSeparator1 = New System.Windows.Forms.MenuItem
        Me.mmiAbout = New System.Windows.Forms.MenuItem
        Me.mmiTesting = New System.Windows.Forms.MenuItem
        Me.mmiPing = New System.Windows.Forms.MenuItem
        Me.lblTitle = New System.Windows.Forms.Label
        Me.flpNavButtons = New System.Windows.Forms.FlowLayoutPanel
        Me.grpQuickAccess = New System.Windows.Forms.GroupBox
        Me.btnOpenTestReport = New System.Windows.Forms.Button
        Me.btnOpenApplication = New System.Windows.Forms.Button
        Me.btnOpenTestLog = New System.Windows.Forms.Button
        Me.btnOpenSscpItem = New System.Windows.Forms.Button
        Me.btnOpenEnforcement = New System.Windows.Forms.Button
        Me.btnOpenFacilitySummary = New System.Windows.Forms.Button
        Me.SbeapQuickAccessPanel = New System.Windows.Forms.Panel
        Me.txtOpenSbeapCaseLog = New System.Windows.Forms.TextBox
        Me.btnOpenSbeapCaseLog = New System.Windows.Forms.Button
        Me.btnOpenSbeapClient = New System.Windows.Forms.Button
        Me.SbeapCaseLogNumberLabel = New System.Windows.Forms.Label
        Me.SbeapClientIDLabel = New System.Windows.Forms.Label
        Me.txtOpenSbeapClient = New System.Windows.Forms.TextBox
        Me.txtOpenTestLog = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblOpenFacilitySummary = New System.Windows.Forms.Label
        Me.txtOpenFacilitySummary = New System.Windows.Forms.TextBox
        Me.txtOpenSscpItem = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtOpenApplication = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtOpenEnforcement = New System.Windows.Forms.TextBox
        Me.lblOpenTestReport = New System.Windows.Forms.Label
        Me.txtOpenTestReport = New System.Windows.Forms.TextBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.rdbPMView = New System.Windows.Forms.RadioButton
        Me.rdbUCView = New System.Windows.Forms.RadioButton
        Me.rdbStaffView = New System.Windows.Forms.RadioButton
        Me.btnChangeWorkViewerContext = New System.Windows.Forms.Button
        Me.lblWorkViewerContext = New System.Windows.Forms.Label
        Me.cboWorkViewerContext = New System.Windows.Forms.ComboBox
        Me.lblResultsCount = New System.Windows.Forms.Label
        Me.dgvWorkViewer = New System.Windows.Forms.DataGridView
        Me.bgrLoadWorkViewer = New System.ComponentModel.BackgroundWorker
        Me.lblMessageLabel = New System.Windows.Forms.Label
        Me.bgrLoadButtons = New System.ComponentModel.BackgroundWorker
        Me.pnlCurrentList = New System.Windows.Forms.Panel
        Me.pnlContextSubView = New System.Windows.Forms.Panel
        Me.grpQuickAccess.SuspendLayout()
        Me.SbeapQuickAccessPanel.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgvWorkViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrentList.SuspendLayout()
        Me.pnlContextSubView.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiFile, Me.mmiTools, Me.mmiHelp, Me.mmiTesting})
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
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
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
        Me.mmiResetForm.Text = "&Reset All Forms"
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
        'mmiTesting
        '
        Me.mmiTesting.Index = 3
        Me.mmiTesting.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiPing})
        Me.mmiTesting.Text = "Testing"
        Me.mmiTesting.Visible = False
        '
        'mmiPing
        '
        Me.mmiPing.Index = 0
        Me.mmiPing.Text = "PingDB"
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
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
        Me.flpNavButtons.Size = New System.Drawing.Size(118, 328)
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
        Me.grpQuickAccess.Controls.Add(Me.Label2)
        Me.grpQuickAccess.Controls.Add(Me.Label6)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenApplication)
        Me.grpQuickAccess.Controls.Add(Me.Label5)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenEnforcement)
        Me.grpQuickAccess.Controls.Add(Me.lblOpenTestReport)
        Me.grpQuickAccess.Controls.Add(Me.txtOpenTestReport)
        Me.grpQuickAccess.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpQuickAccess.Location = New System.Drawing.Point(118, 204)
        Me.grpQuickAccess.Name = "grpQuickAccess"
        Me.grpQuickAccess.Size = New System.Drawing.Size(686, 100)
        Me.grpQuickAccess.TabIndex = 2
        Me.grpQuickAccess.TabStop = False
        Me.grpQuickAccess.Text = "Quick Access"
        '
        'btnOpenTestReport
        '
        Me.btnOpenTestReport.FlatAppearance.BorderSize = 0
        Me.btnOpenTestReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenTestReport.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenTestReport.Location = New System.Drawing.Point(105, 71)
        Me.btnOpenTestReport.Name = "btnOpenTestReport"
        Me.btnOpenTestReport.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenTestReport.TabIndex = 3
        Me.btnOpenTestReport.Text = "Open"
        Me.btnOpenTestReport.UseVisualStyleBackColor = True
        '
        'btnOpenApplication
        '
        Me.btnOpenApplication.FlatAppearance.BorderSize = 0
        Me.btnOpenApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenApplication.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenApplication.Location = New System.Drawing.Point(425, 31)
        Me.btnOpenApplication.Name = "btnOpenApplication"
        Me.btnOpenApplication.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenApplication.TabIndex = 9
        Me.btnOpenApplication.Text = "Open"
        Me.btnOpenApplication.UseVisualStyleBackColor = True
        '
        'btnOpenTestLog
        '
        Me.btnOpenTestLog.FlatAppearance.BorderSize = 0
        Me.btnOpenTestLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenTestLog.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenTestLog.Location = New System.Drawing.Point(425, 71)
        Me.btnOpenTestLog.Name = "btnOpenTestLog"
        Me.btnOpenTestLog.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenTestLog.TabIndex = 11
        Me.btnOpenTestLog.Text = "Open"
        Me.btnOpenTestLog.UseVisualStyleBackColor = True
        '
        'btnOpenSscpItem
        '
        Me.btnOpenSscpItem.FlatAppearance.BorderSize = 0
        Me.btnOpenSscpItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSscpItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenSscpItem.Location = New System.Drawing.Point(265, 71)
        Me.btnOpenSscpItem.Name = "btnOpenSscpItem"
        Me.btnOpenSscpItem.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenSscpItem.TabIndex = 7
        Me.btnOpenSscpItem.Text = "Open"
        Me.btnOpenSscpItem.UseVisualStyleBackColor = True
        '
        'btnOpenEnforcement
        '
        Me.btnOpenEnforcement.FlatAppearance.BorderSize = 0
        Me.btnOpenEnforcement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenEnforcement.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenEnforcement.Location = New System.Drawing.Point(265, 31)
        Me.btnOpenEnforcement.Name = "btnOpenEnforcement"
        Me.btnOpenEnforcement.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenEnforcement.TabIndex = 5
        Me.btnOpenEnforcement.Text = "Open"
        Me.btnOpenEnforcement.UseVisualStyleBackColor = True
        '
        'btnOpenFacilitySummary
        '
        Me.btnOpenFacilitySummary.FlatAppearance.BorderSize = 0
        Me.btnOpenFacilitySummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFacilitySummary.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenFacilitySummary.Location = New System.Drawing.Point(105, 31)
        Me.btnOpenFacilitySummary.Name = "btnOpenFacilitySummary"
        Me.btnOpenFacilitySummary.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenFacilitySummary.TabIndex = 1
        Me.btnOpenFacilitySummary.Text = "Open"
        Me.btnOpenFacilitySummary.UseVisualStyleBackColor = True
        '
        'SbeapQuickAccessPanel
        '
        Me.SbeapQuickAccessPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.txtOpenSbeapCaseLog)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.btnOpenSbeapCaseLog)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.btnOpenSbeapClient)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.SbeapCaseLogNumberLabel)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.SbeapClientIDLabel)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.txtOpenSbeapClient)
        Me.SbeapQuickAccessPanel.Enabled = False
        Me.SbeapQuickAccessPanel.Location = New System.Drawing.Point(483, 16)
        Me.SbeapQuickAccessPanel.Name = "SbeapQuickAccessPanel"
        Me.SbeapQuickAccessPanel.Size = New System.Drawing.Size(147, 81)
        Me.SbeapQuickAccessPanel.TabIndex = 12
        Me.SbeapQuickAccessPanel.Visible = False
        '
        'txtOpenSbeapCaseLog
        '
        Me.txtOpenSbeapCaseLog.Location = New System.Drawing.Point(7, 57)
        Me.txtOpenSbeapCaseLog.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenSbeapCaseLog.MaxLength = 10
        Me.txtOpenSbeapCaseLog.Name = "txtOpenSbeapCaseLog"
        Me.txtOpenSbeapCaseLog.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenSbeapCaseLog.TabIndex = 2
        '
        'btnOpenSbeapCaseLog
        '
        Me.btnOpenSbeapCaseLog.FlatAppearance.BorderSize = 0
        Me.btnOpenSbeapCaseLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSbeapCaseLog.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenSbeapCaseLog.Location = New System.Drawing.Point(102, 55)
        Me.btnOpenSbeapCaseLog.Name = "btnOpenSbeapCaseLog"
        Me.btnOpenSbeapCaseLog.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenSbeapCaseLog.TabIndex = 3
        Me.btnOpenSbeapCaseLog.Text = "Open"
        Me.btnOpenSbeapCaseLog.UseVisualStyleBackColor = True
        '
        'btnOpenSbeapClient
        '
        Me.btnOpenSbeapClient.FlatAppearance.BorderSize = 0
        Me.btnOpenSbeapClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSbeapClient.ForeColor = System.Drawing.SystemColors.GrayText
        Me.btnOpenSbeapClient.Location = New System.Drawing.Point(102, 15)
        Me.btnOpenSbeapClient.Name = "btnOpenSbeapClient"
        Me.btnOpenSbeapClient.Size = New System.Drawing.Size(43, 23)
        Me.btnOpenSbeapClient.TabIndex = 1
        Me.btnOpenSbeapClient.Text = "Open"
        Me.btnOpenSbeapClient.UseVisualStyleBackColor = True
        '
        'SbeapCaseLogNumberLabel
        '
        Me.SbeapCaseLogNumberLabel.AutoSize = True
        Me.SbeapCaseLogNumberLabel.Location = New System.Drawing.Point(4, 42)
        Me.SbeapCaseLogNumberLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SbeapCaseLogNumberLabel.Name = "SbeapCaseLogNumberLabel"
        Me.SbeapCaseLogNumberLabel.Size = New System.Drawing.Size(100, 13)
        Me.SbeapCaseLogNumberLabel.TabIndex = 268
        Me.SbeapCaseLogNumberLabel.Text = "SBEAP Case Log #"
        '
        'SbeapClientIDLabel
        '
        Me.SbeapClientIDLabel.AutoSize = True
        Me.SbeapClientIDLabel.Location = New System.Drawing.Point(4, 2)
        Me.SbeapClientIDLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SbeapClientIDLabel.Name = "SbeapClientIDLabel"
        Me.SbeapClientIDLabel.Size = New System.Drawing.Size(99, 13)
        Me.SbeapClientIDLabel.TabIndex = 259
        Me.SbeapClientIDLabel.Text = "SBEAP Customer #"
        '
        'txtOpenSbeapClient
        '
        Me.txtOpenSbeapClient.Location = New System.Drawing.Point(7, 17)
        Me.txtOpenSbeapClient.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenSbeapClient.MaxLength = 10
        Me.txtOpenSbeapClient.Name = "txtOpenSbeapClient"
        Me.txtOpenSbeapClient.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenSbeapClient.TabIndex = 0
        '
        'txtOpenTestLog
        '
        Me.txtOpenTestLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenTestLog.Location = New System.Drawing.Point(330, 73)
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
        Me.Label8.Location = New System.Drawing.Point(327, 58)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 268
        Me.Label8.Text = "ISMP Test Log #"
        '
        'lblOpenFacilitySummary
        '
        Me.lblOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOpenFacilitySummary.AutoSize = True
        Me.lblOpenFacilitySummary.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblOpenFacilitySummary.Location = New System.Drawing.Point(7, 18)
        Me.lblOpenFacilitySummary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOpenFacilitySummary.Name = "lblOpenFacilitySummary"
        Me.lblOpenFacilitySummary.Size = New System.Drawing.Size(77, 13)
        Me.lblOpenFacilitySummary.TabIndex = 265
        Me.lblOpenFacilitySummary.Text = "Facility AIRS #"
        '
        'txtOpenFacilitySummary
        '
        Me.txtOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenFacilitySummary.Location = New System.Drawing.Point(10, 33)
        Me.txtOpenFacilitySummary.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenFacilitySummary.MaxLength = 8
        Me.txtOpenFacilitySummary.Name = "txtOpenFacilitySummary"
        Me.txtOpenFacilitySummary.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenFacilitySummary.TabIndex = 0
        Me.txtOpenFacilitySummary.Tag = ""
        '
        'txtOpenSscpItem
        '
        Me.txtOpenSscpItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenSscpItem.Location = New System.Drawing.Point(170, 73)
        Me.txtOpenSscpItem.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenSscpItem.MaxLength = 10
        Me.txtOpenSscpItem.Name = "txtOpenSscpItem"
        Me.txtOpenSscpItem.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenSscpItem.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(167, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 261
        Me.Label2.Text = "SSCP Item #"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(327, 18)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 259
        Me.Label6.Text = "SSPP Application #"
        '
        'txtOpenApplication
        '
        Me.txtOpenApplication.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenApplication.Location = New System.Drawing.Point(330, 33)
        Me.txtOpenApplication.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenApplication.MaxLength = 10
        Me.txtOpenApplication.Name = "txtOpenApplication"
        Me.txtOpenApplication.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenApplication.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(167, 18)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 255
        Me.Label5.Text = "Enforcement #"
        '
        'txtOpenEnforcement
        '
        Me.txtOpenEnforcement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenEnforcement.Location = New System.Drawing.Point(170, 33)
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
        Me.lblOpenTestReport.Location = New System.Drawing.Point(7, 58)
        Me.lblOpenTestReport.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOpenTestReport.Name = "lblOpenTestReport"
        Me.lblOpenTestReport.Size = New System.Drawing.Size(102, 13)
        Me.lblOpenTestReport.TabIndex = 249
        Me.lblOpenTestReport.Text = "ISMP Test Report #"
        '
        'txtOpenTestReport
        '
        Me.txtOpenTestReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOpenTestReport.Location = New System.Drawing.Point(10, 73)
        Me.txtOpenTestReport.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOpenTestReport.MaxLength = 9
        Me.txtOpenTestReport.Name = "txtOpenTestReport"
        Me.txtOpenTestReport.Size = New System.Drawing.Size(90, 20)
        Me.txtOpenTestReport.TabIndex = 2
        Me.txtOpenTestReport.Tag = ""
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3, Me.pnl4})
        Me.StatusStrip1.Location = New System.Drawing.Point(118, 304)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(686, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.Stretch = False
        Me.StatusStrip1.TabIndex = 264
        '
        'pnl1
        '
        Me.pnl1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnl1.Size = New System.Drawing.Size(506, 19)
        Me.pnl1.Spring = True
        Me.pnl1.Text = "Program"
        Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl2
        '
        Me.pnl2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.pnl2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnl2.Size = New System.Drawing.Size(47, 19)
        Me.pnl2.Text = "Name"
        '
        'pnl3
        '
        Me.pnl3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.pnl3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnl3.Size = New System.Drawing.Size(39, 19)
        Me.pnl3.Text = "Date"
        '
        'pnl4
        '
        Me.pnl4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.pnl4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl4.Name = "pnl4"
        Me.pnl4.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnl4.Size = New System.Drawing.Size(83, 19)
        Me.pnl4.Text = "Environment"
        '
        'rdbPMView
        '
        Me.rdbPMView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbPMView.AutoSize = True
        Me.rdbPMView.Location = New System.Drawing.Point(158, 6)
        Me.rdbPMView.Name = "rdbPMView"
        Me.rdbPMView.Size = New System.Drawing.Size(90, 17)
        Me.rdbPMView.TabIndex = 4
        Me.rdbPMView.Text = "Program View"
        Me.rdbPMView.UseVisualStyleBackColor = True
        '
        'rdbUCView
        '
        Me.rdbUCView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbUCView.AutoSize = True
        Me.rdbUCView.Location = New System.Drawing.Point(82, 6)
        Me.rdbUCView.Name = "rdbUCView"
        Me.rdbUCView.Size = New System.Drawing.Size(70, 17)
        Me.rdbUCView.TabIndex = 3
        Me.rdbUCView.Text = "Unit View"
        Me.rdbUCView.UseVisualStyleBackColor = True
        '
        'rdbStaffView
        '
        Me.rdbStaffView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbStaffView.AutoSize = True
        Me.rdbStaffView.Checked = True
        Me.rdbStaffView.Location = New System.Drawing.Point(3, 6)
        Me.rdbStaffView.Name = "rdbStaffView"
        Me.rdbStaffView.Size = New System.Drawing.Size(73, 17)
        Me.rdbStaffView.TabIndex = 2
        Me.rdbStaffView.TabStop = True
        Me.rdbStaffView.Text = "Staff View"
        Me.rdbStaffView.UseVisualStyleBackColor = True
        '
        'btnChangeWorkViewerContext
        '
        Me.btnChangeWorkViewerContext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangeWorkViewerContext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnChangeWorkViewerContext.Location = New System.Drawing.Point(278, 5)
        Me.btnChangeWorkViewerContext.Name = "btnChangeWorkViewerContext"
        Me.btnChangeWorkViewerContext.Size = New System.Drawing.Size(61, 23)
        Me.btnChangeWorkViewerContext.TabIndex = 1
        Me.btnChangeWorkViewerContext.Text = "Loading…"
        Me.btnChangeWorkViewerContext.UseVisualStyleBackColor = True
        '
        'lblWorkViewerContext
        '
        Me.lblWorkViewerContext.AutoSize = True
        Me.lblWorkViewerContext.Location = New System.Drawing.Point(4, 9)
        Me.lblWorkViewerContext.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblWorkViewerContext.Name = "lblWorkViewerContext"
        Me.lblWorkViewerContext.Size = New System.Drawing.Size(63, 13)
        Me.lblWorkViewerContext.TabIndex = 294
        Me.lblWorkViewerContext.Text = "Current List:"
        '
        'cboWorkViewerContext
        '
        Me.cboWorkViewerContext.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboWorkViewerContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkViewerContext.FormattingEnabled = True
        Me.cboWorkViewerContext.Location = New System.Drawing.Point(72, 6)
        Me.cboWorkViewerContext.Name = "cboWorkViewerContext"
        Me.cboWorkViewerContext.Size = New System.Drawing.Size(200, 21)
        Me.cboWorkViewerContext.TabIndex = 0
        '
        'lblResultsCount
        '
        Me.lblResultsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResultsCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultsCount.Location = New System.Drawing.Point(597, 9)
        Me.lblResultsCount.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(83, 15)
        Me.lblResultsCount.TabIndex = 253
        Me.lblResultsCount.Text = "99999 results"
        Me.lblResultsCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblResultsCount.Visible = False
        '
        'dgvWorkViewer
        '
        Me.dgvWorkViewer.AllowUserToAddRows = False
        Me.dgvWorkViewer.AllowUserToDeleteRows = False
        Me.dgvWorkViewer.AllowUserToOrderColumns = True
        Me.dgvWorkViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvWorkViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWorkViewer.Location = New System.Drawing.Point(118, 33)
        Me.dgvWorkViewer.Name = "dgvWorkViewer"
        Me.dgvWorkViewer.ReadOnly = True
        Me.dgvWorkViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvWorkViewer.Size = New System.Drawing.Size(686, 134)
        Me.dgvWorkViewer.TabIndex = 4
        '
        'bgrLoadWorkViewer
        '
        '
        'lblMessageLabel
        '
        Me.lblMessageLabel.AutoSize = True
        Me.lblMessageLabel.Location = New System.Drawing.Point(125, 73)
        Me.lblMessageLabel.Name = "lblMessageLabel"
        Me.lblMessageLabel.Size = New System.Drawing.Size(108, 13)
        Me.lblMessageLabel.TabIndex = 124
        Me.lblMessageLabel.Text = "Message placeholder"
        Me.lblMessageLabel.Visible = False
        '
        'bgrLoadButtons
        '
        '
        'pnlCurrentList
        '
        Me.pnlCurrentList.Controls.Add(Me.btnChangeWorkViewerContext)
        Me.pnlCurrentList.Controls.Add(Me.pnlContextSubView)
        Me.pnlCurrentList.Controls.Add(Me.lblWorkViewerContext)
        Me.pnlCurrentList.Controls.Add(Me.cboWorkViewerContext)
        Me.pnlCurrentList.Controls.Add(Me.lblResultsCount)
        Me.pnlCurrentList.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCurrentList.Location = New System.Drawing.Point(118, 167)
        Me.pnlCurrentList.Name = "pnlCurrentList"
        Me.pnlCurrentList.Size = New System.Drawing.Size(686, 37)
        Me.pnlCurrentList.TabIndex = 1
        '
        'pnlContextSubView
        '
        Me.pnlContextSubView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlContextSubView.Controls.Add(Me.rdbStaffView)
        Me.pnlContextSubView.Controls.Add(Me.rdbUCView)
        Me.pnlContextSubView.Controls.Add(Me.rdbPMView)
        Me.pnlContextSubView.Location = New System.Drawing.Point(342, 1)
        Me.pnlContextSubView.Name = "pnlContextSubView"
        Me.pnlContextSubView.Size = New System.Drawing.Size(252, 30)
        Me.pnlContextSubView.TabIndex = 295
        '
        'IAIPNavigation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 328)
        Me.Controls.Add(Me.dgvWorkViewer)
        Me.Controls.Add(Me.pnlCurrentList)
        Me.Controls.Add(Me.grpQuickAccess)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.flpNavButtons)
        Me.Controls.Add(Me.lblMessageLabel)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(750, 300)
        Me.Name = "IAIPNavigation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IAIP Navigation Screen"
        Me.grpQuickAccess.ResumeLayout(False)
        Me.grpQuickAccess.PerformLayout()
        Me.SbeapQuickAccessPanel.ResumeLayout(False)
        Me.SbeapQuickAccessPanel.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgvWorkViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrentList.ResumeLayout(False)
        Me.pnlCurrentList.PerformLayout()
        Me.pnlContextSubView.ResumeLayout(False)
        Me.pnlContextSubView.PerformLayout()
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
    Friend WithEvents txtOpenSscpItem As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtOpenApplication As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtOpenEnforcement As System.Windows.Forms.TextBox
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents lblOpenTestReport As System.Windows.Forms.Label
    Friend WithEvents txtOpenTestReport As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblOpenFacilitySummary As System.Windows.Forms.Label
    Friend WithEvents txtOpenFacilitySummary As System.Windows.Forms.TextBox
    Friend WithEvents dgvWorkViewer As System.Windows.Forms.DataGridView
    Friend WithEvents pnl4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgrLoadWorkViewer As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblMessageLabel As System.Windows.Forms.Label
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents txtOpenTestLog As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents bgrLoadButtons As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnChangeWorkViewerContext As System.Windows.Forms.Button
    Friend WithEvents lblWorkViewerContext As System.Windows.Forms.Label
    Friend WithEvents cboWorkViewerContext As System.Windows.Forms.ComboBox
    Friend WithEvents rdbPMView As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUCView As System.Windows.Forms.RadioButton
    Friend WithEvents rdbStaffView As System.Windows.Forms.RadioButton
    Friend WithEvents mmiExport As System.Windows.Forms.MenuItem
    Friend WithEvents mmiSeparator1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiTesting As System.Windows.Forms.MenuItem
    Friend WithEvents mmiResetForm As System.Windows.Forms.MenuItem
    Friend WithEvents pnlCurrentList As System.Windows.Forms.Panel
    Friend WithEvents pnlContextSubView As System.Windows.Forms.Panel
    Friend WithEvents mmiPing As System.Windows.Forms.MenuItem
    Friend WithEvents flpNavButtons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtOpenSbeapCaseLog As System.Windows.Forms.TextBox
    Friend WithEvents SbeapCaseLogNumberLabel As System.Windows.Forms.Label
    Friend WithEvents SbeapClientIDLabel As System.Windows.Forms.Label
    Friend WithEvents txtOpenSbeapClient As System.Windows.Forms.TextBox
    Friend WithEvents SbeapQuickAccessPanel As System.Windows.Forms.Panel
    Friend WithEvents btnOpenFacilitySummary As System.Windows.Forms.Button
    Friend WithEvents btnOpenTestReport As System.Windows.Forms.Button
    Friend WithEvents btnOpenApplication As System.Windows.Forms.Button
    Friend WithEvents btnOpenTestLog As System.Windows.Forms.Button
    Friend WithEvents btnOpenSscpItem As System.Windows.Forms.Button
    Friend WithEvents btnOpenEnforcement As System.Windows.Forms.Button
    Friend WithEvents btnOpenSbeapCaseLog As System.Windows.Forms.Button
    Friend WithEvents btnOpenSbeapClient As System.Windows.Forms.Button
End Class
