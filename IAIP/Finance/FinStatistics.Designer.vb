<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FinStatistics
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
        Me.btnRunReport = New System.Windows.Forms.Button()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dgvResults = New Iaip.IaipDataGridView()
        Me.lblResults = New System.Windows.Forms.Label()
        Me.cmbReportType = New System.Windows.Forms.ComboBox()
        Me.grpDateBias = New System.Windows.Forms.GroupBox()
        Me.chkApplyDates = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDateBias.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRunReport
        '
        Me.btnRunReport.AutoSize = True
        Me.btnRunReport.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnRunReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRunReport.Location = New System.Drawing.Point(32, 231)
        Me.btnRunReport.Name = "btnRunReport"
        Me.btnRunReport.Size = New System.Drawing.Size(182, 27)
        Me.btnRunReport.TabIndex = 2
        Me.btnRunReport.Text = "Run Report"
        Me.btnRunReport.UseVisualStyleBackColor = True
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = False
        Me.dtpStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpStartDate.Enabled = False
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(97, 65)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(102, 20)
        Me.dtpStartDate.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Start Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "End Date"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Checked = False
        Me.dtpEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndDate.Enabled = False
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(97, 91)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(102, 20)
        Me.dtpEndDate.TabIndex = 2
        '
        'dgvResults
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.LinkifyColumnByName = Nothing
        Me.dgvResults.Location = New System.Drawing.Point(250, 40)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.ResultsCountLabel = Me.lblResults
        Me.dgvResults.ResultsCountLabelFormat = "{0} found"
        Me.dgvResults.Size = New System.Drawing.Size(548, 542)
        Me.dgvResults.StandardTab = True
        Me.dgvResults.TabIndex = 3
        '
        'lblResults
        '
        Me.lblResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResults.AutoSize = True
        Me.lblResults.Location = New System.Drawing.Point(722, 24)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(73, 13)
        Me.lblResults.TabIndex = 5
        Me.lblResults.Text = "Results Count"
        '
        'cmbReportType
        '
        Me.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReportType.FormattingEnabled = True
        Me.cmbReportType.Location = New System.Drawing.Point(15, 40)
        Me.cmbReportType.Name = "cmbReportType"
        Me.cmbReportType.Size = New System.Drawing.Size(218, 21)
        Me.cmbReportType.TabIndex = 0
        '
        'grpDateBias
        '
        Me.grpDateBias.Controls.Add(Me.chkApplyDates)
        Me.grpDateBias.Controls.Add(Me.dtpStartDate)
        Me.grpDateBias.Controls.Add(Me.Label4)
        Me.grpDateBias.Controls.Add(Me.Label1)
        Me.grpDateBias.Controls.Add(Me.dtpEndDate)
        Me.grpDateBias.Location = New System.Drawing.Point(15, 80)
        Me.grpDateBias.Name = "grpDateBias"
        Me.grpDateBias.Size = New System.Drawing.Size(218, 132)
        Me.grpDateBias.TabIndex = 1
        Me.grpDateBias.TabStop = False
        Me.grpDateBias.Text = "Date Range"
        '
        'chkApplyDates
        '
        Me.chkApplyDates.AutoSize = True
        Me.chkApplyDates.Location = New System.Drawing.Point(17, 31)
        Me.chkApplyDates.Name = "chkApplyDates"
        Me.chkApplyDates.Size = New System.Drawing.Size(88, 17)
        Me.chkApplyDates.TabIndex = 0
        Me.chkApplyDates.Text = "Filter by Date"
        Me.chkApplyDates.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Report Type"
        '
        'FinStatistics
        '
        Me.AcceptButton = Me.btnRunReport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(810, 594)
        Me.Controls.Add(Me.lblResults)
        Me.Controls.Add(Me.grpDateBias)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbReportType)
        Me.Controls.Add(Me.dgvResults)
        Me.Controls.Add(Me.btnRunReport)
        Me.Name = "FinStatistics"
        Me.Text = "Permit Application Fee Statistics"
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDateBias.ResumeLayout(False)
        Me.grpDateBias.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRunReport As System.Windows.Forms.Button
    Friend WithEvents dtpStartDate As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpEndDate As DateTimePicker
    Friend WithEvents dgvResults As IaipDataGridView
    Friend WithEvents cmbReportType As ComboBox
    Friend WithEvents grpDateBias As GroupBox
    Friend WithEvents chkApplyDates As CheckBox
    Friend WithEvents lblResults As Label
    Friend WithEvents Label2 As Label
End Class
