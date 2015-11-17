Imports Oracle.ManagedDataAccess.Client
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class PASPFeeStatistics
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim dsViewCount As DataSet
    Dim daViewCount As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    Dim crParameterFieldDefinition As ParameterFieldDefinition
    Dim crParameterValues As New ParameterValues
    Dim crParameterDiscreteValue As New ParameterDiscreteValue
    Dim rpt As ReportClass
    Dim progress1 As ProgressStatus
    Dim sb As StatusBar = New StatusBar
    Dim pnl2 As StatusBarPanel = New StatusBarPanel
    Dim pnl3 As StatusBarPanel = New StatusBarPanel

    Private Sub DEVFeeStatistics_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            monitor.TrackFeature("Forms." & Me.Name)

            'tspnl1.Text = "Misc Web Tools Loading..."
            'tspnl2.Text = UserName
            'tspnl3.Text = OracleDate

            'Web Tab
            pnlDetails.Dock = DockStyle.None

            If AccountFormAccess(12, 3) <> "1" Then
                TCMailoutAndStats.TabPages.Remove(TPNonRespondersReport)
            End If
            loadDepositAndPayment()

            dtpStartDepositDate.Text = Format(CDate(OracleDate).AddMonths(-1), "dd-MMM-yyyy")
            dtpEndDepositDate.Text = OracleDate
            dtpStartDepositDate.Enabled = False
            dtpEndDepositDate.Enabled = False
            chbDepositDateSearch.Checked = False
            btnRunDepositReport.Enabled = False

            AddProgressBar()
            progress1.progress = -1

            LoadComboBoxesF()

            lblFacilityBalanceReportTag.Visible = False
            mtbFacilityBalanceYear.Visible = False
            btnRunBalanceReport.Visible = False

            chbFacilityBalance.Visible = False
            chbFacilityBalance.Enabled = False
            chbFacilityBalance.Checked = False
            cboFeeStatYear.Text = cboFeeStatYear.Items.Item(0)

            If AccountFormAccess(135, 1) = "1" Or AccountFormAccess(135, 2) = "1" Or AccountFormAccess(135, 3) = "1" Or AccountFormAccess(135, 4) = "1" Then
                btnOpenFeesLog.Visible = True
                txtFeeStatAirsNumber.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            progress1.progress = 0
        End Try

    End Sub
    Sub loadDepositAndPayment()
        Try
            Dim Year As String

            Year = Now.Year
            SQL = "Select distinct(numFeeYear) as FeeYear " & _
            "from AIRBRANCH.FS_Admin " & _
            "order by numFeeYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("FeeYear")) Then
                    Year = Now.Year
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
            End While
            dr.Close()

            cboStatPayType.Items.Add("ALL")
            cboStatPayType.Items.Add("ALL QUARTERS")

            SQL = "Select strPayTypeDesc " & _
            "from AIRBRANCH.FSLK_PayType " & _
            "where Active = '1' " & _
            "order by numPaytypeID "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                cboStatPayType.Items.Add(dr.Item("strPayTypeDesc"))
            End While
            dr.Close()

            cboStatYear.Text = cboStatYear.Items.Item(0)
            cboStatPayType.Text = cboStatPayType.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
 
    Private Sub btnViewDepositsStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDepositsStats.Click
        Try
            Dim SQLReported As String = ""
            Dim SQLInvoiced As String = ""
            Dim SQLPaid As String = ""

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Admin.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSnumber " & _
                    "and AIRBRANCH.FS_Admin.numFeeYear = AIRBRANCH.FS_FeeAuditedData.numFeeYear " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and numCurrentStatus <> '12' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeInvoice.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeYEar = AIRBRANCH.fs_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "Select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions " & _
                    "where numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and Active = '1' "

                    'SQLPaidInvoiced = "select sum(numPayment) as TotalInvoicedPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "ANNUAL"
                    SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan = 'Entire Annual Year' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeInvoice.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeyear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_admin.active = '1' " & _
                    "and AIRbranch.FS_FeeInvoice.strPayType = '1'  " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '1' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "


                Case "ALL QUARTERS"
                    SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                     "and AIRBRANCH.FS_FeeInvoice.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeYEar = AIRBRANCH.fs_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and AIRbranch.FS_FeeInvoice.strPayType <> '1'  " & _
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' " & _
                    "and (AIRBRANCH.FS_FeeInvoice.strPayType = '2' " & _
                    "or AIRBRANCH.FS_FeeInvoice.strPayType = '3' " & _
                    "or AIRBRANCH.FS_FeeInvoice.strPayType = '4' " & _
                    "or AIRBRANCH.FS_FeeInvoice.strPayType = '5') "

                    '      SQLPaidInvoiced = "select sum(numPayment) as TotalInvoicedPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType <> '1' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "QUARTER ONE"
                    SQLReported = "Select sum(numtotalFee/4) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                       "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice,  AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                      "and AIRBRANCH.FS_FeeInvoice.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeYEar = AIRBRANCH.fs_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and AIRbranch.FS_FeeInvoice.strPayType = '2'  "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '2' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "

                    '      SQLPaidInvoiced = "select sum(numPayment) as TotalInvoicedPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '2' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "QUARTER TWO"
                    SQLReported = "Select sum(numtotalFee/4) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeInvoice.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeYEar = AIRBRANCH.fs_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and AIRbranch.FS_FeeInvoice.strPayType = '3'   "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '3' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "

                    '      SQLPaidInvoiced = "select sum(numPayment) as TotalInvoicedPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '3' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "QUARTER THREE"
                    SQLReported = "Select sum(numtotalFee/4) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeInvoice.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeYEar = AIRBRANCH.fs_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and AIRbranch.FS_FeeInvoice.strPayType = '4'   "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '4' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "

                    '      SQLPaidInvoiced = "select sum(numPayment) as TotalInvoicedPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '4' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "QUARTER FOUR"
                    SQLReported = "Select sum(numtotalFee/4 ) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                   "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan = 'Four Quarterly Payments' "

                    SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    "from AIRBRANCH.FS_FeeInvoice, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeInvoice.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeInvoice.numFeeYEar = AIRBRANCH.fs_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and AIRbranch.FS_FeeInvoice.strPayType = '5'  "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '5' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "

                    'SQLPaidInvoiced = "select sum(numPayment) as TotalInvoicedPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '5' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "AMENDMENT"
                    SQLReported = "Select sum(numtotalFee ) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan is null "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '6' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "
                Case "ONE-TIME"
                    SQLReported = "Select sum(numtotalFee ) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin  " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strpaymentplan is null "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '8' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "
                Case "REFUND"
                    SQLReported = "Select sum(0) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    "and AIRBRANCH.FS_FeeInvoice.strPayType = '7' " & _
                    "and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_Transactions.active = '1' "
                Case Else
                    SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    "from AIRBRANCH.FS_FeeAuditedData, AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.strAIRSNumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_FeeAuditedData.nuMFeeYear = AIRBRANCH.FS_Admin.numFeeyear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' "

                    SQLPaid = "Select sum(numPayment) as TotalPaid " & _
                    "from AIRBRANCH.FS_Transactions " & _
                    "where numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and Active = '1' "
            End Select
            If SQLReported <> "" Then
                cmd = New OracleCommand(SQLReported, CurrentConnection)
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
                cmd = New OracleCommand(SQLInvoiced, CurrentConnection)
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
                cmd = New OracleCommand(SQLPaid, CurrentConnection)
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
            '    cmd = New OracleCommand(SQLPaidInvoiced, conn)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewPaymentDue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewPaymentDue.Click
        Try
            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select  " & _
                    "substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strFacilityName, strPaymentPlan,  " & _
                    "(numTotalFee ) as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    "numPart70Fee, numSMFee, numNSPSFee,  " & _
                    "numTotalFee, strClass, numAdminFee  " & _
                    "From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData, " & _
                    "AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                    "and AIRBRANCH.FS_feeAuditedData.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_feeAuditedData.numFeeYear = AIRBRANCH.FS_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' "
                Case "ANNUAL"
                    SQL = "select  " & _
                    "substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strFacilityName, strPaymentPlan,  " & _
                    "(numTotalFee) as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    "numPart70Fee, numSMFee, numNSPSFee,  " & _
                    "numTotalFee, strClass, numAdminFee  " & _
                    "From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData, " & _
                    "AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                    "and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_feeAuditedData.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_feeAuditedData.numFeeYear = AIRBRANCH.FS_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strPaymentPlan = 'Entire Annual Year' "
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    SQL = "select  " & _
                    "substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strFacilityName, strPaymentPlan,  " & _
                    "(numTotalFee)/4 as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    "numPart70Fee, numSMFee, numNSPSFee,  " & _
                    "numTotalFee, strClass, numAdminFee  " & _
                    "From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData, " & _
                    "AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                    "and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.FS_feeAuditedData.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_feeAuditedData.numFeeYear = AIRBRANCH.FS_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strPaymentPlan = 'Four Quarterly Payments' "
                Case "AMENDMENT", "ONE-TIME", "REFUND"
                    SQL = "select  " & _
                    "substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strFacilityName, strPaymentPlan,  " & _
                    "(numTotalFee)/4 as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    "numPart70Fee, numSMFee, numNSPSFee,  " & _
                    "numTotalFee, strClass, numAdminFee  " & _
                    "From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData, " & _
                    "AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                    "and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                     "and AIRBRANCH.FS_feeAuditedData.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_feeAuditedData.numFeeYear = AIRBRANCH.FS_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and strPaymentPlan is null "
                Case Else
                    SQL = "select  " & _
                    "substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strFacilityName, strPaymentPlan,  " & _
                    "(numTotalFee) as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    "numPart70Fee, numSMFee, numNSPSFee,  " & _
                    "numTotalFee, strClass, numAdminFee  " & _
                    "From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData, " & _
                    "AIRBRANCH.FS_Admin " & _
                    "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                     "and AIRBRANCH.FS_feeAuditedData.strAIRSnumber = AIRBRANCH.FS_Admin.strAIRSNumber " & _
                    "and AIRBRANCH.FS_feeAuditedData.numFeeYear = AIRBRANCH.FS_Admin.numFeeYear " & _
                    "and AIRBRANCH.FS_FeeAuditedData.active = '1' " & _
                    "and AIRBRANCH.FS_Admin.Active = '1' " & _
                    "and numCurrentStatus <> '12' " & _
                    "and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' "
            End Select

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bntViewTotalPaid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntViewTotalPaid.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " & _
                      "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                      "strFacilityName, " & _
                      "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                      "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                      "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                      "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                      "numAdminFee, (numTotalFee) as Due " & _
                      "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                      "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                      "AIRBranch.FSLK_PayType " & _
                      "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                      "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
                      "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                      "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                      "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                      "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                      "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' "
                Case "ANNUAL"
                    SQL = "select " & _
                       "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                       "strFacilityName, " & _
                       "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                       "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                       "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                       "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                       "numAdminFee, (numTotalFee) as Due " & _
                       "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                       "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                      "AIRBranch.FSLK_PayType " & _
                       "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                       "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                       "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                        "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                      "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                      "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                       "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                       "and strPaymentPlan = 'Entire Annual Year' "
                Case "QUARTER ONE"
                    SQL = "select " & _
                "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                "strFacilityName, " & _
                "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                "numAdminFee, (numTotalFee) as Due " & _
                "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                  "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                  "AIRBranch.FSLK_PayType " & _
                "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                 "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                  "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                  "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                "and numPayTypeID = '2' "
                Case "QUARTER TWO"
                    SQL = "select " & _
                   "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                   "strFacilityName, " & _
                   "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                   "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                   "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                   "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                   "numAdminFee, (numTotalFee) as Due " & _
                   "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                    "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                    "AIRBranch.FSLK_PayType " & _
                   "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                   "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                   "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                    "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                   "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                   "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                   "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                   "and numPayTypeID = '3' "
                Case "QUARTER THREE"
                    SQL = "select " & _
           "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
           "strFacilityName, " & _
           "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
           "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
           "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
           "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
           "numAdminFee, (numTotalFee) as Due " & _
           "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
             "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
             "AIRBranch.FSLK_PayType " & _
           "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
           "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
           "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
             "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
             "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
           "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
           "and numPayTypeID = '4' "
                Case "QUARTER FOUR"
                    SQL = "select " & _
           "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
           "strFacilityName, " & _
           "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
           "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
           "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
           "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
           "numAdminFee, (numTotalFee) as Due " & _
           "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
             "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
             "AIRBranch.FSLK_PayType " & _
           "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
           "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
           "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
             "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
             "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
           "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
           "and numPayTypeID = '5' "

                Case "ALL QUARTERS"
                    SQL = "select " & _
                    "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                    "strFacilityName, " & _
                    "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                    "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                    "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                    "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                    "numAdminFee, (numTotalFee) as Due " & _
                    "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                      "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                      "AIRBranch.FSLK_PayType " & _
                    "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                    "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                    "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                     "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                      "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                      "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                    "and strPaymentPlan = 'Four Quarterly Payments' "
                Case "AMENDMENT"
                    SQL = "select " & _
                     "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                     "strFacilityName, " & _
                     "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                     "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                     "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                     "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                     "numAdminFee, (numTotalFee) as Due " & _
                     "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                       "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                       "AIRBranch.FSLK_PayType " & _
                     "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                     "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                     "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                      "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                       "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                       "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                     "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                     "and numPayTypeID = '6' "


                Case "ONE-TIME"
                    SQL = "select " & _
                   "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                   "strFacilityName, " & _
                   "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                   "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                   "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                   "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                   "numAdminFee, (numTotalFee) as Due " & _
                   "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                     "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                     "AIRBranch.FSLK_PayType " & _
                   "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                   "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                   "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                    "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                     "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                     "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                   "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                   "and numPayTypeID = '8' "

                Case "REFUND"
                    SQL = "select " & _
                   "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                   "strFacilityName, " & _
                   "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                   "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                   "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                   "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                   "numAdminFee, (numTotalFee) as Due " & _
                   "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                     "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                     "AIRBranch.FSLK_PayType " & _
                   "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                   "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                   "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                    "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                     "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                     "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                   "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' " & _
                   "and numPayTypeID = '7' "

                Case Else
                    SQL = "select " & _
                     "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                     "strFacilityName, " & _
                     "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " & _
                     "datTransactionDate, strCheckNo, AIRBranch.FS_Transactions.InvoiceID, " & _
                     "AIRBranch.FS_Transactions.numFeeYear, numPart70Fee, " & _
                     "numSMFee, numNSPSFee, numTotalFee, strClass, " & _
                     "numAdminFee, (numTotalFee) as Due " & _
                     "From AIRBranch.APBFacilityInformation, AIRBranch.FS_Transactions, " & _
                       "AIRBranch.FS_FeeAuditedData, AIRBranch.FS_FeeInvoice, " & _
                       "AIRBranch.FSLK_PayType " & _
                     "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
                     "and AIRBranch.FS_Transactions.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
                     "and airbranch.FS_Transactions.numFeeyear = Airbranch.FS_FeeAuditedData.numFeeYear (+) " & _
                      "and AIRBranch.FS_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  (+) " & _
                       "and airbranch.FS_FeeInvoice.strPayType = AIRbranch.FSLK_PayType.numPayTypeID " & _
                       "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                      "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                     "and AIRBranch.FS_Transactions.numFeeYear = '" & cboStatYear.Text & "' "
            End Select

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewBalance.Click
        Try
            
            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select * from " & _
                    "(select " & _
                    "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, " & _
                    "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                    "case " & _
                    "when TotalDue is null then 0 " & _
                    "else TotalDue " & _
                    "end totalDue, " & _
                    "case " & _
                    "when TotalPaid is null then 0 " & _
                    "else TotalPaid " & _
                    "end TotalPaid, " & _
                    "case " & _
                    "when TotalDue is null and TotalPaid is null then 0 " & _
                    " when totaldue is null and totalpaid is not null then -totalpaid " & _
                    "when totaldue is not null and totalpaid is null then totaldue " & _
                    "else (TOTALDUE - TOTALPAID) " & _
                    "end Balance " & _
                    "from (select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE) as TOTALDUE " & _
                    "from  AIRBRANCH.fs_feeAuditedData where active = '1') TOTALDUE,  " & _
                    "( " & _
                    "  SELECT  " & _
                    " STRAIRSNUMBER, INTYEAR, " & _
                    " CASE " & _
                    " WHEN TOTALPAID IS NULL THEN 0 " & _
                    " ELSE TOTALPAID " & _
                    " END TOTALPAID " & _
                    " FROM " & _
                    " ( " & _
                    " select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
                    " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData   " & _
                    " where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
                    " and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
                    " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
                    " group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
                    " UNION " & _
                    "  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
                    " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData   " & _
                    " where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
                    " and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
                    " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
                    " group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
                    "  )) TOTALPAID, " & _
                    " AIRBRANCH.APBFACILITYINFORMATION, " & _
                    "AIRBRANCH.fs_feeAuditedData   " & _
                    "where (AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                    "or AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                    "and AIRBRANCH.APBFACILITYINFORMATION.strAIRSNumber = AIRBRANCH.fs_feeAuditedData.strAIRSNumber " & _
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and AIRBRANCH.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "


                Case "ANNUAL"
                    SQL = "select * from " & _
                   "(select " & _
                   "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                   "STRFACILITYNAME, " & _
                   "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                   "case " & _
                   "when TotalDue is null then 0 " & _
                   "else TotalDue " & _
                   "end totalDue, " & _
                   "case " & _
                   "when TotalPaid is null then 0 " & _
                   "else TotalPaid " & _
                   "end TotalPaid, " & _
                   "case " & _
                   "when TotalDue is null and TotalPaid is null then 0 " & _
                   " when totaldue is null and totalpaid is not null then -totalpaid " & _
                   "when totaldue is not null and totalpaid is null then totaldue " & _
                   "else (TOTALDUE - TOTALPAID) " & _
                   "end Balance " & _
                   "from (select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE) as TOTALDUE " & _
                   "from  AIRBRANCH.fs_feeAuditedData where active = '1') TOTALDUE,  " & _
                   "( " & _
                   "  SELECT  " & _
                   " STRAIRSNUMBER, INTYEAR, " & _
                   " CASE " & _
                   " WHEN TOTALPAID IS NULL THEN 0 " & _
                   " ELSE TOTALPAID " & _
                   " END TOTALPAID " & _
                   " FROM " & _
                   " ( " & _
                   " select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
                   " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData   " & _
                   " where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
                   " and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
                   " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
                   " group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
                   " UNION " & _
                   "  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
                   " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData   " & _
                   " where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
                   " and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
                   " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
                   " group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
                   "  )) TOTALPAID, " & _
                   " AIRBRANCH.APBFACILITYINFORMATION, " & _
                   "AIRBRANCH.fs_feeAuditedData   " & _
                   "where (AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                   "or AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                   "and AIRBRANCH.APBFACILITYINFORMATION.strAIRSNumber = AIRBRANCH.fs_feeAuditedData.strAIRSNumber " & _
                   "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                   "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                   "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                   "and AIRBRANCH.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                   "and strPaymentPlan = 'Entire Annual Year' )"
                Case "ALL QUARTERS"
                    SQL = "select * from " & _
                     "(select " & _
                     "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, " & _
                     "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                     "case " & _
                     "when TotalDue is null then 0 " & _
                     "else TotalDue " & _
                     "end totalDue, " & _
                     "case " & _
                     "when TotalPaid is null then 0 " & _
                     "else TotalPaid " & _
                     "end TotalPaid, " & _
                     "case " & _
                     "when TotalDue is null and TotalPaid is null then 0 " & _
                     " when totaldue is null and totalpaid is not null then -totalpaid " & _
                     "when totaldue is not null and totalpaid is null then totaldue " & _
                     "else (TOTALDUE - TOTALPAID) " & _
                     "end Balance " & _
                   "from (select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE) as TOTALDUE " & _
                   "from  AIRBRANCH.fs_feeAuditedData where active = '1') TOTALDUE,  " & _
                     "( " & _
 "  SELECT  " & _
 " STRAIRSNUMBER, INTYEAR, " & _
 " CASE " & _
 " WHEN TOTALPAID IS NULL THEN 0 " & _
 " ELSE TOTALPAID " & _
 " END TOTALPAID " & _
 " FROM " & _
 " ( " & _
 " select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
 " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData   " & _
 " where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
 " and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
 " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
 " group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
 " UNION " & _
 "  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
 " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData   " & _
 " where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
 " and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
 " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
 " group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
 "  )) TOTALPAID, " & _
 " AIRBRANCH.APBFACILITYINFORMATION, " & _
                   "AIRBRANCH.fs_feeAuditedData   " & _
                   "where (AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                   "or AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                   "and AIRBRANCH.APBFACILITYINFORMATION.strAIRSNumber = AIRBRANCH.fs_feeAuditedData.strAIRSNumber " & _
                   "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                   "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                   "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                   "and AIRBRANCH.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                   "and strPaymentPlan = 'Four Quarterly Payments') "

                Case "QUARTER ONE"
                    SQL = "select * from " & _
                    "(select " & _
                    "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, " & _
                    "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                    "case " & _
                    "when TotalDue is null then 0 " & _
                    "else TotalDue " & _
                    "end totalDue, " & _
                    "case " & _
                    "when TotalPaid is null then 0 " & _
                    "else TotalPaid " & _
                    "end TotalPaid, " & _
                    "case " & _
                    "when TotalDue is null and TotalPaid is null then 0 " & _
                    " when totaldue is null and totalpaid is not null then -totalpaid " & _
                    "when totaldue is not null and totalpaid is null then totaldue " & _
                    "else (TOTALDUE - TOTALPAID) " & _
                    "end Balance " & _
                  "from " & _
                  "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                  "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
             "( " & _
