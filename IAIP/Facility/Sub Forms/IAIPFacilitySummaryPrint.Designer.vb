<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipFacilitySummaryPrint
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ShowFullReportButton = New System.Windows.Forms.Button()
        Me.ShowBasicReportButton = New System.Windows.Forms.Button()
        Me.EndDateLabel = New System.Windows.Forms.Label()
        Me.StartDateLabel = New System.Windows.Forms.Label()
        Me.FullPrintEndDate = New System.Windows.Forms.DateTimePicker()
        Me.FullPrintStartDate = New System.Windows.Forms.DateTimePicker()
        Me.FacilityDisplay = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FacilityDisplay)
        Me.Panel1.Controls.Add(Me.ShowFullReportButton)
        Me.Panel1.Controls.Add(Me.ShowBasicReportButton)
        Me.Panel1.Controls.Add(Me.EndDateLabel)
        Me.Panel1.Controls.Add(Me.StartDateLabel)
        Me.Panel1.Controls.Add(Me.FullPrintEndDate)
        Me.Panel1.Controls.Add(Me.FullPrintStartDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(406, 162)
        Me.Panel1.TabIndex = 0
        '
        'ShowFullReportButton
        '
        Me.ShowFullReportButton.Location = New System.Drawing.Point(225, 52)
        Me.ShowFullReportButton.Name = "ShowFullReportButton"
        Me.ShowFullReportButton.Size = New System.Drawing.Size(161, 41)
        Me.ShowFullReportButton.TabIndex = 1
        Me.ShowFullReportButton.Text = "&Full Facility Report"
        Me.ShowFullReportButton.UseVisualStyleBackColor = True
        '
        'ShowBasicReportButton
        '
        Me.ShowBasicReportButton.Location = New System.Drawing.Point(21, 52)
        Me.ShowBasicReportButton.Name = "ShowBasicReportButton"
        Me.ShowBasicReportButton.Size = New System.Drawing.Size(161, 41)
        Me.ShowBasicReportButton.TabIndex = 0
        Me.ShowBasicReportButton.Text = "&Basic Facility Report"
        Me.ShowBasicReportButton.UseVisualStyleBackColor = True
        '
        'EndDateLabel
        '
        Me.EndDateLabel.AutoSize = True
        Me.EndDateLabel.Location = New System.Drawing.Point(222, 132)
        Me.EndDateLabel.Name = "EndDateLabel"
        Me.EndDateLabel.Size = New System.Drawing.Size(55, 13)
        Me.EndDateLabel.TabIndex = 374
        Me.EndDateLabel.Text = "End Date:"
        '
        'StartDateLabel
        '
        Me.StartDateLabel.AutoSize = True
        Me.StartDateLabel.Location = New System.Drawing.Point(222, 106)
        Me.StartDateLabel.Name = "StartDateLabel"
        Me.StartDateLabel.Size = New System.Drawing.Size(58, 13)
        Me.StartDateLabel.TabIndex = 373
        Me.StartDateLabel.Text = "Start Date:"
        '
        'FullPrintEndDate
        '
        Me.FullPrintEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.FullPrintEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FullPrintEndDate.Location = New System.Drawing.Point(286, 128)
        Me.FullPrintEndDate.Name = "FullPrintEndDate"
        Me.FullPrintEndDate.Size = New System.Drawing.Size(100, 20)
        Me.FullPrintEndDate.TabIndex = 3
        Me.FullPrintEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'FullPrintStartDate
        '
        Me.FullPrintStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.FullPrintStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FullPrintStartDate.Location = New System.Drawing.Point(286, 102)
        Me.FullPrintStartDate.Name = "FullPrintStartDate"
        Me.FullPrintStartDate.Size = New System.Drawing.Size(100, 20)
        Me.FullPrintStartDate.TabIndex = 2
        Me.FullPrintStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'FacilityDisplay
        '
        Me.FacilityDisplay.AutoSize = True
        Me.FacilityDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityDisplay.Location = New System.Drawing.Point(18, 19)
        Me.FacilityDisplay.Name = "FacilityDisplay"
        Me.FacilityDisplay.Size = New System.Drawing.Size(130, 17)
        Me.FacilityDisplay.TabIndex = 384
        Me.FacilityDisplay.Text = " No facility selected"
        '
        'IaipFacilitySummaryPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 162)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "IaipFacilitySummaryPrint"
        Me.Text = "Print Facility Summary"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FullPrintStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents EndDateLabel As System.Windows.Forms.Label
    Friend WithEvents StartDateLabel As System.Windows.Forms.Label
    Friend WithEvents FullPrintEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ShowBasicReportButton As System.Windows.Forms.Button
    Friend WithEvents ShowFullReportButton As System.Windows.Forms.Button
    Friend WithEvents FacilityDisplay As System.Windows.Forms.Label
End Class
