<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPFacilitySummaryPrint
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPFacilitySummaryPrint))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.btnRunReport = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.DTPExtendedPrintEndDate = New System.Windows.Forms.DateTimePicker
        Me.DTPExtendedPrintStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DTPFullPrintEndDate = New System.Windows.Forms.DateTimePicker
        Me.DTPFullPrintStartDate = New System.Windows.Forms.DateTimePicker
        Me.rdbExtendedReport = New System.Windows.Forms.RadioButton
        Me.rdbFullReport = New System.Windows.Forms.RadioButton
        Me.rdbBasicReport = New System.Windows.Forms.RadioButton
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtFacilityName)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.mtbAIRSNumber)
        Me.Panel1.Controls.Add(Me.btnRunReport)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.DTPExtendedPrintEndDate)
        Me.Panel1.Controls.Add(Me.DTPExtendedPrintStartDate)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.DTPFullPrintEndDate)
        Me.Panel1.Controls.Add(Me.DTPFullPrintStartDate)
        Me.Panel1.Controls.Add(Me.rdbExtendedReport)
        Me.Panel1.Controls.Add(Me.rdbFullReport)
        Me.Panel1.Controls.Add(Me.rdbBasicReport)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(406, 160)
        Me.Panel1.TabIndex = 0
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(189, 7)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(205, 20)
        Me.txtFacilityName.TabIndex = 383
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(118, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 382
        Me.Label6.Text = "Facility Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 381
        Me.Label5.Text = "AIRS #"
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(52, 7)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.ReadOnly = True
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(62, 20)
        Me.mtbAIRSNumber.TabIndex = 380
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnRunReport
        '
        Me.btnRunReport.Location = New System.Drawing.Point(266, 33)
        Me.btnRunReport.Name = "btnRunReport"
        Me.btnRunReport.Size = New System.Drawing.Size(75, 23)
        Me.btnRunReport.TabIndex = 379
        Me.btnRunReport.Text = "Run Report"
        Me.btnRunReport.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(139, 194)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 378
        Me.Label3.Text = "End Date"
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 377
        Me.Label4.Text = "Start Date"
        Me.Label4.Visible = False
        '
        'DTPExtendedPrintEndDate
        '
        Me.DTPExtendedPrintEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPExtendedPrintEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPExtendedPrintEndDate.Location = New System.Drawing.Point(142, 171)
        Me.DTPExtendedPrintEndDate.Name = "DTPExtendedPrintEndDate"
        Me.DTPExtendedPrintEndDate.Size = New System.Drawing.Size(95, 20)
        Me.DTPExtendedPrintEndDate.TabIndex = 376
        Me.DTPExtendedPrintEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPExtendedPrintEndDate.Visible = False
        '
        'DTPExtendedPrintStartDate
        '
        Me.DTPExtendedPrintStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPExtendedPrintStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPExtendedPrintStartDate.Location = New System.Drawing.Point(29, 171)
        Me.DTPExtendedPrintStartDate.Name = "DTPExtendedPrintStartDate"
        Me.DTPExtendedPrintStartDate.Size = New System.Drawing.Size(95, 20)
        Me.DTPExtendedPrintStartDate.TabIndex = 375
        Me.DTPExtendedPrintStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPExtendedPrintStartDate.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(140, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 374
        Me.Label2.Text = "End Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 373
        Me.Label1.Text = "Start Date"
        '
        'DTPFullPrintEndDate
        '
        Me.DTPFullPrintEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFullPrintEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFullPrintEndDate.Location = New System.Drawing.Point(143, 99)
        Me.DTPFullPrintEndDate.Name = "DTPFullPrintEndDate"
        Me.DTPFullPrintEndDate.Size = New System.Drawing.Size(95, 20)
        Me.DTPFullPrintEndDate.TabIndex = 372
        Me.DTPFullPrintEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPFullPrintStartDate
        '
        Me.DTPFullPrintStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFullPrintStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFullPrintStartDate.Location = New System.Drawing.Point(30, 99)
        Me.DTPFullPrintStartDate.Name = "DTPFullPrintStartDate"
        Me.DTPFullPrintStartDate.Size = New System.Drawing.Size(95, 20)
        Me.DTPFullPrintStartDate.TabIndex = 371
        Me.DTPFullPrintStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'rdbExtendedReport
        '
        Me.rdbExtendedReport.AutoSize = True
        Me.rdbExtendedReport.Location = New System.Drawing.Point(7, 147)
        Me.rdbExtendedReport.Name = "rdbExtendedReport"
        Me.rdbExtendedReport.Size = New System.Drawing.Size(94, 17)
        Me.rdbExtendedReport.TabIndex = 3
        Me.rdbExtendedReport.TabStop = True
        Me.rdbExtendedReport.Text = "Extended Print"
        Me.rdbExtendedReport.UseVisualStyleBackColor = True
        Me.rdbExtendedReport.Visible = False
        '
        'rdbFullReport
        '
        Me.rdbFullReport.AutoSize = True
        Me.rdbFullReport.Location = New System.Drawing.Point(8, 76)
        Me.rdbFullReport.Name = "rdbFullReport"
        Me.rdbFullReport.Size = New System.Drawing.Size(65, 17)
        Me.rdbFullReport.TabIndex = 2
        Me.rdbFullReport.TabStop = True
        Me.rdbFullReport.Text = "Full Print"
        Me.rdbFullReport.UseVisualStyleBackColor = True
        '
        'rdbBasicReport
        '
        Me.rdbBasicReport.AutoSize = True
        Me.rdbBasicReport.Location = New System.Drawing.Point(7, 53)
        Me.rdbBasicReport.Name = "rdbBasicReport"
        Me.rdbBasicReport.Size = New System.Drawing.Size(75, 17)
        Me.rdbBasicReport.TabIndex = 1
        Me.rdbBasicReport.TabStop = True
        Me.rdbBasicReport.Text = "Basic Print"
        Me.rdbBasicReport.UseVisualStyleBackColor = True
        '
        'IAIPFacilitySummaryPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 160)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "IAIPFacilitySummaryPrint"
        Me.Text = "IAIP Facility Summary Print Out"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdbExtendedReport As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFullReport As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBasicReport As System.Windows.Forms.RadioButton
    Friend WithEvents DTPFullPrintStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTPFullPrintEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRunReport As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTPExtendedPrintEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPExtendedPrintStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
End Class
