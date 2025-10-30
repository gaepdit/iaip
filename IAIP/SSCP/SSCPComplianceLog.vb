Imports Iaip.UrlHelpers

Public Class SSCPComplianceLog

    Private Sub lnkWebComplianceWork_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebComplianceWork.LinkClicked
        OpenComplianceWorkOnWeb(Me)
    End Sub

    Private Sub lnkWebFce_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebFce.LinkClicked
        OpenFcesOnWeb(Me)
    End Sub

    Private Sub lnkWebEnforcement_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebEnforcement.LinkClicked
        OpenEnforcementOnWeb(Me)
    End Sub

End Class
