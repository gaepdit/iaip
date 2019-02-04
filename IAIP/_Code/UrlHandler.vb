Imports System.Configuration

Public Module UrlHandler

    'URLs

    Private ReadOnly DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Private ReadOnly SupportUrl As New Uri("https://iaip.gaepd.org/")
    Private ReadOnly ChangelogUrl As New Uri("https://iaip.gaepd.org/changelog/")
    Private ReadOnly PrereqInstallUrl As New Uri("https://iaip.gaepd.org/pre-install/")

    Private ReadOnly MapUrlFragment As String = "http://maps.google.com/maps?q="
    Private ReadOnly PermitSearchUrlFragment As String = "http://permitsearch.gaepd.org/?AirsNumber="
    Private ReadOnly VesaUrl As New Uri("https://vnap.cloudapp.net/vnap")

    Friend ReadOnly GecoUrl As New Uri(ConfigurationManager.AppSettings("GecoUrl"))
    Private ReadOnly InvoiceViewUrlFragment As String = String.Concat(GecoUrl.ToString, "Invoice/?id={0}")
    Private ReadOnly ApplicationViewUrlFragment As String = String.Concat(GecoUrl.ToString, "Permits/Application.aspx?id={0}")

    ' Public URL methods

    Public Sub OpenDocumentationUrl(Optional objectSender As Form = Nothing)
        OpenUri(DocumentationUrl, objectSender)
    End Sub

    Public Sub OpenSupportUrl(Optional objectSender As Form = Nothing)
        OpenUri(SupportUrl, objectSender)
    End Sub

    Public Sub OpenChangelogUrl(Optional objectSender As Form = Nothing)
        OpenUri(ChangelogUrl, objectSender)
    End Sub

    Public Sub OpenPrereqInstallUrl(Optional objectSender As Form = Nothing)
        OpenUri(PrereqInstallUrl, objectSender)
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

    Public Function GetInvoiceUrl(invoiceGuid As Guid) As String
        Return String.Format(InvoiceViewUrlFragment, invoiceGuid.ToString)
    End Function

    Public Sub OpenInvoiceUrl(invoiceGuid As Guid, Optional objectSender As Form = Nothing)
        OpenUri(New Uri(GetInvoiceUrl(invoiceGuid)), objectSender)
    End Sub

    Public Function GetPermitApplicationUrl(appNumber As Integer) As String
        Return String.Format(ApplicationViewUrlFragment, appNumber.ToString)
    End Function

    Public Sub OpenPermitApplicationUrl(appNumber As Integer, Optional objectSender As Form = Nothing)
        OpenUri(New Uri(GetPermitApplicationUrl(appNumber)), objectSender)
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