"  SELECT  " & _
" STRAIRSNUMBER, INTYEAR, " & _
" CASE " & _
" WHEN TOTALPAID IS NULL THEN 0 " & _
" ELSE TOTALPAID " & _
" END TOTALPAID " & _
" FROM " & _
" ( " & _
" select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
" from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice " & _
" where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
" and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
"and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
"and strPayType = '2' " & _
" and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
" group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
" UNION " & _
"  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
" from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice    " & _
" where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
" and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
"and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
" and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
"and strPayType = '2' " & _
" group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
"  )) TOTALPAID, " & _
" AIRBRANCH.APBFACILITYINFORMATION, " & _
                  "  AIRBranch.fs_feeAuditedData  " & _
                  "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                  "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                  "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                  "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                  "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                  "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                  "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "

                Case "QUARTER TWO"
                    SQL = "select * from " & _
                     "(select " & _
                     "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, " & _
                     "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                     "case " & _
                     "when TotalDue is null then 0 " & _
                     "else TotalDue " & _
                     "end totalDue, " & _
                     "case " & _
                     "when TotalPaid is null then 0 " & _
                     "else TotalPaid " & _
                     "end TotalPaid, " & _
                     "case " & _
                     "when TotalDue is null and TotalPaid is null then 0 " & _
                     " when totaldue is null and totalpaid is not null then -totalpaid " & _
                     "when totaldue is not null and totalpaid is null then totaldue " & _
                     "else (TOTALDUE - TOTALPAID) " & _
                     "end Balance " & _
                        "from " & _
                        "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                        "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
                   "( " & _
      "  SELECT  " & _
      " STRAIRSNUMBER, INTYEAR, " & _
      " CASE " & _
      " WHEN TOTALPAID IS NULL THEN 0 " & _
      " ELSE TOTALPAID " & _
      " END TOTALPAID " & _
      " FROM " & _
      " ( " & _
      " select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
      " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice " & _
      " where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
      " and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
      "and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
      "and strPayType = '3' " & _
      " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
      " group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
      " UNION " & _
      "  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
      " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice    " & _
      " where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
      " and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
      "and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
      "and strPayType = '3' " & _
      " and AIRBRANCH.fs_Transactions.active = '1' and AIRBRANCH.fs_feeAuditeddata.active = '1' " & _
      " group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
      "  )) TOTALPAID, " & _
      " AIRBRANCH.APBFACILITYINFORMATION, " & _
                        "  AIRBranch.fs_feeAuditedData  " & _
                        "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                        "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                        "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                        "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                        "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                        "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                        "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "

                Case "QUARTER THREE"
                    SQL = "select * from " & _
                        "(select " & _
                        "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                        "STRFACILITYNAME, " & _
                        "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                        "case " & _
                        "when TotalDue is null then 0 " & _
                        "else TotalDue " & _
                        "end totalDue, " & _
                        "case " & _
                        "when TotalPaid is null then 0 " & _
                        "else TotalPaid " & _
                        "end TotalPaid, " & _
                        "case " & _
                        "when TotalDue is null and TotalPaid is null then 0 " & _
                        " when totaldue is null and totalpaid is not null then -totalpaid " & _
                        "when totaldue is not null and totalpaid is null then totaldue " & _
                        "else (TOTALDUE - TOTALPAID) " & _
                        "end Balance " & _
                                     "from " & _
                                     "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                                     "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
                                "( " & _
                   "  SELECT  " & _
                   " STRAIRSNUMBER, INTYEAR, " & _
                   " CASE " & _
                   " WHEN TOTALPAID IS NULL THEN 0 " & _
                   " ELSE TOTALPAID " & _
                   " END TOTALPAID " & _
                   " FROM " & _
                   " ( " & _
                   " select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
                   " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice " & _
                   " where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
                   " and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
                   "and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
                   "and strPayType = '4' " & _
                   " group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
                   " UNION " & _
                   "  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
                   " from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice    " & _
                   " where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
                   " and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
                   "and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
                   "and strPayType = '4' " & _
                   " group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
                   "  )) TOTALPAID, " & _
                   " AIRBRANCH.APBFACILITYINFORMATION, " & _
                                     "  AIRBranch.fs_feeAuditedData  " & _
                                     "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                                     "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                                     "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                                     "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "

                Case "QUARTER FOUR"
                    SQL = "select * from " & _
                   "(select " & _
                   "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                   "STRFACILITYNAME, " & _
                   "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                   "case " & _
                   "when TotalDue is null then 0 " & _
                   "else TotalDue " & _
                   "end totalDue, " & _
                   "case " & _
                   "when TotalPaid is null then 0 " & _
                   "else TotalPaid " & _
                   "end TotalPaid, " & _
                   "case " & _
                   "when TotalDue is null and TotalPaid is null then 0 " & _
                   " when totaldue is null and totalpaid is not null then -totalpaid " & _
                   "when totaldue is not null and totalpaid is null then totaldue " & _
                   "else (TOTALDUE - TOTALPAID) " & _
                   "end Balance " & _
                  "from " & _
                  "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                  "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
             "( " & _
"  SELECT  " & _
" STRAIRSNUMBER, INTYEAR, " & _
" CASE " & _
" WHEN TOTALPAID IS NULL THEN 0 " & _
" ELSE TOTALPAID " & _
" END TOTALPAID " & _
" FROM " & _
" ( " & _
" select AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
" from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice " & _
" where AIRBRANCH.fs_Transactions.STRAIRSNUMBER = AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER (+) " & _
" and AIRBRANCH.fs_Transactions.numfeeyear = AIRBRANCH.fs_feeAuditedData.numfeeyear   (+) " & _
"and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
"and strPayType = '5' " & _
" group by AIRBRANCH.fs_Transactions.STRAIRSNUMBER, AIRBRANCH.fs_Transactions.numfeeyear" & _
" UNION " & _
"  select AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear as INTYEAR, sum(NUMPAYMENT) as TotalPaid      " & _
" from AIRBRANCH.fs_Transactions, AIRBRANCH.fs_feeAuditedData, AIRBRANCH.fs_feeinvoice    " & _
" where AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER = AIRBRANCH.fs_Transactions.STRAIRSNUMBER (+) " & _
" and AIRBRANCH.fs_feeAuditedData.numfeeyear = AIRBRANCH.fs_Transactions.numfeeyear   (+) " & _
"and AIRBRANCH.Fs_transactions.invoiceid = AIRBRANCH.fs_feeinvoice.invoiceid (+) " & _
"and strPayType = '5' " & _
" group by AIRBRANCH.fs_feeAuditedData.STRAIRSNUMBER, AIRBRANCH.fs_feeAuditedData.numfeeyear" & _
"  )) TOTALPAID, " & _
" AIRBRANCH.APBFACILITYINFORMATION, " & _
                  "  AIRBranch.fs_feeAuditedData  " & _
                  "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                  "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                  "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                  "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER (+) " & _
                  "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR (+) " & _
                  "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                  "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                  " and strPaymentPlan = 'Four Quarterly Payments' ) "
                Case "AMENDMENT"
                    SQL = "select * from " & _
                    "(select " & _
                    "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, " & _
                    "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                    "case " & _
                    "when TotalDue is null then 0 " & _
                    "else TotalDue " & _
                    "end totalDue, " & _
                    "case " & _
                    "when TotalPaid is null then 0 " & _
                    "else TotalPaid " & _
                    "end TotalPaid, " & _
                    "case " & _
                    "when TotalDue is null and TotalPaid is null then 0 " & _
                    " when totaldue is null and totalpaid is not null then -totalpaid " & _
                    "when totaldue is not null and totalpaid is null then totaldue " & _
                    "else (TOTALDUE - TOTALPAID) " & _
                    "end Balance " & _
                    "from " & _
                    "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                    "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
                    "(select airbranch.Fs_transactions.STRAIRSNUMBER, airbranch.Fs_transactions.numfeeyear as INTYEAR, " & _
                    "sum(NUMPAYMENT) as TotalPaid  " & _
                    "from AIRBranch.fs_Transactions, airbranch.fs_feeinvoice " & _
                    "where airbranch.Fs_transactions.invoiceid = airbranch.fs_feeinvoice.invoiceid " & _
                    "and strPayType = '6' " & _
                    "group by airbranch.Fs_transactions.STRAIRSNUMBER, airbranch.Fs_transactions.numfeeyear, AIRBranch.fs_Transactions.invoiceid) TOTALPAID, " & _
                    "AIRBranch.APBFACILITYINFORMATION, AIRBranch.fs_feeAuditedData  " & _
                    "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                    "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                    "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "

                Case "ONE-TIME"
                    SQL = "select * from " & _
                    "(select " & _
                    "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, " & _
                    "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                    "case " & _
                    "when TotalDue is null then 0 " & _
                    "else TotalDue " & _
                    "end totalDue, " & _
                    "case " & _
                    "when TotalPaid is null then 0 " & _
                    "else TotalPaid " & _
                    "end TotalPaid, " & _
                    "case " & _
                    "when TotalDue is null and TotalPaid is null then 0 " & _
                    " when totaldue is null and totalpaid is not null then -totalpaid " & _
                    "when totaldue is not null and totalpaid is null then totaldue " & _
                    "else (TOTALDUE - TOTALPAID) " & _
                    "end Balance " & _
                    "from " & _
                    "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                    "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
                    "(select airbranch.Fs_transactions.STRAIRSNUMBER, airbranch.Fs_transactions.numfeeyear as INTYEAR, " & _
                    "sum(NUMPAYMENT) as TotalPaid  " & _
                    "from AIRBranch.fs_Transactions, airbranch.fs_feeinvoice " & _
                    "where airbranch.Fs_transactions.invoiceid = airbranch.fs_feeinvoice.invoiceid " & _
                    "and strPayType = '8' " & _
                    "group by airbranch.Fs_transactions.STRAIRSNUMBER, airbranch.Fs_transactions.numfeeyear, AIRBranch.fs_Transactions.invoiceid) TOTALPAID, " & _
                    "AIRBranch.APBFACILITYINFORMATION, AIRBranch.fs_feeAuditedData  " & _
                    "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                    "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                    "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "

                Case "REFUND"
                    SQL = "select * from " & _
                       "(select " & _
                       "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                       "STRFACILITYNAME, " & _
                       "TOTALDUE.INTYEAR, strPaymentPlan, " & _
                       "case " & _
                       "when TotalDue is null then 0 " & _
                       "else TotalDue " & _
                       "end totalDue, " & _
                       "case " & _
                       "when TotalPaid is null then 0 " & _
                       "else TotalPaid " & _
                       "end TotalPaid, " & _
                       "case " & _
                       "when TotalDue is null and TotalPaid is null then 0 " & _
                       " when totaldue is null and totalpaid is not null then -totalpaid " & _
                       "when totaldue is not null and totalpaid is null then totaldue " & _
                       "else (TOTALDUE - TOTALPAID) " & _
                       "end Balance " & _
                       "from " & _
                       "(select STRAIRSNUMBER, numfeeyear as  intyear, NUMTOTALFEE, NUMADMINFEE, (NUMTOTALFEE)/4 as TOTALDUE " & _
                       "from  AIRBranch.fs_feeAuditedData where active = '1') TOTALDUE, " & _
                       "(select airbranch.Fs_transactions.STRAIRSNUMBER, airbranch.Fs_transactions.numfeeyear as INTYEAR, " & _
                       "sum(NUMPAYMENT) as TotalPaid  " & _
                       "from AIRBranch.fs_Transactions, airbranch.fs_feeinvoice " & _
                       "where airbranch.Fs_transactions.invoiceid = airbranch.fs_feeinvoice.invoiceid " & _
                       "and strPayType = '7' " & _
                       "group by airbranch.Fs_transactions.STRAIRSNUMBER, airbranch.Fs_transactions.numfeeyear, AIRBranch.fs_Transactions.invoiceid) TOTALPAID, " & _
                       "AIRBranch.APBFACILITYINFORMATION, AIRBranch.fs_feeAuditedData  " & _
                       "where (AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                       "or AIRBranch.APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                       "and AIRBranch.APBFACILITYINFORMATION.strAIRSNumber = AIRBranch.fs_feeAuditedData.strAIRSNumber " & _
                       "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                       "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                       "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                       "and AIRBranch.fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "') "
            End Select
            If chbNonZeroBalance.Checked = True Then
                SQL = SQL & " where (TOTALDUE - TOTALPAID) <> '0'  "

            End If

            ds = New DataSet
            If SQL <> "" Then
                da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewSelectedFeeData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewSelectedFeeData.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSelectedFeeData()
        Try
            SQL = "select " & _
            "strAIRSNUmber, nuMFeeYear, " & _
            "intSubmittal, datSubmittal, " & _
            "strComment, strIAIPdesc  " & _
            "from AIRBRANCH.FS_Admin, airbranch.fslk_admin_status " & _
            "where airbranch.fs_admin.numCurrentStatus = AIRbranch.FSLK_Admin_Status.ID  " & _
            "and strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " & _
            "and numFeeYear = '" & txtSelectedYear.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "Select " & _
            "intVOCTons, intPMTons, " & _
            "intSO2Tons, intNOxTons, " & _
            "numPart70Fee, numSMFee, " & _
            "numNSPSFee, numTotalFee, " & _
            "strNSPSExempt, " & _
            "strOperate, numFeeRate, " & _
            "strNSPSExemptReason, strPart70, " & _
            "strSyntheticMinor, numCalculatedFee, " & _
            "strClass, strNSPS, " & _
            "datshutDown,  " & _
            "numAdminFee, " & _
            "(numTotalFee) as AllFees, " & _
            "strPaymentPlan " & _
            "from AIRBRANCH.FS_FeeAuditedData " & _
            "where strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " & _
            "and numFeeYear = '" & txtSelectedYear.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Select description " & _
                    "from AIRBranch.FSLK_NSPSReason " & _
                    "where nspsreasoncode = '0' " & SQL2
                Else
                    SQL = "Select description " & _
                    "from AIRBranch.FSLK_NSPSReason " & _
                    "where nspsreasoncode = '" & temp & "' "
                End If

                txtNSPSExemptReason.Clear()
                cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "select " & _
            "TransactionID, Airbranch.FS_feeInvoice.InvoiceID, " & _
            "numPayment, datTransactionDate, " & _
            "strPayTypeDesc, strDepositNo, strCreditcardno, " & _
            "strCheckNo, StrBatchNo, numAmount,  " & _
            "AIRBranch.FS_Transactions.strComment " & _
            "from AIRBranch.FS_Transactions, airbranch.FS_FeeInvoice, " & _
            "AIRBranch.FSLK_PayType " & _
            "where  Airbranch.FS_feeInvoice.strAIrsnumber = airbranch.FS_Transactions.strAIRSNumber (+) " & _
            "and   Airbranch.FS_feeInvoice.nuMFeeyear = airbranch.FS_Transactions.nuMFeeYear (+) " & _
            "and   Airbranch.FS_feeInvoice.InvoiceID = airbranch.FS_Transactions.InvoiceID (+) " & _
            "and airbranch.fs_feeinvoice.strPaytype = AIRbranch.FSLK_PayType.numPayTypeID " & _
            "and airbranch.FS_Transactions.active = '1' " & _
            "and airbranch.FS_FeeInvoice.active = '1' " & _
            "and airbranch.FS_feeInvoice.numfeeyear = '" & txtSelectedYear.Text & "'  " & _
            "and airbranch.FS_feeInvoice.strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvDepositsAndPayments_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDepositsAndPayments.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvDepositsAndPayments.HitTest(e.X, e.Y)
        Try
            If dgvDepositsAndPayments.RowCount > 0 And hti.RowIndex <> -1 Then
                txtSelectedAIRSNumber.Text = dgvDepositsAndPayments(0, hti.RowIndex).Value
                txtSelectedFacilityName.Text = dgvDepositsAndPayments(1, hti.RowIndex).Value
                txtSelectedYear.Text = cboStatYear.Text
                pnlDetails.Dock = DockStyle.None
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnHideResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHideResults.Click
        Try
            If pnlDetails.Dock = DockStyle.None Then
                pnlDetails.Dock = DockStyle.Top
            Else
                pnlDetails.Dock = DockStyle.None
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        dgvDepositsAndPayments.ExportToExcel(Me)
    End Sub
   

