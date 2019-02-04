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
        Me.dtpNewRateDate = New System.Windows.Forms.DateTimePicker()
        Me.txtNewRate = New Iaip.CurrencyTextBox()
        Me.btnCancelNewRate = New System.Windows.Forms.Button()
        Me.btnSaveNewRate = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSaveRateItemName = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvRateItems = New Iaip.IaipDataGridView()
        Me.dgvRateItemHistory = New Iaip.IaipDataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRateItemName = New System.Windows.Forms.TextBox()
        Me.btnNewEffectiveRate = New System.Windows.Forms.Button()
        Me.btnEndRateItem = New System.Windows.Forms.Button()
        Me.grpEndRateItem = New System.Windows.Forms.GroupBox()
        Me.dtpEndRateItemDate = New System.Windows.Forms.DateTimePicker()
        Me.btnCancelEndRateItem = New System.Windows.Forms.Button()
        Me.btnSaveEndRateItem = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnEditEffectiveRate = New System.Windows.Forms.Button()
        Me.grpEditRate = New System.Windows.Forms.GroupBox()
        Me.dtpEditRateDate = New System.Windows.Forms.DateTimePicker()
        Me.txtEditRate = New Iaip.CurrencyTextBox()
        Me.btnCancelEditRate = New System.Windows.Forms.Button()
        Me.btnSaveEditRate = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnAddNewRateItem = New System.Windows.Forms.Button()
        Me.grpNewRate.SuspendLayout()
        CType(Me.dgvRateItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRateItemHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEndRateItem.SuspendLayout()
        Me.grpEditRate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Category"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(71, 25)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(230, 21)
        Me.cmbCategory.TabIndex = 0
        '
        'chkShowInactive
        '
        Me.chkShowInactive.AutoSize = True
        Me.chkShowInactive.Location = New System.Drawing.Point(307, 27)
        Me.chkShowInactive.Name = "chkShowInactive"
        Me.chkShowInactive.Size = New System.Drawing.Size(94, 17)
        Me.chkShowInactive.TabIndex = 1
        Me.chkShowInactive.Text = "Show Inactive"
        Me.chkShowInactive.UseVisualStyleBackColor = True
        '
        'grpNewRate
        '
        Me.grpNewRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpNewRate.Controls.Add(Me.dtpNewRateDate)
        Me.grpNewRate.Controls.Add(Me.txtNewRate)
        Me.grpNewRate.Controls.Add(Me.btnCancelNewRate)
        Me.grpNewRate.Controls.Add(Me.btnSaveNewRate)
        Me.grpNewRate.Controls.Add(Me.Label5)
        Me.grpNewRate.Controls.Add(Me.Label4)
        Me.grpNewRate.Location = New System.Drawing.Point(337, 335)
        Me.grpNewRate.Name = "grpNewRate"
        Me.grpNewRate.Size = New System.Drawing.Size(219, 138)
        Me.grpNewRate.TabIndex = 9
        Me.grpNewRate.TabStop = False
        Me.grpNewRate.Text = "New Effective Rate"
        '
        'dtpNewRateDate
        '
        Me.dtpNewRateDate.Checked = False
        Me.dtpNewRateDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpNewRateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNewRateDate.Location = New System.Drawing.Point(111, 54)
        Me.dtpNewRateDate.Name = "dtpNewRateDate"
        Me.dtpNewRateDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpNewRateDate.TabIndex = 1
        '
        'txtNewRate
        '
        Me.txtNewRate.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtNewRate.Cue = "$ 0"
        Me.txtNewRate.Location = New System.Drawing.Point(111, 28)
        Me.txtNewRate.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNewRate.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNewRate.Name = "txtNewRate"
        Me.txtNewRate.Size = New System.Drawing.Size(91, 20)
        Me.txtNewRate.TabIndex = 0
        Me.txtNewRate.Text = "$0"
        Me.txtNewRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCancelNewRate
        '
        Me.btnCancelNewRate.Location = New System.Drawing.Point(127, 80)
        Me.btnCancelNewRate.Name = "btnCancelNewRate"
        Me.btnCancelNewRate.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelNewRate.TabIndex = 3
        Me.btnCancelNewRate.Text = "Cancel"
        Me.btnCancelNewRate.UseVisualStyleBackColor = True
        '
        'btnSaveNewRate
        '
        Me.btnSaveNewRate.Location = New System.Drawing.Point(18, 80)
        Me.btnSaveNewRate.Name = "btnSaveNewRate"
        Me.btnSaveNewRate.Size = New System.Drawing.Size(103, 23)
        Me.btnSaveNewRate.TabIndex = 2
        Me.btnSaveNewRate.Text = "Save New Rate"
        Me.btnSaveNewRate.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Rate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Effective Date"
        '
        'btnSaveRateItemName
        '
        Me.btnSaveRateItemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveRateItemName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveRateItemName.Location = New System.Drawing.Point(461, 268)
        Me.btnSaveRateItemName.Name = "btnSaveRateItemName"
        Me.btnSaveRateItemName.Size = New System.Drawing.Size(95, 23)
        Me.btnSaveRateItemName.TabIndex = 4
        Me.btnSaveRateItemName.Text = "Update Name"
        Me.btnSaveRateItemName.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 319)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Rate History"
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
        Me.dgvRateItems.Location = New System.Drawing.Point(12, 52)
        Me.dgvRateItems.Name = "dgvRateItems"
        Me.dgvRateItems.ResultsCountLabel = Nothing
        Me.dgvRateItems.ResultsCountLabelFormat = "{0} found"
        Me.dgvRateItems.ShowEditingIcon = False
        Me.dgvRateItems.Size = New System.Drawing.Size(544, 186)
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
        Me.dgvRateItemHistory.Location = New System.Drawing.Point(12, 335)
        Me.dgvRateItemHistory.Name = "dgvRateItemHistory"
        Me.dgvRateItemHistory.ResultsCountLabel = Nothing
        Me.dgvRateItemHistory.ResultsCountLabelFormat = "{0} found"
        Me.dgvRateItemHistory.ShowEditingIcon = False
        Me.dgvRateItemHistory.Size = New System.Drawing.Size(199, 138)
        Me.dgvRateItemHistory.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 273)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Rate Item"
        '
        'txtRateItemName
        '
        Me.txtRateItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRateItemName.Location = New System.Drawing.Point(71, 270)
        Me.txtRateItemName.Name = "txtRateItemName"
        Me.txtRateItemName.Size = New System.Drawing.Size(384, 20)
        Me.txtRateItemName.TabIndex = 3
        '
        'btnNewEffectiveRate
        '
        Me.btnNewEffectiveRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewEffectiveRate.Location = New System.Drawing.Point(223, 335)
        Me.btnNewEffectiveRate.Name = "btnNewEffectiveRate"
        Me.btnNewEffectiveRate.Size = New System.Drawing.Size(95, 42)
        Me.btnNewEffectiveRate.TabIndex = 6
        Me.btnNewEffectiveRate.Text = "Add New " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Effective Rate"
        Me.btnNewEffectiveRate.UseVisualStyleBackColor = True
        '
        'btnEndRateItem
        '
        Me.btnEndRateItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEndRateItem.Location = New System.Drawing.Point(223, 383)
        Me.btnEndRateItem.Name = "btnEndRateItem"
        Me.btnEndRateItem.Size = New System.Drawing.Size(95, 42)
        Me.btnEndRateItem.TabIndex = 7
        Me.btnEndRateItem.Text = "End Use of " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Rate Item"
        Me.btnEndRateItem.UseVisualStyleBackColor = True
        '
        'grpEndRateItem
        '
        Me.grpEndRateItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEndRateItem.Controls.Add(Me.dtpEndRateItemDate)
        Me.grpEndRateItem.Controls.Add(Me.btnCancelEndRateItem)
        Me.grpEndRateItem.Controls.Add(Me.btnSaveEndRateItem)
        Me.grpEndRateItem.Controls.Add(Me.Label7)
        Me.grpEndRateItem.Location = New System.Drawing.Point(337, 335)
        Me.grpEndRateItem.Name = "grpEndRateItem"
        Me.grpEndRateItem.Size = New System.Drawing.Size(219, 138)
        Me.grpEndRateItem.TabIndex = 10
        Me.grpEndRateItem.TabStop = False
        Me.grpEndRateItem.Text = "End Rate Item"
        '
        'dtpEndRateItemDate
        '
        Me.dtpEndRateItemDate.Checked = False
        Me.dtpEndRateItemDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndRateItemDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndRateItemDate.Location = New System.Drawing.Point(111, 29)
        Me.dtpEndRateItemDate.Name = "dtpEndRateItemDate"
        Me.dtpEndRateItemDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpEndRateItemDate.TabIndex = 0
        '
        'btnCancelEndRateItem
        '
        Me.btnCancelEndRateItem.Location = New System.Drawing.Point(127, 55)
        Me.btnCancelEndRateItem.Name = "btnCancelEndRateItem"
        Me.btnCancelEndRateItem.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelEndRateItem.TabIndex = 2
        Me.btnCancelEndRateItem.Text = "Cancel"
        Me.btnCancelEndRateItem.UseVisualStyleBackColor = True
        '
        'btnSaveEndRateItem
        '
        Me.btnSaveEndRateItem.Location = New System.Drawing.Point(18, 55)
        Me.btnSaveEndRateItem.Name = "btnSaveEndRateItem"
        Me.btnSaveEndRateItem.Size = New System.Drawing.Size(103, 23)
        Me.btnSaveEndRateItem.TabIndex = 1
        Me.btnSaveEndRateItem.Text = "Save End Date"
        Me.btnSaveEndRateItem.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "End Date"
        '
        'btnEditEffectiveRate
        '
        Me.btnEditEffectiveRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditEffectiveRate.Location = New System.Drawing.Point(223, 431)
        Me.btnEditEffectiveRate.Name = "btnEditEffectiveRate"
        Me.btnEditEffectiveRate.Size = New System.Drawing.Size(95, 42)
        Me.btnEditEffectiveRate.TabIndex = 8
        Me.btnEditEffectiveRate.Text = "Edit Selected " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Effective Rate"
        Me.btnEditEffectiveRate.UseVisualStyleBackColor = True
        '
        'grpEditRate
        '
        Me.grpEditRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEditRate.Controls.Add(Me.dtpEditRateDate)
        Me.grpEditRate.Controls.Add(Me.txtEditRate)
        Me.grpEditRate.Controls.Add(Me.btnCancelEditRate)
        Me.grpEditRate.Controls.Add(Me.btnSaveEditRate)
        Me.grpEditRate.Controls.Add(Me.Label6)
        Me.grpEditRate.Controls.Add(Me.Label8)
        Me.grpEditRate.Location = New System.Drawing.Point(337, 335)
        Me.grpEditRate.Name = "grpEditRate"
        Me.grpEditRate.Size = New System.Drawing.Size(219, 138)
        Me.grpEditRate.TabIndex = 11
        Me.grpEditRate.TabStop = False
        Me.grpEditRate.Text = "Edit Effective Rate"
        '
        'dtpEditRateDate
        '
        Me.dtpEditRateDate.Checked = False
        Me.dtpEditRateDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEditRateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEditRateDate.Location = New System.Drawing.Point(111, 54)
        Me.dtpEditRateDate.Name = "dtpEditRateDate"
        Me.dtpEditRateDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpEditRateDate.TabIndex = 1
        '
        'txtEditRate
        '
        Me.txtEditRate.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtEditRate.Cue = "$ 0"
        Me.txtEditRate.Location = New System.Drawing.Point(111, 28)
        Me.txtEditRate.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtEditRate.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtEditRate.Name = "txtEditRate"
        Me.txtEditRate.Size = New System.Drawing.Size(91, 20)
        Me.txtEditRate.TabIndex = 0
        Me.txtEditRate.Text = "$0"
        Me.txtEditRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCancelEditRate
        '
        Me.btnCancelEditRate.Location = New System.Drawing.Point(127, 80)
        Me.btnCancelEditRate.Name = "btnCancelEditRate"
        Me.btnCancelEditRate.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelEditRate.TabIndex = 3
        Me.btnCancelEditRate.Text = "Cancel"
        Me.btnCancelEditRate.UseVisualStyleBackColor = True
        '
        'btnSaveEditRate
        '
        Me.btnSaveEditRate.Location = New System.Drawing.Point(18, 80)
        Me.btnSaveEditRate.Name = "btnSaveEditRate"
        Me.btnSaveEditRate.Size = New System.Drawing.Size(103, 23)
        Me.btnSaveEditRate.TabIndex = 2
        Me.btnSaveEditRate.Text = "Update Rate"
        Me.btnSaveEditRate.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Rate"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 57)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Effective Date"
        '
        'btnAddNewRateItem
        '
        Me.btnAddNewRateItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewRateItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewRateItem.Location = New System.Drawing.Point(441, 23)
        Me.btnAddNewRateItem.Name = "btnAddNewRateItem"
        Me.btnAddNewRateItem.Size = New System.Drawing.Size(115, 23)
        Me.btnAddNewRateItem.TabIndex = 4
        Me.btnAddNewRateItem.Text = "Add New Rate Item"
        Me.btnAddNewRateItem.UseVisualStyleBackColor = True
        '
        'FinFeeRateManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 485)
        Me.Controls.Add(Me.dgvRateItemHistory)
        Me.Controls.Add(Me.dgvRateItems)
        Me.Controls.Add(Me.grpNewRate)
        Me.Controls.Add(Me.grpEndRateItem)
        Me.Controls.Add(Me.grpEditRate)
        Me.Controls.Add(Me.btnEndRateItem)
        Me.Controls.Add(Me.chkShowInactive)
        Me.Controls.Add(Me.cmbCategory)
        Me.Controls.Add(Me.btnEditEffectiveRate)
        Me.Controls.Add(Me.btnNewEffectiveRate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRateItemName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAddNewRateItem)
        Me.Controls.Add(Me.btnSaveRateItemName)
        Me.Controls.Add(Me.Label3)
        Me.MinimumSize = New System.Drawing.Size(550, 430)
        Me.Name = "FinFeeRateManagement"
        Me.Text = "Fee Rate Management"
        Me.grpNewRate.ResumeLayout(False)
        Me.grpNewRate.PerformLayout()
        CType(Me.dgvRateItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRateItemHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEndRateItem.ResumeLayout(False)
        Me.grpEndRateItem.PerformLayout()
        Me.grpEditRate.ResumeLayout(False)
        Me.grpEditRate.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents chkShowInactive As CheckBox
    Friend WithEvents grpNewRate As GroupBox
    Friend WithEvents btnSaveRateItemName As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtRateItemName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dgvRateItems As IaipDataGridView
    Friend WithEvents dgvRateItemHistory As IaipDataGridView
    Friend WithEvents txtNewRate As CurrencyTextBox
    Friend WithEvents btnNewEffectiveRate As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpNewRateDate As DateTimePicker
    Friend WithEvents btnCancelNewRate As Button
    Friend WithEvents btnSaveNewRate As Button
    Friend WithEvents btnEndRateItem As Button
    Friend WithEvents grpEndRateItem As GroupBox
    Friend WithEvents dtpEndRateItemDate As DateTimePicker
    Friend WithEvents btnCancelEndRateItem As Button
    Friend WithEvents btnSaveEndRateItem As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents btnEditEffectiveRate As Button
    Friend WithEvents grpEditRate As GroupBox
    Friend WithEvents dtpEditRateDate As DateTimePicker
    Friend WithEvents txtEditRate As CurrencyTextBox
    Friend WithEvents btnCancelEditRate As Button
    Friend WithEvents btnSaveEditRate As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents btnAddNewRateItem As Button
End Class
