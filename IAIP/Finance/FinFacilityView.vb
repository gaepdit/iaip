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
        lblFacilityDisplay.Text = FacilityID.FormattedString & " " & GetFacilityName(FacilityID)

        Dim ds As DataSet = GetFacilityFinances(FacilityID)

        If ds Is Nothing OrElse ds.Tables.Count <> 3 Then
            lblDataErrorMessage.Visible = True
            lblDataErrorMessage.BackColor = IaipColors.ErrorBackColor
            lblDataErrorMessage.ForeColor = IaipColors.ErrorForeColor

            btnRefresh.Visible = False

            Exit Sub
        End If

        With dgvInvoices
            .DataSource = ds.Tables(0)
            .SelectNone()
        End With

        If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            txtAmountInvoiced.Amount = ds.Tables(0).AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
            txtPaymentsApplied.Amount = ds.Tables(0).AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Payments Applied"))
            txtInvoiceBalance.Amount = txtAmountInvoiced.Amount + txtPaymentsApplied.Amount
        End If

        With dgvCredits
            .DataSource = ds.Tables(1)
            .SelectNone()
        End With

        If ds.Tables(1) IsNot Nothing AndAlso ds.Tables(1).Rows.Count > 0 Then
            txtCredits.Amount = ds.Tables(1).AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Unused Balance"))
            btnAddRefund.Visible = (txtCredits.Amount > 0)
        Else
            btnAddRefund.Visible = False
        End If

        With dgvUnInvoiced
            .DataSource = ds.Tables(2)
            .SelectNone()
        End With
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadFacility()
    End Sub

    Private Sub btnAddRefund_Click(sender As Object, e As EventArgs) Handles btnAddRefund.Click
        ' TODO
    End Sub

    ' Invoices DataGridView events

    Private Sub dgvSearchResults_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoices.CellClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgvInvoices.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenInvoiceView(CInt(dgvInvoices(0, e.RowIndex).Value))
        End If
    End Sub

    Private Sub dgvSearchResults_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoices.CellDoubleClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgvInvoices.RowCount AndAlso e.ColumnIndex > 0 Then
            OpenInvoiceView(CInt(dgvInvoices(0, e.RowIndex).Value))
        End If
    End Sub

    ' Credits DataGridView events

    Private Sub dgvCredits_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCredits.CellClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgvInvoices.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenDeposit(CInt(dgvCredits(0, e.RowIndex).Value))
        End If
    End Sub

    Private Sub dgvCredits_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCredits.CellDoubleClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < dgvInvoices.RowCount AndAlso e.ColumnIndex > 0 Then
            OpenDeposit(CInt(dgvCredits(0, e.RowIndex).Value))
        End If
    End Sub

End Class