<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPStaffReports
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnRunReport = New System.Windows.Forms.Button()
        Me.rdbUnitStatsAll = New System.Windows.Forms.RadioButton()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.rdbUnitDateCompleted = New System.Windows.Forms.RadioButton()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.rdbUnitDateTestStarted = New System.Windows.Forms.RadioButton()
        Me.rdbUnitDateReceived = New System.Windows.Forms.RadioButton()
        Me.DTPUnitEnd = New System.Windows.Forms.DateTimePicker()
        Me.DTPUnitStart = New System.Windows.Forms.DateTimePicker()
        Me.txtEngineerStatistics = New System.Windows.Forms.TextBox()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnRunReport)
        Me.Panel4.Controls.Add(Me.rdbUnitStatsAll)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.rdbUnitDateCompleted)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.rdbUnitDateTestStarted)
        Me.Panel4.Controls.Add(Me.rdbUnitDateReceived)
        Me.Panel4.Controls.Add(Me.DTPUnitEnd)
        Me.Panel4.Controls.Add(Me.DTPUnitStart)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(700, 138)
        Me.Panel4.TabIndex = 145
        '
        'btnRunReport
        '
        Me.btnRunReport.AutoSize = True
        Me.btnRunReport.Location = New System.Drawing.Point(346, 82)
        Me.btnRunReport.Name = "btnRunReport"
        Me.btnRunReport.Size = New System.Drawing.Size(100, 23)
        Me.btnRunReport.TabIndex = 153
        Me.btnRunReport.Text = "Run Report"
        Me.btnRunReport.UseVisualStyleBackColor = True
        '
        'rdbUnitStatsAll
        '
        Me.rdbUnitStatsAll.AutoSize = True
        Me.rdbUnitStatsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitStatsAll.Location = New System.Drawing.Point(12, 85)
        Me.rdbUnitStatsAll.Name = "rdbUnitStatsAll"
        Me.rdbUnitStatsAll.Size = New System.Drawing.Size(281, 17)
        Me.rdbUnitStatsAll.TabIndex = 152
        Me.rdbUnitStatsAll.Text = "All Test Reports (All Dates - Excluding SM23 Archives)"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(463, 18)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(55, 13)
        Me.Label29.TabIndex = 150
        Me.Label29.Text = "End Date "
        '
        'rdbUnitDateCompleted
        '
        Me.rdbUnitDateCompleted.AutoSize = True
        Me.rdbUnitDateCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateCompleted.Location = New System.Drawing.Point(12, 62)
        Me.rdbUnitDateCompleted.Name = "rdbUnitDateCompleted"
        Me.rdbUnitDateCompleted.Size = New System.Drawing.Size(136, 17)
        Me.rdbUnitDateCompleted.TabIndex = 2
        Me.rdbUnitDateCompleted.Text = "Date Report Completed"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(343, 18)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(55, 13)
        Me.Label28.TabIndex = 149
        Me.Label28.Text = "Start Date"
        '
        'rdbUnitDateTestStarted
        '
        Me.rdbUnitDateTestStarted.AutoSize = True
        Me.rdbUnitDateTestStarted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateTestStarted.Location = New System.Drawing.Point(12, 16)
        Me.rdbUnitDateTestStarted.Name = "rdbUnitDateTestStarted"
        Me.rdbUnitDateTestStarted.Size = New System.Drawing.Size(109, 17)
        Me.rdbUnitDateTestStarted.TabIndex = 1
        Me.rdbUnitDateTestStarted.Text = "Date Test Started"
        '
        'rdbUnitDateReceived
        '
        Me.rdbUnitDateReceived.AutoSize = True
        Me.rdbUnitDateReceived.Checked = True
        Me.rdbUnitDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateReceived.Location = New System.Drawing.Point(12, 39)
        Me.rdbUnitDateReceived.Name = "rdbUnitDateReceived"
        Me.rdbUnitDateReceived.Size = New System.Drawing.Size(97, 17)
        Me.rdbUnitDateReceived.TabIndex = 0
        Me.rdbUnitDateReceived.TabStop = True
        Me.rdbUnitDateReceived.Text = "Date Received"
        '
        'DTPUnitEnd
        '
        Me.DTPUnitEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitEnd.Location = New System.Drawing.Point(466, 34)
        Me.DTPUnitEnd.Name = "DTPUnitEnd"
        Me.DTPUnitEnd.Size = New System.Drawing.Size(100, 20)
        Me.DTPUnitEnd.TabIndex = 148
        Me.DTPUnitEnd.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'DTPUnitStart
        '
        Me.DTPUnitStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitStart.Location = New System.Drawing.Point(346, 34)
        Me.DTPUnitStart.Name = "DTPUnitStart"
        Me.DTPUnitStart.Size = New System.Drawing.Size(100, 20)
        Me.DTPUnitStart.TabIndex = 147
        Me.DTPUnitStart.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'txtEngineerStatistics
        '
        Me.txtEngineerStatistics.AcceptsReturn = True
        Me.txtEngineerStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEngineerStatistics.Location = New System.Drawing.Point(0, 138)
        Me.txtEngineerStatistics.Multiline = True
        Me.txtEngineerStatistics.Name = "txtEngineerStatistics"
        Me.txtEngineerStatistics.ReadOnly = True
        Me.txtEngineerStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEngineerStatistics.Size = New System.Drawing.Size(700, 350)
        Me.txtEngineerStatistics.TabIndex = 157
        '
        'ISMPStaffReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 488)
        Me.Controls.Add(Me.txtEngineerStatistics)
        Me.Controls.Add(Me.Panel4)
        Me.MinimumSize = New System.Drawing.Size(595, 393)
        Me.Name = "ISMPStaffReports"
        Me.Text = "ISMP Staff Reports"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbUnitStatsAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateTestStarted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents DTPUnitEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPUnitStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtEngineerStatistics As System.Windows.Forms.TextBox
    Friend WithEvents btnRunReport As Button
End Class