#Region "Late Fee Payer Report"


#End Region

    Private Sub btnRunLateFeeReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunLateFeeReport.Click
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


            SQL = "select " & _
            "substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, strCountyName, " & _
            "strClass, " & _
            "case " & _
            "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " & _
            "else strOperationalStatus " & _
            "End strOperationalStatus, " & _
            "strSICCode, " & _
            "case " & _
            "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end NSPSStatus, " & _
            "case " & _
            "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end TVStatus, " & _
            "'" & cboFeeYear.Text & "' as FeeYear " & _
            "from AIRBRANCH.FSPayAndSubmit, AIRBRANCH.APBFacilityInformation, " & _
            "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.APBHeaderData " & _
            "where AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
            "and substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber,5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
            "and intYear = '" & cboFeeYear.Text & "' " & _
            "and intSubmittal = '0' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "select " & _
                "max(to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as LastApp " & _
                "from AirBranch.SSPPApplicationMaster " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                "and datFinalizedDate is not null"

                cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "select " & _
                    "strApplicationTypeDesc, strPermitNumber, " & _
                    "case " & _
                    "when datPermitIssued is null then to_char(datFinalizedDate, 'dd-Mon-yyyy') " & _
                    "else to_char(datPermitIssued, 'dd-Mon-yyyy') " & _
                    "end FinalDate " & _
                    "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.LookUpApplicationTypes, " & _
                    "AIRBRANCH.SSPPApplicationTracking, AIRBRANCH.SSPPApplicationData  " & _
                    "where AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & LastApp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                                    PermitNumber = Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" & _
                                                     Mid(PermitNumber, 8, 4) & "-" & Mid(PermitNumber, 12, 1) & "-" & _
                                                      Mid(PermitNumber, 13, 2) & "-" & Mid(PermitNumber, 15)
                                End If
                            End If
                        End If
                    End While
                    dr2.Close()
                End If

                SQL = "select " & _
                "strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where datfinalizedDate Is null " & _
                "and strAIRSNumber = '0413" & AIRSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "select " & _
                "max(datReceiveddate) as MaxDate " & _
                "from AIRBranch.SSCPItemMaster " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "select " & _
                "max(datFCECompleted) as MaxDate " & _
                "from AIRBRANCH.SSCPFCEMaster, AIRBRANCH.SSCPFCE  " & _
                "where AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber " & _
                "and strAIRSnumber = '0413" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "select " & _
                "max(datEnforcementFinalized) as MaxDate " & _
                "from AIRBranch.SSCP_AuditedEnforcement " & _
                "where strAIRSnumber = '0413" & AIRSNumber & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
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
                        SQL = "select strTrackingNumber, datCompleteDate, " & _
                        "strActivityName " & _
                        "from AIRBRANCH.SSCPItemMaster, AIRBRANCH.LookupComplianceActivities  " & _
                        "where AIRBRANCH.SSCPItemMaster.strEventType = AIRBRANCH.LookUpComplianceActivities.strActivityType  " & _
                        "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and datCompleteDate = (select max(datCompleteDate) from AIRBRANCH.SSCPItemMaster " & _
                        "where strAIRSNumber = '0413" & AIRSNumber & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        SQL = "select " & _
                        "AIRBRANCH.SSCPFCE.strFCENumber, datFCECompleted " & _
                        "from AIRBRANCH.SSCPFCE, AIRBRANCH.SSCPFCEMaster  " & _
                        "where AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber " & _
                        "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and AIRBRANCH.SSCPFCE.datFCECompleted = (select " & _
                        "max(datFCECompleted) " & _
                        "from AIRBRANCH.SSCPFCEMaster, AIRBRANCH.SSCPFCE  " & _
                        "where AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber " & _
                        "and strAIRSnumber = '0413" & AIRSNumber & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        SQL = "select " & _
                        "strEnforcementNumber, datEnforcementFinalized " & _
                        "from AIRBRANCH.SSCP_AuditedEnforcement " & _
                        "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and datEnforcementFinalized = (Select " & _
                        "max(datEnforcementFinalized) " & _
                        "from AIRBRANCH.SSCP_AuditedEnforcement " & _
                        "where strairsnumber = '0413" & AIRSNumber & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
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

                dgvLateFeePayerReport.Rows.Add(AIRSNumber, FacilityName, County, Classification, _
                                               OperationalStatus, SIC, NSPS, TV, FeeYear, LastCompliance, ComplianceDate, _
                                               LastApp, AppDate, PermitNumber, PendingApp)
            End While
            dr.Close()

            txtFeeCount.Text = dgvLateFeePayerReport.RowCount.ToString
            dgvLateFeePayerReport.Columns("PermittingDate").DefaultCellStyle.Format = "dd-MMM-yyyy"


        Catch ex As Exception



            ' ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRunReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunReport.Click
        Try
            SQL = "select " & _
            "substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, strCountyName, " & _
            "strClass, " & _
            "case " & _
            "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " & _
            "else strOperationalStatus " & _
            "End strOperationalStatus, " & _
            "strSICCode, " & _
            "case " & _
            "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end NSPSStatus, " & _
            "case " & _
            "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end TVStatus, " & _
            "'" & cboFeeYear.Text & "' as FeeYear " & _
            "from AIRBRANCH.FSPayAndSubmit, AIRBRANCH.APBFacilityInformation, " & _
            "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.APBHeaderData " & _
            "where AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
            "and substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber,5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
            "and intYear = '" & cboFeeYear.Text & "' " & _
            "and intSubmittal = '0' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCheckforFeesPaid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckforFeesPaid.Click
        Try
            If rdbHasPaidFee.Checked = True Then
                SQL = "select  " & _
                "substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber,   " & _
                "strFacilityName, strCountyName,   " & _
                "strClass,  " & _
                "case   " & _
                "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate   " & _
                "else strOperationalStatus   " & _
                "End strOperationalStatus,  " & _
                "strSICCode,  " & _
                "case   " & _
                "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes'   " & _
                "else 'No'   " & _
                "end NSPSStatus, " & _
                "case " & _
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
                "else 'No' " & _
                "end TVStatus, " & _
                "sum(numPayment) TotalPaid, " & _
                 "'" & cboFeeYear.Text & "' as FeeYear " & _
                "from AIRBRANCH.FSPayAndSubmit, AIRBRANCH.APBFacilityInformation,   " & _
                "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.APBHeaderData,  " & _
                "AIRBRANCH.FSAddPaid  " & _
                "where AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber   " & _
                "and AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber   " & _
                "and AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.FSAddPaid.strAIRSnumber   " & _
                "and AIRBRANCH.FSPayAndSubmit.intYear = AIRBRANCH.FSAddPaid.intYear  " & _
                "and substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber,5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode   " & _
                "and AIRBRANCH.FSPayAndSubmit.intYear = '" & cboFeeYear.Text & "'   " & _
                "and intSubmittal = '0'   " & _
                "group by AIRBRANCH.FSPayAndSubmit.strAIRSNumber, strFacilityName, strCountyName,   " & _
                "strClass, strOperationalStatus, datShutDownDate, strSICCode, strAirProgramCodes  " & _
                "order by AIRSNumber "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, CurrentConnection)
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
                SQL = "select " & _
                "substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " & _
                "strFacilityName, strCountyName, " & _
                "strClass, " & _
                "case " & _
                "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " & _
                "else strOperationalStatus " & _
                "End strOperationalStatus, " & _
                "strSICCode, " & _
                "case " & _
                "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " & _
                "else 'No' " & _
                "end NSPSStatus, " & _
                "case " & _
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
                "else 'No' " & _
                "end TVStatus, " & _
                 "'" & cboFeeYear.Text & "' as FeeYear " & _
                "from AIRBRANCH.FSPayAndSubmit, AIRBRANCH.APBFacilityInformation, " & _
                "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.APBHeaderData " & _
                "where AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.FSPayAndSubmit.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
                "and substr(AIRBRANCH.FSPayAndSubmit.strAIRSNumber,5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
                "and intYear = '" & cboFeeYear.Text & "' " & _
                "and intSubmittal = '0' " & _
                "and not exists (select * from AIRBRANCH.FSAddPaid " & _
                "where AIRBRANCH.FSPayAndSubmit.strAIRSnumber = AIRBRANCH.FSAddPaid.strAIRSnumber " & _
                "and AIRBRANCH.FSPayAndSubmit.intYear = AIRBRANCH.FSAddPaid.intYear) " & _
                "order by AIRSNumber "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemovePaidFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePaidFacilities.Click
        Try
            Dim AIRSNumber As String = ""
            Dim i As Integer
            temp = dgvLateFeeReport.ColumnCount.ToString

            If dgvLateFeeReport.ColumnCount.ToString = 10 Then
                If dgvLateFeeReport.Columns(8).HeaderText = "Total Paid" Then
                    For i = 0 To dgvLateFeeReport.RowCount.ToString - 1
                        AIRSNumber = dgvLateFeeReport(0, i).Value

                        SQL = "update AIRBRANCH.FSPayAndSubmit set " & _
                        "intSubmittal = '1' " & _
                        "where strAIRSnumber = '0413" & AIRSNumber & "' " & _
                        "and intYear = '" & cboFeeYear.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Connecting Then
                            CurrentConnection.Close()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        temp = "0"

                        SQL = "Select " & _
                        "count(*) as FSCalc " & _
                        "from AIRBRANCH.FSCalculations " & _
                        "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and intYear = '" & cboFeeYear.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                            SQL = "Insert into AIRBRANCH.FSCalculations " & _
                            "values " & _
                            "('0413" & AIRSNumber & "', '" & cboFeeYear.Text & "', " & _
                            "'0', '0', '0', '0', " & _
                            "'0', '0', '0', '0', " & _
                            "'NO', '0', 'YES', '33.0', " & _
                            "'', 'No', 'No', '0', " & _
                            "'', '', '', '', '', '0') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewUnenrolled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewUnenrolled.Click
        Try
            SQL = "select " & _
            "substr(AIRBRANCH.FEEMailOut.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName " & _
            "from AIRBRANCH.FeeMailout " & _
            "where intYear = '" & cboFeeYear.Text & "' " & _
            "and not exists (select * from AIRBRANCH.FSPayAndSubmit " & _
            "where AIRBRANCH.FeeMailOut.strAIRSnumber = AIRBRANCH.FSPayAndSubmit.strAIRSnumber " & _
            "and AIRBRANCH.FeeMailOut.intYear = AIRBRANCH.FSPayAndSubmit.intYear) "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvLateFeeReport_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvLateFeeReport.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFeeData()
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

            SQL = "select " & _
            "max(datReceiveddate) as MaxDate " & _
            "from AIRBranch.SSCPItemMaster " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "select " & _
            "max(datFCECompleted) as MaxDate " & _
            "from AIRBRANCH.SSCPFCEMaster, AIRBRANCH.SSCPFCE  " & _
            "where AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber " & _
            "and strAIRSnumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "select " & _
            "max(datEnforcementFinalized) as MaxDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAIRSnumber = '0413" & AIRSNumber & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "select strTrackingNumber, datCompleteDate, " & _
                    "strActivityName " & _
                    "from AIRBRANCH.SSCPItemMaster, AIRBRANCH.LookupComplianceActivities  " & _
                    "where AIRBRANCH.SSCPItemMaster.strEventType = AIRBRANCH.LookUpComplianceActivities.strActivityType  " & _
                    "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and datCompleteDate = (select max(datCompleteDate) from AIRBRANCH.SSCPItemMaster " & _
                    "where strAIRSNumber = '0413" & AIRSNumber & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "select " & _
                    "AIRBRANCH.SSCPFCE.strFCENumber, datFCECompleted " & _
                    "from AIRBRANCH.SSCPFCE, AIRBRANCH.SSCPFCEMaster  " & _
                    "where AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber " & _
                    "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and AIRBRANCH.SSCPFCE.datFCECompleted = (select " & _
                    "max(datFCECompleted) " & _
                    "from AIRBRANCH.SSCPFCEMaster, AIRBRANCH.SSCPFCE  " & _
                    "where AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber " & _
                    "and strAIRSnumber = '0413" & AIRSNumber & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "select " & _
                    "strEnforcementNumber, datEnforcementFinalized " & _
                    "from AIRBRANCH.SSCP_AuditedEnforcement " & _
                    "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and datEnforcementFinalized = (Select " & _
                    "max(datEnforcementFinalized) " & _
                    "from AIRBRANCH.SSCP_AuditedEnforcement " & _
                    "where strairsnumber = '0413" & AIRSNumber & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "select " & _
            "max(to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as LastApp " & _
            "from AirBranch.SSPPApplicationMaster " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
            "and datFinalizedDate is not null"

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                SQL = "select " & _
                "strApplicationTypeDesc, strPermitNumber, " & _
                "case " & _
                "when datPermitIssued is null then to_char(datFinalizedDate, 'dd-Mon-yyyy') " & _
                "else to_char(datPermitIssued, 'dd-Mon-yyyy') " & _
                "end FinalDate " & _
                "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.LookUpApplicationTypes, " & _
                "AIRBRANCH.SSPPApplicationTracking, AIRBRANCH.SSPPApplicationData  " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & LastApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
                                PermitNumber = Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" & _
                                                 Mid(PermitNumber, 8, 4) & "-" & Mid(PermitNumber, 12, 1) & "-" & _
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

            SQL = "select " & _
            "strApplicationNumber " & _
            "from AIRBRANCH.SSPPApplicationMaster " & _
            "where datfinalizedDate Is null " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            SQL = "select " & _
            "strApplicationNumber, strApplicationTypeDesc " & _
            "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.LookUpApplicationTypes " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+) " & _
            "and datfinalizedDate Is null " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFeeFacilitySummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeFacilitySummary.Click
        OpenFormFacilitySummary(txtFeeAIRSNumber.Text)
    End Sub
    Private Sub btnFeeViewComplianceEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeViewComplianceEvent.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFeeViewPermittingEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeViewPermittingEvent.Click
        OpenFormPermitApplication(txtFeePermittingEvent.Text)
    End Sub
    Private Sub btnFeePendingPermittingEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeePendingPermittingEvent.Click
        OpenFormPermitApplication(txtFeePendingPermit.Text)
    End Sub
    Private Sub btnViewData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewData.Click
        Try
            If txtFeeAIRSNumber.Text <> "" Then
                LoadFeeData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportFeeReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFeeReport.Click
        dgvLateFeeReport.ExportToExcel(Me)
    End Sub
    Private Sub chbDepositDateSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbDepositDateSearch.CheckedChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunDepositReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunDepositReport.Click
        Try
            Dim StartDate As String
            Dim EndDate As String

            StartDate = dtpStartDepositDate.Text
            EndDate = dtpEndDepositDate.Text

            'SQL = "select " & _
            '"substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            '"strFacilityName, " & _
            '"strPayType, numPayment, " & _
            '"strDepositNo, datPayDate, " & _
            '"strCheckNo, strInvoiceNo, " & _
            '"AIRBRANCH.FSAddPaid.intYear " & _
            '"From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FSAddPaid " & _
            '"where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FSAddPaid.strAIRSNumber " & _
            '"and datPaydate between '" & StartDate & "' and '" & EndDate & "' "

            SQL = "select " & _
            "substr(airbranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNUmber, " & _
            "strFacilityName, " & _
            "case " & _
            "when transactionTypeCode = '1' then 'Deposit' " & _
            "when transactionTypeCode = '2' then 'Refund' " & _
            "else 'N/A' " & _
            "end transactionTypeCode, " & _
            "sum(numPayment) as PaidAmount, strDepositNo, " & _
            "strBatchNo, datTransactionDate, " & _
            "strCheckNo, " & _
            "InvoiceID, numFeeYear " & _
            "from AIRBranch.FS_Transactions, airbranch.apbfacilityinformation " & _
            "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
            "and datTransactionDate between '" & StartDate & "' and '" & EndDate & "' " & _
            "and FS_Transactions.active = '1' " & _
            "group by airbranch.APBFacilityInformation.strAIRSNumber, strFacilityName, " & _
            "transactionTypeCode, strDepositNo, strBatchNo, datTransactionDate, " & _
            "strCheckNo, InvoiceID, numFeeYear "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            dgvDepositsAndPayments.Columns("transactionTypeCode").HeaderText = "Pay Type"
            dgvDepositsAndPayments.Columns("transactionTypeCode").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("PaidAmount").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("PaidAmount").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("PaidAmount").DefaultCellStyle.Format = "c"
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
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4
            '
            dgvDepositsAndPayments.Columns("strBatchNo").HeaderText = "Batch #"
            dgvDepositsAndPayments.Columns("strBatchNo").DisplayIndex = 9
            dgvDepositsAndPayments.Columns("strBatchNo").Visible = False

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Fee Reports "
    Sub AddProgressBar()
        Try

            sb.Parent = Me
            sb.ShowPanels = True

            progress1 = New ProgressStatus(sb)

            With progress1
                .Style = StatusBarPanelStyle.OwnerDraw
                .AutoSize = StatusBarPanelAutoSize.Spring
                .MinWidth = 300
                .BorderStyle = StatusBarPanelBorderStyle.Sunken
                .Alignment = HorizontalAlignment.Right
            End With

            sb.Panels.Add(progress1)
            progress1.progress = 0

            'Dim pnl2 As StatusBarPanel = New StatusBarPanel
            pnl2 = New StatusBarPanel
            pnl2.AutoSize = StatusBarPanelAutoSize.Contents
            pnl2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            pnl2.Alignment = HorizontalAlignment.Center
            pnl2.Text = UserName
            sb.Panels.Add(pnl2)

            'Dim pnl3 As StatusBarPanel = New StatusBarPanel
            pnl3 = New StatusBarPanel
            pnl3.AutoSize = StatusBarPanelAutoSize.Contents
            pnl3.BorderStyle = StatusBarPanelBorderStyle.Sunken
            pnl3.Alignment = HorizontalAlignment.Center
            pnl3.Text = OracleDate
            sb.Panels.Add(pnl3)


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Sub LoadComboBoxesF()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try
            SQL = "Select DISTINCT substr(AIRBRANCH.FSCalculations.strairsnumber, 5) as strairsnumber, " _
            + "strfacilityname " _
            + "from AIRBRANCH.FSCalculations, AIRBRANCH.APBFacilityInformation " _
            + "where AIRBRANCH.FSCalculations.strairsnumber = AIRBRANCH.APBFacilityInformation.strairsnumber " _
            + "Order by strfacilityname "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "facilityInfo")



            dtAIRS.Columns.Add("strairsnumber", GetType(System.String))
            dtAIRS.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtAIRS.Rows.Add(drNewRow)
            Next

            With cboAirsNo
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub LoadComboBoxesD()

        Try

            ' SQL = "Select distinct strdepositno from AIRBRANCH.FSAddPaid " _
            '+ "order by strdepositno"

            SQL = "Select distinct strdepositno from AIRBRANCH.FS_Transactions " _
          + "order by strdepositno"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader
            dr.Read()
            cboDepositNo.Items.Add("")
            Do
                cboDepositNo.Items.Add(dr("strdepositno"))
            Loop While dr.Read
            If dr.IsClosed = False Then dr.Close()

        Catch ex As Exception
            ErrorReport(ex, "PASPFeeReports.LoadComboBoxesD(Sub1)")
        Finally


        End Try

        Try

            SQL = "Select distinct substr(strairsnumber,5) as strairsnumber " _
            + "from AIRBRANCH.FSAddPaid order by strairsnumber"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader
            dr.Read()
            cboAirs.Items.Add("")
            Do
                cboAirs.Items.Add(dr("strairsnumber"))
            Loop While dr.Read
            If dr.IsClosed = False Then dr.Close()
        Catch ex As Exception
            ErrorReport(ex, "PASPFeeReports.LoadComboBoxesD(Sub2)")
        Finally


        End Try
    End Sub
    Private Sub tabReport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabReport.SelectedIndexChanged
        Try

            Select Case tabReport.SelectedTab.Name

                Case "TPfacSpecific"
                Case "TPYear"
                Case "TPFinancial"
                Case "TPCompliance"
                Case "TPDeposits"
                    If cboDepositNo.Items.Count = 0 Then
                        LoadComboBoxesD()
                    End If

                Case "TPGeneral"

            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Facility Specific"

    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked

        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityFee10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from AIRBRANCH.VW_Facility_Fee " & _
            "where strAIRSNumber = '0413" & cboAirsNo.SelectedValue & "' "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Fee")
            rpt.SetDataSource(ds)

            crParameterDiscreteValue.Value = "0413" & cboAirsNo.Text
            crParameterFieldDefinitions = rpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("AirsNo")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            SetUpCrystalReportViewer(rpt, CRFeesReports, cboAirsNo.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
        progress1.progress = 0

    End Sub

#End Region

#Region "Year Specific"
    Private Sub btnFeesandEmissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeesandEmissions.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New TotalFee10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.VW_Total_fee "

            SQL = "SELECT  intYear, sum(intVOCTons) as intvoctons, " & _
            "sum(intPMTons) as intPMTons, " & _
            "sum(intSO2Tons) as intSO2Tons, " & _
            "sum(intNOXtons) as intNOXTons, " & _
            "sum(numSMFee) as numSMFee, " & _
            "sum(numNSPSFee) as numNSPSFee, " & _
            "sum(numTotalFee) as numTotalFee, " & _
            "round(avg(numFeeRate)) as numFeeRate, " & _
            "Round(avg(titlevminfee)) as titlevminfee, " & _
            "round(avg(titlevfee)) as titlevfee  " & _
            "from AIRBRANCH.vw_total_fee " & _
            "group by intyear "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Total_Fee")

            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Annual Emission and Fee")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
        progress1.progress = 0

    End Sub
    Private Sub btnClassification_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassification.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityClassification10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.FSCalculations "
            SQL = "Select * from AIRBRANCH.VW_Facility_Class_Counts "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Class_Counts")

            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Classification Totals")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0

    End Sub
    Private Sub btnRunBalanceReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunBalanceReport.Click
        Try
            If mtbFacilityBalanceYear.Text <> "" Then
                If mtbFacilityBalanceYear.TextLength = 4 Then
                    mtbFacilityBalanceYear.Text = mtbFacilityBalanceYear.Text
                Else
                    mtbFacilityBalanceYear.Text = Date.Today.Year
                End If
            Else
                mtbFacilityBalanceYear.Text = Date.Today.Year
            End If

            FeeBalanceReport()

            lblFacilityBalanceReportTag.Visible = False
            mtbFacilityBalanceYear.Visible = False
            btnRunBalanceReport.Visible = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub FeeBalanceReport()
        Try
            progress1.progress = -1
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

            'SQL = "SELECT " & _
            '"strFacilityName, " & _
            '"AIRBranch.FeeDetails.strAIRSNumber, " & _
            '"AIRBranch.FeeDetails.intyear, " & _
            '"totalDue, totalPaid, " & _
            '"strContactFirstName, strContactLastName, " & _
            '"strContactPhoneNumber1, strContactFaxNumber, " & _
            '"strContactEmail, strContactAddress1, " & _
            '"strContactCity, strContactState, " & _
            '"strContactZipCode, strSICCode, " & _
            '"strPaymentType, PaidYear   " & _
            '"FROM AIRBranch.APBFacilityInformation, " & _
            '"AIRBranch.FeeDetails, AIRBranch.FeesContact, " & _
            '"AIRBranch.APBHeaderData, AIRBranch.FSPayAndSubmit  " & _
            '"WHERE AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FeeDetails.strAIRSNumber " & _
            '"AND AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FeesContact.strAIRSnumber " & _
            '"AND AIRBranch.APBFacilityInformation.strAIRSnumber = AIRBranch.APBHeaderData.strAIRSNumber " & _
            '"AND AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FSPayAndSubmit.strAIRSNumber " & _
            '"and airbranch.feedetails.intyear = AIRBranch.fsPayAndSubmit.intYear " & _
            '"and airbranch.feedetails.intyear = '" & mtbFacilityBalanceYear.Text & "' " & _
            '"order by strairsnumber "

            SQL = "SELECT " & _
        "strFacilityName, " & _
        "AIRBranch.FeeDetails.strAIRSNumber, " & _
        "AIRBranch.FeeDetails.intyear, " & _
        "totalDue, totalPaid, " & _
        "strContactFirstName, strContactLastName, " & _
        "strContactPhoneNumber1, strContactFaxNumber, " & _
        "strContactEmail, strContactAddress1, " & _
        "strContactCity, strContactState, " & _
        "strContactZipCode, strSICCode, " & _
        "numPayment, PaidYear   " & _
        "FROM AIRBranch.APBFacilityInformation, " & _
        "AIRBranch.FeeDetails, AIRBranch.FeesContact, " & _
        "AIRBranch.APBHeaderData, AIRBranch.FS_Transactions  " & _
        "WHERE AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FeeDetails.strAIRSNumber " & _
        "AND AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FeesContact.strAIRSnumber " & _
        "AND AIRBranch.APBFacilityInformation.strAIRSnumber = AIRBranch.APBHeaderData.strAIRSNumber " & _
        "AND AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
        "and airbranch.feedetails.intyear = AIRBranch.FS_Transactions.numFeeYear " & _
        "and airbranch.feedetails.intyear = '" & mtbFacilityBalanceYear.Text & "' " & _
        "order by strairsnumber "

            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            spValue.Value = mtbFacilityBalanceYear.Text
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Private Sub btnFeeBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeBalance.Click
        Try
            lblFacilityBalanceReportTag.Visible = True
            mtbFacilityBalanceYear.Visible = True
            btnRunBalanceReport.Visible = True

            chbFacilityBalance.Visible = False
            chbFacilityBalance.Checked = False
            chbFacilityBalance.Enabled = False
            mtbFacilityBalanceYear.Focus()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub btnFeeBalanceZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeBalanceZero.Click

        Try
            lblFacilityBalanceReportTag.Visible = True
            mtbFacilityBalanceYear.Visible = True
            btnRunBalanceReport.Visible = True

            chbFacilityBalance.Visible = False
            chbFacilityBalance.Checked = True
            chbFacilityBalance.Enabled = False
            mtbFacilityBalanceYear.Focus()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Financial"
    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayment.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New TotalPayment10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from AIRBRANCH.VW_Total_PAYMENT "
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Total_PAYMENT")

            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Overall Fee Balance")
            CRFeesReports.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnPayDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayDate.Click
        Try

            DateTimePicker1.Text = OracleDate
            DateTimePicker2.Text = OracleDate
            DateTimePicker1.Visible = True
            DateTimePicker2.Visible = True
            pnlDateRange.Visible = True
            Label3.Visible = True
            Label4.Visible = True
            Label5.Visible = True

            rdb2005Variance.Visible = False
            rdb2006Variance.Visible = False
            btnRunVarianceReport.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnDateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateReport.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            Dim p As New ParameterFields
            Dim p1 As New ParameterField
            Dim p2 As New ParameterField
            Dim p3 As New ParameterDiscreteValue
            Dim p4 As New ParameterDiscreteValue

            rpt = New DepositDataQA10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.FSAddPaid " & _
            "where datPayDate between '" & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & "' and '" & Format(DateTimePicker2.Value, "dd-MMM-yyyy") & "' " & _
            "order by strBatchNo "

            SQL = "select " & _
            "case " & _
            "when TransactionTypeCode = '1' then 'Deposit' " & _
            "when TransactionTypeCode = '0' then 'Refund' " & _
            "else 'N/A' " & _
            "end TransactionTypeCode, " & _
            "datTransactionDate, numPayment, " & _
            "strCheckNo, strDepositNo, " & _
            "strBatchNo, " & _
            "strAIRSNumber, " & _
            "nuMFeeYear, strCreditCardNo " & _
            "from AIRBRANCH.FS_Transactions " & _
            "where datTransactionDate between '" & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & "' " & _
                  "and '" & Format(DateTimePicker2.Value, "dd-MMM-yyyy") & "' " & _
            "order by strBatchNo "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSAddPaid")

            rpt.SetDataSource(ds)

            p1.ParameterFieldName = "StartDate"
            p3.Value = DateTimePicker1.Value
            p1.CurrentValues.Add(p3)
            p.Add(p1)
            CRFeesReports.ParameterFieldInfo = p

            p2.ParameterFieldName = "EndDate"
            p4.Value = DateTimePicker2.Value
            p2.CurrentValues.Add(p4)
            p.Add(p2)
            CRFeesReports.ParameterFieldInfo = p

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Payments by Date - " & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & " --> " & Format(DateTimePicker2.Value, "dd-MMM-yyyy"))
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0

    End Sub

    Private Sub btndeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeposit.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New DepositData10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.FSAddPaid " & _
            "where datPayDate between '" & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & "' and '" & Format(DateTimePicker2.Value, "dd-MMM-yyyy") & "' "

            SQL = "select " & _
            "case " & _
            "when TransactionTypeCode = '1' then 'Deposit' " & _
            "when TransactionTypeCode = '0' then 'Refund' " & _
            "else 'N/A' " & _
            "end TransactionTypeCode, " & _
            "datTransactionDate, numPayment, " & _
            "strCheckNo, strDepositNo, " & _
            "strBatchNo, " & _
            "strAIRSNumber, " & _
            "nuMFeeYear, strCreditCardNo " & _
            "from AIRBRANCH.FS_Transactions " & _
            "where datTransactionDate between '" & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & "' " & _
                "and '" & Format(DateTimePicker2.Value, "dd-MMM-yyyy") & "' " & _
            "order by strDepositNo "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSAddPaid")
            rpt.SetDataSource(ds)

            Dim p As New ParameterFields
            Dim p1 As New ParameterField
            Dim p2 As New ParameterField
            Dim p3 As New ParameterDiscreteValue
            Dim p4 As New ParameterDiscreteValue

            p1.ParameterFieldName = "StartDate"
            p3.Value = DateTimePicker1.Value
            p1.CurrentValues.Add(p3)
            p.Add(p1)
            CRFeesReports.ParameterFieldInfo = p

            p2.ParameterFieldName = "EndDate"
            p4.Value = DateTimePicker2.Value
            p2.CurrentValues.Add(p4)
            p.Add(p2)
            CRFeesReports.ParameterFieldInfo = p

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Payments by Date - " & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & " --> " & Format(DateTimePicker2.Value, "dd-MMM-yyyy"))

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnBankrupt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankrupt.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New Bankrupt10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "select * from AIRBRANCH.VW_Bankrupt"
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Bankrupt")
            rpt.SetDataSource(ds)

            CRFeesReports.ReportSource = rpt
            CRFeesReports.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnFeeByYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeByYear.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New feeByYear10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from AIRBRANCH.FeesDue "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FeesDue")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Total Fee by Year")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Sub VarianceReport()
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            ds = New DataSet
            rpt = New Variance10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            If rdb2005Variance.Checked = True Then
                SQL = "Select * from AIRBRANCH.FeeVariance " & _
                "where difference2005 <> '0' and vCheck2005 <> 'YES' "

            Else
                SQL = "Select * from AIRBRANCH.FeeVariance " & _
                "where difference2006 <> '0' and vCheck2006 <> 'YES' "

            End If

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FeeVariance")
            rpt.SetDataSource(ds)



            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Year"
            If rdb2005Variance.Checked = True Then
                spValue.Value = "2005"
            Else
                spValue.Value = "2006"
            End If
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRFeesReports.ParameterFieldInfo = ParameterFields

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Variance Report")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        progress1.progress = 0
    End Sub

    Private Sub btnRunVarianceReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunVarianceReport.Click
        Try
            VarianceReport()
            pnlDateRange.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnvariance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvariance.Click
        Try
            rdb2005Variance.Visible = True
            rdb2006Variance.Visible = True
            btnRunVarianceReport.Visible = True
            pnlDateRange.Visible = True

            DateTimePicker1.Visible = False
            DateTimePicker2.Visible = False
            btndeposit.Visible = False
            btnDateReport.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region

