Imports Iaip.Apb
Imports Iaip.Apb.Finance
Imports Iaip.DAL.Finance

Public Class FinRefundView

    ' Properties

    Public Property OpenWithDepositId As Integer
        Get
            Return _openWithDepositID
        End Get
        Set
            _openWithDepositID = Value
            LoadDeposit()
        End Set
    End Property
    Private _openWithDepositID As Integer

    Public Property RefundID As Integer
        Get
            Return _refundID
        End Get
        Set(value As Integer)
            If value <> _refundID Then
                _refundID = value
                LoadRefund()
            End If
        End Set
    End Property
    Private _refundID As Integer = -1

    Private refund As Refund
    Private thisDeposit As Deposit

    Private amountErrorProvider As IaipErrorProvider

    ' Load

    Private Sub DepositView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearMessages()
        dtpRefundDate.Value = Today
    End Sub

    Private Sub ClearMessages()
        lblErrorMessage.Text = ""
        lblErrorMessage.BackColor = New Color()
        lblSaveMessage.Text = ""
        lblSaveMessage.BackColor = New Color()
    End Sub

    ''' <summary>
    ''' Only runs for existing refunds opened by ID (or by refresh button)
    ''' </summary>
    Private Sub LoadRefund()
        btnSaveNew.Visible = False

        refund = GetRefund(RefundID)

        If refund Is Nothing Then
            DisableForm("Refund ID does not exist.", ErrorLevel.Error)
            Exit Sub
        End If

        dtpRefundDate.Value = refund.RefundDate
        txtComments.Text = refund.Comment
        txtRefundAmount.Amount = refund.Amount

        If refund.Deleted Then
            DisableForm("Refund has been deleted.", ErrorLevel.Info)
        End If
    End Sub

    ''' <summary>
    ''' Only runs for new refunds opened with Deposit ID
    ''' </summary>
    Private Sub LoadDeposit()
        btnUpdate.Visible = False
        btnDelete.Visible = False
        btnRefresh.Visible = False

        thisDeposit = GetDeposit(OpenWithDepositId)

        If thisDeposit.DepositBalance <= 0 Then
            DisableForm("No balance to refund.", ErrorLevel.Info)
        End If

        txtRefundAmount.MaxValue = thisDeposit.DepositBalance
    End Sub

    Private Sub DisableForm(msg As String, errorLevel As ErrorLevel)
        DisableControls({btnSaveNew, btnUpdate, txtComments, txtRefundAmount, dtpRefundDate, btnDelete, btnRefresh})
        lblErrorMessage.ShowMessage(msg, errorLevel)
    End Sub

    ' Button events

    Private Sub btnSaveNew_Click(sender As Object, e As EventArgs) Handles btnSaveNew.Click
        If RefundID <> -1 Then
            DisableForm("Error.", ErrorLevel.Error)
            Exit Sub
        End If




    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If RefundID = -1 Then
            DisableForm("Refund ID does not exist.", ErrorLevel.Error)
            Exit Sub
        End If




    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim result As DeleteDepositResult = DeleteDeposit(RefundID)

        Select Case result
            Case DeleteDepositResult.AlreadyDeleted
                DisableForm("Refund has already been deleted.", ErrorLevel.Warning)
            Case DeleteDepositResult.DoesNotExist
                DisableForm("Refund does not exist.", ErrorLevel.Error)
            Case DeleteDepositResult.Success
                DisableForm("Refund successfully deleted.", ErrorLevel.Success)
            Case Else
                DisableForm("A database occurred.", ErrorLevel.Error)
        End Select
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If RefundID > -1 Then
            LoadRefund()
        End If
    End Sub

End Class