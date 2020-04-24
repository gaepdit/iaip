Imports Iaip.Apb
Imports Iaip.DAL.Finance.Invoices

Public Class FinSearchInvoices

    Private selectedID As Integer
    Private selectedIdErrorProvider As IaipErrorProvider

    Protected Overrides Sub OnLoad(e As EventArgs)
        ClearMessages()

        MyBase.OnLoad(e)
    End Sub

    Private Sub ClearMessages()
        lblSelectedIdMessage.ClearMessage()
        lblAirsSearchMessage.ClearMessage()
        lblResultsCount.ClearMessage()
        selectedIdErrorProvider = New IaipErrorProvider(txtSelectedItem, lblSelectedIdMessage)
    End Sub

    ' Search

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim category As Char = Nothing
        If rdbCategoryApplicationFees.Checked Then
            category = "P"c
        ElseIf rdbCategoryEmissionFees.Checked Then
            category = "E"c
        End If

        dgvSearchResults.DataSource = SearchInvoices(
            txtAirsNumberSearch.AirsNumber,
            txtFacilityName.Text,
            If(dtpDateStart.Checked, CType(dtpDateStart.Value, Date?), Nothing),
            If(dtpDateEnd.Checked, CType(dtpDateEnd.Value, Date?), Nothing),
            category,
            chkOnlyOpenInvoices.Checked,
            chkIncludeVoided.Checked)

        If dgvSearchResults.Rows.Count > 0 Then
            dgvSearchResults.Columns().Item("Voided").Visible = chkIncludeVoided.Checked
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtAirsNumberSearch.Clear()
        txtFacilityName.Clear()
        chkIncludeVoided.Checked = False
        chkOnlyOpenInvoices.Checked = False
        dtpDateEnd.Value = Today
        dtpDateEnd.Checked = False
        dtpDateStart.Value = Today
        dtpDateStart.Checked = False
        txtSelectedItem.Text = ""
        ValidateSelectedID()
        dgvSearchResults.DataSource = Nothing
    End Sub

    ' Open selected

    Private Sub btnOpenSelectedItem_Click(sender As Object, e As EventArgs) Handles btnOpenSelectedItem.Click
        OpenSelectedItem()
    End Sub

    Private Sub OpenSelectedItem()
        If selectedID > 0 AndAlso Not selectedIdErrorProvider.HasError Then
            OpenInvoiceView(selectedID)
        End If
    End Sub

    ' Invoice ID validation

    Private Sub txtSelectedItem_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSelectedItem.Validating
        ValidateSelectedID()
    End Sub

    Private Sub ValidateSelectedID()
        selectedID = 0

        If String.IsNullOrEmpty(txtSelectedItem.Text) Then
            selectedIdErrorProvider.ClearError()
            Return
        End If

        Dim newID As Integer = 0
        Dim errorMessage As String

        Select Case ValidateInvoiceId(txtSelectedItem.Text, newID, True)
            Case InvoiceValidationResult.Malformed
                errorMessage = "Invalid Invoice ID."
                selectedIdErrorProvider.SetError(errorMessage)
            Case InvoiceValidationResult.Nonexistent
                errorMessage = "Invoice ID does not exist."
                selectedIdErrorProvider.SetError(errorMessage)
            Case InvoiceValidationResult.Valid
                selectedID = newID
                txtSelectedItem.Text = newID.ToString
                selectedIdErrorProvider.ClearError()
        End Select
    End Sub

    ' Accept button

    Private Sub txtSelectedItem_Enter(sender As Object, e As EventArgs) Handles txtSelectedItem.Enter
        AcceptButton = btnOpenSelectedItem
    End Sub

    Private Sub txtSelectedItem_Leave(sender As Object, e As EventArgs) Handles txtSelectedItem.Leave
        AcceptButton = Nothing
    End Sub

    Private Sub MainAcceptButtonSet(sender As Object, e As EventArgs) _
        Handles grpFacility.Enter, grpDates.Enter, grpCategory.Enter, grpStatus.Enter
        AcceptButton = btnSearch
    End Sub

    Private Sub MainAcceptButtonUnset(sender As Object, e As EventArgs) _
        Handles grpFacility.Leave, grpDates.Leave, grpCategory.Leave, grpStatus.Leave
        AcceptButton = Nothing
    End Sub

    ' DataGridView events

    Private Sub dgvSearchResults_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvSearchResults.CellLinkActivated
        OpenSelectedItem()
    End Sub

    Private Sub dgvSearchResults_CellLinkSelected(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvSearchResults.CellLinkSelected
        txtSelectedItem.Text = e.LinkValue.ToString
        ValidateSelectedID()
    End Sub

    Private Sub dgvSearchResults_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvSearchResults.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) AndAlso
                dgvSearchResults.Columns(e.ColumnIndex).Name = "Facility ID" Then

            e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
        End If
    End Sub

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If selectedIdErrorProvider IsNot Nothing Then selectedIdErrorProvider.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class