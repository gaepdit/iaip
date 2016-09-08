Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports Iaip.SharedData

Public Class PASPFeeStatistics
    Dim SQL, SQL2 As String
    Dim cmd As SqlCommand
    Dim dr, dr2 As SqlDataReader
    Dim dsViewCount As DataSet
    Dim daViewCount As SqlDataAdapter
    Dim ds As DataSet
    Dim da As SqlDataAdapter
    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    Dim crParameterFieldDefinition As ParameterFieldDefinition
    Dim crParameterValues As New ParameterValues
    Dim crParameterDiscreteValue As New ParameterDiscreteValue
    Dim rpt As ReportClass

    Private Sub DEVFeeStatistics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            pnlDetails.Dock = DockStyle.None

            If AccountFormAccess(12, 3) <> "1" Then
                TCMailoutAndStats.TabPages.Remove(TPNonRespondersReport)
            End If

            loadDepositAndPayment()

            dtpStartDepositDate.Value = Today.AddMonths(-1)
            dtpEndDepositDate.Value = Today
            dtpDepositReportStartDate.Value = Today.AddMonths(-1)
            dtpDepositReportEndDate.Value = Today
            dtpStartDepositDate.Enabled = False
            dtpEndDepositDate.Enabled = False
            chbDepositDateSearch.Checked = False
            btnRunDepositReport.Enabled = False

            LoadComboBoxesF()

            mtbFacilityBalanceYear.Text = Today.Year.ToString

            cboFeeStatYear.Text = cboFeeStatYear.Items.Item(0)

            If AccountFormAccess(135, 1) = "1" Or AccountFormAccess(135, 2) = "1" Or AccountFormAccess(135, 3) = "1" Or AccountFormAccess(135, 4) = "1" Then
                btnOpenFeesLog.Visible = True
                txtFeeStatAirsNumber.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub loadDepositAndPayment()
        Try
            Dim Year As String

            Dim SQL As String = "Select distinct(numFeeYear) as FeeYear " &
            "from FS_Admin " &
            "order by numFeeYear desc "
            Dim dt As DataTable = DB.GetDataTable(SQL)

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("FeeYear")) Then
                    Year = Today.Year.ToString
                Else
                    Year = dr.Item("FeeYear")
                End If

                If cboStatYear.Items.Contains(Year) Then
                Else
                    cboStatYear.Items.Add(Year)
                End If

                If cboFeeStatYear.Items.Contains(Year) Then
                Else
                    cboFeeStatYear.Items.Add(Year)
                End If

                If TCMailoutAndStats.TabPages.Contains(TPNonRespondersReport) Then
                    cboFeeYear.Items.Add(Year)
                End If
            Next

            cboStatPayType.Items.Add("ALL")
            cboStatPayType.Items.Add("ALL QUARTERS")

            SQL = "Select strPayTypeDesc " &
            "from FSLK_PayType " &
            "where Active = '1' " &
            "order by numPaytypeID "
            Dim dt2 As DataTable = DB.GetDataTable(SQL)

            For Each dr As DataRow In dt2.Rows
                cboStatPayType.Items.Add(dr.Item("strPayTypeDesc"))
            Next

            cboStatYear.Text = cboStatYear.Items.Item(0)
            cboStatPayType.Text = cboStatPayType.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewDepositsStats_Click(sender As Object, e As EventArgs) Handles btnViewDepositsStats.Click
        Try
            Dim SQLReported As String = ""
            Dim SQLInvoiced As String = ""
            Dim SQLPaid As String = ""

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin  " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "and FS_Admin.Active = '1' " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice, FS_Admin  " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "Select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions " &
                    "where numFeeYear = '" & cboStatYear.Text & "' " &
                    "and Active = '1' "

                Case "ANNUAL"
                    SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Entire Annual Year' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice, FS_Admin " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeInvoice.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeyear = FS_Admin.numFeeyear " &
                    "and FS_admin.active = '1' " &
                    "and FS_FeeInvoice.strPayType = '1'  " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '1' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "


                Case "ALL QUARTERS"
                    SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice, FS_Admin " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                     "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and FS_FeeInvoice.strPayType <> '1'  " &
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' " &
                    "and (FS_FeeInvoice.strPayType = '2' " &
                    "or FS_FeeInvoice.strPayType = '3' " &
                    "or FS_FeeInvoice.strPayType = '4' " &
                    "or FS_FeeInvoice.strPayType = '5') "

                Case "QUARTER ONE"
                    SQLReported = "Select sum(numtotalFee/4) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                       "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice,  FS_Admin " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                      "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '2'  "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '2' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "

                Case "QUARTER TWO"
                    SQLReported = "Select sum(numtotalFee/4) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice, FS_Admin " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '3'   "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '3' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "

                Case "QUARTER THREE"
                    SQLReported = "Select sum(numtotalFee/4) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice, FS_Admin  " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '4'   "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '4' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "

                Case "QUARTER FOUR"
                    SQLReported = "Select sum(numtotalFee/4 ) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin  " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                   "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice, FS_Admin  " &
                    "where FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '5'  "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '5' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "

                Case "AMENDMENT"
                    SQLReported = "Select sum(numtotalFee ) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin  " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan is null "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '6' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "
                Case "ONE-TIME"
                    SQLReported = "Select sum(numtotalFee ) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin  " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan is null "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '8' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "
                Case "REFUND"
                    SQLReported = "Select sum(0) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '7' " &
                    "and FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " &
                    "and FS_Transactions.active = '1' "
                Case Else
                    SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData, FS_Admin " &
                    "where FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeAuditedData.nuMFeeYear = FS_Admin.numFeeyear " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "Select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions " &
                    "where numFeeYear = '" & cboStatYear.Text & "' " &
                    "and Active = '1' "
            End Select
            If SQLReported <> "" Then
                cmd = New SqlCommand(SQLReported, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("TotalDue")) Then
                        txtTotalPaymentDue.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaymentDue.Text = CDec(dr.Item("TotalDue")).ToString("c")
                    End If
                End While
                dr.Close()
            Else
                txtTotalPaymentDue.Text = CDec("0").ToString("c")
            End If

            If SQLInvoiced <> "" Then
                cmd = New SqlCommand(SQLInvoiced, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("TotalInvoiced")) Then
                        txtTotalPaymentInvoiced.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaymentInvoiced.Text = CDec(dr.Item("TotalInvoiced")).ToString("c")
                    End If
                End While
                dr.Close()
            Else
                txtTotalPaymentInvoiced.Text = CDec("0").ToString("c")
            End If

            If SQLPaid <> "" Then
                cmd = New SqlCommand(SQLPaid, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("TotalPaid")) Then
                        txtTotalPaid.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaid.Text = CDec(dr.Item("TotalPaid")).ToString("c")
                    End If
                End While
                dr.Close()
            Else
                txtTotalPaid.Text = CDec("0").ToString("c")
            End If

            'If SQLPaidInvoiced <> "" Then
            '    cmd = New SqlCommand(SQLPaidInvoiced, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    dr = cmd.ExecuteReader
            '    While dr.Read
            '        If IsDBNull(dr.Item("TotalInvoicedPaid")) Then
            '            txtTotalInvoicedPaid.Text = CDec("0").ToString("c")
            '        Else
            '            txtTotalInvoicedPaid.Text = CDec(dr.Item("TotalInvoicedPaid")).ToString("c")
            '        End If
            '    End While
            '    dr.Close()
            'Else
            '    txtTotalInvoicedPaid.Text = CDec("0").ToString("c")
            'End If

            txtBalance.Text = CDec(txtTotalPaymentDue.Text) - CDec(txtTotalPaid.Text)
            txtBalance.Text = CDec(txtBalance.Text).ToString("c")

            txtInvoicedBalance.Text = CDec(txtTotalPaymentInvoiced.Text) - CDec(txtTotalPaid.Text)
            txtInvoicedBalance.Text = CDec(txtInvoicedBalance.Text).ToString("c")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewPaymentDue_Click(sender As Object, e As EventArgs) Handles btnViewPaymentDue.Click
        Try
            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select  " &
                    "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee ) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation, FS_FeeAuditedData, " &
                    "FS_Admin " &
                    "where APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "and FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' "
                Case "ANNUAL"
                    SQL = "select  " &
                    "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation, FS_FeeAuditedData, " &
                    "FS_Admin " &
                    "where APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "and FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strPaymentPlan = 'Entire Annual Year' "
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    SQL = "select  " &
                    "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee)/4 as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation, FS_FeeAuditedData, " &
                    "FS_Admin " &
                    "where APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "and FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strPaymentPlan = 'Four Quarterly Payments' "
                Case "AMENDMENT", "ONE-TIME", "REFUND"
                    SQL = "select  " &
                    "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee)/4 as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation, FS_FeeAuditedData, " &
                    "FS_Admin " &
                    "where APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "and FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                     "and FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strPaymentPlan is null "
                Case Else
                    SQL = "select  " &
                    "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation, FS_FeeAuditedData, " &
                    "FS_Admin " &
                    "where APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                     "and FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' "
            End Select

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "PaymentDue")
            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPaymentPlan").HeaderText = "Payment Plan"
            dgvDepositsAndPayments.Columns("strPaymentPlan").DisplayIndex = 2
            Select Case cboStatPayType.Text
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    dgvDepositsAndPayments.Columns("Due").HeaderText = "Amount Reported per Quarter"
                Case Else
                    dgvDepositsAndPayments.Columns("Due").HeaderText = "Amount Reported"
            End Select
            dgvDepositsAndPayments.Columns("Due").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("Due").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4

            dgvDepositsAndPayments.Columns("strClass").HeaderText = "Classification"
            dgvDepositsAndPayments.Columns("strClass").DisplayIndex = 5

            dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
            dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 7
            dgvDepositsAndPayments.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 8
            dgvDepositsAndPayments.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
            dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 9
            dgvDepositsAndPayments.Columns("numTotalFee").DefaultCellStyle.Format = "c"

            dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
            dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 10
            dgvDepositsAndPayments.Columns("numAdminFee").DefaultCellStyle.Format = "c"

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bntViewTotalPaid_Click(sender As Object, e As EventArgs) Handles bntViewTotalPaid.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " &
                      "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                      "strFacilityName, " &
                      "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                      "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                      "FS_Transactions.numFeeYear, numPart70Fee, " &
                      "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                      "numAdminFee, (numTotalFee) as Due " &
                      "From APBFacilityInformation, FS_Transactions, " &
                      "FS_FeeAuditedData, FS_FeeInvoice, " &
                      "FSLK_PayType " &
                      "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                      "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
                      "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                      "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                      "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                      "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                      "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' "
                Case "ANNUAL"
                    SQL = "select " &
                       "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                       "strFacilityName, " &
                       "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                       "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                       "FS_Transactions.numFeeYear, numPart70Fee, " &
                       "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                       "numAdminFee, (numTotalFee) as Due " &
                       "From APBFacilityInformation, FS_Transactions, " &
                       "FS_FeeAuditedData, FS_FeeInvoice, " &
                      "FSLK_PayType " &
                       "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                       "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                       "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                        "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                      "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                      "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                       "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                       "and strPaymentPlan = 'Entire Annual Year' "
                Case "QUARTER ONE"
                    SQL = "select " &
                "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                "strFacilityName, " &
                "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                "FS_Transactions.numFeeYear, numPart70Fee, " &
                "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                "numAdminFee, (numTotalFee) as Due " &
                "From APBFacilityInformation, FS_Transactions, " &
                  "FS_FeeAuditedData, FS_FeeInvoice, " &
                  "FSLK_PayType " &
                "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                 "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                  "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                  "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                "and numPayTypeID = '2' "
                Case "QUARTER TWO"
                    SQL = "select " &
                   "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                   "strFacilityName, " &
                   "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                   "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                   "FS_Transactions.numFeeYear, numPart70Fee, " &
                   "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                   "numAdminFee, (numTotalFee) as Due " &
                   "From APBFacilityInformation, FS_Transactions, " &
                    "FS_FeeAuditedData, FS_FeeInvoice, " &
                    "FSLK_PayType " &
                   "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                   "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                   "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                    "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                   "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                   "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                   "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                   "and numPayTypeID = '3' "
                Case "QUARTER THREE"
                    SQL = "select " &
           "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
           "strFacilityName, " &
           "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
           "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
           "FS_Transactions.numFeeYear, numPart70Fee, " &
           "numSMFee, numNSPSFee, numTotalFee, strClass, " &
           "numAdminFee, (numTotalFee) as Due " &
           "From APBFacilityInformation, FS_Transactions, " &
             "FS_FeeAuditedData, FS_FeeInvoice, " &
             "FSLK_PayType " &
           "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
           "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
           "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
             "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
             "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
           "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
           "and numPayTypeID = '4' "
                Case "QUARTER FOUR"
                    SQL = "select " &
           "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
           "strFacilityName, " &
           "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
           "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
           "FS_Transactions.numFeeYear, numPart70Fee, " &
           "numSMFee, numNSPSFee, numTotalFee, strClass, " &
           "numAdminFee, (numTotalFee) as Due " &
           "From APBFacilityInformation, FS_Transactions, " &
             "FS_FeeAuditedData, FS_FeeInvoice, " &
             "FSLK_PayType " &
           "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
           "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
           "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
             "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
             "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
           "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
           "and numPayTypeID = '5' "

                Case "ALL QUARTERS"
                    SQL = "select " &
                    "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                    "strFacilityName, " &
                    "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                    "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                    "FS_Transactions.numFeeYear, numPart70Fee, " &
                    "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                    "numAdminFee, (numTotalFee) as Due " &
                    "From APBFacilityInformation, FS_Transactions, " &
                      "FS_FeeAuditedData, FS_FeeInvoice, " &
                      "FSLK_PayType " &
                    "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                    "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                    "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                     "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                      "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                      "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                    "and strPaymentPlan = 'Four Quarterly Payments' "
                Case "AMENDMENT"
                    SQL = "select " &
                     "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                     "strFacilityName, " &
                     "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                     "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                     "FS_Transactions.numFeeYear, numPart70Fee, " &
                     "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                     "numAdminFee, (numTotalFee) as Due " &
                     "From APBFacilityInformation, FS_Transactions, " &
                       "FS_FeeAuditedData, FS_FeeInvoice, " &
                       "FSLK_PayType " &
                     "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                     "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                     "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                      "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                       "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                       "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                     "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                     "and numPayTypeID = '6' "


                Case "ONE-TIME"
                    SQL = "select " &
                   "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                   "strFacilityName, " &
                   "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                   "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                   "FS_Transactions.numFeeYear, numPart70Fee, " &
                   "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                   "numAdminFee, (numTotalFee) as Due " &
                   "From APBFacilityInformation, FS_Transactions, " &
                     "FS_FeeAuditedData, FS_FeeInvoice, " &
                     "FSLK_PayType " &
                   "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                   "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                   "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                    "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                     "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                     "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                   "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                   "and numPayTypeID = '8' "

                Case "REFUND"
                    SQL = "select " &
                   "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                   "strFacilityName, " &
                   "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                   "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                   "FS_Transactions.numFeeYear, numPart70Fee, " &
                   "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                   "numAdminFee, (numTotalFee) as Due " &
                   "From APBFacilityInformation, FS_Transactions, " &
                     "FS_FeeAuditedData, FS_FeeInvoice, " &
                     "FSLK_PayType " &
                   "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                   "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                   "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                    "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                     "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                     "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                   "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " &
                   "and numPayTypeID = '7' "

                Case Else
                    SQL = "select " &
                     "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " &
                     "strFacilityName, " &
                     "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                     "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                     "FS_Transactions.numFeeYear, numPart70Fee, " &
                     "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                     "numAdminFee, (numTotalFee) as Due " &
                     "From APBFacilityInformation, FS_Transactions, " &
                       "FS_FeeAuditedData, FS_FeeInvoice, " &
                       "FSLK_PayType " &
                     "where APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                     "and FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
                     "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear (+) " &
                      "and FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid  (+) " &
                       "and FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                       "and FS_Transactions.Active = '1' " &
                      "and FS_FeeAuditedData.Active = '1' " &
                      "and FS_FeeInvoice.Active = '1' " &
                     "and FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' "
            End Select

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "PaymentDue")
            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPaymentPlan").HeaderText = "Payment Plan"
            dgvDepositsAndPayments.Columns("strPaymentPlan").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("strPayTypedesc").HeaderText = "Invoice Type"
            dgvDepositsAndPayments.Columns("strPayTypedesc").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("numPayment").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("numPayment").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("numPayment").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("strDepositNo").HeaderText = "Deposit #"
            dgvDepositsAndPayments.Columns("strDepositNo").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("datTransactionDate").HeaderText = "Pay Date"
            dgvDepositsAndPayments.Columns("datTransactionDate").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvDepositsAndPayments.Columns("strCheckNo").HeaderText = "Check #"
            dgvDepositsAndPayments.Columns("strCheckNo").DisplayIndex = 7
            dgvDepositsAndPayments.Columns("InvoiceID").HeaderText = "Invoice #"
            dgvDepositsAndPayments.Columns("InvoiceID").DisplayIndex = 8
            dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 9
            dgvDepositsAndPayments.Columns("strClass").HeaderText = "Classification"
            dgvDepositsAndPayments.Columns("strClass").DisplayIndex = 10
            dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 11
            dgvDepositsAndPayments.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
            dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 12
            dgvDepositsAndPayments.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 13
            dgvDepositsAndPayments.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
            dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 14
            dgvDepositsAndPayments.Columns("numTotalFee").DefaultCellStyle.Format = "c"

            dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
            dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 15
            dgvDepositsAndPayments.Columns("numAdminFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("Due").HeaderText = "Total Due"
            dgvDepositsAndPayments.Columns("Due").DisplayIndex = 16
            dgvDepositsAndPayments.Columns("Due").DefaultCellStyle.Format = "c"

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewBalance_Click(sender As Object, e As EventArgs) Handles btnViewBalance.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select * from " &
                    "(select " &
                    "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                    "STRFACILITYNAME, " &
                    "TOTALDUE.INTYEAR, strPaymentPlan, " &
                    "case " &
                    "when TotalDue is null then 0 " &
                    "else TotalDue " &
                    "end totalDue, " &
                    "case " &
                    "when TotalPaid is null then 0 " &
                    "else TotalPaid " &
                    "end TotalPaid, " &
                    "case " &
                    "when TotalDue is null and TotalPaid is null then 0 " &
                    " when totaldue is null and totalpaid is not null then -totalpaid " &
                    "when totaldue is not null and totalpaid is null then totaldue " &
                    "else (TOTALDUE - TOTALPAID) " &
                    "end Balance " &
                    "from (select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE) as TOTALDUE " &
                    "from  fs_feeAuditedData where active = '1') TOTALDUE,  " &
                    "( " &
                    "  SELECT  " &
                    " STRAIRSNUMBER, INTYEAR, " &
                    " CASE " &
                    " WHEN TOTALPAID IS NULL THEN 0 " &
                    " ELSE TOTALPAID " &
                    " END TOTALPAID " &
                    " FROM " &
                    " ( " &
                    " select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
                    " from fs_Transactions, fs_feeAuditedData   " &
                    " where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
                    " and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
                    " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
                    " group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
                    " UNION " &
                    "  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
                    " from fs_Transactions, fs_feeAuditedData   " &
                    " where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
                    " and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
                    " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
                    " group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
                    "  )) TOTALPAID, " &
                    " APBFACILITYINFORMATION, " &
                    "fs_feeAuditedData   " &
                    "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                    "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                    "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                    "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "


                Case "ANNUAL"
                    SQL = "select * from " &
                   "(select " &
                   "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                   "STRFACILITYNAME, " &
                   "TOTALDUE.INTYEAR, strPaymentPlan, " &
                   "case " &
                   "when TotalDue is null then 0 " &
                   "else TotalDue " &
                   "end totalDue, " &
                   "case " &
                   "when TotalPaid is null then 0 " &
                   "else TotalPaid " &
                   "end TotalPaid, " &
                   "case " &
                   "when TotalDue is null and TotalPaid is null then 0 " &
                   " when totaldue is null and totalpaid is not null then -totalpaid " &
                   "when totaldue is not null and totalpaid is null then totaldue " &
                   "else (TOTALDUE - TOTALPAID) " &
                   "end Balance " &
                   "from (select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE) as TOTALDUE " &
                   "from  fs_feeAuditedData where active = '1') TOTALDUE,  " &
                   "( " &
                   "  SELECT  " &
                   " STRAIRSNUMBER, INTYEAR, " &
                   " CASE " &
                   " WHEN TOTALPAID IS NULL THEN 0 " &
                   " ELSE TOTALPAID " &
                   " END TOTALPAID " &
                   " FROM " &
                   " ( " &
                   " select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
                   " from fs_Transactions, fs_feeAuditedData   " &
                   " where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
                   " and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
                   " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
                   " group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
                   " UNION " &
                   "  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
                   " from fs_Transactions, fs_feeAuditedData   " &
                   " where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
                   " and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
                   " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
                   " group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
                   "  )) TOTALPAID, " &
                   " APBFACILITYINFORMATION, " &
                   "fs_feeAuditedData   " &
                   "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                   "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                   "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                   "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                   "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                   "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                   "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                   "and strPaymentPlan = 'Entire Annual Year' )"
                Case "ALL QUARTERS"
                    SQL = "select * from " &
                     "(select " &
                     "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                     "STRFACILITYNAME, " &
                     "TOTALDUE.INTYEAR, strPaymentPlan, " &
                     "case " &
                     "when TotalDue is null then 0 " &
                     "else TotalDue " &
                     "end totalDue, " &
                     "case " &
                     "when TotalPaid is null then 0 " &
                     "else TotalPaid " &
                     "end TotalPaid, " &
                     "case " &
                     "when TotalDue is null and TotalPaid is null then 0 " &
                     " when totaldue is null and totalpaid is not null then -totalpaid " &
                     "when totaldue is not null and totalpaid is null then totaldue " &
                     "else (TOTALDUE - TOTALPAID) " &
                     "end Balance " &
                   "from (select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE) as TOTALDUE " &
                   "from  fs_feeAuditedData where active = '1') TOTALDUE,  " &
                     "( " &
 "  SELECT  " &
 " STRAIRSNUMBER, INTYEAR, " &
 " CASE " &
 " WHEN TOTALPAID IS NULL THEN 0 " &
 " ELSE TOTALPAID " &
 " END TOTALPAID " &
 " FROM " &
 " ( " &
 " select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
 " from fs_Transactions, fs_feeAuditedData   " &
 " where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
 " and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
 " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
 " group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
 " UNION " &
 "  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
 " from fs_Transactions, fs_feeAuditedData   " &
 " where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
 " and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
 " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
 " group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
 "  )) TOTALPAID, " &
 " APBFACILITYINFORMATION, " &
                   "fs_feeAuditedData   " &
                   "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                   "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                   "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                   "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                   "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                   "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                   "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                   "and strPaymentPlan = 'Four Quarterly Payments') "

                Case "QUARTER ONE"
                    SQL = "select * from " &
                    "(select " &
                    "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                    "STRFACILITYNAME, " &
                    "TOTALDUE.INTYEAR, strPaymentPlan, " &
                    "case " &
                    "when TotalDue is null then 0 " &
                    "else TotalDue " &
                    "end totalDue, " &
                    "case " &
                    "when TotalPaid is null then 0 " &
                    "else TotalPaid " &
                    "end TotalPaid, " &
                    "case " &
                    "when TotalDue is null and TotalPaid is null then 0 " &
                    " when totaldue is null and totalpaid is not null then -totalpaid " &
                    "when totaldue is not null and totalpaid is null then totaldue " &
                    "else (TOTALDUE - TOTALPAID) " &
                    "end Balance " &
                  "from " &
                  "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                  "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
             "( " &
