Imports System.Configuration

Namespace UrlHelpers

    Public Module GecoUrls

        Friend ReadOnly GecoUrl As New Uri(ConfigurationManager.AppSettings("GecoUrl"))

        ' Invoices (by GUID)

        Public Function GetInvoiceUrl(invoiceGuid As Guid) As Uri
            Return New Uri(GecoUrl, $"Invoice/?id={invoiceGuid}")
        End Function

        Public Sub OpenInvoiceUrl(invoiceGuid As Guid, Optional objectSender As Form = Nothing)
            OpenUrl(GetInvoiceUrl(invoiceGuid), objectSender)
        End Sub

        ' Emission fee invoices (by Facility ID and fee year)

        Public Function GetEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer) As Uri
            ArgumentNotNull(airs, NameOf(airs))
            Return New Uri(GecoUrl, $"Invoice/?Facility={airs.ShortString}&FeeYear={feeYear}")
        End Function

        Public Function GetEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer, invoiceID As Integer) As Uri
            ArgumentNotNull(airs, NameOf(airs))
            Return New Uri(GecoUrl, $"Invoice/?Facility={airs.ShortString}&FeeYear={feeYear}&InvoiceId={invoiceID}")
        End Function

        Public Sub OpenEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer, Optional objectSender As Form = Nothing)
            OpenUrl(GetEmissionFeeInvoiceUrl(airs, feeYear), objectSender)
        End Sub

        Public Sub OpenEmissionFeeInvoiceUrl(airs As Apb.ApbFacilityId, feeYear As Integer, invoiceID As Integer, Optional objectSender As Form = Nothing)
            OpenUrl(GetEmissionFeeInvoiceUrl(airs, feeYear, invoiceID), objectSender)
        End Sub

        ' Permit applications

        Public Function GetPermitApplicationUrl(appNumber As Integer) As Uri
            Return New Uri(GecoUrl, $"Permits/Application.aspx?id={appNumber}")
        End Function

        Public Sub OpenPermitApplicationUrl(appNumber As Integer, Optional objectSender As Form = Nothing)
            OpenUrl(GetPermitApplicationUrl(appNumber), objectSender)
        End Sub

        ' GECO accounts

        Public Sub OpenEmailChangeSuccessPage(email As String, token As String, Optional objectSender As Form = Nothing)
            Dim uri As New Uri(GecoUrl, $"Account.aspx?action=change-email&acct={email}&token={token}")
            OpenUrl(uri, objectSender)
        End Sub

    End Module

End Namespace
