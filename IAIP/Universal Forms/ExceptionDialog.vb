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
    Inherits Form

    Const _intSpacing As Integer = 10
    Const showMoreText As String = "Show error details >>"
    Const hideMoreText As String = "Hide error details <<"

    Public Property Unrecoverable As Boolean = False

    Private Sub UserErrorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '-- make sure our window is on top
        TopMost = True
        TopMost = False

        '-- show whether unrecoverable 
        If Unrecoverable Then
            btnOK.Text = "Exit"
            Icon = My.Resources.ErrorIcon
        End If

        '-- More >> has to be expanded
        btnMore.Text = showMoreText
        ErrorDetails.Anchor = AnchorStyles.None
        ErrorDetails.Visible = False
        btnCopy.Visible = False

        '-- size the labels' height to accommodate the amount of text in them
        SizeBox(ErrorMessage)
        SizeBox(ActionMessage)

        '-- now shift everything up
        ActionHeading.Top = ErrorMessage.Top + ErrorMessage.Height + _intSpacing
        ActionMessage.Top = ActionHeading.Top + ActionHeading.Height + _intSpacing

        btnMore.Top = ActionMessage.Top + ActionMessage.Height + _intSpacing - 3
        btnCopy.Top = btnMore.Top

        '-- now shift bottom of dialog up
        ClientSize = New Size(ClientSize.Width, btnMore.Top + btnMore.Height + _intSpacing + btnOK.Height + _intSpacing)

        CenterToScreen()
    End Sub

    Private Sub SizeBox(ctl As TextBox)
        Try
            '-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
            Using g As Graphics = Graphics.FromHwnd(ctl.Handle)
                Dim objSizeF As SizeF = g.MeasureString(ctl.Text, ctl.Font, New SizeF(ctl.Width, ctl.Height))
                ctl.Height = Convert.ToInt32(objSizeF.Height) + 5
            End Using
        Catch ex As Security.SecurityException
            '-- do nothing; we can't set control sizes without full trust
        End Try
    End Sub

    Private Sub btnMore_Click(sender As Object, e As EventArgs) Handles btnMore.Click
        If btnMore.Text = showMoreText Then
            Height = Height + 300
            With ErrorDetails
                .Location = New Point(btnMore.Left, btnMore.Top + btnMore.Height + _intSpacing)
                .Height = ClientSize.Height - ErrorDetails.Top - _intSpacing - btnOK.Height - _intSpacing
                .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom _
                            Or AnchorStyles.Left Or AnchorStyles.Right
                .Visible = True
            End With
            btnMore.Text = hideMoreText
            btnCopy.Visible = True
            btnCopy.Focus()
        Else
            SuspendLayout()
            btnMore.Text = showMoreText
            ClientSize = New Size(ClientSize.Width, btnMore.Top + btnMore.Height + _intSpacing + btnOK.Height + _intSpacing)
            ErrorDetails.Anchor = AnchorStyles.None
            ErrorDetails.Visible = False
            btnCopy.Visible = False
            ResumeLayout()
        End If
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Clipboard.SetText(ErrorDetails.Text)
        btnCopy.Text = "Copied!"
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Close()
    End Sub

End Class