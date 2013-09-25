Imports Oracle.DataAccess.Client


Public Class ISMPClosePrint
    Inherits DefaultForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean



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
    Friend WithEvents txtOrigin As System.Windows.Forms.TextBox
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents TBFCE As System.Windows.Forms.ToolBar
    Friend WithEvents TbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents GBFacilityData As System.Windows.Forms.GroupBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDaysInAPB As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents GBRecordStatus As System.Windows.Forms.GroupBox
    Friend WithEvents rdbCloseReport As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOpenReport As System.Windows.Forms.RadioButton
    Friend WithEvents DTPDateClosed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTestDateEnd As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTestDateStart As System.Windows.Forms.TextBox
    Friend WithEvents txtComplianceStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtSourceTested As System.Windows.Forms.TextBox
    Friend WithEvents txtPollutant As System.Windows.Forms.TextBox
    Friend WithEvents txtEngineer As System.Windows.Forms.TextBox
    Friend WithEvents txtUnitManager As System.Windows.Forms.TextBox
    Friend WithEvents txtReportType As System.Windows.Forms.TextBox
    Friend WithEvents txtTestReportType As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents llbEmissionLog As System.Windows.Forms.ListBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TbbPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtEmissionLog As System.Windows.Forms.TextBox
    Friend WithEvents txtTestLinksCount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPrintAFSForm As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPrintTestReport As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPClosePrint))
        Me.txtOrigin = New System.Windows.Forms.TextBox
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
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiPrintAFSForm = New System.Windows.Forms.MenuItem
        Me.mmiPrintTestReport = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.TBFCE = New System.Windows.Forms.ToolBar
        Me.TbbSave = New System.Windows.Forms.ToolBarButton
        Me.TbbPrint = New System.Windows.Forms.ToolBarButton
        Me.TbbBack = New System.Windows.Forms.ToolBarButton
        Me.GBFacilityData = New System.Windows.Forms.GroupBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDaysInAPB = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.GBRecordStatus = New System.Windows.Forms.GroupBox
        Me.rdbCloseReport = New System.Windows.Forms.RadioButton
        Me.rdbOpenReport = New System.Windows.Forms.RadioButton
        Me.DTPDateClosed = New System.Windows.Forms.DateTimePicker
        Me.Label17 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTestLinksCount = New System.Windows.Forms.TextBox
        Me.llbEmissionLog = New System.Windows.Forms.ListBox
        Me.txtEmissionLog = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtTestDateEnd = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtTestDateStart = New System.Windows.Forms.TextBox
        Me.txtComplianceStatus = New System.Windows.Forms.TextBox
        Me.txtSourceTested = New System.Windows.Forms.TextBox
        Me.txtPollutant = New System.Windows.Forms.TextBox
        Me.txtEngineer = New System.Windows.Forms.TextBox
        Me.txtUnitManager = New System.Windows.Forms.TextBox
        Me.txtReportType = New System.Windows.Forms.TextBox
        Me.txtTestReportType = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.GBFacilityData.SuspendLayout()
        Me.GBRecordStatus.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtOrigin
        '
        Me.txtOrigin.Location = New System.Drawing.Point(24, 464)
        Me.txtOrigin.Name = "txtOrigin"
        Me.txtOrigin.Size = New System.Drawing.Size(8, 20)
        Me.txtOrigin.TabIndex = 3
        Me.txtOrigin.Visible = False
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
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste})
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
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        Me.MenuItem4.Text = "Tools"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiPrintAFSForm, Me.mmiPrintTestReport})
        Me.MenuItem3.Text = "Print"
        '
        'mmiPrintAFSForm
        '
        Me.mmiPrintAFSForm.Index = 0
        Me.mmiPrintAFSForm.Text = "AFS Form"
        '
        'mmiPrintTestReport
        '
        Me.mmiPrintTestReport.Index = 1
        Me.mmiPrintTestReport.Text = "Test Report"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 3
        Me.mmiHelp.Text = "Help"
        '
        'TBFCE
        '
        Me.TBFCE.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbSave, Me.TbbPrint, Me.TbbBack})
        Me.TBFCE.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBFCE.DropDownArrows = True
        Me.TBFCE.ImageList = Me.Image_List_All
        Me.TBFCE.Location = New System.Drawing.Point(0, 0)
        Me.TBFCE.Name = "TBFCE"
        Me.TBFCE.ShowToolTips = True
        Me.TBFCE.Size = New System.Drawing.Size(792, 28)
        Me.TBFCE.TabIndex = 49
        '
        'TbbSave
        '
        Me.TbbSave.ImageIndex = 65
        Me.TbbSave.Name = "TbbSave"
        Me.TbbSave.ToolTipText = "Save"
        '
        'TbbPrint
        '
        Me.TbbPrint.ImageIndex = 56
        Me.TbbPrint.Name = "TbbPrint"
        Me.TbbPrint.ToolTipText = "Print"
        '
        'TbbBack
        '
        Me.TbbBack.ImageIndex = 2
        Me.TbbBack.Name = "TbbBack"
        Me.TbbBack.ToolTipText = "Back"
        '
        'GBFacilityData
        '
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
        Me.GBFacilityData.TabIndex = 148
        Me.GBFacilityData.TabStop = False
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(104, 40)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(72, 20)
        Me.txtAIRSNumber.TabIndex = 144
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
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(104, 16)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(184, 20)
        Me.txtFacilityName.TabIndex = 143
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(472, 16)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.ReadOnly = True
        Me.txtReferenceNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtReferenceNumber.TabIndex = 146
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(368, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 145
        Me.Label3.Text = "Reference Number:"
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(240, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 151
        Me.Label4.Text = "Days"
        '
        'txtDaysInAPB
        '
        Me.txtDaysInAPB.Location = New System.Drawing.Point(128, 30)
        Me.txtDaysInAPB.Name = "txtDaysInAPB"
        Me.txtDaysInAPB.ReadOnly = True
        Me.txtDaysInAPB.Size = New System.Drawing.Size(100, 20)
        Me.txtDaysInAPB.TabIndex = 150
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(24, 32)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(96, 13)
        Me.Label24.TabIndex = 149
        Me.Label24.Text = "Total Days in APB:"
        '
        'GBRecordStatus
        '
        Me.GBRecordStatus.Controls.Add(Me.rdbCloseReport)
        Me.GBRecordStatus.Controls.Add(Me.rdbOpenReport)
        Me.GBRecordStatus.Location = New System.Drawing.Point(8, 8)
        Me.GBRecordStatus.Name = "GBRecordStatus"
        Me.GBRecordStatus.Size = New System.Drawing.Size(208, 40)
        Me.GBRecordStatus.TabIndex = 154
        Me.GBRecordStatus.TabStop = False
        Me.GBRecordStatus.Text = "Record Status"
        '
        'rdbCloseReport
        '
        Me.rdbCloseReport.Location = New System.Drawing.Point(104, 16)
        Me.rdbCloseReport.Name = "rdbCloseReport"
        Me.rdbCloseReport.Size = New System.Drawing.Size(96, 16)
        Me.rdbCloseReport.TabIndex = 1
        Me.rdbCloseReport.Text = "Report Closed"
        '
        'rdbOpenReport
        '
        Me.rdbOpenReport.Location = New System.Drawing.Point(16, 16)
        Me.rdbOpenReport.Name = "rdbOpenReport"
        Me.rdbOpenReport.Size = New System.Drawing.Size(88, 16)
        Me.rdbOpenReport.TabIndex = 0
        Me.rdbOpenReport.Text = "Report Open"
        '
        'DTPDateClosed
        '
        Me.DTPDateClosed.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateClosed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateClosed.Location = New System.Drawing.Point(304, 15)
        Me.DTPDateClosed.Name = "DTPDateClosed"
        Me.DTPDateClosed.Size = New System.Drawing.Size(104, 20)
        Me.DTPDateClosed.TabIndex = 153
        Me.DTPDateClosed.Value = New Date(2005, 2, 25, 0, 0, 0, 0)
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(232, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 13)
        Me.Label17.TabIndex = 152
        Me.Label17.Text = "Date Closed:"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Splitter2)
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 96)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(792, 313)
        Me.Panel4.TabIndex = 155
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 56)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(792, 5)
        Me.Splitter2.TabIndex = 157
        Me.Splitter2.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtTestLinksCount)
        Me.GroupBox2.Controls.Add(Me.llbEmissionLog)
        Me.GroupBox2.Controls.Add(Me.txtEmissionLog)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtTestDateEnd)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtTestDateStart)
        Me.GroupBox2.Controls.Add(Me.txtComplianceStatus)
        Me.GroupBox2.Controls.Add(Me.txtSourceTested)
        Me.GroupBox2.Controls.Add(Me.txtPollutant)
        Me.GroupBox2.Controls.Add(Me.txtEngineer)
        Me.GroupBox2.Controls.Add(Me.txtUnitManager)
        Me.GroupBox2.Controls.Add(Me.txtReportType)
        Me.GroupBox2.Controls.Add(Me.txtTestReportType)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtDaysInAPB)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(792, 257)
        Me.GroupBox2.TabIndex = 156
        Me.GroupBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(536, 160)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 183
        Me.Label7.Text = "Count:"
        '
        'txtTestLinksCount
        '
        Me.txtTestLinksCount.Location = New System.Drawing.Point(576, 160)
        Me.txtTestLinksCount.Name = "txtTestLinksCount"
        Me.txtTestLinksCount.ReadOnly = True
        Me.txtTestLinksCount.Size = New System.Drawing.Size(40, 20)
        Me.txtTestLinksCount.TabIndex = 182
        '
        'llbEmissionLog
        '
        Me.llbEmissionLog.Location = New System.Drawing.Point(536, 64)
        Me.llbEmissionLog.Name = "llbEmissionLog"
        Me.llbEmissionLog.Size = New System.Drawing.Size(104, 95)
        Me.llbEmissionLog.TabIndex = 181
        '
        'txtEmissionLog
        '
        Me.txtEmissionLog.Location = New System.Drawing.Point(536, 32)
        Me.txtEmissionLog.Name = "txtEmissionLog"
        Me.txtEmissionLog.ReadOnly = True
        Me.txtEmissionLog.Size = New System.Drawing.Size(56, 20)
        Me.txtEmissionLog.TabIndex = 180
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(368, 64)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(128, 32)
        Me.Label18.TabIndex = 178
        Me.Label18.Text = "Other Test(s) Linked to the same Emission Log:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(368, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 13)
        Me.Label6.TabIndex = 176
        Me.Label6.Text = "Test linked to Emissions Log:"
        '
        'txtTestDateEnd
        '
        Me.txtTestDateEnd.Location = New System.Drawing.Point(232, 176)
        Me.txtTestDateEnd.Name = "txtTestDateEnd"
        Me.txtTestDateEnd.ReadOnly = True
        Me.txtTestDateEnd.Size = New System.Drawing.Size(96, 20)
        Me.txtTestDateEnd.TabIndex = 175
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(312, 58)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(31, 13)
        Me.Label19.TabIndex = 173
        Me.Label19.Text = "Days"
        '
        'txtTestDateStart
        '
        Me.txtTestDateStart.Location = New System.Drawing.Point(128, 176)
        Me.txtTestDateStart.Name = "txtTestDateStart"
        Me.txtTestDateStart.ReadOnly = True
        Me.txtTestDateStart.Size = New System.Drawing.Size(96, 20)
        Me.txtTestDateStart.TabIndex = 172
        '
        'txtComplianceStatus
        '
        Me.txtComplianceStatus.Location = New System.Drawing.Point(128, 200)
        Me.txtComplianceStatus.Name = "txtComplianceStatus"
        Me.txtComplianceStatus.ReadOnly = True
        Me.txtComplianceStatus.Size = New System.Drawing.Size(200, 20)
        Me.txtComplianceStatus.TabIndex = 171
        '
        'txtSourceTested
        '
        Me.txtSourceTested.Location = New System.Drawing.Point(128, 128)
        Me.txtSourceTested.Name = "txtSourceTested"
        Me.txtSourceTested.ReadOnly = True
        Me.txtSourceTested.Size = New System.Drawing.Size(200, 20)
        Me.txtSourceTested.TabIndex = 168
        '
        'txtPollutant
        '
        Me.txtPollutant.Location = New System.Drawing.Point(128, 152)
        Me.txtPollutant.Name = "txtPollutant"
        Me.txtPollutant.ReadOnly = True
        Me.txtPollutant.Size = New System.Drawing.Size(200, 20)
        Me.txtPollutant.TabIndex = 167
        '
        'txtEngineer
        '
        Me.txtEngineer.Location = New System.Drawing.Point(224, 56)
        Me.txtEngineer.Name = "txtEngineer"
        Me.txtEngineer.ReadOnly = True
        Me.txtEngineer.Size = New System.Drawing.Size(40, 20)
        Me.txtEngineer.TabIndex = 166
        '
        'txtUnitManager
        '
        Me.txtUnitManager.Location = New System.Drawing.Point(268, 56)
        Me.txtUnitManager.Name = "txtUnitManager"
        Me.txtUnitManager.ReadOnly = True
        Me.txtUnitManager.Size = New System.Drawing.Size(40, 20)
        Me.txtUnitManager.TabIndex = 165
        '
        'txtReportType
        '
        Me.txtReportType.Location = New System.Drawing.Point(128, 80)
        Me.txtReportType.Name = "txtReportType"
        Me.txtReportType.ReadOnly = True
        Me.txtReportType.Size = New System.Drawing.Size(200, 20)
        Me.txtReportType.TabIndex = 164
        '
        'txtTestReportType
        '
        Me.txtTestReportType.Location = New System.Drawing.Point(128, 104)
        Me.txtTestReportType.Name = "txtTestReportType"
        Me.txtTestReportType.ReadOnly = True
        Me.txtTestReportType.Size = New System.Drawing.Size(200, 20)
        Me.txtTestReportType.TabIndex = 163
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(24, 128)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 162
        Me.Label14.Text = "Source Tested:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(24, 152)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 161
        Me.Label13.Text = "Pollutant:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(24, 200)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(98, 13)
        Me.Label12.TabIndex = 160
        Me.Label12.Text = "Compliance Status:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(24, 176)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 159
        Me.Label11.Text = "Test Date(s):"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(24, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 158
        Me.Label9.Text = "Report Type:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(24, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 157
        Me.Label8.Text = "Test Type:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(24, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(190, 13)
        Me.Label15.TabIndex = 153
        Me.Label15.Text = "Total Days for Engineer/Unit Manager:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 19)
        Me.Label5.TabIndex = 152
        Me.Label5.Text = "Test Report Data"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GBRecordStatus)
        Me.GroupBox1.Controls.Add(Me.DTPDateClosed)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 56)
        Me.GroupBox1.TabIndex = 155
        Me.GroupBox1.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 96)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(792, 5)
        Me.Splitter1.TabIndex = 156
        Me.Splitter1.TabStop = False
        '
        'ISMPClosePrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 409)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GBFacilityData)
        Me.Controls.Add(Me.TBFCE)
        Me.Controls.Add(Me.txtOrigin)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPClosePrint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Close/Print Test Report"
        Me.GBFacilityData.ResumeLayout(False)
        Me.GBFacilityData.PerformLayout()
        Me.GBRecordStatus.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub ISMPClosePrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            LoadInformation()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadInformation()
        Try

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            SQL = "Select " & _
            "DatReceivedDate, strClosed, " & _
            "to_Char(datCompleteDate, 'dd-Mon-yyyy') as CompleteDate, " & _
            "CASE " & _
            "     When DatREviewedByUnitManager = '04-Jul-1776' Then 'False' " & _
            "     Else to_char(DatReviewedByUnitManager, 'dd-Mon-yyyy') " & _
            "END as UnitManagerReviewed, " & _
            "(select strReportType from " & DBNameSpace & ".ISMPReportType " & _
            "where " & DBNameSpace & ".ISMPReportType.strKey = " & DBNameSpace & ".ISMPReportInformation.strReportType) As ReportType, " & _
            "strEmissionSource, " & _
            "(Select strPollutantDescription From " & DBNameSpace & ".LookUPPollutants " & _
            "where " & DBNameSpace & ".LookUPPollutants.strPollutantCode = " & DBNameSpace & ".ISMPReportInformation.strPollutant) as Pollutant, " & _
            "to_Char(DatTestDateStart, 'dd-Mon-yyyy') as TestDateStart, " & _
            "to_char(DatTestDateEnd, 'dd-Mon-yyyy') as TestDateEnd, " & _
            "(select strComplianceStatus From " & DBNameSpace & ".LookUPISMPComplianceStatus " & _
            "where " & DBNameSpace & ".LookUPISMPComplianceStatus.strComplianceKey = " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus) as ComplianceStatus " & _
            "from " & DBNameSpace & ".ISMPReportInformation " & _
            "where strReferenceNumber = '" & txtReferenceNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If dr.Item("strClosed") = True Then
                    rdbOpenReport.Checked = False
                    rdbCloseReport.Checked = True
                    DTPDateClosed.Enabled = False
                Else
                    rdbOpenReport.Checked = True
                    rdbCloseReport.Checked = False
                    DTPDateClosed.Enabled = True
                End If
                If dr.Item("CompleteDate") = "04-Jul-1776" Then
                    DTPDateClosed.Text = Date.Today
                Else
                    DTPDateClosed.Text = dr.Item("CompleteDate")
                End If
                If rdbCloseReport.Checked = True Then
                    txtDaysInAPB.Text = DateDiff(DateInterval.Day, CDate(dr.Item("DatReceivedDate")), CDate(dr.Item("CompleteDate")))
                    txtEngineer.Text = DateDiff(DateInterval.Day, CDate(dr.Item("DatReceivedDate")), CDate(dr.Item("CompleteDate")))
                Else
                    txtDaysInAPB.Text = DateDiff(DateInterval.Day, CDate(dr.Item("DatReceivedDate")), CDate(OracleDate))
                    txtEngineer.Text = DateDiff(DateInterval.Day, CDate(dr.Item("DatReceivedDate")), CDate(OracleDate))
                End If
                If dr.Item("UnitManagerReviewed") = "False" Then
                    txtUnitManager.Text = "X"
                Else
                    txtUnitManager.Text = DateDiff(DateInterval.Day, CDate(dr.Item("DatReceivedDate")), CDate(dr.Item("UnitManagerReviewed")))
                    If rdbCloseReport.Checked = True Then
                        txtEngineer.Text = DateDiff(DateInterval.Day, CDate(dr.Item("UnitManagerReviewed")), CDate(dr.Item("CompleteDate")))
                    Else
                        txtEngineer.Text = DateDiff(DateInterval.Day, CDate(dr.Item("UnitManagerReviewed")), CDate(OracleDate))
                    End If
                End If
                txtReportType.Text = dr.Item("ReportType")
                txtSourceTested.Text = dr.Item("strEmissionSource")
                txtPollutant.Text = dr.Item("Pollutant")
                txtTestDateStart.Text = dr.Item("TestDateStart")
                txtTestDateEnd.Text = dr.Item("TestDateEnd")
                txtComplianceStatus.Text = dr.Item("ComplianceStatus")
            Else
                txtReportType.Text = "ERROR"
                txtSourceTested.Text = "ERROR"
                txtPollutant.Text = "ERROR"
                txtTestDateStart.Text = "ERROR"
                txtTestDateEnd.Text = "ERROR"
                txtComplianceStatus.Text = "ERROR"
            End If

            SQL = "Select strTestLogNumber " & _
            "from " & DBNameSpace & ".ISMPTestLogLink " & _
            "where strReferenceNumber = '" & txtReferenceNumber.Text & "' "
            cmd = New OracleCommand(SQL, Conn)
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then

                llbEmissionLog.Items.Clear()
                txtEmissionLog.Text = dr.Item("strTestLogNumber")

                SQL = "select strReferenceNumber " & _
                "from " & DBNameSpace & ".ISMPTestLogLink " & _
                "where strTestLogNumber = '" & dr.Item("strTestLogNumber") & "' " & _
                "order by strReferenceNumber "

                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    llbEmissionLog.Items.Add(dr.Item("strREferenceNUmber"))
                End While
            Else
                txtEmissionLog.Text = "N/A"
                llbEmissionLog.Items.Clear()
            End If

            txtTestLinksCount.Text = llbEmissionLog.Items.Count
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
    Private Sub TBFCE_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFCE.ButtonClick
        Try

            Select Case TBFCE.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Print()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#Region "Functions and Subs"
    Sub Save()
        Dim CloseState As String

        Try

            If rdbCloseReport.Checked = True Then
                CloseState = True
            Else
                CloseState = False
            End If

            SQL = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
            "strClosed = '" & CloseState & "', " & _
            "datCompleteDate = '" & DTPDateClosed.Text & "', " & _
            "strModifingPerson = '" & UserGCode & "', " & _
            "datModifingDate = '" & OracleDate & "' " & _
            "where strReferencenumber = '" & txtReferenceNumber.Text & "' "

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            cmd = New OracleCommand(SQL, Conn)
            dr = cmd.ExecuteReader

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            LoadInformation()

            MsgBox("Done", MsgBoxStyle.Information, "ISMP Close Print")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Print()
        Try

            PrintOut = Nothing
            If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            PrintOut.txtPrintType.Text = "ISMPAIRSForm"
            PrintOut.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            PrintOut.Show()
            PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub PrintTestReport()
        Try

            PrintOut = Nothing
            If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            PrintOut.txtPrintType.Text = "ISMPTestReport"
            PrintOut.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            PrintOut.Show()
            PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub Back()
        Try

            Select Case txtOrigin.Text
                Case "Facility Summary"
                    ISMPCloseAndPrint = Nothing
                    Me.Hide()
                Case Else
                    ISMPCloseAndPrint = Nothing
                    Conn.Dispose()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region








    Private Sub ISMPClosePrint_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("^(X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("^(C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("^(V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiPrintAFSForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPrintAFSForm.Click
        Try

            Print()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiPrintTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPrintTestReport.Click
        Try

            PrintTestReport()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
End Class
