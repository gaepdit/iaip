﻿Public Class IaipAbout

    Private Sub IaipAbout_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        
        lblVersion.Text = "Version " & GetCurrentVersion().ToString

#If SqlServer Then
        lblVersion.Text = lblVersion.Text & " UAT - SQL Server edition"
        Me.LogoBox.Image = My.Resources.Resources.SSTestLogo
#ElseIf UAT Then
        lblVersion.Text = lblVersion.Text & " UAT"
        Me.LogoBox.Image = My.Resources.Resources.UatLogo
#End If

    End Sub

    Private Sub lblSupport_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblSupport.LinkClicked
        OpenSupportUrl(Me)
    End Sub

    Private Sub lblDocumentation_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblDocumentation.LinkClicked
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub lblChangelog_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblChangelog.LinkClicked
        OpenChangelogUrl(Me)
    End Sub

    Private Sub lblUpdateCheck_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblUpdateCheck.LinkClicked
        App.CheckForUpdate()
    End Sub

End Class