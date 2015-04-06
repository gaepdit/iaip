<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DmuEdtErrorMessages
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
        Me.EdtErrorMessageGrid = New System.Windows.Forms.DataGridView
        Me.DisplayMine = New System.Windows.Forms.RadioButton
        Me.DisplayEveryone = New System.Windows.Forms.RadioButton
        Me.DisplayOpen = New System.Windows.Forms.RadioButton
        Me.DisplayAll = New System.Windows.Forms.RadioButton
        Me.EdtErrorCountDisplay = New System.Windows.Forms.Label
        Me.ReloadButton = New System.Windows.Forms.Button
        Me.ResolvedStatusGroupPanel = New System.Windows.Forms.Panel
        Me.OwnerGroupPanel = New System.Windows.Forms.Panel
        CType(Me.EdtErrorMessageGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ResolvedStatusGroupPanel.SuspendLayout()
        Me.OwnerGroupPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'EdtErrorMessageGrid
        '
        Me.EdtErrorMessageGrid.AllowUserToAddRows = False
        Me.EdtErrorMessageGrid.AllowUserToDeleteRows = False
        Me.EdtErrorMessageGrid.AllowUserToOrderColumns = True
        Me.EdtErrorMessageGrid.AllowUserToResizeRows = False
        Me.EdtErrorMessageGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EdtErrorMessageGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.EdtErrorMessageGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.EdtErrorMessageGrid.GridColor = System.Drawing.SystemColors.ControlLight
        Me.EdtErrorMessageGrid.Location = New System.Drawing.Point(12, 45)
        Me.EdtErrorMessageGrid.Name = "EdtErrorMessageGrid"
        Me.EdtErrorMessageGrid.ReadOnly = True
        Me.EdtErrorMessageGrid.RowHeadersVisible = False
        Me.EdtErrorMessageGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EdtErrorMessageGrid.Size = New System.Drawing.Size(579, 320)
        Me.EdtErrorMessageGrid.TabIndex = 3
        '
        'DisplayMine
        '
        Me.DisplayMine.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayMine.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayMine.Checked = True
        Me.DisplayMine.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayMine.Location = New System.Drawing.Point(0, 10)
        Me.DisplayMine.Name = "DisplayMine"
        Me.DisplayMine.Size = New System.Drawing.Size(66, 23)
        Me.DisplayMine.TabIndex = 1
        Me.DisplayMine.TabStop = True
        Me.DisplayMine.Text = "Mine"
        Me.DisplayMine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayMine.UseVisualStyleBackColor = True
        '
        'DisplayEveryone
        '
        Me.DisplayEveryone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayEveryone.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayEveryone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayEveryone.Location = New System.Drawing.Point(65, 10)
        Me.DisplayEveryone.Name = "DisplayEveryone"
        Me.DisplayEveryone.Size = New System.Drawing.Size(66, 23)
        Me.DisplayEveryone.TabIndex = 2
        Me.DisplayEveryone.Text = "All"
        Me.DisplayEveryone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayEveryone.UseVisualStyleBackColor = True
        '
        'DisplayOpen
        '
        Me.DisplayOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOpen.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayOpen.Checked = True
        Me.DisplayOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayOpen.Location = New System.Drawing.Point(0, 10)
        Me.DisplayOpen.Name = "DisplayOpen"
        Me.DisplayOpen.Size = New System.Drawing.Size(66, 23)
        Me.DisplayOpen.TabIndex = 1
        Me.DisplayOpen.TabStop = True
        Me.DisplayOpen.Text = "Open"
        Me.DisplayOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayOpen.UseVisualStyleBackColor = True
        '
        'DisplayAll
        '
        Me.DisplayAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayAll.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayAll.Location = New System.Drawing.Point(65, 10)
        Me.DisplayAll.Name = "DisplayAll"
        Me.DisplayAll.Size = New System.Drawing.Size(66, 23)
        Me.DisplayAll.TabIndex = 2
        Me.DisplayAll.Text = "All"
        Me.DisplayAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayAll.UseVisualStyleBackColor = True
        '
        'EdtErrorCountDisplay
        '
        Me.EdtErrorCountDisplay.AutoSize = True
        Me.EdtErrorCountDisplay.Location = New System.Drawing.Point(93, 17)
        Me.EdtErrorCountDisplay.Name = "EdtErrorCountDisplay"
        Me.EdtErrorCountDisplay.Size = New System.Drawing.Size(97, 13)
        Me.EdtErrorCountDisplay.TabIndex = 5
        Me.EdtErrorCountDisplay.Text = "No errors to display"
        '
        'ReloadButton
        '
        Me.ReloadButton.Location = New System.Drawing.Point(12, 12)
        Me.ReloadButton.Name = "ReloadButton"
        Me.ReloadButton.Size = New System.Drawing.Size(75, 23)
        Me.ReloadButton.TabIndex = 0
        Me.ReloadButton.Text = "Reload all"
        Me.ReloadButton.UseVisualStyleBackColor = True
        '
        'ResolvedStatusGroupPanel
        '
        Me.ResolvedStatusGroupPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResolvedStatusGroupPanel.Controls.Add(Me.DisplayAll)
        Me.ResolvedStatusGroupPanel.Controls.Add(Me.DisplayOpen)
        Me.ResolvedStatusGroupPanel.Enabled = False
        Me.ResolvedStatusGroupPanel.Location = New System.Drawing.Point(460, 13)
        Me.ResolvedStatusGroupPanel.Name = "ResolvedStatusGroupPanel"
        Me.ResolvedStatusGroupPanel.Size = New System.Drawing.Size(131, 33)
        Me.ResolvedStatusGroupPanel.TabIndex = 6
        '
        'OwnerGroupPanel
        '
        Me.OwnerGroupPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OwnerGroupPanel.Controls.Add(Me.DisplayMine)
        Me.OwnerGroupPanel.Controls.Add(Me.DisplayEveryone)
        Me.OwnerGroupPanel.Enabled = False
        Me.OwnerGroupPanel.Location = New System.Drawing.Point(324, 13)
        Me.OwnerGroupPanel.Name = "OwnerGroupPanel"
        Me.OwnerGroupPanel.Size = New System.Drawing.Size(131, 33)
        Me.OwnerGroupPanel.TabIndex = 7
        '
        'DmuEdtErrorMessages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 377)
        Me.Controls.Add(Me.OwnerGroupPanel)
        Me.Controls.Add(Me.ResolvedStatusGroupPanel)
        Me.Controls.Add(Me.ReloadButton)
        Me.Controls.Add(Me.EdtErrorCountDisplay)
        Me.Controls.Add(Me.EdtErrorMessageGrid)
        Me.MinimumSize = New System.Drawing.Size(560, 200)
        Me.Name = "DmuEdtErrorMessages"
        Me.Text = "ICIS-Air EDT Error Messages"
        CType(Me.EdtErrorMessageGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResolvedStatusGroupPanel.ResumeLayout(False)
        Me.OwnerGroupPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EdtErrorMessageGrid As System.Windows.Forms.DataGridView
    Friend WithEvents DisplayMine As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayOpen As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayAll As System.Windows.Forms.RadioButton
    Friend WithEvents EdtErrorCountDisplay As System.Windows.Forms.Label
    Friend WithEvents ReloadButton As System.Windows.Forms.Button
    Friend WithEvents ResolvedStatusGroupPanel As System.Windows.Forms.Panel
    Friend WithEvents OwnerGroupPanel As System.Windows.Forms.Panel
End Class
