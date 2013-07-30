Imports System.Data.OracleClient

'Imports Microsoft.Office.Interop

Public Class SSCPEngineerInspectionTool
    Inherits System.Windows.Forms.Form
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Panel1temp As String

    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Dim dsFacilities As DataSet
    Dim daFacilities As OracleDataAdapter
    Dim dsInspections As DataSet
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPOldInspectionTool As System.Windows.Forms.TabPage
    Friend WithEvents TPInspectionTool As System.Windows.Forms.TabPage
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents dgvFacilitesList As System.Windows.Forms.DataGridView
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents llbExportFacilitiesList As System.Windows.Forms.LinkLabel
    Friend WithEvents txtFacilitiesCount As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents llbFacilityExport As System.Windows.Forms.LinkLabel
    Friend WithEvents txtFacilityCount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Dim daInspections As OracleDataAdapter


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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiEdit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MmiClearPage As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents TBEngineerInspection As System.Windows.Forms.ToolBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DTPStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents PanelInspectionSchedule As System.Windows.Forms.Panel
    Friend WithEvents cboInspectionScheduleFilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblInitialSchedule As System.Windows.Forms.Label
    Friend WithEvents lblCurrentSchedule As System.Windows.Forms.Label
    Friend WithEvents lblActualInspection As System.Windows.Forms.Label
    Friend WithEvents txtFacilityAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents llbSearchAIRSNumber As System.Windows.Forms.LinkLabel
    Friend WithEvents llbScheduleInspection As System.Windows.Forms.LinkLabel
    Friend WithEvents dgrFacilityList As System.Windows.Forms.DataGrid
    Friend WithEvents dgrInspectionList As System.Windows.Forms.DataGrid
    Friend WithEvents txtFacilityCityCounty As System.Windows.Forms.TextBox
    Friend WithEvents DTPEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtInspectionNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblScheduleLocked As System.Windows.Forms.Label
    Friend WithEvents txtScheduleStart As System.Windows.Forms.TextBox
    Friend WithEvents txtScheduleEnd As System.Windows.Forms.TextBox
    Friend WithEvents lblDeleteInspection As System.Windows.Forms.LinkLabel
    Friend WithEvents chbDeleteInspection As System.Windows.Forms.CheckBox
    Friend WithEvents lblExportInspectionList As System.Windows.Forms.LinkLabel
    Friend WithEvents lblExportFacilityList As System.Windows.Forms.LinkLabel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnMoreOptions As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPEngineerInspectionTool))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiEdit = New System.Windows.Forms.MenuItem
        Me.MmiCut = New System.Windows.Forms.MenuItem
        Me.MmiCopy = New System.Windows.Forms.MenuItem
        Me.MmiPaste = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MmiClearPage = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBEngineerInspection = New System.Windows.Forms.ToolBar
        Me.TbbClear = New System.Windows.Forms.ToolBarButton
        Me.TbbBack = New System.Windows.Forms.ToolBarButton
        Me.TbbExit = New System.Windows.Forms.ToolBarButton
        Me.dgrFacilityList = New System.Windows.Forms.DataGrid
        Me.PanelInspectionSchedule = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnMoreOptions = New System.Windows.Forms.Button
        Me.lblDeleteInspection = New System.Windows.Forms.LinkLabel
        Me.chbDeleteInspection = New System.Windows.Forms.CheckBox
        Me.txtScheduleEnd = New System.Windows.Forms.TextBox
        Me.txtScheduleStart = New System.Windows.Forms.TextBox
        Me.lblScheduleLocked = New System.Windows.Forms.Label
        Me.txtInspectionNumber = New System.Windows.Forms.TextBox
        Me.lblExportInspectionList = New System.Windows.Forms.LinkLabel
        Me.lblExportFacilityList = New System.Windows.Forms.LinkLabel
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.DTPEndDate = New System.Windows.Forms.DateTimePicker
        Me.llbScheduleInspection = New System.Windows.Forms.LinkLabel
        Me.lblInitialSchedule = New System.Windows.Forms.Label
        Me.lblCurrentSchedule = New System.Windows.Forms.Label
        Me.lblActualInspection = New System.Windows.Forms.Label
        Me.DTPStartDate = New System.Windows.Forms.DateTimePicker
        Me.txtFacilityCityCounty = New System.Windows.Forms.TextBox
        Me.txtFacilityAddress = New System.Windows.Forms.TextBox
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.llbSearchAIRSNumber = New System.Windows.Forms.LinkLabel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboInspectionScheduleFilter = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dgrInspectionList = New System.Windows.Forms.DataGrid
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPOldInspectionTool = New System.Windows.Forms.TabPage
        Me.TPInspectionTool = New System.Windows.Forms.TabPage
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.llbExportFacilitiesList = New System.Windows.Forms.LinkLabel
        Me.txtFacilitiesCount = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.dgvFacilitesList = New System.Windows.Forms.DataGridView
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.llbFacilityExport = New System.Windows.Forms.LinkLabel
        Me.txtFacilityCount = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.Label19 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        CType(Me.dgrFacilityList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelInspectionSchedule.SuspendLayout()
        CType(Me.dgrInspectionList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TPOldInspectionTool.SuspendLayout()
        Me.TPInspectionTool.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.dgvFacilitesList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel11.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFile, Me.MmiEdit, Me.MmiView, Me.MmiHelp})
        '
        'MmiFile
        '
        Me.MmiFile.Index = 0
        Me.MmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack})
        Me.MmiFile.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Back"
        '
        'MmiEdit
        '
        Me.MmiEdit.Index = 1
        Me.MmiEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiCut, Me.MmiCopy, Me.MmiPaste})
        Me.MmiEdit.Text = "Edit"
        '
        'MmiCut
        '
        Me.MmiCut.Index = 0
        Me.MmiCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX
        Me.MmiCut.Text = "Cut"
        '
        'MmiCopy
        '
        Me.MmiCopy.Index = 1
        Me.MmiCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.MmiCopy.Text = "&Copy"
        '
        'MmiPaste
        '
        Me.MmiPaste.Index = 2
        Me.MmiPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV
        Me.MmiPaste.Text = "Paste"
        '
        'MmiView
        '
        Me.MmiView.Index = 2
        Me.MmiView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiClearPage})
        Me.MmiView.Text = "View"
        '
        'MmiClearPage
        '
        Me.MmiClearPage.Index = 0
        Me.MmiClearPage.Text = "Clear Page"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 3
        Me.MmiHelp.Text = "Help"
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
        'TBEngineerInspection
        '
        Me.TBEngineerInspection.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbClear, Me.TbbBack, Me.TbbExit})
        Me.TBEngineerInspection.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBEngineerInspection.DropDownArrows = True
        Me.TBEngineerInspection.ImageList = Me.Image_List_All
        Me.TBEngineerInspection.Location = New System.Drawing.Point(0, 0)
        Me.TBEngineerInspection.Name = "TBEngineerInspection"
        Me.TBEngineerInspection.ShowToolTips = True
        Me.TBEngineerInspection.Size = New System.Drawing.Size(1192, 28)
        Me.TBEngineerInspection.TabIndex = 48
        '
        'TbbClear
        '
        Me.TbbClear.ImageIndex = 84
        Me.TbbClear.Name = "TbbClear"
        '
        'TbbBack
        '
        Me.TbbBack.ImageIndex = 2
        Me.TbbBack.Name = "TbbBack"
        '
        'TbbExit
        '
        Me.TbbExit.ImageIndex = 81
        Me.TbbExit.Name = "TbbExit"
        Me.TbbExit.Visible = False
        '
        'dgrFacilityList
        '
        Me.dgrFacilityList.DataMember = ""
        Me.dgrFacilityList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrFacilityList.Location = New System.Drawing.Point(484, 3)
        Me.dgrFacilityList.Name = "dgrFacilityList"
        Me.dgrFacilityList.ReadOnly = True
        Me.dgrFacilityList.Size = New System.Drawing.Size(397, 191)
        Me.dgrFacilityList.TabIndex = 49
        '
        'PanelInspectionSchedule
        '
        Me.PanelInspectionSchedule.Controls.Add(Me.Label9)
        Me.PanelInspectionSchedule.Controls.Add(Me.btnMoreOptions)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblDeleteInspection)
        Me.PanelInspectionSchedule.Controls.Add(Me.chbDeleteInspection)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtScheduleEnd)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtScheduleStart)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblScheduleLocked)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtInspectionNumber)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblExportInspectionList)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblExportFacilityList)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label13)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label12)
        Me.PanelInspectionSchedule.Controls.Add(Me.DTPEndDate)
        Me.PanelInspectionSchedule.Controls.Add(Me.llbScheduleInspection)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblInitialSchedule)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblCurrentSchedule)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblActualInspection)
        Me.PanelInspectionSchedule.Controls.Add(Me.DTPStartDate)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtFacilityCityCounty)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtFacilityAddress)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtFacilityName)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtAIRSNumber)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label8)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label7)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label6)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label5)
        Me.PanelInspectionSchedule.Controls.Add(Me.llbSearchAIRSNumber)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label4)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label3)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label2)
        Me.PanelInspectionSchedule.Controls.Add(Me.cboInspectionScheduleFilter)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label1)
        Me.PanelInspectionSchedule.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelInspectionSchedule.Location = New System.Drawing.Point(3, 3)
        Me.PanelInspectionSchedule.Name = "PanelInspectionSchedule"
        Me.PanelInspectionSchedule.Size = New System.Drawing.Size(448, 665)
        Me.PanelInspectionSchedule.TabIndex = 50
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(232, 466)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 9)
        Me.Label9.TabIndex = 296
        Me.Label9.Text = "More Options:"
        '
        'btnMoreOptions
        '
        Me.btnMoreOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoreOptions.ImageIndex = 53
        Me.btnMoreOptions.ImageList = Me.Image_List_All
        Me.btnMoreOptions.Location = New System.Drawing.Point(288, 464)
        Me.btnMoreOptions.Name = "btnMoreOptions"
        Me.btnMoreOptions.Size = New System.Drawing.Size(24, 16)
        Me.btnMoreOptions.TabIndex = 295
        '
        'lblDeleteInspection
        '
        Me.lblDeleteInspection.AutoSize = True
        Me.lblDeleteInspection.Location = New System.Drawing.Point(136, 464)
        Me.lblDeleteInspection.Name = "lblDeleteInspection"
        Me.lblDeleteInspection.Size = New System.Drawing.Size(38, 13)
        Me.lblDeleteInspection.TabIndex = 164
        Me.lblDeleteInspection.TabStop = True
        Me.lblDeleteInspection.Text = "Delete"
        '
        'chbDeleteInspection
        '
        Me.chbDeleteInspection.Location = New System.Drawing.Point(24, 464)
        Me.chbDeleteInspection.Name = "chbDeleteInspection"
        Me.chbDeleteInspection.Size = New System.Drawing.Size(112, 16)
        Me.chbDeleteInspection.TabIndex = 163
        Me.chbDeleteInspection.Text = "Delete Inspection"
        '
        'txtScheduleEnd
        '
        Me.txtScheduleEnd.Location = New System.Drawing.Point(272, 272)
        Me.txtScheduleEnd.Name = "txtScheduleEnd"
        Me.txtScheduleEnd.Size = New System.Drawing.Size(16, 20)
        Me.txtScheduleEnd.TabIndex = 162
        Me.txtScheduleEnd.Visible = False
        '
        'txtScheduleStart
        '
        Me.txtScheduleStart.Location = New System.Drawing.Point(256, 272)
        Me.txtScheduleStart.Name = "txtScheduleStart"
        Me.txtScheduleStart.Size = New System.Drawing.Size(16, 20)
        Me.txtScheduleStart.TabIndex = 161
        Me.txtScheduleStart.Visible = False
        '
        'lblScheduleLocked
        '
        Me.lblScheduleLocked.AutoSize = True
        Me.lblScheduleLocked.ForeColor = System.Drawing.Color.Firebrick
        Me.lblScheduleLocked.Location = New System.Drawing.Point(152, 280)
        Me.lblScheduleLocked.Name = "lblScheduleLocked"
        Me.lblScheduleLocked.Size = New System.Drawing.Size(49, 13)
        Me.lblScheduleLocked.TabIndex = 160
        Me.lblScheduleLocked.Text = "- Locked"
        Me.lblScheduleLocked.Visible = False
        '
        'txtInspectionNumber
        '
        Me.txtInspectionNumber.Location = New System.Drawing.Point(240, 272)
        Me.txtInspectionNumber.Name = "txtInspectionNumber"
        Me.txtInspectionNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtInspectionNumber.TabIndex = 159
        Me.txtInspectionNumber.Visible = False
        '
        'lblExportInspectionList
        '
        Me.lblExportInspectionList.AutoSize = True
        Me.lblExportInspectionList.Location = New System.Drawing.Point(328, 304)
        Me.lblExportInspectionList.Name = "lblExportInspectionList"
        Me.lblExportInspectionList.Size = New System.Drawing.Size(149, 13)
        Me.lblExportInspectionList.TabIndex = 158
        Me.lblExportInspectionList.TabStop = True
        Me.lblExportInspectionList.Text = "Export Inspection List to Excel"
        '
        'lblExportFacilityList
        '
        Me.lblExportFacilityList.AutoSize = True
        Me.lblExportFacilityList.Location = New System.Drawing.Point(328, 32)
        Me.lblExportFacilityList.Name = "lblExportFacilityList"
        Me.lblExportFacilityList.Size = New System.Drawing.Size(132, 13)
        Me.lblExportFacilityList.TabIndex = 157
        Me.lblExportFacilityList.TabStop = True
        Me.lblExportFacilityList.Text = "Export Facility List to Excel"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(136, 440)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 156
        Me.Label13.Text = "End Date"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(24, 440)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 13)
        Me.Label12.TabIndex = 155
        Me.Label12.Text = "Start Date"
        '
        'DTPEndDate
        '
        Me.DTPEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEndDate.Location = New System.Drawing.Point(136, 416)
        Me.DTPEndDate.Name = "DTPEndDate"
        Me.DTPEndDate.Size = New System.Drawing.Size(104, 20)
        Me.DTPEndDate.TabIndex = 154
        Me.DTPEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'llbScheduleInspection
        '
        Me.llbScheduleInspection.AutoSize = True
        Me.llbScheduleInspection.Location = New System.Drawing.Point(16, 400)
        Me.llbScheduleInspection.Name = "llbScheduleInspection"
        Me.llbScheduleInspection.Size = New System.Drawing.Size(234, 13)
        Me.llbScheduleInspection.TabIndex = 153
        Me.llbScheduleInspection.TabStop = True
        Me.llbScheduleInspection.Text = "Select Inspection Date Below and Click to Save"
        '
        'lblInitialSchedule
        '
        Me.lblInitialSchedule.AutoSize = True
        Me.lblInitialSchedule.Location = New System.Drawing.Point(24, 296)
        Me.lblInitialSchedule.Name = "lblInitialSchedule"
        Me.lblInitialSchedule.Size = New System.Drawing.Size(86, 13)
        Me.lblInitialSchedule.TabIndex = 152
        Me.lblInitialSchedule.Text = "Read Only Label"
        '
        'lblCurrentSchedule
        '
        Me.lblCurrentSchedule.AutoSize = True
        Me.lblCurrentSchedule.Location = New System.Drawing.Point(24, 328)
        Me.lblCurrentSchedule.Name = "lblCurrentSchedule"
        Me.lblCurrentSchedule.Size = New System.Drawing.Size(86, 13)
        Me.lblCurrentSchedule.TabIndex = 151
        Me.lblCurrentSchedule.Text = "Read Only Label"
        '
        'lblActualInspection
        '
        Me.lblActualInspection.AutoSize = True
        Me.lblActualInspection.Location = New System.Drawing.Point(24, 360)
        Me.lblActualInspection.Name = "lblActualInspection"
        Me.lblActualInspection.Size = New System.Drawing.Size(86, 13)
        Me.lblActualInspection.TabIndex = 150
        Me.lblActualInspection.Text = "Read Only Label"
        '
        'DTPStartDate
        '
        Me.DTPStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartDate.Location = New System.Drawing.Point(24, 416)
        Me.DTPStartDate.Name = "DTPStartDate"
        Me.DTPStartDate.Size = New System.Drawing.Size(104, 20)
        Me.DTPStartDate.TabIndex = 146
        Me.DTPStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'txtFacilityCityCounty
        '
        Me.txtFacilityCityCounty.Location = New System.Drawing.Point(24, 240)
        Me.txtFacilityCityCounty.Name = "txtFacilityCityCounty"
        Me.txtFacilityCityCounty.ReadOnly = True
        Me.txtFacilityCityCounty.Size = New System.Drawing.Size(272, 20)
        Me.txtFacilityCityCounty.TabIndex = 13
        '
        'txtFacilityAddress
        '
        Me.txtFacilityAddress.Location = New System.Drawing.Point(24, 200)
        Me.txtFacilityAddress.Name = "txtFacilityAddress"
        Me.txtFacilityAddress.ReadOnly = True
        Me.txtFacilityAddress.Size = New System.Drawing.Size(272, 20)
        Me.txtFacilityAddress.TabIndex = 12
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(24, 160)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(272, 20)
        Me.txtFacilityName.TabIndex = 11
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(24, 120)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtAIRSNumber.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "City/County:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Facility Address:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Facility Name:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "AIRS Number:"
        '
        'llbSearchAIRSNumber
        '
        Me.llbSearchAIRSNumber.AutoSize = True
        Me.llbSearchAIRSNumber.Location = New System.Drawing.Point(8, 72)
        Me.llbSearchAIRSNumber.Name = "llbSearchAIRSNumber"
        Me.llbSearchAIRSNumber.Size = New System.Drawing.Size(91, 13)
        Me.llbSearchAIRSNumber.TabIndex = 5
        Me.llbSearchAIRSNumber.TabStop = True
        Me.llbSearchAIRSNumber.Text = "Search for Facility"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 344)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(158, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Actual Inspection (If applicable) "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 312)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Current Inspection Schedule"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 280)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Initial Inspection Schedule"
        '
        'cboInspectionScheduleFilter
        '
        Me.cboInspectionScheduleFilter.ItemHeight = 13
        Me.cboInspectionScheduleFilter.Location = New System.Drawing.Point(24, 40)
        Me.cboInspectionScheduleFilter.Name = "cboInspectionScheduleFilter"
        Me.cboInspectionScheduleFilter.Size = New System.Drawing.Size(272, 21)
        Me.cboInspectionScheduleFilter.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select List of Facilties"
        '
        'dgrInspectionList
        '
        Me.dgrInspectionList.DataMember = ""
        Me.dgrInspectionList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrInspectionList.Location = New System.Drawing.Point(484, 211)
        Me.dgrInspectionList.Name = "dgrInspectionList"
        Me.dgrInspectionList.ReadOnly = True
        Me.dgrInspectionList.Size = New System.Drawing.Size(441, 340)
        Me.dgrInspectionList.TabIndex = 51
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPOldInspectionTool)
        Me.TabControl1.Controls.Add(Me.TPInspectionTool)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 28)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1192, 697)
        Me.TabControl1.TabIndex = 163
        '
        'TPOldInspectionTool
        '
        Me.TPOldInspectionTool.Controls.Add(Me.PanelInspectionSchedule)
        Me.TPOldInspectionTool.Controls.Add(Me.dgrInspectionList)
        Me.TPOldInspectionTool.Controls.Add(Me.dgrFacilityList)
        Me.TPOldInspectionTool.Location = New System.Drawing.Point(4, 22)
        Me.TPOldInspectionTool.Name = "TPOldInspectionTool"
        Me.TPOldInspectionTool.Padding = New System.Windows.Forms.Padding(3)
        Me.TPOldInspectionTool.Size = New System.Drawing.Size(1184, 671)
        Me.TPOldInspectionTool.TabIndex = 0
        Me.TPOldInspectionTool.Text = "Old"
        Me.TPOldInspectionTool.UseVisualStyleBackColor = True
        '
        'TPInspectionTool
        '
        Me.TPInspectionTool.Controls.Add(Me.Panel5)
        Me.TPInspectionTool.Controls.Add(Me.Panel4)
        Me.TPInspectionTool.Location = New System.Drawing.Point(4, 22)
        Me.TPInspectionTool.Name = "TPInspectionTool"
        Me.TPInspectionTool.Padding = New System.Windows.Forms.Padding(3)
        Me.TPInspectionTool.Size = New System.Drawing.Size(1184, 691)
        Me.TPInspectionTool.TabIndex = 1
        Me.TPInspectionTool.Text = "Inspection Tool"
        Me.TPInspectionTool.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(391, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(790, 685)
        Me.Panel5.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(388, 685)
        Me.Panel4.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.llbExportFacilitiesList)
        Me.Panel9.Controls.Add(Me.txtFacilitiesCount)
        Me.Panel9.Controls.Add(Me.Label17)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel9.Location = New System.Drawing.Point(0, 389)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(374, 31)
        Me.Panel9.TabIndex = 0
        '
        'llbExportFacilitiesList
        '
        Me.llbExportFacilitiesList.AutoSize = True
        Me.llbExportFacilitiesList.Location = New System.Drawing.Point(6, 9)
        Me.llbExportFacilitiesList.Name = "llbExportFacilitiesList"
        Me.llbExportFacilitiesList.Size = New System.Drawing.Size(78, 13)
        Me.llbExportFacilitiesList.TabIndex = 8
        Me.llbExportFacilitiesList.TabStop = True
        Me.llbExportFacilitiesList.Text = "Export to Excel"
        '
        'txtFacilitiesCount
        '
        Me.txtFacilitiesCount.Location = New System.Drawing.Point(487, 6)
        Me.txtFacilitiesCount.Name = "txtFacilitiesCount"
        Me.txtFacilitiesCount.ReadOnly = True
        Me.txtFacilitiesCount.Size = New System.Drawing.Size(58, 20)
        Me.txtFacilitiesCount.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(446, 9)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 13)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "Count"
        '
        'dgvFacilitesList
        '
        Me.dgvFacilitesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFacilitesList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFacilitesList.Location = New System.Drawing.Point(0, 23)
        Me.dgvFacilitesList.Name = "dgvFacilitesList"
        Me.dgvFacilitesList.Size = New System.Drawing.Size(374, 366)
        Me.dgvFacilitesList.TabIndex = 1
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.llbFacilityExport)
        Me.Panel10.Controls.Add(Me.txtFacilityCount)
        Me.Panel10.Controls.Add(Me.Label18)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 420)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(374, 31)
        Me.Panel10.TabIndex = 1
        '
        'llbFacilityExport
        '
        Me.llbFacilityExport.AutoSize = True
        Me.llbFacilityExport.Location = New System.Drawing.Point(6, 9)
        Me.llbFacilityExport.Name = "llbFacilityExport"
        Me.llbFacilityExport.Size = New System.Drawing.Size(78, 13)
        Me.llbFacilityExport.TabIndex = 8
        Me.llbFacilityExport.TabStop = True
        Me.llbFacilityExport.Text = "Export to Excel"
        '
        'txtFacilityCount
        '
        Me.txtFacilityCount.Location = New System.Drawing.Point(262, 6)
        Me.txtFacilityCount.Name = "txtFacilityCount"
        Me.txtFacilityCount.ReadOnly = True
        Me.txtFacilityCount.Size = New System.Drawing.Size(58, 20)
        Me.txtFacilityCount.TabIndex = 7
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(221, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 13)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "Count"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 23)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(374, 397)
        Me.DataGridView2.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvFacilitesList)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel10)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel12)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel13)
        Me.SplitContainer1.Size = New System.Drawing.Size(790, 451)
        Me.SplitContainer1.SplitterDistance = 374
        Me.SplitContainer1.TabIndex = 3
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label19)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(374, 23)
        Me.Panel11.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(82, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Inspection Data"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 23)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(412, 397)
        Me.DataGridView1.TabIndex = 5
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.Label20)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(412, 23)
        Me.Panel12.TabIndex = 6
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(160, 13)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Full Compliance Evaluation Data"
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.LinkLabel1)
        Me.Panel13.Controls.Add(Me.TextBox6)
        Me.Panel13.Controls.Add(Me.Label21)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel13.Location = New System.Drawing.Point(0, 420)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(412, 31)
        Me.Panel13.TabIndex = 4
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 9)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(78, 13)
        Me.LinkLabel1.TabIndex = 8
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Export to Excel"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(262, 6)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(58, 20)
        Me.TextBox6.TabIndex = 7
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(221, 9)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(35, 13)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "Count"
        '
        'SSCPEngineerInspectionTool
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1192, 725)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TBEngineerInspection)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPEngineerInspectionTool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SSCP Engineer Inspection Schedule"
        CType(Me.dgrFacilityList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelInspectionSchedule.ResumeLayout(False)
        Me.PanelInspectionSchedule.PerformLayout()
        CType(Me.dgrInspectionList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TPOldInspectionTool.ResumeLayout(False)
        Me.TPInspectionTool.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.dgvFacilitesList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub SSCPEngineerInspectionSchedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()


            Exit Sub



            LoadComboBoxes()

            DTPStartDate.Value = OracleDate
            DTPEndDate.Value = OracleDate

            txtInspectionNumber.Clear()
            lblInitialSchedule.Text = "N/A"
            lblCurrentSchedule.Text = "N/A"
            lblActualInspection.Text = "N/A"
            lblScheduleLocked.Visible = False

            FormatFacilitiesGrid()
            FormatInspectionGrid()

            LoadFacilitiesGrid()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Page Load Functions"
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
    Sub LoadComboBoxes()
        Try

            cboInspectionScheduleFilter.Items.Clear()
            cboInspectionScheduleFilter.Items.Add("Currently Assigned Facilities")
            cboInspectionScheduleFilter.Items.Add("Facilities Requiring Inspections/Assigned to Engineer")
            cboInspectionScheduleFilter.Items.Add("Facilities Requiring Inspections/Not Assigned to Engineer")
            cboInspectionScheduleFilter.Items.Add("All Facilities that Engineer has Scheduled")

            cboInspectionScheduleFilter.Text = cboInspectionScheduleFilter.Items.Item(0)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadFacilitiesGrid()
        Try

            'SQL = "Select * " & _
            '     "from " & DBNameSpace & ".VW_SSCPInspection_List " & _
            '     "where strSSCPEngineer = '" & UserGCode & "' " & _
            '     "order by STRFACILITYNAME "

            SQL = "Select * " & _
            "From " & DBNameSpace & ".VW_SSCPInspection_List " & _
            "where numSSCPEngineer = '" & UserGCode & "' " & _
            "order by strFacilityName "

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dsFacilities = New DataSet

            daFacilities = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daFacilities.Fill(dsFacilities, "Facilities")
            dgrFacilityList.DataSource = dsFacilities
            dgrFacilityList.DataMember = "Facilities"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadInspectionGrid()
        Try

            SQL = "Select * " & _
                  "from " & DBNameSpace & ".VW_SSCPInspection_List2 " & _
                  "where AIRSNumber = '" & txtAIRSNumber.Text & "' " & _
                  "Order by InspectionKey ASC "

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dsInspections = New DataSet

            daInspections = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daInspections.Fill(dsInspections, "Inspections")
            dgrInspectionList.DataSource = dsInspections
            dgrInspectionList.DataMember = "Inspections"

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            chbDeleteInspection.Checked = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FormatFacilitiesGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Facilities"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            '0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRAIRSNUMBER"
            objtextcol.HeaderText = "AIRS Number"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRFACILITYNAME"
            objtextcol.HeaderText = "Facility Name"
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            '2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFacilityStreet1"
            objtextcol.HeaderText = "Address"
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            '3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRFACILITYCITY"
            objtextcol.HeaderText = "City"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            '4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRCOUNTYNAME"
            objtextcol.HeaderText = "County Name"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            '5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Engineer"
            objtextcol.HeaderText = "Engineer"
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            '6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionRequired"
            objtextcol.HeaderText = "Inspection Required"
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrFacilityList.TableStyles.Clear()
            dgrFacilityList.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFacilityList.CaptionText = "Facilities List"
            dgrFacilityList.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FormatInspectionGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Inspections"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            '0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionKey"
            objtextcol.HeaderText = "Inspection Key"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            '1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionRequired"
            objtextcol.HeaderText = "Inspection Required"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ScheduleStart"
            objtextcol.HeaderText = "Scheduled"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ScheduleEnd"
            objtextcol.HeaderText = "Schedule End"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            '4   ScheduleLocked
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ScheduleLocked"
            objtextcol.HeaderText = "Schedule Locked"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            '5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "CurrentStart"
            objtextcol.HeaderText = "Current"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "CurrentEnd"
            objtextcol.HeaderText = "Current End"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            '7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ActualStart"
            objtextcol.HeaderText = "Acutal"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ActualEnd"
            objtextcol.HeaderText = "Actual End"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrInspectionList.TableStyles.Clear()
            dgrInspectionList.TableStyles.Add(objGrid)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub



#End Region

#Region "Subs and Functions"
    Sub ClearInspections()
        Try

            txtAIRSNumber.Clear()
            txtFacilityAddress.Clear()
            txtFacilityCityCounty.Clear()
            txtFacilityName.Clear()
            txtInspectionNumber.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub SaveInspectionData()
        Try

            If AccountArray(21, 2) = "0" And AccountArray(21, 3) = "0" And AccountArray(21, 4) = "0" Then
                MsgBox("You do not have sufficent permission to save Compliance Events.", MsgBoxStyle.Information, "Compliance Events")
            Else
                Dim AIRSNumber As String = ""
                Dim ScheduleStart As String = ""
                Dim ScheduleEnd As String = ""
                Dim CurrentStart As String = ""
                Dim CurrentEnd As String = ""

                If txtAIRSNumber.Text.Length = 8 Then
                    AIRSNumber = txtAIRSNumber.Text

                    If lblScheduleLocked.Visible = True Then
                        If DTPStartDate.Text <> "" Then
                            ScheduleStart = txtScheduleStart.Text
                            CurrentStart = DTPStartDate.Text
                        End If
                        If DTPEndDate.Text <> "" Then
                            ScheduleEnd = txtScheduleEnd.Text
                            CurrentEnd = DTPEndDate.Text
                        End If
                    Else
                        If DTPStartDate.Text <> "" Then
                            ScheduleStart = DTPStartDate.Text
                            CurrentStart = DTPStartDate.Text
                        End If
                        If DTPEndDate.Text <> "" Then
                            ScheduleEnd = DTPEndDate.Text
                            CurrentEnd = DTPEndDate.Text
                        End If
                    End If

                    If txtInspectionNumber.Text = "" Then
                        SQL = "Insert into " & DBNameSpace & ".SSCPInspectionTracking " & _
                        "(InspectionKey, strAIRSNumber, strInspectingEngineer, " & _
                        "strLockSchedule, SSCPTrackingNumber, " & _
                        "datScheduleDateStart, datScheduleDateEnd, " & _
                        "datCurrentDateStart, datCurrentDateEnd, " & _
                        "datActualDateStart, datActualDateEnd) " & _
                        "values " & _
                        "(" & DBNameSpace & ".SSCPInspectionTrackingKey.nextval, '0413" & AIRSNumber & "', '" & UserGCode & "', " & _
                        "'False', '', " & _
                        "'" & ScheduleStart & "', '" & ScheduleEnd & "', " & _
                        "'" & CurrentStart & "', '" & CurrentEnd & "', " & _
                        "'', '') "
                    Else
                        SQL = "Update " & DBNameSpace & ".SSCPInspectionTracking set " & _
                        "strInspectingEngineer = '" & UserGCode & "', " & _
                        "datScheduleDateStart = '" & ScheduleStart & "', " & _
                        "datScheduleDateEnd = '" & ScheduleEnd & "', " & _
                        "datCurrentDateStart = '" & CurrentStart & "', " & _
                        "datCurrentDateEnd = '" & CurrentEnd & "' " & _
                        "where InspectionKey = '" & txtInspectionNumber.Text & "' "
                    End If

                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    cmd = New OracleCommand(SQL, Conn)
                    dr = cmd.ExecuteReader

                    If Conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If

                    LoadInspectionGrid()

                    txtInspectionNumber.Text = dgrInspectionList(0, 0)

                Else
                    MsgBox("The AIRS Number is incorrect.", MsgBoxStyle.Information, "Inspection Tracking")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub DeleteInspectionData()
        Try

            If chbDeleteInspection.Checked = False Then
                MsgBox("The Delete Checkbox must be selected to delete inspection.", MsgBoxStyle.Information, "Inspection Tracking")
            Else
                SQL = "Delete " & DBNameSpace & ".SSCPInspectionTracking " & _
                   "where InspectionKey = '" & txtInspectionNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FilterFacilityGrid()
        Try

            If cboInspectionScheduleFilter.Items.Contains(cboInspectionScheduleFilter.Text) Then

                Select Case cboInspectionScheduleFilter.Items.IndexOf(cboInspectionScheduleFilter.Text)
                    Case 0
                        SQL = "Select * " & _
                        "from " & DBNameSpace & ".VW_SSCPInspection_List " & _
                        "where numSSCPEngineer = '" & UserGCode & "' " & _
                        "order by STRFACILITYNAME "

                    Case 1
                        SQL = "Select * " & _
                        "from " & DBNameSpace & ".VW_SSCPInspection_List " & _
                        "where numSSCPEngineer = '" & UserGCode & "' " & _
                        "and InspectionRequired = 'True' " & _
                        "order by STRFACILITYNAME "
                    Case 2
                        SQL = "Select * " & _
                        "from " & DBNameSpace & ".VW_SSCPInspection_List " & _
                        "where numSSCPEngineer <> '" & UserGCode & "' " & _
                        "and InspectionRequired = 'True' " & _
                        "order by STRFACILITYNAME "
                    Case 3
                        SQL = "Select distinct(substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5)) as STRAIRSNUMBER, " & _
                        "strFacilityName, strFacilityStreet1, " & _
                        "strFacilityCity, strCountyName,  " & _
                        "(strLastName|| ', ' ||strFirstName) as Engineer,  " & _
                        "strSSCPEngineer,  " & _
                        "CASE  " & _
                        "    When strInspectionRequired = 'True' then 'True'  " & _
                        "ELSE 'False' " & _
                        "End as InspectionRequired,  " & _
                        "strInspectingEngineer  " & _
                        "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".LookUpCountyInformation,  " & _
                        "" & DBNameSpace & ".SSCPFacilityAssignment, " & DBNameSpace & ".SSCPInspectionsRequired, " & DBNameSpace & ".EPDUserProfiles,  " & _
                        "" & DBNameSpace & ".SSCPInspectionTracking " & _
                        "where substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNUmber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode  " & _
                        "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber " & _
                        "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".SSCPInspectionsRequired.strAIRSNumber (+) " & _
                        "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".SSCPInspectionTracking.strAIRSNumber  " & _
                        "and " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCPFacilityAssignment.strSSCPENgineer "
                    Case Else
                        SQL = "Select * " & _
                        "from " & DBNameSpace & ".VW_SSCPInspection_List " & _
                        "where numSSCPEngineer = '" & UserGCode & "' " & _
                        "order by STRFACILITYNAME "

                End Select

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dsFacilities = New DataSet

                daFacilities = New OracleDataAdapter(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                daFacilities.Fill(dsFacilities, "Facilities")
                dgrFacilityList.DataSource = dsFacilities
                dgrFacilityList.DataMember = "Facilities"

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If


                dsInspections = New DataSet

                dgrInspectionList.DataSource = dsInspections
                dgrInspectionList.CaptionText = ""
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub ExportFacilityListToExcel()
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim intRow As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            With ExcelApp
                .SheetsInNewWorkbook = 1
                .Workbooks.Add()
                .Worksheets(1).Select()
                'For displaying the column name in the the excel file.

                .Cells(1, 1) = "AIRS Number"
                .Cells(1, 2) = "Facility Name"
                .Cells(1, 3) = "Facility Address"
                .Cells(1, 4) = "City"
                .Cells(1, 5) = "County Name"
                .Cells(1, 6) = "Engineer"
                .Cells(1, 7) = "Inspection Required"

                For intRow = 0 To Me.BindingContext(dsFacilities, "Facilities").Count - 1
                    .Cells(intRow + 2, 1).value = dgrFacilityList.Item(intRow, 0)
                    .Cells(intRow + 2, 2).value = dgrFacilityList.Item(intRow, 1)
                    .Cells(intRow + 2, 3).value = dgrFacilityList.Item(intRow, 2)
                    .Cells(intRow + 2, 4).value = dgrFacilityList.Item(intRow, 3)
                    .Cells(intRow + 2, 5).value = dgrFacilityList.Item(intRow, 4)
                    .Cells(intRow + 2, 6).value = dgrFacilityList.Item(intRow, 5)
                    .Cells(intRow + 2, 7).value = dgrFacilityList.Item(intRow, 6)
                Next

            End With
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub ExportInspectionListToExcel()
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim intRow As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            With ExcelApp
                .SheetsInNewWorkbook = 1
                .Workbooks.Add()
                .Worksheets(1).Select()
                'For displaying the column name in the the excel file.

                .Cells(1, 1) = "Facility Name"
                .Cells(1, 2) = "Inspection Required"
                .Cells(1, 3) = "Scheduled Start"
                .Cells(1, 4) = "Scheduled End"
                .Cells(1, 5) = "Scheduled Locked"
                .Cells(1, 6) = "Current Start"
                .Cells(1, 7) = "Current End"
                .Cells(1, 8) = "Actual Inspection Start"
                .Cells(1, 9) = "Actual Inspection End"

                For intRow = 0 To Me.BindingContext(dsInspections, "Inspections").Count - 1
                    .Cells(intRow + 2, 1).value = txtFacilityName.Text
                    .Cells(intRow + 2, 2).value = dgrInspectionList.Item(intRow, 1)
                    .Cells(intRow + 2, 3).value = dgrInspectionList.Item(intRow, 2)
                    .Cells(intRow + 2, 4).value = dgrInspectionList.Item(intRow, 3)
                    .Cells(intRow + 2, 5).value = dgrInspectionList.Item(intRow, 4)
                    .Cells(intRow + 2, 6).value = dgrInspectionList.Item(intRow, 5)
                    .Cells(intRow + 2, 7).value = dgrInspectionList.Item(intRow, 6)
                    .Cells(intRow + 2, 8).value = dgrInspectionList.Item(intRow, 7)
                    .Cells(intRow + 2, 9).value = dgrInspectionList.Item(intRow, 8)
                Next

            End With
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
#Region "Main Menu"
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiClearPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClearPage.Click
        Try

            ClearInspections()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        Try

            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region


    Private Sub SSCPEngineerInspectionTool_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            SSCPInspectionsTool = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrFacilityList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrFacilityList.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrFacilityList.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrFacilityList(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrFacilityList(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrFacilityList(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrFacilityList(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrFacilityList(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrFacilityList(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrFacilityList(hti.Row, 6)) Then
                                        Else
                                            txtAIRSNumber.Clear()
                                            txtFacilityName.Clear()
                                            txtFacilityAddress.Clear()
                                            txtFacilityCityCounty.Clear()

                                            dgrInspectionList.CaptionText = dgrFacilityList(hti.Row, 1)
                                            txtAIRSNumber.Text = dgrFacilityList(hti.Row, 0)
                                            txtFacilityName.Text = dgrFacilityList(hti.Row, 1)
                                            txtFacilityAddress.Text = dgrFacilityList(hti.Row, 2)
                                            txtFacilityCityCounty.Text = dgrFacilityList(hti.Row, 3) & " / " & dgrFacilityList(hti.Row, 4)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub txtAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAIRSNumber.TextChanged
        Try

            txtFacilityName.Clear()
            txtFacilityAddress.Clear()
            txtFacilityCityCounty.Clear()

            txtInspectionNumber.Clear()
            lblInitialSchedule.Text = "N/A"
            lblCurrentSchedule.Text = "N/A"
            lblActualInspection.Text = "N/A"
            lblScheduleLocked.Visible = False

            If txtAIRSNumber.Text <> "" Then
                If txtAIRSNumber.Text.Length = 8 Then
                    If txtFacilityName.Text = "" Then
                        SQL = "Select strFacilityName, strFacilityStreet1, " & _
                        "strFacilityCity, strCountyName " & _
                        "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".LookUpCountyInformation " & _
                        "where substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNUmber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                        "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNUmber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, Conn)

                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If

                        dr = cmd.ExecuteReader

                        While dr.Read
                            txtFacilityName.Text = dr.Item("strFacilityName")
                            dgrInspectionList.CaptionText = dr.Item("strFacilityName")
                            txtFacilityAddress.Text = dr.Item("strFacilityStreet1")
                            txtFacilityCityCounty.Text = dr.Item("strFacilityCity") & " / " & dr.Item("strCountyName")
                        End While

                        If Conn.State = ConnectionState.Open Then
                            'conn.close()
                        End If

                    End If
                    LoadInspectionGrid()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrInspectionList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrInspectionList.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrInspectionList.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrInspectionList(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrInspectionList(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrInspectionList(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrInspectionList(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrInspectionList(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrInspectionList(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrInspectionList(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrInspectionList(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrInspectionList(hti.Row, 8)) Then
                                                Else
                                                    txtInspectionNumber.Text = dgrInspectionList(hti.Row, 0)

                                                    txtScheduleStart.Text = dgrInspectionList(hti.Row, 2)
                                                    txtScheduleEnd.Text = dgrInspectionList(hti.Row, 3)

                                                    If dgrInspectionList(hti.Row, 2) = dgrInspectionList(hti.Row, 3) Then
                                                        lblInitialSchedule.Text = dgrInspectionList(hti.Row, 2)
                                                    Else
                                                        lblInitialSchedule.Text = dgrInspectionList(hti.Row, 2) & " --> " & dgrInspectionList(hti.Row, 3)
                                                    End If
                                                    If dgrInspectionList(hti.Row, 4) = "True" Then
                                                        lblScheduleLocked.Visible = True
                                                    Else
                                                        lblScheduleLocked.Visible = False
                                                    End If

                                                    If dgrInspectionList(hti.Row, 5) = dgrInspectionList(hti.Row, 6) Then
                                                        lblCurrentSchedule.Text = dgrInspectionList(hti.Row, 5)
                                                    Else
                                                        lblCurrentSchedule.Text = dgrInspectionList(hti.Row, 5) & " --> " & dgrInspectionList(hti.Row, 6)
                                                    End If

                                                    DTPStartDate.Value = dgrInspectionList(hti.Row, 5)
                                                    DTPEndDate.Value = dgrInspectionList(hti.Row, 6)

                                                    If dgrInspectionList(hti.Row, 7) = "N/A" Then
                                                        lblActualInspection.Text = "N/A"
                                                    Else
                                                        If dgrInspectionList(hti.Row, 7) = dgrInspectionList(hti.Row, 8) Then
                                                            lblActualInspection.Text = dgrInspectionList(hti.Row, 7)
                                                        Else
                                                            lblActualInspection.Text = dgrInspectionList(hti.Row, 7) & " --> " & dgrInspectionList(hti.Row, 8)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub llbScheduleInspection_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbScheduleInspection.LinkClicked
        Try

            SaveInspectionData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub DTPStartDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPStartDate.ValueChanged
        Try

            If DTPEndDate.Value < DTPStartDate.Value Then
                DTPEndDate.Value = DTPStartDate.Value
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub DTPEndDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPEndDate.ValueChanged
        Try

            If DTPEndDate.Value < DTPStartDate.Value Then
                DTPEndDate.Value = DTPStartDate.Value
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub lblDeleteInspection_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblDeleteInspection.LinkClicked
        Try

            If lblScheduleLocked.Visible = True Then
                MsgBox("You cannot delete this inspection because it is locked by a Manager.", MsgBoxStyle.Information, "Inspection Tracking")
            Else
                DeleteInspectionData()
                LoadInspectionGrid()
                chbDeleteInspection.Checked = False

                txtInspectionNumber.Clear()
                lblInitialSchedule.Text = "N/A"
                lblCurrentSchedule.Text = "N/A"
                lblActualInspection.Text = "N/A"
                lblScheduleLocked.Visible = False

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub llbSearchAIRSNumber_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchAIRSNumber.LinkClicked
        Try

            FilterFacilityGrid()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBEngineerInspection_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBEngineerInspection.ButtonClick
        Try

            Select Case TBEngineerInspection.Buttons.IndexOf(e.Button)
                Case 0
                    ClearInspections()
                Case 1
                    Me.Close()
                Case 2
                    Me.Close()
                Case Else

            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub lblExportFacilityList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblExportFacilityList.LinkClicked
        Try

            ExportFacilityListToExcel()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub lblExportInspectionList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblExportInspectionList.LinkClicked
        Try

            ExportInspectionListToExcel()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub



    Private Sub btnMoreOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreOptions.Click
        Dim TempHeight As String
        Dim TempWidth As String
        Try

            TempHeight = PanelInspectionSchedule.Size.Height
            TempWidth = PanelInspectionSchedule.Size.Width

            If TempWidth <= 312 Then
                PanelInspectionSchedule.Size = New System.Drawing.Size(500, TempHeight)
            Else
                PanelInspectionSchedule.Size = New System.Drawing.Size(312, TempHeight)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub btnSearchFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SQL = "select " & _
            "SUBSTR(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNUMBER, " & _
            "STRFACILITYNAME, STRCLASS, STRFACILITYCITY, " & _
            "STROPERATIONALSTATUS, STRCOUNTYNAME, STRCMSMEMBER, " & _
            "LASTFCE, LASTINSPECTION, " & _
            "case " & _
            "when NUMSSCPENGINEER is null then '' " & _
            "else (STRLASTNAME||', '||STRFIRSTNAME) " & _
            "end STAFFRESPONSIBLE, " & _
            "STRUNITDESC " & _
            "from " & DBNameSpace & ".VW_SSCP_MT_FACILITYASSIGNMENT, " & _
            "" & DBNameSpace & ".SSCPINSPECTIONSREQUIRED, " & DBNameSpace & ".EPDUSERPROFILES, " & _
            "" & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = " & DBNameSpace & ".VW_SSCP_MT_FACILITYASSIGNMENT.STRAIRSNUMBER " & _
            "and " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.NUMSSCPENGINEER =  " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID (+) " & _
            "and " & DBNameSpace & ".EPDUSERPROFILES.NUMUNIT = " & DBNameSpace & ".LOOKUPEPDUNITS.NUMUNITCODE (+) " & _
            "and INTYEAR = '2011' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "FacilitiesList")
            dgvFacilitesList.DataSource = ds
            dgvFacilitesList.DataMember = "FacilitiesList"



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class
