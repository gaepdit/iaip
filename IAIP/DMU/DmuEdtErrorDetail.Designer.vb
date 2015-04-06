<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DmuEdtErrorDetail
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
        Me.ErrorIDDisplay = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ErrorIDDisplay
        '
        Me.ErrorIDDisplay.AutoSize = True
        Me.ErrorIDDisplay.Location = New System.Drawing.Point(103, 99)
        Me.ErrorIDDisplay.Name = "ErrorIDDisplay"
        Me.ErrorIDDisplay.Size = New System.Drawing.Size(40, 13)
        Me.ErrorIDDisplay.TabIndex = 0
        Me.ErrorIDDisplay.Text = "ErrorID"
        '
        'DmuEdtErrorDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.ErrorIDDisplay)
        Me.Name = "DmuEdtErrorDetail"
        Me.Text = "DmuEdtErrorDetail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ErrorIDDisplay As System.Windows.Forms.Label
End Class
