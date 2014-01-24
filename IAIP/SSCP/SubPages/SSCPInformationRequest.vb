Imports Oracle.DataAccess.Client


Public Class SSCPInformationRequest
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
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiLetterTemplates As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TPContact1 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact2 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact3 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact4 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact5 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact6 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact7 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact8 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact9 As System.Windows.Forms.TabPage
    Friend WithEvents TPContact10 As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents txtContact1Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact1Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact2Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact3Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact4Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact5Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact6Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact7Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact8Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact9Description As System.Windows.Forms.TextBox
    Friend WithEvents txtContact10Description As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TCContactInformation As System.Windows.Forms.TabControl
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtContact4Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact3Email As System.Windows.Forms.TextBox
    Friend WithEvents txtcontact2Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact5Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact6Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact7Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact8Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact9Email As System.Windows.Forms.TextBox
    Friend WithEvents txtContact10Email As System.Windows.Forms.TextBox
    Friend WithEvents PanelInformationRequest As System.Windows.Forms.Panel
    Friend WithEvents mmiAddContact As System.Windows.Forms.MenuItem
    Friend WithEvents DTPDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents btnEmailContact As System.Windows.Forms.Button
    Friend WithEvents btnOpenRequestDocument As System.Windows.Forms.Button
    Friend WithEvents btnOpenBlankTemplate As System.Windows.Forms.Button
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents chbRequestInformationDate As System.Windows.Forms.CheckBox
    Friend WithEvents txtContact1 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact1Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact2 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact2Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact3 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact3Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact4 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact5 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact6 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact4Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact5Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact6Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact7 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact8 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact9 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact10 As System.Windows.Forms.TextBox
    Friend WithEvents txtContact7Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact8Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact9Info As System.Windows.Forms.TextBox
    Friend WithEvents txtContact10Info As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPInformationRequest))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mmiAddContact = New System.Windows.Forms.MenuItem
        Me.mmiLetterTemplates = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TCContactInformation = New System.Windows.Forms.TabControl
        Me.TPContact1 = New System.Windows.Forms.TabPage
        Me.txtContact1Info = New System.Windows.Forms.TextBox
        Me.txtContact1 = New System.Windows.Forms.TextBox
        Me.txtContact1Description = New System.Windows.Forms.TextBox
        Me.txtContact1Email = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TPContact3 = New System.Windows.Forms.TabPage
        Me.txtContact3Info = New System.Windows.Forms.TextBox
        Me.txtContact3 = New System.Windows.Forms.TextBox
        Me.txtContact3Description = New System.Windows.Forms.TextBox
        Me.txtContact3Email = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.TPContact4 = New System.Windows.Forms.TabPage
        Me.txtContact4Info = New System.Windows.Forms.TextBox
        Me.txtContact4 = New System.Windows.Forms.TextBox
        Me.txtContact4Description = New System.Windows.Forms.TextBox
        Me.txtContact4Email = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.TPContact2 = New System.Windows.Forms.TabPage
        Me.txtContact2Info = New System.Windows.Forms.TextBox
        Me.txtContact2 = New System.Windows.Forms.TextBox
        Me.txtContact2Description = New System.Windows.Forms.TextBox
        Me.txtcontact2Email = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.TPContact5 = New System.Windows.Forms.TabPage
        Me.txtContact5Info = New System.Windows.Forms.TextBox
        Me.txtContact5 = New System.Windows.Forms.TextBox
        Me.txtContact5Description = New System.Windows.Forms.TextBox
        Me.txtContact5Email = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.TPContact6 = New System.Windows.Forms.TabPage
        Me.txtContact6Info = New System.Windows.Forms.TextBox
        Me.txtContact6 = New System.Windows.Forms.TextBox
        Me.txtContact6Description = New System.Windows.Forms.TextBox
        Me.txtContact6Email = New System.Windows.Forms.TextBox
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.TPContact7 = New System.Windows.Forms.TabPage
        Me.txtContact7Info = New System.Windows.Forms.TextBox
        Me.txtContact7 = New System.Windows.Forms.TextBox
        Me.txtContact7Description = New System.Windows.Forms.TextBox
        Me.txtContact7Email = New System.Windows.Forms.TextBox
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.TPContact8 = New System.Windows.Forms.TabPage
        Me.txtContact8Info = New System.Windows.Forms.TextBox
        Me.txtContact8 = New System.Windows.Forms.TextBox
        Me.txtContact8Description = New System.Windows.Forms.TextBox
        Me.txtContact8Email = New System.Windows.Forms.TextBox
        Me.Label73 = New System.Windows.Forms.Label
        Me.Label75 = New System.Windows.Forms.Label
        Me.TPContact9 = New System.Windows.Forms.TabPage
        Me.txtContact9Info = New System.Windows.Forms.TextBox
        Me.txtContact9 = New System.Windows.Forms.TextBox
        Me.txtContact9Description = New System.Windows.Forms.TextBox
        Me.txtContact9Email = New System.Windows.Forms.TextBox
        Me.Label83 = New System.Windows.Forms.Label
        Me.Label85 = New System.Windows.Forms.Label
        Me.TPContact10 = New System.Windows.Forms.TabPage
        Me.txtContact10Info = New System.Windows.Forms.TextBox
        Me.txtContact10 = New System.Windows.Forms.TextBox
        Me.txtContact10Description = New System.Windows.Forms.TextBox
        Me.txtContact10Email = New System.Windows.Forms.TextBox
        Me.Label93 = New System.Windows.Forms.Label
        Me.Label95 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.PanelInformationRequest = New System.Windows.Forms.Panel
        Me.chbRequestInformationDate = New System.Windows.Forms.CheckBox
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.btnOpenBlankTemplate = New System.Windows.Forms.Button
        Me.btnOpenRequestDocument = New System.Windows.Forms.Button
        Me.btnEmailContact = New System.Windows.Forms.Button
        Me.Label101 = New System.Windows.Forms.Label
        Me.DTPDueDate = New System.Windows.Forms.DateTimePicker
        Me.GroupBox1.SuspendLayout()
        Me.TCContactInformation.SuspendLayout()
        Me.TPContact1.SuspendLayout()
        Me.TPContact3.SuspendLayout()
        Me.TPContact4.SuspendLayout()
        Me.TPContact2.SuspendLayout()
        Me.TPContact5.SuspendLayout()
        Me.TPContact6.SuspendLayout()
        Me.TPContact7.SuspendLayout()
        Me.TPContact8.SuspendLayout()
        Me.TPContact9.SuspendLayout()
        Me.TPContact10.SuspendLayout()
        Me.PanelInformationRequest.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiBack, Me.MenuItem9, Me.MmiExit})
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
        'MenuItem9
        '
        Me.MenuItem9.Index = 3
        Me.MenuItem9.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 4
        Me.MmiExit.Text = "Exit"
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
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "View"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiAddContact, Me.mmiLetterTemplates})
        Me.MenuItem4.Text = "Tools"
        '
        'mmiAddContact
        '
        Me.mmiAddContact.Index = 0
        Me.mmiAddContact.Text = "Add Contact"
        '
        'mmiLetterTemplates
        '
        Me.mmiLetterTemplates.Index = 1
        Me.mmiLetterTemplates.Text = "Letter Templates"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 4
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TCContactInformation)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(648, 184)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Contact Information"
        '
        'TCContactInformation
        '
        Me.TCContactInformation.Controls.Add(Me.TPContact1)
        Me.TCContactInformation.Controls.Add(Me.TPContact3)
        Me.TCContactInformation.Controls.Add(Me.TPContact4)
        Me.TCContactInformation.Controls.Add(Me.TPContact2)
        Me.TCContactInformation.Controls.Add(Me.TPContact5)
        Me.TCContactInformation.Controls.Add(Me.TPContact6)
        Me.TCContactInformation.Controls.Add(Me.TPContact7)
        Me.TCContactInformation.Controls.Add(Me.TPContact8)
        Me.TCContactInformation.Controls.Add(Me.TPContact9)
        Me.TCContactInformation.Controls.Add(Me.TPContact10)
        Me.TCContactInformation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCContactInformation.Location = New System.Drawing.Point(3, 16)
        Me.TCContactInformation.Name = "TCContactInformation"
        Me.TCContactInformation.SelectedIndex = 0
        Me.TCContactInformation.Size = New System.Drawing.Size(642, 165)
        Me.TCContactInformation.TabIndex = 1
        '
        'TPContact1
        '
        Me.TPContact1.Controls.Add(Me.txtContact1Info)
        Me.TPContact1.Controls.Add(Me.txtContact1)
        Me.TPContact1.Controls.Add(Me.txtContact1Description)
        Me.TPContact1.Controls.Add(Me.txtContact1Email)
        Me.TPContact1.Controls.Add(Me.Label8)
        Me.TPContact1.Controls.Add(Me.Label6)
        Me.TPContact1.Location = New System.Drawing.Point(4, 22)
        Me.TPContact1.Name = "TPContact1"
        Me.TPContact1.Size = New System.Drawing.Size(634, 139)
        Me.TPContact1.TabIndex = 0
        Me.TPContact1.Text = "Contact 1"
        '
        'txtContact1Info
        '
        Me.txtContact1Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact1Info.Multiline = True
        Me.txtContact1Info.Name = "txtContact1Info"
        Me.txtContact1Info.ReadOnly = True
        Me.txtContact1Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact1Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact1Info.TabIndex = 27
        '
        'txtContact1
        '
        Me.txtContact1.Location = New System.Drawing.Point(16, 8)
        Me.txtContact1.Multiline = True
        Me.txtContact1.Name = "txtContact1"
        Me.txtContact1.ReadOnly = True
        Me.txtContact1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact1.Size = New System.Drawing.Size(296, 104)
        Me.txtContact1.TabIndex = 26
        '
        'txtContact1Description
        '
        Me.txtContact1Description.AcceptsReturn = True
        Me.txtContact1Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact1Description.Multiline = True
        Me.txtContact1Description.Name = "txtContact1Description"
        Me.txtContact1Description.ReadOnly = True
        Me.txtContact1Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact1Description.TabIndex = 19
        '
        'txtContact1Email
        '
        Me.txtContact1Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact1Email.Name = "txtContact1Email"
        Me.txtContact1Email.ReadOnly = True
        Me.txtContact1Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact1Email.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(328, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Description:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Email:"
        '
        'TPContact3
        '
        Me.TPContact3.Controls.Add(Me.txtContact3Info)
        Me.TPContact3.Controls.Add(Me.txtContact3)
        Me.TPContact3.Controls.Add(Me.txtContact3Description)
        Me.TPContact3.Controls.Add(Me.txtContact3Email)
        Me.TPContact3.Controls.Add(Me.Label23)
        Me.TPContact3.Controls.Add(Me.Label25)
        Me.TPContact3.Location = New System.Drawing.Point(4, 22)
        Me.TPContact3.Name = "TPContact3"
        Me.TPContact3.Size = New System.Drawing.Size(634, 139)
        Me.TPContact3.TabIndex = 2
        Me.TPContact3.Text = "Contact 3"
        '
        'txtContact3Info
        '
        Me.txtContact3Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact3Info.Multiline = True
        Me.txtContact3Info.Name = "txtContact3Info"
        Me.txtContact3Info.ReadOnly = True
        Me.txtContact3Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact3Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact3Info.TabIndex = 53
        '
        'txtContact3
        '
        Me.txtContact3.Location = New System.Drawing.Point(16, 8)
        Me.txtContact3.Multiline = True
        Me.txtContact3.Name = "txtContact3"
        Me.txtContact3.ReadOnly = True
        Me.txtContact3.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact3.Size = New System.Drawing.Size(296, 104)
        Me.txtContact3.TabIndex = 52
        '
        'txtContact3Description
        '
        Me.txtContact3Description.AcceptsReturn = True
        Me.txtContact3Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact3Description.Multiline = True
        Me.txtContact3Description.Name = "txtContact3Description"
        Me.txtContact3Description.ReadOnly = True
        Me.txtContact3Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact3Description.TabIndex = 44
        '
        'txtContact3Email
        '
        Me.txtContact3Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact3Email.Name = "txtContact3Email"
        Me.txtContact3Email.ReadOnly = True
        Me.txtContact3Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact3Email.TabIndex = 43
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(328, 80)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(63, 13)
        Me.Label23.TabIndex = 32
        Me.Label23.Text = "Description:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(16, 114)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(35, 13)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "Email:"
        '
        'TPContact4
        '
        Me.TPContact4.Controls.Add(Me.txtContact4Info)
        Me.TPContact4.Controls.Add(Me.txtContact4)
        Me.TPContact4.Controls.Add(Me.txtContact4Description)
        Me.TPContact4.Controls.Add(Me.txtContact4Email)
        Me.TPContact4.Controls.Add(Me.Label33)
        Me.TPContact4.Controls.Add(Me.Label35)
        Me.TPContact4.Location = New System.Drawing.Point(4, 22)
        Me.TPContact4.Name = "TPContact4"
        Me.TPContact4.Size = New System.Drawing.Size(634, 139)
        Me.TPContact4.TabIndex = 3
        Me.TPContact4.Text = "Contact 4"
        '
        'txtContact4Info
        '
        Me.txtContact4Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact4Info.Multiline = True
        Me.txtContact4Info.Name = "txtContact4Info"
        Me.txtContact4Info.ReadOnly = True
        Me.txtContact4Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact4Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact4Info.TabIndex = 54
        '
        'txtContact4
        '
        Me.txtContact4.Location = New System.Drawing.Point(16, 8)
        Me.txtContact4.Multiline = True
        Me.txtContact4.Name = "txtContact4"
        Me.txtContact4.ReadOnly = True
        Me.txtContact4.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact4.Size = New System.Drawing.Size(296, 104)
        Me.txtContact4.TabIndex = 53
        '
        'txtContact4Description
        '
        Me.txtContact4Description.AcceptsReturn = True
        Me.txtContact4Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact4Description.Multiline = True
        Me.txtContact4Description.Name = "txtContact4Description"
        Me.txtContact4Description.ReadOnly = True
        Me.txtContact4Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact4Description.TabIndex = 44
        '
        'txtContact4Email
        '
        Me.txtContact4Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact4Email.Name = "txtContact4Email"
        Me.txtContact4Email.ReadOnly = True
        Me.txtContact4Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact4Email.TabIndex = 43
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(328, 80)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(63, 13)
        Me.Label33.TabIndex = 32
        Me.Label33.Text = "Description:"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(16, 114)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(35, 13)
        Me.Label35.TabIndex = 30
        Me.Label35.Text = "Email:"
        '
        'TPContact2
        '
        Me.TPContact2.Controls.Add(Me.txtContact2Info)
        Me.TPContact2.Controls.Add(Me.txtContact2)
        Me.TPContact2.Controls.Add(Me.txtContact2Description)
        Me.TPContact2.Controls.Add(Me.txtcontact2Email)
        Me.TPContact2.Controls.Add(Me.Label13)
        Me.TPContact2.Controls.Add(Me.Label15)
        Me.TPContact2.Location = New System.Drawing.Point(4, 22)
        Me.TPContact2.Name = "TPContact2"
        Me.TPContact2.Size = New System.Drawing.Size(634, 139)
        Me.TPContact2.TabIndex = 1
        Me.TPContact2.Text = "Contact 2"
        '
        'txtContact2Info
        '
        Me.txtContact2Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact2Info.Multiline = True
        Me.txtContact2Info.Name = "txtContact2Info"
        Me.txtContact2Info.ReadOnly = True
        Me.txtContact2Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact2Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact2Info.TabIndex = 52
        '
        'txtContact2
        '
        Me.txtContact2.Location = New System.Drawing.Point(16, 8)
        Me.txtContact2.Multiline = True
        Me.txtContact2.Name = "txtContact2"
        Me.txtContact2.ReadOnly = True
        Me.txtContact2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact2.Size = New System.Drawing.Size(296, 104)
        Me.txtContact2.TabIndex = 51
        '
        'txtContact2Description
        '
        Me.txtContact2Description.AcceptsReturn = True
        Me.txtContact2Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact2Description.Multiline = True
        Me.txtContact2Description.Name = "txtContact2Description"
        Me.txtContact2Description.ReadOnly = True
        Me.txtContact2Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact2Description.TabIndex = 44
        '
        'txtcontact2Email
        '
        Me.txtcontact2Email.Location = New System.Drawing.Point(56, 112)
        Me.txtcontact2Email.Name = "txtcontact2Email"
        Me.txtcontact2Email.ReadOnly = True
        Me.txtcontact2Email.Size = New System.Drawing.Size(256, 20)
        Me.txtcontact2Email.TabIndex = 43
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(328, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(63, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Description:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(16, 114)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(35, 13)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Email:"
        '
        'TPContact5
        '
        Me.TPContact5.Controls.Add(Me.txtContact5Info)
        Me.TPContact5.Controls.Add(Me.txtContact5)
        Me.TPContact5.Controls.Add(Me.txtContact5Description)
        Me.TPContact5.Controls.Add(Me.txtContact5Email)
        Me.TPContact5.Controls.Add(Me.Label43)
        Me.TPContact5.Controls.Add(Me.Label45)
        Me.TPContact5.Location = New System.Drawing.Point(4, 22)
        Me.TPContact5.Name = "TPContact5"
        Me.TPContact5.Size = New System.Drawing.Size(634, 139)
        Me.TPContact5.TabIndex = 4
        Me.TPContact5.Text = "Contact 5"
        '
        'txtContact5Info
        '
        Me.txtContact5Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact5Info.Multiline = True
        Me.txtContact5Info.Name = "txtContact5Info"
        Me.txtContact5Info.ReadOnly = True
        Me.txtContact5Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact5Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact5Info.TabIndex = 54
        '
        'txtContact5
        '
        Me.txtContact5.Location = New System.Drawing.Point(16, 8)
        Me.txtContact5.Multiline = True
        Me.txtContact5.Name = "txtContact5"
        Me.txtContact5.ReadOnly = True
        Me.txtContact5.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact5.Size = New System.Drawing.Size(296, 104)
        Me.txtContact5.TabIndex = 53
        '
        'txtContact5Description
        '
        Me.txtContact5Description.AcceptsReturn = True
        Me.txtContact5Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact5Description.Multiline = True
        Me.txtContact5Description.Name = "txtContact5Description"
        Me.txtContact5Description.ReadOnly = True
        Me.txtContact5Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact5Description.TabIndex = 44
        '
        'txtContact5Email
        '
        Me.txtContact5Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact5Email.Name = "txtContact5Email"
        Me.txtContact5Email.ReadOnly = True
        Me.txtContact5Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact5Email.TabIndex = 43
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(328, 80)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(63, 13)
        Me.Label43.TabIndex = 32
        Me.Label43.Text = "Description:"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(16, 114)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(35, 13)
        Me.Label45.TabIndex = 30
        Me.Label45.Text = "Email:"
        '
        'TPContact6
        '
        Me.TPContact6.Controls.Add(Me.txtContact6Info)
        Me.TPContact6.Controls.Add(Me.txtContact6)
        Me.TPContact6.Controls.Add(Me.txtContact6Description)
        Me.TPContact6.Controls.Add(Me.txtContact6Email)
        Me.TPContact6.Controls.Add(Me.Label53)
        Me.TPContact6.Controls.Add(Me.Label55)
        Me.TPContact6.Location = New System.Drawing.Point(4, 22)
        Me.TPContact6.Name = "TPContact6"
        Me.TPContact6.Size = New System.Drawing.Size(634, 139)
        Me.TPContact6.TabIndex = 5
        Me.TPContact6.Text = "Contact 6"
        '
        'txtContact6Info
        '
        Me.txtContact6Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact6Info.Multiline = True
        Me.txtContact6Info.Name = "txtContact6Info"
        Me.txtContact6Info.ReadOnly = True
        Me.txtContact6Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact6Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact6Info.TabIndex = 54
        '
        'txtContact6
        '
        Me.txtContact6.Location = New System.Drawing.Point(16, 8)
        Me.txtContact6.Multiline = True
        Me.txtContact6.Name = "txtContact6"
        Me.txtContact6.ReadOnly = True
        Me.txtContact6.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact6.Size = New System.Drawing.Size(296, 104)
        Me.txtContact6.TabIndex = 53
        '
        'txtContact6Description
        '
        Me.txtContact6Description.AcceptsReturn = True
        Me.txtContact6Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact6Description.Multiline = True
        Me.txtContact6Description.Name = "txtContact6Description"
        Me.txtContact6Description.ReadOnly = True
        Me.txtContact6Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact6Description.TabIndex = 44
        '
        'txtContact6Email
        '
        Me.txtContact6Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact6Email.Name = "txtContact6Email"
        Me.txtContact6Email.ReadOnly = True
        Me.txtContact6Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact6Email.TabIndex = 43
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(328, 80)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(63, 13)
        Me.Label53.TabIndex = 32
        Me.Label53.Text = "Description:"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(16, 114)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(35, 13)
        Me.Label55.TabIndex = 30
        Me.Label55.Text = "Email:"
        '
        'TPContact7
        '
        Me.TPContact7.Controls.Add(Me.txtContact7Info)
        Me.TPContact7.Controls.Add(Me.txtContact7)
        Me.TPContact7.Controls.Add(Me.txtContact7Description)
        Me.TPContact7.Controls.Add(Me.txtContact7Email)
        Me.TPContact7.Controls.Add(Me.Label63)
        Me.TPContact7.Controls.Add(Me.Label65)
        Me.TPContact7.Location = New System.Drawing.Point(4, 22)
        Me.TPContact7.Name = "TPContact7"
        Me.TPContact7.Size = New System.Drawing.Size(634, 139)
        Me.TPContact7.TabIndex = 6
        Me.TPContact7.Text = "Contact 7"
        '
        'txtContact7Info
        '
        Me.txtContact7Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact7Info.Multiline = True
        Me.txtContact7Info.Name = "txtContact7Info"
        Me.txtContact7Info.ReadOnly = True
        Me.txtContact7Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact7Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact7Info.TabIndex = 54
        '
        'txtContact7
        '
        Me.txtContact7.Location = New System.Drawing.Point(16, 8)
        Me.txtContact7.Multiline = True
        Me.txtContact7.Name = "txtContact7"
        Me.txtContact7.ReadOnly = True
        Me.txtContact7.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact7.Size = New System.Drawing.Size(296, 104)
        Me.txtContact7.TabIndex = 53
        '
        'txtContact7Description
        '
        Me.txtContact7Description.AcceptsReturn = True
        Me.txtContact7Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact7Description.Multiline = True
        Me.txtContact7Description.Name = "txtContact7Description"
        Me.txtContact7Description.ReadOnly = True
        Me.txtContact7Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact7Description.TabIndex = 44
        '
        'txtContact7Email
        '
        Me.txtContact7Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact7Email.Name = "txtContact7Email"
        Me.txtContact7Email.ReadOnly = True
        Me.txtContact7Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact7Email.TabIndex = 43
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(328, 80)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(63, 13)
        Me.Label63.TabIndex = 32
        Me.Label63.Text = "Description:"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(16, 114)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(35, 13)
        Me.Label65.TabIndex = 30
        Me.Label65.Text = "Email:"
        '
        'TPContact8
        '
        Me.TPContact8.Controls.Add(Me.txtContact8Info)
        Me.TPContact8.Controls.Add(Me.txtContact8)
        Me.TPContact8.Controls.Add(Me.txtContact8Description)
        Me.TPContact8.Controls.Add(Me.txtContact8Email)
        Me.TPContact8.Controls.Add(Me.Label73)
        Me.TPContact8.Controls.Add(Me.Label75)
        Me.TPContact8.Location = New System.Drawing.Point(4, 22)
        Me.TPContact8.Name = "TPContact8"
        Me.TPContact8.Size = New System.Drawing.Size(634, 139)
        Me.TPContact8.TabIndex = 7
        Me.TPContact8.Text = "Contact 8"
        '
        'txtContact8Info
        '
        Me.txtContact8Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact8Info.Multiline = True
        Me.txtContact8Info.Name = "txtContact8Info"
        Me.txtContact8Info.ReadOnly = True
        Me.txtContact8Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact8Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact8Info.TabIndex = 54
        '
        'txtContact8
        '
        Me.txtContact8.Location = New System.Drawing.Point(16, 8)
        Me.txtContact8.Multiline = True
        Me.txtContact8.Name = "txtContact8"
        Me.txtContact8.ReadOnly = True
        Me.txtContact8.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact8.Size = New System.Drawing.Size(296, 104)
        Me.txtContact8.TabIndex = 53
        '
        'txtContact8Description
        '
        Me.txtContact8Description.AcceptsReturn = True
        Me.txtContact8Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact8Description.Multiline = True
        Me.txtContact8Description.Name = "txtContact8Description"
        Me.txtContact8Description.ReadOnly = True
        Me.txtContact8Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact8Description.TabIndex = 44
        '
        'txtContact8Email
        '
        Me.txtContact8Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact8Email.Name = "txtContact8Email"
        Me.txtContact8Email.ReadOnly = True
        Me.txtContact8Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact8Email.TabIndex = 43
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(328, 80)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(63, 13)
        Me.Label73.TabIndex = 32
        Me.Label73.Text = "Description:"
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(16, 114)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(35, 13)
        Me.Label75.TabIndex = 30
        Me.Label75.Text = "Email:"
        '
        'TPContact9
        '
        Me.TPContact9.Controls.Add(Me.txtContact9Info)
        Me.TPContact9.Controls.Add(Me.txtContact9)
        Me.TPContact9.Controls.Add(Me.txtContact9Description)
        Me.TPContact9.Controls.Add(Me.txtContact9Email)
        Me.TPContact9.Controls.Add(Me.Label83)
        Me.TPContact9.Controls.Add(Me.Label85)
        Me.TPContact9.Location = New System.Drawing.Point(4, 22)
        Me.TPContact9.Name = "TPContact9"
        Me.TPContact9.Size = New System.Drawing.Size(634, 139)
        Me.TPContact9.TabIndex = 8
        Me.TPContact9.Text = "Contact 9"
        '
        'txtContact9Info
        '
        Me.txtContact9Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact9Info.Multiline = True
        Me.txtContact9Info.Name = "txtContact9Info"
        Me.txtContact9Info.ReadOnly = True
        Me.txtContact9Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact9Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact9Info.TabIndex = 54
        '
        'txtContact9
        '
        Me.txtContact9.Location = New System.Drawing.Point(16, 8)
        Me.txtContact9.Multiline = True
        Me.txtContact9.Name = "txtContact9"
        Me.txtContact9.ReadOnly = True
        Me.txtContact9.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact9.Size = New System.Drawing.Size(296, 104)
        Me.txtContact9.TabIndex = 53
        '
        'txtContact9Description
        '
        Me.txtContact9Description.AcceptsReturn = True
        Me.txtContact9Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact9Description.Multiline = True
        Me.txtContact9Description.Name = "txtContact9Description"
        Me.txtContact9Description.ReadOnly = True
        Me.txtContact9Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact9Description.TabIndex = 44
        '
        'txtContact9Email
        '
        Me.txtContact9Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact9Email.Name = "txtContact9Email"
        Me.txtContact9Email.ReadOnly = True
        Me.txtContact9Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact9Email.TabIndex = 43
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Location = New System.Drawing.Point(328, 80)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(63, 13)
        Me.Label83.TabIndex = 32
        Me.Label83.Text = "Description:"
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Location = New System.Drawing.Point(16, 114)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(35, 13)
        Me.Label85.TabIndex = 30
        Me.Label85.Text = "Email:"
        '
        'TPContact10
        '
        Me.TPContact10.Controls.Add(Me.txtContact10Info)
        Me.TPContact10.Controls.Add(Me.txtContact10)
        Me.TPContact10.Controls.Add(Me.txtContact10Description)
        Me.TPContact10.Controls.Add(Me.txtContact10Email)
        Me.TPContact10.Controls.Add(Me.Label93)
        Me.TPContact10.Controls.Add(Me.Label95)
        Me.TPContact10.Location = New System.Drawing.Point(4, 22)
        Me.TPContact10.Name = "TPContact10"
        Me.TPContact10.Size = New System.Drawing.Size(634, 139)
        Me.TPContact10.TabIndex = 9
        Me.TPContact10.Text = "Contact 10"
        '
        'txtContact10Info
        '
        Me.txtContact10Info.Location = New System.Drawing.Point(328, 8)
        Me.txtContact10Info.Multiline = True
        Me.txtContact10Info.Name = "txtContact10Info"
        Me.txtContact10Info.ReadOnly = True
        Me.txtContact10Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact10Info.Size = New System.Drawing.Size(296, 64)
        Me.txtContact10Info.TabIndex = 54
        '
        'txtContact10
        '
        Me.txtContact10.Location = New System.Drawing.Point(16, 8)
        Me.txtContact10.Multiline = True
        Me.txtContact10.Name = "txtContact10"
        Me.txtContact10.ReadOnly = True
        Me.txtContact10.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtContact10.Size = New System.Drawing.Size(296, 104)
        Me.txtContact10.TabIndex = 53
        '
        'txtContact10Description
        '
        Me.txtContact10Description.AcceptsReturn = True
        Me.txtContact10Description.Location = New System.Drawing.Point(400, 80)
        Me.txtContact10Description.Multiline = True
        Me.txtContact10Description.Name = "txtContact10Description"
        Me.txtContact10Description.ReadOnly = True
        Me.txtContact10Description.Size = New System.Drawing.Size(224, 56)
        Me.txtContact10Description.TabIndex = 44
        '
        'txtContact10Email
        '
        Me.txtContact10Email.Location = New System.Drawing.Point(56, 112)
        Me.txtContact10Email.Name = "txtContact10Email"
        Me.txtContact10Email.ReadOnly = True
        Me.txtContact10Email.Size = New System.Drawing.Size(256, 20)
        Me.txtContact10Email.TabIndex = 43
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(328, 80)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(63, 13)
        Me.Label93.TabIndex = 32
        Me.Label93.Text = "Description:"
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(16, 114)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(35, 13)
        Me.Label95.TabIndex = 30
        Me.Label95.Text = "Email:"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 184)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(648, 5)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(8, 280)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtAIRSNumber.TabIndex = 2
        Me.txtAIRSNumber.Visible = False
        '
        'PanelInformationRequest
        '
        Me.PanelInformationRequest.Controls.Add(Me.chbRequestInformationDate)
        Me.PanelInformationRequest.Controls.Add(Me.txtTrackingNumber)
        Me.PanelInformationRequest.Controls.Add(Me.btnOpenBlankTemplate)
        Me.PanelInformationRequest.Controls.Add(Me.btnOpenRequestDocument)
        Me.PanelInformationRequest.Controls.Add(Me.btnEmailContact)
        Me.PanelInformationRequest.Controls.Add(Me.Label101)
        Me.PanelInformationRequest.Controls.Add(Me.DTPDueDate)
        Me.PanelInformationRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelInformationRequest.Location = New System.Drawing.Point(0, 189)
        Me.PanelInformationRequest.Name = "PanelInformationRequest"
        Me.PanelInformationRequest.Size = New System.Drawing.Size(648, 116)
        Me.PanelInformationRequest.TabIndex = 6
        '
        'chbRequestInformationDate
        '
        Me.chbRequestInformationDate.Checked = True
        Me.chbRequestInformationDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbRequestInformationDate.Location = New System.Drawing.Point(222, 9)
        Me.chbRequestInformationDate.Name = "chbRequestInformationDate"
        Me.chbRequestInformationDate.Size = New System.Drawing.Size(48, 16)
        Me.chbRequestInformationDate.TabIndex = 120
        Me.chbRequestInformationDate.Text = "N/A"
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(208, 8)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(8, 20)
        Me.txtTrackingNumber.TabIndex = 8
        Me.txtTrackingNumber.Visible = False
        '
        'btnOpenBlankTemplate
        '
        Me.btnOpenBlankTemplate.Location = New System.Drawing.Point(424, 40)
        Me.btnOpenBlankTemplate.Name = "btnOpenBlankTemplate"
        Me.btnOpenBlankTemplate.Size = New System.Drawing.Size(192, 23)
        Me.btnOpenBlankTemplate.TabIndex = 7
        Me.btnOpenBlankTemplate.Text = "Open Blank Document Template"
        Me.btnOpenBlankTemplate.Visible = False
        '
        'btnOpenRequestDocument
        '
        Me.btnOpenRequestDocument.Location = New System.Drawing.Point(200, 40)
        Me.btnOpenRequestDocument.Name = "btnOpenRequestDocument"
        Me.btnOpenRequestDocument.Size = New System.Drawing.Size(176, 23)
        Me.btnOpenRequestDocument.TabIndex = 6
        Me.btnOpenRequestDocument.Text = "Open Request Document"
        Me.btnOpenRequestDocument.Visible = False
        '
        'btnEmailContact
        '
        Me.btnEmailContact.Location = New System.Drawing.Point(48, 40)
        Me.btnEmailContact.Name = "btnEmailContact"
        Me.btnEmailContact.Size = New System.Drawing.Size(104, 23)
        Me.btnEmailContact.TabIndex = 5
        Me.btnEmailContact.Text = "Email Contact"
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(8, 10)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(99, 13)
        Me.Label101.TabIndex = 4
        Me.Label101.Text = "Request Due Date:"
        '
        'DTPDueDate
        '
        Me.DTPDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDueDate.Location = New System.Drawing.Point(112, 8)
        Me.DTPDueDate.Name = "DTPDueDate"
        Me.DTPDueDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPDueDate.TabIndex = 3
        '
        'SSCPInformationRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(648, 305)
        Me.Controls.Add(Me.PanelInformationRequest)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPInformationRequest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Compliance Information Request"
        Me.GroupBox1.ResumeLayout(False)
        Me.TCContactInformation.ResumeLayout(False)
        Me.TPContact1.ResumeLayout(False)
        Me.TPContact1.PerformLayout()
        Me.TPContact3.ResumeLayout(False)
        Me.TPContact3.PerformLayout()
        Me.TPContact4.ResumeLayout(False)
        Me.TPContact4.PerformLayout()
        Me.TPContact2.ResumeLayout(False)
        Me.TPContact2.PerformLayout()
        Me.TPContact5.ResumeLayout(False)
        Me.TPContact5.PerformLayout()
        Me.TPContact6.ResumeLayout(False)
        Me.TPContact6.PerformLayout()
        Me.TPContact7.ResumeLayout(False)
        Me.TPContact7.PerformLayout()
        Me.TPContact8.ResumeLayout(False)
        Me.TPContact8.PerformLayout()
        Me.TPContact9.ResumeLayout(False)
        Me.TPContact9.PerformLayout()
        Me.TPContact10.ResumeLayout(False)
        Me.TPContact10.PerformLayout()
        Me.PanelInformationRequest.ResumeLayout(False)
        Me.PanelInformationRequest.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub SSCPInformationRequest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            LoadFacilityContactInformation()

            DTPDueDate.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadFacilityContactInformation()
        Dim ContactName As String = ""
        Dim ContactTitle As String = ""
        Dim ContactCompany As String = ""
        Dim ContactAreaCode1 As String = ""
        Dim ContactPhone1 As String = ""
        Dim ContactAreaCode2 As String = ""
        Dim ContactPhone2 As String = ""
        Dim ContactAreaCode3 As String = ""
        Dim ContactFax As String = ""
        Dim ContactEmail As String = ""
        Dim ContactStreet As String = ""
        Dim ContactStreet2 As String = ""
        Dim ContactAddress As String = ""
        Dim ContactCity As String = ""
        Dim ContactState As String = ""
        Dim ContactZipCode As String = ""
        Dim ContactDescription As String = ""

        Try


            RemoveContactTabPages()

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            SQL = "Select strContactKey, " & _
            "strContactFirstName, strContactLastName, " & _
            "strContactPrefix, strContactSuffix, " & _
            "strContactTitle, strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
            "order by strContactKey "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        ContactName = ""
                    Else
                        If dr.Item("strContactPrefix") <> "N/A" Then
                            ContactName = dr.Item("strContactPrefix") & " "
                        Else
                            ContactName = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        ContactName = ContactName & ""
                    Else
                        If dr.Item("strContactFirstName") <> "N/A" Then
                            ContactName = ContactName & dr.Item("strContactFirstName") & " "
                        Else
                            ContactName = ContactName & ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        ContactName = ContactName & ""
                    Else
                        If dr.Item("strContactLastName") <> "N/A" Then
                            ContactName = ContactName & dr.Item("strContactLastName") & " "
                        Else
                            ContactName = ContactName & ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        ContactName = ContactName & ""
                    Else
                        If dr.Item("strContactSuffix") <> "N/A" Then
                            ContactName = ContactName & dr.Item("strContactSuffix")
                        Else
                            ContactName = ContactName & ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strcontactTitle")) Then
                        ContactTitle = ""
                    Else
                        If dr.Item("strcontactTitle") <> "N/A" Then
                            ContactTitle = dr.Item("strContactTitle")
                        Else
                            ContactTitle = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        ContactCompany = ""
                    Else
                        If dr.Item("strContactCompanyName") <> "N/A" Then
                            ContactCompany = dr.Item("strContactCompanyName")
                        Else
                            ContactCompany = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                        ContactAreaCode1 = ""
                        ContactPhone1 = ""
                    Else
                        If dr.Item("strContactPhoneNumber1") <> "N/A" Then
                            If dr.Item("strContactPhoneNumber1").ToString.Length >= 3 Then
                                ContactAreaCode1 = Mid(dr.Item("strContactPhoneNumber1"), 1, 3)
                            End If
                            If dr.Item("strContactPhoneNumber1").ToString.Length >= 10 Then
                                ContactPhone1 = Mid(dr.Item("strContactPhoneNumber1"), 4, 3) & "-" & Mid(dr.Item("strContactPhoneNumber1"), 7, 4)
                            End If
                            If dr.Item("strContactPhoneNumber1").ToString.Length > 11 Then
                                If Mid(dr.Item("strContactPhoneNumber1"), 11) <> "" Then
                                    ContactPhone1 = ContactPhone1 & "-" & Mid(dr.Item("strContactPhoneNumber1"), 11)
                                End If
                            End If
                        Else
                            ContactAreaCode1 = ""
                            ContactPhone1 = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactphoneNumber2")) Then
                        ContactAreaCode2 = ""
                        ContactPhone2 = ""
                    Else
                        If IsDBNull(dr("strContactphoneNumber2")) Then
                        Else
                            If dr.Item("strContactPhoneNumber2") <> "N/A" Then
                                If dr.Item("strContactPhoneNumber2").ToString.Length >= 3 Then
                                    ContactAreaCode2 = Mid(dr.Item("strContactPhoneNumber2"), 1, 3)
                                End If
                                If dr.Item("strContactPhoneNumber2").ToString.Length >= 10 Then
                                    ContactPhone2 = Mid(dr.Item("strContactPhoneNumber2"), 4, 3) & "-" & Mid(dr.Item("strContactPhoneNumber2"), 7, 4)
                                End If
                                If dr.Item("strContactPhoneNumber2").ToString.Length > 11 Then
                                    If Mid(dr.Item("strContactPhoneNumber2"), 11) <> "" Then
                                        ContactPhone2 = ContactPhone2 & "-" & Mid(dr.Item("strContactPhoneNumber2"), 11)
                                    End If
                                End If
                            Else
                                ContactAreaCode2 = ""
                                ContactPhone2 = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactFaxNumber")) Then
                        ContactAreaCode3 = ""
                        ContactFax = ""
                    Else
                        If dr.Item("strContactFaxNumber") <> "N/A" Then
                            If dr.Item("strContactFaxNumber").ToString.Length >= 3 Then
                                ContactAreaCode3 = Mid(dr.Item("strContactFaxNumber"), 1, 3)
                            End If
                            If dr.Item("strContactFaxNumber").ToString.Length >= 10 Then
                                ContactFax = Mid(dr.Item("strContactFaxNumber"), 4, 3) & "-" & Mid(dr.Item("strContactFaxNumber"), 7, 4)
                            End If
                            If dr.Item("strContactFaxNumber").ToString.Length > 11 Then
                                If Mid(dr.Item("strContactFaxNumber"), 11) <> "" Then
                                    ContactFax = ContactFax & "-" & Mid(dr.Item("strContactFaxNumber"), 11)
                                End If
                            End If
                        Else
                            ContactAreaCode3 = ""
                            ContactFax = ""
                        End If
                    End If
                    If IsDBNull(dr("strContactEmail")) Then
                    Else
                        If dr.Item("strContactEmail") <> "N/A" Then
                            ContactEmail = dr.Item("strContactEmail")
                        Else
                            ContactEmail = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        ContactStreet = ""
                    Else
                        If dr.Item("strContactAddress1") <> "N/A" Then
                            ContactStreet = dr.Item("strContactAddress1")
                        Else
                            ContactStreet = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactAddress2")) Then
                        ContactStreet2 = ""
                    Else
                        If dr.Item("strContactAddress2") <> "N/A" Then
                            ContactStreet2 = dr.Item("strContactAddress2")
                        Else
                            ContactStreet2 = ""
                        End If
                    End If
                    If ContactStreet2 = "" Then
                        ContactAddress = ContactStreet
                    Else
                        ContactAddress = ContactStreet & vbCrLf & ContactStreet2
                    End If

                    If IsDBNull(dr.Item("strContactCity")) Then
                        ContactCity = ""
                    Else
                        If dr.Item("strContactCity") <> "N/A" Then
                            ContactCity = dr.Item("strContactCity")
                        Else
                            ContactCity = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        ContactState = ""
                    Else
                        If dr.Item("strContactState") <> " " Then
                            ContactState = dr.Item("strContactState")
                        Else
                            ContactState = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        ContactZipCode = ""
                    Else
                        If dr.Item("strContactZipCode") <> "N/A" Then
                            If dr.Item("strContactZipCode").ToString.Length > 5 Then
                                ContactZipCode = Mid(dr.Item("strContactZipCode"), 1, 5)
                            End If
                            If dr.Item("strContactZipCode").ToString.Length > 5 Then
                                If Mid(dr.Item("strContactZipCode"), 6) <> "" Then
                                    ContactZipCode = ContactZipCode & "-" & Mid(dr.Item("strContactZipCode"), 6)
                                End If
                            End If
                        Else
                            ContactZipCode = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strContactDescription")) Then
                        ContactDescription = "N/A"
                    Else
                        If IsDBNull(dr.Item("strContactDescription")) Then
                            ContactDescription = "N/A"
                        Else
                            ContactDescription = dr.Item("strContactDescription")
                        End If
                    End If
                    Select Case Mid(dr.Item("strContactKey"), 13, 1)
                        Case 2
                            Select Case Mid(dr.Item("strContactKey"), 14, 1)
                                Case 0
                                    txtContact1.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact1Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact1Email.Text = ContactEmail
                                    txtContact1Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact1)
                                Case 1
                                    txtContact2.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact2Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtcontact2Email.Text = ContactEmail
                                    txtContact2Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact2)
                                Case 2
                                    txtContact3.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact3Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact3Email.Text = ContactEmail
                                    txtContact3Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact3)
                                Case 3
                                    txtContact4.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact4Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact4Email.Text = ContactEmail
                                    txtContact4Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact4)
                                Case 4
                                    txtContact5.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact5Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact5Email.Text = ContactEmail
                                    txtContact5Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact5)
                                Case 5
                                    txtContact6.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact6Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact6Email.Text = ContactEmail
                                    txtContact6Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact6)
                                Case 6
                                    txtContact7.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact7Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact7Email.Text = ContactEmail
                                    txtContact7Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact7)
                                Case 7
                                    txtContact8.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact8Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact8Email.Text = ContactEmail
                                    txtContact8Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact8)
                                Case 8
                                    txtContact9.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact9Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact9Email.Text = ContactEmail
                                    txtContact9Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact9)
                                Case 9
                                    txtContact10.Text = ContactName & vbCrLf & _
                                    ContactTitle & vbCrLf & _
                                    ContactCompany & vbCrLf & _
                                    ContactAddress & vbCrLf & _
                                    ContactCity & ", " & ContactState & " " & ContactZipCode

                                    txtContact10Info.Text = "Phone Number 1: " & ContactAreaCode1 & "-" & ContactPhone1 & vbCrLf & _
                                    "Phone Number 2: " & ContactAreaCode2 & "-" & ContactPhone2 & vbCrLf & _
                                    "FaxNumber : " & ContactAreaCode3 & "-" & ContactFax & vbCrLf
                                    txtContact10Email.Text = ContactEmail
                                    txtContact10Description.Text = ContactDescription
                                    TCContactInformation.TabPages.Add(TPContact10)
                            End Select
                    End Select
                Loop
            End If
        Catch ex As Exception
            ErrorReport(txtAIRSNumber.Text.ToString() & ex.ToString(), "SSCPInformationRequest.LoadFacilityContactInformation")
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub RemoveContactTabPages()
        Try

            TCContactInformation.TabPages.Remove(TPContact1)
            TCContactInformation.TabPages.Remove(TPContact2)
            TCContactInformation.TabPages.Remove(TPContact3)
            TCContactInformation.TabPages.Remove(TPContact4)
            TCContactInformation.TabPages.Remove(TPContact5)
            TCContactInformation.TabPages.Remove(TPContact6)
            TCContactInformation.TabPages.Remove(TPContact7)
            TCContactInformation.TabPages.Remove(TPContact8)
            TCContactInformation.TabPages.Remove(TPContact9)
            TCContactInformation.TabPages.Remove(TPContact10)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

