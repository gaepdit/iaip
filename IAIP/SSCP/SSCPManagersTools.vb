Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
'Imports System.Text

Public Class SSCPManagersTools

    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As SqlCommand
    Dim dr, dr2, dr3 As SqlDataReader
    Dim recExist As Boolean

    Dim dsStaff As DataSet
    Dim daStaff As SqlDataAdapter
    Dim dsAssignStaff As DataSet
    Dim daAssignStaff As SqlDataAdapter
    Dim dsUnits As DataSet
    Dim daUnits As SqlDataAdapter
    Dim dsFilterUnits As DataSet
    Dim daFilterUnits As SqlDataAdapter
    Dim dsClassFilter As DataSet
    Dim daClassFilter As SqlDataAdapter
    Dim dsCMSMemberFilter As DataSet
    Dim daCMSMemberFilter As SqlDataAdapter
    Dim dsCountyFilter As DataSet
    Dim daCountyFilter As SqlDataAdapter
    Dim dsDistrictFilter As DataSet
    Dim daDistrictFilter As SqlDataAdapter

    Dim dsAdminStaff As DataSet
    Dim daAdminStaff As SqlDataAdapter
    Dim dsAirStaff As DataSet
    Dim daAirStaff As SqlDataAdapter
    Dim dsChemStaff As DataSet
    Dim daChemStaff As SqlDataAdapter
    Dim dsVOCStaff As DataSet
    Dim daVOCStaff As SqlDataAdapter
    Dim dsDistrictStaff As DataSet
    Dim daDistrictStaff As SqlDataAdapter
    Dim dsCMSDataSet As DataSet
    Dim daCMSDataSet As SqlDataAdapter
    Dim dsCMSWarningDataSet As DataSet
    Dim daCMSWarningDataSet As SqlDataAdapter
    Dim dsPollutantList As DataSet
    Dim daPollutantList As SqlDataAdapter
    Dim dsStatisticalReport As DataSet
    Dim daStatisticalReport As SqlDataAdapter
    Dim dsEnforcementPenalties As DataSet
    Dim daEnforcementPenalties As SqlDataAdapter
    Dim ds As DataSet
    Dim da As SqlDataAdapter

    Private Sub SSCP_Managers_Tools_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            TCManagerTools.TabPages.Remove(TPPollutantBubbleUp)
            'TCManagerTools.TabPages.Remove(TPStatisticalPage)
            TCManagerTools.TabPages.Remove(TPWatchList)
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
            If AccountFormAccess(129, 3) = "1" Or
                (AccountFormAccess(22, 4) = "1" And AccountFormAccess(22, 3) = "0") Then
                TCNewFacilitySearch.TabPages.Add(TPCopyYear)
            End If
            If AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" Then
                btnAddToCmsUniverse.Visible = False
                btnDeleteFacilityFromCms.Visible = False
                Panel8.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "Select  " &
            "(strLastName||', '||strFirstName) as UserName, " &
            "strUnitDesc, numUserID " &
            "from EPDUserProfiles, LookUpEPDUnits  " &
            "where EPDUserProfiles.numUnit = LookupEPDUnits.numUnitCode (+) " &
            "and (numProgram = '4' " &
            "or strLastname = 'District') " &
            "UNION " &
            "Select  " &
            "(strLastName||', '||strFirstName) as UserName, " &
            "strUnitDesc, numUserID " &
            "from EPDUserProfiles, LookUpEPDUnits, " &
            "SSCPInspectionsRequired   " &
            "where SSCPInspectionsRequired.numSSCPEngineer = EPDUserProfiles.numUserID " &
            "and EPDUserProfiles.numUnit = LookupEPDUnits.numUnitCode (+) " &
            "group by strLastName, strFirstName, strUnitDesc, numUserID  " &
            "order by UserName "

            daStaff = New SqlDataAdapter(SQL, CurrentConnection)

            If AccountFormAccess(22, 2) = "1" Then 'District Liason 
                SQL = "select " &
                "strUnitDesc, numUnitCode " &
                "from LookUPEPDUnits " &
                "where numProgramCode = '4' " &
                "or numUnitCode = '44' " &
                "or numUnitCode = '43' " &
                "or numUnitCode = '42' " &
                "or numUnitCode = '41' " &
                "or numUnitCode = '40' " &
                "or numUnitCode = '39' " &
                "or numUnitCode = '38'  " &
                "or numUnitCode = '37'  " &
                "or numUnitCode = '36' "
            Else
                SQL = "select strUnitDesc, numUnitCode " &
                "from LookUpEPDUnits   " &
                "where numProgramCode = '4' " &
                "order by strUnitDesc  "
            End If

            daUnits = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "Select " &
            "(strLastName||', '||strFirstName) as UserName, " &
            "strUnitDesc, numUserID " &
            "from EPDUserProfiles, LookUpEPDUnits  " &
            "where EPDUserProfiles.numUnit = LookupEPDUnits.numUnitCode (+) " &
            "and ((numProgram = '4' " &
            "or strLastname = 'District') " &
            "    or numbranch = '5') " &
            "and numEmployeeStatus = '1'  " &
            "order by strLastName  "
            daAssignStaff = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "select " &
           "strUnitDesc, numUnitCode " &
           "from LookUPEPDUnits " &
           "where numProgramCode = '4' " &
           "or numUnitCode = '44' " &
           "or numUnitCode = '43' " &
           "or numUnitCode = '42' " &
           "or numUnitCode = '41' " &
           "or numUnitCode = '40' " &
           "or numUnitCode = '39' " &
           "or numUnitCode = '38'  " &
           "or numUnitCode = '37'  "

            daFilterUnits = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "select distinct(strClass) as strClass " &
            "from APBHeaderData  " &
            "order by strClass "

            daClassFilter = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "Select distinct(strCMSMember) as strCMSMember " &
            "from APBSupplamentalData " &
            "order by strCMSMember "

            daCMSMemberFilter = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "select " &
            "strCountyName, strCountyCode " &
            "from LookUpCountyInformation " &
            "order by strCountyName "

            daCountyFilter = New SqlDataAdapter(SQL, CurrentConnection)

            SQL = "Select " &
            "strDistrictName " &
            "from LookupDistricts " &
            "order by strDistrictname "

            daDistrictFilter = New SqlDataAdapter(SQL, CurrentConnection)

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
                SQL = "Select (strLastName||', '||strFirstName) as UserName,  " &
                "strUnitDesc, numUserID    " &
                "from EPDUserProfiles, LookUpEPDUnits    " &
                "where EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+)  " &
                "and (numProgram = '4' or strLastName = 'District') " &
                "order by strLastName "

                dsStaff = New DataSet
                daStaff = New SqlDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daStaff.Fill(dsStaff, "Staff")
            End If

            cboFiscalYear.Items.Add(((Date.Now.Year) + 1).ToString)
            cboFiscalYear.Items.Add(((Date.Now.Year)).ToString)

            SQL = "select " &
            "distinct(intYear) as FCEYear " &
            "from SSCPInspectionsRequired " &
            "order by intYear desc "

            cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "Select " &
            "(strLastName||', '||strFirstName) as UserName, " &
            "numUserID " &
            "from EPDUserProfiles " &
            "where numProgram = '4' " &
            "and numUnit is null " &
            "order by strLastName "

            dsAdminStaff = New DataSet
            daAdminStaff = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAdminStaff.Fill(dsAdminStaff, "AdminStaff")

            SQL = "Select " &
            "(strLastName||', '||strFirstName) as UserName, " &
            "numUserID " &
            "from EPDUserProfiles " &
            "where numProgram = '4' " &
            "and numUnit = '30' " &
            "order by strLastName "

            dsAirStaff = New DataSet
            daAirStaff = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAirStaff.Fill(dsAirStaff, "AirStaff")

            SQL = "Select " &
            "(strLastName||', '||strFirstName) as UserName, " &
            "numUserID " &
            "from EPDUserProfiles " &
            "where numProgram = '4' " &
            "and numUnit = '31' " &
            "order by strLastName "

            dsChemStaff = New DataSet
            daChemStaff = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daChemStaff.Fill(dsChemStaff, "ChemStaff")

            SQL = "Select " &
             "(strLastName||', '||strFirstName) as UserName, " &
             "numUserID " &
             "from EPDUserProfiles " &
             "where numProgram = '4' " &
             "and numUnit = '32' " &
             "order by strLastName "

            dsVOCStaff = New DataSet
            daVOCStaff = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daVOCStaff.Fill(dsVOCStaff, "VOCStaff")

            SQL = "Select " &
            "(strLastName||', '||strFirstName) as UserName,  " &
            "numUserID   " &
            "from EPDUserProfiles  " &
            "where numBranch = '5' " &
            "and (numProgram = '7' " &
            "or numProgram = '9'  " &
            "or numProgram = '10' " &
            "or numProgram = '11' " &
            "or numProgram = '12' " &
            "or numProgram = '13' " &
            "or numProgram = '14' " &
            "or numProgram = '15') " &
            "order by strLastName "

            dsDistrictStaff = New DataSet
            daDistrictStaff = New SqlDataAdapter(SQL, CurrentConnection)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
    Sub AddFacilityToCMS()
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
    Sub RemoveFacilityFromCMS()
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
                    '  

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

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-670), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1765), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
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

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-640), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-550), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1735), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1645), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
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

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-610), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-490), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1705), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1585), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
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

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-0), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-1460), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
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

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " &
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

                            CMSStatus = " and ((strCMSMember = 'A' and lastFCE between '" & StartCMSA & "' and '" & EndCMSA & "') " &
                            "or (strCMSMember = 'S' and LastFCE between '" & StartCMSS & "' and '" & EndCMSS & "')) "
                        Case Else
                            StartCMSA = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSA = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")
                            StartCMSS = Format(CDate(OracleDate), "yyyy-MM-dd")
                            EndCMSS = Format(CDate(OracleDate).AddDays(-365), "yyyy-MM-dd")

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
                PollutantLine = "where (" & PollutantLine & ") "
            Else
                PollutantLine = ""
            End If

            SQL =
            "SELECT SUBSTR( pp.STRAIRSNUMBER, 5 ) AS AIRSNumber , " &
            "  fi.STRFACILITYNAME ,( pp.STRCOMPLIANCESTATUS || ' - ' || " &
            "  lc.STRCOMPLIANCEDESC ) AS PollutantStatus , " &
            "  lp.STRPOLLUTANTDESCRIPTION , CASE                    WHEN SUBSTR( " &
            "      strAirPollutantKey, 13, 1 ) = '0'         THEN 'SIP'     WHEN SUBSTR( " &
            "      strAirPollutantKey, 13, 1 ) = '1'         THEN 'Fed SIP' WHEN " &
            "      SUBSTR( strAirPollutantKey, 13, 1 ) = '3' THEN " &
            "      'Non-Fed SIP'                WHEN SUBSTR( strAirPollutantKey, 13, 1 ) = " &
            "      '4'                                       THEN 'CFC'       WHEN SUBSTR( strAirPollutantKey, 13, 1 ) = " &
            "      '6'                                       THEN 'PSD'       WHEN SUBSTR( strAirPollutantKey, 13, 1 ) = " &
            "      '7'                                       THEN 'NSR'       WHEN SUBSTR( strAirPollutantKey, 13, 1 ) = " &
            "      '8'                                       THEN 'NESHAP'    WHEN SUBSTR( strAirPollutantKey, 13, 1 " &
            "      ) = '9'                                   THEN 'NSPS'      WHEN SUBSTR( strAirPollutantKey, 13 " &
            "      , 1 ) = 'A'                               THEN 'Acid Rain' WHEN SUBSTR( " &
            "      strAirPollutantKey, 13, 1 ) = 'F'         THEN 'FESOP'     WHEN " &
            "      SUBSTR( strAirPollutantKey, 13, 1 ) = 'I' THEN " &
            "      'Native American'   WHEN SUBSTR( strAirPollutantKey, 13, 1 " &
            "      ) = 'M'   THEN 'MACT' WHEN SUBSTR( strAirPollutantKey, 13, " &
            "      1 ) = 'V' THEN 'Title V' ELSE '' END AirProgram " &
            "FROM APBAIRPROGRAMPOLLUTANTS pp " &
            "INNER JOIN LOOKUPCOMPLIANCESTATUS lc " &
            "ON lc.STRCOMPLIANCECODE = pp.STRCOMPLIANCESTATUS " &
            "INNER JOIN LOOKUPPOLLUTANTS lp " &
            "ON lp.STRPOLLUTANTCODE = pp.STRPOLLUTANTKEY " &
            "INNER JOIN APBFACILITYINFORMATION fi " &
            "ON pp.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
            PollutantLine

            dsPollutantList = New DataSet
            daPollutantList = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daPollutantList.Fill(dsPollutantList, "PollutantList")
            dgvPollutantFacilities.DataSource = dsPollutantList
            dgvPollutantFacilities.DataMember = "PollutantList"

            dgvPollutantFacilities.RowHeadersVisible = False
            dgvPollutantFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvPollutantFacilities.AllowUserToResizeColumns = True
            dgvPollutantFacilities.AllowUserToAddRows = False
            dgvPollutantFacilities.AllowUserToDeleteRows = False
            dgvPollutantFacilities.AllowUserToOrderColumns = True
            dgvPollutantFacilities.AllowUserToResizeRows = True

            dgvPollutantFacilities.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvPollutantFacilities.Columns("AIRSNumber").DisplayIndex = 0
            dgvPollutantFacilities.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvPollutantFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvPollutantFacilities.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgvPollutantFacilities.Columns("strPollutantDescription").DisplayIndex = 2
            dgvPollutantFacilities.Columns("PollutantStatus").HeaderText = "Status"
            dgvPollutantFacilities.Columns("PollutantStatus").DisplayIndex = 3
            dgvPollutantFacilities.Columns("AirProgram").HeaderText = "Air Program"
            dgvPollutantFacilities.Columns("AirProgram").DisplayIndex = 4

            dgvPollutantFacilities.SanelyResizeColumns()

            txtPollutantCount.Text = dgvPollutantFacilities.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
        Try

            LoadCMSUniverse()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbCMSOpenFacilitySummary_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary.LinkClicked
        OpenFormFacilitySummary(txtCMSAIRSNumber.Text)
    End Sub
    Private Sub llbCMSOpenFacilitySummary2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary2.LinkClicked
        OpenFormFacilitySummary(txtCMSAIRSNumber2.Text)
    End Sub
    Private Sub btnAddToCmsUniverse_LinkClicked(sender As Object, e As EventArgs) Handles btnAddToCmsUniverse.Click
        Try
            AddFacilityToCMS()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnDeleteFacilityFromCms_Click(sender As Object, e As EventArgs) Handles btnDeleteFacilityFromCms.Click
        Try
            RemoveFacilityFromCMS()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblRunInspectionReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblRunInspectionReport.LinkClicked
        Try
            RunInspectionReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

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
        Try

            RunCMSWarningReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbPrintStaffReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbPrintStaffReport.LinkClicked
        Try
            If rtbInspectionReport.Text <> "" Then
                PrintStaffReport()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewFacilities_Click(sender As Object, e As EventArgs) Handles btnViewFacilities.Click
        FilterPollutantSearch()
    End Sub
    Private Sub dgvPollutantFacilities_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvPollutantFacilities.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvPollutantFacilities.HitTest(e.X, e.Y)

        Try


            If dgvPollutantFacilities.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvPollutantFacilities.Columns(0).HeaderText = "AIRS #" Then
                    txtAIRSNumber.Text = dgvPollutantFacilities(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRunStatisticalReport_Click(sender As Object, e As EventArgs) Handles btnRunStatisticalReport.Click
        Try

            RunACCStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "select " &
            "SUBSTR(TABLE1.STRAIRSNUMBER, 5) as AIRSNUMBER, " &
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
            SQL = "select " &
                "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " &
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
            SQL = "select " &
                "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
           "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
                "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
                "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
                "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
            "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
            "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
                "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
            "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
            "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
            SQL = "select " &
            "substr(APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " &
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
        Try

            ViewACCTotalAssigned()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCReporting_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCReporting.LinkClicked
        Try

            ViewACCReporting()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCRequiringResubmittal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCRequiringResubmittal.LinkClicked
        Try

            ViewACCRequiringResubmittal()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCSubmittedLate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCSubmittedLate.LinkClicked
        Try

            ViewACCSubmittedLate()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReported.LinkClicked
        Try

            ViewACCDeviationsReported()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsReportedCorrectly_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReportedCorrectly.LinkClicked
        Try

            ViewACCDeviationsReportedCorrectly()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsIncorrectlyReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsIncorrectlyReported.LinkClicked
        Try

            ViewACCDeviationsReportedIncorrectly()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsInFinal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsInFinal.LinkClicked
        Try

            ViewACCDeviationsInFinal()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsNotReported_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsNotReported.LinkClicked
        Try

            ViewACCDeviationsNotReported()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCEnforcementTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCEnforcementTaken.LinkClicked
        Try

            ViewACCEnforcementTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCCOTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCCOTaken.LinkClicked
        Try

            ViewACCCOTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCNOVTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCNOVTaken.LinkClicked
        Try

            ViewACCNOVTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCLONTaken_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbACCLONTaken.LinkClicked
        Try

            ViewACCLONTaken()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

    Sub EnforcementTotals()
        Try
            SQL = "select " &
            "'$'||to_number((CoPenalty + Stipulated), '99999999.99') as TotalPen  " &
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
                "'$'||to_number((CoPenalty + Stipulated), '99999999.99') as TotalPen  " &
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
        Try
            If txtEnforcementAIRSNumber.Text <> "" Then
                EnforcementTotals()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewEnforcements_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEnforcements.LinkClicked
        Try
            If txtEnforcementAIRSNumber.Text <> "" Then
                SQL = "select " &
                "strFacilityName, " &
                "substr(SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " &
                "SSCP_AuditedEnforcement.strEnforcementNumber, " &
                "'$'||to_number(SSCP_AuditedEnforcement.strCOPenaltyAmount, '99999999.99') as COPenalty, " &
                "'$'||to_number(SSCPEnforcementStipulated.strStipulatedPenalty, '99999999.99') as StipulatedPenalty, " &
                "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " &
                "from SSCP_AuditedEnforcement, " &
                "SSCPEnforcementStipulated, APBFacilityInformation  " &
                "where SSCP_AuditedEnforcement.strEnforcementNumber = SSCPEnforcementStipulated.strEnforcementNumber " &
                "and SSCP_AuditedEnforcement.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "

                If chbUseEnforcementDateRange.Checked = True Then
                    SQL = "select " &
                    "strFacilityName, " &
                    "substr(SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " &
                    "SSCP_AuditedEnforcement.strEnforcementNumber, " &
                    "'$'||to_number(SSCP_AuditedEnforcement.strCOPenaltyAmount, '99999999.99') as COPenalty, " &
                    "'$'||to_number(SSCPEnforcementStipulated.strStipulatedPenalty, '99999999.99') as StipulatedPenalty, " &
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
        Try
            If chbUseEnforcementDateRange.Checked = True Then
                dtpEnforcementStartDate.Enabled = True
                dtpEnforcementEndDate.Enabled = True
            Else
                dtpEnforcementStartDate.Enabled = False
                dtpEnforcementEndDate.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
    Sub OpenFacilitySummary()
        OpenFormFacilitySummary(txtRecordNumber.Text)
    End Sub
    Sub OpenEnforcement()
        OpenFormEnforcement(txtRecordNumber.Text)
    End Sub
    Sub OpenSSCPWork()
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
    Private Sub btnViewWatchListFacilities_Click(sender As Object, e As EventArgs) Handles btnViewWatchListFacilities.Click
        Try
            Dim ComplianceWhere As String = "and strComplianceStatus = '0' "

            If rdbAllNegativeStatus.Checked = True Then
                ComplianceWhere = " and (strComplianceStatus = 'B' or strComplianceStatus = '1' " &
                "or strComplianceStatus = '6' or strComplianceStatus = 'W' " &
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

            SQL = "select " &
            "distinct(substr(APBAirProgramPollutants.strAIRSNumber, 5)) as AIRSNumber, " &
            "strFacilityName, strPollutantDescription, " &
            "(strComplianceStatus||' - '||strComplianceDesc) as ComplianceStatus " &
            "from APBAirProgramPollutants, APBFacilityInformation, " &
            "LookUpPollutants, LookUpComplianceStatus  " &
            "where APBAirProgramPollutants.strAIRSnumber = APBFacilityInformation.strAIRSNumber  " &
            "and APBAirProgramPollutants.strPollutantKey = LookUpPollutants.strPollutantCode " &
            "and APBAirProgramPollutants.strComplianceStatus = LookupComplianceStatus.strComplianceCode  " &
               ComplianceWhere

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFacilitySearch(Location As String)
        Try
            Dim SQLLine1 As String = " "
            Dim SQLLine2 As String = " "
            Dim SQLOrder1 As String = " "
            Dim SQLOrder2 As String = " "
            ' Dim dtEngineers As New DataTable
            Dim SQLLine As String = ""
            Dim TotalText As String = ""

            If chbIgnoreFiscalYear.Checked = True Then
                SQL = "Select " &
                "substr(VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " &
                "strFacilityCity, " &
                "strCMSMember, " &
                "strClass, strOperationalStatus, " &
                "LastInspection," &
                "LastFCE, " &
                "(strLastName||', '||strFirstName) as SSCPEngineer, " &
                "strUnitDesc," &
                " strDistrictResponsible, " &
                "strCountyName " &
                "from VW_SSCP_MT_FacilityAssignment, " &
                "EPDUserProfiles, " &
                " SSCPInspectionsRequired, " &
                "LookUpEPDUnits, " &
                "(select " &
                "max(intYear) as MaxYear, SSCPInspectionsRequired.strairsnumber " &
                "from SSCPInspectionsRequired " &
                "group by SSCPInspectionsRequired.strAIRSNumber) MaxResults " &
              " where SSCPInspectionsRequired.numSSCPEngineer = EPDUserProfiles.numUserID (+) " &
              "and SSCPInspectionsRequired.numSSCPUnit = LookUpEPDunits.numUnitCode (+) " &
              " and VW_SSCP_MT_FacilityAssignment.strairsnumber = sscpinspectionsrequired.strairsnumber (+) " &
              "and SSCPInspectionsRequired.strAIRSNumber = MaxResults.strAIRSNumber " &
              "and SSCPInspectionsRequired.intYear = MaxResults.maxYear "

            Else
                SQL = "Select " &
              "substr(VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " &
              "strFacilityCity, " &
              "strCMSMember, " &
              " strClass, strOperationalStatus, " &
              " case " &
              " when strInspectionRequired = 'True' then 'True' " &
              " when strinspectionrequired = 'False' then 'False' " &
              " when strInspectionRequired is null then 'False' " &
              " end InspectionRequired, " &
              " LastInspection," &
              "   case " &
              " when strFCERequired = 'True' then 'True' " &
              " when strFCERequired = 'False' then 'False' " &
              " when strFCERequired is null then 'False' " &
              " end FCERequired, " &
              "   LastFCE, " &
              "(strLastName||', '||strFirstName) as SSCPEngineer, " &
              "  strUnitDesc," &
              "   strDistrictResponsible, " &
              "  strCountyName " &
              " from VW_SSCP_MT_FacilityAssignment, " &
              "EPDUserProfiles, " &
              " SSCPInspectionsRequired, " &
              "LookUpEPDUnits " &
              " where SSCPInspectionsRequired.numSSCPEngineer = EPDUserProfiles.numUserID (+) " &
              "and SSCPInspectionsRequired.numSSCPUnit = LookUpEPDunits.numUnitCode (+) " &
              " and VW_SSCP_MT_FacilityAssignment.strairsnumber = sscpinspectionsrequired.strairsnumber (+) " &
              " and sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "
            End If

            SQLLine1 = " "
            SQLLine2 = " "
            SQLOrder1 = " "
            SQLOrder2 = " "
            If Location = "Filter" Then
                If cboFacSearch1.Items.Contains(cboFacSearch1.Text) And cboFilterEngineer1.Text <> "" Then
                    Select Case cboFacSearch1.Text
                        Case "AIRS Number"
                            SQLLine1 = " upper(VW_SSCP_MT_FacilityAssignment.strAIRSNumber) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
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
                            SQLLine2 = " upper(VW_SSCP_MT_FacilityAssignment.strAIRSNumber) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
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
                    SQLLine = SQLLine & " VW_SSCP_MT_FacilityAssignment.strairsnumber = '0413" & Mid(TotalText, 1, 8) & "' or "
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
            da = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFacilitySearch_Click(sender As Object, e As EventArgs) Handles btnFacilitySearch.Click
        Try
            LoadFacilitySearch("Filter")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
                Case "Unassigned Facilities"

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
                Case "Unassigned Facilities"

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
        Try

            txtManualAIRSNumber.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFilterManualAIRSList_Click(sender As Object, e As EventArgs) Handles btnFilterManualAIRSList.Click
        Try
            LoadFacilitySearch("Manual")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEngineerResponsibility_Click(sender As Object, e As EventArgs) Handles btnSaveEngineerResponsibility.Click
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

                SQL = "Select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPEngineer = '" & Eng & "', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "DATASSIGNINGDATE = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' " &
                    "and sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    "numSSCPEngineer, strAssigningManager, DATASSIGNINGDATE) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                    "'" & Eng & "', '" & CurrentUser.UserID & "', " &
                    "'" & OracleDate & "') "
                End If

                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveSSCPUnitAssignment_Click(sender As Object, e As EventArgs) Handles btnSaveSSCPUnitAssignment.Click
        Try
            Dim AIRSNum As String = ""
            'Dim SSCPUnit As String = ""

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "DATASSIGNINGDATE = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "'" &
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    "numSSCPUnit, strAssigningManager, DATASSIGNINGDATE) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                    "'" & cboSSCPUnit2.SelectedValue & "', " &
                    "'" & CurrentUser.UserID & "', " &
                    "'" & OracleDate & "') "
                End If

                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveDistResponsible_Click(sender As Object, e As EventArgs) Handles btnSaveDistResponsible.Click
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

                SQL = "Select strAIRSNumber " &
                "from SSCPDistrictResponsible " &
                "where strAIRSNumber = '0413" & AIRSNum & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update SSCPDistrictResponsible set " &
                    "strDistrictResponsible = '" & DistResp & "', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "datAssigningDate = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' "
                Else
                    SQL = "Insert into SSCPDistrictResponsible " &
                    "values " &
                    "('0413" & AIRSNum & ", '" & DistResp & "', " &
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
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
    Private Sub btnSaveInspectionReq_Click(sender As Object, e As EventArgs) Handles btnSaveInspectionReq.Click
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

                SQL = "Select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "strInspectionRequired = '" & InspectionRequired & "', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "datAssigningDate = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' " &
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    "strInspectionRequired, strAssigningManager, datAssigningDate) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                    "'" & InspectionRequired & "', " &
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveFCEReq_Click(sender As Object, e As EventArgs) Handles btnSaveFCEReq.Click
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

                SQL = "Select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "strFCERequired = '" & FCERequired & "', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "datAssigningDate = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' " &
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    "strFCERequired, strAssigningManager, datAssigningDate) " &
                   "values " &
                   "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                   "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                   "'" & FCERequired & "', '" & CurrentUser.UserID & "', '" & OracleDate & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveAllSettings_Click(sender As Object, e As EventArgs) Handles btnSaveAllSettings.Click
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

                SQL = "select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPEngineer = '" & Eng & "', " &
                    "numSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " &
                    "strInspectionRequired = '" & InspectionRequired & "', " &
                    "strFCERequired = '" & FCERequired & "', " &
                    "STRASSIGNINGMANAGER = '" & CurrentUser.UserID & "', " &
                    "DATASSIGNINGDATE = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' " &
                    "and intyear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    "numSSCPEngineer, numSSCPUnit, " &
                    "strInspectionRequired, strFCERequired, " &
                    "STRASSIGNINGMANAGER, DATASSIGNINGDATE) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                    "'" & Eng & "', '" & cboSSCPUnit2.SelectedValue & "', " &
                    "'" & InspectionRequired & "', '" & FCERequired & "', " &
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " &
                "from SSCPDistrictResponsible " &
                "where strAIRSNumber = '0413" & AIRSNum & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update SSCPDistrictResponsible set " &
                    "strDistrictResponsible = '" & DistResp & "', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "datAssigningDate = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' "
                Else
                    SQL = "Insert into SSCPDistrictResponsible " &
                    "values " &
                    "('0413" & AIRSNum & ", '" & DistResp & "', " &
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnForceAIRSNumber_Click(sender As Object, e As EventArgs) Handles btnForceAIRSNumber.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0
            Dim FacilityName As String = ""

            SQL = "Select strAIRSNumber " &
            "From APBMasterAIRS " &
            "where strAIRSNumber = '0413" & mtbForcedAIRS.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Select " &
                "strFacilityName " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = '0413" & mtbForcedAIRS.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearEngineerAssignment_Click(sender As Object, e As EventArgs) Handles btnClearEngineerAssignment.Click
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

                SQL = "Select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPEngineer = '', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                    "DATASSIGNINGDATE = '" & OracleDate & "' " &
                    "where strAIRSNumber = '0413" & AIRSNum & "' " &
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    "numSSCPEngineer, strAssigningManager, DATASSIGNINGDATE) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                    "'', '" & CurrentUser.UserID & "', " &
                    "'" & OracleDate & "') "
                End If

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(2, i).Value = ""
            Next
            MsgBox("Assignment(s) Cleared", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearSSCPUnitAssignment_Click(sender As Object, e As EventArgs) Handles btnClearSSCPUnitAssignment.Click
        Try
            Dim AIRSNum As String = ""
            ' Dim SSCPUnit As String = ""

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            For i As Integer = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & AIRSNum & "' " &
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPUnit = '', " &
                    "strAssigningManager = '" & CurrentUser.UserID & "', " &
                  "DATASSIGNINGDATE = '" & OracleDate & "' " &
                  "where strAIRSNumber = '0413" & AIRSNum & "'" &
                  "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into SSCPInspectionsRequired " &
                 "(numKey, strAIRSNumber, intYear, " &
                 "numSSCPUnit, strAssigningManager, DATASSIGNINGDATE) " &
                 "values " &
                 "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                 "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " &
                    "'', " &
                    "'" & CurrentUser.UserID & "', " &
                    "'" & OracleDate & "') "
                End If

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(3, i).Value = ""
            Next
            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveCMS_Click(sender As Object, e As EventArgs) Handles btnSaveCMS.Click
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

                SQL = "Update APBSupplamentalData set " &
                "strCMSMember = '" & CMSStatus & "' " &
                "where strAIRSNumber = '0413" & AIRSNum & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboFiscalYear_TextChanged(sender As Object, e As EventArgs) Handles cboFiscalYear.SelectedIndexChanged
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
                If Not cboExistingYears.Items.Contains(targetYear) Then
                    cboExistingYears.Items.Add(targetYear)
                End If

                If Not cboFiscalYear.Items.Contains(targetYear) Then
                    cboFiscalYear.Items.Add(mtbNewYear.Text)
                End If

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
            SQL = "SELECT DISTINCT SUBSTR(FI.STRAIRSNUMBER, 5) AS AIRSNumber, " &
            "  FI.STRFACILITYNAME, " &
            "  HD.STROPERATIONALSTATUS, " &
            "  (UP.STRLASTNAME " &
            "  || ', ' " &
            "  || UP.STRFIRSTNAME) AS StaffResponsible " &
            "FROM APBFacilityinformation FI, " &
            "  APBHeaderdata HD, " &
            "  EPDUserProfiles UP, " &
            "  SSCPINSPECTIONSREQUIRED IR, " &
            "  (SELECT DISTINCT SUBSTR(tFI.STRAIRSNUMBER, 5) AS AIRSNumber " &
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
            "  AND SUBSTR(tHD.STRAIRPROGRAMCODES, 13, 1) = '1' " &
            "  AND ((tAT.DATPERMITISSUED                IS NOT NULL " &
            "  AND tAT.DATPERMITISSUED                   < add_months(SysDate, -51)) " &
            "  OR (tAT.DATEFFECTIVE                     IS NOT NULL " &
            "  AND tAT.DATEFFECTIVE                      < add_months(SysDate, -51))) " &
            "  MINUS " &
            "    (SELECT DISTINCT SUBSTR(tAM.STRAIRSNUMBER, 5) AS AIRSNumber " &
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
            "    AND ((tAT.DATPERMITISSUED BETWEEN add_months(SysDate, -51) AND SysDate " &
            "    AND tAT.DATEFFECTIVE BETWEEN add_months(SysDate,      -51) AND SysDate) " &
            "    OR (tAT.DATRECEIVEDDATE BETWEEN add_months(SysDate,   -51) AND SysDate)) " &
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


    Private Sub btnRunComplianceReport_Click(sender As Object, e As EventArgs) Handles btnRunComplianceReport.Click
        Try
            SQL = "select " &
"SSCP_AuditedEnforcement.strEnforcementnumber,  " &
"substr(apbheaderdata.strairsnumber,  5) as AIRSNumber,  " &
"APBFacilityInformation.strfacilityname,  " &
"apbheaderdata.strclass,  " &
"case " &
"when strDistrictResponsible = 'True' then 'District Responsible' " &
"else 'SSCP Responsible' " &
"end DistrictResponsible, " &
"strActionType, datDiscoverydate,  " &
"case  " &
"when strActionType = 'LON' then datLONSent   " &
"when strActionType = 'NOV' then datNOVSent   " &
"when strActionType = 'NOVCO' then datCOProposed  " &
"when strActionType = 'NOVCOP' then datCOProposed  " &
"when strActionType = 'HPV' then datNOVSent   " &
"when strActionType = 'HPVCO' then datCOProposed  " &
"when strActionType = 'HPVCOP' then datCOProposed   " &
"when strActionType = 'HPVAO' then datAOExecuted   " &
"end DateIssued, " &
"case  " &
"when strActionType = 'LON' then datLONResolved   " &
"when strActionType = 'NOV' then datNFALetterSent   " &
"when strActionType = 'NOVCO' then datCOResolved  " &
"when strActionType = 'NOVCOP' then datCOResolved   " &
"when strActionType = 'HPV' then datNFALetterSent  " &
"when strActionType = 'HPVCO' then datCOResolved  " &
"when strActionType = 'HPVCOP' then datCOResolved " &
"when strActionType = 'HPVAO' then datAOResolved    " &
"end DateResolved " &
"from apbheaderdata, APBFacilityInformation,  " &
"SSCP_AuditedEnforcement, SSCPDistrictResponsible " &
"where apbheaderdata.strairsnumber = APBFacilityInformation.strairsnumber  " &
"and APBHeaderdata.strairsnumber = SSCPDistrictResponsible.strAIRSnumber  " &
"and APBHeaderdata.strairsnumber = SSCP_AuditedEnforcement.strAIRSnumber  " &
"order by datDiscoverydate desc Nulls Last "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub txtManualAIRSNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtManualAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtManualAIRSNumber.SelectAll()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region "Export to Excel"

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvStatisticalReports.ExportToExcel(Me)
    End Sub

    Private Sub btnExportMiscToExcel_Click(sender As Object, e As EventArgs) Handles btnExportMiscToExcel.Click
        dgvMiscReport.ExportToExcel(Me)
    End Sub

    Private Sub btnExportCmsWarningToExcel_Click(sender As Object, e As EventArgs) Handles btnExportCmsWarningToExcel.Click
        dgvCMSWarning.ExportToExcel(Me)
    End Sub

    Private Sub btnExportPollutantsToExcel_Click(sender As Object, e As EventArgs) Handles btnExportPollutantsToExcel.Click
        dgvPollutantFacilities.ExportToExcel(Me)
    End Sub

    Private Sub btnExportWatchListToExcel_Click(sender As Object, e As EventArgs) Handles btnExportWatchListToExcel.Click
        dgvWatchList.ExportToExcel(Me)
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
        End With
    End Sub

    Private Sub EnableEnfDocTypeUpdate()
        EnableDisableEnfDocTypeUpdate(True)
    End Sub

    Private Sub DisableEnfDocTypeUpdate()
        EnableDisableEnfDocTypeUpdate(False)
    End Sub

    Private Sub EnableDisableEnfDocTypeUpdate(enable As Boolean)
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

    Private Sub btnAddDocumentType_Click(sender As Object, e As EventArgs) _
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

#Region "Change Accept Button"

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
    Handles txtNewName.Leave, txtUpdateName.Leave, mtxtNewPosition.Leave, mtxtUpdatePosition.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub NewEnfDocType_Enter(sender As Object, e As EventArgs) _
    Handles txtNewName.Enter, mtxtNewPosition.Enter
        Me.AcceptButton = btnAddDocumentType
    End Sub

    Private Sub UpdateEnfDocType_Enter(sender As Object, e As EventArgs) _
    Handles txtUpdateName.Enter, mtxtUpdatePosition.Enter
        Me.AcceptButton = btnUpdateDocumentType
    End Sub

#End Region

#End Region

    Private Sub OpenFacilityButton_Click(sender As Object, e As EventArgs) Handles OpenFacilityButton.Click
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtAIRSNumber.Text) Then
            OpenFormFacilitySummary(txtAIRSNumber.Text)
        End If
    End Sub

End Class