"  SELECT  " &
" STRAIRSNUMBER, INTYEAR, " &
" CASE " &
" WHEN TOTALPAID IS NULL THEN 0 " &
" ELSE TOTALPAID " &
" END TOTALPAID " &
" FROM " &
" ( " &
" select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
" from fs_Transactions, fs_feeAuditedData, fs_feeinvoice " &
" where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
" and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
"and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
"and strPayType = '2' " &
" and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
" group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
" UNION " &
"  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
" from fs_Transactions, fs_feeAuditedData, fs_feeinvoice    " &
" where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
" and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
"and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
" and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
"and strPayType = '2' " &
" group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
"  )) TOTALPAID, " &
" APBFACILITYINFORMATION, " &
                  "  fs_feeAuditedData  " &
                  "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                  "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                  "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                  "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                  "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                  "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                  "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "

                Case "QUARTER TWO"
                    SQL = "select * from " &
                     "(select " &
                     "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                     "STRFACILITYNAME, " &
                     "TOTALDUE.INTYEAR, strPaymentPlan, " &
                     "case " &
                     "when TotalDue is null then 0 " &
                     "else TotalDue " &
                     "end totalDue, " &
                     "case " &
                     "when TotalPaid is null then 0 " &
                     "else TotalPaid " &
                     "end TotalPaid, " &
                     "case " &
                     "when TotalDue is null and TotalPaid is null then 0 " &
                     " when totaldue is null and totalpaid is not null then -totalpaid " &
                     "when totaldue is not null and totalpaid is null then totaldue " &
                     "else (TOTALDUE - TOTALPAID) " &
                     "end Balance " &
                        "from " &
                        "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                        "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
                   "( " &
      "  SELECT  " &
      " STRAIRSNUMBER, INTYEAR, " &
      " CASE " &
      " WHEN TOTALPAID IS NULL THEN 0 " &
      " ELSE TOTALPAID " &
      " END TOTALPAID " &
      " FROM " &
      " ( " &
      " select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
      " from fs_Transactions, fs_feeAuditedData, fs_feeinvoice " &
      " where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
      " and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
      "and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
      "and strPayType = '3' " &
      " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
      " group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
      " UNION " &
      "  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
      " from fs_Transactions, fs_feeAuditedData, fs_feeinvoice    " &
      " where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
      " and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
      "and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
      "and strPayType = '3' " &
      " and fs_Transactions.active = '1' and fs_feeAuditeddata.active = '1' " &
      " group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
      "  )) TOTALPAID, " &
      " APBFACILITYINFORMATION, " &
                        "  fs_feeAuditedData  " &
                        "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                        "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                        "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                        "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                        "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                        "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                        "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "

                Case "QUARTER THREE"
                    SQL = "select * from " &
                        "(select " &
                        "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                        "STRFACILITYNAME, " &
                        "TOTALDUE.INTYEAR, strPaymentPlan, " &
                        "case " &
                        "when TotalDue is null then 0 " &
                        "else TotalDue " &
                        "end totalDue, " &
                        "case " &
                        "when TotalPaid is null then 0 " &
                        "else TotalPaid " &
                        "end TotalPaid, " &
                        "case " &
                        "when TotalDue is null and TotalPaid is null then 0 " &
                        " when totaldue is null and totalpaid is not null then -totalpaid " &
                        "when totaldue is not null and totalpaid is null then totaldue " &
                        "else (TOTALDUE - TOTALPAID) " &
                        "end Balance " &
                                     "from " &
                                     "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                                     "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
                                "( " &
                   "  SELECT  " &
                   " STRAIRSNUMBER, INTYEAR, " &
                   " CASE " &
                   " WHEN TOTALPAID IS NULL THEN 0 " &
                   " ELSE TOTALPAID " &
                   " END TOTALPAID " &
                   " FROM " &
                   " ( " &
                   " select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
                   " from fs_Transactions, fs_feeAuditedData, fs_feeinvoice " &
                   " where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
                   " and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
                   "and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
                   "and strPayType = '4' " &
                   " group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
                   " UNION " &
                   "  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
                   " from fs_Transactions, fs_feeAuditedData, fs_feeinvoice    " &
                   " where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
                   " and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
                   "and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
                   "and strPayType = '4' " &
                   " group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
                   "  )) TOTALPAID, " &
                   " APBFACILITYINFORMATION, " &
                                     "  fs_feeAuditedData  " &
                                     "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                                     "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                                     "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                                     "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "

                Case "QUARTER FOUR"
                    SQL = "select * from " &
                   "(select " &
                   "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                   "STRFACILITYNAME, " &
                   "TOTALDUE.INTYEAR, strPaymentPlan, " &
                   "case " &
                   "when TotalDue is null then 0 " &
                   "else TotalDue " &
                   "end totalDue, " &
                   "case " &
                   "when TotalPaid is null then 0 " &
                   "else TotalPaid " &
                   "end TotalPaid, " &
                   "case " &
                   "when TotalDue is null and TotalPaid is null then 0 " &
                   " when totaldue is null and totalpaid is not null then -totalpaid " &
                   "when totaldue is not null and totalpaid is null then totaldue " &
                   "else (TOTALDUE - TOTALPAID) " &
                   "end Balance " &
                  "from " &
                  "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                  "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
             "( " &