#Region "Deposits"

    Private Sub lblDepositData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblDepositData.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet
            Dim depositno, airsno, header As String

            SQL = ""
            If cboDepositNo.Text <> "" Then
                'SQL = "Select * from AIRBRANCH.FSAddPaid " & _
                '"where strDepositNo like '%" & cboDepositNo.Text & "%' " & _
                '"order by intyear desc  "

                SQL = "Select * from AIRBRANCH.FS_Transactions " & _
                "where strDepositNo like '%" & cboDepositNo.Text & "%' " & _
                "and Active = '1' " & _
                "order by nuMFeeYear desc  "
            Else
                If cboAirs.Text <> "" Then
                    'SQL = "Select * from AIRBRANCH.FSAddPaid " & _
                    '"where strAIRSNumber like '0413%" & cboAirs.Text & "%' " & _
                    '"order by intyear desc  "

                    SQL = "Select * from AIRBRANCH.FS_Transactions " & _
                    "where strAIRSNumber like '0413%" & cboAirs.Text & "%' " & _
                    "and Active = '1' " & _
                    "order by nuMFeeYear desc  "
                End If
            End If
            If SQL = "" Then
                'SQL = "Select * from AIRBRANCH.FSAddPaid " & _
                '"order by intyear desc  "

                SQL = "Select * from AIRBRANCH.FS_Transactions " & _
                "where Active = '1' " & _
                "order by nuMFeeYear desc  "
            End If

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FS_Transactions")
            rpt = New DepositQA11
            monitor.TrackFeature("Report." & rpt.ResourceName)
            rpt.SetDataSource(ds)


            If cboDepositNo.Text <> "" Then
                depositno = cboDepositNo.Text
                header = depositno
                airsno = ""
            Else
                If cboAirs.Text <> "" Then
                    airsno = cboAirs.Text
                    header = airsno
                    depositno = ""
                Else
                    MsgBox("Please select at least one value.", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If

            Dim p As New ParameterFields
            Dim p1 As New ParameterField
            Dim p2 As New ParameterDiscreteValue

            p1.ParameterFieldName = "SearchType"
            If cboDepositNo.Text = "" Then
                p2.Value = "Deposit"
            Else
                p2.Value = "AIRS"
            End If
            p1.CurrentValues.Add(p2)
            p.Add(p1)
            CRFeesReports.ParameterFieldInfo = p

            SetUpCrystalReportViewer(rpt, CRFeesReports, header)
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

#End Region

#Region "Compliance"

    Private Sub btnClassChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassChange.Click
        progress1.progress = -1
        Try
            pnlNSPS.Visible = False
            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False

            ds = New DataSet
            rpt = New ClassChanged10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "select * from AIRBRANCH.VW_Class_Changed"

            da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnNSPSChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSChange.Click
        Try

            pnlNSPS.Visible = True

            lblNSPS1.Visible = True
            lblNSPS2.Visible = True
            lblNSPS3.Visible = True

            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblNSPS1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNSPS1.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet

            SQL = "Select * " & _
            "from AIRBRANCH.VW_NSPS_Status " & _
            "where strnsps = 'YES' " & _
            "and STRnspsexempt = 'YES'"

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")

            rpt = New NSPSStatus10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            rpt.SetDataSource(ds)

            pnlNSPS.Visible = False

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS Exempt - Subject but exempt")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0

    End Sub
    Private Sub lblNSPS2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNSPS2.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New NSPSStatus1_10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * " & _
            "from AIRBRANCH.VW_NSPS_Status " & _
            "where Strnsps1 = 'YES' " & _
            "and strnsps = 'NO'"

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")
            rpt.SetDataSource(ds)

            pnlNSPS.Visible = False

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS Subject - Not subject")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Private Sub lblNSPS3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNSPS3.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New NSPSStatus2_10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * " & _
            "from AIRBRANCH.VW_NSPS_Status " & _
            "where strnsps = 'YES' " & _
            "and STRoperate <> 'YES'"

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")
            rpt.SetDataSource(ds)

            pnlNSPS.Visible = False

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS, Did not Operate")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Private Sub btnRunNonRespondent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunNonRespondent.Click
        Try

            If mtbNonRespondentYear.Text.Length <> 4 Or mtbNonRespondentYear.Text = "" Then
                mtbNonRespondentYear.Text = Date.Today.Year.ToString
            End If

            NoResponse()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub NoResponse()
        Try
            pnlNSPS.Visible = False
            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New NonRespondent10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.VW_NonRespondent " & _
            "where intYear = '" & mtbNonRespondentYear.Text & "' " & _
            "and intSubmittal <> '1' "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NonRespondent")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Failed to Submit Fee Data - " & mtbNonRespondentYear.Text)
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

        progress1.progress = 0

    End Sub
    Private Sub btnNoResponse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoResponse.Click
        Try

            pnlNSPS.Visible = True

            lblNSPS1.Visible = False
            lblNSPS2.Visible = False
            lblNSPS3.Visible = False

            mtbNonRespondentYear.Visible = True
            btnRunNonRespondent.Visible = True
            lblNonRespondant.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub btnNoOperate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoOperate.Click
        Try
            pnlNSPS.Visible = False
            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New NoOperate10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            SQL = "Select * from AIRBRANCH.VW_No_Operate "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_No_Operate")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Did Not Operate")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

