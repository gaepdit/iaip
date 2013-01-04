<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPInventory
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPInventory))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TCComputerInventory = New System.Windows.Forms.TabControl
        Me.TPInvenotry = New System.Windows.Forms.TabPage
        Me.dgvInventory = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblAssetProgramUnit = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtCurrentStaff = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rdbInactiveInventory = New System.Windows.Forms.RadioButton
        Me.rdbActiveInventory = New System.Windows.Forms.RadioButton
        Me.btnClearAsset = New System.Windows.Forms.Button
        Me.btnClearAssetID = New System.Windows.Forms.Button
        Me.btnDeleteAsset = New System.Windows.Forms.Button
        Me.btnUpdateAsset = New System.Windows.Forms.Button
        Me.gbInventorySearch = New System.Windows.Forms.GroupBox
        Me.btnExportInventory = New System.Windows.Forms.Button
        Me.Label28 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rdbInactiveSearch = New System.Windows.Forms.RadioButton
        Me.rdbBothSearch = New System.Windows.Forms.RadioButton
        Me.rdbActiveSearch = New System.Windows.Forms.RadioButton
        Me.txtInventoryCount = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.btnSearchForAsset = New System.Windows.Forms.Button
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.DTPDatAcquiredEnd = New System.Windows.Forms.DateTimePicker
        Me.Label16 = New System.Windows.Forms.Label
        Me.DTPDatAcquiredStart = New System.Windows.Forms.DateTimePicker
        Me.cboSearchByStaff = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtDecalSearch = New System.Windows.Forms.TextBox
        Me.cboInventorySearch = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.mtbReplacementDate = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtPOID = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSericalID = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCost = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtAssetID = New System.Windows.Forms.TextBox
        Me.cboInventoryType = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.btnAddNewAsset = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.DTPDateAssetAcquired = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Description = New System.Windows.Forms.Label
        Me.txtDNRDecal = New System.Windows.Forms.TextBox
        Me.TPTransactions = New System.Windows.Forms.TabPage
        Me.dgvTransactions = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label40 = New System.Windows.Forms.Label
        Me.txtDNRDecalTransaction = New System.Windows.Forms.TextBox
        Me.btnClearTransaction = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnExportTransactions = New System.Windows.Forms.Button
        Me.txtTransactionCount = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtTransactionIDSearch = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.btnTransactionSearch = New System.Windows.Forms.Button
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label31 = New System.Windows.Forms.Label
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.cboTransactionStaffSearch = New System.Windows.Forms.ComboBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtDecalSearch2 = New System.Windows.Forms.TextBox
        Me.cboTransactionTypeSearch = New System.Windows.Forms.ComboBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.btnClearTransactionID = New System.Windows.Forms.Button
        Me.Label27 = New System.Windows.Forms.Label
        Me.DTPTransactionDate = New System.Windows.Forms.DateTimePicker
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rdbSecondaryUse = New System.Windows.Forms.RadioButton
        Me.rdbPrimaryUse = New System.Windows.Forms.RadioButton
        Me.btnDeleteTransaction = New System.Windows.Forms.Button
        Me.btnEditTransaction = New System.Windows.Forms.Button
        Me.btnAddTransaction = New System.Windows.Forms.Button
        Me.lblTransactionProgramUnit = New System.Windows.Forms.Label
        Me.txtTransactionComments = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.chbReplacementOrdered = New System.Windows.Forms.CheckBox
        Me.cboStaff = New System.Windows.Forms.ComboBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtTransactionID = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtAssetTransaction = New System.Windows.Forms.TextBox
        Me.cboTransactionType = New System.Windows.Forms.ComboBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TPReports = New System.Windows.Forms.TabPage
        Me.TCReport = New System.Windows.Forms.TabControl
        Me.TPGridView = New System.Windows.Forms.TabPage
        Me.dgvReport = New System.Windows.Forms.DataGridView
        Me.TPCrystalReport = New System.Windows.Forms.TabPage
        Me.CRInventoryReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rdbUnassignedInventory = New System.Windows.Forms.RadioButton
        Me.rdbAssignedInventory = New System.Windows.Forms.RadioButton
        Me.cboReportInventoryType = New System.Windows.Forms.ComboBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.btnViewInventoryReport = New System.Windows.Forms.Button
        Me.cboReportUnit = New System.Windows.Forms.ComboBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.cboReportProgram = New System.Windows.Forms.ComboBox
        Me.btnViewReport = New System.Windows.Forms.Button
        Me.Label24 = New System.Windows.Forms.Label
        Me.TPListTools = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnClearTransactionType = New System.Windows.Forms.Button
        Me.txtTransactionIDValue = New System.Windows.Forms.TextBox
        Me.btnEditTransactionType = New System.Windows.Forms.Button
        Me.btnAddTransactionType = New System.Windows.Forms.Button
        Me.btnDeleteTransactionType = New System.Windows.Forms.Button
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtNewTransactionType = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.cboExistingTransactionTypes = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtInventoryID = New System.Windows.Forms.TextBox
        Me.btnEditType = New System.Windows.Forms.Button
        Me.btnAddNewType = New System.Windows.Forms.Button
        Me.btnDeleteInventory = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtNewInventoryType = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cboExistingInventoryTypes = New System.Windows.Forms.ComboBox
        Me.TPGAITInventory = New System.Windows.Forms.TabPage
        Me.dgvGAITInventory = New System.Windows.Forms.DataGridView
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.Panel17 = New System.Windows.Forms.Panel
        Me.rdbDeleted = New System.Windows.Forms.RadioButton
        Me.rdbActive = New System.Windows.Forms.RadioButton
        Me.txtGAITComment = New System.Windows.Forms.TextBox
        Me.Label56 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtGAITCommentSearch = New System.Windows.Forms.TextBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.clbAssestCategory = New System.Windows.Forms.CheckedListBox
        Me.Label63 = New System.Windows.Forms.Label
        Me.chbGAITUseDate = New System.Windows.Forms.CheckBox
        Me.pnlGAITDateSearch = New System.Windows.Forms.Panel
        Me.rdbGAITDateAquired = New System.Windows.Forms.RadioButton
        Me.rdbGAITDatePurchased = New System.Windows.Forms.RadioButton
        Me.btnExportGAIT = New System.Windows.Forms.Button
        Me.Label60 = New System.Windows.Forms.Label
        Me.txtGAITCount = New System.Windows.Forms.TextBox
        Me.Label57 = New System.Windows.Forms.Label
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.rdbGAITInactive = New System.Windows.Forms.RadioButton
        Me.rdbGAITBoth = New System.Windows.Forms.RadioButton
        Me.rdbGAITActive = New System.Windows.Forms.RadioButton
        Me.btnSearchGAITAssets = New System.Windows.Forms.Button
        Me.Label69 = New System.Windows.Forms.Label
        Me.Label70 = New System.Windows.Forms.Label
        Me.dtpGAITDateEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpGAITDateStart = New System.Windows.Forms.DateTimePicker
        Me.cboGAITProgram = New System.Windows.Forms.ComboBox
        Me.Label72 = New System.Windows.Forms.Label
        Me.txtGAITAssetSearch = New System.Windows.Forms.TextBox
        Me.Label74 = New System.Windows.Forms.Label
        Me.btnDeleteGAITAsset = New System.Windows.Forms.Button
        Me.btnSaveGAITAsset = New System.Windows.Forms.Button
        Me.btnAddNewGAITAsset = New System.Windows.Forms.Button
        Me.cboGAITModelNumber = New System.Windows.Forms.ComboBox
        Me.cboGAITIAIPUser = New System.Windows.Forms.ComboBox
        Me.txtGAITTimeInService = New System.Windows.Forms.TextBox
        Me.txtGAITPurchaseOrder = New System.Windows.Forms.TextBox
        Me.txtGAITSerial = New System.Windows.Forms.TextBox
        Me.dtpGAITAquired = New System.Windows.Forms.DateTimePicker
        Me.cboGAITQuality = New System.Windows.Forms.ComboBox
        Me.cboGAITModel = New System.Windows.Forms.ComboBox
        Me.cboGAITManufacturer = New System.Windows.Forms.ComboBox
        Me.dtpGAITPurchased = New System.Windows.Forms.DateTimePicker
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.btnClearGAIT = New System.Windows.Forms.Button
        Me.Label41 = New System.Windows.Forms.Label
        Me.txtGAITAssetTag = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.txtGAITID = New System.Windows.Forms.TextBox
        Me.cboGAITCategory = New System.Windows.Forms.ComboBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.TPGAITLists = New System.Windows.Forms.TabPage
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.gbGAITQuality = New System.Windows.Forms.GroupBox
        Me.rdbGAITQualityActive = New System.Windows.Forms.RadioButton
        Me.btnClearGaitQuality = New System.Windows.Forms.Button
        Me.txtGAITQualityID = New System.Windows.Forms.TextBox
        Me.btnEditGAITQuality = New System.Windows.Forms.Button
        Me.btnAddGAITQuality = New System.Windows.Forms.Button
        Me.btnDeleteGAITQuality = New System.Windows.Forms.Button
        Me.Label67 = New System.Windows.Forms.Label
        Me.txtAddEditGAITQuality = New System.Windows.Forms.TextBox
        Me.Label68 = New System.Windows.Forms.Label
        Me.cboExistingGAITQuality = New System.Windows.Forms.ComboBox
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.btnLoadGAITQuality = New System.Windows.Forms.Button
        Me.gbGAITModelNumber = New System.Windows.Forms.GroupBox
        Me.rdbGAITModelNumberActive = New System.Windows.Forms.RadioButton
        Me.btnClearGaitModelNumber = New System.Windows.Forms.Button
        Me.txtGAITModelNumberID = New System.Windows.Forms.TextBox
        Me.btnEditGAITModelNumber = New System.Windows.Forms.Button
        Me.btnAddGAITModelNumber = New System.Windows.Forms.Button
        Me.btnDeleteGAITModelNumber = New System.Windows.Forms.Button
        Me.Label64 = New System.Windows.Forms.Label
        Me.txtAddEditGAITModelNumber = New System.Windows.Forms.TextBox
        Me.Label65 = New System.Windows.Forms.Label
        Me.cboExistingGAITModelNumber = New System.Windows.Forms.ComboBox
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnLoadGAITModelNumber = New System.Windows.Forms.Button
        Me.gbGAITModel = New System.Windows.Forms.GroupBox
        Me.rdbGAITModelActive = New System.Windows.Forms.RadioButton
        Me.btnClearGaitModel = New System.Windows.Forms.Button
        Me.txtGAITModelID = New System.Windows.Forms.TextBox
        Me.btnEditGAITModel = New System.Windows.Forms.Button
        Me.btnAddGAITModel = New System.Windows.Forms.Button
        Me.btnDeleteGAITModel = New System.Windows.Forms.Button
        Me.Label61 = New System.Windows.Forms.Label
        Me.txtAddEditGAITModel = New System.Windows.Forms.TextBox
        Me.Label62 = New System.Windows.Forms.Label
        Me.cboExistingGAITModel = New System.Windows.Forms.ComboBox
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.btnLoadGAITModel = New System.Windows.Forms.Button
        Me.gbGAITManufacturer = New System.Windows.Forms.GroupBox
        Me.rdbGAITManufacturerActive = New System.Windows.Forms.RadioButton
        Me.btnClearGaitManufacturer = New System.Windows.Forms.Button
        Me.txtGAITManufacturerID = New System.Windows.Forms.TextBox
        Me.btnEditGAITManufacturer = New System.Windows.Forms.Button
        Me.btnAddGAITManufaturer = New System.Windows.Forms.Button
        Me.btnDeleteGAITManufaturer = New System.Windows.Forms.Button
        Me.Label58 = New System.Windows.Forms.Label
        Me.txtAddEditGAITManufacturer = New System.Windows.Forms.TextBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.cboExistingGAITManufacturer = New System.Windows.Forms.ComboBox
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.btnLoadGAITManufacturer = New System.Windows.Forms.Button
        Me.gbGAITCategory = New System.Windows.Forms.GroupBox
        Me.rdbGAITCategoryActive = New System.Windows.Forms.RadioButton
        Me.btnClearGaitCategory = New System.Windows.Forms.Button
        Me.txtGAITCategoryID = New System.Windows.Forms.TextBox
        Me.btnEditGAITCategory = New System.Windows.Forms.Button
        Me.btnAddGAITCategory = New System.Windows.Forms.Button
        Me.btnDeleteGAITCategory = New System.Windows.Forms.Button
        Me.Label54 = New System.Windows.Forms.Label
        Me.txtAddEditGAITCategory = New System.Windows.Forms.TextBox
        Me.Label55 = New System.Windows.Forms.Label
        Me.cboExistingGAITCategory = New System.Windows.Forms.ComboBox
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.btnLoadGAITCategory = New System.Windows.Forms.Button
        Me.Panel16 = New System.Windows.Forms.Panel
        Me.btnRefreshGAITDropdowns = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.bgwInventory = New System.ComponentModel.BackgroundWorker
        Me.bgwTransactions = New System.ComponentModel.BackgroundWorker
        Me.bgwReports = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCComputerInventory.SuspendLayout()
        Me.TPInvenotry.SuspendLayout()
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.gbInventorySearch.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TPTransactions.SuspendLayout()
        CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TPReports.SuspendLayout()
        Me.TCReport.SuspendLayout()
        Me.TPGridView.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPCrystalReport.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TPListTools.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TPGAITInventory.SuspendLayout()
        CType(Me.dgvGAITInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.pnlGAITDateSearch.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.TPGAITLists.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.gbGAITQuality.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.gbGAITModelNumber.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.gbGAITModel.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.gbGAITManufacturer.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.gbGAITCategory.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 724)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 10
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pnl1
        '
        Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(769, 17)
        Me.pnl1.Spring = True
        Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl2
        '
        Me.pnl2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(4, 17)
        '
        'pnl3
        '
        Me.pnl3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(4, 17)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'TCComputerInventory
        '
        Me.TCComputerInventory.Controls.Add(Me.TPInvenotry)
        Me.TCComputerInventory.Controls.Add(Me.TPTransactions)
        Me.TCComputerInventory.Controls.Add(Me.TPReports)
        Me.TCComputerInventory.Controls.Add(Me.TPListTools)
        Me.TCComputerInventory.Controls.Add(Me.TPGAITInventory)
        Me.TCComputerInventory.Controls.Add(Me.TPGAITLists)
        Me.TCComputerInventory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCComputerInventory.Location = New System.Drawing.Point(0, 24)
        Me.TCComputerInventory.Name = "TCComputerInventory"
        Me.TCComputerInventory.SelectedIndex = 0
        Me.TCComputerInventory.Size = New System.Drawing.Size(792, 700)
        Me.TCComputerInventory.TabIndex = 11
        '
        'TPInvenotry
        '
        Me.TPInvenotry.Controls.Add(Me.dgvInventory)
        Me.TPInvenotry.Controls.Add(Me.Panel1)
        Me.TPInvenotry.Location = New System.Drawing.Point(4, 22)
        Me.TPInvenotry.Name = "TPInvenotry"
        Me.TPInvenotry.Padding = New System.Windows.Forms.Padding(3)
        Me.TPInvenotry.Size = New System.Drawing.Size(784, 674)
        Me.TPInvenotry.TabIndex = 0
        Me.TPInvenotry.Text = "Inventory"
        Me.TPInvenotry.UseVisualStyleBackColor = True
        '
        'dgvInventory
        '
        Me.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInventory.Location = New System.Drawing.Point(3, 340)
        Me.dgvInventory.Name = "dgvInventory"
        Me.dgvInventory.ReadOnly = True
        Me.dgvInventory.Size = New System.Drawing.Size(778, 331)
        Me.dgvInventory.TabIndex = 38
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.lblAssetProgramUnit)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.txtCurrentStaff)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.btnClearAsset)
        Me.Panel1.Controls.Add(Me.btnClearAssetID)
        Me.Panel1.Controls.Add(Me.btnDeleteAsset)
        Me.Panel1.Controls.Add(Me.btnUpdateAsset)
        Me.Panel1.Controls.Add(Me.gbInventorySearch)
        Me.Panel1.Controls.Add(Me.mtbReplacementDate)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtPOID)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtSericalID)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCost)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtAssetID)
        Me.Panel1.Controls.Add(Me.cboInventoryType)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.btnAddNewAsset)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.DTPDateAssetAcquired)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Description)
        Me.Panel1.Controls.Add(Me.txtDNRDecal)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(778, 337)
        Me.Panel1.TabIndex = 37
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(412, 64)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(84, 13)
        Me.Label22.TabIndex = 78
        Me.Label22.Text = "Inventory Status"
        '
        'lblAssetProgramUnit
        '
        Me.lblAssetProgramUnit.AutoSize = True
        Me.lblAssetProgramUnit.Location = New System.Drawing.Point(254, 89)
        Me.lblAssetProgramUnit.Name = "lblAssetProgramUnit"
        Me.lblAssetProgramUnit.Size = New System.Drawing.Size(70, 13)
        Me.lblAssetProgramUnit.TabIndex = 77
        Me.lblAssetProgramUnit.Text = "Program/Unit"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(17, 89)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(66, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Current Staff"
        '
        'txtCurrentStaff
        '
        Me.txtCurrentStaff.Location = New System.Drawing.Point(89, 85)
        Me.txtCurrentStaff.Name = "txtCurrentStaff"
        Me.txtCurrentStaff.ReadOnly = True
        Me.txtCurrentStaff.Size = New System.Drawing.Size(159, 20)
        Me.txtCurrentStaff.TabIndex = 75
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.rdbInactiveInventory)
        Me.Panel4.Controls.Add(Me.rdbActiveInventory)
        Me.Panel4.Location = New System.Drawing.Point(499, 59)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(126, 23)
        Me.Panel4.TabIndex = 74
        '
        'rdbInactiveInventory
        '
        Me.rdbInactiveInventory.AutoSize = True
        Me.rdbInactiveInventory.Location = New System.Drawing.Point(60, 3)
        Me.rdbInactiveInventory.Name = "rdbInactiveInventory"
        Me.rdbInactiveInventory.Size = New System.Drawing.Size(63, 17)
        Me.rdbInactiveInventory.TabIndex = 1
        Me.rdbInactiveInventory.TabStop = True
        Me.rdbInactiveInventory.Text = "Inactive"
        Me.rdbInactiveInventory.UseVisualStyleBackColor = True
        '
        'rdbActiveInventory
        '
        Me.rdbActiveInventory.AutoSize = True
        Me.rdbActiveInventory.Location = New System.Drawing.Point(3, 3)
        Me.rdbActiveInventory.Name = "rdbActiveInventory"
        Me.rdbActiveInventory.Size = New System.Drawing.Size(55, 17)
        Me.rdbActiveInventory.TabIndex = 0
        Me.rdbActiveInventory.TabStop = True
        Me.rdbActiveInventory.Text = "Active"
        Me.rdbActiveInventory.UseVisualStyleBackColor = True
        '
        'btnClearAsset
        '
        Me.btnClearAsset.AutoSize = True
        Me.btnClearAsset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAsset.Location = New System.Drawing.Point(639, 8)
        Me.btnClearAsset.Name = "btnClearAsset"
        Me.btnClearAsset.Size = New System.Drawing.Size(70, 23)
        Me.btnClearAsset.TabIndex = 61
        Me.btnClearAsset.Text = "Clear Asset"
        Me.btnClearAsset.UseVisualStyleBackColor = True
        '
        'btnClearAssetID
        '
        Me.btnClearAssetID.BackgroundImage = CType(resources.GetObject("btnClearAssetID.BackgroundImage"), System.Drawing.Image)
        Me.btnClearAssetID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClearAssetID.Location = New System.Drawing.Point(191, 8)
        Me.btnClearAssetID.Name = "btnClearAssetID"
        Me.btnClearAssetID.Size = New System.Drawing.Size(20, 23)
        Me.btnClearAssetID.TabIndex = 60
        Me.ToolTip1.SetToolTip(Me.btnClearAssetID, "Clear Asset ID ONLY")
        Me.btnClearAssetID.UseVisualStyleBackColor = True
        '
        'btnDeleteAsset
        '
        Me.btnDeleteAsset.AutoSize = True
        Me.btnDeleteAsset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteAsset.Location = New System.Drawing.Point(632, 189)
        Me.btnDeleteAsset.Name = "btnDeleteAsset"
        Me.btnDeleteAsset.Size = New System.Drawing.Size(77, 23)
        Me.btnDeleteAsset.TabIndex = 59
        Me.btnDeleteAsset.Text = "Delete Asset"
        Me.btnDeleteAsset.UseVisualStyleBackColor = True
        '
        'btnUpdateAsset
        '
        Me.btnUpdateAsset.AutoSize = True
        Me.btnUpdateAsset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateAsset.Location = New System.Drawing.Point(194, 189)
        Me.btnUpdateAsset.Name = "btnUpdateAsset"
        Me.btnUpdateAsset.Size = New System.Drawing.Size(64, 23)
        Me.btnUpdateAsset.TabIndex = 58
        Me.btnUpdateAsset.Text = "Edit Asset"
        Me.btnUpdateAsset.UseVisualStyleBackColor = True
        '
        'gbInventorySearch
        '
        Me.gbInventorySearch.Controls.Add(Me.btnExportInventory)
        Me.gbInventorySearch.Controls.Add(Me.Label28)
        Me.gbInventorySearch.Controls.Add(Me.Panel5)
        Me.gbInventorySearch.Controls.Add(Me.txtInventoryCount)
        Me.gbInventorySearch.Controls.Add(Me.Label36)
        Me.gbInventorySearch.Controls.Add(Me.btnSearchForAsset)
        Me.gbInventorySearch.Controls.Add(Me.Label18)
        Me.gbInventorySearch.Controls.Add(Me.Label17)
        Me.gbInventorySearch.Controls.Add(Me.DTPDatAcquiredEnd)
        Me.gbInventorySearch.Controls.Add(Me.Label16)
        Me.gbInventorySearch.Controls.Add(Me.DTPDatAcquiredStart)
        Me.gbInventorySearch.Controls.Add(Me.cboSearchByStaff)
        Me.gbInventorySearch.Controls.Add(Me.Label14)
        Me.gbInventorySearch.Controls.Add(Me.txtDecalSearch)
        Me.gbInventorySearch.Controls.Add(Me.cboInventorySearch)
        Me.gbInventorySearch.Controls.Add(Me.Label12)
        Me.gbInventorySearch.Controls.Add(Me.Label11)
        Me.gbInventorySearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbInventorySearch.Location = New System.Drawing.Point(0, 221)
        Me.gbInventorySearch.Name = "gbInventorySearch"
        Me.gbInventorySearch.Size = New System.Drawing.Size(778, 116)
        Me.gbInventorySearch.TabIndex = 57
        Me.gbInventorySearch.TabStop = False
        Me.gbInventorySearch.Text = "Search Fields"
        '
        'btnExportInventory
        '
        Me.btnExportInventory.AutoSize = True
        Me.btnExportInventory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportInventory.Location = New System.Drawing.Point(651, 90)
        Me.btnExportInventory.Name = "btnExportInventory"
        Me.btnExportInventory.Size = New System.Drawing.Size(94, 23)
        Me.btnExportInventory.TabIndex = 80
        Me.btnExportInventory.Text = "Export To Excell"
        Me.btnExportInventory.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(410, 52)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(84, 13)
        Me.Label28.TabIndex = 79
        Me.Label28.Text = "Inventory Status"
        '
        'Panel5
        '
        Me.Panel5.AutoSize = True
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.Controls.Add(Me.rdbInactiveSearch)
        Me.Panel5.Controls.Add(Me.rdbBothSearch)
        Me.Panel5.Controls.Add(Me.rdbActiveSearch)
        Me.Panel5.Location = New System.Drawing.Point(500, 49)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(69, 53)
        Me.Panel5.TabIndex = 75
        '
        'rdbInactiveSearch
        '
        Me.rdbInactiveSearch.AutoSize = True
        Me.rdbInactiveSearch.Location = New System.Drawing.Point(3, 18)
        Me.rdbInactiveSearch.Name = "rdbInactiveSearch"
        Me.rdbInactiveSearch.Size = New System.Drawing.Size(63, 17)
        Me.rdbInactiveSearch.TabIndex = 2
        Me.rdbInactiveSearch.Text = "Inactive"
        Me.rdbInactiveSearch.UseVisualStyleBackColor = True
        '
        'rdbBothSearch
        '
        Me.rdbBothSearch.AutoSize = True
        Me.rdbBothSearch.Location = New System.Drawing.Point(3, 33)
        Me.rdbBothSearch.Name = "rdbBothSearch"
        Me.rdbBothSearch.Size = New System.Drawing.Size(47, 17)
        Me.rdbBothSearch.TabIndex = 1
        Me.rdbBothSearch.Text = "Both"
        Me.rdbBothSearch.UseVisualStyleBackColor = True
        '
        'rdbActiveSearch
        '
        Me.rdbActiveSearch.AutoSize = True
        Me.rdbActiveSearch.Checked = True
        Me.rdbActiveSearch.Location = New System.Drawing.Point(3, 3)
        Me.rdbActiveSearch.Name = "rdbActiveSearch"
        Me.rdbActiveSearch.Size = New System.Drawing.Size(55, 17)
        Me.rdbActiveSearch.TabIndex = 0
        Me.rdbActiveSearch.TabStop = True
        Me.rdbActiveSearch.Text = "Active"
        Me.rdbActiveSearch.UseVisualStyleBackColor = True
        '
        'txtInventoryCount
        '
        Me.txtInventoryCount.Location = New System.Drawing.Point(692, 62)
        Me.txtInventoryCount.Name = "txtInventoryCount"
        Me.txtInventoryCount.Size = New System.Drawing.Size(53, 20)
        Me.txtInventoryCount.TabIndex = 66
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(651, 65)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(35, 13)
        Me.Label36.TabIndex = 65
        Me.Label36.Text = "Count"
        '
        'btnSearchForAsset
        '
        Me.btnSearchForAsset.AutoSize = True
        Me.btnSearchForAsset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchForAsset.Location = New System.Drawing.Point(639, 23)
        Me.btnSearchForAsset.Name = "btnSearchForAsset"
        Me.btnSearchForAsset.Size = New System.Drawing.Size(106, 23)
        Me.btnSearchForAsset.TabIndex = 64
        Me.btnSearchForAsset.Text = "Search for Asset(s)"
        Me.btnSearchForAsset.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(227, 77)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(26, 13)
        Me.Label18.TabIndex = 63
        Me.Label18.Text = "End"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(97, 77)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(29, 13)
        Me.Label17.TabIndex = 62
        Me.Label17.Text = "Start"
        '
        'DTPDatAcquiredEnd
        '
        Me.DTPDatAcquiredEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDatAcquiredEnd.Enabled = False
        Me.DTPDatAcquiredEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDatAcquiredEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDatAcquiredEnd.Location = New System.Drawing.Point(220, 52)
        Me.DTPDatAcquiredEnd.Name = "DTPDatAcquiredEnd"
        Me.DTPDatAcquiredEnd.Size = New System.Drawing.Size(105, 22)
        Me.DTPDatAcquiredEnd.TabIndex = 61
        Me.DTPDatAcquiredEnd.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 57)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 13)
        Me.Label16.TabIndex = 59
        Me.Label16.Text = "Date Acquired"
        '
        'DTPDatAcquiredStart
        '
        Me.DTPDatAcquiredStart.Checked = False
        Me.DTPDatAcquiredStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDatAcquiredStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDatAcquiredStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDatAcquiredStart.Location = New System.Drawing.Point(89, 52)
        Me.DTPDatAcquiredStart.Name = "DTPDatAcquiredStart"
        Me.DTPDatAcquiredStart.ShowCheckBox = True
        Me.DTPDatAcquiredStart.Size = New System.Drawing.Size(125, 22)
        Me.DTPDatAcquiredStart.TabIndex = 60
        Me.DTPDatAcquiredStart.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboSearchByStaff
        '
        Me.cboSearchByStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSearchByStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchByStaff.FormattingEnabled = True
        Me.cboSearchByStaff.Location = New System.Drawing.Point(482, 22)
        Me.cboSearchByStaff.Name = "cboSearchByStaff"
        Me.cboSearchByStaff.Size = New System.Drawing.Size(121, 21)
        Me.cboSearchByStaff.TabIndex = 58
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(410, 27)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 13)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Current Staff"
        '
        'txtDecalSearch
        '
        Me.txtDecalSearch.Location = New System.Drawing.Point(55, 19)
        Me.txtDecalSearch.Name = "txtDecalSearch"
        Me.txtDecalSearch.Size = New System.Drawing.Size(121, 20)
        Me.txtDecalSearch.TabIndex = 54
        '
        'cboInventorySearch
        '
        Me.cboInventorySearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboInventorySearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboInventorySearch.FormattingEnabled = True
        Me.cboInventorySearch.Location = New System.Drawing.Point(272, 19)
        Me.cboInventorySearch.Name = "cboInventorySearch"
        Me.cboInventorySearch.Size = New System.Drawing.Size(121, 21)
        Me.cboInventorySearch.TabIndex = 56
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(188, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 13)
        Me.Label12.TabIndex = 55
        Me.Label12.Text = "Inventory Type"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Decal #"
        '
        'mtbReplacementDate
        '
        Me.mtbReplacementDate.Location = New System.Drawing.Point(585, 35)
        Me.mtbReplacementDate.Mask = "0000"
        Me.mtbReplacementDate.Name = "mtbReplacementDate"
        Me.mtbReplacementDate.Size = New System.Drawing.Size(40, 20)
        Me.mtbReplacementDate.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(490, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 13)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Replacement Year"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(447, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Purchase Order Id"
        '
        'txtPOID
        '
        Me.txtPOID.Location = New System.Drawing.Point(546, 9)
        Me.txtPOID.Name = "txtPOID"
        Me.txtPOID.Size = New System.Drawing.Size(79, 20)
        Me.txtPOID.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(258, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Serial ID"
        '
        'txtSericalID
        '
        Me.txtSericalID.Location = New System.Drawing.Point(311, 60)
        Me.txtSericalID.Name = "txtSericalID"
        Me.txtSericalID.Size = New System.Drawing.Size(79, 20)
        Me.txtSericalID.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(55, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Cost"
        '
        'txtCost
        '
        Me.txtCost.Location = New System.Drawing.Point(89, 60)
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(79, 20)
        Me.txtCost.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Asset ID"
        '
        'txtAssetID
        '
        Me.txtAssetID.Location = New System.Drawing.Point(89, 9)
        Me.txtAssetID.Name = "txtAssetID"
        Me.txtAssetID.ReadOnly = True
        Me.txtAssetID.Size = New System.Drawing.Size(96, 20)
        Me.txtAssetID.TabIndex = 0
        '
        'cboInventoryType
        '
        Me.cboInventoryType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboInventoryType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboInventoryType.FormattingEnabled = True
        Me.cboInventoryType.Location = New System.Drawing.Point(89, 35)
        Me.cboInventoryType.Name = "cboInventoryType"
        Me.cboInventoryType.Size = New System.Drawing.Size(121, 21)
        Me.cboInventoryType.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(5, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 13)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "Inventory Type"
        '
        'btnAddNewAsset
        '
        Me.btnAddNewAsset.AutoSize = True
        Me.btnAddNewAsset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewAsset.Location = New System.Drawing.Point(89, 189)
        Me.btnAddNewAsset.Name = "btnAddNewAsset"
        Me.btnAddNewAsset.Size = New System.Drawing.Size(90, 23)
        Me.btnAddNewAsset.TabIndex = 11
        Me.btnAddNewAsset.Text = "Add New Asset"
        Me.btnAddNewAsset.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(230, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Date Acquired"
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.Location = New System.Drawing.Point(89, 112)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(620, 68)
        Me.txtDescription.TabIndex = 10
        '
        'DTPDateAssetAcquired
        '
        Me.DTPDateAssetAcquired.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateAssetAcquired.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateAssetAcquired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateAssetAcquired.Location = New System.Drawing.Point(311, 34)
        Me.DTPDateAssetAcquired.Name = "DTPDateAssetAcquired"
        Me.DTPDateAssetAcquired.Size = New System.Drawing.Size(108, 22)
        Me.DTPDateAssetAcquired.TabIndex = 3
        Me.DTPDateAssetAcquired.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(227, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "DNR Decal #"
        '
        'Description
        '
        Me.Description.AutoSize = True
        Me.Description.Location = New System.Drawing.Point(23, 115)
        Me.Description.Name = "Description"
        Me.Description.Size = New System.Drawing.Size(60, 13)
        Me.Description.TabIndex = 32
        Me.Description.Text = "Description"
        '
        'txtDNRDecal
        '
        Me.txtDNRDecal.Location = New System.Drawing.Point(311, 9)
        Me.txtDNRDecal.Name = "txtDNRDecal"
        Me.txtDNRDecal.Size = New System.Drawing.Size(121, 20)
        Me.txtDNRDecal.TabIndex = 1
        '
        'TPTransactions
        '
        Me.TPTransactions.Controls.Add(Me.dgvTransactions)
        Me.TPTransactions.Controls.Add(Me.Panel2)
        Me.TPTransactions.Location = New System.Drawing.Point(4, 22)
        Me.TPTransactions.Name = "TPTransactions"
        Me.TPTransactions.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTransactions.Size = New System.Drawing.Size(784, 694)
        Me.TPTransactions.TabIndex = 1
        Me.TPTransactions.Text = "Transactions"
        Me.TPTransactions.UseVisualStyleBackColor = True
        '
        'dgvTransactions
        '
        Me.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransactions.Location = New System.Drawing.Point(3, 340)
        Me.dgvTransactions.Name = "dgvTransactions"
        Me.dgvTransactions.ReadOnly = True
        Me.dgvTransactions.Size = New System.Drawing.Size(778, 351)
        Me.dgvTransactions.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label40)
        Me.Panel2.Controls.Add(Me.txtDNRDecalTransaction)
        Me.Panel2.Controls.Add(Me.btnClearTransaction)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.btnClearTransactionID)
        Me.Panel2.Controls.Add(Me.Label27)
        Me.Panel2.Controls.Add(Me.DTPTransactionDate)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.btnDeleteTransaction)
        Me.Panel2.Controls.Add(Me.btnEditTransaction)
        Me.Panel2.Controls.Add(Me.btnAddTransaction)
        Me.Panel2.Controls.Add(Me.lblTransactionProgramUnit)
        Me.Panel2.Controls.Add(Me.txtTransactionComments)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.chbReplacementOrdered)
        Me.Panel2.Controls.Add(Me.cboStaff)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtTransactionID)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.txtAssetTransaction)
        Me.Panel2.Controls.Add(Me.cboTransactionType)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(778, 337)
        Me.Panel2.TabIndex = 0
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(193, 11)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(72, 13)
        Me.Label40.TabIndex = 82
        Me.Label40.Text = "DNR Decal #"
        '
        'txtDNRDecalTransaction
        '
        Me.txtDNRDecalTransaction.Location = New System.Drawing.Point(275, 7)
        Me.txtDNRDecalTransaction.Name = "txtDNRDecalTransaction"
        Me.txtDNRDecalTransaction.ReadOnly = True
        Me.txtDNRDecalTransaction.Size = New System.Drawing.Size(96, 20)
        Me.txtDNRDecalTransaction.TabIndex = 81
        '
        'btnClearTransaction
        '
        Me.btnClearTransaction.AutoSize = True
        Me.btnClearTransaction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearTransaction.Location = New System.Drawing.Point(610, 7)
        Me.btnClearTransaction.Name = "btnClearTransaction"
        Me.btnClearTransaction.Size = New System.Drawing.Size(100, 23)
        Me.btnClearTransaction.TabIndex = 80
        Me.btnClearTransaction.Text = "Clear Transaction"
        Me.btnClearTransaction.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnExportTransactions)
        Me.GroupBox3.Controls.Add(Me.txtTransactionCount)
        Me.GroupBox3.Controls.Add(Me.Label37)
        Me.GroupBox3.Controls.Add(Me.txtTransactionIDSearch)
        Me.GroupBox3.Controls.Add(Me.Label35)
        Me.GroupBox3.Controls.Add(Me.btnTransactionSearch)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox3.Controls.Add(Me.cboTransactionStaffSearch)
        Me.GroupBox3.Controls.Add(Me.Label32)
        Me.GroupBox3.Controls.Add(Me.txtDecalSearch2)
        Me.GroupBox3.Controls.Add(Me.cboTransactionTypeSearch)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label34)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(0, 207)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(778, 130)
        Me.GroupBox3.TabIndex = 79
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Search Fields"
        '
        'btnExportTransactions
        '
        Me.btnExportTransactions.AutoSize = True
        Me.btnExportTransactions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportTransactions.Location = New System.Drawing.Point(616, 102)
        Me.btnExportTransactions.Name = "btnExportTransactions"
        Me.btnExportTransactions.Size = New System.Drawing.Size(94, 23)
        Me.btnExportTransactions.TabIndex = 81
        Me.btnExportTransactions.Text = "Export To Excell"
        Me.btnExportTransactions.UseVisualStyleBackColor = True
        '
        'txtTransactionCount
        '
        Me.txtTransactionCount.Location = New System.Drawing.Point(657, 76)
        Me.txtTransactionCount.Name = "txtTransactionCount"
        Me.txtTransactionCount.Size = New System.Drawing.Size(53, 20)
        Me.txtTransactionCount.TabIndex = 68
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(611, 79)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(35, 13)
        Me.Label37.TabIndex = 67
        Me.Label37.Text = "Count"
        '
        'txtTransactionIDSearch
        '
        Me.txtTransactionIDSearch.Location = New System.Drawing.Point(83, 16)
        Me.txtTransactionIDSearch.Name = "txtTransactionIDSearch"
        Me.txtTransactionIDSearch.Size = New System.Drawing.Size(88, 20)
        Me.txtTransactionIDSearch.TabIndex = 66
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(6, 20)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(77, 13)
        Me.Label35.TabIndex = 65
        Me.Label35.Text = "Transaction ID"
        '
        'btnTransactionSearch
        '
        Me.btnTransactionSearch.AutoSize = True
        Me.btnTransactionSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnTransactionSearch.Location = New System.Drawing.Point(574, 47)
        Me.btnTransactionSearch.Name = "btnTransactionSearch"
        Me.btnTransactionSearch.Size = New System.Drawing.Size(136, 23)
        Me.btnTransactionSearch.TabIndex = 64
        Me.btnTransactionSearch.Text = "Search for Transaction(s)"
        Me.btnTransactionSearch.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(232, 77)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(26, 13)
        Me.Label29.TabIndex = 63
        Me.Label29.Text = "End"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(102, 77)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(29, 13)
        Me.Label30.TabIndex = 62
        Me.Label30.Text = "Start"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd-MMM-yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(225, 52)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(105, 22)
        Me.DateTimePicker1.TabIndex = 61
        Me.DateTimePicker1.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(13, 57)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(75, 13)
        Me.Label31.TabIndex = 59
        Me.Label31.Text = "Date Acquired"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Checked = False
        Me.DateTimePicker2.CustomFormat = "dd-MMM-yyyy"
        Me.DateTimePicker2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(94, 52)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.ShowCheckBox = True
        Me.DateTimePicker2.Size = New System.Drawing.Size(125, 22)
        Me.DateTimePicker2.TabIndex = 60
        Me.DateTimePicker2.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboTransactionStaffSearch
        '
        Me.cboTransactionStaffSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTransactionStaffSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTransactionStaffSearch.FormattingEnabled = True
        Me.cboTransactionStaffSearch.Location = New System.Drawing.Point(583, 16)
        Me.cboTransactionStaffSearch.Name = "cboTransactionStaffSearch"
        Me.cboTransactionStaffSearch.Size = New System.Drawing.Size(121, 21)
        Me.cboTransactionStaffSearch.TabIndex = 58
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(548, 20)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(29, 13)
        Me.Label32.TabIndex = 57
        Me.Label32.Text = "Staff"
        '
        'txtDecalSearch2
        '
        Me.txtDecalSearch2.Location = New System.Drawing.Point(235, 16)
        Me.txtDecalSearch2.Name = "txtDecalSearch2"
        Me.txtDecalSearch2.Size = New System.Drawing.Size(81, 20)
        Me.txtDecalSearch2.TabIndex = 54
        '
        'cboTransactionTypeSearch
        '
        Me.cboTransactionTypeSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTransactionTypeSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTransactionTypeSearch.FormattingEnabled = True
        Me.cboTransactionTypeSearch.Location = New System.Drawing.Point(422, 16)
        Me.cboTransactionTypeSearch.Name = "cboTransactionTypeSearch"
        Me.cboTransactionTypeSearch.Size = New System.Drawing.Size(121, 21)
        Me.cboTransactionTypeSearch.TabIndex = 56
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(326, 20)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(90, 13)
        Me.Label33.TabIndex = 55
        Me.Label33.Text = "Transaction Type"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(186, 20)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(45, 13)
        Me.Label34.TabIndex = 53
        Me.Label34.Text = "Decal #"
        '
        'btnClearTransactionID
        '
        Me.btnClearTransactionID.BackgroundImage = CType(resources.GetObject("btnClearTransactionID.BackgroundImage"), System.Drawing.Image)
        Me.btnClearTransactionID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClearTransactionID.Location = New System.Drawing.Point(170, 6)
        Me.btnClearTransactionID.Name = "btnClearTransactionID"
        Me.btnClearTransactionID.Size = New System.Drawing.Size(20, 23)
        Me.btnClearTransactionID.TabIndex = 78
        Me.ToolTip1.SetToolTip(Me.btnClearTransactionID, "Clear Transaction ID ONLY")
        Me.btnClearTransactionID.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(352, 37)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(89, 13)
        Me.Label27.TabIndex = 75
        Me.Label27.Text = "Transaction Date"
        '
        'DTPTransactionDate
        '
        Me.DTPTransactionDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTransactionDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTransactionDate.Location = New System.Drawing.Point(447, 32)
        Me.DTPTransactionDate.Name = "DTPTransactionDate"
        Me.DTPTransactionDate.Size = New System.Drawing.Size(108, 22)
        Me.DTPTransactionDate.TabIndex = 74
        Me.DTPTransactionDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.Controls.Add(Me.rdbSecondaryUse)
        Me.Panel3.Controls.Add(Me.rdbPrimaryUse)
        Me.Panel3.Location = New System.Drawing.Point(373, 58)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(191, 23)
        Me.Panel3.TabIndex = 73
        '
        'rdbSecondaryUse
        '
        Me.rdbSecondaryUse.AutoSize = True
        Me.rdbSecondaryUse.Location = New System.Drawing.Point(90, 2)
        Me.rdbSecondaryUse.Name = "rdbSecondaryUse"
        Me.rdbSecondaryUse.Size = New System.Drawing.Size(98, 17)
        Me.rdbSecondaryUse.TabIndex = 1
        Me.rdbSecondaryUse.TabStop = True
        Me.rdbSecondaryUse.Text = "Secondary Use"
        Me.rdbSecondaryUse.UseVisualStyleBackColor = True
        '
        'rdbPrimaryUse
        '
        Me.rdbPrimaryUse.AutoSize = True
        Me.rdbPrimaryUse.Location = New System.Drawing.Point(3, 3)
        Me.rdbPrimaryUse.Name = "rdbPrimaryUse"
        Me.rdbPrimaryUse.Size = New System.Drawing.Size(81, 17)
        Me.rdbPrimaryUse.TabIndex = 0
        Me.rdbPrimaryUse.TabStop = True
        Me.rdbPrimaryUse.Text = "Primary Use"
        Me.rdbPrimaryUse.UseVisualStyleBackColor = True
        '
        'btnDeleteTransaction
        '
        Me.btnDeleteTransaction.AutoSize = True
        Me.btnDeleteTransaction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteTransaction.Location = New System.Drawing.Point(603, 176)
        Me.btnDeleteTransaction.Name = "btnDeleteTransaction"
        Me.btnDeleteTransaction.Size = New System.Drawing.Size(107, 23)
        Me.btnDeleteTransaction.TabIndex = 71
        Me.btnDeleteTransaction.Text = "Delete Transaction"
        Me.btnDeleteTransaction.UseVisualStyleBackColor = True
        '
        'btnEditTransaction
        '
        Me.btnEditTransaction.AutoSize = True
        Me.btnEditTransaction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditTransaction.Location = New System.Drawing.Point(233, 176)
        Me.btnEditTransaction.Name = "btnEditTransaction"
        Me.btnEditTransaction.Size = New System.Drawing.Size(94, 23)
        Me.btnEditTransaction.TabIndex = 70
        Me.btnEditTransaction.Text = "Edit Transaction"
        Me.btnEditTransaction.UseVisualStyleBackColor = True
        '
        'btnAddTransaction
        '
        Me.btnAddTransaction.AutoSize = True
        Me.btnAddTransaction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddTransaction.Location = New System.Drawing.Point(96, 176)
        Me.btnAddTransaction.Name = "btnAddTransaction"
        Me.btnAddTransaction.Size = New System.Drawing.Size(120, 23)
        Me.btnAddTransaction.TabIndex = 69
        Me.btnAddTransaction.Text = "Add New Transaction"
        Me.btnAddTransaction.UseVisualStyleBackColor = True
        '
        'lblTransactionProgramUnit
        '
        Me.lblTransactionProgramUnit.AutoSize = True
        Me.lblTransactionProgramUnit.Location = New System.Drawing.Point(101, 84)
        Me.lblTransactionProgramUnit.Name = "lblTransactionProgramUnit"
        Me.lblTransactionProgramUnit.Size = New System.Drawing.Size(70, 13)
        Me.lblTransactionProgramUnit.TabIndex = 68
        Me.lblTransactionProgramUnit.Text = "Program/Unit"
        '
        'txtTransactionComments
        '
        Me.txtTransactionComments.AcceptsReturn = True
        Me.txtTransactionComments.Location = New System.Drawing.Point(101, 102)
        Me.txtTransactionComments.Multiline = True
        Me.txtTransactionComments.Name = "txtTransactionComments"
        Me.txtTransactionComments.Size = New System.Drawing.Size(603, 68)
        Me.txtTransactionComments.TabIndex = 66
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(17, 105)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(56, 13)
        Me.Label23.TabIndex = 67
        Me.Label23.Text = "Comments"
        '
        'chbReplacementOrdered
        '
        Me.chbReplacementOrdered.AutoSize = True
        Me.chbReplacementOrdered.Location = New System.Drawing.Point(580, 60)
        Me.chbReplacementOrdered.Name = "chbReplacementOrdered"
        Me.chbReplacementOrdered.Size = New System.Drawing.Size(130, 17)
        Me.chbReplacementOrdered.TabIndex = 65
        Me.chbReplacementOrdered.Text = "Replacement Ordered"
        Me.chbReplacementOrdered.UseVisualStyleBackColor = True
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(101, 60)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(157, 21)
        Me.cboStaff.TabIndex = 60
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(66, 64)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 13)
        Me.Label20.TabIndex = 59
        Me.Label20.Text = "Staff"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Transaction ID"
        '
        'txtTransactionID
        '
        Me.txtTransactionID.Location = New System.Drawing.Point(101, 7)
        Me.txtTransactionID.Name = "txtTransactionID"
        Me.txtTransactionID.ReadOnly = True
        Me.txtTransactionID.Size = New System.Drawing.Size(63, 20)
        Me.txtTransactionID.TabIndex = 44
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(394, 11)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 13)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Asset ID"
        '
        'txtAssetTransaction
        '
        Me.txtAssetTransaction.Location = New System.Drawing.Point(447, 7)
        Me.txtAssetTransaction.Name = "txtAssetTransaction"
        Me.txtAssetTransaction.Size = New System.Drawing.Size(96, 20)
        Me.txtAssetTransaction.TabIndex = 40
        '
        'cboTransactionType
        '
        Me.cboTransactionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTransactionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTransactionType.FormattingEnabled = True
        Me.cboTransactionType.Location = New System.Drawing.Point(101, 33)
        Me.cboTransactionType.Name = "cboTransactionType"
        Me.cboTransactionType.Size = New System.Drawing.Size(215, 21)
        Me.cboTransactionType.TabIndex = 41
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(5, 37)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(90, 13)
        Me.Label19.TabIndex = 42
        Me.Label19.Text = "Transaction Type"
        '
        'TPReports
        '
        Me.TPReports.Controls.Add(Me.TCReport)
        Me.TPReports.Controls.Add(Me.Panel6)
        Me.TPReports.Location = New System.Drawing.Point(4, 22)
        Me.TPReports.Name = "TPReports"
        Me.TPReports.Size = New System.Drawing.Size(784, 694)
        Me.TPReports.TabIndex = 3
        Me.TPReports.Text = "Reports"
        Me.TPReports.UseVisualStyleBackColor = True
        '
        'TCReport
        '
        Me.TCReport.Controls.Add(Me.TPGridView)
        Me.TCReport.Controls.Add(Me.TPCrystalReport)
        Me.TCReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCReport.Location = New System.Drawing.Point(0, 112)
        Me.TCReport.Name = "TCReport"
        Me.TCReport.SelectedIndex = 0
        Me.TCReport.Size = New System.Drawing.Size(784, 582)
        Me.TCReport.TabIndex = 2
        '
        'TPGridView
        '
        Me.TPGridView.Controls.Add(Me.dgvReport)
        Me.TPGridView.Location = New System.Drawing.Point(4, 22)
        Me.TPGridView.Name = "TPGridView"
        Me.TPGridView.Padding = New System.Windows.Forms.Padding(3)
        Me.TPGridView.Size = New System.Drawing.Size(776, 556)
        Me.TPGridView.TabIndex = 0
        Me.TPGridView.Text = "Raw Data"
        Me.TPGridView.UseVisualStyleBackColor = True
        '
        'dgvReport
        '
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReport.Location = New System.Drawing.Point(3, 3)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(770, 550)
        Me.dgvReport.TabIndex = 1
        '
        'TPCrystalReport
        '
        Me.TPCrystalReport.Controls.Add(Me.CRInventoryReport)
        Me.TPCrystalReport.Location = New System.Drawing.Point(4, 22)
        Me.TPCrystalReport.Name = "TPCrystalReport"
        Me.TPCrystalReport.Padding = New System.Windows.Forms.Padding(3)
        Me.TPCrystalReport.Size = New System.Drawing.Size(776, 556)
        Me.TPCrystalReport.TabIndex = 1
        Me.TPCrystalReport.Text = "Report"
        Me.TPCrystalReport.UseVisualStyleBackColor = True
        '
        'CRInventoryReport
        '
        Me.CRInventoryReport.ActiveViewIndex = -1
        Me.CRInventoryReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRInventoryReport.DisplayGroupTree = False
        Me.CRInventoryReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRInventoryReport.Location = New System.Drawing.Point(3, 3)
        Me.CRInventoryReport.Margin = New System.Windows.Forms.Padding(2)
        Me.CRInventoryReport.Name = "CRInventoryReport"
        Me.CRInventoryReport.SelectionFormula = ""
        Me.CRInventoryReport.Size = New System.Drawing.Size(770, 550)
        Me.CRInventoryReport.TabIndex = 270
        Me.CRInventoryReport.ViewTimeSelectionFormula = ""
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Controls.Add(Me.cboReportInventoryType)
        Me.Panel6.Controls.Add(Me.Label38)
        Me.Panel6.Controls.Add(Me.btnViewInventoryReport)
        Me.Panel6.Controls.Add(Me.cboReportUnit)
        Me.Panel6.Controls.Add(Me.Label39)
        Me.Panel6.Controls.Add(Me.cboReportProgram)
        Me.Panel6.Controls.Add(Me.btnViewReport)
        Me.Panel6.Controls.Add(Me.Label24)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(784, 112)
        Me.Panel6.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.AutoSize = True
        Me.Panel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel7.Controls.Add(Me.rdbUnassignedInventory)
        Me.Panel7.Controls.Add(Me.rdbAssignedInventory)
        Me.Panel7.Location = New System.Drawing.Point(261, 65)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(255, 23)
        Me.Panel7.TabIndex = 88
        '
        'rdbUnassignedInventory
        '
        Me.rdbUnassignedInventory.AutoSize = True
        Me.rdbUnassignedInventory.Location = New System.Drawing.Point(124, 3)
        Me.rdbUnassignedInventory.Name = "rdbUnassignedInventory"
        Me.rdbUnassignedInventory.Size = New System.Drawing.Size(128, 17)
        Me.rdbUnassignedInventory.TabIndex = 1
        Me.rdbUnassignedInventory.Text = "Unassigned Inventory"
        Me.rdbUnassignedInventory.UseVisualStyleBackColor = True
        '
        'rdbAssignedInventory
        '
        Me.rdbAssignedInventory.AutoSize = True
        Me.rdbAssignedInventory.Checked = True
        Me.rdbAssignedInventory.Location = New System.Drawing.Point(3, 3)
        Me.rdbAssignedInventory.Name = "rdbAssignedInventory"
        Me.rdbAssignedInventory.Size = New System.Drawing.Size(115, 17)
        Me.rdbAssignedInventory.TabIndex = 0
        Me.rdbAssignedInventory.TabStop = True
        Me.rdbAssignedInventory.Text = "Assigned Inventory"
        Me.rdbAssignedInventory.UseVisualStyleBackColor = True
        '
        'cboReportInventoryType
        '
        Me.cboReportInventoryType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReportInventoryType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReportInventoryType.FormattingEnabled = True
        Me.cboReportInventoryType.Location = New System.Drawing.Point(22, 67)
        Me.cboReportInventoryType.Name = "cboReportInventoryType"
        Me.cboReportInventoryType.Size = New System.Drawing.Size(146, 21)
        Me.cboReportInventoryType.TabIndex = 86
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(8, 51)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(111, 13)
        Me.Label38.TabIndex = 87
        Me.Label38.Text = "Select Inventory Type"
        '
        'btnViewInventoryReport
        '
        Me.btnViewInventoryReport.AutoSize = True
        Me.btnViewInventoryReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewInventoryReport.Location = New System.Drawing.Point(651, 27)
        Me.btnViewInventoryReport.Name = "btnViewInventoryReport"
        Me.btnViewInventoryReport.Size = New System.Drawing.Size(75, 23)
        Me.btnViewInventoryReport.TabIndex = 85
        Me.btnViewInventoryReport.Text = "View Report"
        Me.btnViewInventoryReport.UseVisualStyleBackColor = True
        '
        'cboReportUnit
        '
        Me.cboReportUnit.FormattingEnabled = True
        Me.cboReportUnit.Location = New System.Drawing.Point(261, 27)
        Me.cboReportUnit.Name = "cboReportUnit"
        Me.cboReportUnit.Size = New System.Drawing.Size(213, 21)
        Me.cboReportUnit.TabIndex = 84
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(247, 11)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(59, 13)
        Me.Label39.TabIndex = 83
        Me.Label39.Text = "Select Unit"
        '
        'cboReportProgram
        '
        Me.cboReportProgram.FormattingEnabled = True
        Me.cboReportProgram.Location = New System.Drawing.Point(22, 27)
        Me.cboReportProgram.Name = "cboReportProgram"
        Me.cboReportProgram.Size = New System.Drawing.Size(212, 21)
        Me.cboReportProgram.TabIndex = 82
        '
        'btnViewReport
        '
        Me.btnViewReport.AutoSize = True
        Me.btnViewReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewReport.Location = New System.Drawing.Point(520, 25)
        Me.btnViewReport.Name = "btnViewReport"
        Me.btnViewReport.Size = New System.Drawing.Size(66, 23)
        Me.btnViewReport.TabIndex = 65
        Me.btnViewReport.Text = "View Data"
        Me.btnViewReport.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(8, 11)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(79, 13)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "Select Program"
        '
        'TPListTools
        '
        Me.TPListTools.Controls.Add(Me.GroupBox1)
        Me.TPListTools.Controls.Add(Me.GroupBox2)
        Me.TPListTools.Location = New System.Drawing.Point(4, 22)
        Me.TPListTools.Name = "TPListTools"
        Me.TPListTools.Size = New System.Drawing.Size(784, 694)
        Me.TPListTools.TabIndex = 2
        Me.TPListTools.Text = "List tools"
        Me.TPListTools.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnClearTransactionType)
        Me.GroupBox1.Controls.Add(Me.txtTransactionIDValue)
        Me.GroupBox1.Controls.Add(Me.btnEditTransactionType)
        Me.GroupBox1.Controls.Add(Me.btnAddTransactionType)
        Me.GroupBox1.Controls.Add(Me.btnDeleteTransactionType)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtNewTransactionType)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.cboExistingTransactionTypes)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 162)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(784, 162)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transaction Type"
        '
        'btnClearTransactionType
        '
        Me.btnClearTransactionType.AutoSize = True
        Me.btnClearTransactionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearTransactionType.Location = New System.Drawing.Point(142, 47)
        Me.btnClearTransactionType.Name = "btnClearTransactionType"
        Me.btnClearTransactionType.Size = New System.Drawing.Size(41, 23)
        Me.btnClearTransactionType.TabIndex = 47
        Me.btnClearTransactionType.Text = "Clear"
        Me.btnClearTransactionType.UseVisualStyleBackColor = True
        '
        'txtTransactionIDValue
        '
        Me.txtTransactionIDValue.Location = New System.Drawing.Point(282, 21)
        Me.txtTransactionIDValue.Name = "txtTransactionIDValue"
        Me.txtTransactionIDValue.ReadOnly = True
        Me.txtTransactionIDValue.Size = New System.Drawing.Size(43, 20)
        Me.txtTransactionIDValue.TabIndex = 46
        '
        'btnEditTransactionType
        '
        Me.btnEditTransactionType.AutoSize = True
        Me.btnEditTransactionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditTransactionType.Location = New System.Drawing.Point(438, 20)
        Me.btnEditTransactionType.Name = "btnEditTransactionType"
        Me.btnEditTransactionType.Size = New System.Drawing.Size(101, 23)
        Me.btnEditTransactionType.TabIndex = 45
        Me.btnEditTransactionType.Text = "Edit Existing Type"
        Me.btnEditTransactionType.UseVisualStyleBackColor = True
        '
        'btnAddTransactionType
        '
        Me.btnAddTransactionType.AutoSize = True
        Me.btnAddTransactionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddTransactionType.Location = New System.Drawing.Point(344, 20)
        Me.btnAddTransactionType.Name = "btnAddTransactionType"
        Me.btnAddTransactionType.Size = New System.Drawing.Size(88, 23)
        Me.btnAddTransactionType.TabIndex = 44
        Me.btnAddTransactionType.Text = "Add New Type"
        Me.btnAddTransactionType.UseVisualStyleBackColor = True
        '
        'btnDeleteTransactionType
        '
        Me.btnDeleteTransactionType.AutoSize = True
        Me.btnDeleteTransactionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteTransactionType.Location = New System.Drawing.Point(344, 87)
        Me.btnDeleteTransactionType.Name = "btnDeleteTransactionType"
        Me.btnDeleteTransactionType.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteTransactionType.TabIndex = 43
        Me.btnDeleteTransactionType.Text = "Delete Type"
        Me.btnDeleteTransactionType.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 25)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(135, 13)
        Me.Label25.TabIndex = 41
        Me.Label25.Text = "Add/Edit Transaction Type"
        '
        'txtNewTransactionType
        '
        Me.txtNewTransactionType.Location = New System.Drawing.Point(142, 21)
        Me.txtNewTransactionType.Name = "txtNewTransactionType"
        Me.txtNewTransactionType.Size = New System.Drawing.Size(128, 20)
        Me.txtNewTransactionType.TabIndex = 42
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 92)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(129, 13)
        Me.Label26.TabIndex = 1
        Me.Label26.Text = "Existing Transaction Type"
        '
        'cboExistingTransactionTypes
        '
        Me.cboExistingTransactionTypes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingTransactionTypes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingTransactionTypes.FormattingEnabled = True
        Me.cboExistingTransactionTypes.Location = New System.Drawing.Point(136, 88)
        Me.cboExistingTransactionTypes.Name = "cboExistingTransactionTypes"
        Me.cboExistingTransactionTypes.Size = New System.Drawing.Size(189, 21)
        Me.cboExistingTransactionTypes.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.txtInventoryID)
        Me.GroupBox2.Controls.Add(Me.btnEditType)
        Me.GroupBox2.Controls.Add(Me.btnAddNewType)
        Me.GroupBox2.Controls.Add(Me.btnDeleteInventory)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtNewInventoryType)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.cboExistingInventoryTypes)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(784, 162)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Inventory Type"
        '
        'btnClear
        '
        Me.btnClear.AutoSize = True
        Me.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClear.Location = New System.Drawing.Point(135, 47)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(41, 23)
        Me.btnClear.TabIndex = 47
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtInventoryID
        '
        Me.txtInventoryID.Location = New System.Drawing.Point(275, 21)
        Me.txtInventoryID.Name = "txtInventoryID"
        Me.txtInventoryID.ReadOnly = True
        Me.txtInventoryID.Size = New System.Drawing.Size(43, 20)
        Me.txtInventoryID.TabIndex = 46
        '
        'btnEditType
        '
        Me.btnEditType.AutoSize = True
        Me.btnEditType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditType.Location = New System.Drawing.Point(431, 20)
        Me.btnEditType.Name = "btnEditType"
        Me.btnEditType.Size = New System.Drawing.Size(101, 23)
        Me.btnEditType.TabIndex = 45
        Me.btnEditType.Text = "Edit Existing Type"
        Me.btnEditType.UseVisualStyleBackColor = True
        '
        'btnAddNewType
        '
        Me.btnAddNewType.AutoSize = True
        Me.btnAddNewType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewType.Location = New System.Drawing.Point(337, 20)
        Me.btnAddNewType.Name = "btnAddNewType"
        Me.btnAddNewType.Size = New System.Drawing.Size(88, 23)
        Me.btnAddNewType.TabIndex = 44
        Me.btnAddNewType.Text = "Add New Type"
        Me.btnAddNewType.UseVisualStyleBackColor = True
        '
        'btnDeleteInventory
        '
        Me.btnDeleteInventory.AutoSize = True
        Me.btnDeleteInventory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteInventory.Location = New System.Drawing.Point(337, 87)
        Me.btnDeleteInventory.Name = "btnDeleteInventory"
        Me.btnDeleteInventory.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteInventory.TabIndex = 43
        Me.btnDeleteInventory.Text = "Delete Type"
        Me.btnDeleteInventory.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 13)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "Add/Edit Inventory Type"
        '
        'txtNewInventoryType
        '
        Me.txtNewInventoryType.Location = New System.Drawing.Point(135, 21)
        Me.txtNewInventoryType.Name = "txtNewInventoryType"
        Me.txtNewInventoryType.Size = New System.Drawing.Size(128, 20)
        Me.txtNewInventoryType.TabIndex = 42
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 92)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(117, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Existing Inventory Type"
        '
        'cboExistingInventoryTypes
        '
        Me.cboExistingInventoryTypes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingInventoryTypes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingInventoryTypes.FormattingEnabled = True
        Me.cboExistingInventoryTypes.Location = New System.Drawing.Point(129, 88)
        Me.cboExistingInventoryTypes.Name = "cboExistingInventoryTypes"
        Me.cboExistingInventoryTypes.Size = New System.Drawing.Size(189, 21)
        Me.cboExistingInventoryTypes.TabIndex = 0
        '
        'TPGAITInventory
        '
        Me.TPGAITInventory.Controls.Add(Me.dgvGAITInventory)
        Me.TPGAITInventory.Controls.Add(Me.Panel8)
        Me.TPGAITInventory.Location = New System.Drawing.Point(4, 22)
        Me.TPGAITInventory.Name = "TPGAITInventory"
        Me.TPGAITInventory.Size = New System.Drawing.Size(784, 694)
        Me.TPGAITInventory.TabIndex = 4
        Me.TPGAITInventory.Text = "GAIT Inventory"
        Me.TPGAITInventory.UseVisualStyleBackColor = True
        '
        'dgvGAITInventory
        '
        Me.dgvGAITInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGAITInventory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvGAITInventory.Location = New System.Drawing.Point(0, 297)
        Me.dgvGAITInventory.Name = "dgvGAITInventory"
        Me.dgvGAITInventory.ReadOnly = True
        Me.dgvGAITInventory.Size = New System.Drawing.Size(784, 397)
        Me.dgvGAITInventory.TabIndex = 1
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Panel17)
        Me.Panel8.Controls.Add(Me.txtGAITComment)
        Me.Panel8.Controls.Add(Me.Label56)
        Me.Panel8.Controls.Add(Me.GroupBox4)
        Me.Panel8.Controls.Add(Me.btnDeleteGAITAsset)
        Me.Panel8.Controls.Add(Me.btnSaveGAITAsset)
        Me.Panel8.Controls.Add(Me.btnAddNewGAITAsset)
        Me.Panel8.Controls.Add(Me.cboGAITModelNumber)
        Me.Panel8.Controls.Add(Me.cboGAITIAIPUser)
        Me.Panel8.Controls.Add(Me.txtGAITTimeInService)
        Me.Panel8.Controls.Add(Me.txtGAITPurchaseOrder)
        Me.Panel8.Controls.Add(Me.txtGAITSerial)
        Me.Panel8.Controls.Add(Me.dtpGAITAquired)
        Me.Panel8.Controls.Add(Me.cboGAITQuality)
        Me.Panel8.Controls.Add(Me.cboGAITModel)
        Me.Panel8.Controls.Add(Me.cboGAITManufacturer)
        Me.Panel8.Controls.Add(Me.dtpGAITPurchased)
        Me.Panel8.Controls.Add(Me.Label53)
        Me.Panel8.Controls.Add(Me.Label52)
        Me.Panel8.Controls.Add(Me.Label51)
        Me.Panel8.Controls.Add(Me.Label50)
        Me.Panel8.Controls.Add(Me.Label49)
        Me.Panel8.Controls.Add(Me.Label48)
        Me.Panel8.Controls.Add(Me.Label47)
        Me.Panel8.Controls.Add(Me.Label46)
        Me.Panel8.Controls.Add(Me.Label45)
        Me.Panel8.Controls.Add(Me.Label44)
        Me.Panel8.Controls.Add(Me.btnClearGAIT)
        Me.Panel8.Controls.Add(Me.Label41)
        Me.Panel8.Controls.Add(Me.txtGAITAssetTag)
        Me.Panel8.Controls.Add(Me.Label42)
        Me.Panel8.Controls.Add(Me.txtGAITID)
        Me.Panel8.Controls.Add(Me.cboGAITCategory)
        Me.Panel8.Controls.Add(Me.Label43)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(784, 297)
        Me.Panel8.TabIndex = 0
        '
        'Panel17
        '
        Me.Panel17.AutoSize = True
        Me.Panel17.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel17.Controls.Add(Me.rdbDeleted)
        Me.Panel17.Controls.Add(Me.rdbActive)
        Me.Panel17.Location = New System.Drawing.Point(500, 1)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(129, 25)
        Me.Panel17.TabIndex = 422
        '
        'rdbDeleted
        '
        Me.rdbDeleted.AutoSize = True
        Me.rdbDeleted.Location = New System.Drawing.Point(64, 5)
        Me.rdbDeleted.Name = "rdbDeleted"
        Me.rdbDeleted.Size = New System.Drawing.Size(62, 17)
        Me.rdbDeleted.TabIndex = 1
        Me.rdbDeleted.TabStop = True
        Me.rdbDeleted.Text = "Deleted"
        Me.rdbDeleted.UseVisualStyleBackColor = True
        '
        'rdbActive
        '
        Me.rdbActive.AutoSize = True
        Me.rdbActive.Location = New System.Drawing.Point(3, 5)
        Me.rdbActive.Name = "rdbActive"
        Me.rdbActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbActive.TabIndex = 0
        Me.rdbActive.TabStop = True
        Me.rdbActive.Text = "Active"
        Me.rdbActive.UseVisualStyleBackColor = True
        '
        'txtGAITComment
        '
        Me.txtGAITComment.Location = New System.Drawing.Point(318, 124)
        Me.txtGAITComment.Name = "txtGAITComment"
        Me.txtGAITComment.Size = New System.Drawing.Size(303, 20)
        Me.txtGAITComment.TabIndex = 420
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(264, 128)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(51, 13)
        Me.Label56.TabIndex = 421
        Me.Label56.Text = "Comment"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtGAITCommentSearch)
        Me.GroupBox4.Controls.Add(Me.Label66)
        Me.GroupBox4.Controls.Add(Me.clbAssestCategory)
        Me.GroupBox4.Controls.Add(Me.Label63)
        Me.GroupBox4.Controls.Add(Me.chbGAITUseDate)
        Me.GroupBox4.Controls.Add(Me.pnlGAITDateSearch)
        Me.GroupBox4.Controls.Add(Me.btnExportGAIT)
        Me.GroupBox4.Controls.Add(Me.Label60)
        Me.GroupBox4.Controls.Add(Me.txtGAITCount)
        Me.GroupBox4.Controls.Add(Me.Label57)
        Me.GroupBox4.Controls.Add(Me.Panel15)
        Me.GroupBox4.Controls.Add(Me.btnSearchGAITAssets)
        Me.GroupBox4.Controls.Add(Me.Label69)
        Me.GroupBox4.Controls.Add(Me.Label70)
        Me.GroupBox4.Controls.Add(Me.dtpGAITDateEnd)
        Me.GroupBox4.Controls.Add(Me.dtpGAITDateStart)
        Me.GroupBox4.Controls.Add(Me.cboGAITProgram)
        Me.GroupBox4.Controls.Add(Me.Label72)
        Me.GroupBox4.Controls.Add(Me.txtGAITAssetSearch)
        Me.GroupBox4.Controls.Add(Me.Label74)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox4.Location = New System.Drawing.Point(0, 150)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(784, 147)
        Me.GroupBox4.TabIndex = 419
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Search Fields"
        '
        'txtGAITCommentSearch
        '
        Me.txtGAITCommentSearch.Location = New System.Drawing.Point(61, 117)
        Me.txtGAITCommentSearch.Name = "txtGAITCommentSearch"
        Me.txtGAITCommentSearch.Size = New System.Drawing.Size(278, 20)
        Me.txtGAITCommentSearch.TabIndex = 424
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(7, 121)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(51, 13)
        Me.Label66.TabIndex = 425
        Me.Label66.Text = "Comment"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'clbAssestCategory
        '
        Me.clbAssestCategory.CheckOnClick = True
        Me.clbAssestCategory.FormattingEnabled = True
        Me.clbAssestCategory.Location = New System.Drawing.Point(484, 59)
        Me.clbAssestCategory.Name = "clbAssestCategory"
        Me.clbAssestCategory.Size = New System.Drawing.Size(180, 64)
        Me.clbAssestCategory.TabIndex = 423
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(400, 61)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(78, 13)
        Me.Label63.TabIndex = 422
        Me.Label63.Text = "Asset Category"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chbGAITUseDate
        '
        Me.chbGAITUseDate.AutoSize = True
        Me.chbGAITUseDate.Location = New System.Drawing.Point(9, 62)
        Me.chbGAITUseDate.Name = "chbGAITUseDate"
        Me.chbGAITUseDate.Size = New System.Drawing.Size(76, 17)
        Me.chbGAITUseDate.TabIndex = 421
        Me.chbGAITUseDate.Text = "Use Dates"
        Me.chbGAITUseDate.UseVisualStyleBackColor = True
        '
        'pnlGAITDateSearch
        '
        Me.pnlGAITDateSearch.AutoSize = True
        Me.pnlGAITDateSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlGAITDateSearch.Controls.Add(Me.rdbGAITDateAquired)
        Me.pnlGAITDateSearch.Controls.Add(Me.rdbGAITDatePurchased)
        Me.pnlGAITDateSearch.Location = New System.Drawing.Point(6, 75)
        Me.pnlGAITDateSearch.Name = "pnlGAITDateSearch"
        Me.pnlGAITDateSearch.Size = New System.Drawing.Size(108, 39)
        Me.pnlGAITDateSearch.TabIndex = 420
        '
        'rdbGAITDateAquired
        '
        Me.rdbGAITDateAquired.AutoSize = True
        Me.rdbGAITDateAquired.Location = New System.Drawing.Point(3, 19)
        Me.rdbGAITDateAquired.Name = "rdbGAITDateAquired"
        Me.rdbGAITDateAquired.Size = New System.Drawing.Size(87, 17)
        Me.rdbGAITDateAquired.TabIndex = 4
        Me.rdbGAITDateAquired.Text = "Date Aquired"
        Me.rdbGAITDateAquired.UseVisualStyleBackColor = True
        '
        'rdbGAITDatePurchased
        '
        Me.rdbGAITDatePurchased.AutoSize = True
        Me.rdbGAITDatePurchased.Checked = True
        Me.rdbGAITDatePurchased.Location = New System.Drawing.Point(3, 4)
        Me.rdbGAITDatePurchased.Name = "rdbGAITDatePurchased"
        Me.rdbGAITDatePurchased.Size = New System.Drawing.Size(102, 17)
        Me.rdbGAITDatePurchased.TabIndex = 3
        Me.rdbGAITDatePurchased.TabStop = True
        Me.rdbGAITDatePurchased.Text = "Date Purchased"
        Me.rdbGAITDatePurchased.UseVisualStyleBackColor = True
        '
        'btnExportGAIT
        '
        Me.btnExportGAIT.AutoSize = True
        Me.btnExportGAIT.Location = New System.Drawing.Point(670, 90)
        Me.btnExportGAIT.Name = "btnExportGAIT"
        Me.btnExportGAIT.Size = New System.Drawing.Size(106, 23)
        Me.btnExportGAIT.TabIndex = 80
        Me.btnExportGAIT.Text = "Export To Excel"
        Me.btnExportGAIT.UseVisualStyleBackColor = True
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(155, 19)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(84, 13)
        Me.Label60.TabIndex = 79
        Me.Label60.Text = "Inventory Status"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGAITCount
        '
        Me.txtGAITCount.Location = New System.Drawing.Point(723, 64)
        Me.txtGAITCount.Name = "txtGAITCount"
        Me.txtGAITCount.Size = New System.Drawing.Size(53, 20)
        Me.txtGAITCount.TabIndex = 415
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(682, 68)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(35, 13)
        Me.Label57.TabIndex = 414
        Me.Label57.Text = "Count"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel15
        '
        Me.Panel15.AutoSize = True
        Me.Panel15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel15.Controls.Add(Me.rdbGAITInactive)
        Me.Panel15.Controls.Add(Me.rdbGAITBoth)
        Me.Panel15.Controls.Add(Me.rdbGAITActive)
        Me.Panel15.Location = New System.Drawing.Point(240, 16)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(68, 53)
        Me.Panel15.TabIndex = 75
        '
        'rdbGAITInactive
        '
        Me.rdbGAITInactive.AutoSize = True
        Me.rdbGAITInactive.Location = New System.Drawing.Point(3, 18)
        Me.rdbGAITInactive.Name = "rdbGAITInactive"
        Me.rdbGAITInactive.Size = New System.Drawing.Size(62, 17)
        Me.rdbGAITInactive.TabIndex = 2
        Me.rdbGAITInactive.Text = "Deleted"
        Me.rdbGAITInactive.UseVisualStyleBackColor = True
        '
        'rdbGAITBoth
        '
        Me.rdbGAITBoth.AutoSize = True
        Me.rdbGAITBoth.Location = New System.Drawing.Point(3, 33)
        Me.rdbGAITBoth.Name = "rdbGAITBoth"
        Me.rdbGAITBoth.Size = New System.Drawing.Size(47, 17)
        Me.rdbGAITBoth.TabIndex = 1
        Me.rdbGAITBoth.Text = "Both"
        Me.rdbGAITBoth.UseVisualStyleBackColor = True
        '
        'rdbGAITActive
        '
        Me.rdbGAITActive.AutoSize = True
        Me.rdbGAITActive.Checked = True
        Me.rdbGAITActive.Location = New System.Drawing.Point(3, 3)
        Me.rdbGAITActive.Name = "rdbGAITActive"
        Me.rdbGAITActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbGAITActive.TabIndex = 0
        Me.rdbGAITActive.TabStop = True
        Me.rdbGAITActive.Text = "Active"
        Me.rdbGAITActive.UseVisualStyleBackColor = True
        '
        'btnSearchGAITAssets
        '
        Me.btnSearchGAITAssets.AutoSize = True
        Me.btnSearchGAITAssets.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchGAITAssets.Location = New System.Drawing.Point(670, 20)
        Me.btnSearchGAITAssets.Name = "btnSearchGAITAssets"
        Me.btnSearchGAITAssets.Size = New System.Drawing.Size(106, 23)
        Me.btnSearchGAITAssets.TabIndex = 64
        Me.btnSearchGAITAssets.Text = "Search for Asset(s)"
        Me.btnSearchGAITAssets.UseVisualStyleBackColor = True
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(234, 75)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(26, 13)
        Me.Label69.TabIndex = 63
        Me.Label69.Text = "End"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(121, 75)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(29, 13)
        Me.Label70.TabIndex = 62
        Me.Label70.Text = "Start"
        '
        'dtpGAITDateEnd
        '
        Me.dtpGAITDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.dtpGAITDateEnd.Enabled = False
        Me.dtpGAITDateEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpGAITDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGAITDateEnd.Location = New System.Drawing.Point(234, 89)
        Me.dtpGAITDateEnd.Name = "dtpGAITDateEnd"
        Me.dtpGAITDateEnd.Size = New System.Drawing.Size(105, 22)
        Me.dtpGAITDateEnd.TabIndex = 61
        Me.dtpGAITDateEnd.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'dtpGAITDateStart
        '
        Me.dtpGAITDateStart.Checked = False
        Me.dtpGAITDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.dtpGAITDateStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpGAITDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGAITDateStart.Location = New System.Drawing.Point(120, 89)
        Me.dtpGAITDateStart.Name = "dtpGAITDateStart"
        Me.dtpGAITDateStart.Size = New System.Drawing.Size(108, 22)
        Me.dtpGAITDateStart.TabIndex = 60
        Me.dtpGAITDateStart.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboGAITProgram
        '
        Me.cboGAITProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITProgram.FormattingEnabled = True
        Me.cboGAITProgram.Location = New System.Drawing.Point(408, 20)
        Me.cboGAITProgram.Name = "cboGAITProgram"
        Me.cboGAITProgram.Size = New System.Drawing.Size(256, 21)
        Me.cboGAITProgram.TabIndex = 58
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(337, 23)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(70, 13)
        Me.Label72.TabIndex = 57
        Me.Label72.Text = "APB Program"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGAITAssetSearch
        '
        Me.txtGAITAssetSearch.Location = New System.Drawing.Point(61, 16)
        Me.txtGAITAssetSearch.Name = "txtGAITAssetSearch"
        Me.txtGAITAssetSearch.Size = New System.Drawing.Size(79, 20)
        Me.txtGAITAssetSearch.TabIndex = 54
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(3, 20)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(55, 13)
        Me.Label74.TabIndex = 53
        Me.Label74.Text = "Asset Tag"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDeleteGAITAsset
        '
        Me.btnDeleteGAITAsset.AutoSize = True
        Me.btnDeleteGAITAsset.Location = New System.Drawing.Point(670, 82)
        Me.btnDeleteGAITAsset.Name = "btnDeleteGAITAsset"
        Me.btnDeleteGAITAsset.Size = New System.Drawing.Size(106, 23)
        Me.btnDeleteGAITAsset.TabIndex = 14
        Me.btnDeleteGAITAsset.Text = "Delete Asset"
        Me.btnDeleteGAITAsset.UseVisualStyleBackColor = True
        '
        'btnSaveGAITAsset
        '
        Me.btnSaveGAITAsset.AutoSize = True
        Me.btnSaveGAITAsset.Location = New System.Drawing.Point(670, 43)
        Me.btnSaveGAITAsset.Name = "btnSaveGAITAsset"
        Me.btnSaveGAITAsset.Size = New System.Drawing.Size(106, 23)
        Me.btnSaveGAITAsset.TabIndex = 13
        Me.btnSaveGAITAsset.Text = "Save Asset"
        Me.btnSaveGAITAsset.UseVisualStyleBackColor = True
        '
        'btnAddNewGAITAsset
        '
        Me.btnAddNewGAITAsset.AutoSize = True
        Me.btnAddNewGAITAsset.Location = New System.Drawing.Point(670, 5)
        Me.btnAddNewGAITAsset.Name = "btnAddNewGAITAsset"
        Me.btnAddNewGAITAsset.Size = New System.Drawing.Size(106, 23)
        Me.btnAddNewGAITAsset.TabIndex = 12
        Me.btnAddNewGAITAsset.Text = "Add New Asset"
        Me.btnAddNewGAITAsset.UseVisualStyleBackColor = True
        '
        'cboGAITModelNumber
        '
        Me.cboGAITModelNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITModelNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITModelNumber.FormattingEnabled = True
        Me.cboGAITModelNumber.Location = New System.Drawing.Point(90, 62)
        Me.cboGAITModelNumber.Name = "cboGAITModelNumber"
        Me.cboGAITModelNumber.Size = New System.Drawing.Size(121, 21)
        Me.cboGAITModelNumber.TabIndex = 5
        '
        'cboGAITIAIPUser
        '
        Me.cboGAITIAIPUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITIAIPUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITIAIPUser.FormattingEnabled = True
        Me.cboGAITIAIPUser.Location = New System.Drawing.Point(90, 120)
        Me.cboGAITIAIPUser.Name = "cboGAITIAIPUser"
        Me.cboGAITIAIPUser.Size = New System.Drawing.Size(121, 21)
        Me.cboGAITIAIPUser.TabIndex = 11
        '
        'txtGAITTimeInService
        '
        Me.txtGAITTimeInService.Location = New System.Drawing.Point(318, 6)
        Me.txtGAITTimeInService.Name = "txtGAITTimeInService"
        Me.txtGAITTimeInService.ReadOnly = True
        Me.txtGAITTimeInService.Size = New System.Drawing.Size(79, 20)
        Me.txtGAITTimeInService.TabIndex = 1
        '
        'txtGAITPurchaseOrder
        '
        Me.txtGAITPurchaseOrder.Location = New System.Drawing.Point(90, 94)
        Me.txtGAITPurchaseOrder.Name = "txtGAITPurchaseOrder"
        Me.txtGAITPurchaseOrder.Size = New System.Drawing.Size(79, 20)
        Me.txtGAITPurchaseOrder.TabIndex = 8
        '
        'txtGAITSerial
        '
        Me.txtGAITSerial.Location = New System.Drawing.Point(318, 62)
        Me.txtGAITSerial.Name = "txtGAITSerial"
        Me.txtGAITSerial.Size = New System.Drawing.Size(121, 20)
        Me.txtGAITSerial.TabIndex = 6
        '
        'dtpGAITAquired
        '
        Me.dtpGAITAquired.CustomFormat = "dd-MMM-yyyy"
        Me.dtpGAITAquired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGAITAquired.Location = New System.Drawing.Point(525, 94)
        Me.dtpGAITAquired.Name = "dtpGAITAquired"
        Me.dtpGAITAquired.Size = New System.Drawing.Size(96, 20)
        Me.dtpGAITAquired.TabIndex = 10
        '
        'cboGAITQuality
        '
        Me.cboGAITQuality.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITQuality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITQuality.FormattingEnabled = True
        Me.cboGAITQuality.Location = New System.Drawing.Point(500, 62)
        Me.cboGAITQuality.Name = "cboGAITQuality"
        Me.cboGAITQuality.Size = New System.Drawing.Size(121, 21)
        Me.cboGAITQuality.TabIndex = 7
        '
        'cboGAITModel
        '
        Me.cboGAITModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITModel.FormattingEnabled = True
        Me.cboGAITModel.Location = New System.Drawing.Point(500, 31)
        Me.cboGAITModel.Name = "cboGAITModel"
        Me.cboGAITModel.Size = New System.Drawing.Size(121, 21)
        Me.cboGAITModel.TabIndex = 4
        '
        'cboGAITManufacturer
        '
        Me.cboGAITManufacturer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITManufacturer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITManufacturer.FormattingEnabled = True
        Me.cboGAITManufacturer.Location = New System.Drawing.Point(318, 31)
        Me.cboGAITManufacturer.Name = "cboGAITManufacturer"
        Me.cboGAITManufacturer.Size = New System.Drawing.Size(121, 21)
        Me.cboGAITManufacturer.TabIndex = 3
        '
        'dtpGAITPurchased
        '
        Me.dtpGAITPurchased.CustomFormat = "dd-MMM-yyyy"
        Me.dtpGAITPurchased.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGAITPurchased.Location = New System.Drawing.Point(318, 94)
        Me.dtpGAITPurchased.Name = "dtpGAITPurchased"
        Me.dtpGAITPurchased.Size = New System.Drawing.Size(96, 20)
        Me.dtpGAITPurchased.TabIndex = 9
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(32, 124)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(52, 13)
        Me.Label53.TabIndex = 77
        Me.Label53.Text = "IAIP User"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(455, 66)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(39, 13)
        Me.Label52.TabIndex = 76
        Me.Label52.Text = "Quality"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(3, 98)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(81, 13)
        Me.Label51.TabIndex = 75
        Me.Label51.Text = "Purchase Order"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(233, 98)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(84, 13)
        Me.Label50.TabIndex = 74
        Me.Label50.Text = "Date Purchased"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(445, 98)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(69, 13)
        Me.Label49.TabIndex = 73
        Me.Label49.Text = "Date Aquired"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(201, 10)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(116, 13)
        Me.Label48.TabIndex = 72
        Me.Label48.Text = "Time in Service (Years)"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(247, 35)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(70, 13)
        Me.Label47.TabIndex = 71
        Me.Label47.Text = "Manufacturer"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(458, 35)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(36, 13)
        Me.Label46.TabIndex = 70
        Me.Label46.Text = "Model"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(28, 66)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(56, 13)
        Me.Label45.TabIndex = 69
        Me.Label45.Text = "Model No."
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(264, 66)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(53, 13)
        Me.Label44.TabIndex = 68
        Me.Label44.Text = "Serial No."
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClearGAIT
        '
        Me.btnClearGAIT.BackgroundImage = CType(resources.GetObject("btnClearGAIT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearGAIT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClearGAIT.Location = New System.Drawing.Point(175, 5)
        Me.btnClearGAIT.Name = "btnClearGAIT"
        Me.btnClearGAIT.Size = New System.Drawing.Size(23, 23)
        Me.btnClearGAIT.TabIndex = 67
        Me.ToolTip1.SetToolTip(Me.btnClearGAIT, "Clear Information")
        Me.btnClearGAIT.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(35, 35)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(49, 13)
        Me.Label41.TabIndex = 66
        Me.Label41.Text = "Category"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGAITAssetTag
        '
        Me.txtGAITAssetTag.Location = New System.Drawing.Point(90, 6)
        Me.txtGAITAssetTag.Name = "txtGAITAssetTag"
        Me.txtGAITAssetTag.Size = New System.Drawing.Size(79, 20)
        Me.txtGAITAssetTag.TabIndex = 0
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(450, 10)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(18, 13)
        Me.Label42.TabIndex = 65
        Me.Label42.Text = "ID"
        Me.Label42.Visible = False
        '
        'txtGAITID
        '
        Me.txtGAITID.Location = New System.Drawing.Point(470, 6)
        Me.txtGAITID.Name = "txtGAITID"
        Me.txtGAITID.ReadOnly = True
        Me.txtGAITID.Size = New System.Drawing.Size(13, 20)
        Me.txtGAITID.TabIndex = 61
        Me.txtGAITID.Visible = False
        '
        'cboGAITCategory
        '
        Me.cboGAITCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGAITCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGAITCategory.FormattingEnabled = True
        Me.cboGAITCategory.Location = New System.Drawing.Point(90, 31)
        Me.cboGAITCategory.Name = "cboGAITCategory"
        Me.cboGAITCategory.Size = New System.Drawing.Size(121, 21)
        Me.cboGAITCategory.TabIndex = 2
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(29, 10)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(55, 13)
        Me.Label43.TabIndex = 64
        Me.Label43.Text = "Asset Tag"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TPGAITLists
        '
        Me.TPGAITLists.Controls.Add(Me.Panel14)
        Me.TPGAITLists.Location = New System.Drawing.Point(4, 22)
        Me.TPGAITLists.Name = "TPGAITLists"
        Me.TPGAITLists.Size = New System.Drawing.Size(784, 694)
        Me.TPGAITLists.TabIndex = 5
        Me.TPGAITLists.Text = "GAIT Lists"
        Me.TPGAITLists.UseVisualStyleBackColor = True
        '
        'Panel14
        '
        Me.Panel14.AutoScroll = True
        Me.Panel14.Controls.Add(Me.gbGAITQuality)
        Me.Panel14.Controls.Add(Me.Panel13)
        Me.Panel14.Controls.Add(Me.gbGAITModelNumber)
        Me.Panel14.Controls.Add(Me.Panel12)
        Me.Panel14.Controls.Add(Me.gbGAITModel)
        Me.Panel14.Controls.Add(Me.Panel11)
        Me.Panel14.Controls.Add(Me.gbGAITManufacturer)
        Me.Panel14.Controls.Add(Me.Panel10)
        Me.Panel14.Controls.Add(Me.gbGAITCategory)
        Me.Panel14.Controls.Add(Me.Panel9)
        Me.Panel14.Controls.Add(Me.Panel16)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(784, 694)
        Me.Panel14.TabIndex = 63
        '
        'gbGAITQuality
        '
        Me.gbGAITQuality.Controls.Add(Me.rdbGAITQualityActive)
        Me.gbGAITQuality.Controls.Add(Me.btnClearGaitQuality)
        Me.gbGAITQuality.Controls.Add(Me.txtGAITQualityID)
        Me.gbGAITQuality.Controls.Add(Me.btnEditGAITQuality)
        Me.gbGAITQuality.Controls.Add(Me.btnAddGAITQuality)
        Me.gbGAITQuality.Controls.Add(Me.btnDeleteGAITQuality)
        Me.gbGAITQuality.Controls.Add(Me.Label67)
        Me.gbGAITQuality.Controls.Add(Me.txtAddEditGAITQuality)
        Me.gbGAITQuality.Controls.Add(Me.Label68)
        Me.gbGAITQuality.Controls.Add(Me.cboExistingGAITQuality)
        Me.gbGAITQuality.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbGAITQuality.Location = New System.Drawing.Point(0, 615)
        Me.gbGAITQuality.Name = "gbGAITQuality"
        Me.gbGAITQuality.Size = New System.Drawing.Size(767, 111)
        Me.gbGAITQuality.TabIndex = 1
        Me.gbGAITQuality.TabStop = False
        Me.gbGAITQuality.Text = "GAIT Quality"
        '
        'rdbGAITQualityActive
        '
        Me.rdbGAITQualityActive.AutoSize = True
        Me.rdbGAITQualityActive.Enabled = False
        Me.rdbGAITQualityActive.Location = New System.Drawing.Point(267, 45)
        Me.rdbGAITQualityActive.Name = "rdbGAITQualityActive"
        Me.rdbGAITQualityActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbGAITQualityActive.TabIndex = 119
        Me.rdbGAITQualityActive.TabStop = True
        Me.rdbGAITQualityActive.Text = "Active"
        Me.rdbGAITQualityActive.UseVisualStyleBackColor = True
        '
        'btnClearGaitQuality
        '
        Me.btnClearGaitQuality.AutoSize = True
        Me.btnClearGaitQuality.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearGaitQuality.Location = New System.Drawing.Point(132, 42)
        Me.btnClearGaitQuality.Name = "btnClearGaitQuality"
        Me.btnClearGaitQuality.Size = New System.Drawing.Size(41, 23)
        Me.btnClearGaitQuality.TabIndex = 116
        Me.btnClearGaitQuality.Text = "Clear"
        Me.btnClearGaitQuality.UseVisualStyleBackColor = True
        '
        'txtGAITQualityID
        '
        Me.txtGAITQualityID.Location = New System.Drawing.Point(132, 19)
        Me.txtGAITQualityID.Name = "txtGAITQualityID"
        Me.txtGAITQualityID.ReadOnly = True
        Me.txtGAITQualityID.Size = New System.Drawing.Size(27, 20)
        Me.txtGAITQualityID.TabIndex = 115
        '
        'btnEditGAITQuality
        '
        Me.btnEditGAITQuality.AutoSize = True
        Me.btnEditGAITQuality.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditGAITQuality.Location = New System.Drawing.Point(453, 16)
        Me.btnEditGAITQuality.Name = "btnEditGAITQuality"
        Me.btnEditGAITQuality.Size = New System.Drawing.Size(119, 23)
        Me.btnEditGAITQuality.TabIndex = 114
        Me.btnEditGAITQuality.Text = "Edit Existing Category"
        Me.btnEditGAITQuality.UseVisualStyleBackColor = True
        '
        'btnAddGAITQuality
        '
        Me.btnAddGAITQuality.AutoSize = True
        Me.btnAddGAITQuality.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddGAITQuality.Location = New System.Drawing.Point(341, 16)
        Me.btnAddGAITQuality.Name = "btnAddGAITQuality"
        Me.btnAddGAITQuality.Size = New System.Drawing.Size(106, 23)
        Me.btnAddGAITQuality.TabIndex = 113
        Me.btnAddGAITQuality.Text = "Add New Category"
        Me.btnAddGAITQuality.UseVisualStyleBackColor = True
        '
        'btnDeleteGAITQuality
        '
        Me.btnDeleteGAITQuality.AutoSize = True
        Me.btnDeleteGAITQuality.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteGAITQuality.Location = New System.Drawing.Point(341, 71)
        Me.btnDeleteGAITQuality.Name = "btnDeleteGAITQuality"
        Me.btnDeleteGAITQuality.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteGAITQuality.TabIndex = 112
        Me.btnDeleteGAITQuality.Text = "Flag as Inactive"
        Me.btnDeleteGAITQuality.UseVisualStyleBackColor = True
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(6, 23)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(112, 13)
        Me.Label67.TabIndex = 110
        Me.Label67.Text = "Add/Edit GAIT Quality"
        '
        'txtAddEditGAITQuality
        '
        Me.txtAddEditGAITQuality.Location = New System.Drawing.Point(165, 19)
        Me.txtAddEditGAITQuality.Name = "txtAddEditGAITQuality"
        Me.txtAddEditGAITQuality.Size = New System.Drawing.Size(157, 20)
        Me.txtAddEditGAITQuality.TabIndex = 111
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(10, 76)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(106, 13)
        Me.Label68.TabIndex = 109
        Me.Label68.Text = "Existing GAIT Quality"
        '
        'cboExistingGAITQuality
        '
        Me.cboExistingGAITQuality.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingGAITQuality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingGAITQuality.FormattingEnabled = True
        Me.cboExistingGAITQuality.Location = New System.Drawing.Point(133, 72)
        Me.cboExistingGAITQuality.Name = "cboExistingGAITQuality"
        Me.cboExistingGAITQuality.Size = New System.Drawing.Size(189, 21)
        Me.cboExistingGAITQuality.TabIndex = 108
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.btnLoadGAITQuality)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 583)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(767, 32)
        Me.Panel13.TabIndex = 62
        '
        'btnLoadGAITQuality
        '
        Me.btnLoadGAITQuality.AutoSize = True
        Me.btnLoadGAITQuality.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadGAITQuality.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadGAITQuality.Name = "btnLoadGAITQuality"
        Me.btnLoadGAITQuality.Size = New System.Drawing.Size(104, 23)
        Me.btnLoadGAITQuality.TabIndex = 57
        Me.btnLoadGAITQuality.Text = "Load GAIT Quality"
        Me.btnLoadGAITQuality.UseVisualStyleBackColor = True
        '
        'gbGAITModelNumber
        '
        Me.gbGAITModelNumber.Controls.Add(Me.rdbGAITModelNumberActive)
        Me.gbGAITModelNumber.Controls.Add(Me.btnClearGaitModelNumber)
        Me.gbGAITModelNumber.Controls.Add(Me.txtGAITModelNumberID)
        Me.gbGAITModelNumber.Controls.Add(Me.btnEditGAITModelNumber)
        Me.gbGAITModelNumber.Controls.Add(Me.btnAddGAITModelNumber)
        Me.gbGAITModelNumber.Controls.Add(Me.btnDeleteGAITModelNumber)
        Me.gbGAITModelNumber.Controls.Add(Me.Label64)
        Me.gbGAITModelNumber.Controls.Add(Me.txtAddEditGAITModelNumber)
        Me.gbGAITModelNumber.Controls.Add(Me.Label65)
        Me.gbGAITModelNumber.Controls.Add(Me.cboExistingGAITModelNumber)
        Me.gbGAITModelNumber.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbGAITModelNumber.Location = New System.Drawing.Point(0, 477)
        Me.gbGAITModelNumber.Name = "gbGAITModelNumber"
        Me.gbGAITModelNumber.Size = New System.Drawing.Size(767, 106)
        Me.gbGAITModelNumber.TabIndex = 1
        Me.gbGAITModelNumber.TabStop = False
        Me.gbGAITModelNumber.Text = "GAIT Model Number"
        '
        'rdbGAITModelNumberActive
        '
        Me.rdbGAITModelNumberActive.AutoSize = True
        Me.rdbGAITModelNumberActive.Enabled = False
        Me.rdbGAITModelNumberActive.Location = New System.Drawing.Point(267, 45)
        Me.rdbGAITModelNumberActive.Name = "rdbGAITModelNumberActive"
        Me.rdbGAITModelNumberActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbGAITModelNumberActive.TabIndex = 107
        Me.rdbGAITModelNumberActive.TabStop = True
        Me.rdbGAITModelNumberActive.Text = "Active"
        Me.rdbGAITModelNumberActive.UseVisualStyleBackColor = True
        '
        'btnClearGaitModelNumber
        '
        Me.btnClearGaitModelNumber.AutoSize = True
        Me.btnClearGaitModelNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearGaitModelNumber.Location = New System.Drawing.Point(132, 42)
        Me.btnClearGaitModelNumber.Name = "btnClearGaitModelNumber"
        Me.btnClearGaitModelNumber.Size = New System.Drawing.Size(41, 23)
        Me.btnClearGaitModelNumber.TabIndex = 104
        Me.btnClearGaitModelNumber.Text = "Clear"
        Me.btnClearGaitModelNumber.UseVisualStyleBackColor = True
        '
        'txtGAITModelNumberID
        '
        Me.txtGAITModelNumberID.Location = New System.Drawing.Point(132, 19)
        Me.txtGAITModelNumberID.Name = "txtGAITModelNumberID"
        Me.txtGAITModelNumberID.ReadOnly = True
        Me.txtGAITModelNumberID.Size = New System.Drawing.Size(27, 20)
        Me.txtGAITModelNumberID.TabIndex = 103
        '
        'btnEditGAITModelNumber
        '
        Me.btnEditGAITModelNumber.AutoSize = True
        Me.btnEditGAITModelNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditGAITModelNumber.Location = New System.Drawing.Point(453, 16)
        Me.btnEditGAITModelNumber.Name = "btnEditGAITModelNumber"
        Me.btnEditGAITModelNumber.Size = New System.Drawing.Size(119, 23)
        Me.btnEditGAITModelNumber.TabIndex = 102
        Me.btnEditGAITModelNumber.Text = "Edit Existing Category"
        Me.btnEditGAITModelNumber.UseVisualStyleBackColor = True
        '
        'btnAddGAITModelNumber
        '
        Me.btnAddGAITModelNumber.AutoSize = True
        Me.btnAddGAITModelNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddGAITModelNumber.Location = New System.Drawing.Point(341, 16)
        Me.btnAddGAITModelNumber.Name = "btnAddGAITModelNumber"
        Me.btnAddGAITModelNumber.Size = New System.Drawing.Size(106, 23)
        Me.btnAddGAITModelNumber.TabIndex = 101
        Me.btnAddGAITModelNumber.Text = "Add New Category"
        Me.btnAddGAITModelNumber.UseVisualStyleBackColor = True
        '
        'btnDeleteGAITModelNumber
        '
        Me.btnDeleteGAITModelNumber.AutoSize = True
        Me.btnDeleteGAITModelNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteGAITModelNumber.Location = New System.Drawing.Point(341, 70)
        Me.btnDeleteGAITModelNumber.Name = "btnDeleteGAITModelNumber"
        Me.btnDeleteGAITModelNumber.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteGAITModelNumber.TabIndex = 100
        Me.btnDeleteGAITModelNumber.Text = "Flag as Inactive"
        Me.btnDeleteGAITModelNumber.UseVisualStyleBackColor = True
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(6, 23)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(129, 13)
        Me.Label64.TabIndex = 98
        Me.Label64.Text = "Add/Edit GAIT Model No."
        '
        'txtAddEditGAITModelNumber
        '
        Me.txtAddEditGAITModelNumber.Location = New System.Drawing.Point(165, 18)
        Me.txtAddEditGAITModelNumber.Name = "txtAddEditGAITModelNumber"
        Me.txtAddEditGAITModelNumber.Size = New System.Drawing.Size(157, 20)
        Me.txtAddEditGAITModelNumber.TabIndex = 99
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(10, 76)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(123, 13)
        Me.Label65.TabIndex = 97
        Me.Label65.Text = "Existing GAIT Model No."
        '
        'cboExistingGAITModelNumber
        '
        Me.cboExistingGAITModelNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingGAITModelNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingGAITModelNumber.FormattingEnabled = True
        Me.cboExistingGAITModelNumber.Location = New System.Drawing.Point(133, 72)
        Me.cboExistingGAITModelNumber.Name = "cboExistingGAITModelNumber"
        Me.cboExistingGAITModelNumber.Size = New System.Drawing.Size(189, 21)
        Me.cboExistingGAITModelNumber.TabIndex = 96
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.btnLoadGAITModelNumber)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 445)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(767, 32)
        Me.Panel12.TabIndex = 61
        '
        'btnLoadGAITModelNumber
        '
        Me.btnLoadGAITModelNumber.AutoSize = True
        Me.btnLoadGAITModelNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadGAITModelNumber.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadGAITModelNumber.Name = "btnLoadGAITModelNumber"
        Me.btnLoadGAITModelNumber.Size = New System.Drawing.Size(141, 23)
        Me.btnLoadGAITModelNumber.TabIndex = 57
        Me.btnLoadGAITModelNumber.Text = "Load GAIT Model Number"
        Me.btnLoadGAITModelNumber.UseVisualStyleBackColor = True
        '
        'gbGAITModel
        '
        Me.gbGAITModel.Controls.Add(Me.rdbGAITModelActive)
        Me.gbGAITModel.Controls.Add(Me.btnClearGaitModel)
        Me.gbGAITModel.Controls.Add(Me.txtGAITModelID)
        Me.gbGAITModel.Controls.Add(Me.btnEditGAITModel)
        Me.gbGAITModel.Controls.Add(Me.btnAddGAITModel)
        Me.gbGAITModel.Controls.Add(Me.btnDeleteGAITModel)
        Me.gbGAITModel.Controls.Add(Me.Label61)
        Me.gbGAITModel.Controls.Add(Me.txtAddEditGAITModel)
        Me.gbGAITModel.Controls.Add(Me.Label62)
        Me.gbGAITModel.Controls.Add(Me.cboExistingGAITModel)
        Me.gbGAITModel.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbGAITModel.Location = New System.Drawing.Point(0, 337)
        Me.gbGAITModel.Name = "gbGAITModel"
        Me.gbGAITModel.Size = New System.Drawing.Size(767, 108)
        Me.gbGAITModel.TabIndex = 1
        Me.gbGAITModel.TabStop = False
        Me.gbGAITModel.Text = "GAIT Model"
        '
        'rdbGAITModelActive
        '
        Me.rdbGAITModelActive.AutoSize = True
        Me.rdbGAITModelActive.Enabled = False
        Me.rdbGAITModelActive.Location = New System.Drawing.Point(267, 44)
        Me.rdbGAITModelActive.Name = "rdbGAITModelActive"
        Me.rdbGAITModelActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbGAITModelActive.TabIndex = 95
        Me.rdbGAITModelActive.TabStop = True
        Me.rdbGAITModelActive.Text = "Active"
        Me.rdbGAITModelActive.UseVisualStyleBackColor = True
        '
        'btnClearGaitModel
        '
        Me.btnClearGaitModel.AutoSize = True
        Me.btnClearGaitModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearGaitModel.Location = New System.Drawing.Point(118, 41)
        Me.btnClearGaitModel.Name = "btnClearGaitModel"
        Me.btnClearGaitModel.Size = New System.Drawing.Size(41, 23)
        Me.btnClearGaitModel.TabIndex = 92
        Me.btnClearGaitModel.Text = "Clear"
        Me.btnClearGaitModel.UseVisualStyleBackColor = True
        '
        'txtGAITModelID
        '
        Me.txtGAITModelID.Location = New System.Drawing.Point(118, 18)
        Me.txtGAITModelID.Name = "txtGAITModelID"
        Me.txtGAITModelID.ReadOnly = True
        Me.txtGAITModelID.Size = New System.Drawing.Size(27, 20)
        Me.txtGAITModelID.TabIndex = 91
        '
        'btnEditGAITModel
        '
        Me.btnEditGAITModel.AutoSize = True
        Me.btnEditGAITModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditGAITModel.Location = New System.Drawing.Point(453, 16)
        Me.btnEditGAITModel.Name = "btnEditGAITModel"
        Me.btnEditGAITModel.Size = New System.Drawing.Size(119, 23)
        Me.btnEditGAITModel.TabIndex = 90
        Me.btnEditGAITModel.Text = "Edit Existing Category"
        Me.btnEditGAITModel.UseVisualStyleBackColor = True
        '
        'btnAddGAITModel
        '
        Me.btnAddGAITModel.AutoSize = True
        Me.btnAddGAITModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddGAITModel.Location = New System.Drawing.Point(341, 16)
        Me.btnAddGAITModel.Name = "btnAddGAITModel"
        Me.btnAddGAITModel.Size = New System.Drawing.Size(106, 23)
        Me.btnAddGAITModel.TabIndex = 89
        Me.btnAddGAITModel.Text = "Add New Category"
        Me.btnAddGAITModel.UseVisualStyleBackColor = True
        '
        'btnDeleteGAITModel
        '
        Me.btnDeleteGAITModel.AutoSize = True
        Me.btnDeleteGAITModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteGAITModel.Location = New System.Drawing.Point(341, 69)
        Me.btnDeleteGAITModel.Name = "btnDeleteGAITModel"
        Me.btnDeleteGAITModel.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteGAITModel.TabIndex = 88
        Me.btnDeleteGAITModel.Text = "Flag as Inactive"
        Me.btnDeleteGAITModel.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(6, 23)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(109, 13)
        Me.Label61.TabIndex = 86
        Me.Label61.Text = "Add/Edit GAIT Model"
        '
        'txtAddEditGAITModel
        '
        Me.txtAddEditGAITModel.Location = New System.Drawing.Point(151, 18)
        Me.txtAddEditGAITModel.Name = "txtAddEditGAITModel"
        Me.txtAddEditGAITModel.Size = New System.Drawing.Size(171, 20)
        Me.txtAddEditGAITModel.TabIndex = 87
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(10, 76)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(103, 13)
        Me.Label62.TabIndex = 85
        Me.Label62.Text = "Existing GAIT Model"
        '
        'cboExistingGAITModel
        '
        Me.cboExistingGAITModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingGAITModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingGAITModel.FormattingEnabled = True
        Me.cboExistingGAITModel.Location = New System.Drawing.Point(119, 71)
        Me.cboExistingGAITModel.Name = "cboExistingGAITModel"
        Me.cboExistingGAITModel.Size = New System.Drawing.Size(203, 21)
        Me.cboExistingGAITModel.TabIndex = 84
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.btnLoadGAITModel)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 305)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(767, 32)
        Me.Panel11.TabIndex = 60
        '
        'btnLoadGAITModel
        '
        Me.btnLoadGAITModel.AutoSize = True
        Me.btnLoadGAITModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadGAITModel.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadGAITModel.Name = "btnLoadGAITModel"
        Me.btnLoadGAITModel.Size = New System.Drawing.Size(101, 23)
        Me.btnLoadGAITModel.TabIndex = 57
        Me.btnLoadGAITModel.Text = "Load GAIT Model"
        Me.btnLoadGAITModel.UseVisualStyleBackColor = True
        '
        'gbGAITManufacturer
        '
        Me.gbGAITManufacturer.Controls.Add(Me.rdbGAITManufacturerActive)
        Me.gbGAITManufacturer.Controls.Add(Me.btnClearGaitManufacturer)
        Me.gbGAITManufacturer.Controls.Add(Me.txtGAITManufacturerID)
        Me.gbGAITManufacturer.Controls.Add(Me.btnEditGAITManufacturer)
        Me.gbGAITManufacturer.Controls.Add(Me.btnAddGAITManufaturer)
        Me.gbGAITManufacturer.Controls.Add(Me.btnDeleteGAITManufaturer)
        Me.gbGAITManufacturer.Controls.Add(Me.Label58)
        Me.gbGAITManufacturer.Controls.Add(Me.txtAddEditGAITManufacturer)
        Me.gbGAITManufacturer.Controls.Add(Me.Label59)
        Me.gbGAITManufacturer.Controls.Add(Me.cboExistingGAITManufacturer)
        Me.gbGAITManufacturer.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbGAITManufacturer.Location = New System.Drawing.Point(0, 201)
        Me.gbGAITManufacturer.Name = "gbGAITManufacturer"
        Me.gbGAITManufacturer.Size = New System.Drawing.Size(767, 104)
        Me.gbGAITManufacturer.TabIndex = 1
        Me.gbGAITManufacturer.TabStop = False
        Me.gbGAITManufacturer.Text = "GAIT Manufcturer"
        '
        'rdbGAITManufacturerActive
        '
        Me.rdbGAITManufacturerActive.AutoSize = True
        Me.rdbGAITManufacturerActive.Enabled = False
        Me.rdbGAITManufacturerActive.Location = New System.Drawing.Point(267, 45)
        Me.rdbGAITManufacturerActive.Name = "rdbGAITManufacturerActive"
        Me.rdbGAITManufacturerActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbGAITManufacturerActive.TabIndex = 83
        Me.rdbGAITManufacturerActive.TabStop = True
        Me.rdbGAITManufacturerActive.Text = "Active"
        Me.rdbGAITManufacturerActive.UseVisualStyleBackColor = True
        '
        'btnClearGaitManufacturer
        '
        Me.btnClearGaitManufacturer.AutoSize = True
        Me.btnClearGaitManufacturer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearGaitManufacturer.Location = New System.Drawing.Point(151, 42)
        Me.btnClearGaitManufacturer.Name = "btnClearGaitManufacturer"
        Me.btnClearGaitManufacturer.Size = New System.Drawing.Size(41, 23)
        Me.btnClearGaitManufacturer.TabIndex = 80
        Me.btnClearGaitManufacturer.Text = "Clear"
        Me.btnClearGaitManufacturer.UseVisualStyleBackColor = True
        '
        'txtGAITManufacturerID
        '
        Me.txtGAITManufacturerID.Location = New System.Drawing.Point(151, 19)
        Me.txtGAITManufacturerID.Name = "txtGAITManufacturerID"
        Me.txtGAITManufacturerID.ReadOnly = True
        Me.txtGAITManufacturerID.Size = New System.Drawing.Size(27, 20)
        Me.txtGAITManufacturerID.TabIndex = 79
        '
        'btnEditGAITManufacturer
        '
        Me.btnEditGAITManufacturer.AutoSize = True
        Me.btnEditGAITManufacturer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditGAITManufacturer.Location = New System.Drawing.Point(453, 18)
        Me.btnEditGAITManufacturer.Name = "btnEditGAITManufacturer"
        Me.btnEditGAITManufacturer.Size = New System.Drawing.Size(119, 23)
        Me.btnEditGAITManufacturer.TabIndex = 78
        Me.btnEditGAITManufacturer.Text = "Edit Existing Category"
        Me.btnEditGAITManufacturer.UseVisualStyleBackColor = True
        '
        'btnAddGAITManufaturer
        '
        Me.btnAddGAITManufaturer.AutoSize = True
        Me.btnAddGAITManufaturer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddGAITManufaturer.Location = New System.Drawing.Point(341, 18)
        Me.btnAddGAITManufaturer.Name = "btnAddGAITManufaturer"
        Me.btnAddGAITManufaturer.Size = New System.Drawing.Size(106, 23)
        Me.btnAddGAITManufaturer.TabIndex = 77
        Me.btnAddGAITManufaturer.Text = "Add New Category"
        Me.btnAddGAITManufaturer.UseVisualStyleBackColor = True
        '
        'btnDeleteGAITManufaturer
        '
        Me.btnDeleteGAITManufaturer.AutoSize = True
        Me.btnDeleteGAITManufaturer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteGAITManufaturer.Location = New System.Drawing.Point(341, 70)
        Me.btnDeleteGAITManufaturer.Name = "btnDeleteGAITManufaturer"
        Me.btnDeleteGAITManufaturer.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteGAITManufaturer.TabIndex = 76
        Me.btnDeleteGAITManufaturer.Text = "Flag as Inactive"
        Me.btnDeleteGAITManufaturer.UseVisualStyleBackColor = True
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(6, 23)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(143, 13)
        Me.Label58.TabIndex = 74
        Me.Label58.Text = "Add/Edit GAIT Manufacturer"
        '
        'txtAddEditGAITManufacturer
        '
        Me.txtAddEditGAITManufacturer.Location = New System.Drawing.Point(184, 19)
        Me.txtAddEditGAITManufacturer.Name = "txtAddEditGAITManufacturer"
        Me.txtAddEditGAITManufacturer.Size = New System.Drawing.Size(138, 20)
        Me.txtAddEditGAITManufacturer.TabIndex = 75
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(10, 76)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(137, 13)
        Me.Label59.TabIndex = 73
        Me.Label59.Text = "Existing GAIT Manufacturer"
        '
        'cboExistingGAITManufacturer
        '
        Me.cboExistingGAITManufacturer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingGAITManufacturer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingGAITManufacturer.FormattingEnabled = True
        Me.cboExistingGAITManufacturer.Location = New System.Drawing.Point(152, 72)
        Me.cboExistingGAITManufacturer.Name = "cboExistingGAITManufacturer"
        Me.cboExistingGAITManufacturer.Size = New System.Drawing.Size(170, 21)
        Me.cboExistingGAITManufacturer.TabIndex = 72
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.btnLoadGAITManufacturer)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 169)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(767, 32)
        Me.Panel10.TabIndex = 59
        '
        'btnLoadGAITManufacturer
        '
        Me.btnLoadGAITManufacturer.AutoSize = True
        Me.btnLoadGAITManufacturer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadGAITManufacturer.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadGAITManufacturer.Name = "btnLoadGAITManufacturer"
        Me.btnLoadGAITManufacturer.Size = New System.Drawing.Size(135, 23)
        Me.btnLoadGAITManufacturer.TabIndex = 57
        Me.btnLoadGAITManufacturer.Text = "Load GAIT Manufacturer"
        Me.btnLoadGAITManufacturer.UseVisualStyleBackColor = True
        '
        'gbGAITCategory
        '
        Me.gbGAITCategory.Controls.Add(Me.rdbGAITCategoryActive)
        Me.gbGAITCategory.Controls.Add(Me.btnClearGaitCategory)
        Me.gbGAITCategory.Controls.Add(Me.txtGAITCategoryID)
        Me.gbGAITCategory.Controls.Add(Me.btnEditGAITCategory)
        Me.gbGAITCategory.Controls.Add(Me.btnAddGAITCategory)
        Me.gbGAITCategory.Controls.Add(Me.btnDeleteGAITCategory)
        Me.gbGAITCategory.Controls.Add(Me.Label54)
        Me.gbGAITCategory.Controls.Add(Me.txtAddEditGAITCategory)
        Me.gbGAITCategory.Controls.Add(Me.Label55)
        Me.gbGAITCategory.Controls.Add(Me.cboExistingGAITCategory)
        Me.gbGAITCategory.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbGAITCategory.Location = New System.Drawing.Point(0, 64)
        Me.gbGAITCategory.Name = "gbGAITCategory"
        Me.gbGAITCategory.Size = New System.Drawing.Size(767, 105)
        Me.gbGAITCategory.TabIndex = 0
        Me.gbGAITCategory.TabStop = False
        Me.gbGAITCategory.Text = "GAIT Category"
        '
        'rdbGAITCategoryActive
        '
        Me.rdbGAITCategoryActive.AutoSize = True
        Me.rdbGAITCategoryActive.Enabled = False
        Me.rdbGAITCategoryActive.Location = New System.Drawing.Point(267, 41)
        Me.rdbGAITCategoryActive.Name = "rdbGAITCategoryActive"
        Me.rdbGAITCategoryActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbGAITCategoryActive.TabIndex = 71
        Me.rdbGAITCategoryActive.TabStop = True
        Me.rdbGAITCategoryActive.Text = "Active"
        Me.rdbGAITCategoryActive.UseVisualStyleBackColor = True
        '
        'btnClearGaitCategory
        '
        Me.btnClearGaitCategory.AutoSize = True
        Me.btnClearGaitCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearGaitCategory.Location = New System.Drawing.Point(132, 38)
        Me.btnClearGaitCategory.Name = "btnClearGaitCategory"
        Me.btnClearGaitCategory.Size = New System.Drawing.Size(41, 23)
        Me.btnClearGaitCategory.TabIndex = 56
        Me.btnClearGaitCategory.Text = "Clear"
        Me.btnClearGaitCategory.UseVisualStyleBackColor = True
        '
        'txtGAITCategoryID
        '
        Me.txtGAITCategoryID.Location = New System.Drawing.Point(132, 15)
        Me.txtGAITCategoryID.Name = "txtGAITCategoryID"
        Me.txtGAITCategoryID.ReadOnly = True
        Me.txtGAITCategoryID.Size = New System.Drawing.Size(27, 20)
        Me.txtGAITCategoryID.TabIndex = 55
        '
        'btnEditGAITCategory
        '
        Me.btnEditGAITCategory.AutoSize = True
        Me.btnEditGAITCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditGAITCategory.Location = New System.Drawing.Point(453, 12)
        Me.btnEditGAITCategory.Name = "btnEditGAITCategory"
        Me.btnEditGAITCategory.Size = New System.Drawing.Size(119, 23)
        Me.btnEditGAITCategory.TabIndex = 54
        Me.btnEditGAITCategory.Text = "Edit Existing Category"
        Me.btnEditGAITCategory.UseVisualStyleBackColor = True
        '
        'btnAddGAITCategory
        '
        Me.btnAddGAITCategory.AutoSize = True
        Me.btnAddGAITCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddGAITCategory.Location = New System.Drawing.Point(341, 12)
        Me.btnAddGAITCategory.Name = "btnAddGAITCategory"
        Me.btnAddGAITCategory.Size = New System.Drawing.Size(106, 23)
        Me.btnAddGAITCategory.TabIndex = 53
        Me.btnAddGAITCategory.Text = "Add New Category"
        Me.btnAddGAITCategory.UseVisualStyleBackColor = True
        '
        'btnDeleteGAITCategory
        '
        Me.btnDeleteGAITCategory.AutoSize = True
        Me.btnDeleteGAITCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteGAITCategory.Location = New System.Drawing.Point(341, 67)
        Me.btnDeleteGAITCategory.Name = "btnDeleteGAITCategory"
        Me.btnDeleteGAITCategory.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteGAITCategory.TabIndex = 52
        Me.btnDeleteGAITCategory.Text = "Flag as Inactive"
        Me.btnDeleteGAITCategory.UseVisualStyleBackColor = True
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(6, 19)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(122, 13)
        Me.Label54.TabIndex = 50
        Me.Label54.Text = "Add/Edit GAIT Category"
        '
        'txtAddEditGAITCategory
        '
        Me.txtAddEditGAITCategory.Location = New System.Drawing.Point(165, 15)
        Me.txtAddEditGAITCategory.Name = "txtAddEditGAITCategory"
        Me.txtAddEditGAITCategory.Size = New System.Drawing.Size(157, 20)
        Me.txtAddEditGAITCategory.TabIndex = 51
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(10, 72)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(116, 13)
        Me.Label55.TabIndex = 49
        Me.Label55.Text = "Existing GAIT Category"
        '
        'cboExistingGAITCategory
        '
        Me.cboExistingGAITCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboExistingGAITCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboExistingGAITCategory.FormattingEnabled = True
        Me.cboExistingGAITCategory.Location = New System.Drawing.Point(133, 68)
        Me.cboExistingGAITCategory.Name = "cboExistingGAITCategory"
        Me.cboExistingGAITCategory.Size = New System.Drawing.Size(189, 21)
        Me.cboExistingGAITCategory.TabIndex = 48
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.btnLoadGAITCategory)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 32)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(767, 32)
        Me.Panel9.TabIndex = 58
        '
        'btnLoadGAITCategory
        '
        Me.btnLoadGAITCategory.AutoSize = True
        Me.btnLoadGAITCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadGAITCategory.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadGAITCategory.Name = "btnLoadGAITCategory"
        Me.btnLoadGAITCategory.Size = New System.Drawing.Size(114, 23)
        Me.btnLoadGAITCategory.TabIndex = 57
        Me.btnLoadGAITCategory.Text = "Load GAIT Category"
        Me.btnLoadGAITCategory.UseVisualStyleBackColor = True
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.btnRefreshGAITDropdowns)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(767, 32)
        Me.Panel16.TabIndex = 63
        '
        'btnRefreshGAITDropdowns
        '
        Me.btnRefreshGAITDropdowns.AutoSize = True
        Me.btnRefreshGAITDropdowns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshGAITDropdowns.Location = New System.Drawing.Point(3, 3)
        Me.btnRefreshGAITDropdowns.Name = "btnRefreshGAITDropdowns"
        Me.btnRefreshGAITDropdowns.Size = New System.Drawing.Size(225, 23)
        Me.btnRefreshGAITDropdowns.TabIndex = 419
        Me.btnRefreshGAITDropdowns.Text = "Refresh Dropdowns on GAIT Inventory Tool"
        Me.btnRefreshGAITDropdowns.UseVisualStyleBackColor = True
        '
        'bgwInventory
        '
        '
        'bgwTransactions
        '
        '
        'bgwReports
        '
        '
        'PASPInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 746)
        Me.Controls.Add(Me.TCComputerInventory)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PASPInventory"
        Me.Text = "GAIT Inventory"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCComputerInventory.ResumeLayout(False)
        Me.TPInvenotry.ResumeLayout(False)
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.gbInventorySearch.ResumeLayout(False)
        Me.gbInventorySearch.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TPTransactions.ResumeLayout(False)
        CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TPReports.ResumeLayout(False)
        Me.TCReport.ResumeLayout(False)
        Me.TPGridView.ResumeLayout(False)
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPCrystalReport.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TPListTools.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TPGAITInventory.ResumeLayout(False)
        CType(Me.dgvGAITInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.pnlGAITDateSearch.ResumeLayout(False)
        Me.pnlGAITDateSearch.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.TPGAITLists.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.gbGAITQuality.ResumeLayout(False)
        Me.gbGAITQuality.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.gbGAITModelNumber.ResumeLayout(False)
        Me.gbGAITModelNumber.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.gbGAITModel.ResumeLayout(False)
        Me.gbGAITModel.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.gbGAITManufacturer.ResumeLayout(False)
        Me.gbGAITManufacturer.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.gbGAITCategory.ResumeLayout(False)
        Me.gbGAITCategory.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TCComputerInventory As System.Windows.Forms.TabControl
    Friend WithEvents TPInvenotry As System.Windows.Forms.TabPage
    Friend WithEvents TPTransactions As System.Windows.Forms.TabPage
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDNRDecal As System.Windows.Forms.TextBox
    Friend WithEvents Description As System.Windows.Forms.Label
    Friend WithEvents DTPDateAssetAcquired As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAddNewAsset As System.Windows.Forms.Button
    Friend WithEvents dgvInventory As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboInventoryType As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCost As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAssetID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSericalID As System.Windows.Forms.TextBox
    Friend WithEvents mtbReplacementDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPOID As System.Windows.Forms.TextBox
    Friend WithEvents TPListTools As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNewInventoryType As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboExistingInventoryTypes As System.Windows.Forms.ComboBox
    Friend WithEvents btnEditType As System.Windows.Forms.Button
    Friend WithEvents btnAddNewType As System.Windows.Forms.Button
    Friend WithEvents btnDeleteInventory As System.Windows.Forms.Button
    Friend WithEvents txtInventoryID As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents gbInventorySearch As System.Windows.Forms.GroupBox
    Friend WithEvents txtDecalSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboInventorySearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboSearchByStaff As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents DTPDatAcquiredStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDatAcquiredEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteAsset As System.Windows.Forms.Button
    Friend WithEvents btnUpdateAsset As System.Windows.Forms.Button
    Friend WithEvents btnClearAssetID As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSearchForAsset As System.Windows.Forms.Button
    Friend WithEvents dgvTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAssetTransaction As System.Windows.Forms.TextBox
    Friend WithEvents cboTransactionType As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTransactionID As System.Windows.Forms.TextBox
    Friend WithEvents txtTransactionComments As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents chbReplacementOrdered As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearAsset As System.Windows.Forms.Button
    Friend WithEvents btnDeleteTransaction As System.Windows.Forms.Button
    Friend WithEvents btnEditTransaction As System.Windows.Forms.Button
    Friend WithEvents btnAddTransaction As System.Windows.Forms.Button
    Friend WithEvents lblTransactionProgramUnit As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdbSecondaryUse As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPrimaryUse As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearTransactionType As System.Windows.Forms.Button
    Friend WithEvents txtTransactionIDValue As System.Windows.Forms.TextBox
    Friend WithEvents btnEditTransactionType As System.Windows.Forms.Button
    Friend WithEvents btnAddTransactionType As System.Windows.Forms.Button
    Friend WithEvents btnDeleteTransactionType As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtNewTransactionType As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cboExistingTransactionTypes As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents DTPTransactionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnClearTransactionID As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTransactionSearch As System.Windows.Forms.Button
    Friend WithEvents cboTransactionStaffSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtDecalSearch2 As System.Windows.Forms.TextBox
    Friend WithEvents cboTransactionTypeSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtTransactionIDSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCount As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtTransactionCount As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnClearTransaction As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbActiveInventory As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInactiveInventory As System.Windows.Forms.RadioButton
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtCurrentStaff As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdbInactiveSearch As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBothSearch As System.Windows.Forms.RadioButton
    Friend WithEvents rdbActiveSearch As System.Windows.Forms.RadioButton
    Friend WithEvents lblAssetProgramUnit As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnExportInventory As System.Windows.Forms.Button
    Friend WithEvents btnExportTransactions As System.Windows.Forms.Button
    Friend WithEvents TPReports As System.Windows.Forms.TabPage
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents bgwInventory As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTransactions As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnViewReport As System.Windows.Forms.Button
    Friend WithEvents cboReportUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents cboReportProgram As System.Windows.Forms.ComboBox
    Friend WithEvents bgwReports As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnViewInventoryReport As System.Windows.Forms.Button
    Friend WithEvents TCReport As System.Windows.Forms.TabControl
    Friend WithEvents TPGridView As System.Windows.Forms.TabPage
    Friend WithEvents TPCrystalReport As System.Windows.Forms.TabPage
    Friend WithEvents CRInventoryReport As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cboReportInventoryType As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnassignedInventory As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAssignedInventory As System.Windows.Forms.RadioButton
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtDNRDecalTransaction As System.Windows.Forms.TextBox
    Friend WithEvents TPGAITInventory As System.Windows.Forms.TabPage
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents btnClearGAIT As System.Windows.Forms.Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtGAITAssetTag As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtGAITID As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents cboGAITCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents cboGAITIAIPUser As System.Windows.Forms.ComboBox
    Friend WithEvents txtGAITTimeInService As System.Windows.Forms.TextBox
    Friend WithEvents txtGAITPurchaseOrder As System.Windows.Forms.TextBox
    Friend WithEvents txtGAITSerial As System.Windows.Forms.TextBox
    Friend WithEvents dtpGAITAquired As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboGAITQuality As System.Windows.Forms.ComboBox
    Friend WithEvents cboGAITModel As System.Windows.Forms.ComboBox
    Friend WithEvents cboGAITManufacturer As System.Windows.Forms.ComboBox
    Friend WithEvents dtpGAITPurchased As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvGAITInventory As System.Windows.Forms.DataGridView
    Friend WithEvents TPGAITLists As System.Windows.Forms.TabPage
    Friend WithEvents btnLoadGAITCategory As System.Windows.Forms.Button
    Friend WithEvents gbGAITModelNumber As System.Windows.Forms.GroupBox
    Friend WithEvents gbGAITModel As System.Windows.Forms.GroupBox
    Friend WithEvents gbGAITManufacturer As System.Windows.Forms.GroupBox
    Friend WithEvents gbGAITQuality As System.Windows.Forms.GroupBox
    Friend WithEvents gbGAITCategory As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearGaitCategory As System.Windows.Forms.Button
    Friend WithEvents txtGAITCategoryID As System.Windows.Forms.TextBox
    Friend WithEvents btnEditGAITCategory As System.Windows.Forms.Button
    Friend WithEvents btnAddGAITCategory As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGAITCategory As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtAddEditGAITCategory As System.Windows.Forms.TextBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents cboExistingGAITCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents rdbGAITCategoryActive As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGAITQualityActive As System.Windows.Forms.RadioButton
    Friend WithEvents btnClearGaitQuality As System.Windows.Forms.Button
    Friend WithEvents txtGAITQualityID As System.Windows.Forms.TextBox
    Friend WithEvents btnEditGAITQuality As System.Windows.Forms.Button
    Friend WithEvents btnAddGAITQuality As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGAITQuality As System.Windows.Forms.Button
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtAddEditGAITQuality As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents cboExistingGAITQuality As System.Windows.Forms.ComboBox
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents btnLoadGAITQuality As System.Windows.Forms.Button
    Friend WithEvents rdbGAITModelNumberActive As System.Windows.Forms.RadioButton
    Friend WithEvents btnClearGaitModelNumber As System.Windows.Forms.Button
    Friend WithEvents txtGAITModelNumberID As System.Windows.Forms.TextBox
    Friend WithEvents btnEditGAITModelNumber As System.Windows.Forms.Button
    Friend WithEvents btnAddGAITModelNumber As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGAITModelNumber As System.Windows.Forms.Button
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtAddEditGAITModelNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents cboExistingGAITModelNumber As System.Windows.Forms.ComboBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnLoadGAITModelNumber As System.Windows.Forms.Button
    Friend WithEvents rdbGAITModelActive As System.Windows.Forms.RadioButton
    Friend WithEvents btnClearGaitModel As System.Windows.Forms.Button
    Friend WithEvents txtGAITModelID As System.Windows.Forms.TextBox
    Friend WithEvents btnEditGAITModel As System.Windows.Forms.Button
    Friend WithEvents btnAddGAITModel As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGAITModel As System.Windows.Forms.Button
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtAddEditGAITModel As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents cboExistingGAITModel As System.Windows.Forms.ComboBox
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents btnLoadGAITModel As System.Windows.Forms.Button
    Friend WithEvents rdbGAITManufacturerActive As System.Windows.Forms.RadioButton
    Friend WithEvents btnClearGaitManufacturer As System.Windows.Forms.Button
    Friend WithEvents txtGAITManufacturerID As System.Windows.Forms.TextBox
    Friend WithEvents btnEditGAITManufacturer As System.Windows.Forms.Button
    Friend WithEvents btnAddGAITManufaturer As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGAITManufaturer As System.Windows.Forms.Button
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents txtAddEditGAITManufacturer As System.Windows.Forms.TextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents cboExistingGAITManufacturer As System.Windows.Forms.ComboBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents btnLoadGAITManufacturer As System.Windows.Forms.Button
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents cboGAITModelNumber As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddNewGAITAsset As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGAITAsset As System.Windows.Forms.Button
    Friend WithEvents btnSaveGAITAsset As System.Windows.Forms.Button
    Friend WithEvents txtGAITCount As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chbGAITUseDate As System.Windows.Forms.CheckBox
    Friend WithEvents pnlGAITDateSearch As System.Windows.Forms.Panel
    Friend WithEvents rdbGAITDateAquired As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGAITDatePurchased As System.Windows.Forms.RadioButton
    Friend WithEvents btnExportGAIT As System.Windows.Forms.Button
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents rdbGAITInactive As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGAITBoth As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGAITActive As System.Windows.Forms.RadioButton
    Friend WithEvents btnSearchGAITAssets As System.Windows.Forms.Button
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents dtpGAITDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpGAITDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboGAITProgram As System.Windows.Forms.ComboBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtGAITAssetSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents clbAssestCategory As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents btnRefreshGAITDropdowns As System.Windows.Forms.Button
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents txtGAITComment As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtGAITCommentSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents rdbDeleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbActive As System.Windows.Forms.RadioButton
End Class
