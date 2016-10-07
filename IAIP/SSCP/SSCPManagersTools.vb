Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Iaip.SharedData

Public Class SSCPManagersTools

#Region " TO DELETE "
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As SqlCommand
    Dim dr, dr2, dr3 As SqlDataReader
    Dim recExist As Boolean

    Dim dsStaff As DataSet

    Dim dsCMSDataSet As DataSet
    Dim daCMSDataSet As SqlDataAdapter
    Dim dsCMSWarningDataSet As DataSet
    Dim daCMSWarningDataSet As SqlDataAdapter

    Dim dsStatisticalReport As DataSet
    Dim daStatisticalReport As SqlDataAdapter
    Dim dsEnforcementPenalties As DataSet
    Dim daEnforcementPenalties As SqlDataAdapter
    Dim ds As DataSet
    Dim da As SqlDataAdapter
#End Region

#Region "Page Load subs"

    Private Sub SSCPManagersTools_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            DTPStartDate.Value = Today.AddDays(-30)
            DTPEndDate.Value = Today
            dtpEnforcementStartDate.Value = Today
            dtpEnforcementEndDate.Value = Today
            dtpEnforcementStartDate.Enabled = False
            dtpEnforcementEndDate.Enabled = False

            DTPSearchDateStart.Value = Today.AddYears(-1)
            DTPSearchDateEnd.Value = Today

            cboFilterEngineer1.Visible = False
            cboFilterEngineer2.Visible = False
            txtFacSearch1.Visible = True
            txtFacSearch2.Visible = True

            LoadComboBoxes()
            LoadStatisticsListboxes()

            LoadSelectedFacilitesGrid()

            TCNewFacilitySearch.TabPages.Remove(TPCopyYear)

            If AccountFormAccess(129, 3) = "1" Or
                (AccountFormAccess(22, 4) = "1" And AccountFormAccess(22, 3) = "0") Then
                TCNewFacilitySearch.TabPages.Add(TPCopyYear)
            End If

            If AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" Then
                btnAddToCmsUniverse.Visible = False
                btnDeleteFacilityFromCms.Visible = False
                CmsClassSelectionPanel.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadComboBoxes()
        Try
            Dim SQL As String = "SELECT DISTINCT INTYEAR
                FROM SSCPINSPECTIONSREQUIRED
                UNION SELECT YEAR(GETDATE())
                UNION SELECT YEAR(GETDATE()) + 1
                ORDER BY INTYEAR DESC"

            With cboFiscalYear
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "INTYEAR"
                .ValueMember = "INTYEAR"
            End With

            SQL = "SELECT DISTINCT INTYEAR
                FROM SSCPINSPECTIONSREQUIRED
                ORDER BY INTYEAR DESC"

            With cboExistingYears
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "INTYEAR"
                .ValueMember = "INTYEAR"
            End With

            SQL = "SELECT CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName, NUMUSERID
                FROM EPDUSERPROFILES AS p
                LEFT JOIN LOOKUPEPDUNITS AS l ON p.NUMUNIT = l.NUMUNITCODE
                WHERE NUMPROGRAM IN (4, 5) AND NUMEMPLOYEESTATUS = 1
                ORDER BY STRLASTNAME"

            With cboSSCPEngineer
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "UserName"
                .ValueMember = "NUMUSERID"
            End With

            SQL = "SELECT CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName, NUMUSERID 
                FROM EPDUSERPROFILES AS p
                LEFT JOIN LOOKUPEPDUNITS AS l ON p.NUMUNIT = l.NUMUNITCODE
                WHERE NUMPROGRAM = '4'
                UNION
                SELECT CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName, NUMUSERID
                FROM EPDUSERPROFILES AS p
                LEFT JOIN LOOKUPEPDUNITS AS l ON p.NUMUNIT = l.NUMUNITCODE
                INNER JOIN SSCPINSPECTIONSREQUIRED AS r ON r.NUMSSCPENGINEER = p.NUMUSERID
                ORDER BY UserName"

            Dim dtStaffSearch As DataTable = DB.GetDataTable(SQL)

            With cboFilterEngineer1
                .DataSource = dtStaffSearch
                .DisplayMember = "UserName"
                .ValueMember = "NUMUSERID"
            End With

            With cboFilterEngineer2
                .DataSource = dtStaffSearch
                .DisplayMember = "UserName"
                .ValueMember = "NUMUSERID"
            End With

            SQL = "SELECT STRUNITDESC, NUMUNITCODE FROM LOOKUPEPDUNITS 
                WHERE NUMPROGRAMCODE = 4 OR NUMUNITCODE 
                IN(44, 43, 42, 41, 40, 39, 38, 37)"

            Dim dtUnits As DataTable = DB.GetDataTable(SQL)

            With cboSSCPUnit2
                .DataSource = dtUnits
                .DisplayMember = "STRUNITDESC"
                .ValueMember = "NUMUNITCODE"
            End With

            With cboSSCPUnitFilter
                .DataSource = New BindingSource(dtUnits, Nothing)
                .DisplayMember = "STRUNITDESC"
                .ValueMember = "NUMUNITCODE"
            End With

            With cboSSCPUnitFilter2
                .DataSource = New BindingSource(dtUnits, Nothing)
                .DisplayMember = "STRUNITDESC"
                .ValueMember = "NUMUNITCODE"
            End With

            With cboClassFilter1.Items
                .Add("A")
                .Add("B")
                .Add("C")
                .Add("PR")
                .Add("SM")
            End With

            With cboClassFilter2.Items
                .Add("A")
                .Add("B")
                .Add("C")
                .Add("PR")
                .Add("SM")
            End With

            With cboCMSMemberFilter1.Items
                .Add("")
                .Add("A")
                .Add("S")
                .Add("M")
            End With

            With cboCMSMemberFilter2.Items
                .Add("")
                .Add("A")
                .Add("S")
                .Add("M")
            End With

            With cboCMSFrequency.Items
                .Add("")
                .Add("A")
                .Add("S")
                .Add("M")
            End With

            With cboCMSWarningFrequency.Items
                .Add("")
                .Add("A")
                .Add("S")
                .Add("M")
            End With

            With cboCountyFilter1
                .DataSource = New BindingSource(GetSharedData(SharedTable.Counties), Nothing)
                .DisplayMember = "County"
                .ValueMember = "County"
            End With

            With cboCountyFilter2
                .DataSource = New BindingSource(GetSharedData(SharedTable.Counties), Nothing)
                .DisplayMember = "County"
                .ValueMember = "County"
            End With

            With cboDistrictFilter1
                .DataSource = DAL.GetDistrictsAsLookup()
                .DisplayMember = "District"
                .ValueMember = "District"
            End With

            With cboDistrictFilter2
                .DataSource = DAL.GetDistrictsAsLookup()
                .DisplayMember = "District"
                .ValueMember = "District"
            End With

            '--- This loads the Combo Box Compliance Units/Districts

            cboComplianceUnits.Items.Add("<Select a Unit or District>")
            cboComplianceUnits.Items.Add("Administrative")
            cboComplianceUnits.Items.Add("Air Toxics")
            cboComplianceUnits.Items.Add("Chemicals/Minerals")
            cboComplianceUnits.Items.Add("VOC/Combustion")
            cboComplianceUnits.Items.Add("All Units")
            cboComplianceUnits.Items.Add("District")

            '--- This loads the Combo Box Filter Option 1 on New Search 
            cboFacSearch1.Items.Add("<Select a Filter Option>")
            cboFacSearch1.Items.Add("AIRS Number")
            cboFacSearch1.Items.Add("City")
            cboFacSearch1.Items.Add("Classification")
            cboFacSearch1.Items.Add("CMS Status")
            cboFacSearch1.Items.Add("County")
            cboFacSearch1.Items.Add("District")
            cboFacSearch1.Items.Add("District Responsible")
            cboFacSearch1.Items.Add("Engineer")
            cboFacSearch1.Items.Add("Facility Name")
            cboFacSearch1.Items.Add("Operational Status")
            cboFacSearch1.Items.Add("SIC Codes")
            cboFacSearch1.Items.Add("SSCP Unit")
            cboFacSearch1.Items.Add("Unassigned Facilities")
            cboFacSearch1.SelectedIndex = 0

            '--- This loads the Combo Box Filter Option 2 on New Search 
            cboFacSearch2.Items.Add("<Select a Filter Option>")
            cboFacSearch2.Items.Add("AIRS Number")
            cboFacSearch2.Items.Add("City")
            cboFacSearch2.Items.Add("Classification")
            cboFacSearch2.Items.Add("CMS Status")
            cboFacSearch2.Items.Add("County")
            cboFacSearch2.Items.Add("District")
            cboFacSearch2.Items.Add("District Responsible")
            cboFacSearch2.Items.Add("Engineer")
            cboFacSearch2.Items.Add("Facility Name")
            cboFacSearch2.Items.Add("Operational Status")
            cboFacSearch2.Items.Add("SIC Codes")
            cboFacSearch2.Items.Add("SSCP Unit")
            cboFacSearch2.Items.Add("Unassigned Facilities")
            cboFacSearch2.SelectedIndex = 0

            '--- This loads the operating status 1 for New Facility Search 
            cboOpStatus1.Items.Add("O")
            cboOpStatus1.Items.Add("X")
            cboOpStatus1.Items.Add("T")
            cboOpStatus1.Items.Add("C")
            cboOpStatus1.Items.Add("I")
            cboOpStatus1.Items.Add("P")

            '--- This loads the operating status 2 for New Facility Search 
            cboOpStatus2.Items.Add("O")
            cboOpStatus2.Items.Add("X")
            cboOpStatus2.Items.Add("T")
            cboOpStatus2.Items.Add("C")
            cboOpStatus2.Items.Add("I")
            cboOpStatus2.Items.Add("P")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadStatisticsListboxes()
        With clbAirToxicUnit
            .DataSource = DAL.GetStaffAsDataTableByUnit("Air Toxics", True)
            .DisplayMember = "UserName"
            .ValueMember = "UserID"
        End With

        With clbChemicalsMinerals
            .DataSource = DAL.GetStaffAsDataTableByUnit("Chemicals/Minerals", True)
            .DisplayMember = "UserName"
            .ValueMember = "UserID"
        End With

        With clbVOCCombustion
            .DataSource = DAL.GetStaffAsDataTableByUnit("VOC/Combustion", True)
            .DisplayMember = "UserName"
            .ValueMember = "UserID"
        End With

        With clbDistricts
            .DataSource = DAL.GetStaffAsDataTableByBranch(5, True)
            .DisplayMember = "UserName"
            .ValueMember = "UserID"
        End With
    End Sub

    Private Sub LoadSelectedFacilitesGrid()
        dgvSelectedFacilityList.RowHeadersVisible = False
        dgvSelectedFacilityList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSelectedFacilityList.AllowUserToResizeColumns = True
        dgvSelectedFacilityList.AllowUserToAddRows = False
        dgvSelectedFacilityList.AllowUserToDeleteRows = False
        dgvSelectedFacilityList.AllowUserToOrderColumns = True
        dgvSelectedFacilityList.AllowUserToResizeRows = True
        dgvSelectedFacilityList.ColumnHeadersHeight = "35"

        dgvSelectedFacilityList.Columns.Add("AIRSNumber", "AIRS #")
        dgvSelectedFacilityList.Columns("AIRSNumber").DisplayIndex = 0
        dgvSelectedFacilityList.Columns("AIRSNumber").Width = 75
        dgvSelectedFacilityList.Columns("AIRSNumber").Visible = True

        dgvSelectedFacilityList.Columns.Add("strFacilityName", "Facility Name")
        dgvSelectedFacilityList.Columns("strFacilityName").DisplayIndex = 1
        dgvSelectedFacilityList.Columns("strFacilityName").Width = 100
        dgvSelectedFacilityList.Columns("strFacilityName").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("SSCPEngineer", "SSCP Engineer")
        dgvSelectedFacilityList.Columns("SSCPEngineer").DisplayIndex = 2
        dgvSelectedFacilityList.Columns("SSCPEngineer").Width = 100
        dgvSelectedFacilityList.Columns("SSCPEngineer").ReadOnly = False

        dgvSelectedFacilityList.Columns.Add("strUnitDesc", "SSCP Unit")
        dgvSelectedFacilityList.Columns("strUnitDesc").DisplayIndex = 3
        dgvSelectedFacilityList.Columns("strUnitDesc").Width = 100
        dgvSelectedFacilityList.Columns("strUnitDesc").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("strDistrictResponsible", "District Source")
        dgvSelectedFacilityList.Columns("strDistrictResponsible").DisplayIndex = 4
        dgvSelectedFacilityList.Columns("strDistrictResponsible").Width = 100
        dgvSelectedFacilityList.Columns("strDistrictResponsible").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("InspectionRequired", "Inspection Required")
        dgvSelectedFacilityList.Columns("InspectionRequired").DisplayIndex = 5
        dgvSelectedFacilityList.Columns("InspectionRequired").Width = 150
        dgvSelectedFacilityList.Columns("InspectionRequired").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("LastInspection", "Last Inspection")
        dgvSelectedFacilityList.Columns("LastInspection").DisplayIndex = 6
        dgvSelectedFacilityList.Columns("LastInspection").Width = 150
        dgvSelectedFacilityList.Columns("LastInspection").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("FCERequired", "FCE Required")
        dgvSelectedFacilityList.Columns("FCERequired").DisplayIndex = 7
        dgvSelectedFacilityList.Columns("FCERequired").Width = 150
        dgvSelectedFacilityList.Columns("FCERequired").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("LastFCE", "Last FCE")
        dgvSelectedFacilityList.Columns("LastFCE").DisplayIndex = 8
        dgvSelectedFacilityList.Columns("LastFCE").Width = 150
        dgvSelectedFacilityList.Columns("LastFCE").ReadOnly = True

        dgvSelectedFacilityList.Columns.Add("strCMSStatus", "CMS Status")
        dgvSelectedFacilityList.Columns("strCMSStatus").DisplayIndex = 9
        dgvSelectedFacilityList.Columns("strCMSStatus").Width = 150
        dgvSelectedFacilityList.Columns("strCMSStatus").ReadOnly = True
    End Sub

#End Region

