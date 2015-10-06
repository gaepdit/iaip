Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic


Public Class SSCPComplianceLog
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr3 As OracleDataReader
    Dim recExist As Boolean
    Dim dsCompliance As DataSet
    Dim daCompliance As OracleDataAdapter
    Dim dsStaff As DataSet
    Dim daStaff As OracleDataAdapter
    Dim dsWorkEntry As DataSet
    Dim daWorkEntry As OracleDataAdapter
    Dim dsNotifications As DataSet
    Dim daNotifications As OracleDataAdapter
    Dim dsComplianceUnit As DataSet
    Dim daComplianceUnit As OracleDataAdapter
    Dim dsDistrictUnit As DataSet
    Dim daDistrictUnit As OracleDataAdapter

    Private Sub DevWorkEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate
            DTPDateReceived.Text = OracleDate

            chbEngineer.Text = UserName

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
            And Not UserAccounts.Contains("(19)") _
            And Not UserAccounts.Contains("(113)") _
            And Not UserAccounts.Contains("(114)") _
            And Not UserAccounts.Contains("(141)") _
            And Not UserAccounts.Contains("(118)") _
            Then
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Page Load"
    Private Sub LoadDataSets()
        Try

            dsCompliance = New DataSet
            dsStaff = New DataSet
            dsNotifications = New DataSet
            dsComplianceUnit = New DataSet
            dsDistrictUnit = New DataSet


            SQL = "select strActivityType, strActivityName, strActivityDescription " & _
            "from AIRBRANCH.LookUPComplianceActivities " & _
            "where strActivityName <> 'Performance Tests' " & _
            "order by strActivityName"

            daCompliance = New OracleDataAdapter(SQL, CurrentConnection)

            'SQL = "Select " & _
            '"(StrLastName||', '||strFirstname) as Staff, " & _
            '"numUserID " & _
            '"from AIRBRANCH.EPDUserProfiles " & _
            '"where numProgram = '4' " & _
            '"Order by strLastName "

            'SQL = "select * " & _
            '"from " & _
            '"(Select  " & _
            '"distinct(numUserID) as numUserID, " & _
            '"(StrLastName||', '||strFirstname) as Staff " & _
            '"from AIRBRANCH.EPDUserProfiles  " & _
            '"where numProgram = '4' " & _
            '"union " & _
            '"Select  " & _
            '"distinct(numUserID) as numUserID, " & _
            '"(StrLastName||', '||strFirstname) as Staff " & _
            '"from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSCPItemMaster   " & _
            '"where AIRBRANCH.EPDUserProfiles.numuserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff) " & _
            '"where numUserID <> '0' " & _
            '"order by Staff "

            'SQL = "select * " & _
            '"from " & _
            '"(Select  " & _
            '"distinct(numUserID) as numUserID, " & _
            '"(StrLastName||', '||strFirstname) as Staff " & _
            '"from AIRBRANCH.EPDUserProfiles  " & _
            '"where numProgram = '4' " & _
            '"union " & _
            '"Select  " & _
            '"distinct(numUserID) as numUserID, " & _
            '"(StrLastName||', '||strFirstname) as Staff " & _
            '"from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSCPItemMaster   " & _
            '"where AIRBRANCH.EPDUserProfiles.numuserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff) " & _
            '"order by Staff "
            'VW_COMPLIANCESTAFF

            SQL = "Select " & _
            "numUserID, Staff " & _
            "from AIRBranch.VW_ComplianceStaff " & _
            "order by Staff "

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select " & _
            "strNotificationDesc, strNotificationKey " & _
            "from AIRBRANCH.LookUpSSCPNotifications " & _
            "order by strNotificationDesc "

            daNotifications = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "select " & _
            "strUnitDesc, numUnitCode " & _
            "from AIRBranch.LookUPEPDUnits " & _
            "where numProgramCode = '4'" & _
            "order by strUnitDesc "

            daComplianceUnit = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "select " & _
            "strProgramDesc, numProgramCode  " & _
            "from airbranch.lookupepdprograms " & _
            "where numbranchcode = '5' " & _
            "and strProgramdesc <> 'Vacant' " & _
            "and strProgramDesc <> 'Small Business Assistance Program' " & _
            "order by strprogramDesc "

            daDistrictUnit = New OracleDataAdapter(SQL, CurrentConnection)


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daCompliance.Fill(dsCompliance, "ComplianceActivity")
            daStaff.Fill(dsStaff, "Staff")
            daNotifications.Fill(dsNotifications, "Notifications")
            daComplianceUnit.Fill(dsComplianceUnit, "ComplianceUnit")
            daDistrictUnit.Fill(dsDistrictUnit, "DistrictUnit")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadComboBoxes()
        Dim dtActivity As New DataTable
        Dim dtStaff As New DataTable
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

            dtStaff.Columns.Add("Staff", GetType(System.String))
            dtStaff.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaff.NewRow
                drNewRow("Staff") = drDSRow("Staff")
                drNewRow("numUserID") = drDSRow("numUserId")
                dtStaff.Rows.Add(drNewRow)
            Next

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "select " & _
            "ALLDATA.AIRSNUMBER, STRFACILITYNAME, " & _
            "STRCLASS, WORKEVENT, " & _
            "StaffResponsible, UniqueNumber, " & _
            "TO_DATE(RECEIVEDDATE, 'dd-Mon-YY') as RECEIVEDDATE, " & _
            "TO_DATE(INSPECTIONDATE, 'dd-Mon-YY') as INSPECTIONDATE, " & _
            "TO_DATE(FCEDATE, 'dd-Mon-YY') as FCEDATE, " & _
            "TO_DATE(DISCOVERYDATE, 'dd-Mon-YY') as DISCOVERYDATE, " & _
            "NUMUSERID, FLAG, COMPLETESTATUS, " & _
            "CURRENTSTATUS, " & _
            "to_date(INSDATE, 'dd-Mon-YY') as InsDate, " & _
            "strNotificationType, " & _
            "TO_DATE(LASTMODIFIED) as LASTMODIFIED " & _
            "from " & _
            "(select " & _
            "SUBSTR(AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
            "STRFACILITYNAME, STRCLASS, " & _
            "case " & _
            "when STREVENTTYPE = '05' then (strActivityName||'-'||strNotificationDesc) " & _
            "else STRACTIVITYNAME " & _
            "end WorkEvent, " & _
            "(STRLASTNAME||', '||STRFIRSTNAME) as STAFFRESPONSIBLE, " & _
            "AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER as UNIQUENUMBER, " & _
            "to_char(AIRBRANCH.SSCPITEMMASTER.DATRECEIVEDDATE) as RECEIVEDDATE, " & _
            "case " & _
            "when TO_CHAR(DATINSPECTIONDATESTART) is null then '' " & _
            "else TO_CHAR(DATINSPECTIONDATESTART) " & _
            "end INSPECTIONDATE, " & _
            "'' as FCEDATE, " & _
            "'' as DISCOVERYDATE, " & _
            "to_char(AIRBRANCH.SSCPITEMMASTER.STRRESPONSIBLESTAFF) as NUMUSERID, " & _
            "case " & _
            "when STREVENTTYPE = '01' then 'IT' " & _
            "when STREVENTTYPE = '02' then 'IT' " & _
            "when STREVENTTYPE = '03' then 'IT' " & _
            "when STREVENTTYPE = '04' then 'IT' " & _
            "when STREVENTTYPE = '05' then 'IT' " & _
            "when STREventType = '07' then 'IT' " & _
            "end FLAG, " & _
            "to_char(datCompleteDate) as CompleteStatus, " & _
            "case " & _
            "when STRDELETE is not null then 'Deleted' " & _
            "when DATCOMPLETEDATE is not null then 'Closed' " & _
            "else 'Open' " & _
            "end CURRENTSTATUS, " & _
            "case " & _
            "when TO_CHAR(DATINSPECTIONDATESTART) is null then '' " & _
            "else TO_CHAR(DATINSPECTIONDATESTART) " & _
            "end INSDate, " & _
            "strNotificationType, " & _
            "case " & _
            "when STREVENTTYPE = '01' then AIRBRANCH.SSCPREPORTS.DATMODIFINGDATE " & _
            "when STREVENTTYPE = '02' then AIRBRANCH.SSCPINSPECTIONS.DATMODIFINGDATE " & _
            "when STREVENTTYPE = '03' then AIRBRANCH.sscptestreports.datmodifingdate " & _
            "when STREVENTTYPE = '04' then AIRBRANCH.SSCPACCS.DATMODIFINGDATE  " & _
            "when STREVENTTYPE = '05' then AIRBRANCH.SSCPNOTIFICATIONS.DATMODIFINGDATE " & _
            "when strEventType = '07' then AIRBRANCH.SSCPINSPECTIONS.DATMODIFINGDATE " & _
            "end LASTMODIFIED " & _
            "from AIRBRANCH.SSCPITEMMASTER, AIRBRANCH.APBFACILITYINFORMATION, " & _
            "AIRBRANCH.LOOKUPCOMPLIANCEACTIVITIES, AIRBRANCH.EPDUSERPROFILES, " & _
            "AIRBRANCH.APBHEADERDATA, AIRBRANCH.SSCPINSPECTIONS, " & _
            "AIRBRANCH.SSCPNOTIFICATIONS, AIRBRANCH.LOOKUPSSCPNOTIFICATIONS, " & _
            "AIRBRANCH.SSCPREPORTS, AIRBRANCH.SSCPACCS, " & _
            "AIRBRANCH.sscptestreports " & _
            "where AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER   " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
            "and AIRBRANCH.SSCPITEMMASTER.STREVENTTYPE = AIRBRANCH.LOOKUPCOMPLIANCEACTIVITIES.STRACTIVITYTYPE " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRRESPONSIBLESTAFF = AIRBRANCH.EPDUSERPROFILES.NUMUSERID  " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER = AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER (+) " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER = AIRBRANCH.SSCPNOTIFICATIONS.STRTRACKINGNUMBER (+) " & _
            "and AIRBRANCH.SSCPNOTIFICATIONS.STRNOTIFICATIONTYPE = AIRBRANCH.LOOKUPSSCPNOTIFICATIONS.STRNOTIFICATIONKEY  (+) " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER = AIRBRANCH.SSCPREPORTS.STRTRACKINGNUMBER (+) " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER = AIRBRANCH.SSCPACCS.STRTRACKINGNUMBER (+) " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER = AIRBRANCH.SSCPTESTREPORTS.STRTRACKINGNUMBER (+) " & _
            "union " & _
            "select " & _
            "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
            "strFacilityName, strClass, " & _
            "'Full Compliance Evaluation' as WorkEvent, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
            "AIRBRANCH.SSCPFCEMaster.strFCENumber as UniqueNumber, " & _
            "'' as ReceivedDate, '' as INSPECTIONDATE, " & _
            "to_char(datFCECompleted) as FCEDate, " & _
            "'' as DISCOVERYDATE, " & _
            "to_char(numUserID) as numUserID, " & _
            "'FC' as FLAG, " & _
            "to_char(datFCECompleted) as CompleteStatus, " & _
            "case " & _
            "when datFCECompleted is Not Null then 'Closed' " & _
            "else 'Open' " & _
            "End CurrentStatus, " & _
            "'' AS INSDate, " & _
            "'' as strNotificationType, " & _
            "AIRBRANCH.SSCPFCE.datModifingDate as LastModified " & _
            "from " & _
            "AIRBRANCH.APBFACILITYINFORMATION, AIRBRANCH.SSCPFCEMASTER, " & _
            "AIRBRANCH.SSCPFCE, AIRBRANCH.EPDUSERPROFILES, " & _
            "AIRBRANCH.APBHeaderData " & _
            "where AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER = AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber  " & _
            "and AIRBRANCH.EPDUSERPROFILES.NUMUSERID = AIRBRANCH.SSCPFCE.STRREVIEWER  " & _
            "and AIRBRANCH.SSCPFCEMaster.strFCENumber = AIRBRANCH.SSCPFCE.strFCENumber  " & _
            "union " & _
            "select " & _
            "SUBSTR(AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
            "STRFACILITYNAME, STRCLASS, " & _
            "'Enforcement-'||stractiontype as WorkEvent, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
            "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber as UniqueNumber, " & _
            "'' as ReceivedDate, '' as InspectionDate, '' as FCEDate, " & _
            "TO_CHAR(DATDISCOVERYDATE) as DISCOVERYDATE, " & _
            "to_char(numUserID) as numuserID, " & _
            "'EN' as FLAG, " & _
            "to_char(datEnforcementFinalized) as CompleteStatus, " & _
            "case " & _
              "when datEnforcementFinalized is Not Null then 'Closed' " & _
            "else 'Open' " & _
            "End CurrentStatus, " & _
            "'' AS INSDate,  " & _
            "'' as strNotificationType, " & _
            "AIRBRANCH.SSCP_AuditedEnforcement.datModifingDate as LASTMODIFIED " & _
            "from " & _
            "AIRBRANCH.APBFACILITYINFORMATION, AIRBRANCH.SSCP_AuditedEnforcement, " & _
            "AIRBRANCH.EPDUSERPROFILES, AIRBRANCH.APBHEADERDATA " & _
            "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber " & _
            "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber) AllData " & _
            "left join (select " & _
            "SUBSTR(AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
            "NUMSSCPENGINEER " & _
            "from AIRBRANCH.SSCPINSPECTIONSREQUIREd, " & _
            "(select " & _
            "max(INTYEAR) MAXYEAR,  AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " & _
            "from AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
            "group by AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER) MAXRESULTS " & _
            "where AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = MAXRESULTS.STRAIRSNUMBER " & _
            "and AIRBRANCH.SSCPINSPECTIONSREQUIRED.INTYEAR = MAXRESULTS.MAXYEAR ) RESPONSIBLESTAFF " & _
            "on Alldata.AIRSNumber = REsponsibleStaff.AIRSNumber " & _
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
                    SQLLine = " and (to_date(ReceivedDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " & _
                    "or to_date(InspectionDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " & _
                    "or to_date(FCEDate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " & _
                    "or to_date(discoverydate) between '" & DTPFilterStart.Text & "' and '" & DTPFilterEnd.Text & "' " & _
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
                            SQLLine = " and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & _
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
                    SQLLine = " numUserID = '" & UserGCode & "' Or "
                Else
                    '                    Dim GCode As String = ""
                    For x As Integer = 0 To clbEngineer.Items.Count - 1
                        If clbEngineer.GetItemChecked(x) = True Then
                            clbEngineer.SelectedIndex = x
                            SQLLine = SQLLine & " (numUserID = '" & clbEngineer.SelectedValue & "' " & _
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
                        SubSQL = "select numUserId " & _
                        "from AIRBRANCH.EPDUserProfiles " & _
                        "where numUnit = '" & temp & "' and numProgram = '4' "
                        cmd = New OracleCommand(SubSQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            SQLLine = SQLLine & " (numUserID = '" & dr.Item("numUserID") & "' " & _
                            "or NUMSSCPENGINEER = '" & dr.Item("numUserID") & "') Or "
                        End While
                        dr.Close()

                    End If
                Next

                For x As Integer = 0 To clbDistrictOffices.Items.Count - 1
                    If clbDistrictOffices.GetItemChecked(x) = True Then
                        clbDistrictOffices.SelectedIndex = x
                        temp = clbDistrictOffices.SelectedValue

                        SubSQL = "select numUserId " & _
                        "from AIRBRANCH.EPDUserProfiles " & _
                        "where numProgram = '" & temp & "' and numBranch = '5' "
                        cmd = New OracleCommand(SubSQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            SQLLine = SQLLine & " (numUserID = '" & dr.Item("numUserID") & "' " & _
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
            daWorkEntry = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub AddNewEvent()
        Try
            If rdbEnforcementAction.Checked = True Then
                Dim parameters As New Dictionary(Of String, String)
                parameters("airsnumber") = txtNewAIRSNumber.Text
                OpenMultiForm("SscpEnforcement", -1, parameters)
            ElseIf rdbFCE.Checked = True Then
                OpenFormFce(New Apb.ApbFacilityId(txtNewAIRSNumber.Text))
            ElseIf rdbPerformanceTest.Checked = True Then
                If txtTrackingNumber.Text <> "" Then
                    Dim RefNum As String = ""

                    SQL = "Select " & _
                    "AIRBRANCH.ISMPReportInformation.strReferenceNumber, " & _
                    "AIRBRANCH.ISMPDocumentType.strDocumentType " & _
                    "from AIRBRANCH.ISMPReportInformation, " & _
                    "AIRBRANCH.ISMPDocumentType " & _
                    "where AIRBRANCH.ISMPReportInformation.strDocumentType = AIRBRANCH.ISMPDocumentType.strKEy " & _
                    "and strReferenceNumber = '" & txtTrackingNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                        If DAL.Ismp.StackTestExists(RefNum) Then OpenMultiForm("ISMPTestReports", RefNum)
                    Else
                        MsgBox("The Reference Number is not valid." & vbCrLf & _
                        "Please check the number you entered.", MsgBoxStyle.Information, "SSCP Compliance Log")
                    End If
                End If
            ElseIf Me.rdbOther.Checked = True Then
                If Me.cboEvent.SelectedIndex > 0 Then
                    SQL = "Insert into AIRBRANCH.SSCPItemMaster " & _
                    "(strTrackingNumber, strAIRSNumber, " & _
                    "datReceivedDate, strEventType, " & _
                    "strResponsibleStaff, datCompleteDate, " & _
                    "strModifingPerson, datModifingDate) " & _
                    "values " & _
                    "(AIRBRANCH.SSCPTrackingNumber.nextval, '0413" & txtNewAIRSNumber.Text & "', " & _
                    "'" & DTPDateReceived.Text & "', " & _
                    "'" & cboEvent.SelectedValue & "', " & _
                    "'" & UserGCode & "', '', " & _
                    "'" & UserGCode & "', '" & OracleDate & "')"

                    If cboEvent.SelectedValue = "04" Then
                        SQL2 = "Insert into AIRBRANCH.SSCPItemMaster " & _
                        "(strTrackingNumber, strAIRSNumber, " & _
                        "datReceivedDate, strEventType, " & _
                        "strResponsibleStaff, datCompleteDate, " & _
                        "strModifingPerson, datModifingDate) " & _
                        "values " & _
                        "(AIRBRANCH.SSCPTrackingNumber.nextval, '0413" & txtNewAIRSNumber.Text & "', " & _
                        "'" & DTPDateReceived.Text & "', " & _
                        "'06', " & _
                        "'" & UserGCode & "', '" & DTPDateReceived.Text & "', " & _
                        "'" & UserGCode & "', '" & OracleDate & "')"
                    Else
                        SQL2 = ""
                    End If

                    SQL3 = "Select AIRBRANCH.SSCPTrackingNumber.Currval from Dual"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    cmd3 = New OracleCommand(SQL3, CurrentConnection)

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub DeleteWork()
        Try


            Dim DeleteStatus As String = ""
            Dim tempAIRS As String = ""

            Select Case txtTestType.Text
                Case "Annual Compliance Certification"
                    SQL = "Select strTrackingNumber " & _
                    "from AIRBRANCH.SSCPItemMaster " & _
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?", _
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                                "strDelete = 'True' " & _
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
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

                    DeleteStatus = MessageBox.Show("Should this Enforcement Item be deleted? (This cannot be undone!)", _
                                     "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    If DeleteStatus = DialogResult.Yes Then

                        SQL = "Delete AIRBRANCH.SSCPEnforcementLetter " & _
                        "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete AIRBRANCH.SSCPEnforcementStipulated " & _
                        "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete AIRBRANCH.SSCP_AuditedEnforcement " & _
                        "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Select strUpDateStatus " & _
                    "from AIRBRANCH.AFSSSCPEnforcementRecords " & _
                    "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                        MsgBox("This Enforcement Action has already been submitted to EPD, contact DMU to delete this Enforcement Action.", _
                               MsgBoxStyle.Information, "Compliance Log")
                    Else
                        DeleteStatus = MessageBox.Show("Should this Enforcement Item be deleted? (This cannot be undone!)", _
                                     "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Select strAIRSNumber " & _
                                "from AIRBRANCH.SSCP_AuditedEnforcement " & _
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
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

                                SQL = "Delete AIRBRANCH.AFSSSCPEnforcementRecords " & _
                                                      "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                                "values " & _
                                "(" & _
                                "(select " & _
                                "case when max(numCounter) is null then 1 " & _
                                "else max(numCounter) + 1 " & _
                                "end numCounter " & _
                                "from AIRBRANCH.AFSDeletions), " & _
                                "'" & tempAIRS & "', " & _
                                "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                                "'" & OracleDate & "', '', " & _
                                "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete AIRBRANCH.SSCPEnforcementLetter " & _
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()



                                SQL = "Delete AIRBRANCH.SSCPEnforcementStipulated " & _
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete AIRBRANCH.SSCP_AuditedEnforcement " & _
                                "where strEnforcementNumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
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
                    SQL = "Select strUpDateStatus " & _
                    "from AIRBRANCH.AFSSSCPFCERecords " & _
                    "where strFceNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                        MsgBox("This FCE has already been submitted to EPA, contact DMU to delete this FCE.", MsgBoxStyle.Information, "Compliance Log")
                    Else

                        DeleteStatus = MessageBox.Show("Should this FCE be deleted? (This cannot be undone!)", _
                                     "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Select strAIRSNumber " & _
                                "from AIRBRANCH.SSCPItemMaster " & _
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
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


                                SQL = "Delete AIRBRANCH.AFSSSCPFCERecords " & _
                            "where strFCENumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete AIRBRANCH.SSCPFCE " & _
                                "where strFCENumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete AIRBRANCH.SSCPFCEMaster " & _
                                "where strFCENumber = '" & txtWorkNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from AIRBRANCH.AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                                cmd = New OracleCommand(SQL2, CurrentConnection)
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
                    SQL = "Select strTrackingNumber " & _
                    "from AIRBRANCH.SSCPItemMaster " & _
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?", _
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                                "strDelete = 'True' " & _
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Select strTrackingNumber " & _
                    "from AIRBRANCH.SSCPItemMaster " & _
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?", _
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                                "strDelete = 'True' " & _
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
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

                    'SQL = "Select strTrackingNumber " & _
                    '"from AIRBRANCH.SSCPItemMaster " & _
                    '"where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    'cmd = New OracleCommand(SQL, conn)
                    'If conn.State = ConnectionState.Closed Then
                    '    conn.Open()
                    'End If
                    'dr = cmd.ExecuteReader
                    'recExist = dr.Read
                    'dr.Close()
                    'If recExist = True Then
                    '    DeleteStatus = MessageBox.Show("Should this Work Item be deleted?", _
                    '                  "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    '    Select Case DeleteStatus
                    '        Case DialogResult.Yes
                    '            SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                    '            "strDelete = 'True' " & _
                    '            "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    '            cmd = New OracleCommand(SQL, conn)
                    '            If conn.State = ConnectionState.Closed Then
                    '                conn.Open()
                    '            End If
                    '            dr = cmd.ExecuteReader
                    '            dr.Close()

                    '            LoaddgvWork()

                    '        Case DialogResult.No
                    '            SQL = ""
                    '        Case DialogResult.Cancel
                    '            SQL = ""
                    '        Case Else
                    '            SQL = ""
                    '    End Select
                    'End If
                Case "Report"
                    SQL = "Select strTrackingNumber " & _
                    "from AIRBRANCH.SSCPItemMaster " & _
                    "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        DeleteStatus = MessageBox.Show("Should this Work Item be deleted?", _
                                      "Work Entry Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Select Case DeleteStatus
                            Case DialogResult.Yes
                                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                                "strDelete = 'True' " & _
                                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub UnDeleteWork()
        Try

            SQL = "Select strTrackingNumber " & _
            "from AIRBRANCH.SSCPItemMaster " & _
            "where strTrackingNumber = '" & txtWorkNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                "strDelete = '' " & _
                "where strTrackingNumber = '" & txtWorkNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Sub ExportToExcel()
        dgvWork.ExportToExcel()
    End Sub

#End Region

#Region "Declarations"
    Private Sub cboEvent_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEvent.Leave
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboEvent_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEvent.SelectedValueChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub chbEnforcementDates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbFilterDates.CheckedChanged
        Try


            If chbFilterDates.Checked = True Then
                DTPFilterStart.Enabled = True
                DTPFilterEnd.Enabled = True
            Else
                DTPFilterStart.Enabled = False
                DTPFilterEnd.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRunFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunFilter.Click
        LoaddgvWork()
    End Sub
    Private Sub dgvWork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvWork.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAIRSNumber.TextChanged
        Try


            If txtAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "strFacilityCity, strCountyName " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.LookUpCountyInformation " & _
                "where substr(strAIRSNumber, 5, 3) = strCountyCode " & _
                "and strAIRSnumber = '0413" & txtAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSelectWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectWork.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtNewAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNewAIRSNumber.TextChanged
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
            Dim fac As Apb.Facilities.Facility = DAL.FacilityData.GetFacility(txtNewAIRSNumber.Text)
            txtFacilityInformation.Text = fac.LongDisplay
        Else
            txtFacilityInformation.Text = ""
        End If
    End Sub
    Private Sub rdbPerformanceTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPerformanceTest.CheckedChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub rdbOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOther.CheckedChanged
        Try


            If rdbOther.Checked = True Then
                pnlOtherEvents.Visible = True
            Else
                pnlOtherEvents.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddNewEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewEntry.Click
        Try
            If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
                AddNewEvent()
            Else
                MsgBox("Invalid AIRS Number.", MsgBoxStyle.Information, "Work Entry")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAIRSNumberFilter.Text = Value
        End Set
    End Property
    Public WriteOnly Property ValueFromFacilityLookUp2() As String
        Set(ByVal Value As String)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FacilitySearch()
        OpenFacilityLookupTool()
    End Sub
    Private Sub TBWork_EnTry_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBWork_EnTry.ButtonClick
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnDeleteWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteWork.Click
        DeleteWork()
    End Sub
    Private Sub btnUndeleteWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndeleteWork.Click
        UnDeleteWork()
    End Sub
    Private Sub txtWorkNumber_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWorkNumber.Leave
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenSummary.Click
        OpenFormFacilitySummary(txtAIRSNumber.Text)
    End Sub

#End Region

    Private Sub chbNotifications_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbNotifications.CheckedChanged
        Try
            If chbNotifications.Checked = True Then
                GBNotifications.Enabled = True
                '  GBWorkTypes.Size = New System.Drawing.Size(208, 310)
            Else
                GBNotifications.Enabled = False
                ' GBWorkTypes.Size = New System.Drawing.Size(208, 168)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbIgnoreEngineer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbIgnoreEngineer.CheckedChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbUseEngineer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUseEngineer.CheckedChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbUseUnits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUseUnits.CheckedChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Menu"
    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub mmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExport.Click
        dgvWork.ExportToExcel()
    End Sub

    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        LoadDefaultSettings()
    End Sub

    Private Sub mmiSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSearch.Click
        FacilitySearch()
    End Sub

    Private Sub mmiRunFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRunFilter.Click
        LoaddgvWork()
    End Sub
#End Region

#Region "Change Accept Button"

    Private Sub pnlFilterPanel_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlFilterPanel.Enter
        Me.AcceptButton = btnRunFilter
    End Sub

    Private Sub btnRunFilter_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunFilter.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

End Class