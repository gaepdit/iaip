Namespace UrlHelpers

    Public Module EventUrls

        Public Function GetEventRegistrationLinkAddress(selectedEventId As Integer?) As Uri
            Return New Uri(GecoUrl, $"/EventRegistration/Details.aspx?eventid={selectedEventId}")
        End Function

    End Module

End Namespace
