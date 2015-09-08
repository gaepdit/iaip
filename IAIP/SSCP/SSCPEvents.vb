Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic


Public Class SSCPEvents
    Inherits BaseForm
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsNotifications As DataSet
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cboStaffResponsible As System.Windows.Forms.ComboBox
    Dim daNotifications As OracleDataAdapter
    Dim dsStaff As DataSet
    Dim daStaff As OracleDataAdapter

    Dim ItemIsDeleted As Boolean = False
    Dim AIRSNumber As String = ""

#Region " Properties "

    Private eventType As Apb.Sscp.WorkItem.WorkItemEventType

#End Region

#Region " Form load "

    Private Sub SSCP_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            tbbPrint.Enabled = False
            tbbPrint.Visible = False

            DefaultDateTimePickers()
            Loadcombos()

            If AccountFormAccess(49, 2) = "1" Or AccountFormAccess(49, 3) = "1" Or AccountFormAccess(49, 4) = "1" Then
                tbToolbar.Visible = True
                mmiSave.Visible = True
            Else
                tbToolbar.Visible = False
                mmiSave.Visible = False
                If txtEnforcementNumber.Text = "" Or txtEnforcementNumber.Text = "N/A" Then
                    btnEnforcementProcess.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, "SSCPEvents.SSCP_Reports_Load")
        End Try
    End Sub

    Private Sub SSCPEvents_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            ShowCorrectTab()
        Catch ex As Exception
            ErrorReport(ex, txtTrackingNumber.Text, "SSCPEvents.SSCP_Reports_Shown")
        End Try
    End Sub

#End Region

#Region "Page Load Functions"

    Private Sub Loadcombos()
        Dim dtStaff As New DataTable

        Dim drNewRow As DataRow
        Dim drDSRow As DataRow

        Try

            SQL = "select numuserID, Staff as StaffName, strLastName " & _
            "from AIRBranch.VW_ComplianceStaff "


            dsStaff = New DataSet

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daStaff.Fill(dsStaff, "Staff")

            dtStaff.Columns.Add("numUserID", GetType(System.String))
            dtStaff.Columns.Add("StaffName", GetType(System.String))

            drNewRow = dtStaff.NewRow()
            drNewRow("numUserID") = "0"
            drNewRow("StaffName") = "N/A"
            dtStaff.Rows.Add(drNewRow)

            For Each drDSRow In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaff.NewRow
                drNewRow("numUserID") = drDSRow("numUserID")
                drNewRow("StaffName") = drDSRow("StaffName")
                dtStaff.Rows.Add(drNewRow)
            Next

            With cboStaffResponsible
                .DataSource = dtStaff
                .DisplayMember = "StaffName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ShowCorrectTab()
        Dim EventTypeDbString As String = ""
        Dim ReceivedDate As String = ""

        SQL = "Select * from AIRBRANCH.SSCPItemMaster " & _
        "where strTrackingNumber = '" & txtTrackingNumber.Text & "'"

        If CurrentConnection.State = ConnectionState.Closed Then
            CurrentConnection.Open()
        End If

        cmd = New OracleCommand(SQL, CurrentConnection)
        dr = cmd.ExecuteReader

        While dr.Read
            If IsDBNull(dr("strEventType")) Then
                EventTypeDbString = ""
            Else
                EventTypeDbString = dr.Item("strEventType")
            End If
            If IsDBNull(dr.Item("DatReceivedDate")) Then
                ReceivedDate = OracleDate
            Else
                ReceivedDate = Format(dr.Item("DatReceivedDate"), "dd-MMM-yyyy")
            End If
            If IsDBNull(dr.Item("strAIRSNumber")) Then
                AIRSNumber = ""
            Else
                txtFacilityInformation.Text = "AIRS # - " & Mid(dr.Item("strAIRSNumber"), 5)
                txtAIRSNumber.Text = Mid(dr.Item("strAIRSNumber"), 5)
                AIRSNumber = txtAIRSNumber.Text
            End If
            If IsDBNull(dr.Item("datAcknoledgmentLetterSent")) Then
                DTPAcknowledgmentLetterSent.Text = OracleDate
                DTPAcknowledgmentLetterSent.Checked = False
            Else
                DTPAcknowledgmentLetterSent.Text = Format(dr.Item("datAcknoledgmentlettersent"), "dd-MMM-yyyy")
                DTPAcknowledgmentLetterSent.Checked = True
            End If

        End While

        Select Case EventTypeDbString

            Case "01" ' Report
                Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.Report
                TCItems.TabPages.Clear()
                TCItems.TabPages.Add(TPReport)
                DTPReportReceivedDate.Text = ReceivedDate
                LoadHeader()
                AddReportsCombo()
                LoadReport()
                LoadReportSubmittalDGR()
                FormatReportsDGR()

            Case "02" ' Inspection
                Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.Inspection
                TCItems.TabPages.Clear()
                TCItems.TabPages.Add(TPInspection)
                DTPInspectionDateStart.Text = ReceivedDate
                DTPInspectionDateEnd.Text = ReceivedDate
                LoadHeader()
                FillInspectionCombos()
                LoadInspection()

            Case "03" ' Test report
                Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.StackTest
                TCItems.TabPages.Clear()
                TCItems.TabPages.Add(TPTestReports)
                txtTestReportReceivedbySSCPDate.Text = ReceivedDate
                LoadHeader()
                LoadTestReport()

            Case "04" ' ACC
                Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.TvAcc
                TCItems.TabPages.Clear()
                TCItems.TabPages.Add(TPACC)
                DTPACCReceivedDate.Text = ReceivedDate
                DTPACCPostmarked.Text = OracleDate
                dtpAccReportingYear.Value = DateTime.Today.AddYears(-1)
                dtpAccReportingYear.Checked = True
                LoadHeader()
                LoadACC()
                LoadACCSubmittalDGR()
                FormatACCDGR()
                tbbPrint.Enabled = True
                tbbPrint.Visible = True

            Case "05" ' Notification
                Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.Notification
                TCItems.TabPages.Clear()
                TCItems.TabPages.Add(TPNotifications)
                DTPNotificationReceived.Text = ReceivedDate
                LoadHeader()
                FillNotificationCombos()
                LoadNotification()
                btnEnforcementProcess.Visible = False

            Case "07" ' Risk Management Plan Inspection
                Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.RmpInspection
                TCItems.TabPages.Clear()
                TCItems.TabPages.Add(TPInspection)
                TPInspection.Text = "Risk Mgmt. Plan Inspection"
                DTPInspectionDateStart.Text = ReceivedDate
                DTPInspectionDateEnd.Text = ReceivedDate
                LoadHeader()
                FillInspectionCombos()
                LoadInspection()

            Case Else
                LoadHeader()

        End Select

        CheckCompleteDate()
        CompleteReport()
        CheckEnforcement()

    End Sub
    Sub AddReportsCombo()
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
    Sub FillInspectionCombos()

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
    Sub FillNotificationCombos()
        Dim dtNotification As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        If CurrentConnection.State = ConnectionState.Closed Then
            CurrentConnection.Open()
        End If

        dsNotifications = New DataSet

        SQL = "Select strNotificationKey, strNotificationDESC " & _
        "from AIRBRANCH.LookUPSSCPNotifications " & _
        "order by strNotificationDESC "

        daNotifications = New OracleDataAdapter(SQL, CurrentConnection)

        daNotifications.Fill(dsNotifications, "Notifications")

        dtNotification.Columns.Add("strNotificationDESC", GetType(System.String))
        dtNotification.Columns.Add("strNotificationKey", GetType(System.String))

        drNewRow = dtNotification.NewRow()
        drNewRow("strNotificationDESC") = " "
        drNewRow("strNotificationKey") = " "
        dtNotification.Rows.Add(drNewRow)

        For Each drDSRow In dsNotifications.Tables("Notifications").Rows()
            drNewRow = dtNotification.NewRow()
            drNewRow("strNotificationDESC") = drDSRow("strNotificationDESC")
            drNewRow("strNotificationKey") = drDSRow("strNotificationKey")
            dtNotification.Rows.Add(drNewRow)
        Next

        With cboNotificationType
            .DataSource = dtNotification
            .DisplayMember = "strNotificationDESC"
            .ValueMember = "strNotificationKey"
            .SelectedIndex = 0
        End With

    End Sub
    Sub LoadHeader()
        Dim temp As String
        Dim Staff As String = ""
        Dim DelStatus As String = ""

        Try
            SQL = "Select " & _
            "strFacilityName, strFacilityStreet1, " & _
            "strFacilityCity, strFacilityState, " & _
            "strFacilityZipCode, strCountyName, " & _
            "strClass, strAIRProgramCodes, " & _
            "strlastname, strfirstname, " & _
            "strResponsibleStaff, " & _
            "strDelete " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.LookUpCountyInformation,  " & _
            "AIRBRANCH.APBHeaderData, AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSCPItemMaster  " & _
            "where AIRBRANCH.APBFacilityInformation.strAIRSNUMBER = AIRBRANCH.APBHeaderData.strAIRSNUmber " & _
            "and AIRBRANCH.LookUpCountyInformation.strCountyCode = substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3)  " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCPItemMaster.strAIRSNumber  " & _
            "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " & _
            "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = '" & txtTrackingNumber.Text & "' "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist Then
                If IsDBNull(dr.Item("strDelete")) Then
                    DelStatus = ""
                Else
                    DelStatus = dr.Item("strDelete")
                End If
                If DelStatus <> "" Then
                    ItemIsDeleted = True
                    txtFacilityInformation.Text = "FLAGGED AS DELETED"
                End If

                temp = Mid(dr.Item("strFacilityZipCode"), 1, 5)
                If Mid(dr.Item("strFacilityZipCode"), 6) <> "" Then
                    temp = temp & "-" & Mid(dr.Item("strFacilityZipCode"), 6)
                End If

                txtFacilityInformation.Text = txtFacilityInformation.Text & vbCrLf & _
                dr.Item("strFacilityName") & vbCrLf & _
                dr.Item("strFacilityStreet1") & vbCrLf & _
                dr.Item("StrFacilityCity") & ", " & dr.Item("strFacilityState") & " " & temp & _
                vbCrLf & vbCrLf & _
                "County - " & dr.Item("strCountyName")

                txtEventInformation.Text = "Tracking # - " & txtTrackingNumber.Text & vbCrLf & _
                "Staff Responsible - " & dr.Item("strFirstName") & " " & dr.Item("strLastName") & vbCrLf & _
                "Classification - " & dr.Item("strClass") & vbCrLf & _
                "Air Program Code(s) - " & vbCrLf

                If Me.eventType = Apb.Sscp.WorkItem.WorkItemEventType.Inspection Then
                    Dim geosInspectionId As String = DAL.SSCP.GetGeosInspectionId(txtTrackingNumber.Text)
                    If geosInspectionId <> "" Then
                        txtEventInformation.Text = "GEOS Inspection ID " & geosInspectionId & vbNewLine & txtEventInformation.Text
                    End If
                End If

                AddAirProgramCodes(dr.Item("StrAirProgramCodes"))

                If IsDBNull(dr.Item("strResponsibleStaff")) Then
                    Staff = "0"
                Else
                    Staff = dr.Item("strResponsibleStaff")
                End If
            End If

            If Staff <> "" Then
                cboStaffResponsible.SelectedValue = Staff
            End If


            SQL = "Select strRMPID " & _
            "from AIRBRANCH.APBSupplamentalData " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strRMPID")) Then
                    txtRMPID.Clear()
                    lblRMPID.Visible = False
                    txtRMPID.Visible = False

                Else
                    txtRMPID.Text = dr.Item("strRMPID")
                    lblRMPID.Visible = True
                    txtRMPID.Visible = True

                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Dim AirList As String = ""

        If Mid(AirProgramCode, 1, 1) = 1 Then
            AirList = vbTab & "0 - SIP" & vbCrLf
        End If
        If Mid(AirProgramCode, 2, 1) = 1 Then
            AirList = AirList & vbTab & "1 - Federal SIP" & vbCrLf
        End If
        If Mid(AirProgramCode, 3, 1) = 1 Then
            AirList = AirList & vbTab & "3 - Non-Federal SIP" & vbCrLf
        End If
        If Mid(AirProgramCode, 4, 1) = 1 Then
            AirList = AirList & vbTab & "4 - CFC Tracking" & vbCrLf
        End If
        If Mid(AirProgramCode, 5, 1) = 1 Then
            AirList = AirList & vbTab & "6 - PSD" & vbCrLf
        End If
        If Mid(AirProgramCode, 6, 1) = 1 Then
            AirList = AirList & vbTab & "7 - NSR" & vbCrLf
        End If
        If Mid(AirProgramCode, 7, 1) = 1 Then
            AirList = AirList & vbTab & "8 - NESHAP" & vbCrLf
        End If
        If Mid(AirProgramCode, 8, 1) = 1 Then
            AirList = AirList & vbTab & "9 - NSPS" & vbCrLf
        End If
        If Mid(AirProgramCode, 9, 1) = 1 Then
            AirList = AirList & vbTab & "F - FESOP" & vbCrLf
        End If
        If Mid(AirProgramCode, 10, 1) = 1 Then
            AirList = AirList & vbTab & "A - Acid Precipitation" & vbCrLf
        End If
        If Mid(AirProgramCode, 11, 1) = 1 Then
            AirList = AirList & vbTab & "I - Native American" & vbCrLf
        End If
        If Mid(AirProgramCode, 12, 1) = 1 Then
            AirList = AirList & vbTab & "M - MACT" & vbCrLf
        End If
        If Mid(AirProgramCode, 13, 1) = 1 Then
            AirList = AirList & vbTab & "V - Title V Permit" & vbCrLf
        End If
        If Mid(AirProgramCode, 14, 1) = 1 Then
            AirList = AirList & vbTab & "RMP - Risk Mgmt. Plan" & vbCrLf
        End If
        If AirList = "" Then
            AirList = vbTab & "No Air Program Codes available" & vbCrLf
        End If
        AirList = Mid(AirList, 1, (Len(AirList) - 2))

        txtEventInformation.Text = txtEventInformation.Text & AirList

    End Sub
    Sub DefaultDateTimePickers()

        DTPAcknowledgmentLetterSent.Value = OracleDate
        DTPReportPeriodStart.Value = OracleDate
        DTPReportPeriodEnd.Value = OracleDate
        dtpDueDate.Value = OracleDate
        DTPSentDate.Value = OracleDate
        DTPInspectionDateStart.Value = OracleDate
        DTPInspectionDateEnd.Value = OracleDate
        dtpInspectionTimeStart.Text = "8:00:00 AM"
        dtpInspectionTimeEnd.Text = "12:00:00 PM"

        NUPReportSubmittal.Value = 1
        DTPTestReportDueDate.Text = Date.Today

        DTPTestReportNewDueDate.Text = Date.Today

    End Sub
    Sub CheckCompleteDate()
        Dim Completedate As String = ""

        SQL = "Select datCompleteDate " & _
        "from AIRBRANCH.SSCPItemMaster " & _
        "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

        cmd = New OracleCommand(SQL, CurrentConnection)
        If CurrentConnection.State = ConnectionState.Closed Then
            CurrentConnection.Open()
        End If
        dr = cmd.ExecuteReader
        recExist = dr.Read
        If recExist = True Then
            If IsDBNull(dr.Item("datCompleteDate")) Then
                Completedate = ""
            Else
                Completedate = dr.Item("datCompleteDate")
            End If
        End If
        dr.Close()

        If Completedate = "" Then
            chbEventComplete.Checked = False
            DTPEventCompleteDate.Text = OracleDate
        Else
            chbEventComplete.Checked = True
            DTPEventCompleteDate.Text = Completedate
        End If

    End Sub
    Sub CompleteReport()
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
            'lblInspectionScheduleLink.Enabled = False

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
            'lblInspectionScheduleLink.Enabled = True

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

                'chbACCReceivedByAPB.Enabled = False
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
    Sub CheckEnforcement()
        Dim enfNum As String = ""
        If DAL.Sscp.EnforcementExistsForTrackingNumber(txtTrackingNumber.Text, enfNum) Then
            txtEnforcementNumber.Text = enfNum
            txtEnforcementNumber.Visible = True
        Else
            txtEnforcementNumber.Text = "N/A"
            txtEnforcementNumber.Visible = False
        End If
    End Sub
