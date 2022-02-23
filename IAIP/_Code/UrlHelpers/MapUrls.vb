Namespace UrlHelpers

    Public Module MapUrls

        Public Sub OpenGoogleMapUrl(addressString As String, Optional sender As Form = Nothing)
            OpenUrl(New Uri($"https://maps.google.com/maps?q={addressString}"), sender)
        End Sub

        Public Sub OpenAcmeMapUrl(Optional sender As Form = Nothing)
            OpenUrl(New Uri("https://mapper.acme.com/"), sender)
        End Sub

    End Module

End Namespace
