<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IAIPNewFacilityColocation
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SelectedFacilitiesGrid = New Iaip.IaipDataGridView()
        Me.btnAddFacility = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnRemoveFacility = New System.Windows.Forms.Button()
        Me.FacilityNameDisplay = New System.Windows.Forms.Label()
        Me.AirsNumberDisplay = New System.Windows.Forms.Label()
        CType(Me.SelectedFacilitiesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectedFacilitiesGrid
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SelectedFacilitiesGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.SelectedFacilitiesGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectedFacilitiesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.SelectedFacilitiesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SelectedFacilitiesGrid.GridColor = System.Drawing.SystemColors.ControlLight
        Me.SelectedFacilitiesGrid.LinkifyColumnByName = Nothing
        Me.SelectedFacilitiesGrid.Location = New System.Drawing.Point(12, 87)
        Me.SelectedFacilitiesGrid.MultiSelect = True
        Me.SelectedFacilitiesGrid.Name = "SelectedFacilitiesGrid"
        Me.SelectedFacilitiesGrid.ResultsCountLabel = Nothing
        Me.SelectedFacilitiesGrid.ResultsCountLabelFormat = "{0} found"
        Me.SelectedFacilitiesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.SelectedFacilitiesGrid.Size = New System.Drawing.Size(309, 196)
        Me.SelectedFacilitiesGrid.StandardTab = True
        Me.SelectedFacilitiesGrid.TabIndex = 4
        '
        'btnAddFacility
        '
        Me.btnAddFacility.Location = New System.Drawing.Point(12, 46)
        Me.btnAddFacility.Name = "btnAddFacility"
        Me.btnAddFacility.Size = New System.Drawing.Size(114, 35)
        Me.btnAddFacility.TabIndex = 2
        Me.btnAddFacility.Text = "Add a facility"
        Me.btnAddFacility.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.btnSave.Location = New System.Drawing.Point(171, 289)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(150, 35)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save new " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "co-location group"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(12, 289)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(114, 35)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnRemoveFacility
        '
        Me.btnRemoveFacility.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveFacility.Enabled = False
        Me.btnRemoveFacility.Location = New System.Drawing.Point(171, 46)
        Me.btnRemoveFacility.Name = "btnRemoveFacility"
        Me.btnRemoveFacility.Size = New System.Drawing.Size(150, 35)
        Me.btnRemoveFacility.TabIndex = 3
        Me.btnRemoveFacility.Text = "Remove selected facilities"
        Me.btnRemoveFacility.UseVisualStyleBackColor = True
        '
        'FacilityNameDisplay
        '
        Me.FacilityNameDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityNameDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityNameDisplay.Location = New System.Drawing.Point(92, 9)
        Me.FacilityNameDisplay.Name = "FacilityNameDisplay"
        Me.FacilityNameDisplay.Size = New System.Drawing.Size(229, 34)
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
        'IAIPNewFacilityColocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 336)
        Me.Controls.Add(Me.FacilityNameDisplay)
        Me.Controls.Add(Me.AirsNumberDisplay)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnRemoveFacility)
        Me.Controls.Add(Me.btnAddFacility)
        Me.Controls.Add(Me.SelectedFacilitiesGrid)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(349, 375)
        Me.Name = "IAIPNewFacilityColocation"
        Me.Text = "Create a new facility co-location group"
        CType(Me.SelectedFacilitiesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SelectedFacilitiesGrid As IaipDataGridView
    Friend WithEvents btnAddFacility As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnRemoveFacility As Button
    Friend WithEvents FacilityNameDisplay As Label
    Friend WithEvents AirsNumberDisplay As Label
End Class
