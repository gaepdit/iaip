<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPDistrictSourceTool
    Inherits BaseForm

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TCDistrictSourcesTool = New System.Windows.Forms.TabControl()
        Me.TPManageDistricts = New System.Windows.Forms.TabPage()
        Me.clbCounties = New System.Windows.Forms.CheckedListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDistricts = New System.Windows.Forms.ComboBox()
        Me.btnSaveDistricts = New System.Windows.Forms.Button()
        Me.btnClearChecks = New System.Windows.Forms.Button()
        Me.TPNewDistricts = New System.Windows.Forms.TabPage()
        Me.PanelDistrictChanges = New System.Windows.Forms.Panel()
        Me.cboDistrictManager = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnAddUpdateInfo = New System.Windows.Forms.Button()
        Me.txtNewDistrictCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNewDistrict = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lsbDistricts = New System.Windows.Forms.ListBox()
        Me.TCDistrictSourcesTool.SuspendLayout()
        Me.TPManageDistricts.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TPNewDistricts.SuspendLayout()
        Me.PanelDistrictChanges.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TCDistrictSourcesTool
        '
        Me.TCDistrictSourcesTool.Controls.Add(Me.TPManageDistricts)
        Me.TCDistrictSourcesTool.Controls.Add(Me.TPNewDistricts)
        Me.TCDistrictSourcesTool.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDistrictSourcesTool.Location = New System.Drawing.Point(0, 0)
        Me.TCDistrictSourcesTool.Name = "TCDistrictSourcesTool"
        Me.TCDistrictSourcesTool.SelectedIndex = 0
        Me.TCDistrictSourcesTool.Size = New System.Drawing.Size(792, 566)
        Me.TCDistrictSourcesTool.TabIndex = 0
        '
        'TPManageDistricts
        '
        Me.TPManageDistricts.Controls.Add(Me.clbCounties)
        Me.TPManageDistricts.Controls.Add(Me.Panel1)
        Me.TPManageDistricts.Location = New System.Drawing.Point(4, 22)
        Me.TPManageDistricts.Name = "TPManageDistricts"
        Me.TPManageDistricts.Size = New System.Drawing.Size(784, 540)
        Me.TPManageDistricts.TabIndex = 0
        Me.TPManageDistricts.Text = "County Assignments"
        '
        'clbCounties
        '
        Me.clbCounties.CheckOnClick = True
        Me.clbCounties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbCounties.Location = New System.Drawing.Point(0, 94)
        Me.clbCounties.MultiColumn = True
        Me.clbCounties.Name = "clbCounties"
        Me.clbCounties.Size = New System.Drawing.Size(784, 446)
        Me.clbCounties.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cboDistricts)
        Me.Panel1.Controls.Add(Me.btnSaveDistricts)
        Me.Panel1.Controls.Add(Me.btnClearChecks)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 94)
        Me.Panel1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "District:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(349, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(320, 26)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "(No counties will be removed from the District, even if unchecked. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To remove a " &
    "county, add it to a different District.)"
        '
        'cboDistricts
        '
        Me.cboDistricts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistricts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistricts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistricts.Location = New System.Drawing.Point(56, 15)
        Me.cboDistricts.Name = "cboDistricts"
        Me.cboDistricts.Size = New System.Drawing.Size(296, 21)
        Me.cboDistricts.TabIndex = 1
        '
        'btnSaveDistricts
        '
        Me.btnSaveDistricts.AutoSize = True
        Me.btnSaveDistricts.Location = New System.Drawing.Point(129, 55)
        Me.btnSaveDistricts.Name = "btnSaveDistricts"
        Me.btnSaveDistricts.Size = New System.Drawing.Size(214, 23)
        Me.btnSaveDistricts.TabIndex = 2
        Me.btnSaveDistricts.Text = "Add checked counties to selected District"
        '
        'btnClearChecks
        '
        Me.btnClearChecks.AutoSize = True
        Me.btnClearChecks.Location = New System.Drawing.Point(8, 55)
        Me.btnClearChecks.Name = "btnClearChecks"
        Me.btnClearChecks.Size = New System.Drawing.Size(115, 23)
        Me.btnClearChecks.TabIndex = 1
        Me.btnClearChecks.Text = "Clear all checkboxes"
        '
        'TPNewDistricts
        '
        Me.TPNewDistricts.Controls.Add(Me.PanelDistrictChanges)
        Me.TPNewDistricts.Controls.Add(Me.GroupBox1)
        Me.TPNewDistricts.Location = New System.Drawing.Point(4, 22)
        Me.TPNewDistricts.Name = "TPNewDistricts"
        Me.TPNewDistricts.Size = New System.Drawing.Size(784, 540)
        Me.TPNewDistricts.TabIndex = 1
        Me.TPNewDistricts.Text = "Add/Remove District"
        '
        'PanelDistrictChanges
        '
        Me.PanelDistrictChanges.Controls.Add(Me.cboDistrictManager)
        Me.PanelDistrictChanges.Controls.Add(Me.Label5)
        Me.PanelDistrictChanges.Controls.Add(Me.btnAddUpdateInfo)
        Me.PanelDistrictChanges.Controls.Add(Me.txtNewDistrictCode)
        Me.PanelDistrictChanges.Controls.Add(Me.Label3)
        Me.PanelDistrictChanges.Controls.Add(Me.txtNewDistrict)
        Me.PanelDistrictChanges.Controls.Add(Me.Label2)
        Me.PanelDistrictChanges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDistrictChanges.Location = New System.Drawing.Point(177, 0)
        Me.PanelDistrictChanges.Name = "PanelDistrictChanges"
        Me.PanelDistrictChanges.Size = New System.Drawing.Size(607, 540)
        Me.PanelDistrictChanges.TabIndex = 2
        '
        'cboDistrictManager
        '
        Me.cboDistrictManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistrictManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistrictManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrictManager.Location = New System.Drawing.Point(109, 68)
        Me.cboDistrictManager.Name = "cboDistrictManager"
        Me.cboDistrictManager.Size = New System.Drawing.Size(224, 21)
        Me.cboDistrictManager.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "District Manager:"
        '
        'btnAddUpdateInfo
        '
        Me.btnAddUpdateInfo.AutoSize = True
        Me.btnAddUpdateInfo.Location = New System.Drawing.Point(109, 95)
        Me.btnAddUpdateInfo.Name = "btnAddUpdateInfo"
        Me.btnAddUpdateInfo.Size = New System.Drawing.Size(131, 23)
        Me.btnAddUpdateInfo.TabIndex = 3
        Me.btnAddUpdateInfo.Text = "Save Information"
        Me.btnAddUpdateInfo.UseVisualStyleBackColor = True
        '
        'txtNewDistrictCode
        '
        Me.txtNewDistrictCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtNewDistrictCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtNewDistrictCode.Location = New System.Drawing.Point(109, 42)
        Me.txtNewDistrictCode.MaxLength = 1
        Me.txtNewDistrictCode.Name = "txtNewDistrictCode"
        Me.txtNewDistrictCode.Size = New System.Drawing.Size(32, 20)
        Me.txtNewDistrictCode.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "District Code:"
        '
        'txtNewDistrict
        '
        Me.txtNewDistrict.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtNewDistrict.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtNewDistrict.Location = New System.Drawing.Point(109, 16)
        Me.txtNewDistrict.Name = "txtNewDistrict"
        Me.txtNewDistrict.Size = New System.Drawing.Size(240, 20)
        Me.txtNewDistrict.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "District Name:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lsbDistricts)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(177, 540)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current Districts"
        '
        'lsbDistricts
        '
        Me.lsbDistricts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsbDistricts.Location = New System.Drawing.Point(3, 16)
        Me.lsbDistricts.Name = "lsbDistricts"
        Me.lsbDistricts.Size = New System.Drawing.Size(171, 521)
        Me.lsbDistricts.TabIndex = 0
        '
        'IAIPDistrictSourceTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TCDistrictSourcesTool)
        Me.Name = "IAIPDistrictSourceTool"
        Me.Text = "IAIP District Source Tool"
        Me.TCDistrictSourcesTool.ResumeLayout(False)
        Me.TPManageDistricts.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TPNewDistricts.ResumeLayout(False)
        Me.PanelDistrictChanges.ResumeLayout(False)
        Me.PanelDistrictChanges.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCDistrictSourcesTool As System.Windows.Forms.TabControl
    Friend WithEvents TPManageDistricts As System.Windows.Forms.TabPage
    Friend WithEvents clbCounties As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSaveDistricts As System.Windows.Forms.Button
    Friend WithEvents btnClearChecks As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDistricts As System.Windows.Forms.ComboBox
    Friend WithEvents TPNewDistricts As System.Windows.Forms.TabPage
    Friend WithEvents PanelDistrictChanges As System.Windows.Forms.Panel
    Friend WithEvents txtNewDistrictCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNewDistrict As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lsbDistricts As System.Windows.Forms.ListBox
    Friend WithEvents btnAddUpdateInfo As System.Windows.Forms.Button
    Friend WithEvents cboDistrictManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label4 As Label
End Class
