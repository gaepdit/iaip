<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSPPPublicNoticesAndAdvisories
    Inherits BaseForm

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TCPublicNotices = New System.Windows.Forms.TabControl()
        Me.tpPublish = New System.Windows.Forms.TabPage()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.rtbPreview = New System.Windows.Forms.RichTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dtpExpirationDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.btnPreviewDocument = New System.Windows.Forms.Button()
        Me.btnPublishDocument = New System.Windows.Forms.Button()
        Me.dgvApplications = New Iaip.IaipDataGridView()
        Me.lblCountDisplay = New System.Windows.Forms.Label()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.btnSelectNone = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.rdbPublicNotice = New System.Windows.Forms.RadioButton()
        Me.lblSelectedCount = New System.Windows.Forms.Label()
        Me.rdbPublicAdvisories = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtApplicationNumberEditor = New System.Windows.Forms.TextBox()
        Me.btnAddToApplicationList = New System.Windows.Forms.Button()
        Me.tpPrevious = New System.Windows.Forms.TabPage()
        Me.rtbDocument = New System.Windows.Forms.RichTextBox()
        Me.PanelSide = New System.Windows.Forms.Panel()
        Me.btnOpenDocument = New System.Windows.Forms.Button()
        Me.btnUpdateDocumentChanges = New System.Windows.Forms.Button()
        Me.btnDownloadAsPdf = New System.Windows.Forms.Button()
        Me.lblExpirationDate = New System.Windows.Forms.Label()
        Me.cboPAPNReports = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDocumentName = New System.Windows.Forms.Label()
        Me.TCPublicNotices.SuspendLayout()
        Me.tpPublish.SuspendLayout()
        Me.PanelBottom.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvApplications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTop.SuspendLayout()
        Me.tpPrevious.SuspendLayout()
        Me.PanelSide.SuspendLayout()
        Me.SuspendLayout()
        '
        'TCPublicNotices
        '
        Me.TCPublicNotices.Controls.Add(Me.tpPublish)
        Me.TCPublicNotices.Controls.Add(Me.tpPrevious)
        Me.TCPublicNotices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCPublicNotices.Location = New System.Drawing.Point(0, 0)
        Me.TCPublicNotices.Name = "TCPublicNotices"
        Me.TCPublicNotices.SelectedIndex = 0
        Me.TCPublicNotices.Size = New System.Drawing.Size(741, 700)
        Me.TCPublicNotices.TabIndex = 0
        '
        'tpPublish
        '
        Me.tpPublish.Controls.Add(Me.PanelBottom)
        Me.tpPublish.Controls.Add(Me.dgvApplications)
        Me.tpPublish.Controls.Add(Me.PanelTop)
        Me.tpPublish.Location = New System.Drawing.Point(4, 22)
        Me.tpPublish.Name = "tpPublish"
        Me.tpPublish.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPublish.Size = New System.Drawing.Size(733, 674)
        Me.tpPublish.TabIndex = 0
        Me.tpPublish.Text = "Publish PA/PN"
        Me.tpPublish.UseVisualStyleBackColor = True
        '
        'PanelBottom
        '
        Me.PanelBottom.Controls.Add(Me.rtbPreview)
        Me.PanelBottom.Controls.Add(Me.Panel3)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelBottom.Location = New System.Drawing.Point(3, 332)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(727, 339)
        Me.PanelBottom.TabIndex = 2
        '
        'rtbPreview
        '
        Me.rtbPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbPreview.Location = New System.Drawing.Point(200, 0)
        Me.rtbPreview.Name = "rtbPreview"
        Me.rtbPreview.Size = New System.Drawing.Size(527, 339)
        Me.rtbPreview.TabIndex = 1
        Me.rtbPreview.Text = ""
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dtpExpirationDate)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.lblFileName)
        Me.Panel3.Controls.Add(Me.btnPreviewDocument)
        Me.Panel3.Controls.Add(Me.btnPublishDocument)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(200, 339)
        Me.Panel3.TabIndex = 0
        '
        'dtpExpirationDate
        '
        Me.dtpExpirationDate.Checked = False
        Me.dtpExpirationDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpExpirationDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpirationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpirationDate.Location = New System.Drawing.Point(13, 31)
        Me.dtpExpirationDate.Name = "dtpExpirationDate"
        Me.dtpExpirationDate.ShowCheckBox = True
        Me.dtpExpirationDate.Size = New System.Drawing.Size(131, 21)
        Me.dtpExpirationDate.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Date PA Expires"
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(13, 168)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(49, 13)
        Me.lblFileName.TabIndex = 3
        Me.lblFileName.Text = "Filename"
        '
        'btnPreviewDocument
        '
        Me.btnPreviewDocument.Location = New System.Drawing.Point(13, 58)
        Me.btnPreviewDocument.Name = "btnPreviewDocument"
        Me.btnPreviewDocument.Size = New System.Drawing.Size(131, 45)
        Me.btnPreviewDocument.TabIndex = 1
        Me.btnPreviewDocument.Text = "Preview Document"
        Me.btnPreviewDocument.UseVisualStyleBackColor = True
        '
        'btnPublishDocument
        '
        Me.btnPublishDocument.Enabled = False
        Me.btnPublishDocument.Location = New System.Drawing.Point(13, 120)
        Me.btnPublishDocument.Name = "btnPublishDocument"
        Me.btnPublishDocument.Size = New System.Drawing.Size(131, 45)
        Me.btnPublishDocument.TabIndex = 2
        Me.btnPublishDocument.Text = "Publish PA/PN and download PDF"
        Me.btnPublishDocument.UseVisualStyleBackColor = True
        '
        'dgvApplications
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvApplications.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplications.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvApplications.LinkifyColumnByName = "App Number"
        Me.dgvApplications.Location = New System.Drawing.Point(3, 69)
        Me.dgvApplications.Name = "dgvApplications"
        Me.dgvApplications.ResultsCountLabel = Me.lblCountDisplay
        Me.dgvApplications.ResultsCountLabelFormat = "Count: {0}"
        Me.dgvApplications.Size = New System.Drawing.Size(727, 263)
        Me.dgvApplications.StandardTab = True
        Me.dgvApplications.TabIndex = 1
        '
        'lblCountDisplay
        '
        Me.lblCountDisplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCountDisplay.AutoSize = True
        Me.lblCountDisplay.Location = New System.Drawing.Point(652, 36)
        Me.lblCountDisplay.Name = "lblCountDisplay"
        Me.lblCountDisplay.Size = New System.Drawing.Size(47, 13)
        Me.lblCountDisplay.TabIndex = 6
        Me.lblCountDisplay.Text = "Count: 0"
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.btnSelectNone)
        Me.PanelTop.Controls.Add(Me.btnSelectAll)
        Me.PanelTop.Controls.Add(Me.btnRefresh)
        Me.PanelTop.Controls.Add(Me.rdbPublicNotice)
        Me.PanelTop.Controls.Add(Me.lblSelectedCount)
        Me.PanelTop.Controls.Add(Me.lblCountDisplay)
        Me.PanelTop.Controls.Add(Me.rdbPublicAdvisories)
        Me.PanelTop.Controls.Add(Me.Label1)
        Me.PanelTop.Controls.Add(Me.txtApplicationNumberEditor)
        Me.PanelTop.Controls.Add(Me.btnAddToApplicationList)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(3, 3)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(727, 66)
        Me.PanelTop.TabIndex = 0
        '
        'btnSelectNone
        '
        Me.btnSelectNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectNone.Location = New System.Drawing.Point(649, 10)
        Me.btnSelectNone.Name = "btnSelectNone"
        Me.btnSelectNone.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectNone.TabIndex = 6
        Me.btnSelectNone.Text = "Select none"
        Me.btnSelectNone.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.Location = New System.Drawing.Point(568, 10)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectAll.TabIndex = 5
        Me.btnSelectAll.Text = "Select all"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefresh.Location = New System.Drawing.Point(451, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(111, 23)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh table"
        Me.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'rdbPublicNotice
        '
        Me.rdbPublicNotice.AutoSize = True
        Me.rdbPublicNotice.Location = New System.Drawing.Point(138, 30)
        Me.rdbPublicNotice.Name = "rdbPublicNotice"
        Me.rdbPublicNotice.Size = New System.Drawing.Size(88, 17)
        Me.rdbPublicNotice.TabIndex = 2
        Me.rdbPublicNotice.TabStop = True
        Me.rdbPublicNotice.Text = "Public Notice"
        Me.rdbPublicNotice.UseVisualStyleBackColor = True
        '
        'lblSelectedCount
        '
        Me.lblSelectedCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedCount.AutoSize = True
        Me.lblSelectedCount.Location = New System.Drawing.Point(571, 36)
        Me.lblSelectedCount.Name = "lblSelectedCount"
        Me.lblSelectedCount.Size = New System.Drawing.Size(61, 13)
        Me.lblSelectedCount.TabIndex = 6
        Me.lblSelectedCount.Text = "Selected: 0"
        '
        'rdbPublicAdvisories
        '
        Me.rdbPublicAdvisories.AutoSize = True
        Me.rdbPublicAdvisories.Location = New System.Drawing.Point(138, 8)
        Me.rdbPublicAdvisories.Name = "rdbPublicAdvisories"
        Me.rdbPublicAdvisories.Size = New System.Drawing.Size(97, 17)
        Me.rdbPublicAdvisories.TabIndex = 1
        Me.rdbPublicAdvisories.TabStop = True
        Me.rdbPublicAdvisories.Text = "Public Advisory"
        Me.rdbPublicAdvisories.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Add application to table"
        '
        'txtApplicationNumberEditor
        '
        Me.txtApplicationNumberEditor.Location = New System.Drawing.Point(13, 29)
        Me.txtApplicationNumberEditor.Name = "txtApplicationNumberEditor"
        Me.txtApplicationNumberEditor.Size = New System.Drawing.Size(119, 20)
        Me.txtApplicationNumberEditor.TabIndex = 0
        '
        'btnAddToApplicationList
        '
        Me.btnAddToApplicationList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddToApplicationList.Location = New System.Drawing.Point(241, 10)
        Me.btnAddToApplicationList.Name = "btnAddToApplicationList"
        Me.btnAddToApplicationList.Size = New System.Drawing.Size(108, 39)
        Me.btnAddToApplicationList.TabIndex = 3
        Me.btnAddToApplicationList.Text = "Add to table"
        Me.btnAddToApplicationList.UseVisualStyleBackColor = True
        '
        'tpPrevious
        '
        Me.tpPrevious.Controls.Add(Me.rtbDocument)
        Me.tpPrevious.Controls.Add(Me.PanelSide)
        Me.tpPrevious.Location = New System.Drawing.Point(4, 22)
        Me.tpPrevious.Name = "tpPrevious"
        Me.tpPrevious.Size = New System.Drawing.Size(733, 674)
        Me.tpPrevious.TabIndex = 2
        Me.tpPrevious.Text = "Previous Documents"
        Me.tpPrevious.UseVisualStyleBackColor = True
        '
        'rtbDocument
        '
        Me.rtbDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbDocument.Location = New System.Drawing.Point(150, 0)
        Me.rtbDocument.Name = "rtbDocument"
        Me.rtbDocument.Size = New System.Drawing.Size(583, 674)
        Me.rtbDocument.TabIndex = 1
        Me.rtbDocument.Text = ""
        '
        'PanelSide
        '
        Me.PanelSide.Controls.Add(Me.btnOpenDocument)
        Me.PanelSide.Controls.Add(Me.btnUpdateDocumentChanges)
        Me.PanelSide.Controls.Add(Me.btnDownloadAsPdf)
        Me.PanelSide.Controls.Add(Me.lblExpirationDate)
        Me.PanelSide.Controls.Add(Me.cboPAPNReports)
        Me.PanelSide.Controls.Add(Me.Label9)
        Me.PanelSide.Controls.Add(Me.Label3)
        Me.PanelSide.Controls.Add(Me.lblDocumentName)
        Me.PanelSide.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelSide.Location = New System.Drawing.Point(0, 0)
        Me.PanelSide.Name = "PanelSide"
        Me.PanelSide.Size = New System.Drawing.Size(150, 674)
        Me.PanelSide.TabIndex = 0
        '
        'btnOpenDocument
        '
        Me.btnOpenDocument.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenDocument.Location = New System.Drawing.Point(8, 59)
        Me.btnOpenDocument.Name = "btnOpenDocument"
        Me.btnOpenDocument.Size = New System.Drawing.Size(123, 46)
        Me.btnOpenDocument.TabIndex = 1
        Me.btnOpenDocument.Text = "Open PA/PN Document"
        Me.btnOpenDocument.UseVisualStyleBackColor = True
        '
        'btnUpdateDocumentChanges
        '
        Me.btnUpdateDocumentChanges.Enabled = False
        Me.btnUpdateDocumentChanges.Location = New System.Drawing.Point(8, 306)
        Me.btnUpdateDocumentChanges.Name = "btnUpdateDocumentChanges"
        Me.btnUpdateDocumentChanges.Size = New System.Drawing.Size(123, 46)
        Me.btnUpdateDocumentChanges.TabIndex = 5
        Me.btnUpdateDocumentChanges.Text = "Update missing data in PA/PN"
        Me.btnUpdateDocumentChanges.UseVisualStyleBackColor = True
        Me.btnUpdateDocumentChanges.Visible = False
        '
        'btnDownloadAsPdf
        '
        Me.btnDownloadAsPdf.Enabled = False
        Me.btnDownloadAsPdf.Location = New System.Drawing.Point(8, 228)
        Me.btnDownloadAsPdf.Name = "btnDownloadAsPdf"
        Me.btnDownloadAsPdf.Size = New System.Drawing.Size(123, 46)
        Me.btnDownloadAsPdf.TabIndex = 4
        Me.btnDownloadAsPdf.Text = "Download as PDF"
        Me.btnDownloadAsPdf.UseVisualStyleBackColor = True
        '
        'lblExpirationDate
        '
        Me.lblExpirationDate.AutoSize = True
        Me.lblExpirationDate.Location = New System.Drawing.Point(17, 186)
        Me.lblExpirationDate.Name = "lblExpirationDate"
        Me.lblExpirationDate.Size = New System.Drawing.Size(101, 13)
        Me.lblExpirationDate.TabIndex = 3
        Me.lblExpirationDate.Text = "date PA/PN expires"
        '
        'cboPAPNReports
        '
        Me.cboPAPNReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPAPNReports.FormattingEnabled = True
        Me.cboPAPNReports.Location = New System.Drawing.Point(8, 32)
        Me.cboPAPNReports.Name = "cboPAPNReports"
        Me.cboPAPNReports.Size = New System.Drawing.Size(123, 21)
        Me.cboPAPNReports.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 173)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "PA Expiration Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Viewing:"
        '
        'lblDocumentName
        '
        Me.lblDocumentName.AutoSize = True
        Me.lblDocumentName.Location = New System.Drawing.Point(17, 147)
        Me.lblDocumentName.Name = "lblDocumentName"
        Me.lblDocumentName.Size = New System.Drawing.Size(95, 13)
        Me.lblDocumentName.TabIndex = 2
        Me.lblDocumentName.Text = "PA/PN Doc Name"
        '
        'SSPPPublicNoticesAndAdvisories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 700)
        Me.Controls.Add(Me.TCPublicNotices)
        Me.MinimumSize = New System.Drawing.Size(723, 604)
        Me.Name = "SSPPPublicNoticesAndAdvisories"
        Me.Text = "SSPP Public Notices And Advisories"
        Me.TCPublicNotices.ResumeLayout(False)
        Me.tpPublish.ResumeLayout(False)
        Me.PanelBottom.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgvApplications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        Me.tpPrevious.ResumeLayout(False)
        Me.PanelSide.ResumeLayout(False)
        Me.PanelSide.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCPublicNotices As System.Windows.Forms.TabControl
    Friend WithEvents tpPublish As System.Windows.Forms.TabPage
    Friend WithEvents btnPreviewDocument As System.Windows.Forms.Button
    Friend WithEvents btnAddToApplicationList As System.Windows.Forms.Button
    Friend WithEvents txtApplicationNumberEditor As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCountDisplay As System.Windows.Forms.Label
    Friend WithEvents tpPrevious As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboPAPNReports As System.Windows.Forms.ComboBox
    Friend WithEvents btnDownloadAsPdf As System.Windows.Forms.Button
    Friend WithEvents rtbDocument As System.Windows.Forms.RichTextBox
    Friend WithEvents lblExpirationDate As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblDocumentName As System.Windows.Forms.Label
    Friend WithEvents btnOpenDocument As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbPublicNotice As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPublicAdvisories As System.Windows.Forms.RadioButton
    Friend WithEvents btnUpdateDocumentChanges As System.Windows.Forms.Button
    Friend WithEvents dtpExpirationDate As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents btnPublishDocument As Button
    Friend WithEvents rtbPreview As RichTextBox
    Friend WithEvents dgvApplications As IaipDataGridView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSelectNone As Button
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents lblSelectedCount As Label
    Friend WithEvents lblFileName As Label
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PanelTop As Panel
    Friend WithEvents PanelSide As Panel
End Class
