Public Class IaipFacilitySummaryPrint

    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property FacilityName As String

    Private Sub IaipFacilitySummaryPrintLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        FullPrintStartDate.Value = Today.AddMonths(-12)
        FullPrintEndDate.Value = Today
        If AirsNumber IsNot Nothing Then
            FacilityDisplay.Text = AirsNumber.FormattedString & ", " & FacilityName
        End If
    End Sub

    Private Sub ShowReport(sender As Object, e As EventArgs) Handles ShowBasicReportButton.Click, ShowFullReportButton.Click
        If Not CrystalReportsIsAvailable() Then
            Exit Sub
        End If

        If AirsNumber Is Nothing Then
            MessageBox.Show("The AIRS number is invalid")
            Exit Sub
        End If

        Cursor = Cursors.AppStarting

        Select Case CType(sender, Button).Name.ToString
            Case NameOf(ShowBasicReportButton)
                ShowBasicReport()
            Case NameOf(ShowFullReportButton)
                ShowFullReport()
        End Select

        Cursor = Nothing
    End Sub

    Private Sub ShowBasicReport()
        Cursor = Cursors.WaitCursor

        Dim rpt As New CR.Reports.FacilityBasicReport

        Dim dt As DataTable = ConvertToDataTable(Of Apb.Facilities.Facility)(New Apb.Facilities.Facility() {DAL.GetFacility(AirsNumber).RetrieveHeaderData})
        rpt.Subreports("FacilityBasicInfo.rpt").SetDataSource(dt)

        Dim crv As New CRViewerForm(rpt)

        If crv IsNot Nothing AndAlso Not crv.IsDisposed Then
            crv.Show()
        End If

        Cursor = Nothing
    End Sub

    Private Sub ShowFullReport()
        Cursor = Cursors.WaitCursor

        Dim rpt As New CR.Reports.FacilityDetailedReport

        Dim startdate As Date = FullPrintStartDate.Value
        Dim enddate As Date = FullPrintEndDate.Value

        If Date.Compare(FullPrintStartDate.Value, FullPrintEndDate.Value) > 0 Then
            startdate = FullPrintEndDate.Value
            enddate = FullPrintStartDate.Value
        End If

        Dim dt1 As DataTable = ConvertToDataTable(Of Apb.Facilities.Facility)(New Apb.Facilities.Facility() {DAL.GetFacility(AirsNumber).RetrieveHeaderData})
        rpt.Subreports("FacilityBasicInfo.rpt").SetDataSource(dt1)

        rpt.Subreports("SscpInspections.rpt").SetDataSource(DAL.Sscp.GetInspectionDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("SscpRmpInspections.rpt").SetDataSource(DAL.Sscp.GetRmpInspectionDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("SscpAcc.rpt").SetDataSource(DAL.Sscp.GetAccDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("SscpReports.rpt").SetDataSource(DAL.Sscp.GetCompReportsDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("SscpNotifications.rpt").SetDataSource(DAL.Sscp.GetCompNotificationsDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("SscpStackTests.rpt").SetDataSource(DAL.Sscp.GetCompStackTestDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("SscpFce.rpt").SetDataSource(DAL.Sscp.GetFceDataTable(startdate, enddate, AirsNumber))

        rpt.Subreports("FeesFacilitySum.rpt").SetDataSource(DAL.GetFeesFacilitySummaryAsDataTable(startdate.Year, enddate.Year, AirsNumber))

        rpt.Subreports("SscpEnforcementSum.rpt").SetDataSource(DAL.Sscp.GetEnforcementSummaryDataTable(startdate, enddate, AirsNumber))

        Dim pd As New Generic.Dictionary(Of String, String) From {
            {"StartDate", String.Format("{0:MMMM d, yyyy}", startdate)},
            {"EndDate", String.Format("{0:MMMM d, yyyy}", enddate)}
        }

        Dim crv As New CRViewerForm(rpt, pd)

        If crv IsNot Nothing AndAlso Not crv.IsDisposed Then
            crv.Show()
        End If

        Cursor = Nothing
    End Sub

End Class