#End Region

    Private Sub cboReportSchedule_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboReportSchedule.TextChanged
        Dim Year As String
        Try

            Year = Date.Now.Year

            Select Case cboReportSchedule.Text
                Case "First Quarter"
                    DTPReportPeriodStart.Value = "1-Jan-" & Year
                    DTPReportPeriodEnd.Value = "31-Mar-" & Year
                Case "Second Quarter"
                    DTPReportPeriodStart.Value = "1-Apr-" & Year
                    DTPReportPeriodEnd.Value = "30-Jun-" & Year
                Case "Third Quarter"
                    DTPReportPeriodStart.Value = "1-Jul-" & Year
                    DTPReportPeriodEnd.Value = "30-Sep-" & Year
                Case "Fourth Quarter"
                    DTPReportPeriodStart.Value = "1-Oct-" & Year
                    DTPReportPeriodEnd.Value = "31-Dec-" & Year
                Case "First Semiannual"
                    DTPReportPeriodStart.Value = "1-Jan-" & Year
                    DTPReportPeriodEnd.Value = "30-Jun-" & Year
                Case "Second Semiannual"
                    DTPReportPeriodStart.Value = "1-Jul-" & Year
                    DTPReportPeriodEnd.Value = "31-Dec-" & Year
                Case "Annual"
                    DTPReportPeriodStart.Value = "1-Jan-" & Year
                    DTPReportPeriodEnd.Value = "31-Dec-" & Year
                Case "Other"
                    DTPReportPeriodStart.Value = "1-Jul-" & Year
                    DTPReportPeriodEnd.Value = "1-Jul-" & Year
                Case "Monthly"
                    DTPReportPeriodStart.Value = Date.Today.AddMonths(-1)
                    DTPReportPeriodEnd.Value = Date.Today
                Case "Malfunction/Deviation"
                    DTPReportPeriodStart.Value = OracleDate
                    DTPReportPeriodEnd.Value = OracleDate
                Case Else
                    DTPReportPeriodStart.Value = OracleDate
                    DTPReportPeriodEnd.Value = OracleDate
            End Select



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#Region "Opening Enforcement Actions"
    Private Sub OpenEnforcement()
        Try
            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" And txtFacilityInformation.Text <> "" Then
                OpenFormEnforcement(txtEnforcementNumber.Text)
            Else
                Dim parameters As New Dictionary(Of String, String)
                parameters("airsnumber") = txtAIRSNumber.Text
                If txtTrackingNumber.Text <> "" Then parameters("trackingnumber") = txtTrackingNumber.Text
                OpenSingleForm(SSCPEnforcementSelector, parameters:=parameters, closeFirst:=True)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Saves"
    Sub SaveMaster()
        Try

            If AccountFormAccess(49, 2) = "0" And AccountFormAccess(49, 3) = "0" And AccountFormAccess(49, 4) = "0" Then
                MsgBox("You do not have sufficent permission to save Compliance Events.", MsgBoxStyle.Information, "Compliance Events")
            Else
                If TPReport.Focus = True Then
                    SaveReport()
                    LoadReportSubmittalDGR()
                End If
                If TPInspection.Focus = True Then
                    SaveInspection()
                End If
                If TPACC.Focus = True Then
                    SaveACC()
                    LoadACCSubmittalDGR()
                End If
                If TPTestReports.Focus = True Then
                    SaveISMPTestReport()
                End If
                If TPNotifications.Focus = True Then

                    If cboNotificationType.SelectedValue = "07" Then
                        MsgBox("Malfunctions are no longer saved as notifications." & vbCrLf & _
                               "Please save this malfunction as a Report.", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    If cboNotificationType.SelectedValue = "08" Then
                        MsgBox("Deviations are no longer saved as notifications." & vbCrLf & _
                               "Please save this Deviation as a Report.", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    SaveNotifications()
                End If
                SaveDate()

                MsgBox("Save Complete", MsgBoxStyle.Information, "SSCP Events")


            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveReport()
        Dim PeriodComments As String
        Dim GeneralComments As String
        Dim Completeness As String
        Dim NeedsEnforcement As String
        Dim Deviation As String

        Try

            ValidateALLReport()

            If wrnCompleteReport.Visible = True Or wrnEnforcementNeeded.Visible = True _
                    Or wrnReportPeriod.Visible = True Or wrnShowDeviation.Visible = True _
                    Or wrnReportSubmittal.Visible = True Then
                MsgBox("Data not saved")
            Else
                If rdbReportCompleteYes.Checked = True Then
                    Completeness = "True"
                Else
                    Completeness = "False"
                End If
                If rdbReportDeviationYes.Checked = True Then
                    Deviation = "True"
                Else
                    Deviation = "False"
                End If
                If rdbReportEnforcementYes.Checked = True Then
                    NeedsEnforcement = "True"
                Else
                    NeedsEnforcement = "False"
                End If
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

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select strTrackingNumber from AIRBRANCH.SSCPREports where " & _
                "strTrackingNumber = '" & txtTrackingNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read

                If recExist = False Then
                    SQL = "Insert into AIRBRANCH.SSCPREports " & _
                    "(strTrackingNumber, strReportPeriod, " & _
                    "DatReportingPeriodStart, DatReportingPeriodEnd, " & _
                    "strReportingPeriodComments, datreportduedate, " & _
                    "datsentbyfacilitydate, strcompletestatus, " & _
                    "strenforcementneeded, strshowdeviation, " & _
                    "strgeneralcomments, strmodifingperson, " & _
                    "datmodifingdate, strSubmittalNumber) " & _
                    "values " & _
                    "('" & txtTrackingNumber.Text & "', '" & cboReportSchedule.Text & "', " & _
                    "'" & DTPReportPeriodStart.Text & "', '" & DTPReportPeriodEnd.Text & "', " & _
                    "'" & Replace(PeriodComments, "'", "''") & "', '" & dtpDueDate.Text & "', " & _
                    "'" & DTPSentDate.Text & "', '" & Completeness & "', " & _
                    "'" & NeedsEnforcement & "', '" & Deviation & "', " & _
                    "'" & Replace(GeneralComments, "'", "''") & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "', '1')"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader

                    SQL = "Insert into AIRBRANCH.SSCPREportsHistory " & _
                    "(strTrackingNumber, strSubmittalNumber, " & _
                    "strReportPeriod, DatReportingPeriodStart, " & _
                    "DatReportingPeriodEnd, strReportingPeriodComments, " & _
                    "datreportduedate, datsentbyfacilitydate, " & _
                    "strcompletestatus, strenforcementneeded, " & _
                    "strshowdeviation, strgeneralcomments, " & _
                    "strmodifingperson, datmodifingdate) " & _
                    "values " & _
                    "('" & txtTrackingNumber.Text & "', '1', " & _
                    "'" & cboReportSchedule.Text & "', '" & DTPReportPeriodStart.Text & "', " & _
                    "'" & DTPReportPeriodEnd.Text & "', '" & Replace(PeriodComments, "'", "''") & "', " & _
                    "'" & dtpDueDate.Text & "', '" & DTPSentDate.Text & "', " & _
                    "'" & Completeness & "', '" & NeedsEnforcement & "', " & _
                    "'" & Deviation & "', '" & Replace(GeneralComments, "'", "''") & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "')"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader

                Else
                    SQL = "Update AIRBRANCH.SSCPREports set " & _
                    "strSubmittalNumber = '" & NUPReportSubmittal.Value & "', " & _
                    "strReportPeriod = '" & cboReportSchedule.Text & "', " & _
                    "DatReportingPeriodStart = '" & DTPReportPeriodStart.Text & "', " & _
                    "DatReportingPeriodEnd = '" & DTPReportPeriodEnd.Text & "', " & _
                    "strReportingPeriodComments = '" & Replace(PeriodComments, "'", "''") & "', " & _
                    "datreportduedate = '" & dtpDueDate.Text & "', " & _
                    "datsentbyfacilitydate = '" & DTPSentDate.Text & "', " & _
                    "strcompletestatus= '" & Completeness & "', " & _
                    "strenforcementneeded = '" & NeedsEnforcement & "', " & _
                    "strshowdeviation = '" & Deviation & "', " & _
                    "strgeneralcomments = '" & Replace(GeneralComments, "'", "''") & "', " & _
                    "strmodifingperson = '" & UserGCode & "', " & _
                    "datmodifingdate = '" & OracleDate & "' " & _
                    "where strTrackingNumber = '" & txtTrackingNumber.Text & "'"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select strSubmittalNumber " & _
                    "from AIRBRANCH.SSCPREportsHistory " & _
                    "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
                    "and strSubmittalNumber = '" & NUPReportSubmittal.Value & "'"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        SQL = "Update AIRBRANCH.SSCPREportsHistory set " & _
                         "strSubmittalNumber = '" & NUPReportSubmittal.Value & "', " & _
                         "strReportPeriod = '" & cboReportSchedule.Text & "', " & _
                         "DatReportingPeriodStart = '" & DTPReportPeriodStart.Text & "', " & _
                         "DatReportingPeriodEnd = '" & DTPReportPeriodEnd.Text & "', " & _
                         "strReportingPeriodComments = '" & Replace(PeriodComments, "'", "''") & "', " & _
                         "datreportduedate = '" & dtpDueDate.Text & "', " & _
                         "datsentbyfacilitydate = '" & DTPSentDate.Text & "', " & _
                         "strcompletestatus= '" & Completeness & "', " & _
                         "strenforcementneeded = '" & NeedsEnforcement & "', " & _
                         "strshowdeviation = '" & Deviation & "', " & _
                         "strgeneralcomments = '" & Replace(GeneralComments, "'", "''") & "', " & _
                         "strmodifingperson = '" & UserGCode & "', " & _
                         "datmodifingdate = '" & OracleDate & "' " & _
                         "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
                         "and strSubmittalNumber = '" & NUPReportSubmittal.Value & "'"
                    Else
                        SQL = "Insert into AIRBRANCH.SSCPREportsHistory " & _
                        "(strTrackingNumber, strSubmittalNumber, " & _
                        "strReportPeriod, DatReportingPeriodStart, " & _
                        "DatReportingPeriodEnd, strReportingPeriodComments, " & _
                        "datreportduedate, datsentbyfacilitydate, " & _
                        "strcompletestatus, strenforcementneeded, " & _
                        "strshowdeviation, strgeneralcomments, " & _
                        "strmodifingperson, datmodifingdate) " & _
                        "values " & _
                        "('" & txtTrackingNumber.Text & "', '" & NUPReportSubmittal.Value & "', " & _
                        "'" & cboReportSchedule.Text & "', '" & DTPReportPeriodStart.Text & "', " & _
                        "'" & DTPReportPeriodEnd.Text & "', '" & Replace(PeriodComments, "'", "''") & "', " & _
                        "'" & dtpDueDate.Text & "', '" & DTPSentDate.Text & "', " & _
                        "'" & Completeness & "', '" & NeedsEnforcement & "', " & _
                        "'" & Deviation & "', '" & Replace(GeneralComments, "'", "''") & "', " & _
                        "'" & UserGCode & "', '" & OracleDate & "')"
                    End If

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    dr.Close()

                    If Me.chbReportReceivedByAPB.Checked = True Then
                        SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                        "datReceivedDate = '" & Me.DTPReportReceivedDate.Text & "' " & _
                        "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If

                End If 'If recExist = False Then
            End If ' MsgBox("Data not saved")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveInspection()
        Dim InspectionTimeStart As String
        Dim InspectionTimeEnd As String
        Dim InspectionGuide As String
        Dim InspectionComments As String
        Dim OperatingStatus As String
        Dim EnforcementFollowUp As String = False
        Dim InspectionReason As String
        Dim WeatherCondition As String

        Try

            ValidateAllInspection()

            If wrnInspectionOperating.Visible = True _
               Or wrnInspectionComplianceStatus.Visible = True _
                Or wrnInspectionDates.Visible = True Then
                MsgBox("Data not saved")
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
                If rdbInspectionFacilityOperatingYes.Checked = True Then
                    OperatingStatus = "True"
                Else
                    OperatingStatus = "False"
                End If
                If rdbInspectionFollowUpYes.Checked = True Then
                    EnforcementFollowUp = "True"
                Else
                    EnforcementFollowUp = "False"
                End If

                InspectionTimeStart = DTPInspectionDateStart.Text & " " & dtpInspectionTimeStart.Text
                InspectionTimeEnd = DTPInspectionDateEnd.Text & " " & dtpInspectionTimeEnd.Text

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select * from AIRBRANCH.SSCPInspections " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read

                If recExist = False Then
                    SQL = "Insert into AIRBRANCH.SSCPInspections " & _
                    "(strTrackingNumber, DatInspectionDateStart, " & _
                    "datinspectionDateEnd, strInspectionReason, " & _
                    "strWeatherConditions, strInspectionGuide, " & _
                    "strFacilityOperating, strInspectionComplianceStatus, " & _
                    "strInspectionComments, " & _
                    "strInspectionFollowUp, strModifingPerson, " & _
                    "datModifingDate) " & _
                    "values " & _
                    "('" & txtTrackingNumber.Text & "', " & _
                    "to_date('" & InspectionTimeStart & "', 'dd.mm.yyyy HH24:mi:ss'), " & _
                    "to_date('" & InspectionTimeEnd & "', 'dd.mm.yyyy HH24:mi:ss'), " & _
                    "'" & Replace(InspectionReason, "'", "''") & "', " & _
                    "'" & Replace(WeatherCondition, "'", "''") & "', '" & Replace(InspectionGuide, "'", "''") & "', " & _
                    "'" & Replace(OperatingStatus, "'", "''") & "', '" & cboInspectionComplianceStatus.Text & "', " & _
                    "'" & Replace(InspectionComments, "'", "''") & "', " & _
                    "'" & EnforcementFollowUp & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "')"
                Else
                    SQL = "Update AIRBRANCH.SSCPInspections set " & _
                    "DatInspectionDateStart = to_date('" & InspectionTimeStart & "', 'dd.mm.yyyy HH24:mi:ss'), " & _
                    "datinspectionDateEnd = to_date('" & InspectionTimeEnd & "', 'dd.mm.yyyy HH24:mi:ss'), " & _
                    "strInspectionReason = '" & Replace(InspectionReason, "'", "''") & "', " & _
                    "strWeatherConditions = '" & Replace(WeatherCondition, "'", "''") & "', " & _
                    "strInspectionGuide = '" & Replace(InspectionGuide, "'", "''") & "', " & _
                    "strFacilityOperating = '" & Replace(OperatingStatus, "'", "''") & "', " & _
                    "strInspectionComplianceStatus = '" & cboInspectionComplianceStatus.Text & "', " & _
                    "strInspectionComments = '" & Replace(InspectionComments, "'", "''") & "', " & _
                    "strInspectionFollowUp = '" & EnforcementFollowUp & "', " & _
                    "strModifingPerson = '" & UserGCode & "', " & _
                    "datModifingDate = '" & OracleDate & "' " & _
                    "where strtrackingNumber = '" & txtTrackingNumber.Text & "'"
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader

            End If

        Catch ex As Exception
            ErrorReport(ex, txtTrackingNumber.Text & vbCrLf & SQL, "SSCPEvents.SaveInspection")
        Finally

        End Try


    End Sub
    Sub SaveACC()
        Dim PostedOnTime As String
        Dim SignedByRO As String
        Dim CorrectACCForm As String
        Dim TitleVConditions As String
        Dim ACCCorrectlyFilledOut As String
        Dim ReportedDeviations As String
        Dim ReportedUnReportedDeviations As String
        Dim ACCComments As String
        Dim EnforcementNeeded As String
        Dim AccReportingYear As String
        Dim ResubmittalRequested As String
        Dim AllDeviationsReported As String

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
            Else
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select strTrackingNumber " & _
                "from AIRBRANCH.SSCPACCS where " & _
                "strTrackingNumber = '" & txtTrackingNumber.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

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
                If dtpAccReportingYear.Checked Then
                    AccReportingYear = Format(dtpAccReportingYear.Value, "dd-MMM-yyyy")
                Else
                    AccReportingYear = ""
                End If

                If recExist = False Then
                    NUPACCSubmittal.Text = 1

                    SQL = "Insert into AIRBRANCH.SSCPACCS " & _
                    "(strTrackingNumber, strSubmittalNumber, " & _
                    "strPostMarkedOnTime, DATPostMarkDate, " & _
                    "strsignedbyRO, strCorrectACCFOrms, " & _
                    "strTitleVConditionsListed, strACCCorrectlyFilledOut, " & _
                    "strReportedDeviations, strDeviationsUnreported, " & _
                    "strcomments, strEnforcementneeded, " & _
                    "strModifingPerson, DatModifingDate, datAccReportingYear, " & _
                    "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED) " & _
                    "values " & _
                    "('" & txtTrackingNumber.Text & "', '" & NUPACCSubmittal.Text & "', " & _
                    "'" & PostedOnTime & "', '" & DTPACCPostmarked.Text & "', " & _
                    "'" & SignedByRO & "', '" & CorrectACCForm & "', " & _
                    "'" & TitleVConditions & "', '" & ACCCorrectlyFilledOut & "', " & _
                    "'" & ReportedDeviations & "', '" & ReportedUnReportedDeviations & "', " & _
                    "'" & Replace(ACCComments, "'", "''") & "', " & _
                    "'" & EnforcementNeeded & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "', '" & AccReportingYear & "', " & _
                    "'" & AllDeviationsReported & "', '" & ResubmittalRequested & "')"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader

                    SQL = "Insert into AIRBRANCH.SSCPACCSHistory " & _
                    "(strTrackingNumber, strSubmittalNumber, " & _
                    "strPostMarkedOnTime, DATPostMarkDate, " & _
                    "strsignedbyRO, StrCorrectACCForms, " & _
                    "strTitleVConditionsListed, strACCCorrectlyFilledOut, " & _
                    "strReportedDeviations, strDeviationsUnreported, " & _
                    "strcomments, strEnforcementneeded, " & _
                    "strModifingPerson, DatModifingDate, datAccReportingYear, " & _
                    "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED) " & _
                    "values " & _
                    "('" & txtTrackingNumber.Text & "', '" & NUPACCSubmittal.Text & "', " & _
                    "'" & PostedOnTime & "', '" & DTPACCPostmarked.Text & "', " & _
                    "'" & SignedByRO & "', '" & CorrectACCForm & "', " & _
                    "'" & TitleVConditions & "', '" & ACCCorrectlyFilledOut & "', " & _
                    "'" & ReportedDeviations & "', '" & ReportedUnReportedDeviations & "', " & _
                    "'" & Replace(ACCComments, "'", "''") & "', " & _
                    "'" & EnforcementNeeded & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "', '" & AccReportingYear & "', " & _
                    "'" & AllDeviationsReported & "', '" & ResubmittalRequested & "')"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader

                Else  'recExist = False 
                    SQL = "Update AIRBRANCH.SSCPACCS set " & _
                    "strSubmittalNumber = '" & NUPACCSubmittal.Text & "', " & _
                    "strPostMarkedOnTime = '" & PostedOnTime & "', " & _
                    "DATPostMarkDate = '" & DTPACCPostmarked.Text & "', " & _
                    "strsignedbyRO = '" & SignedByRO & "', " & _
                    "StrCorrectACCFOrms = '" & CorrectACCForm & "', " & _
                    "strTitleVConditionsListed = '" & TitleVConditions & "', " & _
                    "strACCCorrectlyFilledOut = '" & ACCCorrectlyFilledOut & "', " & _
                    "strReportedDeviations =  '" & ReportedDeviations & "', " & _
                    "strDeviationsUnreported = '" & ReportedUnReportedDeviations & "', " & _
                    "strcomments = '" & Replace(ACCComments, "'", "''") & "', " & _
                    "strEnforcementneeded = '" & EnforcementNeeded & "', " & _
                    "strModifingPerson = '" & UserGCode & "', " & _
                    "DatModifingDate = '" & OracleDate & "', " & _
                    "datAccReportingYear = '" & AccReportingYear & "', " & _
                    "STRKNOWNDEVIATIONSREPORTED = '" & AllDeviationsReported & "', " & _
                    "STRRESUBMITTALREQUIRED = '" & ResubmittalRequested & "' " & _
                    "where strTrackingnumber = '" & txtTrackingNumber.Text & "'"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select strSubmittalNumber " & _
                    "from AIRBRANCH.SSCPACCSHistory " & _
                    "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
                    "and strSubmittalNumber = '" & NUPACCSubmittal.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        SQL = "Update AIRBRANCH.SSCPACCSHistory set " & _
                        "strSubmittalNumber = '" & NUPACCSubmittal.Text & "', " & _
                        "strPostMarkedOnTime = '" & PostedOnTime & "', " & _
                        "DATPostMarkDate = '" & DTPACCPostmarked.Text & "', " & _
                        "strsignedbyRO = '" & SignedByRO & "', " & _
                        "StrCorrectACCFOrms = '" & CorrectACCForm & "', " & _
                        "strTitleVConditionsListed = '" & TitleVConditions & "', " & _
                        "strACCCorrectlyFilledOut = '" & ACCCorrectlyFilledOut & "', " & _
                        "strReportedDeviations =  '" & ReportedDeviations & "', " & _
                        "strDeviationsUnreported = '" & ReportedUnReportedDeviations & "', " & _
                        "strcomments = '" & Replace(ACCComments, "'", "''") & "', " & _
                        "strEnforcementneeded = '" & EnforcementNeeded & "', " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "DatModifingDate = '" & OracleDate & "', " & _
                        "datAccReportingYear = '" & AccReportingYear & "', " & _
                        "STRKNOWNDEVIATIONSREPORTED = '" & AllDeviationsReported & "', " & _
                        "STRRESUBMITTALREQUIRED = '" & ResubmittalRequested & "' " & _
                        "where strTrackingnumber = '" & txtTrackingNumber.Text & "' " & _
                        "and strSubmittalNumber = '" & NUPACCSubmittal.Text & "'"
                    Else
                        SQL = "Insert into AIRBRANCH.SSCPACCSHistory " & _
                        "(strTrackingNumber, strSubmittalNumber, " & _
                        "strPostMarkedOnTime, DATPostMarkDate, " & _
                        "strsignedbyRO, StrCorrectACCForms, " & _
                        "strTitleVConditionsListed, strACCCorrectlyFilledOut, " & _
                        "strReportedDeviations, strDeviationsUnreported, " & _
                        "strcomments, strEnforcementneeded, " & _
                        "strModifingPerson, DatModifingDate, datAccReportingYear, " & _
                        "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED) " & _
                        "values " & _
                        "('" & txtTrackingNumber.Text & "', '" & NUPACCSubmittal.Text & "', " & _
                        "'" & PostedOnTime & "', '" & DTPACCPostmarked.Text & "', " & _
                        "'" & SignedByRO & "', '" & CorrectACCForm & "', " & _
                        "'" & TitleVConditions & "', '" & ACCCorrectlyFilledOut & "', " & _
                        "'" & ReportedDeviations & "', '" & ReportedUnReportedDeviations & "', " & _
                        "'" & Replace(ACCComments, "'", "''") & "', " & _
                        "'" & EnforcementNeeded & "', '" & UserGCode & "', " & _
                        "'" & OracleDate & "', '" & AccReportingYear & "', " & _
                        "'" & AllDeviationsReported & "', '" & ResubmittalRequested & "')"
                    End If
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader

                    If chbACCReceivedByAPB.Checked = True Then
                        SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                        "datReceivedDate = '" & DTPACCReceivedDate.Text & "' " & _
                        "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If

                End If 'If recExist in the AIRBRANCH.SSCPACCS table
            End If  'Warnings Check

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveISMPTestReport()
        Dim TestReportDue As String
        Dim TestReportComments As String
        Dim TestReportFollowUp As String
        Dim ReferenceNumber As String

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
                TestReportComments = Replace(txtTestReportComments.Text, "'", "''")
            End If
            If txtISMPReferenceNumber.Text = "" Then
                ReferenceNumber = "N/A"
            Else
                ReferenceNumber = txtISMPReferenceNumber.Text
            End If
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            SQL = "Select strTrackingNumber " & _
            "from AIRBRANCH.SSCPTestReports " & _
            "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                SQL = "Update AIRBRANCH.SSCPTestReports set " & _
                "strReferenceNumber = '" & ReferenceNumber & "', " & _
                "datTestReportDue = '" & TestReportDue & "', " & _
                "strTestReportComments = '" & Replace(TestReportComments, "'", "''") & "', " & _
                "strTestReportFollowUp = '" & TestReportFollowUp & "', " & _
                "strModifingPerson = '" & UserGCode & "', " & _
                "datModifingDate = '" & OracleDate & "' " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
            Else
                SQL = "Insert into AIRBRANCH.SSCPTestReports " & _
                "(strTrackingNumber, strReferenceNumber, " & _
                "datTestReportDue, " & _
                "strTestReportComments, strTestReportFollowUp, " & _
                "strModifingPerson, datModifingDate) " & _
                "Values " & _
                "('" & txtTrackingNumber.Text & "', '" & ReferenceNumber & "', " & _
                "'" & TestReportDue & "', " & _
                "'" & Replace(TestReportComments, "'", "''") & "', '" & TestReportFollowUp & "', " & _
                "'" & UserGCode & "', '" & OracleDate & "') "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select strAIRSnumber " & _
            "from AIRBRANCH.APBSupplamentalData " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                "datSSCPTestReportDue = '" & DTPTestReportNewDueDate.Text & "' " & _
                "where strAIRSNUmber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Close()
            End If

            If Me.chbISMPTestReportReceivedByAPB.Checked = True Then
                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                "datReceivedDate = '" & Me.DTPTestReportReceivedDate.Text & "' " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveNotifications()
        Dim NotificationDue As String = ""
        Dim NotificationDueDate As String = ""
        Dim NotificationSent As String = ""
        Dim NotificationSentDate As String = ""
        Dim NotificationTypeOther As String = ""
        Dim NotificationComment As String = ""
        Dim NotificationFollowUp As String = ""

        Try

            If dtpNotificationDate.Checked = True Or dtpNotificationDate.ShowCheckBox = False Then
                NotificationDue = "False"
                NotificationDueDate = dtpNotificationDate.Text
            Else
                NotificationDue = "True"
                NotificationDueDate = ""
            End If
            If dtpNotificationDate2.Checked = True Then
                NotificationSent = "False"
                NotificationSentDate = dtpNotificationDate2.Text
            Else
                NotificationSent = "True"
                NotificationSentDate = ""
            End If
            If txtNotificationTypeOther.Text <> "" Then
                NotificationTypeOther = Replace(txtNotificationTypeOther.Text, "'", "''")
            Else
                NotificationTypeOther = ""
            End If
            If txtNotificationComments.Text = "" Then
                NotificationComment = ""
            Else
                NotificationComment = Replace(txtNotificationComments.Text, "'", "''")
            End If
            If rdbNotificationFollowUpYes.Checked = True Then
                NotificationFollowUp = "True"
            Else
                NotificationFollowUp = "False"
            End If

            SQL = "Select strTrackingNumber " & _
            "from AIRBRANCH.SSCPNotifications " & _
            "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                SQL = "UPdate AIRBRANCH.SSCPNotifications set " & _
                "datNotificationDue = '" & NotificationDueDate & "', " & _
                "strNotificationDue = '" & NotificationDue & "', " & _
                "datNotificationSent = '" & NotificationSentDate & "', " & _
                "strNotificationSent = '" & NotificationSent & "', " & _
                "strNotificationType = '" & cboNotificationType.SelectedValue & "', " & _
                "strNotificationTypeOther = '" & Replace(NotificationTypeOther, "'", "''") & "', " & _
                "strNotificationComment = '" & Replace(NotificationComment, "'", "''") & "', " & _
                "strNotificationFollowUp = '" & NotificationFollowUp & "', " & _
                "strModifingPerson = '" & UserGCode & "', " & _
                "datModifingDate = '" & OracleDate & "' " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
            Else
                SQL = "Insert into AIRBRANCH.SSCPNotifications " & _
                "(strTrackingNumber, datNotificationDue, " & _
                "strNotificationDue, datNotificationSent, " & _
                "strNotificationSent, strNotificationType, " & _
                "strNotificationTypeOther, strNotificationComment, " & _
                "strNotificationFollowUp, strModifingPerson, " & _
                "datModifingDate) " & _
                "values " & _
                "('" & txtTrackingNumber.Text & "', '" & NotificationDueDate & "', " & _
                "'" & NotificationDue & "', '" & NotificationSentDate & "', " & _
                "'" & NotificationSent & "', '" & cboNotificationType.SelectedValue & "', " & _
                "'" & Replace(NotificationTypeOther, "'", "''") & "', '" & Replace(NotificationComment, "'", "''") & "', " & _
                "'" & NotificationFollowUp & "', '" & UserGCode & "', " & _
                "'" & OracleDate & "') "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If Me.chbNotificationReceivedByAPB.Checked = True Then
                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                "datReceivedDate = '" & DTPNotificationReceived.Text & "' " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveDate()
        Dim CompleteDate As String
        Dim Staff As String = ""
        Dim AcknoledgmentLetter As String
        Dim UpdateCode As String
        Dim ActionNumber As String = ""

        Try
            SQL = "Select strTrackingNumber " & _
            "from AIRBRANCH.SSCPItemMaster " & _
            "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If chbEventComplete.Checked = True Then
                    CompleteDate = DTPEventCompleteDate.Text
                Else
                    CompleteDate = ""
                End If

                If DTPAcknowledgmentLetterSent.Checked = True Then
                    AcknoledgmentLetter = DTPAcknowledgmentLetterSent.Text
                Else
                    AcknoledgmentLetter = ""
                End If

                Staff = Me.cboStaffResponsible.SelectedValue
                If Staff = "" Then
                    Staff = "0"
                End If

                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                "datCompleteDate = '" & CompleteDate & "', " & _
                "datAcknoledgmentLetterSent = '" & AcknoledgmentLetter & "', " & _
                "strResponsibleStaff = '" & Staff & "', " & _
                "strDelete = '' " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If TPTestReports.Focus = False Then
                    SQL = "Select strUpDateStatus " & _
                    "from AIRBRANCH.AFSSSCPRecords " & _
                    "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader

                    recExist = dr.Read
                    If recExist = True Then
                        UpdateCode = dr.Item("strUpDateStatus")
                        dr.Close()
                        Select Case UpdateCode
                            Case "A"
                                'Leave it alone
                            Case "C"
                                'Leave it alone
                            Case "N"
                                SQL = "Update AIRBRANCH.AFSSSCPRecords set " & _
                                "strUpDateStatus = 'C' " & _
                                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            Case Else
                                'Leave it alone 
                        End Select
                    Else
                        dr.Close()

                        If Me.TPACC.Focus = True Or Me.TPInspection.Focus = True Or _
                              (Me.TPNotifications.Focus = True And Me.rdbNotificationFollowUpYes.Checked = True) Then

                            SQL = "Select strAFSActionNumber " & _
                            "from AIRBRANCH.APBSupplamentalData " & _
                            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                ActionNumber = dr.Item("strAFSActionNumber")
                            End While
                            dr.Close()

                            SQL = "Insert into AIRBRANCH.AFSSSCPRecords " & _
                            "(strTrackingNumber, strAFSActionNumber, " & _
                            "strUpDateStatus, strModifingPerson, " & _
                            "datModifingdate) " & _
                            "values " & _
                            "('" & txtTrackingNumber.Text & "', '" & ActionNumber & "', " & _
                            "'A', '" & UserGCode & "', " & _
                            "'" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()

                            ActionNumber = CStr(CInt(ActionNumber) + 1)

                            SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                            "strAFSActionNUmber = '" & ActionNumber & "' " & _
                            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                    End If
                End If

                If TPACC.Focus = True Then
                    SQL = "Select strTrackingNumber " & _
                    "from AIRBRANCH.SSCPItemMaster " & _
                    "where strTrackingnumber = '" & CStr(CInt(txtTrackingNumber.Text + 1)) & "' " & _
                    "and streventType = '06' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()

                    If recExist = True Then
                        SQL = "Select strUpDateStatus " & _
                        "from AIRBRANCH.AFSSSCPRecords " & _
                        "where strTrackingNumber = '" & CStr(CInt(txtTrackingNumber.Text + 1)) & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader

                        recExist = dr.Read
                        If recExist = True Then
                            UpdateCode = dr.Item("strUpDateStatus")
                            dr.Close()
                            Select Case UpdateCode
                                Case "A"
                                    'Leave it alone
                                Case "C"
                                    'Leave it alone
                                Case "N"
                                    SQL = "Update AIRBRANCH.AFSSSCPRecords set " & _
                                    "strUpDateStatus = 'C' " & _
                                    "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                                    cmd = New OracleCommand(SQL, CurrentConnection)
                                    If CurrentConnection.State = ConnectionState.Closed Then
                                        CurrentConnection.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    dr.Close()
                                Case Else
                                    'Leave it alone 
                            End Select
                        Else
                            dr.Close()

                            SQL = "Select strAFSActionNumber " & _
                            "from AIRBRANCH.APBSupplamentalData " & _
                            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                ActionNumber = dr.Item("strAFSActionNumber")
                            End While
                            dr.Close()

                            SQL = "Insert into AIRBRANCH.AFSSSCPRecords " & _
                            "(strTrackingNumber, strAFSActionNumber, " & _
                            "strUpDateStatus, strModifingPerson, " & _
                            "datModifingdate) " & _
                            "values " & _
                            "('" & CStr(CInt(txtTrackingNumber.Text + 1)) & "', '" & ActionNumber & "', " & _
                            "'A', '" & UserGCode & "', " & _
                            "'" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()

                            ActionNumber = CStr(CInt(ActionNumber) + 1)

                            SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                            "strAFSActionNUmber = '" & ActionNumber & "' " & _
                            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                    End If
                End If

                If TPReport.Focus = True Then
                    SQL = "Select strUpDateStatus " & _
                    "from AIRBRANCH.AFSSSCPRecords " & _
                    "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader

                    recExist = dr.Read
                    If recExist = True Then
                        UpdateCode = dr.Item("strUpDateStatus")
                        dr.Close()
                        Select Case UpdateCode
                            Case "A"
                                'Leave it alone
                            Case "C"
                                'Leave it alone
                            Case "N"
                                SQL = "Update AIRBRANCH.AFSSSCPRecords set " & _
                                "strUpDateStatus = 'C' " & _
                                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            Case Else
                                'Leave it alone 
                        End Select
                    Else
                        dr.Close()

                        SQL = "Select strAFSActionNumber " & _
                        "from AIRBRANCH.APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            ActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        SQL = "Insert into AIRBRANCH.AFSSSCPRecords " & _
                        "(strTrackingNumber, strAFSActionNumber, " & _
                        "strUpDateStatus, strModifingPerson, " & _
                        "datModifingdate) " & _
                        "values " & _
                        "('" & txtTrackingNumber.Text & "', '" & ActionNumber & "', " & _
                        "'A', '" & UserGCode & "', " & _
                        "'" & OracleDate & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        ActionNumber = CStr(CInt(ActionNumber) + 1)

                        SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                        "strAFSActionNUmber = '" & ActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                End If

                LoadHeader()
            Else
                MsgBox("There is no record of this Tracking Number in the Database.", MsgBoxStyle.Information, "SSCP Events")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Loads"
    Sub LoadReport()
        Dim Completeness As String = ""
        Dim Enforcement As String = ""
        Dim Deviation As String = ""
        Dim temp As String = ""

        Try

            If txtTrackingNumber.Text <> "" Then
                temp = txtTrackingNumber.Text

                SQL = "Select " & _
                "strTrackingNumber, strReportPeriod, " & _
                "datReportingPeriodStart, datReportingPeriodEnd, " & _
                "strReportingPeriodComments, datReportDueDate, " & _
                "datSentByFacilityDate, strCompleteStatus, " & _
                "strEnforcementNeeded, strShowDeviation, " & _
                "strGeneralComments, strModifingPerson, " & _
                "datModifingDate, strSubmittalNumber " & _
                "from AIRBRANCH.SSCPREports " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "'"

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read

                If recExist = True Then
                    NUPReportSubmittal.Value = dr.Item("strSubmittalNumber")
                    cboReportSchedule.Text = dr.Item("strReportPeriod")
                    DTPReportPeriodStart.Text = dr.Item("DatReportingPeriodStart")
                    DTPReportPeriodEnd.Text = dr.Item("DatReportingPeriodEnd")
                    If dr.Item("strReportingPeriodComments") = "N/A" Then
                        txtReportPeriodComments.Text = "N/A"
                    Else
                        txtReportPeriodComments.Text = dr.Item("strReportingPeriodComments")
                    End If
                    dtpDueDate.Text = dr.Item("datreportduedate")
                    DTPSentDate.Text = dr.Item("datsentbyfacilitydate")
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
                    If dr.Item("strGeneralComments") = "N/A" Then
                        txtReportsGeneralComments.Text = "N/A"
                    Else
                        txtReportsGeneralComments.Text = dr.Item("strGeneralComments")
                    End If

                Else

                End If
            Else
                MsgBox("Can't Load")
            End If

        Catch ex As Exception
            ErrorReport(ex, temp, "SSCPEvents.LoadReport")
        Finally

        End Try


    End Sub
    Sub LoadReportFromSubmittal()
        Dim Completeness As String = ""
        Dim Enforcement As String = ""
        Dim Deviation As String = ""

        Try

            If txtTrackingNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select * from AIRBRANCH.SSCPREportsHistory " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
                "and strSubmittalNumber = '" & NUPReportSubmittal.Value & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read
                If recExist = True Then
                    NUPReportSubmittal.Value = dr.Item("strSubmittalNumber")
                    cboReportSchedule.Text = dr.Item("strReportPeriod")
                    DTPReportPeriodStart.Text = dr.Item("DatReportingPeriodStart")
                    DTPReportPeriodEnd.Text = dr.Item("DatReportingPeriodEnd")
                    If dr.Item("strReportingPeriodComments") = "N/A" Then
                        txtReportPeriodComments.Text = "N/A"
                    Else
                        txtReportPeriodComments.Text = dr.Item("strReportingPeriodComments")
                    End If
                    dtpDueDate.Text = dr.Item("datreportduedate")
                    DTPSentDate.Text = dr.Item("datsentbyfacilitydate")
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
                    If dr.Item("strGeneralComments") = "N/A" Then
                        txtReportsGeneralComments.Text = "N/A"
                    Else
                        txtReportsGeneralComments.Text = dr.Item("strGeneralComments")
                    End If
                Else

                End If

            Else
                MsgBox("Can't Load")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadReportSubmittalDGR()
        Dim dsReportsDGR As DataSet
        Dim daReportsDGR As OracleDataAdapter

        Try

            SQL = "Select strSubmittalNumber, datModifingDate, " & _
                         "(strLastName|| ', ' ||strFirstName) as UserName " & _
                         "from AIRBRANCH.SSCPREportsHistory, AIRBRANCH.EPDUserProfiles " & _
                         "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
                         "and AIRBRANCH.SSCPREportsHistory.strModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                         "order by strsubmittalnumber"

            dsReportsDGR = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)

            daReportsDGR = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daReportsDGR.Fill(dsReportsDGR, "Reports")
            dgrReportResubmittal.DataSource = dsReportsDGR
            dgrReportResubmittal.DataMember = "Reports"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatReportsDGR()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadInspection()
        Try

            If txtTrackingNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select " & _
                "strTrackingNumber, datInspectionDateStart, " & _
                "datInspectionDateEnd, strInspectionreason, " & _
                "strWeatherConditions, strInspectionGuide, " & _
                "strFacilityOperating, strInspectionComplianceStatus, " & _
                "strInspectionComments, " & _
                "strInspectionFollowUP, strModifingPerson, " & _
                "datModifingDate " & _
                "from AIRBRANCH.SSCPInspections " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist = True Then
                    cboInspectionReason.Text = dr.Item("strInspectionReason")
                    txtWeatherConditions.Text = dr.Item("strWeatherConditions")
                    If dr.Item("strInspectionGuide") = "N/A" Then
                        txtInspectionGuide.Text = "N/A"
                    Else
                        txtInspectionGuide.Text = dr.Item("strInspectionguide")
                    End If
                    If dr.Item("strFacilityOperating") = "True" Then
                        rdbInspectionFacilityOperatingYes.Checked = True
                        rdbInspectionFacilityOperatingNo.Checked = False
                    Else
                        rdbInspectionFacilityOperatingNo.Checked = True
                        rdbInspectionFacilityOperatingYes.Checked = False
                    End If
                    cboInspectionComplianceStatus.Text = dr.Item("strInspectioncompliancestatus")

                    If dr.Item("strInspectionComments") = "N/A" Then
                        txtInspectionConclusion.Text = "N/A"
                    Else
                        txtInspectionConclusion.Text = dr.Item("strInspectionComments")
                    End If

                    If IsDBNull(dr.Item("strInspectionFollowUp")) Then
                        rdbInspectionFollowUpNo.Checked = True
                    Else
                        If dr.Item("strInspectionFollowUp") = True Then
                            rdbInspectionFollowUpNo.Checked = False
                            rdbInspectionFollowUpYes.Checked = True
                        Else
                            rdbInspectionFollowUpNo.Checked = True
                            rdbInspectionFollowUpYes.Checked = False
                        End If

                    End If

                    DTPInspectionDateStart.Text = dr.Item("DatINspectionDateStart")
                    dtpInspectionTimeStart.Text = dr.Item("DatINspectionDateStart")
                    DTPInspectionDateEnd.Text = dr.Item("datinspectionDateEnd")
                    dtpInspectionTimeEnd.Text = dr.Item("datinspectionDateEnd")

                End If  'If recExist = True Then
            End If   'If txtTrackingNumber.Text <> "" Then

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadACC()
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

            If txtTrackingNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select " & _
                "strTrackingNumber, strSubmittalNumber, " & _
                "strPostmarkedOnTime, datPostmarkDate, " & _
                "strSignedByRO, strCorrectACCForms, " & _
                "strTitleVConditionsListed, strACCCorrectlyFilledOut, " & _
                "strReportedDeviations, strDeviationsUnReported, " & _
                "strComments, strEnforcementNeeded, " & _
                "strModifingPerson, datModifingDate, datAccReportingYear, " & _
                "STRKNOWNDEVIATIONSREPORTED, STRRESUBMITTALREQUIRED " & _
                "from AIRBRANCH.SSCPACCS " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read
                If recExist = True Then
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
                        dtpAccReportingYear.Value = DateTime.Today.AddYears(-1)
                        dtpAccReportingYear.Checked = False
                    Else
                        dtpAccReportingYear.Value = dr.Item("datAccReportingYear")
                        dtpAccReportingYear.Checked = True
                    End If
                    If dr.Item("DATPostmarkDate") = "04-Jul-1776" Then
                        DTPACCPostmarked.Text = OracleDate
                    Else
                        DTPACCPostmarked.Text = dr.Item("datPostmarkDate")
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
                        txtACCComments.Text = "N/A"
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
                    AllDeviationsReported = DB.GetNullable(Of String)(dr.Item("STRKNOWNDEVIATIONSREPORTED"))
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
                    ResubmittalRequested = DB.GetNullable(Of String)(dr.Item("STRRESUBMITTALREQUIRED"))
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
                    dtpAccReportingYear.Enabled = False
                    rdbACCAllDeviationsReportedYes.Enabled = False
                    rdbACCAllDeviationsReportedNo.Enabled = False
                    rdbACCAllDeviationsReportedUnknown.Enabled = False
                    rdbACCResubmittalRequestedYes.Enabled = False
                    rdbACCResubmittalRequestedNo.Enabled = False
                    rdbACCResubmittalRequestedUnknown.Enabled = False

                    'chbACCReceivedByAPB.Enabled = False
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
            Else
                MsgBox("Can't Load")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadACCFromSubmittal()
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

            If txtTrackingNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select * from AIRBRANCH.SSCPACCSHistory " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
                "and strSubmittalNumber = '" & NUPACCSubmittal.Value & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read
                If recExist = True Then
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
                        dtpAccReportingYear.Value = DateTime.Today.AddYears(-1)
                        dtpAccReportingYear.Checked = False
                    Else
                        dtpAccReportingYear.Value = dr.Item("datAccReportingYear")
                        dtpAccReportingYear.Checked = True
                    End If
                    If dr.Item("DATPostmarkDate") = "04-Jul-1776" Then
                        DTPACCPostmarked.Text = OracleDate
                    Else
                        DTPACCPostmarked.Text = dr.Item("datPostmarkDate")
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
                    AllDeviationsReported = DB.GetNullable(Of String)(dr.Item("STRKNOWNDEVIATIONSREPORTED"))
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
                    ResubmittalRequested = DB.GetNullable(Of String)(dr.Item("STRRESUBMITTALREQUIRED"))
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
                    'DTPACCPostmarked.Enabled = False
                    'chbACCReceivedByAPB.Enabled = False
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
            Else
                MsgBox("Can't Load")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadACCSubmittalDGR()
        Dim dsACCsDGR As DataSet
        Dim daACCsDGR As OracleDataAdapter

        Try

            SQL = "Select strSubmittalNumber, datModifingDate, " & _
            "(strLastName|| ', ' ||strFirstName) as UserName " & _
            "from AIRBRANCH.SSCPACCSHistory, AIRBRANCH.EPDUserProfiles " & _
            "where strTrackingNumber = '" & txtTrackingNumber.Text & "' " & _
            "and AIRBRANCH.SSCPACCSHistory.strModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
            "order by strsubmittalnumber"

            dsACCsDGR = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)

            daACCsDGR = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daACCsDGR.Fill(dsACCsDGR, "ACCs")
            DGRACCResubmittal.DataSource = dsACCsDGR
            DGRACCResubmittal.DataMember = "ACCs"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatACCDGR()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadTestReport()
        Try

            If txtTrackingNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select " & _
                "strTrackingNUmber, strReferenceNumber, " & _
                "datTestReportDue, " & _
                "strTestReportComments, strTestReportFOllowUP, " & _
                "strModifingPerson, datModifingDate " & _
                "from AIRBRANCH.SSCPTestReports " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                recExist = dr.Read
                If recExist = True Then
                    txtISMPReferenceNumber.Text = dr.Item("StrReferenceNumber")
                    If dr.Item("DatTestReportDue") = "04-Jul-1776" Then
                        txtTestReportDueDate.Text = "Unknown"
                    Else
                        txtTestReportDueDate.Text = Format(dr.Item("datTestReportdue"), "dd-MMM-yyyy")
                    End If
                    If dr.Item("strTestREportComments") <> "N/A" Then
                        txtTestReportComments.Text = dr.Item("strTestREportComments")
                    Else
                        txtTestReportComments.Text = "N/A"
                    End If
                    If dr.Item("strTestReportFollowUp") = "True" Then
                        rdbTestReportFollowUpYes.Checked = True
                    Else
                        rdbTestReportFollowUpNo.Checked = True
                    End If
                Else
                    txtISMPReferenceNumber.Text = "N/A"
                    txtTestReportDueDate.Text = "Unknown"
                    txtTestReportComments.Text = "N/A"
                End If

                If txtISMPReferenceNumber.Text = "N/A" Or txtISMPReferenceNumber.Text = "" Then
                    DTPTestReportReceivedDate.Text = OracleDate
                    txtTestReportISMPCompleteDate.Text = "N/A"
                Else
                    SQL = "Select datReceivedDate, datCompleteDate " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where strReferenceNumber = '" & txtISMPReferenceNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        DTPTestReportReceivedDate.Text = Format(dr.Item("datReceivedDate"), "dd-MMM-yyyy")
                        If dr.Item("datCompleteDate") = "07-Jul-1776" Then
                            txtTestReportISMPCompleteDate.Text = "Open"
                        Else
                            txtTestReportISMPCompleteDate.Text = Format(dr.Item("datReceivedDate"), "dd-MMM-yyyy")
                        End If
                    Else
                        DTPTestReportReceivedDate.Text = OracleDate
                        txtTestReportISMPCompleteDate.Text = OracleDate
                    End If
                    dr.Close()
                End If

                SQL = "Select datSSCPTestReportDue " & _
                "from AIRBRANCH.APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("datSSCPTestReportDue")) Then
                        DTPTestReportNewDueDate.Text = Date.Today
                    Else
                        DTPTestReportNewDueDate.Text = Format(dr.Item("datSSCPTestReportDue"), "dd-MMM-yyyy")
                    End If
                Else
                    DTPTestReportNewDueDate.Text = Date.Today
                End If
                dr.Close()

                If txtISMPReferenceNumber.Text <> "N/A" Then
                    SQL = "Select " & _
                    "strEmissionSource, strPollutantDescription " & _
                    "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUPPollutants " & _
                    "where strReferenceNumber = '" & txtISMPReferenceNumber.Text & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUPPollutants.strPollutantCode "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        txtPollutantTested.Text = dr.Item("strPollutantDescription")
                        txtUnitTested.Text = dr.Item("strEmissionSource")
                    Else
                        txtPollutantTested.Text = "N/A"
                        txtUnitTested.Text = "N/A"
                    End If
                    dr.Close()
                End If
            Else
                MsgBox("Can't Load")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadNotification()
        Dim temp As String = ""
        Try

            If txtTrackingNumber.Text <> "" Then
                temp = txtTrackingNumber.Text
                SQL = "Select " & _
                "strTrackingNumber, datNotificationDue, " & _
                "strNotificationDue, datNotificationSent, " & _
                "strNotificationSent, strNotificationType, " & _
                "strNotificationTypeOther, strNotificationComment, " & _
                "strNotificationFollowUp, strModifingPerson, " & _
                "datModifingDate " & _
                "From AIRBRANCH.SSCPNotifications " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strNotificationType")) Then
                        cboNotificationType.SelectedValue = "01"
                    Else
                        cboNotificationType.SelectedValue = dr.Item("strNotificationType")
                    End If

                    If IsDBNull(dr.Item("datNotificationDue")) Then
                        dtpNotificationDate.Text = Date.Today
                    Else
                        If dr.Item("datNotificationDue") <> "04-Jul-1776" Then
                            dtpNotificationDate.Text = dr.Item("datNotificationDue")
                        Else
                            dtpNotificationDate.Text = Date.Today
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
                        dtpNotificationDate2.Text = Date.Today
                    Else
                        If dr.Item("datNotificationSent") <> "04-Jul-1776" Then
                            dtpNotificationDate2.Text = dr.Item("datNotificationSent")
                        Else
                            dtpNotificationDate2.Text = Date.Today
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
                        If dr.Item("strNotificationComment") <> "N/A" Then
                            txtNotificationComments.Text = dr.Item("strNotificationComment")
                        Else
                            txtNotificationComments.Text = "N/A"
                        End If
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
                    temp = "No Tracking Number"
                    dtpNotificationDate.Checked = False
                    dtpNotificationDate.Text = Date.Today
                    If dtpNotificationDate.ShowCheckBox = True Then
                        dtpNotificationDate.Checked = False
                    End If
                    dtpNotificationDate2.Text = Date.Today
                    cboNotificationType.SelectedValue = "01"
                    txtNotificationTypeOther.Text = "N/A"
                    txtNotificationComments.Text = "N/A"
                End If
            Else
                MsgBox("Unable to load data.", MsgBoxStyle.Exclamation, "SSCP Events")
            End If
        Catch ex As Exception
            ErrorReport(ex, temp, "SSCPEvents.LoadNotification")
        Finally

        End Try

    End Sub
