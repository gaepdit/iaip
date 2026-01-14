<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SSCPComplianceLog
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
        Me.lnkWebEnforcement = New System.Windows.Forms.LinkLabel()
        Me.lnkWebFce = New System.Windows.Forms.LinkLabel()
        Me.lnkWebComplianceWork = New System.Windows.Forms.LinkLabel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lnkWebEnforcement
        '
        Me.lnkWebEnforcement.AutoSize = True
        Me.lnkWebEnforcement.Location = New System.Drawing.Point(25, 107)
        Me.lnkWebEnforcement.Name = "lnkWebEnforcement"
        Me.lnkWebEnforcement.Size = New System.Drawing.Size(104, 13)
        Me.lnkWebEnforcement.TabIndex = 3
        Me.lnkWebEnforcement.TabStop = True
        Me.lnkWebEnforcement.Text = "Enforcement Search"
        '
        'lnkWebFce
        '
        Me.lnkWebFce.AutoSize = True
        Me.lnkWebFce.Location = New System.Drawing.Point(25, 81)
        Me.lnkWebFce.Name = "lnkWebFce"
        Me.lnkWebFce.Size = New System.Drawing.Size(64, 13)
        Me.lnkWebFce.TabIndex = 4
        Me.lnkWebFce.TabStop = True
        Me.lnkWebFce.Text = "FCE Search"
        '
        'lnkWebComplianceWork
        '
        Me.lnkWebComplianceWork.AutoSize = True
        Me.lnkWebComplianceWork.Location = New System.Drawing.Point(25, 55)
        Me.lnkWebComplianceWork.Name = "lnkWebComplianceWork"
        Me.lnkWebComplianceWork.Size = New System.Drawing.Size(128, 13)
        Me.lnkWebComplianceWork.TabIndex = 5
        Me.lnkWebComplianceWork.TabStop = True
        Me.lnkWebComplianceWork.Text = "Compliance Work Search"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(25, 29)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(314, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Compliance data has been moved to the Air Web App:"
        '
        'SSCPComplianceLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(362, 282)
        Me.Controls.Add(Me.lnkWebEnforcement)
        Me.Controls.Add(Me.lnkWebFce)
        Me.Controls.Add(Me.lnkWebComplianceWork)
        Me.Controls.Add(Me.Label12)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(378, 321)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(378, 321)
        Me.Name = "SSCPComplianceLog"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Compliance Log"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lnkWebEnforcement As LinkLabel
    Friend WithEvents lnkWebFce As LinkLabel
    Friend WithEvents lnkWebComplianceWork As LinkLabel
    Friend WithEvents Label12 As Label
End Class
