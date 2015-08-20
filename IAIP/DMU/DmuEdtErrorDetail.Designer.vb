<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DmuEdtErrorDetail
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
        Me.ErrorIDDisplay = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GenericErrorMessageLabel = New System.Windows.Forms.Label()
        Me.GenericErrorMessageDisplayContainer = New System.Windows.Forms.Panel()
        Me.GenericErrorMessageDisplay = New System.Windows.Forms.Label()
        Me.BusinessRuleDisplayContainer = New System.Windows.Forms.Panel()
        Me.BusinessRuleDisplay = New System.Windows.Forms.Label()
        Me.BusinessRuleLabel = New System.Windows.Forms.Label()
        Me.UserAssigned = New System.Windows.Forms.ComboBox()
        Me.ChangeStatus = New System.Windows.Forms.Button()
        Me.AssignSelectedToUser = New System.Windows.Forms.Button()
        Me.EdtErrorCodeLabel = New System.Windows.Forms.Label()
        Me.CurrentStatus = New System.Windows.Forms.Label()
        Me.TableNameLabel = New System.Windows.Forms.Label()
        Me.EdtIdLabel = New System.Windows.Forms.Label()
        Me.EdtForeignKeyLabel = New System.Windows.Forms.Label()
        Me.EdtOperationLabel = New System.Windows.Forms.Label()
        Me.EdtStatusLabel = New System.Windows.Forms.Label()
        Me.EdtDateSubmittedLabel = New System.Windows.Forms.Label()
        Me.ErrorMessageDisplayContainer = New System.Windows.Forms.Panel()
        Me.ErrorMessageDisplay = New System.Windows.Forms.Label()
        Me.ErrorMessageLabel = New System.Windows.Forms.Label()
        Me.EdtErrorCode = New System.Windows.Forms.TextBox()
        Me.TableName = New System.Windows.Forms.TextBox()
        Me.EdtId = New System.Windows.Forms.TextBox()
        Me.EdtForeignKey = New System.Windows.Forms.TextBox()
        Me.EdtOperation = New System.Windows.Forms.TextBox()
        Me.EdtStatus = New System.Windows.Forms.TextBox()
        Me.EdtDateSubmitted = New System.Windows.Forms.TextBox()
        Me.IaipIdLabel = New System.Windows.Forms.Label()
        Me.IaipForeignIdLabel = New System.Windows.Forms.Label()
        Me.IaipId = New System.Windows.Forms.LinkLabel()
        Me.IaipForeignId = New System.Windows.Forms.LinkLabel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GenericErrorMessageDisplayContainer.SuspendLayout()
        Me.BusinessRuleDisplayContainer.SuspendLayout()
        Me.ErrorMessageDisplayContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'ErrorIDDisplay
        '
        Me.ErrorIDDisplay.AutoSize = True
        Me.ErrorIDDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrorIDDisplay.Location = New System.Drawing.Point(15, 9)
        Me.ErrorIDDisplay.Name = "ErrorIDDisplay"
        Me.ErrorIDDisplay.Size = New System.Drawing.Size(66, 20)
        Me.ErrorIDDisplay.TabIndex = 0
        Me.ErrorIDDisplay.Text = "Error #0"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 387)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GenericErrorMessageLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GenericErrorMessageDisplayContainer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BusinessRuleDisplayContainer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BusinessRuleLabel)
        Me.SplitContainer1.Size = New System.Drawing.Size(414, 114)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 4
        '
        'GenericErrorMessageLabel
        '
        Me.GenericErrorMessageLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GenericErrorMessageLabel.AutoSize = True
        Me.GenericErrorMessageLabel.Location = New System.Drawing.Point(16, 2)
        Me.GenericErrorMessageLabel.Name = "GenericErrorMessageLabel"
        Me.GenericErrorMessageLabel.Size = New System.Drawing.Size(121, 13)
        Me.GenericErrorMessageLabel.TabIndex = 13
        Me.GenericErrorMessageLabel.Text = "Error Message (Generic)"
        '
        'GenericErrorMessageDisplayContainer
        '
        Me.GenericErrorMessageDisplayContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GenericErrorMessageDisplayContainer.AutoScroll = True
        Me.GenericErrorMessageDisplayContainer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.GenericErrorMessageDisplayContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GenericErrorMessageDisplayContainer.Controls.Add(Me.GenericErrorMessageDisplay)
        Me.GenericErrorMessageDisplayContainer.Location = New System.Drawing.Point(12, 18)
        Me.GenericErrorMessageDisplayContainer.Name = "GenericErrorMessageDisplayContainer"
        Me.GenericErrorMessageDisplayContainer.Size = New System.Drawing.Size(180, 88)
        Me.GenericErrorMessageDisplayContainer.TabIndex = 0
        '
        'GenericErrorMessageDisplay
        '
        Me.GenericErrorMessageDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GenericErrorMessageDisplay.AutoSize = True
        Me.GenericErrorMessageDisplay.Location = New System.Drawing.Point(3, 0)
        Me.GenericErrorMessageDisplay.MaximumSize = New System.Drawing.Size(150, 0)
        Me.GenericErrorMessageDisplay.Name = "GenericErrorMessageDisplay"
        Me.GenericErrorMessageDisplay.Size = New System.Drawing.Size(0, 13)
        Me.GenericErrorMessageDisplay.TabIndex = 13
        '
        'BusinessRuleDisplayContainer
        '
        Me.BusinessRuleDisplayContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BusinessRuleDisplayContainer.AutoScroll = True
        Me.BusinessRuleDisplayContainer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BusinessRuleDisplayContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BusinessRuleDisplayContainer.Controls.Add(Me.BusinessRuleDisplay)
        Me.BusinessRuleDisplayContainer.Location = New System.Drawing.Point(12, 18)
        Me.BusinessRuleDisplayContainer.Name = "BusinessRuleDisplayContainer"
        Me.BusinessRuleDisplayContainer.Size = New System.Drawing.Size(180, 87)
        Me.BusinessRuleDisplayContainer.TabIndex = 0
        '
        'BusinessRuleDisplay
        '
        Me.BusinessRuleDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BusinessRuleDisplay.AutoSize = True
        Me.BusinessRuleDisplay.Location = New System.Drawing.Point(3, 0)
        Me.BusinessRuleDisplay.MaximumSize = New System.Drawing.Size(150, 0)
        Me.BusinessRuleDisplay.Name = "BusinessRuleDisplay"
        Me.BusinessRuleDisplay.Size = New System.Drawing.Size(0, 13)
        Me.BusinessRuleDisplay.TabIndex = 13
        '
        'BusinessRuleLabel
        '
        Me.BusinessRuleLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BusinessRuleLabel.AutoSize = True
        Me.BusinessRuleLabel.Location = New System.Drawing.Point(16, 2)
        Me.BusinessRuleLabel.Name = "BusinessRuleLabel"
        Me.BusinessRuleLabel.Size = New System.Drawing.Size(74, 13)
        Me.BusinessRuleLabel.TabIndex = 13
        Me.BusinessRuleLabel.Text = "Business Rule"
        '
        'UserAssigned
        '
        Me.UserAssigned.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UserAssigned.FormattingEnabled = True
        Me.UserAssigned.Location = New System.Drawing.Point(19, 54)
        Me.UserAssigned.Name = "UserAssigned"
        Me.UserAssigned.Size = New System.Drawing.Size(167, 21)
        Me.UserAssigned.TabIndex = 2
        '
        'ChangeStatus
        '
        Me.ChangeStatus.Location = New System.Drawing.Point(297, 53)
        Me.ChangeStatus.Name = "ChangeStatus"
        Me.ChangeStatus.Size = New System.Drawing.Size(99, 23)
        Me.ChangeStatus.TabIndex = 4
        Me.ChangeStatus.Text = "Reopen"
        Me.ChangeStatus.UseVisualStyleBackColor = True
        '
        'AssignSelectedToUser
        '
        Me.AssignSelectedToUser.Location = New System.Drawing.Point(192, 53)
        Me.AssignSelectedToUser.Name = "AssignSelectedToUser"
        Me.AssignSelectedToUser.Size = New System.Drawing.Size(99, 23)
        Me.AssignSelectedToUser.TabIndex = 3
        Me.AssignSelectedToUser.Text = "Assign User"
        Me.AssignSelectedToUser.UseVisualStyleBackColor = True
        '
        'EdtErrorCodeLabel
        '
        Me.EdtErrorCodeLabel.AutoSize = True
        Me.EdtErrorCodeLabel.Location = New System.Drawing.Point(16, 95)
        Me.EdtErrorCodeLabel.Name = "EdtErrorCodeLabel"
        Me.EdtErrorCodeLabel.Size = New System.Drawing.Size(57, 13)
        Me.EdtErrorCodeLabel.TabIndex = 0
        Me.EdtErrorCodeLabel.Text = "Error Code"
        '
        'CurrentStatus
        '
        Me.CurrentStatus.AutoSize = True
        Me.CurrentStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrentStatus.Location = New System.Drawing.Point(138, 9)
        Me.CurrentStatus.Name = "CurrentStatus"
        Me.CurrentStatus.Size = New System.Drawing.Size(65, 20)
        Me.CurrentStatus.TabIndex = 1
        Me.CurrentStatus.Text = "No data"
        '
        'TableNameLabel
        '
        Me.TableNameLabel.AutoSize = True
        Me.TableNameLabel.Location = New System.Drawing.Point(16, 114)
        Me.TableNameLabel.Name = "TableNameLabel"
        Me.TableNameLabel.Size = New System.Drawing.Size(65, 13)
        Me.TableNameLabel.TabIndex = 0
        Me.TableNameLabel.Text = "Table Name"
        '
        'EdtIdLabel
        '
        Me.EdtIdLabel.AutoSize = True
        Me.EdtIdLabel.Location = New System.Drawing.Point(16, 133)
        Me.EdtIdLabel.Name = "EdtIdLabel"
        Me.EdtIdLabel.Size = New System.Drawing.Size(43, 13)
        Me.EdtIdLabel.TabIndex = 0
        Me.EdtIdLabel.Text = "EDT ID"
        '
        'EdtForeignKeyLabel
        '
        Me.EdtForeignKeyLabel.AutoSize = True
        Me.EdtForeignKeyLabel.Location = New System.Drawing.Point(16, 171)
        Me.EdtForeignKeyLabel.Name = "EdtForeignKeyLabel"
        Me.EdtForeignKeyLabel.Size = New System.Drawing.Size(63, 13)
        Me.EdtForeignKeyLabel.TabIndex = 0
        Me.EdtForeignKeyLabel.Text = "Foreign Key"
        '
        'EdtOperationLabel
        '
        Me.EdtOperationLabel.AutoSize = True
        Me.EdtOperationLabel.Location = New System.Drawing.Point(16, 209)
        Me.EdtOperationLabel.Name = "EdtOperationLabel"
        Me.EdtOperationLabel.Size = New System.Drawing.Size(53, 13)
        Me.EdtOperationLabel.TabIndex = 0
        Me.EdtOperationLabel.Text = "Operation"
        '
        'EdtStatusLabel
        '
        Me.EdtStatusLabel.AutoSize = True
        Me.EdtStatusLabel.Location = New System.Drawing.Point(16, 228)
        Me.EdtStatusLabel.Name = "EdtStatusLabel"
        Me.EdtStatusLabel.Size = New System.Drawing.Size(37, 13)
        Me.EdtStatusLabel.TabIndex = 0
        Me.EdtStatusLabel.Text = "Status"
        '
        'EdtDateSubmittedLabel
        '
        Me.EdtDateSubmittedLabel.AutoSize = True
        Me.EdtDateSubmittedLabel.Location = New System.Drawing.Point(16, 247)
        Me.EdtDateSubmittedLabel.Name = "EdtDateSubmittedLabel"
        Me.EdtDateSubmittedLabel.Size = New System.Drawing.Size(80, 13)
        Me.EdtDateSubmittedLabel.TabIndex = 0
        Me.EdtDateSubmittedLabel.Text = "Date Submitted"
        '
        'ErrorMessageDisplayContainer
        '
        Me.ErrorMessageDisplayContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorMessageDisplayContainer.AutoScroll = True
        Me.ErrorMessageDisplayContainer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ErrorMessageDisplayContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ErrorMessageDisplayContainer.Controls.Add(Me.ErrorMessageDisplay)
        Me.ErrorMessageDisplayContainer.Location = New System.Drawing.Point(12, 298)
        Me.ErrorMessageDisplayContainer.Name = "ErrorMessageDisplayContainer"
        Me.ErrorMessageDisplayContainer.Size = New System.Drawing.Size(391, 73)
        Me.ErrorMessageDisplayContainer.TabIndex = 12
        '
        'ErrorMessageDisplay
        '
        Me.ErrorMessageDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorMessageDisplay.AutoSize = True
        Me.ErrorMessageDisplay.Location = New System.Drawing.Point(3, 0)
        Me.ErrorMessageDisplay.MaximumSize = New System.Drawing.Size(361, 0)
        Me.ErrorMessageDisplay.Name = "ErrorMessageDisplay"
        Me.ErrorMessageDisplay.Size = New System.Drawing.Size(0, 13)
        Me.ErrorMessageDisplay.TabIndex = 13
        '
        'ErrorMessageLabel
        '
        Me.ErrorMessageLabel.AutoSize = True
        Me.ErrorMessageLabel.Location = New System.Drawing.Point(16, 282)
        Me.ErrorMessageLabel.Name = "ErrorMessageLabel"
        Me.ErrorMessageLabel.Size = New System.Drawing.Size(122, 13)
        Me.ErrorMessageLabel.TabIndex = 13
        Me.ErrorMessageLabel.Text = "Error Message (Specific)"
        '
        'EdtErrorCode
        '
        Me.EdtErrorCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EdtErrorCode.Location = New System.Drawing.Point(125, 95)
        Me.EdtErrorCode.Name = "EdtErrorCode"
        Me.EdtErrorCode.ReadOnly = True
        Me.EdtErrorCode.Size = New System.Drawing.Size(271, 13)
        Me.EdtErrorCode.TabIndex = 5
        Me.EdtErrorCode.Text = "N/A"
        '
        'TableName
        '
        Me.TableName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TableName.Location = New System.Drawing.Point(125, 114)
        Me.TableName.Name = "TableName"
        Me.TableName.ReadOnly = True
        Me.TableName.Size = New System.Drawing.Size(271, 13)
        Me.TableName.TabIndex = 6
        Me.TableName.Text = "N/A"
        '
        'EdtId
        '
        Me.EdtId.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EdtId.Location = New System.Drawing.Point(125, 133)
        Me.EdtId.Name = "EdtId"
        Me.EdtId.ReadOnly = True
        Me.EdtId.Size = New System.Drawing.Size(271, 13)
        Me.EdtId.TabIndex = 7
        Me.EdtId.Text = "N/A"
        '
        'EdtForeignKey
        '
        Me.EdtForeignKey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EdtForeignKey.Location = New System.Drawing.Point(125, 171)
        Me.EdtForeignKey.Name = "EdtForeignKey"
        Me.EdtForeignKey.ReadOnly = True
        Me.EdtForeignKey.Size = New System.Drawing.Size(271, 13)
        Me.EdtForeignKey.TabIndex = 8
        Me.EdtForeignKey.Text = "N/A"
        '
        'EdtOperation
        '
        Me.EdtOperation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EdtOperation.Location = New System.Drawing.Point(125, 209)
        Me.EdtOperation.Name = "EdtOperation"
        Me.EdtOperation.ReadOnly = True
        Me.EdtOperation.Size = New System.Drawing.Size(271, 13)
        Me.EdtOperation.TabIndex = 9
        Me.EdtOperation.Text = "N/A"
        '
        'EdtStatus
        '
        Me.EdtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EdtStatus.Location = New System.Drawing.Point(125, 228)
        Me.EdtStatus.Name = "EdtStatus"
        Me.EdtStatus.ReadOnly = True
        Me.EdtStatus.Size = New System.Drawing.Size(271, 13)
        Me.EdtStatus.TabIndex = 10
        Me.EdtStatus.Text = "N/A"
        '
        'EdtDateSubmitted
        '
        Me.EdtDateSubmitted.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EdtDateSubmitted.Location = New System.Drawing.Point(125, 247)
        Me.EdtDateSubmitted.Name = "EdtDateSubmitted"
        Me.EdtDateSubmitted.ReadOnly = True
        Me.EdtDateSubmitted.Size = New System.Drawing.Size(271, 13)
        Me.EdtDateSubmitted.TabIndex = 11
        Me.EdtDateSubmitted.Text = "N/A"
        '
        'IaipIdLabel
        '
        Me.IaipIdLabel.AutoSize = True
        Me.IaipIdLabel.Location = New System.Drawing.Point(16, 152)
        Me.IaipIdLabel.Name = "IaipIdLabel"
        Me.IaipIdLabel.Size = New System.Drawing.Size(41, 13)
        Me.IaipIdLabel.TabIndex = 0
        Me.IaipIdLabel.Text = "IAIP ID"
        '
        'IaipForeignIdLabel
        '
        Me.IaipForeignIdLabel.AutoSize = True
        Me.IaipForeignIdLabel.Location = New System.Drawing.Point(16, 190)
        Me.IaipForeignIdLabel.Name = "IaipForeignIdLabel"
        Me.IaipForeignIdLabel.Size = New System.Drawing.Size(100, 13)
        Me.IaipForeignIdLabel.TabIndex = 0
        Me.IaipForeignIdLabel.Text = "IAIP Foreign Key ID"
        '
        'IaipId
        '
        Me.IaipId.AutoSize = True
        Me.IaipId.Enabled = False
        Me.IaipId.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.IaipId.Location = New System.Drawing.Point(122, 152)
        Me.IaipId.Name = "IaipId"
        Me.IaipId.Size = New System.Drawing.Size(27, 13)
        Me.IaipId.TabIndex = 14
        Me.IaipId.TabStop = True
        Me.IaipId.Text = "N/A"
        '
        'IaipForeignId
        '
        Me.IaipForeignId.AutoSize = True
        Me.IaipForeignId.Enabled = False
        Me.IaipForeignId.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.IaipForeignId.Location = New System.Drawing.Point(122, 190)
        Me.IaipForeignId.Name = "IaipForeignId"
        Me.IaipForeignId.Size = New System.Drawing.Size(27, 13)
        Me.IaipForeignId.TabIndex = 15
        Me.IaipForeignId.TabStop = True
        Me.IaipForeignId.Text = "N/A"
        '
        'DmuEdtErrorDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 502)
        Me.Controls.Add(Me.IaipForeignId)
        Me.Controls.Add(Me.IaipId)
        Me.Controls.Add(Me.EdtDateSubmitted)
        Me.Controls.Add(Me.EdtStatus)
        Me.Controls.Add(Me.EdtOperation)
        Me.Controls.Add(Me.EdtForeignKey)
        Me.Controls.Add(Me.EdtId)
        Me.Controls.Add(Me.TableName)
        Me.Controls.Add(Me.EdtErrorCode)
        Me.Controls.Add(Me.ErrorMessageDisplayContainer)
        Me.Controls.Add(Me.ErrorMessageLabel)
        Me.Controls.Add(Me.UserAssigned)
        Me.Controls.Add(Me.ChangeStatus)
        Me.Controls.Add(Me.TableNameLabel)
        Me.Controls.Add(Me.EdtErrorCodeLabel)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.AssignSelectedToUser)
        Me.Controls.Add(Me.ErrorIDDisplay)
        Me.Controls.Add(Me.CurrentStatus)
        Me.Controls.Add(Me.IaipForeignIdLabel)
        Me.Controls.Add(Me.EdtDateSubmittedLabel)
        Me.Controls.Add(Me.IaipIdLabel)
        Me.Controls.Add(Me.EdtIdLabel)
        Me.Controls.Add(Me.EdtStatusLabel)
        Me.Controls.Add(Me.EdtForeignKeyLabel)
        Me.Controls.Add(Me.EdtOperationLabel)
        Me.MinimumSize = New System.Drawing.Size(431, 540)
        Me.Name = "DmuEdtErrorDetail"
        Me.Text = "EDT Error Item Detail"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GenericErrorMessageDisplayContainer.ResumeLayout(False)
        Me.GenericErrorMessageDisplayContainer.PerformLayout()
        Me.BusinessRuleDisplayContainer.ResumeLayout(False)
        Me.BusinessRuleDisplayContainer.PerformLayout()
        Me.ErrorMessageDisplayContainer.ResumeLayout(False)
        Me.ErrorMessageDisplayContainer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ErrorIDDisplay As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GenericErrorMessageLabel As System.Windows.Forms.Label
    Friend WithEvents GenericErrorMessageDisplayContainer As System.Windows.Forms.Panel
    Friend WithEvents GenericErrorMessageDisplay As System.Windows.Forms.Label
    Friend WithEvents BusinessRuleDisplayContainer As System.Windows.Forms.Panel
    Friend WithEvents BusinessRuleDisplay As System.Windows.Forms.Label
    Friend WithEvents BusinessRuleLabel As System.Windows.Forms.Label
    Friend WithEvents UserAssigned As System.Windows.Forms.ComboBox
    Friend WithEvents ChangeStatus As System.Windows.Forms.Button
    Friend WithEvents AssignSelectedToUser As System.Windows.Forms.Button
    Friend WithEvents EdtErrorCodeLabel As System.Windows.Forms.Label
    Friend WithEvents CurrentStatus As System.Windows.Forms.Label
    Friend WithEvents TableNameLabel As System.Windows.Forms.Label
    Friend WithEvents EdtIdLabel As System.Windows.Forms.Label
    Friend WithEvents EdtForeignKeyLabel As System.Windows.Forms.Label
    Friend WithEvents EdtOperationLabel As System.Windows.Forms.Label
    Friend WithEvents EdtStatusLabel As System.Windows.Forms.Label
    Friend WithEvents EdtDateSubmittedLabel As System.Windows.Forms.Label
    Friend WithEvents ErrorMessageDisplayContainer As System.Windows.Forms.Panel
    Friend WithEvents ErrorMessageDisplay As System.Windows.Forms.Label
    Friend WithEvents ErrorMessageLabel As System.Windows.Forms.Label
    Friend WithEvents EdtErrorCode As System.Windows.Forms.TextBox
    Friend WithEvents TableName As System.Windows.Forms.TextBox
    Friend WithEvents EdtId As System.Windows.Forms.TextBox
    Friend WithEvents EdtForeignKey As System.Windows.Forms.TextBox
    Friend WithEvents EdtOperation As System.Windows.Forms.TextBox
    Friend WithEvents EdtStatus As System.Windows.Forms.TextBox
    Friend WithEvents EdtDateSubmitted As System.Windows.Forms.TextBox
    Friend WithEvents IaipIdLabel As System.Windows.Forms.Label
    Friend WithEvents IaipForeignIdLabel As System.Windows.Forms.Label
    Friend WithEvents IaipId As System.Windows.Forms.LinkLabel
    Friend WithEvents IaipForeignId As System.Windows.Forms.LinkLabel
End Class
