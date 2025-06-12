Imports System.Linq
Imports Iaip.Apb
Imports Iaip.Apb.ApplicationFees
Imports Iaip.DAL
Imports Iaip.DAL.ApplicationFees

Public Class FinRefundView

    ' Properties

    ' Either FacilityId or RefundId will be set when opening form, but not both.
    ' If RefundId is set first, FacilityId will be set when loading refund details.
    Public Property FacilityId As ApbFacilityId
        Get
            Return _facilityId
        End Get
        Set(value As ApbFacilityId)
            If value <> _facilityId Then
                _facilityId = value

                DisplayFacility()

                If RefundId = -1 Then
                    LoadUnbalancedDeposits()
                End If
            End If
        End Set
    End Property
    Private _facilityId As ApbFacilityId

    Public Property RefundId As Integer
        Get
            Return _refundId
        End Get
        Set(value As Integer)
            If value <> _refundId Then
                _refundId = value

                If value > -1 Then
                    LoadRefund()
                End If
            End If
        End Set
    End Property
    Private _refundId As Integer = -1

    ' Load

    Protected Overrides Sub OnLoad(e As EventArgs)
        SetUpAsNewRefund()
        MyBase.OnLoad(e)
    End Sub

    Private Sub DisableForm(msg As String, errorLevel As ErrorLevel)
        DisableControls({btnSaveNew, txtComment, txtRefundAmount, dtpRefundDate, btnDelete, btnRefresh, btnUpdateComment})
        HideControls({btnSaveNew, btnDelete, btnUpdateComment, btnRefresh})
        lblRefundDisplay.Location = New Point(12, 15)
        lblMessage.ShowMessage(msg, errorLevel)
    End Sub

    ' New or existing refund

    Private Sub DisplayFacility()
        If FacilityId Is Nothing Then
            lblFacility.Text = String.Empty
            DisableForm("Error loading facility.", ErrorLevel.Error)
            Return
        End If

        lblFacility.Text = String.Concat(FacilityId.FormattedString, " ", GetFacilityName(FacilityId))
    End Sub

    ' New refund
    Private Sub SetUpAsNewRefund()
        lblMessage.Text = ""
        lblMessage.BackColor = New Color()
        dtpRefundDate.Value = Today
        btnRefresh.Visible = False
        lblRefundDisplay.Location = New Point(12, 15)
        btnSaveNew.Visible = True
        btnUpdateComment.Visible = False
        btnDelete.Visible = False

        SetPermissions()
    End Sub

    Private Sub LoadUnbalancedDeposits()
        Dim ds As DataSet = GetFacilityFinances(FacilityId)
        dgvDeposits.LinkifyColumnByName = "Deposit ID"
        dgvDeposits.DataSource = ds.Tables("Credits")
        dgvDeposits.SelectNone()

        If ds.Tables("Credits") IsNot Nothing AndAlso ds.Tables("Credits").Rows.Count > 0 Then
            txtCredits.Amount = ds.Tables("Credits").AsEnumerable().
                Sum(Function(x) x.Field(Of Decimal)("Unused Balance"))
            txtRefundAmount.MaxValue = -txtCredits.Amount
        Else
            DisableForm("No credits available to refund.", ErrorLevel.Warning)
        End If
    End Sub

    Private Sub btnSaveNew_Click(sender As Object, e As EventArgs) Handles btnSaveNew.Click
        If RefundId <> -1 Then
            DisableForm("Error.", ErrorLevel.Error)
            Return
        End If

        If txtRefundAmount.Amount = 0 Then
            lblMessage.ShowMessage("Refund amount must be greater than zero.", ErrorLevel.Warning)
            Return
        End If

        Dim refund As New Refund With {
            .Amount = txtRefundAmount.Amount,
            .Comment = txtComment.Text,
            .FacilityID = FacilityId,
            .RefundDate = dtpRefundDate.Value
        }

        Dim newRefundId As Integer

        Dim result As SaveRefundResult = SaveNewRefund(refund, newRefundId)

        Select Case result
            Case SaveRefundResult.Success
                lblMessage.ShowMessage("Refund successfully saved.", ErrorLevel.Success)
                RefundId = newRefundId

            Case SaveRefundResult.NonPositiveAmount
                lblMessage.ShowMessage("Refund amount must be greater than zero.", ErrorLevel.Warning)

            Case SaveRefundResult.InsufficientFunds
                lblMessage.ShowMessage("Refund amount cannot be greater than available credits.", ErrorLevel.Warning)

            Case Else
                DisableForm("A database occurred.", ErrorLevel.Error)
        End Select

    End Sub

    ' Existing refund

    Private Sub LoadRefund()
        btnSaveNew.Visible = False
        btnUpdateComment.Visible = True
        btnDelete.Visible = True

        If AcceptButton Is btnSaveNew Then
            AcceptButton = btnUpdateComment
        End If

        lblRefundDisplay.Location = New Point(42, 15)
        lblRefundDisplay.Text = String.Concat("Refund ID ", RefundId.ToString)
        Name = String.Concat("Refund ID ", RefundId.ToString)

        Dim refund As Refund = GetRefund(RefundId)

        If refund Is Nothing Then
            DisableForm("Refund ID does not exist.", ErrorLevel.Error)
            Return
        End If

        btnRefresh.Visible = True

        dtpRefundDate.Value = refund.RefundDate
        txtRefundAmount.Amount = refund.Amount
        txtComment.Text = refund.Comment

        DisableControls({dtpRefundDate, txtRefundAmount})

        txtCredits.Visible = False
        lblCredits.Visible = False

        FacilityId = refund.FacilityID

        If refund.Deleted Then
            DisableForm("Refund has been deleted.", ErrorLevel.Info)
            lblDepositList.Visible = False
            dgvDeposits.Visible = False
        Else
            lblDepositList.Text = "Refund applied to following deposits:"
            dgvDeposits.LinkifyColumnByName = "DepositId"
            dgvDeposits.DataSource = refund.RefundsApplied
            dgvDeposits.Columns("RefundID").Visible = False
            dgvDeposits.SelectNone()
        End If

        SetPermissions()
    End Sub

    Private Sub SetPermissions()
        If Not CurrentUser.HasPermission(UserCan.EditFinancialData) Then
            HideControls({btnSaveNew, btnDelete})
        End If
    End Sub

    Private Sub btnUpdateComment_Click(sender As Object, e As EventArgs) Handles btnUpdateComment.Click
        If RefundId = -1 Then
            DisableForm("Refund does not exist.", ErrorLevel.Error)
            Return
        End If

        Dim result As UpdateRefundCommentResult = UpdateRefundComment(RefundId, txtComment.Text)

        Select Case result
            Case UpdateRefundCommentResult.Success
                lblMessage.ShowMessage("Comment successfully updated.", ErrorLevel.Success)
            Case UpdateRefundCommentResult.DoesNotExist
                DisableForm("Refund does not exist.", ErrorLevel.Error)
            Case UpdateRefundCommentResult.RefundDeleted
                DisableForm("Refund has been deleted.", ErrorLevel.Error)
            Case Else
                DisableForm("A database occurred.", ErrorLevel.Error)
        End Select
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If RefundId = -1 Then
            DisableForm("Refund does not exist.", ErrorLevel.Error)
            Return
        End If

        Dim result As DeleteRefundResult = DeleteRefund(RefundId)

        Select Case result
            Case DeleteRefundResult.Success
                DisableForm("Refund successfully deleted.", ErrorLevel.Success)
            Case DeleteRefundResult.AlreadyDeleted
                DisableForm("Refund has already been deleted.", ErrorLevel.Warning)
            Case DeleteRefundResult.DoesNotExist
                DisableForm("Refund does not exist.", ErrorLevel.Error)
            Case Else
                DisableForm("A database occurred.", ErrorLevel.Error)
        End Select
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If RefundId > -1 Then
            LoadRefund()
        End If
    End Sub

    ' DataGridView events

    Private Sub dgvDeposits_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvDeposits.CellLinkActivated
        OpenDepositView(CInt(e.LinkValue))
    End Sub

    Private Sub SetAcceptButton(sender As Object, e As EventArgs) _
        Handles dtpRefundDate.Enter, txtRefundAmount.Enter, txtComment.Enter

        If btnSaveNew.Visible Then
            AcceptButton = btnSaveNew
        Else
            AcceptButton = btnUpdateComment
        End If
    End Sub

    Private Sub UnsetAcceptButton(sender As Object, e As EventArgs) _
        Handles dtpRefundDate.Leave, txtRefundAmount.Leave, txtComment.Leave
        AcceptButton = Nothing
    End Sub

End Class