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
        Me.OpenSbeapCaseNumber = New System.Windows.Forms.LinkLabel
        Me.llbOpenTestLog = New System.Windows.Forms.LinkLabel
        Me.SbeapCaseNumber = New System.Windows.Forms.TextBox
        Me.txtTestLogNumber = New System.Windows.Forms.TextBox
        Me.SbeapCaseLogNumberLabel = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.llbFacilitySummary = New System.Windows.Forms.LinkLabel
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.llbTrackingNumber = New System.Windows.Forms.LinkLabel
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.OpenSbeapClientID = New System.Windows.Forms.LinkLabel
        Me.llbOpenApplication = New System.Windows.Forms.LinkLabel
        Me.SbeapClientIDLabel = New System.Windows.Forms.Label
        Me.SbeapClientID = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.llbEnforcementRecord = New System.Windows.Forms.LinkLabel
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.LLSelectReport = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl5 = New System.Windows.Forms.ToolStripStatusLabel
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
        Me.flpNavButtons.Size = New System.Drawing.Size(118, 358)
        Me.flpNavButtons.TabIndex = 0
        '
        'grpQuickAccess
        '
        Me.grpQuickAccess.Controls.Add(Me.OpenSbeapCaseNumber)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenTestLog)
        Me.grpQuickAccess.Controls.Add(Me.SbeapCaseNumber)
        Me.grpQuickAccess.Controls.Add(Me.txtTestLogNumber)
        Me.grpQuickAccess.Controls.Add(Me.SbeapCaseLogNumberLabel)
        Me.grpQuickAccess.Controls.Add(Me.Label8)
        Me.grpQuickAccess.Controls.Add(Me.llbFacilitySummary)
        Me.grpQuickAccess.Controls.Add(Me.Label7)
        Me.grpQuickAccess.Controls.Add(Me.txtAIRSNumber)
        Me.grpQuickAccess.Controls.Add(Me.llbTrackingNumber)
        Me.grpQuickAccess.Controls.Add(Me.txtTrackingNumber)
        Me.grpQuickAccess.Controls.Add(Me.Label2)
        Me.grpQuickAccess.Controls.Add(Me.OpenSbeapClientID)
        Me.grpQuickAccess.Controls.Add(Me.llbOpenApplication)
        Me.grpQuickAccess.Controls.Add(Me.SbeapClientIDLabel)
        Me.grpQuickAccess.Controls.Add(Me.SbeapClientID)
        Me.grpQuickAccess.Controls.Add(Me.Label6)
        Me.grpQuickAccess.Controls.Add(Me.txtApplicationNumber)
        Me.grpQuickAccess.Controls.Add(Me.llbEnforcementRecord)
        Me.grpQuickAccess.Controls.Add(Me.Label5)
        Me.grpQuickAccess.Controls.Add(Me.txtEnforcementNumber)
        Me.grpQuickAccess.Controls.Add(Me.LLSelectReport)
        Me.grpQuickAccess.Controls.Add(Me.Label3)
        Me.grpQuickAccess.Controls.Add(Me.txtReferenceNumber)
        Me.grpQuickAccess.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpQuickAccess.Location = New System.Drawing.Point(118, 267)
        Me.grpQuickAccess.Name = "grpQuickAccess"
        Me.grpQuickAccess.Size = New System.Drawing.Size(686, 100)
        Me.grpQuickAccess.TabIndex = 2
        Me.grpQuickAccess.TabStop = False
        Me.grpQuickAccess.Text = "Quick Access"
        '
        'OpenSbeapCaseNumber
        '
        Me.OpenSbeapCaseNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.OpenSbeapCaseNumber.AutoSize = True
        Me.OpenSbeapCaseNumber.Location = New System.Drawing.Point(594, 76)
        Me.OpenSbeapCaseNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.OpenSbeapCaseNumber.Name = "OpenSbeapCaseNumber"
        Me.OpenSbeapCaseNumber.Size = New System.Drawing.Size(33, 13)
        Me.OpenSbeapCaseNumber.TabIndex = 11
        Me.OpenSbeapCaseNumber.TabStop = True
        Me.OpenSbeapCaseNumber.Text = "Open"
        '
        'llbOpenTestLog
        '
        Me.llbOpenTestLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenTestLog.AutoSize = True
        Me.llbOpenTestLog.Location = New System.Drawing.Point(426, 76)
        Me.llbOpenTestLog.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenTestLog.Name = "llbOpenTestLog"
        Me.llbOpenTestLog.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenTestLog.TabIndex = 11
        Me.llbOpenTestLog.TabStop = True
        Me.llbOpenTestLog.Text = "Open"
        '
        'SbeapCaseNumber
        '
        Me.SbeapCaseNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapCaseNumber.Location = New System.Drawing.Point(500, 73)
        Me.SbeapCaseNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.SbeapCaseNumber.MaxLength = 10
        Me.SbeapCaseNumber.Name = "SbeapCaseNumber"
        Me.SbeapCaseNumber.Size = New System.Drawing.Size(90, 20)
        Me.SbeapCaseNumber.TabIndex = 10
        '
        'txtTestLogNumber
        '
        Me.txtTestLogNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTestLogNumber.Location = New System.Drawing.Point(332, 73)
        Me.txtTestLogNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTestLogNumber.MaxLength = 10
        Me.txtTestLogNumber.Name = "txtTestLogNumber"
        Me.txtTestLogNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtTestLogNumber.TabIndex = 10
        '
        'SbeapCaseLogNumberLabel
        '
        Me.SbeapCaseLogNumberLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapCaseLogNumberLabel.AutoSize = True
        Me.SbeapCaseLogNumberLabel.Location = New System.Drawing.Point(497, 58)
        Me.SbeapCaseLogNumberLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SbeapCaseLogNumberLabel.Name = "SbeapCaseLogNumberLabel"
        Me.SbeapCaseLogNumberLabel.Size = New System.Drawing.Size(100, 13)
        Me.SbeapCaseLogNumberLabel.TabIndex = 268
        Me.SbeapCaseLogNumberLabel.Text = "SBEAP Case Log #"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(329, 58)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 268
        Me.Label8.Text = "ISMP Test Log #"
        '
        'llbFacilitySummary
        '
        Me.llbFacilitySummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbFacilitySummary.AutoSize = True
        Me.llbFacilitySummary.Location = New System.Drawing.Point(104, 36)
        Me.llbFacilitySummary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbFacilitySummary.Name = "llbFacilitySummary"
        Me.llbFacilitySummary.Size = New System.Drawing.Size(33, 13)
        Me.llbFacilitySummary.TabIndex = 1
        Me.llbFacilitySummary.TabStop = True
        Me.llbFacilitySummary.Text = "Open"
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
        'llbTrackingNumber
        '
        Me.llbTrackingNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbTrackingNumber.AutoSize = True
        Me.llbTrackingNumber.Location = New System.Drawing.Point(263, 76)
        Me.llbTrackingNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbTrackingNumber.Name = "llbTrackingNumber"
        Me.llbTrackingNumber.Size = New System.Drawing.Size(33, 13)
        Me.llbTrackingNumber.TabIndex = 9
        Me.llbTrackingNumber.TabStop = True
        Me.llbTrackingNumber.Text = "Open"
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTrackingNumber.Location = New System.Drawing.Point(169, 73)
        Me.txtTrackingNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTrackingNumber.MaxLength = 10
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtTrackingNumber.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(166, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 261
        Me.Label2.Text = "SSCP Item #"
        '
        'OpenSbeapClientID
        '
        Me.OpenSbeapClientID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.OpenSbeapClientID.AutoSize = True
        Me.OpenSbeapClientID.Location = New System.Drawing.Point(594, 36)
        Me.OpenSbeapClientID.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.OpenSbeapClientID.Name = "OpenSbeapClientID"
        Me.OpenSbeapClientID.Size = New System.Drawing.Size(33, 13)
        Me.OpenSbeapClientID.TabIndex = 5
        Me.OpenSbeapClientID.TabStop = True
        Me.OpenSbeapClientID.Text = "Open"
        '
        'llbOpenApplication
        '
        Me.llbOpenApplication.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbOpenApplication.AutoSize = True
        Me.llbOpenApplication.Location = New System.Drawing.Point(426, 36)
        Me.llbOpenApplication.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenApplication.Name = "llbOpenApplication"
        Me.llbOpenApplication.Size = New System.Drawing.Size(33, 13)
        Me.llbOpenApplication.TabIndex = 5
        Me.llbOpenApplication.TabStop = True
        Me.llbOpenApplication.Text = "Open"
        '
        'SbeapClientIDLabel
        '
        Me.SbeapClientIDLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapClientIDLabel.AutoSize = True
        Me.SbeapClientIDLabel.Location = New System.Drawing.Point(497, 18)
        Me.SbeapClientIDLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SbeapClientIDLabel.Name = "SbeapClientIDLabel"
        Me.SbeapClientIDLabel.Size = New System.Drawing.Size(99, 13)
        Me.SbeapClientIDLabel.TabIndex = 259
        Me.SbeapClientIDLabel.Text = "SBEAP Customer #"
        '
        'SbeapClientID
        '
        Me.SbeapClientID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SbeapClientID.Location = New System.Drawing.Point(500, 33)
        Me.SbeapClientID.Margin = New System.Windows.Forms.Padding(2)
        Me.SbeapClientID.MaxLength = 10
        Me.SbeapClientID.Name = "SbeapClientID"
        Me.SbeapClientID.Size = New System.Drawing.Size(90, 20)
        Me.SbeapClientID.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(329, 18)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 259
        Me.Label6.Text = "SSPP Application #"
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtApplicationNumber.Location = New System.Drawing.Point(332, 33)
        Me.txtApplicationNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtApplicationNumber.MaxLength = 10
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtApplicationNumber.TabIndex = 4
        '
        'llbEnforcementRecord
        '
        Me.llbEnforcementRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbEnforcementRecord.AutoSize = True
        Me.llbEnforcementRecord.Location = New System.Drawing.Point(263, 36)
        Me.llbEnforcementRecord.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbEnforcementRecord.Name = "llbEnforcementRecord"
        Me.llbEnforcementRecord.Size = New System.Drawing.Size(33, 13)
        Me.llbEnforcementRecord.TabIndex = 3
        Me.llbEnforcementRecord.TabStop = True
        Me.llbEnforcementRecord.Text = "Open"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(166, 18)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 255
        Me.Label5.Text = "Enforcement #"
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(169, 33)
        Me.txtEnforcementNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtEnforcementNumber.MaxLength = 8
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtEnforcementNumber.TabIndex = 2
        '
        'LLSelectReport
        '
        Me.LLSelectReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LLSelectReport.AutoSize = True
        Me.LLSelectReport.Location = New System.Drawing.Point(103, 76)
        Me.LLSelectReport.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LLSelectReport.Name = "LLSelectReport"
        Me.LLSelectReport.Size = New System.Drawing.Size(33, 13)
        Me.LLSelectReport.TabIndex = 7
        Me.LLSelectReport.TabStop = True
        Me.LLSelectReport.Text = "Open"
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
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtReferenceNumber.Location = New System.Drawing.Point(10, 73)
        Me.txtReferenceNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtReferenceNumber.MaxLength = 9
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtReferenceNumber.TabIndex = 6
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3, Me.pnl4, Me.pnl5})
        Me.StatusStrip1.Location = New System.Drawing.Point(118, 367)
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
        'pnl5
        '
        Me.pnl5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.pnl5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl5.Name = "pnl5"
        Me.pnl5.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pnl5.Size = New System.Drawing.Size(47, 19)
        Me.pnl5.Text = "Server"
        Me.pnl5.Visible = False
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
        Me.dgvWorkViewer.Size = New System.Drawing.Size(686, 197)
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
        Me.pnlCurrentList.Location = New System.Drawing.Point(118, 230)
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
        Me.ClientSize = New System.Drawing.Size(804, 391)
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
    Friend WithEvents llbTrackingNumber As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents llbOpenApplication As System.Windows.Forms.LinkLabel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents llbEnforcementRecord As System.Windows.Forms.LinkLabel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents LLSelectReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents llbFacilitySummary As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents dgvWorkViewer As System.Windows.Forms.DataGridView
    Friend WithEvents pnl4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl5 As System.Windows.Forms.ToolStripStatusLabel
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
    Friend WithEvents OpenSbeapCaseNumber As System.Windows.Forms.LinkLabel
    Friend WithEvents SbeapCaseNumber As System.Windows.Forms.TextBox
    Friend WithEvents SbeapCaseLogNumberLabel As System.Windows.Forms.Label
    Friend WithEvents OpenSbeapClientID As System.Windows.Forms.LinkLabel
    Friend WithEvents SbeapClientIDLabel As System.Windows.Forms.Label
    Friend WithEvents SbeapClientID As System.Windows.Forms.TextBox
End Class
