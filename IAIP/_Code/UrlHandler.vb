Public Module UrlHandler

    Public Sub OpenDocumentationUrl(Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenHelp")
        If objectSender Is Nothing Then
            ApplicationInsights.TrackEvent("Url.OpenHelp")
        Else
            ApplicationInsights.TrackEvent("Url.OpenHelp", "FromForm", objectSender.Name)
        End If
        OpenUri(DocumentationUrl, objectSender)
    End Sub

    Public Sub OpenSupportUrl(Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenSupport")
        If objectSender Is Nothing Then
            ApplicationInsights.TrackEvent("Url.OpenSupport")
        Else
            ApplicationInsights.TrackEvent("Url.OpenSupport", "FromForm", objectSender.Name)
        End If
        OpenUri(SupportUrl, objectSender)
    End Sub

    Public Sub OpenChangelogUrl(Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenChangelog")
        If objectSender Is Nothing Then
            ApplicationInsights.TrackEvent("Url.OpenChangelog")
        Else
            ApplicationInsights.TrackEvent("Url.OpenChangelog", "FromForm", objectSender.Name)
        End If
        OpenUri(ChangelogUrl, objectSender)
    End Sub

    Public Sub OpenMapUrl(ByVal addressString As String, Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenMap")
        If objectSender Is Nothing Then
            ApplicationInsights.TrackEvent("Url.OpenMap")
        Else
            ApplicationInsights.TrackEvent("Url.OpenMap", "FromForm", objectSender.Name)
        End If
        OpenUri(New Uri(MapUrlFragment & addressString), objectSender)
    End Sub

    Public Sub OpenPermitSearchUrl(ByVal airsNumber As Apb.ApbFacilityId, Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenPermitSearch")
        If objectSender Is Nothing Then
            ApplicationInsights.TrackEvent("Url.OpenPermitSearch")
        Else
            ApplicationInsights.TrackEvent("Url.OpenPermitSearch", "FromForm", objectSender.Name)
        End If
        OpenUri(New Uri(PermitSearchUrlFragment & airsNumber.ToString), objectSender)
    End Sub

    Private Function OpenUri(ByVal uriString As String, Optional ByVal objectSender As Object = Nothing) As Boolean
        ' Reference: http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html
        Try
            If objectSender IsNot Nothing Then objectSender.Cursor = Cursors.AppStarting
            If uriString Is Nothing OrElse uriString = "" Then Return False

            Process.Start(uriString)
            Return True
        Catch ee As Exception When _
        TypeOf ee Is System.ComponentModel.Win32Exception OrElse
        TypeOf ee Is System.ObjectDisposedException OrElse
        TypeOf ee Is System.IO.FileNotFoundException
            Return False
        Finally
            If objectSender IsNot Nothing Then objectSender.Cursor = Nothing
        End Try
    End Function

    Public Function OpenUri(ByVal uri As Uri, Optional ByVal objectSender As Object = Nothing) As Boolean
        Return OpenUri(uri.ToString, objectSender)
    End Function

End Module
