<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AirNumberEntryForm
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.AirsEntryErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.AirsEntryErrorLabel = New System.Windows.Forms.Label()
        Me.AirsEntryTextBox = New Iaip.AirsNumberTextBox()
        CType(Me.AirsEntryErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AirsEntryErrorProvider
        '
        Me.AirsEntryErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.AirsEntryErrorProvider.ContainerControl = Me
        '
        'AirsEntryErrorLabel
        '
        Me.AirsEntryErrorLabel.AutoSize = True
        Me.AirsEntryErrorLabel.Location = New System.Drawing.Point(0, 23)
        Me.AirsEntryErrorLabel.Name = "AirsEntryErrorLabel"
        Me.AirsEntryErrorLabel.Size = New System.Drawing.Size(0, 13)
        Me.AirsEntryErrorLabel.TabIndex = 1
        '
        'AirsEntryTextBox
        '
        Me.AirsEntryTextBox.AirsNumber = Nothing
        Me.AirsEntryTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AirsEntryTextBox.Cue = "000-00000"
        Me.AirsEntryTextBox.FacilityMustExist = False
        Me.AirsEntryErrorProvider.SetIconPadding(Me.AirsEntryTextBox, -19)
        Me.AirsEntryTextBox.Location = New System.Drawing.Point(0, 0)
        Me.AirsEntryTextBox.MaxLength = 9
        Me.AirsEntryTextBox.Name = "AirsEntryTextBox"
        Me.AirsEntryTextBox.Size = New System.Drawing.Size(107, 21)
        Me.AirsEntryTextBox.TabIndex = 0
        '
        'AirNumberEntryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.AirsEntryErrorLabel)
        Me.Controls.Add(Me.AirsEntryTextBox)
        Me.Name = "AirNumberEntryForm"
        Me.Size = New System.Drawing.Size(107, 39)
        CType(Me.AirsEntryErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AirsEntryTextBox As AirsNumberTextBox
    Friend WithEvents AirsEntryErrorProvider As ErrorProvider
    Friend WithEvents AirsEntryErrorLabel As Label
End Class
