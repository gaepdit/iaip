Imports System.Data.OracleClient


Public Class PASPFeeDeposits
    Inherits System.Windows.Forms.Form
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Panel1temp As String

    Dim dsWorkEnTry As DataSet
    Dim daWorkEnTry As OracleDataAdapter
    Dim recExist As Boolean
    Dim feeyear As String
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvDeposit As System.Windows.Forms.DataGridView
    Friend WithEvents cboFeeYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbbExportToExcel As System.Windows.Forms.ToolBarButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbAllInvoices As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDeposited As System.Windows.Forms.RadioButton
    Friend WithEvents mtbAirsNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label

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
    Friend WithEvents mmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TBFacilitySummary As System.Windows.Forms.ToolBar
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents llbViewAll As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labReferenceNumber As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboDepositNo As System.Windows.Forms.ComboBox
    Friend WithEvents cboAirsNo As System.Windows.Forms.ComboBox
    Friend WithEvents lblFlag As System.Windows.Forms.Label
    Friend WithEvents txtPayId As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDepositdate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboPayType As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label435 As System.Windows.Forms.Label
    Friend WithEvents Label434 As System.Windows.Forms.Label
    Friend WithEvents Label433 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteEnTry As System.Windows.Forms.Button
    Friend WithEvents btnUpdateEnTry As System.Windows.Forms.Button
    Friend WithEvents btnAddNewEnTry As System.Windows.Forms.Button
    Friend WithEvents txtAirsNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPayment As System.Windows.Forms.TextBox
    Friend WithEvents txtbatchNo As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPFeeDeposits))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem13 = New System.Windows.Forms.MenuItem
        Me.mmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBFacilitySummary = New System.Windows.Forms.ToolBar
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.tbbExportToExcel = New System.Windows.Forms.ToolBarButton
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rdbAllInvoices = New System.Windows.Forms.RadioButton
        Me.rdbDeposited = New System.Windows.Forms.RadioButton
        Me.txtCount = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.cboFeeYear = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblFlag = New System.Windows.Forms.Label
        Me.cboAirsNo = New System.Windows.Forms.ComboBox
        Me.cboDepositNo = New System.Windows.Forms.ComboBox
        Me.llbViewAll = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.labReferenceNumber = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPayId = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.mtbAirsNo = New System.Windows.Forms.MaskedTextBox
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnDeleteEnTry = New System.Windows.Forms.Button
        Me.btnUpdateEnTry = New System.Windows.Forms.Button
        Me.btnAddNewEnTry = New System.Windows.Forms.Button
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDepositdate = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboPayType = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label435 = New System.Windows.Forms.Label
        Me.Label434 = New System.Windows.Forms.Label
        Me.Label433 = New System.Windows.Forms.Label
        Me.txtCheckNo = New System.Windows.Forms.TextBox
        Me.txtDepositNo = New System.Windows.Forms.TextBox
        Me.txtPayment = New System.Windows.Forms.TextBox
        Me.txtbatchNo = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtYear = New System.Windows.Forms.TextBox
        Me.txtAirsNo = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dgvDeposit = New System.Windows.Forms.DataGridView
        Me.PanelFacility.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.mmiHelp})
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
        Me.mmiBack.Text = "Back"
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
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem6, Me.mmiClear})
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
        'MenuItem6
        '
        Me.MenuItem6.Index = 3
        Me.MenuItem6.Text = "-"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 4
        Me.mmiClear.Text = "Clear"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "View"
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
        'TBFacilitySummary
        '
        Me.TBFacilitySummary.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbClear, Me.tbbBack, Me.tbbExportToExcel})
        Me.TBFacilitySummary.DropDownArrows = True
        Me.TBFacilitySummary.ImageList = Me.Image_List_All
        Me.TBFacilitySummary.Location = New System.Drawing.Point(0, 0)
        Me.TBFacilitySummary.Name = "TBFacilitySummary"
        Me.TBFacilitySummary.ShowToolTips = True
        Me.TBFacilitySummary.Size = New System.Drawing.Size(969, 28)
        Me.TBFacilitySummary.TabIndex = 140
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
        'tbbExportToExcel
        '
        Me.tbbExportToExcel.ImageIndex = 14
        Me.tbbExportToExcel.Name = "tbbExportToExcel"
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.Panel4)
        Me.PanelFacility.Controls.Add(Me.txtCount)
        Me.PanelFacility.Controls.Add(Me.Label12)
        Me.PanelFacility.Controls.Add(Me.cboFeeYear)
        Me.PanelFacility.Controls.Add(Me.Label11)
        Me.PanelFacility.Controls.Add(Me.lblFlag)
        Me.PanelFacility.Controls.Add(Me.cboAirsNo)
        Me.PanelFacility.Controls.Add(Me.cboDepositNo)
        Me.PanelFacility.Controls.Add(Me.llbViewAll)
        Me.PanelFacility.Controls.Add(Me.Label1)
        Me.PanelFacility.Controls.Add(Me.labReferenceNumber)
        Me.PanelFacility.Controls.Add(Me.Label2)
        Me.PanelFacility.Controls.Add(Me.Label3)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(969, 57)
        Me.PanelFacility.TabIndex = 271
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.rdbAllInvoices)
        Me.Panel4.Controls.Add(Me.rdbDeposited)
        Me.Panel4.Location = New System.Drawing.Point(670, 6)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(122, 42)
        Me.Panel4.TabIndex = 170
        '
        'rdbAllInvoices
        '
        Me.rdbAllInvoices.AutoSize = True
        Me.rdbAllInvoices.Location = New System.Drawing.Point(3, 22)
        Me.rdbAllInvoices.Name = "rdbAllInvoices"
        Me.rdbAllInvoices.Size = New System.Drawing.Size(78, 17)
        Me.rdbAllInvoices.TabIndex = 1
        Me.rdbAllInvoices.Text = "All invoices"
        Me.rdbAllInvoices.UseVisualStyleBackColor = True
        '
        'rdbDeposited
        '
        Me.rdbDeposited.AutoSize = True
        Me.rdbDeposited.Checked = True
        Me.rdbDeposited.Location = New System.Drawing.Point(3, 3)
        Me.rdbDeposited.Name = "rdbDeposited"
        Me.rdbDeposited.Size = New System.Drawing.Size(116, 17)
        Me.rdbDeposited.TabIndex = 0
        Me.rdbDeposited.TabStop = True
        Me.rdbDeposited.Text = "Deposited Invoices"
        Me.rdbDeposited.UseVisualStyleBackColor = True
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(922, 6)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(42, 20)
        Me.txtCount.TabIndex = 122
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(878, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 13)
        Me.Label12.TabIndex = 121
        Me.Label12.Text = "Count:"
        '
        'cboFeeYear
        '
        Me.cboFeeYear.FormattingEnabled = True
        Me.cboFeeYear.Location = New System.Drawing.Point(543, 4)
        Me.cboFeeYear.Name = "cboFeeYear"
        Me.cboFeeYear.Size = New System.Drawing.Size(121, 21)
        Me.cboFeeYear.TabIndex = 120
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(469, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 13)
        Me.Label11.TabIndex = 119
        Me.Label11.Text = "Or Fee year:"
        '
        'lblFlag
        '
        Me.lblFlag.Location = New System.Drawing.Point(540, 6)
        Me.lblFlag.Name = "lblFlag"
        Me.lblFlag.Size = New System.Drawing.Size(83, 17)
        Me.lblFlag.TabIndex = 118
        Me.lblFlag.Visible = False
        '
        'cboAirsNo
        '
        Me.cboAirsNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboAirsNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirsNo.Location = New System.Drawing.Point(353, 4)
        Me.cboAirsNo.Name = "cboAirsNo"
        Me.cboAirsNo.Size = New System.Drawing.Size(99, 21)
        Me.cboAirsNo.TabIndex = 117
        '
        'cboDepositNo
        '
        Me.cboDepositNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboDepositNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDepositNo.Location = New System.Drawing.Point(107, 4)
        Me.cboDepositNo.Name = "cboDepositNo"
        Me.cboDepositNo.Size = New System.Drawing.Size(126, 21)
        Me.cboDepositNo.TabIndex = 116
        '
        'llbViewAll
        '
        Me.llbViewAll.AutoSize = True
        Me.llbViewAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbViewAll.Location = New System.Drawing.Point(806, 7)
        Me.llbViewAll.Name = "llbViewAll"
        Me.llbViewAll.Size = New System.Drawing.Size(56, 13)
        Me.llbViewAll.TabIndex = 115
        Me.llbViewAll.TabStop = True
        Me.llbViewAll.Text = "View Data"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(240, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "OR  AIRS Number:"
        '
        'labReferenceNumber
        '
        Me.labReferenceNumber.AutoSize = True
        Me.labReferenceNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labReferenceNumber.Location = New System.Drawing.Point(8, 8)
        Me.labReferenceNumber.Name = "labReferenceNumber"
        Me.labReferenceNumber.Size = New System.Drawing.Size(86, 13)
        Me.labReferenceNumber.TabIndex = 106
        Me.labReferenceNumber.Text = "Deposit Number:"
        Me.labReferenceNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 106
        Me.Label2.Text = "Deposit Number:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(240, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 107
        Me.Label3.Text = "OR  AIRS Number:"
        '
        'txtPayId
        '
        Me.txtPayId.Location = New System.Drawing.Point(353, 140)
        Me.txtPayId.Name = "txtPayId"
        Me.txtPayId.Size = New System.Drawing.Size(83, 22)
        Me.txtPayId.TabIndex = 0
        Me.txtPayId.Text = "PayId"
        Me.txtPayId.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.mtbAirsNo)
        Me.GroupBox4.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.btnDeleteEnTry)
        Me.GroupBox4.Controls.Add(Me.btnUpdateEnTry)
        Me.GroupBox4.Controls.Add(Me.btnAddNewEnTry)
        Me.GroupBox4.Controls.Add(Me.txtComments)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtDepositdate)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.cboPayType)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label435)
        Me.GroupBox4.Controls.Add(Me.Label434)
        Me.GroupBox4.Controls.Add(Me.Label433)
        Me.GroupBox4.Controls.Add(Me.txtCheckNo)
        Me.GroupBox4.Controls.Add(Me.txtDepositNo)
        Me.GroupBox4.Controls.Add(Me.txtPayment)
        Me.GroupBox4.Controls.Add(Me.txtbatchNo)
        Me.GroupBox4.Controls.Add(Me.txtPayId)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtYear)
        Me.GroupBox4.Controls.Add(Me.txtAirsNo)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(0, 57)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(969, 224)
        Me.GroupBox4.TabIndex = 272
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Payment Info"
        '
        'mtbAirsNo
        '
        Me.mtbAirsNo.Location = New System.Drawing.Point(353, 29)
        Me.mtbAirsNo.Mask = "000_00000"
        Me.mtbAirsNo.Name = "mtbAirsNo"
        Me.mtbAirsNo.Size = New System.Drawing.Size(83, 22)
        Me.mtbAirsNo.TabIndex = 170
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(531, 51)
        Me.txtInvoiceNo.MaxLength = 8
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(210, 20)
        Me.txtInvoiceNo.TabIndex = 169
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(528, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 15)
        Me.Label10.TabIndex = 168
        Me.Label10.Text = "Invoice No:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(432, 113)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 167
        Me.Label9.Text = "(ex. 2006)"
        '
        'btnDeleteEnTry
        '
        Me.btnDeleteEnTry.Location = New System.Drawing.Point(346, 186)
        Me.btnDeleteEnTry.Name = "btnDeleteEnTry"
        Me.btnDeleteEnTry.Size = New System.Drawing.Size(114, 23)
        Me.btnDeleteEnTry.TabIndex = 11
        Me.btnDeleteEnTry.Text = "Delete Entry"
        '
        'btnUpdateEnTry
        '
        Me.btnUpdateEnTry.Location = New System.Drawing.Point(193, 186)
        Me.btnUpdateEnTry.Name = "btnUpdateEnTry"
        Me.btnUpdateEnTry.Size = New System.Drawing.Size(113, 23)
        Me.btnUpdateEnTry.TabIndex = 10
        Me.btnUpdateEnTry.Text = "Update Entry"
        '
        'btnAddNewEnTry
        '
        Me.btnAddNewEnTry.Location = New System.Drawing.Point(40, 186)
        Me.btnAddNewEnTry.Name = "btnAddNewEnTry"
        Me.btnAddNewEnTry.Size = New System.Drawing.Size(113, 23)
        Me.btnAddNewEnTry.TabIndex = 9
        Me.btnAddNewEnTry.Text = "Add New Entry"
        '
        'txtComments
        '
        Me.txtComments.AcceptsTab = True
        Me.txtComments.Location = New System.Drawing.Point(100, 140)
        Me.txtComments.MaxLength = 4000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComments.Size = New System.Drawing.Size(247, 34)
        Me.txtComments.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 164
        Me.Label4.Text = "Comments:"
        '
        'txtDepositdate
        '
        Me.txtDepositdate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepositdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositdate.Location = New System.Drawing.Point(100, 28)
        Me.txtDepositdate.MaxLength = 20
        Me.txtDepositdate.Name = "txtDepositdate"
        Me.txtDepositdate.Size = New System.Drawing.Size(144, 20)
        Me.txtDepositdate.TabIndex = 163
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 162
        Me.Label5.Text = "Deposit Date:"
        '
        'cboPayType
        '
        Me.cboPayType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPayType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPayType.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPayType.Location = New System.Drawing.Point(100, 109)
        Me.cboPayType.Name = "cboPayType"
        Me.cboPayType.Size = New System.Drawing.Size(147, 23)
        Me.cboPayType.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(7, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 13)
        Me.Label13.TabIndex = 160
        Me.Label13.Text = "Pay Type:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(267, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 159
        Me.Label7.Text = "Batch Number:"
        '
        'Label435
        '
        Me.Label435.AutoSize = True
        Me.Label435.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label435.Location = New System.Drawing.Point(7, 85)
        Me.Label435.Name = "Label435"
        Me.Label435.Size = New System.Drawing.Size(86, 13)
        Me.Label435.TabIndex = 155
        Me.Label435.Text = "Deposit Number:"
        '
        'Label434
        '
        Me.Label434.AutoSize = True
        Me.Label434.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label434.Location = New System.Drawing.Point(267, 60)
        Me.Label434.Name = "Label434"
        Me.Label434.Size = New System.Drawing.Size(81, 13)
        Me.Label434.TabIndex = 154
        Me.Label434.Text = "Check Number:"
        '
        'Label433
        '
        Me.Label433.AutoSize = True
        Me.Label433.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label433.Location = New System.Drawing.Point(7, 57)
        Me.Label433.Name = "Label433"
        Me.Label433.Size = New System.Drawing.Size(70, 13)
        Me.Label433.TabIndex = 153
        Me.Label433.Text = "Amount Paid:"
        '
        'txtCheckNo
        '
        Me.txtCheckNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCheckNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheckNo.Location = New System.Drawing.Point(353, 57)
        Me.txtCheckNo.MaxLength = 15
        Me.txtCheckNo.Name = "txtCheckNo"
        Me.txtCheckNo.Size = New System.Drawing.Size(144, 20)
        Me.txtCheckNo.TabIndex = 3
        '
        'txtDepositNo
        '
        Me.txtDepositNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepositNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositNo.Location = New System.Drawing.Point(100, 83)
        Me.txtDepositNo.MaxLength = 20
        Me.txtDepositNo.Name = "txtDepositNo"
        Me.txtDepositNo.Size = New System.Drawing.Size(144, 20)
        Me.txtDepositNo.TabIndex = 4
        '
        'txtPayment
        '
        Me.txtPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment.Location = New System.Drawing.Point(100, 55)
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.Size = New System.Drawing.Size(144, 20)
        Me.txtPayment.TabIndex = 2
        '
        'txtbatchNo
        '
        Me.txtbatchNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtbatchNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbatchNo.Location = New System.Drawing.Point(353, 83)
        Me.txtbatchNo.MaxLength = 20
        Me.txtbatchNo.Name = "txtbatchNo"
        Me.txtbatchNo.Size = New System.Drawing.Size(144, 20)
        Me.txtbatchNo.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(267, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 164
        Me.Label8.Text = "Fee Year:"
        '
        'txtYear
        '
        Me.txtYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.Location = New System.Drawing.Point(353, 109)
        Me.txtYear.MaxLength = 4
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(74, 20)
        Me.txtYear.TabIndex = 7
        '
        'txtAirsNo
        '
        Me.txtAirsNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAirsNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAirsNo.Location = New System.Drawing.Point(792, 95)
        Me.txtAirsNo.MaxLength = 8
        Me.txtAirsNo.Name = "txtAirsNo"
        Me.txtAirsNo.Size = New System.Drawing.Size(144, 20)
        Me.txtAirsNo.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(267, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 166
        Me.Label6.Text = "AIRS Number:"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 28)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PanelFacility)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvDeposit)
        Me.SplitContainer1.Size = New System.Drawing.Size(969, 554)
        Me.SplitContainer1.SplitterDistance = 281
        Me.SplitContainer1.SplitterWidth = 10
        Me.SplitContainer1.TabIndex = 273
        '
        'dgvDeposit
        '
        Me.dgvDeposit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDeposit.Location = New System.Drawing.Point(0, 0)
        Me.dgvDeposit.Name = "dgvDeposit"
        Me.dgvDeposit.ReadOnly = True
        Me.dgvDeposit.Size = New System.Drawing.Size(969, 263)
        Me.dgvDeposit.TabIndex = 271
        '
        'PASPFeeDeposits
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(969, 582)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TBFacilitySummary)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "PASPFeeDeposits"
        Me.Text = "PASP Fee Deposits"
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub PASPFeeDeposits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            CreateStatusBar()

            'If Now.Date < "09/01/" & Now.Year Then
            '    feeyear = Now.Year - 2
            'Else
            '    feeyear = Now.Year - 1
            'End If
            feeyear = 2005
            txtDepositdate.Text = Format$(Now, "dd-MMM-yyyy")

            FillComboBoxes()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Deposit Number or AIRS Number..."
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

        End Try

    End Sub
    Private Sub FillComboBoxes()
        Try

            cboDepositNo.Items.Add("")
            cboAirsNo.Items.Add("")
            cboFeeYear.Items.Add("")

            SQL = "Select distinct strdepositno from " & connNameSpace & ".FSAddPaid " _
           + "order by strdepositno"

            cmd = New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            dr.Read()
            Do
                cboDepositNo.Items.Add(dr("strdepositno"))

            Loop While dr.Read
            dr.Close()

            SQL2 = "Select distinct substr(strairsnumber, 5) as strairsnumber " _
           + "from " & connNameSpace & ".FSAddPaid order by strairsnumber"

            cmd2 = New OracleCommand(SQL2, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr2 = cmd2.ExecuteReader

            dr2.Read()
            Do
                cboAirsNo.Items.Add(dr2("strairsnumber"))

            Loop While dr2.Read
            dr2.Close()

            SQL3 = "Select paytype " & _
            "from " & connNameSpace & ".FSPayType " & _
            "order by paytype"

            cmd3 = New OracleCommand(SQL3, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr3 = cmd3.ExecuteReader

            dr3.Read()
            Do
                cboPayType.Items.Add(dr3("paytype"))

            Loop While dr3.Read
            dr3.Close()

            SQL = "Select " & _
            "distinct(intYear) as intYear " & _
            "from " & connNameSpace & ".FSAddPaid " & _
            "order by intyear desc "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                cboFeeYear.Items.Add(dr.Item("intYear"))
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked
        Try

            LoadDataGrid()
            txtCount.Text = dgvDeposit.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadDataGrid()

        Dim SQL As String = ""
        Try

            dsWorkEnTry = New DataSet

            If cboDepositNo.Text <> "" Then
                SQL = "Select " & _
                "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                "strDepositNo, datPayDate, " & _
                "numPayment, intYear, " & _
                "strCheckNo, strBatchNo, " & _
                "strPayType, intPayId, " & _
                "strComments, strInvoiceNo " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where strDepositNo = '" & cboDepositNo.Text & "' " & _
                "order by strBatchNo "

            Else
                If cboAirsNo.Text <> "" Then
                    SQL = "Select " & _
                   "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                   "strDepositNo, datPayDate, " & _
                   "numPayment, intYear, " & _
                   "strCheckNo, strBatchNo, " & _
                   "strPayType, intPayId, " & _
                   "strComments, strInvoiceNo " & _
                   "from " & connNameSpace & ".FSAddPaid " & _
                   "where strairsnumber = '0413" & cboAirsNo.Text & "' " & _
                   "order by strBatchNo "
                Else
                    If cboFeeYear.Text <> "" Then
                        If rdbDeposited.Checked = True Then
                            SQL = "Select " & _
                           "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                           "strDepositNo, datPayDate, " & _
                           "numPayment, intYear, " & _
                           "strCheckNo, strBatchNo, " & _
                           "strPayType, intPayId, " & _
                           "strComments, strInvoiceNo " & _
                           "from " & connNameSpace & ".FSAddPaid " & _
                           "where intYear = '" & cboFeeYear.Text & "' " & _
                           "and strDepositNo is Not Null " & _
                           "order by strBatchNo "
                        Else
                            SQL = "Select " & _
                           "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                           "strDepositNo, datPayDate, " & _
                           "numPayment, intYear, " & _
                           "strCheckNo, strBatchNo, " & _
                           "strPayType, intPayId, " & _
                           "strComments, strInvoiceNo " & _
                           "from " & connNameSpace & ".FSAddPaid " & _
                           "where intYear = '" & cboFeeYear.Text & "' " & _
                           "order by strBatchNo "
                        End If
                    Else
                        MsgBox("Please select a Deposit Number or AIRS Number or Fee Year first", MsgBoxStyle.OkOnly, "Incorrect Selection")
                    End If
                End If
            End If

            If SQL <> "" Then
                daWorkEnTry = New OracleDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                daWorkEnTry.Fill(dsWorkEnTry, "tblWorkEnTry")
                dgvDeposit.DataSource = dsWorkEnTry
                dgvDeposit.DataMember = "tblWorkEntry"

                dgvDeposit.RowHeadersVisible = False
                dgvDeposit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvDeposit.AllowUserToResizeColumns = True
                dgvDeposit.AllowUserToAddRows = False
                dgvDeposit.AllowUserToDeleteRows = False
                dgvDeposit.AllowUserToOrderColumns = True
                dgvDeposit.AllowUserToResizeRows = True
                dgvDeposit.ColumnHeadersHeight = "35"
                dgvDeposit.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvDeposit.Columns("strairsnumber").DisplayIndex = 0
                dgvDeposit.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvDeposit.Columns("strdepositno").DisplayIndex = 1
                dgvDeposit.Columns("datpaydate").HeaderText = "Deposit Date"
                dgvDeposit.Columns("datpaydate").DisplayIndex = 2
                dgvDeposit.Columns("datpaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvDeposit.Columns("numpayment").HeaderText = "Payment"
                dgvDeposit.Columns("numpayment").DisplayIndex = 3
                dgvDeposit.Columns("intyear").HeaderText = "Year"
                dgvDeposit.Columns("intyear").DisplayIndex = 4
                dgvDeposit.Columns("strcheckno").HeaderText = "Check Number"
                dgvDeposit.Columns("strcheckno").DisplayIndex = 5
                dgvDeposit.Columns("strbatchno").HeaderText = "Batch Number"
                dgvDeposit.Columns("strbatchno").DisplayIndex = 6
                dgvDeposit.Columns("strpaytype").HeaderText = "Payment Type"
                dgvDeposit.Columns("strpaytype").DisplayIndex = 7
                dgvDeposit.Columns("intpayid").HeaderText = "Pay ID"
                dgvDeposit.Columns("intpayid").DisplayIndex = 8
                dgvDeposit.Columns("intpayid").Visible = False
                dgvDeposit.Columns("strcomments").HeaderText = "Comments"
                dgvDeposit.Columns("strcomments").DisplayIndex = 9
                dgvDeposit.Columns("strcomments").Visible = False
                dgvDeposit.Columns("strinvoiceno").HeaderText = "Invoce Number"
                dgvDeposit.Columns("strinvoiceno").DisplayIndex = 10
                dgvDeposit.Columns("strinvoiceno").Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ExportToExcel()
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i As Integer
            Dim j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvDeposit.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvDeposit.ColumnCount - 1
                        .Cells(1, i + 1) = dgvDeposit.Columns(i).HeaderText.ToString
                    Next
                    For i = 0 To dgvDeposit.ColumnCount - 1
                        For j = 0 To dgvDeposit.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvDeposit.Item(i, j).Value.ToString
                        Next
                    Next
                End With

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvDeposit_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDeposit.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvDeposit.HitTest(e.X, e.Y)
        Dim temp As String

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvDeposit.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvDeposit.Columns(1).HeaderText

                    If dgvDeposit.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAirsNo.Text = dgvDeposit(0, hti.RowIndex).Value
                        txtPayId.Text = dgvDeposit(8, hti.RowIndex).Value
                        If IsDBNull(dgvDeposit(1, hti.RowIndex).Value) Then
                            txtDepositNo.Clear()
                        Else
                            txtDepositNo.Text = dgvDeposit(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(2, hti.RowIndex).Value) Then
                            txtDepositdate.Clear()
                        Else
                            txtDepositdate.Text = dgvDeposit(2, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvDeposit(3, hti.RowIndex).Value) Then
                            txtPayment.Clear()
                        Else
                            txtPayment.Text = dgvDeposit(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(4, hti.RowIndex).Value) Then
                            txtYear.Clear()
                        Else
                            txtYear.Text = dgvDeposit(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(5, hti.RowIndex).Value) Then
                            txtCheckNo.Clear()
                        Else
                            txtCheckNo.Text = dgvDeposit(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(6, hti.RowIndex).Value) Then
                            txtbatchNo.Clear()
                        Else
                            txtbatchNo.Text = dgvDeposit(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(7, hti.RowIndex).Value) Then
                            cboPayType.Text = ""
                        Else
                            cboPayType.Text = ""
                            cboPayType.Text = dgvDeposit(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(9, hti.RowIndex).Value) Then
                            txtComments.Clear()
                        Else
                            txtComments.Text = dgvDeposit(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(10, hti.RowIndex).Value) Then
                            txtInvoiceNo.Clear()
                        Else
                            txtInvoiceNo.Text = dgvDeposit(10, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try


    End Sub
    Private Sub btnAddNewEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewEnTry.Click
        Try


            ValidateEntry()

            SQL = "Insert into " & connNameSpace & ".FSAddPaid (strairsnumber, intyear, numpayment, " _
             + "datpaydate, strcheckno, strdepositno, strpaytype, strbatchno, " _
             + "strentryperson, strcomments, intpayid) values('0413" & mtbAirsNo.Text & "', " _
             + " " & CInt(txtYear.Text) & " ,  " & CDec(txtPayment.Text) & " , '" & txtDepositdate.Text & "', " _
             + "'" & txtCheckNo.Text & "', '" & txtDepositNo.Text & "', " _
             + "'" & cboPayType.Text & "', '" & txtbatchNo.Text & "', " _
             + "'" & UserGCode & "', '" & Replace(txtComments.Text, "'", "''") & "', " & connNameSpace & ".seqfsdeposit.nextval)"

            cmd = New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record was added successfully", MsgBoxStyle.Information, "Entry Success!")
            ClearPage()
            LoadDataGrid()
            FillComboBoxes()

        Catch ex As Exception
            ErrorReport(SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ValidateEntry()
        Try

            If mtbAirsNo.Text.Length <> 8 Then
                MsgBox("Please enter a valid AIRS Number.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                Exit Sub
            End If
            If cboAirsNo.ValueMember.Contains(mtbAirsNo.Text) = False Then
                MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                       "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                Exit Sub
            Else
                MsgBox("Valid AIRS #")
                Exit Sub
            End If
            If txtYear.Text = "" Then
                If IsNumeric(txtYear.Text) Then
                Else
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    Exit Sub
                End If
            Else
                If txtYear.Text > feeyear + 1 Then
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    Exit Sub
                End If
            End If

            If txtDepositdate.Text = "" Then
                MsgBox("Please enter a Deposit Date", MsgBoxStyle.OkOnly, "Incorrect Date")
                Exit Sub
            End If

            If txtPayment.Text = "" Then
                MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
                Exit Sub
            End If

            If txtDepositNo.Text = "" Then
                MsgBox("Please enter a Deposit Number", MsgBoxStyle.OkOnly, "Incorrect Deposit No.")
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnUpdateEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEnTry.Click

        Try

            SQL = "Update " & connNameSpace & ".FSAddPaid set strairsnumber = '0413" & mtbAirsNo.Text & "', " _
            + "datpaydate = '" & txtDepositdate.Text & "', " _
            + "numpayment = '" & CDec(txtPayment.Text) & "', " _
            + "strcheckno = '" & txtCheckNo.Text & "', " _
            + "strbatchno = '" & txtbatchNo.Text & "', " _
            + "strpaytype = '" & cboPayType.Text & "', " _
            + "strdepositno = '" & txtDepositNo.Text & "', " _
            + "intyear = '" & CInt(txtYear.Text) & "', " _
            + "strcomments = '" & Replace(txtComments.Text, "'", "''") & "', " _
            + "strentryperson = '" & UserGCode & "' " _
            + "where intpayid = '" & txtPayId.Text & "'"

            cmd = New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record has been updated successfully", MsgBoxStyle.Information, "Update Success!")
            ClearPage()
            LoadDataGrid()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnDeleteEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEnTry.Click

        Try

            SQL = "Delete from " & connNameSpace & ".FSAddPaid " _
            + "where intpayid = '" & txtPayId.Text & "'"

            cmd = New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record has been deleted successfully", MsgBoxStyle.Information, "Delete Success!")
            ClearPage()
            LoadDataGrid()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ClearPage()
        Try

            txtPayId.Text = ""
            txtDepositdate.Text = Format$(Now, "dd-MMM-yyyy")
            txtDepositNo.Text = ""
            txtPayment.Text = ""
            txtCheckNo.Text = ""
            txtbatchNo.Text = ""
            txtComments.Text = ""
            txtYear.Text = ""
            cboPayType.Text = ""
            mtbAirsNo.Text = ""
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Try

            Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
                Case 0
                    ClearPage()
                Case 1
                    Me.Close()
                Case 2
                    ExportToExcel()
                Case Else
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub PASPFeeDeposits_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            FeeDeposits = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

   
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try
            Help.ShowHelp(Label1, "https://sites.google.com/a/dnr.state.ga.us/iaip-docs/")
        Catch ex As Exception
        End Try

    End Sub
End Class
