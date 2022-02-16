Namespace UrlHelpers

    Public Module MapUrls

        Public Sub OpenGoogleMapUrl(addressString As String, Optional objectSender As Form = Nothing)
            OpenUrl(New Uri($"https://maps.google.com/maps?q={addressString}"), objectSender)
        End Sub

        Public Sub OpenAcmeMapUrl(Optional objectSender As Form = Nothing)
            OpenUrl(New Uri("https://mapper.acme.com/"), objectSender)
        End Sub

    End Module

End Namespace
