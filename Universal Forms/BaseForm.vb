Public Class BaseForm

#If Not Debug Then
    Private Sub BaseForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If TestingEnvironment Then Me.Icon = My.Resources.TestingIcon
    End Sub
#End If

End Class