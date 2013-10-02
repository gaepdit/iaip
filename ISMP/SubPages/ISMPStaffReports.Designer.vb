<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPStaffReports
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPStaffReports))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.SCStaffReport = New System.Windows.Forms.SplitContainer
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.llbExportStatsToWord = New System.Windows.Forms.LinkLabel
        Me.llbRunEngineerStatReport = New System.Windows.Forms.LinkLabel
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.DTPUnitEnd = New System.Windows.Forms.DateTimePicker
        Me.DTPUnitStart = New System.Windows.Forms.DateTimePicker
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.rdbUnitStatsAll = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.rdbUnitDateCompleted = New System.Windows.Forms.RadioButton
        Me.rdbUnitDateTestStarted = New System.Windows.Forms.RadioButton
        Me.rdbUnitDateReceived = New System.Windows.Forms.RadioButton
        Me.txtEngineerStatistics = New System.Windows.Forms.TextBox
        Me.TCStaffReports = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SCStaffReport.Panel1.SuspendLayout()
        Me.SCStaffReport.Panel2.SuspendLayout()
        Me.SCStaffReport.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.TCStaffReports.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(775, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "Back"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 489)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(775, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(775, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mmiHelp
        '
        Me.mmiHelp.Name = "mmiHelp"
        Me.mmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.mmiHelp.Text = "Help"
        '
        'SCStaffReport
        '
        Me.SCStaffReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCStaffReport.Location = New System.Drawing.Point(3, 3)
        Me.SCStaffReport.Name = "SCStaffReport"
        Me.SCStaffReport.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCStaffReport.Panel1
        '
        Me.SCStaffReport.Panel1.Controls.Add(Me.Panel4)
        Me.SCStaffReport.Panel1.Controls.Add(Me.GroupBox10)
        '
        'SCStaffReport.Panel2
        '
        Me.SCStaffReport.Panel2.Controls.Add(Me.txtEngineerStatistics)
        Me.SCStaffReport.Size = New System.Drawing.Size(761, 408)
        Me.SCStaffReport.SplitterDistance = 129
        Me.SCStaffReport.TabIndex = 6
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.llbExportStatsToWord)
        Me.Panel4.Controls.Add(Me.llbRunEngineerStatReport)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.DTPUnitEnd)
        Me.Panel4.Controls.Add(Me.DTPUnitStart)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(232, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(529, 129)
        Me.Panel4.TabIndex = 145
        '
        'llbExportStatsToWord
        '
        Me.llbExportStatsToWord.AutoSize = True
        Me.llbExportStatsToWord.Location = New System.Drawing.Point(14, 77)
        Me.llbExportStatsToWord.Name = "llbExportStatsToWord"
        Me.llbExportStatsToWord.Size = New System.Drawing.Size(116, 13)
        Me.llbExportStatsToWord.TabIndex = 153
        Me.llbExportStatsToWord.TabStop = True
        Me.llbExportStatsToWord.Text = "Export Results to Word"
        '
        'llbRunEngineerStatReport
        '
        Me.llbRunEngineerStatReport.AutoSize = True
        Me.llbRunEngineerStatReport.Location = New System.Drawing.Point(14, 53)
        Me.llbRunEngineerStatReport.Name = "llbRunEngineerStatReport"
        Me.llbRunEngineerStatReport.Size = New System.Drawing.Size(65, 13)
        Me.llbRunEngineerStatReport.TabIndex = 152
        Me.llbRunEngineerStatReport.TabStop = True
        Me.llbRunEngineerStatReport.Text = "Run Report "
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(134, 5)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(55, 13)
        Me.Label29.TabIndex = 150
        Me.Label29.Text = "End Date "
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(14, 5)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(55, 13)
        Me.Label28.TabIndex = 149
        Me.Label28.Text = "Start Date"
        '
        'DTPUnitEnd
        '
        Me.DTPUnitEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitEnd.Location = New System.Drawing.Point(150, 21)
        Me.DTPUnitEnd.Name = "DTPUnitEnd"
        Me.DTPUnitEnd.Size = New System.Drawing.Size(112, 20)
        Me.DTPUnitEnd.TabIndex = 148
        Me.DTPUnitEnd.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'DTPUnitStart
        '
        Me.DTPUnitStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitStart.Location = New System.Drawing.Point(30, 21)
        Me.DTPUnitStart.Name = "DTPUnitStart"
        Me.DTPUnitStart.Size = New System.Drawing.Size(104, 20)
        Me.DTPUnitStart.TabIndex = 147
        Me.DTPUnitStart.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.rdbUnitStatsAll)
        Me.GroupBox10.Controls.Add(Me.Label2)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateCompleted)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateTestStarted)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateReceived)
        Me.GroupBox10.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox10.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(232, 129)
        Me.GroupBox10.TabIndex = 144
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Date Bias"
        '
        'rdbUnitStatsAll
        '
        Me.rdbUnitStatsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitStatsAll.Location = New System.Drawing.Point(8, 88)
        Me.rdbUnitStatsAll.Name = "rdbUnitStatsAll"
        Me.rdbUnitStatsAll.Size = New System.Drawing.Size(104, 16)
        Me.rdbUnitStatsAll.TabIndex = 152
        Me.rdbUnitStatsAll.Text = "All Test Reports"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(185, 13)
        Me.Label2.TabIndex = 153
        Me.Label2.Text = "(All Dates - Excluding SM23 Archives)"
        '
        'rdbUnitDateCompleted
        '
        Me.rdbUnitDateCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateCompleted.Location = New System.Drawing.Point(8, 64)
        Me.rdbUnitDateCompleted.Name = "rdbUnitDateCompleted"
        Me.rdbUnitDateCompleted.Size = New System.Drawing.Size(144, 24)
        Me.rdbUnitDateCompleted.TabIndex = 2
        Me.rdbUnitDateCompleted.Text = "Date Report Completed"
        '
        'rdbUnitDateTestStarted
        '
        Me.rdbUnitDateTestStarted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateTestStarted.Location = New System.Drawing.Point(8, 16)
        Me.rdbUnitDateTestStarted.Name = "rdbUnitDateTestStarted"
        Me.rdbUnitDateTestStarted.Size = New System.Drawing.Size(136, 24)
        Me.rdbUnitDateTestStarted.TabIndex = 1
        Me.rdbUnitDateTestStarted.Text = "Date Test Started"
        '
        'rdbUnitDateReceived
        '
        Me.rdbUnitDateReceived.Checked = True
        Me.rdbUnitDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateReceived.Location = New System.Drawing.Point(8, 40)
        Me.rdbUnitDateReceived.Name = "rdbUnitDateReceived"
        Me.rdbUnitDateReceived.Size = New System.Drawing.Size(104, 24)
        Me.rdbUnitDateReceived.TabIndex = 0
        Me.rdbUnitDateReceived.TabStop = True
        Me.rdbUnitDateReceived.Text = "Date Received"
        '
        'txtEngineerStatistics
        '
        Me.txtEngineerStatistics.AcceptsReturn = True
        Me.txtEngineerStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEngineerStatistics.Location = New System.Drawing.Point(0, 0)
        Me.txtEngineerStatistics.Multiline = True
        Me.txtEngineerStatistics.Name = "txtEngineerStatistics"
        Me.txtEngineerStatistics.ReadOnly = True
        Me.txtEngineerStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEngineerStatistics.Size = New System.Drawing.Size(761, 275)
        Me.txtEngineerStatistics.TabIndex = 157
        '
        'TCStaffReports
        '
        Me.TCStaffReports.Controls.Add(Me.TabPage1)
        Me.TCStaffReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCStaffReports.Location = New System.Drawing.Point(0, 49)
        Me.TCStaffReports.Name = "TCStaffReports"
        Me.TCStaffReports.SelectedIndex = 0
        Me.TCStaffReports.Size = New System.Drawing.Size(775, 440)
        Me.TCStaffReports.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SCStaffReport)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(767, 414)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Engineer Statistics"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ISMPStaffReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 511)
        Me.Controls.Add(Me.TCStaffReports)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "ISMPStaffReports"
        Me.Text = "ISMP Staff Reports"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SCStaffReport.Panel1.ResumeLayout(False)
        Me.SCStaffReport.Panel2.ResumeLayout(False)
        Me.SCStaffReport.Panel2.PerformLayout()
        Me.SCStaffReport.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.TCStaffReports.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SCStaffReport As System.Windows.Forms.SplitContainer
    Friend WithEvents TCStaffReports As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbUnitStatsAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdbUnitDateCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateTestStarted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents llbExportStatsToWord As System.Windows.Forms.LinkLabel
    Friend WithEvents llbRunEngineerStatReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents DTPUnitEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPUnitStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtEngineerStatistics As System.Windows.Forms.TextBox
End Class
