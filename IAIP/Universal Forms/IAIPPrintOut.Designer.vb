<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPPrintOut
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.txtPrintType = New System.Windows.Forms.TextBox
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.txtProgram = New System.Windows.Forms.TextBox
        Me.CRViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.txtSQLLine = New System.Windows.Forms.TextBox
        Me.txtEndDate = New System.Windows.Forms.TextBox
        Me.txtStartDate = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtOther
        '
        Me.txtOther.Location = New System.Drawing.Point(82, 311)
        Me.txtOther.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(22, 20)
        Me.txtOther.TabIndex = 246
        Me.txtOther.Visible = False
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(61, 311)
        Me.txtAIRSNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(23, 20)
        Me.txtAIRSNumber.TabIndex = 245
        Me.txtAIRSNumber.Visible = False
        '
        'txtPrintType
        '
        Me.txtPrintType.Location = New System.Drawing.Point(39, 311)
        Me.txtPrintType.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPrintType.Name = "txtPrintType"
        Me.txtPrintType.Size = New System.Drawing.Size(23, 20)
        Me.txtPrintType.TabIndex = 244
        Me.txtPrintType.Visible = False
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(17, 311)
        Me.txtReferenceNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(23, 20)
        Me.txtReferenceNumber.TabIndex = 243
        Me.txtReferenceNumber.Visible = False
        '
        'txtProgram
        '
        Me.txtProgram.Location = New System.Drawing.Point(3, 311)
        Me.txtProgram.Margin = New System.Windows.Forms.Padding(2)
        Me.txtProgram.Name = "txtProgram"
        Me.txtProgram.Size = New System.Drawing.Size(15, 20)
        Me.txtProgram.TabIndex = 242
        Me.txtProgram.Visible = False
        '
        'CRViewer
        '
        Me.CRViewer.ActiveViewIndex = -1
        Me.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRViewer.Location = New System.Drawing.Point(0, 0)
        Me.CRViewer.Margin = New System.Windows.Forms.Padding(2)
        Me.CRViewer.Name = "CRViewer"
        Me.CRViewer.SelectionFormula = ""
        Me.CRViewer.ShowGroupTreeButton = False
        Me.CRViewer.ShowRefreshButton = False
        Me.CRViewer.Size = New System.Drawing.Size(792, 545)
        Me.CRViewer.TabIndex = 249
        Me.CRViewer.ViewTimeSelectionFormula = ""
        '
        'txtSQLLine
        '
        Me.txtSQLLine.Location = New System.Drawing.Point(108, 311)
        Me.txtSQLLine.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSQLLine.Name = "txtSQLLine"
        Me.txtSQLLine.Size = New System.Drawing.Size(22, 20)
        Me.txtSQLLine.TabIndex = 250
        Me.txtSQLLine.Visible = False
        '
        'txtEndDate
        '
        Me.txtEndDate.Location = New System.Drawing.Point(160, 311)
        Me.txtEndDate.Margin = New System.Windows.Forms.Padding(2)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(22, 20)
        Me.txtEndDate.TabIndex = 251
        Me.txtEndDate.Visible = False
        '
        'txtStartDate
        '
        Me.txtStartDate.Location = New System.Drawing.Point(134, 311)
        Me.txtStartDate.Margin = New System.Windows.Forms.Padding(2)
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(22, 20)
        Me.txtStartDate.TabIndex = 252
        Me.txtStartDate.Visible = False
        '
        'IAIPPrintOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 545)
        Me.Controls.Add(Me.CRViewer)
        Me.Controls.Add(Me.txtStartDate)
        Me.Controls.Add(Me.txtEndDate)
        Me.Controls.Add(Me.txtSQLLine)
        Me.Controls.Add(Me.txtOther)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.txtPrintType)
        Me.Controls.Add(Me.txtReferenceNumber)
        Me.Controls.Add(Me.txtProgram)
        Me.Location = New System.Drawing.Point(50, 0)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "IAIPPrintOut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IAIP Print Out"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtPrintType As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtProgram As System.Windows.Forms.TextBox
    Friend WithEvents CRViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents txtSQLLine As System.Windows.Forms.TextBox
    Friend WithEvents txtEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtStartDate As System.Windows.Forms.TextBox
End Class
