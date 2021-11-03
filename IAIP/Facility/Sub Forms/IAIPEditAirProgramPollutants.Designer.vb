<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IAIPEditAirProgramPollutants
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.FacilityAirProgramPollutants = New System.Windows.Forms.DataGridView()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.FacilityOperatingStatusDisplay = New System.Windows.Forms.Label()
        Me.FacilityDisplay = New System.Windows.Forms.Label()
        Me.AirsNumberDisplay = New System.Windows.Forms.Label()
        Me.ControlPanel = New System.Windows.Forms.Panel()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.OperatingStatusSelect = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AirProgramSelect = New System.Windows.Forms.ComboBox()
        Me.PollutantSelect = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.FacilityAirProgramPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HeaderPanel.SuspendLayout()
        Me.ControlPanel.SuspendLayout()
        Me.SuspendLayout()
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
        Me.FacilityAirProgramPollutants.Location = New System.Drawing.Point(0, 307)
        Me.FacilityAirProgramPollutants.MultiSelect = False
        Me.FacilityAirProgramPollutants.Name = "FacilityAirProgramPollutants"
        Me.FacilityAirProgramPollutants.ReadOnly = True
        Me.FacilityAirProgramPollutants.RowHeadersVisible = False
        Me.FacilityAirProgramPollutants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FacilityAirProgramPollutants.Size = New System.Drawing.Size(484, 242)
        Me.FacilityAirProgramPollutants.TabIndex = 0
        '
        'HeaderPanel
        '
        Me.HeaderPanel.Controls.Add(Me.FacilityOperatingStatusDisplay)
        Me.HeaderPanel.Controls.Add(Me.FacilityDisplay)
        Me.HeaderPanel.Controls.Add(Me.AirsNumberDisplay)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(484, 84)
        Me.HeaderPanel.TabIndex = 1
        '
        'FacilityOperatingStatusDisplay
        '
        Me.FacilityOperatingStatusDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityOperatingStatusDisplay.AutoSize = True
        Me.FacilityOperatingStatusDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityOperatingStatusDisplay.Location = New System.Drawing.Point(12, 48)
        Me.FacilityOperatingStatusDisplay.Name = "FacilityOperatingStatusDisplay"
        Me.FacilityOperatingStatusDisplay.Size = New System.Drawing.Size(101, 17)
        Me.FacilityOperatingStatusDisplay.TabIndex = 240
        Me.FacilityOperatingStatusDisplay.Text = "Facility status: "
        '
        'FacilityDisplay
        '
        Me.FacilityDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityDisplay.AutoSize = True
        Me.FacilityDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityDisplay.Location = New System.Drawing.Point(92, 18)
        Me.FacilityDisplay.Name = "FacilityDisplay"
        Me.FacilityDisplay.Size = New System.Drawing.Size(92, 17)
        Me.FacilityDisplay.TabIndex = 240
        Me.FacilityDisplay.Text = "Facility Name"
        Me.FacilityDisplay.UseMnemonic = False
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
        'ControlPanel
        '
        Me.ControlPanel.Controls.Add(Me.SaveButton)
        Me.ControlPanel.Controls.Add(Me.OperatingStatusSelect)
        Me.ControlPanel.Controls.Add(Me.Label2)
        Me.ControlPanel.Controls.Add(Me.Label1)
        Me.ControlPanel.Controls.Add(Me.AirProgramSelect)
        Me.ControlPanel.Controls.Add(Me.PollutantSelect)
        Me.ControlPanel.Controls.Add(Me.Label3)
        Me.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.ControlPanel.Location = New System.Drawing.Point(0, 84)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(484, 223)
        Me.ControlPanel.TabIndex = 0
        '
        'SaveButton
        '
        Me.SaveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SaveButton.Location = New System.Drawing.Point(15, 173)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(196, 30)
        Me.SaveButton.TabIndex = 3
        Me.SaveButton.Text = "Add new air program/pollutant"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'OperatingStatusSelect
        '
        Me.OperatingStatusSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OperatingStatusSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OperatingStatusSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OperatingStatusSelect.Location = New System.Drawing.Point(15, 120)
        Me.OperatingStatusSelect.Name = "OperatingStatusSelect"
        Me.OperatingStatusSelect.Size = New System.Drawing.Size(228, 21)
        Me.OperatingStatusSelect.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 13)
        Me.Label2.TabIndex = 236
        Me.Label2.Text = "Pollutant Operating Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 230
        Me.Label1.Text = "Air Program"
        '
        'AirProgramSelect
        '
        Me.AirProgramSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.AirProgramSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AirProgramSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AirProgramSelect.Location = New System.Drawing.Point(15, 19)
        Me.AirProgramSelect.Name = "AirProgramSelect"
        Me.AirProgramSelect.Size = New System.Drawing.Size(228, 21)
        Me.AirProgramSelect.TabIndex = 0
        '
        'PollutantSelect
        '
        Me.PollutantSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.PollutantSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PollutantSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PollutantSelect.Location = New System.Drawing.Point(15, 67)
        Me.PollutantSelect.Name = "PollutantSelect"
        Me.PollutantSelect.Size = New System.Drawing.Size(228, 21)
        Me.PollutantSelect.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 232
        Me.Label3.Text = "Pollutant"
        '
        'IAIPEditAirProgramPollutants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 549)
        Me.Controls.Add(Me.FacilityAirProgramPollutants)
        Me.Controls.Add(Me.ControlPanel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(500, 350)
        Me.Name = "IAIPEditAirProgramPollutants"
        Me.ShowIcon = False
        Me.Text = "Facility Air Programs/Pollutants"
        CType(Me.FacilityAirProgramPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AirProgramSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PollutantSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FacilityAirProgramPollutants As System.Windows.Forms.DataGridView
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents FacilityDisplay As System.Windows.Forms.Label
    Friend WithEvents AirsNumberDisplay As System.Windows.Forms.Label
    Friend WithEvents ControlPanel As Panel
    Friend WithEvents OperatingStatusSelect As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents FacilityOperatingStatusDisplay As Label
End Class
