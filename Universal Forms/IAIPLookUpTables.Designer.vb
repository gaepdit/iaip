<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPLookUpTables
    Inherits DefaultForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPLookUpTables))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TCLookUpTables = New System.Windows.Forms.TabControl
        Me.TPGeneralTables = New System.Windows.Forms.TabPage
        Me.TCGeneralTables = New System.Windows.Forms.TabControl
        Me.TPAPBManagement = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgvLookUpManagement = New System.Windows.Forms.DataGridView
        Me.pnlAPBManagement = New System.Windows.Forms.Panel
        Me.btnViewAllPastTypes = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboManagementType = New System.Windows.Forms.ComboBox
        Me.btnClearManagement = New System.Windows.Forms.Button
        Me.txtAPBManagemetnID = New System.Windows.Forms.TextBox
        Me.chbAPBMangementVacant = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAPBManagementName = New System.Windows.Forms.TextBox
        Me.btnSaveAPBManagement = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnLoadAPBManagement = New System.Windows.Forms.Button
        Me.TPCountryInformation = New System.Windows.Forms.TabPage
        Me.TPDistrictInformation = New System.Windows.Forms.TabPage
        Me.TPDistrictOffices = New System.Windows.Forms.TabPage
        Me.TPDistricts = New System.Windows.Forms.TabPage
        Me.TPEPDBranches = New System.Windows.Forms.TabPage
        Me.TPEPDPrograms = New System.Windows.Forms.TabPage
        Me.TPEPDUnits = New System.Windows.Forms.TabPage
        Me.TPPermitting = New System.Windows.Forms.TabPage
        Me.TCPermitting = New System.Windows.Forms.TabControl
        Me.TPApplicationTypes = New System.Windows.Forms.TabPage
        Me.btnEditAppType = New System.Windows.Forms.Button
        Me.btnDeleteAppType = New System.Windows.Forms.Button
        Me.dgvApplicationType = New System.Windows.Forms.DataGridView
        Me.btnClearAppTypes = New System.Windows.Forms.Button
        Me.txtApplicationID = New System.Windows.Forms.TextBox
        Me.chbActiveAppType = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtApplicationDesc = New System.Windows.Forms.TextBox
        Me.btnAddNewAppType = New System.Windows.Forms.Button
        Me.btnLoadApplicationTypes = New System.Windows.Forms.Button
        Me.TPPermittingUnits = New System.Windows.Forms.TabPage
        Me.TPPermittingTypes = New System.Windows.Forms.TabPage
        Me.TPCompliance = New System.Windows.Forms.TabPage
        Me.TCCompliance = New System.Windows.Forms.TabControl
        Me.TPComplianceActivities = New System.Windows.Forms.TabPage
        Me.TPComplianceStatus = New System.Windows.Forms.TabPage
        Me.TPComplianceUnits = New System.Windows.Forms.TabPage
        Me.TPHPVViolations = New System.Windows.Forms.TabPage
        Me.TPSSCPNotifications = New System.Windows.Forms.TabPage
        Me.TPMonitoring = New System.Windows.Forms.TabPage
        Me.TCMonitoring = New System.Windows.Forms.TabControl
        Me.TPISMPComplianceStatus = New System.Windows.Forms.TabPage
        Me.TPISMPMethods = New System.Windows.Forms.TabPage
        Me.TPMonitoringUnits = New System.Windows.Forms.TabPage
        Me.TPTestingFirms = New System.Windows.Forms.TabPage
        Me.TPUnits = New System.Windows.Forms.TabPage
        Me.TPProgramSupport = New System.Windows.Forms.TabPage
        Me.TCProgramSupport = New System.Windows.Forms.TabControl
        Me.TPPASPInventoryType = New System.Windows.Forms.TabPage
        Me.TPPASPTransactionType = New System.Windows.Forms.TabPage
        Me.TPOther = New System.Windows.Forms.TabPage
        Me.TCOther = New System.Windows.Forms.TabControl
        Me.TPEmployeeStatus = New System.Windows.Forms.TabPage
        Me.TPIAIPAccounts = New System.Windows.Forms.TabPage
        Me.TPIAIPForms = New System.Windows.Forms.TabPage
        Me.TPSubPart60 = New System.Windows.Forms.TabPage
        Me.TPSubPart61 = New System.Windows.Forms.TabPage
        Me.TPPollutants = New System.Windows.Forms.TabPage
        Me.TPSICCodes = New System.Windows.Forms.TabPage
        Me.TPSubPart63 = New System.Windows.Forms.TabPage
        Me.TPSubPartSIP = New System.Windows.Forms.TabPage
        Me.StatusStrip1.SuspendLayout()
        Me.TCLookUpTables.SuspendLayout()
        Me.TPGeneralTables.SuspendLayout()
        Me.TCGeneralTables.SuspendLayout()
        Me.TPAPBManagement.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAPBManagement.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TPPermitting.SuspendLayout()
        Me.TCPermitting.SuspendLayout()
        Me.TPApplicationTypes.SuspendLayout()
        CType(Me.dgvApplicationType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPCompliance.SuspendLayout()
        Me.TCCompliance.SuspendLayout()
        Me.TPMonitoring.SuspendLayout()
        Me.TCMonitoring.SuspendLayout()
        Me.TPProgramSupport.SuspendLayout()
        Me.TCProgramSupport.SuspendLayout()
        Me.TPOther.SuspendLayout()
        Me.TCOther.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MmiView, Me.MenuItem2, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "File"
        '
        'MmiView
        '
        Me.MmiView.Index = 1
        Me.MmiView.Text = "View"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.Text = "Tools"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 3
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp})
        Me.mmiHelp.Text = "Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online Help"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 523)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 0
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
        'TCLookUpTables
        '
        Me.TCLookUpTables.Controls.Add(Me.TPGeneralTables)
        Me.TCLookUpTables.Controls.Add(Me.TPPermitting)
        Me.TCLookUpTables.Controls.Add(Me.TPCompliance)
        Me.TCLookUpTables.Controls.Add(Me.TPMonitoring)
        Me.TCLookUpTables.Controls.Add(Me.TPProgramSupport)
        Me.TCLookUpTables.Controls.Add(Me.TPOther)
        Me.TCLookUpTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCLookUpTables.Location = New System.Drawing.Point(0, 0)
        Me.TCLookUpTables.Name = "TCLookUpTables"
        Me.TCLookUpTables.SelectedIndex = 0
        Me.TCLookUpTables.Size = New System.Drawing.Size(792, 523)
        Me.TCLookUpTables.TabIndex = 1
        '
        'TPGeneralTables
        '
        Me.TPGeneralTables.Controls.Add(Me.TCGeneralTables)
        Me.TPGeneralTables.Location = New System.Drawing.Point(4, 22)
        Me.TPGeneralTables.Name = "TPGeneralTables"
        Me.TPGeneralTables.Padding = New System.Windows.Forms.Padding(3)
        Me.TPGeneralTables.Size = New System.Drawing.Size(784, 497)
        Me.TPGeneralTables.TabIndex = 0
        Me.TPGeneralTables.Text = "General Tables"
        Me.TPGeneralTables.UseVisualStyleBackColor = True
        '
        'TCGeneralTables
        '
        Me.TCGeneralTables.Controls.Add(Me.TPAPBManagement)
        Me.TCGeneralTables.Controls.Add(Me.TPCountryInformation)
        Me.TCGeneralTables.Controls.Add(Me.TPDistrictInformation)
        Me.TCGeneralTables.Controls.Add(Me.TPDistrictOffices)
        Me.TCGeneralTables.Controls.Add(Me.TPDistricts)
        Me.TCGeneralTables.Controls.Add(Me.TPEPDBranches)
        Me.TCGeneralTables.Controls.Add(Me.TPEPDPrograms)
        Me.TCGeneralTables.Controls.Add(Me.TPEPDUnits)
        Me.TCGeneralTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCGeneralTables.Location = New System.Drawing.Point(3, 3)
        Me.TCGeneralTables.Name = "TCGeneralTables"
        Me.TCGeneralTables.SelectedIndex = 0
        Me.TCGeneralTables.Size = New System.Drawing.Size(778, 491)
        Me.TCGeneralTables.TabIndex = 9
        '
        'TPAPBManagement
        '
        Me.TPAPBManagement.Controls.Add(Me.Panel1)
        Me.TPAPBManagement.Controls.Add(Me.pnlAPBManagement)
        Me.TPAPBManagement.Controls.Add(Me.Panel2)
        Me.TPAPBManagement.Location = New System.Drawing.Point(4, 22)
        Me.TPAPBManagement.Name = "TPAPBManagement"
        Me.TPAPBManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAPBManagement.Size = New System.Drawing.Size(770, 465)
        Me.TPAPBManagement.TabIndex = 0
        Me.TPAPBManagement.Text = "APB Management"
        Me.TPAPBManagement.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgvLookUpManagement)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 167)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(764, 295)
        Me.Panel1.TabIndex = 57
        '
        'dgvLookUpManagement
        '
        Me.dgvLookUpManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLookUpManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLookUpManagement.Location = New System.Drawing.Point(0, 0)
        Me.dgvLookUpManagement.Name = "dgvLookUpManagement"
        Me.dgvLookUpManagement.ReadOnly = True
        Me.dgvLookUpManagement.Size = New System.Drawing.Size(764, 295)
        Me.dgvLookUpManagement.TabIndex = 56
        Me.dgvLookUpManagement.Visible = False
        '
        'pnlAPBManagement
        '
        Me.pnlAPBManagement.Controls.Add(Me.btnViewAllPastTypes)
        Me.pnlAPBManagement.Controls.Add(Me.Label5)
        Me.pnlAPBManagement.Controls.Add(Me.cboManagementType)
        Me.pnlAPBManagement.Controls.Add(Me.btnClearManagement)
        Me.pnlAPBManagement.Controls.Add(Me.txtAPBManagemetnID)
        Me.pnlAPBManagement.Controls.Add(Me.chbAPBMangementVacant)
        Me.pnlAPBManagement.Controls.Add(Me.Label3)
        Me.pnlAPBManagement.Controls.Add(Me.Label4)
        Me.pnlAPBManagement.Controls.Add(Me.txtAPBManagementName)
        Me.pnlAPBManagement.Controls.Add(Me.btnSaveAPBManagement)
        Me.pnlAPBManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAPBManagement.Location = New System.Drawing.Point(3, 36)
        Me.pnlAPBManagement.Name = "pnlAPBManagement"
        Me.pnlAPBManagement.Size = New System.Drawing.Size(764, 131)
        Me.pnlAPBManagement.TabIndex = 54
        Me.pnlAPBManagement.Visible = False
        '
        'btnViewAllPastTypes
        '
        Me.btnViewAllPastTypes.AutoSize = True
        Me.btnViewAllPastTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewAllPastTypes.Location = New System.Drawing.Point(367, 32)
        Me.btnViewAllPastTypes.Name = "btnViewAllPastTypes"
        Me.btnViewAllPastTypes.Size = New System.Drawing.Size(108, 23)
        Me.btnViewAllPastTypes.TabIndex = 66
        Me.btnViewAllPastTypes.Text = "View all past Types"
        Me.btnViewAllPastTypes.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Management Type"
        '
        'cboManagementType
        '
        Me.cboManagementType.FormattingEnabled = True
        Me.cboManagementType.Location = New System.Drawing.Point(118, 34)
        Me.cboManagementType.Name = "cboManagementType"
        Me.cboManagementType.Size = New System.Drawing.Size(163, 21)
        Me.cboManagementType.TabIndex = 62
        '
        'btnClearManagement
        '
        Me.btnClearManagement.AutoSize = True
        Me.btnClearManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearManagement.Image = CType(resources.GetObject("btnClearManagement.Image"), System.Drawing.Image)
        Me.btnClearManagement.Location = New System.Drawing.Point(131, 6)
        Me.btnClearManagement.Name = "btnClearManagement"
        Me.btnClearManagement.Size = New System.Drawing.Size(22, 22)
        Me.btnClearManagement.TabIndex = 59
        Me.btnClearManagement.UseVisualStyleBackColor = True
        '
        'txtAPBManagemetnID
        '
        Me.txtAPBManagemetnID.Location = New System.Drawing.Point(65, 8)
        Me.txtAPBManagemetnID.Name = "txtAPBManagemetnID"
        Me.txtAPBManagemetnID.ReadOnly = True
        Me.txtAPBManagemetnID.Size = New System.Drawing.Size(51, 20)
        Me.txtAPBManagemetnID.TabIndex = 56
        '
        'chbAPBMangementVacant
        '
        Me.chbAPBMangementVacant.AutoSize = True
        Me.chbAPBMangementVacant.Location = New System.Drawing.Point(367, 60)
        Me.chbAPBMangementVacant.Name = "chbAPBMangementVacant"
        Me.chbAPBMangementVacant.Size = New System.Drawing.Size(100, 17)
        Me.chbAPBMangementVacant.TabIndex = 58
        Me.chbAPBMangementVacant.Text = "Vacant Position"
        Me.chbAPBMangementVacant.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "ID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Full Name"
        '
        'txtAPBManagementName
        '
        Me.txtAPBManagementName.Location = New System.Drawing.Point(118, 58)
        Me.txtAPBManagementName.Name = "txtAPBManagementName"
        Me.txtAPBManagementName.Size = New System.Drawing.Size(228, 20)
        Me.txtAPBManagementName.TabIndex = 54
        '
        'btnSaveAPBManagement
        '
        Me.btnSaveAPBManagement.AutoSize = True
        Me.btnSaveAPBManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveAPBManagement.Location = New System.Drawing.Point(118, 86)
        Me.btnSaveAPBManagement.Name = "btnSaveAPBManagement"
        Me.btnSaveAPBManagement.Size = New System.Drawing.Size(107, 23)
        Me.btnSaveAPBManagement.TabIndex = 55
        Me.btnSaveAPBManagement.Text = "Save Management"
        Me.btnSaveAPBManagement.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnLoadAPBManagement)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(764, 33)
        Me.Panel2.TabIndex = 55
        '
        'btnLoadAPBManagement
        '
        Me.btnLoadAPBManagement.AutoSize = True
        Me.btnLoadAPBManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadAPBManagement.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadAPBManagement.Name = "btnLoadAPBManagement"
        Me.btnLoadAPBManagement.Size = New System.Drawing.Size(130, 23)
        Me.btnLoadAPBManagement.TabIndex = 52
        Me.btnLoadAPBManagement.Text = "Load APB Management"
        Me.btnLoadAPBManagement.UseVisualStyleBackColor = True
        '
        'TPCountryInformation
        '
        Me.TPCountryInformation.Location = New System.Drawing.Point(4, 22)
        Me.TPCountryInformation.Name = "TPCountryInformation"
        Me.TPCountryInformation.Padding = New System.Windows.Forms.Padding(3)
        Me.TPCountryInformation.Size = New System.Drawing.Size(770, 465)
        Me.TPCountryInformation.TabIndex = 1
        Me.TPCountryInformation.Text = "Country Information"
        Me.TPCountryInformation.UseVisualStyleBackColor = True
        '
        'TPDistrictInformation
        '
        Me.TPDistrictInformation.Location = New System.Drawing.Point(4, 22)
        Me.TPDistrictInformation.Name = "TPDistrictInformation"
        Me.TPDistrictInformation.Padding = New System.Windows.Forms.Padding(3)
        Me.TPDistrictInformation.Size = New System.Drawing.Size(770, 465)
        Me.TPDistrictInformation.TabIndex = 2
        Me.TPDistrictInformation.Text = "District Information"
        Me.TPDistrictInformation.UseVisualStyleBackColor = True
        '
        'TPDistrictOffices
        '
        Me.TPDistrictOffices.Location = New System.Drawing.Point(4, 22)
        Me.TPDistrictOffices.Name = "TPDistrictOffices"
        Me.TPDistrictOffices.Padding = New System.Windows.Forms.Padding(3)
        Me.TPDistrictOffices.Size = New System.Drawing.Size(770, 465)
        Me.TPDistrictOffices.TabIndex = 3
        Me.TPDistrictOffices.Text = "District Office"
        Me.TPDistrictOffices.UseVisualStyleBackColor = True
        '
        'TPDistricts
        '
        Me.TPDistricts.Location = New System.Drawing.Point(4, 22)
        Me.TPDistricts.Name = "TPDistricts"
        Me.TPDistricts.Padding = New System.Windows.Forms.Padding(3)
        Me.TPDistricts.Size = New System.Drawing.Size(770, 465)
        Me.TPDistricts.TabIndex = 4
        Me.TPDistricts.Text = "Districts"
        Me.TPDistricts.UseVisualStyleBackColor = True
        '
        'TPEPDBranches
        '
        Me.TPEPDBranches.Location = New System.Drawing.Point(4, 22)
        Me.TPEPDBranches.Name = "TPEPDBranches"
        Me.TPEPDBranches.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEPDBranches.Size = New System.Drawing.Size(770, 465)
        Me.TPEPDBranches.TabIndex = 5
        Me.TPEPDBranches.Text = "EPD Branches"
        Me.TPEPDBranches.UseVisualStyleBackColor = True
        '
        'TPEPDPrograms
        '
        Me.TPEPDPrograms.Location = New System.Drawing.Point(4, 22)
        Me.TPEPDPrograms.Name = "TPEPDPrograms"
        Me.TPEPDPrograms.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEPDPrograms.Size = New System.Drawing.Size(770, 465)
        Me.TPEPDPrograms.TabIndex = 6
        Me.TPEPDPrograms.Text = "EPD Programs"
        Me.TPEPDPrograms.UseVisualStyleBackColor = True
        '
        'TPEPDUnits
        '
        Me.TPEPDUnits.Location = New System.Drawing.Point(4, 22)
        Me.TPEPDUnits.Name = "TPEPDUnits"
        Me.TPEPDUnits.Size = New System.Drawing.Size(770, 465)
        Me.TPEPDUnits.TabIndex = 7
        Me.TPEPDUnits.Text = "EPD Units"
        Me.TPEPDUnits.UseVisualStyleBackColor = True
        '
        'TPPermitting
        '
        Me.TPPermitting.Controls.Add(Me.TCPermitting)
        Me.TPPermitting.Location = New System.Drawing.Point(4, 22)
        Me.TPPermitting.Name = "TPPermitting"
        Me.TPPermitting.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPermitting.Size = New System.Drawing.Size(784, 497)
        Me.TPPermitting.TabIndex = 1
        Me.TPPermitting.Text = "Permitting"
        Me.TPPermitting.UseVisualStyleBackColor = True
        '
        'TCPermitting
        '
        Me.TCPermitting.Controls.Add(Me.TPApplicationTypes)
        Me.TCPermitting.Controls.Add(Me.TPPermittingUnits)
        Me.TCPermitting.Controls.Add(Me.TPPermittingTypes)
        Me.TCPermitting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCPermitting.Location = New System.Drawing.Point(3, 3)
        Me.TCPermitting.Name = "TCPermitting"
        Me.TCPermitting.SelectedIndex = 0
        Me.TCPermitting.Size = New System.Drawing.Size(778, 491)
        Me.TCPermitting.TabIndex = 12
        '
        'TPApplicationTypes
        '
        Me.TPApplicationTypes.Controls.Add(Me.btnEditAppType)
        Me.TPApplicationTypes.Controls.Add(Me.btnDeleteAppType)
        Me.TPApplicationTypes.Controls.Add(Me.dgvApplicationType)
        Me.TPApplicationTypes.Controls.Add(Me.btnClearAppTypes)
        Me.TPApplicationTypes.Controls.Add(Me.txtApplicationID)
        Me.TPApplicationTypes.Controls.Add(Me.chbActiveAppType)
        Me.TPApplicationTypes.Controls.Add(Me.Label1)
        Me.TPApplicationTypes.Controls.Add(Me.Label2)
        Me.TPApplicationTypes.Controls.Add(Me.txtApplicationDesc)
        Me.TPApplicationTypes.Controls.Add(Me.btnAddNewAppType)
        Me.TPApplicationTypes.Controls.Add(Me.btnLoadApplicationTypes)
        Me.TPApplicationTypes.Location = New System.Drawing.Point(4, 22)
        Me.TPApplicationTypes.Name = "TPApplicationTypes"
        Me.TPApplicationTypes.Padding = New System.Windows.Forms.Padding(3)
        Me.TPApplicationTypes.Size = New System.Drawing.Size(770, 465)
        Me.TPApplicationTypes.TabIndex = 0
        Me.TPApplicationTypes.Text = "Application Types"
        Me.TPApplicationTypes.UseVisualStyleBackColor = True
        '
        'btnEditAppType
        '
        Me.btnEditAppType.AutoSize = True
        Me.btnEditAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditAppType.Location = New System.Drawing.Point(423, 60)
        Me.btnEditAppType.Name = "btnEditAppType"
        Me.btnEditAppType.Size = New System.Drawing.Size(57, 23)
        Me.btnEditAppType.TabIndex = 10
        Me.btnEditAppType.Text = "Edit App"
        Me.btnEditAppType.UseVisualStyleBackColor = True
        '
        'btnDeleteAppType
        '
        Me.btnDeleteAppType.AutoSize = True
        Me.btnDeleteAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteAppType.Location = New System.Drawing.Point(641, 6)
        Me.btnDeleteAppType.Name = "btnDeleteAppType"
        Me.btnDeleteAppType.Size = New System.Drawing.Size(97, 23)
        Me.btnDeleteAppType.TabIndex = 9
        Me.btnDeleteAppType.Text = "Delete App Type"
        Me.btnDeleteAppType.UseVisualStyleBackColor = True
        '
        'dgvApplicationType
        '
        Me.dgvApplicationType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplicationType.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvApplicationType.Location = New System.Drawing.Point(3, 105)
        Me.dgvApplicationType.Name = "dgvApplicationType"
        Me.dgvApplicationType.ReadOnly = True
        Me.dgvApplicationType.Size = New System.Drawing.Size(764, 357)
        Me.dgvApplicationType.TabIndex = 4
        '
        'btnClearAppTypes
        '
        Me.btnClearAppTypes.AutoSize = True
        Me.btnClearAppTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAppTypes.Image = CType(resources.GetObject("btnClearAppTypes.Image"), System.Drawing.Image)
        Me.btnClearAppTypes.Location = New System.Drawing.Point(357, 5)
        Me.btnClearAppTypes.Name = "btnClearAppTypes"
        Me.btnClearAppTypes.Size = New System.Drawing.Size(22, 22)
        Me.btnClearAppTypes.TabIndex = 8
        Me.btnClearAppTypes.UseVisualStyleBackColor = True
        '
        'txtApplicationID
        '
        Me.txtApplicationID.Location = New System.Drawing.Point(300, 8)
        Me.txtApplicationID.Name = "txtApplicationID"
        Me.txtApplicationID.ReadOnly = True
        Me.txtApplicationID.Size = New System.Drawing.Size(51, 20)
        Me.txtApplicationID.TabIndex = 5
        '
        'chbActiveAppType
        '
        Me.chbActiveAppType.AutoSize = True
        Me.chbActiveAppType.Location = New System.Drawing.Point(481, 34)
        Me.chbActiveAppType.Name = "chbActiveAppType"
        Me.chbActiveAppType.Size = New System.Drawing.Size(81, 17)
        Me.chbActiveAppType.TabIndex = 7
        Me.chbActiveAppType.Text = "Active App."
        Me.chbActiveAppType.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(276, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(207, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Application Desc"
        '
        'txtApplicationDesc
        '
        Me.txtApplicationDesc.Location = New System.Drawing.Point(300, 34)
        Me.txtApplicationDesc.Name = "txtApplicationDesc"
        Me.txtApplicationDesc.Size = New System.Drawing.Size(175, 20)
        Me.txtApplicationDesc.TabIndex = 1
        '
        'btnAddNewAppType
        '
        Me.btnAddNewAppType.AutoSize = True
        Me.btnAddNewAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewAppType.Location = New System.Drawing.Point(300, 60)
        Me.btnAddNewAppType.Name = "btnAddNewAppType"
        Me.btnAddNewAppType.Size = New System.Drawing.Size(83, 23)
        Me.btnAddNewAppType.TabIndex = 2
        Me.btnAddNewAppType.Text = "Add New App"
        Me.btnAddNewAppType.UseVisualStyleBackColor = True
        '
        'btnLoadApplicationTypes
        '
        Me.btnLoadApplicationTypes.AutoSize = True
        Me.btnLoadApplicationTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadApplicationTypes.Location = New System.Drawing.Point(10, 8)
        Me.btnLoadApplicationTypes.Name = "btnLoadApplicationTypes"
        Me.btnLoadApplicationTypes.Size = New System.Drawing.Size(134, 23)
        Me.btnLoadApplicationTypes.TabIndex = 3
        Me.btnLoadApplicationTypes.Text = "Load Application Type(s)"
        Me.btnLoadApplicationTypes.UseVisualStyleBackColor = True
        '
        'TPPermittingUnits
        '
        Me.TPPermittingUnits.Location = New System.Drawing.Point(4, 22)
        Me.TPPermittingUnits.Name = "TPPermittingUnits"
        Me.TPPermittingUnits.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPermittingUnits.Size = New System.Drawing.Size(770, 465)
        Me.TPPermittingUnits.TabIndex = 1
        Me.TPPermittingUnits.Text = "Permitting Units"
        Me.TPPermittingUnits.UseVisualStyleBackColor = True
        '
        'TPPermittingTypes
        '
        Me.TPPermittingTypes.Location = New System.Drawing.Point(4, 22)
        Me.TPPermittingTypes.Name = "TPPermittingTypes"
        Me.TPPermittingTypes.Size = New System.Drawing.Size(770, 465)
        Me.TPPermittingTypes.TabIndex = 2
        Me.TPPermittingTypes.Text = "Permitting Types"
        Me.TPPermittingTypes.UseVisualStyleBackColor = True
        '
        'TPCompliance
        '
        Me.TPCompliance.Controls.Add(Me.TCCompliance)
        Me.TPCompliance.Location = New System.Drawing.Point(4, 22)
        Me.TPCompliance.Name = "TPCompliance"
        Me.TPCompliance.Size = New System.Drawing.Size(784, 497)
        Me.TPCompliance.TabIndex = 2
        Me.TPCompliance.Text = "Compliance"
        Me.TPCompliance.UseVisualStyleBackColor = True
        '
        'TCCompliance
        '
        Me.TCCompliance.Controls.Add(Me.TPComplianceActivities)
        Me.TCCompliance.Controls.Add(Me.TPComplianceStatus)
        Me.TCCompliance.Controls.Add(Me.TPComplianceUnits)
        Me.TCCompliance.Controls.Add(Me.TPHPVViolations)
        Me.TCCompliance.Controls.Add(Me.TPSSCPNotifications)
        Me.TCCompliance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCCompliance.Location = New System.Drawing.Point(0, 0)
        Me.TCCompliance.Name = "TCCompliance"
        Me.TCCompliance.SelectedIndex = 0
        Me.TCCompliance.Size = New System.Drawing.Size(784, 497)
        Me.TCCompliance.TabIndex = 11
        '
        'TPComplianceActivities
        '
        Me.TPComplianceActivities.Location = New System.Drawing.Point(4, 22)
        Me.TPComplianceActivities.Name = "TPComplianceActivities"
        Me.TPComplianceActivities.Padding = New System.Windows.Forms.Padding(3)
        Me.TPComplianceActivities.Size = New System.Drawing.Size(776, 471)
        Me.TPComplianceActivities.TabIndex = 0
        Me.TPComplianceActivities.Text = "Compliance Activities"
        Me.TPComplianceActivities.UseVisualStyleBackColor = True
        '
        'TPComplianceStatus
        '
        Me.TPComplianceStatus.Location = New System.Drawing.Point(4, 22)
        Me.TPComplianceStatus.Name = "TPComplianceStatus"
        Me.TPComplianceStatus.Padding = New System.Windows.Forms.Padding(3)
        Me.TPComplianceStatus.Size = New System.Drawing.Size(776, 471)
        Me.TPComplianceStatus.TabIndex = 1
        Me.TPComplianceStatus.Text = "Compliance Status"
        Me.TPComplianceStatus.UseVisualStyleBackColor = True
        '
        'TPComplianceUnits
        '
        Me.TPComplianceUnits.Location = New System.Drawing.Point(4, 22)
        Me.TPComplianceUnits.Name = "TPComplianceUnits"
        Me.TPComplianceUnits.Padding = New System.Windows.Forms.Padding(3)
        Me.TPComplianceUnits.Size = New System.Drawing.Size(776, 471)
        Me.TPComplianceUnits.TabIndex = 2
        Me.TPComplianceUnits.Text = "Compliance Units"
        Me.TPComplianceUnits.UseVisualStyleBackColor = True
        '
        'TPHPVViolations
        '
        Me.TPHPVViolations.Location = New System.Drawing.Point(4, 22)
        Me.TPHPVViolations.Name = "TPHPVViolations"
        Me.TPHPVViolations.Padding = New System.Windows.Forms.Padding(3)
        Me.TPHPVViolations.Size = New System.Drawing.Size(776, 471)
        Me.TPHPVViolations.TabIndex = 3
        Me.TPHPVViolations.Text = "HPV Violations"
        Me.TPHPVViolations.UseVisualStyleBackColor = True
        '
        'TPSSCPNotifications
        '
        Me.TPSSCPNotifications.Location = New System.Drawing.Point(4, 22)
        Me.TPSSCPNotifications.Name = "TPSSCPNotifications"
        Me.TPSSCPNotifications.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSSCPNotifications.Size = New System.Drawing.Size(776, 471)
        Me.TPSSCPNotifications.TabIndex = 4
        Me.TPSSCPNotifications.Text = "SSCP Notifications"
        Me.TPSSCPNotifications.UseVisualStyleBackColor = True
        '
        'TPMonitoring
        '
        Me.TPMonitoring.Controls.Add(Me.TCMonitoring)
        Me.TPMonitoring.Location = New System.Drawing.Point(4, 22)
        Me.TPMonitoring.Name = "TPMonitoring"
        Me.TPMonitoring.Size = New System.Drawing.Size(784, 497)
        Me.TPMonitoring.TabIndex = 3
        Me.TPMonitoring.Text = "Monitoring"
        Me.TPMonitoring.UseVisualStyleBackColor = True
        '
        'TCMonitoring
        '
        Me.TCMonitoring.Controls.Add(Me.TPISMPComplianceStatus)
        Me.TCMonitoring.Controls.Add(Me.TPISMPMethods)
        Me.TCMonitoring.Controls.Add(Me.TPMonitoringUnits)
        Me.TCMonitoring.Controls.Add(Me.TPTestingFirms)
        Me.TCMonitoring.Controls.Add(Me.TPUnits)
        Me.TCMonitoring.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMonitoring.Location = New System.Drawing.Point(0, 0)
        Me.TCMonitoring.Name = "TCMonitoring"
        Me.TCMonitoring.SelectedIndex = 0
        Me.TCMonitoring.Size = New System.Drawing.Size(784, 497)
        Me.TCMonitoring.TabIndex = 14
        '
        'TPISMPComplianceStatus
        '
        Me.TPISMPComplianceStatus.Location = New System.Drawing.Point(4, 22)
        Me.TPISMPComplianceStatus.Name = "TPISMPComplianceStatus"
        Me.TPISMPComplianceStatus.Padding = New System.Windows.Forms.Padding(3)
        Me.TPISMPComplianceStatus.Size = New System.Drawing.Size(776, 471)
        Me.TPISMPComplianceStatus.TabIndex = 0
        Me.TPISMPComplianceStatus.Text = "ISMP Compliance Status"
        Me.TPISMPComplianceStatus.UseVisualStyleBackColor = True
        '
        'TPISMPMethods
        '
        Me.TPISMPMethods.Location = New System.Drawing.Point(4, 22)
        Me.TPISMPMethods.Name = "TPISMPMethods"
        Me.TPISMPMethods.Padding = New System.Windows.Forms.Padding(3)
        Me.TPISMPMethods.Size = New System.Drawing.Size(776, 471)
        Me.TPISMPMethods.TabIndex = 1
        Me.TPISMPMethods.Text = "ISMP Methods"
        Me.TPISMPMethods.UseVisualStyleBackColor = True
        '
        'TPMonitoringUnits
        '
        Me.TPMonitoringUnits.Location = New System.Drawing.Point(4, 22)
        Me.TPMonitoringUnits.Name = "TPMonitoringUnits"
        Me.TPMonitoringUnits.Padding = New System.Windows.Forms.Padding(3)
        Me.TPMonitoringUnits.Size = New System.Drawing.Size(776, 471)
        Me.TPMonitoringUnits.TabIndex = 2
        Me.TPMonitoringUnits.Text = "Monitoring Units"
        Me.TPMonitoringUnits.UseVisualStyleBackColor = True
        '
        'TPTestingFirms
        '
        Me.TPTestingFirms.Location = New System.Drawing.Point(4, 22)
        Me.TPTestingFirms.Name = "TPTestingFirms"
        Me.TPTestingFirms.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTestingFirms.Size = New System.Drawing.Size(776, 471)
        Me.TPTestingFirms.TabIndex = 3
        Me.TPTestingFirms.Text = "Testing Firms"
        Me.TPTestingFirms.UseVisualStyleBackColor = True
        '
        'TPUnits
        '
        Me.TPUnits.Location = New System.Drawing.Point(4, 22)
        Me.TPUnits.Name = "TPUnits"
        Me.TPUnits.Padding = New System.Windows.Forms.Padding(3)
        Me.TPUnits.Size = New System.Drawing.Size(776, 471)
        Me.TPUnits.TabIndex = 4
        Me.TPUnits.Text = "Units"
        Me.TPUnits.UseVisualStyleBackColor = True
        '
        'TPProgramSupport
        '
        Me.TPProgramSupport.Controls.Add(Me.TCProgramSupport)
        Me.TPProgramSupport.Location = New System.Drawing.Point(4, 22)
        Me.TPProgramSupport.Name = "TPProgramSupport"
        Me.TPProgramSupport.Size = New System.Drawing.Size(784, 497)
        Me.TPProgramSupport.TabIndex = 4
        Me.TPProgramSupport.Text = "Program Support"
        Me.TPProgramSupport.UseVisualStyleBackColor = True
        '
        'TCProgramSupport
        '
        Me.TCProgramSupport.Controls.Add(Me.TPPASPInventoryType)
        Me.TCProgramSupport.Controls.Add(Me.TPPASPTransactionType)
        Me.TCProgramSupport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCProgramSupport.Location = New System.Drawing.Point(0, 0)
        Me.TCProgramSupport.Name = "TCProgramSupport"
        Me.TCProgramSupport.SelectedIndex = 0
        Me.TCProgramSupport.Size = New System.Drawing.Size(784, 497)
        Me.TCProgramSupport.TabIndex = 11
        '
        'TPPASPInventoryType
        '
        Me.TPPASPInventoryType.Location = New System.Drawing.Point(4, 22)
        Me.TPPASPInventoryType.Name = "TPPASPInventoryType"
        Me.TPPASPInventoryType.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPASPInventoryType.Size = New System.Drawing.Size(776, 471)
        Me.TPPASPInventoryType.TabIndex = 0
        Me.TPPASPInventoryType.Text = "PASP Inventory Type"
        Me.TPPASPInventoryType.UseVisualStyleBackColor = True
        '
        'TPPASPTransactionType
        '
        Me.TPPASPTransactionType.Location = New System.Drawing.Point(4, 22)
        Me.TPPASPTransactionType.Name = "TPPASPTransactionType"
        Me.TPPASPTransactionType.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPASPTransactionType.Size = New System.Drawing.Size(776, 471)
        Me.TPPASPTransactionType.TabIndex = 1
        Me.TPPASPTransactionType.Text = "PASP Transaction Type"
        Me.TPPASPTransactionType.UseVisualStyleBackColor = True
        '
        'TPOther
        '
        Me.TPOther.Controls.Add(Me.TCOther)
        Me.TPOther.Location = New System.Drawing.Point(4, 22)
        Me.TPOther.Name = "TPOther"
        Me.TPOther.Size = New System.Drawing.Size(784, 497)
        Me.TPOther.TabIndex = 5
        Me.TPOther.Text = "Other"
        Me.TPOther.UseVisualStyleBackColor = True
        '
        'TCOther
        '
        Me.TCOther.Controls.Add(Me.TPEmployeeStatus)
        Me.TCOther.Controls.Add(Me.TPIAIPAccounts)
        Me.TCOther.Controls.Add(Me.TPIAIPForms)
        Me.TCOther.Controls.Add(Me.TPSubPart60)
        Me.TCOther.Controls.Add(Me.TPSubPart61)
        Me.TCOther.Controls.Add(Me.TPPollutants)
        Me.TCOther.Controls.Add(Me.TPSICCodes)
        Me.TCOther.Controls.Add(Me.TPSubPart63)
        Me.TCOther.Controls.Add(Me.TPSubPartSIP)
        Me.TCOther.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCOther.Location = New System.Drawing.Point(0, 0)
        Me.TCOther.Name = "TCOther"
        Me.TCOther.SelectedIndex = 0
        Me.TCOther.Size = New System.Drawing.Size(784, 497)
        Me.TCOther.TabIndex = 16
        '
        'TPEmployeeStatus
        '
        Me.TPEmployeeStatus.Location = New System.Drawing.Point(4, 22)
        Me.TPEmployeeStatus.Name = "TPEmployeeStatus"
        Me.TPEmployeeStatus.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEmployeeStatus.Size = New System.Drawing.Size(776, 471)
        Me.TPEmployeeStatus.TabIndex = 0
        Me.TPEmployeeStatus.Text = "Employee Status"
        Me.TPEmployeeStatus.UseVisualStyleBackColor = True
        '
        'TPIAIPAccounts
        '
        Me.TPIAIPAccounts.Location = New System.Drawing.Point(4, 22)
        Me.TPIAIPAccounts.Name = "TPIAIPAccounts"
        Me.TPIAIPAccounts.Padding = New System.Windows.Forms.Padding(3)
        Me.TPIAIPAccounts.Size = New System.Drawing.Size(776, 471)
        Me.TPIAIPAccounts.TabIndex = 1
        Me.TPIAIPAccounts.Text = "IAIP Accounts"
        Me.TPIAIPAccounts.UseVisualStyleBackColor = True
        '
        'TPIAIPForms
        '
        Me.TPIAIPForms.Location = New System.Drawing.Point(4, 22)
        Me.TPIAIPForms.Name = "TPIAIPForms"
        Me.TPIAIPForms.Padding = New System.Windows.Forms.Padding(3)
        Me.TPIAIPForms.Size = New System.Drawing.Size(776, 471)
        Me.TPIAIPForms.TabIndex = 2
        Me.TPIAIPForms.Text = "IAIP Forms"
        Me.TPIAIPForms.UseVisualStyleBackColor = True
        '
        'TPSubPart60
        '
        Me.TPSubPart60.Location = New System.Drawing.Point(4, 22)
        Me.TPSubPart60.Name = "TPSubPart60"
        Me.TPSubPart60.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSubPart60.Size = New System.Drawing.Size(776, 471)
        Me.TPSubPart60.TabIndex = 3
        Me.TPSubPart60.Text = "Sub Part 60"
        Me.TPSubPart60.UseVisualStyleBackColor = True
        '
        'TPSubPart61
        '
        Me.TPSubPart61.Location = New System.Drawing.Point(4, 22)
        Me.TPSubPart61.Name = "TPSubPart61"
        Me.TPSubPart61.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSubPart61.Size = New System.Drawing.Size(776, 471)
        Me.TPSubPart61.TabIndex = 4
        Me.TPSubPart61.Text = "Sub Part 61"
        Me.TPSubPart61.UseVisualStyleBackColor = True
        '
        'TPPollutants
        '
        Me.TPPollutants.Location = New System.Drawing.Point(4, 22)
        Me.TPPollutants.Name = "TPPollutants"
        Me.TPPollutants.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPollutants.Size = New System.Drawing.Size(776, 471)
        Me.TPPollutants.TabIndex = 5
        Me.TPPollutants.Text = "Pollutants"
        Me.TPPollutants.UseVisualStyleBackColor = True
        '
        'TPSICCodes
        '
        Me.TPSICCodes.Location = New System.Drawing.Point(4, 22)
        Me.TPSICCodes.Name = "TPSICCodes"
        Me.TPSICCodes.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSICCodes.Size = New System.Drawing.Size(776, 471)
        Me.TPSICCodes.TabIndex = 6
        Me.TPSICCodes.Text = "SIC Codes"
        Me.TPSICCodes.UseVisualStyleBackColor = True
        '
        'TPSubPart63
        '
        Me.TPSubPart63.Location = New System.Drawing.Point(4, 22)
        Me.TPSubPart63.Name = "TPSubPart63"
        Me.TPSubPart63.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSubPart63.Size = New System.Drawing.Size(776, 471)
        Me.TPSubPart63.TabIndex = 7
        Me.TPSubPart63.Text = "Sub Part 63"
        Me.TPSubPart63.UseVisualStyleBackColor = True
        '
        'TPSubPartSIP
        '
        Me.TPSubPartSIP.Location = New System.Drawing.Point(4, 22)
        Me.TPSubPartSIP.Name = "TPSubPartSIP"
        Me.TPSubPartSIP.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSubPartSIP.Size = New System.Drawing.Size(776, 471)
        Me.TPSubPartSIP.TabIndex = 8
        Me.TPSubPartSIP.Text = "Sub Part SIP"
        Me.TPSubPartSIP.UseVisualStyleBackColor = True
        '
        'IAIPLookUpTables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 545)
        Me.Controls.Add(Me.TCLookUpTables)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPLookUpTables"
        Me.Text = "IAIP Look Up Tables"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TCLookUpTables.ResumeLayout(False)
        Me.TPGeneralTables.ResumeLayout(False)
        Me.TCGeneralTables.ResumeLayout(False)
        Me.TPAPBManagement.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAPBManagement.ResumeLayout(False)
        Me.pnlAPBManagement.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TPPermitting.ResumeLayout(False)
        Me.TCPermitting.ResumeLayout(False)
        Me.TPApplicationTypes.ResumeLayout(False)
        Me.TPApplicationTypes.PerformLayout()
        CType(Me.dgvApplicationType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPCompliance.ResumeLayout(False)
        Me.TCCompliance.ResumeLayout(False)
        Me.TPMonitoring.ResumeLayout(False)
        Me.TCMonitoring.ResumeLayout(False)
        Me.TPProgramSupport.ResumeLayout(False)
        Me.TCProgramSupport.ResumeLayout(False)
        Me.TPOther.ResumeLayout(False)
        Me.TCOther.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TCLookUpTables As System.Windows.Forms.TabControl
    Friend WithEvents TPGeneralTables As System.Windows.Forms.TabPage
    Friend WithEvents TPPermitting As System.Windows.Forms.TabPage
    Friend WithEvents TPCompliance As System.Windows.Forms.TabPage
    Friend WithEvents TPMonitoring As System.Windows.Forms.TabPage
    Friend WithEvents TPProgramSupport As System.Windows.Forms.TabPage
    Friend WithEvents TPOther As System.Windows.Forms.TabPage
    Friend WithEvents btnLoadApplicationTypes As System.Windows.Forms.Button
    Friend WithEvents btnAddNewAppType As System.Windows.Forms.Button
    Friend WithEvents txtApplicationDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvApplicationType As System.Windows.Forms.DataGridView
    Friend WithEvents txtApplicationID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClearAppTypes As System.Windows.Forms.Button
    Friend WithEvents chbActiveAppType As System.Windows.Forms.CheckBox
    Friend WithEvents TCPermitting As System.Windows.Forms.TabControl
    Friend WithEvents TPApplicationTypes As System.Windows.Forms.TabPage
    Friend WithEvents btnDeleteAppType As System.Windows.Forms.Button
    Friend WithEvents TPPermittingUnits As System.Windows.Forms.TabPage
    Friend WithEvents TPPermittingTypes As System.Windows.Forms.TabPage
    Friend WithEvents btnEditAppType As System.Windows.Forms.Button
    Friend WithEvents TCGeneralTables As System.Windows.Forms.TabControl
    Friend WithEvents TPAPBManagement As System.Windows.Forms.TabPage
    Friend WithEvents TPCountryInformation As System.Windows.Forms.TabPage
    Friend WithEvents TPDistrictInformation As System.Windows.Forms.TabPage
    Friend WithEvents TPDistrictOffices As System.Windows.Forms.TabPage
    Friend WithEvents TPDistricts As System.Windows.Forms.TabPage
    Friend WithEvents TPEPDBranches As System.Windows.Forms.TabPage
    Friend WithEvents TPEPDPrograms As System.Windows.Forms.TabPage
    Friend WithEvents TPEPDUnits As System.Windows.Forms.TabPage
    Friend WithEvents TCCompliance As System.Windows.Forms.TabControl
    Friend WithEvents TPComplianceActivities As System.Windows.Forms.TabPage
    Friend WithEvents TPComplianceStatus As System.Windows.Forms.TabPage
    Friend WithEvents TPComplianceUnits As System.Windows.Forms.TabPage
    Friend WithEvents TPSSCPNotifications As System.Windows.Forms.TabPage
    Friend WithEvents TPHPVViolations As System.Windows.Forms.TabPage
    Friend WithEvents TCMonitoring As System.Windows.Forms.TabControl
    Friend WithEvents TPISMPComplianceStatus As System.Windows.Forms.TabPage
    Friend WithEvents TPISMPMethods As System.Windows.Forms.TabPage
    Friend WithEvents TPMonitoringUnits As System.Windows.Forms.TabPage
    Friend WithEvents TPTestingFirms As System.Windows.Forms.TabPage
    Friend WithEvents TPUnits As System.Windows.Forms.TabPage
    Friend WithEvents TCProgramSupport As System.Windows.Forms.TabControl
    Friend WithEvents TPPASPInventoryType As System.Windows.Forms.TabPage
    Friend WithEvents TPPASPTransactionType As System.Windows.Forms.TabPage
    Friend WithEvents TCOther As System.Windows.Forms.TabControl
    Friend WithEvents TPEmployeeStatus As System.Windows.Forms.TabPage
    Friend WithEvents TPIAIPAccounts As System.Windows.Forms.TabPage
    Friend WithEvents TPIAIPForms As System.Windows.Forms.TabPage
    Friend WithEvents TPSubPart60 As System.Windows.Forms.TabPage
    Friend WithEvents TPSubPart61 As System.Windows.Forms.TabPage
    Friend WithEvents TPPollutants As System.Windows.Forms.TabPage
    Friend WithEvents TPSICCodes As System.Windows.Forms.TabPage
    Friend WithEvents TPSubPart63 As System.Windows.Forms.TabPage
    Friend WithEvents TPSubPartSIP As System.Windows.Forms.TabPage
    Friend WithEvents btnLoadAPBManagement As System.Windows.Forms.Button
    Friend WithEvents pnlAPBManagement As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboManagementType As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearManagement As System.Windows.Forms.Button
    Friend WithEvents txtAPBManagemetnID As System.Windows.Forms.TextBox
    Friend WithEvents chbAPBMangementVacant As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAPBManagementName As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveAPBManagement As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dgvLookUpManagement As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnViewAllPastTypes As System.Windows.Forms.Button
End Class
