Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Apb.Finance
    Public Class Deposit

        <DisplayName("Deposit ID")>
        Public Property DepositID As Integer
        <DisplayName("Deposit Date")>
        Public Property DepositDate As Date
        <DisplayName("Total Amount")>
        Public Property TotalAmount As Decimal
        <DisplayName("Payments Applied")>
        Public Property TotalPaymentsApplied As Decimal
        <DisplayName("Amount Refunded")>
        Public Property TotalRefunded As Decimal

        <DisplayName("Total Amount Allocated")>
        Public ReadOnly Property TotalAmountAllocated As Decimal
            Get
                Return TotalPaymentsApplied + TotalRefunded
            End Get
        End Property

        <DisplayName("Deposit Balance")>
        Public ReadOnly Property DepositBalance As Decimal
            Get
                Return TotalAmount - TotalAmountAllocated
            End Get
        End Property

        <DisplayName("Deposit Number")>
        Public Property DepositNumber As String
        <DisplayName("Batch Number")>
        Public Property BatchNumber As String
        <DisplayName("Check Number")>
        Public Property CheckNumber As String
        <DisplayName("Credit Card Confirmation Number")>
        Public Property CreditCardConf As String
        <DisplayName("Facility ID")>
        Public Property FacilityID As ApbFacilityId
        Public Property Comment As String
        Public Property Deleted As Boolean

        Public Property DepositsApplied As List(Of DepositApplied)
        Public Property RefundsApplied As List(Of RefundApplied)

    End Class
End Namespace
