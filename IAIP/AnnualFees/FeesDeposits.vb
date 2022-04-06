Imports System.Data.SqlClient

Public Class FeesDeposits
    Dim dtInvoice As DataTable
    Dim dtDeposit As DataTable
    Dim mousing As Boolean

#Region "Page Load Functions"

    Private Sub PASPDepositsAmendments_Load(sender As Object, e As EventArgs) Handles Me.Load
        dtpDepositReportStartDate.Value = Today.AddMonths(-1)
        dtpDepositReportEndDate.Value = Today
        cbYear.DataSource = DAL.GetAllFeeYears()
        cbYear2.DataSource = DAL.GetAllFeeYears()
    End Sub

#End Region

#Region "Fee Deposits"

    Private Sub LoadFacilityData(AIRSNumber As String)

        If DAL.AirsNumberExists(mtbAIRSNumber.Text) Then
            Dim query As String = "Select " &
                       "strFacilityName " &
                       "from APBFacilityInformation " &
                       "where strAIRSNumber = @AIRSNumber "
            Dim param As New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(AIRSNumber).DbFormattedString)

            Dim facName As String = DB.GetString(query, param)

            If String.IsNullOrWhiteSpace(facName) Then
                lblFacilityName.Text = "Facility Name"
            Else
                lblFacilityName.Text = "Facility Name: " & facName
            End If
        Else
            MsgBox("Cannot find facility AIRS number.", MsgBoxStyle.Exclamation, "Invalid AIRS number")
        End If

    End Sub

    Private Function ValidateData() As Boolean
        Dim query As String
        Dim param As SqlParameter()

        If Not DAL.AirsNumberExists(mtbAIRSNumber.Text) Then
            MsgBox("This AIRS # is not valid; please verify that it is entered correctly." & vbCrLf &
                       "If you get this message in error, contact the Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
            Return False
        End If

        If cbYear2.Text = "" Then
            MsgBox("Please select a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
            Return False
        End If

        If txtDepositAmount.Text = "" Then
            MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
            Return False
        End If

        If Not IsNumeric(Replace(Replace(txtDepositAmount.Text, ",", ""), "$", "")) Then
            MsgBox("Please enter a valid Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
            Return False
        End If

        If txtBatchNoField.Text = "" Then
            MsgBox("Please enter a Batch Number", MsgBoxStyle.OkOnly, "Incorrect Batch No.")
            Return False
        End If

        If txtCheckNumberField.Text = "" Then
            MsgBox("Please enter a Check Number", MsgBoxStyle.OkOnly, "Incorrect Check No.")
            Return False
        End If

        If txtInvoiceForDeposit.Text = "" Then
            MsgBox("Please select an Invoice Number", MsgBoxStyle.OkOnly, "No Invoice No.")
            Return False
        Else
            query = "Select InvoiceID from FS_FeeInvoice " &
                "where invoiceID = @InvoiceID " &
                "and strAIRSNumber = @AIRSNumber "
            param = {
                New SqlParameter("@InvoiceID", txtInvoiceForDeposit.Text),
                New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString)
            }
            If Not DB.ValueExists(query, param) Then
                MsgBox("Please select a valid Invoice Number", MsgBoxStyle.OkOnly, "No Valid Invoice No.")
                Return False
            End If
        End If

        Return True

    End Function

    Private Function DepositSearch() As Boolean
        Try
            Dim query As String = "SELECT SUBSTRing(tr.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, " &
            "  tr.STRDEPOSITNO, tr.STRBATCHNO, tr.TRANSACTIONID, " &
            "  tr.DATTRANSACTIONDATE, tr.NUMPAYMENT, tr.NUMFEEYEAR, " &
            "  tr.STRCHECKNO, tr.STRCREDITCARDNO, tr.INVOICEID, " &
            "  lpt.STRPAYTYPEDESC AS strPayType, tr.STRCOMMENT " &
            "FROM FS_Transactions tr " &
            "INNER JOIN FS_FeeInvoice fi ON tr.INVOICEID = " &
            "  fi.INVOICEID " &
            "INNER JOIN FSLK_PayType lpt ON lpt.NUMPAYTYPEID = " &
            "  fi.STRPAYTYPE " &
            "WHERE tr.DATTRANSACTIONDATE BETWEEN @StartDate AND @EndDate AND " &
            "  tr.ACTIVE = '1' AND fi.ACTIVE = '1' " &
            "ORDER BY tr.STRBATCHNO"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@StartDate", dtpDepositReportStartDate.Value),
                New SqlParameter("@EndDate", dtpDepositReportEndDate.Value)
            }

            dtDeposit = DB.GetDataTable(query, parameters)

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function ViewInvoices() As Boolean

        Try
            Dim query As String
            Dim param As SqlParameter()

            If txtCheckNumber.Text <> "" Then
                query = "select " &
                    "substring(FS_FeeInvoice.strAIRSnumber, 5, 8) as strAIRSNumber, " &
                    "strDepositNo, datTransactionDate, " &
                    "numPayment, FS_FeeInvoice.numFeeYear, " &
                    "strCheckNo, strBatchNo, " &
                    "Description, TransactionID, " &
                    "FS_Transactions.strComment, FS_FeeInvoice.InvoiceID, " &
                    "case " &
                    "when FS_Transactions.transactionTypeCode = '1' then numAmount " &
                    "when FS_Transactions.TransactionTypeCode = '2' then numAmount/4 " &
                    "else numAmount " &
                    "end FeeDue " &
                    "from FS_FeeInvoice " &
                    "left join FS_Transactions " &
                    "on FS_FeeInvoice.InvoiceID = FS_Transactions.INvoiceID " &
                    "left join FSLK_TransactionType " &
                    "on FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode " &
                    "where strCheckNo like @chknum  " &
                    "and FS_Transactions.Active = '1' " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "order by strBatchNo  "
                param = {New SqlParameter("@chknum", "%" & txtCheckNumber.Text & "%")}
            Else
                query = "select " &
                    "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " &
                    "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " &
                    " strComment,  ALLInvoices.InvoiceID,  " &
                    "FeeDue  " &
                    "from  " &
                    "(select substring(FS_FeeINvoice.strAIRSnumber, 5, 8) as strAIRSNumber, " &
                    "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                    "from  FS_FeeInvoice " &
                    "where FS_FeeInvoice.strAIRSnumber like @airsnum " &
                    "and FS_FeeInvoice.numFeeYear = @feeyear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "union " &
                    "select distinct substring(FS_FeeINvoice.strAIRSnumber, 5, 8) as strAIRSNumber, " &
                    "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                    "from FS_FeeInvoice " &
                    "left join FS_Transactions " &
                    "on FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID " &
                    "left join FSLK_TransactionType  " &
                    "on FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode " &
                    "where FS_FeeInvoice.strAIRSnumber like @airsnum " &
                    "and FS_FeeInvoice.numFeeYear = @feeyear and FS_FeeInvoice.Active = '1' " &
                    "and FS_Transactions.Active = '1'  ) ALLInvoices " &
                    "left join (select distinct substring(FS_FeeINvoice.strAIRSnumber, 5, 8) as strAIRSNumber, strDepositNo, datTransactionDate, " &
                    "numPayment, FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " &
                    "FS_Transactions.strComment, FS_FeeINvoice.InvoiceID, " &
                    "case when FS_Transactions.transactionTypeCode = '1' then numAmount " &
                    "when FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " &
                    "from FS_FeeInvoice " &
                    "left join FS_Transactions " &
                    "on FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID " &
                    "left join FSLK_TransactionType " &
                    "on FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode " &
                    "where FS_FeeInvoice.strAIRSnumber = @airsnum " &
                    "and FS_FeeInvoice.numFeeYear = @feeyear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Transactions.Active = '1' ) Transactions " &
                    "on Allinvoices.InvoiceID = Transactions.InvoiceID " &
                    "order by strBatchNo "
                param = {
                    New SqlParameter("@airsnum", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString),
                    New SqlParameter("@feeyear", cbYear.Text)
                }

            End If

            dtInvoice = DB.GetDataTable(query, param)

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub btnSearchDeposits_Click(sender As Object, e As EventArgs) Handles btnSearchDeposits.Click
        If Not DepositSearch() Then
            MsgBox("There was an error in your search. Check the start and end dates for your search.", MsgBoxStyle.Exclamation, "Deposit Search Error")
        Else
            If Not LoadDepositsGridview() Then
                MsgBox("There was an error filling the deposits grid.", MsgBoxStyle.Exclamation, "Deposit Search Error")
            End If
        End If
    End Sub

    Private Sub btnViewInvoices_Click(sender As Object, e As EventArgs) Handles btnViewInvoices.Click
        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cbYear2.SelectedIndex = 0
            txtDepositAmount.Clear()
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            dtpBatchDepositDateField.Text = Date.Today
            txtCheckNumber.Clear()

            If DAL.AirsNumberExists(mtbAIRSNumber.Text) Then
                If cbYear.Text <> "" Then
                    If Not ViewInvoices() Then
                        MsgBox("There was an error loading invoices.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                    Else
                        If Not LoadInvoicesGridview() Then
                            MsgBox("There was an error filling the invoices grid.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                        End If
                    End If
                Else
                    MsgBox("Select a year to check for invoices.", MsgBoxStyle.Information, "PASP Deposit Amendments")
                End If
            Else
                MsgBox("Please enter a valid AIRS # to check for invoices.", MsgBoxStyle.Information, "PASP Deposit Amendments")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvDeposits_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvDeposits.MouseDown
        mousing = True
    End Sub

    Private Sub dgvDeposits_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvDeposits.MouseUp
        dgvDepositsShowSelection()
        mousing = False
    End Sub

    Private Sub dgvDeposits_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDeposits.SelectionChanged
        If Not mousing Then
            dgvDepositsShowSelection()
        End If
    End Sub

    Private Sub dgvDepositsShowSelection()
        Try
            If dgvDeposits.SelectedRows.Count = 1 Then
                Dim row As DataGridViewRow = dgvDeposits.CurrentRow

                If dgvDeposits.Columns(0).HeaderText = "AIRS Number" Then
                    mtbAIRSNumber.Text = row.Cells(0).Value
                    LoadFacilityData(row.Cells(0).Value)
                    lblAIRSNumber.Text = "AIRS #: " & row.Cells(0).Value

                    If Not IsDBNull(row.Cells(3).Value) Then
                        txtTransactionID.Text = row.Cells(3).Value
                    End If

                    If IsDBNull(row.Cells(2).Value) Then
                        txtBatchNoField.Clear()
                    Else
                        txtBatchNoField.Text = row.Cells(2).Value
                    End If

                    If IsDBNull(row.Cells(4).Value) Then
                        dtpBatchDepositDateField.Text = Date.Today
                    Else
                        dtpBatchDepositDateField.Text = row.Cells(4).FormattedValue
                    End If

                    If IsDBNull(row.Cells(5).Value) Then
                        txtDepositAmount.Clear()
                    Else
                        txtDepositAmount.Text = row.Cells(5).Value
                    End If

                    If IsDBNull(row.Cells(6).Value) Then
                        cbYear.SelectedIndex = 0
                        cbYear2.SelectedIndex = 0
                    Else
                        cbYear.Text = row.Cells(6).Value.ToString
                        cbYear2.Text = row.Cells(6).Value.ToString
                    End If

                    If IsDBNull(row.Cells(11).Value) Then
                        txtDepositComments.Clear()
                    Else
                        txtDepositComments.Text = row.Cells(11).Value
                    End If

                    If IsDBNull(row.Cells(9).Value) Then
                        txtSearchInvoice.Text = ""
                        txtInvoiceForDeposit.Clear()
                    Else
                        txtSearchInvoice.Text = row.Cells(9).Value
                        txtInvoiceForDeposit.Text = row.Cells(9).Value
                    End If

                    If IsDBNull(row.Cells(7).Value) Then
                        txtCheckNumber.Clear()
                    Else
                        txtCheckNumber.Text = row.Cells(7).Value
                    End If

                    If IsDBNull(row.Cells(7).Value) Then
                        txtCheckNumberField.Clear()
                        If txtCheckNumber.Text <> "" Then
                            txtCheckNumberField.Text = txtCheckNumber.Text
                        End If
                    Else
                        txtCheckNumberField.Text = row.Cells(7).Value
                    End If

                    If IsDBNull(row.Cells(1).Value) Then
                        txtDepositNumberField.Clear()
                    Else
                        txtDepositNumberField.Text = row.Cells(1).Value
                    End If

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvInvoices_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvInvoices.MouseDown
        mousing = True
    End Sub

    Private Sub dgvInvoices_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvInvoices.MouseUp
        dgvInvoicesShowSelection()
        mousing = False
    End Sub

    Private Sub dgvInvoices_SelectionChanged(sender As Object, e As EventArgs) Handles dgvInvoices.SelectionChanged
        If Not mousing Then
            dgvInvoicesShowSelection()
        End If
    End Sub

    Private Sub dgvInvoicesShowSelection()
        Try
            If dgvInvoices.SelectedRows.Count = 1 Then
                Dim row As DataGridViewRow = dgvInvoices.CurrentRow

                If dgvInvoices.Columns(0).HeaderText = "AIRS Number" Then
                    mtbAIRSNumber.Text = row.Cells(0).Value
                    LoadFacilityData(row.Cells(0).Value)
                    lblAIRSNumber.Text = "AIRS #: " & row.Cells(0).Value

                    If IsDBNull(row.Cells(8).Value) Then
                        txtTransactionID.Text = ""
                    Else
                        txtTransactionID.Text = row.Cells(8).Value
                    End If

                    If IsDBNull(row.Cells(1).Value) Then
                        txtDepositNumberField.Clear()
                    Else
                        txtDepositNumberField.Text = row.Cells(1).Value
                    End If

                    If IsDBNull(row.Cells(11).Value) Then
                        If IsDBNull(row.Cells(3).Value) Then
                            txtDepositAmount.Clear()
                        Else
                            txtDepositAmount.Text = row.Cells(3).Value
                        End If
                    Else
                        If IsDBNull(row.Cells(3).Value) Then
                            txtDepositAmount.Text = row.Cells(11).Value
                        Else
                            If row.Cells(3).Value = 0 Then
                                txtDepositAmount.Text = row.Cells(11).Value
                            Else
                                txtDepositAmount.Text = row.Cells(3).Value
                            End If
                        End If
                    End If

                    If IsDBNull(row.Cells(4).Value) Then
                        cbYear.SelectedIndex = 0
                        cbYear2.SelectedIndex = 0
                    Else
                        cbYear.Text = row.Cells(4).Value.ToString
                        cbYear2.Text = row.Cells(4).Value.ToString
                    End If

                    If IsDBNull(row.Cells(5).Value) Then
                        txtCheckNumberField.Clear()
                        If txtCheckNumber.Text <> "" Then
                            txtCheckNumberField.Text = txtCheckNumber.Text
                        End If
                    Else
                        txtCheckNumberField.Text = row.Cells(5).Value
                    End If

                    If IsDBNull(row.Cells(6).Value) Then
                        txtBatchNoField.Clear()
                    Else
                        txtBatchNoField.Text = row.Cells(6).Value
                    End If

                    If IsDBNull(row.Cells(9).Value) Then
                        txtDepositComments.Clear()
                    Else
                        txtDepositComments.Text = row.Cells(9).Value
                    End If

                    If IsDBNull(row.Cells(10).Value) Then
                        txtInvoiceForDeposit.Clear()
                    Else
                        txtInvoiceForDeposit.Text = row.Cells(10).Value
                    End If

                    If IsDBNull(row.Cells(2).Value) Then
                        dtpBatchDepositDateField.Text = Date.Today
                    Else
                        dtpBatchDepositDateField.Text = row.Cells(2).Value
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewCheckDeposit_Click(sender As Object, e As EventArgs) Handles btnAddNewCheckDeposit.Click
        Try
            If ValidateData() Then
                If txtTransactionID.Text = "" Then
                    Dim query As String = "INSERT " &
                        "INTO FS_TRANSACTIONS " &
                        "  ( " &
                        "    TRANSACTIONID, INVOICEID, TRANSACTIONTYPECODE, " &
                        "    DATTRANSACTIONDATE, NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, " &
                        "    STRBATCHNO, STRENTRYPERSON, STRCOMMENT, ACTIVE, UPDATEUSER, " &
                        "    UPDATEDATETIME, CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, " &
                        "    STRCREDITCARDNO " &
                        "  ) " &
                        "  VALUES " &
                        "  ( " &
                        "    NEXT VALUE FOR SEQ_FS_TRANSACTIONS, @INVOICEID, " &
                        "    @TRANSACTIONTYPECODE, @DATTRANSACTIONDATE, @NUMPAYMENT, " &
                        "    @STRCHECKNO, @STRDEPOSITNO, @STRBATCHNO, @STRENTRYPERSON, " &
                        "    @STRCOMMENT, @ACTIVE, @UPDATEUSER, getdate(), getdate(), " &
                        "    @STRAIRSNUMBER, @NUMFEEYEAR, @STRCREDITCARDNO " &
                        "  ) "
                    Dim param As SqlParameter() = {
                        New SqlParameter("@INVOICEID", txtInvoiceForDeposit.Text),
                        New SqlParameter("@TRANSACTIONTYPECODE", "1"),
                        New SqlParameter("@DATTRANSACTIONDATE", dtpBatchDepositDateField.Text),
                        New SqlParameter("@NUMPAYMENT", Replace(Replace(txtDepositAmount.Text, ",", ""), "$", "")),
                        New SqlParameter("@STRCHECKNO", txtCheckNumberField.Text),
                        New SqlParameter("@STRDEPOSITNO", txtDepositNumberField.Text),
                        New SqlParameter("@STRBATCHNO", txtBatchNoField.Text),
                        New SqlParameter("@STRENTRYPERSON", CurrentUser.UserID),
                        New SqlParameter("@STRCOMMENT", txtDepositComments.Text),
                        New SqlParameter("@ACTIVE", "1"),
                        New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                        New SqlParameter("@STRAIRSNUMBER", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString),
                        New SqlParameter("@NUMFEEYEAR", cbYear2.Text),
                        New SqlParameter("@STRCREDITCARDNO", txtCreditCardNo.Text)
                    }
                    DB.RunCommand(query, param)

                    If txtInvoiceForDeposit.Text.Trim <> "" Then
                        If Not InvoiceStatusCheck(txtInvoiceForDeposit.Text.Trim) Then
                            MsgBox("There was a problem updating the invoice status in the database.", MsgBoxStyle.Exclamation, "Invoice Status Error")
                        End If
                    Else
                        MsgBox("There is no invoice associated with this deposit.", MsgBoxStyle.Information, "No Invoice Number")
                    End If
                Else
                    MsgBox("Use ""Update Existing Check Deposit"" instead.")
                    Return
                End If

                If Not DAL.UpdateFeeAdminStatus(CInt(cbYear2.Text), New Apb.ApbFacilityId(mtbAIRSNumber.Text)) Then
                    MessageBox.Show("There was an error updating the database.", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                If Not ViewInvoices() Then
                    MsgBox("There was an error loading invoices.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                Else
                    If Not LoadInvoicesGridview() Then
                        MsgBox("There was an error filling the invoices grid.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                    End If
                End If

                ClearForm()
                MsgBox("The record was added successfully.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function InvoiceStatusCheck(invoiceID As String) As Boolean
        Try
            Dim query As String = "SELECT (invoiceTotal - PaymentTotal) AS Balance
                FROM (SELECT isnull(sum(numAmount), 0) AS InvoiceTotal
                      FROM FS_Feeinvoice
                      WHERE invoiceid = @invID
                            AND Active = '1') INVOICED,
                    (SELECT isnull(sum(numPayment), 0) AS PaymentTotal
                     FROM FS_TRANSACTIONS
                     WHERE invoiceid = @invID
                           AND Active = '1') Payments"

            Dim param As New SqlParameter("@invID", invoiceID)

            Dim balance As Decimal = DB.GetSingleValue(Of Decimal)(query, param)

            If balance = 0 Then
                query = "Update FS_FeeInvoice set " &
                "strInvoicestatus = '1' " &
                "where invoiceId = @invID "
            Else
                query = "Update FS_FeeInvoice set " &
                "strInvoicestatus = '0' " &
                "where invoiceId = @invID "
            End If

            Return DB.RunCommand(query, param)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub btnUpdateExistingDeposit_Click(sender As Object, e As EventArgs) Handles btnUpdateExistingDeposit.Click
        Try
            If txtTransactionID.Text <> "" Then
                If ValidateData() Then
                    If txtTransactionID.Text <> "" Then
                        Dim query As String = "UPDATE FS_TRANSACTIONS " &
                            "SET INVOICEID = @INVOICEID, TRANSACTIONTYPECODE = " &
                            "  @TRANSACTIONTYPECODE, DATTRANSACTIONDATE = " &
                            "  @DATTRANSACTIONDATE, NUMPAYMENT = @NUMPAYMENT, STRCHECKNO = " &
                            "  @STRCHECKNO, STRDEPOSITNO = @STRDEPOSITNO, STRBATCHNO = " &
                            "  @STRBATCHNO, STRCOMMENT = @STRCOMMENT, ACTIVE = @ACTIVE, " &
                            "  UPDATEUSER = @UPDATEUSER, UPDATEDATETIME = getdate(), " &
                            "  STRCREDITCARDNO = @STRCREDITCARDNO " &
                            "WHERE TRANSACTIONID = @TRANSACTIONID "
                        Dim param As SqlParameter() = {
                            New SqlParameter("@INVOICEID", txtInvoiceForDeposit.Text),
                            New SqlParameter("@TRANSACTIONTYPECODE", "1"),
                            New SqlParameter("@DATTRANSACTIONDATE", dtpBatchDepositDateField.Text),
                            New SqlParameter("@NUMPAYMENT", Replace(Replace(txtDepositAmount.Text, ",", ""), "$", "")),
                            New SqlParameter("@STRCHECKNO", txtCheckNumberField.Text),
                            New SqlParameter("@STRDEPOSITNO", txtDepositNumberField.Text),
                            New SqlParameter("@STRBATCHNO", txtBatchNoField.Text),
                            New SqlParameter("@STRCOMMENT", txtDepositComments.Text),
                            New SqlParameter("@ACTIVE", "1"),
                            New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                            New SqlParameter("@STRCREDITCARDNO", txtCreditCardNo.Text),
                            New SqlParameter("@TRANSACTIONID", txtTransactionID.Text)
                        }
                        DB.RunCommand(query, param)
                    Else
                        MsgBox("Use ""Add New Check Deposit"" instead.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If

                    If Not DAL.UpdateFeeAdminStatus(CInt(cbYear2.Text), New Apb.ApbFacilityId(mtbAIRSNumber.Text)) Then
                        MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    If txtInvoiceForDeposit.Text.Trim <> "" Then
                        If Not InvoiceStatusCheck(txtInvoiceForDeposit.Text.Trim) Then
                            MsgBox("There was a problem updating the invoice status in the database.", MsgBoxStyle.Exclamation, "Invoice Status Error")
                        End If
                    Else
                        MsgBox("There is no invoice associated with this deposit.", MsgBoxStyle.Information, "No Invoice Number")
                    End If

                    If Not ViewInvoices() Then
                        MsgBox("There was an error loading invoices.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                    Else
                        If Not LoadInvoicesGridview() Then
                            MsgBox("There was an error filling the invoices grid.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                        End If
                    End If

                    ClearForm()
                    MsgBox("The record has been updated successfully", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Please select an existing record from either of the two list.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteCheckDeposit_Click(sender As Object, e As EventArgs) Handles btnDeleteCheckDeposit.Click
        Try
            If txtTransactionID.Text = "" Then
                MsgBox("Select a transaction first.", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            Dim result As DialogResult =
                MessageBox.Show("Are you sure you want to remove transaction #" & txtTransactionID.Text &
                                " for AIRS # - " & mtbAIRSNumber.Text & "?",
                                "Fee Tool", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If result = System.Windows.Forms.DialogResult.No Then
                Return
            End If

            Dim query As String = "Update FS_Transactions set " &
                "active = '0', " &
                "updateUser = @updateUser, " &
                "updateDateTime = getdate() " &
                "where TransactionId = @trID "

            Dim p As SqlParameter() = {
                New SqlParameter("@updateUser", CurrentUser.UserID),
                New SqlParameter("@trID", txtTransactionID.Text)
            }

            If Not DB.RunCommand(query, p) Then
                MsgBox("There was an error deleting the deposit.", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            If Not DAL.InvoiceStatusCheck(txtInvoiceForDeposit.Text) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If Not DAL.UpdateFeeAdminStatus(cbYear2.Text, mtbAIRSNumber.Text) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If Not ViewInvoices() OrElse Not LoadInvoicesGridview() Then
                MsgBox("There was an error loading invoices.", MsgBoxStyle.Exclamation, "Invoice Search Error")
            End If

            txtCheckNumber.Clear()
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cbYear2.SelectedIndex = 0
            txtDepositAmount.Clear()
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            dtpBatchDepositDateField.Text = Date.Today

            MsgBox("The deposit has been deleted successfully.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function LoadDepositsGridview() As Boolean
        Try
            dgvDeposits.DataSource = dtDeposit

            If dtDeposit IsNot Nothing Then
                dgvDeposits.RowHeadersVisible = False
                dgvDeposits.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvDeposits.AllowUserToResizeColumns = True
                dgvDeposits.AllowUserToAddRows = False
                dgvDeposits.AllowUserToDeleteRows = False
                dgvDeposits.AllowUserToOrderColumns = False
                dgvDeposits.AllowUserToResizeRows = False
                dgvDeposits.MultiSelect = False
                dgvDeposits.ColumnHeadersHeight = "35"
                dgvDeposits.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvDeposits.Columns("strairsnumber").DisplayIndex = 0
                dgvDeposits.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvDeposits.Columns("strdepositno").DisplayIndex = 1
                dgvDeposits.Columns("datTransactionDate").HeaderText = "Transaction Date"
                dgvDeposits.Columns("datTransactionDate").DisplayIndex = 2
                dgvDeposits.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvDeposits.Columns("numpayment").HeaderText = "Payment"
                dgvDeposits.Columns("numpayment").DisplayIndex = 3
                dgvDeposits.Columns("numpayment").DefaultCellStyle.Format = "c"
                dgvDeposits.Columns("nuMFeeyear").HeaderText = "Year"
                dgvDeposits.Columns("numFeeyear").DisplayIndex = 4
                dgvDeposits.Columns("strcheckno").HeaderText = "Check Number"
                dgvDeposits.Columns("strcheckno").DisplayIndex = 5
                dgvDeposits.Columns("strCreditCardNo").HeaderText = "Credit Card Conf. #"
                dgvDeposits.Columns("strCreditCardNo").DisplayIndex = 6

                dgvDeposits.Columns("strbatchno").HeaderText = "Batch Number"
                dgvDeposits.Columns("strbatchno").DisplayIndex = 7
                dgvDeposits.Columns("strpaytype").HeaderText = "Payment Type"
                dgvDeposits.Columns("strpaytype").DisplayIndex = 8
                dgvDeposits.Columns("TransactionID").HeaderText = "Transaction ID"
                dgvDeposits.Columns("TransactionID").DisplayIndex = 9
                dgvDeposits.Columns("TransactionID").Visible = True
                dgvDeposits.Columns("strcomment").HeaderText = "Comments"
                dgvDeposits.Columns("strcomment").DisplayIndex = 10
                dgvDeposits.Columns("strcomment").Visible = False
                dgvDeposits.Columns("invoiceID").HeaderText = "Invoice Number"
                dgvDeposits.Columns("invoiceID").DisplayIndex = 11
                dgvDeposits.Columns("invoiceID").Visible = True

            End If
            txtDepositCount.Text = dgvDeposits.RowCount.ToString

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function LoadInvoicesGridview() As Boolean
        Try
            dgvInvoices.DataSource = dtInvoice

            If dtInvoice IsNot Nothing Then
                dgvInvoices.RowHeadersVisible = False
                dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvInvoices.AllowUserToResizeColumns = True
                dgvInvoices.AllowUserToAddRows = False
                dgvInvoices.AllowUserToDeleteRows = False
                dgvInvoices.AllowUserToOrderColumns = False
                dgvInvoices.AllowUserToResizeRows = False
                dgvInvoices.MultiSelect = False
                dgvInvoices.ColumnHeadersHeight = "35"
                dgvInvoices.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvInvoices.Columns("strairsnumber").DisplayIndex = 0
                dgvInvoices.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvInvoices.Columns("strdepositno").DisplayIndex = 1
                dgvInvoices.Columns("datTransactionDate").HeaderText = "Deposit Date"
                dgvInvoices.Columns("datTransactionDate").DisplayIndex = 2
                dgvInvoices.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvInvoices.Columns("numpayment").HeaderText = "Payment"
                dgvInvoices.Columns("numpayment").DisplayIndex = 3
                dgvInvoices.Columns("numpayment").DefaultCellStyle.Format = "c"
                dgvInvoices.Columns("numFeeyear").HeaderText = "Year"
                dgvInvoices.Columns("numFeeyear").DisplayIndex = 4
                dgvInvoices.Columns("strcheckno").HeaderText = "Check Number"
                dgvInvoices.Columns("strcheckno").DisplayIndex = 5
                dgvInvoices.Columns("strbatchno").HeaderText = "Batch Number"
                dgvInvoices.Columns("strbatchno").DisplayIndex = 6
                dgvInvoices.Columns("Description").HeaderText = "Payment Type"
                dgvInvoices.Columns("Description").DisplayIndex = 7
                dgvInvoices.Columns("TransactionID").HeaderText = "Transaction ID"
                dgvInvoices.Columns("TransactionID").DisplayIndex = 8
                dgvInvoices.Columns("TransactionID").Visible = False
                dgvInvoices.Columns("strcomment").HeaderText = "Comments"
                dgvInvoices.Columns("strcomment").DisplayIndex = 9
                dgvInvoices.Columns("strcomment").Visible = False
                dgvInvoices.Columns("InvoiceID").HeaderText = "Invoice Number"
                dgvInvoices.Columns("InvoiceID").DisplayIndex = 10
                dgvInvoices.Columns("InvoiceID").Visible = True
                dgvInvoices.Columns("FeeDue").HeaderText = "Fees Due"
                dgvInvoices.Columns("FeeDue").DisplayIndex = 11
                dgvInvoices.Columns("FeeDue").Visible = True
            End If

            txtCountInvoices.Text = dgvInvoices.RowCount.ToString

            If DAL.AirsNumberExists(mtbAIRSNumber.Text) AndAlso dgvInvoices.RowCount = 0 Then
                Dim query As String = "Select " &
                "strFacilityName " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @AIRSNumber "
                Dim param As New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString)

                Dim facName As String = DB.GetString(query, param)

                If String.IsNullOrWhiteSpace(facName) Then
                    lblAIRSNumber.Text = "AIRS #"
                    lblFacilityName.Text = "Facility Name"
                Else
                    lblAIRSNumber.Text = "AIRS #: " & mtbAIRSNumber.Text
                    lblFacilityName.Text = "Facility Name: " & facName
                End If
            End If

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub ClearForm()
        txtCheckNumber.Clear()
        mtbAIRSNumber.Clear()
        cbYear.SelectedIndex = 0
        lblAIRSNumber.Text = "AIRS #"
        lblFacilityName.Text = "Facility Name"
        cbYear2.SelectedIndex = 0
        txtDepositAmount.Clear()
        txtTransactionID.Clear()
        txtDepositComments.Clear()
        txtDepositNumberField.Clear()
        txtBatchNoField.Clear()
        txtCheckNumberField.Clear()
        dtpBatchDepositDateField.Value = Date.Today
        txtCreditCardNo.Clear()
    End Sub

#End Region

    Private Sub btnClearForm_Click(sender As Object, e As EventArgs) Handles btnClearForm.Click
        Try
            ClearForm()
            dgvDeposits.DataSource = Nothing
            dgvInvoices.DataSource = Nothing
            ActiveControl = mtbAIRSNumber
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSearchForCheck_Click(sender As Object, e As EventArgs) Handles btnSearchForCheck.Click
        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cbYear2.SelectedIndex = 0
            txtDepositAmount.Clear()
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            dtpBatchDepositDateField.Value = Date.Today

            If txtCheckNumber.Text <> "" Then
                If Not ViewInvoices() Then
                    MsgBox("There was an error loading invoices.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                Else
                    If Not LoadInvoicesGridview() Then
                        MsgBox("There was an error filling the invoices grid.", MsgBoxStyle.Exclamation, "Invoice Search Error")
                    End If
                End If
            Else
                MsgBox("You must enter a check # (partial or complete).")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSearchForInvoice_Click(sender As Object, e As EventArgs) Handles btnSearchForInvoice.Click
        Try
            Dim query As String
            Dim param As SqlParameter()

            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cbYear2.SelectedIndex = 0
            txtDepositAmount.Clear()
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            dtpBatchDepositDateField.Value = Date.Today

            If txtSearchInvoice.Text <> "" Then
                query = "select " &
                "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " &
                "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " &
                " strComment,  ALLInvoices.InvoiceID,  " &
                "FeeDue  " &
                "from  " &
                "(select substring(FS_FeeINvoice.strAIRSnumber, 5, 8) as strAIRSNumber, " &
                "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                "from  FS_FeeInvoice " &
                "where FS_FeeInvoice.InvoiceID like @invID " &
                "and FS_FeeInvoice.Active = '1' " &
                "union " &
                "select distinct substring(FS_FeeINvoice.strAIRSnumber, 5, 8) as strAIRSNumber, " &
                "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                "from FS_Transactions " &
                "right join FS_FeeInvoice " &
                "on FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID " &
                "left join FSLK_TransactionType  " &
                "on FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode " &
                "where FS_FeeInvoice.InvoiceID like @invID " &
                "and FS_FeeInvoice.Active = '1' " &
                "and FS_Transactions.Active = '1'  ) ALLInvoices " &
                "left join (select distinct substring(FS_FeeINvoice.strAIRSnumber, 5, 8) as strAIRSNumber, " &
                "strDepositNo, datTransactionDate, " &
                "numPayment, FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " &
                "FS_Transactions.strComment, FS_FeeINvoice.InvoiceID, " &
                "case when FS_Transactions.transactionTypeCode = '1' then numAmount " &
                "when FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " &
                "from FS_Transactions " &
                "right join FS_FeeInvoice " &
                "on FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID " &
                "left join FSLK_TransactionType  " &
                "on FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode " &
                "where FS_FeeInvoice.Active = '1' " &
                "and FS_Transactions.Active = '1' ) Transactions " &
                "on Allinvoices.InvoiceID = Transactions.InvoiceID " &
                "order by strBatchNo "

                param = {New SqlParameter("@invID", "%" & txtSearchInvoice.Text & "%")}

                dtInvoice = DB.GetDataTable(query, param)

                dgvInvoices.DataSource = dtInvoice

                dgvInvoices.RowHeadersVisible = False
                dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvInvoices.AllowUserToResizeColumns = True
                dgvInvoices.AllowUserToAddRows = False
                dgvInvoices.AllowUserToDeleteRows = False
                dgvInvoices.AllowUserToOrderColumns = True
                dgvInvoices.AllowUserToResizeRows = True
                dgvInvoices.ColumnHeadersHeight = "35"
                dgvInvoices.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvInvoices.Columns("strairsnumber").DisplayIndex = 0
                dgvInvoices.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvInvoices.Columns("strdepositno").DisplayIndex = 3
                dgvInvoices.Columns("datTransactionDate").HeaderText = "Deposit Date"
                dgvInvoices.Columns("datTransactionDate").DisplayIndex = 7
                dgvInvoices.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvInvoices.Columns("numpayment").HeaderText = "Payment"
                dgvInvoices.Columns("numpayment").DisplayIndex = 6
                dgvInvoices.Columns("numpayment").DefaultCellStyle.Format = "c"
                dgvInvoices.Columns("numFeeyear").HeaderText = "Year"
                dgvInvoices.Columns("numFeeyear").DisplayIndex = 1
                dgvInvoices.Columns("strcheckno").HeaderText = "Check Number"
                dgvInvoices.Columns("strcheckno").DisplayIndex = 5
                dgvInvoices.Columns("strbatchno").HeaderText = "Batch Number"
                dgvInvoices.Columns("strbatchno").DisplayIndex = 4
                dgvInvoices.Columns("Description").HeaderText = "Payment Type"
                dgvInvoices.Columns("Description").DisplayIndex = 8
                dgvInvoices.Columns("TransactionID").HeaderText = "Transaction ID"
                dgvInvoices.Columns("TransactionID").DisplayIndex = 10
                dgvInvoices.Columns("TransactionID").Visible = False
                dgvInvoices.Columns("strcomment").HeaderText = "Comments"
                dgvInvoices.Columns("strcomment").DisplayIndex = 11
                dgvInvoices.Columns("strcomment").Visible = False
                dgvInvoices.Columns("InvoiceID").HeaderText = "Invoice Number"
                dgvInvoices.Columns("InvoiceID").DisplayIndex = 2
                dgvInvoices.Columns("InvoiceID").Visible = True
                dgvInvoices.Columns("FeeDue").HeaderText = "Fees Due"
                dgvInvoices.Columns("FeeDue").DisplayIndex = 9
                dgvInvoices.Columns("FeeDue").Visible = True
                txtCountInvoices.Text = dgvInvoices.RowCount.ToString

                If DAL.AirsNumberExists(mtbAIRSNumber.Text) AndAlso dgvInvoices.RowCount = 0 Then
                    query = "Select " &
                        "strFacilityName " &
                        "from APBFacilityInformation " &
                        "where strAIRSNumber = @AIRSNumber "
                    param = {New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString)}

                    Dim facName As String = DB.GetString(query, param)

                    If String.IsNullOrWhiteSpace(facName) Then
                        lblAIRSNumber.Text = "AIRS #: "
                        lblFacilityName.Text = "Facility Name: "
                    Else
                        lblAIRSNumber.Text = "AIRS #: " & mtbAIRSNumber.Text
                        lblFacilityName.Text = "Facility Name: " & facName
                    End If
                End If
            Else
                MsgBox("You must enter an invoice # (Partial or complete).", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region " Accept Button "

    Private Sub AcceptButton_Leave(sender As Object, e As EventArgs) _
        Handles dtpDepositReportStartDate.Leave, dtpDepositReportEndDate.Leave,
        txtCheckNumber.Leave, txtSearchInvoice.Leave,
        mtbAIRSNumber.Leave, cbYear.Leave, cbYear2.Leave

        AcceptButton = Nothing
    End Sub

    Private Sub dtpDepositReportStartDate_Enter(sender As Object, e As EventArgs) Handles dtpDepositReportStartDate.Enter, dtpDepositReportEndDate.Enter
        AcceptButton = btnSearchDeposits
    End Sub

    Private Sub txtCheckNumber_Enter(sender As Object, e As EventArgs) Handles txtCheckNumber.Enter
        AcceptButton = btnSearchForCheck
    End Sub

    Private Sub txtSearchInvoice_Enter(sender As Object, e As EventArgs) Handles txtSearchInvoice.Enter
        AcceptButton = btnSearchForInvoice
    End Sub

    Private Sub mtbAIRSNumber_Enter(sender As Object, e As EventArgs) Handles mtbAIRSNumber.Enter, cbYear.Enter, cbYear2.Enter
        AcceptButton = btnViewInvoices
    End Sub

#End Region

    'Form overrides dispose to clean up the component list.
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If dtInvoice IsNot Nothing Then dtInvoice.Dispose()
                If dtDeposit IsNot Nothing Then dtDeposit.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class