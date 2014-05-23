<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUDeveloperTools
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DMUDeveloperTools))
        Me.bgwTransfer = New System.ComponentModel.BackgroundWorker
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TCDMUTools = New System.Windows.Forms.TabControl
        Me.TPAFSFileGenerator = New System.Windows.Forms.TabPage
        Me.txtAFSBatchFile = New System.Windows.Forms.TextBox
        Me.PanelBatchOrder = New System.Windows.Forms.Panel
        Me.pnlBasicRefresh = New System.Windows.Forms.Panel
        Me.btnForceBasicRefresh = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.pnlSubParts = New System.Windows.Forms.Panel
        Me.btnUpdateAllSubParts = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.pnlAIRSSpecific = New System.Windows.Forms.Panel
        Me.btnAIRSSpecificRefresh = New System.Windows.Forms.Button
        Me.mtbAFSAirsNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.pnlStandardFile = New System.Windows.Forms.Panel
        Me.btnGenerateBatchFile = New System.Windows.Forms.Button
        Me.Label55 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label59 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.rdbBasicData = New System.Windows.Forms.RadioButton
        Me.rdbUpdateAllSubparts = New System.Windows.Forms.RadioButton
        Me.rdbAIRSSpecific = New System.Windows.Forms.RadioButton
        Me.rdbGenerateStandardFile = New System.Windows.Forms.RadioButton
        Me.btnClearAFSFileGenerator = New System.Windows.Forms.Button
        Me.TPErrorLog = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnExporttoExcel = New System.Windows.Forms.Button
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.rdbNoLimit = New System.Windows.Forms.RadioButton
        Me.rdbLast60days = New System.Windows.Forms.RadioButton
        Me.rdbLast30Days = New System.Windows.Forms.RadioButton
        Me.Label61 = New System.Windows.Forms.Label
        Me.txtErrorCount = New System.Windows.Forms.TextBox
        Me.txtErrorNumber = New System.Windows.Forms.TextBox
        Me.btnFilterErrors = New System.Windows.Forms.Button
        Me.btnSaveError = New System.Windows.Forms.Button
        Me.rdbViewResolvedErrors = New System.Windows.Forms.RadioButton
        Me.Label62 = New System.Windows.Forms.Label
        Me.rdbViewUnresolvedErrors = New System.Windows.Forms.RadioButton
        Me.Label64 = New System.Windows.Forms.Label
        Me.rdbViewAllErrors = New System.Windows.Forms.RadioButton
        Me.Label65 = New System.Windows.Forms.Label
        Me.txtErrorSolution = New System.Windows.Forms.TextBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.txtErrorMessage = New System.Windows.Forms.TextBox
        Me.Label67 = New System.Windows.Forms.Label
        Me.txtErrorDate = New System.Windows.Forms.TextBox
        Me.txtErrorUser = New System.Windows.Forms.TextBox
        Me.txtErrorLocation = New System.Windows.Forms.TextBox
        Me.dgvErrorList = New System.Windows.Forms.DataGridView
        Me.TPWebErrorLog = New System.Windows.Forms.TabPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label95 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.txtWebErrorNumber = New System.Windows.Forms.TextBox
        Me.txtIPAddress = New System.Windows.Forms.TextBox
        Me.btnSaveWebErrorSolution = New System.Windows.Forms.Button
        Me.txtWebErrorCount = New System.Windows.Forms.TextBox
        Me.Label91 = New System.Windows.Forms.Label
        Me.btnFilterWebErrors = New System.Windows.Forms.Button
        Me.Label90 = New System.Windows.Forms.Label
        Me.rdbResolvedWebErrors = New System.Windows.Forms.RadioButton
        Me.Label88 = New System.Windows.Forms.Label
        Me.rdbUnresolvedWebErrors = New System.Windows.Forms.RadioButton
        Me.Label71 = New System.Windows.Forms.Label
        Me.rdbAllWebErrors = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtWebErrorSolution = New System.Windows.Forms.TextBox
        Me.txtWebErrorUser = New System.Windows.Forms.TextBox
        Me.txtWebErrorMessage = New System.Windows.Forms.TextBox
        Me.txtWebErrorLocation = New System.Windows.Forms.TextBox
        Me.txtWebErrorDate = New System.Windows.Forms.TextBox
        Me.dgrWebErrorList = New System.Windows.Forms.DataGrid
        Me.TPAddNewFacility = New System.Windows.Forms.TabPage
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.btnPreLoadNewFacility = New System.Windows.Forms.Button
        Me.btnDeleteAIRSNumber = New System.Windows.Forms.Button
        Me.txtDeleteAIRSNumber = New System.Windows.Forms.TextBox
        Me.btnClearAddNewFacility = New System.Windows.Forms.Button
        Me.GBContactInformation = New System.Windows.Forms.GroupBox
        Me.mtbContactNumberExtension = New System.Windows.Forms.MaskedTextBox
        Me.txtContactPedigree = New System.Windows.Forms.TextBox
        Me.txtContactSocialTitle = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtContactLastName = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtContactFirstName = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.mtbContactPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtContactTitle = New System.Windows.Forms.TextBox
        Me.GBAirProgramCodes = New System.Windows.Forms.GroupBox
        Me.chbCDS_14 = New System.Windows.Forms.CheckBox
        Me.chbCDS_7 = New System.Windows.Forms.CheckBox
        Me.chbCDS_4 = New System.Windows.Forms.CheckBox
        Me.chbCDS_13 = New System.Windows.Forms.CheckBox
        Me.chbCDS_3 = New System.Windows.Forms.CheckBox
        Me.chbCDS_12 = New System.Windows.Forms.CheckBox
        Me.chbCDS_9 = New System.Windows.Forms.CheckBox
        Me.chbCDS_10 = New System.Windows.Forms.CheckBox
        Me.chbCDS_2 = New System.Windows.Forms.CheckBox
        Me.chbCDS_6 = New System.Windows.Forms.CheckBox
        Me.chbCDS_1 = New System.Windows.Forms.CheckBox
        Me.chbCDS_5 = New System.Windows.Forms.CheckBox
        Me.chbCDS_11 = New System.Windows.Forms.CheckBox
        Me.chbCDS_8 = New System.Windows.Forms.CheckBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.GBHeaderData = New System.Windows.Forms.GroupBox
        Me.mtbCDSSICCode = New System.Windows.Forms.MaskedTextBox
        Me.txtCDSRegionCode = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.cboCDSOperationalStatus = New System.Windows.Forms.ComboBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.cboCDSClassCode = New System.Windows.Forms.ComboBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.txtFacilityDescription = New System.Windows.Forms.TextBox
        Me.GBMailingLocation = New System.Windows.Forms.GroupBox
        Me.mtbMailingZipCode = New System.Windows.Forms.MaskedTextBox
        Me.txtMailingState = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtMailingCity = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtMailingAddress = New System.Windows.Forms.TextBox
        Me.GBFacilityInformation = New System.Windows.Forms.GroupBox
        Me.mtbFacilityLongitude = New System.Windows.Forms.MaskedTextBox
        Me.mtbFacilityLatitude = New System.Windows.Forms.MaskedTextBox
        Me.mtbCDSZipCode = New System.Windows.Forms.MaskedTextBox
        Me.Label103 = New System.Windows.Forms.Label
        Me.Label102 = New System.Windows.Forms.Label
        Me.txtCDSStreetAddress = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtCDSFacilityName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCDSCity = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCDSState = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.llbContactInformation = New System.Windows.Forms.LinkLabel
        Me.llbAirProgramCodes = New System.Windows.Forms.LinkLabel
        Me.llbHeaderData = New System.Windows.Forms.LinkLabel
        Me.llbMailingLocation = New System.Windows.Forms.LinkLabel
        Me.llbFacilityInformation = New System.Windows.Forms.LinkLabel
        Me.btnNewFacility = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCDSAIRSNumber = New System.Windows.Forms.TextBox
        Me.TCDMUTools.SuspendLayout()
        Me.TPAFSFileGenerator.SuspendLayout()
        Me.PanelBatchOrder.SuspendLayout()
        Me.pnlBasicRefresh.SuspendLayout()
        Me.pnlSubParts.SuspendLayout()
        Me.pnlAIRSSpecific.SuspendLayout()
        Me.pnlStandardFile.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.TPErrorLog.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.dgvErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPWebErrorLog.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgrWebErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPAddNewFacility.SuspendLayout()
        Me.GBContactInformation.SuspendLayout()
        Me.GBAirProgramCodes.SuspendLayout()
        Me.GBHeaderData.SuspendLayout()
        Me.GBMailingLocation.SuspendLayout()
        Me.GBFacilityInformation.SuspendLayout()
        Me.SuspendLayout()
        '
        'Image_List_All
        '
        Me.Image_List_All.ImageStream = CType(resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        Me.Image_List_All.Images.SetKeyName(0, "")
        Me.Image_List_All.Images.SetKeyName(1, "")
        Me.Image_List_All.Images.SetKeyName(2, "")
        Me.Image_List_All.Images.SetKeyName(3, "")
        Me.Image_List_All.Images.SetKeyName(4, "")
        Me.Image_List_All.Images.SetKeyName(5, "")
        Me.Image_List_All.Images.SetKeyName(6, "")
        Me.Image_List_All.Images.SetKeyName(7, "")
        Me.Image_List_All.Images.SetKeyName(8, "")
        Me.Image_List_All.Images.SetKeyName(9, "")
        Me.Image_List_All.Images.SetKeyName(10, "")
        Me.Image_List_All.Images.SetKeyName(11, "")
        Me.Image_List_All.Images.SetKeyName(12, "")
        Me.Image_List_All.Images.SetKeyName(13, "")
        Me.Image_List_All.Images.SetKeyName(14, "")
        Me.Image_List_All.Images.SetKeyName(15, "")
        Me.Image_List_All.Images.SetKeyName(16, "")
        Me.Image_List_All.Images.SetKeyName(17, "")
        Me.Image_List_All.Images.SetKeyName(18, "")
        Me.Image_List_All.Images.SetKeyName(19, "")
        Me.Image_List_All.Images.SetKeyName(20, "")
        Me.Image_List_All.Images.SetKeyName(21, "")
        Me.Image_List_All.Images.SetKeyName(22, "")
        Me.Image_List_All.Images.SetKeyName(23, "")
        Me.Image_List_All.Images.SetKeyName(24, "")
        Me.Image_List_All.Images.SetKeyName(25, "")
        Me.Image_List_All.Images.SetKeyName(26, "")
        Me.Image_List_All.Images.SetKeyName(27, "")
        Me.Image_List_All.Images.SetKeyName(28, "")
        Me.Image_List_All.Images.SetKeyName(29, "")
        Me.Image_List_All.Images.SetKeyName(30, "")
        Me.Image_List_All.Images.SetKeyName(31, "")
        Me.Image_List_All.Images.SetKeyName(32, "")
        Me.Image_List_All.Images.SetKeyName(33, "")
        Me.Image_List_All.Images.SetKeyName(34, "")
        Me.Image_List_All.Images.SetKeyName(35, "")
        Me.Image_List_All.Images.SetKeyName(36, "")
        Me.Image_List_All.Images.SetKeyName(37, "")
        Me.Image_List_All.Images.SetKeyName(38, "")
        Me.Image_List_All.Images.SetKeyName(39, "")
        Me.Image_List_All.Images.SetKeyName(40, "")
        Me.Image_List_All.Images.SetKeyName(41, "")
        Me.Image_List_All.Images.SetKeyName(42, "")
        Me.Image_List_All.Images.SetKeyName(43, "")
        Me.Image_List_All.Images.SetKeyName(44, "")
        Me.Image_List_All.Images.SetKeyName(45, "")
        Me.Image_List_All.Images.SetKeyName(46, "")
        Me.Image_List_All.Images.SetKeyName(47, "")
        Me.Image_List_All.Images.SetKeyName(48, "")
        Me.Image_List_All.Images.SetKeyName(49, "")
        Me.Image_List_All.Images.SetKeyName(50, "")
        Me.Image_List_All.Images.SetKeyName(51, "")
        Me.Image_List_All.Images.SetKeyName(52, "")
        Me.Image_List_All.Images.SetKeyName(53, "")
        Me.Image_List_All.Images.SetKeyName(54, "")
        Me.Image_List_All.Images.SetKeyName(55, "")
        Me.Image_List_All.Images.SetKeyName(56, "")
        Me.Image_List_All.Images.SetKeyName(57, "")
        Me.Image_List_All.Images.SetKeyName(58, "")
        Me.Image_List_All.Images.SetKeyName(59, "")
        Me.Image_List_All.Images.SetKeyName(60, "")
        Me.Image_List_All.Images.SetKeyName(61, "")
        Me.Image_List_All.Images.SetKeyName(62, "")
        Me.Image_List_All.Images.SetKeyName(63, "")
        Me.Image_List_All.Images.SetKeyName(64, "")
        Me.Image_List_All.Images.SetKeyName(65, "")
        Me.Image_List_All.Images.SetKeyName(66, "")
        Me.Image_List_All.Images.SetKeyName(67, "")
        Me.Image_List_All.Images.SetKeyName(68, "")
        Me.Image_List_All.Images.SetKeyName(69, "")
        Me.Image_List_All.Images.SetKeyName(70, "")
        Me.Image_List_All.Images.SetKeyName(71, "")
        Me.Image_List_All.Images.SetKeyName(72, "")
        Me.Image_List_All.Images.SetKeyName(73, "")
        Me.Image_List_All.Images.SetKeyName(74, "")
        Me.Image_List_All.Images.SetKeyName(75, "")
        Me.Image_List_All.Images.SetKeyName(76, "")
        Me.Image_List_All.Images.SetKeyName(77, "")
        Me.Image_List_All.Images.SetKeyName(78, "")
        Me.Image_List_All.Images.SetKeyName(79, "")
        Me.Image_List_All.Images.SetKeyName(80, "")
        Me.Image_List_All.Images.SetKeyName(81, "")
        Me.Image_List_All.Images.SetKeyName(82, "")
        Me.Image_List_All.Images.SetKeyName(83, "")
        Me.Image_List_All.Images.SetKeyName(84, "")
        '
        'TCDMUTools
        '
        Me.TCDMUTools.Controls.Add(Me.TPAFSFileGenerator)
        Me.TCDMUTools.Controls.Add(Me.TPErrorLog)
        Me.TCDMUTools.Controls.Add(Me.TPWebErrorLog)
        Me.TCDMUTools.Controls.Add(Me.TPAddNewFacility)
        Me.TCDMUTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDMUTools.Location = New System.Drawing.Point(0, 0)
        Me.TCDMUTools.Name = "TCDMUTools"
        Me.TCDMUTools.SelectedIndex = 0
        Me.TCDMUTools.Size = New System.Drawing.Size(792, 687)
        Me.TCDMUTools.TabIndex = 256
        '
        'TPAFSFileGenerator
        '
        Me.TPAFSFileGenerator.Controls.Add(Me.txtAFSBatchFile)
        Me.TPAFSFileGenerator.Controls.Add(Me.PanelBatchOrder)
        Me.TPAFSFileGenerator.Location = New System.Drawing.Point(4, 22)
        Me.TPAFSFileGenerator.Name = "TPAFSFileGenerator"
        Me.TPAFSFileGenerator.Size = New System.Drawing.Size(784, 661)
        Me.TPAFSFileGenerator.TabIndex = 1
        Me.TPAFSFileGenerator.Text = "AFS File Generator"
        Me.TPAFSFileGenerator.UseVisualStyleBackColor = True
        '
        'txtAFSBatchFile
        '
        Me.txtAFSBatchFile.AcceptsReturn = True
        Me.txtAFSBatchFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAFSBatchFile.Location = New System.Drawing.Point(0, 257)
        Me.txtAFSBatchFile.Multiline = True
        Me.txtAFSBatchFile.Name = "txtAFSBatchFile"
        Me.txtAFSBatchFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAFSBatchFile.Size = New System.Drawing.Size(784, 404)
        Me.txtAFSBatchFile.TabIndex = 11
        '
        'PanelBatchOrder
        '
        Me.PanelBatchOrder.Controls.Add(Me.pnlBasicRefresh)
        Me.PanelBatchOrder.Controls.Add(Me.pnlSubParts)
        Me.PanelBatchOrder.Controls.Add(Me.pnlAIRSSpecific)
        Me.PanelBatchOrder.Controls.Add(Me.pnlStandardFile)
        Me.PanelBatchOrder.Controls.Add(Me.Panel8)
        Me.PanelBatchOrder.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelBatchOrder.Location = New System.Drawing.Point(0, 0)
        Me.PanelBatchOrder.Name = "PanelBatchOrder"
        Me.PanelBatchOrder.Size = New System.Drawing.Size(784, 257)
        Me.PanelBatchOrder.TabIndex = 10
        '
        'pnlBasicRefresh
        '
        Me.pnlBasicRefresh.Controls.Add(Me.btnForceBasicRefresh)
        Me.pnlBasicRefresh.Controls.Add(Me.Label11)
        Me.pnlBasicRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBasicRefresh.Enabled = False
        Me.pnlBasicRefresh.Location = New System.Drawing.Point(296, 160)
        Me.pnlBasicRefresh.Name = "pnlBasicRefresh"
        Me.pnlBasicRefresh.Size = New System.Drawing.Size(488, 97)
        Me.pnlBasicRefresh.TabIndex = 21
        '
        'btnForceBasicRefresh
        '
        Me.btnForceBasicRefresh.AutoSize = True
        Me.btnForceBasicRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnForceBasicRefresh.Location = New System.Drawing.Point(3, 8)
        Me.btnForceBasicRefresh.Name = "btnForceBasicRefresh"
        Me.btnForceBasicRefresh.Size = New System.Drawing.Size(113, 23)
        Me.btnForceBasicRefresh.TabIndex = 10
        Me.btnForceBasicRefresh.Text = "Force Basic Refresh"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(175, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "This will force an add of all facilities."
        '
        'pnlSubParts
        '
        Me.pnlSubParts.Controls.Add(Me.btnUpdateAllSubParts)
        Me.pnlSubParts.Controls.Add(Me.Label6)
        Me.pnlSubParts.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSubParts.Enabled = False
        Me.pnlSubParts.Location = New System.Drawing.Point(296, 98)
        Me.pnlSubParts.Name = "pnlSubParts"
        Me.pnlSubParts.Size = New System.Drawing.Size(488, 62)
        Me.pnlSubParts.TabIndex = 20
        '
        'btnUpdateAllSubParts
        '
        Me.btnUpdateAllSubParts.AutoSize = True
        Me.btnUpdateAllSubParts.Location = New System.Drawing.Point(3, 5)
        Me.btnUpdateAllSubParts.Name = "btnUpdateAllSubParts"
        Me.btnUpdateAllSubParts.Size = New System.Drawing.Size(112, 23)
        Me.btnUpdateAllSubParts.TabIndex = 11
        Me.btnUpdateAllSubParts.Text = "Update All SubParts"
        Me.btnUpdateAllSubParts.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(252, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "This will add and update all SubParts for all Facilities"
        '
        'pnlAIRSSpecific
        '
        Me.pnlAIRSSpecific.Controls.Add(Me.btnAIRSSpecificRefresh)
        Me.pnlAIRSSpecific.Controls.Add(Me.mtbAFSAirsNumber)
        Me.pnlAIRSSpecific.Controls.Add(Me.Label12)
        Me.pnlAIRSSpecific.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAIRSSpecific.Enabled = False
        Me.pnlAIRSSpecific.Location = New System.Drawing.Point(296, 40)
        Me.pnlAIRSSpecific.Name = "pnlAIRSSpecific"
        Me.pnlAIRSSpecific.Size = New System.Drawing.Size(488, 58)
        Me.pnlAIRSSpecific.TabIndex = 19
        '
        'btnAIRSSpecificRefresh
        '
        Me.btnAIRSSpecificRefresh.AutoSize = True
        Me.btnAIRSSpecificRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAIRSSpecificRefresh.Enabled = False
        Me.btnAIRSSpecificRefresh.Location = New System.Drawing.Point(83, 7)
        Me.btnAIRSSpecificRefresh.Name = "btnAIRSSpecificRefresh"
        Me.btnAIRSSpecificRefresh.Size = New System.Drawing.Size(123, 23)
        Me.btnAIRSSpecificRefresh.TabIndex = 15
        Me.btnAIRSSpecificRefresh.Text = "AIRS Specific Refresh"
        '
        'mtbAFSAirsNumber
        '
        Me.mtbAFSAirsNumber.Location = New System.Drawing.Point(7, 9)
        Me.mtbAFSAirsNumber.Mask = "000-00000"
        Me.mtbAFSAirsNumber.Name = "mtbAFSAirsNumber"
        Me.mtbAFSAirsNumber.Size = New System.Drawing.Size(70, 20)
        Me.mtbAFSAirsNumber.TabIndex = 14
        Me.mtbAFSAirsNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 33)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(254, 13)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "This will add and update all of a Facilities Information"
        '
        'pnlStandardFile
        '
        Me.pnlStandardFile.Controls.Add(Me.btnGenerateBatchFile)
        Me.pnlStandardFile.Controls.Add(Me.Label55)
        Me.pnlStandardFile.Controls.Add(Me.Label3)
        Me.pnlStandardFile.Controls.Add(Me.Label41)
        Me.pnlStandardFile.Controls.Add(Me.Label2)
        Me.pnlStandardFile.Controls.Add(Me.Label59)
        Me.pnlStandardFile.Controls.Add(Me.Label58)
        Me.pnlStandardFile.Controls.Add(Me.Label38)
        Me.pnlStandardFile.Controls.Add(Me.Label29)
        Me.pnlStandardFile.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlStandardFile.Enabled = False
        Me.pnlStandardFile.Location = New System.Drawing.Point(0, 40)
        Me.pnlStandardFile.Name = "pnlStandardFile"
        Me.pnlStandardFile.Size = New System.Drawing.Size(296, 217)
        Me.pnlStandardFile.TabIndex = 17
        '
        'btnGenerateBatchFile
        '
        Me.btnGenerateBatchFile.Location = New System.Drawing.Point(11, 7)
        Me.btnGenerateBatchFile.Name = "btnGenerateBatchFile"
        Me.btnGenerateBatchFile.Size = New System.Drawing.Size(88, 23)
        Me.btnGenerateBatchFile.TabIndex = 0
        Me.btnGenerateBatchFile.Text = "Generate File"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(139, 117)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(112, 13)
        Me.Label55.TabIndex = 5
        Me.Label55.Text = "4) Compliance Actions"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(123, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Batch File Hierarchy"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(139, 93)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(103, 13)
        Me.Label41.TabIndex = 4
        Me.Label41.Text = "3) Permitting Actions"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(139, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "1) New Facilities"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(139, 189)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(109, 13)
        Me.Label59.TabIndex = 7
        Me.Label59.Text = "7) ISMP Test Reports"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(139, 165)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(117, 13)
        Me.Label58.TabIndex = 6
        Me.Label58.Text = "6) Enforcement Actions"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(139, 69)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(137, 13)
        Me.Label38.TabIndex = 3
        Me.Label38.Text = "2) Changes to Header Data"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(139, 141)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(151, 13)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "5) Full Compliance Evaluations"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.rdbBasicData)
        Me.Panel8.Controls.Add(Me.rdbUpdateAllSubparts)
        Me.Panel8.Controls.Add(Me.rdbAIRSSpecific)
        Me.Panel8.Controls.Add(Me.rdbGenerateStandardFile)
        Me.Panel8.Controls.Add(Me.btnClearAFSFileGenerator)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(784, 40)
        Me.Panel8.TabIndex = 18
        '
        'rdbBasicData
        '
        Me.rdbBasicData.AutoSize = True
        Me.rdbBasicData.Location = New System.Drawing.Point(488, 11)
        Me.rdbBasicData.Name = "rdbBasicData"
        Me.rdbBasicData.Size = New System.Drawing.Size(169, 17)
        Me.rdbBasicData.TabIndex = 13
        Me.rdbBasicData.TabStop = True
        Me.rdbBasicData.Text = "Force Refresh of all basic data"
        Me.rdbBasicData.UseVisualStyleBackColor = True
        '
        'rdbUpdateAllSubparts
        '
        Me.rdbUpdateAllSubparts.AutoSize = True
        Me.rdbUpdateAllSubparts.Location = New System.Drawing.Point(363, 11)
        Me.rdbUpdateAllSubparts.Name = "rdbUpdateAllSubparts"
        Me.rdbUpdateAllSubparts.Size = New System.Drawing.Size(119, 17)
        Me.rdbUpdateAllSubparts.TabIndex = 12
        Me.rdbUpdateAllSubparts.TabStop = True
        Me.rdbUpdateAllSubparts.Text = "Update All Subparts"
        Me.rdbUpdateAllSubparts.UseVisualStyleBackColor = True
        '
        'rdbAIRSSpecific
        '
        Me.rdbAIRSSpecific.AutoSize = True
        Me.rdbAIRSSpecific.Location = New System.Drawing.Point(266, 11)
        Me.rdbAIRSSpecific.Name = "rdbAIRSSpecific"
        Me.rdbAIRSSpecific.Size = New System.Drawing.Size(91, 17)
        Me.rdbAIRSSpecific.TabIndex = 11
        Me.rdbAIRSSpecific.TabStop = True
        Me.rdbAIRSSpecific.Text = "AIRS Specific"
        Me.rdbAIRSSpecific.UseVisualStyleBackColor = True
        '
        'rdbGenerateStandardFile
        '
        Me.rdbGenerateStandardFile.AutoSize = True
        Me.rdbGenerateStandardFile.Location = New System.Drawing.Point(126, 11)
        Me.rdbGenerateStandardFile.Name = "rdbGenerateStandardFile"
        Me.rdbGenerateStandardFile.Size = New System.Drawing.Size(134, 17)
        Me.rdbGenerateStandardFile.TabIndex = 10
        Me.rdbGenerateStandardFile.TabStop = True
        Me.rdbGenerateStandardFile.Text = "Generate Standard File"
        Me.rdbGenerateStandardFile.UseVisualStyleBackColor = True
        '
        'btnClearAFSFileGenerator
        '
        Me.btnClearAFSFileGenerator.Location = New System.Drawing.Point(11, 8)
        Me.btnClearAFSFileGenerator.Name = "btnClearAFSFileGenerator"
        Me.btnClearAFSFileGenerator.Size = New System.Drawing.Size(87, 23)
        Me.btnClearAFSFileGenerator.TabIndex = 9
        Me.btnClearAFSFileGenerator.Text = "Clear Form"
        '
        'TPErrorLog
        '
        Me.TPErrorLog.Controls.Add(Me.GroupBox3)
        Me.TPErrorLog.Controls.Add(Me.dgvErrorList)
        Me.TPErrorLog.Location = New System.Drawing.Point(4, 22)
        Me.TPErrorLog.Name = "TPErrorLog"
        Me.TPErrorLog.Size = New System.Drawing.Size(784, 661)
        Me.TPErrorLog.TabIndex = 7
        Me.TPErrorLog.Text = "IAIP Error Log"
        Me.TPErrorLog.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 242)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(784, 419)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "IAIP Error Log"
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.Controls.Add(Me.btnExporttoExcel)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Label61)
        Me.Panel4.Controls.Add(Me.txtErrorCount)
        Me.Panel4.Controls.Add(Me.txtErrorNumber)
        Me.Panel4.Controls.Add(Me.btnFilterErrors)
        Me.Panel4.Controls.Add(Me.btnSaveError)
        Me.Panel4.Controls.Add(Me.rdbViewResolvedErrors)
        Me.Panel4.Controls.Add(Me.Label62)
        Me.Panel4.Controls.Add(Me.rdbViewUnresolvedErrors)
        Me.Panel4.Controls.Add(Me.Label64)
        Me.Panel4.Controls.Add(Me.rdbViewAllErrors)
        Me.Panel4.Controls.Add(Me.Label65)
        Me.Panel4.Controls.Add(Me.txtErrorSolution)
        Me.Panel4.Controls.Add(Me.Label66)
        Me.Panel4.Controls.Add(Me.txtErrorMessage)
        Me.Panel4.Controls.Add(Me.Label67)
        Me.Panel4.Controls.Add(Me.txtErrorDate)
        Me.Panel4.Controls.Add(Me.txtErrorUser)
        Me.Panel4.Controls.Add(Me.txtErrorLocation)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 16)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(778, 400)
        Me.Panel4.TabIndex = 18
        '
        'btnExporttoExcel
        '
        Me.btnExporttoExcel.Location = New System.Drawing.Point(659, 6)
        Me.btnExporttoExcel.Name = "btnExporttoExcel"
        Me.btnExporttoExcel.Size = New System.Drawing.Size(99, 23)
        Me.btnExporttoExcel.TabIndex = 20
        Me.btnExporttoExcel.Text = "Export to Excel"
        Me.btnExporttoExcel.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdbNoLimit)
        Me.Panel6.Controls.Add(Me.rdbLast60days)
        Me.Panel6.Controls.Add(Me.rdbLast30Days)
        Me.Panel6.Location = New System.Drawing.Point(243, 239)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(104, 68)
        Me.Panel6.TabIndex = 19
        '
        'rdbNoLimit
        '
        Me.rdbNoLimit.AutoSize = True
        Me.rdbNoLimit.Location = New System.Drawing.Point(3, 44)
        Me.rdbNoLimit.Name = "rdbNoLimit"
        Me.rdbNoLimit.Size = New System.Drawing.Size(59, 17)
        Me.rdbNoLimit.TabIndex = 20
        Me.rdbNoLimit.TabStop = True
        Me.rdbNoLimit.Text = "No limit"
        Me.rdbNoLimit.UseVisualStyleBackColor = True
        '
        'rdbLast60days
        '
        Me.rdbLast60days.AutoSize = True
        Me.rdbLast60days.Location = New System.Drawing.Point(3, 25)
        Me.rdbLast60days.Name = "rdbLast60days"
        Me.rdbLast60days.Size = New System.Drawing.Size(85, 17)
        Me.rdbLast60days.TabIndex = 19
        Me.rdbLast60days.TabStop = True
        Me.rdbLast60days.Text = "Last 60 days"
        Me.rdbLast60days.UseVisualStyleBackColor = True
        '
        'rdbLast30Days
        '
        Me.rdbLast30Days.AutoSize = True
        Me.rdbLast30Days.Checked = True
        Me.rdbLast30Days.Location = New System.Drawing.Point(3, 4)
        Me.rdbLast30Days.Name = "rdbLast30Days"
        Me.rdbLast30Days.Size = New System.Drawing.Size(85, 17)
        Me.rdbLast30Days.TabIndex = 18
        Me.rdbLast30Days.TabStop = True
        Me.rdbLast30Days.Text = "Last 30 days"
        Me.rdbLast30Days.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(11, 10)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(69, 13)
        Me.Label61.TabIndex = 0
        Me.Label61.Text = "Error Number"
        '
        'txtErrorCount
        '
        Me.txtErrorCount.Location = New System.Drawing.Point(619, 7)
        Me.txtErrorCount.Name = "txtErrorCount"
        Me.txtErrorCount.ReadOnly = True
        Me.txtErrorCount.Size = New System.Drawing.Size(34, 20)
        Me.txtErrorCount.TabIndex = 17
        Me.txtErrorCount.Text = "0"
        '
        'txtErrorNumber
        '
        Me.txtErrorNumber.Location = New System.Drawing.Point(91, 8)
        Me.txtErrorNumber.Name = "txtErrorNumber"
        Me.txtErrorNumber.ReadOnly = True
        Me.txtErrorNumber.Size = New System.Drawing.Size(60, 20)
        Me.txtErrorNumber.TabIndex = 1
        '
        'btnFilterErrors
        '
        Me.btnFilterErrors.Location = New System.Drawing.Point(157, 239)
        Me.btnFilterErrors.Name = "btnFilterErrors"
        Me.btnFilterErrors.Size = New System.Drawing.Size(63, 20)
        Me.btnFilterErrors.TabIndex = 16
        Me.btnFilterErrors.Text = "Filter"
        '
        'btnSaveError
        '
        Me.btnSaveError.Location = New System.Drawing.Point(11, 190)
        Me.btnSaveError.Name = "btnSaveError"
        Me.btnSaveError.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveError.TabIndex = 2
        Me.btnSaveError.Text = "Save"
        '
        'rdbViewResolvedErrors
        '
        Me.rdbViewResolvedErrors.AutoSize = True
        Me.rdbViewResolvedErrors.Location = New System.Drawing.Point(17, 280)
        Me.rdbViewResolvedErrors.Name = "rdbViewResolvedErrors"
        Me.rdbViewResolvedErrors.Size = New System.Drawing.Size(121, 17)
        Me.rdbViewResolvedErrors.TabIndex = 15
        Me.rdbViewResolvedErrors.Text = "View Resolved Error"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(171, 10)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(29, 13)
        Me.Label62.TabIndex = 3
        Me.Label62.Text = "User"
        '
        'rdbViewUnresolvedErrors
        '
        Me.rdbViewUnresolvedErrors.AutoSize = True
        Me.rdbViewUnresolvedErrors.Checked = True
        Me.rdbViewUnresolvedErrors.Location = New System.Drawing.Point(17, 259)
        Me.rdbViewUnresolvedErrors.Name = "rdbViewUnresolvedErrors"
        Me.rdbViewUnresolvedErrors.Size = New System.Drawing.Size(135, 17)
        Me.rdbViewUnresolvedErrors.TabIndex = 14
        Me.rdbViewUnresolvedErrors.TabStop = True
        Me.rdbViewUnresolvedErrors.Text = "View Unresolved Errors"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(11, 32)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(76, 13)
        Me.Label64.TabIndex = 4
        Me.Label64.Text = "Error Location "
        '
        'rdbViewAllErrors
        '
        Me.rdbViewAllErrors.AutoSize = True
        Me.rdbViewAllErrors.Location = New System.Drawing.Point(17, 239)
        Me.rdbViewAllErrors.Name = "rdbViewAllErrors"
        Me.rdbViewAllErrors.Size = New System.Drawing.Size(92, 17)
        Me.rdbViewAllErrors.TabIndex = 13
        Me.rdbViewAllErrors.Text = "View All Errors"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(11, 58)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(75, 13)
        Me.Label65.TabIndex = 5
        Me.Label65.Text = "Error Message"
        '
        'txtErrorSolution
        '
        Me.txtErrorSolution.AcceptsReturn = True
        Me.txtErrorSolution.Location = New System.Drawing.Point(91, 169)
        Me.txtErrorSolution.Multiline = True
        Me.txtErrorSolution.Name = "txtErrorSolution"
        Me.txtErrorSolution.Size = New System.Drawing.Size(546, 56)
        Me.txtErrorSolution.TabIndex = 12
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(11, 169)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(73, 13)
        Me.Label66.TabIndex = 6
        Me.Label66.Text = "Error Solution "
        '
        'txtErrorMessage
        '
        Me.txtErrorMessage.AcceptsReturn = True
        Me.txtErrorMessage.Location = New System.Drawing.Point(91, 58)
        Me.txtErrorMessage.Multiline = True
        Me.txtErrorMessage.Name = "txtErrorMessage"
        Me.txtErrorMessage.ReadOnly = True
        Me.txtErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtErrorMessage.Size = New System.Drawing.Size(553, 104)
        Me.txtErrorMessage.TabIndex = 11
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(384, 32)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(55, 13)
        Me.Label67.TabIndex = 7
        Me.Label67.Text = "Error Date"
        '
        'txtErrorDate
        '
        Me.txtErrorDate.Location = New System.Drawing.Point(471, 31)
        Me.txtErrorDate.Name = "txtErrorDate"
        Me.txtErrorDate.ReadOnly = True
        Me.txtErrorDate.Size = New System.Drawing.Size(106, 20)
        Me.txtErrorDate.TabIndex = 10
        '
        'txtErrorUser
        '
        Me.txtErrorUser.Location = New System.Drawing.Point(204, 8)
        Me.txtErrorUser.Name = "txtErrorUser"
        Me.txtErrorUser.ReadOnly = True
        Me.txtErrorUser.Size = New System.Drawing.Size(193, 20)
        Me.txtErrorUser.TabIndex = 8
        '
        'txtErrorLocation
        '
        Me.txtErrorLocation.Location = New System.Drawing.Point(91, 31)
        Me.txtErrorLocation.Name = "txtErrorLocation"
        Me.txtErrorLocation.ReadOnly = True
        Me.txtErrorLocation.Size = New System.Drawing.Size(266, 20)
        Me.txtErrorLocation.TabIndex = 9
        '
        'dgvErrorList
        '
        Me.dgvErrorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvErrorList.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvErrorList.Location = New System.Drawing.Point(0, 0)
        Me.dgvErrorList.Name = "dgvErrorList"
        Me.dgvErrorList.Size = New System.Drawing.Size(784, 242)
        Me.dgvErrorList.TabIndex = 21
        '
        'TPWebErrorLog
        '
        Me.TPWebErrorLog.Controls.Add(Me.GroupBox4)
        Me.TPWebErrorLog.Controls.Add(Me.dgrWebErrorList)
        Me.TPWebErrorLog.Location = New System.Drawing.Point(4, 22)
        Me.TPWebErrorLog.Name = "TPWebErrorLog"
        Me.TPWebErrorLog.Size = New System.Drawing.Size(784, 661)
        Me.TPWebErrorLog.TabIndex = 8
        Me.TPWebErrorLog.Text = "Web Site Error Log"
        Me.TPWebErrorLog.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Panel5)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 241)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(784, 420)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Web Error Log"
        '
        'Panel5
        '
        Me.Panel5.AutoScroll = True
        Me.Panel5.Controls.Add(Me.Label95)
        Me.Panel5.Controls.Add(Me.Label96)
        Me.Panel5.Controls.Add(Me.txtWebErrorNumber)
        Me.Panel5.Controls.Add(Me.txtIPAddress)
        Me.Panel5.Controls.Add(Me.btnSaveWebErrorSolution)
        Me.Panel5.Controls.Add(Me.txtWebErrorCount)
        Me.Panel5.Controls.Add(Me.Label91)
        Me.Panel5.Controls.Add(Me.btnFilterWebErrors)
        Me.Panel5.Controls.Add(Me.Label90)
        Me.Panel5.Controls.Add(Me.rdbResolvedWebErrors)
        Me.Panel5.Controls.Add(Me.Label88)
        Me.Panel5.Controls.Add(Me.rdbUnresolvedWebErrors)
        Me.Panel5.Controls.Add(Me.Label71)
        Me.Panel5.Controls.Add(Me.rdbAllWebErrors)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.txtWebErrorSolution)
        Me.Panel5.Controls.Add(Me.txtWebErrorUser)
        Me.Panel5.Controls.Add(Me.txtWebErrorMessage)
        Me.Panel5.Controls.Add(Me.txtWebErrorLocation)
        Me.Panel5.Controls.Add(Me.txtWebErrorDate)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(778, 401)
        Me.Panel5.TabIndex = 20
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(8, 11)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(69, 13)
        Me.Label95.TabIndex = 0
        Me.Label95.Text = "Error Number"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(368, 11)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(58, 13)
        Me.Label96.TabIndex = 19
        Me.Label96.Text = "IP Address"
        '
        'txtWebErrorNumber
        '
        Me.txtWebErrorNumber.Location = New System.Drawing.Point(88, 9)
        Me.txtWebErrorNumber.Name = "txtWebErrorNumber"
        Me.txtWebErrorNumber.ReadOnly = True
        Me.txtWebErrorNumber.Size = New System.Drawing.Size(60, 20)
        Me.txtWebErrorNumber.TabIndex = 1
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(441, 9)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.ReadOnly = True
        Me.txtIPAddress.Size = New System.Drawing.Size(140, 20)
        Me.txtIPAddress.TabIndex = 18
        '
        'btnSaveWebErrorSolution
        '
        Me.btnSaveWebErrorSolution.Location = New System.Drawing.Point(8, 191)
        Me.btnSaveWebErrorSolution.Name = "btnSaveWebErrorSolution"
        Me.btnSaveWebErrorSolution.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveWebErrorSolution.TabIndex = 2
        Me.btnSaveWebErrorSolution.Text = "Save"
        '
        'txtWebErrorCount
        '
        Me.txtWebErrorCount.Location = New System.Drawing.Point(614, 7)
        Me.txtWebErrorCount.Name = "txtWebErrorCount"
        Me.txtWebErrorCount.ReadOnly = True
        Me.txtWebErrorCount.Size = New System.Drawing.Size(34, 20)
        Me.txtWebErrorCount.TabIndex = 17
        Me.txtWebErrorCount.Text = "0"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Location = New System.Drawing.Point(168, 11)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(29, 13)
        Me.Label91.TabIndex = 3
        Me.Label91.Text = "User"
        '
        'btnFilterWebErrors
        '
        Me.btnFilterWebErrors.Location = New System.Drawing.Point(154, 240)
        Me.btnFilterWebErrors.Name = "btnFilterWebErrors"
        Me.btnFilterWebErrors.Size = New System.Drawing.Size(63, 20)
        Me.btnFilterWebErrors.TabIndex = 16
        Me.btnFilterWebErrors.Text = "Filter"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(8, 33)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(76, 13)
        Me.Label90.TabIndex = 4
        Me.Label90.Text = "Error Location "
        '
        'rdbResolvedWebErrors
        '
        Me.rdbResolvedWebErrors.AutoSize = True
        Me.rdbResolvedWebErrors.Location = New System.Drawing.Point(14, 281)
        Me.rdbResolvedWebErrors.Name = "rdbResolvedWebErrors"
        Me.rdbResolvedWebErrors.Size = New System.Drawing.Size(121, 17)
        Me.rdbResolvedWebErrors.TabIndex = 15
        Me.rdbResolvedWebErrors.Text = "View Resolved Error"
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(8, 59)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(75, 13)
        Me.Label88.TabIndex = 5
        Me.Label88.Text = "Error Message"
        '
        'rdbUnresolvedWebErrors
        '
        Me.rdbUnresolvedWebErrors.AutoSize = True
        Me.rdbUnresolvedWebErrors.Location = New System.Drawing.Point(14, 260)
        Me.rdbUnresolvedWebErrors.Name = "rdbUnresolvedWebErrors"
        Me.rdbUnresolvedWebErrors.Size = New System.Drawing.Size(135, 17)
        Me.rdbUnresolvedWebErrors.TabIndex = 14
        Me.rdbUnresolvedWebErrors.Text = "View Unresolved Errors"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(8, 170)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(73, 13)
        Me.Label71.TabIndex = 6
        Me.Label71.Text = "Error Solution "
        '
        'rdbAllWebErrors
        '
        Me.rdbAllWebErrors.AutoSize = True
        Me.rdbAllWebErrors.Location = New System.Drawing.Point(14, 240)
        Me.rdbAllWebErrors.Name = "rdbAllWebErrors"
        Me.rdbAllWebErrors.Size = New System.Drawing.Size(92, 17)
        Me.rdbAllWebErrors.TabIndex = 13
        Me.rdbAllWebErrors.Text = "View All Errors"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(381, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Error Date"
        '
        'txtWebErrorSolution
        '
        Me.txtWebErrorSolution.AcceptsReturn = True
        Me.txtWebErrorSolution.Location = New System.Drawing.Point(88, 170)
        Me.txtWebErrorSolution.Multiline = True
        Me.txtWebErrorSolution.Name = "txtWebErrorSolution"
        Me.txtWebErrorSolution.Size = New System.Drawing.Size(546, 56)
        Me.txtWebErrorSolution.TabIndex = 12
        '
        'txtWebErrorUser
        '
        Me.txtWebErrorUser.Location = New System.Drawing.Point(201, 9)
        Me.txtWebErrorUser.Name = "txtWebErrorUser"
        Me.txtWebErrorUser.ReadOnly = True
        Me.txtWebErrorUser.Size = New System.Drawing.Size(153, 20)
        Me.txtWebErrorUser.TabIndex = 8
        '
        'txtWebErrorMessage
        '
        Me.txtWebErrorMessage.AcceptsReturn = True
        Me.txtWebErrorMessage.Location = New System.Drawing.Point(88, 59)
        Me.txtWebErrorMessage.Multiline = True
        Me.txtWebErrorMessage.Name = "txtWebErrorMessage"
        Me.txtWebErrorMessage.ReadOnly = True
        Me.txtWebErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWebErrorMessage.Size = New System.Drawing.Size(553, 104)
        Me.txtWebErrorMessage.TabIndex = 11
        '
        'txtWebErrorLocation
        '
        Me.txtWebErrorLocation.Location = New System.Drawing.Point(88, 32)
        Me.txtWebErrorLocation.Name = "txtWebErrorLocation"
        Me.txtWebErrorLocation.ReadOnly = True
        Me.txtWebErrorLocation.Size = New System.Drawing.Size(266, 20)
        Me.txtWebErrorLocation.TabIndex = 9
        '
        'txtWebErrorDate
        '
        Me.txtWebErrorDate.Location = New System.Drawing.Point(468, 32)
        Me.txtWebErrorDate.Name = "txtWebErrorDate"
        Me.txtWebErrorDate.ReadOnly = True
        Me.txtWebErrorDate.Size = New System.Drawing.Size(106, 20)
        Me.txtWebErrorDate.TabIndex = 10
        '
        'dgrWebErrorList
        '
        Me.dgrWebErrorList.DataMember = ""
        Me.dgrWebErrorList.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgrWebErrorList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrWebErrorList.Location = New System.Drawing.Point(0, 0)
        Me.dgrWebErrorList.Name = "dgrWebErrorList"
        Me.dgrWebErrorList.ReadOnly = True
        Me.dgrWebErrorList.Size = New System.Drawing.Size(784, 241)
        Me.dgrWebErrorList.TabIndex = 2
        '
        'TPAddNewFacility
        '
        Me.TPAddNewFacility.Controls.Add(Me.txtApplicationNumber)
        Me.TPAddNewFacility.Controls.Add(Me.btnPreLoadNewFacility)
        Me.TPAddNewFacility.Controls.Add(Me.btnDeleteAIRSNumber)
        Me.TPAddNewFacility.Controls.Add(Me.txtDeleteAIRSNumber)
        Me.TPAddNewFacility.Controls.Add(Me.btnClearAddNewFacility)
        Me.TPAddNewFacility.Controls.Add(Me.GBContactInformation)
        Me.TPAddNewFacility.Controls.Add(Me.GBAirProgramCodes)
        Me.TPAddNewFacility.Controls.Add(Me.GBHeaderData)
        Me.TPAddNewFacility.Controls.Add(Me.GBMailingLocation)
        Me.TPAddNewFacility.Controls.Add(Me.GBFacilityInformation)
        Me.TPAddNewFacility.Controls.Add(Me.llbContactInformation)
        Me.TPAddNewFacility.Controls.Add(Me.llbAirProgramCodes)
        Me.TPAddNewFacility.Controls.Add(Me.llbHeaderData)
        Me.TPAddNewFacility.Controls.Add(Me.llbMailingLocation)
        Me.TPAddNewFacility.Controls.Add(Me.llbFacilityInformation)
        Me.TPAddNewFacility.Controls.Add(Me.btnNewFacility)
        Me.TPAddNewFacility.Controls.Add(Me.Label4)
        Me.TPAddNewFacility.Controls.Add(Me.txtCDSAIRSNumber)
        Me.TPAddNewFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPAddNewFacility.Name = "TPAddNewFacility"
        Me.TPAddNewFacility.Size = New System.Drawing.Size(784, 661)
        Me.TPAddNewFacility.TabIndex = 2
        Me.TPAddNewFacility.Text = "Add New Facility"
        Me.TPAddNewFacility.UseVisualStyleBackColor = True
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Location = New System.Drawing.Point(16, 419)
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtApplicationNumber.TabIndex = 69
        Me.txtApplicationNumber.Text = "App No."
        '
        'btnPreLoadNewFacility
        '
        Me.btnPreLoadNewFacility.Location = New System.Drawing.Point(16, 390)
        Me.btnPreLoadNewFacility.Name = "btnPreLoadNewFacility"
        Me.btnPreLoadNewFacility.Size = New System.Drawing.Size(75, 23)
        Me.btnPreLoadNewFacility.TabIndex = 68
        Me.btnPreLoadNewFacility.Text = "PreLoad"
        Me.btnPreLoadNewFacility.UseVisualStyleBackColor = True
        Me.btnPreLoadNewFacility.Visible = False
        '
        'btnDeleteAIRSNumber
        '
        Me.btnDeleteAIRSNumber.Location = New System.Drawing.Point(13, 335)
        Me.btnDeleteAIRSNumber.Name = "btnDeleteAIRSNumber"
        Me.btnDeleteAIRSNumber.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteAIRSNumber.TabIndex = 67
        Me.btnDeleteAIRSNumber.Text = "Delete AIRS Number"
        Me.btnDeleteAIRSNumber.UseVisualStyleBackColor = True
        Me.btnDeleteAIRSNumber.Visible = False
        '
        'txtDeleteAIRSNumber
        '
        Me.txtDeleteAIRSNumber.Location = New System.Drawing.Point(13, 309)
        Me.txtDeleteAIRSNumber.Name = "txtDeleteAIRSNumber"
        Me.txtDeleteAIRSNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtDeleteAIRSNumber.TabIndex = 66
        '
        'btnClearAddNewFacility
        '
        Me.btnClearAddNewFacility.Location = New System.Drawing.Point(13, 263)
        Me.btnClearAddNewFacility.Name = "btnClearAddNewFacility"
        Me.btnClearAddNewFacility.Size = New System.Drawing.Size(112, 24)
        Me.btnClearAddNewFacility.TabIndex = 65
        Me.btnClearAddNewFacility.Text = "Clear Form"
        '
        'GBContactInformation
        '
        Me.GBContactInformation.Controls.Add(Me.mtbContactNumberExtension)
        Me.GBContactInformation.Controls.Add(Me.txtContactPedigree)
        Me.GBContactInformation.Controls.Add(Me.txtContactSocialTitle)
        Me.GBContactInformation.Controls.Add(Me.Label33)
        Me.GBContactInformation.Controls.Add(Me.Label34)
        Me.GBContactInformation.Controls.Add(Me.txtContactLastName)
        Me.GBContactInformation.Controls.Add(Me.Label35)
        Me.GBContactInformation.Controls.Add(Me.txtContactFirstName)
        Me.GBContactInformation.Controls.Add(Me.Label36)
        Me.GBContactInformation.Controls.Add(Me.mtbContactPhoneNumber)
        Me.GBContactInformation.Controls.Add(Me.Label23)
        Me.GBContactInformation.Controls.Add(Me.Label22)
        Me.GBContactInformation.Controls.Add(Me.Label30)
        Me.GBContactInformation.Controls.Add(Me.txtContactTitle)
        Me.GBContactInformation.Location = New System.Drawing.Point(187, 582)
        Me.GBContactInformation.Name = "GBContactInformation"
        Me.GBContactInformation.Size = New System.Drawing.Size(466, 157)
        Me.GBContactInformation.TabIndex = 64
        Me.GBContactInformation.TabStop = False
        Me.GBContactInformation.Text = "Contact Information"
        Me.GBContactInformation.Visible = False
        '
        'mtbContactNumberExtension
        '
        Me.mtbContactNumberExtension.Location = New System.Drawing.Point(248, 118)
        Me.mtbContactNumberExtension.Mask = "000000"
        Me.mtbContactNumberExtension.Name = "mtbContactNumberExtension"
        Me.mtbContactNumberExtension.Size = New System.Drawing.Size(46, 20)
        Me.mtbContactNumberExtension.TabIndex = 372
        '
        'txtContactPedigree
        '
        Me.txtContactPedigree.Location = New System.Drawing.Point(75, 91)
        Me.txtContactPedigree.Name = "txtContactPedigree"
        Me.txtContactPedigree.Size = New System.Drawing.Size(72, 20)
        Me.txtContactPedigree.TabIndex = 4
        '
        'txtContactSocialTitle
        '
        Me.txtContactSocialTitle.Location = New System.Drawing.Point(76, 16)
        Me.txtContactSocialTitle.Name = "txtContactSocialTitle"
        Me.txtContactSocialTitle.Size = New System.Drawing.Size(72, 20)
        Me.txtContactSocialTitle.TabIndex = 1
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(14, 20)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(59, 13)
        Me.Label33.TabIndex = 371
        Me.Label33.Text = "Social Title"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(14, 95)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(49, 13)
        Me.Label34.TabIndex = 370
        Me.Label34.Text = "Pedigree"
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Location = New System.Drawing.Point(75, 66)
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(151, 20)
        Me.txtContactLastName.TabIndex = 3
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(14, 70)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(58, 13)
        Me.Label35.TabIndex = 369
        Me.Label35.Text = "Last Name"
        '
        'txtContactFirstName
        '
        Me.txtContactFirstName.Location = New System.Drawing.Point(76, 41)
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(151, 20)
        Me.txtContactFirstName.TabIndex = 2
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(14, 45)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(57, 13)
        Me.Label36.TabIndex = 368
        Me.Label36.Text = "First Name"
        '
        'mtbContactPhoneNumber
        '
        Me.mtbContactPhoneNumber.Location = New System.Drawing.Point(102, 118)
        Me.mtbContactPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbContactPhoneNumber.Name = "mtbContactPhoneNumber"
        Me.mtbContactPhoneNumber.Size = New System.Drawing.Size(100, 20)
        Me.mtbContactPhoneNumber.TabIndex = 6
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(220, 122)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(24, 13)
        Me.Label23.TabIndex = 133
        Me.Label23.Text = "ext."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(14, 122)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(78, 13)
        Me.Label22.TabIndex = 130
        Me.Label22.Text = "Phone Number"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(237, 20)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(27, 13)
        Me.Label30.TabIndex = 128
        Me.Label30.Text = "Title"
        '
        'txtContactTitle
        '
        Me.txtContactTitle.Location = New System.Drawing.Point(273, 16)
        Me.txtContactTitle.Name = "txtContactTitle"
        Me.txtContactTitle.Size = New System.Drawing.Size(180, 20)
        Me.txtContactTitle.TabIndex = 5
        '
        'GBAirProgramCodes
        '
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_14)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_7)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_4)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_13)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_3)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_12)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_9)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_10)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_2)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_6)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_1)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_5)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_11)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_8)
        Me.GBAirProgramCodes.Controls.Add(Me.Label37)
        Me.GBAirProgramCodes.Location = New System.Drawing.Point(187, 488)
        Me.GBAirProgramCodes.Name = "GBAirProgramCodes"
        Me.GBAirProgramCodes.Size = New System.Drawing.Size(466, 104)
        Me.GBAirProgramCodes.TabIndex = 63
        Me.GBAirProgramCodes.TabStop = False
        Me.GBAirProgramCodes.Text = "Air Program Codes && Pollutants"
        Me.GBAirProgramCodes.Visible = False
        '
        'chbCDS_14
        '
        Me.chbCDS_14.AutoSize = True
        Me.chbCDS_14.Location = New System.Drawing.Point(327, 51)
        Me.chbCDS_14.Name = "chbCDS_14"
        Me.chbCDS_14.Size = New System.Drawing.Size(128, 17)
        Me.chbCDS_14.TabIndex = 158
        Me.chbCDS_14.Text = "G - Green House Gas"
        '
        'chbCDS_7
        '
        Me.chbCDS_7.AutoSize = True
        Me.chbCDS_7.Location = New System.Drawing.Point(127, 66)
        Me.chbCDS_7.Name = "chbCDS_7"
        Me.chbCDS_7.Size = New System.Drawing.Size(85, 17)
        Me.chbCDS_7.TabIndex = 7
        Me.chbCDS_7.Text = "8 - NESHAP"
        '
        'chbCDS_4
        '
        Me.chbCDS_4.AutoSize = True
        Me.chbCDS_4.Location = New System.Drawing.Point(20, 82)
        Me.chbCDS_4.Name = "chbCDS_4"
        Me.chbCDS_4.Size = New System.Drawing.Size(106, 17)
        Me.chbCDS_4.TabIndex = 4
        Me.chbCDS_4.Text = "4 - CFC Tracking"
        '
        'chbCDS_13
        '
        Me.chbCDS_13.AutoSize = True
        Me.chbCDS_13.Location = New System.Drawing.Point(327, 35)
        Me.chbCDS_13.Name = "chbCDS_13"
        Me.chbCDS_13.Size = New System.Drawing.Size(72, 17)
        Me.chbCDS_13.TabIndex = 13
        Me.chbCDS_13.Text = "V - Title V"
        '
        'chbCDS_3
        '
        Me.chbCDS_3.AutoSize = True
        Me.chbCDS_3.Location = New System.Drawing.Point(20, 66)
        Me.chbCDS_3.Name = "chbCDS_3"
        Me.chbCDS_3.Size = New System.Drawing.Size(85, 17)
        Me.chbCDS_3.TabIndex = 3
        Me.chbCDS_3.Text = "3 - Non Fed."
        '
        'chbCDS_12
        '
        Me.chbCDS_12.AutoSize = True
        Me.chbCDS_12.Location = New System.Drawing.Point(213, 82)
        Me.chbCDS_12.Name = "chbCDS_12"
        Me.chbCDS_12.Size = New System.Drawing.Size(74, 17)
        Me.chbCDS_12.TabIndex = 12
        Me.chbCDS_12.Text = "M - MACT"
        '
        'chbCDS_9
        '
        Me.chbCDS_9.AutoSize = True
        Me.chbCDS_9.Location = New System.Drawing.Point(213, 35)
        Me.chbCDS_9.Name = "chbCDS_9"
        Me.chbCDS_9.Size = New System.Drawing.Size(79, 17)
        Me.chbCDS_9.TabIndex = 9
        Me.chbCDS_9.Text = "F - FESOP "
        '
        'chbCDS_10
        '
        Me.chbCDS_10.AutoSize = True
        Me.chbCDS_10.Location = New System.Drawing.Point(213, 50)
        Me.chbCDS_10.Name = "chbCDS_10"
        Me.chbCDS_10.Size = New System.Drawing.Size(99, 17)
        Me.chbCDS_10.TabIndex = 10
        Me.chbCDS_10.Text = "A - Acid Precip."
        '
        'chbCDS_2
        '
        Me.chbCDS_2.AutoSize = True
        Me.chbCDS_2.Location = New System.Drawing.Point(20, 50)
        Me.chbCDS_2.Name = "chbCDS_2"
        Me.chbCDS_2.Size = New System.Drawing.Size(82, 17)
        Me.chbCDS_2.TabIndex = 2
        Me.chbCDS_2.Text = "1 - Fed. SIP"
        '
        'chbCDS_6
        '
        Me.chbCDS_6.AutoSize = True
        Me.chbCDS_6.Location = New System.Drawing.Point(127, 50)
        Me.chbCDS_6.Name = "chbCDS_6"
        Me.chbCDS_6.Size = New System.Drawing.Size(64, 17)
        Me.chbCDS_6.TabIndex = 6
        Me.chbCDS_6.Text = "7 - NSR"
        '
        'chbCDS_1
        '
        Me.chbCDS_1.AutoSize = True
        Me.chbCDS_1.Location = New System.Drawing.Point(20, 35)
        Me.chbCDS_1.Name = "chbCDS_1"
        Me.chbCDS_1.Size = New System.Drawing.Size(58, 17)
        Me.chbCDS_1.TabIndex = 1
        Me.chbCDS_1.Text = "0 - SIP"
        '
        'chbCDS_5
        '
        Me.chbCDS_5.AutoSize = True
        Me.chbCDS_5.Location = New System.Drawing.Point(127, 35)
        Me.chbCDS_5.Name = "chbCDS_5"
        Me.chbCDS_5.Size = New System.Drawing.Size(63, 17)
        Me.chbCDS_5.TabIndex = 5
        Me.chbCDS_5.Text = "6 - PSD"
        '
        'chbCDS_11
        '
        Me.chbCDS_11.AutoSize = True
        Me.chbCDS_11.Location = New System.Drawing.Point(213, 66)
        Me.chbCDS_11.Name = "chbCDS_11"
        Me.chbCDS_11.Size = New System.Drawing.Size(116, 17)
        Me.chbCDS_11.TabIndex = 11
        Me.chbCDS_11.Text = "I - Native American"
        '
        'chbCDS_8
        '
        Me.chbCDS_8.AutoSize = True
        Me.chbCDS_8.Location = New System.Drawing.Point(127, 82)
        Me.chbCDS_8.Name = "chbCDS_8"
        Me.chbCDS_8.Size = New System.Drawing.Size(70, 17)
        Me.chbCDS_8.TabIndex = 8
        Me.chbCDS_8.Text = "9 - NSPS"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(16, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(106, 13)
        Me.Label37.TabIndex = 157
        Me.Label37.Text = "(Select All that apply)"
        '
        'GBHeaderData
        '
        Me.GBHeaderData.Controls.Add(Me.mtbCDSSICCode)
        Me.GBHeaderData.Controls.Add(Me.txtCDSRegionCode)
        Me.GBHeaderData.Controls.Add(Me.Label21)
        Me.GBHeaderData.Controls.Add(Me.cboCDSOperationalStatus)
        Me.GBHeaderData.Controls.Add(Me.Label51)
        Me.GBHeaderData.Controls.Add(Me.cboCDSClassCode)
        Me.GBHeaderData.Controls.Add(Me.Label49)
        Me.GBHeaderData.Controls.Add(Me.Label42)
        Me.GBHeaderData.Controls.Add(Me.Label63)
        Me.GBHeaderData.Controls.Add(Me.txtFacilityDescription)
        Me.GBHeaderData.Location = New System.Drawing.Point(186, 351)
        Me.GBHeaderData.Name = "GBHeaderData"
        Me.GBHeaderData.Size = New System.Drawing.Size(466, 131)
        Me.GBHeaderData.TabIndex = 62
        Me.GBHeaderData.TabStop = False
        Me.GBHeaderData.Text = "Header Data"
        Me.GBHeaderData.Visible = False
        '
        'mtbCDSSICCode
        '
        Me.mtbCDSSICCode.Location = New System.Drawing.Point(104, 47)
        Me.mtbCDSSICCode.Mask = "0000"
        Me.mtbCDSSICCode.Name = "mtbCDSSICCode"
        Me.mtbCDSSICCode.Size = New System.Drawing.Size(76, 20)
        Me.mtbCDSSICCode.TabIndex = 169
        '
        'txtCDSRegionCode
        '
        Me.txtCDSRegionCode.Location = New System.Drawing.Point(104, 16)
        Me.txtCDSRegionCode.MaxLength = 4
        Me.txtCDSRegionCode.Name = "txtCDSRegionCode"
        Me.txtCDSRegionCode.ReadOnly = True
        Me.txtCDSRegionCode.Size = New System.Drawing.Size(76, 20)
        Me.txtCDSRegionCode.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(27, 18)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 13)
        Me.Label21.TabIndex = 167
        Me.Label21.Text = "Region Code"
        '
        'cboCDSOperationalStatus
        '
        Me.cboCDSOperationalStatus.Location = New System.Drawing.Point(300, 16)
        Me.cboCDSOperationalStatus.Name = "cboCDSOperationalStatus"
        Me.cboCDSOperationalStatus.Size = New System.Drawing.Size(153, 21)
        Me.cboCDSOperationalStatus.TabIndex = 1
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(200, 18)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(94, 26)
        Me.Label51.TabIndex = 168
        Me.Label51.Text = "Operational Status" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Use X only)"
        '
        'cboCDSClassCode
        '
        Me.cboCDSClassCode.Location = New System.Drawing.Point(300, 47)
        Me.cboCDSClassCode.Name = "cboCDSClassCode"
        Me.cboCDSClassCode.Size = New System.Drawing.Size(153, 21)
        Me.cboCDSClassCode.TabIndex = 3
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(193, 49)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(95, 13)
        Me.Label49.TabIndex = 164
        Me.Label49.Text = "Facility Class Code"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(44, 47)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(52, 13)
        Me.Label42.TabIndex = 161
        Me.Label42.Text = "SIC Code"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(8, 72)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(87, 13)
        Me.Label63.TabIndex = 8
        Me.Label63.Text = "Plant Description"
        '
        'txtFacilityDescription
        '
        Me.txtFacilityDescription.Location = New System.Drawing.Point(24, 88)
        Me.txtFacilityDescription.MaxLength = 4000
        Me.txtFacilityDescription.Name = "txtFacilityDescription"
        Me.txtFacilityDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityDescription.Size = New System.Drawing.Size(429, 20)
        Me.txtFacilityDescription.TabIndex = 4
        '
        'GBMailingLocation
        '
        Me.GBMailingLocation.Controls.Add(Me.mtbMailingZipCode)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingState)
        Me.GBMailingLocation.Controls.Add(Me.Label18)
        Me.GBMailingLocation.Controls.Add(Me.Label19)
        Me.GBMailingLocation.Controls.Add(Me.Label20)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingCity)
        Me.GBMailingLocation.Controls.Add(Me.Label24)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingAddress)
        Me.GBMailingLocation.Location = New System.Drawing.Point(187, 243)
        Me.GBMailingLocation.Name = "GBMailingLocation"
        Me.GBMailingLocation.Size = New System.Drawing.Size(465, 104)
        Me.GBMailingLocation.TabIndex = 61
        Me.GBMailingLocation.TabStop = False
        Me.GBMailingLocation.Text = "Mailing Location"
        Me.GBMailingLocation.Visible = False
        '
        'mtbMailingZipCode
        '
        Me.mtbMailingZipCode.Location = New System.Drawing.Point(104, 66)
        Me.mtbMailingZipCode.Mask = "00000-9999"
        Me.mtbMailingZipCode.Name = "mtbMailingZipCode"
        Me.mtbMailingZipCode.Size = New System.Drawing.Size(72, 20)
        Me.mtbMailingZipCode.TabIndex = 3
        '
        'txtMailingState
        '
        Me.txtMailingState.Location = New System.Drawing.Point(312, 40)
        Me.txtMailingState.MaxLength = 2
        Me.txtMailingState.Name = "txtMailingState"
        Me.txtMailingState.Size = New System.Drawing.Size(24, 20)
        Me.txtMailingState.TabIndex = 3
        Me.txtMailingState.Text = "GA"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(38, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 13)
        Me.Label18.TabIndex = 162
        Me.Label18.Text = "Mailing Zip"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(272, 42)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 161
        Me.Label19.Text = "State"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(34, 42)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 13)
        Me.Label20.TabIndex = 160
        Me.Label20.Text = "Mailing City"
        '
        'txtMailingCity
        '
        Me.txtMailingCity.Location = New System.Drawing.Point(104, 40)
        Me.txtMailingCity.Name = "txtMailingCity"
        Me.txtMailingCity.Size = New System.Drawing.Size(160, 20)
        Me.txtMailingCity.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 18)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(81, 13)
        Me.Label24.TabIndex = 159
        Me.Label24.Text = "Mailing Address"
        '
        'txtMailingAddress
        '
        Me.txtMailingAddress.Location = New System.Drawing.Point(104, 16)
        Me.txtMailingAddress.Name = "txtMailingAddress"
        Me.txtMailingAddress.Size = New System.Drawing.Size(352, 20)
        Me.txtMailingAddress.TabIndex = 1
        '
        'GBFacilityInformation
        '
        Me.GBFacilityInformation.Controls.Add(Me.mtbFacilityLongitude)
        Me.GBFacilityInformation.Controls.Add(Me.mtbFacilityLatitude)
        Me.GBFacilityInformation.Controls.Add(Me.mtbCDSZipCode)
        Me.GBFacilityInformation.Controls.Add(Me.Label103)
        Me.GBFacilityInformation.Controls.Add(Me.Label102)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSStreetAddress)
        Me.GBFacilityInformation.Controls.Add(Me.Label5)
        Me.GBFacilityInformation.Controls.Add(Me.Label9)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSFacilityName)
        Me.GBFacilityInformation.Controls.Add(Me.Label7)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSCity)
        Me.GBFacilityInformation.Controls.Add(Me.Label8)
        Me.GBFacilityInformation.Controls.Add(Me.Label10)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSState)
        Me.GBFacilityInformation.Controls.Add(Me.Label27)
        Me.GBFacilityInformation.Controls.Add(Me.Label28)
        Me.GBFacilityInformation.Location = New System.Drawing.Point(187, 0)
        Me.GBFacilityInformation.Name = "GBFacilityInformation"
        Me.GBFacilityInformation.Size = New System.Drawing.Size(466, 243)
        Me.GBFacilityInformation.TabIndex = 60
        Me.GBFacilityInformation.TabStop = False
        Me.GBFacilityInformation.Text = "Facility Information"
        Me.GBFacilityInformation.Visible = False
        '
        'mtbFacilityLongitude
        '
        Me.mtbFacilityLongitude.Location = New System.Drawing.Point(330, 135)
        Me.mtbFacilityLongitude.Mask = "-00.000000"
        Me.mtbFacilityLongitude.Name = "mtbFacilityLongitude"
        Me.mtbFacilityLongitude.Size = New System.Drawing.Size(69, 20)
        Me.mtbFacilityLongitude.TabIndex = 175
        '
        'mtbFacilityLatitude
        '
        Me.mtbFacilityLatitude.Location = New System.Drawing.Point(104, 131)
        Me.mtbFacilityLatitude.Mask = "00.000000"
        Me.mtbFacilityLatitude.Name = "mtbFacilityLatitude"
        Me.mtbFacilityLatitude.Size = New System.Drawing.Size(72, 20)
        Me.mtbFacilityLatitude.TabIndex = 174
        '
        'mtbCDSZipCode
        '
        Me.mtbCDSZipCode.Location = New System.Drawing.Point(104, 95)
        Me.mtbCDSZipCode.Mask = "00000-9999"
        Me.mtbCDSZipCode.Name = "mtbCDSZipCode"
        Me.mtbCDSZipCode.Size = New System.Drawing.Size(72, 20)
        Me.mtbCDSZipCode.TabIndex = 4
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Location = New System.Drawing.Point(294, 158)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(159, 13)
        Me.Label103.TabIndex = 173
        Me.Label103.Text = "(Range = 80.84111 -- 85.60444)"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(80, 158)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(147, 13)
        Me.Label102.TabIndex = 171
        Me.Label102.Text = "(Range = 30.3555 -- 35.0000)"
        '
        'txtCDSStreetAddress
        '
        Me.txtCDSStreetAddress.Location = New System.Drawing.Point(104, 48)
        Me.txtCDSStreetAddress.MaxLength = 100
        Me.txtCDSStreetAddress.Name = "txtCDSStreetAddress"
        Me.txtCDSStreetAddress.Size = New System.Drawing.Size(349, 20)
        Me.txtCDSStreetAddress.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(272, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "State"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(24, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Facility Name"
        '
        'txtCDSFacilityName
        '
        Me.txtCDSFacilityName.Location = New System.Drawing.Point(104, 24)
        Me.txtCDSFacilityName.MaxLength = 100
        Me.txtCDSFacilityName.Name = "txtCDSFacilityName"
        Me.txtCDSFacilityName.Size = New System.Drawing.Size(349, 20)
        Me.txtCDSFacilityName.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(73, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "City"
        '
        'txtCDSCity
        '
        Me.txtCDSCity.Location = New System.Drawing.Point(104, 72)
        Me.txtCDSCity.MaxLength = 50
        Me.txtCDSCity.Name = "txtCDSCity"
        Me.txtCDSCity.Size = New System.Drawing.Size(160, 20)
        Me.txtCDSCity.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Street Address"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(47, 98)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Zip Code"
        '
        'txtCDSState
        '
        Me.txtCDSState.Location = New System.Drawing.Point(312, 72)
        Me.txtCDSState.MaxLength = 4
        Me.txtCDSState.Name = "txtCDSState"
        Me.txtCDSState.ReadOnly = True
        Me.txtCDSState.Size = New System.Drawing.Size(24, 20)
        Me.txtCDSState.TabIndex = 7
        Me.txtCDSState.TabStop = False
        Me.txtCDSState.Text = "GA"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(270, 138)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(54, 13)
        Me.Label27.TabIndex = 156
        Me.Label27.Text = "Longitude"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(47, 138)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(45, 13)
        Me.Label28.TabIndex = 155
        Me.Label28.Text = "Latitude"
        '
        'llbContactInformation
        '
        Me.llbContactInformation.AutoSize = True
        Me.llbContactInformation.Enabled = False
        Me.llbContactInformation.Location = New System.Drawing.Point(13, 180)
        Me.llbContactInformation.Name = "llbContactInformation"
        Me.llbContactInformation.Size = New System.Drawing.Size(99, 13)
        Me.llbContactInformation.TabIndex = 57
        Me.llbContactInformation.TabStop = True
        Me.llbContactInformation.Text = "Contact Information"
        '
        'llbAirProgramCodes
        '
        Me.llbAirProgramCodes.AutoSize = True
        Me.llbAirProgramCodes.Enabled = False
        Me.llbAirProgramCodes.Location = New System.Drawing.Point(13, 146)
        Me.llbAirProgramCodes.Name = "llbAirProgramCodes"
        Me.llbAirProgramCodes.Size = New System.Drawing.Size(152, 13)
        Me.llbAirProgramCodes.TabIndex = 56
        Me.llbAirProgramCodes.TabStop = True
        Me.llbAirProgramCodes.Text = "Air Program Codes && Pollutants"
        '
        'llbHeaderData
        '
        Me.llbHeaderData.AutoSize = True
        Me.llbHeaderData.Enabled = False
        Me.llbHeaderData.Location = New System.Drawing.Point(13, 111)
        Me.llbHeaderData.Name = "llbHeaderData"
        Me.llbHeaderData.Size = New System.Drawing.Size(68, 13)
        Me.llbHeaderData.TabIndex = 55
        Me.llbHeaderData.TabStop = True
        Me.llbHeaderData.Text = "Header Data"
        '
        'llbMailingLocation
        '
        Me.llbMailingLocation.AutoSize = True
        Me.llbMailingLocation.Enabled = False
        Me.llbMailingLocation.Location = New System.Drawing.Point(13, 76)
        Me.llbMailingLocation.Name = "llbMailingLocation"
        Me.llbMailingLocation.Size = New System.Drawing.Size(84, 13)
        Me.llbMailingLocation.TabIndex = 54
        Me.llbMailingLocation.TabStop = True
        Me.llbMailingLocation.Text = "Mailing Location"
        '
        'llbFacilityInformation
        '
        Me.llbFacilityInformation.AutoSize = True
        Me.llbFacilityInformation.Enabled = False
        Me.llbFacilityInformation.Location = New System.Drawing.Point(13, 42)
        Me.llbFacilityInformation.Name = "llbFacilityInformation"
        Me.llbFacilityInformation.Size = New System.Drawing.Size(94, 13)
        Me.llbFacilityInformation.TabIndex = 53
        Me.llbFacilityInformation.TabStop = True
        Me.llbFacilityInformation.Text = "Facility Information"
        '
        'btnNewFacility
        '
        Me.btnNewFacility.Enabled = False
        Me.btnNewFacility.Location = New System.Drawing.Point(13, 215)
        Me.btnNewFacility.Name = "btnNewFacility"
        Me.btnNewFacility.Size = New System.Drawing.Size(112, 23)
        Me.btnNewFacility.TabIndex = 58
        Me.btnNewFacility.Text = "Create New Facility"
        Me.btnNewFacility.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "AIRS Number:"
        '
        'txtCDSAIRSNumber
        '
        Me.txtCDSAIRSNumber.Location = New System.Drawing.Point(87, 7)
        Me.txtCDSAIRSNumber.MaxLength = 12
        Me.txtCDSAIRSNumber.Name = "txtCDSAIRSNumber"
        Me.txtCDSAIRSNumber.Size = New System.Drawing.Size(88, 20)
        Me.txtCDSAIRSNumber.TabIndex = 52
        Me.txtCDSAIRSNumber.Text = "0413"
        '
        'DMUDeveloperTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 687)
        Me.Controls.Add(Me.TCDMUTools)
        Me.Name = "DMUDeveloperTools"
        Me.Text = "AFS Tools"
        Me.TCDMUTools.ResumeLayout(False)
        Me.TPAFSFileGenerator.ResumeLayout(False)
        Me.TPAFSFileGenerator.PerformLayout()
        Me.PanelBatchOrder.ResumeLayout(False)
        Me.pnlBasicRefresh.ResumeLayout(False)
        Me.pnlBasicRefresh.PerformLayout()
        Me.pnlSubParts.ResumeLayout(False)
        Me.pnlSubParts.PerformLayout()
        Me.pnlAIRSSpecific.ResumeLayout(False)
        Me.pnlAIRSSpecific.PerformLayout()
        Me.pnlStandardFile.ResumeLayout(False)
        Me.pnlStandardFile.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.TPErrorLog.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.dgvErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPWebErrorLog.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgrWebErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPAddNewFacility.ResumeLayout(False)
        Me.TPAddNewFacility.PerformLayout()
        Me.GBContactInformation.ResumeLayout(False)
        Me.GBContactInformation.PerformLayout()
        Me.GBAirProgramCodes.ResumeLayout(False)
        Me.GBAirProgramCodes.PerformLayout()
        Me.GBHeaderData.ResumeLayout(False)
        Me.GBHeaderData.PerformLayout()
        Me.GBMailingLocation.ResumeLayout(False)
        Me.GBMailingLocation.PerformLayout()
        Me.GBFacilityInformation.ResumeLayout(False)
        Me.GBFacilityInformation.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bgwTransfer As System.ComponentModel.BackgroundWorker
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TCDMUTools As System.Windows.Forms.TabControl
    Friend WithEvents TPWebErrorLog As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorCount As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterWebErrors As System.Windows.Forms.Button
    Friend WithEvents rdbResolvedWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnresolvedWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAllWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents txtWebErrorSolution As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorDate As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorUser As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents btnSaveWebErrorSolution As System.Windows.Forms.Button
    Friend WithEvents txtWebErrorNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents dgrWebErrorList As System.Windows.Forms.DataGrid
    Friend WithEvents TPErrorLog As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtErrorCount As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterErrors As System.Windows.Forms.Button
    Friend WithEvents rdbViewResolvedErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewUnresolvedErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewAllErrors As System.Windows.Forms.RadioButton
    Friend WithEvents txtErrorSolution As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorDate As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorUser As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents btnSaveError As System.Windows.Forms.Button
    Friend WithEvents txtErrorNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents TPAddNewFacility As System.Windows.Forms.TabPage
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnPreLoadNewFacility As System.Windows.Forms.Button
    Friend WithEvents btnDeleteAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents txtDeleteAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnClearAddNewFacility As System.Windows.Forms.Button
    Friend WithEvents GBContactInformation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbContactNumberExtension As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtContactPedigree As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents mtbContactPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents GBAirProgramCodes As System.Windows.Forms.GroupBox
    Friend WithEvents chbCDS_7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_13 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_12 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_10 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_11 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_8 As System.Windows.Forms.CheckBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents GBHeaderData As System.Windows.Forms.GroupBox
    Friend WithEvents mtbCDSSICCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCDSRegionCode As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboCDSOperationalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cboCDSClassCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityDescription As System.Windows.Forms.TextBox
    Friend WithEvents GBMailingLocation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbMailingZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtMailingState As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMailingCity As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtMailingAddress As System.Windows.Forms.TextBox
    Friend WithEvents GBFacilityInformation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbFacilityLongitude As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbFacilityLatitude As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbCDSZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents txtCDSStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCDSFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCDSCity As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCDSState As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents llbContactInformation As System.Windows.Forms.LinkLabel
    Friend WithEvents llbAirProgramCodes As System.Windows.Forms.LinkLabel
    Friend WithEvents llbHeaderData As System.Windows.Forms.LinkLabel
    Friend WithEvents llbMailingLocation As System.Windows.Forms.LinkLabel
    Friend WithEvents llbFacilityInformation As System.Windows.Forms.LinkLabel
    Friend WithEvents btnNewFacility As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCDSAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents TPAFSFileGenerator As System.Windows.Forms.TabPage
    Friend WithEvents txtAFSBatchFile As System.Windows.Forms.TextBox
    Friend WithEvents PanelBatchOrder As System.Windows.Forms.Panel
    Friend WithEvents btnClearAFSFileGenerator As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents btnGenerateBatchFile As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents btnForceBasicRefresh As System.Windows.Forms.Button
    Friend WithEvents chbCDS_14 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbNoLimit As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLast60days As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLast30Days As System.Windows.Forms.RadioButton
    Friend WithEvents btnExporttoExcel As System.Windows.Forms.Button
    Friend WithEvents dgvErrorList As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdateAllSubParts As System.Windows.Forms.Button
    Friend WithEvents mtbAFSAirsNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAIRSSpecificRefresh As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlStandardFile As System.Windows.Forms.Panel
    Friend WithEvents pnlBasicRefresh As System.Windows.Forms.Panel
    Friend WithEvents pnlSubParts As System.Windows.Forms.Panel
    Friend WithEvents pnlAIRSSpecific As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents rdbUpdateAllSubparts As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAIRSSpecific As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGenerateStandardFile As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBasicData As System.Windows.Forms.RadioButton
End Class
