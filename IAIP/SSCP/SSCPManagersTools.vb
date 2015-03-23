Imports Oracle.DataAccess.Client
Imports System.Data
Imports System.IO
'Imports System.Text

Public Class SSCPManagersTools
    
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim recExist As Boolean
    
    Dim dsStaff As DataSet
    Dim daStaff As OracleDataAdapter
    Dim dsAssignStaff As DataSet
    Dim daAssignStaff As OracleDataAdapter
    Dim dsUnits As DataSet
    Dim daUnits As OracleDataAdapter
    Dim dsFilterUnits As DataSet
    Dim daFilterUnits As OracleDataAdapter
    Dim dsClassFilter As DataSet
    Dim daClassFilter As OracleDataAdapter
    Dim dsCMSMemberFilter As DataSet
    Dim daCMSMemberFilter As OracleDataAdapter
    Dim dsCountyFilter As DataSet
    Dim daCountyFilter As OracleDataAdapter
    Dim dsDistrictFilter As DataSet
    Dim daDistrictFilter As OracleDataAdapter

    Dim dsAdminStaff As DataSet
    Dim daAdminStaff As OracleDataAdapter
    Dim dsAirStaff As DataSet
    Dim daAirStaff As OracleDataAdapter
    Dim dsChemStaff As DataSet
    Dim daChemStaff As OracleDataAdapter
    Dim dsVOCStaff As DataSet
    Dim daVOCStaff As OracleDataAdapter
    Dim dsDistrictStaff As DataSet
    Dim daDistrictStaff As OracleDataAdapter
    Dim dsFacilityAssignment As DataSet
    Dim daFacilityAssignment As OracleDataAdapter
    Dim dsInspectionList As DataSet
    Dim daInspectionList As OracleDataAdapter
    Dim dsCMSDataSet As DataSet
    Dim daCMSDataSet As OracleDataAdapter
    Dim dsCMSWarningDataSet As DataSet
    Dim daCMSWarningDataSet As OracleDataAdapter
    Dim dsPollutantList As DataSet
    Dim daPollutantList As OracleDataAdapter
    Dim dsStatisticalReport As DataSet
    Dim daStatisticalReport As OracleDataAdapter
    Dim dsEnforcementPenalties As DataSet
    Dim daEnforcementPenalties As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub SSCP_Managers_Tools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            LoadDataSets()
            LoadComboBoxes()

            DTPStartDate.Value = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPEndDate.Value = OracleDate
            dtpEnforcementStartDate.Value = OracleDate
            dtpEnforcementEndDate.Value = OracleDate
            dtpEnforcementStartDate.Enabled = False
            dtpEnforcementEndDate.Enabled = False

            'TCManagerTools.TabPages.Remove(TPCMSWarning)
            'TCManagerTools.TabPages.Remove(TPUniverse)
            'TCManagerTools.TabPages.Remove(TPStaffReports)
            'TCManagerTools.TabPages.Remove(TPPollutantBubbleUp)
            'TCManagerTools.TabPages.Remove(TPStatisticalPage)
            'TCManagerTools.TabPages.Remove(TPWatchList)
            'TCManagerTools.TabPages.Remove(TPFacilityAssignments)
            'TCManagerTools.TabPages.Remove(TPMiscReports)

            'TCManagerTools.TabPages.Add(TPFacilityAssignments)
            'TCManagerTools.TabPages.Add(TPStaffReports)
            'TCManagerTools.TabPages.Add(TPUniverse)
            'TCManagerTools.TabPages.Add(TPCMSWarning)
            'TCManagerTools.TabPages.Add(TPPollutantBubbleUp)
            'TCManagerTools.TabPages.Add(TPStatisticalPage)
            'TCManagerTools.TabPages.Add(TPWatchList)
            'TCManagerTools.TabPages.Add(TPMiscReports)

            LoadStatisticalLists()
            DTPSearchDateStart.Text = Format(Date.Today.AddYears(-1), "dd-MMM-yyyy")
            DTPSearchDateEnd.Text = OracleDate

            cboFilterEngineer1.Visible = False
            cboFilterEngineer2.Visible = False
            txtFacSearch1.Visible = True
            txtFacSearch2.Visible = True
            LoadSelectedFacilitesGrid()

            Panel10.Enabled = False
            Panel9.Enabled = False
            Panel15.Enabled = False

            TCNewFacilitySearch.TabPages.Remove(TPCopyYear)
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(22, 4) = "1" And AccountFormAccess(22, 3) = "0") Then
                TCNewFacilitySearch.TabPages.Add(TPCopyYear)
            End If
            If AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" Then
                btnAddToCmsUniverse.Visible = False
                btnDeleteFacilityFromCms.Visible = False
                Panel8.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#Region "Page Load subs"
    Sub LoadDataSets()
        Try

            dsStaff = New DataSet
            dsUnits = New DataSet
            dsAssignStaff = New DataSet
            dsFilterUnits = New DataSet
            dsClassFilter = New DataSet
            dsCMSMemberFilter = New DataSet
            dsCountyFilter = New DataSet
            dsDistrictFilter = New DataSet

            SQL = "Select  " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "strUnitDesc, numUserID " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.LookUpEPDUnits  " & _
            "where AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookupEPDUnits.numUnitCode (+) " & _
            "and (numProgram = '4' " & _
            "or strLastname = 'District') " & _
            "UNION " & _
            "Select  " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "strUnitDesc, numUserID " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.LookUpEPDUnits, " & _
            "AIRBRANCH.SSCPInspectionsRequired   " & _
            "where AIRBRANCH.SSCPInspectionsRequired.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
            "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookupEPDUnits.numUnitCode (+) " & _
            "group by strLastName, strFirstName, strUnitDesc, numUserID  " & _
            "order by UserName "

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If AccountFormAccess(22, 2) = "1" Then 'District Liason 
                SQL = "select " & _
                "strUnitDesc, numUnitCode " & _
                "from AIRBRANCH.LookUPEPDUnits " & _
                "where numProgramCode = '4' " & _
                "or numUnitCode = '44' " & _
                "or numUnitCode = '43' " & _
                "or numUnitCode = '42' " & _
                "or numUnitCode = '41' " & _
                "or numUnitCode = '40' " & _
                "or numUnitCode = '39' " & _
                "or numUnitCode = '38'  " & _
                "or numUnitCode = '37'  " & _
                "or numUnitCode = '36' "
            Else
                SQL = "select strUnitDesc, numUnitCode " & _
                "from AIRBRANCH.LookUpEPDUnits   " & _
                "where numProgramCode = '4' " & _
                "order by strUnitDesc  "
            End If

            daUnits = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "strUnitDesc, numUserID " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.LookUpEPDUnits  " & _
            "where AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookupEPDUnits.numUnitCode (+) " & _
            "and ((numProgram = '4' " & _
            "or strLastname = 'District') " & _
            "    or numbranch = '5') " & _
            "and numEmployeeStatus = '1'  " & _
            "order by strLastName  "
            daAssignStaff = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "select " & _
           "strUnitDesc, numUnitCode " & _
           "from AIRBRANCH.LookUPEPDUnits " & _
           "where numProgramCode = '4' " & _
           "or numUnitCode = '44' " & _
           "or numUnitCode = '43' " & _
           "or numUnitCode = '42' " & _
           "or numUnitCode = '41' " & _
           "or numUnitCode = '40' " & _
           "or numUnitCode = '39' " & _
           "or numUnitCode = '38'  " & _
           "or numUnitCode = '37'  "

            daFilterUnits = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "select distinct(strClass) as strClass " & _
            "from AIRBRANCH.APBHeaderData  " & _
            "order by strClass "

            daClassFilter = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select distinct(strCMSMember) as strCMSMember " & _
            "from AIRBRANCH.APBSupplamentalData " & _
            "order by strCMSMember "

            daCMSMemberFilter = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "select " & _
            "strCountyName, strCountyCode " & _
            "from AIRBRANCH.LookUpCountyInformation " & _
            "order by strCountyName "

            daCountyFilter = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select " & _
            "strDistrictName " & _
            "from AIRBRANCH.LookupDistricts " & _
            "order by strDistrictname "

            daDistrictFilter = New OracleDataAdapter(SQL, CurrentConnection)

            'If AccountArray(22, 3) = "1" And UserUnit <> "---" Then  'SSCP Unit Manager 
            '    SQL2 = "SELECT strunitdesc, " & _
            '    "numunitcode " & _
            '    "FROM AIRBRANCH.lookupepdunits " & _
            '    "WHERE numprogramCode  = '4' " & _
            '    "ORDER BY strunitdesc "
            'End If
            'If AccountArray(22, 2) = "1" Then 'District Liason 
            '    SQL2 = "select " & _
            '    "strUnitDesc, numUnitCode " & _
            '    "from AIRBRANCH.LookUPEPDUnits " & _
            '    "where numProgramCode = '4' " & _
            '    "or numUnitCode = '44' " & _
            '    "or numUnitCode = '43' " & _
            '    "or numUnitCode = '42' " & _
            '    "or numUnitCode = '41' " & _
            '    "or numUnitCode = '40' " & _
            '    "or numUnitCode = '39' " & _
            '    "or numUnitCode = '38'  " & _
            '    "or numUnitCode = '37'  "
            'Else
            '    SQL2 = "select strUnitDesc, numUnitCode " & _
            '                   "from AIRBRANCH.LookUpEPDUnits   " & _
            '                   "where numProgramCode = '4' " & _
            '                   "order by strUnitDesc  "
            'End If
            'If AccountArray(22, 4) = "1" Then
            '    SQL2 = "select strUnitDesc, numUnitCode " & _
            '                   "from AIRBRANCH.LookUpEPDUnits   " & _
            '                   "where numProgramCode = '4' " & _
            '                   "order by strUnitDesc  "
            'End If

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daStaff.Fill(dsStaff, "Staff")
            daUnits.Fill(dsUnits, "Units")
            daAssignStaff.Fill(dsAssignStaff, "AssignStaff")
            daFilterUnits.Fill(dsFilterUnits, "FilterUnits")
            daClassFilter.Fill(dsClassFilter, "ClassFilter")
            daCMSMemberFilter.Fill(dsCMSMemberFilter, "CMSMemberFilter")
            daCountyFilter.Fill(dsCountyFilter, "CountyFilter")
            daDistrictFilter.Fill(dsDistrictFilter, "DistrictFilter")

            If dsStaff.Tables(0).Rows.Count = 0 Then
                SQL = "Select (strLastName||', '||strFirstName) as UserName,  " & _
                "strUnitDesc, numUserID    " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.LookUpEPDUnits    " & _
                "where AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode (+)  " & _
                "and (numProgram = '4' or strLastName = 'District') " & _
                "order by strLastName "

                dsStaff = New DataSet
                daStaff = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daStaff.Fill(dsStaff, "Staff")
            End If

            cboFiscalYear.Items.Add(((Date.Now.Year) + 1).ToString)
            cboFiscalYear.Items.Add(((Date.Now.Year)).ToString)

            SQL = "select " & _
            "distinct(intYear) as FCEYear " & _
            "from AIRBRANCH.SSCPInspectionsRequired " & _
            "order by intYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("FCEYear")) Then
                Else
                    If cboFiscalYear.Items.Contains(dr.Item("FceYear").ToString) Then
                    Else
                        cboFiscalYear.Items.Add(dr.Item("FCEYear").ToString)
                    End If
                    If cboExistingYears.Items.Contains(dr.Item("FCEYear").ToString) Then
                    Else
                        cboExistingYears.Items.Add(dr.Item("FCEYear").ToString)
                    End If
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadComboBoxes()
        'Dim drStaff As New DataTable
        'Dim dtStaff As New DataTable
        Dim dtStaffSearch1 As New DataTable
        Dim dtStaffSearch2 As New DataTable
        Dim dtAssignStaff As New DataTable
        Dim dtUnits As New DataTable
        Dim dtFilterUnits As New DataTable
        Dim dtClassFilter As New DataTable
        Dim dtCMSMemberFilter As New DataTable
        Dim dtCountyFilter As New DataTable
        Dim dtDistrictFilter As New DataTable

        Dim drNewRow As DataRow
        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drDSRow3 As DataRow
        Dim drDSRow4 As DataRow
        Dim drDSRow5 As DataRow
        Dim drDSRow6 As DataRow
        Dim drDSRow7 As DataRow
        Dim drDSRow8 As DataRow

        Try

            dtAssignStaff.Columns.Add("UserName", GetType(System.String))
            dtAssignStaff.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtAssignStaff.NewRow()
            drNewRow("UserName") = " "
            drNewRow("numUserID") = "0"
            dtAssignStaff.Rows.Add(drNewRow)

            For Each drDSRow In dsAssignStaff.Tables("AssignStaff").Rows()
                drNewRow = dtAssignStaff.NewRow
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtAssignStaff.Rows.Add(drNewRow)
            Next

            With cboSSCPEngineer
                .DataSource = dtAssignStaff
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtStaffSearch1.Columns.Add("UserName", GetType(System.String))
            dtStaffSearch1.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtStaffSearch1.NewRow()
            drNewRow("UserName") = " "
            drNewRow("numUserID") = "0"
            dtStaffSearch1.Rows.Add(drNewRow)

            For Each drDSRow3 In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaffSearch1.NewRow
                drNewRow("UserName") = drDSRow3("UserName")
                drNewRow("numUserID") = drDSRow3("numUserID")
                dtStaffSearch1.Rows.Add(drNewRow)
            Next

            With cboFilterEngineer1
                .DataSource = dtStaffSearch1
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtStaffSearch2.Columns.Add("UserName", GetType(System.String))
            dtStaffSearch2.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtStaffSearch2.NewRow()
            drNewRow("UserName") = " "
            drNewRow("numUserID") = "0"
            dtStaffSearch2.Rows.Add(drNewRow)

            For Each drDSRow3 In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaffSearch2.NewRow
                drNewRow("UserName") = drDSRow3("UserName")
                drNewRow("numUserID") = drDSRow3("numUserID")
                dtStaffSearch2.Rows.Add(drNewRow)
            Next

            With cboFilterEngineer2
                .DataSource = dtStaffSearch2
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtUnits.Columns.Add("strUnitDesc", GetType(System.String))
            dtUnits.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtUnits.NewRow()
            drNewRow("strUnitDesc") = " "
            drNewRow("numUnitCode") = ""
            dtUnits.Rows.Add(drNewRow)

            For Each drDSRow2 In dsUnits.Tables("Units").Rows()
                drNewRow = dtUnits.NewRow
                drNewRow("strUnitDesc") = drDSRow2("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow2("numUnitCode")
                dtUnits.Rows.Add(drNewRow)
            Next


            With cboSSCPUnit2
                .DataSource = dtUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

            dtFilterUnits.Columns.Add("strUnitDesc", GetType(System.String))
            dtFilterUnits.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtFilterUnits.NewRow()
            drNewRow("strUnitDesc") = " "
            drNewRow("numUnitCode") = " "
            dtFilterUnits.Rows.Add(drNewRow)

            For Each drDSRow4 In dsFilterUnits.Tables("FilterUnits").Rows()
                drNewRow = dtFilterUnits.NewRow
                drNewRow("strUnitDesc") = drDSRow4("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow4("numUnitCode")
                dtFilterUnits.Rows.Add(drNewRow)
            Next

            With cboSSCPUnitFilter
                .DataSource = dtFilterUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

            With cboSSCPUnitFilter2
                .DataSource = dtFilterUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

            dtClassFilter.Columns.Add("strClass", GetType(System.String))

            drNewRow = dtClassFilter.NewRow()
            drNewRow("strClass") = " "
            dtClassFilter.Rows.Add(drNewRow)

            For Each drDSRow5 In dsClassFilter.Tables("ClassFilter").Rows()
                drNewRow = dtClassFilter.NewRow
                drNewRow("strClass") = drDSRow5("strClass")
                dtClassFilter.Rows.Add(drNewRow)
            Next

            With cboClassFilter1
                .DataSource = dtClassFilter
                .DisplayMember = "strClass"
                .ValueMember = "strClass"
                .SelectedIndex = 0
            End With

            With cboClassFilter2
                .DataSource = dtClassFilter
                .DisplayMember = "strClass"
                .ValueMember = "strClass"
                .SelectedIndex = 0
            End With


            dtCMSMemberFilter.Columns.Add("strCMSmember", GetType(System.String))

            drNewRow = dtCMSMemberFilter.NewRow()
            drNewRow("strCMSMember") = " "
            dtCMSMemberFilter.Rows.Add(drNewRow)

            For Each drDSRow8 In dsCMSMemberFilter.Tables("CMSMemberFilter").Rows()
                drNewRow = dtCMSMemberFilter.NewRow
                drNewRow("strCMSMember") = drDSRow8("strCMSMember")
                dtCMSMemberFilter.Rows.Add(drNewRow)
            Next

            With cboCMSMemberFilter1
                .DataSource = dtCMSMemberFilter
                .DisplayMember = "strCMSMember"
                .ValueMember = "strCMSMember"
                .SelectedIndex = 0
            End With

            With cboCMSMemberFilter2
                .DataSource = dtCMSMemberFilter
                .DisplayMember = "strCMSMember"
                .ValueMember = "strCMSMember"
                .SelectedIndex = 0
            End With

            With cboCMSFrequency
                .DataSource = dtCMSMemberFilter
                .DisplayMember = "strCMSMember"
                .ValueMember = "strCMSMember"
                .SelectedIndex = 0
            End With

            With cboCMSWarningFrequency
                .DataSource = dtCMSMemberFilter
                .DisplayMember = "strCMSMember"
                .ValueMember = "strCMSMember"
                .SelectedIndex = 0
            End With


            dtCountyFilter.Columns.Add("strCountyName", GetType(System.String))
            dtCountyFilter.Columns.Add("strCountyCode", GetType(System.String))

            drNewRow = dtCountyFilter.NewRow()
            drNewRow("strCountyName") = " "
            drNewRow("strCountyCode") = " "
            dtCountyFilter.Rows.Add(drNewRow)

            For Each drDSRow6 In dsCountyFilter.Tables("CountyFilter").Rows()
                drNewRow = dtCountyFilter.NewRow
                drNewRow("strCountyName") = drDSRow6("strCountyName")
                drNewRow("strCountyCode") = drDSRow6("strCountyCode")
                dtCountyFilter.Rows.Add(drNewRow)
            Next

            With cboCountyFilter1
                .DataSource = dtCountyFilter
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyName"
                .SelectedValue = 0
            End With

            With cboCountyFilter2
                .DataSource = dtCountyFilter
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyName"
                .SelectedValue = 0
            End With

            dtDistrictFilter.Columns.Add("strDistrictName", GetType(System.String))

            drNewRow = dtDistrictFilter.NewRow()
            drNewRow("strDistrictName") = " "
            dtDistrictFilter.Rows.Add(drNewRow)

            For Each drDSRow7 In dsDistrictFilter.Tables("DistrictFilter").Rows()
                drNewRow = dtDistrictFilter.NewRow
                drNewRow("strDistrictName") = drDSRow7("strDistrictName")
                dtDistrictFilter.Rows.Add(drNewRow)
            Next

            With cboDistrictFilter1
                .DataSource = dtDistrictFilter
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictName"
                .SelectedValue = 0
            End With

            With cboDistrictFilter2
                .DataSource = dtDistrictFilter
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictName"
                .SelectedValue = 0
            End With





            '--- This loads the Combo Box Compliance Units/Districts

            cboComplianceUnits.Items.Add("<Select a Unit or District>")
            cboComplianceUnits.Items.Add("Administrative")
            cboComplianceUnits.Items.Add("Air Toxics")
            cboComplianceUnits.Items.Add("Chemicals/Minerals")
            cboComplianceUnits.Items.Add("VOC/Combustion")
            cboComplianceUnits.Items.Add("All Units")
            cboComplianceUnits.Items.Add("District")

            cboComplianceUnits.SelectedIndex = 0


            '--- This loads the Combo Box Filter Option 1 on New Search 
            cboFacSearch1.Items.Add("<Select a Filter Option>")
            cboFacSearch1.Items.Add("AIRS Number")
            cboFacSearch1.Items.Add("City")
            cboFacSearch1.Items.Add("Classification")
            cboFacSearch1.Items.Add("CMS Status")
            cboFacSearch1.Items.Add("County")
            cboFacSearch1.Items.Add("District")
            'cboFacSearch1.Items.Add("District Engineer")
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
            'cboFacSearch2.Items.Add("District Engineer")
            cboFacSearch2.Items.Add("District Responsible")
            cboFacSearch2.Items.Add("Engineer")
            cboFacSearch2.Items.Add("Facility Name")
            cboFacSearch2.Items.Add("Operational Status")
            cboFacSearch2.Items.Add("SIC Codes")
            cboFacSearch2.Items.Add("SSCP Unit")
            cboFacSearch2.Items.Add("Unassigned Facilities")

            cboFacSearch2.SelectedIndex = 0

            '--- This loads the Combo Box Sort Option 1 for New Facility Search
            cboSort1.Items.Add("<Select a Filter Option>")
            cboSort1.Items.Add("AIRS Number")
            cboSort1.Items.Add("City")
            cboSort1.Items.Add("Classification")
            cboSort1.Items.Add("County")
            cboSort1.Items.Add("District")
            'cboSort1.Items.Add("District Engineer")
            cboSort1.Items.Add("District Responsible")
            cboSort1.Items.Add("Engineer")
            cboSort1.Items.Add("Facility Name")
            cboSort1.Items.Add("Operational Status")
            cboSort1.Items.Add("SIC Codes")
            cboSort1.Items.Add("SSCP Unit")

            cboSort1.SelectedIndex = 0

            '--- This loads the Combo Box Sort Option 2 for New Facility Search
            cboSort2.Items.Add("<Select a Filter Option>")
            cboSort2.Items.Add("AIRS Number")
            cboSort2.Items.Add("City")
            cboSort2.Items.Add("Classification")
            cboSort2.Items.Add("County")
            cboSort2.Items.Add("District")
            'cboSort2.Items.Add("District Engineer")
            cboSort2.Items.Add("District Responsible")
            cboSort2.Items.Add("Engineer")
            cboSort2.Items.Add("Facility Name")
            cboSort2.Items.Add("Operational Status")
            cboSort2.Items.Add("SIC Codes")
            cboSort2.Items.Add("SSCP Unit")

            cboSort2.SelectedIndex = 0

            '--- This loads the Combo Box Sort Option Order 1 for New Facility Search
            cboSortOrder1.Items.Add("Ascending Order")
            cboSortOrder1.Items.Add("Descending Order")
            cboSortOrder1.Text = "Ascending Order"

            '--- This loads the Combo Box Sort Option Order 2 for New Facility Search
            cboSortOrder2.Items.Add("Ascending Order")
            cboSortOrder2.Items.Add("Descending Order")
            cboSortOrder2.Text = "Ascending Order"

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadStatisticalLists()
        Try
            Dim dtAdmin As New DataTable
            Dim dtAir As New DataTable
            Dim dtChem As New DataTable
            Dim dtVOC As New DataTable
            Dim dtDistricts As New DataTable

            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles " & _
            "where numProgram = '4' " & _
            "and numUnit is null " & _
            "order by strLastName "

            dsAdminStaff = New DataSet
            daAdminStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAdminStaff.Fill(dsAdminStaff, "AdminStaff")

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles " & _
            "where numProgram = '4' " & _
            "and numUnit = '30' " & _
            "order by strLastName "

            dsAirStaff = New DataSet
            daAirStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAirStaff.Fill(dsAirStaff, "AirStaff")

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles " & _
            "where numProgram = '4' " & _
            "and numUnit = '31' " & _
            "order by strLastName "

            dsChemStaff = New DataSet
            daChemStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daChemStaff.Fill(dsChemStaff, "ChemStaff")

            SQL = "Select " & _
             "(strLastName||', '||strFirstName) as UserName, " & _
             "numUserID " & _
             "from AIRBRANCH.EPDUserProfiles " & _
             "where numProgram = '4' " & _
             "and numUnit = '32' " & _
             "order by strLastName "

            dsVOCStaff = New DataSet
            daVOCStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daVOCStaff.Fill(dsVOCStaff, "VOCStaff")

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName,  " & _
            "numUserID   " & _
            "from AIRBranch.EPDUserProfiles  " & _
            "where numBranch = '5' " & _
            "and (numProgram = '7' " & _
            "or numProgram = '9'  " & _
            "or numProgram = '10' " & _
            "or numProgram = '11' " & _
            "or numProgram = '12' " & _
            "or numProgram = '13' " & _
            "or numProgram = '14' " & _
            "or numProgram = '15') " & _
            "order by strLastName "

            dsDistrictStaff = New DataSet
            daDistrictStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daDistrictStaff.Fill(dsDistrictStaff, "DistrictStaff")

            dtAdmin.Columns.Add("UserName", GetType(System.String))
            dtAdmin.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsAdminStaff.Tables("AdminStaff").Rows()
                drNewRow = dtAdmin.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtAdmin.Rows.Add(drNewRow)
            Next

            dtAir.Columns.Add("UserName", GetType(System.String))
            dtAir.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsAirStaff.Tables("AirStaff").Rows()
                drNewRow = dtAir.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtAir.Rows.Add(drNewRow)
            Next

            dtChem.Columns.Add("UserName", GetType(System.String))
            dtChem.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsChemStaff.Tables("ChemStaff").Rows()
                drNewRow = dtChem.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtChem.Rows.Add(drNewRow)
            Next

            dtVOC.Columns.Add("UserName", GetType(System.String))
            dtVOC.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsVOCStaff.Tables("VOCStaff").Rows()
                drNewRow = dtVOC.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtVOC.Rows.Add(drNewRow)
            Next

            dtDistricts.Columns.Add("UserName", GetType(System.String))
            dtDistricts.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsDistrictStaff.Tables("DistrictStaff").Rows()
                drNewRow = dtDistricts.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtDistricts.Rows.Add(drNewRow)
            Next

            With clbAdministrative
                .DataSource = dtAdmin
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With
            With clbAirToxicUnit
                .DataSource = dtAir
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With
            With clbChemicalsMinerals
                .DataSource = dtChem
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With
            With clbVOCCombustion
                .DataSource = dtVOC
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With
            With clbDistricts
                .DataSource = dtDistricts
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadSelectedFacilitesGrid()
        Try
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
            ' dgvFilteredFacilityList.Columns("LastInspection").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvSelectedFacilityList.Columns.Add("FCERequired", "FCE Required")
            dgvSelectedFacilityList.Columns("FCERequired").DisplayIndex = 7
            dgvSelectedFacilityList.Columns("FCERequired").Width = 150
            dgvSelectedFacilityList.Columns("FCERequired").ReadOnly = True

            dgvSelectedFacilityList.Columns.Add("LastFCE", "Last FCE")
            dgvSelectedFacilityList.Columns("LastFCE").DisplayIndex = 8
            dgvSelectedFacilityList.Columns("LastFCE").Width = 150
            dgvSelectedFacilityList.Columns("LastFCE").ReadOnly = True
            '  dgvFilteredFacilityList.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvSelectedFacilityList.Columns.Add("strCMSStatus", "CMS Status")
            dgvSelectedFacilityList.Columns("strCMSStatus").DisplayIndex = 9
            dgvSelectedFacilityList.Columns("strCMSStatus").Width = 150
            dgvSelectedFacilityList.Columns("strCMSStatus").ReadOnly = True

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Subs and Functions"

    Sub LoadCMSUniverse()
        Dim CMSStatus As String = ""
        'Dim SQLLine As String = ""

        Try
            Select Case cboCMSFrequency.Text
                Case "A"
                    CMSStatus = " and strCMSMember = 'A' "
                Case "S"
                    CMSStatus = " and strCMSMember = 'S' "
                Case Else
                    CMSStatus = " and strCMSMember is not null "
            End Select

            SQL = "Select * " & _
           "from AIRBRANCH.VW_SSCP_CMSWarning " & _
           "where AIRSNumber is not Null " & _
           CMSStatus

            dsCMSDataSet = New DataSet

            daCMSDataSet = New OracleDataAdapter(SQL, CurrentConnection)
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
            '  dgvCMSUniverse.Columns("strClass").DisplayIndex = 9

            txtCMSCount.Text = dgvCMSUniverse.RowCount

            Exit Sub
            ' 
            'Select Case cboCMSFrequency.Text
            '    Case "A"
            '        SQLLine = "and StrCMSMember = 'A' "
            '    Case "S"
            '        SQLLine = "and strCMSMember = 'S' "
            '    Case "A & S"
            '        SQLLine = "and strCMSMember Is NOT Null "
            '    Case Else
            '        SQLLine = "and strCMSMember Is NOT Null "
            'End Select

            'SQL = "Select " & _
            '"substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            '"strFacilityName, strFacilityCity,   " & _
            '"strCMSMember, strOperationalStatus,  " & _
            '"strCountyName, strDistrictCounty,  " & _
            '"strDistrictName   " & _
            '"from AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBSupplamentalData,   " & _
            '"AIRBRANCH.APBHeaderData, AIRBRANCH.LookUpCountyInformation,  " & _
            '"AIRBRANCH.LookUpDistrictInformation, AIRBRANCH.LookUPDistricts  " & _
            '"where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber   " & _
            '"and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
            '"and substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
            '"and substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpDistrictInformation.strDistrictCounty " & _
            '"and AIRBRANCH.LookUPDistricts.strDistrictCode = AIRBRANCH.LookUpDistrictInformation.strDistrictCode  " & SQLLine
            ''"and StrCMSMember = 'A' "

            'dsCMSDataSet = New DataSet

            'daCMSDataSet = New OracleDataAdapter(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If

            'daCMSDataSet.Fill(dsCMSDataSet, "CMSData")

            'dgvCMSUniverse.DataSource = dsCMSDataSet
            'dgvCMSUniverse.DataMember = "CMSData"

            'dgvCMSUniverse.RowHeadersVisible = False
            'dgvCMSUniverse.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            'dgvCMSUniverse.AllowUserToResizeColumns = True
            'dgvCMSUniverse.AllowUserToAddRows = False
            'dgvCMSUniverse.AllowUserToDeleteRows = False
            'dgvCMSUniverse.AllowUserToOrderColumns = True
            'dgvCMSUniverse.AllowUserToResizeRows = True
            'dgvCMSUniverse.ColumnHeadersHeight = "35"
            'dgvCMSUniverse.Columns("strCMSMember").HeaderText = "CMS Class"
            'dgvCMSUniverse.Columns("strCMSMember").DisplayIndex = 0
            'dgvCMSUniverse.Columns("AIRSNumber").HeaderText = "AIRS Number"
            'dgvCMSUniverse.Columns("AIRSNumber").DisplayIndex = 1
            'dgvCMSUniverse.Columns("strFacilityName").HeaderText = "Facility Name"
            'dgvCMSUniverse.Columns("strFacilityName").DisplayIndex = 2
            'dgvCMSUniverse.Columns("strFacilityCity").HeaderText = "City"
            'dgvCMSUniverse.Columns("strFacilityCity").DisplayIndex = 3
            'dgvCMSUniverse.Columns("strCountyName").HeaderText = "County"
            'dgvCMSUniverse.Columns("strCountyName").DisplayIndex = 4
            'dgvCMSUniverse.Columns("strDistrictName").HeaderText = "District"
            'dgvCMSUniverse.Columns("strDistrictName").DisplayIndex = 5
            'dgvCMSUniverse.Columns("strOperationalStatus").HeaderText = "Operational Status"
            'dgvCMSUniverse.Columns("strOperationalStatus").DisplayIndex = 6
            'dgvCMSUniverse.Columns("strDistrictCounty").HeaderText = "County Code"
            'dgvCMSUniverse.Columns("strDistrictCounty").DisplayIndex = 7
            'dgvCMSUniverse.Columns("strDistrictCounty").Visible = False

            'txtCMSCount.Text = dsCMSDataSet.Tables.Item(0).Rows.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub AddFacilityToCMS()
        Dim CMSState As String = ""

        Try

            If rdbCMSClassA.Checked = True Or rdbCMSClassS.Checked = True Then
                If rdbCMSClassA.Checked = True Then
                    CMSState = "A"
                Else
                    CMSState = "S"
                End If
                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                    "strCMSMember = '" & CMSState & "' " & _
                    "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader

                End If
            Else
                MsgBox("Select a CMS status of either 'A' or 'S'.", MsgBoxStyle.Information, "SSSCP Managers Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub RemoveFacilityFromCMS()
        Try

            SQL = "Select strAIRSNumber " & _
                              "from AIRBRANCH.APBSupplamentalData " & _
                              "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                "strCMSMember = '' " & _
                "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub RunInspectionReport()
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

                    SQL = "Select " & _
                  "distinct(strLastname||', '||strFirstName) as Staff,  " & _
                  "case " & _
                  "     when Startedreport is NUll then 0  " & _
                  "     ELSE startedReport " & _
                  "end as StartedReport, " & _
                  "case  " & _
                  "     when StartedInspection is NULL then 0  " & _
                  "     Else StartedInspection " & _
                  "End as StartedInspection,  " & _
                  "Case  " & _
                  "     when StartedISMPTest is Null then 0  " & _
                  "     Else StartedISMPTest  " & _
                  "end as StartedISMPTest,  " & _
                  "Case  " & _
                  "     when StartedACC is Null then 0  " & _
                  "     Else StartedACC " & _
                  "End as StartedACC, " & _
                  "Case  " & _
                  "     when StartedNotification is NULL then 0  " & _
                  "     Else StartedNotification  " & _
                  "End as StartedNotification,  " & _
                  "case  " & _
                  "     when ClosedReport is NUll then 0  " & _
                  "     ELSE ClosedReport " & _
                  "end as ClosedReport,  " & _
                  "case  " & _
                  "     when ClosedInspection is NULL then 0  " & _
                  "     Else ClosedInspection " & _
                  "End as ClosedInspection,  " & _
                  "Case  " & _
                  "     when ClosedISMPTest is Null then 0  " & _
                  "     Else ClosedISMPTest  " & _
                  "end as ClosedISMPTest,  " & _
                  "Case  " & _
                  "     when ClosedACC is Null then 0  " & _
                  "     Else ClosedACC " & _
                  "End as ClosedACC, " & _
                  "Case  " & _
                  "     when ClosedNotification is NULL then 0  " & _
                  "     Else ClosedNotification  " & _
                  "End as ClosedNotification,  " & _
                  "case  " & _
                  "     when OpenReport is NUll then 0  " & _
                  "     ELSE OpenReport " & _
                  "end as OpenReport,  " & _
                  "case  " & _
                  "     when OpenInspection is NULL then 0  " & _
                  "     Else OpenInspection " & _
                  "End as OpenInspection,  " & _
                  "Case  " & _
                  "     when OpenISMPTest is Null then 0  " & _
                  "     Else OpenISMPTest  " & _
                  "end as OpenISMPTest,  " & _
                  "Case  " & _
                  "     when OpenACC is Null then 0  " & _
                  "     Else OpenACC " & _
                  "End as OpenACC, " & _
                  "Case  " & _
                  "     when OpenNotification is NULL then 0  " & _
                  "     Else OpenNotification  " & _
                  "End as OpenNotification  " & _
                  "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSCPItemMaster,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedReport  " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '01'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedReports,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedInspection " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '02'  " & _
                  DateReceivedBias & " " & _
                  "group by strResponsibleStaff) StartedInspections,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedISMPTest " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '03'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedISMPTEsts,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedAcc " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '04'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedACCs,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedNotification " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '05'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedNotifications,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedReport  " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '01'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedReports,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedInspection " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '02'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedInspections,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedISMPTest " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '03'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedISMPTEsts,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedAcc " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '04'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedACCs,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedNotification " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '05'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedNotifications, " & _
                  "(Select strResponsibleStaff, count(*) as OpenReport  " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '01'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenReports,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenInspection " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '02'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenInspections,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenISMPTest " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '03'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenISMPTEsts,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenAcc " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '04'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenACCs,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenNotification " & _
                  "from AIRBRANCH.SSCPItemMaster   " & _
                  "where strEventType = '05'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenNotifications " & _
                  "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = StartedInspections.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = StartedReports.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = StartedISMPTests.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = StartedACCS.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = StartedNotifications.strResponsibleStaff (+)  " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClosedInspections.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClosedReports.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClosedISMPTests.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClosedACCS.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClosedNotifications.strResponsibleStaff (+)  " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = OpenInspections.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = OpenReports.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = OpenISMPTests.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = OpenACCS.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = OpenNotifications.strResponsibleStaff (+) " & _
                  "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " & _
                  "and strDelete is Null "

                    SQL2 = "Select distinct(AIRBRANCH.SSCPItemMaster.strResponsibleStaff), " & _
                    "Case " & _
                    "	When ClassA is Null then 0  " & _
                    "	else CLassA " & _
                    "end ClassA,  " & _
                    "Case  " & _
                    "	When ClassSM is Null then 0  " & _
                    "	else CLassSM " & _
                    "end ClassSM,  " & _
                    "Case  " & _
                    "	When ClassB is Null then 0  " & _
                    "	else CLassB  " & _
                    "end ClassB  " & _
                    "from AIRBRANCH.SSCPItemMaster,  " & _
                    "(select strResponsibleStaff, count(*) as ClassA  " & _
                    "from AIRBRANCH.APBHeaderData, AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPInspections  " & _
                    "where AIRBRANCH.APBHeaderData.strAIRSNumber = AIRBRANCH.SSCPItemMaster.strAIRSNUmber  " & _
                    "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPInspections.strTrackingNumber  " & _
                    "and strClass = 'A'  " & _
                    DateInspection & _
                    "group by strResponsibleStaff) ClassAs,  " & _
                    "(select strResponsibleStaff, count(*) as ClassSM  " & _
                    "from AIRBRANCH.APBHeaderData, AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPInspections  " & _
                    "where AIRBRANCH.APBHeaderData.strAIRSNumber = AIRBRANCH.SSCPItemMaster.strAIRSNUmber  " & _
                    "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPInspections.strTrackingNumber  " & _
                    "and strClass = 'SM'  " & _
                    DateInspection & _
                    "group by strResponsibleStaff) ClassSMs,  " & _
                    "(select strResponsibleStaff, count(*) as ClassB  " & _
                    "from AIRBRANCH.APBHeaderData, AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPInspections  " & _
                    "where AIRBRANCH.APBHeaderData.strAIRSNumber = AIRBRANCH.SSCPItemMaster.strAIRSNUmber  " & _
                    "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPInspections.strTrackingNumber  " & _
                    "and strClass = 'B'  " & _
                    DateInspection & _
                    "group by strResponsibleStaff) ClassBs " & _
                    "where " & _
                    "AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClassAs.strResponsibleStaff (+)  " & _
                    "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClassSMs.strResponsibleStaff (+) " & _
                    "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = ClassBs.strResponsibleStaff (+) " & _
                    "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " & _
                    "and strDelete is Null "

                    SQL3 = "Select " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "AIRBRANCH.SSCPItemMaster.strTrackingNumber, datInspectionDateStart,  " & _
                    "strClass  " & _
                    "from AIRBRANCH.APBHeaderData, AIRBRANCH.APBFacilityInformation,  " & _
                    "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPInspections  " & _
                    "where AIRBRANCH.APBHeaderData.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIrSnumber  " & _
                    "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCPItemMaster.strAIRSNumber  " & _
                    "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPInspections.strTrackingNumber  " & _
                    DateInspection & _
                    "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " & _
                    "and strDelete is Null " & _
                    "order by strClass, datInspectionDateStart "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                    '  

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        InspectA = dr2.Item("ClassA")
                        InspectSM = dr2.Item("ClassSM")
                        InspectB = dr2.Item("ClassB")
                    End While

                    cmd3 = New OracleCommand(SQL3, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr3 = cmd3.ExecuteReader
                    InspectList = ""

                    While dr3.Read
                        InspectList = InspectList & dr3.Item("strFacilityName") & ", " & dr3.Item("strFacilityCity") & vbCrLf & _
                        vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & _
                        Format(dr3.Item("datInspectionDateStart"), "dd-MMM-yyyy") & vbTab & _
                        dr3.Item("strClass") & vbTab & vbTab & dr3.Item("strtrackingNumber") & vbCrLf

                    End While

                    OpenTotal = CStr(CInt(OpenReports) + CInt(OpenInspections) + CInt(OpenISMPTests) + CInt(OpenNotifications) + CInt(OpenACCS))
                    ClosedTotal = CStr(CInt(ClosedReports) + CInt(ClosedInspections) + CInt(ClosedISMPTests) + CInt(ClosedNotifications) + CInt(ClosedACCS))
                    StartedTotal = CStr(CInt(StartedReports) + CInt(StartedInspections) + CInt(StartedISMPTests) + CInt(StartedNotifications) + CInt(StartedACCS))

                    Statement = Statement & "For the staff member(s): " & StaffName & vbCrLf & _
                    vbTab & DateText & vbCrLf & vbCrLf & "I. Event(s):" & vbCrLf & _
                    vbTab & vbTab & vbTab & vbTab & "Started" & vbTab & vbTab & "Closed" & vbTab & vbTab & "Currently Open" & _
                    vbCrLf & "Report(s)" & vbTab & vbTab & vbTab & vbTab & StartedReports & vbTab & vbTab & ClosedReports & vbTab & vbTab & OpenReports & _
                    vbCrLf & "Inspection(s)" & vbTab & vbTab & vbTab & StartedInspections & vbTab & vbTab & ClosedInspections & vbTab & vbTab & OpenInspections & _
                    vbCrLf & "Notification(s)" & vbTab & vbTab & vbTab & StartedNotifications & vbTab & vbTab & ClosedNotifications & vbTab & vbTab & OpenNotifications & _
                    vbCrLf & "Performance Test(s)" & vbTab & vbTab & vbTab & StartedISMPTests & vbTab & vbTab & ClosedISMPTests & vbTab & vbTab & OpenISMPTests & _
                    vbCrLf & "ACC(s)" & vbTab & vbTab & vbTab & vbTab & StartedACCS & vbTab & vbTab & ClosedACCS & vbTab & vbTab & OpenACCS & _
                    vbCrLf & Line & vbCrLf & _
                    "Total(s)" & vbTab & vbTab & vbTab & vbTab & StartedTotal & vbTab & vbTab & ClosedTotal & vbTab & vbTab & OpenTotal & _
                    vbCrLf & vbCrLf & "II. Inspection(s):" & vbCrLf & _
                    vbTab & InspectA & " A Source(s)" & _
                    vbCrLf & vbTab & InspectSM & " SM Source(s)" & _
                    vbCrLf & vbTab & InspectB & " B Source(s)" & _
                    vbCrLf & vbCrLf & "Company, City" & vbTab & vbTab & vbTab & vbTab & vbTab & _
                    "Date" & vbTab & vbTab & "Class" & vbTab & vbTab & "Tracking #" & vbCrLf & Line & vbCrLf & _
                    InspectList
                Next
            Next

            rtbInspectionReport.Clear()

            rtbInspectionReport.Text = Statement

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub RunCMSWarningReport()
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
                            StartCMSA = Format(CDate(OracleDate).AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbNext90Days.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-550), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-550), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-550), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbNext120Days.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-490), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-490), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-490), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbNextYear.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-0), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-0), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-0), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
                If rdbFCEOverdue.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  < '" & StartCMSA & "' "
                        Case "S"
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  < '" & StartCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " & _
                          "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "
                    End Select
                End If

                If rdbFCEPerformedWithinYear.Checked = True Then
                    Select Case cboCMSWarningFrequency.Text
                        Case "A"
                            StartCMSA = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'A' and LastFCE  between '" & StartCMSA & "' and '" & EndCMSA & "' "
                        Case "S"
                            StartCMSS = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and strCMSMember = 'S' and LastFCE  between '" & StartCMSS & "' and '" & EndCMSS & "' "
                        Case "A & S"
                            StartCMSA = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " & _
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                    End Select
                End If
            End If


            SQL = "Select * " & _
            "from AIRBRANCH.VW_SSCP_CMSWarning " & _
            "where AIRSNumber is not Null " & _
            FCEStatus & CMSStatus

            If SQL <> "" Then
                dsCMSWarningDataSet = New DataSet
                daCMSWarningDataSet = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub PrintStaffReport()
        Try
            'Dim WordApp As New Word.ApplicationClass
            'Dim wordDoc As Word.DocumentClass
            Dim wordDoc As Microsoft.Office.Interop.Word.Document
            Dim WordApp As New Microsoft.Office.Interop.Word.Application

            wordDoc = WordApp.Documents.Add()
            wordDoc.Activate()
            WordApp.Selection.TypeText(rtbInspectionReport.Text)
            WordApp.Visible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub FilterPollutantSearch()
        Try
            Dim PollutantLine As String

            PollutantLine = ""
            If chbStatusB.Checked = True Then
                PollutantLine = " strComplianceStatus = 'B' "
            End If
            If chbStatus1.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '1' "
                Else
                    PollutantLine = " strComplianceStatus = '1' "
                End If
            End If
            If chbStatus6.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '6' "
                Else
                    PollutantLine = " strComplianceStatus = '6' "
                End If
            End If
            If chbStatusW.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = 'W' "
                Else
                    PollutantLine = " strComplianceStatus = 'W' "
                End If
            End If
            If chbStatus8.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '8' "
                Else
                    PollutantLine = " strComplianceStatus = '8' "
                End If
            End If
            If chbStatus0.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '0' "
                Else
                    PollutantLine = " strComplianceStatus = '0' "
                End If
            End If
            If chbStatus5.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '5' "
                Else
                    PollutantLine = " strComplianceStatus = '5' "
                End If
            End If
            If chbStatus2.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '2' "
                Else
                    PollutantLine = " strComplianceStatus = '2' "
                End If
            End If
            If chbStatus3.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '3' "
                Else
                    PollutantLine = " strComplianceStatus = '3' "
                End If
            End If
            If chbStatus4.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '4' "
                Else
                    PollutantLine = " strComplianceStatus = '4' "
                End If
            End If
            If chbStatus9.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = '9' "
                Else
                    PollutantLine = " strComplianceStatus = '9' "
                End If
            End If
            If chbStatusC.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = 'C' "
                Else
                    PollutantLine = " strComplianceStatus = 'C' "
                End If
            End If
            If chbStatusM.Checked = True Then
                If PollutantLine <> "" Then
                    PollutantLine = PollutantLine & " or strComplianceStatus = 'M' "
                Else
                    PollutantLine = " strComplianceStatus = 'M' "
                End If
            End If
            If PollutantLine <> "" Then
                PollutantLine = "and (" & PollutantLine & ") "
            Else
                PollutantLine = ""
            End If

            SQL = "Select " & _
            "substr(AIRBRANCH.APBAirProgramPollutants.strAIRSnumber, 5) as AIRSNumber, " & _
            "strFacilityName, " & _
            "(strComplianceStatus|| ' - ' ||strComplianceDesc) as PollutantStatus, " & _
            "strPollutantDescription " & _
            "from AIRBRANCH.APBAirProgramPollutants, AIRBRANCH.APBFacilityInformation, " & _
            "AIRBRANCH.LookUpComplianceStatus, AIRBRANCH.LookUPPollutants " & _
            "where AIRBRANCH.APBAirProgramPollutants.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber " & _
            "and AIRBRANCH.APBAirProgramPollutants.strComplianceStatus  = AIRBRANCH.LookUpComplianceStatus.strComplianceCode " & _
            "and AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.APBAirProgramPollutants.strPollutantKey " & _
            PollutantLine

            dsPollutantList = New DataSet
            daPollutantList = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daPollutantList.Fill(dsPollutantList, "PollutantList")
            dgvPollutantFacilities.DataSource = dsPollutantList
            dgvPollutantFacilities.DataMember = "PollutantList"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvPollutantFacilities.RowHeadersVisible = False
            dgvPollutantFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvPollutantFacilities.AllowUserToResizeColumns = True
            dgvPollutantFacilities.AllowUserToAddRows = False
            dgvPollutantFacilities.AllowUserToDeleteRows = False
            dgvPollutantFacilities.AllowUserToOrderColumns = True
            dgvPollutantFacilities.AllowUserToResizeRows = True
            dgvPollutantFacilities.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvPollutantFacilities.Columns("AIRSNumber").DisplayIndex = 0
            dgvPollutantFacilities.Columns("AIRSNumber").Width = 100
            dgvPollutantFacilities.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvPollutantFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvPollutantFacilities.Columns("strFacilityName").Width = 250
            dgvPollutantFacilities.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgvPollutantFacilities.Columns("strPollutantDescription").DisplayIndex = 2
            dgvPollutantFacilities.Columns("strPollutantDescription").Width = 200
            dgvPollutantFacilities.Columns("PollutantStatus").HeaderText = "Status"
            dgvPollutantFacilities.Columns("PollutantStatus").DisplayIndex = 3
            dgvPollutantFacilities.Columns("PollutantStatus").Width = 400

            txtPollutantCount.Text = dgvPollutantFacilities.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub RunACCStats()
        Try
            Dim EngineerList As String = ""
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPEngineer = '" & clbAdministrative.SelectedValue & "' Or "
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
            "count(*) as TotalFacilities " & _
            "from " & _
            "(select " & _
            "max(intYear), strAIRSNumber " & _
            "from AIRBRANCH.SSCPInspectionsRequired " & _
            EngineerList & _
            "group by strAIRSNumber) "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            SQL = "select count(*) as TotalACCs " & _
            "from AIRBRANCH.SSCPItemMaster " & _
            "where strEventType = '04' " & _
            "and datReceivedDate between '" & Me.DTPSearchDateStart.Text & "' and '" & Me.DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtFacilitiesReporting.Text = dr.Item("TotalACCs")
            End While
            dr.Close()

            '---Requiring resubmittals
            SQL = "select " & _
            "count(*) as TotalRequiringResubmittals  " & _
            "from AIRBRANCH.SSCPACCs, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCs.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '2'  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtResubmittals.Text = dr.Item("TotalRequiringResubmittals")
            End While
            dr.Close()

            '---Submitted Late
            SQL = "select " & _
            "count(*) as SubmittedLate " & _
            "from AIRBRANCH.SSCPACCs, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCs.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strPostMarkedOnTime = 'False' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtSubmittedLate.Text = dr.Item("SubmittedLate")
            End While
            dr.Close()

            '---Devations Reported in first Submittal
            SQL = "select " & _
            "count(*) as DeviationsReported " & _
            "from AIRBRANCH.SSCPACCs, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCs.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '1'  " & _
            "and strReportedDeviations = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            SQL = "select  " & _
            "count(*) as DeviationsCorrect " & _
            "from AIRBRANCH.SSCPACCsHistory, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCsHistory.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '1'  " & _
            "and strReportedDeviations = 'False' " & _
            "and strDeviationsUnReported = 'False' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsCorrectlyReported.Text = dr.Item("DeviationsCorrect")
            End While
            dr.Close()

            '   ---Incorrectly
            SQL = "select " & _
            "count(*) as DeviationsIncorrect " & _
            "from AIRBRANCH.SSCPACCsHistory, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCsHistory.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '1'  " & _
            "and strReportedDeviations = 'False' " & _
            "and strDeviationsUnReported = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsIncorrectlyReported.Text = dr.Item("DeviationsIncorrect")
            End While
            dr.Close()

            '---Deviations Reported in Final Report 
            SQL = "select " & _
            "count(*) as DeviationsInFinal " & _
            "from AIRBRANCH.SSCPACCs, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCs.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strReportedDeviations = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsReportedInFinal.Text = dr.Item("DeviationsInFinal")
            End While
            dr.Close()

            '---Deviations Not Previously Report
            SQL = "select  " & _
            "count(*) as DeviationsNotReported " & _
            "from AIRBRANCH.SSCPACCs, AIRBRANCH.SSCPItemMaster " & _
            "where AIRBRANCH.SSCPACCs.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strDeviationsUnReported = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtDeviationsNotPreviouslyReported.Text = dr.Item("DeviationsNotReported")
            End While
            dr.Close()

            '---Enforcement Action Taken 
            SQL = "select count(*) as EnforcementTaken " & _
            "from AIRBRANCH.SSCP_AuditedEnforcement, AIRBRANCH.SSCPItemMaster  " & _
            "where AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
            "and strEventType = '04'  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtEnforcementActionTaken.Text = dr.Item("EnforcementTaken")
            End While
            dr.Close()

            '    ---LON
            SQL = "select count(*) as LONTaken  " & _
            "from AIRBRANCH.SSCP_AuditedEnforcement, AIRBRANCH.SSCPItemMaster  " & _
             "where AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
            "and strEventType = '04'  " & _
            "and datLONSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtLONTaken.Text = dr.Item("LONTaken")
            End While
            dr.Close()

            '---NOV 
            SQL = "select count(*) as NOVTaken " & _
            "from AIRBRANCH.SSCP_AuditedEnforcement, AIRBRANCH.SSCPItemMaster " & _
             "where AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
           "and strEventType = '04'  " & _
            "and datNFALetterSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNOVTaken.Text = dr.Item("NOVTaken")
            End While
            dr.Close()

            '---CO 
            SQL = "select count(*) as COTaken " & _
            "from AIRBRANCH.SSCP_AuditedEnforcement, AIRBRANCH.SSCPItemMaster  " & _
            "where AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
            "and strEventType = '04'  " & _
            "and datCOResolved is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtCOTaken.Text = dr.Item("COTaken")
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Buttons"
    Private Sub llbViewCMSUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewCMSUniverse.LinkClicked
        Try

            LoadCMSUniverse()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbCMSOpenFacilitySummary_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary.LinkClicked
        Try
            Dim parameters As New Generic.Dictionary(Of String, String)
            parameters("airsnumber") = txtCMSAIRSNumber.Text
            OpenSingleForm(IAIPFacilitySummary, parameters:=parameters, closeFirst:=True)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbCMSOpenFacilitySummary2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary2.LinkClicked
        Try
            If Not DAL.Facility.AirsNumberExists(txtCMSAIRSNumber2.Text) Then
                MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
                Exit Sub
            End If
            Dim parameters As New Generic.Dictionary(Of String, String)
            parameters("airsnumber") = txtCMSAIRSNumber.Text
            OpenSingleForm(IAIPFacilitySummary, parameters:=parameters, closeFirst:=True)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddToCmsUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToCmsUniverse.Click
        Try
            AddFacilityToCMS()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnDeleteFacilityFromCms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacilityFromCms.Click
        Try
            RemoveFacilityFromCMS()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblRunInspectionReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblRunInspectionReport.LinkClicked
        Try
            RunInspectionReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region
    Private Sub dgvCMSWarning_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvCMSWarning.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtCMSAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCMSAIRSNumber.TextChanged
        Try

            If txtCMSAIRSNumber.Text.Length = 8 Then
                SQL = "select " & _
               "AIRSNUMBER, STRFACILITYNAME, " & _
               "STRFACILITYCITY, STRCOUNTYNAME, " & _
               "STRDISTRICTNAME, STROPERATIONALSTATUS, " & _
               "STRCMSMEMBER, LASTFCE, strClass, " & _
               "(strLastName||', '||strFirstName) as AssignedEngineer " & _
               "from " & _
               "(select * " & _
               "from AIRBRANCH.VW_SSCP_CMSWARNING) TABLE1, " & _
               "(select " & _
               "max(INTYEAR), NUMSSCPENGINEER, " & _
               "strairsnumber " & _
               "from AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
               "group by NUMSSCPENGINEER, STRAIRSNUMBER)TABLE2, " & _
               "AIRBRANCH.EPDUSERPROFILES " & _
               "where '0413'||TABLE1.AIRSNUMBER = TABLE2.STRAIRSNUMBER (+) " & _
               "and Table2.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numuserid (+)  " & _
               "and AIRSNumber = '" & txtCMSAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtCMSAIRSNumber2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCMSAIRSNumber2.TextChanged
        Try

            If txtCMSAIRSNumber2.Text.Length = 8 Then
                SQL = "select " & _
                "AIRSNUMBER, STRFACILITYNAME, " & _
                "STRFACILITYCITY, STRCOUNTYNAME, " & _
                "STRDISTRICTNAME, STROPERATIONALSTATUS, " & _
                "STRCMSMEMBER, LASTFCE, strClass, " & _
                "(strLastName||', '||strFirstName) as AssignedEngineer " & _
                "from " & _
                "(select * " & _
                "from AIRBRANCH.VW_SSCP_CMSWARNING) TABLE1, " & _
                "(select " & _
                "max(INTYEAR), NUMSSCPENGINEER, " & _
                "strairsnumber " & _
                "from AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                "group by NUMSSCPENGINEER, STRAIRSNUMBER)TABLE2, " & _
                "AIRBRANCH.EPDUSERPROFILES " & _
                "where '0413'||TABLE1.AIRSNUMBER = TABLE2.STRAIRSNUMBER (+) " & _
                "and Table2.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numuserid (+)  " & _
                "and AIRSNumber = '" & txtCMSAIRSNumber2.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboComplianceUnits_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboComplianceUnits.SelectedIndexChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub lblCMSWarning_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblCMSWarning.LinkClicked
        Try

            RunCMSWarningReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbPrintStaffReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPrintStaffReport.LinkClicked
        Try
            If rtbInspectionReport.Text <> "" Then
                PrintStaffReport()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFacilities.Click
        Try

            FilterPollutantSearch()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Try
            If txtAIRSNumber.Text <> "" Then

                EditAirProgramPollutants = Nothing
                If EditAirProgramPollutants Is Nothing Then EditAirProgramPollutants = New IAIPEditAirProgramPollutants
                EditAirProgramPollutants.txtAirsNumber.Text = Me.txtAIRSNumber.Text
                EditAirProgramPollutants.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgvPollutantFacilities_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvPollutantFacilities.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvPollutantFacilities.HitTest(e.X, e.Y)

        Try


            If dgvPollutantFacilities.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvPollutantFacilities.Columns(0).HeaderText = "AIRS #" Then
                    txtAIRSNumber.Text = dgvPollutantFacilities(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRunStatisticalReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunStatisticalReport.Click
        Try

            RunACCStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "ACC Subs and Functions"
    Sub ViewACCTotalAssigned()
        Try
            Dim EngineerList As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    EngineerList = EngineerList & " numSSCPENGINEER = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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

            SQL = "select " & _
            "SUBSTR(TABLE1.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "(strLastName||', '||strFirstName) as UserName " & _
            "from " & _
            "(select " & _
            "max(intYear), strAIRSNumber, " & _
            "numSSCPEngineer " & _
            "from AIRBRANCH.SSCPInspectionsRequired " & _
            EngineerList & _
            "group by strAIRSNumber, numSSCPEngineer) Table1, " & _
            "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles " & _
            "where Table1.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRsnumber " & _
            "and Table1.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numUserID "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCReporting()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
                "strFacilityName,  " & _
                "(strLastname||', '||strFirstName) as UserName,  " & _
                "strTrackingNumber " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,  " & _
                "AIRBRANCH.SSCPItemMaster  " & _
                "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber  " & _
                "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID   " & _
                "and strEventType = '04'  " & _
                "and datReceivedDate between '" & Me.DTPSearchDateStart.Text & "' and '" & Me.DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCRequiringResubmittal()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
                "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCs    " & _
                "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
                "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCs.strTrackingNumber  " & _
                "and strSubmittalNumber = '2'  " & _
                "and strEventType = '04'   " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCSubmittedLate()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
           "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
           "strFacilityName,   " & _
           "(strLastName||', '||strFirstName) as UserName,   " & _
           "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
           "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
           "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCs    " & _
           "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
           "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
           "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCs.strTrackingNumber  " & _
           "and strPostMarkedOnTime = 'False' " & _
           "and strEventType = '04'   " & _
           "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
           ResponsibleStaff & _
           "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCDeviationsReported()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
                "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCs    " & _
                "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
                "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCs.strTrackingNumber  " & _
                "and strSubmittalNumber = '1'  " & _
                "and strEventType = '04'   " & _
                "and strReportedDeviations = 'True' " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCDeviationsReportedCorrectly()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
                "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCsHistory    " & _
                "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
                "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCsHistory.strTrackingNumber  " & _
                "and strSubmittalNumber = '1'  " & _
                "and strEventType = '04'   " & _
                "and strReportedDeviations = 'False' " & _
                "and strDeviationsUnReported = 'False' " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCDeviationsReportedIncorrectly()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
                "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCsHistory    " & _
                "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
                "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCsHistory.strTrackingNumber  " & _
                "and strSubmittalNumber = '1'  " & _
                "and strEventType = '04'   " & _
                "and strReportedDeviations = 'False' " & _
                "and strDeviationsUnReported = 'True' " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCDeviationsInFinal()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
            "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastname||', '||strFirstName) as UserName,   " & _
            "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
            "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCs    " & _
            "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
            "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
            "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCs.strTrackingNumber  " & _
            "and strEventType = '04' " & _
            "and strReportedDeviations = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCDeviationsNotReported()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
            "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "AIRBRANCH.SSCPItemMaster.strTrackingNumber  " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
            "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCs    " & _
            "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
            "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
            "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCs.strTrackingNumber  " & _
            "and strEventType = '04'   " & _
            "and strDeviationsUnReported = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCEnforcementTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "AIRBRANCH.SSCPItemMaster.strTrackingNumber,  " & _
                "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber  " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
                "AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPACCs,  " & _
                "AIRBRANCH.SSCP_AuditedEnforcement   " & _
                "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
                "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingnumber = AIRBRANCH.SSCPACCs.strTrackingNumber  " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber  " & _
                "and strEventType = '04'   " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCCOTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
            "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "AIRBRANCH.SSCPItemMaster.strTrackingNumber,  " & _
            "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber  " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
            "AIRBRANCH.SSCPItemMaster,  " & _
            "AIRBRANCH.SSCP_AuditedEnforcement   " & _
            "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
            "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
            "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber  " & _
            "and strEventType = '04'   " & _
            "and datCOResolved is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCNOVTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
            "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "AIRBRANCH.SSCPItemMaster.strTrackingNumber,  " & _
            "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber  " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
            "AIRBRANCH.SSCPItemMaster,  " & _
            "AIRBRANCH.SSCP_AuditedEnforcement   " & _
            "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
            "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
             "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber  " & _
             "and strEventType = '04'   " & _
             "and datNFALetterSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewACCLONTaken()
        Try
            Dim ResponsibleStaff As String = ""

            For x As Integer = 0 To clbAdministrative.Items.Count - 1
                If clbAdministrative.GetItemChecked(x) = True Then
                    clbAdministrative.SelectedIndex = x
                    ResponsibleStaff = ResponsibleStaff & " strResponsibleStaff = '" & clbAdministrative.SelectedValue & "' Or "
                End If
            Next
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
            SQL = "select " & _
            "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "AIRBRANCH.SSCPItemMaster.strTrackingNumber,  " & _
            "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber  " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,   " & _
            "AIRBRANCH.SSCPItemMaster,   " & _
            "AIRBRANCH.SSCP_AuditedEnforcement   " & _
            "where AIRBRANCH.SSCPItemMaster.strAirsnumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber   " & _
            "and AIRBRANCH.SSCPItemMaster.strResponsibleStaff = AIRBRANCH.EPDUserProfiles.numUserID    " & _
            "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCP_AuditedEnforcement.strTrackingNumber  " & _
             "and strEventType = '04'   " & _
            "and datLONSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatisticalReports.DataSource = dsStatisticalReport
            dgvStatisticalReports.DataMember = "TotalFacilities"
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "ACC Views"
    Private Sub llbViewACCTotalAssigned_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewACCTotalAssigned.LinkClicked
        Try

            ViewACCTotalAssigned()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCReporting_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCReporting.LinkClicked
        Try

            ViewACCReporting()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCRequiringResubmittal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCRequiringResubmittal.LinkClicked
        Try

            ViewACCRequiringResubmittal()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCSubmittedLate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCSubmittedLate.LinkClicked
        Try

            ViewACCSubmittedLate()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReported.LinkClicked
        Try

            ViewACCDeviationsReported()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsReportedCorrectly_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReportedCorrectly.LinkClicked
        Try

            ViewACCDeviationsReportedCorrectly()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsIncorrectlyReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsIncorrectlyReported.LinkClicked
        Try

            ViewACCDeviationsReportedIncorrectly()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsInFinal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsInFinal.LinkClicked
        Try

            ViewACCDeviationsInFinal()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsNotReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsNotReported.LinkClicked
        Try

            ViewACCDeviationsNotReported()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCEnforcementTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCEnforcementTaken.LinkClicked
        Try

            ViewACCEnforcementTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCCOTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCCOTaken.LinkClicked
        Try

            ViewACCCOTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCNOVTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCNOVTaken.LinkClicked
        Try

            ViewACCNOVTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCLONTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCLONTaken.LinkClicked
        Try

            ViewACCLONTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

    Sub EnforcementTotals()
        Try
            SQL = "select " & _
            "'$'||to_number((CoPenalty + Stipulated), '99999999.99') as TotalPen  " & _
            "from  " & _
            "(select  " & _
            "sum(AIRBRANCH.SSCP_AuditedEnforcement.strCOPenaltyAmount) as COPenalty " & _
            "from AIRBRANCH.SSCP_AuditedEnforcement  " & _
            "where AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "') COPEn,  " & _
            "(select  " & _
            "sum(AIRBRANCH.SSCPEnforcementStipulated.strStipulatedPenalty) as Stipulated " & _
            "from AIRBRANCH.SSCPEnforcementStipulated, AIRBRANCH.SSCP_AuditedEnforcement  " & _
            "where AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber = AIRBRANCH.SSCPEnforcementStipulated.strEnforcementNumber  " & _
            "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "') StipPen "

            If chbUseEnforcementDateRange.Checked = True Then
                SQL = "select " & _
                "'$'||to_number((CoPenalty + Stipulated), '99999999.99') as TotalPen  " & _
                "from  " & _
                "(select  " & _
                "sum(AIRBRANCH.SSCP_AuditedEnforcement.strCOPenaltyAmount) as COPenalty " & _
                "from AIRBRANCH.SSCP_AuditedEnforcement  " & _
                "where  AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' " & _
                "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "') COPEn,  " & _
                "(select  " & _
                "sum(AIRBRANCH.SSCPEnforcementStipulated.strStipulatedPenalty) as Stipulated " & _
                "from AIRBRANCH.SSCPEnforcementStipulated, AIRBRANCH.SSCP_AuditedEnforcement  " & _
                "where AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber = AIRBRANCH.SSCPEnforcementStipulated.strEnforcementNumber  " & _
                 "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' " & _
                "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "') StipPen "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPenaltySummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPenaltySummary.Click
        Try
            If txtEnforcementAIRSNumber.Text <> "" Then
                EnforcementTotals()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewEnforcements_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewEnforcements.LinkClicked
        Try
            If txtEnforcementAIRSNumber.Text <> "" Then
                SQL = "select " & _
                "strFacilityName, " & _
                "substr(SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber, " & _
                "'$'||to_number(AIRBRANCH.SSCP_AuditedEnforcement.strCOPenaltyAmount, '99999999.99') as COPenalty, " & _
                "'$'||to_number(AIRBRANCH.SSCPEnforcementStipulated.strStipulatedPenalty, '99999999.99') as StipulatedPenalty, " & _
                "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " & _
                "from AIRBRANCH.SSCP_AuditedEnforcement, " & _
                "AIRBRANCH.SSCPEnforcementStipulated, AIRBRANCH.APBFacilityInformation  " & _
                "where AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber = AIRBRANCH.SSCPEnforcementStipulated.strEnforcementNumber " & _
                "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "

                If chbUseEnforcementDateRange.Checked = True Then
                    SQL = "select " & _
                    "strFacilityName, " & _
                    "substr(SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                    "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber, " & _
                    "'$'||to_number(AIRBRANCH.SSCP_AuditedEnforcement.strCOPenaltyAmount, '99999999.99') as COPenalty, " & _
                    "'$'||to_number(AIRBRANCH.SSCPEnforcementStipulated.strStipulatedPenalty, '99999999.99') as StipulatedPenalty, " & _
                    "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " & _
                    "from AIRBRANCH.SSCP_AuditedEnforcement, " & _
                    "AIRBRANCH.SSCPEnforcementStipulated, AIRBRANCH.APBFacilityInformation " & _
                    "where  AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber = AIRBRANCH.SSCPEnforcementStipulated.strEnforcementNumber " & _
                    "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "' " & _
                    "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "
                End If

                dsEnforcementPenalties = New DataSet
                daEnforcementPenalties = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daEnforcementPenalties.Fill(dsEnforcementPenalties, "EnforcementPenalties")
                dgvStatisticalReports.DataSource = dsEnforcementPenalties
                dgvStatisticalReports.DataMember = "EnforcementPenalties"
                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbUseEnforcementDateRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbUseEnforcementDateRange.CheckedChanged
        Try
            If chbUseEnforcementDateRange.Checked = True Then
                dtpEnforcementStartDate.Enabled = True
                dtpEnforcementEndDate.Enabled = True
            Else
                dtpEnforcementStartDate.Enabled = False
                dtpEnforcementEndDate.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvStatisticalReports_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvStatisticalReports.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbViewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles llbViewRecord.LinkClicked
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub OpenFacilitySummary()
        Try
            If Not DAL.Facility.AirsNumberExists(txtRecordNumber.Text) Then
                MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
                Exit Sub
            End If
            Dim parameters As New Generic.Dictionary(Of String, String)
            parameters("airsnumber") = txtRecordNumber.Text
            OpenSingleForm(IAIPFacilitySummary, parameters:=parameters, closeFirst:=True)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub OpenEnforcement()
        Try

            Dim enfNum As String = txtRecordNumber.Text
            If enfNum = "" Then Exit Sub
            If DAL.SSCP.EnforcementExists(enfNum) Then
                OpenMultiForm("SscpEnforcement", enfNum)
            Else
                MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub OpenSSCPWork()
        Try

            If txtRecordNumber.Text <> "" And IsNumeric(txtRecordNumber.Text) Then
                SQL = "Select " & _
                "strTrackingNumber " & _
                "from AIRBRANCH.SSCPItemMaster " & _
                "where strTrackingNumber = '" & txtRecordNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    If SSCPReports Is Nothing Then
                        SSCPReports = Nothing
                        If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                        SSCPReports.txtTrackingNumber.Text = txtRecordNumber.Text
                        SSCPReports.Show()
                    Else
                        SSCPReports.txtTrackingNumber.Text = txtRecordNumber.Text
                        SSCPReports.Show()
                    End If
                Else
                    MsgBox("Tracking Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
                End If
            Else
                MsgBox("Tracking Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub dgvCMSUniverse_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvCMSUniverse.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewWatchListFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewWatchListFacilities.Click
        Try
            Dim ComplianceWhere As String = "and strComplianceStatus = '0' "

            If rdbAllNegativeStatus.Checked = True Then
                ComplianceWhere = " and (strComplianceStatus = 'B' or strComplianceStatus = '1' " & _
                "or strComplianceStatus = '6' or strComplianceStatus = 'W' " & _
                "or strComplianceStatus = '0' ) "
            End If
            If rdbInViolationProceduralEmissions.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = 'B' "
            End If
            If rdbInViolationNoSchedule.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '1' "
            End If
            If rdbInViolationNotMeetingSchedule.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '6' "
            End If
            If rdbInViolationProcedural.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = 'W' "
            End If
            If rdbUnknownCompliance.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '0' "
            End If
            If rdbMeetingCompliance.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '5' "
            End If
            If rdbNoApplicableStateReg.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '8' "
            End If
            If rdbInComplianceSourceTest.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '2' "
            End If
            If rdbInComplianceInspection.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '3' "
            End If
            If rdbInComplianceCertification.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '4' "
            End If
            If rdbInComplianceShutDown.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = '9' "
            End If
            If rdbInComplianceProcedural.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = 'C' "
            End If
            If rdbInComplianceCEMSData.Checked = True Then
                ComplianceWhere = " and strComplianceStatus = 'M' "
            End If

            SQL = "select " & _
            "distinct(substr(AIRBRANCH.APBAirProgramPollutants.strAIRSNumber, 5)) as AIRSNumber, " & _
            "strFacilityName, strPollutantDescription, " & _
            "(strComplianceStatus||' - '||strComplianceDesc) as ComplianceStatus " & _
            "from AIRBRANCH.APBAirProgramPollutants, AIRBRANCH.APBFacilityInformation, " & _
            "AIRBRANCH.LookUpPollutants, AIRBRANCH.LookUpComplianceStatus  " & _
            "where AIRBRANCH.APBAirProgramPollutants.strAIRSnumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBRANCH.APBAirProgramPollutants.strPollutantKey = AIRBRANCH.LookUpPollutants.strPollutantCode " & _
            "and AIRBRANCH.APBAirProgramPollutants.strComplianceStatus = AIRBRANCH.LookupComplianceStatus.strComplianceCode  " & _
               ComplianceWhere

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "ComplianceStatus")
            dgvWatchList.DataSource = ds
            dgvWatchList.DataMember = "ComplianceStatus"

            dgvWatchList.RowHeadersVisible = False
            dgvWatchList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWatchList.AllowUserToResizeColumns = True
            dgvWatchList.AllowUserToAddRows = False
            dgvWatchList.AllowUserToDeleteRows = False
            dgvWatchList.AllowUserToOrderColumns = True
            dgvWatchList.AllowUserToResizeRows = True

            dgvWatchList.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWatchList.Columns("AIRSNumber").DisplayIndex = 0
            dgvWatchList.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWatchList.Columns("strFacilityName").DisplayIndex = 1
            dgvWatchList.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgvWatchList.Columns("strPollutantDescription").DisplayIndex = 2
            dgvWatchList.Columns("ComplianceStatus").HeaderText = "Compliance Status"
            dgvWatchList.Columns("ComplianceStatus").DisplayIndex = 3

            lblWatchListCount.Text = "Count: " & dgvWatchList.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFacilitySearch(ByVal Location As String)
        Try
            Dim SQLLine1 As String = " "
            Dim SQLLine2 As String = " "
            Dim SQLOrder1 As String = " "
            Dim SQLOrder2 As String = " "
            ' Dim dtEngineers As New DataTable
            Dim SQLLine As String = ""
            Dim TotalText As String = ""

            If chbIgnoreFiscalYear.Checked = True Then
                SQL = "Select " & _
                "substr(AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                "strFacilityCity, " & _
                "strCMSMember, " & _
                "strClass, strOperationalStatus, " & _
                "LastInspection," & _
                "LastFCE, " & _
                "(strLastName||', '||strFirstName) as SSCPEngineer, " & _
                "strUnitDesc," & _
                " strDistrictResponsible, " & _
                "strCountyName " & _
                "from AIRBRANCH.VW_SSCP_MT_FacilityAssignment, " & _
                "AIRBRANCH.EPDUserProfiles, " & _
                " AIRBRANCH.SSCPInspectionsRequired, " & _
                "AIRBRANCH.LookUpEPDUnits, " & _
                "(select " & _
                "max(intYear) as MaxYear, AIRBRANCH.SSCPInspectionsRequired.strairsnumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "group by AIRBRANCH.SSCPInspectionsRequired.strAIRSNumber) MaxResults " & _
              " where AIRBRANCH.SSCPInspectionsRequired.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
              "and AIRBRANCH.SSCPInspectionsRequired.numSSCPUnit = AIRBRANCH.LookUpEPDunits.numUnitCode (+) " & _
              " and AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strairsnumber = AIRBRANCH.sscpinspectionsrequired.strairsnumber (+) " & _
              "and AIRBRANCH.SSCPInspectionsRequired.strAIRSNumber = MaxResults.strAIRSNumber " & _
              "and AIRBRANCH.SSCPInspectionsRequired.intYear = MaxResults.maxYear "

            Else
                SQL = "Select " & _
              "substr(AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
              "strFacilityCity, " & _
              "strCMSMember, " & _
              " strClass, strOperationalStatus, " & _
              " case " & _
              " when strInspectionRequired = 'True' then 'True' " & _
              " when strinspectionrequired = 'False' then 'False' " & _
              " when strInspectionRequired is null then 'False' " & _
              " end InspectionRequired, " & _
              " LastInspection," & _
              "   case " & _
              " when strFCERequired = 'True' then 'True' " & _
              " when strFCERequired = 'False' then 'False' " & _
              " when strFCERequired is null then 'False' " & _
              " end FCERequired, " & _
              "   LastFCE, " & _
              "(strLastName||', '||strFirstName) as SSCPEngineer, " & _
              "  strUnitDesc," & _
              "   strDistrictResponsible, " & _
              "  strCountyName " & _
              " from AIRBRANCH.VW_SSCP_MT_FacilityAssignment, " & _
              "AIRBRANCH.EPDUserProfiles, " & _
              " AIRBRANCH.SSCPInspectionsRequired, " & _
              "AIRBRANCH.LookUpEPDUnits " & _
              " where AIRBRANCH.SSCPInspectionsRequired.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
              "and AIRBRANCH.SSCPInspectionsRequired.numSSCPUnit = AIRBRANCH.LookUpEPDunits.numUnitCode (+) " & _
              " and AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strairsnumber = AIRBRANCH.sscpinspectionsrequired.strairsnumber (+) " & _
              " and AIRBRANCH.sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "
            End If

            SQLLine1 = " "
            SQLLine2 = " "
            SQLOrder1 = " "
            SQLOrder2 = " "
            If Location = "Filter" Then
                If cboFacSearch1.Items.Contains(cboFacSearch1.Text) And cboFilterEngineer1.Text <> "" Then
                    Select Case cboFacSearch1.Text
                        Case "AIRS Number"
                            SQLLine1 = " upper(AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strAIRSNumber) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
                        Case "City"
                            SQLLine1 = " upper(strFacilityCity) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
                        Case "Classification"
                            SQLLine1 = " upper(strClass) like '%" & Replace(cboClassFilter1.Text.ToUpper, "'", "''") & "%' "
                        Case "CMS Status"
                            SQLLine1 = " Upper(strCMSMember) like '%" & Replace(cboCMSMemberFilter1.Text.ToUpper, "'", "''") & "%' "
                        Case "County"
                            SQLLine1 = " upper(strCountyName) like '%" & Replace(cboCountyFilter1.Text.ToUpper, "'", "''") & "%' "
                        Case "District"
                            SQLLine1 = " upper(strDistrictName) like '%" & Replace(cboDistrictFilter1.Text.ToUpper, "'", "''") & "%' "
                        Case "District Engineer"
                            SQLLine1 = " strDistrictEngineer = '" & cboFilterEngineer1.SelectedValue & "' "
                        Case "District Responsible"
                            If rdbDistResp1True.Checked = True Then
                                SQLLine1 = " strDistrictResponsible = 'True' "
                            Else
                                SQLLine1 = " strDistrictResponsible = 'False' "
                            End If
                        Case "Engineer"
                            SQLLine1 = " numSSCPEngineer = '" & cboFilterEngineer1.SelectedValue & "' "
                        Case "Facility Name"
                            SQLLine1 = " upper(strFacilityName) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
                        Case "Unassigned Facilities"
                            SQLLine1 = " numSSCPEngineer is null "
                        Case "Operational Status"
                            SQLLine1 = " upper(strOperationalStatus) like '%" & Replace(cboOpStatus1.Text.ToUpper, "'", "''") & "%' "
                        Case "SIC Codes"
                            SQLLine1 = " upper(strSICCode) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
                        Case "SSCP Unit"
                            SQLLine1 = " upper(strUnitDesc) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
                            SQLLine1 = " upper(strUnitDesc) like '%" & Replace(cboSSCPUnitFilter.Text.ToUpper, "'", "''") & "%' "
                        Case Else
                            SQLLine1 = " "
                    End Select
                End If

                If cboFacSearch2.Items.Contains(cboFacSearch2.Text) And cboFilterEngineer2.Text <> "" Then
                    Select Case cboFacSearch2.Text
                        Case "AIRS Number"
                            SQLLine2 = " upper(AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strAIRSNumber) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
                        Case "City"
                            SQLLine2 = " upper(strFacilityCity) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
                        Case "Classification"
                            SQLLine2 = " upper(strClass) like '%" & Replace(cboClassFilter2.Text.ToUpper, "'", "''") & "%' "
                        Case "CMS Status"
                            SQLLine2 = " Upper(strCMSMember) like '%" & Replace(cboCMSMemberFilter2.Text.ToUpper, "'", "''") & "%' "
                        Case "County"
                            SQLLine2 = " upper(strCountyName) like '%" & Replace(cboCountyFilter2.Text.ToUpper, "'", "''") & "%' "
                        Case "District"
                            SQLLine2 = " upper(strDistrictName) like '%" & Replace(cboDistrictFilter2.Text.ToUpper, "'", "''") & "%' "
                        Case "District Engineer"
                            SQLLine2 = " strDistrictEngineer = '" & cboFilterEngineer2.SelectedValue & "' "
                        Case "District Responsible"
                            If rdbDistResp2True.Checked = True Then
                                SQLLine2 = " strDistrictResponsible = 'True' "
                            Else
                                SQLLine2 = " strDistrictResponsible = 'False' "
                            End If
                        Case "Engineer"
                            SQLLine2 = " numSSCPEngineer = '" & cboFilterEngineer2.SelectedValue & "' "
                        Case "Facility Name"
                            SQLLine2 = " upper(strFacilityName) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
                        Case "Unassigned Facilities"
                            SQLLine2 = " numSSCPEngineer is null "
                        Case "Operational Status"
                            SQLLine2 = " upper(strOperationalStatus) like '%" & Replace(cboOpStatus2.Text.ToUpper, "'", "''") & "%' "
                        Case "SIC Codes"
                            SQLLine2 = " upper(strSICCode) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
                        Case "SSCP Unit"
                            SQLLine2 = " upper(strUnitDesc) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
                            SQLLine2 = " upper(strUnitDesc) like '%" & Replace(cboSSCPUnitFilter2.Text.ToUpper, "'", "''") & "%' "
                        Case Else
                            SQLLine2 = " "
                    End Select
                End If

                If cboSort1.Items.Contains(cboSort1.Text) And cboSort1.SelectedIndex <> 0 Then
                    Select Case cboSort1.Text
                        Case "AIRS Number"
                            SQLOrder1 = " strAIRSNumber"
                        Case "City"
                            SQLOrder1 = " strFacilityCity"
                        Case "Classification"
                            SQLOrder1 = " strClass"
                        Case "County"
                            SQLOrder1 = " strCountyName"
                        Case "District"
                            SQLOrder1 = " strDistrictName"
                        Case "District Engineer"
                            SQLOrder1 = " strDistrictEngineer"
                        Case "District Responsible"
                            SQLOrder1 = " strDistrictResponsible"
                        Case "Engineer"
                            SQLOrder1 = " numSSCPEngineer"
                        Case "Facility Name"
                            SQLOrder1 = " strFacilityName"
                        Case "Operational Status"
                            SQLOrder1 = " strOperationalStatus"
                        Case "SIC Codes"
                            SQLOrder1 = " strSICCode"
                        Case "SSCP Unit"
                            SQLOrder1 = " strUnitDesc"
                        Case Else
                            SQLOrder1 = " "
                    End Select
                    If SQLOrder1 <> " " Then
                        If cboSortOrder1.Text = "Ascending Order" Then
                            SQLOrder1 = SQLOrder1 & " asc "
                        Else
                            SQLOrder1 = SQLOrder1 & " desc "
                        End If
                    End If
                End If

                If cboSort2.Items.Contains(cboSort2.Text) And cboSort2.SelectedIndex <> 0 Then
                    Select Case cboSort2.Text
                        Case "AIRS Number"
                            SQLOrder2 = " strAIRSNumber"
                        Case "City"
                            SQLOrder2 = " strFacilityCity"
                        Case "Classification"
                            SQLOrder2 = " strClass"
                        Case "County"
                            SQLOrder2 = " strCountyName"
                        Case "District"
                            SQLOrder2 = " strDistrictName"
                        Case "District Engineer"
                            SQLOrder2 = " strDistrictEngineer"
                        Case "District Responsible"
                            SQLOrder2 = " strDistrictResponsible"
                        Case "Engineer"
                            SQLOrder2 = " numSSCPEngineer"
                        Case "Facility Name"
                            SQLOrder2 = " strFacilityName"
                        Case "Operational Status"
                            SQLOrder2 = " strOperationalStatus"
                        Case "SIC Codes"
                            SQLOrder2 = " strSICCode"
                        Case "SSCP Unit"
                            SQLOrder2 = " strUnitDesc"
                        Case Else
                            SQLOrder2 = " "
                    End Select
                    If SQLOrder2 <> " " Then
                        If cboSortOrder2.Text = "Ascending Order" Then
                            SQLOrder2 = SQLOrder2 & " asc "
                        Else
                            SQLOrder2 = SQLOrder2 & " desc "
                        End If
                    End If
                End If

                If SQLLine1 <> " " Then
                    SQL = SQL & " and " & SQLLine1
                End If
                If SQLLine2 <> " " Then
                    SQL = SQL & " and " & SQLLine2
                End If

                If SQLOrder1 <> " " Then
                    SQL = SQL & " Order by " & SQLOrder1
                    If SQLOrder2 <> " " Then
                        SQL = SQL & " , " & SQLLine2
                    End If
                Else
                    If SQLOrder2 <> " " Then
                        SQL = SQL & " Order by " & SQLOrder2
                    End If
                End If

            Else
                TotalText = txtManualAIRSNumber.Text
                SQLLine = ""

                Do While TotalText <> ""
                    SQLLine = SQLLine & " AIRBRANCH.VW_SSCP_MT_FacilityAssignment.strairsnumber = '0413" & Mid(TotalText, 1, 8) & "' or "
                    If TotalText.Length > 10 Then
                        TotalText = Microsoft.VisualBasic.Right(TotalText, TotalText.Length - 10)
                    Else
                        TotalText = ""
                    End If
                Loop

                If SQLLine <> "" Then
                    SQL = SQL & " and ( " & Mid(SQLLine, 1, SQLLine.Length - 4) & " ) "
                End If
            End If

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "FacilitySearch")
            dgvFilteredFacilityList.DataSource = ds
            dgvFilteredFacilityList.DataMember = "FacilitySearch"

            dgvFilteredFacilityList.RowHeadersVisible = False
            dgvFilteredFacilityList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFilteredFacilityList.AllowUserToResizeColumns = True
            dgvFilteredFacilityList.AllowUserToAddRows = False
            dgvFilteredFacilityList.AllowUserToDeleteRows = False
            dgvFilteredFacilityList.AllowUserToOrderColumns = True
            dgvFilteredFacilityList.AllowUserToResizeRows = True

            'If chbViewAllFields.Checked = True Then
            '    dgvFilteredFacilityList.Columns("AIRSNumber").HeaderText = "AIRS #"
            '    dgvFilteredFacilityList.Columns("AIRSNumber").DisplayIndex = 0
            '    dgvFilteredFacilityList.Columns("strFacilityName").HeaderText = "Facility Name"
            '    dgvFilteredFacilityList.Columns("strFacilityName").DisplayIndex = 1
            '    dgvFilteredFacilityList.Columns("strFacilityCity").HeaderText = "City"
            '    dgvFilteredFacilityList.Columns("strFacilityCity").DisplayIndex = 2
            '    dgvFilteredFacilityList.Columns("strCountyName").HeaderText = "County"
            '    dgvFilteredFacilityList.Columns("strCountyName").DisplayIndex = 3
            '    dgvFilteredFacilityList.Columns("strDistrictName").HeaderText = "District"
            '    dgvFilteredFacilityList.Columns("strDistrictName").DisplayIndex = 4
            '    dgvFilteredFacilityList.Columns("strClass").HeaderText = "Classification"
            '    dgvFilteredFacilityList.Columns("strClass").DisplayIndex = 5
            '    dgvFilteredFacilityList.Columns("strOperationalStatus").HeaderText = "Operational Status"
            '    dgvFilteredFacilityList.Columns("strOperationalStatus").DisplayIndex = 7
            '    dgvFilteredFacilityList.Columns("strCMSMember").HeaderText = "CMS Status"
            '    dgvFilteredFacilityList.Columns("strCMSMember").DisplayIndex = 6
            '    dgvFilteredFacilityList.Columns("strSICCode").HeaderText = "SIC Code"
            '    dgvFilteredFacilityList.Columns("strSICCode").DisplayIndex = 8
            '    dgvFilteredFacilityList.Columns("strUnitDesc").HeaderText = "SSCP Title"
            '    dgvFilteredFacilityList.Columns("strUnitDesc").DisplayIndex = 9
            '    dgvFilteredFacilityList.Columns("SSCPEngineer").HeaderText = "SSCP Engineer"
            '    dgvFilteredFacilityList.Columns("SSCPEngineer").DisplayIndex = 10
            '    dgvFilteredFacilityList.Columns("strDistrictResponsible").HeaderText = "District Responsible"
            '    dgvFilteredFacilityList.Columns("strDistrictResponsible").DisplayIndex = 11
            '    dgvFilteredFacilityList.Columns("LastFCE").HeaderText = "Last FCE"
            '    dgvFilteredFacilityList.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
            '    dgvFilteredFacilityList.Columns("LastFCE").DisplayIndex = 12
            '    dgvFilteredFacilityList.Columns("LastInspectionDate").HeaderText = "Last Inspection"
            '    dgvFilteredFacilityList.Columns("LastInspectionDate").DisplayIndex = 13
            '    dgvFilteredFacilityList.Columns("LastInspectionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            '    dgvFilteredFacilityList.Columns("strInspectionRequired").HeaderText = "Inspection Required"
            '    dgvFilteredFacilityList.Columns("strInspectionRequired").DisplayIndex = 14
            'Else
            'End If

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
            ErrorReport(SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFacilitySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacilitySearch.Click
        Try
            LoadFacilitySearch("Filter")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboFacSearch1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFacSearch1.SelectedValueChanged
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
                Case "Unassigned Facilities"

                Case Else
                    txtFacSearch1.Visible = True
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboFacSearch2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFacSearch2.SelectedValueChanged
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
                Case "Unassigned Facilities"

                Case Else
                    txtFacSearch2.Visible = True
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFacility.Click
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

                    'If chbViewAllFields.Checked = True Then
                    '    If IsDBNull(dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(2).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    '    If IsDBNull(dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(3).Value = dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    '    If IsDBNull(dgvFilteredFacilityList(12, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(4).Value = dgvFilteredFacilityList(12, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    '    If IsDBNull(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(5).Value = dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    '    If IsDBNull(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(6).Value = dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If

                    '    If IsDBNull(dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(7).Value = dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    '    If IsDBNull(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(8).Value = dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    '    If IsDBNull(dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                    '    Else
                    '        dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value
                    '    End If
                    'Else

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
                'dgvRow.CreateCells(dgvSelectedFacilityList)
                'dgvRow.Cells(0).Value = dgvFilteredFacilityList(0, dgvFilteredFacilityList.CurrentRow.Index).Value
                'dgvRow.Cells(1).Value = dgvFilteredFacilityList(1, dgvFilteredFacilityList.CurrentRow.Index).Value

                'If chbViewAllFields.Checked = True Then
                '    dgvRow.Cells(2).Value = dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    dgvRow.Cells(3).Value = dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    dgvRow.Cells(4).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    If IsDBNull(dgvFilteredFacilityList(13, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                '    Else
                '        dgvRow.Cells(5).Value = dgvFilteredFacilityList(13, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    End If
                '    If IsDBNull(dgvFilteredFacilityList(14, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                '    Else
                '        dgvRow.Cells(6).Value = dgvFilteredFacilityList(14, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    End If
                'Else
                '    dgvRow.Cells(2).Value = dgvFilteredFacilityList(2, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    dgvRow.Cells(3).Value = dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    dgvRow.Cells(4).Value = dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    If IsDBNull(dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                '    Else
                '        dgvRow.Cells(5).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    End If
                '    If IsDBNull(dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                '    Else
                '        dgvRow.Cells(6).Value = dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value
                '    End If
                'End If
                'dgvSelectedFacilityList.Rows.Add(dgvRow)
            End If

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectAllFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllFacilities.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0

            dgvSelectedFacilityList.Rows.Clear()

            For i = 0 To dgvFilteredFacilityList.Rows.Count - 1
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSelectedFacilityList)
                dgvRow.Cells(0).Value = dgvFilteredFacilityList(0, i).Value
                dgvRow.Cells(1).Value = dgvFilteredFacilityList(1, i).Value

                'If chbViewAllFields.Checked = True Then
                '    dgvRow.Cells(2).Value = dgvFilteredFacilityList(9, i).Value
                '    dgvRow.Cells(3).Value = dgvFilteredFacilityList(7, i).Value
                '    dgvRow.Cells(4).Value = dgvFilteredFacilityList(10, i).Value
                '    dgvRow.Cells(5).Value = dgvFilteredFacilityList(13, i).Value
                '    dgvRow.Cells(6).Value = dgvFilteredFacilityList(14, i).Value
                'Else

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnselectFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectFacility.Click
        Try
            If dgvSelectedFacilityList.Rows.Count > 0 Then
                dgvSelectedFacilityList.Rows.Remove(dgvSelectedFacilityList.CurrentRow)
            End If

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnselectAllFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAllFacilities.Click
        Try

            dgvSelectedFacilityList.Rows.Clear()

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearManualAIRSNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearManualAIRSNum.Click
        Try

            txtManualAIRSNumber.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFilterManualAIRSList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterManualAIRSList.Click
        Try
            LoadFacilitySearch("Manual")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEngineerResponsibility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEngineerResponsibility.Click
        Try
            Dim AIRSNum As String = ""
            Dim Eng As String = ""
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = ""
                Eng = ""

                AIRSNum = dgvSelectedFacilityList(0, i).Value
                Eng = cboSSCPEngineer.SelectedValue

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & Eng & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and AIRBRANCH.sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, strAssigningManager, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & Eng & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(2, i).Value = cboSSCPEngineer.Text
            Next

            MsgBox("Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveSSCPUnitAssignment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSSCPUnitAssignment.Click
        Try
            Dim AIRSNum As String = ""
            'Dim SSCPUnit As String = ""

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "numSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "'" & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPUnit, strAssigningManager, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & cboSSCPUnit2.SelectedValue & "', " & _
                    "'" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(3, i).Value = cboSSCPUnit2.Text
            Next

            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveDistResponsible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDistResponsible.Click
        Try
            Dim AIRSNum As String = ""
            Dim DistResp As String = ""

            If rdbDistResponsibleTrue.Checked = True Then
                DistResp = "True"
            Else
                DistResp = "False"
            End If

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPDistrictResponsible set " & _
                    "strDistrictResponsible = '" & DistResp & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPDistrictResponsible " & _
                    "values " & _
                    "('0413" & AIRSNum & ", '" & DistResp & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(4, i).Value = DistResp
            Next
            MsgBox("District Responsibilitie(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnSaveInspectionReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveInspectionReq.Click
        Try

            Dim AIRSNum As String = ""
            Dim InspectionRequired As String = ""
            Dim InspectionNote As String = ""

            If rdbInspectionRequired.Checked = True Then
                InspectionRequired = "True"
                InspectionNote = "FY-" & Mid(cboFiscalYear.Text, 3)
            Else
                InspectionRequired = "False"
                InspectionNote = ""
            End If
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "strInspectionRequired = '" & InspectionRequired & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "strInspectionRequired, strAssigningManager, datAssigningDate) " & _
                    "values " & _
                    "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & InspectionRequired & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(5, i).Value = InspectionNote
            Next

            MsgBox("Inspection(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveFCEReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFCEReq.Click
        Try
            Dim AIRSNum As String = ""
            Dim FCERequired As String = ""
            Dim FCENote As String = ""

            If rdbFCERequired.Checked = True Then
                FCERequired = "True"
                FCENote = "FY-" & Mid(cboFiscalYear.Text, 3)
            Else
                FCERequired = "False"
                FCENote = ""
            End If

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "strFCERequired = '" & FCERequired & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "strFCERequired, strAssigningManager, datAssigningDate) " & _
                   "values " & _
                   "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                   "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                   "'" & FCERequired & "', '" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(7, i).Value = FCENote
            Next

            MsgBox("FCE(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveAllSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAllSettings.Click
        Try
            Dim AIRSNum As String = ""
            Dim Eng As String = ""
            Dim DistResp As String = ""
            Dim InspectionRequired As String = ""
            Dim FCERequired As String = ""

            Eng = cboSSCPEngineer.SelectedValue

            If rdbDistResponsibleTrue.Checked = True Then
                DistResp = "True"
            Else
                DistResp = "False"
            End If

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If rdbInspectionRequired.Checked = True Then
                InspectionRequired = "True"
            Else
                InspectionRequired = "False"
            End If
            If rdbFCERequired.Checked = True Then
                FCERequired = "True"
            Else
                FCERequired = "False"
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & Eng & "', " & _
                    "numSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " & _
                    "strInspectionRequired = '" & InspectionRequired & "', " & _
                    "strFCERequired = '" & FCERequired & "', " & _
                    "STRASSIGNINGMANAGER = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intyear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, numSSCPUnit, " & _
                    "strInspectionRequired, strFCERequired, " & _
                    "STRASSIGNINGMANAGER, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & Eng & "', '" & cboSSCPUnit2.SelectedValue & "', " & _
                    "'" & InspectionRequired & "', '" & FCERequired & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPDistrictResponsible set " & _
                    "strDistrictResponsible = '" & DistResp & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPDistrictResponsible " & _
                    "values " & _
                    "('0413" & AIRSNum & ", '" & DistResp & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(4, i).Value = DistResp
                dgvSelectedFacilityList(2, i).Value = cboSSCPEngineer.Text
                dgvSelectedFacilityList(3, i).Value = cboSSCPUnit2.Text
                dgvSelectedFacilityList(5, i).Value = InspectionRequired
                dgvSelectedFacilityList(7, i).Value = FCERequired
            Next

            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnForceAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForceAIRSNumber.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0
            Dim FacilityName As String = ""

            SQL = "Select strAIRSNumber " & _
            "From AIRBRANCH.APBMasterAIRS " & _
            "where strAIRSNumber = '0413" & mtbForcedAIRS.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Select " & _
                "strFacilityName " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbForcedAIRS.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr.Item("strFacilityName")
                    End If
                End While
                dr.Close()

                i = dgvSelectedFacilityList.Rows.Count

                If i > 0 Then
                    temp = mtbForcedAIRS.Text
                    For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                        If dgvSelectedFacilityList(0, i).Value = temp Then
                            temp2 = "Ignore"
                        End If
                    Next
                    If temp2 <> "Ignore" Then
                        dgvRow.CreateCells(dgvSelectedFacilityList)
                        dgvRow.Cells(0).Value = mtbForcedAIRS.Text
                        dgvRow.Cells(1).Value = FacilityName
                        dgvRow.Cells(2).Value = ""
                        dgvRow.Cells(3).Value = ""
                        dgvRow.Cells(4).Value = ""
                        dgvRow.Cells(5).Value = ""

                        dgvSelectedFacilityList.Rows.Add(dgvRow)
                    End If
                Else
                    dgvRow.CreateCells(dgvSelectedFacilityList)
                    dgvRow.Cells(0).Value = mtbForcedAIRS.Text
                    dgvRow.Cells(1).Value = FacilityName
                    dgvRow.Cells(2).Value = ""
                    dgvRow.Cells(3).Value = ""
                    dgvRow.Cells(4).Value = ""
                    dgvRow.Cells(5).Value = ""

                    dgvSelectedFacilityList.Rows.Add(dgvRow)
                End If

                lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString
            Else
                MsgBox("AIRS # does not exist in the database.", MsgBoxStyle.Exclamation, "SSCP Managers Tools")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearEngineerAssignment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearEngineerAssignment.Click
        Try
            Dim AIRSNum As String = ""
            ' Dim Eng As String = ""

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = ""
                '  Eng = ""

                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, strAssigningManager, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'', '" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(2, i).Value = ""
            Next
            MsgBox("Assignment(s) Cleared", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearSSCPUnitAssignment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSSCPUnitAssignment.Click
        Try
            Dim AIRSNum As String = ""
            ' Dim SSCPUnit As String = ""

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "numSSCPUnit = '', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                  "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                  "where strAIRSNumber = '0413" & AIRSNum & "'" & _
                  "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.SSCPInspectionsRequired " & _
                 "(numKey, strAIRSNumber, intYear, " & _
                 "numSSCPUnit, strAssigningManager, DATASSIGNINGDATE) " & _
                 "values " & _
                 "((select max(numKey) + 1 from AIRBRANCH.SSCPInspectionsRequired), " & _
                 "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'', " & _
                    "'" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(3, i).Value = ""
            Next
            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveCMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCMS.Click
        Try
            Dim AIRSNum As String = ""
            Dim CMSStatus As String = ""

            If rdbCMS_A.Checked = True Then
                CMSStatus = "A"
            End If
            If rdbCMS_SM.Checked = True Then
                CMSStatus = "S"
            End If
            If rdbCMS_None.Checked = True Then
                CMSStatus = ""
            End If
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                "strCMSMember = '" & CMSStatus & "' " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(9, i).Value = CMSStatus
            Next

            For i As Integer = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(5, i).Value = CMSStatus
            Next
            MsgBox("Compliance Monitoring Strategy Updated", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboFiscalYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiscalYear.SelectedIndexChanged
        Try
            If cboFiscalYear.Items.Contains(cboFiscalYear.Text) Then
                Panel10.Enabled = True
                Panel9.Enabled = True
                Panel15.Enabled = True
            Else
                Panel10.Enabled = False
                Panel9.Enabled = False
                Panel15.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyYear.Click
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

            Dim confirmResult As DialogResult = _
                MessageBox.Show("Warning: This may take a VERY, VERY long time. The IAIP will be unresponsive until finished. " & _
                                "Are you sure you want to proceed?", "Confirm Patience", _
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            If DAL.SSCP.AssignmentYearExists(targetYear) Then
                If chbClearExistingData.Checked = True Then
                    Dim dialogResult As DialogResult = _
                        MessageBox.Show("Warning: This will delete all facility assignments for " & mtbNewYear.Text & _
                                        ". Are you sure you want to proceed?", "Confirm Delete", _
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    If dialogResult = Windows.Forms.DialogResult.Yes Then
                        Dim deleteResult As Boolean = DAL.SSCP.DeleteAssignmentYear(targetYear)
                        If Not deleteResult Then
                            MsgBox("There was an error when attempting to clear data from target year. " & _
                                   "Please check the value and try again." & vbCrLf & "No data altered.", _
                                   MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If
                    End If
                Else
                    Dim dialogResult As DialogResult = _
                        MessageBox.Show("Warning: This will merge data from " & oldYear & " into " & targetYear & _
                                        ". Are you sure you want to proceed?", "Confirm Merge", _
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    If dialogResult = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            End If

            Dim recordsAdded As Integer = DAL.SSCP.CopyAssignmentYear(oldYear, targetYear)
            If recordsAdded > 0 Then
                If Not cboExistingYears.Items.Contains(targetYear) Then
                    cboExistingYears.Items.Add(targetYear)
                End If

                If Not cboFiscalYear.Items.Contains(targetYear) Then
                    cboFiscalYear.Items.Add(mtbNewYear.Text)
                End If

                MsgBox(recordsAdded & " new records entered into " & targetYear, MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("There was an error adding the data to the target year. " & _
                       "Please check the value and try again.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Private Sub btnRunTitleVSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunTitleVSearch.Click
        Try
            SQL = "SELECT DISTINCT SUBSTR(FI.STRAIRSNUMBER, 5) AS AIRSNumber, " & _
            "  FI.STRFACILITYNAME, " & _
            "  HD.STROPERATIONALSTATUS, " & _
            "  (UP.STRLASTNAME " & _
            "  || ', ' " & _
            "  || UP.STRFIRSTNAME) AS StaffResponsible " & _
            "FROM AIRBRANCH.APBFacilityinformation FI, " & _
            "  AIRBRANCH.APBHeaderdata HD, " & _
            "  AIRBRANCH.EPDUserProfiles UP, " & _
            "  AIRBRANCH.SSCPINSPECTIONSREQUIRED IR, " & _
            "  (SELECT DISTINCT SUBSTR(tFI.STRAIRSNUMBER, 5) AS AIRSNumber " & _
            "  FROM AIRBRANCH.APBHeaderData tHD, " & _
            "    AIRBRANCH.APBFacilityInformation tFI, " & _
            "    AIRBRANCH.SSPPApplicationMaster tAM, " & _
            "    AIRBRANCH.SSPPApplicationData tAD, " & _
            "    AIRBRANCH.SSPPApplicationTracking tAT " & _
            "  WHERE tAM.STRAPPLICATIONNUMBER = tAD.STRAPPLICATIONNUMBER " & _
            "  AND tAM.STRAIRSNUMBER          = tHD.STRAIRSNUMBER " & _
            "  AND tAM.STRAIRSNUMBER          = tFI.STRAIRSNUMBER " & _
            "  AND tAM.STRAPPLICATIONNUMBER   = tAT.STRAPPLICATIONNUMBER " & _
            "  AND tAD.STRPERMITNUMBER LIKE '%V__0' " & _
            "  AND tHD.STROPERATIONALSTATUS             <> 'X' " & _
            "  AND SUBSTR(tHD.STRAIRPROGRAMCODES, 13, 1) = '1' " & _
            "  AND ((tAT.DATPERMITISSUED                IS NOT NULL " & _
            "  AND tAT.DATPERMITISSUED                   < add_months(SysDate, -51)) " & _
            "  OR (tAT.DATEFFECTIVE                     IS NOT NULL " & _
            "  AND tAT.DATEFFECTIVE                      < add_months(SysDate, -51))) " & _
            "  MINUS " & _
            "    (SELECT DISTINCT SUBSTR(tAM.STRAIRSNUMBER, 5) AS AIRSNumber " & _
            "    FROM AIRBRANCH.SSPPApplicationMaster tAM, " & _
            "      AIRBRANCH.SSPPApplicationData tAD, " & _
            "      AIRBRANCH.SSPPApplicationTracking tAT, " & _
            "      AIRBRANCH.APBHeaderData tHD " & _
            "    WHERE tAM.STRAPPLICATIONNUMBER = tAD.STRAPPLICATIONNUMBER " & _
            "    AND tAM.STRAPPLICATIONNUMBER   = tAT.STRAPPLICATIONNUMBER " & _
            "    AND tAM.STRAIRSNUMBER          = tHD.STRAIRSNUMBER " & _
            "    AND ((tAM.STRAPPLICATIONTYPE   = '14' " & _
            "    OR tAM.STRAPPLICATIONTYPE      = '16' " & _
            "    OR tAM.STRAPPLICATIONTYPE      = '27') " & _
            "    OR (tAD.STRPERMITNUMBER LIKE '%V__0')) " & _
            "    AND ((tAT.DATPERMITISSUED BETWEEN add_months(SysDate, -51) AND SysDate " & _
            "    AND tAT.DATEFFECTIVE BETWEEN add_months(SysDate,      -51) AND SysDate) " & _
            "    OR (tAT.DATRECEIVEDDATE BETWEEN add_months(SysDate,   -51) AND SysDate)) " & _
            "    ) " & _
            "  ) TVFacilities, " & _
            "  (SELECT MAX(SSCPINSPECTIONSREQUIRED.INTYEAR) AS maxyear " & _
            "  FROM AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
            "  ) maxyear " & _
            "WHERE FI.STRAIRSNUMBER = '0413' " & _
            "  || TVFacilities.AIRSNumber " & _
            "AND FI.STRAIRSNUMBER   = HD.STRAIRSNUMBER " & _
            "AND IR.NUMSSCPENGINEER = UP.NUMUSERID " & _
            "AND FI.STRAIRSNUMBER   = IR.STRAIRSNUMBER " & _
            "AND IR.INTYEAR         = maxyear.maxyear " & _
            "ORDER BY AIRSNumber"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnRunComplianceReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunComplianceReport.Click
        Try
            SQL = "select " & _
"airbranch.SSCP_AuditedEnforcement.strEnforcementnumber,  " & _
"substr(airbranch.apbheaderdata.strairsnumber,  5) as AIRSNumber,  " & _
"airbranch.APBFacilityInformation.strfacilityname,  " & _
"airbranch.apbheaderdata.strclass,  " & _
"case " & _
"when strDistrictResponsible = 'True' then 'District Responsible' " & _
"else 'SSCP Responsible' " & _
"end DistrictResponsible, " & _
"strActionType, datDiscoverydate,  " & _
"case  " & _
"when strActionType = 'LON' then datLONSent   " & _
"when strActionType = 'NOV' then datNOVSent   " & _
"when strActionType = 'NOVCO' then datCOProposed  " & _
"when strActionType = 'NOVCOP' then datCOProposed  " & _
"when strActionType = 'HPV' then datNOVSent   " & _
"when strActionType = 'HPVCO' then datNOVSent  " & _
"when strActionType = 'HPVCOP' then datNovsent   " & _
"when strActionType = 'HPVAO' then datNOVSent   " & _
"end DateIssued, " & _
"case  " & _
"when strActionType = 'LON' then datLONResolved   " & _
"when strActionType = 'NOV' then datNFALetterSent   " & _
"when strActionType = 'NOVCO' then datCOResolved  " & _
"when strActionType = 'NOVCOP' then datCOResolved   " & _
"when strActionType = 'HPV' then datNFALetterSent  " & _
"when strActionType = 'HPVCO' then datNFALetterSent  " & _
"when strActionType = 'HPVCOP' then datNFALetterSent  " & _
"when strActionType = 'HPVAO' then datNFALetterSent    " & _
"end DateResolved " & _
"from airbranch.apbheaderdata, airbranch.APBFacilityInformation,  " & _
"airbranch.SSCP_AuditedEnforcement, airbranch.SSCPDistrictResponsible " & _
"where airbranch.apbheaderdata.strairsnumber = airbranch.APBFacilityInformation.strairsnumber  " & _
"and airbranch.APBHeaderdata.strairsnumber = airbranch.SSCPDistrictResponsible.strAIRSnumber  " & _
"and AIRBranch.APBHeaderdata.strairsnumber = airbranch.SSCP_AuditedEnforcement.strAIRSnumber  " & _
"order by datDiscoverydate desc Nulls Last "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "MiscReport")
            dgvMiscReport.DataSource = ds
            dgvMiscReport.DataMember = "MiscReport"

            dgvMiscReport.RowHeadersVisible = False
            dgvMiscReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMiscReport.AllowUserToResizeColumns = True
            dgvMiscReport.AllowUserToAddRows = False
            dgvMiscReport.AllowUserToDeleteRows = False
            dgvMiscReport.AllowUserToOrderColumns = True
            dgvMiscReport.AllowUserToResizeRows = True

            dgvMiscReport.Columns("strEnforcementnumber").HeaderText = "Enforcement #"
            dgvMiscReport.Columns("strEnforcementnumber").DisplayIndex = 0

            dgvMiscReport.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvMiscReport.Columns("AIRSNumber").DisplayIndex = 1
            dgvMiscReport.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvMiscReport.Columns("strFacilityName").DisplayIndex = 2
            dgvMiscReport.Columns("strFacilityName").Width = 150
            dgvMiscReport.Columns("strclass").HeaderText = "Classification"
            dgvMiscReport.Columns("strclass").DisplayIndex = 3
            dgvMiscReport.Columns("DistrictResponsible").HeaderText = "Source Responsible"
            dgvMiscReport.Columns("DistrictResponsible").DisplayIndex = 4
            dgvMiscReport.Columns("strActionType").HeaderText = "Enforcement Type"
            dgvMiscReport.Columns("strActionType").DisplayIndex = 5

            dgvMiscReport.Columns("datDiscoverydate").HeaderText = "Discovery Date"
            dgvMiscReport.Columns("datDiscoverydate").DisplayIndex = 6
            dgvMiscReport.Columns("datDiscoverydate").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvMiscReport.Columns("DateIssued").HeaderText = "Date Issued"
            dgvMiscReport.Columns("DateIssued").DisplayIndex = 7
            dgvMiscReport.Columns("DateIssued").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvMiscReport.Columns("DateResolved").HeaderText = "Date Resolved"
            dgvMiscReport.Columns("DateResolved").DisplayIndex = 8
            dgvMiscReport.Columns("DateResolved").DefaultCellStyle.Format = "dd-MMM-yyyy"

            txtMiscReportCount.Text = dgvMiscReport.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub txtManualAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManualAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtManualAIRSNumber.SelectAll()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region "Export to Excel"

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        If dgvStatisticalReports.RowCount > 0 Then dgvStatisticalReports.ExportToExcel()
    End Sub

    Private Sub btnExportMiscToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportMiscToExcel.Click
        If dgvMiscReport.RowCount > 0 Then dgvMiscReport.ExportToExcel()
    End Sub

    Private Sub btnExportCmsWarningToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportCmsWarningToExcel.Click
        If dgvCMSWarning.RowCount > 0 Then dgvCMSWarning.ExportToExcel()
    End Sub

    Private Sub btnExportPollutantsToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPollutantsToExcel.Click
        If dgvPollutantFacilities.RowCount > 0 Then dgvPollutantFacilities.ExportToExcel()
    End Sub

    Private Sub btnExportWatchListToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportWatchListToExcel.Click
        If dgvWatchList.RowCount > 0 Then dgvWatchList.ExportToExcel()
    End Sub

    Private Sub btnExportCmsUniverseToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportCmsUniverseToExcel.Click
        If dgvCMSUniverse.RowCount > 0 Then dgvCMSUniverse.ExportToExcel()
    End Sub

    Private Sub btnExportFiltered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFiltered.Click
        If dgvFilteredFacilityList.RowCount > 0 Then dgvFilteredFacilityList.ExportToExcel()
    End Sub

    Private Sub btnExportSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportSelected.Click
        If dgvSelectedFacilityList.RowCount > 0 Then dgvSelectedFacilityList.ExportToExcel()
    End Sub

#End Region

#Region "Document Types"

    Private Sub TCManagerTools_Selected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles TCManagerTools.Selected
        If e.TabPage Is TPDocuments AndAlso dgvEnfDocumentTypes.RowCount = 0 Then
            LoadEnforcementDocumentTypes()
        End If
    End Sub

    Private enfDocumentTypesList As Generic.List(Of DocumentType)
    Private Sub LoadEnforcementDocumentTypes()
        ' Get list of various document types and bind that list to the datagridview
        enfDocumentTypesList = DAL.GetEnforcementDocumentTypes

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
            With .Columns("DocumentTypeId")
                .Visible = False
            End With
            With .Columns("Active")
                .HeaderText = "Active"
                .DisplayIndex = 0
            End With
            With .Columns("Ordinal")
                .HeaderText = "Pos."
                .DisplayIndex = 1
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns("DocumentType")
                .HeaderText = "Name"
                .DisplayIndex = 2
            End With
            'With .Columns("ActiveString")
            '    .Visible = False
            'End With
        End With
    End Sub

    Private Sub EnableEnfDocTypeUpdate()
        EnableDisableEnfDocTypeUpdate(True)
    End Sub

    Private Sub DisableEnfDocTypeUpdate()
        EnableDisableEnfDocTypeUpdate(False)
    End Sub

    Private Sub EnableDisableEnfDocTypeUpdate(ByVal enable As Boolean)
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

    Private Sub btnAddDocumentType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnAddDocumentType.Click

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

        Dim saved As Boolean = DAL.SaveEnforcementDocumentType(newEnfDocType, Me)

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

    Private Sub btnUpdateDocumentType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateDocumentType.Click
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

    Private Function EnforcementDocumentTypeFromFileListRow(ByVal row As DataGridViewRow) As DocumentType
        Dim d As New DocumentType
        With d
            .Active = row.Cells("Active").Value
            .DocumentType = row.Cells("DocumentType").Value
            .DocumentTypeId = row.Cells("DocumentTypeId").Value
            .Ordinal = row.Cells("Ordinal").Value
        End With
        Return d
    End Function

    Private Sub dgvEnfDocumentTypes_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvEnfDocumentTypes.SelectionChanged
        If dgvEnfDocumentTypes.SelectedRows.Count = 1 Then
            EnableEnfDocTypeUpdate()
        Else
            DisableEnfDocTypeUpdate()
        End If
    End Sub

    Private Sub dgvEnfDocumentTypes_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvEnfDocumentTypes.DataBindingComplete
        CType(sender, DataGridView).SanelyResizeColumns()
        CType(sender, DataGridView).ClearSelection()
    End Sub

#Region "Change Accept Button"

    Private Sub NoAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtNewName.Leave, txtUpdateName.Leave, mtxtNewPosition.Leave, mtxtUpdatePosition.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub NewEnfDocType_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtNewName.Enter, mtxtNewPosition.Enter
        Me.AcceptButton = btnAddDocumentType
    End Sub

    Private Sub UpdateEnfDocType_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtUpdateName.Enter, mtxtUpdatePosition.Enter
        Me.AcceptButton = btnUpdateDocumentType
    End Sub

#End Region

#End Region

End Class