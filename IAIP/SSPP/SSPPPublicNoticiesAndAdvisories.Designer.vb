<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSPPPublicNoticiesAndAdvisories
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TCPublicNotices = New System.Windows.Forms.TabControl()
        Me.TPPreview = New System.Windows.Forms.TabPage()
        Me.SCPreviewAndGenerate = New System.Windows.Forms.SplitContainer()
        Me.dgvPublicNotice = New System.Windows.Forms.DataGridView()
        Me.SCGenerate = New System.Windows.Forms.SplitContainer()
        Me.btnClearPreview = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rdbPublicNotice = New System.Windows.Forms.RadioButton()
        Me.rdbPublicAdvisories = New System.Windows.Forms.RadioButton()
        Me.btnRemoveFromApplicationList = New System.Windows.Forms.Button()
        Me.txtPreviewCount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnGeneratePublicNotice = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnAddToApplicationList = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtApplicationNumberEditor = New System.Windows.Forms.TextBox()
        Me.lsbApplicationList = New System.Windows.Forms.ListBox()
        Me.TPPublishDocument = New System.Windows.Forms.TabPage()
        Me.SCPublicNoticeTab = New System.Windows.Forms.SplitContainer()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lsbPublicNoticies = New System.Windows.Forms.ListBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lsbPublicAdvisories = New System.Windows.Forms.ListBox()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DTPPADeadline = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox()
        Me.btnPublishPDF = New System.Windows.Forms.Button()
        Me.btnOpenApplication = New System.Windows.Forms.Button()
        Me.btnGeneratePNReport = New System.Windows.Forms.Button()
        Me.txtPublicNoticeDocument = New System.Windows.Forms.RichTextBox()
        Me.CRVPublicNotices = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.TPOldDocuments = New System.Windows.Forms.TabPage()
        Me.rtbPAPNDocument2 = New System.Windows.Forms.RichTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnOpenPAPN = New System.Windows.Forms.Button()
        Me.btnSavePAPNChanges = New System.Windows.Forms.Button()
        Me.btnViewOldPDFs = New System.Windows.Forms.Button()
        Me.lblPAPNExpiresDate2 = New System.Windows.Forms.Label()
        Me.cboPAPNReports = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPAPNDocumentName = New System.Windows.Forms.Label()
        Me.TCPublicNotices.SuspendLayout()
        Me.TPPreview.SuspendLayout()
        CType(Me.SCPreviewAndGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCPreviewAndGenerate.Panel1.SuspendLayout()
        Me.SCPreviewAndGenerate.Panel2.SuspendLayout()
        Me.SCPreviewAndGenerate.SuspendLayout()
        CType(Me.dgvPublicNotice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SCGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCGenerate.Panel1.SuspendLayout()
        Me.SCGenerate.Panel2.SuspendLayout()
        Me.SCGenerate.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TPPublishDocument.SuspendLayout()
        CType(Me.SCPublicNoticeTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCPublicNoticeTab.Panel1.SuspendLayout()
        Me.SCPublicNoticeTab.Panel2.SuspendLayout()
        Me.SCPublicNoticeTab.SuspendLayout()
        Me.TPOldDocuments.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TCPublicNotices
        '
        Me.TCPublicNotices.Controls.Add(Me.TPPreview)
        Me.TCPublicNotices.Controls.Add(Me.TPPublishDocument)
        Me.TCPublicNotices.Controls.Add(Me.TPOldDocuments)
        Me.TCPublicNotices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCPublicNotices.Location = New System.Drawing.Point(0, 0)
        Me.TCPublicNotices.Name = "TCPublicNotices"
        Me.TCPublicNotices.SelectedIndex = 0
        Me.TCPublicNotices.Size = New System.Drawing.Size(652, 619)
        Me.TCPublicNotices.TabIndex = 3
        '
        'TPPreview
        '
        Me.TPPreview.Controls.Add(Me.SCPreviewAndGenerate)
        Me.TPPreview.Location = New System.Drawing.Point(4, 22)
        Me.TPPreview.Name = "TPPreview"
        Me.TPPreview.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPreview.Size = New System.Drawing.Size(644, 593)
        Me.TPPreview.TabIndex = 0
        Me.TPPreview.Text = "Add/Remove Apps to PA/PN"
        Me.TPPreview.UseVisualStyleBackColor = True
        '
        'SCPreviewAndGenerate
        '
        Me.SCPreviewAndGenerate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCPreviewAndGenerate.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SCPreviewAndGenerate.Location = New System.Drawing.Point(3, 3)
        Me.SCPreviewAndGenerate.Name = "SCPreviewAndGenerate"
        Me.SCPreviewAndGenerate.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCPreviewAndGenerate.Panel1
        '
        Me.SCPreviewAndGenerate.Panel1.Controls.Add(Me.dgvPublicNotice)
        '
        'SCPreviewAndGenerate.Panel2
        '
        Me.SCPreviewAndGenerate.Panel2.Controls.Add(Me.SCGenerate)
        Me.SCPreviewAndGenerate.Size = New System.Drawing.Size(638, 587)
        Me.SCPreviewAndGenerate.SplitterDistance = 322
        Me.SCPreviewAndGenerate.TabIndex = 2
        '
        'dgvPublicNotice
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPublicNotice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPublicNotice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPublicNotice.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPublicNotice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPublicNotice.Location = New System.Drawing.Point(0, 0)
        Me.dgvPublicNotice.Name = "dgvPublicNotice"
        Me.dgvPublicNotice.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPublicNotice.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPublicNotice.Size = New System.Drawing.Size(638, 322)
        Me.dgvPublicNotice.TabIndex = 0
        '
        'SCGenerate
        '
        Me.SCGenerate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCGenerate.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SCGenerate.Location = New System.Drawing.Point(0, 0)
        Me.SCGenerate.Name = "SCGenerate"
        '
        'SCGenerate.Panel1
        '
        Me.SCGenerate.Panel1.Controls.Add(Me.btnClearPreview)
        Me.SCGenerate.Panel1.Controls.Add(Me.Panel4)
        Me.SCGenerate.Panel1.Controls.Add(Me.btnRemoveFromApplicationList)
        Me.SCGenerate.Panel1.Controls.Add(Me.txtPreviewCount)
        Me.SCGenerate.Panel1.Controls.Add(Me.Label2)
        Me.SCGenerate.Panel1.Controls.Add(Me.btnGeneratePublicNotice)
        Me.SCGenerate.Panel1.Controls.Add(Me.btnPreview)
        Me.SCGenerate.Panel1.Controls.Add(Me.btnAddToApplicationList)
        Me.SCGenerate.Panel1.Controls.Add(Me.Label1)
        Me.SCGenerate.Panel1.Controls.Add(Me.txtApplicationNumberEditor)
        '
        'SCGenerate.Panel2
        '
        Me.SCGenerate.Panel2.Controls.Add(Me.lsbApplicationList)
        Me.SCGenerate.Size = New System.Drawing.Size(638, 261)
        Me.SCGenerate.SplitterDistance = 284
        Me.SCGenerate.TabIndex = 0
        '
        'btnClearPreview
        '
        Me.btnClearPreview.AutoSize = True
        Me.btnClearPreview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearPreview.Location = New System.Drawing.Point(144, 30)
        Me.btnClearPreview.Name = "btnClearPreview"
        Me.btnClearPreview.Size = New System.Drawing.Size(60, 23)
        Me.btnClearPreview.TabIndex = 12
        Me.btnClearPreview.Text = "Clear List"
        Me.btnClearPreview.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.rdbPublicNotice)
        Me.Panel4.Controls.Add(Me.rdbPublicAdvisories)
        Me.Panel4.Location = New System.Drawing.Point(38, 111)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(103, 46)
        Me.Panel4.TabIndex = 11
        '
        'rdbPublicNotice
        '
        Me.rdbPublicNotice.AutoSize = True
        Me.rdbPublicNotice.Location = New System.Drawing.Point(3, 26)
        Me.rdbPublicNotice.Name = "rdbPublicNotice"
        Me.rdbPublicNotice.Size = New System.Drawing.Size(88, 17)
        Me.rdbPublicNotice.TabIndex = 2
        Me.rdbPublicNotice.TabStop = True
        Me.rdbPublicNotice.Text = "Public Notice"
        Me.rdbPublicNotice.UseVisualStyleBackColor = True
        '
        'rdbPublicAdvisories
        '
        Me.rdbPublicAdvisories.AutoSize = True
        Me.rdbPublicAdvisories.Location = New System.Drawing.Point(3, 3)
        Me.rdbPublicAdvisories.Name = "rdbPublicAdvisories"
        Me.rdbPublicAdvisories.Size = New System.Drawing.Size(97, 17)
        Me.rdbPublicAdvisories.TabIndex = 1
        Me.rdbPublicAdvisories.TabStop = True
        Me.rdbPublicAdvisories.Text = "Public Advisory"
        Me.rdbPublicAdvisories.UseVisualStyleBackColor = True
        '
        'btnRemoveFromApplicationList
        '
        Me.btnRemoveFromApplicationList.AutoSize = True
        Me.btnRemoveFromApplicationList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRemoveFromApplicationList.Location = New System.Drawing.Point(38, 160)
        Me.btnRemoveFromApplicationList.Name = "btnRemoveFromApplicationList"
        Me.btnRemoveFromApplicationList.Size = New System.Drawing.Size(154, 23)
        Me.btnRemoveFromApplicationList.TabIndex = 10
        Me.btnRemoveFromApplicationList.Text = "Remove from Application List"
        Me.btnRemoveFromApplicationList.UseVisualStyleBackColor = True
        '
        'txtPreviewCount
        '
        Me.txtPreviewCount.Location = New System.Drawing.Point(49, 4)
        Me.txtPreviewCount.Name = "txtPreviewCount"
        Me.txtPreviewCount.ReadOnly = True
        Me.txtPreviewCount.Size = New System.Drawing.Size(42, 20)
        Me.txtPreviewCount.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Count:"
        '
        'btnGeneratePublicNotice
        '
        Me.btnGeneratePublicNotice.AutoSize = True
        Me.btnGeneratePublicNotice.Location = New System.Drawing.Point(5, 202)
        Me.btnGeneratePublicNotice.Name = "btnGeneratePublicNotice"
        Me.btnGeneratePublicNotice.Size = New System.Drawing.Size(179, 23)
        Me.btnGeneratePublicNotice.TabIndex = 5
        Me.btnGeneratePublicNotice.Text = "Preview Public Notice Document"
        Me.btnGeneratePublicNotice.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.AutoSize = True
        Me.btnPreview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnPreview.Location = New System.Drawing.Point(5, 30)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(133, 23)
        Me.btnPreview.TabIndex = 0
        Me.btnPreview.Text = "Preview public notice list"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnAddToApplicationList
        '
        Me.btnAddToApplicationList.AutoSize = True
        Me.btnAddToApplicationList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddToApplicationList.Location = New System.Drawing.Point(144, 83)
        Me.btnAddToApplicationList.Name = "btnAddToApplicationList"
        Me.btnAddToApplicationList.Size = New System.Drawing.Size(122, 23)
        Me.btnAddToApplicationList.TabIndex = 4
        Me.btnAddToApplicationList.Text = "Add to Application List"
        Me.btnAddToApplicationList.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Application Number"
        '
        'txtApplicationNumberEditor
        '
        Me.txtApplicationNumberEditor.Location = New System.Drawing.Point(38, 85)
        Me.txtApplicationNumberEditor.Name = "txtApplicationNumberEditor"
        Me.txtApplicationNumberEditor.Size = New System.Drawing.Size(100, 20)
        Me.txtApplicationNumberEditor.TabIndex = 3
        '
        'lsbApplicationList
        '
        Me.lsbApplicationList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsbApplicationList.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lsbApplicationList.FormattingEnabled = True
        Me.lsbApplicationList.HorizontalScrollbar = True
        Me.lsbApplicationList.ItemHeight = 14
        Me.lsbApplicationList.Location = New System.Drawing.Point(0, 0)
        Me.lsbApplicationList.Name = "lsbApplicationList"
        Me.lsbApplicationList.Size = New System.Drawing.Size(350, 261)
        Me.lsbApplicationList.TabIndex = 1
        '
        'TPPublishDocument
        '
        Me.TPPublishDocument.Controls.Add(Me.SCPublicNoticeTab)
        Me.TPPublishDocument.Location = New System.Drawing.Point(4, 22)
        Me.TPPublishDocument.Name = "TPPublishDocument"
        Me.TPPublishDocument.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPublishDocument.Size = New System.Drawing.Size(644, 593)
        Me.TPPublishDocument.TabIndex = 1
        Me.TPPublishDocument.Text = "Publish PA/PN"
        Me.TPPublishDocument.UseVisualStyleBackColor = True
        '
        'SCPublicNoticeTab
        '
        Me.SCPublicNoticeTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCPublicNoticeTab.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SCPublicNoticeTab.Location = New System.Drawing.Point(3, 3)
        Me.SCPublicNoticeTab.Name = "SCPublicNoticeTab"
        '
        'SCPublicNoticeTab.Panel1
        '
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.Label11)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.lsbPublicNoticies)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.Label10)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.lsbPublicAdvisories)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.lblFileName)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.Label5)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.DTPPADeadline)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.Label4)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.txtApplicationNumber)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.btnPublishPDF)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.btnOpenApplication)
        Me.SCPublicNoticeTab.Panel1.Controls.Add(Me.btnGeneratePNReport)
        '
        'SCPublicNoticeTab.Panel2
        '
        Me.SCPublicNoticeTab.Panel2.Controls.Add(Me.txtPublicNoticeDocument)
        Me.SCPublicNoticeTab.Panel2.Controls.Add(Me.CRVPublicNotices)
        Me.SCPublicNoticeTab.Panel2MinSize = 75
        Me.SCPublicNoticeTab.Size = New System.Drawing.Size(638, 587)
        Me.SCPublicNoticeTab.SplitterDistance = 142
        Me.SCPublicNoticeTab.SplitterWidth = 5
        Me.SCPublicNoticeTab.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 169)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Public Noticies"
        '
        'lsbPublicNoticies
        '
        Me.lsbPublicNoticies.FormattingEnabled = True
        Me.lsbPublicNoticies.Location = New System.Drawing.Point(5, 185)
        Me.lsbPublicNoticies.Name = "lsbPublicNoticies"
        Me.lsbPublicNoticies.Size = New System.Drawing.Size(123, 82)
        Me.lsbPublicNoticies.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(5, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Public Advisories"
        '
        'lsbPublicAdvisories
        '
        Me.lsbPublicAdvisories.FormattingEnabled = True
        Me.lsbPublicAdvisories.Location = New System.Drawing.Point(5, 84)
        Me.lsbPublicAdvisories.Name = "lsbPublicAdvisories"
        Me.lsbPublicAdvisories.Size = New System.Drawing.Size(123, 69)
        Me.lsbPublicAdvisories.TabIndex = 10
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(20, 464)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(72, 13)
        Me.lblFileName.TabIndex = 13
        Me.lblFileName.Text = "pdf File Name"
        Me.lblFileName.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 451)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "PA/PN File Name"
        '
        'DTPPADeadline
        '
        Me.DTPPADeadline.Checked = False
        Me.DTPPADeadline.CustomFormat = "dd-MMM-yyyy"
        Me.DTPPADeadline.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPPADeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPPADeadline.Location = New System.Drawing.Point(5, 299)
        Me.DTPPADeadline.Name = "DTPPADeadline"
        Me.DTPPADeadline.ShowCheckBox = True
        Me.DTPPADeadline.Size = New System.Drawing.Size(123, 21)
        Me.DTPPADeadline.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 283)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Date PA/PN Expires"
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Location = New System.Drawing.Point(5, 3)
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.ReadOnly = True
        Me.txtApplicationNumber.Size = New System.Drawing.Size(57, 20)
        Me.txtApplicationNumber.TabIndex = 9
        '
        'btnPublishPDF
        '
        Me.btnPublishPDF.AutoSize = True
        Me.btnPublishPDF.Location = New System.Drawing.Point(5, 382)
        Me.btnPublishPDF.Name = "btnPublishPDF"
        Me.btnPublishPDF.Size = New System.Drawing.Size(123, 53)
        Me.btnPublishPDF.TabIndex = 12
        Me.btnPublishPDF.Text = "Publish PA/PN"
        Me.btnPublishPDF.UseVisualStyleBackColor = True
        '
        'btnOpenApplication
        '
        Me.btnOpenApplication.AutoSize = True
        Me.btnOpenApplication.Location = New System.Drawing.Point(5, 29)
        Me.btnOpenApplication.Name = "btnOpenApplication"
        Me.btnOpenApplication.Size = New System.Drawing.Size(123, 23)
        Me.btnOpenApplication.TabIndex = 11
        Me.btnOpenApplication.Text = "Open Application Log"
        Me.btnOpenApplication.UseVisualStyleBackColor = True
        '
        'btnGeneratePNReport
        '
        Me.btnGeneratePNReport.AutoSize = True
        Me.btnGeneratePNReport.Location = New System.Drawing.Point(5, 340)
        Me.btnGeneratePNReport.Name = "btnGeneratePNReport"
        Me.btnGeneratePNReport.Size = New System.Drawing.Size(123, 23)
        Me.btnGeneratePNReport.TabIndex = 10
        Me.btnGeneratePNReport.Text = "Refresh PA/PN"
        Me.btnGeneratePNReport.UseVisualStyleBackColor = True
        '
        'txtPublicNoticeDocument
        '
        Me.txtPublicNoticeDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPublicNoticeDocument.Location = New System.Drawing.Point(0, 0)
        Me.txtPublicNoticeDocument.Name = "txtPublicNoticeDocument"
        Me.txtPublicNoticeDocument.Size = New System.Drawing.Size(491, 587)
        Me.txtPublicNoticeDocument.TabIndex = 9
        Me.txtPublicNoticeDocument.Text = ""
        '
        'CRVPublicNotices
        '
        Me.CRVPublicNotices.ActiveViewIndex = -1
        Me.CRVPublicNotices.AutoScroll = True
        Me.CRVPublicNotices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRVPublicNotices.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRVPublicNotices.DisplayStatusBar = False
        Me.CRVPublicNotices.DisplayToolbar = False
        Me.CRVPublicNotices.EnableDrillDown = False
        Me.CRVPublicNotices.Location = New System.Drawing.Point(54, 110)
        Me.CRVPublicNotices.Name = "CRVPublicNotices"
        Me.CRVPublicNotices.SelectionFormula = ""
        Me.CRVPublicNotices.Size = New System.Drawing.Size(524, 243)
        Me.CRVPublicNotices.TabIndex = 15
        Me.CRVPublicNotices.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CRVPublicNotices.ViewTimeSelectionFormula = ""
        Me.CRVPublicNotices.Visible = False
        '
        'TPOldDocuments
        '
        Me.TPOldDocuments.Controls.Add(Me.rtbPAPNDocument2)
        Me.TPOldDocuments.Controls.Add(Me.Panel1)
        Me.TPOldDocuments.Location = New System.Drawing.Point(4, 22)
        Me.TPOldDocuments.Name = "TPOldDocuments"
        Me.TPOldDocuments.Size = New System.Drawing.Size(644, 593)
        Me.TPOldDocuments.TabIndex = 2
        Me.TPOldDocuments.Text = "PA/PN Documents"
        Me.TPOldDocuments.UseVisualStyleBackColor = True
        '
        'rtbPAPNDocument2
        '
        Me.rtbPAPNDocument2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbPAPNDocument2.Location = New System.Drawing.Point(150, 0)
        Me.rtbPAPNDocument2.Name = "rtbPAPNDocument2"
        Me.rtbPAPNDocument2.Size = New System.Drawing.Size(494, 593)
        Me.rtbPAPNDocument2.TabIndex = 17
        Me.rtbPAPNDocument2.Text = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnOpenPAPN)
        Me.Panel1.Controls.Add(Me.btnSavePAPNChanges)
        Me.Panel1.Controls.Add(Me.btnViewOldPDFs)
        Me.Panel1.Controls.Add(Me.lblPAPNExpiresDate2)
        Me.Panel1.Controls.Add(Me.cboPAPNReports)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblPAPNDocumentName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(150, 593)
        Me.Panel1.TabIndex = 23
        '
        'btnOpenPAPN
        '
        Me.btnOpenPAPN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenPAPN.Location = New System.Drawing.Point(8, 59)
        Me.btnOpenPAPN.Name = "btnOpenPAPN"
        Me.btnOpenPAPN.Size = New System.Drawing.Size(123, 23)
        Me.btnOpenPAPN.TabIndex = 18
        Me.btnOpenPAPN.Text = "Open PA/PN"
        Me.btnOpenPAPN.UseVisualStyleBackColor = True
        '
        'btnSavePAPNChanges
        '
        Me.btnSavePAPNChanges.AutoSize = True
        Me.btnSavePAPNChanges.Location = New System.Drawing.Point(8, 292)
        Me.btnSavePAPNChanges.Name = "btnSavePAPNChanges"
        Me.btnSavePAPNChanges.Size = New System.Drawing.Size(123, 46)
        Me.btnSavePAPNChanges.TabIndex = 22
        Me.btnSavePAPNChanges.Text = "Save changes " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to PA/PN"
        Me.btnSavePAPNChanges.UseVisualStyleBackColor = True
        '
        'btnViewOldPDFs
        '
        Me.btnViewOldPDFs.AutoSize = True
        Me.btnViewOldPDFs.Location = New System.Drawing.Point(8, 211)
        Me.btnViewOldPDFs.Name = "btnViewOldPDFs"
        Me.btnViewOldPDFs.Size = New System.Drawing.Size(123, 46)
        Me.btnViewOldPDFs.TabIndex = 16
        Me.btnViewOldPDFs.Text = "Download PA/PN"
        Me.btnViewOldPDFs.UseVisualStyleBackColor = True
        '
        'lblPAPNExpiresDate2
        '
        Me.lblPAPNExpiresDate2.AutoSize = True
        Me.lblPAPNExpiresDate2.Location = New System.Drawing.Point(17, 166)
        Me.lblPAPNExpiresDate2.Name = "lblPAPNExpiresDate2"
        Me.lblPAPNExpiresDate2.Size = New System.Drawing.Size(101, 13)
        Me.lblPAPNExpiresDate2.TabIndex = 21
        Me.lblPAPNExpiresDate2.Text = "date PA/PN expires"
        '
        'cboPAPNReports
        '
        Me.cboPAPNReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPAPNReports.FormattingEnabled = True
        Me.cboPAPNReports.Location = New System.Drawing.Point(8, 32)
        Me.cboPAPNReports.Name = "cboPAPNReports"
        Me.cboPAPNReports.Size = New System.Drawing.Size(123, 21)
        Me.cboPAPNReports.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 153)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(119, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "PA/PN Expiration Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Viewing PA/PN document:"
        '
        'lblPAPNDocumentName
        '
        Me.lblPAPNDocumentName.AutoSize = True
        Me.lblPAPNDocumentName.Location = New System.Drawing.Point(17, 127)
        Me.lblPAPNDocumentName.Name = "lblPAPNDocumentName"
        Me.lblPAPNDocumentName.Size = New System.Drawing.Size(95, 13)
        Me.lblPAPNDocumentName.TabIndex = 19
        Me.lblPAPNDocumentName.Text = "PA/PN Doc Name"
        '
        'SSPPPublicNoticiesAndAdvisories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 619)
        Me.Controls.Add(Me.TCPublicNotices)
        Me.MinimumSize = New System.Drawing.Size(500, 556)
        Me.Name = "SSPPPublicNoticiesAndAdvisories"
        Me.Text = "SSPP Public Noticies And Advisories"
        Me.TCPublicNotices.ResumeLayout(False)
        Me.TPPreview.ResumeLayout(False)
        Me.SCPreviewAndGenerate.Panel1.ResumeLayout(False)
        Me.SCPreviewAndGenerate.Panel2.ResumeLayout(False)
        CType(Me.SCPreviewAndGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCPreviewAndGenerate.ResumeLayout(False)
        CType(Me.dgvPublicNotice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCGenerate.Panel1.ResumeLayout(False)
        Me.SCGenerate.Panel1.PerformLayout()
        Me.SCGenerate.Panel2.ResumeLayout(False)
        CType(Me.SCGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCGenerate.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TPPublishDocument.ResumeLayout(False)
        Me.SCPublicNoticeTab.Panel1.ResumeLayout(False)
        Me.SCPublicNoticeTab.Panel1.PerformLayout()
        Me.SCPublicNoticeTab.Panel2.ResumeLayout(False)
        CType(Me.SCPublicNoticeTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCPublicNoticeTab.ResumeLayout(False)
        Me.TPOldDocuments.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCPublicNotices As System.Windows.Forms.TabControl
    Friend WithEvents TPPreview As System.Windows.Forms.TabPage
    Friend WithEvents TPPublishDocument As System.Windows.Forms.TabPage
    Friend WithEvents dgvPublicNotice As System.Windows.Forms.DataGridView
    Friend WithEvents btnGeneratePublicNotice As System.Windows.Forms.Button
    Friend WithEvents btnAddToApplicationList As System.Windows.Forms.Button
    Friend WithEvents txtApplicationNumberEditor As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lsbApplicationList As System.Windows.Forms.ListBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents SCPublicNoticeTab As System.Windows.Forms.SplitContainer
    Friend WithEvents SCPreviewAndGenerate As System.Windows.Forms.SplitContainer
    Friend WithEvents SCGenerate As System.Windows.Forms.SplitContainer
    Friend WithEvents txtPreviewCount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOpenApplication As System.Windows.Forms.Button
    Friend WithEvents btnGeneratePNReport As System.Windows.Forms.Button
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnPublishPDF As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFromApplicationList As System.Windows.Forms.Button
    Friend WithEvents TPOldDocuments As System.Windows.Forms.TabPage
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DTPPADeadline As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboPAPNReports As System.Windows.Forms.ComboBox
    Friend WithEvents btnViewOldPDFs As System.Windows.Forms.Button
    Friend WithEvents txtPublicNoticeDocument As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbPAPNDocument2 As System.Windows.Forms.RichTextBox
    Friend WithEvents lblPAPNExpiresDate2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblPAPNDocumentName As System.Windows.Forms.Label
    Friend WithEvents btnOpenPAPN As System.Windows.Forms.Button
    Friend WithEvents lsbPublicAdvisories As System.Windows.Forms.ListBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbPublicNotice As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPublicAdvisories As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lsbPublicNoticies As System.Windows.Forms.ListBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CRVPublicNotices As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnClearPreview As System.Windows.Forms.Button
    Friend WithEvents btnSavePAPNChanges As System.Windows.Forms.Button
    Friend WithEvents Panel1 As Panel
End Class
