<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExceptionDialog
    Inherits System.Windows.Forms.Form


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents ActionMessage As System.Windows.Forms.TextBox
    Friend WithEvents ErrorDescription As System.Windows.Forms.TextBox
    Friend WithEvents ErrorDetails As System.Windows.Forms.TextBox
    Friend WithEvents ActionHeading As System.Windows.Forms.Label
    Friend WithEvents ErrorHeading As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ErrorHeading = New System.Windows.Forms.Label()
        Me.ActionHeading = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ErrorDetails = New System.Windows.Forms.TextBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.ErrorDescription = New System.Windows.Forms.TextBox()
        Me.ActionMessage = New System.Windows.Forms.TextBox()
        Me.IntroMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ErrorHeading
        '
        Me.ErrorHeading.AutoSize = True
        Me.ErrorHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ErrorHeading.Location = New System.Drawing.Point(12, 41)
        Me.ErrorHeading.Name = "ErrorHeading"
        Me.ErrorHeading.Size = New System.Drawing.Size(99, 13)
        Me.ErrorHeading.TabIndex = 1
        Me.ErrorHeading.Text = "What happened:"
        '
        'ActionHeading
        '
        Me.ActionHeading.AutoSize = True
        Me.ActionHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ActionHeading.Location = New System.Drawing.Point(12, 180)
        Me.ActionHeading.Name = "ActionHeading"
        Me.ActionHeading.Size = New System.Drawing.Size(104, 13)
        Me.ActionHeading.TabIndex = 3
        Me.ActionHeading.Text = "What you can do:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(12, 316)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 24)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        '
        'ErrorDetails
        '
        Me.ErrorDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorDetails.CausesValidation = False
        Me.ErrorDetails.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrorDetails.Location = New System.Drawing.Point(12, 346)
        Me.ErrorDetails.Multiline = True
        Me.ErrorDetails.Name = "ErrorDetails"
        Me.ErrorDetails.ReadOnly = True
        Me.ErrorDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ErrorDetails.Size = New System.Drawing.Size(410, 150)
        Me.ErrorDetails.TabIndex = 7
        Me.ErrorDetails.Text = "(detailed information, such as exception details)"
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(293, 316)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(129, 24)
        Me.btnCopy.TabIndex = 6
        Me.btnCopy.Text = "Copy error details >>"
        '
        'ErrorDescription
        '
        Me.ErrorDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ErrorDescription.Enabled = False
        Me.ErrorDescription.Location = New System.Drawing.Point(15, 57)
        Me.ErrorDescription.Multiline = True
        Me.ErrorDescription.Name = "ErrorDescription"
        Me.ErrorDescription.ReadOnly = True
        Me.ErrorDescription.Size = New System.Drawing.Size(407, 120)
        Me.ErrorDescription.TabIndex = 2
        Me.ErrorDescription.TabStop = False
        Me.ErrorDescription.Text = "An error has occurred."
        '
        'ActionMessage
        '
        Me.ActionMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ActionMessage.Enabled = False
        Me.ActionMessage.Location = New System.Drawing.Point(15, 196)
        Me.ActionMessage.Multiline = True
        Me.ActionMessage.Name = "ActionMessage"
        Me.ActionMessage.ReadOnly = True
        Me.ActionMessage.Size = New System.Drawing.Size(407, 114)
        Me.ActionMessage.TabIndex = 4
        Me.ActionMessage.TabStop = False
        Me.ActionMessage.Text = "Restart the IAIP and try repeating your last action."
        '
        'IntroMessage
        '
        Me.IntroMessage.AutoSize = True
        Me.IntroMessage.Location = New System.Drawing.Point(12, 9)
        Me.IntroMessage.Name = "IntroMessage"
        Me.IntroMessage.Size = New System.Drawing.Size(263, 13)
        Me.IntroMessage.TabIndex = 0
        Me.IntroMessage.Text = "A copy of this error message has been sent to EPD-IT."
        '
        'ExceptionDialog
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(434, 508)
        Me.ControlBox = False
        Me.Controls.Add(Me.IntroMessage)
        Me.Controls.Add(Me.ErrorHeading)
        Me.Controls.Add(Me.ErrorDescription)
        Me.Controls.Add(Me.ActionHeading)
        Me.Controls.Add(Me.ActionMessage)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.ErrorDetails)
        Me.Icon = Global.Iaip.My.Resources.Resources.WarningIcon
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 0)
        Me.Name = "ExceptionDialog"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IAIP has encountered a problem"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IntroMessage As Label
End Class
