Imports System.Data.OracleClient

Imports System.IO

Public Class IAIPQueryGenerator
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsCounty As DataSet
    Dim daCounty As OracleDataAdapter
    Dim dsDistrict As DataSet
    Dim daDistrict As OracleDataAdapter
    Dim dsSIPSubPart As DataSet
    Dim daSIPSubPart As OracleDataAdapter
    Dim dsPart61SubPart As DataSet
    Dim daPart61SubPart As OracleDataAdapter
    Dim dsPart60SubPart As DataSet
    Dim daPart60SubPart As OracleDataAdapter
    Dim dsPart63SubPart As DataSet
    Dim daPart63SubPart As OracleDataAdapter
    Dim dsSSCPStaff As DataSet
    Dim daSSCPStaff As OracleDataAdapter
    Dim dsSSCPUnit As DataSet
    Dim daSSCPUnit As OracleDataAdapter

    Dim dsSQLQuery As DataSet
    Dim daSQLQuery As OracleDataAdapter

    Private Sub DevQuerryGenerator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Panel1.Text = "Select Filter options...."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            cboCountySearch1.Visible = False
            cboCountySearch2.Visible = False
            cboDistrictSearch1.Visible = False
            cboDistrictSearch2.Visible = False
            cboOperationStatusSearch1.Visible = False
            cboOperationStatusSearch2.Visible = False
            cboClassificationSearch1.Visible = False
            cboClassificationSearch2.Visible = False
            cboCMSUniverseSearch1.Visible = False
            cboCMSUniverseSearch2.Visible = False
            cboSIPSearch1.Visible = False
            cboSIPSearch2.Visible = False
            cboPart61Search1.Visible = False
            cboPart61Search2.Visible = False
            cboPart60Search1.Visible = False
            cboPart60Search2.Visible = False
            cboPart63Search1.Visible = False
            cboPart63Search2.Visible = False

            If bgwQueryGenerator.IsBusy = True Then
                bgwQueryGenerator.CancelAsync()
            Else
                bgwQueryGenerator.WorkerReportsProgress = True
                bgwQueryGenerator.WorkerSupportsCancellation = True
                bgwQueryGenerator.RunWorkerAsync()
            End If

            DTPStartUpDateSearch1.Text = OracleDate
            DTPStartUpDateSearch1.Checked = False
            DTPStartUpDateSearch2.Text = OracleDate
            DTPStartUpDateSearch2.Checked = False
            DTPShutDownDateSearch1.Text = OracleDate
            DTPShutDownDateSearch1.Checked = False
            DTPShutDownDateSearch2.Text = OracleDate
            DTPShutDownDateSearch2.Checked = False
            DTPLastFCESearch1.Text = OracleDate
            DTPLastFCESearch1.Checked = False
            DTPLastFCESearch2.Text = OracleDate
            DTPLastFCESearch2.Checked = False

            TCQuerryOptions.Size = New Drawing.Size(TCQuerryOptions.Size.Width, 389)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".APBFacilitySummary_Load")
        Finally

        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadDataSets()
        Try
            dsCounty = New DataSet
            dsDistrict = New DataSet
            dsSIPSubPart = New DataSet
            dsPart61SubPart = New DataSet
            dsPart60SubPart = New DataSet
            dsPart63SubPart = New DataSet
            dsSSCPStaff = New DataSet
            dsSSCPUnit = New DataSet

            SQL = "Select strCountyCode, strCountyName, strAttainmentStatus " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "order by strCountyName "

            daCounty = New OracleDataAdapter(SQL, Conn)

            SQL = "select strDistrictCode, strDistrictName " & _
            "from " & DBNameSpace & ".LookUPDistricts " & _
            "order by strDistrictName "

            daDistrict = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
            "strSubPart, strSubPart as SubPartCode " & _
            "from " & DBNameSpace & ".LookUpSubPartSIP " & _
            "order by strSubPart "

            daSIPSubPart = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
            "strSubPart, strSubPart as SubPartCode " & _
            "from " & DBNameSpace & ".LookUpSubPart61 " & _
            "order by strSubPart "

            daPart61SubPart = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
            "strSubPart, strSubPart as SubPartCode " & _
            "from " & DBNameSpace & ".LookUpSubPart60 " & _
            "order by strSubPart "

            daPart60SubPart = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
            "strSubPart, strSubPart as SubPartCode " & _
            "from " & DBNameSpace & ".LookUpSubPart63 " & _
            "order by strSubPart "

            daPart63SubPart = New OracleDataAdapter(SQL, Conn)

            SQL = "select * " & _
                "from " & _
                "(Select  " & _
                "distinct(numUserID) as numUserID, " & _
                "(StrLastName||', '||strFirstname) as Staff " & _
                "from " & DBNameSpace & ".EPDUserProfiles  " & _
                "where numProgram = '4' " & _
                "union " & _
                "Select  " & _
                "distinct(numUserID) as numUserID, " & _
                "(StrLastName||', '||strFirstname) as Staff " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCPItemMaster   " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numuserID = " & DBNameSpace & ".SSCPItemMaster.strResponsibleStaff) " & _
                "order by Staff "

            daSSCPStaff = New OracleDataAdapter(SQL, Conn)

            SQL = "select " & _
            "numUnitCode, strUnitDesc " & _
            "from AIRBranch.LookUpEPDUnits " & _
            "where numProgramcode = '4' "

            daSSCPUnit = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daCounty.Fill(dsCounty, "County")
            daDistrict.Fill(dsDistrict, "District")
            daSIPSubPart.Fill(dsSIPSubPart, "SIPSubPart")
            daPart61SubPart.Fill(dsPart61SubPart, "Part61SubPart")
            daPart60SubPart.Fill(dsPart60SubPart, "Part60SubPart")
            daPart63SubPart.Fill(dsPart63SubPart, "Part63SubPart")
            daSSCPStaff.Fill(dsSSCPStaff, "SSCPStaff")
            daSSCPUnit.Fill(dsSSCPUnit, "SSCPUnit")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".LoadDataSets")
        Finally

        End Try
    End Sub
    Sub LoadComboBoxes()
        Try
            Dim dtCounty As New DataTable
            Dim dtCounty2 As New DataTable
            Dim dtDistrict As New DataTable
            Dim dtDistrict2 As New DataTable
            Dim dtSIPSubPart As New DataTable
            Dim dtSIPSubPart2 As New DataTable
            Dim dtPart61SubPart As New DataTable
            Dim dtPart61SubPart2 As New DataTable
            Dim dtPart60SubPart As New DataTable
            Dim dtPart60SubPart2 As New DataTable
            Dim dtPart63SubPart As New DataTable
            Dim dtPart63SubPart2 As New DataTable
            Dim dtSSCPStaff As New DataTable
            Dim dtSSCPStaff2 As New DataTable
            Dim dtSSCPUnit As New DataTable
            Dim dtSSCPUnit2 As New DataTable

            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dtCounty.Columns.Add("strCountyCode", GetType(System.String))
            dtCounty.Columns.Add("strCountyName", GetType(System.String))

            dtCounty2.Columns.Add("strCountyCode", GetType(System.String))
            dtCounty2.Columns.Add("strCountyName", GetType(System.String))

            dtDistrict.Columns.Add("strDistrictCode", GetType(System.String))
            dtDistrict.Columns.Add("strDistrictName", GetType(System.String))

            dtDistrict2.Columns.Add("strDistrictCode", GetType(System.String))
            dtDistrict2.Columns.Add("strDistrictName", GetType(System.String))

            dtSIPSubPart.Columns.Add("strSubPart", GetType(System.String))
            dtSIPSubPart.Columns.Add("SubPartCode", GetType(System.String))
            dtSIPSubPart2.Columns.Add("strSubPart", GetType(System.String))
            dtSIPSubPart2.Columns.Add("SubPartCode", GetType(System.String))

            dtPart61SubPart.Columns.Add("strSubPart", GetType(System.String))
            dtPart61SubPart.Columns.Add("SubPartCode", GetType(System.String))
            dtPart61SubPart2.Columns.Add("strSubPart", GetType(System.String))
            dtPart61SubPart2.Columns.Add("SubPartCode", GetType(System.String))

            dtPart60SubPart.Columns.Add("strSubPart", GetType(System.String))
            dtPart60SubPart.Columns.Add("SubPartCode", GetType(System.String))
            dtPart60SubPart2.Columns.Add("strSubPart", GetType(System.String))
            dtPart60SubPart2.Columns.Add("SubPartCode", GetType(System.String))

            dtPart63SubPart.Columns.Add("strSubPart", GetType(System.String))
            dtPart63SubPart.Columns.Add("SubPartCode", GetType(System.String))
            dtPart63SubPart2.Columns.Add("strSubPart", GetType(System.String))
            dtPart63SubPart2.Columns.Add("SubPartCode", GetType(System.String))

            dtSSCPStaff.Columns.Add("numUserID", GetType(System.String))
            dtSSCPStaff.Columns.Add("Staff", GetType(System.String))
            dtSSCPStaff2.Columns.Add("numUserID", GetType(System.String))
            dtSSCPStaff2.Columns.Add("Staff", GetType(System.String))

            dtSSCPUnit.Columns.Add("numUnitCode", GetType(System.String))
            dtSSCPUnit.Columns.Add("strUnitDesc", GetType(System.String))
            dtSSCPUnit2.Columns.Add("numUnitCode", GetType(System.String))
            dtSSCPUnit2.Columns.Add("strUnitDesc", GetType(System.String))

            drNewRow = dtCounty.NewRow()
            drNewRow("strCountyCode") = " "
            drNewRow("strCountyName") = " "
            dtCounty.Rows.Add(drNewRow)

            drNewRow = dtCounty2.NewRow()
            drNewRow("strCountyCode") = " "
            drNewRow("strCountyName") = " "
            dtCounty2.Rows.Add(drNewRow)

            drNewRow = dtDistrict.NewRow()
            drNewRow("strDistrictCode") = " "
            drNewRow("strDistrictName") = " "
            dtDistrict.Rows.Add(drNewRow)

            drNewRow = dtDistrict2.NewRow()
            drNewRow("strDistrictCode") = " "
            drNewRow("strDistrictName") = " "
            dtDistrict2.Rows.Add(drNewRow)

            drNewRow = dtSIPSubPart.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtSIPSubPart.Rows.Add(drNewRow)

            drNewRow = dtSIPSubPart2.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtSIPSubPart2.Rows.Add(drNewRow)

            drNewRow = dtPart61SubPart.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtPart61SubPart.Rows.Add(drNewRow)

            drNewRow = dtPart61SubPart2.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtPart61SubPart2.Rows.Add(drNewRow)

            drNewRow = dtPart60SubPart.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtPart60SubPart.Rows.Add(drNewRow)

            drNewRow = dtPart60SubPart2.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtPart60SubPart2.Rows.Add(drNewRow)

            drNewRow = dtPart63SubPart.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtPart63SubPart.Rows.Add(drNewRow)

            drNewRow = dtPart63SubPart2.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("SubPartCode") = " "
            dtPart63SubPart2.Rows.Add(drNewRow)

            drNewRow = dtSSCPStaff.NewRow()
            drNewRow("numUserID") = " "
            drNewRow("Staff") = " "
            dtSSCPStaff.Rows.Add(drNewRow)

            drNewRow = dtSSCPStaff2.NewRow()
            drNewRow("numUserID") = " "
            drNewRow("Staff") = " "
            dtSSCPStaff2.Rows.Add(drNewRow)

            drNewRow = dtSSCPUnit.NewRow()
            drNewRow("numUnitCode") = " "
            drNewRow("strUnitDesc") = " "
            dtSSCPUnit.Rows.Add(drNewRow)

            drNewRow = dtSSCPUnit2.NewRow()
            drNewRow("numUnitCode") = " "
            drNewRow("strUnitDesc") = " "
            dtSSCPUnit2.Rows.Add(drNewRow)

            For Each drDSRow In dsCounty.Tables("County").Rows()
                drNewRow = dtCounty.NewRow()
                drNewRow("strCountyCode") = drDSRow("strCountyCode")
                drNewRow("strCountyName") = drDSRow("strCountyName")
                dtCounty.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsCounty.Tables("County").Rows()
                drNewRow = dtCounty2.NewRow()
                drNewRow("strCountyCode") = drDSRow("strCountyCode")
                drNewRow("strCountyName") = drDSRow("strCountyName")
                dtCounty2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsDistrict.Tables("District").Rows()
                drNewRow = dtDistrict.NewRow()
                drNewRow("strDistrictCode") = drDSRow("strDistrictCode")
                drNewRow("strDistrictName") = drDSRow("strDistrictName")
                dtDistrict.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsDistrict.Tables("District").Rows()
                drNewRow = dtDistrict2.NewRow()
                drNewRow("strDistrictCode") = drDSRow("strDistrictCode")
                drNewRow("strDistrictName") = drDSRow("strDistrictName")
                dtDistrict2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsSIPSubPart.Tables("SIPSubPart").Rows()
                drNewRow = dtSIPSubPart.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtSIPSubPart.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsSIPSubPart.Tables("SIPSubPart").Rows()
                drNewRow = dtSIPSubPart2.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtSIPSubPart2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsPart61SubPart.Tables("Part61SubPart").Rows()
                drNewRow = dtPart61SubPart.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtPart61SubPart.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsPart61SubPart.Tables("Part61SubPart").Rows()
                drNewRow = dtPart61SubPart2.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtPart61SubPart2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsPart60SubPart.Tables("Part60SubPart").Rows()
                drNewRow = dtPart60SubPart.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtPart60SubPart.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsPart60SubPart.Tables("Part60SubPart").Rows()
                drNewRow = dtPart60SubPart2.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtPart60SubPart2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsPart63SubPart.Tables("Part63SubPart").Rows()
                drNewRow = dtPart63SubPart.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtPart63SubPart.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsPart63SubPart.Tables("Part63SubPart").Rows()
                drNewRow = dtPart63SubPart2.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("SubPartCode") = drDSRow("SubPartCode")
                dtPart63SubPart2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsSSCPStaff.Tables("SSCPStaff").Rows()
                drNewRow = dtSSCPStaff.NewRow()
                drNewRow("numUserID") = drDSRow("numUserID")
                drNewRow("Staff") = drDSRow("Staff")
                dtSSCPStaff.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsSSCPStaff.Tables("SSCPStaff").Rows()
                drNewRow = dtSSCPStaff2.NewRow()
                drNewRow("numUserID") = drDSRow("numUserID")
                drNewRow("Staff") = drDSRow("Staff")
                dtSSCPStaff2.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsSSCPUnit.Tables("SSCPUnit").Rows()
                drNewRow = dtSSCPUnit.NewRow()
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")
                dtSSCPUnit.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsSSCPUnit.Tables("SSCPUnit").Rows()
                drNewRow = dtSSCPUnit2.NewRow()
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")
                dtSSCPUnit2.Rows.Add(drNewRow)
            Next

            With cboCountySearch1
                .DataSource = dtCounty
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                If cboCountySearch1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboCountySearch1.Visible = True

            With cboCountySearch2
                .DataSource = dtCounty2
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                If cboCountySearch2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboCountySearch2.Visible = True

            With cboDistrictSearch1
                .DataSource = dtDistrict
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictCode"
                If cboDistrictSearch1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboDistrictSearch1.Visible = True

            With cboDistrictSearch2
                .DataSource = dtDistrict2
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictCode"
                If cboDistrictSearch2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboDistrictSearch2.Visible = True

            With cboSIPSearch1
                .DataSource = dtSIPSubPart
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboSIPSearch1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboSIPSearch1.Visible = True

            With cboSIPSearch2
                .DataSource = dtSIPSubPart2
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboSIPSearch2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboSIPSearch2.Visible = True

            With cboPart61Search1
                .DataSource = dtPart61SubPart
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboPart61Search1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboPart61Search1.Visible = True

            With cboPart61Search2
                .DataSource = dtPart61SubPart2
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboPart61Search2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboPart61Search2.Visible = True

            With cboPart60Search1
                .DataSource = dtPart60SubPart
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboPart60Search1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboPart60Search1.Visible = True

            With cboPart60Search2
                .DataSource = dtPart60SubPart2
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboPart60Search2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboPart60Search2.Visible = True

            With cboPart63Search1
                .DataSource = dtPart63SubPart
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboPart63Search1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With
            cboPart63Search1.Visible = True

            With cboPart63Search2
                .DataSource = dtPart63SubPart2
                .DisplayMember = "strSubPart"
                .ValueMember = "SubPartCode"
                If cboPart63Search2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With

            With cboSSCPEngineerSearch1
                .DataSource = dtSSCPStaff
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
                If cboSSCPEngineerSearch1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With

            With cboSSCPEngineerSearch2
                .DataSource = dtSSCPStaff2
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
                If cboSSCPEngineerSearch2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With

            With cboSSCPUnitSearch1
                .DataSource = dtSSCPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                If cboSSCPUnitSearch1.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With

            With cboSSCPUnitSearch2
                .DataSource = dtSSCPUnit2
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                If cboSSCPUnitSearch2.SelectedIndex > -1 Then
                    .SelectedIndex = 0
                End If
            End With

            cboPart63Search2.Visible = True

            cboOperationStatusSearch1.Text = " "
            cboOperationStatusSearch1.Items.Add(" ")
            cboOperationStatusSearch1.Items.Add("O - Operational")
            cboOperationStatusSearch1.Items.Add("P - Planned")
            cboOperationStatusSearch1.Items.Add("C - Under Construction")
            cboOperationStatusSearch1.Items.Add("T - Temporarily Closed")
            cboOperationStatusSearch1.Items.Add("X - Closed/Dismantled")
            cboOperationStatusSearch1.Items.Add("I - Seasonal Operation")
            cboOperationStatusSearch1.Visible = True

            cboOperationStatusSearch2.Text = " "
            cboOperationStatusSearch2.Items.Add(" ")
            cboOperationStatusSearch2.Items.Add("O - Operational")
            cboOperationStatusSearch2.Items.Add("P - Planned")
            cboOperationStatusSearch2.Items.Add("C - Under Construction")
            cboOperationStatusSearch2.Items.Add("T - Temporarily Closed")
            cboOperationStatusSearch2.Items.Add("X - Closed/Dismantled")
            cboOperationStatusSearch2.Items.Add("I - Seasonal Operation")
            cboOperationStatusSearch2.Visible = True

            cboClassificationSearch1.Text = " "
            cboClassificationSearch1.Items.Add(" ")
            cboClassificationSearch1.Items.Add("A")
            cboClassificationSearch1.Items.Add("B")
            cboClassificationSearch1.Items.Add("SM")
            cboClassificationSearch1.Items.Add("PR")
            cboClassificationSearch1.Items.Add("C")
            cboClassificationSearch1.Visible = True

            cboClassificationSearch2.Text = " "
            cboClassificationSearch2.Items.Add(" ")
            cboClassificationSearch2.Items.Add("A")
            cboClassificationSearch2.Items.Add("B")
            cboClassificationSearch2.Items.Add("SM")
            cboClassificationSearch2.Items.Add("PR")
            cboClassificationSearch2.Items.Add("C")
            cboClassificationSearch2.Visible = True

            cboCMSUniverseSearch1.Text = " "
            cboCMSUniverseSearch1.Items.Add(" ")
            cboCMSUniverseSearch1.Items.Add("A")
            cboCMSUniverseSearch1.Items.Add("S")
            'cboCMSUniverseSearch1.Items.Add("A & S")
            cboCMSUniverseSearch1.Visible = True

            cboCMSUniverseSearch2.Text = " "
            cboCMSUniverseSearch2.Items.Add(" ")
            cboCMSUniverseSearch2.Items.Add("A")
            cboCMSUniverseSearch2.Items.Add("S")
            'cboCMSUniverseSearch2.Items.Add("A & S")
            cboCMSUniverseSearch2.Visible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".LoadComboBoxes")
        Finally

        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub GenerateSQL()
        Try
            Dim SQLSelect As String = ""
            Dim SQLFrom As String = ""
            Dim SQLWhere As String = ""
            Dim SQLWhereCase1 As String = ""
            Dim SQLWhereCase2 As String = ""
            Dim SQLOrder As String = ""

            Dim temp As String = ""
            Dim temp2 As String = ""
            Dim temp3 As String = ""
            Dim temp4 As String = ""

            If txtFacilityAIRSNumberOrder.Text <> "" Or txtFacilityNameOrder.Text <> "" Or _
            txtFacilityStreet1Order.Text <> "" Or txtFacilityStreet2Order.Text <> "" Or _
            txtFacilityCityOrder.Text <> "" Or txtFacilityZipCodeOrder.Text <> "" Or _
            txtFacilityLongitudeOrder.Text <> "" Or txtFacilityLatitudeOrder.Text <> "" Or _
            txtCountyOrder.Text <> "" Or txtDistrictOrder.Text <> "" Or _
            txtOperationStatusOrder.Text <> "" Or txtClassificationOrder.Text <> "" Or _
            txtSICCodeOrder.Text <> "" Or _
            txtStartUpDateOrder.Text <> "" Or txtShutDownDateOrder.Text <> "" Or _
            txtCMSUniverseOrder.Text <> "" Or txtPlantDescriptionOrder.Text <> "" Or _
            txtAPC0Order.Text <> "" Or txtAPC1Order.Text <> "" Or _
            txtAPC3Order.Text <> "" Or txtAPC4Order.Text <> "" Or _
            txtAPC6Order.Text <> "" Or txtAPC7Order.Text <> "" Or _
            txtAPC8Order.Text <> "" Or txtAPC9Order.Text <> "" Or _
            txtAPCAOrder.Text <> "" Or txtAPCFOrder.Text <> "" Or _
            txtAPCIOrder.Text <> "" Or txtAPCMOrder.Text <> "" Or _
            txtAPCVOrder.Text <> "" Then

                If chbAIRSNumber.Checked = True And txtFacilityAIRSNumberOrder.Text <> "0" Then
                    temp = temp & "-" & txtFacilityAIRSNumberOrder.Text & "-" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber#"
                End If
                If Me.chbFacilityName.Checked = True And txtFacilityNameOrder.Text <> "0" Then
                    temp = temp & "-" & txtFacilityNameOrder.Text & "-strFacilityName#"
                End If
                If Me.chbFacilityStreet1.Checked = True And txtFacilityStreet1Order.Text <> "0" Then
                    temp = temp & "-" & txtFacilityStreet1Order.Text & "-strFacilityStreet1#"
                End If
                If Me.chbFacilityStreet2.Checked = True And txtFacilityStreet2Order.Text <> "0" Then
                    temp = temp & "-" & txtFacilityStreet2Order.Text & "-strFacilityStreet2#"
                End If
                If Me.chbFacilityCity.Checked = True And txtFacilityCityOrder.Text <> "0" Then
                    temp = temp & "-" & txtFacilityCityOrder.Text & "-strFacilityCity#"
                End If
                If Me.chbFacilityZipCode.Checked = True And txtFacilityZipCodeOrder.Text <> "0" Then
                    temp = temp & "-" & txtFacilityZipCodeOrder.Text & "-strFacilityZipCode#"
                End If
                If Me.chbFacilityLatitude.Checked = True And txtFacilityLatitudeOrder.Text <> "0" Then
                    temp = temp & "-" & txtFacilityLatitudeOrder.Text & "-numFacilityLatitude#"
                End If
                If Me.chbFacilityLongitude.Checked = True And txtFacilityLongitudeOrder.Text <> "0" Then
                    temp = temp & "-" & txtFacilityLongitudeOrder.Text & "-numFacilityLongitude#"
                End If
                If Me.chbCounty.Checked = True And txtCountyOrder.Text <> "0" Then
                    temp = temp & "-" & txtCountyOrder.Text & "-strCountyName#"
                End If
                If Me.chbDistrict.Checked = True And txtDistrictOrder.Text <> "0" Then
                    temp = temp & "-" & txtDistrictOrder.Text & "-strDistrictName#"
                End If
                If Me.chbOperationStatus.Checked = True And txtOperationStatusOrder.Text <> "0" Then
                    temp = temp & "-" & txtOperationStatusOrder.Text & "-strOperationalStatus#"
                End If
                If Me.chbClassification.Checked = True And txtClassificationOrder.Text <> "0" Then
                    temp = temp & "-" & txtClassificationOrder.Text & "-strClass#"
                End If
                If Me.chbSICCode.Checked = True And txtSICCodeOrder.Text <> "0" Then
                    temp = temp & "-" & txtSICCodeOrder.Text & "-strSICCode#"
                End If
                If Me.chbStartUpDate.Checked = True And txtStartUpDateOrder.Text <> "0" Then
                    temp = temp & "-" & txtStartUpDateOrder.Text & "-datStartUpDate#"
                End If
                If Me.chbShutDownDate.Checked = True And txtShutDownDateOrder.Text <> "0" Then
                    temp = temp & "-" & txtShutDownDateOrder.Text & "-datShutDownDate#"
                End If
                If chbCMSUniverse.Checked = True And txtCMSUniverseOrder.Text <> "0" Then
                    temp = temp & "-" & txtCMSUniverseOrder.Text & "-strCMSMember#"
                End If
                If chbPlantDescription.Checked = True And txtPlantDescriptionOrder.Text <> "0" Then
                    temp = temp & "-" & txtPlantDescriptionOrder.Text & "-strPlantDescription#"
                End If
                If chbAPC0.Checked = True And txtAPC0Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC0Order.Text & "-APC0#"
                End If
                If chbAPC1.Checked = True And txtAPC1Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC1Order.Text & "-APC1#"
                End If
                If chbAPC3.Checked = True And txtAPC3Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC3Order.Text & "-APC3#"
                End If
                If chbAPC4.Checked = True And txtAPC4Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC4Order.Text & "-APC4#"
                End If
                If chbAPC6.Checked = True And txtAPC6Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC6Order.Text & "-APC6#"
                End If
                If chbAPC7.Checked = True And txtAPC7Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC7Order.Text & "-APC7#"
                End If
                If chbAPC8.Checked = True And txtAPC8Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC8Order.Text & "-APC8#"
                End If
                If chbAPC9.Checked = True And txtAPC9Order.Text <> "0" Then
                    temp = temp & "-" & txtAPC9Order.Text & "-APC9#"
                End If
                If chbAPCA.Checked = True And txtAPCAOrder.Text <> "0" Then
                    temp = temp & "-" & txtAPCAOrder.Text & "-APCA#"
                End If
                If chbAPCF.Checked = True And txtAPCFOrder.Text <> "0" Then
                    temp = temp & "-" & txtAPCFOrder.Text & "-APCF#"
                End If
                If chbAPCI.Checked = True And txtAPCIOrder.Text <> "0" Then
                    temp = temp & "-" & txtAPCIOrder.Text & "-APCI#"
                End If
                If chbAPCM.Checked = True And txtAPCMOrder.Text <> "0" Then
                    temp = temp & "-" & txtAPCMOrder.Text & "-APCM#"
                End If
                If chbAPCV.Checked = True And txtAPCVOrder.Text <> "0" Then
                    temp = temp & "-" & txtAPCVOrder.Text & "-APCV#"
                End If

                SQLOrder = " Order by "

                If temp.IndexOf("1-") <> -1 Then
                    temp2 = temp.IndexOf("1-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("2-") <> -1 Then
                    temp2 = temp.IndexOf("2-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("3-") <> -1 Then
                    temp2 = temp.IndexOf("3-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("4-") <> -1 Then
                    temp2 = temp.IndexOf("4-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("5-") <> -1 Then
                    temp2 = temp.IndexOf("5-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("6-") <> -1 Then
                    temp2 = temp.IndexOf("6-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("7-") <> -1 Then
                    temp2 = temp.IndexOf("7-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("8-") <> -1 Then
                    temp2 = temp.IndexOf("8-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("9-") <> -1 Then
                    temp2 = temp.IndexOf("9-") + 2
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("10-") <> -1 Then
                    temp2 = temp.IndexOf("10-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("11-") <> -1 Then
                    temp2 = temp.IndexOf("11-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("12-") <> -1 Then
                    temp2 = temp.IndexOf("12-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("13-") <> -1 Then
                    temp2 = temp.IndexOf("13-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("14-") <> -1 Then
                    temp2 = temp.IndexOf("14-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("15-") <> -1 Then
                    temp2 = temp.IndexOf("15-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("16-") <> -1 Then
                    temp2 = temp.IndexOf("16-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("17-") <> -1 Then
                    temp2 = temp.IndexOf("17-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
                If temp.IndexOf("18-") <> -1 Then
                    temp2 = temp.IndexOf("18-") + 3
                    temp3 = Mid(temp, (temp2 + 1)).IndexOf("#")
                    temp4 = Mid(temp, (temp2 + 1), temp3)
                    SQLOrder = SQLOrder & temp4 & ", "
                End If
            Else
                SQLOrder = ""
            End If

            If SQLOrder = " Order by " Then
                SQLOrder = ""
            Else
              '  SQLOrder = SQLOrder
            End If

            If chbAIRSNumber.Checked = True Then
                SQLSelect = "Select substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as strAIRSNumber, "
                SQLFrom = " From " & DBNameSpace & ".APBMasterAIRS, " & DBNameSpace & ".APBFacilityInformation, "
                SQLWhere = " Where " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                If rdbAIRSNumberOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbAIRSNumberEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtAIRSNumberSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & SQLWhereCase2 & " '%" & Replace(txtAIRSNumberSearch1.Text, "'", "''") & "%') "
                End If
                If txtAIRSNumberSearch2.Text <> "" Then
                    If txtAIRSNumberSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber " & SQLWhereCase2 & " '%" & Replace(txtAIRSNumberSearch2.Text, "'", "''") & "%') "
                    Else
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & SQLWhereCase2 & " '%" & Replace(txtAIRSNumberSearch2.Text, "'", "''") & "%' "
                    End If
                End If
                If SQLOrder = "" Then
                    SQLOrder = " Order by " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, "
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityName.Checked = True Then
                SQLSelect = SQLSelect & " strFacilityName, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
        '            SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If rdbFacilityNameOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityNameEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtFacilityNameSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strFacilityname) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityNameSearch1.Text, "'", "''") & "%')) "
                End If
                If txtFacilityNameSearch2.Text <> "" Then
                    If txtFacilityNameSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strFacilityname) " & SQLWhereCase2 & " UPPER('%" & Replace(txtFacilityNameSearch2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strFacilityname) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityNameSearch2.Text, "'", "''") & "%') "
                    End If

                End If
                If SQLOrder = " Order by " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, " Then
                    SQLOrder = SQLOrder & " strFacilityName, "
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityStreet1.Checked = True Then
                SQLSelect = SQLSelect & " strFacilityStreet1, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    '         SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If rdbFacilityStreet1Or.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityStreet1Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtFacilityStreet1Search1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strFacilityStreet1) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityStreet1Search1.Text, "'", "''") & "%')) "
                End If
                If txtFacilityStreet1Search2.Text <> "" Then
                    If txtFacilityStreet1Search1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strFacilityStreet1) " & SQLWhereCase2 & " UPPER('%" & Replace(txtFacilityStreet1Search2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strFacilityStreet1) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityStreet1Search2.Text, "'", "''") & "%') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityStreet2.Checked = True Then
                SQLSelect = SQLSelect & " case  " & _
                "when strFacilityStreet2 is Null then ' ' " & _
                "when strFacilityStreet2 = 'N/A' then ' ' " & _
                "when strFacilityStreet2 is Not Null then strFacilityStreet2 " & _
                "end strFacilityStreet2, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    '       SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If rdbFacilityStreet2Or.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityStreet2Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtFacilityStreet2Search1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strFacilityStreet2) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityStreet2Search1.Text, "'", "''") & "%')) "
                End If
                If txtFacilityStreet2Search2.Text <> "" Then
                    If txtFacilityStreet1Search1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strFacilityStreet2) " & SQLWhereCase2 & " UPPER('%" & Replace(txtFacilityStreet2Search2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strFacilityStreet2) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityStreet2Search2.Text, "'", "''") & "%') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityCity.Checked = True Then
                SQLSelect = SQLSelect & " strFacilityCity, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    '     SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If rdbFacilityCityOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityCityEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtFacilityCitySearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strFacilityCity) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityCitySearch1.Text, "'", "''") & "%')) "
                End If
                If txtFacilityCitySearch2.Text <> "" Then
                    If txtFacilityCitySearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strFacilitycity) " & SQLWhereCase2 & " UPPER('%" & Replace(txtFacilityCitySearch2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strFacilitycity) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityCitySearch2.Text, "'", "''") & "%') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityZipCode.Checked = True Then
                SQLSelect = SQLSelect & " case  " & _
                "when strFacilityZipCode is Null then ' ' " & _
                "when strFacilityZipCode = '00000' then ' ' " & _
                "when strFacilityZipCode is Not Null then strFacilityZipCode " & _
                "end strFacilityZipCode, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If rdbFacilityZipCodeOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityZipCodeEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtFacilityZipCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strFacilityZipCode) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityZipCodeSearch1.Text, "'", "''") & "%')) "
                End If
                If txtFacilityZipCodeSearch2.Text <> "" Then
                    If txtFacilityZipCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strFacilityZipCode) " & SQLWhereCase2 & " UPPER('%" & Replace(txtFacilityZipCodeSearch2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strFacilityZipCode) " & SQLWhereCase2 & " Upper('%" & Replace(txtFacilityZipCodeSearch2.Text, "'", "''") & "%') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityLatitude.Checked = True Then
                SQLSelect = SQLSelect & " numFacilityLatitude, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                  '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If txtFacilityLatitudeSearch1.Text <> "" Then
                    If txtFacilityLatitudeSearch2.Text <> "" Then
                        SQLWhere = SQLWhere & " and numFacilityLatitude between to_number('" & Replace(txtFacilityLatitudeSearch1.Text, ".", "") & "') and to_Number('" & Replace(txtFacilityLatitudeSearch2.Text, ".", "") & "') "
                    Else
                        SQLWhere = SQLWhere & " and numFacilityLatitude = to_number('" & Replace(txtFacilityLatitudeSearch1.Text, ".", "") & "') "
                    End If
                Else
                    If txtFacilityLatitudeSearch2.Text <> "" Then
                        SQLWhere = SQLWhere & " and numFacilityLatitude = to_number('" & Replace(txtFacilityLatitudeSearch2.Text, ".", "") & "') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbFacilityLongitude.Checked = True Then
                SQLSelect = SQLSelect & " numFacilityLongitude, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                End If

                If txtFacilityLongitudeSearch1.Text <> "" Then
                    If txtFacilityLongitudeSearch2.Text <> "" Then
                        SQLWhere = SQLWhere & " and numFacilityLongitude between to_number('" & Replace(txtFacilityLongitudeSearch1.Text, ".", "") & "') and to_Number('" & Replace(txtFacilityLongitudeSearch2.Text, ".", "") & "') "
                    Else
                        SQLWhere = SQLWhere & " and numFacilityLongitude = to_number('" & Replace(txtFacilityLongitudeSearch1.Text, ".", "") & "') "
                    End If
                Else
                    If txtFacilityLongitudeSearch2.Text <> "" Then
                        SQLWhere = SQLWhere & " and numFacilityLongitude = to_number('" & Replace(txtFacilityLongitudeSearch2.Text, ".", "") & "') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbCounty.Checked = True Then
                SQLSelect = SQLSelect & " strCountyName, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".LookUpCountyInformation") <> -1 Then
                    'SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".LookUpCountyInformation, "
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode "
                End If

                If rdbCountyOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbCountyEqual.Checked = True Then
                    SQLWhereCase2 = " = "
                Else
                    SQLWhereCase2 = " <> "
                End If

                If cboCountySearch1.SelectedIndex <> -1 And cboCountySearch1.SelectedIndex <> 0 Then
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " '" & cboCountySearch1.SelectedValue & "') "
                End If
                If cboCountySearch2.SelectedIndex <> -1 And cboCountySearch2.SelectedIndex <> 0 Then
                    If cboCountySearch1.SelectedIndex <> -1 And cboCountySearch1.SelectedIndex <> 0 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " '" & cboCountySearch2.SelectedValue & "') "
                    Else
                        SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " '" & cboCountySearch2.SelectedValue & "') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbDistrict.Checked = True Then
                SQLSelect = SQLSelect & " strDistrictName, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".LookUPDistricts") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LookUpDistrictInformation, "
                    SQLWhere = SQLWhere & "and " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode " & _
                            "and " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCounty = substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) "
                End If

                If rdbDistrictOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbDistrictEqual.Checked = True Then
                    SQLWhereCase2 = " = "
                Else
                    SQLWhereCase2 = " <> "
                End If

                If cboDistrictSearch1.SelectedIndex <> -1 And cboDistrictSearch1.SelectedIndex <> 0 Then
                    SQLWhere = SQLWhere & " and (" & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " '" & cboDistrictSearch1.SelectedValue & "') "
                End If
                If cboDistrictSearch2.SelectedIndex <> -1 And cboDistrictSearch2.SelectedIndex <> 0 Then
                    If cboDistrictSearch1.SelectedIndex <> -1 And cboDistrictSearch1.SelectedIndex <> 0 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " '" & cboDistrictSearch2.SelectedValue & "') "
                    Else
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " '" & cboDistrictSearch2.SelectedValue & "') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbOperationStatus.Checked = True Then
                SQLSelect = SQLSelect & " strOperationalStatus, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If

                If rdbOperationalStatusOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbOperationStatusEqual.Checked = True Then
                    SQLWhereCase2 = " = "
                Else
                    SQLWhereCase2 = " <> "
                End If

                If cboOperationStatusSearch1.Text <> " " And cboOperationStatusSearch1.Items.Contains(cboOperationStatusSearch1.Text) Then
                    SQLWhere = SQLWhere & " and (strOperationalStatus " & SQLWhereCase2 & " '" & Mid(cboOperationStatusSearch1.Text, 1, 1) & "') "
                End If
                If cboOperationStatusSearch2.Text <> " " And cboOperationStatusSearch2.Items.Contains(cboOperationStatusSearch2.Text) Then
                    If cboOperationStatusSearch1.Text <> " " And cboOperationStatusSearch1.Items.Contains(cboOperationStatusSearch1.Text) Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " strOperationalStatus " & SQLWhereCase2 & " '" & Mid(cboOperationStatusSearch2.Text, 1, 1) & "') "
                    Else
                        SQLWhere = SQLWhere & " and strOperationalStatus " & SQLWhereCase2 & " '" & Mid(cboOperationStatusSearch2.Text, 1, 1) & "' "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbClassification.Checked = True Then
                SQLSelect = SQLSelect & " strClass, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If

                If rdbClassificationOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbClassificationEqual.Checked = True Then
                    SQLWhereCase2 = " = "
                Else
                    SQLWhereCase2 = " <> "
                End If

                If cboClassificationSearch1.Text <> " " And cboClassificationSearch1.Items.Contains(cboClassificationSearch1.Text) Then
                    SQLWhere = SQLWhere & " and (strClass " & SQLWhereCase2 & " '" & cboClassificationSearch1.Text & "') "
                End If
                If cboClassificationSearch2.Text <> " " And cboClassificationSearch2.Items.Contains(cboClassificationSearch2.Text) Then
                    If cboClassificationSearch1.Text <> " " And cboClassificationSearch1.Items.Contains(cboClassificationSearch1.Text) Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " strClass " & SQLWhereCase2 & " '" & cboClassificationSearch2.Text & "') "
                    Else
                        SQLWhere = SQLWhere & " and strClass " & SQLWhereCase2 & " '" & cboClassificationSearch2.Text & "' "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbSICCode.Checked = True Then
                SQLSelect = SQLSelect & " strSICCode, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If

                If rdbSICCodeOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSICCodeEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtSICCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strSICCode) " & SQLWhereCase2 & " Upper('" & Replace(txtSICCodeSearch1.Text, "'", "''") & "%')) "
                End If
                If txtSICCodeSearch2.Text <> "" Then
                    If txtFacilityCitySearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strSICCode) " & SQLWhereCase2 & " UPPER('" & Replace(txtSICCodeSearch2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strSICCode) " & SQLWhereCase2 & " Upper('" & Replace(txtSICCodeSearch2.Text, "'", "''") & "%') "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbStartUpDate.Checked = True Then
                SQLSelect = SQLSelect & " case  " & _
                "when datStartUpDate is Null then ' ' " & _
                "when datStartUpDate is Not Null then to_char(datStartUpDate, 'RRRR-MM-dd') " & _
                "end datStartUpDate, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If

                'If rdbStartUpDateOr.Checked = True Then
                '    SQLWhereCase1 = " OR "
                'End If
                'If rdbStartUpDateAnd.Checked = True Then
                '    SQLWhereCase1 = " AND "
                'End If
                If rdbStartUpDateBetween.Checked = True Then
                    SQLWhereCase1 = " Between "
                End If



                Select Case SQLWhereCase1
                    Case " Between "
                        If DTPStartUpDateSearch1.Checked = True Then
                            SQLWhere = SQLWhere & " and (datStartUpDate " & SQLWhereCase1 & " '" & DTPStartUpDateSearch1.Text & "' and '" & DTPStartUpDateSearch2.Text & "' ) "
                        End If
                    Case Else
                        If DTPStartUpDateSearch1.Checked = True Then
                            SQLWhere = SQLWhere & " and (datStartUpDate " & SQLWhereCase2 & " '" & DTPStartUpDateSearch1.Text & "') "
                        End If

                        If DTPStartUpDateSearch2.Checked = True Then
                            If DTPStartUpDateSearch1.Checked = True Then
                                SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))

                                SQLWhere = SQLWhere & SQLWhereCase1 & " datStartUpDate " & SQLWhereCase2 & " '" & DTPStartUpDateSearch2.Text & "') "
                            Else
                                SQLWhere = SQLWhere & " and datStartUpDate " & SQLWhereCase2 & " '" & DTPStartUpDateSearch2.Text & "') "
                            End If
                        End If
                End Select
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbShutDownDate.Checked = True Then
                SQLSelect = SQLSelect & " case  " & _
                "when datShutDownDate is Null then ' ' " & _
                "when datShutDownDate is Not Null then to_char(datShutDownDate, 'RRRR-MM-dd') " & _
                "end datShutDownDate, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '       SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If

                'If rdbShutDownDateOr.Checked = True Then
                '    SQLWhereCase1 = " OR "
                'End If
                'If rdbShutDownDateAnd.Checked = True Then
                '    SQLWhereCase1 = " AND "
                'End If
                If rdbShutDownDateBetween.Checked = True Then
                    SQLWhereCase1 = " Between "
                End If

                'If rdbShutDownDateEqual.Checked = True Then
                '    SQLWhereCase2 = " = "
                'Else
                '    SQLWhereCase2 = " <> "
                'End If

                Select Case SQLWhereCase1
                    Case " Between "
                        If DTPShutDownDateSearch1.Checked = True Then
                            SQLWhere = SQLWhere & " and (datShutDownDate " & SQLWhereCase1 & " '" & DTPShutDownDateSearch1.Text & "' and '" & DTPStartUpDateSearch2.Text & "' ) "
                        End If
                    Case Else
                        If DTPShutDownDateSearch1.Checked = True Then
                            SQLWhere = SQLWhere & " and (datShutDownDate " & SQLWhereCase2 & " '" & DTPShutDownDateSearch1.Text & "') "
                        End If

                        If DTPShutDownDateSearch2.Checked = True Then
                            If DTPShutDownDateSearch1.Checked = True Then
                                SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))

                                SQLWhere = SQLWhere & SQLWhereCase1 & " datShutDownDate " & SQLWhereCase2 & " '" & DTPShutDownDateSearch2.Text & "') "
                            Else
                                SQLWhere = SQLWhere & " and datShutDownDate " & SQLWhereCase2 & " '" & DTPShutDownDateSearch2.Text & "') "
                            End If
                        End If
                End Select
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbCMSUniverse.Checked = True Then
                SQLSelect = SQLSelect & " strCMSMember, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSupplamentalData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSupplamentalData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSupplamentalData.strAIRSNumber "
                End If

                If rdbCMSUniverseOR.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbCMSUniverseEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If cboCMSUniverseSearch1.Text <> " " And cboCMSUniverseSearch1.Items.Contains(cboCMSUniverseSearch1.Text) Then
                    SQLWhere = SQLWhere & " and (strCMSMember " & SQLWhereCase2 & " '" & cboCMSUniverseSearch1.Text & "') "
                End If
                If cboCMSUniverseSearch2.Text <> " " And cboCMSUniverseSearch2.Items.Contains(cboCMSUniverseSearch2.Text) Then
                    If cboCMSUniverseSearch1.Text <> " " And cboCMSUniverseSearch1.Items.Contains(cboCMSUniverseSearch1.Text) Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " strCMSMember " & SQLWhereCase2 & " '" & cboCMSUniverseSearch2.Text & "') "
                    Else
                        SQLWhere = SQLWhere & " and strCMSMember " & SQLWhereCase2 & " '" & cboCMSUniverseSearch2.Text & "' "
                    End If
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbPlantDescription.Checked = True Then
                SQLSelect = SQLSelect & " case  " & _
                "when strPlantDescription is Null then ' ' " & _
                "when strPlantDescription ='N/A' then ' ' " & _
                "when strPlantDescription is Not Null then strPlantDescription " & _
                "end strPlantDescription, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '        SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If

                If rdbPlantDescriptionOR.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbPlantDescriptionEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If

                If txtPlantDescriptionSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (UPPER(strPlantDescription) " & SQLWhereCase2 & " Upper('%" & Replace(txtPlantDescriptionSearch1.Text, "'", "''") & "%')) "
                End If
                If txtPlantDescriptionSearch2.Text <> "" Then
                    If txtPlantDescriptionSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                        SQLWhere = SQLWhere & SQLWhereCase1 & " UPPER(strPlantDescription) " & SQLWhereCase2 & " UPPER('%" & Replace(txtPlantDescriptionSearch2.Text, "'", "''") & "%')) "
                    Else
                        SQLWhere = SQLWhere & " and Upper(strPlantDescription) " & SQLWhereCase2 & " Upper('%" & Replace(txtPlantDescriptionSearch2.Text, "'", "''") & "%') "
                    End If
                End If
            End If

            If Me.chbAttainmentStatus.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '1' then '1-Hr Yes' " & _
                            "Else '' end OneHrYes, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '2' then '1-Hr Contribute' " & _
                            "Else '' end OneHrContribute, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '0' then '1-Hr No' " & _
                            "Else '' End OneHrNo, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '1' then '8-Hr Atlanta' " & _
                            "else '' end EightHrAtlanta, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '2' then '8-Hr Macon' " & _
                            "else '' end EightHrMacon, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '0' then '8-Hr No' " & _
                            "else '' end EightHrNo, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '1' then 'PM-2.5 Atlanta' " & _
                            "else '' end PMAtlanta, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '2' then 'PM-2.5 Chattanooga' " & _
                            "else '' end PMChattanooga, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '3' then 'PM-2.5 Floyd' " & _
                            "else '' end PMFloyd, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '4' then 'PM-2.5 Macon' " & _
                            "else '' end PMMacon, " & _
                            "case " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                            "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '0' then 'PM-2.5 No' " & _
                            "else '' end PMNo, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '     SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If
            If chb1HrYes.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '1' then '1-Hr Yes' " & _
                "else '' end OneHrYes, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdb1HrYesEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '1' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) <> '1' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chb1HrContribute.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '2' then '1-Hr Contribute' " & _
                            "else '' end OneHrContribute, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '        SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdb1HrContributeEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '2' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) <> '2' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chb1HrNo.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '0' then '1-Hr No' " & _
                            "else '' end OneHrNo, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '        SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdb1HrNoEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '0' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) <> '0' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chb8HrAtlanta.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '1' then '8-Hr Atlanta' " & _
                            "else '' end EightHrAtlanta, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '       SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdb8HrAtlantaEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '1' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) <> '1' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chb8HrMacon.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '2' then '8-Hr Macon' " & _
                            "else '' end EightHrMacon, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '        SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdb8HrMaconEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '2' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) <> '2' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chb8HrNo.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '0' then '8-Hr No' " & _
                            "else '' end EightHrNo, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '         SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdb8HrNoEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '0' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) <> '0' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chbPMAtlanta.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '1' then 'PM-2.5 Atlanta' " & _
                            "else '' end PMAtlanta, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '         SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbPMAtlantaEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '1' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) <> '1' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chbPMChattanooga.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '2' then 'PM-2.5 Chattanooga' " & _
                            "else '' end PMChattanooga, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '           SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbPMChattanoogaEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '2' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) <> '2' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chbPMFloyd.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '3' then 'PM-2.5 Floyd' " & _
                            "else '' end PMFloyd, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '          SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbPMFloydEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '3' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) <> '3' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chbPMMacon.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '4' then 'PM-2.5 Macon' " & _
                            "else '' end PMMacon, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '          SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbPMMaconEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '4' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) <> '4' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If
            If chbPMNo.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '0' then 'PM-2.5 No' " & _
                            "else '' end PMNo, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
          '          SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbPMNoEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '0' "
                Else
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) <> '0' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strAttainmentStatus is Null) "
                End If
            End If

            If chbStateProgramCodes.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(strStateProgramCodes, 1, 1) is null then '' " & _
                "when substr(strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major' " & _
                "Else '' end NSRPSD, " & _
                "case " & _
                "when substr(strStateProgramCodes, 2, 1) is Null then '' " & _
                "when substr(strStateProgramCodes, 2, 1) = '1' then 'HAPs Major' " & _
                "Else '' end HAP, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                   ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbNSRPSDMajor.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(strStateProgramCodes, 1, 1) is null then '' " & _
                "when substr(strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major' " & _
                "Else '' end NSRPSD, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbNSRPSDMajorEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(strStateProgramCodes, 1, 1) = '1' "
                Else
                    SQLWhere = SQLWhere & " and (substr(strStateProgramCodes, 1, 1) <> '1' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strStateProgramCodes is Null) "
                End If
            End If

            If chbHAPMajor.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(strStateProgramCodes, 2, 1) is Null then '' " & _
                "when substr(strStateProgramCodes, 2, 1) = '1' then 'HAPs Major' " & _
                "Else '' end HAP, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbHAPMajorEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and substr(strStateProgramCodes, 2, 1) = '1' "
                Else
                    SQLWhere = SQLWhere & " and (substr(strStateProgramCodes, 2, 1) <> '1' " & _
                                          " or " & DBNameSpace & ".APBHeaderData.strStateProgramCodes is Null) "
                End If
            End If

            If chbViewAirPrograms.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 1, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 1, 1) = '1' then '0 - SIP' " & _
                            "Else '' end APC0, " & _
                            "case when substr(strAirProgramCodes, 2, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 2, 1) = '1' then '1 - Federal SIP' " & _
                            "Else '' end APC1, " & _
                            "case when substr(strAirProgramCodes, 3, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 3, 1) = '1' then '3 - Non-Fed' " & _
                            "Else '' end APC3, " & _
                            "case when substr(strAirProgramCodes, 4, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 4, 1) = '1' then '4 - CFC Tracking' " & _
                            "Else '' end APC4, " & _
                            "case when substr(strAirProgramCodes, 5, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 5, 1) = '1' then '6 - PSD' " & _
                            "Else '' end APC6, " & _
                            "case when substr(strAirProgramCodes, 6, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 6, 1) = '1' then '7 - NSR' " & _
                            "Else '' end APC7, " & _
                            "case when substr(strAirProgramCodes, 7, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 7, 1) = '1' then '8 - NESHAP' " & _
                            "Else '' end APC8, " & _
                            "case when substr(strAirProgramCodes, 8, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 8, 1) = '1' then '9 - NSPS' " & _
                            "Else '' end APC9, " & _
                            "case when substr(strAirProgramCodes, 9, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 9, 1) = '1' then 'A - Acid Percipitation' " & _
                            "Else '' end APCA, " & _
                            "case when substr(strAirProgramCodes, 10, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 10, 1) = '1' then 'F - FESHOP' " & _
                            "Else '' end APCF, " & _
                            "case when substr(strAirProgramCodes, 11, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 11, 1) = '1' then 'I - Native American' " & _
                            "Else '' end APCI, " & _
                            "case when substr(strAirProgramCodes, 12, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 12, 1) = '1' then 'M - MACT' " & _
                            "Else '' end APCM, " & _
                            "case when substr(strAirProgramCodes, 13, 1) = '0' then '' " & _
                            "when substr(strAirProgramCodes, 13, 1) = '1' then 'V - Title V' " & _
                            "Else '' end APCV, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbAPC0.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 1, 1) = '0' then '' when substr(strAirProgramCodes, 1, 1) = '1' then '0 - SIP' Else '' end APC0, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    'SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC0Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '0%' "
                End If
            End If

            If chbAPC1.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 2, 1) = '0' then '' when substr(strAirProgramCodes, 2, 1) = '1' then '1 - Federal SIP' Else '' end APC1, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC1Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_0%' "
                End If
            End If

            If chbAPC3.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 3, 1) = '0' then '' when substr(strAirProgramCodes, 3, 1) = '1' then '3 - Non-Fed' Else '' end APC3, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC3Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '__1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '__0%' "
                End If
            End If

            If chbAPC4.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 4, 1) = '0' then '' when substr(strAirProgramCodes, 4, 1) = '1' then '4 - CFC Tracking' Else '' end APC4, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC4Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '___1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '___0%' "
                End If
            End If

            If chbAPC6.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 5, 1) = '0' then '' when substr(strAirProgramCodes, 5, 1) = '1' then '6 - PSD' Else '' end APC6, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                '    SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC6Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '____1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '____0%' "
                End If
            End If

            If chbAPC7.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 6, 1) = '0' then '' when substr(strAirProgramCodes, 6, 1) = '1' then '7 - NSR' Else '' end APC7, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC7Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_____1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_____0%' "
                End If
            End If

            If chbAPC8.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 7, 1) = '0' then '' when substr(strAirProgramCodes, 7, 1) = '1' then '8 - NESHAP' Else '' end APC8, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC8Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '______1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '______0%' "
                End If
            End If

            If chbAPC9.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 8, 1) = '0' then '' when substr(strAirProgramCodes, 8, 1) = '1' then '9 - NSPS' Else '' end APC9, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPC9Equal.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_______1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_______0%' "
                End If
            End If

            If chbAPCA.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 9, 1) = '0' then '' when substr(strAirProgramCodes, 9, 1) = '1' then 'A - Acid Percipitation' Else '' end APCA, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPCAEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '________1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '________0%' "
                End If
            End If

            If chbAPCF.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 10, 1) = '0' then '' when substr(strAirProgramCodes, 10, 1) = '1' then 'F - FESHOP' Else '' end APCF, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '    SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPCFEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_________1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '_________0%' "
                End If
            End If

            If chbAPCI.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 11, 1) = '0' then '' when substr(strAirProgramCodes, 11, 1) = '1' then 'I - Native American' Else '' end APCI, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '    SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPCIEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '__________1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '__________0%' "
                End If
            End If

            If chbAPCM.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 12, 1) = '0' then '' when substr(strAirProgramCodes, 12, 1) = '1' then 'M - MACT' Else '' end APCM, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPCMEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '___________1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '___________0%' "
                End If
            End If

            If chbAPCV.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strAirProgramCodes, 13, 1) = '0' then '' when substr(strAirProgramCodes, 13, 1) = '1' then 'V - Title V' Else '' end APCV, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
                If rdbAPCVEqual.Checked = True Then
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '____________1%' "
                Else
                    SQLWhere = SQLWhere & " and strAirProgramCodes like '____________0%' "
                End If
            End If

            If chbAllSubparts.Checked = True Then
                SQLSelect = SQLSelect & _
                " case when substr(strSubPartKey, 13, 1) = '0' then strSubPart " & _
                "end GASIP, " & _
                "case " & _
                "when substr(strSubPartKey, 13, 1) = '8' then strSubPart " & _
                "end Part61, " & _
                "case " & _
                "when substr(strSubPartKey, 13, 1) = '9' then strSubPart " & _
                "end Part60, " & _
                "case " & _
                "when substr(strSubPartKey, 13, 1) = 'M' then strSubPart " & _
                "End Part63, "
                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                    " and strSubPart Is not Null and airbranch.apbsubpartdata.active = '1' "
                End If

            Else

            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbSIP.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strSubPartKey, 13, 1) = '1' then strSubPart " & _
                "End GASIP, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                    " and strSubPart is Not Null and airbranch.apbsubpartdata.active = '1' "
                End If
                'If rdbSubPartOr.Checked = True Then
                '    SQLWhereCase1 = " OR "
                'Else
                '    SQLWhereCase1 = " AND "
                'End If
                'If rdbSIPEqual.Checked = True Then
                '    SQLWhereCase2 = " Like "
                'Else
                '    SQLWhereCase2 = " Not Like "
                'End If
                'If txtSIPSearch1.Text <> "" Then
                '    SQLWhere = SQLWhere & " and (strSubPart " & SQLWhereCase2 & " '%" & Replace(txtSIPSearch1.Text, "'", "''") & "%' ) "
                'End If
                'If txtSIPSearch2.Text <> "" Then
                '    If txtSIPSearch1.Text <> "" Then
                '        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                '        SQLWhere = SQLWhere & SQLWhereCase1 & "strSubPart " & SQLWhereCase2 & " '%" & Replace(txtSIPSearch2.Text, "'", "''") & "%') "
                '    Else
                '        SQLWhere = SQLWhere & " and strSubPart " & SQLWhereCase2 & " '%" & Replace(txtSIPSearch2.Text, "'", "''") & "%' "
                '    End If
                'End If
                SQLWhere = SQLWhere & " and (substr(strSubPartKey, 13, 1) = '0' ) "
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbPart61Subpart.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strSubPartKey, 13, 1) = '8' then strSubPart " & _
                "End Part61, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                    " and strSubPart is Not Null and airbranch.apbsubpartdata.active = '1' "
                End If
                'If rdbPart61Or.Checked = True Then
                '    SQLWhereCase1 = " OR "
                'Else
                '    SQLWhereCase1 = " AND "
                'End If
                If rdbPart61Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                'If txtPart61Search1.Text <> "" Then
                '    SQLWhere = SQLWhere & " and (strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart61Search1.Text, "'", "''") & "%' ) "
                'End If
                'If txtPart61Search2.Text <> "" Then
                '    If txtPart61Search1.Text <> "" Then
                '        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                '        SQLWhere = SQLWhere & SQLWhereCase1 & "strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart61Search2.Text, "'", "''") & "%') "
                '    Else
                '        SQLWhere = SQLWhere & " and strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart61Search2.Text, "'", "''") & "%' "
                '    End If
                'End If
                If chbSIP.Checked = True Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                    SQLWhere = SQLWhere & " or (substr(strSubPartKey, 13, 1) = '8' )) "
                Else
                    SQLWhere = SQLWhere & " and (substr(strSubPartKey, 13, 1) = '8' ) "
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbPart60Subpart.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strSubPartKey, 13, 1) = '9' then strSubPart " & _
                "End Part60, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                    " and strSubPart is Not Null and airbranch.apbsubpartdata.active = '1' "
                End If
                'If rdbPart60Or.Checked = True Then
                '    SQLWhereCase1 = " OR "
                'Else
                '    SQLWhereCase1 = " AND "
                'End If
                If rdbPart60Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                'If txtPart60Search1.Text <> "" Then
                '    SQLWhere = SQLWhere & " and (strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart60Search1.Text, "'", "''") & "%' ) "
                'End If
                'If txtPart60Search2.Text <> "" Then
                '    If txtPart60Search1.Text <> "" Then
                '        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                '        SQLWhere = SQLWhere & SQLWhereCase1 & "strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart60Search2.Text, "'", "''") & "%') "
                '    Else
                '        SQLWhere = SQLWhere & " and strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart60Search2.Text, "'", "''") & "%' "
                '    End If
                'End If
                If chbSIP.Checked = True Or chbPart61Subpart.Checked = True Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                    SQLWhere = SQLWhere & " or (substr(strSubPartKey, 13, 1) = '9' )) "
                Else
                    SQLWhere = SQLWhere & " and (substr(strSubPartKey, 13, 1) = '9' ) "
                End If
            End If

            SQLWhereCase1 = ""
            SQLWhereCase2 = ""

            If chbPart63Subpart.Checked = True Then
                SQLSelect = SQLSelect & "case when substr(strSubPartKey, 13, 1) = 'M' then strSubPart " & _
                "End Part63, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                   ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                    " and strSubPart is Not Null and airbranch.apbsubpartdata.active = '1' "
                End If
                'If rdbPart63Or.Checked = True Then
                '    SQLWhereCase1 = " OR "
                'Else
                '    SQLWhereCase1 = " AND "
                'End If
                If rdbPart63Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                'If txtPart63Search1.Text <> "" Then
                '    SQLWhere = SQLWhere & " and (strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart63Search1.Text, "'", "''") & "%' ) "
                'End If
                'If txtPart63Search2.Text <> "" Then
                '    If txtPart63Search1.Text <> "" Then
                '        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                '        SQLWhere = SQLWhere & SQLWhereCase1 & "strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart63Search2.Text, "'", "''") & "%') "
                '    Else
                '        SQLWhere = SQLWhere & " and strSubPart " & SQLWhereCase2 & " '%" & Replace(txtPart63Search2.Text, "'", "''") & "%' "
                '    End If
                'End If
                If chbSIP.Checked = True Or chbPart61Subpart.Checked = True Or chbPart60Subpart.Checked = True Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2))
                    SQLWhere = SQLWhere & " or (substr(strSubPartKey, 13, 1) = 'M' )) "
                Else
                    SQLWhere = SQLWhere & " and (substr(strSubPartKey, 13, 1) = 'M' ) "
                End If
            End If



            SQL = Mid(SQLSelect, 1, (SQLSelect.Length - 2)) & _
            Mid(SQLFrom, 1, (SQLFrom.Length - 2)) & _
            SQLWhere & _
            Mid(SQLOrder, 1, (SQLOrder.Length - 2))

            txtSQLStatement.Text = SQL.ToString()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".GenerateSQL")
        Finally

        End Try

    End Sub
    Sub GenerateSQL2()
        Try
            Dim MasterSQL As String = ""
            Dim SQLSelect As String = ""
            Dim SQLFrom As String = ""
            Dim SQLWhere As String = ""
            Dim SQLOrder As String = ""
            Dim SQLWhereCase1 As String = ""
            Dim SQLWhereCase2 As String = ""
            Dim temp As String = ""
            Dim i As Integer = 0
            Dim j As Integer = 0

            SQLSelect = "Select " & _
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            "" & DBNameSpace & ".APBFacilityInformation.strFacilityName, "

            SQLFrom = " From " & DBNameSpace & ".APBMasterAIRS, " & DBNameSpace & ".APBFacilityInformation, "

            SQLWhere = " Where " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "

            'Adding Select/From to SQL statements
            If chbFacilityStreet1.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityStreet1, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                  '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                End If
            End If
            If chbFacilityStreet2.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityStreet2, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbFacilityCity.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityCity, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbFacilityZipCode.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityZipCode, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbFacilityLatitude.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBFacilityInformation.numFacilityLatitude, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbSSCPEngineer.Checked = True Then
                SQLSelect = SQLSelect & _
                "(strLastName||', '||strFirstName) as SSCPEngineer, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".SSCPInspectionsRequired") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".SSCPInspectionsRequired, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSnumber = " & DBNameSpace & ".SSCPInspectionsRequired.strAIRSNumber "
                End If
                If SQLFrom.IndexOf("" & DBNameSpace & ".EPDUserProflies") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".EPDUserProfiles, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer "
                End If
            End If

            If chbFacilityLongitude.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBFacilityInformation.numFacilityLongitude, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbCounty.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".LookUpCountyInformation.strCountyName, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".LookUpCountyInformation") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".LookUpCountyInformation, "
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBMasterAIRS.strAIRSNumber, 5, 3) = " & _
                    "" & DBNameSpace & ".LookUpCountyInformation.strCountyCode "
                End If
            End If

            If chbDistrict.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".LookUpDistricts.strDistrictName, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".LookUpDistricts") <> -1 Then
                    'SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".LookUpDistricts, " & DBNameSpace & ".LookUpDistrictInformation, "
                    SQLWhere = SQLWhere & " and substr(" & DBNameSpace & ".APBMasterAIRS.strAIRSNumber, 5, 3) = " & _
                    "" & DBNameSpace & ".LookUpDistrictInformation.strDistrictCounty " & _
                    " and " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode = " & DBNameSpace & ".LookUpDistricts.strDistrictCode "
                End If
            End If

            If chbDistrictResponsible.Checked = True Then
                'SQLSelect = SQLSelect & _
                '"AIRBranch.SSCPDistrictResponsible, "

                If SQLFrom.IndexOf("AIRBranch.SSCPDistrictResponsible") <> -1 Then

                Else
                    SQLFrom = SQLFrom & " AIRBranch.SSCPDistrictResponsible, "
                    SQLWhere = SQLWhere & " AND airbranch.APBMasterAIRS.strAIRSnumber = airbranch.SSCPDistrictResponsible.strAIRSNumber "
                    If rdbDistrictResponsibleTrue.Checked = True Then
                        SQLWhere = SQLWhere & " and SSCPDistrictResponsible.strDistrictResponsible = 'True' "
                    Else
                        SQLWhere = SQLWhere & " and SSCPDistrictResponsible.strDistrictResponsible = 'False' "
                    End If
                End If
            End If


            If chbOperationStatus.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.strOperationalStatus, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbClassification.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.strClass, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbSICCode.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.strSICCode, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    'SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbNAICSCode.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.strNAICSCode, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbStartUpDate.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.datStartUpDate, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbShutDownDate.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.datShutDownDate, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbCMSUniverse.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBSupplamentalData.strCMSMember, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSupplamentalData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSupplamentalData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBSupplamentalData.strAIRSNumber "
                End If
            End If

            If chbPlantDescription.Checked = True Then
                SQLSelect = SQLSelect & _
                "" & DBNameSpace & ".APBHeaderData.strPlantDescription, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbAttainmentStatus.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '1' then '1-Hr Yes' " & _
                "Else '' end OneHrYes, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '2' then '1-Hr Contribute' " & _
                "Else '' end OneHrContribute, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '0' then '1-Hr No' " & _
                "Else '' End OneHrNo, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '1' then '8-Hr Atlanta' " & _
                "else '' end EightHrAtlanta, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '2' then '8-Hr Macon' " & _
                "else '' end EightHrMacon, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '0' then '8-Hr No' " & _
                "else '' end EightHrNo, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '1' then 'PM-2.5 Atlanta' " & _
                "else '' end PMAtlanta, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '2' then 'PM-2.5 Chattanooga' " & _
                "else '' end PMChattanooga, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '3' then 'PM-2.5 Floyd' " & _
                "else '' end PMFloyd, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '4' then 'PM-2.5 Macon' " & _
                "else '' end PMMacon, " & _
                "case " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '0' then 'PM-2.5 No' " & _
                "else '' end PMNo, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            Else
                If chb1HrYes.Checked = True Then
                    SQLSelect = SQLSelect & "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '1' then '1-Hr Yes' " & _
                    "Else '' end OneHrYes, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb1HrNo.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '0' then '1-Hr No' " & _
                    "Else '' End OneHrNo, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb1HrContribute.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) = '2' then '1-Hr Contribute' " & _
                    "Else '' end OneHrContribute, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb8HrAtlanta.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '1' then '8-Hr Atlanta' " & _
                    "else '' end EightHrAtlanta, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb8HrMacon.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '2' then '8-Hr Macon' " & _
                    "else '' end EightHrMacon, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb8HrNo.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 3, 1) = '0' then '8-Hr No' " & _
                    "else '' end EightHrNo, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMAtlanta.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '1' then 'PM-2.5 Atlanta' " & _
                    "else '' end PMAtlanta, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMChattanooga.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '2' then 'PM-2.5 Chattanooga' " & _
                    "else '' end PMChattanooga, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMFloyd.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '3' then 'PM-2.5 Floyd' " & _
                    "else '' end PMFloyd, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMMacon.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '4' then 'PM-2.5 Macon' " & _
                    "else '' end PMMacon, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMNo.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 2, 1) is Null then '' " & _
                    "when substr(" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, 4, 1) = '0' then 'PM-2.5 No' " & _
                    "else '' end PMNo, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
            End If
            If chbStateProgramCodes.Checked = True Then
                SQLSelect = SQLSelect & "case " & _
                "when substr(strStateProgramCodes, 1, 1) is null then '' " & _
                "when substr(strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major' " & _
                "Else '' end NSRPSD, " & _
                "case " & _
                "when substr(strStateProgramCodes, 2, 1) is Null then '' " & _
                "when substr(strStateProgramCodes, 2, 1) = '1' then 'HAPs Major' " & _
                "Else '' end HAP, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            Else
                If chbNSRPSDMajor.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strStateProgramCodes, 1, 1) is null then '' " & _
                    "when substr(strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major' " & _
                    "Else '' end NSRPSD, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If

                If chbHAPMajor.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strStateProgramCodes, 2, 1) is Null then '' " & _
                    "when substr(strStateProgramCodes, 2, 1) = '1' then 'HAPs Major' " & _
                    "Else '' end HAP, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
            End If

            If chbViewAirPrograms.Checked = True Then
                SQLSelect = SQLSelect & _
                "case " & _
                "when substr(strAirProgramCodes, 1, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 1, 1) = '1' then '0 - SIP' " & _
                "Else '' end APC0, " & _
                "case when substr(strAirProgramCodes, 2, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 2, 1) = '1' then '1 - Federal SIP' " & _
                "Else '' end APC1, " & _
                "case when substr(strAirProgramCodes, 3, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 3, 1) = '1' then '3 - Non-Fed' " & _
                "Else '' end APC3, " & _
                "case when substr(strAirProgramCodes, 4, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 4, 1) = '1' then '4 - CFC Tracking' " & _
                "Else '' end APC4, " & _
                "case when substr(strAirProgramCodes, 5, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 5, 1) = '1' then '6 - PSD' " & _
                "Else '' end APC6, " & _
                "case when substr(strAirProgramCodes, 6, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 6, 1) = '1' then '7 - NSR' " & _
                "Else '' end APC7, " & _
                "case when substr(strAirProgramCodes, 7, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 7, 1) = '1' then '8 - NESHAP' " & _
                "Else '' end APC8, " & _
                "case when substr(strAirProgramCodes, 8, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 8, 1) = '1' then '9 - NSPS' " & _
                "Else '' end APC9, " & _
                "case when substr(strAirProgramCodes, 9, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 9, 1) = '1' then 'A - Acid Percipitation' " & _
                "Else '' end APCA, " & _
                "case when substr(strAirProgramCodes, 10, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 10, 1) = '1' then 'F - FESHOP' " & _
                "Else '' end APCF, " & _
                "case when substr(strAirProgramCodes, 11, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 11, 1) = '1' then 'I - Native American' " & _
                "Else '' end APCI, " & _
                "case when substr(strAirProgramCodes, 12, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 12, 1) = '1' then 'M - MACT' " & _
                "Else '' end APCM, " & _
                "case when substr(strAirProgramCodes, 13, 1) = '0' then '' " & _
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'V - Title V' " & _
                "Else '' end APCV, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                End If
            Else
                If chbAPC0.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 1, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 1, 1) = '1' then '0 - SIP' " & _
                    "Else '' end APC0, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '    SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC1.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 2, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 2, 1) = '1' then '1 - Federal SIP' " & _
                    "Else '' end APC1, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC3.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 3, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 3, 1) = '1' then '3 - Non-Fed' " & _
                    "Else '' end APC3, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC4.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 4, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 4, 1) = '1' then '4 - CFC Tracking' " & _
                    "Else '' end APC4, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC6.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case when substr(strAirProgramCodes, 5, 1) = '0' then '' " & _
                                    "when substr(strAirProgramCodes, 5, 1) = '1' then '6 - PSD' " & _
                                    "Else '' end APC6, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC7.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 6, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 6, 1) = '1' then '7 - NSR' " & _
                    "Else '' end APC7, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC8.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 7, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 7, 1) = '1' then '8 - NESHAP' " & _
                    "Else '' end APC8, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '      SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC9.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 8, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 8, 1) = '1' then '9 - NSPS' " & _
                    "Else '' end APC9, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '      SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCA.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 9, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 9, 1) = '1' then 'A - Acid Percipitation' " & _
                    "Else '' end APCA, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '         SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCF.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 10, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 10, 1) = '1' then 'F - FESHOP' " & _
                    "Else '' end APCF, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '      SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCI.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 11, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 11, 1) = '1' then 'I - Native American' " & _
                    "Else '' end APCI, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '        SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCM.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 12, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 12, 1) = '1' then 'M - MACT' " & _
                    "Else '' end APCM, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCV.Checked = True Then
                    SQLSelect = SQLSelect & _
                    "case " & _
                    "when substr(strAirProgramCodes, 13, 1) = '0' then '' " & _
                    "when substr(strAirProgramCodes, 13, 1) = '1' then 'V - Title V' " & _
                    "Else '' end APCV, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBHeaderData, "
                        SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber "
                    End If
                End If
            End If

            If chbAllSubparts.Checked = True Then
                SQLSelect = SQLSelect & _
                   " case " & _
                   "when substr(strSubPartKey, 13, 1) = '0' then strSubPart " & _
                   "end GASIP, " & _
                   "case " & _
                   "when substr(strSubPartKey, 13, 1) = '8' then strSubPart " & _
                   "end Part61, " & _
                   "case " & _
                   "when substr(strSubPartKey, 13, 1) = '9' then strSubPart " & _
                   "end Part60, " & _
                   "case " & _
                   "when substr(strSubPartKey, 13, 1) = 'M' then strSubPart " & _
                   "End Part63, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                    SQLWhere = SQLWhere & _
                    " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                    " and strSubPart Is not Null and airbranch.apbsubpartdata.active = '1' "
                End If
            Else
                If chbSIP.Checked = True Then
                    SQLSelect = SQLSelect & _
                    " case " & _
                    "when substr(strSubPartKey, 13, 1) = '0' then strSubPart " & _
                    "end GASIP, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                        '          SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                        SQLWhere = SQLWhere & _
                        " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                        " and strSubPart Is not Null "
                    End If
                End If
                If chbPart61Subpart.Checked = True Then
                    SQLSelect = SQLSelect & _
                    " case " & _
                    "when substr(strSubPartKey, 13, 1) = '8' then strSubPart " & _
                    "end Part61, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                        '         SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                        SQLWhere = SQLWhere & _
                        " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                        " and strSubPart Is not Null and airbranch.apbsubpartdata.active = '1' "
                    End If
                End If
                If chbPart60Subpart.Checked = True Then
                    SQLSelect = SQLSelect & _
                    " case " & _
                    "when substr(strSubPartKey, 13, 1) = '9' then strSubPart " & _
                    "end Part60, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                        '       SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                        SQLWhere = SQLWhere & _
                        " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                        " and strSubPart Is not Null and airbranch.apbsubpartdata.active = '1' "
                    End If
                End If
                If chbPart63Subpart.Checked = True Then
                    SQLSelect = SQLSelect & _
                    " case " & _
                    "when substr(strSubPartKey, 13, 1) = 'M' then strSubPart " & _
                    "end Part63, "

                    If SQLFrom.IndexOf("" & DBNameSpace & ".APBSubPartData") <> -1 Then
                        '       SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " " & DBNameSpace & ".APBSubPartData, "
                        SQLWhere = SQLWhere & _
                        " and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber " & _
                        " and strSubPart Is not Null and airbranch.apbsubpartdata.active = '1' "
                    End If
                End If
            End If

            If chbLastFCE.Checked = True Then
                SQLSelect = SQLSelect & _
                "LastFCE, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".VW_SSCP_MT_FacilityAssignment.strAIRSNumber "
                End If
            End If

            If chbSSCPUnit.Checked = True Then
                SQLSelect = SQLSelect & _
                "strUnitDesc, "

                If SQLFrom.IndexOf("" & DBNameSpace & ".SSCPInspectionsRequired") <> -1 Then
                    '     SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".SSCPInspectionsRequired, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".SSCPInspectionsRequired.strAIRSNumber (+)  " & _
                    " and intyear = (select max(intyear) from airbranch.SSCPInspectionsRequired)   "
                End If
                If SQLFrom.IndexOf("" & DBNameSpace & ".LookUpEPDUnits") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " " & DBNameSpace & ".LookUpEPDUnits, "
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPUnit = " & DBNameSpace & ".LookUpEPDUnits.nuMUnitCode (+) "
                End If
            End If




            'Adding Where to SQL Statement
            If rdbAIRSNumberOr.Checked = True Then
                SQLWhereCase1 = " OR "
            Else
                SQLWhereCase1 = " AND "
            End If
            If rdbAIRSNumberEqual.Checked = True Then
                SQLWhereCase2 = " Like "
            Else
                SQLWhereCase2 = " Not Like "
            End If

            If txtAIRSNumberSearch1.Text <> "" Then
                SQLWhere = SQLWhere & " and (" & DBNameSpace & ".APBMasterAIRS.strairsnumber " & SQLWhereCase2 & " '0413%" & txtAIRSNumberSearch1.Text & "%') "
            End If
            If txtAIRSNumberSearch2.Text <> "" Then
                If txtAIRSNumberSearch1.Text <> "" Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                    " " & SQLWhereCase1 & " " & DBNameSpace & ".APBMasterAIRS.strairsnumber " & SQLWhereCase2 & " '0413%" & txtAIRSNumberSearch2.Text & "%' ) "
                Else
                    SQLWhere = SQLWhere & " and (" & DBNameSpace & ".APBMasterAIRS.strairsNumber " & SQLWhereCase2 & " '0413%" & txtAIRSNumberSearch2.Text & "%') "
                End If
            End If

            If rdbFacilityNameOr.Checked = True Then
                SQLWhereCase1 = " OR "
            Else
                SQLWhereCase1 = " AND "
            End If
            If rdbFacilityNameEqual.Checked = True Then
                SQLWhereCase2 = " Like "
            Else
                SQLWhereCase2 = " Not Like "
            End If
            If txtFacilityNameSearch1.Text <> "" Then
                SQLWhere = SQLWhere & " and (upper(strFacilityName) " & SQLWhereCase2 & " '%" & txtFacilityNameSearch1.Text.ToUpper & "%') "
            End If
            If txtFacilityNameSearch2.Text <> "" Then
                If txtFacilityNameSearch1.Text <> "" Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                    " " & SQLWhereCase1 & " upper(strFacilityName) " & SQLWhereCase2 & " '%" & txtFacilityNameSearch2.Text.ToUpper & "%' ) "
                Else
                    SQLWhere = SQLWhere & " and (upper(strFacilityName) " & SQLWhereCase2 & " '%" & txtFacilityNameSearch2.Text.ToUpper & "%') "
                End If
            End If

            If chbFacilityStreet1.Checked = True Then
                If rdbFacilityStreet1Or.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityStreet1Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityStreet1Search1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(strFacilityStreet1) " & SQLWhereCase2 & " '%" & txtFacilityStreet1Search1.Text.ToUpper & "%') "
                End If
                If txtFacilityStreet1Search2.Text <> "" Then
                    If txtFacilityStreet1Search1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(strFacilityStreet1) " & SQLWhereCase2 & " '%" & txtFacilityStreet1Search2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(strFacilityStreet1) " & SQLWhereCase2 & " '%" & txtFacilityStreet1Search2.Text.ToUpper & "%') "
                    End If
                End If
            End If

            If chbFacilityStreet2.Checked = True Then
                If rdbFacilityStreet2Or.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityStreet2Equal.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityStreet2Search1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(strFacilityStreet2) " & SQLWhereCase2 & " '%" & txtFacilityStreet2Search1.Text.ToUpper & "%') "
                End If
                If txtFacilityStreet2Search2.Text <> "" Then
                    If txtFacilityStreet2Search1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(strFacilityStreet2) " & SQLWhereCase2 & " '%" & txtFacilityStreet2Search2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(strFacilityStreet2) " & SQLWhereCase2 & " '%" & txtFacilityStreet2Search2.Text.ToUpper & "%') "
                    End If
                End If
            End If

            If chbFacilityCity.Checked = True Then
                If rdbFacilityCityOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityCityEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityCitySearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(strFacilityCity) " & SQLWhereCase2 & " '%" & txtFacilityCitySearch1.Text.ToUpper & "%') "
                End If
                If txtFacilityCitySearch2.Text <> "" Then
                    If txtFacilityCitySearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(strFacilityCity) " & SQLWhereCase2 & " '%" & txtFacilityCitySearch2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(strFacilityCity) " & SQLWhereCase2 & " '%" & txtFacilityCitySearch2.Text.ToUpper & "%') "
                    End If
                End If
            End If

            If chbFacilityZipCode.Checked = True Then
                If rdbFacilityZipCodeOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityZipCodeEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityZipCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(strFacilityZipCode) " & SQLWhereCase2 & " '%" & txtFacilityZipCodeSearch1.Text.ToUpper & "%') "
                End If
                If txtFacilityZipCodeSearch2.Text <> "" Then
                    If txtFacilityZipCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(strFacilityZipCode) " & SQLWhereCase2 & " '%" & txtFacilityZipCodeSearch2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(strFacilityzipcode) " & SQLWhereCase2 & " '%" & txtFacilityZipCodeSearch2.Text.ToUpper & "%') "
                    End If
                End If
            End If

            If chbFacilityLatitude.Checked = True Then
                If txtFacilityLatitudeSearch1.Text <> "" Or txtFacilityLatitudeSearch2.Text <> "" Then
                    If txtFacilityLatitudeSearch1.Text <> "" And txtFacilityLatitudeSearch2.Text = "" Then
                        SQLWhereCase1 = txtFacilityLatitudeSearch1.Text
                        SQLWhereCase2 = txtFacilityLatitudeSearch1.Text
                    End If
                    If txtFacilityLatitudeSearch1.Text = "" And txtFacilityLatitudeSearch2.Text <> "" Then
                        SQLWhereCase1 = txtFacilityLatitudeSearch2.Text
                        SQLWhereCase2 = txtFacilityLatitudeSearch2.Text
                    End If
                    If txtFacilityLatitudeSearch1.Text <> "" And txtFacilityLatitudeSearch2.Text <> "" Then
                        SQLWhereCase1 = txtFacilityLatitudeSearch1.Text
                        SQLWhereCase2 = txtFacilityLatitudeSearch2.Text
                    End If
                    SQLWhere = SQLWhere & " and numFacilityLatitude between " & SQLWhereCase1 & " and " & SQLWhereCase2 & ""
                End If
            End If

            If chbFacilityLongitude.Checked = True Then
                If txtFacilityLongitudeSearch1.Text <> "" Or txtFacilityLongitudeSearch2.Text <> "" Then
                    If txtFacilityLongitudeSearch1.Text <> "" And txtFacilityLongitudeSearch2.Text = "" Then
                        SQLWhereCase1 = txtFacilityLongitudeSearch1.Text
                        SQLWhereCase2 = txtFacilityLongitudeSearch1.Text
                    End If
                    If txtFacilityLongitudeSearch1.Text = "" And txtFacilityLongitudeSearch2.Text <> "" Then
                        SQLWhereCase1 = txtFacilityLongitudeSearch2.Text
                        SQLWhereCase2 = txtFacilityLongitudeSearch2.Text
                    End If
                    If txtFacilityLongitudeSearch1.Text <> "" And txtFacilityLongitudeSearch2.Text <> "" Then
                        SQLWhereCase1 = txtFacilityLongitudeSearch1.Text
                        SQLWhereCase2 = txtFacilityLongitudeSearch2.Text
                    End If
                    SQLWhere = SQLWhere & " and numFacilityLongitude between " & SQLWhereCase1 & " and " & SQLWhereCase2 & ""
                End If
            End If

            If chbCounty.Checked = True Then
                If rdbCountyOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbCountyEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboCountySearch1.SelectedIndex <> -1 And cboCountySearch1.SelectedIndex <> 0 Then
                    SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBMasterAIRS.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " '" & cboCountySearch1.SelectedValue & "') "
                End If
                If cboCountySearch2.SelectedIndex <> -1 And cboCountySearch2.SelectedIndex <> 0 Then
                    If cboCountySearch1.SelectedIndex <> -1 And cboCountySearch1.SelectedIndex <> 0 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " substr(" & DBNameSpace & ".APBMasterAIRS.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " '" & cboCountySearch2.SelectedValue & "' ) "
                    Else
                        SQLWhere = SQLWhere & " and (substr(" & DBNameSpace & ".APBMasterAIRS.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " '" & cboCountySearch2.SelectedValue & "') "
                    End If
                End If
            End If

            If chbSSCPEngineer.Checked = True Then
                If rdbSSCPEngineerOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSSCPEngineerEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboSSCPEngineerSearch1.SelectedIndex <> -1 And cboSSCPEngineerSearch1.SelectedIndex <> 0 Then
                    SQLWhere = SQLWhere & " and (" & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer " & SQLWhereCase2 & " '" & cboSSCPEngineerSearch1.SelectedValue & "') "
                End If
                If cboSSCPEngineerSearch2.SelectedIndex <> -1 And cboSSCPEngineerSearch2.SelectedIndex <> 0 Then
                    If cboSSCPEngineerSearch1.SelectedIndex <> -1 And cboSSCPEngineerSearch1.SelectedIndex <> 0 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " " & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer " & SQLWhereCase2 & " '" & cboSSCPEngineerSearch2.SelectedValue & "' ) "
                    Else
                        SQLWhere = SQLWhere & " and (" & DBNameSpace & ".SSCPInspectionsRequired.numSSCPEngineer " & SQLWhereCase2 & " '" & cboSSCPEngineerSearch2.SelectedValue & "') "
                    End If
                End If
            End If

            If chbDistrict.Checked = True Then
                If rdbDistrictOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbDistrictEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboDistrictSearch1.SelectedIndex <> -1 And cboDistrictSearch1.SelectedIndex <> 0 Then
                    SQLWhere = SQLWhere & " and (" & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " '" & cboDistrictSearch1.SelectedValue & "') "
                End If
                If cboDistrictSearch2.SelectedIndex <> -1 And cboDistrictSearch2.SelectedIndex <> 0 Then
                    If cboDistrictSearch1.SelectedIndex <> -1 And cboDistrictSearch1.SelectedIndex <> 0 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " " & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " '" & cboDistrictSearch2.SelectedValue & "' ) "
                    Else
                        SQLWhere = MasterSQL & " and (" & DBNameSpace & ".LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " '" & cboDistrictSearch2.SelectedValue & "') "
                    End If
                End If
            End If

            If chbOperationStatus.Checked = True Then
                If rdbOperationalStatusOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbOperationStatusEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboOperationStatusSearch1.Text <> "" And cboOperationStatusSearch1.Text <> " " Then
                    SQLWhere = SQLWhere & " and (APBHeaderdata.strOperationalStatus " & SQLWhereCase2 & " '%" & Mid(cboOperationStatusSearch1.Text.ToUpper, 1, 1) & "%') "
                End If
                If cboOperationStatusSearch2.Text <> "" And cboOperationStatusSearch2.Text <> " " Then
                    If cboOperationStatusSearch1.Text <> "" And cboOperationStatusSearch1.Text <> " " Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " APBHeaderdata.strOperationalStatus " & SQLWhereCase2 & " '%" & Mid(cboOperationStatusSearch2.Text.ToUpper, 1, 1) & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderdata.strOperationalStatus " & SQLWhereCase2 & " '%" & Mid(cboOperationStatusSearch2.Text.ToUpper, 1, 1) & "%') "
                    End If
                End If
            End If

            If chbClassification.Checked = True Then
                If rdbClassificationOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbClassificationEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboClassificationSearch1.Text <> "" And cboClassificationSearch1.Text <> " " Then
                    SQLWhere = SQLWhere & " and (APBHeaderdata.strClass " & SQLWhereCase2 & " '%" & Mid(cboClassificationSearch1.Text.ToUpper, 1, 1) & "%') "
                End If
                If cboClassificationSearch2.Text <> "" And cboClassificationSearch2.Text <> " " Then
                    If cboClassificationSearch1.Text <> "" And cboClassificationSearch1.Text <> " " Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " APBHeaderdata.strClass " & SQLWhereCase2 & " '%" & Mid(cboClassificationSearch2.Text.ToUpper, 1, 1) & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderdata.strClass " & SQLWhereCase2 & " '%" & Mid(cboClassificationSearch2.Text.ToUpper, 1, 1) & "%') "
                    End If
                End If
            End If

            If chbSICCode.Checked = True Then
                If rdbSICCodeOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSICCodeEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtSICCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(APBHeaderdata.strSICCode) " & SQLWhereCase2 & " '%" & txtSICCodeSearch1.Text.ToUpper & "%') "
                End If
                If txtSICCodeSearch2.Text <> "" Then
                    If txtSICCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(APBHeaderdata.strSICCode) " & SQLWhereCase2 & " '%" & txtSICCodeSearch2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(APBHeaderdata.strSICCode) " & SQLWhereCase2 & " '%" & txtSICCodeSearch2.Text.ToUpper & "%') "
                    End If
                End If
            End If

            If chbNAICSCode.Checked = True Then
                If rdbNAICSCodeOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbNAICSCodeEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtNAICSCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(APBHeaderdata.strNAICSCode) " & SQLWhereCase2 & " '%" & txtNAICSCodeSearch1.Text.ToUpper & "%') "
                End If
                If txtNAICSCodeSearch2.Text <> "" Then
                    If txtNAICSCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(APBHeaderdata.strNAICSCode) " & SQLWhereCase2 & " '%" & txtNAICSCodeSearch2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(APBHeaderdata.strNAICSCode) " & SQLWhereCase2 & " '%" & txtNAICSCodeSearch2.Text.ToUpper & "%') "
                    End If
                End If
            End If

            If chbStartUpDate.Checked = True Then
                If DTPStartUpDateSearch1.Checked = True Or DTPStartUpDateSearch2.Checked = True Then
                    If DTPStartUpDateSearch1.Checked = True And DTPStartUpDateSearch2.Checked = False Then
                        SQLWhereCase1 = DTPStartUpDateSearch1.Text
                        SQLWhereCase2 = DTPStartUpDateSearch1.Text
                    End If
                    If DTPStartUpDateSearch1.Checked = False And DTPStartUpDateSearch2.Checked = True Then
                        SQLWhereCase1 = DTPStartUpDateSearch2.Text
                        SQLWhereCase2 = DTPStartUpDateSearch2.Text
                    End If
                    If DTPStartUpDateSearch1.Checked = True And DTPStartUpDateSearch2.Checked = True Then
                        SQLWhereCase1 = DTPStartUpDateSearch1.Text
                        SQLWhereCase2 = DTPStartUpDateSearch2.Text
                    End If
                    SQLWhere = SQLWhere & " and datStartUpDate between '" & SQLWhereCase1 & "' and '" & SQLWhereCase2 & "' "
                End If
            End If

            If chbShutDownDate.Checked = True Then
                If DTPShutDownDateSearch1.Checked = True Or DTPShutDownDateSearch2.Checked = True Then
                    If DTPShutDownDateSearch1.Checked = True And DTPShutDownDateSearch2.Checked = False Then
                        SQLWhereCase1 = DTPShutDownDateSearch1.Text
                        SQLWhereCase2 = DTPShutDownDateSearch1.Text
                    End If
                    If DTPShutDownDateSearch1.Checked = False And DTPShutDownDateSearch2.Checked = True Then
                        SQLWhereCase1 = DTPShutDownDateSearch2.Text
                        SQLWhereCase2 = DTPShutDownDateSearch2.Text
                    End If
                    If DTPShutDownDateSearch1.Checked = True And DTPShutDownDateSearch2.Checked = True Then
                        SQLWhereCase1 = DTPShutDownDateSearch1.Text
                        SQLWhereCase2 = DTPShutDownDateSearch2.Text
                    End If
                    SQLWhere = SQLWhere & " and datShutdownDate between '" & SQLWhereCase1 & "' and '" & SQLWhereCase2 & "' "
                End If
            End If

            If chbLastFCE.Checked = True Then
                If DTPLastFCESearch1.Checked = True Or DTPLastFCESearch2.Checked = True Then
                    If DTPLastFCESearch1.Checked = True And DTPLastFCESearch2.Checked = False Then
                        SQLWhereCase1 = DTPLastFCESearch1.Text
                        SQLWhereCase2 = DTPLastFCESearch1.Text
                    End If
                    If DTPLastFCESearch1.Checked = False And DTPLastFCESearch2.Checked = True Then
                        SQLWhereCase1 = DTPLastFCESearch2.Text
                        SQLWhereCase2 = DTPLastFCESearch2.Text
                    End If
                    If DTPLastFCESearch1.Checked = True And DTPLastFCESearch2.Checked = True Then
                        SQLWhereCase1 = DTPLastFCESearch1.Text
                        SQLWhereCase2 = DTPLastFCESearch2.Text
                    End If
                    SQLWhere = SQLWhere & " and to_date(LastFCE) between '" & SQLWhereCase1 & "' and '" & SQLWhereCase2 & "' "
                End If
            End If

            If chbCMSUniverse.Checked = True Then
                If rdbCMSUniverseOR.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbCMSUniverseEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboCMSUniverseSearch1.SelectedIndex <> -1 And cboCMSUniverseSearch1.SelectedIndex <> 0 Then
                    SQLWhere = SQLWhere & " and (APBSupplamentalData.strCMSMember " & SQLWhereCase2 & " '" & cboCMSUniverseSearch1.Text & "') "
                End If
                If cboCMSUniverseSearch2.SelectedIndex <> -1 And cboCMSUniverseSearch2.SelectedIndex <> 0 Then
                    If cboCMSUniverseSearch1.SelectedIndex <> -1 And cboCMSUniverseSearch1.SelectedIndex <> 0 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " APBSupplamentalData.strCMSMember " & SQLWhereCase2 & " '" & cboCMSUniverseSearch2.Text & "' ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBSupplamentalData.strCMSMember " & SQLWhereCase2 & " '" & cboCMSUniverseSearch2.Text & "') "
                    End If
                End If
            End If

            If chbPlantDescription.Checked = True Then
                If rdbPlantDescriptionOR.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbPlantDescriptionEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtPlantDescriptionSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (upper(APBHeaderData.strPlantDescription) " & SQLWhereCase2 & " '%" & txtPlantDescriptionSearch1.Text.ToUpper & "%') "
                End If
                If txtPlantDescriptionSearch2.Text <> "" Then
                    If txtPlantDescriptionSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(APBHeaderData.strPlantDescription) " & SQLWhereCase2 & " '%" & txtPlantDescriptionSearch2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(APBHeaderData.strPlantDescription) " & SQLWhereCase2 & " '%" & txtPlantDescriptionSearch2.Text.ToUpper & "%') "
                    End If
                End If
            End If


            If chbSSCPUnit.Checked = True Then
                If rdbSSCPUnitOr.Checked = True Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSSCPUnitEqual.Checked = True Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboSSCPUnitSearch1.Text <> " " Then
                    SQLWhere = SQLWhere & " and (upper(strUnitDesc) " & SQLWhereCase2 & " '%" & cboSSCPUnitSearch1.Text.ToUpper & "%') "
                End If
                If cboSSCPUnitSearch2.Text <> " " Then
                    If cboSSCPUnitSearch1.Text <> " " Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) & _
                        " " & SQLWhereCase1 & " upper(strUnitDesc) " & SQLWhereCase2 & " '%" & cboSSCPUnitSearch2.Text.ToUpper & "%' ) "
                    Else
                        SQLWhere = SQLWhere & " and (upper(strUnitDesc) " & SQLWhereCase2 & " '%" & cboSSCPUnitSearch2.Text.ToUpper & "%') "
                    End If
                End If
            End If


            If txtFacilityAIRSNumberOrder.Text <> "" Or txtFacilityNameOrder.Text <> "" _
                     Or txtFacilityStreet1Order.Text <> "" Or txtFacilityStreet2Order.Text <> "" _
                     Or txtFacilityCityOrder.Text <> "" Or txtFacilityZipCodeOrder.Text <> "" _
                     Or txtFacilityLatitudeOrder.Text <> "" Or txtFacilityLongitudeOrder.Text <> "" _
                     Or txtCountyOrder.Text <> "" Or txtDistrictOrder.Text <> "" _
                     Or txtOperationStatusOrder.Text <> "" Or txtClassificationOrder.Text <> "" _
                     Or txtSICCodeOrder.Text <> "" Or txtStartUpDateOrder.Text <> "" _
                     Or txtShutDownDateOrder.Text <> "" Or txtCMSUniverseOrder.Text <> "" _
                     Or txtPlantDescriptionOrder.Text <> "" Or txtAPC0Order.Text <> "" _
                     Or txtAPC1Order.Text <> "" Or txtAPC3Order.Text <> "" _
                     Or txtAPC4Order.Text <> "" Or txtAPC6Order.Text <> "" _
                     Or txtAPC7Order.Text <> "" Or txtAPC8Order.Text <> "" _
                     Or txtAPC9Order.Text <> "" Or txtAPCAOrder.Text <> "" _
                     Or txtAPCFOrder.Text <> "" Or txtAPCIOrder.Text <> "" _
                     Or txtAPCMOrder.Text <> "" Or txtAPCVOrder.Text <> "" Then
                i = 1
                If txtFacilityAIRSNumberOrder.Text <> "" And chbAIRSNumber.Checked = True Then
                    temp = temp & txtFacilityAIRSNumberOrder.Text & "-AIRSNumber, "
                    i += 1
                End If
                If txtFacilityNameOrder.Text <> "" And chbFacilityName.Checked = True Then
                    temp = temp & txtFacilityNameOrder.Text & "-strFacilityName, "
                    i += 1
                End If
                If txtFacilityStreet1Order.Text <> "" And chbFacilityStreet1.Checked = True Then
                    temp = temp & txtFacilityStreet1Order.Text & "-strFacilityStreet1, "
                    i += 1
                End If
                If txtFacilityStreet2Order.Text <> "" And chbFacilityStreet2.Checked = True Then
                    temp = temp & txtFacilityStreet2Order.Text & "-strFacilityStreet2, "
                    i += 1
                End If
                If txtFacilityCityOrder.Text <> "" And chbFacilityCity.Checked = True Then
                    temp = temp & txtFacilityCityOrder.Text & "-strFacilityCity, "
                    i += 1
                End If
                If txtFacilityZipCodeOrder.Text <> "" And chbFacilityZipCode.Checked = True Then
                    temp = temp & txtFacilityZipCodeOrder.Text & "-strFacilityZipCode, "
                    i += 1
                End If
                If txtFacilityLatitudeOrder.Text <> "" And chbFacilityLatitude.Checked = True Then
                    temp = temp & txtFacilityLatitudeOrder.Text & "-numFacilityLatitude, "
                    i += 1
                End If
                If txtFacilityLongitudeOrder.Text <> "" And chbFacilityLongitude.Checked = True Then
                    temp = temp & txtFacilityLongitudeOrder.Text & "-numFacilityLongitude, "
                    i += 1
                End If
                If txtCountyOrder.Text <> "" And chbCounty.Checked = True Then
                    temp = temp & txtCountyOrder.Text & "-strCountyName, "
                    i += 1
                End If
                If txtDistrictOrder.Text <> "" And chbDistrict.Checked = True Then
                    temp = temp & txtDistrictOrder.Text & "-strDistrictName, "
                    i += 1
                End If
                If txtOperationStatusOrder.Text <> "" And chbOperationStatus.Checked = True Then
                    temp = temp & txtOperationStatusOrder.Text & "-strOperationalStatus, "
                    i += 1
                End If
                If txtClassificationOrder.Text <> "" And chbClassification.Checked = True Then
                    temp = temp & txtClassificationOrder.Text & "-strClass, "
                    i += 1
                End If
                If txtSICCodeOrder.Text <> "" And chbSICCode.Checked = True Then
                    temp = temp & txtSICCodeOrder.Text & "-strSICCode, "
                    i += 1
                End If
                If txtNAICSCodeOrder.Text <> "" And chbNAICSCode.Checked = True Then
                    temp = temp & txtNAICSCodeOrder.Text & "-strNAICSCode, "
                    i += 1
                End If
                If txtStartUpDateOrder.Text <> "" And chbStartUpDate.Checked = True Then
                    temp = temp & txtStartUpDateOrder.Text & "-datStartUpDate, "
                    i += 1
                End If
                If txtShutDownDateOrder.Text <> "" And chbShutDownDate.Checked = True Then
                    temp = temp & txtShutDownDateOrder.Text & "-datShutDownDate, "
                    i += 1
                End If
                If txtLastFCEOrder.Text <> "" And chbLastFCE.Checked = True Then
                    temp = temp & txtLastFCEOrder.Text & "-LastFCE, "
                    i += 1
                End If
                If txtCMSUniverseOrder.Text <> "" And chbCMSUniverse.Checked = True Then
                    temp = temp & txtCMSUniverseOrder.Text & "-strCMSmember, "
                    i += 1
                End If
                If txtPlantDescriptionOrder.Text <> "" And chbPlantDescription.Checked = True Then
                    temp = temp & txtPlantDescriptionOrder.Text & "-strPlantDescription, "
                    i += 1
                End If
                If txtAPC0Order.Text <> "" And chbAPC0.Checked = True Then
                    temp = temp & txtAPC0Order.Text & "-APC0, "
                    i += 1
                End If
                If txtAPC1Order.Text <> "" And chbAPC1.Checked = True Then
                    temp = temp & txtAPC1Order.Text & "-APC1, "
                    i += 1
                End If
                If txtAPC3Order.Text <> "" And chbAPC3.Checked = True Then
                    temp = temp & txtAPC3Order.Text & "-APC3, "
                    i += 1
                End If
                If txtAPC4Order.Text <> "" And chbAPC4.Checked = True Then
                    temp = temp & txtAPC4Order.Text & "-APC4, "
                    i += 1
                End If
                If txtAPC6Order.Text <> "" And chbAPC6.Checked = True Then
                    temp = temp & txtAPC6Order.Text & "-APC6, "
                    i += 1
                End If
                If txtAPC7Order.Text <> "" And chbAPC7.Checked = True Then
                    temp = temp & txtAPC7Order.Text & "-APC7, "
                    i += 1
                End If
                If txtAPC8Order.Text <> "" And chbAPC8.Checked = True Then
                    temp = temp & txtAPC8Order.Text & "-APC8, "
                    i += 1
                End If
                If txtAPC9Order.Text <> "" And chbAPC9.Checked = True Then
                    temp = temp & txtAPC9Order.Text & "-APC9, "
                    i += 1
                End If
                If txtAPCAOrder.Text <> "" And chbAPCA.Checked = True Then
                    temp = temp & txtAPCAOrder.Text & "-APCA, "
                    i += 1
                End If
                If txtAPCFOrder.Text <> "" And chbAPCF.Checked = True Then
                    temp = temp & txtAPCFOrder.Text & "-APCF, "
                    i += 1
                End If
                If txtAPCIOrder.Text <> "" And chbAPCI.Checked = True Then
                    temp = temp & txtAPCIOrder.Text & "-APCI, "
                    i += 1
                End If
                If txtAPCMOrder.Text <> "" And chbAPCM.Checked = True Then
                    temp = temp & txtAPCMOrder.Text & "-APCM, "
                    i += 1
                End If
                If txtAPCVOrder.Text <> "" And chbAPCV.Checked = True Then
                    temp = temp & txtAPCVOrder.Text & "-APCV, "
                    i += 1
                End If

                For j = 1 To i - 1
                    Select Case j.ToString.Length
                        Case 1
                            SQLOrder = SQLOrder & Mid(temp, (temp.IndexOf(j.ToString & "-") + 3), Mid(temp, temp.IndexOf(j.ToString & "-") + 3).IndexOf(", ") + 2)
                        Case 2
                            SQLOrder = SQLOrder & Mid(temp, (temp.IndexOf(j.ToString & "-") + 4), Mid(temp, temp.IndexOf(j.ToString & "-") + 3).IndexOf(", ") + 1)
                        Case 3
                            SQLOrder = SQLOrder & Mid(temp, (temp.IndexOf(j.ToString & "-") + 5), Mid(temp, temp.IndexOf(j.ToString & "-") + 3).IndexOf(", "))
                    End Select
                Next j

                If SQLOrder <> "" Then
                    SQLOrder = " Order by " & SQLOrder
                Else
                    SQLOrder = " Order by AIRSNumber, strFacilityName, "
                End If
            Else
                SQLOrder = " Order by AIRSNumber, strFacilityName, "
            End If

            SQL = Mid(SQLSelect, 1, (SQLSelect.Length - 2)) & _
            Mid(SQLFrom, 1, (SQLFrom.Length - 2)) & _
            SQLWhere & _
            Mid(SQLOrder, 1, (SQLOrder.Length - 2))

            MasterSQL = "Select distinct * " & _
            "from (" & SQL & ") MasterSQL " & _
            "Where AIRSNumber is Not Null "

            If chb1HrYes.Checked = True Then
                If rdb1HrYesEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and OneHRYes is not null "
                Else
                    MasterSQL = MasterSQL & " and OneHRYes is null "
                End If
            End If

            If chb1HrNo.Checked = True Then
                If rdb1HrNoEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and OneHRNo is not null "
                Else
                    MasterSQL = MasterSQL & " and OneHRNo is null "
                End If
            End If

            If chb1HrContribute.Checked = True Then
                If rdb1HrContributeEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and OneHRContribute is not null "
                Else
                    MasterSQL = MasterSQL & " and OneHRContribute is null "
                End If
            End If

            If chb8HrAtlanta.Checked = True Then
                If rdb8HrAtlantaEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and EightHRAtlanta is not null "
                Else
                    MasterSQL = MasterSQL & " and EightHRAtlanta is null "
                End If
            End If

            If chb8HrMacon.Checked = True Then
                If rdb8HrMaconEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and EightHRMacon is not null "
                Else
                    MasterSQL = MasterSQL & " and EightHRMacon is null "
                End If
            End If

            If chb8HrNo.Checked = True Then
                If rdb8HrNoEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and EightHRNo is not null "
                Else
                    MasterSQL = MasterSQL & " and EightHRNo is null "
                End If
            End If

            If chbPMAtlanta.Checked = True Then
                If rdbPMAtlantaEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and PMAtlanta is not null "
                Else
                    MasterSQL = MasterSQL & " and PMAtlanta is null "
                End If
            End If

            If chbPMChattanooga.Checked = True Then
                If rdbPMChattanoogaEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and PMChattanooga is not null "
                Else
                    MasterSQL = MasterSQL & " and PMChattanooga is null "
                End If
            End If

            If chbPMFloyd.Checked = True Then
                If rdbPMFloydEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and PMFloyd is not null "
                Else
                    MasterSQL = MasterSQL & " and PMFloyd is null "
                End If
            End If

            If chbPMMacon.Checked = True Then
                If rdbPMMaconEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and PMMacon is not null "
                Else
                    MasterSQL = MasterSQL & " and PMMacon is null "
                End If
            End If

            If chbPMNo.Checked = True Then
                If rdbPMNoEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and PMNo is not null "
                Else
                    MasterSQL = MasterSQL & " and PMNo is null "
                End If
            End If

            If chbNSRPSDMajor.Checked = True Then
                If rdbNSRPSDMajorEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and NSRPSD is not Null "
                Else
                    MasterSQL = MasterSQL & " and NSRPSD is Null "
                End If
            End If

            If chbHAPMajor.Checked = True Then
                If rdbHAPMajorEqual.Checked = True Then
                    MasterSQL = MasterSQL & " and HAP is not Null "
                Else
                    MasterSQL = MasterSQL & " and HAP is Null "
                End If
            End If

            If rdbAPCAnd.Checked = True Then
                If chbAPC0.Checked = True Then
                    If rdbAPC0Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC0 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC0 is null "
                    End If
                End If
                If chbAPC1.Checked = True Then
                    If rdbAPC1Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC1 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC1 is null "
                    End If
                End If
                If chbAPC3.Checked = True Then
                    If rdbAPC3Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC3 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC3 is null "
                    End If
                End If
                If chbAPC4.Checked = True Then
                    If rdbAPC4Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC4 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC4 is null "
                    End If
                End If
                If chbAPC6.Checked = True Then
                    If rdbAPC6Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC6 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC6 is null "
                    End If
                End If
                If chbAPC7.Checked = True Then
                    If rdbAPC7Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC7 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC7 is null "
                    End If
                End If
                If chbAPC8.Checked = True Then
                    If rdbAPC8Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC8 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC8 is null "
                    End If
                End If
                If chbAPC9.Checked = True Then
                    If rdbAPC9Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and APC9 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC9 is null "
                    End If
                End If
                If chbAPCA.Checked = True Then
                    If rdbAPCAEqual.Checked = True Then
                        MasterSQL = MasterSQL & " and APCA is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCA is null "
                    End If
                End If
                If chbAPCF.Checked = True Then
                    If rdbAPCFEqual.Checked = True Then
                        MasterSQL = MasterSQL & " and APCF is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCF is null "
                    End If
                End If
                If chbAPCI.Checked = True Then
                    If rdbAPCIEqual.Checked = True Then
                        MasterSQL = MasterSQL & " and APCI is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCI is null "
                    End If
                End If
                If chbAPCM.Checked = True Then
                    If rdbAPCMEqual.Checked = True Then
                        MasterSQL = MasterSQL & " and APCM is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCM is null "
                    End If
                End If
                If chbAPCV.Checked = True Then
                    If rdbAPCVEqual.Checked = True Then
                        MasterSQL = MasterSQL & " and APCV is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCV is null "
                    End If
                End If
            Else
                If chbAPC0.Checked = True Or chbAPC1.Checked = True _
                 Or chbAPC3.Checked = True Or chbAPC4.Checked = True _
                  Or chbAPC6.Checked = True Or chbAPC7.Checked = True _
                   Or chbAPC8.Checked = True Or chbAPC9.Checked = True _
                    Or chbAPCA.Checked = True Or chbAPCF.Checked = True _
                     Or chbAPCI.Checked = True Or chbAPCM.Checked = True _
                      Or chbAPCV.Checked = True Then
                    MasterSQL = MasterSQL & " and ("

                    If chbAPC0.Checked = True Then
                        If rdbAPC0Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC0 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC0 is null or "
                        End If
                    End If
                    If chbAPC1.Checked = True Then
                        If rdbAPC1Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC1 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC1 is null or "
                        End If
                    End If
                    If chbAPC3.Checked = True Then
                        If rdbAPC3Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC3 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC3 is null or "
                        End If
                    End If
                    If chbAPC4.Checked = True Then
                        If rdbAPC4Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC4 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC4 is null or "
                        End If
                    End If
                    If chbAPC6.Checked = True Then
                        If rdbAPC6Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC6 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC6 is null or "
                        End If
                    End If
                    If chbAPC7.Checked = True Then
                        If rdbAPC7Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC7 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC7 is null or "
                        End If
                    End If
                    If chbAPC8.Checked = True Then
                        If rdbAPC8Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC8 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC8 is null or "
                        End If
                    End If
                    If chbAPC9.Checked = True Then
                        If rdbAPC9Equal.Checked = True Then
                            MasterSQL = MasterSQL & " APC9 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC9 is null or "
                        End If
                    End If
                    If chbAPCA.Checked = True Then
                        If rdbAPCAEqual.Checked = True Then
                            MasterSQL = MasterSQL & " APCA is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCA is null or "
                        End If
                    End If
                    If chbAPCF.Checked = True Then
                        If rdbAPCFEqual.Checked = True Then
                            MasterSQL = MasterSQL & " APCF is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCF is null or "
                        End If
                    End If
                    If chbAPCI.Checked = True Then
                        If rdbAPCIEqual.Checked = True Then
                            MasterSQL = MasterSQL & " APCI is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCI is null or "
                        End If
                    End If
                    If chbAPCM.Checked = True Then
                        If rdbAPCMEqual.Checked = True Then
                            MasterSQL = MasterSQL & " APCM is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCM is null or "
                        End If
                    End If
                    If chbAPCV.Checked = True Then
                        If rdbAPCVEqual.Checked = True Then
                            MasterSQL = MasterSQL & " APCV is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCV is null or "
                        End If
                    End If
                    MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 3)) & " ) "
                End If
            End If

            If chbAllSubparts.Checked = True Then
                If chbSIP.Checked = True Then
                    If rdbSIPEqual.Checked = True Then
                        MasterSQL = MasterSQL & " and GASIP is not null "
                    Else
                        MasterSQL = MasterSQL & " and GASIP is null "
                    End If
                End If

                If chbPart61Subpart.Checked = True Then
                    If rdbPart61Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and Part61 is not null "
                    Else
                        MasterSQL = MasterSQL & " and Part61 is null "
                    End If
                End If

                If chbPart60Subpart.Checked = True Then
                    If rdbPart60Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and Part60 is not null "
                    Else
                        MasterSQL = MasterSQL & " and Part60 is null "
                    End If
                End If

                If chbPart63Subpart.Checked = True Then
                    If rdbPart63Equal.Checked = True Then
                        MasterSQL = MasterSQL & " and Part63 is not null "
                    Else
                        MasterSQL = MasterSQL & " and Part63 is null "
                    End If
                End If
            Else
                If chbSIP.Checked = True Then
                    If rdbSIPSubPartOr.Checked = True Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbSIPEqual.Checked = True Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboSIPSearch1.Text <> "" And cboSIPSearch1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (GASIP " & SQLWhereCase2 & " '" & cboSIPSearch1.Text & "' ) "
                    End If
                    If cboSIPSearch2.Text <> "" And cboSIPSearch2.Text <> " " Then
                        If cboSIPSearch1.Text <> "" And cboSIPSearch1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) & _
                            " " & SQLWhereCase1 & " GASIP " & SQLWhereCase2 & " '" & cboSIPSearch2.Text & "' ) "
                        Else
                            MasterSQL = MasterSQL & " and (GASIP " & SQLWhereCase2 & " '" & cboSIPSearch2.Text & "') "
                        End If
                    End If
                End If

                If chbPart61Subpart.Checked = True Then
                    If rdbPart61SubPartOr.Checked = True Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbPart61Equal.Checked = True Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboPart61Search1.Text <> "" And cboPart61Search1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (Part61 " & SQLWhereCase2 & " '" & cboPart61Search1.Text & "' ) "
                    End If
                    If cboPart61Search2.Text <> "" And cboPart61Search2.Text <> " " Then
                        If cboPart61Search1.Text <> "" And cboPart61Search1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) & _
                            " " & SQLWhereCase1 & " Part61 " & SQLWhereCase2 & " '" & cboPart61Search2.Text & "' ) "
                        Else
                            MasterSQL = MasterSQL & " and (Part61 " & SQLWhereCase2 & " '" & cboPart61Search2.Text & "') "
                        End If
                    End If
                End If

                If chbPart60Subpart.Checked = True Then
                    If rdbPart60SubPartOr.Checked = True Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbPart60Equal.Checked = True Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboPart60Search1.Text <> "" And cboPart60Search1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (Part60 " & SQLWhereCase2 & " '" & cboPart60Search1.Text & "' ) "
                    End If
                    If cboPart60Search2.Text <> "" And cboPart60Search2.Text <> " " Then
                        If cboPart60Search1.Text <> "" And cboPart60Search1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) & _
                            " " & SQLWhereCase1 & " Part60 " & SQLWhereCase2 & " '" & cboPart60Search2.Text & "' ) "
                        Else
                            MasterSQL = MasterSQL & " and (Part60 " & SQLWhereCase2 & " '" & cboPart60Search2.Text & "') "
                        End If
                    End If
                End If

                If chbPart63Subpart.Checked = True Then
                    If rdbPart63SubPartOR.Checked = True Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbPart63Equal.Checked = True Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboPart63Search1.Text <> "" And cboPart63Search1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (Part63 " & SQLWhereCase2 & " '" & cboPart63Search1.Text & "' ) "
                    End If
                    If cboPart63Search2.Text <> "" And cboPart63Search2.Text <> " " Then
                        If cboPart63Search1.Text <> "" And cboPart63Search1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) & _
                            " " & SQLWhereCase1 & " Part63 " & SQLWhereCase2 & " '" & cboPart63Search2.Text & "' ) "
                        Else
                            MasterSQL = MasterSQL & " and (Part63 " & SQLWhereCase2 & " '" & cboPart63Search2.Text & "') "
                        End If
                    End If
                End If
                If chbSIP.Checked = True Or chbPart60Subpart.Checked = True Or chbPart61Subpart.Checked = True Or chbPart63Subpart.Checked = True Then
                    If chbSIP.Checked = True And chbPart60Subpart.Checked = True And chbPart61Subpart.Checked = True And chbPart63Subpart.Checked = True Then
                        MasterSQL = MasterSQL & " and (Part60 is not null or GASIP is not null or Part61 is not null or Part63 is not null) "
                    Else
                        MasterSQL = MasterSQL & " and ( "
                        If chbSIP.Checked = True Then
                            MasterSQL = MasterSQL & " GASIP is not Null or "
                        End If
                        If chbPart60Subpart.Checked = True Then
                            MasterSQL = MasterSQL & " Part60 is not Null or "
                        End If
                        If chbPart61Subpart.Checked = True Then
                            MasterSQL = MasterSQL & " Part61 is not Null or "
                        End If
                        If chbPart63Subpart.Checked = True Then
                            MasterSQL = MasterSQL & " Part63 is not Null or "
                        End If
                        MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 3)) & " ) "
                    End If
                End If
            End If

            txtSQLStatement.Text = MasterSQL.ToString()

            dsSQLQuery = New DataSet

            daSQLQuery = New OracleDataAdapter(MasterSQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daSQLQuery.Fill(dsSQLQuery, "SQLQuery")
            dgvQueryGenerator.DataSource = dsSQLQuery
            dgvQueryGenerator.DataMember = "SQLQuery"

            i = 0
            dgvQueryGenerator.RowHeadersVisible = False
            dgvQueryGenerator.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvQueryGenerator.AllowUserToResizeColumns = True
            dgvQueryGenerator.AllowUserToAddRows = False
            dgvQueryGenerator.AllowUserToDeleteRows = False
            dgvQueryGenerator.AllowUserToOrderColumns = True
            dgvQueryGenerator.AllowUserToResizeRows = True
            dgvQueryGenerator.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvQueryGenerator.Columns("AIRSNumber").DisplayIndex = i
            dgvQueryGenerator.Columns("AIRSNumber").Width = "75"
            i += 1
            dgvQueryGenerator.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvQueryGenerator.Columns("strFacilityName").DisplayIndex = i
            dgvQueryGenerator.Columns("strFacilityName").Width = "200"
            i += 1

            If chbFacilityStreet1.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityStreet1").HeaderText = "Street Address 1"
                dgvQueryGenerator.Columns("strFacilityStreet1").DisplayIndex = i
                dgvQueryGenerator.Columns("strFacilityStreet1").Width = "200"
                i += 1
            End If
            If chbFacilityStreet2.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityStreet2").HeaderText = "Street Address 2"
                dgvQueryGenerator.Columns("strFacilityStreet2").DisplayIndex = i
                dgvQueryGenerator.Columns("strFacilityStreet2").Width = "200"
                i += 1
            End If
            If chbFacilityCity.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityCity").HeaderText = "City"
                dgvQueryGenerator.Columns("strFacilityCity").DisplayIndex = i
                dgvQueryGenerator.Columns("strFacilityCity").Width = "100"
                i += 1
            End If
            If chbFacilityZipCode.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityZipCode").HeaderText = "Zip Code"
                dgvQueryGenerator.Columns("strFacilityZipCode").DisplayIndex = i
                dgvQueryGenerator.Columns("strFacilityZipCode").Width = "100"
                i += 1
            End If
            If chbFacilityLatitude.Checked = True Then
                dgvQueryGenerator.Columns("numFacilityLatitude").HeaderText = "Latitude"
                dgvQueryGenerator.Columns("numFacilityLatitude").DisplayIndex = i
                dgvQueryGenerator.Columns("numFacilityLatitude").Width = "100"
                i += 1
            End If
            If chbFacilityLongitude.Checked = True Then
                dgvQueryGenerator.Columns("numFacilityLongitude").HeaderText = "Longitude"
                dgvQueryGenerator.Columns("numFacilityLongitude").DisplayIndex = i
                dgvQueryGenerator.Columns("numFacilityLongitude").Width = "100"
                i += 1
            End If
            If chbCounty.Checked = True Then
                dgvQueryGenerator.Columns("strCountyName").HeaderText = "County"
                dgvQueryGenerator.Columns("strCountyName").DisplayIndex = i
                dgvQueryGenerator.Columns("strCountyName").Width = "100"
                i += 1
            End If
            If chbSSCPEngineer.Checked = True Then
                dgvQueryGenerator.Columns("SSCPEngineer").HeaderText = "Complinace Engineer"
                dgvQueryGenerator.Columns("SSCPEngineer").DisplayIndex = i
                dgvQueryGenerator.Columns("SSCPEngineer").Width = "150"
                i += 1
            End If
            If chbDistrict.Checked = True Then
                dgvQueryGenerator.Columns("strDistrictName").HeaderText = "Distirct"
                dgvQueryGenerator.Columns("strDistrictName").DisplayIndex = i
                dgvQueryGenerator.Columns("strDistrictName").Width = "100"
                i += 1
            End If
            If chbOperationStatus.Checked = True Then
                dgvQueryGenerator.Columns("strOperationalStatus").HeaderText = "Operation Status"
                dgvQueryGenerator.Columns("strOperationalStatus").DisplayIndex = i
                dgvQueryGenerator.Columns("strOperationalStatus").Width = "75"
                i += 1
            End If
            If chbClassification.Checked = True Then
                dgvQueryGenerator.Columns("strClass").HeaderText = "Classification"
                dgvQueryGenerator.Columns("strClass").DisplayIndex = i
                dgvQueryGenerator.Columns("strClass").Width = "75"
                i += 1
            End If
            If chbSICCode.Checked = True Then
                dgvQueryGenerator.Columns("strSICCode").HeaderText = "SIC"
                dgvQueryGenerator.Columns("strSICCode").DisplayIndex = i
                dgvQueryGenerator.Columns("strSICCode").Width = "50"
                i += 1
            End If
            If chbNAICSCode.Checked = True Then
                dgvQueryGenerator.Columns("strNAICSCode").HeaderText = "NAICS"
                dgvQueryGenerator.Columns("strNAICSCode").DisplayIndex = i
                dgvQueryGenerator.Columns("strNAICSCode").Width = "50"
                i += 1
            End If
            If chbStartUpDate.Checked = True Then
                dgvQueryGenerator.Columns("datStartUpDate").HeaderText = "Start Up Date"
                dgvQueryGenerator.Columns("datStartUpDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvQueryGenerator.Columns("datStartUpDate").DisplayIndex = i
                dgvQueryGenerator.Columns("datStartUpDate").Width = "90"
                i += 1
            End If
            If chbShutDownDate.Checked = True Then
                dgvQueryGenerator.Columns("datShutDownDate").HeaderText = "Permit Revocation Date"
                dgvQueryGenerator.Columns("datShutDownDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvQueryGenerator.Columns("datShutDownDate").DisplayIndex = i
                dgvQueryGenerator.Columns("datShutDownDate").Width = "90"
                i += 1
            End If
            If chbLastFCE.Checked = True Then
                dgvQueryGenerator.Columns("LastFCE").HeaderText = "Last FCE"
                dgvQueryGenerator.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvQueryGenerator.Columns("LastFCE").DisplayIndex = i
                dgvQueryGenerator.Columns("LastFCE").Width = "90"
                i += 1
            End If
            If chbCMSUniverse.Checked = True Then
                dgvQueryGenerator.Columns("strCMSMember").HeaderText = "CMS"
                dgvQueryGenerator.Columns("strCMSMember").DisplayIndex = i
                dgvQueryGenerator.Columns("strCMSMember").Width = "50"
                i += 1
            End If
            If chbPlantDescription.Checked = True Then
                dgvQueryGenerator.Columns("strPlantDescription").HeaderText = "Plant Description"
                dgvQueryGenerator.Columns("strPlantDescription").DisplayIndex = i
                dgvQueryGenerator.Columns("strPlantDescription").Width = "200"
                i += 1
            End If
            If chbAttainmentStatus.Checked = True Then
                dgvQueryGenerator.Columns("OneHRYes").HeaderText = "One HR Yes"
                dgvQueryGenerator.Columns("OneHRYes").DisplayIndex = i
                dgvQueryGenerator.Columns("OneHRYes").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("OneHRContribute").HeaderText = "One Hr Contributing"
                dgvQueryGenerator.Columns("OneHRContribute").DisplayIndex = i
                dgvQueryGenerator.Columns("OneHRContribute").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("OneHRNo").HeaderText = "One Hr No"
                dgvQueryGenerator.Columns("OneHRNo").DisplayIndex = i
                dgvQueryGenerator.Columns("OneHRNo").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("EightHRAtlanta").HeaderText = "8-Hr Atlanta"
                dgvQueryGenerator.Columns("EightHRAtlanta").DisplayIndex = i
                dgvQueryGenerator.Columns("EightHRAtlanta").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("EightHRMacon").HeaderText = "8-Hr Macon"
                dgvQueryGenerator.Columns("EightHRMacon").DisplayIndex = i
                dgvQueryGenerator.Columns("EightHRMacon").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("EightHRNo").HeaderText = "8-Hr No"
                dgvQueryGenerator.Columns("EightHRNo").DisplayIndex = i
                dgvQueryGenerator.Columns("EightHRNo").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("PMAtlanta").HeaderText = "PM Atlanta"
                dgvQueryGenerator.Columns("PMAtlanta").DisplayIndex = i
                dgvQueryGenerator.Columns("PMAtlanta").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("PMChattanooga").HeaderText = "PM Chattanooga"
                dgvQueryGenerator.Columns("PMChattanooga").DisplayIndex = i
                dgvQueryGenerator.Columns("PMChattanooga").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("PMMacon").HeaderText = "PM Macon"
                dgvQueryGenerator.Columns("PMMacon").DisplayIndex = i
                dgvQueryGenerator.Columns("PMMacon").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("PMFloyd").HeaderText = "PM Floyd"
                dgvQueryGenerator.Columns("PMFloyd").DisplayIndex = i
                dgvQueryGenerator.Columns("PMFloyd").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("PMNo").HeaderText = "PM No"
                dgvQueryGenerator.Columns("PMNo").DisplayIndex = i
                dgvQueryGenerator.Columns("PMNo").Width = "100"
                i += 1
            Else
                If chb1HrYes.Checked = True Then
                    dgvQueryGenerator.Columns("OneHRYes").HeaderText = "One HR Yes"
                    dgvQueryGenerator.Columns("OneHRYes").DisplayIndex = i
                    dgvQueryGenerator.Columns("OneHRYes").Width = "100"
                    i += 1
                End If
                If chb1HrContribute.Checked = True Then
                    dgvQueryGenerator.Columns("OneHRContribute").HeaderText = "One Hr Contributing"
                    dgvQueryGenerator.Columns("OneHRContribute").DisplayIndex = i
                    dgvQueryGenerator.Columns("OneHRContribute").Width = "100"
                    i += 1
                End If
                If chb1HrNo.Checked = True Then
                    dgvQueryGenerator.Columns("OneHRNo").HeaderText = "One Hr No"
                    dgvQueryGenerator.Columns("OneHRNo").DisplayIndex = i
                    dgvQueryGenerator.Columns("OneHRNo").Width = "100"
                    i += 1
                End If
                If chb8HrAtlanta.Checked = True Then
                    dgvQueryGenerator.Columns("EightHRAtlanta").HeaderText = "8-Hr Atlanta"
                    dgvQueryGenerator.Columns("EightHRAtlanta").DisplayIndex = i
                    dgvQueryGenerator.Columns("EightHRAtlanta").Width = "100"
                    i += 1
                End If
                If chb8HrMacon.Checked = True Then
                    dgvQueryGenerator.Columns("EightHRMacon").HeaderText = "8-Hr Macon"
                    dgvQueryGenerator.Columns("EightHRMacon").DisplayIndex = i
                    dgvQueryGenerator.Columns("EightHRMacon").Width = "100"
                    i += 1
                End If
                If chb8HrNo.Checked = True Then
                    dgvQueryGenerator.Columns("EightHRNo").HeaderText = "8-Hr No"
                    dgvQueryGenerator.Columns("EightHRNo").DisplayIndex = i
                    dgvQueryGenerator.Columns("EightHRNo").Width = "100"
                    i += 1
                End If
                If chbPMAtlanta.Checked = True Then
                    dgvQueryGenerator.Columns("PMAtlanta").HeaderText = "PM Atlanta"
                    dgvQueryGenerator.Columns("PMAtlanta").DisplayIndex = i
                    dgvQueryGenerator.Columns("PMAtlanta").Width = "100"
                    i += 1
                End If
                If chbPMChattanooga.Checked = True Then
                    dgvQueryGenerator.Columns("PMChattanooga").HeaderText = "PM Chattanooga"
                    dgvQueryGenerator.Columns("PMChattanooga").DisplayIndex = i
                    dgvQueryGenerator.Columns("PMChattanooga").Width = "100"
                    i += 1
                End If
                If chbPMFloyd.Checked = True Then
                    dgvQueryGenerator.Columns("PMFloyd").HeaderText = "PM Floyd"
                    dgvQueryGenerator.Columns("PMFloyd").DisplayIndex = i
                    dgvQueryGenerator.Columns("PMFloyd").Width = "100"
                    i += 1
                End If
                If chbPMMacon.Checked = True Then
                    dgvQueryGenerator.Columns("PMMacon").HeaderText = "PM Macon"
                    dgvQueryGenerator.Columns("PMMacon").DisplayIndex = i
                    dgvQueryGenerator.Columns("PMMacon").Width = "100"
                    i += 1
                End If
                If chbPMNo.Checked = True Then
                    dgvQueryGenerator.Columns("PMNo").HeaderText = "PM No"
                    dgvQueryGenerator.Columns("PMNo").DisplayIndex = i
                    dgvQueryGenerator.Columns("PMNo").Width = "100"
                    i += 1
                End If
            End If
            If chbStateProgramCodes.Checked = True Then
                dgvQueryGenerator.Columns("NSRPSD").HeaderText = "NSR/PSD"
                dgvQueryGenerator.Columns("NSRPSD").DisplayIndex = i
                dgvQueryGenerator.Columns("NSRPSD").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("HAP").HeaderText = "HAPs"
                dgvQueryGenerator.Columns("HAP").DisplayIndex = i
                dgvQueryGenerator.Columns("HAP").Width = "100"
                i += 1
            Else
                If chbNSRPSDMajor.Checked = True Then
                    dgvQueryGenerator.Columns("NSRPSD").HeaderText = "NSR/PSD"
                    dgvQueryGenerator.Columns("NSRPSD").DisplayIndex = i
                    dgvQueryGenerator.Columns("NSRPSD").Width = "100"
                    i += 1
                End If
                If chbHAPMajor.Checked = True Then
                    dgvQueryGenerator.Columns("HAP").HeaderText = "HAPs"
                    dgvQueryGenerator.Columns("HAP").DisplayIndex = i
                    dgvQueryGenerator.Columns("HAP").Width = "100"
                    i += 1
                End If
            End If
            If chbViewAirPrograms.Checked = True Then
                dgvQueryGenerator.Columns("APC0").HeaderText = "0 - SIP"
                dgvQueryGenerator.Columns("APC0").DisplayIndex = i
                dgvQueryGenerator.Columns("APC0").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC1").HeaderText = "1 - Federal SIP"
                dgvQueryGenerator.Columns("APC1").DisplayIndex = i
                dgvQueryGenerator.Columns("APC1").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC3").HeaderText = "3 - Non-Federal SIP"
                dgvQueryGenerator.Columns("APC3").DisplayIndex = i
                dgvQueryGenerator.Columns("APC3").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC4").HeaderText = "4 - CFC Tracking"
                dgvQueryGenerator.Columns("APC4").DisplayIndex = i
                dgvQueryGenerator.Columns("APC4").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC6").HeaderText = "6 - PSD"
                dgvQueryGenerator.Columns("APC6").DisplayIndex = i
                dgvQueryGenerator.Columns("APC6").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC7").HeaderText = "7 - NSR"
                dgvQueryGenerator.Columns("APC7").DisplayIndex = i
                dgvQueryGenerator.Columns("APC7").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC8").HeaderText = "8 - NESHAP"
                dgvQueryGenerator.Columns("APC8").DisplayIndex = i
                dgvQueryGenerator.Columns("APC8").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APC9").HeaderText = "9 - NSPS"
                dgvQueryGenerator.Columns("APC9").DisplayIndex = i
                dgvQueryGenerator.Columns("APC9").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APCA").HeaderText = "A - Acid Precipitation"
                dgvQueryGenerator.Columns("APCA").DisplayIndex = i
                dgvQueryGenerator.Columns("APCA").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APCF").HeaderText = "F - FESOP"
                dgvQueryGenerator.Columns("APCF").DisplayIndex = i
                dgvQueryGenerator.Columns("APCF").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APCI").HeaderText = "I - Native American"
                dgvQueryGenerator.Columns("APCI").DisplayIndex = i
                dgvQueryGenerator.Columns("APCI").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APCM").HeaderText = "M - MACT"
                dgvQueryGenerator.Columns("APCM").DisplayIndex = i
                dgvQueryGenerator.Columns("APCM").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("APCV").HeaderText = "V - Title V"
                dgvQueryGenerator.Columns("APCV").DisplayIndex = i
                dgvQueryGenerator.Columns("APCV").Width = "100"
                i += 1
            Else
                If chbAPC0.Checked = True Then
                    dgvQueryGenerator.Columns("APC0").HeaderText = "0 - SIP"
                    dgvQueryGenerator.Columns("APC0").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC0").Width = "100"
                    i += 1
                End If
                If chbAPC1.Checked = True Then
                    dgvQueryGenerator.Columns("APC1").HeaderText = "1 - Federal SIP"
                    dgvQueryGenerator.Columns("APC1").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC1").Width = "100"
                    i += 1
                End If
                If chbAPC3.Checked = True Then
                    dgvQueryGenerator.Columns("APC3").HeaderText = "3 - Non-Federal SIP"
                    dgvQueryGenerator.Columns("APC3").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC3").Width = "100"
                    i += 1
                End If
                If chbAPC4.Checked = True Then
                    dgvQueryGenerator.Columns("APC4").HeaderText = "4 - CFC Tracking"
                    dgvQueryGenerator.Columns("APC4").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC4").Width = "100"
                    i += 1
                End If
                If chbAPC6.Checked = True Then
                    dgvQueryGenerator.Columns("APC6").HeaderText = "6 - PSD"
                    dgvQueryGenerator.Columns("APC6").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC6").Width = "100"
                    i += 1
                End If
                If chbAPC7.Checked = True Then
                    dgvQueryGenerator.Columns("APC7").HeaderText = "7 - NSR"
                    dgvQueryGenerator.Columns("APC7").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC7").Width = "100"
                    i += 1
                End If
                If chbAPC8.Checked = True Then
                    dgvQueryGenerator.Columns("APC8").HeaderText = "8 - NESHAP"
                    dgvQueryGenerator.Columns("APC8").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC8").Width = "100"
                    i += 1
                End If
                If chbAPC9.Checked = True Then
                    dgvQueryGenerator.Columns("APC9").HeaderText = "9 - NSPS"
                    dgvQueryGenerator.Columns("APC9").DisplayIndex = i
                    dgvQueryGenerator.Columns("APC9").Width = "100"
                    i += 1
                End If
                If chbAPCA.Checked = True Then
                    dgvQueryGenerator.Columns("APCA").HeaderText = "A - Acid Precipitation"
                    dgvQueryGenerator.Columns("APCA").DisplayIndex = i
                    dgvQueryGenerator.Columns("APCA").Width = "100"
                    i += 1
                End If
                If chbAPCF.Checked = True Then
                    dgvQueryGenerator.Columns("APCF").HeaderText = "F - FESOP"
                    dgvQueryGenerator.Columns("APCF").DisplayIndex = i
                    dgvQueryGenerator.Columns("APCF").Width = "100"
                    i += 1
                End If
                If chbAPCI.Checked = True Then
                    dgvQueryGenerator.Columns("APCI").HeaderText = "I - Native American"
                    dgvQueryGenerator.Columns("APCI").DisplayIndex = i
                    dgvQueryGenerator.Columns("strFacilityZipCode").Width = "100"
                    i += 1
                End If
                If chbAPCM.Checked = True Then
                    dgvQueryGenerator.Columns("APCM").HeaderText = "M - MACT"
                    dgvQueryGenerator.Columns("APCM").DisplayIndex = i
                    dgvQueryGenerator.Columns("APCM").Width = "100"
                    i += 1
                End If
                If chbAPCV.Checked = True Then
                    dgvQueryGenerator.Columns("APCV").HeaderText = "V - Title V"
                    dgvQueryGenerator.Columns("APCV").DisplayIndex = i
                    dgvQueryGenerator.Columns("APCV").Width = "100"
                    i += 1
                End If
            End If
            If chbAllSubparts.Checked = True Then
                dgvQueryGenerator.Columns("GASIP").HeaderText = "GA SIP"
                dgvQueryGenerator.Columns("GASIP").DisplayIndex = i
                dgvQueryGenerator.Columns("GASIP").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("Part61").HeaderText = "NESHAP"
                dgvQueryGenerator.Columns("Part61").DisplayIndex = i
                dgvQueryGenerator.Columns("Part61").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("Part60").HeaderText = "NSPS"
                dgvQueryGenerator.Columns("Part60").DisplayIndex = i
                dgvQueryGenerator.Columns("Part60").Width = "100"
                i += 1
                dgvQueryGenerator.Columns("Part63").HeaderText = "MACT"
                dgvQueryGenerator.Columns("Part63").DisplayIndex = i
                dgvQueryGenerator.Columns("Part63").Width = "100"
                i += 1
            Else
                If chbSIP.Checked = True Then
                    dgvQueryGenerator.Columns("GASIP").HeaderText = "GA SIP"
                    dgvQueryGenerator.Columns("GASIP").DisplayIndex = i
                    dgvQueryGenerator.Columns("GASIP").Width = "100"
                    i += 1
                End If
                If chbPart61Subpart.Checked = True Then
                    dgvQueryGenerator.Columns("Part61").HeaderText = "NESHAP"
                    dgvQueryGenerator.Columns("Part61").DisplayIndex = i
                    dgvQueryGenerator.Columns("Part61").Width = "100"
                    i += 1
                End If
                If chbPart60Subpart.Checked = True Then
                    dgvQueryGenerator.Columns("Part60").HeaderText = "NSPS"
                    dgvQueryGenerator.Columns("Part60").DisplayIndex = i
                    dgvQueryGenerator.Columns("Part60").Width = "100"
                    i += 1
                End If
                If chbPart63Subpart.Checked = True Then
                    dgvQueryGenerator.Columns("Part63").HeaderText = "MACT"
                    dgvQueryGenerator.Columns("Part63").DisplayIndex = i
                    dgvQueryGenerator.Columns("Part63").Width = "100"
                    i += 1
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".GenerateSQL2")
        Finally
        End Try
    End Sub
    Sub RunSQL()
        Try
            dsSQLQuery = New DataSet

            daSQLQuery = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daSQLQuery.Fill(dsSQLQuery, "SQLQuery")
            dgvQueryGenerator.DataSource = dsSQLQuery
            dgvQueryGenerator.DataMember = "SQLQuery"

            dgvQueryGenerator.RowHeadersVisible = False
            dgvQueryGenerator.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvQueryGenerator.AllowUserToResizeColumns = True
            dgvQueryGenerator.AllowUserToAddRows = False
            dgvQueryGenerator.AllowUserToDeleteRows = False
            dgvQueryGenerator.AllowUserToOrderColumns = True
            dgvQueryGenerator.AllowUserToResizeRows = True
            dgvQueryGenerator.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvQueryGenerator.Columns("strAIRSNumber").DisplayIndex = 0
            dgvQueryGenerator.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvQueryGenerator.Columns("strFacilityName").DisplayIndex = 1

            If chbFacilityStreet1.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityStreet1").HeaderText = "Street Address 1"
                If txtFacilityStreet1Order.Text <> "" Then
                    If CInt(txtFacilityStreet1Order.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strFacilityStreet1").DisplayIndex = txtFacilityStreet1Order.Text
                    End If
                End If
            End If
            If chbFacilityStreet2.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityStreet2").HeaderText = "Street Address 2"
                If txtFacilityStreet2Order.Text <> "" Then
                    If CInt(txtFacilityStreet2Order.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strFacilityStreet2").DisplayIndex = txtFacilityStreet2Order.Text
                    End If
                End If
            End If
            If chbFacilityCity.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityCity").HeaderText = "City"
                If txtFacilityCityOrder.Text <> "" Then
                    If CInt(txtFacilityCityOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strFacilityCity").DisplayIndex = txtFacilityCityOrder.Text
                    End If
                End If
            End If
            If chbFacilityZipCode.Checked = True Then
                dgvQueryGenerator.Columns("strFacilityZipCode").HeaderText = "Zip Code"
                If txtFacilityZipCodeOrder.Text <> "" Then
                    If CInt(txtFacilityZipCodeOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strFacilityZipCode").DisplayIndex = txtFacilityZipCodeOrder.Text
                    End If
                End If
            End If
            If chbFacilityLatitude.Checked = True Then
                dgvQueryGenerator.Columns("numFacilityLatitude").HeaderText = "Latitude"
                If txtFacilityLatitudeOrder.Text <> "" Then
                    If CInt(txtFacilityLatitudeOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("numFacilityLatitude").DisplayIndex = txtFacilityLatitudeOrder.Text
                    End If
                End If
            End If
            If chbFacilityLongitude.Checked = True Then
                dgvQueryGenerator.Columns("numFacilityLongitude").HeaderText = "Longitude"
                If txtFacilityLongitudeOrder.Text <> "" Then
                    If CInt(txtFacilityLongitudeOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("numFacilityLongitude").DisplayIndex = txtFacilityLongitudeOrder.Text
                    End If
                End If
            End If
            If chbCounty.Checked = True Then
                dgvQueryGenerator.Columns("strCountyName").HeaderText = "County"
                If txtCountyOrder.Text <> "" Then
                    If CInt(txtCountyOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strCountyName").DisplayIndex = txtCountyOrder.Text
                    End If
                End If
            End If
            If chbSSCPEngineer.Checked = True Then
                dgvQueryGenerator.Columns("SSCPEngineer").HeaderText = "Complinace Engineer"
                If txtSSCPEngineerOrder.Text <> "" Then
                    If CInt(txtSSCPEngineerOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("SSCPEngineer").DisplayIndex = txtSSCPEngineerOrder.Text
                    End If
                End If
            End If
            If chbDistrict.Checked = True Then
                dgvQueryGenerator.Columns("strDistrictName").HeaderText = "District"
                If txtDistrictOrder.Text <> "" Then
                    If CInt(txtDistrictOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strDistrictName").DisplayIndex = txtDistrictOrder.Text
                    End If
                End If
            End If
            If chbOperationStatus.Checked = True Then
                dgvQueryGenerator.Columns("strOperationalStatus").HeaderText = "Operation Status"
                If txtOperationStatusOrder.Text <> "" Then
                    If CInt(txtOperationStatusOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strOperationalStatus").DisplayIndex = txtOperationStatusOrder.Text
                    End If
                End If
            End If
            If chbClassification.Checked = True Then
                dgvQueryGenerator.Columns("strClass").HeaderText = "Class"
                If txtClassificationOrder.Text <> "" Then
                    If CInt(txtClassificationOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strClass").DisplayIndex = txtClassificationOrder.Text
                    End If
                End If
            End If
            If chbSICCode.Checked = True Then
                dgvQueryGenerator.Columns("strSICCode").HeaderText = "SIC Code"
                If txtSICCodeOrder.Text <> "" Then
                    If CInt(txtSICCodeOrder.Text) <= dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strSICCode").DisplayIndex = txtSICCodeOrder.Text
                    End If
                End If
            End If
            If chbNAICSCode.Checked = True Then
                dgvQueryGenerator.Columns("strNAICSCode").HeaderText = "NAICS Code"
                If txtNAICSCodeOrder.Text <> "" Then
                    If CInt(txtNAICSCodeOrder.Text) <= dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strNAICSCode").DisplayIndex = txtNAICSCodeOrder.Text
                    End If
                End If
            End If
            If chbStartUpDate.Checked = True Then
                dgvQueryGenerator.Columns("datStartUpDate").HeaderText = "Start Up Date"
                If txtStartUpDateOrder.Text <> "" Then
                    If CInt(txtStartUpDateOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("datStartUpDate").DisplayIndex = txtStartUpDateOrder.Text
                    End If
                End If
            End If
            If chbShutDownDate.Checked = True Then
                dgvQueryGenerator.Columns("datShutDownDate").HeaderText = "Permit Revocation Date"
                If txtShutDownDateOrder.Text <> "" Then
                    If CInt(txtShutDownDateOrder.Text) <= dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("datShutDownDate").DisplayIndex = txtShutDownDateOrder.Text
                    End If
                End If
            End If
            If chbCMSUniverse.Checked = True Then
                dgvQueryGenerator.Columns("strCMSMember").HeaderText = "CMS Status"
                If txtCMSUniverseOrder.Text <> "" Then
                    If CInt(txtCMSUniverseOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strCMSMember").DisplayIndex = txtCMSUniverseOrder.Text
                    End If
                End If
            End If
            If chbPlantDescription.Checked = True Then
                dgvQueryGenerator.Columns("strPlantDescription").HeaderText = "Plant Description"
                If txtPlantDescriptionOrder.Text <> "" Then
                    If CInt(txtPlantDescriptionOrder.Text) < dgvQueryGenerator.Columns.Count Then
                        dgvQueryGenerator.Columns("strPlantDescription").DisplayIndex = txtPlantDescriptionOrder.Text
                    End If
                End If
            End If

            If Me.chbAttainmentStatus.Checked = True Then
                dgvQueryGenerator.Columns("OneHrYes").HeaderText = "1-Hr Yes"
                dgvQueryGenerator.Columns("OneHrContribute").HeaderText = "1-Hr Contribute"
                dgvQueryGenerator.Columns("OneHrNo").HeaderText = "1-Hr No"
                dgvQueryGenerator.Columns("EightHrAtlanta").HeaderText = "8-Hr Atlanta"
                dgvQueryGenerator.Columns("EightHrMacon").HeaderText = "8-Hr Macon"
                dgvQueryGenerator.Columns("EightHrNo").HeaderText = "8-Hr No"
                dgvQueryGenerator.Columns("PMAtlanta").HeaderText = "PM-2.5 Atlanta"
                dgvQueryGenerator.Columns("PMChattanooga").HeaderText = "PM-2.5 Chattanooga"
                dgvQueryGenerator.Columns("PMFloyd").HeaderText = "PM-2.5 Floyd"
                dgvQueryGenerator.Columns("PMMacon").HeaderText = "PM-2.5 Macon"
                dgvQueryGenerator.Columns("PMNo").HeaderText = "PM-2.5 No"
            End If

            If chb1HrYes.Checked = True Then
                dgvQueryGenerator.Columns("OneHrYes").HeaderText = "1-Hr Yes"
            End If
            If chb1HrNo.Checked = True Then
                dgvQueryGenerator.Columns("OneHrNo").HeaderText = "1-Hr No"
            End If
            If chb1HrContribute.Checked = True Then
                dgvQueryGenerator.Columns("OneHrContribute").HeaderText = "1-Hr Contribute"
            End If
            If chb8HrAtlanta.Checked = True Then
                dgvQueryGenerator.Columns("EightHrAtlanta").HeaderText = "8-Hr Atlanta"
            End If
            If chb8HrMacon.Checked = True Then
                dgvQueryGenerator.Columns("EightHrMacon").HeaderText = "8-Hr Macon"
            End If
            If chb8HrNo.Checked = True Then
                dgvQueryGenerator.Columns("EightHrNo").HeaderText = "8-Hr No"
            End If
            If chbPMAtlanta.Checked = True Then
                dgvQueryGenerator.Columns("PMAtlanta").HeaderText = "PM-2.5 Atlanta"
            End If
            If chbPMChattanooga.Checked = True Then
                dgvQueryGenerator.Columns("PMChattanooga").HeaderText = "PM-2.5 Chattanooga"
            End If
            If chbPMFloyd.Checked = True Then
                dgvQueryGenerator.Columns("PMFloyd").HeaderText = "PM-2.5 Floyd"
            End If
            If chbPMMacon.Checked = True Then
                dgvQueryGenerator.Columns("PMMacon").HeaderText = "PM-2.5 Macon"
            End If
            If chbPMNo.Checked = True Then
                dgvQueryGenerator.Columns("PMNo").HeaderText = "PM-2.5 No"
            End If

            If chbStateProgramCodes.Checked = True Then
                dgvQueryGenerator.Columns("NSRPSD").HeaderText = "NSR/PSD Major"
                dgvQueryGenerator.Columns("HAP").HeaderText = "HAPs Major"
            End If

            If chbNSRPSDMajor.Checked = True Then
                dgvQueryGenerator.Columns("NSRPSD").HeaderText = "NSR/PSD Major"
            End If
            If chbHAPMajor.Checked = True Then
                dgvQueryGenerator.Columns("HAP").HeaderText = "HAPs Major"
            End If

            If chbViewAirPrograms.Checked = True Then
                dgvQueryGenerator.Columns("APC0").HeaderText = "SIP"
                dgvQueryGenerator.Columns("APC1").HeaderText = "Federal SIP"
                dgvQueryGenerator.Columns("APC3").HeaderText = "Non-Federal SIP"
                dgvQueryGenerator.Columns("APC4").HeaderText = "CFC Tracking"
                dgvQueryGenerator.Columns("APC6").HeaderText = "PSD"
                dgvQueryGenerator.Columns("APC7").HeaderText = "NSR"
                dgvQueryGenerator.Columns("APC8").HeaderText = "NESHAP"
                dgvQueryGenerator.Columns("APC9").HeaderText = "NSPS"
                dgvQueryGenerator.Columns("APCA").HeaderText = "Acid Rain"
                dgvQueryGenerator.Columns("APCF").HeaderText = "FESOP"
                dgvQueryGenerator.Columns("APCI").HeaderText = "Native American"
                dgvQueryGenerator.Columns("APCM").HeaderText = "MACT"
                dgvQueryGenerator.Columns("APCV").HeaderText = "Title V"
            Else

                If chbAPC0.Checked = True Then
                    dgvQueryGenerator.Columns("APC0").HeaderText = "SIP"
                    If txtAPC0Order.Text <> "" Then
                        If CInt(txtAPC0Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC0").DisplayIndex = txtAPC0Order.Text
                        End If
                    End If
                End If
                If chbAPC1.Checked = True Then
                    dgvQueryGenerator.Columns("APC1").HeaderText = "Federal SIP"
                    If txtAPC1Order.Text <> "" Then
                        If CInt(txtAPC1Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC1").DisplayIndex = txtAPC1Order.Text
                        End If
                    End If
                End If
                If chbAPC3.Checked = True Then
                    dgvQueryGenerator.Columns("APC3").HeaderText = "Non-Federal SIP"
                    If txtAPC3Order.Text <> "" Then
                        If CInt(txtAPC3Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC3").DisplayIndex = txtAPC3Order.Text
                        End If
                    End If
                End If
                If chbAPC4.Checked = True Then
                    dgvQueryGenerator.Columns("APC4").HeaderText = "CFC Tracking"
                    If txtAPC4Order.Text <> "" Then
                        If CInt(txtAPC4Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC4").DisplayIndex = txtAPC4Order.Text
                        End If
                    End If
                End If
                If chbAPC6.Checked = True Then
                    dgvQueryGenerator.Columns("APC6").HeaderText = "PSD"
                    If txtAPC6Order.Text <> "" Then
                        If CInt(txtAPC6Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC6").DisplayIndex = CInt(txtAPC6Order.Text)
                        End If
                    End If
                End If
                If chbAPC7.Checked = True Then
                    dgvQueryGenerator.Columns("APC7").HeaderText = "NSR"
                    If txtAPC7Order.Text <> "" Then
                        If CInt(txtAPC7Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC7").DisplayIndex = txtAPC7Order.Text
                        End If
                    End If
                End If
                If chbAPC8.Checked = True Then
                    dgvQueryGenerator.Columns("APC8").HeaderText = "NESHAP"
                    If txtAPC8Order.Text <> "" Then
                        If CInt(txtAPC8Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC8").DisplayIndex = txtAPC8Order.Text
                        End If
                    End If
                End If
                If chbAPC9.Checked = True Then
                    dgvQueryGenerator.Columns("APC9").HeaderText = "NSPS"
                    If txtAPC9Order.Text <> "" Then
                        If CInt(txtAPC9Order.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APC9").DisplayIndex = txtAPC9Order.Text
                        End If
                    End If
                End If
                If chbAPCA.Checked = True Then
                    dgvQueryGenerator.Columns("APCA").HeaderText = "Acid Rain"
                    If txtAPCAOrder.Text <> "" Then
                        If CInt(txtAPCAOrder.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APCA").DisplayIndex = txtAPCAOrder.Text
                        End If
                    End If
                End If
                If chbAPCF.Checked = True Then
                    dgvQueryGenerator.Columns("APCF").HeaderText = "FESOP"
                    If txtAPCFOrder.Text <> "" Then
                        If CInt(txtAPCFOrder.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APCF").DisplayIndex = txtAPCFOrder.Text
                        End If
                    End If
                End If
                If chbAPCI.Checked = True Then
                    dgvQueryGenerator.Columns("APCI").HeaderText = "Native American"
                    If txtAPCIOrder.Text <> "" Then
                        If CInt(txtAPCIOrder.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APCI").DisplayIndex = txtAPCIOrder.Text
                        End If
                    End If
                End If
                If chbAPCM.Checked = True Then
                    dgvQueryGenerator.Columns("APCM").HeaderText = "MACT"
                    If txtAPCMOrder.Text <> "" Then
                        If CInt(txtAPCMOrder.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APCM").DisplayIndex = txtAPCMOrder.Text
                        End If
                    End If
                End If
                If chbAPCV.Checked = True Then
                    dgvQueryGenerator.Columns("APCV").HeaderText = "Title V"
                    If txtAPCVOrder.Text <> "" Then
                        If CInt(txtAPCVOrder.Text) < dgvQueryGenerator.Columns.Count Then
                            dgvQueryGenerator.Columns("APCV").DisplayIndex = CInt(txtAPCVOrder.Text)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(txtSQLStatement.Text & vbCrLf & ex.ToString(), Me.Name & ".RunSQL")
        Finally

        End Try
    End Sub
    Sub ExportToExcel()
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvQueryGenerator.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvQueryGenerator.ColumnCount - 1
                        .Cells(1, i + 1) = dgvQueryGenerator.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvQueryGenerator.ColumnCount - 1
                        For j = 0 To dgvQueryGenerator.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvQueryGenerator.Item(i, j).Value.ToString
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
                ErrorReport(txtSQLStatement.Text & vbCrLf & ex.ToString(), Me.Name & ".ExportToExcel")
            End If
        Finally

        End Try
    End Sub
    Sub ResetForm()
        Try
            chbAIRSNumber.Checked = True
            txtAIRSNumberSearch1.Clear()
            txtAIRSNumberSearch2.Clear()
            rdbAIRSNumberOr.Checked = True
            rdbAIRSNumberEqual.Checked = True
            txtFacilityAIRSNumberOrder.Clear()

            chbFacilityName.Checked = True
            txtFacilityNameSearch1.Clear()
            txtFacilityNameSearch2.Clear()
            rdbFacilityNameOr.Checked = True
            rdbFacilityNameEqual.Checked = True
            txtFacilityNameOrder.Clear()

            chbFacilityStreet1.Checked = False
            txtFacilityStreet1Search1.Clear()
            txtFacilityStreet1Search2.Clear()
            rdbFacilityStreet1Or.Checked = True
            rdbFacilityStreet1Equal.Checked = True
            txtFacilityStreet1Order.Clear()

            chbFacilityStreet2.Checked = False
            txtFacilityStreet2Search1.Clear()
            txtFacilityStreet2Search2.Clear()
            rdbFacilityStreet2Or.Checked = True
            rdbFacilityStreet2Equal.Checked = True
            txtFacilityStreet2Order.Clear()

            chbFacilityCity.Checked = False
            txtFacilityCitySearch1.Clear()
            txtFacilityCitySearch2.Clear()
            rdbFacilityCityOr.Checked = True
            rdbFacilityCityEqual.Checked = True
            txtFacilityCityOrder.Clear()

            chbFacilityZipCode.Checked = False
            txtFacilityZipCodeSearch1.Clear()
            txtFacilityZipCodeSearch2.Clear()
            rdbFacilityZipCodeOr.Checked = True
            rdbFacilityZipCodeEqual.Checked = True
            txtFacilityZipCodeOrder.Clear()

            chbFacilityLatitude.Checked = False
            txtFacilityLatitudeSearch1.Clear()
            txtFacilityLatitudeSearch2.Clear()
            rdbFacilityLatitudeBetween.Checked = True
            txtFacilityLatitudeOrder.Clear()

            chbFacilityLongitude.Checked = False
            txtFacilityLongitudeSearch1.Clear()
            txtFacilityLongitudeSearch2.Clear()
            rdbFacilityLongitudeBetween.Checked = True
            txtFacilityLongitudeOrder.Clear()

            chbCounty.Checked = False
            cboCountySearch1.SelectedIndex = 0
            cboCountySearch2.SelectedIndex = 0
            rdbCountyOr.Checked = True
            rdbCountyEqual.Checked = True
            txtCountyOrder.Clear()

            chbDistrict.Checked = False
            cboDistrictSearch1.SelectedIndex = 0
            cboDistrictSearch2.SelectedIndex = 0
            rdbDistrictOr.Checked = True
            rdbDistrictEqual.Checked = True
            txtDistrictOrder.Clear()

            chbOperationStatus.Checked = False
            cboOperationStatusSearch1.SelectedIndex = 0
            cboOperationStatusSearch2.SelectedIndex = 0
            rdbOperationalStatusOr.Checked = True
            rdbOperationStatusEqual.Checked = True
            txtOperationStatusOrder.Clear()

            chbClassification.Checked = False
            cboClassificationSearch1.SelectedIndex = 0
            cboClassificationSearch2.SelectedIndex = 0
            rdbClassificationOr.Checked = True
            rdbClassificationEqual.Checked = True
            txtClassificationOrder.Clear()

            chbSICCode.Checked = False
            txtSICCodeSearch1.Clear()
            txtSICCodeSearch2.Clear()
            rdbSICCodeOr.Checked = True
            rdbSICCodeEqual.Checked = True
            txtSICCodeOrder.Clear()

            chbNAICSCode.Checked = False
            txtNAICSCodeSearch1.Clear()
            txtNAICSCodeSearch2.Clear()
            rdbNAICSCodeOr.Checked = True
            rdbNAICSCodeEqual.Checked = True
            txtNAICSCodeOrder.Clear()

            chbStartUpDate.Checked = False
            DTPStartUpDateSearch1.Checked = False
            DTPStartUpDateSearch1.Text = OracleDate
            DTPStartUpDateSearch2.Checked = False
            DTPStartUpDateSearch2.Text = OracleDate
            rdbStartUpDateBetween.Checked = True
            txtStartUpDateOrder.Clear()

            chbShutDownDate.Checked = False
            DTPShutDownDateSearch1.Checked = False
            DTPShutDownDateSearch1.Text = OracleDate
            DTPShutDownDateSearch2.Checked = False
            DTPShutDownDateSearch2.Text = OracleDate
            rdbShutDownDateBetween.Checked = True
            txtShutDownDateOrder.Clear()

            chbCMSUniverse.Checked = False
            cboCMSUniverseSearch1.SelectedIndex = 0
            cboCMSUniverseSearch2.SelectedIndex = 0
            rdbCMSUniverseOR.Checked = True
            rdbCMSUniverseEqual.Checked = True
            txtCMSUniverseOrder.Clear()

            chbPlantDescription.Checked = False
            txtPlantDescriptionSearch1.Clear()
            txtPlantDescriptionSearch2.Clear()
            rdbPlantDescriptionOR.Checked = True
            rdbPlantDescriptionEqual.Checked = True
            txtPlantDescriptionOrder.Clear()

            chbAttainmentStatus.Checked = False
            chb1HrYes.Checked = False
            chb1HrNo.Checked = False
            chb1HrContribute.Checked = False
            chb8HrAtlanta.Checked = False
            chb8HrMacon.Checked = False
            chb8HrNo.Checked = False
            chbPMAtlanta.Checked = False
            chbPMChattanooga.Checked = False
            chbPMFloyd.Checked = False
            chbPMMacon.Checked = False
            chbPMNo.Checked = False
            chbStateProgramCodes.Checked = False
            chbNSRPSDMajor.Checked = False
            chbHAPMajor.Checked = False
            chbViewAirPrograms.Checked = False
            chbAPC0.Checked = False
            chbAPC1.Checked = False
            chbAPC3.Checked = False
            chbAPC4.Checked = False
            chbAPC6.Checked = False
            chbAPC7.Checked = False
            chbAPC8.Checked = False
            chbAPC9.Checked = False
            chbAPCA.Checked = False
            chbAPCF.Checked = False
            chbAPCI.Checked = False
            chbAPCM.Checked = False
            chbAPCV.Checked = False
            txtAPC0Order.Clear()
            txtAPC1Order.Clear()
            txtAPC3Order.Clear()
            txtAPC4Order.Clear()
            txtAPC6Order.Clear()
            txtAPC7Order.Clear()
            txtAPC8Order.Clear()
            txtAPC9Order.Clear()
            txtAPCAOrder.Clear()
            txtAPCFOrder.Clear()
            txtAPCIOrder.Clear()
            txtAPCMOrder.Clear()
            txtAPCVOrder.Clear()

            chbAllSubparts.Checked = False
            chbSIP.Checked = False
            chbPart61Subpart.Checked = False
            chbPart60Subpart.Checked = False
            chbPart63Subpart.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".ResetForm")
        Finally

        End Try
    End Sub
    Sub ResizeFilter()
        Try

            If TCQuerryOptions.Size.Height > 27 Then
                TCQuerryOptions.Size = New Drawing.Size(TCQuerryOptions.Size.Width, 27)
            Else
                TCQuerryOptions.Size = New Drawing.Size(TCQuerryOptions.Size.Width, 389)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".ResizeFilter")
        Finally

        End Try
    End Sub
    Sub UpdateDefaultSearch()
        Dim DefaultsText As String = ""
        'Dim UpdatedText As String = ""
        Dim CheckBoxes As String = ""
        Dim SearchText As String = ""
        Dim BooleanText As String = ""
        Dim SearchOrder As String = ""

        Try
            CheckBoxes = ""
            SearchText = ""
            BooleanText = ""
            SearchOrder = "AIRSNumber#xxxx#%xxxx%*And*@Equal@^3^rebmuNSRIA"

            If Me.chbAIRSNumber.Checked = True Then
                DefaultsText = DefaultsText & "AIRSNumber"
                If txtAIRSNumberSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtAIRSNumberSearch1.Text & "-#"
                End If
                If txtAIRSNumberSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtAIRSNumberSearch2.Text & "-%"
                End If
                If rdbAIRSNumberAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbAIRSNumberEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityAIRSNumberOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityAIRSNumberOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "rebmuNSRIA"
            End If
            If Me.chbFacilityName.Checked = True Then
                DefaultsText = DefaultsText & "FacilityName"
                If txtFacilityNameSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityNameSearch1.Text & "-#"
                End If
                If txtFacilityNameSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityNameSearch2.Text & "-%"
                End If
                If rdbFacilityNameAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityNameEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityNameOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityNameOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "emaNytilicaF"
            End If
            If Me.chbFacilityStreet1.Checked = True Then
                DefaultsText = DefaultsText & "Street1"
                If txtFacilityStreet1Search1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityStreet1Search1.Text & "-#"
                End If
                If txtFacilityStreet1Search2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityStreet1Search2.Text & "-%"
                End If
                If rdbFacilityStreet1And.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityStreet1Equal.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityStreet1Order.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityStreet1Order.Text & "-^"
                End If
                DefaultsText = DefaultsText & "1teertS"
            End If
            If Me.chbFacilityStreet2.Checked = True Then
                DefaultsText = DefaultsText & "Street2"
                If txtFacilityStreet2Search1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityStreet2Search1.Text & "-#"
                End If
                If txtFacilityStreet2Search2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityStreet2Search2.Text & "-%"
                End If
                If rdbFacilityStreet2And.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityStreet2Equal.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityStreet2Order.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityStreet2Order.Text & "-^"
                End If
                DefaultsText = DefaultsText & "2teertS"
            End If
            If Me.chbFacilityCity.Checked = True Then
                DefaultsText = DefaultsText & "City"
                If txtFacilityCitySearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityCitySearch1.Text & "-#"
                End If
                If txtFacilityCitySearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityCitySearch2.Text & "-%"
                End If
                If rdbFacilityCityAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityCityEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityCityOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityCityOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "ytiC"
            End If
            If Me.chbFacilityZipCode.Checked = True Then
                DefaultsText = DefaultsText & "ZipCode"
                If txtFacilityZipCodeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityZipCodeSearch1.Text & "-#"
                End If
                If txtFacilityZipCodeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityZipCodeSearch2.Text & "-%"
                End If
                If rdbFacilityZipCodeAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityZipCodeEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityZipCodeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityZipCodeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "edoCpiZ"
            End If
            If Me.chbFacilityLongitude.Checked = True Then
                DefaultsText = DefaultsText & "Longitude"
                If txtFacilityLongitudeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityLongitudeSearch1.Text & "-#"
                End If
                If txtFacilityLongitudeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityLongitudeSearch2.Text & "-%"
                End If
                If txtFacilityLongitudeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityLongitudeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "edutignoL"
            End If
            If Me.chbFacilityLatitude.Checked = True Then
                DefaultsText = DefaultsText & "Latitude"
                If txtFacilityLatitudeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityLatitudeSearch1.Text & "-#"
                End If
                If txtFacilityLatitudeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityLatitudeSearch2.Text & "-%"
                End If
                If txtFacilityLatitudeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityLatitudeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "edutitaL"
            End If
            If Me.chbCounty.Checked = True Then
                DefaultsText = DefaultsText & "County"
                If cboCountySearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboCountySearch1.Text & "-#"
                End If
                If cboCountySearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboCountySearch2.Text & "-%"
                End If
                If rdbCountyAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbCountyEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtCountyOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtCountyOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "ytnuoC"
            End If
            If Me.chbDistrict.Checked = True Then
                DefaultsText = DefaultsText & "District"
                If cboDistrictSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboDistrictSearch1.Text & "-#"
                End If
                If cboDistrictSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboDistrictSearch2.Text & "-%"
                End If
                If rdbDistrictAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbDistrictEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtDistrictOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtDistrictOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "tcirtsiD"
            End If
            If Me.chbOperationStatus.Checked = True Then
                DefaultsText = DefaultsText & "OpStatus"
                If cboOperationStatusSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboOperationStatusSearch1.Text & "-#"
                End If
                If cboOperationStatusSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboOperationStatusSearch2.Text & "-%"
                End If
                If rdbOperationalStatusAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbOperationStatusEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtOperationStatusOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtOperationStatusOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "sutatSpO"
            End If
            If Me.chbClassification.Checked = True Then
                DefaultsText = DefaultsText & "Classification"
                If cboClassificationSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboClassificationSearch1.Text & "-#"
                End If
                If cboClassificationSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboClassificationSearch2.Text & "-%"
                End If
                If rdbClassificationAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbClassificationEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtClassificationOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtClassificationOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "noitacifissalC"
            End If
            If Me.chbSICCode.Checked = True Then
                DefaultsText = DefaultsText & "SIC"
                If txtSICCodeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtSICCodeSearch1.Text & "-#"
                End If
                If txtSICCodeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtSICCodeSearch2.Text & "-%"
                End If
                If rdbSICCodeAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbSICCodeEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtSICCodeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtSICCodeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "CIS"
            End If
            If Me.chbNAICSCode.Checked = True Then
                DefaultsText = DefaultsText & "NAICS"
                If txtNAICSCodeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtNAICSCodeSearch1.Text & "-#"
                End If
                If txtNAICSCodeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtNAICSCodeSearch2.Text & "-%"
                End If
                If rdbNAICSCodeAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbNAICSCodeEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtNAICSCodeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtNAICSCodeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "SCIAN"
            End If
            If Me.chbStartUpDate.Checked = True Then
                DefaultsText = DefaultsText & "StartUp"
                If DTPStartUpDateSearch1.Checked = True Then
                    DefaultsText = DefaultsText & "#-" & DTPStartUpDateSearch1.Text & "-#"
                End If
                If DTPStartUpDateSearch2.Checked = True Then
                    DefaultsText = DefaultsText & "%-" & DTPStartUpDateSearch2.Text & "-%"
                End If
                If rdbStartUpDateBetween.Checked = True Then
                    DefaultsText = DefaultsText & "*-BETWEEN-*"
                End If
                If txtStartUpDateOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtStartUpDateOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "pUtratS"
            End If
            If Me.chbShutDownDate.Checked = True Then
                DefaultsText = DefaultsText & "ShutDown"
                If DTPShutDownDateSearch1.Checked = True Then
                    DefaultsText = DefaultsText & "#-" & DTPShutDownDateSearch1.Text & "-#"
                End If
                If DTPShutDownDateSearch2.Checked = True Then
                    DefaultsText = DefaultsText & "%-" & DTPShutDownDateSearch2.Text & "-%"
                End If
                If rdbShutDownDateBetween.Checked = True Then
                    DefaultsText = DefaultsText & "*-BETWEEN-*"
                End If
                If txtShutDownDateOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtShutDownDateOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "nwoDtuhS"
            End If
            If Me.chbCMSUniverse.Checked = True Then
                DefaultsText = DefaultsText & "CMS"
                If cboCMSUniverseSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboCMSUniverseSearch1.Text & "-#"
                End If
                If cboCMSUniverseSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboCMSUniverseSearch2.Text & "-%"
                End If
                If rdbCMSUniverseAnd.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbCMSUniverseEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtCMSUniverseOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtCMSUniverseOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "SMC"
            End If
            If Me.chbPlantDescription.Checked = True Then
                DefaultsText = DefaultsText & "Plant"
                If txtPlantDescriptionSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtPlantDescriptionSearch1.Text & "-#"
                End If
                If txtPlantDescriptionSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtPlantDescriptionSearch2.Text & "-%"
                End If
                If rdbPlantDescriptionAND.Checked = True Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbPlantDescriptionEqual.Checked = True Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtPlantDescriptionOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtPlantDescriptionOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "tnalP"
            End If

            If System.IO.Directory.Exists("C:\APB\SQL") Then
            Else
                System.IO.Directory.CreateDirectory("C:\APB\SQL")
            End If

            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"

            path.InitialDirectory = "C:\APB\SQL"
            path.DefaultExt = ".txt"

            If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                DestFilePath = path.FileName.ToString
            Else
                DestFilePath = "N/A"
            End If

            If DestFilePath <> "N/A" Then
                Dim Encoder As New System.Text.ASCIIEncoding

                Dim bytedata As Byte() = Encoder.GetBytes(DefaultsText)

                Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                fs.Write(bytedata, 0, bytedata.Length)
                fs.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".UpdateDefaultSearch")
        Finally

        End Try



    End Sub
    Sub LoadDefaults()
        Dim DefaultsText As String = ""
        Dim AIRSNumber As String = ""
        Dim FacilityName As String = ""
        Dim FacilityStreet1 As String = ""
        Dim FacilityStreet2 As String = ""
        Dim FacilityCity As String = ""
        Dim FacilityZipCode As String = ""
        Dim Longitude As String = ""
        Dim Latitude As String = ""
        Dim County As String = ""
        Dim District As String = ""
        Dim OperationStatus As String = ""
        Dim Classification As String = ""
        Dim SICCode As String = ""
        Dim NAICSCode As String = ""
        Dim StartUpDate As String = ""
        Dim ShutDownDate As String = ""
        Dim CMSUniverse As String = ""
        Dim PlantDesc As String = ""

        Try


            If System.IO.Directory.Exists("C:\APB\SQL") Then
            Else
                System.IO.Directory.CreateDirectory("C:\APB\SQL")
            End If

            'Dim path As New SaveFileDialog
            Dim path As New OpenFileDialog
            Dim DestFilePath As String = "N/A"

            path.InitialDirectory = "C:\APB\SQL"
            path.DefaultExt = ".txt"

            If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                DestFilePath = path.FileName.ToString
            Else
                DestFilePath = "N/A"
            End If

            If DestFilePath <> "N/A" Then
                If File.Exists(DestFilePath) Then
                    Dim reader As StreamReader = New StreamReader(DestFilePath)
                    Do
                        DefaultsText = DefaultsText & reader.ReadLine
                    Loop Until reader.Peek = -1
                    reader.Close()

                    If DefaultsText <> "" Then
                        If DefaultsText.IndexOf("AIRSNumber") <> -1 Then
                            AIRSNumber = Mid(DefaultsText, DefaultsText.IndexOf("AIRSNumber") + 1, (DefaultsText.IndexOf("rebmuNSRIA") - DefaultsText.IndexOf("AIRSNumber") + 10))
                            Me.chbAIRSNumber.Checked = True
                            If AIRSNumber.IndexOf("#-") <> -1 Then
                                txtAIRSNumberSearch1.Text = Mid(AIRSNumber, (AIRSNumber.IndexOf("#-") + 3), (AIRSNumber.IndexOf("-#") - (AIRSNumber.IndexOf("#-") + 2)))
                            End If
                            If AIRSNumber.IndexOf("%-") <> -1 Then
                                txtAIRSNumberSearch2.Text = Mid(AIRSNumber, (AIRSNumber.IndexOf("%-") + 3), (AIRSNumber.IndexOf("-%") - (AIRSNumber.IndexOf("%-") + 2)))
                            End If
                            If AIRSNumber.IndexOf("*-") <> -1 Then
                                If Mid(AIRSNumber, AIRSNumber.IndexOf("*-") + 3, (AIRSNumber.IndexOf("-*") - (AIRSNumber.IndexOf("*-") + 2))) = "OR" Then
                                    rdbAIRSNumberOr.Checked = True
                                Else
                                    rdbAIRSNumberAnd.Checked = True
                                End If
                            End If
                            If AIRSNumber.IndexOf("@-") <> -1 Then
                                If Mid(AIRSNumber, AIRSNumber.IndexOf("@-") + 3, (AIRSNumber.IndexOf("-@") - (AIRSNumber.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbAIRSNumberEqual.Checked = True
                                Else
                                    rdbAIRSNumberNotEqual.Checked = True
                                End If
                            End If
                            If AIRSNumber.IndexOf("^-") <> -1 Then
                                txtFacilityAIRSNumberOrder.Text = Mid(AIRSNumber, AIRSNumber.IndexOf("^-") + 3, (AIRSNumber.IndexOf("-^") - (AIRSNumber.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("FacilityName") <> -1 Then
                            FacilityName = Mid(DefaultsText, DefaultsText.IndexOf("FacilityName") + 1, (DefaultsText.IndexOf("emaNytilicaF") - DefaultsText.IndexOf("FacilityName") + 12))
                            Me.chbFacilityName.Checked = True
                            If FacilityName.IndexOf("#-") <> -1 Then
                                txtFacilityNameSearch1.Text = Mid(FacilityName, (FacilityName.IndexOf("#-") + 3), (FacilityName.IndexOf("-#") - (FacilityName.IndexOf("#-") + 2)))
                            End If
                            If FacilityName.IndexOf("%-") <> -1 Then
                                txtFacilityNameSearch2.Text = Mid(FacilityName, (FacilityName.IndexOf("%-") + 3), (FacilityName.IndexOf("-%") - (FacilityName.IndexOf("%-") + 2)))
                            End If
                            If FacilityName.IndexOf("*-") <> -1 Then
                                If Mid(FacilityName, FacilityName.IndexOf("*-") + 3, (FacilityName.IndexOf("-*") - (FacilityName.IndexOf("*-") + 2))) = "OR" Then
                                    rdbFacilityNameOr.Checked = True
                                Else
                                    rdbFacilityNameAnd.Checked = True
                                End If
                            End If
                            If FacilityName.IndexOf("@-") <> -1 Then
                                If Mid(FacilityName, FacilityName.IndexOf("@-") + 3, (FacilityName.IndexOf("-@") - (FacilityName.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbFacilityNameEqual.Checked = True
                                Else
                                    rdbFacilityNameNotEqual.Checked = True
                                End If
                            End If
                            If FacilityName.IndexOf("^-") <> -1 Then
                                txtFacilityNameOrder.Text = Mid(FacilityName, FacilityName.IndexOf("^-") + 3, (FacilityName.IndexOf("-^") - (FacilityName.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("Street1") <> -1 Then
                            FacilityStreet1 = Mid(DefaultsText, DefaultsText.IndexOf("Street1") + 1, (DefaultsText.IndexOf("1teertS") - DefaultsText.IndexOf("Street1") + 7))
                            Me.chbFacilityStreet1.Checked = True
                            If FacilityStreet1.IndexOf("#-") <> -1 Then
                                txtFacilityStreet1Search1.Text = Mid(FacilityStreet1, (FacilityStreet1.IndexOf("#-") + 3), (FacilityStreet1.IndexOf("-#") - (FacilityStreet1.IndexOf("#-") + 2)))
                            End If
                            If FacilityStreet1.IndexOf("%-") <> -1 Then
                                txtFacilityStreet1Search2.Text = Mid(FacilityStreet1, (FacilityStreet1.IndexOf("%-") + 3), (FacilityStreet1.IndexOf("-%") - (FacilityStreet1.IndexOf("%-") + 2)))
                            End If
                            If FacilityStreet1.IndexOf("*-") <> -1 Then
                                If Mid(FacilityStreet1, FacilityStreet1.IndexOf("*-") + 3, (FacilityStreet1.IndexOf("-*") - (FacilityStreet1.IndexOf("*-") + 2))) = "OR" Then
                                    rdbFacilityStreet1Or.Checked = True
                                Else
                                    rdbFacilityStreet1And.Checked = True
                                End If
                            End If
                            If FacilityStreet1.IndexOf("@-") <> -1 Then
                                If Mid(FacilityStreet1, FacilityStreet1.IndexOf("@-") + 3, (FacilityStreet1.IndexOf("-@") - (FacilityStreet1.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbFacilityStreet1Equal.Checked = True
                                Else
                                    rdbFacilityStreet1NotEqual.Checked = True
                                End If
                            End If
                            If FacilityStreet1.IndexOf("^-") <> -1 Then
                                txtFacilityStreet1Order.Text = Mid(FacilityStreet1, FacilityStreet1.IndexOf("^-") + 3, (FacilityStreet1.IndexOf("-^") - (FacilityStreet1.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("Street2") <> -1 Then
                            FacilityStreet2 = Mid(DefaultsText, DefaultsText.IndexOf("Street2") + 1, (DefaultsText.IndexOf("2teertS") - DefaultsText.IndexOf("Street2") + 7))
                            Me.chbFacilityStreet2.Checked = True
                            If FacilityStreet2.IndexOf("#-") <> -1 Then
                                txtFacilityStreet2Search1.Text = Mid(FacilityStreet2, (FacilityStreet2.IndexOf("#-") + 3), (FacilityStreet2.IndexOf("-#") - (FacilityStreet2.IndexOf("#-") + 2)))
                            End If
                            If FacilityStreet2.IndexOf("%-") <> -1 Then
                                txtFacilityStreet2Search2.Text = Mid(FacilityStreet2, (FacilityStreet2.IndexOf("%-") + 3), (FacilityStreet2.IndexOf("-%") - (FacilityStreet2.IndexOf("%-") + 2)))
                            End If
                            If FacilityStreet2.IndexOf("*-") <> -1 Then
                                If Mid(FacilityStreet2, FacilityStreet2.IndexOf("*-") + 3, (FacilityStreet2.IndexOf("-*") - (FacilityStreet2.IndexOf("*-") + 2))) = "OR" Then
                                    rdbFacilityStreet2Or.Checked = True
                                Else
                                    rdbFacilityStreet2And.Checked = True
                                End If
                            End If
                            If FacilityStreet2.IndexOf("@-") <> -1 Then
                                If Mid(FacilityStreet2, FacilityStreet2.IndexOf("@-") + 3, (FacilityStreet2.IndexOf("-@") - (FacilityStreet2.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbFacilityStreet2Equal.Checked = True
                                Else
                                    rdbFacilityStreet2NotEqual.Checked = True
                                End If
                            End If
                            If FacilityStreet2.IndexOf("^-") <> -1 Then
                                txtFacilityStreet2Order.Text = Mid(FacilityStreet2, FacilityStreet2.IndexOf("^-") + 3, (FacilityStreet2.IndexOf("-^") - (FacilityStreet2.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("City") <> -1 Then
                            FacilityCity = Mid(DefaultsText, DefaultsText.IndexOf("City") + 1, (DefaultsText.IndexOf("ytiC") - DefaultsText.IndexOf("City") + 4))
                            Me.chbFacilityCity.Checked = True
                            If FacilityCity.IndexOf("#-") <> -1 Then
                                txtFacilityCitySearch1.Text = Mid(FacilityCity, (FacilityCity.IndexOf("#-") + 3), (FacilityCity.IndexOf("-#") - (FacilityCity.IndexOf("#-") + 2)))
                            End If
                            If FacilityCity.IndexOf("%-") <> -1 Then
                                txtFacilityCitySearch2.Text = Mid(FacilityCity, (FacilityCity.IndexOf("%-") + 3), (FacilityCity.IndexOf("-%") - (FacilityCity.IndexOf("%-") + 2)))
                            End If
                            If FacilityCity.IndexOf("*-") <> -1 Then
                                If Mid(FacilityCity, FacilityCity.IndexOf("*-") + 3, (FacilityCity.IndexOf("-*") - (FacilityCity.IndexOf("*-") + 2))) = "OR" Then
                                    rdbFacilityCityOr.Checked = True
                                Else
                                    rdbFacilityCityAnd.Checked = True
                                End If
                            End If
                            If FacilityCity.IndexOf("@-") <> -1 Then
                                If Mid(FacilityCity, FacilityCity.IndexOf("@-") + 3, (FacilityCity.IndexOf("-@") - (FacilityCity.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbFacilityCityEqual.Checked = True
                                Else
                                    rdbFacilityCityNotEqual.Checked = True
                                End If
                            End If
                            If FacilityCity.IndexOf("^-") <> -1 Then
                                txtFacilityCityOrder.Text = Mid(FacilityCity, FacilityCity.IndexOf("^-") + 3, (FacilityCity.IndexOf("-^") - (FacilityCity.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("ZipCode") <> -1 Then
                            FacilityZipCode = Mid(DefaultsText, DefaultsText.IndexOf("ZipCode") + 1, (DefaultsText.IndexOf("edoCpiZ") - DefaultsText.IndexOf("ZipCode") + 7))
                            Me.chbFacilityZipCode.Checked = True
                            If FacilityZipCode.IndexOf("#-") <> -1 Then
                                txtFacilityZipCodeSearch1.Text = Mid(FacilityZipCode, (FacilityZipCode.IndexOf("#-") + 3), (FacilityZipCode.IndexOf("-#") - (FacilityZipCode.IndexOf("#-") + 2)))
                            End If
                            If FacilityZipCode.IndexOf("%-") <> -1 Then
                                txtFacilityZipCodeSearch2.Text = Mid(FacilityZipCode, (FacilityZipCode.IndexOf("%-") + 3), (FacilityZipCode.IndexOf("-%") - (FacilityZipCode.IndexOf("%-") + 2)))
                            End If
                            If FacilityZipCode.IndexOf("*-") <> -1 Then
                                If Mid(FacilityZipCode, FacilityZipCode.IndexOf("*-") + 3, (FacilityZipCode.IndexOf("-*") - (FacilityZipCode.IndexOf("*-") + 2))) = "OR" Then
                                    rdbFacilityZipCodeOr.Checked = True
                                Else
                                    rdbFacilityZipCodeAnd.Checked = True
                                End If
                            End If
                            If FacilityZipCode.IndexOf("@-") <> -1 Then
                                If Mid(FacilityZipCode, FacilityZipCode.IndexOf("@-") + 3, (FacilityZipCode.IndexOf("-@") - (FacilityZipCode.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbFacilityZipCodeEqual.Checked = True
                                Else
                                    rdbFacilityZipCodeNotEqual.Checked = True
                                End If
                            End If
                            If FacilityZipCode.IndexOf("^-") <> -1 Then
                                txtFacilityZipCodeOrder.Text = Mid(FacilityZipCode, FacilityZipCode.IndexOf("^-") + 3, (FacilityZipCode.IndexOf("-^") - (FacilityZipCode.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("Longitude") <> -1 Then
                            Longitude = Mid(DefaultsText, DefaultsText.IndexOf("Longitude") + 1, (DefaultsText.IndexOf("edutignoL") - DefaultsText.IndexOf("Longitude") + 9))
                            Me.chbFacilityLongitude.Checked = True
                            If Longitude.IndexOf("#-") <> -1 Then
                                txtFacilityLongitudeSearch1.Text = Mid(Longitude, (Longitude.IndexOf("#-") + 3), (Longitude.IndexOf("-#") - (Longitude.IndexOf("#-") + 2)))
                            End If
                            If Longitude.IndexOf("%-") <> -1 Then
                                txtFacilityLongitudeSearch2.Text = Mid(Longitude, (Longitude.IndexOf("%-") + 3), (Longitude.IndexOf("-%") - (Longitude.IndexOf("%-") + 2)))
                            End If
                            If Longitude.IndexOf("^-") <> -1 Then
                                txtFacilityLongitudeOrder.Text = Mid(Longitude, Longitude.IndexOf("^-") + 3, (Longitude.IndexOf("-^") - (Longitude.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("Latitude") <> -1 Then
                            Latitude = Mid(DefaultsText, DefaultsText.IndexOf("Latitude") + 1, (DefaultsText.IndexOf("edutitaL") - DefaultsText.IndexOf("Latitude") + 8))
                            Me.chbFacilityLatitude.Checked = True
                            If Latitude.IndexOf("#-") <> -1 Then
                                txtFacilityLatitudeSearch1.Text = Mid(Latitude, (Latitude.IndexOf("#-") + 3), (Latitude.IndexOf("-#") - (Latitude.IndexOf("#-") + 2)))
                            End If
                            If Latitude.IndexOf("%-") <> -1 Then
                                txtFacilityLatitudeSearch2.Text = Mid(Latitude, (Latitude.IndexOf("%-") + 3), (Latitude.IndexOf("-%") - (Latitude.IndexOf("%-") + 2)))
                            End If
                            If Latitude.IndexOf("^-") <> -1 Then
                                txtFacilityLatitudeOrder.Text = Mid(Latitude, Latitude.IndexOf("^-") + 3, (Latitude.IndexOf("-^") - (Latitude.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("County") <> -1 Then
                            County = Mid(DefaultsText, DefaultsText.IndexOf("County") + 1, (DefaultsText.IndexOf("ytnuoC") - DefaultsText.IndexOf("County") + 6))
                            Me.chbCounty.Checked = True
                            If County.IndexOf("#-") <> -1 Then
                                cboCountySearch1.Text = Mid(County, (County.IndexOf("#-") + 3), (County.IndexOf("-#") - (County.IndexOf("#-") + 2)))
                            End If
                            If County.IndexOf("%-") <> -1 Then
                                cboCountySearch2.Text = Mid(County, (County.IndexOf("%-") + 3), (County.IndexOf("-%") - (County.IndexOf("%-") + 2)))
                            End If
                            If County.IndexOf("*-") <> -1 Then
                                If Mid(County, County.IndexOf("*-") + 3, (County.IndexOf("-*") - (County.IndexOf("*-") + 2))) = "OR" Then
                                    rdbCountyOr.Checked = True
                                Else
                                    rdbCountyAnd.Checked = True
                                End If
                            End If
                            If County.IndexOf("@-") <> -1 Then
                                If Mid(County, County.IndexOf("@-") + 3, (County.IndexOf("-@") - (County.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbCountyEqual.Checked = True
                                Else
                                    rdbCountyNotEqual.Checked = True
                                End If
                            End If
                            If County.IndexOf("^-") <> -1 Then
                                txtCountyOrder.Text = Mid(County, County.IndexOf("^-") + 3, (County.IndexOf("-^") - (County.IndexOf("^-") + 2)))
                            End If
                        End If

                        If DefaultsText.IndexOf("District") <> -1 Then
                            District = Mid(DefaultsText, DefaultsText.IndexOf("District") + 1, (DefaultsText.IndexOf("tcirtsiD") - DefaultsText.IndexOf("District") + 8))
                            Me.chbDistrict.Checked = True
                            If District.IndexOf("#-") <> -1 Then
                                cboDistrictSearch1.Text = Mid(District, (District.IndexOf("#-") + 3), (District.IndexOf("-#") - (District.IndexOf("#-") + 2)))
                            End If
                            If District.IndexOf("%-") <> -1 Then
                                cboDistrictSearch2.Text = Mid(District, (District.IndexOf("%-") + 3), (District.IndexOf("-%") - (District.IndexOf("%-") + 2)))
                            End If
                            If District.IndexOf("*-") <> -1 Then
                                If Mid(District, District.IndexOf("*-") + 3, (District.IndexOf("-*") - (District.IndexOf("*-") + 2))) = "OR" Then
                                    rdbDistrictOr.Checked = True
                                Else
                                    rdbDistrictAnd.Checked = True
                                End If
                            End If
                            If District.IndexOf("@-") <> -1 Then
                                If Mid(District, District.IndexOf("@-") + 3, (District.IndexOf("-@") - (District.IndexOf("@-") + 2))) = "EQUAL" Then
                                    rdbDistrictEqual.Checked = True
                                Else
                                    rdbDistrictNotEqual.Checked = True
                                End If
                            End If

                            If District.IndexOf("^-") <> -1 Then
                                txtDistrictOrder.Text = Mid(District, District.IndexOf("^-") + 3, (District.IndexOf("-^") - (District.IndexOf("^-") + 2)))
                            End If
                        End If
                    End If

                    If DefaultsText.IndexOf("OpStatus") <> -1 Then
                        OperationStatus = Mid(DefaultsText, DefaultsText.IndexOf("OpStatus") + 1, (DefaultsText.IndexOf("sutatSpO") - DefaultsText.IndexOf("OpStatus") + 8))
                        Me.chbOperationStatus.Checked = True
                        If OperationStatus.IndexOf("#-") <> -1 Then
                            cboOperationStatusSearch1.Text = Mid(OperationStatus, (OperationStatus.IndexOf("#-") + 3), (OperationStatus.IndexOf("-#") - (OperationStatus.IndexOf("#-") + 2)))
                        End If
                        If OperationStatus.IndexOf("%-") <> -1 Then
                            cboOperationStatusSearch2.Text = Mid(OperationStatus, (OperationStatus.IndexOf("%-") + 3), (OperationStatus.IndexOf("-%") - (OperationStatus.IndexOf("%-") + 2)))
                        End If
                        If OperationStatus.IndexOf("*-") <> -1 Then
                            If Mid(OperationStatus, OperationStatus.IndexOf("*-") + 3, (OperationStatus.IndexOf("-*") - (OperationStatus.IndexOf("*-") + 2))) = "OR" Then
                                rdbOperationalStatusOr.Checked = True
                            Else
                                rdbOperationalStatusAnd.Checked = True
                            End If
                        End If
                        If OperationStatus.IndexOf("@-") <> -1 Then
                            If Mid(OperationStatus, OperationStatus.IndexOf("@-") + 3, (OperationStatus.IndexOf("-@") - (OperationStatus.IndexOf("@-") + 2))) = "EQUAL" Then
                                rdbOperationStatusEqual.Checked = True
                            Else
                                rdbOperationStatusNotEqual.Checked = True
                            End If
                        End If
                        If OperationStatus.IndexOf("^-") <> -1 Then
                            txtOperationStatusOrder.Text = Mid(OperationStatus, OperationStatus.IndexOf("^-") + 3, (OperationStatus.IndexOf("-^") - (OperationStatus.IndexOf("^-") + 2)))
                        End If
                    End If

                    If DefaultsText.IndexOf("Classification") <> -1 Then
                        Classification = Mid(DefaultsText, DefaultsText.IndexOf("Classification") + 1, (DefaultsText.IndexOf("noitacifissalC") - DefaultsText.IndexOf("Classification") + 14))
                        Me.chbClassification.Checked = True
                        If Classification.IndexOf("#-") <> -1 Then
                            cboClassificationSearch1.Text = Mid(Classification, (Classification.IndexOf("#-") + 3), (Classification.IndexOf("-#") - (Classification.IndexOf("#-") + 2)))
                        End If
                        If Classification.IndexOf("%-") <> -1 Then
                            cboClassificationSearch2.Text = Mid(Classification, (Classification.IndexOf("%-") + 3), (Classification.IndexOf("-%") - (Classification.IndexOf("%-") + 2)))
                        End If
                        If Classification.IndexOf("*-") <> -1 Then
                            If Mid(Classification, Classification.IndexOf("*-") + 3, (Classification.IndexOf("-*") - (Classification.IndexOf("*-") + 2))) = "OR" Then
                                rdbClassificationOr.Checked = True
                            Else
                                rdbClassificationAnd.Checked = True
                            End If
                        End If
                        If Classification.IndexOf("@-") <> -1 Then
                            If Mid(Classification, Classification.IndexOf("@-") + 3, (Classification.IndexOf("-@") - (Classification.IndexOf("@-") + 2))) = "EQUAL" Then
                                rdbClassificationEqual.Checked = True
                            Else
                                rdbClassificationNotEqual.Checked = True
                            End If
                        End If
                        If Classification.IndexOf("^-") <> -1 Then
                            txtClassificationOrder.Text = Mid(Classification, Classification.IndexOf("^-") + 3, (Classification.IndexOf("-^") - (Classification.IndexOf("^-") + 2)))
                        End If
                    End If

                    If DefaultsText.IndexOf("SIC") <> -1 Then
                        SICCode = Mid(DefaultsText, DefaultsText.IndexOf("SIC") + 1, (DefaultsText.IndexOf("CIS") - DefaultsText.IndexOf("SIC") + 3))
                        Me.chbSICCode.Checked = True
                        If SICCode.IndexOf("#-") <> -1 Then
                            txtSICCodeSearch1.Text = Mid(SICCode, (SICCode.IndexOf("#-") + 3), (SICCode.IndexOf("-#") - (SICCode.IndexOf("#-") + 2)))
                        End If
                        If SICCode.IndexOf("%-") <> -1 Then
                            txtSICCodeSearch2.Text = Mid(SICCode, (SICCode.IndexOf("%-") + 3), (SICCode.IndexOf("-%") - (SICCode.IndexOf("%-") + 2)))
                        End If
                        If SICCode.IndexOf("*-") <> -1 Then
                            If Mid(SICCode, SICCode.IndexOf("*-") + 3, (SICCode.IndexOf("-*") - (SICCode.IndexOf("*-") + 2))) = "OR" Then
                                rdbSICCodeOr.Checked = True
                            Else
                                rdbSICCodeAnd.Checked = True
                            End If
                        End If
                        If SICCode.IndexOf("@-") <> -1 Then
                            If Mid(SICCode, SICCode.IndexOf("@-") + 3, (SICCode.IndexOf("-@") - (SICCode.IndexOf("@-") + 2))) = "EQUAL" Then
                                rdbSICCodeEqual.Checked = True
                            Else
                                rdbSICCodeNotEqual.Checked = True
                            End If
                        End If
                        If SICCode.IndexOf("^-") <> -1 Then
                            txtSICCodeOrder.Text = Mid(SICCode, SICCode.IndexOf("^-") + 3, (SICCode.IndexOf("-^") - (SICCode.IndexOf("^-") + 2)))
                        End If
                    End If
                    If DefaultsText.IndexOf("NAICS") <> -1 Then
                        NAICSCode = Mid(DefaultsText, DefaultsText.IndexOf("NAICS") + 1, (DefaultsText.IndexOf("SCIAN") - DefaultsText.IndexOf("NAICS") + 3))
                        Me.chbNAICSCode.Checked = True
                        If NAICSCode.IndexOf("#-") <> -1 Then
                            txtNAICSCodeSearch1.Text = Mid(NAICSCode, (NAICSCode.IndexOf("#-") + 3), (NAICSCode.IndexOf("-#") - (NAICSCode.IndexOf("#-") + 2)))
                        End If
                        If NAICSCode.IndexOf("%-") <> -1 Then
                            txtNAICSCodeSearch2.Text = Mid(NAICSCode, (NAICSCode.IndexOf("%-") + 3), (NAICSCode.IndexOf("-%") - (NAICSCode.IndexOf("%-") + 2)))
                        End If
                        If NAICSCode.IndexOf("*-") <> -1 Then
                            If Mid(NAICSCode, NAICSCode.IndexOf("*-") + 3, (NAICSCode.IndexOf("-*") - (NAICSCode.IndexOf("*-") + 2))) = "OR" Then
                                rdbNAICSCodeOr.Checked = True
                            Else
                                rdbNAICSCodeAnd.Checked = True
                            End If
                        End If
                        If NAICSCode.IndexOf("@-") <> -1 Then
                            If Mid(NAICSCode, NAICSCode.IndexOf("@-") + 3, (NAICSCode.IndexOf("-@") - (NAICSCode.IndexOf("@-") + 2))) = "EQUAL" Then
                                rdbNAICSCodeEqual.Checked = True
                            Else
                                rdbNAICSCodeNotEqual.Checked = True
                            End If
                        End If
                        If NAICSCode.IndexOf("^-") <> -1 Then
                            txtNAICSCodeOrder.Text = Mid(NAICSCode, NAICSCode.IndexOf("^-") + 3, (NAICSCode.IndexOf("-^") - (NAICSCode.IndexOf("^-") + 2)))
                        End If
                    End If
                    If DefaultsText.IndexOf("StartUp") <> -1 Then
                        StartUpDate = Mid(DefaultsText, DefaultsText.IndexOf("StartUp") + 1, (DefaultsText.IndexOf("pUtratS") - DefaultsText.IndexOf("StartUp") + 7))
                        Me.chbStartUpDate.Checked = True
                        If StartUpDate.IndexOf("#-") <> -1 Then
                            DTPStartUpDateSearch1.Checked = True
                            DTPStartUpDateSearch1.Text = Mid(StartUpDate, (StartUpDate.IndexOf("#-") + 3), (StartUpDate.IndexOf("-#") - (StartUpDate.IndexOf("#-") + 2)))
                        End If
                        If StartUpDate.IndexOf("%-") <> -1 Then
                            DTPStartUpDateSearch2.Checked = True
                            DTPStartUpDateSearch2.Text = Mid(StartUpDate, (StartUpDate.IndexOf("%-") + 3), (StartUpDate.IndexOf("-%") - (StartUpDate.IndexOf("%-") + 2)))
                        End If
                        If StartUpDate.IndexOf("*-") <> -1 Then
                            If Mid(StartUpDate, StartUpDate.IndexOf("*-") + 3, (StartUpDate.IndexOf("-*") - (StartUpDate.IndexOf("*-") + 2))) = "Between" Then
                                rdbStartUpDateBetween.Checked = True
                            End If
                        End If
                        If StartUpDate.IndexOf("@-") <> -1 Then
                        End If
                        If StartUpDate.IndexOf("^-") <> -1 Then
                            txtStartUpDateOrder.Text = Mid(StartUpDate, StartUpDate.IndexOf("^-") + 3, (StartUpDate.IndexOf("-^") - (StartUpDate.IndexOf("^-") + 2)))
                        End If
                    End If
                    If DefaultsText.IndexOf("ShutDown") <> -1 Then
                        ShutDownDate = Mid(DefaultsText, DefaultsText.IndexOf("ShutDown") + 1, (DefaultsText.IndexOf("nwoDtuhS") - DefaultsText.IndexOf("ShutDown") + 8))
                        Me.chbShutDownDate.Checked = True
                        If ShutDownDate.IndexOf("#-") <> -1 Then
                            DTPShutDownDateSearch1.Checked = True
                            DTPShutDownDateSearch1.Text = Mid(ShutDownDate, (ShutDownDate.IndexOf("#-") + 3), (ShutDownDate.IndexOf("-#") - (ShutDownDate.IndexOf("#-") + 2)))
                        End If
                        If ShutDownDate.IndexOf("%-") <> -1 Then
                            DTPShutDownDateSearch2.Checked = True
                            DTPShutDownDateSearch2.Text = Mid(ShutDownDate, (ShutDownDate.IndexOf("%-") + 3), (ShutDownDate.IndexOf("-%") - (ShutDownDate.IndexOf("%-") + 2)))
                        End If
                        If ShutDownDate.IndexOf("*-") <> -1 Then
                            If Mid(ShutDownDate, ShutDownDate.IndexOf("*-") + 3, (ShutDownDate.IndexOf("-*") - (ShutDownDate.IndexOf("*-") + 2))) = "Between" Then
                                rdbShutDownDateBetween.Checked = True
                            End If
                        End If
                        If ShutDownDate.IndexOf("^-") <> -1 Then
                            txtShutDownDateOrder.Text = Mid(ShutDownDate, ShutDownDate.IndexOf("^-") + 3, (ShutDownDate.IndexOf("-^") - (ShutDownDate.IndexOf("^-") + 2)))
                        End If
                    End If

                    If DefaultsText.IndexOf("CMS") <> -1 Then
                        CMSUniverse = Mid(DefaultsText, DefaultsText.IndexOf("CMS") + 1, (DefaultsText.IndexOf("SMC") - DefaultsText.IndexOf("CMS") + 3))
                        Me.chbCMSUniverse.Checked = True
                        If CMSUniverse.IndexOf("#-") <> -1 Then
                            cboCMSUniverseSearch1.Text = Mid(CMSUniverse, (CMSUniverse.IndexOf("#-") + 3), (CMSUniverse.IndexOf("-#") - (CMSUniverse.IndexOf("#-") + 2)))
                        End If
                        If CMSUniverse.IndexOf("%-") <> -1 Then
                            cboCMSUniverseSearch2.Text = Mid(CMSUniverse, (CMSUniverse.IndexOf("%-") + 3), (CMSUniverse.IndexOf("-%") - (CMSUniverse.IndexOf("%-") + 2)))
                        End If
                        If CMSUniverse.IndexOf("*-") <> -1 Then
                            If Mid(CMSUniverse, CMSUniverse.IndexOf("*-") + 3, (CMSUniverse.IndexOf("-*") - (CMSUniverse.IndexOf("*-") + 2))) = "OR" Then
                                rdbCMSUniverseOR.Checked = True
                            Else
                                rdbCMSUniverseAnd.Checked = True
                            End If
                        End If
                        If CMSUniverse.IndexOf("@-") <> -1 Then
                            If Mid(CMSUniverse, CMSUniverse.IndexOf("@-") + 3, (CMSUniverse.IndexOf("-@") - (CMSUniverse.IndexOf("@-") + 2))) = "EQUAL" Then
                                rdbCMSUniverseEqual.Checked = True
                            Else
                                rdbCMSUniverseNotEqual.Checked = True
                            End If
                        End If
                        If CMSUniverse.IndexOf("^-") <> -1 Then
                            txtCMSUniverseOrder.Text = Mid(CMSUniverse, CMSUniverse.IndexOf("^-") + 3, (CMSUniverse.IndexOf("-^") - (CMSUniverse.IndexOf("^-") + 2)))
                        End If
                    End If

                    If DefaultsText.IndexOf("Plant") <> -1 Then
                        PlantDesc = Mid(DefaultsText, DefaultsText.IndexOf("Plant") + 1, (DefaultsText.IndexOf("tnalP") - DefaultsText.IndexOf("Plant") + 9))
                        Me.chbPlantDescription.Checked = True
                        If PlantDesc.IndexOf("#-") <> -1 Then
                            txtPlantDescriptionSearch1.Text = Mid(PlantDesc, (PlantDesc.IndexOf("#-") + 3), (PlantDesc.IndexOf("-#") - (PlantDesc.IndexOf("#-") + 2)))
                        End If
                        If PlantDesc.IndexOf("%-") <> -1 Then
                            txtPlantDescriptionSearch2.Text = Mid(PlantDesc, (PlantDesc.IndexOf("%-") + 3), (PlantDesc.IndexOf("-%") - (PlantDesc.IndexOf("%-") + 2)))
                        End If
                        If PlantDesc.IndexOf("*-") <> -1 Then
                            If Mid(PlantDesc, PlantDesc.IndexOf("*-") + 3, (PlantDesc.IndexOf("-*") - (PlantDesc.IndexOf("*-") + 2))) = "OR" Then
                                rdbPlantDescriptionOR.Checked = True
                            Else
                                rdbPlantDescriptionAND.Checked = True
                            End If
                        End If
                        If PlantDesc.IndexOf("@-") <> -1 Then
                            If Mid(PlantDesc, PlantDesc.IndexOf("@-") + 3, (PlantDesc.IndexOf("-@") - (PlantDesc.IndexOf("@-") + 2))) = "EQUAL" Then
                                rdbPlantDescriptionEqual.Checked = True
                            Else
                                rdbPlantDescriptionNotEqual.Checked = True
                            End If
                        End If
                        If PlantDesc.IndexOf("^-") <> -1 Then
                            txtPlantDescriptionOrder.Text = Mid(PlantDesc, PlantDesc.IndexOf("^-") + 3, (PlantDesc.IndexOf("-^") - (PlantDesc.IndexOf("^-") + 2)))
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".FindLogIn")
        Finally

        End Try
    End Sub
#End Region
#Region "Declarations"
    Private Sub btnRunSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunSearch.Click
        Try
            '  GenerateSQL()
            GenerateSQL2()
            If txtSQLStatement.Text <> "" Then
                'RunSQL()

                txtQueryCount.Text = dsSQLQuery.Tables(0).Rows.Count

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnRunSearch_Click")
        Finally

        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            ResetForm()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnReset_Click")
        Finally

        End Try
    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Me.Close()
    End Sub
    Private Sub tsbReSizeFilterOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbReSizeFilterOptions.Click
        Try

            ResizeFilter()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".tsbReSizeFilterOptions_Click")
        Finally

        End Try
    End Sub

#End Region
    Private Sub bgwQueryGenerator_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwQueryGenerator.DoWork
        Try
            LoadDataSets()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".bgwQueryGenerator_DoWork")
        Finally
        End Try
    End Sub
    Private Sub bgwQueryGenerator_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwQueryGenerator.RunWorkerCompleted
        Try
            LoadComboBoxes()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".bgwQueryGenerator_RunWorkerCompleted")
        Finally
        End Try
    End Sub

    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        Try
            ExportToExcel()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".tsbExport_Click")
        Finally
        End Try
    End Sub

    Private Sub tsbSearchQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSearchQuery.Click
        Try
            LoadDefaults()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".tsbSearchQuery_Click")
        Finally

        End Try
    End Sub

    Private Sub tsbSaveQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveQuery.Click
        Try
            UpdateDefaultSearch()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".tsbSaveQuery_Click")
        Finally

        End Try
    End Sub

    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try
            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnRunPermitContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunPermitContact.Click
        Try
            SQL = "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, strFacilityStreet1, " & _
            "strFacilityStreet2, strFacilityCity, " & _
            "strFacilityState, strFacilityZipCode, " & _
            "numFacilityLongitude, numFacilityLatitude, " & _
            "strOperationalStatus, strClass, " & _
            "strSICCode, strNAICSCode, " & _
            "strPlantDescription, ContactType, " & _
            "strContactFirstName, strContactLastName, " & _
            "strContactAddress1, strContactCity, " & _
            "strContactState, strContactZiPCode, " & _
            "strContactPhoneNumber1, strContactEmail, " & _
            "PermitNumber, IssuanceDate " & _
            "from AIRBranch.VW_Permit_Contact_Data " & _
            "order by strAIRSNumber "

            dsSQLQuery = New DataSet
            daSQLQuery = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daSQLQuery.Fill(dsSQLQuery, "SQLQuery")
            dgvQueryGenerator.DataSource = dsSQLQuery
            dgvQueryGenerator.DataMember = "SQLQuery"

            i = 0
            dgvQueryGenerator.RowHeadersVisible = False
            dgvQueryGenerator.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvQueryGenerator.AllowUserToResizeColumns = True
            dgvQueryGenerator.AllowUserToAddRows = False
            dgvQueryGenerator.AllowUserToDeleteRows = False
            dgvQueryGenerator.AllowUserToOrderColumns = True
            dgvQueryGenerator.AllowUserToResizeRows = True
            dgvQueryGenerator.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvQueryGenerator.Columns("AIRSNumber").DisplayIndex = 0
            dgvQueryGenerator.Columns("AIRSNumber").Width = "75"
            dgvQueryGenerator.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvQueryGenerator.Columns("strFacilityName").DisplayIndex = 1
            dgvQueryGenerator.Columns("strFacilityName").Width = "125"
            dgvQueryGenerator.Columns("strFacilityStreet1").HeaderText = "Facility Street"
            dgvQueryGenerator.Columns("strFacilityStreet1").DisplayIndex = 2
            dgvQueryGenerator.Columns("strFacilityStreet1").Width = "125"
            dgvQueryGenerator.Columns("strFacilityStreet2").HeaderText = "Facility Street 2"
            dgvQueryGenerator.Columns("strFacilityStreet2").DisplayIndex = 3
            dgvQueryGenerator.Columns("strFacilityStreet2").Width = "100"
            dgvQueryGenerator.Columns("strFacilityCity").HeaderText = "Facility City"
            dgvQueryGenerator.Columns("strFacilityCity").DisplayIndex = 4
            dgvQueryGenerator.Columns("strFacilityCity").Width = "75"
            dgvQueryGenerator.Columns("strFacilityState").HeaderText = "Facility State"
            dgvQueryGenerator.Columns("strFacilityState").DisplayIndex = 5
            dgvQueryGenerator.Columns("strFacilityState").Width = "75"
            dgvQueryGenerator.Columns("strFacilityZipCode").HeaderText = "Facility Zip Code"
            dgvQueryGenerator.Columns("strFacilityZipCode").DisplayIndex = 6
            dgvQueryGenerator.Columns("strFacilityZipCode").Width = "75"
            dgvQueryGenerator.Columns("numFacilityLongitude").HeaderText = "Longitude"
            dgvQueryGenerator.Columns("numFacilityLongitude").DisplayIndex = 7
            dgvQueryGenerator.Columns("numFacilityLongitude").Width = "75"
            dgvQueryGenerator.Columns("numFacilityLatitude").HeaderText = "Latitude"
            dgvQueryGenerator.Columns("numFacilityLatitude").DisplayIndex = 8
            dgvQueryGenerator.Columns("numFacilityLatitude").Width = "75"
            dgvQueryGenerator.Columns("strOperationalStatus").HeaderText = "Status"
            dgvQueryGenerator.Columns("strOperationalStatus").DisplayIndex = 9
            dgvQueryGenerator.Columns("strOperationalStatus").Width = "75"
            dgvQueryGenerator.Columns("strClass").HeaderText = "Classificaiton"
            dgvQueryGenerator.Columns("strClass").DisplayIndex = 10
            dgvQueryGenerator.Columns("strClass").Width = "75"
            dgvQueryGenerator.Columns("strSICCode").HeaderText = "SIC"
            dgvQueryGenerator.Columns("strSICCode").DisplayIndex = 11
            dgvQueryGenerator.Columns("strSICCode").Width = "75"
            dgvQueryGenerator.Columns("strNAICSCode").HeaderText = "NAICS"
            dgvQueryGenerator.Columns("strNAICSCode").DisplayIndex = 12
            dgvQueryGenerator.Columns("strNAICSCode").Width = "75"
            dgvQueryGenerator.Columns("strPlantDescription").HeaderText = "Plant Desc."
            dgvQueryGenerator.Columns("strPlantDescription").DisplayIndex = 13
            dgvQueryGenerator.Columns("strPlantDescription").Width = "75"
            dgvQueryGenerator.Columns("ContactType").HeaderText = "Contact Type"
            dgvQueryGenerator.Columns("ContactType").DisplayIndex = 14
            dgvQueryGenerator.Columns("ContactType").Width = "75"
            dgvQueryGenerator.Columns("strContactFirstName").HeaderText = "First Name"
            dgvQueryGenerator.Columns("strContactFirstName").DisplayIndex = 15
            dgvQueryGenerator.Columns("strContactFirstName").Width = "75"
            dgvQueryGenerator.Columns("strContactLastName").HeaderText = "Last Name"
            dgvQueryGenerator.Columns("strContactLastName").DisplayIndex = 16
            dgvQueryGenerator.Columns("strContactLastName").Width = "75"
            dgvQueryGenerator.Columns("strContactAddress1").HeaderText = "City Address"
            dgvQueryGenerator.Columns("strContactAddress1").DisplayIndex = 17
            dgvQueryGenerator.Columns("strContactAddress1").Width = "75"
            dgvQueryGenerator.Columns("strContactCity").HeaderText = "Contact City"
            dgvQueryGenerator.Columns("strContactCity").DisplayIndex = 18
            dgvQueryGenerator.Columns("strContactCity").Width = "75"
            dgvQueryGenerator.Columns("strContactState").HeaderText = "Contact State"
            dgvQueryGenerator.Columns("strContactState").DisplayIndex = 19
            dgvQueryGenerator.Columns("strContactState").Width = "75"
            dgvQueryGenerator.Columns("strContactZiPCode").HeaderText = "Contact Zip Code"
            dgvQueryGenerator.Columns("strContactZiPCode").DisplayIndex = 20
            dgvQueryGenerator.Columns("strContactZiPCode").Width = "75"
            dgvQueryGenerator.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
            dgvQueryGenerator.Columns("strContactPhoneNumber1").DisplayIndex = 21
            dgvQueryGenerator.Columns("strContactPhoneNumber1").Width = "75"
            dgvQueryGenerator.Columns("strContactEmail").HeaderText = "Email"
            dgvQueryGenerator.Columns("strContactEmail").DisplayIndex = 22
            dgvQueryGenerator.Columns("strContactEmail").Width = "75"
            dgvQueryGenerator.Columns("PermitNumber").HeaderText = "Permit #"
            dgvQueryGenerator.Columns("PermitNumber").DisplayIndex = 23
            dgvQueryGenerator.Columns("PermitNumber").Width = "75"
            dgvQueryGenerator.Columns("IssuanceDate").HeaderText = "Issued Date"
            dgvQueryGenerator.Columns("IssuanceDate").DisplayIndex = 24
            dgvQueryGenerator.Columns("IssuanceDate").Width = "75"

            txtQueryCount.Text = dgvQueryGenerator.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class