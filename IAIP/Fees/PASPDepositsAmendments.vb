Imports System.Data.SqlClient


Public Class PASPDepositsAmendments
    Dim dtInvoice As DataTable
    Dim dtDeposit As DataTable

#Region "Page Load Functions"

    Private Sub PASPDepositsAmendments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dtpBatchDepositDate.Text = Date.Today
        dtpDepositReportStartDate.Text = Format(CDate(Date.Today).AddMonths(-1), "dd-MMM-yyyy")
        dtpDepositReportEndDate.Text = Date.Today
    End Sub

#End Region

#Region "Fee Deposits"

    Private Sub LoadFacilityData(ByVal AIRSNumber As String)
        Dim query As String = "Select " &
            "strFacilityName " &
            "from APBFacilityInformation " &
            "where strAIRSNumber = @AIRSNumber "
        Dim param As New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(AIRSNumber).DbFormattedString)

        Dim facName As String = DB.GetSingleValue(Of String)(query, param)

        If String.IsNullOrWhiteSpace(facName) Then
            lblFacilityName.Text = "Facility Name"
        Else
            lblFacilityName.Text = "Facility Name: " & facName
        End If
    End Sub

    Private Function ValidateData() As Boolean
        Dim query As String
        Dim param() As SqlParameter

        If mtbAIRSNumber.Text <> "" Then
            query = "Select " &
                "strAIRSNumber " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @AIRSNumber "
            param = {New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString)}

            If Not DB.ValueExists(query, param) Then
                MsgBox("This AIRS # is not valid; please verify that it is entered correctly." & vbCrLf &
                       "If you get this message in error, contact the Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                Return False
            End If
        Else
            MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf &
             "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
            Return False
        End If

        If mtbFeeYear2.Text = "" Then
            If Not IsNumeric(mtbFeeYear.Text) Then
                MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                Return False
            End If
        End If

        If txtDepositAmount.Text = "" Then
            MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
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

    Private Sub DepositSearch()
        Dim query As String = "SELECT SUBSTR(tr.STRAIRSNUMBER, 5) AS strAIRSNumber, " &
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
    End Sub

    Sub DeleteInvoice()
        Try

            Dim Result As DialogResult = MessageBox.Show("Are you sure you want to remove Invoice # " & txtInvoiceForDeposit.Text & " for AIRS # - " & mtbAIRSNumber.Text & "?",
                                                         "PASP Fee Tool", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If Result <> DialogResult.Yes Then
                Exit Sub
            End If

            Dim query As String = "Delete FSAddPaid " &
            "where intPayID = @trID"
            Dim param As New SqlParameter("@trID", txtTransactionID.Text)
            DB.RunCommand(query, param)

            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

            lblViewInvoices.Enabled = False

            bgwInvoices.WorkerReportsProgress = True
            bgwInvoices.WorkerSupportsCancellation = True
            bgwInvoices.RunWorkerAsync()

            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositAmount.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = Date.Today

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewInvoices()
        Dim query As String
        Dim param() As SqlParameter

        If txtCheckNumber.Text <> "" Then
            query = "select " &
                "substr(FS_FeeInvoice.strAIRSnumber, 5) as strAIRSNumber, " &
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
                "from FS_Transactions, FS_FeeInvoice, " &
                "FSLK_TransactionType  " &
                "where FS_FeeInvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                "and strCheckNo like @chknum  " &
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
                "(select substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                "from  FS_FeeInvoice " &
                "where FS_FeeInvoice.strAIRSnumber like @airsnum " &
                "and FS_FeeInvoice.numFeeYear = @feeyear " &
                "and FS_FeeInvoice.Active = '1' " &
                "union " &
                "select distinct substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                "from FS_Transactions, FS_FeeInvoice, FSLK_TransactionType  " &
                "where FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                "and FS_FeeInvoice.strAIRSnumber like @airsnum " &
                "and FS_FeeInvoice.numFeeYear = @feeyear and FS_FeeInvoice.Active = '1' " &
                "and FS_Transactions.Active = '1'  ) ALLInvoices,  " &
                "(select distinct substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, strDepositNo, datTransactionDate, " &
                "numPayment, FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " &
                "FS_Transactions.strComment, FS_FeeINvoice.InvoiceID, " &
                "case when FS_Transactions.transactionTypeCode = '1' then numAmount " &
                "when FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " &
                "from FS_Transactions, FS_FeeInvoice, FSLK_TransactionType  " &
                "where FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                "and FS_FeeInvoice.strAIRSnumber like @airsnum " &
                "and FS_FeeInvoice.numFeeYear = @feeyear " &
                "and FS_FeeInvoice.Active = '1' " &
                "and FS_Transactions.Active = '1' order by strBatchNo) Transactions " &
                "where Allinvoices.InvoiceID = Transactions.InvoiceID  (+) "
            param = {
                New SqlParameter("@airsnum", "%" & mtbAIRSNumber.Text & "%"),
                New SqlParameter("@feeyear", mtbFeeYear.Text)
            }

        End If

        dtInvoice = DB.GetDataTable(query, param)
    End Sub

    Private Sub btnSearchDeposits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDeposits.Click
        btnSearchDeposits.Enabled = False

        bgwDeposits.WorkerReportsProgress = True
        bgwDeposits.WorkerSupportsCancellation = True
        bgwDeposits.RunWorkerAsync()
    End Sub

    Private Sub lblViewInvoices_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewInvoices.LinkClicked
        lblAIRSNumber.Text = "AIRS #"
        lblFacilityName.Text = "Facility Name"
        mtbFeeYear2.Clear()
        txtDepositAmount.Clear()
        txtTransactionID.Clear()
        txtDepositComments.Clear()
        txtDepositNumberField.Clear()
        txtBatchNoField.Clear()
        txtCheckNumberField.Clear()
        DTPBatchDepositDateField.Text = Date.Today
        txtCheckNumber.Clear()

        If mtbAIRSNumber.Text <> "" Then
            If mtbFeeYear.Text <> "" Then
                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()
            Else
                MsgBox("Select a year to check for invoices.", MsgBoxStyle.Information, "PASP Deposit Amendments")
            End If
        End If
    End Sub

    Private Sub dgvDeposits_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDeposits.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvDeposits.HitTest(e.X, e.Y)

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvDeposits.RowCount > 0 And hti.RowIndex <> -1 Then

                    If dgvDeposits.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAIRSNumber.Text = dgvDeposits(0, hti.RowIndex).Value
                        LoadFacilityData(dgvDeposits(0, hti.RowIndex).Value)
                        lblAIRSNumber.Text = "AIRS #: " & dgvDeposits(0, hti.RowIndex).Value
                        If IsDBNull(dgvDeposits(3, hti.RowIndex).Value) Then
                        Else
                            txtTransactionID.Text = dgvDeposits(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(2, hti.RowIndex).Value) Then
                            txtBatchNumber.Clear()
                        Else
                            txtBatchNumber.Text = dgvDeposits(2, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(2, hti.RowIndex).Value) Then
                            txtBatchNoField.Clear()
                            If txtBatchNumber.Text <> "" Then
                                txtBatchNoField.Text = txtBatchNumber.Text
                            End If
                        Else
                            txtBatchNoField.Text = dgvDeposits(2, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(4, hti.RowIndex).Value) Then
                            dtpBatchDepositDate.Text = Date.Today
                            DTPBatchDepositDateField.Text = Date.Today
                        Else
                            dtpBatchDepositDate.Text = dgvDeposits(4, hti.RowIndex).FormattedValue
                            DTPBatchDepositDateField.Text = dgvDeposits(4, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvDeposits(5, hti.RowIndex).Value) Then
                            txtDepositAmount.Clear()
                        Else
                            txtDepositAmount.Text = dgvDeposits(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(6, hti.RowIndex).Value) Then
                            mtbFeeYear.Clear()
                            mtbFeeYear2.Clear()
                        Else
                            mtbFeeYear.Text = dgvDeposits(6, hti.RowIndex).Value
                            mtbFeeYear2.Text = dgvDeposits(6, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(11, hti.RowIndex).Value) Then
                            txtDepositComments.Clear()
                        Else
                            txtDepositComments.Text = dgvDeposits(11, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(9, hti.RowIndex).Value) Then
                            txtSearchInvoice.Text = ""
                            txtInvoiceForDeposit.Clear()
                        Else
                            txtSearchInvoice.Text = dgvDeposits(9, hti.RowIndex).Value
                            txtInvoiceForDeposit.Text = dgvDeposits(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(7, hti.RowIndex).Value) Then
                            txtCheckNumber.Clear()
                        Else
                            txtCheckNumber.Text = dgvDeposits(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(7, hti.RowIndex).Value) Then
                            txtCheckNumberField.Clear()
                            If txtCheckNumber.Text <> "" Then
                                txtCheckNumberField.Text = txtCheckNumber.Text
                            End If
                        Else
                            txtCheckNumberField.Text = dgvDeposits(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(1, hti.RowIndex).Value) Then
                            txtDepositNumberField.Clear()
                        Else
                            txtDepositNumberField.Text = dgvDeposits(1, hti.RowIndex).Value
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvInvoices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvInvoices.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvInvoices.HitTest(e.X, e.Y)

            If hti.Type = DataGrid.HitTestType.Cell Then
                If dgvInvoices.RowCount > 0 And hti.RowIndex <> -1 Then

                    If dgvInvoices.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAIRSNumber.Text = dgvInvoices(0, hti.RowIndex).Value
                        LoadFacilityData(dgvInvoices(0, hti.RowIndex).Value)
                        lblAIRSNumber.Text = "AIRS #: " & dgvInvoices(0, hti.RowIndex).Value
                        If IsDBNull(dgvInvoices(8, hti.RowIndex).Value) Then
                            txtTransactionID.Text = ""
                        Else
                            txtTransactionID.Text = dgvInvoices(8, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(1, hti.RowIndex).Value) Then
                            txtDepositNumberField.Clear()
                        Else
                            txtDepositNumberField.Text = dgvInvoices(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(11, hti.RowIndex).Value) Then
                            If IsDBNull(dgvInvoices(3, hti.RowIndex).Value) Then
                                txtDepositAmount.Clear()
                            Else
                                txtDepositAmount.Text = dgvInvoices(3, hti.RowIndex).Value
                            End If
                        Else
                            If IsDBNull(dgvInvoices(3, hti.RowIndex).Value) Then
                                txtDepositAmount.Text = dgvInvoices(11, hti.RowIndex).Value
                            Else
                                If dgvInvoices(3, hti.RowIndex).Value = 0 Then
                                    txtDepositAmount.Text = dgvInvoices(11, hti.RowIndex).Value
                                Else
                                    txtDepositAmount.Text = dgvInvoices(3, hti.RowIndex).Value
                                End If
                            End If
                        End If

                        If IsDBNull(dgvInvoices(4, hti.RowIndex).Value) Then
                            mtbFeeYear.Clear()
                            mtbFeeYear2.Clear()
                        Else
                            mtbFeeYear.Text = dgvInvoices(4, hti.RowIndex).Value
                            mtbFeeYear2.Text = dgvInvoices(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(5, hti.RowIndex).Value) Then
                            txtCheckNumberField.Clear()
                            If txtCheckNumber.Text <> "" Then
                                txtCheckNumberField.Text = txtCheckNumber.Text
                            End If
                        Else
                            txtCheckNumberField.Text = dgvInvoices(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(6, hti.RowIndex).Value) Then
                            txtBatchNoField.Clear()
                            If txtBatchNumber.Text <> "" Then
                                txtBatchNoField.Text = txtBatchNumber.Text
                            End If
                        Else
                            txtBatchNoField.Text = dgvInvoices(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(9, hti.RowIndex).Value) Then
                            txtDepositComments.Clear()
                        Else
                            txtDepositComments.Text = dgvInvoices(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(10, hti.RowIndex).Value) Then
                            txtInvoiceForDeposit.Clear()
                        Else
                            txtInvoiceForDeposit.Text = dgvInvoices(10, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(2, hti.RowIndex).Value) Then
                            DTPBatchDepositDateField.Text = dtpBatchDepositDate.Text
                        Else
                            DTPBatchDepositDateField.Text = dgvInvoices(2, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewCheckDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewCheckDeposit.Click
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
                        "    SEQ_FS_TRANSACTIONS.nextval, @INVOICEID, " &
                        "    @TRANSACTIONTYPECODE, @DATTRANSACTIONDATE, @NUMPAYMENT, " &
                        "    @STRCHECKNO, @STRDEPOSITNO, @STRBATCHNO, @STRENTRYPERSON, " &
                        "    @STRCOMMENT, @ACTIVE, @UPDATEUSER, sysdate, sysdate, " &
                        "    @STRAIRSNUMBER, @NUMFEEYEAR, @STRCREDITCARDNO " &
                        "  ) "
                    Dim param() As SqlParameter
                    param = {
                        New SqlParameter("@INVOICEID", txtInvoiceForDeposit.Text),
                        New SqlParameter("@TRANSACTIONTYPECODE", "1"),
                        New SqlParameter("@DATTRANSACTIONDATE", DTPBatchDepositDateField.Text),
                        New SqlParameter("@NUMPAYMENT", Replace(Replace(txtDepositAmount.Text, ",", ""), "$", "")),
                        New SqlParameter("@STRCHECKNO", txtCheckNumberField.Text),
                        New SqlParameter("@STRDEPOSITNO", txtDepositNumberField.Text),
                        New SqlParameter("@STRBATCHNO", txtBatchNoField.Text),
                        New SqlParameter("@STRENTRYPERSON", CurrentUser.UserID),
                        New SqlParameter("@STRCOMMENT", txtDepositComments.Text),
                        New SqlParameter("@ACTIVE", "1"),
                        New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                        New SqlParameter("@STRAIRSNUMBER", "0413" & mtbAIRSNumber.Text),
                        New SqlParameter("@NUMFEEYEAR", mtbFeeYear2.Text),
                        New SqlParameter("@STRCREDITCARDNO", txtCreditCardNo.Text)
                    }
                    DB.RunCommand(query, param)

                    InvoiceStatusCheck(txtInvoiceForDeposit.Text)
                Else
                    MsgBox("Use the Update Existing Check Deposit instead.", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If

                If Not DAL.Update_FS_Admin_Status(mtbFeeYear2.Text, mtbAIRSNumber.Text) Then
                    MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                btnSearchDeposits.Enabled = False

                bgwDeposits.WorkerReportsProgress = True
                bgwDeposits.WorkerSupportsCancellation = True
                bgwDeposits.RunWorkerAsync()

                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()

                ClearForm()
                MsgBox("The record was added successfully", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub InvoiceStatusCheck(ByVal invoiceID As String)
        Try
            Dim query As String = "select " &
            "(invoiceTotal - PaymentTotal) as Balance " &
            "from (select " &
            "sum(numAmount) as InvoiceTotal " &
            "from FS_Feeinvoice " &
            "where invoiceid = @invID " &
            "and Active = '1' ) INVOICED, " &
            "(select " &
            "sum(numPayment) as PaymentTotal " &
            "from FS_TRANSACTIONS " &
            "where invoiceid = @invID " &
            "and Active = '1' ) Payments "
            Dim param As New SqlParameter("@invID", invoiceID)

            Dim result As String = DB.GetSingleValue(Of String)(query, param)
            If String.IsNullOrWhiteSpace(result) Then
                result = "1"
            End If

            If result <> "0" Then
                query = "Update FS_FeeInvoice set " &
                "strInvoicestatus = '0' " &
                "where invoiceId = @invID "
            Else
                query = "Update FS_FeeInvoice set " &
                "strInvoicestatus = '1' " &
                "where invoiceId = @ invID "
            End If
            DB.RunCommand(query, param)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateExistingDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateExistingDeposit.Click
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
                            "  UPDATEUSER = @UPDATEUSER, UPDATEDATETIME = sysdate, " &
                            "  STRCREDITCARDNO = @STRCREDITCARDNO " &
                            "WHERE TRANSACTIONID = @TRANSACTIONID "
                        Dim param As SqlParameter() = {
                            New SqlParameter("@INVOICEID", txtInvoiceForDeposit.Text),
                            New SqlParameter("@TRANSACTIONTYPECODE", "1"),
                            New SqlParameter("@DATTRANSACTIONDATE", DTPBatchDepositDateField.Text),
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
                        MsgBox("Use the Add New Check Deposit.", MsgBoxStyle.Information, Me.Text)
                        Exit Sub
                    End If

                    If Not DAL.Update_FS_Admin_Status(mtbFeeYear2.Text, mtbAIRSNumber.Text) Then
                        MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    InvoiceStatusCheck(txtInvoiceForDeposit.Text)

                    btnSearchDeposits.Enabled = False

                    bgwDeposits.WorkerReportsProgress = True
                    bgwDeposits.WorkerSupportsCancellation = True
                    bgwDeposits.RunWorkerAsync()

                    lblViewInvoices.Enabled = False

                    bgwInvoices.WorkerReportsProgress = True
                    bgwInvoices.WorkerSupportsCancellation = True
                    bgwInvoices.RunWorkerAsync()

                    ClearForm()
                    MsgBox("The record has been updated successfully", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Please select an existing record from either of the two list.", MsgBoxStyle.Information, "PASP Deposits and Amendments")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteCheckDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCheckDeposit.Click
        Try
            If txtTransactionID.Text = "" Then
                MsgBox("Select a transaction first.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to remove " & txtCheckNumberField.Text &
                                                         " for AIRS # - " & mtbAIRSNumber.Text & "?",
                                                         "PASP Fee Tool", MessageBoxButtons.YesNoCancel,
                                                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If result = System.Windows.Forms.DialogResult.Yes Then
                Dim query As String = "Update FS_Transactions set " &
                        "active = '0' " &
                        "where TransactionId = @trID "
                Dim param As New SqlParameter("@trID", txtTransactionID.Text)
                DB.RunCommand(query, param)

                btnSearchDeposits.Enabled = False

                bgwDeposits.WorkerReportsProgress = True
                bgwDeposits.WorkerSupportsCancellation = True
                bgwDeposits.RunWorkerAsync()

                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()

                txtCheckNumber.Clear()
                lblAIRSNumber.Text = "AIRS #"
                lblFacilityName.Text = "Facility Name"
                mtbFeeYear2.Clear()
                txtDepositAmount.Clear()
                txtTransactionID.Clear()
                txtDepositComments.Clear()
                txtDepositNumberField.Clear()
                txtBatchNoField.Clear()
                txtCheckNumberField.Clear()
                DTPBatchDepositDateField.Text = Date.Today

                MsgBox("The record has been deleted successfully", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteInventoryRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteInventoryRecords.Click
        If txtTransactionID.Text <> "" Then
            DeleteInvoice()
        End If
    End Sub

    Private Sub bgwDeposits_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwDeposits.DoWork
        DepositSearch()
    End Sub

    Private Sub bgwDeposits_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDeposits.RunWorkerCompleted
        Try
            dgvDeposits.DataSource = dtDeposit

            dgvDeposits.RowHeadersVisible = False
            dgvDeposits.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDeposits.AllowUserToResizeColumns = True
            dgvDeposits.AllowUserToAddRows = False
            dgvDeposits.AllowUserToDeleteRows = False
            dgvDeposits.AllowUserToOrderColumns = True
            dgvDeposits.AllowUserToResizeRows = True
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

            txtDepositCount.Text = dgvDeposits.RowCount.ToString
            btnSearchDeposits.Enabled = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgwInvoices_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwInvoices.DoWork
        ViewInvoices()
    End Sub

    Private Sub bgwInvoices_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInvoices.RunWorkerCompleted
        Try
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
            txtCountInvoices.Text = dgvInvoices.RowCount.ToString
            lblViewInvoices.Enabled = True

            If mtbAIRSNumber.Text <> "" And dgvInvoices.RowCount = 0 Then
                Dim query As String = "Select " &
                "strFacilityName " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @AIRSNumber "
                Dim param As New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString)

                Dim facName As String = DB.GetSingleValue(Of String)(query, param)

                If String.IsNullOrWhiteSpace(facName) Then
                    lblAIRSNumber.Text = "AIRS #: "
                    lblFacilityName.Text = "Facility Name: "
                Else
                    lblAIRSNumber.Text = "AIRS #: " & mtbAIRSNumber.Text
                    lblFacilityName.Text = "Facility Name: " & facName
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearForm()
        txtCheckNumber.Clear()
        mtbAIRSNumber.Clear()
        mtbFeeYear.Clear()
        lblAIRSNumber.Text = "AIRS #"
        lblFacilityName.Text = "Facility Name"
        mtbFeeYear2.Clear()
        txtDepositAmount.Clear()
        txtTransactionID.Clear()
        txtDepositComments.Clear()
        txtDepositNumberField.Clear()
        txtBatchNoField.Clear()
        txtCheckNumberField.Clear()
        DTPBatchDepositDateField.Text = Date.Today
        txtCreditCardNo.Clear()
    End Sub

    Private Sub btnClearEntryInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearEntryInformation.Click
        ClearForm()

        lblViewInvoices.Enabled = False

        bgwInvoices.WorkerReportsProgress = True
        bgwInvoices.WorkerSupportsCancellation = True
        bgwInvoices.RunWorkerAsync()
    End Sub

#End Region

    Private Sub btnClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        Try
            ClearForm()
            txtBatchNumber.Clear()
            dtpBatchDepositDate.Text = Date.Today

            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

            lblViewInvoices.Enabled = False

            bgwInvoices.WorkerReportsProgress = True
            bgwInvoices.WorkerSupportsCancellation = True
            bgwInvoices.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSearchForCheck_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchForCheck.LinkClicked
        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            mtbFeeYear2.Clear()
            txtDepositAmount.Clear()
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = Date.Today

            If txtCheckNumber.Text <> "" Then
                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()
            Else
                MsgBox("You must enter a check # (Partial or complete).", MsgBoxStyle.Information, "PASP Deposit Amendments")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSearchForInvoice_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchForInvoice.LinkClicked
        Dim query As String
        Dim param As SqlParameter()

        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            mtbFeeYear2.Clear()
            txtDepositAmount.Clear()
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = Date.Today

            If txtSearchInvoice.Text <> "" Then
                lblViewInvoices.Enabled = False

                If txtSearchInvoice.Text <> "" Then
                    query = "select " &
                    "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " &
                    "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " &
                    " strComment,  ALLInvoices.InvoiceID,  " &
                    "FeeDue  " &
                    "from  " &
                    "(select substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                    "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                    "from  FS_FeeInvoice " &
                    "where FS_FeeInvoice.InvoiceID like @invID " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "union " &
                    "select distinct substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                    "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                    "from FS_Transactions, FS_FeeInvoice, FSLK_TransactionType  " &
                    "where FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                    "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                    "and FS_FeeInvoice.InvoiceID like @invID " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Transactions.Active = '1'  ) ALLInvoices,  " &
                    "(select distinct substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                    "strDepositNo, datTransactionDate, " &
                    "numPayment, FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " &
                    "FS_Transactions.strComment, FS_FeeINvoice.InvoiceID, " &
                    "case when FS_Transactions.transactionTypeCode = '1' then numAmount " &
                    "when FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " &
                    "from FS_Transactions, FS_FeeInvoice, FSLK_TransactionType  " &
                    "where FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                    "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Transactions.Active = '1' order by strBatchNo) Transactions " &
                    "where Allinvoices.InvoiceID = Transactions.InvoiceID  (+) "

                    param = {New SqlParameter("@invID", "%" & txtSearchInvoice.Text & "%")}
                Else
                    query = "select " &
                        "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " &
                        "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " &
                        " strComment,  ALLInvoices.InvoiceID,  " &
                        "FeeDue  " &
                        "from  " &
                        "(select substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                        "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                        "from  FS_FeeInvoice " &
                        "where FS_FeeInvoice.strAIRSnumber like @airs " &
                        "and FS_FeeInvoice.numFeeYear = @feeyear " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "union " &
                        "select distinct substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " &
                        "FS_FeeINvoice.numFeeYear, FS_FeeINvoice.InvoiceID " &
                        "from FS_Transactions, FS_FeeInvoice, FSLK_TransactionType  " &
                        "where FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                        "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                        "and FS_FeeInvoice.strAIRSnumber like @airs " &
                        "and FS_FeeInvoice.numFeeYear = @feeyear and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.Active = '1'  ) ALLInvoices,  " &
                        "(select distinct substr(FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, strDepositNo, datTransactionDate, " &
                        "numPayment, FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " &
                        "FS_Transactions.strComment, FS_FeeINvoice.InvoiceID, " &
                        "case when FS_Transactions.transactionTypeCode = '1' then numAmount " &
                        "when FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " &
                        "from FS_Transactions, FS_FeeInvoice, FSLK_TransactionType  " &
                        "where FS_FeeINvoice.InvoiceID = FS_Transactions.INvoiceID (+) " &
                        "and FS_Transactions.transactionTypeCode = FSLK_TransactionType.TransactionTypeCode (+) " &
                        "and FS_FeeInvoice.strAIRSnumber like @airsnum " &
                        "and FS_FeeInvoice.numFeeYear = @feeyear " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.Active = '1' order by InvoiceID desc) Transactions " &
                        "where Allinvoices.InvoiceID = Transactions.InvoiceID  (+) "
                    param = {
                        New SqlParameter("@airs", "0413%" & mtbAIRSNumber.Text & "%"),
                        New SqlParameter("@feeyear", mtbFeeYear.Text)
                    }
                End If
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
                lblViewInvoices.Enabled = True

                If mtbAIRSNumber.Text <> "" And dgvInvoices.RowCount = 0 Then
                    query = "Select " &
                        "strFacilityName " &
                        "from APBFacilityInformation " &
                        "where strAIRSNumber = @AIRSNumber "
                    param = {New SqlParameter("@AIRSNumber", New Apb.ApbFacilityId(mtbAIRSNumber.Text).DbFormattedString)}

                    Dim facName As String = DB.GetSingleValue(Of String)(query, param)

                    If String.IsNullOrWhiteSpace(facName) Then
                        lblAIRSNumber.Text = "AIRS #: "
                        lblFacilityName.Text = "Facility Name: "
                    Else
                        lblAIRSNumber.Text = "AIRS #: " & mtbAIRSNumber.Text
                        lblFacilityName.Text = "Facility Name: " & facName
                    End If
                End If
            Else
                MsgBox("You must enter an invoice # (Partial or complete).", MsgBoxStyle.Information, "PASP Deposit Amendments")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class