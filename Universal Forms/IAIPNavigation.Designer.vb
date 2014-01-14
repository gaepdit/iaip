<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPNavigation
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
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiFile = New System.Windows.Forms.MenuItem
        Me.mmiExit = New System.Windows.Forms.MenuItem
        Me.mmiTools = New System.Windows.Forms.MenuItem
        Me.mmiExport = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem
        Me.mmiResetForm = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiAbout = New System.Windows.Forms.MenuItem
        Me.mmiTesting = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnNav40 = New System.Windows.Forms.Button
        Me.btnNav39 = New System.Windows.Forms.Button
        Me.btnNav38 = New System.Windows.Forms.Button
        Me.btnNav37 = New System.Windows.Forms.Button
        Me.btnNav36 = New System.Windows.Forms.Button
        Me.btnNav35 = New System.Windows.Forms.Button
        Me.btnNav34 = New System.Windows.Forms.Button
        Me.btnNav33 = New System.Windows.Forms.Button
        Me.btnNav32 = New System.Windows.Forms.Button
        Me.btnNav31 = New System.Windows.Forms.Button
        Me.btnNav30 = New System.Windows.Forms.Button
        Me.btnNav29 = New System.Windows.Forms.Button
        Me.btnNav21 = New System.Windows.Forms.Button
        Me.btnNav20 = New System.Windows.Forms.Button
        Me.btnNav19 = New System.Windows.Forms.Button
        Me.btnNav18 = New System.Windows.Forms.Button
        Me.btnNav17 = New System.Windows.Forms.Button
        Me.btnNav16 = New System.Windows.Forms.Button
        Me.btnNav15 = New System.Windows.Forms.Button
        Me.btnNav28 = New System.Windows.Forms.Button
        Me.btnNav27 = New System.Windows.Forms.Button
        Me.btnNav26 = New System.Windows.Forms.Button
        Me.btnNav25 = New System.Windows.Forms.Button
        Me.btnNav24 = New System.Windows.Forms.Button
        Me.btnNav23 = New System.Windows.Forms.Button
        Me.btnNav22 = New System.Windows.Forms.Button
        Me.btnNav14 = New System.Windows.Forms.Button
        Me.btnNav13 = New System.Windows.Forms.Button
        Me.btnNav12 = New System.Windows.Forms.Button
        Me.btnNav11 = New System.Windows.Forms.Button
        Me.btnNav10 = New System.Windows.Forms.Button
        Me.btnNav9 = New System.Windows.Forms.Button
        Me.btnNav8 = New System.Windows.Forms.Button
        Me.btnNav7 = New System.Windows.Forms.Button
        Me.btnNav6 = New System.Windows.Forms.Button
        Me.btnNav5 = New System.Windows.Forms.Button
        Me.btnNav4 = New System.Windows.Forms.Button
        Me.btnNav3 = New System.Windows.Forms.Button
        Me.btnNav2 = New System.Windows.Forms.Button
        Me.btnNav1 = New System.Windows.Forms.Button
        Me.GPWorkSelectorTool = New System.Windows.Forms.GroupBox
        Me.pnlWorkViewerContext = New System.Windows.Forms.Panel
        Me.rdbPMView = New System.Windows.Forms.RadioButton
        Me.rdbUCView = New System.Windows.Forms.RadioButton
        Me.rdbStaffView = New System.Windows.Forms.RadioButton
        Me.btnChangeWorkViewerContext = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.cboWorkViewerContext = New System.Windows.Forms.ComboBox
        Me.llbOpenTestLog = New System.Windows.Forms.LinkLabel
        Me.txtTestLogNumber = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.llbFacilitySummary = New System.Windows.Forms.LinkLabel
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl5 = New System.Windows.Forms.ToolStripStatusLabel
        Me.llbTrackingNumber = New System.Windows.Forms.LinkLabel
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.llbOpenApplication = New System.Windows.Forms.LinkLabel
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.llbEnforcementRecord = New System.Windows.Forms.LinkLabel
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDataGridCount = New System.Windows.Forms.TextBox
        Me.LLSelectReport = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.dgvWorkViewer = New System.Windows.Forms.DataGridView
        Me.bgrLoadWorkViewer = New System.ComponentModel.BackgroundWorker
        Me.lblMessageLabel = New System.Windows.Forms.Label
        Me.bgrLoadButtons = New System.ComponentModel.BackgroundWorker
        Me.Panel4.SuspendLayout()
        Me.GPWorkSelectorTool.SuspendLayout()
        Me.pnlWorkViewerContext.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgvWorkViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiFile, Me.mmiTools, Me.mmiHelp, Me.mmiTesting})
        '
        'mmiFile
        '
        Me.mmiFile.Index = 0
        Me.mmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiExit})
        Me.mmiFile.Text = "&File"
        '
        'mmiExit
        '
        Me.mmiExit.Index = 0
        Me.mmiExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ
        Me.mmiExit.Text = "E&xit IAIP"
        '
        'mmiTools
        '
        Me.mmiTools.Index = 1
        Me.mmiTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiExport})
        Me.mmiTools.Text = "&Tools"
        '
        'mmiExport
        '
        Me.mmiExport.Index = 0
        Me.mmiExport.Text = "&Export list to Excel"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp, Me.mmiResetForm, Me.MenuItem1, Me.mmiAbout})
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online &Help"
        '
        'mmiResetForm
        '
        Me.mmiResetForm.Index = 1
        Me.mmiResetForm.Text = "&Reset All Forms"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "-"
        '
        'mmiAbout
        '
        Me.mmiAbout.Index = 3
        Me.mmiAbout.Text = "&About IAIP"
        '
        'mmiTesting
        '
        Me.mmiTesting.Index = 3
        Me.mmiTesting.Text = "Testing"
        Me.mmiTesting.Visible = False
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(792, 33)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "AIR Protection Branch Navigation Screen"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.btnNav40)
        Me.Panel4.Controls.Add(Me.btnNav39)
        Me.Panel4.Controls.Add(Me.btnNav38)
        Me.Panel4.Controls.Add(Me.btnNav37)
        Me.Panel4.Controls.Add(Me.btnNav36)
        Me.Panel4.Controls.Add(Me.btnNav35)
        Me.Panel4.Controls.Add(Me.btnNav34)
        Me.Panel4.Controls.Add(Me.btnNav33)
        Me.Panel4.Controls.Add(Me.btnNav32)
        Me.Panel4.Controls.Add(Me.btnNav31)
        Me.Panel4.Controls.Add(Me.btnNav30)
        Me.Panel4.Controls.Add(Me.btnNav29)
        Me.Panel4.Controls.Add(Me.btnNav21)
        Me.Panel4.Controls.Add(Me.btnNav20)
        Me.Panel4.Controls.Add(Me.btnNav19)
        Me.Panel4.Controls.Add(Me.btnNav18)
        Me.Panel4.Controls.Add(Me.btnNav17)
        Me.Panel4.Controls.Add(Me.btnNav16)
        Me.Panel4.Controls.Add(Me.btnNav15)
        Me.Panel4.Controls.Add(Me.btnNav28)
        Me.Panel4.Controls.Add(Me.btnNav27)
        Me.Panel4.Controls.Add(Me.btnNav26)
        Me.Panel4.Controls.Add(Me.btnNav25)
        Me.Panel4.Controls.Add(Me.btnNav24)
        Me.Panel4.Controls.Add(Me.btnNav23)
        Me.Panel4.Controls.Add(Me.btnNav22)
        Me.Panel4.Controls.Add(Me.btnNav14)
        Me.Panel4.Controls.Add(Me.btnNav13)
        Me.Panel4.Controls.Add(Me.btnNav12)
        Me.Panel4.Controls.Add(Me.btnNav11)
        Me.Panel4.Controls.Add(Me.btnNav10)
        Me.Panel4.Controls.Add(Me.btnNav9)
        Me.Panel4.Controls.Add(Me.btnNav8)
        Me.Panel4.Controls.Add(Me.btnNav7)
        Me.Panel4.Controls.Add(Me.btnNav6)
        Me.Panel4.Controls.Add(Me.btnNav5)
        Me.Panel4.Controls.Add(Me.btnNav4)
        Me.Panel4.Controls.Add(Me.btnNav3)
        Me.Panel4.Controls.Add(Me.btnNav2)
        Me.Panel4.Controls.Add(Me.btnNav1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 33)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(118, 368)
        Me.Panel4.TabIndex = 117
        '
        'btnNav40
        '
        Me.btnNav40.Location = New System.Drawing.Point(508, 191)
        Me.btnNav40.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav40.Name = "btnNav40"
        Me.btnNav40.Size = New System.Drawing.Size(84, 39)
        Me.btnNav40.TabIndex = 164
        Me.btnNav40.Text = "Button 40"
        Me.btnNav40.Visible = False
        '
        'btnNav39
        '
        Me.btnNav39.Location = New System.Drawing.Point(508, 146)
        Me.btnNav39.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav39.Name = "btnNav39"
        Me.btnNav39.Size = New System.Drawing.Size(84, 39)
        Me.btnNav39.TabIndex = 164
        Me.btnNav39.Text = "Button 39"
        Me.btnNav39.Visible = False
        '
        'btnNav38
        '
        Me.btnNav38.Location = New System.Drawing.Point(508, 101)
        Me.btnNav38.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav38.Name = "btnNav38"
        Me.btnNav38.Size = New System.Drawing.Size(84, 39)
        Me.btnNav38.TabIndex = 163
        Me.btnNav38.Text = "Button 38"
        Me.btnNav38.Visible = False
        '
        'btnNav37
        '
        Me.btnNav37.Location = New System.Drawing.Point(508, 55)
        Me.btnNav37.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav37.Name = "btnNav37"
        Me.btnNav37.Size = New System.Drawing.Size(84, 39)
        Me.btnNav37.TabIndex = 162
        Me.btnNav37.Text = "Button 37"
        Me.btnNav37.Visible = False
        '
        'btnNav36
        '
        Me.btnNav36.Location = New System.Drawing.Point(508, 9)
        Me.btnNav36.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav36.Name = "btnNav36"
        Me.btnNav36.Size = New System.Drawing.Size(84, 39)
        Me.btnNav36.TabIndex = 161
        Me.btnNav36.Text = "Button 36"
        Me.btnNav36.Visible = False
        '
        'btnNav35
        '
        Me.btnNav35.Location = New System.Drawing.Point(415, 288)
        Me.btnNav35.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav35.Name = "btnNav35"
        Me.btnNav35.Size = New System.Drawing.Size(84, 39)
        Me.btnNav35.TabIndex = 160
        Me.btnNav35.Text = "Button 35"
        Me.btnNav35.Visible = False
        '
        'btnNav34
        '
        Me.btnNav34.Location = New System.Drawing.Point(415, 238)
        Me.btnNav34.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav34.Name = "btnNav34"
        Me.btnNav34.Size = New System.Drawing.Size(84, 39)
        Me.btnNav34.TabIndex = 159
        Me.btnNav34.Text = "Button 34"
        Me.btnNav34.Visible = False
        '
        'btnNav33
        '
        Me.btnNav33.Location = New System.Drawing.Point(413, 191)
        Me.btnNav33.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav33.Name = "btnNav33"
        Me.btnNav33.Size = New System.Drawing.Size(84, 39)
        Me.btnNav33.TabIndex = 158
        Me.btnNav33.Text = "Button 33"
        Me.btnNav33.Visible = False
        '
        'btnNav32
        '
        Me.btnNav32.Location = New System.Drawing.Point(415, 146)
        Me.btnNav32.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav32.Name = "btnNav32"
        Me.btnNav32.Size = New System.Drawing.Size(84, 39)
        Me.btnNav32.TabIndex = 157
        Me.btnNav32.Text = "Button 32"
        Me.btnNav32.Visible = False
        '
        'btnNav31
        '
        Me.btnNav31.Location = New System.Drawing.Point(415, 101)
        Me.btnNav31.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav31.Name = "btnNav31"
        Me.btnNav31.Size = New System.Drawing.Size(84, 39)
        Me.btnNav31.TabIndex = 156
        Me.btnNav31.Text = "Button 31"
        Me.btnNav31.Visible = False
        '
        'btnNav30
        '
        Me.btnNav30.Location = New System.Drawing.Point(415, 55)
        Me.btnNav30.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav30.Name = "btnNav30"
        Me.btnNav30.Size = New System.Drawing.Size(84, 39)
        Me.btnNav30.TabIndex = 155
        Me.btnNav30.Text = "Button 30"
        Me.btnNav30.Visible = False
        '
        'btnNav29
        '
        Me.btnNav29.Location = New System.Drawing.Point(415, 9)
        Me.btnNav29.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav29.Name = "btnNav29"
        Me.btnNav29.Size = New System.Drawing.Size(84, 39)
        Me.btnNav29.TabIndex = 154
        Me.btnNav29.Text = "Button 29"
        Me.btnNav29.Visible = False
        '
        'btnNav21
        '
        Me.btnNav21.Location = New System.Drawing.Point(217, 288)
        Me.btnNav21.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav21.Name = "btnNav21"
        Me.btnNav21.Size = New System.Drawing.Size(84, 39)
        Me.btnNav21.TabIndex = 153
        Me.btnNav21.Text = "Button 21"
        Me.btnNav21.Visible = False
        '
        'btnNav20
        '
        Me.btnNav20.Location = New System.Drawing.Point(217, 238)
        Me.btnNav20.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav20.Name = "btnNav20"
        Me.btnNav20.Size = New System.Drawing.Size(84, 39)
        Me.btnNav20.TabIndex = 152
        Me.btnNav20.Text = "Button 20"
        Me.btnNav20.Visible = False
        '
        'btnNav19
        '
        Me.btnNav19.Location = New System.Drawing.Point(217, 191)
        Me.btnNav19.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav19.Name = "btnNav19"
        Me.btnNav19.Size = New System.Drawing.Size(84, 39)
        Me.btnNav19.TabIndex = 151
        Me.btnNav19.Text = "Button 19"
        Me.btnNav19.Visible = False
        '
        'btnNav18
        '
        Me.btnNav18.Location = New System.Drawing.Point(217, 146)
        Me.btnNav18.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav18.Name = "btnNav18"
        Me.btnNav18.Size = New System.Drawing.Size(84, 39)
        Me.btnNav18.TabIndex = 150
        Me.btnNav18.Text = "Button 18"
        Me.btnNav18.Visible = False
        '
        'btnNav17
        '
        Me.btnNav17.Location = New System.Drawing.Point(217, 101)
        Me.btnNav17.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav17.Name = "btnNav17"
        Me.btnNav17.Size = New System.Drawing.Size(84, 39)
        Me.btnNav17.TabIndex = 149
        Me.btnNav17.Text = "Button 17"
        Me.btnNav17.Visible = False
        '
        'btnNav16
        '
        Me.btnNav16.Location = New System.Drawing.Point(217, 55)
        Me.btnNav16.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav16.Name = "btnNav16"
        Me.btnNav16.Size = New System.Drawing.Size(84, 39)
        Me.btnNav16.TabIndex = 148
        Me.btnNav16.Text = "Button 16"
        Me.btnNav16.Visible = False
        '
        'btnNav15
        '
        Me.btnNav15.Location = New System.Drawing.Point(217, 9)
        Me.btnNav15.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav15.Name = "btnNav15"
        Me.btnNav15.Size = New System.Drawing.Size(84, 39)
        Me.btnNav15.TabIndex = 147
        Me.btnNav15.Text = "Button 15"
        Me.btnNav15.Visible = False
        '
        'btnNav28
        '
        Me.btnNav28.Location = New System.Drawing.Point(320, 288)
        Me.btnNav28.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav28.Name = "btnNav28"
        Me.btnNav28.Size = New System.Drawing.Size(84, 39)
        Me.btnNav28.TabIndex = 146
        Me.btnNav28.Text = "Button 28"
        Me.btnNav28.Visible = False
        '
        'btnNav27
        '
        Me.btnNav27.Location = New System.Drawing.Point(320, 238)
        Me.btnNav27.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav27.Name = "btnNav27"
        Me.btnNav27.Size = New System.Drawing.Size(84, 39)
        Me.btnNav27.TabIndex = 145
        Me.btnNav27.Text = "Button 27"
        Me.btnNav27.Visible = False
        '
        'btnNav26
        '
        Me.btnNav26.Location = New System.Drawing.Point(320, 191)
        Me.btnNav26.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav26.Name = "btnNav26"
        Me.btnNav26.Size = New System.Drawing.Size(84, 39)
        Me.btnNav26.TabIndex = 144
        Me.btnNav26.Text = "Button 26"
        Me.btnNav26.Visible = False
        '
        'btnNav25
        '
        Me.btnNav25.Location = New System.Drawing.Point(320, 146)
        Me.btnNav25.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav25.Name = "btnNav25"
        Me.btnNav25.Size = New System.Drawing.Size(84, 39)
        Me.btnNav25.TabIndex = 143
        Me.btnNav25.Text = "Button 25"
        Me.btnNav25.Visible = False
        '
        'btnNav24
        '
        Me.btnNav24.Location = New System.Drawing.Point(320, 101)
        Me.btnNav24.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav24.Name = "btnNav24"
        Me.btnNav24.Size = New System.Drawing.Size(84, 39)
        Me.btnNav24.TabIndex = 142
        Me.btnNav24.Text = "Button 24"
        Me.btnNav24.Visible = False
        '
        'btnNav23
        '
        Me.btnNav23.Location = New System.Drawing.Point(320, 55)
        Me.btnNav23.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav23.Name = "btnNav23"
        Me.btnNav23.Size = New System.Drawing.Size(84, 39)
        Me.btnNav23.TabIndex = 141
        Me.btnNav23.Text = "Button 23"
        Me.btnNav23.Visible = False
        '
        'btnNav22
        '
        Me.btnNav22.Location = New System.Drawing.Point(320, 9)
        Me.btnNav22.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav22.Name = "btnNav22"
        Me.btnNav22.Size = New System.Drawing.Size(84, 39)
        Me.btnNav22.TabIndex = 140
        Me.btnNav22.Text = "Button 22"
        Me.btnNav22.Visible = False
        '
        'btnNav14
        '
        Me.btnNav14.Location = New System.Drawing.Point(121, 285)
        Me.btnNav14.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav14.Name = "btnNav14"
        Me.btnNav14.Size = New System.Drawing.Size(84, 39)
        Me.btnNav14.TabIndex = 139
        Me.btnNav14.Text = "Button 14"
        Me.btnNav14.Visible = False
        '
        'btnNav13
        '
        Me.btnNav13.Location = New System.Drawing.Point(121, 237)
        Me.btnNav13.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav13.Name = "btnNav13"
        Me.btnNav13.Size = New System.Drawing.Size(84, 39)
        Me.btnNav13.TabIndex = 138
        Me.btnNav13.Text = "Button 13"
        Me.btnNav13.Visible = False
        '
        'btnNav12
        '
        Me.btnNav12.Location = New System.Drawing.Point(121, 191)
        Me.btnNav12.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav12.Name = "btnNav12"
        Me.btnNav12.Size = New System.Drawing.Size(84, 39)
        Me.btnNav12.TabIndex = 137
        Me.btnNav12.Text = "Button 12"
        Me.btnNav12.Visible = False
        '
        'btnNav11
        '
        Me.btnNav11.Location = New System.Drawing.Point(121, 146)
        Me.btnNav11.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav11.Name = "btnNav11"
        Me.btnNav11.Size = New System.Drawing.Size(84, 39)
        Me.btnNav11.TabIndex = 136
        Me.btnNav11.Text = "Button 11"
        Me.btnNav11.Visible = False
        '
        'btnNav10
        '
        Me.btnNav10.Location = New System.Drawing.Point(121, 101)
        Me.btnNav10.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav10.Name = "btnNav10"
        Me.btnNav10.Size = New System.Drawing.Size(84, 39)
        Me.btnNav10.TabIndex = 135
        Me.btnNav10.Text = "Button 10"
        Me.btnNav10.Visible = False
        '
        'btnNav9
        '
        Me.btnNav9.Location = New System.Drawing.Point(121, 55)
        Me.btnNav9.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav9.Name = "btnNav9"
        Me.btnNav9.Size = New System.Drawing.Size(84, 39)
        Me.btnNav9.TabIndex = 134
        Me.btnNav9.Text = "Button 9"
        Me.btnNav9.Visible = False
        '
        'btnNav8
        '
        Me.btnNav8.Location = New System.Drawing.Point(121, 9)
        Me.btnNav8.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav8.Name = "btnNav8"
        Me.btnNav8.Size = New System.Drawing.Size(84, 39)
        Me.btnNav8.TabIndex = 133
        Me.btnNav8.Text = "Button 8"
        Me.btnNav8.Visible = False
        '
        'btnNav7
        '
        Me.btnNav7.Location = New System.Drawing.Point(11, 285)
        Me.btnNav7.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav7.Name = "btnNav7"
        Me.btnNav7.Size = New System.Drawing.Size(84, 39)
        Me.btnNav7.TabIndex = 132
        Me.btnNav7.Text = "Button 7"
        Me.btnNav7.Visible = False
        '
        'btnNav6
        '
        Me.btnNav6.Location = New System.Drawing.Point(11, 235)
        Me.btnNav6.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav6.Name = "btnNav6"
        Me.btnNav6.Size = New System.Drawing.Size(84, 39)
        Me.btnNav6.TabIndex = 131
        Me.btnNav6.Text = "Button 6"
        Me.btnNav6.Visible = False
        '
        'btnNav5
        '
        Me.btnNav5.Location = New System.Drawing.Point(11, 188)
        Me.btnNav5.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav5.Name = "btnNav5"
        Me.btnNav5.Size = New System.Drawing.Size(84, 39)
        Me.btnNav5.TabIndex = 130
        Me.btnNav5.Text = "Button 5"
        Me.btnNav5.Visible = False
        '
        'btnNav4
        '
        Me.btnNav4.Location = New System.Drawing.Point(11, 143)
        Me.btnNav4.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav4.Name = "btnNav4"
        Me.btnNav4.Size = New System.Drawing.Size(84, 39)
        Me.btnNav4.TabIndex = 129
        Me.btnNav4.Text = "Button 4"
        Me.btnNav4.Visible = False
        '
        'btnNav3
        '
        Me.btnNav3.Location = New System.Drawing.Point(11, 98)
        Me.btnNav3.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav3.Name = "btnNav3"
        Me.btnNav3.Size = New System.Drawing.Size(84, 39)
        Me.btnNav3.TabIndex = 128
        Me.btnNav3.Text = "Button 3"
        Me.btnNav3.Visible = False
        '
        'btnNav2
        '
        Me.btnNav2.Location = New System.Drawing.Point(11, 52)
        Me.btnNav2.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav2.Name = "btnNav2"
        Me.btnNav2.Size = New System.Drawing.Size(84, 39)
        Me.btnNav2.TabIndex = 127
        Me.btnNav2.Text = "Button 2"
        Me.btnNav2.Visible = False
        '
        'btnNav1
        '
        Me.btnNav1.Location = New System.Drawing.Point(11, 6)
        Me.btnNav1.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNav1.Name = "btnNav1"
        Me.btnNav1.Size = New System.Drawing.Size(84, 39)
        Me.btnNav1.TabIndex = 126
        Me.btnNav1.Text = "Button 1"
        Me.btnNav1.Visible = False
        '
        'GPWorkSelectorTool
        '
        Me.GPWorkSelectorTool.Controls.Add(Me.pnlWorkViewerContext)
        Me.GPWorkSelectorTool.Controls.Add(Me.btnChangeWorkViewerContext)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label9)
        Me.GPWorkSelectorTool.Controls.Add(Me.cboWorkViewerContext)
        Me.GPWorkSelectorTool.Controls.Add(Me.llbOpenTestLog)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtTestLogNumber)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label8)
        Me.GPWorkSelectorTool.Controls.Add(Me.llbFacilitySummary)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label7)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtAIRSNumber)
        Me.GPWorkSelectorTool.Controls.Add(Me.StatusStrip1)
        Me.GPWorkSelectorTool.Controls.Add(Me.llbTrackingNumber)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtTrackingNumber)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label2)
        Me.GPWorkSelectorTool.Controls.Add(Me.llbOpenApplication)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label6)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtApplicationNumber)
        Me.GPWorkSelectorTool.Controls.Add(Me.llbEnforcementRecord)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label5)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtEnforcementNumber)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label4)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtDataGridCount)
        Me.GPWorkSelectorTool.Controls.Add(Me.LLSelectReport)
        Me.GPWorkSelectorTool.Controls.Add(Me.Label3)
        Me.GPWorkSelectorTool.Controls.Add(Me.txtReferenceNumber)
        Me.GPWorkSelectorTool.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GPWorkSelectorTool.Location = New System.Drawing.Point(118, 235)
        Me.GPWorkSelectorTool.Margin = New System.Windows.Forms.Padding(2)
        Me.GPWorkSelectorTool.Name = "GPWorkSelectorTool"
        Me.GPWorkSelectorTool.Padding = New System.Windows.Forms.Padding(2)
        Me.GPWorkSelectorTool.Size = New System.Drawing.Size(674, 166)
        Me.GPWorkSelectorTool.TabIndex = 120
        Me.GPWorkSelectorTool.TabStop = False
        '
        'pnlWorkViewerContext
        '
        Me.pnlWorkViewerContext.Controls.Add(Me.rdbPMView)
        Me.pnlWorkViewerContext.Controls.Add(Me.rdbUCView)
        Me.pnlWorkViewerContext.Controls.Add(Me.rdbStaffView)
        Me.pnlWorkViewerContext.Location = New System.Drawing.Point(337, 112)
        Me.pnlWorkViewerContext.Name = "pnlWorkViewerContext"
        Me.pnlWorkViewerContext.Size = New System.Drawing.Size(261, 28)
        Me.pnlWorkViewerContext.TabIndex = 298
        '
        'rdbPMView
        '
        Me.rdbPMView.AutoSize = True
        Me.rdbPMView.Location = New System.Drawing.Point(158, 2)
        Me.rdbPMView.Name = "rdbPMView"
        Me.rdbPMView.Size = New System.Drawing.Size(90, 17)
        Me.rdbPMView.TabIndex = 297
        Me.rdbPMView.Text = "Program View"
        Me.rdbPMView.UseVisualStyleBackColor = True
        '
        'rdbUCView
        '
        Me.rdbUCView.AutoSize = True
        Me.rdbUCView.Location = New System.Drawing.Point(82, 2)
        Me.rdbUCView.Name = "rdbUCView"
        Me.rdbUCView.Size = New System.Drawing.Size(70, 17)
        Me.rdbUCView.TabIndex = 296
        Me.rdbUCView.Text = "Unit View"
        Me.rdbUCView.UseVisualStyleBackColor = True
        '
        'rdbStaffView
        '
        Me.rdbStaffView.AutoSize = True
        Me.rdbStaffView.Checked = True
        Me.rdbStaffView.Location = New System.Drawing.Point(3, 2)
        Me.rdbStaffView.Name = "rdbStaffView"
        Me.rdbStaffView.Size = New System.Drawing.Size(73, 17)
        Me.rdbStaffView.TabIndex = 295
        Me.rdbStaffView.TabStop = True
        Me.rdbStaffView.Text = "Staff View"
        Me.rdbStaffView.UseVisualStyleBackColor = True
        '
        'btnChangeWorkViewerContext
        '
        Me.btnChangeWorkViewerContext.AutoSize = True
        Me.btnChangeWorkViewerContext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnChangeWorkViewerContext.Location = New System.Drawing.Point(270, 112)
        Me.btnChangeWorkViewerContext.Name = "btnChangeWorkViewerContext"
        Me.btnChangeWorkViewerContext.Size = New System.Drawing.Size(61, 23)
        Me.btnChangeWorkViewerContext.TabIndex = 292
        Me.btnChangeWorkViewerContext.Text = "Loading…"
        Me.btnChangeWorkViewerContext.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 97)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 13)
        Me.Label9.TabIndex = 294
        Me.Label9.Text = "Change Current List"
        '
        'cboWorkViewerContext
        '
        Me.cboWorkViewerContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkViewerContext.FormattingEnabled = True
        Me.cboWorkViewerContext.Location = New System.Drawing.Point(10, 113)
        Me.cboWorkViewerContext.Name = "cboWorkViewerContext"
        Me.cboWorkViewerContext.Size = New System.Drawing.Size(254, 21)
        Me.cboWorkViewerContext.TabIndex = 293
        '
        'llbOpenTestLog
        '
        Me.llbOpenTestLog.AutoSize = True
        Me.llbOpenTestLog.Location = New System.Drawing.Point(544, 73)
        Me.llbOpenTestLog.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenTestLog.Name = "llbOpenTestLog"
        Me.llbOpenTestLog.Size = New System.Drawing.Size(54, 13)
        Me.llbOpenTestLog.TabIndex = 270
        Me.llbOpenTestLog.TabStop = True
        Me.llbOpenTestLog.Text = "Open Log"
        '
        'txtTestLogNumber
        '
        Me.txtTestLogNumber.Location = New System.Drawing.Point(450, 70)
        Me.txtTestLogNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTestLogNumber.MaxLength = 10
        Me.txtTestLogNumber.Name = "txtTestLogNumber"
        Me.txtTestLogNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtTestLogNumber.TabIndex = 269
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(447, 55)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 268
        Me.Label8.Text = "ISMP Test Log #"
        '
        'llbFacilitySummary
        '
        Me.llbFacilitySummary.AutoSize = True
        Me.llbFacilitySummary.Location = New System.Drawing.Point(104, 33)
        Me.llbFacilitySummary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbFacilitySummary.Name = "llbFacilitySummary"
        Me.llbFacilitySummary.Size = New System.Drawing.Size(79, 13)
        Me.llbFacilitySummary.TabIndex = 267
        Me.llbFacilitySummary.TabStop = True
        Me.llbFacilitySummary.Text = "Open Summary"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 15)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 265
        Me.Label7.Text = "Facility Summary"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(10, 30)
        Me.txtAIRSNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtAIRSNumber.MaxLength = 8
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtAIRSNumber.TabIndex = 266
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.pnl1, Me.pnl2, Me.pnl3, Me.pnl4, Me.pnl5})
        Me.StatusStrip1.Location = New System.Drawing.Point(2, 140)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(670, 24)
        Me.StatusStrip1.TabIndex = 264
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 18)
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ToolStripProgressBar1.Visible = False
        '
        'pnl1
        '
        Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(462, 19)
        Me.pnl1.Spring = True
        Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl2
        '
        Me.pnl2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(43, 19)
        Me.pnl2.Text = "Name"
        '
        'pnl3
        '
        Me.pnl3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(35, 19)
        Me.pnl3.Text = "Date"
        '
        'pnl4
        '
        Me.pnl4.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl4.Name = "pnl4"
        Me.pnl4.Size = New System.Drawing.Size(79, 19)
        Me.pnl4.Text = "Environment"
        '
        'pnl5
        '
        Me.pnl5.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl5.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.pnl5.Name = "pnl5"
        Me.pnl5.Size = New System.Drawing.Size(40, 19)
        Me.pnl5.Text = "Blank"
        '
        'llbTrackingNumber
        '
        Me.llbTrackingNumber.AutoSize = True
        Me.llbTrackingNumber.Location = New System.Drawing.Point(317, 73)
        Me.llbTrackingNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbTrackingNumber.Name = "llbTrackingNumber"
        Me.llbTrackingNumber.Size = New System.Drawing.Size(114, 13)
        Me.llbTrackingNumber.TabIndex = 263
        Me.llbTrackingNumber.TabStop = True
        Me.llbTrackingNumber.Text = "Open Compliance Item"
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(223, 70)
        Me.txtTrackingNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTrackingNumber.MaxLength = 10
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtTrackingNumber.TabIndex = 262
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 55)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 261
        Me.Label2.Text = "SSCP Item #"
        '
        'llbOpenApplication
        '
        Me.llbOpenApplication.AutoSize = True
        Me.llbOpenApplication.Location = New System.Drawing.Point(544, 34)
        Me.llbOpenApplication.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbOpenApplication.Name = "llbOpenApplication"
        Me.llbOpenApplication.Size = New System.Drawing.Size(88, 13)
        Me.llbOpenApplication.TabIndex = 260
        Me.llbOpenApplication.TabStop = True
        Me.llbOpenApplication.Text = "Open Application"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(447, 15)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 259
        Me.Label6.Text = "SSPP Application #"
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Location = New System.Drawing.Point(450, 30)
        Me.txtApplicationNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtApplicationNumber.MaxLength = 10
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtApplicationNumber.TabIndex = 258
        '
        'llbEnforcementRecord
        '
        Me.llbEnforcementRecord.AutoSize = True
        Me.llbEnforcementRecord.Location = New System.Drawing.Point(317, 33)
        Me.llbEnforcementRecord.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbEnforcementRecord.Name = "llbEnforcementRecord"
        Me.llbEnforcementRecord.Size = New System.Drawing.Size(96, 13)
        Me.llbEnforcementRecord.TabIndex = 256
        Me.llbEnforcementRecord.TabStop = True
        Me.llbEnforcementRecord.Text = "Open Enforcement"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(220, 15)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 255
        Me.Label5.Text = "Enforcement #"
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(223, 30)
        Me.txtEnforcementNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtEnforcementNumber.MaxLength = 8
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtEnforcementNumber.TabIndex = 254
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(621, 116)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 13)
        Me.Label4.TabIndex = 253
        Me.Label4.Text = "#"
        '
        'txtDataGridCount
        '
        Me.txtDataGridCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataGridCount.Location = New System.Drawing.Point(639, 113)
        Me.txtDataGridCount.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDataGridCount.Name = "txtDataGridCount"
        Me.txtDataGridCount.Size = New System.Drawing.Size(31, 20)
        Me.txtDataGridCount.TabIndex = 252
        '
        'LLSelectReport
        '
        Me.LLSelectReport.AutoSize = True
        Me.LLSelectReport.Location = New System.Drawing.Point(103, 73)
        Me.LLSelectReport.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LLSelectReport.Name = "LLSelectReport"
        Me.LLSelectReport.Size = New System.Drawing.Size(68, 13)
        Me.LLSelectReport.TabIndex = 251
        Me.LLSelectReport.TabStop = True
        Me.LLSelectReport.Text = "Open Report"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 55)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 249
        Me.Label3.Text = "ISMP Test Report #"
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(10, 70)
        Me.txtReferenceNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtReferenceNumber.MaxLength = 9
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(90, 20)
        Me.txtReferenceNumber.TabIndex = 250
        '
        'dgvWorkViewer
        '
        Me.dgvWorkViewer.AllowUserToAddRows = False
        Me.dgvWorkViewer.AllowUserToDeleteRows = False
        Me.dgvWorkViewer.AllowUserToOrderColumns = True
        Me.dgvWorkViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvWorkViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWorkViewer.Location = New System.Drawing.Point(118, 33)
        Me.dgvWorkViewer.Name = "dgvWorkViewer"
        Me.dgvWorkViewer.ReadOnly = True
        Me.dgvWorkViewer.Size = New System.Drawing.Size(674, 202)
        Me.dgvWorkViewer.TabIndex = 123
        '
        'bgrLoadWorkViewer
        '
        '
        'lblMessageLabel
        '
        Me.lblMessageLabel.AutoSize = True
        Me.lblMessageLabel.Location = New System.Drawing.Point(125, 52)
        Me.lblMessageLabel.Name = "lblMessageLabel"
        Me.lblMessageLabel.Size = New System.Drawing.Size(0, 13)
        Me.lblMessageLabel.TabIndex = 124
        Me.lblMessageLabel.Visible = False
        '
        'bgrLoadButtons
        '
        '
        'IAIPNavigation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 401)
        Me.Controls.Add(Me.dgvWorkViewer)
        Me.Controls.Add(Me.GPWorkSelectorTool)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMessageLabel)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPNavigation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Air Protection Branch Navigation Screen"
        Me.Panel4.ResumeLayout(False)
        Me.GPWorkSelectorTool.ResumeLayout(False)
        Me.GPWorkSelectorTool.PerformLayout()
        Me.pnlWorkViewerContext.ResumeLayout(False)
        Me.pnlWorkViewerContext.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgvWorkViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents mmiTools As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiAbout As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GPWorkSelectorTool As System.Windows.Forms.GroupBox
    Friend WithEvents llbTrackingNumber As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents llbOpenApplication As System.Windows.Forms.LinkLabel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents llbEnforcementRecord As System.Windows.Forms.LinkLabel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDataGridCount As System.Windows.Forms.TextBox
    Friend WithEvents LLSelectReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents llbFacilitySummary As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents dgvWorkViewer As System.Windows.Forms.DataGridView
    Friend WithEvents pnl4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgrLoadWorkViewer As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblMessageLabel As System.Windows.Forms.Label
    Friend WithEvents btnNav7 As System.Windows.Forms.Button
    Friend WithEvents btnNav6 As System.Windows.Forms.Button
    Friend WithEvents btnNav5 As System.Windows.Forms.Button
    Friend WithEvents btnNav4 As System.Windows.Forms.Button
    Friend WithEvents btnNav3 As System.Windows.Forms.Button
    Friend WithEvents btnNav2 As System.Windows.Forms.Button
    Friend WithEvents btnNav1 As System.Windows.Forms.Button
    Friend WithEvents btnNav12 As System.Windows.Forms.Button
    Friend WithEvents btnNav11 As System.Windows.Forms.Button
    Friend WithEvents btnNav10 As System.Windows.Forms.Button
    Friend WithEvents btnNav9 As System.Windows.Forms.Button
    Friend WithEvents btnNav8 As System.Windows.Forms.Button
    Friend WithEvents btnNav13 As System.Windows.Forms.Button
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents btnNav14 As System.Windows.Forms.Button
    Friend WithEvents llbOpenTestLog As System.Windows.Forms.LinkLabel
    Friend WithEvents txtTestLogNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents bgrLoadButtons As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnNav21 As System.Windows.Forms.Button
    Friend WithEvents btnNav20 As System.Windows.Forms.Button
    Friend WithEvents btnNav19 As System.Windows.Forms.Button
    Friend WithEvents btnNav18 As System.Windows.Forms.Button
    Friend WithEvents btnNav17 As System.Windows.Forms.Button
    Friend WithEvents btnNav16 As System.Windows.Forms.Button
    Friend WithEvents btnNav15 As System.Windows.Forms.Button
    Friend WithEvents btnNav28 As System.Windows.Forms.Button
    Friend WithEvents btnNav27 As System.Windows.Forms.Button
    Friend WithEvents btnNav26 As System.Windows.Forms.Button
    Friend WithEvents btnNav25 As System.Windows.Forms.Button
    Friend WithEvents btnNav24 As System.Windows.Forms.Button
    Friend WithEvents btnNav23 As System.Windows.Forms.Button
    Friend WithEvents btnNav22 As System.Windows.Forms.Button
    Friend WithEvents btnNav29 As System.Windows.Forms.Button
    Friend WithEvents btnNav31 As System.Windows.Forms.Button
    Friend WithEvents btnNav30 As System.Windows.Forms.Button
    Friend WithEvents btnNav33 As System.Windows.Forms.Button
    Friend WithEvents btnNav32 As System.Windows.Forms.Button
    Friend WithEvents btnNav34 As System.Windows.Forms.Button
    Friend WithEvents btnNav35 As System.Windows.Forms.Button
    Friend WithEvents btnNav36 As System.Windows.Forms.Button
    Friend WithEvents btnNav37 As System.Windows.Forms.Button
    Friend WithEvents btnNav38 As System.Windows.Forms.Button
    Friend WithEvents btnNav39 As System.Windows.Forms.Button
    Friend WithEvents btnChangeWorkViewerContext As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboWorkViewerContext As System.Windows.Forms.ComboBox
    Friend WithEvents rdbPMView As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUCView As System.Windows.Forms.RadioButton
    Friend WithEvents rdbStaffView As System.Windows.Forms.RadioButton
    Friend WithEvents mmiExport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiTesting As System.Windows.Forms.MenuItem
    Friend WithEvents mmiResetForm As System.Windows.Forms.MenuItem
    Friend WithEvents btnNav40 As System.Windows.Forms.Button
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents pnlWorkViewerContext As System.Windows.Forms.Panel
End Class
