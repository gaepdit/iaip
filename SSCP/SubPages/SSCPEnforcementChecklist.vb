Imports Oracle.DataAccess.Client


Public Class SSCPEnforcementChecklist
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim dsWorkEnTry As DataSet
    Dim daWorkEnTry As OracleDataAdapter

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityInformation As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TBEnforcement As System.Windows.Forms.ToolBar
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbDelete As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTPViolationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOpenEvent As System.Windows.Forms.Button
    Friend WithEvents chbWorkType As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chbAllDates As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DTPStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnViewLON As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GBFilterOptions As System.Windows.Forms.GroupBox
    Friend WithEvents dgrComplianceEvents As System.Windows.Forms.DataGrid
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtWorkCount As System.Windows.Forms.TextBox
    Friend WithEvents chbAllWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbInspections As System.Windows.Forms.CheckBox
    Friend WithEvents chbPerformanceTests As System.Windows.Forms.CheckBox
    Friend WithEvents chbNotifications As System.Windows.Forms.CheckBox
    Friend WithEvents chbReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbACCs As System.Windows.Forms.CheckBox
    Friend WithEvents chbDateReceived As System.Windows.Forms.CheckBox
    Friend WithEvents btnLinkEvent As System.Windows.Forms.Button
    Friend WithEvents btnCreateNewEvent As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPEnforcementChecklist))
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFacilityInformation = New System.Windows.Forms.TextBox
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem12 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mmiDelete = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBEnforcement = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbDelete = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.tbbExit = New System.Windows.Forms.ToolBarButton
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnLinkEvent = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnViewLON = New System.Windows.Forms.Button
        Me.btnOpenEvent = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.DTPViolationDate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgrComplianceEvents = New System.Windows.Forms.DataGrid
        Me.GBFilterOptions = New System.Windows.Forms.GroupBox
        Me.btnCreateNewEvent = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtWorkCount = New System.Windows.Forms.TextBox
        Me.chbDateReceived = New System.Windows.Forms.CheckBox
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.DTPEndDate = New System.Windows.Forms.DateTimePicker
        Me.DTPStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.chbAllDates = New System.Windows.Forms.CheckBox
        Me.btnRunFilter = New System.Windows.Forms.Button
        Me.chbWorkType = New System.Windows.Forms.CheckBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chbInspections = New System.Windows.Forms.CheckBox
        Me.chbPerformanceTests = New System.Windows.Forms.CheckBox
        Me.chbNotifications = New System.Windows.Forms.CheckBox
        Me.chbReports = New System.Windows.Forms.CheckBox
        Me.chbACCs = New System.Windows.Forms.CheckBox
        Me.chbAllWork = New System.Windows.Forms.CheckBox
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgrComplianceEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBFilterOptions.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 256
        Me.Label3.Text = "Facility Info:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtFacilityInformation
        '
        Me.txtFacilityInformation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFacilityInformation.Location = New System.Drawing.Point(80, 8)
        Me.txtFacilityInformation.Multiline = True
        Me.txtFacilityInformation.Name = "txtFacilityInformation"
        Me.txtFacilityInformation.ReadOnly = True
        Me.txtFacilityInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityInformation.Size = New System.Drawing.Size(280, 86)
        Me.txtFacilityInformation.TabIndex = 254
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiBack, Me.MenuItem9, Me.MmiExit})
        Me.MenuItem1.Text = "File"
        '
        'MmiSave
        '
        Me.MmiSave.Index = 0
        Me.MmiSave.Text = "Save"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "-"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 2
        Me.MmiBack.Text = "Back"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 3
        Me.MenuItem9.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 4
        Me.MmiExit.Text = "Exit"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem12, Me.mmiClear})
        Me.MenuItem2.Text = "Edit"
        '
        'mmiCut
        '
        Me.mmiCut.Index = 0
        Me.mmiCut.Text = "Cut"
        '
        'mmiCopy
        '
        Me.mmiCopy.Index = 1
        Me.mmiCopy.Text = "Copy"
        '
        'mmiPaste
        '
        Me.mmiPaste.Index = 2
        Me.mmiPaste.Text = "Paste"
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 3
        Me.MenuItem12.Text = "-"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 4
        Me.mmiClear.Text = "Clear"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiDelete})
        Me.MenuItem4.Text = "Tools"
        '
        'mmiDelete
        '
        Me.mmiDelete.Index = 0
        Me.mmiDelete.Text = "Delete"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 3
        Me.mmiHelp.Text = "Help"
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
        'TBEnforcement
        '
        Me.TBEnforcement.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbDelete, Me.tbbBack, Me.tbbExit})
        Me.TBEnforcement.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBEnforcement.DropDownArrows = True
        Me.TBEnforcement.ImageList = Me.Image_List_All
        Me.TBEnforcement.Location = New System.Drawing.Point(0, 0)
        Me.TBEnforcement.Name = "TBEnforcement"
        Me.TBEnforcement.ShowToolTips = True
        Me.TBEnforcement.Size = New System.Drawing.Size(376, 28)
        Me.TBEnforcement.TabIndex = 257
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        '
        'tbbDelete
        '
        Me.tbbDelete.ImageIndex = 13
        Me.tbbDelete.Name = "tbbDelete"
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        '
        'tbbExit
        '
        Me.tbbExit.ImageIndex = 81
        Me.tbbExit.Name = "tbbExit"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(0, 48)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtAIRSNumber.TabIndex = 258
        Me.txtAIRSNumber.Visible = False
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(16, 48)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtEnforcementNumber.TabIndex = 259
        Me.txtEnforcementNumber.Visible = False
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(104, 102)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(104, 20)
        Me.txtTrackingNumber.TabIndex = 260
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnLinkEvent)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.btnViewLON)
        Me.Panel4.Controls.Add(Me.btnOpenEvent)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.DTPViolationDate)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.txtFacilityInformation)
        Me.Panel4.Controls.Add(Me.txtTrackingNumber)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 28)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(376, 156)
        Me.Panel4.TabIndex = 262
        '
        'btnLinkEvent
        '
        Me.btnLinkEvent.Location = New System.Drawing.Point(216, 104)
        Me.btnLinkEvent.Name = "btnLinkEvent"
        Me.btnLinkEvent.Size = New System.Drawing.Size(75, 23)
        Me.btnLinkEvent.TabIndex = 297
        Me.btnLinkEvent.Text = "Link Event"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(288, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 9)
        Me.Label9.TabIndex = 296
        Me.Label9.Text = "Filter Options:"
        '
        'btnViewLON
        '
        Me.btnViewLON.AutoSize = True
        Me.btnViewLON.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewLON.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewLON.ImageIndex = 53
        Me.btnViewLON.ImageList = Me.Image_List_All
        Me.btnViewLON.Location = New System.Drawing.Point(344, 132)
        Me.btnViewLON.Name = "btnViewLON"
        Me.btnViewLON.Size = New System.Drawing.Size(22, 22)
        Me.btnViewLON.TabIndex = 295
        '
        'btnOpenEvent
        '
        Me.btnOpenEvent.Location = New System.Drawing.Point(296, 104)
        Me.btnOpenEvent.Name = "btnOpenEvent"
        Me.btnOpenEvent.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenEvent.TabIndex = 263
        Me.btnOpenEvent.Text = "View Event"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 262
        Me.Label2.Text = "Discovery Date:"
        '
        'DTPViolationDate
        '
        Me.DTPViolationDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPViolationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPViolationDate.Location = New System.Drawing.Point(104, 126)
        Me.DTPViolationDate.Name = "DTPViolationDate"
        Me.DTPViolationDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPViolationDate.TabIndex = 261
        Me.DTPViolationDate.Value = New Date(2005, 5, 13, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 257
        Me.Label1.Text = "Tracking Number:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgrComplianceEvents)
        Me.GroupBox1.Controls.Add(Me.GBFilterOptions)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 184)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(376, 497)
        Me.GroupBox1.TabIndex = 263
        Me.GroupBox1.TabStop = False
        '
        'dgrComplianceEvents
        '
        Me.dgrComplianceEvents.DataMember = ""
        Me.dgrComplianceEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrComplianceEvents.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrComplianceEvents.Location = New System.Drawing.Point(3, 248)
        Me.dgrComplianceEvents.Name = "dgrComplianceEvents"
        Me.dgrComplianceEvents.ReadOnly = True
        Me.dgrComplianceEvents.Size = New System.Drawing.Size(370, 246)
        Me.dgrComplianceEvents.TabIndex = 263
        '
        'GBFilterOptions
        '
        Me.GBFilterOptions.Controls.Add(Me.btnCreateNewEvent)
        Me.GBFilterOptions.Controls.Add(Me.Label8)
        Me.GBFilterOptions.Controls.Add(Me.txtWorkCount)
        Me.GBFilterOptions.Controls.Add(Me.chbDateReceived)
        Me.GBFilterOptions.Controls.Add(Me.Panel6)
        Me.GBFilterOptions.Controls.Add(Me.btnRunFilter)
        Me.GBFilterOptions.Controls.Add(Me.chbWorkType)
        Me.GBFilterOptions.Controls.Add(Me.Panel5)
        Me.GBFilterOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBFilterOptions.Location = New System.Drawing.Point(3, 16)
        Me.GBFilterOptions.Name = "GBFilterOptions"
        Me.GBFilterOptions.Size = New System.Drawing.Size(370, 232)
        Me.GBFilterOptions.TabIndex = 262
        Me.GBFilterOptions.TabStop = False
        Me.GBFilterOptions.Text = "Filter for Facility Events"
        '
        'btnCreateNewEvent
        '
        Me.btnCreateNewEvent.AutoSize = True
        Me.btnCreateNewEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCreateNewEvent.Location = New System.Drawing.Point(264, 144)
        Me.btnCreateNewEvent.Name = "btnCreateNewEvent"
        Me.btnCreateNewEvent.Size = New System.Drawing.Size(104, 23)
        Me.btnCreateNewEvent.TabIndex = 290
        Me.btnCreateNewEvent.Text = "Create New Event"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(264, 216)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 9)
        Me.Label8.TabIndex = 289
        Me.Label8.Text = "Record Count:"
        '
        'txtWorkCount
        '
        Me.txtWorkCount.Location = New System.Drawing.Point(320, 208)
        Me.txtWorkCount.Name = "txtWorkCount"
        Me.txtWorkCount.ReadOnly = True
        Me.txtWorkCount.Size = New System.Drawing.Size(48, 20)
        Me.txtWorkCount.TabIndex = 288
        '
        'chbDateReceived
        '
        Me.chbDateReceived.Checked = True
        Me.chbDateReceived.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbDateReceived.Location = New System.Drawing.Point(0, 144)
        Me.chbDateReceived.Name = "chbDateReceived"
        Me.chbDateReceived.Size = New System.Drawing.Size(104, 16)
        Me.chbDateReceived.TabIndex = 3
        Me.chbDateReceived.Text = "Date Recieved"
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.DTPEndDate)
        Me.Panel6.Controls.Add(Me.DTPStartDate)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Controls.Add(Me.chbAllDates)
        Me.Panel6.Location = New System.Drawing.Point(8, 144)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(248, 88)
        Me.Panel6.TabIndex = 4
        '
        'DTPEndDate
        '
        Me.DTPEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEndDate.Enabled = False
        Me.DTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEndDate.Location = New System.Drawing.Point(136, 56)
        Me.DTPEndDate.Name = "DTPEndDate"
        Me.DTPEndDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEndDate.TabIndex = 263
        Me.DTPEndDate.Value = New Date(2005, 5, 13, 0, 0, 0, 0)
        '
        'DTPStartDate
        '
        Me.DTPStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartDate.Enabled = False
        Me.DTPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartDate.Location = New System.Drawing.Point(24, 56)
        Me.DTPStartDate.Name = "DTPStartDate"
        Me.DTPStartDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPStartDate.TabIndex = 262
        Me.DTPStartDate.Value = New Date(2005, 5, 13, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(144, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "End Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Start Date"
        '
        'chbAllDates
        '
        Me.chbAllDates.Checked = True
        Me.chbAllDates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbAllDates.Location = New System.Drawing.Point(16, 16)
        Me.chbAllDates.Name = "chbAllDates"
        Me.chbAllDates.Size = New System.Drawing.Size(80, 16)
        Me.chbAllDates.TabIndex = 0
        Me.chbAllDates.Text = "All date(s)"
        '
        'btnRunFilter
        '
        Me.btnRunFilter.AutoSize = True
        Me.btnRunFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunFilter.Location = New System.Drawing.Point(264, 24)
        Me.btnRunFilter.Name = "btnRunFilter"
        Me.btnRunFilter.Size = New System.Drawing.Size(62, 23)
        Me.btnRunFilter.TabIndex = 2
        Me.btnRunFilter.Text = "Run Filter"
        '
        'chbWorkType
        '
        Me.chbWorkType.Checked = True
        Me.chbWorkType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbWorkType.Location = New System.Drawing.Point(0, 16)
        Me.chbWorkType.Name = "chbWorkType"
        Me.chbWorkType.Size = New System.Drawing.Size(80, 16)
        Me.chbWorkType.TabIndex = 0
        Me.chbWorkType.Text = "Work Type"
        '
        'Panel5
        '
        Me.Panel5.AutoSize = True
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.chbInspections)
        Me.Panel5.Controls.Add(Me.chbPerformanceTests)
        Me.Panel5.Controls.Add(Me.chbNotifications)
        Me.Panel5.Controls.Add(Me.chbReports)
        Me.Panel5.Controls.Add(Me.chbACCs)
        Me.Panel5.Controls.Add(Me.chbAllWork)
        Me.Panel5.Location = New System.Drawing.Point(8, 24)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(249, 111)
        Me.Panel5.TabIndex = 1
        '
        'chbInspections
        '
        Me.chbInspections.Location = New System.Drawing.Point(16, 44)
        Me.chbInspections.Name = "chbInspections"
        Me.chbInspections.Size = New System.Drawing.Size(104, 24)
        Me.chbInspections.TabIndex = 2
        Me.chbInspections.Text = "Inspection(s)"
        '
        'chbPerformanceTests
        '
        Me.chbPerformanceTests.Location = New System.Drawing.Point(16, 63)
        Me.chbPerformanceTests.Name = "chbPerformanceTests"
        Me.chbPerformanceTests.Size = New System.Drawing.Size(136, 24)
        Me.chbPerformanceTests.TabIndex = 4
        Me.chbPerformanceTests.Text = "Performance Test(s)"
        '
        'chbNotifications
        '
        Me.chbNotifications.Location = New System.Drawing.Point(140, 82)
        Me.chbNotifications.Name = "chbNotifications"
        Me.chbNotifications.Size = New System.Drawing.Size(104, 24)
        Me.chbNotifications.TabIndex = 3
        Me.chbNotifications.Text = "Notification(s)"
        Me.chbNotifications.Visible = False
        '
        'chbReports
        '
        Me.chbReports.Location = New System.Drawing.Point(16, 82)
        Me.chbReports.Name = "chbReports"
        Me.chbReports.Size = New System.Drawing.Size(104, 24)
        Me.chbReports.TabIndex = 5
        Me.chbReports.Text = "Report(s)"
        '
        'chbACCs
        '
        Me.chbACCs.Location = New System.Drawing.Point(16, 26)
        Me.chbACCs.Name = "chbACCs"
        Me.chbACCs.Size = New System.Drawing.Size(208, 24)
        Me.chbACCs.TabIndex = 1
        Me.chbACCs.Text = "Annual Compliance Certification(s)"
        '
        'chbAllWork
        '
        Me.chbAllWork.Checked = True
        Me.chbAllWork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbAllWork.Location = New System.Drawing.Point(16, 8)
        Me.chbAllWork.Name = "chbAllWork"
        Me.chbAllWork.Size = New System.Drawing.Size(104, 24)
        Me.chbAllWork.TabIndex = 0
        Me.chbAllWork.Text = "All"
        '
        'SSCPEnforcementChecklist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(376, 681)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.txtEnforcementNumber)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.TBEnforcement)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPEnforcementChecklist"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Enforcement Linking tool"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgrComplianceEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBFilterOptions.ResumeLayout(False)
        Me.GBFilterOptions.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub SSCPEnforcementChecklist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            LoadHeader()

            FormatDataGridForWorkEnTry()
            FilterWork()

            Dim TempWidth As String
            TempWidth = GBFilterOptions.Size.Width
            GBFilterOptions.Size = New System.Drawing.Size(TempWidth, 0)

            DTPViolationDate.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

#Region "Page Load"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = UserName
            panel3.Text = OracleDate

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadHeader()
        Dim ZipCode As String = " "
        Dim EnforcementNumber As String = "N/A"
        Dim TrackingNumber As String = "N/A"
        Dim Staff As String = ""

        Try

            SQL = "Select strFacilityName, strFacilityStreet1, " & _
                 "strFacilityCity, strCountyName, strFacilityState, strFacilityZipCode, " & _
                 "strClass, strAIRProgramCodes " & _
                 "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".LookUpCountyInformation, " & _
                 "" & DBNameSpace & ".APBHeaderData " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
                 "and strCountyCode = '" & Mid(txtAIRSNumber.Text, 1, 3) & "' " & _
                 "and " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".APBHeaderData.strairsnumber"

            cmd = New OracleCommand(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist Then
                ZipCode = Mid(dr.Item("strFacilityZipCode"), 1, 5)
                If Mid(dr.Item("strFacilityZipCode"), 6) <> "" Then
                    ZipCode = ZipCode & "-" & Mid(dr.Item("strFacilityZipCode"), 6)
                End If

                txtFacilityInformation.Text = "AIRS # - " & txtAIRSNumber.Text & vbCrLf & _
                dr.Item("strFacilityName") & vbCrLf & _
                dr.Item("strFacilityStreet1") & vbCrLf & _
                dr.Item("StrFacilityCity") & ", " & dr.Item("strFacilityState") & " " & ZipCode & vbCrLf & _
                "County - " & dr.Item("strCountyName") & vbCrLf & _
                "Classification - " & dr.Item("strClass") & vbCrLf & _
                "Air Program Code(s) - " & vbCrLf
                AddAirProgramCodes(dr.Item("strAirProgramCodes"))
            End If

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            If txtEnforcementNumber.Text = "" Then
                If txtTrackingNumber.Text <> "" Then
                    SQL = "Select strEnforcementNumber " & _
                    "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                    "where strTrackingNumber= '" & txtTrackingNumber.Text & "'"

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("strEnforcementNumber")) Then
                            EnforcementNumber = "N/A"
                        Else
                            EnforcementNumber = dr.Item("strEnforcementNumber")
                        End If
                    Else
                        EnforcementNumber = "N/A"
                    End If
                    dr.Close()
                    If Conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                Else
                    EnforcementNumber = "N/A"
                End If
            Else
                EnforcementNumber = txtEnforcementNumber.Text
            End If

            If txtTrackingNumber.Text = "" Then
                If txtEnforcementNumber.Text <> "" Then
                    SQL = "Select strTrackingNumber " & _
                                 "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                                 "where strEnforcementNumber= '" & txtEnforcementNumber.Text & "'"
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("strTrackingNumber")) Then
                            TrackingNumber = "N/A"
                        Else
                            TrackingNumber = dr.Item("strTrackingNumber")
                        End If
                    Else
                        TrackingNumber = "N/A"
                    End If
                    dr.Close()
                    If Conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                Else
                    TrackingNumber = "N/A"
                End If
            Else
                TrackingNumber = txtTrackingNumber.Text
            End If

            If TrackingNumber <> "N/A" Then
                txtFacilityInformation.Text = txtFacilityInformation.Text & vbCrLf & "Tracking # - " & TrackingNumber
                txtTrackingNumber.Text = TrackingNumber
            End If

            If EnforcementNumber <> "N/A" Then
                txtFacilityInformation.Text = txtFacilityInformation.Text & vbCrLf & "Enforcement # - " & EnforcementNumber
                txtEnforcementNumber.Text = EnforcementNumber
            End If

            If EnforcementNumber <> "N/A" Then
                SQL = "Select strFirstName, strLastName " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCP_AuditedEnforcement.numStaffResponsible " & _
                "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = '" & EnforcementNumber & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    Staff = dr.Item("strFirstName") & " " & dr.Item("strLastName")
                Else
                    Staff = ""
                End If
            End If

            If Staff = "" Then
                If TrackingNumber <> "N/A" Then
                    SQL = "Select strFirstName, strLastName " & _
                    "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCPItemMaster " & _
                    "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = '" & TrackingNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        Staff = dr.Item("strFirstName") & " " & dr.Item("strLastName")
                    Else
                        Staff = ""
                    End If
                End If
            End If

            If Staff = "" Then
                Staff = UserName
            End If

            txtFacilityInformation.Text = txtFacilityInformation.Text & vbCrLf & "Staff Responsible - " & Staff

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Dim AirList As String = ""

        Try

            If Mid(AirProgramCode, 1, 1) = 1 Then
                AirList = vbTab & "0 - SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 2, 1) = 1 Then
                AirList = AirList & vbTab & "1 - Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 3, 1) = 1 Then
                AirList = AirList & vbTab & "3 - Non-Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 4, 1) = 1 Then
                AirList = AirList & vbTab & "4 - CFC Tracking" & vbCrLf
            End If
            If Mid(AirProgramCode, 5, 1) = 1 Then
                AirList = AirList & vbTab & "6 - PSD" & vbCrLf
            End If
            If Mid(AirProgramCode, 6, 1) = 1 Then
                AirList = AirList & vbTab & "7 - NSR" & vbCrLf
            End If
            If Mid(AirProgramCode, 7, 1) = 1 Then
                AirList = AirList & vbTab & "8 - NESHAP" & vbCrLf
            End If
            If Mid(AirProgramCode, 8, 1) = 1 Then
                AirList = AirList & vbTab & "9 - NSPS" & vbCrLf
            End If
            If Mid(AirProgramCode, 9, 1) = 1 Then
                AirList = AirList & vbTab & "F - FESOP" & vbCrLf
            End If
            If Mid(AirProgramCode, 10, 1) = 1 Then
                AirList = AirList & vbTab & "A - Acid Precipitation" & vbCrLf
            End If
            If Mid(AirProgramCode, 11, 1) = 1 Then
                AirList = AirList & vbTab & "I - Native American" & vbCrLf
            End If
            If Mid(AirProgramCode, 12, 1) = 1 Then
                AirList = AirList & vbTab & "M - MACT" & vbCrLf
            End If
            If Mid(AirProgramCode, 13, 1) = 1 Then
                AirList = AirList & vbTab & "V - Title V Permit" & vbCrLf
            End If
            If AirList = "" Then
                AirList = vbTab & "No Air Program Codes available" & vbCrLf
            End If
            AirList = Mid(AirList, 1, (Len(AirList) - 2))

            txtFacilityInformation.Text = txtFacilityInformation.Text & AirList

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FormatDataGridForWorkEnTry()

        'Formatting our DataGrid
        Dim objGrid As New DataGridTableStyle
        Dim objtextcol As New DataGridTextBoxColumn

        Try

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "tblWorkEntry"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            'objtextcol = New DataGridTextBoxColumn
            'objtextcol.MappingName = "AIRSNumber"
            'objtextcol.HeaderText = "AIRS Number"
            'objtextcol.Width = 90
            'objGrid.GridColumnStyles.Add(objtextcol)

            'objtextcol = New DataGridTextBoxColumn
            'objtextcol.MappingName = "strFacilityName"
            'objtextcol.HeaderText = "Facility Name"
            'objtextcol.Width = 200
            'objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strActivityName"
            objtextcol.HeaderText = "Compliance Event"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received by GEPD"
            objtextcol.Width = 130
            objtextcol.Format = "yyyy-MM-dd"
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Staff"
            objtextcol.HeaderText = "Staff Member"
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrComplianceEvents.TableStyles.Clear()
            dgrComplianceEvents.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrComplianceEvents.CaptionText = "Work Currently Entered"
            dgrComplianceEvents.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub


