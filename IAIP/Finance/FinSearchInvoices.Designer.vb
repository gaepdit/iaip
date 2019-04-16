<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinSearchInvoices
    Inherits BaseForm

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblResultsCount = New System.Windows.Forms.Label()
        Me.dtpDateEnd = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateStart = New System.Windows.Forms.DateTimePicker()
        Me.grpFacility = New System.Windows.Forms.GroupBox()
        Me.lblAirsSearchMessage = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAirsNumberSearch = New Iaip.AirNumberEntryForm()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnOpenSelectedItem = New System.Windows.Forms.Button()
        Me.dgvSearchResults = New Iaip.IaipDataGridView()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblSelectedIdMessage = New System.Windows.Forms.Label()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.chkOnlyOpenInvoices = New System.Windows.Forms.CheckBox()
        Me.chkIncludeVoided = New System.Windows.Forms.CheckBox()
        Me.grpCategory = New System.Windows.Forms.GroupBox()
        Me.rdbCategoryApplicationFees = New System.Windows.Forms.RadioButton()
        Me.rdbCategoryEmissionFees = New System.Windows.Forms.RadioButton()
        Me.rdbCategoryAll = New System.Windows.Forms.RadioButton()
        Me.grpDates = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSelectedItem = New Iaip.CueTextBox()
        Me.grpFacility.SuspendLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        Me.grpStatus.SuspendLayout()
        Me.grpCategory.SuspendLayout()
        Me.grpDates.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblResultsCount
        '
        Me.lblResultsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblResultsCount.AutoSize = True
        Me.lblResultsCount.Location = New System.Drawing.Point(279, 231)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(73, 13)
        Me.lblResultsCount.TabIndex = 454
        Me.lblResultsCount.Text = "Results Count"
        '
        'dtpDateEnd
        '
        Me.dtpDateEnd.Checked = False
        Me.dtpDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateEnd.Location = New System.Drawing.Point(67, 45)
        Me.dtpDateEnd.Name = "dtpDateEnd"
        Me.dtpDateEnd.ShowCheckBox = True
        Me.dtpDateEnd.Size = New System.Drawing.Size(118, 20)
        Me.dtpDateEnd.TabIndex = 1
        '
        'dtpDateStart
        '
        Me.dtpDateStart.Checked = False
        Me.dtpDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateStart.Location = New System.Drawing.Point(67, 19)
        Me.dtpDateStart.Name = "dtpDateStart"
        Me.dtpDateStart.ShowCheckBox = True
        Me.dtpDateStart.Size = New System.Drawing.Size(118, 20)
        Me.dtpDateStart.TabIndex = 0
        '
        'grpFacility
        '
        Me.grpFacility.Controls.Add(Me.lblAirsSearchMessage)
        Me.grpFacility.Controls.Add(Me.Label7)
        Me.grpFacility.Controls.Add(Me.txtAirsNumberSearch)
        Me.grpFacility.Controls.Add(Me.txtFacilityName)
        Me.grpFacility.Controls.Add(Me.Label1)
        Me.grpFacility.Location = New System.Drawing.Point(12, 13)
        Me.grpFacility.Name = "grpFacility"
        Me.grpFacility.Size = New System.Drawing.Size(210, 97)
        Me.grpFacility.TabIndex = 0
        Me.grpFacility.TabStop = False
        Me.grpFacility.Text = "Facility Search"
        '
        'lblAirsSearchMessage
        '
        Me.lblAirsSearchMessage.AutoSize = True
        Me.lblAirsSearchMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAirsSearchMessage.Location = New System.Drawing.Point(82, 42)
        Me.lblAirsSearchMessage.Name = "lblAirsSearchMessage"
        Me.lblAirsSearchMessage.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.lblAirsSearchMessage.Size = New System.Drawing.Size(60, 13)
        Me.lblAirsSearchMessage.TabIndex = 456
        Me.lblAirsSearchMessage.Text = "Error label"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Facility Name"
        '
        'txtAirsNumberSearch
        '
        Me.txtAirsNumberSearch.AirsNumber = Nothing
        Me.txtAirsNumberSearch.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.txtAirsNumberSearch.BackColor = System.Drawing.Color.Transparent
        Me.txtAirsNumberSearch.ErrorMessageLabel = Me.lblAirsSearchMessage
        Me.txtAirsNumberSearch.FacilityMustExist = True
        Me.txtAirsNumberSearch.InvalidFormatMessage = "Invalid AIRS #."
        Me.txtAirsNumberSearch.Location = New System.Drawing.Point(82, 19)
        Me.txtAirsNumberSearch.Name = "txtAirsNumberSearch"
        Me.txtAirsNumberSearch.ReadOnly = False
        Me.txtAirsNumberSearch.Size = New System.Drawing.Size(103, 20)
        Me.txtAirsNumberSearch.TabIndex = 0
        Me.txtAirsNumberSearch.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(82, 58)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.Size = New System.Drawing.Size(103, 20)
        Me.txtFacilityName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "AIRS #"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.Iaip.My.Resources.Resources.SearchIcon
        Me.btnSearch.Location = New System.Drawing.Point(21, 224)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnSearch.Size = New System.Drawing.Size(149, 27)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search Invoices"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnOpenSelectedItem
        '
        Me.btnOpenSelectedItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenSelectedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenSelectedItem.Location = New System.Drawing.Point(549, 28)
        Me.btnOpenSelectedItem.Name = "btnOpenSelectedItem"
        Me.btnOpenSelectedItem.Size = New System.Drawing.Size(105, 27)
        Me.btnOpenSelectedItem.TabIndex = 7
        Me.btnOpenSelectedItem.Text = "Open Invoice"
        Me.btnOpenSelectedItem.UseVisualStyleBackColor = True
        '
        'dgvSearchResults
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        Me.dgvSearchResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSearchResults.LinkifyColumnByName = Nothing
        Me.dgvSearchResults.LinkifyFirstColumn = True
        Me.dgvSearchResults.Location = New System.Drawing.Point(0, 265)
        Me.dgvSearchResults.Name = "dgvSearchResults"
        Me.dgvSearchResults.ResultsCountLabel = Me.lblResultsCount
        Me.dgvSearchResults.ResultsCountLabelFormat = "{0} found"
        Me.dgvSearchResults.Size = New System.Drawing.Size(668, 224)
        Me.dgvSearchResults.StandardTab = True
        Me.dgvSearchResults.TabIndex = 1
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.btnClear)
        Me.TopPanel.Controls.Add(Me.lblSelectedIdMessage)
        Me.TopPanel.Controls.Add(Me.grpStatus)
        Me.TopPanel.Controls.Add(Me.grpCategory)
        Me.TopPanel.Controls.Add(Me.grpDates)
        Me.TopPanel.Controls.Add(Me.grpFacility)
        Me.TopPanel.Controls.Add(Me.lblResultsCount)
        Me.TopPanel.Controls.Add(Me.txtSelectedItem)
        Me.TopPanel.Controls.Add(Me.btnOpenSelectedItem)
        Me.TopPanel.Controls.Add(Me.btnSearch)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.TopPanel.Size = New System.Drawing.Size(668, 265)
        Me.TopPanel.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.AutoSize = True
        Me.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClear.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.btnClear.Location = New System.Drawing.Point(176, 226)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnClear.Size = New System.Drawing.Size(97, 23)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear Search"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lblSelectedIdMessage
        '
        Me.lblSelectedIdMessage.AutoSize = True
        Me.lblSelectedIdMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblSelectedIdMessage.Location = New System.Drawing.Point(465, 58)
        Me.lblSelectedIdMessage.Name = "lblSelectedIdMessage"
        Me.lblSelectedIdMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblSelectedIdMessage.Size = New System.Drawing.Size(60, 19)
        Me.lblSelectedIdMessage.TabIndex = 455
        Me.lblSelectedIdMessage.Text = "Error label"
        '
        'grpStatus
        '
        Me.grpStatus.Controls.Add(Me.chkOnlyOpenInvoices)
        Me.grpStatus.Controls.Add(Me.chkIncludeVoided)
        Me.grpStatus.Location = New System.Drawing.Point(235, 13)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Size = New System.Drawing.Size(210, 97)
        Me.grpStatus.TabIndex = 2
        Me.grpStatus.TabStop = False
        Me.grpStatus.Text = "Status Search"
        '
        'chkOnlyOpenInvoices
        '
        Me.chkOnlyOpenInvoices.AutoSize = True
        Me.chkOnlyOpenInvoices.Location = New System.Drawing.Point(13, 21)
        Me.chkOnlyOpenInvoices.Name = "chkOnlyOpenInvoices"
        Me.chkOnlyOpenInvoices.Size = New System.Drawing.Size(116, 17)
        Me.chkOnlyOpenInvoices.TabIndex = 0
        Me.chkOnlyOpenInvoices.Text = "Only open invoices"
        Me.chkOnlyOpenInvoices.UseVisualStyleBackColor = True
        '
        'chkIncludeVoided
        '
        Me.chkIncludeVoided.AutoSize = True
        Me.chkIncludeVoided.Location = New System.Drawing.Point(13, 60)
        Me.chkIncludeVoided.Name = "chkIncludeVoided"
        Me.chkIncludeVoided.Size = New System.Drawing.Size(138, 17)
        Me.chkIncludeVoided.TabIndex = 1
        Me.chkIncludeVoided.Text = "Include voided invoices"
        Me.chkIncludeVoided.UseVisualStyleBackColor = True
        '
        'grpCategory
        '
        Me.grpCategory.Controls.Add(Me.rdbCategoryApplicationFees)
        Me.grpCategory.Controls.Add(Me.rdbCategoryEmissionFees)
        Me.grpCategory.Controls.Add(Me.rdbCategoryAll)
        Me.grpCategory.Enabled = False
        Me.grpCategory.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.grpCategory.Location = New System.Drawing.Point(235, 116)
        Me.grpCategory.Name = "grpCategory"
        Me.grpCategory.Size = New System.Drawing.Size(210, 97)
        Me.grpCategory.TabIndex = 3
        Me.grpCategory.TabStop = False
        Me.grpCategory.Text = "Category Search (disabled pending Emissions Fees implementation)"
        Me.grpCategory.Visible = False
        '
        'rdbCategoryApplicationFees
        '
        Me.rdbCategoryApplicationFees.AutoSize = True
        Me.rdbCategoryApplicationFees.Location = New System.Drawing.Point(13, 66)
        Me.rdbCategoryApplicationFees.Name = "rdbCategoryApplicationFees"
        Me.rdbCategoryApplicationFees.Size = New System.Drawing.Size(135, 17)
        Me.rdbCategoryApplicationFees.TabIndex = 2
        Me.rdbCategoryApplicationFees.Text = "Permit Application Fees"
        Me.rdbCategoryApplicationFees.UseVisualStyleBackColor = True
        '
        'rdbCategoryEmissionFees
        '
        Me.rdbCategoryEmissionFees.AutoSize = True
        Me.rdbCategoryEmissionFees.Location = New System.Drawing.Point(13, 43)
        Me.rdbCategoryEmissionFees.Name = "rdbCategoryEmissionFees"
        Me.rdbCategoryEmissionFees.Size = New System.Drawing.Size(97, 17)
        Me.rdbCategoryEmissionFees.TabIndex = 1
        Me.rdbCategoryEmissionFees.Text = "Emissions Fees"
        Me.rdbCategoryEmissionFees.UseVisualStyleBackColor = True
        '
        'rdbCategoryAll
        '
        Me.rdbCategoryAll.AutoSize = True
        Me.rdbCategoryAll.Checked = True
        Me.rdbCategoryAll.Location = New System.Drawing.Point(13, 20)
        Me.rdbCategoryAll.Name = "rdbCategoryAll"
        Me.rdbCategoryAll.Size = New System.Drawing.Size(43, 17)
        Me.rdbCategoryAll.TabIndex = 0
        Me.rdbCategoryAll.TabStop = True
        Me.rdbCategoryAll.Text = "Any"
        Me.rdbCategoryAll.UseVisualStyleBackColor = True
        '
        'grpDates
        '
        Me.grpDates.Controls.Add(Me.Label3)
        Me.grpDates.Controls.Add(Me.Label2)
        Me.grpDates.Controls.Add(Me.dtpDateStart)
        Me.grpDates.Controls.Add(Me.dtpDateEnd)
        Me.grpDates.Location = New System.Drawing.Point(12, 116)
        Me.grpDates.Name = "grpDates"
        Me.grpDates.Size = New System.Drawing.Size(210, 79)
        Me.grpDates.TabIndex = 1
        Me.grpDates.TabStop = False
        Me.grpDates.Text = "Date Range Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "End Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Start Date"
        '
        'txtSelectedItem
        '
        Me.txtSelectedItem.Cue = "Invoice ID"
        Me.txtSelectedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectedItem.Location = New System.Drawing.Point(460, 30)
        Me.txtSelectedItem.Name = "txtSelectedItem"
        Me.txtSelectedItem.Size = New System.Drawing.Size(83, 23)
        Me.txtSelectedItem.TabIndex = 6
        '
        'FinSearchInvoices
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(668, 489)
        Me.Controls.Add(Me.dgvSearchResults)
        Me.Controls.Add(Me.TopPanel)
        Me.MinimumSize = New System.Drawing.Size(684, 422)
        Me.Name = "FinSearchInvoices"
        Me.Text = "Application Fee Invoice Search"
        Me.grpFacility.ResumeLayout(False)
        Me.grpFacility.PerformLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.grpStatus.ResumeLayout(False)
        Me.grpStatus.PerformLayout()
        Me.grpCategory.ResumeLayout(False)
        Me.grpCategory.PerformLayout()
        Me.grpDates.ResumeLayout(False)
        Me.grpDates.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvSearchResults As IaipDataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents grpFacility As System.Windows.Forms.GroupBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOpenSelectedItem As System.Windows.Forms.Button
    Friend WithEvents dtpDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents TopPanel As Panel
    Friend WithEvents txtAirsNumberSearch As AirNumberEntryForm
    Friend WithEvents txtSelectedItem As CueTextBox
    Friend WithEvents grpDates As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents grpCategory As GroupBox
    Friend WithEvents rdbCategoryApplicationFees As RadioButton
    Friend WithEvents rdbCategoryEmissionFees As RadioButton
    Friend WithEvents rdbCategoryAll As RadioButton
    Friend WithEvents grpStatus As GroupBox
    Friend WithEvents chkIncludeVoided As CheckBox
    Friend WithEvents lblSelectedIdMessage As Label
    Friend WithEvents lblAirsSearchMessage As Label
    Friend WithEvents chkOnlyOpenInvoices As CheckBox
    Friend WithEvents btnClear As Button
End Class
