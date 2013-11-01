<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPFacilityLookUpTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPFacilityLookUpTool))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem13 = New System.Windows.Forms.MenuItem
        Me.mmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBWork_Entry = New System.Windows.Forms.ToolBar
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.tbbCut = New System.Windows.Forms.ToolBarButton
        Me.tbCopy = New System.Windows.Forms.ToolBarButton
        Me.tbbPaste = New System.Windows.Forms.ToolBarButton
        Me.tcSearchOptions = New System.Windows.Forms.TabControl
        Me.tpFacilityName = New System.Windows.Forms.TabPage
        Me.chbHistoricalNames = New System.Windows.Forms.CheckBox
        Me.btnFacilityNameSearch = New System.Windows.Forms.Button
        Me.txtFacilityNameSearch = New System.Windows.Forms.TextBox
        Me.Label69 = New System.Windows.Forms.Label
        Me.tpAIRSNumber = New System.Windows.Forms.TabPage
        Me.btnAIRSNumberSearch = New System.Windows.Forms.Button
        Me.txtAIRSNumberSearch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.tpComplianceSearch = New System.Windows.Forms.TabPage
        Me.btnComplianceSearch = New System.Windows.Forms.Button
        Me.txtComplianceEngineer = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tpCity = New System.Windows.Forms.TabPage
        Me.btnCitySearch = New System.Windows.Forms.Button
        Me.txtCityNameSearch = New System.Windows.Forms.TextBox
        Me.Label64 = New System.Windows.Forms.Label
        Me.tpZipCode = New System.Windows.Forms.TabPage
        Me.btnZipCodeSearch = New System.Windows.Forms.Button
        Me.txtZipCodeSearch = New System.Windows.Forms.TextBox
        Me.Label65 = New System.Windows.Forms.Label
        Me.tpSIC = New System.Windows.Forms.TabPage
        Me.btnSICCodeSearch = New System.Windows.Forms.Button
        Me.txtSICCodeSearch = New System.Windows.Forms.TextBox
        Me.Label70 = New System.Windows.Forms.Label
        Me.tpCounty = New System.Windows.Forms.TabPage
        Me.txtCountyNameSearch = New System.Windows.Forms.TextBox
        Me.btnCountySearch = New System.Windows.Forms.Button
        Me.Label63 = New System.Windows.Forms.Label
        Me.tpSubpart = New System.Windows.Forms.TabPage
        Me.rdbGASIP = New System.Windows.Forms.RadioButton
        Me.rdbPart63 = New System.Windows.Forms.RadioButton
        Me.rdbPart60 = New System.Windows.Forms.RadioButton
        Me.rdbPart61 = New System.Windows.Forms.RadioButton
        Me.txtSubpartSearch = New System.Windows.Forms.TextBox
        Me.btnSubpartSearch = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.dgvPossibleMatches = New System.Windows.Forms.DataGridView
        Me.Label68 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.Label72 = New System.Windows.Forms.Label
        Me.Label67 = New System.Windows.Forms.Label
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.btnSelectAIRSNumber = New System.Windows.Forms.Button
        Me.StatusStrip1.SuspendLayout()
        Me.tcSearchOptions.SuspendLayout()
        Me.tpFacilityName.SuspendLayout()
        Me.tpAIRSNumber.SuspendLayout()
        Me.tpComplianceSearch.SuspendLayout()
        Me.tpCity.SuspendLayout()
        Me.tpZipCode.SuspendLayout()
        Me.tpSIC.SuspendLayout()
        Me.tpCounty.SuspendLayout()
        Me.tpSubpart.SuspendLayout()
        CType(Me.dgvPossibleMatches, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 423)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(857, 22)
        Me.StatusStrip1.TabIndex = 203
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 17)
        Me.Panel1.Spring = True
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
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiBack, Me.MenuItem13, Me.mmiExit})
        Me.MenuItem1.Text = "File"
        '
        'mmiBack
        '
        Me.mmiBack.Index = 0
        Me.mmiBack.Text = "Close"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 1
        Me.MenuItem13.Text = "-"
        '
        'mmiExit
        '
        Me.mmiExit.Index = 2
        Me.mmiExit.Text = "Exit"
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
        Me.mmiCopy.Text = "Copy "
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
        'TBWork_Entry
        '
        Me.TBWork_Entry.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbClear, Me.tbbBack, Me.tbbCut, Me.tbCopy, Me.tbbPaste})
        Me.TBWork_Entry.DropDownArrows = True
        Me.TBWork_Entry.ImageList = Me.Image_List_All
        Me.TBWork_Entry.Location = New System.Drawing.Point(0, 0)
        Me.TBWork_Entry.Name = "TBWork_Entry"
        Me.TBWork_Entry.ShowToolTips = True
        Me.TBWork_Entry.Size = New System.Drawing.Size(857, 28)
        Me.TBWork_Entry.TabIndex = 204
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        '
        'tbbCut
        '
        Me.tbbCut.ImageIndex = 9
        Me.tbbCut.Name = "tbbCut"
        Me.tbbCut.ToolTipText = "Cut"
        '
        'tbCopy
        '
        Me.tbCopy.ImageIndex = 8
        Me.tbCopy.Name = "tbCopy"
        Me.tbCopy.ToolTipText = "Copy"
        '
        'tbbPaste
        '
        Me.tbbPaste.ImageIndex = 50
        Me.tbbPaste.Name = "tbbPaste"
        Me.tbbPaste.ToolTipText = "Paste"
        '
        'tcSearchOptions
        '
        Me.tcSearchOptions.Controls.Add(Me.tpFacilityName)
        Me.tcSearchOptions.Controls.Add(Me.tpAIRSNumber)
        Me.tcSearchOptions.Controls.Add(Me.tpComplianceSearch)
        Me.tcSearchOptions.Controls.Add(Me.tpCity)
        Me.tcSearchOptions.Controls.Add(Me.tpZipCode)
        Me.tcSearchOptions.Controls.Add(Me.tpSIC)
        Me.tcSearchOptions.Controls.Add(Me.tpCounty)
        Me.tcSearchOptions.Controls.Add(Me.tpSubpart)
        Me.tcSearchOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.tcSearchOptions.Location = New System.Drawing.Point(0, 28)
        Me.tcSearchOptions.Name = "tcSearchOptions"
        Me.tcSearchOptions.SelectedIndex = 0
        Me.tcSearchOptions.Size = New System.Drawing.Size(857, 104)
        Me.tcSearchOptions.TabIndex = 205
        '
        'tpFacilityName
        '
        Me.tpFacilityName.Controls.Add(Me.chbHistoricalNames)
        Me.tpFacilityName.Controls.Add(Me.btnFacilityNameSearch)
        Me.tpFacilityName.Controls.Add(Me.txtFacilityNameSearch)
        Me.tpFacilityName.Controls.Add(Me.Label69)
        Me.tpFacilityName.Location = New System.Drawing.Point(4, 22)
        Me.tpFacilityName.Name = "tpFacilityName"
        Me.tpFacilityName.Size = New System.Drawing.Size(849, 78)
        Me.tpFacilityName.TabIndex = 1
        Me.tpFacilityName.Text = "Facility Name Search"
        Me.tpFacilityName.UseVisualStyleBackColor = True
        '
        'chbHistoricalNames
        '
        Me.chbHistoricalNames.AutoSize = True
        Me.chbHistoricalNames.Location = New System.Drawing.Point(136, 34)
        Me.chbHistoricalNames.Name = "chbHistoricalNames"
        Me.chbHistoricalNames.Size = New System.Drawing.Size(105, 17)
        Me.chbHistoricalNames.TabIndex = 24
        Me.chbHistoricalNames.Text = "Historical Names"
        Me.chbHistoricalNames.UseVisualStyleBackColor = True
        '
        'btnFacilityNameSearch
        '
        Me.btnFacilityNameSearch.Location = New System.Drawing.Point(300, 8)
        Me.btnFacilityNameSearch.Name = "btnFacilityNameSearch"
        Me.btnFacilityNameSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnFacilityNameSearch.TabIndex = 23
        Me.btnFacilityNameSearch.Text = "Search"
        '
        'txtFacilityNameSearch
        '
        Me.txtFacilityNameSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtFacilityNameSearch.Name = "txtFacilityNameSearch"
        Me.txtFacilityNameSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtFacilityNameSearch.TabIndex = 20
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(4, 8)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(125, 13)
        Me.Label69.TabIndex = 17
        Me.Label69.Text = "Facility Name Search By:"
        '
        'tpAIRSNumber
        '
        Me.tpAIRSNumber.Controls.Add(Me.btnAIRSNumberSearch)
        Me.tpAIRSNumber.Controls.Add(Me.txtAIRSNumberSearch)
        Me.tpAIRSNumber.Controls.Add(Me.Label1)
        Me.tpAIRSNumber.Location = New System.Drawing.Point(4, 22)
        Me.tpAIRSNumber.Name = "tpAIRSNumber"
        Me.tpAIRSNumber.Size = New System.Drawing.Size(849, 78)
        Me.tpAIRSNumber.TabIndex = 0
        Me.tpAIRSNumber.Text = "AIRS Number Search"
        Me.tpAIRSNumber.UseVisualStyleBackColor = True
        '
        'btnAIRSNumberSearch
        '
        Me.btnAIRSNumberSearch.Location = New System.Drawing.Point(300, 8)
        Me.btnAIRSNumberSearch.Name = "btnAIRSNumberSearch"
        Me.btnAIRSNumberSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnAIRSNumberSearch.TabIndex = 14
        Me.btnAIRSNumberSearch.Text = "Search"
        '
        'txtAIRSNumberSearch
        '
        Me.txtAIRSNumberSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtAIRSNumberSearch.Name = "txtAIRSNumberSearch"
        Me.txtAIRSNumberSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtAIRSNumberSearch.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "AIRS Number Search By:"
        '
        'tpComplianceSearch
        '
        Me.tpComplianceSearch.Controls.Add(Me.btnComplianceSearch)
        Me.tpComplianceSearch.Controls.Add(Me.txtComplianceEngineer)
        Me.tpComplianceSearch.Controls.Add(Me.Label2)
        Me.tpComplianceSearch.Location = New System.Drawing.Point(4, 22)
        Me.tpComplianceSearch.Name = "tpComplianceSearch"
        Me.tpComplianceSearch.Size = New System.Drawing.Size(849, 78)
        Me.tpComplianceSearch.TabIndex = 8
        Me.tpComplianceSearch.Text = "Compliance Search"
        Me.tpComplianceSearch.UseVisualStyleBackColor = True
        '
        'btnComplianceSearch
        '
        Me.btnComplianceSearch.Location = New System.Drawing.Point(300, 8)
        Me.btnComplianceSearch.Name = "btnComplianceSearch"
        Me.btnComplianceSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnComplianceSearch.TabIndex = 17
        Me.btnComplianceSearch.Text = "Search"
        '
        'txtComplianceEngineer
        '
        Me.txtComplianceEngineer.Location = New System.Drawing.Point(136, 8)
        Me.txtComplianceEngineer.Name = "txtComplianceEngineer"
        Me.txtComplianceEngineer.Size = New System.Drawing.Size(160, 20)
        Me.txtComplianceEngineer.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Compliance Engineer:"
        '
        'tpCity
        '
        Me.tpCity.Controls.Add(Me.btnCitySearch)
        Me.tpCity.Controls.Add(Me.txtCityNameSearch)
        Me.tpCity.Controls.Add(Me.Label64)
        Me.tpCity.Location = New System.Drawing.Point(4, 22)
        Me.tpCity.Name = "tpCity"
        Me.tpCity.Size = New System.Drawing.Size(849, 78)
        Me.tpCity.TabIndex = 3
        Me.tpCity.Text = "City Search"
        Me.tpCity.UseVisualStyleBackColor = True
        '
        'btnCitySearch
        '
        Me.btnCitySearch.Location = New System.Drawing.Point(300, 8)
        Me.btnCitySearch.Name = "btnCitySearch"
        Me.btnCitySearch.Size = New System.Drawing.Size(78, 23)
        Me.btnCitySearch.TabIndex = 26
        Me.btnCitySearch.Text = "Search"
        '
        'txtCityNameSearch
        '
        Me.txtCityNameSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtCityNameSearch.Name = "txtCityNameSearch"
        Me.txtCityNameSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtCityNameSearch.TabIndex = 25
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(4, 8)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(79, 13)
        Me.Label64.TabIndex = 24
        Me.Label64.Text = "City Search By:"
        '
        'tpZipCode
        '
        Me.tpZipCode.Controls.Add(Me.btnZipCodeSearch)
        Me.tpZipCode.Controls.Add(Me.txtZipCodeSearch)
        Me.tpZipCode.Controls.Add(Me.Label65)
        Me.tpZipCode.Location = New System.Drawing.Point(4, 22)
        Me.tpZipCode.Name = "tpZipCode"
        Me.tpZipCode.Size = New System.Drawing.Size(849, 78)
        Me.tpZipCode.TabIndex = 4
        Me.tpZipCode.Text = "Zip Code Search"
        Me.tpZipCode.UseVisualStyleBackColor = True
        '
        'btnZipCodeSearch
        '
        Me.btnZipCodeSearch.Location = New System.Drawing.Point(300, 8)
        Me.btnZipCodeSearch.Name = "btnZipCodeSearch"
        Me.btnZipCodeSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnZipCodeSearch.TabIndex = 29
        Me.btnZipCodeSearch.Text = "Search"
        '
        'txtZipCodeSearch
        '
        Me.txtZipCodeSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtZipCodeSearch.Name = "txtZipCodeSearch"
        Me.txtZipCodeSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtZipCodeSearch.TabIndex = 28
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(4, 8)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(105, 13)
        Me.Label65.TabIndex = 27
        Me.Label65.Text = "Zip Code Search By:"
        '
        'tpSIC
        '
        Me.tpSIC.Controls.Add(Me.btnSICCodeSearch)
        Me.tpSIC.Controls.Add(Me.txtSICCodeSearch)
        Me.tpSIC.Controls.Add(Me.Label70)
        Me.tpSIC.Location = New System.Drawing.Point(4, 22)
        Me.tpSIC.Name = "tpSIC"
        Me.tpSIC.Size = New System.Drawing.Size(849, 78)
        Me.tpSIC.TabIndex = 6
        Me.tpSIC.Text = "SIC Code Search"
        Me.tpSIC.UseVisualStyleBackColor = True
        '
        'btnSICCodeSearch
        '
        Me.btnSICCodeSearch.Location = New System.Drawing.Point(300, 8)
        Me.btnSICCodeSearch.Name = "btnSICCodeSearch"
        Me.btnSICCodeSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnSICCodeSearch.TabIndex = 29
        Me.btnSICCodeSearch.Text = "Search"
        '
        'txtSICCodeSearch
        '
        Me.txtSICCodeSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtSICCodeSearch.Name = "txtSICCodeSearch"
        Me.txtSICCodeSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtSICCodeSearch.TabIndex = 28
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(4, 8)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(107, 13)
        Me.Label70.TabIndex = 27
        Me.Label70.Text = "SIC Code Search By:"
        '
        'tpCounty
        '
        Me.tpCounty.Controls.Add(Me.txtCountyNameSearch)
        Me.tpCounty.Controls.Add(Me.btnCountySearch)
        Me.tpCounty.Controls.Add(Me.Label63)
        Me.tpCounty.Location = New System.Drawing.Point(4, 22)
        Me.tpCounty.Name = "tpCounty"
        Me.tpCounty.Size = New System.Drawing.Size(849, 78)
        Me.tpCounty.TabIndex = 2
        Me.tpCounty.Text = "County Search"
        Me.tpCounty.UseVisualStyleBackColor = True
        '
        'txtCountyNameSearch
        '
        Me.txtCountyNameSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtCountyNameSearch.Name = "txtCountyNameSearch"
        Me.txtCountyNameSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtCountyNameSearch.TabIndex = 27
        '
        'btnCountySearch
        '
        Me.btnCountySearch.Location = New System.Drawing.Point(300, 8)
        Me.btnCountySearch.Name = "btnCountySearch"
        Me.btnCountySearch.Size = New System.Drawing.Size(78, 23)
        Me.btnCountySearch.TabIndex = 26
        Me.btnCountySearch.Text = "Search"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(4, 8)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(95, 13)
        Me.Label63.TabIndex = 24
        Me.Label63.Text = "County Search By:"
        '
        'tpSubpart
        '
        Me.tpSubpart.Controls.Add(Me.rdbGASIP)
        Me.tpSubpart.Controls.Add(Me.rdbPart63)
        Me.tpSubpart.Controls.Add(Me.rdbPart60)
        Me.tpSubpart.Controls.Add(Me.rdbPart61)
        Me.tpSubpart.Controls.Add(Me.txtSubpartSearch)
        Me.tpSubpart.Controls.Add(Me.btnSubpartSearch)
        Me.tpSubpart.Controls.Add(Me.Label3)
        Me.tpSubpart.Location = New System.Drawing.Point(4, 22)
        Me.tpSubpart.Name = "tpSubpart"
        Me.tpSubpart.Size = New System.Drawing.Size(849, 78)
        Me.tpSubpart.TabIndex = 9
        Me.tpSubpart.Text = "Subpart "
        Me.tpSubpart.UseVisualStyleBackColor = True
        '
        'rdbGASIP
        '
        Me.rdbGASIP.AutoSize = True
        Me.rdbGASIP.Location = New System.Drawing.Point(266, 57)
        Me.rdbGASIP.Name = "rdbGASIP"
        Me.rdbGASIP.Size = New System.Drawing.Size(86, 17)
        Me.rdbGASIP.TabIndex = 37
        Me.rdbGASIP.TabStop = True
        Me.rdbGASIP.Text = "SIP - GA SIP"
        Me.rdbGASIP.UseVisualStyleBackColor = True
        '
        'rdbPart63
        '
        Me.rdbPart63.AutoSize = True
        Me.rdbPart63.Location = New System.Drawing.Point(136, 57)
        Me.rdbPart63.Name = "rdbPart63"
        Me.rdbPart63.Size = New System.Drawing.Size(116, 17)
        Me.rdbPart63.TabIndex = 36
        Me.rdbPart63.TabStop = True
        Me.rdbPart63.Text = "M - MACT (Part 63)"
        Me.rdbPart63.UseVisualStyleBackColor = True
        '
        'rdbPart60
        '
        Me.rdbPart60.AutoSize = True
        Me.rdbPart60.Location = New System.Drawing.Point(266, 34)
        Me.rdbPart60.Name = "rdbPart60"
        Me.rdbPart60.Size = New System.Drawing.Size(112, 17)
        Me.rdbPart60.TabIndex = 35
        Me.rdbPart60.TabStop = True
        Me.rdbPart60.Text = "9 - NSPS (Part 60)"
        Me.rdbPart60.UseVisualStyleBackColor = True
        '
        'rdbPart61
        '
        Me.rdbPart61.AutoSize = True
        Me.rdbPart61.Checked = True
        Me.rdbPart61.Location = New System.Drawing.Point(136, 34)
        Me.rdbPart61.Name = "rdbPart61"
        Me.rdbPart61.Size = New System.Drawing.Size(127, 17)
        Me.rdbPart61.TabIndex = 34
        Me.rdbPart61.TabStop = True
        Me.rdbPart61.Text = "8 - NESHAP (Part 61)"
        Me.rdbPart61.UseVisualStyleBackColor = True
        '
        'txtSubpartSearch
        '
        Me.txtSubpartSearch.Location = New System.Drawing.Point(136, 8)
        Me.txtSubpartSearch.Name = "txtSubpartSearch"
        Me.txtSubpartSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtSubpartSearch.TabIndex = 30
        '
        'btnSubpartSearch
        '
        Me.btnSubpartSearch.Location = New System.Drawing.Point(300, 8)
        Me.btnSubpartSearch.Name = "btnSubpartSearch"
        Me.btnSubpartSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnSubpartSearch.TabIndex = 29
        Me.btnSubpartSearch.Text = "Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Subpart Search By:"
        '
        'dgvPossibleMatches
        '
        Me.dgvPossibleMatches.AllowUserToOrderColumns = True
        Me.dgvPossibleMatches.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPossibleMatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPossibleMatches.Location = New System.Drawing.Point(0, 198)
        Me.dgvPossibleMatches.Name = "dgvPossibleMatches"
        Me.dgvPossibleMatches.ReadOnly = True
        Me.dgvPossibleMatches.Size = New System.Drawing.Size(857, 222)
        Me.dgvPossibleMatches.TabIndex = 218
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(8, 150)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(104, 13)
        Me.Label68.TabIndex = 212
        Me.Label68.Text = "Facility (AIRS) Name"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(144, 158)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(192, 20)
        Me.txtFacilityName.TabIndex = 215
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(8, 182)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(195, 13)
        Me.Label72.TabIndex = 216
        Me.Label72.Text = "Possible Matches to Your Search"
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(8, 134)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(72, 13)
        Me.Label67.TabIndex = 213
        Me.Label67.Text = "AIRS Number"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(144, 134)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(192, 20)
        Me.txtAIRSNumber.TabIndex = 214
        '
        'btnSelectAIRSNumber
        '
        Me.btnSelectAIRSNumber.Location = New System.Drawing.Point(352, 134)
        Me.btnSelectAIRSNumber.Name = "btnSelectAIRSNumber"
        Me.btnSelectAIRSNumber.Size = New System.Drawing.Size(40, 40)
        Me.btnSelectAIRSNumber.TabIndex = 217
        Me.btnSelectAIRSNumber.Text = "Use"
        '
        'IAIPFacilityLookUpTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 445)
        Me.Controls.Add(Me.dgvPossibleMatches)
        Me.Controls.Add(Me.Label68)
        Me.Controls.Add(Me.txtFacilityName)
        Me.Controls.Add(Me.Label72)
        Me.Controls.Add(Me.Label67)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.btnSelectAIRSNumber)
        Me.Controls.Add(Me.tcSearchOptions)
        Me.Controls.Add(Me.TBWork_Entry)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPFacilityLookUpTool"
        Me.Text = "IAIP Facility Lookup Tool"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.tcSearchOptions.ResumeLayout(False)
        Me.tpFacilityName.ResumeLayout(False)
        Me.tpFacilityName.PerformLayout()
        Me.tpAIRSNumber.ResumeLayout(False)
        Me.tpAIRSNumber.PerformLayout()
        Me.tpComplianceSearch.ResumeLayout(False)
        Me.tpComplianceSearch.PerformLayout()
        Me.tpCity.ResumeLayout(False)
        Me.tpCity.PerformLayout()
        Me.tpZipCode.ResumeLayout(False)
        Me.tpZipCode.PerformLayout()
        Me.tpSIC.ResumeLayout(False)
        Me.tpSIC.PerformLayout()
        Me.tpCounty.ResumeLayout(False)
        Me.tpCounty.PerformLayout()
        Me.tpSubpart.ResumeLayout(False)
        Me.tpSubpart.PerformLayout()
        CType(Me.dgvPossibleMatches, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TBWork_Entry As System.Windows.Forms.ToolBar
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents tcSearchOptions As System.Windows.Forms.TabControl
    Friend WithEvents tpAIRSNumber As System.Windows.Forms.TabPage
    Friend WithEvents btnAIRSNumberSearch As System.Windows.Forms.Button
    Friend WithEvents txtAIRSNumberSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tpComplianceSearch As System.Windows.Forms.TabPage
    Friend WithEvents btnComplianceSearch As System.Windows.Forms.Button
    Friend WithEvents txtComplianceEngineer As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tpCity As System.Windows.Forms.TabPage
    Friend WithEvents btnCitySearch As System.Windows.Forms.Button
    Friend WithEvents txtCityNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents tpZipCode As System.Windows.Forms.TabPage
    Friend WithEvents btnZipCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtZipCodeSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents tpSIC As System.Windows.Forms.TabPage
    Friend WithEvents btnSICCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSICCodeSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents tpCounty As System.Windows.Forms.TabPage
    Friend WithEvents txtCountyNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnCountySearch As System.Windows.Forms.Button
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents tpFacilityName As System.Windows.Forms.TabPage
    Friend WithEvents btnFacilityNameSearch As System.Windows.Forms.Button
    Friend WithEvents txtFacilityNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents dgvPossibleMatches As System.Windows.Forms.DataGridView
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents chbHistoricalNames As System.Windows.Forms.CheckBox
    Friend WithEvents tpSubpart As System.Windows.Forms.TabPage
    Friend WithEvents txtSubpartSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSubpartSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbPart63 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPart60 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPart61 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGASIP As System.Windows.Forms.RadioButton
    Friend WithEvents tbbCut As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbCopy As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbPaste As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
End Class
