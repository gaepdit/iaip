Imports System.Data.OracleClient
Imports System
Imports System.Data
Imports System.IO
'Imports System.Text
Imports System.Windows.Forms

Public Class SSCPManagersTools
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Panel1temp As String

    Dim SQL As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim recExist As Boolean
    Dim Profile_Code As String

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


            CreateStatusBar()
            LoadDataSets()
            LoadComboBoxes()
            FormatdgrInspectionList()

            LoadInspectionEngineers()
            LoadComboYear()

            DTPStartDate.Value = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPEndDate.Value = OracleDate
            dtpEnforcementStartDate.Value = OracleDate
            dtpEnforcementEndDate.Value = OracleDate
            dtpEnforcementStartDate.Enabled = False
            dtpEnforcementEndDate.Enabled = False

            TCManagerTools.TabPages.Remove(TPCMSWarning)
            TCManagerTools.TabPages.Remove(TPUniverse)
            TCManagerTools.TabPages.Remove(TPEngineerInspections)
            TCManagerTools.TabPages.Remove(TPStaffReports)
            TCManagerTools.TabPages.Remove(TPFacilityAssignments)
            TCManagerTools.TabPages.Remove(TPPollutantBubbleUp)
            TCManagerTools.TabPages.Remove(TPStatisticalPage)
            TCManagerTools.TabPages.Remove(TPWatchList)
            TCManagerTools.TabPages.Remove(TPNewFacilityAssignments)
            TCManagerTools.TabPages.Remove(TPMiscReports)

            TCManagerTools.TabPages.Add(TPNewFacilityAssignments)
            'TCManagerTools.TabPages.Add(TPFacilityAssignments)
            TCManagerTools.TabPages.Add(TPStaffReports)
            ' TCManagerTools.TabPages.Add(TPEngineerInspections)
            TCManagerTools.TabPages.Add(TPUniverse)
            TCManagerTools.TabPages.Add(TPCMSWarning)
            TCManagerTools.TabPages.Add(TPPollutantBubbleUp)
            TCManagerTools.TabPages.Add(TPStatisticalPage)
            TCManagerTools.TabPages.Add(TPWatchList)
            TCManagerTools.TabPages.Add(TPMiscReports)

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
            If AccountArray(129, 3) = "1" Or _
                (AccountArray(22, 4) = "1" And AccountArray(22, 3) = "0") Then
                TCNewFacilitySearch.TabPages.Add(TPCopyYear)
            End If
            If AccountArray(48, 2) = "1" And AccountArray(48, 3) = "0" Then
                llbAddFacilityToCMS.Visible = False
                llbDeleteFacilityFromCMS.Visible = False
                Panel8.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#Region "Page Load subs"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = UserName
            panel3.Text = OracleDate

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)
            pnlDistResp1.Visible = False
            pnlDistResp2.Visible = False
            cboOpStatus1.Visible = False
            cboOpStatus2.Visible = False

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
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
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits  " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookupEPDUnits.numUnitCode (+) " & _
            "and (numProgram = '4' " & _
            "or strLastname = 'District') " & _
            "UNION " & _
            "Select  " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "strUnitDesc, numUserID " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits, " & _
            "" & DBNameSpace & ".SSCPInspectionsRequired   " & _
            "where " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookupEPDUnits.numUnitCode (+) " & _
            "group by strLastName, strFirstName, strUnitDesc, numUserID  " & _
            "order by UserName "

            daStaff = New OracleDataAdapter(SQL, Conn)

            If AccountArray(22, 2) = "1" Then 'District Liason 
                SQL = "select " & _
                "strUnitDesc, numUnitCode " & _
                "from " & DBNameSpace & ".LookUPEPDUnits " & _
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
                "from " & DBNameSpace & ".LookUpEPDUnits   " & _
                "where numProgramCode = '4' " & _
                "order by strUnitDesc  "
            End If

            daUnits = New OracleDataAdapter(SQL, Conn)

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "strUnitDesc, numUserID " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits  " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookupEPDUnits.numUnitCode (+) " & _
            "and ((numProgram = '4' " & _
            "or strLastname = 'District') " & _
            "    or numbranch = '5') " & _
            "and numEmployeeStatus = '1'  " & _
            "order by strLastName  "
            daAssignStaff = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
           "strUnitDesc, numUnitCode " & _
           "from " & DBNameSpace & ".LookUPEPDUnits " & _
           "where numProgramCode = '4' " & _
           "or numUnitCode = '44' " & _
           "or numUnitCode = '43' " & _
           "or numUnitCode = '42' " & _
           "or numUnitCode = '41' " & _
           "or numUnitCode = '40' " & _
           "or numUnitCode = '39' " & _
           "or numUnitCode = '38'  " & _
           "or numUnitCode = '37'  "

            daFilterUnits = New OracleDataAdapter(SQL, Conn)

            SQL = "select distinct(strClass) as strClass " & _
            "from " & DBNameSpace & ".APBHeaderData  " & _
            "order by strClass "

            daClassFilter = New OracleDataAdapter(SQL, Conn)

            SQL = "Select distinct(strCMSMember) as strCMSMember " & _
            "from " & DBNameSpace & ".APBSupplamentalData " & _
            "order by strCMSMember "

            daCMSMemberFilter = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
            "strCountyName, strCountyCode " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "order by strCountyName "

            daCountyFilter = New OracleDataAdapter(SQL, Conn)

            SQL = "Select " & _
            "strDistrictName " & _
            "from " & DBNameSpace & ".LookupDistricts " & _
            "order by strDistrictname "

            daDistrictFilter = New OracleDataAdapter(SQL, Conn)

            'If AccountArray(22, 3) = "1" And UserUnit <> "---" Then  'SSCP Unit Manager 
            '    SQL2 = "SELECT strunitdesc, " & _
            '    "numunitcode " & _
            '    "FROM " & DBNameSpace & ".lookupepdunits " & _
            '    "WHERE numprogramCode  = '4' " & _
            '    "ORDER BY strunitdesc "
            'End If
            'If AccountArray(22, 2) = "1" Then 'District Liason 
            '    SQL2 = "select " & _
            '    "strUnitDesc, numUnitCode " & _
            '    "from " & DBNameSpace & ".LookUPEPDUnits " & _
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
            '                   "from " & DBNameSpace & ".LookUpEPDUnits   " & _
            '                   "where numProgramCode = '4' " & _
            '                   "order by strUnitDesc  "
            'End If
            'If AccountArray(22, 4) = "1" Then
            '    SQL2 = "select strUnitDesc, numUnitCode " & _
            '                   "from " & DBNameSpace & ".LookUpEPDUnits   " & _
            '                   "where numProgramCode = '4' " & _
            '                   "order by strUnitDesc  "
            'End If

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits    " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+)  " & _
                "and (numProgram = '4' or strLastName = 'District') " & _
                "order by strLastName "

                dsStaff = New DataSet
                daStaff = New OracleDataAdapter(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                daStaff.Fill(dsStaff, "Staff")
            End If

            cboFiscalYear.Items.Add(((Date.Now.Year) + 1).ToString)
            cboFiscalYear.Items.Add(((Date.Now.Year)).ToString)

            SQL = "select " & _
            "distinct(intYear) as FCEYear " & _
            "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
            "order by intYear desc "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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

            Dim DefaultsText As String = ""
            If File.Exists("C:\APB\Defaults.txt") Then
                Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                Do
                    DefaultsText = DefaultsText & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()

                If DefaultsText.IndexOf("SSCPProfile-") <> -1 Then
                    Profile_Code = Mid(DefaultsText, ((DefaultsText.IndexOf("SSCPProfile-")) + 13), ((DefaultsText.IndexOf("-eliforPPCSS")) - (DefaultsText.IndexOf("SSCPProfile-") + 12)))
                Else
                    Profile_Code = ""
                End If
            Else
                Profile_Code = ""
            End If

            If Profile_Code = "" Then
                SQL3 = "Select strSSCPFacilityAssignment " & _
                "from " & DBNameSpace & ".APBUsers " & _
                "where strUserGCode = '" & UserGCode & "' "

                cmd = New OracleCommand(SQL3, Conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strSSCPFacilityAssignment")) Then
                        Profile_Code = "313000000200000"
                    Else
                        Profile_Code = dr.Item("strSSCPFacilityAssignment")
                    End If

                End While
                dr.Close()

                DefaultsText = ""
                temp = "SSCPProfile-313000000200000-eliforPPCSS"
                If File.Exists("C:\APB\Defaults.txt") Then
                    Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                    Do
                        DefaultsText = DefaultsText & reader.ReadLine
                    Loop Until reader.Peek = -1
                    reader.Close()

                    If DefaultsText.IndexOf("SSCPProfile-") <> -1 Then
                        DefaultsText = DefaultsText.Replace(Mid(DefaultsText, ((DefaultsText.IndexOf("SSCPProfile-")) + 1), (DefaultsText.IndexOf("-eliforPPCSS")) + 12), temp)
                    Else
                        DefaultsText = DefaultsText & vbCrLf & temp
                    End If

                    Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Close()
                    Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                    writer.WriteLine(DefaultsText)
                    writer.Close()
                Else
                    DefaultsText = temp
                    Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)
                    fs.Close()
                    Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                    writer.WriteLine(DefaultsText)
                    writer.Close()
                End If

            End If
            lblProfileCode.Text = Profile_Code
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            With cboEngineer
                .DataSource = dtAssignStaff
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

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

            With cboSSCPUnit
                .DataSource = dtUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

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

            '--- This loads the Combo Box Filter Option 1
            cboFilterOption1.Items.Add("<Select a Filter Option>")
            cboFilterOption1.Items.Add("AIRS Number")
            cboFilterOption1.Items.Add("City")
            cboFilterOption1.Items.Add("Classification")
            cboFilterOption1.Items.Add("County")
            cboFilterOption1.Items.Add("District")
            cboFilterOption1.Items.Add("District Engineer")
            cboFilterOption1.Items.Add("District Responsible")
            cboFilterOption1.Items.Add("Engineer")
            cboFilterOption1.Items.Add("Facility Name")
            cboFilterOption1.Items.Add("Operational Status")
            cboFilterOption1.Items.Add("SIC Codes")
            cboFilterOption1.Items.Add("SSCP Unit")

            cboFilterOption1.SelectedIndex = 0

            '--- This loads the Combo Box Filter Option 2
            cboFilterOption2.Items.Add("<Select a Filter Option>")
            cboFilterOption2.Items.Add("AIRS Number")
            cboFilterOption2.Items.Add("City")
            cboFilterOption2.Items.Add("Classification")
            cboFilterOption2.Items.Add("County")
            cboFilterOption2.Items.Add("District")
            cboFilterOption2.Items.Add("District Engineer")
            cboFilterOption2.Items.Add("District Responsible")
            cboFilterOption2.Items.Add("Engineer")
            cboFilterOption2.Items.Add("Facility Name")
            cboFilterOption2.Items.Add("Operational Status")
            cboFilterOption2.Items.Add("SIC Codes")
            cboFilterOption2.Items.Add("SSCP Unit")

            cboFilterOption2.SelectedIndex = 0

            '--- This loads the Combo Box Filter Option 3
            cboFilterOption3.Items.Add("<Select a Filter Option>")
            cboFilterOption3.Items.Add("AIRS Number")
            cboFilterOption3.Items.Add("City")
            cboFilterOption3.Items.Add("Classification")
            cboFilterOption3.Items.Add("County")
            cboFilterOption3.Items.Add("District")
            cboFilterOption3.Items.Add("District Engineer")
            cboFilterOption3.Items.Add("District Responsible")
            cboFilterOption3.Items.Add("Engineer")
            cboFilterOption3.Items.Add("Facility Name")
            cboFilterOption3.Items.Add("Operational Status")
            cboFilterOption3.Items.Add("SIC Codes")
            cboFilterOption3.Items.Add("SSCP Unit")

            cboFilterOption3.SelectedIndex = 0

            '--- This loads the Combo Box Filter Option 4
            cboFilterOption4.Items.Add("<Select a Filter Option>")
            cboFilterOption4.Items.Add("AIRS Number")
            cboFilterOption4.Items.Add("City")
            cboFilterOption4.Items.Add("Classification")
            cboFilterOption4.Items.Add("County")
            cboFilterOption4.Items.Add("District")
            cboFilterOption4.Items.Add("District Engineer")
            cboFilterOption4.Items.Add("District Responsible")
            cboFilterOption4.Items.Add("Engineer")
            cboFilterOption4.Items.Add("Facility Name")
            cboFilterOption4.Items.Add("Operational Status")
            cboFilterOption4.Items.Add("SIC Codes")
            cboFilterOption4.Items.Add("SSCP Unit")

            cboFilterOption4.SelectedIndex = 0

            '--- This loads the Combo Box Filter Option 5
            cboFilterOption5.Items.Add("<Select a Filter Option>")
            cboFilterOption5.Items.Add("AIRS Number")
            cboFilterOption5.Items.Add("City")
            cboFilterOption5.Items.Add("Classification")
            cboFilterOption5.Items.Add("County")
            cboFilterOption5.Items.Add("District")
            cboFilterOption5.Items.Add("District Engineer")
            cboFilterOption5.Items.Add("District Responsible")
            cboFilterOption5.Items.Add("Engineer")
            cboFilterOption5.Items.Add("Facility Name")
            cboFilterOption5.Items.Add("Operational Status")
            cboFilterOption5.Items.Add("SIC Codes")
            cboFilterOption5.Items.Add("SSCP Unit")

            cboFilterOption5.SelectedIndex = 0

            '--- This loads the Combo Box Sort Option 1
            cboSortOption1.Items.Add("<Select a Filter Option>")
            cboSortOption1.Items.Add("AIRS Number")
            cboSortOption1.Items.Add("City")
            cboSortOption1.Items.Add("Classification")
            cboSortOption1.Items.Add("County")
            cboSortOption1.Items.Add("District")
            cboSortOption1.Items.Add("District Engineer")
            cboSortOption1.Items.Add("District Responsible")
            cboSortOption1.Items.Add("Engineer")
            cboSortOption1.Items.Add("Facility Name")
            cboSortOption1.Items.Add("Operational Status")
            cboSortOption1.Items.Add("SIC Codes")
            cboSortOption1.Items.Add("SSCP Unit")

            cboSortOption1.SelectedIndex = 0

            '--- This loads the Combo Box Sort Option 2
            cboSortOption2.Items.Add("<Select a Filter Option>")
            cboSortOption2.Items.Add("AIRS Number")
            cboSortOption2.Items.Add("City")
            cboSortOption2.Items.Add("Classification")
            cboSortOption2.Items.Add("County")
            cboSortOption2.Items.Add("District")
            cboSortOption2.Items.Add("District Engineer")
            cboSortOption2.Items.Add("District Responsible")
            cboSortOption2.Items.Add("Engineer")
            cboSortOption2.Items.Add("Facility Name")
            cboSortOption2.Items.Add("Operational Status")
            cboSortOption2.Items.Add("SIC Codes")
            cboSortOption2.Items.Add("SSCP Unit")

            cboSortOption2.SelectedIndex = 0

            '--- This loads the Combo Box Sort Option 3
            cboSortOption3.Items.Add("<Select a Filter Option>")
            cboSortOption3.Items.Add("AIRS Number")
            cboSortOption3.Items.Add("City")
            cboSortOption3.Items.Add("Classification")
            cboSortOption3.Items.Add("County")
            cboSortOption3.Items.Add("District")
            cboSortOption3.Items.Add("District Engineer")
            cboSortOption3.Items.Add("District Responsible")
            cboSortOption3.Items.Add("Engineer")
            cboSortOption3.Items.Add("Facility Name")
            cboSortOption3.Items.Add("Operational Status")
            cboSortOption3.Items.Add("SIC Codes")
            cboSortOption3.Items.Add("SSCP Unit")

            cboSortOption3.SelectedIndex = 0

            cboCMSFrequency.Items.Add(" ")
            cboCMSFrequency.Items.Add("A")
            cboCMSFrequency.Items.Add("S")
            cboCMSFrequency.Items.Add("A & S")

            cboCMSWarningFrequency.Items.Add(" ")
            cboCMSWarningFrequency.Items.Add("A")
            cboCMSWarningFrequency.Items.Add("S")
            cboCMSWarningFrequency.Items.Add("A & S")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub FormatdgrInspectionList()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "InspectionList"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            '0   
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionKey"
            objtextcol.HeaderText = "Inspection Key"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            '1   
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "AIRSNUMBER"
            objtextcol.HeaderText = "AIRS Number"
            objtextcol.Format = "N"
            objtextcol.Width = 80
            objtextcol.Alignment = HorizontalAlignment.Center
            objGrid.GridColumnStyles.Add(objtextcol)

            '2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRFACILITYNAME"
            objtextcol.HeaderText = "Facility Name"
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            '3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRFACILITYCITY"
            objtextcol.HeaderText = "Facility City"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            '4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "STRCOUNTYNAME"
            objtextcol.HeaderText = "County Name"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ENGINEER"
            objtextcol.HeaderText = "Engineer"
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            '6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DATSCHEDULEDATESTART"
            objtextcol.HeaderText = "Initial Inspection Start"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            '7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DATSCHEDULEDATEEND"
            objtextcol.HeaderText = "Initial Inspection End"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            '8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DATCURRENTDATESTART"
            objtextcol.HeaderText = "Current Inspection Start"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            '9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DATCURRENTDATEEND"
            objtextcol.HeaderText = "Current Inspection End"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            '10
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DATACTUALDATESTART"
            objtextcol.HeaderText = "Actual Inspection Start"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            '11
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DATACTUALDATEEND"
            objtextcol.HeaderText = "Actual Inspection End"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrInspectionList.TableStyles.Clear()
            dgrInspectionList.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrInspectionList.CaptionText = "Inspections Schedule"
            dgrInspectionList.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadComboYear()
        Dim YearValue As Integer = "00"
        Dim LastYear As String = Date.Today.AddYears(1).Year

        Try

            cboYear.Items.Clear()

            If LastYear.Length = 4 Then
                LastYear = Mid(LastYear, 3)
            Else
                LastYear = "00"
            End If

            YearValue = CInt(LastYear)

            Do While YearValue <> 0
                Select Case CStr(YearValue).Length
                    Case "1"
                        cboYear.Items.Add("200" & YearValue)
                    Case "2"
                        cboYear.Items.Add("20" & YearValue)
                    Case Else
                        cboYear.Items.Add(YearValue)
                End Select
                YearValue -= 1
            Loop

            cboYear.Items.Add("2000")

            cboYear.Text = cboYear.Items.Item(0)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadInspectionEngineers()
        Try


            SQL = "select distinct(strLastName||', '||strFirstName) as Engineers, " & _
            "strLastName  " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCPInspectionTracking " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCPInspectionTracking.strInspectingEngineer " & _
            "order by strLastName  "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                clbEngineerInspections.Items.Add(dr.Item("Engineers"))
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "from " & DBNameSpace & ".EPDUserProfiles " & _
            "where numProgram = '4' " & _
            "and numUnit is null " & _
            "order by strLastName "

            dsAdminStaff = New DataSet
            daAdminStaff = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daAdminStaff.Fill(dsAdminStaff, "AdminStaff")

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "numUserID " & _
            "from " & DBNameSpace & ".EPDUserProfiles " & _
            "where numProgram = '4' " & _
            "and numUnit = '30' " & _
            "order by strLastName "

            dsAirStaff = New DataSet
            daAirStaff = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daAirStaff.Fill(dsAirStaff, "AirStaff")

            SQL = "Select " & _
            "(strLastName||', '||strFirstName) as UserName, " & _
            "numUserID " & _
            "from " & DBNameSpace & ".EPDUserProfiles " & _
            "where numProgram = '4' " & _
            "and numUnit = '31' " & _
            "order by strLastName "

            dsChemStaff = New DataSet
            daChemStaff = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daChemStaff.Fill(dsChemStaff, "ChemStaff")

            SQL = "Select " & _
             "(strLastName||', '||strFirstName) as UserName, " & _
             "numUserID " & _
             "from " & DBNameSpace & ".EPDUserProfiles " & _
             "where numProgram = '4' " & _
             "and numUnit = '32' " & _
             "order by strLastName "

            dsVOCStaff = New DataSet
            daVOCStaff = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            daDistrictStaff = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
    Sub CreateListDataSet()
        Dim SQLFilter1 As String = ""
        Dim SQLFilter2 As String = ""
        Dim SQLFilter3 As String = ""
        Dim SQLFilter4 As String = ""
        Dim SQLFilter5 As String = ""
        Dim SQLOR1 As String = ""
        Dim SQLOR2 As String = ""
        Dim SQLOR3 As String = ""
        Dim SQLOR4 As String = ""
        Dim SQLKey As String = "00000"
        Dim SQLWhere As String = ""
        Dim SQLSort1 As String = ""
        Dim SQLSort2 As String = ""
        Dim SQLSort3 As String = ""
        Dim SQLOrder As String = ""
        Dim dtEngineers As New DataTable
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try


            SQL = "Select * " & _
                "from " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment "

            If cboFilterOption1.Items.Contains(cboFilterOption1.Text) And txtSearchText1.Text <> "" Then
                Select Case cboFilterOption1.Text
                    Case "AIRS Number"
                        SQLFilter1 = " UPPER(strAIRSNumber) like UPPER('%" & txtSearchText1.Text & "%') "
                    Case "City"
                        SQLFilter1 = " UPPER(strFacilityCity) like UPPER('%" & txtSearchText1.Text & "%') "
                    Case "Classification"
                        SQLFilter1 = " strClass = '" & txtSearchText1.Text & "' "
                    Case "County"
                        SQLFilter1 = " UPPER(strCountyName) like UPPER('%" & txtSearchText1.Text & "%') "
                    Case "District"
                        SQLFilter1 = " UPPER(strDistrictName) like UPPER('%" & txtSearchText1.Text & "%') "
                    Case "District Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText1.Text & "%')", "UserName")
                        SQLFilter1 = ""
                        For Each row In drEngineers
                            SQLFilter1 = " strDistrictEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter1 <> "" Then
                            SQLFilter1 = Mid(SQLFilter1, 1, (Len(SQLFilter1) - 3))
                        End If
                    Case "District Responsible"
                        SQLFilter1 = " upper(strDistrictResponsible) like Upper('%" & txtSearchText1.Text & "%') "
                    Case "Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText1.Text & "%')", "UserName")
                        SQLFilter1 = ""
                        For Each row In drEngineers
                            SQLFilter1 = " strSSCPEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter1 <> "" Then
                            SQLFilter1 = Mid(SQLFilter1, 1, (Len(SQLFilter1) - 3))
                        End If
                    Case "Facility Name"
                        SQLFilter1 = " UPPER(strFacilityName) like UPPER('%" & txtSearchText1.Text & "%') "
                    Case "Operational Status"
                        SQLFilter1 = " strOperationalStatus = '" & Mid(txtSearchText1.Text, 1, 1) & "' "
                    Case "SIC Codes"
                        SQLFilter1 = " UPPER(strSICCode) like UPPER('%" & txtSearchText1.Text & "%') "
                    Case "SSCP Unit"
                        SQLFilter1 = " UPPER(strUnitDesc) Like UPPER('%" & txtSearchText1.Text & "%') "
                    Case Else
                        SQLFilter1 = ""
                End Select
            End If

            If cboFilterOption2.Items.Contains(cboFilterOption2.Text) And txtSearchText2.Text <> "" Then
                Select Case cboFilterOption2.Text
                    Case "AIRS Number"
                        SQLFilter2 = " UPPER(strAIRSNumber) like UPPER('%" & txtSearchText2.Text & "%') "
                    Case "City"
                        SQLFilter2 = " UPPER(strFacilityCity) like UPPER('%" & txtSearchText2.Text & "%') "
                    Case "Classification"
                        SQLFilter2 = " strClass = '" & txtSearchText2.Text & "' "
                    Case "County"
                        SQLFilter2 = " UPPER(strCountyName) like UPPER('%" & txtSearchText2.Text & "%') "
                    Case "District"
                        SQLFilter2 = " UPPER(strDistrictName) like UPPER('%" & txtSearchText2.Text & "%') "
                    Case "District Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText2.Text & "%')", "UserName")
                        SQLFilter2 = ""
                        For Each row In drEngineers
                            SQLFilter2 = " strDistrictEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter2 <> "" Then
                            SQLFilter2 = Mid(SQLFilter2, 1, (Len(SQLFilter2) - 3))
                        End If
                    Case "District Responsible"
                        SQLFilter2 = " upper(strDistrictResponsible) like Upper('%" & txtSearchText2.Text & "%') "
                    Case "Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText2.Text & "%')", "UserName")
                        SQLFilter2 = ""
                        For Each row In drEngineers
                            SQLFilter2 = " strSSCPEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter2 <> "" Then
                            SQLFilter2 = Mid(SQLFilter2, 1, (Len(SQLFilter2) - 3))
                        End If
                    Case "Facility Name"
                        SQLFilter2 = " UPPER(strFacilityName) like UPPER('%" & txtSearchText2.Text & "%') "
                    Case "Operational Status"
                        SQLFilter2 = " strOperationalStatus = '" & Mid(txtSearchText2.Text, 1, 1) & "' "
                    Case "SIC Codes"
                        SQLFilter2 = " UPPER(strSICCode) like UPPER('%" & txtSearchText2.Text & "%') "
                    Case "SSCP Unit"
                        SQLFilter2 = " UPPER(strUnitDesc) Like UPPER('%" & txtSearchText2.Text & "%') "
                    Case Else
                        SQLFilter2 = ""
                End Select
            End If

            If cboFilterOption3.Items.Contains(cboFilterOption3.Text) And txtSearchText3.Text <> "" Then
                Select Case cboFilterOption3.Text
                    Case "AIRS Number"
                        SQLFilter3 = " UPPER(strAIRSNumber) like UPPER('%" & txtSearchText3.Text & "%') "
                    Case "City"
                        SQLFilter3 = " UPPER(strFacilityCity) like UPPER('%" & txtSearchText3.Text & "%') "
                    Case "Classification"
                        SQLFilter3 = " strClass = '" & txtSearchText3.Text & "' "
                    Case "County"
                        SQLFilter3 = " UPPER(strCountyName) like UPPER('%" & txtSearchText3.Text & "%') "
                    Case "District"
                        SQLFilter3 = " UPPER(strDistrictName) like UPPER('%" & txtSearchText3.Text & "%') "
                    Case "District Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText3.Text & "%')", "UserName")
                        SQLFilter3 = ""
                        For Each row In drEngineers
                            SQLFilter3 = " strDistrictEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter3 <> "" Then
                            SQLFilter3 = Mid(SQLFilter3, 1, (Len(SQLFilter3) - 3))
                        End If
                    Case "District Responsible"
                        SQLFilter3 = " upper(strDistrictResponsible) like Upper('%" & txtSearchText3.Text & "%') "
                    Case "Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText3.Text & "%')", "UserName")
                        SQLFilter3 = ""
                        For Each row In drEngineers
                            SQLFilter3 = " strSSCPEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter3 <> "" Then
                            SQLFilter3 = Mid(SQLFilter3, 1, (Len(SQLFilter3) - 3))
                        End If
                    Case "Facility Name"
                        SQLFilter3 = " UPPER(strFacilityName) like UPPER('%" & txtSearchText3.Text & "%') "
                    Case "Operational Status"
                        SQLFilter3 = " strOperationalStatus = '" & Mid(txtSearchText3.Text, 1, 1) & "' "
                    Case "SIC Codes"
                        SQLFilter3 = " UPPER(strSICCode) like UPPER('%" & txtSearchText3.Text & "%') "
                    Case "SSCP Unit"
                        SQLFilter3 = " UPPER(strUnitDesc) Like UPPER('%" & txtSearchText3.Text & "%') "
                    Case Else
                        SQLFilter3 = ""
                End Select
            End If

            If cboFilterOption4.Items.Contains(cboFilterOption4.Text) And txtSearchText4.Text <> "" Then
                Select Case cboFilterOption4.Text
                    Case "AIRS Number"
                        SQLFilter4 = " UPPER(strAIRSNumber) like UPPER('%" & txtSearchText4.Text & "%') "
                    Case "City"
                        SQLFilter4 = " UPPER(strFacilityCity) like UPPER('%" & txtSearchText4.Text & "%') "
                    Case "Classification"
                        SQLFilter4 = " strClass = '" & txtSearchText4.Text & "' "
                    Case "County"
                        SQLFilter4 = " UPPER(strCountyName) like UPPER('%" & txtSearchText4.Text & "%') "
                    Case "District"
                        SQLFilter4 = " UPPER(strDistrictName) like UPPER('%" & txtSearchText4.Text & "%') "
                    Case "District Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText4.Text & "%')", "UserName")
                        SQLFilter4 = ""
                        For Each row In drEngineers
                            SQLFilter4 = " strDistrictEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter1 <> "" Then
                            SQLFilter4 = Mid(SQLFilter4, 1, (Len(SQLFilter4) - 3))
                        End If
                    Case "District Responsible"
                        SQLFilter4 = " upper(strDistrictResponsible) like Upper('%" & txtSearchText4.Text & "%') "
                    Case "Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText4.Text & "%')", "UserName")
                        SQLFilter4 = ""
                        For Each row In drEngineers
                            SQLFilter4 = " strSSCPEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter4 <> "" Then
                            SQLFilter4 = Mid(SQLFilter4, 1, (Len(SQLFilter4) - 3))
                        End If
                    Case "Facility Name"
                        SQLFilter4 = " UPPER(strFacilityName) like UPPER('%" & txtSearchText4.Text & "%') "
                    Case "Operational Status"
                        SQLFilter4 = " strOperationalStatus = '" & Mid(txtSearchText4.Text, 1, 1) & "' "
                    Case "SIC Codes"
                        SQLFilter4 = " UPPER(strSICCode) like UPPER('%" & txtSearchText4.Text & "%') "
                    Case "SSCP Unit"
                        SQLFilter4 = " UPPER(strUnitDesc) Like UPPER('%" & txtSearchText4.Text & "%') "
                    Case Else
                        SQLFilter4 = ""
                End Select
            End If

            If cboFilterOption5.Items.Contains(cboFilterOption5.Text) And txtSearchText5.Text <> "" Then
                Select Case cboFilterOption5.Text
                    Case "AIRS Number"
                        SQLFilter5 = " UPPER(strAIRSNumber) like UPPER('%" & txtSearchText5.Text & "%') "
                    Case "City"
                        SQLFilter5 = " UPPER(strFacilityCity) like UPPER('%" & txtSearchText5.Text & "%') "
                    Case "Classification"
                        SQLFilter5 = " strClass = '" & txtSearchText5.Text & "' "
                    Case "County"
                        SQLFilter5 = " UPPER(strCountyName) like UPPER('%" & txtSearchText5.Text & "%') "
                    Case "District"
                        SQLFilter5 = " UPPER(strDistrictName) like UPPER('%" & txtSearchText5.Text & "%') "
                    Case "District Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText5.Text & "%')", "UserName")
                        SQLFilter5 = ""
                        For Each row In drEngineers
                            SQLFilter5 = " strDistrictEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter5 <> "" Then
                            SQLFilter5 = Mid(SQLFilter5, 1, (Len(SQLFilter5) - 3))
                        End If
                    Case "District Responsible"
                        SQLFilter5 = " upper(strDistrictResponsible) like Upper('%" & txtSearchText5.Text & "%') "
                    Case "Engineer"
                        dtEngineers = dsStaff.Tables("Staff")
                        drEngineers = dtEngineers.Select("(UserName) Like ('%" & txtSearchText5.Text & "%')", "UserName")
                        SQLFilter5 = ""
                        For Each row In drEngineers
                            SQLFilter5 = " strSSCPEngineer = '" & row("numUserID") & "' Or "
                        Next
                        If SQLFilter5 <> "" Then
                            SQLFilter5 = Mid(SQLFilter5, 1, (Len(SQLFilter5) - 3))
                        End If
                    Case "Facility Name"
                        SQLFilter5 = " UPPER(strFacilityName) like UPPER('%" & txtSearchText5.Text & "%') "
                    Case "Operational Status"
                        SQLFilter5 = " strOperationalStatus = '" & Mid(txtSearchText5.Text, 1, 1) & "' "
                    Case "SIC Codes"
                        SQLFilter5 = " UPPER(strSICCode) like UPPER('%" & txtSearchText5.Text & "%') "
                    Case "SSCP Unit"
                        SQLFilter5 = " UPPER(strUnitDesc) Like UPPER('%" & txtSearchText5.Text & "%') "
                    Case Else
                        SQLFilter5 = ""
                End Select
            End If

            If cboSortOption1.Items.Contains(cboSortOption1.Text) And cboSortOption1.SelectedIndex <> 0 Then
                Select Case cboSortOption1.Text
                    Case "AIRS Number"
                        SQLSort1 = " strAIRSNumber"
                    Case "City"
                        SQLSort1 = " strFacilityCity"
                    Case "Classification"
                        SQLSort1 = " strClass"
                    Case "County"
                        SQLSort1 = " strCountyName"
                    Case "District"
                        SQLSort1 = " strDistrictName"
                    Case "District Engineer"
                        SQLSort1 = " strDistrictEngineer"
                    Case "District Responsible"
                        SQLSort1 = " strDistrictResponsible"
                    Case "Engineer"
                        SQLSort1 = " strSSCPEngineer"
                    Case "Facility Name"
                        SQLSort1 = " strFacilityName"
                    Case "Operational Status"
                        SQLSort1 = " strOperationalStatus"
                    Case "SIC Codes"
                        SQLSort1 = " strSICCode"
                    Case "SSCP Unit"
                        SQLSort1 = " strUnitDesc"
                    Case Else
                        SQLSort1 = ""
                End Select
            End If

            If cboSortOption2.Items.Contains(cboSortOption2.Text) And cboSortOption2.SelectedIndex <> 0 Then
                Select Case cboSortOption2.Text
                    Case "AIRS Number"
                        SQLSort2 = " strAIRSNumber"
                    Case "City"
                        SQLSort2 = " strFacilityCity"
                    Case "Classification"
                        SQLSort2 = " strClass"
                    Case "County"
                        SQLSort2 = " strCountyName'"
                    Case "District"
                        SQLSort2 = " strDistrictName"
                    Case "District Engineer"
                        SQLSort2 = " strDistrictEngineer"
                    Case "District Responsible"
                        SQLSort2 = " strDistrictResponsible"
                    Case "Engineer"
                        SQLSort2 = " strSSCPEngineer"
                    Case "Facility Name"
                        SQLSort2 = " strFacilityName"
                    Case "Operational Status"
                        SQLSort2 = " strOperationalStatus"
                    Case "SIC Codes"
                        SQLSort2 = " strSICCode"
                    Case "SSCP Unit"
                        SQLSort2 = " strUnitDesc"
                    Case Else
                        SQLSort2 = ""
                End Select
            End If

            If cboSortOption3.Items.Contains(cboSortOption3.Text) And cboSortOption3.SelectedIndex <> 0 Then
                Select Case cboSortOption3.Text
                    Case "AIRS Number"
                        SQLSort3 = " strAIRSNumber"
                    Case "City"
                        SQLSort3 = " strFacilityCity"
                    Case "Classification"
                        SQLSort3 = " strClass"
                    Case "County"
                        SQLSort3 = " strCountyName"
                    Case "District"
                        SQLSort3 = " strDistrictName"
                    Case "District Engineer"
                        SQLSort3 = " strDistrictEngineer"
                    Case "District Responsible"
                        SQLSort3 = " strDistrictResponsible"
                    Case "Engineer"
                        SQLSort3 = " strSSCPEngineer"
                    Case "Facility Name"
                        SQLSort3 = " strFacilityName"
                    Case "Operational Status"
                        SQLSort3 = " strOperationalStatus"
                    Case "SIC Codes"
                        SQLSort3 = " strSICCode"
                    Case "SSCP Unit"
                        SQLSort3 = " strUnitDesc"
                    Case Else
                        SQLSort3 = ""
                End Select
            End If

            SQLOR1 = ""
            SQLOR2 = ""
            SQLOR3 = ""
            SQLOR4 = ""

            If cboFilterOption1.Text = cboFilterOption2.Text And cboFilterOption1.SelectedIndex <> 0 Then
                SQLOR1 = SQLOR1 & SQLFilter1 & " OR " & SQLFilter2 & " OR "
                SQLKey = "11" & Mid(SQLKey, 3)
            End If
            If cboFilterOption1.Text = cboFilterOption3.Text And cboFilterOption1.SelectedIndex <> 0 Then
                SQLOR1 = SQLOR1 & SQLFilter1 & " OR " & SQLFilter3 & " OR "
                SQLKey = "1" & Mid(SQLKey, 2, 1) & "1" & Mid(SQLKey, 4)
            End If
            If cboFilterOption1.Text = cboFilterOption4.Text And cboFilterOption1.SelectedIndex <> 0 Then
                SQLOR1 = SQLOR1 & SQLFilter1 & " OR " & SQLFilter4 & " OR "
                SQLKey = "1" & Mid(SQLKey, 2, 2) & "1" & Mid(SQLKey, 5)
            End If
            If cboFilterOption1.Text = cboFilterOption5.Text And cboFilterOption1.SelectedIndex <> 0 Then
                SQLOR1 = SQLOR1 & SQLFilter1 & " OR " & SQLFilter5 & " OR "
                SQLKey = "1" & Mid(SQLKey, 2, 3) & "1"
            End If

            If cboFilterOption2.Text = cboFilterOption3.Text And cboFilterOption1.Text <> cboFilterOption2.Text _
                  And cboFilterOption2.SelectedIndex <> 0 Then
                SQLOR2 = SQLOR2 & SQLFilter2 & " OR " & SQLFilter3 & " OR "
                SQLKey = Mid(SQLKey, 1, 1) & "11" & Mid(SQLKey, 4)
            End If
            If cboFilterOption2.Text = cboFilterOption4.Text And cboFilterOption1.Text <> cboFilterOption2.Text _
                  And cboFilterOption2.SelectedIndex <> 0 Then
                SQLOR2 = SQLOR2 & SQLFilter2 & " OR " & SQLFilter4 & " OR "
                SQLKey = Mid(SQLKey, 1, 1) & "1" & Mid(SQLKey, 3, 1) & "1" & Mid(SQLKey, 5)
            End If
            If cboFilterOption2.Text = cboFilterOption5.Text And cboFilterOption1.Text <> cboFilterOption2.Text _
                  And cboFilterOption2.SelectedIndex <> 0 Then
                SQLOR2 = SQLOR2 & SQLFilter2 & " OR " & SQLFilter5 & " OR "
                SQLKey = Mid(SQLKey, 1, 1) & "1" & Mid(SQLKey, 3, 2) & "1"
            End If

            If cboFilterOption3.Text = cboFilterOption4.Text And cboFilterOption1.Text <> cboFilterOption3.Text _
                  And cboFilterOption2.Text <> cboFilterOption3.Text And cboFilterOption3.SelectedIndex <> 0 Then
                SQLOR3 = SQLOR3 & SQLFilter3 & " OR " & SQLFilter4 & " OR "
                SQLKey = Mid(SQLKey, 1, 2) & "11" & Mid(SQLKey, 5)
            End If
            If cboFilterOption3.Text = cboFilterOption5.Text And cboFilterOption1.Text <> cboFilterOption3.Text _
                  And cboFilterOption2.Text <> cboFilterOption3.Text And cboFilterOption3.SelectedIndex <> 0 Then
                SQLOR3 = SQLOR3 & SQLFilter3 & " OR " & SQLFilter5 & " OR "
                SQLKey = Mid(SQLKey, 1, 2) & "1" & Mid(SQLKey, 4, 1) & "1"
            End If

            If cboFilterOption4.Text = cboFilterOption5.Text And cboFilterOption1.Text <> cboFilterOption4.Text _
                  And cboFilterOption2.Text <> cboFilterOption4.Text And cboFilterOption3.Text <> cboFilterOption4.Text _
                  And cboFilterOption4.SelectedIndex <> 0 Then
                SQLOR4 = SQLFilter4 & " OR " & SQLFilter5 & " OR "
                SQLKey = Mid(SQLKey, 1, 3) & "11"
            End If

            If SQLOR1 <> "" Then
                SQLOR1 = Mid(SQLOR1, 1, (Len(SQLOR1) - 3))
                SQLOR1 = " (" & SQLOR1 & ") "
            End If
            If SQLOR2 <> "" Then
                SQLOR2 = Mid(SQLOR2, 1, (Len(SQLOR2) - 3))
                SQLOR2 = " (" & SQLOR2 & ") "
            End If
            If SQLOR3 <> "" Then
                SQLOR3 = Mid(SQLOR3, 1, (Len(SQLOR3) - 3))
                SQLOR3 = " (" & SQLOR3 & ") "
            End If
            If SQLOR4 <> "" Then
                SQLOR4 = Mid(SQLOR4, 1, (Len(SQLOR4) - 3))
                SQLOR4 = " (" & SQLOR4 & ") "
            End If

            If SQLFilter1 <> "" Or SQLFilter2 <> "" Or SQLFilter3 <> "" Or SQLFilter4 <> "" Or SQLFilter5 <> "" Then
                SQLWhere = "Where "

                If SQLKey <> "00000" Then
                    If SQLOR1 <> "" Then
                        SQLWhere = SQLWhere & SQLOR1 & " AND "
                    End If
                    If SQLOR2 <> "" Then
                        SQLWhere = SQLWhere & SQLOR2 & " AND "
                    End If
                    If SQLOR3 <> "" Then
                        SQLWhere = SQLWhere & SQLOR3 & " AND "
                    End If
                    If SQLOR4 <> "" Then
                        SQLWhere = SQLWhere & SQLOR4 & " AND "
                    End If
                End If

                If Mid(SQLKey, 1, 1) = "0" And SQLFilter1 <> "" Then
                    SQLWhere = SQLWhere & SQLFilter1 & " AND "
                End If
                If Mid(SQLKey, 2, 1) = "0" And SQLFilter2 <> "" Then
                    SQLWhere = SQLWhere & SQLFilter2 & " AND "
                End If
                If Mid(SQLKey, 3, 1) = "0" And SQLFilter3 <> "" Then
                    SQLWhere = SQLWhere & SQLFilter3 & " AND "
                End If
                If Mid(SQLKey, 4, 1) = "0" And SQLFilter4 <> "" Then
                    SQLWhere = SQLWhere & SQLFilter4 & " AND "
                End If
                If Mid(SQLKey, 5, 1) = "0" And SQLFilter5 <> "" Then
                    SQLWhere = SQLWhere & SQLFilter5 & " AND "
                End If
                SQLWhere = Mid(SQLWhere, 1, (Len(SQLWhere) - 4))
                SQL = SQL & SQLWhere
            Else
                '  SQL = SQL
            End If

            If SQLSort1 <> "" Or SQLSort2 <> "" Or SQLSort3 <> "" Then
                SQLOrder = " Order by "
                If SQLSort1 <> "" Then
                    SQLOrder = SQLOrder & SQLSort1 & ", "
                End If
                If SQLSort2 <> "" Then
                    SQLOrder = SQLOrder & SQLSort2 & ", "
                End If
                If SQLSort3 <> "" Then
                    SQLOrder = SQLOrder & SQLSort3 & ", "
                End If
                SQLOrder = Mid(SQLOrder, 1, (Len(SQLOrder) - 2))
            End If

            If SQLOrder <> "" Then
                SQL = SQL & SQLOrder
            Else
                '  SQL = SQL
            End If

            dsFacilityAssignment = New DataSet

            daFacilityAssignment = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daFacilityAssignment.Fill(dsFacilityAssignment, "FacilityAssignment")

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            txtFilterType.Text = "DataSet1"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub CreateListDataSet2()
        Dim SQLFilter As String = ""
        Dim i As Integer

        Try

            SQL = "Select * " & _
                  "from " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment "

            If lsbAIRSNumber.Items.Count <> 0 Then
                For i = 0 To lsbAIRSNumber.Items.Count - 1
                    SQLFilter = SQLFilter & " strAIRSNumber = '0413" & lsbAIRSNumber.Items.Item(i) & "' Or "
                Next
                If SQLFilter <> "" Then
                    SQLFilter = Mid(SQLFilter, 1, (Len(SQLFilter) - 4))
                    SQLFilter = "where ( " & SQLFilter & " ) "
                End If
            Else
                SQLFilter = ""
            End If

            SQL = SQL & SQLFilter

            dsFacilityAssignment = New DataSet

            daFacilityAssignment = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daFacilityAssignment.Fill(dsFacilityAssignment, "FacilityAssignment")

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            txtFilterType.Text = "DataSet2"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadListView()
        Dim AIRS_Number As String
        Dim District_Engineer As String
        Dim DistEngName As String
        Dim Engineer As String
        Dim EngName As String
        Dim Last_FCE As String
        Dim Last_Inspection As String
        Dim count As Integer = 0

        Dim temp As String

        Dim ColumnArray(1, 13) As String
        Dim i As Integer

        Try


            lvFacilityList.View = View.Details
            lvFacilityList.AllowColumnReorder = True
            lvFacilityList.CheckBoxes = True
            lvFacilityList.GridLines = True
            lvFacilityList.FullRowSelect = True

            Dim dtFacilityAssignment As New DataTable
            dtFacilityAssignment = dsFacilityAssignment.Tables("FacilityAssignment")
            Dim dtEngineers As New DataTable
            dtEngineers = dsStaff.Tables("Staff")

            Dim drFacilityInfo As DataRow()
            Dim drEngineer As DataRow()
            Dim row As DataRow
            Dim row2 As DataRow
            Dim row3 As DataRow

            drFacilityInfo = dtFacilityAssignment.Select()
            If lblProfileCode.Text <> Profile_Code Then
                Profile_Code = lblProfileCode.Text
            End If

            If Mid(Profile_Code, 1, 1) = "0" Then
                temp = "15"
            Else
                temp = Mid(Profile_Code, 1, 1)
            End If

            Select Case temp
                Case "A"
                    temp = 10
                Case "B"
                    temp = 11
                Case "C"
                    temp = 12
                Case "D"
                    temp = 13
                Case "E"
                    temp = 14
                Case Else
                    ' temp = temp
            End Select

            lvFacilityList.Columns.Add("AIRS Number", 100, HorizontalAlignment.Left)

            For i = 2 To temp
                Select Case i
                    Case 2
                        If Mid(Profile_Code, 3, 1) = "2" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "2" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "2" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "2" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "2" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "2" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "2" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "2" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "2" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "2" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "2" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "2" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "2" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 3
                        If Mid(Profile_Code, 3, 1) = "3" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "3" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "3" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "3" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "3" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "3" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "3" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "3" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "3" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "3" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "3" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "3" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "3" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 4
                        If Mid(Profile_Code, 3, 1) = "4" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "4" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "4" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "4" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "4" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "4" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "4" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "4" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "4" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "4" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "4" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "4" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "4" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 5
                        If Mid(Profile_Code, 3, 1) = "5" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "5" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "5" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "5" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "5" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "5" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "5" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "5" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "5" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "5" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "5" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "5" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "5" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 6
                        If Mid(Profile_Code, 3, 1) = "6" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "6" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "6" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "6" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "6" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "6" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "6" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "6" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "6" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "6" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "6" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "6" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "6" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 7
                        If Mid(Profile_Code, 3, 1) = "7" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "7" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "7" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "7" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "7" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "7" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "7" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "7" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "7" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "7" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "7" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "7" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "7" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 8
                        If Mid(Profile_Code, 3, 1) = "8" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "8" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "8" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "8" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "8" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "8" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "8" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "8" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "8" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "8" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "8" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "8" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "8" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 9
                        If Mid(Profile_Code, 3, 1) = "9" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "9" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "9" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "9" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "9" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "9" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "9" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "9" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "9" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "9" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "9" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "9" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "9" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 10
                        If Mid(Profile_Code, 3, 1) = "A" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "A" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "A" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "A" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "A" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "A" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "A" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "A" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "A" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "A" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "A" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "A" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "A" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 11
                        If Mid(Profile_Code, 3, 1) = "B" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "B" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "B" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "B" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "B" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "B" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "B" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "B" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "B" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "B" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "B" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "B" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "B" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 12
                        If Mid(Profile_Code, 3, 1) = "C" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "C" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "C" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "C" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "C" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "C" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "C" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "C" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "C" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "C" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "C" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "C" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "C" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 13
                        If Mid(Profile_Code, 3, 1) = "D" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "D" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "D" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "D" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "D" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "D" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "D" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "D" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "D" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "D" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "D" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "D" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "D" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case 14
                        If Mid(Profile_Code, 3, 1) = "E" Then
                            lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 4, 1) = "E" Then
                            lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 5, 1) = "E" Then
                            lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 6, 1) = "E" Then
                            lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 7, 1) = "E" Then
                            lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 8, 1) = "E" Then
                            lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 9, 1) = "E" Then
                            lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 10, 1) = "E" Then
                            lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 11, 1) = "E" Then
                            lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 12, 1) = "E" Then
                            lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 13, 1) = "E" Then
                            lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 14, 1) = "E" Then
                            lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        End If
                        If Mid(Profile_Code, 15, 1) = "E" Then
                            lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                        End If
                    Case "15"
                        lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                    Case Else
                        lvFacilityList.Columns.Add("City", 90, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Source Class", 75, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("County", 55, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("District", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("District Engineer", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("District Responsible", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Engineer", 80, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Facility Name", 160, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Last FCE", 50, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Last Inspection", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Operational Status", 100, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("SIC Code", 60, HorizontalAlignment.Left)
                        lvFacilityList.Columns.Add("Unit Assigned", 105, HorizontalAlignment.Left)
                End Select
            Next

            For Each row In drFacilityInfo
                AIRS_Number = Mid(row("strAIRSNumber").ToString(), 5)

                If Mid(Profile_Code, 3, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 3, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strFacilityCity").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strFacilityCity").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strFacilityCity").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strFacilityCity").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strFacilityCity").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strFacilityCity").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strFacilityCity").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strFacilityCity").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strFacilityCity").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strFacilityCity").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strFacilityCity").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strFacilityCity").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strFacilityCity").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 2) = row("strFacilityCity").ToString()
                    End If
                End If
                If Mid(Profile_Code, 4, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 4, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strClass").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strClass").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strClass").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strClass").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strClass").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strClass").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strClass").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strClass").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strClass").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strClass").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strClass").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strClass").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strClass").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 3) = row("strClass").ToString()
                    End If
                End If
                If Mid(Profile_Code, 5, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 5, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strCountyName").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strCountyName").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strCountyName").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strCountyName").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strCountyName").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strCountyName").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strCountyName").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strCountyName").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strCountyName").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strCountyName").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strCountyName").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strCountyName").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strCountyName").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 4) = row("strCountyName").ToString()
                    End If
                End If

                If Mid(Profile_Code, 6, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 6, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strDistrictName").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strDistrictName").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strDistrictName").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strDistrictName").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strDistrictName").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strDistrictName").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strDistrictName").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strDistrictName").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strDistrictName").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strDistrictName").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strDistrictName").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strDistrictName").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strDistrictName").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 5) = row("strDistrictName").ToString()
                    End If
                End If


                DistEngName = ""
                District_Engineer = row("strDistrictEngineer").ToString()
                If District_Engineer = "" Then
                    DistEngName = ""
                Else
                    drEngineer = dtEngineers.Select("numUserID = '" & District_Engineer & "'")

                    If drEngineer.Length = 0 Then
                        DistEngName = ""
                    End If
                    For Each row3 In drEngineer
                        DistEngName = row3("UserName").ToString()
                    Next row3
                End If

                If Mid(Profile_Code, 7, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 7, 1)
                        Case "2"
                            ColumnArray(1, 2) = DistEngName
                        Case "3"
                            ColumnArray(1, 2) = DistEngName
                        Case "4"
                            ColumnArray(1, 4) = DistEngName
                        Case "5"
                            ColumnArray(1, 5) = DistEngName
                        Case "6"
                            ColumnArray(1, 6) = DistEngName
                        Case "7"
                            ColumnArray(1, 7) = DistEngName
                        Case "8"
                            ColumnArray(1, 8) = DistEngName
                        Case "9"
                            ColumnArray(1, 9) = DistEngName
                        Case "A"
                            ColumnArray(1, 10) = DistEngName
                        Case "B"
                            ColumnArray(1, 11) = DistEngName
                        Case "C"
                            ColumnArray(1, 12) = DistEngName
                        Case "D"
                            ColumnArray(1, 13) = DistEngName
                        Case "E"
                            ColumnArray(1, 14) = DistEngName
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 6) = DistEngName
                    End If
                End If


                If Mid(Profile_Code, 8, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 8, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strDistrictResponsible").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strDistrictResponsible").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strDistrictResponsible").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strDistrictResponsible").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strDistrictResponsible").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strDistrictResponsible").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strDistrictResponsible").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strDistrictResponsible").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strDistrictResponsible").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strDistrictResponsible").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strDistrictResponsible").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strDistrictResponsible").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strDistrictResponsible").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 7) = row("strDistrictResponsible").ToString()
                    End If
                End If

                EngName = ""
                Engineer = row("strSSCPEngineer").ToString()
                If Engineer = "" Then
                    EngName = ""
                Else
                    drEngineer = dtEngineers.Select("numUserID = '" & Engineer & "'")

                    If drEngineer.Length = 0 Then
                        EngName = ""
                    End If
                    For Each row2 In drEngineer
                        EngName = row2("UserName").ToString()
                    Next row2
                End If

                If Mid(Profile_Code, 9, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 9, 1)
                        Case "2"
                            ColumnArray(1, 2) = EngName
                        Case "3"
                            ColumnArray(1, 3) = EngName
                        Case "4"
                            ColumnArray(1, 4) = EngName
                        Case "5"
                            ColumnArray(1, 5) = EngName
                        Case "6"
                            ColumnArray(1, 6) = EngName
                        Case "7"
                            ColumnArray(1, 7) = EngName
                        Case "8"
                            ColumnArray(1, 8) = EngName
                        Case "9"
                            ColumnArray(1, 9) = EngName
                        Case "A"
                            ColumnArray(1, 10) = EngName
                        Case "B"
                            ColumnArray(1, 11) = EngName
                        Case "C"
                            ColumnArray(1, 12) = EngName
                        Case "D"
                            ColumnArray(1, 13) = EngName
                        Case "E"
                            ColumnArray(1, 14) = EngName
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 8) = EngName
                    End If
                End If

                If Mid(Profile_Code, 10, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 10, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strFacilityName").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strFacilityName").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strFacilityName").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strFacilityName").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strFacilityName").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strFacilityName").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strFacilityName").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strFacilityName").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strFacilityName").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strFacilityName").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strFacilityName").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strFacilityName").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strFacilityName").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 9) = row("strFacilityName").ToString()
                    End If
                End If


                If row("LastFCE").ToString() = "" Then
                    Last_FCE = "7/4/1776"
                Else
                    Last_FCE = CDate(row("LastFCE").ToString())
                End If

                If Last_FCE = "7/4/1776" Then
                    Last_FCE = "Unknown"
                Else
                    ' Last_FCE = Last_FCE
                End If
                If Mid(Profile_Code, 11, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 11, 1)
                        Case "2"
                            ColumnArray(1, 2) = Last_FCE
                        Case "3"
                            ColumnArray(1, 3) = Last_FCE
                        Case "4"
                            ColumnArray(1, 4) = Last_FCE
                        Case "5"
                            ColumnArray(1, 5) = Last_FCE
                        Case "6"
                            ColumnArray(1, 6) = Last_FCE
                        Case "7"
                            ColumnArray(1, 7) = Last_FCE
                        Case "8"
                            ColumnArray(1, 8) = Last_FCE
                        Case "9"
                            ColumnArray(1, 9) = Last_FCE
                        Case "A"
                            ColumnArray(1, 10) = Last_FCE
                        Case "B"
                            ColumnArray(1, 11) = Last_FCE
                        Case "C"
                            ColumnArray(1, 12) = Last_FCE
                        Case "D"
                            ColumnArray(1, 13) = Last_FCE
                        Case "E"
                            ColumnArray(1, 14) = Last_FCE
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 10) = Last_FCE
                    End If
                End If

                If row("LastInspectionDate").ToString() = "" Then
                    Last_Inspection = "7/4/1776"
                Else
                    Last_Inspection = CDate(row("LastInspectionDate").ToString())
                End If

                If Last_Inspection = "7/4/1776" Then
                    Last_Inspection = "Unknown"
                Else
                    'Last_Inspection = Last_Inspection
                End If
                If Mid(Profile_Code, 12, 1) <> "0" Then

                    Select Case Mid(Profile_Code, 12, 1)
                        Case "2"
                            ColumnArray(1, 2) = Last_Inspection
                        Case "3"
                            ColumnArray(1, 3) = Last_Inspection
                        Case "4"
                            ColumnArray(1, 4) = Last_Inspection
                        Case "5"
                            ColumnArray(1, 5) = Last_Inspection
                        Case "6"
                            ColumnArray(1, 6) = Last_Inspection
                        Case "7"
                            ColumnArray(1, 7) = Last_Inspection
                        Case "8"
                            ColumnArray(1, 8) = Last_Inspection
                        Case "9"
                            ColumnArray(1, 9) = Last_Inspection
                        Case "A"
                            ColumnArray(1, 10) = Last_Inspection
                        Case "B"
                            ColumnArray(1, 11) = Last_Inspection
                        Case "C"
                            ColumnArray(1, 12) = Last_Inspection
                        Case "D"
                            ColumnArray(1, 13) = Last_Inspection
                        Case "E"
                            ColumnArray(1, 14) = Last_Inspection
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 11) = Last_Inspection
                    End If
                End If

                If Mid(Profile_Code, 13, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 13, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strOperationalStatus").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strOperationalStatus").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strOperationalStatus").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strOperationalStatus").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strOperationalStatus").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strOperationalStatus").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strOperationalStatus").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strOperationalStatus").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strOperationalStatus").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strOperationalStatus").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strOperationalStatus").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strOperationalStatus").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strOperationalStatus").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 12) = row("strOperationalStatus").ToString()
                    End If
                End If
                If Mid(Profile_Code, 14, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 14, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strSICCode").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strSICCode").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strSICCode").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strSICCode").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strSICCode").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strSICCode").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strSICCode").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strSICCode").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strSICCode").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strSICCode").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strSICCode").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strSICCode").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strSICCode").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 13) = row("strSICCode").ToString()
                    End If
                End If

                If Mid(Profile_Code, 15, 1) <> "0" Then
                    Select Case Mid(Profile_Code, 15, 1)
                        Case "2"
                            ColumnArray(1, 2) = row("strUnitDesc").ToString()
                        Case "3"
                            ColumnArray(1, 3) = row("strUnitDesc").ToString()
                        Case "4"
                            ColumnArray(1, 4) = row("strUnitDesc").ToString()
                        Case "5"
                            ColumnArray(1, 5) = row("strUnitDesc").ToString()
                        Case "6"
                            ColumnArray(1, 6) = row("strUnitDesc").ToString()
                        Case "7"
                            ColumnArray(1, 7) = row("strUnitDesc").ToString()
                        Case "8"
                            ColumnArray(1, 8) = row("strUnitDesc").ToString()
                        Case "9"
                            ColumnArray(1, 9) = row("strUnitDesc").ToString()
                        Case "A"
                            ColumnArray(1, 10) = row("strUnitDesc").ToString()
                        Case "B"
                            ColumnArray(1, 11) = row("strUnitDesc").ToString()
                        Case "C"
                            ColumnArray(1, 12) = row("strUnitDesc").ToString()
                        Case "D"
                            ColumnArray(1, 13) = row("strUnitDesc").ToString()
                        Case "E"
                            ColumnArray(1, 14) = row("strUnitDesc").ToString()
                    End Select
                Else
                    If temp = "15" Then
                        ColumnArray(1, 14) = row("strUnitDesc").ToString()
                    End If
                End If

                Dim item1 As New ListViewItem(AIRS_Number)
                Dim tempshow As String

                item1.Checked = False

                If temp > 1 Then
                    For i = 2 To temp

                        tempshow = ColumnArray(1, i)

                        item1.SubItems.Add(ColumnArray(1, i))
                    Next
                End If
                lvFacilityList.Items.AddRange(New ListViewItem() {item1})
                count = count + 1
            Next row

            txtCount.Text = CStr(count)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub SaveEngineerAssignments()
        Dim temp As String = ""
        Try

            Dim strObject As String
            Dim EngineerGCode As String = "0"

            Dim dtEngineers As New DataTable
            dtEngineers = dsStaff.Tables("Staff")
            Dim drEngineers As DataRow()
            Dim row As DataRow

            drEngineers = dtEngineers.Select("UserName = '" & cboEngineer.Text & "'")
            For Each row In drEngineers
                EngineerGCode = row("numUserID")
            Next

            For Each strObject In lsbSelectedFacilities.Items
                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & strObject.ToString & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If UserUnit = "---" Then
                    temp = ""
                Else
                    temp = UserUnit
                End If
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspecationRequired set " & _
                    "numSSCPUnit = '" & temp & "', " & _
                    "numSSCPEngineer = '" & EngineerGCode & "', " & _
                    "strSSCPAssigningManager = '" & UserGCode & "', " & _
                    "datAssignmentdate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & strObject.ToString & "'" & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspecationRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, numSSCPUnit, " & _
                    "strSSCPAssigningManager, " & _
                    "datAssignmentDate) " & _
                    "values " & _
                    "(select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired) " & _
                    "('0413" & strObject.ToString & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & temp & "', " & _
                    "'" & EngineerGCode & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                temp = "0413" & strObject.ToString & " " & UserGCode & " " & EngineerGCode & vbCrLf

                cmd = New OracleCommand(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            Next


            cboEngineer.Text = ""
            lsbSelectedFacilities.Items.Clear()
            txtFacilitiesSelected.Text = lsbSelectedFacilities.Items.Count
            lvFacilityList.Clear()

            Select Case txtFilterType.Text
                Case "DataSet1"
                    CreateListDataSet()
                Case "DataSet2"
                    CreateListDataSet2()
                Case Else
                    CreateListDataSet()
            End Select

            LoadListView()


            MsgBox("Assignments complete", MsgBoxStyle.Information, "SSCP Managers Tools")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SaveUnitAssignments()
        'Dim strObject As String = ""
        'Dim temp As String = ""

        Try
            ' 
            'For Each strObject In lsbSelectedFacilities.Items

            '    SQL = "Select strAIRSNumber " & _
            '    "from " & DBNameSpace & ".SSCPFacilityAssignment " & _
            '    "where strAIRSNumber = '0413" & strObject.ToString & "' "

            '    cmd = New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    dr = cmd.ExecuteReader
            '    recExist = dr.Read
            '    dr.Close()
            '    If recExist = True Then
            '        SQL = "Update " & DBNameSpace & ".SSCPFacilityAssignment set " & _
            '        "strSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " & _
            '        "strSSCPAssigningManager = '" & UserGCode & "', " & _
            '        "datAssignmentdate = '" & OracleDate & "' " & _
            '        "where strAIRSNumber = '0413" & strObject.ToString & "'"

            '        'SQL = "Update " & DBNameSpace & ".SSCPFacilityAssignment set " & _
            '        '"strSSCPUnit = (select strUnit from " & DBNameSpace & ".APBUnits where strUnitDesc = '" & cboSSCPUnit.Text & "'), " & _
            '        '"strSSCPAssigningManager = '" & UserGCode & "', " & _
            '        '"datAssignmentdate = '" & OracleDate & "' where strAIRSNumber = '0413" & strObject.ToString & "'"

            '    Else
            '        SQL = "Insert into " & DBNameSpace & ".SSCPFacilityAssignment " & _
            '        "(strAIRSNumber, strSSCPUnit, " & _
            '        "strSSCPEngineer, strSSCPAssigningManager, " & _
            '        "datAssignmentDate) " & _
            '        "values " & _
            '        "('0413" & strObject.ToString & "', " & _
            '        "'" & cboSSCPUnit2.SelectedValue & "', " & _
            '        "'', '" & UserGCode & "', " & _
            '        "'" & OracleDate & "') "

            '        ' SQL = "Insert into " & DBNameSpace & ".SSCPFacilityAssignment " & _
            '        '"(strAIRSNumber, strSSCPUnit, " & _
            '        '"strSSCPEngineer, strSSCPAssigningManager, " & _
            '        '"datAssignmentDate) " & _
            '        '"values " & _
            '        '"('0413" & strObject.ToString & "', " & _
            '        '"(select strUnit from " & DBNameSpace & ".APBUnits " & _
            '        '"where strUnitDesc = '" & cboSSCPUnit.Text & "'), " & _
            '        '"'" & UserGCode & "', '" & UserGCode & "', " & _
            '        '"'" & OracleDate & "') "
            '    End If

            '    temp = "0413" & strObject.ToString & vbCrLf

            '    cmd = New OracleCommand(SQL, conn)

            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If

            '    dr = cmd.ExecuteReader

            '    If conn.State = ConnectionState.Open Then
            '        'conn.close()
            '    End If
            'Next

            ' 
            'cboSSCPUnit.Text = ""
            'lsbSelectedFacilities.Items.Clear()
            'txtFacilitiesSelected.Text = lsbSelectedFacilities.Items.Count
            'lvFacilityList.Clear()
            'Select Case txtFilterType.Text
            '    Case "DataSet1"
            '        CreateListDataSet()
            '    Case "DataSet2"
            '        CreateListDataSet2()
            '    Case Else
            '        CreateListDataSet()
            'End Select
            'LoadListView()
            ' 

            'MsgBox("Assignments complete", MsgBoxStyle.Information, "SSCP Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub SaveDistrictAssignments()
        Dim SQL As String
        Dim strObject As String
        Dim Status As String = ""

        Try

            If rdbAssignDistrictSource.Checked = True Then
                Status = "True"
            End If
            If rdbNonDistrictSource.Checked = True Then
                Status = "False"
            End If
            If Status = "" Then
                Status = "False"
            End If

            For Each strObject In lsbSelectedFacilities.Items
                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & strObject & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "update " & DBNameSpace & ".SSCPDistrictResponsible set strdistrictresponsible = '" & Status & "', " & _
                    "strassigningmanager = '" & UserGCode & "', " & _
                    "datassigningdate = '" & OracleDate & "' " & _
                    "where strairsnumber = '0413" & strObject & "'"
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPDistrictResponsible " & _
                    "(strAIRSNumber, strDistrictResponsible, " & _
                    "strAssigningManager, datAssigningDate) " & _
                    "values " & _
                    "('0413" & strObject & "', '" & Status & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd.ExecuteReader()
                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            Next



            cboSSCPUnit.Text = ""
            lsbSelectedFacilities.Items.Clear()
            txtFacilitiesSelected.Text = lsbSelectedFacilities.Items.Count
            lvFacilityList.Clear()
            Select Case txtFilterType.Text
                Case "DataSet1"
                    CreateListDataSet()
                Case "DataSet2"
                    CreateListDataSet2()
                Case Else
                    CreateListDataSet()
            End Select
            LoadListView()


            MsgBox("Assignments complete", MsgBoxStyle.Information, "SSCP Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub SaveInspectionRequired()
        Dim SQL As String
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader
        Dim recExist As Boolean
        Dim strObject As String
        Dim temp As String
        Dim Status As String = ""

        Try

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            If rdbRequiresAnInspection.Checked = True Then
                Status = "True"
            End If
            If rdbDoesNotRequireInspection.Checked = True Then
                Status = "False"
            End If
            If Status = "" Then
                Status = "False"
            End If

            For Each strObject In lsbSelectedFacilities.Items
                temp = "0413" & strObject.ToString

                SQL = "Select strairsnumber from " & DBNameSpace & ".SSCPInspectionsRequired where strairsnumber = '" & temp & "'"

                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsREQUIRED set strInspectionRequired = '" & Status & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '" & temp & "'"
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired values " & _
                    "('" & temp & "', 'True', '" & UserGCode & "', '" & OracleDate & "')"
                End If
                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader
            Next

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If


            lsbSelectedFacilities.Items.Clear()
            txtFacilitiesSelected.Text = lsbSelectedFacilities.Items.Count
            lvFacilityList.Clear()
            Select Case txtFilterType.Text
                Case "DataSet1"
                    CreateListDataSet()
                Case "DataSet2"
                    CreateListDataSet2()
                Case Else
                    CreateListDataSet()
            End Select
            LoadListView()


            MsgBox("Inspection Assignments complete", MsgBoxStyle.Information, "SSCP Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub ViewEngineerSchedule()
        Dim SQLLine As String = ""
        Dim Engineer As String = ""
        Dim Year, Year2 As String

        Try

            If cboYear.Items.Contains(cboYear.Text) Then
                Year = CStr(CInt(cboYear.Text) - 1)
                Year2 = cboYear.Text
            Else
                Year = CStr(CInt(Date.Today.Year) - 1)
                Year2 = Date.Today.Year
            End If

            SQL = "Select * from " & _
            "" & DBNameSpace & ".VW_SSCPManagerInspections "

            For Each Engineer In clbEngineerInspections.CheckedItems
                SQLLine = SQLLine & " Engineer = '" & Engineer.ToString & "' OR "
            Next

            Select Case clbEngineerInspections.CheckedItems.Count
                Case 0
                    SQL = SQL & " Where DatScheduleDateStart between '01-Oct-" & Year & "' and '30-Sep-" & Year2 & "'"

                Case Else
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))
                    SQL = SQL & " Where (" & SQLLine & ") and DatScheduleDateStart between '01-Oct-" & Year & "' and '30-Sep-" & Year2 & "'"
            End Select

            SQL = SQL & " Order by datScheduleDateStart ASC "

            dsInspectionList = New DataSet

            daInspectionList = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daInspectionList.Fill(dsInspectionList, "InspectionList")
            dgrInspectionList.DataSource = dsInspectionList
            dgrInspectionList.DataMember = "InspectionList"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LockInspectionSchedule()
        Dim i As Integer
        Dim count As Integer
        Dim SQLLine As String = ""

        Try

            If dsInspectionList Is Nothing Then
                count = 0
            Else
                count = Me.BindingContext(dsInspectionList, "InspectionList").Count
            End If

            For i = 0 To count - 1
                SQLLine = SQLLine & "InspectionKey = '" & dgrInspectionList.Item(i, 0) & "' Or "
            Next

            If count <> 0 Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length) - 3)

                SQL = "Update " & DBNameSpace & ".SSCPInspectionTracking set " & _
                       "strLockSchedule = 'True' " & _
                       "where (" & SQLLine & " ) "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub UnLockInspectionSchedule()
        Dim i As Integer
        Dim count As Integer
        Dim SQLLine As String = ""

        Try

            If dsInspectionList Is Nothing Then
                count = 0
            Else
                count = Me.BindingContext(dsInspectionList, "InspectionList").Count
            End If

            For i = 0 To count - 1
                SQLLine = SQLLine & "InspectionKey = '" & dgrInspectionList.Item(i, 0) & "' Or "
            Next

            If count <> 0 Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length) - 3)

                SQL = "Update " & DBNameSpace & ".SSCPInspectionTracking set " & _
                       "strLockSchedule = 'False' " & _
                       "where (" & SQLLine & " ) "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ExportToExcel()
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim intRow As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            With ExcelApp
                .SheetsInNewWorkbook = 1
                .Workbooks.Add()
                .Worksheets(1).Select()
                'For displaying the column name in the the excel file.

                .Cells(1, 1) = "AIRS Number"
                .Cells(1, 2) = "Facility Name"
                .Cells(1, 3) = "Facility City"
                .Cells(1, 4) = "County Name"
                .Cells(1, 5) = "Engineer"
                .Cells(1, 6) = "Initial Inspection Start"
                .Cells(1, 7) = "Initial Inspection End"
                .Cells(1, 8) = "Current Inspection Start"
                .Cells(1, 9) = "Current Inspection End"
                .Cells(1, 10) = "Actual Inspection Start"
                .Cells(1, 11) = "Actual Inspection End"

                For intRow = 0 To Me.BindingContext(dsInspectionList, "InspectionList").Count - 1
                    .Cells(intRow + 2, 1).value = dgrInspectionList.Item(intRow, 1)
                    .Cells(intRow + 2, 2).value = dgrInspectionList.Item(intRow, 2)
                    .Cells(intRow + 2, 3).value = dgrInspectionList.Item(intRow, 3)
                    .Cells(intRow + 2, 4).value = dgrInspectionList.Item(intRow, 4)
                    .Cells(intRow + 2, 5).value = dgrInspectionList.Item(intRow, 5)
                    .Cells(intRow + 2, 6).value = dgrInspectionList.Item(intRow, 6)
                    .Cells(intRow + 2, 7).value = dgrInspectionList.Item(intRow, 7)
                    .Cells(intRow + 2, 8).value = dgrInspectionList.Item(intRow, 8)
                    .Cells(intRow + 2, 9).value = dgrInspectionList.Item(intRow, 9)
                    .Cells(intRow + 2, 10).value = dgrInspectionList.Item(intRow, 10)
                    .Cells(intRow + 2, 11).value = dgrInspectionList.Item(intRow, 11)
                Next

            End With
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try


    End Sub
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
           "from " & DBNameSpace & ".VW_SSCP_CMSWarning " & _
           "where AIRSNumber is not Null " & _
           CMSStatus

            dsCMSDataSet = New DataSet

            daCMSDataSet = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            '"substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            '"strFacilityName, strFacilityCity,   " & _
            '"strCMSMember, strOperationalStatus,  " & _
            '"strCountyName, strDistrictCounty,  " & _
            '"strDistrictName   " & _
            '"from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBSupplamentalData,   " & _
            '"" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".LookUpCountyInformation,  " & _
            '"" & DBNameSpace & ".LookUpDistrictInformation, " & DBNameSpace & ".LookUPDistricts  " & _
            '"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSupplamentalData.strAIRSNumber   " & _
            '"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
            '"and substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
            '"and substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCounty " & _
            '"and " & DBNameSpace & ".LookUPDistricts.strDistrictCode = " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode  " & SQLLine
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "from " & DBNameSpace & ".APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader

                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                    "strCMSMember = '" & CMSState & "' " & _
                    "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    dr = cmd.ExecuteReader

                End If
            Else
                MsgBox("Select a CMS status of either 'A' or 'S'.", MsgBoxStyle.Information, "SSSCP Managers Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub RemoveFacilityFromCMS()
        Try

            SQL = "Select strAIRSNumber " & _
                              "from " & DBNameSpace & ".APBSupplamentalData " & _
                              "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                "strCMSMember = '' " & _
                "where strAIRSNumber = '0413" & txtCMSAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                  "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCPItemMaster,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedReport  " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '01'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedReports,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedInspection " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '02'  " & _
                  DateReceivedBias & " " & _
                  "group by strResponsibleStaff) StartedInspections,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedISMPTest " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '03'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedISMPTEsts,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedAcc " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '04'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedACCs,  " & _
                  "(Select strResponsibleStaff, count(*) as StartedNotification " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '05'  " & _
                  DateReceivedBias & "  " & _
                  "group by strResponsibleStaff) StartedNotifications,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedReport  " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '01'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedReports,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedInspection " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '02'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedInspections,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedISMPTest " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '03'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedISMPTEsts,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedAcc " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '04'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedACCs,  " & _
                  "(Select strResponsibleStaff, count(*) as ClosedNotification " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '05'  " & _
                  DateCompletedBias & "  " & _
                  "group by strResponsibleStaff) ClosedNotifications, " & _
                  "(Select strResponsibleStaff, count(*) as OpenReport  " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '01'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenReports,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenInspection " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '02'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenInspections,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenISMPTest " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '03'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenISMPTEsts,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenAcc " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '04'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenACCs,  " & _
                  "(Select strResponsibleStaff, count(*) as OpenNotification " & _
                  "from " & DBNameSpace & ".SSCPItemMaster   " & _
                  "where strEventType = '05'  " & _
                  "and DatCompleteDate IS NULL  " & _
                  "group by strResponsibleStaff) OpenNotifications " & _
                  "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = StartedInspections.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = StartedReports.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = StartedISMPTests.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = StartedACCS.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = StartedNotifications.strResponsibleStaff (+)  " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClosedInspections.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClosedReports.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClosedISMPTests.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClosedACCS.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClosedNotifications.strResponsibleStaff (+)  " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = OpenInspections.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = OpenReports.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = OpenISMPTests.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = OpenACCS.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = OpenNotifications.strResponsibleStaff (+) " & _
                  "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " & _
                  "and strDelete is Null "

                    SQL2 = "Select distinct(" & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff), " & _
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
                    "from " & DBNameSpace & ".SSCPItemMaster,  " & _
                    "(select strResponsibleStaff, count(*) as ClassA  " & _
                    "from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPInspections  " & _
                    "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".SSCPItemMaster.strAIRSNUmber  " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCPInspections.strTrackingNumber  " & _
                    "and strClass = 'A'  " & _
                    DateInspection & _
                    "group by strResponsibleStaff) ClassAs,  " & _
                    "(select strResponsibleStaff, count(*) as ClassSM  " & _
                    "from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPInspections  " & _
                    "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".SSCPItemMaster.strAIRSNUmber  " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCPInspections.strTrackingNumber  " & _
                    "and strClass = 'SM'  " & _
                    DateInspection & _
                    "group by strResponsibleStaff) ClassSMs,  " & _
                    "(select strResponsibleStaff, count(*) as ClassB  " & _
                    "from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPInspections  " & _
                    "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".SSCPItemMaster.strAIRSNUmber  " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCPInspections.strTrackingNumber  " & _
                    "and strClass = 'B'  " & _
                    DateInspection & _
                    "group by strResponsibleStaff) ClassBs " & _
                    "where " & _
                    "" & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClassAs.strResponsibleStaff (+)  " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClassSMs.strResponsibleStaff (+) " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = ClassBs.strResponsibleStaff (+) " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " & _
                    "and strDelete is Null "

                    SQL3 = "Select " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber, datInspectionDateStart,  " & _
                    "strClass  " & _
                    "from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,  " & _
                    "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPInspections  " & _
                    "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIrSnumber  " & _
                    "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber  " & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCPInspections.strTrackingNumber  " & _
                    DateInspection & _
                    "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = '" & Staff & "' " & _
                    "and strDelete is Null " & _
                    "order by strClass, datInspectionDateStart "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
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

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        InspectA = dr2.Item("ClassA")
                        InspectSM = dr2.Item("ClassSM")
                        InspectB = dr2.Item("ClassB")
                    End While

                    cmd3 = New OracleCommand(SQL3, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "from " & DBNameSpace & ".VW_SSCP_CMSWarning " & _
            "where AIRSNumber is not Null " & _
            FCEStatus & CMSStatus

            If SQL <> "" Then
                dsCMSWarningDataSet = New DataSet
                daCMSWarningDataSet = New OracleDataAdapter(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ExportCMSWarningToExcel()
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvCMSWarning.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvCMSWarning.ColumnCount - 1
                        .Cells(1, i + 1) = dgvCMSWarning.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvCMSWarning.ColumnCount - 1
                        For j = 0 To dgvCMSWarning.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvCMSWarning.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
    Sub ExportPollutantsToExcel()
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvPollutantFacilities.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvPollutantFacilities.ColumnCount - 1
                        .Cells(1, i + 1) = dgvPollutantFacilities.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvPollutantFacilities.ColumnCount - 1
                        For j = 0 To dgvPollutantFacilities.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvPollutantFacilities.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "substr(" & DBNameSpace & ".APBAirProgramPollutants.strAIRSnumber, 5) as AIRSNumber, " & _
            "strFacilityName, " & _
            "(strComplianceStatus|| ' - ' ||strComplianceDesc) as PollutantStatus, " & _
            "strPollutantDescription " & _
            "from " & DBNameSpace & ".APBAirProgramPollutants, " & DBNameSpace & ".APBFacilityInformation, " & _
            "" & DBNameSpace & ".LookUpComplianceStatus, " & DBNameSpace & ".LookUPPollutants " & _
            "where " & DBNameSpace & ".APBAirProgramPollutants.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber " & _
            "and " & DBNameSpace & ".APBAirProgramPollutants.strComplianceStatus  = " & DBNameSpace & ".LookUpComplianceStatus.strComplianceCode " & _
            "and " & DBNameSpace & ".LookUPPollutants.strPollutantCode = " & DBNameSpace & ".APBAirProgramPollutants.strPollutantKey " & _
            PollutantLine

            dsPollutantList = New DataSet
            daPollutantList = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daPollutantList.Fill(dsPollutantList, "PollutantList")
            dgvPollutantFacilities.DataSource = dsPollutantList
            dgvPollutantFacilities.DataMember = "PollutantList"
            If Conn.State = ConnectionState.Open Then
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
            EngineerList & _
            "group by strAIRSNumber) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            "from " & DBNameSpace & ".SSCPItemMaster " & _
            "where strEventType = '04' " & _
            "and datReceivedDate between '" & Me.DTPSearchDateStart.Text & "' and '" & Me.DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtFacilitiesReporting.Text = dr.Item("TotalACCs")
            End While
            dr.Close()

            '---Requiring resubmittals
            SQL = "select " & _
            "count(*) as TotalRequiringResubmittals  " & _
            "from " & DBNameSpace & ".SSCPACCs, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCs.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '2'  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtResubmittals.Text = dr.Item("TotalRequiringResubmittals")
            End While
            dr.Close()

            '---Submitted Late
            SQL = "select " & _
            "count(*) as SubmittedLate " & _
            "from " & DBNameSpace & ".SSCPACCs, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCs.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strPostMarkedOnTime = 'False' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtSubmittedLate.Text = dr.Item("SubmittedLate")
            End While
            dr.Close()

            '---Devations Reported in first Submittal
            SQL = "select " & _
            "count(*) as DeviationsReported " & _
            "from " & DBNameSpace & ".SSCPACCs, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCs.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '1'  " & _
            "and strReportedDeviations = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            "from " & DBNameSpace & ".SSCPACCsHistory, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCsHistory.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '1'  " & _
            "and strReportedDeviations = 'False' " & _
            "and strDeviationsUnReported = 'False' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsCorrectlyReported.Text = dr.Item("DeviationsCorrect")
            End While
            dr.Close()

            '   ---Incorrectly
            SQL = "select " & _
            "count(*) as DeviationsIncorrect " & _
            "from " & DBNameSpace & ".SSCPACCsHistory, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCsHistory.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strSubmittalNumber = '1'  " & _
            "and strReportedDeviations = 'False' " & _
            "and strDeviationsUnReported = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsIncorrectlyReported.Text = dr.Item("DeviationsIncorrect")
            End While
            dr.Close()

            '---Deviations Reported in Final Report 
            SQL = "select " & _
            "count(*) as DeviationsInFinal " & _
            "from " & DBNameSpace & ".SSCPACCs, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCs.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strReportedDeviations = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtDeviationsReportedInFinal.Text = dr.Item("DeviationsInFinal")
            End While
            dr.Close()

            '---Deviations Not Previously Report
            SQL = "select  " & _
            "count(*) as DeviationsNotReported " & _
            "from " & DBNameSpace & ".SSCPACCs, " & DBNameSpace & ".SSCPItemMaster " & _
            "where " & DBNameSpace & ".SSCPACCs.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
            "and strEventType = '04' " & _
            "and strDeviationsUnReported = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtDeviationsNotPreviouslyReported.Text = dr.Item("DeviationsNotReported")
            End While
            dr.Close()

            '---Enforcement Action Taken 
            SQL = "select count(*) as EnforcementTaken " & _
            "from " & DBNameSpace & ".SSCP_AuditedEnforcement, " & DBNameSpace & ".SSCPItemMaster  " & _
            "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
            "and strEventType = '04'  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtEnforcementActionTaken.Text = dr.Item("EnforcementTaken")
            End While
            dr.Close()

            '    ---LON
            SQL = "select count(*) as LONTaken  " & _
            "from " & DBNameSpace & ".SSCP_AuditedEnforcement, " & DBNameSpace & ".SSCPItemMaster  " & _
             "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
            "and strEventType = '04'  " & _
            "and datLONSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtLONTaken.Text = dr.Item("LONTaken")
            End While
            dr.Close()

            '---NOV 
            SQL = "select count(*) as NOVTaken " & _
            "from " & DBNameSpace & ".SSCP_AuditedEnforcement, " & DBNameSpace & ".SSCPItemMaster " & _
             "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
           "and strEventType = '04'  " & _
            "and datNFALetterSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNOVTaken.Text = dr.Item("NOVTaken")
            End While
            dr.Close()

            '---CO 
            SQL = "select count(*) as COTaken " & _
            "from " & DBNameSpace & ".SSCP_AuditedEnforcement, " & DBNameSpace & ".SSCPItemMaster  " & _
            "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
            "and strEventType = '04'  " & _
            "and datCOResolved is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtCOTaken.Text = dr.Item("COTaken")
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Buttons"
    Private Sub btnFilterSearchOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSearchOptions.Click
        Try


            lvFacilityList.Items.Clear()
            lvFacilityList.Columns.Clear()
            CreateListDataSet()
            LoadListView()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnFilterAirsNumbers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterAirsNumbers.Click
        Try


            Dim temp As String
            Dim totaltext As String

            totaltext = txtAIRSNumberFilter.Text

            lsbAIRSNumber.Items.Clear()

            Do While totaltext <> ""
                temp = Mid(totaltext, 1, 8)
                lsbAIRSNumber.Items.Add(temp)
                If totaltext.Length > 10 Then
                    totaltext = Microsoft.VisualBasic.Right(totaltext, totaltext.Length - 10)
                Else
                    totaltext = ""
                End If
            Loop

            lvFacilityList.Items.Clear()
            lvFacilityList.Columns.Clear()
            CreateListDataSet2()
            LoadListView()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnClearManualList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearManualList.Click
        Try

            txtAIRSNumberFilter.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnClearResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearResults.Click
        Dim i As Integer

        Try

            For i = 0 To txtCount.Text - 1
                lvFacilityList.Items.Item(i).Checked = False
            Next
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnCheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckAll.Click
        Dim i As Integer

        Try

            For i = 0 To txtCount.Text - 1
                lvFacilityList.Items.Item(i).Checked = True
            Next
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnMakeEngineerAssignments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMakeEngineerAssignments.Click
        Try

            SaveEngineerAssignments()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnMakeUnitAssignments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMakeUnitAssignments.Click
        Try

            SaveUnitAssignments()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveDistrictResponsiblity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDistrictResponsiblity.Click
        Try

            SaveDistrictAssignments()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveInspectionRequirements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveInspectionRequirements.Click
        Try

            SaveInspectionRequired()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblViewEngineerSchedule_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEngineerSchedule.LinkClicked
        Try

            ViewEngineerSchedule()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblLockSchedule_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblLockSchedule.LinkClicked
        Try

            LockInspectionSchedule()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblUnlockSchedule_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblUnlockSchedule.LinkClicked
        Try

            UnLockInspectionSchedule()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblExportScheduleToExcel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblExportScheduleToExcel.LinkClicked
        Try

            ExportToExcel()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbViewCMSUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewCMSUniverse.LinkClicked
        Try

            LoadCMSUniverse()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbCMSOpenFacilitySummary_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary.LinkClicked
        Try

            If FacilitySummary Is Nothing Then
                If FacilitySummary Is Nothing Then FacilitySummary = New IAIPFacilitySummary
            Else
                FacilitySummary.Show()
            End If
            FacilitySummary.Show()
            FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            FacilitySummary.mtbAIRSNumber.Text = txtCMSAIRSNumber.Text
            FacilitySummary.LoadInitialData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbCMSOpenFacilitySummary2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary2.LinkClicked
        Try

            If FacilitySummary Is Nothing Then
                If FacilitySummary Is Nothing Then FacilitySummary = New IAIPFacilitySummary
            Else
                FacilitySummary.Show()
            End If
            FacilitySummary.Show()
            FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            FacilitySummary.mtbAIRSNumber.Text = txtCMSAIRSNumber2.Text
            FacilitySummary.LoadInitialData()
            FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbAddFacilityToCMS_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbAddFacilityToCMS.LinkClicked
        Try

            AddFacilityToCMS()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbDeleteFacilityFromCMS_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDeleteFacilityFromCMS.LinkClicked
        Try

            RemoveFacilityFromCMS()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lblRunInspectionReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblRunInspectionReport.LinkClicked
        Try

            RunInspectionReport()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region
    Private Sub lvFacilityList_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvFacilityList.ColumnClick
        Try

            ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
            lvFacilityList.ListViewItemSorter = New ListViewItemComparer(e.Column)
            ' Call the sort method to manually sort the column based on the ListViewItemComparer implementation.
            lvFacilityList.Sort()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub lvFacilityList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lvFacilityList.ItemCheck
        Dim temp As String

        Try

            If lvFacilityList.Items.Item(e.Index).Checked = True Then
                temp = lvFacilityList.Items.Item(e.Index).Text
                lsbSelectedFacilities.Items.Remove(temp)
            Else
                temp = lvFacilityList.Items.Item(e.Index).Text
                lsbSelectedFacilities.Items.Add(temp)
            End If
            txtFacilitiesSelected.Text = lsbSelectedFacilities.Items.Count
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
               "from " & DBNameSpace & ".VW_SSCP_CMSWARNING) TABLE1, " & _
               "(select " & _
               "max(INTYEAR), NUMSSCPENGINEER, " & _
               "strairsnumber " & _
               "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
               "group by NUMSSCPENGINEER, STRAIRSNUMBER)TABLE2, " & _
               "" & DBNameSpace & ".EPDUSERPROFILES " & _
               "where '0413'||TABLE1.AIRSNUMBER = TABLE2.STRAIRSNUMBER (+) " & _
               "and Table2.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numuserid (+)  " & _
               "and AIRSNumber = '" & txtCMSAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "from " & DBNameSpace & ".VW_SSCP_CMSWARNING) TABLE1, " & _
                "(select " & _
                "max(INTYEAR), NUMSSCPENGINEER, " & _
                "strairsnumber " & _
                "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
                "group by NUMSSCPENGINEER, STRAIRSNUMBER)TABLE2, " & _
                "" & DBNameSpace & ".EPDUSERPROFILES " & _
                "where '0413'||TABLE1.AIRSNUMBER = TABLE2.STRAIRSNUMBER (+) " & _
                "and Table2.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numuserid (+)  " & _
                "and AIRSNumber = '" & txtCMSAIRSNumber2.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboComplianceUnits_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboComplianceUnits.TextChanged
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub lblCMSWarning_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblCMSWarning.LinkClicked
        Try

            RunCMSWarningReport()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbExportCMSWarningToExcel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExportCMSWarningToExcel.LinkClicked
        Try

            If txtCMSWarningCount.Text <> "" Or txtCMSWarningCount.Text <> "0" Then
                ExportCMSWarningToExcel()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbPrintStaffReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPrintStaffReport.LinkClicked
        Try
            If rtbInspectionReport.Text <> "" Then
                PrintStaffReport()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFacilities.Click
        Try

            FilterPollutantSearch()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Try
            If txtAIRSNumber.Text <> "" Then

                EditAirProgramPollutants = Nothing
                If EditAirProgramPollutants Is Nothing Then EditAirProgramPollutants = New IAIPEditAirProgramPollutants
                EditAirProgramPollutants.txtAirsNumber.Text = Me.txtAIRSNumber.Text
                EditAirProgramPollutants.Show()
                EditAirProgramPollutants.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbExportPollutantsToExcel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExportPollutantsToExcel.LinkClicked
        Try

            If txtCMSWarningCount.Text <> "" Or txtCMSWarningCount.Text <> "0" Then
                ExportPollutantsToExcel()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRunStatisticalReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunStatisticalReport.Click
        Try

            RunACCStats()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
            EngineerList & _
            "group by strAIRSNumber, numSSCPEngineer) Table1, " & _
            "" & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles " & _
            "where Table1.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRsnumber " & _
            "and Table1.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
                "strFacilityName,  " & _
                "(strLastname||', '||strFirstName) as UserName,  " & _
                "strTrackingNumber " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,  " & _
                "" & DBNameSpace & ".SSCPItemMaster  " & _
                "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber  " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID   " & _
                "and strEventType = '04'  " & _
                "and datReceivedDate between '" & Me.DTPSearchDateStart.Text & "' and '" & Me.DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
                "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCs    " & _
                "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCs.strTrackingNumber  " & _
                "and strSubmittalNumber = '2'  " & _
                "and strEventType = '04'   " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
           "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
           "strFacilityName,   " & _
           "(strLastName||', '||strFirstName) as UserName,   " & _
           "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
           "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
           "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCs    " & _
           "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
           "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
           "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCs.strTrackingNumber  " & _
           "and strPostMarkedOnTime = 'False' " & _
           "and strEventType = '04'   " & _
           "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
           ResponsibleStaff & _
           "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
                "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCs    " & _
                "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCs.strTrackingNumber  " & _
                "and strSubmittalNumber = '1'  " & _
                "and strEventType = '04'   " & _
                "and strReportedDeviations = 'True' " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
                "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCsHistory    " & _
                "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCsHistory.strTrackingNumber  " & _
                "and strSubmittalNumber = '1'  " & _
                "and strEventType = '04'   " & _
                "and strReportedDeviations = 'False' " & _
                "and strDeviationsUnReported = 'False' " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
                "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCsHistory    " & _
                "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCsHistory.strTrackingNumber  " & _
                "and strSubmittalNumber = '1'  " & _
                "and strEventType = '04'   " & _
                "and strReportedDeviations = 'False' " & _
                "and strDeviationsUnReported = 'True' " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastname||', '||strFirstName) as UserName,   " & _
            "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
            "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCs    " & _
            "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCs.strTrackingNumber  " & _
            "and strEventType = '04' " & _
            "and strReportedDeviations = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber  " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
            "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCs    " & _
            "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCs.strTrackingNumber  " & _
            "and strEventType = '04'   " & _
            "and strDeviationsUnReported = 'True' " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
                "strFacilityName,   " & _
                "(strLastName||', '||strFirstName) as UserName,   " & _
                "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber,  " & _
                "" & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber  " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
                "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".SSCPACCs,  " & _
                "" & DBNameSpace & ".SSCP_AuditedEnforcement   " & _
                "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingnumber = " & DBNameSpace & ".SSCPACCs.strTrackingNumber  " & _
                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber  " & _
                "and strEventType = '04'   " & _
                "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
                ResponsibleStaff & _
                "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3
            dgvStatistialReports.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
            dgvStatistialReports.Columns("strEnforcementNumber").DisplayIndex = 4

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber,  " & _
            "" & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber  " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
            "" & DBNameSpace & ".SSCPItemMaster,  " & _
            "" & DBNameSpace & ".SSCP_AuditedEnforcement   " & _
            "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber  " & _
            "and strEventType = '04'   " & _
            "and datCOResolved is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber,  " & _
            "" & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber  " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
            "" & DBNameSpace & ".SSCPItemMaster,  " & _
            "" & DBNameSpace & ".SSCP_AuditedEnforcement   " & _
            "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
             "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber  " & _
             "and strEventType = '04'   " & _
             "and datNFALetterSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,   " & _
            "strFacilityName,   " & _
            "(strLastName||', '||strFirstName) as UserName,   " & _
            "" & DBNameSpace & ".SSCPItemMaster.strTrackingNumber,  " & _
            "" & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber  " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,   " & _
            "" & DBNameSpace & ".SSCPItemMaster,   " & _
            "" & DBNameSpace & ".SSCP_AuditedEnforcement   " & _
            "where " & DBNameSpace & ".SSCPItemMaster.strAirsnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber   " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID    " & _
            "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber  " & _
             "and strEventType = '04'   " & _
            "and datLONSent is Not Null  " & _
            "and datReceivedDate between '" & DTPSearchDateStart.Text & "' and '" & DTPSearchDateEnd.Text & "'  " & _
            ResponsibleStaff & _
            "order by strFacilityName"

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("Username").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("Username").DisplayIndex = 2
            dgvStatistialReports.Columns("strTrackingNumber").HeaderText = "ACC Tracking #"
            dgvStatistialReports.Columns("strTrackingNumber").DisplayIndex = 3

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "ACC Views"
    Private Sub llbViewACCTotalAssigned_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewACCTotalAssigned.LinkClicked
        Try

            ViewACCTotalAssigned()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCReporting_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCReporting.LinkClicked
        Try

            ViewACCReporting()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCRequiringResubmittal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCRequiringResubmittal.LinkClicked
        Try

            ViewACCRequiringResubmittal()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCSubmittedLate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCSubmittedLate.LinkClicked
        Try

            ViewACCSubmittedLate()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReported.LinkClicked
        Try

            ViewACCDeviationsReported()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsReportedCorrectly_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsReportedCorrectly.LinkClicked
        Try

            ViewACCDeviationsReportedCorrectly()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsIncorrectlyReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsIncorrectlyReported.LinkClicked
        Try

            ViewACCDeviationsReportedIncorrectly()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsInFinal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsInFinal.LinkClicked
        Try

            ViewACCDeviationsInFinal()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCDeviationsNotReported_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCDeviationsNotReported.LinkClicked
        Try

            ViewACCDeviationsNotReported()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCEnforcementTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCEnforcementTaken.LinkClicked
        Try

            ViewACCEnforcementTaken()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCCOTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCCOTaken.LinkClicked
        Try

            ViewACCCOTaken()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCNOVTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCNOVTaken.LinkClicked
        Try

            ViewACCNOVTaken()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbACCLONTaken_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbACCLONTaken.LinkClicked
        Try

            ViewACCLONTaken()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

    Sub EnforcementTotals()
        Try
            SQL = "select " & _
            "'$'||to_number((CoPenalty + Stipulated), '99999999.99') as TotalPen  " & _
            "from  " & _
            "(select  " & _
            "sum(" & DBNameSpace & ".SSCP_AuditedEnforcement.strCOPenaltyAmount) as COPenalty " & _
            "from " & DBNameSpace & ".SSCP_AuditedEnforcement  " & _
            "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "') COPEn,  " & _
            "(select  " & _
            "sum(" & DBNameSpace & ".SSCPEnforcementStipulated.strStipulatedPenalty) as Stipulated " & _
            "from " & DBNameSpace & ".SSCPEnforcementStipulated, " & DBNameSpace & ".SSCP_AuditedEnforcement  " & _
            "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = " & DBNameSpace & ".SSCPEnforcementStipulated.strEnforcementNumber  " & _
            "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "') StipPen "

            If chbUseEnforcementDateRange.Checked = True Then
                SQL = "select " & _
                "'$'||to_number((CoPenalty + Stipulated), '99999999.99') as TotalPen  " & _
                "from  " & _
                "(select  " & _
                "sum(" & DBNameSpace & ".SSCP_AuditedEnforcement.strCOPenaltyAmount) as COPenalty " & _
                "from " & DBNameSpace & ".SSCP_AuditedEnforcement  " & _
                "where  " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' " & _
                "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "') COPEn,  " & _
                "(select  " & _
                "sum(" & DBNameSpace & ".SSCPEnforcementStipulated.strStipulatedPenalty) as Stipulated " & _
                "from " & DBNameSpace & ".SSCPEnforcementStipulated, " & DBNameSpace & ".SSCP_AuditedEnforcement  " & _
                "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = " & DBNameSpace & ".SSCPEnforcementStipulated.strEnforcementNumber  " & _
                 "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' " & _
                "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "') StipPen "
            End If

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewEnforcements_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewEnforcements.LinkClicked
        Try
            If txtEnforcementAIRSNumber.Text <> "" Then
                SQL = "select " & _
                "strFacilityName, " & _
                "substr(SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                "" & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, " & _
                "'$'||to_number(" & DBNameSpace & ".SSCP_AuditedEnforcement.strCOPenaltyAmount, '99999999.99') as COPenalty, " & _
                "'$'||to_number(" & DBNameSpace & ".SSCPEnforcementStipulated.strStipulatedPenalty, '99999999.99') as StipulatedPenalty, " & _
                "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " & _
                "from " & DBNameSpace & ".SSCP_AuditedEnforcement, " & _
                "" & DBNameSpace & ".SSCPEnforcementStipulated, " & DBNameSpace & ".APBFacilityInformation  " & _
                "where " & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = " & DBNameSpace & ".SSCPEnforcementStipulated.strEnforcementNumber " & _
                "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & _
                "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "

                If chbUseEnforcementDateRange.Checked = True Then
                    SQL = "select " & _
                    "strFacilityName, " & _
                    "substr(SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                    "" & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, " & _
                    "'$'||to_number(" & DBNameSpace & ".SSCP_AuditedEnforcement.strCOPenaltyAmount, '99999999.99') as COPenalty, " & _
                    "'$'||to_number(" & DBNameSpace & ".SSCPEnforcementStipulated.strStipulatedPenalty, '99999999.99') as StipulatedPenalty, " & _
                    "to_char(datDiscoveryDate, 'dd-Mon-yyyy') as datDiscoveryDate " & _
                    "from " & DBNameSpace & ".SSCP_AuditedEnforcement, " & _
                    "" & DBNameSpace & ".SSCPEnforcementStipulated, " & DBNameSpace & ".APBFacilityInformation " & _
                    "where  " & DBNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = " & DBNameSpace & ".SSCPEnforcementStipulated.strEnforcementNumber " & _
                    "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & _
                    "and datDiscoveryDate between '" & dtpEnforcementStartDate.Text & "' and '" & dtpEnforcementEndDate.Text & "' " & _
                    "and " & DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtEnforcementAIRSNumber.Text & "' "
                End If

                dsEnforcementPenalties = New DataSet
                daEnforcementPenalties = New OracleDataAdapter(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                daEnforcementPenalties.Fill(dsEnforcementPenalties, "EnforcementPenalties")
                dgvStatistialReports.DataSource = dsEnforcementPenalties
                dgvStatistialReports.DataMember = "EnforcementPenalties"
                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
                dgvStatistialReports.RowHeadersVisible = False
                dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvStatistialReports.AllowUserToResizeColumns = True
                dgvStatistialReports.AllowUserToAddRows = False
                dgvStatistialReports.AllowUserToDeleteRows = False
                dgvStatistialReports.AllowUserToOrderColumns = True
                dgvStatistialReports.AllowUserToResizeRows = True

                dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
                dgvStatistialReports.Columns("AIRSNumber").Width = 75
                dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
                dgvStatistialReports.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
                dgvStatistialReports.Columns("strEnforcementNumber").DisplayIndex = 2
                dgvStatistialReports.Columns("COPenalty").HeaderText = "Penalty Amount"
                dgvStatistialReports.Columns("COPenalty").DisplayIndex = 3
                dgvStatistialReports.Columns("StipulatedPenalty").HeaderText = "Stipulated Penalty Totals"
                dgvStatistialReports.Columns("StipulatedPenalty").DisplayIndex = 4
                dgvStatistialReports.Columns("datDiscoveryDate").HeaderText = "Discovery Date"
                dgvStatistialReports.Columns("datDiscoveryDate").DisplayIndex = 5

                txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvStatistialReports_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvStatistialReports.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvStatistialReports.HitTest(e.X, e.Y)

        Try

            If dgvStatistialReports.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvStatistialReports.Columns(1).HeaderText = "AIRS #" And dgvStatistialReports.Columns(2).HeaderText = "Enforcement #" Then
                    txtRecordNumber.Text = dgvStatistialReports(2, hti.RowIndex).Value
                    lblStatisticalRecords.Text = "Enforcement #"
                Else
                    If dgvStatistialReports.ColumnCount = 3 Then
                        txtRecordNumber.Text = dgvStatistialReports(0, hti.RowIndex).Value
                        lblStatisticalRecords.Text = "AIRS #"
                    Else
                        If dgvStatistialReports.ColumnCount = 4 Then
                            If dgvStatistialReports.Columns(3).HeaderText = "ACC Tracking #" Then
                                txtRecordNumber.Text = dgvStatistialReports(3, hti.RowIndex).Value
                                lblStatisticalRecords.Text = "ACC Tracking #"
                            Else
                                txtRecordNumber.Text = dgvStatistialReports(0, hti.RowIndex).Value
                                lblStatisticalRecords.Text = "AIRS #"
                            End If
                        Else
                            If dgvStatistialReports.ColumnCount = 5 Then
                                If hti.ColumnIndex = 3 Then
                                    txtRecordNumber.Text = dgvStatistialReports(3, hti.RowIndex).Value
                                    lblStatisticalRecords.Text = "ACC Tracking #"
                                Else
                                    txtRecordNumber.Text = dgvStatistialReports(4, hti.RowIndex).Value
                                    lblStatisticalRecords.Text = "Enforcement #"
                                End If
                            Else
                                txtRecordNumber.Text = dgvStatistialReports(0, hti.RowIndex).Value
                                lblStatisticalRecords.Text = "AIRS #"
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnViewComplianceRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewComplianceRecord.Click
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub OpenFacilitySummary()
        Try

            If txtRecordNumber.Text <> "" And txtRecordNumber.Text.Length = 8 Then
                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".APBMasterAIRS " & _
                "where strAIRSnumber = '0413" & txtRecordNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    If FacilitySummary Is Nothing Then
                        FacilitySummary = Nothing
                        If FacilitySummary Is Nothing Then FacilitySummary = New IAIPFacilitySummary
                        FacilitySummary.mtbAIRSNumber.Text = txtRecordNumber.Text
                        FacilitySummary.Show()
                    Else
                        FacilitySummary.mtbAIRSNumber.Text = txtRecordNumber.Text
                        FacilitySummary.Show()
                    End If
                    FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                    FacilitySummary.LoadInitialData()
                Else
                    MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
                End If
            Else
                MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub OpenEnforcement()
        Try

            If txtRecordNumber.Text <> "" Then
                SQL = "select strEnforcementNumber " & _
                "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                "where strEnforcementNumber = '" & txtRecordNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    If SSCP_Enforcement Is Nothing Then
                        If SSCP_Enforcement Is Nothing Then SSCP_Enforcement = New SSCPEnforcementAudit
                        If txtRecordNumber.Text <> "" Then
                            SSCP_Enforcement.txtEnforcementNumber.Text = txtRecordNumber.Text
                        End If
                        SSCP_Enforcement.Show()
                    Else
                        SSCP_Enforcement.Close()
                        SSCP_Enforcement = Nothing
                        If SSCP_Enforcement Is Nothing Then SSCP_Enforcement = New SSCPEnforcementAudit
                        If txtRecordNumber.Text <> "" Then
                            SSCP_Enforcement.txtEnforcementNumber.Text = txtRecordNumber.Text
                        End If
                        SSCP_Enforcement.Show()
                    End If
                    SSCP_Enforcement.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                Else
                    MsgBox("Enforcement Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub OpenSSCPWork()
        Try

            If txtRecordNumber.Text <> "" And IsNumeric(txtRecordNumber.Text) Then
                SQL = "Select " & _
                "strTrackingNumber " & _
                "from " & DBNameSpace & ".SSCPItemMaster " & _
                "where strTrackingNumber = '" & txtRecordNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    If SSCPREports Is Nothing Then
                        SSCPREports = Nothing
                        If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                        SSCPREports.txtTrackingNumber.Text = txtRecordNumber.Text
                        SSCPREports.Show()
                    Else
                        SSCPREports.txtTrackingNumber.Text = txtRecordNumber.Text
                        SSCPREports.Show()
                    End If
                    SSCPREports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Else
                    MsgBox("Tracking Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
                End If
            Else
                MsgBox("Tracking Number is not in the system.", MsgBoxStyle.Information, "SSCP Managers Tools")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        Try
            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub mmiChangeProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiChangeProfile.Click
        Try
            ManagerProfile = Nothing
            If ManagerProfile Is Nothing Then ManagerProfile = New SSCPManagerProfile
            'If ManagerProfile Is Nothing Then
            '    If ManagerProfile Is Nothing Then ManagerProfile = New SSCPManagerProfile
            'Else

            'End If
            ManagerProfile.Show()
            ManagerProfile.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportDataToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportDataToExcel.Click
        Try
            'Dim lvColumnCount As String = lvFacilityList.Columns.Count
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim intRow As Integer
            Dim intColumn As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            With ExcelApp
                .SheetsInNewWorkbook = 1
                .Workbooks.Add()
                .Worksheets(1).Select()

                For intColumn = 0 To lvFacilityList.Columns.Count - 1
                    .Cells(1, intColumn + 1).value = lvFacilityList.Columns(intColumn).Text.ToString
                Next


                For intRow = 0 To lvFacilityList.Items.Count - 1
                    For intColumn = 0 To lvFacilityList.Columns.Count - 1
                        .Cells(intRow + 2, intColumn + 1).value = lvFacilityList.Items(intRow).SubItems(intColumn).Text.ToString
                    Next
                Next
            End With

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try

    End Sub
    Private Sub lblExportToExcelCMSUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblExportToExcelCMSUniverse.LinkClicked
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i As Integer
            Dim j As Integer

            If dgvCMSUniverse.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvCMSUniverse.ColumnCount - 1
                        .Cells(1, i + 1) = dgvCMSUniverse.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvCMSUniverse.ColumnCount - 1
                        For j = 0 To dgvCMSUniverse.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvCMSUniverse.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "distinct(substr(" & DBNameSpace & ".APBAirProgramPollutants.strAIRSNumber, 5)) as AIRSNumber, " & _
            "strFacilityName, strPollutantDescription, " & _
            "(strComplianceStatus||' - '||strComplianceDesc) as ComplianceStatus " & _
            "from " & DBNameSpace & ".APBAirProgramPollutants, " & DBNameSpace & ".APBFacilityInformation, " & _
            "" & DBNameSpace & ".LookUpPollutants, " & DBNameSpace & ".LookUpComplianceStatus  " & _
            "where " & DBNameSpace & ".APBAirProgramPollutants.strAIRSnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber  " & _
            "and " & DBNameSpace & ".APBAirProgramPollutants.strPollutantKey = " & DBNameSpace & ".LookUpPollutants.strPollutantCode " & _
            "and " & DBNameSpace & ".APBAirProgramPollutants.strComplianceStatus = " & DBNameSpace & ".LookupComplianceStatus.strComplianceCode  " & _
               ComplianceWhere

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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

            txtWatchListCount.Text = dgvWatchList.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbExportWatchList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExportWatchList.LinkClicked
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvWatchList.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvWatchList.ColumnCount - 1
                        .Cells(1, i + 1) = dgvWatchList.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvWatchList.ColumnCount - 1
                        For j = 0 To dgvWatchList.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvWatchList.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

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
                "substr(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                "strFacilityCity, " & _
                "strCMSMember, " & _
                "strClass, strOperationalStatus, " & _
                "LastInspection," & _
                "LastFCE, " & _
                "(strLastName||', '||strFirstName) as SSCPEngineer, " & _
                "strUnitDesc," & _
                " strDistrictResponsible, " & _
                "strCountyName " & _
                "from " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment, " & _
                "" & DBNameSpace & ".EPDUserProfiles, " & _
                " " & DBNameSpace & ".SSCPInspectionsRequired, " & _
                "" & DBNameSpace & ".LookUpEPDUnits, " & _
                "(select " & _
                "max(intYear) as MaxYear, " & DBNameSpace & ".SSCPInspectionsRequired.strairsnumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "group by " & DBNameSpace & ".SSCPInspectionsRequired.strAIRSNumber) MaxResults " & _
              " where " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
              "and " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPUnit = " & DBNameSpace & ".LookUpEPDunits.numUnitCode (+) " & _
              " and " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strairsnumber = " & DBNameSpace & ".sscpinspectionsrequired.strairsnumber (+) " & _
              "and " & DBNameSpace & ".SSCPInspectionsRequired.strAIRSNumber = MaxResults.strAIRSNumber " & _
              "and " & DBNameSpace & ".SSCPInspectionsRequired.intYear = MaxResults.maxYear "

            Else
                SQL = "Select " & _
              "substr(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
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
              " from " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment, " & _
              "" & DBNameSpace & ".EPDUserProfiles, " & _
              " " & DBNameSpace & ".SSCPInspectionsRequired, " & _
              "" & DBNameSpace & ".LookUpEPDUnits " & _
              " where " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
              "and " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPUnit = " & DBNameSpace & ".LookUpEPDunits.numUnitCode (+) " & _
              " and " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strairsnumber = " & DBNameSpace & ".sscpinspectionsrequired.strairsnumber (+) " & _
              " and " & DBNameSpace & ".sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "
            End If

            SQLLine1 = " "
            SQLLine2 = " "
            SQLOrder1 = " "
            SQLOrder2 = " "
            If Location = "Filter" Then
                If cboFacSearch1.Items.Contains(cboFacSearch1.Text) And cboFilterEngineer1.Text <> "" Then
                    Select Case cboFacSearch1.Text
                        Case "AIRS Number"
                            SQLLine1 = " upper(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber) like '%" & Replace(txtFacSearch1.Text.ToUpper, "'", "''") & "%' "
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
                            SQLLine2 = " upper(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber) like '%" & Replace(txtFacSearch2.Text.ToUpper, "'", "''") & "%' "
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
                    SQLLine = SQLLine & " " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strairsnumber = '0413" & Mid(TotalText, 1, 8) & "' or "
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
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnselectFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectFacility.Click
        Try
            If dgvSelectedFacilityList.Rows.Count > 0 Then
                dgvSelectedFacilityList.Rows.Remove(dgvSelectedFacilityList.CurrentRow)
            End If

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnselectAllFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAllFacilities.Click
        Try

            dgvSelectedFacilityList.Rows.Clear()

            lblSelectedCount.Text = "Count: " & dgvSelectedFacilityList.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearManualAIRSNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearManualAIRSNum.Click
        Try

            txtManualAIRSNumber.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFilterManualAIRSList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterManualAIRSList.Click
        Try
            LoadFacilitySearch("Manual")
            Exit Sub
            '   Dim SQLLine As String = ""
            '   Dim TotalText As String = ""

            '   'If chbViewAllFields.Checked = True Then
            '   '    chbViewAllFields.Checked = False
            '   'End If
            '   If chbIgnoreFiscalYear.Checked = True Then
            '       chbIgnoreFiscalYear.Checked = False
            '   End If

            '   TotalText = txtManualAIRSNumber.Text
            '   SQLLine = ""

            '   'SQL = "Select " & _
            '   '"distinct(substr(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5)) as AIRSNumber, strFacilityName, " & _
            '   '"strFacilityCity, " & _
            '   '"strCMSMember, " & _
            '   '"strClass, strOperationalStatus, " & _
            '   '"LastInspection," & _
            '   '"LastFCE, " & _
            '   '"(strLastName||', '||strFirstName) as SSCPEngineer, " & _
            '   '"strUnitDesc," & _
            '   '" strDistrictResponsible, " & _
            '   '"strCountyName " & _
            '   '"from " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment, " & _
            '   '"" & DBNameSpace & ".EPDUserProfiles, " & _
            '   '"" & DBNameSpace & ".SSCPInspectionsRequired, " & _
            '   '"" & DBNameSpace & ".LookUpEPDUnits " & _
            '   '"where " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            '   '"and " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPUnit = " & DBNameSpace & ".LookUpEPDunits.numUnitCode (+) " & _
            '   '"and " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strairsnumber = " & DBNameSpace & ".sscpinspectionsrequired.strairsnumber (+) " & _
            '   '"and intyear = '" & cboFiscalYear.Text & "' "

            '   SQL = "Select " & _
            '   "substr(" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
            '   "strFacilityCity, " & _
            '   "strCMSMember, " & _
            '   " strClass, strOperationalStatus, " & _
            '   " case " & _
            '   " when strInspectionRequired = 'True' then 'True' " & _
            '   " when strinspectionrequired = 'False' then 'False' " & _
            '   " when strInspectionRequired is null then 'False' " & _
            '   " end InspectionRequired, " & _
            '   " LastInspection," & _
            '   "   case " & _
            '   " when strFCERequired = 'True' then 'True' " & _
            '   " when strFCERequired = 'False' then 'False' " & _
            '   " when strFCERequired is null then 'False' " & _
            '   " end FCERequired, " & _
            '   "   LastFCE, " & _
            '   "(strLastName||', '||strFirstName) as SSCPEngineer, " & _
            '   "  strUnitDesc," & _
            '   "   strDistrictResponsible, " & _
            '   "  strCountyName " & _
            '   " from " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment, " & _
            '   "" & DBNameSpace & ".EPDUserProfiles, " & _
            '   " " & DBNameSpace & ".SSCPInspectionsRequired, " & _
            '   "" & DBNameSpace & ".LookUpEPDUnits " & _
            ' " where " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            '  "and " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPUnit = " & DBNameSpace & ".LookUpEPDunits.numUnitCode (+) " & _
            '" and " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strairsnumber = " & DBNameSpace & ".sscpinspectionsrequired.strairsnumber (+) " & _
            '  " and " & DBNameSpace & ".sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "

            '   Do While TotalText <> ""
            '       SQLLine = SQLLine & " " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strairsnumber = '0413" & Mid(TotalText, 1, 8) & "' or "
            '       If TotalText.Length > 10 Then
            '           TotalText = Microsoft.VisualBasic.Right(TotalText, TotalText.Length - 10)
            '       Else
            '           TotalText = ""
            '       End If
            '   Loop

            '   If SQLLine <> "" Then
            '       SQL = SQL & " and ( " & Mid(SQLLine, 1, SQLLine.Length - 4) & " ) "
            '   End If

            '   ds = New DataSet
            '   da = New OracleDataAdapter(SQL, conn)
            '   If conn.State = ConnectionState.Closed Then
            '       conn.Open()
            '   End If
            '   da.Fill(ds, "FacilitySearch")
            '   dgvFilteredFacilityList.DataSource = ds
            '   dgvFilteredFacilityList.DataMember = "FacilitySearch"

            '   dgvFilteredFacilityList.RowHeadersVisible = False
            '   dgvFilteredFacilityList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            '   dgvFilteredFacilityList.AllowUserToResizeColumns = True
            '   dgvFilteredFacilityList.AllowUserToAddRows = False
            '   dgvFilteredFacilityList.AllowUserToDeleteRows = False
            '   dgvFilteredFacilityList.AllowUserToOrderColumns = True
            '   dgvFilteredFacilityList.AllowUserToResizeRows = True

            '   'dgvFilteredFacilityList.Columns("AIRSNumber").HeaderText = "AIRS #"
            '   'dgvFilteredFacilityList.Columns("AIRSNumber").DisplayIndex = 0
            '   'dgvFilteredFacilityList.Columns("strFacilityName").HeaderText = "Facility Name"
            '   'dgvFilteredFacilityList.Columns("strFacilityName").DisplayIndex = 1
            '   'dgvFilteredFacilityList.Columns("strFacilityCity").HeaderText = "City"
            '   'dgvFilteredFacilityList.Columns("strFacilityCity").DisplayIndex = 2
            '   'dgvFilteredFacilityList.Columns("strCMSMember").HeaderText = "CMS Status"
            '   'dgvFilteredFacilityList.Columns("strCMSMember").DisplayIndex = 3
            '   'dgvFilteredFacilityList.Columns("strClass").HeaderText = "Classification"
            '   'dgvFilteredFacilityList.Columns("strClass").DisplayIndex = 4
            '   'dgvFilteredFacilityList.Columns("strOperationalStatus").HeaderText = "Operational Status"
            '   'dgvFilteredFacilityList.Columns("strOperationalStatus").DisplayIndex = 5
            '   'dgvFilteredFacilityList.Columns("LastInspection").HeaderText = "Last Inspection"
            '   'dgvFilteredFacilityList.Columns("LastInspection").DisplayIndex = 6
            '   'dgvFilteredFacilityList.Columns("LastInspection").DefaultCellStyle.Format = "dd-MMM-yyyy"
            '   'dgvFilteredFacilityList.Columns("LastFCE").HeaderText = "Last FCE"
            '   'dgvFilteredFacilityList.Columns("LastFCE").DisplayIndex = 7
            '   'dgvFilteredFacilityList.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
            '   'dgvFilteredFacilityList.Columns("SSCPEngineer").HeaderText = "SSCP Engineer"
            '   'dgvFilteredFacilityList.Columns("SSCPEngineer").DisplayIndex = 8
            '   'dgvFilteredFacilityList.Columns("strUnitDesc").HeaderText = "SSCP Title"
            '   'dgvFilteredFacilityList.Columns("strUnitDesc").DisplayIndex = 9
            '   'dgvFilteredFacilityList.Columns("strDistrictResponsible").HeaderText = "District Source"
            '   'dgvFilteredFacilityList.Columns("strDistrictResponsible").DisplayIndex = 10
            '   'dgvFilteredFacilityList.Columns("strCountyName").HeaderText = "County"
            '   'dgvFilteredFacilityList.Columns("strCountyName").DisplayIndex = 11

            '   dgvFilteredFacilityList.Columns("AIRSNumber").HeaderText = "AIRS #"
            '   dgvFilteredFacilityList.Columns("AIRSNumber").DisplayIndex = 0
            '   dgvFilteredFacilityList.Columns("strFacilityName").HeaderText = "Facility Name"
            '   dgvFilteredFacilityList.Columns("strFacilityName").DisplayIndex = 1
            '   dgvFilteredFacilityList.Columns("strFacilityCity").HeaderText = "City"
            '   dgvFilteredFacilityList.Columns("strFacilityCity").DisplayIndex = 2
            '   dgvFilteredFacilityList.Columns("strCMSMember").HeaderText = "Current CMS Status"
            '   dgvFilteredFacilityList.Columns("strCMSMember").DisplayIndex = 3
            '   dgvFilteredFacilityList.Columns("strClass").HeaderText = "Current Classification"
            '   dgvFilteredFacilityList.Columns("strClass").DisplayIndex = 4
            '   dgvFilteredFacilityList.Columns("strOperationalStatus").HeaderText = "Current Operational Status"
            '   dgvFilteredFacilityList.Columns("strOperationalStatus").DisplayIndex = 5
            '   dgvFilteredFacilityList.Columns("LastInspection").HeaderText = "Last Inspection"
            '   dgvFilteredFacilityList.Columns("LastInspection").DisplayIndex = 6
            '   dgvFilteredFacilityList.Columns("LastInspection").DefaultCellStyle.Format = "dd-MMM-yyyy"
            '   dgvFilteredFacilityList.Columns("LastFCE").HeaderText = "Last FCE"
            '   dgvFilteredFacilityList.Columns("LastFCE").DisplayIndex = 7
            '   dgvFilteredFacilityList.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
            '   dgvFilteredFacilityList.Columns("SSCPEngineer").HeaderText = "SSCP Engineer"
            '   dgvFilteredFacilityList.Columns("SSCPEngineer").DisplayIndex = 8
            '   dgvFilteredFacilityList.Columns("strUnitDesc").HeaderText = "SSCP Title"
            '   dgvFilteredFacilityList.Columns("strUnitDesc").DisplayIndex = 9
            '   dgvFilteredFacilityList.Columns("strDistrictResponsible").HeaderText = "District Source"
            '   dgvFilteredFacilityList.Columns("strDistrictResponsible").DisplayIndex = 10
            '   dgvFilteredFacilityList.Columns("strCountyName").HeaderText = "County"
            '   dgvFilteredFacilityList.Columns("strCountyName").DisplayIndex = 11

            '   lblFilteredCount.Text = "Count: " & dgvFilteredFacilityList.Rows.Count.ToString
            '   chbIgnoreFiscalYear.Checked = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportFiltered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFiltered.Click
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvFilteredFacilityList.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvFilteredFacilityList.ColumnCount - 1
                        .Cells(1, i + 1) = dgvFilteredFacilityList.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvFilteredFacilityList.ColumnCount - 1
                        For j = 0 To dgvFilteredFacilityList.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvFilteredFacilityList.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try
    End Sub
    Private Sub btnExportSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportSelected.Click
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvSelectedFacilityList.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvSelectedFacilityList.ColumnCount - 1
                        .Cells(1, i + 1) = dgvSelectedFacilityList.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvSelectedFacilityList.ColumnCount - 1
                        For j = 0 To dgvSelectedFacilityList.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvSelectedFacilityList.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = ""
                Eng = ""

                AIRSNum = dgvSelectedFacilityList(0, i).Value
                Eng = cboSSCPEngineer.SelectedValue

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & Eng & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and " & DBNameSpace & ".sscpinspectionsrequired.intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, strAssigningManager, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & Eng & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(2, i).Value = cboSSCPEngineer.Text
            Next

            MsgBox("Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "numSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "'" & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPUnit, strAssigningManager, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & cboSSCPUnit2.SelectedValue & "', " & _
                    "'" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(3, i).Value = cboSSCPUnit2.Text
            Next

            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPDistrictResponsible set " & _
                    "strDistrictResponsible = '" & DistResp & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPDistrictResponsible " & _
                    "values " & _
                    "('0413" & AIRSNum & ", '" & DistResp & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "strInspectionRequired = '" & InspectionRequired & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "strInspectionRequired, strAssigningManager, datAssigningDate) " & _
                    "values " & _
                    "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & InspectionRequired & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(5, i).Value = InspectionNote
            Next

            MsgBox("Inspection(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "strFCERequired = '" & FCERequired & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "strFCERequired, strAssigningManager, datAssigningDate) " & _
                   "values " & _
                   "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                   "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                   "'" & FCERequired & "', '" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(7, i).Value = FCENote
            Next

            MsgBox("FCE(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & Eng & "', " & _
                    "numSSCPUnit = '" & cboSSCPUnit2.SelectedValue & "' , " & _
                    "strInspectionRequired = '" & InspectionRequired & "', " & _
                    "strFCERequired = '" & FCERequired & "', " & _
                    "STRASSIGNINGMANAGER = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intyear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, numSSCPUnit, " & _
                    "strInspectionRequired, strFCERequired, " & _
                    "STRASSIGNINGMANAGER, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'" & Eng & "', '" & cboSSCPUnit2.SelectedValue & "', " & _
                    "'" & InspectionRequired & "', '" & FCERequired & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') " 
                End If
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPDistrictResponsible set " & _
                    "strDistrictResponsible = '" & DistResp & "', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "datAssigningDate = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPDistrictResponsible " & _
                    "values " & _
                    "('0413" & AIRSNum & ", '" & DistResp & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "
                End If
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(4, i).Value = DistResp
                dgvSelectedFacilityList(2, i).Value = cboSSCPEngineer.Text
                dgvSelectedFacilityList(3, i).Value = cboSSCPUnit2.Text
                dgvSelectedFacilityList(5, i).Value = InspectionRequired
                dgvSelectedFacilityList(7, i).Value = FCERequired
            Next

            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "From " & DBNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '0413" & mtbForcedAIRS.Text & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Select " & _
                "strFacilityName " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbForcedAIRS.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = ""
              '  Eng = ""

                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                    "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                    "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                    "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "(numKey, strAIRSNumber, intYear, " & _
                    "numSSCPEngineer, strAssigningManager, DATASSIGNINGDATE) " & _
                    "values " & _
                    "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                    "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'', '" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(2, i).Value = ""
            Next
            MsgBox("Assignment(s) Cleared", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' " & _
                "and intYear = '" & cboFiscalYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "numSSCPUnit = '', " & _
                    "strAssigningManager = '" & UserGCode & "', " & _
                  "DATASSIGNINGDATE = '" & OracleDate & "' " & _
                  "where strAIRSNumber = '0413" & AIRSNum & "'" & _
                  "and intYear = '" & cboFiscalYear.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                 "(numKey, strAIRSNumber, intYear, " & _
                 "numSSCPUnit, strAssigningManager, DATASSIGNINGDATE) " & _
                 "values " & _
                 "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                 "'0413" & AIRSNum & "', '" & cboFiscalYear.Text & "', " & _
                    "'', " & _
                    "'" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                End If

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(3, i).Value = ""
            Next
            MsgBox("Unit Assignment(s) Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                AIRSNum = dgvSelectedFacilityList(0, i).Value

                SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                "strCMSMember = '" & CMSStatus & "' " & _
                "where strAIRSNumber = '0413" & AIRSNum & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                dgvSelectedFacilityList(9, i).Value = CMSStatus
            Next

            For i = 0 To dgvSelectedFacilityList.RowCount - 1
                dgvSelectedFacilityList(5, i).Value = CMSStatus
            Next
            MsgBox("Compliance Monitoring Strategy Updated", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboFiscalYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiscalYear.TextChanged
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyYear.Click
        Try
            Dim AIRSNumber As String = ""
            Dim SSCPEngineer As String = ""
            Dim SSCPUnit As String = ""
            Dim InspectionRequired As String = ""
            Dim FCERequired As String = ""
            Dim AssigningManager As String = ""

            If cboExistingYears.Text = "" Then '  cboExistingYears.Items.Contains(cboExistingYears.Text) = True Then
                MsgBox("Select an existing year from the dropdown." & vbCrLf & "No Data altered", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If cboExistingYears.Items.Contains(cboExistingYears.Text) Then
            Else
                MsgBox("Select an existing year from the dropdown." & vbCrLf & "No Data altered", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If mtbNewYear.Text = "" Or mtbNewYear.Text.Length <> "4" Then
                MsgBox("Please enter a complete 4-digit Year" & vbCrLf & "No Data altered", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If chbClearExistingData.Checked = True Then
                SQL = "Delete " & DBNameSpace & ".sscpinspectionsrequired " & _
                "where intYear = '" & mtbNewYear.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            SQL = "select * " & _
            "from " & DBNameSpace & ".sscpinspectionsrequired " & _
            "where intYear = '" & cboExistingYears.Text & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If IsDBNull(dr.Item("strInspectionRequired")) Then
                    InspectionRequired = ""
                Else
                    InspectionRequired = dr.Item("strInspectionRequired")
                End If
                If IsDBNull(dr.Item("numSSCPEngineer")) Then
                    SSCPEngineer = ""
                Else
                    SSCPEngineer = dr.Item("numSSCPEngineer")
                End If
                If IsDBNull(dr.Item("numSSCPUnit")) Then
                    SSCPUnit = ""
                Else
                    SSCPUnit = dr.Item("numSSCPUnit")
                End If
                If IsDBNull(dr.Item("strFCERequired")) Then
                    FCERequired = ""
                Else
                    FCERequired = dr.Item("strFCERequired")
                End If
                If IsDBNull(dr.Item("strAssigningManager")) Then
                    AssigningManager = UserGCode
                Else
                    AssigningManager = dr.Item("strAssigningManager")
                End If

                SQL = "Select count(*) as ckCount " & _
                "from " & DBNameSpace & ".sscpinspectionsrequired " & _
                "where strAIRSNumber = '" & AIRSNumber & "' " & _
                "and intYear = '" & mtbNewYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("CkCount")) Then
                        temp = "0"
                    Else
                        temp = dr2.Item("ckCount")
                    End If
                End While
                dr2.Close()
                If temp = "0" Then
                    SQL = "Insert into " & DBNameSpace & ".sscpinspectionsrequired " & _
                    "values " & _
                    "((select max(numKey) + 1 from " & DBNameSpace & ".SSCPinspectionsRequired), " & _
                    "'" & AIRSNumber & "', '" & mtbNewYear.Text & "', " & _
                    "'" & SSCPEngineer & "', '" & SSCPUnit & "', " & _
                    "'" & InspectionRequired & "', '" & FCERequired & "', " & _
                    "'" & AssigningManager & "', '" & OracleDate & "' ) "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                Else
                End If
            End While
            dr.Close()
            If cboExistingYears.Items.Contains(mtbNewYear.Text) Then
            Else
                cboExistingYears.Items.Add(mtbNewYear.Text)
            End If
            If cboFiscalYear.Items.Contains(mtbNewYear.Text) Then
            Else
                cboFiscalYear.Items.Add(mtbNewYear.Text)
            End If

            MsgBox("New data entered into desired year.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

 
    
    Private Sub btnRunTitleVSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunTitleVSearch.Click
        Try
            'Dim StartDate As String = OracleDate
            'Dim EndDate As String = OracleDate

            'StartDate = Format(Date.Today, "dd-MMM-yyyy")

            'StartDate = Format(CDate(StartDate).AddMonths(-51), "dd-MMM-yyyy")
            'EndDate = Format(CDate(EndDate).AddMonths(-50), "dd-MMM-yyyy")


            SQL = "select " & _
"substr(airbranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
"airbranch.APBFacilityInformation.strFacilityname,  strOperationalStatus " & _
"from AIRBranch.APBFacilityinformation, AIRBranch.APBHeaderdata , " & _
"(select distinct substr(AIRBranch.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber " & _
"from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation, " & _
"AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, " & _
"AIRBranch.SSPPApplicationTracking " & _
"where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
"and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
"and AIRBranch.SSPPApplicationMaster.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRSNumber  " & _
"and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
"and strPermitNumber Like '%V__0'  and AIRBranch.APBHeaderData.strOperationalStatus <> 'X'  " & _
"and substr(AIRBranch.apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
"and ((DatPermitissued is not null and DatPermitissued < add_months(sysdate, -51) ) " & _
"or (DATEFFECTIVE is not null and  DATEFFECTIVE < add_months(sysdate, -51))) " & _
"minus " & _
"(select distinct substr(airbranch.SSPPApplicationmaster.strairsnumber, 5) as AIRSNumber " & _
"from AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, AIRBranch.SSPPApplicationTracking, " & _
"AIRBranch.APBHeaderData " & _
"where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber  " & _
"and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
"and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
"and ((strApplicationType =  '14' or strApplicationType = '16' or strApplicationType = '27' ) " & _
"or (strPermitNumber Like '%V__0'  )) " & _
"and ((datPermitIssued between  add_months(sysdate, -60) and sysdate  " & _
"and datEffective between  add_months(sysdate, -60) and sysdate ) " & _
"or (datREceiveddate between  add_months(sysdate, -60) and sysdate  ) ) )) TVFacilities " & _
"where AIRbranch.APBFacilityInformation.strAIRSNumber = '0413'||TVFacilities.AIRSNumber " & _
"and AIRbranch.APBFacilityInformation.strAIRSNumber = AIRbranch.APBHeaderdata.strairsnumber "


            SQL = "select " & _
            " count(*) as LateTV " & _
            "from AIRBranch.APBFacilityinformation, AIRBranch.APBHeaderdata , " & _
            "(select distinct substr(AIRBranch.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber " & _
            "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, " & _
            "AIRBranch.SSPPApplicationTracking " & _
            "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
            "and strPermitNumber Like '%V__0'  and AIRBranch.APBHeaderData.strOperationalStatus <> 'X'  " & _
            "and substr(AIRBranch.apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
            "and ((DatPermitissued is not null and DatPermitissued < add_months(sysdate, -51) ) " & _
            "or (DATEFFECTIVE is not null and  DATEFFECTIVE < add_months(sysdate, -51))) " & _
            "minus " & _
            "(select distinct substr(airbranch.SSPPApplicationmaster.strairsnumber, 5) as AIRSNumber " & _
            "from AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, AIRBranch.SSPPApplicationTracking, " & _
            "AIRBranch.APBHeaderData " & _
            "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
            "and ((strApplicationType =  '14' or strApplicationType = '16' or strApplicationType = '27' ) " & _
            "or (strPermitNumber Like '%V__0'  )) " & _
            "and ((datPermitIssued between  add_months(sysdate, -51) and sysdate  " & _
            "and datEffective between  add_months(sysdate, -51) and sysdate ) " & _
            "or (datREceiveddate between  add_months(sysdate, -51) and sysdate  ) ) )) TVFacilities " & _
            "where AIRbranch.APBFacilityInformation.strAIRSNumber = '0413'||TVFacilities.AIRSNumber " & _
            "and AIRbranch.APBFacilityInformation.strAIRSNumber = AIRbranch.APBHeaderdata.strairsnumber "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("LateTV")) Then
                    txtTitleVRenewals.Text = "0"
                Else
                    txtTitleVRenewals.Text = dr.Item("lateTV")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbTitleVRenewal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbTitleVRenewal.LinkClicked
        Try
            SQL = "select distinct " & _
            "substr(airbranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            "airbranch.APBFacilityInformation.strFacilityname,  strOperationalStatus " & _
            "from AIRBranch.APBFacilityinformation, AIRBranch.APBHeaderdata , " & _
            "(select distinct substr(AIRBranch.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber " & _
            "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, " & _
            "AIRBranch.SSPPApplicationTracking " & _
            "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
            "and strPermitNumber Like '%V__0'  and AIRBranch.APBHeaderData.strOperationalStatus <> 'X'  " & _
            "and substr(AIRBranch.apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
            "and ((DatPermitissued is not null and DatPermitissued < add_months(sysdate, -51) ) " & _
            "or (DATEFFECTIVE is not null and  DATEFFECTIVE < add_months(sysdate, -51))) " & _
            "minus " & _
            "(select distinct substr(airbranch.SSPPApplicationmaster.strairsnumber, 5) as AIRSNumber " & _
            "from AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, AIRBranch.SSPPApplicationTracking, " & _
            "AIRBranch.APBHeaderData " & _
            "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
            "and ((strApplicationType =  '14' or strApplicationType = '16' or strApplicationType = '27' ) " & _
            "or (strPermitNumber Like '%V__0'  )) " & _
            "and ((datPermitIssued between  add_months(sysdate, -51) and sysdate  " & _
            "and datEffective between  add_months(sysdate, -51) and sysdate ) " & _
            "or (datREceiveddate between  add_months(sysdate, -51) and sysdate  ) ) )) TVFacilities " & _
            "where AIRbranch.APBFacilityInformation.strAIRSNumber = '0413'||TVFacilities.AIRSNumber " & _
            "and AIRbranch.APBFacilityInformation.strAIRSNumber = AIRbranch.APBHeaderdata.strairsnumber "

            SQL = "select distinct " & _
            "substr(airbranch.APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            "airbranch.APBFacilityInformation.strFacilityname,  strOperationalStatus, " & _
            "(strLastName||', '||strFirstName) as StaffResponsible " & _
            "from AIRBranch.APBFacilityinformation, AIRBranch.APBHeaderdata , " & _
            "AIRBranch.EPDUserProfiles, AIRBranch.SSCPFacilityAssignment ,  " & _
            "(select distinct substr(AIRBranch.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber " & _
            "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, " & _
            "AIRBranch.SSPPApplicationTracking " & _
            "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
            "and strPermitNumber Like '%V__0'  and AIRBranch.APBHeaderData.strOperationalStatus <> 'X'  " & _
            "and substr(AIRBranch.apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
            "and ((DatPermitissued is not null and DatPermitissued < add_months(sysdate, -51) ) " & _
            "or (DATEFFECTIVE is not null and  DATEFFECTIVE < add_months(sysdate, -51))) " & _
            "minus " & _
            "(select distinct substr(airbranch.SSPPApplicationmaster.strairsnumber, 5) as AIRSNumber " & _
            "from AIRBranch.SSPPApplicationMaster,  AIRBranch.SSPPApplicationData, AIRBranch.SSPPApplicationTracking, " & _
            "AIRBranch.APBHeaderData " & _
            "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber  " & _
            "and AIRBranch.SSPPApplicationMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
            "and ((strApplicationType =  '14' or strApplicationType = '16' or strApplicationType = '27' ) " & _
            "or (strPermitNumber Like '%V__0'  )) " & _
            "and ((datPermitIssued between  add_months(sysdate, -51) and sysdate  " & _
            "and datEffective between  add_months(sysdate, -51) and sysdate ) " & _
            "or (datREceiveddate between  add_months(sysdate, -51) and sysdate  ) ) )) TVFacilities " & _
            "where AIRbranch.APBFacilityInformation.strAIRSNumber = '0413'||TVFacilities.AIRSNumber " & _
            "and AIRbranch.APBFacilityInformation.strAIRSNumber = AIRbranch.APBHeaderdata.strairsnumber " & _
            "and airbranch.SSCPFacilityAssignment.strSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " & _
            "and AIRbranch.APBFacilityInformation.strAIRSNumber = Airbranch.SSCPFacilityAssignment.strairsnumber " & _
            "order by AIRSNumber "

            dsStatisticalReport = New DataSet
            daStatisticalReport = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daStatisticalReport.Fill(dsStatisticalReport, "TotalFacilities")
            dgvStatistialReports.DataSource = dsStatisticalReport
            dgvStatistialReports.DataMember = "TotalFacilities"
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            dgvStatistialReports.RowHeadersVisible = False
            dgvStatistialReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatistialReports.AllowUserToResizeColumns = True
            dgvStatistialReports.AllowUserToAddRows = False
            dgvStatistialReports.AllowUserToDeleteRows = False
            dgvStatistialReports.AllowUserToOrderColumns = True
            dgvStatistialReports.AllowUserToResizeRows = True

            dgvStatistialReports.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvStatistialReports.Columns("AIRSNumber").DisplayIndex = 0
            dgvStatistialReports.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvStatistialReports.Columns("strFacilityName").DisplayIndex = 1
            dgvStatistialReports.Columns("strFacilityName").Width = 150
            dgvStatistialReports.Columns("strOperationalStatus").HeaderText = "Op. Status"
            dgvStatistialReports.Columns("strOperationalStatus").DisplayIndex = 2
            dgvStatistialReports.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvStatistialReports.Columns("StaffResponsible").DisplayIndex = 3
            dgvStatistialReports.Columns("StaffResponsible").Width = 150

            txtStatisticalCount.Text = dgvStatistialReports.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportMiscToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportMiscToExcel.Click
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvMiscReport.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvMiscReport.ColumnCount - 1
                        If IsDBNull(dgvMiscReport.Columns(i).HeaderText.ToString) Then
                            .Cells(1, i + 1) = "No Header"
                        Else
                            .Cells(1, i + 1) = dgvMiscReport.Columns(i).HeaderText.ToString
                        End If
                    Next

                    For i = 0 To dgvMiscReport.ColumnCount - 1
                        For j = 0 To dgvMiscReport.RowCount - 1
                            If IsDBNull(dgvMiscReport.Item(i, j).Value.ToString) Then
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = "  "
                            Else
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = dgvMiscReport.Item(i, j).Value.ToString
                            End If

                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub

    Private Sub txtManualAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManualAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtManualAIRSNumber.SelectAll()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
End Class