"  SELECT  " &
" STRAIRSNUMBER, INTYEAR, " &
" CASE " &
" WHEN TOTALPAID IS NULL THEN 0 " &
" ELSE TOTALPAID " &
" END TOTALPAID " &
" FROM " &
" ( " &
" select fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
" from fs_Transactions, fs_feeAuditedData, fs_feeinvoice " &
" where fs_Transactions.STRAIRSNUMBER = fs_feeAuditedData.STRAIRSNUMBER (+) " &
" and fs_Transactions.numfeeyear = fs_feeAuditedData.numfeeyear   (+) " &
"and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
"and strPayType = '5' " &
" group by fs_Transactions.STRAIRSNUMBER, fs_Transactions.numfeeyear" &
" UNION " &
"  select fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " &
" from fs_Transactions, fs_feeAuditedData, fs_feeinvoice    " &
" where fs_feeAuditedData.STRAIRSNUMBER = fs_Transactions.STRAIRSNUMBER (+) " &
" and fs_feeAuditedData.numfeeyear = fs_Transactions.numfeeyear   (+) " &
"and Fs_transactions.invoiceid = fs_feeinvoice.invoiceid (+) " &
"and strPayType = '5' " &
" group by fs_feeAuditedData.STRAIRSNUMBER, fs_feeAuditedData.numfeeyear" &
"  )) TOTALPAID, " &
" APBFACILITYINFORMATION, " &
                  "  fs_feeAuditedData  " &
                  "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                  "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                  "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                  "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " &
                  "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " &
                  "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                  "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "
                Case "AMENDMENT"
                    SQL = "select * from " &
                    "(select " &
                    "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                    "STRFACILITYNAME, " &
                    "TOTALDUE.INTYEAR, strPaymentPlan, " &
                    "case " &
                    "when TotalDue is null then 0 " &
                    "else TotalDue " &
                    "end totalDue, " &
                    "case " &
                    "when TotalPaid is null then 0 " &
                    "else TotalPaid " &
                    "end TotalPaid, " &
                    "case " &
                    "when TotalDue is null and TotalPaid is null then 0 " &
                    " when totaldue is null and totalpaid is not null then -totalpaid " &
                    "when totaldue is not null and totalpaid is null then totaldue " &
                    "else (TOTALDUE - TOTALPAID) " &
                    "end Balance " &
                    "from " &
                    "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                    "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
                    "(select Fs_transactions.STRAIRSNUMBER, Fs_transactions.numfeeyear as INTYEAR, " &
                    "sum(NUMPAYMENT) as TotalPaid  " &
                    "from fs_Transactions, fs_feeinvoice " &
                    "where Fs_transactions.invoiceid = fs_feeinvoice.invoiceid " &
                    "and strPayType = '6' " &
                    "group by Fs_transactions.STRAIRSNUMBER, Fs_transactions.numfeeyear, fs_Transactions.invoiceid) TOTALPAID, " &
                    "APBFACILITYINFORMATION, fs_feeAuditedData  " &
                    "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                    "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                    "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " &
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " &
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                    "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "

                Case "ONE-TIME"
                    SQL = "select * from " &
                    "(select " &
                    "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                    "STRFACILITYNAME, " &
                    "TOTALDUE.INTYEAR, strPaymentPlan, " &
                    "case " &
                    "when TotalDue is null then 0 " &
                    "else TotalDue " &
                    "end totalDue, " &
                    "case " &
                    "when TotalPaid is null then 0 " &
                    "else TotalPaid " &
                    "end TotalPaid, " &
                    "case " &
                    "when TotalDue is null and TotalPaid is null then 0 " &
                    " when totaldue is null and totalpaid is not null then -totalpaid " &
                    "when totaldue is not null and totalpaid is null then totaldue " &
                    "else (TOTALDUE - TOTALPAID) " &
                    "end Balance " &
                    "from " &
                    "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                    "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
                    "(select Fs_transactions.STRAIRSNUMBER, Fs_transactions.numfeeyear as INTYEAR, " &
                    "sum(NUMPAYMENT) as TotalPaid  " &
                    "from fs_Transactions, fs_feeinvoice " &
                    "where Fs_transactions.invoiceid = fs_feeinvoice.invoiceid " &
                    "and strPayType = '8' " &
                    "group by Fs_transactions.STRAIRSNUMBER, Fs_transactions.numfeeyear, fs_Transactions.invoiceid) TOTALPAID, " &
                    "APBFACILITYINFORMATION, fs_feeAuditedData  " &
                    "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                    "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                    "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " &
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " &
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                    "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "

                Case "REFUND"
                    SQL = "select * from " &
                       "(select " &
                       "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
                       "STRFACILITYNAME, " &
                       "TOTALDUE.INTYEAR, strPaymentPlan, " &
                       "case " &
                       "when TotalDue is null then 0 " &
                       "else TotalDue " &
                       "end totalDue, " &
                       "case " &
                       "when TotalPaid is null then 0 " &
                       "else TotalPaid " &
                       "end TotalPaid, " &
                       "case " &
                       "when TotalDue is null and TotalPaid is null then 0 " &
                       " when totaldue is null and totalpaid is not null then -totalpaid " &
                       "when totaldue is not null and totalpaid is null then totaldue " &
                       "else (TOTALDUE - TOTALPAID) " &
                       "end Balance " &
                       "from " &
                       "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " &
                       "from  fs_feeAuditedData where active = '1') TOTALDUE, " &
                       "(select Fs_transactions.STRAIRSNUMBER, Fs_transactions.numfeeyear as INTYEAR, " &
                       "sum(NUMPAYMENT) as TotalPaid  " &
                       "from fs_Transactions, fs_feeinvoice " &
                       "where Fs_transactions.invoiceid = fs_feeinvoice.invoiceid " &
                       "and strPayType = '7' " &
                       "group by Fs_transactions.STRAIRSNUMBER, Fs_transactions.numfeeyear, fs_Transactions.invoiceid) TOTALPAID, " &
                       "APBFACILITYINFORMATION, fs_feeAuditedData  " &
                       "where (APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " &
                       "or APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " &
                       "and APBFACILITYINFORMATION.strAIRSNumber = fs_feeAuditedData.strAIRSNumber " &
                       "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " &
                       "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " &
                       "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " &
                       "and fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "
            End Select
            If chbNonZeroBalance.Checked = True Then
                SQL = SQL & " where (TOTALDUE - TOTALPAID) <> '0'  "

            End If

            ds = New DataSet
            If SQL <> "" Then
                da = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "PaymentDue")
            End If

            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPaymentPlan").HeaderText = "Payment Plan"
            dgvDepositsAndPayments.Columns("strPaymentPlan").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Reported"
            dgvDepositsAndPayments.Columns("TotalDue").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("TotalDue").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("TotalPaid").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("TotalPaid").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("TotalPaid").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance"
            dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 6

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

            Exit Sub

            If cboStatPayType.Text = "ALL QUARTERS" Then
                dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Paid Type"
                dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 3
                dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Reported per Quarter"
                dgvDepositsAndPayments.Columns("TotalDue").DisplayIndex = 4
                dgvDepositsAndPayments.Columns("TotalDue").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("TotalPaid").HeaderText = "Amount Paid"
                dgvDepositsAndPayments.Columns("TotalPaid").DisplayIndex = 5
                dgvDepositsAndPayments.Columns("TotalPaid").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance"
                dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 6
                dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
                dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 7
                Select Case cboStatPayType.Text
                    Case "ONE-TIME", "AMENDMENT", "REFUND"
                        dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Pay Type"
                        dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 8
                End Select

            Else
                Select Case cboStatPayType.Text
                    Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                        dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Reported per Quarter"
                    Case Else
                        dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Reported"
                End Select
                dgvDepositsAndPayments.Columns("TotalDue").DisplayIndex = 3
                dgvDepositsAndPayments.Columns("TotalDue").DefaultCellStyle.Format = "c"

                dgvDepositsAndPayments.Columns("TotalPaid").HeaderText = "Amount Paid"
                dgvDepositsAndPayments.Columns("TotalPaid").DisplayIndex = 4
                dgvDepositsAndPayments.Columns("TotalPaid").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance"
                dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 5
                dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
                dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 6
                Select Case cboStatPayType.Text
                    Case "ONE-TIME", "AMENDMENT", "REFUND"
                        dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Pay Type"
                        dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 7
                End Select

            End If


            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewSelectedFeeData_Click(sender As Object, e As EventArgs) Handles btnViewSelectedFeeData.Click
        Try
            If pnlDetails.Dock = DockStyle.None Then
                pnlDetails.Dock = DockStyle.Top
            Else
                pnlDetails.Dock = DockStyle.None
            End If

            If txtSelectedAIRSNumber.Text <> "" And txtSelectedAIRSNumber.Text.Length = 8 And txtSelectedYear.Text <> "" Then
                LoadSelectedFeeData()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadSelectedFeeData()
        Try
            SQL = "select " &
            "strAIRSNUmber, nuMFeeYear, " &
            "intSubmittal, datSubmittal, " &
            "strComment, strIAIPdesc  " &
            "from FS_Admin, fslk_admin_status " &
            "where fs_admin.numCurrentStatus = FSLK_Admin_Status.ID  " &
            "and strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " &
            "and numFeeYear = '" & txtSelectedYear.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("intSubmittal")) Then
                    txtOnlineSubmittalStatus.Text = ""
                Else
                    If dr.Item("IntSubmittal") = "0" Then
                        txtOnlineSubmittalStatus.Text = "Not Finalized"
                    Else
                        txtOnlineSubmittalStatus.Text = "Finalized"
                    End If
                End If
                If IsDBNull(dr.Item("datSubmittal")) Then
                    txtDateSubmitted.Text = ""
                Else
                    If txtOnlineSubmittalStatus.Text = "Finalized" Then
                        txtDateSubmitted.Text = Format(dr.Item("datSubmittal"), "dd-MMM-yyyy")
                    Else
                        txtDateSubmitted.Text = ""
                    End If
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtSubmittalComments.Text = ""
                Else
                    txtSubmittalComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("strIAIPDesc")) Then
                    txtIAIPStatus.Clear()
                Else
                    txtIAIPStatus.Text = dr.Item("strIAIPDesc")
                End If
            End While
            dr.Close()

            SQL = "Select " &
            "intVOCTons, intPMTons, " &
            "intSO2Tons, intNOxTons, " &
            "numPart70Fee, numSMFee, " &
            "numNSPSFee, numTotalFee, " &
            "strNSPSExempt, " &
            "strOperate, numFeeRate, " &
            "strNSPSExemptReason, strPart70, " &
            "strSyntheticMinor, numCalculatedFee, " &
            "strClass, strNSPS, " &
            "datshutDown,  " &
            "numAdminFee, " &
            "(numTotalFee) as AllFees, " &
            "strPaymentPlan " &
            "from FS_FeeAuditedData " &
            "where strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " &
            "and numFeeYear = '" & txtSelectedYear.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("intVOCTons")) Then
                    txtVOCTons.Clear()
                Else
                    txtVOCTons.Text = dr.Item("intVOCTons")
                End If
                If IsDBNull(dr.Item("intPMTons")) Then
                    txtPMTons.Clear()
                Else
                    txtPMTons.Text = dr.Item("intPMTons")
                End If
                If IsDBNull(dr.Item("intSO2Tons")) Then
                    txtSO2Tons.Clear()
                Else
                    txtSO2Tons.Text = dr.Item("intSO2Tons")
                End If
                If IsDBNull(dr.Item("intNOxTons")) Then
                    txtNOxTons.Clear()
                Else
                    txtNOxTons.Text = dr.Item("intNOxTons")
                End If
                If IsDBNull(dr.Item("numPart70Fee")) Then
                    txtPart70Fee.Clear()
                Else
                    txtPart70Fee.Text = Format(dr.Item("numPart70Fee"), "c")
                End If
                If IsDBNull(dr.Item("numSMFee")) Then
                    txtSMfee.Clear()
                Else
                    txtSMfee.Text = Format(dr.Item("numSMFee"), "c")
                End If
                If IsDBNull(dr.Item("numNSPSFee")) Then
                    txtNSPSfee.Clear()
                Else
                    txtNSPSfee.Text = Format(dr.Item("numNSPSFee"), "c")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtTotalFee.Clear()
                Else
                    txtTotalFee.Text = Format(dr.Item("numTotalFee"), "c")
                End If
                If IsDBNull(dr.Item("strNSPSExempt")) Then
                    txtNSPSExempt.Clear()
                    txtNSPSExemptReason.Clear()
                Else
                    txtNSPSExempt.Text = dr.Item("strNSPSExempt")
                    If dr.Item("strNSPSexempt") = "1" Then
                        txtNSPSExempt.Text = "YES"
                    Else
                        txtNSPSExempt.Clear()
                    End If
                End If
                If txtNSPSExempt.Text = "YES" Then
                    If IsDBNull(dr.Item("strNSPSExemptReason")) Then
                        txtNSPSExemptReason.Clear()
                    Else
                        txtNSPSExemptReason.Text = dr.Item("strNSPSExemptReason")
                    End If
                Else
                    txtNSPSExemptReason.Clear()
                End If
                If IsDBNull(dr.Item("strOperate")) Then
                    txtOperate.Clear()
                Else
                    txtOperate.Text = dr.Item("strOperate")
                    If txtOperate.Text = "1" Then
                        txtOperate.Text = "YES"
                    Else
                        txtOperate.Text = "NO"
                    End If
                End If
                If IsDBNull(dr.Item("numFeeRate")) Then
                    txtFeeRate.Clear()
                Else
                    txtFeeRate.Text = Format(dr.Item("numFeeRate"), "c")
                End If

                If IsDBNull(dr.Item("strPart70")) Then
                    txtPart70.Clear()
                Else
                    txtPart70.Text = dr.Item("strPart70")
                    If txtPart70.Text = "1" Then
                        txtPart70.Text = "YES"
                    Else
                        txtPart70.Text = "NO"
                    End If
                End If
                If IsDBNull(dr.Item("strSyntheticMinor")) Then
                    txtSyntheticMinor.Clear()
                Else
                    txtSyntheticMinor.Text = dr.Item("strSyntheticMinor")
                    If txtSyntheticMinor.Text = "1" Then
                        txtSyntheticMinor.Text = "YES"
                    Else
                        txtSyntheticMinor.Text = "NO"
                    End If
                End If
                If IsDBNull(dr.Item("numCalculatedFee")) Then
                    txtCalculatedFee.Clear()
                Else
                    txtCalculatedFee.Text = Format(dr.Item("numCalculatedFee"), "c")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    txtClass.Clear()
                Else
                    txtClass.Text = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strNSPS")) Then
                    txtNSPS.Clear()
                Else
                    txtNSPS.Text = dr.Item("strNSPS")
                    If txtNSPS.Text = "1" Then
                        txtNSPS.Text = "YES"
                    Else
                        txtNSPS.Text = "NO"
                    End If
                End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    txtAdminFee.Text = "0"
                Else
                    txtAdminFee.Text = Format(dr.Item("numAdminFee"), "c")
                End If
                If IsDBNull(dr.Item("AllFees")) Then
                    txtAllFees.Text = "ERROR"
                Else
                    txtAllFees.Text = Format(dr.Item("AllFees"), "c")
                End If
                If IsDBNull(dr.Item("strPaymentPlan")) Then
                    txtPaymentType.Text = ""
                Else
                    txtPaymentType.Text = dr.Item("strPaymentPlan")
                End If
                If IsDBNull(dr.Item("datshutDown")) Then
                    txtShutDown.Clear()
                Else
                    txtShutDown.Text = Format(dr.Item("datShutDown"), "dd-MMM-yyyy")
                End If
            End While
            dr.Close()

            If txtNSPSExempt.Text = "YES" Then
                temp = txtNSPSExemptReason.Text
                If txtNSPSExemptReason.Text.Contains(",") Then
                    Do While txtNSPSExemptReason.Text <> ""
                        If txtNSPSExemptReason.Text.Contains(",") Then
                            temp = Mid(txtNSPSExemptReason.Text, 1, InStr(txtNSPSExemptReason.Text, ",", CompareMethod.Text) - 1)
                            txtNSPSExemptReason.Text = Mid(txtNSPSExemptReason.Text, InStr(txtNSPSExemptReason.Text, (temp & ","), CompareMethod.Text) + (temp.Length) + 1)
                        Else
                            temp = txtNSPSExemptReason.Text
                            txtNSPSExemptReason.Text = Replace(txtNSPSExemptReason.Text, temp, "")
                        End If

                        SQL2 = SQL2 & " or nspsreasoncode = '" & temp & "' "
                        temp = ""
                    Loop
                    SQL = "Select description " &
                    "from FSLK_NSPSReason " &
                    "where nspsreasoncode = '0' " & SQL2
                Else
                    SQL = "Select description " &
                    "from FSLK_NSPSReason " &
                    "where nspsreasoncode = '" & temp & "' "
                End If

                txtNSPSExemptReason.Clear()
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("description")) Then
                        txtNSPSExemptReason.Text = txtNSPSExemptReason.Text
                    Else
                        txtNSPSExemptReason.Text = "- " & txtNSPSExemptReason.Text & dr.Item("description") & vbCrLf
                    End If
                End While
                dr.Close()
            Else
                txtNSPSExemptReason.Clear()
            End If

            SQL = "select " &
            "TransactionID, FS_feeInvoice.InvoiceID, " &
            "numPayment, datTransactionDate, " &
            "strPayTypeDesc, strDepositNo, strCreditcardno, " &
            "strCheckNo, StrBatchNo, numAmount,  " &
            "FS_Transactions.strComment " &
            "from FS_Transactions, FS_FeeInvoice, " &
            "FSLK_PayType " &
            "where  FS_feeInvoice.strAIrsnumber = FS_Transactions.strAIRSNumber (+) " &
            "and   FS_feeInvoice.nuMFeeyear = FS_Transactions.nuMFeeYear (+) " &
            "and   FS_feeInvoice.InvoiceID = FS_Transactions.InvoiceID (+) " &
            "and fs_feeinvoice.strPaytype = FSLK_PayType.numPayTypeID " &
            "and FS_Transactions.active = '1' " &
            "and FS_FeeInvoice.active = '1' " &
            "and FS_feeInvoice.numfeeyear = '" & txtSelectedYear.Text & "'  " &
            "and FS_feeInvoice.strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "Transactions")
            dgvStats.DataSource = ds
            dgvStats.DataMember = "Transactions"

            dgvStats.RowHeadersVisible = False
            dgvStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStats.AllowUserToResizeColumns = True
            dgvStats.AllowUserToResizeRows = True
            dgvStats.AllowUserToAddRows = False
            dgvStats.AllowUserToDeleteRows = False
            dgvStats.AllowUserToOrderColumns = True
            dgvStats.Columns("transactionID").HeaderText = "Transaction ID"
            dgvStats.Columns("transactionID").DisplayIndex = 0
            dgvStats.Columns("INVOICEID").HeaderText = "Invoice ID"
            dgvStats.Columns("INVOICEID").DisplayIndex = 1
            dgvStats.Columns("DATTRANSACTIONDATE").HeaderText = "Transaction Date"
            dgvStats.Columns("DATTRANSACTIONDATE").DisplayIndex = 2
            dgvStats.Columns("DATTRANSACTIONDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvStats.Columns("NUMPAYMENT").HeaderText = "Amount Paid"
            dgvStats.Columns("NUMPAYMENT").DisplayIndex = 3
            dgvStats.Columns("NUMPAYMENT").DefaultCellStyle.Format = "c"
            dgvStats.Columns("strPayTypeDesc").HeaderText = "Invoice Type"
            dgvStats.Columns("strPayTypeDesc").DisplayIndex = 4

            dgvStats.Columns("numAmount").HeaderText = "Amount Owed"
            dgvStats.Columns("numAmount").DisplayIndex = 5
            dgvStats.Columns("numAmount").DefaultCellStyle.Format = "c"

            dgvStats.Columns("STRDEPOSITNO").HeaderText = "Deposit No"
            dgvStats.Columns("STRDEPOSITNO").DisplayIndex = 6
            dgvStats.Columns("STRBATCHNO").HeaderText = "Batch No."
            dgvStats.Columns("STRBATCHNO").DisplayIndex = 7
            dgvStats.Columns("strCreditcardno").HeaderText = "Credit Card Conf. #"
            dgvStats.Columns("strCreditcardno").DisplayIndex = 8
            dgvStats.Columns("STRCHECKNO").HeaderText = "Check No"
            dgvStats.Columns("STRCHECKNO").DisplayIndex = 9
            dgvStats.Columns("strComment").HeaderText = "Comments"
            dgvStats.Columns("strComment").DisplayIndex = 10



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvDepositsAndPayments_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvDepositsAndPayments.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvDepositsAndPayments.HitTest(e.X, e.Y)
        Try
            If dgvDepositsAndPayments.RowCount > 0 And hti.RowIndex <> -1 Then
                txtSelectedAIRSNumber.Text = dgvDepositsAndPayments(0, hti.RowIndex).Value
                txtSelectedFacilityName.Text = dgvDepositsAndPayments(1, hti.RowIndex).Value
                txtSelectedYear.Text = cboStatYear.Text
                pnlDetails.Dock = DockStyle.None
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnHideResults_Click(sender As Object, e As EventArgs) Handles btnHideResults.Click
        Try
            If pnlDetails.Dock = DockStyle.None Then
                pnlDetails.Dock = DockStyle.Top
            Else
                pnlDetails.Dock = DockStyle.None
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvDepositsAndPayments.ExportToExcel(Me)
    End Sub

    Private Sub btnRunLateFeeReport_Click(sender As Object, e As EventArgs) Handles btnRunLateFeeReport.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim Classification As String = ""
            Dim OperationalStatus As String = ""
            Dim SIC As String = ""
            Dim NSPS As String = ""
            Dim TV As String = ""
            Dim LastApp As String = ""
            Dim AppDate As String = ""
            Dim PermitNumber As String = ""
            Dim PendingApp As String = ""
            Dim LastCompliance As String = ""
            Dim ComplianceDate As String = ""
            Dim FeeYear As String = ""

            dgvLateFeePayerReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLateFeePayerReport.AllowUserToDeleteRows = False
            dgvLateFeePayerReport.AllowUserToResizeColumns = True
            dgvLateFeePayerReport.AllowUserToAddRows = False
            dgvLateFeePayerReport.Columns.Add("AIRSNumber", "AIRS Number")
            dgvLateFeePayerReport.Columns.Add("strFacilityName", "Facility Name")
            dgvLateFeePayerReport.Columns.Add("strCountyName", "County Name")
            dgvLateFeePayerReport.Columns.Add("strClass", "Classification")
            dgvLateFeePayerReport.Columns.Add("strOperationalStatus", "Operating Status")
            dgvLateFeePayerReport.Columns.Add("strSICCode", "SIC")
            dgvLateFeePayerReport.Columns.Add("NSPSStatus", "NSPS (Y/N)")
            dgvLateFeePayerReport.Columns.Add("TVStatus", "TV (Y/N)")
            dgvLateFeePayerReport.Columns.Add("FeeYear", "Fee Year")
            dgvLateFeePayerReport.Columns.Add("ComplianceEvent", "Compliance Event")
            dgvLateFeePayerReport.Columns.Add("ComplianceDate", "Compliance Date")
            dgvLateFeePayerReport.Columns.Add("PermittingAction", "Permitting Action")
            dgvLateFeePayerReport.Columns.Add("PermittingDate", "Permitting Date")
            dgvLateFeePayerReport.Columns("PermittingDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvLateFeePayerReport.Columns.Add("PermitNumber", "Permit Number")
            dgvLateFeePayerReport.Columns.Add("PendingPermit", "Pending Permit")


            SQL = "select " &
            "substr(FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " &
            "strFacilityName, strCountyName, " &
            "strClass, " &
            "case " &
            "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " &
            "else strOperationalStatus " &
            "End strOperationalStatus, " &
            "strSICCode, " &
            "case " &
            "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " &
            "else 'No' " &
            "end NSPSStatus, " &
            "case " &
            "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " &
            "else 'No' " &
            "end TVStatus, " &
            "'" & cboFeeYear.Text & "' as FeeYear " &
            "from FSPayAndSubmit, APBFacilityInformation, " &
            "LookUpCountyInformation, APBHeaderData " &
            "where FSPayAndSubmit.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FSPayAndSubmit.strAIRSNumber = APBHeaderData.strAIRSNumber " &
            "and substr(FSPayAndSubmit.strAIRSNumber,5, 3) = LookUpCountyInformation.strCountyCode " &
            "and intYear = '" & cboFeeYear.Text & "' " &
            "and intSubmittal = '0' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = ""
                FacilityName = ""
                County = ""
                Classification = ""
                OperationalStatus = ""
                SIC = ""
                NSPS = ""
                TV = ""
                FeeYear = ""
                LastApp = ""
                AppDate = ""
                PermitNumber = ""
                PendingApp = ""
                LastCompliance = ""
                ComplianceDate = ""

                If IsDBNull(dr.Item("AIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("AIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = ""
                Else
                    FacilityName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    County = ""
                Else
                    County = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    Classification = ""
                Else
                    Classification = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    OperationalStatus = ""
                Else
                    OperationalStatus = dr.Item("strOperationalStatus")
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    SIC = ""
                Else
                    SIC = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("NSPSStatus")) Then
                    NSPS = ""
                Else
                    NSPS = dr.Item("NSPSStatus")
                End If
                If IsDBNull(dr.Item("TVStatus")) Then
                    TV = ""
                Else
                    TV = dr.Item("TVStatus")
                End If
                If IsDBNull(dr.Item("FeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("FeeYear")
                End If

                SQL = "select " &
                "max(to_number(SSPPApplicationMaster.strApplicationNumber)) as LastApp " &
                "from SSPPApplicationMaster " &
                "where strAIRSNumber = '0413" & AIRSNumber & "' " &
                "and datFinalizedDate is not null"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("LastApp")) Then
                        LastApp = ""
                    Else
                        LastApp = dr2.Item("LastApp")
                    End If
                End While
                dr2.Close()

                If LastApp <> "" Then
                    SQL = "select " &
                    "strApplicationTypeDesc, strPermitNumber, " &
                    "case " &
                    "when datPermitIssued is null then to_char(datFinalizedDate, 'dd-Mon-yyyy') " &
                    "else to_char(datPermitIssued, 'dd-Mon-yyyy') " &
                    "end FinalDate " &
                    "from SSPPApplicationMaster, LookUpApplicationTypes, " &
                    "SSPPApplicationTracking, SSPPApplicationData  " &
                    "where SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & LastApp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strApplicationTypeDesc")) Then
                            LastApp = LastApp
                        Else
                            LastApp = LastApp & " - " & dr2.Item("strApplicationTypeDesc")
                        End If
                        If IsDBNull(dr2.Item("FinalDate")) Then
                            AppDate = ""
                        Else
                            AppDate = dr2.Item("FinalDate")
                        End If
                        If IsDBNull(dr2.Item("strPermitNumber")) Then
                            PermitNumber = ""
                        Else
                            PermitNumber = dr2.Item("strPermitNumber")
                            If PermitNumber.Length = 15 Then
                                If IsNumeric(Mid(PermitNumber, 1, 4)) Then
                                    PermitNumber = Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" &
                                                     Mid(PermitNumber, 8, 4) & "-" & Mid(PermitNumber, 12, 1) & "-" &
                                                      Mid(PermitNumber, 13, 2) & "-" & Mid(PermitNumber, 15)
                                End If
                            End If
                        End If
                    End While
                    dr2.Close()
                End If

                SQL = "select " &
                "strApplicationNumber " &
                "from SSPPApplicationMaster " &
                "where datfinalizedDate Is null " &
                "and strAIRSNumber = '0413" & AIRSNumber & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                PendingApp = "No"
                While dr2.Read
                    If IsDBNull(dr2.Item("strApplicationNumber")) Then
                        PendingApp = "No"
                    Else
                        PendingApp = dr2.Item("strApplicationNumber")
                    End If
                End While
                dr2.Close()

                SQL = "select " &
                "max(datReceiveddate) as MaxDate " &
                "from SSCPItemMaster " &
                "where strAIRSNumber = '0413" & AIRSNumber & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("MaxDate")) Then
                        ComplianceDate = ""
                    Else
                        ComplianceDate = dr2.Item("MaxDate")
                        LastCompliance = "Item"
                    End If
                End While
                dr2.Close()

                SQL = "select " &
                "max(datFCECompleted) as MaxDate " &
                "from SSCPFCEMaster, SSCPFCE  " &
                "where SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber " &
                "and strAIRSnumber = '0413" & AIRSNumber & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("MaxDate")) Then
                        ComplianceDate = ""
                    Else
                        If ComplianceDate <> "" Then
                            If ComplianceDate < dr2.Item("maxdate") Then
                                ComplianceDate = dr2.Item("MaxDate")
                                LastCompliance = "FCE"
                            End If
                        Else
                            ComplianceDate = dr2.Item("MaxDate")
                            LastCompliance = "FCE"
                        End If
                    End If
                End While
                dr2.Close()

                SQL = "select " &
                "max(datEnforcementFinalized) as MaxDate " &
                "from SSCP_AuditedEnforcement " &
                "where strAIRSnumber = '0413" & AIRSNumber & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("MaxDate")) Then
                        ComplianceDate = ""
                    Else
                        If ComplianceDate <> "" Then
                            If ComplianceDate < dr2.Item("maxdate") Then
                                ComplianceDate = dr2.Item("MaxDate")
                                LastCompliance = "Enforcement"
                            End If
                        Else
                            ComplianceDate = dr2.Item("MaxDate")
                            LastCompliance = "Enforcement"
                        End If
                    End If
                End While
                dr2.Close()

                Select Case LastCompliance
                    Case "Item"
                        SQL = "select strTrackingNumber, datCompleteDate, " &
                        "strActivityName " &
                        "from SSCPItemMaster, LookupComplianceActivities  " &
                        "where SSCPItemMaster.strEventType = LookUpComplianceActivities.strActivityType  " &
                        "and strAIRSNumber = '0413" & AIRSNumber & "' " &
                        "and datCompleteDate = (select max(datCompleteDate) from SSCPItemMaster " &
                        "where strAIRSNumber = '0413" & AIRSNumber & "') "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strTrackingNumber")) Then
                                LastCompliance = ""
                            Else
                                LastCompliance = dr2.Item("strTrackingNumber")
                            End If
                            If IsDBNull(dr2.Item("strActivityName")) Then
                                LastCompliance = LastCompliance
                            Else
                                LastCompliance = LastCompliance & " - " & dr2.Item("strActivityName")
                            End If
                            If IsDBNull(dr2.Item("datCompleteDate")) Then
                                ComplianceDate = ""
                            Else
                                ComplianceDate = dr2.Item("datCompleteDate")
                            End If
                        End While
                    Case "FCE"
                        SQL = "select " &
                        "SSCPFCE.strFCENumber, datFCECompleted " &
                        "from SSCPFCE, SSCPFCEMaster  " &
                        "where SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber " &
                        "and strAIRSNumber = '0413" & AIRSNumber & "' " &
                        "and SSCPFCE.datFCECompleted = (select " &
                        "max(datFCECompleted) " &
                        "from SSCPFCEMaster, SSCPFCE  " &
                        "where SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber " &
                        "and strAIRSnumber = '0413" & AIRSNumber & "') "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strFCENumber")) Then
                                LastCompliance = ""
                            Else
                                LastCompliance = dr2.Item("strFCENumber") & " - FCE "
                            End If
                            If IsDBNull(dr2.Item("datFCECompleted")) Then
                                ComplianceDate = ""
                            Else
                                ComplianceDate = dr2.Item("datFCECompleted")
                            End If
                        End While
                        dr2.Close()
                    Case "Enforcement"
                        SQL = "select " &
                        "strEnforcementNumber, datEnforcementFinalized " &
                        "from SSCP_AuditedEnforcement " &
                        "where strAIRSNumber = '0413" & AIRSNumber & "' " &
                        "and datEnforcementFinalized = (Select " &
                        "max(datEnforcementFinalized) " &
                        "from SSCP_AuditedEnforcement " &
                        "where strairsnumber = '0413" & AIRSNumber & "') "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strEnforcementNumber")) Then
                                LastCompliance = ""
                            Else
                                LastCompliance = dr2.Item("strEnforcementNumber") & " - Enforcement "
                            End If
                            If IsDBNull(dr2.Item("datEnforcementFinalized")) Then
                                ComplianceDate = ""
                            Else
                                ComplianceDate = dr2.Item("datEnforcementFinalized")
                            End If
                        End While
                        dr2.Close()
                    Case Else
                End Select

                dgvLateFeePayerReport.Rows.Add(AIRSNumber, FacilityName, County, Classification,
                                               OperationalStatus, SIC, NSPS, TV, FeeYear, LastCompliance, ComplianceDate,
                                               LastApp, AppDate, PermitNumber, PendingApp)
            End While
            dr.Close()

            txtFeeCount.Text = dgvLateFeePayerReport.RowCount.ToString
            dgvLateFeePayerReport.Columns("PermittingDate").DefaultCellStyle.Format = "dd-MMM-yyyy"


        Catch ex As Exception



            ' ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        Try
            SQL = "select " &
            "substr(FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " &
            "strFacilityName, strCountyName, " &
            "strClass, " &
            "case " &
            "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " &
            "else strOperationalStatus " &
            "End strOperationalStatus, " &
            "strSICCode, " &
            "case " &
            "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " &
            "else 'No' " &
            "end NSPSStatus, " &
            "case " &
            "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " &
            "else 'No' " &
            "end TVStatus, " &
            "'" & cboFeeYear.Text & "' as FeeYear " &
            "from FSPayAndSubmit, APBFacilityInformation, " &
            "LookUpCountyInformation, APBHeaderData " &
            "where FSPayAndSubmit.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FSPayAndSubmit.strAIRSNumber = APBHeaderData.strAIRSNumber " &
            "and substr(FSPayAndSubmit.strAIRSNumber,5, 3) = LookUpCountyInformation.strCountyCode " &
            "and intYear = '" & cboFeeYear.Text & "' " &
            "and intSubmittal = '0' "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "LateFeeReport")

            dgvLateFeeReport.DataSource = ds
            dgvLateFeeReport.DataMember = "LateFeeReport"

            dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLateFeeReport.AllowUserToResizeColumns = True
            dgvLateFeeReport.AllowUserToResizeRows = True
            dgvLateFeeReport.AllowUserToAddRows = False
            dgvLateFeeReport.AllowUserToDeleteRows = False
            dgvLateFeeReport.AllowUserToOrderColumns = True
            dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
            dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1
            dgvLateFeeReport.Columns("strCountyName").HeaderText = "County Name"
            dgvLateFeeReport.Columns("strCountyName").DisplayIndex = 2
            dgvLateFeeReport.Columns("strClass").HeaderText = "Classification"
            dgvLateFeeReport.Columns("strClass").DisplayIndex = 3
            dgvLateFeeReport.Columns("strOperationalStatus").HeaderText = "Operating Status"
            dgvLateFeeReport.Columns("strOperationalStatus").DisplayIndex = 4
            dgvLateFeeReport.Columns("strSICCode").HeaderText = "SIC"
            dgvLateFeeReport.Columns("strSICCode").DisplayIndex = 5
            dgvLateFeeReport.Columns("NSPSStatus").HeaderText = "NSPS (Y/N)"
            dgvLateFeeReport.Columns("NSPSStatus").DisplayIndex = 6
            dgvLateFeeReport.Columns("TVStatus").HeaderText = "TV (Y/N)"
            dgvLateFeeReport.Columns("TVStatus").DisplayIndex = 7
            dgvLateFeeReport.Columns("FeeYear").HeaderText = "Fee Year"
            dgvLateFeeReport.Columns("FeeYear").DisplayIndex = 8

            txtFeeCount.Text = dgvLateFeeReport.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckforFeesPaid_Click(sender As Object, e As EventArgs) Handles btnCheckforFeesPaid.Click
        Try
            If rdbHasPaidFee.Checked = True Then
                SQL = "select  " &
                "substr(FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber,   " &
                "strFacilityName, strCountyName,   " &
                "strClass,  " &
                "case   " &
                "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate   " &
                "else strOperationalStatus   " &
                "End strOperationalStatus,  " &
                "strSICCode,  " &
                "case   " &
                "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes'   " &
                "else 'No'   " &
                "end NSPSStatus, " &
                "case " &
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " &
                "else 'No' " &
                "end TVStatus, " &
                "sum(numPayment) TotalPaid, " &
                 "'" & cboFeeYear.Text & "' as FeeYear " &
                "from FSPayAndSubmit, APBFacilityInformation,   " &
                "LookUpCountyInformation, APBHeaderData,  " &
                "FSAddPaid  " &
                "where FSPayAndSubmit.strAIRSNumber = APBFacilityInformation.strAIRSNumber   " &
                "and FSPayAndSubmit.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                "and FSPayAndSubmit.strAIRSNumber = FSAddPaid.strAIRSnumber   " &
                "and FSPayAndSubmit.intYear = FSAddPaid.intYear  " &
                "and substr(FSPayAndSubmit.strAIRSNumber,5, 3) = LookUpCountyInformation.strCountyCode   " &
                "and FSPayAndSubmit.intYear = '" & cboFeeYear.Text & "'   " &
                "and intSubmittal = '0'   " &
                "group by FSPayAndSubmit.strAIRSNumber, strFacilityName, strCountyName,   " &
                "strClass, strOperationalStatus, datShutDownDate, strSICCode, strAirProgramCodes  " &
                "order by AIRSNumber "

                ds = New DataSet
                da = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "LateFeeReport")

                dgvLateFeeReport.DataSource = ds
                dgvLateFeeReport.DataMember = "LateFeeReport"

                dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvLateFeeReport.AllowUserToResizeColumns = True
                dgvLateFeeReport.AllowUserToResizeRows = True
                dgvLateFeeReport.AllowUserToAddRows = False
                dgvLateFeeReport.AllowUserToDeleteRows = False
                dgvLateFeeReport.AllowUserToOrderColumns = True
                dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
                dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
                dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1
                dgvLateFeeReport.Columns("strCountyName").HeaderText = "County Name"
                dgvLateFeeReport.Columns("strCountyName").DisplayIndex = 2
                dgvLateFeeReport.Columns("strClass").HeaderText = "Classification"
                dgvLateFeeReport.Columns("strClass").DisplayIndex = 3
                dgvLateFeeReport.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvLateFeeReport.Columns("strOperationalStatus").DisplayIndex = 4
                dgvLateFeeReport.Columns("strSICCode").HeaderText = "SIC"
                dgvLateFeeReport.Columns("strSICCode").DisplayIndex = 5
                dgvLateFeeReport.Columns("NSPSStatus").HeaderText = "NSPS (Y/N)"
                dgvLateFeeReport.Columns("NSPSStatus").DisplayIndex = 6
                dgvLateFeeReport.Columns("TVStatus").HeaderText = "TV (Y/N)"
                dgvLateFeeReport.Columns("TVStatus").DisplayIndex = 7
                dgvLateFeeReport.Columns("TotalPaid").HeaderText = "Total Paid"
                dgvLateFeeReport.Columns("TotalPaid").DisplayIndex = 8
                dgvLateFeeReport.Columns("FeeYear").HeaderText = "Fee Year"
                dgvLateFeeReport.Columns("FeeYear").DisplayIndex = 9
            Else
                SQL = "select " &
                "substr(FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " &
                "strFacilityName, strCountyName, " &
                "strClass, " &
                "case " &
                "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " &
                "else strOperationalStatus " &
                "End strOperationalStatus, " &
                "strSICCode, " &
                "case " &
                "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " &
                "else 'No' " &
                "end NSPSStatus, " &
                "case " &
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " &
                "else 'No' " &
                "end TVStatus, " &
                 "'" & cboFeeYear.Text & "' as FeeYear " &
                "from FSPayAndSubmit, APBFacilityInformation, " &
                "LookUpCountyInformation, APBHeaderData " &
                "where FSPayAndSubmit.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and FSPayAndSubmit.strAIRSNumber = APBHeaderData.strAIRSNumber " &
                "and substr(FSPayAndSubmit.strAIRSNumber,5, 3) = LookUpCountyInformation.strCountyCode " &
                "and intYear = '" & cboFeeYear.Text & "' " &
                "and intSubmittal = '0' " &
                "and not exists (select * from FSAddPaid " &
                "where FSPayAndSubmit.strAIRSnumber = FSAddPaid.strAIRSnumber " &
                "and FSPayAndSubmit.intYear = FSAddPaid.intYear) " &
                "order by AIRSNumber "

                ds = New DataSet
                da = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "LateFeeReport")

                dgvLateFeeReport.DataSource = ds
                dgvLateFeeReport.DataMember = "LateFeeReport"

                dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvLateFeeReport.AllowUserToResizeColumns = True
                dgvLateFeeReport.AllowUserToResizeRows = True
                dgvLateFeeReport.AllowUserToAddRows = False
                dgvLateFeeReport.AllowUserToDeleteRows = False
                dgvLateFeeReport.AllowUserToOrderColumns = True
                dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
                dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
                dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1
                dgvLateFeeReport.Columns("strCountyName").HeaderText = "County Name"
                dgvLateFeeReport.Columns("strCountyName").DisplayIndex = 2
                dgvLateFeeReport.Columns("strClass").HeaderText = "Classification"
                dgvLateFeeReport.Columns("strClass").DisplayIndex = 3
                dgvLateFeeReport.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvLateFeeReport.Columns("strOperationalStatus").DisplayIndex = 4
                dgvLateFeeReport.Columns("strSICCode").HeaderText = "SIC"
                dgvLateFeeReport.Columns("strSICCode").DisplayIndex = 5
                dgvLateFeeReport.Columns("NSPSStatus").HeaderText = "NSPS (Y/N)"
                dgvLateFeeReport.Columns("NSPSStatus").DisplayIndex = 6
                dgvLateFeeReport.Columns("TVStatus").HeaderText = "TV (Y/N)"
                dgvLateFeeReport.Columns("TVStatus").DisplayIndex = 7
                dgvLateFeeReport.Columns("FeeYear").HeaderText = "Fee Year"
                dgvLateFeeReport.Columns("FeeYear").DisplayIndex = 8
            End If

            txtFeeCount.Text = dgvLateFeeReport.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemovePaidFacilities_Click(sender As Object, e As EventArgs) Handles btnRemovePaidFacilities.Click
        Try
            Dim AIRSNumber As String = ""
            Dim i As Integer
            temp = dgvLateFeeReport.ColumnCount.ToString

            If dgvLateFeeReport.ColumnCount.ToString = 10 Then
                If dgvLateFeeReport.Columns(8).HeaderText = "Total Paid" Then
                    For i = 0 To dgvLateFeeReport.RowCount.ToString - 1
                        AIRSNumber = dgvLateFeeReport(0, i).Value

                        SQL = "update FSPayAndSubmit set " &
                        "intSubmittal = '1' " &
                        "where strAIRSnumber = '0413" & AIRSNumber & "' " &
                        "and intYear = '" & cboFeeYear.Text & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Connecting Then
                            CurrentConnection.Close()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        temp = "0"

                        SQL = "Select " &
                        "count(*) as FSCalc " &
                        "from FSCalculations " &
                        "where strAIRSNumber = '0413" & AIRSNumber & "' " &
                        "and intYear = '" & cboFeeYear.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("FSCalc")) Then
                                temp = "0"
                            Else
                                temp = dr.Item("FSCalc")
                            End If
                        End While
                        dr.Close()
                        If temp = "0" Then
                            SQL = "Insert into FSCalculations " &
                            "values " &
                            "('0413" & AIRSNumber & "', '" & cboFeeYear.Text & "', " &
                            "'0', '0', '0', '0', " &
                            "'0', '0', '0', '0', " &
                            "'NO', '0', 'YES', '33.0', " &
                            "'', 'No', 'No', '0', " &
                            "'', '', '', '', '', '0') "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                    Next

                    MessageBox.Show("Removal Complete", "Fee Stats & Mailout", MessageBoxButtons.OK)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewUnenrolled_Click(sender As Object, e As EventArgs) Handles btnViewUnenrolled.Click
        Try
            SQL = "select " &
            "substr(FEEMailOut.strAIRSNumber, 5) as AIRSNumber, " &
            "strFacilityName " &
            "from FeeMailout " &
            "where intYear = '" & cboFeeYear.Text & "' " &
            "and not exists (select * from FSPayAndSubmit " &
            "where FeeMailOut.strAIRSnumber = FSPayAndSubmit.strAIRSnumber " &
            "and FeeMailOut.intYear = FSPayAndSubmit.intYear) "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "LateFeeReport")

            dgvLateFeeReport.DataSource = ds
            dgvLateFeeReport.DataMember = "LateFeeReport"

            dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLateFeeReport.AllowUserToResizeColumns = True
            dgvLateFeeReport.AllowUserToResizeRows = True
            dgvLateFeeReport.AllowUserToAddRows = False
            dgvLateFeeReport.AllowUserToDeleteRows = False
            dgvLateFeeReport.AllowUserToOrderColumns = True
            dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
            dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1

            txtFeeCount.Text = dgvLateFeeReport.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvLateFeeReport_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvLateFeeReport.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvLateFeeReport.HitTest(e.X, e.Y)
            If dgvLateFeeReport.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvLateFeeReport.Columns(0).HeaderText = "AIRS Number" Then
                    If IsDBNull(dgvLateFeeReport(0, hti.RowIndex).Value) Then
                        txtFeeAIRSNumber.Clear()
                    Else
                        txtFeeAIRSNumber.Text = dgvLateFeeReport(0, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvLateFeeReport(1, hti.RowIndex).Value) Then
                        txtFeeFacilityName.Clear()
                    Else
                        txtFeeFacilityName.Text = dgvLateFeeReport(1, hti.RowIndex).Value
                    End If

                    txtFeeComplianceEvent.Clear()
                    txtFeeComplianceEventType.Clear()
                    txtFeeLastComplianceEvent.Clear()
                    txtFeePermittingEvent.Clear()
                    txtFeePermittingEventType.Clear()
                    txtFeePermittingDate.Clear()
                    txtFeePermitNumber.Clear()
                    txtFeePendingPermit.Clear()
                    txtFeePendingPermitType.Clear()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFeeData()
        Try
            Dim AIRSNumber As String
            Dim ComplianceDate As String = ""
            Dim ComplianceType As String = ""
            Dim LastCompliance As String = ""
            Dim LastApp As String = ""
            Dim AppDate As String = ""
            Dim AppType As String = ""
            Dim PermitNumber As String = ""
            Dim PendingApp As String = ""
            Dim PendingAppType As String = ""

            AIRSNumber = txtFeeAIRSNumber.Text

            SQL = "select " &
            "max(datReceiveddate) as MaxDate " &
            "from SSCPItemMaster " &
            "where strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("MaxDate")) Then
                    ComplianceDate = ""
                Else
                    ComplianceDate = dr2.Item("MaxDate")
                    ComplianceType = "Item"
                End If
            End While
            dr2.Close()

            SQL = "select " &
            "max(datFCECompleted) as MaxDate " &
            "from SSCPFCEMaster, SSCPFCE  " &
            "where SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber " &
            "and strAIRSnumber = '0413" & AIRSNumber & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("MaxDate")) Then
                    ComplianceDate = ""
                Else
                    If ComplianceDate <> "" Then
                        If ComplianceDate < dr2.Item("maxdate") Then
                            ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                            ComplianceType = "FCE"
                        End If
                    Else
                        ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                        ComplianceType = "FCE"
                    End If
                End If
            End While
            dr2.Close()

            SQL = "select " &
            "max(datEnforcementFinalized) as MaxDate " &
            "from SSCP_AuditedEnforcement " &
            "where strAIRSnumber = '0413" & AIRSNumber & "'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("MaxDate")) Then
                    ComplianceDate = ""
                Else
                    If ComplianceDate <> "" Then
                        If ComplianceDate < dr2.Item("maxdate") Then
                            ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                            ComplianceType = "Enforcement"
                        End If
                    Else
                        ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                        ComplianceType = "Enforcement"
                    End If
                End If
            End While
            dr2.Close()

            Select Case ComplianceType
                Case "Item"
                    SQL = "select strTrackingNumber, datCompleteDate, " &
                    "strActivityName " &
                    "from SSCPItemMaster, LookupComplianceActivities  " &
                    "where SSCPItemMaster.strEventType = LookUpComplianceActivities.strActivityType  " &
                    "and strAIRSNumber = '0413" & AIRSNumber & "' " &
                    "and datCompleteDate = (select max(datCompleteDate) from SSCPItemMaster " &
                    "where strAIRSNumber = '0413" & AIRSNumber & "') "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strTrackingNumber")) Then
                            LastCompliance = ""
                        Else
                            LastCompliance = dr2.Item("strTrackingNumber")
                        End If
                        If IsDBNull(dr2.Item("strActivityName")) Then
                            ComplianceType = ""
                        Else
                            ComplianceType = dr2.Item("strActivityName")
                        End If
                        If IsDBNull(dr2.Item("datCompleteDate")) Then
                            ComplianceDate = ""
                        Else
                            ComplianceDate = Format(dr2.Item("datCompleteDate"), "dd-MMM-yyyy")
                        End If
                    End While
                    lblComplianceDate.Text = "Date Completed"
                Case "FCE"
                    SQL = "select " &
                    "SSCPFCE.strFCENumber, datFCECompleted " &
                    "from SSCPFCE, SSCPFCEMaster  " &
                    "where SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber " &
                    "and strAIRSNumber = '0413" & AIRSNumber & "' " &
                    "and SSCPFCE.datFCECompleted = (select " &
                    "max(datFCECompleted) " &
                    "from SSCPFCEMaster, SSCPFCE  " &
                    "where SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber " &
                    "and strAIRSnumber = '0413" & AIRSNumber & "') "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        ComplianceType = "FCE"
                        If IsDBNull(dr2.Item("strFCENumber")) Then
                            LastCompliance = ""
                        Else
                            LastCompliance = dr2.Item("strFCENumber")
                        End If
                        If IsDBNull(dr2.Item("datFCECompleted")) Then
                            ComplianceDate = ""
                        Else
                            ComplianceDate = Format(dr2.Item("datFCECompleted"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr2.Close()
                    lblComplianceDate.Text = "Date FCE Completed"
                Case "Enforcement"
                    SQL = "select " &
                    "strEnforcementNumber, datEnforcementFinalized " &
                    "from SSCP_AuditedEnforcement " &
                    "where strAIRSNumber = '0413" & AIRSNumber & "' " &
                    "and datEnforcementFinalized = (Select " &
                    "max(datEnforcementFinalized) " &
                    "from SSCP_AuditedEnforcement " &
                    "where strairsnumber = '0413" & AIRSNumber & "') "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        ComplianceType = "Enforcement"
                        If IsDBNull(dr2.Item("strEnforcementNumber")) Then
                            LastCompliance = ""
                        Else
                            LastCompliance = dr2.Item("strEnforcementNumber")
                        End If
                        If IsDBNull(dr2.Item("datEnforcementFinalized")) Then
                            ComplianceDate = ""
                        Else
                            ComplianceDate = Format(dr2.Item("datEnforcementFinalized"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr2.Close()
                    lblComplianceDate.Text = "Date Enforcement Finalized"
                Case Else
                    lblComplianceDate.Text = "Date"
            End Select

            txtFeeComplianceEvent.Text = LastCompliance
            txtFeeComplianceEventType.Text = ComplianceType
            txtFeeLastComplianceEvent.Text = ComplianceDate

            SQL = "select " &
            "max(to_number(SSPPApplicationMaster.strApplicationNumber)) as LastApp " &
            "from SSPPApplicationMaster " &
            "where strAIRSNumber = '0413" & AIRSNumber & "' " &
            "and datFinalizedDate is not null"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("LastApp")) Then
                    LastApp = ""
                Else
                    LastApp = dr2.Item("LastApp")
                End If
            End While
            dr2.Close()

            If LastApp <> "" Then
                SQL = "select " &
                "strApplicationTypeDesc, strPermitNumber, " &
                "case " &
                "when datPermitIssued is null then to_char(datFinalizedDate, 'dd-Mon-yyyy') " &
                "else to_char(datPermitIssued, 'dd-Mon-yyyy') " &
                "end FinalDate " &
                "from SSPPApplicationMaster, LookUpApplicationTypes, " &
                "SSPPApplicationTracking, SSPPApplicationData  " &
                "where SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber (+) " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber (+) " &
                "and SSPPApplicationMaster.strApplicationNumber = '" & LastApp & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("strApplicationTypeDesc")) Then
                        AppType = ""
                    Else
                        AppType = dr2.Item("strApplicationTypeDesc")
                    End If
                    If IsDBNull(dr2.Item("FinalDate")) Then
                        AppDate = ""
                    Else
                        AppDate = dr2.Item("FinalDate")
                    End If
                    If IsDBNull(dr2.Item("strPermitNumber")) Then
                        PermitNumber = ""
                    Else
                        PermitNumber = dr2.Item("strPermitNumber")
                        If PermitNumber.Length = 15 Then
                            If IsNumeric(Mid(PermitNumber, 1, 4)) Then
                                PermitNumber = Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" &
                                                 Mid(PermitNumber, 8, 4) & "-" & Mid(PermitNumber, 12, 1) & "-" &
                                                  Mid(PermitNumber, 13, 2) & "-" & Mid(PermitNumber, 15)
                            End If
                        End If
                    End If
                End While
                dr2.Close()
            End If

            txtFeePermittingEvent.Text = LastApp
            txtFeePermittingEventType.Text = AppType
            txtFeePermittingDate.Text = AppDate
            txtFeePermitNumber.Text = PermitNumber

            SQL = "select " &
            "strApplicationNumber " &
            "from SSPPApplicationMaster " &
            "where datfinalizedDate Is null " &
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            SQL = "select " &
            "strApplicationNumber, strApplicationTypeDesc " &
            "from SSPPApplicationMaster, LookUpApplicationTypes " &
            "where SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
            "and datfinalizedDate Is null " &
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr2 = cmd.ExecuteReader
            PendingApp = "No"
            While dr2.Read
                If IsDBNull(dr2.Item("strApplicationNumber")) Then
                    PendingApp = "No"
                Else
                    PendingApp = dr2.Item("strApplicationNumber")
                End If
                If IsDBNull(dr2.Item("strApplicationTypeDesc")) Then
                    PendingAppType = ""
                Else
                    PendingAppType = dr2.Item("strApplicationTypeDesc")
                End If
            End While
            dr2.Close()

            txtFeePendingPermit.Text = PendingApp
            txtFeePendingPermitType.Text = PendingAppType

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnFeeFacilitySummary_Click(sender As Object, e As EventArgs) Handles btnFeeFacilitySummary.Click
        OpenFormFacilitySummary(txtFeeAIRSNumber.Text)
    End Sub

    Private Sub btnFeeViewComplianceEvent_Click(sender As Object, e As EventArgs) Handles btnFeeViewComplianceEvent.Click
        Try
            If txtFeeComplianceEvent.Text <> "" Then
                Select Case txtFeeComplianceEventType.Text
                    Case "FCE"
                        OpenFormFce(txtFeeComplianceEvent.Text)
                    Case "Enforcement"
                        OpenFormEnforcement(txtFeeComplianceEvent.Text)
                    Case Else
                        OpenFormSscpWorkItem(txtFeeComplianceEvent.Text)
                End Select
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnFeeViewPermittingEvent_Click(sender As Object, e As EventArgs) Handles btnFeeViewPermittingEvent.Click
        OpenFormPermitApplication(txtFeePermittingEvent.Text)
    End Sub

    Private Sub btnFeePendingPermittingEvent_Click(sender As Object, e As EventArgs) Handles btnFeePendingPermittingEvent.Click
        OpenFormPermitApplication(txtFeePendingPermit.Text)
    End Sub

    Private Sub btnViewData_Click(sender As Object, e As EventArgs) Handles btnViewData.Click
        Try
            If txtFeeAIRSNumber.Text <> "" Then
                LoadFeeData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportFeeReport_Click(sender As Object, e As EventArgs) Handles btnExportFeeReport.Click
        dgvLateFeeReport.ExportToExcel(Me)
    End Sub

    Private Sub chbDepositDateSearch_CheckedChanged(sender As Object, e As EventArgs) Handles chbDepositDateSearch.CheckedChanged
        Try
            If chbDepositDateSearch.Checked = True Then
                dtpStartDepositDate.Enabled = True
                dtpEndDepositDate.Enabled = True
                btnRunDepositReport.Enabled = True
                cboStatYear.Enabled = False
                cboStatPayType.Enabled = False
                btnViewDepositsStats.Enabled = False
                btnViewPaymentDue.Enabled = False
                bntViewTotalPaid.Enabled = False
                btnViewBalance.Enabled = False
                chbNonZeroBalance.Enabled = False
            Else
                dtpStartDepositDate.Enabled = False
                dtpEndDepositDate.Enabled = False
                btnRunDepositReport.Enabled = False
                cboStatYear.Enabled = True
                cboStatPayType.Enabled = True
                btnViewDepositsStats.Enabled = True
                btnViewPaymentDue.Enabled = True
                bntViewTotalPaid.Enabled = True
                btnViewBalance.Enabled = True
                chbNonZeroBalance.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunDepositReport_Click(sender As Object, e As EventArgs) Handles btnRunDepositReport.Click
        Try
            Dim query As String = "SELECT SUBSTR(fi.STRAIRSNUMBER, 5) AS AIRSNUMBER, " &
                "  fi.STRFACILITYNAME, tr.TRANSACTIONTYPECODE, " &
                "  CASE WHEN tr.TRANSACTIONTYPECODE = '1' THEN 'Deposit' WHEN " &
                "      tr.TRANSACTIONTYPECODE = '2'       THEN 'Refund' ELSE " &
                "      'N/A' " &
                "  END AS TRANSACTIONTYPE, SUM(tr.NUMPAYMENT) AS PaidAmount, " &
                "  tr.STRDEPOSITNO, tr.STRBATCHNO, tr.DATTRANSACTIONDATE, " &
                "  tr.STRCHECKNO, tr.INVOICEID, tr.NUMFEEYEAR " &
                "FROM FS_TRANSACTIONS tr " &
                "INNER JOIN APBFACILITYINFORMATION fi ON " &
                "  tr.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
                "WHERE tr.DATTRANSACTIONDATE BETWEEN @StartDate AND @EndDate AND " &
                "  tr.ACTIVE = '1' " &
                "GROUP BY fi.STRFACILITYNAME, tr.TRANSACTIONTYPECODE, " &
                "  tr.STRDEPOSITNO, tr.STRBATCHNO, tr.DATTRANSACTIONDATE, " &
                "  tr.STRCHECKNO, tr.INVOICEID, tr.NUMFEEYEAR, fi.STRAIRSNUMBER, " &
                "  tr.TRANSACTIONTYPECODE"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@StartDate", dtpStartDepositDate.Value),
                New SqlParameter("@EndDate", dtpEndDepositDate.Value)
            }

            dgvDepositsAndPayments.DataSource = DB.GetDataTable(query, parameters)

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("transactionTypeCode").HeaderText = "Pay Type"
            dgvDepositsAndPayments.Columns("transactionTypeCode").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("PaidAmount").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("PaidAmount").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("PaidAmount").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("strDepositNo").HeaderText = "Deposit #"
            dgvDepositsAndPayments.Columns("strDepositNo").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("strBatchNo").HeaderText = "Batch #"
            dgvDepositsAndPayments.Columns("strBatchNo").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("datTransactionDate").HeaderText = "Pay Date"
            dgvDepositsAndPayments.Columns("datTransactionDate").DisplayIndex = 7
            dgvDepositsAndPayments.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvDepositsAndPayments.Columns("strCheckNo").HeaderText = "Check #"
            dgvDepositsAndPayments.Columns("strCheckNo").DisplayIndex = 8
            dgvDepositsAndPayments.Columns("InvoiceID").HeaderText = "Invoice #"
            dgvDepositsAndPayments.Columns("InvoiceID").DisplayIndex = 9
            dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4

            dgvDepositsAndPayments.SanelyResizeColumns()

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Fee Reports "

    Private Sub LoadComboBoxesF()
        With cboAirsNo
            .DataSource = GetSharedData(SharedTable.AllFeeFacilities)
            .DisplayMember = "AIRS Number"
            .ValueMember = "STRAIRSNUMBER"
            .SelectedIndex = 0
        End With

        With cboFacilityName
            .DataSource = GetSharedData(SharedTable.AllFeeFacilities)
            .DisplayMember = "Facility Name"
            .ValueMember = "STRAIRSNUMBER"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub LoadComboBoxesD()
        Dim query As String = "Select distinct substring(strairsnumber, 5, 8) as strairsnumber " &
            "from FS_Transactions order by strairsnumber"
        cboAirs.DataSource = DB.GetDataTable(query)
        cboAirs.DisplayMember = "strairsnumber"
    End Sub

    Private Sub tabReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabReport.SelectedIndexChanged
        If tabReport.SelectedTab Is TPDeposits AndAlso cboAirs.Items.Count = 0 Then
            LoadComboBoxesD()
        End If
    End Sub

#Region "Facility Specific"

    Private Sub btnViewFacilitySpecificData_Click(sender As Object, e As EventArgs) Handles btnViewFacilitySpecificData.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New FacilityFee10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from VW_Facility_Fee " &
            "where strAIRSNumber = '0413" & cboAirsNo.SelectedValue & "' "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Fee")
            rpt.SetDataSource(ds)

            crParameterDiscreteValue.Value = "0413" & cboAirsNo.SelectedValue
            crParameterFieldDefinitions = rpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("AirsNo")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            SetUpCrystalReportViewer(rpt, CRFeesReports, cboAirsNo.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Year Specific"
    Private Sub btnFeesandEmissions_Click(sender As Object, e As EventArgs) Handles btnFeesandEmissions.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New TotalFee10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from VW_Total_fee "

            SQL = "SELECT  intYear, sum(intVOCTons) as intvoctons, " &
            "sum(intPMTons) as intPMTons, " &
            "sum(intSO2Tons) as intSO2Tons, " &
            "sum(intNOXtons) as intNOXTons, " &
            "sum(numSMFee) as numSMFee, " &
            "sum(numNSPSFee) as numNSPSFee, " &
            "sum(numTotalFee) as numTotalFee, " &
            "round(avg(numFeeRate)) as numFeeRate, " &
            "Round(avg(titlevminfee)) as titlevminfee, " &
            "round(avg(titlevfee)) as titlevfee  " &
            "from vw_total_fee " &
            "group by intyear "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Total_Fee")

            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Annual Emission and Fee")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClassification_Click(sender As Object, e As EventArgs) Handles btnClassification.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New FacilityClassification10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from FSCalculations "
            SQL = "Select * from VW_Facility_Class_Counts "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Class_Counts")

            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Classification Totals")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRunBalanceReport_Click(sender As Object, e As EventArgs) Handles btnRunBalanceReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim selectedYear As Integer = 0

            If Not Integer.TryParse(mtbFacilityBalanceYear.Text, selectedYear) Then
                mtbFacilityBalanceYear.Text = Date.Today.Year
            End If

            If selectedYear < 1990 Or selectedYear > Date.Today.Year Then
                mtbFacilityBalanceYear.Text = Date.Today.Year
            End If

            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            ds = New DataSet

            If chbFacilityBalance.Checked = False Then
                rpt = New FacilityBalance10
            Else
                rpt = New FacilityBalancewithZero10
            End If
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "SELECT " &
        "strFacilityName, " &
        "FeeDetails.strAIRSNumber, " &
        "FeeDetails.intyear, " &
        "totalDue, totalPaid, " &
        "strContactFirstName, strContactLastName, " &
        "strContactPhoneNumber1, strContactFaxNumber, " &
        "strContactEmail, strContactAddress1, " &
        "strContactCity, strContactState, " &
        "strContactZipCode, strSICCode, " &
        "numPayment, PaidYear   " &
        "FROM APBFacilityInformation, " &
        "FeeDetails, FeesContact, " &
        "APBHeaderData, FS_Transactions  " &
        "WHERE APBFacilityInformation.strAIRSNumber = FeeDetails.strAIRSNumber " &
        "AND APBFacilityInformation.strAIRSNumber = FeesContact.strAIRSnumber " &
        "AND APBFacilityInformation.strAIRSnumber = APBHeaderData.strAIRSNumber " &
        "AND APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
        "and feedetails.intyear = FS_Transactions.numFeeYear " &
        "and feedetails.intyear = '" & selectedYear.ToString & "' " &
        "order by strairsnumber "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Balance")

            rpt.SetDataSource(ds)

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Year"
            spValue.Value = selectedYear.ToString
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRFeesReports.ParameterFieldInfo = ParameterFields

            If chbFacilityBalance.Checked = False Then
                SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Fee Balance")
            Else
                SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Fee Balance with Zero Balance")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Financial"
    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        Try
            Me.Cursor = Cursors.Default
            ds = New DataSet
            rpt = New TotalPayment10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from VW_Total_PAYMENT "
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Total_PAYMENT")

            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Overall Fee Balance")
            CRFeesReports.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnFeeByYear_Click(sender As Object, e As EventArgs) Handles btnFeeByYear.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New feeByYear10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from FeesDue "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FeesDue")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Total Fee by Year")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

#Region "Deposits"

    Private Sub btnViewDepositsReportByDate_Click(sender As Object, e As EventArgs) Handles btnViewDepositsReportByDate.Click
        Me.Cursor = Cursors.WaitCursor
        Dim query As String = "SELECT TRANSACTIONID, INVOICEID, TRANSACTIONTYPECODE, " &
        "  DATTRANSACTIONDATE, NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, " &
        "  STRBATCHNO, STRENTRYPERSON, STRCOMMENT, ACTIVE, UPDATEUSER, " &
        "  UPDATEDATETIME, CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, " &
        "  STRCREDITCARDNO " &
        "FROM FS_TRANSACTIONS " &
        "WHERE ACTIVE = '1' AND DATTRANSACTIONDATE BETWEEN @StartDate " &
        "  AND @EndDate " &
        "ORDER BY NUMFEEYEAR DESC"
        Dim parameters As SqlParameter() = {
            New SqlParameter("@StartDate", dtpDepositReportStartDate.Value),
            New SqlParameter("@EndDate", dtpDepositReportEndDate.Value)
        }

        Dim ds As New DataSet
        Dim dt As DataTable = DB.GetDataTable(query, parameters)
        dt.TableName = "FS_Transactions"
        ds.Tables.Add(dt)

        rpt = New DepositQA11
        monitor.TrackFeature("Report." & rpt.ResourceName & ".byDate")
        rpt.SetDataSource(ds)

        SetUpCrystalReportViewer(rpt, CRFeesReports, "Deposits")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnViewFacilityDepositsReport_Click(sender As Object, e As EventArgs) Handles btnViewFacilityDepositsReport.Click
        If cboAirs.Text <> "" Then
            Dim query As String = "SELECT TRANSACTIONID, INVOICEID, TRANSACTIONTYPECODE, " &
                "  DATTRANSACTIONDATE, NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, " &
                "  STRBATCHNO, STRENTRYPERSON, STRCOMMENT, ACTIVE, UPDATEUSER, " &
                "  UPDATEDATETIME, CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, " &
                "  STRCREDITCARDNO " &
                "FROM FS_TRANSACTIONS " &
                "WHERE ACTIVE = '1' AND STRAIRSNUMBER = @airs " &
                "ORDER BY NUMFEEYEAR DESC"
            Dim parameter As New SqlParameter("@airs", "0413" & cboAirs.Text)

            Dim ds As New DataSet
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.TableName = "FS_Transactions"
            ds.Tables.Add(dt)

            rpt = New DepositQA11
            monitor.TrackFeature("Report." & rpt.ResourceName & ".byAirs")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Deposits")
        End If
    End Sub

#End Region

#Region "Compliance"

    Private Sub btnClassChange_Click(sender As Object, e As EventArgs) Handles btnClassChange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New ClassChanged10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "select * from VW_Class_Changed"

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Class_Changed")
            rpt.SetDataSource(ds)

            CRFeesReports.ReportSource = rpt
            CRFeesReports.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            CRFeesReports.ShowGroupTreeButton = True
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub lblNSPS1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS1.LinkClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet

            SQL = "Select * " &
            "from VW_NSPS_Status " &
            "where strnsps = 'YES' " &
            "and STRnspsexempt = '1'"

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")

            rpt = New NSPSStatus10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS Exempt - Subject but exempt")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub lblNSPS2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS2.LinkClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New NSPSStatus1_10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * " &
            "from VW_NSPS_Status " &
            "where Strnsps1 = 'YES' " &
            "and strnsps = 'NO'"

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS Subject - Not subject")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub lblNSPS3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS3.LinkClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New NSPSStatus2_10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * " &
            "from VW_NSPS_Status " &
            "where strnsps = 'YES' " &
            "and STRoperate <> 'YES'"

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS, Did not Operate")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnNoOperate_Click(sender As Object, e As EventArgs) Handles btnNoOperate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New NoOperate10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from VW_No_Operate "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_No_Operate")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Did Not Operate")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

