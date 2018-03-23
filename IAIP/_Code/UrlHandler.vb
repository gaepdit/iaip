Public Module UrlHandler

    Public Sub OpenDocumentationUrl(Optional objectSender As Form = Nothing)
        OpenUri(DocumentationUrl, objectSender)
    End Sub

    Public Sub OpenSupportUrl(Optional objectSender As Form = Nothing)
        OpenUri(SupportUrl, objectSender)
    End Sub

    Public Sub OpenChangelogUrl(Optional objectSender As Form = Nothing)
        OpenUri(ChangelogUrl, objectSender)
    End Sub

    Public Sub OpenMapUrl(addressString As String, Optional objectSender As Form = Nothing)
        OpenUri(New Uri(MapUrlFragment & addressString), objectSender)
    End Sub

    Public Sub OpenPermitSearchUrl(airsNumber As Apb.ApbFacilityId, Optional objectSender As Form = Nothing)
        OpenUri(New Uri(PermitSearchUrlFragment & airsNumber.ToString), objectSender)
    End Sub

    Public Sub OpenVesaUrl(Optional objectSender As Form = Nothing)
        OpenUri(VesaUrl, objectSender)
    End Sub

    Private Function OpenUri(uriString As String, Optional sender As Object = Nothing, Optional isMailto As Boolean = False) As Boolean
        ' Reference: http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html
        Try
            If sender IsNot Nothing AndAlso TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            If uriString Is Nothing OrElse uriString = "" OrElse Not IsValidURL(uriString, isMailto) Then Return False

            Process.Start(uriString)

            Return True
        Catch ee As Exception When _
        TypeOf ee Is ComponentModel.Win32Exception OrElse
        TypeOf ee Is ObjectDisposedException OrElse
        TypeOf ee Is IO.FileNotFoundException
            Return False
        Finally
            If sender IsNot Nothing AndAlso TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If
        End Try
    End Function

    Public Function OpenUri(uri As Uri, Optional objectSender As Object = Nothing, Optional isMailto As Boolean = False) As Boolean
        Return OpenUri(uri.ToString, objectSender, isMailto)
    End Function

End Module
