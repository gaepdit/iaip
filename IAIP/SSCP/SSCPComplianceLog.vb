Imports System.Data.SqlClient
Imports Iaip.SharedData

Public Class SSCPComplianceLog
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As SqlCommand
    Dim dr, dr3 As SqlDataReader
    Dim recExist As Boolean
    Dim dsCompliance As DataSet
    Dim daCompliance As SqlDataAdapter
    Dim dsWorkEntry As DataSet
    Dim daWorkEntry As SqlDataAdapter
    Dim dsNotifications As DataSet
    Dim daNotifications As SqlDataAdapter
    Dim dsComplianceUnit As DataSet
    Dim daComplianceUnit As SqlDataAdapter
    Dim dsDistrictUnit As DataSet
    Dim daDistrictUnit As SqlDataAdapter
    Dim dtStaff As DataTable

    Private Sub DevWorkEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Panel1.Text = "Select a Function..."
            Panel2.Text = CurrentUser.AlphaName
            Panel3.Text = OracleDate
            DTPDateReceived.Text = OracleDate

            chbEngineer.Text = CurrentUser.AlphaName

            DTPFilterStart.Enabled = False
            DTPFilterEnd.Enabled = False
            DTPFilterStart.Text = Format(CDate(OracleDate).AddMonths(-1), "dd-MMM-yyyy")
            DTPFilterEnd.Text = OracleDate

            LoadDefaultSettings()
            LoadDataSets()
            LoadComboBoxes()

            LoaddgvWork()
            clbEngineer.SelectedItems.Clear()
            clbAirBranchUnits.SelectedItems.Clear()
            clbDistrictOffices.SelectedItems.Clear()

            'If AccountArray(4, 1) = "1" Then 'District Only 
            If AccountFormAccess(4, 1) = "1" _
                AndAlso Not CurrentUser.HasRole({19, 113, 114, 141, 118}) Then

                btnAddNewEntry.Visible = False
                btnDeleteWork.Visible = False
                btnUndeleteWork.Visible = False
                rdbEnforcementAction.Enabled = False
                rdbFCE.Enabled = False
                TCComplianceLog.TabPages.Remove(TPStartNewWork)
            Else

            End If
            If AccountFormAccess(4, 2) = "1" Then
                rdbEnforcementAction.Enabled = True
                rdbFCE.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Page Load"
    Private Sub LoadDataSets()
        Try

            dsCompliance = New DataSet
            dsNotifications = New DataSet
            dsComplianceUnit = New DataSet
            dsDistrictUnit = New DataSet


            SQL = "select strActivityType, strActivityName, strActivityDescription " &
            "from LookUPComplianceActivities " &
            "where strActivityName <> 'Performance Tests' " &
            "order by strActivityName"

            daCompliance = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "Select " &
            "strNotificationDesc, strNotificationKey " &
            "from LookUpSSCPNotifications " &
            "order by strNotificationDesc "

            daNotifications = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "select " &
            "strUnitDesc, numUnitCode " &
            "from LookUPEPDUnits " &
            "where numProgramCode = '4'" &
            "order by strUnitDesc "

            daComplianceUnit = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "select " &
            "strProgramDesc, numProgramCode  " &
            "from lookupepdprograms " &
            "where numbranchcode = '5' " &
            "and strProgramdesc <> 'Vacant' " &
            "and strProgramDesc <> 'Small Business Assistance Program' " &
            "order by strprogramDesc "

            daDistrictUnit = New SqlDataAdapter(SQL, CurrentConnection)

            daCompliance.Fill(dsCompliance, "ComplianceActivity")

            daNotifications.Fill(dsNotifications, "Notifications")
            daComplianceUnit.Fill(dsComplianceUnit, "ComplianceUnit")
            daDistrictUnit.Fill(dsDistrictUnit, "DistrictUnit")

            dtStaff = GetSharedData(SharedTable.AllComplianceStaff)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComboBoxes()
        Dim dtActivity As New DataTable
        Dim dtNotifications As New DataTable
        Dim dtComplianceUnit As New DataTable
        Dim dtDistrictUnit As New DataTable

        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            dtActivity.Columns.Add("strActivityName", GetType(System.String))
            dtActivity.Columns.Add("strActivityType", GetType(System.String))

            drNewRow = dtActivity.NewRow()
            drNewRow("strActivityName") = " "
            drNewRow("strActivityType") = " "
            dtActivity.Rows.Add(drNewRow)

            For Each drDSRow In dsCompliance.Tables("ComplianceActivity").Rows()
                drNewRow = dtActivity.NewRow()
                drNewRow("strActivityName") = drDSRow("strActivityName")
                drNewRow("strActivityType") = drDSRow("strActivityType")
                dtActivity.Rows.Add(drNewRow)
            Next

            With cboEvent
                .DataSource = dtActivity
                .DisplayMember = "strActivityName"
                .ValueMember = "strActivityType"
                If cboEvent.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With

            With clbEngineer
                .DataSource = dtStaff
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
            End With

            dtNotifications.Columns.Add("strNotificationDesc", GetType(System.String))
            dtNotifications.Columns.Add("strNotificationKey", GetType(System.String))

            For Each drDSRow In dsNotifications.Tables("Notifications").Rows()
                drNewRow = dtNotifications.NewRow
                drNewRow("strNotificationDesc") = drDSRow("strNotificationDesc")
                drNewRow("strNotificationKey") = drDSRow("strNotificationKey")
                dtNotifications.Rows.Add(drNewRow)
            Next

            With clbNotifications
                .DataSource = dtNotifications
                .DisplayMember = "strNotificationDesc"
                .ValueMember = "strNotificationKey"
            End With

            dtComplianceUnit.Columns.Add("strUnitDesc", GetType(System.String))
            dtComplianceUnit.Columns.Add("numUnitCode", GetType(System.String))

            For Each drDSRow In dsComplianceUnit.Tables("ComplianceUnit").Rows()
                drNewRow = dtComplianceUnit.NewRow
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                dtComplianceUnit.Rows.Add(drNewRow)
            Next

            With clbAirBranchUnits
                .DataSource = dtComplianceUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
            End With

            dtDistrictUnit.Columns.Add("strProgramDesc", GetType(System.String))
            dtDistrictUnit.Columns.Add("numProgramCode", GetType(System.String))

            For Each drDSRow In dsDistrictUnit.Tables("DistrictUnit").Rows()
                drNewRow = dtDistrictUnit.NewRow
                drNewRow("strProgramDesc") = drDSRow("strProgramDesc")
                drNewRow("numProgramCode") = drDSRow("numProgramCode")
                dtDistrictUnit.Rows.Add(drNewRow)
            Next

            With clbDistrictOffices
                .DataSource = dtDistrictUnit
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadDefaultSettings()
        Try
            If AccountFormAccess(4, 3) = "1" Or AccountFormAccess(4, 4) = "1" Then 'Full Access in unit or District Liaison
                chbEngineer.Checked = False
            Else
                chbEngineer.Checked = True
            End If

            chbAllWork.Checked = True
            chbACCs.Checked = False
            chbEnforcement.Checked = False
            chbFCE.Checked = False
            chbInspections.Checked = False
            chbNotifications.Checked = False
            chbPerformanceTests.Checked = False
            chbReports.Checked = False

            For x As Integer = 0 To clbEngineer.Items.Count - 1
                clbEngineer.SetItemCheckState(x, CheckState.Unchecked)
            Next

            txtAIRSNumberFilter.Clear()
            txtTrackingNumberFilter.Clear()
            txtEnforcementNumberFilter.Clear()
            txtFCENumberFilter.Clear()
            txtFacilityNameFilter.Clear()

            chbOpenWork.Checked = True
            chbCompletedWork.Checked = False
            chbDeletedWork.Checked = False

            txtWorkNumber.Clear()
            DTPFilterStart.Text = OracleDate
            DTPFilterEnd.Text = OracleDate
            chbFilterDates.Checked = False

            'lblWorkType.Text = ""
            txtAIRSNumber.Clear()
            txtFacilityName.Clear()
            txtTestType.Clear()
            txtFacilityCity.Clear()
            txtFacilityCounty.Clear()

            txtNewAIRSNumber.Clear()
            rdbEnforcementAction.Checked = False
            rdbFCE.Checked = False
            rdbOther.Checked = False
            txtFacilityInformation.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
