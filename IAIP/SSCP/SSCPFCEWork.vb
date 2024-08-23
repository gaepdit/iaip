Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports System.Text
Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports Iaip.UrlHelpers

Public Class SSCPFCEWork

#Region " Properties "

    Private _airsNumber As ApbFacilityId
    Public Property AirsNumber As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(value As ApbFacilityId)
            _airsNumber = value
            SetSqlParameters()
        End Set
    End Property

    Private facility As Facility
    Private fceTable As DataTable
    Private paramAirs As SqlParameter
    Private paramWithDates As SqlParameter()

#End Region

#Region " Page Load "

    Private Sub SSCPFCEWork_Load(sender As Object, e As EventArgs) Handles Me.Load
        DTPFCECompleteDate.Value = Today
        DTPFilterStartDate.Value = Today.AddDays(-365)
        DTPFilterEndDate.Value = Today

        LoadHeaderData()
        LoadFCEDataset()
        LoadReviewerCombo()
        FillFCEData()

        If Not (AccountFormAccess(50, 1) = "1" OrElse AccountFormAccess(50, 2) = "1" OrElse AccountFormAccess(50, 3) = "1" OrElse AccountFormAccess(50, 4) = "1" OrElse
           CurrentUser.HasRole(118)) Then
            btnSave.Visible = False
        End If
    End Sub

    Private Sub LoadHeaderData()
        facility = DAL.GetFacility(AirsNumber).RetrieveHeaderData

        If facility Is Nothing Then
            MessageBox.Show("There was an error loading the facility.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        Dim sb As New StringBuilder
        sb.AppendLine(AirsNumber.FormattedString)
        sb.AppendLine(facility.FacilityName)
        sb.AppendLine(facility.DisplayFacilityAddress)
        sb.AppendLine(facility.DisplayCounty & " County")
        sb.AppendLine("Classification: " & facility.DisplayClassification)
        sb.AppendLine("Air Program Codes:")
        sb.AppendLine(facility.DisplayAirPrograms)

        txtFacilityInformation.Text = sb.ToString
    End Sub

    Private Sub LoadFCEDataset()
        Dim SQL As String = "SELECT
                f.STRFCENUMBER,
                f.STRFCEYEAR
            FROM SSCPFCE AS f
                INNER JOIN SSCPFCEMASTER AS m
                    ON m.STRFCENUMBER = f.STRFCENUMBER
            WHERE m.STRAIRSNUMBER = @airs
                    and (IsDeleted is null or IsDeleted = 0)
            ORDER BY f.DATFCECOMPLETED DESC "
        Dim p As New SqlParameter("@airs", AirsNumber.DbFormattedString)
        fceTable = DB.GetDataTable(SQL, p)
        fceTable.PrimaryKey = Nothing
        fceTable.Columns("STRFCENUMBER").AllowDBNull = True
    End Sub

    Private Sub LoadReviewerCombo()
        With cboReviewer
            .DataSource = GetSharedData(SharedTable.AllComplianceStaff)
            .DisplayMember = "StaffName"
            .ValueMember = "UserID"
            .SelectedIndex = -1
        End With
        cboReviewer.SelectedValue = CurrentUser.UserID
    End Sub

    Private Sub FillFCEData()
        Try
            Dim fiscalEnd As New Date(Today.Year, 10, 1)

            If fceTable.Rows.Count = 0 Then
                ' Only add next (calendar) year after October 1 of this year
                If Today >= fiscalEnd Then cboFCEYear.Items.Add(Today.AddYears(1).Year)

                ' Add current year
                cboFCEYear.Items.Add(Today.Year)

                ' Only add previous year before October 1 of this year
                If Today < fiscalEnd Then cboFCEYear.Items.Add(Today.AddYears(-1).Year)

                cboFCEYear.Text = Today.Year
                txtFCENumber.Text = ""
            Else
                Dim dtFCE As DataTable = fceTable.Copy

                ' Only add next (calendar) year after October 1 of this year
                If Today >= fiscalEnd AndAlso
                    dtFCE.Select("STRFCEYEAR=" & Today.AddYears(1).Year).Length = 0 Then

                    Dim dr As DataRow = dtFCE.NewRow()
                    dr("STRFCEYEAR") = Today.AddYears(1).Year
                    dtFCE.Rows.Add(dr)
                End If

                ' Add current year if missing
                If dtFCE.Select("STRFCEYEAR=" & Today.Year).Length = 0 Then
                    Dim dr As DataRow = dtFCE.NewRow()
                    dr("STRFCEYEAR") = Today.Year
                    dtFCE.Rows.Add(dr)
                End If

                ' Only add previous year before October 1 of this year
                If Today < fiscalEnd AndAlso
                    dtFCE.Select("STRFCEYEAR=" & Today.AddYears(-1).Year).Length = 0 Then

                    Dim dr As DataRow = dtFCE.NewRow()
                    dr("STRFCEYEAR") = Today.AddYears(-1).Year
                    dtFCE.Rows.Add(dr)
                End If

                dtFCE.DefaultView.Sort = "STRFCEYEAR DESC"

                With txtFCENumber
                    .DataBindings.Clear()
                    .DataBindings.Add(New Binding("Text", dtFCE, "STRFCENUMBER"))
                End With

                With cboFCEYear
                    .DataSource = dtFCE
                    .DisplayMember = "STRFCEYEAR"
                    .ValueMember = "STRFCENUMBER"
                    .SelectedIndex = 0
                End With
            End If
            AddHandler cboFCEYear.SelectedIndexChanged, AddressOf cboFCEYear_SelectedIndexChanged
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Sub SetFceYear(Year As Integer)
        cboFCEYear.SelectedIndex = cboFCEYear.FindStringExact(Year)
    End Sub

#End Region

#Region " Load Datasets "

    Private Sub LoadFCEInspectionData()
        Dim SQL As String = "select convert(int, SSCPInspections.strTrackingNumber) as strTrackingNumber, " &
        "datReceivedDate as ReceivedDate,  " &
        "CONCAT(strLastName, ', ', strFirstName) as ReviewingEngineer, " &
        "datInspectionDateStart as InspectionDateStart,  " &
        "datInspectionDateStart as InspectionTimeStart,  " &
        "datInspectionDateEnd as InspectionDateEnd,  " &
        "datInspectionDateEnd as InspectionTimeEnd,  " &
        "strInspectionReason, strWeatherConditions, strInspectionGuide,  " &
        "strFacilityOperating, strInspectionComplianceStatus,  " &
        "datCompleteDate as InspectionReportComplete,  " &
        "datAcknoledgmentLetterSent as AcknowledgmentLetterSent,  " &
        "strInspectionComments  " &
        "from SSCPInspections, EPDUserProfiles, SSCPItemMaster  " &
        "where  " &
        "EPDUserProfiles.numUserID = SSCPItemMaster.strResponsibleStaff  " &
        "and SSCPItemMaster.strTrackingNumber = SSCPInspections.strTrackingNumber  " &
        "and SSCPItemMaster.strAIRSNumber = @airs " &
        "and ((datCompleteDate between @startDate and @endDate ) " &
        "or (datReceivedDate between @startDate and @endDate )) " &
        "and (strDelete is Null or strDelete <> 'True') " &
        "Order by SSCPInspections.strTrackingNumber DESC  "

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrFCEInspections.DataSource = dt
            dgrFCEInspections.Columns("strTrackingNumber").HeaderText = "Tracking Number"
            dgrFCEInspections.Columns("ReceivedDate").HeaderText = "Date Received"
            dgrFCEInspections.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
            dgrFCEInspections.Columns("InspectionDateStart").HeaderText = "Inspection Start Date"
            dgrFCEInspections.Columns("InspectionDateEnd").HeaderText = "Inspection End Date"
            dgrFCEInspections.Columns("InspectionTimeStart").HeaderText = "Inspection Start Time"
            dgrFCEInspections.Columns("InspectionTimeEnd").HeaderText = "Inspection End Time"
            dgrFCEInspections.Columns("strInspectionReason").HeaderText = "Inspection Reason"
            dgrFCEInspections.Columns("strWeatherConditions").HeaderText = "Weather Conditions"
            dgrFCEInspections.Columns("strInspectionGuide").HeaderText = "Inspection Guide"
            dgrFCEInspections.Columns("InspectionReportComplete").HeaderText = "Inspection Complete Date"
            dgrFCEInspections.Columns("AcknowledgmentLetterSent").HeaderText = "Date Acknowledgment Letter Sent"
            dgrFCEInspections.Columns("strInspectionComments").HeaderText = "Comments"
            dgrFCEInspections.SanelyResizeColumns
        End If
    End Sub

    Private Sub LoadFCEACCData()
        Dim SQL As String = "SELECT
            convert(int, b.strTrackingNumber) as strTrackingNumber,
            datReceivedDate                         AS ReceivedDate,
            CONCAT(strLastName, ', ', strFirstName) AS ReviewingEngineer,
            strPostMarkedOnTime,
            datPostMarkDate                         AS PostmarkDate,
            strSignedByRO,
            strCorrectACCForms,
            strTitleVConditionsListed,
            strACCCorrectlyFilledOut,
            strReportedDeviations,
            strDeviationsUnreported,
            strComments,
            strEnforcementNeeded,
            datCompleteDate                         AS CompleteDate
        FROM SSCPACCS a
            INNER JOIN SSCPItemMaster b
                ON b.strTrackingNumber = a.strTrackingNumber
            INNER JOIN EPDUSerProfiles c
                ON c.numUserID = b.strModifingperson
        WHERE ((datCompleteDate BETWEEN @startDate AND @endDate)
               OR (datReceivedDate BETWEEN @startDate AND @endDate))
              AND b.strAIrsnumber = @airs
              AND (strDelete IS NULL OR strDelete <> 'True')"

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrFCEACC.DataSource = dt
            dgrFCEACC.Columns("strTrackingNumber").HeaderText = "Tracking Number"
            dgrFCEACC.Columns("ReceivedDate").HeaderText = "Date Received"
            dgrFCEACC.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
            dgrFCEACC.Columns("strPostMarkedOnTime").HeaderText = "Postmarked On Time"
            dgrFCEACC.Columns("PostmarkDate").HeaderText = "Date Postmarked"
            dgrFCEACC.Columns("strSignedByRO").HeaderText = "Signed by Responsible Official"
            dgrFCEACC.Columns("strCorrectACCForms").HeaderText = "Correct Forms Used"
            dgrFCEACC.Columns("strTitleVConditionsListed").HeaderText = "Listed Title V Conditions"
            dgrFCEACC.Columns("strACCCorrectlyFilledOut").HeaderText = "ACC Correctly Filled Out"
            dgrFCEACC.Columns("strReportedDeviations").HeaderText = "Deviations Reported"
            dgrFCEACC.Columns("strDeviationsUnreported").HeaderText = "Any Unreported Deviations"
            dgrFCEACC.Columns("strEnforcementNeeded").HeaderText = "Enforcement Needed"
            dgrFCEACC.Columns("CompleteDate").HeaderText = "Date Completed"
            dgrFCEACC.Columns("strComments").HeaderText = "Comments"
            dgrFCEACC.SanelyResizeColumns
        End If
    End Sub

    Private Sub LoadFCEReports()
        Dim SQL As String = "SELECT
            convert(int, b.strTrackingNumber) as strTrackingNumber,
            datReceivedDate         AS ReceivedDate,
            strReportPeriod,
            DatReportingPeriodStart AS ReportingStartDate,
            datReportingPeriodEnd   AS ReportingEndDate,
            strReportingPeriodComments,
            datReportDueDate        AS ReportDueDate,
            datSentByFacilityDate   AS DateSentByFacility,
            strCompleteStatus,
            strEnforcementNeeded,
            strShowDeviation,
            strGeneralComments
        FROM SSCPREports a
            INNER JOIN SSCPItemMaster b
                ON b.strTrackingNumber = a.strTrackingNumber
        WHERE ((datCompleteDate BETWEEN @startDate AND @endDate)
               OR (datReceivedDate BETWEEN @startDate AND @endDate))
              AND (strDelete IS NULL OR strDelete <> 'True')
              AND b.strAIrsnumber = @airs"

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrFCEReports.DataSource = dt
            dgrFCEReports.Columns("strTrackingNumber").HeaderText = "Tracking Number"
            dgrFCEReports.Columns("ReceivedDate").HeaderText = "Date Received"
            dgrFCEReports.Columns("strReportPeriod").HeaderText = "Reporting Period"
            dgrFCEReports.Columns("ReportingStartDate").HeaderText = "Report Start Date"
            dgrFCEReports.Columns("ReportingEndDate").HeaderText = "Report End Date"
            dgrFCEReports.Columns("strReportingPeriodComments").HeaderText = "Reporting Period Comments"
            dgrFCEReports.Columns("ReportDueDate").HeaderText = "Report Due Date"
            dgrFCEReports.Columns("DateSentByFacility").HeaderText = "Date Report Sent by Facility"
            dgrFCEReports.Columns("strCompleteStatus").HeaderText = "Report Complete Status"
            dgrFCEReports.Columns("strEnforcementNeeded").HeaderText = "Enforcement Needed"
            dgrFCEReports.Columns("strShowDeviation").HeaderText = "Deviations"
            dgrFCEReports.Columns("strGeneralComments").HeaderText = "Comments"
            dgrFCEReports.SanelyResizeColumns
        End If
    End Sub

    Private Sub LoadFCECorrespondance()
        Dim SQL As String = "SELECT
            convert(int, b.strTrackingNumber) as strTrackingNumber,
            datReceivedDate                AS ReceivedDate,
            CASE WHEN strNotificationDue = 'True'
                THEN datNotificationDue
            WHEN strNotificationDue = 'False'
                THEN NULL END              AS NotificationDate,
            CASE WHEN strNotificationSent = 'True'
                THEN DatNotificationSent
            WHEN strNotificationSent = 'False'
                THEN NULL END              AS NotificationSent,
            CASE WHEN strNotificationType = '01'
                THEN strNotificationTypeOther
            ELSE j.strNotificationDesc END AS Notification,
            strNotificationComment
        FROM SSCPNotifications a
            INNER JOIN SSCPItemMaster b
                ON b.strTrackingNumber = a.strTrackingNumber
            LEFT JOIN LookUPSSCPNotifications j
                ON j.STRNOTIFICATIONKEY = a.STRNOTIFICATIONTYPE
        WHERE ((datCompleteDate BETWEEN @startDate AND @endDate)
               OR (datReceivedDate BETWEEN @startDate AND @endDate))
              AND (strDelete IS NULL OR strDelete <> 'True')
              AND b.strAIrsnumber = @airs"

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrFCECorrespondance.DataSource = dt
            dgrFCECorrespondance.Columns("strTrackingNumber").HeaderText = "Tracking Number"
            dgrFCECorrespondance.Columns("ReceivedDate").HeaderText = "Date Received"
            dgrFCECorrespondance.Columns("NotificationDate").HeaderText = "Notification Due Date"
            dgrFCECorrespondance.Columns("NotificationSent").HeaderText = "Date Notification Sent"
            dgrFCECorrespondance.Columns("Notification").HeaderText = "Notification Type"
            dgrFCECorrespondance.Columns("strNotificationComment").HeaderText = "Comments"
            dgrFCECorrespondance.SanelyResizeColumns
        End If
    End Sub

    Private Sub LoadFCESummaryReports()
        Dim SQL As String = "SELECT
            convert(int, b.strReferenceNumber) as strReferenceNumber,
            b.strEmissionSource,
            c.strPollutantDescription,
            d.strReportType,
            CONCAT(e.strLastName, ', ', e.strFirstName) AS ReviewingEngineer,
            b.datTestDateStart                          AS TestDateStart,
            b.datReceivedDate                           AS REceivedDate,
            CASE WHEN b.datCompleteDate = '04-Jul-1776'
                THEN NULL
            ELSE b.datCompleteDate END                  AS CompleteDate,
            f.strComplianceStatus,
            CASE WHEN b.strClosed = 'False'
                THEN 'Open'
            ELSE 'Closed' END                           AS Status,
            b.mmoCommentArea,
            g.strDocumentType,
            b.strApplicableRequirement,
            h.strUnitDesc
        FROM ISMPMaster a
            INNER JOIN ISMPReportInformation b
                ON b.strREferenceNumber = a.strReferenceNumber
            INNER JOIN LookUPPollutants c
                ON c.strPOllutantCode = b.strPOllutant
            INNER JOIN ISMPReportType d
                ON d.strKey = b.strReportType
            INNER JOIN EPDUserProfiles e
                ON e.numUserID = b.strReviewingEngineer
            INNER JOIN LookUPISMPComplianceStatus
                       f ON f.strComplianceKey = b.strComplianceStatus
            INNER JOIN ISMPDocumentType g
                ON g.strKey = b.strDocumentType
            INNER JOIN LookUpEPDUnits h
                ON b.strReviewingUnit = h.numUnitCode
        WHERE a.strAIRSNumber = @airs
              AND ((b.datCompleteDate BETWEEN @startDate AND @endDate)
                   OR (b.datReceivedDate BETWEEN @startDate AND @endDate))
              AND (b.strDelete IS NULL OR b.strDelete <> 'True')
        ORDER BY b.strReferenceNumber DESC"

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrISMPSummaryReports.DataSource = dt
            dgrISMPSummaryReports.Columns("strReferenceNumber").HeaderText = "Reference Number"
            dgrISMPSummaryReports.Columns("strEmissionSource").HeaderText = "Emission Source"
            dgrISMPSummaryReports.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgrISMPSummaryReports.Columns("strReportType").HeaderText = "Report Type"
            dgrISMPSummaryReports.Columns("strDocumentType").HeaderText = "Document Type"
            dgrISMPSummaryReports.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
            dgrISMPSummaryReports.Columns("TestDateStart").HeaderText = "Test Date"
            dgrISMPSummaryReports.Columns("ReceivedDate").HeaderText = "Received Date"
            dgrISMPSummaryReports.Columns("CompleteDate").HeaderText = "Complete Date"
            dgrISMPSummaryReports.Columns("Status").HeaderText = "Report Open/Closed"
            dgrISMPSummaryReports.Columns("strComplianceStatus").HeaderText = "Compliance Status"
            dgrISMPSummaryReports.Columns("mmoCommentAREA").HeaderText = "Comment Field"
            dgrISMPSummaryReports.SanelyResizeColumns
        End If
    End Sub

    Private Sub LoadFCEPerformanceTests()
        Dim SQL As String = "SELECT
            convert(int, b.strTrackingNumber) as strTrackingNumber,
            datReceivedDate                         AS ReceivedDate,
            CONCAT(strLastName, ', ', strFirstName) AS ReviewingEngineer,
            strReferenceNumber,
            strTestReportComments,
            datCompleteDate                         AS CompleteDate,
            strTestReportFollowUp
        FROM SSCPItemMaster a
            INNER JOIN SSCPTestReports b
                ON a.strTrackingNumber = b.strTrackingNumber
            LEFT JOIN EPDUserProfiles c
                ON c.numUserID = a.strResponsibleStaff
        WHERE ((datCompleteDate BETWEEN @startDate AND @endDate)
               OR (datReceivedDate BETWEEN @startDate AND @endDate))
              AND (strDelete IS NULL OR strDelete <> 'True')
              AND a.strAIrsnumber = @airs"

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrPerformanceTests.DataSource = dt
            dgrPerformanceTests.Columns("strTrackingNumber").HeaderText = "Tracking Number"
            dgrPerformanceTests.Columns("ReceivedDate").HeaderText = "Date Received"
            dgrPerformanceTests.Columns("ReviewingEngineer").HeaderText = "Staff Responsible"
            dgrPerformanceTests.Columns("strReferenceNumber").HeaderText = "ISMP Reference Number"
            dgrPerformanceTests.Columns("CompleteDate").HeaderText = "Complete Date"
            dgrPerformanceTests.Columns("strTestReportFollowUp").HeaderText = "Enforcement Needed"
            dgrPerformanceTests.Columns("strTestReportComments").HeaderText = "Comments"
            dgrPerformanceTests.SanelyResizeColumns
        End If
    End Sub

    Private Sub LoadFCEEnforcement()
        Dim SQL As String = "SELECT
            convert(int, a.strEnforcementNumber) as strEnforcementNumber,
            datEnforcementFinalized                 AS EnforcementFinalized,
            CONCAT(strLastName, ', ', strFirstName) AS StaffResponsible,
            strActionType
        FROM SSCP_AuditedEnforcement a
            INNER JOIN EPDUserProfiles b
                ON b.numUserID = a.numStaffResponsible
        WHERE datDiscoveryDate BETWEEN @startDate AND @endDate
              AND (strStatus IS NULL OR strStatus <> 'True')
              AND strAIrsnumber = @airs
              and (IsDeleted = 0 or IsDeleted is null)"

        Dim dt As DataTable = DB.GetDataTable(SQL, paramWithDates)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgrFCEEnforcement.DataSource = dt
            dgrFCEEnforcement.Columns("strEnforcementNumber").HeaderText = "Enforcement Number"
            dgrFCEEnforcement.Columns("strActionType").HeaderText = "Enforcement Type"
            dgrFCEEnforcement.Columns("EnforcementFinalized").HeaderText = "Enforcement Resolved"
            dgrFCEEnforcement.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgrFCEEnforcement.SanelyResizeColumns
        End If
    End Sub

#End Region

#Region " Load data "

    Private Sub cboFCEYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Not IsNumeric(cboFCEYear.SelectedValue) Then
                cboReviewer.SelectedValue = CurrentUser.UserID
                rdbFCEOnSite.Checked = False
                rdbFCENoOnsite.Checked = False

                Dim fiscalEndDate As New Date(cboFCEYear.Text, 9, 30)
                If Today > fiscalEndDate Then
                    DTPFCECompleteDate.Value = fiscalEndDate
                Else
                    DTPFCECompleteDate.Value = Today
                End If

                DTPFilterStartDate.Value = DTPFCECompleteDate.Value.AddDays(-365)
                DTPFilterEndDate.Value = DTPFCECompleteDate.Value
                txtFCEComments.Clear()
                btnPrint.Enabled = False
                ClearReviewData()
                Return
            End If

            Dim SQL As String = "SELECT
                datFCECompleted,
                strFCEComments,
                CASE WHEN strSiteInspection IS NULL
                    THEN 'False'
                ELSE strSiteInspection END strSiteInspection,
                strReviewer,
                strFCEYear
            FROM SSCPFCE
            WHERE strFCENumber = @fce"

            Dim p As New SqlParameter("@fce", cboFCEYear.SelectedValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("datFCECompleted")) Then
                    DTPFCECompleteDate.Value = Today
                    DTPFilterStartDate.Value = Today.AddDays(-365)
                    DTPFilterEndDate.Value = Today
                Else
                    Dim fceDate As Date = dr.Item("datFCECompleted")
                    DTPFCECompleteDate.Value = fceDate
                    DTPFilterStartDate.Value = fceDate.AddDays(-365)
                    DTPFilterEndDate.Value = fceDate
                End If
                If IsDBNull(dr.Item("strFCEComments")) Then
                    txtFCEComments.Text = "No Comments"
                Else
                    txtFCEComments.Text = dr.Item("strFCEComments")
                End If
                If IsDBNull(dr.Item("strReviewer")) Then
                    cboReviewer.SelectedValue = CurrentUser.UserID
                Else
                    cboReviewer.SelectedValue = dr.Item("strReviewer")
                End If
                If IsDBNull(dr.Item("strSiteInspection")) Then
                    rdbFCENoOnsite.Checked = True
                Else
                    If dr.Item("strSiteInspection") = "True" Then
                        rdbFCEOnSite.Checked = True
                    Else
                        rdbFCENoOnsite.Checked = True
                    End If
                End If
                If Not IsDBNull(dr.Item("strFCEYear")) Then
                    cboFCEYear.Text = dr.Item("strFCEYear")
                End If
            End If

            btnPrint.Enabled = True
            LoadReviewData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbViewFCEData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewFCEData.LinkClicked
        LoadReviewData()
    End Sub

    Private Sub LoadReviewData()
        LoadFCEInspectionData()
        LoadFCEACCData()
        LoadFCEReports()
        LoadFCECorrespondance()
        LoadFCEPerformanceTests()
        LoadFCESummaryReports()
        LoadFCEEnforcement()
    End Sub

    Private Sub ClearReviewData()
        dgrFCEACC.DataSource = Nothing
        dgrFCECorrespondance.DataSource = Nothing
        dgrFCEEnforcement.DataSource = Nothing
        dgrFCEInspections.DataSource = Nothing
        dgrFCEReports.DataSource = Nothing
        dgrISMPSummaryReports.DataSource = Nothing
        dgrPerformanceTests.DataSource = Nothing
    End Sub

#End Region

#Region " DataGrid mouse events "

    Private Sub dgrFCEACC_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrFCEACC.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrFCEACC.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then

                txtACCTrackingNumber.Text = dgrFCEACC(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrFCECorrespondance_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrFCECorrespondance.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrFCECorrespondance.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then
                txtNotificationTrackingNumber.Text = dgrFCECorrespondance(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrFCEEnforcement_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrFCEEnforcement.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrFCEEnforcement.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then
                txtEnforcement.Text = dgrFCEEnforcement(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrFCEInspections_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrFCEInspections.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrFCEInspections.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then
                txtInspectionTrackingNumber.Text = dgrFCEInspections(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrISMPSummaryReports_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrISMPSummaryReports.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrISMPSummaryReports.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then
                txtISMPReferenceNumber.Text = dgrISMPSummaryReports(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrFCEReports_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrFCEReports.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrFCEReports.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then
                txtReportTrackingNumber.Text = dgrFCEReports(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrPerformanceTests_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrPerformanceTests.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrPerformanceTests.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGridViewHitTestType.Cell Then
                txtPerformanceTests.Text = dgrPerformanceTests(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Saving "

    Private Sub SaveFCE()
        Try
            If AccountFormAccess(50, 2) = "0" AndAlso AccountFormAccess(50, 3) = "0" AndAlso AccountFormAccess(50, 4) = "0" Then
                MsgBox("Insufficient permissions to save Full Compliance Evaluations.", MsgBoxStyle.Information, "Full Compliance Evaluation.")
            Else
                Dim FCEYear As Integer = CInt(cboFCEYear.Text)

                ' During first two weeks of October, double-check FCE year selection
                Dim currentYear As Integer = Today.Year
                If Today >= New Date(currentYear, 10, 1) AndAlso Today <= New Date(currentYear, 10, 14) AndAlso txtFCENumber.Text = "" Then

                    Dim msg As New StringBuilder("Please verify that you have selected the correct FCE year. You selected ")
                    msg.Append(FCEYear)
                    msg.Append(", the fiscal year ")
                    If FCEYear = currentYear Then
                        msg.Append("that ended September 30.")
                    Else
                        msg.Append("that began October 1.")
                    End If
                    msg.Append(" Do you want to continue with ")
                    msg.Append(FCEYear)
                    msg.Append("?"c)

                    Dim fceYearResult As DialogResult = MessageBox.Show(msg.ToString, "Verify FCE Year", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                    If fceYearResult = DialogResult.No Then
                        Return
                    End If
                End If

                Dim SQL As String
                Dim sqlList As New List(Of String)
                Dim paramList As New List(Of SqlParameter())

                Dim FCENumber As Integer
                Dim FCECompleteDate As Date = DTPFCECompleteDate.Value
                Dim FCEComments As String = ""

                If txtFCEComments.Text = "" Then
                    FCEComments = "N/A"
                Else
                    FCEComments = txtFCEComments.Text
                End If

                Dim StaffResponsible As Integer = CInt(cboReviewer.SelectedValue)
                Dim FCEOnSite As String = rdbFCEOnSite.Checked.ToString

                If txtFCENumber.Text = "" Then
                    SQL = "select MAX(STRFCENUMBER) from SSCPFCEMASTER"
                    FCENumber = DB.GetInteger(SQL) + 1

                    sqlList.Add("Insert into SSCPFCEMASTER " &
                        "(strFCENumber, strAIRSNumber, " &
                        "strModifingPerson, datModifingDate) " &
                        "values " &
                        "(@strFCENumber, @strAIRSNumber, " &
                        "@strModifingPerson, GETDATE() ) ")

                    paramList.Add({
                        New SqlParameter("@strFCENumber", FCENumber),
                        New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID)
                    })

                    sqlList.Add("Insert into SSCPFCE " &
                        "(strFCENumber, strFCEStatus, strReviewer, " &
                        "datFCECompleted, strFCEComments, strModifingPerson, " &
                        "datModifingDate, strSiteInspection, strFCEYear) " &
                        "values " &
                        "(@strFCENumber, 'True', @strReviewer, " &
                        "@datFCECompleted, @strFCEComments, @strModifingPerson, " &
                        "GETDATE(), @strSiteInspection, @strFCEYear) ")

                    paramList.Add({
                    New SqlParameter("@strFCENumber", FCENumber),
                    New SqlParameter("@strReviewer", StaffResponsible),
                    New SqlParameter("@datFCECompleted", FCECompleteDate),
                    New SqlParameter("@strFCEComments", FCEComments),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@strSiteInspection", FCEOnSite),
                    New SqlParameter("@strFCEYear", FCEYear.ToString)
                })

                    If facility.HeaderData.Classification = FacilityClassification.A OrElse
                    facility.HeaderData.Classification = FacilityClassification.SM Then

                        SQL = "Select strAFSActionNumber " &
                            "from APBSupplamentalData " &
                            "where strAIRSNumber = @airs "

                        Dim ActionNumber As Integer = DB.GetInteger(SQL, paramAirs)

                        sqlList.Add("Insert into AFSSSCPFCERecords " &
                            "(strFCENumber, strAFSActionNumber, " &
                            "strUpDateStatus, strModifingPerson, " &
                            "datModifingDate) " &
                            "values " &
                            "(@strFCENumber, @strAFSActionNumber, " &
                            "'A', @strModifingPerson, " &
                            "GETDATE()) ")

                        paramList.Add({
                            New SqlParameter("@strFCENumber", FCENumber),
                            New SqlParameter("@strAFSActionNumber", ActionNumber),
                            New SqlParameter("@strModifingPerson", CurrentUser.UserID)
                        })

                        sqlList.Add("Update APBSupplamentalData set " &
                            "strAFSActionNUmber = @strAFSActionNUmber " &
                            "where strAIRSNumber = @airs ")

                        paramList.Add({
                            New SqlParameter("@strAFSActionNUmber", ActionNumber + 1),
                            paramAirs
                        })
                    End If
                Else
                    FCENumber = CInt(txtFCENumber.Text)

                    sqlList.Add("Update SSCPFCEMaster set " &
                        "strModifingPerson = @strModifingPerson, " &
                        "datModifingDate =  GETDATE()  " &
                        "where strFCENumber = @strFCENumber ")

                    paramList.Add({
                        New SqlParameter("@strFCENumber", FCENumber),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID)
                    })


                    sqlList.Add("Update SSCPFCE Set " &
                        "strFCEStatus = 'True', " &
                        "strReviewer = @strReviewer, " &
                        "DatFCECompleted = @DatFCECompleted, " &
                        "strFCEComments = @strFCEComments, " &
                        "strModifingPerson = @strModifingPerson, " &
                        "datModifingDate =  GETDATE(), " &
                        "strSiteInspection = @strSiteInspection, " &
                        "strFCEYear = @strFCEYear " &
                        "where strFCENumber = @strFCENumber")

                    paramList.Add({
                        New SqlParameter("@strReviewer", StaffResponsible),
                        New SqlParameter("@DatFCECompleted", FCECompleteDate),
                        New SqlParameter("@strFCEComments", FCEComments),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                        New SqlParameter("@strSiteInspection", FCEOnSite),
                        New SqlParameter("@strFCEYear", FCEYear),
                        New SqlParameter("@strFCENumber", FCENumber)
                    })
                End If

                DB.RunCommand(sqlList, paramList)

                RemoveHandler cboFCEYear.SelectedIndexChanged, AddressOf cboFCEYear_SelectedIndexChanged
                LoadFCEDataset()
                FillFCEData()
                SetFceYear(FCEYear)
                MsgBox("FCE Saved", MsgBoxStyle.Information, "Full Compliance Evaluation")
            End If
        Catch ex As Exception
            ErrorReport(ex, txtFCENumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Open Supporting Documents"

    Private Sub llbFCEInspections_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFCEInspections.LinkClicked
        OpenFormSscpWorkItem(txtInspectionTrackingNumber.Text)
    End Sub

    Private Sub llbFCEACC_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFCEACC.LinkClicked
        OpenFormSscpWorkItem(txtACCTrackingNumber.Text)
    End Sub

    Private Sub llbFCEReports_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFCEReports.LinkClicked
        OpenFormSscpWorkItem(txtReportTrackingNumber.Text)
    End Sub

    Private Sub llbPerformanceTests_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbPerformanceTests.LinkClicked
        OpenFormSscpWorkItem(txtPerformanceTests.Text)
    End Sub

    Private Sub llbNotification_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbNotification.LinkClicked
        OpenFormSscpWorkItem(txtNotificationTrackingNumber.Text)
    End Sub

    Private Sub llbISMPSummaryReports_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbISMPSummaryReports.LinkClicked
        OpenFormTestReportPrintout(AirsNumber, txtISMPReferenceNumber.Text, Me)
    End Sub

    Private Sub llbFCEEnforcement_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFCEEnforcement.LinkClicked
        OpenFormEnforcement(txtEnforcement.Text)
    End Sub

#End Region

#Region "Print"

    Private Sub OpenFceReport()
        If String.IsNullOrWhiteSpace(txtFCENumber.Text) Then
            MessageBox.Show("Select an existing FCE before printing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        OpenFceUrl(AirsNumber, txtFCENumber.Text, Me)
    End Sub

#End Region

#Region "Toolbar"

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveFCE()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        OpenFceReport()
    End Sub

#End Region

#Region " Reset SQL Parameters "

    Private Sub SetSqlParameters()
        If AirsNumber IsNot Nothing Then
            paramAirs = New SqlParameter("@airs", AirsNumber.DbFormattedString)
            paramWithDates = {
                paramAirs,
                New SqlParameter("@startDate", DTPFilterStartDate.Value),
                New SqlParameter("@endDate", DTPFilterEndDate.Value)
            }
        End If
    End Sub

    Private Sub FilterDates_ValueChanged(sender As Object, e As EventArgs) Handles DTPFilterStartDate.ValueChanged, DTPFilterEndDate.ValueChanged
        SetSqlParameters()
    End Sub

#End Region

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If fceTable IsNot Nothing Then fceTable.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class