Namespace UrlHelpers

    Public Module SupportUrls

        Private ReadOnly DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
        Private ReadOnly SupportUrl As New Uri("https://iaip.gaepd.org/")
        Private ReadOnly ChangelogUrl As New Uri("https://iaip.gaepd.org/changelog/")
        Private ReadOnly PrereqInstallUrl As New Uri("https://iaip.gaepd.org/pre-install/")

        Public Sub OpenDocumentationUrl(Optional sender As Form = Nothing)
            OpenUrl(DocumentationUrl, sender)
        End Sub

        Public Sub OpenSupportUrl(Optional sender As Form = Nothing)
            OpenUrl(SupportUrl, sender)
        End Sub

        Public Sub OpenChangelogUrl(Optional sender As Form = Nothing)
            OpenUrl(ChangelogUrl, sender)
        End Sub

        Public Sub OpenPrereqInstallUrl(Optional sender As Form = Nothing)
            OpenUrl(PrereqInstallUrl, sender)
        End Sub

    End Module

End Namespace
