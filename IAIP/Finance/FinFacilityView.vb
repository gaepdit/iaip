Imports System.Linq
Imports Iaip.Apb
Imports Iaip.DAL.FacilityData
Imports Iaip.DAL.Finance

Public Class FinFacilityView

    Private _facilityID As ApbFacilityId
    Public Property FacilityID As ApbFacilityId
        Get
            Return _facilityID
        End Get
        Set(value As ApbFacilityId)
            If value <> _facilityID Then
                _facilityID = value
                LoadFacility()
            End If
        End Set
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        lblDataErrorMessage.Visible = False

        MyBase.OnLoad(e)
    End Sub

    Private Sub LoadFacility()
        lblFacilityDisplay.Text = String.Concat(FacilityID.FormattedString, " ", GetFacilityName(FacilityID))

        Dim ds As DataSet = GetFacilityFinances(FacilityID)

        If ds Is Nothing OrElse ds.Tables.Count <> 4 Then
            lblDataErrorMessage.Visible = True
            lblDataErrorMessage.BackColor = IaipColors.ErrorBackColor
            lblDataErrorMessage.ForeColor = IaipColors.ErrorForeColor

            Exit Sub
        End If

        With dgvInvoices
            .DataSource = ds.Tables("Invoices")
            .SelectNone()
        End With

        If ds.Tables("Invoices") IsNot Nothing AndAlso ds.Tables("Invoices").Rows.Count > 0 Then
            txtAmountInvoiced.Amount = ds.Tables("Invoices").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
            txtPaymentsApplied.Amount = ds.Tables("Invoices").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Payments Applied"))
            txtInvoiceBalance.Amount = txtAmountInvoiced.Amount + txtPaymentsApplied.Amount
        End If

        With dgvCredits
            .DataSource = ds.Tables("Credits")
            .SelectNone()
        End With

        If ds.Tables("Credits") IsNot Nothing AndAlso ds.Tables("Credits").Rows.Count > 0 Then
            txtCredits.Amount = ds.Tables("Credits").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Unused Balance"))
            btnAddRefund.Visible = (txtCredits.Amount > 0)
        Else
            btnAddRefund.Visible = False
        End If

        With dgvPendingItems
            .DataSource = ds.Tables("Pending")
            .SelectNone()
        End With

        If ds.Tables("Pending") IsNot Nothing AndAlso ds.Tables("Pending").Rows.Count > 0 Then
            txtPending.Amount = ds.Tables("Pending").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
        End If

        With dgvRefunds
            .DataSource = ds.Tables("Refunds")
            .SelectNone()
        End With

        If ds.Tables("Refunds") IsNot Nothing AndAlso ds.Tables("Refunds").Rows.Count > 0 Then
            txtRefunds.Amount = ds.Tables("Refunds").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadFacility()
    End Sub

    Private Sub btnAddRefund_Click(sender As Object, e As EventArgs) Handles btnAddRefund.Click
        OpenNewRefund(FacilityID)
    End Sub

    ' Invoices DataGridView events

    Private Sub dgvInvoices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoices.CellClick
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgv.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenInvoiceView(CInt(dgv(0, e.RowIndex).Value))
        End If
    End Sub

    Private Sub dgvInvoices_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoices.CellDoubleClick
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgv.RowCount AndAlso e.ColumnIndex > 0 Then
            OpenInvoiceView(CInt(dgv(0, e.RowIndex).Value))
        End If
    End Sub

    ' Credits DataGridView events

    Private Sub dgvCredits_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCredits.CellClick
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgv.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenDepositView(CInt(dgv(0, e.RowIndex).Value))
        End If
    End Sub

    Private Sub dgvCredits_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCredits.CellDoubleClick
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgv.RowCount AndAlso e.ColumnIndex > 0 Then
            OpenDepositView(CInt(dgv(0, e.RowIndex).Value))
        End If
    End Sub

    ' Refunds DataGridView events

    Private Sub dgvRefunds_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRefunds.CellClick
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgv.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenRefundView(CInt(dgv(0, e.RowIndex).Value))
        End If
    End Sub

    Private Sub dgvRefunds_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRefunds.CellDoubleClick
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgv.RowCount AndAlso e.ColumnIndex > 0 Then
            OpenRefundView(CInt(dgv(0, e.RowIndex).Value))
        End If
    End Sub

End Class