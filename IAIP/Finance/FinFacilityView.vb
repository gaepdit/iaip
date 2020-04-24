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

        If ds Is Nothing Then
            lblDataErrorMessage.Visible = True
            lblDataErrorMessage.BackColor = IaipColors.ErrorBackColor
            lblDataErrorMessage.ForeColor = IaipColors.ErrorForeColor

            btnOpenFacilitySummary.Visible = False

            dgvDeposits.DataSource = Nothing
            dgvInvoices.DataSource = Nothing
            dgvPending.DataSource = Nothing
            dgvRefunds.DataSource = Nothing
            dgvCredits.DataSource = Nothing

            Return
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
        Else
            txtAmountInvoiced.Amount = 0
            txtPaymentsApplied.Amount = 0
            txtInvoiceBalance.Amount = 0
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
            txtCredits.Amount = 0
            btnAddRefund.Visible = False
        End If

        With dgvPending
            .DataSource = ds.Tables("Pending")
            .SelectNone()
        End With

        If ds.Tables("Pending") IsNot Nothing AndAlso ds.Tables("Pending").Rows.Count > 0 Then
            txtPending.Amount = ds.Tables("Pending").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
        Else
            txtPending.Amount = 0
        End If

        With dgvRefunds
            .DataSource = ds.Tables("Refunds")
            .SelectNone()
        End With

        If ds.Tables("Refunds") IsNot Nothing AndAlso ds.Tables("Refunds").Rows.Count > 0 Then
            txtRefunds.Amount = ds.Tables("Refunds").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
        Else
            txtRefunds.Amount = 0
        End If

        With dgvDeposits
            .DataSource = ds.Tables("Deposits")
            .SelectNone()
        End With

        If ds.Tables("Deposits") IsNot Nothing AndAlso ds.Tables("Deposits").Rows.Count > 0 Then
            txtDeposits.Amount = ds.Tables("Deposits").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Amount"))
        Else
            txtDeposits.Amount = 0
        End If

        SetPermissions()
    End Sub

    Private Sub SetPermissions()
        If Not CurrentUser.HasPermission(UserCan.EditFinancialData) Then
            HideControls({btnAddRefund})
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadFacility()
    End Sub

    Private Sub btnAddRefund_Click(sender As Object, e As EventArgs) Handles btnAddRefund.Click
        OpenNewRefund(FacilityID)
    End Sub

    ' DataGridView events

    Private Sub dgvInvoices_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvInvoices.CellLinkActivated
        OpenInvoiceView(CInt(e.LinkValue))
    End Sub

    Private Sub dgvDepositsCredits_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvDeposits.CellLinkActivated, dgvCredits.CellLinkActivated
        OpenDepositView(CInt(e.LinkValue))
    End Sub

    Private Sub dgvRefunds_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvRefunds.CellLinkActivated
        OpenRefundView(CInt(e.LinkValue))
    End Sub

    Private Sub btnOpenFacilitySummary_Click(sender As Object, e As EventArgs) Handles btnOpenFacilitySummary.Click
        OpenFormFacilitySummary(FacilityID)
    End Sub
End Class