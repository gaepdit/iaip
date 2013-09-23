<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PassFailNoShow
    Inherits DefaultForm

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
        Me.txtStartDate = New System.Windows.Forms.TextBox
        Me.txtEndDate = New System.Windows.Forms.TextBox
        Me.crPassFailNoShowViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.txtPassFailNoShow = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtStartDate
        '
        Me.txtStartDate.Location = New System.Drawing.Point(146, -1)
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(100, 20)
        Me.txtStartDate.TabIndex = 0
        '
        'txtEndDate
        '
        Me.txtEndDate.Location = New System.Drawing.Point(272, -1)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(100, 20)
        Me.txtEndDate.TabIndex = 1
        '
        'crPassFailNoShowViewer
        '
        Me.crPassFailNoShowViewer.ActiveViewIndex = -1
        Me.crPassFailNoShowViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crPassFailNoShowViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crPassFailNoShowViewer.Location = New System.Drawing.Point(0, 0)
        Me.crPassFailNoShowViewer.Name = "crPassFailNoShowViewer"
        Me.crPassFailNoShowViewer.SelectionFormula = ""
        Me.crPassFailNoShowViewer.Size = New System.Drawing.Size(1147, 752)
        Me.crPassFailNoShowViewer.TabIndex = 2
        Me.crPassFailNoShowViewer.ViewTimeSelectionFormula = ""
        '
        'txtPassFailNoShow
        '
        Me.txtPassFailNoShow.Location = New System.Drawing.Point(402, -1)
        Me.txtPassFailNoShow.Name = "txtPassFailNoShow"
        Me.txtPassFailNoShow.Size = New System.Drawing.Size(100, 20)
        Me.txtPassFailNoShow.TabIndex = 3
        '
        'PassFailNoShow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1147, 752)
        Me.Controls.Add(Me.crPassFailNoShowViewer)
        Me.Controls.Add(Me.txtPassFailNoShow)
        Me.Controls.Add(Me.txtEndDate)
        Me.Controls.Add(Me.txtStartDate)
        Me.Name = "PassFailNoShow"
        Me.Text = "Pass"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtStartDate As System.Windows.Forms.TextBox
    Friend WithEvents txtEndDate As System.Windows.Forms.TextBox
    Friend WithEvents crPassFailNoShowViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents txtPassFailNoShow As System.Windows.Forms.TextBox
End Class
