Namespace UrlHelpers

    Public Module PermitSearchUrls

        Private ReadOnly PermitSearchUrl As New Uri("https://permitsearch.gaepd.org/")

        Public Sub OpenPermitSearchUrl(airsNumber As Apb.ApbFacilityId, Optional sender As Form = Nothing)
            NotNull(airsNumber, NameOf(airsNumber))
            OpenUrl(New Uri(PermitSearchUrl, $"?AirsNumber={airsNumber.ShortString}"), sender)
        End Sub

    End Module

End Namespace
