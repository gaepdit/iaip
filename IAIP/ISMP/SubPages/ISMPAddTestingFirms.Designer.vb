<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPAddTestingFirms
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TBAddTestingFirm As System.Windows.Forms.ToolBar
    Friend WithEvents txtTestingFirmKey As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmFaxNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmPhoneNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmPhoneNumber1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAreaCode3 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAreaCode2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAreaCode1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTestingFirmZipCode As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmState As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmCity As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTestingFirmAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTestingFirm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgrTestingFirms As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPAddTestingFirms))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBAddTestingFirm = New System.Windows.Forms.ToolBar()
        Me.tbbSave = New System.Windows.Forms.ToolBarButton()
        Me.tbbClear = New System.Windows.Forms.ToolBarButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTestingFirmKey = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmEmail = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmFaxNumber = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmPhoneNumber2 = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmPhoneNumber1 = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmAreaCode3 = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmAreaCode2 = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmAreaCode1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTestingFirmZipCode = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmState = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmCity = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTestingFirmAddress2 = New System.Windows.Forms.TextBox()
        Me.txtTestingFirmAddress1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTestingFirm = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgrTestingFirms = New System.Windows.Forms.DataGrid()
        Me.MmiSave = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mmiClear = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
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
        'TBAddTestingFirm
        '
        Me.TBAddTestingFirm.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear})
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTestingFirmKey)
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
        Me.GroupBox1.Size = New System.Drawing.Size(680, 150)
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
        'dgrTestingFirms
        '
        Me.dgrTestingFirms.DataMember = ""
        Me.dgrTestingFirms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrTestingFirms.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrTestingFirms.Location = New System.Drawing.Point(0, 178)
        Me.dgrTestingFirms.Name = "dgrTestingFirms"
        Me.dgrTestingFirms.ReadOnly = True
        Me.dgrTestingFirms.Size = New System.Drawing.Size(680, 167)
        Me.dgrTestingFirms.TabIndex = 147
        '
        'MmiSave
        '
        Me.MmiSave.Index = 0
        Me.MmiSave.Text = "Save"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave})
        Me.MenuItem1.Text = "File"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 0
        Me.mmiClear.Text = "Clear"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiClear})
        Me.MenuItem2.Text = "Edit"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2})
        '
        'ISMPAddTestingFirms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(680, 345)
        Me.Controls.Add(Me.dgrTestingFirms)
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

    Friend WithEvents MmiSave As MenuItem
    Friend WithEvents MenuItem1 As MenuItem
    Friend WithEvents mmiClear As MenuItem
    Friend WithEvents MenuItem2 As MenuItem
    Friend WithEvents MainMenu1 As MainMenu
End Class