#End Region

#Region "Subs and Functions"
    Sub EmailContact()
        Dim EmailLink As String = ""

        Try

            If TCContactInformation.Contains(TPContact1) Then
                If TCContactInformation.TabPages.Item(0).Focus Then
                    EmailLink = "mailto:" & txtContact1Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact2) Then
                If TCContactInformation.TabPages.Item(1).Focus Then
                    EmailLink = "mailto:" & txtcontact2Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact3) Then
                If TCContactInformation.TabPages.Item(2).Focus Then
                    EmailLink = "mailto:" & txtContact3Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact4) Then
                If TCContactInformation.TabPages.Item(3).Focus Then
                    EmailLink = "mailto:" & txtContact4Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact5) Then
                If TCContactInformation.TabPages.Item(4).Focus Then
                    EmailLink = "mailto:" & txtContact5Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact6) Then
                If TCContactInformation.TabPages.Item(5).Focus Then
                    EmailLink = "mailto:" & txtContact6Email.Text

                End If
            End If
            If TCContactInformation.Contains(TPContact7) Then
                If TCContactInformation.TabPages.Item(6).Focus Then
                    EmailLink = "mailto:" & txtContact7Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact8) Then
                If TCContactInformation.TabPages.Item(7).Focus Then
                    EmailLink = "mailto:" & txtContact8Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact9) Then
                If TCContactInformation.TabPages.Item(8).Focus Then
                    EmailLink = "mailto:" & txtContact9Email.Text
                End If
            End If
            If TCContactInformation.Contains(TPContact10) Then
                If TCContactInformation.TabPages.Item(9).Focus Then
                    EmailLink = "mailto:" & txtContact10Email.Text
                End If
            End If

            If EmailLink <> "" Then
                '   EmailLink = EmailLink
                System.Diagnostics.Process.Start(EmailLink)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub SaveRequestDate()
        Try

            If txtTrackingNumber.Text <> "" Then
                SQL = "Select strTrackingNumber " & _
                "from " & DBNameSpace & ".SSCPItemMaster " & _
                "where strtrackingNumber = '" & txtTrackingNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPItemMaster set " & _
                    "datInformationRequestDate = '" & DTPDueDate.Text & "' " & _
                    "where strTrackingnumber = '" & txtTrackingNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                End If
            End If

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
            If SSCPReports Is Nothing Then
            Else
                SSCPReports.txtRequestInformationDate.Text = DTPDueDate.Text
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
    Private Sub mmiAddContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddContact.Click
        Try

            If EditContacts Is Nothing Then
                If EditContacts Is Nothing Then EditContacts = New IAIPEditContacts
                EditContacts.AirsNumber = txtAIRSNumber.Text
                EditContacts.lblFacilityName.Text = ""
                EditContacts.Show()
            Else
                EditContacts.Dispose()
                EditContacts = New IAIPEditContacts
                If EditContacts Is Nothing Then EditContacts = New IAIPEditContacts
                EditContacts.AirsNumber = txtAIRSNumber.Text
                EditContacts.lblFacilityName.Text = ""
                EditContacts.Show()
            End If
            'EditContacts.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub


    Private Sub btnEmailContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailContact.Click
        Try

            If chbRequestInformationDate.Checked = False Then
                SaveRequestDate()
            End If

            EmailContact()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try
            Help.ShowHelp(Label101, HelpUrl)
        Catch ex As Exception
        End Try

    End Sub
End Class
