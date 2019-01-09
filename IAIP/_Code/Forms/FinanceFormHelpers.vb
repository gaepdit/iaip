Imports Iaip.Apb

Module FinanceFormHelpers

    ' Invoice

    Public Function OpenInvoiceView(invoiceId As Integer) As FinInvoiceView
        If MultiFormIsOpen(FinInvoiceView, invoiceId) Then
            Return CType(OpenMultiForm(FinInvoiceView, invoiceId), FinInvoiceView)
        End If

        Dim newInvoiceView As FinInvoiceView = CType(OpenMultiForm(FinInvoiceView, invoiceId), FinInvoiceView)
        newInvoiceView.InvoiceID = invoiceId

        Return newInvoiceView
    End Function

    ' Deposit

    Public Function OpenDepositView(depositID As Integer) As FinDepositView
        If MultiFormIsOpen(FinDepositView, depositID) Then
            Return CType(OpenMultiForm(FinDepositView, depositID), FinDepositView)
        End If

        Dim frm As FinDepositView = CType(OpenMultiForm(FinDepositView, depositID), FinDepositView)
        frm.DepositID = depositID

        Return frm
    End Function

    'Facility

    Public Function OpenFacilityAccount(facilityID As ApbFacilityId) As FinFacilityView
        If MultiFormIsOpen(FinFacilityView, facilityID.ToInt()) Then
            Return CType(OpenMultiForm(FinFacilityView, facilityID.ToInt()), FinFacilityView)
        End If

        Dim newFacilityView As FinFacilityView = CType(OpenMultiForm(FinFacilityView, facilityID.ToInt()), FinFacilityView)
        newFacilityView.FacilityID = facilityID

        Return newFacilityView
    End Function

    ' Refund

    Public Function OpenNewRefund(facilityId As ApbFacilityId) As FinRefundView
        If MultiFormIsOpen(FinRefundView, -facilityId.ToInt()) Then
            Return CType(OpenMultiForm(FinRefundView, -facilityId.ToInt()), FinRefundView)
        End If

        Dim newRefund As FinRefundView = CType(OpenMultiForm(FinRefundView, -facilityId.ToInt()), FinRefundView)
        newRefund.FacilityId = facilityId

        Return newRefund
    End Function

    Public Function OpenRefundView(refundId As Integer) As FinRefundView
        If MultiFormIsOpen(FinRefundView, refundId) Then
            Return CType(OpenMultiForm(FinRefundView, refundId), FinRefundView)
        End If

        Dim refundView As FinRefundView = CType(OpenMultiForm(FinRefundView, refundId), FinRefundView)
        refundView.RefundId = refundId

        Return refundView
    End Function

End Module
