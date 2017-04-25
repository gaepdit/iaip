<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPCaseLog
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiOpenNewCase = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiExportToExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.SCCaseLog = New System.Windows.Forms.SplitContainer()
        Me.dgvCaseLog = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbAllCases = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdbClosedCase = New System.Windows.Forms.RadioButton()
        Me.btnResetSearch = New System.Windows.Forms.Button()
        Me.rdbOpenCases = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnOpenCase = New System.Windows.Forms.Button()
        Me.txtCaseID = New System.Windows.Forms.TextBox()
        Me.btnSearchCaseLog = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboSortOrder2 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboSortType2 = New System.Windows.Forms.ComboBox()
        Me.cboSortOrder1 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboSortType1 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DTPSearchDate3 = New System.Windows.Forms.DateTimePicker()
        Me.DTPSearchDate4 = New System.Windows.Forms.DateTimePicker()
        Me.cboSearchText2 = New System.Windows.Forms.ComboBox()
        Me.txtSearchText2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DTPSearchDate1 = New System.Windows.Forms.DateTimePicker()
        Me.DTPSearchDate2 = New System.Windows.Forms.DateTimePicker()
        Me.cboSearchText1 = New System.Windows.Forms.ComboBox()
        Me.txtSearchText1 = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.cboFieldType2 = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cboFieldType1 = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.bgw1 = New System.ComponentModel.BackgroundWorker()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SCCaseLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCCaseLog.Panel1.SuspendLayout()
        Me.SCCaseLog.Panel2.SuspendLayout()
        Me.SCCaseLog.SuspendLayout()
        CType(Me.dgvCaseLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiTools})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mmiTools
        '
        Me.mmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiOpenNewCase, Me.mmiExportToExcel})
        Me.mmiTools.Name = "mmiTools"
        Me.mmiTools.Size = New System.Drawing.Size(48, 20)
        Me.mmiTools.Text = "&Tools"
        '
        'mmiOpenNewCase
        '
        Me.mmiOpenNewCase.Name = "mmiOpenNewCase"
        Me.mmiOpenNewCase.Size = New System.Drawing.Size(158, 22)
        Me.mmiOpenNewCase.Text = "Open &New Case"
        '
        'mmiExportToExcel
        '
        Me.mmiExportToExcel.Name = "mmiExportToExcel"
        Me.mmiExportToExcel.Size = New System.Drawing.Size(158, 22)
        Me.mmiExportToExcel.Text = "&Export to Excel"
        '
        'SCCaseLog
        '
        Me.SCCaseLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCCaseLog.Location = New System.Drawing.Point(0, 24)
        Me.SCCaseLog.Name = "SCCaseLog"
        Me.SCCaseLog.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCCaseLog.Panel1
        '
        Me.SCCaseLog.Panel1.Controls.Add(Me.dgvCaseLog)
        '
        'SCCaseLog.Panel2
        '
        Me.SCCaseLog.Panel2.Controls.Add(Me.GroupBox1)
        Me.SCCaseLog.Size = New System.Drawing.Size(792, 542)
        Me.SCCaseLog.SplitterDistance = 339
        Me.SCCaseLog.SplitterWidth = 10
        Me.SCCaseLog.TabIndex = 9
        '
        'dgvCaseLog
        '
        Me.dgvCaseLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCaseLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCaseLog.Location = New System.Drawing.Point(0, 0)
        Me.dgvCaseLog.Name = "dgvCaseLog"
        Me.dgvCaseLog.Size = New System.Drawing.Size(792, 339)
        Me.dgvCaseLog.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbAllCases)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.rdbClosedCase)
        Me.GroupBox1.Controls.Add(Me.btnResetSearch)
        Me.GroupBox1.Controls.Add(Me.rdbOpenCases)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.btnOpenCase)
        Me.GroupBox1.Controls.Add(Me.txtCaseID)
        Me.GroupBox1.Controls.Add(Me.btnSearchCaseLog)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cboSortOrder2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cboSortType2)
        Me.GroupBox1.Controls.Add(Me.cboSortOrder1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cboSortType1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.DTPSearchDate3)
        Me.GroupBox1.Controls.Add(Me.DTPSearchDate4)
        Me.GroupBox1.Controls.Add(Me.cboSearchText2)
        Me.GroupBox1.Controls.Add(Me.txtSearchText2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.DTPSearchDate1)
        Me.GroupBox1.Controls.Add(Me.DTPSearchDate2)
        Me.GroupBox1.Controls.Add(Me.cboSearchText1)
        Me.GroupBox1.Controls.Add(Me.txtSearchText1)
        Me.GroupBox1.Controls.Add(Me.Label39)
        Me.GroupBox1.Controls.Add(Me.cboFieldType2)
        Me.GroupBox1.Controls.Add(Me.Label29)
        Me.GroupBox1.Controls.Add(Me.cboFieldType1)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 193)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Specify Multiple Search Criteria, Select Sort Order and Click Find to Search"
        '
        'rdbAllCases
        '
        Me.rdbAllCases.AutoSize = True
        Me.rdbAllCases.Location = New System.Drawing.Point(353, 129)
        Me.rdbAllCases.Name = "rdbAllCases"
        Me.rdbAllCases.Size = New System.Drawing.Size(68, 17)
        Me.rdbAllCases.TabIndex = 2
        Me.rdbAllCases.TabStop = True
        Me.rdbAllCases.Text = "All Cases"
        Me.rdbAllCases.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Location = New System.Drawing.Point(460, 94)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(104, 57)
        Me.Panel1.TabIndex = 26
        '
        'rdbClosedCase
        '
        Me.rdbClosedCase.AutoSize = True
        Me.rdbClosedCase.Location = New System.Drawing.Point(353, 112)
        Me.rdbClosedCase.Name = "rdbClosedCase"
        Me.rdbClosedCase.Size = New System.Drawing.Size(89, 17)
        Me.rdbClosedCase.TabIndex = 1
        Me.rdbClosedCase.TabStop = True
        Me.rdbClosedCase.Text = "Closed Cases"
        Me.rdbClosedCase.UseVisualStyleBackColor = True
        '
        'btnResetSearch
        '
        Me.btnResetSearch.Location = New System.Drawing.Point(705, 50)
        Me.btnResetSearch.Name = "btnResetSearch"
        Me.btnResetSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnResetSearch.TabIndex = 14
        Me.btnResetSearch.Text = "Reset Form"
        Me.btnResetSearch.UseVisualStyleBackColor = True
        '
        'rdbOpenCases
        '
        Me.rdbOpenCases.AutoSize = True
        Me.rdbOpenCases.Checked = True
        Me.rdbOpenCases.Location = New System.Drawing.Point(353, 94)
        Me.rdbOpenCases.Name = "rdbOpenCases"
        Me.rdbOpenCases.Size = New System.Drawing.Size(83, 17)
        Me.rdbOpenCases.TabIndex = 0
        Me.rdbOpenCases.TabStop = True
        Me.rdbOpenCases.Text = "Open Cases"
        Me.rdbOpenCases.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(596, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Case ID"
        '
        'btnOpenCase
        '
        Me.btnOpenCase.Location = New System.Drawing.Point(705, 101)
        Me.btnOpenCase.Name = "btnOpenCase"
        Me.btnOpenCase.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenCase.TabIndex = 12
        Me.btnOpenCase.Text = "Open Case Work"
        Me.btnOpenCase.UseVisualStyleBackColor = True
        '
        'txtCaseID
        '
        Me.txtCaseID.Location = New System.Drawing.Point(599, 103)
        Me.txtCaseID.Name = "txtCaseID"
        Me.txtCaseID.Size = New System.Drawing.Size(100, 20)
        Me.txtCaseID.TabIndex = 11
        '
        'btnSearchCaseLog
        '
        Me.btnSearchCaseLog.Location = New System.Drawing.Point(705, 17)
        Me.btnSearchCaseLog.Name = "btnSearchCaseLog"
        Me.btnSearchCaseLog.Size = New System.Drawing.Size(75, 23)
        Me.btnSearchCaseLog.TabIndex = 13
        Me.btnSearchCaseLog.Text = "Search"
        Me.btnSearchCaseLog.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(658, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Search"
        '
        'cboSortOrder2
        '
        Me.cboSortOrder2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSortOrder2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSortOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortOrder2.FormattingEnabled = True
        Me.cboSortOrder2.Location = New System.Drawing.Point(183, 130)
        Me.cboSortOrder2.Name = "cboSortOrder2"
        Me.cboSortOrder2.Size = New System.Drawing.Size(126, 21)
        Me.cboSortOrder2.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(19, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "(2)"
        '
        'cboSortType2
        '
        Me.cboSortType2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSortType2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSortType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortType2.FormattingEnabled = True
        Me.cboSortType2.Location = New System.Drawing.Point(51, 130)
        Me.cboSortType2.Name = "cboSortType2"
        Me.cboSortType2.Size = New System.Drawing.Size(126, 21)
        Me.cboSortType2.TabIndex = 9
        '
        'cboSortOrder1
        '
        Me.cboSortOrder1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSortOrder1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSortOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortOrder1.FormattingEnabled = True
        Me.cboSortOrder1.Location = New System.Drawing.Point(183, 103)
        Me.cboSortOrder1.Name = "cboSortOrder1"
        Me.cboSortOrder1.Size = New System.Drawing.Size(126, 21)
        Me.cboSortOrder1.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(28, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "(1)"
        '
        'cboSortType1
        '
        Me.cboSortType1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSortType1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSortType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortType1.FormattingEnabled = True
        Me.cboSortType1.Location = New System.Drawing.Point(51, 103)
        Me.cboSortType1.Name = "cboSortType1"
        Me.cboSortType1.Size = New System.Drawing.Size(126, 21)
        Me.cboSortType1.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Sort By"
        '
        'DTPSearchDate3
        '
        Me.DTPSearchDate3.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate3.Location = New System.Drawing.Point(379, 46)
        Me.DTPSearchDate3.Name = "DTPSearchDate3"
        Me.DTPSearchDate3.Size = New System.Drawing.Size(94, 20)
        Me.DTPSearchDate3.TabIndex = 6
        '
        'DTPSearchDate4
        '
        Me.DTPSearchDate4.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate4.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate4.Location = New System.Drawing.Point(479, 46)
        Me.DTPSearchDate4.Name = "DTPSearchDate4"
        Me.DTPSearchDate4.Size = New System.Drawing.Size(94, 20)
        Me.DTPSearchDate4.TabIndex = 5
        '
        'cboSearchText2
        '
        Me.cboSearchText2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchText2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchText2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchText2.FormattingEnabled = True
        Me.cboSearchText2.Location = New System.Drawing.Point(379, 46)
        Me.cboSearchText2.Name = "cboSearchText2"
        Me.cboSearchText2.Size = New System.Drawing.Size(194, 21)
        Me.cboSearchText2.TabIndex = 4
        '
        'txtSearchText2
        '
        Me.txtSearchText2.Location = New System.Drawing.Point(379, 46)
        Me.txtSearchText2.Name = "txtSearchText2"
        Me.txtSearchText2.Size = New System.Drawing.Size(194, 20)
        Me.txtSearchText2.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(308, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Search Text"
        '
        'DTPSearchDate1
        '
        Me.DTPSearchDate1.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate1.Location = New System.Drawing.Point(77, 46)
        Me.DTPSearchDate1.Name = "DTPSearchDate1"
        Me.DTPSearchDate1.Size = New System.Drawing.Size(94, 20)
        Me.DTPSearchDate1.TabIndex = 1
        '
        'DTPSearchDate2
        '
        Me.DTPSearchDate2.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSearchDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSearchDate2.Location = New System.Drawing.Point(177, 46)
        Me.DTPSearchDate2.Name = "DTPSearchDate2"
        Me.DTPSearchDate2.Size = New System.Drawing.Size(94, 20)
        Me.DTPSearchDate2.TabIndex = 2
        '
        'cboSearchText1
        '
        Me.cboSearchText1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboSearchText1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSearchText1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchText1.FormattingEnabled = True
        Me.cboSearchText1.Location = New System.Drawing.Point(77, 46)
        Me.cboSearchText1.Name = "cboSearchText1"
        Me.cboSearchText1.Size = New System.Drawing.Size(194, 21)
        Me.cboSearchText1.Sorted = True
        Me.cboSearchText1.TabIndex = 6
        '
        'txtSearchText1
        '
        Me.txtSearchText1.Location = New System.Drawing.Point(77, 46)
        Me.txtSearchText1.Name = "txtSearchText1"
        Me.txtSearchText1.Size = New System.Drawing.Size(194, 20)
        Me.txtSearchText1.TabIndex = 5
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(6, 50)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(65, 13)
        Me.Label39.TabIndex = 4
        Me.Label39.Text = "Search Text"
        '
        'cboFieldType2
        '
        Me.cboFieldType2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboFieldType2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFieldType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFieldType2.FormattingEnabled = True
        Me.cboFieldType2.Location = New System.Drawing.Point(379, 19)
        Me.cboFieldType2.Name = "cboFieldType2"
        Me.cboFieldType2.Size = New System.Drawing.Size(194, 21)
        Me.cboFieldType2.TabIndex = 3
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(306, 23)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(72, 13)
        Me.Label29.TabIndex = 2
        Me.Label29.Text = "Field Type #2"
        '
        'cboFieldType1
        '
        Me.cboFieldType1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboFieldType1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFieldType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFieldType1.FormattingEnabled = True
        Me.cboFieldType1.Location = New System.Drawing.Point(77, 19)
        Me.cboFieldType1.Name = "cboFieldType1"
        Me.cboFieldType1.Size = New System.Drawing.Size(194, 21)
        Me.cboFieldType1.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(4, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Field Type #1"
        '
        'bgw1
        '
        '
        'SBEAPCaseLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.SCCaseLog)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "SBEAPCaseLog"
        Me.Text = "SBEAP Case Log"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SCCaseLog.Panel1.ResumeLayout(False)
        Me.SCCaseLog.Panel2.ResumeLayout(False)
        CType(Me.SCCaseLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCCaseLog.ResumeLayout(False)
        CType(Me.dgvCaseLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SCCaseLog As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvCaseLog As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents cboFieldType2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cboFieldType1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cboSearchText1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchText1 As System.Windows.Forms.TextBox
    Friend WithEvents DTPSearchDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPSearchDate2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPSearchDate3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPSearchDate4 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboSearchText2 As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchText2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboSortOrder2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboSortType2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSortOrder1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboSortType1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearchCaseLog As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnOpenCase As System.Windows.Forms.Button
    Friend WithEvents txtCaseID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnResetSearch As System.Windows.Forms.Button
    Friend WithEvents mmiOpenNewCase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bgw1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdbAllCases As System.Windows.Forms.RadioButton
    Friend WithEvents rdbClosedCase As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOpenCases As System.Windows.Forms.RadioButton
    Friend WithEvents mmiExportToExcel As ToolStripMenuItem
End Class
