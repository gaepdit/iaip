<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditAirProgramPollutants
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ComplianceStatusSelect = New System.Windows.Forms.ComboBox()
        Me.lblComplianceStatus = New System.Windows.Forms.Label()
        Me.AirProgramSelect = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PollutantSelect = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FacilityAirProgramPollutants = New System.Windows.Forms.DataGridView()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.PanelPollutants = New System.Windows.Forms.Panel()
        Me.FacilityDisplay = New System.Windows.Forms.Label()
        Me.AirsNumberDisplay = New System.Windows.Forms.Label()
        CType(Me.FacilityAirProgramPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPollutants.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComplianceStatusSelect
        '
        Me.ComplianceStatusSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ComplianceStatusSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComplianceStatusSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComplianceStatusSelect.Location = New System.Drawing.Point(419, 72)
        Me.ComplianceStatusSelect.Name = "ComplianceStatusSelect"
        Me.ComplianceStatusSelect.Size = New System.Drawing.Size(196, 21)
        Me.ComplianceStatusSelect.TabIndex = 237
        '
        'lblComplianceStatus
        '
        Me.lblComplianceStatus.AutoSize = True
        Me.lblComplianceStatus.Location = New System.Drawing.Point(416, 56)
        Me.lblComplianceStatus.Name = "lblComplianceStatus"
        Me.lblComplianceStatus.Size = New System.Drawing.Size(95, 13)
        Me.lblComplianceStatus.TabIndex = 236
        Me.lblComplianceStatus.Text = "Compliance Status"
        '
        'AirProgramSelect
        '
        Me.AirProgramSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.AirProgramSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AirProgramSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AirProgramSelect.Location = New System.Drawing.Point(15, 72)
        Me.AirProgramSelect.Name = "AirProgramSelect"
        Me.AirProgramSelect.Size = New System.Drawing.Size(135, 21)
        Me.AirProgramSelect.TabIndex = 229
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(153, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 232
        Me.Label3.Text = "Pollutant"
        '
        'PollutantSelect
        '
        Me.PollutantSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.PollutantSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PollutantSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PollutantSelect.Location = New System.Drawing.Point(156, 72)
        Me.PollutantSelect.Name = "PollutantSelect"
        Me.PollutantSelect.Size = New System.Drawing.Size(257, 21)
        Me.PollutantSelect.TabIndex = 231
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 230
        Me.Label1.Text = "Air Program Code"
        '
        'FacilityAirProgramPollutants
        '
        Me.FacilityAirProgramPollutants.AllowUserToAddRows = False
        Me.FacilityAirProgramPollutants.AllowUserToDeleteRows = False
        Me.FacilityAirProgramPollutants.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FacilityAirProgramPollutants.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.FacilityAirProgramPollutants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FacilityAirProgramPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FacilityAirProgramPollutants.Location = New System.Drawing.Point(0, 156)
        Me.FacilityAirProgramPollutants.MultiSelect = False
        Me.FacilityAirProgramPollutants.Name = "FacilityAirProgramPollutants"
        Me.FacilityAirProgramPollutants.ReadOnly = True
        Me.FacilityAirProgramPollutants.RowHeadersVisible = False
        Me.FacilityAirProgramPollutants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FacilityAirProgramPollutants.Size = New System.Drawing.Size(627, 196)
        Me.FacilityAirProgramPollutants.TabIndex = 5
        '
        'SaveButton
        '
        Me.SaveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SaveButton.Location = New System.Drawing.Point(15, 111)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(135, 23)
        Me.SaveButton.TabIndex = 238
        Me.SaveButton.Text = "Save New Value"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'PanelPollutants
        '
        Me.PanelPollutants.Controls.Add(Me.FacilityDisplay)
        Me.PanelPollutants.Controls.Add(Me.AirsNumberDisplay)
        Me.PanelPollutants.Controls.Add(Me.SaveButton)
        Me.PanelPollutants.Controls.Add(Me.ComplianceStatusSelect)
        Me.PanelPollutants.Controls.Add(Me.lblComplianceStatus)
        Me.PanelPollutants.Controls.Add(Me.Label1)
        Me.PanelPollutants.Controls.Add(Me.AirProgramSelect)
        Me.PanelPollutants.Controls.Add(Me.PollutantSelect)
        Me.PanelPollutants.Controls.Add(Me.Label3)
        Me.PanelPollutants.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelPollutants.Location = New System.Drawing.Point(0, 0)
        Me.PanelPollutants.Name = "PanelPollutants"
        Me.PanelPollutants.Size = New System.Drawing.Size(627, 156)
        Me.PanelPollutants.TabIndex = 8
        '
        'FacilityDisplay
        '
        Me.FacilityDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityDisplay.Location = New System.Drawing.Point(92, 18)
        Me.FacilityDisplay.Name = "FacilityDisplay"
        Me.FacilityDisplay.Size = New System.Drawing.Size(523, 34)
        Me.FacilityDisplay.TabIndex = 240
        Me.FacilityDisplay.Text = "Facility Name"
        '
        'AirsNumberDisplay
        '
        Me.AirsNumberDisplay.AutoSize = True
        Me.AirsNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirsNumberDisplay.Location = New System.Drawing.Point(12, 18)
        Me.AirsNumberDisplay.Name = "AirsNumberDisplay"
        Me.AirsNumberDisplay.Size = New System.Drawing.Size(77, 17)
        Me.AirsNumberDisplay.TabIndex = 239
        Me.AirsNumberDisplay.Text = "000-00000"
        '
        'IAIPEditAirProgramPollutants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 352)
        Me.Controls.Add(Me.FacilityAirProgramPollutants)
        Me.Controls.Add(Me.PanelPollutants)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(643, 390)
        Me.Name = "IAIPEditAirProgramPollutants"
        Me.ShowIcon = False
        Me.Text = "Facility Air Program Pollutants"
        CType(Me.FacilityAirProgramPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPollutants.ResumeLayout(False)
        Me.PanelPollutants.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComplianceStatusSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lblComplianceStatus As System.Windows.Forms.Label
    Friend WithEvents AirProgramSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PollutantSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FacilityAirProgramPollutants As System.Windows.Forms.DataGridView
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents PanelPollutants As System.Windows.Forms.Panel
    Friend WithEvents FacilityDisplay As System.Windows.Forms.Label
    Friend WithEvents AirsNumberDisplay As System.Windows.Forms.Label
End Class
