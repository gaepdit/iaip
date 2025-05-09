Imports System.ComponentModel

Namespace Apb.ApplicationFees
    Public Class DepositApplied

        <Browsable(False)>
        Public Property DepositAppliedID As Integer
        <DisplayName("Deposit ID")>
        Public Property DepositID As Integer
        <DisplayName("Invoice ID")>
        Public Property InvoiceID As Integer
        <DisplayName("Facility ID")>
        Public Property FacilityID As ApbFacilityId
        <DisplayName("Amount Applied")>
        Public Property AmountApplied As Decimal
        <Browsable(False)>
        Public Property DepositDate As Date
        <Browsable(False)>
        Public Property InvoiceVoided As Boolean
        <Browsable(False)>
        Public Property DepositDeleted As Boolean

    End Class
End Namespace