#End Region

#Region "General"

    Private Sub btnComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComments.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityComments10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.FSPAYANDSUBMIT " & _
            "where strComments is not Null "

            SQL = "Select * from AIRBRANCH.FS_FeeData " & _
            "where strComment is not null " & _
            "and Active = '1' " & _
            "order by numfeeyear desc, strairsnumber "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSPAYANDSUBMIT")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Comments")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnFacInfoChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacInfoChange.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityInfo10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBRANCH.VW_Facility_Info "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Info")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Info")
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
    End Sub

    Private Sub btnTrainingReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrainingReg.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New TrainingReg10
            monitor.TrackFeature("Report." & rpt.ResourceName)

            SQL = "Select * from AIRBranch.VW_Training_reg "
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Training_reg")
            rpt.SetDataSource(ds)

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Training Registrants")
            CRFeesReports.ShowGroupTreeButton = False
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
    End Sub

#End Region


#End Region

 
  
  
    
    Private Sub btnViewStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewStats.Click
        Try

            SQL = "select * from " & _
"(select  " & _
"count(*) as FeeUniverse  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and Active = '1' ),  " & _
"(select  " & _
"count(*) as UnEnrolled  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '0' or strEnrolled is null)  " & _
"and Active = '1' ),  " & _
"(select  " & _
"count(*) as CeaseCollections  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "' " & _
"and numCurrentStatus = '12'  " & _
"and strEnrolled = '1'  " & _
"and Active = '1' ),  " & _
"(select  " & _
"count(*) as Enrolled  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numCurrentStatus <> '12'  " & _
"and strEnrolled = '1'  " & _
"and Active = '1'),  " & _
"(select  " & _
"count(*) as MailOUt  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numCurrentStatus <> '12'  " & _
"and strEnrolled = '1'  " & _
"and strInitialMailout = '1'  " & _
"and Active = '1'),   " & _
"(select  " & _
"count(*) as AddOnMailOut  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numCurrentStatus <> '12'  " & _
"and strEnrolled = '1'  " & _
"and strInitialMailout = '0'  " & _
"and Active = '1' ),   " & _
"(select  " & _
"count(*) as NotReported  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numcurrentstatus < '5'  " & _
"and strEnrolled = '1'  " & _
"and Active = '1' ) ,   " & _
"(select  " & _
"count(*) as InProgress  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numcurrentstatus > '4' " & _
"and numCurrentStatus < '8' " & _
"and strEnrolled = '1'  " & _
"and Active = '1' )  ,   " & _
"(select  " & _
"count(*) as Finalized  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numcurrentstatus > '7' " & _
"and strEnrolled = '1'  " & _
"and Active = '1' " & _
"and not exists (select * " & _
"from airbranch.fs_feeAudit " & _
"where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " & _
"and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " & _
"and fs_feeAudit.numfeeyear = '" & cboFeeStatYear.Text & "' " & _
"and fs_feeAudit.strendcollections = 'True')) ,   " & _
"(select  " & _
"count(*) as OnTime  " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numcurrentstatus > '4' " & _
"and numcurrentstatus < '12'  " & _
"and datSubmittal <= (select datFeeDueDate from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
"and Intsubmittal = '1' " & _
"and strEnrolled = '1'  " & _
"and Active = '1' ) ,   " & _
"(select  " & _
"count(*) as LateNoFees   " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numcurrentstatus > '4' " & _
"and numcurrentstatus < '12'  " & _
"and datSubmittal > (select datFeeDueDate from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')  " & _
"and datSubmittal <= (select datAdminApplicable from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
"and Intsubmittal = '1' " & _
"and strEnrolled = '1'  " & _
"and Active = '1' ) ,   " & _
"(select  " & _
"count(*) as LateWithFees   " & _
"from airbranch.FS_Admin  " & _
"where numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and numcurrentstatus > '4' " & _
"and numcurrentstatus < '12'  " & _
"and datSubmittal > (select datAdminApplicable from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
"and Intsubmittal = '1' " & _
"and strEnrolled = '1'  " & _
"and Active = '1' ) ,  " & _
"(select  " & _
"count(*) as NotPaid  " & _
"from AIRbranch.FS_Admin  " & _
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and active = '1'  " & _
"and numcurrentstatus <= '8'),  " & _
"(select  " & _
"count(*) as OutOfBalance   " & _
"from AIRbranch.FS_Admin  " & _
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and active = '1'  " & _
"and (numcurrentstatus = '9' or numcurrentstatus = '11' )),  " & _
                        "(select " & _
"count(*) as UnderPaid " & _
"from (select " & _
"(numTotalFee) - sum(numAmount) as TotalPaid " & _
"from AIRbranch.FS_Admin, " & _
"airbranch.FS_FeeAuditedData, " & _
"AIRbranch.FS_FeeInvoice " & _
"where  AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.FS_FeeAuditedData.strAIRSnumber " & _
"and AIRbranch.FS_Admin.numFeeYear = AIRbranch.FS_FeeAuditedData.numFeeyear " & _
"and airbranch.FS_Admin.strAIRSNumber = AIRbranch.FS_FeeInvoice.strAIRSNumber " & _
"and AIRbranch.FS_Admin.numFeeYear = AIRbranch.FS_FeeInvoice.numFeeyear " & _
"and AIRbranch.FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "' " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and AIRbranch.FS_Admin.active = '1' " & _
"and numcurrentstatus = '9' " & _
"group by numtotalfee ) " & _
"where totalpaid > 0 ), " & _
"(select " & _
"count(*) as OverPaid " & _
"from (select " & _
"AIRBranch.FS_Admin.strAIRSNumber " & _
"from AIRbranch.FS_Admin, " & _
"airbranch.FS_FeeAuditedData, " & _
"AIRbranch.FS_FeeInvoice " & _
"where  AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.FS_FeeAuditedData.strAIRSnumber " & _
"and AIRbranch.FS_Admin.numFeeYear = AIRbranch.FS_FeeAuditedData.numFeeyear " & _
"and airbranch.FS_Admin.strAIRSNumber = AIRbranch.FS_FeeInvoice.strAIRSNumber " & _
"and AIRbranch.FS_Admin.numFeeYear = AIRbranch.FS_FeeInvoice.numFeeyear " & _
"and AIRbranch.FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "' " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and AIRbranch.FS_Admin.active = '1' " & _
"and (numcurrentstatus = '9' or numcurrentstatus = '11' )) " & _
" ), " & _
"(select  " & _
"count(*) as OutOfBalanceAnnual  " & _
"from AIRbranch.FS_Admin, airbranch.fs_feeAuditedData   " & _
"where airbranch.fs_admin.strAIRSNumber = Airbranch.FS_FeeAuditedData.strAIRSNumber " & _
"and airbranch.fs_admin.nuMFeeYear  = Airbranch.FS_FeeAuditedData.nuMFeeYear  " & _
"and airbranch.FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and AIRBranch.FS_Admin.active = '1'  " & _
"and numcurrentstatus = '9' " & _
"and (strPaymentPlan = 'Entire Annual Year' or strPaymentPlan is null) ),  " & _
"(select  " & _
"count(*) as OutOfBalanceQuarterly " & _
"from AIRbranch.FS_Admin, airbranch.fs_feeAuditedData   " & _
"where airbranch.fs_admin.strAIRSNumber = Airbranch.FS_FeeAuditedData.strAIRSNumber " & _
"and airbranch.fs_admin.nuMFeeYear  = Airbranch.FS_FeeAuditedData.nuMFeeYear  " & _
"and airbranch.FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and AIRBranch.FS_Admin.active = '1'  " & _
"and numcurrentstatus = '9' " & _
"and strPaymentPlan = 'Four Quarterly Payments'),  " & _
"(select  " & _
"count(*) as PaidInFull   " & _
"from AIRbranch.FS_Admin  " & _
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and active = '1'  " & _
"and numcurrentstatus = '10'),  " & _
"(select  " & _
"count(*) as FinalPaid     " & _
"from AIRbranch.FS_Admin  " & _
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and active = '1'  " & _
"and numcurrentstatus = '10' " & _
"and intSubmittal = '1' ) ,  " & _
"(select  " & _
"count(*) as NotFinalPaid     " & _
"from AIRbranch.FS_Admin  " & _
"where numfeeyear = '" & cboFeeStatYear.Text & "'  " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and active = '1'  " & _
"and numcurrentstatus = '10' " & _
"and (intSubmittal = '0' or intsubmittal is null))    "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryFeeUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFeeUniverse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryUnEnrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryUnEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '0' or strEnrolled is null)  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryCeaseCollection_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryCeaseCollection.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numCurrentStatus = '12'  " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryEnrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numCurrentStatus <> '12'  " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryMailOut.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numCurrentStatus <> '12'  " & _
            "and strEnrolled = '1'  " & _
            "and strInitialMailout = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryAdditions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryAdditions.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numCurrentStatus <> '12'  " & _
            "and strEnrolled = '1'  " & _
            "and strInitialMailout = '0'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryNotReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryNotReported.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If
 
            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus < '5'  " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryInProgress_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryInProgress.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If
 
            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus > '4' " & _
            "and numCurrentStatus < '8' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If
 
            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus > '7' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
                "and not exists (select * " & _
                "from airbranch.fs_feeAudit " & _
                "where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " & _
                "and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " & _
                "and fs_feeAudit.numfeeyear = '" & cboFeeStatYear.Text & "' " & _
                "and fs_feeAudit.strendcollections = 'True')" & _
                "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryOnTime_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOnTime.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus > '4' " & _
            "and numcurrentstatus < '12'  " & _
            "and datSubmittal <= (select datFeeDueDate from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
            "and Intsubmittal = '1' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryLateResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryLateResponse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
           "and numcurrentstatus > '4' " & _
            "and numcurrentstatus < '12'  " & _
            "and datSubmittal > (select datFeeDueDate from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')  " & _
            "and datSubmittal <= (select datAdminApplicable from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
            "and Intsubmittal = '1' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryLateWithFee_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryLateWithFee.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus > '4' " & _
            "and numcurrentstatus < '12'  " & _
            "and datSubmittal > (select datAdminApplicable from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
            "and Intsubmittal = '1' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryNotPaid_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryNotPaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and numcurrentstatus <= '8' " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryOutofBalance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOutofBalance.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If
             
            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPartial_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPartial.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If


            SQL = "Select  " & _
            "substr(AIRBranch.FSUnderPaid.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc  " & _
"from (select " & _
"(numTotalFee) - sum(numAmount) as TotalPaid, AIRbranch.FS_Admin.strairsnumber " & _
"from AIRbranch.FS_Admin, Airbranch.FS_FeeAuditedData, " & _
"AIRbranch.FS_FeeInvoice , " & _
            "AIRBranch.FSLK_Admin_Status " & _
