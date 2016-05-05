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

    Const _intSpacing As Integer = 10

    Private Sub UserErrorDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)

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
        Try
            '-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
            Using g As Graphics = Graphics.FromHwnd(ctl.Handle)
                Dim objSizeF As SizeF = g.MeasureString(ctl.Text, ctl.Font, New SizeF(ctl.Width, ctl.Height))
                ctl.Height = Convert.ToInt32(objSizeF.Height) + 5
            End Using
        Catch ex As System.Security.SecurityException
            '-- do nothing; we can't set control sizes without full trust
        End Try
    End Sub

    Private Sub btnMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMore.Click
        If btnMore.Text = ">>" Then
            Me.Height = Me.Height + 300
            With ErrorDetails
                .Location = New System.Drawing.Point(MoreHeading.Left, btnMore.Top + btnMore.Height + _intSpacing)
                .Height = Me.ClientSize.Height - ErrorDetails.Top - _intSpacing - btnOK.Height - _intSpacing
                .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom _
                            Or AnchorStyles.Left Or AnchorStyles.Right
                .Visible = True
            End With
            btnMore.Text = "<<"
            btnCopy.Visible = True
            btnCopy.Focus()
        Else
            Me.SuspendLayout()
            btnMore.Text = ">>"
            Me.ClientSize = New Size(Me.ClientSize.Width, btnMore.Top + btnMore.Height + _intSpacing + btnOK.Height + _intSpacing)
            ErrorDetails.Anchor = AnchorStyles.None
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