#Region "Subs and Functions"

    Private Sub LoadCMSUniverse()
        Dim CMSStatus As String = ""

        Try
            Select Case cboCMSFrequency.Text
                Case "A"
                    CMSStatus = " and strCMSMember = 'A' "
                Case "S"
                    CMSStatus = " and strCMSMember = 'S' "
                Case Else
                    CMSStatus = " and strCMSMember is not null "
            End Select

            SQL = "Select * " &
           "from VW_SSCP_CMSWarning " &
           "where AIRSNumber is not Null " &
           CMSStatus

            dsCMSDataSet = New DataSet

            daCMSDataSet = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daCMSDataSet.Fill(dsCMSDataSet, "CMSData")

            dgvCMSUniverse.DataSource = dsCMSDataSet
            dgvCMSUniverse.DataMember = "CMSData"

            dgvCMSUniverse.RowHeadersVisible = False
            dgvCMSUniverse.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCMSUniverse.AllowUserToResizeColumns = True
            dgvCMSUniverse.AllowUserToAddRows = False
            dgvCMSUniverse.AllowUserToDeleteRows = False
            dgvCMSUniverse.AllowUserToOrderColumns = True
            dgvCMSUniverse.AllowUserToResizeRows = True
            dgvCMSUniverse.ColumnHeadersHeight = "35"
            dgvCMSUniverse.Columns("strCMSMember").HeaderText = "CMS Class"
            dgvCMSUniverse.Columns("strCMSMember").DisplayIndex = 0
            dgvCMSUniverse.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvCMSUniverse.Columns("AIRSNumber").DisplayIndex = 1
            dgvCMSUniverse.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvCMSUniverse.Columns("strFacilityName").DisplayIndex = 2
            dgvCMSUniverse.Columns("strFacilityCity").HeaderText = "City"
            dgvCMSUniverse.Columns("strFacilityCity").DisplayIndex = 3
            dgvCMSUniverse.Columns("strCountyName").HeaderText = "County"
            dgvCMSUniverse.Columns("strCountyName").DisplayIndex = 4
            dgvCMSUniverse.Columns("strDistrictName").HeaderText = "District"
            dgvCMSUniverse.Columns("strDistrictName").DisplayIndex = 5
            dgvCMSUniverse.Columns("strOperationalStatus").HeaderText = "Operational Status"
            dgvCMSUniverse.Columns("strOperationalStatus").DisplayIndex = 6
            dgvCMSUniverse.Columns("LASTFCE").HeaderText = "Last FCE"
            dgvCMSUniverse.Columns("LASTFCE").DisplayIndex = 7

            dgvCMSUniverse.Columns("strCountyName").HeaderText = "County Code"
            dgvCMSUniverse.Columns("strCountyName").DisplayIndex = 8
            dgvCMSUniverse.Columns("STRClass").HeaderText = "Class"

            txtCMSCount.Text = dgvCMSUniverse.RowCount
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub AddFacilityToCMS()
        Dim CMSState As String = ""

        Try

            If rdbCMSClassA.Checked = True Or rdbCMSClassS.Checked = True Then
                If rdbCMSClassA.Checked = True Then
                    CMSState = "A"
                Else
                    CMSState = "S"
                End If
                SQL = "Select strAIRSNumber " &
                "from APBSupplamentalData " &
                "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update APBSupplamentalData set " &
                    "strCMSMember = '" & CMSState & "' " &
                    "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader

                End If
            Else
                MsgBox("Select a CMS status of either 'A' or 'S'.", MsgBoxStyle.Information, "SSSCP Managers Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RemoveFacilityFromCMS()
        Try

            SQL = "Select strAIRSNumber " &
                              "from APBSupplamentalData " &
                              "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Update APBSupplamentalData set " &
                "strCMSMember = '' " &
                "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RunInspectionReport()
        Dim dtEngineers As New DataTable
        dtEngineers = dsStaff.Tables("Staff")
        Dim drEngineers As DataRow()
        Dim row As DataRow
        Dim strObject As String = ""
        Dim Staff As String = ""
        Dim DateReceivedBias As String = ""
        Dim DateCompletedBias As String = ""
        Dim DateInspection As String = ""

        Dim Statement As String = ""
        Dim Line As String = "__________________________________________________________________________________________"
        Dim DateText As String = ""
        Dim StaffName As String = ""
        Dim OpenReports As String = "0"
        Dim OpenInspections As String = "0"
        Dim OpenISMPTests As String = "0"
        Dim OpenNotifications As String = "0"
        Dim OpenACCS As String = "0"
        Dim OpenTotal As String = "0"
        Dim ClosedReports As String = "0"
        Dim ClosedInspections As String = "0"
        Dim ClosedISMPTests As String = "0"
        Dim ClosedNotifications As String = "0"
        Dim ClosedACCS As String = "0"
        Dim ClosedTotal As String = "0"
        Dim StartedReports As String = "0"
        Dim StartedInspections As String = "0"
        Dim StartedISMPTests As String = "0"
        Dim StartedNotifications As String = "0"
        Dim StartedACCS As String = "0"
        Dim StartedTotal As String = "0"
        Dim InspectA As String = "0"
        Dim InspectSM As String = "0"
        Dim InspectB As String = "0"
        Dim InspectList As String = "0"

        Try


            If chbALLDates.Checked = True Then
                DateReceivedBias = "  "
                DateCompletedBias = " "
                DateInspection = " "
                DateText = "This Report is for all dates in the Database:"
            Else
                DateReceivedBias = " and datReceivedDate between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "'  "
                DateCompletedBias = " and datCompleteDate between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                DateInspection = " and datInspectionDateStart between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                DateText = "This Report is for the dates between (" & DTPStartDate.Text & ") and (" & DTPEndDate.Text & "): "
            End If

            For Each strObject In clbEngineers.CheckedItems
                drEngineers = dtEngineers.Select("UserName = '" & strObject & "'")
                StaffName = strObject

                For Each row In drEngineers
                    Staff = row("numUserID")

                    SQL = "Select " &
                  "distinct(strLastname||', '||strFirstName) as Staff,  " &
                  "case " &
                  "     when Startedreport is NUll then 0  " &
                  "     ELSE startedReport " &
                  "end as StartedReport, " &
                  "case  " &
                  "     when StartedInspection is NULL then 0  " &
                  "     Else StartedInspection " &
                  "End as StartedInspection,  " &
                  "Case  " &
                  "     when StartedISMPTest is Null then 0  " &
                  "     Else StartedISMPTest  " &
                  "end as StartedISMPTest,  " &
                  "Case  " &
                  "     when StartedACC is Null then 0  " &
                  "     Else StartedACC " &
                  "End as StartedACC, " &
                  "Case  " &
                  "     when StartedNotification is NULL then 0  " &
                  "     Else StartedNotification  " &
                  "End as StartedNotification,  " &
                  "case  " &
                  "     when ClosedReport is NUll then 0  " &
                  "     ELSE ClosedReport " &
                  "end as ClosedReport,  " &
                  "case  " &
                  "     when ClosedInspection is NULL then 0  " &
                  "     Else ClosedInspection " &
                  "End as ClosedInspection,  " &
                  "Case  " &
                  "     when ClosedISMPTest is Null then 0  " &
                  "     Else ClosedISMPTest  " &
                  "end as ClosedISMPTest,  " &
                  "Case  " &
                  "     when ClosedACC is Null then 0  " &
                  "     Else ClosedACC " &
                  "End as ClosedACC, " &
                  "Case  " &
                  "     when ClosedNotification is NULL then 0  " &
                  "     Else ClosedNotification  " &
                  "End as ClosedNotification,  " &
                  "case  " &
                  "     when OpenReport is NUll then 0  " &
                  "     ELSE OpenReport " &
                  "end as OpenReport,  " &
                  "case  " &
                  "     when OpenInspection is NULL then 0  " &
                  "     Else OpenInspection " &
                  "End as OpenInspection,  " &
                  "Case  " &
                  "     when OpenISMPTest is Null then 0  " &
                  "     Else OpenISMPTest  " &
                  "end as OpenISMPTest,  " &
                  "Case  " &
                  "     when OpenACC is Null then 0  " &
                  "     Else OpenACC " &
                  "End as OpenACC, " &
                  "Case  " &
                  "     when OpenNotification is NULL then 0  " &
                  "     Else OpenNotification  " &
                  "End as OpenNotification  " &
                  "from EPDUserProfiles, SSCPItemMaster,  " &
                  "(Select strResponsibleStaff, count(*) as StartedReport  " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '01'  " &
                  DateReceivedBias & "  " &
                  "group by strResponsibleStaff) StartedReports,  " &
                  "(Select strResponsibleStaff, count(*) as StartedInspection " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '02'  " &
                  DateReceivedBias & " " &
                  "group by strResponsibleStaff) StartedInspections,  " &
                  "(Select strResponsibleStaff, count(*) as StartedISMPTest " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '03'  " &
                  DateReceivedBias & "  " &
                  "group by strResponsibleStaff) StartedISMPTEsts,  " &
                  "(Select strResponsibleStaff, count(*) as StartedAcc " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '04'  " &
                  DateReceivedBias & "  " &
                  "group by strResponsibleStaff) StartedACCs,  " &
                  "(Select strResponsibleStaff, count(*) as StartedNotification " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '05'  " &
                  DateReceivedBias & "  " &
                  "group by strResponsibleStaff) StartedNotifications,  " &
                  "(Select strResponsibleStaff, count(*) as ClosedReport  " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '01'  " &
                  DateCompletedBias & "  " &
                  "group by strResponsibleStaff) ClosedReports,  " &
                  "(Select strResponsibleStaff, count(*) as ClosedInspection " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '02'  " &
                  DateCompletedBias & "  " &
                  "group by strResponsibleStaff) ClosedInspections,  " &
                  "(Select strResponsibleStaff, count(*) as ClosedISMPTest " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '03'  " &
                  DateCompletedBias & "  " &
                  "group by strResponsibleStaff) ClosedISMPTEsts,  " &
                  "(Select strResponsibleStaff, count(*) as ClosedAcc " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '04'  " &
                  DateCompletedBias & "  " &
                  "group by strResponsibleStaff) ClosedACCs,  " &
                  "(Select strResponsibleStaff, count(*) as ClosedNotification " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '05'  " &
                  DateCompletedBias & "  " &
                  "group by strResponsibleStaff) ClosedNotifications, " &
                  "(Select strResponsibleStaff, count(*) as OpenReport  " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '01'  " &
                  "and DatCompleteDate IS NULL  " &
                  "group by strResponsibleStaff) OpenReports,  " &
                  "(Select strResponsibleStaff, count(*) as OpenInspection " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '02'  " &
                  "and DatCompleteDate IS NULL  " &
                  "group by strResponsibleStaff) OpenInspections,  " &
                  "(Select strResponsibleStaff, count(*) as OpenISMPTest " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '03'  " &
                  "and DatCompleteDate IS NULL  " &
                  "group by strResponsibleStaff) OpenISMPTEsts,  " &
                  "(Select strResponsibleStaff, count(*) as OpenAcc " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '04'  " &
                  "and DatCompleteDate IS NULL  " &
                  "group by strResponsibleStaff) OpenACCs,  " &
                  "(Select strResponsibleStaff, count(*) as OpenNotification " &
                  "from SSCPItemMaster   " &
                  "where strEventType = '05'  " &
                  "and DatCompleteDate IS NULL  " &
                  "group by strResponsibleStaff) OpenNotifications " &
                  "where EPDUserProfiles.numUserID = SSCPItemMaster.strResponsibleStaff " &
                  "and SSCPItemMaster.strResponsibleStaff = StartedInspections.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = StartedReports.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = StartedISMPTests.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = StartedACCS.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = StartedNotifications.strResponsibleStaff (+)  " &
                  "and SSCPItemMaster.strResponsibleStaff = ClosedInspections.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = ClosedReports.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = ClosedISMPTests.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = ClosedACCS.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = ClosedNotifications.strResponsibleStaff (+)  " &
                  "and SSCPItemMaster.strResponsibleStaff = OpenInspections.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = OpenReports.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = OpenISMPTests.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = OpenACCS.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = OpenNotifications.strResponsibleStaff (+) " &
                  "and SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " &
                  "and strDelete is Null "

                    SQL2 = "Select distinct(SSCPItemMaster.strResponsibleStaff), " &
                    "Case " &
                    "	When ClassA is Null then 0  " &
                    "	else CLassA " &
                    "end ClassA,  " &
                    "Case  " &
                    "	When ClassSM is Null then 0  " &
                    "	else CLassSM " &
                    "end ClassSM,  " &
                    "Case  " &
                    "	When ClassB is Null then 0  " &
                    "	else CLassB  " &
                    "end ClassB  " &
                    "from SSCPItemMaster,  " &
                    "(select strResponsibleStaff, count(*) as ClassA  " &
                    "from APBHeaderData, SSCPItemMaster, SSCPInspections  " &
                    "where APBHeaderData.strAIRSNumber = SSCPItemMaster.strAIRSNUmber  " &
                    "and SSCPItemMaster.strTrackingNumber = SSCPInspections.strTrackingNumber  " &
                    "and strClass = 'A'  " &
                    DateInspection &
                    "group by strResponsibleStaff) ClassAs,  " &
                    "(select strResponsibleStaff, count(*) as ClassSM  " &
                    "from APBHeaderData, SSCPItemMaster, SSCPInspections  " &
                    "where APBHeaderData.strAIRSNumber = SSCPItemMaster.strAIRSNUmber  " &
                    "and SSCPItemMaster.strTrackingNumber = SSCPInspections.strTrackingNumber  " &
                    "and strClass = 'SM'  " &
                    DateInspection &
                    "group by strResponsibleStaff) ClassSMs,  " &
                    "(select strResponsibleStaff, count(*) as ClassB  " &
                    "from APBHeaderData, SSCPItemMaster, SSCPInspections  " &
                    "where APBHeaderData.strAIRSNumber = SSCPItemMaster.strAIRSNUmber  " &
                    "and SSCPItemMaster.strTrackingNumber = SSCPInspections.strTrackingNumber  " &
                    "and strClass = 'B'  " &
                    DateInspection &
                    "group by strResponsibleStaff) ClassBs " &
                    "where " &
                    "SSCPItemMaster.strResponsibleStaff = ClassAs.strResponsibleStaff (+)  " &
                    "and SSCPItemMaster.strResponsibleStaff = ClassSMs.strResponsibleStaff (+) " &
                    "and SSCPItemMaster.strResponsibleStaff = ClassBs.strResponsibleStaff (+) " &
                    "and SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " &
                    "and strDelete is Null "

                    SQL3 = "Select " &
                    "strFacilityName, strFacilityCity,  " &
                    "SSCPItemMaster.strTrackingNumber, datInspectionDateStart,  " &
                    "strClass  " &
                    "from APBHeaderData, APBFacilityInformation,  " &
                    "SSCPItemMaster, SSCPInspections  " &
                    "where APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIrSnumber  " &
                    "and APBFacilityInformation.strAIRSNumber = SSCPItemMaster.strAIRSNumber  " &
                    "and SSCPItemMaster.strTrackingNumber = SSCPInspections.strTrackingNumber  " &
                    DateInspection &
                    "and SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " &
                    "and strDelete is Null " &
                    "order by strClass, datInspectionDateStart "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    Try
                        dr = cmd.ExecuteReader
                        While dr.Read
                            StaffName = dr.Item("Staff")
                            ClosedReports = dr.Item("ClosedReport")
                            ClosedInspections = dr.Item("ClosedInspection")
                            ClosedISMPTests = dr.Item("ClosedISMPTest")
                            ClosedNotifications = dr.Item("ClosedNotification")
                            ClosedACCS = dr.Item("ClosedACC")
                            StartedReports = dr.Item("StartedReport")
                            StartedInspections = dr.Item("StartedInspection")
                            StartedISMPTests = dr.Item("StartedISMPTest")
                            StartedNotifications = dr.Item("StartedNotification")
                            StartedACCS = dr.Item("StartedACC")
                            OpenReports = dr.Item("OpenReport")
                            OpenInspections = dr.Item("OpenInspection")
                            OpenISMPTests = dr.Item("OpenISMPTest")
                            OpenNotifications = dr.Item("OpenNotification")
                            OpenACCS = dr.Item("OpenACC")
                        End While
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try

                    cmd2 = New SqlCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        InspectA = dr2.Item("ClassA")
                        InspectSM = dr2.Item("ClassSM")
                        InspectB = dr2.Item("ClassB")
                    End While

                    cmd3 = New SqlCommand(SQL3, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr3 = cmd3.ExecuteReader
                    InspectList = ""

                    While dr3.Read
                        InspectList = InspectList & dr3.Item("strFacilityName") & ", " & dr3.Item("strFacilityCity") & vbCrLf &
                        vbTab & vbTab & vbTab & vbTab & vbTab & vbTab &
                        Format(dr3.Item("datInspectionDateStart"), "dd-MMM-yyyy") & vbTab &
                        dr3.Item("strClass") & vbTab & vbTab & dr3.Item("strtrackingNumber") & vbCrLf

                    End While

                    OpenTotal = CStr(CInt(OpenReports) + CInt(OpenInspections) + CInt(OpenISMPTests) + CInt(OpenNotifications) + CInt(OpenACCS))
                    ClosedTotal = CStr(CInt(ClosedReports) + CInt(ClosedInspections) + CInt(ClosedISMPTests) + CInt(ClosedNotifications) + CInt(ClosedACCS))
                    StartedTotal = CStr(CInt(StartedReports) + CInt(StartedInspections) + CInt(StartedISMPTests) + CInt(StartedNotifications) + CInt(StartedACCS))

                    Statement = Statement & "For the staff member(s): " & StaffName & vbCrLf &
                    vbTab & DateText & vbCrLf & vbCrLf & "I. Event(s):" & vbCrLf &
                    vbTab & vbTab & vbTab & vbTab & "Started" & vbTab & vbTab & "Closed" & vbTab & vbTab & "Currently Open" &
                    vbCrLf & "Report(s)" & vbTab & vbTab & vbTab & vbTab & StartedReports & vbTab & vbTab & ClosedReports & vbTab & vbTab & OpenReports &
                    vbCrLf & "Inspection(s)" & vbTab & vbTab & vbTab & StartedInspections & vbTab & vbTab & ClosedInspections & vbTab & vbTab & OpenInspections &
                    vbCrLf & "Notification(s)" & vbTab & vbTab & vbTab & StartedNotifications & vbTab & vbTab & ClosedNotifications & vbTab & vbTab & OpenNotifications &
                    vbCrLf & "Performance Test(s)" & vbTab & vbTab & vbTab & StartedISMPTests & vbTab & vbTab & ClosedISMPTests & vbTab & vbTab & OpenISMPTests &
                    vbCrLf & "ACC(s)" & vbTab & vbTab & vbTab & vbTab & StartedACCS & vbTab & vbTab & ClosedACCS & vbTab & vbTab & OpenACCS &
                    vbCrLf & Line & vbCrLf &
                    "Total(s)" & vbTab & vbTab & vbTab & vbTab & StartedTotal & vbTab & vbTab & ClosedTotal & vbTab & vbTab & OpenTotal &
                    vbCrLf & vbCrLf & "II. Inspection(s):" & vbCrLf &
                    vbTab & InspectA & " A Source(s)" &
                    vbCrLf & vbTab & InspectSM & " SM Source(s)" &
                    vbCrLf & vbTab & InspectB & " B Source(s)" &
                    vbCrLf & vbCrLf & "Company, City" & vbTab & vbTab & vbTab & vbTab & vbTab &
                    "Date" & vbTab & vbTab & "Class" & vbTab & vbTab & "Tracking #" & vbCrLf & Line & vbCrLf &
                    InspectList
                Next
            Next

            rtbInspectionReport.Clear()

            rtbInspectionReport.Text = Statement

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RunCMSWarningReport()
        'Dim SQLLine As String
        'Dim SQLline2 As String
        'Dim StartDate As String
        'Dim EndDate As String
        Dim CMSStatus As String
        Dim FCEStatus As String
        Dim StartCMSA As String
        Dim EndCMSA As String
        Dim StartCMSS As String
        Dim EndCMSS As String

        Try


            Select Case cboCMSWarningFrequency.Text
                Case "A"
                    CMSStatus = " and strCMSMember = 'A' "
                Case "S"
                    CMSStatus = " and strCMSMember = 'S' "
                Case Else
                    CMSStatus = " and strCMSMember is not null "
            End Select
            FCEStatus = " "
            If chbNoFCE.Checked = True Then
                FCEStatus = " and lastFCE is null "
            Else
                '  FCEStatus = " and lastFCE is not null "
                If rdbNext60Days.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(Today.AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-610), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(Today.AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(Today.AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-610), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(Today.AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-610), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbNext90Days.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(Today.AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-550), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(Today.AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(Today.AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-550), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(Today.AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-550), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbNext120Days.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(Today.AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-490), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(Today.AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(Today.AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-490), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(Today.AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-490), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbNextYear.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(Today.AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-0), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(Today.AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(Today.AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-0), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(Today.AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-0), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbFCEOverdue.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(Today.AddDays(-730), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  < '" & StartCMSA & "' "
                        Case "S"
                            StartCMSS = Format(Today.AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  < '" & StartCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(Today.AddDays(-730), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "
                        Case Else
                            StartCMSA = Format(Today.AddDays(-730), "yyyy-MM-dd")
                            StartCMSS = Format(Today.AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " &
                          "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "
                    End Select
                End If

                If rdbFCEPerformedWithinYear.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(Today, "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(Today, "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(Today, "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-365), "yyyy-MM-dd")
                            StartCMSS = Format(Today, "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(Today, "yyyy-MM-dd")
                            EndCMSA = Format(Today.AddDays(-365), "yyyy-MM-dd")
                            StartCMSS = Format(Today, "yyyy-MM-dd")
                            EndCMSS = Format(Today.AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
            End If


            SQL = "Select * " &
            "from VW_SSCP_CMSWarning " &
            "where AIRSNumber is not Null " &
            FCEStatus & CMSStatus

            If SQL <> "" Then
                dsCMSWarningDataSet = New DataSet
                daCMSWarningDataSet = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daCMSWarningDataSet.Fill(dsCMSWarningDataSet, "CMSWarning")

                dgvCMSWarning.DataSource = dsCMSWarningDataSet
                dgvCMSWarning.DataMember = "CMSWarning"

                dgvCMSWarning.RowHeadersVisible = False
                dgvCMSWarning.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvCMSWarning.AllowUserToResizeColumns = True
                dgvCMSWarning.AllowUserToAddRows = False
                dgvCMSWarning.AllowUserToDeleteRows = False
                dgvCMSWarning.AllowUserToOrderColumns = True
                dgvCMSWarning.AllowUserToResizeRows = True

                dgvCMSWarning.Columns("strCMSMember").HeaderText = "CMS Class"
                dgvCMSWarning.Columns("strCMSMember").DisplayIndex = 0
                dgvCMSWarning.Columns("AIRSNumber").HeaderText = "AIRS Number"
                dgvCMSWarning.Columns("AIRSNumber").DisplayIndex = 1
                dgvCMSWarning.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvCMSWarning.Columns("strFacilityName").DisplayIndex = 2
                dgvCMSWarning.Columns("strFacilityCity").HeaderText = "City"
                dgvCMSWarning.Columns("strFacilityCity").DisplayIndex = 3
                dgvCMSWarning.Columns("strCountyName").HeaderText = "County"
                dgvCMSWarning.Columns("strCountyName").DisplayIndex = 4
                dgvCMSWarning.Columns("strDistrictName").HeaderText = "District"
                dgvCMSWarning.Columns("strDistrictName").DisplayIndex = 5
                dgvCMSWarning.Columns("strOperationalStatus").HeaderText = "Operational Status"
                dgvCMSWarning.Columns("strOperationalStatus").DisplayIndex = 6
                dgvCMSWarning.Columns("LastFCE").HeaderText = "Last FCE"
                dgvCMSWarning.Columns("LastFCE").DisplayIndex = 7
                dgvCMSWarning.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvCMSWarning.Columns("strClass").HeaderText = "Class"
                dgvCMSWarning.Columns("strClass").DisplayIndex = 8

                txtCMSWarningCount.Text = dgvCMSWarning.RowCount
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RunACCStats()
        Try
            Dim EngineerList As String = ""
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPEngineer = '" & clbAirToxicUnit.SelectedValue & "' Or "
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPEngineer = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPEngineer = '" & clbVOCCombustion.SelectedValue & "' Or "
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If EngineerList.Length > 3 Then
                EngineerList = Mid(EngineerList, 1, (EngineerList.Length - 3))
                EngineerList = "Where " & EngineerList
            Else
                EngineerList = ""
            End If

            '---Total Facilities assigned to Unit
            SQL = "select " &
            "count(*) as TotalFacilities " &
            "from " &
            "(select " &
            "max(intYear), strAIRSNumber " &
            "from SSCPInspectionsRequired " &
            EngineerList &
            "group by strAIRSNumber) "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtTotalFacilities.Text = dr.Item("TotalFacilities")
            End While
            dr.Close()

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Total Facilities reporting ACC's
            SQL = "select count(*) as TotalACCs " &
            "from SSCPItemMaster " &
            "where strEventType = '04' " &
            "and datReceivedDate between '" & Me.DTPSearchDateStart.Text & "' and '" & Me.DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtFacilitiesReporting.Text = dr.Item("TotalACCs")
            End While
            dr.Close()

            '---Requiring resubmittals
            SQL = "select " &
            "count(*) as TotalRequiringResubmittals  " &
            "from SSCPACCs, SSCPItemMaster " &
            "where SSCPACCs.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strSubmittalNumber = '2'  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtResubmittals.Text = dr.Item("TotalRequiringResubmittals")
            End While
            dr.Close()

            '---Submitted Late
            SQL = "select " &
            "count(*) as SubmittedLate " &
            "from SSCPACCs, SSCPItemMaster " &
            "where SSCPACCs.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strPostMarkedOnTime = 'False' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtSubmittedLate.Text = dr.Item("SubmittedLate")
            End While
            dr.Close()

            '---Devations Reported in first Submittal
            SQL = "select " &
            "count(*) as DeviationsReported " &
            "from SSCPACCs, SSCPItemMaster " &
            "where SSCPACCs.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strSubmittalNumber = '1'  " &
            "and strReportedDeviations = 'True' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsReportedInOrigional.Text = dr.Item("DeviationsReported")
            End While
            dr.Close()

            '---No Deviations Reported in first Submittal
            '   ---Correctly 
            SQL = "select  " &
            "count(*) as DeviationsCorrect " &
            "from SSCPACCsHistory, SSCPItemMaster " &
            "where SSCPACCsHistory.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strSubmittalNumber = '1'  " &
            "and strReportedDeviations = 'False' " &
            "and strDeviationsUnReported = 'False' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsCorrectlyReported.Text = dr.Item("DeviationsCorrect")
            End While
            dr.Close()

            '   ---Incorrectly
            SQL = "select " &
            "count(*) as DeviationsIncorrect " &
            "from SSCPACCsHistory, SSCPItemMaster " &
            "where SSCPACCsHistory.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strSubmittalNumber = '1'  " &
            "and strReportedDeviations = 'False' " &
            "and strDeviationsUnReported = 'True' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsIncorrectlyReported.Text = dr.Item("DeviationsIncorrect")
            End While
            dr.Close()

            '---Deviations Reported in Final Report 
            SQL = "select " &
            "count(*) as DeviationsInFinal " &
            "from SSCPACCs, SSCPItemMaster " &
            "where SSCPACCs.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strReportedDeviations = 'True' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsReportedInFinal.Text = dr.Item("DeviationsInFinal")
            End While
            dr.Close()

            '---Deviations Not Previously Report
            SQL = "select  " &
            "count(*) as DeviationsNotReported " &
            "from SSCPACCs, SSCPItemMaster " &
            "where SSCPACCs.strTrackingNumber = SSCPItemMaster.strTrackingNumber " &
            "and strEventType = '04' " &
            "and strDeviationsUnReported = 'True' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtDeviationsNotPreviouslyReported.Text = dr.Item("DeviationsNotReported")
            End While
            dr.Close()

            '---Enforcement Action Taken 
            SQL = "select count(*) as EnforcementTaken " &
            "from SSCP_AuditedEnforcement, SSCPItemMaster  " &
            "where SSCP_AuditedEnforcement.strTrackingNumber = SSCPItemMaster.strTrackingNumber  " &
            "and strEventType = '04'  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtEnforcementActionTaken.Text = dr.Item("EnforcementTaken")
            End While
            dr.Close()

            '    ---LON
            SQL = "select count(*) as LONTaken  " &
            "from SSCP_AuditedEnforcement, SSCPItemMaster  " &
             "where SSCP_AuditedEnforcement.strTrackingNumber = SSCPItemMaster.strTrackingNumber  " &
            "and strEventType = '04'  " &
            "and datLONSent is Not Null  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtLONTaken.Text = dr.Item("LONTaken")
            End While
            dr.Close()

            '---NOV 
            SQL = "select count(*) as NOVTaken " &
            "from SSCP_AuditedEnforcement, SSCPItemMaster " &
             "where SSCP_AuditedEnforcement.strTrackingNumber = SSCPItemMaster.strTrackingNumber  " &
           "and strEventType = '04'  " &
            "and datNFALetterSent is Not Null  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNOVTaken.Text = dr.Item("NOVTaken")
            End While
            dr.Close()

            '---CO 
            SQL = "select count(*) as COTaken " &
            "from SSCP_AuditedEnforcement, SSCPItemMaster  " &
            "where SSCP_AuditedEnforcement.strTrackingNumber = SSCPItemMaster.strTrackingNumber  " &
            "and strEventType = '04'  " &
            "and datCOResolved is Not Null  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtCOTaken.Text = dr.Item("COTaken")
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Buttons"

    Private Sub llbViewCMSUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewCMSUniverse.LinkClicked
        LoadCMSUniverse()
    End Sub

    Private Sub llbCMSOpenFacilitySummary_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary.LinkClicked
        OpenFormFacilitySummary(txtCMSAIRSNumber.Text)
    End Sub

    Private Sub llbCMSOpenFacilitySummary2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary2.LinkClicked
        OpenFormFacilitySummary(txtCMSAIRSNumber2.Text)
    End Sub

    Private Sub btnAddToCmsUniverse_LinkClicked(sender As Object, e As EventArgs) Handles btnAddToCmsUniverse.Click
        AddFacilityToCMS()
    End Sub

    Private Sub btnDeleteFacilityFromCms_Click(sender As Object, e As EventArgs) Handles btnDeleteFacilityFromCms.Click
        RemoveFacilityFromCMS()
    End Sub

    Private Sub lblRunInspectionReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblRunInspectionReport.LinkClicked
        RunInspectionReport()
    End Sub

#End Region

    Private Sub dgvCMSWarning_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvCMSWarning.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvCMSWarning.HitTest(e.X, e.Y)

            If dgvCMSWarning.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvCMSWarning(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtCMSAIRSNumber2.Text = dgvCMSWarning(0, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtCMSAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtCMSAIRSNumber.TextChanged
        Try

            If txtCMSAIRSNumber.Text.Length = 8 Then
                SQL = "select " &
               "AIRSNUMBER, STRFACILITYNAME, " &
               "STRFACILITYCITY, STRCOUNTYNAME, " &
               "STRDISTRICTNAME, STROPERATIONALSTATUS, " &
               "STRCMSMEMBER, LASTFCE, strClass, " &
               "(strLastName||', '||strFirstName) as AssignedEngineer " &
               "from " &
               "(select * " &
               "from VW_SSCP_CMSWARNING) TABLE1, " &
               "(select " &
               "max(INTYEAR), NUMSSCPENGINEER, " &
               "strairsnumber " &
               "from SSCPINSPECTIONSREQUIRED " &
               "group by NUMSSCPENGINEER, STRAIRSNUMBER)TABLE2, " &
               "EPDUSERPROFILES " &
               "where '0413'||TABLE1.AIRSNUMBER = TABLE2.STRAIRSNUMBER (+) " &
               "and Table2.numSSCPEngineer = EPDUserProfiles.numuserid (+)  " &
               "and AIRSNumber = '" & txtCMSAIRSNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    txtCMSFacilityName.Text = dr.Item("strFacilityName")
                    txtCMSOperationalStatus.Text = dr.Item("strOperationalStatus")
                    txtCMSClassification.Text = dr.Item("strClass")
                    If IsDBNull(dr.Item("strCMSMember")) Then
                        txtCMSState.Text = ""
                        txtCMSState.BackColor = Color.Tomato
                    Else
                        txtCMSState.Text = dr.Item("strCMSMember")
                        txtCMSState.BackColor = Color.LightGreen
                    End If
                    If IsDBNull(dr.Item("AssignedEngineer")) Then
                        txtCMSAssignedEngineer.Clear()
                    Else
                        If dr.Item("AssignedEngineer") = ", " Then
                            txtCMSAssignedEngineer.Clear()
                        Else
                            txtCMSAssignedEngineer.Text = dr.Item("AssignedEngineer")
                        End If
                    End If

                    If IsDBNull(dr.Item("LastFCE")) Then
                        txtCMSLastFCE.Text = "Unknown"
                    Else
                        txtCMSLastFCE.Text = dr.Item("LastFCE")
                    End If
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtCMSAIRSNumber2_TextChanged(sender As Object, e As EventArgs) Handles txtCMSAIRSNumber2.TextChanged
        Try

            If txtCMSAIRSNumber2.Text.Length = 8 Then
                SQL = "select " &
                "AIRSNUMBER, STRFACILITYNAME, " &
                "STRFACILITYCITY, STRCOUNTYNAME, " &
                "STRDISTRICTNAME, STROPERATIONALSTATUS, " &
                "STRCMSMEMBER, LASTFCE, strClass, " &
                "(strLastName||', '||strFirstName) as AssignedEngineer " &
                "from " &
                "(select * " &
                "from VW_SSCP_CMSWARNING) TABLE1, " &
                "(select " &
                "max(INTYEAR), NUMSSCPENGINEER, " &
                "strairsnumber " &
                "from SSCPINSPECTIONSREQUIRED " &
                "group by NUMSSCPENGINEER, STRAIRSNUMBER)TABLE2, " &
                "EPDUSERPROFILES " &
                "where '0413'||TABLE1.AIRSNUMBER = TABLE2.STRAIRSNUMBER (+) " &
                "and Table2.numSSCPEngineer = EPDUserProfiles.numuserid (+)  " &
                "and AIRSNumber = '" & txtCMSAIRSNumber2.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtCMSFacilityName2.Clear()
                    Else
                        txtCMSFacilityName2.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        txtCMSOperationalStatus2.Clear()
                    Else
                        txtCMSOperationalStatus2.Text = dr.Item("strOperationalStatus")
                    End If
                    If IsDBNull(dr.Item("strClass")) Then
                        txtCMSClassification2.Clear()
                    Else
                        txtCMSClassification2.Text = dr.Item("strClass")
                    End If
                    If IsDBNull(dr.Item("AssignedEngineer")) Then
                        txtCMSAssignedEngineer2.Clear()
                    Else
                        If dr.Item("AssignedEngineer") = ", " Then
                            txtCMSAssignedEngineer2.Clear()
                        Else
                            txtCMSAssignedEngineer2.Text = dr.Item("AssignedEngineer")
                        End If
                    End If
                    If IsDBNull(dr.Item("LastFCE")) Then
                        txtCMSLastFCE2.Text = "Unknown"
                    Else
                        txtCMSLastFCE2.Text = dr.Item("LastFCE")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboComplianceUnits_TextChanged(sender As Object, e As EventArgs) Handles cboComplianceUnits.SelectedIndexChanged
        Dim dtEngineers As New DataTable
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try

            If cboComplianceUnits.Items.Contains(cboComplianceUnits.Text) Then

                dtEngineers = dsStaff.Tables("Staff")

                clbEngineers.Items.Clear()
                'Chemicals/Minerals
                Select Case cboComplianceUnits.Text
                    Case "Administrative"
                        drEngineers = dtEngineers.Select("strUnitDesc is null", "UserName")
                        For Each row In drEngineers
                            clbEngineers.Items.Add(row("UserName"))
                        Next
                    Case "Air Toxics"
                        drEngineers = dtEngineers.Select("strUnitDesc = 'Air Toxics'", "UserName")
                        For Each row In drEngineers
                            clbEngineers.Items.Add(row("UserName"))
                        Next
                    Case "Chemicals/Minerals"
                        drEngineers = dtEngineers.Select("strUnitDesc = 'Chemicals/Minerals'", "UserName")
                        For Each row In drEngineers
                            clbEngineers.Items.Add(row("UserName"))
                        Next
                    Case "VOC/Combustion"
                        drEngineers = dtEngineers.Select("strUnitDesc = 'VOC/Combustion'", "UserName")
                        For Each row In drEngineers
                            clbEngineers.Items.Add(row("UserName"))
                        Next
                    Case "All Units"
                        drEngineers = dtEngineers.Select("strUnitDesc = 'Air Toxics' or strUnitDesc = 'Chemicals/Minerals' or strUnitDesc = 'VOC/Combustion' ", "UserName")
                        For Each row In drEngineers
                            clbEngineers.Items.Add(row("UserName"))
                        Next
                    Case "District"
                        drEngineers = dtEngineers.Select("strUnitDesc <> 'Air Toxics' and strUnitDesc <> 'Chemicals/Minerals' and strUnitDesc <> 'VOC/Combustion' and strUnitDesc is not null ", "UserName")
                        For Each row In drEngineers
                            clbEngineers.Items.Add(row("UserName"))
                        Next
                    Case Else

                End Select

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblCMSWarning_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCMSWarning.LinkClicked
        RunCMSWarningReport()
    End Sub

    Private Sub btnRunStatisticalReport_Click(sender As Object, e As EventArgs) Handles btnRunStatisticalReport.Click
        RunACCStats()
    End Sub

#Region "ACC Subs and Functions"

    Private Sub ViewACCTotalAssigned()
        Try
            Dim EngineerList As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPENGINEER = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPENGINEER = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPENGINEER = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If EngineerList.Length > 3 Then
                EngineerList = Mid(EngineerList, 1, (EngineerList.Length - 3))
                EngineerList = "where ( " & EngineerList & ") "
            Else
                EngineerList = ""
            End If

            '---Total Facilities assigned to Unit

            SQL = "select " &
            "SUBSTRING(TABLE1.STRAIRSNUMBER, 5,8) as AIRSNUMBER, " &
            "STRFACILITYNAME, " &
            "(strLastName||', '||strFirstName) as UserName " &
            "from " &
            "(select " &
            "max(intYear), strAIRSNumber, " &
            "numSSCPEngineer " &
            "from SSCPInspectionsRequired " &
            EngineerList &
            "group by strAIRSNumber, numSSCPEngineer) Table1, " &
            "APBFacilityInformation, EPDUserProfiles " &
            "where Table1.strAIRSNumber = APBFacilityInformation.strAIRsnumber " &
            "and Table1.numSSCPEngineer = EPDUserProfiles.numUserID "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCReporting()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Total Facilities reporting ACC's
            SQL = "select " &
                "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,  " &
                "strFacilityName,  " &
                "(strLastname||', '||strFirstName) as UserName,  " &
                "strTrackingNumber " &
                "from APBFacilityInformation, EPDUserProfiles,  " &
                "SSCPItemMaster  " &
                "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber  " &
                "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID   " &
                "and strEventType = '04'  " &
                "and datReceivedDate between '" & Me.DTPSearchDateStart.Text & "' and '" & Me.DTPSearchDateEnd.Text & "'  " &
                ResponsibleStaff &
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCRequiringResubmittal()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Requiring resubmittals
            SQL = "select " &
                "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
                "strFacilityName,   " &
                "(strLastName||', '||strFirstName) as UserName,   " &
                "SSCPItemMaster.strTrackingNumber  " &
                "from APBFacilityInformation, EPDUserProfiles,   " &
                "SSCPItemMaster, SSCPACCs    " &
                "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
                "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
                "and SSCPItemMaster.strTrackingnumber = SSCPACCs.strTrackingNumber  " &
                "and strSubmittalNumber = '2'  " &
                "and strEventType = '04'   " &
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
                ResponsibleStaff &
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCSubmittedLate()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Submitted Late
            SQL = "select " &
           "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
           "strFacilityName,   " &
           "(strLastName||', '||strFirstName) as UserName,   " &
           "SSCPItemMaster.strTrackingNumber  " &
           "from APBFacilityInformation, EPDUserProfiles,   " &
           "SSCPItemMaster, SSCPACCs    " &
           "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
           "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
           "and SSCPItemMaster.strTrackingnumber = SSCPACCs.strTrackingNumber  " &
           "and strPostMarkedOnTime = 'False' " &
           "and strEventType = '04'   " &
           "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
           ResponsibleStaff &
           "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCDeviationsReported()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If


            '---Devations Reported in first Submittal
            SQL = "select " &
                "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
                "strFacilityName,   " &
                "(strLastName||', '||strFirstName) as UserName,   " &
                "SSCPItemMaster.strTrackingNumber  " &
                "from APBFacilityInformation, EPDUserProfiles,   " &
                "SSCPItemMaster, SSCPACCs    " &
                "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
                "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
                "and SSCPItemMaster.strTrackingnumber = SSCPACCs.strTrackingNumber  " &
                "and strSubmittalNumber = '1'  " &
                "and strEventType = '04'   " &
                "and strReportedDeviations = 'True' " &
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
                ResponsibleStaff &
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCDeviationsReportedCorrectly()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---No Deviations Reported in first Submittal
            '   ---Correctly 
            SQL = "select " &
                "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
                "strFacilityName,   " &
                "(strLastName||', '||strFirstName) as UserName,   " &
                "SSCPItemMaster.strTrackingNumber  " &
                "from APBFacilityInformation, EPDUserProfiles,   " &
                "SSCPItemMaster, SSCPACCsHistory    " &
                "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
                "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
                "and SSCPItemMaster.strTrackingnumber = SSCPACCsHistory.strTrackingNumber  " &
                "and strSubmittalNumber = '1'  " &
                "and strEventType = '04'   " &
                "and strReportedDeviations = 'False' " &
                "and strDeviationsUnReported = 'False' " &
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
                ResponsibleStaff &
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCDeviationsReportedIncorrectly()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If


            '---No Deviations Reported in first Submittal
            '   ---Incorrectly
            SQL = "select " &
                "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
                "strFacilityName,   " &
                "(strLastName||', '||strFirstName) as UserName,   " &
                "SSCPItemMaster.strTrackingNumber  " &
                "from APBFacilityInformation, EPDUserProfiles,   " &
                "SSCPItemMaster, SSCPACCsHistory    " &
                "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
                "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
                "and SSCPItemMaster.strTrackingnumber = SSCPACCsHistory.strTrackingNumber  " &
                "and strSubmittalNumber = '1'  " &
                "and strEventType = '04'   " &
                "and strReportedDeviations = 'False' " &
                "and strDeviationsUnReported = 'True' " &
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
                ResponsibleStaff &
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCDeviationsInFinal()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Deviations Reported in Final Report 
            SQL = "select " &
            "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
            "strFacilityName,   " &
            "(strLastname||', '||strFirstName) as UserName,   " &
            "SSCPItemMaster.strTrackingNumber  " &
            "from APBFacilityInformation, EPDUserProfiles,   " &
            "SSCPItemMaster, SSCPACCs    " &
            "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
            "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
            "and SSCPItemMaster.strTrackingnumber = SSCPACCs.strTrackingNumber  " &
            "and strEventType = '04' " &
            "and strReportedDeviations = 'True' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff &
            "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCDeviationsNotReported()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Deviations Not Previously Report
            SQL = "select " &
            "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
            "strFacilityName,   " &
            "(strLastName||', '||strFirstName) as UserName,   " &
            "SSCPItemMaster.strTrackingNumber  " &
            "from APBFacilityInformation, EPDUserProfiles,   " &
            "SSCPItemMaster, SSCPACCs    " &
            "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
            "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
            "and SSCPItemMaster.strTrackingnumber = SSCPACCs.strTrackingNumber  " &
            "and strEventType = '04'   " &
            "and strDeviationsUnReported = 'True' " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff &
            "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCEnforcementTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---Enforcement Action Taken 
            SQL = "select " &
                "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
                "strFacilityName,   " &
                "(strLastName||', '||strFirstName) as UserName,   " &
                "SSCPItemMaster.strTrackingNumber,  " &
                "SSCP_AuditedEnforcement.strEnforcementNumber  " &
                "from APBFacilityInformation, EPDUserProfiles,   " &
                "SSCPItemMaster, SSCPACCs,  " &
                "SSCP_AuditedEnforcement   " &
                "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
                "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
                "and SSCPItemMaster.strTrackingnumber = SSCPACCs.strTrackingNumber  " &
                "and SSCPItemMaster.strTrackingNumber = SSCP_AuditedEnforcement.strTrackingNumber  " &
                "and strEventType = '04'   " &
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
                ResponsibleStaff &
                "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3
            dgvStatisticalReports.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
            dgvStatisticalReports.Columns("strEnforcementNumber").DisplayIndex = 4

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCCOTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---CO 
            SQL = "select " &
            "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
            "strFacilityName,   " &
            "(strLastName||', '||strFirstName) as UserName,   " &
            "SSCPItemMaster.strTrackingNumber,  " &
            "SSCP_AuditedEnforcement.strEnforcementNumber  " &
            "from APBFacilityInformation, EPDUserProfiles,   " &
            "SSCPItemMaster,  " &
            "SSCP_AuditedEnforcement   " &
            "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
            "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
            "and SSCPItemMaster.strTrackingNumber = SSCP_AuditedEnforcement.strTrackingNumber  " &
            "and strEventType = '04'   " &
            "and datCOResolved is Not Null  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff &
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCNOVTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '---NOV 
            SQL = "select " &
            "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
            "strFacilityName,   " &
            "(strLastName||', '||strFirstName) as UserName,   " &
            "SSCPItemMaster.strTrackingNumber,  " &
            "SSCP_AuditedEnforcement.strEnforcementNumber  " &
            "from APBFacilityInformation, EPDUserProfiles,   " &
            "SSCPItemMaster,  " &
            "SSCP_AuditedEnforcement   " &
            "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
            "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
             "and SSCPItemMaster.strTrackingNumber = SSCP_AuditedEnforcement.strTrackingNumber  " &
             "and strEventType = '04'   " &
             "and datNFALetterSent is Not Null  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff &
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewACCLONTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAirToxicUnit.Items.Count - 1
                If clbAirToxicUnit.GetItemChecked(x) = True Then
                    clbAirToxicUnit.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAirToxicUnit.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbChemicalsMinerals.Items.Count - 1
                If clbChemicalsMinerals.GetItemChecked(x) = True Then
                    clbChemicalsMinerals.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbChemicalsMinerals.SelectedValue & "' Or "
                End If
            Next
            For x As Integer = 0 To clbVOCCombustion.Items.Count - 1
                If clbVOCCombustion.GetItemChecked(x) = True Then
                    clbVOCCombustion.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbVOCCombustion.SelectedValue & "' Or "
                End If
            Next

            If ResponsibleStaff.Length > 3 Then
                ResponsibleStaff = Mid(ResponsibleStaff, 1, (ResponsibleStaff.Length - 3))
                ResponsibleStaff = "and (" & ResponsibleStaff & ") "
            End If

            '    ---LON
            SQL = "select " &
            "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,   " &
            "strFacilityName,   " &
            "(strLastName||', '||strFirstName) as UserName,   " &
            "SSCPItemMaster.strTrackingNumber,  " &
            "SSCP_AuditedEnforcement.strEnforcementNumber  " &
            "from APBFacilityInformation, EPDUserProfiles,   " &
            "SSCPItemMaster,   " &
            "SSCP_AuditedEnforcement   " &
            "where SSCPItemMaster.strAirsnumber = APBFacilityInformation.strAIRSnumber   " &
            "and SSCPItemMaster.strResponsibleStaff = EPDUserProfiles.numUserID    " &
            "and SSCPItemMaster.strTrackingNumber = SSCP_AuditedEnforcement.strTrackingNumber  " &
             "and strEventType = '04'   " &
            "and datLONSent is Not Null  " &
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " &
            ResponsibleStaff &
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("Username").DisplayIndex = 2
            dgvStatisticalReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatisticalReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "ACC Views"

    Private Sub llbViewACCTotalAssigned_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewACCTotalAssigned.LinkClicked
        ViewACCTotalAssigned()
    End Sub

    Private Sub llbACCReporting_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCReporting.LinkClicked
        ViewACCReporting()
    End Sub

    Private Sub llbACCRequiringResubmittal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCRequiringResubmittal.LinkClicked
        ViewACCRequiringResubmittal()
    End Sub

    Private Sub llbACCSubmittedLate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCSubmittedLate.LinkClicked
        ViewACCSubmittedLate()
    End Sub

    Private Sub llbACCDeviationsReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReported.LinkClicked
        ViewACCDeviationsReported()
    End Sub

    Private Sub llbACCDeviationsReportedCorrectly_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReportedCorrectly.LinkClicked
        ViewACCDeviationsReportedCorrectly()
    End Sub

    Private Sub llbACCDeviationsIncorrectlyReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsIncorrectlyReported.LinkClicked
        ViewACCDeviationsReportedIncorrectly()
    End Sub

    Private Sub llbACCDeviationsInFinal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsInFinal.LinkClicked
        ViewACCDeviationsInFinal()
    End Sub

    Private Sub llbACCDeviationsNotReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsNotReported.LinkClicked
        ViewACCDeviationsNotReported()
    End Sub

    Private Sub llbACCEnforcementTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCEnforcementTaken.LinkClicked
        ViewACCEnforcementTaken()
    End Sub

    Private Sub llbACCCOTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCCOTaken.LinkClicked
        ViewACCCOTaken()
    End Sub

    Private Sub llbACCNOVTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCNOVTaken.LinkClicked
        ViewACCNOVTaken()
    End Sub

    Private Sub llbACCLONTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCLONTaken.LinkClicked
        ViewACCLONTaken()
    End Sub

#End Region

    Private Sub EnforcementTotals()
        Try
            SQL = "select " &
            "CONCAT('$', CONVERT(decimal(12, 2), CoPenalty + Stipulated)) as TotalPen  " &
            "from  " &
            "(select  " &
            "sum(SSCP_AuditedEnforcement.strCOPenaltyAmount) as COPenalty " &
            "from SSCP_AuditedEnforcement  " &
            "where SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "') COPEn,  " &
            "(select  " &
            "sum(SSCPEnforcementStipulated.strStipulatedPenalty) as Stipulated " &
            "from SSCPEnforcementStipulated, SSCP_AuditedEnforcement  " &
            "where SSCP_AuditedEnforcement.strEnforcementNumber = SSCPEnforcementStipulated.strEnforcementNumber  " &
            "and SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "') StipPen "

            If chbUseEnforcementDateRange.Checked = True Then
                SQL = "select " &
                "CONCAT('$', CONVERT(decimal(12, 2), CoPenalty + Stipulated)) as TotalPen  " &
                "from  " &
                "(select  " &
                "sum(SSCP_AuditedEnforcement.strCOPenaltyAmount) as COPenalty " &
                "from SSCP_AuditedEnforcement  " &
                "where  SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' " &
                "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "') COPEn,  " &
                "(select  " &
                "sum(SSCPEnforcementStipulated.strStipulatedPenalty) as Stipulated " &
                "from SSCPEnforcementStipulated, SSCP_AuditedEnforcement  " &
                "where SSCP_AuditedEnforcement.strEnforcementNumber = SSCPEnforcementStipulated.strEnforcementNumber  " &
                 "and SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' " &
                "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "') StipPen "
            End If

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("totalPen")) Then
                    mtbEnforcementSummary.Text = ""
                Else
                    mtbEnforcementSummary.Text = dr.Item("TotalPen")
                End If
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPenaltySummary_Click(sender As Object, e As EventArgs) Handles btnPenaltySummary.Click
        If txtEnforcementAIRSNumber.Text <> "" Then
            EnforcementTotals()
        End If
    End Sub

    Private Sub llbViewEnforcements_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEnforcements.LinkClicked
        Try
            If txtEnforcementAIRSNumber.Text <> "" Then
                SQL = "select " &
                "strFacilityName, " &
                "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber, " &
                "SSCP_AuditedEnforcement.strEnforcementNumber, " &
                " CONCAT('$', CONVERT(decimal(12, 2), SSCP_AuditedEnforcement.strCOPenaltyAmount)) as COPenalty, " &
                " CONCAT('$', CONVERT(decimal(12, 2), SSCPEnforcementStipulated.strStipulatedPenalty)) as StipulatedPenalty, " &
                "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " &
                "from SSCP_AuditedEnforcement, " &
                "SSCPEnforcementStipulated, APBFacilityInformation  " &
                "where SSCP_AuditedEnforcement.strEnforcementNumber = SSCPEnforcementStipulated.strEnforcementNumber " &
                "and SSCP_AuditedEnforcement.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "

                If chbUseEnforcementDateRange.Checked = True Then
                    SQL = "select " &
                    "strFacilityName, " &
                    "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber, " &
                    "SSCP_AuditedEnforcement.strEnforcementNumber, " &
                    "CONCAT('$', CONVERT(decimal(12, 2), SSCP_AuditedEnforcement.strCOPenaltyAmount)) as COPenalty, " &
                    "CONCAT('$', CONVERT(decimal(12, 2), SSCPEnforcementStipulated.strStipulatedPenalty)) as StipulatedPenalty, " &
                    "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " &
                    "from SSCP_AuditedEnforcement, " &
                    "SSCPEnforcementStipulated, APBFacilityInformation " &
                    "where  SSCP_AuditedEnforcement.strEnforcementNumber = SSCPEnforcementStipulated.strEnforcementNumber " &
                    "and SSCP_AuditedEnforcement.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                    "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "' " &
                    "and SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "
                End If

                dsEnforcementPenalties = New DataSet
                daEnforcementPenalties = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daEnforcementPenalties.Fill(dsEnforcementPenalties, "EnforcementPenalties")
                dgvStatisticalReports.DataSource = dsEnforcementPenalties
                dgvStatisticalReports.DataMember = "EnforcementPenalties"

                dgvStatisticalReports.RowHeadersVisible = False
                dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvStatisticalReports.AllowUserToResizeColumns = True
                dgvStatisticalReports.AllowUserToAddRows = False
                dgvStatisticalReports.AllowUserToDeleteRows = False
                dgvStatisticalReports.AllowUserToOrderColumns = True
                dgvStatisticalReports.AllowUserToResizeRows = True

                dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
                dgvStatisticalReports.Columns("AIRSNumber").Width = 75
                dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
                dgvStatisticalReports.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
                dgvStatisticalReports.Columns("strEnforcementNumber").DisplayIndex = 2
                dgvStatisticalReports.Columns("COPenalty").HeaderText = "Penalty Amount"
                dgvStatisticalReports.Columns("COPenalty").DisplayIndex = 3
                dgvStatisticalReports.Columns("StipulatedPenalty").HeaderText = "Stipulated Penalty Totals"
                dgvStatisticalReports.Columns("StipulatedPenalty").DisplayIndex = 4
                dgvStatisticalReports.Columns("datDiscoveryDate").HeaderText = "Discovery Date"
                dgvStatisticalReports.Columns("datDiscoveryDate").DisplayIndex = 5

                txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbUseEnforcementDateRange_CheckedChanged(sender As Object, e As EventArgs) Handles chbUseEnforcementDateRange.CheckedChanged
        dtpEnforcementStartDate.Enabled = chbUseEnforcementDateRange.Checked
        dtpEnforcementEndDate.Enabled = chbUseEnforcementDateRange.Checked
    End Sub

    Private Sub dgvStatisticalReports_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvStatisticalReports.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvStatisticalReports.HitTest(e.X, e.Y)

        Try

            If dgvStatisticalReports.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvStatisticalReports.Columns(1).HeaderText = "AIRS #" And dgvStatisticalReports.Columns(2).HeaderText = "Enforcement #" Then
                    txtRecordNumber.Text = dgvStatisticalReports(2, hti.RowIndex).Value
                    lblStatisticalRecords.Text = "Enforcement #"
                Else
                    If dgvStatisticalReports.ColumnCount = 3 Then
                        txtRecordNumber.Text = dgvStatisticalReports(0, hti.RowIndex).Value
                        lblStatisticalRecords.Text = "AIRS #"
                    Else
                        If dgvStatisticalReports.ColumnCount = 4 Then
                            If dgvStatisticalReports.Columns(3).HeaderText = "ACC Tracking #" Then
                                txtRecordNumber.Text = dgvStatisticalReports(3, hti.RowIndex).Value
                                lblStatisticalRecords.Text = "ACC Tracking #"
                            Else
                                txtRecordNumber.Text = dgvStatisticalReports(0, hti.RowIndex).Value
                                lblStatisticalRecords.Text = "AIRS #"
                            End If
                        Else
                            If dgvStatisticalReports.ColumnCount = 5 Then
                                If hti.ColumnIndex = 3 Then
                                    txtRecordNumber.Text = dgvStatisticalReports(3, hti.RowIndex).Value
                                    lblStatisticalRecords.Text = "ACC Tracking #"
                                Else
                                    txtRecordNumber.Text = dgvStatisticalReports(4, hti.RowIndex).Value
                                    lblStatisticalRecords.Text = "Enforcement #"
                                End If
                            Else
                                txtRecordNumber.Text = dgvStatisticalReports(0, hti.RowIndex).Value
                                lblStatisticalRecords.Text = "AIRS #"
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbViewRecord_Click(sender As Object, e As EventArgs) Handles llbViewRecord.LinkClicked
        Try
            If txtRecordNumber.Text <> "" Then
                Select Case lblStatisticalRecords.Text
                    Case "AIRS #"
                        If txtRecordNumber.Text.Length = 8 Then
                            OpenFacilitySummary()
                        End If
                    Case "ACC Tracking #"
                        OpenSSCPWork()
                    Case "Enforcement #"
                        OpenEnforcement()
                    Case Else

                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenFacilitySummary()
        OpenFormFacilitySummary(txtRecordNumber.Text)
    End Sub
    Private Sub OpenEnforcement()
        OpenFormEnforcement(txtRecordNumber.Text)
    End Sub

    Private Sub OpenSSCPWork()
        OpenFormSscpWorkItem(txtRecordNumber.Text)
    End Sub

    Private Sub dgvCMSUniverse_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvCMSUniverse.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvCMSUniverse.HitTest(e.X, e.Y)

            If dgvCMSUniverse.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvCMSUniverse.Columns(0).HeaderText = "AIRS Number" Then
                    If IsDBNull(dgvCMSUniverse(0, hti.RowIndex).Value) Then
                    Else
                        txtCMSAIRSNumber.Text = dgvCMSUniverse(0, hti.RowIndex).Value
                    End If
                Else

                End If
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFacilitySearch(Location As String)
        Try
            Dim SQL As String
            Dim SqlFilter As New List(Of String)
            Dim ParamList As New List(Of SqlParameter)

            If chbIgnoreFiscalYear.Checked Then
                SQL = "SELECT SUBSTRING(v.strAIRSNumber, 5, 8) AS AIRSNumber, STRFACILITYNAME, STRFACILITYCITY, 
                    STRCMSMEMBER, STRCLASS, STROPERATIONALSTATUS, LASTINSPECTION, LASTFCE, 
                    CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS SSCPEngineer, STRUNITDESC, 
                    STRDISTRICTRESPONSIBLE, STRCOUNTYNAME
                    FROM VW_SSCP_MT_FACILITYASSIGNMENT AS v
                    LEFT JOIN SSCPINSPECTIONSREQUIRED AS r ON v.STRAIRSNUMBER = r.STRAIRSNUMBER
                    LEFT JOIN EPDUSERPROFILES AS p ON r.NUMSSCPENGINEER = p.NUMUSERID
                    LEFT JOIN LOOKUPEPDUNITS AS l ON r.NUMSSCPUNIT = l.NUMUNITCODE
                    INNER JOIN (SELECT MAX(intYear) AS MaxYear, STRAIRSNUMBER
                    FROM SSCPINSPECTIONSREQUIRED
                    GROUP BY STRAIRSNUMBER) AS MaxResults 
                    ON r.STRAIRSNUMBER = MaxResults.STRAIRSNUMBER AND r.INTYEAR = MaxResults.MaxYear"
            Else
                SQL = "SELECT SUBSTRING(v.STRAIRSNUMBER, 5, 8) AS AIRSNumber, STRFACILITYNAME, STRFACILITYCITY, 
                    STRCMSMEMBER, STRCLASS, STROPERATIONALSTATUS,
                    CASE WHEN STRINSPECTIONREQUIRED = 'True' THEN 'True' ELSE 'False' END AS InspectionRequired, LASTINSPECTION,
                    CASE WHEN strFCERequired = 'True' THEN 'True' ELSE 'False' END AS FCERequired, LASTFCE, 
                    CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS SSCPEngineer, STRUNITDESC, STRDISTRICTRESPONSIBLE, STRCOUNTYNAME
                    FROM VW_SSCP_MT_FACILITYASSIGNMENT AS v
                    LEFT JOIN SSCPINSPECTIONSREQUIRED AS r ON v.STRAIRSNUMBER = r.STRAIRSNUMBER
                    LEFT JOIN EPDUSERPROFILES AS p ON r.NUMSSCPENGINEER = p.NUMUSERID
                    LEFT JOIN LOOKUPEPDUNITS AS l ON r.NUMSSCPUNIT = l.NUMUNITCODE"

                SqlFilter.Add(" r.INTYEAR = @year ")
                ParamList.Add(New SqlParameter("@year", cboFiscalYear.Text))
            End If

            If Location = "Filter" Then

                Select Case cboFacSearch1.Text
                    Case "AIRS Number"
                        SqlFilter.Add(" v.STRAIRSNUMBER LIKE @search1 ")
                        ParamList.Add(New SqlParameter("@search1", "%" & txtFacSearch1.Text & "%"))
                    Case "City"
                        SqlFilter.Add(" STRFACILITYCITY LIKE @search1 ")
                        ParamList.Add(New SqlParameter("@search1", "%" & txtFacSearch1.Text & "%"))
                    Case "Classification"
                        SqlFilter.Add(" STRCLASS = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboClassFilter1.Text))
                    Case "CMS Status"
                        SqlFilter.Add(" STRCMSMEMBER = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboCMSMemberFilter1.Text))
                    Case "County"
                        SqlFilter.Add(" STRCOUNTYNAME = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboCountyFilter1.Text))
                    Case "District"
                        SqlFilter.Add(" STRDISTRICTNAME = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboDistrictFilter1.Text))
                    Case "District Responsible"
                        SqlFilter.Add(" STRDISTRICTRESPONSIBLE = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", rdbDistResp1True.Checked))
                    Case "Engineer"
                        SqlFilter.Add(" NUMSSCPENGINEER = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboFilterEngineer1.SelectedValue))
                    Case "Facility Name"
                        SqlFilter.Add(" STRFACILITYNAME LIKE @search1 ")
                        ParamList.Add(New SqlParameter("@search1", "%" & txtFacSearch1.Text & "%"))
                    Case "Unassigned Facilities"
                        SqlFilter.Add(" NUMSSCPENGINEER is null ")
                    Case "Operational Status"
                        SqlFilter.Add(" STROPERATIONALSTATUS = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboOpStatus1.Text))
                    Case "SIC Codes"
                        SqlFilter.Add(" STRSICCODE LIKE @search1 ")
                        ParamList.Add(New SqlParameter("@search1", "%" & txtFacSearch1.Text & "%"))
                    Case "SSCP Unit"
                        SqlFilter.Add(" STRUNITDESC = @search1 ")
                        ParamList.Add(New SqlParameter("@search1", cboSSCPUnitFilter.Text))
                End Select

                Select Case cboFacSearch2.Text
                    Case "AIRS Number"
                        SqlFilter.Add(" v.STRAIRSNUMBER LIKE @search2 ")
                        ParamList.Add(New SqlParameter("@search2", "%" & txtFacSearch2.Text & "%"))
                    Case "City"
                        SqlFilter.Add(" STRFACILITYCITY LIKE @search2 ")
                        ParamList.Add(New SqlParameter("@search2", "%" & txtFacSearch2.Text & "%"))
                    Case "Classification"
                        SqlFilter.Add(" STRCLASS = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboClassFilter2.Text))
                    Case "CMS Status"
                        SqlFilter.Add(" STRCMSMEMBER = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboCMSMemberFilter2.Text))
                    Case "County"
                        SqlFilter.Add(" STRCOUNTYNAME = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboCountyFilter2.Text))
                    Case "District"
                        SqlFilter.Add(" STRDISTRICTNAME = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboDistrictFilter2.Text))
                    Case "District Responsible"
                        SqlFilter.Add(" STRDISTRICTRESPONSIBLE = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", rdbDistResp2True.Checked))
                    Case "Engineer"
                        SqlFilter.Add(" NUMSSCPENGINEER = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboFilterEngineer2.SelectedValue))
                    Case "Facility Name"
                        SqlFilter.Add(" STRFACILITYNAME LIKE @search2 ")
                        ParamList.Add(New SqlParameter("@search2", "%" & txtFacSearch2.Text & "%"))
                    Case "Unassigned Facilities"
                        SqlFilter.Add(" NUMSSCPENGINEER is null ")
                    Case "Operational Status"
                        SqlFilter.Add(" STROPERATIONALSTATUS = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboOpStatus2.Text))
                    Case "SIC Codes"
                        SqlFilter.Add(" STRSICCODE LIKE @search2 ")
                        ParamList.Add(New SqlParameter("@search2", "%" & txtFacSearch2.Text & "%"))
                    Case "SSCP Unit"
                        SqlFilter.Add(" STRUNITDESC = @search2 ")
                        ParamList.Add(New SqlParameter("@search2", cboSSCPUnitFilter.Text))
                End Select

            Else
                Dim airslist As String() = txtManualAIRSNumber.Text.Split(New String() {vbNewLine, "\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)

                If airslist.Length > 0 Then
                    For i As Integer = 0 To airslist.Length - 1
                        airslist(i) = "0413" & airslist(i)
                    Next

                    SqlFilter.Add(" v.STRAIRSNUMBER IN (select * from @airslist) ")

                    ParamList.Add(New SqlParameter("@airslist", SqlDbType.Structured) With {
                        .Value = airslist.AsSqlDataRecord,
                        .TypeName = "dbo.StringList"
                    })
                End If
            End If

            If SqlFilter.Count > 0 Then
                SQL &= " WHERE " & String.Join(" AND ", SqlFilter)
            End If

            dgvFilteredFacilityList.DataSource = DB.GetDataTable(SQL, ParamList.ToArray)

            dgvFilteredFacilityList.RowHeadersVisible = False
            dgvFilteredFacilityList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFilteredFacilityList.AllowUserToResizeColumns = True
            dgvFilteredFacilityList.AllowUserToAddRows = False
            dgvFilteredFacilityList.AllowUserToDeleteRows = False
            dgvFilteredFacilityList.AllowUserToOrderColumns = True
            dgvFilteredFacilityList.AllowUserToResizeRows = True

            If chbIgnoreFiscalYear.Checked = True Then
                dgvFilteredFacilityList.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvFilteredFacilityList.Columns("AIRSNumber").DisplayIndex = 0
                dgvFilteredFacilityList.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvFilteredFacilityList.Columns("strFacilityName").DisplayIndex = 1
                dgvFilteredFacilityList.Columns("strFacilityCity").HeaderText = "City"
                dgvFilteredFacilityList.Columns("strFacilityCity").DisplayIndex = 2
                dgvFilteredFacilityList.Columns("strCMSMember").HeaderText = "Current CMS Status"
                dgvFilteredFacilityList.Columns("strCMSMember").DisplayIndex = 3
                dgvFilteredFacilityList.Columns("strClass").HeaderText = "Current Classification"
                dgvFilteredFacilityList.Columns("strClass").DisplayIndex = 4
                dgvFilteredFacilityList.Columns("strOperationalStatus").HeaderText = "Current Operational Status"
                dgvFilteredFacilityList.Columns("strOperationalStatus").DisplayIndex = 5
                dgvFilteredFacilityList.Columns("LastInspection").HeaderText = "Last Inspection"
                dgvFilteredFacilityList.Columns("LastInspection").DisplayIndex = 6
                dgvFilteredFacilityList.Columns("LastInspection").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvFilteredFacilityList.Columns("LastFCE").HeaderText = "Last FCE"
                dgvFilteredFacilityList.Columns("LastFCE").DisplayIndex = 7
                dgvFilteredFacilityList.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvFilteredFacilityList.Columns("SSCPEngineer").HeaderText = "SSCP Engineer"
                dgvFilteredFacilityList.Columns("SSCPEngineer").DisplayIndex = 8
                dgvFilteredFacilityList.Columns("strUnitDesc").HeaderText = "SSCP Title"
                dgvFilteredFacilityList.Columns("strUnitDesc").DisplayIndex = 9
                dgvFilteredFacilityList.Columns("strDistrictResponsible").HeaderText = "District Source"
                dgvFilteredFacilityList.Columns("strDistrictResponsible").DisplayIndex = 10
                dgvFilteredFacilityList.Columns("strCountyName").HeaderText = "County"
                dgvFilteredFacilityList.Columns("strCountyName").DisplayIndex = 11
            Else
                dgvFilteredFacilityList.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvFilteredFacilityList.Columns("AIRSNumber").DisplayIndex = 0
                dgvFilteredFacilityList.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvFilteredFacilityList.Columns("strFacilityName").DisplayIndex = 1
                dgvFilteredFacilityList.Columns("strFacilityCity").HeaderText = "City"
                dgvFilteredFacilityList.Columns("strFacilityCity").DisplayIndex = 2
                dgvFilteredFacilityList.Columns("strCMSMember").HeaderText = "Current CMS Status"
                dgvFilteredFacilityList.Columns("strCMSMember").DisplayIndex = 3
                dgvFilteredFacilityList.Columns("strClass").HeaderText = "Current Classification"
                dgvFilteredFacilityList.Columns("strClass").DisplayIndex = 4
                dgvFilteredFacilityList.Columns("strOperationalStatus").HeaderText = "Current Operational Status"
                dgvFilteredFacilityList.Columns("strOperationalStatus").DisplayIndex = 5
                dgvFilteredFacilityList.Columns("InspectionRequired").HeaderText = "Inspection Required"
                dgvFilteredFacilityList.Columns("InspectionRequired").DisplayIndex = 6
                dgvFilteredFacilityList.Columns("LastInspection").HeaderText = "Last Inspection"
                dgvFilteredFacilityList.Columns("LastInspection").DisplayIndex = 7
                dgvFilteredFacilityList.Columns("LastInspection").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvFilteredFacilityList.Columns("FCERequired").HeaderText = "FCE Required"
                dgvFilteredFacilityList.Columns("FCERequired").DisplayIndex = 8
                dgvFilteredFacilityList.Columns("LastFCE").HeaderText = "Last FCE"
                dgvFilteredFacilityList.Columns("LastFCE").DisplayIndex = 9
                dgvFilteredFacilityList.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvFilteredFacilityList.Columns("SSCPEngineer").HeaderText = "SSCP Engineer"
                dgvFilteredFacilityList.Columns("SSCPEngineer").DisplayIndex = 10
                dgvFilteredFacilityList.Columns("strUnitDesc").HeaderText = "SSCP Title"
                dgvFilteredFacilityList.Columns("strUnitDesc").DisplayIndex = 11
                dgvFilteredFacilityList.Columns("strDistrictResponsible").HeaderText = "District Source"
                dgvFilteredFacilityList.Columns("strDistrictResponsible").DisplayIndex = 12
                dgvFilteredFacilityList.Columns("strCountyName").HeaderText = "County"
                dgvFilteredFacilityList.Columns("strCountyName").DisplayIndex = 13
            End If

            lblFilteredCount.Text = "Count: " & dgvFilteredFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnFacilitySearch_Click(sender As Object, e As EventArgs) Handles btnFacilitySearch.Click
        LoadFacilitySearch("Filter")
    End Sub

    Private Sub cboFacSearch1_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFacSearch1.SelectedValueChanged
        Try
            cboFilterEngineer1.Visible = False
            txtFacSearch1.Visible = False
            pnlDistResp1.Visible = False
            cboOpStatus1.Visible = False
            cboSSCPUnitFilter.Visible = False
            cboClassFilter1.Visible = False
            cboCMSMemberFilter1.Visible = False
            cboCountyFilter1.Visible = False
            cboDistrictFilter1.Visible = False

            Select Case cboFacSearch1.Text
                Case "Classification"
                    cboClassFilter1.Visible = True
                Case "CMS Status"
                    cboCMSMemberFilter1.Visible = True
                Case "County"
                    cboCountyFilter1.Visible = True
                Case "District"
                    cboDistrictFilter1.Visible = True
                Case "District Engineer"
                    cboFilterEngineer1.Visible = True
                Case "District Responsible"
                    pnlDistResp1.Visible = True
                Case "Engineer"
                    cboFilterEngineer1.Visible = True
                Case "Operational Status"
                    cboOpStatus1.Visible = True
                Case "SSCP Unit"
                    cboSSCPUnitFilter.Visible = True
                Case "<Select a Filter Option>", "Unassigned Facilities"
                Case Else
                    txtFacSearch1.Visible = True
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboFacSearch2_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFacSearch2.SelectedValueChanged
        Try
            cboFilterEngineer2.Visible = False
            txtFacSearch2.Visible = False
            pnlDistResp2.Visible = False
            cboOpStatus2.Visible = False
            cboSSCPUnitFilter2.Visible = False
            cboClassFilter2.Visible = False
            cboCMSMemberFilter2.Visible = False
            cboCountyFilter2.Visible = False
            cboDistrictFilter2.Visible = False

            Select Case cboFacSearch2.Text
                Case "Classification"
                    cboClassFilter2.Visible = True
                Case "CMS Status"
                    cboCMSMemberFilter2.Visible = True
                Case "County"
                    cboCountyFilter2.Visible = True
                Case "District"
                    cboDistrictFilter2.Visible = True
                Case "District Engineer"
                    cboFilterEngineer2.Visible = True
                Case "District Responsible"
                    pnlDistResp2.Visible = True
                Case "Engineer"
                    cboFilterEngineer2.Visible = True
                Case "Operational Status"
                    cboOpStatus2.Visible = True
                Case "SSCP Unit"
                    cboSSCPUnitFilter2.Visible = True
                Case "<Select a Filter Option>", "Unassigned Facilities"
                Case Else
                    txtFacSearch2.Visible = True
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSelectFacility_Click(sender As Object, e As EventArgs) Handles btnSelectFacility.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvFilteredFacilityList.Rows.Count

            If i > 0 Then
                temp = dgvFilteredFacilityList(0, dgvFilteredFacilityList.CurrentRow.Index).Value
                For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                    If dgvSelectedFacilityList(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvSelectedFacilityList)
                    dgvRow.Cells(0).Value = dgvFilteredFacilityList(0, dgvFilteredFacilityList.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvFilteredFacilityList(1, dgvFilteredFacilityList.CurrentRow.Index).Value

                    If chbIgnoreFiscalYear.Checked = True Then
                        If IsDBNull(dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(2).Value = dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        If IsDBNull(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(3).Value = dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        If IsDBNull(dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(4).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        dgvRow.Cells(5).Value = ""

                        'Last Inspection Date
                        If IsDBNull(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                        End If
                        dgvRow.Cells(7).Value = ""
                        'Last FCE date
                        If IsDBNull(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                        End If
                        If IsDBNull(dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                    Else
                        If IsDBNull(dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(2).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        If IsDBNull(dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(3).Value = dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        If IsDBNull(dgvFilteredFacilityList(12, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(4).Value = dgvFilteredFacilityList(12, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        If IsDBNull(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(5).Value = dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        'Last Inspection Date
                        If IsDBNull(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                        End If
                        If IsDBNull(dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(7).Value = dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                        'Last FCE date
                        If IsDBNull(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                        End If
                        If IsDBNull(dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                        Else
                            dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value
                        End If
                    End If
                    'End If
                    dgvSelectedFacilityList.Rows.Add(dgvRow)
                End If
            Else
                MsgBox("There must be a selected facility in the data grid to the left.", MsgBoxStyle.Exclamation, Me.Text)
            End If

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSelectAllFacilities_Click(sender As Object, e As EventArgs) Handles btnSelectAllFacilities.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0

            dgvSelectedFacilityList.Rows.Clear()

            For i = 0 To dgvFilteredFacilityList.Rows.Count - 1
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSelectedFacilityList)
                dgvRow.Cells(0).Value = dgvFilteredFacilityList(0, i).Value
                dgvRow.Cells(1).Value = dgvFilteredFacilityList(1, i).Value

                If chbIgnoreFiscalYear.Checked = True Then
                    If IsDBNull(dgvFilteredFacilityList(8, i).Value) Then
                    Else
                        dgvRow.Cells(2).Value = dgvFilteredFacilityList(8, i).Value
                    End If
                    If IsDBNull(dgvFilteredFacilityList(9, i).Value) Then
                    Else
                        dgvRow.Cells(3).Value = dgvFilteredFacilityList(9, i).Value
                    End If
                    If IsDBNull(dgvFilteredFacilityList(10, i).Value) Then
                    Else
                        dgvRow.Cells(4).Value = dgvFilteredFacilityList(10, i).Value
                    End If
                    dgvRow.Cells(5).Value = ""

                    'Last Inspection Date
                    If IsDBNull(dgvFilteredFacilityList(6, i).Value) Then
                    Else
                        dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(6, i).Value), "dd-MMM-yyyy"))
                    End If
                    dgvRow.Cells(7).Value = ""
                    'Last FCE date
                    If IsDBNull(dgvFilteredFacilityList(7, i).Value) Then
                    Else
                        dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, i).Value), "dd-MMM-yyyy"))
                    End If
                    If IsDBNull(dgvFilteredFacilityList(3, i).Value) Then
                    Else
                        dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, i).Value
                    End If
                Else
                    If IsDBNull(dgvFilteredFacilityList(10, i).Value) Then
                    Else
                        dgvRow.Cells(2).Value = dgvFilteredFacilityList(10, i).Value
                    End If
                    If IsDBNull(dgvFilteredFacilityList(11, i).Value) Then
                    Else
                        dgvRow.Cells(3).Value = dgvFilteredFacilityList(11, i).Value
                    End If
                    If IsDBNull(dgvFilteredFacilityList(12, i).Value) Then
                    Else
                        dgvRow.Cells(4).Value = dgvFilteredFacilityList(12, i).Value
                    End If
                    If IsDBNull(dgvFilteredFacilityList(6, i).Value) Then
                    Else
                        dgvRow.Cells(5).Value = dgvFilteredFacilityList(6, i).Value
                    End If
                    'Last Inspection Date
                    If IsDBNull(dgvFilteredFacilityList(7, i).Value) Then
                    Else
                        dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, i).Value), "dd-MMM-yyyy"))
                    End If
                    If IsDBNull(dgvFilteredFacilityList(8, i).Value) Then
                    Else
                        dgvRow.Cells(7).Value = dgvFilteredFacilityList(8, i).Value
                    End If
                    'Last FCE date
                    If IsDBNull(dgvFilteredFacilityList(9, i).Value) Then
                    Else
                        dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(9, i).Value), "dd-MMM-yyyy"))
                    End If
                    If IsDBNull(dgvFilteredFacilityList(3, i).Value) Then
                    Else
                        dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, i).Value
                    End If
                    'End If
                End If
                dgvSelectedFacilityList.Rows.Add(dgvRow)
            Next

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnselectFacility_Click(sender As Object, e As EventArgs) Handles btnUnselectFacility.Click
        Try
            If dgvSelectedFacilityList.Rows.Count > 0 Then
                dgvSelectedFacilityList.Rows.Remove(dgvSelectedFacilityList.CurrentRow)
            End If

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnselectAllFacilities_Click(sender As Object, e As EventArgs) Handles btnUnselectAllFacilities.Click
        Try

            dgvSelectedFacilityList.Rows.Clear()

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearManualAIRSNum_Click(sender As Object, e As EventArgs) Handles btnClearManualAIRSNum.Click
        txtManualAIRSNumber.Clear()
    End Sub

    Private Sub btnFilterManualAIRSList_Click(sender As Object, e As EventArgs) Handles btnFilterManualAIRSList.Click
        LoadFacilitySearch("Manual")
    End Sub

    Private Sub btnSaveEngineerResponsibility_Click(sender As Object, e As EventArgs) Handles btnSaveEngineerResponsibility.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "No data saved.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityStaffAssignment(row.Cells(0).Value.ToString, cboFiscalYear.Text, cboSSCPEngineer.SelectedValue, CurrentUser.UserID)
                row.Cells(2).Value = cboSSCPEngineer.Text
            Next

            MsgBox("Assignments Completed", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveSSCPUnitAssignment_Click(sender As Object, e As EventArgs) Handles btnSaveSSCPUnitAssignment.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityUnitAssignment(row.Cells(0).Value.ToString, cboFiscalYear.Text, cboSSCPUnit2.SelectedValue, CurrentUser.UserID)
                row.Cells(3).Value = cboSSCPUnit2.Text
            Next

            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveDistResponsible_Click(sender As Object, e As EventArgs) Handles btnSaveDistResponsible.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim SQL As String
            Dim DistResp As String = rdbDistResponsibleTrue.Checked.ToString

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                SQL = "Select 1 " &
                    "from SSCPDistrictResponsible " &
                    "where strAIRSNumber = @airs "

                Dim p As New SqlParameter("@airs", "0413" & row.Cells(0).Value)

                If DB.ValueExists(SQL, p) Then
                    SQL = "Update SSCPDistrictResponsible set " &
                    "strDistrictResponsible = @d, " &
                    "strAssigningManager = @mgr , " &
                    "datAssigningDate =  GETDATE() " &
                    "where strAIRSNumber = @airs "
                Else
                    SQL = "Insert into SSCPDistrictResponsible " &
                    "values " &
                    "(@airs, @d, " &
                    " @mgr,  GETDATE() ) "
                End If

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@d", DistResp),
                    New SqlParameter("@mgr", CurrentUser.UserID),
                    p
                }

                DB.RunCommand(SQL, parameters)

                row.Cells(4).Value = DistResp
            Next

            MsgBox("District Responsibilities Completed", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveInspectionReq_Click(sender As Object, e As EventArgs) Handles btnSaveInspectionReq.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim SQL As String

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                If DAL.Sscp.FacilityAssignmentYearExists(row.Cells(0).Value.ToString, CInt(cboFiscalYear.Text)) Then
                    SQL = "Update SSCPInspectionsRequired set " &
                        "strInspectionRequired = @i, " &
                        "strAssigningManager = @mgr, " &
                        "datAssigningDate =  GETDATE()  " &
                        "where strAIRSNumber = @airs " &
                        "and intYear = @year "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                        "(numKey, strAIRSNumber, intYear, " &
                        "strInspectionRequired, strAssigningManager, datAssigningDate) " &
                        "values " &
                        "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                        "(@airs, @year, " &
                        " @i, @mgr,  GETDATE() ) "
                End If

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@i", rdbInspectionRequired.Checked.ToString),
                    New SqlParameter("@mgr", CurrentUser.UserID),
                    New SqlParameter("@airs", "0413" & row.Cells(0).Value),
                    New SqlParameter("@year", cboFiscalYear.Text)
                }

                DB.RunCommand(SQL, parameters)

                If rdbInspectionRequired.Checked Then
                    row.Cells(5).Value = "FY-" & Mid(cboFiscalYear.Text, 3)
                Else
                    row.Cells(5).Value = "No"
                End If
            Next

            MsgBox("Inspections Completed", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveFCEReq_Click(sender As Object, e As EventArgs) Handles btnSaveFCEReq.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                If DAL.Sscp.FacilityAssignmentYearExists(row.Cells(0).Value.ToString, CInt(cboFiscalYear.Text)) Then
                    SQL = "Update SSCPInspectionsRequired set " &
                        "strFCERequired = @i, " &
                        "strAssigningManager = @mgr, " &
                        "datAssigningDate =  GETDATE()  " &
                        "where strAIRSNumber = @airs " &
                        "and intYear = @year "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                        "(numKey, strAIRSNumber, intYear, " &
                        "strFCERequired, strAssigningManager, datAssigningDate) " &
                        "values " &
                        "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                        "(@airs, @year, " &
                        " @i, @mgr,  GETDATE() ) "
                End If

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@i", rdbFCERequired.Checked.ToString),
                    New SqlParameter("@mgr", CurrentUser.UserID),
                    New SqlParameter("@airs", "0413" & row.Cells(0).Value),
                    New SqlParameter("@year", cboFiscalYear.Text)
                }

                DB.RunCommand(SQL, parameters)

                If rdbFCERequired.Checked Then
                    row.Cells(7).Value = "FY-" & Mid(cboFiscalYear.Text, 3)
                Else
                    row.Cells(7).Value = "No"
                End If
            Next

            MsgBox("FCE Assignment Completed", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnForceAIRSNumber_Click(sender As Object, e As EventArgs) Handles btnForceAIRSNumber.Click
        Try
            If Apb.ApbFacilityId.IsValidAirsNumberFormat(mtbForcedAIRS.Text) Then
                Dim airs As New Apb.ApbFacilityId(mtbForcedAIRS.Text)

                If DAL.AirsNumberExists(airs) Then
                    Dim FacilityName As String = DAL.GetFacilityName(airs)
                    Dim exists As Boolean = False

                    For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                        If row.Cells(0).Value.ToString.Equals(mtbForcedAIRS.Text) Then
                            exists = True
                        End If
                    Next

                    If Not exists Then
                        Dim dgvRow As New DataGridViewRow

                        dgvRow.CreateCells(dgvSelectedFacilityList)
                        dgvRow.Cells(0).Value = mtbForcedAIRS.Text
                        dgvRow.Cells(1).Value = FacilityName

                        dgvSelectedFacilityList.Rows.Add(dgvRow)
                    End If

                    lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString
                Else
                    MsgBox("AIRS # does not exist in the database.", MsgBoxStyle.Exclamation, "SSCP Managers Tools")
                End If
            Else
                MsgBox("Invalid AIRS #.", MsgBoxStyle.Exclamation, "SSCP Managers Tools")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearEngineerAssignment_Click(sender As Object, e As EventArgs) Handles btnClearEngineerAssignment.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "No data saved.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityStaffAssignment(row.Cells(0).Value.ToString, cboFiscalYear.Text, Nothing, CurrentUser.UserID)
                row.Cells(2).Value = Nothing
            Next

            MsgBox("Assignments Cleared", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearSSCPUnitAssignment_Click(sender As Object, e As EventArgs) Handles btnClearSSCPUnitAssignment.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityUnitAssignment(row.Cells(0).Value.ToString, cboFiscalYear.Text, Nothing, CurrentUser.UserID)
                row.Cells(3).Value = Nothing
            Next

            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveCMS_Click(sender As Object, e As EventArgs) Handles btnSaveCMS.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim SQL As String
            Dim CMSStatus As String = ""

            If rdbCMS_A.Checked Then
                CMSStatus = "A"
            ElseIf rdbCMS_SM.Checked Then
                CMSStatus = "S"
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                SQL = "Update APBSupplamentalData set " &
                    "strCMSMember = @c " &
                    "where strAIRSNumber = @airs "

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@c", CMSStatus),
                    New SqlParameter("@airs", "0413" & row.Cells(0).Value)
                }

                DB.RunCommand(SQL, parameters)

                row.Cells(9).Value = CMSStatus
            Next

            MsgBox("CMS Status Updated", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCopyYear_Click(sender As Object, e As EventArgs) Handles btnCopyYear.Click
        Dim targetYear As Integer
        Dim oldYear As Integer

        Try

            If cboExistingYears.Text = "" Then
                MsgBox("Select an existing year from the dropdown." & vbCrLf & "No data altered", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            Else
                oldYear = CType(cboExistingYears.Text, Integer)
            End If

            If mtbNewYear.Text = "" OrElse mtbNewYear.Text.Length <> "4" OrElse Not IsNumeric(mtbNewYear.Text) Then
                MsgBox("Please enter a complete 4-digit year" & vbCrLf & "No data altered", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            Else
                targetYear = CType(mtbNewYear.Text, Integer)
            End If

            Dim confirmResult As DialogResult =
                MessageBox.Show("Warning: This may take a VERY, VERY long time. The IAIP will be unresponsive until finished. " &
                                "Are you sure you want to proceed?", "Confirm Patience",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If confirmResult = DialogResult.Cancel Then
                Exit Sub
            End If

            If DAL.Sscp.AssignmentYearExists(targetYear) Then
                If chbClearExistingData.Checked = True Then
                    Dim dialogResult As DialogResult =
                        MessageBox.Show("Warning: This will delete all facility assignments for " & mtbNewYear.Text &
                                        ". Are you sure you want to proceed?", "Confirm Delete",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    If dialogResult = DialogResult.Yes Then
                        Dim deleteResult As Boolean = DAL.Sscp.DeleteAssignmentYear(targetYear)
                        If Not deleteResult Then
                            MsgBox("There was an error when attempting to clear data from target year. " &
                                   "Please check the value and try again." & vbCrLf & "No data altered.",
                                   MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If
                    End If
                Else
                    Dim dialogResult As DialogResult =
                        MessageBox.Show("Warning: This will merge data from " & oldYear & " into " & targetYear &
                                        ". Are you sure you want to proceed?", "Confirm Merge",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    If dialogResult = DialogResult.No Then
                        Exit Sub
                    End If
                End If
            End If

            Dim recordsAdded As Integer = DAL.Sscp.CopyAssignmentYear(oldYear, targetYear)
            If recordsAdded > 0 Then
                MsgBox(recordsAdded & " new records entered into " & targetYear, MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("There was an error adding the data to the target year. " &
                       "Please check the value and try again.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunTitleVSearch_Click(sender As Object, e As EventArgs) Handles btnRunTitleVSearch.Click
        Try
            SQL = "SELECT DISTINCT SUBSTRING(FI.STRAIRSNUMBER, 5,8) AS AIRSNumber, " &
            "  FI.STRFACILITYNAME, " &
            "  HD.STROPERATIONALSTATUS, " &
            "  (UP.STRLASTNAME " &
            "  || ', ' " &
            "  || UP.STRFIRSTNAME) AS StaffResponsible " &
            "FROM APBFacilityinformation FI, " &
            "  APBHeaderdata HD, " &
            "  EPDUserProfiles UP, " &
            "  SSCPINSPECTIONSREQUIRED IR, " &
            "  (SELECT DISTINCT SUBSTRING(tFI.STRAIRSNUMBER, 5,8) AS AIRSNumber " &
            "  FROM APBHeaderData tHD, " &
            "    APBFacilityInformation tFI, " &
            "    SSPPApplicationMaster tAM, " &
            "    SSPPApplicationData tAD, " &
            "    SSPPApplicationTracking tAT " &
            "  WHERE tAM.STRAPPLICATIONNUMBER = tAD.STRAPPLICATIONNUMBER " &
            "  AND tAM.STRAIRSNUMBER          = tHD.STRAIRSNUMBER " &
            "  AND tAM.STRAIRSNUMBER          = tFI.STRAIRSNUMBER " &
            "  AND tAM.STRAPPLICATIONNUMBER   = tAT.STRAPPLICATIONNUMBER " &
            "  AND tAD.STRPERMITNUMBER LIKE '%V__0' " &
            "  AND tHD.STROPERATIONALSTATUS             <> 'X' " &
            "  AND SUBSTRING(tHD.STRAIRPROGRAMCODES, 13, 1) = '1' " &
            "  AND ((tAT.DATPERMITISSUED                IS NOT NULL " &
            "  AND tAT.DATPERMITISSUED                   < add_months(GETDATE(), -51)) " &
            "  OR (tAT.DATEFFECTIVE                     IS NOT NULL " &
            "  AND tAT.DATEFFECTIVE                      < add_months(GETDATE(), -51))) " &
            "  MINUS " &
            "    (SELECT DISTINCT SUBSTRING(tAM.STRAIRSNUMBER, 5,8) AS AIRSNumber " &
            "    FROM SSPPApplicationMaster tAM, " &
            "      SSPPApplicationData tAD, " &
            "      SSPPApplicationTracking tAT, " &
            "      APBHeaderData tHD " &
            "    WHERE tAM.STRAPPLICATIONNUMBER = tAD.STRAPPLICATIONNUMBER " &
            "    AND tAM.STRAPPLICATIONNUMBER   = tAT.STRAPPLICATIONNUMBER " &
            "    AND tAM.STRAIRSNUMBER          = tHD.STRAIRSNUMBER " &
            "    AND ((tAM.STRAPPLICATIONTYPE   = '14' " &
            "    OR tAM.STRAPPLICATIONTYPE      = '16' " &
            "    OR tAM.STRAPPLICATIONTYPE      = '27') " &
            "    OR (tAD.STRPERMITNUMBER LIKE '%V__0')) " &
            "    AND ((tAT.DATPERMITISSUED BETWEEN add_months(GETDATE(), -51) AND GETDATE() " &
            "    AND tAT.DATEFFECTIVE BETWEEN add_months(GETDATE(),      -51) AND GETDATE()) " &
            "    OR (tAT.DATRECEIVEDDATE BETWEEN add_months(GETDATE(),   -51) AND GETDATE())) " &
            "    ) " &
            "  ) TVFacilities, " &
            "  (SELECT MAX(SSCPINSPECTIONSREQUIRED.INTYEAR) AS maxyear " &
            "  FROM SSCPINSPECTIONSREQUIRED " &
            "  ) maxyear " &
            "WHERE FI.STRAIRSNUMBER = '0413' " &
            "  || TVFacilities.AIRSNumber " &
            "AND FI.STRAIRSNUMBER   = HD.STRAIRSNUMBER " &
            "AND IR.NUMSSCPENGINEER = UP.NUMUSERID " &
            "AND FI.STRAIRSNUMBER   = IR.STRAIRSNUMBER " &
            "AND IR.INTYEAR         = maxyear.maxyear " &
            "ORDER BY AIRSNumber"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = True
            dgvStatisticalReports.AllowUserToResizeRows = True

            dgvStatisticalReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatisticalReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatisticalReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatisticalReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatisticalReports.Columns("strFacilityName").Width = 150
            dgvStatisticalReports.Columns("strOperationalStatus").HeaderText = "Op. Status"
            dgvStatisticalReports.Columns("strOperationalStatus").DisplayIndex = 2
            dgvStatisticalReports.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvStatisticalReports.Columns("StaffResponsible").DisplayIndex = 3
            dgvStatisticalReports.Columns("StaffResponsible").Width = 150

            txtStatisticalCount.Text = dgvStatisticalReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtManualAIRSNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtManualAIRSNumber.KeyPress
        If e.KeyChar = ChrW(1) Then
            txtManualAIRSNumber.SelectAll()
        End If
    End Sub

#Region "Export to Excel"

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvStatisticalReports.ExportToExcel(Me)
    End Sub

    Private Sub btnExportCmsWarningToExcel_Click(sender As Object, e As EventArgs) Handles btnExportCmsWarningToExcel.Click
        dgvCMSWarning.ExportToExcel(Me)
    End Sub

    Private Sub btnExportCmsUniverseToExcel_Click(sender As Object, e As EventArgs) Handles btnExportCmsUniverseToExcel.Click
        dgvCMSUniverse.ExportToExcel(Me)
    End Sub

    Private Sub btnExportFiltered_Click(sender As Object, e As EventArgs) Handles btnExportFiltered.Click
        dgvFilteredFacilityList.ExportToExcel(Me)
    End Sub

    Private Sub btnExportSelected_Click(sender As Object, e As EventArgs) Handles btnExportSelected.Click
        dgvSelectedFacilityList.ExportToExcel(Me)
    End Sub

#End Region

#Region "Document Types"

    Private Sub TCManagerTools_Selected(sender As Object, e As TabControlEventArgs) Handles TCManagerTools.Selected
        If e.TabPage Is TPDocuments AndAlso dgvEnfDocumentTypes.RowCount = 0 Then
            LoadEnforcementDocumentTypes()
        End If
    End Sub

    Private enfDocumentTypesList As List(Of DocumentType)

    Private Sub LoadEnforcementDocumentTypes()
        ' Get list of various document types and bind that list to the datagridview
        enfDocumentTypesList = DAL.GetEnforcementDocumentTypes()

        If enfDocumentTypesList.Count > 0 Then
            With dgvEnfDocumentTypes
                .DataSource = New BindingSource(enfDocumentTypesList, Nothing)
                .Enabled = True
            End With
            FormatEnfDocTypeList()
        Else
            With dgvEnfDocumentTypes
                .DataSource = Nothing
                .Enabled = False
            End With
        End If
    End Sub

    Private Sub FormatEnfDocTypeList()
        With dgvEnfDocumentTypes
            .Columns("DocumentTypeId").Visible = False
            .Columns("Active").HeaderText = "Active"
            .Columns("Active").DisplayIndex = 0
            .Columns("Ordinal").HeaderText = "Pos."
            .Columns("Ordinal").DisplayIndex = 1
            .Columns("Ordinal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("DocumentType").HeaderText = "Name"
            .Columns("DocumentType").DisplayIndex = 2
        End With
    End Sub

    Private Sub EnableEnfDocTypeUpdate()
        EnableDisableEnfDocTypeUpdate(EnableOrDisable.Enable)
    End Sub

    Private Sub DisableEnfDocTypeUpdate()
        EnableDisableEnfDocTypeUpdate(EnableOrDisable.Disable)
    End Sub

    Private Sub EnableDisableEnfDocTypeUpdate(enable As EnableOrDisable)
        With pnlUpdateDocumentType
            .Enabled = enable
            .Visible = enable
        End With
        If enable Then
            txtUpdateName.Text = dgvEnfDocumentTypes.CurrentRow.Cells("DocumentType").Value
            mtxtUpdatePosition.Text = dgvEnfDocumentTypes.CurrentRow.Cells("Ordinal").Value
            chkUpdateActive.Checked = dgvEnfDocumentTypes.CurrentRow.Cells("Active").Value
        End If
    End Sub

    Private Sub btnAddDocumentType_Click(sender As Object, e As EventArgs) Handles btnAddDocumentType.Click
        ' Create Document object
        Dim newEnfDocType As New DocumentType
        With newEnfDocType
            .Active = True
            .DocumentType = txtNewName.Text
            If Integer.TryParse(mtxtNewPosition.Text, Nothing) Then
                .Ordinal = mtxtNewPosition.Text
            Else
                Dim max As Integer = 0
                For Each row As DataGridViewRow In dgvEnfDocumentTypes.Rows
                    max = Math.Max(row.Cells("Ordinal").Value, max)
                Next
                .Ordinal = max + 1
            End If
        End With

        DAL.SaveEnforcementDocumentType(newEnfDocType, Me)

        ClearNewEnfDocTypesForm()
        LoadEnforcementDocumentTypes()
    End Sub

    Private Sub ClearNewEnfDocTypesForm()
        txtNewName.Text = ""
        mtxtNewPosition.Text = ""
    End Sub

    Private Sub ClearUpdateEnfDocTypesForm()
        txtUpdateName.Text = ""
        mtxtUpdatePosition.Text = ""
    End Sub

    Private Sub btnUpdateDocumentType_Click(sender As Object, e As EventArgs) Handles btnUpdateDocumentType.Click
        Dim d As DocumentType = EnforcementDocumentTypeFromFileListRow(dgvEnfDocumentTypes.CurrentRow)
        With d
            .Active = chkUpdateActive.Checked
            .DocumentType = txtUpdateName.Text
            Dim ord As Integer
            If Integer.TryParse(mtxtUpdatePosition.Text, ord) Then
                .Ordinal = ord
            End If
        End With
        Dim updated As Boolean = DAL.UpdateEnforcementDocumentType(d, Me)
        If updated Then
            ClearUpdateEnfDocTypesForm()
            LoadEnforcementDocumentTypes()
        End If
    End Sub

    Private Function EnforcementDocumentTypeFromFileListRow(row As DataGridViewRow) As DocumentType
        Dim d As New DocumentType
        With d
            .Active = row.Cells("Active").Value
            .DocumentType = row.Cells("DocumentType").Value
            .DocumentTypeId = row.Cells("DocumentTypeId").Value
            .Ordinal = row.Cells("Ordinal").Value
        End With
        Return d
    End Function

    Private Sub dgvEnfDocumentTypes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEnfDocumentTypes.SelectionChanged
        If dgvEnfDocumentTypes.SelectedRows.Count = 1 Then
            EnableEnfDocTypeUpdate()
        Else
            DisableEnfDocTypeUpdate()
        End If
    End Sub

    Private Sub dgvEnfDocumentTypes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvEnfDocumentTypes.DataBindingComplete
        CType(sender, DataGridView).SanelyResizeColumns()
        CType(sender, DataGridView).ClearSelection()
    End Sub

#Region " Accept Button "

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) Handles txtNewName.Leave, txtUpdateName.Leave, mtxtNewPosition.Leave, mtxtUpdatePosition.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub NewEnfDocType_Enter(sender As Object, e As EventArgs) Handles txtNewName.Enter, mtxtNewPosition.Enter
        Me.AcceptButton = btnAddDocumentType
    End Sub

    Private Sub UpdateEnfDocType_Enter(sender As Object, e As EventArgs) Handles txtUpdateName.Enter, mtxtUpdatePosition.Enter
        Me.AcceptButton = btnUpdateDocumentType
    End Sub

#End Region

#End Region

End Class