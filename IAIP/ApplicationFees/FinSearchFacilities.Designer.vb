<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinSearchFacilities
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblResultsCount = New System.Windows.Forms.Label()
        Me.grpFacility = New System.Windows.Forms.GroupBox()
        Me.txtAirsNumberSearch = New Iaip.AirsNumberEntryForm()
        Me.lblAirsSearchMessage = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnOpenSelectedItem = New System.Windows.Forms.Button()
        Me.dgvSearchResults = New Iaip.IaipDataGridView()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblSelectedAirsMessage = New System.Windows.Forms.Label()
        Me.txtSelectedItem = New Iaip.AirsNumberEntryForm()
        Me.btnAccounts = New System.Windows.Forms.GroupBox()
        Me.chkUnusedCredits = New System.Windows.Forms.CheckBox()
        Me.chkOpenInvoices = New System.Windows.Forms.CheckBox()
        Me.chkPendingItems = New System.Windows.Forms.CheckBox()
        Me.grpFacility.SuspendLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        Me.btnAccounts.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblResultsCount
        '
        Me.lblResultsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblResultsCount.AutoSize = True
        Me.lblResultsCount.Location = New System.Drawing.Point(279, 149)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(73, 13)
        Me.lblResultsCount.TabIndex = 454
        Me.lblResultsCount.Text = "Results Count"
        '
        'grpFacility
        '
        Me.grpFacility.Controls.Add(Me.txtAirsNumberSearch)
        Me.grpFacility.Controls.Add(Me.lblAirsSearchMessage)
        Me.grpFacility.Controls.Add(Me.Label7)
        Me.grpFacility.Controls.Add(Me.txtFacilityName)
        Me.grpFacility.Controls.Add(Me.Label1)
        Me.grpFacility.Location = New System.Drawing.Point(12, 13)
        Me.grpFacility.Name = "grpFacility"
        Me.grpFacility.Size = New System.Drawing.Size(210, 110)
        Me.grpFacility.TabIndex = 0
        Me.grpFacility.TabStop = False
        Me.grpFacility.Text = "Facility Search"
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
        'lblAirsSearchMessage
        '
        Me.lblAirsSearchMessage.AutoSize = True
        Me.lblAirsSearchMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAirsSearchMessage.Location = New System.Drawing.Point(82, 42)
        Me.lblAirsSearchMessage.Name = "lblAirsSearchMessage"
        Me.lblAirsSearchMessage.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.lblAirsSearchMessage.Size = New System.Drawing.Size(60, 13)
        Me.lblAirsSearchMessage.TabIndex = 457
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
        'txtFacilityName
        '
        Me.txtFacilityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtFacilityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
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
        Me.btnSearch.Location = New System.Drawing.Point(21, 142)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnSearch.Size = New System.Drawing.Size(149, 27)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search Facilities"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnOpenSelectedItem
        '
        Me.btnOpenSelectedItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenSelectedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenSelectedItem.Location = New System.Drawing.Point(576, 28)
        Me.btnOpenSelectedItem.Name = "btnOpenSelectedItem"
        Me.btnOpenSelectedItem.Size = New System.Drawing.Size(104, 27)
        Me.btnOpenSelectedItem.TabIndex = 5
        Me.btnOpenSelectedItem.Text = "Open Facility"
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
        Me.dgvSearchResults.Location = New System.Drawing.Point(0, 184)
        Me.dgvSearchResults.Name = "dgvSearchResults"
        Me.dgvSearchResults.ResultsCountLabel = Me.lblResultsCount
        Me.dgvSearchResults.ResultsCountLabelFormat = "{0} found"
        Me.dgvSearchResults.ShowEditingIcon = False
        Me.dgvSearchResults.Size = New System.Drawing.Size(692, 316)
        Me.dgvSearchResults.StandardTab = True
        Me.dgvSearchResults.TabIndex = 1
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.btnClear)
        Me.TopPanel.Controls.Add(Me.lblSelectedAirsMessage)
        Me.TopPanel.Controls.Add(Me.txtSelectedItem)
        Me.TopPanel.Controls.Add(Me.btnAccounts)
        Me.TopPanel.Controls.Add(Me.grpFacility)
        Me.TopPanel.Controls.Add(Me.lblResultsCount)
        Me.TopPanel.Controls.Add(Me.btnOpenSelectedItem)
        Me.TopPanel.Controls.Add(Me.btnSearch)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.TopPanel.Size = New System.Drawing.Size(692, 184)
        Me.TopPanel.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.AutoSize = True
        Me.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClear.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.btnClear.Location = New System.Drawing.Point(176, 144)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnClear.Size = New System.Drawing.Size(97, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear Search"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lblSelectedAirsMessage
        '
        Me.lblSelectedAirsMessage.AutoSize = True
        Me.lblSelectedAirsMessage.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblSelectedAirsMessage.Location = New System.Drawing.Point(465, 58)
        Me.lblSelectedAirsMessage.Name = "lblSelectedAirsMessage"
        Me.lblSelectedAirsMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblSelectedAirsMessage.Size = New System.Drawing.Size(60, 19)
        Me.lblSelectedAirsMessage.TabIndex = 456
        Me.lblSelectedAirsMessage.Text = "Error label"
        '
        'txtSelectedItem
        '
        Me.txtSelectedItem.AirsNumber = Nothing
        Me.txtSelectedItem.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.txtSelectedItem.BackColor = System.Drawing.Color.Transparent
        Me.txtSelectedItem.ErrorMessageLabel = Me.lblSelectedAirsMessage
        Me.txtSelectedItem.FacilityMustExist = True
        Me.txtSelectedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectedItem.Location = New System.Drawing.Point(460, 30)
        Me.txtSelectedItem.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSelectedItem.Name = "txtSelectedItem"
        Me.txtSelectedItem.ReadOnly = False
        Me.txtSelectedItem.Size = New System.Drawing.Size(109, 25)
        Me.txtSelectedItem.TabIndex = 4
        Me.txtSelectedItem.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'btnAccounts
        '
        Me.btnAccounts.Controls.Add(Me.chkUnusedCredits)
        Me.btnAccounts.Controls.Add(Me.chkOpenInvoices)
        Me.btnAccounts.Controls.Add(Me.chkPendingItems)
        Me.btnAccounts.Location = New System.Drawing.Point(235, 12)
        Me.btnAccounts.Name = "btnAccounts"
        Me.btnAccounts.Size = New System.Drawing.Size(210, 111)
        Me.btnAccounts.TabIndex = 1
        Me.btnAccounts.TabStop = False
        Me.btnAccounts.Text = "Account Search"
        '
        'chkUnusedCredits
        '
        Me.chkUnusedCredits.AutoSize = True
        Me.chkUnusedCredits.Location = New System.Drawing.Point(9, 48)
        Me.chkUnusedCredits.Name = "chkUnusedCredits"
        Me.chkUnusedCredits.Size = New System.Drawing.Size(160, 17)
        Me.chkUnusedCredits.TabIndex = 1
        Me.chkUnusedCredits.Text = "Facilities with unused credits"
        Me.chkUnusedCredits.UseVisualStyleBackColor = True
        '
        'chkOpenInvoices
        '
        Me.chkOpenInvoices.AutoSize = True
        Me.chkOpenInvoices.Location = New System.Drawing.Point(9, 22)
        Me.chkOpenInvoices.Name = "chkOpenInvoices"
        Me.chkOpenInvoices.Size = New System.Drawing.Size(165, 17)
        Me.chkOpenInvoices.TabIndex = 0
        Me.chkOpenInvoices.Text = "Facilities with unpaid invoices"
        Me.chkOpenInvoices.UseVisualStyleBackColor = True
        '
        'chkPendingItems
        '
        Me.chkPendingItems.AutoSize = True
        Me.chkPendingItems.Location = New System.Drawing.Point(9, 68)
        Me.chkPendingItems.Name = "chkPendingItems"
        Me.chkPendingItems.Size = New System.Drawing.Size(159, 30)
        Me.chkPendingItems.TabIndex = 2
        Me.chkPendingItems.Text = "Facilities with pending items " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "not invoiced"
        Me.chkPendingItems.UseVisualStyleBackColor = True
        '
        'FinSearchFacilities
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 500)
        Me.Controls.Add(Me.dgvSearchResults)
        Me.Controls.Add(Me.TopPanel)
        Me.MinimumSize = New System.Drawing.Size(708, 422)
        Me.Name = "FinSearchFacilities"
        Me.Text = "Application Fee Facility Search"
        Me.grpFacility.ResumeLayout(False)
        Me.grpFacility.PerformLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.btnAccounts.ResumeLayout(False)
        Me.btnAccounts.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvSearchResults As IaipDataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents grpFacility As System.Windows.Forms.GroupBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOpenSelectedItem As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents TopPanel As Panel
    Friend WithEvents txtSelectedItem As AirsNumberEntryForm
    Friend WithEvents btnAccounts As GroupBox
    Friend WithEvents lblSelectedAirsMessage As Label
    Friend WithEvents lblAirsSearchMessage As Label
    Friend WithEvents txtAirsNumberSearch As AirsNumberEntryForm
    Friend WithEvents chkPendingItems As CheckBox
    Friend WithEvents chkOpenInvoices As CheckBox
    Friend WithEvents chkUnusedCredits As CheckBox
    Friend WithEvents btnClear As Button
End Class
