<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPMemo
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents TCISMPMemo As System.Windows.Forms.TabControl
    Friend WithEvents TPInternalMemo As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents txtMemoOut As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMemoIN As System.Windows.Forms.TextBox
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtMemoIN = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMemoOut = New System.Windows.Forms.TextBox()
        Me.TCISMPMemo = New System.Windows.Forms.TabControl()
        Me.TPInternalMemo = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblReferenceNumber = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.bbtSave = New System.Windows.Forms.ToolStripButton()
        Me.TCISMPMemo.SuspendLayout()
        Me.TPInternalMemo.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMemoIN
        '
        Me.txtMemoIN.AcceptsReturn = True
        Me.txtMemoIN.AcceptsTab = True
        Me.txtMemoIN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMemoIN.Location = New System.Drawing.Point(0, 0)
        Me.txtMemoIN.Multiline = True
        Me.txtMemoIN.Name = "txtMemoIN"
        Me.txtMemoIN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMemoIN.Size = New System.Drawing.Size(936, 224)
        Me.txtMemoIN.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(253, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(219, 22)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "ISMP Test Report Memo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(478, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 16)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "For Reference Number:"
        '
        'txtMemoOut
        '
        Me.txtMemoOut.AcceptsReturn = True
        Me.txtMemoOut.AcceptsTab = True
        Me.txtMemoOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMemoOut.Location = New System.Drawing.Point(0, 0)
        Me.txtMemoOut.Multiline = True
        Me.txtMemoOut.Name = "txtMemoOut"
        Me.txtMemoOut.ReadOnly = True
        Me.txtMemoOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMemoOut.Size = New System.Drawing.Size(936, 317)
        Me.txtMemoOut.TabIndex = 53
        '
        'TCISMPMemo
        '
        Me.TCISMPMemo.Controls.Add(Me.TPInternalMemo)
        Me.TCISMPMemo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCISMPMemo.Location = New System.Drawing.Point(0, 55)
        Me.TCISMPMemo.Name = "TCISMPMemo"
        Me.TCISMPMemo.SelectedIndex = 0
        Me.TCISMPMemo.Size = New System.Drawing.Size(950, 583)
        Me.TCISMPMemo.TabIndex = 57
        '
        'TPInternalMemo
        '
        Me.TPInternalMemo.Controls.Add(Me.SplitContainer1)
        Me.TPInternalMemo.Location = New System.Drawing.Point(4, 22)
        Me.TPInternalMemo.Name = "TPInternalMemo"
        Me.TPInternalMemo.Padding = New System.Windows.Forms.Padding(3)
        Me.TPInternalMemo.Size = New System.Drawing.Size(942, 557)
        Me.TPInternalMemo.TabIndex = 0
        Me.TPInternalMemo.Text = "Internal Memo"
        Me.TPInternalMemo.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMemoOut)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtMemoIN)
        Me.SplitContainer1.Size = New System.Drawing.Size(936, 551)
        Me.SplitContainer1.SplitterDistance = 317
        Me.SplitContainer1.SplitterWidth = 10
        Me.SplitContainer1.TabIndex = 54
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.lblReferenceNumber)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 25)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(950, 30)
        Me.Panel4.TabIndex = 58
        '
        'lblReferenceNumber
        '
        Me.lblReferenceNumber.AutoSize = True
        Me.lblReferenceNumber.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReferenceNumber.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblReferenceNumber.Location = New System.Drawing.Point(641, 8)
        Me.lblReferenceNumber.Name = "lblReferenceNumber"
        Me.lblReferenceNumber.Size = New System.Drawing.Size(30, 16)
        Me.lblReferenceNumber.TabIndex = 55
        Me.lblReferenceNumber.Text = "N/A"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bbtSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(950, 25)
        Me.ToolStrip1.TabIndex = 143
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'bbtSave
        '
        Me.bbtSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bbtSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.bbtSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bbtSave.Name = "bbtSave"
        Me.bbtSave.Size = New System.Drawing.Size(23, 22)
        Me.bbtSave.Text = "Save"
        '
        'ISMPMemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(950, 638)
        Me.Controls.Add(Me.TCISMPMemo)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "ISMPMemo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Memo"
        Me.TCISMPMemo.ResumeLayout(False)
        Me.TPInternalMemo.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents bbtSave As ToolStripButton
    Friend WithEvents lblReferenceNumber As Label
End Class
