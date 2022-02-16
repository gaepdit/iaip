Namespace UrlHelpers

    Public Module PermitSearchUrls

        Private PermitSearchUrl As New Uri("https://permitsearch.gaepd.org/")

        Public Sub OpenPermitSearchUrl(airsNumber As Apb.ApbFacilityId, Optional objectSender As Form = Nothing)
            ArgumentNotNull(airsNumber, NameOf(airsNumber))
            OpenUrl(New Uri(PermitSearchUrl, $"?AirsNumber={airsNumber.ShortString}"), objectSender)
        End Sub

    End Module

End Namespace
