<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPListTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPListTool))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbClear = New System.Windows.Forms.ToolStripButton
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tsbRefreshForm = New System.Windows.Forms.ToolStripMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.btnClearBranch = New System.Windows.Forms.Button
        Me.btnClearAccount = New System.Windows.Forms.Button
        Me.btnClearUnit = New System.Windows.Forms.Button
        Me.btnClearProgram = New System.Windows.Forms.Button
        Me.txtAccountCode = New System.Windows.Forms.TextBox
        Me.txtUnitCode = New System.Windows.Forms.TextBox
        Me.txtProgramCode = New System.Windows.Forms.TextBox
        Me.txtBranchCode = New System.Windows.Forms.TextBox
        Me.btnDeleteAccount = New System.Windows.Forms.Button
        Me.btnDeleteProgram = New System.Windows.Forms.Button
        Me.btnDeleteUnit = New System.Windows.Forms.Button
        Me.btnDeleteBranch = New System.Windows.Forms.Button
        Me.btnEditAccount = New System.Windows.Forms.Button
        Me.btnEditProgram = New System.Windows.Forms.Button
        Me.btnEditUnit = New System.Windows.Forms.Button
        Me.btnEditBranch = New System.Windows.Forms.Button
        Me.btnAddAccount = New System.Windows.Forms.Button
        Me.btnAddProgram = New System.Windows.Forms.Button
        Me.btnAddUnit = New System.Windows.Forms.Button
        Me.btnAddBranch = New System.Windows.Forms.Button
        Me.txtAccount = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtUnit = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProgram = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBranch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.dgvBranch = New System.Windows.Forms.DataGridView
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.dgvProgram = New System.Windows.Forms.DataGridView
        Me.dgvUnit = New System.Windows.Forms.DataGridView
        Me.dgvAccounts = New System.Windows.Forms.DataGridView
        Me.TCOrganizationTool = New System.Windows.Forms.TabControl
        Me.TPAccounts = New System.Windows.Forms.TabPage
        Me.TPAssignForms = New System.Windows.Forms.TabPage
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnViewAccountForms = New System.Windows.Forms.Button
        Me.lblTemp = New System.Windows.Forms.Label
        Me.lblFormAccess = New System.Windows.Forms.Label
        Me.cboBranch = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnUpdateAccount = New System.Windows.Forms.Button
        Me.clbAccounts = New System.Windows.Forms.CheckedListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboUnit = New System.Windows.Forms.ComboBox
        Me.chbCascadeProgram = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.chbCascadeBranch = New System.Windows.Forms.CheckBox
        Me.cboProgram = New System.Windows.Forms.ComboBox
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer
        Me.dgvAvailableForms = New System.Windows.Forms.DataGridView
        Me.Label8 = New System.Windows.Forms.Label
        Me.dgvSelectedForms = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblSelectedFormCount = New System.Windows.Forms.Label
        Me.btnSelectForm = New System.Windows.Forms.Button
        Me.btnUnselectForm = New System.Windows.Forms.Button
        Me.btnSelectAllForms = New System.Windows.Forms.Button
        Me.btnUnselectAllForms = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dgvBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgvProgram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TCOrganizationTool.SuspendLayout()
        Me.TPAccounts.SuspendLayout()
        Me.TPAssignForms.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.dgvAvailableForms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSelectedForms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClear})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbClear
        '
        Me.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), System.Drawing.Image)
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.Text = "ToolStripButton3"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolToolStripMenuItem
        '
        Me.ToolToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbRefreshForm})
        Me.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem"
        Me.ToolToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.ToolToolStripMenuItem.Text = "Tool"
        '
        'tsbRefreshForm
        '
        Me.tsbRefreshForm.Name = "tsbRefreshForm"
        Me.tsbRefreshForm.Size = New System.Drawing.Size(144, 22)
        Me.tsbRefreshForm.Text = "Refresh Form"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvAccounts)
        Me.SplitContainer1.Size = New System.Drawing.Size(778, 485)
        Me.SplitContainer1.SplitterDistance = 360
        Me.SplitContainer1.SplitterWidth = 10
        Me.SplitContainer1.TabIndex = 6
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnClearBranch)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnClearAccount)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnClearUnit)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnClearProgram)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtAccountCode)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtUnitCode)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtProgramCode)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtBranchCode)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnDeleteAccount)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnDeleteProgram)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnDeleteUnit)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnDeleteBranch)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnEditAccount)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnEditProgram)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnEditUnit)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnEditBranch)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnAddAccount)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnAddProgram)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnAddUnit)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnAddBranch)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtAccount)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtUnit)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtProgram)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtBranch)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer4.Size = New System.Drawing.Size(778, 360)
        Me.SplitContainer4.SplitterDistance = 114
        Me.SplitContainer4.SplitterWidth = 10
        Me.SplitContainer4.TabIndex = 0
        '
        'btnClearBranch
        '
        Me.btnClearBranch.AutoSize = True
        Me.btnClearBranch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearBranch.Image = CType(resources.GetObject("btnClearBranch.Image"), System.Drawing.Image)
        Me.btnClearBranch.Location = New System.Drawing.Point(231, 7)
        Me.btnClearBranch.Name = "btnClearBranch"
        Me.btnClearBranch.Size = New System.Drawing.Size(22, 22)
        Me.btnClearBranch.TabIndex = 27
        Me.btnClearBranch.UseVisualStyleBackColor = True
        '
        'btnClearAccount
        '
        Me.btnClearAccount.AutoSize = True
        Me.btnClearAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAccount.Image = CType(resources.GetObject("btnClearAccount.Image"), System.Drawing.Image)
        Me.btnClearAccount.Location = New System.Drawing.Point(231, 85)
        Me.btnClearAccount.Name = "btnClearAccount"
        Me.btnClearAccount.Size = New System.Drawing.Size(22, 22)
        Me.btnClearAccount.TabIndex = 26
        Me.btnClearAccount.UseVisualStyleBackColor = True
        '
        'btnClearUnit
        '
        Me.btnClearUnit.AutoSize = True
        Me.btnClearUnit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearUnit.Image = CType(resources.GetObject("btnClearUnit.Image"), System.Drawing.Image)
        Me.btnClearUnit.Location = New System.Drawing.Point(231, 59)
        Me.btnClearUnit.Name = "btnClearUnit"
        Me.btnClearUnit.Size = New System.Drawing.Size(22, 22)
        Me.btnClearUnit.TabIndex = 25
        Me.btnClearUnit.UseVisualStyleBackColor = True
        '
        'btnClearProgram
        '
        Me.btnClearProgram.AutoSize = True
        Me.btnClearProgram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearProgram.Image = CType(resources.GetObject("btnClearProgram.Image"), System.Drawing.Image)
        Me.btnClearProgram.Location = New System.Drawing.Point(231, 33)
        Me.btnClearProgram.Name = "btnClearProgram"
        Me.btnClearProgram.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProgram.TabIndex = 24
        Me.btnClearProgram.UseVisualStyleBackColor = True
        '
        'txtAccountCode
        '
        Me.txtAccountCode.Location = New System.Drawing.Point(195, 86)
        Me.txtAccountCode.Name = "txtAccountCode"
        Me.txtAccountCode.ReadOnly = True
        Me.txtAccountCode.Size = New System.Drawing.Size(31, 20)
        Me.txtAccountCode.TabIndex = 23
        '
        'txtUnitCode
        '
        Me.txtUnitCode.Location = New System.Drawing.Point(195, 60)
        Me.txtUnitCode.Name = "txtUnitCode"
        Me.txtUnitCode.ReadOnly = True
        Me.txtUnitCode.Size = New System.Drawing.Size(31, 20)
        Me.txtUnitCode.TabIndex = 22
        '
        'txtProgramCode
        '
        Me.txtProgramCode.Location = New System.Drawing.Point(195, 34)
        Me.txtProgramCode.Name = "txtProgramCode"
        Me.txtProgramCode.ReadOnly = True
        Me.txtProgramCode.Size = New System.Drawing.Size(31, 20)
        Me.txtProgramCode.TabIndex = 21
        '
        'txtBranchCode
        '
        Me.txtBranchCode.Location = New System.Drawing.Point(195, 8)
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.ReadOnly = True
        Me.txtBranchCode.Size = New System.Drawing.Size(31, 20)
        Me.txtBranchCode.TabIndex = 20
        '
        'btnDeleteAccount
        '
        Me.btnDeleteAccount.AutoSize = True
        Me.btnDeleteAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteAccount.Location = New System.Drawing.Point(532, 85)
        Me.btnDeleteAccount.Name = "btnDeleteAccount"
        Me.btnDeleteAccount.Size = New System.Drawing.Size(91, 23)
        Me.btnDeleteAccount.TabIndex = 19
        Me.btnDeleteAccount.Text = "Delete Account"
        Me.btnDeleteAccount.UseVisualStyleBackColor = True
        '
        'btnDeleteProgram
        '
        Me.btnDeleteProgram.AutoSize = True
        Me.btnDeleteProgram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteProgram.Location = New System.Drawing.Point(532, 33)
        Me.btnDeleteProgram.Name = "btnDeleteProgram"
        Me.btnDeleteProgram.Size = New System.Drawing.Size(90, 23)
        Me.btnDeleteProgram.TabIndex = 18
        Me.btnDeleteProgram.Text = "Delete Program"
        Me.btnDeleteProgram.UseVisualStyleBackColor = True
        '
        'btnDeleteUnit
        '
        Me.btnDeleteUnit.AutoSize = True
        Me.btnDeleteUnit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteUnit.Location = New System.Drawing.Point(532, 59)
        Me.btnDeleteUnit.Name = "btnDeleteUnit"
        Me.btnDeleteUnit.Size = New System.Drawing.Size(70, 23)
        Me.btnDeleteUnit.TabIndex = 17
        Me.btnDeleteUnit.Text = "Delete Unit"
        Me.btnDeleteUnit.UseVisualStyleBackColor = True
        '
        'btnDeleteBranch
        '
        Me.btnDeleteBranch.AutoSize = True
        Me.btnDeleteBranch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteBranch.Location = New System.Drawing.Point(532, 7)
        Me.btnDeleteBranch.Name = "btnDeleteBranch"
        Me.btnDeleteBranch.Size = New System.Drawing.Size(85, 23)
        Me.btnDeleteBranch.TabIndex = 16
        Me.btnDeleteBranch.Text = "Delete Branch"
        Me.btnDeleteBranch.UseVisualStyleBackColor = True
        '
        'btnEditAccount
        '
        Me.btnEditAccount.AutoSize = True
        Me.btnEditAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditAccount.Location = New System.Drawing.Point(418, 85)
        Me.btnEditAccount.Name = "btnEditAccount"
        Me.btnEditAccount.Size = New System.Drawing.Size(78, 23)
        Me.btnEditAccount.TabIndex = 15
        Me.btnEditAccount.Text = "Edit Account"
        Me.btnEditAccount.UseVisualStyleBackColor = True
        '
        'btnEditProgram
        '
        Me.btnEditProgram.AutoSize = True
        Me.btnEditProgram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditProgram.Location = New System.Drawing.Point(418, 33)
        Me.btnEditProgram.Name = "btnEditProgram"
        Me.btnEditProgram.Size = New System.Drawing.Size(77, 23)
        Me.btnEditProgram.TabIndex = 14
        Me.btnEditProgram.Text = "Edit Program"
        Me.btnEditProgram.UseVisualStyleBackColor = True
        '
        'btnEditUnit
        '
        Me.btnEditUnit.AutoSize = True
        Me.btnEditUnit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditUnit.Location = New System.Drawing.Point(418, 59)
        Me.btnEditUnit.Name = "btnEditUnit"
        Me.btnEditUnit.Size = New System.Drawing.Size(57, 23)
        Me.btnEditUnit.TabIndex = 13
        Me.btnEditUnit.Text = "Edit Unit"
        Me.btnEditUnit.UseVisualStyleBackColor = True
        '
        'btnEditBranch
        '
        Me.btnEditBranch.AutoSize = True
        Me.btnEditBranch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditBranch.Location = New System.Drawing.Point(418, 7)
        Me.btnEditBranch.Name = "btnEditBranch"
        Me.btnEditBranch.Size = New System.Drawing.Size(72, 23)
        Me.btnEditBranch.TabIndex = 12
        Me.btnEditBranch.Text = "Edit Branch"
        Me.btnEditBranch.UseVisualStyleBackColor = True
        '
        'btnAddAccount
        '
        Me.btnAddAccount.AutoSize = True
        Me.btnAddAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddAccount.Location = New System.Drawing.Point(275, 85)
        Me.btnAddAccount.Name = "btnAddAccount"
        Me.btnAddAccount.Size = New System.Drawing.Size(104, 23)
        Me.btnAddAccount.TabIndex = 11
        Me.btnAddAccount.Text = "Add New Account"
        Me.btnAddAccount.UseVisualStyleBackColor = True
        '
        'btnAddProgram
        '
        Me.btnAddProgram.AutoSize = True
        Me.btnAddProgram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddProgram.Location = New System.Drawing.Point(275, 33)
        Me.btnAddProgram.Name = "btnAddProgram"
        Me.btnAddProgram.Size = New System.Drawing.Size(103, 23)
        Me.btnAddProgram.TabIndex = 10
        Me.btnAddProgram.Text = "Add New Program"
        Me.btnAddProgram.UseVisualStyleBackColor = True
        '
        'btnAddUnit
        '
        Me.btnAddUnit.AutoSize = True
        Me.btnAddUnit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddUnit.Location = New System.Drawing.Point(275, 59)
        Me.btnAddUnit.Name = "btnAddUnit"
        Me.btnAddUnit.Size = New System.Drawing.Size(83, 23)
        Me.btnAddUnit.TabIndex = 9
        Me.btnAddUnit.Text = "Add New Unit"
        Me.btnAddUnit.UseVisualStyleBackColor = True
        '
        'btnAddBranch
        '
        Me.btnAddBranch.AutoSize = True
        Me.btnAddBranch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddBranch.Location = New System.Drawing.Point(275, 7)
        Me.btnAddBranch.Name = "btnAddBranch"
        Me.btnAddBranch.Size = New System.Drawing.Size(98, 23)
        Me.btnAddBranch.TabIndex = 8
        Me.btnAddBranch.Text = "Add New Branch"
        Me.btnAddBranch.UseVisualStyleBackColor = True
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(55, 86)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(133, 20)
        Me.txtAccount.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Account:"
        '
        'txtUnit
        '
        Me.txtUnit.Location = New System.Drawing.Point(55, 60)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(133, 20)
        Me.txtUnit.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Unit:"
        '
        'txtProgram
        '
        Me.txtProgram.Location = New System.Drawing.Point(55, 34)
        Me.txtProgram.Name = "txtProgram"
        Me.txtProgram.Size = New System.Drawing.Size(133, 20)
        Me.txtProgram.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Program:"
        '
        'txtBranch
        '
        Me.txtBranch.Location = New System.Drawing.Point(55, 8)
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(133, 20)
        Me.txtBranch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Branch:"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvBranch)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(778, 236)
        Me.SplitContainer2.SplitterDistance = 259
        Me.SplitContainer2.SplitterWidth = 10
        Me.SplitContainer2.TabIndex = 1
        '
        'dgvBranch
        '
        Me.dgvBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBranch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBranch.Location = New System.Drawing.Point(0, 0)
        Me.dgvBranch.Name = "dgvBranch"
        Me.dgvBranch.ReadOnly = True
        Me.dgvBranch.Size = New System.Drawing.Size(259, 236)
        Me.dgvBranch.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.dgvProgram)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.dgvUnit)
        Me.SplitContainer3.Size = New System.Drawing.Size(509, 236)
        Me.SplitContainer3.SplitterDistance = 249
        Me.SplitContainer3.SplitterWidth = 10
        Me.SplitContainer3.TabIndex = 0
        '
        'dgvProgram
        '
        Me.dgvProgram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProgram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProgram.Location = New System.Drawing.Point(0, 0)
        Me.dgvProgram.Name = "dgvProgram"
        Me.dgvProgram.ReadOnly = True
        Me.dgvProgram.Size = New System.Drawing.Size(249, 236)
        Me.dgvProgram.TabIndex = 1
        '
        'dgvUnit
        '
        Me.dgvUnit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUnit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUnit.Location = New System.Drawing.Point(0, 0)
        Me.dgvUnit.Name = "dgvUnit"
        Me.dgvUnit.ReadOnly = True
        Me.dgvUnit.Size = New System.Drawing.Size(250, 236)
        Me.dgvUnit.TabIndex = 0
        '
        'dgvAccounts
        '
        Me.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccounts.Location = New System.Drawing.Point(0, 0)
        Me.dgvAccounts.Name = "dgvAccounts"
        Me.dgvAccounts.ReadOnly = True
        Me.dgvAccounts.Size = New System.Drawing.Size(778, 115)
        Me.dgvAccounts.TabIndex = 2
        '
        'TCOrganizationTool
        '
        Me.TCOrganizationTool.Controls.Add(Me.TPAccounts)
        Me.TCOrganizationTool.Controls.Add(Me.TPAssignForms)
        Me.TCOrganizationTool.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCOrganizationTool.Location = New System.Drawing.Point(0, 49)
        Me.TCOrganizationTool.Name = "TCOrganizationTool"
        Me.TCOrganizationTool.SelectedIndex = 0
        Me.TCOrganizationTool.Size = New System.Drawing.Size(792, 517)
        Me.TCOrganizationTool.TabIndex = 7
        '
        'TPAccounts
        '
        Me.TPAccounts.Controls.Add(Me.SplitContainer1)
        Me.TPAccounts.Location = New System.Drawing.Point(4, 22)
        Me.TPAccounts.Name = "TPAccounts"
        Me.TPAccounts.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAccounts.Size = New System.Drawing.Size(784, 491)
        Me.TPAccounts.TabIndex = 0
        Me.TPAccounts.Text = "Add/Edit Accounts"
        Me.TPAccounts.UseVisualStyleBackColor = True
        '
        'TPAssignForms
        '
        Me.TPAssignForms.Controls.Add(Me.SplitContainer5)
        Me.TPAssignForms.Location = New System.Drawing.Point(4, 22)
        Me.TPAssignForms.Name = "TPAssignForms"
        Me.TPAssignForms.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAssignForms.Size = New System.Drawing.Size(784, 469)
        Me.TPAssignForms.TabIndex = 1
        Me.TPAssignForms.Text = "Assign Accounts Permission"
        Me.TPAssignForms.UseVisualStyleBackColor = True
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnViewAccountForms)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblTemp)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblFormAccess)
        Me.SplitContainer5.Panel1.Controls.Add(Me.cboBranch)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnUpdateAccount)
        Me.SplitContainer5.Panel1.Controls.Add(Me.clbAccounts)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer5.Panel1.Controls.Add(Me.cboUnit)
        Me.SplitContainer5.Panel1.Controls.Add(Me.chbCascadeProgram)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer5.Panel1.Controls.Add(Me.chbCascadeBranch)
        Me.SplitContainer5.Panel1.Controls.Add(Me.cboProgram)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.SplitContainer6)
        Me.SplitContainer5.Size = New System.Drawing.Size(778, 463)
        Me.SplitContainer5.SplitterDistance = 229
        Me.SplitContainer5.SplitterWidth = 10
        Me.SplitContainer5.TabIndex = 44
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(339, 166)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(180, 26)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Warning - Only the last account " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                 selected will be displayed."
        '
        'btnViewAccountForms
        '
        Me.btnViewAccountForms.AutoSize = True
        Me.btnViewAccountForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewAccountForms.Location = New System.Drawing.Point(339, 140)
        Me.btnViewAccountForms.Name = "btnViewAccountForms"
        Me.btnViewAccountForms.Size = New System.Drawing.Size(120, 23)
        Me.btnViewAccountForms.TabIndex = 45
        Me.btnViewAccountForms.Text = "View Account Form(s)"
        Me.btnViewAccountForms.UseVisualStyleBackColor = True
        '
        'lblTemp
        '
        Me.lblTemp.AutoSize = True
        Me.lblTemp.Location = New System.Drawing.Point(476, 140)
        Me.lblTemp.Name = "lblTemp"
        Me.lblTemp.Size = New System.Drawing.Size(0, 13)
        Me.lblTemp.TabIndex = 42
        '
        'lblFormAccess
        '
        Me.lblFormAccess.AutoSize = True
        Me.lblFormAccess.Location = New System.Drawing.Point(478, 100)
        Me.lblFormAccess.Name = "lblFormAccess"
        Me.lblFormAccess.Size = New System.Drawing.Size(0, 13)
        Me.lblFormAccess.TabIndex = 41
        Me.lblFormAccess.Visible = False
        '
        'cboBranch
        '
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(66, 6)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(213, 21)
        Me.cboBranch.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Branch:"
        '
        'btnUpdateAccount
        '
        Me.btnUpdateAccount.AutoSize = True
        Me.btnUpdateAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateAccount.Location = New System.Drawing.Point(339, 95)
        Me.btnUpdateAccount.Name = "btnUpdateAccount"
        Me.btnUpdateAccount.Size = New System.Drawing.Size(133, 23)
        Me.btnUpdateAccount.TabIndex = 40
        Me.btnUpdateAccount.Text = "Update Account Access"
        Me.btnUpdateAccount.UseVisualStyleBackColor = True
        '
        'clbAccounts
        '
        Me.clbAccounts.CheckOnClick = True
        Me.clbAccounts.FormattingEnabled = True
        Me.clbAccounts.Location = New System.Drawing.Point(5, 95)
        Me.clbAccounts.Name = "clbAccounts"
        Me.clbAccounts.Size = New System.Drawing.Size(328, 124)
        Me.clbAccounts.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Unit:"
        '
        'cboUnit
        '
        Me.cboUnit.FormattingEnabled = True
        Me.cboUnit.Location = New System.Drawing.Point(66, 60)
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.Size = New System.Drawing.Size(213, 21)
        Me.cboUnit.TabIndex = 3
        '
        'chbCascadeProgram
        '
        Me.chbCascadeProgram.AutoSize = True
        Me.chbCascadeProgram.Location = New System.Drawing.Point(293, 35)
        Me.chbCascadeProgram.Name = "chbCascadeProgram"
        Me.chbCascadeProgram.Size = New System.Drawing.Size(153, 17)
        Me.chbCascadeProgram.TabIndex = 38
        Me.chbCascadeProgram.Text = "Cascade Through Program"
        Me.chbCascadeProgram.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Program:"
        '
        'chbCascadeBranch
        '
        Me.chbCascadeBranch.AutoSize = True
        Me.chbCascadeBranch.Location = New System.Drawing.Point(293, 8)
        Me.chbCascadeBranch.Name = "chbCascadeBranch"
        Me.chbCascadeBranch.Size = New System.Drawing.Size(148, 17)
        Me.chbCascadeBranch.TabIndex = 37
        Me.chbCascadeBranch.Text = "Cascade Through Branch"
        Me.chbCascadeBranch.UseVisualStyleBackColor = True
        '
        'cboProgram
        '
        Me.cboProgram.FormattingEnabled = True
        Me.cboProgram.Location = New System.Drawing.Point(66, 33)
        Me.cboProgram.Name = "cboProgram"
        Me.cboProgram.Size = New System.Drawing.Size(213, 21)
        Me.cboProgram.TabIndex = 5
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.dgvAvailableForms)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label8)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.dgvSelectedForms)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label9)
        Me.SplitContainer6.Size = New System.Drawing.Size(778, 224)
        Me.SplitContainer6.SplitterDistance = 266
        Me.SplitContainer6.SplitterWidth = 10
        Me.SplitContainer6.TabIndex = 43
        '
        'dgvAvailableForms
        '
        Me.dgvAvailableForms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAvailableForms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAvailableForms.Location = New System.Drawing.Point(0, 13)
        Me.dgvAvailableForms.Name = "dgvAvailableForms"
        Me.dgvAvailableForms.ReadOnly = True
        Me.dgvAvailableForms.Size = New System.Drawing.Size(266, 211)
        Me.dgvAvailableForms.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Available Forms"
        '
        'dgvSelectedForms
        '
        Me.dgvSelectedForms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSelectedForms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSelectedForms.Location = New System.Drawing.Point(66, 13)
        Me.dgvSelectedForms.Name = "dgvSelectedForms"
        Me.dgvSelectedForms.Size = New System.Drawing.Size(436, 211)
        Me.dgvSelectedForms.TabIndex = 31
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblSelectedFormCount)
        Me.Panel1.Controls.Add(Me.btnSelectForm)
        Me.Panel1.Controls.Add(Me.btnUnselectForm)
        Me.Panel1.Controls.Add(Me.btnSelectAllForms)
        Me.Panel1.Controls.Add(Me.btnUnselectAllForms)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(66, 211)
        Me.Panel1.TabIndex = 44
        '
        'lblSelectedFormCount
        '
        Me.lblSelectedFormCount.AutoSize = True
        Me.lblSelectedFormCount.Location = New System.Drawing.Point(0, 193)
        Me.lblSelectedFormCount.Name = "lblSelectedFormCount"
        Me.lblSelectedFormCount.Size = New System.Drawing.Size(0, 13)
        Me.lblSelectedFormCount.TabIndex = 43
        '
        'btnSelectForm
        '
        Me.btnSelectForm.AutoSize = True
        Me.btnSelectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectForm.Image = CType(resources.GetObject("btnSelectForm.Image"), System.Drawing.Image)
        Me.btnSelectForm.Location = New System.Drawing.Point(18, 27)
        Me.btnSelectForm.Name = "btnSelectForm"
        Me.btnSelectForm.Size = New System.Drawing.Size(30, 28)
        Me.btnSelectForm.TabIndex = 32
        Me.btnSelectForm.UseVisualStyleBackColor = True
        '
        'btnUnselectForm
        '
        Me.btnUnselectForm.AutoSize = True
        Me.btnUnselectForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectForm.Image = CType(resources.GetObject("btnUnselectForm.Image"), System.Drawing.Image)
        Me.btnUnselectForm.Location = New System.Drawing.Point(18, 109)
        Me.btnUnselectForm.Name = "btnUnselectForm"
        Me.btnUnselectForm.Size = New System.Drawing.Size(30, 28)
        Me.btnUnselectForm.TabIndex = 34
        Me.btnUnselectForm.UseVisualStyleBackColor = True
        '
        'btnSelectAllForms
        '
        Me.btnSelectAllForms.AutoSize = True
        Me.btnSelectAllForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectAllForms.Image = CType(resources.GetObject("btnSelectAllForms.Image"), System.Drawing.Image)
        Me.btnSelectAllForms.Location = New System.Drawing.Point(18, 68)
        Me.btnSelectAllForms.Name = "btnSelectAllForms"
        Me.btnSelectAllForms.Size = New System.Drawing.Size(30, 28)
        Me.btnSelectAllForms.TabIndex = 33
        Me.btnSelectAllForms.UseVisualStyleBackColor = True
        '
        'btnUnselectAllForms
        '
        Me.btnUnselectAllForms.AutoSize = True
        Me.btnUnselectAllForms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUnselectAllForms.Image = CType(resources.GetObject("btnUnselectAllForms.Image"), System.Drawing.Image)
        Me.btnUnselectAllForms.Location = New System.Drawing.Point(18, 150)
        Me.btnUnselectAllForms.Name = "btnUnselectAllForms"
        Me.btnUnselectAllForms.Size = New System.Drawing.Size(30, 28)
        Me.btnUnselectAllForms.TabIndex = 35
        Me.btnUnselectAllForms.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Selected Forms"
        '
        'IAIPListTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TCOrganizationTool)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "IAIPListTool"
        Me.Text = "IAIP Organization Tool"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgvBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgvProgram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TCOrganizationTool.ResumeLayout(False)
        Me.TPAccounts.ResumeLayout(False)
        Me.TPAssignForms.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.Panel2.PerformLayout()
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.dgvAvailableForms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSelectedForms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAddBranch As System.Windows.Forms.Button
    Friend WithEvents txtAccount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProgram As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvBranch As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvProgram As System.Windows.Forms.DataGridView
    Friend WithEvents dgvUnit As System.Windows.Forms.DataGridView
    Friend WithEvents dgvAccounts As System.Windows.Forms.DataGridView
    Friend WithEvents txtAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtUnitCode As System.Windows.Forms.TextBox
    Friend WithEvents txtProgramCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBranchCode As System.Windows.Forms.TextBox
    Friend WithEvents btnDeleteAccount As System.Windows.Forms.Button
    Friend WithEvents btnDeleteProgram As System.Windows.Forms.Button
    Friend WithEvents btnDeleteUnit As System.Windows.Forms.Button
    Friend WithEvents btnDeleteBranch As System.Windows.Forms.Button
    Friend WithEvents btnEditAccount As System.Windows.Forms.Button
    Friend WithEvents btnEditProgram As System.Windows.Forms.Button
    Friend WithEvents btnEditUnit As System.Windows.Forms.Button
    Friend WithEvents btnEditBranch As System.Windows.Forms.Button
    Friend WithEvents btnAddAccount As System.Windows.Forms.Button
    Friend WithEvents btnAddProgram As System.Windows.Forms.Button
    Friend WithEvents btnAddUnit As System.Windows.Forms.Button
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClearAccount As System.Windows.Forms.Button
    Friend WithEvents btnClearUnit As System.Windows.Forms.Button
    Friend WithEvents btnClearProgram As System.Windows.Forms.Button
    Friend WithEvents btnClearBranch As System.Windows.Forms.Button
    Friend WithEvents TCOrganizationTool As System.Windows.Forms.TabControl
    Friend WithEvents TPAccounts As System.Windows.Forms.TabPage
    Friend WithEvents TPAssignForms As System.Windows.Forms.TabPage
    Friend WithEvents clbAccounts As System.Windows.Forms.CheckedListBox
    Friend WithEvents cboProgram As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSelectForm As System.Windows.Forms.Button
    Friend WithEvents dgvAvailableForms As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSelectedForms As System.Windows.Forms.DataGridView
    Friend WithEvents btnUnselectAllForms As System.Windows.Forms.Button
    Friend WithEvents btnUnselectForm As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllForms As System.Windows.Forms.Button
    Friend WithEvents chbCascadeProgram As System.Windows.Forms.CheckBox
    Friend WithEvents chbCascadeBranch As System.Windows.Forms.CheckBox
    Friend WithEvents btnUpdateAccount As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblSelectedFormCount As System.Windows.Forms.Label
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblFormAccess As System.Windows.Forms.Label
    Friend WithEvents lblTemp As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnViewAccountForms As System.Windows.Forms.Button
    Friend WithEvents tsbRefreshForm As System.Windows.Forms.ToolStripMenuItem
End Class
