Imports Iaip.ApiCalls.IaipCx
Imports Iaip.UrlHelpers

Public Class IaipAbout

    Private Sub IaipAbout_Load(sender As Object, e As EventArgs) Handles Me.Load

        lblVersion.Text = "Version " & GetCurrentVersion().ToString

#If DEBUG Then
        lblVersion.Text = lblVersion.Text & " DEV"
        LogoBox.Image = My.Resources.Resources.DevLogo
        DisplayDevInfo()
#ElseIf UAT Then
        lblVersion.Text = lblVersion.Text & " UAT"
        LogoBox.Image = My.Resources.Resources.UatLogo
        DisplayDevInfo()
#End If

    End Sub

    Private Sub DisplayDevInfo()
        DevInfoPanel.Visible = True
        lblExternalIPAddress.Text = $"External IP Address: {ExternalIPAddress}"
        lblCurrentServerEnvironment.Text = $"Current Server Environment: {CurrentServerEnvironment}"
        lblCurrentUser.Text = $"Current User: {If(CurrentUser IsNot Nothing, CurrentUser.Username, "None")}"
        lblNetworkStatus.Text = $"Initial Network Status: {NetworkStatus}"
        lblVpnInterfaceAdapter.Text = $"VPN Interface Adapter: {VpnInterfaceAdapter}"
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