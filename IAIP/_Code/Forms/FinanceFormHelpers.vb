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

    ' Single-form => New deposit opened from Nav Screen, Invoice, or Facility
    ' Multi-form  => Existing deposit

    'Public Function OpenNewDeposit(facilityID As ApbFacilityId) As FinDepositView
    '    If SingleFormIsOpen(FinDepositView) Then
    '        Return CType(OpenSingleForm(FinDepositView), FinDepositView)
    '    End If

    '    Dim frm As FinDepositView = CType(OpenSingleForm(FinDepositView), FinDepositView)
    '    frm.OpenWithFacilityID = facilityID

    '    Return frm
    'End Function

    'Public Function OpenNewDeposit(invoiceID As Integer) As FinDepositView
    '    If SingleFormIsOpen(FinDepositView) Then
    '        Return CType(OpenSingleForm(FinDepositView), FinDepositView)
    '    End If

    '    Dim frm As FinDepositView = CType(OpenSingleForm(FinDepositView), FinDepositView)
    '    frm.OpenWithInvoiceID = invoiceID

    '    Return frm
    'End Function

    Public Function OpenDeposit(depositID As Integer) As FinDepositView
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

End Module
