Imports System.Data.OracleClient



Public Class ISMPTestMemoViewer
    Inherits DefaultForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim dsMemo As DataSet
    Dim daMemo As OracleDataAdapter



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
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiReset As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiShowToolbar As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TbbReset As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents dgrMemoViewer As System.Windows.Forms.DataGrid
    Friend WithEvents GBFilterAndSortOption As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents txtFilterText1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LLRunSearch As System.Windows.Forms.LinkLabel
    Friend WithEvents TBTestMemoViewer As System.Windows.Forms.ToolBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LLSelectReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chbDelete As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chbClosed As System.Windows.Forms.CheckBox
    Friend WithEvents chbOpen As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chbComplianceStatus5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus1 As System.Windows.Forms.CheckBox
    Friend WithEvents LLViewMemo As System.Windows.Forms.LinkLabel
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceNumber2 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPTestMemoViewer))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.MmiCopy = New System.Windows.Forms.MenuItem
        Me.MmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiReset = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiShowToolbar = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBTestMemoViewer = New System.Windows.Forms.ToolBar
        Me.TbbReset = New System.Windows.Forms.ToolBarButton
        Me.TbbBack = New System.Windows.Forms.ToolBarButton
        Me.TbbExit = New System.Windows.Forms.ToolBarButton
        Me.dgrMemoViewer = New System.Windows.Forms.DataGrid
        Me.GBFilterAndSortOption = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chbClosed = New System.Windows.Forms.CheckBox
        Me.chbOpen = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chbComplianceStatus5 = New System.Windows.Forms.CheckBox
        Me.chbComplianceStatus4 = New System.Windows.Forms.CheckBox
        Me.chbComplianceStatus3 = New System.Windows.Forms.CheckBox
        Me.chbComplianceStatus2 = New System.Windows.Forms.CheckBox
        Me.chbComplianceStatus1 = New System.Windows.Forms.CheckBox
        Me.chbDelete = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.LLViewMemo = New System.Windows.Forms.LinkLabel
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtReferenceNumber2 = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LLSelectReport = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.LLRunSearch = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFilterText1 = New System.Windows.Forms.TextBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        CType(Me.dgrMemoViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBFilterAndSortOption.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack, Me.MenuItem9, Me.MmiExit})
        Me.MenuItem1.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Return to Navigation"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 1
        Me.MenuItem9.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 2
        Me.MmiExit.Text = "Exit"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.MmiCopy, Me.MmiPaste, Me.MenuItem10, Me.MmiReset})
        Me.MenuItem2.Text = "Edit"
        '
        'mmiCut
        '
        Me.mmiCut.Index = 0
        Me.mmiCut.Text = "Cut"
        '
        'MmiCopy
        '
        Me.MmiCopy.Index = 1
        Me.MmiCopy.Text = "Copy "
        '
        'MmiPaste
        '
        Me.MmiPaste.Index = 2
        Me.MmiPaste.Text = "Paste"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 3
        Me.MenuItem10.Text = "-"
        '
        'MmiReset
        '
        Me.MmiReset.Index = 4
        Me.MmiReset.Text = "Reset Options"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiShowToolbar})
        Me.MenuItem3.Text = "Tools"
        '
        'mmiShowToolbar
        '
        Me.mmiShowToolbar.Index = 0
        Me.mmiShowToolbar.Text = "Show Toolbar"
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
        'TBTestMemoViewer
        '
        Me.TBTestMemoViewer.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbReset, Me.TbbBack, Me.TbbExit})
        Me.TBTestMemoViewer.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBTestMemoViewer.DropDownArrows = True
        Me.TBTestMemoViewer.ImageList = Me.Image_List_All
        Me.TBTestMemoViewer.Location = New System.Drawing.Point(0, 0)
        Me.TBTestMemoViewer.Name = "TBTestMemoViewer"
        Me.TBTestMemoViewer.ShowToolTips = True
        Me.TBTestMemoViewer.Size = New System.Drawing.Size(950, 28)
        Me.TBTestMemoViewer.TabIndex = 231
        '
        'TbbReset
        '
        Me.TbbReset.ImageIndex = 84
        Me.TbbReset.Name = "TbbReset"
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
        'dgrMemoViewer
        '
        Me.dgrMemoViewer.DataMember = ""
        Me.dgrMemoViewer.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgrMemoViewer.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrMemoViewer.Location = New System.Drawing.Point(0, 286)
        Me.dgrMemoViewer.Name = "dgrMemoViewer"
        Me.dgrMemoViewer.ReadOnly = True
        Me.dgrMemoViewer.Size = New System.Drawing.Size(950, 352)
        Me.dgrMemoViewer.TabIndex = 232
        '
        'GBFilterAndSortOption
        '
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox3)
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox4)
        Me.GBFilterAndSortOption.Controls.Add(Me.chbDelete)
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox2)
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox1)
        Me.GBFilterAndSortOption.Controls.Add(Me.LLRunSearch)
        Me.GBFilterAndSortOption.Controls.Add(Me.Label1)
        Me.GBFilterAndSortOption.Controls.Add(Me.txtFilterText1)
        Me.GBFilterAndSortOption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBFilterAndSortOption.Location = New System.Drawing.Point(0, 28)
        Me.GBFilterAndSortOption.Name = "GBFilterAndSortOption"
        Me.GBFilterAndSortOption.Size = New System.Drawing.Size(950, 258)
        Me.GBFilterAndSortOption.TabIndex = 236
        Me.GBFilterAndSortOption.TabStop = False
        Me.GBFilterAndSortOption.Text = "Filter Options"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.chbClosed)
        Me.GroupBox3.Controls.Add(Me.chbOpen)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 64)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(288, 56)
        Me.GroupBox3.TabIndex = 258
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Open/Closed"
        '
        'chbClosed
        '
        Me.chbClosed.Location = New System.Drawing.Point(8, 32)
        Me.chbClosed.Name = "chbClosed"
        Me.chbClosed.Size = New System.Drawing.Size(80, 16)
        Me.chbClosed.TabIndex = 244
        Me.chbClosed.Text = "Closed"
        '
        'chbOpen
        '
        Me.chbOpen.Location = New System.Drawing.Point(8, 16)
        Me.chbOpen.Name = "chbOpen"
        Me.chbOpen.Size = New System.Drawing.Size(80, 16)
        Me.chbOpen.TabIndex = 243
        Me.chbOpen.Text = "Open"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus5)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus4)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus3)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus2)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus1)
        Me.GroupBox4.Location = New System.Drawing.Point(320, 24)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(228, 100)
        Me.GroupBox4.TabIndex = 257
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Compliance Status"
        '
        'chbComplianceStatus5
        '
        Me.chbComplianceStatus5.Location = New System.Drawing.Point(8, 80)
        Me.chbComplianceStatus5.Name = "chbComplianceStatus5"
        Me.chbComplianceStatus5.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus5.TabIndex = 246
        Me.chbComplianceStatus5.Text = "Not In Compliance"
        '
        'chbComplianceStatus4
        '
        Me.chbComplianceStatus4.Location = New System.Drawing.Point(8, 64)
        Me.chbComplianceStatus4.Name = "chbComplianceStatus4"
        Me.chbComplianceStatus4.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus4.TabIndex = 245
        Me.chbComplianceStatus4.Text = "Indeterminate"
        '
        'chbComplianceStatus3
        '
        Me.chbComplianceStatus3.Location = New System.Drawing.Point(8, 48)
        Me.chbComplianceStatus3.Name = "chbComplianceStatus3"
        Me.chbComplianceStatus3.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus3.TabIndex = 244
        Me.chbComplianceStatus3.Text = "In Compliance"
        '
        'chbComplianceStatus2
        '
        Me.chbComplianceStatus2.Location = New System.Drawing.Point(8, 32)
        Me.chbComplianceStatus2.Name = "chbComplianceStatus2"
        Me.chbComplianceStatus2.Size = New System.Drawing.Size(184, 16)
        Me.chbComplianceStatus2.TabIndex = 243
        Me.chbComplianceStatus2.Text = "For Information Purpose Only"
        '
        'chbComplianceStatus1
        '
        Me.chbComplianceStatus1.Location = New System.Drawing.Point(8, 16)
        Me.chbComplianceStatus1.Name = "chbComplianceStatus1"
        Me.chbComplianceStatus1.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus1.TabIndex = 242
        Me.chbComplianceStatus1.Text = "File Open"
        '
        'chbDelete
        '
        Me.chbDelete.Location = New System.Drawing.Point(328, 128)
        Me.chbDelete.Name = "chbDelete"
        Me.chbDelete.Size = New System.Drawing.Size(104, 16)
        Me.chbDelete.TabIndex = 254
        Me.chbDelete.Text = "Deleted Record"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LLViewMemo)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtReferenceNumber2)
        Me.GroupBox2.Location = New System.Drawing.Point(568, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(224, 72)
        Me.GroupBox2.TabIndex = 253
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "View Memo Only"
        '
        'LLViewMemo
        '
        Me.LLViewMemo.AutoSize = True
        Me.LLViewMemo.Location = New System.Drawing.Point(136, 42)
        Me.LLViewMemo.Name = "LLViewMemo"
        Me.LLViewMemo.Size = New System.Drawing.Size(62, 13)
        Me.LLViewMemo.TabIndex = 250
        Me.LLViewMemo.TabStop = True
        Me.LLViewMemo.Text = "View Memo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 249
        Me.Label4.Text = "Reference Number"
        '
        'txtReferenceNumber2
        '
        Me.txtReferenceNumber2.Location = New System.Drawing.Point(24, 40)
        Me.txtReferenceNumber2.Name = "txtReferenceNumber2"
        Me.txtReferenceNumber2.Size = New System.Drawing.Size(100, 20)
        Me.txtReferenceNumber2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LLSelectReport)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtReferenceNumber)
        Me.GroupBox1.Location = New System.Drawing.Point(568, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 72)
        Me.GroupBox1.TabIndex = 252
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Test Report"
        '
        'LLSelectReport
        '
        Me.LLSelectReport.AutoSize = True
        Me.LLSelectReport.Location = New System.Drawing.Point(136, 42)
        Me.LLSelectReport.Name = "LLSelectReport"
        Me.LLSelectReport.Size = New System.Drawing.Size(72, 13)
        Me.LLSelectReport.TabIndex = 250
        Me.LLSelectReport.TabStop = True
        Me.LLSelectReport.Text = "Select Report"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 249
        Me.Label3.Text = "Reference Number"
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(24, 40)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtReferenceNumber.TabIndex = 0
        '
        'LLRunSearch
        '
        Me.LLRunSearch.AutoSize = True
        Me.LLRunSearch.Location = New System.Drawing.Point(8, 128)
        Me.LLRunSearch.Name = "LLRunSearch"
        Me.LLRunSearch.Size = New System.Drawing.Size(193, 13)
        Me.LLRunSearch.TabIndex = 251
        Me.LLRunSearch.TabStop = True
        Me.LLRunSearch.Text = "Run Search with Filter and Sort Options"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 248
        Me.Label1.Text = "Memo Text"
        '
        'txtFilterText1
        '
        Me.txtFilterText1.Location = New System.Drawing.Point(48, 40)
        Me.txtFilterText1.Name = "txtFilterText1"
        Me.txtFilterText1.Size = New System.Drawing.Size(248, 20)
        Me.txtFilterText1.TabIndex = 247
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 283)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(950, 3)
        Me.Splitter1.TabIndex = 237
        Me.Splitter1.TabStop = False
        '
        'ISMPTestMemoViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(950, 638)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.GBFilterAndSortOption)
        Me.Controls.Add(Me.dgrMemoViewer)
        Me.Controls.Add(Me.TBTestMemoViewer)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPTestMemoViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Test Memo Viewer"
        CType(Me.dgrMemoViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBFilterAndSortOption.ResumeLayout(False)
        Me.GBFilterAndSortOption.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub ISMPTestMemoViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()

            LoadDataSet(True)
            FormatdgrTestReportViewer()
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


#End Region

    Private Sub ISMPTestMemoViewer_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            ISMPMemoViewer = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#Region "Functions"
    Sub LoadDataSet(ByVal Loading As String)
        Dim SQL As String
        Dim SQLLine As String = " "
        Dim SQLLine2 As String = "AND ("
        Dim SQLLine3 As String = "AND ("
        Dim SQLLine4 As String = " "

        Try

            If Loading = True Then
                SQLLine = "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = '" & UserGCode & "' " & _
                "and strClosed = 'False' "
            End If

            If txtFilterText1.Text <> "" Then
                SQLLine = "And Upper(strMemorandumField) like Upper('%" & txtFilterText1.Text & "%') "
            Else
                'SQLLine = SQLLine
            End If

            If chbOpen.Checked = True Then
                SQLLine2 = SQLLine2 & "strClosed = 'False' or "
            End If
            If chbClosed.Checked = True Then
                SQLLine2 = SQLLine2 & "strClosed = 'True' or "
            End If
            If SQLLine2 = "AND (" Then
                SQLLine2 = ""
            Else
                SQLLine2 = Mid(SQLLine2, 1, (Len(SQLLine2) - 4)) & ") "
            End If

            If chbComplianceStatus1.Checked = True Then
                SQLLine3 = SQLLine3 & "" & DBNameSpace & ".ISMPReportInformation.strCOmplianceStatus = '01' or "
            End If
            If chbComplianceStatus2.Checked = True Then
                SQLLine3 = SQLLine3 & "" & DBNameSpace & ".ISMPReportInformation.strCOmplianceStatus = '02' or "
            End If
            If chbComplianceStatus3.Checked = True Then
                SQLLine3 = SQLLine3 & "" & DBNameSpace & ".ISMPReportInformation.strCOmplianceStatus = '03' or "
            End If
            If chbComplianceStatus4.Checked = True Then
                SQLLine3 = SQLLine3 & "" & DBNameSpace & ".ISMPReportInformation.strCOmplianceStatus = '04' or "
            End If
            If chbComplianceStatus5.Checked = True Then
                SQLLine3 = SQLLine3 & "" & DBNameSpace & ".ISMPReportInformation.strCOmplianceStatus = '05' or "
            End If
            If SQLLine3 = "AND (" Then
                SQLLine3 = ""
            Else
                SQLLine3 = Mid(SQLLine3, 1, (Len(SQLLine3) - 4)) & ") "
            End If
            If chbDelete.Checked = True Then
                SQLLine4 = "And " & DBNameSpace & ".ISMPReportInformation.strDelete = 'DELETE' "
            Else
                SQLLine4 = "And " & DBNameSpace & ".ISMPReportInformation.strDelete is NULL "
            End If

            SQL = "select " & DBNameSpace & ".ISMPTestREportMemo.strReferenceNumber, strMemorandumField " & _
            "from " & DBNameSpace & ".ISMPTestREportMemo, " & DBNameSpace & ".ISMPReportInformation " & _
            "where " & DBNameSpace & ".ISMPTestREportMemo.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
            SQLLine & SQLLine2 & SQLLine3 & SQLLine4

            dsMemo = New DataSet

            Dim cmd As New OracleCommand(SQL, Conn)

            daMemo = New OracleDataAdapter(cmd)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daMemo.Fill(dsMemo, "TestMemoViewer")
            dgrMemoViewer.DataSource = dsMemo
            dgrMemoViewer.DataMember = "TestMemoViewer"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FormatdgrTestReportViewer()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "TestMemoViewer"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReferenceNumber"
            objtextcol.HeaderText = "Reference Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings   2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strMemorandumField"
            objtextcol.HeaderText = "Memo Text Body"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrMemoViewer.TableStyles.Clear()
            dgrMemoViewer.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrMemoViewer.CaptionText = "Test Memo Viewer"
            dgrMemoViewer.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub ResetOptions()
        Try

            txtFilterText1.Clear()
            chbOpen.Checked = False
            chbClosed.Checked = False
            chbDelete.Checked = False
            chbComplianceStatus1.Checked = False
            chbComplianceStatus2.Checked = False
            chbComplianceStatus3.Checked = False
            chbComplianceStatus4.Checked = False
            chbComplianceStatus5.Checked = False
            txtReferenceNumber.Clear()
            txtReferenceNumber2.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub SelectTestReport()
        Dim SQL As String

        Try

            If txtReferenceNumber.Text <> "" Then
                SQL = "select " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
                 "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey and " & _
                 "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Dim cmd As New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    ISMPTestReportsEntry = Nothing
                    If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                    ISMPTestReportsEntry.txtReferenceNumber.Text = txtReferenceNumber.Text
                    ISMPTestReportsEntry.Show()
                    Me.Hide()
                    ISMPTestReportsEntry.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
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
#End Region

    Private Sub TBTestMemoViewer_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBTestMemoViewer.ButtonClick
        Try

            Select Case TBTestMemoViewer.Buttons.IndexOf(e.Button)
                Case 0
                    ResetOptions()
                Case 1
                    Me.Close()
                Case 2
                    Me.Close()
                Case Else
                    MsgBox("try clicking again")
            End Select
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

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiExit.Click
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
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("^(x)")
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

            SendKeys.Send("^(c)")
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

            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiReset.Click
        Try

            ResetOptions()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiShowToolbar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiShowToolbar.Click
        Try

            If TBTestMemoViewer.Visible = True Then
                TBTestMemoViewer.Visible = False
                mmiShowToolbar.Checked = True
            Else
                TBTestMemoViewer.Visible = True
                mmiShowToolbar.Checked = False
            End If
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
    Private Sub LLSelectReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLSelectReport.LinkClicked
        Try

            SelectTestReport()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub LLViewMemo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLViewMemo.LinkClicked
        Try

            ISMPMemoEdit = Nothing
            If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
            ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber2.Text
            ISMPMemoEdit.Show()
            ISMPMemoEdit.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub LLRunSearch_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLRunSearch.LinkClicked
        Try

            dsMemo.Clear()
            LoadDataSet(False)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrMemoViewer_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrMemoViewer.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrMemoViewer.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrMemoViewer(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrMemoViewer(hti.Row, 1)) Then
                    Else
                        txtReferenceNumber.Text = dgrMemoViewer(hti.Row, 0)
                        txtReferenceNumber2.Text = dgrMemoViewer(hti.Row, 0)
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


End Class
