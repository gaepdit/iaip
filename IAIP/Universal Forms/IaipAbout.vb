Public Class IaipAbout

    Private Sub IaipAbout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblVersion.Text = "Version " & GetCurrentVersionAsMajorMinorBuild().ToString
    End Sub

    Private Sub lblSupport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblSupport.LinkClicked
        OpenSupportUrl(Me)
    End Sub

    Private Sub lblDocumentation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblDocumentation.LinkClicked
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub lblChangelog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblChangelog.LinkClicked
        OpenChangelogUrl(Me)
    End Sub

    Private Sub lblUpdateCheck_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblUpdateCheck.LinkClicked
        App.CheckForUpdate()
    End Sub

End Class