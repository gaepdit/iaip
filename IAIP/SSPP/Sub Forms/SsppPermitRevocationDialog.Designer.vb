<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SsppPermitRevocationDialog
    Inherits System.Windows.Forms.Form

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
        Me.OkButton = New System.Windows.Forms.Button
        Me.Intro = New System.Windows.Forms.Label
        Me.NoneCheckbox = New System.Windows.Forms.CheckBox
        Me.ActivePermitsCheckedListBox = New System.Windows.Forms.CheckedListBox
        Me.Warning = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'OkButton
        '
        Me.OkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OkButton.Location = New System.Drawing.Point(211, 298)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(67, 23)
        Me.OkButton.TabIndex = 0
        Me.OkButton.Text = "OK"
        '
        'Intro
        '
        Me.Intro.AutoSize = True
        Me.Intro.Location = New System.Drawing.Point(9, 18)
        Me.Intro.Name = "Intro"
        Me.Intro.Size = New System.Drawing.Size(203, 13)
        Me.Intro.TabIndex = 1
        Me.Intro.Text = "Select any permits revoked by this action:"
        '
        'NoneCheckbox
        '
        Me.NoneCheckbox.AutoSize = True
        Me.NoneCheckbox.Location = New System.Drawing.Point(15, 48)
        Me.NoneCheckbox.Name = "NoneCheckbox"
        Me.NoneCheckbox.Size = New System.Drawing.Size(52, 17)
        Me.NoneCheckbox.TabIndex = 2
        Me.NoneCheckbox.Text = "None"
        Me.NoneCheckbox.UseVisualStyleBackColor = True
        '
        'ActivePermitsCheckedListBox
        '
        Me.ActivePermitsCheckedListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ActivePermitsCheckedListBox.CheckOnClick = True
        Me.ActivePermitsCheckedListBox.FormattingEnabled = True
        Me.ActivePermitsCheckedListBox.Location = New System.Drawing.Point(12, 71)
        Me.ActivePermitsCheckedListBox.Name = "ActivePermitsCheckedListBox"
        Me.ActivePermitsCheckedListBox.Size = New System.Drawing.Size(266, 214)
        Me.ActivePermitsCheckedListBox.TabIndex = 3
        '
        'Warning
        '
        Me.Warning.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Warning.AutoSize = True
        Me.Warning.ForeColor = System.Drawing.Color.DarkRed
        Me.Warning.Location = New System.Drawing.Point(9, 303)
        Me.Warning.Name = "Warning"
        Me.Warning.Size = New System.Drawing.Size(156, 13)
        Me.Warning.TabIndex = 1
        Me.Warning.Text = "You must make a selection first."
        Me.Warning.Visible = False
        '
        'SsppPermitRevocationDialog
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(290, 333)
        Me.ControlBox = False
        Me.Controls.Add(Me.ActivePermitsCheckedListBox)
        Me.Controls.Add(Me.NoneCheckbox)
        Me.Controls.Add(Me.Warning)
        Me.Controls.Add(Me.Intro)
        Me.Controls.Add(Me.OkButton)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SsppPermitRevocationDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Permit Revocation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents Intro As System.Windows.Forms.Label
    Friend WithEvents NoneCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents ActivePermitsCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Warning As System.Windows.Forms.Label

End Class
