Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports EpdIt
Imports Iaip.Apb.Sscp
Imports Iaip.Apb.Sscp.WorkItem

Public Class SSCPEvents

#Region " Properties "

    Public Property TrackingNumber As Integer
    Private Property EventType As WorkItemEventType
    Private Property AirsNumber As Apb.ApbFacilityId
    Private Property ItemIsDeleted As Boolean = False

#End Region

#Region " Form load "

    Private Sub SSCPEvents_Load(sender As Object, e As EventArgs) Handles Me.Load
        DefaultDateTimePickers()
        LoadCombos()

        If AccountFormAccess(49, 2) = "1" Or AccountFormAccess(49, 3) = "1" Or AccountFormAccess(49, 4) = "1" Then
            ToolStrip1.Visible = True
            mmiSave.Visible = True
        Else
            ToolStrip1.Visible = False
            mmiSave.Visible = False
        End If
    End Sub

    Private Sub SSCPEvents_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ShowCorrectTab()
    End Sub

#End Region

#Region " Page Load Functions "

    Private Sub LoadCombos()
        With cboStaffResponsible
            .DataSource = GetSharedData(SharedTable.AllComplianceStaff)
            .DisplayMember = "StaffName"
            .ValueMember = "UserID"
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub ShowCorrectTab()
        Dim ReceivedDate As Date

        Dim SQL As String = "SELECT i.STRAIRSNUMBER, i.DATRECEIVEDDATE, i.STREVENTTYPE, i.DATCOMPLETEDATE, i.DATACKNOLEDGMENTLETTERSENT, " &
            "f.STRFACILITYNAME, f.STRFACILITYSTREET1, f.STRFACILITYCITY, f.STRFACILITYSTATE, f.STRFACILITYZIPCODE, l.STRCOUNTYNAME, " &
            "h.STRCLASS, h.STRAIRPROGRAMCODES, u.STRLASTNAME, u.STRFIRSTNAME, i.STRRESPONSIBLESTAFF, i.STRDELETE, s.STRRMPID, g.INSPECTION_ID " &
            "FROM SSCPITEMMASTER AS i " &
            "INNER JOIN APBFACILITYINFORMATION AS f ON f.STRAIRSNUMBER = i.STRAIRSNUMBER " &
            "INNER JOIN LOOKUPCOUNTYINFORMATION AS l ON l.STRCOUNTYCODE = SUBSTRING(i.STRAIRSNUMBER, 5, 3) " &
            "INNER JOIN APBHEADERDATA AS h ON h.STRAIRSNUMBER = i.STRAIRSNUMBER " &
            "INNER JOIN EPDUSERPROFILES AS u ON u.NUMUSERID = i.STRRESPONSIBLESTAFF " &
            "LEFT JOIN APBSUPPLAMENTALDATA AS s ON s.STRAIRSNUMBER = f.STRAIRSNUMBER " &
            "LEFT JOIN GEOS_INSPECTIONS_XREF AS g ON g.STRTRACKINGNUMBER = i.STRTRACKINGNUMBER " &
            "WHERE i.STRTRACKINGNUMBER = @num "

        Dim p As New SqlParameter("@num", TrackingNumber)

        Dim dr As DataRow = DB.GetDataRow(SQL, p)

        If dr Is Nothing Then
            MessageBox.Show("Item does Not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End If

        If IsDBNull(dr("strEventType")) Then
            MessageBox.Show("Item Is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        Else
            EventType = CType(CInt(dr.Item("strEventType")), WorkItemEventType)
        End If

        If IsDBNull(dr.Item("DatReceivedDate")) Then
            ReceivedDate = Today
        Else
            ReceivedDate = dr.Item("DatReceivedDate")
        End If

        If IsDBNull(dr.Item("strAIRSNumber")) Then
            AirsNumber = Nothing
        Else
            AirsNumber = New Apb.ApbFacilityId(dr.Item("strAIRSNumber"))
            txtFacilityInformation.Text = "AIRS # " & AirsNumber.FormattedString
        End If

        If IsDBNull(dr.Item("datAcknoledgmentLetterSent")) Then
            DTPAcknowledgmentLetterSent.Value = Today
            DTPAcknowledgmentLetterSent.Checked = False
        Else
            DTPAcknowledgmentLetterSent.Value = dr.Item("datAcknoledgmentlettersent")
            DTPAcknowledgmentLetterSent.Checked = True
        End If

        If Not IsDBNull(dr.Item("strDelete")) Then
            ItemIsDeleted = True
            txtFacilityInformation.Text = "FLAGGED As DELETED"
        End If

        txtFacilityInformation.Text = txtFacilityInformation.Text & vbNewLine &
            dr.Item("strFacilityName") & vbNewLine &
            dr.Item("strFacilityStreet1") & vbNewLine &
            dr.Item("StrFacilityCity") & ", " & dr.Item("strFacilityState") & " " &
            Address.FormatPostalCode(dr.Item("strFacilityZipCode")) & vbNewLine & vbNewLine &
            "County - " & dr.Item("strCountyName")

        txtEventInformation.Text = "Tracking # " & TrackingNumber & vbNewLine &
            "Staff Responsible: " & dr.Item("strFirstName") & " " & dr.Item("strLastName") & vbNewLine &
            "Classification: " & dr.Item("strClass") & vbNewLine &
            "Air Program Codes: " & vbNewLine

        If Not IsDBNull(dr.Item("INSPECTION_ID")) Then
            txtEventInformation.Text = "GEOS Inspection ID " & dr.Item("INSPECTION_ID") & vbNewLine & txtEventInformation.Text
        End If

        If Not IsDBNull(dr.Item("strResponsibleStaff")) Then
            cboStaffResponsible.SelectedValue = dr.Item("strResponsibleStaff")
        End If

        If Not IsDBNull(dr.Item("strRMPID")) Then
            txtRMPID.Text = dr.Item("strRMPID")
        End If

        TCItems.TabPages.Clear()

        Select Case EventType

            Case WorkItemEventType.Report
                TCItems.TabPages.Add(TPReport)
                DTPReportReceivedDate.Value = ReceivedDate
                AddReportsCombo()
                LoadReport()
                LoadReportSubmittalDGR()
                FormatReportsDGR()

            Case WorkItemEventType.Inspection
                TCItems.TabPages.Add(TPInspection)
                DTPInspectionDateStart.Value = ReceivedDate
                DTPInspectionDateEnd.Value = ReceivedDate
                FillInspectionCombos()
                LoadInspection()

            Case WorkItemEventType.StackTest
                TCItems.TabPages.Add(TPTestReports)
                txtTestReportReceivedbySSCPDate.Text = ReceivedDate
                LoadTestReport()

            Case WorkItemEventType.TvAcc
                TCItems.TabPages.Add(TPACC)
                DTPACCReceivedDate.Value = ReceivedDate
                DTPACCPostmarked.Value = Today
                dtpAccReportingYear.Value = Today.AddYears(-1)
                dtpAccReportingYear.Checked = True
                LoadACC()
                LoadACCSubmittalDGR()
                FormatACCDGR()
                btnPrint.Visible = True

            Case WorkItemEventType.Notification
                TCItems.TabPages.Add(TPNotifications)
                DTPNotificationReceived.Value = ReceivedDate
                FillNotificationCombos()
                LoadNotification()
                btnEnforcementProcess.Visible = False

            Case WorkItemEventType.RmpInspection
                TCItems.TabPages.Add(TPInspection)
                TPInspection.Text = "Risk Mgmt. Plan Inspection"
                DTPInspectionDateStart.Value = ReceivedDate
                DTPInspectionDateEnd.Value = ReceivedDate
                FillInspectionCombos()
                LoadInspection()

            Case Else

        End Select

        AddAirProgramCodes(dr.Item("StrAirProgramCodes"))
        CheckCompleteDate()
        CompleteReport()
        DisplayEnforcementCases()

    End Sub

    Private Sub AddReportsCombo()
        cboReportSchedule.Items.Add("")
        cboReportSchedule.Items.Add("First Quarter")
        cboReportSchedule.Items.Add("Second Quarter")
        cboReportSchedule.Items.Add("Third Quarter")
        cboReportSchedule.Items.Add("Fourth Quarter")
        cboReportSchedule.Items.Add("First Semiannual")
        cboReportSchedule.Items.Add("Second Semiannual")
        cboReportSchedule.Items.Add("Annual")
        cboReportSchedule.Items.Add("Other")
        cboReportSchedule.Items.Add("Monthly")
        cboReportSchedule.Items.Add("Malfunction/Deviation")

    End Sub

    Private Sub FillInspectionCombos()

        cboInspectionComplianceStatus.Items.Add("")
        cboInspectionComplianceStatus.Items.Add("Compliant")
        cboInspectionComplianceStatus.Items.Add("Deviation(s) Noted")

        cboInspectionReason.Items.Add("")
        cboInspectionReason.Items.Add("Planned Unannounced")
        cboInspectionReason.Items.Add("Planned Announced")
        cboInspectionReason.Items.Add("Unplanned")
        cboInspectionReason.Items.Add("Complaint Investigation")
        cboInspectionReason.Items.Add("Joint EPD/EPA")
        cboInspectionReason.Items.Add("Multimedia")
        cboInspectionReason.Items.Add("Follow Up")

    End Sub

    Private Sub FillNotificationCombos()
        With cboNotificationType
            .DataSource = GetSharedData(SharedTable.SscpNotificationTypes)
            .DisplayMember = "STRNOTIFICATIONDESC"
            .ValueMember = "STRNOTIFICATIONKEY"
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Dim AirList As String = ""

        If Mid(AirProgramCode, 1, 1) = 1 Then
            AirList = vbTab & "SIP" & vbCrLf
        End If
        If Mid(AirProgramCode, 2, 1) = 1 Then
            AirList = AirList & vbTab & "Federal SIP" & vbCrLf
        End If
        If Mid(AirProgramCode, 3, 1) = 1 Then
            AirList = AirList & vbTab & "Non-Federal SIP" & vbCrLf
        End If
        If Mid(AirProgramCode, 4, 1) = 1 Then
            AirList = AirList & vbTab & "CFC Tracking" & vbCrLf
        End If
        If Mid(AirProgramCode, 5, 1) = 1 Then
            AirList = AirList & vbTab & "PSD" & vbCrLf
        End If
        If Mid(AirProgramCode, 6, 1) = 1 Then
            AirList = AirList & vbTab & "NSR" & vbCrLf
        End If
        If Mid(AirProgramCode, 7, 1) = 1 Then
            AirList = AirList & vbTab & "NESHAP" & vbCrLf
        End If
        If Mid(AirProgramCode, 8, 1) = 1 Then
            AirList = AirList & vbTab & "NSPS" & vbCrLf
        End If
        If Mid(AirProgramCode, 9, 1) = 1 Then
            AirList = AirList & vbTab & "FESOP" & vbCrLf
        End If
        If Mid(AirProgramCode, 10, 1) = 1 Then
            AirList = AirList & vbTab & "Acid Precipitation" & vbCrLf
        End If
        If Mid(AirProgramCode, 11, 1) = 1 Then
            AirList = AirList & vbTab & "Native American" & vbCrLf
        End If
        If Mid(AirProgramCode, 12, 1) = 1 Then
            AirList = AirList & vbTab & "MACT" & vbCrLf
        End If
        If Mid(AirProgramCode, 13, 1) = 1 Then
            AirList = AirList & vbTab & "Title V Permit" & vbCrLf
        End If
        If Mid(AirProgramCode, 14, 1) = 1 Then
            AirList = AirList & vbTab & "Risk Mgmt. Plan" & vbCrLf
        End If
        If AirList = "" Then
            AirList = vbTab & "No Air Program Codes available" & vbCrLf
        End If
        AirList = Mid(AirList, 1, (Len(AirList) - 2))

        txtEventInformation.Text = txtEventInformation.Text & AirList
    End Sub

    Private Sub DefaultDateTimePickers()
        DTPAcknowledgmentLetterSent.Value = Today
        DTPReportPeriodStart.Value = Today
        DTPReportPeriodEnd.Value = Today
        dtpDueDate.Value = Today
        DTPSentDate.Value = Today
        DTPInspectionDateStart.Value = Today
        DTPInspectionDateEnd.Value = Today
        DTPTestReportDueDate.Value = Today
        DTPTestReportNewDueDate.Value = Today
        NUPReportSubmittal.Value = 1
        NUPACCSubmittal.Value = 1
    End Sub

    Private Sub CheckCompleteDate()
        Dim SQL As String = "Select datCompleteDate " &
            "from SSCPItemMaster " &
            "where strTrackingNumber = @num "
        Dim p As New SqlParameter("@num", TrackingNumber)

        Dim dr As DataRow = DB.GetDataRow(SQL, p)

        If dr IsNot Nothing Then
            If IsDBNull(dr.Item("datCompleteDate")) Then
                chbEventComplete.Checked = False
                DTPEventCompleteDate.Value = Today
            Else
                chbEventComplete.Checked = True
                DTPEventCompleteDate.Value = dr.Item("datCompleteDate")
            End If
        End If
    End Sub

    Private Sub CompleteReport()
        If chbEventComplete.Checked = True Then
            DTPAcknowledgmentLetterSent.Enabled = False
            chbNotificationReceivedByAPB.Enabled = False
            cboStaffResponsible.Enabled = False

            'Report
            DTPEventCompleteDate.Enabled = False
            NUPReportSubmittal.Enabled = False
            cboReportSchedule.Enabled = False
            DTPReportPeriodStart.Enabled = False
            DTPReportPeriodEnd.Enabled = False
            txtReportPeriodComments.ReadOnly = True
            dtpDueDate.Enabled = False
            DTPSentDate.Enabled = False
            rdbReportCompleteYes.Enabled = False
            rdbReportCompleteNo.Enabled = False
            rdbReportEnforcementYes.Enabled = False
            rdbReportEnforcementNo.Enabled = False
            rdbReportDeviationYes.Enabled = False
            rdbReportDeviationNo.Enabled = False
            txtReportsGeneralComments.ReadOnly = True

            'Test Report
            txtISMPReferenceNumber.ReadOnly = True
            txtPollutantTested.ReadOnly = True
            txtUnitTested.ReadOnly = True
            chbTestReportChangeDueDate.Enabled = False
            DTPTestReportDueDate.Enabled = False
            txtTestReportComments.ReadOnly = True
            DTPTestReportNewDueDate.Enabled = False
            rdbTestReportFollowUpYes.Enabled = False
            rdbTestReportFollowUpNo.Enabled = False

            'Inspection
            DTPInspectionDateStart.Enabled = False
            DTPInspectionDateEnd.Enabled = False
            dtpInspectionTimeStart.Enabled = False
            dtpInspectionTimeEnd.Enabled = False
            cboInspectionReason.Enabled = False
            txtWeatherConditions.ReadOnly = True
            txtInspectionGuide.ReadOnly = True
            rdbInspectionFacilityOperatingYes.Enabled = False
            rdbInspectionFacilityOperatingNo.Enabled = False
            cboInspectionComplianceStatus.Enabled = False
            txtInspectionConclusion.ReadOnly = True
            rdbInspectionFollowUpYes.Enabled = False
            rdbInspectionFollowUpNo.Enabled = False

            'Notifications
            dtpNotificationDate.Enabled = False
            dtpNotificationDate2.Enabled = False
            cboNotificationType.Enabled = False
            txtNotificationTypeOther.ReadOnly = True
            txtNotificationComments.ReadOnly = True
            rdbNotificationFollowUpYes.Enabled = False
            rdbNotificationFollowUpNo.Enabled = False

            'ACC
            NUPACCSubmittal.Enabled = False
            rdbACCPostmarkYes.Enabled = False
            rdbACCPostmarkNo.Enabled = False
            rdbACCROYes.Enabled = False
            rdbACCRONo.Enabled = False
            rdbACCCorrectACCYes.Enabled = False
            rdbACCCorrectACCNo.Enabled = False
            rdbACCConditionsYes.Enabled = False
            rdbACCConditionsNo.Enabled = False
            rdbACCCorrectYes.Enabled = False
            rdbACCCorrectNo.Enabled = False
            rdbACCDeviationsReportedYes.Enabled = False
            rdbACCDeviationsReportedNo.Enabled = False
            rdbACCPreviouslyUnreportedDeviationsYes.Enabled = False
            rdbACCPreviouslyUnreportedDeviationsNo.Enabled = False
            DTPACCPostmarked.Enabled = False
            dtpAccReportingYear.Enabled = False
            txtACCComments.ReadOnly = True
            rdbACCEnforcementNeededYes.Enabled = False
            rdbACCEnforcementNeededNo.Enabled = False
            rdbACCResubmittalRequestedYes.Enabled = False
            rdbACCResubmittalRequestedNo.Enabled = False
            rdbACCResubmittalRequestedUnknown.Enabled = False
            rdbACCAllDeviationsReportedYes.Enabled = False
            rdbACCAllDeviationsReportedNo.Enabled = False
            rdbACCAllDeviationsReportedUnknown.Enabled = False
        Else
            DTPAcknowledgmentLetterSent.Enabled = True
            chbNotificationReceivedByAPB.Enabled = True
            cboStaffResponsible.Enabled = True

            'Report
            DTPEventCompleteDate.Enabled = True
            NUPReportSubmittal.Enabled = True
            cboReportSchedule.Enabled = True
            DTPReportPeriodStart.Enabled = True
            DTPReportPeriodEnd.Enabled = True
            txtReportPeriodComments.ReadOnly = False
            dtpDueDate.Enabled = True
            DTPSentDate.Enabled = True
            rdbReportCompleteYes.Enabled = True
            rdbReportCompleteNo.Enabled = True
            rdbReportEnforcementYes.Enabled = True
            rdbReportEnforcementNo.Enabled = True
            rdbReportDeviationYes.Enabled = True
            rdbReportDeviationNo.Enabled = True
            txtReportsGeneralComments.ReadOnly = False

            'Test Report
            txtISMPReferenceNumber.ReadOnly = False
            txtPollutantTested.ReadOnly = False
            txtUnitTested.ReadOnly = False
            chbTestReportChangeDueDate.Enabled = True
            DTPTestReportDueDate.Enabled = True
            txtTestReportComments.ReadOnly = False
            DTPTestReportNewDueDate.Enabled = True
            rdbTestReportFollowUpYes.Enabled = True
            rdbTestReportFollowUpNo.Enabled = True

            'Inspection
            DTPInspectionDateStart.Enabled = True
            DTPInspectionDateEnd.Enabled = True
            dtpInspectionTimeStart.Enabled = True
            dtpInspectionTimeEnd.Enabled = True
            cboInspectionReason.Enabled = True
            txtWeatherConditions.ReadOnly = False
            txtInspectionGuide.ReadOnly = False
            rdbInspectionFacilityOperatingYes.Enabled = True
            rdbInspectionFacilityOperatingNo.Enabled = True
            cboInspectionComplianceStatus.Enabled = True
            txtInspectionConclusion.ReadOnly = False
            rdbInspectionFollowUpYes.Enabled = True
            rdbInspectionFollowUpNo.Enabled = True

            'Notifications
            dtpNotificationDate.Enabled = True
            dtpNotificationDate2.Enabled = True
            cboNotificationType.Enabled = True
            txtNotificationTypeOther.ReadOnly = False
            txtNotificationComments.ReadOnly = False
            rdbNotificationFollowUpYes.Enabled = True
            rdbNotificationFollowUpNo.Enabled = True

            'ACC
            NUPACCSubmittal.Enabled = True
            DTPACCPostmarked.Enabled = True
            dtpAccReportingYear.Enabled = True
            txtACCComments.ReadOnly = False

            If NUPACCSubmittal.Value > 1 Then
                rdbACCPostmarkYes.Enabled = False
                rdbACCPostmarkNo.Enabled = False
                rdbACCROYes.Enabled = False
                rdbACCRONo.Enabled = False
                rdbACCCorrectACCYes.Enabled = False
                rdbACCCorrectACCNo.Enabled = False
                rdbACCConditionsYes.Enabled = False
                rdbACCConditionsNo.Enabled = False
                rdbACCCorrectYes.Enabled = False
                rdbACCCorrectNo.Enabled = False
                rdbACCDeviationsReportedYes.Enabled = False
                rdbACCDeviationsReportedNo.Enabled = False
                rdbACCPreviouslyUnreportedDeviationsYes.Enabled = False
                rdbACCPreviouslyUnreportedDeviationsNo.Enabled = False
                rdbACCEnforcementNeededYes.Enabled = False
                rdbACCEnforcementNeededNo.Enabled = False
                rdbACCResubmittalRequestedYes.Enabled = False
                rdbACCAllDeviationsReportedUnknown.Enabled = False
                rdbACCResubmittalRequestedNo.Enabled = False
                rdbACCAllDeviationsReportedYes.Enabled = False
                rdbACCAllDeviationsReportedUnknown.Enabled = False
                rdbACCAllDeviationsReportedNo.Enabled = False
            Else
                rdbACCPostmarkYes.Enabled = True
                rdbACCPostmarkNo.Enabled = True
                rdbACCROYes.Enabled = True
                rdbACCRONo.Enabled = True
                rdbACCCorrectACCYes.Enabled = True
                rdbACCCorrectACCNo.Enabled = True
                rdbACCConditionsYes.Enabled = True
                rdbACCConditionsNo.Enabled = True
                rdbACCCorrectYes.Enabled = True
                rdbACCCorrectNo.Enabled = True
                rdbACCDeviationsReportedYes.Enabled = True
                rdbACCDeviationsReportedNo.Enabled = True
                rdbACCPreviouslyUnreportedDeviationsYes.Enabled = True
                rdbACCPreviouslyUnreportedDeviationsNo.Enabled = True
                rdbACCEnforcementNeededYes.Enabled = True
                rdbACCEnforcementNeededNo.Enabled = True
                rdbACCResubmittalRequestedYes.Enabled = True
                rdbACCResubmittalRequestedUnknown.Enabled = True
                rdbACCResubmittalRequestedNo.Enabled = True
                rdbACCAllDeviationsReportedYes.Enabled = True
                rdbACCAllDeviationsReportedNo.Enabled = True
                rdbACCAllDeviationsReportedUnknown.Enabled = True
                DTPACCPostmarked.Enabled = True
                dtpAccReportingYear.Enabled = True
                chbACCReceivedByAPB.Enabled = True
            End If
        End If
    End Sub

    Private Sub DisplayEnforcementCases()
        Dim dt As DataTable = DAL.Sscp.GetAllEnforcementForTrackingNumber(TrackingNumber)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            llEnforcementCases.Links.Clear()
            Dim i As Integer = 0
            For Each row As DataRow In dt.Rows
                i += 1
                Dim enfNum As String = row(0).ToString()
                Dim start As Integer = llEnforcementCases.Text.Length + 1
                Dim linkLength As Integer = enfNum.Length
                llEnforcementCases.Text &= " " & enfNum
                llEnforcementCases.Links.Add(start, linkLength, enfNum)
                If i < dt.Rows.Count Then
                    llEnforcementCases.Text &= ","
                End If
            Next
            btnEnforcementProcess.Visible = False
            llEnforcementCases.Visible = True
            llEnforcementCases.TabStop = True
        Else
            llEnforcementCases.Visible = False
            llEnforcementCases.Text = "Enforcement cases:"
        End If
    End Sub

#End Region

#Region " Enforcement Actions "

    Private Sub llEnforcementCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llEnforcementCases.LinkClicked
        OpenFormEnforcement(e.Link.LinkData.ToString)
    End Sub

    Private Sub btnEnforcementProcess_Click(sender As Object, e As EventArgs) Handles btnEnforcementProcess.Click
        OpenFormEnforcement(AirsNumber, TrackingNumber)
    End Sub

#End Region

#Region " Saves "

    Private Sub SaveMaster()
        Try
            If AccountFormAccess(49, 2) = "0" And AccountFormAccess(49, 3) = "0" And AccountFormAccess(49, 4) = "0" Then
                MsgBox("You do not have sufficient permission to save Compliance Events.", MsgBoxStyle.Information, "Compliance Events")
            Else
                Dim result As Boolean = False
                Select Case EventType
                    Case WorkItemEventType.Report
                        result = SaveReport()
                        LoadReportSubmittalDGR()
                    Case WorkItemEventType.Inspection, WorkItemEventType.RmpInspection
                        result = SaveInspection()
                    Case WorkItemEventType.TvAcc
                        result = SaveACC()
                        LoadACCSubmittalDGR()
                    Case WorkItemEventType.StackTest
                        result = SaveISMPTestReport()
                    Case WorkItemEventType.Notification
                        If cboNotificationType.SelectedValue.ToString = "07" Or cboNotificationType.SelectedValue.ToString = "08" Then
                            MsgBox("Malfunctions/deviations are no longer saved as notifications." & vbCrLf &
                                   "Please save this as a Report.", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If
                        result = SaveNotifications()
                    Case WorkItemEventType.Unknown
                        Exit Sub
                End Select

                If result AndAlso SaveDate() Then
                    MsgBox("Save Complete", MsgBoxStyle.Information, "SSCP Events")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function SaveReport() As Boolean
        Dim PeriodComments As String
        Dim GeneralComments As String
        Dim sqlList As New List(Of String)
        Dim paramList As New List(Of SqlParameter())

        Try
            ValidateALLReport()

            If wrnCompleteReport.Visible = True Or wrnEnforcementNeeded.Visible = True _
                    Or wrnReportPeriod.Visible = True Or wrnShowDeviation.Visible = True _
                    Or wrnReportSubmittal.Visible = True Then
                MsgBox("Data not saved")
                Return False
            Else
                If txtReportPeriodComments.Text = "" Then
                    PeriodComments = "N/A"
                Else
                    PeriodComments = txtReportPeriodComments.Text
                End If
                If txtReportsGeneralComments.Text = "" Then
                    GeneralComments = "N/A"
                Else
                    GeneralComments = txtReportsGeneralComments.Text
                End If

                Dim SQL As String = "Select 1 from SSCPREports where strTrackingNumber = @num "
                Dim pTrkNum As New SqlParameter("@num", TrackingNumber)

                If Not DB.ValueExists(SQL, pTrkNum) Then
                    sqlList.Add("Insert into SSCPREports " &
                        "(strTrackingNumber, strReportPeriod, " &
                        "DatReportingPeriodStart, DatReportingPeriodEnd, " &
                        "strReportingPeriodComments, datreportduedate, " &
                        "datsentbyfacilitydate, strcompletestatus, " &
                        "strenforcementneeded, strshowdeviation, " &
                        "strgeneralcomments, strmodifingperson, " &
                        "datmodifingdate, strSubmittalNumber) " &
                        "values " &
                        "(@strTrackingNumber, @strReportPeriod, " &
                        "@DatReportingPeriodStart, @DatReportingPeriodEnd, " &
                        "@strReportingPeriodComments, @datreportduedate, " &
                        "@datsentbyfacilitydate, @strcompletestatus, " &
                        "@strenforcementneeded, @strshowdeviation, " &
                        "@strgeneralcomments, @strmodifingperson, " &
                        "GETDATE(), '1') ")

                    paramList.Add({
                        New SqlParameter("@strTrackingNumber", TrackingNumber),
                        New SqlParameter("@strReportPeriod", cboReportSchedule.Text),
                        New SqlParameter("@DatReportingPeriodStart", DTPReportPeriodStart.Value),
                        New SqlParameter("@DatReportingPeriodEnd", DTPReportPeriodEnd.Value),
                        New SqlParameter("@strReportingPeriodComments", PeriodComments),
                        New SqlParameter("@datreportduedate", dtpDueDate.Value),
                        New SqlParameter("@datsentbyfacilitydate", DTPSentDate.Value),
                        New SqlParameter("@strcompletestatus", rdbReportCompleteYes.Checked.ToString),
                        New SqlParameter("@strenforcementneeded", rdbReportEnforcementYes.Checked.ToString),
                        New SqlParameter("@strshowdeviation", rdbReportDeviationYes.Checked.ToString),
                        New SqlParameter("@strgeneralcomments", GeneralComments),
                        New SqlParameter("@strmodifingperson", CurrentUser.UserID)
                    })

                    sqlList.Add("Insert into SSCPREportsHistory " &
                        "(strTrackingNumber, strSubmittalNumber, " &
                        "strReportPeriod, DatReportingPeriodStart, " &
                        "DatReportingPeriodEnd, strReportingPeriodComments, " &
                        "datreportduedate, datsentbyfacilitydate, " &
                        "strcompletestatus, strenforcementneeded, " &
                        "strshowdeviation, strgeneralcomments, " &
                        "strmodifingperson, datmodifingdate) " &
                        "values " &
                        "(@num, '1', " &
                        "@strReportPeriod, @DatReportingPeriodStart, " &
                        "@DatReportingPeriodEnd, @strReportingPeriodComments, " &
                        "@datreportduedate, @datsentbyfacilitydate, " &
                        "@strcompletestatus, @strenforcementneeded, " &
                        "@strshowdeviation, @strgeneralcomments, " &
                        "@strmodifingperson, GETDATE()) ")

                    paramList.Add({
                        pTrkNum,
                        New SqlParameter("@strReportPeriod", cboReportSchedule.Text),
                        New SqlParameter("@DatReportingPeriodStart", DTPReportPeriodStart.Value),
                        New SqlParameter("@DatReportingPeriodEnd", DTPReportPeriodEnd.Value),
                        New SqlParameter("@strReportingPeriodComments", PeriodComments),
                        New SqlParameter("@datreportduedate", dtpDueDate.Value),
                        New SqlParameter("@datsentbyfacilitydate", DTPSentDate.Value),
                        New SqlParameter("@strcompletestatus", rdbReportCompleteYes.Checked.ToString),
                        New SqlParameter("@strenforcementneeded", rdbReportEnforcementYes.Checked.ToString),
                        New SqlParameter("@strshowdeviation", rdbReportDeviationYes.Checked.ToString),
                        New SqlParameter("@strgeneralcomments", GeneralComments),
                        New SqlParameter("@strmodifingperson", CurrentUser.UserID)
                    })

                    Return DB.RunCommand(sqlList, paramList)
                Else
                    sqlList.Add("Update SSCPREports set " &
                        "strSubmittalNumber = @strSubmittalNumber, " &
                        "strReportPeriod = @strReportPeriod, " &
                        "DatReportingPeriodStart = @DatReportingPeriodStart, " &
                        "DatReportingPeriodEnd = @DatReportingPeriodEnd, " &
                        "strReportingPeriodComments = @strReportingPeriodComments, " &
                        "datreportduedate = @datreportduedate, " &
                        "datsentbyfacilitydate = @datsentbyfacilitydate, " &
                        "strcompletestatus= @strcompletestatus, " &
                        "strenforcementneeded = @strenforcementneeded, " &
                        "strshowdeviation = @strshowdeviation, " &
                        "strgeneralcomments = @strgeneralcomments, " &
                        "strmodifingperson = @strmodifingperson, " &
                        "datmodifingdate = GETDATE() " &
                        "where strTrackingNumber = @num ")

                    paramList.Add({
                        New SqlParameter("@strSubmittalNumber", NUPReportSubmittal.Value),
                        New SqlParameter("@strReportPeriod", cboReportSchedule.Text),
                        New SqlParameter("@DatReportingPeriodStart", DTPReportPeriodStart.Value),
                        New SqlParameter("@DatReportingPeriodEnd", DTPReportPeriodEnd.Value),
                        New SqlParameter("@strReportingPeriodComments", PeriodComments),
                        New SqlParameter("@datreportduedate", dtpDueDate.Value),
                        New SqlParameter("@datsentbyfacilitydate", DTPSentDate.Value),
                        New SqlParameter("@strcompletestatus", rdbReportCompleteYes.Checked.ToString),
                        New SqlParameter("@strenforcementneeded", rdbReportEnforcementYes.Checked.ToString),
                        New SqlParameter("@strshowdeviation", rdbReportDeviationYes.Checked.ToString),
                        New SqlParameter("@strgeneralcomments", GeneralComments),
                        New SqlParameter("@strmodifingperson", CurrentUser.UserID),
                        pTrkNum
                    })

                    SQL = "Select 1 " &
                        "from SSCPREportsHistory " &
                        "where strTrackingNumber = @num " &
                        "and strSubmittalNumber = @strSubmittalNumber "

                    Dim p2 As SqlParameter() = {
                        pTrkNum,
                        New SqlParameter("@strSubmittalNumber", NUPReportSubmittal.Value)
                    }

                    If DB.ValueExists(SQL, p2) Then
                        sqlList.Add("Update SSCPREportsHistory set " &
                            "strReportPeriod = @strReportPeriod, " &
                            "DatReportingPeriodStart = @DatReportingPeriodStart, " &
                            "DatReportingPeriodEnd = @DatReportingPeriodEnd, " &
                            "strReportingPeriodComments = @strReportingPeriodComments, " &
                            "datreportduedate = @datreportduedate, " &
                            "datsentbyfacilitydate = @datsentbyfacilitydate, " &
                            "strcompletestatus= @strcompletestatus, " &
                            "strenforcementneeded = @strenforcementneeded, " &
                            "strshowdeviation = @strshowdeviation, " &
                            "strgeneralcomments = @strgeneralcomments, " &
                            "strmodifingperson = @strmodifingperson, " &
                            "datmodifingdate =  GETDATE()  " &
                            "where strTrackingNumber = @num " &
                            "and strSubmittalNumber = @strSubmittalNumber ")

                        paramList.Add({
                            New SqlParameter("@strReportPeriod", cboReportSchedule.Text),
                            New SqlParameter("@DatReportingPeriodStart", DTPReportPeriodStart.Value),
                            New SqlParameter("@DatReportingPeriodEnd", DTPReportPeriodEnd.Value),
                            New SqlParameter("@strReportingPeriodComments", PeriodComments),
                            New SqlParameter("@datreportduedate", dtpDueDate.Value),
                            New SqlParameter("@datsentbyfacilitydate", DTPSentDate.Value),
                            New SqlParameter("@strcompletestatus", rdbReportCompleteYes.Checked.ToString),
                            New SqlParameter("@strenforcementneeded", rdbReportEnforcementYes.Checked.ToString),
                            New SqlParameter("@strshowdeviation", rdbReportDeviationYes.Checked.ToString),
                            New SqlParameter("@strgeneralcomments", GeneralComments),
                            New SqlParameter("@strmodifingperson", CurrentUser.UserID),
                            pTrkNum,
                            New SqlParameter("@strSubmittalNumber", NUPReportSubmittal.Value)
                        })
                    Else
                        sqlList.Add("Insert into SSCPREportsHistory " &
                            "(strTrackingNumber, strSubmittalNumber, " &
                            "strReportPeriod, DatReportingPeriodStart, " &
                            "DatReportingPeriodEnd, strReportingPeriodComments, " &
                            "datreportduedate, datsentbyfacilitydate, " &
                            "strcompletestatus, strenforcementneeded, " &
                            "strshowdeviation, strgeneralcomments, " &
                            "strmodifingperson, datmodifingdate) " &
                            "values " &
                            "(@num, @strSubmittalNumber, " &
                            "@strReportPeriod, @DatReportingPeriodStart, " &
                            "@DatReportingPeriodEnd, @strReportingPeriodComments, " &
                            "@datreportduedate, @datsentbyfacilitydate, " &
                            "@strcompletestatus, @strenforcementneeded, " &
                            "@strshowdeviation, @strgeneralcomments, " &
                            "@strmodifingperson, GETDATE()) ")

                        paramList.Add({
                            pTrkNum,
                            New SqlParameter("@strSubmittalNumber", NUPReportSubmittal.Value),
                            New SqlParameter("@strReportPeriod", cboReportSchedule.Text),
                            New SqlParameter("@DatReportingPeriodStart", DTPReportPeriodStart.Value),
                            New SqlParameter("@DatReportingPeriodEnd", DTPReportPeriodEnd.Value),
                            New SqlParameter("@strReportingPeriodComments", PeriodComments),
                            New SqlParameter("@datreportduedate", dtpDueDate.Value),
                            New SqlParameter("@datsentbyfacilitydate", DTPSentDate.Value),
                            New SqlParameter("@strcompletestatus", rdbReportCompleteYes.Checked.ToString),
                            New SqlParameter("@strenforcementneeded", rdbReportEnforcementYes.Checked.ToString),
                            New SqlParameter("@strshowdeviation", rdbReportDeviationYes.Checked.ToString),
                            New SqlParameter("@strgeneralcomments", GeneralComments),
                            New SqlParameter("@strmodifingperson", CurrentUser.UserID)
                        })
                    End If
                End If

                If chbReportReceivedByAPB.Checked = True Then
                    sqlList.Add("Update SSCPItemMaster set " &
                            "datReceivedDate = @date " &
                            "where strTrackingNumber = @num ")

                    paramList.Add({
                            New SqlParameter("@date", DTPReportReceivedDate.Value),
                            pTrkNum
                        })
                End If

                Return DB.RunCommand(sqlList, paramList)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function SaveInspection() As Boolean
        Dim InspectionDateTimeStart As Date
        Dim InspectionDateTimeEnd As Date
        Dim InspectionGuide As String
        Dim InspectionComments As String
        Dim InspectionReason As String
        Dim WeatherCondition As String

        Try

            ValidateAllInspection()

            If wrnInspectionOperating.Visible = True _
                Or wrnInspectionComplianceStatus.Visible = True _
                Or wrnInspectionDates.Visible = True Then
                MsgBox("Data not saved")

                Return False
            Else
                If cboInspectionReason.Items.Contains(cboInspectionReason.Text) And cboInspectionReason.Text <> cboInspectionReason.Items.Item(0) Then
                    InspectionReason = cboInspectionReason.Text
                Else
                    InspectionReason = "N/A"
                End If
                If txtWeatherConditions.Text <> "" Then
                    WeatherCondition = txtWeatherConditions.Text
                Else
                    WeatherCondition = "N/A"
                End If
                If txtInspectionGuide.Text = "" Then
                    InspectionGuide = "N/A"
                Else
                    InspectionGuide = txtInspectionGuide.Text
                End If
                If txtInspectionConclusion.Text = "" Then
                    InspectionComments = "N/A"
                Else
                    InspectionComments = txtInspectionConclusion.Text
                End If

                InspectionDateTimeStart = New Date(DTPInspectionDateStart.Value.Year, DTPInspectionDateStart.Value.Month, DTPInspectionDateStart.Value.Day,
                                                   dtpInspectionTimeStart.Value.Hour, dtpInspectionTimeStart.Value.Minute, dtpInspectionTimeStart.Value.Second)
                InspectionDateTimeEnd = New Date(DTPInspectionDateEnd.Value.Year, DTPInspectionDateEnd.Value.Month, DTPInspectionDateEnd.Value.Day,
                                                   dtpInspectionTimeEnd.Value.Hour, dtpInspectionTimeEnd.Value.Minute, dtpInspectionTimeEnd.Value.Second)

                Dim SQL As String = "Select 1 from SSCPInspections where strTrackingNumber = @num"
                Dim p As New SqlParameter("@num", TrackingNumber)

                If Not DB.ValueExists(SQL, p) Then
                    SQL = "Insert into SSCPInspections " &
                        "(strTrackingNumber, DatInspectionDateStart, " &
                        "datinspectionDateEnd, strInspectionReason, " &
                        "strWeatherConditions, strInspectionGuide, " &
                        "strFacilityOperating, strInspectionComplianceStatus, " &
                        "strInspectionComments, " &
                        "strInspectionFollowUp, strModifingPerson, " &
                        "datModifingDate) " &
                        "values " &
                        "(@strTrackingNumber, @DatInspectionDateStart, " &
                        "@datinspectionDateEnd, @strInspectionReason, " &
                        "@strWeatherConditions, @strInspectionGuide, " &
                        "@strFacilityOperating, @strInspectionComplianceStatus, " &
                        "@strInspectionComments, " &
                        "@strInspectionFollowUp, @strModifingPerson, " &
                        "GETDATE() ) "
                Else
                    SQL = "Update SSCPInspections set " &
                        "DatInspectionDateStart = @DatInspectionDateStart, " &
                        "datinspectionDateEnd = @datinspectionDateEnd, " &
                        "strInspectionReason = @strInspectionReason, " &
                        "strWeatherConditions = @strWeatherConditions, " &
                        "strInspectionGuide = @strInspectionGuide, " &
                        "strFacilityOperating = @strFacilityOperating, " &
                        "strInspectionComplianceStatus = @strInspectionComplianceStatus, " &
                        "strInspectionComments = @strInspectionComments, " &
                        "strInspectionFollowUp = @strInspectionFollowUp, " &
                        "strModifingPerson = @strModifingPerson, " &
                        "datModifingDate =  GETDATE()  " &
                        "where strtrackingNumber = @strtrackingNumber "
                End If

                Dim p2 As SqlParameter() = {
                        New SqlParameter("@strTrackingNumber", TrackingNumber),
                        New SqlParameter("@DatInspectionDateStart", InspectionDateTimeStart),
                        New SqlParameter("@datinspectionDateEnd", InspectionDateTimeEnd),
                        New SqlParameter("@strInspectionReason", InspectionReason),
                        New SqlParameter("@strWeatherConditions", WeatherCondition),
                        New SqlParameter("@strInspectionGuide", InspectionGuide),
                        New SqlParameter("@strFacilityOperating", rdbInspectionFacilityOperatingYes.Checked.ToString),
                        New SqlParameter("@strInspectionComplianceStatus", cboInspectionComplianceStatus.Text),
                        New SqlParameter("@strInspectionComments", InspectionComments),
                        New SqlParameter("@strInspectionFollowUp", rdbInspectionFollowUpYes.Checked.ToString),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID)
                    }

                Return DB.RunCommand(SQL, p2)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function SaveACC() As Boolean
        Dim PostedOnTime As String
        Dim SignedByRO As String
        Dim CorrectACCForm As String
        Dim TitleVConditions As String
        Dim ACCCorrectlyFilledOut As String
        Dim ReportedDeviations As String
        Dim ReportedUnReportedDeviations As String
        Dim ACCComments As String
        Dim EnforcementNeeded As String
        Dim ResubmittalRequested As String
        Dim AllDeviationsReported As String
        Dim sqlList As New List(Of String)
        Dim paramList As New List(Of SqlParameter())
        Dim AccReportingYear As Object

        Try

            ValidateAllACC()

            If wrnACCConditions.Visible = True Or wrnACCCorrect.Visible = True _
            Or wrnACCCorrectACC.Visible = True _
            Or wrnACCDatePostmarked.Visible = True Or wrnACCDeviationsReported.Visible = True _
            Or wrnACCEnforcementNeeded.Visible = True Or wrnACCPostmark.Visible = True _
            Or wrnACCPreviousDeviations.Visible = True Or wrnACCAllDeviationsReported.Visible _
            Or wrnACCResubmittalRequested.Visible _
            Or wrnACCRO.Visible = True Or wrnACCSubmittal.Visible = True Then
                MsgBox("Data not saved", MsgBoxStyle.Information, "SSCP Events.")
                Return False
            End If

            If dtpAccReportingYear.Checked Then
                AccReportingYear = dtpAccReportingYear.Value
            Else
                AccReportingYear = DBNull.Value
            End If
            If rdbACCPostmarkYes.Checked = True Then
                PostedOnTime = "True"
            Else
                PostedOnTime = "False"
            End If
            If rdbACCROYes.Checked = True Then
                SignedByRO = "True"
            Else
                SignedByRO = "False"
            End If
            If rdbACCCorrectACCYes.Checked = True Then
                CorrectACCForm = "True"
            Else
                CorrectACCForm = "False"
            End If
            If rdbACCConditionsYes.Checked = True Then
                TitleVConditions = "True"
            Else
                TitleVConditions = "False"
            End If
            If rdbACCCorrectYes.Checked = True Then
                ACCCorrectlyFilledOut = "True"
            Else
                ACCCorrectlyFilledOut = "False"
            End If
            If rdbACCDeviationsReportedYes.Checked = True Then
                ReportedDeviations = "True"
            Else
                ReportedDeviations = "False"
            End If
            If rdbACCPreviouslyUnreportedDeviationsYes.Checked = True Then
                ReportedUnReportedDeviations = "True"
            Else
                ReportedUnReportedDeviations = "False"
            End If
            If txtACCComments.Text = "" Then
                ACCComments = "N/A"
            Else
                ACCComments = txtACCComments.Text
            End If
            If rdbACCEnforcementNeededYes.Checked = True Then
                EnforcementNeeded = "True"
            Else
                EnforcementNeeded = "False"
            End If
            If rdbACCAllDeviationsReportedYes.Checked Then
                AllDeviationsReported = "True"
            Else
                AllDeviationsReported = "False"
            End If
            If rdbACCResubmittalRequestedYes.Checked Then
                ResubmittalRequested = "True"
            Else
                ResubmittalRequested = "False"
            End If

            Dim SQL As String = "Select 1 from SSCPACCS where strTrackingNumber = @num"
            Dim p As New SqlParameter("@num", TrackingNumber)

            If Not DB.ValueExists(SQL, p) Then
                NUPACCSubmittal.Value = 1

                sqlList.Add("Insert into SSCPACCS " &
                    "(strTrackingNumber, strSubmittalNumber, " &
                    "strPostMarkedOnTime, DATPostMarkDate, " &
                    "strsignedbyRO, strCorrectACCFOrms, " &
                    "strTitleVConditionsListed, strACCCorrectlyFilledOut, " &
                    "strReportedDeviations, strDeviationsUnreported, " &
                    "strcomments, strEnforcementneeded, " &
                    "strModifingPerson, DatModifingDate, datAccReportingYear, " &
                    "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED) " &
                    "values " &
                    "(@strTrackingNumber, @strSubmittalNumber, " &
                    "@strPostMarkedOnTime, @DATPostMarkDate, " &
                    "@strsignedbyRO, @strCorrectACCFOrms, " &
                    "@strTitleVConditionsListed, @strACCCorrectlyFilledOut, " &
                    "@strReportedDeviations, @strDeviationsUnreported, " &
                    "@strcomments, @strEnforcementneeded, " &
                    "@strModifingPerson, GETDATE(), @datAccReportingYear, " &
                    "@STRKNOWNDEVIATIONSREPORTED, @STRRESUBMITTALREQUIRED) ")

                paramList.Add({
                    New SqlParameter("@strTrackingNumber", TrackingNumber),
                    New SqlParameter("@strSubmittalNumber", NUPACCSubmittal.Value),
                    New SqlParameter("@strPostMarkedOnTime", PostedOnTime),
                    New SqlParameter("@DATPostMarkDate", DTPACCPostmarked.Value),
                    New SqlParameter("@strsignedbyRO", SignedByRO),
                    New SqlParameter("@strCorrectACCFOrms", CorrectACCForm),
                    New SqlParameter("@strTitleVConditionsListed", TitleVConditions),
                    New SqlParameter("@strACCCorrectlyFilledOut", ACCCorrectlyFilledOut),
                    New SqlParameter("@strReportedDeviations", ReportedDeviations),
                    New SqlParameter("@strDeviationsUnreported", ReportedUnReportedDeviations),
                    New SqlParameter("@strcomments", ACCComments),
                    New SqlParameter("@strEnforcementneeded", EnforcementNeeded),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@datAccReportingYear", AccReportingYear),
                    New SqlParameter("@STRKNOWNDEVIATIONSREPORTED", AllDeviationsReported),
                    New SqlParameter("@STRRESUBMITTALREQUIRED", ResubmittalRequested)
                })

                sqlList.Add("Insert into SSCPACCSHistory " &
                    "(strTrackingNumber, strSubmittalNumber, " &
                    "strPostMarkedOnTime, DATPostMarkDate, " &
                    "strsignedbyRO, StrCorrectACCForms, " &
                    "strTitleVConditionsListed, strACCCorrectlyFilledOut, " &
                    "strReportedDeviations, strDeviationsUnreported, " &
                    "strcomments, strEnforcementneeded, " &
                    "strModifingPerson, DatModifingDate, datAccReportingYear, " &
                    "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED) " &
                    "values " &
                    "(@strTrackingNumber, @strSubmittalNumber, " &
                    "@strPostMarkedOnTime, @DATPostMarkDate, " &
                    "@strsignedbyRO, @StrCorrectACCForms, " &
                    "@strTitleVConditionsListed, @strACCCorrectlyFilledOut, " &
                    "@strReportedDeviations, @strDeviationsUnreported, " &
                    "@strcomments, @strEnforcementneeded, " &
                    "@strModifingPerson, GETDATE(), @datAccReportingYear, " &
                    "@STRKNOWNDEVIATIONSREPORTED, @STRRESUBMITTALREQUIRED) ")

                paramList.Add({
                    New SqlParameter("@strTrackingNumber", TrackingNumber),
                    New SqlParameter("@strSubmittalNumber", NUPACCSubmittal.Value),
                    New SqlParameter("@strPostMarkedOnTime", PostedOnTime),
                    New SqlParameter("@DATPostMarkDate", DTPACCPostmarked.Value),
                    New SqlParameter("@strsignedbyRO", SignedByRO),
                    New SqlParameter("@StrCorrectACCForms", CorrectACCForm),
                    New SqlParameter("@strTitleVConditionsListed", TitleVConditions),
                    New SqlParameter("@strACCCorrectlyFilledOut", ACCCorrectlyFilledOut),
                    New SqlParameter("@strReportedDeviations", ReportedDeviations),
                    New SqlParameter("@strDeviationsUnreported", ReportedUnReportedDeviations),
                    New SqlParameter("@strcomments", ACCComments),
                    New SqlParameter("@strEnforcementneeded", EnforcementNeeded),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@datAccReportingYear", AccReportingYear),
                    New SqlParameter("@STRKNOWNDEVIATIONSREPORTED", AllDeviationsReported),
                    New SqlParameter("@STRRESUBMITTALREQUIRED", ResubmittalRequested)
                })

            Else  'ValueExists = True 
                sqlList.Add("Update SSCPACCS set " &
                    "strSubmittalNumber = @strSubmittalNumber, " &
                    "strPostMarkedOnTime = @strPostMarkedOnTime, " &
                    "DATPostMarkDate = @DATPostMarkDate, " &
                    "strsignedbyRO = @strsignedbyRO, " &
                    "StrCorrectACCFOrms = @StrCorrectACCFOrms, " &
                    "strTitleVConditionsListed = @strTitleVConditionsListed, " &
                    "strACCCorrectlyFilledOut = @strACCCorrectlyFilledOut, " &
                    "strReportedDeviations = @strReportedDeviations, " &
                    "strDeviationsUnreported = @strDeviationsUnreported, " &
                    "strcomments = @strcomments, " &
                    "strEnforcementneeded = @strEnforcementneeded, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "DatModifingDate = GETDATE(), " &
                    "datAccReportingYear = @datAccReportingYear, " &
                    "STRKNOWNDEVIATIONSREPORTED = @STRKNOWNDEVIATIONSREPORTED, " &
                    "STRRESUBMITTALREQUIRED = @STRRESUBMITTALREQUIRED " &
                    "where strTrackingnumber = @strTrackingnumber ")

                paramList.Add({
                    New SqlParameter("@strTrackingNumber", TrackingNumber),
                    New SqlParameter("@strSubmittalNumber", NUPACCSubmittal.Value),
                    New SqlParameter("@strPostMarkedOnTime", PostedOnTime),
                    New SqlParameter("@DATPostMarkDate", DTPACCPostmarked.Value),
                    New SqlParameter("@strsignedbyRO", SignedByRO),
                    New SqlParameter("@StrCorrectACCForms", CorrectACCForm),
                    New SqlParameter("@strTitleVConditionsListed", TitleVConditions),
                    New SqlParameter("@strACCCorrectlyFilledOut", ACCCorrectlyFilledOut),
                    New SqlParameter("@strReportedDeviations", ReportedDeviations),
                    New SqlParameter("@strDeviationsUnreported", ReportedUnReportedDeviations),
                    New SqlParameter("@strcomments", ACCComments),
                    New SqlParameter("@strEnforcementneeded", EnforcementNeeded),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@datAccReportingYear", AccReportingYear),
                    New SqlParameter("@STRKNOWNDEVIATIONSREPORTED", AllDeviationsReported),
                    New SqlParameter("@STRRESUBMITTALREQUIRED", ResubmittalRequested)
                })

                SQL = "Select 1 from SSCPACCSHistory where strTrackingNumber = @num and strSubmittalNumber = @sub "

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@num", TrackingNumber),
                    New SqlParameter("@sub", NUPACCSubmittal.Text)
                }

                If DB.ValueExists(SQL, p2) Then
                    sqlList.Add("Update SSCPACCSHistory set " &
                        "strSubmittalNumber = @strSubmittalNumber, " &
                        "strPostMarkedOnTime = @strPostMarkedOnTime, " &
                        "DATPostMarkDate = @DATPostMarkDate, " &
                        "strsignedbyRO = @strsignedbyRO, " &
                        "StrCorrectACCFOrms = @StrCorrectACCFOrms, " &
                        "strTitleVConditionsListed = @strTitleVConditionsListed, " &
                        "strACCCorrectlyFilledOut = @strACCCorrectlyFilledOut, " &
                        "strReportedDeviations = @strReportedDeviations, " &
                        "strDeviationsUnreported = @strDeviationsUnreported, " &
                        "strcomments = @strcomments, " &
                        "strEnforcementneeded = @strEnforcementneeded, " &
                        "strModifingPerson = @strModifingPerson, " &
                        "DatModifingDate = GETDATE(), " &
                        "datAccReportingYear = @datAccReportingYear, " &
                        "STRKNOWNDEVIATIONSREPORTED = @STRKNOWNDEVIATIONSREPORTED, " &
                        "STRRESUBMITTALREQUIRED = @STRRESUBMITTALREQUIRED " &
                        "where strTrackingnumber = @strTrackingnumber " &
                        "and strSubmittalNumber = @strSubmittalNumber ")

                    paramList.Add({
                        New SqlParameter("@strTrackingNumber", TrackingNumber),
                        New SqlParameter("@strSubmittalNumber", NUPACCSubmittal.Value),
                        New SqlParameter("@strPostMarkedOnTime", PostedOnTime),
                        New SqlParameter("@DATPostMarkDate", DTPACCPostmarked.Value),
                        New SqlParameter("@strsignedbyRO", SignedByRO),
                        New SqlParameter("@StrCorrectACCForms", CorrectACCForm),
                        New SqlParameter("@strTitleVConditionsListed", TitleVConditions),
                        New SqlParameter("@strACCCorrectlyFilledOut", ACCCorrectlyFilledOut),
                        New SqlParameter("@strReportedDeviations", ReportedDeviations),
                        New SqlParameter("@strDeviationsUnreported", ReportedUnReportedDeviations),
                        New SqlParameter("@strcomments", ACCComments),
                        New SqlParameter("@strEnforcementneeded", EnforcementNeeded),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                        New SqlParameter("@datAccReportingYear", AccReportingYear),
                        New SqlParameter("@STRKNOWNDEVIATIONSREPORTED", AllDeviationsReported),
                        New SqlParameter("@STRRESUBMITTALREQUIRED", ResubmittalRequested)
                    })
                Else
                    sqlList.Add("Insert into SSCPACCSHistory " &
                        "(strTrackingNumber, strSubmittalNumber, " &
                        "strPostMarkedOnTime, DATPostMarkDate, " &
                        "strsignedbyRO, StrCorrectACCForms, " &
                        "strTitleVConditionsListed, strACCCorrectlyFilledOut, " &
                        "strReportedDeviations, strDeviationsUnreported, " &
                        "strcomments, strEnforcementneeded, " &
                        "strModifingPerson, DatModifingDate, datAccReportingYear, " &
                        "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED) " &
                        "values " &
                        "(@strTrackingNumber, @strSubmittalNumber, " &
                        "@strPostMarkedOnTime, @DATPostMarkDate, " &
                        "@strsignedbyRO, @StrCorrectACCForms, " &
                        "@strTitleVConditionsListed, @strACCCorrectlyFilledOut, " &
                        "@strReportedDeviations, @strDeviationsUnreported, " &
                        "@strcomments, @strEnforcementneeded, " &
                        "@strModifingPerson, GETDATE(), @datAccReportingYear, " &
                        "@STRKNOWNDEVIATIONSREPORTED, @STRRESUBMITTALREQUIRED) ")

                    paramList.Add({
                        New SqlParameter("@strTrackingNumber", TrackingNumber),
                        New SqlParameter("@strSubmittalNumber", NUPACCSubmittal.Value),
                        New SqlParameter("@strPostMarkedOnTime", PostedOnTime),
                        New SqlParameter("@DATPostMarkDate", DTPACCPostmarked.Value),
                        New SqlParameter("@strsignedbyRO", SignedByRO),
                        New SqlParameter("@StrCorrectACCForms", CorrectACCForm),
                        New SqlParameter("@strTitleVConditionsListed", TitleVConditions),
                        New SqlParameter("@strACCCorrectlyFilledOut", ACCCorrectlyFilledOut),
                        New SqlParameter("@strReportedDeviations", ReportedDeviations),
                        New SqlParameter("@strDeviationsUnreported", ReportedUnReportedDeviations),
                        New SqlParameter("@strcomments", ACCComments),
                        New SqlParameter("@strEnforcementneeded", EnforcementNeeded),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                        New SqlParameter("@datAccReportingYear", AccReportingYear),
                        New SqlParameter("@STRKNOWNDEVIATIONSREPORTED", AllDeviationsReported),
                        New SqlParameter("@STRRESUBMITTALREQUIRED", ResubmittalRequested)
                    })
                End If

                If chbACCReceivedByAPB.Checked = True Then
                    sqlList.Add("Update SSCPItemMaster set " &
                        "datReceivedDate = @date " &
                        "where strTrackingNumber = @num ")

                    paramList.Add({
                        New SqlParameter("@date", DTPACCReceivedDate.Value),
                        New SqlParameter("@num", TrackingNumber)
                    })
                End If

            End If 'If ValueExists in the SSCPACCS table

            Return DB.RunCommand(sqlList, paramList)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function SaveISMPTestReport() As Boolean
        Dim TestReportDue As String
        Dim TestReportComments As String
        Dim TestReportFollowUp As String
        Dim ReferenceNumber As String
        Dim sqlList As New List(Of String)
        Dim plist As New List(Of SqlParameter())

        Try
            If txtISMPReferenceNumber.Text = "" Then
                txtISMPReferenceNumber.Text = "N/A"
            End If
            If rdbTestReportFollowUpYes.Checked = True Then
                TestReportFollowUp = "True"
            Else
                TestReportFollowUp = "False"
            End If
            If txtTestReportDueDate.Text = "Unknown" Then
                TestReportDue = "04-Jul-1776"
            Else
                TestReportDue = txtTestReportDueDate.Text
            End If
            If txtTestReportComments.Text = "" Then
                TestReportComments = "N/A"
            Else
                TestReportComments = txtTestReportComments.Text
            End If
            If txtISMPReferenceNumber.Text = "" Then
                ReferenceNumber = "N/A"
            Else
                ReferenceNumber = txtISMPReferenceNumber.Text
            End If

            Dim SQL As String = "Select convert(bit, count(*)) from SSCPTestReports where strTrackingNumber = @num"

            Dim p As New SqlParameter("@num", TrackingNumber)

            If DB.GetBoolean(SQL, p) Then
                sqlList.Add("Update SSCPTestReports set " &
                    "strReferenceNumber = @strReferenceNumber, " &
                    "datTestReportDue = @datTestReportDue, " &
                    "strTestReportComments = @strTestReportComments, " &
                    "strTestReportFollowUp = @strTestReportFollowUp, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingDate =  GETDATE() " &
                    "where strTrackingNumber = @num")
            Else
                sqlList.Add("Insert into SSCPTestReports " &
                    "(strTrackingNumber, strReferenceNumber, " &
                    "datTestReportDue, " &
                    "strTestReportComments, strTestReportFollowUp, " &
                    "strModifingPerson, datModifingDate) " &
                    "Values " &
                    "(@num, @strReferenceNumber, " &
                    "@datTestReportDue, " &
                    "@strTestReportComments, @strTestReportFollowUp, " &
                    "@strModifingPerson, GETDATE() ) ")
            End If
            plist.Add({
                New SqlParameter("@strReferenceNumber", ReferenceNumber),
                New SqlParameter("@datTestReportDue", TestReportDue),
                New SqlParameter("@strTestReportComments", TestReportComments),
                New SqlParameter("@strTestReportFollowUp", TestReportFollowUp),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@num", TrackingNumber)
            })

            SQL = "Select 1 from APBSupplamentalData where strAIRSNumber = @airs "

            Dim p2 As New SqlParameter("@airs", AirsNumber.DbFormattedString)

            If DB.ValueExists(SQL, p2) Then
                sqlList.Add("Update APBSupplamentalData set " &
                            "datSSCPTestReportDue = @date " &
                            "where strAIRSNUmber = @airs ")

                plist.Add({
                    New SqlParameter("@date", DTPTestReportNewDueDate.Value),
                    New SqlParameter("@airs", AirsNumber.DbFormattedString)
                })
            End If

            If chbISMPTestReportReceivedByAPB.Checked = True Then
                sqlList.Add("Update SSCPItemMaster set " &
                    "datReceivedDate = @date " &
                    "where strTrackingNumber = @num")

                plist.Add({
                    New SqlParameter("@date", DTPTestReportReceivedDate.Value),
                    New SqlParameter("@num", TrackingNumber)
                })
            End If

            Return DB.RunCommand(sqlList, plist)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function SaveNotifications() As Boolean
        Dim NotificationDue As String = "True"
        Dim NotificationDueDate As Object
        Dim NotificationSent As String = "True"
        Dim NotificationSentDate As Object
        Dim NotificationTypeOther As String
        Dim NotificationComment As String
        Dim NotificationFollowUp As String
        Dim sqlList As New List(Of String)
        Dim plist As New List(Of SqlParameter())

        Try

            If dtpNotificationDate.Checked = True Or dtpNotificationDate.ShowCheckBox = False Then
                NotificationDue = "False"
                NotificationDueDate = dtpNotificationDate.Value
            Else
                NotificationDueDate = DBNull.Value
            End If

            If dtpNotificationDate2.Checked = True Then
                NotificationSent = "False"
                NotificationSentDate = dtpNotificationDate2.Text
            Else
                NotificationSentDate = DBNull.Value
            End If

            If txtNotificationTypeOther.Text <> "" Then
                NotificationTypeOther = txtNotificationTypeOther.Text
            Else
                NotificationTypeOther = ""
            End If
            If txtNotificationComments.Text = "" Then
                NotificationComment = ""
            Else
                NotificationComment = txtNotificationComments.Text
            End If
            If rdbNotificationFollowUpYes.Checked = True Then
                NotificationFollowUp = "True"
            Else
                NotificationFollowUp = "False"
            End If

            Dim SQL As String = "Select 1 from SSCPNotifications where strTrackingNumber = @num"
            Dim p As New SqlParameter("@num", TrackingNumber)

            If DB.ValueExists(SQL, p) Then
                sqlList.Add("UPdate SSCPNotifications set " &
                    "datNotificationDue = @datNotificationDue, " &
                    "strNotificationDue = @strNotificationDue, " &
                    "datNotificationSent = @datNotificationSent, " &
                    "strNotificationSent = @strNotificationSent, " &
                    "strNotificationType = @strNotificationType, " &
                    "strNotificationTypeOther = @strNotificationTypeOther, " &
                    "strNotificationComment = @strNotificationComment, " &
                    "strNotificationFollowUp = @strNotificationFollowUp, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingDate =  GETDATE()  " &
                    "where strTrackingNumber = @num")
            Else
                sqlList.Add("Insert into SSCPNotifications " &
                    "(strTrackingNumber, datNotificationDue, " &
                    "strNotificationDue, datNotificationSent, " &
                    "strNotificationSent, strNotificationType, " &
                    "strNotificationTypeOther, strNotificationComment, " &
                    "strNotificationFollowUp, strModifingPerson, " &
                    "datModifingDate) " &
                    "values " &
                    "(@num, @datNotificationDue, " &
                    "@strNotificationDue, @datNotificationSent, " &
                    "@strNotificationSent, @strNotificationType, " &
                    "@strNotificationTypeOther, @strNotificationComment, " &
                    "@strNotificationFollowUp, @strModifingPerson, " &
                    "GETDATE() ) ")
            End If

            plist.Add({
                New SqlParameter("@datNotificationDue", NotificationDueDate),
                New SqlParameter("@strNotificationDue", NotificationDue),
                New SqlParameter("@datNotificationSent", NotificationSentDate),
                New SqlParameter("@strNotificationSent", NotificationSent),
                New SqlParameter("@strNotificationType", cboNotificationType.SelectedValue),
                New SqlParameter("@strNotificationTypeOther", NotificationTypeOther),
                New SqlParameter("@strNotificationComment", NotificationComment),
                New SqlParameter("@strNotificationFollowUp", NotificationFollowUp),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@num", TrackingNumber)
            })

            If chbNotificationReceivedByAPB.Checked = True Then
                sqlList.Add("Update SSCPItemMaster set " &
                    "datReceivedDate = @date " &
                    "where strTrackingNumber = @num")

                plist.Add({
                    New SqlParameter("@date", DTPNotificationReceived.Value),
                    New SqlParameter("@num", TrackingNumber)
                })
            End If

            Return DB.RunCommand(sqlList, plist)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function SaveDate() As Boolean
        Dim Staff As Integer
        Dim SqlList As New List(Of String)
        Dim ParamList As New List(Of SqlParameter())
        Dim comDate As Object
        Dim sentDate As Object

        Try
            Staff = cboStaffResponsible.SelectedValue
            If Staff = -1 Then
                Staff = 0
            End If

            If chbEventComplete.Checked Then
                comDate = DTPEventCompleteDate.Value
            Else
                comDate = DBNull.Value
            End If

            If DTPAcknowledgmentLetterSent.Checked Then
                sentDate = DTPAcknowledgmentLetterSent.Value
            Else
                sentDate = DBNull.Value
            End If

            SqlList.Add("Update SSCPItemMaster set " &
                        "datCompleteDate = @complete, " &
                        "datAcknoledgmentLetterSent = @sent, " &
                        "strResponsibleStaff = @staff, " &
                        "strDelete = null " &
                        "where strTrackingNumber = @num ")

            ParamList.Add({New SqlParameter("@complete", comDate),
                          New SqlParameter("@sent", sentDate),
                          New SqlParameter("@staff", Staff),
                          New SqlParameter("@num", TrackingNumber)})

            Select Case EventType
                Case WorkItemEventType.Report, WorkItemEventType.TvAcc, WorkItemEventType.Inspection
                    SqlList.Add("Update AFSSSCPRecords set " &
                                "strUpDateStatus = 'C' " &
                                "where strTrackingNumber = @num " &
                                "and strUpDateStatus = 'N' ")

                    ParamList.Add({New SqlParameter("@num", TrackingNumber)})

                    SqlList.Add("UPDATE APBSUPPLAMENTALDATA " &
                                "SET STRAFSACTIONNUMBER = STRAFSACTIONNUMBER + 1 " &
                                "WHERE STRAIRSNUMBER = @airs " &
                                "AND @num NOT IN (SELECT STRTRACKINGNUMBER FROM AFSSSCPRECORDS)")

                    ParamList.Add({New SqlParameter("@airs", AirsNumber.DbFormattedString),
                                  New SqlParameter("@num", TrackingNumber)})

                    SqlList.Add("INSERT INTO AFSSSCPRecords " &
                                "(strTrackingNumber, strAFSActionNumber, strUpDateStatus, strModifingPerson, datModifingdate) " &
                                "SELECT @num, " &
                                "(Select strAFSActionNumber - 1 from APBSupplamentalData where strAIRSNumber = @airs), " &
                                "'A', @user, GETDATE() " &
                                "WHERE @num NOT IN " &
                                "(SELECT strTrackingNumber FROM AFSSSCPRecords)")

                    ParamList.Add({New SqlParameter("@num", TrackingNumber),
                                  New SqlParameter("@airs", AirsNumber.DbFormattedString),
                                  New SqlParameter("@user", CurrentUser.UserID)})

                Case WorkItemEventType.Notification, WorkItemEventType.RmpInspection
                    SqlList.Add("Update AFSSSCPRecords set " &
                                "strUpDateStatus = 'C' " &
                                "where strTrackingNumber = @num " &
                                "and strUpDateStatus = 'N' ")

                    ParamList.Add({New SqlParameter("@num", TrackingNumber)})

            End Select

            If EventType = WorkItemEventType.TvAcc Then
                SqlList.Add("INSERT INTO AFSSSCPRecords " &
                            "(strTrackingNumber, strAFSActionNumber, strUpDateStatus, strModifingPerson, datModifingdate) " &
                            "SELECT @num, " &
                            "(Select strAFSActionNumber from APBSupplamentalData where strAIRSNumber = @airs), " &
                            "'A', @user, GETDATE() " &
                            "WHERE @num NOT IN " &
                            "(SELECT strTrackingNumber FROM AFSSSCPRecords)")

                ParamList.Add({New SqlParameter("@num", TrackingNumber + 1),
                                  New SqlParameter("@airs", AirsNumber.DbFormattedString),
                                  New SqlParameter("@user", CurrentUser.UserID)})

                SqlList.Add("UPDATE APBSUPPLAMENTALDATA " &
                                "SET STRAFSACTIONNUMBER = STRAFSACTIONNUMBER + 1 " &
                                "WHERE STRAIRSNUMBER = @airs " &
                                "AND @num NOT IN (SELECT STRTRACKINGNUMBER FROM AFSSSCPRECORDS)")

                ParamList.Add({New SqlParameter("@airs", AirsNumber.DbFormattedString),
                              New SqlParameter("@num", TrackingNumber + 1)})
            End If

            Return DB.RunCommand(SqlList, ParamList)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

#End Region

#Region " Loads "

    Private Sub LoadReport()
        Try
            Dim SQL As String = "Select " &
                "strTrackingNumber, strReportPeriod, " &
                "datReportingPeriodStart, datReportingPeriodEnd, " &
                "strReportingPeriodComments, datReportDueDate, " &
                "datSentByFacilityDate, strCompleteStatus, " &
                "strEnforcementNeeded, strShowDeviation, " &
                "strGeneralComments, strModifingPerson, " &
                "datModifingDate, strSubmittalNumber " &
                "from SSCPREports " &
                "where strTrackingNumber = @num "

            Dim p As New SqlParameter("@num", TrackingNumber)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                NUPReportSubmittal.Value = dr.Item("strSubmittalNumber")
                cboReportSchedule.Text = dr.Item("strReportPeriod")
                DTPReportPeriodStart.Value = dr.Item("DatReportingPeriodStart")
                DTPReportPeriodEnd.Value = dr.Item("DatReportingPeriodEnd")
                txtReportPeriodComments.Text = dr.Item("strReportingPeriodComments")
                dtpDueDate.Value = dr.Item("datreportduedate")
                DTPSentDate.Value = dr.Item("datsentbyfacilitydate")
                If dr.Item("strCompletestatus") = "True" Then
                    rdbReportCompleteYes.Checked = True
                Else
                    rdbReportCompleteNo.Checked = True
                End If
                If dr.Item("strEnforcementneeded") = "True" Then
                    rdbReportEnforcementYes.Checked = True
                Else
                    rdbReportEnforcementNo.Checked = True
                End If
                If dr.Item("strShowDeviation") = "True" Then
                    rdbReportDeviationYes.Checked = True
                Else
                    rdbReportDeviationNo.Checked = True
                End If
                txtReportsGeneralComments.Text = dr.Item("strGeneralComments")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadReportFromSubmittal()
        Dim Completeness As String
        Dim Enforcement As String
        Dim Deviation As String

        Try

            Dim SQL As String = "Select * from SSCPREportsHistory " &
                "where strTrackingNumber = @num " &
                "and strSubmittalNumber = @sub "

            Dim p As SqlParameter() = {
                New SqlParameter("@num", TrackingNumber),
                New SqlParameter("@sub", NUPReportSubmittal.Value)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                NUPReportSubmittal.Value = dr.Item("strSubmittalNumber")
                cboReportSchedule.Text = dr.Item("strReportPeriod")
                DTPReportPeriodStart.Value = dr.Item("DatReportingPeriodStart")
                DTPReportPeriodEnd.Value = dr.Item("DatReportingPeriodEnd")
                txtReportPeriodComments.Text = dr.Item("strReportingPeriodComments")
                dtpDueDate.Value = dr.Item("datreportduedate")
                DTPSentDate.Value = dr.Item("datsentbyfacilitydate")
                Completeness = dr.Item("strCompletestatus")
                If Completeness = "True" Then
                    rdbReportCompleteYes.Checked = True
                Else
                    rdbReportCompleteNo.Checked = True
                End If
                Enforcement = dr.Item("strEnforcementneeded")
                If Enforcement = "True" Then
                    rdbReportEnforcementYes.Checked = True
                Else
                    rdbReportEnforcementNo.Checked = True
                End If
                Deviation = dr.Item("strShowDeviation")
                If Deviation = "True" Then
                    rdbReportDeviationYes.Checked = True
                Else
                    rdbReportDeviationNo.Checked = True
                End If
                txtReportsGeneralComments.Text = dr.Item("strGeneralComments")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadReportSubmittalDGR()
        Try
            Dim SQL As String = "SELECT STRSUBMITTALNUMBER, DATMODIFINGDATE, CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName " &
                "FROM SSCPREPORTSHISTORY " &
                "INNER JOIN EPDUSERPROFILES ON SSCPREPORTSHISTORY.STRMODIFINGPERSON = EPDUSERPROFILES.NUMUSERID " &
                "WHERE STRTRACKINGNUMBER = @num " &
                "ORDER BY STRSUBMITTALNUMBER "

            Dim p As New SqlParameter("@num", TrackingNumber)

            dgrReportResubmittal.DataSource = DB.GetDataTable(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormatReportsDGR()
        Try
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Reports"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strSubmittalNumber"
            objtextcol.HeaderText = "#"
            objtextcol.Width = 30
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datModifingDate"
            objtextcol.HeaderText = "Date"
            objtextcol.Width = 50
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "UserName"
            objtextcol.HeaderText = "Modifying Individual"
            objtextcol.Width = 180
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrReportResubmittal.TableStyles.Clear()
            dgrReportResubmittal.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrReportResubmittal.CaptionText = "Submittal History"
            dgrReportResubmittal.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadInspection()
        Try
            Dim SQL As String = "Select " &
                "strTrackingNumber, datInspectionDateStart, " &
                "datInspectionDateEnd, strInspectionreason, " &
                "strWeatherConditions, strInspectionGuide, " &
                "strFacilityOperating, strInspectionComplianceStatus, " &
                "strInspectionComments, " &
                "strInspectionFollowUP, strModifingPerson, " &
                "datModifingDate " &
                "from SSCPInspections " &
                "where strTrackingNumber = @num "

            Dim p As New SqlParameter("@num", TrackingNumber)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                cboInspectionReason.Text = dr.Item("strInspectionReason")
                txtWeatherConditions.Text = dr.Item("strWeatherConditions")
                txtInspectionGuide.Text = dr.Item("strInspectionguide")
                If dr.Item("strFacilityOperating") = "True" Then
                    rdbInspectionFacilityOperatingYes.Checked = True
                Else
                    rdbInspectionFacilityOperatingNo.Checked = True
                End If
                cboInspectionComplianceStatus.Text = dr.Item("strInspectioncompliancestatus")
                txtInspectionConclusion.Text = dr.Item("strInspectionComments")
                If dr.Item("strInspectionFollowUp") = "True" Then
                    rdbInspectionFollowUpYes.Checked = True
                Else
                    rdbInspectionFollowUpNo.Checked = True
                End If

                DTPInspectionDateStart.Value = dr.Item("DatINspectionDateStart")
                dtpInspectionTimeStart.Value = dr.Item("DatINspectionDateStart")
                DTPInspectionDateEnd.Value = dr.Item("datinspectionDateEnd")
                dtpInspectionTimeEnd.Value = dr.Item("datinspectionDateEnd")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadACC()
        Dim AllDeviationsReported As String
        Dim ResubmittalRequested As String

        Try

            Dim SQL As String = "Select " &
                "strTrackingNumber, strSubmittalNumber, " &
                "strPostmarkedOnTime, datPostmarkDate, " &
                "strSignedByRO, strCorrectACCForms, " &
                "strTitleVConditionsListed, strACCCorrectlyFilledOut, " &
                "strReportedDeviations, strDeviationsUnReported, " &
                "strComments, strEnforcementNeeded, " &
                "strModifingPerson, datModifingDate, datAccReportingYear, " &
                "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED " &
                "from SSCPACCS " &
                "where strTrackingNumber = @num"

            Dim p As New SqlParameter("@num", TrackingNumber)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                NUPACCSubmittal.Value = dr.Item("strSubmittalNumber")
                If dr.Item("strPostMarkedOnTime") = "True" Then
                    rdbACCPostmarkYes.Checked = True
                Else
                    rdbACCPostmarkNo.Checked = True
                End If
                If IsDBNull(dr.Item("datAccReportingYear")) OrElse dr.Item("datAccReportingYear") = "04-Jul-1776" Then
                    dtpAccReportingYear.Value = Today.AddYears(-1)
                    dtpAccReportingYear.Checked = False
                Else
                    dtpAccReportingYear.Value = dr.Item("datAccReportingYear")
                    dtpAccReportingYear.Checked = True
                End If
                If dr.Item("DATPostmarkDate") = "04-Jul-1776" Then
                    DTPACCPostmarked.Value = Today
                Else
                    DTPACCPostmarked.Value = dr.Item("datPostmarkDate")
                End If
                If dr.Item("strSignedByRO") = "True" Then
                    rdbACCROYes.Checked = True
                Else
                    rdbACCRONo.Checked = True
                End If
                If dr.Item("strCorrectACCForms") = "True" Then
                    rdbACCCorrectACCYes.Checked = True
                Else
                    rdbACCCorrectACCNo.Checked = True
                End If
                If dr.Item("strTitleVConditionsListed") = "True" Then
                    rdbACCConditionsYes.Checked = True
                Else
                    rdbACCConditionsNo.Checked = True
                End If
                If dr.Item("strACCCorrectlyFilledOut") = "True" Then
                    rdbACCCorrectYes.Checked = True
                Else
                    rdbACCCorrectNo.Checked = True
                End If
                If dr.Item("strReportedDeviations") = "True" Then
                    rdbACCDeviationsReportedYes.Checked = True
                Else
                    rdbACCDeviationsReportedNo.Checked = True
                End If
                If dr.Item("strDeviationsUnreported") = "True" Then
                    rdbACCPreviouslyUnreportedDeviationsYes.Checked = True
                Else
                    rdbACCPreviouslyUnreportedDeviationsNo.Checked = True
                End If
                txtACCComments.Text = dr.Item("strcomments")
                If dr.Item("strEnforcementNeeded") = "True" Then
                    rdbACCEnforcementNeededYes.Checked = True
                Else
                    rdbACCEnforcementNeededNo.Checked = True
                End If
                AllDeviationsReported = DBUtilities.GetNullable(Of String)(dr.Item("STRKNOWNDEVIATIONSREPORTED"))
                If AllDeviationsReported = "" Then
                    rdbACCAllDeviationsReportedYes.Checked = False
                    rdbACCAllDeviationsReportedNo.Checked = False
                    rdbACCAllDeviationsReportedUnknown.Checked = True
                    rdbACCAllDeviationsReportedUnknown.Visible = True
                ElseIf AllDeviationsReported = "True" Then
                    rdbACCAllDeviationsReportedYes.Checked = True
                    rdbACCAllDeviationsReportedNo.Checked = False
                    rdbACCAllDeviationsReportedUnknown.Checked = False
                    rdbACCAllDeviationsReportedUnknown.Visible = False
                Else
                    rdbACCAllDeviationsReportedYes.Checked = False
                    rdbACCAllDeviationsReportedNo.Checked = True
                    rdbACCAllDeviationsReportedUnknown.Checked = False
                    rdbACCAllDeviationsReportedUnknown.Visible = False
                End If
                ResubmittalRequested = DBUtilities.GetNullable(Of String)(dr.Item("STRRESUBMITTALREQUIRED"))
                If ResubmittalRequested = "" Then
                    rdbACCResubmittalRequestedYes.Checked = False
                    rdbACCResubmittalRequestedNo.Checked = False
                    rdbACCResubmittalRequestedUnknown.Checked = True
                    rdbACCResubmittalRequestedUnknown.Visible = True
                ElseIf ResubmittalRequested = "True" Then
                    rdbACCResubmittalRequestedYes.Checked = True
                    rdbACCResubmittalRequestedNo.Checked = False
                    rdbACCResubmittalRequestedUnknown.Checked = False
                    rdbACCResubmittalRequestedUnknown.Visible = False
                Else
                    rdbACCResubmittalRequestedYes.Checked = False
                    rdbACCResubmittalRequestedNo.Checked = True
                    rdbACCResubmittalRequestedUnknown.Checked = False
                    rdbACCResubmittalRequestedUnknown.Visible = False
                End If
            Else
                dtpAccReportingYear.Value = Today.AddYears(-1)
                dtpAccReportingYear.Checked = True
            End If

            DTPACCPostmarked.Enabled = True
            dtpAccReportingYear.Enabled = True
            txtACCComments.ReadOnly = False
            If NUPACCSubmittal.Value > 1 Then
                rdbACCPostmarkYes.Enabled = False
                rdbACCPostmarkNo.Enabled = False
                rdbACCROYes.Enabled = False
                rdbACCRONo.Enabled = False
                rdbACCCorrectACCYes.Enabled = False
                rdbACCCorrectACCNo.Enabled = False
                rdbACCConditionsYes.Enabled = False
                rdbACCConditionsNo.Enabled = False
                rdbACCCorrectYes.Enabled = False
                rdbACCCorrectNo.Enabled = False
                rdbACCDeviationsReportedYes.Enabled = False
                rdbACCDeviationsReportedNo.Enabled = False
                rdbACCPreviouslyUnreportedDeviationsYes.Enabled = False
                rdbACCPreviouslyUnreportedDeviationsNo.Enabled = False
                rdbACCEnforcementNeededYes.Enabled = False
                rdbACCEnforcementNeededNo.Enabled = False
                dtpAccReportingYear.Enabled = False
                rdbACCAllDeviationsReportedYes.Enabled = False
                rdbACCAllDeviationsReportedNo.Enabled = False
                rdbACCAllDeviationsReportedUnknown.Enabled = False
                rdbACCResubmittalRequestedYes.Enabled = False
                rdbACCResubmittalRequestedNo.Enabled = False
                rdbACCResubmittalRequestedUnknown.Enabled = False
            Else
                rdbACCPostmarkYes.Enabled = True
                rdbACCPostmarkNo.Enabled = True
                rdbACCROYes.Enabled = True
                rdbACCRONo.Enabled = True
                rdbACCCorrectACCYes.Enabled = True
                rdbACCCorrectACCNo.Enabled = True
                rdbACCConditionsYes.Enabled = True
                rdbACCConditionsNo.Enabled = True
                rdbACCCorrectYes.Enabled = True
                rdbACCCorrectNo.Enabled = True
                rdbACCDeviationsReportedYes.Enabled = True
                rdbACCDeviationsReportedNo.Enabled = True
                rdbACCPreviouslyUnreportedDeviationsYes.Enabled = True
                rdbACCPreviouslyUnreportedDeviationsNo.Enabled = True
                rdbACCEnforcementNeededYes.Enabled = True
                rdbACCEnforcementNeededNo.Enabled = True
                DTPACCPostmarked.Enabled = True
                dtpAccReportingYear.Enabled = True
                chbACCReceivedByAPB.Enabled = True
                rdbACCAllDeviationsReportedYes.Enabled = True
                rdbACCAllDeviationsReportedNo.Enabled = True
                rdbACCAllDeviationsReportedUnknown.Enabled = True
                rdbACCResubmittalRequestedYes.Enabled = True
                rdbACCResubmittalRequestedNo.Enabled = True
                rdbACCResubmittalRequestedUnknown.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadACCFromSubmittal()
        Dim PostedOnTime As String
        Dim SignedByRO As String
        Dim CorrectACCForm As String
        Dim TitleVConditions As String
        Dim ACCCorrectlyFilledOut As String
        Dim ReportedDeviations As String
        Dim ReportedUnReportedDeviations As String
        Dim ACCComments As String
        Dim EnforcementNeeded As String
        Dim AllDeviationsReported As String
        Dim ResubmittalRequested As String

        Try
            Dim SQL As String = "Select * from SSCPACCSHistory " &
                "where strTrackingNumber = @num " &
                "and strSubmittalNumber = @sub "

            Dim p As SqlParameter() = {
                New SqlParameter("@num", TrackingNumber),
                New SqlParameter("@sub", NUPACCSubmittal.Value)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                NUPACCSubmittal.Value = dr.Item("strSubmittalNumber")
                PostedOnTime = dr.Item("strPostMarkedOnTime")
                If PostedOnTime = "True" Then
                    rdbACCPostmarkYes.Checked = True
                    rdbACCPostmarkNo.Checked = False
                Else
                    rdbACCPostmarkYes.Checked = False
                    rdbACCPostmarkNo.Checked = True
                End If
                If IsDBNull(dr.Item("datAccReportingYear")) OrElse dr.Item("datAccReportingYear") = "04-Jul-1776" Then
                    dtpAccReportingYear.Value = Today.AddYears(-1)
                    dtpAccReportingYear.Checked = False
                Else
                    dtpAccReportingYear.Value = dr.Item("datAccReportingYear")
                    dtpAccReportingYear.Checked = True
                End If
                If dr.Item("DATPostmarkDate") = "04-Jul-1776" Then
                    DTPACCPostmarked.Value = Today
                Else
                    DTPACCPostmarked.Value = dr.Item("datPostmarkDate")
                End If
                SignedByRO = dr.Item("strSignedByRO")
                If SignedByRO = "True" Then
                    rdbACCROYes.Checked = True
                    rdbACCRONo.Checked = False
                Else
                    rdbACCROYes.Checked = False
                    rdbACCRONo.Checked = True
                End If
                CorrectACCForm = dr.Item("strCorrectACCForms")
                If CorrectACCForm = "True" Then
                    rdbACCCorrectACCYes.Checked = True
                    rdbACCCorrectACCNo.Checked = False
                Else
                    rdbACCCorrectACCNo.Checked = True
                    rdbACCCorrectACCYes.Checked = False
                End If
                TitleVConditions = dr.Item("strTitleVConditionsListed")
                If TitleVConditions = "True" Then
                    rdbACCConditionsYes.Checked = True
                    rdbACCConditionsNo.Checked = False
                Else
                    rdbACCConditionsNo.Checked = True
                    rdbACCConditionsYes.Checked = False
                End If
                ACCCorrectlyFilledOut = dr.Item("strACCCorrectlyFilledOut")
                If ACCCorrectlyFilledOut = "True" Then
                    rdbACCCorrectYes.Checked = True
                    rdbACCCorrectNo.Checked = False
                Else
                    rdbACCCorrectYes.Checked = False
                    rdbACCCorrectNo.Checked = True
                End If
                ReportedDeviations = dr.Item("strReportedDeviations")
                If ReportedDeviations = "True" Then
                    rdbACCDeviationsReportedYes.Checked = True
                    rdbACCDeviationsReportedNo.Checked = False
                Else
                    rdbACCDeviationsReportedYes.Checked = False
                    rdbACCDeviationsReportedNo.Checked = True
                End If
                ReportedUnReportedDeviations = dr.Item("strDeviationsUnreported")
                If ReportedUnReportedDeviations = "True" Then
                    rdbACCPreviouslyUnreportedDeviationsYes.Checked = True
                    rdbACCPreviouslyUnreportedDeviationsNo.Checked = False
                Else
                    rdbACCPreviouslyUnreportedDeviationsYes.Checked = False
                    rdbACCPreviouslyUnreportedDeviationsNo.Checked = True
                End If
                ACCComments = dr.Item("strcomments")
                If ACCComments = "N/A" Then
                    txtACCComments.Text = ""
                Else
                    txtACCComments.Text = ACCComments
                End If
                EnforcementNeeded = dr.Item("strEnforcementNeeded")
                If EnforcementNeeded = "True" Then
                    rdbACCEnforcementNeededYes.Checked = True
                    rdbACCEnforcementNeededNo.Checked = False
                Else
                    rdbACCEnforcementNeededYes.Checked = False
                    rdbACCEnforcementNeededNo.Checked = True
                End If
                AllDeviationsReported = DBUtilities.GetNullable(Of String)(dr.Item("STRKNOWNDEVIATIONSREPORTED"))
                If AllDeviationsReported = "" Then
                    rdbACCAllDeviationsReportedYes.Checked = False
                    rdbACCAllDeviationsReportedNo.Checked = False
                    rdbACCAllDeviationsReportedUnknown.Checked = True
                ElseIf AllDeviationsReported = "True" Then
                    rdbACCAllDeviationsReportedYes.Checked = True
                    rdbACCAllDeviationsReportedNo.Checked = False
                    rdbACCAllDeviationsReportedUnknown.Checked = False
                Else
                    rdbACCAllDeviationsReportedYes.Checked = False
                    rdbACCAllDeviationsReportedNo.Checked = True
                    rdbACCAllDeviationsReportedUnknown.Checked = False
                End If
                ResubmittalRequested = DBUtilities.GetNullable(Of String)(dr.Item("STRRESUBMITTALREQUIRED"))
                If ResubmittalRequested = "" Then
                    rdbACCResubmittalRequestedYes.Checked = False
                    rdbACCResubmittalRequestedNo.Checked = False
                    rdbACCResubmittalRequestedUnknown.Checked = True
                ElseIf ResubmittalRequested = "True" Then
                    rdbACCResubmittalRequestedYes.Checked = True
                    rdbACCResubmittalRequestedNo.Checked = False
                    rdbACCResubmittalRequestedUnknown.Checked = False
                Else
                    rdbACCResubmittalRequestedYes.Checked = False
                    rdbACCResubmittalRequestedNo.Checked = True
                    rdbACCResubmittalRequestedUnknown.Checked = False
                End If
            Else
                dtpAccReportingYear.Value = DateTime.Today.AddYears(-1)
                dtpAccReportingYear.Checked = True
            End If

            DTPACCPostmarked.Enabled = True
            dtpAccReportingYear.Enabled = True
            txtACCComments.ReadOnly = False
            If NUPACCSubmittal.Value > 1 Then
                rdbACCPostmarkYes.Enabled = False
                rdbACCPostmarkNo.Enabled = False
                rdbACCROYes.Enabled = False
                rdbACCRONo.Enabled = False
                rdbACCCorrectACCYes.Enabled = False
                rdbACCCorrectACCNo.Enabled = False
                rdbACCConditionsYes.Enabled = False
                rdbACCConditionsNo.Enabled = False
                rdbACCCorrectYes.Enabled = False
                rdbACCCorrectNo.Enabled = False
                rdbACCDeviationsReportedYes.Enabled = False
                rdbACCDeviationsReportedNo.Enabled = False
                rdbACCPreviouslyUnreportedDeviationsYes.Enabled = False
                rdbACCPreviouslyUnreportedDeviationsNo.Enabled = False
                rdbACCEnforcementNeededYes.Enabled = False
                rdbACCEnforcementNeededNo.Enabled = False
                rdbACCAllDeviationsReportedYes.Enabled = False
                rdbACCAllDeviationsReportedNo.Enabled = False
                rdbACCAllDeviationsReportedUnknown.Enabled = False
                rdbACCResubmittalRequestedNo.Enabled = False
                rdbACCResubmittalRequestedUnknown.Enabled = False
                rdbACCResubmittalRequestedYes.Enabled = False
                dtpAccReportingYear.Enabled = False
            Else
                rdbACCPostmarkYes.Enabled = True
                rdbACCPostmarkNo.Enabled = True
                rdbACCROYes.Enabled = True
                rdbACCRONo.Enabled = True
                rdbACCCorrectACCYes.Enabled = True
                rdbACCCorrectACCNo.Enabled = True
                rdbACCConditionsYes.Enabled = True
                rdbACCConditionsNo.Enabled = True
                rdbACCCorrectYes.Enabled = True
                rdbACCCorrectNo.Enabled = True
                rdbACCDeviationsReportedYes.Enabled = True
                rdbACCDeviationsReportedNo.Enabled = True
                rdbACCPreviouslyUnreportedDeviationsYes.Enabled = True
                rdbACCPreviouslyUnreportedDeviationsNo.Enabled = True
                rdbACCEnforcementNeededYes.Enabled = True
                rdbACCEnforcementNeededNo.Enabled = True
                rdbACCAllDeviationsReportedYes.Enabled = True
                rdbACCAllDeviationsReportedNo.Enabled = True
                rdbACCAllDeviationsReportedUnknown.Enabled = True
                rdbACCResubmittalRequestedNo.Enabled = True
                rdbACCResubmittalRequestedUnknown.Enabled = True
                rdbACCResubmittalRequestedYes.Enabled = True
                DTPACCPostmarked.Enabled = True
                chbACCReceivedByAPB.Enabled = True
                dtpAccReportingYear.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadACCSubmittalDGR()
        Try
            Dim SQL As String = "Select strSubmittalNumber, datModifingDate, " &
                "concat(strLastName, ', ' ,strFirstName) as UserName " &
                "from SSCPACCSHistory inner join EPDUserProfiles " &
                "on SSCPACCSHistory.strModifingPerson = EPDUserProfiles.numUserID " &
                "where strTrackingNumber = @num " &
                "order by strsubmittalnumber"
            Dim p As New SqlParameter("@num", TrackingNumber)

            DGRACCResubmittal.DataSource = DB.GetDataTable(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormatACCDGR()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ACCs"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strSubmittalNumber"
            objtextcol.HeaderText = "#"
            objtextcol.Width = 30
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datModifingDate"
            objtextcol.HeaderText = "Date"
            objtextcol.Width = 50
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "UserName"
            objtextcol.HeaderText = "Modifying Individual"
            objtextcol.Width = 180
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            DGRACCResubmittal.TableStyles.Clear()
            DGRACCResubmittal.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            DGRACCResubmittal.CaptionText = "Submittal History"
            DGRACCResubmittal.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadTestReport()
        Try
            Dim SQL As String = "Select " &
                            "strTrackingNUmber, strReferenceNumber, " &
                            "datTestReportDue, " &
                            "strTestReportComments, strTestReportFOllowUP, " &
                            "strModifingPerson, datModifingDate " &
                            "from SSCPTestReports " &
                            "where strTrackingNumber = @num"

            Dim p As New SqlParameter("@num", TrackingNumber)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                txtISMPReferenceNumber.Text = dr.Item("StrReferenceNumber").ToString
                If NormalizeDate(CDate(dr.Item("datTestReportdue"))).HasValue Then
                    txtTestReportDueDate.Text = CDate(dr.Item("datTestReportdue")).ToString(DateFormat)
                End If

                txtTestReportComments.Text = dr.Item("strTestREportComments").ToString
                rdbTestReportFollowUpNo.Checked = CBool(dr.Item("strTestReportFollowUp"))
            Else
                txtISMPReferenceNumber.Text = "N/A"
                txtTestReportDueDate.Text = "Unknown"
                txtTestReportComments.Text = "N/A"
            End If

            If txtISMPReferenceNumber.Text = "N/A" Or txtISMPReferenceNumber.Text = "" Then
                DTPTestReportReceivedDate.Value = Today
                txtTestReportISMPCompleteDate.Text = "N/A"
            Else
                SQL = "Select datReceivedDate, datCompleteDate " &
                    "from ISMPReportInformation " &
                    "where strReferenceNumber = @ref "

                Dim p2 As New SqlParameter("@ref", txtISMPReferenceNumber.Text)

                Dim dr2 As DataRow = DB.GetDataRow(SQL, p2)

                If dr2 IsNot Nothing Then
                    DTPTestReportReceivedDate.Value = NormalizeDate(CDate(dr2.Item("datReceivedDate"))).Value
                    If Not NormalizeDate(CDate(dr2.Item("datCompleteDate"))).HasValue Then
                        txtTestReportISMPCompleteDate.Text = "Open"
                    Else
                        txtTestReportISMPCompleteDate.Text = CDate(dr2.Item("datReceivedDate")).ToString(DateFormat)
                    End If
                Else
                    DTPTestReportReceivedDate.Value = Today
                    txtTestReportISMPCompleteDate.Text = TodayFormatted
                End If
            End If

            SQL = "Select datSSCPTestReportDue " &
                "from APBSupplamentalData " &
                "where strAIRSNumber = @airs "

            Dim p3 As New SqlParameter("@airs", AirsNumber.DbFormattedString)

            Dim dr3 As DataRow = DB.GetDataRow(SQL, p3)

            If dr3 IsNot Nothing Then
                If IsDBNull(dr3.Item("datSSCPTestReportDue")) Then
                    DTPTestReportNewDueDate.Value = Today
                Else
                    DTPTestReportNewDueDate.Value = CDate(dr3.Item("datSSCPTestReportDue"))
                End If
            Else
                DTPTestReportNewDueDate.Value = Today
            End If

            If txtISMPReferenceNumber.Text <> "N/A" Then
                SQL = "Select " &
                    "strEmissionSource, strPollutantDescription " &
                    "from ISMPReportInformation inner join LookUPPollutants " &
                    "on  ISMPReportInformation.strPollutant = LookUPPollutants.strPollutantCode " &
                    "where strReferenceNumber = @ref "

                Dim p2 As New SqlParameter("@ref", txtISMPReferenceNumber.Text)

                Dim dr2 As DataRow = DB.GetDataRow(SQL, p2)

                If dr2 IsNot Nothing Then
                    txtPollutantTested.Text = dr2.Item("strPollutantDescription").ToString
                    txtUnitTested.Text = dr2.Item("strEmissionSource").ToString
                Else
                    txtPollutantTested.Text = "N/A"
                    txtUnitTested.Text = "N/A"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadNotification()
        Try

            Dim SQL As String = "Select " &
                "strTrackingNumber, datNotificationDue, " &
                "strNotificationDue, datNotificationSent, " &
                "strNotificationSent, strNotificationType, " &
                "strNotificationTypeOther, strNotificationComment, " &
                "strNotificationFollowUp, strModifingPerson, " &
                "datModifingDate " &
                "From SSCPNotifications " &
                "where strTrackingNumber = @num"

            Dim p As New SqlParameter("@num", TrackingNumber)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strNotificationType")) Then
                    cboNotificationType.SelectedValue = "01"
                Else
                    cboNotificationType.SelectedValue = dr.Item("strNotificationType")
                End If

                If IsDBNull(dr.Item("datNotificationDue")) Then
                    dtpNotificationDate.Value = Today
                Else
                    If dr.Item("datNotificationDue") <> "04-Jul-1776" Then
                        dtpNotificationDate.Value = dr.Item("datNotificationDue")
                    Else
                        dtpNotificationDate.Value = Today
                    End If
                End If
                If dtpNotificationDate.ShowCheckBox = True Then
                    'If value is True then leave field blank 
                    If IsDBNull(dr.Item("strNotificationDue")) Then
                        dtpNotificationDate.Checked = False
                    Else
                        If dr.Item("strNotificationDue") = "True" Then
                            dtpNotificationDate.Checked = False
                        Else
                            dtpNotificationDate.Checked = True
                        End If
                    End If
                End If
                If IsDBNull(dr.Item("datNotificationSent")) Then
                    dtpNotificationDate2.Text = Today
                Else
                    If dr.Item("datNotificationSent") <> "04-Jul-1776" Then
                        dtpNotificationDate2.Text = dr.Item("datNotificationSent")
                    Else
                        dtpNotificationDate2.Text = Today
                    End If
                End If
                'If value is True then leave field blank 
                If IsDBNull(dr.Item("strNotificationSent")) Then
                    dtpNotificationDate2.Checked = False
                Else
                    If dr.Item("strNotificationSent") = "True" Then
                        dtpNotificationDate2.Checked = False
                    Else
                        dtpNotificationDate2.Checked = True
                    End If
                End If

                If IsDBNull(dr.Item("strNotificationTypeOther")) Then
                    txtNotificationTypeOther.Text = ""
                Else
                    If dr.Item("strNotificationTypeOther") <> "N/A" Then
                        txtNotificationTypeOther.Text = dr.Item("strNotificationTypeOther")
                    End If
                End If
                If IsDBNull(dr.Item("strNotificationComment")) Then
                    txtNotificationComments.Text = "N/A"
                Else
                    txtNotificationComments.Text = dr.Item("strNotificationComment")
                End If
                If IsDBNull(dr.Item("strNotificationFollowUp")) Then
                    rdbNotificationFollowUpNo.Checked = True
                Else
                    If dr.Item("strNotificationFollowUp") = "True" Then
                        rdbNotificationFollowUpYes.Checked = True
                    Else
                        rdbNotificationFollowUpNo.Checked = True
                    End If
                End If
            Else
                dtpNotificationDate.Checked = False
                dtpNotificationDate.Value = Today
                If dtpNotificationDate.ShowCheckBox = True Then
                    dtpNotificationDate.Checked = False
                End If
                dtpNotificationDate2.Text = Today
                cboNotificationType.SelectedValue = "01"
                txtNotificationTypeOther.Text = "N/A"
                txtNotificationComments.Text = "N/A"
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Deletes "

    Private Sub DeleteSSCPData()
        Try
            If EventType = WorkItemEventType.StackTest Then
                MessageBox.Show("Performance tests must be deleted by ISMP.",
                                "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            If DAL.Sscp.TrackingNumberHasEnforcement(TrackingNumber) Then
                MessageBox.Show("This Compliance Action is currently linked to enforcement." & vbCrLf &
                                "Disassociate this action from any enforcement before deleting.",
                                "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            If MessageBox.Show("Should this work item be deleted?",
                               "Confirm Deletion",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            ' Mark as deleted in SSCP item master and AFSSSCPRECORDS
            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of SqlParameter())

            queryList.Add(" UPDATE AFSSSCPRECORDS SET STRUPDATESTATUS = 'D' WHERE STRTRACKINGNUMBER = @id ")

            parametersList.Add({New SqlParameter("@id", TrackingNumber)})

            queryList.Add(" UPDATE SSCPITEMMASTER SET STRDELETE = '" & Boolean.TrueString & "' " &
                          " WHERE STRTRACKINGNUMBER = @id ")

            parametersList.Add({New SqlParameter("@id", TrackingNumber)})

            Dim result As Boolean = DB.RunCommand(queryList, parametersList)

            If result Then
                MsgBox("Compliance Event Deleted.", MsgBoxStyle.Information, "SSCP Events")
                Me.Close()
            Else
                MsgBox("There was an error deleting the event.", MsgBoxStyle.Exclamation, "SSCP Events")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Validate "

#Region " Reports Validating "

    Private Sub cboReportSchedule_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboReportSchedule.Validating
        ValidatecboReportSchedule()
    End Sub

    Private Sub rdbReportCompleteYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbReportCompleteYes.Validating
        ValidateReportComplete()
    End Sub

    Private Sub rdbReportCompleteNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbReportCompleteNo.Validating
        ValidateReportComplete()
    End Sub

    Private Sub rdbReportDeviationYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbReportDeviationYes.Validating
        ValidateShowDeviation()
    End Sub

    Private Sub rdbReportDeviationNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbReportDeviationNo.Validating
        ValidateShowDeviation()
    End Sub

    Private Sub rdbReportEnforcementYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbReportEnforcementYes.Validating
        ValidateEnforcementNeeded()
    End Sub

    Private Sub rdbReportEnforcementNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbReportEnforcementNo.Validating
        ValidateEnforcementNeeded()
    End Sub

    Private Sub NUPSubmittal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NUPReportSubmittal.Validating
        ValidateSubmittalNumber()
    End Sub

#End Region

#Region " Inspections Validating "

    Private Sub cboInspectionComplianceStatus_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboInspectionComplianceStatus.Validating
        ValidatecboInspectionComplianceStatus()
    End Sub

    Private Sub rdbInspectionFacilityOperatingYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbInspectionFacilityOperatingYes.Validating
        ValidateFacilityOperating()
    End Sub

    Private Sub rdbInspectionFacilityOperatingNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbInspectionFacilityOperatingNo.Validating
        ValidateFacilityOperating()
    End Sub

    Private Sub DTPInspectionDateStart_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DTPInspectionDateStart.Validating
        ValidateDTPInspectionDates()
    End Sub

    Private Sub DTPInspectionDateEnd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DTPInspectionDateEnd.Validating
        ValidateDTPInspectionDates()
    End Sub

#End Region

#Region " ACC Validating "

    Private Sub rdbACCConditionsYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCConditionsYes.Validating
        ValidateTitleVConditions()
    End Sub

    Private Sub rdbACCConditionsNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCConditionsNo.Validating
        ValidateTitleVConditions()
    End Sub

    Private Sub rdbACCCorrectACCNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectACCNo.Validating
        ValidateCorrectACCForms()
    End Sub

    Private Sub rdbACCCorrectACCYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectACCYes.Validating
        ValidateCorrectACCForms()
    End Sub

    Private Sub rdbACCCorrectYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectYes.Validating
        ValidateCorrectlyFilledOut()
    End Sub

    Private Sub rdbACCCorrectNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectNo.Validating, rdbACCAllDeviationsReportedUnknown.Validating
        ValidateCorrectlyFilledOut()
    End Sub

    Private Sub rdbACCDeviationsReportedYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCDeviationsReportedYes.Validating
        ValidateReportedDeviations()
    End Sub

    Private Sub rdbACCDeviationsReportedNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCDeviationsReportedNo.Validating
        ValidateReportedDeviations()
    End Sub

    Private Sub rdbACCEnforcementNeededYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCEnforcementNeededYes.Validating
        ValidateACCEnforcementNeeded()
    End Sub

    Private Sub rdbACCAllDeviationsReported_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    Handles rdbACCAllDeviationsReportedYes.Validating, rdbACCAllDeviationsReportedNo.Validating, rdbACCAllDeviationsReportedUnknown.Validating
        ValidateACCAllDeviationsReported()
    End Sub

    Private Sub rdbACCResubmittalRequested_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    Handles rdbACCResubmittalRequestedYes.Validating, rdbACCResubmittalRequestedNo.Validating, rdbACCResubmittalRequestedUnknown.Validating
        ValidateACCResubmittalRequested()
    End Sub

    Private Sub rdbACCEnforcementNeededNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCEnforcementNeededNo.Validating
        ValidateACCEnforcementNeeded()
    End Sub

    Private Sub rdbACCPostmarkYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCPostmarkYes.Validating
        ValidatePostmarkDate()
    End Sub

    Private Sub rdbACCPostmarkNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCPostmarkNo.Validating
        ValidatePostmarkDate()
        ValidateDatePostmarked()
    End Sub

    Private Sub rdbACCPreviousDeviationsYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCPreviouslyUnreportedDeviationsYes.Validating
        ValidatePreviouslyReportedDeviations()
    End Sub

    Private Sub rdbACCPreviousDeviationsNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCPreviouslyUnreportedDeviationsNo.Validating
        ValidatePreviouslyReportedDeviations()
    End Sub

    Private Sub rdbACCROYes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCROYes.Validating
        ValidateROSigned()
    End Sub

    Private Sub rdbACCRONo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles rdbACCRONo.Validating
        ValidateROSigned()
    End Sub

    Private Sub NUPACCSubmittal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NUPACCSubmittal.Validating
        ValidateNUPACCSubmittal()
    End Sub

#End Region

#Region "Validate Report"

    Private Sub ValidateALLReport()
        ValidatecboReportSchedule()
        ValidateReportComplete()
        ValidateShowDeviation()
        ValidateEnforcementNeeded()
        ValidateSubmittalNumber()
    End Sub

    Private Sub ValidatecboReportSchedule()
        If cboReportSchedule.Text = "" Then
            wrnReportPeriod.Visible = True
        Else
            wrnReportPeriod.Visible = False
        End If
    End Sub

    Private Sub ValidateReportComplete()
        If rdbReportCompleteYes.Checked = False And rdbReportCompleteNo.Checked = False Then
            wrnCompleteReport.Visible = True
        Else
            wrnCompleteReport.Visible = False
        End If
    End Sub

    Private Sub ValidateShowDeviation()
        If rdbReportDeviationYes.Checked = False And rdbReportDeviationNo.Checked = False Then
            wrnShowDeviation.Visible = True
        Else
            wrnShowDeviation.Visible = False
        End If
    End Sub

    Private Sub ValidateEnforcementNeeded()
        If rdbReportEnforcementYes.Checked = False And rdbReportEnforcementNo.Checked = False Then
            wrnEnforcementNeeded.Visible = True
        Else
            wrnEnforcementNeeded.Visible = False
        End If
    End Sub

    Private Sub ValidateSubmittalNumber()
        If NUPReportSubmittal.Text = "" Then
            wrnReportSubmittal.Visible = True
        Else
            wrnReportSubmittal.Visible = False
        End If
    End Sub

#End Region

#Region "Validate Inspection"

    Private Sub ValidateAllInspection()
        ValidatecboInspectionComplianceStatus()
        ValidateFacilityOperating()
        ValidateDTPInspectionDates()
    End Sub

    Private Sub ValidatecboInspectionComplianceStatus()
        If cboInspectionComplianceStatus.Text = "" Then
            wrnInspectionComplianceStatus.Visible = True
        Else
            wrnInspectionComplianceStatus.Visible = False
        End If
    End Sub

    Private Sub ValidateFacilityOperating()
        If rdbInspectionFacilityOperatingYes.Checked = False And rdbInspectionFacilityOperatingNo.Checked = False Then
            wrnInspectionOperating.Visible = True
        Else
            wrnInspectionOperating.Visible = False
        End If
    End Sub

    Private Sub ValidateDTPInspectionDates()
        If DTPInspectionDateEnd.Value.Date < DTPInspectionDateStart.Value.Date Then
            wrnInspectionDates.Visible = True
        Else
            wrnInspectionDates.Visible = False
        End If
    End Sub
#End Region

#Region "Validate ACC"

    Private Sub ValidateAllACC()
        ValidateNUPACCSubmittal()
        ValidatePostmarkDate()
        ValidateROSigned()
        ValidateCorrectACCForms()
        ValidateTitleVConditions()
        ValidateCorrectlyFilledOut()
        ValidateReportedDeviations()
        ValidatePreviouslyReportedDeviations()
        ValidateACCEnforcementNeeded()
        ValidateACCAllDeviationsReported()
        ValidateACCResubmittalRequested()
    End Sub

    Private Sub ValidateNUPACCSubmittal()
        If NUPACCSubmittal.Text = "" Then
            wrnACCSubmittal.Visible = True
        Else
            wrnACCSubmittal.Visible = False
        End If
    End Sub

    Private Sub ValidatePostmarkDate()
        If rdbACCPostmarkYes.Checked = False And rdbACCPostmarkNo.Checked = False Then
            wrnACCPostmark.Visible = True
        Else
            wrnACCPostmark.Visible = False
        End If
    End Sub

    Private Sub ValidateROSigned()
        If rdbACCROYes.Checked = False And rdbACCRONo.Checked = False Then
            wrnACCRO.Visible = True
        Else
            wrnACCRO.Visible = False
        End If
    End Sub

    Private Sub ValidateCorrectACCForms()
        If rdbACCCorrectACCYes.Checked = False And rdbACCCorrectACCNo.Checked = False Then
            wrnACCCorrectACC.Visible = True
        Else
            wrnACCCorrectACC.Visible = False
        End If
    End Sub

    Private Sub ValidateTitleVConditions()
        If rdbACCConditionsYes.Checked = False And rdbACCConditionsNo.Checked = False Then
            wrnACCConditions.Visible = True
        Else
            wrnACCConditions.Visible = False
        End If
    End Sub

    Private Sub ValidateCorrectlyFilledOut()
        If rdbACCCorrectYes.Checked = False And rdbACCCorrectNo.Checked = False Then
            wrnACCCorrect.Visible = True
        Else
            wrnACCCorrect.Visible = False
        End If
    End Sub

    Private Sub ValidateReportedDeviations()
        If rdbACCDeviationsReportedYes.Checked = False And rdbACCDeviationsReportedNo.Checked = False Then
            wrnACCDeviationsReported.Visible = True
        Else
            wrnACCDeviationsReported.Visible = False
        End If
    End Sub

    Private Sub ValidatePreviouslyReportedDeviations()
        If rdbACCPreviouslyUnreportedDeviationsYes.Checked = False And rdbACCPreviouslyUnreportedDeviationsNo.Checked = False Then
            wrnACCPreviousDeviations.Visible = True
        Else
            wrnACCPreviousDeviations.Visible = False
        End If
    End Sub

    Private Sub ValidateACCEnforcementNeeded()
        If rdbACCEnforcementNeededYes.Checked = False And rdbACCEnforcementNeededNo.Checked = False Then
            wrnACCEnforcementNeeded.Visible = True
        Else
            wrnACCEnforcementNeeded.Visible = False
        End If
    End Sub

    Private Sub ValidateACCAllDeviationsReported()
        If (rdbACCAllDeviationsReportedYes.Checked Or rdbACCAllDeviationsReportedNo.Checked) Then
            wrnACCAllDeviationsReported.Visible = False
        Else
            wrnACCAllDeviationsReported.Visible = True
            If rdbACCAllDeviationsReportedUnknown.Visible Then
                wrnACCAllDeviationsReported.Location = New Point(406, wrnACCAllDeviationsReported.Location.Y)
            Else
                wrnACCAllDeviationsReported.Location = New Point(329, wrnACCAllDeviationsReported.Location.Y)
            End If
        End If
    End Sub

    Private Sub ValidateACCResubmittalRequested()
        If (rdbACCResubmittalRequestedYes.Checked Or rdbACCResubmittalRequestedNo.Checked) Then
            wrnACCResubmittalRequested.Visible = False
        Else
            wrnACCResubmittalRequested.Visible = True
            If rdbACCResubmittalRequestedUnknown.Visible Then
                wrnACCResubmittalRequested.Location = New Point(406, wrnACCResubmittalRequested.Location.Y)
            Else
                wrnACCResubmittalRequested.Location = New Point(329, wrnACCResubmittalRequested.Location.Y)
            End If
        End If
    End Sub

    Private Sub ValidateDatePostmarked()
        Dim StartDate As Date = CDate("01-Feb-" & (Date.Today.Year - 1))
        Dim Enddate As Date = CDate("30-Jan-" & Date.Today.Year)

        If StartDate <= CDate(DTPACCPostmarked.Value) And CDate(DTPACCPostmarked.Value) <= Enddate Then
            wrnACCDatePostmarked.Visible = False
        Else
            If DTPACCPostmarked.Value > Enddate Then
                wrnACCDatePostmarked.Visible = False
            Else
                If DTPACCPostmarked.Value <= StartDate Then
                    wrnACCDatePostmarked.Visible = False
                End If
            End If
        End If
    End Sub

#End Region

#End Region

#Region " Misc control events "

    Private Sub cboReportSchedule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReportSchedule.SelectedIndexChanged
        Dim year As String = Today.Year

        Select Case cboReportSchedule.Text
            Case "First Quarter"
                If Date.Compare(Today, New Date(year, 3, 31)) > 0 Then
                    DTPReportPeriodStart.Value = New Date(year, 1, 1)
                    DTPReportPeriodEnd.Value = New Date(year, 3, 31)
                Else
                    DTPReportPeriodStart.Value = New Date(year - 1, 1, 1)
                    DTPReportPeriodEnd.Value = New Date(year - 1, 3, 31)
                End If
            Case "Second Quarter"
                If Date.Compare(Today, New Date(year, 6, 30)) > 0 Then
                    DTPReportPeriodStart.Value = New Date(year, 4, 1)
                    DTPReportPeriodEnd.Value = New Date(year, 6, 30)
                Else
                    DTPReportPeriodStart.Value = New Date(year - 1, 4, 1)
                    DTPReportPeriodEnd.Value = New Date(year - 1, 6, 30)
                End If
            Case "Third Quarter"
                If Date.Compare(Today, New Date(year, 9, 30)) > 0 Then
                    DTPReportPeriodStart.Value = New Date(year, 7, 1)
                    DTPReportPeriodEnd.Value = New Date(year, 9, 30)
                Else
                    DTPReportPeriodStart.Value = New Date(year - 1, 7, 1)
                    DTPReportPeriodEnd.Value = New Date(year - 1, 9, 30)
                End If
            Case "Fourth Quarter"
                DTPReportPeriodStart.Value = New Date(year - 1, 10, 1)
                DTPReportPeriodEnd.Value = New Date(year - 1, 12, 31)
            Case "First Semiannual"
                If Date.Compare(Today, New Date(year, 6, 30)) > 0 Then
                    DTPReportPeriodStart.Value = New Date(year, 1, 1)
                    DTPReportPeriodEnd.Value = New Date(year, 6, 30)
                Else
                    DTPReportPeriodStart.Value = New Date(year - 1, 1, 1)
                    DTPReportPeriodEnd.Value = New Date(year - 1, 6, 30)
                End If
            Case "Second Semiannual"
                DTPReportPeriodStart.Value = New Date(year - 1, 7, 1)
                DTPReportPeriodEnd.Value = New Date(year - 1, 12, 31)
            Case "Annual"
                DTPReportPeriodStart.Value = New Date(year - 1, 1, 1)
                DTPReportPeriodEnd.Value = New Date(year - 1, 12, 31)
            Case "Monthly"
                If Today.Month = 1 Then
                    DTPReportPeriodStart.Value = New Date(year - 1, 12, 1)
                    DTPReportPeriodEnd.Value = New Date(year - 1, 12, 31)
                Else
                    DTPReportPeriodStart.Value = New Date(year, Today.Month - 1, 1)
                    DTPReportPeriodEnd.Value = New Date(year, Today.Month, 1).AddDays(-1)
                End If
            Case Else
                DTPReportPeriodStart.Value = Today.AddDays(-1)
                DTPReportPeriodEnd.Value = Today.AddDays(-1)
        End Select
    End Sub

    Private Sub dgrReportResubmittal_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrReportResubmittal.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrReportResubmittal.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrReportResubmittal(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrReportResubmittal(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrReportResubmittal(hti.Row, 2)) Then
                        Else
                            NUPReportSubmittal.Value = dgrReportResubmittal(hti.Row, 0)
                            LoadReportFromSubmittal()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DGRACCResubmittal_MouseUp(sender As Object, e As MouseEventArgs) Handles DGRACCResubmittal.MouseUp
        Dim hti As DataGrid.HitTestInfo = DGRACCResubmittal.HitTest(e.X, e.Y)
        Try
            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(DGRACCResubmittal(hti.Row, 0)) Then
                Else
                    If IsDBNull(DGRACCResubmittal(hti.Row, 1)) Then
                    Else
                        If IsDBNull(DGRACCResubmittal(hti.Row, 2)) Then
                        Else
                            NUPACCSubmittal.Value = DGRACCResubmittal(hti.Row, 0)
                            LoadACCFromSubmittal()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbTestReportChangeDueDate_CheckedChanged(sender As Object, e As EventArgs) Handles chbTestReportChangeDueDate.CheckedChanged
        Try
            If chbTestReportChangeDueDate.Checked = False Then
                DTPTestReportDueDate.Visible = False
            Else
                DTPTestReportDueDate.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DTPTestReportDueDate_TextChanged(sender As Object, e As EventArgs) Handles DTPTestReportDueDate.TextChanged
        Try
            If chbTestReportChangeDueDate.Checked = True Then
                txtTestReportDueDate.Text = DTPTestReportDueDate.Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboNotificationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNotificationType.SelectedIndexChanged
        Try

            Select Case cboNotificationType.Text
                Case "Permit Revocation"
                    txtNotificationTypeOther.Visible = False
                    lblNotificationDate.Text = "Permit Revocation Date:"
                    lblNotificationDate2.Text = "Physical Shutdown:"
                    lblNotificationOther.Text = "NOTE: This will NOT change the facility operating status or CMS status. Your manager will need to make those changes."
                    lblNotificationOther.Visible = True
                    chbEventComplete.Text = "Complete*"
                    dtpNotificationDate.ShowCheckBox = False
                    lblNotificationDue.Text = "(Mandatory Date Field)"
                    lblDateSent.Text = "(Optional Date Field)"
                Case Else
                    txtNotificationTypeOther.Visible = False
                    lblNotificationDate.Text = "Notification Due Date:"
                    lblNotificationDate2.Text = "Date Sent by Facility:"
                    lblNotificationOther.Text = ""
                    lblNotificationOther.Visible = False
                    chbEventComplete.Text = "Complete"
                    dtpNotificationDate.ShowCheckBox = True
                    lblNotificationDue.Text = "(Do not check if no due date)"
                    lblDateSent.Text = "(Do not check if date is unknown)"
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbEventComplete_CheckedChanged(sender As Object, e As EventArgs) Handles chbEventComplete.CheckedChanged
        Try
            If AccountFormAccess(49, 1) = "1" Or AccountFormAccess(49, 2) = "1" Or AccountFormAccess(49, 3) = "1" Or AccountFormAccess(49, 4) = "1" Then
                chbEventComplete.Enabled = True
                CompleteReport()
            Else
                chbEventComplete.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReportMoreOptions_Click(sender As Object, e As EventArgs) Handles btnReportMoreOptions.Click
        Try
            Dim tempWidth As String = dgrReportResubmittal.Size.Width
            Dim tempHeight As String = dgrReportResubmittal.Size.Height

            If tempWidth <= 11 Then
                dgrReportResubmittal.Size = New Size(200, tempHeight)
            Else
                dgrReportResubmittal.Size = New Size(10, tempHeight)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewTestReport_Click(sender As Object, e As EventArgs) Handles btnViewTestReport.Click
        Try
            If txtISMPReferenceNumber.Text <> "N/A" Then
                OpenFormTestReportPrintout(txtISMPReferenceNumber.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnACCSubmittals_Click(sender As Object, e As EventArgs) Handles btnACCSubmittals.Click
        Try
            Dim tempWidth As String = DGRACCResubmittal.Size.Width
            Dim tempHeight As String = DGRACCResubmittal.Size.Height

            If tempWidth <= 11 Then
                DGRACCResubmittal.Size = New Size(200, tempHeight)
            Else
                DGRACCResubmittal.Size = New Size(10, tempHeight)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbReportReceivedByAPB_CheckedChanged(sender As Object, e As EventArgs) Handles chbReportReceivedByAPB.CheckedChanged
        Try
            If chbReportReceivedByAPB.Checked = True Then
                DTPReportReceivedDate.Enabled = True
            Else
                DTPReportReceivedDate.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbISMPTestReportReceivedByAPB_CheckedChanged(sender As Object, e As EventArgs) Handles chbISMPTestReportReceivedByAPB.CheckedChanged
        Try
            If chbISMPTestReportReceivedByAPB.Checked = True Then
                DTPTestReportReceivedDate.Enabled = True
            Else
                DTPTestReportReceivedDate.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbNotificationReceivedByAPB_CheckedChanged(sender As Object, e As EventArgs) Handles chbNotificationReceivedByAPB.CheckedChanged
        Try
            If chbNotificationReceivedByAPB.Checked = True Then
                DTPNotificationReceived.Enabled = True
            Else
                DTPNotificationReceived.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbACCReceivedByAPB_CheckedChanged(sender As Object, e As EventArgs) Handles chbACCReceivedByAPB.CheckedChanged
        Try
            If chbACCReceivedByAPB.Checked = True Then
                DTPACCReceivedDate.Enabled = True
            Else
                DTPACCReceivedDate.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Special ACC functions "

    Private Sub PrintACC()
        LoadACC()
        If Not dtpAccReportingYear.Checked Then
            MsgBox("Please save a reporting year before printing this ACC.", MsgBoxStyle.Critical, "Print Error")
            Exit Sub
        End If
        Try
            Dim acc As Acc = LoadAccFromForm()
            Dim accList As New List(Of Acc) From {acc}

            Using dataTable As DataTable = ConvertToDataTable(Of Acc)(accList)
                Dim title As String = acc.AccReportingYear & " ACC for " & acc.Facility.AirsNumber.FormattedString
                Dim crv As New CRViewerForm(New CR.Reports.AccMemo, dataTable, title:=title)
                crv.Show()
            End Using
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function LoadAccFromForm() As Acc
        Dim thisAcc As New Acc

        With thisAcc
            If dtpAccReportingYear.Checked Then .AccReportingYear = dtpAccReportingYear.Value.Year
            .AllDeviationsReported = rdbACCAllDeviationsReportedYes.Checked
            .AllTitleVConditionsListed = rdbACCConditionsYes.Checked
            .Comments = txtACCComments.Text
            .CorrectFormsUsed = rdbACCCorrectACCYes.Checked
            .CorrectlyFilledOut = rdbACCCorrectYes.Checked
            If DTPAcknowledgmentLetterSent.Checked Then .DateAcknowledgmentLetterSent = DTPAcknowledgmentLetterSent.Value
            If chbEventComplete.Checked Then .DateComplete = DTPEventCompleteDate.Value
            .DatePostmarked = DTPACCPostmarked.Value
            .DateReceived = DTPACCReceivedDate.Value
            .Deleted = ItemIsDeleted
            .DeviationsReported = rdbACCDeviationsReportedYes.Checked
            .EnforcementNeeded = rdbACCEnforcementNeededYes.Checked
            .Facility = DAL.GetFacility(AirsNumber)
            .PostmarkedByDeadline = rdbACCPostmarkYes.Checked
            .ResubmittalRequested = rdbACCResubmittalRequestedYes.Checked
            .SignedByResponsibleOfficial = rdbACCROYes.Checked
            .StaffResponsible = DAL.GetIaipUserByUserId(cboStaffResponsible.SelectedValue)
            .UnreportedDeviationsReported = rdbACCPreviouslyUnreportedDeviationsYes.Checked
        End With

        Return thisAcc
    End Function

#End Region

#Region " Main menu/toolbar "

    Private Sub mmiClose_Click(sender As Object, e As EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiDelete_Click(sender As Object, e As EventArgs) Handles mmiDelete.Click
        DeleteSSCPData()
    End Sub

    Private Sub mmiSave_Click(sender As Object, e As EventArgs) Handles mmiSave.Click
        SaveMaster()
    End Sub

    Private Sub mmiPrint_Click(sender As Object, e As EventArgs) Handles mmiPrint.Click
        PrintACC()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveMaster()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintACC()
    End Sub

#End Region

End Class
