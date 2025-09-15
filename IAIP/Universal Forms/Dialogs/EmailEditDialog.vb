Public Class EmailEditDialog

    Protected Overrides Sub OnLoad(e As EventArgs)

#If DEBUG Then
        Icon = My.Resources.DevIcon
#ElseIf UAT Then
        Icon = My.Resources.UatIcon
#End If

        MyBase.OnLoad(e)
    End Sub

    Private Sub AcknowledgmentEmailDialog_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        btnCancel.Focus()
    End Sub

End Class
