Imports System.ComponentModel

Namespace Apb.ApplicationFees
    Public Class RefundApplied

        <Browsable(False)>
        Public Property RefundAppliedId As Integer
        <DisplayName("Refund ID")>
        Public Property RefundId As Integer
        <DisplayName("Deposit ID")>
        Public Property DepositId As Integer
        <DisplayName("Amount Applied")>
        Public Property AmountApplied As Decimal
        <DisplayName("Refund Date")>
        Public Property RefundDate As Date
        <Browsable(False)>
        Public Property RefundDeleted As Boolean
        <Browsable(False)>
        Public Property DepositDeleted As Boolean

    End Class
End Namespace
