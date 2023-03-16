<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditFacilityColocation
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnRemoveFromGroup = New System.Windows.Forms.Button()
        Me.btnRemoveGroup = New System.Windows.Forms.Button()
        Me.CurrentFacilitiesGrid = New Iaip.IaipDataGridView()
        Me.FacilityNameDisplay = New System.Windows.Forms.Label()
        Me.AirsNumberDisplay = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.CurrentFacilitiesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label10.Location = New System.Drawing.Point(12, 152)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 13)
        Me.Label10.TabIndex = 351
        Me.Label10.Text = "Co-located facilities"
        '
        'btnRemoveFromGroup
        '
        Me.btnRemoveFromGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveFromGroup.Location = New System.Drawing.Point(12, 46)
        Me.btnRemoveFromGroup.Name = "btnRemoveFromGroup"
        Me.btnRemoveFromGroup.Size = New System.Drawing.Size(117, 42)
        Me.btnRemoveFromGroup.TabIndex = 3
        Me.btnRemoveFromGroup.Text = "Remove this facility from the group"
        Me.btnRemoveFromGroup.UseVisualStyleBackColor = True
        '
        'btnRemoveGroup
        '
        Me.btnRemoveGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveGroup.Location = New System.Drawing.Point(12, 94)
        Me.btnRemoveGroup.Name = "btnRemoveGroup"
        Me.btnRemoveGroup.Size = New System.Drawing.Size(117, 42)
        Me.btnRemoveGroup.TabIndex = 4
        Me.btnRemoveGroup.Text = "Remove the whole group"
        Me.btnRemoveGroup.UseVisualStyleBackColor = True
        '
        'CurrentFacilitiesGrid
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CurrentFacilitiesGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.CurrentFacilitiesGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrentFacilitiesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.CurrentFacilitiesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.CurrentFacilitiesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CurrentFacilitiesGrid.GridColor = System.Drawing.SystemColors.ControlLight
        Me.CurrentFacilitiesGrid.LinkifyColumnByName = Nothing
        Me.CurrentFacilitiesGrid.Location = New System.Drawing.Point(12, 168)
        Me.CurrentFacilitiesGrid.Name = "CurrentFacilitiesGrid"
        Me.CurrentFacilitiesGrid.ResultsCountLabel = Nothing
        Me.CurrentFacilitiesGrid.ResultsCountLabelFormat = "{0} found"
        Me.CurrentFacilitiesGrid.Size = New System.Drawing.Size(396, 111)
        Me.CurrentFacilitiesGrid.StandardTab = True
        Me.CurrentFacilitiesGrid.TabIndex = 2
        '
        'FacilityNameDisplay
        '
        Me.FacilityNameDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityNameDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityNameDisplay.Location = New System.Drawing.Point(92, 9)
        Me.FacilityNameDisplay.Name = "FacilityNameDisplay"
        Me.FacilityNameDisplay.Size = New System.Drawing.Size(316, 17)
        Me.FacilityNameDisplay.TabIndex = 1
        Me.FacilityNameDisplay.Text = "Facility Name"
        Me.FacilityNameDisplay.UseMnemonic = False
        '
        'AirsNumberDisplay
        '
        Me.AirsNumberDisplay.AutoSize = True
        Me.AirsNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirsNumberDisplay.Location = New System.Drawing.Point(12, 9)
        Me.AirsNumberDisplay.Name = "AirsNumberDisplay"
        Me.AirsNumberDisplay.Size = New System.Drawing.Size(77, 17)
        Me.AirsNumberDisplay.TabIndex = 0
        Me.AirsNumberDisplay.Text = "000-00000"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(135, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(270, 42)
        Me.Label1.TabIndex = 361
        Me.Label1.Text = "The other facilities in the group below will remain co-located with each other."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(135, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(270, 42)
        Me.Label2.TabIndex = 361
        Me.Label2.Text = "All facilities will be removed from the group and the group will be deleted."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'IAIPEditFacilityColocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 291)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FacilityNameDisplay)
        Me.Controls.Add(Me.AirsNumberDisplay)
        Me.Controls.Add(Me.CurrentFacilitiesGrid)
        Me.Controls.Add(Me.btnRemoveGroup)
        Me.Controls.Add(Me.btnRemoveFromGroup)
        Me.Controls.Add(Me.Label10)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(436, 330)
        Me.Name = "IAIPEditFacilityColocation"
        Me.Text = "Edit Facility Co-location"
        CType(Me.CurrentFacilitiesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label10 As Label
    Friend WithEvents btnRemoveFromGroup As Button
    Friend WithEvents btnRemoveGroup As Button
    Friend WithEvents CurrentFacilitiesGrid As IaipDataGridView
    Friend WithEvents FacilityNameDisplay As Label
    Friend WithEvents AirsNumberDisplay As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
