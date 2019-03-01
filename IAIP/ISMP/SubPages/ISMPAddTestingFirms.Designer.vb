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
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.bSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.bClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgrTestingFirms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.GroupBox1.Location = New System.Drawing.Point(0, 24)
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
        Me.dgrTestingFirms.Location = New System.Drawing.Point(0, 174)
        Me.dgrTestingFirms.Name = "dgrTestingFirms"
        Me.dgrTestingFirms.ReadOnly = True
        Me.dgrTestingFirms.Size = New System.Drawing.Size(680, 171)
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bSave, Me.bClear})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(680, 24)
        Me.MenuStrip1.TabIndex = 148
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'bSave
        '
        Me.bSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.bSave.Name = "bSave"
        Me.bSave.Size = New System.Drawing.Size(59, 20)
        Me.bSave.Text = "Save"
        '
        'bClear
        '
        Me.bClear.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(62, 20)
        Me.bClear.Text = "Clear"
        '
        'ISMPAddTestingFirms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(680, 345)
        Me.Controls.Add(Me.dgrTestingFirms)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPAddTestingFirms"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Add Testing Firms"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgrTestingFirms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MmiSave As MenuItem
    Friend WithEvents MenuItem1 As MenuItem
    Friend WithEvents mmiClear As MenuItem
    Friend WithEvents MenuItem2 As MenuItem
    Friend WithEvents MainMenu1 As MainMenu
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents bSave As ToolStripMenuItem
    Friend WithEvents bClear As ToolStripMenuItem
End Class
