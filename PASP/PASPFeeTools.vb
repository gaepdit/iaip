Imports System.Data.OracleClient

Public Class PASPFeeTools
    Dim da As OracleDataAdapter
    Dim ds As DataSet
    Dim daInvoice As OracleDataAdapter
    Dim dsInvoice As DataSet
    Dim ValidatingState As Boolean
    Dim feeyear As String = Now.Year
   
    Private Sub PASPFeeTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            pnl1.Text = " "
            pnl2.Text = UserName
            pnl3.Text = OracleDate
            feeyear = Now.AddYears(-1).Year

            LoadComboboxes()
         
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub LoadComboboxes()
        Try
            Dim dtPayType As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            ds = New DataSet

            SQL = "Select " & _
            "PayID, PayType " & _
            "from " & DBNameSpace & ".FSPayType " & _
            "order by paytype"

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            da.Fill(ds, "PayType")

            dtPayType.Columns.Add("PayID", GetType(System.String))
            dtPayType.Columns.Add("PayType", GetType(System.String))

            drNewRow = dtPayType.NewRow()
            drNewRow("PayType") = " "
            drNewRow("PayID") = " "
            dtPayType.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("PayType").Rows()
                drNewRow = dtPayType.NewRow()
                drNewRow("PayID") = drDSRow("PayID")
                drNewRow("PayType") = drDSRow("PayType")
                dtPayType.Rows.Add(drNewRow)
            Next

            With cboDepositPayType
                .DataSource = dtPayType
                .DisplayMember = "PayType"
                .ValueMember = "PayID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Sub LoadFacilityData(ByVal AIRSNumber As String)
        Try
            SQL = "Select " & _
            "strAIRSNumber, strFacilityName " & _
            "from " & DBNameSpace & ".APBFacilityInformation " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' "
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    lblFacilityName.Text = "Facility Name"
                Else
                    lblFacilityName.Text = "Facility Name: " & dr.Item("strFacilityName")
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub
    Sub ValidateData()
        Try
            If mtbAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "strAIRSNumber " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                    "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                    ValidatingState = False
                    Exit Sub
                End If
            Else
                MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                 "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                ValidatingState = False
                Exit Sub
            End If
            If mtbFeeYear.Text = "" Then
                If IsNumeric(mtbFeeYear.Text) Then
                Else
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    ValidatingState = False
                    Exit Sub
                End If
            Else
                If mtbFeeYear.Text > feeyear + 1 Then
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    ValidatingState = False
                    Exit Sub
                End If
            End If
            If dtpBatchDepositDate.Text = "" Then
                MsgBox("Please enter a Deposit Date", MsgBoxStyle.OkOnly, "Incorrect Date")
                ValidatingState = False
                Exit Sub
            End If
            If txtDepositAmount.Text = "" Then
                MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
                ValidatingState = False
                Exit Sub
            End If
            If txtDepositNumber.Text = "" Then
                MsgBox("Please enter a Deposit Number", MsgBoxStyle.OkOnly, "Incorrect Deposit No.")
                ValidatingState = False
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DepositSearch()
        Try

            ds = New DataSet

            SQL = "Select " & _
             "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
             "strDepositNo, datPayDate, " & _
             "numPayment, intYear, " & _
             "strCheckNo, strBatchNo, " & _
             "strPayType, intPayId, " & _
             "strComments, strInvoiceNo " & _
             "from " & DBNameSpace & ".FSAddPaid " & _
             "where strDepositNo = '" & txtDepositNumber.Text & "' " & _
             "order by strBatchNo "

            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            da.Fill(ds, "Deposit")


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchDeposits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDeposits.Click
        Try
            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewInvoices()
        Try

            dsInvoice = New DataSet

            SQL = "Select " & _
            "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
            "strDepositNo, datPayDate, " & _
            "numPayment, intYear, " & _
            "strCheckNo, strBatchNo, " & _
            "strPayType, intPayId, " & _
            "strComments, strInvoiceNo " & _
            "from " & DBNameSpace & ".FSAddPaid " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and intYear = '" & mtbFeeYear.Text & "' " & _
            "order by strBatchNo "

            daInvoice = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daInvoice.Fill(dsInvoice, "Deposit")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub lblViewInvoices_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewInvoices.LinkClicked
        Try
            If mtbAIRSNumber.Text <> "" Then
                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvDeposits_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDeposits.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvDeposits.HitTest(e.X, e.Y)
            Dim temp As String

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvDeposits.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvDeposits.Columns(1).HeaderText

                    If dgvDeposits.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAIRSNumber.Text = dgvDeposits(0, hti.RowIndex).Value
                        LoadFacilityData(dgvDeposits(0, hti.RowIndex).Value)
                        lblAIRSNumber.Text = "AIRS #: " & dgvDeposits(0, hti.RowIndex).Value
                        txtDepositPayID.Text = dgvDeposits(8, hti.RowIndex).Value
                        If IsDBNull(dgvDeposits(1, hti.RowIndex).Value) Then
                            txtDepositNumber.Clear()
                        Else
                            txtDepositNumber.Text = dgvDeposits(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(2, hti.RowIndex).Value) Then
                            dtpBatchDepositDate.Text = OracleDate
                        Else
                            dtpBatchDepositDate.Text = dgvDeposits(2, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvDeposits(3, hti.RowIndex).Value) Then
                            txtDepositAmount.Clear()
                        Else
                            txtDepositAmount.Text = dgvDeposits(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(4, hti.RowIndex).Value) Then
                            mtbFeeYear.Clear()
                            lblFeeYear.Text = "Fee Year"
                        Else
                            mtbFeeYear.Text = dgvDeposits(4, hti.RowIndex).Value
                            lblFeeYear.Text = "Fee Year: " & dgvDeposits(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(5, hti.RowIndex).Value) Then
                            txtCheckNumber.Clear()
                        Else
                            txtCheckNumber.Text = dgvDeposits(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(6, hti.RowIndex).Value) Then
                            txtBatchNumber.Clear()
                        Else
                            txtBatchNumber.Text = dgvDeposits(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(7, hti.RowIndex).Value) Then
                            cboDepositPayType.Text = ""
                        Else
                            cboDepositPayType.Text = ""
                            cboDepositPayType.Text = dgvDeposits(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(9, hti.RowIndex).Value) Then
                            txtDepositComments.Clear()
                        Else
                            txtDepositComments.Text = dgvDeposits(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(10, hti.RowIndex).Value) Then
                            txtDepositInvoiceNo.Clear()
                        Else
                            txtDepositInvoiceNo.Text = dgvDeposits(10, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvInvoices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvInvoices.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvInvoices.HitTest(e.X, e.Y)
            Dim temp As String

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvInvoices.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvInvoices.Columns(1).HeaderText

                    If dgvInvoices.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAIRSNumber.Text = dgvInvoices(0, hti.RowIndex).Value
                        LoadFacilityData(dgvInvoices(0, hti.RowIndex).Value)
                        lblAIRSNumber.Text = "AIRS #: " & dgvInvoices(0, hti.RowIndex).Value
                        txtDepositPayID.Text = dgvInvoices(8, hti.RowIndex).Value
                        If IsDBNull(dgvInvoices(1, hti.RowIndex).Value) Then
                            txtDepositNumber.Clear()
                        Else
                            txtDepositNumber.Text = dgvInvoices(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(2, hti.RowIndex).Value) Then
                            dtpBatchDepositDate.Text = OracleDate
                        Else
                            dtpBatchDepositDate.Text = dgvInvoices(2, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvInvoices(3, hti.RowIndex).Value) Then
                            txtDepositAmount.Clear()
                        Else
                            txtDepositAmount.Text = dgvInvoices(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(4, hti.RowIndex).Value) Then
                            mtbFeeYear.Clear()
                            lblFeeYear.Text = "Fee Year"
                        Else
                            mtbFeeYear.Text = dgvInvoices(4, hti.RowIndex).Value
                            lblFeeYear.Text = "Fee Year: " & dgvInvoices(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(5, hti.RowIndex).Value) Then
                            txtCheckNumber.Clear()
                        Else
                            txtCheckNumber.Text = dgvInvoices(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(6, hti.RowIndex).Value) Then
                            txtBatchNumber.Clear()
                        Else
                            txtBatchNumber.Text = dgvInvoices(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(7, hti.RowIndex).Value) Then
                            cboDepositPayType.Text = ""
                        Else
                            cboDepositPayType.Text = ""
                            cboDepositPayType.Text = dgvInvoices(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(9, hti.RowIndex).Value) Then
                            txtDepositComments.Clear()
                        Else
                            txtDepositComments.Text = dgvInvoices(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(10, hti.RowIndex).Value) Then
                            txtDepositInvoiceNo.Clear()
                        Else
                            txtDepositInvoiceNo.Text = dgvInvoices(10, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewCheckDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewCheckDeposit.Click
        Try
            Dim InvoiceNumber As String = ""
            Dim PayId As String = ""

            ValidatingState = True
            ValidateData()
            If ValidatingState = True Then
                Select Case cboDepositPayType.Text
                    Case "ANNUAL"
                        InvoiceNumber = mtbAIRSNumber.Text & "-A1-" & mtbFeeYear.Text
                    Case "QUARTER ONE"
                        InvoiceNumber = mtbAIRSNumber.Text & "-Q1-" & mtbFeeYear.Text
                    Case "QUARTER TWO"
                        InvoiceNumber = mtbAIRSNumber.Text & "-Q2-" & mtbFeeYear.Text
                    Case "QUARTER THREE"
                        InvoiceNumber = mtbAIRSNumber.Text & "-Q3-" & mtbFeeYear.Text
                    Case "QUARTER FOUR"
                        InvoiceNumber = mtbAIRSNumber.Text & "-Q4-" & mtbFeeYear.Text
                    Case Else
                        InvoiceNumber = ""
                End Select

                If InvoiceNumber <> "" Then
                    SQL = "Select intPayID " & _
                    "from " & DBNameSpace & ".FSAddPaid " & _
                    "where strInvoiceNo = '" & Replace(InvoiceNumber, "'", "''") & "' " & _
                    "and datPayDate is null "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("intPayID")) Then
                            PayId = ""
                        Else
                            PayId = dr.Item("intPayID")
                        End If
                    End While
                    dr.Close()
                End If

                SQL = "Insert into " & DBNameSpace & ".FSAddPaid " & _
                "(strAIRSNumber, intYear, " & _
                "numPayment, datPayDate, " & _
                "strCheckNo, strDepositNo, " & _
                "strPayType, strBatchNo, " & _
                "strEntryPerson, strComments, " & _
                "intpayid, strInvoiceNo) " & _
                "values " & _
                "('0413" & mtbAIRSNumber.Text & "', " & CInt(mtbFeeYear.Text) & ", " & _
                "" & CDec(txtDepositAmount.Text) & ", '" & dtpBatchDepositDate.Text & "', " & _
                "'" & txtCheckNumber.Text & "', '" & txtDepositNumber.Text & "', " & _
                "'" & cboDepositPayType.Text & "', '" & txtBatchNumber.Text & "', " & _
                "'" & UserGCode & "', '" & Replace(txtDepositComments.Text, "'", "''") & "', " & _
                "" & DBNameSpace & ".seqFSDeposit.nextval, " & _
                "'" & InvoiceNumber & "') "

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If

                dr = cmd.ExecuteReader

                btnSearchDeposits.Enabled = False

                bgwDeposits.WorkerReportsProgress = True
                bgwDeposits.WorkerSupportsCancellation = True
                bgwDeposits.RunWorkerAsync()

                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()

                ClearForm()
                MsgBox("The record was added successfully", MsgBoxStyle.Information, "Entry Success!")

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateExistingDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateExistingDeposit.Click
        Try
            Dim InvoiceNumber As String = ""

            ValidatingState = True
            ValidateData()
            If ValidatingState = True Then

                If ValidatingState = True Then
                    Select Case cboDepositPayType.Text
                        Case "ANNUAL"
                            InvoiceNumber = mtbAIRSNumber.Text & "-A1-" & mtbFeeYear.Text
                        Case "QUARTER ONE"
                            InvoiceNumber = mtbAIRSNumber.Text & "-Q1-" & mtbFeeYear.Text
                        Case "QUARTER TWO"
                            InvoiceNumber = mtbAIRSNumber.Text & "-Q2-" & mtbFeeYear.Text
                        Case "QUARTER THREE"
                            InvoiceNumber = mtbAIRSNumber.Text & "-Q3-" & mtbFeeYear.Text
                        Case "QUARTER FOUR"
                            InvoiceNumber = mtbAIRSNumber.Text & "-Q4-" & mtbFeeYear.Text
                        Case Else
                            InvoiceNumber = ""
                    End Select

                    SQL = "Update " & DBNameSpace & ".FSAddPaid set strairsnumber = '0413" & mtbAIRSNumber.Text & "', " & _
                    "datPaydate = '" & dtpBatchDepositDate.Text & "', " & _
                    "numPayment = '" & CDec(txtDepositAmount.Text) & "', " & _
                    "strCheckno = '" & txtCheckNumber.Text & "', " & _
                    "strBatchno = '" & txtBatchNumber.Text & "', " & _
                    "strPaytype = '" & cboDepositPayType.Text & "', " & _
                    "strDepositno = '" & txtDepositNumber.Text & "', " & _
                    "intYear = '" & CInt(mtbFeeYear.Text) & "', " & _
                    "strComments = '" & Replace(txtDepositComments.Text, "'", "''") & "', " & _
                    "strInvoiceNo = '" & Replace(InvoiceNumber, "'", "''") & "', " & _
                    "strEntryPerson = '" & UserGCode & "' " & _
                    "where intpayid = '" & txtDepositPayID.Text & "'"

                    cmd = New OracleCommand(SQL, DBConn)

                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    btnSearchDeposits.Enabled = False

                    bgwDeposits.WorkerReportsProgress = True
                    bgwDeposits.WorkerSupportsCancellation = True
                    bgwDeposits.RunWorkerAsync()

                    lblViewInvoices.Enabled = False

                    bgwInvoices.WorkerReportsProgress = True
                    bgwInvoices.WorkerSupportsCancellation = True
                    bgwInvoices.RunWorkerAsync()

                    ClearForm()
                    MsgBox("The record has been updated successfully", MsgBoxStyle.Information, "Update Success!")
                Else
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteCheckDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCheckDeposit.Click
        Try

            Dim Result As DialogResult
            Result = MessageBox.Show("Are you sure you want to remove this record?", _
              "PASP Fee Tool", MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case Windows.Forms.DialogResult.Yes
                    SQL = "Delete from " & DBNameSpace & ".FSAddPaid " & _
                    "where intpayid = '" & txtDepositPayID.Text & "'"

                    cmd = New OracleCommand(SQL, DBConn)
                    cmd.CommandType = CommandType.Text
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If

                    dr = cmd.ExecuteReader

                    btnSearchDeposits.Enabled = False

                    bgwDeposits.WorkerReportsProgress = True
                    bgwDeposits.WorkerSupportsCancellation = True
                    bgwDeposits.RunWorkerAsync()

                    lblViewInvoices.Enabled = False

                    bgwInvoices.WorkerReportsProgress = True
                    bgwInvoices.WorkerSupportsCancellation = True
                    bgwInvoices.RunWorkerAsync()

                    ClearForm()
                    MsgBox("The record has been deleted successfully", MsgBoxStyle.Information, "Delete Success!")

                Case Else
                    Exit Sub
            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwDeposits_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwDeposits.DoWork
        Try

            DepositSearch()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwDeposits_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDeposits.RunWorkerCompleted
        Try
            dgvDeposits.DataSource = ds
            dgvDeposits.DataMember = "Deposit"

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
            dgvDeposits.Columns("datpaydate").HeaderText = "Deposit Date"
            dgvDeposits.Columns("datpaydate").DisplayIndex = 2
            dgvDeposits.Columns("datpaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvDeposits.Columns("numpayment").HeaderText = "Payment"
            dgvDeposits.Columns("numpayment").DisplayIndex = 3
            dgvDeposits.Columns("numpayment").DefaultCellStyle.Format = "c"
            dgvDeposits.Columns("intyear").HeaderText = "Year"
            dgvDeposits.Columns("intyear").DisplayIndex = 4
            dgvDeposits.Columns("strcheckno").HeaderText = "Check Number"
            dgvDeposits.Columns("strcheckno").DisplayIndex = 5
            dgvDeposits.Columns("strbatchno").HeaderText = "Batch Number"
            dgvDeposits.Columns("strbatchno").DisplayIndex = 6
            dgvDeposits.Columns("strpaytype").HeaderText = "Payment Type"
            dgvDeposits.Columns("strpaytype").DisplayIndex = 7
            dgvDeposits.Columns("intpayid").HeaderText = "Pay ID"
            dgvDeposits.Columns("intpayid").DisplayIndex = 8
            dgvDeposits.Columns("intpayid").Visible = False
            dgvDeposits.Columns("strcomments").HeaderText = "Comments"
            dgvDeposits.Columns("strcomments").DisplayIndex = 9
            dgvDeposits.Columns("strcomments").Visible = False
            dgvDeposits.Columns("strinvoiceno").HeaderText = "Invoce Number"
            dgvDeposits.Columns("strinvoiceno").DisplayIndex = 10
            dgvDeposits.Columns("strinvoiceno").Visible = True

            btnSearchDeposits.Enabled = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwInvoices_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwInvoices.DoWork
        Try

            ViewInvoices()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwInvoices_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInvoices.RunWorkerCompleted
        Try
            dgvInvoices.DataSource = dsInvoice
            dgvInvoices.DataMember = "Deposit"

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
            dgvInvoices.Columns("datpaydate").HeaderText = "Deposit Date"
            dgvInvoices.Columns("datpaydate").DisplayIndex = 2
            dgvInvoices.Columns("datpaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvInvoices.Columns("numpayment").HeaderText = "Payment"
            dgvInvoices.Columns("numpayment").DisplayIndex = 3
            dgvInvoices.Columns("numpayment").DefaultCellStyle.Format = "c"
            dgvInvoices.Columns("intyear").HeaderText = "Year"
            dgvInvoices.Columns("intyear").DisplayIndex = 4
            dgvInvoices.Columns("strcheckno").HeaderText = "Check Number"
            dgvInvoices.Columns("strcheckno").DisplayIndex = 5
            dgvInvoices.Columns("strbatchno").HeaderText = "Batch Number"
            dgvInvoices.Columns("strbatchno").DisplayIndex = 6
            dgvInvoices.Columns("strpaytype").HeaderText = "Payment Type"
            dgvInvoices.Columns("strpaytype").DisplayIndex = 7
            dgvInvoices.Columns("intpayid").HeaderText = "Pay ID"
            dgvInvoices.Columns("intpayid").DisplayIndex = 8
            dgvInvoices.Columns("intpayid").Visible = False
            dgvInvoices.Columns("strcomments").HeaderText = "Comments"
            dgvInvoices.Columns("strcomments").DisplayIndex = 9
            dgvInvoices.Columns("strcomments").Visible = False
            dgvInvoices.Columns("strinvoiceno").HeaderText = "Invoce Number"
            dgvInvoices.Columns("strinvoiceno").DisplayIndex = 10
            dgvInvoices.Columns("strinvoiceno").Visible = True

            lblViewInvoices.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearForm()
        Try
            txtCheckNumber.Clear()
            mtbAIRSNumber.Clear()
            mtbFeeYear.Clear()
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cboDepositPayType.Text = ""
            lblFeeYear.Text = "Fee Year"
            txtDepositAmount.Clear()
            txtDepositInvoiceNo.Clear()
            txtDepositPayID.Clear()
            txtDepositComments.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        Try
            ClearForm()
            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

            lblViewInvoices.Enabled = False

            bgwInvoices.WorkerReportsProgress = True
            bgwInvoices.WorkerSupportsCancellation = True
            bgwInvoices.RunWorkerAsync()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
   

  
End Class