Imports Iaip.Apb
Imports Iaip.DAL.Finance.Deposits

Public Class FinSearchDeposits

    Private selectedID As Integer
    Private selectedIdErrorProvider As IaipErrorProvider

    Protected Overrides Sub OnLoad(e As EventArgs)
        lblSelectedIdMessage.Text = ""
        lblAirsSearchMessage.Text = ""
        lblResultsCount.Text = ""
        selectedIdErrorProvider = New IaipErrorProvider(txtSelectedItem, lblSelectedIdMessage)

        MyBase.OnLoad(e)
    End Sub

    ' Search

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        dgvSearchResults.DataSource = SearchDeposits(
            txtDepositNo.Text,
            txtBatch.Text,
            txtCheckNo.Text,
            txtCreditConf.Text,
            txtAirsNumberSearch.AirsNumber,
            If(dtpDateStart.Checked, CType(dtpDateStart.Value, Date?), Nothing),
            If(dtpDateEnd.Checked, CType(dtpDateEnd.Value, Date?), Nothing),
            chkUnusedBalance.Checked,
            chkIncludeDeleted.Checked)

        If dgvSearchResults.Rows.Count > 0 Then
            dgvSearchResults.Columns().Item("Deleted").Visible = chkIncludeDeleted.Checked
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtDepositNo.Clear()
        txtBatch.Clear()
        txtCheckNo.Clear()
        txtCreditConf.Clear()
        txtAirsNumberSearch.Clear()
        chkIncludeDeleted.Checked = False
        chkUnusedBalance.Checked = False
        dtpDateEnd.Value = Today
        dtpDateEnd.Checked = False
        dtpDateStart.Value = Today
        dtpDateStart.Checked = False
        txtSelectedItem.Text = ""
        ValidateSelectedID()
        dgvSearchResults.DataSource = Nothing
    End Sub

    ' Open selected

    Private Sub btnOpenDeposit_Click(sender As Object, e As EventArgs) Handles btnOpenSelectedItem.Click
        OpenSelectedItem()
    End Sub

    Private Sub OpenSelectedItem()
        If selectedID > 0 AndAlso Not selectedIdErrorProvider.HasError Then
            OpenDepositView(selectedID)
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

        Select Case ValidateDepositId(txtSelectedItem.Text, newID, True)
            Case DepositValidationResult.Malformed
                errorMessage = "Invalid Deposit ID."
                selectedIdErrorProvider.SetError(errorMessage)
            Case DepositValidationResult.Nonexistent
                errorMessage = "Deposit ID does not exist."
                selectedIdErrorProvider.SetError(errorMessage)
            Case DepositValidationResult.Valid
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
        Handles grpDates.Enter, grpDepositDetails.Enter, grpStatus.Enter
        AcceptButton = btnSearch
    End Sub

    Private Sub MainAcceptButtonUnset(sender As Object, e As EventArgs) _
        Handles grpDates.Leave, grpDepositDetails.Leave, grpStatus.Leave
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