"where  AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.FS_FeeAuditedData.strAIRSnumber " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
"and AIRbranch.FS_Admin.numFeeYear = AIRbranch.FS_FeeAuditedData.numFeeyear " & _
"and airbranch.FS_Admin.strAIRSNumber = AIRbranch.FS_FeeInvoice.strAIRSNumber " & _
"and AIRbranch.FS_Admin.numFeeYear = AIRbranch.FS_FeeInvoice.numFeeyear " & _
"and AIRbranch.FS_Admin.numfeeyear = '" & cboFeeStatYear.Text & "' " & _
"and (strEnrolled = '1' or strEnrolled is null)  " & _
"and AIRbranch.FS_Admin.active = '1' " & _
"and numcurrentstatus = '9' " & _
"group by numtotalfee )FSUnderPaid, AIRBranch.APBFacilityInformation " & _
"where totalpaid > 0   " & _
"and AIRBranch.FSUnderPaid.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryAnnual_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryAnnual.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
          "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
          "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
          "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                      "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
          "and (strEnrolled = '1' or strEnrolled is null)  " & _
          "and numcurrentstatus = '9' " & _
          "and (strPaymentPlan = 'Entire Annual Year' or strPaymentPlan is null) " & _
          "and AIRBranch.FS_Admin.Active = '1' " & _
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryQuarterly_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryQuarterly.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
          "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
          "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
          "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                      "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
          "and (strEnrolled = '1' or strEnrolled is null)  " & _
          "and numcurrentstatus = '9' " & _
          "and strPaymentPlan = 'Four Quarterly Payments' " & _
          "and AIRBranch.FS_Admin.Active = '1' " & _
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryOverpaid_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOverpaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If


            SQL = "Select  " & _
          "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
          "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
          "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                      "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
          "and (strEnrolled = '1' or strEnrolled is null)  " & _
          "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " & _
          "and strPaymentPlan = 'Four Quarterly Payments' " & _
          "and AIRBranch.FS_Admin.Active = '1' " & _
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPaidInFull_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidInFull.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                        "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and numcurrentstatus = '10' " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPaidFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
          "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
          "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
          "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                      "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
          "and (strEnrolled = '1' or strEnrolled is null)  " & _
          "and numcurrentstatus = '10' " & _
          "and intSubmittal = '1' " & _
          "and AIRBranch.FS_Admin.Active = '1' " & _
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryPaidNotFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidNotFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
          "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " & _
          "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status " & _
          "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
                      "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
          "and numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
          "and (strEnrolled = '1' or strEnrolled is null)  " & _
          "and numcurrentstatus = '10' " & _
          "and (intSubmittal = '0' or intsubmittal is null) " & _
          "and AIRBranch.FS_Admin.Active = '1' " & _
          "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    
    Private Sub llbDetailFeeUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailFeeUniverse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "



            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, " & _
            "AIRBranch.APBFacilityInformation.strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBranch.FS_Mailout.STRFIRSTNAME as strContactFirstName,  " & _
            "AIRBranch.FS_Mailout.STRLASTNAME as strContactLastName,  " & _
            "AIRBranch.FS_Mailout.STRContactCONAME as strContactCompanyName,  " & _
            "AIRBranch.FS_Mailout.STRCONTACTADDRESS1 as strContactAddress,  " & _
            "AIRBranch.FS_Mailout.STRCONTACTCITY,  " & _
            "AIRBranch.FS_Mailout.STRCONTACTSTATE,  " & _
            "AIRBranch.FS_Mailout.STRCONTACTZIPCODE,  " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1,  " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY,  " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE,  " & _
            "AIRBranch.FS_Mailout.strGecoUserEmail as strContactEmail,  " & _
            "'' as strContactPhoneNumber, " & _
            "datShutDown, FS_Mailout.strClass, " & _
            "case  " & _
            "when strOperate = '1' then 'Operating'  " & _
            "else 'Not Operating'  " & _
            "end Operating,  " & _
            "case  " & _
            "when FS_Mailout.strPart70 = '1' then 'True'  " & _
            "else 'False'  " & _
            "end Part70,  " & _
            "case  " & _
            "when FS_Mailout.strNSPS = '1' then 'True'  " & _
            "else 'False'  " & _
            "end NSPS,  " & _
            "numTotalFee, sum(numPayment) as TotalPaid  " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation,  " & _
            "AIRBranch.FSLK_Admin_Status,   " & _
            "AIRBranch.FS_Mailout, AIRBranch.FS_FeeAuditedData,  " & _
            "AIRBranch.FS_Transactions  " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+)  " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Mailout.strAIRSNumber (+)  " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+)  " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+)  " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Mailout.numFeeYear (+)  " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+)  " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id  " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'    " & _
            "and AIRBranch.FS_Admin.Active = '1' GROUP BY substr(AIRBranch.FS_Admin.strAIRSNumber, 5), AIRBranch.APBFacilityInformation.strFacilityName, strIAIPDesc, AIRBranch.FS_Mailout.STRFIRSTNAME, AIRBranch.FS_Mailout.STRLASTNAME, AIRBranch.FS_Mailout.STRContactCONAME, AIRBranch.FS_Mailout.STRCONTACTADDRESS1, AIRBranch.FS_Mailout.STRCONTACTCITY, AIRBranch.FS_Mailout.STRCONTACTSTATE, AIRBranch.FS_Mailout.STRCONTACTZIPCODE, AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_Mailout.strGecoUserEmail, '', datShutDown, airbranch.FS_Admin.strComment,  FS_Mailout.strClass, case  " & _
            "when strOperate = '1' then 'Operating'  " & _
            "else 'Not Operating'  " & _
            "end, case  " & _
            "when FS_Mailout.strPart70 = '1' then 'True'  " & _
            "else 'False'  " & _
            "end, case  " & _
            "when FS_Mailout.strNSPS = '1' then 'True'  " & _
            "else 'False'  " & _
            "end, numTotalFee "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailUnEnrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailUnEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '0' or strEnrolled is null)  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailCeaseCollection_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailCeaseCollection.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numCurrentStatus = '12'  " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailEnrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailEnrolled.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
                  "and numCurrentStatus <> '12'  " & _
          "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailMailout_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailMailout.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
              "and numCurrentStatus <> '12'  " & _
          "and strEnrolled = '1'  " & _
          "and strInitialMailout = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailAdditions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailAdditions.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
                  "and numCurrentStatus <> '12'  " & _
            "and strEnrolled = '1'  " & _
            "and strInitialMailout = '0'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailNotReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailNotReported.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, airbranch.FS_Admin.strcomment, " & _
            "APBFacilityInformation.strFacilityName, strIAIPDesc,  " & _
            "AIRBRANCH.FS_Mailout.STRFIRSTNAME, " & _
            "AIRBRANCH.FS_Mailout.STRLASTNAME, " & _
            "AIRBRANCH.FS_Mailout.STRContactCONAME, " & _
            "AIRBRANCH.FS_Mailout.STRCONTACTADDRESS1, " & _
            "AIRBRANCH.FS_Mailout.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_Mailout.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_Mailout.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "datShutDown, AIRBRANCH.FS_Mailout.strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when AIRBRANCH.FS_Mailout.strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when AIRBRANCH.FS_Mailout.strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_Mailout, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Mailout.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Mailout.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
              "and numcurrentstatus < '5'  " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , APBFACILITYINFORMATION.strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_Mailout.STRFIRSTNAME, " & _
            "AIRBranch.FS_Mailout.STRLastNAME, AIRBranch.FS_Mailout.STRContactCONAME, " & _
            "AIRBranch.FS_Mailout.STRCONTACTADDRESS1, AIRBranch.FS_Mailout.STRCONTACTCITY, " & _
            "AIRBranch.FS_Mailout.STRCONTACTSTATE, AIRBranch.FS_Mailout.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "datShutDown, FS_Mailout.strClass, " & _
            "StrOperate, " & _
            "FS_Mailout.strPart70," & _
            "FS_Mailout.strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailInProgress_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailInProgress.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
              "and numcurrentstatus > '4' " & _
         "and numCurrentStatus < '8' " & _
         "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
              "and numcurrentstatus > '7' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
               "and not exists (select * " & _
                "from airbranch.fs_feeAudit " & _
                "where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " & _
                "and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " & _
                "and fs_feeAudit.numfeeyear = '" & cboFeeStatYear.Text & "' " & _
                "and fs_feeAudit.strendcollections = 'True')" & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "




            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailLateResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailLateResponse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus > '4' " & _
            "and numcurrentstatus < '12'  " & _
            "and datSubmittal > (select datFeeDueDate from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')  " & _
            "and datSubmittal <= (select datAdminApplicable from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
            "and Intsubmittal = '1' " & _
            "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailLateWithFee_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailLateWithFee.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and numcurrentstatus > '4' " & _
           "and numcurrentstatus < '12'  " & _
          "and datSubmittal > (select datAdminApplicable from AIRbranch.FS_FeeRate where numFeeyear = '" & cboFeeStatYear.Text & "')   " & _
          "and Intsubmittal = '1' " & _
          "and strEnrolled = '1'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailNotPaid_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailNotPaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "SELECT   SUBSTR(AD.STRAIRSNUMBER, 5) AS strAIRSNumber, " & _
            "    FI.STRFACILITYNAME, " & _
            "    LK_AS.STRIAIPDESC, " & _
            "    AD.STRCOMMENT, " & _
            "    CI.STRCONTACTFIRSTNAME, " & _
            "    CI.STRCONTACTLASTNAME, " & _
            "    CI.STRCONTACTCOMPANYNAME, " & _
            "    CI.STRCONTACTADDRESS, " & _
            "    CI.STRCONTACTCITY, " & _
            "    CI.STRCONTACTSTATE, " & _
            "    CI.STRCONTACTZIPCODE, " & _
            "    FI.STRFACILITYSTREET1, " & _
            "    FI.STRFACILITYCITY, " & _
            "    FI.STRFACILITYZIPCODE, " & _
            "    CI.STRCONTACTEMAIL, " & _
            "    CI.STRCONTACTPHONENUMBER, " & _
            "    FAD.DATSHUTDOWN, " & _
            "    FAD.STRCLASS, " & _
            "    CASE " & _
            "      WHEN FAD.STROPERATE = '1' " & _
            "      THEN 'Operating' " & _
            "      ELSE 'Not Operating' " & _
            "    END Operating, " & _
            "    CASE " & _
            "      WHEN FAD.STRPART70 = '1' " & _
            "      THEN 'True' " & _
            "      ELSE 'False' " & _
            "    END Part70, " & _
            "    CASE " & _
            "      WHEN FAD.STRNSPS = '1' " & _
            "      THEN 'True' " & _
            "      ELSE 'False' " & _
            "    END NSPS, " & _
            "    FAD.NUMTOTALFEE, " & _
            "    SUM(TRX.NUMPAYMENT) AS TotalPaid " & _
            "  FROM airbranch.FS_Admin AD, " & _
            "    AIRBRANCH.APBFacilityInformation FI, " & _
            "    AIRBRANCH.FSLK_Admin_Status LK_AS, " & _
            "    AIRBRANCH.FS_ContactInfo CI, " & _
            "    AIRBRANCH.FS_FeeAuditedData FAD, " & _
            "    (SELECT * FROM AIRBRANCH.FS_Transactions TR WHERE TR.ACTIVE = '1' " & _
            "    ) TRX " & _
            "  WHERE AD.STRAIRSNUMBER     = FI.STRAIRSNUMBER " & _
            "    AND AD.STRAIRSNUMBER     = FAD.STRAIRSNUMBER(+) " & _
            "    AND AD.STRAIRSNUMBER     = CI.STRAIRSNUMBER(+) " & _
            "    AND AD.STRAIRSNUMBER     = TRX.STRAIRSNUMBER(+) " & _
            "    AND AD.NUMFEEYEAR        = FAD.NUMFEEYEAR(+) " & _
            "    AND AD.NUMFEEYEAR        = CI.NUMFEEYEAR(+) " & _
            "    AND AD.NUMFEEYEAR        = TRX.NUMFEEYEAR(+) " & _
            "    AND AD.NUMCURRENTSTATUS  = LK_AS.ID " & _
            "    AND AD.NUMFEEYEAR        = '2013' " & _
            "    AND (AD.STRENROLLED      = '1' " & _
            "    OR AD.STRENROLLED       IS NULL) " & _
            "    AND AD.NUMCURRENTSTATUS <= '8' " & _
            "    AND AD.ACTIVE            = '1' " & _
            "  GROUP BY FI.STRFACILITYNAME, " & _
            "    LK_AS.STRIAIPDESC, " & _
            "    AD.STRCOMMENT, " & _
            "    CI.STRCONTACTFIRSTNAME, " & _
            "    CI.STRCONTACTLASTNAME, " & _
            "    CI.STRCONTACTCOMPANYNAME, " & _
            "    CI.STRCONTACTADDRESS, " & _
            "    CI.STRCONTACTCITY, " & _
            "    CI.STRCONTACTSTATE, " & _
            "    CI.STRCONTACTZIPCODE, " & _
            "    FI.STRFACILITYSTREET1, " & _
            "    FI.STRFACILITYCITY, " & _
            "    FI.STRFACILITYZIPCODE, " & _
            "    CI.STRCONTACTEMAIL, " & _
            "    CI.STRCONTACTPHONENUMBER, " & _
            "    FAD.DATSHUTDOWN, " & _
            "    FAD.STRCLASS, " & _
            "    FAD.NUMTOTALFEE, " & _
            "    AD.STRAIRSNUMBER, " & _
            "    FAD.STROPERATE, " & _
            "    FAD.STRPART70, " & _
            "    FAD.STRNSPS " & _
            "  ORDER BY strAIRSNumber"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailOutOfBalance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailOutOfBalance.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
             "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "



            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPartial_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailPartial.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailAnnual_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailAnnual.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailQuarterly_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailQuarterly.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailOverpaid_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailOverpaid.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPaidInFull_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailPaidInFull.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
                 "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and numcurrentstatus = '10' " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPaidFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailPaidFinalized.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, " & _
            "airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and numcurrentstatus = '10' " & _
            "and intSubmittal = '1' " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strComment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbDetailPaidNotFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDetailPaidNotFinalized.LinkClicked

        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                ds = New DataSet
                dgvFeeStats.DataSource = ds
                Exit Sub
            End If

            SQL = "Select  " & _
            "substr(AIRBranch.FS_Admin.strAIRSNumber, 5) as strAIRSNumber, strFacilityName, strIAIPDesc, " & _
            "airbranch.FS_Admin.strComment,  " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTLASTNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTADDRESS, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTSTATE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "AIRBRANCH.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBRANCH.FS_ContactInfo.strContactPhoneNumber, " & _
            "datShutDown, strClass, " & _
            "case " & _
            "when strOperate = '1' then 'Operating' " & _
            "else 'Not Operating' " & _
            "end Operating, " & _
            "case " & _
            "when strPart70 = '1' then 'True' " & _
            "else 'False' " & _
            "end Part70, " & _
            "case " & _
            "when strNSPS = '1' then 'True' " & _
            "else 'False' " & _
            "end NSPS, " & _
            "numTotalFee, sum(numPayment) as TotalPaid " & _
            "from airbranch.FS_Admin, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FSLK_Admin_Status,  " & _
            "AIRBranch.FS_ContactInfo, AIRBranch.FS_FeeAuditedData, " & _
            "AIRBranch.FS_Transactions " & _
            "where AIRBranch.FS_Admin.strAIRSNumber = AIRbranch.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_ContactInfo.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FS_Transactions.strAIRSNumber (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_ContactInfo.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numFeeYear = AIRBranch.FS_Transactions.numFeeYear (+) " & _
            "and AIRBranch.FS_Admin.numcurrentstatus = airbranch.FSLK_Admin_Status.id " & _
            "and AIRBranch.FS_Admin.numFeeyear = '" & cboFeeStatYear.Text & "'  " & _
            "and (strEnrolled = '1' or strEnrolled is null)  " & _
            "and numcurrentstatus = '10' " & _
            "and (intSubmittal = '0' or intsubmittal is null) " & _
            "and AIRBranch.FS_Admin.Active = '1' " & _
            "group by AIRBranch.FS_Admin.strAIRSNumber , strFacilityName, " & _
            "strIAIPDesc, AIRBranch.FS_ContactInfo.STRCONTACTFIRSTNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTLASTNAME, AIRBranch.FS_ContactInfo.STRContactCOMPANYNAME, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTADDRESS, AIRBranch.FS_ContactInfo.STRCONTACTCITY, " & _
            "AIRBranch.FS_ContactInfo.STRCONTACTSTATE, AIRBranch.FS_ContactInfo.STRCONTACTZIPCODE, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYSTREET1, AIRBranch.APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "AIRBranch.APBFACILITYINFORMATION.STRFACILITYZIPCODE, AIRBranch.FS_ContactInfo.STRCONTACTEMAIL, " & _
            "AIRBranch.FS_ContactInfo.strContactPhoneNumber, datShutDown, strClass, " & _
            "StrOperate, " & _
            "strPart70," & _
            "strNSPS, " & _
            "numTotalFee, airbranch.FS_Admin.strcomment " & _
            "order by strAIRSNumber "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

 
   
    Private Sub btnExportFeeStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFeeStats.Click
        dgvFeeStats.ExportToExcel(Me)
    End Sub

    Private Sub dgvFeeStats_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFeeStats.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckInvoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckInvoices.Click
        Try
            If cboFeeStatYear.Text <> "" Then
                Dim query As String = "Update airbranch.FS_FeeInvoice set " & _
                "strInvoiceStatus = '1', " & _
                "UpdateUser = :Username,  " & _
                "updateDateTime = sysdate " & _
                "where numFeeYear = :FeeYear " & _
                "and numAmount = '0' " & _
                "and strInvoiceStatus = '0' " & _
                "and active = '1' "

                Dim parameters As OracleParameter() = New OracleParameter() { _
                    New OracleParameter("Username", UserName), _
                    New OracleParameter("FeeYear", cboFeeStatYear.Text)
                }

                If Not DB.RunCommand(query, parameters) Then
                    MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                query = "Select " & _
                "strAirsnumber " & _
                "from AIRBranch.FS_FeeInvoice " & _
                "where numAmount = '0' " & _
                "and strInvoiceStatus = '1' " & _
                "and Active = '1' " & _
                "and updateUser = :Username " & _
                "and numFeeyear = :FeeYear "

                Using connection As New OracleConnection(DB.CurrentConnectionString)
                    Using cmd As OracleCommand = connection.CreateCommand
                        cmd.CommandType = CommandType.Text
                        cmd.BindByName = True
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnInvoicedPaymentDue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoicedPaymentDue.Click
        Try
            SQL = " "


            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " & _
"substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, " & _
"case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
"numAmount as Due,  " & _
"AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
"numAmount as numTotalFee, strClass, '' as numAdminFee   " & _
"From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
"AIRBranch.APBHeaderData, AIRBranch.FS_Admin " & _
"where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
"and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  "

                Case "ANNUAL"
                    SQL = "select " & _
  "substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
  "strFacilityName, " & _
  "case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
 "numAmount as Due,  " & _
 "AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
 "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
 "numAmount as numTotalFee, strClass, '' as numAdminFee   " & _
  "From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
  "AIRBranch.APBHeaderData, AIRBranch.FS_Admin  " & _
  "where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
  "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
  "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " & _
  "and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
  " and strPayType = '1' "

                Case "ALL QUARTERS"
                    SQL = "select " & _
"substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, " & _
"case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
"numAmount as Due,  " & _
"AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
"numAmount as numTotalFee, strClass, '' as numAdminFee   " & _
"From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
"AIRBranch.APBHeaderData, AIRBranch.FS_Admin  " & _
"where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
"and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " & _
"and (AIRBranch.FS_FeeInvoice.strPayType = '2' " & _
"or AIRBranch.FS_FeeInvoice.strPayType = '3' " & _
"or AIRBranch.FS_FeeInvoice.strPayType = '4' " & _
"or AIRBranch.FS_FeeInvoice.strPayType = '5') "
                Case "QUARTER ONE"
                    SQL = "select " & _
"substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, " & _
"case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
"numAmount as Due,  " & _
"AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " & _
"From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
"AIRBranch.APBHeaderData, AIRBranch.FS_Admin  " & _
"where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
"and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " & _
"and aIRBranch.FS_FeeInvoice.strPayType = '2'  "
                Case "QUARTER TWO"
                    SQL = "select " & _
"substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, " & _
"case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
"numAmount as Due,  " & _
"AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " & _
"From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
"AIRBranch.APBHeaderData, AIRBranch.FS_Admin  " & _
"where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
"and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " & _
"and AIRBranch.FS_FeeInvoice.strPayType = '3'  "
                Case "QUARTER THREE"
                    SQL = "select " & _
"substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, " & _
"case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
"numAmount as Due,  " & _
"AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " & _
"From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
"AIRBranch.APBHeaderData, AIRBranch.FS_Admin  " & _
"where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
"and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " & _
"and AIRBranch.FS_FeeInvoice.strPayType = '4'  "
                Case "QUARTER FOUR"
                    SQL = "select " & _
