﻿Imports Iaip.Apb
Imports Iaip.DAL.Finance.FacilityFinances

Public Class FinSearchFacilities

    Protected Overrides Sub OnLoad(e As EventArgs)
        lblAirsSearchMessage.Text = ""
        lblSelectedAirsMessage.Text = ""
        lblResultsCount.Text = ""

        MyBase.OnLoad(e)
    End Sub

    ' Search

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        dgvSearchResults.DataSource = SearchFacilityAccounts(
            txtAirsNumberSearch.AirsNumber,
            txtFacilityName.Text,
            chkOpenInvoices.Checked,
            chkUnusedCredits.Checked,
            chkPendingItems.Checked)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtAirsNumberSearch.Clear()
        txtFacilityName.Clear()
        chkOpenInvoices.Checked = False
        chkPendingItems.Checked = False
        chkUnusedCredits.Checked = False
        txtSelectedItem.Clear()
        dgvSearchResults.DataSource = Nothing
    End Sub

    ' Open selected

    Private Sub btnOpenSelectedItem_Click(sender As Object, e As EventArgs) Handles btnOpenSelectedItem.Click
        OpenSelectedItem()
    End Sub

    Private Sub OpenSelectedItem()
        If txtSelectedItem.AirsNumber IsNot Nothing AndAlso Not txtSelectedItem.HasError Then
            OpenFacilityAccount(txtSelectedItem.AirsNumber)
        End If
    End Sub

    ' Accept button

    Private Sub txtSelectedAirsNumber_Enter(sender As Object, e As EventArgs) Handles txtSelectedItem.Enter
        AcceptButton = btnOpenSelectedItem
    End Sub

    Private Sub txtInvoiceID_Leave(sender As Object, e As EventArgs) Handles txtSelectedItem.Leave
        AcceptButton = Nothing
    End Sub

    Private Sub MainAcceptButtonSet(sender As Object, e As EventArgs) _
        Handles grpFacility.Enter, btnAccounts.Enter
        AcceptButton = btnSearch
    End Sub

    Private Sub MainAcceptButtonUnset(sender As Object, e As EventArgs) _
        Handles grpFacility.Leave, btnAccounts.Leave
        AcceptButton = Nothing
    End Sub

    ' DataGridView events

    Private Sub dgvSearchResults_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSearchResults.CellClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgvSearchResults.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub dgvSearchResults_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSearchResults.CellDoubleClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgvSearchResults.RowCount AndAlso e.ColumnIndex <> 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub dgvSearchResults_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSearchResults.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvSearchResults.RowCount AndAlso dgvSearchResults(0, e.RowIndex).Value IsNot Nothing Then
            txtSelectedItem.AirsNumber = New ApbFacilityId(dgvSearchResults(0, e.RowIndex).Value.ToString)
        End If
    End Sub

    Private Sub dgvSearchResults_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvSearchResults.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) AndAlso
            dgvSearchResults.Columns(e.ColumnIndex).Name = "Facility ID" Then

            e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
        End If
    End Sub

End Class