Imports Iaip.Apb.ApplicationFees
Imports Iaip.DAL.FacilityData
Imports Iaip.DAL.ApplicationFees
Imports Iaip.UrlHelpers

Public Class FinInvoiceView

#Region " Properties "

    Private _invoiceId As Integer
    Public Property InvoiceID As Integer
        Get
            Return _invoiceId
        End Get
        Set(value As Integer)
            If value <> _invoiceId Then
                _invoiceId = value
                LoadInvoice()
            End If
        End Set
    End Property

    Private Property thisInvoice As Invoice

#End Region

    Protected Overrides Sub OnLoad(e As EventArgs)
        ClearMessages()

        MyBase.OnLoad(e)
    End Sub

    Private Sub ClearMessages()
        lblSaveCommentMessage.Text = ""
        lblSaveCommentMessage.BackColor = New Color()
        lblVoidMessage.Text = ""
        lblVoidMessage.BackColor = New Color()
    End Sub

    Private Sub LoadInvoice()
        thisInvoice = GetInvoice(InvoiceID)

        If thisInvoice Is Nothing Then
            InvalidateForm()
            Return
        End If

        With thisInvoice
            thisInvoice.Facility = GetFacility(thisInvoice.FacilityID)

            Text = "Invoice " & .InvoiceID
            lblInvoiceID.Text = "Invoice " & .InvoiceID
            lblStatus.Text = "Status: "

            UrlToolTip.SetToolTip(btnViewInvoice, GetInvoiceUrl(thisInvoice.InvoiceGuid).ToString())

            If .Voided Then
                lblStatus.Text &= "VOID"
                If .VoidedDate.HasValue Then
                    lblStatus.Text &= " (voided on " & .VoidedDate.Value.ToString(DateFormat) & ")"
                End If
            Else
                lblStatus.Text &= .SettlementStatus

                If .CeasedCollections Then
                    lblStatus.Text &= " - ceased collections"
                End If
            End If

            txtComments.Text = .Comment
            lnkFacility.Text = .FacilityID.FormattedString
            lblFacilityDisplay.Text = .Facility.FacilityName.ToString()
            txtInvoiceDate.Text = .InvoiceDate.ToShortDateString
            txtDueDate.Text = .DueDate.ToShortDateString
            txtTotalDue.Amount = .TotalAmountDue
            txtAmountPaid.Amount = .PaymentsApplied
            txtCurrentBalance.Amount = .InvoiceBalance

            Select Case .InvoiceCategory
                Case InvoiceCategory.EmissionsFees
                    lblInvoiceDescription.Text = "Annual/Emissions Fees for " & .FeeYear.ToString
                Case InvoiceCategory.PermitApplicationFees
                    lblInvoiceDescription.Text = "Permit Application Fees for #" & .ApplicationID.ToString
                Case Else
                    lblInvoiceDescription.Text = ""
            End Select

            dgvInvoiceItems.DataSource = .InvoiceItems
            dgvInvoiceItems.SelectNone()

            dgvPaymentsApplied.DataSource = .DepositsApplied
            dgvPaymentsApplied.Columns("InvoiceID").Visible = False
            dgvPaymentsApplied.SelectNone()

            If .Voided Then
                btnVoid.Visible = False
                DisableControls({btnNewDeposit})
            Else
                btnVoid.Visible = (.DepositsApplied.Count = 0)
                btnNewDeposit.Enabled = (.InvoiceBalance <> 0)
            End If
        End With

        SetPermissions()
    End Sub

    Private Sub SetPermissions()
        If Not CurrentUser.HasPermission(UserCan.VoidUnpaidApplicationFeeInvoices) Then
            HideControls({btnNewDeposit, btnVoid})
        End If
    End Sub

    Private Sub InvalidateForm()
        Text = "Invoice " & InvoiceID.ToString
        lblInvoiceID.Text = "Invoice " & InvoiceID.ToString
        lblStatus.Text = "Invalid Invoice ID"
        lblFacilityDisplay.Text = ""
        lblInvoiceDescription.Text = ""
        UrlToolTip.RemoveAll()
        DisableControls({btnSaveComment, btnNewDeposit, btnVoid, btnViewInvoice})
    End Sub

    Private Sub txtSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        If SaveInvoiceComment(InvoiceID, txtComments.Text) Then
            lblSaveCommentMessage.ShowMessage("Comment saved.", ErrorLevel.Success)
        Else
            lblSaveCommentMessage.ShowMessage("Error saving comment.", ErrorLevel.Error)
        End If
    End Sub

    'Private Sub btnNewDeposit_Click(sender As Object, e As EventArgs) Handles btnNewDeposit.Click
    '    OpenNewDeposit(InvoiceID)
    'End Sub

    Private Sub btnVoid_Click(sender As Object, e As EventArgs) Handles btnVoid.Click
        If DialogResult.No = MessageBox.Show("Are you sure you want to void this invoice?", $"Void Invoice {InvoiceID}",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) Then
            Return
        End If

        lblVoidMessage.ClearMessage()
        btnVoid.Enabled = False

        Select Case VoidInvoice(InvoiceID)
            Case VoidInvoiceResult.AlreadyVoided
                lblVoidMessage.ShowMessage("Invoice has already been voided.", ErrorLevel.Error)
                lblStatus.Text = "Status: VOID"

            Case VoidInvoiceResult.DbError
                btnVoid.Enabled = True
                lblVoidMessage.ShowMessage("A database error has occurred.", ErrorLevel.Error)
                lblStatus.Text = "Status: ERROR"

            Case VoidInvoiceResult.DoesNotExist
                lblVoidMessage.ShowMessage("This invoice does not exist.", ErrorLevel.Error)
                lblStatus.Text = "Status: ERROR"

            Case VoidInvoiceResult.HasPayments
                lblVoidMessage.ShowMessage("Cannot void invoice because payments have been applied.", ErrorLevel.Error)

            Case VoidInvoiceResult.Success
                lblVoidMessage.ShowMessage("This invoice has successfully been voided.", ErrorLevel.Info)
                lblStatus.Text = "Status: VOID"

        End Select
    End Sub

    Private Sub lnkFacility_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkFacility.LinkClicked
        OpenFacilityAccount(thisInvoice.FacilityID)
    End Sub

    Private Sub btnViewInvoice_Click(sender As Object, e As EventArgs) Handles btnViewInvoice.Click
        OpenInvoiceUrl(thisInvoice.InvoiceGuid)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadInvoice()
    End Sub

    Private Sub dgvPaymentsApplied_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvPaymentsApplied.CellLinkActivated
        OpenDepositView(CInt(e.LinkValue))
    End Sub

    Private Sub txtComments_Enter(sender As Object, e As EventArgs) Handles txtComments.Enter
        AcceptButton = btnSaveComment
    End Sub

    Private Sub txtComments_Leave(sender As Object, e As EventArgs) Handles txtComments.Leave
        AcceptButton = Nothing
    End Sub

End Class