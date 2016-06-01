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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPLookUpTables))
        Me.TCLookUpTables = New System.Windows.Forms.TabControl()
        Me.TPAPBManagement = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgvLookUpManagement = New System.Windows.Forms.DataGridView()
        Me.pnlAPBManagement = New System.Windows.Forms.Panel()
        Me.btnViewAllPastTypes = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboManagementType = New System.Windows.Forms.ComboBox()
        Me.btnClearManagement = New System.Windows.Forms.Button()
        Me.txtAPBManagemetnID = New System.Windows.Forms.TextBox()
        Me.chbAPBMangementVacant = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAPBManagementName = New System.Windows.Forms.TextBox()
        Me.btnSaveAPBManagement = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnLoadAPBManagement = New System.Windows.Forms.Button()
        Me.TPApplicationTypes = New System.Windows.Forms.TabPage()
        Me.btnEditAppType = New System.Windows.Forms.Button()
        Me.btnDeleteAppType = New System.Windows.Forms.Button()
        Me.dgvApplicationType = New System.Windows.Forms.DataGridView()
        Me.btnClearAppTypes = New System.Windows.Forms.Button()
        Me.txtApplicationID = New System.Windows.Forms.TextBox()
        Me.chbActiveAppType = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtApplicationDesc = New System.Windows.Forms.TextBox()
        Me.btnAddNewAppType = New System.Windows.Forms.Button()
        Me.btnLoadApplicationTypes = New System.Windows.Forms.Button()
        Me.TCLookUpTables.SuspendLayout()
        Me.TPAPBManagement.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAPBManagement.SuspendLayout()
        Me.Panel2.SuspendLayout()
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
        Me.TCLookUpTables.Size = New System.Drawing.Size(792, 545)
        Me.TCLookUpTables.TabIndex = 1
        '
        'TPAPBManagement
        '
        Me.TPAPBManagement.Controls.Add(Me.Panel1)
        Me.TPAPBManagement.Controls.Add(Me.pnlAPBManagement)
        Me.TPAPBManagement.Controls.Add(Me.Panel2)
        Me.TPAPBManagement.Location = New System.Drawing.Point(4, 22)
        Me.TPAPBManagement.Name = "TPAPBManagement"
        Me.TPAPBManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAPBManagement.Size = New System.Drawing.Size(784, 519)
        Me.TPAPBManagement.TabIndex = 0
        Me.TPAPBManagement.Text = "APB Management"
        Me.TPAPBManagement.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgvLookUpManagement)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 167)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(778, 349)
        Me.Panel1.TabIndex = 57
        '
        'dgvLookUpManagement
        '
        Me.dgvLookUpManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLookUpManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLookUpManagement.Location = New System.Drawing.Point(0, 0)
        Me.dgvLookUpManagement.Name = "dgvLookUpManagement"
        Me.dgvLookUpManagement.ReadOnly = True
        Me.dgvLookUpManagement.Size = New System.Drawing.Size(778, 349)
        Me.dgvLookUpManagement.TabIndex = 56
        Me.dgvLookUpManagement.Visible = False
        '
        'pnlAPBManagement
        '
        Me.pnlAPBManagement.Controls.Add(Me.btnViewAllPastTypes)
        Me.pnlAPBManagement.Controls.Add(Me.Label5)
        Me.pnlAPBManagement.Controls.Add(Me.cboManagementType)
        Me.pnlAPBManagement.Controls.Add(Me.btnClearManagement)
        Me.pnlAPBManagement.Controls.Add(Me.txtAPBManagemetnID)
        Me.pnlAPBManagement.Controls.Add(Me.chbAPBMangementVacant)
        Me.pnlAPBManagement.Controls.Add(Me.Label3)
        Me.pnlAPBManagement.Controls.Add(Me.Label4)
        Me.pnlAPBManagement.Controls.Add(Me.txtAPBManagementName)
        Me.pnlAPBManagement.Controls.Add(Me.btnSaveAPBManagement)
        Me.pnlAPBManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAPBManagement.Location = New System.Drawing.Point(3, 36)
        Me.pnlAPBManagement.Name = "pnlAPBManagement"
        Me.pnlAPBManagement.Size = New System.Drawing.Size(778, 131)
        Me.pnlAPBManagement.TabIndex = 54
        Me.pnlAPBManagement.Visible = False
        '
        'btnViewAllPastTypes
        '
        Me.btnViewAllPastTypes.AutoSize = True
        Me.btnViewAllPastTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewAllPastTypes.Location = New System.Drawing.Point(367, 32)
        Me.btnViewAllPastTypes.Name = "btnViewAllPastTypes"
        Me.btnViewAllPastTypes.Size = New System.Drawing.Size(108, 23)
        Me.btnViewAllPastTypes.TabIndex = 66
        Me.btnViewAllPastTypes.Text = "View all past Types"
        Me.btnViewAllPastTypes.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Management Type"
        '
        'cboManagementType
        '
        Me.cboManagementType.FormattingEnabled = True
        Me.cboManagementType.Location = New System.Drawing.Point(118, 34)
        Me.cboManagementType.Name = "cboManagementType"
        Me.cboManagementType.Size = New System.Drawing.Size(163, 21)
        Me.cboManagementType.TabIndex = 62
        '
        'btnClearManagement
        '
        Me.btnClearManagement.AutoSize = True
        Me.btnClearManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearManagement.Image = CType(resources.GetObject("btnClearManagement.Image"), System.Drawing.Image)
        Me.btnClearManagement.Location = New System.Drawing.Point(131, 6)
        Me.btnClearManagement.Name = "btnClearManagement"
        Me.btnClearManagement.Size = New System.Drawing.Size(22, 22)
        Me.btnClearManagement.TabIndex = 59
        Me.btnClearManagement.UseVisualStyleBackColor = True
        '
        'txtAPBManagemetnID
        '
        Me.txtAPBManagemetnID.Location = New System.Drawing.Point(65, 8)
        Me.txtAPBManagemetnID.Name = "txtAPBManagemetnID"
        Me.txtAPBManagemetnID.ReadOnly = True
        Me.txtAPBManagemetnID.Size = New System.Drawing.Size(51, 20)
        Me.txtAPBManagemetnID.TabIndex = 56
        '
        'chbAPBMangementVacant
        '
        Me.chbAPBMangementVacant.AutoSize = True
        Me.chbAPBMangementVacant.Location = New System.Drawing.Point(367, 60)
        Me.chbAPBMangementVacant.Name = "chbAPBMangementVacant"
        Me.chbAPBMangementVacant.Size = New System.Drawing.Size(100, 17)
        Me.chbAPBMangementVacant.TabIndex = 58
        Me.chbAPBMangementVacant.Text = "Vacant Position"
        Me.chbAPBMangementVacant.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "ID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Full Name"
        '
        'txtAPBManagementName
        '
        Me.txtAPBManagementName.Location = New System.Drawing.Point(118, 58)
        Me.txtAPBManagementName.Name = "txtAPBManagementName"
        Me.txtAPBManagementName.Size = New System.Drawing.Size(228, 20)
        Me.txtAPBManagementName.TabIndex = 54
        '
        'btnSaveAPBManagement
        '
        Me.btnSaveAPBManagement.AutoSize = True
        Me.btnSaveAPBManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveAPBManagement.Location = New System.Drawing.Point(118, 86)
        Me.btnSaveAPBManagement.Name = "btnSaveAPBManagement"
        Me.btnSaveAPBManagement.Size = New System.Drawing.Size(107, 23)
        Me.btnSaveAPBManagement.TabIndex = 55
        Me.btnSaveAPBManagement.Text = "Save Management"
        Me.btnSaveAPBManagement.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnLoadAPBManagement)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(778, 33)
        Me.Panel2.TabIndex = 55
        '
        'btnLoadAPBManagement
        '
        Me.btnLoadAPBManagement.AutoSize = True
        Me.btnLoadAPBManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadAPBManagement.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadAPBManagement.Name = "btnLoadAPBManagement"
        Me.btnLoadAPBManagement.Size = New System.Drawing.Size(130, 23)
        Me.btnLoadAPBManagement.TabIndex = 52
        Me.btnLoadAPBManagement.Text = "Load APB Management"
        Me.btnLoadAPBManagement.UseVisualStyleBackColor = True
        '
        'TPApplicationTypes
        '
        Me.TPApplicationTypes.Controls.Add(Me.btnEditAppType)
        Me.TPApplicationTypes.Controls.Add(Me.btnDeleteAppType)
        Me.TPApplicationTypes.Controls.Add(Me.dgvApplicationType)
        Me.TPApplicationTypes.Controls.Add(Me.btnClearAppTypes)
        Me.TPApplicationTypes.Controls.Add(Me.txtApplicationID)
        Me.TPApplicationTypes.Controls.Add(Me.chbActiveAppType)
        Me.TPApplicationTypes.Controls.Add(Me.Label1)
        Me.TPApplicationTypes.Controls.Add(Me.Label2)
        Me.TPApplicationTypes.Controls.Add(Me.txtApplicationDesc)
        Me.TPApplicationTypes.Controls.Add(Me.btnAddNewAppType)
        Me.TPApplicationTypes.Controls.Add(Me.btnLoadApplicationTypes)
        Me.TPApplicationTypes.Location = New System.Drawing.Point(4, 22)
        Me.TPApplicationTypes.Name = "TPApplicationTypes"
        Me.TPApplicationTypes.Padding = New System.Windows.Forms.Padding(3)
        Me.TPApplicationTypes.Size = New System.Drawing.Size(784, 519)
        Me.TPApplicationTypes.TabIndex = 0
        Me.TPApplicationTypes.Text = "Application Types"
        Me.TPApplicationTypes.UseVisualStyleBackColor = True
        '
        'btnEditAppType
        '
        Me.btnEditAppType.AutoSize = True
        Me.btnEditAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditAppType.Location = New System.Drawing.Point(423, 60)
        Me.btnEditAppType.Name = "btnEditAppType"
        Me.btnEditAppType.Size = New System.Drawing.Size(57, 23)
        Me.btnEditAppType.TabIndex = 10
        Me.btnEditAppType.Text = "Edit App"
        Me.btnEditAppType.UseVisualStyleBackColor = True
        '
        'btnDeleteAppType
        '
        Me.btnDeleteAppType.AutoSize = True
        Me.btnDeleteAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteAppType.Location = New System.Drawing.Point(641, 6)
        Me.btnDeleteAppType.Name = "btnDeleteAppType"
        Me.btnDeleteAppType.Size = New System.Drawing.Size(97, 23)
        Me.btnDeleteAppType.TabIndex = 9
        Me.btnDeleteAppType.Text = "Delete App Type"
        Me.btnDeleteAppType.UseVisualStyleBackColor = True
        '
        'dgvApplicationType
        '
        Me.dgvApplicationType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplicationType.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvApplicationType.Location = New System.Drawing.Point(3, 159)
        Me.dgvApplicationType.Name = "dgvApplicationType"
        Me.dgvApplicationType.ReadOnly = True
        Me.dgvApplicationType.Size = New System.Drawing.Size(778, 357)
        Me.dgvApplicationType.TabIndex = 4
        '
        'btnClearAppTypes
        '
        Me.btnClearAppTypes.AutoSize = True
        Me.btnClearAppTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAppTypes.Image = CType(resources.GetObject("btnClearAppTypes.Image"), System.Drawing.Image)
        Me.btnClearAppTypes.Location = New System.Drawing.Point(357, 5)
        Me.btnClearAppTypes.Name = "btnClearAppTypes"
        Me.btnClearAppTypes.Size = New System.Drawing.Size(22, 22)
        Me.btnClearAppTypes.TabIndex = 8
        Me.btnClearAppTypes.UseVisualStyleBackColor = True
        '
        'txtApplicationID
        '
        Me.txtApplicationID.Location = New System.Drawing.Point(300, 8)
        Me.txtApplicationID.Name = "txtApplicationID"
        Me.txtApplicationID.ReadOnly = True
        Me.txtApplicationID.Size = New System.Drawing.Size(51, 20)
        Me.txtApplicationID.TabIndex = 5
        '
        'chbActiveAppType
        '
        Me.chbActiveAppType.AutoSize = True
        Me.chbActiveAppType.Location = New System.Drawing.Point(481, 34)
        Me.chbActiveAppType.Name = "chbActiveAppType"
        Me.chbActiveAppType.Size = New System.Drawing.Size(81, 17)
        Me.chbActiveAppType.TabIndex = 7
        Me.chbActiveAppType.Text = "Active App."
        Me.chbActiveAppType.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(276, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(207, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Application Desc"
        '
        'txtApplicationDesc
        '
        Me.txtApplicationDesc.Location = New System.Drawing.Point(300, 34)
        Me.txtApplicationDesc.Name = "txtApplicationDesc"
        Me.txtApplicationDesc.Size = New System.Drawing.Size(175, 20)
        Me.txtApplicationDesc.TabIndex = 1
        '
        'btnAddNewAppType
        '
        Me.btnAddNewAppType.AutoSize = True
        Me.btnAddNewAppType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewAppType.Location = New System.Drawing.Point(300, 60)
        Me.btnAddNewAppType.Name = "btnAddNewAppType"
        Me.btnAddNewAppType.Size = New System.Drawing.Size(83, 23)
        Me.btnAddNewAppType.TabIndex = 2
        Me.btnAddNewAppType.Text = "Add New App"
        Me.btnAddNewAppType.UseVisualStyleBackColor = True
        '
        'btnLoadApplicationTypes
        '
        Me.btnLoadApplicationTypes.AutoSize = True
        Me.btnLoadApplicationTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadApplicationTypes.Location = New System.Drawing.Point(10, 8)
        Me.btnLoadApplicationTypes.Name = "btnLoadApplicationTypes"
        Me.btnLoadApplicationTypes.Size = New System.Drawing.Size(134, 23)
        Me.btnLoadApplicationTypes.TabIndex = 3
        Me.btnLoadApplicationTypes.Text = "Load Application Type(s)"
        Me.btnLoadApplicationTypes.UseVisualStyleBackColor = True
        '
        'IAIPLookUpTables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 545)
        Me.Controls.Add(Me.TCLookUpTables)
        Me.Name = "IAIPLookUpTables"
        Me.Text = "IAIP Look Up Tables"
        Me.TCLookUpTables.ResumeLayout(False)
        Me.TPAPBManagement.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvLookUpManagement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAPBManagement.ResumeLayout(False)
        Me.pnlAPBManagement.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TPApplicationTypes.ResumeLayout(False)
        Me.TPApplicationTypes.PerformLayout()
        CType(Me.dgvApplicationType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCLookUpTables As System.Windows.Forms.TabControl
    Friend WithEvents btnLoadApplicationTypes As System.Windows.Forms.Button
    Friend WithEvents btnAddNewAppType As System.Windows.Forms.Button
    Friend WithEvents txtApplicationDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvApplicationType As System.Windows.Forms.DataGridView
    Friend WithEvents txtApplicationID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClearAppTypes As System.Windows.Forms.Button
    Friend WithEvents chbActiveAppType As System.Windows.Forms.CheckBox
    Friend WithEvents TPApplicationTypes As System.Windows.Forms.TabPage
    Friend WithEvents btnDeleteAppType As System.Windows.Forms.Button
    Friend WithEvents btnEditAppType As System.Windows.Forms.Button
    Friend WithEvents TPAPBManagement As System.Windows.Forms.TabPage
    Friend WithEvents btnLoadAPBManagement As System.Windows.Forms.Button
    Friend WithEvents pnlAPBManagement As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboManagementType As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearManagement As System.Windows.Forms.Button
    Friend WithEvents txtAPBManagemetnID As System.Windows.Forms.TextBox
    Friend WithEvents chbAPBMangementVacant As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAPBManagementName As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveAPBManagement As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dgvLookUpManagement As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnViewAllPastTypes As System.Windows.Forms.Button
End Class
