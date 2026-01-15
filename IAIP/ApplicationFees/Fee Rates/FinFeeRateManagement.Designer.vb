<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinFeeRateManagement
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.chkShowInactive = New System.Windows.Forms.CheckBox()
        Me.grpNewRate = New System.Windows.Forms.GroupBox()
        Me.btnSaveNewEffectiveRate = New System.Windows.Forms.Button()
        Me.btnCancelNewRate = New System.Windows.Forms.Button()
        Me.dtpNewEffectiveRateDate = New System.Windows.Forms.DateTimePicker()
        Me.txtNewEffectiveRate = New Iaip.CurrencyTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnEndRateItem = New System.Windows.Forms.Button()
        Me.btnUpdateRateItemName = New System.Windows.Forms.Button()
        Me.dgvRateItems = New Iaip.IaipDataGridView()
        Me.dgvRateItemHistory = New Iaip.IaipDataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRateItemName = New System.Windows.Forms.TextBox()
        Me.btnAddNewEffectiveRate = New System.Windows.Forms.Button()
        Me.grpEndRateItem = New System.Windows.Forms.GroupBox()
        Me.dtpEndRateItemDate = New System.Windows.Forms.DateTimePicker()
        Me.btnCancelEndRateItem = New System.Windows.Forms.Button()
        Me.btnSaveEndRateItem = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnAddNewRateItem = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.grpNewRate.SuspendLayout()
        CType(Me.dgvRateItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRateItemHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEndRateItem.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Category"
        '
        'cmbCategory
        '
        Me.cmbCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(71, 23)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(204, 21)
        Me.cmbCategory.TabIndex = 0
        '
        'chkShowInactive
        '
        Me.chkShowInactive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkShowInactive.AutoSize = True
        Me.chkShowInactive.Location = New System.Drawing.Point(283, 25)
        Me.chkShowInactive.Name = "chkShowInactive"
        Me.chkShowInactive.Size = New System.Drawing.Size(94, 17)
        Me.chkShowInactive.TabIndex = 1
        Me.chkShowInactive.Text = "Show Inactive"
        Me.chkShowInactive.UseVisualStyleBackColor = True
        '
        'grpNewRate
        '
        Me.grpNewRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpNewRate.Controls.Add(Me.btnSaveNewEffectiveRate)
        Me.grpNewRate.Controls.Add(Me.btnCancelNewRate)
        Me.grpNewRate.Controls.Add(Me.dtpNewEffectiveRateDate)
        Me.grpNewRate.Controls.Add(Me.txtNewEffectiveRate)
        Me.grpNewRate.Controls.Add(Me.Label5)
        Me.grpNewRate.Controls.Add(Me.Label4)
        Me.grpNewRate.Location = New System.Drawing.Point(311, 371)
        Me.grpNewRate.Name = "grpNewRate"
        Me.grpNewRate.Size = New System.Drawing.Size(218, 123)
        Me.grpNewRate.TabIndex = 7
        Me.grpNewRate.TabStop = False
        Me.grpNewRate.Text = "New Effective Rate"
        Me.grpNewRate.Visible = False
        '
        'btnSaveNewEffectiveRate
        '
        Me.btnSaveNewEffectiveRate.Location = New System.Drawing.Point(15, 80)
        Me.btnSaveNewEffectiveRate.Name = "btnSaveNewEffectiveRate"
        Me.btnSaveNewEffectiveRate.Size = New System.Drawing.Size(103, 23)
        Me.btnSaveNewEffectiveRate.TabIndex = 2
        Me.btnSaveNewEffectiveRate.Text = "Save New Rate"
        Me.btnSaveNewEffectiveRate.UseVisualStyleBackColor = True
        '
        'btnCancelNewRate
        '
        Me.btnCancelNewRate.Location = New System.Drawing.Point(124, 80)
        Me.btnCancelNewRate.Name = "btnCancelNewRate"
        Me.btnCancelNewRate.Size = New System.Drawing.Size(78, 23)
        Me.btnCancelNewRate.TabIndex = 3
        Me.btnCancelNewRate.Text = "Cancel"
        Me.btnCancelNewRate.UseVisualStyleBackColor = True
        '
        'dtpNewEffectiveRateDate
        '
        Me.dtpNewEffectiveRateDate.Checked = False
        Me.dtpNewEffectiveRateDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpNewEffectiveRateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNewEffectiveRateDate.Location = New System.Drawing.Point(111, 54)
        Me.dtpNewEffectiveRateDate.Name = "dtpNewEffectiveRateDate"
        Me.dtpNewEffectiveRateDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpNewEffectiveRateDate.TabIndex = 1
        '
        'txtNewEffectiveRate
        '
        Me.txtNewEffectiveRate.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtNewEffectiveRate.Cue = "$ 0"
        Me.txtNewEffectiveRate.Location = New System.Drawing.Point(111, 28)
        Me.txtNewEffectiveRate.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNewEffectiveRate.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNewEffectiveRate.Name = "txtNewEffectiveRate"
        Me.txtNewEffectiveRate.Size = New System.Drawing.Size(91, 20)
        Me.txtNewEffectiveRate.TabIndex = 0
        Me.txtNewEffectiveRate.Text = "$0"
        Me.txtNewEffectiveRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Rate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Effective Date"
        '
        'btnEndRateItem
        '
        Me.btnEndRateItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEndRateItem.Location = New System.Drawing.Point(423, 368)
        Me.btnEndRateItem.Name = "btnEndRateItem"
        Me.btnEndRateItem.Size = New System.Drawing.Size(106, 42)
        Me.btnEndRateItem.TabIndex = 6
        Me.btnEndRateItem.Text = "End Use of " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Rate Item"
        Me.btnEndRateItem.UseVisualStyleBackColor = True
        '
        'btnUpdateRateItemName
        '
        Me.btnUpdateRateItemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateRateItemName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateRateItemName.Location = New System.Drawing.Point(414, 274)
        Me.btnUpdateRateItemName.Name = "btnUpdateRateItemName"
        Me.btnUpdateRateItemName.Size = New System.Drawing.Size(115, 23)
        Me.btnUpdateRateItemName.TabIndex = 4
        Me.btnUpdateRateItemName.Text = "Update Description"
        Me.btnUpdateRateItemName.UseVisualStyleBackColor = True
        '
        'dgvRateItems
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRateItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRateItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRateItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRateItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRateItems.LinkifyColumnByName = Nothing
        Me.dgvRateItems.LinkifyFirstColumn = True
        Me.dgvRateItems.Location = New System.Drawing.Point(12, 52)
        Me.dgvRateItems.Name = "dgvRateItems"
        Me.dgvRateItems.ResultsCountLabel = Nothing
        Me.dgvRateItems.ResultsCountLabelFormat = "{0} found"
        Me.dgvRateItems.ShowEditingIcon = False
        Me.dgvRateItems.Size = New System.Drawing.Size(517, 203)
        Me.dgvRateItems.StandardTab = True
        Me.dgvRateItems.TabIndex = 2
        '
        'dgvRateItemHistory
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRateItemHistory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvRateItemHistory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRateItemHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRateItemHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRateItemHistory.LinkifyColumnByName = Nothing
        Me.dgvRateItemHistory.Location = New System.Drawing.Point(12, 368)
        Me.dgvRateItemHistory.Name = "dgvRateItemHistory"
        Me.dgvRateItemHistory.ResultsCountLabel = Nothing
        Me.dgvRateItemHistory.ResultsCountLabelFormat = "{0} found"
        Me.dgvRateItemHistory.ShowEditingIcon = False
        Me.dgvRateItemHistory.Size = New System.Drawing.Size(284, 126)
        Me.dgvRateItemHistory.StandardTab = True
        Me.dgvRateItemHistory.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 279)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Rate Item"
        '
        'txtRateItemName
        '
        Me.txtRateItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRateItemName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtRateItemName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtRateItemName.Location = New System.Drawing.Point(71, 276)
        Me.txtRateItemName.Name = "txtRateItemName"
        Me.txtRateItemName.Size = New System.Drawing.Size(327, 20)
        Me.txtRateItemName.TabIndex = 3
        '
        'btnAddNewEffectiveRate
        '
        Me.btnAddNewEffectiveRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewEffectiveRate.Location = New System.Drawing.Point(311, 368)
        Me.btnAddNewEffectiveRate.Name = "btnAddNewEffectiveRate"
        Me.btnAddNewEffectiveRate.Size = New System.Drawing.Size(106, 42)
        Me.btnAddNewEffectiveRate.TabIndex = 5
        Me.btnAddNewEffectiveRate.Text = "Add New " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Effective Rate"
        Me.btnAddNewEffectiveRate.UseVisualStyleBackColor = True
        '
        'grpEndRateItem
        '
        Me.grpEndRateItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEndRateItem.Controls.Add(Me.dtpEndRateItemDate)
        Me.grpEndRateItem.Controls.Add(Me.btnCancelEndRateItem)
        Me.grpEndRateItem.Controls.Add(Me.btnSaveEndRateItem)
        Me.grpEndRateItem.Controls.Add(Me.Label7)
        Me.grpEndRateItem.Location = New System.Drawing.Point(311, 371)
        Me.grpEndRateItem.Name = "grpEndRateItem"
        Me.grpEndRateItem.Size = New System.Drawing.Size(218, 123)
        Me.grpEndRateItem.TabIndex = 8
        Me.grpEndRateItem.TabStop = False
        Me.grpEndRateItem.Text = "End Rate Item"
        Me.grpEndRateItem.Visible = False
        '
        'dtpEndRateItemDate
        '
        Me.dtpEndRateItemDate.Checked = False
        Me.dtpEndRateItemDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndRateItemDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndRateItemDate.Location = New System.Drawing.Point(111, 28)
        Me.dtpEndRateItemDate.Name = "dtpEndRateItemDate"
        Me.dtpEndRateItemDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpEndRateItemDate.TabIndex = 0
        '
        'btnCancelEndRateItem
        '
        Me.btnCancelEndRateItem.Location = New System.Drawing.Point(124, 80)
        Me.btnCancelEndRateItem.Name = "btnCancelEndRateItem"
        Me.btnCancelEndRateItem.Size = New System.Drawing.Size(78, 23)
        Me.btnCancelEndRateItem.TabIndex = 2
        Me.btnCancelEndRateItem.Text = "Cancel"
        Me.btnCancelEndRateItem.UseVisualStyleBackColor = True
        '
        'btnSaveEndRateItem
        '
        Me.btnSaveEndRateItem.Location = New System.Drawing.Point(15, 80)
        Me.btnSaveEndRateItem.Name = "btnSaveEndRateItem"
        Me.btnSaveEndRateItem.Size = New System.Drawing.Size(103, 23)
        Me.btnSaveEndRateItem.TabIndex = 1
        Me.btnSaveEndRateItem.Text = "Save End Date"
        Me.btnSaveEndRateItem.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "End Date"
        '
        'btnAddNewRateItem
        '
        Me.btnAddNewRateItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewRateItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewRateItem.Location = New System.Drawing.Point(391, 21)
        Me.btnAddNewRateItem.Name = "btnAddNewRateItem"
        Me.btnAddNewRateItem.Size = New System.Drawing.Size(138, 23)
        Me.btnAddNewRateItem.TabIndex = 9
        Me.btnAddNewRateItem.Text = "→ Add New Rate Item"
        Me.btnAddNewRateItem.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 352)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Rate Item History"
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(12, 310)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.lblMessage.Size = New System.Drawing.Size(182, 32)
        Me.lblMessage.TabIndex = 12
        Me.lblMessage.Text = "Label6" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Warning Warning Warning Warning"
        '
        'FinFeeRateManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(541, 506)
        Me.Controls.Add(Me.dgvRateItemHistory)
        Me.Controls.Add(Me.chkShowInactive)
        Me.Controls.Add(Me.cmbCategory)
        Me.Controls.Add(Me.txtRateItemName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAddNewRateItem)
        Me.Controls.Add(Me.btnUpdateRateItemName)
        Me.Controls.Add(Me.btnAddNewEffectiveRate)
        Me.Controls.Add(Me.btnEndRateItem)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.grpEndRateItem)
        Me.Controls.Add(Me.dgvRateItems)
        Me.Controls.Add(Me.grpNewRate)
        Me.MinimumSize = New System.Drawing.Size(550, 430)
        Me.Name = "FinFeeRateManagement"
        Me.Text = "Fee Rate Management"
        Me.grpNewRate.ResumeLayout(False)
        Me.grpNewRate.PerformLayout()
        CType(Me.dgvRateItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRateItemHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEndRateItem.ResumeLayout(False)
        Me.grpEndRateItem.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents chkShowInactive As CheckBox
    Friend WithEvents grpNewRate As GroupBox
    Friend WithEvents btnUpdateRateItemName As Button
    Friend WithEvents txtRateItemName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dgvRateItems As IaipDataGridView
    Friend WithEvents dgvRateItemHistory As IaipDataGridView
    Friend WithEvents txtNewEffectiveRate As CurrencyTextBox
    Friend WithEvents btnAddNewEffectiveRate As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpNewEffectiveRateDate As DateTimePicker
    Friend WithEvents btnCancelNewRate As Button
    Friend WithEvents btnSaveNewEffectiveRate As Button
    Friend WithEvents btnEndRateItem As Button
    Friend WithEvents grpEndRateItem As GroupBox
    Friend WithEvents dtpEndRateItemDate As DateTimePicker
    Friend WithEvents btnCancelEndRateItem As Button
    Friend WithEvents btnSaveEndRateItem As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents btnAddNewRateItem As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents lblMessage As Label
End Class
