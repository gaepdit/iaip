Imports System.Drawing

'--
'-- Generic user error dialog
'--
'-- UI adapted from
'--
'-- Alan Cooper's "About Face: The Essentials of User Interface Design"
'-- Chapter VII, "The End of Errors", pages 423-440
'--
'-- Jeff Atwood
'-- http://www.codinghorror.com
'--
Friend Class ExceptionDialog
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ErrorHeading As System.Windows.Forms.Label
    Friend WithEvents ActionHeading As System.Windows.Forms.Label
    Friend WithEvents MoreHeading As System.Windows.Forms.Label
    Friend WithEvents ErrorDetails As System.Windows.Forms.TextBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents ErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents ActionMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnMore As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ErrorHeading = New System.Windows.Forms.Label
        Me.ActionHeading = New System.Windows.Forms.Label
        Me.MoreHeading = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.ErrorDetails = New System.Windows.Forms.TextBox
        Me.btnMore = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.ErrorMessage = New System.Windows.Forms.TextBox
        Me.ActionMessage = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ErrorHeading
        '
        Me.ErrorHeading.AutoSize = True
        Me.ErrorHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ErrorHeading.Location = New System.Drawing.Point(12, 9)
        Me.ErrorHeading.Name = "ErrorHeading"
        Me.ErrorHeading.Size = New System.Drawing.Size(99, 13)
        Me.ErrorHeading.TabIndex = 0
        Me.ErrorHeading.Text = "What happened:"
        '
        'ActionHeading
        '
        Me.ActionHeading.AutoSize = True
        Me.ActionHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ActionHeading.Location = New System.Drawing.Point(12, 161)
        Me.ActionHeading.Name = "ActionHeading"
        Me.ActionHeading.Size = New System.Drawing.Size(104, 13)
        Me.ActionHeading.TabIndex = 4
        Me.ActionHeading.Text = "What you can do:"
        '
        'MoreHeading
        '
        Me.MoreHeading.AutoSize = True
        Me.MoreHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.MoreHeading.Location = New System.Drawing.Point(12, 321)
        Me.MoreHeading.Name = "MoreHeading"
        Me.MoreHeading.Size = New System.Drawing.Size(76, 13)
        Me.MoreHeading.TabIndex = 6
        Me.MoreHeading.Text = "Error details"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(347, 477)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 24)
        Me.btnOK.TabIndex = 2
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
        Me.ErrorDetails.Size = New System.Drawing.Size(410, 125)
        Me.ErrorDetails.TabIndex = 3
        Me.ErrorDetails.Text = "(detailed information, such as exception details)"
        '
        'btnMore
        '
        Me.btnMore.Location = New System.Drawing.Point(94, 316)
        Me.btnMore.Name = "btnMore"
        Me.btnMore.Size = New System.Drawing.Size(28, 24)
        Me.btnMore.TabIndex = 0
        Me.btnMore.Text = ">>"
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.Location = New System.Drawing.Point(347, 316)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(75, 24)
        Me.btnCopy.TabIndex = 1
        Me.btnCopy.Text = "Copy"
        '
        'ErrorMessage
        '
        Me.ErrorMessage.BackColor = System.Drawing.SystemColors.Control
        Me.ErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ErrorMessage.Enabled = False
        Me.ErrorMessage.Location = New System.Drawing.Point(15, 25)
        Me.ErrorMessage.Multiline = True
        Me.ErrorMessage.Name = "ErrorMessage"
        Me.ErrorMessage.ReadOnly = True
        Me.ErrorMessage.Size = New System.Drawing.Size(407, 120)
        Me.ErrorMessage.TabIndex = 7
        Me.ErrorMessage.TabStop = False
        Me.ErrorMessage.Text = "An error has occurred."
        '
        'ActionMessage
        '
        Me.ActionMessage.BackColor = System.Drawing.SystemColors.Control
        Me.ActionMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ActionMessage.Enabled = False
        Me.ActionMessage.Location = New System.Drawing.Point(15, 177)
        Me.ActionMessage.Multiline = True
        Me.ActionMessage.Name = "ActionMessage"
        Me.ActionMessage.ReadOnly = True
        Me.ActionMessage.Size = New System.Drawing.Size(407, 120)
        Me.ActionMessage.TabIndex = 7
        Me.ActionMessage.TabStop = False
        Me.ActionMessage.Text = "Restart the IAIP and try repeating your last action."
        '
        'ExceptionDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(434, 508)
        Me.Controls.Add(Me.ActionMessage)
        Me.Controls.Add(Me.ErrorMessage)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnMore)
        Me.Controls.Add(Me.ErrorDetails)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.MoreHeading)
        Me.Controls.Add(Me.ActionHeading)
        Me.Controls.Add(Me.ErrorHeading)
        Me.Icon = Global.Iaip.My.Resources.Resources.Icon
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 0)
        Me.Name = "ExceptionDialog"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IAIP has encountered a problem"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Const _intSpacing As Integer = 10

    Private Sub UserErrorDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-- make sure our window is on top
        Me.TopMost = True
        Me.TopMost = False

        '-- More >> has to be expanded
        Me.ErrorDetails.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ErrorDetails.Visible = False
        Me.btnCopy.Visible = False

        '-- size the labels' height to accommodate the amount of text in them
        SizeBox(ErrorMessage)
        SizeBox(ActionMessage)

        '-- now shift everything up
        ActionHeading.Top = ErrorMessage.Top + ErrorMessage.Height + _intSpacing
        ActionMessage.Top = ActionHeading.Top + ActionHeading.Height + _intSpacing

        MoreHeading.Top = ActionMessage.Top + ActionMessage.Height + _intSpacing
        btnMore.Top = MoreHeading.Top - 3
        btnCopy.Top = MoreHeading.Top - 3

        '-- now shift bottom of dialog up
        Me.ClientSize = New Size(Me.ClientSize.Width, btnMore.Top + btnMore.Height + _intSpacing + btnOK.Height + _intSpacing)

        Me.CenterToScreen()
    End Sub

    Private Sub SizeBox(ByVal ctl As System.Windows.Forms.TextBox)
        Dim g As Graphics = Nothing
        Try
            '-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
            g = Graphics.FromHwnd(ctl.Handle)
            Dim objSizeF As SizeF = g.MeasureString(ctl.Text, ctl.Font, New SizeF(ctl.Width, ctl.Height))
            g.Dispose()
            ctl.Height = Convert.ToInt32(objSizeF.Height) + 5
        Catch ex As System.Security.SecurityException
            '-- do nothing; we can't set control sizes without full trust
        Finally
            If Not g Is Nothing Then g.Dispose()
        End Try
    End Sub

    Private Sub btnMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMore.Click
        If btnMore.Text = ">>" Then
            Me.Height = Me.Height + 300
            With ErrorDetails
                .Location = New System.Drawing.Point(MoreHeading.Left, btnMore.Top + btnMore.Height + _intSpacing)
                .Height = Me.ClientSize.Height - ErrorDetails.Top - _intSpacing - btnOK.Height - _intSpacing
                .Anchor = Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Bottom _
                            Or Windows.Forms.AnchorStyles.Left Or Windows.Forms.AnchorStyles.Right
                .Visible = True
            End With
            btnMore.Text = "<<"
            btnCopy.Visible = True
            btnCopy.Focus()
        Else
            Me.SuspendLayout()
            btnMore.Text = ">>"
            Me.ClientSize = New Size(Me.ClientSize.Width, btnMore.Top + btnMore.Height + _intSpacing + btnOK.Height + _intSpacing)
            ErrorDetails.Anchor = Windows.Forms.AnchorStyles.None
            ErrorDetails.Visible = False
            btnCopy.Visible = False
            Me.ResumeLayout()
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(ErrorDetails.Text)
        btnCopy.Text = "Copied!"
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

End Class