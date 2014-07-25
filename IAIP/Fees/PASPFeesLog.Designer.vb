<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PASPFeesLog
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnExportToExcel = New System.Windows.Forms.Button
        Me.txtResultsCount = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpEndShutDown = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpStartShutDown = New System.Windows.Forms.DateTimePicker
        Me.chbShutdown = New System.Windows.Forms.CheckBox
        Me.chbOwesFees = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chbSeasonal = New System.Windows.Forms.CheckBox
        Me.chbTempClosed = New System.Windows.Forms.CheckBox
        Me.chbClosed = New System.Windows.Forms.CheckBox
        Me.chbConstruction = New System.Windows.Forms.CheckBox
        Me.chbPlanned = New System.Windows.Forms.CheckBox
        Me.chbOperating = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.mtbSearchAirsNumber = New System.Windows.Forms.MaskedTextBox
        Me.txtSearchFacilityName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtInvoice = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.clbFeeYear = New System.Windows.Forms.CheckedListBox
        Me.btnRunFilter = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPFacilitySpecific = New System.Windows.Forms.TabPage
        Me.mtbSelectedFeeYear = New System.Windows.Forms.MaskedTextBox
        Me.mtbSelectedAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.btnOpenFeeWorkTool = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSelectedFacilityName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dgvExistingYearAdmin = New System.Windows.Forms.DataGridView
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TPFacilitySpecific.SuspendLayout()
        CType(Me.dgvExistingYearAdmin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExportToExcel)
        Me.GroupBox1.Controls.Add(Me.txtResultsCount)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.dtpEndShutDown)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.dtpStartShutDown)
        Me.GroupBox1.Controls.Add(Me.chbShutdown)
        Me.GroupBox1.Controls.Add(Me.chbOwesFees)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnRunFilter)
        Me.GroupBox1.Controls.Add(Me.TabControl1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 241)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter and Sort Options"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.AutoSize = True
        Me.btnExportToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportToExcel.Location = New System.Drawing.Point(204, 19)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(88, 23)
        Me.btnExportToExcel.TabIndex = 455
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'txtResultsCount
        '
        Me.txtResultsCount.Location = New System.Drawing.Point(157, 21)
        Me.txtResultsCount.Name = "txtResultsCount"
        Me.txtResultsCount.ReadOnly = True
        Me.txtResultsCount.Size = New System.Drawing.Size(41, 20)
        Me.txtResultsCount.TabIndex = 453
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(83, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 454
        Me.Label8.Text = "Results Count"
        '
        'dtpEndShutDown
        '
        Me.dtpEndShutDown.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndShutDown.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndShutDown.Location = New System.Drawing.Point(323, 210)
        Me.dtpEndShutDown.Name = "dtpEndShutDown"
        Me.dtpEndShutDown.Size = New System.Drawing.Size(100, 20)
        Me.dtpEndShutDown.TabIndex = 452
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(292, 214)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(25, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "and"
        '
        'dtpStartShutDown
        '
        Me.dtpStartShutDown.CustomFormat = "dd-MMM-yyyy"
        Me.dtpStartShutDown.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartShutDown.Location = New System.Drawing.Point(186, 211)
        Me.dtpStartShutDown.Name = "dtpStartShutDown"
        Me.dtpStartShutDown.Size = New System.Drawing.Size(100, 20)
        Me.dtpStartShutDown.TabIndex = 451
        '
        'chbShutdown
        '
        Me.chbShutdown.AutoSize = True
        Me.chbShutdown.Location = New System.Drawing.Point(16, 213)
        Me.chbShutdown.Name = "chbShutdown"
        Me.chbShutdown.Size = New System.Drawing.Size(164, 17)
        Me.chbShutdown.TabIndex = 8
        Me.chbShutdown.Text = "Facilities Shut down between"
        Me.chbShutdown.UseVisualStyleBackColor = True
        '
        'chbOwesFees
        '
        Me.chbOwesFees.AutoSize = True
        Me.chbOwesFees.Location = New System.Drawing.Point(16, 188)
        Me.chbOwesFees.Name = "chbOwesFees"
        Me.chbOwesFees.Size = New System.Drawing.Size(182, 17)
        Me.chbOwesFees.TabIndex = 7
        Me.chbOwesFees.Text = "All Facilities w/ Outstanding Fees"
        Me.chbOwesFees.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.AutoSize = True
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.chbSeasonal)
        Me.GroupBox4.Controls.Add(Me.chbTempClosed)
        Me.GroupBox4.Controls.Add(Me.chbClosed)
        Me.GroupBox4.Controls.Add(Me.chbConstruction)
        Me.GroupBox4.Controls.Add(Me.chbPlanned)
        Me.GroupBox4.Controls.Add(Me.chbOperating)
        Me.GroupBox4.Location = New System.Drawing.Point(313, 48)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(146, 154)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Op. Status"
        '
        'chbSeasonal
        '
        Me.chbSeasonal.AutoSize = True
        Me.chbSeasonal.Location = New System.Drawing.Point(7, 118)
        Me.chbSeasonal.Name = "chbSeasonal"
        Me.chbSeasonal.Size = New System.Drawing.Size(131, 17)
        Me.chbSeasonal.TabIndex = 5
        Me.chbSeasonal.Text = "I - Seasonal Opeartion"
        Me.chbSeasonal.UseVisualStyleBackColor = True
        '
        'chbTempClosed
        '
        Me.chbTempClosed.AutoSize = True
        Me.chbTempClosed.Location = New System.Drawing.Point(7, 98)
        Me.chbTempClosed.Name = "chbTempClosed"
        Me.chbTempClosed.Size = New System.Drawing.Size(107, 17)
        Me.chbTempClosed.TabIndex = 4
        Me.chbTempClosed.Text = "T - Temp. Closed"
        Me.chbTempClosed.UseVisualStyleBackColor = True
        '
        'chbClosed
        '
        Me.chbClosed.AutoSize = True
        Me.chbClosed.Location = New System.Drawing.Point(7, 38)
        Me.chbClosed.Name = "chbClosed"
        Me.chbClosed.Size = New System.Drawing.Size(74, 17)
        Me.chbClosed.TabIndex = 3
        Me.chbClosed.Text = "X - Closed"
        Me.chbClosed.UseVisualStyleBackColor = True
        '
        'chbConstruction
        '
        Me.chbConstruction.AutoSize = True
        Me.chbConstruction.Location = New System.Drawing.Point(7, 58)
        Me.chbConstruction.Name = "chbConstruction"
        Me.chbConstruction.Size = New System.Drawing.Size(133, 17)
        Me.chbConstruction.TabIndex = 2
        Me.chbConstruction.Text = "C - Under Construction"
        Me.chbConstruction.UseVisualStyleBackColor = True
        '
        'chbPlanned
        '
        Me.chbPlanned.AutoSize = True
        Me.chbPlanned.Location = New System.Drawing.Point(7, 78)
        Me.chbPlanned.Name = "chbPlanned"
        Me.chbPlanned.Size = New System.Drawing.Size(81, 17)
        Me.chbPlanned.TabIndex = 1
        Me.chbPlanned.Text = "P - Planned"
        Me.chbPlanned.UseVisualStyleBackColor = True
        '
        'chbOperating
        '
        Me.chbOperating.AutoSize = True
        Me.chbOperating.Location = New System.Drawing.Point(7, 18)
        Me.chbOperating.Name = "chbOperating"
        Me.chbOperating.Size = New System.Drawing.Size(92, 17)
        Me.chbOperating.TabIndex = 0
        Me.chbOperating.Text = "O - Operating "
        Me.chbOperating.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.mtbSearchAirsNumber)
        Me.GroupBox3.Controls.Add(Me.txtSearchFacilityName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.txtInvoice)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Location = New System.Drawing.Point(106, 48)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(201, 115)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Text Searches"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Facility Name"
        '
        'mtbSearchAirsNumber
        '
        Me.mtbSearchAirsNumber.Location = New System.Drawing.Point(80, 19)
        Me.mtbSearchAirsNumber.Mask = "00000000"
        Me.mtbSearchAirsNumber.Name = "mtbSearchAirsNumber"
        Me.mtbSearchAirsNumber.Size = New System.Drawing.Size(63, 20)
        Me.mtbSearchAirsNumber.TabIndex = 2
        '
        'txtSearchFacilityName
        '
        Me.txtSearchFacilityName.Location = New System.Drawing.Point(80, 45)
        Me.txtSearchFacilityName.Name = "txtSearchFacilityName"
        Me.txtSearchFacilityName.Size = New System.Drawing.Size(100, 20)
        Me.txtSearchFacilityName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "AIRS #"
        '
        'txtInvoice
        '
        Me.txtInvoice.Location = New System.Drawing.Point(80, 71)
        Me.txtInvoice.Name = "txtInvoice"
        Me.txtInvoice.Size = New System.Drawing.Size(100, 20)
        Me.txtInvoice.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Invoice #"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.clbFeeYear)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 48)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(94, 100)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Fee Year"
        '
        'clbFeeYear
        '
        Me.clbFeeYear.CheckOnClick = True
        Me.clbFeeYear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFeeYear.FormattingEnabled = True
        Me.clbFeeYear.Location = New System.Drawing.Point(3, 16)
        Me.clbFeeYear.Name = "clbFeeYear"
        Me.clbFeeYear.Size = New System.Drawing.Size(88, 79)
        Me.clbFeeYear.TabIndex = 0
        '
        'btnRunFilter
        '
        Me.btnRunFilter.AutoSize = True
        Me.btnRunFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunFilter.Location = New System.Drawing.Point(6, 19)
        Me.btnRunFilter.Name = "btnRunFilter"
        Me.btnRunFilter.Size = New System.Drawing.Size(62, 23)
        Me.btnRunFilter.TabIndex = 1
        Me.btnRunFilter.Text = "Run Filter"
        Me.btnRunFilter.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPFacilitySpecific)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.TabControl1.Location = New System.Drawing.Point(539, 16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(250, 222)
        Me.TabControl1.TabIndex = 0
        '
        'TPFacilitySpecific
        '
        Me.TPFacilitySpecific.Controls.Add(Me.mtbSelectedFeeYear)
        Me.TPFacilitySpecific.Controls.Add(Me.mtbSelectedAIRSNumber)
        Me.TPFacilitySpecific.Controls.Add(Me.btnOpenFeeWorkTool)
        Me.TPFacilitySpecific.Controls.Add(Me.Label4)
        Me.TPFacilitySpecific.Controls.Add(Me.txtSelectedFacilityName)
        Me.TPFacilitySpecific.Controls.Add(Me.Label3)
        Me.TPFacilitySpecific.Controls.Add(Me.Label2)
        Me.TPFacilitySpecific.Location = New System.Drawing.Point(4, 22)
        Me.TPFacilitySpecific.Name = "TPFacilitySpecific"
        Me.TPFacilitySpecific.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFacilitySpecific.Size = New System.Drawing.Size(242, 196)
        Me.TPFacilitySpecific.TabIndex = 0
        Me.TPFacilitySpecific.Text = "Select Facility Specific"
        Me.TPFacilitySpecific.UseVisualStyleBackColor = True
        '
        'mtbSelectedFeeYear
        '
        Me.mtbSelectedFeeYear.Location = New System.Drawing.Point(9, 110)
        Me.mtbSelectedFeeYear.Mask = "0000"
        Me.mtbSelectedFeeYear.Name = "mtbSelectedFeeYear"
        Me.mtbSelectedFeeYear.Size = New System.Drawing.Size(39, 20)
        Me.mtbSelectedFeeYear.TabIndex = 10
        '
        'mtbSelectedAIRSNumber
        '
        Me.mtbSelectedAIRSNumber.Location = New System.Drawing.Point(9, 26)
        Me.mtbSelectedAIRSNumber.Mask = "000-00000"
        Me.mtbSelectedAIRSNumber.Name = "mtbSelectedAIRSNumber"
        Me.mtbSelectedAIRSNumber.Size = New System.Drawing.Size(64, 20)
        Me.mtbSelectedAIRSNumber.TabIndex = 9
        Me.mtbSelectedAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnOpenFeeWorkTool
        '
        Me.btnOpenFeeWorkTool.AutoSize = True
        Me.btnOpenFeeWorkTool.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenFeeWorkTool.Location = New System.Drawing.Point(79, 24)
        Me.btnOpenFeeWorkTool.Name = "btnOpenFeeWorkTool"
        Me.btnOpenFeeWorkTool.Size = New System.Drawing.Size(66, 23)
        Me.btnOpenFeeWorkTool.TabIndex = 8
        Me.btnOpenFeeWorkTool.Text = "View Data"
        Me.btnOpenFeeWorkTool.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Fee year"
        '
        'txtSelectedFacilityName
        '
        Me.txtSelectedFacilityName.Location = New System.Drawing.Point(9, 68)
        Me.txtSelectedFacilityName.Name = "txtSelectedFacilityName"
        Me.txtSelectedFacilityName.Size = New System.Drawing.Size(227, 20)
        Me.txtSelectedFacilityName.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Facility Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "AIRS #"
        '
        'dgvExistingYearAdmin
        '
        Me.dgvExistingYearAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExistingYearAdmin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvExistingYearAdmin.Location = New System.Drawing.Point(0, 241)
        Me.dgvExistingYearAdmin.Name = "dgvExistingYearAdmin"
        Me.dgvExistingYearAdmin.Size = New System.Drawing.Size(792, 325)
        Me.dgvExistingYearAdmin.TabIndex = 4
        '
        'PASPFeesLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.dgvExistingYearAdmin)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "PASPFeesLog"
        Me.Text = "Fees Log"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TPFacilitySpecific.ResumeLayout(False)
        Me.TPFacilitySpecific.PerformLayout()
        CType(Me.dgvExistingYearAdmin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvExistingYearAdmin As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents clbFeeYear As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPFacilitySpecific As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents mtbSearchAirsNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtSearchFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOpenFeeWorkTool As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mtbSelectedAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbSelectedFeeYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chbOperating As System.Windows.Forms.CheckBox
    Friend WithEvents chbClosed As System.Windows.Forms.CheckBox
    Friend WithEvents chbConstruction As System.Windows.Forms.CheckBox
    Friend WithEvents chbPlanned As System.Windows.Forms.CheckBox
    Friend WithEvents chbSeasonal As System.Windows.Forms.CheckBox
    Friend WithEvents chbTempClosed As System.Windows.Forms.CheckBox
    Friend WithEvents chbOwesFees As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInvoice As System.Windows.Forms.TextBox
    Friend WithEvents chbShutdown As System.Windows.Forms.CheckBox
    Friend WithEvents dtpEndShutDown As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpStartShutDown As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtResultsCount As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
End Class