#End Region

    Sub DeleteSSCPData()
        Try

            If TPTestReports.Focus = True Then
                MessageBox.Show("Performance tests must be deleted by ISMP.", "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                MsgBox("This Compliance Action is currently linked to an Enforcement Action." & vbCrLf & _
                      "Disassociate this action from any enforcement before deleting.", MsgBoxStyle.Exclamation, "SSCP Events")
                Exit Sub
            End If

            If MessageBox.Show("Should this work item be deleted?", _
                               "Confirm Deletion", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Warning, _
                               MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            ' Determine if action has been submitted to EPA
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.AFSSSCPRECORDS " & _
                " WHERE RowNum = 1 AND STRUPDATESTATUS  <> 'A' " & _
                " AND STRTRACKINGNUMBER = :pId "
            Dim parameter As OracleParameter = New OracleParameter("pId", txtTrackingNumber.Text)
            recExist = Convert.ToBoolean(DB.GetSingleValue(Of String)(query, parameter))

            If recExist = True Then
                MsgBox("This Compliance Action has already been submitted to EPA and must be manually removed." & vbCrLf & _
                       "Please contact the Data Management Unit (Michael Floyd) with the tracking number to delete this action.", _
                       MsgBoxStyle.Exclamation, "SSCP Events")
                Exit Sub
            End If

            ' Delete record from AFSSSCPRECORDS and mark as deleted in SSCP item master
            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim parameters As OracleParameter()

            query = " DELETE FROM AIRBRANCH.AFSSSCPRECORDS WHERE STRTRACKINGNUMBER = :pId "
            queryList.Add(query)
            parameters = New OracleParameter() {New OracleParameter("pId", txtTrackingNumber.Text)}
            parametersList.Add(parameters)

            query = " UPDATE AIRBRANCH.SSCPITEMMASTER SET STRDELETE = '" & Boolean.TrueString & "' " & _
                " WHERE STRTRACKINGNUMBER = :pId "
            queryList.Add(query)
            parametersList.Add(parameters) ' parameters are same for both queries

            Dim result As Boolean = DB.RunCommand(queryList, parametersList)

            If result Then
                MsgBox("Compliance Event Deleted.", MsgBoxStyle.Information, "SSCP Events")
                Me.Close()
            Else
                MsgBox("There was an error deleting the event.", MsgBoxStyle.Exclamation, "SSCP Events")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Validate"
#Region "Reports"
    Private Sub cboReportSchedule_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboReportSchedule.Validating
        Try

            ValidatecboReportSchedule()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbReportCompleteYes_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbReportCompleteYes.Validating
        Try

            ValidateReportComplete()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbReportCompleteNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbReportCompleteNo.Validating
        Try

            ValidateReportComplete()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbReportDeviationYes_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbReportDeviationYes.Validating
        Try

            ValidateShowDeviation()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbReportDeviationNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbReportDeviationNo.Validating
        Try

            ValidateShowDeviation()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbReportEnforcementYes_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbReportEnforcementYes.Validating
        Try

            ValidateEnforcementNeeded()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbReportEnforcementNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbReportEnforcementNo.Validating
        Try

            ValidateEnforcementNeeded()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub NUPSubmittal_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NUPReportSubmittal.Validating
        Try

            ValidateSubmittalNumber()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Inspections"
    Private Sub cboInspectionComplianceStatus_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboInspectionComplianceStatus.Validating
        Try

            ValidatecboInspectionComplianceStatus()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbInspectionFacilityOperatingYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbInspectionFacilityOperatingYes.Validating
        Try

            ValidateFacilityOperating()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbInspectionFacilityOperatingNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbInspectionFacilityOperatingNo.Validating
        Try

            ValidateFacilityOperating()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub DTPInspectionDateStart_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTPInspectionDateStart.Validating
        Try

            ValidateDTPInspectionDates()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub DTPInspectionDateEnd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTPInspectionDateEnd.Validating
        Try

            ValidateDTPInspectionDates()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "ACC"
    Private Sub rdbACCConditionsYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCConditionsYes.Validating
        Try

            ValidateTitleVConditions()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCConditionsNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCConditionsNo.Validating
        Try

            ValidateTitleVConditions()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCCorrectACCNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectACCNo.Validating
        Try

            ValidateCorrectACCForms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCCorrectACCYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectACCYes.Validating
        Try

            ValidateCorrectACCForms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCCorrectYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectYes.Validating
        Try

            ValidateCorrectlyFilledOut()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCCorrectNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCCorrectNo.Validating, rdbACCAllDeviationsReportedUnknown.Validating
        Try

            ValidateCorrectlyFilledOut()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCDeviationsReportedYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCDeviationsReportedYes.Validating
        Try

            ValidateReportedDeviations()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCDeviationsReportedNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCDeviationsReportedNo.Validating
        Try

            ValidateReportedDeviations()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCEnforcementNeededYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCEnforcementNeededYes.Validating
        Try

            ValidateACCEnforcementNeeded()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCAllDeviationsReported_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
    Handles rdbACCAllDeviationsReportedYes.Validating, rdbACCAllDeviationsReportedNo.Validating, rdbACCAllDeviationsReportedUnknown.Validating
        Try
            ValidateACCAllDeviationsReported()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbACCResubmittalRequested_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
    Handles rdbACCResubmittalRequestedYes.Validating, rdbACCResubmittalRequestedNo.Validating, rdbACCResubmittalRequestedUnknown.Validating
        Try
            ValidateACCResubmittalRequested()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbACCEnforcementNeededNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCEnforcementNeededNo.Validating
        Try

            ValidateACCEnforcementNeeded()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCPostmarkYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCPostmarkYes.Validating
        Try

            ValidatePostmarkDate()
            'ValidateDatePostmarked()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCPostmarkNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCPostmarkNo.Validating
        Try

            ValidatePostmarkDate()
            ValidateDatePostmarked()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCPreviousDeviationsYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCPreviouslyUnreportedDeviationsYes.Validating
        Try

            ValidatePreviouslyReportedDeviations()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCPreviousDeviationsNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCPreviouslyUnreportedDeviationsNo.Validating
        Try

            ValidatePreviouslyReportedDeviations()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCROYes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCROYes.Validating
        Try

            ValidateROSigned()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub rdbACCRONo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles rdbACCRONo.Validating
        Try

            ValidateROSigned()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub NUPACCSubmittal_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NUPACCSubmittal.Validating
        Try

            ValidateNUPACCSubmittal()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Notifications"

#End Region
#End Region

#Region "Validate Items"
#Region "Validate Report"
    Sub ValidateALLReport()
        Try

            ValidatecboReportSchedule()
            ValidateReportComplete()
            ValidateShowDeviation()
            ValidateEnforcementNeeded()
            ValidateSubmittalNumber()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidatecboReportSchedule()
        Try

            If cboReportSchedule.Text = "" Then
                wrnReportPeriod.Visible = True
            Else
                wrnReportPeriod.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateReportComplete()
        Try

            If rdbReportCompleteYes.Checked = False And rdbReportCompleteNo.Checked = False Then
                wrnCompleteReport.Visible = True
            Else
                wrnCompleteReport.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateShowDeviation()
        Try

            If rdbReportDeviationYes.Checked = False And rdbReportDeviationNo.Checked = False Then
                wrnShowDeviation.Visible = True
            Else
                wrnShowDeviation.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateEnforcementNeeded()
        Try

            If rdbReportEnforcementYes.Checked = False And rdbReportEnforcementNo.Checked = False Then
                wrnEnforcementNeeded.Visible = True
            Else
                wrnEnforcementNeeded.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateSubmittalNumber()
        Try

            If NUPReportSubmittal.Text = "" Then
                wrnReportSubmittal.Visible = True
            Else
                wrnReportSubmittal.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Validate Inspection"
    Sub ValidateAllInspection()
        Try

            ValidatecboInspectionComplianceStatus()
            ValidateFacilityOperating()
            ValidateDTPInspectionDates()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ValidatecboInspectionComplianceStatus()
        Try

            If cboInspectionComplianceStatus.Text = "" Then
                wrnInspectionComplianceStatus.Visible = True
            Else
                wrnInspectionComplianceStatus.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateFacilityOperating()
        Try

            If rdbInspectionFacilityOperatingYes.Checked = False And rdbInspectionFacilityOperatingNo.Checked = False Then
                wrnInspectionOperating.Visible = True
            Else
                wrnInspectionOperating.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateDTPInspectionDates()
        Try

            If DTPInspectionDateEnd.Value.Date < DTPInspectionDateStart.Value.Date Then
                wrnInspectionDates.Visible = True
            Else
                wrnInspectionDates.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Validate Notification"


#End Region
#Region "Validate ACC"
    Sub ValidateAllACC()
        Try

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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateNUPACCSubmittal()
        Try

            If NUPACCSubmittal.Text = "" Then
                wrnACCSubmittal.Visible = True
            Else
                wrnACCSubmittal.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidatePostmarkDate()
        Try

            If rdbACCPostmarkYes.Checked = False And rdbACCPostmarkNo.Checked = False Then
                wrnACCPostmark.Visible = True
            Else
                wrnACCPostmark.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateROSigned()
        Try

            If rdbACCROYes.Checked = False And rdbACCRONo.Checked = False Then
                wrnACCRO.Visible = True
            Else
                wrnACCRO.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateCorrectACCForms()
        Try

            If rdbACCCorrectACCYes.Checked = False And rdbACCCorrectACCNo.Checked = False Then
                wrnACCCorrectACC.Visible = True
            Else
                wrnACCCorrectACC.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateTitleVConditions()
        Try

            If rdbACCConditionsYes.Checked = False And rdbACCConditionsNo.Checked = False Then
                wrnACCConditions.Visible = True
            Else
                wrnACCConditions.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateCorrectlyFilledOut()
        Try

            If rdbACCCorrectYes.Checked = False And rdbACCCorrectNo.Checked = False Then
                wrnACCCorrect.Visible = True
            Else
                wrnACCCorrect.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateReportedDeviations()
        Try

            If rdbACCDeviationsReportedYes.Checked = False And rdbACCDeviationsReportedNo.Checked = False Then
                wrnACCDeviationsReported.Visible = True
            Else
                wrnACCDeviationsReported.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidatePreviouslyReportedDeviations()
        Try

            If rdbACCPreviouslyUnreportedDeviationsYes.Checked = False And rdbACCPreviouslyUnreportedDeviationsNo.Checked = False Then
                wrnACCPreviousDeviations.Visible = True
            Else
                wrnACCPreviousDeviations.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateACCEnforcementNeeded()
        Try

            If rdbACCEnforcementNeededYes.Checked = False And rdbACCEnforcementNeededNo.Checked = False Then
                wrnACCEnforcementNeeded.Visible = True
            Else
                wrnACCEnforcementNeeded.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ValidateACCAllDeviationsReported()
        Try
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ValidateACCResubmittalRequested()
        Try
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ValidateDatePostmarked()
        Try
            Dim StartDate As Date = CDate("01-Feb-" & (Date.Today.Year - 1))
            Dim Enddate As Date = CDate("30-Jan-" & Date.Today.Year)


            If StartDate <= CDate(DTPACCPostmarked.Value) And CDate(DTPACCPostmarked.Value) <= Enddate Then
                wrnACCDatePostmarked.Visible = False
                'If rdbACCPostmarkYes.Checked = True Then
                '    wrnACCDatePostmarked.Visible = False
                'Else
                '    wrnACCDatePostmarked.Visible = False
                '    'wrnACCDatePostmarked.Visible = True
                'End If
            Else
                If DTPACCPostmarked.Value > Enddate Then
                    wrnACCDatePostmarked.Visible = False
                    'If rdbACCPostmarkYes.Checked = True Then
                    '    wrnACCDatePostmarked.Visible = False
                    'Else
                    '    wrnACCDatePostmarked.Visible = False
                    '    'wrnACCDatePostmarked.Visible = True
                    'End If
                Else
                    If DTPACCPostmarked.Value <= StartDate Then
                        wrnACCDatePostmarked.Visible = False
                        'If rdbACCPostmarkYes.Checked = True Then
                        '    wrnACCDatePostmarked.Visible = False
                        'Else
                        '    wrnACCDatePostmarked.Visible = False
                        '    'wrnACCDatePostmarked.Visible = True
                        'End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#End Region

    Private Sub dgrReportResubmittal_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrReportResubmittal.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub DGRACCResubmittal_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DGRACCResubmittal.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbTestReportChangeDueDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbTestReportChangeDueDate.CheckedChanged
        Try

            If chbTestReportChangeDueDate.Checked = False Then
                DTPTestReportDueDate.Visible = False
            Else
                DTPTestReportDueDate.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub DTPTestReportDueDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPTestReportDueDate.TextChanged
        Try

            If chbTestReportChangeDueDate.Checked = True Then
                txtTestReportDueDate.Text = DTPTestReportDueDate.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub cboNotificationType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNotificationType.SelectedIndexChanged
        Try

            Select Case cboNotificationType.Text
                Case "Other"
                    txtNotificationTypeOther.Visible = True
                    lblNotificationDate.Text = "Notification Due Date:"
                    lblNotificationDate2.Text = "Date Sent by Facility:"
                    lblNotificationOther.Text = ""
                    lblNotificationOther.Visible = False
                    chbEventComplete.Text = "Complete"
                    dtpNotificationDate.ShowCheckBox = True
                    lblNotificationDue.Text = "(Do not check if no due date)"
                    lblDateSent.Text = "(Do not check if date is unknown)"
                    'Case "Shutdown"
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


    Private Sub chbEventComplete_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbEventComplete.CheckedChanged
        Try

            If AccountFormAccess(49, 1) = "1" Or AccountFormAccess(49, 2) = "1" Or AccountFormAccess(49, 3) = "1" Or AccountFormAccess(49, 4) = "1" Then
                chbEventComplete.Enabled = True
                CompleteReport()
            Else
                chbEventComplete.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEnforcementProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnforcementProcess.Click
        OpenEnforcement()
    End Sub
    Private Sub btnReportMoreOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportMoreOptions.Click
        Dim tempWidth As String
        Dim tempHeight As String

        Try

            tempWidth = dgrReportResubmittal.Size.Width
            tempHeight = dgrReportResubmittal.Size.Height

            If tempWidth <= 11 Then
                dgrReportResubmittal.Size = New System.Drawing.Size(200, tempHeight)
            Else
                dgrReportResubmittal.Size = New System.Drawing.Size(10, tempHeight)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnViewTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTestReport.Click
        Try

            If txtISMPReferenceNumber.Text <> "N/A" Then
                PrintOut = Nothing
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                PrintOut.txtReferenceNumber.Text = Me.txtISMPReferenceNumber.Text
                PrintOut.txtPrintType.Text = "SSCP"
                PrintOut.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnACCSubmittals_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnACCSubmittals.Click
        Dim tempWidth As String
        Dim tempHeight As String

        Try

            tempWidth = DGRACCResubmittal.Size.Width
            tempHeight = DGRACCResubmittal.Size.Height

            If tempWidth <= 11 Then
                DGRACCResubmittal.Size = New System.Drawing.Size(200, tempHeight)
            Else
                DGRACCResubmittal.Size = New System.Drawing.Size(10, tempHeight)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbReportReceivedByAPB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbReportReceivedByAPB.CheckedChanged
        Try
            If chbReportReceivedByAPB.Checked = True Then
                DTPReportReceivedDate.Enabled = True
            Else
                DTPReportReceivedDate.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub chbISMPTestReportReceivedByAPB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbISMPTestReportReceivedByAPB.CheckedChanged
        Try
            If chbISMPTestReportReceivedByAPB.Checked = True Then
                DTPTestReportReceivedDate.Enabled = True
            Else
                DTPTestReportReceivedDate.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub chbNotificationReceivedByAPB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbNotificationReceivedByAPB.CheckedChanged
        Try
            If chbNotificationReceivedByAPB.Checked = True Then
                DTPNotificationReceived.Enabled = True
            Else
                DTPNotificationReceived.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub chbACCReceivedByAPB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbACCReceivedByAPB.CheckedChanged
        Try
            If chbACCReceivedByAPB.Checked = True Then
                DTPACCReceivedDate.Enabled = True
            Else
                DTPACCReceivedDate.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub PrintACC()
        LoadACC()
        If Not dtpAccReportingYear.Checked Then
            MsgBox("Please save a reporting year before printing this ACC.", MsgBoxStyle.Critical, "Print Error")
            Exit Sub
        End If
        Try
            Dim acc As Apb.Sscp.Acc = LoadAccFromForm()
            Dim accList As New List(Of Apb.Sscp.Acc) From {acc}

            Dim dataTable As DataTable = CollectionHelper.ConvertToDataTable(Of Apb.Sscp.Acc)(accList)
            Dim title As String = acc.AccReportingYear & " ACC for " & acc.Facility.AirsNumber.FormattedString
            Dim crv As New CRViewerForm(New CR.Reports.AccMemo, dataTable, title:=title)
            crv.Show()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function LoadAccFromForm() As Apb.Sscp.Acc
        Dim thisAcc As New Apb.Sscp.Acc

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
            .Facility = DAL.GetFacility(AIRSNumber)
            .PostmarkedByDeadline = rdbACCPostmarkYes.Checked
            .ResubmittalRequested = rdbACCResubmittalRequestedYes.Checked
            .SignedByResponsibleOfficial = rdbACCROYes.Checked
            .StaffResponsible = DAL.GetStaff(cboStaffResponsible.SelectedValue)
            .UnreportedDeviationsReported = rdbACCPreviouslyUnreportedDeviationsYes.Checked
        End With

        Return thisAcc
    End Function

#Region "Main menu/toolbar"

    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub mmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiDelete.Click
        DeleteSSCPData()
    End Sub

    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        SaveMaster()
    End Sub

    Private Sub mmiPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPrint.Click
        PrintACC()
    End Sub

    Private Sub tbToolbar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbToolbar.ButtonClick
        Select Case tbToolbar.Buttons.IndexOf(e.Button)
            Case 0
                SaveMaster()
            Case 1
                PrintACC()
        End Select
    End Sub

#End Region

End Class
