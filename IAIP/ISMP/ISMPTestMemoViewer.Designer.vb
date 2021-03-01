<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ISMPTestMemoViewer
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents txtReferenceNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents LLViewMemo As System.Windows.Forms.LinkLabel
    Friend WithEvents chbComplianceStatus1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceStatus5 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chbOpen As System.Windows.Forms.CheckBox
    Friend WithEvents chbClosed As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chbDelete As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LLSelectReport As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LLRunSearch As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFilterText1 As System.Windows.Forms.TextBox
    Friend WithEvents GBFilterAndSortOption As System.Windows.Forms.GroupBox
    Friend WithEvents dgrMemoViewer As IaipDataGridView
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgrMemoViewer = New Iaip.IaipDataGridView()
        Me.GBFilterAndSortOption = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chbClosed = New System.Windows.Forms.CheckBox()
        Me.chbOpen = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chbComplianceStatus5 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus4 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus3 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus2 = New System.Windows.Forms.CheckBox()
        Me.chbComplianceStatus1 = New System.Windows.Forms.CheckBox()
        Me.chbDelete = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LLViewMemo = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtReferenceNumber2 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LLSelectReport = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox()
        Me.LLRunSearch = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFilterText1 = New System.Windows.Forms.TextBox()
        CType(Me.dgrMemoViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBFilterAndSortOption.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgrMemoViewer
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgrMemoViewer.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgrMemoViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgrMemoViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrMemoViewer.LinkifyColumnByName = Nothing
        Me.dgrMemoViewer.Location = New System.Drawing.Point(0, 162)
        Me.dgrMemoViewer.Name = "dgrMemoViewer"
        Me.dgrMemoViewer.ResultsCountLabel = Nothing
        Me.dgrMemoViewer.ResultsCountLabelFormat = "{0} found"
        Me.dgrMemoViewer.Size = New System.Drawing.Size(950, 476)
        Me.dgrMemoViewer.StandardTab = True
        Me.dgrMemoViewer.TabIndex = 232
        '
        'GBFilterAndSortOption
        '
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox3)
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox4)
        Me.GBFilterAndSortOption.Controls.Add(Me.chbDelete)
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox2)
        Me.GBFilterAndSortOption.Controls.Add(Me.GroupBox1)
        Me.GBFilterAndSortOption.Controls.Add(Me.LLRunSearch)
        Me.GBFilterAndSortOption.Controls.Add(Me.Label1)
        Me.GBFilterAndSortOption.Controls.Add(Me.txtFilterText1)
        Me.GBFilterAndSortOption.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBFilterAndSortOption.Location = New System.Drawing.Point(0, 0)
        Me.GBFilterAndSortOption.Name = "GBFilterAndSortOption"
        Me.GBFilterAndSortOption.Size = New System.Drawing.Size(950, 162)
        Me.GBFilterAndSortOption.TabIndex = 236
        Me.GBFilterAndSortOption.TabStop = False
        Me.GBFilterAndSortOption.Text = "Filter Options"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.chbClosed)
        Me.GroupBox3.Controls.Add(Me.chbOpen)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 64)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(288, 56)
        Me.GroupBox3.TabIndex = 258
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Open/Closed"
        '
        'chbClosed
        '
        Me.chbClosed.Location = New System.Drawing.Point(8, 32)
        Me.chbClosed.Name = "chbClosed"
        Me.chbClosed.Size = New System.Drawing.Size(80, 16)
        Me.chbClosed.TabIndex = 244
        Me.chbClosed.Text = "Closed"
        '
        'chbOpen
        '
        Me.chbOpen.Location = New System.Drawing.Point(8, 16)
        Me.chbOpen.Name = "chbOpen"
        Me.chbOpen.Size = New System.Drawing.Size(80, 16)
        Me.chbOpen.TabIndex = 243
        Me.chbOpen.Text = "Open"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus5)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus4)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus3)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus2)
        Me.GroupBox4.Controls.Add(Me.chbComplianceStatus1)
        Me.GroupBox4.Location = New System.Drawing.Point(320, 24)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(228, 100)
        Me.GroupBox4.TabIndex = 257
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Compliance Status"
        '
        'chbComplianceStatus5
        '
        Me.chbComplianceStatus5.Location = New System.Drawing.Point(8, 80)
        Me.chbComplianceStatus5.Name = "chbComplianceStatus5"
        Me.chbComplianceStatus5.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus5.TabIndex = 246
        Me.chbComplianceStatus5.Text = "Not In Compliance"
        '
        'chbComplianceStatus4
        '
        Me.chbComplianceStatus4.Location = New System.Drawing.Point(8, 64)
        Me.chbComplianceStatus4.Name = "chbComplianceStatus4"
        Me.chbComplianceStatus4.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus4.TabIndex = 245
        Me.chbComplianceStatus4.Text = "Indeterminate"
        '
        'chbComplianceStatus3
        '
        Me.chbComplianceStatus3.Location = New System.Drawing.Point(8, 48)
        Me.chbComplianceStatus3.Name = "chbComplianceStatus3"
        Me.chbComplianceStatus3.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus3.TabIndex = 244
        Me.chbComplianceStatus3.Text = "In Compliance"
        '
        'chbComplianceStatus2
        '
        Me.chbComplianceStatus2.Location = New System.Drawing.Point(8, 32)
        Me.chbComplianceStatus2.Name = "chbComplianceStatus2"
        Me.chbComplianceStatus2.Size = New System.Drawing.Size(184, 16)
        Me.chbComplianceStatus2.TabIndex = 243
        Me.chbComplianceStatus2.Text = "For Information Purpose Only"
        '
        'chbComplianceStatus1
        '
        Me.chbComplianceStatus1.Location = New System.Drawing.Point(8, 16)
        Me.chbComplianceStatus1.Name = "chbComplianceStatus1"
        Me.chbComplianceStatus1.Size = New System.Drawing.Size(120, 16)
        Me.chbComplianceStatus1.TabIndex = 242
        Me.chbComplianceStatus1.Text = "File Open"
        '
        'chbDelete
        '
        Me.chbDelete.Location = New System.Drawing.Point(328, 128)
        Me.chbDelete.Name = "chbDelete"
        Me.chbDelete.Size = New System.Drawing.Size(104, 16)
        Me.chbDelete.TabIndex = 254
        Me.chbDelete.Text = "Deleted Record"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LLViewMemo)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtReferenceNumber2)
        Me.GroupBox2.Location = New System.Drawing.Point(568, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(224, 72)
        Me.GroupBox2.TabIndex = 253
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "View Memo Only"
        '
        'LLViewMemo
        '
        Me.LLViewMemo.AutoSize = True
        Me.LLViewMemo.Location = New System.Drawing.Point(136, 42)
        Me.LLViewMemo.Name = "LLViewMemo"
        Me.LLViewMemo.Size = New System.Drawing.Size(62, 13)
        Me.LLViewMemo.TabIndex = 250
        Me.LLViewMemo.TabStop = True
        Me.LLViewMemo.Text = "View Memo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 249
        Me.Label4.Text = "Reference Number"
        '
        'txtReferenceNumber2
        '
        Me.txtReferenceNumber2.Location = New System.Drawing.Point(24, 40)
        Me.txtReferenceNumber2.Name = "txtReferenceNumber2"
        Me.txtReferenceNumber2.Size = New System.Drawing.Size(100, 20)
        Me.txtReferenceNumber2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LLSelectReport)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtReferenceNumber)
        Me.GroupBox1.Location = New System.Drawing.Point(568, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 72)
        Me.GroupBox1.TabIndex = 252
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Test Report"
        '
        'LLSelectReport
        '
        Me.LLSelectReport.AutoSize = True
        Me.LLSelectReport.Location = New System.Drawing.Point(136, 42)
        Me.LLSelectReport.Name = "LLSelectReport"
        Me.LLSelectReport.Size = New System.Drawing.Size(72, 13)
        Me.LLSelectReport.TabIndex = 250
        Me.LLSelectReport.TabStop = True
        Me.LLSelectReport.Text = "Select Report"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 249
        Me.Label3.Text = "Reference Number"
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(24, 40)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtReferenceNumber.TabIndex = 0
        '
        'LLRunSearch
        '
        Me.LLRunSearch.AutoSize = True
        Me.LLRunSearch.Location = New System.Drawing.Point(8, 128)
        Me.LLRunSearch.Name = "LLRunSearch"
        Me.LLRunSearch.Size = New System.Drawing.Size(193, 13)
        Me.LLRunSearch.TabIndex = 251
        Me.LLRunSearch.TabStop = True
        Me.LLRunSearch.Text = "Run Search with Filter and Sort Options"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 248
        Me.Label1.Text = "Memo Text"
        '
        'txtFilterText1
        '
        Me.txtFilterText1.Location = New System.Drawing.Point(48, 40)
        Me.txtFilterText1.Name = "txtFilterText1"
        Me.txtFilterText1.Size = New System.Drawing.Size(248, 20)
        Me.txtFilterText1.TabIndex = 247
        '
        'ISMPTestMemoViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(950, 638)
        Me.Controls.Add(Me.dgrMemoViewer)
        Me.Controls.Add(Me.GBFilterAndSortOption)
        Me.MinimumSize = New System.Drawing.Size(819, 330)
        Me.Name = "ISMPTestMemoViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Test Memo Viewer"
        CType(Me.dgrMemoViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBFilterAndSortOption.ResumeLayout(False)
        Me.GBFilterAndSortOption.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
End Class
