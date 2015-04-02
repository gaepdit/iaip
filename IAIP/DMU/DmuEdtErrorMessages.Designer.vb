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
        Me.OwnerGroupBox = New System.Windows.Forms.GroupBox
        Me.ResolvedStatusGroupBox = New System.Windows.Forms.GroupBox
        Me.DisplayOpen = New System.Windows.Forms.RadioButton
        Me.DisplayAll = New System.Windows.Forms.RadioButton
        Me.EdtErrorCountDisplay = New System.Windows.Forms.Label
        Me.ReloadButton = New System.Windows.Forms.Button
        CType(Me.EdtErrorMessageGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OwnerGroupBox.SuspendLayout()
        Me.ResolvedStatusGroupBox.SuspendLayout()
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
        Me.EdtErrorMessageGrid.Location = New System.Drawing.Point(12, 84)
        Me.EdtErrorMessageGrid.Name = "EdtErrorMessageGrid"
        Me.EdtErrorMessageGrid.ReadOnly = True
        Me.EdtErrorMessageGrid.RowHeadersVisible = False
        Me.EdtErrorMessageGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EdtErrorMessageGrid.Size = New System.Drawing.Size(579, 281)
        Me.EdtErrorMessageGrid.TabIndex = 3
        '
        'DisplayMine
        '
        Me.DisplayMine.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayMine.Checked = True
        Me.DisplayMine.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DisplayMine.Location = New System.Drawing.Point(14, 19)
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
        Me.DisplayEveryone.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayEveryone.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DisplayEveryone.Location = New System.Drawing.Point(80, 19)
        Me.DisplayEveryone.Name = "DisplayEveryone"
        Me.DisplayEveryone.Size = New System.Drawing.Size(66, 23)
        Me.DisplayEveryone.TabIndex = 2
        Me.DisplayEveryone.Text = "All"
        Me.DisplayEveryone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DisplayEveryone.UseVisualStyleBackColor = True
        '
        'OwnerGroupBox
        '
        Me.OwnerGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OwnerGroupBox.Controls.Add(Me.DisplayMine)
        Me.OwnerGroupBox.Controls.Add(Me.DisplayEveryone)
        Me.OwnerGroupBox.Location = New System.Drawing.Point(257, 12)
        Me.OwnerGroupBox.Name = "OwnerGroupBox"
        Me.OwnerGroupBox.Size = New System.Drawing.Size(164, 56)
        Me.OwnerGroupBox.TabIndex = 1
        Me.OwnerGroupBox.TabStop = False
        Me.OwnerGroupBox.Text = "Show"
        '
        'ResolvedStatusGroupBox
        '
        Me.ResolvedStatusGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResolvedStatusGroupBox.Controls.Add(Me.DisplayOpen)
        Me.ResolvedStatusGroupBox.Controls.Add(Me.DisplayAll)
        Me.ResolvedStatusGroupBox.Location = New System.Drawing.Point(427, 12)
        Me.ResolvedStatusGroupBox.Name = "ResolvedStatusGroupBox"
        Me.ResolvedStatusGroupBox.Size = New System.Drawing.Size(164, 56)
        Me.ResolvedStatusGroupBox.TabIndex = 2
        Me.ResolvedStatusGroupBox.TabStop = False
        '
        'DisplayOpen
        '
        Me.DisplayOpen.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayOpen.Checked = True
        Me.DisplayOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayOpen.Location = New System.Drawing.Point(14, 19)
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
        Me.DisplayAll.Appearance = System.Windows.Forms.Appearance.Button
        Me.DisplayAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DisplayAll.Location = New System.Drawing.Point(79, 19)
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
        Me.EdtErrorCountDisplay.Location = New System.Drawing.Point(93, 36)
        Me.EdtErrorCountDisplay.Name = "EdtErrorCountDisplay"
        Me.EdtErrorCountDisplay.Size = New System.Drawing.Size(97, 13)
        Me.EdtErrorCountDisplay.TabIndex = 5
        Me.EdtErrorCountDisplay.Text = "No errors to display"
        '
        'ReloadButton
        '
        Me.ReloadButton.Location = New System.Drawing.Point(12, 31)
        Me.ReloadButton.Name = "ReloadButton"
        Me.ReloadButton.Size = New System.Drawing.Size(75, 23)
        Me.ReloadButton.TabIndex = 0
        Me.ReloadButton.Text = "Reload all"
        Me.ReloadButton.UseVisualStyleBackColor = True
        '
        'DmuEdtErrorMessages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 377)
        Me.Controls.Add(Me.ReloadButton)
        Me.Controls.Add(Me.EdtErrorCountDisplay)
        Me.Controls.Add(Me.ResolvedStatusGroupBox)
        Me.Controls.Add(Me.OwnerGroupBox)
        Me.Controls.Add(Me.EdtErrorMessageGrid)
        Me.MinimumSize = New System.Drawing.Size(560, 200)
        Me.Name = "DmuEdtErrorMessages"
        Me.Text = "ICIS-Air EDT Errors"
        CType(Me.EdtErrorMessageGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OwnerGroupBox.ResumeLayout(False)
        Me.ResolvedStatusGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EdtErrorMessageGrid As System.Windows.Forms.DataGridView
    Friend WithEvents DisplayMine As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents OwnerGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ResolvedStatusGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DisplayOpen As System.Windows.Forms.RadioButton
    Friend WithEvents DisplayAll As System.Windows.Forms.RadioButton
    Friend WithEvents EdtErrorCountDisplay As System.Windows.Forms.Label
    Friend WithEvents ReloadButton As System.Windows.Forms.Button
End Class
