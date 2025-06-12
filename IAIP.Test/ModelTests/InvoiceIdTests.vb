Imports System.Globalization
Imports IAIP.DAL.ApplicationFees.Invoices
Imports Xunit

Public Class InvoiceIdTests

    <Theory>
    <InlineData("")>
    <InlineData("-1234")>
    <InlineData("E-1234")>
    <InlineData("E1234")>
    <InlineData("E-")>
    Public Sub RejectsInvalidInvoiceId(input As String)
        Dim newInvoiceID As Integer = 0
        Assert.Equal(InvoiceValidationResult.Malformed, ValidateInvoiceId(input, newInvoiceID))
        Assert.Equal(0, newInvoiceID)
    End Sub

    <Theory>
    <InlineData("1234")>
    Public Sub AcceptsValidInvoiceId(input As String)
        Dim newInvoiceID As Integer = 0
        Assert.Equal(InvoiceValidationResult.Valid, ValidateInvoiceId(input, newInvoiceID))
        Assert.Equal(Integer.Parse(input, NumberStyles.Integer, CultureInfo.CurrentCulture), newInvoiceID)
    End Sub

End Class
