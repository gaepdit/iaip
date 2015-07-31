<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPAddPollutants
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

    Friend WithEvents TBAddPollutant As System.Windows.Forms.ToolBar
    Friend WithEvents chbDeletePollutant As System.Windows.Forms.CheckBox
    Friend WithEvents dgrPollutants As System.Windows.Forms.DataGrid
    Friend WithEvents llbGetNextValue As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPollutantCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPollutant As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPAddPollutants))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MmiSave = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MmiBack = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mmiCut = New System.Windows.Forms.MenuItem()
        Me.mmiCopy = New System.Windows.Forms.MenuItem()
        Me.mmiPaste = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.mmiClear = New System.Windows.Forms.MenuItem()
        Me.mmiHelp = New System.Windows.Forms.MenuItem()
        Me.TBAddPollutant = New System.Windows.Forms.ToolBar()
        Me.tbbSave = New System.Windows.Forms.ToolBarButton()
        Me.tbbClear = New System.Windows.Forms.ToolBarButton()
        Me.tbbBack = New System.Windows.Forms.ToolBarButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chbDeletePollutant = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPollutantCode = New System.Windows.Forms.TextBox()
        Me.txtPollutant = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.llbGetNextValue = New System.Windows.Forms.LinkLabel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.dgrPollutants = New System.Windows.Forms.DataGrid()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgrPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'TBAddPollutant
        '
        Me.TBAddPollutant.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbBack})
        Me.TBAddPollutant.DropDownArrows = True
        Me.TBAddPollutant.ImageList = Me.Image_List_All
        Me.TBAddPollutant.Location = New System.Drawing.Point(0, 0)
        Me.TBAddPollutant.Name = "TBAddPollutant"
        Me.TBAddPollutant.ShowToolTips = True
        Me.TBAddPollutant.Size = New System.Drawing.Size(492, 28)
        Me.TBAddPollutant.TabIndex = 141
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chbDeletePollutant)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPollutantCode)
        Me.GroupBox1.Controls.Add(Me.txtPollutant)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.llbGetNextValue)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(492, 76)
        Me.GroupBox1.TabIndex = 142
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pollutant Information"
        '
        'chbDeletePollutant
        '
        Me.chbDeletePollutant.Location = New System.Drawing.Point(72, 48)
        Me.chbDeletePollutant.Name = "chbDeletePollutant"
        Me.chbDeletePollutant.Size = New System.Drawing.Size(104, 16)
        Me.chbDeletePollutant.TabIndex = 145
        Me.chbDeletePollutant.Text = "Delete Pollutant"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(248, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Pollutant Code:"
        '
        'txtPollutantCode
        '
        Me.txtPollutantCode.Location = New System.Drawing.Point(336, 24)
        Me.txtPollutantCode.Name = "txtPollutantCode"
        Me.txtPollutantCode.Size = New System.Drawing.Size(100, 20)
        Me.txtPollutantCode.TabIndex = 2
        '
        'txtPollutant
        '
        Me.txtPollutant.Location = New System.Drawing.Point(72, 24)
        Me.txtPollutant.Name = "txtPollutant"
        Me.txtPollutant.Size = New System.Drawing.Size(168, 20)
        Me.txtPollutant.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pollutant:"
        '
        'llbGetNextValue
        '
        Me.llbGetNextValue.AutoSize = True
        Me.llbGetNextValue.Location = New System.Drawing.Point(336, 48)
        Me.llbGetNextValue.Name = "llbGetNextValue"
        Me.llbGetNextValue.Size = New System.Drawing.Size(77, 13)
        Me.llbGetNextValue.TabIndex = 144
        Me.llbGetNextValue.TabStop = True
        Me.llbGetNextValue.Text = "Get Next Code"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 104)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(492, 5)
        Me.Splitter1.TabIndex = 143
        Me.Splitter1.TabStop = False
        '
        'dgrPollutants
        '
        Me.dgrPollutants.DataMember = ""
        Me.dgrPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrPollutants.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrPollutants.Location = New System.Drawing.Point(0, 109)
        Me.dgrPollutants.Name = "dgrPollutants"
        Me.dgrPollutants.ReadOnly = True
        Me.dgrPollutants.Size = New System.Drawing.Size(492, 236)
        Me.dgrPollutants.TabIndex = 144
        '
        'ISMPAddPollutants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(492, 345)
        Me.Controls.Add(Me.dgrPollutants)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TBAddPollutant)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPAddPollutants"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Add Pollutants"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgrPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
