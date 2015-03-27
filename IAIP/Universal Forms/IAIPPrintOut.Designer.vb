<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPPrintOut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPPrintOut))
        Me.CMBack = New System.Windows.Forms.ContextMenu()
        Me.cmiBackNavScreen = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.cmiBackFacInfo = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.cmiBackSelScreen = New System.Windows.Forms.MenuItem()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MmiBack = New System.Windows.Forms.MenuItem()
        Me.mmiHelp = New System.Windows.Forms.MenuItem()
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.txtOther = New System.Windows.Forms.TextBox()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.txtPrintType = New System.Windows.Forms.TextBox()
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox()
        Me.txtProgram = New System.Windows.Forms.TextBox()
        Me.CRViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.txtSQLLine = New System.Windows.Forms.TextBox()
        Me.txtEndDate = New System.Windows.Forms.TextBox()
        Me.txtStartDate = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CMBack
        '
        Me.CMBack.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmiBackNavScreen, Me.MenuItem8, Me.cmiBackFacInfo, Me.MenuItem7, Me.cmiBackSelScreen})
        '
        'cmiBackNavScreen
        '
        Me.cmiBackNavScreen.Index = 0
        Me.cmiBackNavScreen.Text = "Return To Navigation Screen"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 1
        Me.MenuItem8.Text = "-"
        '
        'cmiBackFacInfo
        '
        Me.cmiBackFacInfo.Index = 2
        Me.cmiBackFacInfo.Text = "Return to Facility Information"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 3
        Me.MenuItem7.Text = "-"
        '
        'cmiBackSelScreen
        '
        Me.cmiBackSelScreen.Index = 4
        Me.cmiBackSelScreen.Text = "Return to Selection Screen"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack})
        Me.MenuItem1.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Back"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 1
        Me.mmiHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiHelp.Text = "&Help"
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
        'txtOther
        '
        Me.txtOther.Location = New System.Drawing.Point(82, 311)
        Me.txtOther.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(22, 20)
        Me.txtOther.TabIndex = 246
        Me.txtOther.Visible = False
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(61, 311)
        Me.txtAIRSNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(23, 20)
        Me.txtAIRSNumber.TabIndex = 245
        Me.txtAIRSNumber.Visible = False
        '
        'txtPrintType
        '
        Me.txtPrintType.Location = New System.Drawing.Point(39, 311)
        Me.txtPrintType.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPrintType.Name = "txtPrintType"
        Me.txtPrintType.Size = New System.Drawing.Size(23, 20)
        Me.txtPrintType.TabIndex = 244
        Me.txtPrintType.Visible = False
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(17, 311)
        Me.txtReferenceNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(23, 20)
        Me.txtReferenceNumber.TabIndex = 243
        Me.txtReferenceNumber.Visible = False
        '
        'txtProgram
        '
        Me.txtProgram.Location = New System.Drawing.Point(3, 311)
        Me.txtProgram.Margin = New System.Windows.Forms.Padding(2)
        Me.txtProgram.Name = "txtProgram"
        Me.txtProgram.Size = New System.Drawing.Size(15, 20)
        Me.txtProgram.TabIndex = 242
        Me.txtProgram.Visible = False
        '
        'CRViewer
        '
        Me.CRViewer.ActiveViewIndex = -1
        Me.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer.CachedPageNumberPerDoc = 10
        Me.CRViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRViewer.Location = New System.Drawing.Point(0, 0)
        Me.CRViewer.Margin = New System.Windows.Forms.Padding(2)
        Me.CRViewer.Name = "CRViewer"
        Me.CRViewer.SelectionFormula = ""
        Me.CRViewer.ShowGroupTreeButton = False
        Me.CRViewer.ShowRefreshButton = False
        Me.CRViewer.Size = New System.Drawing.Size(792, 545)
        Me.CRViewer.TabIndex = 249
        Me.CRViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CRViewer.ViewTimeSelectionFormula = ""
        '
        'txtSQLLine
        '
        Me.txtSQLLine.Location = New System.Drawing.Point(108, 311)
        Me.txtSQLLine.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSQLLine.Name = "txtSQLLine"
        Me.txtSQLLine.Size = New System.Drawing.Size(22, 20)
        Me.txtSQLLine.TabIndex = 250
        Me.txtSQLLine.Visible = False
        '
        'txtEndDate
        '
        Me.txtEndDate.Location = New System.Drawing.Point(160, 311)
        Me.txtEndDate.Margin = New System.Windows.Forms.Padding(2)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(22, 20)
        Me.txtEndDate.TabIndex = 251
        Me.txtEndDate.Visible = False
        '
        'txtStartDate
        '
        Me.txtStartDate.Location = New System.Drawing.Point(134, 311)
        Me.txtStartDate.Margin = New System.Windows.Forms.Padding(2)
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(22, 20)
        Me.txtStartDate.TabIndex = 252
        Me.txtStartDate.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(157, 497)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 253
        Me.Label1.Visible = False
        '
        'IAIPPrintOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 545)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CRViewer)
        Me.Controls.Add(Me.txtStartDate)
        Me.Controls.Add(Me.txtEndDate)
        Me.Controls.Add(Me.txtSQLLine)
        Me.Controls.Add(Me.txtOther)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.txtPrintType)
        Me.Controls.Add(Me.txtReferenceNumber)
        Me.Controls.Add(Me.txtProgram)
        Me.Location = New System.Drawing.Point(50, 0)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPPrintOut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IAIP Print Out"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CMBack As System.Windows.Forms.ContextMenu
    Friend WithEvents cmiBackNavScreen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiBackFacInfo As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiBackSelScreen As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtPrintType As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtProgram As System.Windows.Forms.TextBox
    Friend WithEvents CRViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents txtSQLLine As System.Windows.Forms.TextBox
    Friend WithEvents txtEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
