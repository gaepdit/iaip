'--
'-- Generic user error dialog
'--
'-- UI adapted from
'--
'-- Alan Cooper's "About Face: The Essentials of User Interface Design"
'-- Chapter VII, "The End of Errors", pages 423-440
'--
'-- Jeff Atwood
'-- https://www.codinghorror.com
'--
Friend Class ExceptionDialog
    Inherits Form

    Private Const _intSpacing As Integer = 10
    Private Const _showMoreText As String = "Show error details >>"

    Public Property Unrecoverable As Boolean
    Public Property Logged As Boolean

    Private Sub UserErrorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '-- make sure our window is on top
        TopMost = True
        TopMost = False

        '-- show whether unrecoverable 
        If Unrecoverable Then
            btnOK.Text = "Exit"
            Icon = My.Resources.ErrorIcon
        End If

        '-- show whether logged
        If Not Logged Then
            IntroMessage.Text = "A copy of this error message has NOT been sent to EPD-IT."
        End If

        '-- More >> has to be expanded
        btnMore.Text = _showMoreText
        ErrorDetails.Anchor = AnchorStyles.None
        ErrorDetails.Visible = False

        '-- size the labels' height to accommodate the amount of text in them
        SizeBox(ErrorMessage)
        SizeBox(ActionMessage)

        '-- now shift everything up
        ActionHeading.Top = ErrorMessage.Top + ErrorMessage.Height + _intSpacing
        ActionMessage.Top = ActionHeading.Top + ActionHeading.Height + _intSpacing
        btnMore.Top = ActionMessage.Top + ActionMessage.Height + _intSpacing + _intSpacing - 3
        btnOK.Top = ActionMessage.Top + ActionMessage.Height + _intSpacing + _intSpacing - 3

        '-- now shift bottom of dialog up
        ClientSize = New Size(ClientSize.Width, btnOK.Top + btnOK.Height + _intSpacing)

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
        Height += 300

        With ErrorDetails
            .Location = New Point(btnOK.Left, btnOK.Top + btnOK.Height + _intSpacing)
            .Height = ClientSize.Height - ErrorDetails.Top - _intSpacing
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            .Visible = True
        End With

        btnMore.Visible = False
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Unrecoverable Then
            CloseIaip()
        Else
            Close()
        End If
    End Sub

End Class