Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient

Public Class FinStatistics

    Protected Overrides Sub OnLoad(e As EventArgs)
        SetDateRange()
        SetUpReportTypes()

        MyBase.OnLoad(e)
    End Sub

    Private Sub SetUpReportTypes()
        cmbReportType.BindToDictionary(ReportList)
        cmbReportType.SetDropDownWidth()
    End Sub

    Private Enum ReportType
        FeesAssessedPerApplicationType
        FeesAssessedPerApplicationFeeType
        FeesAssessedPerExpeditedFeeType
        FeesAssessedPerFeeType
        FeesReceivedPerType
        UnpaidApplications
    End Enum

    Private ReadOnly ReportList As New Dictionary(Of ReportType, String) From {
        {ReportType.FeesAssessedPerApplicationType, "Fees assessed per application type"},
        {ReportType.FeesAssessedPerApplicationFeeType, "Fees assessed per application fee type"},
        {ReportType.FeesAssessedPerExpeditedFeeType, "Fees assessed per expedited fee type"},
        {ReportType.FeesAssessedPerFeeType, "Fees assessed per application/expedited fee type"},
        {ReportType.FeesReceivedPerType, "Fees received per application type"},
        {ReportType.UnpaidApplications, "Unpaid permit application fees"}
    }

    Private Sub SetDateRange()
        dtpStartDate.Value = New Date(Today.Year, Today.Month, 1).AddMonths(-1)
        dtpEndDate.Value = New Date(Today.Year, Today.Month, 1).AddDays(-1)
    End Sub

    Private Sub chkApplyDates_CheckedChanged(sender As Object, e As EventArgs) Handles chkApplyDates.CheckedChanged
        dtpStartDate.Enabled = chkApplyDates.Checked
        dtpEndDate.Enabled = chkApplyDates.Checked
    End Sub

    Private Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        RunReport()
    End Sub

    Private Sub RunReport()
        dgvResults.DataSource = Nothing

        Dim spName As String = ""

        Select Case CType(cmbReportType.SelectedValue, ReportType)
            Case ReportType.UnpaidApplications
                spName = "fees.GetApplicationFeeStatusReport"
            Case ReportType.FeesAssessedPerApplicationType
                spName = "fees.GetApplicationTypeFeeSummary"
            Case ReportType.FeesReceivedPerType
                spName = "fees.GetApplicationTypeDepositSummary"
            Case ReportType.FeesAssessedPerApplicationFeeType
                spName = "fees.GetApplicationFeeTypeFeeSummary"
            Case ReportType.FeesAssessedPerExpeditedFeeType
                spName = "fees.GetExpeditedFeeTypeFeeSummary"
            Case ReportType.FeesAssessedPerFeeType
                spName = "fees.GetAllFeeTypesFeeSummary"
        End Select

        Dim params As SqlParameter() = Array.Empty(Of SqlParameter)()

        If chkApplyDates.Checked Then
            params = {
                New SqlParameter("@startDate", dtpStartDate.Value),
                New SqlParameter("@endDate", dtpEndDate.Value)
            }
        End If

        dgvResults.DataSource = DB.SPGetDataTable(spName, params)
        dgvResults.SelectNone()
    End Sub
End Class