#Region "General"

    Private Sub btnFacInfoChange_Click(sender As Object, e As EventArgs) Handles btnFacInfoChange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ds = New DataSet
            rpt = New FacilityInfo10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from VW_Facility_Info "

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Info")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Info")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

#End Region

    Private Sub btnViewStats_Click(sender As Object, e As EventArgs) Handles btnViewStats.Click
        Try

            SQL = "select * from " &
"(select  " &
"count(*) as FeeUniverse  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and Active = '1' ),  " &
"(select  " &
"count(*) as UnEnrolled  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '0' or strEnrolled is null)  " &
"and Active = '1' ),  " &
"(select  " &
"count(*) as CeaseCollections  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "' " &
"and numCurrentStatus = '12'  " &
"and strEnrolled = '1'  " &
"and Active = '1' ),  " &
"(select  " &
"count(*) as Enrolled  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numCurrentStatus <> '12'  " &
"and strEnrolled = '1'  " &
"and Active = '1'),  " &
"(select  " &
"count(*) as MailOUt  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numCurrentStatus <> '12'  " &
"and strEnrolled = '1'  " &
"and strInitialMailout = '1'  " &
"and Active = '1'),   " &
"(select  " &
"count(*) as AddOnMailOut  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numCurrentStatus <> '12'  " &
"and strEnrolled = '1'  " &
"and strInitialMailout = '0'  " &
"and Active = '1' ),   " &
"(select  " &
"count(*) as NotReported  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numcurrentstatus < '5'  " &
"and strEnrolled = '1'  " &
"and Active = '1' ) ,   " &
"(select  " &
"count(*) as InProgress  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numcurrentstatus > '4' " &
"and numCurrentStatus < '8' " &
"and strEnrolled = '1'  " &
"and Active = '1' )  ,   " &
"(select  " &
"count(*) as Finalized  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numcurrentstatus > '7' " &
"and strEnrolled = '1'  " &
"and Active = '1' " &
"and not exists (select * " &
"from fs_feeAudit " &
"where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " &
"and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " &
"and fs_feeAudit.numfeeyear = '" & cboFeeStatYear.Text & "' " &
"and fs_feeAudit.strendcollections = 'True')) ,   " &
"(select  " &
"count(*) as OnTime  " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numcurrentstatus > '4' " &
"and numcurrentstatus < '12'  " &
"and datSubmittal <= (select datFeeDueDate from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
"and Intsubmittal = '1' " &
"and strEnrolled = '1'  " &
"and Active = '1' ) ,   " &
"(select  " &
"count(*) as LateNoFees   " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numcurrentstatus > '4' " &
"and numcurrentstatus < '12'  " &
"and datSubmittal > (select datFeeDueDate from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')  " &
"and datSubmittal <= (select datAdminApplicable from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
"and Intsubmittal = '1' " &
"and strEnrolled = '1'  " &
"and Active = '1' ) ,   " &
"(select  " &
"count(*) as LateWithFees   " &
"from FS_Admin  " &
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " &
"and numcurrentstatus > '4' " &
"and numcurrentstatus < '12'  " &
"and datSubmittal > (select datAdminApplicable from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
"and Intsubmittal = '1' " &
"and strEnrolled = '1'  " &
"and Active = '1' ) ,  " &
"(select  " &
"count(*) as NotPaid  " &
"from FS_Admin  " &
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and active = '1'  " &
"and numcurrentstatus <= '8'),  " &
"(select  " &
"count(*) as OutOfBalance   " &
"from FS_Admin  " &
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and active = '1'  " &
"and (numcurrentstatus = '9' or numcurrentstatus = '11' )),  " &
                        "(select " &
