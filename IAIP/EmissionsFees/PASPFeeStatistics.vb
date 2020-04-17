Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class PASPFeeStatistics

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

                    SQLPaid = "Select sum(numPayment) as TotalPaid " &
                    "from FS_Transactions " &
                    "where numFeeYear = @year " &
                    "and Active = '1' "

            End Select

            Dim p As New SqlParameter("@year", cboStatYear.Text)

            If SQLReported <> "" Then
                Dim dr As DataRow = DB.GetDataRow(SQLReported, p)
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("TotalDue")) Then
                        txtTotalPaymentDue.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaymentDue.Text = CDec(dr.Item("TotalDue")).ToString("c")
                    End If
                End If
            Else
                txtTotalPaymentDue.Text = CDec("0").ToString("c")
            End If

            If SQLInvoiced <> "" Then
                Dim dr As DataRow = DB.GetDataRow(SQLInvoiced, p)
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("TotalInvoiced")) Then
                        txtTotalPaymentInvoiced.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaymentInvoiced.Text = CDec(dr.Item("TotalInvoiced")).ToString("c")
                    End If
                End If
            Else
                txtTotalPaymentInvoiced.Text = CDec("0").ToString("c")
            End If

            If SQLPaid <> "" Then
                Dim dr As DataRow = DB.GetDataRow(SQLPaid, p)
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("TotalPaid")) Then
                        txtTotalPaid.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaid.Text = CDec(dr.Item("TotalPaid")).ToString("c")
                    End If
                End If
            Else
                txtTotalPaid.Text = CDec("0").ToString("c")
            End If

            txtBalance.Text = (CDec(txtTotalPaymentDue.Text) - CDec(txtTotalPaid.Text)).ToString("c")
            txtInvoicedBalance.Text = (CDec(txtTotalPaymentInvoiced.Text) - CDec(txtTotalPaid.Text)).ToString("c")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewPaymentDue_Click(sender As Object, e As EventArgs) Handles btnViewPaymentDue.Click
        Try
            Dim SQL As String = ""

            Select Case cboStatPayType.Text

                Case "ALL"
                    SQL = "select  " &
                    "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber,  " &
                    "strFacilityName, strPaymentPlan,  " &
                    "(numTotalFee ) as Due, FS_FeeAuditedData.numFeeYear,  " &
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
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
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
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
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
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
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
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
                    "numPart70Fee, numSMFee, numNSPSFee,  " &
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

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
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

    Private Sub bntViewTotalPaid_Click(sender As Object, e As EventArgs) Handles btnViewTotalPaid.Click
        Try
            Dim SQL As String = ""

            Select Case cboStatPayType.Text

                Case "ALL"
                    SQL = "select " &
                        "substring(APBFacilityInformation.strAIRSNumber, 5, 8) as AIRSNumber, " &
                        "strFacilityName, " &
                        "strPaymentPlan, strPayTypedesc, numPayment, strDepositNo, " &
                        "datTransactionDate, strCheckNo, FS_Transactions.InvoiceID, " &
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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
                        "FS_Transactions.numFeeYear, numPart70Fee, " &
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

        Try
            Dim temp As String
            Dim SQL2 As String = ""

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
            End If

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
            "strPaymentPlan " &
            "from FS_FeeAuditedData " &
            "where strAIRSNumber = @airs " &
            "and numFeeYear = @year "

            dr = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
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
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtAllFees.Text = "ERROR"
                Else
                    txtAllFees.Text = Format(dr.Item("numTotalFee"), "c")
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
            End If

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

                Dim dt As DataTable = DB.GetDataTable(SQL)
                For Each dr2 As DataRow In dt.Rows
                    If IsDBNull(dr2.Item("description")) Then
                    Else
                        txtNSPSExemptReason.Text = "- " & txtNSPSExemptReason.Text & dr2.Item("description") & vbCrLf
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvDepositsAndPayments_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDepositsAndPayments.SelectionChanged
        If dgvDepositsAndPayments.SelectedRows.Count = 1 Then
            txtSelectedAIRSNumber.Text = dgvDepositsAndPayments.CurrentRow.Cells(0).Value
            txtSelectedFacilityName.Text = dgvDepositsAndPayments.CurrentRow.Cells(1).Value
            txtSelectedYear.Text = cboStatYear.Text
            If pnlDetails.Dock <> DockStyle.None Then
                LoadSelectedFeeData()
            End If
        End If
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvDepositsAndPayments.ExportToExcel(Me)
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
                btnViewTotalPaid.Enabled = False
                chbNonZeroBalance.Enabled = False
            Else
                dtpStartDepositDate.Enabled = False
                dtpEndDepositDate.Enabled = False
                btnRunDepositReport.Enabled = False
                cboStatYear.Enabled = True
                cboStatPayType.Enabled = True
                btnViewDepositsStats.Enabled = True
                btnViewPaymentDue.Enabled = True
                btnViewTotalPaid.Enabled = True
                chbNonZeroBalance.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunDepositReport_Click(sender As Object, e As EventArgs) Handles btnRunDepositReport.Click
        Try
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
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New FacilityFee10

            Dim SQL As String = "Select * from VW_Facility_Fee " &
            "where strAIRSNumber = @airs "

            Dim p As New SqlParameter("@airs", "0413" & cboAirsNo.SelectedValue)

            rpt.SetDataSource(DB.GetDataTable(SQL, p))

            Dim crParameterDiscreteValue As New ParameterDiscreteValue
            crParameterDiscreteValue.Value = "0413" & cboAirsNo.SelectedValue
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions = rpt.DataDefinition.ParameterFields
            Dim crParameterFieldDefinition As ParameterFieldDefinition = crParameterFieldDefinitions.Item("AirsNo")
            Dim crParameterValues As ParameterValues = crParameterFieldDefinition.CurrentValues
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

    Private Sub btnClassification_Click(sender As Object, e As EventArgs) Handles btnClassification.Click
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New FacilityClassification10

            Dim SQL As String = "Select * from VW_Facility_Class_Counts "

            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Facility Classification Totals")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRunBalanceReport_Click(sender As Object, e As EventArgs) Handles btnRunBalanceReport.Click
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim selectedYear As Integer = CInt(cbBalanceYear.Text)

            Dim ParameterFields As ParameterFields
            Dim ParameterField As ParameterField
            Dim spValue As ParameterDiscreteValue

            Dim rpt As ReportClass
            If chbFacilityBalance.Checked = False Then
                rpt = New FacilityBalance10
            Else
                rpt = New FacilityBalancewithZero10
            End If

            Dim SQL As String = "SELECT " &
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
        "FROM APBFacilityInformation " &
        "inner join FeeDetails " &
        "on APBFacilityInformation.strAIRSNumber = FeeDetails.strAIRSNumber " &
        "inner join FeesContact " &
        "on APBFacilityInformation.strAIRSNumber = FeesContact.strAIRSnumber " &
        "inner join APBHeaderData " &
        "on APBFacilityInformation.strAIRSnumber = APBHeaderData.strAIRSNumber " &
        "inner join FS_Transactions  " &
        "on APBFacilityInformation.strAIRSNumber = FS_Transactions.strAIRSNumber " &
        "and feedetails.intyear = FS_Transactions.numFeeYear " &
        "where feedetails.intyear = @year " &
        "order by strairsnumber "
            Dim p As New SqlParameter("@year", selectedYear.ToString)

            rpt.SetDataSource(DB.GetDataTable(SQL, p))

            'Do this just once at the start
            ParameterFields = New ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

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
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.Default
            Dim rpt As ReportClass = New TotalPayment10
            Dim SQL As String = "Select * from VW_Total_PAYMENT "

            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Overall Fee Balance")
            CRFeesReports.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnFeeByYear_Click(sender As Object, e As EventArgs) Handles btnFeeByYear.Click
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New feeByYear10
            Dim SQL As String = "Select * from FeesDue "

            rpt.SetDataSource(DB.GetDataTable(SQL))

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
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

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

        Dim rpt As ReportClass = New DepositQA11
        rpt.SetDataSource(DB.GetDataTable(query, parameters))

        SetUpCrystalReportViewer(rpt, CRFeesReports, "Deposits")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnViewFacilityDepositsReport_Click(sender As Object, e As EventArgs) Handles btnViewFacilityDepositsReport.Click
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

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

            Dim rpt As ReportClass = New DepositQA11
            rpt.SetDataSource(DB.GetDataTable(query, parameter))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Deposits")
        End If
    End Sub

