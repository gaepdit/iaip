Imports Oracle.DataAccess.Client


Public Class PASPFacilityFee
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim feeyear As String = Now.Year
    Dim airsnumber As String
    Dim feeTon, feeSM, feePart70, feeNSPS As Double
    Dim totalfee, part70fee, smfee, nspsfee, calculatedfee As Double
    Dim ds As DataSet
    Dim da As OracleDataAdapter
 

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
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents txtAirsNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents TBFacilitySummary As System.Windows.Forms.ToolBar
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents TTFacilitySummary As System.Windows.Forms.ToolTip
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents cboFacilityName As System.Windows.Forms.ComboBox
    Friend WithEvents cboAirsNo2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboFeeYear2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents llbViewAll2 As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlFeeCalculation As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddlClass As System.Windows.Forms.ComboBox
    Friend WithEvents chkNSPS1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkDidNotOperate As System.Windows.Forms.CheckBox
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtvoctons As System.Windows.Forms.TextBox
    Friend WithEvents txtpmtons As System.Windows.Forms.TextBox
    Friend WithEvents txtso2tons As System.Windows.Forms.TextBox
    Friend WithEvents txtnoxtons As System.Windows.Forms.TextBox
    Friend WithEvents lblvocfee As System.Windows.Forms.Label
    Friend WithEvents lblpmfee As System.Windows.Forms.Label
    Friend WithEvents lblso2fee As System.Windows.Forms.Label
    Friend WithEvents lblnoxfee As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblpart70fee As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cblNSPSExempt As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblPart70SMFee As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblNSPSFee As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblTotalFee As System.Windows.Forms.Label
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents btnAmend As System.Windows.Forms.Button
    Friend WithEvents chkNonAttainment As System.Windows.Forms.CheckBox
    Friend WithEvents chkNSPSExempt As System.Windows.Forms.CheckBox
    Friend WithEvents chkPart70SM As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblPart70 As System.Windows.Forms.Label
    Friend WithEvents lblcalculated As System.Windows.Forms.Label
    Friend WithEvents lblSM As System.Windows.Forms.Label
    Friend WithEvents pnlEmissions As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPFacilityFee))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.TBFacilitySummary = New System.Windows.Forms.ToolBar
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.TTFacilitySummary = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.cboFacilityName = New System.Windows.Forms.ComboBox
        Me.cboAirsNo2 = New System.Windows.Forms.ComboBox
        Me.cboFeeYear2 = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.llbViewAll2 = New System.Windows.Forms.LinkLabel
        Me.Label = New System.Windows.Forms.Label
        Me.pnlFeeCalculation = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblSM = New System.Windows.Forms.Label
        Me.lblcalculated = New System.Windows.Forms.Label
        Me.lblPart70 = New System.Windows.Forms.Label
        Me.btnAmend = New System.Windows.Forms.Button
        Me.btnCalculate = New System.Windows.Forms.Button
        Me.lblTotalFee = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblNSPSFee = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.cblNSPSExempt = New System.Windows.Forms.CheckedListBox
        Me.pnlEmissions = New System.Windows.Forms.GroupBox
        Me.lblpart70fee = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblnoxfee = New System.Windows.Forms.Label
        Me.lblso2fee = New System.Windows.Forms.Label
        Me.lblpmfee = New System.Windows.Forms.Label
        Me.lblvocfee = New System.Windows.Forms.Label
        Me.txtnoxtons = New System.Windows.Forms.TextBox
        Me.txtso2tons = New System.Windows.Forms.TextBox
        Me.txtpmtons = New System.Windows.Forms.TextBox
        Me.txtvoctons = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkPart70SM = New System.Windows.Forms.CheckedListBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblPart70SMFee = New System.Windows.Forms.Label
        Me.chkNSPSExempt = New System.Windows.Forms.CheckBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.ddlClass = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkNonAttainment = New System.Windows.Forms.CheckBox
        Me.chkDidNotOperate = New System.Windows.Forms.CheckBox
        Me.chkNSPS1 = New System.Windows.Forms.CheckBox
        Me.PanelFacility.SuspendLayout()
        Me.pnlFeeCalculation.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlEmissions.SuspendLayout()
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
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.mmiHelp})
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
        Me.MmiBack.Text = "Back"
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
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem5, Me.mmiClear})
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
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.Text = "-"
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
        'TBFacilitySummary
        '
        Me.TBFacilitySummary.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbClear, Me.tbbBack})
        Me.TBFacilitySummary.DropDownArrows = True
        Me.TBFacilitySummary.ImageList = Me.Image_List_All
        Me.TBFacilitySummary.Location = New System.Drawing.Point(0, 0)
        Me.TBFacilitySummary.Name = "TBFacilitySummary"
        Me.TBFacilitySummary.ShowToolTips = True
        Me.TBFacilitySummary.Size = New System.Drawing.Size(950, 28)
        Me.TBFacilitySummary.TabIndex = 139
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
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.cboFacilityName)
        Me.PanelFacility.Controls.Add(Me.cboAirsNo2)
        Me.PanelFacility.Controls.Add(Me.cboFeeYear2)
        Me.PanelFacility.Controls.Add(Me.Label9)
        Me.PanelFacility.Controls.Add(Me.Label10)
        Me.PanelFacility.Controls.Add(Me.llbViewAll2)
        Me.PanelFacility.Controls.Add(Me.Label)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(916, 36)
        Me.PanelFacility.TabIndex = 142
        '
        'cboFacilityName
        '
        Me.cboFacilityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityName.Location = New System.Drawing.Point(92, 4)
        Me.cboFacilityName.Name = "cboFacilityName"
        Me.cboFacilityName.Size = New System.Drawing.Size(215, 21)
        Me.cboFacilityName.TabIndex = 1
        '
        'cboAirsNo2
        '
        Me.cboAirsNo2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirsNo2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirsNo2.Location = New System.Drawing.Point(410, 4)
        Me.cboAirsNo2.Name = "cboAirsNo2"
        Me.cboAirsNo2.Size = New System.Drawing.Size(90, 21)
        Me.cboAirsNo2.TabIndex = 2
        '
        'cboFeeYear2
        '
        Me.cboFeeYear2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFeeYear2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFeeYear2.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFeeYear2.Location = New System.Drawing.Point(593, 3)
        Me.cboFeeYear2.Name = "cboFeeYear2"
        Me.cboFeeYear2.Size = New System.Drawing.Size(66, 23)
        Me.cboFeeYear2.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(513, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 13)
        Me.Label9.TabIndex = 139
        Me.Label9.Text = "Report Year:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(307, 7)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 13)
        Me.Label10.TabIndex = 107
        Me.Label10.Text = "OR AIRS Number:"
        '
        'llbViewAll2
        '
        Me.llbViewAll2.AutoSize = True
        Me.llbViewAll2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbViewAll2.Location = New System.Drawing.Point(667, 7)
        Me.llbViewAll2.Name = "llbViewAll2"
        Me.llbViewAll2.Size = New System.Drawing.Size(56, 13)
        Me.llbViewAll2.TabIndex = 143
        Me.llbViewAll2.TabStop = True
        Me.llbViewAll2.Text = "View Data"
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label.Location = New System.Drawing.Point(7, 7)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(73, 13)
        Me.Label.TabIndex = 106
        Me.Label.Text = "Facility Name:"
        Me.Label.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'pnlFeeCalculation
        '
        Me.pnlFeeCalculation.Controls.Add(Me.GroupBox1)
        Me.pnlFeeCalculation.Controls.Add(Me.PanelFacility)
        Me.pnlFeeCalculation.Location = New System.Drawing.Point(0, 64)
        Me.pnlFeeCalculation.Name = "pnlFeeCalculation"
        Me.pnlFeeCalculation.Size = New System.Drawing.Size(916, 515)
        Me.pnlFeeCalculation.TabIndex = 143
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblSM)
        Me.GroupBox1.Controls.Add(Me.lblcalculated)
        Me.GroupBox1.Controls.Add(Me.lblPart70)
        Me.GroupBox1.Controls.Add(Me.btnAmend)
        Me.GroupBox1.Controls.Add(Me.btnCalculate)
        Me.GroupBox1.Controls.Add(Me.lblTotalFee)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblNSPSFee)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cblNSPSExempt)
        Me.GroupBox1.Controls.Add(Me.pnlEmissions)
        Me.GroupBox1.Controls.Add(Me.chkPart70SM)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblPart70SMFee)
        Me.GroupBox1.Controls.Add(Me.chkNSPSExempt)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.ddlClass)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkNonAttainment)
        Me.GroupBox1.Controls.Add(Me.chkDidNotOperate)
        Me.GroupBox1.Controls.Add(Me.chkNSPS1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(916, 479)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Amendment to Facility Emissions"
        '
        'lblSM
        '
        Me.lblSM.AutoSize = True
        Me.lblSM.Location = New System.Drawing.Point(393, 359)
        Me.lblSM.Name = "lblSM"
        Me.lblSM.Size = New System.Drawing.Size(45, 13)
        Me.lblSM.TabIndex = 174
        Me.lblSM.Text = "Label19"
        Me.lblSM.Visible = False
        '
        'lblcalculated
        '
        Me.lblcalculated.AutoSize = True
        Me.lblcalculated.Location = New System.Drawing.Point(313, 359)
        Me.lblcalculated.Name = "lblcalculated"
        Me.lblcalculated.Size = New System.Drawing.Size(45, 13)
        Me.lblcalculated.TabIndex = 173
        Me.lblcalculated.Text = "Label19"
        Me.lblcalculated.Visible = False
        '
        'lblPart70
        '
        Me.lblPart70.AutoSize = True
        Me.lblPart70.Location = New System.Drawing.Point(233, 360)
        Me.lblPart70.Name = "lblPart70"
        Me.lblPart70.Size = New System.Drawing.Size(45, 13)
        Me.lblPart70.TabIndex = 172
        Me.lblPart70.Text = "Label19"
        Me.lblPart70.Visible = False
        '
        'btnAmend
        '
        Me.btnAmend.AutoSize = True
        Me.btnAmend.BackColor = System.Drawing.SystemColors.Control
        Me.btnAmend.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmend.ForeColor = System.Drawing.SystemColors.WindowText
        Me.btnAmend.Location = New System.Drawing.Point(547, 395)
        Me.btnAmend.Name = "btnAmend"
        Me.btnAmend.Size = New System.Drawing.Size(233, 27)
        Me.btnAmend.TabIndex = 13
        Me.btnAmend.Text = "Amend Emission Information"
        Me.btnAmend.UseVisualStyleBackColor = False
        '
        'btnCalculate
        '
        Me.btnCalculate.AutoSize = True
        Me.btnCalculate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnCalculate.Location = New System.Drawing.Point(153, 359)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(63, 23)
        Me.btnCalculate.TabIndex = 12
        Me.btnCalculate.Text = "Calculate"
        '
        'lblTotalFee
        '
        Me.lblTotalFee.AutoSize = True
        Me.lblTotalFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalFee.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalFee.Location = New System.Drawing.Point(100, 385)
        Me.lblTotalFee.Name = "lblTotalFee"
        Me.lblTotalFee.Size = New System.Drawing.Size(19, 20)
        Me.lblTotalFee.TabIndex = 168
        Me.lblTotalFee.Text = "$"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(7, 388)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(78, 13)
        Me.Label18.TabIndex = 167
        Me.Label18.Text = "Total Fee Due:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(7, 361)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(139, 13)
        Me.Label15.TabIndex = 166
        Me.Label15.Text = "23. The GRAND TOTAL....."
        '
        'lblNSPSFee
        '
        Me.lblNSPSFee.AutoSize = True
        Me.lblNSPSFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNSPSFee.ForeColor = System.Drawing.Color.Blue
        Me.lblNSPSFee.Location = New System.Drawing.Point(280, 187)
        Me.lblNSPSFee.Name = "lblNSPSFee"
        Me.lblNSPSFee.Size = New System.Drawing.Size(0, 13)
        Me.lblNSPSFee.TabIndex = 165
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(187, 187)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(60, 13)
        Me.Label17.TabIndex = 164
        Me.Label17.Text = "NSPS Fee:"
        '
        'cblNSPSExempt
        '
        Me.cblNSPSExempt.CheckOnClick = True
        Me.cblNSPSExempt.HorizontalScrollbar = True
        Me.cblNSPSExempt.Items.AddRange(New Object() {"No equipment subject to NSPS Fees was operated during the calendar year 2004.", "Subpart AAA - New Residential Wood Heaters", "Natural gas fired steam generating units permitted to fire only natural gas, prop" & _
                        "ane, or LPG that are subject to Subpart Dc.", "Metal furniture surface coating operations which permitted to use less than 1000 " & _
                        "gallons of coating (as applied) per year and are subject to Subpart EE.", "Pressure sensitive tape and label surface coating operations which permitted to i" & _
                        "nput less than 50,000 gallons of VOC per year to the coating process and are sub" & _
                        "ject to Subpart RR.", "Magnetic tape coating operations that are permitted to use less than 10,000 gallo" & _
                        "ns of solvent which are subject to Subpart SSS.", resources.GetString("cblNSPSExempt.Items"), "Municipal solid waste landfills with a design capacity of less than 2.5 million m" & _
                        "egagrams by mass of 2.5 million cubic meters by volume and are subject to Subpar" & _
                        "t WWW.", "Stationary Compression Ignition Internal Combustion Engines which are subject to " & _
                        "Subpart IIII"})
        Me.cblNSPSExempt.Location = New System.Drawing.Point(7, 203)
        Me.cblNSPSExempt.Name = "cblNSPSExempt"
        Me.cblNSPSExempt.Size = New System.Drawing.Size(773, 109)
        Me.cblNSPSExempt.TabIndex = 163
        '
        'pnlEmissions
        '
        Me.pnlEmissions.Controls.Add(Me.lblpart70fee)
        Me.pnlEmissions.Controls.Add(Me.Label12)
        Me.pnlEmissions.Controls.Add(Me.lblnoxfee)
        Me.pnlEmissions.Controls.Add(Me.lblso2fee)
        Me.pnlEmissions.Controls.Add(Me.lblpmfee)
        Me.pnlEmissions.Controls.Add(Me.lblvocfee)
        Me.pnlEmissions.Controls.Add(Me.txtnoxtons)
        Me.pnlEmissions.Controls.Add(Me.txtso2tons)
        Me.pnlEmissions.Controls.Add(Me.txtpmtons)
        Me.pnlEmissions.Controls.Add(Me.txtvoctons)
        Me.pnlEmissions.Controls.Add(Me.Label5)
        Me.pnlEmissions.Controls.Add(Me.Label7)
        Me.pnlEmissions.Controls.Add(Me.Label8)
        Me.pnlEmissions.Controls.Add(Me.Label11)
        Me.pnlEmissions.Controls.Add(Me.Label3)
        Me.pnlEmissions.Controls.Add(Me.Label6)
        Me.pnlEmissions.Controls.Add(Me.Label4)
        Me.pnlEmissions.Controls.Add(Me.Label2)
        Me.pnlEmissions.Location = New System.Drawing.Point(7, 49)
        Me.pnlEmissions.Name = "pnlEmissions"
        Me.pnlEmissions.Size = New System.Drawing.Size(726, 90)
        Me.pnlEmissions.TabIndex = 149
        Me.pnlEmissions.TabStop = False
        Me.pnlEmissions.Text = "Emissions in Tons"
        '
        'lblpart70fee
        '
        Me.lblpart70fee.AutoSize = True
        Me.lblpart70fee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpart70fee.ForeColor = System.Drawing.Color.Blue
        Me.lblpart70fee.Location = New System.Drawing.Point(140, 69)
        Me.lblpart70fee.Name = "lblpart70fee"
        Me.lblpart70fee.Size = New System.Drawing.Size(0, 13)
        Me.lblpart70fee.TabIndex = 159
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(7, 69)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(132, 13)
        Me.Label12.TabIndex = 158
        Me.Label12.Text = "20. Total Part 70 Fee:"
        '
        'lblnoxfee
        '
        Me.lblnoxfee.AutoSize = True
        Me.lblnoxfee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnoxfee.ForeColor = System.Drawing.Color.Blue
        Me.lblnoxfee.Location = New System.Drawing.Point(587, 42)
        Me.lblnoxfee.Name = "lblnoxfee"
        Me.lblnoxfee.Size = New System.Drawing.Size(0, 13)
        Me.lblnoxfee.TabIndex = 157
        '
        'lblso2fee
        '
        Me.lblso2fee.AutoSize = True
        Me.lblso2fee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblso2fee.ForeColor = System.Drawing.Color.Blue
        Me.lblso2fee.Location = New System.Drawing.Point(247, 42)
        Me.lblso2fee.Name = "lblso2fee"
        Me.lblso2fee.Size = New System.Drawing.Size(0, 13)
        Me.lblso2fee.TabIndex = 156
        '
        'lblpmfee
        '
        Me.lblpmfee.AutoSize = True
        Me.lblpmfee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpmfee.ForeColor = System.Drawing.Color.Blue
        Me.lblpmfee.Location = New System.Drawing.Point(587, 21)
        Me.lblpmfee.Name = "lblpmfee"
        Me.lblpmfee.Size = New System.Drawing.Size(0, 13)
        Me.lblpmfee.TabIndex = 155
        '
        'lblvocfee
        '
        Me.lblvocfee.AutoSize = True
        Me.lblvocfee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvocfee.ForeColor = System.Drawing.Color.Blue
        Me.lblvocfee.Location = New System.Drawing.Point(247, 21)
        Me.lblvocfee.Name = "lblvocfee"
        Me.lblvocfee.Size = New System.Drawing.Size(0, 13)
        Me.lblvocfee.TabIndex = 154
        '
        'txtnoxtons
        '
        Me.txtnoxtons.Location = New System.Drawing.Point(407, 40)
        Me.txtnoxtons.Name = "txtnoxtons"
        Me.txtnoxtons.Size = New System.Drawing.Size(83, 20)
        Me.txtnoxtons.TabIndex = 10
        '
        'txtso2tons
        '
        Me.txtso2tons.Location = New System.Drawing.Point(67, 40)
        Me.txtso2tons.Name = "txtso2tons"
        Me.txtso2tons.Size = New System.Drawing.Size(83, 20)
        Me.txtso2tons.TabIndex = 9
        '
        'txtpmtons
        '
        Me.txtpmtons.Location = New System.Drawing.Point(407, 19)
        Me.txtpmtons.Name = "txtpmtons"
        Me.txtpmtons.Size = New System.Drawing.Size(83, 20)
        Me.txtpmtons.TabIndex = 8
        '
        'txtvoctons
        '
        Me.txtvoctons.Location = New System.Drawing.Point(67, 19)
        Me.txtvoctons.Name = "txtvoctons"
        Me.txtvoctons.Size = New System.Drawing.Size(83, 20)
        Me.txtvoctons.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(520, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 149
        Me.Label5.Text = "PM Fee:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(520, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 147
        Me.Label7.Text = "NOX Fee:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(180, 42)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 13)
        Me.Label8.TabIndex = 148
        Me.Label8.Text = "SO2 Fee:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(180, 21)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 13)
        Me.Label11.TabIndex = 146
        Me.Label11.Text = "VOC Fee:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(347, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 145
        Me.Label3.Text = "17. PM"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(347, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 143
        Me.Label6.Text = "19. NOX"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 144
        Me.Label4.Text = "18. SO2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 142
        Me.Label2.Text = "16. VOC"
        '
        'chkPart70SM
        '
        Me.chkPart70SM.CheckOnClick = True
        Me.chkPart70SM.Items.AddRange(New Object() {"Part 70 Fee", "Synthetic Minor Fee"})
        Me.chkPart70SM.Location = New System.Drawing.Point(27, 146)
        Me.chkPart70SM.Name = "chkPart70SM"
        Me.chkPart70SM.Size = New System.Drawing.Size(126, 19)
        Me.chkPart70SM.TabIndex = 150
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 146)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(22, 13)
        Me.Label13.TabIndex = 151
        Me.Label13.Text = "21."
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(187, 146)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(86, 13)
        Me.Label14.TabIndex = 152
        Me.Label14.Text = "Part 70/SM Fee:"
        '
        'lblPart70SMFee
        '
        Me.lblPart70SMFee.AutoSize = True
        Me.lblPart70SMFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPart70SMFee.ForeColor = System.Drawing.Color.Blue
        Me.lblPart70SMFee.Location = New System.Drawing.Point(280, 146)
        Me.lblPart70SMFee.Name = "lblPart70SMFee"
        Me.lblPart70SMFee.Size = New System.Drawing.Size(0, 13)
        Me.lblPart70SMFee.TabIndex = 160
        '
        'chkNSPSExempt
        '
        Me.chkNSPSExempt.Location = New System.Drawing.Point(33, 184)
        Me.chkNSPSExempt.Name = "chkNSPSExempt"
        Me.chkNSPSExempt.Size = New System.Drawing.Size(94, 16)
        Me.chkNSPSExempt.TabIndex = 11
        Me.chkNSPSExempt.Text = "NSPS Exempt"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(7, 187)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(22, 13)
        Me.Label16.TabIndex = 161
        Me.Label16.Text = "22."
        '
        'ddlClass
        '
        Me.ddlClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlClass.Items.AddRange(New Object() {"", "A", "B", "SM", "PR"})
        Me.ddlClass.Location = New System.Drawing.Point(80, 21)
        Me.ddlClass.Name = "ddlClass"
        Me.ddlClass.Size = New System.Drawing.Size(101, 21)
        Me.ddlClass.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Classification:"
        '
        'chkNonAttainment
        '
        Me.chkNonAttainment.Enabled = False
        Me.chkNonAttainment.Location = New System.Drawing.Point(400, 21)
        Me.chkNonAttainment.Name = "chkNonAttainment"
        Me.chkNonAttainment.Size = New System.Drawing.Size(180, 21)
        Me.chkNonAttainment.TabIndex = 171
        Me.chkNonAttainment.Text = "1-Hour Ozone Non Attainment"
        '
        'chkDidNotOperate
        '
        Me.chkDidNotOperate.Location = New System.Drawing.Point(273, 17)
        Me.chkDidNotOperate.Name = "chkDidNotOperate"
        Me.chkDidNotOperate.Size = New System.Drawing.Size(107, 29)
        Me.chkDidNotOperate.TabIndex = 6
        Me.chkDidNotOperate.Text = "Did Not Operate:"
        '
        'chkNSPS1
        '
        Me.chkNSPS1.Location = New System.Drawing.Point(200, 21)
        Me.chkNSPS1.Name = "chkNSPS1"
        Me.chkNSPS1.Size = New System.Drawing.Size(60, 21)
        Me.chkNSPS1.TabIndex = 5
        Me.chkNSPS1.Text = "NSPS:"
        '
        'PASPFacilityFee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(950, 591)
        Me.Controls.Add(Me.pnlFeeCalculation)
        Me.Controls.Add(Me.TBFacilitySummary)
        Me.Menu = Me.MainMenu1
        Me.Name = "PASPFacilityFee"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PASP Facility Fee"
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.pnlFeeCalculation.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlEmissions.ResumeLayout(False)
        Me.pnlEmissions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub PASPFacilityFee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            LoadComboBoxes()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Load Function"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select an AIRS Number or Facility Name..."
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
    Sub LoadComboBoxes()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            'feeyear = Now.Year
            Dim dtYear As Date
            dtYear = Now
            Dim i As Integer
            If dtYear < "09/01/" & Now.Year Then
                i = Now.Year - 2
            Else
                i = Now.Year - 1
            End If
            While i > 2000
                cboFeeYear2.Items.Add(i)
                i -= 1
            End While

            'cboFeeYear.Items.Add(feeyear - 1)
            'cboFeeYear.Items.Add(feeyear - 2)
            'cboFeeYear.Items.Add(feeyear - 3)
            'cboFeeYear.Items.Add(feeyear - 4)
            'cboFeeYear.Items.Add(feeyear - 5)
            'cboFeeYear.Items.Add(feeyear - 6)

            Dim SQL As String

            SQL = "Select DISTINCT substr(" & DBNameSpace & ".FSCalculations.strairsnumber, 5) as strairsnumber, " _
            + "strfacilityname " _
            + "from " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation " _
            + "where " & DBNameSpace & ".FSCalculations.strairsnumber = " & DBNameSpace & ".APBFacilityInformation.strairsnumber(+) " _
            + "Order by strFacilityName "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "facilityInfo")

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtAIRS.Columns.Add("strairsnumber", GetType(System.String))
            dtAIRS.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtAIRS.Rows.Add(drNewRow)
            Next

            With cboAirsNo2
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

#End Region

    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll2.LinkClicked
        feeyear = cboFeeYear2.Text
        airsnumber = cboAirsNo2.Text
        LoadFeeRates()
        Dim county As String = Mid(cboAirsNo2.Text, 1, 3)

        Try

            If county = "057" Or county = "063" Or county = "067" Or county = "077" Or county = "089" _
                      Or county = "097" Or county = "113" Or county = "117" Or county = "121" _
                      Or county = "135" Or county = "151" Or county = "223" Or county = "247" Then

                chkNonAttainment.Checked = True
            Else
                chkNonAttainment.Checked = False

            End If
            GetCalculationInfo()
            ClassCalculate()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Other Functions"
    Sub LoadFeeRates()
        Dim SQL As String
        Try


            SQL = "Select smfee, pertonrate, nspsfee, part70fee " & _
            " from " & DBNameSpace & ".FSFeeRates " & _
            " where intyear = '" & feeyear & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim recExist As Boolean = dr.Read

            If recExist = True Then

                If dr.IsDBNull(0) Then
                    feeSM = 0.0
                Else
                    feeSM = dr.Item("smfee")
                End If

                If dr.IsDBNull(1) Then
                    feeTon = 0.0
                Else
                    feeTon = dr.Item("pertonrate")
                End If

                If dr.IsDBNull(2) Then
                    feeNSPS = 0.0
                Else
                    feeNSPS = dr.Item("nspsfee")
                End If

                If dr.IsDBNull(3) Then
                    feePart70 = 0.0
                Else
                    feePart70 = dr.Item("part70fee")
                End If
            Else
            End If

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub DidNotOperate()

        Try

            'If the facility has checked did not operate do thefollowing:

            btnCalculate.Enabled = False
            chkPart70SM.Enabled = False
            chkNSPSExempt.Enabled = False
            cblNSPSExempt.Enabled = False
            txtvoctons.Text = 0
            txtnoxtons.Text = 0
            txtso2tons.Text = 0
            txtpmtons.Text = 0
            part70fee = 0.0
            smfee = 0.0
            totalfee = 0.0
            nspsfee = 0.0
            calculatedfee = 0.0
            lblpart70fee.Text = String.Format("{0:C}", 0.0)
            lblTotalFee.Text = String.Format("{0:C}", 0.0)
            lblNSPSFee.Text = String.Format("{0:C}", 0.0)
            lblPart70SMFee.Text = String.Format("{0:C}", 0.0)
            lblPart70.Text = String.Format("{0:C}", 0.0)
            lblSM.Text = String.Format("{0:C}", 0.0)
            lblcalculated.Text = String.Format("{0:C}", 0.0)
            lblvocfee.Text = String.Format("{0:C}", 0.0)
            lblnoxfee.Text = String.Format("{0:C}", 0.0)
            lblso2fee.Text = String.Format("{0:C}", 0.0)
            lblpmfee.Text = String.Format("{0:C}", 0.0)

            'CalculateFees()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try


    End Sub
    Private Sub GetCalculationInfo()

        Dim SQL, nspsReason As String
        Dim i As Integer
        Dim fee As Double

        Try

            nspsReason = "000000000"

            SQL = "Select intvoctons, intpmtons, intso2tons, intnoxtons, " _
                + "numpart70fee, numsmfee, numnspsfee, numtotalfee, " _
                + "strnspsexempt, strnspsreason, stroperate, numfeerate, " _
                + "strclass1, strnsps1, strpart70, strsyntheticminor, numcalculatedfee " _
                + "from " & DBNameSpace & ".FSCalculations " _
                + "where strairsnumber = '0413" & airsnumber & "' " _
                + "and intyear = '" & feeyear & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim recExist As Boolean = dr.Read

            If recExist = True Then

                'Getting emission details from FSCalculations. 
                'This table has all the information that goes into panel fee calculations.

                If dr.IsDBNull(12) Then
                Else
                    ddlClass.Text = dr.Item("strclass1")
                End If

                If dr.IsDBNull(0) Then
                    txtvoctons.Text = ""
                    lblvocfee.Text = ""
                Else
                    txtvoctons.Text = dr.Item("intvoctons")
                    fee = PollutantVOCNOx(CInt(txtvoctons.Text))
                    lblvocfee.Text = String.Format("{0:C}", fee)
                End If

                If dr.IsDBNull(1) Then
                    txtpmtons.Text = ""
                    lblpmfee.Text = ""
                Else
                    txtpmtons.Text = dr.Item("intpmtons")
                    fee = PollutantPMSO2(CInt(txtpmtons.Text))
                    lblpmfee.Text = String.Format("{0:C}", fee)

                End If

                If dr.IsDBNull(2) Then
                    txtso2tons.Text = ""
                    lblso2fee.Text = ""
                Else
                    txtso2tons.Text = dr.Item("intso2tons")
                    fee = PollutantPMSO2(CInt(txtso2tons.Text))
                    lblso2fee.Text = String.Format("{0:C}", fee)

                End If

                If dr.IsDBNull(3) Then
                    txtnoxtons.Text = ""
                    lblnoxfee.Text = ""
                Else
                    txtnoxtons.Text = dr.Item("intnoxtons")
                    fee = PollutantVOCNOx(CInt(txtnoxtons.Text))
                    lblnoxfee.Text = String.Format("{0:C}", fee)
                End If

                If dr.IsDBNull(4) Then
                    part70fee = 0
                Else
                    part70fee = dr.Item("numpart70fee")
                    'lblPart70Fee.Text = String.Format("{0:C}", dr.Item("numtotalpart70fee"))
                End If

                If dr.IsDBNull(5) Then
                    smfee = 0
                Else
                    smfee = dr.Item("numsmfee")
                    'lblpart70SMFee.Text = String.Format("{0:C}", dr.Item("numpart70smfee"))
                End If

                If dr.IsDBNull(6) Then
                    nspsfee = 0
                Else
                    nspsfee = dr.Item("numnspsfee")
                    'lblNSPSFee.Text = String.Format("{0:C}", dr.Item("numnspsfee"))
                End If

                If dr.IsDBNull(7) Then
                    totalfee = 0
                Else
                    totalfee = dr.Item("numtotalfee")
                    'lblTotalFee.Text = String.Format("{0:C}", totalfee)
                End If

                If dr.IsDBNull(16) Then
                    calculatedfee = 0
                Else
                    calculatedfee = dr.Item("numcalculatedfee")
                End If

                If dr.Item("strnsps1") = "YES" Then
                    chkNSPS1.Checked = True
                Else
                    chkNSPS1.Checked = False
                End If

                If dr.Item("strnspsexempt") = "YES" Then
                    chkNSPSExempt.Checked = True
                    cblNSPSExempt.Enabled = True
                    'nspsfee = 0
                Else
                    chkNSPSExempt.Checked = False
                End If

                If dr.IsDBNull(9) Then
                    nspsReason = "000000000"
                Else
                    nspsReason = dr.Item("strnspsreason")
                End If

                If dr.Item("stroperate") = "NO" Then
                    chkDidNotOperate.Checked = True
                    DidNotOperate()
                Else
                    chkDidNotOperate.Checked = False
                End If

                If dr.IsDBNull(14) Then
                Else
                    If dr.Item("strpart70") = "YES" Then
                        chkPart70SM.SelectedIndex = 0
                    End If
                End If

                If dr.IsDBNull(15) Then
                Else
                    If dr.Item("strsyntheticminor") = "YES" Then
                        chkPart70SM.SelectedIndex = 1
                    End If
                End If

            Else
                MsgBox("No record exist for this AIRS Number and selected Fee Year", MsgBoxStyle.Information, "try Again")
                Exit Sub
                'If a record for this AIRS number does not exist for that year

            End If

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

            For i = 1 To nspsReason.Length
                If Mid(nspsReason, i, 1) = 1 Then
                    cblNSPSExempt.SetItemCheckState(i - 1, CheckState.Checked)
                Else
                    cblNSPSExempt.SetItemCheckState(i - 1, CheckState.Unchecked)
                End If
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub ClassCalculate()

        Try

            If chkDidNotOperate.Checked = True Then
                DidNotOperate()
            Else
                Select Case ddlClass.Text

                    Case "A"
                        pnlEmissions.Enabled = True
                        btnCalculate.Enabled = True
                        chkPart70SM.SetItemCheckState(0, CheckState.Checked)
                        If part70fee > feePart70 Then
                        Else
                            part70fee = feePart70
                        End If
                        ResetFees()

                        'lblpart70SMFee.Text = String.Format("{0:C}", part70smfee)

                    Case "B"
                        pnlEmissions.Enabled = False
                        btnCalculate.Enabled = True
                        ResetFees()

                    Case "SM"
                        pnlEmissions.Enabled = False
                        btnCalculate.Enabled = True
                        chkPart70SM.SetItemCheckState(1, CheckState.Checked)
                        If smfee > feeSM Then
                        Else
                            smfee = feeSM
                        End If
                        ResetFees()

                    Case "PR"
                        pnlEmissions.Enabled = False
                        btnCalculate.Enabled = True
                        ResetFees()

                    Case Else
                        'MsgBox("There is no information for this facility", MsgBoxStyle.Information, "try Again")

                End Select

            End If

            CalculateFees()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try

    End Sub
    Private Sub ResetFees()

        Try

            If ddlClass.Text = "A" Then
                If txtvoctons.Text = "" Then
                    txtvoctons.Text = 0
                End If
                If txtpmtons.Text = "" Then
                    txtpmtons.Text = 0
                End If
                If txtnoxtons.Text = "" Then
                    txtnoxtons.Text = 0
                End If
                If txtso2tons.Text = "" Then
                    txtso2tons.Text = 0
                End If

            Else
                txtvoctons.Text = 0
                txtnoxtons.Text = 0
                txtso2tons.Text = 0
                txtpmtons.Text = 0
                lblvocfee.Text = 0
                lblnoxfee.Text = 0
                lblso2fee.Text = 0
                lblpmfee.Text = 0
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try


    End Sub
    Function PollutantVOCNOx(ByVal tons As Integer) As Double
        Dim fee As Double

        Try


            'For 1-hour zone non-attainment counties, the VOC/NOx emissions
            'threshold is 25 tons

            If chkNonAttainment.Checked = True Then
                If tons <= 25 Then
                    fee = 0.0
                Else
                    fee = tons * feeTon
                End If
            Else
                If tons <= 100 Then
                    fee = 0.0
                Else
                    fee = tons * feeTon
                End If

            End If

            Return fee

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try


    End Function
    Function PollutantPMSO2(ByVal tons As Integer) As Double

        Dim fee As Double

        Try


            If tons <= 100 Then
                fee = 0.0
            Else
                fee = tons * feeTon
            End If

            Return fee

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try


    End Function
    Sub PerformCalculations()

        Dim fee As Double

        Try


            If IsNumeric(txtvoctons.Text) Then
                fee = PollutantVOCNOx(CInt(txtvoctons.Text))
                lblvocfee.Text = String.Format("{0:C}", fee)
            Else
                txtvoctons.Text = ""
                lblvocfee.Text = String.Format("{0:C}", 0.0)
            End If

            If IsNumeric(txtnoxtons.Text) Then
                fee = PollutantVOCNOx(CInt(txtnoxtons.Text))
                lblnoxfee.Text = String.Format("{0:C}", fee)
            Else
                txtnoxtons.Text = ""
                lblnoxfee.Text = String.Format("{0:C}", 0.0)
            End If

            If IsNumeric(txtpmtons.Text) Then
                fee = PollutantPMSO2(CInt(txtpmtons.Text))
                lblpmfee.Text = String.Format("{0:C}", fee)
            Else
                txtpmtons.Text = ""
                lblpmfee.Text = String.Format("{0:C}", 0.0)
            End If

            If IsNumeric(txtso2tons.Text) Then
                fee = PollutantPMSO2(CInt(txtso2tons.Text))
                lblso2fee.Text = String.Format("{0:C}", fee)
            Else
                txtso2tons.Text = ""
                lblso2fee.Text = String.Format("{0:C}", 0.0)
            End If

            ClassCalculate()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub CalculateFees()

        Try

            If chkDidNotOperate.Checked = True Then
                DidNotOperate()
                Exit Sub
            End If

            If lblvocfee.Text = "" Then
                lblvocfee.Text = 0
            End If
            If lblpmfee.Text = "" Then
                lblpmfee.Text = 0
            End If
            If lblnoxfee.Text = "" Then
                lblnoxfee.Text = 0
            End If
            If lblso2fee.Text = "" Then
                lblso2fee.Text = 0
            End If

            calculatedfee = CDbl(lblvocfee.Text) + CDbl(lblso2fee.Text) + _
            CDbl(lblpmfee.Text) + CDbl(lblnoxfee.Text)

            lblpart70fee.Text = String.Format("{0:C}", calculatedfee)

            If chkPart70SM.CheckedIndices.Contains(0) = True Then
                If calculatedfee < feePart70 Then
                    'lblpart70SMFee.Text = String.Format("{0:C}", 2500.0)
                    part70fee = feePart70
                Else
                    part70fee = calculatedfee
                    'lblpart70SMFee.Text = String.Format("{0:C}", part70fee)
                End If
            Else
                If ddlClass.Text <> "A" Then part70fee = 0
            End If

            If chkPart70SM.CheckedIndices.Contains(1) = True Then
                If part70fee < feeSM Then
                    smfee = feeSM
                Else
                    smfee = 0.0
                End If
            Else
                smfee = 0
            End If

            If chkPart70SM.CheckedIndices.Contains(0) = True And _
                chkPart70SM.CheckedIndices.Contains(1) = True Then
                If calculatedfee < feePart70 Then
                    part70fee = feePart70
                    'lblPart70SMFee.Text = String.Format("{0:C}", feePart70)
                Else
                    part70fee = calculatedfee
                    'lblPart70SMFee.Text = String.Format("{0:C}", part70fee)
                End If
            End If

            If part70fee < feeSM Then
                lblPart70SMFee.Text = String.Format("{0:C}", smfee)
            Else
                lblPart70SMFee.Text = String.Format("{0:C}", part70fee)
            End If

            If chkNSPS1.Checked = True Then
                chkNSPSExempt.Enabled = True
                nspsfee = feeNSPS
            Else
                chkNSPSExempt.Checked = False
                chkNSPSExempt.Enabled = False
                nspsfee = 0.0
            End If

            If chkNSPSExempt.Checked = True Then
                'chkNSPS1.Checked = True
                cblNSPSExempt.Enabled = True
                nspsfee = 0.0
            Else
                cblNSPSExempt.Enabled = False
            End If

            lblNSPSFee.Text = String.Format("{0:C}", nspsfee)

            totalfee = part70fee + nspsfee + smfee

            'The following three lines are just for writing to the datapase purpose
            'All the three labels never come into picture otherwise
            lblcalculated.Text = String.Format("{0:C}", calculatedfee)
            lblPart70.Text = String.Format("{0:C}", part70fee)
            lblSM.Text = String.Format("{0:C}", smfee)
            lblcalculated.Visible = False
            lblPart70.Visible = False
            lblSM.Visible = False

            lblTotalFee.Text = String.Format("{0:C}", totalfee)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try


    End Sub
    Private Sub ClearPage()

        Try

            cboFacilityName.Text = ""
            cboAirsNo2.Text = ""
            cboFeeYear2.Text = ""
            ddlClass.Text = ""
            chkNSPS1.Checked = False
            chkDidNotOperate.Checked = False
            chkNonAttainment.Checked = False
            chkNSPSExempt.Checked = False
            txtvoctons.Text = 0
            txtnoxtons.Text = 0
            txtso2tons.Text = 0
            txtpmtons.Text = 0
            part70fee = 0.0
            smfee = 0.0
            totalfee = 0.0
            nspsfee = 0.0
            calculatedfee = 0.0
            lblpart70fee.Text = String.Format("{0:C}", 0.0)
            lblTotalFee.Text = String.Format("{0:C}", 0.0)
            lblNSPSFee.Text = String.Format("{0:C}", 0.0)
            lblPart70SMFee.Text = String.Format("{0:C}", 0.0)
            lblPart70.Text = String.Format("{0:C}", 0.0)
            lblSM.Text = String.Format("{0:C}", 0.0)
            lblcalculated.Text = String.Format("{0:C}", 0.0)
            lblvocfee.Text = String.Format("{0:C}", 0.0)
            lblnoxfee.Text = String.Format("{0:C}", 0.0)
            lblso2fee.Text = String.Format("{0:C}", 0.0)
            lblpmfee.Text = String.Format("{0:C}", 0.0)

            chkPart70SM.SetItemCheckState(1, CheckState.Unchecked)
            chkPart70SM.SetItemCheckState(0, CheckState.Unchecked)

            Dim i As Integer
            For i = 0 To (cblNSPSExempt.Items.Count - 1)
                cblNSPSExempt.SetItemCheckState(i, CheckState.Unchecked)
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
    Private Sub btnAmend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmend.Click
        Try

            If chkNSPSExempt.Checked = True Then
                Dim chkNSPSReason As Boolean
                Dim i As Integer
                For i = 0 To (cblNSPSExempt.Items.Count - 1)
                    If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
                        chkNSPSReason = True
                        Exit For
                    End If
                Next
                If chkNSPSReason = False Then
                    MsgBox("Please select at least one checkbox for NSPS exempt", MsgBoxStyle.OkOnly, "Invalid NSPS Exempt")
                    Exit Sub
                Else
                End If
            End If

            SaveOldData()
            PerformCalculations()
            SaveFeeCalcInfo()

            MsgBox("The emission information has been updated.", MsgBoxStyle.Information, "Update Success")
            ClearPage()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Try

            PerformCalculations()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Text Box, Check Boxes Changed Events"

    Private Sub chkDidNotOperate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDidNotOperate.CheckedChanged
        Try

            If chkDidNotOperate.Checked = True Then
                DidNotOperate()
            Else
                btnCalculate.Enabled = True
                chkPart70SM.Enabled = True
                chkNSPSExempt.Enabled = True
                If chkNSPSExempt.Checked = True Then
                    cblNSPSExempt.Enabled = True
                End If
                ClassCalculate()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try

    End Sub

    Private Sub chkNSPSExempt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNSPSExempt.CheckedChanged
        Try

            If chkNSPSExempt.Checked = True Then
                cblNSPSExempt.Enabled = True
            Else
                cblNSPSExempt.Enabled = False
                Dim i As Integer
                For i = 0 To (cblNSPSExempt.Items.Count - 1)
                    cblNSPSExempt.SetItemCheckState(i, CheckState.Unchecked)
                Next
            End If

            CalculateFees()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try

    End Sub

    Private Sub ddlClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        Try

            chkDidNotOperate.Checked = False
            chkPart70SM.Enabled = True
            chkNSPSExempt.Enabled = True
            If chkNSPSExempt.Checked = True Then
                cblNSPSExempt.Enabled = True
            End If
            ClassCalculate()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try

    End Sub

#End Region
#Region "Update and Insert Functions"

    Private Sub SaveOldData()

        Dim SQL As String

        Try


            SQL = "INSERT INTO " & DBNameSpace & ".FSAmendment " _
            + "Select STRAIRSNUMBER, INTYEAR, INTVOCTONS, INTPMTONS, " _
            + "INTSO2TONS, INTNOXTONS, STRNSPSEXEMPT, STRNSPSREASON, " _
            + "STROPERATE, STRNSPSEXEMPTREASON, STRPART70, STRSYNTHETICMINOR, " _
            + "NUMCALCULATEDFEE, STRCLASS1, STRNSPS1, " _
            + "to_date('" & Format$(Now, "dd-MMM-yyyy hh:mm:ss") & "', " _
            + "'dd-mon-yyyy hh:mi:ss'), " _
            + "'" & UserGCode & "' from " & DBNameSpace & ".FSCalculations " _
            + "where strairsnumber = '0413" & cboAirsNo2.Text & "' " _
            + "and intyear = '" & cboFeeYear2.Text & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
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

    Private Sub UpdateFeeCalculations()

        Dim SQL, didnotoperate, exemptnsps, nspsreason, nsps1, exemptreasontext As String
        Dim part70, syntheticminor As String
        Dim i As Integer
        Dim cmd As OracleCommand

        Try

            nspsreason = "000000000"
            exemptreasontext = ""

            If chkNSPS1.Checked = True Then
                nsps1 = "YES"
            Else
                nsps1 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 0 Then
                part70 = "YES"
            Else
                part70 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 1 Then
                syntheticminor = "YES"
            Else
                syntheticminor = "NO"
            End If

            If chkNSPSExempt.Checked = True Then
                exemptnsps = "YES"
                nspsreason = ""
                For i = 0 To cblNSPSExempt.Items.Count - 1
                    If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
                        nspsreason = nspsreason & 1
                        exemptreasontext = exemptreasontext & cblNSPSExempt.Items(i).ToString() & "; "
                    Else
                        nspsreason = nspsreason & 0
                    End If
                Next
            Else
                exemptnsps = "NO"
            End If

            If chkDidNotOperate.Checked = True Then
                didnotoperate = "NO"
            Else
                didnotoperate = "YES"
            End If

            SQL = "Update " & DBNameSpace & ".FSCalculations set " _
            + "intvoctons = '" & CInt(txtvoctons.Text) & "', " _
            + "intnoxtons = '" & CInt(txtnoxtons.Text) & "', " _
            + "intpmtons = '" & CInt(txtpmtons.Text) & "', " _
            + "intso2tons = '" & CInt(txtso2tons.Text) & "', " _
            + "numpart70fee = '" & CDbl(lblPart70.Text) & "', " _
            + "numsmfee = '" & CDbl(lblSM.Text) & "', " _
            + "numnspsfee = '" & CDbl(lblNSPSFee.Text) & "', " _
            + "numtotalfee = '" & CDbl(lblTotalFee.Text) & "', " _
            + "strnspsexempt = '" & exemptnsps & "', " _
            + "strnspsreason = '" & nspsreason & "', " _
            + "stroperate = '" & didnotoperate & "', " _
            + "strclass1 = '" & ddlClass.Text & "', " _
            + "strnsps1 = '" & nsps1 & "', " _
            + "strnspsexemptreason = '" & Replace(exemptreasontext, "'", "''") & "', " _
            + "strpart70 = '" & part70 & "', " _
            + "strsyntheticminor = '" & syntheticminor & "', " _
            + "numcalculatedfee = '" & CDbl(lblcalculated.Text) & "' " _
            + "where strairsnumber = '0413" & cboAirsNo2.Text & "' " _
            + "and intyear = '" & cboFeeYear2.Text & "' "


            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Private Sub SaveFeeCalcInfo()
        Try


            If chkNSPSExempt.Checked = True Then
                Dim chkNSPSReason As Boolean
                Dim i As Integer
                For i = 0 To (cblNSPSExempt.Items.Count - 1)
                    If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
                        chkNSPSReason = True
                        Exit For
                    End If
                Next
                If chkNSPSReason = False Then
                    MsgBox("Please select at least one checkbox for NSPS exempt", MsgBoxStyle.OkOnly, "Invalid NSPS Exempt")
                    Exit Sub
                Else
                End If
            End If

            'This sub will first check if a record for this facility exists in 
            'the table FSCalculations for the fee year If the record exists it
            'will update the record or else it will insert a new record for the
            'facility in the table

            Dim SQL As String

            SQL = "Select strairsnumber " _
         + "from " & DBNameSpace & ".FSCalculations " _
         + "where strairsnumber = '0413" & cboAirsNo2.Text & "' " _
         + "and intyear = '" & cboFeeYear2.Text & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim recExist As Boolean = dr.Read

            If recExist = True Then
                UpdateFeeCalculations()
            Else
                InsertFeeCalculations()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Private Sub InsertFeeCalculations()
        Dim SQL, didnotoperate, exemptnsps, exemptreasontext As String
        Dim part70, syntheticminor As String
        Dim nsps1, nspsreason As String
        Dim i As Integer

        Try


            nspsreason = "000000000"
            exemptreasontext = ""
            If chkNSPS1.Checked = True Then
                nsps1 = "YES"
            Else
                nsps1 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 0 Then
                part70 = "YES"
            Else
                part70 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 1 Then
                syntheticminor = "YES"
            Else
                syntheticminor = "NO"
            End If

            If chkNSPSExempt.Checked = True Then
                exemptnsps = "YES"
                nspsreason = ""
                For i = 0 To cblNSPSExempt.Items.Count - 1
                    If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
                        nspsreason = nspsreason & 1
                        exemptreasontext = exemptreasontext & cblNSPSExempt.Items(i).ToString() & "; "
                    Else
                        nspsreason = nspsreason & 0
                    End If
                Next
            Else
                exemptnsps = "NO"
            End If

            If chkDidNotOperate.Checked = True Then
                didnotoperate = "NO"
            Else
                didnotoperate = "YES"
            End If

            SQL = "Insert into " & DBNameSpace & ".FSCalculations " _
            + "(strairsnumber, intyear, " _
            + "intvoctons, intpmtons, intso2tons, intnoxtons, " _
            + "numpart70fee, numsmfee, numnspsfee, " _
            + "numtotalfee, strnspsexempt, strnspsreason, stroperate, " _
            + "strclass1, strnsps1, strnspsexemptreason, strpart70, " _
            + "strsyntheticminor, numcalculatedfee) " _
            + "values('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " _
            + "'" & CInt(txtvoctons.Text) & "', '" & CInt(txtpmtons.Text) & "', " _
            + "'" & CInt(txtso2tons.Text) & "', '" & CInt(txtnoxtons.Text) & "', " _
            + "'" & CDbl(lblPart70.Text) & "', '" & CDbl(lblSM.Text) & "', " _
            + "'" & CDbl(lblNSPSFee.Text) & "', " _
            + "'" & CDbl(lblTotalFee.Text) & "', '" & exemptnsps & "', " _
            + "'" & nspsreason & "', '" & didnotoperate & "', " _
            + "'" & ddlClass.Text & "', '" & nsps1 & "', " _
            + "'" & Replace(exemptreasontext, "'", "''") & "', " _
            + "'" & part70 & "', '" & syntheticminor & "', " _
            + "'" & CDbl(lblcalculated.Text) & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    'Function validatePnlFeeCalc()

    '    Dim validate As Boolean

    '    validate = True

    '    If chkDidNotOperate.Checked = False And ddlClass.Text = "A" Then

    '        For Each ctrl As Control In pnlFeeCalculation.Controls

    '            If TypeOf ctrl Is TextBox Then
    '                If CType(ctrl, TextBox).Text = "" Or _
    '                Not IsNumeric(CType(ctrl, TextBox).Text) Or _
    '                CType(ctrl, TextBox).Text > 4000 Or _
    '                CType(ctrl, TextBox).Text < 0 Then
    '                    validate = False
    '                    'ErrorMessage()
    '                    Return validate
    '                End If
    '            Else
    '                validate = True
    '            End If
    '        Next
    '    End If

    '    If chkNSPSExempt.Checked = True Then
    '        Dim i As Integer
    '        For i = 0 To (cblNSPSExempt.Items.Count - 1)
    '            If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
    '                validate = True
    '                Exit For
    '            End If
    '            validate = False
    '        Next
    '        If validate = False Then
    '            MsgBox("Please select at least one checkbox for NSPS exempt", MsgBoxStyle.OKOnly, "Invalid NSPS Exempt")
    '            'ErrorMessage()
    '            Return validate
    '        Else
    '        End If
    '    End If

    '    Return validate

    'End Function

#End Region
    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Try

            Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
                Case 0
                    ClearPage()
                Case 1
                    Me.Close()
                Case Else
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub PASPFacilityFee_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub


    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiExit.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class