"count(*) as UnderPaid " &
"from (select " &
"(numTotalFee) - sum(numAmount) as TotalPaid " &
"from FS_Admin, " &
"FS_FeeAuditedData, " &
"FS_FeeInvoice " &
"where  FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
"and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeyear " &
"and FS_Admin.strAIRSNumber = FS_FeeInvoice.strAIRSNumber " &
"and FS_Admin.numFeeYear = FS_FeeInvoice.numFeeyear " &
"and FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "' " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and FS_Admin.active = '1' " &
"and numcurrentstatus = '9' " &
"group by numtotalfee ) " &
"where totalpaid > 0 ), " &
"(select " &
"count(*) as OverPaid " &
"from (select " &
"FS_Admin.strAIRSNumber " &
"from FS_Admin, " &
"FS_FeeAuditedData, " &
"FS_FeeInvoice " &
"where  FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
"and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeyear " &
"and FS_Admin.strAIRSNumber = FS_FeeInvoice.strAIRSNumber " &
"and FS_Admin.numFeeYear = FS_FeeInvoice.numFeeyear " &
"and FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "' " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and FS_Admin.active = '1' " &
"and (numcurrentstatus = '9' or numcurrentstatus = '11' )) " &
" ), " &
"(select  " &
"count(*) as OutOfBalanceAnnual  " &
"from FS_Admin, fs_feeAuditedData   " &
"where fs_admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
"and fs_admin.nuMFeeYear  = FS_FeeAuditedData.nuMFeeYear  " &
"and FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and FS_Admin.active = '1'  " &
"and numcurrentstatus = '9' " &
"and (strPaymentPlan = 'Entire Annual Year' or strPaymentPlan is null) ),  " &
"(select  " &
"count(*) as OutOfBalanceQuarterly " &
"from FS_Admin, fs_feeAuditedData   " &
"where fs_admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
"and fs_admin.nuMFeeYear  = FS_FeeAuditedData.nuMFeeYear  " &
"and FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and FS_Admin.active = '1'  " &
"and numcurrentstatus = '9' " &
"and strPaymentPlan = 'Four Quarterly Payments'),  " &
"(select  " &
"count(*) as PaidInFull   " &
"from FS_Admin  " &
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and active = '1'  " &
"and numcurrentstatus = '10'),  " &
"(select  " &
"count(*) as FinalPaid     " &
"from FS_Admin  " &
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and active = '1'  " &
"and numcurrentstatus = '10' " &
"and intSubmittal = '1' ) ,  " &
"(select  " &
"count(*) as NotFinalPaid     " &
"from FS_Admin  " &
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and active = '1'  " &
"and numcurrentstatus = '10' " &
"and (intSubmittal = '0' or intsubmittal is null))    "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("feeUniverse")) Then
                    txtFSFeeUniverse.Text = ""
                Else
                    txtFSFeeUniverse.Text = dr.Item("FeeUniverse")
                End If
                If IsDBNull(dr.Item("Unenrolled")) Then
                    txtFSUnEnrolled.Text = ""
                Else
                    txtFSUnEnrolled.Text = dr.Item("Unenrolled")
                End If
                If IsDBNull(dr.Item("CeaseCollections")) Then
                    txtFSCeaseCollection.Text = ""
                Else
                    txtFSCeaseCollection.Text = dr.Item("CeaseCollections")
                End If
                If IsDBNull(dr.Item("Enrolled")) Then
                    txtFSEnrolled.Text = ""
                Else
                    txtFSEnrolled.Text = dr.Item("Enrolled")
                End If
                If IsDBNull(dr.Item("Mailout")) Then
                    txtFSMailout.Text = ""
                Else
                    txtFSMailout.Text = dr.Item("MailOut")
                End If
                If IsDBNull(dr.Item("AddOnMailout")) Then
                    txtFSAdditions.Text = ""
                Else
                    txtFSAdditions.Text = dr.Item("AddOnMailOut")
                End If
                If IsDBNull(dr.Item("NotReported")) Then
                    txtFSNotReported.Text = ""
                Else
                    txtFSNotReported.Text = dr.Item("NotReported")
                End If
                If IsDBNull(dr.Item("InProgress")) Then
                    txtFSInProgress.Text = ""
                Else
                    txtFSInProgress.Text = dr.Item("InProgress")
                End If
                If IsDBNull(dr.Item("Finalized")) Then
                    txtFSFinalized.Text = ""
                Else
                    txtFSFinalized.Text = dr.Item("Finalized")
                End If
                If IsDBNull(dr.Item("OnTime")) Then
                    txtFSOnTimeResponse.Text = ""
                Else
                    txtFSOnTimeResponse.Text = dr.Item("OnTime")
                End If
                If IsDBNull(dr.Item("LateNoFees")) Then
                    txtFSLateResponse.Text = ""
                Else
                    txtFSLateResponse.Text = dr.Item("LateNoFees")
                End If
                If IsDBNull(dr.Item("LateWithFees")) Then
                    txtFSLateFee.Text = ""
                Else
                    txtFSLateFee.Text = dr.Item("LateWithFees")
                End If
                If IsDBNull(dr.Item("NotPaid")) Then
                    txtFSNotPaid.Text = ""
                Else
                    txtFSNotPaid.Text = dr.Item("NotPaid")
                End If
                If IsDBNull(dr.Item("OutOfBalance")) Then
                    txtFSOutOfBalance.Text = ""
                Else
                    txtFSOutOfBalance.Text = dr.Item("OutOfBalance")
                End If
                If IsDBNull(dr.Item("UnderPaid")) Then
                    txtFSPartial.Text = ""
                Else
                    txtFSPartial.Text = dr.Item("UnderPaid")
                End If
                If IsDBNull(dr.Item("OverPaid")) Then
                    txtFSOverPaid.Text = ""
                Else
                    txtFSOverPaid.Text = dr.Item("OverPaid")
                End If
                If IsDBNull(dr.Item("OutofBalanceAnnual")) Then
                    txtFSAnnual.Text = ""
                Else
                    txtFSAnnual.Text = dr.Item("OutOfBalanceAnnual")
                End If
                If IsDBNull(dr.Item("OutOfbalanceQuarterly")) Then
                    txtFSQuarterly.Text = ""
                Else
                    txtFSQuarterly.Text = dr.Item("OutofBalanceQuarterly")
                End If
                If IsDBNull(dr.Item("PaidInFull")) Then
                    txtFSPaidInFull.Text = ""
                Else
                    txtFSPaidInFull.Text = dr.Item("PaidInFull")
                End If
                If IsDBNull(dr.Item("FinalPaid")) Then
                    txtFSPaidFinalized.Text = ""
                Else
                    txtFSPaidFinalized.Text = dr.Item("FinalPaid")
                End If
                If IsDBNull(dr.Item("NotFinalPaid")) Then
                    txtFSPaidNotFinalized.Text = ""
                Else
                    txtFSPaidNotFinalized.Text = dr.Item("NotFinalPaid")
                End If

            End While
            dr.Close()


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryFeeUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFeeUniverse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryUnEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryUnEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '0' or strEnrolled is null)  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryCeaseCollection_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryCeaseCollection.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numCurrentStatus = '12'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryMailOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryMailOut.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and strInitialMailout = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryAdditions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryAdditions.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and strInitialMailout = '0'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryNotReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryNotReported.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus < '5'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryInProgress_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryInProgress.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus > '4' " &
            "and numCurrentStatus < '8' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus > '7' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
                "and not exists (select * " &
                "from fs_feeAudit " &
                "where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " &
                "and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " &
                "and fs_feeAudit.numfeeyear = '" & cboFeeStatYear.Text & "' " &
                "and fs_feeAudit.strendcollections = 'True')" &
                "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryOnTime_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOnTime.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal <= (select datFeeDueDate from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryLateResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryLateResponse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
           "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal > (select datFeeDueDate from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')  " &
            "and datSubmittal <= (select datAdminApplicable from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryLateWithFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryLateWithFee.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal > (select datAdminApplicable from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryNotPaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryNotPaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus <= '8' " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryOutofBalance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOutofBalance.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPartial_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPartial.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If


            SQL = "Select  " &
            "substr(FSUnderPaid.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc  " &
