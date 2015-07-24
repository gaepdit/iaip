<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CRViewerForm
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
        Me.CRViewerControl = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CRViewerControl
        '
        Me.CRViewerControl.ActiveViewIndex = -1
        Me.CRViewerControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewerControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRViewerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRViewerControl.EnableDrillDown = False
        Me.CRViewerControl.Location = New System.Drawing.Point(0, 0)
        Me.CRViewerControl.Name = "CRViewerControl"
        Me.CRViewerControl.SelectionFormula = ""
        Me.CRViewerControl.ShowCloseButton = False
        Me.CRViewerControl.ShowGroupTreeButton = False
        Me.CRViewerControl.ShowRefreshButton = False
        Me.CRViewerControl.Size = New System.Drawing.Size(575, 520)
        Me.CRViewerControl.TabIndex = 0
        Me.CRViewerControl.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CRViewerControl.ViewTimeSelectionFormula = ""
        '
        'CRViewerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 520)
        Me.Controls.Add(Me.CRViewerControl)
        Me.Name = "CRViewerForm"
        Me.Text = "Report Preview"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRViewerControl As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
