Public Class IaipAbout

    Private Sub IaipAbout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblVersion.Text = "Version " & GetCurrentVersionAsMajorMinorBuild().ToString
    End Sub

    Private Sub lblSupport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblSupport.LinkClicked
        OpenAboutUrl(Me)
    End Sub

    Private Sub lblDocumentation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblDocumentation.LinkClicked
        OpenHelpUrl(Me)
    End Sub

    Private Sub lblChangelog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblChangelog.LinkClicked, lblUpdateCheck.LinkClicked
        OpenChangelogUrl(Me)
    End Sub

End Class