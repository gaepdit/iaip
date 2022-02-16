Namespace UrlHelpers

    Public Module SupportUrls

        Private ReadOnly DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
        Private ReadOnly SupportUrl As New Uri("https://iaip.gaepd.org/")
        Private ReadOnly ChangelogUrl As New Uri("https://iaip.gaepd.org/changelog/")
        Private ReadOnly PrereqInstallUrl As New Uri("https://iaip.gaepd.org/pre-install/")

        Public Sub OpenDocumentationUrl(Optional objectSender As Form = Nothing)
            OpenUrl(DocumentationUrl, objectSender)
        End Sub

        Public Sub OpenSupportUrl(Optional objectSender As Form = Nothing)
            OpenUrl(SupportUrl, objectSender)
        End Sub

        Public Sub OpenChangelogUrl(Optional objectSender As Form = Nothing)
            OpenUrl(ChangelogUrl, objectSender)
        End Sub

        Public Sub OpenPrereqInstallUrl(Optional objectSender As Form = Nothing)
            OpenUrl(PrereqInstallUrl, objectSender)
        End Sub

    End Module

End Namespace
