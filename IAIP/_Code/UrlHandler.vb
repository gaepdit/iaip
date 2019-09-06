Imports System.Configuration

Public Module UrlHandler

    'URLs

    Private ReadOnly DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Private ReadOnly SupportUrl As New Uri("https://iaip.gaepd.org/")
    Private ReadOnly ChangelogUrl As New Uri("https://iaip.gaepd.org/changelog/")
    Private ReadOnly PrereqInstallUrl As New Uri("https://iaip.gaepd.org/pre-install/")

    Private Const MapUrlFragment As String = "https://maps.google.com/maps?q="
    Private Const PermitSearchUrlFragment As String = "https://permitsearch.gaepd.org/?AirsNumber="

    Friend ReadOnly GecoUrl As New Uri(ConfigurationManager.AppSettings("GecoUrl"))
    Private ReadOnly InvoiceViewUrlFragment As String = String.Concat(GecoUrl.ToString, "Invoice/?id={0}")
    Private ReadOnly EmissionFeeInvoiceViewUrlFragment As String = String.Concat(GecoUrl.ToString, "Invoice/?Facility={0}&FeeYear={1}")
    Private ReadOnly EmissionFeeYearInvoiceViewUrlFragment As String = String.Concat(GecoUrl.ToString, "Invoice/?Facility={0}&FeeYear={1}&InvoiceId={2}")
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

    Public Function GetInvoiceUrl(invoiceGuid As Guid) As Uri
        Return New Uri(GetInvoiceLinkAddress(invoiceGuid))
    End Function

    Public Function GetInvoiceLinkAddress(invoiceGuid As Guid) As String
        Return String.Format(InvoiceViewUrlFragment, invoiceGuid.ToString)
    End Function

    Public Sub OpenInvoiceUrl(invoiceGuid As Guid, Optional objectSender As Form = Nothing)
        OpenUri(GetInvoiceUrl(invoiceGuid), objectSender)
    End Sub

    Public Function GetEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer) As String
        Return String.Format(EmissionFeeInvoiceViewUrlFragment, airs.ShortString, feeYear)
    End Function

    Public Function GetEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer, invoiceID As Integer) As String
        Return String.Format(EmissionFeeYearInvoiceViewUrlFragment, airs.ShortString, feeYear, invoiceID)
    End Function

    Public Sub OpenEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer, Optional objectSender As Form = Nothing)
        OpenUri(New Uri(GetEmissionFeeInvoiceUrl(airs, feeYear)), objectSender)
    End Sub

    Public Sub OpenEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer, invoiceID As Integer, Optional objectSender As Form = Nothing)
        OpenUri(New Uri(GetEmissionFeeInvoiceUrl(airs, feeYear, invoiceID)), objectSender)
    End Sub

    Public Function GetPermitApplicationUrl(appNumber As Integer) As Uri
        Return New Uri(GetPermitApplicationLinkAddress(appNumber))
    End Function

    Public Function GetPermitApplicationLinkAddress(appNumber As Integer) As String
        Return String.Format(ApplicationViewUrlFragment, appNumber.ToString)
    End Function

    Public Sub OpenPermitApplicationUrl(appNumber As Integer, Optional objectSender As Form = Nothing)
        OpenUri(GetPermitApplicationUrl(appNumber), objectSender)
    End Sub

    Private Function OpenUri(uriString As String, Optional sender As Object = Nothing, Optional isMailto As Boolean = False) As Boolean
        ' Reference: https://faithlife.codes/blog/2008/01/using_processstart_to_link_to/
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