#End Region

#Region "Compliance"

    Private Sub btnClassChange_Click(sender As Object, e As EventArgs) Handles btnClassChange.Click
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New ClassChanged10

            Dim SQL As String = "SELECT * FROM VW_CLASS_CHANGED WHERE INTYEAR >= (year(getdate()) - 5)"

            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "Deposits")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lblNSPS1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS1.LinkClicked
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim SQL As String = "Select * " &
            "from VW_NSPS_Status " &
            "where strnsps = 'YES' " &
            "and STRnspsexempt = '1'"

            Dim rpt As ReportClass = New NSPSStatus10
            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS Exempt - Subject but exempt")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lblNSPS2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS2.LinkClicked
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New NSPSStatus1_10
            Dim SQL As String = "Select * " &
            "from VW_NSPS_Status " &
            "where Strnsps1 = 'YES' " &
            "and strnsps = 'NO'"

            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS Subject - Not subject")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lblNSPS3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNSPS3.LinkClicked
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New NSPSStatus2_10
            Dim SQL As String = "Select * " &
            "from VW_NSPS_Status " &
            "where strnsps = 'YES' " &
            "and STRoperate <> 'YES'"

            rpt.SetDataSource(DB.GetDataTable(SQL))

            SetUpCrystalReportViewer(rpt, CRFeesReports, "NSPS, Did not Operate")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnNoOperate_Click(sender As Object, e As EventArgs) Handles btnNoOperate.Click
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New NoOperate10
            Dim SQL As String = "SELECT * FROM VW_NO_OPERATE WHERE INTYEAR >= (year(getdate()) - 5)"

            rpt.SetDataSource(DB.GetDataTable(SQL))

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
        If Not CrystalReportsIsAvailable() Then
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rpt As ReportClass = New FacilityInfo10
            Dim SQL As String = "Select * from VW_Facility_Info "

            rpt.SetDataSource(DB.GetDataTable(SQL))

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
        ViewFeeStats()
    End Sub

    Private Sub ViewFeeStats()
        Try

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

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFSSummaryFeeUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFSSummaryFeeUniverse.LinkClicked
        Try
            If cboFeeStatYear.Text <> "" Then
            Else
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "
            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and (strEnrolled = '0' or strEnrolled is null)  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin, APBFacilityInformation, " &
            "FSLK_Admin_Status " &
            "where FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                        "and FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "and numFeeyear = @year " &
            "and numCurrentStatus = '12'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and strInitialMailout = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numCurrentStatus <> '12'  " &
            "and strEnrolled = '1'  " &
            "and strInitialMailout = '0'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numcurrentstatus < '5'  " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numcurrentstatus > '4' " &
            "and numCurrentStatus < '8' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numcurrentstatus > '7' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
                "and not exists (select * " &
                "from fs_feeAudit " &
                "where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " &
                "and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " &
                "and fs_feeAudit.numfeeyear = @year " &
                "and fs_feeAudit.strendcollections = 'True')" &
                "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal <= (select datFeeDueDate from FS_FeeRate where numFeeyear = @year) " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
           "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal > (select datFeeDueDate from FS_FeeRate where numFeeyear = @year) " &
            "and datSubmittal <= (select datAdminApplicable from FS_FeeRate where numFeeyear = @year) " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal > (select datAdminApplicable from FS_FeeRate where numFeeyear = @year) " &
            "and Intsubmittal = '1' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus <= '8' " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and (numcurrentstatus = '9' or numcurrentstatus = '11' ) " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
            "and (strEnrolled = '1' or strEnrolled is null)  " &
            "and numcurrentstatus = '10' " &
            "and FS_Admin.Active = '1' " &
            "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
          "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and numcurrentstatus = '10' " &
          "and intSubmittal = '1' " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
          "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, strComment  " &
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            " inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "where numFeeyear = @year " &
          "and (strEnrolled = '1' or strEnrolled is null)  " &
          "and numcurrentstatus = '10' " &
          "and (intSubmittal = '0' or intsubmittal is null) " &
          "and FS_Admin.Active = '1' " &
          "order by strAIRSNumber "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id  " &
            "left join FS_Mailout " &
            "on FS_Admin.strAIRSNumber = FS_Mailout.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Mailout.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
            "and FS_Admin.Active = '1' GROUP BY substring(FS_Admin.strAIRSNumber, 5, 8), APBFacilityInformation.strFacilityName, strIAIPDesc, FS_Mailout.STRFIRSTNAME, FS_Mailout.STRLASTNAME, FS_Mailout.STRContactCONAME, FS_Mailout.STRCONTACTADDRESS1, FS_Mailout.STRCONTACTCITY, FS_Mailout.STRCONTACTSTATE, FS_Mailout.STRCONTACTZIPCODE, APBFACILITYINFORMATION.STRFACILITYSTREET1, APBFACILITYINFORMATION.STRFACILITYCITY, APBFACILITYINFORMATION.STRFACILITYZIPCODE, FS_Mailout.strGecoUserEmail, datShutDown, FS_Admin.strComment,  FS_Mailout.strClass, case  " &
            "when strOperate = '1' then 'Operating'  " &
            "else 'Not Operating'  " &
            "end, case  " &
            "when FS_Mailout.strPart70 = '1' then 'True'  " &
            "else 'False'  " &
            "end, case  " &
            "when FS_Mailout.strNSPS = '1' then 'True'  " &
            "else 'False'  " &
            "end, numTotalFee "

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, FS_Admin.strcomment, " &
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
            "from FS_Admin inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status  " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_Mailout " &
            "on FS_Admin.strAIRSNumber = FS_Mailout.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Mailout.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
              "and numcurrentstatus > '7' " &
            "and strEnrolled = '1'  " &
            "and FS_Admin.Active = '1' " &
               "and not exists (select * " &
                "from fs_feeAudit " &
                "where fs_admin.strairsnumber = fs_feeAudit.strAIRSnumber " &
                "and fs_admin.numfeeyear = fs_feeAudit.numfeeyear " &
                "and fs_feeAudit.numfeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
            "and numcurrentstatus > '4' " &
            "and numcurrentstatus < '12'  " &
            "and datSubmittal > (select datFeeDueDate from FS_FeeRate where numFeeyear = @year) " &
            "and datSubmittal <= (select datAdminApplicable from FS_FeeRate where numFeeyear = @year) " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
            "and numcurrentstatus > '4' " &
           "and numcurrentstatus < '12'  " &
          "and datSubmittal > (select datAdminApplicable from FS_FeeRate where numFeeyear = @year) " &
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


            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "SELECT   SUBSTRing(AD.STRAIRSNUMBER, 5, 8) AS strAIRSNumber, " &
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
            "  FROM FS_Admin AD " &
            "  inner join APBFacilityInformation FI " &
            "  on AD.STRAIRSNUMBER     = FI.STRAIRSNUMBER " &
            "  inner join FSLK_Admin_Status LK_AS " &
            "    on AD.NUMCURRENTSTATUS  = LK_AS.ID " &
            "    left join FS_ContactInfo CI " &
            "    on AD.NUMFEEYEAR        = CI.NUMFEEYEAR " &
            "    AND AD.STRAIRSNUMBER     = CI.STRAIRSNUMBER " &
            "    left join FS_FeeAuditedData FAD " &
            "    on AD.STRAIRSNUMBER     = FAD.STRAIRSNUMBER " &
            "    AND AD.NUMFEEYEAR        = FAD.NUMFEEYEAR " &
            "    left join (SELECT * FROM FS_Transactions TR WHERE TR.ACTIVE = '1' " &
            "    ) as TRX " &
            "    on AD.STRAIRSNUMBER     = TRX.STRAIRSNUMBER " &
            "    AND AD.NUMFEEYEAR        = TRX.NUMFEEYEAR " &
            "    where AD.NUMFEEYEAR        = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, FS_Admin.strComment,  " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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


            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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
                Return
            End If

            Dim SQL As String = "Select  " &
            "substring(FS_Admin.strAIRSNumber, 5, 8) as strAIRSNumber, strFacilityName, strIAIPDesc, " &
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
            "from FS_Admin " &
            "inner join APBFacilityInformation " &
            "on FS_Admin.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "inner join FSLK_Admin_Status " &
            "on FS_Admin.numcurrentstatus = FSLK_Admin_Status.id " &
            "left join FS_ContactInfo " &
            "on FS_Admin.strAIRSNumber = FS_ContactInfo.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_ContactInfo.numFeeYear " &
            "left join FS_FeeAuditedData " &
            "on FS_Admin.strAIRSNumber = FS_FeeAuditedData.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_FeeAuditedData.numFeeYear " &
            "left join FS_Transactions " &
            "on FS_Admin.strAIRSNumber = FS_Transactions.strAIRSNumber " &
            "and FS_Admin.numFeeYear = FS_Transactions.numFeeYear " &
            "where FS_Admin.numFeeyear = @year " &
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

            Dim p As New SqlParameter("@year", cboFeeStatYear.Text)

            dgvFeeStats.DataSource = DB.GetDataTable(SQL, p)

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

            If dgvFeeStats.RowCount > 0 AndAlso hti.RowIndex <> -1 AndAlso
                dgvFeeStats.Columns(0).HeaderText = "Airs No." Then

                If IsDBNull(dgvFeeStats(0, hti.RowIndex).Value) Then
                    txtFeeStatAirsNumber.Clear()
                Else
                    txtFeeStatAirsNumber.Text = dgvFeeStats(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckInvoices_Click(sender As Object, e As EventArgs) Handles btnCheckInvoices.Click
        Try
            If cboFeeStatYear.Text IsNot Nothing Then
                Dim feeYear As Integer = CInt(cboFeeStatYear.Text)

                Dim query As String = "Update FS_FeeInvoice set " &
                "strInvoiceStatus = '1', " &
                "UpdateUser = @Username,  " &
                "updateDateTime = getdate() " &
                "where numFeeYear = @FeeYear " &
                "and numAmount = '0' " &
                "and strInvoiceStatus = '0' " &
                "and active = '1' "

                Dim feeYearParam As SqlParameter = New SqlParameter("@FeeYear", SqlDbType.SmallInt) With {.Value = feeYear}

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
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnInvoicedPaymentDue_Click(sender As Object, e As EventArgs) Handles btnInvoicedPaymentDue.Click
        Try
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount as numTotalFee, strClass, '' as numAdminFee   " &
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount as numTotalFee, strClass, '' as numAdminFee   " &
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount as numTotalFee, strClass, '' as numAdminFee   " &
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
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
                        "'' as numPart70Fee, '' as numSMFee, '' as numNSPSFee,   " &
                        "numAmount *5 as numTotalFee, strClass, '' as numAdminFee   " &
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewInvoicedBalance_Click(sender As Object, e As EventArgs) Handles btnViewInvoicedBalance.Click
        Try
            Dim SQL As String = ""

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

            If chbNonZeroBalance.Checked = True Then
                SQL = SQL & " where balance <> 0 "
            End If

            SQL = SQL & " ORDER BY strairsnumber "

            Dim p As New SqlParameter("@year", cboStatYear.Text)

            dgvDepositsAndPayments.DataSource = DB.GetDataTable(SQL, p)

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
            NUMPART70FEE      as [Part 70 Fees],
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
        dgvReported.SanelyResizeColumns()
    End Sub

    Private Sub btnReportedExport_Click(sender As Object, e As EventArgs) Handles btnReportedExport.Click
        dgvReported.ExportToExcel(Me)
    End Sub

End Class