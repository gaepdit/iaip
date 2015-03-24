<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUDeveloperTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DMUDeveloperTool))
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
        'DMUDeveloperTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 687)
        Me.Controls.Add(Me.TCDMUTools)
        Me.Name = "DMUDeveloperTool"
        Me.Text = "DMU Developer Tools"
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
