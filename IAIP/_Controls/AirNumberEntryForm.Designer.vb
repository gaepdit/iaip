<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AirNumberEntryForm
    Inherits System.Windows.Forms.UserControl

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.AirsEntryTextBox = New Iaip.AirsNumberTextBox()
        Me.SuspendLayout()
        '
        'AirsEntryTextBox
        '
        Me.AirsEntryTextBox.AirsNumber = Nothing
        Me.AirsEntryTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AirsEntryTextBox.Location = New System.Drawing.Point(0, 0)
        Me.AirsEntryTextBox.MaxLength = 9
        Me.AirsEntryTextBox.Name = "AirsEntryTextBox"
        Me.AirsEntryTextBox.Size = New System.Drawing.Size(107, 20)
        Me.AirsEntryTextBox.TabIndex = 0
        '
        'AirNumberEntryForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Controls.Add(Me.AirsEntryTextBox)
        Me.Name = "AirNumberEntryForm"
        Me.Size = New System.Drawing.Size(107, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AirsEntryTextBox As AirsNumberTextBox
End Class
