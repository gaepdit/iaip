Imports System.ComponentModel

Namespace Apb.Finance
    Public Class Refund

        <DisplayName("Refund ID")>
        Public Property RefundID As Integer
        <DisplayName("Deposit ID")>
        Public Property DepositID As Integer
        <DisplayName("Refund Date")>
        Public Property RefundDate As Date
        Public Property Amount As Decimal
        <Browsable(False)>
        Public Property Comment As String
        Public Property Deleted As Boolean

    End Class
End Namespace
