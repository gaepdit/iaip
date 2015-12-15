<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SscpEnforcement
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SscpEnforcement))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.SaveButton = New System.Windows.Forms.ToolStripButton()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mmiClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiShowAuditHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiShowEpaActionNumbersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteEnforcementMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnforcementTabs = New System.Windows.Forms.TabControl()
        Me.InfoTabPage = New System.Windows.Forms.TabPage()
        Me.LinkedEventDisplay = New System.Windows.Forms.LinkLabel()
        Me.EAGroupBox = New System.Windows.Forms.GroupBox()
        Me.COCheckBox = New System.Windows.Forms.CheckBox()
        Me.NovCheckBox = New System.Windows.Forms.CheckBox()
        Me.LonCheckBox = New System.Windows.Forms.CheckBox()
        Me.AOCheckBox = New System.Windows.Forms.CheckBox()
        Me.ViolationTypeGroupbox = New System.Windows.Forms.GroupBox()
        Me.ViolationTypeSelect = New System.Windows.Forms.ComboBox()
        Me.DayZeroDisplay = New System.Windows.Forms.Label()
        Me.ViolationTypeNone = New System.Windows.Forms.RadioButton()
        Me.ViolationTypeNonFrv = New System.Windows.Forms.RadioButton()
        Me.ViolationTypeHpv = New System.Windows.Forms.RadioButton()
        Me.ViolationTypeFrv = New System.Windows.Forms.RadioButton()
        Me.DiscoveryDateLabel = New System.Windows.Forms.Label()
        Me.LinkToEvent = New System.Windows.Forms.Button()
        Me.LastEditedDateDisplay = New System.Windows.Forms.Label()
        Me.ClearLinkedEvent = New System.Windows.Forms.Button()
        Me.DiscoveryDate = New System.Windows.Forms.DateTimePicker()
        Me.GeneralComments = New System.Windows.Forms.TextBox()
        Me.SubmitToEpa = New System.Windows.Forms.Button()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.SubmitToUC = New System.Windows.Forms.Button()
        Me.PollutantsTabPage = New System.Windows.Forms.TabPage()
        Me.PollutantsListView = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.EditAirProgramPollutantsButton = New System.Windows.Forms.Button()
        Me.lblPollutants = New System.Windows.Forms.Label()
        Me.LonTabPage = New System.Windows.Forms.TabPage()
        Me.LonComments = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.LonToUC = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LonResolved = New System.Windows.Forms.DateTimePicker()
        Me.LonSent = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.NovTabPage = New System.Windows.Forms.TabPage()
        Me.NovComments = New System.Windows.Forms.TextBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NovToUC = New System.Windows.Forms.DateTimePicker()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.NovSent = New System.Windows.Forms.DateTimePicker()
        Me.NfaToPM = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.NovResponseReceived = New System.Windows.Forms.DateTimePicker()
        Me.NfaToUC = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.NfaSent = New System.Windows.Forms.DateTimePicker()
        Me.NovToPM = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.COTabPage = New System.Windows.Forms.TabPage()
        Me.COComments = New System.Windows.Forms.TextBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.CoNumber = New System.Windows.Forms.NumericUpDown()
        Me.COToUC = New System.Windows.Forms.DateTimePicker()
        Me.COPenaltyComments = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.COPenaltyAmount = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.COProposed = New System.Windows.Forms.DateTimePicker()
        Me.COToPM = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.COReceivedFromDirector = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.COExecuted = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.COReceivedfromCompany = New System.Windows.Forms.DateTimePicker()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.COResolved = New System.Windows.Forms.DateTimePicker()
        Me.StipulatedPenaltiesGroupBox = New System.Windows.Forms.GroupBox()
        Me.StipulatedPenaltyAmount = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.StipulatedPenalties = New System.Windows.Forms.DataGridView()
        Me.StipulatedPenaltyComments = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.StipulatedPenaltyControls = New System.Windows.Forms.Panel()
        Me.UpdateStipulatedPenaltyButton = New System.Windows.Forms.Button()
        Me.DeleteStipulatedPenaltyButton = New System.Windows.Forms.Button()
        Me.SaveNewStipulatedPenaltyButton = New System.Windows.Forms.Button()
        Me.ClearStipulatedPenaltyFormButton = New System.Windows.Forms.Button()
        Me.AOTabPage = New System.Windows.Forms.TabPage()
        Me.AOComments = New System.Windows.Forms.TextBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.AOExecuted = New System.Windows.Forms.DateTimePicker()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.AOResolved = New System.Windows.Forms.DateTimePicker()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.AOAppealed = New System.Windows.Forms.DateTimePicker()
        Me.DocumentsTabPage = New System.Windows.Forms.TabPage()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.pnlDocument = New System.Windows.Forms.Panel()
        Me.lblDocumentDescription = New System.Windows.Forms.Label()
        Me.btnDocumentDownload = New System.Windows.Forms.Button()
        Me.txtDocumentDescription = New System.Windows.Forms.TextBox()
        Me.DocumentUpdateButton = New System.Windows.Forms.Button()
        Me.lblDocumentName = New System.Windows.Forms.Label()
        Me.lblCurrentFiles = New System.Windows.Forms.Label()
        Me.DocumentList = New System.Windows.Forms.DataGridView()
        Me.AuditHistoryTabPage = New System.Windows.Forms.TabPage()
        Me.AuditHistory = New System.Windows.Forms.DataGridView()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.RefreshAuditHistory = New System.Windows.Forms.Button()
        Me.ExportAuditHistory = New System.Windows.Forms.Button()
        Me.EpaValuesTabPage = New System.Windows.Forms.TabPage()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.AfsAoResolvedActionNumber = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.AfsNfaActionNumber = New System.Windows.Forms.TextBox()
        Me.AfsAoCivilCourtActionNumber = New System.Windows.Forms.TextBox()
        Me.AfsKeyActionNumber = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.AfsNovActionNumber = New System.Windows.Forms.TextBox()
        Me.AfsAoToAgActionNumber = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.AfsCoProposedActionNumber = New System.Windows.Forms.TextBox()
        Me.AfsStipulatedPenalitiesActionNumbers = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.AfsCoExecutedActionNumber = New System.Windows.Forms.TextBox()
        Me.AfsCoResolvedActionNumber = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.StaffResponsible = New System.Windows.Forms.ComboBox()
        Me.ResolvedDate = New System.Windows.Forms.DateTimePicker()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.ResolvedCheckBox = New System.Windows.Forms.CheckBox()
        Me.FacilityNotApprovedDisplay = New System.Windows.Forms.Label()
        Me.FacilityNameDisplay = New System.Windows.Forms.Label()
        Me.ComplianceStatusDisplay = New System.Windows.Forms.Label()
        Me.AirsNumberDisplay = New System.Windows.Forms.Label()
        Me.EnforcementIdDisplay = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ProgramsListView = New System.Windows.Forms.ListView()
        Me.PollutantsProgramSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.EnforcementTabs.SuspendLayout()
        Me.InfoTabPage.SuspendLayout()
        Me.EAGroupBox.SuspendLayout()
        Me.ViolationTypeGroupbox.SuspendLayout()
        Me.PollutantsTabPage.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.LonTabPage.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.NovTabPage.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.COTabPage.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.CoNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StipulatedPenaltiesGroupBox.SuspendLayout()
        CType(Me.StipulatedPenalties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StipulatedPenaltyControls.SuspendLayout()
        Me.AOTabPage.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.DocumentsTabPage.SuspendLayout()
        Me.pnlDocument.SuspendLayout()
        CType(Me.DocumentList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AuditHistoryTabPage.SuspendLayout()
        CType(Me.AuditHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.EpaValuesTabPage.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PollutantsProgramSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PollutantsProgramSplitContainer.Panel1.SuspendLayout()
        Me.PollutantsProgramSplitContainer.Panel2.SuspendLayout()
        Me.PollutantsProgramSplitContainer.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(629, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'SaveButton
        '
        Me.SaveButton.Image = Global.Iaip.My.Resources.Resources.SaveButtonImage
        Me.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(51, 22)
        Me.SaveButton.Text = "Save"
        Me.SaveButton.ToolTipText = "Save (Ctrl + S)"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiFile, Me.mmiTools})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(629, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mmiFile
        '
        Me.mmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveMenuItem, Me.ToolStripSeparator2, Me.mmiClose})
        Me.mmiFile.Name = "mmiFile"
        Me.mmiFile.Size = New System.Drawing.Size(37, 20)
        Me.mmiFile.Text = "&File"
        '
        'SaveMenuItem
        '
        Me.SaveMenuItem.Name = "SaveMenuItem"
        Me.SaveMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.SaveMenuItem.Text = "&Save"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(145, 6)
        '
        'mmiClose
        '
        Me.mmiClose.Name = "mmiClose"
        Me.mmiClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.mmiClose.Size = New System.Drawing.Size(148, 22)
        Me.mmiClose.Text = "&Close"
        '
        'mmiTools
        '
        Me.mmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiShowAuditHistory, Me.mmiShowEpaActionNumbersToolStripMenuItem, Me.ToolStripSeparator1, Me.DeleteEnforcementMenuItem})
        Me.mmiTools.Name = "mmiTools"
        Me.mmiTools.Size = New System.Drawing.Size(48, 20)
        Me.mmiTools.Text = "&Tools"
        '
        'mmiShowAuditHistory
        '
        Me.mmiShowAuditHistory.Name = "mmiShowAuditHistory"
        Me.mmiShowAuditHistory.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.mmiShowAuditHistory.Size = New System.Drawing.Size(219, 22)
        Me.mmiShowAuditHistory.Text = "Show Audit &History"
        '
        'mmiShowEpaActionNumbersToolStripMenuItem
        '
        Me.mmiShowEpaActionNumbersToolStripMenuItem.Name = "mmiShowEpaActionNumbersToolStripMenuItem"
        Me.mmiShowEpaActionNumbersToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.mmiShowEpaActionNumbersToolStripMenuItem.Text = "Show &EPA Action Numbers"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(216, 6)
        '
        'DeleteEnforcementMenuItem
        '
        Me.DeleteEnforcementMenuItem.Enabled = False
        Me.DeleteEnforcementMenuItem.Name = "DeleteEnforcementMenuItem"
        Me.DeleteEnforcementMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.DeleteEnforcementMenuItem.Text = "Delete this enforcement"
        '
        'EnforcementTabs
        '
        Me.EnforcementTabs.Controls.Add(Me.InfoTabPage)
        Me.EnforcementTabs.Controls.Add(Me.PollutantsTabPage)
        Me.EnforcementTabs.Controls.Add(Me.LonTabPage)
        Me.EnforcementTabs.Controls.Add(Me.NovTabPage)
        Me.EnforcementTabs.Controls.Add(Me.COTabPage)
        Me.EnforcementTabs.Controls.Add(Me.AOTabPage)
        Me.EnforcementTabs.Controls.Add(Me.DocumentsTabPage)
        Me.EnforcementTabs.Controls.Add(Me.AuditHistoryTabPage)
        Me.EnforcementTabs.Controls.Add(Me.EpaValuesTabPage)
        Me.EnforcementTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EnforcementTabs.Location = New System.Drawing.Point(0, 145)
        Me.EnforcementTabs.Name = "EnforcementTabs"
        Me.EnforcementTabs.SelectedIndex = 0
        Me.EnforcementTabs.Size = New System.Drawing.Size(629, 456)
        Me.EnforcementTabs.TabIndex = 267
        '
        'InfoTabPage
        '
        Me.InfoTabPage.AutoScroll = True
        Me.InfoTabPage.Controls.Add(Me.LinkedEventDisplay)
        Me.InfoTabPage.Controls.Add(Me.EAGroupBox)
        Me.InfoTabPage.Controls.Add(Me.ViolationTypeGroupbox)
        Me.InfoTabPage.Controls.Add(Me.DiscoveryDateLabel)
        Me.InfoTabPage.Controls.Add(Me.LinkToEvent)
        Me.InfoTabPage.Controls.Add(Me.LastEditedDateDisplay)
        Me.InfoTabPage.Controls.Add(Me.ClearLinkedEvent)
        Me.InfoTabPage.Controls.Add(Me.DiscoveryDate)
        Me.InfoTabPage.Controls.Add(Me.GeneralComments)
        Me.InfoTabPage.Controls.Add(Me.SubmitToEpa)
        Me.InfoTabPage.Controls.Add(Me.Label34)
        Me.InfoTabPage.Controls.Add(Me.SubmitToUC)
        Me.InfoTabPage.Location = New System.Drawing.Point(4, 22)
        Me.InfoTabPage.Name = "InfoTabPage"
        Me.InfoTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.InfoTabPage.Size = New System.Drawing.Size(621, 430)
        Me.InfoTabPage.TabIndex = 0
        Me.InfoTabPage.Text = "General Information"
        Me.InfoTabPage.UseVisualStyleBackColor = True
        '
        'LinkedEventDisplay
        '
        Me.LinkedEventDisplay.AutoSize = True
        Me.LinkedEventDisplay.LinkArea = New System.Windows.Forms.LinkArea(17, 17)
        Me.LinkedEventDisplay.Location = New System.Drawing.Point(322, 25)
        Me.LinkedEventDisplay.Name = "LinkedEventDisplay"
        Me.LinkedEventDisplay.Size = New System.Drawing.Size(92, 17)
        Me.LinkedEventDisplay.TabIndex = 376
        Me.LinkedEventDisplay.Text = "Discovery Event: "
        Me.LinkedEventDisplay.UseCompatibleTextRendering = True
        Me.LinkedEventDisplay.Visible = False
        '
        'EAGroupBox
        '
        Me.EAGroupBox.Controls.Add(Me.COCheckBox)
        Me.EAGroupBox.Controls.Add(Me.NovCheckBox)
        Me.EAGroupBox.Controls.Add(Me.LonCheckBox)
        Me.EAGroupBox.Controls.Add(Me.AOCheckBox)
        Me.EAGroupBox.Location = New System.Drawing.Point(11, 71)
        Me.EAGroupBox.Name = "EAGroupBox"
        Me.EAGroupBox.Size = New System.Drawing.Size(212, 77)
        Me.EAGroupBox.TabIndex = 375
        Me.EAGroupBox.TabStop = False
        Me.EAGroupBox.Text = "Enforcement Actions"
        '
        'COCheckBox
        '
        Me.COCheckBox.AutoSize = True
        Me.COCheckBox.Location = New System.Drawing.Point(117, 29)
        Me.COCheckBox.Name = "COCheckBox"
        Me.COCheckBox.Size = New System.Drawing.Size(41, 17)
        Me.COCheckBox.TabIndex = 351
        Me.COCheckBox.Text = "CO"
        Me.COCheckBox.UseVisualStyleBackColor = True
        '
        'NovCheckBox
        '
        Me.NovCheckBox.AutoSize = True
        Me.NovCheckBox.Location = New System.Drawing.Point(62, 29)
        Me.NovCheckBox.Name = "NovCheckBox"
        Me.NovCheckBox.Size = New System.Drawing.Size(49, 17)
        Me.NovCheckBox.TabIndex = 350
        Me.NovCheckBox.Text = "NOV"
        Me.NovCheckBox.UseVisualStyleBackColor = True
        '
        'LonCheckBox
        '
        Me.LonCheckBox.AutoSize = True
        Me.LonCheckBox.Location = New System.Drawing.Point(8, 29)
        Me.LonCheckBox.Name = "LonCheckBox"
        Me.LonCheckBox.Size = New System.Drawing.Size(48, 17)
        Me.LonCheckBox.TabIndex = 349
        Me.LonCheckBox.Text = "LON"
        Me.LonCheckBox.UseVisualStyleBackColor = True
        '
        'AOCheckBox
        '
        Me.AOCheckBox.AutoSize = True
        Me.AOCheckBox.Location = New System.Drawing.Point(164, 29)
        Me.AOCheckBox.Name = "AOCheckBox"
        Me.AOCheckBox.Size = New System.Drawing.Size(41, 17)
        Me.AOCheckBox.TabIndex = 352
        Me.AOCheckBox.Text = "AO"
        Me.AOCheckBox.UseVisualStyleBackColor = True
        '
        'ViolationTypeGroupbox
        '
        Me.ViolationTypeGroupbox.Controls.Add(Me.ViolationTypeSelect)
        Me.ViolationTypeGroupbox.Controls.Add(Me.DayZeroDisplay)
        Me.ViolationTypeGroupbox.Controls.Add(Me.ViolationTypeNone)
        Me.ViolationTypeGroupbox.Controls.Add(Me.ViolationTypeNonFrv)
        Me.ViolationTypeGroupbox.Controls.Add(Me.ViolationTypeHpv)
        Me.ViolationTypeGroupbox.Controls.Add(Me.ViolationTypeFrv)
        Me.ViolationTypeGroupbox.Location = New System.Drawing.Point(229, 71)
        Me.ViolationTypeGroupbox.Name = "ViolationTypeGroupbox"
        Me.ViolationTypeGroupbox.Size = New System.Drawing.Size(506, 77)
        Me.ViolationTypeGroupbox.TabIndex = 371
        Me.ViolationTypeGroupbox.TabStop = False
        Me.ViolationTypeGroupbox.Text = "Violation Type"
        Me.ViolationTypeGroupbox.Visible = False
        '
        'ViolationTypeSelect
        '
        Me.ViolationTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ViolationTypeSelect.FormattingEnabled = True
        Me.ViolationTypeSelect.Location = New System.Drawing.Point(7, 43)
        Me.ViolationTypeSelect.Name = "ViolationTypeSelect"
        Me.ViolationTypeSelect.Size = New System.Drawing.Size(493, 21)
        Me.ViolationTypeSelect.TabIndex = 1
        '
        'DayZeroDisplay
        '
        Me.DayZeroDisplay.AutoSize = True
        Me.DayZeroDisplay.Location = New System.Drawing.Point(424, 20)
        Me.DayZeroDisplay.Name = "DayZeroDisplay"
        Me.DayZeroDisplay.Size = New System.Drawing.Size(76, 13)
        Me.DayZeroDisplay.TabIndex = 5
        Me.DayZeroDisplay.Text = "HPV Day Zero"
        Me.DayZeroDisplay.Visible = False
        '
        'ViolationTypeNone
        '
        Me.ViolationTypeNone.AutoSize = True
        Me.ViolationTypeNone.Checked = True
        Me.ViolationTypeNone.Location = New System.Drawing.Point(186, 19)
        Me.ViolationTypeNone.Name = "ViolationTypeNone"
        Me.ViolationTypeNone.Size = New System.Drawing.Size(51, 17)
        Me.ViolationTypeNone.TabIndex = 0
        Me.ViolationTypeNone.TabStop = True
        Me.ViolationTypeNone.Text = "None"
        Me.ViolationTypeNone.UseVisualStyleBackColor = True
        Me.ViolationTypeNone.Visible = False
        '
        'ViolationTypeNonFrv
        '
        Me.ViolationTypeNonFrv.AutoSize = True
        Me.ViolationTypeNonFrv.Location = New System.Drawing.Point(111, 19)
        Me.ViolationTypeNonFrv.Name = "ViolationTypeNonFrv"
        Me.ViolationTypeNonFrv.Size = New System.Drawing.Size(69, 17)
        Me.ViolationTypeNonFrv.TabIndex = 0
        Me.ViolationTypeNonFrv.TabStop = True
        Me.ViolationTypeNonFrv.Text = "Non-FRV"
        Me.ViolationTypeNonFrv.UseVisualStyleBackColor = True
        '
        'ViolationTypeHpv
        '
        Me.ViolationTypeHpv.AutoSize = True
        Me.ViolationTypeHpv.Location = New System.Drawing.Point(58, 19)
        Me.ViolationTypeHpv.Name = "ViolationTypeHpv"
        Me.ViolationTypeHpv.Size = New System.Drawing.Size(47, 17)
        Me.ViolationTypeHpv.TabIndex = 0
        Me.ViolationTypeHpv.TabStop = True
        Me.ViolationTypeHpv.Text = "HPV"
        Me.ViolationTypeHpv.UseVisualStyleBackColor = True
        '
        'ViolationTypeFrv
        '
        Me.ViolationTypeFrv.AutoSize = True
        Me.ViolationTypeFrv.Location = New System.Drawing.Point(6, 19)
        Me.ViolationTypeFrv.Name = "ViolationTypeFrv"
        Me.ViolationTypeFrv.Size = New System.Drawing.Size(46, 17)
        Me.ViolationTypeFrv.TabIndex = 0
        Me.ViolationTypeFrv.TabStop = True
        Me.ViolationTypeFrv.Text = "FRV"
        Me.ViolationTypeFrv.UseVisualStyleBackColor = True
        '
        'DiscoveryDateLabel
        '
        Me.DiscoveryDateLabel.AutoSize = True
        Me.DiscoveryDateLabel.Location = New System.Drawing.Point(8, 25)
        Me.DiscoveryDateLabel.Name = "DiscoveryDateLabel"
        Me.DiscoveryDateLabel.Size = New System.Drawing.Size(80, 13)
        Me.DiscoveryDateLabel.TabIndex = 3
        Me.DiscoveryDateLabel.Text = "Discovery Date"
        '
        'LinkToEvent
        '
        Me.LinkToEvent.AutoSize = True
        Me.LinkToEvent.Location = New System.Drawing.Point(229, 20)
        Me.LinkToEvent.Name = "LinkToEvent"
        Me.LinkToEvent.Size = New System.Drawing.Size(87, 23)
        Me.LinkToEvent.TabIndex = 337
        Me.LinkToEvent.Text = "Link to Event"
        '
        'LastEditedDateDisplay
        '
        Me.LastEditedDateDisplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LastEditedDateDisplay.Location = New System.Drawing.Point(462, 25)
        Me.LastEditedDateDisplay.Name = "LastEditedDateDisplay"
        Me.LastEditedDateDisplay.Size = New System.Drawing.Size(124, 15)
        Me.LastEditedDateDisplay.TabIndex = 7
        Me.LastEditedDateDisplay.Text = "Last edited date"
        Me.LastEditedDateDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LastEditedDateDisplay.Visible = False
        '
        'ClearLinkedEvent
        '
        Me.ClearLinkedEvent.AutoSize = True
        Me.ClearLinkedEvent.Location = New System.Drawing.Point(229, 20)
        Me.ClearLinkedEvent.Name = "ClearLinkedEvent"
        Me.ClearLinkedEvent.Size = New System.Drawing.Size(87, 23)
        Me.ClearLinkedEvent.TabIndex = 348
        Me.ClearLinkedEvent.Text = "Clear"
        Me.ClearLinkedEvent.Visible = False
        '
        'DiscoveryDate
        '
        Me.DiscoveryDate.Checked = False
        Me.DiscoveryDate.CustomFormat = "dd-MMM-yyyy"
        Me.DiscoveryDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DiscoveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DiscoveryDate.Location = New System.Drawing.Point(94, 22)
        Me.DiscoveryDate.Name = "DiscoveryDate"
        Me.DiscoveryDate.ShowCheckBox = True
        Me.DiscoveryDate.Size = New System.Drawing.Size(119, 22)
        Me.DiscoveryDate.TabIndex = 2
        '
        'GeneralComments
        '
        Me.GeneralComments.AcceptsReturn = True
        Me.GeneralComments.AcceptsTab = True
        Me.GeneralComments.Location = New System.Drawing.Point(11, 188)
        Me.GeneralComments.MaxLength = 4000
        Me.GeneralComments.Multiline = True
        Me.GeneralComments.Name = "GeneralComments"
        Me.GeneralComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GeneralComments.Size = New System.Drawing.Size(724, 80)
        Me.GeneralComments.TabIndex = 340
        '
        'SubmitToEpa
        '
        Me.SubmitToEpa.Enabled = False
        Me.SubmitToEpa.Location = New System.Drawing.Point(164, 296)
        Me.SubmitToEpa.Name = "SubmitToEpa"
        Me.SubmitToEpa.Size = New System.Drawing.Size(183, 23)
        Me.SubmitToEpa.TabIndex = 345
        Me.SubmitToEpa.Text = "Submit Enforcement to EPA"
        Me.SubmitToEpa.Visible = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(8, 172)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(96, 13)
        Me.Label34.TabIndex = 341
        Me.Label34.Text = "General Comments"
        '
        'SubmitToUC
        '
        Me.SubmitToUC.Location = New System.Drawing.Point(11, 296)
        Me.SubmitToUC.Name = "SubmitToUC"
        Me.SubmitToUC.Size = New System.Drawing.Size(147, 23)
        Me.SubmitToUC.TabIndex = 346
        Me.SubmitToUC.Text = "Submit Enforcement to UC"
        Me.SubmitToUC.Visible = False
        '
        'PollutantsTabPage
        '
        Me.PollutantsTabPage.Controls.Add(Me.PollutantsProgramSplitContainer)
        Me.PollutantsTabPage.Controls.Add(Me.Panel5)
        Me.PollutantsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PollutantsTabPage.Name = "PollutantsTabPage"
        Me.PollutantsTabPage.Size = New System.Drawing.Size(621, 430)
        Me.PollutantsTabPage.TabIndex = 5
        Me.PollutantsTabPage.Text = "Pollutants & Programs"
        Me.PollutantsTabPage.UseVisualStyleBackColor = True
        '
        'PollutantsListView
        '
        Me.PollutantsListView.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.PollutantsListView.AutoArrange = False
        Me.PollutantsListView.CheckBoxes = True
        Me.PollutantsListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PollutantsListView.HotTracking = True
        Me.PollutantsListView.HoverSelection = True
        Me.PollutantsListView.Location = New System.Drawing.Point(0, 37)
        Me.PollutantsListView.Name = "PollutantsListView"
        Me.PollutantsListView.Size = New System.Drawing.Size(308, 356)
        Me.PollutantsListView.TabIndex = 371
        Me.PollutantsListView.UseCompatibleStateImageBehavior = False
        Me.PollutantsListView.View = System.Windows.Forms.View.List
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.EditAirProgramPollutantsButton)
        Me.Panel5.Controls.Add(Me.lblPollutants)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 393)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(621, 37)
        Me.Panel5.TabIndex = 373
        '
        'EditAirProgramPollutantsButton
        '
        Me.EditAirProgramPollutantsButton.Location = New System.Drawing.Point(8, 6)
        Me.EditAirProgramPollutantsButton.Name = "EditAirProgramPollutantsButton"
        Me.EditAirProgramPollutantsButton.Size = New System.Drawing.Size(163, 23)
        Me.EditAirProgramPollutantsButton.TabIndex = 353
        Me.EditAirProgramPollutantsButton.Text = "Edit Air Programs && Pollutants"
        '
        'lblPollutants
        '
        Me.lblPollutants.AutoSize = True
        Me.lblPollutants.Location = New System.Drawing.Point(177, 11)
        Me.lblPollutants.Name = "lblPollutants"
        Me.lblPollutants.Size = New System.Drawing.Size(172, 13)
        Me.lblPollutants.TabIndex = 354
        Me.lblPollutants.Text = "(Only if you need to add new items)"
        '
        'LonTabPage
        '
        Me.LonTabPage.AutoScroll = True
        Me.LonTabPage.Controls.Add(Me.LonComments)
        Me.LonTabPage.Controls.Add(Me.Panel8)
        Me.LonTabPage.Location = New System.Drawing.Point(4, 22)
        Me.LonTabPage.Name = "LonTabPage"
        Me.LonTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.LonTabPage.Size = New System.Drawing.Size(621, 430)
        Me.LonTabPage.TabIndex = 1
        Me.LonTabPage.Text = "Letter of Noncompliance"
        Me.LonTabPage.UseVisualStyleBackColor = True
        '
        'LonComments
        '
        Me.LonComments.AcceptsReturn = True
        Me.LonComments.AcceptsTab = True
        Me.LonComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LonComments.Location = New System.Drawing.Point(3, 140)
        Me.LonComments.MaxLength = 4000
        Me.LonComments.Multiline = True
        Me.LonComments.Name = "LonComments"
        Me.LonComments.Size = New System.Drawing.Size(615, 287)
        Me.LonComments.TabIndex = 277
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.LonToUC)
        Me.Panel8.Controls.Add(Me.Label3)
        Me.Panel8.Controls.Add(Me.LonResolved)
        Me.Panel8.Controls.Add(Me.LonSent)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Controls.Add(Me.Label43)
        Me.Panel8.Controls.Add(Me.Label19)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(3, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(615, 137)
        Me.Panel8.TabIndex = 283
        '
        'LonToUC
        '
        Me.LonToUC.Checked = False
        Me.LonToUC.CustomFormat = "dd-MMM-yyyy"
        Me.LonToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LonToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.LonToUC.Location = New System.Drawing.Point(133, 19)
        Me.LonToUC.Name = "LonToUC"
        Me.LonToUC.ShowCheckBox = True
        Me.LonToUC.Size = New System.Drawing.Size(119, 22)
        Me.LonToUC.TabIndex = 279
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 282
        Me.Label3.Text = "Date LON Resolved"
        '
        'LonResolved
        '
        Me.LonResolved.Checked = False
        Me.LonResolved.CustomFormat = "dd-MMM-yyyy"
        Me.LonResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LonResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.LonResolved.Location = New System.Drawing.Point(133, 75)
        Me.LonResolved.Name = "LonResolved"
        Me.LonResolved.ShowCheckBox = True
        Me.LonResolved.Size = New System.Drawing.Size(119, 22)
        Me.LonResolved.TabIndex = 281
        '
        'LonSent
        '
        Me.LonSent.Checked = False
        Me.LonSent.CustomFormat = "dd-MMM-yyyy"
        Me.LonSent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LonSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.LonSent.Location = New System.Drawing.Point(133, 47)
        Me.LonSent.Name = "LonSent"
        Me.LonSent.ShowCheckBox = True
        Me.LonSent.Size = New System.Drawing.Size(119, 22)
        Me.LonSent.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(47, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Date LON Sent"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(67, 24)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(60, 13)
        Me.Label43.TabIndex = 280
        Me.Label43.Text = "Date to UC"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(5, 121)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(81, 13)
        Me.Label19.TabIndex = 278
        Me.Label19.Text = "LON Comments"
        '
        'NovTabPage
        '
        Me.NovTabPage.AutoScroll = True
        Me.NovTabPage.Controls.Add(Me.NovComments)
        Me.NovTabPage.Controls.Add(Me.Panel9)
        Me.NovTabPage.Location = New System.Drawing.Point(4, 22)
        Me.NovTabPage.Name = "NovTabPage"
        Me.NovTabPage.Size = New System.Drawing.Size(621, 430)
        Me.NovTabPage.TabIndex = 2
        Me.NovTabPage.Text = "Notice of Violation"
        Me.NovTabPage.UseVisualStyleBackColor = True
        '
        'NovComments
        '
        Me.NovComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NovComments.Location = New System.Drawing.Point(0, 143)
        Me.NovComments.MaxLength = 3980
        Me.NovComments.Multiline = True
        Me.NovComments.Name = "NovComments"
        Me.NovComments.Size = New System.Drawing.Size(621, 287)
        Me.NovComments.TabIndex = 358
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Label1)
        Me.Panel9.Controls.Add(Me.NovToUC)
        Me.Panel9.Controls.Add(Me.Label46)
        Me.Panel9.Controls.Add(Me.Label17)
        Me.Panel9.Controls.Add(Me.NovSent)
        Me.Panel9.Controls.Add(Me.NfaToPM)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Controls.Add(Me.Label45)
        Me.Panel9.Controls.Add(Me.NovResponseReceived)
        Me.Panel9.Controls.Add(Me.NfaToUC)
        Me.Panel9.Controls.Add(Me.Label9)
        Me.Panel9.Controls.Add(Me.Label44)
        Me.Panel9.Controls.Add(Me.NfaSent)
        Me.Panel9.Controls.Add(Me.NovToPM)
        Me.Panel9.Controls.Add(Me.Label13)
        Me.Panel9.Controls.Add(Me.Label26)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(621, 143)
        Me.Panel9.TabIndex = 380
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 380
        Me.Label1.Text = "NOV Comments"
        '
        'NovToUC
        '
        Me.NovToUC.Checked = False
        Me.NovToUC.CustomFormat = "dd-MMM-yyyy"
        Me.NovToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NovToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NovToUC.Location = New System.Drawing.Point(7, 7)
        Me.NovToUC.Name = "NovToUC"
        Me.NovToUC.ShowCheckBox = True
        Me.NovToUC.Size = New System.Drawing.Size(119, 22)
        Me.NovToUC.TabIndex = 372
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(462, 40)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(85, 13)
        Me.Label46.TabIndex = 379
        Me.Label46.Text = "Date NFA to PM"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(7, 219)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 13)
        Me.Label17.TabIndex = 360
        Me.Label17.Text = "NOV Comments"
        '
        'NovSent
        '
        Me.NovSent.Checked = False
        Me.NovSent.CustomFormat = "dd-MMM-yyyy"
        Me.NovSent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NovSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NovSent.Location = New System.Drawing.Point(7, 67)
        Me.NovSent.Name = "NovSent"
        Me.NovSent.ShowCheckBox = True
        Me.NovSent.Size = New System.Drawing.Size(119, 22)
        Me.NovSent.TabIndex = 366
        '
        'NfaToPM
        '
        Me.NfaToPM.Checked = False
        Me.NfaToPM.CustomFormat = "dd-MMM-yyyy"
        Me.NfaToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NfaToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NfaToPM.Location = New System.Drawing.Point(338, 35)
        Me.NfaToPM.Name = "NfaToPM"
        Me.NfaToPM.ShowCheckBox = True
        Me.NfaToPM.Size = New System.Drawing.Size(119, 22)
        Me.NfaToPM.TabIndex = 378
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(131, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 367
        Me.Label8.Text = "Date NOV Sent"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(462, 10)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(84, 13)
        Me.Label45.TabIndex = 377
        Me.Label45.Text = "Date NFA to UC"
        '
        'NovResponseReceived
        '
        Me.NovResponseReceived.Checked = False
        Me.NovResponseReceived.CustomFormat = "dd-MMM-yyyy"
        Me.NovResponseReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NovResponseReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NovResponseReceived.Location = New System.Drawing.Point(7, 97)
        Me.NovResponseReceived.Name = "NovResponseReceived"
        Me.NovResponseReceived.ShowCheckBox = True
        Me.NovResponseReceived.Size = New System.Drawing.Size(119, 22)
        Me.NovResponseReceived.TabIndex = 368
        '
        'NfaToUC
        '
        Me.NfaToUC.Checked = False
        Me.NfaToUC.CustomFormat = "dd-MMM-yyyy"
        Me.NfaToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NfaToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NfaToUC.Location = New System.Drawing.Point(338, 5)
        Me.NfaToUC.Name = "NfaToUC"
        Me.NfaToUC.ShowCheckBox = True
        Me.NfaToUC.Size = New System.Drawing.Size(119, 22)
        Me.NfaToUC.TabIndex = 376
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(131, 102)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(156, 13)
        Me.Label9.TabIndex = 369
        Me.Label9.Text = "Date NOV Response Received"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(131, 42)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(87, 13)
        Me.Label44.TabIndex = 375
        Me.Label44.Text = "Date NOV to PM"
        '
        'NfaSent
        '
        Me.NfaSent.Checked = False
        Me.NfaSent.CustomFormat = "dd-MMM-yyyy"
        Me.NfaSent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NfaSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NfaSent.Location = New System.Drawing.Point(338, 65)
        Me.NfaSent.Name = "NfaSent"
        Me.NfaSent.ShowCheckBox = True
        Me.NfaSent.Size = New System.Drawing.Size(119, 22)
        Me.NfaSent.TabIndex = 370
        '
        'NovToPM
        '
        Me.NovToPM.Checked = False
        Me.NovToPM.CustomFormat = "dd-MMM-yyyy"
        Me.NovToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NovToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.NovToPM.Location = New System.Drawing.Point(7, 37)
        Me.NovToPM.Name = "NovToPM"
        Me.NovToPM.ShowCheckBox = True
        Me.NovToPM.Size = New System.Drawing.Size(119, 22)
        Me.NovToPM.TabIndex = 374
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(462, 70)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(109, 13)
        Me.Label13.TabIndex = 371
        Me.Label13.Text = "Date NFA Letter Sent"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(131, 12)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(86, 13)
        Me.Label26.TabIndex = 373
        Me.Label26.Text = "Date NOV to UC"
        '
        'COTabPage
        '
        Me.COTabPage.AutoScroll = True
        Me.COTabPage.Controls.Add(Me.COComments)
        Me.COTabPage.Controls.Add(Me.Panel10)
        Me.COTabPage.Location = New System.Drawing.Point(4, 22)
        Me.COTabPage.Name = "COTabPage"
        Me.COTabPage.Size = New System.Drawing.Size(621, 430)
        Me.COTabPage.TabIndex = 3
        Me.COTabPage.Text = "Consent Order"
        Me.COTabPage.UseVisualStyleBackColor = True
        '
        'COComments
        '
        Me.COComments.AcceptsReturn = True
        Me.COComments.AcceptsTab = True
        Me.COComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.COComments.Location = New System.Drawing.Point(0, 294)
        Me.COComments.MaxLength = 4000
        Me.COComments.Multiline = True
        Me.COComments.Name = "COComments"
        Me.COComments.Size = New System.Drawing.Size(621, 136)
        Me.COComments.TabIndex = 357
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.CoNumber)
        Me.Panel10.Controls.Add(Me.COToUC)
        Me.Panel10.Controls.Add(Me.COPenaltyComments)
        Me.Panel10.Controls.Add(Me.Label29)
        Me.Panel10.Controls.Add(Me.Label21)
        Me.Panel10.Controls.Add(Me.COPenaltyAmount)
        Me.Panel10.Controls.Add(Me.Label49)
        Me.Panel10.Controls.Add(Me.Label28)
        Me.Panel10.Controls.Add(Me.Label48)
        Me.Panel10.Controls.Add(Me.COProposed)
        Me.Panel10.Controls.Add(Me.COToPM)
        Me.Panel10.Controls.Add(Me.Label14)
        Me.Panel10.Controls.Add(Me.Label47)
        Me.Panel10.Controls.Add(Me.COReceivedFromDirector)
        Me.Panel10.Controls.Add(Me.Label15)
        Me.Panel10.Controls.Add(Me.COExecuted)
        Me.Panel10.Controls.Add(Me.Label16)
        Me.Panel10.Controls.Add(Me.COReceivedfromCompany)
        Me.Panel10.Controls.Add(Me.Label20)
        Me.Panel10.Controls.Add(Me.Label18)
        Me.Panel10.Controls.Add(Me.COResolved)
        Me.Panel10.Controls.Add(Me.StipulatedPenaltiesGroupBox)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(621, 294)
        Me.Panel10.TabIndex = 393
        '
        'CoNumber
        '
        Me.CoNumber.Location = New System.Drawing.Point(135, 186)
        Me.CoNumber.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.CoNumber.Name = "CoNumber"
        Me.CoNumber.Size = New System.Drawing.Size(66, 20)
        Me.CoNumber.TabIndex = 399
        '
        'COToUC
        '
        Me.COToUC.Checked = False
        Me.COToUC.CustomFormat = "dd-MMM-yyyy"
        Me.COToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COToUC.Location = New System.Drawing.Point(7, 6)
        Me.COToUC.Name = "COToUC"
        Me.COToUC.ShowCheckBox = True
        Me.COToUC.Size = New System.Drawing.Size(119, 22)
        Me.COToUC.TabIndex = 385
        '
        'COPenaltyComments
        '
        Me.COPenaltyComments.AcceptsReturn = True
        Me.COPenaltyComments.AcceptsTab = True
        Me.COPenaltyComments.Location = New System.Drawing.Point(475, 28)
        Me.COPenaltyComments.MaxLength = 4000
        Me.COPenaltyComments.Multiline = True
        Me.COPenaltyComments.Name = "COPenaltyComments"
        Me.COPenaltyComments.Size = New System.Drawing.Size(373, 48)
        Me.COPenaltyComments.TabIndex = 355
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(372, 31)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(97, 13)
        Me.Label29.TabIndex = 353
        Me.Label29.Text = "Penalty Comments:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(11, 275)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(74, 13)
        Me.Label21.TabIndex = 359
        Me.Label21.Text = "CO Comments"
        '
        'COPenaltyAmount
        '
        Me.COPenaltyAmount.Location = New System.Drawing.Point(475, 4)
        Me.COPenaltyAmount.MaxLength = 20
        Me.COPenaltyAmount.Name = "COPenaltyAmount"
        Me.COPenaltyAmount.Size = New System.Drawing.Size(100, 20)
        Me.COPenaltyAmount.TabIndex = 354
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(44, 188)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(85, 13)
        Me.Label49.TabIndex = 389
        Me.Label49.Text = "CO # EPD-AQC-"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(376, 7)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(93, 13)
        Me.Label28.TabIndex = 344
        Me.Label28.Text = "Penalty Assessed:"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(132, 39)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(79, 13)
        Me.Label48.TabIndex = 388
        Me.Label48.Text = "Date CO to PM"
        '
        'COProposed
        '
        Me.COProposed.Checked = False
        Me.COProposed.CustomFormat = "dd-MMM-yyyy"
        Me.COProposed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COProposed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COProposed.Location = New System.Drawing.Point(7, 64)
        Me.COProposed.Name = "COProposed"
        Me.COProposed.ShowCheckBox = True
        Me.COProposed.Size = New System.Drawing.Size(119, 22)
        Me.COProposed.TabIndex = 372
        '
        'COToPM
        '
        Me.COToPM.Checked = False
        Me.COToPM.CustomFormat = "dd-MMM-yyyy"
        Me.COToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COToPM.Location = New System.Drawing.Point(7, 34)
        Me.COToPM.Name = "COToPM"
        Me.COToPM.ShowCheckBox = True
        Me.COToPM.Size = New System.Drawing.Size(119, 22)
        Me.COToPM.TabIndex = 387
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(132, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(149, 13)
        Me.Label14.TabIndex = 373
        Me.Label14.Text = "Date Consent Order Proposed"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(132, 11)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(78, 13)
        Me.Label47.TabIndex = 386
        Me.Label47.Text = "Date CO to UC"
        '
        'COReceivedFromDirector
        '
        Me.COReceivedFromDirector.Checked = False
        Me.COReceivedFromDirector.CustomFormat = "dd-MMM-yyyy"
        Me.COReceivedFromDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COReceivedFromDirector.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COReceivedFromDirector.Location = New System.Drawing.Point(7, 124)
        Me.COReceivedFromDirector.Name = "COReceivedFromDirector"
        Me.COReceivedFromDirector.ShowCheckBox = True
        Me.COReceivedFromDirector.Size = New System.Drawing.Size(119, 22)
        Me.COReceivedFromDirector.TabIndex = 374
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(132, 129)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(172, 13)
        Me.Label15.TabIndex = 375
        Me.Label15.Text = "CO Received from Director's Office"
        '
        'COExecuted
        '
        Me.COExecuted.Checked = False
        Me.COExecuted.CustomFormat = "dd-MMM-yyyy"
        Me.COExecuted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COExecuted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COExecuted.Location = New System.Drawing.Point(7, 154)
        Me.COExecuted.Name = "COExecuted"
        Me.COExecuted.ShowCheckBox = True
        Me.COExecuted.Size = New System.Drawing.Size(119, 22)
        Me.COExecuted.TabIndex = 376
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(132, 159)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(126, 13)
        Me.Label16.TabIndex = 377
        Me.Label16.Text = "Consent Order Executed "
        '
        'COReceivedfromCompany
        '
        Me.COReceivedfromCompany.Checked = False
        Me.COReceivedfromCompany.CustomFormat = "dd-MMM-yyyy"
        Me.COReceivedfromCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COReceivedfromCompany.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COReceivedfromCompany.Location = New System.Drawing.Point(7, 94)
        Me.COReceivedfromCompany.Name = "COReceivedfromCompany"
        Me.COReceivedfromCompany.ShowCheckBox = True
        Me.COReceivedfromCompany.Size = New System.Drawing.Size(119, 22)
        Me.COReceivedfromCompany.TabIndex = 378
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(132, 217)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(123, 13)
        Me.Label20.TabIndex = 381
        Me.Label20.Text = "Consent Order Resolved"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(132, 99)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(144, 13)
        Me.Label18.TabIndex = 379
        Me.Label18.Text = "CO Received From Company"
        '
        'COResolved
        '
        Me.COResolved.Checked = False
        Me.COResolved.CustomFormat = "dd-MMM-yyyy"
        Me.COResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COResolved.Location = New System.Drawing.Point(7, 212)
        Me.COResolved.Name = "COResolved"
        Me.COResolved.ShowCheckBox = True
        Me.COResolved.Size = New System.Drawing.Size(119, 22)
        Me.COResolved.TabIndex = 380
        '
        'StipulatedPenaltiesGroupBox
        '
        Me.StipulatedPenaltiesGroupBox.Controls.Add(Me.StipulatedPenaltyAmount)
        Me.StipulatedPenaltiesGroupBox.Controls.Add(Me.Label30)
        Me.StipulatedPenaltiesGroupBox.Controls.Add(Me.StipulatedPenalties)
        Me.StipulatedPenaltiesGroupBox.Controls.Add(Me.StipulatedPenaltyComments)
        Me.StipulatedPenaltiesGroupBox.Controls.Add(Me.Label31)
        Me.StipulatedPenaltiesGroupBox.Controls.Add(Me.StipulatedPenaltyControls)
        Me.StipulatedPenaltiesGroupBox.Enabled = False
        Me.StipulatedPenaltiesGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.StipulatedPenaltiesGroupBox.Location = New System.Drawing.Point(475, 82)
        Me.StipulatedPenaltiesGroupBox.Name = "StipulatedPenaltiesGroupBox"
        Me.StipulatedPenaltiesGroupBox.Size = New System.Drawing.Size(373, 206)
        Me.StipulatedPenaltiesGroupBox.TabIndex = 397
        Me.StipulatedPenaltiesGroupBox.TabStop = False
        Me.StipulatedPenaltiesGroupBox.Text = "Stipulated Penalties"
        '
        'StipulatedPenaltyAmount
        '
        Me.StipulatedPenaltyAmount.Location = New System.Drawing.Point(58, 18)
        Me.StipulatedPenaltyAmount.Name = "StipulatedPenaltyAmount"
        Me.StipulatedPenaltyAmount.Size = New System.Drawing.Size(91, 20)
        Me.StipulatedPenaltyAmount.TabIndex = 351
        Me.StipulatedPenaltyAmount.Tag = ""
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 21)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(46, 13)
        Me.Label30.TabIndex = 348
        Me.Label30.Text = "Amount:"
        '
        'StipulatedPenalties
        '
        Me.StipulatedPenalties.AllowUserToAddRows = False
        Me.StipulatedPenalties.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.StipulatedPenalties.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.StipulatedPenalties.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StipulatedPenalties.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.StipulatedPenalties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.StipulatedPenalties.DefaultCellStyle = DataGridViewCellStyle3
        Me.StipulatedPenalties.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.StipulatedPenalties.Location = New System.Drawing.Point(9, 100)
        Me.StipulatedPenalties.MultiSelect = False
        Me.StipulatedPenalties.Name = "StipulatedPenalties"
        Me.StipulatedPenalties.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StipulatedPenalties.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.StipulatedPenalties.RowHeadersVisible = False
        Me.StipulatedPenalties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.StipulatedPenalties.Size = New System.Drawing.Size(358, 106)
        Me.StipulatedPenalties.TabIndex = 382
        Me.StipulatedPenalties.Visible = False
        '
        'StipulatedPenaltyComments
        '
        Me.StipulatedPenaltyComments.Location = New System.Drawing.Point(8, 58)
        Me.StipulatedPenaltyComments.Multiline = True
        Me.StipulatedPenaltyComments.Name = "StipulatedPenaltyComments"
        Me.StipulatedPenaltyComments.Size = New System.Drawing.Size(359, 36)
        Me.StipulatedPenaltyComments.TabIndex = 366
        Me.StipulatedPenaltyComments.Tag = ""
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(6, 41)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(105, 13)
        Me.Label31.TabIndex = 350
        Me.Label31.Text = "Comments (optional):"
        '
        'StipulatedPenaltyControls
        '
        Me.StipulatedPenaltyControls.Controls.Add(Me.UpdateStipulatedPenaltyButton)
        Me.StipulatedPenaltyControls.Controls.Add(Me.DeleteStipulatedPenaltyButton)
        Me.StipulatedPenaltyControls.Controls.Add(Me.SaveNewStipulatedPenaltyButton)
        Me.StipulatedPenaltyControls.Controls.Add(Me.ClearStipulatedPenaltyFormButton)
        Me.StipulatedPenaltyControls.Location = New System.Drawing.Point(152, 16)
        Me.StipulatedPenaltyControls.Name = "StipulatedPenaltyControls"
        Me.StipulatedPenaltyControls.Size = New System.Drawing.Size(214, 32)
        Me.StipulatedPenaltyControls.TabIndex = 394
        '
        'UpdateStipulatedPenaltyButton
        '
        Me.UpdateStipulatedPenaltyButton.AutoSize = True
        Me.UpdateStipulatedPenaltyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UpdateStipulatedPenaltyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.UpdateStipulatedPenaltyButton.Location = New System.Drawing.Point(3, 0)
        Me.UpdateStipulatedPenaltyButton.Name = "UpdateStipulatedPenaltyButton"
        Me.UpdateStipulatedPenaltyButton.Size = New System.Drawing.Size(90, 23)
        Me.UpdateStipulatedPenaltyButton.TabIndex = 393
        Me.UpdateStipulatedPenaltyButton.Tag = ""
        Me.UpdateStipulatedPenaltyButton.Text = "Update Penalty"
        Me.UpdateStipulatedPenaltyButton.Visible = False
        '
        'DeleteStipulatedPenaltyButton
        '
        Me.DeleteStipulatedPenaltyButton.AutoSize = True
        Me.DeleteStipulatedPenaltyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.DeleteStipulatedPenaltyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteStipulatedPenaltyButton.Location = New System.Drawing.Point(99, 0)
        Me.DeleteStipulatedPenaltyButton.Name = "DeleteStipulatedPenaltyButton"
        Me.DeleteStipulatedPenaltyButton.Size = New System.Drawing.Size(86, 23)
        Me.DeleteStipulatedPenaltyButton.TabIndex = 349
        Me.DeleteStipulatedPenaltyButton.Tag = ""
        Me.DeleteStipulatedPenaltyButton.Text = "Delete Penalty"
        Me.DeleteStipulatedPenaltyButton.Visible = False
        '
        'SaveNewStipulatedPenaltyButton
        '
        Me.SaveNewStipulatedPenaltyButton.AutoSize = True
        Me.SaveNewStipulatedPenaltyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SaveNewStipulatedPenaltyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveNewStipulatedPenaltyButton.Location = New System.Drawing.Point(3, 0)
        Me.SaveNewStipulatedPenaltyButton.Name = "SaveNewStipulatedPenaltyButton"
        Me.SaveNewStipulatedPenaltyButton.Size = New System.Drawing.Size(99, 23)
        Me.SaveNewStipulatedPenaltyButton.TabIndex = 349
        Me.SaveNewStipulatedPenaltyButton.Tag = ""
        Me.SaveNewStipulatedPenaltyButton.Text = "Add New Penalty"
        '
        'ClearStipulatedPenaltyFormButton
        '
        Me.ClearStipulatedPenaltyFormButton.BackgroundImage = CType(resources.GetObject("ClearStipulatedPenaltyFormButton.BackgroundImage"), System.Drawing.Image)
        Me.ClearStipulatedPenaltyFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClearStipulatedPenaltyFormButton.Location = New System.Drawing.Point(190, 0)
        Me.ClearStipulatedPenaltyFormButton.Name = "ClearStipulatedPenaltyFormButton"
        Me.ClearStipulatedPenaltyFormButton.Size = New System.Drawing.Size(24, 23)
        Me.ClearStipulatedPenaltyFormButton.TabIndex = 384
        '
        'AOTabPage
        '
        Me.AOTabPage.AutoScroll = True
        Me.AOTabPage.Controls.Add(Me.AOComments)
        Me.AOTabPage.Controls.Add(Me.Panel11)
        Me.AOTabPage.Location = New System.Drawing.Point(4, 22)
        Me.AOTabPage.Name = "AOTabPage"
        Me.AOTabPage.Size = New System.Drawing.Size(621, 430)
        Me.AOTabPage.TabIndex = 4
        Me.AOTabPage.Text = "Administrative Order"
        Me.AOTabPage.UseVisualStyleBackColor = True
        '
        'AOComments
        '
        Me.AOComments.AcceptsReturn = True
        Me.AOComments.AcceptsTab = True
        Me.AOComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AOComments.Location = New System.Drawing.Point(0, 111)
        Me.AOComments.MaxLength = 4000
        Me.AOComments.Multiline = True
        Me.AOComments.Name = "AOComments"
        Me.AOComments.Size = New System.Drawing.Size(621, 319)
        Me.AOComments.TabIndex = 343
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.AOExecuted)
        Me.Panel11.Controls.Add(Me.Label42)
        Me.Panel11.Controls.Add(Me.Label25)
        Me.Panel11.Controls.Add(Me.AOResolved)
        Me.Panel11.Controls.Add(Me.Label32)
        Me.Panel11.Controls.Add(Me.Label41)
        Me.Panel11.Controls.Add(Me.AOAppealed)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(621, 111)
        Me.Panel11.TabIndex = 388
        '
        'AOExecuted
        '
        Me.AOExecuted.Checked = False
        Me.AOExecuted.CustomFormat = "dd-MMM-yyyy"
        Me.AOExecuted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AOExecuted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.AOExecuted.Location = New System.Drawing.Point(9, 8)
        Me.AOExecuted.Name = "AOExecuted"
        Me.AOExecuted.ShowCheckBox = True
        Me.AOExecuted.Size = New System.Drawing.Size(119, 22)
        Me.AOExecuted.TabIndex = 382
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(137, 66)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(149, 13)
        Me.Label42.TabIndex = 387
        Me.Label42.Text = "Administrative Order Resolved"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(13, 95)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(74, 13)
        Me.Label25.TabIndex = 339
        Me.Label25.Text = "AO Comments"
        '
        'AOResolved
        '
        Me.AOResolved.Checked = False
        Me.AOResolved.CustomFormat = "dd-MMM-yyyy"
        Me.AOResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AOResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.AOResolved.Location = New System.Drawing.Point(9, 62)
        Me.AOResolved.Name = "AOResolved"
        Me.AOResolved.ShowCheckBox = True
        Me.AOResolved.Size = New System.Drawing.Size(119, 22)
        Me.AOResolved.TabIndex = 386
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(137, 12)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(289, 13)
        Me.Label32.TabIndex = 383
        Me.Label32.Text = "Administrative Order Executed (Referral to Attorney General)"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(137, 38)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(239, 13)
        Me.Label41.TabIndex = 385
        Me.Label41.Text = "Administrative Order Appealed to Civil State Court"
        '
        'AOAppealed
        '
        Me.AOAppealed.Checked = False
        Me.AOAppealed.CustomFormat = "dd-MMM-yyyy"
        Me.AOAppealed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AOAppealed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.AOAppealed.Location = New System.Drawing.Point(9, 34)
        Me.AOAppealed.Name = "AOAppealed"
        Me.AOAppealed.ShowCheckBox = True
        Me.AOAppealed.Size = New System.Drawing.Size(119, 22)
        Me.AOAppealed.TabIndex = 384
        '
        'DocumentsTabPage
        '
        Me.DocumentsTabPage.Controls.Add(Me.lblMessage)
        Me.DocumentsTabPage.Controls.Add(Me.pnlDocument)
        Me.DocumentsTabPage.Controls.Add(Me.lblCurrentFiles)
        Me.DocumentsTabPage.Controls.Add(Me.DocumentList)
        Me.DocumentsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DocumentsTabPage.Name = "DocumentsTabPage"
        Me.DocumentsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DocumentsTabPage.Size = New System.Drawing.Size(621, 430)
        Me.DocumentsTabPage.TabIndex = 7
        Me.DocumentsTabPage.Text = "Documents"
        Me.DocumentsTabPage.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.Color.OldLace
        Me.lblMessage.ForeColor = System.Drawing.Color.DarkRed
        Me.lblMessage.Location = New System.Drawing.Point(15, 216)
        Me.lblMessage.MaximumSize = New System.Drawing.Size(550, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(5)
        Me.lblMessage.Size = New System.Drawing.Size(119, 23)
        Me.lblMessage.TabIndex = 18
        Me.lblMessage.Text = "Message Placeholder"
        Me.lblMessage.Visible = False
        '
        'pnlDocument
        '
        Me.pnlDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDocument.Controls.Add(Me.lblDocumentDescription)
        Me.pnlDocument.Controls.Add(Me.btnDocumentDownload)
        Me.pnlDocument.Controls.Add(Me.txtDocumentDescription)
        Me.pnlDocument.Controls.Add(Me.DocumentUpdateButton)
        Me.pnlDocument.Controls.Add(Me.lblDocumentName)
        Me.pnlDocument.Enabled = False
        Me.pnlDocument.Location = New System.Drawing.Point(14, 127)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Size = New System.Drawing.Size(309, 73)
        Me.pnlDocument.TabIndex = 16
        Me.pnlDocument.Visible = False
        '
        'lblDocumentDescription
        '
        Me.lblDocumentDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentDescription.AutoSize = True
        Me.lblDocumentDescription.Location = New System.Drawing.Point(0, 36)
        Me.lblDocumentDescription.Name = "lblDocumentDescription"
        Me.lblDocumentDescription.Size = New System.Drawing.Size(60, 13)
        Me.lblDocumentDescription.TabIndex = 6
        Me.lblDocumentDescription.Text = "Description"
        '
        'btnDocumentDownload
        '
        Me.btnDocumentDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDocumentDownload.Location = New System.Drawing.Point(197, 50)
        Me.btnDocumentDownload.Name = "btnDocumentDownload"
        Me.btnDocumentDownload.Size = New System.Drawing.Size(112, 23)
        Me.btnDocumentDownload.TabIndex = 2
        Me.btnDocumentDownload.Text = "Download"
        Me.btnDocumentDownload.UseVisualStyleBackColor = True
        '
        'txtDocumentDescription
        '
        Me.txtDocumentDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDocumentDescription.Location = New System.Drawing.Point(0, 52)
        Me.txtDocumentDescription.Name = "txtDocumentDescription"
        Me.txtDocumentDescription.Size = New System.Drawing.Size(73, 20)
        Me.txtDocumentDescription.TabIndex = 0
        '
        'DocumentUpdateButton
        '
        Me.DocumentUpdateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DocumentUpdateButton.AutoSize = True
        Me.DocumentUpdateButton.Location = New System.Drawing.Point(79, 50)
        Me.DocumentUpdateButton.Name = "DocumentUpdateButton"
        Me.DocumentUpdateButton.Size = New System.Drawing.Size(112, 23)
        Me.DocumentUpdateButton.TabIndex = 1
        Me.DocumentUpdateButton.Text = "Update description"
        Me.DocumentUpdateButton.UseVisualStyleBackColor = True
        '
        'lblDocumentName
        '
        Me.lblDocumentName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentName.AutoSize = True
        Me.lblDocumentName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblDocumentName.Location = New System.Drawing.Point(0, 10)
        Me.lblDocumentName.Name = "lblDocumentName"
        Me.lblDocumentName.Size = New System.Drawing.Size(145, 17)
        Me.lblDocumentName.TabIndex = 14
        Me.lblDocumentName.Text = "FileName placeholder"
        '
        'lblCurrentFiles
        '
        Me.lblCurrentFiles.AutoSize = True
        Me.lblCurrentFiles.Location = New System.Drawing.Point(14, 16)
        Me.lblCurrentFiles.Name = "lblCurrentFiles"
        Me.lblCurrentFiles.Size = New System.Drawing.Size(98, 13)
        Me.lblCurrentFiles.TabIndex = 5
        Me.lblCurrentFiles.Text = "Current Documents"
        '
        'DocumentList
        '
        Me.DocumentList.AllowUserToAddRows = False
        Me.DocumentList.AllowUserToDeleteRows = False
        Me.DocumentList.AllowUserToOrderColumns = True
        Me.DocumentList.AllowUserToResizeRows = False
        Me.DocumentList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DocumentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DocumentList.Enabled = False
        Me.DocumentList.Location = New System.Drawing.Point(14, 32)
        Me.DocumentList.MinimumSize = New System.Drawing.Size(300, 55)
        Me.DocumentList.MultiSelect = False
        Me.DocumentList.Name = "DocumentList"
        Me.DocumentList.ReadOnly = True
        Me.DocumentList.RowHeadersVisible = False
        Me.DocumentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DocumentList.Size = New System.Drawing.Size(309, 89)
        Me.DocumentList.StandardTab = True
        Me.DocumentList.TabIndex = 0
        '
        'AuditHistoryTabPage
        '
        Me.AuditHistoryTabPage.Controls.Add(Me.AuditHistory)
        Me.AuditHistoryTabPage.Controls.Add(Me.Panel12)
        Me.AuditHistoryTabPage.Location = New System.Drawing.Point(4, 22)
        Me.AuditHistoryTabPage.Name = "AuditHistoryTabPage"
        Me.AuditHistoryTabPage.Size = New System.Drawing.Size(621, 430)
        Me.AuditHistoryTabPage.TabIndex = 6
        Me.AuditHistoryTabPage.Text = "Audit History"
        Me.AuditHistoryTabPage.UseVisualStyleBackColor = True
        '
        'AuditHistory
        '
        Me.AuditHistory.AllowUserToAddRows = False
        Me.AuditHistory.AllowUserToDeleteRows = False
        Me.AuditHistory.AllowUserToOrderColumns = True
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AuditHistory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.AuditHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.AuditHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AuditHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AuditHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.AuditHistory.Location = New System.Drawing.Point(0, 66)
        Me.AuditHistory.Name = "AuditHistory"
        Me.AuditHistory.ReadOnly = True
        Me.AuditHistory.RowHeadersVisible = False
        Me.AuditHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.AuditHistory.Size = New System.Drawing.Size(621, 364)
        Me.AuditHistory.TabIndex = 1
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.RefreshAuditHistory)
        Me.Panel12.Controls.Add(Me.ExportAuditHistory)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(621, 66)
        Me.Panel12.TabIndex = 0
        '
        'RefreshAuditHistory
        '
        Me.RefreshAuditHistory.AutoSize = True
        Me.RefreshAuditHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RefreshAuditHistory.Location = New System.Drawing.Point(106, 20)
        Me.RefreshAuditHistory.Name = "RefreshAuditHistory"
        Me.RefreshAuditHistory.Size = New System.Drawing.Size(54, 23)
        Me.RefreshAuditHistory.TabIndex = 2
        Me.RefreshAuditHistory.Text = "Refresh"
        Me.RefreshAuditHistory.UseVisualStyleBackColor = True
        '
        'ExportAuditHistory
        '
        Me.ExportAuditHistory.AutoSize = True
        Me.ExportAuditHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ExportAuditHistory.Location = New System.Drawing.Point(8, 20)
        Me.ExportAuditHistory.Name = "ExportAuditHistory"
        Me.ExportAuditHistory.Size = New System.Drawing.Size(88, 23)
        Me.ExportAuditHistory.TabIndex = 0
        Me.ExportAuditHistory.Text = "Export to Excel"
        Me.ExportAuditHistory.UseVisualStyleBackColor = True
        '
        'EpaValuesTabPage
        '
        Me.EpaValuesTabPage.Controls.Add(Me.Label40)
        Me.EpaValuesTabPage.Controls.Add(Me.Label10)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsAoResolvedActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.Label24)
        Me.EpaValuesTabPage.Controls.Add(Me.Label39)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsNfaActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsAoCivilCourtActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsKeyActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.Label38)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsNovActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsAoToAgActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.Label11)
        Me.EpaValuesTabPage.Controls.Add(Me.Label37)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsCoProposedActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsStipulatedPenalitiesActionNumbers)
        Me.EpaValuesTabPage.Controls.Add(Me.Label23)
        Me.EpaValuesTabPage.Controls.Add(Me.Label36)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsCoExecutedActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.AfsCoResolvedActionNumber)
        Me.EpaValuesTabPage.Controls.Add(Me.Label35)
        Me.EpaValuesTabPage.Location = New System.Drawing.Point(4, 22)
        Me.EpaValuesTabPage.Name = "EpaValuesTabPage"
        Me.EpaValuesTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.EpaValuesTabPage.Size = New System.Drawing.Size(621, 430)
        Me.EpaValuesTabPage.TabIndex = 8
        Me.EpaValuesTabPage.Text = "EPA Values"
        Me.EpaValuesTabPage.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(34, 322)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(143, 13)
        Me.Label40.TabIndex = 352
        Me.Label40.Text = "AO Resolved Action Number"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(79, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 13)
        Me.Label10.TabIndex = 334
        Me.Label10.Text = "Key Action Number"
        '
        'AfsAoResolvedActionNumber
        '
        Me.AfsAoResolvedActionNumber.Location = New System.Drawing.Point(183, 319)
        Me.AfsAoResolvedActionNumber.Name = "AfsAoResolvedActionNumber"
        Me.AfsAoResolvedActionNumber.ReadOnly = True
        Me.AfsAoResolvedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsAoResolvedActionNumber.TabIndex = 351
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(76, 113)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(101, 13)
        Me.Label24.TabIndex = 340
        Me.Label24.Text = "NFA Action Number"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(50, 270)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(127, 13)
        Me.Label39.TabIndex = 350
        Me.Label39.Text = "Civil Court Action Number"
        '
        'AfsNfaActionNumber
        '
        Me.AfsNfaActionNumber.Location = New System.Drawing.Point(183, 110)
        Me.AfsNfaActionNumber.Name = "AfsNfaActionNumber"
        Me.AfsNfaActionNumber.ReadOnly = True
        Me.AfsNfaActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsNfaActionNumber.TabIndex = 339
        '
        'AfsAoCivilCourtActionNumber
        '
        Me.AfsAoCivilCourtActionNumber.Location = New System.Drawing.Point(183, 267)
        Me.AfsAoCivilCourtActionNumber.Name = "AfsAoCivilCourtActionNumber"
        Me.AfsAoCivilCourtActionNumber.ReadOnly = True
        Me.AfsAoCivilCourtActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsAoCivilCourtActionNumber.TabIndex = 349
        '
        'AfsKeyActionNumber
        '
        Me.AfsKeyActionNumber.Location = New System.Drawing.Point(183, 32)
        Me.AfsKeyActionNumber.Name = "AfsKeyActionNumber"
        Me.AfsKeyActionNumber.ReadOnly = True
        Me.AfsKeyActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsKeyActionNumber.TabIndex = 333
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(26, 296)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(151, 13)
        Me.Label38.TabIndex = 348
        Me.Label38.Text = "Referred to AG Action Number"
        '
        'AfsNovActionNumber
        '
        Me.AfsNovActionNumber.Location = New System.Drawing.Point(183, 84)
        Me.AfsNovActionNumber.Name = "AfsNovActionNumber"
        Me.AfsNovActionNumber.ReadOnly = True
        Me.AfsNovActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsNovActionNumber.TabIndex = 335
        '
        'AfsAoToAgActionNumber
        '
        Me.AfsAoToAgActionNumber.Location = New System.Drawing.Point(183, 293)
        Me.AfsAoToAgActionNumber.Name = "AfsAoToAgActionNumber"
        Me.AfsAoToAgActionNumber.ReadOnly = True
        Me.AfsAoToAgActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsAoToAgActionNumber.TabIndex = 347
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(74, 87)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 13)
        Me.Label11.TabIndex = 336
        Me.Label11.Text = "NOV Action Number"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(240, 165)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(178, 13)
        Me.Label37.TabIndex = 346
        Me.Label37.Text = "Stipulated Penalties Action Numbers"
        '
        'AfsCoProposedActionNumber
        '
        Me.AfsCoProposedActionNumber.Location = New System.Drawing.Point(183, 163)
        Me.AfsCoProposedActionNumber.Name = "AfsCoProposedActionNumber"
        Me.AfsCoProposedActionNumber.ReadOnly = True
        Me.AfsCoProposedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsCoProposedActionNumber.TabIndex = 337
        '
        'AfsStipulatedPenalitiesActionNumbers
        '
        Me.AfsStipulatedPenalitiesActionNumbers.Location = New System.Drawing.Point(243, 189)
        Me.AfsStipulatedPenalitiesActionNumbers.Multiline = True
        Me.AfsStipulatedPenalitiesActionNumbers.Name = "AfsStipulatedPenalitiesActionNumbers"
        Me.AfsStipulatedPenalitiesActionNumbers.ReadOnly = True
        Me.AfsStipulatedPenalitiesActionNumbers.Size = New System.Drawing.Size(175, 46)
        Me.AfsStipulatedPenalitiesActionNumbers.TabIndex = 345
        Me.AfsStipulatedPenalitiesActionNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(31, 165)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(143, 13)
        Me.Label23.TabIndex = 338
        Me.Label23.Text = "CO Proposed Action Number"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(34, 218)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(143, 13)
        Me.Label36.TabIndex = 344
        Me.Label36.Text = "CO Resolved Action Number"
        '
        'AfsCoExecutedActionNumber
        '
        Me.AfsCoExecutedActionNumber.Location = New System.Drawing.Point(183, 189)
        Me.AfsCoExecutedActionNumber.Name = "AfsCoExecutedActionNumber"
        Me.AfsCoExecutedActionNumber.ReadOnly = True
        Me.AfsCoExecutedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsCoExecutedActionNumber.TabIndex = 341
        '
        'AfsCoResolvedActionNumber
        '
        Me.AfsCoResolvedActionNumber.Location = New System.Drawing.Point(183, 215)
        Me.AfsCoResolvedActionNumber.Name = "AfsCoResolvedActionNumber"
        Me.AfsCoResolvedActionNumber.ReadOnly = True
        Me.AfsCoResolvedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.AfsCoResolvedActionNumber.TabIndex = 343
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(34, 192)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(143, 13)
        Me.Label35.TabIndex = 342
        Me.Label35.Text = "CO Executed Action Number"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(665, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 342
        Me.Label5.Text = "Staff Responsible"
        '
        'StaffResponsible
        '
        Me.StaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.StaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.StaffResponsible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StaffResponsible.Enabled = False
        Me.StaffResponsible.Location = New System.Drawing.Point(761, 40)
        Me.StaffResponsible.Name = "StaffResponsible"
        Me.StaffResponsible.Size = New System.Drawing.Size(119, 21)
        Me.StaffResponsible.TabIndex = 343
        '
        'ResolvedDate
        '
        Me.ResolvedDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResolvedDate.Checked = False
        Me.ResolvedDate.CustomFormat = "dd-MMM-yyyy"
        Me.ResolvedDate.Enabled = False
        Me.ResolvedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResolvedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ResolvedDate.Location = New System.Drawing.Point(498, 67)
        Me.ResolvedDate.Name = "ResolvedDate"
        Me.ResolvedDate.Size = New System.Drawing.Size(119, 22)
        Me.ResolvedDate.TabIndex = 0
        Me.ResolvedDate.Visible = False
        '
        'HeaderPanel
        '
        Me.HeaderPanel.Controls.Add(Me.ResolvedCheckBox)
        Me.HeaderPanel.Controls.Add(Me.FacilityNotApprovedDisplay)
        Me.HeaderPanel.Controls.Add(Me.FacilityNameDisplay)
        Me.HeaderPanel.Controls.Add(Me.ComplianceStatusDisplay)
        Me.HeaderPanel.Controls.Add(Me.Label5)
        Me.HeaderPanel.Controls.Add(Me.AirsNumberDisplay)
        Me.HeaderPanel.Controls.Add(Me.StaffResponsible)
        Me.HeaderPanel.Controls.Add(Me.EnforcementIdDisplay)
        Me.HeaderPanel.Controls.Add(Me.ResolvedDate)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 49)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(629, 96)
        Me.HeaderPanel.TabIndex = 268
        '
        'ResolvedCheckBox
        '
        Me.ResolvedCheckBox.AutoSize = True
        Me.ResolvedCheckBox.Enabled = False
        Me.ResolvedCheckBox.Location = New System.Drawing.Point(684, 71)
        Me.ResolvedCheckBox.Name = "ResolvedCheckBox"
        Me.ResolvedCheckBox.Size = New System.Drawing.Size(71, 17)
        Me.ResolvedCheckBox.TabIndex = 375
        Me.ResolvedCheckBox.Text = "Resolved"
        Me.ResolvedCheckBox.UseVisualStyleBackColor = True
        Me.ResolvedCheckBox.Visible = False
        '
        'FacilityNotApprovedDisplay
        '
        Me.FacilityNotApprovedDisplay.AutoSize = True
        Me.FacilityNotApprovedDisplay.Location = New System.Drawing.Point(12, 80)
        Me.FacilityNotApprovedDisplay.Name = "FacilityNotApprovedDisplay"
        Me.FacilityNotApprovedDisplay.Size = New System.Drawing.Size(371, 13)
        Me.FacilityNotApprovedDisplay.TabIndex = 374
        Me.FacilityNotApprovedDisplay.Text = "Facility not approved in the Facility Creator Tool. Data will not be sent to EPA." &
    ""
        Me.FacilityNotApprovedDisplay.Visible = False
        '
        'FacilityNameDisplay
        '
        Me.FacilityNameDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityNameDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityNameDisplay.Location = New System.Drawing.Point(95, 41)
        Me.FacilityNameDisplay.Name = "FacilityNameDisplay"
        Me.FacilityNameDisplay.Size = New System.Drawing.Size(294, 34)
        Me.FacilityNameDisplay.TabIndex = 13
        Me.FacilityNameDisplay.Text = "Facility Name, City"
        '
        'ComplianceStatusDisplay
        '
        Me.ComplianceStatusDisplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComplianceStatusDisplay.AutoSize = True
        Me.ComplianceStatusDisplay.Location = New System.Drawing.Point(522, 16)
        Me.ComplianceStatusDisplay.Name = "ComplianceStatusDisplay"
        Me.ComplianceStatusDisplay.Size = New System.Drawing.Size(95, 13)
        Me.ComplianceStatusDisplay.TabIndex = 373
        Me.ComplianceStatusDisplay.Text = "Compliance Status"
        Me.ComplianceStatusDisplay.Visible = False
        '
        'AirsNumberDisplay
        '
        Me.AirsNumberDisplay.AutoSize = True
        Me.AirsNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirsNumberDisplay.Location = New System.Drawing.Point(12, 41)
        Me.AirsNumberDisplay.Name = "AirsNumberDisplay"
        Me.AirsNumberDisplay.Size = New System.Drawing.Size(77, 17)
        Me.AirsNumberDisplay.TabIndex = 12
        Me.AirsNumberDisplay.Text = "000-00000"
        '
        'EnforcementIdDisplay
        '
        Me.EnforcementIdDisplay.AutoSize = True
        Me.EnforcementIdDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnforcementIdDisplay.Location = New System.Drawing.Point(12, 12)
        Me.EnforcementIdDisplay.Name = "EnforcementIdDisplay"
        Me.EnforcementIdDisplay.Size = New System.Drawing.Size(152, 17)
        Me.EnforcementIdDisplay.TabIndex = 12
        Me.EnforcementIdDisplay.Text = "New enforcement case"
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.OldLace
        Me.Label4.ForeColor = System.Drawing.Color.DarkRed
        Me.Label4.Location = New System.Drawing.Point(277, 303)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(5)
        Me.Label4.Size = New System.Drawing.Size(418, 37)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Message Placeholder" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2"
        Me.Label4.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Location = New System.Drawing.Point(14, 35)
        Me.Panel1.MinimumSize = New System.Drawing.Size(242, 172)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(242, 251)
        Me.Panel1.TabIndex = 17
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(83, 150)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(164, 150)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Add this file"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(6, 124)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(233, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Visible = False
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 108)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(106, 13)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Description (optional)"
        Me.Label12.Visible = False
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.ForestGreen
        Me.Label22.Location = New System.Drawing.Point(3, 76)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(145, 17)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "FileName placeholder"
        Me.Label22.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(164, 29)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "Select file"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(3, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(49, 13)
        Me.Label27.TabIndex = 3
        Me.Label27.Text = "Add new"
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(58, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(181, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Controls.Add(Me.ComboBox2)
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Button5)
        Me.Panel2.Controls.Add(Me.TextBox2)
        Me.Panel2.Controls.Add(Me.Button6)
        Me.Panel2.Controls.Add(Me.Label56)
        Me.Panel2.Location = New System.Drawing.Point(276, 214)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(572, 73)
        Me.Panel2.TabIndex = 16
        '
        'Label33
        '
        Me.Label33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(0, 36)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(106, 13)
        Me.Label33.TabIndex = 6
        Me.Label33.Text = "Description (optional)"
        Me.Label33.Visible = False
        '
        'ComboBox2
        '
        Me.ComboBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Enabled = False
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(319, 52)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(141, 21)
        Me.ComboBox2.TabIndex = 6
        Me.ComboBox2.Visible = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Enabled = False
        Me.Button4.Location = New System.Drawing.Point(497, 7)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "Delete"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.Enabled = False
        Me.Button5.Location = New System.Drawing.Point(416, 7)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Download"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Location = New System.Drawing.Point(0, 52)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(313, 20)
        Me.TextBox2.TabIndex = 5
        Me.TextBox2.Visible = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.AutoSize = True
        Me.Button6.Enabled = False
        Me.Button6.Location = New System.Drawing.Point(466, 50)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(106, 23)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "Update description"
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'Label56
        '
        Me.Label56.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.ForestGreen
        Me.Label56.Location = New System.Drawing.Point(0, 10)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(145, 17)
        Me.Label56.TabIndex = 14
        Me.Label56.Text = "FileName placeholder"
        Me.Label56.Visible = False
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(276, 19)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(98, 13)
        Me.Label57.TabIndex = 5
        Me.Label57.Text = "Current Documents"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Enabled = False
        Me.DataGridView1.Location = New System.Drawing.Point(276, 35)
        Me.DataGridView1.MinimumSize = New System.Drawing.Size(300, 55)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(572, 173)
        Me.DataGridView1.StandardTab = True
        Me.DataGridView1.TabIndex = 4
        '
        'ProgramsListView
        '
        Me.ProgramsListView.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ProgramsListView.AutoArrange = False
        Me.ProgramsListView.CheckBoxes = True
        Me.ProgramsListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProgramsListView.HotTracking = True
        Me.ProgramsListView.HoverSelection = True
        Me.ProgramsListView.Location = New System.Drawing.Point(0, 37)
        Me.ProgramsListView.Name = "ProgramsListView"
        Me.ProgramsListView.Size = New System.Drawing.Size(309, 356)
        Me.ProgramsListView.TabIndex = 371
        Me.ProgramsListView.UseCompatibleStateImageBehavior = False
        Me.ProgramsListView.View = System.Windows.Forms.View.List
        '
        'PollutantsProgramSplitContainer
        '
        Me.PollutantsProgramSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PollutantsProgramSplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.PollutantsProgramSplitContainer.Name = "PollutantsProgramSplitContainer"
        '
        'PollutantsProgramSplitContainer.Panel1
        '
        Me.PollutantsProgramSplitContainer.Panel1.Controls.Add(Me.PollutantsListView)
        Me.PollutantsProgramSplitContainer.Panel1.Controls.Add(Me.Panel3)
        '
        'PollutantsProgramSplitContainer.Panel2
        '
        Me.PollutantsProgramSplitContainer.Panel2.Controls.Add(Me.ProgramsListView)
        Me.PollutantsProgramSplitContainer.Panel2.Controls.Add(Me.Panel4)
        Me.PollutantsProgramSplitContainer.Size = New System.Drawing.Size(621, 393)
        Me.PollutantsProgramSplitContainer.SplitterDistance = 308
        Me.PollutantsProgramSplitContainer.TabIndex = 374
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(308, 37)
        Me.Panel3.TabIndex = 372
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(210, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Pollutants associated with this enforcement"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(309, 37)
        Me.Panel4.TabIndex = 373
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(222, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Air programs associated with this enforcement"
        '
        'SscpEnforcement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 601)
        Me.Controls.Add(Me.EnforcementTabs)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MinimumSize = New System.Drawing.Size(639, 579)
        Me.Name = "SscpEnforcement"
        Me.Text = "New Enforcement Case"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.EnforcementTabs.ResumeLayout(False)
        Me.InfoTabPage.ResumeLayout(False)
        Me.InfoTabPage.PerformLayout()
        Me.EAGroupBox.ResumeLayout(False)
        Me.EAGroupBox.PerformLayout()
        Me.ViolationTypeGroupbox.ResumeLayout(False)
        Me.ViolationTypeGroupbox.PerformLayout()
        Me.PollutantsTabPage.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.LonTabPage.ResumeLayout(False)
        Me.LonTabPage.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.NovTabPage.ResumeLayout(False)
        Me.NovTabPage.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.COTabPage.ResumeLayout(False)
        Me.COTabPage.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.CoNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StipulatedPenaltiesGroupBox.ResumeLayout(False)
        Me.StipulatedPenaltiesGroupBox.PerformLayout()
        CType(Me.StipulatedPenalties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StipulatedPenaltyControls.ResumeLayout(False)
        Me.StipulatedPenaltyControls.PerformLayout()
        Me.AOTabPage.ResumeLayout(False)
        Me.AOTabPage.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.DocumentsTabPage.ResumeLayout(False)
        Me.DocumentsTabPage.PerformLayout()
        Me.pnlDocument.ResumeLayout(False)
        Me.pnlDocument.PerformLayout()
        CType(Me.DocumentList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AuditHistoryTabPage.ResumeLayout(False)
        CType(Me.AuditHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.EpaValuesTabPage.ResumeLayout(False)
        Me.EpaValuesTabPage.PerformLayout()
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PollutantsProgramSplitContainer.Panel1.ResumeLayout(False)
        Me.PollutantsProgramSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.PollutantsProgramSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PollutantsProgramSplitContainer.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mmiClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteEnforcementMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnforcementTabs As System.Windows.Forms.TabControl
    Friend WithEvents InfoTabPage As System.Windows.Forms.TabPage
    Friend WithEvents LinkToEvent As System.Windows.Forms.Button
    Friend WithEvents ResolvedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DiscoveryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents AOCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DiscoveryDateLabel As System.Windows.Forms.Label
    Friend WithEvents COCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents NovCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LonCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SubmitToUC As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents SubmitToEpa As System.Windows.Forms.Button
    Friend WithEvents GeneralComments As System.Windows.Forms.TextBox
    Friend WithEvents StaffResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PollutantsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents PollutantsListView As System.Windows.Forms.ListView
    Friend WithEvents EditAirProgramPollutantsButton As System.Windows.Forms.Button
    Friend WithEvents lblPollutants As System.Windows.Forms.Label
    Friend WithEvents LonTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LonResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents LonToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents LonComments As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LonSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents NovTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents NfaToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents NfaToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents NovToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents NovToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents NfaSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents NovResponseReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents NovSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents NovComments As System.Windows.Forms.TextBox
    Friend WithEvents COTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents COToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents COToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents ClearStipulatedPenaltyFormButton As System.Windows.Forms.Button
    Friend WithEvents StipulatedPenalties As System.Windows.Forms.DataGridView
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents COResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents COReceivedfromCompany As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents COExecuted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents COReceivedFromDirector As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents COProposed As System.Windows.Forms.DateTimePicker
    Friend WithEvents COComments As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents COPenaltyAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents COPenaltyComments As System.Windows.Forms.TextBox
    Friend WithEvents SaveNewStipulatedPenaltyButton As System.Windows.Forms.Button
    Friend WithEvents StipulatedPenaltyComments As System.Windows.Forms.TextBox
    Friend WithEvents StipulatedPenaltyAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents AOTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents AOResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents AOAppealed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents AOExecuted As System.Windows.Forms.DateTimePicker
    Friend WithEvents AOComments As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents LastEditedDateDisplay As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents AuditHistoryTabPage As System.Windows.Forms.TabPage
    Friend WithEvents mmiShowAuditHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AuditHistory As System.Windows.Forms.DataGridView
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents ExportAuditHistory As System.Windows.Forms.Button
    Friend WithEvents RefreshAuditHistory As System.Windows.Forms.Button
    Friend WithEvents UpdateStipulatedPenaltyButton As System.Windows.Forms.Button
    Friend WithEvents StipulatedPenaltiesGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DeleteStipulatedPenaltyButton As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DocumentsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents lblCurrentFiles As System.Windows.Forms.Label
    Friend WithEvents DocumentList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Friend WithEvents lblDocumentDescription As System.Windows.Forms.Label
    Friend WithEvents btnDocumentDownload As System.Windows.Forms.Button
    Friend WithEvents txtDocumentDescription As System.Windows.Forms.TextBox
    Friend WithEvents DocumentUpdateButton As System.Windows.Forms.Button
    Friend WithEvents lblDocumentName As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CoNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents ViolationTypeGroupbox As System.Windows.Forms.GroupBox
    Friend WithEvents ViolationTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents ViolationTypeNonFrv As System.Windows.Forms.RadioButton
    Friend WithEvents ViolationTypeHpv As System.Windows.Forms.RadioButton
    Friend WithEvents ViolationTypeFrv As System.Windows.Forms.RadioButton
    Friend WithEvents ViolationTypeNone As System.Windows.Forms.RadioButton
    Friend WithEvents ClearLinkedEvent As System.Windows.Forms.Button
    Friend WithEvents FacilityNameDisplay As Label
    Friend WithEvents AirsNumberDisplay As Label
    Friend WithEvents EnforcementIdDisplay As Label
    Friend WithEvents EpaValuesTabPage As TabPage
    Friend WithEvents mmiShowEpaActionNumbersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label35 As Label
    Friend WithEvents AfsCoResolvedActionNumber As TextBox
    Friend WithEvents AfsCoExecutedActionNumber As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents AfsStipulatedPenalitiesActionNumbers As TextBox
    Friend WithEvents AfsCoProposedActionNumber As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents AfsAoToAgActionNumber As TextBox
    Friend WithEvents AfsNovActionNumber As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents AfsKeyActionNumber As TextBox
    Friend WithEvents AfsAoCivilCourtActionNumber As TextBox
    Friend WithEvents AfsNfaActionNumber As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents AfsAoResolvedActionNumber As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents DayZeroDisplay As Label
    Friend WithEvents EAGroupBox As GroupBox
    Friend WithEvents ComplianceStatusDisplay As Label
    Friend WithEvents FacilityNotApprovedDisplay As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents StipulatedPenaltyControls As Panel
    Friend WithEvents ResolvedCheckBox As CheckBox
    Friend WithEvents LinkedEventDisplay As LinkLabel
    Friend WithEvents PollutantsProgramSplitContainer As SplitContainer
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents ProgramsListView As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label6 As Label
End Class
