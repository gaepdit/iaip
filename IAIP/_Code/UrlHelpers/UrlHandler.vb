Namespace UrlHelpers

    Public Module UrlHandler

        Public Function OpenUrl(url As Uri, Optional sender As Form = Nothing, Optional isMailto As Boolean = False) As Boolean
            ArgumentNotNull(url, NameOf(url))
            Return OpenUri(url.ToString, sender, isMailto)
        End Function

        Private Function OpenUri(uriString As String, Optional sender As Form = Nothing, Optional isMailto As Boolean = False) As Boolean
            ' Reference: https://faithlife.codes/blog/2008/01/using_processstart_to_link_to/
            Try
                If sender IsNot Nothing Then sender.Cursor = Cursors.AppStarting

                If String.IsNullOrEmpty(uriString) OrElse Not IsValidURL(uriString, isMailto) Then Return False

                Process.Start(uriString)
                Return True

            Catch ee As Exception When _
            TypeOf ee Is ComponentModel.Win32Exception OrElse
            TypeOf ee Is ObjectDisposedException OrElse
            TypeOf ee Is IO.FileNotFoundException
                Return False
            Finally
                If sender IsNot Nothing Then sender.Cursor = Nothing
            End Try
        End Function

    End Module

End Namespace
