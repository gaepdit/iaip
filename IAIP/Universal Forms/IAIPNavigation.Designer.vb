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
        Me.SbeapQuickAccessPanel = New System.Windows.Forms.Panel
        Me.llbOpenSbeapCase = New System.Windows.Forms.LinkLabel
        Me.txtSbeapCaseNumber = New System.Windows.Forms.TextBox
        Me.SbeapCaseLogNumberLabel = New System.Windows.Forms.Label
        Me.llbOpenSbeapClient = New System.Windows.Forms.LinkLabel
        Me.SbeapClientIDLabel = New System.Windows.Forms.Label
        Me.txtSbeapClientId = New System.Windows.Forms.TextBox
        Me.llbOpenTestLog = New System.Windows.Forms.LinkLabel
        Me.txtTestLogNumber = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.llbOpenFacilitySummary = New System.Windows.Forms.LinkLabel
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.llbOpenSscpItem = New System.Windows.Forms.LinkLabel
        Me.txtSscpItemNumber = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.llbOpenPermitApplication = New System.Windows.Forms.LinkLabel
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.llbOpenEnforcement = New System.Windows.Forms.LinkLabel
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.llbOpenTestReport = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTestReportNumber = New System.Windows.Forms.TextBox
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
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(804, 33)
        Me.lblTitle.TabIndex = 5
        Me.lblTitle.Text = "Integrated Air Information Platform"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'flpNavButtons
        '
        Me.flpNavButtons.AutoScroll = True
        Me.flpNavButtons.Dock = System.Windows.Forms.DockStyle.Left
        Me.flpNavButtons.Location = New System.Drawing.Point(0, 33)
        Me.flpNavButtons.Name = "flpNavButtons"
        Me.flpNavButtons.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.flpNavButtons.Size = New System.Drawing.Size(118, 295)
        Me.flpNavButtons.TabIndex = 0
        '
        'grpQuickAccess
        '
        Me.grpQuickAccess.Controls.Add(Me.SbeapQuickAccessPanel)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenTestLog)
        Me.grpQuickAccess.Controls.Add(Me.txtTestLogNumber)
        Me.grpQuickAccess.Controls.Add(Me.Label8)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenFacilitySummary)
        Me.grpQuickAccess.Controls.Add(Me.Label7)
        Me.grpQuickAccess.Controls.Add(Me.txtAIRSNumber)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenSscpItem)
        Me.grpQuickAccess.Controls.Add(Me.txtSscpItemNumber)
        Me.grpQuickAccess.Controls.Add(Me.Label2)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenPermitApplication)
        Me.grpQuickAccess.Controls.Add(Me.Label6)
        Me.grpQuickAccess.Controls.Add(Me.txtApplicationNumber)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenEnforcement)
        Me.grpQuickAccess.Controls.Add(Me.Label5)
        Me.grpQuickAccess.Controls.Add(Me.txtEnforcementNumber)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenTestReport)
        Me.grpQuickAccess.Controls.Add(Me.Label3)
        Me.grpQuickAccess.Controls.Add(Me.txtTestReportNumber)
        Me.grpQuickAccess.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpQuickAccess.Location = New System.Drawing.Point(118, 204)
        Me.grpQuickAccess.Name = "grpQuickAccess"
        Me.grpQuickAccess.Size = New System.Drawing.Size(686, 100)
        Me.grpQuickAccess.TabIndex = 2
        Me.grpQuickAccess.TabStop = False
        Me.grpQuickAccess.Text = "Quick Access"
        '
        'SbeapQuickAccessPanel
        '
        Me.SbeapQuickAccessPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.llbOpenSbeapCase)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.txtSbeapCaseNumber)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.SbeapCaseLogNumberLabel)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.llbOpenSbeapClient)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.SbeapClientIDLabel)
        Me.SbeapQuickAccessPanel.Controls.Add(Me.txtSbeapClientId)
        Me.SbeapQuickAccessPanel.Enabled = False
        Me.SbeapQuickAccessPanel.Location = New System.Drawing.Point(483, 16)
        Me.SbeapQuickAccessPanel.Name = "SbeapQuickAccessPanel"
        Me.SbeapQuickAccessPanel.Size = New System.Drawing.Size(147, 81)
        Me.SbeapQuickAccessPanel.TabIndex = 12
        Me.SbeapQuickAccessPanel.Visible = False
        '
        'llbOpenSbeapCase
        '
        Me.llbOpenSbeapCase.AutoSize = True
        Me.llbOpenSbeapCase.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenSbeapCase.Location = New System.Drawing.Point(101, 60)
        Me.llbOpenSbeapCase.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenSbeapCase.Name = "llbOpenSbeapCase"
        Me.llbOpenSbeapCase.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenSbeapCase.TabIndex = 3
        Me.llbOpenSbeapCase.TabStop = True
        Me.llbOpenSbeapCase.Text = "Open"
        '
        'txtSbeapCaseNumber
        '
        Me.txtSbeapCaseNumber.Location = New System.Drawing.Point(7, 57)
        Me.txtSbeapCaseNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSbeapCaseNumber.MaxLength = 10
        Me.txtSbeapCaseNumber.Name = "txtSbeapCaseNumber"
        Me.txtSbeapCaseNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtSbeapCaseNumber.TabIndex = 2
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
        'llbOpenSbeapClient
        '
        Me.llbOpenSbeapClient.AutoSize = True
        Me.llbOpenSbeapClient.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenSbeapClient.Location = New System.Drawing.Point(101, 20)
        Me.llbOpenSbeapClient.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenSbeapClient.Name = "llbOpenSbeapClient"
        Me.llbOpenSbeapClient.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenSbeapClient.TabIndex = 1
        Me.llbOpenSbeapClient.TabStop = True
        Me.llbOpenSbeapClient.Text = "Open"
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
        'txtSbeapClientId
        '
        Me.txtSbeapClientId.Location = New System.Drawing.Point(7, 17)
        Me.txtSbeapClientId.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSbeapClientId.MaxLength = 10
        Me.txtSbeapClientId.Name = "txtSbeapClientId"
        Me.txtSbeapClientId.Size = New System.Drawing.Size(90, 20)
        Me.txtSbeapClientId.TabIndex = 0
        '
        'llbOpenTestLog
        '
        Me.llbOpenTestLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenTestLog.AutoSize = True
        Me.llbOpenTestLog.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenTestLog.Location = New System.Drawing.Point(424, 76)
        Me.llbOpenTestLog.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenTestLog.Name = "llbOpenTestLog"
        Me.llbOpenTestLog.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenTestLog.TabIndex = 11
        Me.llbOpenTestLog.TabStop = True
        Me.llbOpenTestLog.Text = "Open"
        '
        'txtTestLogNumber
        '
        Me.txtTestLogNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTestLogNumber.Location = New System.Drawing.Point(330, 73)
        Me.txtTestLogNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTestLogNumber.MaxLength = 10
        Me.txtTestLogNumber.Name = "txtTestLogNumber"
        Me.txtTestLogNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtTestLogNumber.TabIndex = 10
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
        'llbOpenFacilitySummary
        '
        Me.llbOpenFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenFacilitySummary.AutoSize = True
        Me.llbOpenFacilitySummary.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenFacilitySummary.Location = New System.Drawing.Point(104, 36)
        Me.llbOpenFacilitySummary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenFacilitySummary.Name = "llbOpenFacilitySummary"
        Me.llbOpenFacilitySummary.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenFacilitySummary.TabIndex = 1
        Me.llbOpenFacilitySummary.TabStop = True
        Me.llbOpenFacilitySummary.Text = "Open"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label7.Location = New System.Drawing.Point(7, 18)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 265
        Me.Label7.Text = "Facility AIRS #"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAIRSNumber.Location = New System.Drawing.Point(10, 33)
        Me.txtAIRSNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtAIRSNumber.MaxLength = 8
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtAIRSNumber.TabIndex = 0
        '
        'llbOpenSscpItem
        '
        Me.llbOpenSscpItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenSscpItem.AutoSize = True
        Me.llbOpenSscpItem.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenSscpItem.Location = New System.Drawing.Point(264, 76)
        Me.llbOpenSscpItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenSscpItem.Name = "llbOpenSscpItem"
        Me.llbOpenSscpItem.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenSscpItem.TabIndex = 7
        Me.llbOpenSscpItem.TabStop = True
        Me.llbOpenSscpItem.Text = "Open"
        '
        'txtSscpItemNumber
        '
        Me.txtSscpItemNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSscpItemNumber.Location = New System.Drawing.Point(170, 73)
        Me.txtSscpItemNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSscpItemNumber.MaxLength = 10
        Me.txtSscpItemNumber.Name = "txtSscpItemNumber"
        Me.txtSscpItemNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtSscpItemNumber.TabIndex = 6
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
        'llbOpenPermitApplication
        '
        Me.llbOpenPermitApplication.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenPermitApplication.AutoSize = True
        Me.llbOpenPermitApplication.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenPermitApplication.Location = New System.Drawing.Point(424, 36)
        Me.llbOpenPermitApplication.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenPermitApplication.Name = "llbOpenPermitApplication"
        Me.llbOpenPermitApplication.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenPermitApplication.TabIndex = 9
        Me.llbOpenPermitApplication.TabStop = True
        Me.llbOpenPermitApplication.Text = "Open"
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
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtApplicationNumber.Location = New System.Drawing.Point(330, 33)
        Me.txtApplicationNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtApplicationNumber.MaxLength = 10
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtApplicationNumber.TabIndex = 8
        '
        'llbOpenEnforcement
        '
        Me.llbOpenEnforcement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenEnforcement.AutoSize = True
        Me.llbOpenEnforcement.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenEnforcement.Location = New System.Drawing.Point(264, 36)
        Me.llbOpenEnforcement.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenEnforcement.Name = "llbOpenEnforcement"
        Me.llbOpenEnforcement.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenEnforcement.TabIndex = 5
        Me.llbOpenEnforcement.TabStop = True
        Me.llbOpenEnforcement.Text = "Open"
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
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(170, 33)
        Me.txtEnforcementNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtEnforcementNumber.MaxLength = 8
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtEnforcementNumber.TabIndex = 4
        '
        'llbOpenTestReport
        '
        Me.llbOpenTestReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenTestReport.AutoSize = True
        Me.llbOpenTestReport.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llbOpenTestReport.Location = New System.Drawing.Point(103, 76)
        Me.llbOpenTestReport.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenTestReport.Name = "llbOpenTestReport"
        Me.llbOpenTestReport.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenTestReport.TabIndex = 3
        Me.llbOpenTestReport.TabStop = True
        Me.llbOpenTestReport.Text = "Open"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 58)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 249
        Me.Label3.Text = "ISMP Test Report #"
        '
        'txtTestReportNumber
        '
        Me.txtTestReportNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTestReportNumber.Location = New System.Drawing.Point(10, 73)
        Me.txtTestReportNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTestReportNumber.MaxLength = 9
        Me.txtTestReportNumber.Name = "txtTestReportNumber"
        Me.txtTestReportNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtTestReportNumber.TabIndex = 2
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
        Me.pnl1.Size = New System.Drawing.Size(475, 19)
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
        Me.Controls.Add(Me.flpNavButtons)
        Me.Controls.Add(Me.lblMessageLabel)
        Me.Controls.Add(Me.lblTitle)
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
    Friend WithEvents llbOpenSscpItem As System.Windows.Forms.LinkLabel
    Friend WithEvents txtSscpItemNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents llbOpenPermitApplication As System.Windows.Forms.LinkLabel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents llbOpenEnforcement As System.Windows.Forms.LinkLabel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents llbOpenTestReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTestReportNumber As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents llbOpenFacilitySummary As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents dgvWorkViewer As System.Windows.Forms.DataGridView
    Friend WithEvents pnl4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgrLoadWorkViewer As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblMessageLabel As System.Windows.Forms.Label
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents llbOpenTestLog As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTestLogNumber As System.Windows.Forms.TextBox
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
    Friend WithEvents llbOpenSbeapCase As System.Windows.Forms.LinkLabel
    Friend WithEvents txtSbeapCaseNumber As System.Windows.Forms.TextBox
    Friend WithEvents SbeapCaseLogNumberLabel As System.Windows.Forms.Label
    Friend WithEvents llbOpenSbeapClient As System.Windows.Forms.LinkLabel
    Friend WithEvents SbeapClientIDLabel As System.Windows.Forms.Label
    Friend WithEvents txtSbeapClientId As System.Windows.Forms.TextBox
    Friend WithEvents SbeapQuickAccessPanel As System.Windows.Forms.Panel
End Class
