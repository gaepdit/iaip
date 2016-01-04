<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSPPApplicationLog
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiFile = New System.Windows.Forms.MenuItem()
        Me.mmiClose = New System.Windows.Forms.MenuItem()
        Me.mmiTools = New System.Windows.Forms.MenuItem()
        Me.mmiNewApplication = New System.Windows.Forms.MenuItem()
        Me.mmiOpen = New System.Windows.Forms.MenuItem()
        Me.mmiResetSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.mmiExport = New System.Windows.Forms.MenuItem()
        Me.mmiHelp = New System.Windows.Forms.MenuItem()
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem()
        Me.cboSortOrder2 = New System.Windows.Forms.ComboBox()
        Me.cboSort2 = New System.Windows.Forms.ComboBox()
        Me.cboSortOrder1 = New System.Windows.Forms.ComboBox()
        Me.cboSort1 = New System.Windows.Forms.ComboBox()
        Me.cboApplicationStatus = New System.Windows.Forms.ComboBox()
        Me.cboApplicationUnit = New System.Windows.Forms.ComboBox()
        Me.cboApplicationType = New System.Windows.Forms.ComboBox()
        Me.cboFieldType2 = New System.Windows.Forms.ComboBox()
        Me.cboFieldType1 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.DTPSearchDate2 = New System.Windows.Forms.DateTimePicker()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.DTPSearchDate1 = New System.Windows.Forms.DateTimePicker()
        Me.btnResetSearch = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.txtSearchText2 = New System.Windows.Forms.TextBox()
        Me.txtSearchText1 = New System.Windows.Forms.TextBox()
        Me.chbShowAll = New System.Windows.Forms.CheckBox()
        Me.cboEngineer = New System.Windows.Forms.ComboBox()
        Me.cboSearchText1 = New System.Windows.Forms.ComboBox()
        Me.cboSearchText2 = New System.Windows.Forms.ComboBox()
        Me.dgvApplicationLog = New System.Windows.Forms.DataGridView()
        Me.DTPSearchDate1b = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SortGroupBox = New System.Windows.Forms.GroupBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.cboMACT1 = New System.Windows.Forms.ComboBox()
        Me.cboNSPS1 = New System.Windows.Forms.ComboBox()
        Me.cboMACT2 = New System.Windows.Forms.ComboBox()
        Me.cboNSPS2 = New System.Windows.Forms.ComboBox()
        Me.cboNESHAP2 = New System.Windows.Forms.ComboBox()
        Me.cboNESHAP1 = New System.Windows.Forms.ComboBox()
        Me.cboSIP2 = New System.Windows.Forms.ComboBox()
        Me.cboSIP1 = New System.Windows.Forms.ComboBox()
        Me.DTPSearchDate2b = New System.Windows.Forms.DateTimePicker()
        Me.pnlDataGridView = New System.Windows.Forms.Panel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.bgwApplicationLog = New System.ComponentModel.BackgroundWorker()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.dgvApplicationLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SortGroupBox.SuspendLayout()
        Me.pnlDataGridView.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiFile, Me.mmiTools, Me.mmiHelp})
        '
        'mmiFile
        '
        Me.mmiFile.Index = 0
        Me.mmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiClose})
        Me.mmiFile.Text = "&File"
        '
        'mmiClose
        '
        Me.mmiClose.Index = 0
        Me.mmiClose.Shortcut = System.Windows.Forms.Shortcut.CtrlW
        Me.mmiClose.Text = "&Close"
        '
        'mmiTools
        '
        Me.mmiTools.Index = 1
        Me.mmiTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiNewApplication, Me.mmiOpen, Me.mmiResetSearch, Me.MenuItem8, Me.mmiExport})
        Me.mmiTools.Text = "&Tools"
        '
        'mmiNewApplication
        '
        Me.mmiNewApplication.Index = 0
        Me.mmiNewApplication.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.mmiNewApplication.Text = "Start &New Application"
        Me.mmiNewApplication.Visible = False
        '
        'mmiOpen
        '
        Me.mmiOpen.Index = 1
        Me.mmiOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.mmiOpen.Text = "&Open Selected Application"
        '
        'mmiResetSearch
        '
        Me.mmiResetSearch.Index = 2
        Me.mmiResetSearch.Shortcut = System.Windows.Forms.Shortcut.CtrlR
        Me.mmiResetSearch.Text = "&Reset Search Form"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 3
        Me.MenuItem8.Text = "-"
        '
        'mmiExport
        '
        Me.mmiExport.Index = 4
        Me.mmiExport.Shortcut = System.Windows.Forms.Shortcut.CtrlE
        Me.mmiExport.Text = "&Export to Excel"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp})
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online &Help"
        '
        'cboSortOrder2
        '
        Me.cboSortOrder2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSortOrder2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSortOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortOrder2.Location = New System.Drawing.Point(156, 42)
        Me.cboSortOrder2.Name = "cboSortOrder2"
        Me.cboSortOrder2.Size = New System.Drawing.Size(121, 21)
        Me.cboSortOrder2.TabIndex = 11
        '
        'cboSort2
        '
        Me.cboSort2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSort2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSort2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSort2.Location = New System.Drawing.Point(28, 42)
        Me.cboSort2.Name = "cboSort2"
        Me.cboSort2.Size = New System.Drawing.Size(121, 21)
        Me.cboSort2.TabIndex = 10
        '
        'cboSortOrder1
        '
        Me.cboSortOrder1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSortOrder1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSortOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortOrder1.Location = New System.Drawing.Point(161, 81)
        Me.cboSortOrder1.Name = "cboSortOrder1"
        Me.cboSortOrder1.Size = New System.Drawing.Size(121, 21)
        Me.cboSortOrder1.TabIndex = 9
        '
        'cboSort1
        '
        Me.cboSort1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSort1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSort1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSort1.Location = New System.Drawing.Point(33, 81)
        Me.cboSort1.Name = "cboSort1"
        Me.cboSort1.Size = New System.Drawing.Size(121, 21)
        Me.cboSort1.TabIndex = 8
        '
        'cboApplicationStatus
        '
        Me.cboApplicationStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboApplicationStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboApplicationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApplicationStatus.Location = New System.Drawing.Point(679, 67)
        Me.cboApplicationStatus.Name = "cboApplicationStatus"
        Me.cboApplicationStatus.Size = New System.Drawing.Size(121, 21)
        Me.cboApplicationStatus.TabIndex = 6
        '
        'cboApplicationUnit
        '
        Me.cboApplicationUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboApplicationUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboApplicationUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApplicationUnit.Location = New System.Drawing.Point(679, 40)
        Me.cboApplicationUnit.Name = "cboApplicationUnit"
        Me.cboApplicationUnit.Size = New System.Drawing.Size(121, 21)
        Me.cboApplicationUnit.TabIndex = 5
        '
        'cboApplicationType
        '
        Me.cboApplicationType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboApplicationType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboApplicationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApplicationType.Location = New System.Drawing.Point(679, 13)
        Me.cboApplicationType.Name = "cboApplicationType"
        Me.cboApplicationType.Size = New System.Drawing.Size(121, 21)
        Me.cboApplicationType.TabIndex = 4
        '
        'cboFieldType2
        '
        Me.cboFieldType2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFieldType2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFieldType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFieldType2.Location = New System.Drawing.Point(367, 13)
        Me.cboFieldType2.Name = "cboFieldType2"
        Me.cboFieldType2.Size = New System.Drawing.Size(214, 21)
        Me.cboFieldType2.TabIndex = 2
        '
        'cboFieldType1
        '
        Me.cboFieldType1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFieldType1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFieldType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFieldType1.Location = New System.Drawing.Point(75, 13)
        Me.cboFieldType1.Name = "cboFieldType1"
        Me.cboFieldType1.Size = New System.Drawing.Size(215, 21)
        Me.cboFieldType1.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(587, 97)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Engineer"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(587, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Application Status"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(587, 43)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Application Unit"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(587, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Application Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(19, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "(2)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "(1)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(296, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Search Text"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(296, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Field Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Search Text"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Field Type"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2})
        Me.MenuItem1.Text = "View"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Reset"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "Back"
        '
        'btnOpen
        '
        Me.btnOpen.Enabled = False
        Me.btnOpen.Location = New System.Drawing.Point(299, 107)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(160, 23)
        Me.btnOpen.TabIndex = 15
        Me.btnOpen.Text = "View Selected Application"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.Text = "Add New Application"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = -1
        Me.MenuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4})
        Me.MenuItem5.Text = "Tracking Log"
        Me.MenuItem5.Visible = False
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = -1
        Me.MenuItem6.Text = "Help"
        '
        'DTPSearchDate2
        '
        Me.DTPSearchDate2.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate2.Location = New System.Drawing.Point(367, 40)
        Me.DTPSearchDate2.Name = "DTPSearchDate2"
        Me.DTPSearchDate2.Size = New System.Drawing.Size(100, 20)
        Me.DTPSearchDate2.TabIndex = 3
        Me.DTPSearchDate2.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPSearchDate2.Visible = False
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = -1
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        Me.MenuItem7.Text = "File"
        '
        'DTPSearchDate1
        '
        Me.DTPSearchDate1.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate1.Location = New System.Drawing.Point(75, 40)
        Me.DTPSearchDate1.Name = "DTPSearchDate1"
        Me.DTPSearchDate1.Size = New System.Drawing.Size(100, 20)
        Me.DTPSearchDate1.TabIndex = 1
        Me.DTPSearchDate1.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPSearchDate1.Visible = False
        '
        'btnResetSearch
        '
        Me.btnResetSearch.Location = New System.Drawing.Point(382, 73)
        Me.btnResetSearch.Name = "btnResetSearch"
        Me.btnResetSearch.Size = New System.Drawing.Size(77, 30)
        Me.btnResetSearch.TabIndex = 14
        Me.btnResetSearch.Text = "Reset Form"
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(299, 73)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(77, 30)
        Me.btnFind.TabIndex = 13
        Me.btnFind.Text = "Search"
        '
        'txtSearchText2
        '
        Me.txtSearchText2.Location = New System.Drawing.Point(367, 40)
        Me.txtSearchText2.Name = "txtSearchText2"
        Me.txtSearchText2.Size = New System.Drawing.Size(214, 20)
        Me.txtSearchText2.TabIndex = 3
        '
        'txtSearchText1
        '
        Me.txtSearchText1.Location = New System.Drawing.Point(76, 40)
        Me.txtSearchText1.Name = "txtSearchText1"
        Me.txtSearchText1.Size = New System.Drawing.Size(214, 20)
        Me.txtSearchText1.TabIndex = 1
        '
        'chbShowAll
        '
        Me.chbShowAll.AutoSize = True
        Me.chbShowAll.Location = New System.Drawing.Point(477, 80)
        Me.chbShowAll.Name = "chbShowAll"
        Me.chbShowAll.Size = New System.Drawing.Size(97, 17)
        Me.chbShowAll.TabIndex = 12
        Me.chbShowAll.Text = "Show All Fields"
        '
        'cboEngineer
        '
        Me.cboEngineer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEngineer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEngineer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEngineer.Location = New System.Drawing.Point(679, 94)
        Me.cboEngineer.Name = "cboEngineer"
        Me.cboEngineer.Size = New System.Drawing.Size(121, 21)
        Me.cboEngineer.TabIndex = 7
        '
        'cboSearchText1
        '
        Me.cboSearchText1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSearchText1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchText1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchText1.FormattingEnabled = True
        Me.cboSearchText1.Location = New System.Drawing.Point(76, 40)
        Me.cboSearchText1.Name = "cboSearchText1"
        Me.cboSearchText1.Size = New System.Drawing.Size(214, 21)
        Me.cboSearchText1.TabIndex = 1
        '
        'cboSearchText2
        '
        Me.cboSearchText2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSearchText2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchText2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchText2.FormattingEnabled = True
        Me.cboSearchText2.Location = New System.Drawing.Point(367, 40)
        Me.cboSearchText2.Name = "cboSearchText2"
        Me.cboSearchText2.Size = New System.Drawing.Size(214, 21)
        Me.cboSearchText2.TabIndex = 3
        '
        'dgvApplicationLog
        '
        Me.dgvApplicationLog.AllowUserToAddRows = False
        Me.dgvApplicationLog.AllowUserToDeleteRows = False
        Me.dgvApplicationLog.AllowUserToOrderColumns = True
        Me.dgvApplicationLog.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvApplicationLog.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvApplicationLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvApplicationLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvApplicationLog.Location = New System.Drawing.Point(0, 0)
        Me.dgvApplicationLog.Name = "dgvApplicationLog"
        Me.dgvApplicationLog.ReadOnly = True
        Me.dgvApplicationLog.RowHeadersVisible = False
        Me.dgvApplicationLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvApplicationLog.Size = New System.Drawing.Size(812, 379)
        Me.dgvApplicationLog.TabIndex = 253
        Me.dgvApplicationLog.Visible = False
        '
        'DTPSearchDate1b
        '
        Me.DTPSearchDate1b.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate1b.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate1b.Location = New System.Drawing.Point(186, 40)
        Me.DTPSearchDate1b.Name = "DTPSearchDate1b"
        Me.DTPSearchDate1b.Size = New System.Drawing.Size(100, 20)
        Me.DTPSearchDate1b.TabIndex = 1
        Me.DTPSearchDate1b.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPSearchDate1b.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 523)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(812, 22)
        Me.StatusStrip1.TabIndex = 254
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(797, 17)
        Me.Panel1.Spring = True
        Me.Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SortGroupBox
        '
        Me.SortGroupBox.Controls.Add(Me.cboSortOrder2)
        Me.SortGroupBox.Controls.Add(Me.Label7)
        Me.SortGroupBox.Controls.Add(Me.cboSort2)
        Me.SortGroupBox.Location = New System.Drawing.Point(5, 66)
        Me.SortGroupBox.Name = "SortGroupBox"
        Me.SortGroupBox.Size = New System.Drawing.Size(285, 71)
        Me.SortGroupBox.TabIndex = 41
        Me.SortGroupBox.TabStop = False
        Me.SortGroupBox.Text = "Sort By"
        '
        'btnExport
        '
        Me.btnExport.Enabled = False
        Me.btnExport.Location = New System.Drawing.Point(465, 107)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(116, 23)
        Me.btnExport.TabIndex = 16
        Me.btnExport.Text = "Export Grid to Excel"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'cboMACT1
        '
        Me.cboMACT1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMACT1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMACT1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMACT1.FormattingEnabled = True
        Me.cboMACT1.Location = New System.Drawing.Point(75, 40)
        Me.cboMACT1.Name = "cboMACT1"
        Me.cboMACT1.Size = New System.Drawing.Size(215, 21)
        Me.cboMACT1.TabIndex = 1
        '
        'cboNSPS1
        '
        Me.cboNSPS1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNSPS1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNSPS1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNSPS1.FormattingEnabled = True
        Me.cboNSPS1.Location = New System.Drawing.Point(76, 40)
        Me.cboNSPS1.Name = "cboNSPS1"
        Me.cboNSPS1.Size = New System.Drawing.Size(214, 21)
        Me.cboNSPS1.TabIndex = 1
        '
        'cboMACT2
        '
        Me.cboMACT2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMACT2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMACT2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMACT2.FormattingEnabled = True
        Me.cboMACT2.Location = New System.Drawing.Point(367, 40)
        Me.cboMACT2.Name = "cboMACT2"
        Me.cboMACT2.Size = New System.Drawing.Size(214, 21)
        Me.cboMACT2.TabIndex = 3
        '
        'cboNSPS2
        '
        Me.cboNSPS2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNSPS2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNSPS2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNSPS2.FormattingEnabled = True
        Me.cboNSPS2.Location = New System.Drawing.Point(367, 40)
        Me.cboNSPS2.Name = "cboNSPS2"
        Me.cboNSPS2.Size = New System.Drawing.Size(214, 21)
        Me.cboNSPS2.TabIndex = 3
        '
        'cboNESHAP2
        '
        Me.cboNESHAP2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNESHAP2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNESHAP2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNESHAP2.FormattingEnabled = True
        Me.cboNESHAP2.Location = New System.Drawing.Point(367, 40)
        Me.cboNESHAP2.Name = "cboNESHAP2"
        Me.cboNESHAP2.Size = New System.Drawing.Size(214, 21)
        Me.cboNESHAP2.TabIndex = 3
        '
        'cboNESHAP1
        '
        Me.cboNESHAP1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNESHAP1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNESHAP1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNESHAP1.FormattingEnabled = True
        Me.cboNESHAP1.Location = New System.Drawing.Point(76, 40)
        Me.cboNESHAP1.Name = "cboNESHAP1"
        Me.cboNESHAP1.Size = New System.Drawing.Size(214, 21)
        Me.cboNESHAP1.TabIndex = 1
        '
        'cboSIP2
        '
        Me.cboSIP2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSIP2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSIP2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSIP2.FormattingEnabled = True
        Me.cboSIP2.Location = New System.Drawing.Point(367, 40)
        Me.cboSIP2.Name = "cboSIP2"
        Me.cboSIP2.Size = New System.Drawing.Size(214, 21)
        Me.cboSIP2.TabIndex = 3
        '
        'cboSIP1
        '
        Me.cboSIP1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSIP1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSIP1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSIP1.FormattingEnabled = True
        Me.cboSIP1.Location = New System.Drawing.Point(75, 40)
        Me.cboSIP1.Name = "cboSIP1"
        Me.cboSIP1.Size = New System.Drawing.Size(215, 21)
        Me.cboSIP1.TabIndex = 1
        '
        'DTPSearchDate2b
        '
        Me.DTPSearchDate2b.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate2b.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate2b.Location = New System.Drawing.Point(477, 40)
        Me.DTPSearchDate2b.Name = "DTPSearchDate2b"
        Me.DTPSearchDate2b.Size = New System.Drawing.Size(100, 20)
        Me.DTPSearchDate2b.TabIndex = 3
        Me.DTPSearchDate2b.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPSearchDate2b.Visible = False
        '
        'pnlDataGridView
        '
        Me.pnlDataGridView.Controls.Add(Me.lblMessage)
        Me.pnlDataGridView.Controls.Add(Me.dgvApplicationLog)
        Me.pnlDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.pnlDataGridView.Name = "pnlDataGridView"
        Me.pnlDataGridView.Size = New System.Drawing.Size(812, 379)
        Me.pnlDataGridView.TabIndex = 256
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(10)
        Me.lblMessage.Size = New System.Drawing.Size(147, 33)
        Me.lblMessage.TabIndex = 254
        Me.lblMessage.Text = "No applications to display"
        '
        'bgwApplicationLog
        '
        Me.bgwApplicationLog.WorkerReportsProgress = True
        Me.bgwApplicationLog.WorkerSupportsCancellation = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.cboSortOrder1)
        Me.Panel2.Controls.Add(Me.cboSort1)
        Me.Panel2.Controls.Add(Me.SortGroupBox)
        Me.Panel2.Controls.Add(Me.btnExport)
        Me.Panel2.Controls.Add(Me.cboMACT1)
        Me.Panel2.Controls.Add(Me.cboNSPS1)
        Me.Panel2.Controls.Add(Me.cboMACT2)
        Me.Panel2.Controls.Add(Me.cboNSPS2)
        Me.Panel2.Controls.Add(Me.cboNESHAP2)
        Me.Panel2.Controls.Add(Me.cboNESHAP1)
        Me.Panel2.Controls.Add(Me.cboSIP2)
        Me.Panel2.Controls.Add(Me.cboSIP1)
        Me.Panel2.Controls.Add(Me.DTPSearchDate2b)
        Me.Panel2.Controls.Add(Me.DTPSearchDate1b)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnOpen)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.btnResetSearch)
        Me.Panel2.Controls.Add(Me.btnFind)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.chbShowAll)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.cboEngineer)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.cboFieldType1)
        Me.Panel2.Controls.Add(Me.cboApplicationStatus)
        Me.Panel2.Controls.Add(Me.cboFieldType2)
        Me.Panel2.Controls.Add(Me.cboApplicationUnit)
        Me.Panel2.Controls.Add(Me.cboApplicationType)
        Me.Panel2.Controls.Add(Me.cboSearchText1)
        Me.Panel2.Controls.Add(Me.cboSearchText2)
        Me.Panel2.Controls.Add(Me.DTPSearchDate1)
        Me.Panel2.Controls.Add(Me.txtSearchText1)
        Me.Panel2.Controls.Add(Me.DTPSearchDate2)
        Me.Panel2.Controls.Add(Me.txtSearchText2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 379)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(812, 144)
        Me.Panel2.TabIndex = 42
        '
        'SSPPApplicationLog
        '
        Me.AcceptButton = Me.btnFind
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(812, 545)
        Me.Controls.Add(Me.pnlDataGridView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSPPApplicationLog"
        Me.Text = "SSPP Application Log"
        CType(Me.dgvApplicationLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SortGroupBox.ResumeLayout(False)
        Me.SortGroupBox.PerformLayout()
        Me.pnlDataGridView.ResumeLayout(False)
        Me.pnlDataGridView.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mmiTools As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClose As System.Windows.Forms.MenuItem
    Friend WithEvents mmiResetSearch As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiNewApplication As System.Windows.Forms.MenuItem
    Friend WithEvents cboSortOrder2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSort2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSortOrder1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSort1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboApplicationStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cboApplicationUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cboApplicationType As System.Windows.Forms.ComboBox
    Friend WithEvents cboFieldType2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboFieldType1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents DTPSearchDate2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents DTPSearchDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnResetSearch As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents txtSearchText2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchText1 As System.Windows.Forms.TextBox
    Friend WithEvents chbShowAll As System.Windows.Forms.CheckBox
    Friend WithEvents cboEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents dgvApplicationLog As System.Windows.Forms.DataGridView
    Friend WithEvents cboSearchText1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSearchText2 As System.Windows.Forms.ComboBox
    Friend WithEvents DTPSearchDate1b As System.Windows.Forms.DateTimePicker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DTPSearchDate2b As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlDataGridView As System.Windows.Forms.Panel
    Friend WithEvents bgwApplicationLog As System.ComponentModel.BackgroundWorker
    Friend WithEvents cboSIP1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSIP2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboMACT1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboNSPS1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboMACT2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboNSPS2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboNESHAP2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboNESHAP1 As System.Windows.Forms.ComboBox
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExport As System.Windows.Forms.MenuItem
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents SortGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents mmiOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
