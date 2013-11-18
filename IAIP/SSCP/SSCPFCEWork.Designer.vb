<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSCPFCEWork
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPFCEWork))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuFile = New System.Windows.Forms.MenuItem
        Me.MenuSave = New System.Windows.Forms.MenuItem
        Me.MenuPrint = New System.Windows.Forms.MenuItem
        Me.MenuClose = New System.Windows.Forms.MenuItem
        Me.MenuHelp = New System.Windows.Forms.MenuItem
        Me.MenuOpenHelp = New System.Windows.Forms.MenuItem
        Me.TBFCE = New System.Windows.Forms.ToolBar
        Me.TbbSave = New System.Windows.Forms.ToolBarButton
        Me.TbbPring = New System.Windows.Forms.ToolBarButton
        Me.txtOrigin = New System.Windows.Forms.TextBox
        Me.txtAirsNumber = New System.Windows.Forms.TextBox
        Me.ReviewDataTabs = New System.Windows.Forms.TabControl
        Me.TPInspections = New System.Windows.Forms.TabPage
        Me.dgrFCEInspections = New System.Windows.Forms.DataGrid
        Me.PanelFCE = New System.Windows.Forms.Panel
        Me.llbFCEInspections = New System.Windows.Forms.LinkLabel
        Me.txtInspectionTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TPCorrespondance = New System.Windows.Forms.TabPage
        Me.PanelNotifications = New System.Windows.Forms.Panel
        Me.dgrFCECorrespondance = New System.Windows.Forms.DataGrid
        Me.PanelFCENotifications = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtNotificationTrackingNumber = New System.Windows.Forms.TextBox
        Me.llbNotification = New System.Windows.Forms.LinkLabel
        Me.TPTitleVACC = New System.Windows.Forms.TabPage
        Me.dgrFCEACC = New System.Windows.Forms.DataGrid
        Me.PanelFCE3 = New System.Windows.Forms.Panel
        Me.llbFCEACC = New System.Windows.Forms.LinkLabel
        Me.txtACCTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TPReports = New System.Windows.Forms.TabPage
        Me.dgrFCEReports = New System.Windows.Forms.DataGrid
        Me.PanelFCE2 = New System.Windows.Forms.Panel
        Me.llbFCEReports = New System.Windows.Forms.LinkLabel
        Me.txtReportTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TPISMPSummaryReports = New System.Windows.Forms.TabPage
        Me.dgrISMPSummaryReports = New System.Windows.Forms.DataGrid
        Me.PanelFCE5 = New System.Windows.Forms.Panel
        Me.llbISMPSummaryReports = New System.Windows.Forms.LinkLabel
        Me.txtISMPReferenceNumber = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TPPerformanceTests = New System.Windows.Forms.TabPage
        Me.dgrPerformanceTests = New System.Windows.Forms.DataGrid
        Me.PanelPerformanceTests = New System.Windows.Forms.Panel
        Me.llbPerformanceTests = New System.Windows.Forms.LinkLabel
        Me.txtPerformanceTests = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TPEnforcement = New System.Windows.Forms.TabPage
        Me.dgrFCEEnforcement = New System.Windows.Forms.DataGrid
        Me.PanelEnforcement = New System.Windows.Forms.Panel
        Me.llbFCEEnforcement = New System.Windows.Forms.LinkLabel
        Me.txtEnforcement = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.FceDataGroup = New System.Windows.Forms.GroupBox
        Me.CompleteOrIncomplete = New System.Windows.Forms.GroupBox
        Me.rdbFCEIncomplete = New System.Windows.Forms.RadioButton
        Me.rdbFCEComplete = New System.Windows.Forms.RadioButton
        Me.OnsiteOrOffsite = New System.Windows.Forms.GroupBox
        Me.rdbFCENoOnsite = New System.Windows.Forms.RadioButton
        Me.rdbFCEOnSite = New System.Windows.Forms.RadioButton
        Me.cboReviewer = New System.Windows.Forms.ComboBox
        Me.txtFCENumber = New System.Windows.Forms.TextBox
        Me.txtFCEComments = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.DTPFCECompleteDate = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.cboFCEYear = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.DTPFilterStartDate = New System.Windows.Forms.DateTimePicker
        Me.DTPFilterEndDate = New System.Windows.Forms.DateTimePicker
        Me.llbViewFCEData = New System.Windows.Forms.LinkLabel
        Me.TabControlFCE = New System.Windows.Forms.TabControl
        Me.TabPageFCEData = New System.Windows.Forms.TabPage
        Me.FacilityInfoPanel = New System.Windows.Forms.Panel
        Me.txtFacilityInformation = New System.Windows.Forms.TextBox
        Me.labReferenceNumber = New System.Windows.Forms.Label
        Me.ReviewDataGroup = New System.Windows.Forms.GroupBox
        Me.ReviewDataSelectorPanel = New System.Windows.Forms.Panel
        Me.TabPageFCEPrint = New System.Windows.Forms.TabPage
        Me.CRViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.StatusStrip1.SuspendLayout()
        Me.ReviewDataTabs.SuspendLayout()
        Me.TPInspections.SuspendLayout()
        CType(Me.dgrFCEInspections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFCE.SuspendLayout()
        Me.TPCorrespondance.SuspendLayout()
        Me.PanelNotifications.SuspendLayout()
        CType(Me.dgrFCECorrespondance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFCENotifications.SuspendLayout()
        Me.TPTitleVACC.SuspendLayout()
        CType(Me.dgrFCEACC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFCE3.SuspendLayout()
        Me.TPReports.SuspendLayout()
        CType(Me.dgrFCEReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFCE2.SuspendLayout()
        Me.TPISMPSummaryReports.SuspendLayout()
        CType(Me.dgrISMPSummaryReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFCE5.SuspendLayout()
        Me.TPPerformanceTests.SuspendLayout()
        CType(Me.dgrPerformanceTests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPerformanceTests.SuspendLayout()
        Me.TPEnforcement.SuspendLayout()
        CType(Me.dgrFCEEnforcement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEnforcement.SuspendLayout()
        Me.FceDataGroup.SuspendLayout()
        Me.CompleteOrIncomplete.SuspendLayout()
        Me.OnsiteOrOffsite.SuspendLayout()
        Me.TabControlFCE.SuspendLayout()
        Me.TabPageFCEData.SuspendLayout()
        Me.FacilityInfoPanel.SuspendLayout()
        Me.ReviewDataGroup.SuspendLayout()
        Me.ReviewDataSelectorPanel.SuspendLayout()
        Me.TabPageFCEPrint.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 620)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(877, 22)
        Me.StatusStrip1.TabIndex = 201
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
        Me.Panel1.Size = New System.Drawing.Size(752, 17)
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
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuFile, Me.MenuHelp})
        '
        'MenuFile
        '
        Me.MenuFile.Index = 0
        Me.MenuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuSave, Me.MenuPrint, Me.MenuClose})
        Me.MenuFile.Text = "&File"
        '
        'MenuSave
        '
        Me.MenuSave.Index = 0
        Me.MenuSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.MenuSave.Text = "&Save"
        '
        'MenuPrint
        '
        Me.MenuPrint.Index = 1
        Me.MenuPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.MenuPrint.Text = "&Print Preview"
        '
        'MenuClose
        '
        Me.MenuClose.Index = 2
        Me.MenuClose.Shortcut = System.Windows.Forms.Shortcut.CtrlW
        Me.MenuClose.Text = "&Close"
        '
        'MenuHelp
        '
        Me.MenuHelp.Index = 1
        Me.MenuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuOpenHelp})
        Me.MenuHelp.ShowShortcut = False
        Me.MenuHelp.Text = "&Help"
        '
        'MenuOpenHelp
        '
        Me.MenuOpenHelp.Index = 0
        Me.MenuOpenHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.MenuOpenHelp.Text = "Online &Help"
        '
        'TBFCE
        '
        Me.TBFCE.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbSave, Me.TbbPring})
        Me.TBFCE.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBFCE.DropDownArrows = True
        Me.TBFCE.ImageList = Me.Image_List_All
        Me.TBFCE.Location = New System.Drawing.Point(0, 0)
        Me.TBFCE.Name = "TBFCE"
        Me.TBFCE.ShowToolTips = True
        Me.TBFCE.Size = New System.Drawing.Size(877, 28)
        Me.TBFCE.TabIndex = 202
        '
        'TbbSave
        '
        Me.TbbSave.ImageIndex = 65
        Me.TbbSave.Name = "TbbSave"
        '
        'TbbPring
        '
        Me.TbbPring.ImageIndex = 19
        Me.TbbPring.Name = "TbbPring"
        '
        'txtOrigin
        '
        Me.txtOrigin.Location = New System.Drawing.Point(19, 47)
        Me.txtOrigin.Name = "txtOrigin"
        Me.txtOrigin.Size = New System.Drawing.Size(10, 20)
        Me.txtOrigin.TabIndex = 240
        Me.txtOrigin.Visible = False
        '
        'txtAirsNumber
        '
        Me.txtAirsNumber.BackColor = System.Drawing.SystemColors.Control
        Me.txtAirsNumber.Location = New System.Drawing.Point(5, 47)
        Me.txtAirsNumber.Name = "txtAirsNumber"
        Me.txtAirsNumber.ReadOnly = True
        Me.txtAirsNumber.Size = New System.Drawing.Size(12, 20)
        Me.txtAirsNumber.TabIndex = 239
        Me.txtAirsNumber.Visible = False
        '
        'ReviewDataTabs
        '
        Me.ReviewDataTabs.Controls.Add(Me.TPInspections)
        Me.ReviewDataTabs.Controls.Add(Me.TPCorrespondance)
        Me.ReviewDataTabs.Controls.Add(Me.TPTitleVACC)
        Me.ReviewDataTabs.Controls.Add(Me.TPReports)
        Me.ReviewDataTabs.Controls.Add(Me.TPISMPSummaryReports)
        Me.ReviewDataTabs.Controls.Add(Me.TPPerformanceTests)
        Me.ReviewDataTabs.Controls.Add(Me.TPEnforcement)
        Me.ReviewDataTabs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ReviewDataTabs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReviewDataTabs.HotTrack = True
        Me.ReviewDataTabs.Location = New System.Drawing.Point(3, 45)
        Me.ReviewDataTabs.Name = "ReviewDataTabs"
        Me.ReviewDataTabs.SelectedIndex = 0
        Me.ReviewDataTabs.Size = New System.Drawing.Size(857, 207)
        Me.ReviewDataTabs.TabIndex = 238
        '
        'TPInspections
        '
        Me.TPInspections.Controls.Add(Me.dgrFCEInspections)
        Me.TPInspections.Controls.Add(Me.PanelFCE)
        Me.TPInspections.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TPInspections.Location = New System.Drawing.Point(4, 22)
        Me.TPInspections.Name = "TPInspections"
        Me.TPInspections.Size = New System.Drawing.Size(849, 181)
        Me.TPInspections.TabIndex = 0
        Me.TPInspections.Text = "Inspections"
        Me.TPInspections.UseVisualStyleBackColor = True
        '
        'dgrFCEInspections
        '
        Me.dgrFCEInspections.DataMember = ""
        Me.dgrFCEInspections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrFCEInspections.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrFCEInspections.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrFCEInspections.Location = New System.Drawing.Point(0, 39)
        Me.dgrFCEInspections.Name = "dgrFCEInspections"
        Me.dgrFCEInspections.ReadOnly = True
        Me.dgrFCEInspections.Size = New System.Drawing.Size(849, 142)
        Me.dgrFCEInspections.TabIndex = 3
        '
        'PanelFCE
        '
        Me.PanelFCE.Controls.Add(Me.llbFCEInspections)
        Me.PanelFCE.Controls.Add(Me.txtInspectionTrackingNumber)
        Me.PanelFCE.Controls.Add(Me.Label10)
        Me.PanelFCE.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFCE.Location = New System.Drawing.Point(0, 0)
        Me.PanelFCE.Name = "PanelFCE"
        Me.PanelFCE.Size = New System.Drawing.Size(849, 39)
        Me.PanelFCE.TabIndex = 1
        '
        'llbFCEInspections
        '
        Me.llbFCEInspections.AutoSize = True
        Me.llbFCEInspections.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbFCEInspections.Location = New System.Drawing.Point(216, 10)
        Me.llbFCEInspections.Name = "llbFCEInspections"
        Me.llbFCEInspections.Size = New System.Drawing.Size(120, 13)
        Me.llbFCEInspections.TabIndex = 2
        Me.llbFCEInspections.TabStop = True
        Me.llbFCEInspections.Text = "View Inspection Record"
        '
        'txtInspectionTrackingNumber
        '
        Me.txtInspectionTrackingNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspectionTrackingNumber.Location = New System.Drawing.Point(104, 8)
        Me.txtInspectionTrackingNumber.Name = "txtInspectionTrackingNumber"
        Me.txtInspectionTrackingNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtInspectionTrackingNumber.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Tracking Number:"
        '
        'TPCorrespondance
        '
        Me.TPCorrespondance.Controls.Add(Me.PanelNotifications)
        Me.TPCorrespondance.Location = New System.Drawing.Point(4, 22)
        Me.TPCorrespondance.Name = "TPCorrespondance"
        Me.TPCorrespondance.Size = New System.Drawing.Size(849, 181)
        Me.TPCorrespondance.TabIndex = 3
        Me.TPCorrespondance.Text = "Notifications"
        Me.TPCorrespondance.UseVisualStyleBackColor = True
        '
        'PanelNotifications
        '
        Me.PanelNotifications.Controls.Add(Me.dgrFCECorrespondance)
        Me.PanelNotifications.Controls.Add(Me.PanelFCENotifications)
        Me.PanelNotifications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelNotifications.Location = New System.Drawing.Point(0, 0)
        Me.PanelNotifications.Name = "PanelNotifications"
        Me.PanelNotifications.Size = New System.Drawing.Size(849, 181)
        Me.PanelNotifications.TabIndex = 1
        '
        'dgrFCECorrespondance
        '
        Me.dgrFCECorrespondance.DataMember = ""
        Me.dgrFCECorrespondance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrFCECorrespondance.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrFCECorrespondance.Location = New System.Drawing.Point(0, 40)
        Me.dgrFCECorrespondance.Name = "dgrFCECorrespondance"
        Me.dgrFCECorrespondance.ReadOnly = True
        Me.dgrFCECorrespondance.Size = New System.Drawing.Size(849, 141)
        Me.dgrFCECorrespondance.TabIndex = 8
        '
        'PanelFCENotifications
        '
        Me.PanelFCENotifications.Controls.Add(Me.Label13)
        Me.PanelFCENotifications.Controls.Add(Me.txtNotificationTrackingNumber)
        Me.PanelFCENotifications.Controls.Add(Me.llbNotification)
        Me.PanelFCENotifications.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFCENotifications.Location = New System.Drawing.Point(0, 0)
        Me.PanelFCENotifications.Name = "PanelFCENotifications"
        Me.PanelFCENotifications.Size = New System.Drawing.Size(849, 40)
        Me.PanelFCENotifications.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Tracking Number:"
        '
        'txtNotificationTrackingNumber
        '
        Me.txtNotificationTrackingNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotificationTrackingNumber.Location = New System.Drawing.Point(104, 8)
        Me.txtNotificationTrackingNumber.Name = "txtNotificationTrackingNumber"
        Me.txtNotificationTrackingNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtNotificationTrackingNumber.TabIndex = 4
        '
        'llbNotification
        '
        Me.llbNotification.AutoSize = True
        Me.llbNotification.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbNotification.Location = New System.Drawing.Point(216, 10)
        Me.llbNotification.Name = "llbNotification"
        Me.llbNotification.Size = New System.Drawing.Size(86, 13)
        Me.llbNotification.TabIndex = 5
        Me.llbNotification.TabStop = True
        Me.llbNotification.Text = "View Notification"
        '
        'TPTitleVACC
        '
        Me.TPTitleVACC.Controls.Add(Me.dgrFCEACC)
        Me.TPTitleVACC.Controls.Add(Me.PanelFCE3)
        Me.TPTitleVACC.Location = New System.Drawing.Point(4, 22)
        Me.TPTitleVACC.Name = "TPTitleVACC"
        Me.TPTitleVACC.Size = New System.Drawing.Size(849, 181)
        Me.TPTitleVACC.TabIndex = 2
        Me.TPTitleVACC.Text = "Title V Annual Certification"
        Me.TPTitleVACC.UseVisualStyleBackColor = True
        '
        'dgrFCEACC
        '
        Me.dgrFCEACC.DataMember = ""
        Me.dgrFCEACC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrFCEACC.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrFCEACC.Location = New System.Drawing.Point(0, 40)
        Me.dgrFCEACC.Name = "dgrFCEACC"
        Me.dgrFCEACC.ReadOnly = True
        Me.dgrFCEACC.Size = New System.Drawing.Size(849, 141)
        Me.dgrFCEACC.TabIndex = 3
        '
        'PanelFCE3
        '
        Me.PanelFCE3.Controls.Add(Me.llbFCEACC)
        Me.PanelFCE3.Controls.Add(Me.txtACCTrackingNumber)
        Me.PanelFCE3.Controls.Add(Me.Label12)
        Me.PanelFCE3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFCE3.Location = New System.Drawing.Point(0, 0)
        Me.PanelFCE3.Name = "PanelFCE3"
        Me.PanelFCE3.Size = New System.Drawing.Size(849, 40)
        Me.PanelFCE3.TabIndex = 1
        '
        'llbFCEACC
        '
        Me.llbFCEACC.AutoSize = True
        Me.llbFCEACC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbFCEACC.Location = New System.Drawing.Point(216, 10)
        Me.llbFCEACC.Name = "llbFCEACC"
        Me.llbFCEACC.Size = New System.Drawing.Size(124, 13)
        Me.llbFCEACC.TabIndex = 5
        Me.llbFCEACC.TabStop = True
        Me.llbFCEACC.Text = "View Annual Certification"
        '
        'txtACCTrackingNumber
        '
        Me.txtACCTrackingNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtACCTrackingNumber.Location = New System.Drawing.Point(104, 8)
        Me.txtACCTrackingNumber.Name = "txtACCTrackingNumber"
        Me.txtACCTrackingNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtACCTrackingNumber.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(92, 13)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Tracking Number:"
        '
        'TPReports
        '
        Me.TPReports.Controls.Add(Me.dgrFCEReports)
        Me.TPReports.Controls.Add(Me.PanelFCE2)
        Me.TPReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TPReports.Location = New System.Drawing.Point(4, 22)
        Me.TPReports.Name = "TPReports"
        Me.TPReports.Size = New System.Drawing.Size(849, 181)
        Me.TPReports.TabIndex = 1
        Me.TPReports.Text = "Reports"
        Me.TPReports.UseVisualStyleBackColor = True
        '
        'dgrFCEReports
        '
        Me.dgrFCEReports.DataMember = ""
        Me.dgrFCEReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrFCEReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrFCEReports.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrFCEReports.Location = New System.Drawing.Point(0, 40)
        Me.dgrFCEReports.Name = "dgrFCEReports"
        Me.dgrFCEReports.ReadOnly = True
        Me.dgrFCEReports.Size = New System.Drawing.Size(849, 141)
        Me.dgrFCEReports.TabIndex = 2
        '
        'PanelFCE2
        '
        Me.PanelFCE2.Controls.Add(Me.llbFCEReports)
        Me.PanelFCE2.Controls.Add(Me.txtReportTrackingNumber)
        Me.PanelFCE2.Controls.Add(Me.Label11)
        Me.PanelFCE2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFCE2.Location = New System.Drawing.Point(0, 0)
        Me.PanelFCE2.Name = "PanelFCE2"
        Me.PanelFCE2.Size = New System.Drawing.Size(849, 40)
        Me.PanelFCE2.TabIndex = 0
        '
        'llbFCEReports
        '
        Me.llbFCEReports.AutoSize = True
        Me.llbFCEReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbFCEReports.Location = New System.Drawing.Point(216, 10)
        Me.llbFCEReports.Name = "llbFCEReports"
        Me.llbFCEReports.Size = New System.Drawing.Size(65, 13)
        Me.llbFCEReports.TabIndex = 5
        Me.llbFCEReports.TabStop = True
        Me.llbFCEReports.Text = "View Report"
        '
        'txtReportTrackingNumber
        '
        Me.txtReportTrackingNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportTrackingNumber.Location = New System.Drawing.Point(104, 8)
        Me.txtReportTrackingNumber.Name = "txtReportTrackingNumber"
        Me.txtReportTrackingNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtReportTrackingNumber.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(92, 13)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Tracking Number:"
        '
        'TPISMPSummaryReports
        '
        Me.TPISMPSummaryReports.Controls.Add(Me.dgrISMPSummaryReports)
        Me.TPISMPSummaryReports.Controls.Add(Me.PanelFCE5)
        Me.TPISMPSummaryReports.Location = New System.Drawing.Point(4, 22)
        Me.TPISMPSummaryReports.Name = "TPISMPSummaryReports"
        Me.TPISMPSummaryReports.Size = New System.Drawing.Size(849, 181)
        Me.TPISMPSummaryReports.TabIndex = 4
        Me.TPISMPSummaryReports.Text = "ISMP Summary Reports"
        Me.TPISMPSummaryReports.UseVisualStyleBackColor = True
        '
        'dgrISMPSummaryReports
        '
        Me.dgrISMPSummaryReports.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrISMPSummaryReports.DataMember = ""
        Me.dgrISMPSummaryReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrISMPSummaryReports.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrISMPSummaryReports.Location = New System.Drawing.Point(0, 40)
        Me.dgrISMPSummaryReports.Name = "dgrISMPSummaryReports"
        Me.dgrISMPSummaryReports.ReadOnly = True
        Me.dgrISMPSummaryReports.Size = New System.Drawing.Size(849, 141)
        Me.dgrISMPSummaryReports.TabIndex = 4
        '
        'PanelFCE5
        '
        Me.PanelFCE5.Controls.Add(Me.llbISMPSummaryReports)
        Me.PanelFCE5.Controls.Add(Me.txtISMPReferenceNumber)
        Me.PanelFCE5.Controls.Add(Me.Label14)
        Me.PanelFCE5.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFCE5.Location = New System.Drawing.Point(0, 0)
        Me.PanelFCE5.Name = "PanelFCE5"
        Me.PanelFCE5.Size = New System.Drawing.Size(849, 40)
        Me.PanelFCE5.TabIndex = 2
        '
        'llbISMPSummaryReports
        '
        Me.llbISMPSummaryReports.AutoSize = True
        Me.llbISMPSummaryReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbISMPSummaryReports.Location = New System.Drawing.Point(216, 10)
        Me.llbISMPSummaryReports.Name = "llbISMPSummaryReports"
        Me.llbISMPSummaryReports.Size = New System.Drawing.Size(145, 13)
        Me.llbISMPSummaryReports.TabIndex = 5
        Me.llbISMPSummaryReports.TabStop = True
        Me.llbISMPSummaryReports.Text = "View ISMP Summary Reports"
        '
        'txtISMPReferenceNumber
        '
        Me.txtISMPReferenceNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtISMPReferenceNumber.Location = New System.Drawing.Point(104, 8)
        Me.txtISMPReferenceNumber.Name = "txtISMPReferenceNumber"
        Me.txtISMPReferenceNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtISMPReferenceNumber.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 10)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 13)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "Reference Number:"
        '
        'TPPerformanceTests
        '
        Me.TPPerformanceTests.Controls.Add(Me.dgrPerformanceTests)
        Me.TPPerformanceTests.Controls.Add(Me.PanelPerformanceTests)
        Me.TPPerformanceTests.Location = New System.Drawing.Point(4, 22)
        Me.TPPerformanceTests.Name = "TPPerformanceTests"
        Me.TPPerformanceTests.Size = New System.Drawing.Size(849, 181)
        Me.TPPerformanceTests.TabIndex = 6
        Me.TPPerformanceTests.Text = "PerformanceTest Reviews"
        Me.TPPerformanceTests.UseVisualStyleBackColor = True
        '
        'dgrPerformanceTests
        '
        Me.dgrPerformanceTests.DataMember = ""
        Me.dgrPerformanceTests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrPerformanceTests.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrPerformanceTests.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrPerformanceTests.Location = New System.Drawing.Point(0, 40)
        Me.dgrPerformanceTests.Name = "dgrPerformanceTests"
        Me.dgrPerformanceTests.ReadOnly = True
        Me.dgrPerformanceTests.Size = New System.Drawing.Size(849, 141)
        Me.dgrPerformanceTests.TabIndex = 4
        '
        'PanelPerformanceTests
        '
        Me.PanelPerformanceTests.Controls.Add(Me.llbPerformanceTests)
        Me.PanelPerformanceTests.Controls.Add(Me.txtPerformanceTests)
        Me.PanelPerformanceTests.Controls.Add(Me.Label5)
        Me.PanelPerformanceTests.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelPerformanceTests.Location = New System.Drawing.Point(0, 0)
        Me.PanelPerformanceTests.Name = "PanelPerformanceTests"
        Me.PanelPerformanceTests.Size = New System.Drawing.Size(849, 40)
        Me.PanelPerformanceTests.TabIndex = 2
        '
        'llbPerformanceTests
        '
        Me.llbPerformanceTests.AutoSize = True
        Me.llbPerformanceTests.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbPerformanceTests.Location = New System.Drawing.Point(216, 10)
        Me.llbPerformanceTests.Name = "llbPerformanceTests"
        Me.llbPerformanceTests.Size = New System.Drawing.Size(122, 13)
        Me.llbPerformanceTests.TabIndex = 2
        Me.llbPerformanceTests.TabStop = True
        Me.llbPerformanceTests.Text = "View Performance Tests"
        '
        'txtPerformanceTests
        '
        Me.txtPerformanceTests.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPerformanceTests.Location = New System.Drawing.Point(104, 8)
        Me.txtPerformanceTests.Name = "txtPerformanceTests"
        Me.txtPerformanceTests.Size = New System.Drawing.Size(100, 20)
        Me.txtPerformanceTests.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Tracking Number:"
        '
        'TPEnforcement
        '
        Me.TPEnforcement.Controls.Add(Me.dgrFCEEnforcement)
        Me.TPEnforcement.Controls.Add(Me.PanelEnforcement)
        Me.TPEnforcement.Location = New System.Drawing.Point(4, 22)
        Me.TPEnforcement.Name = "TPEnforcement"
        Me.TPEnforcement.Size = New System.Drawing.Size(849, 181)
        Me.TPEnforcement.TabIndex = 5
        Me.TPEnforcement.Text = "Enforcement"
        Me.TPEnforcement.UseVisualStyleBackColor = True
        '
        'dgrFCEEnforcement
        '
        Me.dgrFCEEnforcement.DataMember = ""
        Me.dgrFCEEnforcement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrFCEEnforcement.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrFCEEnforcement.Location = New System.Drawing.Point(0, 40)
        Me.dgrFCEEnforcement.Name = "dgrFCEEnforcement"
        Me.dgrFCEEnforcement.ReadOnly = True
        Me.dgrFCEEnforcement.Size = New System.Drawing.Size(849, 141)
        Me.dgrFCEEnforcement.TabIndex = 2
        '
        'PanelEnforcement
        '
        Me.PanelEnforcement.Controls.Add(Me.llbFCEEnforcement)
        Me.PanelEnforcement.Controls.Add(Me.txtEnforcement)
        Me.PanelEnforcement.Controls.Add(Me.Label16)
        Me.PanelEnforcement.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEnforcement.Location = New System.Drawing.Point(0, 0)
        Me.PanelEnforcement.Name = "PanelEnforcement"
        Me.PanelEnforcement.Size = New System.Drawing.Size(849, 40)
        Me.PanelEnforcement.TabIndex = 0
        '
        'llbFCEEnforcement
        '
        Me.llbFCEEnforcement.AutoSize = True
        Me.llbFCEEnforcement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbFCEEnforcement.Location = New System.Drawing.Point(232, 10)
        Me.llbFCEEnforcement.Name = "llbFCEEnforcement"
        Me.llbFCEEnforcement.Size = New System.Drawing.Size(93, 13)
        Me.llbFCEEnforcement.TabIndex = 8
        Me.llbFCEEnforcement.TabStop = True
        Me.llbFCEEnforcement.Text = "View Enforcement"
        '
        'txtEnforcement
        '
        Me.txtEnforcement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnforcement.Location = New System.Drawing.Point(120, 8)
        Me.txtEnforcement.Name = "txtEnforcement"
        Me.txtEnforcement.Size = New System.Drawing.Size(100, 20)
        Me.txtEnforcement.TabIndex = 7
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(110, 13)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "Enforcement Number:"
        '
        'FceDataGroup
        '
        Me.FceDataGroup.Controls.Add(Me.CompleteOrIncomplete)
        Me.FceDataGroup.Controls.Add(Me.OnsiteOrOffsite)
        Me.FceDataGroup.Controls.Add(Me.cboReviewer)
        Me.FceDataGroup.Controls.Add(Me.txtFCENumber)
        Me.FceDataGroup.Controls.Add(Me.txtFCEComments)
        Me.FceDataGroup.Controls.Add(Me.Label8)
        Me.FceDataGroup.Controls.Add(Me.DTPFCECompleteDate)
        Me.FceDataGroup.Controls.Add(Me.Label7)
        Me.FceDataGroup.Controls.Add(Me.Label2)
        Me.FceDataGroup.Controls.Add(Me.Label9)
        Me.FceDataGroup.Controls.Add(Me.cboFCEYear)
        Me.FceDataGroup.Dock = System.Windows.Forms.DockStyle.Top
        Me.FceDataGroup.Location = New System.Drawing.Point(3, 103)
        Me.FceDataGroup.Name = "FceDataGroup"
        Me.FceDataGroup.Size = New System.Drawing.Size(863, 126)
        Me.FceDataGroup.TabIndex = 0
        Me.FceDataGroup.TabStop = False
        Me.FceDataGroup.Text = "FCE Entry"
        '
        'CompleteOrIncomplete
        '
        Me.CompleteOrIncomplete.Controls.Add(Me.rdbFCEIncomplete)
        Me.CompleteOrIncomplete.Controls.Add(Me.rdbFCEComplete)
        Me.CompleteOrIncomplete.Location = New System.Drawing.Point(571, 62)
        Me.CompleteOrIncomplete.Name = "CompleteOrIncomplete"
        Me.CompleteOrIncomplete.Size = New System.Drawing.Size(87, 58)
        Me.CompleteOrIncomplete.TabIndex = 5
        Me.CompleteOrIncomplete.TabStop = False
        Me.CompleteOrIncomplete.Text = "Deprecated"
        Me.CompleteOrIncomplete.Visible = False
        '
        'rdbFCEIncomplete
        '
        Me.rdbFCEIncomplete.Location = New System.Drawing.Point(6, 41)
        Me.rdbFCEIncomplete.Name = "rdbFCEIncomplete"
        Me.rdbFCEIncomplete.Size = New System.Drawing.Size(78, 16)
        Me.rdbFCEIncomplete.TabIndex = 9
        Me.rdbFCEIncomplete.Text = "Incomplete"
        Me.rdbFCEIncomplete.Visible = False
        '
        'rdbFCEComplete
        '
        Me.rdbFCEComplete.Checked = True
        Me.rdbFCEComplete.Location = New System.Drawing.Point(6, 19)
        Me.rdbFCEComplete.Name = "rdbFCEComplete"
        Me.rdbFCEComplete.Size = New System.Drawing.Size(72, 16)
        Me.rdbFCEComplete.TabIndex = 8
        Me.rdbFCEComplete.TabStop = True
        Me.rdbFCEComplete.Text = "Complete"
        Me.rdbFCEComplete.Visible = False
        '
        'OnsiteOrOffsite
        '
        Me.OnsiteOrOffsite.Controls.Add(Me.rdbFCENoOnsite)
        Me.OnsiteOrOffsite.Controls.Add(Me.rdbFCEOnSite)
        Me.OnsiteOrOffsite.Location = New System.Drawing.Point(280, 6)
        Me.OnsiteOrOffsite.Name = "OnsiteOrOffsite"
        Me.OnsiteOrOffsite.Size = New System.Drawing.Size(166, 62)
        Me.OnsiteOrOffsite.TabIndex = 3
        Me.OnsiteOrOffsite.TabStop = False
        '
        'rdbFCENoOnsite
        '
        Me.rdbFCENoOnsite.AutoSize = True
        Me.rdbFCENoOnsite.Location = New System.Drawing.Point(6, 42)
        Me.rdbFCENoOnsite.Name = "rdbFCENoOnsite"
        Me.rdbFCENoOnsite.Size = New System.Drawing.Size(133, 17)
        Me.rdbFCENoOnsite.TabIndex = 1
        Me.rdbFCENoOnsite.Text = "FCE without inspection"
        '
        'rdbFCEOnSite
        '
        Me.rdbFCEOnSite.AutoSize = True
        Me.rdbFCEOnSite.Location = New System.Drawing.Point(6, 19)
        Me.rdbFCEOnSite.Name = "rdbFCEOnSite"
        Me.rdbFCEOnSite.Size = New System.Drawing.Size(152, 17)
        Me.rdbFCEOnSite.TabIndex = 0
        Me.rdbFCEOnSite.Text = "FCE with on-site inspection"
        '
        'cboReviewer
        '
        Me.cboReviewer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReviewer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReviewer.Location = New System.Drawing.Point(101, 40)
        Me.cboReviewer.Name = "cboReviewer"
        Me.cboReviewer.Size = New System.Drawing.Size(160, 21)
        Me.cboReviewer.TabIndex = 1
        '
        'txtFCENumber
        '
        Me.txtFCENumber.Enabled = False
        Me.txtFCENumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFCENumber.Location = New System.Drawing.Point(208, 13)
        Me.txtFCENumber.Name = "txtFCENumber"
        Me.txtFCENumber.ReadOnly = True
        Me.txtFCENumber.Size = New System.Drawing.Size(53, 22)
        Me.txtFCENumber.TabIndex = 8
        '
        'txtFCEComments
        '
        Me.txtFCEComments.AcceptsReturn = True
        Me.txtFCEComments.Location = New System.Drawing.Point(476, 31)
        Me.txtFCEComments.Multiline = True
        Me.txtFCEComments.Name = "txtFCEComments"
        Me.txtFCEComments.Size = New System.Drawing.Size(384, 56)
        Me.txtFCEComments.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(473, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Comments:"
        '
        'DTPFCECompleteDate
        '
        Me.DTPFCECompleteDate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFCECompleteDate.Checked = False
        Me.DTPFCECompleteDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFCECompleteDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFCECompleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFCECompleteDate.Location = New System.Drawing.Point(101, 65)
        Me.DTPFCECompleteDate.Name = "DTPFCECompleteDate"
        Me.DTPFCECompleteDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPFCECompleteDate.TabIndex = 2
        Me.DTPFCECompleteDate.Value = New Date(2005, 9, 9, 0, 0, 0, 0)
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Date Completed: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 73
        Me.Label2.Text = "Reviewed By:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 104
        Me.Label9.Text = "FCE Year:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboFCEYear
        '
        Me.cboFCEYear.Location = New System.Drawing.Point(101, 13)
        Me.cboFCEYear.Name = "cboFCEYear"
        Me.cboFCEYear.Size = New System.Drawing.Size(101, 21)
        Me.cboFCEYear.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(173, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "End Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 108
        Me.Label3.Text = "Start Date:"
        '
        'DTPFilterStartDate
        '
        Me.DTPFilterStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFilterStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFilterStartDate.Location = New System.Drawing.Point(67, 3)
        Me.DTPFilterStartDate.Name = "DTPFilterStartDate"
        Me.DTPFilterStartDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPFilterStartDate.TabIndex = 107
        Me.DTPFilterStartDate.Value = New Date(2005, 9, 9, 0, 0, 0, 0)
        '
        'DTPFilterEndDate
        '
        Me.DTPFilterEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFilterEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFilterEndDate.Location = New System.Drawing.Point(234, 3)
        Me.DTPFilterEndDate.Name = "DTPFilterEndDate"
        Me.DTPFilterEndDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPFilterEndDate.TabIndex = 106
        Me.DTPFilterEndDate.Value = New Date(2005, 9, 9, 0, 0, 0, 0)
        '
        'llbViewFCEData
        '
        Me.llbViewFCEData.AutoSize = True
        Me.llbViewFCEData.Location = New System.Drawing.Point(336, 7)
        Me.llbViewFCEData.Name = "llbViewFCEData"
        Me.llbViewFCEData.Size = New System.Drawing.Size(56, 13)
        Me.llbViewFCEData.TabIndex = 105
        Me.llbViewFCEData.TabStop = True
        Me.llbViewFCEData.Text = "View Data"
        Me.llbViewFCEData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabControlFCE
        '
        Me.TabControlFCE.Controls.Add(Me.TabPageFCEData)
        Me.TabControlFCE.Controls.Add(Me.TabPageFCEPrint)
        Me.TabControlFCE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlFCE.Location = New System.Drawing.Point(0, 28)
        Me.TabControlFCE.Name = "TabControlFCE"
        Me.TabControlFCE.SelectedIndex = 0
        Me.TabControlFCE.Size = New System.Drawing.Size(877, 592)
        Me.TabControlFCE.TabIndex = 243
        '
        'TabPageFCEData
        '
        Me.TabPageFCEData.Controls.Add(Me.FceDataGroup)
        Me.TabPageFCEData.Controls.Add(Me.FacilityInfoPanel)
        Me.TabPageFCEData.Controls.Add(Me.ReviewDataGroup)
        Me.TabPageFCEData.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFCEData.Name = "TabPageFCEData"
        Me.TabPageFCEData.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFCEData.Size = New System.Drawing.Size(869, 566)
        Me.TabPageFCEData.TabIndex = 0
        Me.TabPageFCEData.Text = "FCE Data"
        Me.TabPageFCEData.UseVisualStyleBackColor = True
        '
        'FacilityInfoPanel
        '
        Me.FacilityInfoPanel.Controls.Add(Me.txtFacilityInformation)
        Me.FacilityInfoPanel.Controls.Add(Me.labReferenceNumber)
        Me.FacilityInfoPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.FacilityInfoPanel.Location = New System.Drawing.Point(3, 3)
        Me.FacilityInfoPanel.Name = "FacilityInfoPanel"
        Me.FacilityInfoPanel.Size = New System.Drawing.Size(863, 100)
        Me.FacilityInfoPanel.TabIndex = 242
        '
        'txtFacilityInformation
        '
        Me.txtFacilityInformation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFacilityInformation.Location = New System.Drawing.Point(107, 6)
        Me.txtFacilityInformation.Multiline = True
        Me.txtFacilityInformation.Name = "txtFacilityInformation"
        Me.txtFacilityInformation.ReadOnly = True
        Me.txtFacilityInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityInformation.Size = New System.Drawing.Size(753, 83)
        Me.txtFacilityInformation.TabIndex = 236
        '
        'labReferenceNumber
        '
        Me.labReferenceNumber.AutoSize = True
        Me.labReferenceNumber.Location = New System.Drawing.Point(4, 14)
        Me.labReferenceNumber.Name = "labReferenceNumber"
        Me.labReferenceNumber.Size = New System.Drawing.Size(97, 13)
        Me.labReferenceNumber.TabIndex = 235
        Me.labReferenceNumber.Text = "Facility Information:"
        Me.labReferenceNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'ReviewDataGroup
        '
        Me.ReviewDataGroup.Controls.Add(Me.ReviewDataSelectorPanel)
        Me.ReviewDataGroup.Controls.Add(Me.ReviewDataTabs)
        Me.ReviewDataGroup.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ReviewDataGroup.Location = New System.Drawing.Point(3, 308)
        Me.ReviewDataGroup.Name = "ReviewDataGroup"
        Me.ReviewDataGroup.Size = New System.Drawing.Size(863, 255)
        Me.ReviewDataGroup.TabIndex = 1
        Me.ReviewDataGroup.TabStop = False
        Me.ReviewDataGroup.Text = "Review Facility Data"
        '
        'ReviewDataSelectorPanel
        '
        Me.ReviewDataSelectorPanel.Controls.Add(Me.Label3)
        Me.ReviewDataSelectorPanel.Controls.Add(Me.DTPFilterEndDate)
        Me.ReviewDataSelectorPanel.Controls.Add(Me.DTPFilterStartDate)
        Me.ReviewDataSelectorPanel.Controls.Add(Me.llbViewFCEData)
        Me.ReviewDataSelectorPanel.Controls.Add(Me.Label4)
        Me.ReviewDataSelectorPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReviewDataSelectorPanel.Location = New System.Drawing.Point(3, 16)
        Me.ReviewDataSelectorPanel.Name = "ReviewDataSelectorPanel"
        Me.ReviewDataSelectorPanel.Size = New System.Drawing.Size(857, 29)
        Me.ReviewDataSelectorPanel.TabIndex = 3
        '
        'TabPageFCEPrint
        '
        Me.TabPageFCEPrint.Controls.Add(Me.CRViewer)
        Me.TabPageFCEPrint.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFCEPrint.Name = "TabPageFCEPrint"
        Me.TabPageFCEPrint.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFCEPrint.Size = New System.Drawing.Size(869, 566)
        Me.TabPageFCEPrint.TabIndex = 1
        Me.TabPageFCEPrint.Text = "FCE Printout"
        Me.TabPageFCEPrint.UseVisualStyleBackColor = True
        '
        'CRViewer
        '
        Me.CRViewer.ActiveViewIndex = -1
        Me.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRViewer.Location = New System.Drawing.Point(3, 3)
        Me.CRViewer.Margin = New System.Windows.Forms.Padding(2)
        Me.CRViewer.Name = "CRViewer"
        Me.CRViewer.SelectionFormula = ""
        Me.CRViewer.ShowGroupTreeButton = False
        Me.CRViewer.ShowRefreshButton = False
        Me.CRViewer.Size = New System.Drawing.Size(863, 560)
        Me.CRViewer.TabIndex = 250
        Me.CRViewer.ViewTimeSelectionFormula = ""
        '
        'SSCPFCEWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 642)
        Me.Controls.Add(Me.TabControlFCE)
        Me.Controls.Add(Me.txtOrigin)
        Me.Controls.Add(Me.txtAirsNumber)
        Me.Controls.Add(Me.TBFCE)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPFCEWork"
        Me.Text = "Full Compliance Evaluation"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ReviewDataTabs.ResumeLayout(False)
        Me.TPInspections.ResumeLayout(False)
        CType(Me.dgrFCEInspections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFCE.ResumeLayout(False)
        Me.PanelFCE.PerformLayout()
        Me.TPCorrespondance.ResumeLayout(False)
        Me.PanelNotifications.ResumeLayout(False)
        CType(Me.dgrFCECorrespondance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFCENotifications.ResumeLayout(False)
        Me.PanelFCENotifications.PerformLayout()
        Me.TPTitleVACC.ResumeLayout(False)
        CType(Me.dgrFCEACC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFCE3.ResumeLayout(False)
        Me.PanelFCE3.PerformLayout()
        Me.TPReports.ResumeLayout(False)
        CType(Me.dgrFCEReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFCE2.ResumeLayout(False)
        Me.PanelFCE2.PerformLayout()
        Me.TPISMPSummaryReports.ResumeLayout(False)
        CType(Me.dgrISMPSummaryReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFCE5.ResumeLayout(False)
        Me.PanelFCE5.PerformLayout()
        Me.TPPerformanceTests.ResumeLayout(False)
        CType(Me.dgrPerformanceTests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPerformanceTests.ResumeLayout(False)
        Me.PanelPerformanceTests.PerformLayout()
        Me.TPEnforcement.ResumeLayout(False)
        CType(Me.dgrFCEEnforcement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEnforcement.ResumeLayout(False)
        Me.PanelEnforcement.PerformLayout()
        Me.FceDataGroup.ResumeLayout(False)
        Me.FceDataGroup.PerformLayout()
        Me.CompleteOrIncomplete.ResumeLayout(False)
        Me.OnsiteOrOffsite.ResumeLayout(False)
        Me.OnsiteOrOffsite.PerformLayout()
        Me.TabControlFCE.ResumeLayout(False)
        Me.TabPageFCEData.ResumeLayout(False)
        Me.FacilityInfoPanel.ResumeLayout(False)
        Me.FacilityInfoPanel.PerformLayout()
        Me.ReviewDataGroup.ResumeLayout(False)
        Me.ReviewDataSelectorPanel.ResumeLayout(False)
        Me.ReviewDataSelectorPanel.PerformLayout()
        Me.TabPageFCEPrint.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuFile As System.Windows.Forms.MenuItem
    Friend WithEvents MenuSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuHelp As System.Windows.Forms.MenuItem
    Friend WithEvents TBFCE As System.Windows.Forms.ToolBar
    Friend WithEvents TbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtOrigin As System.Windows.Forms.TextBox
    Friend WithEvents txtAirsNumber As System.Windows.Forms.TextBox
    Friend WithEvents ReviewDataTabs As System.Windows.Forms.TabControl
    Friend WithEvents TPInspections As System.Windows.Forms.TabPage
    Friend WithEvents dgrFCEInspections As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFCE As System.Windows.Forms.Panel
    Friend WithEvents llbFCEInspections As System.Windows.Forms.LinkLabel
    Friend WithEvents txtInspectionTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TPCorrespondance As System.Windows.Forms.TabPage
    Friend WithEvents PanelNotifications As System.Windows.Forms.Panel
    Friend WithEvents dgrFCECorrespondance As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFCENotifications As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNotificationTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents llbNotification As System.Windows.Forms.LinkLabel
    Friend WithEvents TPTitleVACC As System.Windows.Forms.TabPage
    Friend WithEvents dgrFCEACC As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFCE3 As System.Windows.Forms.Panel
    Friend WithEvents llbFCEACC As System.Windows.Forms.LinkLabel
    Friend WithEvents txtACCTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TPReports As System.Windows.Forms.TabPage
    Friend WithEvents dgrFCEReports As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFCE2 As System.Windows.Forms.Panel
    Friend WithEvents llbFCEReports As System.Windows.Forms.LinkLabel
    Friend WithEvents txtReportTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TPISMPSummaryReports As System.Windows.Forms.TabPage
    Friend WithEvents dgrISMPSummaryReports As System.Windows.Forms.DataGrid
    Friend WithEvents PanelFCE5 As System.Windows.Forms.Panel
    Friend WithEvents llbISMPSummaryReports As System.Windows.Forms.LinkLabel
    Friend WithEvents txtISMPReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TPPerformanceTests As System.Windows.Forms.TabPage
    Friend WithEvents dgrPerformanceTests As System.Windows.Forms.DataGrid
    Friend WithEvents PanelPerformanceTests As System.Windows.Forms.Panel
    Friend WithEvents llbPerformanceTests As System.Windows.Forms.LinkLabel
    Friend WithEvents txtPerformanceTests As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TPEnforcement As System.Windows.Forms.TabPage
    Friend WithEvents dgrFCEEnforcement As System.Windows.Forms.DataGrid
    Friend WithEvents PanelEnforcement As System.Windows.Forms.Panel
    Friend WithEvents llbFCEEnforcement As System.Windows.Forms.LinkLabel
    Friend WithEvents txtEnforcement As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents FceDataGroup As System.Windows.Forms.GroupBox
    Friend WithEvents OnsiteOrOffsite As System.Windows.Forms.GroupBox
    Friend WithEvents rdbFCENoOnsite As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFCEOnSite As System.Windows.Forms.RadioButton
    Friend WithEvents cboReviewer As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFCENumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DTPFilterStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFilterEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents CompleteOrIncomplete As System.Windows.Forms.GroupBox
    Friend WithEvents rdbFCEIncomplete As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFCEComplete As System.Windows.Forms.RadioButton
    Friend WithEvents txtFCEComments As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboFCEYear As System.Windows.Forms.ComboBox
    Friend WithEvents llbViewFCEData As System.Windows.Forms.LinkLabel
    Friend WithEvents TabControlFCE As System.Windows.Forms.TabControl
    Friend WithEvents TabPageFCEData As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents CRViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents TabPageFCEPrint As System.Windows.Forms.TabPage
    Friend WithEvents TbbPring As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuPrint As System.Windows.Forms.MenuItem
    Friend WithEvents MenuClose As System.Windows.Forms.MenuItem
    Friend WithEvents DTPFCECompleteDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ReviewDataGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FacilityInfoPanel As System.Windows.Forms.Panel
    Friend WithEvents txtFacilityInformation As System.Windows.Forms.TextBox
    Friend WithEvents labReferenceNumber As System.Windows.Forms.Label
    Friend WithEvents ReviewDataSelectorPanel As System.Windows.Forms.Panel
    Friend WithEvents MenuOpenHelp As System.Windows.Forms.MenuItem
End Class
