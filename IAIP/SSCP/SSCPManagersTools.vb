Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports Iaip.DAL


Public Class SSCPManagersTools

#Region "Page Load subs"

    Private Sub SSCPManagersTools_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            cboFilterEngineer1.Visible = False
            cboFilterEngineer2.Visible = False
            txtFacSearch1.Visible = True
            txtFacSearch2.Visible = True

            LoadComboBoxes()

            LoadSelectedFacilitesGrid()

            If Not CurrentUser.HasRoleType(RoleType.ProgramManager) OrElse CurrentUser.HasRoleType(RoleType.BranchChief) Then
                TCNewFacilitySearch.TabPages.Remove(TPCopyYear)
            End If

            If AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" Then
                btnAddToCmsUniverse.Visible = False
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
                .SelectedIndex = 0
            End With

            If Date.Today.Month < 10 Then cboFiscalYear.SelectedIndex = 1

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
                WHERE (NUMPROGRAM = 4 OR NUMBRANCH = 5 )
                AND NUMEMPLOYEESTATUS = 1
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

            SQL = "SELECT *
                FROM LOOKUPEPDUNITS
                WHERE (NUMPROGRAMCODE = 4
                    OR NUMUNITCODE IN (44, 43, 42, 41, 40, 39, 38, 37))
                and Active = 1"

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
                .Add("All")
                .Add("A - Major source")
                .Add("S - Synthetic Minor")
                .Add("M - Mega-site")
                .Add("Not in CMS")
            End With

            cboCMSFrequency.SelectedIndex = 0

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
                .DataSource = GetSharedData(SharedTable.DistrictOffices).Copy
                .DisplayMember = "DistrictName"
                .ValueMember = "DistrictName"
            End With

            With cboDistrictFilter2
                .DataSource = GetSharedData(SharedTable.DistrictOffices).Copy
                .DisplayMember = "DistrictName"
                .ValueMember = "DistrictName"
            End With

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

    Private Sub LoadSelectedFacilitesGrid()
        dgvSelectedFacilityList.RowHeadersVisible = False
        dgvSelectedFacilityList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSelectedFacilityList.AllowUserToResizeColumns = True
        dgvSelectedFacilityList.AllowUserToAddRows = False
        dgvSelectedFacilityList.AllowUserToDeleteRows = False
        dgvSelectedFacilityList.AllowUserToOrderColumns = True
        dgvSelectedFacilityList.AllowUserToResizeRows = True
        dgvSelectedFacilityList.ColumnHeadersHeight = 35

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
            Select Case Strings.Left(cboCMSFrequency.Text, 2)
                Case "A "
                    CMSStatus = " and strCMSMember = 'A' "
                Case "S "
                    CMSStatus = " and strCMSMember = 'S' "
                Case "M "
                    CMSStatus = " and strCMSMember = 'M' "
                Case "No"
                    CMSStatus = " and strCMSMember is null "
                Case "Al"
                    CMSStatus = " and strCMSMember is not null "
            End Select

            Dim SQL As String = "Select * " &
                "from VW_SSCP_CMSWarning " &
                "where AIRSNumber is not Null " &
                CMSStatus

            dgvCMSUniverse.DataSource = DB.GetDataTable(SQL)

            dgvCMSUniverse.RowHeadersVisible = False
            dgvCMSUniverse.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCMSUniverse.AllowUserToResizeColumns = True
            dgvCMSUniverse.AllowUserToAddRows = False
            dgvCMSUniverse.AllowUserToDeleteRows = False
            dgvCMSUniverse.AllowUserToOrderColumns = True
            dgvCMSUniverse.AllowUserToResizeRows = True
            dgvCMSUniverse.ColumnHeadersHeight = 35
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
            dgvCMSUniverse.Columns("strCountyName").HeaderText = "County Code"
            dgvCMSUniverse.Columns("strCountyName").DisplayIndex = 7
            dgvCMSUniverse.Columns("STRClass").HeaderText = "Class"

            txtCMSCount.Text = dgvCMSUniverse.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub AddFacilityToCMS()
        Try
            Dim CMSState As String = Nothing

            If rdbCMSClassA.Checked Then
                CMSState = "A"
            ElseIf rdbCMSClassS.Checked Then
                CMSState = "S"
            ElseIf rdbCMSClassM.Checked Then
                CMSState = "M"
            ElseIf Not rdbCMSClassNone.Checked Then
                MsgBox("Select a CMS status first.", MsgBoxStyle.Information, "SSCP Managers Tools")
                Return
            End If

            Dim SQL As String = "Update APBSupplamentalData set " &
                "strCMSMember = @r " &
                "where strAIRSNumber = @airs "

            Dim params As SqlParameter() = {
                New SqlParameter("@airs", "0413" & txtCMSAIRSNumber.Text),
                New SqlParameter("@r", CMSState)
            }

            If DB.RunCommand(SQL, params) Then
                MessageBox.Show("CMS status saved", "Success")
            Else
                MessageBox.Show("There was an error saving the CMS status", "Error")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub llbViewCMSUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewCMSUniverse.LinkClicked
        LoadCMSUniverse()
    End Sub

    Private Sub llbCMSOpenFacilitySummary_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCMSOpenFacilitySummary.LinkClicked
        OpenFormFacilitySummary(txtCMSAIRSNumber.Text)
    End Sub

    Private Sub btnAddToCmsUniverse_LinkClicked(sender As Object, e As EventArgs) Handles btnAddToCmsUniverse.Click
        AddFacilityToCMS()
    End Sub

    Private Sub dgvStatisticalReports_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvStatisticalReports.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvStatisticalReports.HitTest(e.X, e.Y)

        Try

            If dgvStatisticalReports.RowCount > 0 AndAlso hti.RowIndex <> -1 Then
                txtRecordNumber.Text = dgvStatisticalReports(0, hti.RowIndex).Value.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbViewRecord_Click(sender As Object, e As EventArgs) Handles llbViewRecord.LinkClicked
        Try
            If txtRecordNumber.Text <> "" AndAlso txtRecordNumber.Text.Length = 8 Then
                OpenFacilitySummary()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenFacilitySummary()
        OpenFormFacilitySummary(txtRecordNumber.Text)
    End Sub

    Private Sub dgvCMSUniverse_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvCMSUniverse.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvCMSUniverse.HitTest(e.X, e.Y)

            If dgvCMSUniverse.RowCount > 0 AndAlso
                hti.RowIndex <> -1 AndAlso
                dgvCMSUniverse.Columns(0).HeaderText = "AIRS Number" AndAlso
                Not IsDBNull(dgvCMSUniverse(0, hti.RowIndex).Value) Then

                txtCMSAIRSNumber.Text = dgvCMSUniverse(0, hti.RowIndex).Value.ToString
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
                        SqlFilter.Add(" NUMSSCPENGINEER = 0 ")
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
                        SqlFilter.Add(" NUMSSCPENGINEER = 0 ")
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
                Dim airslist As String() = txtManualAIRSNumber.Text.Split({vbNewLine, "\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)

                If airslist.Length > 0 Then
                    For i As Integer = 0 To airslist.Length - 1
                        airslist(i) = "0413" & airslist(i)
                    Next

                    SqlFilter.Add(" v.STRAIRSNUMBER IN (select * from @airslist) ")

                    ParamList.Add(airslist.AsTvpSqlParameter("@airslist"))
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

            If chbIgnoreFiscalYear.Checked Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvFilteredFacilityList.Rows.Count

            If i > 0 Then
                temp = dgvFilteredFacilityList(0, dgvFilteredFacilityList.CurrentRow.Index).Value.ToString
                For i = 0 To dgvSelectedFacilityList.Rows.Count - 1
                    If dgvSelectedFacilityList(0, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    Using dgvRow As New DataGridViewRow
                        dgvRow.CreateCells(dgvSelectedFacilityList)
                        dgvRow.Cells(0).Value = dgvFilteredFacilityList(0, dgvFilteredFacilityList.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvFilteredFacilityList(1, dgvFilteredFacilityList.CurrentRow.Index).Value

                        If chbIgnoreFiscalYear.Checked Then
                            If Not IsDBNull(dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(2).Value = dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(3).Value = dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(4).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            dgvRow.Cells(5).Value = ""

                            'Last Inspection Date
                            If Not IsDBNull(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                            End If
                            dgvRow.Cells(7).Value = ""
                            'Last FCE date
                            If Not IsDBNull(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                        Else
                            If Not IsDBNull(dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(2).Value = dgvFilteredFacilityList(10, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(3).Value = dgvFilteredFacilityList(11, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(12, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(4).Value = dgvFilteredFacilityList(12, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(5).Value = dgvFilteredFacilityList(6, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            'Last Inspection Date
                            If Not IsDBNull(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(7).Value = dgvFilteredFacilityList(8, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                            'Last FCE date
                            If Not IsDBNull(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(9, dgvFilteredFacilityList.CurrentRow.Index).Value), "dd-MMM-yyyy"))
                            End If
                            If Not IsDBNull(dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value) Then
                                dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, dgvFilteredFacilityList.CurrentRow.Index).Value
                            End If
                        End If
                        'End If
                        dgvSelectedFacilityList.Rows.Add(dgvRow)
                    End Using
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
            Dim i As Integer = 0

            dgvSelectedFacilityList.Rows.Clear()

            For i = 0 To dgvFilteredFacilityList.Rows.Count - 1
                Using dgvRow As New DataGridViewRow
                    dgvRow.CreateCells(dgvSelectedFacilityList)
                    dgvRow.Cells(0).Value = dgvFilteredFacilityList(0, i).Value
                    dgvRow.Cells(1).Value = dgvFilteredFacilityList(1, i).Value

                    If chbIgnoreFiscalYear.Checked Then
                        If Not IsDBNull(dgvFilteredFacilityList(8, i).Value) Then
                            dgvRow.Cells(2).Value = dgvFilteredFacilityList(8, i).Value
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(9, i).Value) Then

                            dgvRow.Cells(3).Value = dgvFilteredFacilityList(9, i).Value
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(10, i).Value) Then

                            dgvRow.Cells(4).Value = dgvFilteredFacilityList(10, i).Value
                        End If
                        dgvRow.Cells(5).Value = ""

                        'Last Inspection Date
                        If Not IsDBNull(dgvFilteredFacilityList(6, i).Value) Then

                            dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(6, i).Value), "dd-MMM-yyyy"))
                        End If
                        dgvRow.Cells(7).Value = ""
                        'Last FCE date
                        If Not IsDBNull(dgvFilteredFacilityList(7, i).Value) Then
                            dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, i).Value), "dd-MMM-yyyy"))
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(3, i).Value) Then
                            dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, i).Value
                        End If
                    Else
                        If Not IsDBNull(dgvFilteredFacilityList(10, i).Value) Then
                            dgvRow.Cells(2).Value = dgvFilteredFacilityList(10, i).Value
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(11, i).Value) Then
                            dgvRow.Cells(3).Value = dgvFilteredFacilityList(11, i).Value
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(12, i).Value) Then
                            dgvRow.Cells(4).Value = dgvFilteredFacilityList(12, i).Value
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(6, i).Value) Then
                            dgvRow.Cells(5).Value = dgvFilteredFacilityList(6, i).Value
                        End If
                        'Last Inspection Date
                        If Not IsDBNull(dgvFilteredFacilityList(7, i).Value) Then
                            dgvRow.Cells(6).Value = CStr(Format(CDate(dgvFilteredFacilityList(7, i).Value), "dd-MMM-yyyy"))
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(8, i).Value) Then
                            dgvRow.Cells(7).Value = dgvFilteredFacilityList(8, i).Value
                        End If
                        'Last FCE date
                        If Not IsDBNull(dgvFilteredFacilityList(9, i).Value) Then
                            dgvRow.Cells(8).Value = CStr(Format(CDate(dgvFilteredFacilityList(9, i).Value), "dd-MMM-yyyy"))
                        End If
                        If Not IsDBNull(dgvFilteredFacilityList(3, i).Value) Then
                            dgvRow.Cells(9).Value = dgvFilteredFacilityList(3, i).Value
                        End If
                    End If
                    dgvSelectedFacilityList.Rows.Add(dgvRow)
                End Using
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
                Return
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityStaffAssignment(New Apb.ApbFacilityId(row.Cells(0).Value.ToString), CInt(cboFiscalYear.Text), CInt(cboSSCPEngineer.SelectedValue), CurrentUser.UserID)
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
                Return
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityUnitAssignment(New Apb.ApbFacilityId(row.Cells(0).Value.ToString), CInt(cboFiscalYear.Text), CInt(cboSSCPUnit2.SelectedValue), CurrentUser.UserID)
                row.Cells(3).Value = cboSSCPUnit2.Text
            Next

            MsgBox("Unit Assignments Completed", MsgBoxStyle.Information, "Managers Tools")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveDistResponsible_Click(sender As Object, e As EventArgs) Handles btnSaveDistResponsible.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            Dim SQL As String
            Dim DistResp As String = rdbDistResponsibleTrue.Checked.ToString

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                SQL = "Select 1 " &
                    "from SSCPDistrictResponsible " &
                    "where strAIRSNumber = @airs "

                Dim p As New SqlParameter("@airs", "0413" & row.Cells(0).Value.ToString)

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
                Return
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
                    'Modified by TK for Jira issue: IAIP-603 "Syntax errors in SSCP Managers Tools" - 10/5/2017
                    SQL = "Insert into SSCPInspectionsRequired " &
                        "(numKey, strAIRSNumber, intYear, strInspectionRequired, strAssigningManager, datAssigningDate) " &
                        "values " &
                        "((select max(numKey) + 1 from SSCPInspectionsRequired), @airs, @year, @i, @mgr,  GETDATE())"
                End If

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@i", rdbInspectionRequired.Checked.ToString),
                    New SqlParameter("@mgr", CurrentUser.UserID),
                    New SqlParameter("@airs", "0413" & row.Cells(0).Value.ToString),
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
            Dim SQL As String

            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                If Sscp.FacilityAssignmentYearExists(row.Cells(0).Value.ToString, CInt(cboFiscalYear.Text)) Then
                    SQL = "Update SSCPInspectionsRequired set " &
                        "strFCERequired = @i, " &
                        "strAssigningManager = @mgr, " &
                        "datAssigningDate =  GETDATE()  " &
                        "where strAIRSNumber = @airs " &
                        "And intYear = @year "
                Else
                    'Modified by TK for Jira issue: IAIP-603 "Syntax errors in SSCP Managers Tools" - 10/5/2017
                    SQL = "Insert into SSCPInspectionsRequired" &
                        "(numKey, strAIRSNumber, intYear, strFCERequired, strAssigningManager, datAssigningDate) " &
                        "values " &
                        "((select max(numKey) + 1 from SSCPInspectionsRequired), @airs, @year, @i, @mgr, GETDATE())"
                End If

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@i", rdbFCERequired.Checked.ToString),
                    New SqlParameter("@mgr", CurrentUser.UserID),
                    New SqlParameter("@airs", "0413" & row.Cells(0).Value.ToString),
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
                    MsgBox("AIRS # does Not exist in the database.", MsgBoxStyle.Exclamation, "SSCP Managers Tools")
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
                Return
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityStaffAssignment(New Apb.ApbFacilityId(row.Cells(0).Value.ToString), CInt(cboFiscalYear.Text), 0, CurrentUser.UserID)
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
                Return
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                DAL.Sscp.SetFacilityUnitAssignment(New Apb.ApbFacilityId(row.Cells(0).Value.ToString), CInt(cboFiscalYear.Text), Nothing, CurrentUser.UserID)
                row.Cells(3).Value = Nothing
            Next

            MsgBox("Unit Assignments Completed", MsgBoxStyle.Information, "Managers Tools")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveCMS_Click(sender As Object, e As EventArgs) Handles btnSaveCMS.Click
        Try
            If dgvSelectedFacilityList.RowCount = 0 Then
                MsgBox("There are no selected facilities." & vbCrLf & "NO Data Saved", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            Dim CMSStatus As String = Nothing

            If rdbCMS_A.Checked Then
                CMSStatus = "A"
            ElseIf rdbCMS_SM.Checked Then
                CMSStatus = "S"
            ElseIf rdbCmsMega.Checked Then
                CMSStatus = "M"
            ElseIf Not rdbCMS_None.Checked Then
                MsgBox("Select a CMS status first.", MsgBoxStyle.Information, "SSCP Managers Tools")
                Return
            End If

            For Each row As DataGridViewRow In dgvSelectedFacilityList.Rows
                Dim SQL As String = "Update APBSupplamentalData set " &
                    "strCMSMember = @c " &
                    "where strAIRSNumber = @airs "

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@c", CMSStatus),
                    New SqlParameter("@airs", "0413" & row.Cells(0).Value.ToString)
                }

                If DB.RunCommand(SQL, parameters) Then
                    row.Cells(9).Value = CMSStatus
                    MessageBox.Show("CMS status saved", "Success")
                Else
                    MessageBox.Show("There was an error saving the CMS status", "Error")
                End If
            Next

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
                Return
            Else
                oldYear = CType(cboExistingYears.Text, Integer)
            End If

            If mtbNewYear.Text = "" OrElse mtbNewYear.Text.Length <> 4 OrElse Not IsNumeric(mtbNewYear.Text) Then
                MsgBox("Please enter a complete 4-digit year" & vbCrLf & "No data altered", MsgBoxStyle.Information, Me.Text)
                Return
            Else
                targetYear = CType(mtbNewYear.Text, Integer)
            End If

            Dim confirmResult As DialogResult =
                MessageBox.Show("Warning: This may take a VERY, VERY long time. The IAIP will be unresponsive until finished. " &
                                "Are you sure you want to proceed?", "Confirm Patience",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If confirmResult = DialogResult.Cancel Then
                Return
            End If

            If DAL.Sscp.AssignmentYearExists(targetYear) Then
                If chbClearExistingData.Checked Then
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
                            Return
                        End If
                    End If
                Else
                    Dim dialogResult As DialogResult =
                        MessageBox.Show("Warning: This will merge data from " & oldYear & " into " & targetYear &
                                        ". Are you sure you want to proceed?", "Confirm Merge",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    If dialogResult = DialogResult.No Then
                        Return
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
            Dim SQL As String = "SELECT SUBSTRING(f.STRAIRSNUMBER, 5, 8)            AS [AIRS #],
                       f.STRFACILITYNAME                           AS [Facility Name],
                       h.STROPERATIONALSTATUS                      AS [Op. Status],
                       concat(p.STRLASTNAME, ', ', p.STRFIRSTNAME) AS [Staff Responsible]
                FROM APBFACILITYINFORMATION AS f
                    INNER JOIN APBHEADERDATA AS h
                    ON f.STRAIRSNUMBER = h.STRAIRSNUMBER
                    INNER JOIN (SELECT h.STRAIRSNUMBER
                                FROM SSPPAPPLICATIONMASTER AS m
                                    INNER JOIN APBHEADERDATA AS h
                                    ON h.STRAIRSNUMBER = m.STRAIRSNUMBER
                                    INNER JOIN SSPPAPPLICATIONTRACKING AS a
                                    ON m.STRAPPLICATIONNUMBER = a.STRAPPLICATIONNUMBER
                                    INNER JOIN SSPPAPPLICATIONDATA AS d
                                    ON m.STRAPPLICATIONNUMBER = d.STRAPPLICATIONNUMBER
                                WHERE (m.STRAPPLICATIONTYPE IN ('14', '16', '27') OR d.STRPERMITNUMBER LIKE '%V__0')
                                  AND h.STROPERATIONALSTATUS <> 'X'
                                  AND SUBSTRING(h.STRAIRPROGRAMCODES, 13, 1) = '1'
                                  AND (a.DATPERMITISSUED < DATEADD(MONTH, -51, GETDATE())
                                    OR a.DATEFFECTIVE < DATEADD(MONTH, -51, GETDATE()))
                                    except
                                SELECT m.STRAIRSNUMBER
                                FROM SSPPAPPLICATIONMASTER AS m
                                    INNER JOIN SSPPAPPLICATIONDATA AS d
                                    ON m.STRAPPLICATIONNUMBER = d.STRAPPLICATIONNUMBER
                                    INNER JOIN SSPPAPPLICATIONTRACKING AS a
                                    ON m.STRAPPLICATIONNUMBER = a.STRAPPLICATIONNUMBER
                                WHERE m.STRAPPLICATIONTYPE IN ('14', '16')
                                  AND (a.DATPERMITISSUED > DATEADD(MONTH, -51, GETDATE())
                                    or a.DATEFFECTIVE > DATEADD(MONTH, -51, GETDATE())
                                    or a.DATRECEIVEDDATE > DATEADD(MONTH, -51, GETDATE()))) AS t
                    ON t.STRAIRSNUMBER = f.STRAIRSNUMBER
                    LEFT JOIN (SELECT u.STRAIRSNUMBER, u.NUMSSCPENGINEER
                               FROM (SELECT MAX(INTYEAR) OVER (PARTITION BY STRAIRSNUMBER) AS MaxInspYear,
                                            INTYEAR, STRAIRSNUMBER, NUMSSCPENGINEER
                                     FROM SSCPINSPECTIONSREQUIRED) u
                               WHERE u.INTYEAR = u.MaxInspYear) AS i
                    ON i.STRAIRSNUMBER = f.STRAIRSNUMBER
                    LEFT JOIN EPDUSERPROFILES AS p
                    ON p.NUMUSERID = i.NUMSSCPENGINEER
                ORDER BY [AIRS #]"

            dgvStatisticalReports.DataSource = DB.GetDataTable(SQL)

            dgvStatisticalReports.RowHeadersVisible = False
            dgvStatisticalReports.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStatisticalReports.AllowUserToResizeColumns = True
            dgvStatisticalReports.AllowUserToAddRows = False
            dgvStatisticalReports.AllowUserToDeleteRows = False
            dgvStatisticalReports.AllowUserToOrderColumns = False
            dgvStatisticalReports.AllowUserToResizeRows = False

            dgvStatisticalReports.SanelyResizeColumns

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

    Private Sub btnExportCmsUniverseToExcel_Click(sender As Object, e As EventArgs) Handles btnExportCmsUniverseToExcel.Click
        dgvCMSUniverse.ExportToExcel(Me)
    End Sub

    Private Sub btnExportFiltered_Click(sender As Object, e As EventArgs) Handles btnExportFiltered.Click
        dgvFilteredFacilityList.ExportToExcel(Me)
    End Sub

    Private Sub btnExportSelected_Click(sender As Object, e As EventArgs) Handles btnExportSelected.Click
        dgvSelectedFacilityList.ExportToExcel(Me)
    End Sub

    Private Sub llbViewRecord_Click(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewRecord.LinkClicked

    End Sub

#End Region

End Class