#End Region
#Region "Subs and Functions"
    Sub FilterWork()
        Dim SQLLine As String = ""
        Dim SQLCount As Integer = 0
        Dim SQLUnit As String = ""

        SQL = "Select substr(" & DBNameSpace & ".SSCPItemMaster.strAIrsnumber, 5) as AIRSNumber, strfacilityName, " & _
        "strActivityName, " & _
        "to_char(datReceivedDate, 'yyyy-MM-dd') as ReceivedDate, " & _
        "strTrackingNumber, (strLastName|| ', ' ||strFirstName) as Staff " & _
        "from " & DBNameSpace & ".SSCPItemMaster, " & _
        "" & DBNameSpace & ".LookUPComplianceActivities, " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles " & _
        "where " & _
        "" & DBNameSpace & ".SSCPItemMaster.strEventType = " & DBNameSpace & ".LookUPComplianceActivities.strActivityType " & _
        "and " & DBNameSpace & ".SSCPItemMaster.strairsnumber = " & DBNameSpace & ".APBFacilityInformation.strairsnumber " & _
        "and " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff " & _
        "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
        "and " & DBNameSpace & ".SSCPItemMaster.strEventType <> '05' "

        If chbWorkType.Checked = True Then
            If chbAllWork.Checked <> True Then
                SQLCount = 0
                If chbACCs.Checked = True Then
                    SQLLine = SQLLine & "" & DBNameSpace & ".SSCPItemMaster.strEventType = '04' "
                    SQLCount += 1
                End If
                If chbInspections.Checked = True Then
                    If SQLCount <> 0 Then
                        SQLLine = SQLLine & "OR " & DBNameSpace & ".SSCPItemMaster.strEventType = '02' "
                    Else
                        SQLLine = SQLLine & "" & DBNameSpace & ".SSCPItemMaster.strEventType = '02' "
                    End If
                    SQLCount += 1
                End If
                'If chbNotifications.Checked = True Then
                '    If SQLCount <> 0 Then
                '        SQLLine = SQLLine & "OR " & DBNameSpace & ".SSCPItemMaster.strEventType = '05' "
                '    Else
                '        SQLLine = SQLLine & "" & DBNameSpace & ".SSCPItemMaster.strEventType = '05' "
                '    End If
                '    SQLCount += 1
                'End If
                If chbPerformanceTests.Checked = True Then
                    If SQLCount <> 0 Then
                        SQLLine = SQLLine & "OR " & DBNameSpace & ".SSCPItemMaster.strEventType = '03' "
                    Else
                        SQLLine = SQLLine & "" & DBNameSpace & ".SSCPItemMaster.strEventType = '03' "
                    End If
                    SQLCount += 1
                End If
                If chbReports.Checked = True Then
                    If SQLCount <> 0 Then
                        SQLLine = SQLLine & "OR " & DBNameSpace & ".SSCPItemMaster.strEventType = '01' "
                    Else
                        SQLLine = SQLLine & "" & DBNameSpace & ".SSCPItemMaster.strEventType = '01' "
                    End If
                    SQLCount += 1
                End If
                If SQLLine <> "" Then
                    SQLLine = "And (" & SQLLine & ") "
                End If
            Else
                'SQLLine = SQLLine
            End If
        End If

        SQLCount = 0

        If SQLCount <> 0 Then
            SQLLine = SQLLine & "And (" & SQLUnit & ") "
        End If

        If chbDateReceived.Checked = True Then
            If chbAllDates.Checked = True Then
            Else
                SQLLine = SQLLine & "and " & DBNameSpace & ".SSCPItemMaster.datReceivedDate between " & _
                "'" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
            End If
        End If

        If SQLLine <> "" Then
            SQL = SQL & SQLLine & "Order by datReceivedDate DESC, strTrackingNumber DESC "
        End If

        cmd = New OracleCommand(SQL, Conn)

        dsWorkEnTry = New DataSet

        daWorkEnTry = New OracleDataAdapter(cmd)
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        daWorkEnTry.Fill(dsWorkEnTry, "tblWorkEnTry")
        dgrComplianceEvents.DataSource = dsWorkEnTry
        dgrComplianceEvents.DataMember = "tblWorkEnTry"

        If Conn.State = ConnectionState.Open Then
            'conn.close()
        End If
        txtWorkCount.Text = dsWorkEnTry.Tables(0).Rows.Count

    End Sub
    Private Sub MoveOn()
        If txtTrackingNumber.Text <> "" Then

            SSCPREports = Nothing
            If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
            SSCPREports.txtTrackingNumber.Text = txtTrackingNumber.Text
            SSCPREports.txtOrigin.Text = "Enforcement Checklist"
            SSCPREports.Show()
            'SSCPREports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        End If

    End Sub
