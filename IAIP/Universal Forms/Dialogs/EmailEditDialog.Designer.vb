<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EmailEditDialog
    Inherits System.Windows.Forms.Form

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
        Me.btnOk = New System.Windows.Forms.Button()
        Me.Intro = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.RecipientLabel = New System.Windows.Forms.Label()
        Me.BodyText = New System.Windows.Forms.TextBox()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.BodyLabel = New System.Windows.Forms.Label()
        Me.WarningLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOk.Location = New System.Drawing.Point(397, 487)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(67, 23)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "OK"
        '
        'Intro
        '
        Me.Intro.AutoSize = True
        Me.Intro.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Intro.Location = New System.Drawing.Point(12, 9)
        Me.Intro.Name = "Intro"
        Me.Intro.Size = New System.Drawing.Size(307, 13)
        Me.Intro.TabIndex = 1
        Me.Intro.Text = "Review and edit the generated email before sending."
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(470, 487)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(67, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'RecipientLabel
        '
        Me.RecipientLabel.AutoSize = True
        Me.RecipientLabel.Location = New System.Drawing.Point(92, 35)
        Me.RecipientLabel.Name = "RecipientLabel"
        Me.RecipientLabel.Size = New System.Drawing.Size(52, 13)
        Me.RecipientLabel.TabIndex = 2
        Me.RecipientLabel.Text = "Recipient"
        '
        'BodyText
        '
        Me.BodyText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BodyText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BodyText.Location = New System.Drawing.Point(12, 103)
        Me.BodyText.Multiline = True
        Me.BodyText.Name = "BodyText"
        Me.BodyText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.BodyText.Size = New System.Drawing.Size(525, 365)
        Me.BodyText.TabIndex = 0
        '
        'SubjectLabel
        '
        Me.SubjectLabel.AutoSize = True
        Me.SubjectLabel.Location = New System.Drawing.Point(81, 61)
        Me.SubjectLabel.Name = "SubjectLabel"
        Me.SubjectLabel.Size = New System.Drawing.Size(43, 13)
        Me.SubjectLabel.TabIndex = 2
        Me.SubjectLabel.Text = "Subject"
        '
        'BodyLabel
        '
        Me.BodyLabel.AutoSize = True
        Me.BodyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BodyLabel.Location = New System.Drawing.Point(21, 87)
        Me.BodyLabel.Name = "BodyLabel"
        Me.BodyLabel.Size = New System.Drawing.Size(61, 13)
        Me.BodyLabel.TabIndex = 2
        Me.BodyLabel.Text = "Message:"
        '
        'WarningLabel
        '
        Me.WarningLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarningLabel.AutoSize = True
        Me.WarningLabel.BackColor = System.Drawing.Color.LemonChiffon
        Me.WarningLabel.Location = New System.Drawing.Point(168, 489)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Padding = New System.Windows.Forms.Padding(3)
        Me.WarningLabel.Size = New System.Drawing.Size(226, 19)
        Me.WarningLabel.TabIndex = 1
        Me.WarningLabel.Text = "Clicking ""OK"" will immediately send the email."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Recipient:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Subject:"
        '
        'EmailEditDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(549, 522)
        Me.Controls.Add(Me.BodyText)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RecipientLabel)
        Me.Controls.Add(Me.BodyLabel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SubjectLabel)
        Me.Controls.Add(Me.WarningLabel)
        Me.Controls.Add(Me.Intro)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Icon = Global.Iaip.My.Resources.Resources.EpdIcon
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(565, 538)
        Me.Name = "EmailEditDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Email"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Intro As System.Windows.Forms.Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents RecipientLabel As Label
    Friend WithEvents BodyText As TextBox
    Friend WithEvents SubjectLabel As Label
    Friend WithEvents BodyLabel As Label
    Friend WithEvents WarningLabel As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
