<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewSscpEnforcementAudit
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewSscpEnforcementAudit))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mmiFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiSave = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mmiClose = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiTools = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiShowAuditHistory = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mmiDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiOnlineHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.TCEnforcement = New System.Windows.Forms.TabControl
        Me.TPGeneralInfo = New System.Windows.Forms.TabPage
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.txtSubmitToUC = New System.Windows.Forms.TextBox
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtAFSAOResolvedActionNumber = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.txtAFSNOVResolvedNumber = New System.Windows.Forms.TextBox
        Me.txtAFSCivilCourtActionNumber = New System.Windows.Forms.TextBox
        Me.txtAFSKeyActionNumber = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.txtAFSNOVActionNumber = New System.Windows.Forms.TextBox
        Me.txtAFSAOToAGActionNumber = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtAFSCOProposedActionNumber = New System.Windows.Forms.TextBox
        Me.txtStipulatedPenalitiesActionNumber = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.txtAFSCOExecutedActionNumber = New System.Windows.Forms.TextBox
        Me.txtAFSCOResolvedActionNumber = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.btnLinkEnforcement = New System.Windows.Forms.Button
        Me.btn45DayZero = New System.Windows.Forms.Button
        Me.DTPEnforcementResolved = New System.Windows.Forms.DateTimePicker
        Me.chbHPV = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboHPVType = New System.Windows.Forms.ComboBox
        Me.DTPDiscoveryDate = New System.Windows.Forms.DateTimePicker
        Me.chbAO = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.chbCO = New System.Windows.Forms.CheckBox
        Me.DTPDayZero = New System.Windows.Forms.DateTimePicker
        Me.chbNOV = New System.Windows.Forms.CheckBox
        Me.lblDayZero = New System.Windows.Forms.Label
        Me.chbLON = New System.Windows.Forms.CheckBox
        Me.lblDiscoveryEvent = New System.Windows.Forms.Label
        Me.btnOpenEvent = New System.Windows.Forms.Button
        Me.txtDiscoveryEventNumber = New System.Windows.Forms.TextBox
        Me.btnSubmitToUC = New System.Windows.Forms.Button
        Me.Label34 = New System.Windows.Forms.Label
        Me.btnSubmitEnforcementToEPA = New System.Windows.Forms.Button
        Me.txtGeneralComments = New System.Windows.Forms.TextBox
        Me.cboStaffResponsible = New System.Windows.Forms.ComboBox
        Me.btnManuallyEnterAFS = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.TPPollutants = New System.Windows.Forms.TabPage
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.lvPollutants = New System.Windows.Forms.ListView
        Me.btnEditAirProgramPollutants = New System.Windows.Forms.Button
        Me.cboPollutantStatus = New System.Windows.Forms.ComboBox
        Me.lblPollutantStatus = New System.Windows.Forms.Label
        Me.lblPollutants = New System.Windows.Forms.Label
        Me.TPLON = New System.Windows.Forms.TabPage
        Me.txtLONComments = New System.Windows.Forms.TextBox
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.DTPLONToUC = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.DTPLONResolved = New System.Windows.Forms.DateTimePicker
        Me.DTPLONSent = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.TPNOV = New System.Windows.Forms.TabPage
        Me.txtNOVComments = New System.Windows.Forms.TextBox
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.lblNFAdownload = New System.Windows.Forms.Label
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.btnDownloadNFA = New System.Windows.Forms.Button
        Me.btnUploadNFA = New System.Windows.Forms.Button
        Me.DTPNOVToUC = New System.Windows.Forms.DateTimePicker
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.DTPNOVsent = New System.Windows.Forms.DateTimePicker
        Me.DTPNFAToPM = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.DTPNOVResponseReceived = New System.Windows.Forms.DateTimePicker
        Me.DTPNFAToUC = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.DTPNFALetterSent = New System.Windows.Forms.DateTimePicker
        Me.DTPNOVToPM = New System.Windows.Forms.DateTimePicker
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.TPCO = New System.Windows.Forms.TabPage
        Me.txtCOComments = New System.Windows.Forms.TextBox
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.lblCODownload = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.DTPCOToUC = New System.Windows.Forms.DateTimePicker
        Me.btnDownloadCO = New System.Windows.Forms.Button
        Me.txtPenaltyComments = New System.Windows.Forms.TextBox
        Me.btnUploadCO = New System.Windows.Forms.Button
        Me.txtStipulatedKey = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtCONumber = New System.Windows.Forms.TextBox
        Me.txtCOPenaltyAmount = New System.Windows.Forms.TextBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.DTPCOProposed = New System.Windows.Forms.DateTimePicker
        Me.DTPCOToPM = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.DTPCOReceivedfromDirector = New System.Windows.Forms.DateTimePicker
        Me.Label15 = New System.Windows.Forms.Label
        Me.DTPCOExecuted = New System.Windows.Forms.DateTimePicker
        Me.Label16 = New System.Windows.Forms.Label
        Me.DTPCOReceivedfromCompany = New System.Windows.Forms.DateTimePicker
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.DTPCOResolved = New System.Windows.Forms.DateTimePicker
        Me.StipulatedPenalties = New System.Windows.Forms.GroupBox
        Me.txtStipulatedPenalty = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.dgvStipulatedPenalties = New System.Windows.Forms.DataGridView
        Me.UpdateStipulatedPenaltyButton = New System.Windows.Forms.Button
        Me.txtStipulatedComments = New System.Windows.Forms.TextBox
        Me.ClearStipulatedPenaltyFormButton = New System.Windows.Forms.Button
        Me.Label31 = New System.Windows.Forms.Label
        Me.DeletePenaltyButton = New System.Windows.Forms.Button
        Me.SaveStipulatedPenaltyButton = New System.Windows.Forms.Button
        Me.TPAO = New System.Windows.Forms.TabPage
        Me.txtAOComments = New System.Windows.Forms.TextBox
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.lblDownloadAO = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.btnDownloadAO = New System.Windows.Forms.Button
        Me.btnUploadAO = New System.Windows.Forms.Button
        Me.DTPAOExecuted = New System.Windows.Forms.DateTimePicker
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.DTPAOResolved = New System.Windows.Forms.DateTimePicker
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.DTPAOAppealed = New System.Windows.Forms.DateTimePicker
        Me.TPAuditHistory = New System.Windows.Forms.TabPage
        Me.dgvAuditHistory = New System.Windows.Forms.DataGridView
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnREfreshAudit = New System.Windows.Forms.Button
        Me.btnHideAudit = New System.Windows.Forms.Button
        Me.btnExportAuditToExcel = New System.Windows.Forms.Button
        Me.tabDocuments = New System.Windows.Forms.TabPage
        Me.lblMessage = New System.Windows.Forms.Label
        Me.pnlAddNew = New System.Windows.Forms.Panel
        Me.btnNewFileCancel = New System.Windows.Forms.Button
        Me.btnNewFileUpload = New System.Windows.Forms.Button
        Me.txtNewDescription = New System.Windows.Forms.TextBox
        Me.lblNewDescription = New System.Windows.Forms.Label
        Me.lblNewFileName = New System.Windows.Forms.Label
        Me.btnChooseNewFile = New System.Windows.Forms.Button
        Me.lblDocumentTypes = New System.Windows.Forms.Label
        Me.ddlNewDocumentType = New System.Windows.Forms.ComboBox
        Me.pnlUpdate = New System.Windows.Forms.Panel
        Me.lblUpdateDescription = New System.Windows.Forms.Label
        Me.ddlUpdateDocumentType = New System.Windows.Forms.ComboBox
        Me.btnDeleteFile = New System.Windows.Forms.Button
        Me.btnDownloadFile = New System.Windows.Forms.Button
        Me.txtUpdateDescription = New System.Windows.Forms.TextBox
        Me.btnUpdateFileDescription = New System.Windows.Forms.Button
        Me.lblSelectedFileName = New System.Windows.Forms.Label
        Me.lblCurrentFiles = New System.Windows.Forms.Label
        Me.dgvFileList = New System.Windows.Forms.DataGridView
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label55 = New System.Windows.Forms.Label
        Me.txtCounty = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chbAPCI = New System.Windows.Forms.CheckBox
        Me.chbAPCF = New System.Windows.Forms.CheckBox
        Me.chbAPCA = New System.Windows.Forms.CheckBox
        Me.chbAPCV = New System.Windows.Forms.CheckBox
        Me.chbAPCM = New System.Windows.Forms.CheckBox
        Me.chbAPC9 = New System.Windows.Forms.CheckBox
        Me.chbAPC8 = New System.Windows.Forms.CheckBox
        Me.chbAPC7 = New System.Windows.Forms.CheckBox
        Me.chbAPC6 = New System.Windows.Forms.CheckBox
        Me.chbAPC4 = New System.Windows.Forms.CheckBox
        Me.chbAPC3 = New System.Windows.Forms.CheckBox
        Me.chbAPC1 = New System.Windows.Forms.CheckBox
        Me.chbAPC0 = New System.Windows.Forms.CheckBox
        Me.Label54 = New System.Windows.Forms.Label
        Me.txtClassification = New System.Windows.Forms.TextBox
        Me.DTPLastSave = New System.Windows.Forms.DateTimePicker
        Me.lblLastEdited = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.txtFacilityAddress = New System.Windows.Forms.TextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCEnforcement.SuspendLayout()
        Me.TPGeneralInfo.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TPPollutants.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TPLON.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.TPNOV.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.TPCO.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.StipulatedPenalties.SuspendLayout()
        CType(Me.dgvStipulatedPenalties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPAO.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.TPAuditHistory.SuspendLayout()
        CType(Me.dgvAuditHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.tabDocuments.SuspendLayout()
        Me.pnlAddNew.SuspendLayout()
        Me.pnlUpdate.SuspendLayout()
        CType(Me.dgvFileList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(892, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(51, 22)
        Me.tsbSave.Text = "Save"
        Me.tsbSave.ToolTipText = "Save (Ctrl + S)"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiFile, Me.mmiTools, Me.mmiHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(892, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mmiFile
        '
        Me.mmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiSave, Me.ToolStripSeparator2, Me.mmiClose})
        Me.mmiFile.Name = "mmiFile"
        Me.mmiFile.Size = New System.Drawing.Size(37, 20)
        Me.mmiFile.Text = "&File"
        '
        'mmiSave
        '
        Me.mmiSave.Name = "mmiSave"
        Me.mmiSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mmiSave.Size = New System.Drawing.Size(148, 22)
        Me.mmiSave.Text = "&Save"
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
        Me.mmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiShowAuditHistory, Me.ToolStripSeparator1, Me.mmiDelete})
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(216, 6)
        '
        'mmiDelete
        '
        Me.mmiDelete.Name = "mmiDelete"
        Me.mmiDelete.Size = New System.Drawing.Size(219, 22)
        Me.mmiDelete.Text = "Delete this enforcement"
        '
        'mmiHelp
        '
        Me.mmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiOnlineHelp})
        Me.mmiHelp.Name = "mmiHelp"
        Me.mmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Name = "mmiOnlineHelp"
        Me.mmiOnlineHelp.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.mmiOnlineHelp.Size = New System.Drawing.Size(156, 22)
        Me.mmiOnlineHelp.Text = "Online &Help"
        '
        'TCEnforcement
        '
        Me.TCEnforcement.Controls.Add(Me.TPGeneralInfo)
        Me.TCEnforcement.Controls.Add(Me.TPPollutants)
        Me.TCEnforcement.Controls.Add(Me.TPLON)
        Me.TCEnforcement.Controls.Add(Me.TPNOV)
        Me.TCEnforcement.Controls.Add(Me.TPCO)
        Me.TCEnforcement.Controls.Add(Me.TPAO)
        Me.TCEnforcement.Controls.Add(Me.TPAuditHistory)
        Me.TCEnforcement.Controls.Add(Me.tabDocuments)
        Me.TCEnforcement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCEnforcement.Location = New System.Drawing.Point(0, 171)
        Me.TCEnforcement.Name = "TCEnforcement"
        Me.TCEnforcement.SelectedIndex = 0
        Me.TCEnforcement.Size = New System.Drawing.Size(892, 495)
        Me.TCEnforcement.TabIndex = 267
        '
        'TPGeneralInfo
        '
        Me.TPGeneralInfo.AutoScroll = True
        Me.TPGeneralInfo.Controls.Add(Me.Panel6)
        Me.TPGeneralInfo.Location = New System.Drawing.Point(4, 22)
        Me.TPGeneralInfo.Name = "TPGeneralInfo"
        Me.TPGeneralInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.TPGeneralInfo.Size = New System.Drawing.Size(884, 469)
        Me.TPGeneralInfo.TabIndex = 0
        Me.TPGeneralInfo.Text = "General Information"
        Me.TPGeneralInfo.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.AutoScroll = True
        Me.Panel6.Controls.Add(Me.txtSubmitToUC)
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Controls.Add(Me.btnLinkEnforcement)
        Me.Panel6.Controls.Add(Me.btn45DayZero)
        Me.Panel6.Controls.Add(Me.DTPEnforcementResolved)
        Me.Panel6.Controls.Add(Me.chbHPV)
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Controls.Add(Me.cboHPVType)
        Me.Panel6.Controls.Add(Me.DTPDiscoveryDate)
        Me.Panel6.Controls.Add(Me.chbAO)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Controls.Add(Me.chbCO)
        Me.Panel6.Controls.Add(Me.DTPDayZero)
        Me.Panel6.Controls.Add(Me.chbNOV)
        Me.Panel6.Controls.Add(Me.lblDayZero)
        Me.Panel6.Controls.Add(Me.chbLON)
        Me.Panel6.Controls.Add(Me.lblDiscoveryEvent)
        Me.Panel6.Controls.Add(Me.btnOpenEvent)
        Me.Panel6.Controls.Add(Me.txtDiscoveryEventNumber)
        Me.Panel6.Controls.Add(Me.btnSubmitToUC)
        Me.Panel6.Controls.Add(Me.Label34)
        Me.Panel6.Controls.Add(Me.btnSubmitEnforcementToEPA)
        Me.Panel6.Controls.Add(Me.txtGeneralComments)
        Me.Panel6.Controls.Add(Me.cboStaffResponsible)
        Me.Panel6.Controls.Add(Me.btnManuallyEnterAFS)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(878, 463)
        Me.Panel6.TabIndex = 369
        '
        'txtSubmitToUC
        '
        Me.txtSubmitToUC.Location = New System.Drawing.Point(161, 240)
        Me.txtSubmitToUC.Name = "txtSubmitToUC"
        Me.txtSubmitToUC.Size = New System.Drawing.Size(100, 20)
        Me.txtSubmitToUC.TabIndex = 370
        Me.txtSubmitToUC.Visible = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label40)
        Me.Panel7.Controls.Add(Me.Label10)
        Me.Panel7.Controls.Add(Me.txtAFSAOResolvedActionNumber)
        Me.Panel7.Controls.Add(Me.Label24)
        Me.Panel7.Controls.Add(Me.Label39)
        Me.Panel7.Controls.Add(Me.txtAFSNOVResolvedNumber)
        Me.Panel7.Controls.Add(Me.txtAFSCivilCourtActionNumber)
        Me.Panel7.Controls.Add(Me.txtAFSKeyActionNumber)
        Me.Panel7.Controls.Add(Me.Label38)
        Me.Panel7.Controls.Add(Me.txtAFSNOVActionNumber)
        Me.Panel7.Controls.Add(Me.txtAFSAOToAGActionNumber)
        Me.Panel7.Controls.Add(Me.Label11)
        Me.Panel7.Controls.Add(Me.Label37)
        Me.Panel7.Controls.Add(Me.txtAFSCOProposedActionNumber)
        Me.Panel7.Controls.Add(Me.txtStipulatedPenalitiesActionNumber)
        Me.Panel7.Controls.Add(Me.Label23)
        Me.Panel7.Controls.Add(Me.Label36)
        Me.Panel7.Controls.Add(Me.txtAFSCOExecutedActionNumber)
        Me.Panel7.Controls.Add(Me.txtAFSCOResolvedActionNumber)
        Me.Panel7.Controls.Add(Me.Label35)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 363)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(878, 100)
        Me.Panel7.TabIndex = 369
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(434, 61)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(80, 13)
        Me.Label40.TabIndex = 332
        Me.Label40.Text = "AO Resolved #"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(25, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 13)
        Me.Label10.TabIndex = 314
        Me.Label10.Text = "AFS Key Action Number"
        '
        'txtAFSAOResolvedActionNumber
        '
        Me.txtAFSAOResolvedActionNumber.Location = New System.Drawing.Point(514, 59)
        Me.txtAFSAOResolvedActionNumber.Name = "txtAFSAOResolvedActionNumber"
        Me.txtAFSAOResolvedActionNumber.ReadOnly = True
        Me.txtAFSAOResolvedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSAOResolvedActionNumber.TabIndex = 331
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(409, 13)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(101, 13)
        Me.Label24.TabIndex = 320
        Me.Label24.Text = "NFA Action Number"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(17, 61)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(127, 13)
        Me.Label39.TabIndex = 330
        Me.Label39.Text = "Civil Court Action Number"
        '
        'txtAFSNOVResolvedNumber
        '
        Me.txtAFSNOVResolvedNumber.Location = New System.Drawing.Point(513, 11)
        Me.txtAFSNOVResolvedNumber.Name = "txtAFSNOVResolvedNumber"
        Me.txtAFSNOVResolvedNumber.ReadOnly = True
        Me.txtAFSNOVResolvedNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSNOVResolvedNumber.TabIndex = 319
        '
        'txtAFSCivilCourtActionNumber
        '
        Me.txtAFSCivilCourtActionNumber.Location = New System.Drawing.Point(153, 59)
        Me.txtAFSCivilCourtActionNumber.Name = "txtAFSCivilCourtActionNumber"
        Me.txtAFSCivilCourtActionNumber.ReadOnly = True
        Me.txtAFSCivilCourtActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSCivilCourtActionNumber.TabIndex = 329
        '
        'txtAFSKeyActionNumber
        '
        Me.txtAFSKeyActionNumber.Location = New System.Drawing.Point(153, 11)
        Me.txtAFSKeyActionNumber.Name = "txtAFSKeyActionNumber"
        Me.txtAFSKeyActionNumber.ReadOnly = True
        Me.txtAFSKeyActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSKeyActionNumber.TabIndex = 313
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(201, 61)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(151, 13)
        Me.Label38.TabIndex = 328
        Me.Label38.Text = "Referred to AG Action Number"
        '
        'txtAFSNOVActionNumber
        '
        Me.txtAFSNOVActionNumber.Location = New System.Drawing.Point(361, 11)
        Me.txtAFSNOVActionNumber.Name = "txtAFSNOVActionNumber"
        Me.txtAFSNOVActionNumber.ReadOnly = True
        Me.txtAFSNOVActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSNOVActionNumber.TabIndex = 315
        '
        'txtAFSAOToAGActionNumber
        '
        Me.txtAFSAOToAGActionNumber.Location = New System.Drawing.Point(361, 59)
        Me.txtAFSAOToAGActionNumber.Name = "txtAFSAOToAGActionNumber"
        Me.txtAFSAOToAGActionNumber.ReadOnly = True
        Me.txtAFSAOToAGActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSAOToAGActionNumber.TabIndex = 327
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(252, 13)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 13)
        Me.Label11.TabIndex = 316
        Me.Label11.Text = "NOV Action Number"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(409, 37)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(184, 13)
        Me.Label37.TabIndex = 326
        Me.Label37.Text = "Stipulated Penalties Action Number(s)"
        '
        'txtAFSCOProposedActionNumber
        '
        Me.txtAFSCOProposedActionNumber.Location = New System.Drawing.Point(705, 11)
        Me.txtAFSCOProposedActionNumber.Name = "txtAFSCOProposedActionNumber"
        Me.txtAFSCOProposedActionNumber.ReadOnly = True
        Me.txtAFSCOProposedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSCOProposedActionNumber.TabIndex = 317
        '
        'txtStipulatedPenalitiesActionNumber
        '
        Me.txtStipulatedPenalitiesActionNumber.Location = New System.Drawing.Point(610, 35)
        Me.txtStipulatedPenalitiesActionNumber.Multiline = True
        Me.txtStipulatedPenalitiesActionNumber.Name = "txtStipulatedPenalitiesActionNumber"
        Me.txtStipulatedPenalitiesActionNumber.ReadOnly = True
        Me.txtStipulatedPenalitiesActionNumber.Size = New System.Drawing.Size(135, 44)
        Me.txtStipulatedPenalitiesActionNumber.TabIndex = 325
        Me.txtStipulatedPenalitiesActionNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(553, 13)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(143, 13)
        Me.Label23.TabIndex = 318
        Me.Label23.Text = "CO Proposed Action Number"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(212, 37)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(143, 13)
        Me.Label36.TabIndex = 324
        Me.Label36.Text = "CO Resolved Action Number"
        '
        'txtAFSCOExecutedActionNumber
        '
        Me.txtAFSCOExecutedActionNumber.Location = New System.Drawing.Point(153, 35)
        Me.txtAFSCOExecutedActionNumber.Name = "txtAFSCOExecutedActionNumber"
        Me.txtAFSCOExecutedActionNumber.ReadOnly = True
        Me.txtAFSCOExecutedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSCOExecutedActionNumber.TabIndex = 321
        '
        'txtAFSCOResolvedActionNumber
        '
        Me.txtAFSCOResolvedActionNumber.Location = New System.Drawing.Point(361, 35)
        Me.txtAFSCOResolvedActionNumber.Name = "txtAFSCOResolvedActionNumber"
        Me.txtAFSCOResolvedActionNumber.ReadOnly = True
        Me.txtAFSCOResolvedActionNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtAFSCOResolvedActionNumber.TabIndex = 323
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(1, 37)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(143, 13)
        Me.Label35.TabIndex = 322
        Me.Label35.Text = "CO Executed Action Number"
        '
        'btnLinkEnforcement
        '
        Me.btnLinkEnforcement.AutoSize = True
        Me.btnLinkEnforcement.Location = New System.Drawing.Point(9, 4)
        Me.btnLinkEnforcement.Name = "btnLinkEnforcement"
        Me.btnLinkEnforcement.Size = New System.Drawing.Size(80, 23)
        Me.btnLinkEnforcement.TabIndex = 337
        Me.btnLinkEnforcement.Text = "Link to Event"
        '
        'btn45DayZero
        '
        Me.btn45DayZero.AutoSize = True
        Me.btn45DayZero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btn45DayZero.Location = New System.Drawing.Point(731, 34)
        Me.btn45DayZero.Name = "btn45DayZero"
        Me.btn45DayZero.Size = New System.Drawing.Size(49, 23)
        Me.btn45DayZero.TabIndex = 368
        Me.btn45DayZero.Text = "45-day"
        Me.btn45DayZero.UseVisualStyleBackColor = True
        '
        'DTPEnforcementResolved
        '
        Me.DTPEnforcementResolved.Checked = False
        Me.DTPEnforcementResolved.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEnforcementResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPEnforcementResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEnforcementResolved.Location = New System.Drawing.Point(8, 209)
        Me.DTPEnforcementResolved.Name = "DTPEnforcementResolved"
        Me.DTPEnforcementResolved.ShowCheckBox = True
        Me.DTPEnforcementResolved.Size = New System.Drawing.Size(119, 22)
        Me.DTPEnforcementResolved.TabIndex = 0
        '
        'chbHPV
        '
        Me.chbHPV.Location = New System.Drawing.Point(6, 63)
        Me.chbHPV.Name = "chbHPV"
        Me.chbHPV.Size = New System.Drawing.Size(72, 16)
        Me.chbHPV.TabIndex = 367
        Me.chbHPV.Text = "HPV type"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(131, 214)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "UC Finalized"
        '
        'cboHPVType
        '
        Me.cboHPVType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboHPVType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboHPVType.Location = New System.Drawing.Point(16, 82)
        Me.cboHPVType.Name = "cboHPVType"
        Me.cboHPVType.Size = New System.Drawing.Size(747, 21)
        Me.cboHPVType.TabIndex = 366
        Me.cboHPVType.Visible = False
        '
        'DTPDiscoveryDate
        '
        Me.DTPDiscoveryDate.Checked = False
        Me.DTPDiscoveryDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDiscoveryDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDiscoveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDiscoveryDate.Location = New System.Drawing.Point(6, 34)
        Me.DTPDiscoveryDate.Name = "DTPDiscoveryDate"
        Me.DTPDiscoveryDate.ShowCheckBox = True
        Me.DTPDiscoveryDate.Size = New System.Drawing.Size(119, 22)
        Me.DTPDiscoveryDate.TabIndex = 2
        '
        'chbAO
        '
        Me.chbAO.AutoSize = True
        Me.chbAO.Location = New System.Drawing.Point(357, 37)
        Me.chbAO.Name = "chbAO"
        Me.chbAO.Size = New System.Drawing.Size(41, 17)
        Me.chbAO.TabIndex = 352
        Me.chbAO.Text = "AO"
        Me.chbAO.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(131, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Discovery Date"
        '
        'chbCO
        '
        Me.chbCO.AutoSize = True
        Me.chbCO.Location = New System.Drawing.Point(315, 37)
        Me.chbCO.Name = "chbCO"
        Me.chbCO.Size = New System.Drawing.Size(41, 17)
        Me.chbCO.TabIndex = 351
        Me.chbCO.Text = "CO"
        Me.chbCO.UseVisualStyleBackColor = True
        '
        'DTPDayZero
        '
        Me.DTPDayZero.Checked = False
        Me.DTPDayZero.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDayZero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDayZero.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDayZero.Location = New System.Drawing.Point(448, 34)
        Me.DTPDayZero.Name = "DTPDayZero"
        Me.DTPDayZero.ShowCheckBox = True
        Me.DTPDayZero.Size = New System.Drawing.Size(119, 22)
        Me.DTPDayZero.TabIndex = 4
        '
        'chbNOV
        '
        Me.chbNOV.AutoSize = True
        Me.chbNOV.Location = New System.Drawing.Point(264, 37)
        Me.chbNOV.Name = "chbNOV"
        Me.chbNOV.Size = New System.Drawing.Size(49, 17)
        Me.chbNOV.TabIndex = 350
        Me.chbNOV.Text = "NOV"
        Me.chbNOV.UseVisualStyleBackColor = True
        '
        'lblDayZero
        '
        Me.lblDayZero.AutoSize = True
        Me.lblDayZero.Location = New System.Drawing.Point(573, 39)
        Me.lblDayZero.Name = "lblDayZero"
        Me.lblDayZero.Size = New System.Drawing.Size(152, 13)
        Me.lblDayZero.TabIndex = 5
        Me.lblDayZero.Text = "Day Zero - AFS Required Date"
        '
        'chbLON
        '
        Me.chbLON.AutoSize = True
        Me.chbLON.Location = New System.Drawing.Point(217, 37)
        Me.chbLON.Name = "chbLON"
        Me.chbLON.Size = New System.Drawing.Size(48, 17)
        Me.chbLON.TabIndex = 349
        Me.chbLON.Text = "LON"
        Me.chbLON.UseVisualStyleBackColor = True
        '
        'lblDiscoveryEvent
        '
        Me.lblDiscoveryEvent.AutoSize = True
        Me.lblDiscoveryEvent.Location = New System.Drawing.Point(93, 9)
        Me.lblDiscoveryEvent.Name = "lblDiscoveryEvent"
        Me.lblDiscoveryEvent.Size = New System.Drawing.Size(88, 13)
        Me.lblDiscoveryEvent.TabIndex = 338
        Me.lblDiscoveryEvent.Text = "Discovery Event:"
        Me.lblDiscoveryEvent.Visible = False
        '
        'btnOpenEvent
        '
        Me.btnOpenEvent.AutoSize = True
        Me.btnOpenEvent.Location = New System.Drawing.Point(258, 4)
        Me.btnOpenEvent.Name = "btnOpenEvent"
        Me.btnOpenEvent.Size = New System.Drawing.Size(40, 23)
        Me.btnOpenEvent.TabIndex = 348
        Me.btnOpenEvent.Text = "View"
        Me.btnOpenEvent.Visible = False
        '
        'txtDiscoveryEventNumber
        '
        Me.txtDiscoveryEventNumber.Location = New System.Drawing.Point(187, 5)
        Me.txtDiscoveryEventNumber.Name = "txtDiscoveryEventNumber"
        Me.txtDiscoveryEventNumber.ReadOnly = True
        Me.txtDiscoveryEventNumber.Size = New System.Drawing.Size(64, 20)
        Me.txtDiscoveryEventNumber.TabIndex = 339
        Me.txtDiscoveryEventNumber.Visible = False
        '
        'btnSubmitToUC
        '
        Me.btnSubmitToUC.Location = New System.Drawing.Point(8, 237)
        Me.btnSubmitToUC.Name = "btnSubmitToUC"
        Me.btnSubmitToUC.Size = New System.Drawing.Size(147, 23)
        Me.btnSubmitToUC.TabIndex = 346
        Me.btnSubmitToUC.Text = "Submit Enforcement to UC"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(1, 105)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(96, 13)
        Me.Label34.TabIndex = 341
        Me.Label34.Text = "General Comments"
        '
        'btnSubmitEnforcementToEPA
        '
        Me.btnSubmitEnforcementToEPA.Location = New System.Drawing.Point(218, 209)
        Me.btnSubmitEnforcementToEPA.Name = "btnSubmitEnforcementToEPA"
        Me.btnSubmitEnforcementToEPA.Size = New System.Drawing.Size(183, 23)
        Me.btnSubmitEnforcementToEPA.TabIndex = 345
        Me.btnSubmitEnforcementToEPA.Text = "Submit Enforcement to EPA"
        '
        'txtGeneralComments
        '
        Me.txtGeneralComments.AcceptsReturn = True
        Me.txtGeneralComments.AcceptsTab = True
        Me.txtGeneralComments.Location = New System.Drawing.Point(16, 120)
        Me.txtGeneralComments.MaxLength = 4000
        Me.txtGeneralComments.Multiline = True
        Me.txtGeneralComments.Name = "txtGeneralComments"
        Me.txtGeneralComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGeneralComments.Size = New System.Drawing.Size(741, 80)
        Me.txtGeneralComments.TabIndex = 340
        '
        'cboStaffResponsible
        '
        Me.cboStaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaffResponsible.Enabled = False
        Me.cboStaffResponsible.Location = New System.Drawing.Point(410, 5)
        Me.cboStaffResponsible.Name = "cboStaffResponsible"
        Me.cboStaffResponsible.Size = New System.Drawing.Size(154, 21)
        Me.cboStaffResponsible.TabIndex = 343
        '
        'btnManuallyEnterAFS
        '
        Me.btnManuallyEnterAFS.Location = New System.Drawing.Point(415, 209)
        Me.btnManuallyEnterAFS.Name = "btnManuallyEnterAFS"
        Me.btnManuallyEnterAFS.Size = New System.Drawing.Size(184, 23)
        Me.btnManuallyEnterAFS.TabIndex = 347
        Me.btnManuallyEnterAFS.Text = "Manually Enter EPA Numbers"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(318, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 342
        Me.Label5.Text = "Staff Responsible"
        '
        'TPPollutants
        '
        Me.TPPollutants.Controls.Add(Me.Panel5)
        Me.TPPollutants.Location = New System.Drawing.Point(4, 22)
        Me.TPPollutants.Name = "TPPollutants"
        Me.TPPollutants.Size = New System.Drawing.Size(884, 469)
        Me.TPPollutants.TabIndex = 5
        Me.TPPollutants.Text = "Pollutants"
        Me.TPPollutants.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.lvPollutants)
        Me.Panel5.Controls.Add(Me.btnEditAirProgramPollutants)
        Me.Panel5.Controls.Add(Me.cboPollutantStatus)
        Me.Panel5.Controls.Add(Me.lblPollutantStatus)
        Me.Panel5.Controls.Add(Me.lblPollutants)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(884, 469)
        Me.Panel5.TabIndex = 373
        '
        'lvPollutants
        '
        Me.lvPollutants.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvPollutants.AutoArrange = False
        Me.lvPollutants.Dock = System.Windows.Forms.DockStyle.Left
        Me.lvPollutants.Location = New System.Drawing.Point(0, 0)
        Me.lvPollutants.Name = "lvPollutants"
        Me.lvPollutants.Size = New System.Drawing.Size(607, 469)
        Me.lvPollutants.TabIndex = 371
        Me.lvPollutants.UseCompatibleStateImageBehavior = False
        '
        'btnEditAirProgramPollutants
        '
        Me.btnEditAirProgramPollutants.Location = New System.Drawing.Point(666, 165)
        Me.btnEditAirProgramPollutants.Name = "btnEditAirProgramPollutants"
        Me.btnEditAirProgramPollutants.Size = New System.Drawing.Size(140, 23)
        Me.btnEditAirProgramPollutants.TabIndex = 353
        Me.btnEditAirProgramPollutants.Text = "Air Program Pollutants"
        '
        'cboPollutantStatus
        '
        Me.cboPollutantStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollutantStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollutantStatus.Location = New System.Drawing.Point(636, 23)
        Me.cboPollutantStatus.Name = "cboPollutantStatus"
        Me.cboPollutantStatus.Size = New System.Drawing.Size(216, 21)
        Me.cboPollutantStatus.TabIndex = 356
        '
        'lblPollutantStatus
        '
        Me.lblPollutantStatus.AutoSize = True
        Me.lblPollutantStatus.Location = New System.Drawing.Point(620, 7)
        Me.lblPollutantStatus.Name = "lblPollutantStatus"
        Me.lblPollutantStatus.Size = New System.Drawing.Size(160, 13)
        Me.lblPollutantStatus.TabIndex = 355
        Me.lblPollutantStatus.Text = "Edit Pollutant Compliance Status"
        '
        'lblPollutants
        '
        Me.lblPollutants.AutoSize = True
        Me.lblPollutants.Location = New System.Drawing.Point(630, 133)
        Me.lblPollutants.Name = "lblPollutants"
        Me.lblPollutants.Size = New System.Drawing.Size(228, 26)
        Me.lblPollutants.TabIndex = 354
        Me.lblPollutants.Text = "Add New Pollutants To List" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " (This is only if you need to add new Pollutants)"
        '
        'TPLON
        '
        Me.TPLON.AutoScroll = True
        Me.TPLON.Controls.Add(Me.txtLONComments)
        Me.TPLON.Controls.Add(Me.Panel8)
        Me.TPLON.Location = New System.Drawing.Point(4, 22)
        Me.TPLON.Name = "TPLON"
        Me.TPLON.Padding = New System.Windows.Forms.Padding(3)
        Me.TPLON.Size = New System.Drawing.Size(884, 469)
        Me.TPLON.TabIndex = 1
        Me.TPLON.Text = "Letter of Non Compliance"
        Me.TPLON.UseVisualStyleBackColor = True
        '
        'txtLONComments
        '
        Me.txtLONComments.AcceptsReturn = True
        Me.txtLONComments.AcceptsTab = True
        Me.txtLONComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLONComments.Location = New System.Drawing.Point(3, 96)
        Me.txtLONComments.MaxLength = 4000
        Me.txtLONComments.Multiline = True
        Me.txtLONComments.Name = "txtLONComments"
        Me.txtLONComments.Size = New System.Drawing.Size(878, 370)
        Me.txtLONComments.TabIndex = 277
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.DTPLONToUC)
        Me.Panel8.Controls.Add(Me.Label3)
        Me.Panel8.Controls.Add(Me.DTPLONResolved)
        Me.Panel8.Controls.Add(Me.DTPLONSent)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Controls.Add(Me.Label43)
        Me.Panel8.Controls.Add(Me.Label19)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(3, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(878, 93)
        Me.Panel8.TabIndex = 283
        '
        'DTPLONToUC
        '
        Me.DTPLONToUC.Checked = False
        Me.DTPLONToUC.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLONToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPLONToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLONToUC.Location = New System.Drawing.Point(11, 9)
        Me.DTPLONToUC.Name = "DTPLONToUC"
        Me.DTPLONToUC.ShowCheckBox = True
        Me.DTPLONToUC.Size = New System.Drawing.Size(119, 22)
        Me.DTPLONToUC.TabIndex = 279
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(425, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 282
        Me.Label3.Text = "Date LON Resolved"
        '
        'DTPLONResolved
        '
        Me.DTPLONResolved.Checked = False
        Me.DTPLONResolved.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLONResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPLONResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLONResolved.Location = New System.Drawing.Point(300, 36)
        Me.DTPLONResolved.Name = "DTPLONResolved"
        Me.DTPLONResolved.ShowCheckBox = True
        Me.DTPLONResolved.Size = New System.Drawing.Size(119, 22)
        Me.DTPLONResolved.TabIndex = 281
        '
        'DTPLONSent
        '
        Me.DTPLONSent.Checked = False
        Me.DTPLONSent.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLONSent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPLONSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLONSent.Location = New System.Drawing.Point(11, 37)
        Me.DTPLONSent.Name = "DTPLONSent"
        Me.DTPLONSent.ShowCheckBox = True
        Me.DTPLONSent.Size = New System.Drawing.Size(119, 22)
        Me.DTPLONSent.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(136, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Date LON Sent"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(136, 13)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(60, 13)
        Me.Label43.TabIndex = 280
        Me.Label43.Text = "Date to UC"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 71)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(81, 13)
        Me.Label19.TabIndex = 278
        Me.Label19.Text = "LON Comments"
        '
        'TPNOV
        '
        Me.TPNOV.AutoScroll = True
        Me.TPNOV.Controls.Add(Me.txtNOVComments)
        Me.TPNOV.Controls.Add(Me.Panel9)
        Me.TPNOV.Location = New System.Drawing.Point(4, 22)
        Me.TPNOV.Name = "TPNOV"
        Me.TPNOV.Size = New System.Drawing.Size(884, 469)
        Me.TPNOV.TabIndex = 2
        Me.TPNOV.Text = "Notice of Violation"
        Me.TPNOV.UseVisualStyleBackColor = True
        '
        'txtNOVComments
        '
        Me.txtNOVComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNOVComments.Location = New System.Drawing.Point(0, 237)
        Me.txtNOVComments.MaxLength = 3980
        Me.txtNOVComments.Multiline = True
        Me.txtNOVComments.Name = "txtNOVComments"
        Me.txtNOVComments.Size = New System.Drawing.Size(884, 232)
        Me.txtNOVComments.TabIndex = 358
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.lblNFAdownload)
        Me.Panel9.Controls.Add(Me.Label56)
        Me.Panel9.Controls.Add(Me.Label57)
        Me.Panel9.Controls.Add(Me.btnDownloadNFA)
        Me.Panel9.Controls.Add(Me.btnUploadNFA)
        Me.Panel9.Controls.Add(Me.DTPNOVToUC)
        Me.Panel9.Controls.Add(Me.Label46)
        Me.Panel9.Controls.Add(Me.Label17)
        Me.Panel9.Controls.Add(Me.DTPNOVsent)
        Me.Panel9.Controls.Add(Me.DTPNFAToPM)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Controls.Add(Me.Label45)
        Me.Panel9.Controls.Add(Me.DTPNOVResponseReceived)
        Me.Panel9.Controls.Add(Me.DTPNFAToUC)
        Me.Panel9.Controls.Add(Me.Label9)
        Me.Panel9.Controls.Add(Me.Label44)
        Me.Panel9.Controls.Add(Me.DTPNFALetterSent)
        Me.Panel9.Controls.Add(Me.DTPNOVToPM)
        Me.Panel9.Controls.Add(Me.Label13)
        Me.Panel9.Controls.Add(Me.Label26)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(884, 237)
        Me.Panel9.TabIndex = 380
        '
        'lblNFAdownload
        '
        Me.lblNFAdownload.AutoSize = True
        Me.lblNFAdownload.Location = New System.Drawing.Point(375, 215)
        Me.lblNFAdownload.Name = "lblNFAdownload"
        Me.lblNFAdownload.Size = New System.Drawing.Size(55, 13)
        Me.lblNFAdownload.TabIndex = 401
        Me.lblNFAdownload.Text = "Download"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(300, 215)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(41, 13)
        Me.Label56.TabIndex = 400
        Me.Label56.Text = "Upload"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(274, 192)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(106, 13)
        Me.Label57.TabIndex = 399
        Me.Label57.Text = "NFA file (PDF ONLY)"
        '
        'btnDownloadNFA
        '
        Me.btnDownloadNFA.BackgroundImage = CType(resources.GetObject("btnDownloadNFA.BackgroundImage"), System.Drawing.Image)
        Me.btnDownloadNFA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDownloadNFA.Location = New System.Drawing.Point(350, 210)
        Me.btnDownloadNFA.Name = "btnDownloadNFA"
        Me.btnDownloadNFA.Size = New System.Drawing.Size(24, 23)
        Me.btnDownloadNFA.TabIndex = 398
        '
        'btnUploadNFA
        '
        Me.btnUploadNFA.BackgroundImage = CType(resources.GetObject("btnUploadNFA.BackgroundImage"), System.Drawing.Image)
        Me.btnUploadNFA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUploadNFA.Location = New System.Drawing.Point(276, 210)
        Me.btnUploadNFA.Name = "btnUploadNFA"
        Me.btnUploadNFA.Size = New System.Drawing.Size(24, 23)
        Me.btnUploadNFA.TabIndex = 397
        '
        'DTPNOVToUC
        '
        Me.DTPNOVToUC.Checked = False
        Me.DTPNOVToUC.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNOVToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNOVToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNOVToUC.Location = New System.Drawing.Point(7, 7)
        Me.DTPNOVToUC.Name = "DTPNOVToUC"
        Me.DTPNOVToUC.ShowCheckBox = True
        Me.DTPNOVToUC.Size = New System.Drawing.Size(119, 22)
        Me.DTPNOVToUC.TabIndex = 372
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(131, 162)
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
        'DTPNOVsent
        '
        Me.DTPNOVsent.Checked = False
        Me.DTPNOVsent.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNOVsent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNOVsent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNOVsent.Location = New System.Drawing.Point(7, 67)
        Me.DTPNOVsent.Name = "DTPNOVsent"
        Me.DTPNOVsent.ShowCheckBox = True
        Me.DTPNOVsent.Size = New System.Drawing.Size(119, 22)
        Me.DTPNOVsent.TabIndex = 366
        '
        'DTPNFAToPM
        '
        Me.DTPNFAToPM.Checked = False
        Me.DTPNFAToPM.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNFAToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNFAToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNFAToPM.Location = New System.Drawing.Point(7, 157)
        Me.DTPNFAToPM.Name = "DTPNFAToPM"
        Me.DTPNFAToPM.ShowCheckBox = True
        Me.DTPNFAToPM.Size = New System.Drawing.Size(119, 22)
        Me.DTPNFAToPM.TabIndex = 378
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
        Me.Label45.Location = New System.Drawing.Point(131, 132)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(84, 13)
        Me.Label45.TabIndex = 377
        Me.Label45.Text = "Date NFA to UC"
        '
        'DTPNOVResponseReceived
        '
        Me.DTPNOVResponseReceived.Checked = False
        Me.DTPNOVResponseReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNOVResponseReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNOVResponseReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNOVResponseReceived.Location = New System.Drawing.Point(7, 97)
        Me.DTPNOVResponseReceived.Name = "DTPNOVResponseReceived"
        Me.DTPNOVResponseReceived.ShowCheckBox = True
        Me.DTPNOVResponseReceived.Size = New System.Drawing.Size(119, 22)
        Me.DTPNOVResponseReceived.TabIndex = 368
        '
        'DTPNFAToUC
        '
        Me.DTPNFAToUC.Checked = False
        Me.DTPNFAToUC.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNFAToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNFAToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNFAToUC.Location = New System.Drawing.Point(7, 127)
        Me.DTPNFAToUC.Name = "DTPNFAToUC"
        Me.DTPNFAToUC.ShowCheckBox = True
        Me.DTPNFAToUC.Size = New System.Drawing.Size(119, 22)
        Me.DTPNFAToUC.TabIndex = 376
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
        'DTPNFALetterSent
        '
        Me.DTPNFALetterSent.Checked = False
        Me.DTPNFALetterSent.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNFALetterSent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNFALetterSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNFALetterSent.Location = New System.Drawing.Point(7, 187)
        Me.DTPNFALetterSent.Name = "DTPNFALetterSent"
        Me.DTPNFALetterSent.ShowCheckBox = True
        Me.DTPNFALetterSent.Size = New System.Drawing.Size(119, 22)
        Me.DTPNFALetterSent.TabIndex = 370
        '
        'DTPNOVToPM
        '
        Me.DTPNOVToPM.Checked = False
        Me.DTPNOVToPM.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNOVToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNOVToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNOVToPM.Location = New System.Drawing.Point(7, 37)
        Me.DTPNOVToPM.Name = "DTPNOVToPM"
        Me.DTPNOVToPM.ShowCheckBox = True
        Me.DTPNOVToPM.Size = New System.Drawing.Size(119, 22)
        Me.DTPNOVToPM.TabIndex = 374
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(131, 192)
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
        'TPCO
        '
        Me.TPCO.AutoScroll = True
        Me.TPCO.Controls.Add(Me.txtCOComments)
        Me.TPCO.Controls.Add(Me.Panel10)
        Me.TPCO.Location = New System.Drawing.Point(4, 22)
        Me.TPCO.Name = "TPCO"
        Me.TPCO.Size = New System.Drawing.Size(884, 469)
        Me.TPCO.TabIndex = 3
        Me.TPCO.Text = "Consent Order"
        Me.TPCO.UseVisualStyleBackColor = True
        '
        'txtCOComments
        '
        Me.txtCOComments.AcceptsReturn = True
        Me.txtCOComments.AcceptsTab = True
        Me.txtCOComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCOComments.Location = New System.Drawing.Point(0, 294)
        Me.txtCOComments.MaxLength = 4000
        Me.txtCOComments.Multiline = True
        Me.txtCOComments.Name = "txtCOComments"
        Me.txtCOComments.Size = New System.Drawing.Size(884, 175)
        Me.txtCOComments.TabIndex = 357
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.lblCODownload)
        Me.Panel10.Controls.Add(Me.Label22)
        Me.Panel10.Controls.Add(Me.Label12)
        Me.Panel10.Controls.Add(Me.DTPCOToUC)
        Me.Panel10.Controls.Add(Me.btnDownloadCO)
        Me.Panel10.Controls.Add(Me.txtPenaltyComments)
        Me.Panel10.Controls.Add(Me.btnUploadCO)
        Me.Panel10.Controls.Add(Me.txtStipulatedKey)
        Me.Panel10.Controls.Add(Me.Label29)
        Me.Panel10.Controls.Add(Me.Label21)
        Me.Panel10.Controls.Add(Me.txtCONumber)
        Me.Panel10.Controls.Add(Me.txtCOPenaltyAmount)
        Me.Panel10.Controls.Add(Me.Label49)
        Me.Panel10.Controls.Add(Me.Label28)
        Me.Panel10.Controls.Add(Me.Label48)
        Me.Panel10.Controls.Add(Me.DTPCOProposed)
        Me.Panel10.Controls.Add(Me.DTPCOToPM)
        Me.Panel10.Controls.Add(Me.Label14)
        Me.Panel10.Controls.Add(Me.Label47)
        Me.Panel10.Controls.Add(Me.DTPCOReceivedfromDirector)
        Me.Panel10.Controls.Add(Me.Label15)
        Me.Panel10.Controls.Add(Me.DTPCOExecuted)
        Me.Panel10.Controls.Add(Me.Label16)
        Me.Panel10.Controls.Add(Me.DTPCOReceivedfromCompany)
        Me.Panel10.Controls.Add(Me.Label20)
        Me.Panel10.Controls.Add(Me.Label18)
        Me.Panel10.Controls.Add(Me.DTPCOResolved)
        Me.Panel10.Controls.Add(Me.StipulatedPenalties)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(884, 294)
        Me.Panel10.TabIndex = 393
        '
        'lblCODownload
        '
        Me.lblCODownload.AutoSize = True
        Me.lblCODownload.Location = New System.Drawing.Point(290, 239)
        Me.lblCODownload.Name = "lblCODownload"
        Me.lblCODownload.Size = New System.Drawing.Size(55, 13)
        Me.lblCODownload.TabIndex = 396
        Me.lblCODownload.Text = "Download"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(290, 212)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(41, 13)
        Me.Label22.TabIndex = 395
        Me.Label22.Text = "Upload"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(263, 189)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 13)
        Me.Label12.TabIndex = 394
        Me.Label12.Text = "CO file (PDF ONLY)"
        '
        'DTPCOToUC
        '
        Me.DTPCOToUC.Checked = False
        Me.DTPCOToUC.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOToUC.Location = New System.Drawing.Point(7, 6)
        Me.DTPCOToUC.Name = "DTPCOToUC"
        Me.DTPCOToUC.ShowCheckBox = True
        Me.DTPCOToUC.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOToUC.TabIndex = 385
        '
        'btnDownloadCO
        '
        Me.btnDownloadCO.BackgroundImage = CType(resources.GetObject("btnDownloadCO.BackgroundImage"), System.Drawing.Image)
        Me.btnDownloadCO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDownloadCO.Location = New System.Drawing.Point(265, 234)
        Me.btnDownloadCO.Name = "btnDownloadCO"
        Me.btnDownloadCO.Size = New System.Drawing.Size(24, 23)
        Me.btnDownloadCO.TabIndex = 392
        '
        'txtPenaltyComments
        '
        Me.txtPenaltyComments.AcceptsReturn = True
        Me.txtPenaltyComments.AcceptsTab = True
        Me.txtPenaltyComments.Location = New System.Drawing.Point(475, 28)
        Me.txtPenaltyComments.MaxLength = 4000
        Me.txtPenaltyComments.Multiline = True
        Me.txtPenaltyComments.Name = "txtPenaltyComments"
        Me.txtPenaltyComments.Size = New System.Drawing.Size(373, 48)
        Me.txtPenaltyComments.TabIndex = 355
        '
        'btnUploadCO
        '
        Me.btnUploadCO.BackgroundImage = CType(resources.GetObject("btnUploadCO.BackgroundImage"), System.Drawing.Image)
        Me.btnUploadCO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUploadCO.Location = New System.Drawing.Point(266, 207)
        Me.btnUploadCO.Name = "btnUploadCO"
        Me.btnUploadCO.Size = New System.Drawing.Size(24, 23)
        Me.btnUploadCO.TabIndex = 391
        '
        'txtStipulatedKey
        '
        Me.txtStipulatedKey.Location = New System.Drawing.Point(848, 100)
        Me.txtStipulatedKey.Name = "txtStipulatedKey"
        Me.txtStipulatedKey.ReadOnly = True
        Me.txtStipulatedKey.Size = New System.Drawing.Size(28, 20)
        Me.txtStipulatedKey.TabIndex = 368
        Me.txtStipulatedKey.Tag = "GroupStipulatedPenaltyInput"
        Me.txtStipulatedKey.Visible = False
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
        'txtCONumber
        '
        Me.txtCONumber.Location = New System.Drawing.Point(135, 186)
        Me.txtCONumber.MaxLength = 20
        Me.txtCONumber.Name = "txtCONumber"
        Me.txtCONumber.Size = New System.Drawing.Size(100, 20)
        Me.txtCONumber.TabIndex = 390
        '
        'txtCOPenaltyAmount
        '
        Me.txtCOPenaltyAmount.Location = New System.Drawing.Point(475, 4)
        Me.txtCOPenaltyAmount.MaxLength = 20
        Me.txtCOPenaltyAmount.Name = "txtCOPenaltyAmount"
        Me.txtCOPenaltyAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtCOPenaltyAmount.TabIndex = 354
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(98, 189)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(32, 13)
        Me.Label49.TabIndex = 389
        Me.Label49.Text = "CO #"
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
        'DTPCOProposed
        '
        Me.DTPCOProposed.Checked = False
        Me.DTPCOProposed.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOProposed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOProposed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOProposed.Location = New System.Drawing.Point(7, 64)
        Me.DTPCOProposed.Name = "DTPCOProposed"
        Me.DTPCOProposed.ShowCheckBox = True
        Me.DTPCOProposed.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOProposed.TabIndex = 372
        '
        'DTPCOToPM
        '
        Me.DTPCOToPM.Checked = False
        Me.DTPCOToPM.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOToPM.Location = New System.Drawing.Point(7, 34)
        Me.DTPCOToPM.Name = "DTPCOToPM"
        Me.DTPCOToPM.ShowCheckBox = True
        Me.DTPCOToPM.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOToPM.TabIndex = 387
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
        'DTPCOReceivedfromDirector
        '
        Me.DTPCOReceivedfromDirector.Checked = False
        Me.DTPCOReceivedfromDirector.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOReceivedfromDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOReceivedfromDirector.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOReceivedfromDirector.Location = New System.Drawing.Point(7, 124)
        Me.DTPCOReceivedfromDirector.Name = "DTPCOReceivedfromDirector"
        Me.DTPCOReceivedfromDirector.ShowCheckBox = True
        Me.DTPCOReceivedfromDirector.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOReceivedfromDirector.TabIndex = 374
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
        'DTPCOExecuted
        '
        Me.DTPCOExecuted.Checked = False
        Me.DTPCOExecuted.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOExecuted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOExecuted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOExecuted.Location = New System.Drawing.Point(7, 154)
        Me.DTPCOExecuted.Name = "DTPCOExecuted"
        Me.DTPCOExecuted.ShowCheckBox = True
        Me.DTPCOExecuted.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOExecuted.TabIndex = 376
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
        'DTPCOReceivedfromCompany
        '
        Me.DTPCOReceivedfromCompany.Checked = False
        Me.DTPCOReceivedfromCompany.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOReceivedfromCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOReceivedfromCompany.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOReceivedfromCompany.Location = New System.Drawing.Point(7, 94)
        Me.DTPCOReceivedfromCompany.Name = "DTPCOReceivedfromCompany"
        Me.DTPCOReceivedfromCompany.ShowCheckBox = True
        Me.DTPCOReceivedfromCompany.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOReceivedfromCompany.TabIndex = 378
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
        'DTPCOResolved
        '
        Me.DTPCOResolved.Checked = False
        Me.DTPCOResolved.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCOResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPCOResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCOResolved.Location = New System.Drawing.Point(7, 212)
        Me.DTPCOResolved.Name = "DTPCOResolved"
        Me.DTPCOResolved.ShowCheckBox = True
        Me.DTPCOResolved.Size = New System.Drawing.Size(119, 22)
        Me.DTPCOResolved.TabIndex = 380
        '
        'StipulatedPenalties
        '
        Me.StipulatedPenalties.Controls.Add(Me.txtStipulatedPenalty)
        Me.StipulatedPenalties.Controls.Add(Me.Label30)
        Me.StipulatedPenalties.Controls.Add(Me.dgvStipulatedPenalties)
        Me.StipulatedPenalties.Controls.Add(Me.UpdateStipulatedPenaltyButton)
        Me.StipulatedPenalties.Controls.Add(Me.txtStipulatedComments)
        Me.StipulatedPenalties.Controls.Add(Me.ClearStipulatedPenaltyFormButton)
        Me.StipulatedPenalties.Controls.Add(Me.Label31)
        Me.StipulatedPenalties.Controls.Add(Me.DeletePenaltyButton)
        Me.StipulatedPenalties.Controls.Add(Me.SaveStipulatedPenaltyButton)
        Me.StipulatedPenalties.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.StipulatedPenalties.Location = New System.Drawing.Point(475, 82)
        Me.StipulatedPenalties.Name = "StipulatedPenalties"
        Me.StipulatedPenalties.Size = New System.Drawing.Size(373, 206)
        Me.StipulatedPenalties.TabIndex = 397
        Me.StipulatedPenalties.TabStop = False
        Me.StipulatedPenalties.Text = "Stipulated Penalties"
        '
        'txtStipulatedPenalty
        '
        Me.txtStipulatedPenalty.Location = New System.Drawing.Point(58, 18)
        Me.txtStipulatedPenalty.Name = "txtStipulatedPenalty"
        Me.txtStipulatedPenalty.Size = New System.Drawing.Size(91, 20)
        Me.txtStipulatedPenalty.TabIndex = 351
        Me.txtStipulatedPenalty.Tag = "GroupStipulatedPenaltyInput"
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
        'dgvStipulatedPenalties
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvStipulatedPenalties.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvStipulatedPenalties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvStipulatedPenalties.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvStipulatedPenalties.Location = New System.Drawing.Point(9, 100)
        Me.dgvStipulatedPenalties.Name = "dgvStipulatedPenalties"
        Me.dgvStipulatedPenalties.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvStipulatedPenalties.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvStipulatedPenalties.Size = New System.Drawing.Size(358, 106)
        Me.dgvStipulatedPenalties.TabIndex = 382
        '
        'UpdateStipulatedPenaltyButton
        '
        Me.UpdateStipulatedPenaltyButton.AutoSize = True
        Me.UpdateStipulatedPenaltyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UpdateStipulatedPenaltyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.UpdateStipulatedPenaltyButton.Location = New System.Drawing.Point(155, 16)
        Me.UpdateStipulatedPenaltyButton.Name = "UpdateStipulatedPenaltyButton"
        Me.UpdateStipulatedPenaltyButton.Size = New System.Drawing.Size(90, 23)
        Me.UpdateStipulatedPenaltyButton.TabIndex = 393
        Me.UpdateStipulatedPenaltyButton.Tag = "GroupExistingStipulatedPenalty"
        Me.UpdateStipulatedPenaltyButton.Text = "Update Penalty"
        '
        'txtStipulatedComments
        '
        Me.txtStipulatedComments.Location = New System.Drawing.Point(8, 58)
        Me.txtStipulatedComments.Multiline = True
        Me.txtStipulatedComments.Name = "txtStipulatedComments"
        Me.txtStipulatedComments.Size = New System.Drawing.Size(359, 36)
        Me.txtStipulatedComments.TabIndex = 366
        Me.txtStipulatedComments.Tag = "GroupStipulatedPenaltyInput"
        '
        'ClearStipulatedPenaltyFormButton
        '
        Me.ClearStipulatedPenaltyFormButton.BackgroundImage = CType(resources.GetObject("ClearStipulatedPenaltyFormButton.BackgroundImage"), System.Drawing.Image)
        Me.ClearStipulatedPenaltyFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClearStipulatedPenaltyFormButton.Location = New System.Drawing.Point(343, 16)
        Me.ClearStipulatedPenaltyFormButton.Name = "ClearStipulatedPenaltyFormButton"
        Me.ClearStipulatedPenaltyFormButton.Size = New System.Drawing.Size(24, 23)
        Me.ClearStipulatedPenaltyFormButton.TabIndex = 384
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
        'DeletePenaltyButton
        '
        Me.DeletePenaltyButton.AutoSize = True
        Me.DeletePenaltyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.DeletePenaltyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeletePenaltyButton.Location = New System.Drawing.Point(251, 16)
        Me.DeletePenaltyButton.Name = "DeletePenaltyButton"
        Me.DeletePenaltyButton.Size = New System.Drawing.Size(86, 23)
        Me.DeletePenaltyButton.TabIndex = 349
        Me.DeletePenaltyButton.Tag = "GroupExistingStipulatedPenalty"
        Me.DeletePenaltyButton.Text = "Delete Penalty"
        '
        'SaveStipulatedPenaltyButton
        '
        Me.SaveStipulatedPenaltyButton.AutoSize = True
        Me.SaveStipulatedPenaltyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SaveStipulatedPenaltyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveStipulatedPenaltyButton.Location = New System.Drawing.Point(155, 16)
        Me.SaveStipulatedPenaltyButton.Name = "SaveStipulatedPenaltyButton"
        Me.SaveStipulatedPenaltyButton.Size = New System.Drawing.Size(99, 23)
        Me.SaveStipulatedPenaltyButton.TabIndex = 349
        Me.SaveStipulatedPenaltyButton.Tag = "GroupEmptyStipulatedPenalty"
        Me.SaveStipulatedPenaltyButton.Text = "Add New Penalty"
        '
        'TPAO
        '
        Me.TPAO.AutoScroll = True
        Me.TPAO.Controls.Add(Me.txtAOComments)
        Me.TPAO.Controls.Add(Me.Panel11)
        Me.TPAO.Location = New System.Drawing.Point(4, 22)
        Me.TPAO.Name = "TPAO"
        Me.TPAO.Size = New System.Drawing.Size(884, 469)
        Me.TPAO.TabIndex = 4
        Me.TPAO.Text = "Administrative Order"
        Me.TPAO.UseVisualStyleBackColor = True
        '
        'txtAOComments
        '
        Me.txtAOComments.AcceptsReturn = True
        Me.txtAOComments.AcceptsTab = True
        Me.txtAOComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAOComments.Location = New System.Drawing.Point(0, 111)
        Me.txtAOComments.MaxLength = 4000
        Me.txtAOComments.Multiline = True
        Me.txtAOComments.Name = "txtAOComments"
        Me.txtAOComments.Size = New System.Drawing.Size(884, 358)
        Me.txtAOComments.TabIndex = 343
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.lblDownloadAO)
        Me.Panel11.Controls.Add(Me.Label33)
        Me.Panel11.Controls.Add(Me.Label58)
        Me.Panel11.Controls.Add(Me.btnDownloadAO)
        Me.Panel11.Controls.Add(Me.btnUploadAO)
        Me.Panel11.Controls.Add(Me.DTPAOExecuted)
        Me.Panel11.Controls.Add(Me.Label42)
        Me.Panel11.Controls.Add(Me.Label25)
        Me.Panel11.Controls.Add(Me.DTPAOResolved)
        Me.Panel11.Controls.Add(Me.Label32)
        Me.Panel11.Controls.Add(Me.Label41)
        Me.Panel11.Controls.Add(Me.DTPAOAppealed)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(884, 111)
        Me.Panel11.TabIndex = 388
        '
        'lblDownloadAO
        '
        Me.lblDownloadAO.AutoSize = True
        Me.lblDownloadAO.Location = New System.Drawing.Point(536, 85)
        Me.lblDownloadAO.Name = "lblDownloadAO"
        Me.lblDownloadAO.Size = New System.Drawing.Size(55, 13)
        Me.lblDownloadAO.TabIndex = 406
        Me.lblDownloadAO.Text = "Download"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(461, 85)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(41, 13)
        Me.Label33.TabIndex = 405
        Me.Label33.Text = "Upload"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(435, 62)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(179, 13)
        Me.Label58.TabIndex = 404
        Me.Label58.Text = "Administrative Order file (PDF ONLY)"
        '
        'btnDownloadAO
        '
        Me.btnDownloadAO.BackgroundImage = CType(resources.GetObject("btnDownloadAO.BackgroundImage"), System.Drawing.Image)
        Me.btnDownloadAO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDownloadAO.Location = New System.Drawing.Point(511, 80)
        Me.btnDownloadAO.Name = "btnDownloadAO"
        Me.btnDownloadAO.Size = New System.Drawing.Size(24, 23)
        Me.btnDownloadAO.TabIndex = 403
        '
        'btnUploadAO
        '
        Me.btnUploadAO.BackgroundImage = CType(resources.GetObject("btnUploadAO.BackgroundImage"), System.Drawing.Image)
        Me.btnUploadAO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUploadAO.Location = New System.Drawing.Point(437, 80)
        Me.btnUploadAO.Name = "btnUploadAO"
        Me.btnUploadAO.Size = New System.Drawing.Size(24, 23)
        Me.btnUploadAO.TabIndex = 402
        '
        'DTPAOExecuted
        '
        Me.DTPAOExecuted.Checked = False
        Me.DTPAOExecuted.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAOExecuted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPAOExecuted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAOExecuted.Location = New System.Drawing.Point(9, 8)
        Me.DTPAOExecuted.Name = "DTPAOExecuted"
        Me.DTPAOExecuted.ShowCheckBox = True
        Me.DTPAOExecuted.Size = New System.Drawing.Size(119, 22)
        Me.DTPAOExecuted.TabIndex = 382
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
        'DTPAOResolved
        '
        Me.DTPAOResolved.Checked = False
        Me.DTPAOResolved.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAOResolved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPAOResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAOResolved.Location = New System.Drawing.Point(9, 62)
        Me.DTPAOResolved.Name = "DTPAOResolved"
        Me.DTPAOResolved.ShowCheckBox = True
        Me.DTPAOResolved.Size = New System.Drawing.Size(119, 22)
        Me.DTPAOResolved.TabIndex = 386
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
        'DTPAOAppealed
        '
        Me.DTPAOAppealed.Checked = False
        Me.DTPAOAppealed.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAOAppealed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPAOAppealed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAOAppealed.Location = New System.Drawing.Point(9, 34)
        Me.DTPAOAppealed.Name = "DTPAOAppealed"
        Me.DTPAOAppealed.ShowCheckBox = True
        Me.DTPAOAppealed.Size = New System.Drawing.Size(119, 22)
        Me.DTPAOAppealed.TabIndex = 384
        '
        'TPAuditHistory
        '
        Me.TPAuditHistory.Controls.Add(Me.dgvAuditHistory)
        Me.TPAuditHistory.Controls.Add(Me.Panel12)
        Me.TPAuditHistory.Location = New System.Drawing.Point(4, 22)
        Me.TPAuditHistory.Name = "TPAuditHistory"
        Me.TPAuditHistory.Size = New System.Drawing.Size(884, 469)
        Me.TPAuditHistory.TabIndex = 6
        Me.TPAuditHistory.Text = "Audit History"
        Me.TPAuditHistory.UseVisualStyleBackColor = True
        '
        'dgvAuditHistory
        '
        Me.dgvAuditHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAuditHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAuditHistory.Location = New System.Drawing.Point(0, 66)
        Me.dgvAuditHistory.Name = "dgvAuditHistory"
        Me.dgvAuditHistory.Size = New System.Drawing.Size(884, 403)
        Me.dgvAuditHistory.TabIndex = 1
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.btnREfreshAudit)
        Me.Panel12.Controls.Add(Me.btnHideAudit)
        Me.Panel12.Controls.Add(Me.btnExportAuditToExcel)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(884, 66)
        Me.Panel12.TabIndex = 0
        '
        'btnREfreshAudit
        '
        Me.btnREfreshAudit.AutoSize = True
        Me.btnREfreshAudit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnREfreshAudit.Location = New System.Drawing.Point(689, 18)
        Me.btnREfreshAudit.Name = "btnREfreshAudit"
        Me.btnREfreshAudit.Size = New System.Drawing.Size(54, 23)
        Me.btnREfreshAudit.TabIndex = 2
        Me.btnREfreshAudit.Text = "Refresh"
        Me.btnREfreshAudit.UseVisualStyleBackColor = True
        '
        'btnHideAudit
        '
        Me.btnHideAudit.AutoSize = True
        Me.btnHideAudit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnHideAudit.Location = New System.Drawing.Point(766, 18)
        Me.btnHideAudit.Name = "btnHideAudit"
        Me.btnHideAudit.Size = New System.Drawing.Size(66, 23)
        Me.btnHideAudit.TabIndex = 1
        Me.btnHideAudit.Text = "Hide Audit"
        Me.btnHideAudit.UseVisualStyleBackColor = True
        '
        'btnExportAuditToExcel
        '
        Me.btnExportAuditToExcel.AutoSize = True
        Me.btnExportAuditToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportAuditToExcel.Location = New System.Drawing.Point(8, 18)
        Me.btnExportAuditToExcel.Name = "btnExportAuditToExcel"
        Me.btnExportAuditToExcel.Size = New System.Drawing.Size(88, 23)
        Me.btnExportAuditToExcel.TabIndex = 0
        Me.btnExportAuditToExcel.Text = "Export to Excel"
        Me.btnExportAuditToExcel.UseVisualStyleBackColor = True
        '
        'tabDocuments
        '
        Me.tabDocuments.Controls.Add(Me.lblMessage)
        Me.tabDocuments.Controls.Add(Me.pnlAddNew)
        Me.tabDocuments.Controls.Add(Me.pnlUpdate)
        Me.tabDocuments.Controls.Add(Me.lblCurrentFiles)
        Me.tabDocuments.Controls.Add(Me.dgvFileList)
        Me.tabDocuments.Location = New System.Drawing.Point(4, 22)
        Me.tabDocuments.Name = "tabDocuments"
        Me.tabDocuments.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDocuments.Size = New System.Drawing.Size(884, 469)
        Me.tabDocuments.TabIndex = 7
        Me.tabDocuments.Text = "Documents"
        Me.tabDocuments.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.lblMessage.Location = New System.Drawing.Point(162, 313)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(5)
        Me.lblMessage.Size = New System.Drawing.Size(355, 37)
        Me.lblMessage.TabIndex = 18
        Me.lblMessage.Text = "Message Placeholder" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2"
        Me.lblMessage.Visible = False
        '
        'pnlAddNew
        '
        Me.pnlAddNew.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlAddNew.Controls.Add(Me.btnNewFileCancel)
        Me.pnlAddNew.Controls.Add(Me.btnNewFileUpload)
        Me.pnlAddNew.Controls.Add(Me.txtNewDescription)
        Me.pnlAddNew.Controls.Add(Me.lblNewDescription)
        Me.pnlAddNew.Controls.Add(Me.lblNewFileName)
        Me.pnlAddNew.Controls.Add(Me.btnChooseNewFile)
        Me.pnlAddNew.Controls.Add(Me.lblDocumentTypes)
        Me.pnlAddNew.Controls.Add(Me.ddlNewDocumentType)
        Me.pnlAddNew.Location = New System.Drawing.Point(447, 28)
        Me.pnlAddNew.Name = "pnlAddNew"
        Me.pnlAddNew.Size = New System.Drawing.Size(217, 266)
        Me.pnlAddNew.TabIndex = 17
        '
        'btnNewFileCancel
        '
        Me.btnNewFileCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewFileCancel.Enabled = False
        Me.btnNewFileCancel.Location = New System.Drawing.Point(58, 150)
        Me.btnNewFileCancel.Name = "btnNewFileCancel"
        Me.btnNewFileCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnNewFileCancel.TabIndex = 4
        Me.btnNewFileCancel.Text = "Cancel"
        Me.btnNewFileCancel.UseVisualStyleBackColor = True
        Me.btnNewFileCancel.Visible = False
        '
        'btnNewFileUpload
        '
        Me.btnNewFileUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewFileUpload.Enabled = False
        Me.btnNewFileUpload.Location = New System.Drawing.Point(139, 150)
        Me.btnNewFileUpload.Name = "btnNewFileUpload"
        Me.btnNewFileUpload.Size = New System.Drawing.Size(75, 23)
        Me.btnNewFileUpload.TabIndex = 3
        Me.btnNewFileUpload.Text = "Add this file"
        Me.btnNewFileUpload.UseVisualStyleBackColor = True
        Me.btnNewFileUpload.Visible = False
        '
        'txtNewDescription
        '
        Me.txtNewDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewDescription.Location = New System.Drawing.Point(6, 124)
        Me.txtNewDescription.Name = "txtNewDescription"
        Me.txtNewDescription.Size = New System.Drawing.Size(208, 20)
        Me.txtNewDescription.TabIndex = 2
        Me.txtNewDescription.Visible = False
        '
        'lblNewDescription
        '
        Me.lblNewDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewDescription.AutoSize = True
        Me.lblNewDescription.Location = New System.Drawing.Point(3, 108)
        Me.lblNewDescription.Name = "lblNewDescription"
        Me.lblNewDescription.Size = New System.Drawing.Size(106, 13)
        Me.lblNewDescription.TabIndex = 5
        Me.lblNewDescription.Text = "Description (optional)"
        Me.lblNewDescription.Visible = False
        '
        'lblNewFileName
        '
        Me.lblNewFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewFileName.AutoSize = True
        Me.lblNewFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewFileName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblNewFileName.Location = New System.Drawing.Point(3, 75)
        Me.lblNewFileName.Name = "lblNewFileName"
        Me.lblNewFileName.Size = New System.Drawing.Size(145, 17)
        Me.lblNewFileName.TabIndex = 4
        Me.lblNewFileName.Text = "FileName placeholder"
        Me.lblNewFileName.Visible = False
        '
        'btnChooseNewFile
        '
        Me.btnChooseNewFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChooseNewFile.Location = New System.Drawing.Point(139, 29)
        Me.btnChooseNewFile.Name = "btnChooseNewFile"
        Me.btnChooseNewFile.Size = New System.Drawing.Size(75, 23)
        Me.btnChooseNewFile.TabIndex = 1
        Me.btnChooseNewFile.Text = "Select file"
        Me.btnChooseNewFile.UseVisualStyleBackColor = True
        '
        'lblDocumentTypes
        '
        Me.lblDocumentTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentTypes.AutoSize = True
        Me.lblDocumentTypes.Location = New System.Drawing.Point(3, 3)
        Me.lblDocumentTypes.Name = "lblDocumentTypes"
        Me.lblDocumentTypes.Size = New System.Drawing.Size(49, 13)
        Me.lblDocumentTypes.TabIndex = 3
        Me.lblDocumentTypes.Text = "Add new"
        '
        'ddlNewDocumentType
        '
        Me.ddlNewDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlNewDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlNewDocumentType.FormattingEnabled = True
        Me.ddlNewDocumentType.Location = New System.Drawing.Point(58, 0)
        Me.ddlNewDocumentType.Name = "ddlNewDocumentType"
        Me.ddlNewDocumentType.Size = New System.Drawing.Size(156, 21)
        Me.ddlNewDocumentType.TabIndex = 0
        '
        'pnlUpdate
        '
        Me.pnlUpdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlUpdate.Controls.Add(Me.lblUpdateDescription)
        Me.pnlUpdate.Controls.Add(Me.ddlUpdateDocumentType)
        Me.pnlUpdate.Controls.Add(Me.btnDeleteFile)
        Me.pnlUpdate.Controls.Add(Me.btnDownloadFile)
        Me.pnlUpdate.Controls.Add(Me.txtUpdateDescription)
        Me.pnlUpdate.Controls.Add(Me.btnUpdateFileDescription)
        Me.pnlUpdate.Controls.Add(Me.lblSelectedFileName)
        Me.pnlUpdate.Location = New System.Drawing.Point(14, 221)
        Me.pnlUpdate.Name = "pnlUpdate"
        Me.pnlUpdate.Size = New System.Drawing.Size(419, 73)
        Me.pnlUpdate.TabIndex = 16
        '
        'lblUpdateDescription
        '
        Me.lblUpdateDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUpdateDescription.AutoSize = True
        Me.lblUpdateDescription.Location = New System.Drawing.Point(0, 36)
        Me.lblUpdateDescription.Name = "lblUpdateDescription"
        Me.lblUpdateDescription.Size = New System.Drawing.Size(106, 13)
        Me.lblUpdateDescription.TabIndex = 6
        Me.lblUpdateDescription.Text = "Description (optional)"
        Me.lblUpdateDescription.Visible = False
        '
        'ddlUpdateDocumentType
        '
        Me.ddlUpdateDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlUpdateDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlUpdateDocumentType.Enabled = False
        Me.ddlUpdateDocumentType.FormattingEnabled = True
        Me.ddlUpdateDocumentType.Location = New System.Drawing.Point(151, 52)
        Me.ddlUpdateDocumentType.Name = "ddlUpdateDocumentType"
        Me.ddlUpdateDocumentType.Size = New System.Drawing.Size(156, 21)
        Me.ddlUpdateDocumentType.TabIndex = 6
        Me.ddlUpdateDocumentType.Visible = False
        '
        'btnDeleteFile
        '
        Me.btnDeleteFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteFile.Enabled = False
        Me.btnDeleteFile.Location = New System.Drawing.Point(344, 7)
        Me.btnDeleteFile.Name = "btnDeleteFile"
        Me.btnDeleteFile.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteFile.TabIndex = 8
        Me.btnDeleteFile.Text = "Delete"
        Me.btnDeleteFile.UseVisualStyleBackColor = True
        '
        'btnDownloadFile
        '
        Me.btnDownloadFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDownloadFile.Enabled = False
        Me.btnDownloadFile.Location = New System.Drawing.Point(263, 7)
        Me.btnDownloadFile.Name = "btnDownloadFile"
        Me.btnDownloadFile.Size = New System.Drawing.Size(75, 23)
        Me.btnDownloadFile.TabIndex = 4
        Me.btnDownloadFile.Text = "Download"
        Me.btnDownloadFile.UseVisualStyleBackColor = True
        '
        'txtUpdateDescription
        '
        Me.txtUpdateDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUpdateDescription.Location = New System.Drawing.Point(0, 52)
        Me.txtUpdateDescription.Name = "txtUpdateDescription"
        Me.txtUpdateDescription.Size = New System.Drawing.Size(130, 20)
        Me.txtUpdateDescription.TabIndex = 5
        Me.txtUpdateDescription.Visible = False
        '
        'btnUpdateFileDescription
        '
        Me.btnUpdateFileDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateFileDescription.AutoSize = True
        Me.btnUpdateFileDescription.Enabled = False
        Me.btnUpdateFileDescription.Location = New System.Drawing.Point(313, 50)
        Me.btnUpdateFileDescription.Name = "btnUpdateFileDescription"
        Me.btnUpdateFileDescription.Size = New System.Drawing.Size(106, 23)
        Me.btnUpdateFileDescription.TabIndex = 7
        Me.btnUpdateFileDescription.Text = "Update description"
        Me.btnUpdateFileDescription.UseVisualStyleBackColor = True
        Me.btnUpdateFileDescription.Visible = False
        '
        'lblSelectedFileName
        '
        Me.lblSelectedFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedFileName.AutoSize = True
        Me.lblSelectedFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedFileName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblSelectedFileName.Location = New System.Drawing.Point(0, 10)
        Me.lblSelectedFileName.Name = "lblSelectedFileName"
        Me.lblSelectedFileName.Size = New System.Drawing.Size(145, 17)
        Me.lblSelectedFileName.TabIndex = 14
        Me.lblSelectedFileName.Text = "FileName placeholder"
        Me.lblSelectedFileName.Visible = False
        '
        'lblCurrentFiles
        '
        Me.lblCurrentFiles.AutoSize = True
        Me.lblCurrentFiles.Location = New System.Drawing.Point(11, 12)
        Me.lblCurrentFiles.Name = "lblCurrentFiles"
        Me.lblCurrentFiles.Size = New System.Drawing.Size(65, 13)
        Me.lblCurrentFiles.TabIndex = 5
        Me.lblCurrentFiles.Text = "Current Files"
        '
        'dgvFileList
        '
        Me.dgvFileList.AllowUserToAddRows = False
        Me.dgvFileList.AllowUserToDeleteRows = False
        Me.dgvFileList.AllowUserToOrderColumns = True
        Me.dgvFileList.AllowUserToResizeRows = False
        Me.dgvFileList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFileList.Enabled = False
        Me.dgvFileList.Location = New System.Drawing.Point(14, 28)
        Me.dgvFileList.MultiSelect = False
        Me.dgvFileList.Name = "dgvFileList"
        Me.dgvFileList.ReadOnly = True
        Me.dgvFileList.RowHeadersVisible = False
        Me.dgvFileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFileList.Size = New System.Drawing.Size(419, 187)
        Me.dgvFileList.StandardTab = True
        Me.dgvFileList.TabIndex = 4
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label55)
        Me.Panel4.Controls.Add(Me.txtCounty)
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Controls.Add(Me.Label54)
        Me.Panel4.Controls.Add(Me.txtClassification)
        Me.Panel4.Controls.Add(Me.DTPLastSave)
        Me.Panel4.Controls.Add(Me.lblLastEdited)
        Me.Panel4.Controls.Add(Me.Label53)
        Me.Panel4.Controls.Add(Me.txtFacilityAddress)
        Me.Panel4.Controls.Add(Me.Label52)
        Me.Panel4.Controls.Add(Me.txtFacilityName)
        Me.Panel4.Controls.Add(Me.Label51)
        Me.Panel4.Controls.Add(Me.Label50)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.txtTrackingNumber)
        Me.Panel4.Controls.Add(Me.txtEnforcementNumber)
        Me.Panel4.Controls.Add(Me.txtAIRSNumber)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 49)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(892, 122)
        Me.Panel4.TabIndex = 268
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(338, 59)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(40, 13)
        Me.Label55.TabIndex = 138
        Me.Label55.Text = "County"
        '
        'txtCounty
        '
        Me.txtCounty.Location = New System.Drawing.Point(398, 55)
        Me.txtCounty.Name = "txtCounty"
        Me.txtCounty.ReadOnly = True
        Me.txtCounty.Size = New System.Drawing.Size(71, 20)
        Me.txtCounty.TabIndex = 137
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chbAPCI)
        Me.GroupBox2.Controls.Add(Me.chbAPCF)
        Me.GroupBox2.Controls.Add(Me.chbAPCA)
        Me.GroupBox2.Controls.Add(Me.chbAPCV)
        Me.GroupBox2.Controls.Add(Me.chbAPCM)
        Me.GroupBox2.Controls.Add(Me.chbAPC9)
        Me.GroupBox2.Controls.Add(Me.chbAPC8)
        Me.GroupBox2.Controls.Add(Me.chbAPC7)
        Me.GroupBox2.Controls.Add(Me.chbAPC6)
        Me.GroupBox2.Controls.Add(Me.chbAPC4)
        Me.GroupBox2.Controls.Add(Me.chbAPC3)
        Me.GroupBox2.Controls.Add(Me.chbAPC1)
        Me.GroupBox2.Controls.Add(Me.chbAPC0)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(479, 3)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(379, 96)
        Me.GroupBox2.TabIndex = 136
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Air Program Code(s) "
        '
        'chbAPCI
        '
        Me.chbAPCI.AutoCheck = False
        Me.chbAPCI.AutoSize = True
        Me.chbAPCI.Enabled = False
        Me.chbAPCI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCI.Location = New System.Drawing.Point(257, 30)
        Me.chbAPCI.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPCI.Name = "chbAPCI"
        Me.chbAPCI.Size = New System.Drawing.Size(116, 17)
        Me.chbAPCI.TabIndex = 137
        Me.chbAPCI.Text = "I - Native American"
        '
        'chbAPCF
        '
        Me.chbAPCF.AutoCheck = False
        Me.chbAPCF.AutoSize = True
        Me.chbAPCF.Enabled = False
        Me.chbAPCF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCF.Location = New System.Drawing.Point(257, 15)
        Me.chbAPCF.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPCF.Name = "chbAPCF"
        Me.chbAPCF.Size = New System.Drawing.Size(76, 17)
        Me.chbAPCF.TabIndex = 140
        Me.chbAPCF.Text = "F - FESOP"
        '
        'chbAPCA
        '
        Me.chbAPCA.AutoCheck = False
        Me.chbAPCA.AutoSize = True
        Me.chbAPCA.Enabled = False
        Me.chbAPCA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCA.Location = New System.Drawing.Point(125, 60)
        Me.chbAPCA.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPCA.Name = "chbAPCA"
        Me.chbAPCA.Size = New System.Drawing.Size(124, 17)
        Me.chbAPCA.TabIndex = 142
        Me.chbAPCA.Text = "A - Acid Precipitation"
        '
        'chbAPCV
        '
        Me.chbAPCV.AutoCheck = False
        Me.chbAPCV.AutoSize = True
        Me.chbAPCV.Enabled = False
        Me.chbAPCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCV.Location = New System.Drawing.Point(257, 61)
        Me.chbAPCV.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPCV.Name = "chbAPCV"
        Me.chbAPCV.Size = New System.Drawing.Size(72, 17)
        Me.chbAPCV.TabIndex = 139
        Me.chbAPCV.Text = "V - Title V"
        '
        'chbAPCM
        '
        Me.chbAPCM.AutoCheck = False
        Me.chbAPCM.AutoSize = True
        Me.chbAPCM.Enabled = False
        Me.chbAPCM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCM.Location = New System.Drawing.Point(257, 46)
        Me.chbAPCM.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPCM.Name = "chbAPCM"
        Me.chbAPCM.Size = New System.Drawing.Size(116, 17)
        Me.chbAPCM.TabIndex = 138
        Me.chbAPCM.Text = "M - MACT (part 63)"
        '
        'chbAPC9
        '
        Me.chbAPC9.AutoCheck = False
        Me.chbAPC9.AutoSize = True
        Me.chbAPC9.Enabled = False
        Me.chbAPC9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC9.Location = New System.Drawing.Point(125, 45)
        Me.chbAPC9.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC9.Name = "chbAPC9"
        Me.chbAPC9.Size = New System.Drawing.Size(70, 17)
        Me.chbAPC9.TabIndex = 136
        Me.chbAPC9.Text = "9 - NSPS"
        '
        'chbAPC8
        '
        Me.chbAPC8.AutoCheck = False
        Me.chbAPC8.AutoSize = True
        Me.chbAPC8.Enabled = False
        Me.chbAPC8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC8.Location = New System.Drawing.Point(125, 30)
        Me.chbAPC8.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC8.Name = "chbAPC8"
        Me.chbAPC8.Size = New System.Drawing.Size(128, 17)
        Me.chbAPC8.TabIndex = 135
        Me.chbAPC8.Text = "8 - NESHAP (Part 61)"
        '
        'chbAPC7
        '
        Me.chbAPC7.AutoCheck = False
        Me.chbAPC7.AutoSize = True
        Me.chbAPC7.Enabled = False
        Me.chbAPC7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC7.Location = New System.Drawing.Point(125, 15)
        Me.chbAPC7.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC7.Name = "chbAPC7"
        Me.chbAPC7.Size = New System.Drawing.Size(64, 17)
        Me.chbAPC7.TabIndex = 134
        Me.chbAPC7.Text = "7 - NSR"
        '
        'chbAPC6
        '
        Me.chbAPC6.AutoCheck = False
        Me.chbAPC6.AutoSize = True
        Me.chbAPC6.Enabled = False
        Me.chbAPC6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC6.Location = New System.Drawing.Point(8, 75)
        Me.chbAPC6.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC6.Name = "chbAPC6"
        Me.chbAPC6.Size = New System.Drawing.Size(63, 17)
        Me.chbAPC6.TabIndex = 133
        Me.chbAPC6.Text = "6 - PSD"
        '
        'chbAPC4
        '
        Me.chbAPC4.AutoCheck = False
        Me.chbAPC4.AutoSize = True
        Me.chbAPC4.Enabled = False
        Me.chbAPC4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC4.Location = New System.Drawing.Point(8, 60)
        Me.chbAPC4.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC4.Name = "chbAPC4"
        Me.chbAPC4.Size = New System.Drawing.Size(106, 17)
        Me.chbAPC4.TabIndex = 132
        Me.chbAPC4.Text = "4 - CFC Tracking"
        '
        'chbAPC3
        '
        Me.chbAPC3.AutoCheck = False
        Me.chbAPC3.AutoSize = True
        Me.chbAPC3.Enabled = False
        Me.chbAPC3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC3.Location = New System.Drawing.Point(8, 45)
        Me.chbAPC3.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC3.Name = "chbAPC3"
        Me.chbAPC3.Size = New System.Drawing.Size(119, 17)
        Me.chbAPC3.TabIndex = 131
        Me.chbAPC3.Text = "3 - Non-Federal SIP"
        '
        'chbAPC1
        '
        Me.chbAPC1.AutoCheck = False
        Me.chbAPC1.AutoSize = True
        Me.chbAPC1.Enabled = False
        Me.chbAPC1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC1.Location = New System.Drawing.Point(8, 30)
        Me.chbAPC1.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC1.Name = "chbAPC1"
        Me.chbAPC1.Size = New System.Drawing.Size(96, 17)
        Me.chbAPC1.TabIndex = 130
        Me.chbAPC1.Text = "1 - Federal SIP"
        '
        'chbAPC0
        '
        Me.chbAPC0.AutoCheck = False
        Me.chbAPC0.AutoSize = True
        Me.chbAPC0.Enabled = False
        Me.chbAPC0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC0.Location = New System.Drawing.Point(8, 15)
        Me.chbAPC0.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPC0.Name = "chbAPC0"
        Me.chbAPC0.Size = New System.Drawing.Size(58, 17)
        Me.chbAPC0.TabIndex = 129
        Me.chbAPC0.Text = "0 - SIP"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(338, 35)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(68, 13)
        Me.Label54.TabIndex = 11
        Me.Label54.Text = "Classification"
        '
        'txtClassification
        '
        Me.txtClassification.Location = New System.Drawing.Point(409, 31)
        Me.txtClassification.Name = "txtClassification"
        Me.txtClassification.ReadOnly = True
        Me.txtClassification.Size = New System.Drawing.Size(60, 20)
        Me.txtClassification.TabIndex = 10
        '
        'DTPLastSave
        '
        Me.DTPLastSave.Checked = False
        Me.DTPLastSave.CustomFormat = "dd-MMM-yyyy"
        Me.DTPLastSave.Enabled = False
        Me.DTPLastSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPLastSave.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPLastSave.Location = New System.Drawing.Point(736, 100)
        Me.DTPLastSave.Name = "DTPLastSave"
        Me.DTPLastSave.Size = New System.Drawing.Size(121, 22)
        Me.DTPLastSave.TabIndex = 6
        '
        'lblLastEdited
        '
        Me.lblLastEdited.AutoSize = True
        Me.lblLastEdited.Location = New System.Drawing.Point(658, 106)
        Me.lblLastEdited.Name = "lblLastEdited"
        Me.lblLastEdited.Size = New System.Drawing.Size(74, 13)
        Me.lblLastEdited.TabIndex = 7
        Me.lblLastEdited.Text = "Last edited on"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(38, 59)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(45, 13)
        Me.Label53.TabIndex = 9
        Me.Label53.Text = "Address"
        '
        'txtFacilityAddress
        '
        Me.txtFacilityAddress.Location = New System.Drawing.Point(91, 55)
        Me.txtFacilityAddress.Multiline = True
        Me.txtFacilityAddress.Name = "txtFacilityAddress"
        Me.txtFacilityAddress.ReadOnly = True
        Me.txtFacilityAddress.Size = New System.Drawing.Size(233, 47)
        Me.txtFacilityAddress.TabIndex = 8
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(15, 35)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(70, 13)
        Me.Label52.TabIndex = 7
        Me.Label52.Text = "Facility Name"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(91, 31)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(233, 20)
        Me.txtFacilityName.TabIndex = 6
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(338, 12)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(59, 13)
        Me.Label51.TabIndex = 5
        Me.Label51.Text = "Tracking #"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(173, 12)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(77, 13)
        Me.Label50.TabIndex = 4
        Me.Label50.Text = "Enforcement #"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "AIRS #"
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(398, 8)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(71, 20)
        Me.txtTrackingNumber.TabIndex = 3
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(256, 8)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.ReadOnly = True
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(68, 20)
        Me.txtEnforcementNumber.TabIndex = 2
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(63, 8)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(104, 20)
        Me.txtAIRSNumber.TabIndex = 1
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'NewSscpEnforcementAudit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 666)
        Me.Controls.Add(Me.TCEnforcement)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "NewSscpEnforcementAudit"
        Me.Text = "New SSCP Enforcement Audit"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCEnforcement.ResumeLayout(False)
        Me.TPGeneralInfo.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TPPollutants.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TPLON.ResumeLayout(False)
        Me.TPLON.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.TPNOV.ResumeLayout(False)
        Me.TPNOV.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.TPCO.ResumeLayout(False)
        Me.TPCO.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.StipulatedPenalties.ResumeLayout(False)
        Me.StipulatedPenalties.PerformLayout()
        CType(Me.dgvStipulatedPenalties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPAO.ResumeLayout(False)
        Me.TPAO.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.TPAuditHistory.ResumeLayout(False)
        CType(Me.dgvAuditHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.tabDocuments.ResumeLayout(False)
        Me.tabDocuments.PerformLayout()
        Me.pnlAddNew.ResumeLayout(False)
        Me.pnlAddNew.PerformLayout()
        Me.pnlUpdate.ResumeLayout(False)
        Me.pnlUpdate.PerformLayout()
        CType(Me.dgvFileList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mmiClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TCEnforcement As System.Windows.Forms.TabControl
    Friend WithEvents TPGeneralInfo As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAFSAOResolvedActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtAFSNOVResolvedNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAFSCivilCourtActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAFSKeyActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtAFSNOVActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAFSAOToAGActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtAFSCOProposedActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtStipulatedPenalitiesActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtAFSCOExecutedActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAFSCOResolvedActionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents btnLinkEnforcement As System.Windows.Forms.Button
    Friend WithEvents btn45DayZero As System.Windows.Forms.Button
    Friend WithEvents DTPEnforcementResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbHPV As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboHPVType As System.Windows.Forms.ComboBox
    Friend WithEvents DTPDiscoveryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbAO As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chbCO As System.Windows.Forms.CheckBox
    Friend WithEvents DTPDayZero As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbNOV As System.Windows.Forms.CheckBox
    Friend WithEvents lblDayZero As System.Windows.Forms.Label
    Friend WithEvents chbLON As System.Windows.Forms.CheckBox
    Friend WithEvents lblDiscoveryEvent As System.Windows.Forms.Label
    Friend WithEvents btnOpenEvent As System.Windows.Forms.Button
    Friend WithEvents txtDiscoveryEventNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnSubmitToUC As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents btnSubmitEnforcementToEPA As System.Windows.Forms.Button
    Friend WithEvents txtGeneralComments As System.Windows.Forms.TextBox
    Friend WithEvents cboStaffResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents btnManuallyEnterAFS As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TPPollutants As System.Windows.Forms.TabPage
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lvPollutants As System.Windows.Forms.ListView
    Friend WithEvents btnEditAirProgramPollutants As System.Windows.Forms.Button
    Friend WithEvents cboPollutantStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblPollutantStatus As System.Windows.Forms.Label
    Friend WithEvents lblPollutants As System.Windows.Forms.Label
    Friend WithEvents TPLON As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DTPLONResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents DTPLONToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtLONComments As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DTPLONSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents TPNOV As System.Windows.Forms.TabPage
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents DTPNFAToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents DTPNFAToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents DTPNOVToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents DTPNOVToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DTPNFALetterSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DTPNOVResponseReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DTPNOVsent As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNOVComments As System.Windows.Forms.TextBox
    Friend WithEvents TPCO As System.Windows.Forms.TabPage
    Friend WithEvents btnDownloadCO As System.Windows.Forms.Button
    Friend WithEvents btnUploadCO As System.Windows.Forms.Button
    Friend WithEvents txtCONumber As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents DTPCOToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents DTPCOToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents ClearStipulatedPenaltyFormButton As System.Windows.Forms.Button
    Friend WithEvents dgvStipulatedPenalties As System.Windows.Forms.DataGridView
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents DTPCOResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents DTPCOReceivedfromCompany As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents DTPCOExecuted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DTPCOReceivedfromDirector As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DTPCOProposed As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCOComments As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtCOPenaltyAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtPenaltyComments As System.Windows.Forms.TextBox
    Friend WithEvents SaveStipulatedPenaltyButton As System.Windows.Forms.Button
    Friend WithEvents txtStipulatedComments As System.Windows.Forms.TextBox
    Friend WithEvents txtStipulatedKey As System.Windows.Forms.TextBox
    Friend WithEvents txtStipulatedPenalty As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TPAO As System.Windows.Forms.TabPage
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents DTPAOResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents DTPAOAppealed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents DTPAOExecuted As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAOComments As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtCounty As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chbAPCI As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCF As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCA As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCV As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCM As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC8 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC0 As System.Windows.Forms.CheckBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtClassification As System.Windows.Forms.TextBox
    Friend WithEvents DTPLastSave As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblLastEdited As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents txtSubmitToUC As System.Windows.Forms.TextBox
    Friend WithEvents TPAuditHistory As System.Windows.Forms.TabPage
    Friend WithEvents mmiShowAuditHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvAuditHistory As System.Windows.Forms.DataGridView
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnHideAudit As System.Windows.Forms.Button
    Friend WithEvents btnExportAuditToExcel As System.Windows.Forms.Button
    Friend WithEvents btnREfreshAudit As System.Windows.Forms.Button
    Friend WithEvents UpdateStipulatedPenaltyButton As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblCODownload As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblNFAdownload As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents btnDownloadNFA As System.Windows.Forms.Button
    Friend WithEvents btnUploadNFA As System.Windows.Forms.Button
    Friend WithEvents lblDownloadAO As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents btnDownloadAO As System.Windows.Forms.Button
    Friend WithEvents btnUploadAO As System.Windows.Forms.Button
    Friend WithEvents StipulatedPenalties As System.Windows.Forms.GroupBox
    Friend WithEvents DeletePenaltyButton As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabDocuments As System.Windows.Forms.TabPage
    Friend WithEvents lblCurrentFiles As System.Windows.Forms.Label
    Friend WithEvents dgvFileList As System.Windows.Forms.DataGridView
    Friend WithEvents pnlUpdate As System.Windows.Forms.Panel
    Friend WithEvents lblUpdateDescription As System.Windows.Forms.Label
    Friend WithEvents ddlUpdateDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents btnDeleteFile As System.Windows.Forms.Button
    Friend WithEvents btnDownloadFile As System.Windows.Forms.Button
    Friend WithEvents txtUpdateDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdateFileDescription As System.Windows.Forms.Button
    Friend WithEvents lblSelectedFileName As System.Windows.Forms.Label
    Friend WithEvents pnlAddNew As System.Windows.Forms.Panel
    Friend WithEvents btnNewFileCancel As System.Windows.Forms.Button
    Friend WithEvents btnNewFileUpload As System.Windows.Forms.Button
    Friend WithEvents txtNewDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblNewDescription As System.Windows.Forms.Label
    Friend WithEvents lblNewFileName As System.Windows.Forms.Label
    Friend WithEvents btnChooseNewFile As System.Windows.Forms.Button
    Friend WithEvents lblDocumentTypes As System.Windows.Forms.Label
    Friend WithEvents ddlNewDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
End Class