"substr(AIRBranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, " & _
"case " & _
"when strPayType = '1' then 'ANNUAL' " & _
"when strPayType = '2' then 'QUARTER ONE' " & _
"when strPayType = '3' then 'QUARTER TWO' " & _
"when strPayType = '4' then 'QUARTER THREE' " & _
"when strPayType = '5' then 'QUARTER FOUR' " & _
"End strPaymentPlan, " & _
"numAmount as Due,  " & _
"AIRBranch.FS_FeeInvoice.numFeeYear,   " & _
"'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " & _
"numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " & _
"From AIRBranch.APBFacilityInformation, AIRBranch.FS_FeeInvoice, " & _
"AIRBranch.APBHeaderData, AIRBranch.FS_Admin  " & _
"where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FS_FeeInvoice.strAIRSNumber   " & _
"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
"and AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_Admin.strAIRSnumber " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_Admin.numFeeYear " & _
"and AIRBranch.FS_FeeInvoice.Active = '1' " & _
"and AIRBranch.FS_Admin.Active = '1' " & _
"and numCurrentStatus <> '12'  " & _
"and AIRBranch.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "'  " & _
"and  AIRBranch.FS_FeeInvoice.strPayType = '5'  "
                Case "AMENDMENT", "ONE-TIME", "REFUND"
                    'SQL = "select  " & _
                    '"substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    '"strFacilityName, strPaymentPlan,  " & _
                    '"(numTotalFee)/4 as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    '"numPart70Fee, numSMFee, numNSPSFee,  " & _
                    '"numTotalFee, strClass, numAdminFee  " & _
                    '"From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                    '"and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    '"and strPaymentPlan is null "
                Case Else
                    'SQL = "select  " & _
                    '"substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber,  " & _
                    '"strFacilityName, strPaymentPlan,  " & _
                    '"(numTotalFee) as Due, AIRBRANCH.FS_FeeAuditedData.numFeeYear,  " & _
                    '"numPart70Fee, numSMFee, numNSPSFee,  " & _
                    '"numTotalFee, strClass, numAdminFee  " & _
                    '"From AIRBRANCH.APBFacilityInformation, AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.FS_FeeAuditedData.strAIRSNumber  " & _
                    '"and AIRBRANCH.FS_FeeAuditedData.Active = '1' " & _
                    '"and AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' "
            End Select

            If SQL <> "" Then

                ds = New DataSet
                da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenFeesLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFeesLog.Click
        Dim parameters As New Generic.Dictionary(Of String, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtFeeStatAirsNumber.Text) Then
            parameters("airsnumber") = txtFeeStatAirsNumber.Text
        End If
        parameters("feeyear") = cboFeeStatYear.Text

        OpenSingleForm("PASPFeeAuditLog", parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub btnInvoiceReportVariance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoiceReportVariance.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    'SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' "

                    SQL = "select " & _
  "VarianceReport.strAIRSNumber, strFacilityName, " & _
  "numFeeyear, AmountDue, " & _
  "PayerType  " & _
  "from ( " & _
  "select " & _
  "strAIRSNumber, numFeeYear, " & _
  "numtotalfee as AmountDue, " & _
  "to_char(strPaymentPlan) as PayerType " & _
  "from airbranch.FS_FeeAuditedData " & _
  "where fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
  "and fs_feeAuditedData.Active = '1' " & _
  "and not exists (select * " & _
  "from AIRbranch.FS_FeeInvoice " & _
  "where fs_feeInvoice.nuMFeeyear = '" & cboStatYear.Text & "' " & _
  "and FS_FeeAuditedData.strAIRSNumber = FS_FeeInvoice.strAIRSNumber " & _
  "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeYear " & _
  "and FS_feeInvoice.active = '1' ) " & _
  "union " & _
  "select " & _
  "strAIRSNumber, numfeeyear, " & _
  "numAmount as AmountDue, " & _
  "to_char(strPaytype) as PayerType  " & _
  "from AIRBranch.FS_FeeInvoice " & _
  "where fs_feeInvoice.numFeeyear = '" & cboStatYear.Text & "' " & _
  "and fs_feeInvoice.active = '1' " & _
  "and not exists (select * " & _
  "from AIRBranch.FS_FeeAuditedData " & _
  "where fs_feeAuditedData.nuMfeeyear = '" & cboStatYear.Text & "' " & _
  "and FS_FeeAuditedData.active = '1' " & _
  "and FS_FeeInvoice.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " & _
  "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear ) )VarianceReport , " & _
  "airbranch.APBfacilityinformation " & _
  "where VarianceREport.strAIRSNumber = APBFacilityInformation.strAIRSNumber "






                    SQL = "select " & _
                     "substr(VarianceReport.strAIRSNumber, 5) as strAIRSNumber, " & _
                     "strFacilityName, " & _
                     "numFeeyear, AmountDue, " & _
                     "case " & _
                     "when PayerType = '1' then 'ANNUAL' " & _
                     "when PayerType = '2' then 'QUARTERLY' " & _
                     "else PayerType " & _
                     "End PayerType  " & _
                     "from ( " & _
                   "select   " & _
                    "strAIRSNumber, numFeeYear,   " & _
                    "numtotalfee as AmountDue,   " & _
                    "to_char(strPaymentPlan) as PayerType   " & _
                    "from airbranch.FS_FeeAuditedData   " & _
                    "where fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "'   " & _
                    "and fs_feeAuditedData.Active = '1'   " & _
                    "and not exists (select *   " & _
                    "from AIRbranch.FS_FeeInvoice   " & _
                    "where fs_feeInvoice.nuMFeeyear = '" & cboStatYear.Text & "'   " & _
                    "and FS_FeeAuditedData.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " & _
                    "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeYear   " & _
                    "and FS_feeInvoice.active = '1' ) " & _
                    "and numtotalfee is not null  " & _
                    "union  " & _
                    "select   " & _
                    "strAIRSNumber, numfeeyear,   " & _
                    "numAmount as AmountDue,   " & _
                    "to_char(strPaytype) as PayerType    " & _
                    "from AIRBranch.FS_FeeInvoice   " & _
                    "where fs_feeInvoice.numFeeyear = '" & cboStatYear.Text & "'   " & _
                    "and fs_feeInvoice.active = '1'   " & _
                    "and not exists (select *   " & _
                    "from AIRBranch.FS_FeeAuditedData   " & _
                    "where fs_feeAuditedData.nuMfeeyear = '" & cboStatYear.Text & "'   " & _
                    "and FS_FeeAuditedData.active = '1'   " & _
                    "and FS_FeeInvoice.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber   " & _
                    "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear )  " & _
                    "union   " & _
                    "select   " & _
                    "strAIRSNumber, numfeeyear,   " & _
                    "numAmount as AmountDue,   " & _
                    "to_char(strPaytype) as PayerType    " & _
                    "from AIRBranch.FS_FeeInvoice   " & _
                    "where fs_feeInvoice.numFeeyear = '" & cboStatYear.Text & "'   " & _
                    "and fs_feeInvoice.active = '1'   " & _
                    "and  exists (select *   " & _
                    "from AIRBranch.FS_FeeAuditedData   " & _
                    "where fs_feeAuditedData.nuMfeeyear = '" & cboStatYear.Text & "'   " & _
                    "and FS_FeeAuditedData.active = '1'   " & _
                    "and FS_FeeInvoice.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber   " & _
                    "and FS_FeeInvoice.numFeeYear = FS_FeeAuditedData.numFeeYear  " & _
                    "and numamount <> numTotalFee )  " & _
                    "and strPaytype = '1'  " & _
                    "union   " & _
                    "select   " & _
                    "strAIRSNumber, numFeeYear,   " & _
                    "numtotalfee as AmountDue,   " & _
                    "to_char(strPaymentPlan) as PayerType   " & _
                    "from airbranch.FS_FeeAuditedData   " & _
                    "where fs_feeAuditedData.numFeeYear = '" & cboStatYear.Text & "'   " & _
                    "and fs_feeAuditedData.Active = '1'   " & _
                    "and  exists (select *   " & _
                    "from AIRbranch.FS_FeeInvoice   " & _
                    "where fs_feeInvoice.nuMFeeyear = '" & cboStatYear.Text & "'   " & _
                    "and FS_FeeAuditedData.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " & _
                    "and FS_FeeAuditedData.numFeeYear = FS_FeeInvoice.numFeeYear   " & _
                    "and FS_feeInvoice.active = '1' " & _
                    "and numamount <> numTotalFee) " & _
                    "and numtotalfee is not null " & _
                    "and strPaymentPlan = 'Entire Annual Year'  ) VarianceReport , " & _
                     "airbranch.APBfacilityinformation " & _
                     "where VarianceREport.strAIRSNumber = APBFacilityInformation.strAIRSNumber "




                   
                Case "ANNUAL"
                    'SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan = 'Entire Annual Year' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRbranch.FS_FeeInvoice.strPayType = '1'  " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '1' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "

                   
                Case "ALL QUARTERS"
                    'SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan = 'Four Quarterly Payments' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRbranch.FS_FeeInvoice.strPayType <> '1'  " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' " & _
                    '"and (AIRBRANCH.FS_FeeInvoice.strPayType = '2' " & _
                    '"or AIRBRANCH.FS_FeeInvoice.strPayType = '3' " & _
                    '"or AIRBRANCH.FS_FeeInvoice.strPayType = '4' " & _
                    '"or AIRBRANCH.FS_FeeInvoice.strPayType = '5') "

                   
                Case "QUARTER ONE"
                    'SQLReported = "Select sum(numtotalFee/4) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan = 'Four Quarterly Payments' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRbranch.FS_FeeInvoice.strPayType = '2'  " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '2' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "

                Case "QUARTER TWO"
                    'SQLReported = "Select sum(numtotalFee/4) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan = 'Four Quarterly Payments' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRbranch.FS_FeeInvoice.strPayType = '3'  " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '3' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "

                Case "QUARTER THREE"
                    'SQLReported = "Select sum(numtotalFee/4) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan = 'Four Quarterly Payments' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRbranch.FS_FeeInvoice.strPayType = '4'  " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '4' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "

                Case "QUARTER FOUR"
                    'SQLReported = "Select sum(numtotalFee/4 ) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan = 'Four Quarterly Payments' "

                    'SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " & _
                    '"from AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and AIRbranch.FS_FeeInvoice.strPayType = '5'  " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '5' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "

                   
                Case "AMENDMENT"
                    'SQLReported = "Select sum(numtotalFee ) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan is null "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '6' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "ONE-TIME"
                    'SQLReported = "Select sum(numtotalFee ) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' " & _
                    '"and strpaymentplan is null "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '8' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case "REFUND"
                    'SQLReported = "Select sum(0) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' "

                    'SQLPaid = "select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice " & _
                    '"where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.invoiceID " & _
                    '"and AIRBRANCH.FS_FeeInvoice.strPayType = '7' " & _
                    '"and AIRBRANCH.FS_Transactions.nuMFeeYEar = '" & cboStatYear.Text & "' " & _
                    '"and AIRBRANCH.FS_Transactions.active = '1' "
                Case Else
                    'SQLReported = "Select sum(numtotalFee) as TotalDue " & _
                    '"from AIRBRANCH.FS_FeeAuditedData " & _
                    '"where AIRBRANCH.FS_FeeAuditedData.numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' "

                    'SQLPaid = "Select sum(numPayment) as TotalPaid " & _
                    '"from AIRBRANCH.FS_Transactions " & _
                    '"where numFeeYear = '" & cboStatYear.Text & "' " & _
                    '"and Active = '1' "
            End Select

         

            SQL = "select substr(VarianceReport.strAIRSNumber, 5) as strAIRSNumber, strFacilityname, " & _
            "numfeeyear, to_number(TotalInvoiced) as TotalInvoiced, to_number(TotalReported) as TotalReported  " & _
            "from  " & _
            "(select  " & _
            "INvoiced.strAIRSnumber, INvoiced.numFeeyear, to_char(INvoiced.totalDue) as TotalInvoiced,  " & _
            "to_char(Reported.TotalDue) as TotalReported  " & _
            "from  " & _
            "(select " & _
            "strAIRSNumber, numFeeyear, sum(numAmount) as totalDue  " & _
            "from AIRBranch.FS_FeeInvoice  " & _
            "where numfeeyear = '" & cboStatYear.Text & "' and strPayType = '1'  " & _
            "and active = '1'  " & _
            "group by strAIRSNumber, numFeeyear) INvoiced,    " & _
            "(select strAIRSNumber, numFeeyear, sum(numtotalFee) As totaldue  " & _
            "from AIRBranch.FS_FeeAuditedData   " & _
            "where numFeeyear = '" & cboStatYear.Text & "'  " & _
            "and (strPaymentPlan like 'Entire Annual Year' or strPaymentPlan = '1') " & _
            "and active = '1'  " & _
            "group by strAIRSNumber, numFeeyear ) Reported   " & _
            "where Invoiced.strAIRSNumber = Reported.strAIRSNumber (+)  " & _
            "and (Invoiced.TotalDue <> Reported.TotalDue or  reported.totaldue is null)  " & _
            "union  " & _
            "select strAIRSNumber, numFeeYear, to_char(sum(numAmount)) as TotalInvoiced,  " & _
            "'' as TotalReported " & _
            "from AIRBranch.FS_FeeInvoice  " & _
            "where not exists (select * from AIRBranch.FS_FeeAuditedData  " & _
            "where AIRBranch.FS_FeeAuditedData.strAIRSnumber = AIRBranch.FS_FeeInvoice.strAIRSnumber  " & _
            "and AIRBranch.FS_FeeAuditedData.numFeeYear = AIRBranch.FS_FeeInvoice.numFeeyear  " & _
            "and numfeeyear = '" & cboStatYear.Text & "'  " & _
            "and AIRBranch.FS_FeeAuditedData.active = '1'  " & _
            "and AIRBranch.FS_FeeInvoice.active = '1')  " & _
            "and numfeeyear = '" & cboStatYear.Text & "'  " & _
            "and strPayType = '1'  " & _
            "and active = '1'  " & _
            "group by strAIRSNumber, numFeeYear, ''  " & _
            "union  " & _
            "select strairsnumber, numfeeyear, '' as TotalInvoiced, to_char(sum(numTotalFee)) as TotalReported  " & _
            "from AIRBranch.FS_FeeAuditedData  " & _
            "where active = '1' and numFeeYear = '" & cboStatYear.Text & "'  " & _
            "and strPaymentPlan like 'Entire Annual Year'  " & _
            "and not exists (select * from AIRBranch.FS_FeeInvoice  " & _
            "where AIRBranch.FS_FeeInvoice.strAIRSnumber = AIRBranch.FS_FeeAuditedData.strAIRSnumber " & _
            "and AIRBranch.FS_FeeInvoice.numFeeYear = AIRBranch.FS_FeeAuditedData.numFeeYear  " & _
            "and numfeeyear = '" & cboStatYear.Text & "' and active = '1')  " & _
            "group by strairsnumber, numfeeyear, '' ) VarianceReport, AIRBranch.APBFacilityInformation  " & _
            "where VarianceReport.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber   " & _
            "order by strairsnumber "
            ds = New DataSet

            da = New OracleDataAdapter(SQL, CurrentConnection)
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

            'dgvDepositsAndPayments.Columns("strClass").HeaderText = "Classification"
            'dgvDepositsAndPayments.Columns("strClass").DisplayIndex = 5

            'dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            'dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 6
            'dgvDepositsAndPayments.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            'dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
            'dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 7
            'dgvDepositsAndPayments.Columns("numSMFee").DefaultCellStyle.Format = "c"
            'dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            'dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 8
            'dgvDepositsAndPayments.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            'dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
            'dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 9
            'dgvDepositsAndPayments.Columns("numTotalFee").DefaultCellStyle.Format = "c"

            'dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
            'dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 10
            'dgvDepositsAndPayments.Columns("numAdminFee").DefaultCellStyle.Format = "c"

        

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewInvoicedBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewInvoicedBalance.Click
        Try

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " & _
                    "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                    "strFacilityName, numFeeyear, " & _
                    "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                    "from " & _
                    "(select " & _
                    "Invoices.strairsnumber, strFacilityname, " & _
                    "Payment, AmountOwed, numFeeYear, " & _
                    "numTotalfee as FeeReported, " & _
                    "case " & _
                    "when (AmountOwed - Payment) = 0 then 0 " & _
                    "when (AmountOwed - Payment) is null then AmountOwed " & _
                    "else (AmountOwed - Payment) " & _
                    "end Balance ,  PayType  " & _
                    "from " & _
                    "(select strairsnumber, sum(numpayment)  as Payment " & _
                    "from AIRBranch.FS_Transactions " & _
                    "where numFeeyear = '" & cboStatYear.Text & "' " & _
                    "and active = '1' group by strairsnumber) Transactions, " & _
                    "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                    "numFeeYear, " & _
                    "case " & _
                    "when strPayType = '1' then 'Annual Payer' " & _
                    "Else 'Quarterly Payer' " & _
                    "end PayType " & _
                    "from AIRBranch.FS_FeeInvoice " & _
                    "where numfeeyear = '" & cboStatYear.Text & "' " & _
                    "and active = 1  group by strairsnumber, numFeeYear, case " & _
                    "when strPayType = '1' then 'Annual Payer' " & _
                    "Else 'Quarterly Payer' " & _
                    "end) Invoices, " & _
                    "(select strairsnumber,  " & _
                      "numTotalfee " & _
                      "from Airbranch.FS_FeeAuditedData " & _
                      "where numfeeyear = '" & cboStatYear.Text & "' " & _
                      "and active = '1' ) Reported, " & _
                    "AIRBranch.APBfacilityInformation  " & _
                    "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                    "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                    "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                    " order by strairsnumber) allData "

                Case "ANNUAL"
                    SQL = "select " & _
                    "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                    "strFacilityName, numFeeyear, " & _
                    "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                    "from " & _
                    "(select " & _
                    "Invoices.strairsnumber, strFacilityname, " & _
                    "Payment, AmountOwed, numFeeYear, " & _
                    "numTotalfee as FeeReported, " & _
                    "case " & _
                    "when (AmountOwed - Payment) = 0 then 0 " & _
                    "when (AmountOwed - Payment) is null then AmountOwed " & _
                    "else (AmountOwed - Payment) " & _
                    "end Balance ,  PayType  " & _
                    "from " & _
                    "(select strairsnumber, sum(numpayment)  as Payment " & _
                    "from AIRBranch.FS_Transactions " & _
                    "where numFeeyear = '" & cboStatYear.Text & "' " & _
                    "and active = '1' group by strairsnumber) Transactions, " & _
                    "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                    "numFeeYear, " & _
                    "case " & _
                    "when strPayType = '1' then 'Annual Payer' " & _
                    "Else 'Quarterly Payer' " & _
                    "end PayType " & _
                    "from AIRBranch.FS_FeeInvoice " & _
                    "where numfeeyear = '" & cboStatYear.Text & "' " & _
                    "and active = 1 " & _
                    "and strPayType = '1' " & _
                    " group by strairsnumber, numFeeYear, case " & _
                    "when strPayType = '1' then 'Annual Payer' " & _
                    "Else 'Quarterly Payer' " & _
                    "end) Invoices, " & _
                    "(select strairsnumber,  " & _
                      "numTotalfee " & _
                      "from Airbranch.FS_FeeAuditedData " & _
                      "where numfeeyear = '" & cboStatYear.Text & "' " & _
                      "and active = '1' ) Reported, " & _
                    "AIRBranch.APBfacilityInformation  " & _
                    "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                    "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                    "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                    " order by strairsnumber) allData "
                Case "ALL QUARTERS"
                    SQL = "select " & _
                           "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                           "strFacilityName, numFeeyear, " & _
                           "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                           "from " & _
                           "(select " & _
                           "Invoices.strairsnumber, strFacilityname, " & _
                           "Payment, AmountOwed, numFeeYear, " & _
                           "numTotalfee as FeeReported, " & _
                           "case " & _
                           "when (AmountOwed - Payment) = 0 then 0 " & _
                           "when (AmountOwed - Payment) is null then AmountOwed " & _
                           "else (AmountOwed - Payment) " & _
                           "end Balance ,  PayType  " & _
                           "from " & _
                           "(select strairsnumber, sum(numpayment)  as Payment " & _
                           "from AIRBranch.FS_Transactions " & _
                           "where numFeeyear = '" & cboStatYear.Text & "' " & _
                           "and active = '1' group by strairsnumber) Transactions, " & _
                           "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                           "numFeeYear, " & _
                           "case " & _
                           "when strPayType = '1' then 'Annual Payer' " & _
                           "Else 'Quarterly Payer' " & _
                           "end PayType " & _
                           "from AIRBranch.FS_FeeInvoice " & _
                           "where numfeeyear = '" & cboStatYear.Text & "' " & _
                           "and active = 1 " & _
                           "and strPayType <> '1' " & _
                           " group by strairsnumber, numFeeYear, case " & _
                           "when strPayType = '1' then 'Annual Payer' " & _
                           "Else 'Quarterly Payer' " & _
                           "end) Invoices, " & _
                           "(select strairsnumber,  " & _
                             "numTotalfee " & _
                             "from Airbranch.FS_FeeAuditedData " & _
                             "where numfeeyear = '" & cboStatYear.Text & "' " & _
                             "and active = '1' ) Reported, " & _
                           "AIRBranch.APBfacilityInformation  " & _
                           "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                           "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                           "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                           " order by strairsnumber) allData "
                Case "QUARTER ONE"
                    SQL = "select " & _
                  "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                  "strFacilityName, numFeeyear, " & _
                  "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                  "from " & _
                  "(select " & _
                  "Invoices.strairsnumber, strFacilityname, " & _
                  "Payment, AmountOwed, numFeeYear, " & _
                  "numTotalfee as FeeReported, " & _
                  "case " & _
                  "when (AmountOwed - Payment) = 0 then 0 " & _
                  "when (AmountOwed - Payment) is null then AmountOwed " & _
                  "else (AmountOwed - Payment) " & _
                  "end Balance ,  PayType  " & _
                  "from " & _
                   "(select airbranch.fs_Transactions.strairsnumber, " & _
                    "sum(numpayment) as Payment " & _
                    "from airbranch.fs_Transactions, AIRBranch.FS_FeeInvoice  " & _
                    "where airbranch.fs_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  " & _
                    "and airbranch.fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " & _
                      "and airbranch.fs_transactions.active = '1' " & _
                    "and strPaytype = '2' group by airbranch.fs_Transactions.strairsnumber " & _
                    ")Transactions, " & _
                  "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                  "numFeeYear, " & _
                  "case " & _
                  "when strPayType = '1' then 'Annual Payer' " & _
                  "Else 'Quarterly Payer' " & _
                  "end PayType " & _
                  "from AIRBranch.FS_FeeInvoice " & _
                  "where numfeeyear = '" & cboStatYear.Text & "' " & _
                  "and active = 1 " & _
                  "and strPayType = '2' " & _
                  " group by strairsnumber, numFeeYear, case " & _
                  "when strPayType = '1' then 'Annual Payer' " & _
                  "Else 'Quarterly Payer' " & _
                  "end) Invoices, " & _
                  "(select strairsnumber,  " & _
                    "numTotalfee " & _
                    "from Airbranch.FS_FeeAuditedData " & _
                    "where numfeeyear = '" & cboStatYear.Text & "' " & _
                    "and active = '1' ) Reported, " & _
                  "AIRBranch.APBfacilityInformation  " & _
                  "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                  "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                  "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                  " order by strairsnumber) allData "
                Case "QUARTER TWO"
                    SQL = "select " & _
                     "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                     "strFacilityName, numFeeyear, " & _
                     "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                     "from " & _
                     "(select " & _
                     "Invoices.strairsnumber, strFacilityname, " & _
                     "Payment, AmountOwed, numFeeYear, " & _
                     "numTotalfee as FeeReported, " & _
                     "case " & _
                     "when (AmountOwed - Payment) = 0 then 0 " & _
                     "when (AmountOwed - Payment) is null then AmountOwed " & _
                     "else (AmountOwed - Payment) " & _
                     "end Balance ,  PayType  " & _
                     "from " & _
                       "(select airbranch.fs_Transactions.strairsnumber, " & _
                    "sum(numpayment) as Payment " & _
                    "from airbranch.fs_Transactions, AIRBranch.FS_FeeInvoice  " & _
                    "where airbranch.fs_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  " & _
                    "and airbranch.fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " & _
                      "and airbranch.fs_transactions.active = '1' " & _
                    "and strPaytype = '3' group by airbranch.fs_Transactions.strairsnumber " & _
                    ")Transactions, " & _
                     "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                     "numFeeYear, " & _
                     "case " & _
                     "when strPayType = '1' then 'Annual Payer' " & _
                     "Else 'Quarterly Payer' " & _
                     "end PayType " & _
                     "from AIRBranch.FS_FeeInvoice " & _
                     "where numfeeyear = '" & cboStatYear.Text & "' " & _
                     "and active = 1 " & _
                     "and strPayType = '3' " & _
                     " group by strairsnumber, numFeeYear, case " & _
                     "when strPayType = '1' then 'Annual Payer' " & _
                     "Else 'Quarterly Payer' " & _
                     "end) Invoices, " & _
                     "(select strairsnumber,  " & _
                       "numTotalfee " & _
                       "from Airbranch.FS_FeeAuditedData " & _
                       "where numfeeyear = '" & cboStatYear.Text & "' " & _
                       "and active = '1' ) Reported, " & _
                     "AIRBranch.APBfacilityInformation  " & _
                     "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                     "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                     "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                     " order by strairsnumber) allData "
                Case "QUARTER THREE"
                    SQL = "select " & _
                         "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                         "strFacilityName, numFeeyear, " & _
                         "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                         "from " & _
                         "(select " & _
                         "Invoices.strairsnumber, strFacilityname, " & _
                         "Payment, AmountOwed, numFeeYear, " & _
                         "numTotalfee as FeeReported, " & _
                         "case " & _
                         "when (AmountOwed - Payment) = 0 then 0 " & _
                         "when (AmountOwed - Payment) is null then AmountOwed " & _
                         "else (AmountOwed - Payment) " & _
                         "end Balance ,  PayType  " & _
                         "from " & _
                         "(select airbranch.fs_Transactions.strairsnumber, " & _
                            "sum(numpayment) as Payment " & _
                            "from airbranch.fs_Transactions, AIRBranch.FS_FeeInvoice  " & _
                            "where airbranch.fs_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  " & _
                            "and airbranch.fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " & _
                              "and airbranch.fs_transactions.active = '1' " & _
                            "and strPaytype = '4' group by airbranch.fs_Transactions.strairsnumber " & _
                            ")Transactions, " & _
                         "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                         "numFeeYear, " & _
                         "case " & _
                         "when strPayType = '1' then 'Annual Payer' " & _
                         "Else 'Quarterly Payer' " & _
                         "end PayType " & _
                         "from AIRBranch.FS_FeeInvoice " & _
                         "where numfeeyear = '" & cboStatYear.Text & "' " & _
                         "and active = 1 " & _
                         "and strPayType = '4' " & _
                         " group by strairsnumber, numFeeYear, case " & _
                         "when strPayType = '1' then 'Annual Payer' " & _
                         "Else 'Quarterly Payer' " & _
                         "end) Invoices, " & _
                         "(select strairsnumber,  " & _
                           "numTotalfee " & _
                           "from Airbranch.FS_FeeAuditedData " & _
                           "where numfeeyear = '" & cboStatYear.Text & "' " & _
                           "and active = '1' ) Reported, " & _
                         "AIRBranch.APBfacilityInformation  " & _
                         "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                         "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                         "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                         " order by strairsnumber) allData "
                Case "QUARTER FOUR"
                    SQL = "select " & _
                          "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                          "strFacilityName, numFeeyear, " & _
                          "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                          "from " & _
                          "(select " & _
                          "Invoices.strairsnumber, strFacilityname, " & _
                          "Payment, AmountOwed, numFeeYear, " & _
                          "numTotalfee as FeeReported, " & _
                          "case " & _
                          "when (AmountOwed - Payment) = 0 then 0 " & _
                          "when (AmountOwed - Payment) is null then AmountOwed " & _
                          "else (AmountOwed - Payment) " & _
                          "end Balance ,  PayType  " & _
                          "from " & _
                         "(select airbranch.fs_Transactions.strairsnumber, " & _
                            "sum(numpayment) as Payment " & _
                            "from airbranch.fs_Transactions, AIRBranch.FS_FeeInvoice  " & _
                            "where airbranch.fs_Transactions.invoiceid = AIRBranch.FS_FeeInvoice.invoiceid  " & _
                            "and airbranch.fs_Transactions.numfeeyear = '" & cboStatYear.Text & "' " & _
                            "and airbranch.fs_transactions.active = '1' " & _
                            "and strPaytype = '5' group by airbranch.fs_Transactions.strairsnumber " & _
                            ")Transactions, " & _
                          "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                          "numFeeYear, " & _
                          "case " & _
                          "when strPayType = '1' then 'Annual Payer' " & _
                          "Else 'Quarterly Payer' " & _
                          "end PayType " & _
                          "from AIRBranch.FS_FeeInvoice " & _
                          "where numfeeyear = '" & cboStatYear.Text & "' " & _
                          "and active = 1 " & _
                          "and strPayType = '5' " & _
                          " group by strairsnumber, numFeeYear, case " & _
                          "when strPayType = '1' then 'Annual Payer' " & _
                          "Else 'Quarterly Payer' " & _
                          "end) Invoices, " & _
                          "(select strairsnumber,  " & _
                            "numTotalfee " & _
                            "from Airbranch.FS_FeeAuditedData " & _
                            "where numfeeyear = '" & cboStatYear.Text & "' " & _
                            "and active = '1' ) Reported, " & _
                          "AIRBranch.APBfacilityInformation  " & _
                          "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                          "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                          "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                          " order by strairsnumber) allData "
                Case Else
                    SQL = "select " & _
                         "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                         "strFacilityName, numFeeyear, " & _
                         "Payment, AmountOwed, Balance, PayType, FeeReported " & _
                         "from " & _
                         "(select " & _
                         "Invoices.strairsnumber, strFacilityname, " & _
                         "Payment, AmountOwed, numFeeYear, " & _
                         "numTotalfee as FeeReported, " & _
                         "case " & _
                         "when (AmountOwed - Payment) = 0 then 0 " & _
                         "when (AmountOwed - Payment) is null then AmountOwed " & _
                         "else (AmountOwed - Payment) " & _
                         "end Balance ,  PayType  " & _
                         "from " & _
                         "(select strairsnumber, sum(numpayment)  as Payment " & _
                         "from AIRBranch.FS_Transactions " & _
                         "where numFeeyear = '" & cboStatYear.Text & "' " & _
                         "and active = '1' group by strairsnumber) Transactions, " & _
                         "(select strairsnumber, sum(numamount)   as AmountOwed, " & _
                         "numFeeYear, " & _
                         "case " & _
                         "when strPayType = '1' then 'Annual Payer' " & _
                         "Else 'Quarterly Payer' " & _
                         "end PayType " & _
                         "from AIRBranch.FS_FeeInvoice " & _
                         "where numfeeyear = '" & cboStatYear.Text & "' " & _
                         "and active = 1  group by strairsnumber, numFeeYear, case " & _
                         "when strPayType = '1' then 'Annual Payer' " & _
                         "Else 'Quarterly Payer' " & _
                         "end) Invoices, " & _
                         "(select strairsnumber,  " & _
                           "numTotalfee " & _
                           "from Airbranch.FS_FeeAuditedData " & _
                           "where numfeeyear = '" & cboStatYear.Text & "' " & _
                           "and active = '1' ) Reported, " & _
                         "AIRBranch.APBfacilityInformation  " & _
                         "where Invoices.strAIRSNumber =   transactions.strairsnumber (+) " & _
                         "and Invoices.strairsnumber = APBfacilityInformation.strAIRSNumber " & _
                         "and Invoices.strAIRSNumber =   Reported.strairsnumber (+)  " & _
                         " order by strairsnumber) allData "
            End Select
            If chbNonZeroBalance.Checked = True Then
                SQL = SQL & " where balance <> 0   "

            End If


            ds = New DataSet
            If SQL <> "" Then
                da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class