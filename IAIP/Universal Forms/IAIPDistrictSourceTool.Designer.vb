<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPDistrictSourceTool
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPDistrictSourceTool))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBManagingDistricts = New System.Windows.Forms.ToolBar
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TCDistrictSourcesTool = New System.Windows.Forms.TabControl
        Me.TPManageDistricts = New System.Windows.Forms.TabPage
        Me.PanelCounties = New System.Windows.Forms.Panel
        Me.clbCounties = New System.Windows.Forms.CheckedListBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.TCDistrictAssignmentOptions = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnViewDistrict = New System.Windows.Forms.Button
        Me.BtnSaveDistricts = New System.Windows.Forms.Button
        Me.btnClearChecks = New System.Windows.Forms.Button
        Me.btnCheckAllCounties = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboDistricts = New System.Windows.Forms.ComboBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.llbViewDistricts = New System.Windows.Forms.LinkLabel
        Me.clbDistricts = New System.Windows.Forms.CheckedListBox
        Me.TPNewDistricts = New System.Windows.Forms.TabPage
        Me.PanelDistrictChanges = New System.Windows.Forms.Panel
        Me.cboDistrictManager = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnAddUpdateInfo = New System.Windows.Forms.Button
        Me.cboDistrictToRemove = New System.Windows.Forms.ComboBox
        Me.chbRemoveDistrict = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtNewDistrictCode = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtNewDistrict = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lsbDistricts = New System.Windows.Forms.ListBox
        Me.StatusStrip1.SuspendLayout()
        Me.TCDistrictSourcesTool.SuspendLayout()
        Me.TPManageDistricts.SuspendLayout()
        Me.PanelCounties.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TCDistrictAssignmentOptions.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TPNewDistricts.SuspendLayout()
        Me.PanelDistrictChanges.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack})
        Me.MenuItem1.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Back"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem3, Me.mmiClear})
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
        'MenuItem3
        '
        Me.MenuItem3.Index = 3
        Me.MenuItem3.Text = "-"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 4
        Me.mmiClear.Text = "Clear"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
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
        'TBManagingDistricts
        '
        Me.TBManagingDistricts.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbClear, Me.tbbBack})
        Me.TBManagingDistricts.DropDownArrows = True
        Me.TBManagingDistricts.ImageList = Me.Image_List_All
        Me.TBManagingDistricts.Location = New System.Drawing.Point(0, 0)
        Me.TBManagingDistricts.Name = "TBManagingDistricts"
        Me.TBManagingDistricts.ShowToolTips = True
        Me.TBManagingDistricts.Size = New System.Drawing.Size(792, 28)
        Me.TBManagingDistricts.TabIndex = 18
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
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 19
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(769, 17)
        Me.Panel1.Spring = True
        Me.Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 17)
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 17)
        '
        'TCDistrictSourcesTool
        '
        Me.TCDistrictSourcesTool.Controls.Add(Me.TPManageDistricts)
        Me.TCDistrictSourcesTool.Controls.Add(Me.TPNewDistricts)
        Me.TCDistrictSourcesTool.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDistrictSourcesTool.Location = New System.Drawing.Point(0, 28)
        Me.TCDistrictSourcesTool.Name = "TCDistrictSourcesTool"
        Me.TCDistrictSourcesTool.SelectedIndex = 0
        Me.TCDistrictSourcesTool.Size = New System.Drawing.Size(792, 516)
        Me.TCDistrictSourcesTool.TabIndex = 20
        '
        'TPManageDistricts
        '
        Me.TPManageDistricts.Controls.Add(Me.PanelCounties)
        Me.TPManageDistricts.Controls.Add(Me.Splitter1)
        Me.TPManageDistricts.Controls.Add(Me.Panel4)
        Me.TPManageDistricts.Location = New System.Drawing.Point(4, 22)
        Me.TPManageDistricts.Name = "TPManageDistricts"
        Me.TPManageDistricts.Size = New System.Drawing.Size(784, 490)
        Me.TPManageDistricts.TabIndex = 0
        Me.TPManageDistricts.Text = "County/District Assignment(s)"
        '
        'PanelCounties
        '
        Me.PanelCounties.Controls.Add(Me.clbCounties)
        Me.PanelCounties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelCounties.Location = New System.Drawing.Point(0, 117)
        Me.PanelCounties.Name = "PanelCounties"
        Me.PanelCounties.Size = New System.Drawing.Size(784, 373)
        Me.PanelCounties.TabIndex = 6
        '
        'clbCounties
        '
        Me.clbCounties.CheckOnClick = True
        Me.clbCounties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbCounties.Location = New System.Drawing.Point(0, 0)
        Me.clbCounties.MultiColumn = True
        Me.clbCounties.Name = "clbCounties"
        Me.clbCounties.Size = New System.Drawing.Size(784, 364)
        Me.clbCounties.TabIndex = 3
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 112)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(784, 5)
        Me.Splitter1.TabIndex = 5
        Me.Splitter1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.TCDistrictAssignmentOptions)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(784, 112)
        Me.Panel4.TabIndex = 4
        '
        'TCDistrictAssignmentOptions
        '
        Me.TCDistrictAssignmentOptions.Controls.Add(Me.TabPage1)
        Me.TCDistrictAssignmentOptions.Controls.Add(Me.TabPage2)
        Me.TCDistrictAssignmentOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDistrictAssignmentOptions.Location = New System.Drawing.Point(0, 0)
        Me.TCDistrictAssignmentOptions.Name = "TCDistrictAssignmentOptions"
        Me.TCDistrictAssignmentOptions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TCDistrictAssignmentOptions.SelectedIndex = 0
        Me.TCDistrictAssignmentOptions.Size = New System.Drawing.Size(784, 112)
        Me.TCDistrictAssignmentOptions.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnViewDistrict)
        Me.TabPage1.Controls.Add(Me.BtnSaveDistricts)
        Me.TabPage1.Controls.Add(Me.btnClearChecks)
        Me.TabPage1.Controls.Add(Me.btnCheckAllCounties)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.cboDistricts)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(776, 86)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Make District Assignment(s)"
        '
        'btnViewDistrict
        '
        Me.btnViewDistrict.Location = New System.Drawing.Point(389, 9)
        Me.btnViewDistrict.Name = "btnViewDistrict"
        Me.btnViewDistrict.Size = New System.Drawing.Size(87, 20)
        Me.btnViewDistrict.TabIndex = 6
        Me.btnViewDistrict.Text = "View District"
        '
        'BtnSaveDistricts
        '
        Me.BtnSaveDistricts.Location = New System.Drawing.Point(389, 62)
        Me.BtnSaveDistricts.Name = "BtnSaveDistricts"
        Me.BtnSaveDistricts.Size = New System.Drawing.Size(87, 20)
        Me.BtnSaveDistricts.TabIndex = 5
        Me.BtnSaveDistricts.Text = "Save Districts"
        '
        'btnClearChecks
        '
        Me.btnClearChecks.Location = New System.Drawing.Point(93, 62)
        Me.btnClearChecks.Name = "btnClearChecks"
        Me.btnClearChecks.Size = New System.Drawing.Size(80, 20)
        Me.btnClearChecks.TabIndex = 4
        Me.btnClearChecks.Text = "Clear Checks"
        '
        'btnCheckAllCounties
        '
        Me.btnCheckAllCounties.Location = New System.Drawing.Point(7, 62)
        Me.btnCheckAllCounties.Name = "btnCheckAllCounties"
        Me.btnCheckAllCounties.Size = New System.Drawing.Size(73, 21)
        Me.btnCheckAllCounties.TabIndex = 3
        Me.btnCheckAllCounties.Text = "Check All"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Districts"
        '
        'cboDistricts
        '
        Me.cboDistricts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistricts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistricts.Location = New System.Drawing.Point(64, 8)
        Me.cboDistricts.Name = "cboDistricts"
        Me.cboDistricts.Size = New System.Drawing.Size(296, 21)
        Me.cboDistricts.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.llbViewDistricts)
        Me.TabPage2.Controls.Add(Me.clbDistricts)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TabPage2.Size = New System.Drawing.Size(776, 86)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "View District(s)"
        '
        'llbViewDistricts
        '
        Me.llbViewDistricts.AutoSize = True
        Me.llbViewDistricts.Location = New System.Drawing.Point(16, 8)
        Me.llbViewDistricts.Name = "llbViewDistricts"
        Me.llbViewDistricts.Size = New System.Drawing.Size(76, 13)
        Me.llbViewDistricts.TabIndex = 1
        Me.llbViewDistricts.TabStop = True
        Me.llbViewDistricts.Text = "View District(s)"
        '
        'clbDistricts
        '
        Me.clbDistricts.CheckOnClick = True
        Me.clbDistricts.Location = New System.Drawing.Point(120, 8)
        Me.clbDistricts.MultiColumn = True
        Me.clbDistricts.Name = "clbDistricts"
        Me.clbDistricts.Size = New System.Drawing.Size(619, 64)
        Me.clbDistricts.TabIndex = 0
        '
        'TPNewDistricts
        '
        Me.TPNewDistricts.Controls.Add(Me.PanelDistrictChanges)
        Me.TPNewDistricts.Controls.Add(Me.Splitter2)
        Me.TPNewDistricts.Controls.Add(Me.GroupBox1)
        Me.TPNewDistricts.Location = New System.Drawing.Point(4, 22)
        Me.TPNewDistricts.Name = "TPNewDistricts"
        Me.TPNewDistricts.Size = New System.Drawing.Size(784, 490)
        Me.TPNewDistricts.TabIndex = 1
        Me.TPNewDistricts.Text = "Add New District(s)"
        '
        'PanelDistrictChanges
        '
        Me.PanelDistrictChanges.Controls.Add(Me.cboDistrictManager)
        Me.PanelDistrictChanges.Controls.Add(Me.Label5)
        Me.PanelDistrictChanges.Controls.Add(Me.btnAddUpdateInfo)
        Me.PanelDistrictChanges.Controls.Add(Me.cboDistrictToRemove)
        Me.PanelDistrictChanges.Controls.Add(Me.chbRemoveDistrict)
        Me.PanelDistrictChanges.Controls.Add(Me.Label4)
        Me.PanelDistrictChanges.Controls.Add(Me.txtNewDistrictCode)
        Me.PanelDistrictChanges.Controls.Add(Me.Label3)
        Me.PanelDistrictChanges.Controls.Add(Me.txtNewDistrict)
        Me.PanelDistrictChanges.Controls.Add(Me.Label2)
        Me.PanelDistrictChanges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDistrictChanges.Location = New System.Drawing.Point(317, 0)
        Me.PanelDistrictChanges.Name = "PanelDistrictChanges"
        Me.PanelDistrictChanges.Size = New System.Drawing.Size(467, 490)
        Me.PanelDistrictChanges.TabIndex = 2
        '
        'cboDistrictManager
        '
        Me.cboDistrictManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistrictManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistrictManager.Location = New System.Drawing.Point(120, 84)
        Me.cboDistrictManager.Name = "cboDistrictManager"
        Me.cboDistrictManager.Size = New System.Drawing.Size(224, 21)
        Me.cboDistrictManager.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "District Manager:"
        '
        'btnAddUpdateInfo
        '
        Me.btnAddUpdateInfo.AutoSize = True
        Me.btnAddUpdateInfo.Location = New System.Drawing.Point(120, 285)
        Me.btnAddUpdateInfo.Name = "btnAddUpdateInfo"
        Me.btnAddUpdateInfo.Size = New System.Drawing.Size(131, 23)
        Me.btnAddUpdateInfo.TabIndex = 8
        Me.btnAddUpdateInfo.Text = "Add/Update Information"
        Me.btnAddUpdateInfo.UseVisualStyleBackColor = True
        '
        'cboDistrictToRemove
        '
        Me.cboDistrictToRemove.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistrictToRemove.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistrictToRemove.Location = New System.Drawing.Point(120, 184)
        Me.cboDistrictToRemove.Name = "cboDistrictToRemove"
        Me.cboDistrictToRemove.Size = New System.Drawing.Size(224, 21)
        Me.cboDistrictToRemove.TabIndex = 7
        '
        'chbRemoveDistrict
        '
        Me.chbRemoveDistrict.Location = New System.Drawing.Point(120, 208)
        Me.chbRemoveDistrict.Name = "chbRemoveDistrict"
        Me.chbRemoveDistrict.Size = New System.Drawing.Size(224, 24)
        Me.chbRemoveDistrict.TabIndex = 6
        Me.chbRemoveDistrict.Text = "Verify that you want to remove a District"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 186)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "District to Remove:"
        '
        'txtNewDistrictCode
        '
        Me.txtNewDistrictCode.Location = New System.Drawing.Point(120, 48)
        Me.txtNewDistrictCode.MaxLength = 1
        Me.txtNewDistrictCode.Name = "txtNewDistrictCode"
        Me.txtNewDistrictCode.Size = New System.Drawing.Size(32, 20)
        Me.txtNewDistrictCode.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "District Code:"
        '
        'txtNewDistrict
        '
        Me.txtNewDistrict.Location = New System.Drawing.Point(120, 16)
        Me.txtNewDistrict.Name = "txtNewDistrict"
        Me.txtNewDistrict.Size = New System.Drawing.Size(240, 20)
        Me.txtNewDistrict.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "District to Add:"
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter2.Location = New System.Drawing.Point(312, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(5, 490)
        Me.Splitter2.TabIndex = 1
        Me.Splitter2.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lsbDistricts)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 490)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current Districts"
        '
        'lsbDistricts
        '
        Me.lsbDistricts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsbDistricts.Location = New System.Drawing.Point(3, 16)
        Me.lsbDistricts.Name = "lsbDistricts"
        Me.lsbDistricts.Size = New System.Drawing.Size(306, 459)
        Me.lsbDistricts.TabIndex = 0
        '
        'IAIPDistrictSourceTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TCDistrictSourcesTool)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TBManagingDistricts)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPDistrictSourceTool"
        Me.Text = "IAIP District Source Tool"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TCDistrictSourcesTool.ResumeLayout(False)
        Me.TPManageDistricts.ResumeLayout(False)
        Me.PanelCounties.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.TCDistrictAssignmentOptions.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TPNewDistricts.ResumeLayout(False)
        Me.PanelDistrictChanges.ResumeLayout(False)
        Me.PanelDistrictChanges.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TBManagingDistricts As System.Windows.Forms.ToolBar
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TCDistrictSourcesTool As System.Windows.Forms.TabControl
    Friend WithEvents TPManageDistricts As System.Windows.Forms.TabPage
    Friend WithEvents PanelCounties As System.Windows.Forms.Panel
    Friend WithEvents clbCounties As System.Windows.Forms.CheckedListBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TCDistrictAssignmentOptions As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents BtnSaveDistricts As System.Windows.Forms.Button
    Friend WithEvents btnClearChecks As System.Windows.Forms.Button
    Friend WithEvents btnCheckAllCounties As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDistricts As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents llbViewDistricts As System.Windows.Forms.LinkLabel
    Friend WithEvents clbDistricts As System.Windows.Forms.CheckedListBox
    Friend WithEvents TPNewDistricts As System.Windows.Forms.TabPage
    Friend WithEvents PanelDistrictChanges As System.Windows.Forms.Panel
    Friend WithEvents cboDistrictToRemove As System.Windows.Forms.ComboBox
    Friend WithEvents chbRemoveDistrict As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewDistrictCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNewDistrict As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lsbDistricts As System.Windows.Forms.ListBox
    Friend WithEvents btnAddUpdateInfo As System.Windows.Forms.Button
    Friend WithEvents cboDistrictManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnViewDistrict As System.Windows.Forms.Button
End Class
