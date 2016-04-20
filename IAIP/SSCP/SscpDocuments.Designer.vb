<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SscpDocuments
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
        Me.components = New System.ComponentModel.Container()
        Me.btnNewDocumentCancel = New System.Windows.Forms.Button()
        Me.btnNewDocumentUpload = New System.Windows.Forms.Button()
        Me.btnNewDocumentSelect = New System.Windows.Forms.Button()
        Me.lblNewDocumentType = New System.Windows.Forms.Label()
        Me.ddlNewDocumentType = New System.Windows.Forms.ComboBox()
        Me.lblNewDocumentDescription = New System.Windows.Forms.Label()
        Me.pnlNewDocument = New System.Windows.Forms.Panel()
        Me.pnlNewDocumentDetails = New System.Windows.Forms.Panel()
        Me.txtNewDocumentDescription = New System.Windows.Forms.TextBox()
        Me.lblNewDocumentName = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.pnlDocument = New System.Windows.Forms.Panel()
        Me.lblDocumentDescription = New System.Windows.Forms.Label()
        Me.ddlDocumentType = New System.Windows.Forms.ComboBox()
        Me.btnDocumentDelete = New System.Windows.Forms.Button()
        Me.btnDocumentDownload = New System.Windows.Forms.Button()
        Me.txtDocumentDescription = New System.Windows.Forms.TextBox()
        Me.btnDocumentUpdate = New System.Windows.Forms.Button()
        Me.lblDocumentName = New System.Windows.Forms.Label()
        Me.lblCurrentFiles = New System.Windows.Forms.Label()
        Me.dgvDocumentList = New System.Windows.Forms.DataGridView()
        Me.btnFindEnforcement = New System.Windows.Forms.Button()
        Me.txtFindEnforcement = New System.Windows.Forms.TextBox()
        Me.lblFindEnforcement = New System.Windows.Forms.Label()
        Me.lblEnforcementInfo = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lblEnforcementInfo2 = New System.Windows.Forms.Label()
        Me.pnlNewDocument.SuspendLayout()
        Me.pnlNewDocumentDetails.SuspendLayout()
        Me.pnlDocument.SuspendLayout()
        CType(Me.dgvDocumentList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNewDocumentCancel
        '
        Me.btnNewDocumentCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewDocumentCancel.Location = New System.Drawing.Point(164, 76)
        Me.btnNewDocumentCancel.Name = "btnNewDocumentCancel"
        Me.btnNewDocumentCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnNewDocumentCancel.TabIndex = 2
        Me.btnNewDocumentCancel.Text = "Cancel"
        Me.btnNewDocumentCancel.UseVisualStyleBackColor = True
        '
        'btnNewDocumentUpload
        '
        Me.btnNewDocumentUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewDocumentUpload.Location = New System.Drawing.Point(83, 76)
        Me.btnNewDocumentUpload.Name = "btnNewDocumentUpload"
        Me.btnNewDocumentUpload.Size = New System.Drawing.Size(75, 23)
        Me.btnNewDocumentUpload.TabIndex = 1
        Me.btnNewDocumentUpload.Text = "Add this file"
        Me.btnNewDocumentUpload.UseVisualStyleBackColor = True
        '
        'btnNewDocumentSelect
        '
        Me.btnNewDocumentSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewDocumentSelect.Location = New System.Drawing.Point(164, 27)
        Me.btnNewDocumentSelect.Name = "btnNewDocumentSelect"
        Me.btnNewDocumentSelect.Size = New System.Drawing.Size(75, 23)
        Me.btnNewDocumentSelect.TabIndex = 1
        Me.btnNewDocumentSelect.Text = "Select file"
        Me.btnNewDocumentSelect.UseVisualStyleBackColor = True
        '
        'lblNewDocumentType
        '
        Me.lblNewDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewDocumentType.AutoSize = True
        Me.lblNewDocumentType.Location = New System.Drawing.Point(3, 3)
        Me.lblNewDocumentType.Name = "lblNewDocumentType"
        Me.lblNewDocumentType.Size = New System.Drawing.Size(49, 13)
        Me.lblNewDocumentType.TabIndex = 3
        Me.lblNewDocumentType.Text = "Add new"
        '
        'ddlNewDocumentType
        '
        Me.ddlNewDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlNewDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlNewDocumentType.FormattingEnabled = True
        Me.ddlNewDocumentType.Location = New System.Drawing.Point(58, 0)
        Me.ddlNewDocumentType.Name = "ddlNewDocumentType"
        Me.ddlNewDocumentType.Size = New System.Drawing.Size(181, 21)
        Me.ddlNewDocumentType.TabIndex = 0
        '
        'lblNewDocumentDescription
        '
        Me.lblNewDocumentDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewDocumentDescription.AutoSize = True
        Me.lblNewDocumentDescription.Location = New System.Drawing.Point(4, 34)
        Me.lblNewDocumentDescription.Name = "lblNewDocumentDescription"
        Me.lblNewDocumentDescription.Size = New System.Drawing.Size(106, 13)
        Me.lblNewDocumentDescription.TabIndex = 5
        Me.lblNewDocumentDescription.Text = "Description (optional)"
        '
        'pnlNewDocument
        '
        Me.pnlNewDocument.Controls.Add(Me.pnlNewDocumentDetails)
        Me.pnlNewDocument.Controls.Add(Me.btnNewDocumentSelect)
        Me.pnlNewDocument.Controls.Add(Me.lblNewDocumentType)
        Me.pnlNewDocument.Controls.Add(Me.ddlNewDocumentType)
        Me.pnlNewDocument.Location = New System.Drawing.Point(12, 118)
        Me.pnlNewDocument.MinimumSize = New System.Drawing.Size(242, 172)
        Me.pnlNewDocument.Name = "pnlNewDocument"
        Me.pnlNewDocument.Size = New System.Drawing.Size(242, 172)
        Me.pnlNewDocument.TabIndex = 2
        '
        'pnlNewDocumentDetails
        '
        Me.pnlNewDocumentDetails.Controls.Add(Me.btnNewDocumentCancel)
        Me.pnlNewDocumentDetails.Controls.Add(Me.btnNewDocumentUpload)
        Me.pnlNewDocumentDetails.Controls.Add(Me.txtNewDocumentDescription)
        Me.pnlNewDocumentDetails.Controls.Add(Me.lblNewDocumentDescription)
        Me.pnlNewDocumentDetails.Controls.Add(Me.lblNewDocumentName)
        Me.pnlNewDocumentDetails.Enabled = False
        Me.pnlNewDocumentDetails.Location = New System.Drawing.Point(0, 56)
        Me.pnlNewDocumentDetails.Name = "pnlNewDocumentDetails"
        Me.pnlNewDocumentDetails.Size = New System.Drawing.Size(242, 116)
        Me.pnlNewDocumentDetails.TabIndex = 2
        Me.pnlNewDocumentDetails.Visible = False
        '
        'txtNewDocumentDescription
        '
        Me.txtNewDocumentDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewDocumentDescription.Location = New System.Drawing.Point(7, 50)
        Me.txtNewDocumentDescription.Name = "txtNewDocumentDescription"
        Me.txtNewDocumentDescription.Size = New System.Drawing.Size(233, 20)
        Me.txtNewDocumentDescription.TabIndex = 0
        '
        'lblNewDocumentName
        '
        Me.lblNewDocumentName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewDocumentName.AutoSize = True
        Me.lblNewDocumentName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewDocumentName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblNewDocumentName.Location = New System.Drawing.Point(4, 4)
        Me.lblNewDocumentName.Name = "lblNewDocumentName"
        Me.lblNewDocumentName.Size = New System.Drawing.Size(145, 17)
        Me.lblNewDocumentName.TabIndex = 4
        Me.lblNewDocumentName.Text = "FileName placeholder"
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.Color.LightYellow
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Location = New System.Drawing.Point(9, 43)
        Me.lblMessage.MaximumSize = New System.Drawing.Size(360, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(5)
        Me.lblMessage.Size = New System.Drawing.Size(119, 23)
        Me.lblMessage.TabIndex = 22
        Me.lblMessage.Text = "Message Placeholder"
        Me.lblMessage.Visible = False
        '
        'pnlDocument
        '
        Me.pnlDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDocument.Controls.Add(Me.lblDocumentDescription)
        Me.pnlDocument.Controls.Add(Me.ddlDocumentType)
        Me.pnlDocument.Controls.Add(Me.btnDocumentDelete)
        Me.pnlDocument.Controls.Add(Me.btnDocumentDownload)
        Me.pnlDocument.Controls.Add(Me.txtDocumentDescription)
        Me.pnlDocument.Controls.Add(Me.btnDocumentUpdate)
        Me.pnlDocument.Controls.Add(Me.lblDocumentName)
        Me.pnlDocument.Enabled = False
        Me.pnlDocument.Location = New System.Drawing.Point(277, 363)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Size = New System.Drawing.Size(552, 73)
        Me.pnlDocument.TabIndex = 4
        Me.pnlDocument.Visible = False
        '
        'lblDocumentDescription
        '
        Me.lblDocumentDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentDescription.AutoSize = True
        Me.lblDocumentDescription.Location = New System.Drawing.Point(0, 36)
        Me.lblDocumentDescription.Name = "lblDocumentDescription"
        Me.lblDocumentDescription.Size = New System.Drawing.Size(106, 13)
        Me.lblDocumentDescription.TabIndex = 6
        Me.lblDocumentDescription.Text = "Description (optional)"
        '
        'ddlDocumentType
        '
        Me.ddlDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlDocumentType.FormattingEnabled = True
        Me.ddlDocumentType.Location = New System.Drawing.Point(299, 52)
        Me.ddlDocumentType.Name = "ddlDocumentType"
        Me.ddlDocumentType.Size = New System.Drawing.Size(141, 21)
        Me.ddlDocumentType.TabIndex = 3
        '
        'btnDocumentDelete
        '
        Me.btnDocumentDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDocumentDelete.Location = New System.Drawing.Point(477, 7)
        Me.btnDocumentDelete.Name = "btnDocumentDelete"
        Me.btnDocumentDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDocumentDelete.TabIndex = 1
        Me.btnDocumentDelete.Text = "Delete"
        Me.btnDocumentDelete.UseVisualStyleBackColor = True
        '
        'btnDocumentDownload
        '
        Me.btnDocumentDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDocumentDownload.Location = New System.Drawing.Point(396, 7)
        Me.btnDocumentDownload.Name = "btnDocumentDownload"
        Me.btnDocumentDownload.Size = New System.Drawing.Size(75, 23)
        Me.btnDocumentDownload.TabIndex = 0
        Me.btnDocumentDownload.Text = "Download"
        Me.btnDocumentDownload.UseVisualStyleBackColor = True
        '
        'txtDocumentDescription
        '
        Me.txtDocumentDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDocumentDescription.Location = New System.Drawing.Point(0, 52)
        Me.txtDocumentDescription.Name = "txtDocumentDescription"
        Me.txtDocumentDescription.Size = New System.Drawing.Size(293, 20)
        Me.txtDocumentDescription.TabIndex = 2
        '
        'btnDocumentUpdate
        '
        Me.btnDocumentUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDocumentUpdate.AutoSize = True
        Me.btnDocumentUpdate.Location = New System.Drawing.Point(446, 50)
        Me.btnDocumentUpdate.Name = "btnDocumentUpdate"
        Me.btnDocumentUpdate.Size = New System.Drawing.Size(106, 23)
        Me.btnDocumentUpdate.TabIndex = 4
        Me.btnDocumentUpdate.Text = "Update description"
        Me.btnDocumentUpdate.UseVisualStyleBackColor = True
        '
        'lblDocumentName
        '
        Me.lblDocumentName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentName.AutoSize = True
        Me.lblDocumentName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentName.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblDocumentName.Location = New System.Drawing.Point(0, 10)
        Me.lblDocumentName.Name = "lblDocumentName"
        Me.lblDocumentName.Size = New System.Drawing.Size(145, 17)
        Me.lblDocumentName.TabIndex = 14
        Me.lblDocumentName.Text = "FileName placeholder"
        '
        'lblCurrentFiles
        '
        Me.lblCurrentFiles.AutoSize = True
        Me.lblCurrentFiles.Location = New System.Drawing.Point(278, 105)
        Me.lblCurrentFiles.Name = "lblCurrentFiles"
        Me.lblCurrentFiles.Size = New System.Drawing.Size(98, 13)
        Me.lblCurrentFiles.TabIndex = 20
        Me.lblCurrentFiles.Text = "Current Documents"
        '
        'dgvDocumentList
        '
        Me.dgvDocumentList.AllowUserToAddRows = False
        Me.dgvDocumentList.AllowUserToDeleteRows = False
        Me.dgvDocumentList.AllowUserToOrderColumns = True
        Me.dgvDocumentList.AllowUserToResizeRows = False
        Me.dgvDocumentList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDocumentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDocumentList.Enabled = False
        Me.dgvDocumentList.Location = New System.Drawing.Point(277, 121)
        Me.dgvDocumentList.MinimumSize = New System.Drawing.Size(300, 55)
        Me.dgvDocumentList.MultiSelect = False
        Me.dgvDocumentList.Name = "dgvDocumentList"
        Me.dgvDocumentList.ReadOnly = True
        Me.dgvDocumentList.RowHeadersVisible = False
        Me.dgvDocumentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDocumentList.Size = New System.Drawing.Size(552, 236)
        Me.dgvDocumentList.StandardTab = True
        Me.dgvDocumentList.TabIndex = 3
        '
        'btnFindEnforcement
        '
        Me.btnFindEnforcement.AutoSize = True
        Me.btnFindEnforcement.Location = New System.Drawing.Point(198, 10)
        Me.btnFindEnforcement.Name = "btnFindEnforcement"
        Me.btnFindEnforcement.Size = New System.Drawing.Size(100, 23)
        Me.btnFindEnforcement.TabIndex = 1
        Me.btnFindEnforcement.Text = "Find Enforcement"
        Me.btnFindEnforcement.UseVisualStyleBackColor = True
        '
        'txtFindEnforcement
        '
        Me.txtFindEnforcement.Location = New System.Drawing.Point(125, 12)
        Me.txtFindEnforcement.Name = "txtFindEnforcement"
        Me.txtFindEnforcement.Size = New System.Drawing.Size(67, 20)
        Me.txtFindEnforcement.TabIndex = 0
        '
        'lblFindEnforcement
        '
        Me.lblFindEnforcement.AutoSize = True
        Me.lblFindEnforcement.Location = New System.Drawing.Point(12, 15)
        Me.lblFindEnforcement.Name = "lblFindEnforcement"
        Me.lblFindEnforcement.Size = New System.Drawing.Size(107, 13)
        Me.lblFindEnforcement.TabIndex = 25
        Me.lblFindEnforcement.Text = "Enforcement Number"
        '
        'lblEnforcementInfo
        '
        Me.lblEnforcementInfo.AutoSize = True
        Me.lblEnforcementInfo.Location = New System.Drawing.Point(393, 15)
        Me.lblEnforcementInfo.Name = "lblEnforcementInfo"
        Me.lblEnforcementInfo.Size = New System.Drawing.Size(88, 26)
        Me.lblEnforcementInfo.TabIndex = 26
        Me.lblEnforcementInfo.Text = "Enforcement Info" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2"
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'lblEnforcementInfo2
        '
        Me.lblEnforcementInfo2.AutoSize = True
        Me.lblEnforcementInfo2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblEnforcementInfo2.Location = New System.Drawing.Point(393, 43)
        Me.lblEnforcementInfo2.MaximumSize = New System.Drawing.Size(367, 0)
        Me.lblEnforcementInfo2.Name = "lblEnforcementInfo2"
        Me.lblEnforcementInfo2.Size = New System.Drawing.Size(97, 52)
        Me.lblEnforcementInfo2.TabIndex = 26
        Me.lblEnforcementInfo2.Text = "Enforcement Info II" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4"
        '
        'SscpDocuments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 448)
        Me.Controls.Add(Me.lblEnforcementInfo2)
        Me.Controls.Add(Me.lblEnforcementInfo)
        Me.Controls.Add(Me.btnFindEnforcement)
        Me.Controls.Add(Me.txtFindEnforcement)
        Me.Controls.Add(Me.lblFindEnforcement)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pnlDocument)
        Me.Controls.Add(Me.lblCurrentFiles)
        Me.Controls.Add(Me.dgvDocumentList)
        Me.Controls.Add(Me.pnlNewDocument)
        Me.MinimumSize = New System.Drawing.Size(605, 328)
        Me.Name = "SscpDocuments"
        Me.Text = "SSCP Enforcement Documents"
        Me.pnlNewDocument.ResumeLayout(False)
        Me.pnlNewDocument.PerformLayout()
        Me.pnlNewDocumentDetails.ResumeLayout(False)
        Me.pnlNewDocumentDetails.PerformLayout()
        Me.pnlDocument.ResumeLayout(False)
        Me.pnlDocument.PerformLayout()
        CType(Me.dgvDocumentList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNewDocumentCancel As System.Windows.Forms.Button
    Friend WithEvents btnNewDocumentUpload As System.Windows.Forms.Button
    Friend WithEvents btnNewDocumentSelect As System.Windows.Forms.Button
    Friend WithEvents lblNewDocumentType As System.Windows.Forms.Label
    Friend WithEvents ddlNewDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblNewDocumentDescription As System.Windows.Forms.Label
    Friend WithEvents pnlNewDocument As System.Windows.Forms.Panel
    Friend WithEvents txtNewDocumentDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblNewDocumentName As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Friend WithEvents lblDocumentDescription As System.Windows.Forms.Label
    Friend WithEvents ddlDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents btnDocumentDelete As System.Windows.Forms.Button
    Friend WithEvents btnDocumentDownload As System.Windows.Forms.Button
    Friend WithEvents txtDocumentDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnDocumentUpdate As System.Windows.Forms.Button
    Friend WithEvents lblDocumentName As System.Windows.Forms.Label
    Friend WithEvents lblCurrentFiles As System.Windows.Forms.Label
    Friend WithEvents dgvDocumentList As System.Windows.Forms.DataGridView
    Friend WithEvents btnFindEnforcement As System.Windows.Forms.Button
    Friend WithEvents txtFindEnforcement As System.Windows.Forms.TextBox
    Friend WithEvents lblFindEnforcement As System.Windows.Forms.Label
    Friend WithEvents lblEnforcementInfo As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents pnlNewDocumentDetails As System.Windows.Forms.Panel
    Friend WithEvents lblEnforcementInfo2 As System.Windows.Forms.Label
End Class