#Region "Subs and Functions"
    Sub LoaddgvWork()
        Dim SQLLine As String = ""
        Dim SubSQL As String = ""
        'Dim EnforceDate As String = ""
        'Dim InspectDate As String = ""
        'Dim OtherDate As String = ""
        'Dim FCEDate As String = ""
        Dim NotificationData As String = ""

        Try

            SQL = "select " &
            "ALLDATA.AIRSNUMBER, STRFACILITYNAME, " &
            "STRCLASS, WORKEVENT, " &
            "StaffResponsible, UniqueNumber, " &
            "TO_DATE(RECEIVEDDATE, 'dd-Mon-YY') as RECEIVEDDATE, " &
            "TO_DATE(INSPECTIONDATE, 'dd-Mon-YY') as INSPECTIONDATE, " &
            "TO_DATE(FCEDATE, 'dd-Mon-YY') as FCEDATE, " &
            "TO_DATE(DISCOVERYDATE, 'dd-Mon-YY') as DISCOVERYDATE, " &
            "NUMUSERID, FLAG, COMPLETESTATUS, " &
            "CURRENTSTATUS, " &
            "to_date(INSDATE, 'dd-Mon-YY') as InsDate, " &
            "strNotificationType, " &
            "TO_DATE(LASTMODIFIED) as LASTMODIFIED " &
            "from " &
            "(select " &
            "SUBSTR(SSCPITEMMASTER.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
            "STRFACILITYNAME, STRCLASS, " &
            "case " &
            "when STREVENTTYPE = '05' then (strActivityName||'-'||strNotificationDesc) " &
            "else STRACTIVITYNAME " &
            "end WorkEvent, " &
            "(STRLASTNAME||', '||STRFIRSTNAME) as STAFFRESPONSIBLE, " &
            "SSCPITEMMASTER.STRTRACKINGNUMBER as UNIQUENUMBER, " &
            "to_char(SSCPITEMMASTER.DATRECEIVEDDATE) as RECEIVEDDATE, " &
            "case " &
            "when TO_CHAR(DATINSPECTIONDATESTART) is null then '' " &
            "else TO_CHAR(DATINSPECTIONDATESTART) " &
            "end INSPECTIONDATE, " &
            "'' as FCEDATE, " &
            "'' as DISCOVERYDATE, " &
            "to_char(SSCPITEMMASTER.STRRESPONSIBLESTAFF) as NUMUSERID, " &
            "case " &
            "when STREVENTTYPE = '01' then 'IT' " &
            "when STREVENTTYPE = '02' then 'IT' " &
            "when STREVENTTYPE = '03' then 'IT' " &
            "when STREVENTTYPE = '04' then 'IT' " &
            "when STREVENTTYPE = '05' then 'IT' " &
            "when STREventType = '07' then 'IT' " &
            "end FLAG, " &
            "to_char(datCompleteDate) as CompleteStatus, " &
            "case " &
            "when STRDELETE is not null then 'Deleted' " &
            "when DATCOMPLETEDATE is not null then 'Closed' " &
            "else 'Open' " &
            "end CURRENTSTATUS, " &
            "case " &
            "when TO_CHAR(DATINSPECTIONDATESTART) is null then '' " &
            "else TO_CHAR(DATINSPECTIONDATESTART) " &
            "end INSDate, " &
            "strNotificationType, " &
            "case " &
            "when STREVENTTYPE = '01' then SSCPREPORTS.DATMODIFINGDATE " &
            "when STREVENTTYPE = '02' then SSCPINSPECTIONS.DATMODIFINGDATE " &
            "when STREVENTTYPE = '03' then sscptestreports.datmodifingdate " &
            "when STREVENTTYPE = '04' then SSCPACCS.DATMODIFINGDATE  " &
            "when STREVENTTYPE = '05' then SSCPNOTIFICATIONS.DATMODIFINGDATE " &
            "when strEventType = '07' then SSCPINSPECTIONS.DATMODIFINGDATE " &
            "end LASTMODIFIED " &
            "from SSCPITEMMASTER, APBFACILITYINFORMATION, " &
            "LOOKUPCOMPLIANCEACTIVITIES, EPDUSERPROFILES, " &
            "APBHEADERDATA, SSCPINSPECTIONS, " &
            "SSCPNOTIFICATIONS, LOOKUPSSCPNOTIFICATIONS, " &
            "SSCPREPORTS, SSCPACCS, " &
            "sscptestreports " &
            "where APBFACILITYINFORMATION.STRAIRSNUMBER = SSCPITEMMASTER.STRAIRSNUMBER   " &
            "and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber " &
            "and SSCPITEMMASTER.STREVENTTYPE = LOOKUPCOMPLIANCEACTIVITIES.STRACTIVITYTYPE " &
            "and SSCPITEMMASTER.STRRESPONSIBLESTAFF = EPDUSERPROFILES.NUMUSERID  " &
            "and SSCPITEMMASTER.STRTRACKINGNUMBER = SSCPINSPECTIONS.STRTRACKINGNUMBER (+) " &
            "and SSCPITEMMASTER.STRTRACKINGNUMBER = SSCPNOTIFICATIONS.STRTRACKINGNUMBER (+) " &
            "and SSCPNOTIFICATIONS.STRNOTIFICATIONTYPE = LOOKUPSSCPNOTIFICATIONS.STRNOTIFICATIONKEY  (+) " &
            "and SSCPITEMMASTER.STRTRACKINGNUMBER = SSCPREPORTS.STRTRACKINGNUMBER (+) " &
            "and SSCPITEMMASTER.STRTRACKINGNUMBER = SSCPACCS.STRTRACKINGNUMBER (+) " &
            "and SSCPITEMMASTER.STRTRACKINGNUMBER = SSCPTESTREPORTS.STRTRACKINGNUMBER (+) " &
            "union " &
            "select " &
            "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
            "strFacilityName, strClass, " &
            "'Full Compliance Evaluation' as WorkEvent, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " &
            "SSCPFCEMaster.strFCENumber as UniqueNumber, " &
            "'' as ReceivedDate, '' as INSPECTIONDATE, " &
            "to_char(datFCECompleted) as FCEDate, " &
            "'' as DISCOVERYDATE, " &
            "to_char(numUserID) as numUserID, " &
            "'FC' as FLAG, " &
            "to_char(datFCECompleted) as CompleteStatus, " &
            "case " &
            "when datFCECompleted is Not Null then 'Closed' " &
            "else 'Open' " &
            "End CurrentStatus, " &
            "'' AS INSDate, " &
            "'' as strNotificationType, " &
            "SSCPFCE.datModifingDate as LastModified " &
            "from " &
            "APBFACILITYINFORMATION, SSCPFCEMASTER, " &
            "SSCPFCE, EPDUSERPROFILES, " &
            "APBHeaderData " &
            "where APBFACILITYINFORMATION.STRAIRSNUMBER = SSCPFCEMASTER.STRAIRSNUMBER " &
            "and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber  " &
            "and EPDUSERPROFILES.NUMUSERID = SSCPFCE.STRREVIEWER  " &
            "and SSCPFCEMaster.strFCENumber = SSCPFCE.strFCENumber  " &
            "union " &
            "select " &
            "SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
            "STRFACILITYNAME, STRCLASS, " &
            "'Enforcement-'||stractiontype as WorkEvent, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " &
            "SSCP_AuditedEnforcement.strEnforcementNumber as UniqueNumber, " &
            "'' as ReceivedDate, '' as InspectionDate, '' as FCEDate, " &
            "TO_CHAR(DATDISCOVERYDATE) as DISCOVERYDATE, " &
            "to_char(numUserID) as numuserID, " &
            "'EN' as FLAG, " &
            "to_char(datEnforcementFinalized) as CompleteStatus, " &
            "case " &
              "when datEnforcementFinalized is Not Null then 'Closed' " &
            "else 'Open' " &
            "End CurrentStatus, " &
            "'' AS INSDate,  " &
            "'' as strNotificationType, " &
            "SSCP_AuditedEnforcement.datModifingDate as LASTMODIFIED " &
            "from " &
            "APBFACILITYINFORMATION, SSCP_AuditedEnforcement, " &
            "EPDUSERPROFILES, APBHEADERDATA " &
            "where APBFacilityInformation.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber " &
            "and EPDUserProfiles.numUserID = SSCP_AuditedEnforcement.numStaffResponsible " &
            "and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber) AllData " &
            "left join (select " &
            "SUBSTR(SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
            "NUMSSCPENGINEER " &
            "from SSCPINSPECTIONSREQUIREd, " &
            "(select " &
            "max(INTYEAR) MAXYEAR,  SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " &
            "from SSCPINSPECTIONSREQUIRED " &
            "group by SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER) MAXRESULTS " &
            "where SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = MAXRESULTS.STRAIRSNUMBER " &
            "and SSCPINSPECTIONSREQUIRED.INTYEAR = MAXRESULTS.MAXYEAR ) RESPONSIBLESTAFF " &
            "on Alldata.AIRSNumber = REsponsibleStaff.AIRSNumber " &
            " where 1 = 1 "

            If chbAllWork.Checked <> True Then
                If chbACCs.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent = 'Annual Compliance Certification' or "
                End If
                If chbEnforcement.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent like 'Enforcement%' or "
                End If
                If chbFCE.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent = 'Full Compliance Evaluation' or "
                End If
                If chbInspections.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent = 'Inspection' or "
                End If

                If chbRMPInspections.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent = 'RMP Inspection' or "
                End If

                If chbNotifications.Checked = True Then
                    If chbNotifications.Checked = True Then
                        NotificationData = "and ( "
                        For y As Integer = 0 To clbNotifications.Items.Count - 1
                            If clbNotifications.GetItemChecked(y) = True Then
                                clbNotifications.SelectedIndex = y
                                NotificationData = NotificationData & " strNotificationType = '" & clbNotifications.SelectedValue & "' or "
                            End If
                        Next
                        If NotificationData.Length > 6 Then
                            NotificationData = Mid(NotificationData, 1, NotificationData.Length - 3) & ") "
                        Else
                            NotificationData = " "
                        End If
                    Else
                        NotificationData = " "
                    End If
                    SQLLine = SQLLine & " WorkEvent like 'Notification%' " & NotificationData & " or "
                End If
                If chbPerformanceTests.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent = 'Performance Tests' or "
                End If
                If chbReports.Checked = True Then
                    SQLLine = SQLLine & " WorkEvent = 'Report' or "
                End If
            Else
                SQLLine = ""

            End If

            If SQLLine <> "" Then
                SQLLine = " and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "
            End If

            SQL = SQL & SQLLine

            SQLLine = ""

            If chbFilterDates.Checked = True Then
                If chbAllWork.Checked = True Then
                    SQLLine = " and (to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " &
                    "or to_date(InspectionDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " &
                    "or to_date(FCEDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " &
                    "or to_date(discoverydate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " &
                    "or to_date(LASTMODIFIED) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "') "
                Else
                    If chbACCs.Checked = True Then
                        SQLLine = SQLLine & " to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' or "
                    End If
                    If chbEnforcement.Checked = True Then
                        SQLLine = SQLLine & " to_date(discoverydate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' or "
                    End If
                    If chbFCE.Checked = True Then
                        SQLLine = SQLLine & " to_date(FCEDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' or "
                    End If
                    If chbInspections.Checked = True Then
                        SQLLine = SQLLine & " to_date(InspectionDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "'or "
                    End If
                    If chbNotifications.Checked = True Then
                        SQLLine = SQLLine & " to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "'or "
                    End If
                    If chbPerformanceTests.Checked = True Then
                        SQLLine = SQLLine & " to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "'or "
                    End If
                    If chbReports.Checked = True Then
                        SQLLine = SQLLine & " to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "'or "
                    End If
                    If chbRMPInspections.Checked = True Then
                        SQLLine = SQLLine & " to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "'or "
                    End If
                    If chbLastModifiedDate.Checked = True Then
                        If SQLLine <> "" Then
                            SQLLine = " and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) &
                            " or to_date(LASTMODIFIED) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' ) "
                        End If
                    Else
                        SQLLine = " and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "
                    End If
                End If
            End If
            SQL = SQL & SQLLine

            SQLLine = ""

            If rdbUseEngineer.Checked = True Then
                If Me.chbEngineer.Checked = True Then
                    SQLLine = " numUserID = '" & CurrentUser.UserID & "' Or "
                Else
                    '                    Dim GCode As String = ""
                    For x As Integer = 0 To clbEngineer.Items.Count - 1
                        If clbEngineer.GetItemChecked(x) = True Then
                            clbEngineer.SelectedIndex = x
                            SQLLine = SQLLine & " (numUserID = '" & clbEngineer.SelectedValue & "' " &
                            "or NUMSSCPENGINEER = '" & clbEngineer.SelectedValue & "') Or "
                        End If
                    Next
                End If
            End If

            If rdbUseUnits.Checked = True Then
                For x As Integer = 0 To clbAirBranchUnits.Items.Count - 1
                    If clbAirBranchUnits.GetItemChecked(x) = True Then
                        clbAirBranchUnits.SelectedIndex = x
                        temp = clbAirBranchUnits.SelectedValue
                        SubSQL = "select numUserId " &
                        "from EPDUserProfiles " &
                        "where numUnit = '" & temp & "' and numProgram = '4' "
                        cmd = New SqlCommand(SubSQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            SQLLine = SQLLine & " (numUserID = '" & dr.Item("numUserID") & "' " &
                            "or NUMSSCPENGINEER = '" & dr.Item("numUserID") & "') Or "
                        End While
                        dr.Close()

                    End If
                Next

                For x As Integer = 0 To clbDistrictOffices.Items.Count - 1
                    If clbDistrictOffices.GetItemChecked(x) = True Then
                        clbDistrictOffices.SelectedIndex = x
                        temp = clbDistrictOffices.SelectedValue

                        SubSQL = "select numUserId " &
                        "from EPDUserProfiles " &
                        "where numProgram = '" & temp & "' and numBranch = '5' "
                        cmd = New SqlCommand(SubSQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            SQLLine = SQLLine & " (numUserID = '" & dr.Item("numUserID") & "' " &
                            "or NUMSSCPENGINEER = '" & dr.Item("numUserID") & "') Or "
                        End While
                        dr.Close()

                    End If
                Next
            End If

            If SQLLine <> "" Then
                SQLLine = " And ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "
            End If

            SQL = SQL & SQLLine

            SQLLine = ""

            If chbOpenWork.Checked = True Then
                SQLLine = SQLLine & " CurrentStatus = 'Open' or "
            End If
            If chbCompletedWork.Checked = True Then
                SQLLine = SQLLine & " CurrentStatus = 'Closed' or "
            End If
            If chbDeletedWork.Checked = True Then
                SQLLine = SQLLine & " CurrentStatus = 'Deleted' or "
            End If

            If SQLLine <> "" Then
                SQLLine = " And ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "
            End If

            SQL = SQL & SQLLine

            SQLLine = ""

            If txtAIRSNumberFilter.Text <> "" Then
                SQL = SQL & " and AllData.AIRSNumber like '%" & txtAIRSNumberFilter.Text & "%' "
            End If
            If txtTrackingNumberFilter.Text <> "" Then
                SQL = SQL & " and Flag = 'IT' and UniqueNumber like '%" & txtTrackingNumberFilter.Text & "%' "
            End If
            If txtEnforcementNumberFilter.Text <> "" Then
                SQL = SQL & " and Flag = 'EN' and UniqueNumber like '%" & txtEnforcementNumberFilter.Text & "%' "
            End If
            If txtFCENumberFilter.Text <> "" Then
                SQL = SQL & " and Flag = 'FC' and UniqueNumber like '%" & txtFCENumberFilter.Text & "%' "
            End If
            If txtFacilityNameFilter.Text <> "" Then
                SQL = SQL & " and Upper(strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
            End If

            dsWorkEntry = New DataSet
            daWorkEntry = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEntry.Fill(dsWorkEntry, "WorkEntry")
            dgvWork.DataSource = dsWorkEntry
            dgvWork.DataMember = "WorkEntry"

            txtWorkCount.Text = dgvWork.RowCount.ToString

            dgvWork.RowHeadersVisible = False
            dgvWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWork.AllowUserToResizeColumns = True
            dgvWork.AllowUserToAddRows = False
            dgvWork.AllowUserToDeleteRows = False
            dgvWork.AllowUserToOrderColumns = True
            dgvWork.AllowUserToResizeRows = True
            dgvWork.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dgvWork.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWork.Columns("AIRSNumber").DisplayIndex = 0
            dgvWork.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWork.Columns("strFacilityName").DisplayIndex = 1
            dgvWork.Columns("strFacilityName").Width = "200"
            dgvWork.Columns("strClass").HeaderText = "Classification"
            dgvWork.Columns("strClass").DisplayIndex = 2
            dgvWork.Columns("WorkEvent").HeaderText = "Work Type"
            dgvWork.Columns("WorkEvent").DisplayIndex = 3
            dgvWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvWork.Columns("StaffResponsible").DisplayIndex = 4
            dgvWork.Columns("StaffResponsible").Width = 100
            dgvWork.Columns("UniqueNumber").HeaderText = "Unique #"
            dgvWork.Columns("UniqueNumber").DisplayIndex = 5
            dgvWork.Columns("CurrentStatus").HeaderText = "Current Status"
            dgvWork.Columns("CurrentStatus").DisplayIndex = 6
            dgvWork.Columns("ReceivedDate").HeaderText = "Received Date"
            dgvWork.Columns("ReceivedDate").DisplayIndex = 7
            dgvWork.Columns("ReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("InspectionDate").HeaderText = "Inspection Date"
            dgvWork.Columns("InspectionDate").DisplayIndex = 8
            dgvWork.Columns("InspectionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("FCEDate").HeaderText = "FCE Date"
            dgvWork.Columns("FCEDate").DisplayIndex = 9
            dgvWork.Columns("FCEDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("DiscoveryDate").HeaderText = "Discovery Date"
            dgvWork.Columns("DiscoveryDate").DisplayIndex = 10
            dgvWork.Columns("DiscoveryDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("numUserID").HeaderText = "User ID"
            dgvWork.Columns("numUserID").DisplayIndex = 11
            dgvWork.Columns("numUserID").Visible = False
            dgvWork.Columns("Flag").HeaderText = "Flag"
            dgvWork.Columns("Flag").DisplayIndex = 12
            dgvWork.Columns("Flag").Visible = False
            dgvWork.Columns("CompleteStatus").HeaderText = "Complete Status"
            dgvWork.Columns("CompleteStatus").DisplayIndex = 13
            dgvWork.Columns("CompleteStatus").Visible = False
            dgvWork.Columns("INSDate").HeaderText = "Insp. Date"
            dgvWork.Columns("INSDate").DisplayIndex = 14
            dgvWork.Columns("INSDate").Visible = False
            dgvWork.Columns("INSDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("strNotificationType").HeaderText = "Notification Type"
            dgvWork.Columns("strNotificationType").DisplayIndex = 15
            dgvWork.Columns("strNotificationType").Visible = False
            dgvWork.Columns("LastModified").HeaderText = "Last Modified"
            dgvWork.Columns("LastModified").DisplayIndex = 16
            '  dgvWork.Columns("LastModified").Visible = False
            dgvWork.Columns("LastModified").DefaultCellStyle.Format = "dd-MMM-yyyy"





        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub AddNewEvent()
        Try
            If rdbEnforcementAction.Checked = True Then
                OpenFormEnforcement(New Apb.ApbFacilityId(txtNewAIRSNumber.Text))
            ElseIf rdbFCE.Checked = True Then
                OpenFormFce(New Apb.ApbFacilityId(txtNewAIRSNumber.Text))
            ElseIf rdbPerformanceTest.Checked = True Then
                If txtTrackingNumber.Text <> "" Then
                    Dim RefNum As String = ""

                    SQL = "Select " &
                    "ISMPReportInformation.strReferenceNumber, " &
                    "ISMPDocumentType.strDocumentType " &
                    "from ISMPReportInformation, " &
                    "ISMPDocumentType " &
                    "where ISMPReportInformation.strDocumentType = ISMPDocumentType.strKEy " &
                    "and strReferenceNumber = '" & txtTrackingNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        RefNum = dr.Item("strReferenceNumber")
                    Else
                        RefNum = ""
                    End If
                    dr.Close()
                    If RefNum <> "" Then
                        If DAL.Ismp.StackTestExists(RefNum) Then OpenMultiForm(ISMPTestReports, RefNum)
                    Else
                        MsgBox("The Reference Number is not valid." & vbCrLf &
                        "Please check the number you entered.", MsgBoxStyle.Information, "SSCP Compliance Log")
                    End If
                End If
            ElseIf Me.rdbOther.Checked = True Then
                If Me.cboEvent.SelectedIndex > 0 Then
                    SQL = "Insert into SSCPItemMaster " &
                    "(strTrackingNumber, strAIRSNumber, " &
                    "datReceivedDate, strEventType, " &
                    "strResponsibleStaff, datCompleteDate, " &
                    "strModifingPerson, datModifingDate) " &
                    "values " &
                    "(SSCPTrackingNumber.nextval, '0413" & txtNewAIRSNumber.Text & "', " &
                    "'" & DTPDateReceived.Text & "', " &
                    "'" & cboEvent.SelectedValue & "', " &
                    "'" & CurrentUser.UserID & "', '', " &
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "')"

                    If cboEvent.SelectedValue = "04" Then
                        SQL2 = "Insert into SSCPItemMaster " &
                        "(strTrackingNumber, strAIRSNumber, " &
                        "datReceivedDate, strEventType, " &
                        "strResponsibleStaff, datCompleteDate, " &
                        "strModifingPerson, datModifingDate) " &
                        "values " &
                        "(SSCPTrackingNumber.nextval, '0413" & txtNewAIRSNumber.Text & "', " &
                        "'" & DTPDateReceived.Text & "', " &
                        "'06', " &
                        "'" & CurrentUser.UserID & "', '" & DTPDateReceived.Text & "', " &
                        "'" & CurrentUser.UserID & "', '" & OracleDate & "')"
                    Else
                        SQL2 = ""
                    End If

                    SQL3 = "Select SSCPTrackingNumber.Currval from Dual"

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    cmd2 = New SqlCommand(SQL2, CurrentConnection)
                    cmd3 = New SqlCommand(SQL3, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    dr3 = cmd3.ExecuteReader


                    While dr3.Read
                        txtTrackingNumber.Text = dr3.Item(0)
                    End While

                    If SQL2 <> "" Then
                        cmd2.ExecuteReader()
                    End If

                    OpenFormSscpWorkItem(txtTrackingNumber.Text)
                Else
                    MsgBox("Select a Work type and event type if needed.", MsgBoxStyle.Information, "Work Entry")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub DeleteWork()
        Try


            Dim DeleteStatus As String = ""
            Dim tempAIRS As String = ""

            Select Case txtTestType.Text
                Case "Annual Compliance Certification"
                    SQL = "Select strTrackingNumber " &
                    "from SSCPItemMaster " &
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?",
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update SSCPItemMaster set " &
                                "strDelete = 'True' " &
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                LoaddgvWork()

                            Case DialogResult.No
                                SQL = ""
                            Case DialogResult.Cancel
                                SQL = ""
                            Case Else
                                SQL = ""
                        End Select
                    End If
                Case "Enforcement-LON"
                    ' Case Enforcement-LON MUST precede the next Case, which is a catch-all for remaining enforcement

                    DeleteStatus = MessageBox.Show("Should this Enforcement Item be deleted? (This cannot be undone!)",
                                     "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    If DeleteStatus = DialogResult.Yes Then

                        SQL = "Delete SSCPEnforcementLetter " &
                        "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete SSCPEnforcementStipulated " &
                        "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete SSCP_AuditedEnforcement " &
                        "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        LoadDefaultSettings()
                        LoaddgvWork()
                        MsgBox("Enforcement Deleted", MsgBoxStyle.Information, "Compliance Log")
                    End If
                Case "Enforcement-" To "Enforcement-z"
                    ' This is a catch-all for all non-LON enforcement

                    SQL = "Select strUpDateStatus " &
                    "from AFSSSCPEnforcementRecords " &
                    "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    temp = ""
                    While dr.Read
                        If IsDBNull(dr.Item("strUpDateStatus")) Then
                            temp = ""
                        Else
                            temp = dr.Item("strUpDateStatus")
                        End If
                    End While
                    dr.Close()
                    If temp = "C" Or temp = "N" Then
                        MsgBox("This Enforcement Action has already been submitted to EPA, contact EPD IT to delete this Enforcement Action.",
                               MsgBoxStyle.Information, "Compliance Log")
                    Else
                        DeleteStatus = MessageBox.Show("Should this Enforcement Item be deleted? (This cannot be undone!)",
                                     "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Select strAIRSNumber " &
                                "from SSCP_AuditedEnforcement " &
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                                        tempAIRS = ""
                                    Else
                                        tempAIRS = dr.Item("strAIRSNumber")
                                    End If
                                End While
                                dr.Close()

                                SQL = "Delete AFSSSCPEnforcementRecords " &
                                                      "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                                "values " &
                                "(" &
                                "(select " &
                                "case when max(numCounter) is null then 1 " &
                                "else max(numCounter) + 1 " &
                                "end numCounter " &
                                "from AFSDeletions), " &
                                "'" & tempAIRS & "', " &
                                "'" & Replace(SQL, "'", "''") & "', 'True', " &
                                "'" & OracleDate & "', '', " &
                                "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete SSCPEnforcementLetter " &
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()



                                SQL = "Delete SSCPEnforcementStipulated " &
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete SSCP_AuditedEnforcement " &
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                LoadDefaultSettings()
                                LoaddgvWork()
                                MsgBox("Enforcement Deleted", MsgBoxStyle.Information, "Compliance Log")
                            Case DialogResult.No
                                SQL = ""
                            Case DialogResult.Cancel
                                SQL = ""
                            Case Else
                                SQL = ""
                        End Select
                    End If

                Case "Full Compliance Evaluation"
                    SQL = "Select strUpDateStatus " &
                    "from AFSSSCPFCERecords " &
                    "where strFceNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    temp = ""
                    While dr.Read
                        If IsDBNull(dr.Item("strUpDateStatus")) Then
                            temp = ""
                        Else
                            temp = dr.Item("strUpDateStatus")
                        End If
                    End While
                    dr.Close()
                    If temp = "C" Or temp = "N" Then
                        MsgBox("This FCE has already been submitted to EPA, contact EPD IT to delete this FCE.", MsgBoxStyle.Information, "Compliance Log")
                    Else

                        DeleteStatus = MessageBox.Show("Should this FCE be deleted? (This cannot be undone!)",
                                     "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Select strAIRSNumber " &
                                "from SSCPItemMaster " &
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                                        tempAIRS = ""
                                    Else
                                        tempAIRS = dr.Item("strAIRSNumber")
                                    End If
                                End While
                                dr.Close()


                                SQL = "Delete AFSSSCPFCERecords " &
                            "where strFCENumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete SSCPFCE " &
                                "where strFCENumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete SSCPFCEMaster " &
                                "where strFCENumber = '" & txtWorkNumber.Text & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(SQL, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                                cmd = New SqlCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                LoadDefaultSettings()
                                LoaddgvWork()
                                MsgBox("FCE Deleted.", MsgBoxStyle.Information, "Compliance Log")
                            Case DialogResult.No
                                SQL = ""
                            Case DialogResult.Cancel
                                SQL = ""
                            Case Else
                                SQL = ""
                        End Select
                    End If
                Case "Inspection"
                    SQL = "Select strTrackingNumber " &
                    "from SSCPItemMaster " &
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?",
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update SSCPItemMaster set " &
                                "strDelete = 'True' " &
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                LoaddgvWork()

                            Case DialogResult.No
                                SQL = ""
                            Case DialogResult.Cancel
                                SQL = ""
                            Case Else
                                SQL = ""
                        End Select
                    End If
                Case "Notification" To "Notification-z"
                    SQL = "Select strTrackingNumber " &
                    "from SSCPItemMaster " &
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?",
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update SSCPItemMaster set " &
                                "strDelete = 'True' " &
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                LoaddgvWork()

                            Case DialogResult.No
                                SQL = ""
                            Case DialogResult.Cancel
                                SQL = ""
                            Case Else
                                SQL = ""
                        End Select
                    End If
                Case "Performance Tests"
                    MessageBox.Show("Performance tests must be deleted by ISMP.", "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                Case "Report"
                    SQL = "Select strTrackingNumber " &
                    "from SSCPItemMaster " &
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?",
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update SSCPItemMaster set " &
                                "strDelete = 'True' " &
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                LoaddgvWork()

                            Case DialogResult.No
                                SQL = ""
                            Case DialogResult.Cancel
                                SQL = ""
                            Case Else
                                SQL = ""
                        End Select
                    End If
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub UnDeleteWork()
        Try

            SQL = "Select strTrackingNumber " &
            "from SSCPItemMaster " &
            "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update SSCPItemMaster set " &
                "strDelete = '' " &
                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("Work Item Undeleted.", MsgBoxStyle.Information, "SSCP Work Items")

                LoaddgvWork()
            Else
                MsgBox("This item was unable to be undeleted.", MsgBoxStyle.Information, "Compilance Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Sub ExportToExcel()
        dgvWork.ExportToExcel()
    End Sub

#End Region

#Region "Declarations"
    Private Sub cboEvent_Leave(sender As Object, e As EventArgs) Handles cboEvent.Leave
        Dim dtActivity As New DataTable

        Try

            If cboEvent.Text <> "" Then
                LabEventDescription.Text = ""

                dtActivity = dsCompliance.Tables("ComplianceActivity")

                Dim drActivity As DataRow()
                Dim row As DataRow

                drActivity = dtActivity.Select("strActivityName = '" & cboEvent.Text & "'")

                For Each row In drActivity
                    LabEventDescription.Text = row("strActivityDescription").ToString
                Next
                If LabEventDescription.Text = "" Then
                    LabEventDescription.Text = "Description Not Found"
                End If
                If LabEventDescription.Text <> "" Then
                    LabEventDescription.Visible = True
                Else
                    LabEventDescription.Visible = False
                End If
                If cboEvent.Text = "Inspection" Then
                    lblDateField.Text = "Inspection Date:"
                Else
                    lblDateField.Text = "Received by GEPD:"
                End If
            Else
                LabEventDescription.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboEvent_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboEvent.SelectedValueChanged
        Dim dtActivity As New DataTable

        Try

            If cboEvent.SelectedIndex = 0 Then
                LabEventDescription.Text = ""
            Else
                dtActivity = dsCompliance.Tables("ComplianceActivity")

                Dim drActivity As DataRow()
                Dim row As DataRow

                drActivity = dtActivity.Select("strActivityName = '" & cboEvent.Text & "'")

                For Each row In drActivity
                    LabEventDescription.Text = row("strActivityDescription").ToString
                Next
                If cboEvent.Text = "Inspection" Then
                    lblDateField.Text = "Inspection Date:"
                Else
                    lblDateField.Text = "Received by GEPD:"
                End If
            End If

            If LabEventDescription.Text <> "" Then
                LabEventDescription.Visible = True
            Else
                LabEventDescription.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub chbEnforcementDates_CheckedChanged(sender As Object, e As EventArgs) Handles chbFilterDates.CheckedChanged
        Try


            If chbFilterDates.Checked = True Then
                DTPFilterStart.Enabled = True
                DTPFilterEnd.Enabled = True
            Else
                DTPFilterStart.Enabled = False
                DTPFilterEnd.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRunFilter_Click(sender As Object, e As EventArgs) Handles btnRunFilter.Click
        LoaddgvWork()
    End Sub
    Private Sub dgvWork_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvWork.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvWork.HitTest(e.X, e.Y)

        Try

            If dgvWork.RowCount > 0 And hti.RowIndex <> -1 Then
                txtAIRSNumber.Text = dgvWork(0, hti.RowIndex).Value
                txtNewAIRSNumber.Text = dgvWork(0, hti.RowIndex).Value
                txtFacilityName.Text = dgvWork(1, hti.RowIndex).Value
                txtWorkNumber.Text = dgvWork(5, hti.RowIndex).Value
                txtTestType.Text = dgvWork(3, hti.RowIndex).Value
                'lblWorkType.Text = dgvWork(2, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtAIRSNumber.TextChanged
        Try


            If txtAIRSNumber.Text <> "" Then
                SQL = "Select " &
                "strFacilityCity, strCountyName " &
                "from APBFacilityInformation, LookUpCountyInformation " &
                "where substr(strAIRSNumber, 5, 3) = strCountyCode " &
                "and strAIRSnumber = '0413" & txtAIRSNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtFacilityCity.Text = ""
                    Else
                        txtFacilityCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        txtFacilityCounty.Text = ""
                    Else
                        txtFacilityCounty.Text = dr.Item("strCountyName")
                    End If
                End While
                dr.Close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSelectWork_Click(sender As Object, e As EventArgs) Handles btnSelectWork.Click
        Try
            If txtTestType.Text <> "" Then
                If InStr(txtTestType.Text, "Enforcement") > 0 Then
                    OpenFormEnforcement(txtWorkNumber.Text)
                ElseIf InStr(txtTestType.Text, "Full Compliance Evaluation") > 0 Then
                    OpenFormFce(txtWorkNumber.Text)
                Else
                    OpenFormSscpWorkItem(txtWorkNumber.Text)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtNewAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtNewAIRSNumber.TextChanged
        txtFacilityInformation.Text = ""
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
            Dim fac As Apb.Facilities.Facility = DAL.FacilityData.GetFacility(txtNewAIRSNumber.Text)
            If fac IsNot Nothing Then txtFacilityInformation.Text = fac.LongDisplay
        End If
    End Sub
    Private Sub rdbPerformanceTest_CheckedChanged(sender As Object, e As EventArgs) Handles rdbPerformanceTest.CheckedChanged
        Try


            If rdbPerformanceTest.Checked = True Then
                pnlOtherEvents.Visible = True
                lblDateField.Visible = False
                DTPDateReceived.Visible = False
                Label8.Visible = False
                cboEvent.Visible = False
                LabEventDescription.Visible = False
                lblOtherNumber.Text = "ISMP Reference Number"
                txtTrackingNumber.ReadOnly = False
                txtTrackingNumber.Clear()
            Else
                pnlOtherEvents.Visible = False
                lblDateField.Visible = True
                DTPDateReceived.Visible = True
                Label8.Visible = True
                cboEvent.Visible = True
                LabEventDescription.Visible = True
                lblOtherNumber.Text = "Tracking Number"
                txtTrackingNumber.ReadOnly = True
                txtTrackingNumber.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub rdbOther_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOther.CheckedChanged
        Try


            If rdbOther.Checked = True Then
                pnlOtherEvents.Visible = True
            Else
                pnlOtherEvents.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddNewEnTry_Click(sender As Object, e As EventArgs) Handles btnAddNewEntry.Click
        Try
            If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
                AddNewEvent()
            Else
                MsgBox("Invalid AIRS Number.", MsgBoxStyle.Information, "Work Entry")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(Value As String)
            txtAIRSNumberFilter.Text = Value
        End Set
    End Property
    Public WriteOnly Property ValueFromFacilityLookUp2() As String
        Set(Value As String)
            txtFacilityNameFilter.Text = Value
        End Set
    End Property
    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
                Me.ValueFromFacilityLookUp2 = facilityLookupDialog.SelectedFacilityName
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FacilitySearch()
        OpenFacilityLookupTool()
    End Sub
    Private Sub TBWork_EnTry_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles TBWork_EnTry.ButtonClick
        Try

            Select Case TBWork_EnTry.Buttons.IndexOf(e.Button)
                Case 0
                    FacilitySearch()
                Case 1
                    ExportToExcel()
                Case 2
                    LoadDefaultSettings()
                Case Else
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnDeleteWork_Click(sender As Object, e As EventArgs) Handles btnDeleteWork.Click
        DeleteWork()
    End Sub
    Private Sub btnUndeleteWork_Click(sender As Object, e As EventArgs) Handles btnUndeleteWork.Click
        UnDeleteWork()
    End Sub
    Private Sub txtWorkNumber_Leave(sender As Object, e As EventArgs) Handles txtWorkNumber.Leave
        Try

            If txtWorkNumber.Text = "" Then
                txtAIRSNumber.Clear()
                txtFacilityName.Clear()
                txtWorkNumber.Clear()
                txtTestType.Clear()
                txtFacilityCity.Clear()
                txtFacilityCounty.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenSummary_Click(sender As Object, e As EventArgs) Handles btnOpenSummary.Click
        OpenFormFacilitySummary(txtAIRSNumber.Text)
    End Sub

#End Region

    Private Sub chbNotifications_CheckedChanged(sender As Object, e As EventArgs) Handles chbNotifications.CheckedChanged
        Try
            If chbNotifications.Checked = True Then
                GBNotifications.Enabled = True
                '  GBWorkTypes.Size = New System.Drawing.Size(208, 310)
            Else
                GBNotifications.Enabled = False
                ' GBWorkTypes.Size = New System.Drawing.Size(208, 168)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbIgnoreEngineer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbIgnoreEngineer.CheckedChanged
        Try
            If rdbIgnoreEngineer.Checked = True Then
                chbEngineer.Enabled = False
                clbEngineer.Enabled = False
                clbDistrictOffices.Enabled = False
                clbAirBranchUnits.Enabled = False
            Else
                chbEngineer.Enabled = True
                clbEngineer.Enabled = True
                clbDistrictOffices.Enabled = False
                clbAirBranchUnits.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbUseEngineer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUseEngineer.CheckedChanged
        Try
            If rdbUseEngineer.Checked = True Then
                chbEngineer.Enabled = True
                clbEngineer.Enabled = True
                clbDistrictOffices.Enabled = False
                clbAirBranchUnits.Enabled = False
            Else
                chbEngineer.Enabled = False
                clbEngineer.Enabled = False
                clbDistrictOffices.Enabled = False
                clbAirBranchUnits.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbUseUnits_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUseUnits.CheckedChanged
        Try
            If rdbUseUnits.Checked = True Then
                chbEngineer.Enabled = False
                clbEngineer.Enabled = False
                clbDistrictOffices.Enabled = True
                clbAirBranchUnits.Enabled = True
            Else
                chbEngineer.Enabled = False
                clbEngineer.Enabled = False
                clbDistrictOffices.Enabled = False
                clbAirBranchUnits.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Menu"
    Private Sub mmiClose_Click(sender As Object, e As EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiOnlineHelp_Click(sender As Object, e As EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub mmiExport_Click(sender As Object, e As EventArgs) Handles mmiExport.Click
        dgvWork.ExportToExcel()
    End Sub

    Private Sub mmiClear_Click(sender As Object, e As EventArgs) Handles mmiClear.Click
        LoadDefaultSettings()
    End Sub

    Private Sub mmiSearch_Click(sender As Object, e As EventArgs) Handles mmiSearch.Click
        FacilitySearch()
    End Sub

    Private Sub mmiRunFilter_Click(sender As Object, e As EventArgs) Handles mmiRunFilter.Click
        LoaddgvWork()
    End Sub
#End Region

#Region "Change Accept Button"

    Private Sub pnlFilterPanel_Enter(sender As Object, e As EventArgs) Handles pnlFilterPanel.Enter
        Me.AcceptButton = btnRunFilter
    End Sub

    Private Sub btnRunFilter_Leave(sender As Object, e As EventArgs) Handles btnRunFilter.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

End Class