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
        Me.TPApplicationTypes = New System.Windows.Forms.TabPage()
        Me.btnChangeAppStatus = New System.Windows.Forms.Button()
        Me.dgvApplicationType = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSelectedAppType = New System.Windows.Forms.TextBox()
        Me.txtNewAppType = New System.Windows.Forms.TextBox()
        Me.btnAddNewAppType = New System.Windows.Forms.Button()
        Me.TCLookUpTables.SuspendLayout()
        Me.TPAPBManagement.SuspendLayout()
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAPBManagement.SuspendLayout()
        Me.TPApplicationTypes.SuspendLayout()
        CType(Me.dgvApplicationType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TCLookUpTables
        '
        Me.TCLookUpTables.Controls.Add(Me.TPAPBManagement)
        Me.TCLookUpTables.Controls.Add(Me.TPApplicationTypes)
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
        'TPApplicationTypes
        '
        Me.TPApplicationTypes.Controls.Add(Me.btnChangeAppStatus)
        Me.TPApplicationTypes.Controls.Add(Me.dgvApplicationType)
        Me.TPApplicationTypes.Controls.Add(Me.Label2)
        Me.TPApplicationTypes.Controls.Add(Me.txtSelectedAppType)
        Me.TPApplicationTypes.Controls.Add(Me.txtNewAppType)
        Me.TPApplicationTypes.Controls.Add(Me.btnAddNewAppType)
        Me.TPApplicationTypes.Location = New System.Drawing.Point(4, 22)
        Me.TPApplicationTypes.Name = "TPApplicationTypes"
        Me.TPApplicationTypes.Padding = New System.Windows.Forms.Padding(3)
        Me.TPApplicationTypes.Size = New System.Drawing.Size(784, 399)
        Me.TPApplicationTypes.TabIndex = 0
        Me.TPApplicationTypes.Text = "Application Types"
        Me.TPApplicationTypes.UseVisualStyleBackColor = True
        '
        'btnChangeAppStatus
        '
        Me.btnChangeAppStatus.AutoSize = True
        Me.btnChangeAppStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnChangeAppStatus.Location = New System.Drawing.Point(656, 6)
        Me.btnChangeAppStatus.Name = "btnChangeAppStatus"
        Me.btnChangeAppStatus.Size = New System.Drawing.Size(120, 23)
        Me.btnChangeAppStatus.TabIndex = 3
        Me.btnChangeAppStatus.Text = "Change Active Status"
        Me.btnChangeAppStatus.UseVisualStyleBackColor = True
        '
        'dgvApplicationType
        '
        Me.dgvApplicationType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvApplicationType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplicationType.Location = New System.Drawing.Point(3, 58)
        Me.dgvApplicationType.Name = "dgvApplicationType"
        Me.dgvApplicationType.ReadOnly = True
        Me.dgvApplicationType.Size = New System.Drawing.Size(778, 345)
        Me.dgvApplicationType.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Application Desc"
        '
        'txtSelectedAppType
        '
        Me.txtSelectedAppType.Location = New System.Drawing.Point(475, 8)
        Me.txtSelectedAppType.Name = "txtSelectedAppType"
        Me.txtSelectedAppType.ReadOnly = True
        Me.txtSelectedAppType.Size = New System.Drawing.Size(175, 20)
        Me.txtSelectedAppType.TabIndex = 2
        '
        'txtNewAppType
        '
        Me.txtNewAppType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtNewAppType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtNewAppType.Location = New System.Drawing.Point(101, 8)
        Me.txtNewAppType.Name = "txtNewAppType"
        Me.txtNewAppType.Size = New System.Drawing.Size(175, 20)
        Me.txtNewAppType.TabIndex = 0
        '
        'btnAddNewAppType
        '
        Me.btnAddNewAppType.AutoSize = True
        Me.btnAddNewAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewAppType.Location = New System.Drawing.Point(282, 6)
        Me.btnAddNewAppType.Name = "btnAddNewAppType"
        Me.btnAddNewAppType.Size = New System.Drawing.Size(143, 23)
        Me.btnAddNewAppType.TabIndex = 1
        Me.btnAddNewAppType.Text = "Add New Application Type"
        Me.btnAddNewAppType.UseVisualStyleBackColor = True
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
        Me.TPApplicationTypes.ResumeLayout(False)
        Me.TPApplicationTypes.PerformLayout()
        CType(Me.dgvApplicationType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCLookUpTables As System.Windows.Forms.TabControl
    Friend WithEvents btnAddNewAppType As System.Windows.Forms.Button
    Friend WithEvents txtNewAppType As System.Windows.Forms.TextBox
    Friend WithEvents dgvApplicationType As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TPApplicationTypes As System.Windows.Forms.TabPage
    Friend WithEvents btnChangeAppStatus As System.Windows.Forms.Button
    Friend WithEvents TPAPBManagement As System.Windows.Forms.TabPage
    Friend WithEvents pnlAPBManagement As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtApbManagementName As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveAPBManagement As System.Windows.Forms.Button
    Friend WithEvents dgvLookUpManagement As System.Windows.Forms.DataGridView
    Friend WithEvents txtApbManagementType As TextBox
    Friend WithEvents txtSelectedAppType As TextBox
End Class
