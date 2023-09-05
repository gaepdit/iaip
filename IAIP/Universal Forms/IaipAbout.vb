Imports Iaip.UrlHelpers

Public Class IaipAbout

    Private Sub IaipAbout_Load(sender As Object, e As EventArgs) Handles Me.Load

        lblVersion.Text = "Version " & GetCurrentVersion().ToString

#If DEBUG Then
        lblVersion.Text = lblVersion.Text & " DEV"
        Me.LogoBox.Image = My.Resources.Resources.DevLogo
#ElseIf UAT Then
        lblVersion.Text = lblVersion.Text & " UAT"
        Me.LogoBox.Image = My.Resources.Resources.UatLogo
#End If

    End Sub

    Private Sub lblSupport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblSupport.LinkClicked
        OpenSupportUrl(Me)
    End Sub

    Private Sub lblDocumentation_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblDocumentation.LinkClicked
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub lblChangelog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblChangelog.LinkClicked
        OpenChangelogUrl(Me)
    End Sub

End Class