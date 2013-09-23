Imports System.Data.OracleClient


Public Class ISMPAddTestingFirms
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
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents dgrTestingFirms As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTestingFirm As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTestingFirmAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmCity As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmState As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmZipCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTestingFirmAreaCode1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAreaCode2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAreaCode3 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmPhoneNumber1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmPhoneNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmFaxNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmEmail As System.Windows.Forms.TextBox
    Friend WithEvents chbDeleteTestingFirm As System.Windows.Forms.CheckBox
    Friend WithEvents txtTestingFirmKey As System.Windows.Forms.TextBox
    Friend WithEvents TBAddTestingFirm As System.Windows.Forms.ToolBar
    Friend WithEvents Label8 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPAddTestingFirms))
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
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.TBAddTestingFirm = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtTestingFirmKey = New System.Windows.Forms.TextBox
        Me.chbDeleteTestingFirm = New System.Windows.Forms.CheckBox
        Me.txtTestingFirmEmail = New System.Windows.Forms.TextBox
        Me.txtTestingFirmFaxNumber = New System.Windows.Forms.TextBox
        Me.txtTestingFirmPhoneNumber2 = New System.Windows.Forms.TextBox
        Me.txtTestingFirmPhoneNumber1 = New System.Windows.Forms.TextBox
        Me.txtTestingFirmAreaCode3 = New System.Windows.Forms.TextBox
        Me.txtTestingFirmAreaCode2 = New System.Windows.Forms.TextBox
        Me.txtTestingFirmAreaCode1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTestingFirmZipCode = New System.Windows.Forms.TextBox
        Me.txtTestingFirmState = New System.Windows.Forms.TextBox
        Me.txtTestingFirmCity = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTestingFirmAddress2 = New System.Windows.Forms.TextBox
        Me.txtTestingFirmAddress1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTestingFirm = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.dgrTestingFirms = New System.Windows.Forms.DataGrid
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgrTestingFirms, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
        Me.mmiHelp.Text = "Help"
        '
        'TBAddTestingFirm
        '
        Me.TBAddTestingFirm.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbBack})
        Me.TBAddTestingFirm.DropDownArrows = True
        Me.TBAddTestingFirm.ImageList = Me.Image_List_All
        Me.TBAddTestingFirm.Location = New System.Drawing.Point(0, 0)
        Me.TBAddTestingFirm.Name = "TBAddTestingFirm"
        Me.TBAddTestingFirm.ShowToolTips = True
        Me.TBAddTestingFirm.Size = New System.Drawing.Size(680, 28)
        Me.TBAddTestingFirm.TabIndex = 141
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
        Me.tbbSave.ToolTipText = "Save"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmKey)
        Me.GroupBox1.Controls.Add(Me.chbDeleteTestingFirm)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmEmail)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmFaxNumber)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmPhoneNumber2)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmPhoneNumber1)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmAreaCode3)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmAreaCode2)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmAreaCode1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmZipCode)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmState)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmCity)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmAddress2)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmAddress1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirm)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(680, 156)
        Me.GroupBox1.TabIndex = 145
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Testing Firm Information"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Testing Firm Key:"
        '
        'txtTestingFirmKey
        '
        Me.txtTestingFirmKey.Location = New System.Drawing.Point(128, 24)
        Me.txtTestingFirmKey.Name = "txtTestingFirmKey"
        Me.txtTestingFirmKey.ReadOnly = True
        Me.txtTestingFirmKey.Size = New System.Drawing.Size(56, 20)
        Me.txtTestingFirmKey.TabIndex = 21
        '
        'chbDeleteTestingFirm
        '
        Me.chbDeleteTestingFirm.Location = New System.Drawing.Point(16, 134)
        Me.chbDeleteTestingFirm.Name = "chbDeleteTestingFirm"
        Me.chbDeleteTestingFirm.Size = New System.Drawing.Size(128, 16)
        Me.chbDeleteTestingFirm.TabIndex = 20
        Me.chbDeleteTestingFirm.Text = "Delete Testing Firm"
        '
        'txtTestingFirmEmail
        '
        Me.txtTestingFirmEmail.Location = New System.Drawing.Point(448, 108)
        Me.txtTestingFirmEmail.MaxLength = 100
        Me.txtTestingFirmEmail.Name = "txtTestingFirmEmail"
        Me.txtTestingFirmEmail.Size = New System.Drawing.Size(216, 20)
        Me.txtTestingFirmEmail.TabIndex = 19
        '
        'txtTestingFirmFaxNumber
        '
        Me.txtTestingFirmFaxNumber.Location = New System.Drawing.Point(488, 86)
        Me.txtTestingFirmFaxNumber.Name = "txtTestingFirmFaxNumber"
        Me.txtTestingFirmFaxNumber.Size = New System.Drawing.Size(96, 20)
        Me.txtTestingFirmFaxNumber.TabIndex = 18
        '
        'txtTestingFirmPhoneNumber2
        '
        Me.txtTestingFirmPhoneNumber2.Location = New System.Drawing.Point(488, 66)
        Me.txtTestingFirmPhoneNumber2.Name = "txtTestingFirmPhoneNumber2"
        Me.txtTestingFirmPhoneNumber2.Size = New System.Drawing.Size(96, 20)
        Me.txtTestingFirmPhoneNumber2.TabIndex = 17
        '
        'txtTestingFirmPhoneNumber1
        '
        Me.txtTestingFirmPhoneNumber1.Location = New System.Drawing.Point(488, 44)
        Me.txtTestingFirmPhoneNumber1.Name = "txtTestingFirmPhoneNumber1"
        Me.txtTestingFirmPhoneNumber1.Size = New System.Drawing.Size(96, 20)
        Me.txtTestingFirmPhoneNumber1.TabIndex = 16
        '
        'txtTestingFirmAreaCode3
        '
        Me.txtTestingFirmAreaCode3.Location = New System.Drawing.Point(448, 86)
        Me.txtTestingFirmAreaCode3.MaxLength = 3
        Me.txtTestingFirmAreaCode3.Name = "txtTestingFirmAreaCode3"
        Me.txtTestingFirmAreaCode3.Size = New System.Drawing.Size(40, 20)
        Me.txtTestingFirmAreaCode3.TabIndex = 15
        '
        'txtTestingFirmAreaCode2
        '
        Me.txtTestingFirmAreaCode2.Location = New System.Drawing.Point(448, 66)
        Me.txtTestingFirmAreaCode2.MaxLength = 3
        Me.txtTestingFirmAreaCode2.Name = "txtTestingFirmAreaCode2"
        Me.txtTestingFirmAreaCode2.Size = New System.Drawing.Size(40, 20)
        Me.txtTestingFirmAreaCode2.TabIndex = 14
        '
        'txtTestingFirmAreaCode1
        '
        Me.txtTestingFirmAreaCode1.Location = New System.Drawing.Point(448, 44)
        Me.txtTestingFirmAreaCode1.MaxLength = 3
        Me.txtTestingFirmAreaCode1.Name = "txtTestingFirmAreaCode1"
        Me.txtTestingFirmAreaCode1.Size = New System.Drawing.Size(40, 20)
        Me.txtTestingFirmAreaCode1.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(352, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Email Address:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(352, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Fax Number:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(352, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Phone Number 2:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(352, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Phone Number 1:"
        '
        'txtTestingFirmZipCode
        '
        Me.txtTestingFirmZipCode.Location = New System.Drawing.Point(256, 108)
        Me.txtTestingFirmZipCode.MaxLength = 10
        Me.txtTestingFirmZipCode.Name = "txtTestingFirmZipCode"
        Me.txtTestingFirmZipCode.Size = New System.Drawing.Size(80, 20)
        Me.txtTestingFirmZipCode.TabIndex = 8
        '
        'txtTestingFirmState
        '
        Me.txtTestingFirmState.Location = New System.Drawing.Point(224, 108)
        Me.txtTestingFirmState.MaxLength = 2
        Me.txtTestingFirmState.Name = "txtTestingFirmState"
        Me.txtTestingFirmState.Size = New System.Drawing.Size(32, 20)
        Me.txtTestingFirmState.TabIndex = 7
        '
        'txtTestingFirmCity
        '
        Me.txtTestingFirmCity.Location = New System.Drawing.Point(128, 108)
        Me.txtTestingFirmCity.MaxLength = 50
        Me.txtTestingFirmCity.Name = "txtTestingFirmCity"
        Me.txtTestingFirmCity.Size = New System.Drawing.Size(96, 20)
        Me.txtTestingFirmCity.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "City/State/Zip Code:"
        '
        'txtTestingFirmAddress2
        '
        Me.txtTestingFirmAddress2.Location = New System.Drawing.Point(128, 86)
        Me.txtTestingFirmAddress2.MaxLength = 100
        Me.txtTestingFirmAddress2.Name = "txtTestingFirmAddress2"
        Me.txtTestingFirmAddress2.Size = New System.Drawing.Size(208, 20)
        Me.txtTestingFirmAddress2.TabIndex = 4
        '
        'txtTestingFirmAddress1
        '
        Me.txtTestingFirmAddress1.Location = New System.Drawing.Point(128, 66)
        Me.txtTestingFirmAddress1.MaxLength = 100
        Me.txtTestingFirmAddress1.Name = "txtTestingFirmAddress1"
        Me.txtTestingFirmAddress1.Size = New System.Drawing.Size(208, 20)
        Me.txtTestingFirmAddress1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Address:"
        '
        'txtTestingFirm
        '
        Me.txtTestingFirm.Location = New System.Drawing.Point(128, 44)
        Me.txtTestingFirm.MaxLength = 100
        Me.txtTestingFirm.Name = "txtTestingFirm"
        Me.txtTestingFirm.Size = New System.Drawing.Size(208, 20)
        Me.txtTestingFirm.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Testing Firm Name:"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 184)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(680, 5)
        Me.Splitter1.TabIndex = 146
        Me.Splitter1.TabStop = False
        '
        'dgrTestingFirms
        '
        Me.dgrTestingFirms.DataMember = ""
        Me.dgrTestingFirms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrTestingFirms.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrTestingFirms.Location = New System.Drawing.Point(0, 189)
        Me.dgrTestingFirms.Name = "dgrTestingFirms"
        Me.dgrTestingFirms.ReadOnly = True
        Me.dgrTestingFirms.Size = New System.Drawing.Size(680, 156)
        Me.dgrTestingFirms.TabIndex = 147
        '
        'ISMPAddTestingFirms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(680, 345)
        Me.Controls.Add(Me.dgrTestingFirms)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TBAddTestingFirm)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPAddTestingFirms"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Add Testing Firms"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgrTestingFirms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub ISMPAddTestingFirms_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            LoadDataSet()
            FormatdgrTestingFirms()
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
    Sub LoadDataSet()
        Try

            SQL = "Select strTestingFirmKey, strTestingFirm, " & _
                 "strFirmAddress1, strFirmAddress2, " & _
                 "strFirmCity, strFirmState, " & _
                 "strFirmZipCode, strFirmPhoneNumber1, " & _
                 "strFirmPhoneNumber2, strFirmFax, strFirmEmail " & _
                 "from " & DBNameSpace & ".LookUPTestingFirms " & _
                 "Order by strTestingFirm "

            dsTestingFirms = New DataSet

            daTestingFirms = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daTestingFirms.Fill(dsTestingFirms, "TestingFirms")
            dgrTestingFirms.DataSource = dsTestingFirms
            dgrTestingFirms.DataMember = "TestingFirms"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub FormatdgrTestingFirms()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "TestingFirms"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTestingFirmKey"
            objtextcol.HeaderText = "Key"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 50
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTestingFirm"
            objtextcol.HeaderText = "Testing Firm"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 250
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmAddress1"
            objtextcol.HeaderText = "Firm Address"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmCity"
            objtextcol.HeaderText = "Firm City"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmState"
            objtextcol.HeaderText = "Firm State"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 70
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmZipCode"
            objtextcol.HeaderText = "Firm Zip Code"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmPhoneNumber1"
            objtextcol.HeaderText = "Firm Phone Number 1"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmPhoneNumber2"
            objtextcol.HeaderText = "Firm Phone Number 2"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmFax"
            objtextcol.HeaderText = "Firm Fax Number"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmEmail"
            objtextcol.HeaderText = "Firm Email Address"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrTestingFirms.TableStyles.Clear()
            dgrTestingFirms.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrTestingFirms.CaptionText = "Testing Firm(s)"
            dgrTestingFirms.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region


#Region "Functions and Subs"
    Sub LoadTestingFirmInfo()
        Dim dtTestingFirms As New DataTable
        Try

            dtTestingFirms = dsTestingFirms.Tables("TestingFirms")

            Dim drTestingFirms As DataRow()
            Dim row As DataRow

            drTestingFirms = dtTestingFirms.Select("strTestingFirmKey = '" & txtTestingFirmKey.Text & "'")
            For Each row In drTestingFirms
                txtTestingFirm.Text = row("strTestingFirm")
                If row("strFirmAddress1") <> "N/A" Then
                    txtTestingFirmAddress1.Text = row("strFirmAddress1")
                Else
                    txtTestingFirmAddress1.Text = ""
                End If
                If row("strFirmAddress2") <> "N/A" Then
                    txtTestingFirmAddress2.Text = row("strFirmAddress2")
                Else
                    txtTestingFirmAddress2.Text = ""
                End If
                If row("strFirmCity") <> "N/A" Then
                    txtTestingFirmCity.Text = row("strFirmCity")
                Else
                    txtTestingFirmCity.Text = ""
                End If
                txtTestingFirmState.Text = row("strFirmState")
                If row("strFirmZipCode") <> "N/A" Then
                    txtTestingFirmZipCode.Text = row("strFirmZipCode")
                Else
                    txtTestingFirmZipCode.Text = ""
                End If
                If row("strFirmPhoneNumber1") <> "N/A" Then
                    txtTestingFirmAreaCode1.Text = Mid(row("strFirmPhoneNumber1"), 1, 3)
                    txtTestingFirmPhoneNumber1.Text = Mid(row("strFirmPhoneNumber1"), 4)
                Else
                    txtTestingFirmAreaCode1.Text = ""
                    txtTestingFirmPhoneNumber1.Text = ""
                End If
                If row("strFirmPhoneNumber2") <> "N/A" Then
                    txtTestingFirmAreaCode2.Text = Mid(row("strFirmPhoneNumber2"), 1, 3)
                    txtTestingFirmPhoneNumber2.Text = Mid(row("strFirmPhoneNumber2"), 4)
                Else
                    txtTestingFirmAreaCode2.Text = ""
                    txtTestingFirmPhoneNumber2.Text = ""
                End If
                If row("strFirmFax") <> "N/A" Then
                    txtTestingFirmAreaCode3.Text = Mid(row("strFirmFax"), 1, 3)
                    txtTestingFirmFaxNumber.Text = Mid(row("strFirmFax"), 4)
                Else
                    txtTestingFirmAreaCode3.Text = ""
                    txtTestingFirmFaxNumber.Text = ""
                End If
                If row("StrFirmEmail") <> "N/A" Then
                    txtTestingFirmEmail.Text = row("strFirmEmail")
                Else
                    txtTestingFirmEmail.Text = ""
                End If
            Next

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub GetNextKey()
        Dim TestingFirmKey As String
        Dim newTestingFirmKey As String
        Try

            TestingFirmKey = "00001"
            newTestingFirmKey = "00000"

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Do Until newTestingFirmKey <> "00000"
                Select Case TestingFirmKey.Length
                    Case 1
                        TestingFirmKey = "0000" & TestingFirmKey
                    Case 2
                        TestingFirmKey = "000" & TestingFirmKey
                    Case 3
                        TestingFirmKey = "00" & TestingFirmKey
                    Case 4
                        TestingFirmKey = "0" & TestingFirmKey
                    Case Else
                        ' TestingFirmKey = TestingFirmKey
                End Select

                SQL = "Select strTestingfirmKey " & _
                "from " & DBNameSpace & ".LookUPTestingFirms " & _
                "where strTestingFirmKey = '" & TestingFirmKey & "' "
                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    TestingFirmKey += 1
                Else
                    newTestingFirmKey = TestingFirmKey
                End If
            Loop

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            Select Case TestingFirmKey.Length
                Case 1
                    TestingFirmKey = "0000" & TestingFirmKey
                Case 2
                    TestingFirmKey = "000" & TestingFirmKey
                Case 3
                    TestingFirmKey = "00" & TestingFirmKey
                Case 4
                    TestingFirmKey = "0" & TestingFirmKey
                Case Else
                    'TestingFirmKey = TestingFirmKey
            End Select
            txtTestingFirmKey.Text = TestingFirmKey

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Save()
        Dim TestingFirm As String = ""
        Dim TestingFirmAddress1 As String = ""
        Dim TestingFirmAddress2 As String = ""
        Dim TestingFirmCity As String = ""
        Dim TestingFirmState As String = ""
        Dim TestingFirmZipCode As String = ""
        Dim TestingFirmPhoneNumber1 As String = ""
        Dim TestingFirmPhoneNumber2 As String = ""
        Dim TestingFirmFaxNumber As String = ""
        Dim TestingFirmEmail As String = ""
        Dim x As Integer

        Try

            If txtTestingFirm.Text <> "" Then
                TestingFirm = Replace(txtTestingFirm.Text, "'", "''")
            Else
                TestingFirm = "N/A"
            End If
            If txtTestingFirmAddress1.Text <> "" Then
                TestingFirmAddress1 = Replace(txtTestingFirmAddress1.Text, "'", "''")
            Else
                TestingFirmAddress1 = "N/A"
            End If
            If txtTestingFirmAddress2.Text <> "" Then
                TestingFirmAddress2 = Replace(txtTestingFirmAddress2.Text, "'", "''")
            Else
                TestingFirmAddress2 = "N/A"
            End If
            If txtTestingFirmCity.Text <> "" Then
                TestingFirmCity = Replace(txtTestingFirmCity.Text, "'", "''")
            Else
                TestingFirmCity = "N/A"
            End If
            If txtTestingFirmState.Text <> "" Then
                TestingFirmState = Replace(txtTestingFirmState.Text, "'", "''")
            Else
                TestingFirmState = "GA"
            End If
            If txtTestingFirmZipCode.Text <> "" Then
                For x = 1 To txtTestingFirmZipCode.Text.Length
                    If IsNumeric(Mid(txtTestingFirmZipCode.Text, x, 1)) Then TestingFirmZipCode = TestingFirmZipCode & Mid(txtTestingFirmZipCode.Text, x, 1)
                Next
                If TestingFirmZipCode = "" Then
                    TestingFirmZipCode = "00000"
                End If
            Else
                TestingFirmZipCode = "00000"
            End If
            If txtTestingFirmPhoneNumber1.Text <> "" And txtTestingFirmAreaCode1.Text <> "" Then
                For x = 1 To txtTestingFirmAreaCode1.Text.Length
                    If IsNumeric(Mid(txtTestingFirmAreaCode1.Text, x, 1)) Then TestingFirmPhoneNumber1 = TestingFirmPhoneNumber1 & Mid(txtTestingFirmAreaCode1.Text, x, 1)
                Next
                For x = 1 To txtTestingFirmPhoneNumber1.Text.Length
                    If IsNumeric(Mid(txtTestingFirmPhoneNumber1.Text, x, 1)) Then TestingFirmPhoneNumber1 = TestingFirmPhoneNumber1 & Mid(txtTestingFirmPhoneNumber1.Text, x, 1)
                Next
                If TestingFirmPhoneNumber1 = "" Then
                    TestingFirmPhoneNumber1 = "N/A"
                End If
            Else
                TestingFirmPhoneNumber1 = "N/A"
            End If
            If txtTestingFirmPhoneNumber2.Text <> "" And txtTestingFirmAreaCode2.Text <> "" Then
                For x = 1 To txtTestingFirmAreaCode2.Text.Length
                    If IsNumeric(Mid(txtTestingFirmAreaCode2.Text, x, 1)) Then TestingFirmPhoneNumber2 = TestingFirmPhoneNumber2 & Mid(txtTestingFirmAreaCode2.Text, x, 1)
                Next
                For x = 1 To txtTestingFirmPhoneNumber2.Text.Length
                    If IsNumeric(Mid(txtTestingFirmPhoneNumber2.Text, x, 1)) Then TestingFirmPhoneNumber2 = TestingFirmPhoneNumber2 & Mid(txtTestingFirmPhoneNumber2.Text, x, 1)
                Next
                If TestingFirmPhoneNumber2 = "" Then
                    TestingFirmPhoneNumber2 = "N/A"
                End If
            Else
                TestingFirmPhoneNumber2 = "N/A"
            End If
            If txtTestingFirmFaxNumber.Text <> "" And txtTestingFirmAreaCode3.Text <> "" Then
                For x = 1 To txtTestingFirmAreaCode3.Text.Length
                    If IsNumeric(Mid(txtTestingFirmAreaCode3.Text, x, 1)) Then TestingFirmFaxNumber = TestingFirmFaxNumber & Mid(txtTestingFirmAreaCode3.Text, x, 1)
                Next
                For x = 1 To txtTestingFirmFaxNumber.Text.Length
                    If IsNumeric(Mid(txtTestingFirmFaxNumber.Text, x, 1)) Then TestingFirmFaxNumber = TestingFirmFaxNumber & Mid(txtTestingFirmFaxNumber.Text, x, 1)
                Next
                If TestingFirmFaxNumber = "" Then
                    TestingFirmFaxNumber = "N/A"
                End If
            Else
                TestingFirmFaxNumber = "N/A"
            End If
            If txtTestingFirmEmail.Text <> "" Then
                TestingFirmEmail = Replace(txtTestingFirmEmail.Text, "'", "''")
            Else
                TestingFirmEmail = "N/A"
            End If

            If txtTestingFirm.Text <> "" Then
                If txtTestingFirmKey.Text = "" Then
                    GetNextKey()
                End If

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                If chbDeleteTestingFirm.Checked = True Then
                    SQL = "Delete " & DBNameSpace & ".LookUPTestingFirms " & _
                    "where strTestingFirmKey = '" & txtTestingFirmKey.Text & "'"
                Else
                    SQL = "Select strTestingFirmKey " & _
                    "from " & DBNameSpace & ".LookUPTestingFirms " & _
                    "where strTestingFirmKey = '" & txtTestingFirmKey.Text & "'"
                    cmd = New OracleCommand(SQL, Conn)
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        SQL = "Update " & DBNameSpace & ".LookUPTestingFirms set " & _
                        "strTestingFirm = '" & TestingFirm & "', " & _
                        "strFirmAddress1 = '" & TestingFirmAddress1 & "', " & _
                        "strFirmAddress2 = '" & TestingFirmAddress2 & "', " & _
                        "strFirmCity = '" & TestingFirmCity & "', " & _
                        "strFirmState = '" & TestingFirmState & "', " & _
                        "strFirmZipCode = '" & TestingFirmZipCode & "', " & _
                        "strFirmphoneNumber1 = '" & TestingFirmPhoneNumber1 & "', " & _
                        "strFirmPhoneNumber2 = '" & TestingFirmPhoneNumber2 & "', " & _
                        "strFirmFax = '" & TestingFirmFaxNumber & "', " & _
                        "strFirmEmail = '" & TestingFirmEmail & "' " & _
                        "where strTestingFirmKey = '" & txtTestingFirmKey.Text & "' "
                    Else
                        SQL = "Insert into " & DBNameSpace & ".LookUPTestingFirms " & _
                        "(strTestingFirmKey, strTestingFirm, " & _
                        "strFirmAddress1, strFirmAddress2, " & _
                        "strFirmCity, strFirmState, " & _
                        "strFirmZipCode, strFirmPhoneNumber1, " & _
                        "strFirmPhoneNumber2, strFirmFax, " & _
                        "strFirmEmail) " & _
                        "values " & _
                        "('" & txtTestingFirmKey.Text & "', '" & TestingFirm & "', " & _
                        "'" & TestingFirmAddress1 & "', '" & TestingFirmAddress2 & "', " & _
                        "'" & TestingFirmCity & "', '" & TestingFirmState & "', " & _
                        "'" & TestingFirmZipCode & "', '" & TestingFirmPhoneNumber1 & "', " & _
                        "'" & TestingFirmPhoneNumber2 & "', '" & TestingFirmFaxNumber & "', " & _
                        "'" & TestingFirmEmail & "') "
                    End If
                End If
                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader

                LoadDataSet()

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
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
    Sub Clear()
        Try

            txtTestingFirmKey.Clear()
            txtTestingFirm.Clear()
            txtTestingFirmAddress1.Clear()
            txtTestingFirmAddress2.Clear()
            txtTestingFirmCity.Clear()
            txtTestingFirmState.Clear()
            txtTestingFirmZipCode.Clear()
            txtTestingFirmAreaCode1.Clear()
            txtTestingFirmAreaCode2.Clear()
            txtTestingFirmAreaCode3.Clear()
            txtTestingFirmPhoneNumber1.Clear()
            txtTestingFirmPhoneNumber2.Clear()
            txtTestingFirmFaxNumber.Clear()
            txtTestingFirmEmail.Clear()
            chbDeleteTestingFirm.Checked = False
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

            ISMPAddTestingFirm = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
    Private Sub ISMPAddTestingFirms_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPAddTestingFirm = Nothing
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrTestingFirms_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrTestingFirms.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrTestingFirms.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrTestingFirms(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrTestingFirms(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrTestingFirms(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrTestingFirms(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrTestingFirms(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrTestingFirms(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrTestingFirms(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrTestingFirms(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrTestingFirms(hti.Row, 8)) Then
                                                Else
                                                    If IsDBNull(dgrTestingFirms(hti.Row, 9)) Then
                                                    Else
                                                        txtTestingFirmKey.Text = dgrTestingFirms(hti.Row, 0)
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
    Private Sub txtTestingFirmKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestingFirmKey.TextChanged
        Try

            If txtTestingFirmKey.Text <> "" Then
                LoadTestingFirmInfo()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Main Menu Item"
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
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
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
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
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
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
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
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            Clear()
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

    Private Sub TBAddTestingFirm_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBAddTestingFirm.ButtonClick
        Try

            Select Case TBAddTestingFirm.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
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

   
End Class
