<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DmuEdtErrorMessageDetail
    Inherits BaseForm

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.OwnerGroupPanel = New System.Windows.Forms.Panel
        Me.DisplayOwnerMine = New System.Windows.Forms.RadioButton
        Me.DisplayOwnerEveryone = New System.Windows.Forms.RadioButton
        Me.ResolvedStatusGroupPanel = New System.Windows.Forms.Panel
        Me.DisplayResolutionAll = New System.Windows.Forms.RadioButton
        Me.DisplayResolutionOpen = New System.Windows.Forms.RadioButton
        Me.ReloadButton = New System.Windows.Forms.Button
        Me.EdtErrorCountDisplay = New System.Windows.Forms.Label
        Me.EdtErrorMessageGrid = New System.Windows.Forms.DataGridView
        Me.ErrorCodeDisplay = New System.Windows.Forms.Label
        Me.ErrorMessageDisplay = New System.Windows.Forms.Label
        Me.ErrorMessageLabel = New System.Windows.Forms.Label
        Me.ErrorMessageDisplayContainer = New System.Windows.Forms.Panel
        Me.BusinessRuleLabel = New System.Windows.Forms.Label
        Me.BusinessRuleDisplayContainer = New System.Windows.Forms.Panel
        Me.BusinessRuleDisplay = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.UserAsDefault = New System.Windows.Forms.ComboBox
        Me.DefaultUserLabel = New System.Windows.Forms.Label
        Me.UserToAssign = New System.Windows.Forms.ComboBox
        Me.AssignSelectedToUser = New System.Windows.Forms.Button
        Me.ChangeStatusForSelectedRows = New System.Windows.Forms.Button
        Me.AssignDefaultUser = New System.Windows.Forms.Button
        Me.OpenEdtError = New System.Windows.Forms.Button
        Me.GridSelectionActionPanel = New System.Windows.Forms.Panel
        Me.OwnerGroupPanel.SuspendLayout()
        Me.ResolvedStatusGroupPanel.SuspendLayout()
        CType(Me.EdtErrorMessageGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ErrorMessageDisplayContainer.SuspendLayout()
        Me.BusinessRuleDisplayContainer.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GridSelectionActionPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'OwnerGroupPanel
        '
        Me.OwnerGroupPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OwnerGroupPanel.BackColor = System.Drawing.Color.Transparent
        Me.OwnerGroupPanel.Controls.Add(Me.DisplayOwnerMine)
        Me.OwnerGroupPanel.Controls.Add(Me.DisplayOwnerEveryone)
        Me.OwnerGroupPanel.Enabled = False
        Me.OwnerGroupPanel.Location = New System.Drawing.Point(343, 194)
        Me.OwnerGroupPanel.Name = "OwnerGroupPanel"
        Me.OwnerGroupPanel.Size = New System.Drawing.Size(131, 31)
        Me.OwnerGroupPanel.TabIndex = 5
        '
        'DisplayOwnerMine
        '
        Me.DisplayOwnerMine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOwnerMine.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayOwnerMine.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayOwnerMine.Location = New System.Drawing.Point(0, 8)
        Me.DisplayOwnerMine.Name = "DisplayOwnerMine"
        Me.DisplayOwnerMine.Size = New System.Drawing.Size(66, 23)
        Me.DisplayOwnerMine.TabIndex = 0
        Me.DisplayOwnerMine.Text = "Mine"
        Me.DisplayOwnerMine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayOwnerMine.UseVisualStyleBackColor = True
        '
        'DisplayOwnerEveryone
        '
        Me.DisplayOwnerEveryone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOwnerEveryone.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayOwnerEveryone.Checked = True
        Me.DisplayOwnerEveryone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayOwnerEveryone.Location = New System.Drawing.Point(65, 8)
        Me.DisplayOwnerEveryone.Name = "DisplayOwnerEveryone"
        Me.DisplayOwnerEveryone.Size = New System.Drawing.Size(66, 23)
        Me.DisplayOwnerEveryone.TabIndex = 1
        Me.DisplayOwnerEveryone.TabStop = True
        Me.DisplayOwnerEveryone.Text = "All"
        Me.DisplayOwnerEveryone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayOwnerEveryone.UseVisualStyleBackColor = True
        '
        'ResolvedStatusGroupPanel
        '
        Me.ResolvedStatusGroupPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResolvedStatusGroupPanel.BackColor = System.Drawing.Color.Transparent
        Me.ResolvedStatusGroupPanel.Controls.Add(Me.DisplayResolutionAll)
        Me.ResolvedStatusGroupPanel.Controls.Add(Me.DisplayResolutionOpen)
        Me.ResolvedStatusGroupPanel.Enabled = False
        Me.ResolvedStatusGroupPanel.Location = New System.Drawing.Point(483, 194)
        Me.ResolvedStatusGroupPanel.Name = "ResolvedStatusGroupPanel"
        Me.ResolvedStatusGroupPanel.Size = New System.Drawing.Size(131, 31)
        Me.ResolvedStatusGroupPanel.TabIndex = 6
        '
        'DisplayResolutionAll
        '
        Me.DisplayResolutionAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayResolutionAll.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayResolutionAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayResolutionAll.Location = New System.Drawing.Point(65, 8)
        Me.DisplayResolutionAll.Name = "DisplayResolutionAll"
        Me.DisplayResolutionAll.Size = New System.Drawing.Size(66, 23)
        Me.DisplayResolutionAll.TabIndex = 1
        Me.DisplayResolutionAll.Text = "All"
        Me.DisplayResolutionAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayResolutionAll.UseVisualStyleBackColor = True
        '
        'DisplayResolutionOpen
        '
        Me.DisplayResolutionOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayResolutionOpen.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayResolutionOpen.Checked = True
        Me.DisplayResolutionOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayResolutionOpen.Location = New System.Drawing.Point(0, 8)
        Me.DisplayResolutionOpen.Name = "DisplayResolutionOpen"
        Me.DisplayResolutionOpen.Size = New System.Drawing.Size(66, 23)
        Me.DisplayResolutionOpen.TabIndex = 0
        Me.DisplayResolutionOpen.TabStop = True
        Me.DisplayResolutionOpen.Text = "Open"
        Me.DisplayResolutionOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayResolutionOpen.UseVisualStyleBackColor = True
        '
        'ReloadButton
        '
        Me.ReloadButton.Location = New System.Drawing.Point(12, 191)
        Me.ReloadButton.Name = "ReloadButton"
        Me.ReloadButton.Size = New System.Drawing.Size(96, 23)
        Me.ReloadButton.TabIndex = 4
        Me.ReloadButton.Text = "Reload Errors"
        Me.ReloadButton.UseVisualStyleBackColor = True
        '
        'EdtErrorCountDisplay
        '
        Me.EdtErrorCountDisplay.AutoSize = True
        Me.EdtErrorCountDisplay.Location = New System.Drawing.Point(114, 196)
        Me.EdtErrorCountDisplay.Name = "EdtErrorCountDisplay"
        Me.EdtErrorCountDisplay.Size = New System.Drawing.Size(97, 13)
        Me.EdtErrorCountDisplay.TabIndex = 10
        Me.EdtErrorCountDisplay.Text = "No errors to display"
        '
        'EdtErrorMessageGrid
        '
        Me.EdtErrorMessageGrid.AllowUserToAddRows = False
        Me.EdtErrorMessageGrid.AllowUserToDeleteRows = False
        Me.EdtErrorMessageGrid.AllowUserToOrderColumns = True
        Me.EdtErrorMessageGrid.AllowUserToResizeRows = False
        Me.EdtErrorMessageGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EdtErrorMessageGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.EdtErrorMessageGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.EdtErrorMessageGrid.GridColor = System.Drawing.SystemColors.ControlLight
        Me.EdtErrorMessageGrid.Location = New System.Drawing.Point(12, 224)
        Me.EdtErrorMessageGrid.Name = "EdtErrorMessageGrid"
        Me.EdtErrorMessageGrid.ReadOnly = True
        Me.EdtErrorMessageGrid.RowHeadersVisible = False
        Me.EdtErrorMessageGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EdtErrorMessageGrid.Size = New System.Drawing.Size(602, 245)
        Me.EdtErrorMessageGrid.TabIndex = 7
        '
        'ErrorCodeDisplay
        '
        Me.ErrorCodeDisplay.AutoSize = True
        Me.ErrorCodeDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrorCodeDisplay.Location = New System.Drawing.Point(15, 23)
        Me.ErrorCodeDisplay.Name = "ErrorCodeDisplay"
        Me.ErrorCodeDisplay.Size = New System.Drawing.Size(69, 20)
        Me.ErrorCodeDisplay.TabIndex = 13
        Me.ErrorCodeDisplay.Text = "ABC123"
        '
        'ErrorMessageDisplay
        '
        Me.ErrorMessageDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorMessageDisplay.AutoSize = True
        Me.ErrorMessageDisplay.Location = New System.Drawing.Point(3, 0)
        Me.ErrorMessageDisplay.MaximumSize = New System.Drawing.Size(258, 0)
        Me.ErrorMessageDisplay.Name = "ErrorMessageDisplay"
        Me.ErrorMessageDisplay.Size = New System.Drawing.Size(0, 13)
        Me.ErrorMessageDisplay.TabIndex = 13
        '
        'ErrorMessageLabel
        '
        Me.ErrorMessageLabel.AutoSize = True
        Me.ErrorMessageLabel.Location = New System.Drawing.Point(15, 4)
        Me.ErrorMessageLabel.Name = "ErrorMessageLabel"
        Me.ErrorMessageLabel.Size = New System.Drawing.Size(121, 13)
        Me.ErrorMessageLabel.TabIndex = 13
        Me.ErrorMessageLabel.Text = "Error Message (Generic)"
        '
        'ErrorMessageDisplayContainer
        '
        Me.ErrorMessageDisplayContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorMessageDisplayContainer.AutoScroll = True
        Me.ErrorMessageDisplayContainer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ErrorMessageDisplayContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ErrorMessageDisplayContainer.Controls.Add(Me.ErrorMessageDisplay)
        Me.ErrorMessageDisplayContainer.Location = New System.Drawing.Point(11, 20)
        Me.ErrorMessageDisplayContainer.Name = "ErrorMessageDisplayContainer"
        Me.ErrorMessageDisplayContainer.Size = New System.Drawing.Size(288, 88)
        Me.ErrorMessageDisplayContainer.TabIndex = 0
        '
        'BusinessRuleLabel
        '
        Me.BusinessRuleLabel.AutoSize = True
        Me.BusinessRuleLabel.Location = New System.Drawing.Point(20, 5)
        Me.BusinessRuleLabel.Name = "BusinessRuleLabel"
        Me.BusinessRuleLabel.Size = New System.Drawing.Size(74, 13)
        Me.BusinessRuleLabel.TabIndex = 13
        Me.BusinessRuleLabel.Text = "Business Rule"
        '
        'BusinessRuleDisplayContainer
        '
        Me.BusinessRuleDisplayContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BusinessRuleDisplayContainer.AutoScroll = True
        Me.BusinessRuleDisplayContainer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BusinessRuleDisplayContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BusinessRuleDisplayContainer.Controls.Add(Me.BusinessRuleDisplay)
        Me.BusinessRuleDisplayContainer.Location = New System.Drawing.Point(15, 21)
        Me.BusinessRuleDisplayContainer.Name = "BusinessRuleDisplayContainer"
        Me.BusinessRuleDisplayContainer.Size = New System.Drawing.Size(281, 88)
        Me.BusinessRuleDisplayContainer.TabIndex = 0
        '
        'BusinessRuleDisplay
        '
        Me.BusinessRuleDisplay.AutoSize = True
        Me.BusinessRuleDisplay.Location = New System.Drawing.Point(3, 0)
        Me.BusinessRuleDisplay.MaximumSize = New System.Drawing.Size(251, 0)
        Me.BusinessRuleDisplay.Name = "BusinessRuleDisplay"
        Me.BusinessRuleDisplay.Size = New System.Drawing.Size(0, 13)
        Me.BusinessRuleDisplay.TabIndex = 13
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 62)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ErrorMessageLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ErrorMessageDisplayContainer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BusinessRuleDisplayContainer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BusinessRuleLabel)
        Me.SplitContainer1.Size = New System.Drawing.Size(622, 116)
        Me.SplitContainer1.SplitterDistance = 313
        Me.SplitContainer1.TabIndex = 3
        '
        'UserAsDefault
        '
        Me.UserAsDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserAsDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UserAsDefault.FormattingEnabled = True
        Me.UserAsDefault.Location = New System.Drawing.Point(340, 24)
        Me.UserAsDefault.Name = "UserAsDefault"
        Me.UserAsDefault.Size = New System.Drawing.Size(154, 21)
        Me.UserAsDefault.TabIndex = 0
        '
        'DefaultUserLabel
        '
        Me.DefaultUserLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DefaultUserLabel.AutoSize = True
        Me.DefaultUserLabel.Location = New System.Drawing.Point(340, 9)
        Me.DefaultUserLabel.Name = "DefaultUserLabel"
        Me.DefaultUserLabel.Size = New System.Drawing.Size(66, 13)
        Me.DefaultUserLabel.TabIndex = 17
        Me.DefaultUserLabel.Text = "Default User"
        '
        'UserToAssign
        '
        Me.UserToAssign.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UserToAssign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UserToAssign.FormattingEnabled = True
        Me.UserToAssign.Location = New System.Drawing.Point(11, 6)
        Me.UserToAssign.Name = "UserToAssign"
        Me.UserToAssign.Size = New System.Drawing.Size(154, 21)
        Me.UserToAssign.TabIndex = 0
        '
        'AssignSelectedToUser
        '
        Me.AssignSelectedToUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AssignSelectedToUser.Location = New System.Drawing.Point(171, 5)
        Me.AssignSelectedToUser.Name = "AssignSelectedToUser"
        Me.AssignSelectedToUser.Size = New System.Drawing.Size(99, 23)
        Me.AssignSelectedToUser.TabIndex = 1
        Me.AssignSelectedToUser.Text = "Assign User"
        Me.AssignSelectedToUser.UseVisualStyleBackColor = True
        '
        'ChangeStatusForSelectedRows
        '
        Me.ChangeStatusForSelectedRows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ChangeStatusForSelectedRows.Location = New System.Drawing.Point(276, 5)
        Me.ChangeStatusForSelectedRows.Name = "ChangeStatusForSelectedRows"
        Me.ChangeStatusForSelectedRows.Size = New System.Drawing.Size(99, 23)
        Me.ChangeStatusForSelectedRows.TabIndex = 2
        Me.ChangeStatusForSelectedRows.Text = "Resolve Selected"
        Me.ChangeStatusForSelectedRows.UseVisualStyleBackColor = True
        '
        'AssignDefaultUser
        '
        Me.AssignDefaultUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AssignDefaultUser.Location = New System.Drawing.Point(500, 23)
        Me.AssignDefaultUser.Name = "AssignDefaultUser"
        Me.AssignDefaultUser.Size = New System.Drawing.Size(114, 23)
        Me.AssignDefaultUser.TabIndex = 1
        Me.AssignDefaultUser.Text = "Set Default User"
        Me.AssignDefaultUser.UseVisualStyleBackColor = True
        '
        'OpenEdtError
        '
        Me.OpenEdtError.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OpenEdtError.Location = New System.Drawing.Point(515, 475)
        Me.OpenEdtError.Name = "OpenEdtError"
        Me.OpenEdtError.Size = New System.Drawing.Size(99, 23)
        Me.OpenEdtError.TabIndex = 9
        Me.OpenEdtError.Text = "View Details"
        Me.OpenEdtError.UseVisualStyleBackColor = True
        '
        'GridSelectionActionPanel
        '
        Me.GridSelectionActionPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GridSelectionActionPanel.Controls.Add(Me.UserToAssign)
        Me.GridSelectionActionPanel.Controls.Add(Me.ChangeStatusForSelectedRows)
        Me.GridSelectionActionPanel.Controls.Add(Me.AssignSelectedToUser)
        Me.GridSelectionActionPanel.Location = New System.Drawing.Point(1, 470)
        Me.GridSelectionActionPanel.Name = "GridSelectionActionPanel"
        Me.GridSelectionActionPanel.Size = New System.Drawing.Size(397, 40)
        Me.GridSelectionActionPanel.TabIndex = 8
        '
        'DmuEdtErrorMessageDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(626, 510)
        Me.Controls.Add(Me.GridSelectionActionPanel)
        Me.Controls.Add(Me.AssignDefaultUser)
        Me.Controls.Add(Me.DefaultUserLabel)
        Me.Controls.Add(Me.UserAsDefault)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ErrorCodeDisplay)
        Me.Controls.Add(Me.OwnerGroupPanel)
        Me.Controls.Add(Me.ResolvedStatusGroupPanel)
        Me.Controls.Add(Me.OpenEdtError)
        Me.Controls.Add(Me.ReloadButton)
        Me.Controls.Add(Me.EdtErrorCountDisplay)
        Me.Controls.Add(Me.EdtErrorMessageGrid)
        Me.MinimumSize = New System.Drawing.Size(500, 330)
        Me.Name = "DmuEdtErrorMessageDetail"
        Me.Text = "EDT Error Code Detail"
        Me.OwnerGroupPanel.ResumeLayout(False)
        Me.ResolvedStatusGroupPanel.ResumeLayout(False)
        CType(Me.EdtErrorMessageGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ErrorMessageDisplayContainer.ResumeLayout(False)
        Me.ErrorMessageDisplayContainer.PerformLayout()
        Me.BusinessRuleDisplayContainer.ResumeLayout(False)
        Me.BusinessRuleDisplayContainer.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GridSelectionActionPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OwnerGroupPanel As System.Windows.Forms.Panel
    Friend WithEvents DisplayOwnerMine As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayOwnerEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents ResolvedStatusGroupPanel As System.Windows.Forms.Panel
    Friend WithEvents DisplayResolutionAll As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayResolutionOpen As System.Windows.Forms.RadioButton
    Friend WithEvents ReloadButton As System.Windows.Forms.Button
    Friend WithEvents EdtErrorCountDisplay As System.Windows.Forms.Label
    Friend WithEvents EdtErrorMessageGrid As System.Windows.Forms.DataGridView
    Friend WithEvents ErrorCodeDisplay As System.Windows.Forms.Label
    Friend WithEvents ErrorMessageDisplay As System.Windows.Forms.Label
    Friend WithEvents ErrorMessageLabel As System.Windows.Forms.Label
    Friend WithEvents ErrorMessageDisplayContainer As System.Windows.Forms.Panel
    Friend WithEvents BusinessRuleLabel As System.Windows.Forms.Label
    Friend WithEvents BusinessRuleDisplayContainer As System.Windows.Forms.Panel
    Friend WithEvents BusinessRuleDisplay As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UserAsDefault As System.Windows.Forms.ComboBox
    Friend WithEvents DefaultUserLabel As System.Windows.Forms.Label
    Friend WithEvents UserToAssign As System.Windows.Forms.ComboBox
    Friend WithEvents AssignSelectedToUser As System.Windows.Forms.Button
    Friend WithEvents ChangeStatusForSelectedRows As System.Windows.Forms.Button
    Friend WithEvents AssignDefaultUser As System.Windows.Forms.Button
    Friend WithEvents OpenEdtError As System.Windows.Forms.Button
    Friend WithEvents GridSelectionActionPanel As System.Windows.Forms.Panel
End Class
