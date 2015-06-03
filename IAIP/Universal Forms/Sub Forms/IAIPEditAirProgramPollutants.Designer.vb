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
        Me.cboComplianceStatus = New System.Windows.Forms.ComboBox
        Me.lblComplianceStatus = New System.Windows.Forms.Label
        Me.cboAirProgramCodes = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboPollutants = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dgvAirProgramPollutants = New System.Windows.Forms.DataGridView
        Me.btnSaveNewPollutant = New System.Windows.Forms.Button
        Me.PanelPollutants = New System.Windows.Forms.Panel
        Me.FacilityNameDisplay = New System.Windows.Forms.Label
        Me.AirsNumberDisplay = New System.Windows.Forms.Label
        CType(Me.dgvAirProgramPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPollutants.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboComplianceStatus
        '
        Me.cboComplianceStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboComplianceStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboComplianceStatus.Location = New System.Drawing.Point(416, 72)
        Me.cboComplianceStatus.Name = "cboComplianceStatus"
        Me.cboComplianceStatus.Size = New System.Drawing.Size(199, 21)
        Me.cboComplianceStatus.TabIndex = 237
        '
        'lblComplianceStatus
        '
        Me.lblComplianceStatus.AutoSize = True
        Me.lblComplianceStatus.Location = New System.Drawing.Point(413, 56)
        Me.lblComplianceStatus.Name = "lblComplianceStatus"
        Me.lblComplianceStatus.Size = New System.Drawing.Size(95, 13)
        Me.lblComplianceStatus.TabIndex = 236
        Me.lblComplianceStatus.Text = "Compliance Status"
        '
        'cboAirProgramCodes
        '
        Me.cboAirProgramCodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirProgramCodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirProgramCodes.Location = New System.Drawing.Point(12, 72)
        Me.cboAirProgramCodes.Name = "cboAirProgramCodes"
        Me.cboAirProgramCodes.Size = New System.Drawing.Size(135, 21)
        Me.cboAirProgramCodes.TabIndex = 229
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(152, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 232
        Me.Label3.Text = "Pollutant"
        '
        'cboPollutants
        '
        Me.cboPollutants.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollutants.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollutants.Location = New System.Drawing.Point(153, 72)
        Me.cboPollutants.Name = "cboPollutants"
        Me.cboPollutants.Size = New System.Drawing.Size(257, 21)
        Me.cboPollutants.TabIndex = 231
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
        'dgvAirProgramPollutants
        '
        Me.dgvAirProgramPollutants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAirProgramPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAirProgramPollutants.Location = New System.Drawing.Point(0, 162)
        Me.dgvAirProgramPollutants.Name = "dgvAirProgramPollutants"
        Me.dgvAirProgramPollutants.ReadOnly = True
        Me.dgvAirProgramPollutants.Size = New System.Drawing.Size(627, 190)
        Me.dgvAirProgramPollutants.TabIndex = 5
        '
        'btnSaveNewPollutant
        '
        Me.btnSaveNewPollutant.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveNewPollutant.Location = New System.Drawing.Point(12, 117)
        Me.btnSaveNewPollutant.Name = "btnSaveNewPollutant"
        Me.btnSaveNewPollutant.Size = New System.Drawing.Size(135, 23)
        Me.btnSaveNewPollutant.TabIndex = 238
        Me.btnSaveNewPollutant.Text = "Add/Edit Pollutant Data"
        Me.btnSaveNewPollutant.UseVisualStyleBackColor = True
        '
        'PanelPollutants
        '
        Me.PanelPollutants.Controls.Add(Me.FacilityNameDisplay)
        Me.PanelPollutants.Controls.Add(Me.AirsNumberDisplay)
        Me.PanelPollutants.Controls.Add(Me.btnSaveNewPollutant)
        Me.PanelPollutants.Controls.Add(Me.cboComplianceStatus)
        Me.PanelPollutants.Controls.Add(Me.lblComplianceStatus)
        Me.PanelPollutants.Controls.Add(Me.Label1)
        Me.PanelPollutants.Controls.Add(Me.cboAirProgramCodes)
        Me.PanelPollutants.Controls.Add(Me.cboPollutants)
        Me.PanelPollutants.Controls.Add(Me.Label3)
        Me.PanelPollutants.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelPollutants.Location = New System.Drawing.Point(0, 0)
        Me.PanelPollutants.Name = "PanelPollutants"
        Me.PanelPollutants.Size = New System.Drawing.Size(627, 162)
        Me.PanelPollutants.TabIndex = 8
        '
        'FacilityNameDisplay
        '
        Me.FacilityNameDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityNameDisplay.Location = New System.Drawing.Point(92, 18)
        Me.FacilityNameDisplay.Name = "FacilityNameDisplay"
        Me.FacilityNameDisplay.Size = New System.Drawing.Size(388, 34)
        Me.FacilityNameDisplay.TabIndex = 240
        Me.FacilityNameDisplay.Text = "Facility Name"
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
        Me.Controls.Add(Me.dgvAirProgramPollutants)
        Me.Controls.Add(Me.PanelPollutants)
        Me.MinimumSize = New System.Drawing.Size(643, 390)
        Me.Name = "IAIPEditAirProgramPollutants"
        Me.Text = "Edit Air Program Pollutants"
        CType(Me.dgvAirProgramPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPollutants.ResumeLayout(False)
        Me.PanelPollutants.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboComplianceStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblComplianceStatus As System.Windows.Forms.Label
    Friend WithEvents cboAirProgramCodes As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboPollutants As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvAirProgramPollutants As System.Windows.Forms.DataGridView
    Friend WithEvents btnSaveNewPollutant As System.Windows.Forms.Button
    Friend WithEvents PanelPollutants As System.Windows.Forms.Panel
    Friend WithEvents FacilityNameDisplay As System.Windows.Forms.Label
    Friend WithEvents AirsNumberDisplay As System.Windows.Forms.Label
End Class
