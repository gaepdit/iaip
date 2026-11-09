<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPLookUpTables
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
        Me.TCLookUpTables = New System.Windows.Forms.TabControl()
        Me.TPAPBManagement = New System.Windows.Forms.TabPage()
        Me.dgvLookUpManagement = New System.Windows.Forms.DataGridView()
        Me.pnlAPBManagement = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtApbManagementType = New System.Windows.Forms.TextBox()
        Me.txtApbManagementName = New System.Windows.Forms.TextBox()
        Me.btnSaveAPBManagement = New System.Windows.Forms.Button()
        Me.TCLookUpTables.SuspendLayout()
        Me.TPAPBManagement.SuspendLayout()
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAPBManagement.SuspendLayout()
        Me.SuspendLayout()
        '
        'TCLookUpTables
        '
        Me.TCLookUpTables.Controls.Add(Me.TPAPBManagement)
        Me.TCLookUpTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCLookUpTables.Location = New System.Drawing.Point(0, 0)
        Me.TCLookUpTables.Name = "TCLookUpTables"
        Me.TCLookUpTables.SelectedIndex = 0
        Me.TCLookUpTables.Size = New System.Drawing.Size(792, 425)
        Me.TCLookUpTables.TabIndex = 0
        '
        'TPAPBManagement
        '
        Me.TPAPBManagement.Controls.Add(Me.dgvLookUpManagement)
        Me.TPAPBManagement.Controls.Add(Me.pnlAPBManagement)
        Me.TPAPBManagement.Location = New System.Drawing.Point(4, 22)
        Me.TPAPBManagement.Name = "TPAPBManagement"
        Me.TPAPBManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAPBManagement.Size = New System.Drawing.Size(784, 399)
        Me.TPAPBManagement.TabIndex = 0
        Me.TPAPBManagement.Text = "EPD Management"
        Me.TPAPBManagement.UseVisualStyleBackColor = True
        '
        'dgvLookUpManagement
        '
        Me.dgvLookUpManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLookUpManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLookUpManagement.Location = New System.Drawing.Point(3, 97)
        Me.dgvLookUpManagement.Name = "dgvLookUpManagement"
        Me.dgvLookUpManagement.ReadOnly = True
        Me.dgvLookUpManagement.Size = New System.Drawing.Size(778, 299)
        Me.dgvLookUpManagement.TabIndex = 1
        '
        'pnlAPBManagement
        '
        Me.pnlAPBManagement.Controls.Add(Me.Label5)
        Me.pnlAPBManagement.Controls.Add(Me.Label4)
        Me.pnlAPBManagement.Controls.Add(Me.txtApbManagementType)
        Me.pnlAPBManagement.Controls.Add(Me.txtApbManagementName)
        Me.pnlAPBManagement.Controls.Add(Me.btnSaveAPBManagement)
        Me.pnlAPBManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAPBManagement.Location = New System.Drawing.Point(3, 3)
        Me.pnlAPBManagement.Name = "pnlAPBManagement"
        Me.pnlAPBManagement.Size = New System.Drawing.Size(778, 94)
        Me.pnlAPBManagement.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Management Type"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Full Name"
        '
        'txtApbManagementType
        '
        Me.txtApbManagementType.Location = New System.Drawing.Point(119, 6)
        Me.txtApbManagementType.Name = "txtApbManagementType"
        Me.txtApbManagementType.ReadOnly = True
        Me.txtApbManagementType.Size = New System.Drawing.Size(119, 20)
        Me.txtApbManagementType.TabIndex = 0
        '
        'txtApbManagementName
        '
        Me.txtApbManagementName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtApbManagementName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtApbManagementName.Location = New System.Drawing.Point(119, 32)
        Me.txtApbManagementName.Name = "txtApbManagementName"
        Me.txtApbManagementName.Size = New System.Drawing.Size(228, 20)
        Me.txtApbManagementName.TabIndex = 1
        '
        'btnSaveAPBManagement
        '
        Me.btnSaveAPBManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveAPBManagement.Location = New System.Drawing.Point(119, 58)
        Me.btnSaveAPBManagement.Name = "btnSaveAPBManagement"
        Me.btnSaveAPBManagement.Size = New System.Drawing.Size(119, 23)
        Me.btnSaveAPBManagement.TabIndex = 2
        Me.btnSaveAPBManagement.Text = "Save Management"
        Me.btnSaveAPBManagement.UseVisualStyleBackColor = True
        '
        'IAIPLookUpTables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 425)
        Me.Controls.Add(Me.TCLookUpTables)
        Me.MinimumSize = New System.Drawing.Size(808, 310)
        Me.Name = "IAIPLookUpTables"
        Me.Text = "IAIP Look Up Tables"
        Me.TCLookUpTables.ResumeLayout(False)
        Me.TPAPBManagement.ResumeLayout(False)
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAPBManagement.ResumeLayout(False)
        Me.pnlAPBManagement.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCLookUpTables As System.Windows.Forms.TabControl
    Friend WithEvents TPAPBManagement As System.Windows.Forms.TabPage
    Friend WithEvents pnlAPBManagement As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtApbManagementName As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveAPBManagement As System.Windows.Forms.Button
    Friend WithEvents dgvLookUpManagement As System.Windows.Forms.DataGridView
    Friend WithEvents txtApbManagementType As TextBox
End Class
