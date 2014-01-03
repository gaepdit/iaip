<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Roster
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
        Me.txtLocationTerm = New System.Windows.Forms.TextBox
        Me.crRosterViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'txtLocationTerm
        '
        Me.txtLocationTerm.Location = New System.Drawing.Point(67, -1)
        Me.txtLocationTerm.Name = "txtLocationTerm"
        Me.txtLocationTerm.Size = New System.Drawing.Size(100, 20)
        Me.txtLocationTerm.TabIndex = 0
        '
        'crRosterViewer
        '
        Me.crRosterViewer.ActiveViewIndex = -1
        Me.crRosterViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crRosterViewer.Location = New System.Drawing.Point(1, 26)
        Me.crRosterViewer.Name = "crRosterViewer"
        Me.crRosterViewer.SelectionFormula = ""
        Me.crRosterViewer.Size = New System.Drawing.Size(1004, 667)
        Me.crRosterViewer.TabIndex = 1
        Me.crRosterViewer.ViewTimeSelectionFormula = ""
        '
        'Roster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 705)
        Me.Controls.Add(Me.crRosterViewer)
        Me.Controls.Add(Me.txtLocationTerm)
        Me.Name = "Roster"
        Me.Text = "Roster"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLocationTerm As System.Windows.Forms.TextBox
    Friend WithEvents crRosterViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
