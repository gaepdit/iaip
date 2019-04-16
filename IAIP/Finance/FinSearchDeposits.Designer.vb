<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinSearchDeposits
    Inherits BaseForm

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dtpDateEnd = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateStart = New System.Windows.Forms.DateTimePicker()
        Me.btnOpenSelectedItem = New System.Windows.Forms.Button()
        Me.dgvSearchResults = New Iaip.IaipDataGridView()
        Me.lblResultsCount = New System.Windows.Forms.Label()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblSelectedIdMessage = New System.Windows.Forms.Label()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.chkIncludeDeleted = New System.Windows.Forms.CheckBox()
        Me.chkUnusedBalance = New System.Windows.Forms.CheckBox()
        Me.grpDepositDetails = New System.Windows.Forms.GroupBox()
        Me.txtAirsNumberSearch = New Iaip.AirNumberEntryForm()
        Me.lblAirsSearchMessage = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDepositNo = New System.Windows.Forms.TextBox()
        Me.txtCreditConf = New System.Windows.Forms.TextBox()
        Me.txtBatch = New System.Windows.Forms.TextBox()
        Me.txtCheckNo = New System.Windows.Forms.TextBox()
        Me.grpDates = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSelectedItem = New Iaip.CueTextBox()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        Me.grpStatus.SuspendLayout()
        Me.grpDepositDetails.SuspendLayout()
        Me.grpDates.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpDateEnd
        '
        Me.dtpDateEnd.Checked = False
        Me.dtpDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateEnd.Location = New System.Drawing.Point(72, 46)
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
        Me.dtpDateStart.Location = New System.Drawing.Point(72, 19)
        Me.dtpDateStart.Name = "dtpDateStart"
        Me.dtpDateStart.ShowCheckBox = True
        Me.dtpDateStart.Size = New System.Drawing.Size(118, 20)
        Me.dtpDateStart.TabIndex = 0
        '
        'btnOpenSelectedItem
        '
        Me.btnOpenSelectedItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenSelectedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenSelectedItem.Location = New System.Drawing.Point(549, 28)
        Me.btnOpenSelectedItem.Name = "btnOpenSelectedItem"
        Me.btnOpenSelectedItem.Size = New System.Drawing.Size(132, 27)
        Me.btnOpenSelectedItem.TabIndex = 5
        Me.btnOpenSelectedItem.Text = "Open Deposit"
        Me.btnOpenSelectedItem.UseVisualStyleBackColor = True
        '
        'dgvSearchResults
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvSearchResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSearchResults.LinkifyColumnByName = Nothing
        Me.dgvSearchResults.LinkifyFirstColumn = True
        Me.dgvSearchResults.Location = New System.Drawing.Point(0, 255)
        Me.dgvSearchResults.Name = "dgvSearchResults"
        Me.dgvSearchResults.ResultsCountLabel = Me.lblResultsCount
        Me.dgvSearchResults.ResultsCountLabelFormat = "{0} found"
        Me.dgvSearchResults.ShowEditingIcon = False
        Me.dgvSearchResults.Size = New System.Drawing.Size(694, 258)
        Me.dgvSearchResults.StandardTab = True
        Me.dgvSearchResults.TabIndex = 1
        '
        'lblResultsCount
        '
        Me.lblResultsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblResultsCount.AutoSize = True
        Me.lblResultsCount.Location = New System.Drawing.Point(279, 219)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(73, 13)
        Me.lblResultsCount.TabIndex = 455
        Me.lblResultsCount.Text = "Results Count"
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.btnSearch)
        Me.TopPanel.Controls.Add(Me.lblSelectedIdMessage)
        Me.TopPanel.Controls.Add(Me.grpStatus)
        Me.TopPanel.Controls.Add(Me.lblResultsCount)
        Me.TopPanel.Controls.Add(Me.grpDepositDetails)
        Me.TopPanel.Controls.Add(Me.grpDates)
        Me.TopPanel.Controls.Add(Me.btnClear)
        Me.TopPanel.Controls.Add(Me.txtSelectedItem)
        Me.TopPanel.Controls.Add(Me.btnOpenSelectedItem)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.TopPanel.Size = New System.Drawing.Size(694, 255)
        Me.TopPanel.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.Iaip.My.Resources.Resources.SearchIcon
        Me.btnSearch.Location = New System.Drawing.Point(21, 212)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnSearch.Size = New System.Drawing.Size(149, 27)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search Deposits"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblSelectedIdMessage
        '
        Me.lblSelectedIdMessage.AutoSize = True
        Me.lblSelectedIdMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblSelectedIdMessage.Location = New System.Drawing.Point(465, 58)
        Me.lblSelectedIdMessage.Name = "lblSelectedIdMessage"
        Me.lblSelectedIdMessage.Size = New System.Drawing.Size(54, 13)
        Me.lblSelectedIdMessage.TabIndex = 457
        Me.lblSelectedIdMessage.Text = "Error label"
        '
        'grpStatus
        '
        Me.grpStatus.Controls.Add(Me.chkIncludeDeleted)
        Me.grpStatus.Controls.Add(Me.chkUnusedBalance)
        Me.grpStatus.Location = New System.Drawing.Point(235, 13)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Size = New System.Drawing.Size(210, 77)
        Me.grpStatus.TabIndex = 1
        Me.grpStatus.TabStop = False
        Me.grpStatus.Text = "Status Search"
        '
        'chkIncludeDeleted
        '
        Me.chkIncludeDeleted.AutoSize = True
        Me.chkIncludeDeleted.Location = New System.Drawing.Point(13, 47)
        Me.chkIncludeDeleted.Name = "chkIncludeDeleted"
        Me.chkIncludeDeleted.Size = New System.Drawing.Size(159, 17)
        Me.chkIncludeDeleted.TabIndex = 1
        Me.chkIncludeDeleted.Text = "Include deleted transactions"
        Me.chkIncludeDeleted.UseVisualStyleBackColor = True
        '
        'chkUnusedBalance
        '
        Me.chkUnusedBalance.AutoSize = True
        Me.chkUnusedBalance.Location = New System.Drawing.Point(13, 21)
        Me.chkUnusedBalance.Name = "chkUnusedBalance"
        Me.chkUnusedBalance.Size = New System.Drawing.Size(148, 17)
        Me.chkUnusedBalance.TabIndex = 0
        Me.chkUnusedBalance.Text = "Only with unused balance"
        Me.chkUnusedBalance.UseVisualStyleBackColor = True
        '
        'grpDepositDetails
        '
        Me.grpDepositDetails.Controls.Add(Me.txtAirsNumberSearch)
        Me.grpDepositDetails.Controls.Add(Me.lblAirsSearchMessage)
        Me.grpDepositDetails.Controls.Add(Me.Label5)
        Me.grpDepositDetails.Controls.Add(Me.Label1)
        Me.grpDepositDetails.Controls.Add(Me.Label7)
        Me.grpDepositDetails.Controls.Add(Me.Label6)
        Me.grpDepositDetails.Controls.Add(Me.Label4)
        Me.grpDepositDetails.Controls.Add(Me.txtDepositNo)
        Me.grpDepositDetails.Controls.Add(Me.txtCreditConf)
        Me.grpDepositDetails.Controls.Add(Me.txtBatch)
        Me.grpDepositDetails.Controls.Add(Me.txtCheckNo)
        Me.grpDepositDetails.Location = New System.Drawing.Point(12, 13)
        Me.grpDepositDetails.Name = "grpDepositDetails"
        Me.grpDepositDetails.Size = New System.Drawing.Size(210, 167)
        Me.grpDepositDetails.TabIndex = 0
        Me.grpDepositDetails.TabStop = False
        Me.grpDepositDetails.Text = "Deposit Details Search"
        '
        'txtAirsNumberSearch
        '
        Me.txtAirsNumberSearch.AirsNumber = Nothing
        Me.txtAirsNumberSearch.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.txtAirsNumberSearch.BackColor = System.Drawing.Color.Transparent
        Me.txtAirsNumberSearch.ErrorMessageLabel = Me.lblAirsSearchMessage
        Me.txtAirsNumberSearch.FacilityMustExist = True
        Me.txtAirsNumberSearch.InvalidFormatMessage = "Invalid AIRS #."
        Me.txtAirsNumberSearch.Location = New System.Drawing.Point(87, 123)
        Me.txtAirsNumberSearch.Name = "txtAirsNumberSearch"
        Me.txtAirsNumberSearch.ReadOnly = False
        Me.txtAirsNumberSearch.Size = New System.Drawing.Size(103, 20)
        Me.txtAirsNumberSearch.TabIndex = 4
        Me.txtAirsNumberSearch.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'lblAirsSearchMessage
        '
        Me.lblAirsSearchMessage.AutoSize = True
        Me.lblAirsSearchMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAirsSearchMessage.Location = New System.Drawing.Point(87, 146)
        Me.lblAirsSearchMessage.Name = "lblAirsSearchMessage"
        Me.lblAirsSearchMessage.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.lblAirsSearchMessage.Size = New System.Drawing.Size(60, 13)
        Me.lblAirsSearchMessage.TabIndex = 457
        Me.lblAirsSearchMessage.Text = "Error label"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Batch #"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 126)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "AIRS #"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Credit Crd Conf"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Check #"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Deposit #"
        '
        'txtDepositNo
        '
        Me.txtDepositNo.Location = New System.Drawing.Point(87, 19)
        Me.txtDepositNo.Name = "txtDepositNo"
        Me.txtDepositNo.Size = New System.Drawing.Size(103, 20)
        Me.txtDepositNo.TabIndex = 0
        '
        'txtCreditConf
        '
        Me.txtCreditConf.Location = New System.Drawing.Point(87, 97)
        Me.txtCreditConf.Name = "txtCreditConf"
        Me.txtCreditConf.Size = New System.Drawing.Size(103, 20)
        Me.txtCreditConf.TabIndex = 3
        '
        'txtBatch
        '
        Me.txtBatch.Location = New System.Drawing.Point(87, 45)
        Me.txtBatch.Name = "txtBatch"
        Me.txtBatch.Size = New System.Drawing.Size(103, 20)
        Me.txtBatch.TabIndex = 1
        '
        'txtCheckNo
        '
        Me.txtCheckNo.Location = New System.Drawing.Point(87, 71)
        Me.txtCheckNo.Name = "txtCheckNo"
        Me.txtCheckNo.Size = New System.Drawing.Size(103, 20)
        Me.txtCheckNo.TabIndex = 2
        '
        'grpDates
        '
        Me.grpDates.Controls.Add(Me.Label3)
        Me.grpDates.Controls.Add(Me.Label2)
        Me.grpDates.Controls.Add(Me.dtpDateStart)
        Me.grpDates.Controls.Add(Me.dtpDateEnd)
        Me.grpDates.Location = New System.Drawing.Point(235, 96)
        Me.grpDates.Name = "grpDates"
        Me.grpDates.Size = New System.Drawing.Size(210, 80)
        Me.grpDates.TabIndex = 2
        Me.grpDates.TabStop = False
        Me.grpDates.Text = "Date Range Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 50)
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
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.AutoSize = True
        Me.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClear.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.btnClear.Location = New System.Drawing.Point(176, 214)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnClear.Size = New System.Drawing.Size(97, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear Search"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtSelectedItem
        '
        Me.txtSelectedItem.Cue = "Deposit ID"
        Me.txtSelectedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectedItem.Location = New System.Drawing.Point(460, 30)
        Me.txtSelectedItem.Name = "txtSelectedItem"
        Me.txtSelectedItem.Size = New System.Drawing.Size(83, 23)
        Me.txtSelectedItem.TabIndex = 4
        '
        'FinSearchDeposits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 513)
        Me.Controls.Add(Me.dgvSearchResults)
        Me.Controls.Add(Me.TopPanel)
        Me.MinimumSize = New System.Drawing.Size(710, 422)
        Me.Name = "FinSearchDeposits"
        Me.Text = "Application Fee Deposit Search"
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.grpStatus.ResumeLayout(False)
        Me.grpStatus.PerformLayout()
        Me.grpDepositDetails.ResumeLayout(False)
        Me.grpDepositDetails.PerformLayout()
        Me.grpDates.ResumeLayout(False)
        Me.grpDates.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvSearchResults As IaipDataGridView
    Friend WithEvents btnOpenSelectedItem As System.Windows.Forms.Button
    Friend WithEvents dtpDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents TopPanel As Panel
    Friend WithEvents txtSelectedItem As CueTextBox
    Friend WithEvents grpDates As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents grpDepositDetails As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCreditConf As TextBox
    Friend WithEvents txtBatch As TextBox
    Friend WithEvents txtCheckNo As TextBox
    Friend WithEvents txtDepositNo As TextBox
    Friend WithEvents lblResultsCount As Label
    Friend WithEvents chkUnusedBalance As CheckBox
    Friend WithEvents grpStatus As GroupBox
    Friend WithEvents lblSelectedIdMessage As Label
    Friend WithEvents chkIncludeDeleted As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblAirsSearchMessage As Label
    Friend WithEvents txtAirsNumberSearch As AirNumberEntryForm
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnClear As Button
End Class
