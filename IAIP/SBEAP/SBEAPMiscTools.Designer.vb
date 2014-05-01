<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPMiscTools
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SBEAPMiscTools))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbExport = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.label1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dgvMiscTools = New System.Windows.Forms.DataGridView
        Me.pnlMiscTools = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCount = New System.Windows.Forms.TextBox
        Me.lblCount = New System.Windows.Forms.Label
        Me.btnGetContactData = New System.Windows.Forms.Button
        Me.TCMiscTools = New System.Windows.Forms.TabControl
        Me.TPExports = New System.Windows.Forms.TabPage
        Me.TPCaseWork = New System.Windows.Forms.TabPage
        Me.CRVCaseWork = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.pnlCaseWork = New System.Windows.Forms.Panel
        Me.chbAllDates = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.DTPEndDate = New System.Windows.Forms.DateTimePicker
        Me.DTPStartDate = New System.Windows.Forms.DateTimePicker
        Me.btnGetCaseLog = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgvMiscTools, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMiscTools.SuspendLayout()
        Me.TCMiscTools.SuspendLayout()
        Me.TPExports.SuspendLayout()
        Me.TPCaseWork.SuspendLayout()
        Me.pnlCaseWork.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExport, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 11
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbExport
        '
        Me.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbExport.Image = CType(resources.GetObject("tsbExport.Image"), System.Drawing.Image)
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(23, 22)
        Me.tsbExport.Text = "Export to Excel"
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
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.label1, Me.Label2, Me.Label3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip1.TabIndex = 10
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'label1
        '
        Me.label1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.label1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(769, 17)
        Me.label1.Spring = True
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Label2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(4, 17)
        '
        'Label3
        '
        Me.Label3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Label3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(4, 17)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'dgvMiscTools
        '
        Me.dgvMiscTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMiscTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMiscTools.Location = New System.Drawing.Point(3, 89)
        Me.dgvMiscTools.Name = "dgvMiscTools"
        Me.dgvMiscTools.Size = New System.Drawing.Size(778, 377)
        Me.dgvMiscTools.TabIndex = 12
        '
        'pnlMiscTools
        '
        Me.pnlMiscTools.Controls.Add(Me.Label6)
        Me.pnlMiscTools.Controls.Add(Me.txtCount)
        Me.pnlMiscTools.Controls.Add(Me.lblCount)
        Me.pnlMiscTools.Controls.Add(Me.btnGetContactData)
        Me.pnlMiscTools.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMiscTools.Location = New System.Drawing.Point(3, 3)
        Me.pnlMiscTools.Name = "pnlMiscTools"
        Me.pnlMiscTools.Size = New System.Drawing.Size(778, 86)
        Me.pnlMiscTools.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(123, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(267, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "WARNING - This tool may take several minutes to run. "
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(634, 60)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(100, 20)
        Me.txtCount.TabIndex = 3
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(589, 63)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(35, 13)
        Me.lblCount.TabIndex = 2
        Me.lblCount.Text = "Count"
        '
        'btnGetContactData
        '
        Me.btnGetContactData.AutoSize = True
        Me.btnGetContactData.Location = New System.Drawing.Point(17, 13)
        Me.btnGetContactData.Name = "btnGetContactData"
        Me.btnGetContactData.Size = New System.Drawing.Size(100, 23)
        Me.btnGetContactData.TabIndex = 0
        Me.btnGetContactData.Text = "Get Contact Data"
        Me.btnGetContactData.UseVisualStyleBackColor = True
        '
        'TCMiscTools
        '
        Me.TCMiscTools.Controls.Add(Me.TPExports)
        Me.TCMiscTools.Controls.Add(Me.TPCaseWork)
        Me.TCMiscTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMiscTools.Location = New System.Drawing.Point(0, 49)
        Me.TCMiscTools.Name = "TCMiscTools"
        Me.TCMiscTools.SelectedIndex = 0
        Me.TCMiscTools.Size = New System.Drawing.Size(792, 495)
        Me.TCMiscTools.TabIndex = 14
        '
        'TPExports
        '
        Me.TPExports.Controls.Add(Me.dgvMiscTools)
        Me.TPExports.Controls.Add(Me.pnlMiscTools)
        Me.TPExports.Location = New System.Drawing.Point(4, 22)
        Me.TPExports.Name = "TPExports"
        Me.TPExports.Padding = New System.Windows.Forms.Padding(3)
        Me.TPExports.Size = New System.Drawing.Size(784, 469)
        Me.TPExports.TabIndex = 0
        Me.TPExports.Text = "Export Reports"
        Me.TPExports.UseVisualStyleBackColor = True
        '
        'TPCaseWork
        '
        Me.TPCaseWork.Controls.Add(Me.CRVCaseWork)
        Me.TPCaseWork.Controls.Add(Me.pnlCaseWork)
        Me.TPCaseWork.Location = New System.Drawing.Point(4, 22)
        Me.TPCaseWork.Name = "TPCaseWork"
        Me.TPCaseWork.Size = New System.Drawing.Size(784, 469)
        Me.TPCaseWork.TabIndex = 1
        Me.TPCaseWork.Text = "Case Work Data"
        Me.TPCaseWork.UseVisualStyleBackColor = True
        '
        'CRVCaseWork
        '
        Me.CRVCaseWork.ActiveViewIndex = -1
        Me.CRVCaseWork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRVCaseWork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRVCaseWork.Location = New System.Drawing.Point(0, 77)
        Me.CRVCaseWork.Name = "CRVCaseWork"
        Me.CRVCaseWork.SelectionFormula = ""
        Me.CRVCaseWork.Size = New System.Drawing.Size(784, 392)
        Me.CRVCaseWork.TabIndex = 0
        Me.CRVCaseWork.ViewTimeSelectionFormula = ""
        '
        'pnlCaseWork
        '
        Me.pnlCaseWork.Controls.Add(Me.chbAllDates)
        Me.pnlCaseWork.Controls.Add(Me.Label5)
        Me.pnlCaseWork.Controls.Add(Me.Label4)
        Me.pnlCaseWork.Controls.Add(Me.DTPEndDate)
        Me.pnlCaseWork.Controls.Add(Me.DTPStartDate)
        Me.pnlCaseWork.Controls.Add(Me.btnGetCaseLog)
        Me.pnlCaseWork.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCaseWork.Location = New System.Drawing.Point(0, 0)
        Me.pnlCaseWork.Name = "pnlCaseWork"
        Me.pnlCaseWork.Size = New System.Drawing.Size(784, 77)
        Me.pnlCaseWork.TabIndex = 1
        '
        'chbAllDates
        '
        Me.chbAllDates.AutoSize = True
        Me.chbAllDates.Location = New System.Drawing.Point(146, 12)
        Me.chbAllDates.Name = "chbAllDates"
        Me.chbAllDates.Size = New System.Drawing.Size(68, 17)
        Me.chbAllDates.TabIndex = 51
        Me.chbAllDates.Text = "All Dates"
        Me.chbAllDates.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(303, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "End Date:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(141, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Start Date:"
        '
        'DTPEndDate
        '
        Me.DTPEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEndDate.Location = New System.Drawing.Point(364, 33)
        Me.DTPEndDate.Name = "DTPEndDate"
        Me.DTPEndDate.Size = New System.Drawing.Size(91, 20)
        Me.DTPEndDate.TabIndex = 48
        '
        'DTPStartDate
        '
        Me.DTPStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartDate.Location = New System.Drawing.Point(205, 33)
        Me.DTPStartDate.Name = "DTPStartDate"
        Me.DTPStartDate.Size = New System.Drawing.Size(91, 20)
        Me.DTPStartDate.TabIndex = 47
        '
        'btnGetCaseLog
        '
        Me.btnGetCaseLog.AutoSize = True
        Me.btnGetCaseLog.Location = New System.Drawing.Point(8, 6)
        Me.btnGetCaseLog.Name = "btnGetCaseLog"
        Me.btnGetCaseLog.Size = New System.Drawing.Size(102, 26)
        Me.btnGetCaseLog.TabIndex = 2
        Me.btnGetCaseLog.Text = "Get Case Log"
        Me.btnGetCaseLog.UseVisualStyleBackColor = True
        '
        'SBEAPMiscTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TCMiscTools)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "SBEAPMiscTools"
        Me.Text = "SBEAP Miscellaneous Tools"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgvMiscTools, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMiscTools.ResumeLayout(False)
        Me.pnlMiscTools.PerformLayout()
        Me.TCMiscTools.ResumeLayout(False)
        Me.TPExports.ResumeLayout(False)
        Me.TPCaseWork.ResumeLayout(False)
        Me.pnlCaseWork.ResumeLayout(False)
        Me.pnlCaseWork.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents label1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvMiscTools As System.Windows.Forms.DataGridView
    Friend WithEvents pnlMiscTools As System.Windows.Forms.Panel
    Friend WithEvents TCMiscTools As System.Windows.Forms.TabControl
    Friend WithEvents TPExports As System.Windows.Forms.TabPage
    Friend WithEvents btnGetContactData As System.Windows.Forms.Button
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents TPCaseWork As System.Windows.Forms.TabPage
    Friend WithEvents CRVCaseWork As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents pnlCaseWork As System.Windows.Forms.Panel
    Friend WithEvents btnGetCaseLog As System.Windows.Forms.Button
    Friend WithEvents chbAllDates As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTPEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
