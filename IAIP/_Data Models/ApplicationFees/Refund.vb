Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Apb.ApplicationFees
    Public Class Refund

        <DisplayName("Refund ID")>
        Public Property RefundId As Integer
        <DisplayName("Refund Date")>
        Public Property RefundDate As Date
        Public Property Amount As Decimal
        <DisplayName("Facility ID")>
        Public Property FacilityID As ApbFacilityId
        Public Property Comment As String
        Public Property Deleted As Boolean
        Public Property RefundsApplied As List(Of RefundApplied)

    End Class
End Namespace