#End Region
#Region "Declarations"
    Private Sub btnViewLON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewLON.Click
        Dim TempHeight As String
        Dim TempWidth As String
        Try

            TempHeight = GBFilterOptions.Size.Height
            TempWidth = GBFilterOptions.Size.Width

            If TempHeight = 0 Then
                GBFilterOptions.Size = New System.Drawing.Size(TempWidth, 232)
            Else
                GBFilterOptions.Size = New System.Drawing.Size(TempWidth, 0)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub btnRunFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunFilter.Click
        Try

            FilterWork()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub chbAllDates_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbAllDates.CheckedChanged
        Try

            If chbAllDates.Checked = True Then
                DTPStartDate.Enabled = False
                DTPEndDate.Enabled = False
            Else
                DTPStartDate.Enabled = True
                DTPEndDate.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrComplianceEvents_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrComplianceEvents.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrComplianceEvents.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrComplianceEvents(hti.Row, 0)) Then
                Else
                    'If IsDBNull(dgrComplianceEvents(hti.Row, 1)) Then
                    'Else
                    '    If IsDBNull(dgrComplianceEvents(hti.Row, 2)) Then
                    '    Else
                    '        If IsDBNull(dgrComplianceEvents(hti.Row, 3)) Then
                    '        Else
                    '            If IsDBNull(dgrComplianceEvents(hti.Row, 4)) Then
                    '            Else
                    '                If IsDBNull(dgrComplianceEvents(hti.Row, 5)) Then
                    '                Else
                    txtTrackingNumber.Text = dgrComplianceEvents(hti.Row, 0)
                    '    End If
                    'End If
                    '                End If
                    '            End If
                    '        End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnOpenEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenEvent.Click
        Try

            MoveOn()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnLinkEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkEvent.Click
        Try

            If SSCP_Enforcement Is Nothing Then
            Else
                SSCP_Enforcement.txtTrackingNumber.Text = txtTrackingNumber.Text
                SSCP_Enforcement.txtDiscoveryEventNumber.Text = txtTrackingNumber.Text
                SSCP_Enforcement.SaveEnforcement()
                SSCP_Enforcement.LoadEnforcement()
            End If

            Dim Result As DialogResult

            Result = MessageBox.Show("Do you want to close the Enforcement Linking Tool?", _
              "Enforcement Linking Tool", MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case Windows.Forms.DialogResult.Yes
                    EnforcementChecklist = Nothing
                    Me.Close()
                Case Windows.Forms.DialogResult.No

                Case Windows.Forms.DialogResult.Cancel

                Case Else

            End Select



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnCreateNewEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateNewEvent.Click
        Try

            SSCP_Work = Nothing
            panel1.Text = "Loading Page."
            If SSCP_Work Is Nothing Then SSCP_Work = New SSCPComplianceLog
            SSCP_Work.txtAIRSNumber.Text = txtAIRSNumber.Text
            SSCP_Work.Show()
            'SSCP_Work.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub SSCPEnforcementChecklist_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        EnforcementChecklist = Nothing
    End Sub
#End Region


   
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try
            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
        End Try

    End Sub
End Class
