Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports EpdIt.DBUtilities

Public Class FeesStatistics

    Private Sub PASPFeeStatistics_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            pnlDetails.Dock = DockStyle.None

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

            If AccountFormAccess(135, 1) = "1" OrElse
                AccountFormAccess(135, 2) = "1" OrElse
                AccountFormAccess(135, 3) = "1" OrElse
                AccountFormAccess(135, 4) = "1" Then
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
        Dim allFeeYears As List(Of Integer) = DAL.GetAllFeeYears()

        With cboStatYear
            .DataSource = allFeeYears
            .SelectedIndex = 0
        End With

        With cboFeeStatYear
            .DataSource = allFeeYears
            .SelectedIndex = 0
        End With

        With cbReportedYear
            .DataSource = allFeeYears
            .SelectedIndex = 0
        End With

        With cbBalanceYear
            .DataSource = allFeeYears
            .SelectedIndex = 0
        End With

        With cboStatPayType
            .DataSource = DAL.GetFeePaymentTypesAsList().AddRowToList("ALL QUARTERS").AddRowToList("ALL")
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub cboStatYear_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboStatYear.SelectedIndexChanged, cboStatPayType.SelectedIndexChanged
        LoadTotals()
    End Sub

    Private Sub LoadTotals()
        Dim SQLReported As String
        Dim SQLInvoiced As String
        Dim SQLPaid As String

        Select Case cboStatPayType.Text

            Case "ALL"
                SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_Admin.Active = '1' " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                SQLPaid = "Select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions " &
                    "where numFeeYear = @year " &
                    "and Active = '1' "

            Case "ANNUAL"
                SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Entire Annual Year' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_admin.active = '1' " &
                    "and FS_FeeInvoice.strPayType = '1'  " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '1' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "ALL QUARTERS"
                SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and FS_FeeInvoice.strPayType <> '1'  " &
                    "and numCurrentStatus <> '12' "

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' " &
                    "and (FS_FeeInvoice.strPayType = '2' " &
                    "or FS_FeeInvoice.strPayType = '3' " &
                    "or FS_FeeInvoice.strPayType = '4' " &
                    "or FS_FeeInvoice.strPayType = '5') "

            Case "QUARTER ONE"
                SQLReported = "Select sum(numtotalFee/4) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '2'  "

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '2' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "QUARTER TWO"
                SQLReported = "Select sum(numtotalFee/4) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '3'   "

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '3' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "QUARTER THREE"
                SQLReported = "Select sum(numtotalFee/4) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '4'   "

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '4' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "QUARTER FOUR"
                SQLReported = "Select sum(numtotalFee/4 ) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan = 'Four Quarterly Payments' "

                SQLInvoiced = "Select sum(numAmount) as TotalInvoiced " &
                    "from FS_FeeInvoice inner join FS_Admin  " &
                    "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_FeeInvoice.numFeeYEar = fs_Admin.numFeeYear " &
                    "where FS_FeeInvoice.numFeeYear = @year " &
                    "and FS_FeeInvoice.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeInvoice.strPayType = '5'  "

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '5' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "AMENDMENT"
                SQLReported = "Select sum(numtotalFee ) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan is null "

                SQLInvoiced = ""

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions, FS_FeeInvoice " &
                    "where FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "and FS_FeeInvoice.strPayType = '6' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "ONE-TIME"
                SQLReported = "Select sum(numtotalFee ) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strpaymentplan is null "

                SQLInvoiced = ""

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '8' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case "REFUND"
                SQLReported = "Select sum(0) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                SQLInvoiced = ""

                SQLPaid = "select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions inner join FS_FeeInvoice " &
                    "on FS_Transactions.InvoiceID = FS_FeeInvoice.invoiceID " &
                    "where FS_FeeInvoice.strPayType = '7' " &
                    "and FS_Transactions.nuMFeeYEar = @year " &
                    "and FS_Transactions.active = '1' "

            Case Else
                SQLReported = "Select sum(numtotalFee) as TotalDue " &
                    "from FS_FeeAuditedData inner join FS_Admin  " &
                    "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber " &
                    "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.Active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' "

                SQLInvoiced = ""

                SQLPaid = "Select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions " &
                    "where numFeeYear = @year " &
                    "and Active = '1' "

        End Select

        Dim p As New SqlParameter("@year", cboStatYear.Text)

        If SQLReported <> "" Then
            txtTotalPaymentDue.Text = DB.GetSingleValue(Of Decimal)(SQLReported, p).ToString("c")
        Else
            txtTotalPaymentDue.Text = "$0"
        End If

        If SQLInvoiced <> "" Then
            txtTotalPaymentInvoiced.Text = DB.GetSingleValue(Of Decimal)(SQLInvoiced, p).ToString("c")
        Else
            txtTotalPaymentInvoiced.Text = "$0"
        End If

        If SQLPaid <> "" Then
            txtTotalPaid.Text = DB.GetSingleValue(Of Decimal)(SQLPaid, p).ToString("c")
        Else
            txtTotalPaid.Text = "$0"
        End If

        txtBalance.Text = (CDec(txtTotalPaymentDue.Text) - CDec(txtTotalPaid.Text)).ToString("c")
        txtInvoicedBalance.Text = (CDec(txtTotalPaymentInvoiced.Text) - CDec(txtTotalPaid.Text)).ToString("c")
    End Sub

    Private Sub btnViewPaymentDue_Click(sender As Object, e As EventArgs) Handles btnViewPaymentDue.Click
        Dim SQL As String

        Select Case cboStatPayType.Text

            Case "ALL"
                SQL = "select  " &
                    "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee ) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation inner join FS_FeeAuditedData " &
                    "on APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "inner join FS_Admin " &
                    "on FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "where FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeAuditedData.numFeeYear = @year "

            Case "ANNUAL"
                SQL = "select  " &
                    "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation inner join FS_FeeAuditedData " &
                    "on APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "inner join FS_Admin " &
                    "on FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strPaymentPlan = 'Entire Annual Year' "

            Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                SQL = "select  " &
                    "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee)/4 as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation inner join FS_FeeAuditedData " &
                    "on APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "inner join FS_Admin " &
                    "on FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strPaymentPlan = 'Four Quarterly Payments' "

            Case "AMENDMENT", "ONE-TIME", "REFUND"
                SQL = "select  " &
                    "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee)/4 as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation inner join FS_FeeAuditedData " &
                    "on APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "inner join FS_Admin " &
                    "on FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "where FS_FeeAuditedData.numFeeYear = @year " &
                    "and FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and strPaymentPlan is null "

            Case Else
                SQL = "select  " &
                    "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, numSMFee, numNSPSFee,  " &
                    "numTotalFee, strClass, numAdminFee  " &
                    "From APBFacilityInformation inner join FS_FeeAuditedData " &
                    "on APBFacilityInformation.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber  " &
                    "inner join FS_Admin " &
                    "on FS_feeAuditedData.strAIRSnumber = FS_Admin.strAIRSNumber " &
                    "and FS_feeAuditedData.numFeeYear = FS_Admin.numFeeYear " &
                    "where FS_FeeAuditedData.active = '1' " &
                    "and FS_Admin.Active = '1' " &
                    "and numCurrentStatus <> '12' " &
                    "and FS_FeeAuditedData.numFeeYear = @year "

        End Select

        Dim p As New SqlParameter("@year", cboStatYear.Text)

        dgvDepositsAndPayments.DataSource = DB.GetDataTable(SQL, p)

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
        dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
        dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4
        dgvDepositsAndPayments.Columns("strClass").HeaderText = "Classification"
        dgvDepositsAndPayments.Columns("strClass").DisplayIndex = 5
        dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
        dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 6
        dgvDepositsAndPayments.Columns("MaintenanceFee").HeaderText = "Part 70 Maintenance Fee"
        dgvDepositsAndPayments.Columns("MaintenanceFee").DisplayIndex = 7
        dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
        dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 8
        dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
        dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 9
        dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
        dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 10
        dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
        dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 11

    End Sub

    Private Sub bntViewTotalPaid_Click(sender As Object, e As EventArgs) Handles btnViewTotalPaid.Click
        Dim SQL As String

        Select Case cboStatPayType.Text

            Case "ALL"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year "

            Case "ANNUAL"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and strPaymentPlan = 'Entire Annual Year' "

            Case "QUARTER ONE"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '2' "

            Case "QUARTER TWO"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '3' "

            Case "QUARTER THREE"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '4' "

            Case "QUARTER FOUR"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '5' "

            Case "ALL QUARTERS"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and strPaymentPlan = 'Four Quarterly Payments' "

            Case "AMENDMENT"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '6' "

            Case "ONE-TIME"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '8' "

            Case "REFUND"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year " &
                        "and numPayTypeID = '7' "

            Case Else
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, convert(decimal(9, 2), NUMPART70FEE) as numPart70Fee, MaintenanceFee, " &
                        "numSMFee, numNSPSFee, numTotalFee, strClass, " &
                        "numAdminFee, (numTotalFee) as Due " &
                        "From APBFacilityInformation inner join FS_Transactions " &
                        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
                        "left join FS_FeeAuditedData " &
                        "on FS_Transactions.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
                        "and FS_Transactions.numFeeyear = FS_FeeAuditedData.numFeeYear " &
                        "left join FS_FeeInvoice " &
                        "on FS_Transactions.invoiceid = FS_FeeInvoice.invoiceid " &
                        "inner join FSLK_PayType " &
                        "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
                        "where FS_Transactions.Active = '1' " &
                        "and FS_FeeAuditedData.Active = '1' " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Transactions.numFeeYear = @year "
        End Select

        Dim p As New SqlParameter("@year", cboStatYear.Text)

        dgvDepositsAndPayments.DataSource = DB.GetDataTable(SQL, p)

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
        dgvDepositsAndPayments.Columns("strDepositNo").HeaderText = "Deposit #"
        dgvDepositsAndPayments.Columns("strDepositNo").DisplayIndex = 5
        dgvDepositsAndPayments.Columns("datTransactionDate").HeaderText = "Pay Date"
        dgvDepositsAndPayments.Columns("datTransactionDate").DisplayIndex = 6
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
        dgvDepositsAndPayments.Columns("MaintenanceFee").HeaderText = "Part 70 Maintenance Fee"
        dgvDepositsAndPayments.Columns("MaintenanceFee").DisplayIndex = 12
        dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
        dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 13
        dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
        dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 14
        dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
        dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 15
        dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
        dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 16
        dgvDepositsAndPayments.Columns("Due").HeaderText = "Total Due"
        dgvDepositsAndPayments.Columns("Due").DisplayIndex = 17

    End Sub

    Private Sub btnViewSelectedFeeData_Click(sender As Object, e As EventArgs) Handles btnViewSelectedFeeData.Click
        If pnlDetails.Dock = DockStyle.None Then
            pnlDetails.Dock = DockStyle.Top
            LoadSelectedFeeData()
            btnViewSelectedFeeData.Text = "Hide Details ↑"
        Else
            pnlDetails.Dock = DockStyle.None
            btnViewSelectedFeeData.Text = "View Details ↓"
        End If
    End Sub

    Private Sub LoadSelectedFeeData()
        If txtSelectedAIRSNumber.Text = "" OrElse txtSelectedAIRSNumber.Text.Length <> 8 OrElse txtSelectedYear.Text = "" Then
            Return
        End If

        Dim SQL As String = "SELECT fa.STRAIRSNUMBER, fa.NUMFEEYEAR, fa.INTSUBMITTAL, fa.DATSUBMITTAL, fa.STRCOMMENT, st.STRIAIPDESC " &
                "FROM FS_Admin AS fa " &
                "INNER JOIN fslk_admin_status AS st ON fa.NUMCURRENTSTATUS = st.ID " &
                "WHERE fa.STRAIRSNUMBER = @airs AND fa.NUMFEEYEAR = @year "
        Dim p As SqlParameter() = {
                New SqlParameter("@airs", "0413" & txtSelectedAIRSNumber.Text),
                New SqlParameter("@year", txtSelectedYear.Text)
            }

        Dim dr As DataRow = DB.GetDataRow(SQL, p)
        If dr IsNot Nothing Then
            If IsDBNull(dr.Item("intSubmittal")) Then
                txtOnlineSubmittalStatus.Text = ""
            Else
                If dr.Item("IntSubmittal").ToString = "0" Then
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
            txtSubmittalComments.Text = GetNullableString(dr.Item("strComment"))
            txtIAIPStatus.Text = GetNullableString(dr.Item("strIAIPDesc"))
        End If

        SQL = "Select " &
            "intVOCTons, intPMTons, " &
            "intSO2Tons, intNOxTons, " &
            "numPart70Fee, MaintenanceFee, numSMFee, " &
            "numNSPSFee, numTotalFee, " &
            "strNSPSExempt, " &
            "strOperate, numFeeRate, " &
            "strNSPSExemptReason, strPart70, " &
            "strSyntheticMinor, numCalculatedFee, " &
            "strClass, strNSPS, " &
            "datshutDown,  " &
            "numAdminFee, " &
            "strPaymentPlan " &
            "from FS_FeeAuditedData " &
            "where strAIRSNumber = @airs " &
            "and numFeeYear = @year "

        dr = DB.GetDataRow(SQL, p)
        If dr IsNot Nothing Then

            txtVOCTons.Text = GetNullableString(dr.Item("intVOCTons"))
            txtPMTons.Text = GetNullableString(dr.Item("intPMTons"))
            txtSO2Tons.Text = GetNullableString(dr.Item("intSO2Tons"))
            txtNOxTons.Text = GetNullableString(dr.Item("intNOxTons"))
            If IsDBNull(dr.Item("numPart70Fee")) Then
                txtPart70Fee.Clear()
            Else
                txtPart70Fee.Text = Format(dr.Item("numPart70Fee"), "c")
            End If
            If IsDBNull(dr.Item("MaintenanceFee")) Then
                txtPart70MaintenanceFee.Clear()
            Else
                txtPart70MaintenanceFee.Text = Format(dr.Item("MaintenanceFee"), "c")
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
            If IsDBNull(dr.Item("strNSPSExempt")) Then
                txtNSPSExempt.Clear()
                txtNSPSExemptReason.Clear()
            Else
                txtNSPSExempt.Text = dr.Item("strNSPSExempt").ToString
                If dr.Item("strNSPSexempt").ToString = "1" Then
                    txtNSPSExempt.Text = "YES"
                Else
                    txtNSPSExempt.Clear()
                End If
            End If
            If txtNSPSExempt.Text = "YES" Then
                If IsDBNull(dr.Item("strNSPSExemptReason")) Then
                    txtNSPSExemptReason.Clear()
                Else
                    txtNSPSExemptReason.Text = dr.Item("strNSPSExemptReason").ToString
                End If
            Else
                txtNSPSExemptReason.Clear()
            End If
            If IsDBNull(dr.Item("strOperate")) Then
                txtOperate.Clear()
            Else
                txtOperate.Text = dr.Item("strOperate").ToString
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
                txtPart70.Text = dr.Item("strPart70").ToString
                If txtPart70.Text = "1" Then
                    txtPart70.Text = "YES"
                Else
                    txtPart70.Text = "NO"
                End If
            End If
            If IsDBNull(dr.Item("strSyntheticMinor")) Then
                txtSyntheticMinor.Clear()
            Else
                txtSyntheticMinor.Text = dr.Item("strSyntheticMinor").ToString
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
            txtClass.Text = GetNullableString(dr.Item("strClass"))
            If IsDBNull(dr.Item("strNSPS")) Then
                txtNSPS.Clear()
            Else
                txtNSPS.Text = dr.Item("strNSPS").ToString
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
            If IsDBNull(dr.Item("numTotalFee")) Then
                txtAllFees.Text = "ERROR"
            Else
                txtAllFees.Text = Format(dr.Item("numTotalFee"), "c")
            End If
            txtPaymentType.Text = GetNullableString(dr.Item("strPaymentPlan"))
            If IsDBNull(dr.Item("datshutDown")) Then
                txtShutDown.Clear()
            Else
                txtShutDown.Text = Format(dr.Item("datShutDown"), "dd-MMM-yyyy")
            End If
        End If

        If txtNSPSExempt.Text = "YES" Then
            Dim temp As String = txtNSPSExemptReason.Text

            If txtNSPSExemptReason.Text.Contains(",") Then
                Dim sql2 As New StringBuilder()

                Do While txtNSPSExemptReason.Text <> ""
                    If txtNSPSExemptReason.Text.Contains(",") Then
                        temp = Mid(txtNSPSExemptReason.Text, 1, InStr(txtNSPSExemptReason.Text, ",", CompareMethod.Text) - 1)
                        txtNSPSExemptReason.Text = Mid(txtNSPSExemptReason.Text, InStr(txtNSPSExemptReason.Text, (temp & ","), CompareMethod.Text) + (temp.Length) + 1)
                    Else
                        temp = txtNSPSExemptReason.Text
                        txtNSPSExemptReason.Text = Replace(txtNSPSExemptReason.Text, temp, "")
                    End If

                    sql2.Append(" or nspsreasoncode = '" & temp & "' ")
                Loop

                SQL = "Select description " &
                    "from FSLK_NSPSReason " &
                    "where nspsreasoncode = '0' " & sql2.ToString
            Else
                SQL = "Select description " &
                    "from FSLK_NSPSReason " &
                    "where nspsreasoncode = '" & temp & "' "
            End If

            txtNSPSExemptReason.Clear()

            Dim dt As DataTable = DB.GetDataTable(SQL)
            For Each dr2 As DataRow In dt.Rows
                If Not IsDBNull(dr2.Item("description")) Then
                    txtNSPSExemptReason.Text = "- " & txtNSPSExemptReason.Text & dr2.Item("description").ToString & vbCrLf
                End If
            Next
        Else
            txtNSPSExemptReason.Clear()
        End If

        SQL = "select " &
            "TransactionID, FS_feeInvoice.InvoiceID, " &
            "numPayment, datTransactionDate, " &
            "strPayTypeDesc, strDepositNo, strCreditcardno, " &
            "strCheckNo, StrBatchNo, numAmount,  " &
            "FS_Transactions.strComment " &
            "from FS_FeeInvoice left join FS_Transactions " &
            "on FS_feeInvoice.strAIrsnumber = FS_Transactions.strAIRSNumber " &
            "and FS_feeInvoice.nuMFeeyear = FS_Transactions.nuMFeeYear " &
            "and FS_feeInvoice.InvoiceID = FS_Transactions.InvoiceID " &
            "inner join FSLK_PayType " &
            "on fs_feeinvoice.strPaytype = FSLK_PayType.numPayTypeID " &
            "where FS_Transactions.active = '1' " &
            "and FS_FeeInvoice.active = '1' " &
            "and FS_feeInvoice.numfeeyear = @year " &
            "and FS_feeInvoice.strAIRSNumber = @airs "

        dgvStats.DataSource = DB.GetDataTable(SQL, p)

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
    End Sub

    Private Sub dgvDepositsAndPayments_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDepositsAndPayments.SelectionChanged
        If dgvDepositsAndPayments.SelectedRows.Count = 1 Then
            txtSelectedAIRSNumber.Text = dgvDepositsAndPayments.CurrentRow.Cells(0).Value.ToString
            txtSelectedFacilityName.Text = dgvDepositsAndPayments.CurrentRow.Cells(1).Value.ToString
            txtSelectedYear.Text = cboStatYear.Text
            If pnlDetails.Dock <> DockStyle.None Then
                LoadSelectedFeeData()
            End If
        End If
    End Sub

    Private Sub chbDepositDateSearch_CheckedChanged(sender As Object, e As EventArgs) Handles chbDepositDateSearch.CheckedChanged
        If chbDepositDateSearch.Checked Then
            dtpStartDepositDate.Enabled = True
            dtpEndDepositDate.Enabled = True
            btnRunDepositReport.Enabled = True
            cboStatYear.Enabled = False
            cboStatPayType.Enabled = False
            btnViewPaymentDue.Enabled = False
            btnViewTotalPaid.Enabled = False
            chbNonZeroBalance.Enabled = False
            btnInvoicedPaymentDue.Enabled = False
            btnInvoiceReportVariance.Enabled = False
            btnViewInvoicedBalance.Enabled = False

            txtTotalPaymentDue.Enabled = False
            txtTotalPaid.Enabled = False
            txtTotalPaymentInvoiced.Enabled = False
            txtBalance.Enabled = False
            txtInvoicedBalance.Enabled = False
        Else
            dtpStartDepositDate.Enabled = False
            dtpEndDepositDate.Enabled = False
            btnRunDepositReport.Enabled = False
            cboStatYear.Enabled = True
            cboStatPayType.Enabled = True
            btnViewPaymentDue.Enabled = True
            btnViewTotalPaid.Enabled = True
            chbNonZeroBalance.Enabled = True
            btnInvoicedPaymentDue.Enabled = True
            btnInvoiceReportVariance.Enabled = True
            btnViewInvoicedBalance.Enabled = True

            txtTotalPaymentDue.Enabled = True
            txtTotalPaid.Enabled = True
            txtTotalPaymentInvoiced.Enabled = True
            txtBalance.Enabled = True
            txtInvoicedBalance.Enabled = True
        End If
    End Sub

    Private Sub btnRunDepositReport_Click(sender As Object, e As EventArgs) Handles btnRunDepositReport.Click
        Dim query As String = "SELECT SUBSTRing(fi.STRAIRSNUMBER, 5,8) AS AIRSNUMBER, " &
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

        dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
        dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
        dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
        dgvDepositsAndPayments.Columns("transactionTypeCode").HeaderText = "Pay Type"
        dgvDepositsAndPayments.Columns("transactionTypeCode").DisplayIndex = 2
        dgvDepositsAndPayments.Columns("PaidAmount").HeaderText = "Amount Paid"
        dgvDepositsAndPayments.Columns("PaidAmount").DisplayIndex = 3
        dgvDepositsAndPayments.Columns("strDepositNo").HeaderText = "Deposit #"
        dgvDepositsAndPayments.Columns("strDepositNo").DisplayIndex = 5
        dgvDepositsAndPayments.Columns("strBatchNo").HeaderText = "Batch #"
        dgvDepositsAndPayments.Columns("strBatchNo").DisplayIndex = 6
        dgvDepositsAndPayments.Columns("datTransactionDate").HeaderText = "Pay Date"
        dgvDepositsAndPayments.Columns("datTransactionDate").DisplayIndex = 7
        dgvDepositsAndPayments.Columns("strCheckNo").HeaderText = "Check #"
        dgvDepositsAndPayments.Columns("strCheckNo").DisplayIndex = 8
        dgvDepositsAndPayments.Columns("InvoiceID").HeaderText = "Invoice #"
        dgvDepositsAndPayments.Columns("InvoiceID").DisplayIndex = 9
        dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
        dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4

    End Sub

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

    Private Sub btnViewFacilitySpecificData_Click(sender As Object, e As EventArgs) Handles btnViewFacilitySpecificData.Click
        GridFeesReports.Visible = False
        CRFeesReports.Visible = True

        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New FacilityFee10

            Dim SQL As String = "Select * from VW_Facility_Fee " &
            "where strAIRSNumber = @airs "

            Dim p As New SqlParameter("@airs", "0413" & cboAirsNo.SelectedValue.ToString)

            rpt.SetDataSource(DB.GetDataTable(SQL, p))

            Dim crParameterDiscreteValue As New ParameterDiscreteValue With {
                .Value = "0413" & cboAirsNo.SelectedValue.ToString
            }
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions = rpt.DataDefinition.ParameterFields
            Dim crParameterFieldDefinition As ParameterFieldDefinition = crParameterFieldDefinitions.Item("AirsNo")
            Dim crParameterValues As ParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            SetUpCrystalReportViewer(rpt, CRFeesReports)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClassification_Click(sender As Object, e As EventArgs) Handles btnClassification.Click
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor

        Dim query As String = "select * from VW_FACILITY_CLASS_COUNTS order by [Fee Year] desc"

        GridFeesReports.DataSource = DB.GetDataTable(query)
        Cursor = Cursors.Default
    End Sub

    Private Sub btnRunBalanceReport_Click(sender As Object, e As EventArgs) Handles btnRunBalanceReport.Click
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor
        Dim selectedYear As Integer = CInt(cbBalanceYear.Text)

        Dim query As String = "SELECT iaip_facility.FormatAirsNumber(d.STRAIRSNUMBER) as [AIRS Number],
                    i.STRFACILITYNAME        as [Facility Name],
                    d.TOTALDUE               as [Fees Due],
                    d.TOTALPAID              as [Fees Paid],
                    d.TOTALDUE - d.TOTALPAID as [Balance]
            FROM APBFACILITYINFORMATION i
                inner join FEEDETAILS d
                on i.STRAIRSNUMBER = d.STRAIRSNUMBER
            where d.intyear = @year
            order by i.STRAIRSNUMBER"

        Dim p As New SqlParameter("@year", selectedYear.ToString)

        GridFeesReports.DataSource = DB.GetDataTable(query, p)
        Cursor = Cursors.Default
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.Default

        Dim query As String = "SELECT paid.Year,
                   paid.[Total Paid],
                   due.[Total Due],
                   due.[Total Due] - paid.[Total Paid] as Balance
            FROM (SELECT sum(TOTALPAID) AS [Total Paid],
                         INTYEAR        AS Year
                  FROM dbo.FEESPAID
                  GROUP BY INTYEAR) AS paid
                LEFT JOIN (SELECT sum(TOTALDUE) AS [Total Due],
                                  INTYEAR       AS Year
                           FROM dbo.FEESDUE
                           GROUP BY INTYEAR) AS due
                ON paid.Year = due.Year
            ORDER BY paid.Year desc"

        GridFeesReports.DataSource = DB.GetDataTable(query)
        Cursor = Cursors.Default
    End Sub

    Private Sub btnFeeByYear_Click(sender As Object, e As EventArgs) Handles btnFeeByYear.Click
        GridFeesReports.Visible = False
        CRFeesReports.Visible = True

        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New feeByYear10
            Dim query As String = "select iaip_facility.FormatAirsNumber(STRAIRSNUMBER) as STRAIRSNUMBER, TOTALDUE, INTYEAR from dbo.FeesDue"

            rpt.SetDataSource(DB.GetDataTable(query))

            SetUpCrystalReportViewer(rpt, CRFeesReports)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnViewDepositsReportByDate_Click(sender As Object, e As EventArgs) Handles btnViewDepositsReportByDate.Click
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor

        Dim query As String = "SELECT STRBATCHNO         as [Batch Number],
                   iaip_facility.FormatAirsNumber(STRAIRSNUMBER)
                                      as [AIRS Number],
                   NUMPAYMENT         as [Payment Amount],
                   STRCHECKNO         as [Check Number],
                   DATTRANSACTIONDATE as [Payment Date],
                   IIF(TRANSACTIONTYPECODE = 1, 'Deposit', 'Refund')
                                      as [Transaction Type],
                   NUMFEEYEAR         as [Fee Year],
                   TRANSACTIONID      as [Transaction ID]
            FROM FS_TRANSACTIONS
            WHERE ACTIVE = '1'
              AND DATTRANSACTIONDATE BETWEEN @StartDate AND @EndDate
            ORDER BY [Batch Number], NUMFEEYEAR DESC, [AIRS Number], TRANSACTIONID"

        Dim parameters As SqlParameter() = {
            New SqlParameter("@StartDate", dtpDepositReportStartDate.Value),
            New SqlParameter("@EndDate", dtpDepositReportEndDate.Value)
        }

        GridFeesReports.DataSource = DB.GetDataTable(query, parameters)
        Cursor = Cursors.Default
    End Sub

    Private Sub btnViewFacilityDepositsReport_Click(sender As Object, e As EventArgs) Handles btnViewFacilityDepositsReport.Click
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False

        If cboAirs.Text <> "" Then
            Cursor = Cursors.WaitCursor

            Dim query As String = "SELECT STRBATCHNO         as [Batch Number],
                       iaip_facility.FormatAirsNumber(STRAIRSNUMBER)
                                          as [AIRS Number],
                       NUMPAYMENT         as [Payment Amount],
                       STRCHECKNO         as [Check Number],
                       DATTRANSACTIONDATE as [Payment Date],
                       IIF(TRANSACTIONTYPECODE = 1, 'Deposit', 'Refund')
                                          as [Transaction Type],
                       NUMFEEYEAR         as [Fee Year],
                       TRANSACTIONID      as [Transaction ID]
                FROM FS_TRANSACTIONS
                WHERE ACTIVE = '1'
                  AND STRAIRSNUMBER = @airs
                ORDER BY [Batch Number], NUMFEEYEAR DESC, TRANSACTIONID"
            Dim parameter As New SqlParameter("@airs", "0413" & cboAirs.Text)

            GridFeesReports.DataSource = DB.GetDataTable(query, parameter)
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnClassChange_Click(sender As Object, e As EventArgs) Handles btnClassChange.Click
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor

        Dim query As String = "SELECT f.NUMFEEYEAR AS [Fee Year],
                       iaip_facility.FormatAirsNumber(f.STRAIRSNUMBER)
                                    as [AIRS Number],
                       h.STRCLASS   as [APB Class],
                       f.STRCLASS   AS [Fee Form Class]
                FROM dbo.FS_FEEAUDITEDDATA f
                    INNER JOIN dbo.APBHEADERDATA h
                    ON h.STRAIRSNUMBER = f.STRAIRSNUMBER
                        AND h.STRCLASS <> f.STRCLASS
                WHERE f.NUMFEEYEAR >= (year(getdate()) - 5)
                order by [Fee Year] desc, [AIRS Number]"

        GridFeesReports.DataSource = DB.GetDataTable(query)
        Cursor = Cursors.Default
    End Sub

    Private Sub lblNSPS1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS1.LinkClicked
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor

        Dim query As String = "select [Fee Year],
                   [AIRS Number],
                   [Subject to NSPS],
                   [NSPS Exempt],
                   [NSPS Exempt Reasons]
            from VW_NSPS_STATUS
            where [Subject to NSPS] = 'YES'
              and [NSPS Exempt] = 'YES'
            order by [Fee Year] desc, [AIRS Number]"

        GridFeesReports.DataSource = DB.GetDataTable(query)
        Cursor = Cursors.Default
    End Sub

    Private Sub lblNSPS2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS2.LinkClicked
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor

        Dim query As String = "select [Fee Year],
                   [AIRS Number],
                   [Subject to NSPS],
                   [Indicated as NSPS]
            from VW_NSPS_STATUS
            where [Subject to NSPS] = 'NO'
              and [Indicated as NSPS] = 'YES'
            order by [Fee Year] desc, [AIRS Number]"

        GridFeesReports.DataSource = DB.GetDataTable(query)
        Cursor = Cursors.Default
    End Sub

    Private Sub lblNSPS3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS3.LinkClicked
        GridFeesReports.Visible = True
        CRFeesReports.Visible = False
        Cursor = Cursors.WaitCursor

        Dim query As String = "select [Fee Year],
                   [AIRS Number],
                   [Subject to NSPS],
                   Operated
            from VW_NSPS_STATUS
            where [Subject to NSPS] = 'YES'
              and Operated = 'NO'
            order by [Fee Year] desc, [AIRS Number]"

        GridFeesReports.DataSource = DB.GetDataTable(query)
        Cursor = Cursors.Default
    End Sub

    Private Sub btnNoOperate_Click(sender As Object, e As EventArgs) Handles btnNoOperate.Click
        GridFeesReports.Visible = False
        CRFeesReports.Visible = True

        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New NoOperate10
            Dim SQL As String = "SELECT iaip_facility.FormatAirsNumber(STRAIRSNUMBER) as STRAIRSNUMBER,
                'NO' AS STROPERATE, DATSHUTDOWN AS SHUTDATE, NUMFEEYEAR  AS INTYEAR
                FROM FS_FEEAUDITEDDATA
                WHERE (STROPERATE IS NULL OR STROPERATE = '0')
                  and NUMFEEYEAR >= (year(getdate()) - 5)
                order by STRAIRSNUMBER"

            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cboFeeStatYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFeeStatYear.SelectedIndexChanged
        ViewFeeStats()
    End Sub

    Private Sub ViewFeeStats()
        Cursor = Cursors.WaitCursor

        Dim SQL As String = "SELECT (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND Active = '1') AS FeeUniverse, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND (strEnrolled = '0' OR strEnrolled IS NULL) AND Active = '1') AS UnEnrolled, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numCurrentStatus = '12' AND strEnrolled = '1' AND Active = '1') AS CeaseCollections, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numCurrentStatus <> '12' AND strEnrolled = '1' AND Active = '1') AS Enrolled, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numCurrentStatus <> '12' AND strEnrolled = '1' AND strInitialMailout = '1' AND Active = '1') AS MailOUt, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numCurrentStatus <> '12' AND strEnrolled = '1' AND strInitialMailout = '0' AND Active = '1') AS AddOnMailOut, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numcurrentstatus < '5' AND strEnrolled = '1' AND Active = '1') AS NotReported, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numcurrentstatus > '4' AND numCurrentStatus < '8' AND strEnrolled = '1' AND Active = '1') AS InProgress, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numcurrentstatus > '7' AND strEnrolled = '1' AND Active = '1' AND NOT EXISTS (SELECT * FROM fs_feeAudit " &
            "WHERE fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber AND fs_admin.numfeeyear = fs_feeAudit.numfeeyear AND fs_feeAudit.numfeeyear = @year AND fs_feeAudit.strendcollections = 'True') ) AS Finalized, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numcurrentstatus > '4' AND numcurrentstatus < '12' AND datSubmittal <= (SELECT datFeeDueDate FROM FS_FeeRate " &
            "WHERE numFeeyear = @year) AND Intsubmittal = '1' AND strEnrolled = '1' AND Active = '1') AS OnTime, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numcurrentstatus > '4' AND numcurrentstatus < '12' AND datSubmittal > (SELECT datFeeDueDate FROM FS_FeeRate " &
            "WHERE numFeeyear = @year) AND datSubmittal <= (SELECT datAdminApplicable FROM FS_FeeRate " &
            "WHERE numFeeyear = @year) AND Intsubmittal = '1' AND strEnrolled = '1' AND Active = '1') AS LateNoFees, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numFeeyear = @year AND numcurrentstatus > '4' AND numcurrentstatus < '12' AND datSubmittal > (SELECT datAdminApplicable FROM FS_FeeRate " &
            "WHERE numFeeyear = @year) AND Intsubmittal = '1' AND strEnrolled = '1' AND Active = '1') AS LateWithFees, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND active = '1' AND numcurrentstatus <= '8') AS NotPaid, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND active = '1' AND (numcurrentstatus = '9' OR numcurrentstatus = '11') ) AS OutOfBalance, (SELECT COUNT(*) FROM (SELECT numTotalFee - SUM(numAmount) AS TotalPaid FROM FS_Admin " &
            "INNER JOIN FS_FeeAuditedData ON FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber AND FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeyear " &
            "INNER JOIN FS_FeeInvoice ON FS_Admin.strAIRSNumber = FS_FeeInvoice.strAIRSNumber AND FS_Admin.numFeeYear = FS_FeeInvoice.numFeeyear " &
            "WHERE FS_Admin.numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND FS_Admin.active = '1' AND numcurrentstatus = '9' " &
            "GROUP BY numtotalfee) AS t " &
            "WHERE totalpaid > 0) AS UnderPaid, (SELECT COUNT(*) FROM FS_Admin " &
            "INNER JOIN FS_FeeAuditedData ON FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSnumber AND FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeyear " &
            "INNER JOIN FS_FeeInvoice ON FS_Admin.strAIRSNumber = FS_FeeInvoice.strAIRSNumber AND FS_Admin.numFeeYear = FS_FeeInvoice.numFeeyear " &
            "WHERE FS_Admin.numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND FS_Admin.active = '1' AND (numcurrentstatus = '9' OR numcurrentstatus = '11') ) AS OverPaid, (SELECT COUNT(*) FROM FS_Admin " &
            "INNER JOIN fs_feeAuditedData ON fs_admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber AND fs_admin.nuMFeeYear = FS_FeeAuditedData.nuMFeeYear " &
            "WHERE FS_Admin.numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND FS_Admin.active = '1' AND numcurrentstatus = '9' AND (strPaymentPlan = 'Entire Annual Year' OR strPaymentPlan IS NULL) ) AS OutOfBalanceAnnual, (SELECT COUNT(*) FROM FS_Admin " &
            "INNER JOIN fs_feeAuditedData ON fs_admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber AND fs_admin.nuMFeeYear = FS_FeeAuditedData.nuMFeeYear " &
            "WHERE FS_Admin.numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND FS_Admin.active = '1' AND numcurrentstatus = '9' AND strPaymentPlan = 'Four Quarterly Payments') AS OutOfBalanceQuarterly, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND active = '1' AND numcurrentstatus = '10') AS PaidInFull, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND active = '1' AND numcurrentstatus = '10' AND intSubmittal = '1') AS FinalPaid, (SELECT COUNT(*) FROM FS_Admin " &
            "WHERE numfeeyear = @year AND (strEnrolled = '1' OR strEnrolled IS NULL) AND active = '1' AND numcurrentstatus = '10' AND (intSubmittal = '0' OR intsubmittal IS NULL) ) AS NotFinalPaid "

        Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

        Dim dr As DataRow = DB.GetDataRow(SQL, p)
        If dr IsNot Nothing Then
            txtFSFeeUniverse.Text = GetNullableString(dr.Item("FeeUniverse"))
            txtFSUnEnrolled.Text = GetNullableString(dr.Item("Unenrolled"))
            txtFSCeaseCollection.Text = GetNullableString(dr.Item("CeaseCollections"))
            txtFSEnrolled.Text = GetNullableString(dr.Item("Enrolled"))
            txtFSMailout.Text = GetNullableString(dr.Item("MailOut"))
            txtFSAdditions.Text = GetNullableString(dr.Item("AddOnMailOut"))
            txtFSNotReported.Text = GetNullableString(dr.Item("NotReported"))
            txtFSInProgress.Text = GetNullableString(dr.Item("InProgress"))
            txtFSFinalized.Text = GetNullableString(dr.Item("Finalized"))
            txtFSOnTimeResponse.Text = GetNullableString(dr.Item("OnTime"))
            txtFSLateResponse.Text = GetNullableString(dr.Item("LateNoFees"))
            txtFSLateFee.Text = GetNullableString(dr.Item("LateWithFees"))
            txtFSNotPaid.Text = GetNullableString(dr.Item("NotPaid"))
            txtFSOutOfBalance.Text = GetNullableString(dr.Item("OutOfBalance"))
            txtFSPartial.Text = GetNullableString(dr.Item("UnderPaid"))
            txtFSOverPaid.Text = GetNullableString(dr.Item("OverPaid"))
            txtFSAnnual.Text = GetNullableString(dr.Item("OutOfBalanceAnnual"))
            txtFSQuarterly.Text = GetNullableString(dr.Item("OutofBalanceQuarterly"))
            txtFSPaidInFull.Text = GetNullableString(dr.Item("PaidInFull"))
            txtFSPaidFinalized.Text = GetNullableString(dr.Item("FinalPaid"))
            txtFSPaidNotFinalized.Text = GetNullableString(dr.Item("NotFinalPaid"))
        End If

        Cursor = Cursors.Default
    End Sub

    Const StatisticsSummarySQL As String = "select substring(a.STRAIRSNUMBER, 5, 8) as [Airs No.],
               f.STRFACILITYNAME                as [Facility Name],
               s.STRIAIPDESC                    as [Fee Status],
               a.STRCOMMENT                     as [Comment],
               IIF(p.CommunicationPreference is null, 'Not set',
                   p.CommunicationPreference)   as [Communication Preference]
        from dbo.FS_ADMIN a
            inner join dbo.APBFACILITYINFORMATION f
            on a.STRAIRSNUMBER = f.STRAIRSNUMBER
            left join dbo.FSLK_ADMIN_STATUS s
            on a.NUMCURRENTSTATUS = s.ID
            left join dbo.Geco_CommunicationPreference p
            on a.STRAIRSNUMBER = p.FacilityId
                and p.Category = 'Fees'
            left join (select distinct STRAIRSNUMBER,
                                       NUMFEEYEAR
                       from FS_FEEAUDIT
                       where STRENDCOLLECTIONS = 'True') u
            on a.STRAIRSNUMBER = u.STRAIRSNUMBER
                and a.NUMFEEYEAR = u.NUMFEEYEAR
        where a.NUMFEEYEAR = @year
          and a.ACTIVE = '1' 
    "

    Private Sub ShowFeeStatisticsSummaryData(filter As String)
        If String.IsNullOrEmpty(cboFeeStatYear.Text) Then Return
        Dim year As Short
        If Not Short.TryParse(cboFeeStatYear.Text, year) Then Return
        Cursor = Cursors.WaitCursor
        Dim query As String = StatisticsSummarySQL & filter & " order by 1"
        Dim p As New SqlParameter("@year", year)
        dgvFeeStats.DataSource = DB.GetDataTable(query, p)
        Cursor = Cursors.Default
    End Sub

    Private Sub llbFSSummaryFeeUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFeeUniverse.LinkClicked
        ShowFeeStatisticsSummaryData("")
    End Sub

    Private Sub llbFSSummaryUnEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryUnEnrolled.LinkClicked
        ShowFeeStatisticsSummaryData("and (a.STRENROLLED = '0' or a.STRENROLLED is null)")
    End Sub

    Private Sub llbFSSummaryCeaseCollection_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryCeaseCollection.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS = 12 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbFSSummaryEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryEnrolled.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS <> 12 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbFSSummaryMailOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryMailOut.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS <> 12 and a.STRENROLLED = '1' and a.STRINITIALMAILOUT = '1'")
    End Sub

    Private Sub llbFSSummaryAdditions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryAdditions.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS <> 12 and a.STRENROLLED = '1' and a.STRINITIALMAILOUT = '0'")
    End Sub

    Private Sub llbFSSummaryNotReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryNotReported.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS < 5 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbFSSummaryInProgress_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryInProgress.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS between 5 and 7 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbFSSummaryFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFinalized.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS > 7 and a.STRENROLLED = '1' and u.NUMFEEYEAR is null")
    End Sub

    Private Sub llbFSSummaryOnTime_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOnTime.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS between 5 and 11 and a.INTSUBMITTAL = '1' and a.STRENROLLED = '1'
            and a.DATSUBMITTAL <= (select DATFEEDUEDATE from FS_FEERATE where NUMFEEYEAR = @year)")
    End Sub

    Private Sub llbFSSummaryLateResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryLateResponse.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS between 5 and 11 and a.INTSUBMITTAL = '1' and a.STRENROLLED = '1'
            and a.DATSUBMITTAL > (select DATFEEDUEDATE from FS_FEERATE where NUMFEEYEAR = @year)
            and a.DATSUBMITTAL <= (select DATADMINAPPLICABLE from FS_FEERATE where NUMFEEYEAR = @year)")
    End Sub

    Private Sub llbFSSummaryLateWithFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryLateWithFee.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS between 5 and 11 and a.INTSUBMITTAL = '1' and a.STRENROLLED = '1'
          and a.DATSUBMITTAL > (select DATADMINAPPLICABLE from FS_FEERATE where NUMFEEYEAR = @year)")
    End Sub

    Private Sub llbFSSummaryNotPaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryNotPaid.LinkClicked
        ShowFeeStatisticsSummaryData("and a.NUMCURRENTSTATUS <= 8 and (a.STRENROLLED = '1' or a.STRENROLLED is null)")
    End Sub

    Private Sub llbFSSummaryOutofBalance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryOutofBalance.LinkClicked
        ShowFeeStatisticsSummaryData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and (a.NUMCURRENTSTATUS = 9 or a.NUMCURRENTSTATUS = 11)")
    End Sub

    Private Sub llbFSSummaryPaidInFull_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidInFull.LinkClicked
        ShowFeeStatisticsSummaryData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and a.NUMCURRENTSTATUS = 10")
    End Sub

    Private Sub llbFSSummaryPaidFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidFinalized.LinkClicked
        ShowFeeStatisticsSummaryData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and a.NUMCURRENTSTATUS = 10 and a.INTSUBMITTAL = '1'")
    End Sub

    Private Sub llbFSSummaryPaidNotFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryPaidNotFinalized.LinkClicked
        ShowFeeStatisticsSummaryData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and a.NUMCURRENTSTATUS = 10 
            and (a.INTSUBMITTAL = '0' or a.INTSUBMITTAL is null)")
    End Sub

    Const StatisticDetailsSQL As String = "select substring(a.STRAIRSNUMBER, 5, 8)                      as [Airs No.],
               f.STRFACILITYNAME                                     as [Facility Name],
               s.STRIAIPDESC                                         as [Fee Status],
               a.STRCOMMENT                                          as [Comment],
               d.NUMTOTALFEE                                         as [Total Fees],
               t.[Total Paid],
               IIF(d.strOperate = '1', 'Operating', 'Not Operating') as [Operating],
               d.DATSHUTDOWN                                         as [Date Shut Down],
               m.STRCLASS                                            as [Class (mailout)],
               IIF(m.STRPART70 = '1', 'Part 70', '')                 as [Part 70 (mailout)],
               IIF(m.STRNSPS = '1', 'NSPS', '')                      as [NSPS (mailout)],
               IIF(m.NspsFeeExempt = 1, 'Exempt', '')                as [NSPS Fee Exempt (mailout)],
               d.STRCLASS                                            as [Class (submitted)],
               IIF(d.STRPART70 = '1', 'Part 70', '')                 as [Part 70 (submitted)],
               IIF(d.STRNSPS = '1', 'NSPS', '')                      as [NSPS (submitted)],
               IIF(d.STRNSPSEXEMPT = '1', 'Exempt', '')              as [NSPS Fee Exempt (submitted)],
               IIF(p.CommunicationPreference is null, 'Not set',
                   p.CommunicationPreference)                        as [Communication Preference],
               concat_ws(' ', m.STRFIRSTNAME, m.STRLASTNAME)         as [Mailout contact],
               m.STRGECOUSEREMAIL                                    as [Contact Email],
               m.STRCONTACTADDRESS1                                  as [Contact Address],
               dbo.NullIfNaOrEmpty(m.STRCONTACTADDRESS2)             as [Contact Address 2],
               m.STRCONTACTCITY                                      as [Contact City],
               m.STRCONTACTSTATE                                     as [Contact State],
               m.STRCONTACTZIPCODE                                   as [Contact ZIP],
               m.UPDATEDATETIME                                      as [Contact snapshot last updated]
        from FS_ADMIN a
            inner join APBFACILITYINFORMATION f
            on a.STRAIRSNUMBER = f.STRAIRSNUMBER
            inner join FSLK_ADMIN_STATUS s
            on a.NUMCURRENTSTATUS = s.ID
            left join dbo.Geco_CommunicationPreference p
            on a.STRAIRSNUMBER = p.FacilityId
                and p.Category = 'Fees'
            left join FS_MAILOUT m
            on a.STRAIRSNUMBER = m.STRAIRSNUMBER
                and a.NUMFEEYEAR = m.NUMFEEYEAR
            left join FS_FEEAUDITEDDATA d
            on a.STRAIRSNUMBER = d.STRAIRSNUMBER
                and a.NUMFEEYEAR = d.NUMFEEYEAR
            left join (select distinct STRAIRSNUMBER,
                                       NUMFEEYEAR
                       from FS_FEEAUDIT
                       where STRENDCOLLECTIONS = 'True') u
            on a.STRAIRSNUMBER = u.STRAIRSNUMBER
                and a.NUMFEEYEAR = u.NUMFEEYEAR
            left join (select STRAIRSNUMBER,
                              NUMFEEYEAR,
                              sum(NUMPAYMENT) as [Total Paid]
                       from FS_TRANSACTIONS t
                       group by STRAIRSNUMBER, NUMFEEYEAR) t
            on a.STRAIRSNUMBER = t.STRAIRSNUMBER
                and a.NUMFEEYEAR = t.NUMFEEYEAR
        where a.NUMFEEYEAR = @year
          and a.ACTIVE = '1'
    "

    Private Sub ShowFeeStatisticsDetailsData(filter As String)
        If String.IsNullOrEmpty(cboFeeStatYear.Text) Then Return
        Dim year As Short
        If Not Short.TryParse(cboFeeStatYear.Text, year) Then Return
        Cursor = Cursors.WaitCursor
        Dim query As String = StatisticDetailsSQL & filter & " order by 1"
        Dim p As New SqlParameter("@year", year)
        dgvFeeStats.DataSource = DB.GetDataTable(query, p)
        Cursor = Cursors.Default
    End Sub

    Private Sub llbDetailFeeUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailFeeUniverse.LinkClicked
        ShowFeeStatisticsDetailsData("")
    End Sub

    Private Sub llbDetailUnEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailUnEnrolled.LinkClicked
        ShowFeeStatisticsDetailsData("and (a.STRENROLLED = '0' or a.STRENROLLED is null)")
    End Sub

    Private Sub llbDetailCeaseCollection_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailCeaseCollection.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS = 12 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbDetailEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailEnrolled.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS <> 12 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbDetailMailout_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailMailout.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS <> 12 and a.STRENROLLED = '1' and a.STRINITIALMAILOUT = '1'")
    End Sub

    Private Sub llbDetailAdditions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailAdditions.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS <> 12 and a.STRENROLLED = '1' and a.STRINITIALMAILOUT = '0'")
    End Sub

    Private Sub llbDetailNotReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailNotReported.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS < 5 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbDetailInProgress_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailInProgress.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS between 5 and 7 and a.STRENROLLED = '1'")
    End Sub

    Private Sub llbDetailFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailFinalized.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS > 7 and a.STRENROLLED = '1' and u.NUMFEEYEAR is null")
    End Sub

    Private Sub llbFSDetailOnTime_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSDetailOnTime.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS between 5 and 11 and a.INTSUBMITTAL = '1' and a.STRENROLLED = '1'
            and a.DATSUBMITTAL <= (select DATFEEDUEDATE from FS_FEERATE where NUMFEEYEAR = @year)")
    End Sub

    Private Sub llbDetailLateResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailLateResponse.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS between 5 and 11 and a.INTSUBMITTAL = '1' and a.STRENROLLED = '1'
            and a.DATSUBMITTAL > (select DATFEEDUEDATE from FS_FEERATE where NUMFEEYEAR = @year)
            and a.DATSUBMITTAL <= (select DATADMINAPPLICABLE from FS_FEERATE where NUMFEEYEAR = @year)")
    End Sub

    Private Sub llbDetailLateWithFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailLateWithFee.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS between 5 and 11 and a.INTSUBMITTAL = '1' and a.STRENROLLED = '1'
          and a.DATSUBMITTAL > (select DATADMINAPPLICABLE from FS_FEERATE where NUMFEEYEAR = @year)")
    End Sub

    Private Sub llbDetailNotPaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailNotPaid.LinkClicked
        ShowFeeStatisticsDetailsData("and a.NUMCURRENTSTATUS <= 8 and (a.STRENROLLED = '1' or a.STRENROLLED is null)")
    End Sub

    Private Sub llbDetailOutOfBalance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailOutOfBalance.LinkClicked
        ShowFeeStatisticsDetailsData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and (a.NUMCURRENTSTATUS = 9 or a.NUMCURRENTSTATUS = 11)")
    End Sub

    Private Sub llbDetailPaidInFull_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPaidInFull.LinkClicked
        ShowFeeStatisticsDetailsData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and a.NUMCURRENTSTATUS = 10")
    End Sub

    Private Sub llbDetailPaidFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPaidFinalized.LinkClicked
        ShowFeeStatisticsDetailsData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and a.NUMCURRENTSTATUS = 10 and a.INTSUBMITTAL = '1'")
    End Sub

    Private Sub llbDetailPaidNotFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDetailPaidNotFinalized.LinkClicked
        ShowFeeStatisticsDetailsData("and (a.STRENROLLED = '1' or a.STRENROLLED is null) and a.NUMCURRENTSTATUS = 10 
            and (a.INTSUBMITTAL = '0' or a.INTSUBMITTAL is null)")
    End Sub

    Private Sub btnExportFeeStats_Click(sender As Object, e As EventArgs)
        dgvFeeStats.ExportToExcel(Me)
    End Sub

    Private Sub dgvFeeStats_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvFeeStats.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFeeStats.HitTest(e.X, e.Y)

        If dgvFeeStats.RowCount > 0 AndAlso hti.RowIndex <> -1 AndAlso
                dgvFeeStats.Columns(0).HeaderText = "Airs No." Then

            txtFeeStatAirsNumber.Text = GetNullableString(dgvFeeStats(0, hti.RowIndex).Value)
        End If
    End Sub

    Private Sub btnCheckInvoices_Click(sender As Object, e As EventArgs) Handles btnCheckInvoices.Click
        If cboFeeStatYear.Text IsNot Nothing Then
            Cursor = Cursors.WaitCursor

            Dim feeYear As Integer = CInt(cboFeeStatYear.Text)

            Dim query As String = "Update FS_FeeInvoice set " &
            "strInvoiceStatus = '1', " &
            "UpdateUser = @Username,  " &
            "updateDateTime = getdate() " &
            "where numFeeYear = @FeeYear " &
            "and numAmount = '0' " &
            "and strInvoiceStatus = '0' " &
            "and active = '1' "

            Dim feeYearParam As New SqlParameter("@FeeYear", SqlDbType.SmallInt) With {.Value = feeYear}

            Dim parameters As SqlParameter() = {
                New SqlParameter("@Username", CurrentUser.AlphaName),
                feeYearParam
            }

            If Not DB.RunCommand(query, parameters) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            DB.SPRunCommand("dbo.PD_FEE_STATUSES", feeYearParam)

            MsgBox("Fee Invoices validated.", MsgBoxStyle.Information, Me.Text)

            ViewFeeStats()

            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnInvoicedPaymentDue_Click(sender As Object, e As EventArgs) Handles btnInvoicedPaymentDue.Click
        Dim SQL As String = ""

        Select Case cboStatPayType.Text
            Case "ALL"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        "and FS_FeeInvoice.numFeeYear = @year "

            Case "ANNUAL"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.numFeeYear = @year " &
                        "and FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        " and strPayType = '1' "

            Case "ALL QUARTERS"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        "and FS_FeeInvoice.numFeeYear = @year " &
                        "and (FS_FeeInvoice.strPayType = '2' " &
                        "or FS_FeeInvoice.strPayType = '3' " &
                        "or FS_FeeInvoice.strPayType = '4' " &
                        "or FS_FeeInvoice.strPayType = '5') "

            Case "QUARTER ONE"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount * 4 as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        "and FS_FeeInvoice.numFeeYear = @year " &
                        "and FS_FeeInvoice.strPayType = '2'  "

            Case "QUARTER TWO"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount * 4 as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        "and FS_FeeInvoice.numFeeYear = @year " &
                        "and FS_FeeInvoice.strPayType = '3'  "

            Case "QUARTER THREE"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount * 4 as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        "and FS_FeeInvoice.numFeeYear = @year " &
                        "and FS_FeeInvoice.strPayType = '4'  "

            Case "QUARTER FOUR"
                SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,   " &
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
                        "numAmount * 4 as numTotalFee, strClass " &
                        "From APBFacilityInformation inner join FS_FeeInvoice " &
                        "on APBFacilityInformation.strAIRSNumber = FS_FeeInvoice.strAIRSNumber   " &
                        "inner join APBHeaderData " &
                        "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber   " &
                        "inner join FS_Admin " &
                        "on FS_FeeInvoice.strAIRSnumber = FS_Admin.strAIRSnumber " &
                        "and FS_FeeInvoice.numFeeYear = FS_Admin.numFeeYear " &
                        "where FS_FeeInvoice.Active = '1' " &
                        "and FS_Admin.Active = '1' " &
                        "and numCurrentStatus <> '12'  " &
                        "and FS_FeeInvoice.numFeeYear = @year " &
                        "and  FS_FeeInvoice.strPayType = '5'  "

            Case Else
        End Select

        If SQL <> "" Then

            Dim p As New SqlParameter("@year", cboStatYear.Text)

            dgvDepositsAndPayments.DataSource = DB.GetDataTable(SQL, p)

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
            dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("strClass").HeaderText = "Classification"
            dgvDepositsAndPayments.Columns("strClass").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
            dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 6

        End If
    End Sub

    Private Sub btnOpenFeesLog_Click(sender As Object, e As EventArgs) Handles btnOpenFeesLog.Click
        Dim parameters As New Generic.Dictionary(Of BaseForm.FormParameter, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtFeeStatAirsNumber.Text) Then
            parameters(FormParameter.AirsNumber) = txtFeeStatAirsNumber.Text
        End If
        parameters(FormParameter.FeeYear) = cboFeeStatYear.Text

        OpenSingleForm(FeesAudit, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub btnInvoiceReportVariance_Click(sender As Object, e As EventArgs) Handles btnInvoiceReportVariance.Click
        Dim SQL As String = "SELECT SUBSTRING(VarianceReport.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, af.STRFACILITYNAME, VarianceReport.NUMFEEYEAR, VarianceReport.TotalInvoiced AS TotalInvoiced, VarianceReport.TotalReported AS TotalReported " &
                "FROM ( " &
                "SELECT i.STRAIRSNUMBER, i.NUMFEEYEAR, i.totalDue AS TotalInvoiced, r.totaldue AS TotalReported " &
                "FROM (SELECT fi.STRAIRSNUMBER, fi.NUMFEEYEAR, SUM(fi.NUMAMOUNT) AS totalDue " &
                "FROM FS_FeeInvoice AS fi " &
                "WHERE fi.NUMFEEYEAR = @year AND fi.STRPAYTYPE = '1' AND fi.ACTIVE = '1' " &
                "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR) AS i " &
                "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMFEEYEAR, SUM(fa.NUMTOTALFEE) AS totaldue " &
                "FROM FS_FeeAuditedData AS fa " &
                "WHERE fa.NUMFEEYEAR = @year AND (fa.STRPAYMENTPLAN LIKE 'Entire Annual Year' OR fa.STRPAYMENTPLAN = '1') AND fa.ACTIVE = '1' " &
                "GROUP BY fa.STRAIRSNUMBER, fa.NUMFEEYEAR) AS r ON i.STRAIRSNUMBER = r.STRAIRSNUMBER " &
                "WHERE i.totalDue <> r.totaldue OR r.totaldue IS NULL " &
                "UNION " &
                "SELECT fi.STRAIRSNUMBER, fi.NUMFEEYEAR, SUM(fi.NUMAMOUNT) AS TotalInvoiced, NULL AS TotalReported " &
                "FROM FS_FeeInvoice AS fi " &
                "WHERE NOT EXISTS (SELECT * " &
                "FROM FS_FeeAuditedData AS fa " &
                "WHERE fa.STRAIRSNUMBER = fi.STRAIRSNUMBER AND fa.NUMFEEYEAR = fi.NUMFEEYEAR AND fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1' AND fi.ACTIVE = '1') AND fi.NUMFEEYEAR = @year AND fi.STRPAYTYPE = '1' AND fi.ACTIVE = '1' " &
                "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR " &
                "UNION " &
                "SELECT fa.STRAIRSNUMBER, fa.NUMFEEYEAR, NULL AS TotalInvoiced, SUM(fa.NUMTOTALFEE) AS TotalReported " &
                "FROM FS_FeeAuditedData AS fa " &
                "WHERE fa.ACTIVE = '1' AND fa.NUMFEEYEAR = @year AND fa.STRPAYMENTPLAN LIKE 'Entire Annual Year' AND NOT EXISTS (SELECT * " &
                "FROM FS_FeeInvoice AS fi " &
                "WHERE fi.STRAIRSNUMBER = fa.STRAIRSNUMBER AND fi.NUMFEEYEAR = fa.NUMFEEYEAR AND fi.NUMFEEYEAR = @year AND fi.ACTIVE = '1') " &
                "GROUP BY fa.STRAIRSNUMBER, fa.NUMFEEYEAR) AS VarianceReport " &
                "INNER JOIN APBFacilityInformation AS af ON VarianceReport.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                "ORDER BY VarianceReport.STRAIRSNUMBER "

        Dim p As New SqlParameter("@year", cboStatYear.Text)

        dgvDepositsAndPayments.DataSource = DB.GetDataTable(SQL, p)

        dgvDepositsAndPayments.Columns("strAIRSNumber").HeaderText = "AIRS Number"
        dgvDepositsAndPayments.Columns("strAIRSNumber").DisplayIndex = 0
        dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
        dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
        dgvDepositsAndPayments.Columns("numfeeyear").HeaderText = "Fee Year"
        dgvDepositsAndPayments.Columns("numfeeyear").DisplayIndex = 2
        dgvDepositsAndPayments.Columns("TotalInvoiced").HeaderText = "Invoiced Amount"
        dgvDepositsAndPayments.Columns("TotalInvoiced").DisplayIndex = 3
        dgvDepositsAndPayments.Columns("TotalReported").HeaderText = "Reported Amount"
        dgvDepositsAndPayments.Columns("TotalReported").DisplayIndex = 4

    End Sub

    Private Sub btnViewInvoicedBalance_Click(sender As Object, e As EventArgs) Handles btnViewInvoicedBalance.Click
        Dim SQL As String

        Select Case cboStatPayType.Text

            Case "ALL"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN Invoices.AmountOwed - Transactions.Payment = 0 THEN 0 WHEN Invoices.AmountOwed - Transactions.Payment IS NULL THEN Invoices.AmountOwed ELSE Invoices.AmountOwed - Transactions.Payment END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM FS_Transactions AS ft " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case "ANNUAL"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM FS_Transactions AS ft " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 AND fi.STRPAYTYPE <> '1' " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case "ALL QUARTERS"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, APBfacilityInformation.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM FS_Transactions AS ft " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 AND fi.STRPAYTYPE = '1' " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation ON Invoices.STRAIRSNUMBER = APBfacilityInformation.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case "QUARTER ONE"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM fs_Transactions AS ft " &
                        "INNER JOIN FS_FeeInvoice AS fi ON ft.INVOICEID = fi.INVOICEID " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' AND fi.STRPAYTYPE = '2' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 AND fi.STRPAYTYPE = '2' " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case "QUARTER TWO"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM fs_Transactions AS ft " &
                        "INNER JOIN FS_FeeInvoice AS fi ON ft.INVOICEID = fi.INVOICEID " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' AND fi.STRPAYTYPE = '3' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 AND fi.STRPAYTYPE = '3' " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case "QUARTER THREE"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM fs_Transactions AS ft " &
                        "INNER JOIN FS_FeeInvoice AS fi ON ft.INVOICEID = fi.INVOICEID " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' AND fi.STRPAYTYPE = '4' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 AND fi.STRPAYTYPE = '4' " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case "QUARTER FOUR"
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM fs_Transactions AS ft " &
                        "INNER JOIN FS_FeeInvoice AS fi ON ft.INVOICEID = fi.INVOICEID " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' AND fi.STRPAYTYPE = '5' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 AND fi.STRPAYTYPE = '5' " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "

            Case Else
                SQL = "SELECT SUBSTRING(allData.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, allData.STRFACILITYNAME, allData.NUMFEEYEAR, allData.Payment, allData.AmountOwed, allData.Balance, allData.PayType, allData.FeeReported " &
                        "FROM (SELECT Invoices.STRAIRSNUMBER, af.STRFACILITYNAME, Transactions.Payment, Invoices.AmountOwed, Invoices.NUMFEEYEAR, Reported.NUMTOTALFEE AS FeeReported, " &
                        "CASE WHEN (Invoices.AmountOwed - Transactions.Payment) = 0 THEN 0 WHEN (Invoices.AmountOwed - Transactions.Payment) IS NULL THEN Invoices.AmountOwed ELSE (Invoices.AmountOwed - Transactions.Payment) END AS Balance, Invoices.PayType " &
                        "FROM (SELECT ft.STRAIRSNUMBER, SUM(ft.NUMPAYMENT) AS Payment " &
                        "FROM FS_Transactions AS ft " &
                        "WHERE ft.NUMFEEYEAR = @year AND ft.ACTIVE = '1' " &
                        "GROUP BY ft.STRAIRSNUMBER) AS Transactions " &
                        "RIGHT JOIN (SELECT fi.STRAIRSNUMBER, SUM(fi.NUMAMOUNT) AS AmountOwed, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END AS PayType " &
                        "FROM FS_FeeInvoice AS fi " &
                        "WHERE fi.NUMFEEYEAR = @year AND fi.ACTIVE = 1 " &
                        "GROUP BY fi.STRAIRSNUMBER, fi.NUMFEEYEAR, " &
                        "CASE WHEN fi.STRPAYTYPE = '1' THEN 'Annual Payer' ELSE 'Quarterly Payer' END) AS Invoices ON Invoices.STRAIRSNUMBER = Transactions.STRAIRSNUMBER " &
                        "INNER JOIN APBfacilityInformation AS af ON Invoices.STRAIRSNUMBER = af.STRAIRSNUMBER " &
                        "LEFT JOIN (SELECT fa.STRAIRSNUMBER, fa.NUMTOTALFEE " &
                        "FROM FS_FeeAuditedData AS fa " &
                        "WHERE fa.NUMFEEYEAR = @year AND fa.ACTIVE = '1') AS Reported ON Invoices.STRAIRSNUMBER = Reported.STRAIRSNUMBER) AS allData "
        End Select

        If chbNonZeroBalance.Checked Then
            SQL &= " where balance <> 0 "
        End If

        SQL &= " ORDER BY strairsnumber "

        Dim p As New SqlParameter("@year", cboStatYear.Text)

        dgvDepositsAndPayments.DataSource = DB.GetDataTable(SQL, p)

        dgvDepositsAndPayments.Columns("strAIRSNUmber").HeaderText = "AIRS Number"
        dgvDepositsAndPayments.Columns("strAIRSNUmber").DisplayIndex = 0
        dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
        dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
        dgvDepositsAndPayments.Columns("PayType").HeaderText = "Payment Plan"
        dgvDepositsAndPayments.Columns("PayType").DisplayIndex = 2
        dgvDepositsAndPayments.Columns("FeeReported").HeaderText = "Amount Reported"
        dgvDepositsAndPayments.Columns("FeeReported").DisplayIndex = 3
        dgvDepositsAndPayments.Columns("AmountOwed").HeaderText = "Amount Invoiced"
        dgvDepositsAndPayments.Columns("AmountOwed").DisplayIndex = 4
        dgvDepositsAndPayments.Columns("Payment").HeaderText = "Amount Paid"
        dgvDepositsAndPayments.Columns("Payment").DisplayIndex = 5
        dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance Due"
        dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 6
        dgvDepositsAndPayments.Columns("numFeeYear").HeaderText = "Year"
        dgvDepositsAndPayments.Columns("numFeeYear").DisplayIndex = 7

    End Sub

    Private Sub btnExportedRun_Click(sender As Object, e As EventArgs) Handles btnExportedRun.Click
        If String.IsNullOrEmpty(cbReportedYear.Text) Then
            Return
        End If

        Dim query As String = "SELECT
            concat(substring(d.STRAIRSNUMBER, 5, 3), '-', right(d.STRAIRSNUMBER, 5))
                              as [AIRS Number],
            f.STRFACILITYNAME as [Facility Name],
            STRCLASS          as [Classification],
            case
            when STROPERATE = 0
                then 'No'
            when STROPERATE = 1
                then 'Yes'
            else ''
            end               as [Operated],
            INTVOCTONS        as [VOC Tons],
            INTPMTONS         as [PM Tons],
            INTSO2TONS        as [SO2 Tons],
            INTNOXTONS        as [NOx Tons],
            NUMFEERATE        as [$ Per Ton Fee Rate],
            convert(decimal(9, 2), NUMPART70FEE) as [Part 70 Fees],
            MaintenanceFee as [Part 70 Maintenance Fee],
            NUMSMFEE          as [SM Fees],
            NUMNSPSFEE        as [NSPS Fees],
            NUMADMINFEE       as [Admin Fees],
            NUMTOTALFEE       as [Total Fees]
        from FS_FEEAUDITEDDATA d
            left JOIN APBFACILITYINFORMATION f
                on f.STRAIRSNUMBER = d.STRAIRSNUMBER
        where NUMFEEYEAR = @year"

        Dim param As New SqlParameter("@year", cbReportedYear.Text)

        dgvReported.DataSource = DB.GetDataTable(query, param)
    End Sub

End Class