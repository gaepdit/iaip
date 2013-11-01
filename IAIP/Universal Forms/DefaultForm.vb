Public Class DefaultForm
    Private Sub DefaultForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If TestingEnvironment Then Me.Icon = My.Resources.WarningIcon
    End Sub
End Class