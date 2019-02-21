<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSPPAttainmentStatus
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnPMFineChanges = New System.Windows.Forms.Button()
        Me.rdbPMFineNo = New System.Windows.Forms.RadioButton()
        Me.rdbPMFineMacon = New System.Windows.Forms.RadioButton()
        Me.rdbPMFineFloyd = New System.Windows.Forms.RadioButton()
        Me.rdbPMFineChattanooga = New System.Windows.Forms.RadioButton()
        Me.rdbPMFineAtlanta = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnEightHourChanges = New System.Windows.Forms.Button()
        Me.rdbEightHourNo = New System.Windows.Forms.RadioButton()
        Me.rdbEightHourMacon = New System.Windows.Forms.RadioButton()
        Me.rdbEightHourAtlanta = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnOneHourChanges = New System.Windows.Forms.Button()
        Me.rdbOneHourNo = New System.Windows.Forms.RadioButton()
        Me.rdbOneHourCont = New System.Windows.Forms.RadioButton()
        Me.rdbOneHourYes = New System.Windows.Forms.RadioButton()
        Me.btnSaveChanges = New System.Windows.Forms.Button()
        Me.btnViewCountyInfo = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCounty = New System.Windows.Forms.ComboBox()
        Me.btnViewSelectedData = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboNonAttainmentStatus = New System.Windows.Forms.ComboBox()
        Me.dgvAttainmentStatus = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.bSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvAttainmentStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnSaveChanges)
        Me.GroupBox1.Controls.Add(Me.btnViewCountyInfo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cboCounty)
        Me.GroupBox1.Controls.Add(Me.btnViewSelectedData)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboNonAttainmentStatus)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 249)
        Me.GroupBox1.TabIndex = 257
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Attainment Status"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnPMFineChanges)
        Me.GroupBox4.Controls.Add(Me.rdbPMFineNo)
        Me.GroupBox4.Controls.Add(Me.rdbPMFineMacon)
        Me.GroupBox4.Controls.Add(Me.rdbPMFineFloyd)
        Me.GroupBox4.Controls.Add(Me.rdbPMFineChattanooga)
        Me.GroupBox4.Controls.Add(Me.rdbPMFineAtlanta)
        Me.GroupBox4.Location = New System.Drawing.Point(324, 63)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(245, 178)
        Me.GroupBox4.TabIndex = 21
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "PM 2.5 Attainment Status"
        '
        'btnPMFineChanges
        '
        Me.btnPMFineChanges.AutoSize = True
        Me.btnPMFineChanges.Location = New System.Drawing.Point(138, 44)
        Me.btnPMFineChanges.Name = "btnPMFineChanges"
        Me.btnPMFineChanges.Size = New System.Drawing.Size(97, 49)
        Me.btnPMFineChanges.TabIndex = 29
        Me.btnPMFineChanges.Text = "Save " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PM 2.5 Changes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Only"
        Me.btnPMFineChanges.UseVisualStyleBackColor = True
        '
        'rdbPMFineNo
        '
        Me.rdbPMFineNo.AutoSize = True
        Me.rdbPMFineNo.Location = New System.Drawing.Point(9, 107)
        Me.rdbPMFineNo.Name = "rdbPMFineNo"
        Me.rdbPMFineNo.Size = New System.Drawing.Size(82, 17)
        Me.rdbPMFineNo.TabIndex = 28
        Me.rdbPMFineNo.TabStop = True
        Me.rdbPMFineNo.Text = "PM 2.5 (No)"
        Me.rdbPMFineNo.UseVisualStyleBackColor = True
        '
        'rdbPMFineMacon
        '
        Me.rdbPMFineMacon.AutoSize = True
        Me.rdbPMFineMacon.Location = New System.Drawing.Point(9, 84)
        Me.rdbPMFineMacon.Name = "rdbPMFineMacon"
        Me.rdbPMFineMacon.Size = New System.Drawing.Size(95, 17)
        Me.rdbPMFineMacon.TabIndex = 27
        Me.rdbPMFineMacon.TabStop = True
        Me.rdbPMFineMacon.Text = "PM 2.5 Macon"
        Me.rdbPMFineMacon.UseVisualStyleBackColor = True
        '
        'rdbPMFineFloyd
        '
        Me.rdbPMFineFloyd.AutoSize = True
        Me.rdbPMFineFloyd.Location = New System.Drawing.Point(9, 62)
        Me.rdbPMFineFloyd.Name = "rdbPMFineFloyd"
        Me.rdbPMFineFloyd.Size = New System.Drawing.Size(87, 17)
        Me.rdbPMFineFloyd.TabIndex = 26
        Me.rdbPMFineFloyd.TabStop = True
        Me.rdbPMFineFloyd.Text = "PM 2.5 Floyd"
        Me.rdbPMFineFloyd.UseVisualStyleBackColor = True
        '
        'rdbPMFineChattanooga
        '
        Me.rdbPMFineChattanooga.AutoSize = True
        Me.rdbPMFineChattanooga.Location = New System.Drawing.Point(9, 39)
        Me.rdbPMFineChattanooga.Name = "rdbPMFineChattanooga"
        Me.rdbPMFineChattanooga.Size = New System.Drawing.Size(123, 17)
        Me.rdbPMFineChattanooga.TabIndex = 25
        Me.rdbPMFineChattanooga.TabStop = True
        Me.rdbPMFineChattanooga.Text = "PM 2.5 Chattanooga"
        Me.rdbPMFineChattanooga.UseVisualStyleBackColor = True
        '
        'rdbPMFineAtlanta
        '
        Me.rdbPMFineAtlanta.AutoSize = True
        Me.rdbPMFineAtlanta.Location = New System.Drawing.Point(9, 19)
        Me.rdbPMFineAtlanta.Name = "rdbPMFineAtlanta"
        Me.rdbPMFineAtlanta.Size = New System.Drawing.Size(95, 17)
        Me.rdbPMFineAtlanta.TabIndex = 24
        Me.rdbPMFineAtlanta.TabStop = True
        Me.rdbPMFineAtlanta.Text = "PM 2.5 Atlanta"
        Me.rdbPMFineAtlanta.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnEightHourChanges)
        Me.GroupBox3.Controls.Add(Me.rdbEightHourNo)
        Me.GroupBox3.Controls.Add(Me.rdbEightHourMacon)
        Me.GroupBox3.Controls.Add(Me.rdbEightHourAtlanta)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 151)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(306, 90)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "8-Hr Ozone Attainment Status"
        '
        'btnEightHourChanges
        '
        Me.btnEightHourChanges.AutoSize = True
        Me.btnEightHourChanges.Location = New System.Drawing.Point(203, 27)
        Me.btnEightHourChanges.Name = "btnEightHourChanges"
        Me.btnEightHourChanges.Size = New System.Drawing.Size(97, 49)
        Me.btnEightHourChanges.TabIndex = 24
        Me.btnEightHourChanges.Text = "Save " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "8-Hr Changes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Only"
        Me.btnEightHourChanges.UseVisualStyleBackColor = True
        '
        'rdbEightHourNo
        '
        Me.rdbEightHourNo.AutoSize = True
        Me.rdbEightHourNo.Location = New System.Drawing.Point(6, 66)
        Me.rdbEightHourNo.Name = "rdbEightHourNo"
        Me.rdbEightHourNo.Size = New System.Drawing.Size(66, 17)
        Me.rdbEightHourNo.TabIndex = 23
        Me.rdbEightHourNo.TabStop = True
        Me.rdbEightHourNo.Text = "8 hr (No)"
        Me.rdbEightHourNo.UseVisualStyleBackColor = True
        '
        'rdbEightHourMacon
        '
        Me.rdbEightHourMacon.AutoSize = True
        Me.rdbEightHourMacon.Location = New System.Drawing.Point(6, 43)
        Me.rdbEightHourMacon.Name = "rdbEightHourMacon"
        Me.rdbEightHourMacon.Size = New System.Drawing.Size(79, 17)
        Me.rdbEightHourMacon.TabIndex = 22
        Me.rdbEightHourMacon.TabStop = True
        Me.rdbEightHourMacon.Text = "8 hr Macon"
        Me.rdbEightHourMacon.UseVisualStyleBackColor = True
        '
        'rdbEightHourAtlanta
        '
        Me.rdbEightHourAtlanta.AutoSize = True
        Me.rdbEightHourAtlanta.Location = New System.Drawing.Point(6, 20)
        Me.rdbEightHourAtlanta.Name = "rdbEightHourAtlanta"
        Me.rdbEightHourAtlanta.Size = New System.Drawing.Size(79, 17)
        Me.rdbEightHourAtlanta.TabIndex = 21
        Me.rdbEightHourAtlanta.TabStop = True
        Me.rdbEightHourAtlanta.Text = "8 hr Atlanta"
        Me.rdbEightHourAtlanta.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnOneHourChanges)
        Me.GroupBox2.Controls.Add(Me.rdbOneHourNo)
        Me.GroupBox2.Controls.Add(Me.rdbOneHourCont)
        Me.GroupBox2.Controls.Add(Me.rdbOneHourYes)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 63)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(306, 86)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "1-Hr Ozone Attainment Status"
        '
        'btnOneHourChanges
        '
        Me.btnOneHourChanges.AutoSize = True
        Me.btnOneHourChanges.Location = New System.Drawing.Point(203, 21)
        Me.btnOneHourChanges.Name = "btnOneHourChanges"
        Me.btnOneHourChanges.Size = New System.Drawing.Size(97, 49)
        Me.btnOneHourChanges.TabIndex = 20
        Me.btnOneHourChanges.Text = "Save " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1-Hr Changes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Only"
        Me.btnOneHourChanges.UseVisualStyleBackColor = True
        '
        'rdbOneHourNo
        '
        Me.rdbOneHourNo.AutoSize = True
        Me.rdbOneHourNo.Location = New System.Drawing.Point(6, 60)
        Me.rdbOneHourNo.Name = "rdbOneHourNo"
        Me.rdbOneHourNo.Size = New System.Drawing.Size(142, 17)
        Me.rdbOneHourNo.TabIndex = 22
        Me.rdbOneHourNo.TabStop = True
        Me.rdbOneHourNo.Text = "1 hr Non-Attainment (No)"
        Me.rdbOneHourNo.UseVisualStyleBackColor = True
        '
        'rdbOneHourCont
        '
        Me.rdbOneHourCont.AutoSize = True
        Me.rdbOneHourCont.Location = New System.Drawing.Point(6, 37)
        Me.rdbOneHourCont.Name = "rdbOneHourCont"
        Me.rdbOneHourCont.Size = New System.Drawing.Size(176, 17)
        Me.rdbOneHourCont.TabIndex = 21
        Me.rdbOneHourCont.TabStop = True
        Me.rdbOneHourCont.Text = "1 hr Non-Attainment (Contribute)"
        Me.rdbOneHourCont.UseVisualStyleBackColor = True
        '
        'rdbOneHourYes
        '
        Me.rdbOneHourYes.AutoSize = True
        Me.rdbOneHourYes.Location = New System.Drawing.Point(6, 14)
        Me.rdbOneHourYes.Name = "rdbOneHourYes"
        Me.rdbOneHourYes.Size = New System.Drawing.Size(146, 17)
        Me.rdbOneHourYes.TabIndex = 20
        Me.rdbOneHourYes.TabStop = True
        Me.rdbOneHourYes.Text = "1 hr Non-Attainment (Yes)"
        Me.rdbOneHourYes.UseVisualStyleBackColor = True
        '
        'btnSaveChanges
        '
        Me.btnSaveChanges.AutoSize = True
        Me.btnSaveChanges.Location = New System.Drawing.Point(318, 21)
        Me.btnSaveChanges.Name = "btnSaveChanges"
        Me.btnSaveChanges.Size = New System.Drawing.Size(97, 36)
        Me.btnSaveChanges.TabIndex = 17
        Me.btnSaveChanges.Text = "Save " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "All Changes"
        Me.btnSaveChanges.UseVisualStyleBackColor = True
        '
        'btnViewCountyInfo
        '
        Me.btnViewCountyInfo.AutoSize = True
        Me.btnViewCountyInfo.Location = New System.Drawing.Point(197, 28)
        Me.btnViewCountyInfo.Name = "btnViewCountyInfo"
        Me.btnViewCountyInfo.Size = New System.Drawing.Size(97, 23)
        Me.btnViewCountyInfo.TabIndex = 16
        Me.btnViewCountyInfo.Text = "View County Info"
        Me.btnViewCountyInfo.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "County:"
        '
        'cboCounty
        '
        Me.cboCounty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCounty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCounty.FormattingEnabled = True
        Me.cboCounty.Location = New System.Drawing.Point(28, 30)
        Me.cboCounty.Name = "cboCounty"
        Me.cboCounty.Size = New System.Drawing.Size(154, 21)
        Me.cboCounty.TabIndex = 3
        '
        'btnViewSelectedData
        '
        Me.btnViewSelectedData.AutoSize = True
        Me.btnViewSelectedData.Location = New System.Drawing.Point(675, 30)
        Me.btnViewSelectedData.Name = "btnViewSelectedData"
        Me.btnViewSelectedData.Size = New System.Drawing.Size(111, 23)
        Me.btnViewSelectedData.TabIndex = 2
        Me.btnViewSelectedData.Text = "View Selected Data"
        Me.btnViewSelectedData.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(430, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Types of Non Attainments"
        '
        'cboNonAttainmentStatus
        '
        Me.cboNonAttainmentStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNonAttainmentStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNonAttainmentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNonAttainmentStatus.FormattingEnabled = True
        Me.cboNonAttainmentStatus.Location = New System.Drawing.Point(453, 30)
        Me.cboNonAttainmentStatus.Name = "cboNonAttainmentStatus"
        Me.cboNonAttainmentStatus.Size = New System.Drawing.Size(210, 21)
        Me.cboNonAttainmentStatus.TabIndex = 0
        '
        'dgvAttainmentStatus
        '
        Me.dgvAttainmentStatus.AllowUserToAddRows = False
        Me.dgvAttainmentStatus.AllowUserToDeleteRows = False
        Me.dgvAttainmentStatus.AllowUserToOrderColumns = True
        Me.dgvAttainmentStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAttainmentStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAttainmentStatus.Location = New System.Drawing.Point(0, 273)
        Me.dgvAttainmentStatus.Name = "dgvAttainmentStatus"
        Me.dgvAttainmentStatus.ReadOnly = True
        Me.dgvAttainmentStatus.Size = New System.Drawing.Size(792, 272)
        Me.dgvAttainmentStatus.TabIndex = 258
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bSave})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 259
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'bSave
        '
        Me.bSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.bSave.Name = "bSave"
        Me.bSave.Size = New System.Drawing.Size(59, 20)
        Me.bSave.Text = "Save"
        '
        'SSPPAttainmentStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 545)
        Me.Controls.Add(Me.dgvAttainmentStatus)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "SSPPAttainmentStatus"
        Me.Text = "SSPP Attainment Status"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvAttainmentStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboNonAttainmentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents dgvAttainmentStatus As System.Windows.Forms.DataGridView
    Friend WithEvents btnViewSelectedData As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCounty As System.Windows.Forms.ComboBox
    Friend WithEvents btnSaveChanges As System.Windows.Forms.Button
    Friend WithEvents btnViewCountyInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbOneHourNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOneHourCont As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOneHourYes As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbEightHourNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEightHourMacon As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEightHourAtlanta As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbPMFineNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMFineMacon As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMFineFloyd As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMFineChattanooga As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPMFineAtlanta As System.Windows.Forms.RadioButton
    Friend WithEvents btnPMFineChanges As Button
    Friend WithEvents btnEightHourChanges As Button
    Friend WithEvents btnOneHourChanges As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents bSave As ToolStripMenuItem
End Class
