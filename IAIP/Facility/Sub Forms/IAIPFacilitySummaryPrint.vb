Public Class IaipFacilitySummaryPrint

    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property FacilityName As String

    Private Sub IaipFacilitySummaryPrintLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        FullPrintStartDate.Value = Today.AddMonths(-12)
        FullPrintEndDate.Value = Today
        If AirsNumber IsNot Nothing Then
            FacilityDisplay.Text = AirsNumber.FormattedString & ", " & FacilityName
        End If
    End Sub

    Private Sub ShowReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowBasicReportButton.Click, ShowFullReportButton.Click
        If AirsNumber Is Nothing Then
            MessageBox.Show("The AIRS number is invalid")
            Exit Sub
        End If

        If sender IsNot Nothing Then
            sender.Cursor = Cursors.AppStarting
        End If

        Select Case sender.Name.ToString
            Case ShowBasicReportButton.Name.ToString
                ShowBasicReport()
            Case ShowFullReportButton.Name.ToString
                ShowFullReport()
        End Select

        If sender IsNot Nothing Then
            sender.Cursor = Nothing
        End If
    End Sub

    Private Sub ShowBasicReport()
        Me.Cursor = Cursors.WaitCursor

        Dim rpt As New CR.Reports.FacilityBasicReport

        Dim dt As DataTable = CollectionHelper.ConvertToDataTable(Of Apb.Facilities.Facility)(New Apb.Facilities.Facility() {DAL.GetFacility(AirsNumber).RetrieveHeaderData})
        rpt.Subreports("FacilityBasicInfo.rpt").SetDataSource(dt)

        Dim crv As New CRViewerForm(rpt)
        crv.Show()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ShowFullReport()
        Me.Cursor = Cursors.WaitCursor

        Dim rpt As New CR.Reports.FacilityDetailedReport

        Dim startdate As Date = FullPrintStartDate.Value
        Dim enddate As Date = FullPrintEndDate.Value
        If Date.Compare(FullPrintStartDate.Value, FullPrintEndDate.Value) > 0 Then
            startdate = FullPrintEndDate.Value
            enddate = FullPrintStartDate.Value
        End If

        Dim dt1 As New DataTable
        dt1 = CollectionHelper.ConvertToDataTable(Of Apb.Facilities.Facility)(New Apb.Facilities.Facility() {DAL.GetFacility(AirsNumber).RetrieveHeaderData})
        rpt.Subreports("FacilityBasicInfo.rpt").SetDataSource(dt1)

        Dim dt2 As New DataTable("VW_SSCP_INSPECTIONS")
        dt2 = DAL.Sscp.GetInspectionDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpInspections.rpt").SetDataSource(dt2)

        Dim dt3 As New DataTable("VW_SSCP_RMPINSPECTIONS")
        dt3 = DAL.Sscp.GetRmpInspectionDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpRmpInspections.rpt").SetDataSource(dt3)

        Dim dt4 As New DataTable("VW_SSCP_ACCS")
        dt4 = DAL.Sscp.GetAccDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpAcc.rpt").SetDataSource(dt4)

        Dim dt5 As New DataTable("VW_SSCP_REPORTS")
        dt5 = DAL.Sscp.GetCompReportsDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpReports.rpt").SetDataSource(dt5)

        Dim dt6 As New DataTable("VW_SSCP_NOTIFICATIONS")
        dt6 = DAL.Sscp.GetCompNotificationsDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpNotifications.rpt").SetDataSource(dt6)

        Dim dt7 As New DataTable("VW_SSCP_STACKTESTS")
        dt7 = DAL.Sscp.GetCompStackTestDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpStackTests.rpt").SetDataSource(dt7)

        Dim dt8 As New DataTable("VW_SSCP_FCES")
        dt8 = DAL.Sscp.GetFceDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpFce.rpt").SetDataSource(dt8)

        Dim dt10 As New DataTable("VW_FEES_FACILITY_SUMMARY")
        dt10 = DAL.FeesData.GetFeesFacilitySummaryAsDataTable(startdate.Year, enddate.Year, AirsNumber)
        rpt.Subreports("FeesFacilitySum.rpt").SetDataSource(dt10)

        Dim dt11 As New DataTable("VW_SSCP_ENFORCEMENT_SUMMARY")
        dt11 = DAL.Sscp.GetEnforcementSummaryDataTable(startdate, enddate, AirsNumber)
        rpt.Subreports("SscpEnforcementSum.rpt").SetDataSource(dt11)

        Dim pd As New Generic.Dictionary(Of String, String) From {
            {"StartDate", String.Format("{0:MMMM d, yyyy}", startdate)},
            {"EndDate", String.Format("{0:MMMM d, yyyy}", enddate)}
        }

        Dim crv As New CRViewerForm(rpt, pd)
        crv.Show()

        Me.Cursor = Cursors.Default
    End Sub

End Class