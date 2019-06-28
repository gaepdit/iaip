Imports Iaip.Apb.Finance
Imports Iaip.DAL
Imports Iaip.DAL.Finance

Public Class FinDepositView

    ' Public Properties 

    Private _depositID As Integer = -1
    Public Property DepositID As Integer
        Get
            Return _depositID
        End Get
        Set(value As Integer)
            If value <> _depositID Then
                _depositID = value

                If value > -1 Then
                    LoadDeposit()
                End If
            End If
        End Set
    End Property

    ' Local fields

    Private thisDeposit As Deposit = Nothing
    Private selectedInvoice As Invoice = Nothing
    Private originalSize As Size

    ' Load form

    Protected Overrides Sub OnLoad(e As EventArgs)
        SetUpAsNewDeposit()

        MyBase.OnLoad(e)

        originalSize = Size
        MinimumSize = New Size(271, 467)
        Size = New Size(271, 467)
    End Sub

    Private Sub SetUpAsNewDeposit()
        ClearMessages()

        dtpDepositDate.Value = Today
        dtpDepositDate.MaxDate = Today

        btnSaveNewDeposit.Visible = True

        grpApplyToInvoice.Visible = False
        grpRefunds.Visible = False
        grpInvoiceSearch.Visible = False
        grpSummary.Visible = False

        btnRefresh.Visible = False
        lblDepositDisplay.Location = New Point(12, 15)

        btnDeleteDeposit.Visible = False
        btnUpdateDepositDetails.Visible = False

        SetPermissions()
    End Sub

    Private Sub ClearMessages()
        lblApplyToInvoiceMessage.ClearMessage()
        lblSearchFacilityDisplay.ClearMessage()
        lblDetailsMessage.ClearMessage()
        lblDeleteDepositMessage.ClearMessage()
    End Sub

    ' Don't allow the form to close if no invoices have been added

    Private Sub FinDepositView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DepositID = -1 OrElse thisDeposit Is Nothing Then
            Exit Sub
        End If

        If thisDeposit.DepositsApplied Is Nothing OrElse thisDeposit.DepositsApplied.Count = 0 Then
            lblApplyToInvoiceMessage.ShowMessage("Payment must be applied to at least one invoice.", ErrorLevel.Warning)
            txtInvoiceToApply.Focus()
            e.Cancel = True
        End If
    End Sub

    ' Load deposit

    Private Sub LoadDeposit()
        MinimumSize = New Size(624, 653)
        Size = originalSize

        thisDeposit = GetDeposit(DepositID)

        If thisDeposit Is Nothing Then
            DisableEntireForm()
            Exit Sub
        End If

        btnRefresh.Visible = True
        lblDepositDisplay.Location = New Point(42, 15)

        btnUpdateDepositDetails.Visible = True
        btnSaveNewDeposit.Visible = False

        If AcceptButton Is btnSaveNewDeposit Then
            AcceptButton = btnUpdateDepositDetails
        End If

        grpApplyToInvoice.Visible = True
        grpRefunds.Visible = True
        ' grpInvoiceSearch.Visible = True
        grpSummary.Visible = True

        With thisDeposit
            Text = "Deposit ID " & .DepositID.ToString
            lblDepositDisplay.Text = "Deposit ID " & .DepositID.ToString

            ' Details
            dtpDepositDate.Value = .DepositDate
            txtDepositAmount.Amount = - .DepositAmount
            txtDepositNumber.Text = .DepositNumber
            txtBatchNumber.Text = .BatchNumber
            txtCheckNumber.Text = .CheckNumber
            txtCreditConf.Text = .CreditCardConf
            txtDepositComments.Text = .Comment
            txtDepositAmount.MinValue = .TotalAmountAllocated

            ' Summary 
            txtTotalDeposit.Amount = .DepositAmount
            txtAmountAppliedToInvoices.Amount = .TotalPaymentsApplied
            txtAmountRefunded.Amount = .TotalRefunded
            txtDepositBalance.Amount = .DepositBalance

            ' Search (not used)
            'txtAirsInvoiceSearch.AirsNumber = thisDeposit.FacilityID

            ' Invoices paid
            If .DepositsApplied.Count = 0 Then
                dgvInvoicesPaid.DataSource = Nothing
                dgvInvoicesPaid.Visible = False
                lblInvoicesPaid.Visible = False
            Else
                dgvInvoicesPaid.Visible = True
                lblInvoicesPaid.Visible = True

                dgvInvoicesPaid.DataSource = .DepositsApplied
                dgvInvoicesPaid.Columns("DepositID").Visible = False
                dgvInvoicesPaid.SelectNone()

                If CurrentUser.HasPermission(UserCan.EditFinancialData) Then
                    If dgvInvoicesPaid.Columns("RemoveInvoicePayment") Is Nothing Then
                        Dim removePaymentButtonColumn As New DataGridViewButtonColumn With {
                            .Name = "RemoveInvoicePayment",
                            .HeaderText = "",
                            .Text = "Remove",
                            .UseColumnTextForButtonValue = True
                        }

                        dgvInvoicesPaid.Columns.Add(removePaymentButtonColumn)
                    Else
                        dgvInvoicesPaid.Columns("RemoveInvoicePayment").DisplayIndex = dgvInvoicesPaid.Columns.Count() - 1
                    End If
                End If
            End If

            ' Refunds
            If .RefundsApplied.Count = 0 Then
                dgvRefunds.Visible = False
            Else
                dgvRefunds.Visible = True

                dgvRefunds.DataSource = .RefundsApplied
                dgvRefunds.Columns("DepositID").Visible = False
                dgvRefunds.SelectNone()
            End If

            ' Apply to Invoice section
            txtInvoiceToApply.Clear()
            selectedInvoice = Nothing
            txtAmountToApply.Amount = 0
            txtAmountToApply.MaxValue = - .DepositBalance

            If .DepositBalance >= 0 Then
                DisableApplyingToInvoice("")
            End If

            btnDeleteDeposit.Visible = (.TotalAmountAllocated = 0)
        End With

        SetPermissions()
    End Sub

    Private Sub SetPermissions()
        If Not CurrentUser.HasPermission(UserCan.EditFinancialData) Then
            HideControls({btnApplyToInvoice, btnDeleteDeposit, btnSaveNewDeposit, btnUpdateDepositDetails, txtInvoiceToApply, lblInvoiceToApply, txtAmountToApply, lblAmountToApply})
            lblInvoicesPaid.Location = lblInvoiceToApply.Location
            dgvInvoicesPaid.Size = New Size(dgvInvoicesPaid.Width, 311)
            dgvInvoicesPaid.Location = txtInvoiceToApply.Location
        End If
    End Sub

    Private Sub DisableEntireForm()
        Text = "Deposit ID " & DepositID & " not found."
        lblDepositDisplay.Text = "Deposit ID " & DepositID & " not found."

        DisableControls({grpDepositDetails, grpApplyToInvoice, grpRefunds, grpInvoiceSearch, grpSummary})

        btnDeleteDeposit.Visible = False

        btnRefresh.Visible = False
        lblDepositDisplay.Location = New Point(12, 15)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        ClearMessages()

        If DepositID > -1 Then
            LoadDeposit()
        End If
    End Sub

    ' Invoice search form

    'Private Sub btnInvoiceSearch_Click(sender As Object, e As EventArgs) Handles btnInvoiceSearch.Click
    '    DoInvoiceSearch()
    'End Sub

    'Private Sub DoInvoiceSearch()
    '    If Not txtAirsInvoiceSearch.HasError Then
    '        RemoveHandler dgvSearchResults.CellEnter, AddressOf dgvSearchResults_CellEnter

    '        dgvSearchResults.DataSource = SearchInvoices(txtAirsInvoiceSearch.AirsNumber, chkOnlyOpen.Checked)

    '        dgvSearchResults.Columns().Item("Voided").Visible = False
    '        dgvSearchResults.Columns().Item("Facility ID").Visible = False
    '        dgvSearchResults.Columns().Item("Facility Name").Visible = False
    '        dgvSearchResults.Columns().Item("Type").Visible = False
    '        dgvSearchResults.Columns().Item("Ceased Collections").Visible = False

    '        dgvSearchResults.SelectNone()

    '        AddHandler dgvSearchResults.CellEnter, AddressOf dgvSearchResults_CellEnter
    '    End If
    'End Sub

    'Private Sub txtAirsInvoiceSearch_AirsNumberChanged(sender As Object, e As EventArgs) Handles txtAirsInvoiceSearch.AirsNumberChanged
    '    btnInvoiceSearch.Enabled = Not txtAirsInvoiceSearch.HasError

    '    If txtAirsInvoiceSearch.AirsNumber IsNot Nothing Then
    '        lblSearchFacilityDisplay.ShowMessage(GetFacilityName(txtAirsInvoiceSearch.AirsNumber), ErrorLevel.None)
    '    End If
    'End Sub

    'Private Sub dgvSearchResults_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSearchResults.CellEnter
    '    If e.RowIndex <> -1 AndAlso
    '        e.RowIndex < dgvSearchResults.RowCount AndAlso
    '        dgvSearchResults(0, e.RowIndex).Value IsNot Nothing Then

    '        txtInvoiceToApply.Text = dgvSearchResults(0, e.RowIndex).Value.ToString
    '        SelectInvoice()

    '    End If
    'End Sub

    'Private Sub dgvSearchResults_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvSearchResults.CellFormatting
    '    If e IsNot Nothing AndAlso
    '        e.Value IsNot Nothing AndAlso
    '        Not IsDBNull(e.Value) AndAlso
    '        dgvSearchResults.Columns(e.ColumnIndex).Name = "Facility ID" Then

    '        e.Value = New ApbFacilityId(e.Value.ToString).FormattedString

    '    End If
    'End Sub

    ' Apply to invoice

    Private Sub SelectInvoice()
        lblApplyToInvoiceMessage.ClearMessage()
        txtAmountToApply.Amount = 0

        If String.IsNullOrEmpty(txtInvoiceToApply.Text) Then
            Exit Sub
        End If

        Dim invoiceId As Integer

        If Not Integer.TryParse(txtInvoiceToApply.Text, invoiceId) Then
            DisableApplyingToInvoice("Invoice ID is not valid.")
        End If

        If dgvInvoicesPaid IsNot Nothing AndAlso dgvInvoicesPaid.DataSource IsNot Nothing AndAlso
            dgvInvoicesPaid.Rows.Count > 0 AndAlso dgvInvoicesPaid.Columns("InvoiceID").ContainsValue(invoiceId) Then
            DisableApplyingToInvoice("Invoice already selected. To update amount, first remove existing record below.")
            Exit Sub
        End If

        selectedInvoice = GetInvoice(invoiceId)

        If selectedInvoice Is Nothing Then
            DisableApplyingToInvoice("Invoice ID does not exist.")
        ElseIf selectedInvoice.Voided Then
            DisableApplyingToInvoice("Invoice is voided.")
        Else
            LoadSelectedInvoice()
        End If
    End Sub

    Private Sub LoadSelectedInvoice()
        If DepositID = -1 OrElse thisDeposit Is Nothing Then
            Exit Sub
        End If

        lblApplyToInvoiceMessage.ShowMessage(String.Format("Invoice {0} current balance: {1:c}", selectedInvoice.InvoiceID, selectedInvoice.InvoiceBalance), ErrorLevel.None)

        Dim existingPayment As DepositApplied = selectedInvoice.DepositsApplied.Find(Function(x) x.DepositID = DepositID)

        If existingPayment Is Nothing Then
            If thisDeposit.DepositBalance = 0 Then
                DisableApplyingToInvoice("No deposit balance remaining.")
                Exit Sub
            End If

            If selectedInvoice.InvoiceBalance = 0 Then
                DisableApplyingToInvoice(String.Format("Invoice {0} has zero balance.", selectedInvoice.InvoiceID))
                Exit Sub
            End If

            btnApplyToInvoice.Enabled = True

            txtAmountToApply.Amount = Math.Min(selectedInvoice.InvoiceBalance, -thisDeposit.DepositBalance)
            txtAmountToApply.MaxValue = Math.Min(selectedInvoice.InvoiceBalance, -thisDeposit.DepositBalance)
        Else
            grpApplyToInvoice.Enabled = True
            btnApplyToInvoice.Enabled = False

            txtAmountToApply.Amount = existingPayment.AmountApplied
            txtAmountToApply.MaxValue = existingPayment.AmountApplied + Math.Min(selectedInvoice.InvoiceBalance, -thisDeposit.DepositBalance)
        End If
    End Sub

    Private Sub DisableApplyingToInvoice(msg As String)
        If String.IsNullOrEmpty(msg) Then
            lblApplyToInvoiceMessage.ClearMessage
        Else
            lblApplyToInvoiceMessage.ShowMessage(msg, ErrorLevel.Warning)
        End If

        txtAmountToApply.MaxValue = Integer.MaxValue

        btnApplyToInvoice.Enabled = False
    End Sub

    Private Sub txtInvoiceToApply_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtInvoiceToApply.Validating
        SelectInvoice()
    End Sub

    Private Sub txtAmountToApply_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtAmountToApply.Validating
        btnApplyToInvoice.Enabled = False

        Select Case txtAmountToApply.ValidationStatus
            Case CurrencyTextBox.CurrencyValidationStatus.InvalidFormat
                lblApplyToInvoiceMessage.ShowMessage("Payment amount is invalid.", ErrorLevel.Warning)
            Case CurrencyTextBox.CurrencyValidationStatus.AboveMaximum
                lblApplyToInvoiceMessage.ShowMessage(String.Format("Payment amount cannot be more than {0:c}.", txtAmountToApply.MaxValue), ErrorLevel.Warning)
            Case CurrencyTextBox.CurrencyValidationStatus.BelowMinimum
                lblApplyToInvoiceMessage.ShowMessage(String.Format("Payment amount cannot be less than {0:c}.", txtAmountToApply.MinValue), ErrorLevel.Warning)
            Case CurrencyTextBox.CurrencyValidationStatus.Valid
                lblApplyToInvoiceMessage.ClearMessage()
                btnApplyToInvoice.Enabled = (selectedInvoice IsNot Nothing)
        End Select
    End Sub

    Private Sub btnApplyToInvoice_Click(sender As Object, e As EventArgs) Handles btnApplyToInvoice.Click
        ApplyNewPaymentToInvoice()
    End Sub

    Private Sub ApplyNewPaymentToInvoice()
        ' Attempts to add a new payment
        ClearMessages()

        If txtAmountToApply.Amount = 0 Then
            lblApplyToInvoiceMessage.ShowMessage("New payment amount cannot be 0.", ErrorLevel.Warning)
            Exit Sub
        End If

        If selectedInvoice Is Nothing Then
            lblApplyToInvoiceMessage.ShowMessage("Enter a valid invoice ID.", ErrorLevel.Warning)
            Exit Sub
        End If

        lblApplyToInvoiceMessage.ClearMessage()

        Dim invoiceId As Integer = selectedInvoice.InvoiceID
        Dim newDepositAppliedId As Integer

        Select Case ApplyPaymentToInvoice(invoiceId, txtAmountToApply.Amount, thisDeposit.DepositID, newDepositAppliedId)
            Case ApplyPaymentToInvoiceResult.Success
                LoadDeposit()
                lblApplyToInvoiceMessage.ShowMessage("Payment applied.", ErrorLevel.Success)

            Case ApplyPaymentToInvoiceResult.DepositDoesNotExist, ApplyPaymentToInvoiceResult.DepositDeleted
                lblApplyToInvoiceMessage.ShowMessage("Error: Deposit not found.", ErrorLevel.Error)
                DisableEntireForm()

            Case ApplyPaymentToInvoiceResult.InvoiceDoesNotExist, ApplyPaymentToInvoiceResult.InvoiceVoided
                DisableApplyingToInvoice("Error: Invoice ID not found.")

            Case ApplyPaymentToInvoiceResult.DepositBalanceInsufficient, ApplyPaymentToInvoiceResult.InvoiceBalanceExceeded
                LoadDeposit()
                txtInvoiceToApply.Text = invoiceId.ToString()
                SelectInvoice()
                lblApplyToInvoiceMessage.ShowMessage(String.Format("Payment amount cannot be more than {0}.", txtAmountToApply.MaxValue), ErrorLevel.Warning)

            Case Else
                lblApplyToInvoiceMessage.ShowMessage("An unknown error occurred. Please refresh page and try again.", ErrorLevel.Error)

        End Select
    End Sub

    ' Invoices paid grid events

    Private Sub dgvInvoicesPaid_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvInvoicesPaid.CellLinkActivated
        OpenInvoiceView(CInt(e.LinkValue))
    End Sub

    Private Sub dgvInvoicesPaid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoicesPaid.CellClick
        If CurrentUser.HasPermission(UserCan.EditFinancialData) AndAlso
            e.RowIndex <> -1 AndAlso e.RowIndex < dgvInvoicesPaid.RowCount AndAlso
            e.ColumnIndex = dgvInvoicesPaid.Columns("RemoveInvoicePayment").Index Then

            RemovePaymentFromInvoice(CInt(dgvInvoicesPaid("InvoiceID", e.RowIndex).Value))
        End If
    End Sub

    Private Sub RemovePaymentFromInvoice(invoiceID As Integer)
        ' Attempts to delete a payment
        ClearMessages()

        lblApplyToInvoiceMessage.ClearMessage()

        Select Case ApplyPaymentToInvoice(invoiceID, 0, thisDeposit.DepositID)
            Case ApplyPaymentToInvoiceResult.Success
                LoadDeposit()
                lblApplyToInvoiceMessage.ShowMessage("Payment removed.", ErrorLevel.Success)
                txtInvoiceToApply.Text = invoiceID.ToString
                SelectInvoice()

            Case ApplyPaymentToInvoiceResult.DepositDoesNotExist, ApplyPaymentToInvoiceResult.DepositDeleted
                lblApplyToInvoiceMessage.ShowMessage("Error: Deposit not found.", ErrorLevel.Error)
                DisableEntireForm()

            Case ApplyPaymentToInvoiceResult.InvoiceDoesNotExist, ApplyPaymentToInvoiceResult.InvoiceVoided
                DisableApplyingToInvoice("Error: Invoice ID not found.")

            Case Else
                lblApplyToInvoiceMessage.ShowMessage("An unknown error occurred. Please refresh page and try again.", ErrorLevel.Error)

        End Select
    End Sub

    ' New deposit

    Private Sub btnSaveNewDeposit_Click(sender As Object, e As EventArgs) Handles btnSaveNewDeposit.Click
        ClearMessages()

        If Not ValidateDepositDetails() Then
            Exit Sub
        End If

        Dim deposit As New Deposit() With {
            .DepositDate = dtpDepositDate.Value,
            .DepositAmount = -txtDepositAmount.Amount,
            .DepositNumber = txtDepositNumber.Text,
            .BatchNumber = txtBatchNumber.Text,
            .CheckNumber = txtCheckNumber.Text,
            .CreditCardConf = txtCreditConf.Text,
            .Comment = txtDepositComments.Text
        }

        Dim newDepositId As Integer

        Select Case SaveNewDeposit(deposit, newDepositId)
            Case DbResult.Success
                If newDepositId = -1 Then
                    lblDetailsMessage.ShowMessage("An unknown error occurred. Please contact EPD-IT.", ErrorLevel.Error)
                Else
                    lblDetailsMessage.ShowMessage("New deposit successfully saved.", ErrorLevel.Success)
                    DepositID = newDepositId
                End If

            Case DbResult.DbError
                lblDetailsMessage.ShowMessage("An unknown error occurred. Please refresh page and try again.", ErrorLevel.Error)

        End Select
    End Sub

    Private Function ValidateDepositDetails() As Boolean
        If Not txtDepositAmount.IsValid OrElse CInt(txtDepositAmount.Text) <= 0 Then
            lblDetailsMessage.ShowMessage("Deposit amount is not valid. Nothing saved.", ErrorLevel.Warning)
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtDepositNumber.Text) AndAlso
            String.IsNullOrWhiteSpace(txtBatchNumber.Text) AndAlso
            String.IsNullOrWhiteSpace(txtCheckNumber.Text) AndAlso
            String.IsNullOrWhiteSpace(txtCreditConf.Text) Then
            lblDetailsMessage.ShowMessage("Nothing saved. Either Deposit Number, Batch, Check Number, or Credit Card Confirmation Number must be entered.", ErrorLevel.Warning)
            Return False
        End If

        Return True
    End Function

    ' Update deposit

    Private Sub txtDepositAmount_ValidationStatusChanged(sender As Object, e As EventArgs) Handles txtDepositAmount.ValidationStatusChanged
        Select Case txtDepositAmount.ValidationStatus
            Case CurrencyTextBox.CurrencyValidationStatus.BelowMinimum
                lblDetailsMessage.ShowMessage(String.Format("Deposit amount must be above {0}.", txtDepositAmount.MinValue), ErrorLevel.Warning)
                btnSaveNewDeposit.Enabled = False
                btnUpdateDepositDetails.Enabled = False

            Case CurrencyTextBox.CurrencyValidationStatus.InvalidFormat
                lblDetailsMessage.ShowMessage("Deposit amount must be a number.", ErrorLevel.Warning)
                btnSaveNewDeposit.Enabled = False
                btnUpdateDepositDetails.Enabled = False

            Case Else
                lblDetailsMessage.ClearMessage()
                btnSaveNewDeposit.Enabled = True
                btnUpdateDepositDetails.Enabled = True
        End Select
    End Sub

    Private Sub btnUpdateDeposit_Click(sender As Object, e As EventArgs) Handles btnUpdateDepositDetails.Click
        ClearMessages()

        If Not ValidateDepositDetails() Then
            Exit Sub
        End If

        Dim deposit As New Deposit() With {
            .DepositID = DepositID,
            .DepositDate = dtpDepositDate.Value,
            .DepositAmount = -txtDepositAmount.Amount,
            .DepositNumber = txtDepositNumber.Text,
            .BatchNumber = txtBatchNumber.Text,
            .CheckNumber = txtCheckNumber.Text,
            .CreditCardConf = txtCreditConf.Text,
            .Comment = txtDepositComments.Text
        }

        Select Case UpdateDeposit(deposit)
            Case UpdateDepositResult.Success
                LoadDeposit()
                lblDetailsMessage.ShowMessage("Deposit details updated.", ErrorLevel.Success)

            Case UpdateDepositResult.DepositDeleted, UpdateDepositResult.DoesNotExist
                lblDetailsMessage.ShowMessage("Deposit not found.", ErrorLevel.Error)
                DisableEntireForm()

            Case UpdateDepositResult.AmountBelowMinimum
                LoadDeposit()
                lblDetailsMessage.ShowMessage(String.Format("Deposit amount must be above {0}.", thisDeposit.TotalAmountAllocated), ErrorLevel.Warning)
                txtDepositAmount.Focus()

            Case Else
                lblDetailsMessage.ShowMessage("An unknown error occurred. Please refresh page and try again.", ErrorLevel.Error)

        End Select
    End Sub

    ' Delete deposit

    Private Sub btnDeleteDeposit_Click(sender As Object, e As EventArgs) Handles btnDeleteDeposit.Click
        ClearMessages()

        If DepositID = -1 OrElse thisDeposit Is Nothing Then
            btnDeleteDeposit.Visible = False
            lblDeleteDepositMessage.ShowMessage("No deposit loaded", ErrorLevel.Warning)
            Exit Sub
        End If

        Dim response As DialogResult = MessageBox.Show(String.Format("Are you sure you want to delete Deposit ID {0}?", DepositID), "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If response = DialogResult.No Then
            Exit Sub
        End If

        Select Case DeleteDeposit(DepositID)
            Case DeleteDepositResult.Success
                DisableEntireForm()
                Text = "Deposit ID " & DepositID & " deleted."
                lblDepositDisplay.Text = "Deposit ID " & DepositID & " deleted."
                lblDeleteDepositMessage.ShowMessage("Deposit successfully deleted.", ErrorLevel.Success)

            Case DeleteDepositResult.AlreadyDeleted, DeleteDepositResult.DoesNotExist
                DisableEntireForm()
                btnDeleteDeposit.Visible = False
                lblDeleteDepositMessage.ShowMessage("Deposit not found.", ErrorLevel.Error)

            Case DeleteDepositResult.PaymentsApplied
                btnDeleteDeposit.Visible = False
                lblDeleteDepositMessage.ShowMessage("Deposit cannot be deleted because it has been applied to invoices.", ErrorLevel.Warning)

            Case Else
                btnDeleteDeposit.Visible = False
                lblDeleteDepositMessage.ShowMessage("An unknown error occurred. Please refresh page and try again.", ErrorLevel.Error)

        End Select
    End Sub

    ' Refunds DataGridView events

    Private Sub dgvRefunds_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvRefunds.CellLinkActivated
        OpenRefundView(CInt(e.LinkValue))
    End Sub

    ' Accept Button

    Private Sub DetailsFormAcceptButtonSet(sender As Object, e As EventArgs) _
        Handles dtpDepositDate.Enter, txtDepositAmount.Enter, txtDepositNumber.Enter, txtBatchNumber.Enter,
        txtCheckNumber.Enter, txtCreditConf.Enter, txtDepositComments.Enter

        If btnUpdateDepositDetails.Visible Then
            AcceptButton = btnUpdateDepositDetails
        Else
            AcceptButton = btnSaveNewDeposit
        End If
    End Sub

    Private Sub DetailsFormAcceptButtonUnset(sender As Object, e As EventArgs) _
        Handles dtpDepositDate.Leave, txtDepositAmount.Leave, txtDepositNumber.Leave, txtBatchNumber.Leave,
        txtCheckNumber.Leave, txtCreditConf.Leave, txtDepositComments.Leave

        AcceptButton = Nothing
    End Sub

    Private Sub SearchFormAcceptButtonSet(sender As Object, e As EventArgs) Handles txtAirsInvoiceSearch.Enter, chkOnlyOpen.Enter
        AcceptButton = btnInvoiceSearch
    End Sub

    Private Sub SearchFormAcceptButtonUnset(sender As Object, e As EventArgs) Handles txtAirsInvoiceSearch.Leave, chkOnlyOpen.Leave
        AcceptButton = Nothing
    End Sub

    Private Sub ApplyFormAcceptButtonSet(sender As Object, e As EventArgs) Handles txtInvoiceToApply.Enter, txtAmountToApply.Enter
        AcceptButton = btnApplyToInvoice
    End Sub

    Private Sub ApplyFormAcceptButtonUnset(sender As Object, e As EventArgs) Handles txtInvoiceToApply.Leave, txtAmountToApply.Leave
        AcceptButton = Nothing
    End Sub

End Class