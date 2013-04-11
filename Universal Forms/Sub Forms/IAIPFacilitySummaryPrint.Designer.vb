<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipFacilitySummaryPrint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IaipFacilitySummaryPrint))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.FacilityName = New System.Windows.Forms.TextBox
        Me.FacilityLabel = New System.Windows.Forms.Label
        Me.AirsNumber = New System.Windows.Forms.MaskedTextBox
        Me.ShowFullReport = New System.Windows.Forms.Button
        Me.ShowBasicReport = New System.Windows.Forms.Button
        Me.EndDateLabel = New System.Windows.Forms.Label
        Me.StartDateLabel = New System.Windows.Forms.Label
        Me.FullPrintEndDate = New System.Windows.Forms.DateTimePicker
        Me.FullPrintStartDate = New System.Windows.Forms.DateTimePicker
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FacilityName)
        Me.Panel1.Controls.Add(Me.FacilityLabel)
        Me.Panel1.Controls.Add(Me.AirsNumber)
        Me.Panel1.Controls.Add(Me.ShowFullReport)
        Me.Panel1.Controls.Add(Me.ShowBasicReport)
        Me.Panel1.Controls.Add(Me.EndDateLabel)
        Me.Panel1.Controls.Add(Me.StartDateLabel)
        Me.Panel1.Controls.Add(Me.FullPrintEndDate)
        Me.Panel1.Controls.Add(Me.FullPrintStartDate)
        Me.Panel1.Controls.Add(Me.ShapeContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(406, 160)
        Me.Panel1.TabIndex = 0
        '
        'FacilityName
        '
        Me.FacilityName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.FacilityName.Location = New System.Drawing.Point(128, 15)
        Me.FacilityName.Name = "FacilityName"
        Me.FacilityName.ReadOnly = True
        Me.FacilityName.Size = New System.Drawing.Size(258, 13)
        Me.FacilityName.TabIndex = 383
        Me.FacilityName.TabStop = False
        '
        'FacilityLabel
        '
        Me.FacilityLabel.AutoSize = True
        Me.FacilityLabel.Location = New System.Drawing.Point(18, 15)
        Me.FacilityLabel.Name = "FacilityLabel"
        Me.FacilityLabel.Size = New System.Drawing.Size(42, 13)
        Me.FacilityLabel.TabIndex = 382
        Me.FacilityLabel.Text = "Facility:"
        '
        'AirsNumber
        '
        Me.AirsNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AirsNumber.Location = New System.Drawing.Point(66, 15)
        Me.AirsNumber.Mask = "000-00000, "
        Me.AirsNumber.Name = "AirsNumber"
        Me.AirsNumber.ReadOnly = True
        Me.AirsNumber.Size = New System.Drawing.Size(62, 13)
        Me.AirsNumber.TabIndex = 380
        Me.AirsNumber.TabStop = False
        Me.AirsNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'ShowFullReport
        '
        Me.ShowFullReport.Location = New System.Drawing.Point(225, 52)
        Me.ShowFullReport.Name = "ShowFullReport"
        Me.ShowFullReport.Size = New System.Drawing.Size(161, 41)
        Me.ShowFullReport.TabIndex = 1
        Me.ShowFullReport.Text = "&Full Facility Report"
        Me.ShowFullReport.UseVisualStyleBackColor = True
        '
        'ShowBasicReport
        '
        Me.ShowBasicReport.Location = New System.Drawing.Point(21, 52)
        Me.ShowBasicReport.Name = "ShowBasicReport"
        Me.ShowBasicReport.Size = New System.Drawing.Size(161, 41)
        Me.ShowBasicReport.TabIndex = 0
        Me.ShowBasicReport.Text = "&Basic Facility Report"
        Me.ShowBasicReport.UseVisualStyleBackColor = True
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
        Me.FullPrintEndDate.Location = New System.Drawing.Point(291, 128)
        Me.FullPrintEndDate.Name = "FullPrintEndDate"
        Me.FullPrintEndDate.Size = New System.Drawing.Size(95, 20)
        Me.FullPrintEndDate.TabIndex = 3
        Me.FullPrintEndDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'FullPrintStartDate
        '
        Me.FullPrintStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.FullPrintStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FullPrintStartDate.Location = New System.Drawing.Point(291, 102)
        Me.FullPrintStartDate.Name = "FullPrintStartDate"
        Me.FullPrintStartDate.Size = New System.Drawing.Size(95, 20)
        Me.FullPrintStartDate.TabIndex = 2
        Me.FullPrintStartDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(406, 160)
        Me.ShapeContainer1.TabIndex = 384
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LineShape1.Enabled = False
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 203
        Me.LineShape1.X2 = 203
        Me.LineShape1.Y1 = 52
        Me.LineShape1.Y2 = 148
        '
        'IaipFacilitySummaryPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 160)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
    Friend WithEvents ShowBasicReport As System.Windows.Forms.Button
    Friend WithEvents AirsNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents FacilityLabel As System.Windows.Forms.Label
    Friend WithEvents FacilityName As System.Windows.Forms.TextBox
    Friend WithEvents ShowFullReport As System.Windows.Forms.Button
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
End Class
