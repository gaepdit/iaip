Namespace UrlHelpers

    Public Module UrlHandler

        Public Function OpenUrl(url As Uri, Optional objectSender As Object = Nothing, Optional isMailto As Boolean = False) As Boolean
            ArgumentNotNull(url, NameOf(url))
            Return OpenUri(url.ToString, objectSender, isMailto)
        End Function

        Private Function OpenUri(uriString As String, Optional sender As Object = Nothing, Optional isMailto As Boolean = False) As Boolean
            ' Reference: https://faithlife.codes/blog/2008/01/using_processstart_to_link_to/
            Try
                If TypeOf sender Is Form Then
                    CType(sender, Form).Cursor = Cursors.AppStarting
                End If

                If uriString Is Nothing OrElse uriString = "" OrElse Not IsValidURL(uriString, isMailto) Then
                    Return False
                End If

                Process.Start(uriString)

                Return True
            Catch ee As Exception When _
            TypeOf ee Is ComponentModel.Win32Exception OrElse
            TypeOf ee Is ObjectDisposedException OrElse
            TypeOf ee Is IO.FileNotFoundException
                Return False
            Finally
                If TypeOf sender Is Form Then
                    CType(sender, Form).Cursor = Nothing
                End If
            End Try
        End Function

    End Module

End Namespace
