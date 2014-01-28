Imports Oracle.DataAccess.Client


Public Class ISMPFacilityInfo
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim SQL, SQL2 As String
    ' Dim cmd, cmd2 As OracleCommand
    ' Dim dr, dr2 As OracleDataReader
    ' Dim recExist As Boolean
    Dim dsPollutant As DataSet
    Dim daPollutant As OracleDataAdapter
    Dim dsTestingFirms As DataSet
    Dim daTestingFirms As OracleDataAdapter


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
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GBFacilityData As System.Windows.Forms.GroupBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents DTPDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents DTPTestDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPTestDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboPollutant As System.Windows.Forms.ComboBox
    Friend WithEvents txtEmissionSource As System.Windows.Forms.TextBox
    Friend WithEvents cboTestingFirms As System.Windows.Forms.ComboBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TBTestReportEntry As System.Windows.Forms.ToolBar
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAddTestingFirm As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAddPollutant As System.Windows.Forms.MenuItem
    Friend WithEvents mmiRefreshLists As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAddMemo As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents tbbAddMemo As System.Windows.Forms.ToolBarButton
    Friend WithEvents chbOverright As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPFacilityInfo))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mmiAddMemo = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiAddTestingFirm = New System.Windows.Forms.MenuItem
        Me.mmiAddPollutant = New System.Windows.Forms.MenuItem
        Me.mmiRefreshLists = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.TBTestReportEntry = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbAddMemo = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GBFacilityData = New System.Windows.Forms.GroupBox
        Me.chbOverright = New System.Windows.Forms.CheckBox
        Me.DTPDateReceived = New System.Windows.Forms.DateTimePicker
        Me.Label21 = New System.Windows.Forms.Label
        Me.DTPTestDateEnd = New System.Windows.Forms.DateTimePicker
        Me.DTPTestDateStart = New System.Windows.Forms.DateTimePicker
        Me.cboPollutant = New System.Windows.Forms.ComboBox
        Me.txtEmissionSource = New System.Windows.Forms.TextBox
        Me.cboTestingFirms = New System.Windows.Forms.ComboBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GBFacilityData.SuspendLayout()
        Me.Panel4.SuspendLayout()
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
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiBack})
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
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem11, Me.mmiClear})
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
        'MenuItem11
        '
        Me.MenuItem11.Index = 3
        Me.MenuItem11.Text = "-"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 4
        Me.mmiClear.Text = "Clear"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiAddMemo, Me.MenuItem3, Me.mmiAddTestingFirm, Me.mmiAddPollutant, Me.mmiRefreshLists})
        Me.MenuItem4.Text = "Tools"
        '
        'mmiAddMemo
        '
        Me.mmiAddMemo.Index = 0
        Me.mmiAddMemo.Text = "Add Memo"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.Text = "-"
        '
        'mmiAddTestingFirm
        '
        Me.mmiAddTestingFirm.Index = 2
        Me.mmiAddTestingFirm.Text = "Add Testing Firm"
        '
        'mmiAddPollutant
        '
        Me.mmiAddPollutant.Index = 3
        Me.mmiAddPollutant.Text = "Add Pollutant"
        '
        'mmiRefreshLists
        '
        Me.mmiRefreshLists.Index = 4
        Me.mmiRefreshLists.Text = "Refresh List(s)"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 3
        Me.mmiHelp.Text = "Help"
        '
        'TBTestReportEntry
        '
        Me.TBTestReportEntry.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbAddMemo, Me.tbbClear, Me.tbbBack})
        Me.TBTestReportEntry.DropDownArrows = True
        Me.TBTestReportEntry.ImageList = Me.Image_List_All
        Me.TBTestReportEntry.Location = New System.Drawing.Point(0, 0)
        Me.TBTestReportEntry.Name = "TBTestReportEntry"
        Me.TBTestReportEntry.ShowToolTips = True
        Me.TBTestReportEntry.Size = New System.Drawing.Size(792, 28)
        Me.TBTestReportEntry.TabIndex = 140
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
        Me.tbbSave.ToolTipText = "Save"
        '
        'tbbAddMemo
        '
        Me.tbbAddMemo.ImageIndex = 0
        Me.tbbAddMemo.Name = "tbbAddMemo"
        Me.tbbAddMemo.ToolTipText = "Add Memo"
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        Me.tbbClear.ToolTipText = "Clear"
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        Me.tbbBack.ToolTipText = "Back"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 141
        Me.Label1.Text = "Facility Name: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 142
        Me.Label2.Text = "AIRS Number:"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(104, 16)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(184, 20)
        Me.txtFacilityName.TabIndex = 143
        Me.ToolTip1.SetToolTip(Me.txtFacilityName, "Facility Name")
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(104, 40)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(72, 20)
        Me.txtAIRSNumber.TabIndex = 144
        Me.ToolTip1.SetToolTip(Me.txtAIRSNumber, "AIRS Number")
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(504, 16)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.ReadOnly = True
        Me.txtReferenceNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtReferenceNumber.TabIndex = 146
        Me.ToolTip1.SetToolTip(Me.txtReferenceNumber, "Automatically Generated unless manually entered")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(400, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 145
        Me.Label3.Text = "Reference Number:"
        '
        'GBFacilityData
        '
        Me.GBFacilityData.Controls.Add(Me.chbOverright)
        Me.GBFacilityData.Controls.Add(Me.txtAIRSNumber)
        Me.GBFacilityData.Controls.Add(Me.Label1)
        Me.GBFacilityData.Controls.Add(Me.txtFacilityName)
        Me.GBFacilityData.Controls.Add(Me.txtReferenceNumber)
        Me.GBFacilityData.Controls.Add(Me.Label3)
        Me.GBFacilityData.Controls.Add(Me.Label2)
        Me.GBFacilityData.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBFacilityData.Location = New System.Drawing.Point(0, 28)
        Me.GBFacilityData.Name = "GBFacilityData"
        Me.GBFacilityData.Size = New System.Drawing.Size(792, 68)
        Me.GBFacilityData.TabIndex = 147
        Me.GBFacilityData.TabStop = False
        '
        'chbOverright
        '
        Me.chbOverright.Location = New System.Drawing.Point(504, 40)
        Me.chbOverright.Name = "chbOverright"
        Me.chbOverright.Size = New System.Drawing.Size(200, 16)
        Me.chbOverright.TabIndex = 147
        Me.chbOverright.Text = "Manually Enter Reference Number"
        Me.ToolTip1.SetToolTip(Me.chbOverright, "Warning if Reference Number is currently in use the data will be overwritten.")
        '
        'DTPDateReceived
        '
        Me.DTPDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReceived.Location = New System.Drawing.Point(104, 8)
        Me.DTPDateReceived.Name = "DTPDateReceived"
        Me.DTPDateReceived.Size = New System.Drawing.Size(100, 20)
        Me.DTPDateReceived.TabIndex = 234
        Me.ToolTip1.SetToolTip(Me.DTPDateReceived, "Date Test Report received at APB")
        Me.DTPDateReceived.Value = New Date(2005, 3, 24, 0, 0, 0, 0)
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 10)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(82, 13)
        Me.Label21.TabIndex = 244
        Me.Label21.Text = "Date Recieved:"
        '
        'DTPTestDateEnd
        '
        Me.DTPTestDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateEnd.Location = New System.Drawing.Point(216, 56)
        Me.DTPTestDateEnd.Name = "DTPTestDateEnd"
        Me.DTPTestDateEnd.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestDateEnd.TabIndex = 239
        Me.ToolTip1.SetToolTip(Me.DTPTestDateEnd, "End Date of test conducted")
        Me.DTPTestDateEnd.Value = New Date(2005, 2, 25, 0, 0, 0, 0)
        '
        'DTPTestDateStart
        '
        Me.DTPTestDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateStart.Location = New System.Drawing.Point(104, 56)
        Me.DTPTestDateStart.Name = "DTPTestDateStart"
        Me.DTPTestDateStart.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestDateStart.TabIndex = 238
        Me.ToolTip1.SetToolTip(Me.DTPTestDateStart, "Start Date of test conducted ")
        Me.DTPTestDateStart.Value = New Date(2005, 2, 25, 0, 0, 0, 0)
        '
        'cboPollutant
        '
        Me.cboPollutant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollutant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollutant.Location = New System.Drawing.Point(488, 32)
        Me.cboPollutant.Name = "cboPollutant"
        Me.cboPollutant.Size = New System.Drawing.Size(264, 21)
        Me.cboPollutant.TabIndex = 237
        Me.ToolTip1.SetToolTip(Me.cboPollutant, "Pollutant monitored in test.")
        '
        'txtEmissionSource
        '
        Me.txtEmissionSource.Location = New System.Drawing.Point(104, 32)
        Me.txtEmissionSource.MaxLength = 100
        Me.txtEmissionSource.Name = "txtEmissionSource"
        Me.txtEmissionSource.Size = New System.Drawing.Size(264, 20)
        Me.txtEmissionSource.TabIndex = 235
        Me.ToolTip1.SetToolTip(Me.txtEmissionSource, "Emission Source tested")
        '
        'cboTestingFirms
        '
        Me.cboTestingFirms.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTestingFirms.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTestingFirms.Location = New System.Drawing.Point(488, 8)
        Me.cboTestingFirms.Name = "cboTestingFirms"
        Me.cboTestingFirms.Size = New System.Drawing.Size(264, 21)
        Me.cboTestingFirms.TabIndex = 236
        Me.ToolTip1.SetToolTip(Me.cboTestingFirms, "Testing Firm that performed test.")
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(33, 58)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(62, 13)
        Me.Label51.TabIndex = 243
        Me.Label51.Text = "Test Dates:"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(418, 34)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(51, 13)
        Me.Label47.TabIndex = 242
        Me.Label47.Text = "Pollutant:"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(400, 10)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(67, 13)
        Me.Label46.TabIndex = 241
        Me.Label46.Text = "Testing Firm:"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(14, 34)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(80, 13)
        Me.Label45.TabIndex = 240
        Me.Label45.Text = "Source Tested:"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.DTPTestDateStart)
        Me.Panel4.Controls.Add(Me.DTPTestDateEnd)
        Me.Panel4.Controls.Add(Me.Label21)
        Me.Panel4.Controls.Add(Me.DTPDateReceived)
        Me.Panel4.Controls.Add(Me.Label45)
        Me.Panel4.Controls.Add(Me.Label46)
        Me.Panel4.Controls.Add(Me.Label47)
        Me.Panel4.Controls.Add(Me.Label51)
        Me.Panel4.Controls.Add(Me.cboTestingFirms)
        Me.Panel4.Controls.Add(Me.txtEmissionSource)
        Me.Panel4.Controls.Add(Me.cboPollutant)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 96)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(792, 127)
        Me.Panel4.TabIndex = 245
        '
        'ISMPFacilityInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 223)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GBFacilityData)
        Me.Controls.Add(Me.TBTestReportEntry)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPFacilityInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Test Report Information"
        Me.GBFacilityData.ResumeLayout(False)
        Me.GBFacilityData.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub ISMPFacilityInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            FillFacilityDataSet()
            FillComboBoxes()

            DTPDateReceived.Value = Date.Today
            DTPTestDateStart.Value = Date.Today
            DTPTestDateEnd.Value = Date.Today

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
#Region "Page Load Functions And Subs"
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
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub FillFacilityDataSet()
        Try

            SQL = "Select strPollutantCode, strPollutantDescription from " & DBNameSpace & ".LookUPPollutants order by strPollutantDescription"
            SQL2 = "Select strTestingFirmKey, strTestingFirm from " & DBNameSpace & ".LookUPTestingFirms order by strTestingFirm"

            dsPollutant = New DataSet
            dsTestingFirms = New DataSet

            daPollutant = New OracleDataAdapter(SQL, CurrentConnection)
            daTestingFirms = New OracleDataAdapter(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPollutant.Fill(dsPollutant, "Pollutant")
            daTestingFirms.Fill(dsTestingFirms, "TestingFirms")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub FillComboBoxes()
        Dim dtPollutant As New DataTable
        Dim dtTestingFirms As DataTable

        Try

            dtPollutant = dsPollutant.Tables("Pollutant")
            dtTestingFirms = dsTestingFirms.Tables("TestingFirms")

            Dim drTestingFirms As DataRow()
            Dim drPollutant As DataRow()

            Dim row As DataRow

            drPollutant = dtPollutant.Select()
            For Each row In drPollutant
                cboPollutant.Items.Add(row("strPollutantDescription").ToString())
            Next

            drTestingFirms = dtTestingFirms.Select()
            For Each row In drTestingFirms
                cboTestingFirms.Items.Add(row("strTestingFirm").ToString())
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
#Region "Functions and Subs"
    Private Sub GetNextReferenceNumber()
        Dim RefNum As String = ""

        Try

            SQL = "Select strReferenceNumber from " & DBNameSpace & ".ISMPReferenceNumber"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader
            While dr.Read
                RefNum = dr.Item("strReferenceNumber")
            End While
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
            RefNum = CStr(CInt(RefNum) + 1)

            txtReferenceNumber.Text = RefNum

            SQL = "Update " & DBNameSpace & ".ISMPReferenceNumber set strReferenceNumber = '" & RefNum & "'"
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd.ExecuteReader()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Save()
        Try

            If txtAIRSNumber.Text.Length = 8 Then
                Paneltemp1 = panel1.Text
                panel1.Text = "Getting New Reference Number.."
                If chbOverright.Checked = False Then
                    GetNextReferenceNumber()
                End If
                panel1.Text = "Saving New Test Report."

                Dim RecordStatus As String = "False"

                If cboPollutant.SelectedIndex <> -1 Then
                    panel1.Text = "Saving New Test Report.."
                    If cboTestingFirms.SelectedIndex <> -1 Then
                        panel1.Text = "Saving New Test Report..."
                        If txtEmissionSource.Text = "" Then
                            txtEmissionSource.Text = "Unknown"
                        End If
                        panel1.Text = "Saving New Test Report...."

                        If txtReferenceNumber.Text <> "" Then
                            SQL = "Select strReferenceNumber " & _
                            "from " & DBNameSpace & ".ISMPMaster " & _
                            "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"

                            Dim cmd As New OracleCommand(SQL, CurrentConnection)

                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            Dim dr As OracleDataReader = cmd.ExecuteReader

                            Dim recExist As Boolean = dr.Read
                            If CurrentConnection.State = ConnectionState.Open Then
                                'conn.close()
                            End If
                            panel1.Text = "Saving New Test Report....."

                            If recExist = True Then
                                SQL = "Update " & DBNameSpace & ".ISMPMaster set " & _
                                "strAIRSNumber = '0413" & txtAIRSNumber.Text & "'" & _
                                "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"

                                SQL2 = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                                "strPollutant = " & _
                                "(select strPollutantcode from " & DBNameSpace & ".LookUPPollutants where strPollutantdescription= '" & cboPollutant.Text & "'), " & _
                                "strEmissionSource = '" & txtEmissionSource.Text & "', " & _
                                "strTestingFirm = " & _
                                "(Select strTestingFirmKey from " & DBNameSpace & ".LookUPTestingFirms where strTestingFirm = '" & cboTestingFirms.Text & "'), " & _
                                "datTestDateStart = '" & DTPTestDateStart.Text & "', " & _
                                "datTestDateEnd = '" & DTPTestDateEnd.Text & "', " & _
                                "datReceivedDate = '" & DTPDateReceived.Text & "', " & _
                                "datCompleteDate = '04-Jul-1776', " & _
                                "strClosed = '" & RecordStatus & "', " & _
                                "strDelete = '' " & _
                                "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                            Else
                                SQL = "Insert into " & DBNameSpace & ".ISMPMaster values ('" & txtReferenceNumber.Text & "', " & _
                                "'0413" & txtAIRSNumber.Text & "', '" & UserGCode & "', " & _
                                "'" & OracleDate & "')"

                                ' SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
                                '"(strReferenceNumber, strPollutant, strEmissionSource, " & _
                                '"strReportType, strDocumentType, strApplicableRequirement, " & _
                                '"strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
                                '"strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
                                '"strComplianceManager, datTestDateStart, datTestDateEnd, " & _
                                '"datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
                                '"strDirector, strCommissioner, strProgramManager, " & _
                                '"strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
                                '"strControlEquipmentData, strDelete, numReviewingManager) " & _
                                '"values " & _
                                '"('" & txtReferenceNumber.Text & "', " & _
                                '"'" & cboPollutant.SelectedValue & "', " & _
                                '"'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
                                '"'004', " & _
                                '"'001', " & _
                                '"'Incomplete', " & _
                                '"'" & cboTestingFirms.SelectedValue & "', " & _
                                '"'0', '0', '0', " & _
                                '"'0', " & _
                                '"'04-Jul-1776', " & _
                                '"(SELECT " & _
                                '"CASE  " & _
                                '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL  " & _
                                '"       THEN strDistrictManager  " & _
                                '"WHEN strSSCPAssigningManager <> '1' AND strSSCPAssigningManager IS NOT NULL  " & _
                                '"       THEN strSSCPAssigningManager  " & _
                                '"ELSE '337' " & _
                                '"END ManagerResponsible  " & _
                                '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
                                '"" & DBNameSpace & ".SSCPDistrictResponsible, " & DBNameSpace & ".SSCPFacilityAssignment    " & _
                                '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
                                '"AND " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber = " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber (+) " & _
                                '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
                                '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & txtAIRSNumber.Text & "'), " & _
                                '"'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
                                '"'" & DTPDateReceived.Text & "', " & _
                                '"'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
                                '"(select strDirector from " & DBNameSpace & ".LookUpAPBManagement), " & _
                                '"(select strCommissioner from " & DBNameSpace & ".LookUpAPBManagement), " & _
                                '"(select strISMPProgramMang from " & DBNameSpace & ".LookUpAPBManagement), " & _
                                '"'01', '0', '" & UserGCode & "', '" & OracleDate & "', " & _
                                '"'N/A', '', '')"

                                '  SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
                                '"(strReferenceNumber, strPollutant, strEmissionSource, " & _
                                '"strReportType, strDocumentType, strApplicableRequirement, " & _
                                '"strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
                                '"strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
                                '"strComplianceManager, datTestDateStart, datTestDateEnd, " & _
                                '"datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
                                '"strDirector, strCommissioner, strProgramManager, " & _
                                '"strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
                                '"strControlEquipmentData, strDelete, numReviewingManager) " & _
                                '"values " & _
                                '"('" & txtReferenceNumber.Text & "', " & _
                                '"'" & cboPollutant.SelectedValue & "', " & _
                                '"'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
                                '"'004', " & _
                                '"'001', " & _
                                '"'Incomplete', " & _
                                '"'" & cboTestingFirms.SelectedValue & "', " & _
                                '"'0', '0', '0', " & _
                                '"'0', " & _
                                '"'04-Jul-1776', " & _
                                '"(SELECT " & _
                                '"CASE  " & _
                                '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL  " & _
                                '"       THEN strDistrictManager  " & _
                                '"WHEN strSSCPAssigningManager <> '1' AND strSSCPAssigningManager IS NOT NULL  " & _
                                '"       THEN strSSCPAssigningManager  " & _
                                '"ELSE '337' " & _
                                '"END ManagerResponsible  " & _
                                '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
                                '"" & DBNameSpace & ".SSCPDistrictResponsible, " & DBNameSpace & ".SSCPFacilityAssignment    " & _
                                '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
                                '"AND " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber = " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber (+) " & _
                                '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
                                '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & txtAIRSNumber.Text & "'), " & _
                                '"'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
                                '"'" & DTPDateReceived.Text & "', " & _
                                '"'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
                                '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                                '"where strKey = '1' and strCurrentContact = '1'), " & _
                                ' "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                                '"where strKey = '2' and strCurrentContact = '1'), " & _
                                ' "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                                '"where strKey = '5' and strCurrentContact = '1'), " & _
                                '"'01', '0', '" & UserGCode & "', '" & OracleDate & "', " & _
                                '"'N/A', '', '')"

                                SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
           "(strReferenceNumber, strPollutant, strEmissionSource, " & _
           "strReportType, strDocumentType, strApplicableRequirement, " & _
           "strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
           "strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
           "strComplianceManager, datTestDateStart, datTestDateEnd, " & _
           "datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
           "strDirector, strCommissioner, strProgramManager, " & _
           "strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
           "strControlEquipmentData, strDelete, numReviewingManager) " & _
           "values " & _
           "('" & txtReferenceNumber.Text & "', " & _
           "'" & cboPollutant.SelectedValue & "', " & _
           "'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
           "'004', " & _
           "'001', " & _
           "'Incomplete', " & _
           "'" & cboTestingFirms.SelectedValue & "', " & _
           "'0', '0', '0', " & _
           "'0', " & _
           "'04-Jul-1776', " & _
           "(SELECT " & _
           "CASE  " & _
           "WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL  " & _
           "       THEN strDistrictManager  " & _
           "WHEN to_char(TABLE1.STRASSIGNINGMANAGER) <> '1' AND to_char(TABLE1.STRASSIGNINGMANAGER) IS NOT NULL  " & _
           "       THEN to_char(TABLE1.STRASSIGNINGMANAGER)  " & _
           "ELSE '337' " & _
           "END ManagerResponsible  " & _
           "from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
           "" & DBNameSpace & ".SSCPDistrictResponsible,     " & _
           "(Select " & _
           "max(intYear), strAssigningManager, strAIRSNumber " & _
           "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
           "group by strAssigningManager, strAIRSNumber) Table1 " & _
           "WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber (+) " & _
           "AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & txtAIRSNumber.Text & "'), " & _
           "'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
           "'" & DTPDateReceived.Text & "', " & _
           "'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
           "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
           "where strKey = '1' and strCurrentContact = '1' ), " & _
           "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
           "where strKey = '2' and strCurrentContact = '1' ), " & _
           "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
           "where strKey = '5' and strCurrentContact = '1' ), " & _
           "'01', " & _
           "(SELECT " & _
           "CASE " & _
           "WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL " & _
           " THEN '9' " & _
           "ELSE '0'   " & _
           "END ManagerResponsible " & _
           "from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
           "" & DBNameSpace & ".SSCPDistrictResponsible,     " & _
           "(Select " & _
           "max(intYear), strAssigningManager, strAIRSNumber " & _
           "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
           "group by strAssigningManager, strAIRSNumber) Table1 " & _
           "WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber (+) " & _
           "AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & txtAIRSNumber.Text & "'), " & _
           "'" & UserGCode & "', '" & OracleDate & "', " & _
           "'N/A', '', '')"

                            End If
                            panel1.Text = "Saving New Test Report......"

                            Try

                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                If CurrentConnection.State = ConnectionState.Open Then
                                    'conn.close()
                                End If

                            Catch ex As Exception
                                ErrorReport(ex, "ISMPFacilityInfo.Save(Sub)")
                            Finally
                                If CurrentConnection.State = ConnectionState.Open Then
                                    'conn.close()
                                End If
                            End Try
                            ' 

                            panel1.Text = "Saving New Test Report......."

                            Try

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                If CurrentConnection.State = ConnectionState.Open Then
                                    'conn.close()
                                End If

                            Catch ex As Exception
                                ErrorReport(ex, "ISMPFacilityInfo.Save(Sub2)")
                            Finally
                                If CurrentConnection.State = ConnectionState.Open Then
                                    'conn.close()
                                End If
                            End Try

                            panel1.Text = Paneltemp1
                            MsgBox("Done", MsgBoxStyle.Information, "ISMP Facility Information")
                        Else
                            MsgBox("You must Provide a Reference Number", MsgBoxStyle.Exclamation, "ISMP Facility Information")
                        End If
                    Else
                        MsgBox("The Testing Firm does not match any of the provided Testing Firms." _
                                  & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility Information")
                    End If
                Else
                    MsgBox("The Pollutant does not match any of the provided pollutants." _
                                      & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility Information")
                End If
            Else
                MsgBox("The seems to be a problem with the AIRS Number." _
                           & vbCr & "Please close the form and try again.", MsgBoxStyle.Information, "ISMP Facility Information")
            End If
            panel1.Text = Paneltemp1

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Clear()
        Try

            DTPDateReceived.Value = Date.Today
            DTPTestDateStart.Value = Date.Today
            DTPTestDateEnd.Value = Date.Today

            txtEmissionSource.Clear()
            cboPollutant.Text = " "
            cboTestingFirms.Text = " "

            txtReferenceNumber.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try



    End Sub
    Sub Back()
        Try

            ISMPTestReportInfo = Nothing
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub OpenMemo()
        Try

            If txtReferenceNumber.Text <> "" Then
                ISMPMemoEdit = Nothing
                If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
                ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
                ISMPMemoEdit.Show()
                'ISMPMemoEdit.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region

    Private Sub ISMPFacilityInfo_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPTestReportInfo = Nothing
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBTestReportEnTry_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBTestReportEntry.ButtonClick
        Try

            Select Case TBTestReportEntry.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    OpenMemo()
                Case 2
                    Clear()
                Case 3
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub DTPTestDateStart_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPTestDateStart.ValueChanged
        Try

            If DTPTestDateStart.Value > DTPTestDateEnd.Value Then
                DTPTestDateEnd.Value = DTPTestDateStart.Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub DTPDateReceived_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPDateReceived.ValueChanged
        Try

            If DTPDateReceived.Value < DTPTestDateStart.Value Then
                DTPTestDateStart.Value = DTPDateReceived.Value
            End If
            If DTPDateReceived.Value < DTPTestDateEnd.Value Then
                DTPTestDateEnd.Value = DTPTestDateEnd.Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub DTPTestDateEnd_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPTestDateEnd.ValueChanged
        Try

            If DTPTestDateStart.Value > DTPTestDateEnd.Value Then
                DTPTestDateEnd.Value = DTPTestDateStart.Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Main Menu Items"
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiAddTestingFirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddTestingFirm.Click
        Try

            ISMPAddTestingFirm = Nothing
            If ISMPAddTestingFirm Is Nothing Then ISMPAddTestingFirm = New ISMPAddTestingFirms
            ISMPAddTestingFirm.Show()
            'ISMPAddTestingFirm.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiAddPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddPollutant.Click
        Try

            ISMPAddPollutant = Nothing
            If ISMPAddPollutant Is Nothing Then ISMPAddPollutant = New ISMPAddPollutants
            ISMPAddPollutant.Show()
            'ISMPAddPollutant.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiRefreshLists_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshLists.Click
        Try

            cboPollutant.Items.Clear()
            cboTestingFirms.Items.Clear()
            FillFacilityDataSet()
            FillComboBoxes()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
    Private Sub mmiAddMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddMemo.Click
        Try

            OpenMemo()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub chbOverright_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbOverright.CheckedChanged
        Try

            If chbOverright.Checked = True Then
                txtReferenceNumber.ReadOnly = False
            Else
                txtReferenceNumber.ReadOnly = True
                txtReferenceNumber.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
End Class
