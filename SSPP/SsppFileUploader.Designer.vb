<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SsppFileUploader
    Inherits JohnGaltProject.BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.dgvFileList = New System.Windows.Forms.DataGridView
        Me.btnChooseNewFile = New System.Windows.Forms.Button
        Me.ddlNewDocumentType = New System.Windows.Forms.ComboBox
        Me.lblDocumentTypes = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnNewFileCancel = New System.Windows.Forms.Button
        Me.btnNewFileUpload = New System.Windows.Forms.Button
        Me.txtNewDescription = New System.Windows.Forms.TextBox
        Me.lblNewDescription = New System.Windows.Forms.Label
        Me.lblNewFileName = New System.Windows.Forms.Label
        Me.btnDownloadFile = New System.Windows.Forms.Button
        Me.btnDeleteFile = New System.Windows.Forms.Button
        Me.txtUpdateDescription = New System.Windows.Forms.TextBox
        Me.btnUpdateFileDescription = New System.Windows.Forms.Button
        Me.lblApplicationNumber = New System.Windows.Forms.Label
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.btnFindApplication = New System.Windows.Forms.Button
        Me.lblAppInfo = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lblAppInfo2 = New System.Windows.Forms.Label
        Me.lblSelectedFileName = New System.Windows.Forms.Label
        Me.ddlUpdateDocumentType = New System.Windows.Forms.ComboBox
        CType(Me.dgvFileList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvFileList
        '
        Me.dgvFileList.AllowUserToAddRows = False
        Me.dgvFileList.AllowUserToDeleteRows = False
        Me.dgvFileList.AllowUserToOrderColumns = True
        Me.dgvFileList.AllowUserToResizeRows = False
        Me.dgvFileList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFileList.Enabled = False
        Me.dgvFileList.Location = New System.Drawing.Point(12, 114)
        Me.dgvFileList.MultiSelect = False
        Me.dgvFileList.Name = "dgvFileList"
        Me.dgvFileList.ReadOnly = True
        Me.dgvFileList.RowHeadersVisible = False
        Me.dgvFileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFileList.Size = New System.Drawing.Size(419, 220)
        Me.dgvFileList.StandardTab = True
        Me.dgvFileList.TabIndex = 3
        '
        'btnChooseNewFile
        '
        Me.btnChooseNewFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChooseNewFile.Enabled = False
        Me.btnChooseNewFile.Location = New System.Drawing.Point(139, 29)
        Me.btnChooseNewFile.Name = "btnChooseNewFile"
        Me.btnChooseNewFile.Size = New System.Drawing.Size(75, 23)
        Me.btnChooseNewFile.TabIndex = 1
        Me.btnChooseNewFile.Text = "Choose file"
        Me.btnChooseNewFile.UseVisualStyleBackColor = True
        '
        'ddlNewDocumentType
        '
        Me.ddlNewDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlNewDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlNewDocumentType.Enabled = False
        Me.ddlNewDocumentType.FormattingEnabled = True
        Me.ddlNewDocumentType.Location = New System.Drawing.Point(58, 0)
        Me.ddlNewDocumentType.Name = "ddlNewDocumentType"
        Me.ddlNewDocumentType.Size = New System.Drawing.Size(156, 21)
        Me.ddlNewDocumentType.TabIndex = 0
        '
        'lblDocumentTypes
        '
        Me.lblDocumentTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentTypes.AutoSize = True
        Me.lblDocumentTypes.Location = New System.Drawing.Point(3, 3)
        Me.lblDocumentTypes.Name = "lblDocumentTypes"
        Me.lblDocumentTypes.Size = New System.Drawing.Size(49, 13)
        Me.lblDocumentTypes.TabIndex = 3
        Me.lblDocumentTypes.Text = "Add new"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnNewFileCancel)
        Me.Panel1.Controls.Add(Me.btnNewFileUpload)
        Me.Panel1.Controls.Add(Me.txtNewDescription)
        Me.Panel1.Controls.Add(Me.lblNewDescription)
        Me.Panel1.Controls.Add(Me.lblNewFileName)
        Me.Panel1.Controls.Add(Me.btnChooseNewFile)
        Me.Panel1.Controls.Add(Me.lblDocumentTypes)
        Me.Panel1.Controls.Add(Me.ddlNewDocumentType)
        Me.Panel1.Location = New System.Drawing.Point(446, 113)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(217, 278)
        Me.Panel1.TabIndex = 2
        '
        'btnNewFileCancel
        '
        Me.btnNewFileCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewFileCancel.Enabled = False
        Me.btnNewFileCancel.Location = New System.Drawing.Point(58, 150)
        Me.btnNewFileCancel.Name = "btnNewFileCancel"
        Me.btnNewFileCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnNewFileCancel.TabIndex = 4
        Me.btnNewFileCancel.Text = "Cancel"
        Me.btnNewFileCancel.UseVisualStyleBackColor = True
        Me.btnNewFileCancel.Visible = False
        '
        'btnNewFileUpload
        '
        Me.btnNewFileUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewFileUpload.Enabled = False
        Me.btnNewFileUpload.Location = New System.Drawing.Point(139, 150)
        Me.btnNewFileUpload.Name = "btnNewFileUpload"
        Me.btnNewFileUpload.Size = New System.Drawing.Size(75, 23)
        Me.btnNewFileUpload.TabIndex = 3
        Me.btnNewFileUpload.Text = "Add this file"
        Me.btnNewFileUpload.UseVisualStyleBackColor = True
        Me.btnNewFileUpload.Visible = False
        '
        'txtNewDescription
        '
        Me.txtNewDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewDescription.Location = New System.Drawing.Point(6, 124)
        Me.txtNewDescription.Name = "txtNewDescription"
        Me.txtNewDescription.Size = New System.Drawing.Size(208, 20)
        Me.txtNewDescription.TabIndex = 2
        Me.txtNewDescription.Visible = False
        '
        'lblNewDescription
        '
        Me.lblNewDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewDescription.AutoSize = True
        Me.lblNewDescription.Location = New System.Drawing.Point(3, 108)
        Me.lblNewDescription.Name = "lblNewDescription"
        Me.lblNewDescription.Size = New System.Drawing.Size(106, 13)
        Me.lblNewDescription.TabIndex = 5
        Me.lblNewDescription.Text = "Description (optional)"
        Me.lblNewDescription.Visible = False
        '
        'lblNewFileName
        '
        Me.lblNewFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewFileName.AutoSize = True
        Me.lblNewFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewFileName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblNewFileName.Location = New System.Drawing.Point(3, 75)
        Me.lblNewFileName.Name = "lblNewFileName"
        Me.lblNewFileName.Size = New System.Drawing.Size(145, 17)
        Me.lblNewFileName.TabIndex = 4
        Me.lblNewFileName.Text = "FileName placeholder"
        Me.lblNewFileName.Visible = False
        '
        'btnDownloadFile
        '
        Me.btnDownloadFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDownloadFile.Enabled = False
        Me.btnDownloadFile.Location = New System.Drawing.Point(275, 340)
        Me.btnDownloadFile.Name = "btnDownloadFile"
        Me.btnDownloadFile.Size = New System.Drawing.Size(75, 23)
        Me.btnDownloadFile.TabIndex = 4
        Me.btnDownloadFile.Text = "Download"
        Me.btnDownloadFile.UseVisualStyleBackColor = True
        '
        'btnDeleteFile
        '
        Me.btnDeleteFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteFile.Enabled = False
        Me.btnDeleteFile.Location = New System.Drawing.Point(356, 340)
        Me.btnDeleteFile.Name = "btnDeleteFile"
        Me.btnDeleteFile.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteFile.TabIndex = 8
        Me.btnDeleteFile.Text = "Delete"
        Me.btnDeleteFile.UseVisualStyleBackColor = True
        '
        'txtUpdateDescription
        '
        Me.txtUpdateDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUpdateDescription.Location = New System.Drawing.Point(12, 371)
        Me.txtUpdateDescription.Name = "txtUpdateDescription"
        Me.txtUpdateDescription.Size = New System.Drawing.Size(145, 20)
        Me.txtUpdateDescription.TabIndex = 5
        Me.txtUpdateDescription.Visible = False
        '
        'btnUpdateFileDescription
        '
        Me.btnUpdateFileDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateFileDescription.AutoSize = True
        Me.btnUpdateFileDescription.Enabled = False
        Me.btnUpdateFileDescription.Location = New System.Drawing.Point(325, 369)
        Me.btnUpdateFileDescription.Name = "btnUpdateFileDescription"
        Me.btnUpdateFileDescription.Size = New System.Drawing.Size(106, 23)
        Me.btnUpdateFileDescription.TabIndex = 7
        Me.btnUpdateFileDescription.Text = "Update description"
        Me.btnUpdateFileDescription.UseVisualStyleBackColor = True
        Me.btnUpdateFileDescription.Visible = False
        '
        'lblApplicationNumber
        '
        Me.lblApplicationNumber.AutoSize = True
        Me.lblApplicationNumber.Location = New System.Drawing.Point(12, 13)
        Me.lblApplicationNumber.Name = "lblApplicationNumber"
        Me.lblApplicationNumber.Size = New System.Drawing.Size(99, 13)
        Me.lblApplicationNumber.TabIndex = 9
        Me.lblApplicationNumber.Text = "Application Number"
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Location = New System.Drawing.Point(117, 10)
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(67, 20)
        Me.txtApplicationNumber.TabIndex = 0
        '
        'btnFindApplication
        '
        Me.btnFindApplication.AutoSize = True
        Me.btnFindApplication.Location = New System.Drawing.Point(190, 8)
        Me.btnFindApplication.Name = "btnFindApplication"
        Me.btnFindApplication.Size = New System.Drawing.Size(92, 23)
        Me.btnFindApplication.TabIndex = 1
        Me.btnFindApplication.Text = "Find Application"
        Me.btnFindApplication.UseVisualStyleBackColor = True
        '
        'lblAppInfo
        '
        Me.lblAppInfo.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAppInfo.Location = New System.Drawing.Point(12, 45)
        Me.lblAppInfo.Name = "lblAppInfo"
        Me.lblAppInfo.Size = New System.Drawing.Size(270, 65)
        Me.lblAppInfo.TabIndex = 12
        Me.lblAppInfo.Text = "Application Info" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5"
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EP.SetIconAlignment(Me.lblMessage, System.Windows.Forms.ErrorIconAlignment.TopLeft)
        Me.lblMessage.Location = New System.Drawing.Point(313, 13)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(350, 26)
        Me.lblMessage.TabIndex = 13
        Me.lblMessage.Text = "Message Placeholder" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2"
        Me.lblMessage.Visible = False
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'lblAppInfo2
        '
        Me.lblAppInfo2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAppInfo2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAppInfo2.Location = New System.Drawing.Point(313, 45)
        Me.lblAppInfo2.Name = "lblAppInfo2"
        Me.lblAppInfo2.Size = New System.Drawing.Size(350, 65)
        Me.lblAppInfo2.TabIndex = 12
        Me.lblAppInfo2.Text = "Application Info" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5"
        '
        'lblSelectedFileName
        '
        Me.lblSelectedFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedFileName.AutoSize = True
        Me.lblSelectedFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedFileName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblSelectedFileName.Location = New System.Drawing.Point(12, 343)
        Me.lblSelectedFileName.Name = "lblSelectedFileName"
        Me.lblSelectedFileName.Size = New System.Drawing.Size(145, 17)
        Me.lblSelectedFileName.TabIndex = 14
        Me.lblSelectedFileName.Text = "FileName placeholder"
        Me.lblSelectedFileName.Visible = False
        '
        'ddlUpdateDocumentType
        '
        Me.ddlUpdateDocumentType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlUpdateDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlUpdateDocumentType.Enabled = False
        Me.ddlUpdateDocumentType.FormattingEnabled = True
        Me.ddlUpdateDocumentType.Location = New System.Drawing.Point(163, 371)
        Me.ddlUpdateDocumentType.Name = "ddlUpdateDocumentType"
        Me.ddlUpdateDocumentType.Size = New System.Drawing.Size(156, 21)
        Me.ddlUpdateDocumentType.TabIndex = 6
        Me.ddlUpdateDocumentType.Visible = False
        '
        'SsppFileUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(675, 404)
        Me.Controls.Add(Me.ddlUpdateDocumentType)
        Me.Controls.Add(Me.btnDeleteFile)
        Me.Controls.Add(Me.btnDownloadFile)
        Me.Controls.Add(Me.txtUpdateDescription)
        Me.Controls.Add(Me.btnUpdateFileDescription)
        Me.Controls.Add(Me.lblSelectedFileName)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblAppInfo2)
        Me.Controls.Add(Me.lblAppInfo)
        Me.Controls.Add(Me.btnFindApplication)
        Me.Controls.Add(Me.txtApplicationNumber)
        Me.Controls.Add(Me.lblApplicationNumber)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dgvFileList)
        Me.Name = "SsppFileUploader"
        Me.Text = "Permit Documents"
        CType(Me.dgvFileList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvFileList As System.Windows.Forms.DataGridView
    Friend WithEvents btnChooseNewFile As System.Windows.Forms.Button
    Friend WithEvents ddlNewDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDocumentTypes As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnNewFileCancel As System.Windows.Forms.Button
    Friend WithEvents btnNewFileUpload As System.Windows.Forms.Button
    Friend WithEvents txtNewDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblNewDescription As System.Windows.Forms.Label
    Friend WithEvents lblNewFileName As System.Windows.Forms.Label
    Friend WithEvents btnDownloadFile As System.Windows.Forms.Button
    Friend WithEvents btnDeleteFile As System.Windows.Forms.Button
    Friend WithEvents txtUpdateDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdateFileDescription As System.Windows.Forms.Button
    Friend WithEvents lblApplicationNumber As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnFindApplication As System.Windows.Forms.Button
    Friend WithEvents lblAppInfo As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblAppInfo2 As System.Windows.Forms.Label
    Friend WithEvents lblSelectedFileName As System.Windows.Forms.Label
    Friend WithEvents ddlUpdateDocumentType As System.Windows.Forms.ComboBox

End Class