"from (select " &
"(numTotalFee) - sum(numAmount) as TotalPaid, FS_Admin.strairsnumber " &
"from FS_Admin, FS_FeeAuditedData, " &
"FS_FeeInvoice , " &
            "FSLK_Admin_Status " &
"where  FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
"and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeyear " &
"and FS_Admin.strAIRSNumber = FS_FeeInvoice.strAIRSNumber " &
"and FS_Admin.numFeeYear = FS_FeeInvoice.numFeeyear " &
"and FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "' " &
"and (strEnrolled = '1' or strEnrolled is null)  " &
"and FS_Admin.active = '1' " &
"and numcurrentstatus = '9' " &
"group by numtotalfee )FSUnderPaid, APBFacilityInformation " &
"where totalpaid > 0   " &
"and FSUnderPaid.strAIRSNumber = APBFacilityInformation.strAIRSNumber "


            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryAnnual_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryAnnual.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
          "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
          "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
          "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                      "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and numcurrentstatus = '9' " &
          "and (strPaymentPlan = 'Entire Annual Year' or strPaymentPlan is null) " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryQuarterly_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryQuarterly.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
          "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
          "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
          "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                      "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and numcurrentstatus = '9' " &
          "and strPaymentPlan = 'Four Quarterly Payments' " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryOverpaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOverpaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If


            SQL = "Select  " &
          "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
          "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
          "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                      "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " &
          "and strPaymentPlan = 'Four Quarterly Payments' " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPaidInFull_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidInFull.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus = '10' " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPaidFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
          "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
          "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
          "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                      "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and numcurrentstatus = '10' " &
          "and intSubmittal = '1' " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPaidNotFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidNotFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
          "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
          "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
          "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                      "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and numcurrentstatus = '10' " &
          "and (intSubmittal = '0' or intsubmittal is null) " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailFeeUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailFeeUniverse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment, " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "



            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, " &
            "APBFacilityInformation.strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_Mailout.STRFIRSTNAME as strContactFirstName,  " &
            "FS_Mailout.STRLASTNAME as strContactLastName,  " &
            "FS_Mailout.STRContactCONAME as strContactCompanyName,  " &
            "FS_Mailout.STRCONTACTADDRESS1 as strContactAddress,  " &
            "FS_Mailout.STRCONTACTCITY,  " &
            "FS_Mailout.STRCONTACTSTATE,  " &
            "FS_Mailout.STRCONTACTZIPCODE,  " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1,  " &
            "APBFACILITYINFORMATION.STRFACILITYCITY,  " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE,  " &
            "FS_Mailout.strGecoUserEmail as strContactEmail,  " &
            "'' as strContactPhoneNumber, " &
            "datShutDown, FS_Mailout.strClass, " &
            "case  " &
            "when strOperate = '1' then 'Operating'  " &
            "else 'Not Operating'  " &
            "end Operating,  " &
            "case  " &
            "when FS_Mailout.strPart70 = '1' then 'True'  " &
            "else 'False'  " &
            "end Part70,  " &
            "case  " &
            "when FS_Mailout.strNSPS = '1' then 'True'  " &
            "else 'False'  " &
            "end NSPS,  " &
            "numTotalFee, sum(numPayment) as TotalPaid  " &
            "from FS_Admin, APBFacilityInformation,  " &
            "FSLK_Admin_Status,   " &
            "FS_Mailout, FS_FeeAuditedData,  " &
            "FS_Transactions  " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+)  " &
            "and FS_Admin.strAIRSNumber = FS_Mailout.strAIRSNumber (+)  " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+)  " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+)  " &
            "and FS_Admin.numFeeYear = FS_Mailout.numFeeYear (+)  " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+)  " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id  " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'    " &
            "and FS_Admin.Active = '1' GROUP BY substr(FS_Admin.strAIRSNumber, 5), APBFacilityInformation.strFacilityName, strIAIPDesc, FS_Mailout.STRFIRSTNAME, FS_Mailout.STRLASTNAME, FS_Mailout.STRContactCONAME, FS_Mailout.STRCONTACTADDRESS1, FS_Mailout.STRCONTACTCITY, FS_Mailout.STRCONTACTSTATE, FS_Mailout.STRCONTACTZIPCODE, APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_Mailout.strGecoUserEmail, '', datShutDown, FS_Admin.strComment,  FS_Mailout.strClass, case  " &
            "when strOperate = '1' then 'Operating'  " &
            "else 'Not Operating'  " &
            "end, case  " &
            "when FS_Mailout.strPart70 = '1' then 'True'  " &
            "else 'False'  " &
            "end, case  " &
            "when FS_Mailout.strNSPS = '1' then 'True'  " &
            "else 'False'  " &
            "end, numTotalFee "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailUnEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailUnEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '0' or strEnrolled is null)  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailCeaseCollection_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailCeaseCollection.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numCurrentStatus = '12'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
                  "and numCurrentStatus <> '12'  " &
          "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailMailout_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailMailout.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
              "and numCurrentStatus <> '12'  " &
          "and strEnrolled = '1'  " &
          "and strInitialMailout = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailAdditions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailAdditions.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
                  "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and strInitialMailout = '0'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailNotReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailNotReported.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, FS_Admin.strcomment, " &
            "APBFacilityInformation.strFacilityName, strIAIPDesc,  " &
            "FS_Mailout.STRFIRSTNAME, " &
            "FS_Mailout.STRLASTNAME, " &
            "FS_Mailout.STRContactCONAME, " &
            "FS_Mailout.STRCONTACTADDRESS1, " &
            "FS_Mailout.STRCONTACTCITY, " &
            "FS_Mailout.STRCONTACTSTATE, " &
            "FS_Mailout.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "datShutDown, FS_Mailout.strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when FS_Mailout.strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when FS_Mailout.strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_Mailout, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Mailout.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Mailout.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
              "and numcurrentstatus < '5'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , APBFACILITYINFORMATION.strFacilityName, " &
            "strIAIPDesc, FS_Mailout.STRFIRSTNAME, " &
            "FS_Mailout.STRLastNAME, FS_Mailout.STRContactCONAME, " &
            "FS_Mailout.STRCONTACTADDRESS1, FS_Mailout.STRCONTACTCITY, " &
            "FS_Mailout.STRCONTACTSTATE, FS_Mailout.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "datShutDown, FS_Mailout.strClass, " &
            "StrOperate, " &
            "FS_Mailout.strPart70," &
            "FS_Mailout.strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCONAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCONAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            'dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            'dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            'dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            'dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 14
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 15
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 16
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 17
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 18

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 19
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 20
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailInProgress_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailInProgress.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
              "and numcurrentstatus > '4' " &
         "and numCurrentStatus < '8' " &
         "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"

            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
              "and numcurrentstatus > '7' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
               "and not exists (select * " &
                "from fs_feeAudit " &
                "where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " &
                "and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " &
                "and fs_feeAudit.numfeeyear = '" & cboFeeStatYear.Text & "' " &
                "and fs_feeAudit.strendcollections = 'True')" &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "




            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True


            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailLateResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailLateResponse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal > (select datFeeDueDate from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')  " &
            "and datSubmittal <= (select datAdminApplicable from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailLateWithFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailLateWithFee.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and numcurrentstatus > '4' " &
           "and numcurrentstatus < '12'  " &
          "and datSubmittal > (select datAdminApplicable from FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " &
          "and Intsubmittal = '1' " &
          "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "


            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailNotPaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailNotPaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "SELECT   SUBSTR(AD.STRAIRSNUMBER, 5) AS strAIRSNumber, " &
            "    FI.STRFACILITYNAME, " &
            "    LK_AS.STRIAIPDESC, " &
            "    AD.STRCOMMENT, " &
            "    CI.STRCONTACTFIRSTNAME, " &
            "    CI.STRCONTACTLASTNAME, " &
            "    CI.STRCONTACTCOMPANYNAME, " &
            "    CI.STRCONTACTADDRESS, " &
            "    CI.STRCONTACTCITY, " &
            "    CI.STRCONTACTSTATE, " &
            "    CI.STRCONTACTZIPCODE, " &
            "    FI.STRFACILITYSTREET1, " &
            "    FI.STRFACILITYCITY, " &
            "    FI.STRFACILITYZIPCODE, " &
            "    CI.STRCONTACTEMAIL, " &
            "    CI.STRCONTACTPHONENUMBER, " &
            "    FAD.DATSHUTDOWN, " &
            "    FAD.STRCLASS, " &
            "    CASE " &
            "      WHEN FAD.STROPERATE = '1' " &
            "      THEN 'Operating' " &
            "      ELSE 'Not Operating' " &
            "    END Operating, " &
            "    CASE " &
            "      WHEN FAD.STRPART70 = '1' " &
            "      THEN 'True' " &
            "      ELSE 'False' " &
            "    END Part70, " &
            "    CASE " &
            "      WHEN FAD.STRNSPS = '1' " &
            "      THEN 'True' " &
            "      ELSE 'False' " &
            "    END NSPS, " &
            "    FAD.NUMTOTALFEE, " &
            "    SUM(TRX.NUMPAYMENT) AS TotalPaid " &
            "  FROM FS_Admin AD, " &
            "    APBFacilityInformation FI, " &
            "    FSLK_Admin_Status LK_AS, " &
            "    FS_ContactInfo CI, " &
            "    FS_FeeAuditedData FAD, " &
            "    (SELECT * FROM FS_Transactions TR WHERE TR.ACTIVE = '1' " &
            "    ) TRX " &
            "  WHERE AD.STRAIRSNUMBER     = FI.STRAIRSNUMBER " &
            "    AND AD.STRAIRSNUMBER     = FAD.STRAIRSNUMBER(+) " &
            "    AND AD.STRAIRSNUMBER     = CI.STRAIRSNUMBER(+) " &
            "    AND AD.STRAIRSNUMBER     = TRX.STRAIRSNUMBER(+) " &
            "    AND AD.NUMFEEYEAR        = FAD.NUMFEEYEAR(+) " &
            "    AND AD.NUMFEEYEAR        = CI.NUMFEEYEAR(+) " &
            "    AND AD.NUMFEEYEAR        = TRX.NUMFEEYEAR(+) " &
            "    AND AD.NUMCURRENTSTATUS  = LK_AS.ID " &
            "    AND AD.NUMFEEYEAR        = '" & cboFeeStatYear.Text & "' " &
            "    AND (AD.STRENROLLED      = '1' " &
            "    OR AD.STRENROLLED       IS NULL) " &
            "    AND AD.NUMCURRENTSTATUS <= '8' " &
            "    AND AD.ACTIVE            = '1' " &
            "  GROUP BY FI.STRFACILITYNAME, " &
            "    LK_AS.STRIAIPDESC, " &
            "    AD.STRCOMMENT, " &
            "    CI.STRCONTACTFIRSTNAME, " &
            "    CI.STRCONTACTLASTNAME, " &
            "    CI.STRCONTACTCOMPANYNAME, " &
            "    CI.STRCONTACTADDRESS, " &
            "    CI.STRCONTACTCITY, " &
            "    CI.STRCONTACTSTATE, " &
            "    CI.STRCONTACTZIPCODE, " &
            "    FI.STRFACILITYSTREET1, " &
            "    FI.STRFACILITYCITY, " &
            "    FI.STRFACILITYZIPCODE, " &
            "    CI.STRCONTACTEMAIL, " &
            "    CI.STRCONTACTPHONENUMBER, " &
            "    FAD.DATSHUTDOWN, " &
            "    FAD.STRCLASS, " &
            "    FAD.NUMTOTALFEE, " &
            "    AD.STRAIRSNUMBER, " &
            "    FAD.STROPERATE, " &
            "    FAD.STRPART70, " &
            "    FAD.STRNSPS " &
            "  ORDER BY strAIRSNumber"

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailOutOfBalance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailOutOfBalance.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
             "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "



            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPartial_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPartial.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailAnnual_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailAnnual.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailQuarterly_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailQuarterly.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailOverpaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailOverpaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPaidInFull_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPaidInFull.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
                 "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus = '10' " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "


            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPaidFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPaidFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, " &
            "FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus = '10' " &
            "and intSubmittal = '1' " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strComment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPaidNotFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPaidNotFinalized.LinkClicked

        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " &
            "substr(FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, " &
            "FS_Admin.strComment,  " &
            "FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, " &
            "FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, " &
            "FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, " &
            "FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, " &
            "APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " &
            "FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, " &
            "datShutDown, strClass, " &
            "case " &
            "when strOperate = '1' then 'Operating' " &
            "else 'Not Operating' " &
            "end Operating, " &
            "case " &
            "when strPart70 = '1' then 'True' " &
            "else 'False' " &
            "end Part70, " &
            "case " &
            "when strNSPS = '1' then 'True' " &
            "else 'False' " &
            "end NSPS, " &
            "numTotalFee, sum(numPayment) as TotalPaid " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status,  " &
            "FS_ContactInfo, FS_FeeAuditedData, " &
            "FS_Transactions " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber (+) " &
            "and FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber (+) " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear (+) " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear (+) " &
            "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus = '10' " &
            "and (intSubmittal = '0' or intsubmittal is null) " &
            "and FS_Admin.Active = '1' " &
            "group by FS_Admin.strAIRSNumber , strFacilityName, " &
            "strIAIPDesc, FS_ContactInfo.STRCONTACTFIRSTNAME, " &
            "FS_ContactInfo.STRCONTACTLASTNAME, FS_ContactInfo.STRContactCOMPANYNAME, " &
            "FS_ContactInfo.STRCONTACTADDRESS, FS_ContactInfo.STRCONTACTCITY, " &
            "FS_ContactInfo.STRCONTACTSTATE, FS_ContactInfo.STRCONTACTZIPCODE, " &
            "APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, " &
            "APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_ContactInfo.STRCONTACTEMAIL, " &
            "FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " &
            "StrOperate, " &
            "strPart70," &
            "strNSPS, " &
            "numTotalFee, FS_Admin.strcomment " &
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeStats.DataSource = dsViewCount
            dgvFeeStats.DataMember = "ViewCount"

            dgvFeeStats.RowHeadersVisible = False
            dgvFeeStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeStats.AllowUserToResizeColumns = True
            dgvFeeStats.AllowUserToAddRows = False
            dgvFeeStats.AllowUserToDeleteRows = False
            dgvFeeStats.AllowUserToOrderColumns = True
            dgvFeeStats.AllowUserToResizeRows = True

            dgvFeeStats.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeStats.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeStats.Columns("STRAIRSNUMBER").Width = dgvFeeStats.Width * 0.2
            dgvFeeStats.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeStats.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeStats.Columns("strFacilityName").Width = dgvFeeStats.Width * 0.8
            dgvFeeStats.Columns("strIAIPDesc").HeaderText = "Fee Status"
            dgvFeeStats.Columns("strIAIPDesc").DisplayIndex = 2
            dgvFeeStats.Columns("strIAIPDesc").Width = dgvFeeStats.Width * 0.5
            dgvFeeStats.Columns("strComment").HeaderText = "Fee Statistics Comment"
            dgvFeeStats.Columns("strComment").DisplayIndex = 3
            dgvFeeStats.Columns("strComment").Width = dgvFeeStats.Width * 0.5

            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeStats.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvFeeStats.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeStats.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvFeeStats.Columns("STRContactCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeStats.Columns("STRContactCOMPANYNAME").DisplayIndex = 6
            dgvFeeStats.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeStats.Columns("STRCONTACTADDRESS").DisplayIndex = 7
            dgvFeeStats.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeStats.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvFeeStats.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeStats.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvFeeStats.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeStats.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvFeeStats.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeStats.Columns("STRFACILITYSTREET1").DisplayIndex = 11
            dgvFeeStats.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeStats.Columns("STRFACILITYCITY").DisplayIndex = 12
            dgvFeeStats.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeStats.Columns("STRFACILITYZIPCODE").DisplayIndex = 13
            dgvFeeStats.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeStats.Columns("STRCONTACTEMAIL").DisplayIndex = 14
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").HeaderText = "Contact Phone Number"
            dgvFeeStats.Columns("STRCONTACTPhoneNumber").DisplayIndex = 15
            dgvFeeStats.Columns("datShutDown").HeaderText = "Date Shut Down"
            dgvFeeStats.Columns("datShutDown").DisplayIndex = 16
            dgvFeeStats.Columns("datShutDown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeStats.Columns("strClass").HeaderText = "Classification"
            dgvFeeStats.Columns("strClass").DisplayIndex = 17
            dgvFeeStats.Columns("Operating").HeaderText = "Operating"
            dgvFeeStats.Columns("Operating").DisplayIndex = 18
            dgvFeeStats.Columns("Part70").HeaderText = "Part 70"
            dgvFeeStats.Columns("Part70").DisplayIndex = 19
            dgvFeeStats.Columns("NSPS").HeaderText = "NSPS"
            dgvFeeStats.Columns("NSPS").DisplayIndex = 20

            dgvFeeStats.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeStats.Columns("numTotalFee").DisplayIndex = 21
            dgvFeeStats.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeStats.Columns("TotalPaid").HeaderText = "Total Paid"
            dgvFeeStats.Columns("TotalPaid").DisplayIndex = 22
            dgvFeeStats.Columns("TotalPaid").DefaultCellStyle.Format = "c"


            txtFeeStatsCount.Text = dgvFeeStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportFeeStats_Click(sender As Object, e As EventArgs) Handles btnExportFeeStats.Click
        dgvFeeStats.ExportToExcel(Me)
    End Sub

    Private Sub dgvFeeStats_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvFeeStats.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvFeeStats.HitTest(e.X, e.Y)
            If dgvFeeStats.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFeeStats.Columns(0).HeaderText = "Airs No." Then
                    If IsDBNull(dgvFeeStats(0, hti.RowIndex).Value) Then
                        txtFeeStatAirsNumber.Clear()
                    Else
                        txtFeeStatAirsNumber.Text = dgvFeeStats(0, hti.RowIndex).Value
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckInvoices_Click(sender As Object, e As EventArgs) Handles btnCheckInvoices.Click
        Try
            If cboFeeStatYear.Text <> "" Then
                Dim query As String = "Update FS_FeeInvoice set " &
                "strInvoiceStatus = '1', " &
                "UpdateUser = @Username,  " &
                "updateDateTime = sysdate " &
                "where numFeeYear = @FeeYear " &
                "and numAmount = '0' " &
                "and strInvoiceStatus = '0' " &
                "and active = '1' "

                Dim parameters As SqlParameter() = New SqlParameter() {
                    New SqlParameter("@Username", CurrentUser.AlphaName),
                    New SqlParameter("@FeeYear", cboFeeStatYear.Text)
                }

                If Not DB.RunCommand(query, parameters) Then
                    MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                query = "Select " &
                "strAirsnumber " &
                "from FS_FeeInvoice " &
                "where numAmount = '0' " &
                "and strInvoiceStatus = '1' " &
                "and Active = '1' " &
                "and updateUser = @Username " &
                "and numFeeyear = @FeeYear "

                Using connection As New SqlConnection(CurrentConnectionString)
                    Using cmd As SqlCommand = connection.CreateCommand
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = query

                        cmd.Parameters.AddRange(parameters)
                        cmd.Connection.Open()
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If Not IsDBNull(dr.Item("strAIRSNumber")) Then
                                DAL.Update_FS_Admin_Status(cboFeeStatYear.Text, dr.Item("strAIRSNumber"))
                            End If
                        End While
                        dr.Close()
                        cmd.Connection.Close()
                    End Using
                End Using

                MsgBox("Fee Invoices validated.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnInvoicedPaymentDue_Click(sender As Object, e As EventArgs) Handles btnInvoicedPaymentDue.Click
        Try
            SQL = " "


            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " &
"substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
"strFacilityName, " &
"case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
"numAmount as Due,  " &
"FS_FeeInvoice.numFeeYear,   " &
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
"numAmount as numTotalFee, strClass, '' as numAdminFee   " &
"From APBFacilityInformation, FS_FeeInvoice, " &
"APBHeaderData, FS_Admin " &
"where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
"and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
"and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
"and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  "

                Case "ANNUAL"
                    SQL = "select " &
  "substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
  "strFacilityName, " &
  "case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
 "numAmount as Due,  " &
 "FS_FeeInvoice.numFeeYear,   " &
 "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
 "numAmount as numTotalFee, strClass, '' as numAdminFee   " &
  "From APBFacilityInformation, FS_FeeInvoice, " &
  "APBHeaderData, FS_Admin  " &
  "where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
  "and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
  "and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " &
  "and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
  " and strPayType = '1' "

                Case "ALL QUARTERS"
                    SQL = "select " &
"substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
"strFacilityName, " &
"case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
"numAmount as Due,  " &
"FS_FeeInvoice.numFeeYear,   " &
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
"numAmount as numTotalFee, strClass, '' as numAdminFee   " &
"From APBFacilityInformation, FS_FeeInvoice, " &
"APBHeaderData, FS_Admin  " &
"where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
"and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
"and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
"and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " &
"and (FS_FeeInvoice.strPayType = '2' " &
"or FS_FeeInvoice.strPayType = '3' " &
"or FS_FeeInvoice.strPayType = '4' " &
"or FS_FeeInvoice.strPayType = '5') "
                Case "QUARTER ONE"
                    SQL = "select " &
"substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
"strFacilityName, " &
"case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
"numAmount as Due,  " &
"FS_FeeInvoice.numFeeYear,   " &
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
"From APBFacilityInformation, FS_FeeInvoice, " &
"APBHeaderData, FS_Admin  " &
"where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
"and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
"and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
"and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " &
"and FS_FeeInvoice.strPayType = '2'  "
                Case "QUARTER TWO"
                    SQL = "select " &
"substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
"strFacilityName, " &
"case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
"numAmount as Due,  " &
"FS_FeeInvoice.numFeeYear,   " &
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
"From APBFacilityInformation, FS_FeeInvoice, " &
"APBHeaderData, FS_Admin  " &
"where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
"and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
"and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
"and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " &
"and FS_FeeInvoice.strPayType = '3'  "
                Case "QUARTER THREE"
                    SQL = "select " &
"substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
"strFacilityName, " &
"case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
"numAmount as Due,  " &
"FS_FeeInvoice.numFeeYear,   " &
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
"From APBFacilityInformation, FS_FeeInvoice, " &
"APBHeaderData, FS_Admin  " &
"where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
"and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
"and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
"and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " &
"and FS_FeeInvoice.strPayType = '4'  "
                Case "QUARTER FOUR"
                    SQL = "select " &
"substr(APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " &
"strFacilityName, " &
"case " &
"when strPayType = '1' then 'ANNUAL' " &
"when strPayType = '2' then 'QUARTER ONE' " &
"when strPayType = '3' then 'QUARTER TWO' " &
"when strPayType = '4' then 'QUARTER THREE' " &
"when strPayType = '5' then 'QUARTER FOUR' " &
"End strPaymentPlan, " &
"numAmount as Due,  " &
"FS_FeeInvoice.numFeeYear,   " &
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
"From APBFacilityInformation, FS_FeeInvoice, " &
"APBHeaderData, FS_Admin  " &
"where APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
"and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
"and FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
"and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
"and FS_FeeInvoice.Active = '1' " &
"and FS_Admin.Active = '1' " &
"and numCurrentStatus <> '12'  " &
"and FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " &
"and  FS_FeeInvoice.strPayType = '5'  "
                Case "AMENDMENT", "ONE-TIME", "REFUND"
                Case Else
            End Select

            If SQL <> "" Then

                ds = New DataSet
                da = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                da.Fill(ds, "PaymentDue")
                dgvDepositsAndPayments.DataSource = ds
                dgvDepositsAndPayments.DataMember = "PaymentDue"

                dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvDepositsAndPayments.AllowUserToResizeColumns = True
                dgvDepositsAndPayments.AllowUserToResizeRows = True
                dgvDepositsAndPayments.AllowUserToAddRows = False
                dgvDepositsAndPayments.AllowUserToDeleteRows = False
                dgvDepositsAndPayments.AllowUserToOrderColumns = True
                dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
                dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
                dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
                dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
                dgvDepositsAndPayments.Columns("strPaymentPlan").HeaderText = "Payment Plan"
                dgvDepositsAndPayments.Columns("strPaymentPlan").DisplayIndex = 2
                Select Case cboStatPayType.Text
                    Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                        dgvDepositsAndPayments.Columns("Due").HeaderText = "Amount Invoiced per Quarter"
                    Case Else
                        dgvDepositsAndPayments.Columns("Due").HeaderText = "Amount Invoiced"
                End Select
                dgvDepositsAndPayments.Columns("Due").DisplayIndex = 3
                dgvDepositsAndPayments.Columns("Due").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
                dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4

                dgvDepositsAndPayments.Columns("strClass").HeaderText = "Classification"
                dgvDepositsAndPayments.Columns("strClass").DisplayIndex = 5

                dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
                dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 6
                dgvDepositsAndPayments.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
                dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 7
                dgvDepositsAndPayments.Columns("numSMFee").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
                dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 8
                dgvDepositsAndPayments.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
                dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 9
                dgvDepositsAndPayments.Columns("numTotalFee").DefaultCellStyle.Format = "c"

                dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
                dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 10
                dgvDepositsAndPayments.Columns("numAdminFee").DefaultCellStyle.Format = "c"

                txtCount.Text = dgvDepositsAndPayments.RowCount.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenFeesLog_Click(sender As Object, e As EventArgs) Handles btnOpenFeesLog.Click
        Dim parameters As New Generic.Dictionary(Of BaseForm.FormParameter, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtFeeStatAirsNumber.Text) Then
            parameters(FormParameter.AirsNumber) = txtFeeStatAirsNumber.Text
        End If
        parameters(FormParameter.FeeYear) = cboFeeStatYear.Text

        OpenSingleForm(PASPFeeAuditLog, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub btnInvoiceReportVariance_Click(sender As Object, e As EventArgs) Handles btnInvoiceReportVariance.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " &
  "VarianceReport.strAIRSNumber, strFacilityName, " &
  "numFeeyear, AmountDue, " &
  "PayerType  " &
  "from ( " &
  "select " &
  "strAIRSNumber, numFeeYear, " &
  "numtotalfee as AmountDue, " &
  "to_char(strPaymentPlan) as PayerType " &
  "from FS_FeeAuditedData " &
  "where fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " &
  "and fs_feeAuditedData.Active = '1' " &
  "and not exists (select * " &
  "from FS_FeeInvoice " &
  "where fs_feeInvoice.nuMFeeyear = '" & cboStatYear.Text & "' " &
  "and FS_FeeAuditedData.strAIRSNumber = FS_FeeInvoice.strAIRSNumber " &
  "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeYear " &
  "and FS_feeInvoice.active = '1' ) " &
  "union " &
  "select " &
  "strAIRSNumber, numfeeyear, " &
  "numAmount as AmountDue, " &
  "to_char(strPaytype) as PayerType  " &
  "from FS_FeeInvoice " &
  "where fs_feeInvoice.numFeeyear = '" & cboStatYear.Text & "' " &
  "and fs_feeInvoice.active = '1' " &
  "and not exists (select * " &
  "from FS_FeeAuditedData " &
  "where fs_feeAuditedData.nuMfeeyear = '" & cboStatYear.Text & "' " &
  "and FS_FeeAuditedData.active = '1' " &
  "and FS_FeeInvoice.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
  "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear ) )VarianceReport , " &
  "APBfacilityinformation " &
  "where VarianceREport.strAIRSNumber = APBFacilityInformation.strAIRSNumber "






                    SQL = "select " &
                     "substr(VarianceReport.strAIRSNumber, 5) as strAIRSNumber, " &
                     "strFacilityName, " &
                     "numFeeyear, AmountDue, " &
                     "case " &
                     "when PayerType = '1' then 'ANNUAL' " &
                     "when PayerType = '2' then 'QUARTERLY' " &
                     "else PayerType " &
                     "End PayerType  " &
                     "from ( " &
                   "select   " &
                    "strAIRSNumber, numFeeYear,   " &
                    "numtotalfee as AmountDue,   " &
                    "to_char(strPaymentPlan) as PayerType   " &
                    "from FS_FeeAuditedData   " &
                    "where fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "'   " &
                    "and fs_feeAuditedData.Active = '1'   " &
                    "and not exists (select *   " &
                    "from FS_FeeInvoice   " &
                    "where fs_feeInvoice.nuMFeeyear = '" & cboStatYear.Text & "'   " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                    "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeYear   " &
                    "and FS_feeInvoice.active = '1' ) " &
                    "and numtotalfee is not null  " &
                    "union  " &
                    "select   " &
                    "strAIRSNumber, numfeeyear,   " &
                    "numAmount as AmountDue,   " &
                    "to_char(strPaytype) as PayerType    " &
                    "from FS_FeeInvoice   " &
                    "where fs_feeInvoice.numFeeyear = '" & cboStatYear.Text & "'   " &
                    "and fs_feeInvoice.active = '1'   " &
                    "and not exists (select *   " &
                    "from FS_FeeAuditedData   " &
                    "where fs_feeAuditedData.nuMfeeyear = '" & cboStatYear.Text & "'   " &
                    "and FS_FeeAuditedData.active = '1'   " &
                    "and FS_FeeInvoice.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber   " &
                    "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear )  " &
                    "union   " &
                    "select   " &
                    "strAIRSNumber, numfeeyear,   " &
                    "numAmount as AmountDue,   " &
                    "to_char(strPaytype) as PayerType    " &
                    "from FS_FeeInvoice   " &
                    "where fs_feeInvoice.numFeeyear = '" & cboStatYear.Text & "'   " &
                    "and fs_feeInvoice.active = '1'   " &
                    "and  exists (select *   " &
                    "from FS_FeeAuditedData   " &
                    "where fs_feeAuditedData.nuMfeeyear = '" & cboStatYear.Text & "'   " &
                    "and FS_FeeAuditedData.active = '1'   " &
                    "and FS_FeeInvoice.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber   " &
                    "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear  " &
                    "and numamount <> numTotalFee )  " &
                    "and strPaytype = '1'  " &
                    "union   " &
                    "select   " &
                    "strAIRSNumber, numFeeYear,   " &
                    "numtotalfee as AmountDue,   " &
                    "to_char(strPaymentPlan) as PayerType   " &
                    "from FS_FeeAuditedData   " &
                    "where fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "'   " &
                    "and fs_feeAuditedData.Active = '1'   " &
                    "and  exists (select *   " &
                    "from FS_FeeInvoice   " &
                    "where fs_feeInvoice.nuMFeeyear = '" & cboStatYear.Text & "'   " &
                    "and FS_FeeAuditedData.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                    "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeYear   " &
                    "and FS_feeInvoice.active = '1' " &
                    "and numamount <> numTotalFee) " &
                    "and numtotalfee is not null " &
                    "and strPaymentPlan = 'Entire Annual Year'  ) VarianceReport , " &
                     "APBfacilityinformation " &
                     "where VarianceREport.strAIRSNumber = APBFacilityInformation.strAIRSNumber "

                Case "ANNUAL"

                Case "ALL QUARTERS"

                Case "QUARTER ONE"

                Case "QUARTER TWO"

                Case "QUARTER THREE"

                Case "QUARTER FOUR"

                Case "AMENDMENT"

                Case "ONE-TIME"

                Case "REFUND"

                Case Else

            End Select

            SQL = "select substr(VarianceReport.strAIRSNumber, 5) as strAIRSNumber, strFacilityname, " &
            "numfeeyear, to_number(TotalInvoiced) as TotalInvoiced, to_number(TotalReported) as TotalReported  " &
            "from  " &
            "(select  " &
            "INvoiced.strAIRSnumber, INvoiced.numFeeyear, to_char(INvoiced.totalDue) as TotalInvoiced,  " &
            "to_char(Reported.TotalDue) as TotalReported  " &
            "from  " &
            "(select " &
            "strAIRSNumber, numFeeyear, sum(numAmount) as totalDue  " &
            "from FS_FeeInvoice  " &
            "where numfeeyear = '" & cboStatYear.Text & "' and strPayType = '1'  " &
            "and active = '1'  " &
            "group by strAIRSNumber, numFeeyear) INvoiced,    " &
            "(select strAIRSNumber, numFeeyear, sum(numtotalFee) As totaldue  " &
            "from FS_FeeAuditedData   " &
            "where numFeeyear = '" & cboStatYear.Text & "'  " &
            "and (strPaymentPlan like 'Entire Annual Year' or strPaymentPlan = '1') " &
            "and active = '1'  " &
            "group by strAIRSNumber, numFeeyear ) Reported   " &
            "where Invoiced.strAIRSNumber = Reported.strAIRSNumber (+)  " &
            "and (Invoiced.TotalDue <> Reported.TotalDue or  reported.totaldue is null)  " &
            "union  " &
            "select strAIRSNumber, numFeeYear, to_char(sum(numAmount)) as TotalInvoiced,  " &
            "'' as TotalReported " &
            "from FS_FeeInvoice  " &
            "where not exists (select * from FS_FeeAuditedData  " &
            "where FS_FeeAuditedData.strAIRSnumber = FS_FeeInvoice.strAIRSnumber  " &
            "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeyear  " &
            "and numfeeyear = '" & cboStatYear.Text & "'  " &
            "and FS_FeeAuditedData.active = '1'  " &
            "and FS_FeeInvoice.active = '1')  " &
            "and numfeeyear = '" & cboStatYear.Text & "'  " &
            "and strPayType = '1'  " &
            "and active = '1'  " &
            "group by strAIRSNumber, numFeeYear, ''  " &
            "union  " &
            "select strairsnumber, numfeeyear, '' as TotalInvoiced, to_char(sum(numTotalFee)) as TotalReported  " &
            "from FS_FeeAuditedData  " &
            "where active = '1' and numFeeYear = '" & cboStatYear.Text & "'  " &
            "and strPaymentPlan like 'Entire Annual Year'  " &
            "and not exists (select * from FS_FeeInvoice  " &
            "where FS_FeeInvoice.strAIRSnumber = FS_FeeAuditedData.strAIRSnumber " &
            "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear  " &
            "and numfeeyear = '" & cboStatYear.Text & "' and active = '1')  " &
            "group by strairsnumber, numfeeyear, '' ) VarianceReport, APBFacilityInformation  " &
            "where VarianceReport.strAIRSNumber = APBFacilityInformation.strAIRSNumber   " &
            "order by strairsnumber "
            ds = New DataSet

            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "VarianceReport")
            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "VarianceReport"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("strAIRSNumber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("strAIRSNumber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300

            dgvDepositsAndPayments.Columns("numfeeyear").HeaderText = "Fee Year"
            dgvDepositsAndPayments.Columns("numfeeyear").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("TotalInvoiced").HeaderText = "Invoiced Amount"
            dgvDepositsAndPayments.Columns("TotalInvoiced").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("TotalInvoiced").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("TotalReported").HeaderText = "Reported Amount"
            dgvDepositsAndPayments.Columns("TotalReported").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("TotalReported").DefaultCellStyle.Format = "c"

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

            Exit Sub
            dgvDepositsAndPayments.Columns("PayerType").HeaderText = "Payment Plan"
            dgvDepositsAndPayments.Columns("PayerType").DisplayIndex = 2
            Select Case cboStatPayType.Text
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    dgvDepositsAndPayments.Columns("AmountDue").HeaderText = "Amount Reported per Quarter"
                Case Else
                    dgvDepositsAndPayments.Columns("AmountDue").HeaderText = "Amount Reported"
            End Select
            dgvDepositsAndPayments.Columns("AmountDue").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("AmountDue").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewInvoicedBalance_Click(sender As Object, e As EventArgs) Handles btnViewInvoicedBalance.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " &
                    "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                    "strFacilityName, numFeeyear, " &
                    "Payment, AmountOwed, Balance, PayType, FeeReported " &
                    "from " &
                    "(select " &
                    "Invoices.strairsnumber, strFacilityname, " &
                    "Payment, AmountOwed, numFeeYear, " &
                    "numTotalfee as FeeReported, " &
                    "case " &
                    "when (AmountOwed - Payment) = 0 then 0 " &
                    "when (AmountOwed - Payment) is null then AmountOwed " &
                    "else (AmountOwed - Payment) " &
                    "end Balance ,  PayType  " &
                    "from " &
                    "(select strairsnumber, sum(numpayment)  as Payment " &
                    "from FS_Transactions " &
                    "where numFeeyear = '" & cboStatYear.Text & "' " &
                    "and active = '1' group by strairsnumber) Transactions, " &
                    "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                    "numFeeYear, " &
                    "case " &
                    "when strPayType = '1' then 'Annual Payer' " &
                    "Else 'Quarterly Payer' " &
                    "end PayType " &
                    "from FS_FeeInvoice " &
                    "where numfeeyear = '" & cboStatYear.Text & "' " &
                    "and active = 1  group by strairsnumber, numFeeYear, case " &
                    "when strPayType = '1' then 'Annual Payer' " &
                    "Else 'Quarterly Payer' " &
                    "end) Invoices, " &
                    "(select strairsnumber,  " &
                      "numTotalfee " &
                      "from FS_FeeAuditedData " &
                      "where numfeeyear = '" & cboStatYear.Text & "' " &
                      "and active = '1' ) Reported, " &
                    "APBfacilityInformation  " &
                    "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                    "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                    "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                    " order by strairsnumber) allData "

                Case "ANNUAL"
                    SQL = "select " &
                    "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                    "strFacilityName, numFeeyear, " &
                    "Payment, AmountOwed, Balance, PayType, FeeReported " &
                    "from " &
                    "(select " &
                    "Invoices.strairsnumber, strFacilityname, " &
                    "Payment, AmountOwed, numFeeYear, " &
                    "numTotalfee as FeeReported, " &
                    "case " &
                    "when (AmountOwed - Payment) = 0 then 0 " &
                    "when (AmountOwed - Payment) is null then AmountOwed " &
                    "else (AmountOwed - Payment) " &
                    "end Balance ,  PayType  " &
                    "from " &
                    "(select strairsnumber, sum(numpayment)  as Payment " &
                    "from FS_Transactions " &
                    "where numFeeyear = '" & cboStatYear.Text & "' " &
                    "and active = '1' group by strairsnumber) Transactions, " &
                    "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                    "numFeeYear, " &
                    "case " &
                    "when strPayType = '1' then 'Annual Payer' " &
                    "Else 'Quarterly Payer' " &
                    "end PayType " &
                    "from FS_FeeInvoice " &
                    "where numfeeyear = '" & cboStatYear.Text & "' " &
                    "and active = 1 " &
                    "and strPayType = '1' " &
                    " group by strairsnumber, numFeeYear, case " &
                    "when strPayType = '1' then 'Annual Payer' " &
                    "Else 'Quarterly Payer' " &
                    "end) Invoices, " &
                    "(select strairsnumber,  " &
                      "numTotalfee " &
                      "from FS_FeeAuditedData " &
                      "where numfeeyear = '" & cboStatYear.Text & "' " &
                      "and active = '1' ) Reported, " &
                    "APBfacilityInformation  " &
                    "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                    "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                    "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                    " order by strairsnumber) allData "
                Case "ALL QUARTERS"
                    SQL = "select " &
                           "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                           "strFacilityName, numFeeyear, " &
                           "Payment, AmountOwed, Balance, PayType, FeeReported " &
                           "from " &
                           "(select " &
                           "Invoices.strairsnumber, strFacilityname, " &
                           "Payment, AmountOwed, numFeeYear, " &
                           "numTotalfee as FeeReported, " &
                           "case " &
                           "when (AmountOwed - Payment) = 0 then 0 " &
                           "when (AmountOwed - Payment) is null then AmountOwed " &
                           "else (AmountOwed - Payment) " &
                           "end Balance ,  PayType  " &
                           "from " &
                           "(select strairsnumber, sum(numpayment)  as Payment " &
                           "from FS_Transactions " &
                           "where numFeeyear = '" & cboStatYear.Text & "' " &
                           "and active = '1' group by strairsnumber) Transactions, " &
                           "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                           "numFeeYear, " &
                           "case " &
                           "when strPayType = '1' then 'Annual Payer' " &
                           "Else 'Quarterly Payer' " &
                           "end PayType " &
                           "from FS_FeeInvoice " &
                           "where numfeeyear = '" & cboStatYear.Text & "' " &
                           "and active = 1 " &
                           "and strPayType <> '1' " &
                           " group by strairsnumber, numFeeYear, case " &
                           "when strPayType = '1' then 'Annual Payer' " &
                           "Else 'Quarterly Payer' " &
                           "end) Invoices, " &
                           "(select strairsnumber,  " &
                             "numTotalfee " &
                             "from FS_FeeAuditedData " &
                             "where numfeeyear = '" & cboStatYear.Text & "' " &
                             "and active = '1' ) Reported, " &
                           "APBfacilityInformation  " &
                           "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                           "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                           "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                           " order by strairsnumber) allData "
                Case "QUARTER ONE"
                    SQL = "select " &
                  "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                  "strFacilityName, numFeeyear, " &
                  "Payment, AmountOwed, Balance, PayType, FeeReported " &
                  "from " &
                  "(select " &
                  "Invoices.strairsnumber, strFacilityname, " &
                  "Payment, AmountOwed, numFeeYear, " &
                  "numTotalfee as FeeReported, " &
                  "case " &
                  "when (AmountOwed - Payment) = 0 then 0 " &
                  "when (AmountOwed - Payment) is null then AmountOwed " &
                  "else (AmountOwed - Payment) " &
                  "end Balance ,  PayType  " &
                  "from " &
                   "(select fs_Transactions.strairsnumber, " &
                    "sum(numpayment) as Payment " &
                    "from fs_Transactions, FS_FeeInvoice  " &
                    "where fs_Transactions.invoiceid = FS_FeeInvoice.invoiceid  " &
                    "and fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " &
                      "and fs_transactions.active = '1' " &
                    "and strPaytype = '2' group by fs_Transactions.strairsnumber " &
                    ")Transactions, " &
                  "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                  "numFeeYear, " &
                  "case " &
                  "when strPayType = '1' then 'Annual Payer' " &
                  "Else 'Quarterly Payer' " &
                  "end PayType " &
                  "from FS_FeeInvoice " &
                  "where numfeeyear = '" & cboStatYear.Text & "' " &
                  "and active = 1 " &
                  "and strPayType = '2' " &
                  " group by strairsnumber, numFeeYear, case " &
                  "when strPayType = '1' then 'Annual Payer' " &
                  "Else 'Quarterly Payer' " &
                  "end) Invoices, " &
                  "(select strairsnumber,  " &
                    "numTotalfee " &
                    "from FS_FeeAuditedData " &
                    "where numfeeyear = '" & cboStatYear.Text & "' " &
                    "and active = '1' ) Reported, " &
                  "APBfacilityInformation  " &
                  "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                  "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                  "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                  " order by strairsnumber) allData "
                Case "QUARTER TWO"
                    SQL = "select " &
                     "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                     "strFacilityName, numFeeyear, " &
                     "Payment, AmountOwed, Balance, PayType, FeeReported " &
                     "from " &
                     "(select " &
                     "Invoices.strairsnumber, strFacilityname, " &
                     "Payment, AmountOwed, numFeeYear, " &
                     "numTotalfee as FeeReported, " &
                     "case " &
                     "when (AmountOwed - Payment) = 0 then 0 " &
                     "when (AmountOwed - Payment) is null then AmountOwed " &
                     "else (AmountOwed - Payment) " &
                     "end Balance ,  PayType  " &
                     "from " &
                       "(select fs_Transactions.strairsnumber, " &
                    "sum(numpayment) as Payment " &
                    "from fs_Transactions, FS_FeeInvoice  " &
                    "where fs_Transactions.invoiceid = FS_FeeInvoice.invoiceid  " &
                    "and fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " &
                      "and fs_transactions.active = '1' " &
                    "and strPaytype = '3' group by fs_Transactions.strairsnumber " &
                    ")Transactions, " &
                     "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                     "numFeeYear, " &
                     "case " &
                     "when strPayType = '1' then 'Annual Payer' " &
                     "Else 'Quarterly Payer' " &
                     "end PayType " &
                     "from FS_FeeInvoice " &
                     "where numfeeyear = '" & cboStatYear.Text & "' " &
                     "and active = 1 " &
                     "and strPayType = '3' " &
                     " group by strairsnumber, numFeeYear, case " &
                     "when strPayType = '1' then 'Annual Payer' " &
                     "Else 'Quarterly Payer' " &
                     "end) Invoices, " &
                     "(select strairsnumber,  " &
                       "numTotalfee " &
                       "from FS_FeeAuditedData " &
                       "where numfeeyear = '" & cboStatYear.Text & "' " &
                       "and active = '1' ) Reported, " &
                     "APBfacilityInformation  " &
                     "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                     "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                     "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                     " order by strairsnumber) allData "
                Case "QUARTER THREE"
                    SQL = "select " &
                         "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                         "strFacilityName, numFeeyear, " &
                         "Payment, AmountOwed, Balance, PayType, FeeReported " &
                         "from " &
                         "(select " &
                         "Invoices.strairsnumber, strFacilityname, " &
                         "Payment, AmountOwed, numFeeYear, " &
                         "numTotalfee as FeeReported, " &
                         "case " &
                         "when (AmountOwed - Payment) = 0 then 0 " &
                         "when (AmountOwed - Payment) is null then AmountOwed " &
                         "else (AmountOwed - Payment) " &
                         "end Balance ,  PayType  " &
                         "from " &
                         "(select fs_Transactions.strairsnumber, " &
                            "sum(numpayment) as Payment " &
                            "from fs_Transactions, FS_FeeInvoice  " &
                            "where fs_Transactions.invoiceid = FS_FeeInvoice.invoiceid  " &
                            "and fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " &
                              "and fs_transactions.active = '1' " &
                            "and strPaytype = '4' group by fs_Transactions.strairsnumber " &
                            ")Transactions, " &
                         "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                         "numFeeYear, " &
                         "case " &
                         "when strPayType = '1' then 'Annual Payer' " &
                         "Else 'Quarterly Payer' " &
                         "end PayType " &
                         "from FS_FeeInvoice " &
                         "where numfeeyear = '" & cboStatYear.Text & "' " &
                         "and active = 1 " &
                         "and strPayType = '4' " &
                         " group by strairsnumber, numFeeYear, case " &
                         "when strPayType = '1' then 'Annual Payer' " &
                         "Else 'Quarterly Payer' " &
                         "end) Invoices, " &
                         "(select strairsnumber,  " &
                           "numTotalfee " &
                           "from FS_FeeAuditedData " &
                           "where numfeeyear = '" & cboStatYear.Text & "' " &
                           "and active = '1' ) Reported, " &
                         "APBfacilityInformation  " &
                         "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                         "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                         "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                         " order by strairsnumber) allData "
                Case "QUARTER FOUR"
                    SQL = "select " &
                          "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                          "strFacilityName, numFeeyear, " &
                          "Payment, AmountOwed, Balance, PayType, FeeReported " &
                          "from " &
                          "(select " &
                          "Invoices.strairsnumber, strFacilityname, " &
                          "Payment, AmountOwed, numFeeYear, " &
                          "numTotalfee as FeeReported, " &
                          "case " &
                          "when (AmountOwed - Payment) = 0 then 0 " &
                          "when (AmountOwed - Payment) is null then AmountOwed " &
                          "else (AmountOwed - Payment) " &
                          "end Balance ,  PayType  " &
                          "from " &
                         "(select fs_Transactions.strairsnumber, " &
                            "sum(numpayment) as Payment " &
                            "from fs_Transactions, FS_FeeInvoice  " &
                            "where fs_Transactions.invoiceid = FS_FeeInvoice.invoiceid  " &
                            "and fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " &
                            "and fs_transactions.active = '1' " &
                            "and strPaytype = '5' group by fs_Transactions.strairsnumber " &
                            ")Transactions, " &
                          "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                          "numFeeYear, " &
                          "case " &
                          "when strPayType = '1' then 'Annual Payer' " &
                          "Else 'Quarterly Payer' " &
                          "end PayType " &
                          "from FS_FeeInvoice " &
                          "where numfeeyear = '" & cboStatYear.Text & "' " &
                          "and active = 1 " &
                          "and strPayType = '5' " &
                          " group by strairsnumber, numFeeYear, case " &
                          "when strPayType = '1' then 'Annual Payer' " &
                          "Else 'Quarterly Payer' " &
                          "end) Invoices, " &
                          "(select strairsnumber,  " &
                            "numTotalfee " &
                            "from FS_FeeAuditedData " &
                            "where numfeeyear = '" & cboStatYear.Text & "' " &
                            "and active = '1' ) Reported, " &
                          "APBfacilityInformation  " &
                          "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                          "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                          "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                          " order by strairsnumber) allData "
                Case Else
                    SQL = "select " &
                         "substr(strAIRSNumber, 5) as strAIRSNumber, " &
                         "strFacilityName, numFeeyear, " &
                         "Payment, AmountOwed, Balance, PayType, FeeReported " &
                         "from " &
                         "(select " &
                         "Invoices.strairsnumber, strFacilityname, " &
                         "Payment, AmountOwed, numFeeYear, " &
                         "numTotalfee as FeeReported, " &
                         "case " &
                         "when (AmountOwed - Payment) = 0 then 0 " &
                         "when (AmountOwed - Payment) is null then AmountOwed " &
                         "else (AmountOwed - Payment) " &
                         "end Balance ,  PayType  " &
                         "from " &
                         "(select strairsnumber, sum(numpayment)  as Payment " &
                         "from FS_Transactions " &
                         "where numFeeyear = '" & cboStatYear.Text & "' " &
                         "and active = '1' group by strairsnumber) Transactions, " &
                         "(select strairsnumber, sum(numamount)   as AmountOwed, " &
                         "numFeeYear, " &
                         "case " &
                         "when strPayType = '1' then 'Annual Payer' " &
                         "Else 'Quarterly Payer' " &
                         "end PayType " &
                         "from FS_FeeInvoice " &
                         "where numfeeyear = '" & cboStatYear.Text & "' " &
                         "and active = 1  group by strairsnumber, numFeeYear, case " &
                         "when strPayType = '1' then 'Annual Payer' " &
                         "Else 'Quarterly Payer' " &
                         "end) Invoices, " &
                         "(select strairsnumber,  " &
                           "numTotalfee " &
                           "from FS_FeeAuditedData " &
                           "where numfeeyear = '" & cboStatYear.Text & "' " &
                           "and active = '1' ) Reported, " &
                         "APBfacilityInformation  " &
                         "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " &
                         "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " &
                         "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " &
                         " order by strairsnumber) allData "
            End Select
            If chbNonZeroBalance.Checked = True Then
                SQL = SQL & " where balance <> 0   "

            End If


            ds = New DataSet
            If SQL <> "" Then
                da = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "PaymentDue")
            End If

            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("strAIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("strAIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("PayType").HeaderText = "Payment Plan"
            dgvDepositsAndPayments.Columns("PayType").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("FeeReported").HeaderText = "Amount Reported"
            dgvDepositsAndPayments.Columns("FeeReported").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("FeeReported").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("AmountOwed").HeaderText = "Amount Invoiced"
            dgvDepositsAndPayments.Columns("AmountOwed").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("AmountOwed").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("Payment").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("Payment").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("Payment").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance Due"
            dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 7

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class